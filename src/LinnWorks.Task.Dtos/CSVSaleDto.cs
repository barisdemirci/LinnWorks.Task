using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Dtos
{
    public class CSVSaleDto
    {
        public int SaleId { get; set; }

        public string RegionName { get; set; }

        public string CountryName { get; set; }

        public string ItemTypeName { get; set; }

        public string SalesChannelName { get; set; }

        public string OrderPriorityName { get; set; }

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
