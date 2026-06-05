using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CourseApi.Contracts;
using CourseApi.IntegrationTests.TestInfrastructure;
using CourseApi.Models;

namespace CourseApi.IntegrationTests.Endpoints;

public sealed class CourseEndpointTests : IClassFixture<CourseApiIntegrationTestFixture>
{
    private readonly CourseApiIntegrationTestFixture _fixture;

    public CourseEndpointTests(CourseApiIntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetCourseById_WithUserTokenAndExistingCourse_ReturnsOk()
    {
        var courseId = Guid.NewGuid();

        await _fixture.SeedCoursesAsync(new Course
        {
            Id = courseId,
            Title = "Test Course",
            ImageUrl = "/images/test-course.png"
        });

        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"/api/courses/{courseId}");

        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            JwtTokenFactory.CreateUserToken("user-1"));

        var response = await _fixture.Client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var course = await response.Content.ReadFromJsonAsync<CourseResponse>();

        Assert.NotNull(course);
        Assert.Equal(courseId, course.Id);
        Assert.Equal("Test Course", course.Title);
        Assert.Equal("/images/test-course.png", course.ImageUrl);
    }

    [Fact]
    public async Task SearchCourses_WithUserTokenAndMatchingCourse_ReturnsMatchingCourse()
    {
        var courseId = Guid.NewGuid();

        await _fixture.SeedCoursesAsync(new Course
        {
            Id = courseId,
            Title = "Searchable Course",
            ImageUrl = "/images/searchable-course.png"
        });

        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/api/courses/search?q=Searchable");

        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            JwtTokenFactory.CreateUserToken("user-1"));

        var response = await _fixture.Client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var courses = await response.Content.ReadFromJsonAsync<List<CourseResponse>>();

        Assert.NotNull(courses);

        var course = Assert.Single(
            courses,
            item => item.Id == courseId);

        Assert.Equal("Searchable Course", course.Title);
        Assert.Equal("/images/searchable-course.png", course.ImageUrl);
    }

    [Fact]
    public async Task UpdateCourse_WithAdminTokenAndExistingCourse_ReturnsOk()
    {
        var courseId = Guid.NewGuid();

        await _fixture.SeedCoursesAsync(new Course
        {
            Id = courseId,
            Title = "Old Course Title",
            ImageUrl = "/images/old-course.png"
        });

        using var request = new HttpRequestMessage(
            HttpMethod.Put,
            $"/api/admin/courses/{courseId}");

        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            JwtTokenFactory.CreateAdminToken());

        request.Content = JsonContent.Create(new UpdateCourseRequest
        {
            Title = "Updated Course Title",
            ImageUrl = "/images/updated-course.png"
        });

        var response = await _fixture.Client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var course = await response.Content.ReadFromJsonAsync<CourseResponse>();

        Assert.NotNull(course);
        Assert.Equal(courseId, course.Id);
        Assert.Equal("Updated Course Title", course.Title);
        Assert.Equal("/images/updated-course.png", course.ImageUrl);
    }

    [Fact]
    public async Task DeleteCourse_WithAdminTokenAndExistingCourse_ReturnsNoContent()
    {
        var courseId = Guid.NewGuid();

        await _fixture.SeedCoursesAsync(new Course
        {
            Id = courseId,
            Title = "Course To Delete",
            ImageUrl = "/images/course-to-delete.png"
        });

        using var request = new HttpRequestMessage(
            HttpMethod.Delete,
            $"/api/admin/courses/{courseId}");

        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            JwtTokenFactory.CreateAdminToken());

        var response = await _fixture.Client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}