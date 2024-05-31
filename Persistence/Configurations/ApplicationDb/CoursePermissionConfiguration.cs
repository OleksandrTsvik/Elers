using Domain.Entities;
using Domain.Enums;
using Domain.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.Configurations.ApplicationDb;

public class CoursePermissionConfiguration : IEntityTypeConfiguration<CoursePermission>
{
    public void Configure(EntityTypeBuilder<CoursePermission> builder)
    {
        builder.HasKey(courseRolePermission => courseRolePermission.Id);

        builder
            .Property(courseRolePermission => courseRolePermission.Name)
            .IsRequired()
            .HasConversion(new EnumToStringConverter<CoursePermissionType>())
            .HasMaxLength(CoursePermissionRules.MaxNameLength);

        builder
            .HasIndex(courseRolePermission => courseRolePermission.Name)
            .IsUnique();

        builder
            .HasMany(courseRolePermission => courseRolePermission.CourseRoles)
            .WithMany(courseRole => courseRole.CoursePermissions);
    }
}
