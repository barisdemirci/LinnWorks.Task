using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using LinnWorks.Task.ExcelReader.Interfaces;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.Dtos;

namespace LinnWorks.Task.ExcelReader.Services
{
    public class CSVReader : IExcelReader
    {
        public List<T> ReadDocument<T>(StreamReader reader) where T : class
        {
            List<T> result = new List<T>();
            using (reader)
            {
                bool firstLine = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (firstLine)
                    {
                        firstLine = false;
                        continue;
                    };

                    var values = line.Split(',');
                    SaleDto item = new SaleDto();
                    item.Region = new RegionDto() { RegionName = values[0] };
                    item.Country = new CountryDto() { CountryName = values[1] };
                    item.ItemType = new ItemTypeDto { ItemTypeName = values[2] };
                    item.SalesChannel = new SalesChannelDto() { SalesChannelName = values[3] };
                    item.OrderPriority = new OrderPriorityDto() { OrderPriorityName = values[4] };
                    item.OrderDate = ParseDateTime(values[5]);
                    item.OrderID = int.Parse(values[6]);
                    item.ShipDate = ParseDateTime(values[7]);
                    item.UnitSold = int.Parse(values[8]);
                    decimal unitPrice;
                    if (decimal.TryParse(values[9], out unitPrice))
                    {
                        item.UnitPrice = unitPrice;
                    }
                    decimal unitCost;
                    if (decimal.TryParse(values[10], out unitCost))
                    {
                        item.UnitCost = unitCost;
                    }
                    decimal totalRevenue;
                    if (decimal.TryParse(values[11], out totalRevenue))
                    {
                        item.TotalRevenue = totalRevenue;
                    }
                    decimal totalCost;
                    if (decimal.TryParse(values[12], out totalCost))
                    {
                        item.TotalCost = totalCost;
                    }
                    decimal totalProfit;
                    if (decimal.TryParse(values[13], out totalProfit))
                    {
                        item.TotalProfit = totalProfit;
                    }
                    result.Add(item as T);
                }
            }

            return result;
        }

        private DateTime ParseDateTime(string date)
        {
            DateTime result;
            DateTime.TryParseExact(date, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out result);
            return result;
        }
    }
}