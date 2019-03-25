using System;
using System.Collections.Generic;
using LinnWorks.Task.Dtos.Sales;

namespace LinnWorks.Task.DtosBuilder
{
    public static class SaleDtoBuilder
    {
        public static SaleDto Build(int saleId = 1, int countryId = 1, int itemTypeId = 1, int regionId = 1, int orderPriorityId = 1, int salesChannelId = 1)
        {
            return new SaleDto()
            {
                OrderDate = DateTime.UtcNow,
                OrderID = 1,
                SaleId = saleId,
                CountryId = countryId,
                ItemTypeId = itemTypeId,
                RegionId = regionId,
                OrderPriorityId = orderPriorityId,
                SalesChannelId = salesChannelId
            };
        }

        public static List<SaleDto> BuildList(int countOfItem, int saleId = 1, int countryId = 1, int itemTypeId = 1, int regionId = 1, int orderPriorityId = 1, int salesChannelId = 1)
        {
            List<SaleDto> sales = new List<SaleDto>();
            for (int i = 0; i < countOfItem; i++)
            {
                sales.Add(Build(saleId, countryId, itemTypeId, regionId, orderPriorityId, salesChannelId));
            }
            return sales;
        }
    }
}