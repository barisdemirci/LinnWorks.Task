using System;
using AutoMapper;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.Entities;

namespace LinnWorks.Task.Mapper.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<ItemType, ItemTypeDto>().ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<SalesChannel, SalesChannelDto>().ReverseMap();
            CreateMap<OrderPriority, OrderPriorityDto>().ReverseMap();
            CreateMap<Sale, SaleDto>().ReverseMap();
            CreateMap<FilterParameters, FilterParametersDto>();
        }
    }
}