using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.Web.Services.Sales;
using LinnWorks.Task.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpPost]
        public async Task<IActionResult> GetSalesAsync([FromBody]GetSalesRequestDto requestDto)
        {
            if (requestDto == null) BadRequest();

            var sales = await saleService.GetSalesAsync(requestDto);

            return Ok(sales);
        }

        [HttpPost]
        [Route("getfilterparameters")]
        [ResponseCache(Duration = 10)]
        public async Task<FilterParametersViewModel> GetFilterParameters([FromBody]GetSalesRequestDto requestDto)
        {
            if (requestDto == null) BadRequest();

            return await saleService.GetFilterParameters(requestDto);
        }

        [HttpPost]
        [Route("getlastpageindex")]
        public Task<int> GetLastPageIndexAsync([FromBody]GetSalesRequestDto requestDto)
        {
            if (requestDto == null) BadRequest();

            return saleService.GetLastPageIndex(requestDto);
        }

        [HttpPut]
        public System.Threading.Tasks.Task UpdateSalesAsync([FromBody] IEnumerable<SaleDto> sales)
        {
            if (sales == null) BadRequest();

            return saleService.UpdateSalesAsync(sales);
        }
    }
}