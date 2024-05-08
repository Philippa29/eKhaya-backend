using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eKhaya.Migrations
{
    /// <inheritdoc />
    public partial class amenitiesupdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Declaration",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "TermsandConditions",
                table: "Applications");

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitType",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_PropertyId",
                table: "Applications",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Properties_PropertyId",
                table: "Applications",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Properties_PropertyId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_PropertyId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "UnitType",
                table: "Applications");

            migrationBuilder.AddColumn<string>(
                name: "Declaration",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TermsandConditions",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
