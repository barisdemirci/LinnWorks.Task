using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinnWorks.Task.Services.Sales;
using Microsoft.Extensions.DependencyInjection;


namespace LinnWorks.Task.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<ISaleService, SaleService>();
        }
    }
}