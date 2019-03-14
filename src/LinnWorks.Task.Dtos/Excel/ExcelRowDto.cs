using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Dtos.Excel
{
    public class ExcelRowDto
    {
        public ExcelRowDto()
        {
            Columns = new List<ExcelColumnDto>();
        }

        public IEnumerable<ExcelColumnDto> Columns { get; set; }

        public int Key { get; set; }
    }
}