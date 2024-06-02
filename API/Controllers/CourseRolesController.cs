using Application.CourseRoles.CreateCourseRole;
using Application.CourseRoles.DeleteCourseRole;
using Application.CourseRoles.GetListCourseRoles;
using Application.CourseRoles.UpdateCourseRole;
using Domain.Enums;
using Infrastructure.CourseMemberPermissions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CourseRolesController : ApiControllerBase
{
    [HasCourseMemberPermission(
        [
            CoursePermissionType.CreateCourseRole,
            CoursePermissionType.UpdateCourseRole,
            CoursePermissionType.DeleteCourseRole,
            CoursePermissionType.ChangeCourseMemberRole,
        ],
        [PermissionType.ManageCourse])]
    [HttpGet("{courseId:guid}")]
    public async Task<IActionResult> GetListCourseRoles(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var query = new GetListCourseRolesQuery(courseId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.CreateCourseRole],
        [PermissionType.ManageCourse])]
    [HttpPost("{courseId:guid}")]
    public async Task<IActionResult> CreateCourseRole(
        Guid courseId,
        [FromBody] CreateCourseRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseRoleCommand(courseId, request.Name, request.PermissionIds);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseRole],
        [PermissionType.ManageCourse])]
    [HttpPut("{roleId:guid}")]
    public async Task<IActionResult> UpdateCourseRole(
        Guid roleId,
        [FromBody] UpdateCourseRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseRoleCommand(roleId, request.Name, request.PermissionIds);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.DeleteCourseRole],
        [PermissionType.ManageCourse])]
    [HttpDelete("{roleId:guid}")]
    public async Task<IActionResult> DeleteCourseRole(
        Guid roleId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCourseRoleCommand(roleId);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
