using CourseApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _service;

    public CoursesController(ICourseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var courses = await _service.GetAllCoursesAsync();
            return Ok(courses);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message + " | " + ex.InnerException?.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var course = await _service.GetCourseByIdAsync(id);
            if (course is null) return NotFound();
            return Ok(course);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message + " | " + ex.InnerException?.Message);
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q)
    {
        try
        {
            var results = await _service.SearchCoursesAsync(q);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message + " | " + ex.InnerException?.Message);
        }
    }
}