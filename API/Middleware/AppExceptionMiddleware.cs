using Application.Common.Interfaces;
using Domain.Exceptions;
using Domain.Shared;

namespace API.Middleware;

public class AppExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AppExceptionMiddleware> _logger;
    private readonly ITranslator _translator;

    public AppExceptionMiddleware(
        RequestDelegate next,
        ILogger<AppExceptionMiddleware> logger,
        ITranslator translator)
    {
        _next = next;
        _logger = logger;
        _translator = translator;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        int statusCode = GetStatusCode(exception);
        string message = _translator.GetString(GetMessage(exception));
        object? details = GetDetails(exception);

        var exceptionResponse = new ExceptionResponse(statusCode, message, details);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(exceptionResponse);
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
