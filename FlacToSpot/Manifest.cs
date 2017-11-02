using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Spotifyify
{
    /// <summary>
    /// Abstraction for manifest file
    /// </summary>
    class Manifest : ExcelFile
    {
        /// <summary>
        /// Field used to indicate column in Manifest which contains the starting ISRC
        /// </summary>
        private int ISRCcol;

        /// <summary>
        /// Field used to indicate column in Manifest which contains the UPC number
        /// </summary>
        private int UPCcol;

        /// <summary>
        /// Field used to indicate column in Manifest which contains the album title
        /// </summary>
        private int titleCol;

        /// <summary>
        /// Used for prompt if album in metadata doesn't match any manifest albums
        /// Key is album name, value will be row in manifest that the album name is in
        /// </summary>
        private Dictionary<string, int> albumTitleDict;

        /// <summary>
        /// Used to keep track of current album title either found in Manifest automatically or picked in AlbumPickerForm
        /// </summary>
        private string manifestAlbumTitle;

        /// <summary>
        /// Used to keep track of row count of manifest
        /// </summary>
        private int rowCount;

        /// <summary>
        /// Initializes a new instance of the Manifest class
        /// </summary>
        /// <param name="wb"></param>
        public Manifest(Workbook wb)
            : base(wb)
        {
            ISRCcol = 0;
            UPCcol = 0;
            titleCol = 0;
            
            CheckHeaders();
            rowCount = ws.UsedRange.Rows.Count;
        }

        /// <summary>
        /// Gets or sets row count of manifest
        /// </summary>
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
        /// Assumed that GetISRC will be called after GetUPC, we should have the dictionary and the current album title
        /// </summary>
        /// <returns>First ISRC for album, will return 0 if couldn't find ISRC or album title in manifest</returns>
        public string GetISRC()
        {
            string firstISRC = "";

            if (ISRCcol == 0 || titleCol == 0)
            {
                return firstISRC;
            }

            //Check if we have a manifest album title already (which we should)
            if (!string.IsNullOrEmpty(manifestAlbumTitle))
            {
                int row = albumTitleDict[manifestAlbumTitle];
                Range cell = GetCell(row, ISRCcol);
                return (string)cell.Value;
            }
            //Something's wrong, just open up the album picker
            else
            {
                manifestAlbumTitle = GetAlbumTitleFromPickerForm();
                if (!string.IsNullOrEmpty(manifestAlbumTitle))
                {
                    Range cell = GetCell(albumTitleDict[manifestAlbumTitle], UPCcol);
                    return (string)cell.Value;
                }
                //We tried
                else
                {
                    return firstISRC;
                }
            }
        }

        /// <summary>
        /// Gets UPC from manifest
        /// </summary>
        /// <param name="albumTitle"></param>
        /// <returns>Int64 because UPC is usually pretty long, will return 0 if couldn't find UPC or album title in manifest</returns>
        public Int64 GetUPC(string albumTitle)
        {
            Int64 upc = 0;
            manifestAlbumTitle = "";

            // If we couldn't find the right columns in the manifest, just return 0
            if (UPCcol == 0 || titleCol == 0)
            {
                return upc;
            }

            // If we have a dictionary already, and it contains our album title, return the UPC cell with that album title
            if (albumTitleDict != null && albumTitleDict.Keys.Contains<string>(albumTitle))
            {
                //Set currentAlbumTitle for ISRC use
                manifestAlbumTitle = albumTitle;

                Range cell = GetCell(albumTitleDict[albumTitle], UPCcol);
                return (Int64)cell.Value;
            }

            //If we havent made the dictionary yet, lets iterate through the rows
            if (albumTitleDict == null)
            {
                albumTitleDict = new Dictionary<string, int>();

                //Iterate through the rows and construct dictionary at same time
                for (int row = 2; row <= RowCount; row++)
                {
                    //Get album title from manifest
                    Range cell = GetCell(row, titleCol);
                    string albumCellVal = (string)cell.Value as string;

                    if (albumCellVal == null)
                    {
                        continue;
                    }

                    //Check if this row pertains to our current album
                    if (albumCellVal.Equals(albumTitle))
                    {
                        //Grab UPC
                        var value = GetCell(row, UPCcol).Value;
                        upc = (Int64)value;

                        //Pull out album title for ISRC use
                        manifestAlbumTitle = GetCell(row, titleCol).Value.ToString();
                    }
                    //Add dictionary for manual album selection prompt later
                    try
                    {
                        albumTitleDict.Add(albumCellVal, row);
                    }
                    catch (ArgumentException)
                    {
                        //already in dictionary
                    }
                }
            }

            //If we found right album title, return upc
            if (upc != 0)
            {
                return upc;
            }

            //Else lets get the Album title picker form opened
            manifestAlbumTitle = GetAlbumTitleFromPickerForm();

            //If we found the album title from the picker form, then return the upc
            if (!string.IsNullOrEmpty(manifestAlbumTitle))
            {
                Range cell = GetCell(albumTitleDict[manifestAlbumTitle], UPCcol);
                return (Int64)cell.Value;
            }
            //Else return 0
            else
            {
                return upc;
            }
        }

        /// <summary>
        /// Helper method used to check whether the manifest has the appropriate headers
        /// </summary>
        private void CheckHeaders()
        {
            int count = ws.UsedRange.Columns.Count;

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
                MessageBox.Show("Unable to find Album Titles column in UPC/ISRC file. Header of column must contain 'Title'.\n" +
                "UPC and ISRCs will be left blank in metadata file", "Warning");
            }
            else
            {
                if (ISRCcol == 0)
                {
                    MessageBox.Show("Unable to find Beginning ISRCs column in UPC/ISRC file. Header of column must contain 'ISRC'.\n" + 
                    "ISRCs will be left blank in metadata file", "Warning");
                }
                if (UPCcol == 0)
                {
                    MessageBox.Show("Unable to find UPC column in UPC/ISRC file. Header of column must contain 'UPC'.\n" + 
                    "UPCs will be left blank in metadata file", "Warning");
                }
            }
        }

        /// <summary>
        /// Helper Method for bringing up AlbumPickForm
        /// </summary>
        /// <returns>Returns the Album Title selected from the form, or empty string if nothing was picked</returns>
        private string GetAlbumTitleFromPickerForm()
        {
            //Prompt
            MessageBox.Show("Could not automatically find Album title in manifest.\n\nPlease select album title from list.");

            using (AlbumPickForm albumPickForm = new AlbumPickForm(albumTitleDict.Keys.ToList<string>()))
            {
                var result = albumPickForm.ShowDialog();

                //If we came back with a selected album title
                if (result == DialogResult.OK)
                {
                    //Set currentAlbumTitle for ISRC use
                    return albumPickForm.selectedAlbumTitle;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
