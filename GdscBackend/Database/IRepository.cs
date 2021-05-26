using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GdscBackend.Models;

namespace GdscBackend.Database
{
    public interface IRepository<T> where T : class, IModel
    {
        Task<T> AddAsync([NotNull] T entity);
        Task<T> GetAsync([NotNull] string id);
        Task<IEnumerable<T>> GetAsync();
        Task<T> AddOrUpdateAsync([NotNull] T entity);
        Task<T> UpdateAsync([NotNull] T entity);
        Task<T> DeleteAsync([NotNull] string id);
        Task<IEnumerable<T>> DeleteAsync([NotNull] IEnumerable<string> ids);

    }
}
