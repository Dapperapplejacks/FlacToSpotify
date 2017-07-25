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
        private Workbooks workbooks;
        public string manifestPath;
        public Manifest manifest;

        public ExcelHandler()
        {
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            workbooks = xlApp.Workbooks;

            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed\nExiting application", "Excel Error");
                throw new Exception();
            }
        }

        private Manifest ReadManifest(string path)
        {
            Workbook workbook = workbooks.Open(path, Type.Missing, true);
            manifest = new Manifest(workbook);

            Marshal.ReleaseComObject(workbook);
            workbook = null;

            return manifest;
        }

        public void CreateMetaData(Album album)
        {
            Metadata metadata = new Metadata(workbooks.Add());
            metadata.PopulateSheet(album, ReadManifest(manifestPath));
            metadata.SaveFile(album.Path);

            manifest.CleanUp();
            manifest = null;
        }

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
