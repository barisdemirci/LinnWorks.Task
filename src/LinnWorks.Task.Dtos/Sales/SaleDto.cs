using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Dtos.Sales
{
    public class SaleDto
    {
        public int SaleId { get; set; }

        public int RegionId { get; set; }

        public int CountryId { get; set; }

        public int ItemTypeId { get; set; }

        public int SalesChannelId { get; set; }

        public int OrderPriorityId { get; set; }

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