using System;
using System.Collections.Generic;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Dtos.Sales;

namespace LinnWorks.Task.DtosBuilder
{
    public static class SaleDtoBuilder
    {
        public static SaleDto Build(int saleId = 1, string country = "country", string itemType = "itemType", string region = "region", string orderPriority = "OrderPriority", string salesChannel = "salesChannel")
        {
            return new SaleDto()
            {
                Country = new CountryDto() { CountryName = country },
                ItemType = new ItemTypeDto() { ItemTypeName = itemType },
                OrderDate = DateTime.UtcNow,
                OrderID = 1,
                Region = new RegionDto() { RegionName = region },
                OrderPriority = new OrderPriorityDto() { OrderPriorityName = orderPriority },
                SalesChannel = new SalesChannelDto() { SalesChannelName = salesChannel },
                SaleId = saleId
            };
        }

        public static List<SaleDto> BuildList(int countOfItem, int saleId = 1, string country = "country", string itemType = "itemType", string region = "region", string orderPriority = "OrderPriority", string salesChannel = "salesChannel")
        {
            List<SaleDto> sales = new List<SaleDto>();
            for (int i = 0; i < countOfItem; i++)
            {
                sales.Add(Build(saleId, country, itemType, region, orderPriority, salesChannel));
            }
            return sales;
        }
    }
}