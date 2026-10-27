using MotoGP.Domain.Common;
using MotoGP.Domain.Entities;

namespace MotoGP.Infrastructure.Services;

public interface ITicketRepository
{
    /// <summary>
    /// Returns a filtered, paged collection of tickets (sorted by OrderDate, including the Race),
    /// plus paging metadata.
    /// </summary>
    /// <param name="searchQuery">Matches buyer Name or Email (case-insensitive, partial match).</param>
    /// <param name="countryId">When set, only tickets for this country.</param>
    /// <param name="raceId">When set, only tickets for this race.</param>
    /// <param name="open">When true, only unpaid tickets are returned; when false, all tickets.</param>
    Task<(IEnumerable<Ticket> Tickets, PaginationMetadata PaginationMetadata)> GetTicketsAsync(
        string? searchQuery,
        int? countryId,
        int? raceId,
        bool open = false,
        int pageNumber = 1,
        int pageSize = 10);

    Task<Ticket?> GetTicketAsync(int ticketId);

    /// <summary>Adds a ticket to the context. Not async - Add() is not an I/O operation.</summary>
    void AddTicket(Ticket ticket);

    Task<bool> SaveChangesAsync();

    /// <summary>Explicitly (re)loads the Race navigation property for a ticket after insertion.</summary>
    Task LoadRaceForTicketAsync(Ticket ticket);
}
