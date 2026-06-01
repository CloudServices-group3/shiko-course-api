using CourseApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Data;

public sealed class CourseDbContext(DbContextOptions<CourseDbContext> options)
    : DbContext(options)
{
    public DbSet<Course> Courses => Set<Course>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseDbContext).Assembly);
    }
}