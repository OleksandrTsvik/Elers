using Api.Middleware;

namespace Api.Extensions;

public static class ApiMiddlewaresExtensions
{
    public static IServiceCollection AddApiMiddlewares(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    public static void ApplyApiMiddlewares(this WebApplication app)
    {
        app.UseExceptionHandler();
    }
}
