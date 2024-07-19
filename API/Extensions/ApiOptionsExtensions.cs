using Api.Options;
using Api.Options.AppVars;
using Api.Options.Jwt;
using Domain.Constants;
using Infrastructure.Authentication;
using Infrastructure.CloudinarySetup;
using Infrastructure.Files;
using Infrastructure.SupabaseSetup;
using Microsoft.Extensions.Options;
using Persistence.Options;

namespace Api.Extensions;

public static class ApiOptionsExtensions
{
    public static IServiceCollection AddApiOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddOptionsWithFluentValidation<DatabaseSettings>(ConfigurationSections.DatabaseSettings);
        services.AddOptionsWithFluentValidation<JwtOptions>(ConfigurationSections.Jwt);

        services.AddOptionsWithFluentValidation<SupabaseSettings>(ConfigurationSections.Supabase);
        services.AddOptionsWithFluentValidation<CloudinarySettings>(ConfigurationSections.Cloudinary);

        services.AddOptionsWithFluentValidation<FileSettings>(ConfigurationSections.FileSettings);
        services.AddOptionsWithFluentValidation<AppVariables>(ConfigurationSections.AppVariables);
        services.AddOptionsWithFluentValidation<SeedOptions>(ConfigurationSections.Seed);

        services.AddScoped<IAppVariables, AppVariables>(sp =>
            sp.GetRequiredService<IOptions<AppVariables>>().Value);

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
