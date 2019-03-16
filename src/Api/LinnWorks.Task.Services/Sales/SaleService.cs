using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LinnWorks.Task.Dtos.Sales;

namespace LinnWorks.Task.Services.Sales
{
    public class SaleService : ISaleService
    {
        public async Task<IEnumerable<SaleDto>> GetSalesAsync()
        {
            List<SaleDto> result = new List<SaleDto>();
            result.Add(new SaleDto()
            {
                Country = "TR"
            });

            return await System.Threading.Tasks.Task.FromResult(result);
        }
    }
}