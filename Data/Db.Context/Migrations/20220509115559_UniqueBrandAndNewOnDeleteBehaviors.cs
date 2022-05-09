using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveHelper.Db.Context.Migrations
{
    public partial class UniqueBrandAndNewOnDeleteBehaviors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characteristics_categories_CategoryId",
                table: "characteristics");

            migrationBuilder.CreateIndex(
                name: "IX_brands_Name",
                table: "brands",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_characteristics_categories_CategoryId",
                table: "characteristics",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characteristics_categories_CategoryId",
                table: "characteristics");

            migrationBuilder.DropIndex(
                name: "IX_brands_Name",
                table: "brands");

            migrationBuilder.AddForeignKey(
                name: "FK_characteristics_categories_CategoryId",
                table: "characteristics",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
