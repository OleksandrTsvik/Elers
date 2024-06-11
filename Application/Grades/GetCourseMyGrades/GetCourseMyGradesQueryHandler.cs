using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Grades.DTOs;
using Application.Tests.DTOs;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Grades.GetCourseMyGrades;

public class GetCourseMyGradesQueryHandler
    : IQueryHandler<GetCourseMyGradesQuery, GetCourseMyGradeItemResponse[]>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IGradeQueries _gradeQueries;
    private readonly IGradeRepository _gradeRepository;
    private readonly IUserQueries _userQueries;
    private readonly ITestQueries _testQueries;
    private readonly IUserContext _userContext;

    public GetCourseMyGradesQueryHandler(
        ICourseRepository courseRepository,
        IGradeQueries gradeQueries,
        IGradeRepository gradeRepository,
        IUserQueries userQueries,
        ITestQueries testQueries,
        IUserContext userContext)
    {
        _courseRepository = courseRepository;
        _gradeQueries = gradeQueries;
        _gradeRepository = gradeRepository;
        _userQueries = userQueries;
        _testQueries = testQueries;
        _userContext = userContext;
    }

    public async Task<Result<GetCourseMyGradeItemResponse[]>> Handle(
        GetCourseMyGradesQuery request,
        CancellationToken cancellationToken)
    {
        if (!await _courseRepository.ExistsByIdAsync(request.CourseId, cancellationToken))
        {
            return CourseErrors.NotFound(request.CourseId);
        }

        List<AssessmentItem> assessments = await _gradeQueries.GetAssessments(
            request.CourseId, cancellationToken);

        List<Grade> grades = await _gradeRepository.GetByCourseIdAndStudentIdAsync(
            request.CourseId, _userContext.UserId, cancellationToken);

        List<CourseMyGrade> mappedGrades = await MapGradesAsync(grades, cancellationToken);

        GetCourseMyGradeItemResponse[] response = assessments
            .Select(x => new GetCourseMyGradeItemResponse
            {
                Assessment = x,
                Grade = mappedGrades.FirstOrDefault(grade => grade.AssessmentId == x.Id)
            })
            .ToArray();

        return response;
    }

    private async Task<List<CourseMyGrade>> MapGradesAsync(
        IEnumerable<Grade> grades,
        CancellationToken cancellationToken)
    {
        var result = new List<CourseMyGrade>(grades.Count());

        IEnumerable<Guid> teacherIds = grades.OfType<GradeAssignment>().Select(x => x.TeacherId);
        UserDto[] teachers = await _userQueries.GetUserDtosByIds(teacherIds, cancellationToken);

        IEnumerable<Guid> testIds = grades.OfType<GradeTest>().Select(x => x.TestId);
        List<CourseTestMyGrade> tests = await _testQueries.GetCourseTestMyGradesByIds(
            testIds, cancellationToken);

        IEnumerable<Guid> testSessionIds = grades.OfType<GradeTest>()
            .SelectMany(x => x.Values.Select(value => value.TestSessionId))
            .Distinct();

        List<TestSessionDto> testSessions = await _testQueries.GetTestSessionDtosByIds(
            testSessionIds, cancellationToken);

        foreach (Grade grade in grades)
        {
            CourseMyGrade myGrade;

            if (grade is GradeAssignment gradeAssignment)
            {
                myGrade = new CourseMyGradeAssignment
                {
                    AssessmentId = gradeAssignment.AssignmentId,
                    Grade = gradeAssignment.Value,
                    Type = gradeAssignment.Type,
                    CreatedAt = gradeAssignment.CreatedAt,
                    Teacher = teachers.FirstOrDefault(x => x.Id == gradeAssignment.TeacherId)
                };
            }
            else if (grade is GradeTest gradeTest)
            {
                myGrade = new CourseMyGrade
                {
                    AssessmentId = gradeTest.TestId,
                    Grade = GetGradeTestValue(
                        tests.FirstOrDefault(x => x.TestId == gradeTest.TestId),
                        testSessions.Where(x => gradeTest.Values.Any(value => value.TestSessionId == x.Id)),
                        gradeTest),
                    Type = gradeTest.Type,
                    CreatedAt = gradeTest.CreatedAt
                };
            }
            else
            {
                continue;
            }

            result.Add(myGrade);
        }

        return result;
    }

    private static double? GetGradeTestValue(
        CourseTestMyGrade? test,
        IEnumerable<TestSessionDto> testSessions,
        GradeTest gradeTest)
    {
        if (test is null || gradeTest.Values.Count == 0)
        {
            return null;
        }

        IEnumerable<TestSessionDto> availableTestSessions = testSessions
            .Where(x => x.FinishedAt is not null || (test.TimeLimitInMinutes.HasValue &&
                x.StartedAt.AddMinutes(test.TimeLimitInMinutes.Value) < DateTime.UtcNow));

        IEnumerable<double> availableGradeValues = gradeTest.Values
            .Where(x => availableTestSessions.Any(session => session.Id == x.TestSessionId))
            .Select(x => x.Value);

        if (!availableGradeValues.Any())
        {
            return null;
        }

        return gradeTest.GradingMethod switch
        {
            GradingMethod.LastAttempt => availableGradeValues.Last(),
            GradingMethod.BestAttempt => availableGradeValues.Max(),
            _ => null
        };
    }
}
