using CourseApi.Data;
using CourseApi.Models;
using CourseApi.Repositories;
using CourseApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CourseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
        db.Database.Migrate();

        if (!db.Courses.Any())
        {
            db.Courses.AddRange(
                new Course { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Title = "Artificial Intelligence", ImageUrl = "/images/ai.png", LessonCount = 15, Duration = "13 hr 35 min", Description = "An introduction to artificial intelligence and machine learning." },
                new Course { Id = Guid.NewGuid(), Title = "Data Science & Analytics", ImageUrl = "/images/data-science.png", LessonCount = 25, Duration = "20 hr 40 min", Description = "Learn how to analyse and work with large sets of data." },
                new Course { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Title = "Digital Marketing", ImageUrl = "/images/digital-marketing.png", LessonCount = 5, Duration = "1 hr 18 min", Description = "Understand the basics of marketing in a digital world." },
                new Course { Id = Guid.NewGuid(), Title = "UI/UX Design for Beginner", ImageUrl = "/images/uiux.png", LessonCount = 34, Duration = "27 hr 55 min", Description = "Learn how to design user friendly interfaces from scratch." },
                new Course { Id = Guid.NewGuid(), Title = "Full stack Developer", ImageUrl = "/images/fullstack.png", LessonCount = 30, Duration = "24 hr 45 min", Description = "Build complete web applications with both frontend and backend." },
                new Course { Id = Guid.NewGuid(), Title = "Sketch for Designer", ImageUrl = "/images/sketch.png", LessonCount = 18, Duration = "14 hr 25 min", Description = "Get started with Sketch and learn the basics of digital design." }
            );
            db.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Seed error: {ex.Message}");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.Run();