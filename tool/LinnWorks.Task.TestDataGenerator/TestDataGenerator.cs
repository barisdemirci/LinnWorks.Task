using System;
using System.Collections.Generic;
using System.IO;
using LinnWorks.Task.Dtos;

namespace LinnWorks.Task.TestDataGenerator
{
    public static class TestDataGenerator
    {
        public static List<CountryDto> GenerateCountries()
        {
            string text = File.ReadAllText("Data/country.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<CountryDto>>(text);
        }

        public static List<RegionDto> GenerateRegions()
        {
            string text = File.ReadAllText("Data/region.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<RegionDto>>(text);
        }


        public static List<ItemTypeDto> GenerateItemTypes()
        {
            string text = File.ReadAllText("Data/itemType.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<ItemTypeDto>>(text);
        }

        public static List<OrderPriorityDto> GenerateOrderPriorities()
        {
            string text = File.ReadAllText("Data/orderPriority.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderPriorityDto>>(text);
        }

        public static List<SalesChannelDto> GenerateSalesChannels()
        {
            string text = File.ReadAllText("Data/salesChannel.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<SalesChannelDto>>(text);
        }
    }
}