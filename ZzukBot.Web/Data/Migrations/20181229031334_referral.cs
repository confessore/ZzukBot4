using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZzukBot.Web.Data.Migrations
{
    public partial class referral : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubscriptionExpiration",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "Referral",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Referral",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubscriptionExpiration",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
