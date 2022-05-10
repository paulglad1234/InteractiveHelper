using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveHelper.Db.Context.Migrations
{
    public partial class RefactorTestToQuiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_the_tests_TestId",
                table: "questions");

            migrationBuilder.DropForeignKey(
                name: "FK_results_the_tests_TestId",
                table: "results");

            migrationBuilder.RenameColumn(
                name: "TestId",
                table: "results",
                newName: "QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_results_TestId",
                table: "results",
                newName: "IX_results_QuizId");

            migrationBuilder.RenameColumn(
                name: "TestId",
                table: "questions",
                newName: "QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_questions_TestId",
                table: "questions",
                newName: "IX_questions_QuizId");

            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                table: "answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_questions_the_tests_QuizId",
                table: "questions",
                column: "QuizId",
                principalTable: "the_tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_results_the_tests_QuizId",
                table: "results",
                column: "QuizId",
                principalTable: "the_tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_the_tests_QuizId",
                table: "questions");

            migrationBuilder.DropForeignKey(
                name: "FK_results_the_tests_QuizId",
                table: "results");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "answers");

            migrationBuilder.RenameColumn(
                name: "QuizId",
                table: "results",
                newName: "TestId");

            migrationBuilder.RenameIndex(
                name: "IX_results_QuizId",
                table: "results",
                newName: "IX_results_TestId");

            migrationBuilder.RenameColumn(
                name: "QuizId",
                table: "questions",
                newName: "TestId");

            migrationBuilder.RenameIndex(
                name: "IX_questions_QuizId",
                table: "questions",
                newName: "IX_questions_TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_the_tests_TestId",
                table: "questions",
                column: "TestId",
                principalTable: "the_tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_results_the_tests_TestId",
                table: "results",
                column: "TestId",
                principalTable: "the_tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
