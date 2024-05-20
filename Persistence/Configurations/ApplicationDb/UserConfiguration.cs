using Domain.Entities;
using Domain.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.ApplicationDb;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder
            .Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(UserRules.MaxEmailLength);

        builder
            .HasIndex(user => user.Email)
            .IsUnique();

        builder
            .Property(user => user.PasswordHash)
            .IsRequired()
            .HasMaxLength(UserRules.MaxPasswordHashLength);

        builder
            .Property(user => user.RegistrationDate)
            .IsRequired();

        builder
            .Property(user => user.FirstName)
            .IsRequired(false)
            .HasMaxLength(UserRules.MaxFirstNameLength);

        builder
            .Property(user => user.LastName)
            .IsRequired(false)
            .HasMaxLength(UserRules.MaxLastNameLength);

        builder
            .Property(user => user.Patronymic)
            .IsRequired(false)
            .HasMaxLength(UserRules.MaxPatronymicLength);

        builder
            .Property(user => user.AvatarUrl)
            .IsRequired(false)
            .HasMaxLength(UserRules.MaxAvatarUrlLength);

        builder
            .Property(user => user.BirthDate)
            .IsRequired(false);
    }
}
