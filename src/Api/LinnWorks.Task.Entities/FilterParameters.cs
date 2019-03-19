using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Entities
{
    public class FilterParameters
    {
        public IEnumerable<Country> Countries { get; set; }

        public IEnumerable<ItemType> ItemTypes { get; set; }

        public IEnumerable<SalesChannel> SalesChannels { get; set; }

        public IEnumerable<OrderPriority> OrderPriorities { get; set; }

        public IEnumerable<Region> Regions { get; set; }
    }
}