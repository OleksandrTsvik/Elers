using Domain.Entities;
using Domain.Enums;
using Domain.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
            .Property(course => course.ImageUrl)
            .IsRequired(false)
            .HasMaxLength(CourseRules.MaxImageUrlLength);

        builder
            .Property(course => course.ImageName)
            .IsRequired(false)
            .HasMaxLength(CourseRules.MaxImageNameLength);

        builder
            .Property(course => course.TabType)
            .IsRequired()
            .HasConversion(new EnumToStringConverter<CourseTabType>())
            .HasMaxLength(CourseRules.MaxTabTypeLength);
    }
}
