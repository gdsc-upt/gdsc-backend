using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gdscwebbackend.Migrations
{
    /// <inheritdoc />
    public partial class IconAsFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "Technologies",
                newName: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_IconId",
                table: "Technologies",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technologies_Files_IconId",
                table: "Technologies",
                column: "IconId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_Files_IconId",
                table: "Technologies");

            migrationBuilder.DropIndex(
                name: "IX_Technologies_IconId",
                table: "Technologies");

            migrationBuilder.RenameColumn(
                name: "IconId",
                table: "Technologies",
                newName: "Icon");
        }
    }
}
