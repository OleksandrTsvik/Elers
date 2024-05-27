using API.Options;
using API.Options.Jwt;
using Infrastructure.Authentication;
using Infrastructure.CloudinarySetup;
using Infrastructure.Files;
using Infrastructure.SupabaseSetup;
using Persistence.Options;

namespace API.Extensions;

public static class ApiOptionsExtensions
{
    public static IServiceCollection AddApiOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddOptionsWithFluentValidation<DatabaseSettings>(
            ConfigurationSections.DatabaseSettings);

        services.AddOptionsWithFluentValidation<JwtOptions>(
            ConfigurationSections.Jwt);

        services.AddOptionsWithFluentValidation<SupabaseSettings>(
            ConfigurationSections.Supabase);

        services.AddOptionsWithFluentValidation<CloudinarySettings>(
            ConfigurationSections.Cloudinary);

        services.AddOptionsWithFluentValidation<FileSettings>(
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
