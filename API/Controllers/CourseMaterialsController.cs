using Application.CourseMaterials.CreateCourseMaterialContent;
using Application.CourseMaterials.CreateCourseMaterialLink;
using Application.CourseMaterials.GetListCourseMaterials;
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
}
