using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_rpg.Migrations
{
    public partial class AddUserFootballClubRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FootballClubs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FootballClubs_UserId",
                table: "FootballClubs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FootballClubs_Users_UserId",
                table: "FootballClubs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FootballClubs_Users_UserId",
                table: "FootballClubs");

            migrationBuilder.DropIndex(
                name: "IX_FootballClubs_UserId",
                table: "FootballClubs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FootballClubs");
        }
    }
}
