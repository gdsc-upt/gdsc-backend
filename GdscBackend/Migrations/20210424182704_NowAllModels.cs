using Microsoft.EntityFrameworkCore.Migrations;

namespace GdscBackend.Migrations
{
    public partial class NowAllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Events",
                table => new
                {
                    Id = table.Column<string>("text", nullable: false),
                    Title = table.Column<string>("text", nullable: true),
                    Description = table.Column<string>("text", nullable: true),
                    Image = table.Column<string>("text", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Events", x => x.Id); });

            migrationBuilder.CreateTable(
                "Faqs",
                table => new
                {
                    Id = table.Column<string>("text", nullable: false),
                    Question = table.Column<string>("text", nullable: true),
                    Answer = table.Column<string>("text", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Faqs", x => x.Id); });

            migrationBuilder.CreateTable(
                "Members",
                table => new
                {
                    Id = table.Column<string>("text", nullable: false),
                    Name = table.Column<string>("text", nullable: true),
                    Email = table.Column<string>("text", nullable: true),
                    TeamId = table.Column<string>("text", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Members", x => x.Id); });

            migrationBuilder.CreateTable(
                "MenuItems",
                table => new
                {
                    Id = table.Column<string>("text", nullable: false),
                    Name = table.Column<string>("text", nullable: true),
                    Type = table.Column<int>("integer", nullable: false),
                    Link = table.Column<string>("text", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_MenuItems", x => x.Id); });

            migrationBuilder.CreateTable(
                "Pages",
                table => new
                {
                    Id = table.Column<string>("text", nullable: false),
                    Title = table.Column<string>("text", nullable: true),
                    Body = table.Column<string>("text", nullable: true),
                    isPublished = table.Column<bool>("boolean", nullable: false),
                    Slug = table.Column<string>("text", nullable: true),
                    ShortDescription = table.Column<string>("text", nullable: true),
                    Image = table.Column<string>("text", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Pages", x => x.Id); });

            migrationBuilder.CreateTable(
                "Settings",
                table => new
                {
                    Id = table.Column<string>("text", nullable: false),
                    Name = table.Column<string>("text", nullable: true),
                    Slug = table.Column<string>("text", nullable: true),
                    Type = table.Column<int>("integer", nullable: false),
                    Value = table.Column<bool>("boolean", nullable: false),
                    Image = table.Column<string>("text", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Settings", x => x.Id); });

            migrationBuilder.CreateTable(
                "Teams",
                table => new
                {
                    Id = table.Column<string>("text", nullable: false),
                    Name = table.Column<string>("text", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Teams", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Events");

            migrationBuilder.DropTable(
                "Faqs");

            migrationBuilder.DropTable(
                "Members");

            migrationBuilder.DropTable(
                "MenuItems");

            migrationBuilder.DropTable(
                "Pages");

            migrationBuilder.DropTable(
                "Settings");

            migrationBuilder.DropTable(
                "Teams");
        }
    }
}