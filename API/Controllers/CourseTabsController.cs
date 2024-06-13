using Application.CourseTabs.CreateCourseTab;
using Application.CourseTabs.DeleteCourseTab;
using Application.CourseTabs.ReorderCourseTabs;
using Application.CourseTabs.UpdateCourseTab;
using Application.CourseTabs.UpdateCourseTabColor;
using Domain.Enums;
using Infrastructure.CourseMemberPermissions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CourseTabsController : ApiControllerBase
{
    [HasCourseMemberPermission(
        [CoursePermissionType.CreateCourseTab],
        [PermissionType.ManageCourse])]
    [HttpPost("{courseId:guid}")]
    public async Task<IActionResult> CreateCourseTab(
        Guid courseId,
        [FromBody] CreateCourseTabRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseTabCommand(courseId, request.Name);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseTab],
        [PermissionType.ManageCourse])]
    [HttpPut("{tabId:guid}")]
    public async Task<IActionResult> UpdateCourseTab(
        Guid tabId,
        [FromBody] UpdateCourseTabRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseTabCommand(
            tabId,
            request.Name,
            request.IsActive,
            request.ShowMaterialsCount);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseTab],
        [PermissionType.ManageCourse])]
    [HttpPatch("color/{tabId:guid}")]
    public async Task<IActionResult> UpdateCourseTabColor(
        Guid tabId,
        [FromBody] UpdateCourseTabColorRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseTabColorCommand(tabId, request.Color);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpPut("reorder")]
    public async Task<IActionResult> ReorderCourseTabs(
        [FromBody] ReorderCourseTabsRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ReorderCourseTabsCommand(request.Reorders);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.DeleteCourseTab],
        [PermissionType.ManageCourse])]
    [HttpDelete("{tabId:guid}")]
    public async Task<IActionResult> DeleteCourseTab(
        Guid tabId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCourseTabCommand(tabId);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
