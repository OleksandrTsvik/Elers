using API.Options;
using API.Options.Jwt;
using Infrastructure.Authentication;
using Infrastructure.Files;
using Infrastructure.SupabaseSetup;
using Persistence.Options;

namespace API.Extensions;

public static class ApiOptionsExtensions
{
    public static IServiceCollection AddApiOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddOptionsWithFluentValidation<DatabaseSettingsOptions>(
            ConfigurationSections.DatabaseSettings);

        services.AddOptionsWithFluentValidation<JwtOptions>(
            ConfigurationSections.Jwt);

        services.AddOptionsWithFluentValidation<SupabaseSettingsOptions>(
            ConfigurationSections.Supabase);

        services.AddOptionsWithFluentValidation<FileSettingsOptions>(
            ConfigurationSections.FileSettings);

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
