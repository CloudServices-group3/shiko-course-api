using CourseApi.DTOs;
using CourseApi.Models;
using CourseApi.Repositories;

namespace CourseApi.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repository;

    public CourseService(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
    {
        var courses = await _repository.GetAllAsync();
        return courses.Select(c => new CourseDto
        {
            Id = c.Id,
            Title = c.Title,
            ImageUrl = c.ImageUrl,
            LessonCount = c.LessonCount,
            Duration = c.Duration,
            Description = c.Description
        });
    }

    public async Task<CourseDto?> GetCourseByIdAsync(Guid id)
    {
        var course = await _repository.GetByIdAsync(id);  
        if (course is null) return null;

        return new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            ImageUrl = course.ImageUrl,
            LessonCount = course.LessonCount,
            Duration = course.Duration,
            Description = course.Description
        };
    }

    public async Task<IEnumerable<CourseDto>> SearchCoursesAsync(string query)
    {
        var courses = await _repository.SearchAsync(query);
        return courses.Select(c => new CourseDto
        {
            Id = c.Id,
            Title = c.Title,
            ImageUrl = c.ImageUrl,
            LessonCount = c.LessonCount,
            Duration = c.Duration,
            Description = c.Description
        });
    }
}