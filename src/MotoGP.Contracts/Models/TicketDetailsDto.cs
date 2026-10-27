namespace MotoGP.Contracts.Models;

public class TicketDetailsDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Address { get; set; }
    public int CountryId { get; set; }
    public int RaceId { get; set; }

    /// <summary>Name of the race the tickets are for (explicitly loaded from the Race navigation property).</summary>
    public required string RaceName { get; set; }

    /// <summary>Race date formatted as "dd/MM".</summary>
    public required string ShortRaceDate { get; set; }

    public int Number { get; set; }
    public DateTime OrderDate { get; set; }
    public bool Paid { get; set; }
}
