using Locatic.Web.Models;

namespace Locatic.Web.Services;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task<(bool success, string? error)> CreateAsync(Reservation reservation);
    Task DeleteAsync(int id);
}
