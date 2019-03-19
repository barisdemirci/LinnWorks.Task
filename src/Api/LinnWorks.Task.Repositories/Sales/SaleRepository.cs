using LinnWorks.Task.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public IEnumerable<Sale> GetFilteredSales()
        {
            List<Sale> selectedUserSector = (from sale in ApplicationDbContext.Set<Entities.Sale>()
                                             join country in ApplicationDbContext.Set<Entities.Country>() on sale.Country.CountryId equals country.CountryId
                                             join itemType in ApplicationDbContext.Set<Entities.ItemType>() on sale.ItemType.ItemTypeId equals itemType.ItemTypeId
                                             join order in ApplicationDbContext.Set<Entities.OrderPriority>() on sale.OrderPriority.OrderPriorityId equals order.OrderPriorityId
                                             join region in ApplicationDbContext.Set<Entities.Region>() on sale.Region.RegionId equals region.RegionId
                                             join channel in ApplicationDbContext.Set<Entities.SalesChannel>() on sale.SalesChannel.SalesChannelId equals channel.SalesChannelId
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

                                             }).ToList();
            return selectedUserSector;
        }

        public FilterParameters GetFilterParameters()
        {
            FilterParameters parameters = new FilterParameters();
            parameters.Countries = ApplicationDbContext.Countries.ToList();
            parameters.ItemTypes = ApplicationDbContext.ItemTypes.ToList();
            parameters.Regions = ApplicationDbContext.Regions.ToList();
            parameters.SalesChannels = ApplicationDbContext.SalesChannels.ToList();
            parameters.OrderPriorities = ApplicationDbContext.OrderPriorities.ToList();
            return parameters;
        }
    }
}