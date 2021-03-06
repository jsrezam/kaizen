using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Migrations
{
    public partial class addIndexToStateCampaignDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "CampaignDetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CampaignDetails_State",
                table: "CampaignDetails",
                column: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CampaignDetails_State",
                table: "CampaignDetails");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "CampaignDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
