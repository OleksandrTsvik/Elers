using Application.ManualGradesColumns.CreateManualGradesColumn;
using Application.ManualGradesColumns.DeleteManualGradesColumn;
using Application.ManualGradesColumns.UpdateManualGradesColumn;
using Domain.Enums;
using Infrastructure.CourseMemberPermissions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ManualGradesColumnsController : ApiControllerBase
{
    [HasCourseMemberPermission(
        [CoursePermissionType.GradeCourseStudents],
        [PermissionType.GradeStudents])]
    [HttpPost("{courseId:guid}")]
    public async Task<IActionResult> CreateManualGradesColumn(
        Guid courseId,
        [FromBody] CreateManualGradesColumnRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateManualGradesColumnCommand(
            courseId,
            request.Title,
            request.Date,
            request.MaxGrade);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.GradeCourseStudents],
        [PermissionType.GradeStudents])]
    [HttpPut("{columnGradesId:guid}")]
    public async Task<IActionResult> UpdateManualGradesColumn(
        Guid columnGradesId,
        [FromBody] UpdateManualGradesColumnRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateManualGradesColumnCommand(
            columnGradesId,
            request.Title,
            request.Date,
            request.MaxGrade);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.GradeCourseStudents],
        [PermissionType.GradeStudents])]
    [HttpDelete("{columnGradesId:guid}")]
    public async Task<IActionResult> DeleteManualGradesColumn(
        Guid columnGradesId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteManualGradesColumnCommand(columnGradesId);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
