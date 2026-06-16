using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Locatic.Web.ViewModels;

public class ReservationCreateViewModel
{
    [Required(ErrorMessage = "Veuillez sélectionner un client.")]
    public int ClientId { get; set; }

    [Required(ErrorMessage = "Veuillez sélectionner une voiture.")]
    public int VoitureId { get; set; }

    [Required(ErrorMessage = "La date de début est obligatoire.")]
    [DataType(DataType.Date)]
    public DateTime DateDebut { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "La date de fin est obligatoire.")]
    [DataType(DataType.Date)]
    public DateTime DateFin { get; set; } = DateTime.Today.AddDays(1);

    public SelectList? Clients { get; set; }
    public SelectList? Voitures { get; set; }
}
