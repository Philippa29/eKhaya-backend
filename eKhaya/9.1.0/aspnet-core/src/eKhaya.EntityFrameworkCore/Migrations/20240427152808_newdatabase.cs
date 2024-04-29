using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eKhaya.Migrations
{
    /// <inheritdoc />
    public partial class newdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Properties_OwnerIDId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_OwnerIDId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "OwnerIDId",
                table: "Images");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerID",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Images");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerIDId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_OwnerIDId",
                table: "Images",
                column: "OwnerIDId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Properties_OwnerIDId",
                table: "Images",
                column: "OwnerIDId",
                principalTable: "Properties",
                principalColumn: "Id");
        }
    }
}
