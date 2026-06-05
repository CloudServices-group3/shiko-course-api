using CourseApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Data;

public static class CourseSeeder
{
    public static async Task SeedAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<CourseDbContext>();

        if (await db.Courses.AnyAsync())
        {
            return;
        }

        db.Courses.AddRange(
            new Course
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Title = "Artificial Intelligence",
                ImageUrl = "/images/ai.png"
            },
            new Course
            {
                Id = Guid.Parse("882d1c96-b77f-46cc-994c-d12bbd16a0de"),
                Title = "Data Science & Analytics",
                ImageUrl = "/images/data-science.png"
            },
            new Course
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Title = "Digital Marketing",
                ImageUrl = "/images/digital-marketing.png"
            },
            new Course
            {
                Id = Guid.Parse("d9da7d05-0605-4ef3-8fc2-ec0a1ff90cc1"),
                Title = "UI/UX Design for Beginner",
                ImageUrl = "/images/uiux.png"
            },
            new Course
            {
                Id = Guid.Parse("7a875d05-b4fb-4b27-bc50-b776aaef14d4"),
                Title = "Full stack Developer",
                ImageUrl = "/images/fullstack.png"
            },
            new Course
            {
                Id = Guid.Parse("0be85b35-ba14-417b-ba04-2a1c0bd0d509"),
                Title = "Sketch for Designer",
                ImageUrl = "/images/sketch.png"
            }
        );

        await db.SaveChangesAsync();
    }
}