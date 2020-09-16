using Microsoft.EntityFrameworkCore.Migrations;

namespace BarkBuddies.Migrations
{
    public partial class PetMatchTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Organization",
                table: "PetMatch");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "PetMatch");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "PetMatch");

            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "PetMatch",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "PetMatch",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PetMatch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PetMatch",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PetMatch_UserId",
                table: "PetMatch",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PetMatch_AspNetUsers_UserId",
                table: "PetMatch",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetMatch_AspNetUsers_UserId",
                table: "PetMatch");

            migrationBuilder.DropIndex(
                name: "IX_PetMatch_UserId",
                table: "PetMatch");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PetMatch");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PetMatch");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "PetMatch",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Age",
                table: "PetMatch",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "PetMatch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "PetMatch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "PetMatch",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
