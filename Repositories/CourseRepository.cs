using CourseApi.Models;

namespace CourseApi.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly List<Course> _courses = new()
    {
        new Course { Id = Guid.NewGuid(), Title = "Artificial Intelligence", ImageUrl = "/images/ai.png", LessonCount = 15, Duration = "13 hr 35 min", Description = "An introduction to artificial intelligence and machine learning." },
        new Course { Id = Guid.NewGuid(), Title = "Data Science & Analytics", ImageUrl = "/images/data-science.png", LessonCount = 25, Duration = "20 hr 40 min", Description = "Learn how to analyse and work with large sets of data." },
        new Course { Id = Guid.NewGuid(), Title = "Digital Marketing", ImageUrl = "/images/digital-marketing.png", LessonCount = 5, Duration = "1 hr 18 min", Description = "Understand the basics of marketing in a digital world." },
        new Course { Id = Guid.NewGuid(), Title = "UI/UX Design for Beginner", ImageUrl = "/images/uiux.png", LessonCount = 34, Duration = "27 hr 55 min", Description = "Learn how to design user friendly interfaces from scratch." },
        new Course { Id = Guid.NewGuid(), Title = "Full stack Developer", ImageUrl = "/images/fullstack.png", LessonCount = 30, Duration = "24 hr 45 min", Description = "Build complete web applications with both frontend and backend." },
        new Course { Id = Guid.NewGuid(), Title = "Sketch for Designer", ImageUrl = "/images/sketch.png", LessonCount = 18, Duration = "14 hr 25 min", Description = "Get started with Sketch and learn the basics of digital design." }
    };

    public Task<IEnumerable<Course>> GetAllAsync() =>
        Task.FromResult(_courses.AsEnumerable());

    public Task<Course?> GetByIdAsync(Guid id) =>
        Task.FromResult(_courses.FirstOrDefault(c => c.Id == id));

    public Task<IEnumerable<Course>> SearchAsync(string query) =>
        Task.FromResult(_courses.Where(c =>
            c.Title.Contains(query, StringComparison.OrdinalIgnoreCase)));
}