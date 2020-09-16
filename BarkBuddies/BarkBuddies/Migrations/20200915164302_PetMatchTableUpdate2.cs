using Microsoft.EntityFrameworkCore.Migrations;

namespace BarkBuddies.Migrations
{
    public partial class PetMatchTableUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PetId",
                table: "PetMatch",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetId",
                table: "PetMatch");
        }
    }
}
