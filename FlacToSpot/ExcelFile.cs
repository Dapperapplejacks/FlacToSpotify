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
        protected Sheets worksheets;
        //Only working with 1 worksheet
        protected Worksheet ws;
        

        public ExcelFile()
        {
            wb = new Workbook();
        }

        public ExcelFile(Workbook wb)
        {
            this.wb = wb;
            worksheets = wb.Worksheets;
            ws = (Worksheet)worksheets.get_Item(1);
        }

        public void CleanUp()
        {
            Marshal.ReleaseComObject(ws);
            ws = null;
            Marshal.ReleaseComObject(worksheets);
            worksheets = null;
            Marshal.ReleaseComObject(wb);
            wb = null;
            
        }

    }
}
