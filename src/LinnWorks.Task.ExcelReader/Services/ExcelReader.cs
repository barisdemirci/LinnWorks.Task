using DocumentFormat.OpenXml.Packaging;
using LinnWorks.Task.ExcelReader.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;
using LinnWorks.Task.Dtos.Excel;
using System.Globalization;

namespace LinnWorks.Task.ExcelReader.Services
{
    public class ExcelReader : IExcelReader
    {
        public IWorkbook ReadExcel(string fileName, Stream fileStream)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            var workbook = new WorkbookDto()
            {
                Filename = fileName
            };

            using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileStream, false))
            {
                WorkbookPart workbookPart = document.WorkbookPart;
                SharedStringTablePart stringTableParts =
                    workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

                CellFormats cellFormats = workbookPart.WorkbookStylesPart.Stylesheet.CellFormats;
                NumberingFormats numberingFormats = workbookPart.WorkbookStylesPart.Stylesheet.NumberingFormats;

                foreach (WorksheetPart worksheetPart in workbookPart.WorksheetParts)
                {
                    var Worksheet = new WorksheetDto
                    {
                        SheetName = workbookPart.Workbook.Descendants<Sheet>()
                            .FirstOrDefault(x => x.Id?.Value == workbookPart.GetIdOfPart(worksheetPart))
                            .Name
                    };

                    foreach (SheetData sheetData in worksheetPart.Worksheet.Elements<SheetData>())
                    {
                        foreach (Row row in sheetData.Elements<Row>())
                        {
                            var Row = new ExcelRowDto { Key = int.Parse(row.RowIndex) };

                            foreach (Cell cell in row.Elements<Cell>())
                            {
                                var Column = new ExcelColumnDto { Key = ParseColumnKey(cell.CellReference) };

                                if (cell.DataType != null && cell.DataType == CellValues.SharedString)
                                {
                                    Column.CellValue =
                                        stringTableParts.SharedStringTable.ElementAt(int.Parse(cell.CellValue.Text))
                                            .InnerText;
                                }
                                else if (cell.StyleIndex != null && cell.CellValue != null && numberingFormats != null)
                                {
                                    var cellFormat =
                                        (CellFormat)cellFormats.ElementAt(Convert.ToInt32(cell.StyleIndex.Value));

                                    if (cellFormat.NumberFormatId != null)
                                    {
                                        NumberingFormat numberingFormat =
                                            numberingFormats.Cast<NumberingFormat>()
                                                .SingleOrDefault(
                                                    f => f.NumberFormatId.Value == cellFormat.NumberFormatId.Value);

                                        if (numberingFormat != null &&
                                            (numberingFormat.FormatCode.Value.Contains("mm") ||
                                             numberingFormat.FormatCode.Value.Contains("dd") ||
                                             numberingFormat.FormatCode.Value.Contains("yyyy")))
                                        {
                                            Column.CellValue = FromOaDate(double.Parse(cell.CellValue?.Text));
                                        }
                                        else
                                        {
                                            Column.CellValue = cell.CellValue?.Text;
                                        }
                                    }
                                }
                                else
                                {
                                    Column.CellValue = cell.CellValue?.Text;
                                }

                                if (Column.CellValue != null)
                                {
                                    ((List<ExcelColumnDto>)Row.Columns).Add(Column);
                                }
                            }

                            ((List<ExcelRowDto>)Worksheet.Rows).Add(Row);
                        }
                    }

                    ((List<WorksheetDto>)workbook.Worksheets).Add(Worksheet);
                }
            }

            return workbook;
        }

        private static string ParseColumnKey(string identifier)
        {
            return identifier.TakeWhile(c => !char.IsDigit(c)).Aggregate(string.Empty, (current, c) => current + c);
        }

        private static string FromOaDate(double days)
        {
            return new DateTime(1900, 1, 1).AddDays(days - 2).ToString(CultureInfo.InvariantCulture);
        }
    }
}
