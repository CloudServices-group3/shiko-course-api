using CourseApi.Data;
using CourseApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly CourseDbContext _context;

    public CourseRepository(CourseDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllAsync() =>
        await _context.Courses.ToListAsync();

    public async Task<Course?> GetByIdAsync(Guid id) =>
        await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Course>> SearchAsync(string query) =>
        await _context.Courses
            .Where(c => c.Title.Contains(query))
            .ToListAsync();
}