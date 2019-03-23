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
            string getFilterParametersUrl = GetEndPointForGetSalesRequest(EndPoints.Api.GetFilterParameters, requestDto);
            FilterParametersDto parametersDto = await httpClient.GetAsync<FilterParametersDto>(getFilterParametersUrl);
            FilterParametersViewModel parameters = new FilterParametersViewModel();
            parameters.Countries = new List<DropDownViewModel>();
            DropDownViewModel allItem = new DropDownViewModel() { Value = default(int).ToString(), Label = "All" };
            foreach (var item in parametersDto.Countries)
            {
                parameters.Countries.Add(
                    new DropDownViewModel()
                    {
                        Label = item.CountryName,
                        Value = item.CountryId.ToString()
                    });
            }
            parameters.Countries.Insert(0, allItem);

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
            parameters.ItemTypes.Insert(0, allItem);

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
            parameters.OrderPriorities.Insert(0, allItem);

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
            parameters.Regions.Insert(0, allItem);

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
            parameters.SalesChannels.Insert(0, allItem);
            return parameters;
        }

        public Task<int> GetLastPageIndex(GetSalesRequestDto requestDto)
        {
            string getSalesUrl = GetEndPointForGetSalesRequest(EndPoints.Api.GetLastPageIndex, requestDto);
            return httpClient.GetAsync<int>(getSalesUrl);
        }

        public async Task<IEnumerable<SaleDto>> GetSalesAsync(GetSalesRequestDto requestDto)
        {
            string getSalesUrl = GetEndPointForGetSalesRequest(EndPoints.Api.GetSales, requestDto);
            return await httpClient.GetAsync<IEnumerable<SaleDto>>(getSalesUrl);
        }

        public System.Threading.Tasks.Task UpdateSalesAsync(IEnumerable<SaleDto> salesDto)
        {
            string updateSalesUrl = configuration[EndPoints.Api.UpdateSales];
            return httpClient.PutAsync(updateSalesUrl, salesDto);
        }

        private string GetEndPointForGetSalesRequest(string endPointKey, GetSalesRequestDto requestDto)
        {
            return string.Format(configuration[endPointKey], requestDto.CountryId, requestDto.SalesChannelId, requestDto.OrderPriorityId, requestDto.RegionId, requestDto.ItemTypeId, requestDto.OrderDate, requestDto.OrderId, requestDto.PageIndex, requestDto.PageSize);
        }
    }
}