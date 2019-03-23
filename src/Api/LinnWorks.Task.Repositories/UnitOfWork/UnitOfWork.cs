using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LinnWorks.Task.Common.Base;
using LinnWorks.Task.Entities;
using LinnWorks.Task.Repositories.Sales;

namespace LinnWorks.Task.Repositories.UnitOfWork
{
    public class UnitOfWork : DisposableObject, IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public ApplicationDbContext Context { get { return context; } }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Sales = new SaleRepository(context);
        }

        public ISaleRepository Sales { get; private set; }

        async System.Threading.Tasks.Task IUnitOfWork.SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}