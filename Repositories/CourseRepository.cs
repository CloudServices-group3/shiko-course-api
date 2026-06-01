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

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses
            .OrderBy(course => course.Title)
            .ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(Guid id)
    {
        return await _context.Courses
            .FirstOrDefaultAsync(course => course.Id == id);
    }

    public async Task<IEnumerable<Course>> SearchAsync(string query)
    {
        return await _context.Courses
            .Where(course => course.Title.Contains(query))
            .OrderBy(course => course.Title)
            .ToListAsync();
    }

    public async Task AddAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
    }

    public void Delete(Course course)
    {
        _context.Courses.Remove(course);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}