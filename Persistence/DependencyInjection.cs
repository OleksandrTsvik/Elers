using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.Common;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((sp, options) =>
        {
            var connectionStringsOptions = sp.GetRequiredService<IOptions<ConnectionStringsOptions>>().Value;

            options.UseNpgsql(connectionStringsOptions.DefaultConnection);
        });

        return services;
    }
}
