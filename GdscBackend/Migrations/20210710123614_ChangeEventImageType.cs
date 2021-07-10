using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gdsc_web_backend.Migrations
{
    public partial class ChangeEventImageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Events",
                newName: "ImageId");

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Events",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Events",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Events_ImageId",
                table: "Events",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Files_ImageId",
                table: "Events",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Files_ImageId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_ImageId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "End",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Events",
                newName: "Image");
        }
    }
}
