using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynergyAccounts.Migrations
{
    /// <inheritdoc />
    public partial class companyset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoPath",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TagLine",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinNo",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VatRegistrationNo",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteLink",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoPath",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "TagLine",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "TinNo",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "VatRegistrationNo",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "WebsiteLink",
                table: "Companies");
        }
    }
}
