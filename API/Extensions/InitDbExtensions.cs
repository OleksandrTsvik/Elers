using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.Options;
using Persistence.Seed.ApplicationDb;

namespace Api.Extensions;

public static class InitDbExtensions
{
    public static async Task InitDbAsync(this WebApplication app)
    {
        bool isDevelopment = app.Environment.IsDevelopment();

        await using AsyncServiceScope scope = app.Services.CreateAsyncScope();
        IServiceProvider serviceProvider = scope.ServiceProvider;

        try
        {
            ApplicationDbContext dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            ApplicationDbContextSeed dbSeed = serviceProvider.GetRequiredService<ApplicationDbContextSeed>();

            SeedOptions seedOptions = serviceProvider.GetRequiredService<IOptions<SeedOptions>>().Value;

            await dbContext.Database.MigrateAsync();
            await dbSeed.SeedDataAsync(seedOptions, isDevelopment);
        }
        catch (Exception ex)
        {
            ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during database initialization");
        }
    }
}
