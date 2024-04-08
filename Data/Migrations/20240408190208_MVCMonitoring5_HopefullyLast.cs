using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCMonitoring.Data.Migrations
{
    /// <inheritdoc />
    public partial class MVCMonitoring5_HopefullyLast : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeOutInMinutes",
                table: "Measurements");

            migrationBuilder.AddColumn<int>(
                name: "TimeOutInMinutes",
                table: "Stations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeOutInMinutes",
                table: "Stations");

            migrationBuilder.AddColumn<int>(
                name: "TimeOutInMinutes",
                table: "Measurements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
