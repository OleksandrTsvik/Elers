using System.Reflection;
using API.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace API.Extensions;

public static class ApiServiceExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddControllers();

        services.AddAuth();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey(Auth.CookieAccessTokenKey))
                        {
                            context.Token = context.Request.Cookies[Auth.CookieAccessTokenKey];
                        }

                        return Task.CompletedTask;
                    }
                };
            })
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

        services.AddAuthorizationBuilder()
            .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build());

        return services;
    }
}
