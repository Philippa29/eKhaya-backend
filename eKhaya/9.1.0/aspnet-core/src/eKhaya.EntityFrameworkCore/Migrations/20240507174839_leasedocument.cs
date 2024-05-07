using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eKhaya.Migrations
{
    /// <inheritdoc />
    public partial class leasedocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Units_OwnerIDId",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "DepositPaid",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "DocumentName",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "RentAmount",
                table: "Leases");

            migrationBuilder.RenameColumn(
                name: "OwnerIDId",
                table: "Leases",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Leases_OwnerIDId",
                table: "Leases",
                newName: "IX_Leases_UnitId");

            migrationBuilder.AddColumn<Guid>(
                name: "AgentId",
                table: "Leases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LeaseDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerIDId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaseDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaseDocuments_Leases_OwnerIDId",
                        column: x => x.OwnerIDId,
                        principalTable: "Leases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leases_AgentId",
                table: "Leases",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseDocuments_OwnerIDId",
                table: "LeaseDocuments",
                column: "OwnerIDId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Agents_AgentId",
                table: "Leases",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Units_UnitId",
                table: "Leases",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Agents_AgentId",
                table: "Leases");

            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Units_UnitId",
                table: "Leases");

            migrationBuilder.DropTable(
                name: "LeaseDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Leases_AgentId",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Leases");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "Leases",
                newName: "OwnerIDId");

            migrationBuilder.RenameIndex(
                name: "IX_Leases_UnitId",
                table: "Leases",
                newName: "IX_Leases_OwnerIDId");

            migrationBuilder.AddColumn<bool>(
                name: "DepositPaid",
                table: "Leases",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AddColumn<decimal>(
                name: "RentAmount",
                table: "Leases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Units_OwnerIDId",
                table: "Leases",
                column: "OwnerIDId",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
