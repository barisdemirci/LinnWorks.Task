using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using LinnWorks.AWS.Redis;
using LinnWorks.AWS.S3;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.Entities;
using LinnWorks.Task.ExcelReader.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LinnWorks.Processor.MicroService
{
    public class Function
    {
        private readonly ApplicationDbContext dbContext;

        IAmazonS3 S3Client { get; set; }

        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {
            S3Client = new AmazonS3Client(RegionEndpoint.EUCentral1);
            dbContext = new ApplicationDbContext();
        }

        /// <summary>
        /// Constructs an instance with a preconfigured S3 client. This can be used for testing the outside of the Lambda environment.
        /// </summary>
        /// <param name="s3Client"></param>
        public Function(IAmazonS3 s3Client)
        {
            S3Client = s3Client;
        }

        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an S3 event object and can be used 
        /// to respond to S3 notifications.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(S3Event evnt, ILambdaContext context)
        {
            var s3Event = evnt.Records?[0].S3;
            if (s3Event == null)
            {
                return null;
            }

            try
            {
                RedisDataAgent agent = new RedisDataAgent();
                string value = await agent.RightPopAsync("ExcelTask");
                context.Logger.LogLine($"Parsing of {value} file is started.");
                S3 s3 = new S3(S3Client);
                StreamReader reader = await s3.ReadObjectDataAsync(value);
                CSVReader csvReader = new CSVReader();
                List<CSVSaleDto> sales = csvReader.ReadDocument<CSVSaleDto>(reader);
                InsertMissingData(sales);
                LoadData();
                List<Sale> salesEntity = new List<Sale>();
                foreach (var item in sales.AsParallel())
                {
                    Sale newSale = BuildObject(item);
                    salesEntity.Add(newSale);
                }
                await dbContext.AddRangeAsync(salesEntity);
                await dbContext.SaveChangesAsync();
                await s3.DeleteFileASync(value);
                string returnValue = $"Parsing of {value} file is completed successfully!";
                context.Logger.LogLine(returnValue);
                return returnValue;
            }
            catch (Exception e)
            {
                context.Logger.LogLine(e.Message);
                context.Logger.LogLine(e.InnerException.Message);
                throw;
            }
        }

        private Dictionary<string, Country> Countries;
        private Dictionary<string, ItemType> ItemTypes;
        private Dictionary<string, OrderPriority> OrderPriorities;
        private Dictionary<string, Region> Regions;
        private Dictionary<string, SalesChannel> SalesChannels;

        private void LoadData()
        {
            Countries = dbContext.Countries.ToDictionary(x => x.CountryName);
            ItemTypes = dbContext.ItemTypes.ToDictionary(x => x.ItemTypeName);
            Regions = dbContext.Regions.ToDictionary(x => x.RegionName);
            OrderPriorities = dbContext.OrderPriorities.ToDictionary(x => x.OrderPriorityName);
            SalesChannels = dbContext.SalesChannels.ToDictionary(x => x.SalesChannelName);
        }

        private Sale BuildObject(CSVSaleDto dto)
        {
            return new Sale()
            {
                Country = Countries[dto.CountryName],
                ItemType = ItemTypes[dto.ItemTypeName],
                OrderDate = dto.OrderDate,
                OrderPriority = OrderPriorities[dto.OrderPriorityName],
                Region = Regions[dto.RegionName],
                OrderID = dto.OrderID,
                SalesChannel = SalesChannels[dto.SalesChannelName],
                ShipDate = dto.ShipDate,
                TotalCost = dto.TotalCost,
                TotalProfit = dto.TotalProfit,
                TotalRevenue = dto.TotalRevenue,
                UnitCost = dto.UnitCost,
                UnitPrice = dto.UnitPrice,
                UnitSold = dto.UnitSold
            };
        }

        private async void InsertMissingData(List<CSVSaleDto> sales)
        {
            List<string> countries = sales.Select(x => x.CountryName).Distinct().ToList();
            List<string> regions = sales.Select(x => x.RegionName).Distinct().ToList();
            List<string> itemTypes = sales.Select(x => x.ItemTypeName).Distinct().ToList();
            List<string> salesChannels = sales.Distinct().Select(x => x.SalesChannelName).Distinct().ToList();
            List<string> orderPriorities = sales.Select(x => x.OrderPriorityName).Distinct().ToList();

            var countryList = dbContext.Countries.Select(x => x.CountryName).Intersect(countries).ToList();
            countries.RemoveAll(x => countryList.Contains(x));
            List<Country> newCountryList = countries.Select(x => new Country { CountryName = x }).ToList();
            await dbContext.Countries.AddRangeAsync(newCountryList);

            var regionList = dbContext.Regions.Select(x => x.RegionName).Intersect(regions).ToList();
            regions.RemoveAll(x => regionList.Contains(x));
            List<Region> newRegionList = regions.Distinct().Select(x => new Region { RegionName = x }).ToList();
            await dbContext.Regions.AddRangeAsync(newRegionList);

            var itemTypeList = dbContext.ItemTypes.Select(x => x.ItemTypeName).Intersect(itemTypes).ToList();
            itemTypes.RemoveAll(x => itemTypeList.Contains(x));
            List<ItemType> newItemTypeList = itemTypes.Select(x => new ItemType { ItemTypeName = x }).ToList();
            await dbContext.ItemTypes.AddRangeAsync(newItemTypeList);

            var salesChannelList = dbContext.SalesChannels.Select(x => x.SalesChannelName).Intersect(salesChannels).ToList();
            salesChannels.RemoveAll(x => salesChannelList.Contains(x));
            List<SalesChannel> newSalesChannelList = salesChannels.Distinct().Select(x => new SalesChannel { SalesChannelName = x }).ToList();
            await dbContext.SalesChannels.AddRangeAsync(newSalesChannelList);

            var orderPriorityList = dbContext.OrderPriorities.Select(x => x.OrderPriorityName).Intersect(orderPriorities).ToList();
            orderPriorities.RemoveAll(x => orderPriorityList.Contains(x));
            List<OrderPriority> newOrderPriorityList = orderPriorities.Distinct().Select(x => new OrderPriority { OrderPriorityName = x }).ToList();
            await dbContext.OrderPriorities.AddRangeAsync(newOrderPriorityList);

            dbContext.SaveChanges();
        }
    }
}