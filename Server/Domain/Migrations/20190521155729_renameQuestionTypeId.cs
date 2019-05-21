using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class renameQuestionTypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestinTypeId",
                table: "Questions",
                newName: "QuestionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestionTypeId",
                table: "Questions",
                newName: "QuestinTypeId");
        }
    }
}
