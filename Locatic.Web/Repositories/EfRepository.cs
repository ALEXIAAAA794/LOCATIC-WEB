using Microsoft.EntityFrameworkCore;
using Locatic.Web.Data;

namespace Locatic.Web.Repositories;

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _db;

    public EfRepository(AppDbContext db) => _db = db;

    protected DbSet<T> Entities => _db.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await Entities.AsNoTracking().ToListAsync();

    public async Task<T?> GetByIdAsync(int id) =>
        await Entities.FindAsync(id);

    public async Task AddAsync(T entity)
    {
        await Entities.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Entities.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is not null)
        {
            Entities.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
