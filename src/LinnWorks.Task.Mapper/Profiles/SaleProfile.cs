using System;
using System.Collections.Generic;
using System.Text;
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
            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.CountryName))
                .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => src.ItemType.ItemTypeName))
                .ForMember(dest => dest.OrderPriority, opt => opt.MapFrom(src => src.OrderPriority.OrderPriorityName))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region.RegionName))
                .ForMember(dest => dest.SalesChannel, opt => opt.MapFrom(src => src.SalesChannel.SalesChannelName));
            CreateMap<SaleDto, Sale>();
            CreateMap<FilterParameters, FilterParametersDto>();
        }
    }
}