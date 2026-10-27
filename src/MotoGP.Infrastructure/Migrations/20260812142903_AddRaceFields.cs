using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoGP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRaceFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AvgSpeed",
                table: "Races",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "LapCount",
                table: "Races",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "LapTime",
                table: "Races",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AvgSpeed", "LapCount", "LapTime" },
                values: new object[] { 0.0, 0, 0.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgSpeed",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "LapCount",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "LapTime",
                table: "Races");
        }
    }
}
