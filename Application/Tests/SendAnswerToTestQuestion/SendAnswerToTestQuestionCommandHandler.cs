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
    private readonly IGradeRepository _gradeRepository;
    private readonly ITestQueries _testQueries;
    private readonly ICourseQueries _courseQueries;
    private readonly IUserContext _userContext;

    public SendAnswerToTestQuestionCommandHandler(
        ITestQuestionRepository testQuestionRepository,
        ITestSessionRespository testSessionRespository,
        ICourseMaterialRepository courseMaterialRepository,
        IGradeRepository gradeRepository,
        ITestQueries testQueries,
        ICourseQueries courseQueries,
        IUserContext userContext)
    {
        _testQuestionRepository = testQuestionRepository;
        _testSessionRespository = testSessionRespository;
        _courseMaterialRepository = courseMaterialRepository;
        _gradeRepository = gradeRepository;
        _testQueries = testQueries;
        _courseQueries = courseQueries;
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

        bool isPreviousAnswerCorrect = false;
        bool isCurrentAnswerCorrect = false;

        if (testQuestion is TestQuestionInput questionInput &&
            questionAnswer is TestSessionAnswerInput answerInput)
        {
            isPreviousAnswerCorrect = IsAnswerInputCorrect(questionInput, answerInput.Answer);
            isCurrentAnswerCorrect = IsAnswerInputCorrect(questionInput, request.Answer);

            answerInput.Answer = request.Answer;
        }
        else if (testQuestion is TestQuestionSingleChoice questionSingleChoice &&
            questionAnswer is TestSessionAnswerSingleChoice answerSingleChoice)
        {
            isPreviousAnswerCorrect = IsAnswerSingleChoiceCorrect(
                questionSingleChoice, answerSingleChoice.Answer);

            isCurrentAnswerCorrect = IsAnswerSingleChoiceCorrect(questionSingleChoice, request.Answer);

            answerSingleChoice.Answer = request.Answer;
        }
        else if (testQuestion is TestQuestionMultipleChoice questionMultipleChoice &&
            questionAnswer is TestSessionAnswerMultipleChoice answerMultipleChoice)
        {
            isPreviousAnswerCorrect = IsAnswerMultipleChoiceCorrect(
                questionMultipleChoice, answerMultipleChoice.Answers);

            isCurrentAnswerCorrect = IsAnswerMultipleChoiceCorrect(questionMultipleChoice, request.Answers);

            answerMultipleChoice.Answers = request.Answers;
        }
        else if (testQuestion is TestQuestionMatching questionMatching &&
            questionAnswer is TestSessionAnswerMatching answerMatching)
        {
            isPreviousAnswerCorrect = IsAnswerMatchingCorrect(questionMatching, answerMatching.MatchOptions);
            isCurrentAnswerCorrect = IsAnswerMatchingCorrect(questionMatching, request.MatchOptions);

            answerMatching.MatchOptions = request.MatchOptions;
        }
        else
        {
            return TestErrors.InvalidSessionAnswer();
        }

        double points = 0;

        if (isPreviousAnswerCorrect && !isCurrentAnswerCorrect)
        {
            points = -testQuestion.Points;
        }
        else if (!isPreviousAnswerCorrect && isCurrentAnswerCorrect)
        {
            points = testQuestion.Points;
        }

        await _testSessionRespository.UpdateAnswerAsync(
            request.TestSessionId, questionAnswer, cancellationToken);

        GradeTest? grade = await _gradeRepository.GetByTestIdAndStudentIdAsync(
            test.Id, testSession.UserId, cancellationToken);

        if (grade is not null)
        {
            GradeTestItem? gradeTestItem = grade.Values.Find(x => x.TestSessionId == testSession.Id);

            if (gradeTestItem is not null)
            {
                gradeTestItem.Value += points;
            }
            else
            {
                gradeTestItem = new GradeTestItem
                {
                    TestSessionId = testSession.Id,
                    Value = points
                };

                grade.Values.Add(gradeTestItem);
            }

            await _gradeRepository.UpdateAsync(grade, cancellationToken);
        }
        else
        {
            Guid? courseId = await _courseQueries.GetCourseIdByCourseTabId(
                test.CourseTabId, cancellationToken);

            if (!courseId.HasValue)
            {
                return CourseErrors.NotFoundByTabId(test.CourseTabId);
            }

            grade = new GradeTest
            {
                CourseId = courseId.Value,
                StudentId = _userContext.UserId,
                TestId = test.Id,
                Values = [new GradeTestItem { TestSessionId = testSession.Id, Value = points }]
            };

            await _gradeRepository.AddAsync(grade, cancellationToken);
        }

        return Result.Success();
    }

private static bool IsAnswerInputCorrect(TestQuestionInput question, string? answer) =>
    question.Answer == answer;

private static bool IsAnswerSingleChoiceCorrect(TestQuestionSingleChoice question, string? answer) =>
    question.Options.FirstOrDefault(x => x.IsCorrect)?.Option == answer;

private static bool IsAnswerMultipleChoiceCorrect(
    TestQuestionMultipleChoice question,
    List<string>? answers) =>
    answers is not null &&
    question.Options.Where(x => x.IsCorrect).Select(x => x.Option)
        .All(x => answers.Contains(x));

private static bool IsAnswerMatchingCorrect(
    TestQuestionMatching question,
    List<AnswerMatchOption>? matchOptions) =>
    matchOptions is not null &&
    question.Options.Where(x => !string.IsNullOrEmpty(x.Question))
        .All(x => matchOptions
            .Any(answer => answer.Question == x.Question && answer.Answer == x.Answer));
}
