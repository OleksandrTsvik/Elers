using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseTab> CourseTabs { get; set; }
    public DbSet<CourseMember> CourseMembers { get; set; }
    public DbSet<CourseRole> CourseRoles { get; set; }
    public DbSet<CoursePermission> CoursePermissions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            ApplicationDbConfigurationsFilter);

        base.OnModelCreating(builder);
    }

    private static bool ApplicationDbConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.ApplicationDb") ?? false;
}
