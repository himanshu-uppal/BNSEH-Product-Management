using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductManager.DataAccess.Migrations
{
    public partial class addedsalepricepropertyforproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SalePrice",
                table: "Products",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Products");
        }
    }
}
