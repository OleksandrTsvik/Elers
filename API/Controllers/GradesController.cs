using Application.Grades.GetCourseGrades;
using Application.Grades.GetCourseMyGrades;
using Domain.Enums;
using Infrastructure.CourseMemberPermissions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class GradesController : ApiControllerBase
{
    [HasCourseMemberPermission(
        [CoursePermissionType.GradeCourseStudents],
        [PermissionType.GradeStudents])]
    [HttpGet("course/{courseId:guid}")]
    public async Task<IActionResult> GetCourseGrades(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseGradesQuery(courseId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpGet("course/my/{courseId:guid}")]
    public async Task<IActionResult> GetCourseMyGrades(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseMyGradesQuery(courseId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }
}
