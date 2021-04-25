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

        private async Task<T> GetById(string Id)
        {
            var result = _context.Set<T>().FirstOrDefaultAsync(e => e.Id == Id);
            return await result;
        }

        private async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }


        public async Task<IEnumerable<T>> Get()
        {
            return await GetAll();
        }

        public async Task<T> Get(string Id)
        {
            return await GetById(Id);
        }
    }
}