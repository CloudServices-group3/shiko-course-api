using System.Net;
using CourseApi.IntegrationTests.TestInfrastructure;

namespace CourseApi.IntegrationTests.Endpoints;

public sealed class HealthEndpointTests : IClassFixture<CourseApiIntegrationTestFixture>
{
    private readonly HttpClient _client;

    public HealthEndpointTests(CourseApiIntegrationTestFixture fixture)
    {
        _client = fixture.Client;
    }

    [Fact]
    public async Task GetHealth_ReturnsOk()
    {
        var response = await _client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}