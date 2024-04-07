using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(refreshToken => refreshToken.Id);

        builder
            .Property(refreshToken => refreshToken.Token)
            .IsRequired()
            .HasMaxLength(1024);

        builder
            .Property(refreshToken => refreshToken.ExpiryDate)
            .IsRequired();

        builder
            .Property(refreshToken => refreshToken.RevokedDate)
            .IsRequired(false);

        builder
            .HasOne(refreshToken => refreshToken.User)
            .WithMany()
            .HasForeignKey(refreshToken => refreshToken.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(x => x.IsExpired);
        builder.Ignore(x => x.IsActive);
    }
}
