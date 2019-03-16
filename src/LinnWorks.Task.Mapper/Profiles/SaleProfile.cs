using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.Entities;

namespace LinnWorks.Task.Mapper.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, SaleDto>();
        }
    }
}