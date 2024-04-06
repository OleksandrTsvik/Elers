using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Seed;

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
            var dbSeed = serviceProvider.GetRequiredService<ApplicationDbContextSeed>();

            await context.Database.MigrateAsync();
            await dbSeed.SeedDataAsync();
        }
        catch (Exception ex)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during database initialization");
        }
    }
}
