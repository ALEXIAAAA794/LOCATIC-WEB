using Microsoft.EntityFrameworkCore;
using Locatic.Web.Data;
using Locatic.Web.Models;

namespace Locatic.Web.Services;

public class MarqueService : IMarqueService
{
    private readonly AppDbContext _db;

    public MarqueService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Marque>> GetAllAsync() =>
        await _db.Marques
            .AsNoTracking()
            .OrderBy(m => m.Nom)
            .ToListAsync();

    public async Task<Marque?> GetByIdAsync(int id) =>
        await _db.Marques.FindAsync(id);

    public async Task CreateAsync(Marque marque)
    {
        _db.Marques.Add(marque);
        await _db.SaveChangesAsync();
    }
}
