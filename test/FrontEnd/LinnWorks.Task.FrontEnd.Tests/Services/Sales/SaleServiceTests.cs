using System;
using System.Collections.Generic;
using LinnWorks.Task.Common;
using LinnWorks.Task.Core.Network;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.DtosBuilder;
using LinnWorks.Task.Web.Services.Sales;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;

namespace LinnWorks.Task.FrontEnd.Tests.Services.Sales
{
    public class SaleServiceTests
    {
        private readonly ISaleService saleService;
        private readonly IHttpClientWrapper httpClient;
        private readonly IConfiguration configuration;

        public SaleServiceTests()
        {
            httpClient = Substitute.For<IHttpClientWrapper>();
            configuration = Substitute.For<IConfiguration>();
            saleService = new SaleService(httpClient, configuration);
        }

        [Fact]
        public void Constructor_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => new SaleService(null, configuration));
            Assert.Throws<ArgumentNullException>(() => new SaleService(httpClient, null));
        }

        [Fact]
        public async System.Threading.Tasks.Task GetFilterParameters_CallsApi()
        {
            // arrange
            string endpoint = "endPoint";
            configuration[EndPoints.Api.GetFilterParameters].Returns(endpoint);
            FilterParametersDto parameters = new FilterParametersDto()
            {
                Countries = new List<CountryDto>(),
                ItemTypes = new List<ItemTypeDto>(),
                OrderPriorities = new List<OrderPriorityDto>(),
                Regions = new List<RegionDto>(),
                SalesChannels = new List<SalesChannelDto>()
            };
            httpClient.GetAsync<FilterParametersDto>(endpoint).Returns(parameters);

            // act
            await saleService.GetFilterParameters();

            // assert
            await httpClient.Received(1).GetAsync<FilterParametersDto>(endpoint);
        }

        [Fact]
        public void GetSalesAsync_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.ThrowsAsync<ArgumentNullException>(() => saleService.GetSalesAsync(null));
        }

        [Fact]
        public async System.Threading.Tasks.Task GetSalesAsync_ArgsOk_CallsApi()
        {
            // arrange
            string endpoint = "endPoint";
            configuration[EndPoints.Api.GetSales].Returns(endpoint);
            GetSalesRequestDto requestDto = GetSalesRequestDtoBuilder.Build();

            // act
            await saleService.GetSalesAsync(requestDto);

            // assert
            await httpClient.Received(1).PostAsync<GetSalesRequestDto, IEnumerable<SaleDto>>(endpoint, requestDto);
        }

        [Fact]
        public void UpdateSalesAsync_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // act & assert
            Assert.ThrowsAsync<ArgumentNullException>(() => saleService.UpdateSalesAsync(null));
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateSalesAsync_ArgsOk_CallsApi()
        {
            // arrange
            string endpoint = "endPoint";
            configuration[EndPoints.Api.UpdateSales].Returns(endpoint);
            List<SaleDto> sales = SaleDtoBuilder.BuildList(1);

            // act
            await saleService.UpdateSalesAsync(sales);

            // assert
            await httpClient.Received(1).PutAsync<IEnumerable<SaleDto>>(endpoint, sales);
        }
    }
}