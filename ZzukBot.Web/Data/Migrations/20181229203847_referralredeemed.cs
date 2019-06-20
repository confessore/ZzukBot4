using Microsoft.EntityFrameworkCore.Migrations;

namespace ZzukBot.Web.Data.Migrations
{
    public partial class referralredeemed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReferralRedeemed",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferralRedeemed",
                table: "AspNetUsers");
        }
    }
}
