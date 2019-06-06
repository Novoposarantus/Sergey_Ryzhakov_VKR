using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addtestsresult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReferenceResponseSeconds",
                table: "QuestionToTests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "QuestionsResultAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssignmentModelId = table.Column<int>(nullable: false),
                    AnswerModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsResultAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionsResultAnswers_Answers_AnswerModelId",
                        column: x => x.AnswerModelId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsResultAnswers_Assignments_AssignmentModelId",
                        column: x => x.AssignmentModelId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestResultQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssignmentModelId = table.Column<int>(nullable: false),
                    QuestionModelId = table.Column<int>(nullable: false),
                    Milliseconds = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResultQuestions_Assignments_AssignmentModelId",
                        column: x => x.AssignmentModelId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestResultQuestions_Questions_QuestionModelId",
                        column: x => x.QuestionModelId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsResultAnswers_AnswerModelId",
                table: "QuestionsResultAnswers",
                column: "AnswerModelId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsResultAnswers_AssignmentModelId",
                table: "QuestionsResultAnswers",
                column: "AssignmentModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultQuestions_AssignmentModelId",
                table: "TestResultQuestions",
                column: "AssignmentModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultQuestions_QuestionModelId",
                table: "TestResultQuestions",
                column: "QuestionModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionsResultAnswers");

            migrationBuilder.DropTable(
                name: "TestResultQuestions");

            migrationBuilder.DropColumn(
                name: "ReferenceResponseSeconds",
                table: "QuestionToTests");
        }
    }
}
