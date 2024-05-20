using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Seed.ApplicationDb;

namespace API.Extensions;

public static class InitDbExtensions
{
    public static async Task InitDbAsync(this WebApplication app)
    {
        await using AsyncServiceScope scope = app.Services.CreateAsyncScope();
        IServiceProvider serviceProvider = scope.ServiceProvider;

        try
        {
            ApplicationDbContext dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            ApplicationDbContextSeed dbSeed = serviceProvider.GetRequiredService<ApplicationDbContextSeed>();

            await dbContext.Database.MigrateAsync();
            await dbSeed.SeedDataAsync();
        }
        catch (Exception ex)
        {
            ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during database initialization");
        }
    }
}
