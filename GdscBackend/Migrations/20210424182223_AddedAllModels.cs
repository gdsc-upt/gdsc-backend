using Microsoft.EntityFrameworkCore.Migrations;

namespace GdscBackend.Migrations
{
    public partial class AddedAllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Contacts",
                table => new
                {
                    Id = table.Column<string>("text", nullable: false),
                    Name = table.Column<string>("text", nullable: true),
                    Email = table.Column<string>("text", nullable: true),
                    Subject = table.Column<string>("text", nullable: true),
                    Text = table.Column<string>("text", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Contacts", x => x.Id); });

            migrationBuilder.CreateTable(
                "Examples",
                table => new
                {
                    Id = table.Column<string>("text", nullable: false),
                    Title = table.Column<string>("text", nullable: true),
                    Type = table.Column<int>("integer", nullable: false),
                    Number = table.Column<int>("integer", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Examples", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Contacts");

            migrationBuilder.DropTable(
                "Examples");
        }
    }
}