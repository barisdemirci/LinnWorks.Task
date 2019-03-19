using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace LinnWorks.Task.Repositories.Sales
{
    public class SaleRepositoriesRegistration : Core.DependencyInjection.IServiceRegistration
    {
        public void Register(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddTransient<ISaleRepository, SaleRepository>();
        }
    }
}