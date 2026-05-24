using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseApi.Migrations
{
    /// <inheritdoc />
    public partial class FixGuids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "Description", "Duration", "ImageUrl", "LessonCount", "Title" },
                values: new object[] { "Understand the basics of marketing in a digital world.", "1 hr 18 min", "/images/digital-marketing.png", 5, "Digital Marketing" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Duration", "ImageUrl", "LessonCount", "Title" },
                values: new object[,]
                {
                    { new Guid("0be85b35-ba14-417b-ba04-2a1c0bd0d509"), "Get started with Sketch and learn the basics of digital design.", "14 hr 25 min", "/images/sketch.png", 18, "Sketch for Designer" },
                    { new Guid("7a875d05-b4fb-4b27-bc50-b776aaef14d4"), "Build complete web applications with both frontend and backend.", "24 hr 45 min", "/images/fullstack.png", 30, "Full stack Developer" },
                    { new Guid("882d1c96-b77f-46cc-994c-d12bbd16a0de"), "Learn how to analyse and work with large sets of data.", "20 hr 40 min", "/images/data-science.png", 25, "Data Science & Analytics" },
                    { new Guid("d9da7d05-0605-4ef3-8fc2-ec0a1ff90cc1"), "Learn how to design user friendly interfaces from scratch.", "27 hr 55 min", "/images/uiux.png", 34, "UI/UX Design for Beginner" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("0be85b35-ba14-417b-ba04-2a1c0bd0d509"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("7a875d05-b4fb-4b27-bc50-b776aaef14d4"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("882d1c96-b77f-46cc-994c-d12bbd16a0de"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("d9da7d05-0605-4ef3-8fc2-ec0a1ff90cc1"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "Description", "Duration", "ImageUrl", "LessonCount", "Title" },
                values: new object[] { "Learn how to analyse and work with large sets of data.", "20 hr 40 min", "/images/data-science.png", 25, "Data Science & Analytics" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Duration", "ImageUrl", "LessonCount", "Title" },
                values: new object[,]
                {
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Understand the basics of marketing in a digital world.", "1 hr 18 min", "/images/digital-marketing.png", 5, "Digital Marketing" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "Learn how to design user friendly interfaces from scratch.", "27 hr 55 min", "/images/uiux.png", 34, "UI/UX Design for Beginner" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "Build complete web applications with both frontend and backend.", "24 hr 45 min", "/images/fullstack.png", 30, "Full stack Developer" },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), "Get started with Sketch and learn the basics of digital design.", "14 hr 25 min", "/images/sketch.png", 18, "Sketch for Designer" }
                });
        }
    }
}
