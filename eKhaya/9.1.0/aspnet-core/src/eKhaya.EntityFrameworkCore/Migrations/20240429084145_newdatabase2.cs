using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eKhaya.Migrations
{
    /// <inheritdoc />
    public partial class newdatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationType",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationType",
                table: "Applications");
        }
    }
}
