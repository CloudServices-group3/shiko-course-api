using CourseApi.Models;

namespace CourseApi.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task<IEnumerable<Course>> SearchAsync(string query);
}