using CourseApi.Contracts;
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

    public async Task<IEnumerable<CourseResponse>> GetAllCoursesAsync()
    {
        var courses = await _repository.GetAllAsync();

        return courses.Select(MapToResponse);
    }

    public async Task<CourseResponse?> GetCourseByIdAsync(Guid id)
    {
        var course = await _repository.GetByIdAsync(id);

        if (course is null)
        {
            return null;
        }

        return MapToResponse(course);
    }

    public async Task<IEnumerable<CourseResponse>> SearchCoursesAsync(string query)
    {
        var courses = await _repository.SearchAsync(query);

        return courses.Select(MapToResponse);
    }

    public async Task<CourseResponse> CreateCourseAsync(CreateCourseRequest request)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Title = request.Title.Trim(),
            ImageUrl = request.ImageUrl.Trim()
        };

        await _repository.AddAsync(course);
        await _repository.SaveChangesAsync();

        return MapToResponse(course);
    }

    public async Task<CourseResponse?> UpdateCourseAsync(Guid id, UpdateCourseRequest request)
    {
        var course = await _repository.GetByIdAsync(id);

        if (course is null)
        {
            return null;
        }

        course.Title = request.Title.Trim();
        course.ImageUrl = request.ImageUrl.Trim();

        await _repository.SaveChangesAsync();

        return MapToResponse(course);
    }

    public async Task<bool> DeleteCourseAsync(Guid id)
    {
        var course = await _repository.GetByIdAsync(id);

        if (course is null)
        {
            return false;
        }

        _repository.Delete(course);
        await _repository.SaveChangesAsync();

        return true;
    }

    private static CourseResponse MapToResponse(Course course)
    {
        return new CourseResponse
        {
            Id = course.Id,
            Title = course.Title,
            ImageUrl = course.ImageUrl
        };
    }
}