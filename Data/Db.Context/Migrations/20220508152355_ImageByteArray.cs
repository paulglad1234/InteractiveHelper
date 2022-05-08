using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveHelper.Db.Context.Migrations
{
    public partial class ImageByteArray : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_item_characteristics_brands_BrandId",
                table: "item_characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_item_characteristics_categories_CategoryId",
                table: "item_characteristics");

            migrationBuilder.DropTable(
                name: "ItemCharacteristic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_item_characteristics",
                table: "item_characteristics");

            migrationBuilder.DropIndex(
                name: "IX_item_characteristics_BrandId",
                table: "item_characteristics");

            migrationBuilder.DropIndex(
                name: "IX_item_characteristics_Uid",
                table: "item_characteristics");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "item_characteristics");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "item_characteristics");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "item_characteristics");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "item_characteristics");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "item_characteristics");

            migrationBuilder.DropColumn(
                name: "Uid",
                table: "item_characteristics");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "item_characteristics",
                newName: "CharacteristicId");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "item_characteristics",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_item_characteristics_CategoryId",
                table: "item_characteristics",
                newName: "IX_item_characteristics_CharacteristicId");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "item_characteristics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "-");

            migrationBuilder.AddPrimaryKey(
                name: "PK_item_characteristics",
                table: "item_characteristics",
                columns: new[] { "ItemId", "CharacteristicId" });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(2048)", maxLength: 2048, nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_items_brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_items_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_items_BrandId",
                table: "items",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_items_CategoryId",
                table: "items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_items_Uid",
                table: "items",
                column: "Uid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_item_characteristics_characteristics_CharacteristicId",
                table: "item_characteristics",
                column: "CharacteristicId",
                principalTable: "characteristics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_item_characteristics_items_ItemId",
                table: "item_characteristics",
                column: "ItemId",
                principalTable: "items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_item_characteristics_characteristics_CharacteristicId",
                table: "item_characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_item_characteristics_items_ItemId",
                table: "item_characteristics");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_item_characteristics",
                table: "item_characteristics");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "item_characteristics");

            migrationBuilder.RenameColumn(
                name: "CharacteristicId",
                table: "item_characteristics",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "item_characteristics",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_item_characteristics_CharacteristicId",
                table: "item_characteristics",
                newName: "IX_item_characteristics_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "item_characteristics",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "item_characteristics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "item_characteristics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "item_characteristics",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "item_characteristics",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<Guid>(
                name: "Uid",
                table: "item_characteristics",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_item_characteristics",
                table: "item_characteristics",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ItemCharacteristic",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    CharacteristicId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "-")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCharacteristic", x => new { x.ItemId, x.CharacteristicId });
                    table.ForeignKey(
                        name: "FK_ItemCharacteristic_characteristics_CharacteristicId",
                        column: x => x.CharacteristicId,
                        principalTable: "characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemCharacteristic_item_characteristics_ItemId",
                        column: x => x.ItemId,
                        principalTable: "item_characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_item_characteristics_BrandId",
                table: "item_characteristics",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_item_characteristics_Uid",
                table: "item_characteristics",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemCharacteristic_CharacteristicId",
                table: "ItemCharacteristic",
                column: "CharacteristicId");

            migrationBuilder.AddForeignKey(
                name: "FK_item_characteristics_brands_BrandId",
                table: "item_characteristics",
                column: "BrandId",
                principalTable: "brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_item_characteristics_categories_CategoryId",
                table: "item_characteristics",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
