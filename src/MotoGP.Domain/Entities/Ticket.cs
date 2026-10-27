using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoGP.Domain.Entities;

public class Ticket
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Address { get; set; }

    [ForeignKey(nameof(Country))]
    public int CountryId { get; set; }

    public Country Country { get; set; } = null!;

    [ForeignKey(nameof(Race))]
    public int RaceId { get; set; }

    public Race Race { get; set; } = null!;

    public int Number { get; set; }

    public DateTime OrderDate { get; set; }

    public bool Paid { get; set; }

    public Ticket(string name)
    {
        Name = name;
        Email = string.Empty;
        Address = string.Empty;
    }
}
