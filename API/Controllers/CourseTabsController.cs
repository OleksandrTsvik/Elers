using Application.CourseTabs.CreateCourseTab;
using Application.CourseTabs.DeleteCourseTab;
using Application.CourseTabs.UpdateCourseTab;
using Application.CourseTabs.UpdateCourseTabColor;
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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCourseTab(
        Guid id,
        [FromBody] UpdateCourseTabRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseTabCommand(
            id,
            request.Name,
            request.IsActive,
            request.ShowMaterialsCount);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpPatch("color/{id:guid}")]
    public async Task<IActionResult> UpdateCourseTabColor(
        Guid id,
        [FromBody] UpdateCourseTabColorRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseTabColorCommand(id, request.Color);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCourseTab(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCourseTabCommand(id);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
