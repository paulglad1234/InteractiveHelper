using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveHelper.Db.Context.Migrations
{
    public partial class TreeQuizResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_the_tests_QuizId",
                table: "questions");

            migrationBuilder.DropTable(
                name: "itemset_results");

            migrationBuilder.DropTable(
                name: "result_answers");

            migrationBuilder.DropTable(
                name: "results");

            migrationBuilder.DropIndex(
                name: "IX_questions_QuizId",
                table: "questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_the_tests",
                table: "the_tests");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "answers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "the_tests");

            migrationBuilder.RenameTable(
                name: "the_tests",
                newName: "quizes");

            migrationBuilder.RenameIndex(
                name: "IX_the_tests_Uid",
                table: "quizes",
                newName: "IX_quizes_Uid");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "questions",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "NextId",
                table: "questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeadId",
                table: "quizes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HelloMessage",
                table: "quizes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RootId",
                table: "quizes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "quizes",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "quizes",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_quizes",
                table: "quizes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "result_nodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_result_nodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_result_nodes_answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_result_nodes_questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_result_nodes_result_nodes_ParentId",
                        column: x => x.ParentId,
                        principalTable: "result_nodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "item_results",
                columns: table => new
                {
                    ItemsId = table.Column<int>(type: "int", nullable: false),
                    ResultsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_results", x => new { x.ItemsId, x.ResultsId });
                    table.ForeignKey(
                        name: "FK_item_results_items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_results_result_nodes_ResultsId",
                        column: x => x.ResultsId,
                        principalTable: "result_nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_questions_NextId",
                table: "questions",
                column: "NextId",
                unique: true,
                filter: "[NextId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_quizes_HeadId",
                table: "quizes",
                column: "HeadId",
                unique: true,
                filter: "[HeadId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_quizes_RootId",
                table: "quizes",
                column: "RootId",
                unique: true,
                filter: "[RootId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_item_results_ResultsId",
                table: "item_results",
                column: "ResultsId");

            migrationBuilder.CreateIndex(
                name: "IX_result_nodes_AnswerId",
                table: "result_nodes",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_result_nodes_ParentId",
                table: "result_nodes",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_result_nodes_QuestionId",
                table: "result_nodes",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_result_nodes_Uid",
                table: "result_nodes",
                column: "Uid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_questions_questions_NextId",
                table: "questions",
                column: "NextId",
                principalTable: "questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_quizes_questions_HeadId",
                table: "quizes",
                column: "HeadId",
                principalTable: "questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_quizes_result_nodes_RootId",
                table: "quizes",
                column: "RootId",
                principalTable: "result_nodes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_questions_NextId",
                table: "questions");

            migrationBuilder.DropForeignKey(
                name: "FK_quizes_questions_HeadId",
                table: "quizes");

            migrationBuilder.DropForeignKey(
                name: "FK_quizes_result_nodes_RootId",
                table: "quizes");

            migrationBuilder.DropTable(
                name: "item_results");

            migrationBuilder.DropTable(
                name: "result_nodes");

            migrationBuilder.DropIndex(
                name: "IX_questions_NextId",
                table: "questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_quizes",
                table: "quizes");

            migrationBuilder.DropIndex(
                name: "IX_quizes_HeadId",
                table: "quizes");

            migrationBuilder.DropIndex(
                name: "IX_quizes_RootId",
                table: "quizes");

            migrationBuilder.DropColumn(
                name: "NextId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "HeadId",
                table: "quizes");

            migrationBuilder.DropColumn(
                name: "HelloMessage",
                table: "quizes");

            migrationBuilder.DropColumn(
                name: "RootId",
                table: "quizes");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "quizes");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "quizes");

            migrationBuilder.RenameTable(
                name: "quizes",
                newName: "the_tests");

            migrationBuilder.RenameIndex(
                name: "IX_quizes_Uid",
                table: "the_tests",
                newName: "IX_the_tests_Uid");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "questions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                table: "answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "the_tests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_the_tests",
                table: "the_tests",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    Conclusion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_results_the_tests_QuizId",
                        column: x => x.QuizId,
                        principalTable: "the_tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_questions_QuizId",
                table: "questions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_itemset_results_ResultsId",
                table: "itemset_results",
                column: "ResultsId");

            migrationBuilder.CreateIndex(
                name: "IX_result_answers_ResultsId",
                table: "result_answers",
                column: "ResultsId");

            migrationBuilder.CreateIndex(
                name: "IX_results_QuizId",
                table: "results",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_results_Uid",
                table: "results",
                column: "Uid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_questions_the_tests_QuizId",
                table: "questions",
                column: "QuizId",
                principalTable: "the_tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
