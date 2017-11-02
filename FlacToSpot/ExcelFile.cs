using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace Spotifyify
{
    /// <summary>
    /// Abstraction for an Excel file
    /// </summary>
    class ExcelFile
    {
        #region Fields

        /// <summary>
        /// Workbook object
        /// </summary>
        protected Workbook wb;

        /// <summary>
        /// All worksheets in the workbook
        /// </summary>
        protected Sheets worksheets;

        /// <summary>
        /// Only working with 1 worksheet
        /// </summary>
        protected Worksheet ws;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ExcelFile class
        /// Will create a new workbook
        /// </summary>
        public ExcelFile()
        {
            wb = new Workbook();
        }

        /// <summary>
        /// Initializes a new instance of the ExcelFile class
        /// </summary>
        /// <param name="wb">Workbook to be used</param>
        public ExcelFile(Workbook wb)
        {
            this.wb = wb;
            worksheets = wb.Worksheets;
            ws = (Worksheet)worksheets.get_Item(1);
        }

        #endregion

        /// <summary>
        /// Used for properly releasing excel resources
        /// </summary>
        public void CleanUp()
        {
            if (ws != null)
            {
                Marshal.ReleaseComObject(ws);
                ws = null;
            }

            if (worksheets != null)
            {
                Marshal.ReleaseComObject(worksheets);
                worksheets = null;
            }

            if (wb != null)
            {
                Marshal.ReleaseComObject(wb);
                wb = null;
            }
        }

    }
}
