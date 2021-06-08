using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketingAssetSystem.Migrations
{
    public partial class ExternalEntityNulableininventorytrans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransaction_ExternalEntities_ExternalEntityId",
                table: "InventoryTransaction");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExternalEntityId",
                table: "InventoryTransaction",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransaction_ExternalEntities_ExternalEntityId",
                table: "InventoryTransaction",
                column: "ExternalEntityId",
                principalTable: "ExternalEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransaction_ExternalEntities_ExternalEntityId",
                table: "InventoryTransaction");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExternalEntityId",
                table: "InventoryTransaction",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransaction_ExternalEntities_ExternalEntityId",
                table: "InventoryTransaction",
                column: "ExternalEntityId",
                principalTable: "ExternalEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
