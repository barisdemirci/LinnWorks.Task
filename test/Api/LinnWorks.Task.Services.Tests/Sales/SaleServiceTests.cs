using System;
using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.DtosBuilder;
using LinnWorks.Task.Entities;
using LinnWorks.Task.Mapper.Profiles;
using LinnWorks.Task.Repositories.UnitOfWork;
using LinnWorks.Task.Services.Sales;
using NSubstitute;
using Xunit;

namespace LinnWorks.Task.Services.Tests.Sales
{
    public class SaleServiceTests
    {
        private readonly ISaleService saleService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public SaleServiceTests()
        {
            unitOfWork = Substitute.For<IUnitOfWork>();
            mapper = Substitute.For<IMapper>();
            saleService = new SaleService(unitOfWork, mapper);
        }

        [Fact]
        public void Constructor_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => new SaleService(null, mapper));
            Assert.Throws<ArgumentNullException>(() => new SaleService(unitOfWork, null));
        }

        [Fact]
        public void AddAllAsync_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.ThrowsAsync<ArgumentNullException>(() => saleService.AddAllAsync(null));
        }

        [Fact]
        public void AddAllAsync_ArgsOk_CallsRepository()
        {
            // arrange
            GetSalesRequestDto requestDto = GetSalesRequestDtoBuilder.Build();
            IEnumerable<SaleDto> salesDto = SaleDtoBuilder.BuildList(2);

            // act
            saleService.AddAllAsync(salesDto);

            // assert
            unitOfWork.Sales.Received(1).AddAllAsync(Arg.Any<IEnumerable<Sale>>());
        }

        [Fact]
        public void GetFilteredSales_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => saleService.GetFilteredSales(null));
        }

        [Fact]
        public void GetFilteredSales_ArgsOk_CallsRepository()
        {
            // arrange
            GetSalesRequestDto requestDto = GetSalesRequestDtoBuilder.Build();
            IEnumerable<SaleDto> salesDto = SaleDtoBuilder.BuildList(2);
            List<Sale> sales = new List<Sale>()
            {
                new Sale()
            };
            mapper.Map<IEnumerable<SaleDto>>(Arg.Any<IEnumerable<Sale>>()).Returns(salesDto);
            unitOfWork.Sales.GetFilteredSales(requestDto).Returns(sales);

            // act
            saleService.GetFilteredSales(requestDto);

            // assert
            unitOfWork.Sales.Received(1).GetFilteredSales(requestDto);
        }

        [Fact]
        public void GetFilterParameters_ArgsOk_CallsRepository()
        {

            // act
            var result = saleService.GetFilterParameters();

            // assert
            unitOfWork.Sales.Received(1).GetFilterParameters();
        }

        [Fact]
        public void UpdateSales_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => saleService.UpdateSales(null));
        }

        [Fact]
        public void UpdateSales_ArgsOk_CallsRepository()
        {
            // arrange
            GetSalesRequestDto requestDto = GetSalesRequestDtoBuilder.Build();
            IEnumerable<SaleDto> salesDto = SaleDtoBuilder.BuildList(2);
            List<Sale> sales = new List<Sale>()
            {
                new Sale()
            };
            mapper.Map<IEnumerable<Sale>>(salesDto).Returns(sales);
            unitOfWork.Sales.GetFilteredSales(requestDto).Returns(sales);

            // act
            saleService.UpdateSales(salesDto);

            // assert
            unitOfWork.Sales.Received(1).UpdateRange(sales);
        }
    }
}