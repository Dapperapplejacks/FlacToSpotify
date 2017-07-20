using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace FlacToSpot
{
    class ExcelHandler
    {
        private Microsoft.Office.Interop.Excel.Application xlApp;
        

        public ExcelHandler()
        {
            xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed\nExiting application", "Excel Error");
                throw new Exception();
            }
        }

        public ExcelFile ReadFile(string path)
        {
            Workbook workbook = xlApp.Workbooks.Open(path);

            return null;
            
        }
    }
}
