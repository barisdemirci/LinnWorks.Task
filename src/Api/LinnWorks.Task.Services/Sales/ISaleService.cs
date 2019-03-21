using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Dtos.Sales;

namespace LinnWorks.Task.Services.Sales
{
    public interface ISaleService
    {
        IEnumerable<SaleDto> GetFilteredSales(GetSalesRequestDto requestDto);

        FilterParametersDto GetFilterParameters(GetSalesRequestDto requestDto);

        int GetLastPageIndex(GetSalesRequestDto requestDto);

        System.Threading.Tasks.Task AddAllAsync(IEnumerable<SaleDto> salesDto);

        void UpdateSales(IEnumerable<SaleDto> salesDto);
    }
}