using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinnWorks.Task.Dtos.Sales;

namespace LinnWorks.Task.Services.Sales
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleDto>> GetSalesAsync();
    }
}