using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityProject.Migrations
{
    /// <inheritdoc />
    public partial class NameFoeCourseTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseTime",
                keyColumn: "Id",
                keyValue: new Guid("07bb4974-0bff-4a25-b840-c27ca5ef4b8e"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CourseTime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "CourseTime",
                columns: new[] { "Id", "Days", "EndDate", "IsDeleted", "Name", "StartDate", "Time" },
                values: new object[] { new Guid("e2ac1253-304e-4c4e-bd71-fca15c492602"), "[1,3,5]", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseTime",
                keyColumn: "Id",
                keyValue: new Guid("e2ac1253-304e-4c4e-bd71-fca15c492602"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CourseTime");

            migrationBuilder.InsertData(
                table: "CourseTime",
                columns: new[] { "Id", "Days", "EndDate", "IsDeleted", "StartDate", "Time" },
                values: new object[] { new Guid("07bb4974-0bff-4a25-b840-c27ca5ef4b8e"), "[1,3,5]", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) });
        }
    }
}
