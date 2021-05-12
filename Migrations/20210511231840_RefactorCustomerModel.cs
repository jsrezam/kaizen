using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Migrations
{
    public partial class RefactorCustomerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Customers",
                newName: "PostalCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Customers",
                newName: "CompanyName");
        }
    }
}
