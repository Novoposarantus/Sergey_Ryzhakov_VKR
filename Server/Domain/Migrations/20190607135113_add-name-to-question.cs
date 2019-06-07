using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addnametoquestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Questions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Questions");
        }
    }
}
