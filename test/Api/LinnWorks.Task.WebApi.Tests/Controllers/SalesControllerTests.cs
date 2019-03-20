using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.DtosBuilder;
using LinnWorks.Task.Services.Sales;
using LinnWorks.Task.WebApi.Controllers;
using NSubstitute;
using Xunit;

namespace LinnWorks.Task.WebApi.Tests.Controllers
{
    public class SalesControllerTests
    {
        private readonly SalesController saleController;
        private readonly ISaleService saleService;

        public SalesControllerTests()
        {
            saleService = Substitute.For<ISaleService>();
            saleController = new SalesController(saleService);
        }

        [Fact]
        public void Constructor_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => new SalesController(null));
        }

        [Fact]
        public void GetSales_ArgsOk_CallsService()
        {
            // arrange
            GetSalesRequestDto requestDto = GetSalesRequestDtoBuilder.Build();

            // act
            saleController.GetSales(requestDto);

            // assert
            saleService.Received(1).GetFilteredSales(requestDto);
        }

        [Fact]
        public void GetFilterParameters_NoArgs_CallsService()
        {
            // act
            saleController.GetFilterParameters();

            // assert
            saleService.Received(1).GetFilterParameters();
        }

        [Fact]
        public void UpdateSales_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => saleController.UpdateSales(null));
        }

        [Fact]
        public void UpdateSales_ArgsOk_CallsService()
        {
            // arrange
            List<SaleDto> sales = SaleDtoBuilder.BuildList(1);

            // act
            saleController.UpdateSales(sales);

            // assert
            saleService.Received(1).UpdateSales(sales);
        }
    }
}