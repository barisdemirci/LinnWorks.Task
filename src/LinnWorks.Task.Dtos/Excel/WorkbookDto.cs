using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LinnWorks.Task.Dtos.Excel
{
    public class WorkbookDto : IWorkbook
    {
        public WorkbookDto()
        {
            Worksheets = new List<WorksheetDto>();
        }

        public IEnumerable<WorksheetDto> Worksheets { get; set; }

        public string Filename { get; set; }

        public string Path { get; set; }

        public string GetValue(string worksheetName, int rowkey, string columnKey)
        {
            if (worksheetName == null) throw new ArgumentNullException(nameof(worksheetName));
            if (columnKey == null) throw new ArgumentNullException(nameof(columnKey));

            return
                Worksheets?.FirstOrDefault(sheet => sheet.SheetName == worksheetName)?
                    .Rows?.FirstOrDefault(row => row.Key == rowkey)?
                    .Columns?.FirstOrDefault(column => column.Key == columnKey)?
                    .CellValue ?? string.Empty;
        }

        public IEnumerable<ExcelRowDto> GetDuplicateRowsByValue(string worksheetName, int rowkey, string columnKey)
        {
            if (worksheetName == null) throw new ArgumentNullException(nameof(worksheetName));
            if (columnKey == null) throw new ArgumentNullException(nameof(columnKey));

            string value = GetValue(worksheetName, rowkey, columnKey);

            return Worksheets.First(sheet => sheet.SheetName == worksheetName)
                .Rows.Where(row => row.Columns.Any(column => column.Key == columnKey && column.CellValue == value));
        }

        public bool CellExists(string worksheetName, int rowKey, string columnKey)
        {
            if (worksheetName == null) throw new ArgumentNullException(nameof(worksheetName));
            if (columnKey == null) throw new ArgumentNullException(nameof(columnKey));

            return Worksheets.Any(sheet => sheet.SheetName == worksheetName) &&
                   Worksheets.First(sheet => sheet.SheetName == worksheetName).Rows.Any(row => row.Key == rowKey) &&
                   Worksheets.First(sheet => sheet.SheetName == worksheetName)
                       .Rows.First(row => row.Key == rowKey)
                       .Columns.Any(column => column.Key == columnKey);
        }

        public bool RowExists(string worksheetName, int rowKey)
        {
            if (worksheetName == null) throw new ArgumentNullException(nameof(worksheetName));

            return Worksheets.Any(sheet => sheet.SheetName == worksheetName) &&
                   Worksheets.First(sheet => sheet.SheetName == worksheetName).Rows.Any(row => row.Key == rowKey) &&
                   Worksheets.First(sheet => sheet.SheetName == worksheetName)
                       .Rows.First(row => row.Key == rowKey)
                       .Columns.Any();
        }

        public IEnumerable<KeyValuePair<string, string>> GetColumnIterator(string worksheetName, int rowKey)
        {
            if (worksheetName == null) throw new ArgumentNullException(nameof(worksheetName));

            IEnumerable<ExcelColumnDto> columns = new List<ExcelColumnDto>();

            try
            {
                columns = Worksheets.First(sheet => sheet.SheetName == worksheetName)
                    .Rows.First(row => row.Key == rowKey)
                    .Columns;
            }
            catch (Exception)
            {
                // ignored
            }

            foreach (var column in columns) yield return new KeyValuePair<string, string>(column.Key, column.CellValue);
        }

        public IEnumerable<KeyValuePair<int, string>> GetRowIterator(string worksheetName, string columnKey)
        {
            if (worksheetName == null) throw new ArgumentNullException(nameof(worksheetName));

            IEnumerable<ExcelRowDto> rows = new List<ExcelRowDto>();

            try
            {
                rows = Worksheets.First(sheet => sheet.SheetName == worksheetName).Rows;
            }
            catch (Exception)
            {
                // ignored
            }

            foreach (var row in rows)
            {
                KeyValuePair<int, string> rval;
                try
                {
                    rval = new KeyValuePair<int, string>(row.Key, row.Columns.FirstOrDefault(c => c.Key == columnKey).CellValue);
                }
                catch
                {
                    continue;
                }

                yield return rval;
            }
        }
    }
}