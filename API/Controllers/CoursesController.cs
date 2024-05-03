using Application.Courses.CreateCourse;
using Application.Courses.GetCourseById;
using Application.Courses.GetListCourses;
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
        var command = new CreateCourseCommand(request.Title, request.Description);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
