using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinnWorks.Task.Entities;
using LinnWorks.Task.Repositories.UnitOfWork;
using LinnWorks.Task.Services.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace LinnWorks.Task.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddTransient<ISaleService, SaleService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}