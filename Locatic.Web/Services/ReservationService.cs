using Microsoft.EntityFrameworkCore;
using Locatic.Web.Data;
using Locatic.Web.Models;

namespace Locatic.Web.Services;

public class ReservationService : IReservationService
{
    private readonly AppDbContext _db;

    public ReservationService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Reservation>> GetAllAsync() =>
        await _db.Reservations
            .Include(r => r.Client)
            .Include(r => r.Voiture).ThenInclude(v => v.Modele).ThenInclude(m => m.Marque)
            .ToListAsync();

    public async Task<Reservation?> GetByIdAsync(int id) =>
        await _db.Reservations
            .Include(r => r.Client)
            .Include(r => r.Voiture).ThenInclude(v => v.Modele).ThenInclude(m => m.Marque)
            .FirstOrDefaultAsync(r => r.Id == id);

    public async Task<(bool success, string? error)> CreateAsync(Reservation reservation)
    {
        // Règle métier 1 : date de fin >= date de début
        if (reservation.DateFin < reservation.DateDebut)
            return (false, "La date de fin ne peut pas être antérieure à la date de début.");

        // Règle métier 2 : pas de chevauchement pour la même voiture
        bool chevauche = await _db.Reservations.AnyAsync(r =>
            r.VoitureId == reservation.VoitureId &&
            r.DateDebut < reservation.DateFin &&
            r.DateFin > reservation.DateDebut);

        if (chevauche)
            return (false, "Cette voiture est déjà réservée sur la période demandée.");

        _db.Reservations.Add(reservation);
        await _db.SaveChangesAsync();
        return (true, null);
    }

    public async Task DeleteAsync(int id)
    {
        var r = await _db.Reservations.FindAsync(id);
        if (r is not null) { _db.Reservations.Remove(r); await _db.SaveChangesAsync(); }
    }
}
