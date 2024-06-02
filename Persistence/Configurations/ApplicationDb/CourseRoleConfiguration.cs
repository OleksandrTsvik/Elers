using Domain.Entities;
using Domain.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.ApplicationDb;

public class CourseRoleConfiguration : IEntityTypeConfiguration<CourseRole>
{
    public void Configure(EntityTypeBuilder<CourseRole> builder)
    {
        builder.HasKey(courseRole => courseRole.Id);

        builder
            .Property(courseRole => courseRole.Name)
            .IsRequired()
            .HasMaxLength(CourseRoleRules.MaxNameLength);

        builder
            .HasMany(courseRole => courseRole.CourseMembers)
            .WithOne(courseMember => courseMember.CourseRole)
            .HasForeignKey(courseMember => courseMember.CourseRoleId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
