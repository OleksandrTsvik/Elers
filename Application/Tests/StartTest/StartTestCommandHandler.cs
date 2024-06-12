using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Common.Services;
using Application.TestQuestions.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Tests.StartTest;

public class StartTestCommandHandler : ICommandHandler<StartTestCommand, Guid>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ITestSessionRespository _testSessionRespository;
    private readonly ITestQuestionQueries _testQuestionQueries;
    private readonly ICourseMemberService _courseMemberService;
    private readonly IUserContext _userContext;

    public StartTestCommandHandler(
        ICourseMaterialRepository courseMaterialRepository,
        ITestSessionRespository testSessionRespository,
        ITestQuestionQueries testQuestionQueries,
        ICourseMemberService courseMemberService,
        IUserContext userContext)
    {
        _courseMaterialRepository = courseMaterialRepository;
        _testSessionRespository = testSessionRespository;
        _testQuestionQueries = testQuestionQueries;
        _courseMemberService = courseMemberService;
        _userContext = userContext;
    }

    public async Task<Result<Guid>> Handle(StartTestCommand request, CancellationToken cancellationToken)
    {
        CourseMaterialTest? test = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialTest>(request.TestId, cancellationToken);

        if (test is null)
        {
            return TestErrors.NotFound(request.TestId);
        }

        if (!await _courseMemberService.IsCourseMemberByCourseTabIdAsync(
            _userContext.UserId, test.CourseTabId, cancellationToken))
        {
            return TestErrors.StudentsOnly();
        }

        if (!test.IsActive)
        {
            return TestErrors.NotActive();
        }

        if (test.Deadline.HasValue && test.Deadline.Value.AddDays(1).Date < DateTime.UtcNow.Date)
        {
            return TestErrors.DeadlinePassed();
        }

        if (test.NumberAttempts <= await _testSessionRespository.GetAttemptsCountAsync(
            request.TestId, _userContext.UserId, cancellationToken))
        {
            return TestErrors.AttemptsExceeded();
        }

        if (await _testSessionRespository.ExistsActiveSessionAsync(
            request.TestId, _userContext.UserId, test.TimeLimitInMinutes, cancellationToken))
        {
            return TestErrors.UnfinishedAttempts();
        }

        List<TestQuestionIdsAndTypesDto> questionIdsAndTypes = await _testQuestionQueries
            .GetTestQuestionIdsAndTypesByTestId(request.TestId, cancellationToken);

        if (questionIdsAndTypes.Count == 0)
        {
            return TestErrors.NoQuestions(request.TestId);
        }

        List<TestSessionAnswer> sessionAnswers = GetSessionAnswers(questionIdsAndTypes);

        if (test.ShuffleQuestions)
        {
            var random = new Random();
            sessionAnswers = sessionAnswers.OrderBy(_ => random.Next()).ToList();
        }

        var testSession = new TestSession
        {
            TestId = request.TestId,
            UserId = _userContext.UserId,
            Answers = sessionAnswers
        };

        await _testSessionRespository.AddAsync(testSession, cancellationToken);

        return testSession.Id;
    }

    private static List<TestSessionAnswer> GetSessionAnswers(
        List<TestQuestionIdsAndTypesDto> questionIdsAndTypes)
    {
        var sessionAnswers = new List<TestSessionAnswer>();

        foreach (TestQuestionIdsAndTypesDto question in questionIdsAndTypes)
        {
            switch (question.Type)
            {
                case TestQuestionType.Input:
                    sessionAnswers.Add(new TestSessionAnswerInput
                    {
                        QuestionId = question.Id,
                        QuestionType = question.Type
                    });
                    break;
                case TestQuestionType.SingleChoice:
                    sessionAnswers.Add(new TestSessionAnswerSingleChoice
                    {
                        QuestionId = question.Id,
                        QuestionType = question.Type
                    });
                    break;
                case TestQuestionType.MultipleChoice:
                    sessionAnswers.Add(new TestSessionAnswerMultipleChoice
                    {
                        QuestionId = question.Id,
                        QuestionType = question.Type
                    });
                    break;
                case TestQuestionType.Matching:
                    sessionAnswers.Add(new TestSessionAnswerMatching
                    {
                        QuestionId = question.Id,
                        QuestionType = question.Type
                    });
                    break;
            }
        }

        return sessionAnswers;
    }
}
