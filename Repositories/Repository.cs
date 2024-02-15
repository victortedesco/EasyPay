using EasyPay.Data;
using EasyPay.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyPay.Repositories;

public abstract class Repository<T, R>(DataContext dataContext, DbSet<T> entities) where T : class, IEntity
{
    public async Task<T?> GetById(Guid id) => await entities.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    public async Task<List<T>> GetAll() => await entities.ToListAsync();

    public async Task<bool> Add(T entity)
    {
        entities.Add(entity);
        return await SaveAllChanges();
    }

    public async Task<bool> Remove(Guid id)
    {
        var entityToRemove = await GetById(id);
        if (entityToRemove == null) return false;

        entities.Remove(entityToRemove);
        return await SaveAllChanges();
    }

    public async Task<bool> Update(T entity)
    {
        entities.Update(entity);
        return await SaveAllChanges();
    }

    public async Task<bool> SaveAllChanges() => await dataContext.SaveChangesAsync() > 0;
}