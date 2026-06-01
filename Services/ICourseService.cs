using CourseApi.Contracts;

namespace CourseApi.Services;

public interface ICourseService
{
    Task<IEnumerable<CourseResponse>> GetAllCoursesAsync();

    Task<CourseResponse?> GetCourseByIdAsync(Guid id);

    Task<IEnumerable<CourseResponse>> SearchCoursesAsync(string query);

    Task<CourseResponse> CreateCourseAsync(CreateCourseRequest request);

    Task<CourseResponse?> UpdateCourseAsync(Guid id, UpdateCourseRequest request);

    Task<bool> DeleteCourseAsync(Guid id);
}