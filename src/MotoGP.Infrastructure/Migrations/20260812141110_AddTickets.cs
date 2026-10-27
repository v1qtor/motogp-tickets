using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MotoGP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Address", "CountryId", "Email", "Name", "Number", "OrderDate", "Paid", "RaceId" },
                values: new object[,]
                {
                    { 1, "940 Edinburgh Street, Edinburgh", 182, "liam.thompson@example.com", "Liam Thompson", 1, new DateTime(2026, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3 },
                    { 2, "601 Bruges Street, Bruges", 15, "jasper.desmet@example.com", "Jasper De Smet", 2, new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 12 },
                    { 3, "249 Birmingham Street, Birmingham", 182, "oliver.hughes@example.com", "Oliver Hughes", 1, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 10 },
                    { 4, "869 Johannesburg Street, Johannesburg", 158, "sipho.mokoena@example.com", "Sipho Mokoena", 2, new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 9 },
                    { 5, "535 Houston Street, Houston", 183, "ethan.johnson@example.com", "Ethan Johnson", 2, new DateTime(2026, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 6 },
                    { 6, "677 Stockholm Street, Stockholm", 164, "erik.larsson@example.com", "Erik Larsson", 1, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 15 },
                    { 7, "225 Birmingham Street, Birmingham", 182, "george.brown@example.com", "George Brown", 2, new DateTime(2026, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 13 },
                    { 8, "769 Cape Town Street, Cape Town", 158, "thabo.dlamini@example.com", "Thabo Dlamini", 1, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 14 },
                    { 9, "898 Ghent Street, Ghent", 15, "lotte.vermeulen@example.com", "Lotte Vermeulen", 4, new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 6 },
                    { 10, "901 Chicago Street, Chicago", 183, "emily.anderson@example.com", "Emily Anderson", 3, new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 15 },
                    { 11, "770 Miami Street, Miami", 183, "mason.clark@example.com", "Mason Clark", 3, new DateTime(2026, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 14 },
                    { 12, "199 Stockholm Street, Stockholm", 164, "anna.nilsson@example.com", "Anna Nilsson", 3, new DateTime(2025, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5 },
                    { 13, "241 Johannesburg Street, Johannesburg", 158, "nomsa.khumalo@example.com", "Nomsa Khumalo", 2, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 11 },
                    { 14, "712 Houston Street, Houston", 183, "lucas.martinez@example.com", "Lucas Martinez", 1, new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 9 },
                    { 15, "228 Johannesburg Street, Johannesburg", 158, "bongani.nkosi@example.com", "Bongani Nkosi", 5, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5 },
                    { 16, "320 Port Elizabeth Street, Port Elizabeth", 158, "annelies.peeters@example.com", "Annelies Peeters", 1, new DateTime(2025, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 11 },
                    { 17, "126 Malmö Street, Malmö", 164, "viktor.lindberg@example.com", "Viktor Lindberg", 3, new DateTime(2025, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 16 },
                    { 18, "212 Port Elizabeth Street, Port Elizabeth", 158, "pieter.janssens@example.com", "Pieter Janssens", 5, new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 6 },
                    { 19, "57 Stockholm Street, Stockholm", 164, "fredrik.andersson@example.com", "Fredrik Andersson", 4, new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 6 },
                    { 20, "569 Durban Street, Durban", 158, "jacob.smith@example.com", "Jacob Smith", 4, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CountryId",
                table: "Tickets",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_RaceId",
                table: "Tickets",
                column: "RaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
