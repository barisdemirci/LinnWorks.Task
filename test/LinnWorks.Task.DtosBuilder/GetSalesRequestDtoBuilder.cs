using System;
using LinnWorks.Task.Dtos;

namespace LinnWorks.Task.DtosBuilder
{
    public static class GetSalesRequestDtoBuilder
    {
        public static GetSalesRequestDto Build(int countryId = 1, int itemTypeId = 1, int orderPriorityId = 1, int regionId = 1, int salesChannelId = 1, int pageIndex = 1, int pageSize = 1000)
        {
            return new GetSalesRequestDto()
            {
                CountryId = countryId,
                ItemTypeId = itemTypeId,
                OrderPriorityId = orderPriorityId,
                PageIndex = pageIndex,
                PageSize = pageSize,
                RegionId = regionId,
                SalesChannelId = salesChannelId
            };
        }
    }
}