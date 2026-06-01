using CourseApi.Contracts;
using CourseApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers;

[ApiController]
[Route("api/admin/courses")]
[Authorize(Roles = "Admin")]
public sealed class AdminCoursesController(ICourseService courseService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<CourseResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<CourseResponse>> CreateCourse([FromBody] CreateCourseRequest? request)
    {
        if (request is null)
        {
            return BadRequest("Request body is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("Course title is required.");
        }

        if (string.IsNullOrWhiteSpace(request.ImageUrl))
        {
            return BadRequest("Course image url is required.");
        }

        var course = await courseService.CreateCourseAsync(request);

        return CreatedAtAction(
            nameof(CoursesController.GetCourseById),
            "Courses",
            new { id = course.Id },
            course);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType<CourseResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CourseResponse>> UpdateCourse(
        Guid id,
        [FromBody] UpdateCourseRequest? request)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Course id cannot be empty.");
        }

        if (request is null)
        {
            return BadRequest("Request body is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("Course title is required.");
        }

        if (string.IsNullOrWhiteSpace(request.ImageUrl))
        {
            return BadRequest("Course image url is required.");
        }

        var course = await courseService.UpdateCourseAsync(id, request);

        if (course is null)
        {
            return NotFound();
        }

        return Ok(course);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Course id cannot be empty.");
        }

        var deleted = await courseService.DeleteCourseAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}