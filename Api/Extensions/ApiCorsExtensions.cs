using Microsoft.AspNetCore.CookiePolicy;

namespace Api.Extensions;

public static class ApiCorsExtensions
{
    public const string CorsPolicy = "CorsPolicy";

    public static IServiceCollection AddApiCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicy, policy =>
            {
                policy
                    .WithOrigins("http://localhost:3000")
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("WWW-Authenticate");
            });
        });

        return services;
    }

    public static void ApplyApiCors(this WebApplication app)
    {
        app.UseCors(CorsPolicy);
    }

    public static void ApplyApiCookie(this WebApplication app)
    {
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            MinimumSameSitePolicy = SameSiteMode.Strict,
            HttpOnly = HttpOnlyPolicy.Always,
            Secure = CookieSecurePolicy.Always
        });
    }
}
