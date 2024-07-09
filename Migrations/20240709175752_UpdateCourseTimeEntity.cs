using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourseTimeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseTime",
                keyColumn: "Id",
                keyValue: new Guid("1088870d-e68b-4441-9144-ae02e79f4ac5"));

            migrationBuilder.DropColumn(
                name: "Day",
                table: "CourseTime");

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "CourseTime",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "CourseTime",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "CourseTime",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "CourseTime",
                columns: new[] { "Id", "Days", "EndDate", "IsDeleted", "StartDate", "Time" },
                values: new object[] { new Guid("a4d0f741-a167-489e-a596-e288048c9575"), "[1,3,5]", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseTime",
                keyColumn: "Id",
                keyValue: new Guid("a4d0f741-a167-489e-a596-e288048c9575"));

            migrationBuilder.DropColumn(
                name: "Days",
                table: "CourseTime");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CourseTime");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CourseTime");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "CourseTime",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "CourseTime",
                columns: new[] { "Id", "Day", "IsDeleted", "Time" },
                values: new object[] { new Guid("1088870d-e68b-4441-9144-ae02e79f4ac5"), 1, false, new TimeSpan(0, 9, 0, 0, 0) });
        }
    }
}
