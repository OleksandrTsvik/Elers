using Domain.Entities;
using Domain.Enums;
using Domain.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.Configurations.ApplicationDb;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(permission => permission.Id);

        builder
            .Property(permission => permission.Name)
            .IsRequired()
            .HasConversion(new EnumToStringConverter<PermissionType>())
            .HasMaxLength(PermissionRules.MaxNameLength);

        builder
            .HasIndex(permission => permission.Name)
            .IsUnique();

        builder
            .HasMany(permission => permission.Roles)
            .WithMany(role => role.Permissions);
    }
}
