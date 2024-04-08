using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCMonitoring.Data.Migrations
{
    /// <inheritdoc />
    public partial class MVCMonitoring2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DroughtLevel",
                table: "Measurements");

            migrationBuilder.RenameColumn(
                name: "FloodLevel",
                table: "Measurements",
                newName: "WaterLevel");

            migrationBuilder.AddColumn<int>(
                name: "DroughtLevel",
                table: "Stations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FloodLevel",
                table: "Stations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DroughtLevel",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "FloodLevel",
                table: "Stations");

            migrationBuilder.RenameColumn(
                name: "WaterLevel",
                table: "Measurements",
                newName: "FloodLevel");

            migrationBuilder.AddColumn<int>(
                name: "DroughtLevel",
                table: "Measurements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
