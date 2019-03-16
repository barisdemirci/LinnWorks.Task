using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LinnWorks.Task.Entities
{
    public class Sale : BaseEntity
    {
        [Key]
        public int SaleID { get; set; }

        [ForeignKey("RegionId")]
        public Region Region { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [ForeignKey("ItemTypeId")]
        public ItemType ItemType { get; set; }

        [ForeignKey("SalesChannelId")]
        public SalesChannel SalesChannel { get; set; }

        [ForeignKey("OrderPriorityId")]
        public OrderPriority OrderPriority { get; set; }

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