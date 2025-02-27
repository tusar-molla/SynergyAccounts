using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynergyAccounts.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Companies_CompanyId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_CompanyId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Subscriptions");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Branches_SubscriptionId",
                table: "Branches",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Subscriptions_SubscriptionId",
                table: "Branches",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Subscriptions_SubscriptionId",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_SubscriptionId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Branches");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CompanyId",
                table: "Subscriptions",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Companies_CompanyId",
                table: "Subscriptions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
