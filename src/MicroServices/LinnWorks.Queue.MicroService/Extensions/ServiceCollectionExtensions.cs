using LinnWorks.Queue.MicroService.Services;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinnWorks.Queue.MicroService.Extensions
{

    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>();
            // services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("myredis.p5oyuf.0001.euc1.cache.amazonaws.com:6379"));
        }
    }
}