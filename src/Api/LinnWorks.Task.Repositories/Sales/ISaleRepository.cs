using System;
using System.Collections.Generic;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Entities;

namespace LinnWorks.Task.Repositories.Sales
{
    public interface ISaleRepository : IRepository<Sale>
    {
        IEnumerable<Sale> GetFilteredSales(GetSalesRequestDto requestDto);

        FilterParameters GetFilterParameters(GetSalesRequestDto requestDto);

        int GetLastPageIndex(GetSalesRequestDto requestDto);
    }
}