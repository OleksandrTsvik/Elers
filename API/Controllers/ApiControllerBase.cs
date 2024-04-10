using Application.Common.Interfaces;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
            return Ok(result.Value);
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

        var errorResponse = new ErrorResponse(error.Code, message);

        return StatusCode(GetStatusCode(error.Type), errorResponse);
    }

    [NonAction]
    private static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
}
