using CourseApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApi.Data.Configurations;

public sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses", "course");

        builder.HasKey(course => course.Id);

        builder.Property(course => course.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(course => course.ImageUrl)
            .HasMaxLength(500)
            .IsRequired();
    }
}