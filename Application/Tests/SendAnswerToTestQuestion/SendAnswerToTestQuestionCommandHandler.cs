using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Tests.DTOs;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Tests.SendAnswerToTestQuestion;

public class SendAnswerToTestQuestionCommandHandler : ICommandHandler<SendAnswerToTestQuestionCommand>
{
    private readonly ITestQuestionRepository _testQuestionRepository;
    private readonly ITestSessionRespository _testSessionRespository;
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ITestQueries _testQueries;
    private readonly IUserContext _userContext;

    public SendAnswerToTestQuestionCommandHandler(
        ITestQuestionRepository testQuestionRepository,
        ITestSessionRespository testSessionRespository,
        ICourseMaterialRepository courseMaterialRepository,
        ITestQueries testQueries,
        IUserContext userContext)
    {
        _testQuestionRepository = testQuestionRepository;
        _testSessionRespository = testSessionRespository;
        _courseMaterialRepository = courseMaterialRepository;
        _testQueries = testQueries;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        SendAnswerToTestQuestionCommand request,
        CancellationToken cancellationToken)
    {
        TestSessionDto? testSession = await _testQueries.GetTestSessionDtoByIdAndUserId(
            request.TestSessionId, _userContext.UserId, cancellationToken);

        if (testSession is null)
        {
            return TestErrors.UserSessionNotFound();
        }

        CourseMaterialTest? test = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialTest>(testSession.TestId, cancellationToken);

        if (test is null)
        {
            return TestErrors.NotFound(testSession.TestId);
        }

        if (testSession.FinishedAt is not null ||
            (test.TimeLimitInMinutes.HasValue &&
            testSession.StartedAt.AddMinutes(test.TimeLimitInMinutes.Value) < DateTime.UtcNow))
        {
            return TestErrors.AttemptExpired();
        }

        if (!test.IsActive)
        {
            return TestErrors.NotActive();
        }

        if (test.Deadline.HasValue && test.Deadline.Value.AddDays(1).Date < DateTime.UtcNow.Date)
        {
            return TestErrors.DeadlinePassed();
        }

        TestQuestion? testQuestion = await _testQuestionRepository.GetByIdAsync(
            request.TestQuestionId, cancellationToken);

        if (testQuestion is null)
        {
            return TestQuestionErrors.NotFound(request.TestQuestionId);
        }

        TestSessionAnswer? questionAnswer = await _testSessionRespository.GetQuestionAnswerByIdAsync(
            request.TestSessionId, request.TestQuestionId, cancellationToken);

        if (questionAnswer is null)
        {
            return TestErrors.NotFoundAnswer();
        }

        if (testQuestion is TestQuestionInput questionInput &&
            questionAnswer is TestSessionAnswerInput answerInput)
        {
            answerInput.Answer = request.Answer;

            // await _testSessionRespository.UpdateAnswerAsync(
            //     request.TestSessionId, answerInput, cancellationToken);
        }
        else if (testQuestion is TestQuestionSingleChoice questionSingleChoice &&
            questionAnswer is TestSessionAnswerSingleChoice answerSingleChoice)
        {
            answerSingleChoice.Answer = request.Answer;

            // await _testSessionRespository.UpdateAnswerAsync(
            //     request.TestSessionId, answerSingleChoice, cancellationToken);
        }
        else if (testQuestion is TestQuestionMultipleChoice questionMultipleChoice &&
            questionAnswer is TestSessionAnswerMultipleChoice answerMultipleChoice)
        {
            answerMultipleChoice.Answers = request.Answers;

            // await _testSessionRespository.UpdateAnswerAsync(
            //     request.TestSessionId, answerMultipleChoice, cancellationToken);
        }
        else
        {
            return TestErrors.InvalidSessionAnswer();
        }

        await _testSessionRespository.UpdateAnswerAsync(
            request.TestSessionId, questionAnswer, cancellationToken);

        return Result.Success();
    }
}
