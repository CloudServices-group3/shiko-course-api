using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CourseApi.Data;

public class CourseDbContextFactory : IDesignTimeDbContextFactory<CourseDbContext>
{
    public CourseDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CourseDbContext>();
        optionsBuilder.UseSqlServer("Server=tcp:shiko-lms-sql.database.windows.net,1433;Initial Catalog=ShikoAuthDb;Persist Security Info=False;User ID=CloudSA60d426fe;Password=Shiko123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

        return new CourseDbContext(optionsBuilder.Options);
    }
}