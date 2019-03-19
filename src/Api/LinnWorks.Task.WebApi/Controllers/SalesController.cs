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

        [HttpGet]
        [Route("getfilterparameters")]
        public FilterParametersDto GetFilterParameters()
        {
            return saleService.GetFilterParameters();
        }

        [HttpGet]
        public IEnumerable<SaleDto> GetSales()
        {
            return saleService.GetFilteredSales();
        }

        [HttpPut]
        public void UpdateSales(IEnumerable<SaleDto> salesDto)
        {
            saleService.UpdateSales(salesDto);
        }
    }
}