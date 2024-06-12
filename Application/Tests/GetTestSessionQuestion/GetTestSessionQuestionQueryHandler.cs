using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Tests.GetTestSessionQuestion;

public class GetTestSessionQuestionQueryHandler
    : IQueryHandler<GetTestSessionQuestionQuery, GetTestSessionQuestionResponse>
{
    private readonly ITestQuestionRepository _testQuestionRepository;
    private readonly ITestSessionRespository _testSessionRespository;
    private readonly IUserContext _userContext;

    public GetTestSessionQuestionQueryHandler(
        ITestQuestionRepository testQuestionRepository,
        ITestSessionRespository testSessionRespository,
        IUserContext userContext)
    {
        _testQuestionRepository = testQuestionRepository;
        _testSessionRespository = testSessionRespository;
        _userContext = userContext;
    }

    public async Task<Result<GetTestSessionQuestionResponse>> Handle(
        GetTestSessionQuestionQuery request,
        CancellationToken cancellationToken)
    {
        if (!await _testSessionRespository.ExistsByIdAndUserIdAsync(
            request.TestSessionId, _userContext.UserId, cancellationToken))
        {
            return TestErrors.UserSessionNotFound();
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
            return new GetTestSessionQuestionInputResponse
            {
                QuestionId = questionInput.Id,
                QuestionType = questionInput.Type,
                QuestionText = questionInput.Text,
                Points = questionInput.Points,
                UserAnswer = answerInput.Answer
            };
        }
        else if (testQuestion is TestQuestionSingleChoice questionSingleChoice &&
            questionAnswer is TestSessionAnswerSingleChoice answerSingleChoice)
        {
            return new GetTestSessionQuestionSingleChoiceResponse
            {
                QuestionId = questionSingleChoice.Id,
                QuestionType = questionSingleChoice.Type,
                QuestionText = questionSingleChoice.Text,
                Points = questionSingleChoice.Points,
                Options = questionSingleChoice.Options.Select(x => x.Option).ToArray(),
                UserAnswer = answerSingleChoice.Answer
            };
        }
        else if (testQuestion is TestQuestionMultipleChoice questionMultipleChoice &&
            questionAnswer is TestSessionAnswerMultipleChoice answerMultipleChoice)
        {
            return new GetTestSessionQuestionMultipleChoiceResponse
            {
                QuestionId = questionMultipleChoice.Id,
                QuestionType = questionMultipleChoice.Type,
                QuestionText = questionMultipleChoice.Text,
                Points = questionMultipleChoice.Points,
                Options = questionMultipleChoice.Options.Select(x => x.Option).ToArray(),
                UserAnswers = answerMultipleChoice.Answers
            };
        }
        else if (testQuestion is TestQuestionMatching questionMatching &&
            questionAnswer is TestSessionAnswerMatching answerMatching)
        {
            var random = new Random();

            return new GetTestSessionQuestionMatchingResponse
            {
                QuestionId = questionMatching.Id,
                QuestionType = questionMatching.Type,
                QuestionText = questionMatching.Text,
                Points = questionMatching.Points,
                Questions = questionMatching.Options
                    .Where(x => !string.IsNullOrEmpty(x.Question))
                    .Select(x => x.Question!)
                    .ToArray(),
                Answers = questionMatching.Options
                    .Select(x => x.Answer)
                    .OrderBy(_ => random.Next())
                    .ToArray(),
                UserAnswers = answerMatching.MatchOptions
            };
        }

        return TestErrors.InvalidSessionAnswer();
    }
}
