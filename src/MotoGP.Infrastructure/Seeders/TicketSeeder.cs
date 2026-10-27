using Bogus;
using Microsoft.EntityFrameworkCore;
using MotoGP.Domain.Entities;
using MotoGP.Infrastructure.DbContexts;

namespace MotoGP.Infrastructure.Seeders;

/// <summary>Generates fake but structured ticket data (via Bogus) so filtering/searching/paging have a meaningful dataset to work against.</summary>
public class TicketSeeder
{
    private readonly MotoGPContext _context;

    public TicketSeeder(MotoGPContext context) => _context = context;

    public async Task<List<Ticket>> GenerateTicketsAsync(int count)
    {
        var countryIds = new List<int> { 15, 182, 158, 183, 164, 113, 77, 123, 128, 83, 85 };
        var raceIds = await _context.Races.Select(r => r.Id).ToListAsync();

        var ticketFaker = new Faker<Ticket>()
            .CustomInstantiator(f => new Ticket(f.Name.FirstName() + " " + f.Name.LastName()))
            .RuleFor(t => t.Email, (f, _) => f.Internet.Email())
            .RuleFor(t => t.Address, f => $"{f.Address.StreetAddress()}, {f.Address.City()}")
            .RuleFor(t => t.CountryId, f => f.PickRandom(countryIds))
            .RuleFor(t => t.RaceId, f => f.PickRandom(raceIds))
            .RuleFor(t => t.Number, f => f.Random.Int(1, 6))
            .RuleFor(t => t.OrderDate, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
            .RuleFor(t => t.Paid, f => f.Random.Bool());

        return ticketFaker.Generate(count);
    }
}
