using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThatBusLine.Migrations
{
    public partial class UserCredits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Credit",
                table: "AspNetUsers",
                type: "float",
                maxLength: 25,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "AspNetUsers");
        }
    }
}
