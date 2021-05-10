using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gdsc_web_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace gdsc_web_backend.Database
{
    public class Repository<T> : IRepository<T> where T : Model
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> Get(string id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<T> Update(string Id, T entity)
        {
            var does_exists = _context.Set<T>().FirstOrDefaultAsync(item => item.Id == entity.Id);
             
            if (entity is null)
            {
                throw new ArgumentNullException("entity must not be null");
            }
            if (does_exists is null)
            {
                throw new ArgumentNullException("entity does not exists in database");
            }

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task<T> Delete(string Id)
        {
            var entity = _context.Set<T>().FirstOrDefaultAsync(item => item.Id == Id);
            

            if (entity is null)
            {
                throw new ArgumentNullException("entity does not exists in database");
            }
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return await  entity;

        }
    }
}