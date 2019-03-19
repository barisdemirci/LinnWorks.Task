using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.Web.ViewModel;

namespace LinnWorks.Task.Web.Services.Sales
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleDto>> GetSalesAsync();

        System.Threading.Tasks.Task AddAllAsync(IEnumerable<SaleDto> salesDto);

        System.Threading.Tasks.Task UpdateSalesAsync(IEnumerable<SaleDto> salesDto);

        Task<FilterParametersViewModel> GetFilterParameters();
    }
}