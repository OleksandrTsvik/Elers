using System.Reflection;
using Application.Common.Behaviors;
using Application.Common.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);

        services.AddBehaviors();

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    private static IServiceCollection AddBehaviors(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
