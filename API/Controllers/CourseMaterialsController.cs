using Application.CourseMaterials.CreateCourseMaterialContent;
using Application.CourseMaterials.CreateCourseMaterialLink;
using Application.CourseMaterials.DeleteCourseMaterial;
using Application.CourseMaterials.GetListCourseMaterials;
using Application.CourseMaterials.GetListCourseMaterialsByTabId;
using Application.CourseMaterials.UpdateCourseMaterialActive;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CourseMaterialsController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetListCourseMaterials(CancellationToken cancellationToken)
    {
        var query = new GetListCourseMaterialsQuery();

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpGet("tabs/{tabId:guid}")]
    public async Task<IActionResult> GetListCourseMaterialsByTabId(
        Guid tabId,
        CancellationToken cancellationToken)
    {
        var query = new GetListCourseMaterialsByTabIdQuery(tabId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpPost("content/{tabId:guid}")]
    public async Task<IActionResult> CreateCourseMaterialContent(
        Guid tabId,
        [FromBody] CreateCourseMaterialContentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseMaterialContentCommand(tabId, request.Content);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpPost("link/{tabId:guid}")]
    public async Task<IActionResult> CreateCourseMaterialLink(
        Guid tabId,
        [FromBody] CreateCourseMaterialLinkRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseMaterialLinkCommand(tabId, request.Title, request.Link);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpPatch("active/{id:guid}")]
    public async Task<IActionResult> UpdateCourseMaterialActive(
        Guid id,
        [FromBody] UpdateCourseMaterialActiveRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseMaterialActiveCommand(id, request.IsActive);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCourseMaterial(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCourseMaterialCommand(id);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
