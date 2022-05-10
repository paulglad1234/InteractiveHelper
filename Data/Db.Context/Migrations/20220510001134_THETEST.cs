using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveHelper.Db.Context.Migrations
{
    public partial class THETEST : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "items",
                type: "float(10)", // "float(10)" replaced "real(10), cannot specify width on datatype real"
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.CreateTable(
                name: "the_tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_the_tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: true), // !!! NULLABLE CHANGED TO TRUE MANUALLY !!!
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_questions_the_tests_TestId",
                        column: x => x.TestId,
                        principalTable: "the_tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Conclusion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_results_the_tests_TestId",
                        column: x => x.TestId,
                        principalTable: "the_tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_answers_questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "itemset_results",
                columns: table => new
                {
                    ItemsId = table.Column<int>(type: "int", nullable: false),
                    ResultsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemset_results", x => new { x.ItemsId, x.ResultsId });
                    table.ForeignKey(
                        name: "FK_itemset_results_items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itemset_results_results_ResultsId",
                        column: x => x.ResultsId,
                        principalTable: "results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "result_answers",
                columns: table => new
                {
                    AnswersId = table.Column<int>(type: "int", nullable: false),
                    ResultsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_result_answers", x => new { x.AnswersId, x.ResultsId });
                    table.ForeignKey(
                        name: "FK_result_answers_answers_AnswersId",
                        column: x => x.AnswersId,
                        principalTable: "answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_result_answers_results_ResultsId",
                        column: x => x.ResultsId,
                        principalTable: "results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_answers_QuestionId",
                table: "answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_answers_Uid",
                table: "answers",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_itemset_results_ResultsId",
                table: "itemset_results",
                column: "ResultsId"); // amazing

            migrationBuilder.CreateIndex(
                name: "IX_questions_TestId",
                table: "questions",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_questions_Uid",
                table: "questions",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_result_answers_ResultsId",
                table: "result_answers",
                column: "ResultsId"); // also amazing

            migrationBuilder.CreateIndex(
                name: "IX_results_TestId",
                table: "results",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_results_Uid",
                table: "results",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_the_tests_Uid",
                table: "the_tests",
                column: "Uid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "itemset_results");

            migrationBuilder.DropTable(
                name: "result_answers");

            migrationBuilder.DropTable(
                name: "answers");

            migrationBuilder.DropTable(
                name: "results");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "the_tests");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "items",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real(10)",
                oldPrecision: 10,
                oldScale: 2);
        }
    }
}
