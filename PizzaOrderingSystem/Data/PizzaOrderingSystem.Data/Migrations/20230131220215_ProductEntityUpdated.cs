using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaOrderingSystem.Data.Migrations
{
    public partial class ProductEntityUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Products_ProductId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Shops_ProductId",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Shops");

            //migrationBuilder.AlterColumn<string>(
            //    name: "CardNumber",
            //    table: "CreditCards",
            //    type: "nvarchar(16)",
            //    maxLength: 16,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(19)",
            //    oldMaxLength: 19);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Shops",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "CreditCards",
                type: "nvarchar(19)",
                maxLength: 19,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16);

            migrationBuilder.CreateIndex(
                name: "IX_Shops_ProductId",
                table: "Shops",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Products_ProductId",
                table: "Shops",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
