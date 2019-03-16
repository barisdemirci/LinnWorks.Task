using System.Collections.Generic;
using System.IO;

namespace LinnWorks.Task.ExcelReader.Interfaces
{
    public interface IExcelReader
    {
        List<T> ReadDocument<T>(StreamReader reader) where T : class;
    }
}