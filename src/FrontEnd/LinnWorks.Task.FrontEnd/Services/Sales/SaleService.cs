using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinnWorks.Task.Common;
using LinnWorks.Task.Core;
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

        public System.Threading.Tasks.Task AddAllAsync(IEnumerable<SaleDto> salesDto)
        {
            throw new NotImplementedException();
        }

        public async Task<FilterParametersViewModel> GetFilterParameters()
        {
            string getFilterParametersUrl = configuration[EndPoints.Api.GetFilterParameters];
            FilterParametersDto parametersDto = await httpClient.GetAsync<FilterParametersDto>(getFilterParametersUrl);
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
            return parameters;
        }

        public async Task<IEnumerable<SaleDto>> GetSalesAsync()
        {
            string getSalesUrl = configuration[EndPoints.Api.GetSales];
            IEnumerable<SaleDto> sales = await httpClient.GetAsync<IEnumerable<SaleDto>>(getSalesUrl);
            return sales;
        }

        public System.Threading.Tasks.Task UpdateSalesAsync(IEnumerable<SaleDto> salesDto)
        {
            throw new NotImplementedException();
        }
    }
}
