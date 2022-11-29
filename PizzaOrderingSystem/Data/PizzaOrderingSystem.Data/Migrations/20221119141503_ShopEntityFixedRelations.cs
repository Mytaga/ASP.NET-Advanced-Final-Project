using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaOrderingSystem.Data.Migrations
{
    public partial class ShopEntityFixedRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Shops_ShopId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ShopId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Sales");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShopId",
                table: "Sales",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ShopId",
                table: "Sales",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Shops_ShopId",
                table: "Sales",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id");
        }
    }
}
