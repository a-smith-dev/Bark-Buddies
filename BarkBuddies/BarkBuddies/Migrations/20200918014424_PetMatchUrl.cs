using Microsoft.EntityFrameworkCore.Migrations;

namespace BarkBuddies.Migrations
{
    public partial class PetMatchUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "PetMatch",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "PetMatch");
        }
    }
}
