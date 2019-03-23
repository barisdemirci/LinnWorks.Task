using System;

namespace LinnWorks.Task.Dtos
{
    public class GetSalesRequestDto
    {
        public int CountryId { get; set; }

        public int SalesChannelId { get; set; }

        public int OrderPriorityId { get; set; }

        public int RegionId { get; set; }

        public int ItemTypeId { get; set; }

        public DateTime OrderDate { get; set; }

        public int OrderId { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}