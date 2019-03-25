using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using LinnWorks.Task.ExcelReader.Interfaces;
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
                    CSVSaleDto item = new CSVSaleDto();
                    item.RegionName = values[0];
                    item.CountryName = values[1];
                    item.ItemTypeName = values[2];
                    item.SalesChannelName = values[3];
                    item.OrderPriorityName = values[4];
                    item.OrderDate = ParseDateTime(values[5]);
                    item.OrderID = int.Parse(values[6]);
                    item.ShipDate = ParseDateTime(values[7]);
                    item.UnitSold = int.Parse(values[8]);
                    item.UnitPrice = ParseDecimal(values[9]);
                    item.UnitCost = ParseDecimal(values[10]);
                    item.TotalRevenue = ParseDecimal(values[11]);
                    item.TotalCost = ParseDecimal(values[12]);
                    item.TotalProfit = ParseDecimal(values[13]);
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

        private decimal ParseDecimal(string value)
        {
            decimal result;
            decimal.TryParse(value, out result);
            return result;
        }
    }
}