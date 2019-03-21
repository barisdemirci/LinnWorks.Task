using System;
using System.Collections.Generic;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.DtosBuilder;
using LinnWorks.Task.Web.Controllers;
using LinnWorks.Task.Web.Services.Sales;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Xunit;

namespace LinnWorks.Task.FrontEnd.Tests.Controllers
{
    public class SalesControllerTests
    {

        private readonly SalesController saleController;
        private readonly ISaleService saleService;
        private readonly IFormFile file;

        public SalesControllerTests()
        {
            saleService = Substitute.For<ISaleService>();
            file = Substitute.For<IFormFile>();
            saleController = new SalesController(saleService);
        }

        [Fact]
        public void Constructor_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => new SalesController(null));
        }

        [Fact]
        public void GetSectorsAsync_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.ThrowsAsync<ArgumentNullException>(() => saleController.GetSalesAsync(null));
        }

        [Fact]
        public async System.Threading.Tasks.Task GetSectorsAsync_ArgsOk_CallsService()
        {
            // arrange
            GetSalesRequestDto requestDto = GetSalesRequestDtoBuilder.Build();

            // act
            await saleController.GetSalesAsync(requestDto);

            // assert
            await saleService.Received(1).GetSalesAsync(requestDto);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetFilterParameters_CallsService()
        {
            // arrange 
            GetSalesRequestDto requestDto = GetSalesRequestDtoBuilder.Build();

            // act
            await saleController.GetFilterParameters(requestDto);

            // assert
            await saleService.Received(1).GetFilterParameters(requestDto);
        }

        [Fact]
        public void UpdateSales_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.ThrowsAsync<ArgumentNullException>(() => saleController.UpdateSalesAsync(null));
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateSalesAsync_ArgsOk_CallsService()
        {
            // arrange
            List<SaleDto> sales = SaleDtoBuilder.BuildList(1);

            // act
            await saleController.UpdateSalesAsync(sales);

            // assert
            await saleService.Received(1).UpdateSalesAsync(sales);
        }

        [Fact]
        public void GetLastPageIndexAsync_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.ThrowsAsync<ArgumentNullException>(() => saleController.GetLastPageIndexAsync(null));
        }

        [Fact]
        public async System.Threading.Tasks.Task GetLastPageIndexAsync_ArgsOk_CallsService()
        {
            // arrange
            GetSalesRequestDto requestDto = GetSalesRequestDtoBuilder.Build();

            // act
            await saleController.GetLastPageIndexAsync(requestDto);

            // assert
            await saleService.Received(1).GetLastPageIndexAsync(requestDto);
        }
    }
}