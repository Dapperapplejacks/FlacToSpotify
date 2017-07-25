using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;

namespace FlacToSpot
{
    class Metadata : ExcelFile
    {
        private List<string> headers1;

        private List<string> headers2;

        const string fileName = "metadata.xlsx";
        const string sheetName = "meta";
        const int fontSize = 10;
        const int columnWidth = 13;


        public Metadata(Workbook wb) : base(wb)
        {
            this.wb = wb;
            ws.Name = sheetName;
            ws.Cells.Font.Size = fontSize;
            ws.Cells.ColumnWidth = columnWidth;

            headers1 = new List<string>()
            {
                "Album Data", "Track Data"
            };

            headers2 = new List<string>()
            {
                "upc", "label", "title", "version", "artist", "genre", "original release date\nYYYY, YYYY-MM or YYYY-MM-DD",
                "coverart file path", "pline", "cline", 
                
                "disc no", "track no", "ISRC", "title", "version", "artist", 
                "Parental warning", "pline", "audio file path", "start date\nYYYY-MM-DD", "end date\nYYYY-MM-DD",
                "territories", "territories exclude", "featured artist", "composer", "lyricist", "arranger", "producer",
                "remixer"
            };
        }

        public void PopulateSheet(Album album, Manifest manifest)
        {
            SetHeaders();
            
        }

        private void SetHeaders()
        {
            //Album Data Header
            ws.Cells[1, 1] = headers1[0];
            Range albumData = ws.get_Range("a1", "j1");
            albumData.Merge(false);
            albumData.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            albumData.VerticalAlignment = XlVAlign.xlVAlignBottom;

            ws.Range["A1", "J1"].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
            ws.Range["A2", "J2"].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);

            //Track Data Header
            ws.Cells[1, 11] = headers1[1];
            Range trackData = ws.get_Range("k1", "ac1");
            trackData.Merge(false);
            trackData.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            trackData.VerticalAlignment = XlVAlign.xlVAlignBottom;

            ws.Range["K1", "AC1"].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);
            ws.Range["K2", "AC2"].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

            //Subheaders
            for (int i = 0; i < headers2.Count(); i++)
            {
                try
                {
                    ws.Cells[2, i+1] = headers2[i];
                    Borders borders = ws.Cells[2, i+1].Borders;
                    borders.LineStyle = XlLineStyle.xlContinuous;
                    borders.Weight = 2d;
                }
                catch (COMException ex)
                {
                    throw new Exception("COMException, code: " + ex.ErrorCode);
                }
            }

            



        }

        public void SaveFile(string path)
        {
            try
            {
                string namePath = Path.Combine(path, fileName);
                wb.SaveAs(namePath, Type.Missing, Type.Missing, Type.Missing, false);

                CleanUp();
            }

            catch (Exception ex)
            {
                throw new Exception("Error Saving Metadata File");
            }
        }

    }
}
