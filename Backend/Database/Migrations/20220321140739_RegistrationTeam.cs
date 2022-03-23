using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingDays.Database.Migrations
{
    public partial class RegistrationTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "Registrations",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_TeamId",
                table: "Registrations",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Teams_TeamId",
                table: "Registrations",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Teams_TeamId",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_TeamId",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Registrations");
        }
    }
}
