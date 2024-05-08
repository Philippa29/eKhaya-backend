using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eKhaya.Migrations
{
    /// <inheritdoc />
    public partial class maintainecerequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Residents_TenantId",
                table: "Leases");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Units_UnitIDId",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Workers_WorkerId",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRequests_UnitIDId",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRequests_WorkerId",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_Leases_TenantId",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "UnitIDId",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Leases");

            migrationBuilder.AddColumn<int>(
                name: "UnitID",
                table: "MaintenanceRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Tenant",
                table: "Leases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitID",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "Tenant",
                table: "Leases");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitIDId",
                table: "MaintenanceRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkerId",
                table: "MaintenanceRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Leases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_UnitIDId",
                table: "MaintenanceRequests",
                column: "UnitIDId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_WorkerId",
                table: "MaintenanceRequests",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_TenantId",
                table: "Leases",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Residents_TenantId",
                table: "Leases",
                column: "TenantId",
                principalTable: "Residents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Units_UnitIDId",
                table: "MaintenanceRequests",
                column: "UnitIDId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Workers_WorkerId",
                table: "MaintenanceRequests",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }
    }
}
