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
        private int rowCount;
        public int RowCount
        {
            get
            {
                return rowCount;
            }
            private set
            {
                this.rowCount = value;
            }
        }

        public Manifest(Workbook wb)
            : base(wb)
        {
            headers = GetHeaders();
            rowCount = ws.Rows.Count;
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

        public Range GetCell(int row, int col)
        {
            return ws.Cells[row, col];
        }

        public string GetISRC(string albumTitle)
        {
            string firstISRC = "";

            for (int row = 2; row < RowCount; row++)
            {
                Range cell = GetCell(row, 2);
                string cellVal = (string)cell.Value.ToString();

                if (cellVal == null)
                {
                    continue;
                }
                
                if (cellVal.Equals(albumTitle))
                {
                    var value = GetCell(row, 4).Value;
                    firstISRC = value.ToString();
                    return firstISRC;
                }
            }

            return firstISRC;
        }

        public Int64 GetUPC(string albumTitle)
        {
            Int64 upc = 0;

            for (int row = 2; row < RowCount; row++)
            {
                Range cell = GetCell(row, 2);
                string cellVal = (string)cell.Value.ToString();

                if (cellVal == null)
                {
                    continue;
                }
                //Console.WriteLine("Cell Value: {0}", cellVal);
                if (cellVal.Equals(albumTitle))
                {
                    var value = GetCell(row, 3).Value;
                    upc = (Int64)value;
                    break;
                }
            }

            return upc;
        }
    }
}
