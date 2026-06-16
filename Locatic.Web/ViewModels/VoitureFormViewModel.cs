using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Locatic.Web.ViewModels;

public class VoitureFormViewModel
{
    public int Id { get; set; }

    [Required, MaxLength(20)]
    public string Immatriculation { get; set; } = string.Empty;

    [Range(1900, 2100)]
    public int Annee { get; set; } = DateTime.Today.Year;

    [Range(0, 10000)]
    public decimal TarifJournalier { get; set; }

    [Range(1, 9)]
    public int NombrePlaces { get; set; } = 5;

    [MaxLength(50)]
    public string Carburant { get; set; } = string.Empty;

    [Required]
    public int ModeleId { get; set; }

    public SelectList? Modeles { get; set; }
}
