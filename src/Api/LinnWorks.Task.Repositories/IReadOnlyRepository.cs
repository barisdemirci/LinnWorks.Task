using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinnWorks.Task.Repositories
{
    public interface IReadOnlyRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}