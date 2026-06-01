using CourseApi.Models;

namespace CourseApi.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();

    Task<Course?> GetByIdAsync(Guid id);

    Task<IEnumerable<Course>> SearchAsync(string query);

    Task AddAsync(Course course);

    void Delete(Course course);

    Task SaveChangesAsync();
}