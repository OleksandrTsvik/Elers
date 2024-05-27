using API.Contracts;
using Application.Courses.ChangeCourseImage;
using Application.Courses.CreateCourse;
using Application.Courses.DeleteCourse;
using Application.Courses.DeleteCourseImage;
using Application.Courses.GetCourseById;
using Application.Courses.GetCourseByIdToEdit;
using Application.Courses.GetCourseByTabId;
using Application.Courses.GetListCourses;
using Application.Courses.UpdateCourseDescription;
using Application.Courses.UpdateCourseTabType;
using Application.Courses.UpdateCourseTitle;
using Infrastructure.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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

    [HttpGet("edit/{id:guid}")]
    public async Task<IActionResult> GetCourseByIdToEdit(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseByIdToEditQuery(id);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpGet("tab/{tabId:guid}")]
    public async Task<IActionResult> GetCourseByTabId(
        Guid tabId,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseByTabIdQuery(tabId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetListCourses(CancellationToken cancellationToken)
    {
        var query = new GetListCoursesQuery();

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

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

    [HttpPatch("title/{id:guid}")]
    public async Task<IActionResult> UpdateCourseTitle(
        Guid id,
        [FromBody] UpdateCourseTitleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseTitleCommand(id, request.Title);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpPatch("description/{id:guid}")]
    public async Task<IActionResult> UpdateCourseDescription(
        Guid id,
        [FromBody] UpdateCourseDescriptionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseDescriptionCommand(id, request.Description);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpPatch("tab-type/{id:guid}")]
    public async Task<IActionResult> UpdateCourseTabType(
        Guid id,
        [FromBody] UpdateCourseTabTypeRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseTabTypeCommand(id, request.TabType);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpPatch("image/{id:guid}")]
    public async Task<IActionResult> ChangeCourseImage(
        Guid id,
        [FromForm] ChangeCourseImageRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeCourseImageCommand(
            id,
            new FormFileProxy(request.Image));

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpDelete("image/{id:guid}")]
    public async Task<IActionResult> DeleteCourseImage(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCourseImageCommand(id);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCourse(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCourseCommand(id);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
