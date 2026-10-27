using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoGP.Domain.Entities;

public class Sponsor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public string? Logo { get; set; }

    public string? OfficialWebsite { get; set; }

    public ICollection<TeamSponsor> TeamSponsors { get; set; } = [];

    public Sponsor(string name)
    {
        Name = name;
    }
}
