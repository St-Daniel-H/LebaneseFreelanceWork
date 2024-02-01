using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LebUpwor.core.Migrations
{
    /// <inheritdoc />
    public partial class addedPasswordSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "googleAccountId",
                table: "Users",
                newName: "GoogleAccountId");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "GoogleAccountId",
                table: "Users",
                newName: "googleAccountId");
        }
    }
}
