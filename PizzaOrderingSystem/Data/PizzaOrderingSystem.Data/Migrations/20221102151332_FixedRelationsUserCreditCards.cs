using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaOrderingSystem.Data.Migrations
{
    public partial class FixedRelationsUserCreditCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CreditCards_CreditCardId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CreditCardId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreditCardId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreditCardId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CreditCardId",
                table: "AspNetUsers",
                column: "CreditCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CreditCards_CreditCardId",
                table: "AspNetUsers",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "Id");
        }
    }
}
