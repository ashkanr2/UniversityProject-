using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityProject.Migrations
{
    /// <inheritdoc />
    public partial class Name2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseTime",
                keyColumn: "Id",
                keyValue: new Guid("e2ac1253-304e-4c4e-bd71-fca15c492602"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CourseTime",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "CourseTime",
                columns: new[] { "Id", "Days", "EndDate", "IsDeleted", "Name", "StartDate", "Time" },
                values: new object[] { new Guid("79cd0337-2158-44f7-a7ac-54f6048267a3"), "[1,3,5]", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "", new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseTime",
                keyColumn: "Id",
                keyValue: new Guid("79cd0337-2158-44f7-a7ac-54f6048267a3"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CourseTime",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "CourseTime",
                columns: new[] { "Id", "Days", "EndDate", "IsDeleted", "Name", "StartDate", "Time" },
                values: new object[] { new Guid("e2ac1253-304e-4c4e-bd71-fca15c492602"), "[1,3,5]", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0) });
        }
    }
}
