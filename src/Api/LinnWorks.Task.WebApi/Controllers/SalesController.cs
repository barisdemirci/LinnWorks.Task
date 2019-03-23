using System;
using System.Collections.Generic;
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
        [Route("filterparameters")]
        public IActionResult GetFilterParameters([FromQuery]GetSalesRequestDto requestDto)
        {
            if (requestDto == null) BadRequest();

            FilterParametersDto parameters = saleService.GetFilterParameters(requestDto);

            return Ok(parameters);
        }

        [HttpGet]
        [Route("lastpageindex")]
        public IActionResult GetLastPageIndex([FromQuery]GetSalesRequestDto requestDto)
        {
            if (requestDto == null) BadRequest();

            int lastIndex = saleService.GetLastPageIndex(requestDto);

            return Ok(lastIndex);
        }

        [HttpGet]
        public IActionResult GetSales([FromQuery]GetSalesRequestDto requestDto)
        {
            if (requestDto == null) BadRequest();

            var sales = saleService.GetFilteredSales(requestDto);

            return Ok(sales);
        }

        [HttpPut]
        public IActionResult UpdateSalesAsync(IEnumerable<SaleDto> salesDto)
        {
            if (salesDto == null) BadRequest();

            saleService.UpdateSales(salesDto);

            return Ok();
        }
    }
}