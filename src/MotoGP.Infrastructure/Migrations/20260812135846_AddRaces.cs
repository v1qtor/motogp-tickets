using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MotoGP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "Id", "Date", "Description", "Length", "Name", "Place", "X", "Y" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Assen is the only venue to have held a round of the Motorcycle World Championship every year since its creation in 1949. The circuit was purpose built for the Dutch TT in 1954, with previous events having been held on public roads. The track is narrow, with rapid changes in direction, and is fully surrounded by grass banks and grandstands, providing excellent viewing for the hundreds of thousands of fanatical spectators who are drawn to the most prestigious event in Dutch motorsport every year. A huge favourite with riders, Assen is well known for its festive and extravagant atmosphere.", 4542, "TT Circuit Assen", "Assen (Netherlands)", 517, 19 },
                    { 2, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "The fabulous Losail International Circuit lies on the outskirts of Doha, the capital city of Qatar. Built in little over a year, the track cost $58 million USD and required round-the-clock dedication from almost 1,000 workers in order to get it ready for the inaugural event - the Marlboro Grand Prix of Qatar on the 2nd October 2004.", 5380, "Losail Circuit", "Doha (Qatar)", 859, 249 },
                    { 3, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "One of its more distinctive features is near 41m-elevation change with an impressive incline at the end of the home-straight followed by a sharp left. The circuit is one of the most varied on the GP circuit, with a mix of fast straights and tight hairpins, with most sections mirroring at least some part of a famous track around the world.", 5201, "Autódromo Termas de Río Hondo", "Río Hondo (Argentina)", 194, 428 },
                    { 4, new DateTime(2026, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Built in 1986 and hosting its first Grand Prix one year later, the circuit of Jerez is now one of the most popular MotoGP venues and the focal point for a city fanatical about sport. Set in a slight valley in the south of Spain, Jerez is blessed with consistently good weather and beautiful scenery, its numerous grandstands providing the perfect viewing facilities for up to 250,000 spectators.", 4423, "Circuito Permanente de Jerez", "Jerez (Spain)", 342, 169 },
                    { 5, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Le Mans is a tight track dominated by first gear corners that place the emphasis on late braking and hard acceleration, whilst rear end traction is also a key area. With the capacity to comfortably accommodate up to 100,000 spectators, the Bugatti circuit also plays host to the 24 hour truck race, the FIA GP2 Championship, French Touring Car and GT races.", 4185, "Circuit de la Sarthe", "Le Mans (France)", 564, 161 },
                    { 6, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Car and bike races have been held on closed public roads in the area around the town of Chemnitz, Germany since the 1920s and were still taking place until as recently as 1990. It was decided, however, that the five mile course through such a densely populated area was no longer suitable and a new circuit was built five miles west of Chemnitz in 1996. Although mainly used as a driver training centre and road transport safety station, Sachsenring first hosted MotoGP in 1998 and many improvements have taken place since then, including a drastic layout enhancement in 2001. Numerous tight corners make it one of the slower tracks on the calendar but there is never any shortage of close racing action.", 3671, "Sachsenring", "Hohenstein-Ernstthal (Germany)", 645, 53 },
                    { 7, new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "The first motorsport race which took place at Indy was a motorcycle one on August 14th 1909, on the 2.5 mile oval circuit, and despite its 100-year history it was not until 2008 that MotoGP arrived at the Indianapolis Motor Speedway.", 4216, "Indianapolis Motor Speedway", "Indianapolis (United States)", 21, 114 },
                    { 8, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "With more than 60 years of history Silverstone has become one of the most prestigious venues dedicated to motorsport. Completely revamped in recent years, a multimillion pound investment saw the completion in 2010 of a first phase of works to greatly improve the venue, making it one of the fastest tracks on the MotoGP calendar and earning high praise from the World Championship riders who enjoyed the new layout. The 'Silverstone Wing', a state-of-the-art, multi-million pound complex, became a new landmark at the circuit from 2011, thus adding to already breathtaking facilities at Silverstone.", 5900, "Silverstone Circuit", "Silverstone (Great Britain)", 354, 69 },
                    { 9, new DateTime(2026, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Close to the city of Rimini, the Misano Adriático circuit was constructed in 1972 and has since undergone an array of modifications. A regular scene for Italian Grands Prix throughout the 80s and early 90s, Misano returned to the MotoGP calendar in 2007.With updated facilities, track and grandstands, the Misano Adriatico holds a maximum capacity of 60,000 spectators. In accordance with MotoGP safety regulations, the 4,200m track runs clockwise for the World Championship races.", 4226, "World Circuit Marco Simoncelli", "Misano (Italy)", 502, 337 },
                    { 10, new DateTime(2026, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Located amongst the vast natural beauty of the northern Kanto district, the twin ring circuit at Motegi in Japan consists of a 1.5 mile oval and a 2.9 mile road course constructed to international standards. Built by Honda as the ultimate test facility in August 1997, the road circuit became home to MotoGP in 2000 whilst the oval is designed to introduce American motorsports culture to the country.", 4801, "Twin Ring", "Motegi (Japan)", 921, 132 },
                    { 11, new DateTime(2026, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Specifically built for speed and exciting racing, the Sepang International Circuit in Malaysia is one of the world's best. The 2,300 acre complex which also houses a hotel, shopping centre, golf course and other sports facilities cost around £50m to construct and was built in just 14 months, holding its first Grand Prix in April 1999 and setting the standard for race circuits worldwide.", 5548, "Sepang Circuit", "Sepang (Malaysia)", 700, 275 },
                    { 12, new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Circuito de la Comunitat Valenciana was completed in 1999 and held rounds of the MotoGP and Spanish Motorcycle Championships in the same year. The Cheste track has several layouts, running anti-clockwise with varying lengths. MotoGP events are held on a 4km track comprising of five right handed corners, eight left handers and a 650m straight. Although the track is regarded as quite small, the pit complex contains 48 garages whilst the stadium style grandstands can seat up to 150,000 spectators. The circuit layout which allows all parts of the circuit to be seen from any stand helps to create a unique atmosphere enjoyed by Spanish and international riders alike and as the last race of the season there is always a party feeling to the Grand Prix, which was voted best GP of 2005 by IRTA.", 4005, "Comunitat Valenciana", "Valencia (Spain)", 649, 363 },
                    { 13, new DateTime(2026, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Circuit of the Americas in Austin, Texas, is the first purpose-built Grand Prix facility in the USA, with a capacity for 120,000 fans. The track is 5.513 km long and includes 20 turns with an elevation change of 41 meters.", 5513, "Circuit of the Americas", "Austin (USA)", 103, 253 },
                    { 14, new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Autódromo Internacional Nelson Piquet, also known as Jacarepaguá, was a motorsport circuit in Rio de Janeiro, Brazil. It hosted the Brazilian Grand Prix and was known for its challenging layout and tropical weather conditions.", 5021, "Autódromo Internacional Nelson Piquet", "Rio de Janeiro (Brazil)", 324, 429 },
                    { 15, new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "MotorLand Aragón is a motorsport race track located near Alcañiz in Spain. It was designed by Hermann Tilke and the facility includes a technology park, a sports area, and a cultural and leisure area.", 5078, "MotorLand Aragón", "Aragon (Spain)", 402, 134 },
                    { 16, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Circuit de Barcelona-Catalunya is located in Montmeló, Spain. It has hosted the Spanish Grand Prix since 1991 and is known for its combination of high and low-speed corners, making it a favorite among drivers and teams.", 4655, "Circuit de Barcelona-Catalunya", "Catalunya (Spain)", 378, 239 },
                    { 17, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Misano World Circuit Marco Simoncelli is located next to the town of Misano Adriatico in the Province of Rimini. The circuit was renamed in honor of MotoGP rider Marco Simoncelli, who died in 2011.", 4226, "Misano World Circuit Marco Simoncelli", "San Marino (Italy)", 426, 304 },
                    { 18, new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Automotodrom Brno is a 5.403 km circuit located near Brno, Czech Republic. It has a rich history in motorsports and is known for its elevation changes and challenging layout.", 5403, "Automotodrom Brno", "Brno (Czech Republic)", 685, 115 },
                    { 19, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Phillip Island Grand Prix Circuit is located on Phillip Island, Victoria. It is known for its stunning coastal views and high-speed corners, making it a favorite among riders and fans.", 4445, "Phillip Island Grand Prix Circuit", "Phillip Island (Australia)", 736, 403 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Races");
        }
    }
}
