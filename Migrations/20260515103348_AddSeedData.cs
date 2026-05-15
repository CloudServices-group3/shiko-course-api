using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Duration", "ImageUrl", "LessonCount", "Title" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "An introduction to artificial intelligence and machine learning.", "13 hr 35 min", "/images/ai.png", 15, "Artificial Intelligence" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Learn how to analyse and work with large sets of data.", "20 hr 40 min", "/images/data-science.png", 25, "Data Science & Analytics" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Understand the basics of marketing in a digital world.", "1 hr 18 min", "/images/digital-marketing.png", 5, "Digital Marketing" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "Learn how to design user friendly interfaces from scratch.", "27 hr 55 min", "/images/uiux.png", 34, "UI/UX Design for Beginner" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "Build complete web applications with both frontend and backend.", "24 hr 45 min", "/images/fullstack.png", 30, "Full stack Developer" },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), "Get started with Sketch and learn the basics of digital design.", "14 hr 25 min", "/images/sketch.png", 18, "Sketch for Designer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

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
        }
    }
}
