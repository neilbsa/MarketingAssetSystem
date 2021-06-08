using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketingAssetSystem.Migrations
{
    public partial class DisregardTransactionLookupList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LookupLists_ProductGroups_TransactionId",
                table: "LookupLists");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "LookupLists",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_LookupLists_ProductGroups_TransactionId",
                table: "LookupLists",
                column: "TransactionId",
                principalTable: "ProductGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LookupLists_ProductGroups_TransactionId",
                table: "LookupLists");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "LookupLists",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LookupLists_ProductGroups_TransactionId",
                table: "LookupLists",
                column: "TransactionId",
                principalTable: "ProductGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
