using System;
using Microsoft.EntityFrameworkCore;

namespace LinnWorks.Task.Repositories
{
    public class Repository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity>
         where TEntity : class
    {
        public Repository(DbContext context)
            : base(context)
        {

        }

        public async System.Threading.Tasks.Task AddAsync(TEntity row)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));

            await Context.Set<TEntity>().AddAsync(row);
        }

        public void Delete(TEntity row)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));

            Context.Set<TEntity>().Remove(row);
        }

        public void Update(TEntity row)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));

            Context.Set<TEntity>().Update(row);
        }
    }
}