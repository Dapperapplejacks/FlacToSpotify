using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace FlacToSpot
{
    class ExcelHandler
    {
        private Microsoft.Office.Interop.Excel.Application xlApp;
        private Workbook xlWorkbook;

        public ExcelHandler()
        {
            xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed\nExiting application", "Excel Error");
                throw new Exception();
            }
        }

    }
}
