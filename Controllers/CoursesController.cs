using CourseApi.Contracts;
using CourseApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers;

[ApiController]
[Route("api/courses")]
[Authorize]
public sealed class CoursesController(ICourseService courseService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<CourseResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<CourseResponse>>> GetCourses()
    {
        var courses = await courseService.GetAllCoursesAsync();

        return Ok(courses);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<CourseResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CourseResponse>> GetCourseById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Course id cannot be empty.");
        }

        var course = await courseService.GetCourseByIdAsync(id);

        if (course is null)
        {
            return NotFound();
        }

        return Ok(course);
    }

    [HttpGet("search")]
    [ProducesResponseType<IEnumerable<CourseResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<CourseResponse>>> SearchCourses([FromQuery] string? q)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return BadRequest("Search query is required.");
        }

        var courses = await courseService.SearchCoursesAsync(q);

        return Ok(courses);
    }
}