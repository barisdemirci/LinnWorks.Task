using System;
using System.Collections.Generic;
using System.Text;
using LinnWorks.Task.Core.DependencyInjection;
using LinnWorks.Task.Services.Sales;
using Microsoft.Extensions.DependencyInjection;

namespace LinnWorks.Task.Services
{
    public class ServicesRegistration : IServiceRegistration
    {
        public void Register(IServiceCollection services)
        {
            services.AddTransient<ISaleService, SaleService>();
        }
    }
}