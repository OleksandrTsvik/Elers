using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.ApplicationDb;

public class CourseMemberConfiguration : IEntityTypeConfiguration<CourseMember>
{
    public void Configure(EntityTypeBuilder<CourseMember> builder)
    {
        builder.HasKey(courseMember => courseMember.Id);

        builder
            .Property(courseMember => courseMember.EnrollmentDate)
            .IsRequired();

        builder
            .HasOne(courseMember => courseMember.User)
            .WithMany()
            .HasForeignKey(courseMember => courseMember.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(courseMember => courseMember.Course)
            .WithMany(course => course.CourseMembers)
            .HasForeignKey(courseMember => courseMember.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}