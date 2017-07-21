using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace FlacToSpot
{
    class Metadata : ExcelFile
    {
        private List<string> headers1;

        private List<string> headers2;

        const string fileName = "metadata";
        const string sheetName = "meta";


        public Metadata()
        {
            wb = new Workbook();
            ws = new Worksheet();
            ws.Name = sheetName;

            headers1 = new List<string>()
            {
                "Album Data", "Track Data"
            };

            headers2 = new List<string>()
            {
                "upc", "label", "title", "version", "artist", "genre", "original release date\nYYYY, YYYY-MM or YYYY-MM-DD",
                "coverart file path", "pline", "cline", "disc no", "track no", "ISRC", "title", "version", "artist", 
                "Parental warning", "pline", "audio file path", "start date\nYYYY-MM-DD", "end date\nYYYY-MM-DD",
                "territories", "territories exclude", "featured artist", "composer", "lyricist", "arranger", "producer",
                "remixer"
            };
        }

        public void SaveFile()
        {

        }

    }
}
