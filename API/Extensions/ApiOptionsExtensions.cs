using API.Options;
using API.Options.Jwt;
using Infrastructure.Authentication;
using Persistence.Common;

namespace API.Extensions;

public static class ApiOptionsExtensions
{
    public static IServiceCollection AddApiOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddOptionsWithFluentValidation<ConnectionStringsOptions>(
            ConfigurationSections.ConnectionStrings);

        services.AddOptionsWithFluentValidation<JwtOptions>(
            ConfigurationSections.Jwt);

        return services;
    }

    public static IServiceCollection AddOptionsWithFluentValidation<TOptions>(
        this IServiceCollection services,
        string configurationSection)
        where TOptions : class
    {
        services.AddOptions<TOptions>()
            .BindConfiguration(configurationSection)
            .ValidateFluentValidation()
            .ValidateOnStart();

        return services;
    }
}
