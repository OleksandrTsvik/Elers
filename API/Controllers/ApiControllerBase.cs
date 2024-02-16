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
    protected ObjectResult GetErrorResult(Error error) =>
        StatusCode(GetStatusCode(error.Type), error);

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
