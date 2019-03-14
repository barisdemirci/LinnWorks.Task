using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Dtos.Excel
{
    public interface IWorkbook
    {
        string GetValue(string worksheetName, int rowkey, string columnKey);

        IEnumerable<KeyValuePair<string, string>> GetColumnIterator(string worksheetName, int rowKey);

        IEnumerable<KeyValuePair<int, string>> GetRowIterator(string worksheetName, string columnKey);

        bool CellExists(string worksheetName, int rowKey, string columnKey);

        bool RowExists(string worksheetName, int rowKey);
    }
}