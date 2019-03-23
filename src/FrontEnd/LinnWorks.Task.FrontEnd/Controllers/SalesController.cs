using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.Web.Services.Sales;
using Microsoft.AspNetCore.Mvc;

namespace LinnWorks.Task.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : Controller
    {
        private readonly ISaleService saleService;

        public SalesController(ISaleService saleService)
        {
            this.saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesAsync([FromQuery]GetSalesRequestDto requestDto)
        {
            if (requestDto == null) BadRequest();

            var sales = await saleService.GetSalesAsync(requestDto);

            return Ok(sales);
        }

        [HttpGet]
        [Route("filterparameters")]
        [ResponseCache(Duration = 10)]
        public async Task<IActionResult> GetFilterParameters([FromQuery]GetSalesRequestDto requestDto)
        {
            if (requestDto == null) BadRequest();

            var parameters = await saleService.GetFilterParameters(requestDto);

            return Ok(parameters);
        }

        [HttpGet]
        [Route("lastpageindex")]
        public async Task<IActionResult> GetLastPageIndexAsync([FromQuery]GetSalesRequestDto requestDto)
        {
            if (requestDto == null) BadRequest();

            var lastIndex = await saleService.GetLastPageIndex(requestDto);

            return Ok(lastIndex);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSalesAsync([FromBody] IEnumerable<SaleDto> sales)
        {
            if (sales == null) BadRequest();

            await saleService.UpdateSalesAsync(sales);

            return Ok(true);
        }
    }
}