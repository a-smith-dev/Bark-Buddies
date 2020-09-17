using Microsoft.EntityFrameworkCore.Migrations;

namespace BarkBuddies.Migrations
{
    public partial class ZipUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgeChoice",
                table: "UserProfiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeChoice",
                table: "UserProfiles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeChoice",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "SizeChoice",
                table: "UserProfiles");
        }
    }
}
