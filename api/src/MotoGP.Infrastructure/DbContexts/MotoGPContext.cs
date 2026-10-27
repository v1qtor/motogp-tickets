using Microsoft.EntityFrameworkCore;
using MotoGP.Domain.Entities;

namespace MotoGP.Infrastructure.DbContexts;

public class MotoGPContext : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Sponsor> Sponsors { get; set; }
    public DbSet<Rider> Riders { get; set; }
    public DbSet<TeamSponsor> TeamSponsors { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    public MotoGPContext(DbContextOptions<MotoGPContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Team>()
            .HasMany(t => t.Riders)
            .WithOne(r => r.Team)
            .HasForeignKey(r => r.TeamId);

        modelBuilder.Entity<Country>()
            .HasMany(c => c.Riders)
            .WithOne(r => r.Country)
            .HasForeignKey(r => r.CountryId);

        modelBuilder.Entity<Team>()
            .HasMany(t => t.TeamSponsors)
            .WithOne(ts => ts.Team)
            .HasForeignKey(ts => ts.TeamId);

        modelBuilder.Entity<Sponsor>()
            .HasMany(s => s.TeamSponsors)
            .WithOne(ts => ts.Sponsor)
            .HasForeignKey(ts => ts.SponsorId);

        modelBuilder.Entity<Race>()
            .HasMany(r => r.Tickets)
            .WithOne(t => t.Race)
            .HasForeignKey(t => t.RaceId);

        modelBuilder.Entity<Country>()
            .HasMany(c => c.Tickets)
            .WithOne(t => t.Country)
            .HasForeignKey(t => t.CountryId);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().HasData(
            new Country("Albania") { Id = 1 },
            new Country("Algeria") { Id = 2 },
            new Country("Andorra") { Id = 3 },
            new Country("Angola") { Id = 4 },
            new Country("Antigua") { Id = 5 },
            new Country("Argentina") { Id = 6 },
            new Country("Armenia") { Id = 7 },
            new Country("Australia") { Id = 8 },
            new Country("Austria") { Id = 9 },
            new Country("Azerbaijan") { Id = 10 },
            new Country("Bahamas") { Id = 11 },
            new Country("Bahrain") { Id = 12 },
            new Country("Bangladesh") { Id = 13 },
            new Country("Barbados") { Id = 14 },
            new Country("Belgium") { Id = 15 },
            new Country("Belize") { Id = 16 },
            new Country("Benin") { Id = 17 },
            new Country("Bhutan") { Id = 18 },
            new Country("Bolivia") { Id = 19 },
            new Country("Botswana") { Id = 21 },
            new Country("Brazil") { Id = 22 },
            new Country("Brunei") { Id = 23 },
            new Country("Bulgaria") { Id = 24 },
            new Country("Burkina") { Id = 25 },
            new Country("Burma") { Id = 26 },
            new Country("Burundi") { Id = 27 },
            new Country("Cambodia") { Id = 28 },
            new Country("Cameroon") { Id = 29 },
            new Country("Canada") { Id = 30 },
            new Country("Chad") { Id = 33 },
            new Country("Chile") { Id = 34 },
            new Country("China") { Id = 35 },
            new Country("Colombia") { Id = 36 },
            new Country("Comoros") { Id = 37 },
            new Country("Congo") { Id = 38 },
            new Country("Costa Rica") { Id = 39 },
            new Country("Cote d'Ivoire") { Id = 40 },
            new Country("Croatia") { Id = 41 },
            new Country("Cuba") { Id = 42 },
            new Country("Cyprus") { Id = 43 },
            new Country("Czech Republic") { Id = 44 },
            new Country("Denmark") { Id = 45 },
            new Country("Djibouti") { Id = 46 },
            new Country("Dominica") { Id = 47 },
            new Country("Ecuador") { Id = 49 },
            new Country("Egypt") { Id = 50 },
            new Country("El Salvador") { Id = 51 },
            new Country("Eritrea") { Id = 53 },
            new Country("Estonia") { Id = 54 },
            new Country("Ethiopia") { Id = 55 },
            new Country("Fiji") { Id = 56 },
            new Country("Finland") { Id = 57 },
            new Country("France") { Id = 58 },
            new Country("Gabon") { Id = 59 },
            new Country("Gambia") { Id = 60 },
            new Country("Georgia") { Id = 61 },
            new Country("Germany") { Id = 62 },
            new Country("Ghana") { Id = 63 },
            new Country("Gibraltar") { Id = 64 },
            new Country("Greece") { Id = 65 },
            new Country("Grenada") { Id = 66 },
            new Country("Guadeloupe") { Id = 67 },
            new Country("Guatemala") { Id = 68 },
            new Country("Guinea") { Id = 69 },
            new Country("Guinea-Bissau") { Id = 70 },
            new Country("Guyana") { Id = 71 },
            new Country("Haiti") { Id = 72 },
            new Country("Honduras") { Id = 73 },
            new Country("Hong Kong") { Id = 74 },
            new Country("Hungary") { Id = 75 },
            new Country("Iceland") { Id = 76 },
            new Country("India") { Id = 77 },
            new Country("Indonesia") { Id = 78 },
            new Country("Iran") { Id = 79 },
            new Country("Iraq") { Id = 80 },
            new Country("Irish Republic") { Id = 81 },
            new Country("Israel") { Id = 82 },
            new Country("Italy") { Id = 83 },
            new Country("Jamaica") { Id = 84 },
            new Country("Japan") { Id = 85 },
            new Country("Jordan") { Id = 86 },
            new Country("Kazakhstan") { Id = 87 },
            new Country("Kenya") { Id = 88 },
            new Country("Kirgizstan") { Id = 89 },
            new Country("Kiribati") { Id = 90 },
            new Country("Korea (North)") { Id = 91 },
            new Country("Korea (South)") { Id = 92 },
            new Country("Kuwait") { Id = 93 },
            new Country("Laos") { Id = 94 },
            new Country("Latvia") { Id = 95 },
            new Country("Lebanon") { Id = 96 },
            new Country("Lesotho") { Id = 97 },
            new Country("Liberia") { Id = 98 },
            new Country("Libya") { Id = 99 },
            new Country("Liechtenstein") { Id = 100 },
            new Country("Lithuania") { Id = 101 },
            new Country("Luxembourg") { Id = 102 },
            new Country("Macedonia") { Id = 103 },
            new Country("Madagascar") { Id = 104 },
            new Country("Malawi") { Id = 105 },
            new Country("Malaysia") { Id = 106 },
            new Country("Maldives") { Id = 107 },
            new Country("Mali") { Id = 108 },
            new Country("Malta") { Id = 109 },
            new Country("Mauritania") { Id = 111 },
            new Country("Mauritius") { Id = 112 },
            new Country("Mexico") { Id = 113 },
            new Country("Micronesia") { Id = 114 },
            new Country("Moldova") { Id = 115 },
            new Country("Monaco") { Id = 116 },
            new Country("Mongolia") { Id = 117 },
            new Country("Morocco") { Id = 118 },
            new Country("Mozambique") { Id = 119 },
            new Country("Namibia") { Id = 120 },
            new Country("Nauru") { Id = 121 },
            new Country("Nepal") { Id = 122 },
            new Country("Netherlands") { Id = 123 },
            new Country("New Zealand") { Id = 124 },
            new Country("Nicaragua") { Id = 125 },
            new Country("Niger") { Id = 126 },
            new Country("Nigeria") { Id = 127 },
            new Country("Norway") { Id = 128 },
            new Country("Oman") { Id = 129 },
            new Country("Pakistan") { Id = 130 },
            new Country("Panama") { Id = 131 },
            new Country("Papua New Guinea") { Id = 132 },
            new Country("Paraguay") { Id = 133 },
            new Country("Peru") { Id = 134 },
            new Country("Philippines") { Id = 135 },
            new Country("Poland") { Id = 136 },
            new Country("Portugal") { Id = 137 },
            new Country("Puerto Rico") { Id = 138 },
            new Country("Qatar") { Id = 139 },
            new Country("Romania") { Id = 140 },
            new Country("Russia") { Id = 141 },
            new Country("Rwanda") { Id = 142 },
            new Country("Saint Kitts") { Id = 143 },
            new Country("Saint Lucia") { Id = 144 },
            new Country("Saint Vincent") { Id = 145 },
            new Country("Samoa") { Id = 146 },
            new Country("San Marino") { Id = 147 },
            new Country("Saudi Arabia") { Id = 149 },
            new Country("Senegal") { Id = 150 },
            new Country("Seychelles") { Id = 151 },
            new Country("Sierra Leone") { Id = 152 },
            new Country("Singapore") { Id = 153 },
            new Country("Slovakia") { Id = 154 },
            new Country("Slovenia") { Id = 155 },
            new Country("Solomon Islands") { Id = 156 },
            new Country("Somalia") { Id = 157 },
            new Country("South Africa") { Id = 158 },
            new Country("Spain") { Id = 159 },
            new Country("Sri Lanka") { Id = 160 },
            new Country("Sudan") { Id = 161 },
            new Country("Suriname") { Id = 162 },
            new Country("Swaziland") { Id = 163 },
            new Country("Sweden") { Id = 164 },
            new Country("Switzerland") { Id = 165 },
            new Country("Syria") { Id = 166 },
            new Country("Tahiti") { Id = 167 },
            new Country("Taiwan") { Id = 168 },
            new Country("Tajikistan") { Id = 169 },
            new Country("Tanzania") { Id = 170 },
            new Country("Thailand") { Id = 171 },
            new Country("Togo") { Id = 172 },
            new Country("Tonga") { Id = 173 },
            new Country("Tunisia") { Id = 175 },
            new Country("Turkey") { Id = 176 },
            new Country("Turkmenistan") { Id = 177 },
            new Country("Tuvalu") { Id = 178 },
            new Country("Uganda") { Id = 179 },
            new Country("Ukraine") { Id = 180 },
            new Country("United Kingdom") { Id = 182 },
            new Country("United States") { Id = 183 },
            new Country("Uruguay") { Id = 184 },
            new Country("Uzbekistan") { Id = 185 },
            new Country("Vanuatu") { Id = 186 },
            new Country("Venezuela") { Id = 187 },
            new Country("Vietnam") { Id = 188 },
            new Country("White Russia") { Id = 189 },
            new Country("Yemen") { Id = 190 },
            new Country("Zaire") { Id = 191 },
            new Country("Zambia") { Id = 192 },
            new Country("Zimbabwe") { Id = 193 }
        );

        modelBuilder.Entity<Team>().HasData(
            new Team("Avintia Racing") { Id = 1, Logo = "Avintia.png" },
            new Team("Cardion AB Motoracing") { Id = 2, Logo = "AB.png" },
            new Team("Ducati Team") { Id = 3, Logo = "Ducati.png" },
            new Team("LCR Honda MotoGP") { Id = 4, Logo = "LCR.png" },
            new Team("Repsol Honda Team") { Id = 5, Logo = "Repsol.png" },
            new Team("Drive M7 Aspar") { Id = 6, Logo = "M7.png" },
            new Team("IodaRacing Project") { Id = 7, Logo = "ioda.png" }
        );

        modelBuilder.Entity<Rider>().HasData(
            new Rider("Barbera", "Hector") { Id = 1, CountryId = 159, TeamId = 1, Number = 8 },
            new Rider("Di Meglio", "Mike") { Id = 2, CountryId = 58, TeamId = 1, Number = 63 },
            new Rider("Abraham", "Karel") { Id = 3, CountryId = 44, TeamId = 2, Number = 17 },
            new Rider("Aoyama", "Hiroshi") { Id = 4, CountryId = 85, TeamId = 6, Number = 7 },
            new Rider("Hayden", "Niki") { Id = 5, CountryId = 183, TeamId = 6, Number = 69 },
            new Rider("Dovizioso", "Andrea") { Id = 6, CountryId = 83, TeamId = 3, Number = 4 },
            new Rider("Crutchlow", "Cal") { Id = 7, CountryId = 182, TeamId = 3, Number = 35 },
            new Rider("Camier", "Leon") { Id = 8, CountryId = 182, TeamId = 7, Number = 2 },
            new Rider("Petrucci", "Danilo") { Id = 9, CountryId = 83, TeamId = 7, Number = 9 },
            new Rider("Bradl", "Stefan") { Id = 10, CountryId = 62, TeamId = 4, Number = 6 },
            new Rider("Pedrosa", "Dani") { Id = 11, CountryId = 159, TeamId = 5, Number = 26 },
            new Rider("Marquez", "Marc") { Id = 12, CountryId = 159, TeamId = 5, Number = 93 }
        );

        modelBuilder.Entity<Sponsor>().HasData(
            new Sponsor("Qatar Airways") { Id = 1, Logo = "qatar.png", OfficialWebsite = "https://www.qatarairways.com/es-es/homepage.html" },
            new Sponsor("BMW") { Id = 2, Logo = "bmw.png", OfficialWebsite = "https://www.bmw-m.com/en/fastlane/motogp.html" },
            new Sponsor("Tissot") { Id = 3, Logo = "tissot.png", OfficialWebsite = "https://www.tissotwatches.com/" },
            new Sponsor("Estrella Galicia") { Id = 4, Logo = "estrella.png", OfficialWebsite = "https://estrellagalicia00.es/" },
            new Sponsor("Michelin") { Id = 5, Logo = "michelin.png", OfficialWebsite = "https://www.michelinmotorsport.com/en/motorsport/" },
            new Sponsor("DHL") { Id = 6, Logo = "dhl.png", OfficialWebsite = "https://www.dhl.com/global-en/home/about-us/partnerships.html" }
        );

        modelBuilder.Entity<TeamSponsor>().HasData(
            new TeamSponsor { Id = 1, TeamId = 1, SponsorId = 2 },
            new TeamSponsor { Id = 2, TeamId = 1, SponsorId = 3 },
            new TeamSponsor { Id = 3, TeamId = 1, SponsorId = 4 },
            new TeamSponsor { Id = 4, TeamId = 1, SponsorId = 5 },
            new TeamSponsor { Id = 5, TeamId = 5, SponsorId = 6 },
            new TeamSponsor { Id = 6, TeamId = 2, SponsorId = 1 },
            new TeamSponsor { Id = 7, TeamId = 2, SponsorId = 3 },
            new TeamSponsor { Id = 8, TeamId = 2, SponsorId = 4 },
            new TeamSponsor { Id = 9, TeamId = 2, SponsorId = 5 },
            new TeamSponsor { Id = 10, TeamId = 6, SponsorId = 6 },
            new TeamSponsor { Id = 11, TeamId = 3, SponsorId = 1 },
            new TeamSponsor { Id = 12, TeamId = 3, SponsorId = 2 },
            new TeamSponsor { Id = 13, TeamId = 3, SponsorId = 4 },
            new TeamSponsor { Id = 14, TeamId = 3, SponsorId = 5 },
            new TeamSponsor { Id = 15, TeamId = 7, SponsorId = 6 },
            new TeamSponsor { Id = 16, TeamId = 4, SponsorId = 1 },
            new TeamSponsor { Id = 17, TeamId = 4, SponsorId = 2 },
            new TeamSponsor { Id = 18, TeamId = 4, SponsorId = 3 },
            new TeamSponsor { Id = 19, TeamId = 7, SponsorId = 5 },
            new TeamSponsor { Id = 20, TeamId = 4, SponsorId = 6 }
        );

        modelBuilder.Entity<Race>().HasData(
            new Race("TT Circuit Assen") { Id = 1, Place = "Assen (Netherlands)", Description = "Assen is the only venue to have held a round of the Motorcycle World Championship every year since its creation in 1949. The circuit was purpose built for the Dutch TT in 1954, with previous events having been held on public roads. The track is narrow, with rapid changes in direction, and is fully surrounded by grass banks and grandstands, providing excellent viewing for the hundreds of thousands of fanatical spectators who are drawn to the most prestigious event in Dutch motorsport every year. A huge favourite with riders, Assen is well known for its festive and extravagant atmosphere.", Length = 4542, Date = new DateTime(DateTime.Now.Year, 6, 28), X = 517, Y = 19 },
            new Race("Losail Circuit") { Id = 2, Place = "Doha (Qatar)", Description = "The fabulous Losail International Circuit lies on the outskirts of Doha, the capital city of Qatar. Built in little over a year, the track cost $58 million USD and required round-the-clock dedication from almost 1,000 workers in order to get it ready for the inaugural event - the Marlboro Grand Prix of Qatar on the 2nd October 2004.", Length = 5380, Date = new DateTime(DateTime.Now.Year, 4, 23), X = 859, Y = 249 },
            new Race("Autódromo Termas de Río Hondo") { Id = 3, Place = "Río Hondo (Argentina)", Description = "One of its more distinctive features is near 41m-elevation change with an impressive incline at the end of the home-straight followed by a sharp left. The circuit is one of the most varied on the GP circuit, with a mix of fast straights and tight hairpins, with most sections mirroring at least some part of a famous track around the world.", Length = 5201, Date = new DateTime(DateTime.Now.Year, 4, 27), X = 194, Y = 428 },
            new Race("Circuito Permanente de Jerez") { Id = 4, Place = "Jerez (Spain)", Description = "Built in 1986 and hosting its first Grand Prix one year later, the circuit of Jerez is now one of the most popular MotoGP venues and the focal point for a city fanatical about sport. Set in a slight valley in the south of Spain, Jerez is blessed with consistently good weather and beautiful scenery, its numerous grandstands providing the perfect viewing facilities for up to 250,000 spectators.", Length = 4423, Date = new DateTime(DateTime.Now.Year, 5, 4), X = 342, Y = 169 },
            new Race("Circuit de la Sarthe") { Id = 5, Place = "Le Mans (France)", Description = "Le Mans is a tight track dominated by first gear corners that place the emphasis on late braking and hard acceleration, whilst rear end traction is also a key area. With the capacity to comfortably accommodate up to 100,000 spectators, the Bugatti circuit also plays host to the 24 hour truck race, the FIA GP2 Championship, French Touring Car and GT races.", Length = 4185, Date = new DateTime(DateTime.Now.Year, 5, 18), X = 564, Y = 161 },
            new Race("Sachsenring") { Id = 6, Place = "Hohenstein-Ernstthal (Germany)", Description = "Car and bike races have been held on closed public roads in the area around the town of Chemnitz, Germany since the 1920s and were still taking place until as recently as 1990. It was decided, however, that the five mile course through such a densely populated area was no longer suitable and a new circuit was built five miles west of Chemnitz in 1996. Although mainly used as a driver training centre and road transport safety station, Sachsenring first hosted MotoGP in 1998 and many improvements have taken place since then, including a drastic layout enhancement in 2001. Numerous tight corners make it one of the slower tracks on the calendar but there is never any shortage of close racing action.", Length = 3671, Date = new DateTime(DateTime.Now.Year, 8, 13), X = 645, Y = 53 },
            new Race("Indianapolis Motor Speedway") { Id = 7, Place = "Indianapolis (United States)", Description = "The first motorsport race which took place at Indy was a motorcycle one on August 14th 1909, on the 2.5 mile oval circuit, and despite its 100-year history it was not until 2008 that MotoGP arrived at the Indianapolis Motor Speedway.", Length = 4216, Date = new DateTime(DateTime.Now.Year, 10, 10), X = 21, Y = 114 },
            new Race("Silverstone Circuit") { Id = 8, Place = "Silverstone (Great Britain)", Description = "With more than 60 years of history Silverstone has become one of the most prestigious venues dedicated to motorsport. Completely revamped in recent years, a multimillion pound investment saw the completion in 2010 of a first phase of works to greatly improve the venue, making it one of the fastest tracks on the MotoGP calendar and earning high praise from the World Championship riders who enjoyed the new layout. The 'Silverstone Wing', a state-of-the-art, multi-million pound complex, became a new landmark at the circuit from 2011, thus adding to already breathtaking facilities at Silverstone.", Length = 5900, Date = new DateTime(DateTime.Now.Year, 8, 15), X = 354, Y = 69 },
            new Race("World Circuit Marco Simoncelli") { Id = 9, Place = "Misano (Italy)", Description = "Close to the city of Rimini, the Misano Adriático circuit was constructed in 1972 and has since undergone an array of modifications. A regular scene for Italian Grands Prix throughout the 80s and early 90s, Misano returned to the MotoGP calendar in 2007.With updated facilities, track and grandstands, the Misano Adriatico holds a maximum capacity of 60,000 spectators. In accordance with MotoGP safety regulations, the 4,200m track runs clockwise for the World Championship races.", Length = 4226, Date = new DateTime(DateTime.Now.Year, 8, 14), X = 502, Y = 337 },
            new Race("Twin Ring") { Id = 10, Place = "Motegi (Japan)", Description = "Located amongst the vast natural beauty of the northern Kanto district, the twin ring circuit at Motegi in Japan consists of a 1.5 mile oval and a 2.9 mile road course constructed to international standards. Built by Honda as the ultimate test facility in August 1997, the road circuit became home to MotoGP in 2000 whilst the oval is designed to introduce American motorsports culture to the country.", Length = 4801, Date = new DateTime(DateTime.Now.Year, 10, 12), X = 921, Y = 132 },
            new Race("Sepang Circuit") { Id = 11, Place = "Sepang (Malaysia)", Description = "Specifically built for speed and exciting racing, the Sepang International Circuit in Malaysia is one of the world's best. The 2,300 acre complex which also houses a hotel, shopping centre, golf course and other sports facilities cost around £50m to construct and was built in just 14 months, holding its first Grand Prix in April 1999 and setting the standard for race circuits worldwide.", Length = 5548, Date = new DateTime(DateTime.Now.Year, 10, 26), X = 700, Y = 275 },
            new Race("Comunitat Valenciana") { Id = 12, Place = "Valencia (Spain)", Description = "The Circuito de la Comunitat Valenciana was completed in 1999 and held rounds of the MotoGP and Spanish Motorcycle Championships in the same year. The Cheste track has several layouts, running anti-clockwise with varying lengths. MotoGP events are held on a 4km track comprising of five right handed corners, eight left handers and a 650m straight. Although the track is regarded as quite small, the pit complex contains 48 garages whilst the stadium style grandstands can seat up to 150,000 spectators. The circuit layout which allows all parts of the circuit to be seen from any stand helps to create a unique atmosphere enjoyed by Spanish and international riders alike and as the last race of the season there is always a party feeling to the Grand Prix, which was voted best GP of 2005 by IRTA.", Length = 4005, Date = new DateTime(DateTime.Now.Year, 6, 9), X = 649, Y = 363 },
            new Race("Circuit of the Americas") { Id = 13, Place = "Austin (USA)", Description = "The Circuit of the Americas in Austin, Texas, is the first purpose-built Grand Prix facility in the USA, with a capacity for 120,000 fans. The track is 5.513 km long and includes 20 turns with an elevation change of 41 meters.", Length = 5513, Date = new DateTime(DateTime.Now.Year, 4, 9), X = 103, Y = 253 },
            new Race("Autódromo Internacional Nelson Piquet") { Id = 14, Place = "Rio de Janeiro (Brazil)", Description = "The Autódromo Internacional Nelson Piquet, also known as Jacarepaguá, was a motorsport circuit in Rio de Janeiro, Brazil. It hosted the Brazilian Grand Prix and was known for its challenging layout and tropical weather conditions.", Length = 5021, Date = new DateTime(DateTime.Now.Year, 5, 21), X = 324, Y = 429 },
            new Race("MotorLand Aragón") { Id = 15, Place = "Aragon (Spain)", Description = "MotorLand Aragón is a motorsport race track located near Alcañiz in Spain. It was designed by Hermann Tilke and the facility includes a technology park, a sports area, and a cultural and leisure area.", Length = 5078, Date = new DateTime(DateTime.Now.Year, 9, 17), X = 402, Y = 134 },
            new Race("Circuit de Barcelona-Catalunya") { Id = 16, Place = "Catalunya (Spain)", Description = "The Circuit de Barcelona-Catalunya is located in Montmeló, Spain. It has hosted the Spanish Grand Prix since 1991 and is known for its combination of high and low-speed corners, making it a favorite among drivers and teams.", Length = 4655, Date = new DateTime(DateTime.Now.Year, 6, 4), X = 378, Y = 239 },
            new Race("Misano World Circuit Marco Simoncelli") { Id = 17, Place = "San Marino (Italy)", Description = "The Misano World Circuit Marco Simoncelli is located next to the town of Misano Adriatico in the Province of Rimini. The circuit was renamed in honor of MotoGP rider Marco Simoncelli, who died in 2011.", Length = 4226, Date = new DateTime(DateTime.Now.Year, 9, 10), X = 426, Y = 304 },
            new Race("Automotodrom Brno") { Id = 18, Place = "Brno (Czech Republic)", Description = "The Automotodrom Brno is a 5.403 km circuit located near Brno, Czech Republic. It has a rich history in motorsports and is known for its elevation changes and challenging layout.", Length = 5403, Date = new DateTime(DateTime.Now.Year, 8, 6), X = 685, Y = 115 },
            new Race("Phillip Island Grand Prix Circuit") { Id = 19, Place = "Phillip Island (Australia)", Description = "The Phillip Island Grand Prix Circuit is located on Phillip Island, Victoria. It is known for its stunning coastal views and high-speed corners, making it a favorite among riders and fans.", Length = 4445, Date = new DateTime(DateTime.Now.Year, 10, 15), X = 736, Y = 403 }
        );

        modelBuilder.Entity<Ticket>().HasData(
            new Ticket("Liam Thompson") { Id = 1, Email = "liam.thompson@example.com", Address = "940 Edinburgh Street, Edinburgh", CountryId = 182, RaceId = 3, Number = 1, OrderDate = new DateTime(DateTime.Now.Year, 3, 11), Paid = false },
            new Ticket("Jasper De Smet") { Id = 2, Email = "jasper.desmet@example.com", Address = "601 Bruges Street, Bruges", CountryId = 15, RaceId = 12, Number = 2, OrderDate = new DateTime(DateTime.Now.Year, 2, 22), Paid = false },
            new Ticket("Oliver Hughes") { Id = 3, Email = "oliver.hughes@example.com", Address = "249 Birmingham Street, Birmingham", CountryId = 182, RaceId = 10, Number = 1, OrderDate = new DateTime(DateTime.Now.Year, 1, 17), Paid = true },
            new Ticket("Sipho Mokoena") { Id = 4, Email = "sipho.mokoena@example.com", Address = "869 Johannesburg Street, Johannesburg", CountryId = 158, RaceId = 9, Number = 2, OrderDate = new DateTime(DateTime.Now.Year, 2, 13), Paid = true },
            new Ticket("Ethan Johnson") { Id = 5, Email = "ethan.johnson@example.com", Address = "535 Houston Street, Houston", CountryId = 183, RaceId = 6, Number = 2, OrderDate = new DateTime(DateTime.Now.Year, 1, 26), Paid = true },
            new Ticket("Erik Larsson") { Id = 6, Email = "erik.larsson@example.com", Address = "677 Stockholm Street, Stockholm", CountryId = 164, RaceId = 15, Number = 1, OrderDate = new DateTime(DateTime.Now.Year - 1, 12, 20), Paid = true },
            new Ticket("George Brown") { Id = 7, Email = "george.brown@example.com", Address = "225 Birmingham Street, Birmingham", CountryId = 182, RaceId = 13, Number = 2, OrderDate = new DateTime(DateTime.Now.Year, 1, 27), Paid = false },
            new Ticket("Thabo Dlamini") { Id = 8, Email = "thabo.dlamini@example.com", Address = "769 Cape Town Street, Cape Town", CountryId = 158, RaceId = 14, Number = 1, OrderDate = new DateTime(DateTime.Now.Year - 1, 9, 1), Paid = true },
            new Ticket("Lotte Vermeulen") { Id = 9, Email = "lotte.vermeulen@example.com", Address = "898 Ghent Street, Ghent", CountryId = 15, RaceId = 6, Number = 4, OrderDate = new DateTime(DateTime.Now.Year - 1, 11, 27), Paid = true },
            new Ticket("Emily Anderson") { Id = 10, Email = "emily.anderson@example.com", Address = "901 Chicago Street, Chicago", CountryId = 183, RaceId = 15, Number = 3, OrderDate = new DateTime(DateTime.Now.Year - 1, 5, 12), Paid = true },
            new Ticket("Mason Clark") { Id = 11, Email = "mason.clark@example.com", Address = "770 Miami Street, Miami", CountryId = 183, RaceId = 14, Number = 3, OrderDate = new DateTime(DateTime.Now.Year, 1, 9), Paid = false },
            new Ticket("Anna Nilsson") { Id = 12, Email = "anna.nilsson@example.com", Address = "199 Stockholm Street, Stockholm", CountryId = 164, RaceId = 5, Number = 3, OrderDate = new DateTime(DateTime.Now.Year - 1, 12, 4), Paid = true },
            new Ticket("Nomsa Khumalo") { Id = 13, Email = "nomsa.khumalo@example.com", Address = "241 Johannesburg Street, Johannesburg", CountryId = 158, RaceId = 11, Number = 2, OrderDate = new DateTime(DateTime.Now.Year, 2, 1), Paid = true },
            new Ticket("Lucas Martinez") { Id = 14, Email = "lucas.martinez@example.com", Address = "712 Houston Street, Houston", CountryId = 183, RaceId = 9, Number = 1, OrderDate = new DateTime(DateTime.Now.Year - 1, 11, 27), Paid = false },
            new Ticket("Bongani Nkosi") { Id = 15, Email = "bongani.nkosi@example.com", Address = "228 Johannesburg Street, Johannesburg", CountryId = 158, RaceId = 5, Number = 5, OrderDate = new DateTime(DateTime.Now.Year, 3, 5), Paid = true },
            new Ticket("Annelies Peeters") { Id = 16, Email = "annelies.peeters@example.com", Address = "320 Port Elizabeth Street, Port Elizabeth", CountryId = 158, RaceId = 11, Number = 1, OrderDate = new DateTime(DateTime.Now.Year - 1, 10, 26), Paid = false },
            new Ticket("Viktor Lindberg") { Id = 17, Email = "viktor.lindberg@example.com", Address = "126 Malmö Street, Malmö", CountryId = 164, RaceId = 16, Number = 3, OrderDate = new DateTime(DateTime.Now.Year - 1, 7, 28), Paid = true },
            new Ticket("Pieter Janssens") { Id = 18, Email = "pieter.janssens@example.com", Address = "212 Port Elizabeth Street, Port Elizabeth", CountryId = 158, RaceId = 6, Number = 5, OrderDate = new DateTime(DateTime.Now.Year, 2, 17), Paid = false },
            new Ticket("Fredrik Andersson") { Id = 19, Email = "fredrik.andersson@example.com", Address = "57 Stockholm Street, Stockholm", CountryId = 164, RaceId = 6, Number = 4, OrderDate = new DateTime(DateTime.Now.Year, 2, 10), Paid = false },
            new Ticket("Jacob Smith") { Id = 20, Email = "jacob.smith@example.com", Address = "569 Durban Street, Durban", CountryId = 158, RaceId = 1, Number = 4, OrderDate = new DateTime(DateTime.Now.Year - 1, 8, 5), Paid = true }
        );
    }
}
