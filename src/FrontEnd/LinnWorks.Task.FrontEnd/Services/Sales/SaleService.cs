using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinnWorks.Task.Common;
using LinnWorks.Task.Core.Network;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.Web.ViewModel;
using Microsoft.Extensions.Configuration;

namespace LinnWorks.Task.Web.Services.Sales
{
    public class SaleService : ISaleService
    {
        private readonly IHttpClientWrapper httpClient;
        private readonly IConfiguration configuration;

        public SaleService(IHttpClientWrapper httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            httpClient.BaseUrl = this.configuration[Constants.ApiBaseUrl];
        }

        public async Task<FilterParametersViewModel> GetFilterParameters(GetSalesRequestDto requestDto)
        {
            string getFilterParametersUrl = configuration[EndPoints.Api.GetFilterParameters];
            FilterParametersDto parametersDto = await httpClient.PostAsync<GetSalesRequestDto, FilterParametersDto>(getFilterParametersUrl, requestDto);
            FilterParametersViewModel parameters = new FilterParametersViewModel();
            parameters.Countries = new List<DropDownViewModel>();
            foreach (var item in parametersDto.Countries)
            {
                parameters.Countries.Add(
                    new DropDownViewModel()
                    {
                        Label = item.CountryName,
                        Value = item.CountryId.ToString()
                    });
            }
            parameters.Countries.Insert(0, new DropDownViewModel() { Value = default(int).ToString(), Label = "All" });

            parameters.ItemTypes = new List<DropDownViewModel>();
            foreach (var item in parametersDto.ItemTypes)
            {
                parameters.ItemTypes.Add(
                    new DropDownViewModel()
                    {
                        Label = item.ItemTypeName,
                        Value = item.ItemTypeId.ToString()
                    });
            }
            parameters.ItemTypes.Insert(0, new DropDownViewModel() { Value = default(int).ToString(), Label = "All" });

            parameters.OrderPriorities = new List<DropDownViewModel>();
            foreach (var item in parametersDto.OrderPriorities)
            {
                parameters.OrderPriorities.Add(
                    new DropDownViewModel()
                    {
                        Label = item.OrderPriorityName,
                        Value = item.OrderPriorityId.ToString()
                    });
            }
            parameters.OrderPriorities.Insert(0, new DropDownViewModel() { Value = default(int).ToString(), Label = "All" });

            parameters.Regions = new List<DropDownViewModel>();
            foreach (var item in parametersDto.Regions)
            {
                parameters.Regions.Add(
                    new DropDownViewModel()
                    {
                        Label = item.RegionName,
                        Value = item.RegionId.ToString()
                    });
            }
            parameters.Regions.Insert(0, new DropDownViewModel() { Value = default(int).ToString(), Label = "All" });

            parameters.SalesChannels = new List<DropDownViewModel>();
            foreach (var item in parametersDto.SalesChannels)
            {
                parameters.SalesChannels.Add(
                    new DropDownViewModel()
                    {
                        Label = item.SalesChannelName,
                        Value = item.SalesChannelId.ToString()
                    });
            }
            parameters.SalesChannels.Insert(0, new DropDownViewModel() { Value = default(int).ToString(), Label = "All" });
            return parameters;
        }

        public async Task<int> GetLastPageIndexAsync(GetSalesRequestDto requestDto)
        {
            string getSalesUrl = configuration[EndPoints.Api.GetLastPageIndex];
            int lastPageIndex = await httpClient.PostAsync<GetSalesRequestDto, int>(getSalesUrl, requestDto);
            return lastPageIndex;
        }

        public async Task<IEnumerable<SaleDto>> GetSalesAsync(GetSalesRequestDto requestDto)
        {
            string getSalesUrl = configuration[EndPoints.Api.GetSales];
            IEnumerable<SaleDto> sales = await httpClient.PostAsync<GetSalesRequestDto, IEnumerable<SaleDto>>(getSalesUrl, requestDto);
            return sales;
        }

        public async System.Threading.Tasks.Task UpdateSalesAsync(IEnumerable<SaleDto> salesDto)
        {
            string getSalesUrl = configuration[EndPoints.Api.UpdateSales];
            await httpClient.PutAsync<IEnumerable<SaleDto>>(getSalesUrl, salesDto);
        }
    }
}