using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    private ISender? _sender;
    protected ISender Sender => _sender ??=
        HttpContext.RequestServices.GetRequiredService<ISender>();

    private ITranslator? _translator;
    protected ITranslator Translator => _translator ??=
        HttpContext.RequestServices.GetRequiredService<ITranslator>();

    [NonAction]
    protected ActionResult HandleResult(Result result)
    {
        if (result is null)
        {
            return GetErrorResult(Error.NullResult);
        }

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return GetErrorResult(result.Error);
    }

    [NonAction]
    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result is null)
        {
            return GetErrorResult(Error.NullResult);
        }

        if (result.IsSuccess && result.Value is not null)
        {
            return GetValueResult(result.Value);
        }

        if (result.IsSuccess && result.Value is null)
        {
            return NoContent();
        }

        return GetErrorResult(result.Error);
    }

    [NonAction]
    protected ObjectResult GetErrorResult(Error error)
    {
        string message = Translator.GetString(error.Code, error.MessageArguments);

        if (message == error.Code)
        {
            message = error.Message;
        }

        var errorResponse = new ErrorResultResponse(error.Code, message);

        return StatusCode(GetStatusCode(error.Type), errorResponse);
    }

    [NonAction]
    private static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

    [NonAction]
    private ActionResult GetValueResult<T>(T value)
    {
        switch (value)
        {
            case byte[] bytes:
                return File(bytes, "application/octet-stream");
            case FileDownloadResult fileDownloadResult:
                var fileProvider = new FileExtensionContentTypeProvider();

                if (!fileProvider.TryGetContentType(fileDownloadResult.FileName, out string? contentType))
                {
                    contentType = "application/octet-stream";
                }

                return File(fileDownloadResult.FileContents, contentType, fileDownloadResult.FileName);
            default:
                return Ok(value);
        }
    }
}
