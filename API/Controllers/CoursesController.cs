using Application.Courses.CreateCourse;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CoursesController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCourse(
        [FromBody] CreateCourseRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseCommand(request.Title, request.Description);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
