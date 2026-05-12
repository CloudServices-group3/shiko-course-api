namespace CourseApi.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int LessonCount { get; set; }
    public string Duration { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}