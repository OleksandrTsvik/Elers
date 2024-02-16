namespace Persistence.Common;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDataAsync(ApplicationDbContext context)
    {
        await context.SaveChangesAsync();
    }
}
