using System.Reflection;
using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Services;
using Domain.Errors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped<IAuthService, AuthService>();

        services.AddErrors();

        return services;
    }

    private static IServiceCollection AddErrors(this IServiceCollection services)
    {
        services.AddSingleton<IErrorGenerator, ErrorGenerator>();

        services.AddSingleton<IDefaultErrors, DefaultErrors>();
        services.AddSingleton<IRefreshTokenErrors, RefreshTokenErrors>();
        services.AddSingleton<IUserErrors, UserErrors>();

        return services;
    }
}
