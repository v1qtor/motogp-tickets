using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoGP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Sponsor_OfficialWebsite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfficialWebsite",
                table: "Sponsors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfficialWebsite",
                table: "Sponsors");
        }
    }
}
