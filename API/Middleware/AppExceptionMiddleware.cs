using Application.Common.Exceptions;

namespace API.Middleware;

public class AppExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AppExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public AppExceptionMiddleware(
        RequestDelegate next,
        ILogger<AppExceptionMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
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

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            AppExceptionResponse exceptionResponse = _environment.IsDevelopment()
                ? new AppExceptionResponse(httpContext.Response.StatusCode, ex.Message, ex.StackTrace)
                : new AppExceptionResponse(httpContext.Response.StatusCode, "Internal Server Error");

            await httpContext.Response.WriteAsJsonAsync(exceptionResponse);
        }
    }
}
