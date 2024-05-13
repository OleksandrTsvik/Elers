using Application.Common.Interfaces;
using Infrastructure.Authentication;
using Infrastructure.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddLocalization()
            .AddAuth();

        return services;
    }

    private static IServiceCollection AddLocalization(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddPortableObjectLocalization(options => options.ResourcesPath = "Resources");

        services.AddRequestLocalization(options =>
        {
            // Supported Culture and Country Codes
            // https://azuliadesigns.com/c-sharp-tutorials/list-net-culture-country-codes/
            string[] supportedCultures = ["uk", "en"];

            // header - cookie - query
            options.RequestCultureProviders = options.RequestCultureProviders.Reverse().ToList();

            options
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
        });

        services.AddSingleton<ITranslator, Translator>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordService, PasswordService>();

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();

        return services;
    }
}
