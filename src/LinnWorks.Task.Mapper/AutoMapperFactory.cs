using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using LinnWorks.Task.Mapper.Profiles;

namespace LinnWorks.Task.Mapper
{
    public static class AutoMapperFactory
    {
        public static IMapper CreateAndConfigure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SaleProfile>();
            });

            return new AutoMapper.Mapper(config);
        }
    }
}