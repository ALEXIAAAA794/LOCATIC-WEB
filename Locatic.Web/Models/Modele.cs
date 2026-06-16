using System.ComponentModel.DataAnnotations;

namespace Locatic.Web.Models;

public class Modele
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nom { get; set; } = string.Empty;

    public int MarqueId { get; set; }
    public Marque Marque { get; set; } = null!;

    public ICollection<Voiture> Voitures { get; set; } = new List<Voiture>();
}
