using Application.Common.Interfaces;
using Domain.Exceptions;
using Domain.Shared;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly ITranslator _translator;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, ITranslator translator)
    {
        _logger = logger;
        _translator = translator;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // _logger.LogError(exception, "{Message}", exception.Message);

        int statusCode = GetStatusCode(exception);
        string message = _translator.GetString(GetMessage(exception));
        object? details = GetDetails(exception);

        var exceptionResponse = new ExceptionResponse(statusCode, message, details);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(exceptionResponse, cancellationToken);

        return true;
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            AppValidationException => StatusCodes.Status400BadRequest,
            UserIdUnavailableException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetMessage(Exception exception) =>
        exception switch
        {
            AppValidationException ex => ex.Message,
            _ => "Internal Server Error."
        };

    private static object? GetDetails(Exception exception) =>
        exception switch
        {
            AppValidationException ex => ex.Errors,
            _ => null
        };
}
