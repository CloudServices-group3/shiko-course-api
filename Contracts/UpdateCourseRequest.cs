using System.ComponentModel.DataAnnotations;

namespace CourseApi.Contracts;

public class UpdateCourseRequest
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty;
}