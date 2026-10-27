using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoGP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRaceData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Populate LapCount/AvgSpeed/LapTime with plausible random estimations for every
            // existing race row (ch.8 exercise brief: "UpdateRaceData" migration). Uses SQL
            // Server's NEWID()/CHECKSUM() trick to get a different pseudo-random value per row.
            migrationBuilder.Sql(@"
                UPDATE [Races]
                SET
                    [LapCount] = 18 + ABS(CHECKSUM(NEWID())) % 13,               -- 18-30 laps
                    [AvgSpeed] = 150.0 + (ABS(CHECKSUM(NEWID())) % 250) / 10.0,  -- 150.0-174.9 km/h
                    [LapTime]  = 95.0 + (ABS(CHECKSUM(NEWID())) % 250) / 10.0;   -- 95.0-119.9 sec
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE [Races]
                SET [LapCount] = 0, [AvgSpeed] = 0.0, [LapTime] = 0.0;
            ");
        }
    }
}
