using Domain.Entities;
using Domain.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(permission => permission.Id);

        builder
            .Property(permission => permission.Name)
            .IsRequired()
            .HasMaxLength(PermissionRules.MaxNameLength);

        builder
            .HasIndex(permission => permission.Name)
            .IsUnique();

        builder
            .HasMany(permission => permission.Roles)
            .WithMany(role => role.Permissions);
    }
}
