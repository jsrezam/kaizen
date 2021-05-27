using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Migrations
{
    public partial class AddValidFieldsToCampaignDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CallTimes",
                table: "CampaignDetails",
                newName: "TotalCallTimes");

            migrationBuilder.RenameColumn(
                name: "CallDuration",
                table: "CampaignDetails",
                newName: "LastValidCallDuration");

            migrationBuilder.AddColumn<string>(
                name: "LastCallDuration",
                table: "CampaignDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastValidCallDate",
                table: "CampaignDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastCallDuration",
                table: "CampaignDetails");

            migrationBuilder.DropColumn(
                name: "LastValidCallDate",
                table: "CampaignDetails");

            migrationBuilder.RenameColumn(
                name: "TotalCallTimes",
                table: "CampaignDetails",
                newName: "CallTimes");

            migrationBuilder.RenameColumn(
                name: "LastValidCallDuration",
                table: "CampaignDetails",
                newName: "CallDuration");
        }
    }
}
