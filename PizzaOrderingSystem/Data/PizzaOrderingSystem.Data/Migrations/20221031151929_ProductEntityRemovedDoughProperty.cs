using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaOrderingSystem.Data.Migrations
{
    public partial class ProductEntityRemovedDoughProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dough",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dough",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
