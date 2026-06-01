namespace CourseApi.Contracts;

public class CourseResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;
}