using Application.Common.Interfaces;
using Application.Common.Queries;
using Application.Common.Services;
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
using Persistence.Services;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services
            .AddApplicationDb()
            .AddMongoDb()
            .AddRepositories()
            .AddQueries()
            .AddServices();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddApplicationDb(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            ApplicationDbSettings applicationDbSettings = sp.GetRequiredService<
                IOptions<DatabaseSettings>>().Value.ApplicationDb;

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
                IOptions<DatabaseSettings>>().Value.MongoDb;

            return new MongoClient(mongoDbSettings.ConnectionString);
        });

        services.AddSingleton<IMongoDatabase>(sp =>
        {
            MongoDbSettings mongoDbSettings = sp.GetRequiredService<
                IOptions<DatabaseSettings>>().Value.MongoDb;

            IMongoClient mongoClient = sp.GetRequiredService<IMongoClient>();

            return mongoClient.GetDatabase(mongoDbSettings.DatabaseName);
        });

        return services;
    }

    private static void RegisterMongoDbClassMaps()
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

#pragma warning disable
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
#pragma warning restore

        CourseMaterialClassMap.RegisterClassMaps();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICourseMaterialRepository, CourseMaterialRepository>();
        services.AddScoped<ICourseMemberRepository, CourseMemberRepository>();
        services.AddScoped<ICoursePermissionRepository, CoursePermissionRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseRoleRepository, CourseRoleRepository>();
        services.AddScoped<ICourseTabRepository, CourseTabRepository>();

        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddScoped<ICourseMaterialQueries, CourseMaterialQueries>();
        services.AddScoped<ICourseMemberQueries, CourseMemberQueries>();
        services.AddScoped<ICoursePermissionQueries, CoursePermissionQueries>();
        services.AddScoped<ICourseQueries, CourseQueries>();
        services.AddScoped<ICourseRoleQueries, CourseRoleQueries>();

        services.AddScoped<IPermissionQueries, PermissionQueries>();
        services.AddScoped<IRoleQueries, RoleQueries>();
        services.AddScoped<IUserQueries, UserQueries>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICourseMemberPermissionService, CourseMemberPermissionService>();

        return services;
    }
}
