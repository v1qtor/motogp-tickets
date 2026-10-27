namespace MotoGP.Contracts.Models;

public class SponsorDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Logo { get; set; }
    public string? OfficialWebsite { get; set; }
}
