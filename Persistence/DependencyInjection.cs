using Application.Common.Interfaces;
using Application.Common.Queries;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Persistence.Configurations.MongoDb;
using Persistence.Options;
using Persistence.Queries;
using Persistence.Repositories;
using Persistence.Seed.ApplicationDb;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services
            .AddApplicationDb()
            .AddMongoDb()
            .AddQueries();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddApplicationDb(this IServiceCollection services)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((sp, options) =>
        {
            ApplicationDbSettings applicationDbSettings = sp.GetRequiredService<
                IOptions<DatabaseSettingsOptions>>().Value.ApplicationDb;

            options.UseNpgsql(
                applicationDbSettings.ConnectionString,
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
        });

        services.AddScoped<ApplicationDbContextSeed>();

        return services;
    }

    private static IServiceCollection AddMongoDb(this IServiceCollection services)
    {
        RegisterMongoDbClassMaps();

        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            MongoDbSettings mongoDbSettings = sp.GetRequiredService<
                IOptions<DatabaseSettingsOptions>>().Value.MongoDb;

            return new MongoClient(mongoDbSettings.ConnectionString);
        });

        services.AddSingleton<IMongoDatabase>(sp =>
        {
            MongoDbSettings mongoDbSettings = sp.GetRequiredService<
                IOptions<DatabaseSettingsOptions>>().Value.MongoDb;

            IMongoClient mongoClient = sp.GetRequiredService<IMongoClient>();

            return mongoClient.GetDatabase(mongoDbSettings.DatabaseName);
        });

        services.AddMongoDbRepositories();

        return services;
    }

    private static IServiceCollection AddMongoDbRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICourseMaterialRepository, CourseMaterialRepository>();

        return services;
    }

    private static void RegisterMongoDbClassMaps()
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        CourseMaterialClassMap.RegisterClassMaps();
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddScoped<ICourseMaterialQueries, CourseMaterialQueries>();

        return services;
    }
}
