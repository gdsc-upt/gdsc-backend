using System.Diagnostics.CodeAnalysis;
using GdscBackend.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace GdscBackend.Database;

public interface IRepository<T> where T : class, IModel
{
    DbSet<T> DbSet { get; }
    Task<T> AddAsync([NotNull] T entity);
    Task<T> GetAsync([NotNull] string id);
    Task<IEnumerable<T>> GetAsync();
    Task<T> AddOrUpdateAsync([NotNull] T entity);
    Task<T> UpdateAsync([NotNull] string id ,[NotNull] object entity);
    Task<T>? DeleteAsync([NotNull] string id);
    Task<IEnumerable<T>> DeleteAsync([NotNull] IEnumerable<string> ids);
}