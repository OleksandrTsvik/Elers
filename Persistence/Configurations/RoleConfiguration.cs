using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(role => role.Id);

        builder
            .Property(role => role.Name)
            .IsRequired()
            .HasMaxLength(32);

        builder
            .HasIndex(role => role.Name)
            .IsUnique();

        builder
            .HasMany(role => role.Users)
            .WithMany(user => user.Roles);
    }
}
