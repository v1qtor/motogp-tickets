using Microsoft.AspNetCore.Mvc;
using MotoGP.Infrastructure.Seeders;
using MotoGP.Infrastructure.DbContexts;

namespace MotoGP.Api.Controllers;

/// <summary>
/// Development-only helper endpoint to seed a large, realistic ticket dataset so the
/// filtering/searching/paging feature (ch.10) has something meaningful to work against.
/// Talks to MotoGPContext directly (not through a repository) - this is deliberately a
/// one-off data-generation utility, not part of the application's normal data access path.
/// </summary>
[Route("api/seed")]
[ApiController]
public class SeedController : ControllerBase
{
    private readonly MotoGPContext _context;
    private readonly ILogger<SeedController> _logger;

    public SeedController(MotoGPContext context, ILogger<SeedController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost("tickets")]
    public async Task<IActionResult> SeedTickets()
    {
        var seeder = new TicketSeeder(_context);
        var tickets = await seeder.GenerateTicketsAsync(1000);

        _context.Tickets.AddRange(tickets);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Seeded {TicketCount} fake tickets.", tickets.Count);

        return Ok(new { message = "Tickets seeded successfully!", count = tickets.Count });
    }
}
