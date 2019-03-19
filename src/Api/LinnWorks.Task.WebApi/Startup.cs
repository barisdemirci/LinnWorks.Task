using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.SecretsManager;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Entities;
using LinnWorks.Task.Mapper;
using LinnWorks.Task.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinnWorks.Task.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddServices();
            services.AddSingleton(AutoMapperFactory.CreateAndConfigure());
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<DbContext>(sp => sp.GetService<ApplicationDbContext>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            var options = builder.Build().GetAWSOptions();
            IAmazonSecretsManager client = options.CreateServiceClient<IAmazonSecretsManager>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var context = new ApplicationDbContext(client))
                {
                    context.Database.EnsureCreated();
                    InsertTestData(context);
                    context.SaveChanges();
                }
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCors("CorsPolicy");
        }

        private void InsertTestData(ApplicationDbContext context)
        {
            if (!context.Countries.Any())
            {
                List<CountryDto> countries = TestDataGenerator.TestDataGenerator.GenerateCountries();
                foreach (var country in countries)
                {
                    Country newCountry = new Country()
                    {
                        CountryCode = country.CountryCode,
                        CountryName = country.CountryName
                    };
                    context.Countries.Add(newCountry);
                }
            }

            if (!context.Regions.Any())
            {
                List<RegionDto> regions = TestDataGenerator.TestDataGenerator.GenerateRegions();
                foreach (var item in regions)
                {
                    Region newItem = new Region()
                    {
                        RegionName = item.RegionName
                    };
                    context.Regions.Add(newItem);
                }
            }

            if (!context.OrderPriorities.Any())
            {
                List<OrderPriorityDto> orderPriorities = TestDataGenerator.TestDataGenerator.GenerateOrderPriorities();
                foreach (var item in orderPriorities)
                {
                    OrderPriority newItem = new OrderPriority()
                    {
                        OrderPriorityName = item.OrderPriorityName
                    };
                    context.OrderPriorities.Add(newItem);
                }
            }

            if (!context.SalesChannels.Any())
            {
                List<SalesChannelDto> salesChannels = TestDataGenerator.TestDataGenerator.GenerateSalesChannels();
                foreach (var item in salesChannels)
                {
                    SalesChannel newItem = new SalesChannel()
                    {
                        SalesChannelName = item.SalesChannelName
                    };
                    context.SalesChannels.Add(newItem);
                }
            }

            if (!context.ItemTypes.Any())
            {
                List<ItemTypeDto> itemTypes = TestDataGenerator.TestDataGenerator.GenerateItemTypes();
                foreach (var item in itemTypes)
                {
                    ItemType newItem = new ItemType()
                    {
                        ItemTypeName = item.ItemTypeName
                    };
                    context.ItemTypes.Add(newItem);
                }
            }
        }
    }
}