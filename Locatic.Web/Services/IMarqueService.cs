using Locatic.Web.Models;

namespace Locatic.Web.Services;

public interface IMarqueService
{
    Task<IEnumerable<Marque>> GetAllAsync();
    Task<Marque?> GetByIdAsync(int id);
    Task CreateAsync(Marque marque);
}
