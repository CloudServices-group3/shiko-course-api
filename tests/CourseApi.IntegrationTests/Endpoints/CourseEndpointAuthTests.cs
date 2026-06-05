using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CourseApi.Contracts;
using CourseApi.IntegrationTests.TestInfrastructure;

namespace CourseApi.IntegrationTests.Endpoints;

public sealed class CourseEndpointAuthTests : IClassFixture<CourseApiIntegrationTestFixture>
{
    private readonly HttpClient _client;

    public CourseEndpointAuthTests(CourseApiIntegrationTestFixture fixture)
    {
        _client = fixture.Client;
    }

    [Fact]
    public async Task GetCourses_WithoutToken_ReturnsUnauthorized()
    {
        var response = await _client.GetAsync("/api/courses");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetCourses_WithUserToken_ReturnsOk()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/api/courses");
        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            JwtTokenFactory.CreateUserToken());

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateCourse_WithoutToken_ReturnsUnauthorized()
    {
        var requestBody = new
        {
            title = "Test Course",
            imageUrl = "/images/test-course.png"
        };

        var response = await _client.PostAsJsonAsync("/api/admin/courses", requestBody);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateCourse_WithUserToken_ReturnsForbidden()
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "/api/admin/courses");
        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            JwtTokenFactory.CreateUserToken());

        request.Content = JsonContent.Create(new
        {
            title = "Test Course",
            imageUrl = "/images/test-course.png"
        });

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task CreateCourse_WithAdminToken_ReturnsCreated()
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "/api/admin/courses");
        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            JwtTokenFactory.CreateAdminToken());

        request.Content = JsonContent.Create(new
        {
            title = "Test Course",
            imageUrl = "/images/test-course.png"
        });

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var course = await response.Content.ReadFromJsonAsync<CourseResponse>();

        Assert.NotNull(course);
        Assert.NotEqual(Guid.Empty, course.Id);
        Assert.Equal("Test Course", course.Title);
        Assert.Equal("/images/test-course.png", course.ImageUrl);
    }
}