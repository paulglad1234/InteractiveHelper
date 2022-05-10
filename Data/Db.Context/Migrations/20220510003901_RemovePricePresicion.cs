using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveHelper.Db.Context.Migrations
{
    public partial class RemovePricePresicion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "items",
                type: "real(10)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
