using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eKhaya.Migrations
{
    /// <inheritdoc />
    public partial class amenitiesupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitsAmenities_Units_UnitId",
                table: "UnitsAmenities");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "UnitsAmenities",
                newName: "PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_UnitsAmenities_UnitId",
                table: "UnitsAmenities",
                newName: "IX_UnitsAmenities_PropertyId");

            migrationBuilder.AddColumn<int>(
                name: "UnitType",
                table: "UnitsAmenities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitsAmenities_Properties_PropertyId",
                table: "UnitsAmenities",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitsAmenities_Properties_PropertyId",
                table: "UnitsAmenities");

            migrationBuilder.DropColumn(
                name: "UnitType",
                table: "UnitsAmenities");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "UnitsAmenities",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_UnitsAmenities_PropertyId",
                table: "UnitsAmenities",
                newName: "IX_UnitsAmenities_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitsAmenities_Units_UnitId",
                table: "UnitsAmenities",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
