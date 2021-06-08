using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketingAssetSystem.Migrations
{
    public partial class LinkProductMasterToInventoryDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductMasterId",
                table: "InventoryDetail",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "InventoryDetail",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "EntityCode",
                table: "ExternalEntities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDetail_ProductMasterId",
                table: "InventoryDetail",
                column: "ProductMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDetail_ProductMaster_ProductMasterId",
                table: "InventoryDetail",
                column: "ProductMasterId",
                principalTable: "ProductMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDetail_ProductMaster_ProductMasterId",
                table: "InventoryDetail");

            migrationBuilder.DropIndex(
                name: "IX_InventoryDetail_ProductMasterId",
                table: "InventoryDetail");

            migrationBuilder.DropColumn(
                name: "ProductMasterId",
                table: "InventoryDetail");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "InventoryDetail");

            migrationBuilder.DropColumn(
                name: "EntityCode",
                table: "ExternalEntities");
        }
    }
}
