using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Dtos.Sales
{
    public class SaleDto
    {
        public int SaleId { get; set; }

        public RegionDto Region { get; set; }

        public CountryDto Country { get; set; }

        public ItemTypeDto ItemType { get; set; }

        public SalesChannelDto SalesChannel { get; set; }

        public OrderPriorityDto OrderPriority { get; set; }

        public DateTime OrderDate { get; set; }

        public int OrderID { get; set; }

        public DateTime ShipDate { get; set; }

        public int UnitSold { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitCost { get; set; }

        public decimal TotalRevenue { get; set; }

        public decimal TotalCost { get; set; }

        public decimal TotalProfit { get; set; }
    }
}