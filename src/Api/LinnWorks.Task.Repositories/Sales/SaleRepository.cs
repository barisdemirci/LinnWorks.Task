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
            var selectedUserSectorQuery = GetQueryFilteredSales(requestDto);
            var sales = selectedUserSectorQuery.Skip(skip).Take(requestDto.PageSize).ToList();
            return sales;
        }

        public FilterParameters GetFilterParameters(GetSalesRequestDto requestDto)
        {
            FilterParameters parameters = new FilterParameters();
            parameters.Countries = ApplicationDbContext.Countries.ToList();
            parameters.ItemTypes = ApplicationDbContext.ItemTypes.ToList();
            parameters.Regions = ApplicationDbContext.Regions.ToList();
            parameters.SalesChannels = ApplicationDbContext.SalesChannels.ToList();
            parameters.OrderPriorities = ApplicationDbContext.OrderPriorities.ToList();
            return parameters;
        }

        private IQueryable<Sale> GetQueryFilteredSales(GetSalesRequestDto requestDto)
        {
            return (from sale in ApplicationDbContext.Set<Sale>()
                    join country in ApplicationDbContext.Set<Country>() on sale.Country.CountryId equals country.CountryId
                    where (country.CountryId == requestDto.CountryId || requestDto.CountryId == default(int))
                    join itemType in ApplicationDbContext.Set<ItemType>() on sale.ItemType.ItemTypeId equals itemType.ItemTypeId
                    where (itemType.ItemTypeId == requestDto.ItemTypeId || requestDto.ItemTypeId == default(int))
                    join order in ApplicationDbContext.Set<OrderPriority>() on sale.OrderPriority.OrderPriorityId equals order.OrderPriorityId
                    where (order.OrderPriorityId == requestDto.OrderPriorityId || requestDto.OrderPriorityId == default(int))
                    join region in ApplicationDbContext.Set<Region>() on sale.Region.RegionId equals region.RegionId
                    where (region.RegionId == requestDto.RegionId || requestDto.RegionId == default(int))
                    join channel in ApplicationDbContext.Set<SalesChannel>() on sale.SalesChannel.SalesChannelId equals channel.SalesChannelId
                    where (channel.SalesChannelId == requestDto.SalesChannelId || requestDto.SalesChannelId == default(int))
                    select new Sale
                    {
                        Country = country,
                        OrderPriority = sale.OrderPriority,
                        ItemType = itemType,
                        Region = sale.Region,
                        SalesChannel = sale.SalesChannel,
                        OrderID = sale.OrderID,
                        OrderDate = sale.OrderDate,
                        SaleID = sale.SaleID,
                        ShipDate = sale.ShipDate,
                        TotalCost = sale.TotalCost,
                        TotalProfit = sale.TotalProfit,
                        TotalRevenue = sale.TotalRevenue,
                        UnitCost = sale.UnitCost,
                        UnitPrice = sale.UnitPrice,
                        UnitSold = sale.UnitSold

                    });
        }

        public int GetLastPageIndex(GetSalesRequestDto requestDto)
        {
            int lastPageIndex = GetQueryFilteredSales(requestDto).Count() / requestDto.PageSize + 1;
            return lastPageIndex;
        }
    }
}