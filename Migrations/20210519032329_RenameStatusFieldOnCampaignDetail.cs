using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Migrations
{
    public partial class RenameStatusFieldOnCampaignDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "CampaignDetails",
                newName: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "CampaignDetails",
                newName: "Status");
        }
    }
}
