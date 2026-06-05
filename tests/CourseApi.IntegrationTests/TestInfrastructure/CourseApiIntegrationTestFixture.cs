using CourseApi.Data;
using CourseApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace CourseApi.IntegrationTests.TestInfrastructure;

public sealed class CourseApiIntegrationTestFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _sqlServer = new MsSqlBuilder(
        "mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04")
        .Build();

    private CourseApiFactory? _factory;

    public HttpClient Client { get; private set; } = null!;

    public IServiceProvider Services => _factory!.Services;

    public async Task InitializeAsync()
    {
        await _sqlServer.StartAsync();

        _factory = new CourseApiFactory(_sqlServer.GetConnectionString());

        await _factory.ApplyMigrationsAsync();

        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });
    }

    public async Task DisposeAsync()
    {
        Client?.Dispose();

        _factory?.Dispose();

        await _sqlServer.DisposeAsync();
    }

    public async Task SeedCoursesAsync(params Course[] courses)
    {
        using var scope = Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<CourseDbContext>();

        dbContext.Courses.AddRange(courses);

        await dbContext.SaveChangesAsync();
    }
}