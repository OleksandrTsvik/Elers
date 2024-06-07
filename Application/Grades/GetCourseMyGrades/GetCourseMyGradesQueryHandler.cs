using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Grades.DTOs;
using Application.Users.DTOs;
using Domain.Entities;
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
    private readonly IUserContext _userContext;
    private UserDto[] _teachers = [];

    public GetCourseMyGradesQueryHandler(
        ICourseRepository courseRepository,
        IGradeQueries gradeQueries,
        IGradeRepository gradeRepository,
        IUserQueries userQueries,
        IUserContext userContext)
    {
        _courseRepository = courseRepository;
        _gradeQueries = gradeQueries;
        _gradeRepository = gradeRepository;
        _userQueries = userQueries;
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

        IEnumerable<Guid> teacherIds = grades.OfType<GradeAssignment>().Select(x => x.TeacherId);
        _teachers = await _userQueries.GetUserDtosByIds(teacherIds, cancellationToken);

        List<CourseMyGrade> mappedGrades = MapGrades(grades);

        GetCourseMyGradeItemResponse[] response = assessments
            .Select(x => new GetCourseMyGradeItemResponse
            {
                Assessment = x,
                Grade = mappedGrades.FirstOrDefault(grade => grade.AssessmentId == x.Id)
            })
            .ToArray();

        return response;
    }

    private List<CourseMyGrade> MapGrades(IEnumerable<Grade> grades)
    {
        var result = new List<CourseMyGrade>(grades.Count());

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
                    Teacher = _teachers.FirstOrDefault(x => x.Id == gradeAssignment.TeacherId)
                };
            }
            else if (grade is GradeTest gradeTest)
            {
                myGrade = new CourseMyGrade
                {
                    AssessmentId = gradeTest.TestId,
                    Grade = gradeTest.Value,
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
}
