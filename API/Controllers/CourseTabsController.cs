using Application.CourseTabs.CreateCourseTab;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CourseTabsController : ApiControllerBase
{
    [HttpPost("{courseId:guid}")]
    public async Task<IActionResult> CreateCourseTab(
        Guid courseId,
        [FromBody] CreateCourseTabRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseTabCommand(courseId, request.Name);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
