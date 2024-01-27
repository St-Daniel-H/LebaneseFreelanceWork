using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LebUpwor.core.Migrations
{
    /// <inheritdoc />
    public partial class removeDiamond2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diamond",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Diamond",
                table: "Users",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
