using System.Collections.Generic;
using System.IO;

namespace LinnWorks.Task.ExcelReader.Interfaces
{
    public interface IExcelReader
    {
        List<T> ReadDocument<T>(string path) where T : class;
    }
}