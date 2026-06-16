using Microsoft.EntityFrameworkCore;
using Locatic.Web.Data;
using Locatic.Web.Models;

namespace Locatic.Web.Services;

public class VoitureService : IVoitureService
{
    private readonly AppDbContext _db;

    public VoitureService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Voiture>> GetAllAsync() =>
        await _db.Voitures
            .Include(v => v.Modele).ThenInclude(m => m.Marque)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Voiture?> GetByIdAsync(int id) =>
        await _db.Voitures
            .Include(v => v.Modele).ThenInclude(m => m.Marque)
            .FirstOrDefaultAsync(v => v.Id == id);

    public async Task CreateAsync(Voiture voiture)
    {
        _db.Voitures.Add(voiture);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Voiture voiture)
    {
        _db.Voitures.Update(voiture);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var v = await _db.Voitures.FindAsync(id);
        if (v is not null)
        {
            _db.Voitures.Remove(v);
            await _db.SaveChangesAsync();
        }
    }
}