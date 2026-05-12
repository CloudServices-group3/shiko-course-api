using CourseApi.DTOs;

namespace CourseApi.Services;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
    Task<CourseDto?> GetCourseByIdAsync(int id);
    Task<IEnumerable<CourseDto>> SearchCoursesAsync(string query);
}