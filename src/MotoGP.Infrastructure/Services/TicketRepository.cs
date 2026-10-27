using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MotoGP.Domain.Common;
using MotoGP.Domain.Entities;
using MotoGP.Infrastructure.DbContexts;

namespace MotoGP.Infrastructure.Services;

public class TicketRepository : ITicketRepository
{
    /// <summary>Maximum pageSize a caller may request, to protect the API from unbounded queries.</summary>
    private const int MaxPageSize = 50;

    private readonly MotoGPContext _context;
    private readonly ILogger<TicketRepository> _logger;

    public TicketRepository(MotoGPContext context, ILogger<TicketRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<(IEnumerable<Ticket> Tickets, PaginationMetadata PaginationMetadata)> GetTicketsAsync(
        string? searchQuery,
        int? countryId,
        int? raceId,
        bool open = false,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (pageNumber < 1)
        {
            pageNumber = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 10;
        }
        else if (pageSize > MaxPageSize)
        {
            _logger.LogWarning("Requested pageSize {RequestedPageSize} exceeds the maximum; capping at {MaxPageSize}.", pageSize, MaxPageSize);
            pageSize = MaxPageSize;
        }

        var collection = _context.Tickets.Include(t => t.Race).AsQueryable();

        if (countryId.HasValue)
        {
            collection = collection.Where(t => t.CountryId == countryId.Value);
        }

        if (raceId.HasValue)
        {
            collection = collection.Where(t => t.RaceId == raceId.Value);
        }

        if (open)
        {
            collection = collection.Where(t => !t.Paid);
        }

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            searchQuery = searchQuery.Trim();
            collection = collection.Where(t =>
                t.Name.Contains(searchQuery) ||
                (t.Email != null && t.Email.Contains(searchQuery)));
        }

        var totalItemCount = await collection.CountAsync();
        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var collectionToReturn = await collection
            .OrderBy(t => t.OrderDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (collectionToReturn, paginationMetadata);
    }

    public async Task<Ticket?> GetTicketAsync(int ticketId)
    {
        return await _context.Tickets
            .Include(t => t.Race)
            .FirstOrDefaultAsync(t => t.Id == ticketId);
    }

    public void AddTicket(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) >= 0;
    }

    public async Task LoadRaceForTicketAsync(Ticket ticket)
    {
        await _context.Entry(ticket).Reference(t => t.Race).LoadAsync();
    }
}
