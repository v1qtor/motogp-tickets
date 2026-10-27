namespace MotoGP.Contracts.Models;

public class TicketListDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Number { get; set; }
    public bool Paid { get; set; }

    /// <summary>OrderDate formatted as "dd/MM".</summary>
    public required string ShortOrderDate { get; set; }

    public required string RaceName { get; set; }
}
