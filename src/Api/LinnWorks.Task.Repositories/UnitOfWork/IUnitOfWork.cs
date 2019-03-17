using System;
using System.Collections.Generic;
using System.Text;
using LinnWorks.Task.Repositories.Sales;

namespace LinnWorks.Task.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        ISaleRepository Sales { get; }

        System.Threading.Tasks.Task SaveAsync();
    }
}