using System;
using FluentAssertions;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.ExcelReader.Interfaces;
using LinnWorks.Task.ExcelReader.Services;
using Xunit;

namespace LinnWorks.Task.ExcelReader.Tests
{
    public class CSVReaderTests
    {
        private readonly CSVReader excelReader;

        public CSVReaderTests()
        {
            excelReader = new CSVReader();
        }

        [Fact]
        public void CSVReader_ReadDocument_Returns100000Rows()
        {
            // arrange
            string path = "100000 Sales Records.csv";

            // act
            var result = excelReader.ReadDocument<SaleDto>(path);

            // assert
            result.Count.Should().Be(100000);
        }
    }
}