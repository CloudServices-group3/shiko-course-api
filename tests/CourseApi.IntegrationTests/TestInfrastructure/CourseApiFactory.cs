using CourseApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CourseApi.IntegrationTests.TestInfrastructure;

public sealed class CourseApiFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public CourseApiFactory(string connectionString)
    {
        _connectionString = connectionString;

        Environment.SetEnvironmentVariable(
            "ConnectionStrings__CourseDatabase",
            connectionString);

        Environment.SetEnvironmentVariable("Jwt__Issuer", "test-issuer");
        Environment.SetEnvironmentVariable("Jwt__Audience", "test-audience");
        Environment.SetEnvironmentVariable(
            "Jwt__SigningKey",
            "test-signing-key-for-integration-tests-123456789");
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<CourseDbContext>();
            services.RemoveAll<DbContextOptions<CourseDbContext>>();

            services.AddDbContext<CourseDbContext>(options =>
            {
                options.UseSqlServer(
                    _connectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsHistoryTable(
                            "__EFMigrationsHistory",
                            "course");
                    });
            });
        });
    }

    public async Task ApplyMigrationsAsync()
    {
        using var scope = Services.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<CourseDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}