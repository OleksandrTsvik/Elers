using API.Contracts;
using Application.Assignments.DownloadAssignmentFile;
using Application.Assignments.GetAssignment;
using Application.Assignments.SubmitAssignment;
using Infrastructure.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AssignmentsController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAssignment(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetAssignmentQuery(id);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpGet("file/download/{fileName}")]
    public async Task<IActionResult> DownloadAssignmentFile(
        string fileName,
        CancellationToken cancellationToken)
    {
        var query = new DownloadAssignmentFileQuery(fileName);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> SubmitAssignment(
        Guid id,
        [FromForm] SubmitAssignmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new SubmitAssignmentCommand(
            id,
            request.Text,
            request.Files?.Select(file => new FormFileProxy(file)).ToArray());

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
