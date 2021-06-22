using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GdscBackend.Migrations
{
    public partial class AddedFileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "Title",
                "Examples",
                "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                "Files",
                table => new
                {
                    Id = table.Column<string>("text", nullable: false),
                    Name = table.Column<string>("text", nullable: true),
                    Path = table.Column<string>("text", nullable: true),
                    Extension = table.Column<string>("text", nullable: true),
                    Size = table.Column<long>("bigint", nullable: false),
                    Created = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>("timestamp without time zone", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Files", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Files");

            migrationBuilder.AlterColumn<string>(
                "Title",
                "Examples",
                "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}