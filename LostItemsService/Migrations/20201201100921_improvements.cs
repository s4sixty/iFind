using Microsoft.EntityFrameworkCore.Migrations;

namespace LostItemsService.Migrations
{
    public partial class improvements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Comments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
