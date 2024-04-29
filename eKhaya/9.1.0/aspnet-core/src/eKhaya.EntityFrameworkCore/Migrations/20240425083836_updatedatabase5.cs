using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eKhaya.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Properties_PropertyId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Properties_PropertyId",
                table: "Amenities");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_PropertyId",
                table: "Amenities");

            migrationBuilder.DropIndex(
                name: "IX_Agents_PropertyId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Agents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId",
                table: "Amenities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId",
                table: "Agents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_PropertyId",
                table: "Amenities",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_PropertyId",
                table: "Agents",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Properties_PropertyId",
                table: "Agents",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Properties_PropertyId",
                table: "Amenities",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");
        }
    }
}
