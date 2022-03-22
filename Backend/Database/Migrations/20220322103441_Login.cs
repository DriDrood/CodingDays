using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingDays.Database.Migrations
{
    public partial class Login : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Teams",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Teams",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Teams",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Teams",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Teams",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Teams",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Teams",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Teams",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Teams",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Teams",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Teams",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Teams",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Teams",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Teams");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Teams",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
