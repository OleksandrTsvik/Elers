using Domain.Entities;
using Domain.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CourseTabConfiguration : IEntityTypeConfiguration<CourseTab>
{
    public void Configure(EntityTypeBuilder<CourseTab> builder)
    {
        builder.HasKey(courseTab => courseTab.Id);

        builder
            .Property(courseTab => courseTab.Name)
            .IsRequired()
            .HasMaxLength(CourseTabRules.MaxNameLength);

        builder
            .Property(courseTab => courseTab.IsActive)
            .IsRequired();

        builder
            .Property(courseTab => courseTab.Order)
            .IsRequired();

        builder
            .Property(courseTab => courseTab.Color)
            .IsRequired(false)
            .HasMaxLength(CourseTabRules.MaxColorLength);

        builder
            .Property(courseTab => courseTab.ShowMaterialsCount)
            .IsRequired();

        builder
            .HasOne(courseTab => courseTab.Course)
            .WithMany(course => course.CourseTabs)
            .HasForeignKey(courseTab => courseTab.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
