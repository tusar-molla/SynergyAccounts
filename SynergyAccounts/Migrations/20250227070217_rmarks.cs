﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynergyAccounts.Migrations
{
    /// <inheritdoc />
    public partial class rmarks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Companies");
        }
    }
}
