using System.ComponentModel.DataAnnotations;

namespace Locatic.Web.Models;

public class Marque
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nom { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? PaysOrigine { get; set; }

    public ICollection<Modele> Modeles { get; set; } = new List<Modele>();
}
