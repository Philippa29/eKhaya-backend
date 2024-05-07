using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eKhaya.Migrations
{
    /// <inheritdoc />
    public partial class newtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Units_UnitId",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Leases");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "Leases",
                newName: "OwnerIDId");

            migrationBuilder.RenameIndex(
                name: "IX_Leases_UnitId",
                table: "Leases",
                newName: "IX_Leases_OwnerIDId");

            migrationBuilder.AddColumn<string>(
                name: "DocumentName",
                table: "Leases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Leases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Units_OwnerIDId",
                table: "Leases",
                column: "OwnerIDId",
                principalTable: "Units",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Units_OwnerIDId",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "DocumentName",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Leases");

            migrationBuilder.RenameColumn(
                name: "OwnerIDId",
                table: "Leases",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Leases_OwnerIDId",
                table: "Leases",
                newName: "IX_Leases_UnitId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Leases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Leases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Units_UnitId",
                table: "Leases",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
