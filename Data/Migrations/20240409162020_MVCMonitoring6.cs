using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCMonitoring.Data.Migrations
{
    /// <inheritdoc />
    public partial class MVCMonitoring6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Stations_MonitoringStationId",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_MonitoringStationId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MonitoringStationId",
                table: "Measurements");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_StationId",
                table: "Measurements",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Stations_StationId",
                table: "Measurements",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Stations_StationId",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_StationId",
                table: "Measurements");

            migrationBuilder.AddColumn<int>(
                name: "MonitoringStationId",
                table: "Measurements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_MonitoringStationId",
                table: "Measurements",
                column: "MonitoringStationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Stations_MonitoringStationId",
                table: "Measurements",
                column: "MonitoringStationId",
                principalTable: "Stations",
                principalColumn: "Id");
        }
    }
}
