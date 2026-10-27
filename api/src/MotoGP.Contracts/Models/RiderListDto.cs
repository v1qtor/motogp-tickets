namespace MotoGP.Contracts.Models;

/// <summary>
/// Rider list view (renamed from Chapter 4's RiderDto). Only a list view is needed
/// for riders, per the Chapter 5 exercise brief.
/// </summary>
public class RiderListDto
{
    public int Id { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public int Number { get; set; }

    /// <summary>Name of the team the rider belongs to.</summary>
    public required string TeamName { get; set; }

    /// <summary>The rider's CountryId, used by the front end to render a flag icon.</summary>
    public int FlagNumber { get; set; }
}
