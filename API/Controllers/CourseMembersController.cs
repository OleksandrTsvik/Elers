using Application.CourseMembers.ChangeCourseMemberRole;
using Application.CourseMembers.EnrollToCourse;
using Application.CourseMembers.GetListCourseMembers;
using Application.CourseMembers.RemoveCourseMember;
using Application.CourseMembers.UnenrollFromCourse;
using Domain.Enums;
using Infrastructure.CourseMemberPermissions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CourseMembersController : ApiControllerBase
{
    [HttpGet("{courseId:guid}")]
    public async Task<IActionResult> GetListCourseMembers(
        Guid courseId,
        [FromQuery] GetListCourseMembersQueryParams queryParams,
        CancellationToken cancellationToken)
    {
        var query = new GetListCourseMembersQuery(courseId, queryParams);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpPost("enroll/{courseId:guid}")]
    public async Task<IActionResult> EnrollToCourse(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new EnrollToCourseCommand(courseId);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpPost("unenroll/{courseId:guid}")]
    public async Task<IActionResult> UnenrollFromCourse(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new UnenrollFromCourseCommand(courseId);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.ChangeCourseMemberRole],
        [PermissionType.ManageCourse])]
    [HttpPut("roles/{memberId:guid}")]
    public async Task<IActionResult> ChangeCourseMemberRole(
        Guid memberId,
        [FromBody] ChangeCourseMemberRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeCourseMemberRoleCommand(memberId, request.CourseRoleId);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.RemoveCourseMember],
        [PermissionType.ManageCourse])]
    [HttpDelete("{memberId:guid}")]
    public async Task<IActionResult> RemoveCourseMember(
        Guid memberId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveCourseMemberCommand(memberId);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
