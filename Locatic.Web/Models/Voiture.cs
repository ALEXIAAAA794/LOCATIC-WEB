using System.ComponentModel.DataAnnotations;

namespace Locatic.Web.Models;

public class Voiture
{
    public int Id { get; set; }

    [Required, MaxLength(20)]
    public string Immatriculation { get; set; } = string.Empty;

    [Range(1900, 2100)]
    public int Annee { get; set; }

    [Range(0, 10000)]
    public decimal TarifJournalier { get; set; }

    [Range(1, 9)]
    public int NombrePlaces { get; set; }

    [MaxLength(50)]
    public string Carburant { get; set; } = string.Empty;

    public int ModeleId { get; set; }
    public Modele Modele { get; set; } = null!;

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
