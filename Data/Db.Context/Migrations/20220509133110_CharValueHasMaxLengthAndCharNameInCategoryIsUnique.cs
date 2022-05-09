using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveHelper.Db.Context.Migrations
{
    public partial class CharValueHasMaxLengthAndCharNameInCategoryIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "item_characteristics",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "-",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "-");

            migrationBuilder.CreateIndex(
                name: "IX_characteristics_Name_CategoryId",
                table: "characteristics",
                columns: new[] { "Name", "CategoryId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_characteristics_Name_CategoryId",
                table: "characteristics");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "item_characteristics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "-",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "-");
        }
    }
}
