using Locatic.Web.Models;

namespace Locatic.Web.Services;

public interface IModeleService
{
    Task<IEnumerable<Modele>> GetAllAsync();
    Task<Modele?> GetByIdAsync(int id);
    Task CreateAsync(Modele modele);
}
