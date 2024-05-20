using Domain.Entities;
using Domain.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.ApplicationDb;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(course => course.Id);

        builder
            .Property(course => course.Title)
            .IsRequired()
            .HasMaxLength(CourseRules.MaxTitleLength);

        builder
            .Property(course => course.Description)
            .IsRequired(false)
            .HasMaxLength(CourseRules.MaxDescriptionLength);

        builder
            .Property(course => course.PhotoUrl)
            .IsRequired(false)
            .HasMaxLength(CourseRules.MaxPhotoUrlLength);

        builder
            .Property(course => course.TabType)
            .IsRequired(false)
            .HasMaxLength(CourseRules.MaxTabTypeLength);
    }
}
