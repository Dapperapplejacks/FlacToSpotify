using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FlacToSpot
{
    class ExcelFile
    {
        Workbook wb;

        public ExcelFile(Workbook wb)
        {
            this.wb = wb;
        }
    }
}
