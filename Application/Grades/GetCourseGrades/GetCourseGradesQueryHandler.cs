using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Grades.DTOs;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Grades.GetCourseGrades;

public class GetCourseGradesQueryHandler : IQueryHandler<GetCourseGradesQuery, GetCourseGradesResponse>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IGradeQueries _gradeQueries;
    private readonly IGradeRepository _gradeRepository;
    private readonly IStudentQueries _studentQueries;
    private readonly IUserQueries _userQueries;
    private UserDto[] _teachers = [];

    public GetCourseGradesQueryHandler(
        ICourseRepository courseRepository,
        IGradeQueries gradeQueries,
        IGradeRepository gradeRepository,
        IStudentQueries studentQueries,
        IUserQueries userQueries)
    {
        _courseRepository = courseRepository;
        _gradeQueries = gradeQueries;
        _gradeRepository = gradeRepository;
        _studentQueries = studentQueries;
        _userQueries = userQueries;
    }

    public async Task<Result<GetCourseGradesResponse>> Handle(
        GetCourseGradesQuery request,
        CancellationToken cancellationToken)
    {
        if (!await _courseRepository.ExistsByIdAsync(request.CourseId, cancellationToken))
        {
            return CourseErrors.NotFound(request.CourseId);
        }

        List<AssessmentItem> assessments = await _gradeQueries.GetAssessments(
            request.CourseId, false, cancellationToken);

        UserDto[] students = await _studentQueries.GetCourseStudents(request.CourseId, cancellationToken);

        List<Grade> grades = await _gradeRepository.GetByCourseIdAsync(request.CourseId, cancellationToken);

        var teacherIds = grades.OfType<GradeAssignment>().Select(x => x.TeacherId).ToList();
        teacherIds.AddRange(grades.OfType<GradeManual>().Select(x => x.TeacherId));

        _teachers = await _userQueries.GetUserDtosByIds(teacherIds, cancellationToken);

        var studentGrades = new List<CourseGradeItemResponse>(students.Length);

        foreach (UserDto student in students)
        {
            studentGrades.Add(new CourseGradeItemResponse
            {
                Student = student,
                Grades = MapStudentGrades(grades.Where(x => x.StudentId == student.Id))
            });
        }

        return new GetCourseGradesResponse
        {
            Assessments = assessments,
            StudentGrades = studentGrades
        };
    }

    private List<GradeItemResponse> MapStudentGrades(IEnumerable<Grade> studentGrades)
    {
        var grades = new List<GradeItemResponse>(studentGrades.Count());

        foreach (Grade studentGrade in studentGrades)
        {
            GradeItemResponse grade;

            if (studentGrade is GradeAssignment gradeAssignment)
            {
                grade = new GradeAssignmentItemResponse
                {
                    AssessmentId = gradeAssignment.AssignmentId,
                    GradeId = gradeAssignment.Id,
                    Grade = gradeAssignment.Value,
                    Type = gradeAssignment.Type,
                    CreatedAt = gradeAssignment.CreatedAt,
                    Teacher = _teachers.FirstOrDefault(x => x.Id == gradeAssignment.TeacherId)
                };
            }
            else if (studentGrade is GradeTest gradeTest)
            {
                grade = new GradeItemResponse
                {
                    AssessmentId = gradeTest.TestId,
                    GradeId = gradeTest.Id,
                    Grade = GetGradeTestValue(gradeTest),
                    Type = gradeTest.Type,
                    CreatedAt = gradeTest.CreatedAt
                };
            }
            else if (studentGrade is GradeManual gradeManual)
            {
                grade = new GradeManualItemResponse
                {
                    AssessmentId = gradeManual.ManualGradesColumnId,
                    GradeId = gradeManual.Id,
                    Grade = gradeManual.Value,
                    Type = gradeManual.Type,
                    CreatedAt = gradeManual.CreatedAt,
                    Teacher = _teachers.FirstOrDefault(x => x.Id == gradeManual.TeacherId),
                };
            }
            else
            {
                continue;
            }

            grades.Add(grade);
        }

        return grades;
    }

    private static double? GetGradeTestValue(GradeTest gradeTest) =>
        gradeTest.GradingMethod switch
        {
            GradingMethod.LastAttempt => gradeTest.Values.Count != 0 ? gradeTest.Values.Last().Value : null,
            GradingMethod.BestAttempt => gradeTest.Values.Count != 0
                ? gradeTest.Values.Max(x => x.Value)
                : null,
            _ => null
        };
}
