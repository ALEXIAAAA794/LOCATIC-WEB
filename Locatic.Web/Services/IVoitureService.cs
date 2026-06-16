using Locatic.Web.Models;

namespace Locatic.Web.Services;

public interface IVoitureService
{
    Task<IEnumerable<Voiture>> GetAllAsync();
    Task<Voiture?> GetByIdAsync(int id);
    Task CreateAsync(Voiture voiture);
    Task UpdateAsync(Voiture voiture);
    Task DeleteAsync(int id);
}
