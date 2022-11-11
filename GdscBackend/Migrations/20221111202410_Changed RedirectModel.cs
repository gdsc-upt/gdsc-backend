using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gdsc_web_backend.Migrations
{
    public partial class ChangedRedirectModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "redirectTo",
                table: "Redirects",
                newName: "RedirectTo");

            migrationBuilder.RenameColumn(
                name: "path",
                table: "Redirects",
                newName: "Path");

            migrationBuilder.CreateIndex(
                name: "IX_Redirects_Path",
                table: "Redirects",
                column: "Path",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Redirects_Path",
                table: "Redirects");

            migrationBuilder.RenameColumn(
                name: "RedirectTo",
                table: "Redirects",
                newName: "redirectTo");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Redirects",
                newName: "path");
        }
    }
}
