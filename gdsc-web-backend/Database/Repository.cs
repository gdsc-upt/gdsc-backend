using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using gdsc_web_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace gdsc_web_backend.Database
{
    public class Repository<T> : IRepository<T> where T : Model
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSetCache;
        private Task<int> Save => _context.SaveChangesAsync();

        public DbSet<T> DbSet => _dbSetCache ??= _context.Set<T>();

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync([NotNull] T entity)
        {
            entity.Id = Guid.NewGuid().ToString();

            entity = (await DbSet.AddAsync(entity)).Entity;
            await Save;

            return entity;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetAsync([NotNull] string id)
        {
            return await DbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> AddOrUpdateAsync([NotNull] T entity)
        {
            if (entity is null) return null;

            var existing = await DbSet.FirstOrDefaultAsync(item => item.Id == entity.Id);

            return existing is null ? await AddAsync(entity) : await UpdateAsync(entity);
        }

        public async Task<T> UpdateAsync([NotNull] T entity)
        {
            if (entity?.Id == null) return null;

            entity = DbSet.Update(entity).Entity;
            await Save;

            return entity;
        }

        public async Task<T> DeleteAsync([NotNull] string id)
        {
            var entity = await DbSet.FirstOrDefaultAsync(item => item.Id == id);

            if (entity is null) return null;

            entity = DbSet.Remove(entity).Entity;
            await Save;

            return entity;
        }
    }
}