# Shiko Course Provider

Small ASP.NET Core Web API provider for base course data in Shiko LMS.

This provider handles the basic course identity data used by the frontend and by other course-related providers. It stores course id, title, and image url.

Other providers use the same course id to attach their own data, for example lesson exercises, ratings, reviews, FAQs, and course details.

Uses EF Core with Azure SQL. Tables are stored in the course schema because my providers share the same Azure SQL database.

Connection strings and JWT settings are stored with user secrets locally and environment variables in Azure.

## Repository note

This repository was not originally started by me. The API project was already placed directly in the repository root instead of using the same src/tests structure as my other providers.

I chose to keep the existing base structure and build on top of it instead of restarting the repository close to submission. This avoided unnecessary risk around deploy paths, migrations, project references, and Git history.

The integration tests are placed in a separate tests folder. Since the API project file is in the repository root, the tests folder is excluded from the main API project through DefaultItemExcludes in CourseApi.csproj.

## Live API

### Base URL

https://shiko-course-provider.azurewebsites.net

The base URL is only the root address for the API. It does not have its own page, so opening it directly can return 404. Use the endpoints below instead.

### Health check

https://shiko-course-provider.azurewebsites.net/health

### Scalar

https://shiko-course-provider.azurewebsites.net/scalar

### OpenAPI JSON

https://shiko-course-provider.azurewebsites.net/openapi/v1.json

## Endpoints

### Auth protected endpoints

These require JWT Bearer auth.

GET /api/courses

GET /api/courses/{id}

GET /api/courses/search?q={query}

These endpoints are used by the frontend to list courses, search courses, and show base course data on course detail pages.

### Admin endpoints

These require JWT Bearer auth with the Admin role.

POST /api/admin/courses

PUT /api/admin/courses/{id}

DELETE /api/admin/courses/{id}

The admin endpoints are used to manage the base course data.

CourseProvider only stores the basic course information. Other course content such as lessons, reviews, ratings, FAQs, and course details is handled by separate providers.

## Database

The provider uses EF Core with SQL Server.

Schema:

course

Main table:

course.Courses

Course fields:

* Id
* Title
* ImageUrl

The migration history table is also stored in the course schema.

## Local config

Set the database connection string with user secrets:

dotnet user-secrets set "ConnectionStrings:CourseDatabase" "your-connection-string" --project .\CourseApi.csproj

Set JWT config with user secrets:

dotnet user-secrets set "Jwt:Issuer" "your-issuer" --project .\CourseApi.csproj

dotnet user-secrets set "Jwt:Audience" "your-audience" --project .\CourseApi.csproj

dotnet user-secrets set "Jwt:SigningKey" "your-signing-key" --project .\CourseApi.csproj

## Azure config

Azure Web App uses environment variables and app settings.

The database connection string is stored as:

CourseDatabase

JWT app settings are stored as:

Jwt__Issuer

Jwt__Audience

Jwt__SigningKey

The app should not run with ASPNETCORE_ENVIRONMENT=Testing in Azure. The Testing environment is only used by integration tests.

## Run locally

Run the API with:

dotnet run --project .\CourseApi.csproj

Scalar opens at:

[https://localhost:your-port/scalar](https://localhost:your-port/scalar)

## Tests

The provider has integration tests.

Integration tests use WebApplicationFactory, Testcontainers, SQL Server container, and EF Core migrations against the test database.

Docker Desktop must be running before running all tests.

Run tests with:

dotnet test

The tests cover:

* health endpoint
* JWT protection for course endpoints
* admin role protection for admin endpoints
* creating a course as admin
* reading a course by id
* searching courses
* updating a course as admin
* deleting a course as admin

## Seed data

The provider has demo seed data for local and Azure use.

Seed data is handled in Data/CourseSeeder.cs.

The seeder does not run in the Testing environment because integration tests create their own clean SQL Server database and run migrations before testing.

## Notes

CourseProvider only owns base course data.

It does not own lesson exercises, ratings, reviews, FAQs, course overview text, key points, or progress data.

Those parts are handled by separate providers in the distributed LMS structure.

The CourseProvider structure is slightly different from my other providers because the repository was taken over instead of started from scratch.
