using System.ComponentModel.DataAnnotations;

namespace Locatic.Web.Models;

public class Client
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nom { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Prenom { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Phone, MaxLength(20)]
    public string? Telephone { get; set; }

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
