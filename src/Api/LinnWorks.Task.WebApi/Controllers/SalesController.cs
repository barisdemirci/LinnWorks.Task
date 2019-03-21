using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.Services.Sales;
using Microsoft.AspNetCore.Mvc;

namespace LinnWorks.Task.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService saleService;

        public SalesController(ISaleService saleService)
        {
            this.saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
        }

        [HttpPost]
        [Route("getfilterparameters")]
        public FilterParametersDto GetFilterParameters(GetSalesRequestDto requestDto)
        {
            return saleService.GetFilterParameters(requestDto);
        }

        [HttpPost]
        [Route("getlastpageindex")]
        public int GetLastPageIndex(GetSalesRequestDto requestDto)
        {
            if (requestDto == null) throw new ArgumentNullException(nameof(requestDto));

            return saleService.GetLastPageIndex(requestDto);
        }

        [HttpPost]
        public IEnumerable<SaleDto> GetSales(GetSalesRequestDto requestDto)
        {
            if (requestDto == null) throw new ArgumentNullException(nameof(requestDto));

            return saleService.GetFilteredSales(requestDto);
        }

        [HttpPut]
        public void UpdateSales(IEnumerable<SaleDto> salesDto)
        {
            if (salesDto == null) throw new ArgumentNullException(nameof(salesDto));

            saleService.UpdateSales(salesDto);
        }
    }
}