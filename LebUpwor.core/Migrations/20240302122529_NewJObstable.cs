using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LebUpwor.core.Migrations
{
    /// <inheritdoc />
    public partial class NewJObstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectCount",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsMarkedAsDone",
                table: "AppliedToTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "IsMarkedAsDoneDate",
                table: "AppliedToTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "NewJobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewJobs", x => new { x.JobId, x.UserId });
                    table.ForeignKey(
                        name: "FK_NewJobs_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId");
                    table.ForeignKey(
                        name: "FK_NewJobs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewJobs_JobId",
                table: "NewJobs",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewJobs_UserId",
                table: "NewJobs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewJobs");

            migrationBuilder.DropColumn(
                name: "SelectCount",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsMarkedAsDone",
                table: "AppliedToTasks");

            migrationBuilder.DropColumn(
                name: "IsMarkedAsDoneDate",
                table: "AppliedToTasks");
        }
    }
}
