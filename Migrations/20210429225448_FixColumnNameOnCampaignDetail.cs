using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Migrations
{
    public partial class FixColumnNameOnCampaignDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CallsNumber",
                table: "CampaignDetails",
                newName: "CallTimes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CallTimes",
                table: "CampaignDetails",
                newName: "CallsNumber");
        }
    }
}
