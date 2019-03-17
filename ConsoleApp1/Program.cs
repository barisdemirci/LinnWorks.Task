using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.Entities;
using LinnWorks.Task.ExcelReader.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CSVReader csvReader = new CSVReader();
            StreamReader reader = new StreamReader("100 Sales Records.csv");
            List<SaleDto> sales = csvReader.ReadDocument<SaleDto>(reader);
            ApplicationDbContext dbContext = new ApplicationDbContext();
            foreach (var item in sales)
            {
                Sale newSale = BuildObject(item, dbContext);
                dbContext.Sales.AddAsync(newSale);
            }
            dbContext.SaveChangesAsync();
            Console.ReadKey();
        }

        private static Sale BuildObject(SaleDto dto, ApplicationDbContext dbContext)
        {
            return new Sale()
            {
                Country = dbContext.Countries.FirstOrDefault(x => x.CountryName == dto.Country),
                ItemType = dbContext.ItemTypes.FirstOrDefault(x => x.ItemTypeName == dto.ItemTypes),
                OrderDate = dto.OrderDate,
                OrderPriority = dbContext.OrderPriorities.FirstOrDefault(x => x.OrderPriorityName == dto.OrderPriority),
                Region = dbContext.Regions.FirstOrDefault(x => x.RegionName == dto.Region),
                OrderID = dto.OrderID,
                SalesChannel = dbContext.SalesChannels.FirstOrDefault(x => x.SalesChannelName == dto.SalesChannel),
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
