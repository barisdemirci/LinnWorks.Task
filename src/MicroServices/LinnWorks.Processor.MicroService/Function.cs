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
                S3 s3 = new S3(S3Client);
                StreamReader reader = await s3.ReadObjectDataAsync(value);
                CSVReader csvReader = new CSVReader();
                List<SaleDto> sales = csvReader.ReadDocument<SaleDto>(reader);
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

        private List<Country> Countries;
        private List<ItemType> ItemTypes;
        private List<OrderPriority> OrderPriorities;
        private List<Region> Regions;
        private List<SalesChannel> SalesChannels;

        private void LoadData()
        {
            Countries = dbContext.Countries.ToList();
            ItemTypes = dbContext.ItemTypes.ToList();
            Regions = dbContext.Regions.ToList();
            OrderPriorities = dbContext.OrderPriorities.ToList();
            SalesChannels = dbContext.SalesChannels.ToList();
        }

        private Sale BuildObject(SaleDto dto)
        {
            return new Sale()
            {
                Country = Countries.FirstOrDefault(x => x.CountryName == dto.Country.CountryName),
                ItemType = ItemTypes.FirstOrDefault(x => x.ItemTypeName == dto.ItemType.ItemTypeName),
                OrderDate = dto.OrderDate,
                OrderPriority = OrderPriorities.FirstOrDefault(x => x.OrderPriorityName == dto.OrderPriority.OrderPriorityName),
                Region = Regions.FirstOrDefault(x => x.RegionName == dto.Region.RegionName),
                OrderID = dto.OrderID,
                SalesChannel = SalesChannels.FirstOrDefault(x => x.SalesChannelName == dto.SalesChannel.SalesChannelName),
                ShipDate = dto.ShipDate,
                TotalCost = dto.TotalCost,
                TotalProfit = dto.TotalProfit,
                TotalRevenue = dto.TotalRevenue,
                UnitCost = dto.UnitCost,
                UnitPrice = dto.UnitPrice,
                UnitSold = dto.UnitSold
            };
        }
    }
}