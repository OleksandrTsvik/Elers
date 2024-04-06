using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder
            .Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(64);

        builder
            .HasIndex(user => user.Email)
            .IsUnique();

        builder
            .Property(user => user.PasswordHash)
            .IsRequired()
            .HasMaxLength(512);

        builder
            .Property(user => user.RegistrationDate)
            .IsRequired();

        builder
            .Property(user => user.FirstName)
            .IsRequired(false)
            .HasMaxLength(64);

        builder
            .Property(user => user.LastName)
            .IsRequired(false)
            .HasMaxLength(64);

        builder
            .Property(user => user.Patronymic)
            .IsRequired(false)
            .HasMaxLength(64);

        builder
            .Property(user => user.AvatarUrl)
            .IsRequired(false)
            .HasMaxLength(512);

        builder
            .Property(user => user.BirthDate)
            .IsRequired(false);
    }
}
