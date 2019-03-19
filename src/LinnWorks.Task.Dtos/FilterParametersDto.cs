using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Dtos
{
    public class FilterParametersDto
    {
        public IEnumerable<CountryDto> Countries { get; set; }

        public IEnumerable<ItemTypeDto> ItemTypes { get; set; }

        public IEnumerable<SalesChannelDto> SalesChannels { get; set; }

        public IEnumerable<OrderPriorityDto> OrderPriorities { get; set; }

        public IEnumerable<RegionDto> Regions { get; set; }
    }
}