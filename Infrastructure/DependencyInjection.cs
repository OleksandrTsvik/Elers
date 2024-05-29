using Application.Common.Interfaces;
using Application.Common.Services;
using Infrastructure.Authentication;
using Infrastructure.CloudinarySetup;
using Infrastructure.CourseMemberPermissions;
using Infrastructure.Files;
using Infrastructure.Localization;
using Infrastructure.SupabaseSetup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Supabase;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddLocalization()
            .AddAuth()
            .AddSupabase()
            .AddFiles();

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
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationHandler, CourseMemberPermissionAuthorizationHandler>();

        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordService, PasswordService>();

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();

        return services;
    }

    private static IServiceCollection AddSupabase(this IServiceCollection services)
    {
        services.AddScoped<Supabase.Client>(sp =>
        {
            SupabaseSettings supabaseSettings = sp.GetRequiredService<IOptions<SupabaseSettings>>().Value;

            return new Supabase.Client(
                supabaseSettings.Url,
                supabaseSettings.Key,
                new SupabaseOptions
                {
                    AutoConnectRealtime = true
                });
        });

        return services;
    }

    private static IServiceCollection AddFiles(this IServiceCollection services)
    {
        services.AddScoped<IFileValidator, FileValidator>();

        // Local storage of files and images on the server
        // services.AddScoped<IFolderService, FolderService>();
        // services.AddScoped<IFileService, FileService>();
        // services.AddScoped<IImageService, ImageService>();

        services.AddScoped<IFileService, SupabaseFileService>();

        services.AddScoped<IImageService, CloudinaryImageService>();

        return services;
    }
}
