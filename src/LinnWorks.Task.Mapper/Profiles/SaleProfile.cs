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
            CreateMap<Sale, SaleDto>();
            CreateMap<SaleDto, Sale>()
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.Country.CountryId))
                .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.Region.RegionId))
                .ForMember(dest => dest.SalesChannelId, opt => opt.MapFrom(src => src.SalesChannel.SalesChannelId))
                .ForMember(dest => dest.OrderPriorityId, opt => opt.MapFrom(src => src.OrderPriority.OrderPriorityId))
                .ForMember(dest => dest.ItemTypeId, opt => opt.MapFrom(src => src.ItemType.ItemTypeId));
            CreateMap<FilterParameters, FilterParametersDto>();
        }
    }
}