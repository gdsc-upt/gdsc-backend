using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GdscBackend.Migrations
{
    /// <inheritdoc />
    public partial class MenuItemsModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "MenuItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "MenuItems");
        }
    }
}
