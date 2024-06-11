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

        double points = 0;

        if (testQuestion is TestQuestionInput questionInput &&
            questionAnswer is TestSessionAnswerInput answerInput)
        {
            if (questionInput.Answer == answerInput.Answer && questionInput.Answer != request.Answer)
            {
                points = -questionInput.Points;
            }
            else if (questionInput.Answer != answerInput.Answer && questionInput.Answer == request.Answer)
            {
                points = questionInput.Points;
            }

            answerInput.Answer = request.Answer;
        }
        else if (testQuestion is TestQuestionSingleChoice questionSingleChoice &&
            questionAnswer is TestSessionAnswerSingleChoice answerSingleChoice)
        {
            string? correctAnswer = questionSingleChoice.Options
                .Where(x => x.IsCorrect)
                .Select(x => x.Option)
                .FirstOrDefault();

            if (correctAnswer == answerSingleChoice.Answer && correctAnswer != request.Answer)
            {
                points = -questionSingleChoice.Points;
            }
            else if (correctAnswer != answerSingleChoice.Answer && correctAnswer == request.Answer)
            {
                points = questionSingleChoice.Points;
            }

            answerSingleChoice.Answer = request.Answer;
        }
        else if (testQuestion is TestQuestionMultipleChoice questionMultipleChoice &&
            questionAnswer is TestSessionAnswerMultipleChoice answerMultipleChoice)
        {
            IEnumerable<string> correctAnswers = questionMultipleChoice.Options
                .Where(x => x.IsCorrect)
                .Select(x => x.Option);

            if (answerMultipleChoice.Answers is not null && request.Answers is not null &&
                answerMultipleChoice.Answers.All(x => correctAnswers.Contains(x)) &&
                !request.Answers.All(x => correctAnswers.Contains(x)))
            {
                points = -questionMultipleChoice.Points;
            }
            else if ((answerMultipleChoice.Answers is null || !answerMultipleChoice.Answers.All(x => correctAnswers.Contains(x))) &&
                request.Answers is not null &&
                request.Answers.All(x => correctAnswers.Contains(x)))
            {
                points = questionMultipleChoice.Points;
            }

            answerMultipleChoice.Answers = request.Answers;
        }
        else
        {
            return TestErrors.InvalidSessionAnswer();
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
}
