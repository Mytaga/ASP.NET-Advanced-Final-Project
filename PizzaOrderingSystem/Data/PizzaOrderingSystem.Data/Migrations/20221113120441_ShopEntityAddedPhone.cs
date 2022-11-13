using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaOrderingSystem.Data.Migrations
{
    public partial class ShopEntityAddedPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Shops",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Shops");
        }
    }
}
