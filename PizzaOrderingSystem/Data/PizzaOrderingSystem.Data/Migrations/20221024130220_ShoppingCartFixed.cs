using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaOrderingSystem.Data.Migrations
{
    public partial class ShoppingCartFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ShoppingCart_ShoppingCartId",
                table: "CartItems");

            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ShoppingCartId",
                table: "CartItems");

            migrationBuilder.AlterColumn<string>(
                name: "ShoppingCartId",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShoppingCartId",
                table: "CartItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_IsDeleted",
                table: "ShoppingCart",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ShoppingCart_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
