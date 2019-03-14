using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Entities
{
    public class Sale : BaseEntity
    {
        public string Region { get; set; }

        public string Country { get; set; }

        public string ItemTypes { get; set; }

        public string SalesChannel { get; set; }

        public string OrderPriority { get; set; }

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