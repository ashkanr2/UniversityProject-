using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityProject.Migrations
{
    /// <inheritdoc />
    public partial class REalationCourseAndImageAddImAgeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImageTypeId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId1",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImageType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ImageTypeId",
                table: "Images",
                column: "ImageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ImageId",
                table: "Courses",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ImageId1",
                table: "Courses",
                column: "ImageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Images_ImageId",
                table: "Courses",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Images_ImageId1",
                table: "Courses",
                column: "ImageId1",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ImageType_ImageTypeId",
                table: "Images",
                column: "ImageTypeId",
                principalTable: "ImageType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Images_ImageId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Images_ImageId1",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_ImageType_ImageTypeId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "ImageType");

            migrationBuilder.DropIndex(
                name: "IX_Images_ImageTypeId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ImageId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ImageId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ImageTypeId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ImageId1",
                table: "Courses");
        }
    }
}
