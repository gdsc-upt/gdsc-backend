using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GdscBackend.Migrations
{
    public partial class CreatedUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                "Created",
                "Teams",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Updated",
                "Teams",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Created",
                "Settings",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Updated",
                "Settings",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Created",
                "Pages",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Updated",
                "Pages",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Created",
                "MenuItems",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Updated",
                "MenuItems",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Created",
                "Members",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Updated",
                "Members",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Created",
                "Faqs",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Updated",
                "Faqs",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Created",
                "Examples",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Updated",
                "Examples",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Created",
                "Events",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Updated",
                "Events",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Created",
                "Contacts",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "Updated",
                "Contacts",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Created",
                "Teams");

            migrationBuilder.DropColumn(
                "Updated",
                "Teams");

            migrationBuilder.DropColumn(
                "Created",
                "Settings");

            migrationBuilder.DropColumn(
                "Updated",
                "Settings");

            migrationBuilder.DropColumn(
                "Created",
                "Pages");

            migrationBuilder.DropColumn(
                "Updated",
                "Pages");

            migrationBuilder.DropColumn(
                "Created",
                "MenuItems");

            migrationBuilder.DropColumn(
                "Updated",
                "MenuItems");

            migrationBuilder.DropColumn(
                "Created",
                "Members");

            migrationBuilder.DropColumn(
                "Updated",
                "Members");

            migrationBuilder.DropColumn(
                "Created",
                "Faqs");

            migrationBuilder.DropColumn(
                "Updated",
                "Faqs");

            migrationBuilder.DropColumn(
                "Created",
                "Examples");

            migrationBuilder.DropColumn(
                "Updated",
                "Examples");

            migrationBuilder.DropColumn(
                "Created",
                "Events");

            migrationBuilder.DropColumn(
                "Updated",
                "Events");

            migrationBuilder.DropColumn(
                "Created",
                "Contacts");

            migrationBuilder.DropColumn(
                "Updated",
                "Contacts");
        }
    }
}