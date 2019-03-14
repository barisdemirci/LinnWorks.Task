using System;
using LinnWorks.Task.Core.Network;
using LinnWorks.Task.FrontEnd.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinnWorks.Task.FrontEnd.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IImportFileService, ImportFileService>();
        }

        public static void AddHttpClientWrapper(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.AddSingleton<IHttpClientWrapper>(provider => new HttpClientWrapper(configuration));
        }
    }
}