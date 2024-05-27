using API.Contracts;
using Application.Images.UploadImage;
using Infrastructure.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ImagesController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> UploadImage(
        [FromForm] UploadImageRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UploadImageCommand(new FormFileProxy(request.Upload));

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
