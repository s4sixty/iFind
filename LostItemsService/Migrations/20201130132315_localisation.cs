using Microsoft.EntityFrameworkCore.Migrations;

namespace LostItemsService.Migrations
{
    public partial class localisation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "LostItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "city",
                table: "LostItems");
        }
    }
}
