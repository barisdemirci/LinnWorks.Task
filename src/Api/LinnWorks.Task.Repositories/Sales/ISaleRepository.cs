using System;
using System.Collections.Generic;
using System.Text;
using LinnWorks.Task.Entities;

namespace LinnWorks.Task.Repositories.Sales
{
    public interface ISaleRepository : IRepository<Sale>
    {
        IEnumerable<Sale> GetFilteredSales();

        FilterParameters GetFilterParameters();
    }
}