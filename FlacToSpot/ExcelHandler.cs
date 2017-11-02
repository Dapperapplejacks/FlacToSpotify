using Microsoft.Office.Interop.Excel;
using System;
using System.Runtime.InteropServices;

namespace Spotifyify
{
    /// <summary>
    /// Handles reading the UPC/ISRC manifest and creating the metadata file
    /// </summary>
    class ExcelHandler
    {
        /// <summary>
        /// Field for Excel app 
        /// </summary>
        private Microsoft.Office.Interop.Excel.Application xlApp;

        /// <summary>
        /// Field for all open workbooks
        /// </summary>
        private Workbooks workbooks;

        /// <summary>
        /// Path to manifest
        /// </summary>
        public string manifestPath;

        /// <summary>
        /// Field for manifest object
        /// </summary>
        public Manifest manifest;

        /// <summary>
        /// Initializes a new instance of the ExcelHandler class
        /// </summary>
        public ExcelHandler()
        {
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            workbooks = xlApp.Workbooks;

            if (xlApp == null)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Reads manifest excel file
        /// </summary>
        /// <param name="path">Path of manifest</param>
        /// <returns>Manifest object</returns>
        private Manifest ReadManifest(string path)
        {
            try
            {
                Workbook workbook = workbooks.Open(path, Type.Missing, true);
                manifest = new Manifest(workbook);

                Marshal.ReleaseComObject(workbook);
                workbook = null;

                return manifest;
            }
            catch (Exception)
            {
                throw new Exception("Error locating/reading UPC/ISRC manifest");
            }
        }

        /// <summary>
        /// Creates metadata excel file and saves it in destination folder
        /// </summary>
        /// <param name="album">Album to make metadata file for</param>
        /// <param name="startEndDates">Array of start and end dates taken from GUI</param>
        public void CreateMetaData(Album album, string[] startEndDates)
        {
            Metadata metadata = new Metadata(workbooks.Add());
            metadata.PopulateSheet(album, ReadManifest(manifestPath), startEndDates);
            metadata.SaveFile(album.Path);

            CleanUp();

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            workbooks = xlApp.Workbooks; 
        }

        /// <summary>
        /// Used for properly releasing excel resources
        /// </summary>
        public void CleanUp()
        {
            xlApp.Quit();

            if (manifest != null)
            {
                manifest.CleanUp();
                manifest = null;
            }
            Marshal.ReleaseComObject(workbooks);
            workbooks = null;
            Marshal.ReleaseComObject(xlApp);
            xlApp = null;
        }
    }
}
