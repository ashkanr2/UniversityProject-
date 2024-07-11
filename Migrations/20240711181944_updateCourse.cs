using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityProject.Migrations
{
    /// <inheritdoc />
    public partial class updateCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseTime",
                keyColumn: "Id",
                keyValue: new Guid("79cd0337-2158-44f7-a7ac-54f6048267a3"));

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Courses",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.InsertData(
                table: "CourseTime",
                columns: new[] { "Id", "Days", "EndDate", "IsDeleted", "Name", "StartDate", "Time" },
                values: new object[] { new Guid("b660297d-12b1-4a7c-ba21-194bac99df39"), "[1,3,5]", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseTime",
                keyColumn: "Id",
                keyValue: new Guid("b660297d-12b1-4a7c-ba21-194bac99df39"));

            migrationBuilder.DropColumn(
                name: "Days",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Courses");

            migrationBuilder.InsertData(
                table: "CourseTime",
                columns: new[] { "Id", "Days", "EndDate", "IsDeleted", "Name", "StartDate", "Time" },
                values: new object[] { new Guid("79cd0337-2158-44f7-a7ac-54f6048267a3"), "[1,3,5]", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) });
        }
    }
}
