using Application.Grades.CreateGread;
using Application.Grades.GetCourseGrades;
using Application.Grades.GetCourseMyGrades;
using Application.Grades.UpdateGread;
using Domain.Enums;
using Infrastructure.CourseMemberPermissions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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

    [HttpPost]
    public async Task<IActionResult> CreateGread(
        [FromBody] CreateGreadRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateGreadCommand(
            request.StudentId,
            request.AssessmentId,
            request.GradeType,
            request.Value);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.GradeCourseStudents],
        [PermissionType.GradeStudents])]
    [HttpPut("{gradeId:guid}")]
    public async Task<IActionResult> UpdateGread(
        Guid gradeId,
        [FromBody] UpdateGreadRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateGreadCommand(gradeId, request.Value);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
