using System;
using System.Collections.Generic;
using System.Text;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Entities;

namespace LinnWorks.Task.Repositories.Sales
{
    public interface ISaleRepository : IRepository<Sale>
    {
        IEnumerable<Sale> GetFilteredSales(GetSalesRequestDto requestDto);

        FilterParameters GetFilterParameters();
    }
}