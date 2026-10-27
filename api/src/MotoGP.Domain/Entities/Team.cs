using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoGP.Domain.Entities;

public class Team
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public string? Logo { get; set; }

    public ICollection<Rider> Riders { get; set; } = [];

    public ICollection<TeamSponsor> TeamSponsors { get; set; } = [];

    public Team(string name)
    {
        Name = name;
    }
}
