using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace FlacToSpot
{
    class Manifest : ExcelFile
    {

        private List<string> headers;

        public Manifest(Workbook wb)
            : base(wb)
        {
            headers = GetHeaders();
        }

        private List<string> GetHeaders()
        {

            int count = ws.UsedRange.Columns.Count;
            List<string> headers = new List<string>(count);

            for (int i = 1; i < count; i++)
            {
                if (ws.Cells[1, i].Value != null)
                {

                    string header = ws.Cells[1, i].Value;
                    Console.WriteLine("Header {0}: {1}", i, header);

                    headers.Add(header);
                }
            }

            return headers;

        }

        public Range GetColumn(int col)
        {
            return ws.Range[col, ws.Rows.Count];
        }
    }
}
