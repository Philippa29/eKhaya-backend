using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eKhaya.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Documents_BankStatementId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Documents_Id1",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Documents_PayslipId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_BankStatementId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_Id1",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "BankStatementId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Id1",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "PayslipId",
                table: "Applications",
                newName: "ApplicantId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Applications",
                newName: "TermsandConditions");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_PayslipId",
                table: "Applications",
                newName: "IX_Applications_ApplicantId");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerIDId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyAddress",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyContactNumber",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComunityType",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Applications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Declaration",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Evicited",
                table: "Applications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Insolvent",
                table: "Applications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaritalStatus",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthsWorked",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Salary",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantID",
                table: "Applicants",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Suburb = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Town = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    POBox = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_OwnerIDId",
                table: "Documents",
                column: "OwnerIDId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Applicants_ApplicantId",
                table: "Applications",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Applications_OwnerIDId",
                table: "Documents",
                column: "OwnerIDId",
                principalTable: "Applications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Applicants_ApplicantId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Applications_OwnerIDId",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Documents_OwnerIDId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "OwnerIDId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "CompanyAddress",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CompanyContactNumber",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ComunityType",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Declaration",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Evicited",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Insolvent",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "MonthsWorked",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "TermsandConditions",
                table: "Applications",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "Applications",
                newName: "PayslipId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_ApplicantId",
                table: "Applications",
                newName: "IX_Applications_PayslipId");

            migrationBuilder.AddColumn<Guid>(
                name: "BankStatementId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id1",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantID",
                table: "Applicants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_BankStatementId",
                table: "Applications",
                column: "BankStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_Id1",
                table: "Applications",
                column: "Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Documents_BankStatementId",
                table: "Applications",
                column: "BankStatementId",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Documents_Id1",
                table: "Applications",
                column: "Id1",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Documents_PayslipId",
                table: "Applications",
                column: "PayslipId",
                principalTable: "Documents",
                principalColumn: "Id");
        }
    }
}
