using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Tests.GetTestSession;

public class GetTestSessionQueryHandler : IQueryHandler<GetTestSessionQuery, GetTestSessionResponse>
{
    private readonly ITestSessionRespository _testSessionRespository;
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly IUserContext _userContext;

    public GetTestSessionQueryHandler(
        ITestSessionRespository testSessionRespository,
        ICourseMaterialRepository courseMaterialRepository,
        IUserContext userContext)
    {
        _testSessionRespository = testSessionRespository;
        _courseMaterialRepository = courseMaterialRepository;
        _userContext = userContext;
    }

    public async Task<Result<GetTestSessionResponse>> Handle(
        GetTestSessionQuery request,
        CancellationToken cancellationToken)
    {
        TestSession? testSession = await _testSessionRespository.GetByIdAndUserIdAsync(
            request.TestSessionId, _userContext.UserId, cancellationToken);

        if (testSession is null)
        {
            return TestErrors.UserSessionNotFound();
        }

        if (testSession.Answers.Count == 0)
        {
            return TestErrors.NoQuestions(testSession.TestId);
        }

        CourseMaterialTest? test = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialTest>(testSession.TestId, cancellationToken);

        if (test is null)
        {
            return TestErrors.NotFound(testSession.TestId);
        }

        if (!test.IsActive)
        {
            return TestErrors.NotActive();
        }

        if (testSession.FinishedAt is not null ||
            (test.TimeLimitInMinutes.HasValue &&
            testSession.StartedAt.AddMinutes(test.TimeLimitInMinutes.Value) < DateTime.UtcNow))
        {
            return TestErrors.AttemptExpired();
        }

        if (test.Deadline.HasValue && test.Deadline.Value.AddDays(1).Date < DateTime.UtcNow.Date)
        {
            return TestErrors.DeadlinePassed();
        }

        return new GetTestSessionResponse
        {
            TestSessionId = testSession.Id,
            TestId = test.Id,
            StartedAt = testSession.StartedAt,
            TimeLimitInMinutes = test.TimeLimitInMinutes,
            Questions = GetTestSessionQuestions(testSession.Answers)
        };
    }

    private static List<TestSessionQuestionItem> GetTestSessionQuestions(List<TestSessionAnswer> answers)
    {
        var testSessionQuestions = new List<TestSessionQuestionItem>(answers.Count);

        foreach (TestSessionAnswer answer in answers)
        {
            TestSessionQuestionItem item;

            if (answer is TestSessionAnswerInput answerInput)
            {
                item = new TestSessionQuestionItem
                {
                    QuestionId = answerInput.QuestionId,
                    IsAnswered = !string.IsNullOrEmpty(answerInput.Answer),
                };
            }
            else if (answer is TestSessionAnswerSingleChoice answerSingleChoice)
            {
                item = new TestSessionQuestionItem
                {
                    QuestionId = answerSingleChoice.QuestionId,
                    IsAnswered = !string.IsNullOrEmpty(answerSingleChoice.Answer),
                };
            }
            else if (answer is TestSessionAnswerMultipleChoice answerMultipleChoice)
            {
                item = new TestSessionQuestionItem
                {
                    QuestionId = answerMultipleChoice.QuestionId,
                    IsAnswered = answerMultipleChoice.Answers is not null &&
                        answerMultipleChoice.Answers.Count != 0,
                };
            }
            else
            {
                continue;
            }

            testSessionQuestions.Add(item);
        }

        return testSessionQuestions;
    }
}
