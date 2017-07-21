using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;


namespace FlacToSpot
{

    
    class ExcelFile
    {
        protected Workbook wb;
        //Only working with 1 worksheet
        protected Worksheet ws;
        

        public ExcelFile()
        {
            wb = new Workbook();
        }

        public ExcelFile(Workbook wb)
        {
            this.wb = wb;
            ws = (Worksheet)wb.Worksheets.get_Item(1);

        }

    }
}
