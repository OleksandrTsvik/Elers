using Api.Contracts;
using Application.Courses.ChangeCourseImage;
using Application.Courses.CreateCourse;
using Application.Courses.DeleteCourse;
using Application.Courses.DeleteCourseImage;
using Application.Courses.GetCourseById;
using Application.Courses.GetCourseByIdToEdit;
using Application.Courses.GetCourseByTabId;
using Application.Courses.GetListCourses;
using Application.Courses.GetMyCourses;
using Application.Courses.UpdateCourseDescription;
using Application.Courses.UpdateCourseTabType;
using Application.Courses.UpdateCourseTitle;
using Domain.Enums;
using Infrastructure.Authentication;
using Infrastructure.CourseMemberPermissions;
using Infrastructure.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class CoursesController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCourseById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseByIdQuery(id);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetListCourses(
        [FromQuery] GetListCoursesQueryParams queryParams,
        CancellationToken cancellationToken)
    {
        var query = new GetListCoursesQuery(queryParams);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyCourses(
        [FromQuery] GetMyCoursesQueryParams queryParams,
        CancellationToken cancellationToken)
    {
        var query = new GetMyCoursesQuery(queryParams);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasCourseMemberPermission(
        [
            CoursePermissionType.CreateCourseMaterial,
            CoursePermissionType.CreateCourseTab,
            CoursePermissionType.DeleteCourseMaterial,
            CoursePermissionType.DeleteCourseTab,
            CoursePermissionType.UpdateCourse,
            CoursePermissionType.UpdateCourseMaterial,
            CoursePermissionType.UpdateCourseTab,
            CoursePermissionType.UpdateCourseImage
        ],
        [PermissionType.ManageCourse])]
    [HttpGet("edit/{courseId:guid}")]
    public async Task<IActionResult> GetCourseByIdToEdit(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseByIdToEditQuery(courseId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.CreateCourseMaterial],
        [PermissionType.ManageCourse])]
    [HttpGet("tab/{tabId:guid}")]
    public async Task<IActionResult> GetCourseByTabId(
        Guid tabId,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseByTabIdQuery(tabId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasPermission(PermissionType.CreateCourse)]
    [HttpPost]
    public async Task<IActionResult> CreateCourse(
        [FromBody] CreateCourseRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseCommand(
            request.Title,
            request.Description,
            request.TabType);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourse],
        [PermissionType.ManageCourse])]
    [HttpPatch("title/{courseId:guid}")]
    public async Task<IActionResult> UpdateCourseTitle(
        Guid courseId,
        [FromBody] UpdateCourseTitleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseTitleCommand(courseId, request.Title);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourse],
        [PermissionType.ManageCourse])]
    [HttpPatch("description/{courseId:guid}")]
    public async Task<IActionResult> UpdateCourseDescription(
        Guid courseId,
        [FromBody] UpdateCourseDescriptionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseDescriptionCommand(courseId, request.Description);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourse],
        [PermissionType.ManageCourse])]
    [HttpPatch("tab-type/{courseId:guid}")]
    public async Task<IActionResult> UpdateCourseTabType(
        Guid courseId,
        [FromBody] UpdateCourseTabTypeRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseTabTypeCommand(courseId, request.TabType);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseImage],
        [PermissionType.ManageCourse])]
    [HttpPatch("image/{courseId:guid}")]
    public async Task<IActionResult> ChangeCourseImage(
        Guid courseId,
        [FromForm] ChangeCourseImageRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeCourseImageCommand(
            courseId,
            new FormFileProxy(request.Image));

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.UpdateCourseImage],
        [PermissionType.ManageCourse])]
    [HttpDelete("image/{courseId:guid}")]
    public async Task<IActionResult> DeleteCourseImage(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCourseImageCommand(courseId);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasCourseMemberPermission(
        [CoursePermissionType.DeleteCourse],
        [PermissionType.ManageCourse])]
    [HttpDelete("{courseId:guid}")]
    public async Task<IActionResult> DeleteCourse(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCourseCommand(courseId);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
