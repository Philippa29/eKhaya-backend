using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eKhaya.Migrations
{
    /// <inheritdoc />
    public partial class amenitiesupdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Units_UnitId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_UnitId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Applications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UnitId",
                table: "Applications",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Units_UnitId",
                table: "Applications",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
