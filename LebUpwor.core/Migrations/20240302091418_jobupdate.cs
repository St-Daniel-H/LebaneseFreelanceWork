using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LebUpwor.core.Migrations
{
    /// <inheritdoc />
    public partial class jobupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Users_FinishedByUserId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_FinishedByUserId",
                table: "Jobs");

            migrationBuilder.AddColumn<DateTime>(
                name: "SelectedUserDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SelectedUserId",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_SelectedUserId",
                table: "Jobs",
                column: "SelectedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Users_SelectedUserId",
                table: "Jobs",
                column: "SelectedUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Users_SelectedUserId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_SelectedUserId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "SelectedUserDate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "SelectedUserId",
                table: "Jobs");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_FinishedByUserId",
                table: "Jobs",
                column: "FinishedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Users_FinishedByUserId",
                table: "Jobs",
                column: "FinishedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
