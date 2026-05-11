using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shiko.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAssignments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "Hours", "IconUrl", "Level", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 11, 18, 7, 11, 591, DateTimeKind.Utc).AddTicks(5352), 12, null, 2, "Design Accessibility" },
                    { 2, new DateTime(2026, 5, 11, 18, 7, 11, 591, DateTimeKind.Utc).AddTicks(7320), 16, null, 1, "Figma for Beginner" },
                    { 3, new DateTime(2026, 5, 11, 18, 7, 11, 591, DateTimeKind.Utc).AddTicks(7326), 22, null, 2, "Framer Design" },
                    { 4, new DateTime(2026, 5, 11, 18, 7, 11, 591, DateTimeKind.Utc).AddTicks(7332), 14, null, 1, "Frontend Development" },
                    { 5, new DateTime(2026, 5, 11, 18, 7, 11, 591, DateTimeKind.Utc).AddTicks(7334), 16, null, 1, "Behance Case Study" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignments_CourseId",
                table: "UserAssignments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignments_UserId_CourseId",
                table: "UserAssignments",
                columns: new[] { "UserId", "CourseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAssignments");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
