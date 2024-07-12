using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityProject.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTimeToStartTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseTime",
                keyColumn: "Id",
                keyValue: new Guid("b660297d-12b1-4a7c-ba21-194bac99df39"));

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Courses",
                newName: "StartTime");

            migrationBuilder.InsertData(
                table: "CourseTime",
                columns: new[] { "Id", "Days", "EndDate", "IsDeleted", "Name", "StartDate", "Time" },
                values: new object[] { new Guid("a6b0022a-6e26-44cf-ae38-0f25d161b10c"), "[1,3,5]", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseTime",
                keyColumn: "Id",
                keyValue: new Guid("a6b0022a-6e26-44cf-ae38-0f25d161b10c"));

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Courses",
                newName: "Time");

            migrationBuilder.InsertData(
                table: "CourseTime",
                columns: new[] { "Id", "Days", "EndDate", "IsDeleted", "Name", "StartDate", "Time" },
                values: new object[] { new Guid("b660297d-12b1-4a7c-ba21-194bac99df39"), "[1,3,5]", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) });
        }
    }
}
