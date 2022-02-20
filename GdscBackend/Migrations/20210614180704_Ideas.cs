using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GdscBackend.Migrations
{
    public partial class Ideas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Ideas",
                table => new
                {
                    Id = table.Column<string>("text", nullable: false),
                    Name = table.Column<string>("text", nullable: true),
                    Email = table.Column<string>("text", nullable: true),
                    Branch = table.Column<string>("text", nullable: true),
                    Year = table.Column<int>("integer", nullable: false),
                    Description = table.Column<string>("text", nullable: true),
                    Created = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>("timestamp without time zone", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Ideas", x => x.Id); });

            migrationBuilder.CreateTable(
                "Technologies",
                table => new
                {
                    Id = table.Column<string>("text", nullable: false),
                    Name = table.Column<string>("text", nullable: true),
                    Description = table.Column<string>("text", nullable: true),
                    Icon = table.Column<string>("text", nullable: true),
                    Created = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>("timestamp without time zone", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Technologies", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Ideas");

            migrationBuilder.DropTable(
                "Technologies");
        }
    }
}