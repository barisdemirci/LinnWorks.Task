using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Dtos
{
    public class GetSalesRequestDto
    {
        public int CountryId { get; set; }

        public int SalesChannelId { get; set; }

        public int OrderPriorityId { get; set; }

        public int RegionId { get; set; }

        public int ItemTypeId { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}