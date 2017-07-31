using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FlacToSpot
{
    /// <summary>
    /// Abstraction for manifest file
    /// </summary>
    class Manifest : ExcelFile
    {
        //private List<string> headers;
        private int ISRCcol;
        private int UPCcol;
        private int titleCol;

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
            ISRCcol = 0;
            UPCcol = 0;
            titleCol = 0;
            
            CheckHeaders();
            rowCount = ws.Rows.Count;
        }

        private void CheckHeaders()
        {

            int count = ws.UsedRange.Columns.Count;
            //List<string> headers = new List<string>(count);

            for (int i = 1; i < count; i++)
            {
                if (ws.Cells[1, i].Value != null)
                {

                    string header = ws.Cells[1, i].Value;
                    Console.WriteLine("Header {0}: {1}", i, header);

                    if (header.Contains("ISRC") || header.Contains("isrc"))
                    {
                        ISRCcol = i;
                    }
                    else if (header.Contains("UPC") || header.Contains("upc"))
                    {
                        UPCcol = i;
                    }
                    else if (header.Contains("Title") || header.Contains("title") || header.Contains("Name") || header.Contains("name"))
                    {
                        titleCol = i;
                    }
                }
            }

            if (titleCol == 0)
            {
                MessageBox.Show("Unable to find Album Titles in UPC/ISRC file. Header of column must contain 'Title'." +
                "UPC and ISRCs will be left blank in metadata file", "Warning");
            }
            else
            {
                if (ISRCcol == 0)
                {
                    MessageBox.Show("Unable to find Beginning ISRCs in UPC/ISRC file. Header of column must contain 'ISRC'." + 
                    "ISRCs will be left blank in metadata file", "Warning");
                }
                if (UPCcol == 0)
                {
                    MessageBox.Show("Unable to find UPC numbers in UPC/ISRC file. Header of column must contain 'UPC'." + 
                    "UPCs will be left blank in metadata file", "Warning");
                }
            }

            
            

        }

        /// <summary>
        /// Gets entire column
        /// </summary>
        /// <param name="col"></param>
        /// <returns>Range object representing column</returns>
        public Range GetColumn(int col)
        {
            return ws.Range[col, ws.Rows.Count];
        }

        /// <summary>
        /// Gets specific cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>Range object representing cell</returns>
        public Range GetCell(int row, int col)
        {
            return ws.Cells[row, col];
        }

        /// <summary>
        /// Manifest holds first ISRC for the album, first track will use this ISRC,
        /// Each track after will increment the previous ISRC by 1
        /// </summary>
        /// <param name="albumTitle"></param>
        /// <returns>First ISRC for album, will return 0 if couldn't find ISRC or album title in manifest</returns>
        public string GetISRC(string albumTitle)
        {
            string firstISRC = "";

            if (ISRCcol == 0 || titleCol == 0)
            {
                return firstISRC;
            }

            for (int row = 2; row < RowCount; row++)
            {
                //Get album title from manifest
                Range cell = GetCell(row, titleCol);
                string cellVal = (string)cell.Value.ToString();

                if (cellVal == null)
                {
                    continue;
                }
                
                //Check if this row pertains to our current album
                if (cellVal.Equals(albumTitle))
                {
                    //Grab first ISRC
                    var value = GetCell(row, ISRCcol).Value;
                    firstISRC = value.ToString();
                    return firstISRC;
                }
            }

            return firstISRC;
        }

        /// <summary>
        /// Gets UPC from manifest
        /// </summary>
        /// <param name="albumTitle"></param>
        /// <returns>Int64 because UPC is usually pretty long, will return 0 if couldn't find UPC or album title in manifest</returns>
        public Int64 GetUPC(string albumTitle)
        {
            Int64 upc = 0;

            if (UPCcol == 0 || titleCol == 0)
            {
                return upc;
            }

            for (int row = 2; row < RowCount; row++)
            {
                //Get album title from manifest
                Range cell = GetCell(row, titleCol);
                string cellVal = (string)cell.Value.ToString();

                if (cellVal == null)
                {
                    continue;
                }
                
                //Compare to current album title
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
