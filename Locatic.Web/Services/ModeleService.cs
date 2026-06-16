using Microsoft.EntityFrameworkCore;
using Locatic.Web.Data;
using Locatic.Web.Models;

namespace Locatic.Web.Services;

public class ModeleService : IModeleService
{
    private readonly AppDbContext _db;

    public ModeleService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Modele>> GetAllAsync() =>
        await _db.Modeles
            .Include(m => m.Marque)
            .AsNoTracking()
            .OrderBy(m => m.Nom)
            .ToListAsync();

    public async Task<Modele?> GetByIdAsync(int id) =>
        await _db.Modeles
            .Include(m => m.Marque)
            .FirstOrDefaultAsync(m => m.Id == id);

    public async Task CreateAsync(Modele modele)
    {
        _db.Modeles.Add(modele);
        await _db.SaveChangesAsync();
    }
}
