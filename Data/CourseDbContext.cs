using CourseApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Data;

public class CourseDbContext : DbContext
{
    public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options) { }

    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasData(
            new Course { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Title = "Artificial Intelligence", ImageUrl = "/images/ai.png", LessonCount = 15, Duration = "13 hr 35 min", Description = "An introduction to artificial intelligence and machine learning." },
            new Course { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Title = "Data Science & Analytics", ImageUrl = "/images/data-science.png", LessonCount = 25, Duration = "20 hr 40 min", Description = "Learn how to analyse and work with large sets of data." },
            new Course { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Title = "Digital Marketing", ImageUrl = "/images/digital-marketing.png", LessonCount = 5, Duration = "1 hr 18 min", Description = "Understand the basics of marketing in a digital world." },
            new Course { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Title = "UI/UX Design for Beginner", ImageUrl = "/images/uiux.png", LessonCount = 34, Duration = "27 hr 55 min", Description = "Learn how to design user friendly interfaces from scratch." },
            new Course { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Title = "Full stack Developer", ImageUrl = "/images/fullstack.png", LessonCount = 30, Duration = "24 hr 45 min", Description = "Build complete web applications with both frontend and backend." },
            new Course { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), Title = "Sketch for Designer", ImageUrl = "/images/sketch.png", LessonCount = 18, Duration = "14 hr 25 min", Description = "Get started with Sketch and learn the basics of digital design." }
        );
    }
}