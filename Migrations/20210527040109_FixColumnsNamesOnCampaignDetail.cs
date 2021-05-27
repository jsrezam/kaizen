using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Migrations
{
    public partial class FixColumnsNamesOnCampaignDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalCallTimes",
                table: "CampaignDetails",
                newName: "TotalCallsNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalCallsNumber",
                table: "CampaignDetails",
                newName: "TotalCallTimes");
        }
    }
}
