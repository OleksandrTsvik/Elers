using Domain.Entities;
using Domain.Enums;
using Domain.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.Configurations.ApplicationDb;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder
            .HasDiscriminator(user => user.Type)
            .HasValue<User>(UserType.User)
            .HasValue<Student>(UserType.Student)
            .HasValue<Teacher>(UserType.Teacher);

        builder
            .Property(user => user.Type)
            .IsRequired()
            .HasConversion(new EnumToStringConverter<UserType>());

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
            .IsRequired()
            .HasMaxLength(UserRules.MaxFirstNameLength);

        builder
            .Property(user => user.LastName)
            .IsRequired()
            .HasMaxLength(UserRules.MaxLastNameLength);

        builder
            .Property(user => user.Patronymic)
            .IsRequired()
            .HasMaxLength(UserRules.MaxPatronymicLength);

        builder
            .Property(user => user.AvatarUrl)
            .IsRequired(false)
            .HasMaxLength(UserRules.MaxAvatarUrlLength);

        builder
            .Property(user => user.AvatarImageName)
            .IsRequired(false)
            .HasMaxLength(UserRules.MaxAvatarImageNameLength);

        builder
            .Property(user => user.BirthDate)
            .IsRequired(false);
    }
}
