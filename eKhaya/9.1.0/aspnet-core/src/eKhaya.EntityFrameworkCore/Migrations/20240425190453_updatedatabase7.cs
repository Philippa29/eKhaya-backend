using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eKhaya.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Units_UnitId",
                table: "Amenities");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Units_UnitId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_UnitId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_UnitId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Amenities");

            migrationBuilder.CreateTable(
                name: "UnitsAmenities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AmenityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_UnitsAmenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitsAmenities_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitsAmenities_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitsAmenities_AmenityId",
                table: "UnitsAmenities",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitsAmenities_UnitId",
                table: "UnitsAmenities",
                column: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitsAmenities");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "Amenities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_UnitId",
                table: "Images",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_UnitId",
                table: "Amenities",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Units_UnitId",
                table: "Amenities",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Units_UnitId",
                table: "Images",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
