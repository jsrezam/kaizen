using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Migrations
{
    public partial class TurnUniqueCellPhoneOnCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CellPhone",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CellPhone",
                table: "Customers",
                column: "CellPhone",
                unique: true,
                filter: "[CellPhone] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_CellPhone",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "CellPhone",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
