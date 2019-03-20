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
        public async Task<IEnumerable<SaleDto>> GetSalesAsync([FromBody]GetSalesRequestDto requestDto)
        {
            if (requestDto == null) throw new ArgumentNullException(nameof(requestDto));

            return await saleService.GetSalesAsync(requestDto);
        }

        [HttpGet]
        [Route("getfilterparameters")]
        public async Task<FilterParametersViewModel> GetFilterParameters()
        {
            return await saleService.GetFilterParameters();
        }

        [HttpPut]
        public async System.Threading.Tasks.Task UpdateSalesAsync([FromBody] IEnumerable<SaleDto> sales)
        {
            if (sales == null) throw new ArgumentNullException(nameof(sales));

            await saleService.UpdateSalesAsync(sales);
        }
    }
}