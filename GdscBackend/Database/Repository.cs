using System.Diagnostics.CodeAnalysis;
using GdscBackend.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace GdscBackend.Database;

public class Repository<T> : IRepository<T> where T : class, IModel
{
    private readonly AppDbContext _context;
    private DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    private Task<int> Save => _context.SaveChangesAsync();

    public DbSet<T> DbSet => _dbSet ??= _context.Set<T>();

    public async Task<T> AddAsync([NotNull] T entity)
    {
        entity.Id = Guid.NewGuid().ToString();
        entity.Created = entity.Updated = DateTime.UtcNow;

        entity = (await DbSet.AddAsync(entity)).Entity;
        await Save;

        return entity;
    }

    public async Task<IEnumerable<T>> GetAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<T?> GetAsync(string id)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<T> AddOrUpdateAsync([NotNull] T entity)
    {
        if (entity is null) return null;

        var existing = await DbSet.FirstOrDefaultAsync(item => item.Id == entity.Id);

        return existing is null ? await AddAsync(entity) : await UpdateAsync(entity.Id, entity);
    }

    public async Task<T> UpdateAsync([NotNull] string id, [NotNull] object newEntity)
    {
        var entity = await GetAsync(id);
        if (entity is null) return null;

        CheckUpdateObject(entity, newEntity);

        await _context.SaveChangesAsync();

        return _dbSet.First(e => e.Id == id);
    }

    public async Task<T>? DeleteAsync([NotNull] string id)
    {
        var entity = await DbSet.FirstOrDefaultAsync(item => item.Id == id);

        if (entity is null) return null;

        entity = DbSet.Remove(entity).Entity;
        await Save;

        return entity;
    }

    public async Task<IEnumerable<T>> DeleteAsync([NotNull] IEnumerable<string> ids)
    {
        var entities = await DbSet.Where(e => ids.Contains(e.Id)).ToListAsync();

        DbSet.RemoveRange(entities);
        await Save;

        return entities;
    }

    public static void CheckUpdateObject(T originalObj, object updateObj)
    {
        foreach (var property in updateObj.GetType().GetProperties())
        {
            var propValue = property.GetValue(updateObj, null);
            var originalProp = originalObj.GetType().GetProperty(property.Name);
            if (propValue is not null && originalProp is not null) originalProp.SetValue(originalObj, propValue);
        }
    }
}
