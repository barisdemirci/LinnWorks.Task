using Amazon.S3;
using LinnWorks.Queue.MicroService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LinnWorks.Queue.MicroService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>();
            services.AddAWSService<IAmazonS3>();
        }
    }
}