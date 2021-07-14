using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Migrations
{
    public partial class AddIndexToCampaign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_FinishDate",
                table: "Campaigns",
                column: "FinishDate");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_IsActive",
                table: "Campaigns",
                column: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Campaigns_FinishDate",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_IsActive",
                table: "Campaigns");
        }
    }
}
