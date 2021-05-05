using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Migrations
{
    public partial class removeaddQuantityPerUnitUnitsOnOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityPerUnit",
                table: "Products",
                newName: "UnitsOnOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitsOnOrder",
                table: "Products",
                newName: "QuantityPerUnit");
        }
    }
}
