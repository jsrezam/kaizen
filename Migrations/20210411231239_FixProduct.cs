using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Migrations
{
    public partial class FixProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discontinued",
                table: "Products",
                newName: "IsDiscontinued");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDiscontinued",
                table: "Products",
                newName: "Discontinued");
        }
    }
}
