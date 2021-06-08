using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketingAssetSystem.Migrations
{
    public partial class Initials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransaction_ExternalEntities_InvTransactionId",
                table: "InventoryTransaction");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransaction_InvTransactionId",
                table: "InventoryTransaction");

            migrationBuilder.DropColumn(
                name: "InvTransactionId",
                table: "InventoryTransaction");

            migrationBuilder.DropColumn(
                name: "InvTransactionId",
                table: "ExternalEntities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InvTransactionId",
                table: "InventoryTransaction",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InvTransactionId",
                table: "ExternalEntities",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_InvTransactionId",
                table: "InventoryTransaction",
                column: "InvTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransaction_ExternalEntities_InvTransactionId",
                table: "InventoryTransaction",
                column: "InvTransactionId",
                principalTable: "ExternalEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
