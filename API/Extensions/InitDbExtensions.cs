using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Common;

namespace API.Extensions;

public static class InitDbExtensions
{
    public static async Task InitDbAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var serviceProvider = scope.ServiceProvider;

        try
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            await ApplyMigrationsAsync(context);
            await SeedDataAsync(context);
        }
        catch (Exception ex)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during database initialization");
        }
    }

    private static async Task ApplyMigrationsAsync(ApplicationDbContext context)
    {
        await context.Database.MigrateAsync();
    }

    private static async Task SeedDataAsync(ApplicationDbContext context)
    {
        await ApplicationDbContextSeed.SeedDataAsync(context);
    }
}
