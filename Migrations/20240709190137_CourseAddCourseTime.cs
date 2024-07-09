using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityProject.Migrations
{
    /// <inheritdoc />
    public partial class CourseAddCourseTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseTime",
                keyColumn: "Id",
                keyValue: new Guid("a4d0f741-a167-489e-a596-e288048c9575"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourseTimeId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "CourseTime",
                columns: new[] { "Id", "Days", "EndDate", "IsDeleted", "StartDate", "Time" },
                values: new object[] { new Guid("07bb4974-0bff-4a25-b840-c27ca5ef4b8e"), "[1,3,5]", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseTimeId",
                table: "Courses",
                column: "CourseTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseTime_CourseTimeId",
                table: "Courses",
                column: "CourseTimeId",
                principalTable: "CourseTime",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseTime_CourseTimeId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseTimeId",
                table: "Courses");

            migrationBuilder.DeleteData(
                table: "CourseTime",
                keyColumn: "Id",
                keyValue: new Guid("07bb4974-0bff-4a25-b840-c27ca5ef4b8e"));

            migrationBuilder.DropColumn(
                name: "CourseTimeId",
                table: "Courses");

            migrationBuilder.InsertData(
                table: "CourseTime",
                columns: new[] { "Id", "Days", "EndDate", "IsDeleted", "StartDate", "Time" },
                values: new object[] { new Guid("a4d0f741-a167-489e-a596-e288048c9575"), "[1,3,5]", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) });
        }
    }
}
