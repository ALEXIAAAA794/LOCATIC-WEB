using System.ComponentModel.DataAnnotations;

namespace Locatic.Web.Models;

public class Reservation
{
    public int Id { get; set; }

    [Required]
    public DateTime DateDebut { get; set; }

    [Required]
    public DateTime DateFin { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public int VoitureId { get; set; }
    public Voiture Voiture { get; set; } = null!;

    public decimal MontantTotal => Voiture is not null
        ? (decimal)(DateFin - DateDebut).TotalDays * Voiture.TarifJournalier
        : 0;
}
