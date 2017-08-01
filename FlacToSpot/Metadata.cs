using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace FlacToSpot
{
    /// <summary>
    /// Abstraction of metadata file
    /// </summary>
    class Metadata : ExcelFile
    {
        /// <summary>
        /// First row headers
        /// </summary>
        private readonly List<string> headers1 = new List<string>()
            {
                "Album Data", "Track Data"
            };
        /// <summary>
        /// Second row headers
        /// </summary>
        private readonly List<string> headers2 = new List<string>()
            {
                "upc", "label", "title", "version", "artist", "genre", "original release date\nYYYY, YYYY-MM or YYYY-MM-DD",
                "coverart file path", "pline", "cline", 
                
                "disc no", "track no", "ISRC", "title", "version", "artist", 
                "Parental warning", "pline", "audio file path", "start date\nYYYY-MM-DD", "end date\nYYYY-MM-DD",
                "territories", "territories exclude", "featured artist", "composer", "lyricist", "arranger", "producer",
                "remixer"
            };

        #region Constants

        const string fileName = "metadata.xlsx";
        const string sheetName = "meta";
        const int fontSize = 10;
        const int columnWidth = 13;

        const string version = "";
        const string label = "Orange Mountain Music";
        const string cline = "";
        const string parentalWarning = "not-explicit";

        #endregion

        public Metadata(Workbook wb) : base(wb)
        {
            this.wb = wb;
            ws.Name = sheetName;
            ws.Cells.Font.Size = fontSize;
            ws.Cells.ColumnWidth = columnWidth;
        }

        /// <summary>
        /// Fill out all possible cells in metadata file
        /// </summary>
        /// <param name="album"></param>
        /// <param name="manifest"></param>
        /// <param name="startEndDates"></param>
        public void PopulateSheet(Album album, Manifest manifest, string[] startEndDates)
        {
            try
            {
                SetHeaders();
                FlacFile[] flacs = album.GetFlacs();

                //Album data
                Int64 upc = album.GetUPC(manifest);

                if (upc == 0)
                {
                    MessageBox.Show("Unable to find Album Title '" + album.GetAlbumTitle() + "' in UPC/ISRC file." +
                    "UPC and ISRCs will be left blank in metadata file", "Warning");
                }

                string albumTitle = album.GetAlbumName();
                string artist = album.GetArtists();
                string genre = album.GetGenre();
                string releaseDate = album.GetReleaseDate();
                string coverartFilePath = album.CoverArt.FileName;
                string pline = DateTime.Now.Year + " " + label;

                string[] ISRCs = album.GetAllISRCs(manifest);

                //Iterate through rows
                for (int row = 3; row < album.GetFlacs().Length + 3; row++)
                {
                    int col = 1;

                    //Album Data
                    ws.Cells[row, col] = upc;
                    ((Range)ws.Cells[row, col++]).NumberFormat = "0";
                    ws.Cells[row, col++] = label;
                    ws.Cells[row, col++] = albumTitle;
                    ws.Cells[row, col++] = version;
                    ws.Cells[row, col++] = artist;
                    ws.Cells[row, col++] = genre;
                    ws.Cells[row, col++] = releaseDate;
                    ws.Cells[row, col++] = coverartFilePath;
                    ws.Cells[row, col++] = pline;
                    ws.Cells[row, col++] = cline;

                    //Track data
                    TagLib.Tag tag = flacs[row - 3].Tag;

                    //Disc no
                    ws.Cells[row, col++] = (int)tag.Disc;
                    //Track no
                    ws.Cells[row, col++] = (int)tag.Track;
                    //ISRC
                    ws.Cells[row, col++] = ISRCs[row - 3];
                    //Title
                    ws.Cells[row, col++] = tag.Title;
                    //Version
                    ws.Cells[row, col++] = version;
                    //Artist(s)
                    ws.Cells[row, col++] = String.Join(", ", tag.Performers);
                    //Parental Warning
                    ws.Cells[row, col++] = parentalWarning;
                    //Pline
                    ws.Cells[row, col++] = "";
                    //Audio File Path
                    ws.Cells[row, col++] = flacs[row - 3].FileName;
                    //Start date
                    ws.Cells[row, col] = startEndDates[0];
                    ((Range)ws.Cells[row, col++]).NumberFormat = "yyyy-mm-dd";
                    //End Date
                    ws.Cells[row, col] = startEndDates[1];
                    ((Range)ws.Cells[row, col++]).NumberFormat = "yyyy-mm-dd";
                    //Territories
                    ws.Cells[row, col++] = "";
                    //Territories exclude
                    ws.Cells[row, col++] = "";
                    //Featured Artist
                    ws.Cells[row, col++] = "";
                    //Composer(s)
                    ws.Cells[row, col++] = String.Join(", ", tag.Composers);
                    //Lyricist
                    ws.Cells[row, col++] = "";
                    //Arranger
                    ws.Cells[row, col++] = "";
                    //Producer
                    ws.Cells[row, col++] = "";
                    //Remixer
                    ws.Cells[row, col++] = "";

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        /// <summary>
        /// Put headers in file cells
        /// </summary>
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

        /// <summary>
        /// Saves the metadata file in specified path
        /// </summary>
        /// <param name="path"></param>
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
