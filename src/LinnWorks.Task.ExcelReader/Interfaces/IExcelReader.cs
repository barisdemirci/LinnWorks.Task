using System.IO;
using LinnWorks.Task.Dtos.Excel;

namespace LinnWorks.Task.ExcelReader.Interfaces
{
    public interface IExcelReader
    {
        IWorkbook ReadExcel(string fileName, Stream fileStream);
    }
}