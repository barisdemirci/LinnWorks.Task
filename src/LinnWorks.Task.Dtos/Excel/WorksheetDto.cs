using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Dtos.Excel
{
    public class WorksheetDto
    {
        public WorksheetDto()
        {
            Rows = new List<ExcelRowDto>();
        }

        public IEnumerable<ExcelRowDto> Rows { get; set; }

        public string SheetName { get; set; }
    }
}