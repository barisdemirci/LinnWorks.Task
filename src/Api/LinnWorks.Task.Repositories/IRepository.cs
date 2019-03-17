using System.Collections.Generic;

namespace LinnWorks.Task.Repositories
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class
    {
        System.Threading.Tasks.Task AddAllAsync(IEnumerable<TEntity> rows);

        System.Threading.Tasks.Task AddAsync(TEntity row);

        void Update(TEntity row);

        void Delete(TEntity row);
    }
}