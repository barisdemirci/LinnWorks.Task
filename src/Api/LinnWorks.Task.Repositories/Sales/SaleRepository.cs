using System;
using System.Collections.Generic;
using System.Linq;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinnWorks.Task.Repositories.Sales
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get
            {
                return Context as ApplicationDbContext;
            }
        }

        public SaleRepository(DbContext context) : base(context)
        {

        }

        public IEnumerable<Sale> GetFilteredSales(GetSalesRequestDto requestDto)
        {
            int skip = (requestDto.PageIndex - 1) * requestDto.PageSize;
            var salesQuery = GetQueryFilteredSales(requestDto);
            var sales = salesQuery.Skip(skip).Take(requestDto.PageSize).ToList();
            return sales;
        }

        public FilterParameters GetFilterParameters(GetSalesRequestDto requestDto)
        {
            FilterParameters parameters = new FilterParameters();
            parameters.Countries = ApplicationDbContext.Countries.OrderBy(x => x.CountryName).ToList();
            parameters.ItemTypes = ApplicationDbContext.ItemTypes.OrderBy(x => x.ItemTypeName).ToList();
            parameters.Regions = ApplicationDbContext.Regions.OrderBy(x => x.RegionName).ToList();
            parameters.SalesChannels = ApplicationDbContext.SalesChannels.OrderBy(x => x.SalesChannelName).ToList();
            parameters.OrderPriorities = ApplicationDbContext.OrderPriorities.OrderBy(x => x.OrderPriorityName).ToList();
            return parameters;
        }

        private IQueryable<Sale> GetQueryFilteredSales(GetSalesRequestDto requestDto)
        {
            return ApplicationDbContext.Sales.Where(x => x.OrderDate >= requestDto.OrderDate)
                .Where(x => x.OrderID == requestDto.OrderId || requestDto.OrderId == default(int))
                .Where(x => x.CountryId == requestDto.CountryId || requestDto.CountryId == default(int))
                .Where(x => x.ItemTypeId == requestDto.ItemTypeId || requestDto.ItemTypeId == default(int))
                .Where(x => x.OrderPriorityId == requestDto.OrderPriorityId || requestDto.OrderPriorityId == default(int))
                .Where(x => x.RegionId == requestDto.RegionId || requestDto.RegionId == default(int))
                .Where(x => x.SalesChannelId == requestDto.SalesChannelId || requestDto.SalesChannelId == default(int));
        }

        public int GetLastPageIndex(GetSalesRequestDto requestDto)
        {
            int lastPageIndex = GetQueryFilteredSales(requestDto).Count() / requestDto.PageSize + 1;
            return lastPageIndex;
        }
    }
}