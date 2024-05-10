using Application.Courses.CreateCourse;
using Application.Courses.GetCourseById;
using Application.Courses.GetListCourses;
using Application.Courses.UpdateCourseDescription;
using Application.Courses.UpdateCourseTabType;
using Application.Courses.UpdateCourseTitle;
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
}
