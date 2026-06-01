using CourseApi.Data;
using CourseApi.Models;
using CourseApi.Repositories;
using CourseApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var jwtSigningKey = builder.Configuration["Jwt:SigningKey"];

if (string.IsNullOrWhiteSpace(jwtIssuer))
{
    throw new InvalidOperationException("JWT issuer is not configured.");
}

if (string.IsNullOrWhiteSpace(jwtAudience))
{
    throw new InvalidOperationException("JWT audience is not configured.");
}

if (string.IsNullOrWhiteSpace(jwtSigningKey))
{
    throw new InvalidOperationException("JWT signing key is not configured.");
}

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,

            ValidateAudience = true,
            ValidAudience = jwtAudience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSigningKey)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddDbContext<CourseDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CourseDatabase"),
        sqlOptions =>
        {
            sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "course");
        });
});

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

const string FrontendCorsPolicy = "FrontendCorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3000",
                "https://localhost:3000"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CourseDbContext>();

    if (!db.Courses.Any())
    {
        db.Courses.AddRange(
            new Course
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Title = "Artificial Intelligence",
                ImageUrl = "/images/ai.png"
            },
            new Course
            {
                Id = Guid.Parse("882d1c96-b77f-46cc-994c-d12bbd16a0de"),
                Title = "Data Science & Analytics",
                ImageUrl = "/images/data-science.png"
            },
            new Course
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Title = "Digital Marketing",
                ImageUrl = "/images/digital-marketing.png"
            },
            new Course
            {
                Id = Guid.Parse("d9da7d05-0605-4ef3-8fc2-ec0a1ff90cc1"),
                Title = "UI/UX Design for Beginner",
                ImageUrl = "/images/uiux.png"
            },
            new Course
            {
                Id = Guid.Parse("7a875d05-b4fb-4b27-bc50-b776aaef14d4"),
                Title = "Full stack Developer",
                ImageUrl = "/images/fullstack.png"
            },
            new Course
            {
                Id = Guid.Parse("0be85b35-ba14-417b-ba04-2a1c0bd0d509"),
                Title = "Sketch for Designer",
                ImageUrl = "/images/sketch.png"
            }
        );

        db.SaveChanges();
    }
}

app.UseHttpsRedirection();

app.UseCors(FrontendCorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/health", () =>
    Results.Ok(new
    {
        status = "Healthy",
        service = "Shiko Course Provider",
        utc = DateTime.UtcNow
    }))
    .WithName("HealthCheck")
    .WithTags("Health");

app.Run();