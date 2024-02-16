using API.Middleware;

namespace API.Extensions;

public static class ApiMiddlewaresExtensions
{
    public static void ApplyApiMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<AppExceptionMiddleware>();
    }
}
