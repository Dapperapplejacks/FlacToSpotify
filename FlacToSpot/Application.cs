using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FlacToSpot
{
    public partial class Application : Form
    {

        private DirectoryHandler dirHandler;
        private ExcelHandler excelHandler;

        /// <summary>
        /// Constructor for Application class.
        /// This will initialize all components,
        /// as well as construct an instance of the ExcelHandler
        /// </summary>
        public Application()
        {
            InitializeComponent();

            excelHandler = new ExcelHandler();
        }

        #region Select Directories

        /// <summary>
        /// Button click handler for the Select Album Directory Button.
        /// Uses FolderDialogBrowser to bring up a folder browser window,
        /// and will allow user to select directory.
        /// Will instantiate a DirectoryHandler object, as well as an Album object.
        /// Finally, will update table gui with files found in selected directory.
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Unused</param>
        private void SelectAlbumDirClick(object sender, EventArgs e)
        {

            string dirString = "";

            try
            {
                dirString = GetDirectoryString();

                try
                {
                    if (dirHandler == null)
                    {
                        dirHandler = new DirectoryHandler(dirString);
                    }
                    else
                    {
                        dirHandler.NewAlbum(dirString);
                    }

                    this.SelectAlbumButton.Text = "Album Directory: " + dirHandler.Album.Path;

                    UpdateFileTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error");
                }

                DoneLabel.Visible = false;
            }
            catch (Exception ex)
            {
                //Dont do anything, cancel pressed
            }
        }

        /// <summary>
        /// Button click handler for the Select Destination Directory Button.
        /// Uses FolderDialogBrowser to bring up a folder browser window,
        /// and will allow user to select directory.
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Unused</param>
        private void DestinationFolderClick(object sender, EventArgs e)
        {
            try
            {
                dirHandler.DestinationDirectory = GetDirectoryString();
                this.SelectDeliveryButton.Text = "Destination Directory: " + dirHandler.DestinationDirectory;
            }
            catch (Exception ex)
            {
                //Dont do anything, cancel pressed
            }
        }

        /// <summary>
        /// Helper method for bringing up a folder browser dialog window
        /// </summary>
        /// <returns>String representing path of selected directory</returns>
        private string GetDirectoryString()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = ((CommonDialog)folderBrowserDialog).ShowDialog();

            if (result == DialogResult.OK)
            {
                return folderBrowserDialog.SelectedPath;
            }
            else
            {
                //Pressed cancel
                throw new Exception();
            }
        }

        #endregion

        #region File Table Update
        /// <summary>
        /// Helper for SelectAlbumDirClick method
        /// Used for updating Table element with files currently in selected album directory 
        /// </summary>
        private void UpdateFileTable()
        {

            FileTable.Controls.Clear();
            FileTable.RowStyles.Clear();
            FileTable.Controls.Add(this.ExtensionLabel, 1, 0);
            FileTable.Controls.Add(this.FileLabel, 0, 0);
            FileTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));

            MediaFile[] files = dirHandler.Album.GetAlbumFiles();

            FileTable.RowCount = files.Count() + 1;

            for (int i = 1; i < FileTable.RowCount; i++)
            {
                RowStyle rowStyle = new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25f);

                FileTable.RowStyles.Add(rowStyle);

                string fileName = files[i-1].FileName;
                string extension = files[i-1].Extension;

                AddLabelToTable(fileName, 0, i);
                AddLabelToTable(extension, 1, i);

            }
        }

        /// <summary>
        /// Helper for UpdateFileTable method
        /// Will create new label and add to specified cell of table
        /// </summary>
        /// <param name="text">Text to be added to cell</param>
        /// <param name="col">Cell column</param>
        /// <param name="row">Cell row</param>
        private void AddLabelToTable(string text, int col, int row)
        {
            Label newLabel = new Label();
            newLabel.Text = text;
            newLabel.AutoSize = true;
            newLabel.Font = new Font("Microsoft Sans Serif", 12);
            FileTable.Controls.Add(newLabel, col, row);
        }

        #endregion

        /// <summary>
        /// Button click handler for the Select UPC Button.
        /// Uses OpenFileDialog to bring up a file browser window,
        /// and will allow user to select excel file.
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Unused</param>
        private void SelectUPCUESFileClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files (*.xlsx; *.xls)|*.xlsx; *.xls";
            Stream stream = null;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream = ofd.OpenFile()) != null)
                    {
                        using (stream)
                        {
                            excelHandler.manifestPath = (ofd.FileName);
                            SelectUPCButton.Text = Path.GetFileName(ofd.FileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read file.\nError: " + ex.Message, "Error");
                }
            }
        }

        /// <summary>
        /// Button click handler for Process Files button.
        /// Makes Directory Handler call to process album which consists of renaming and moving files.
        /// Followed by ExcelHandler creating the metadata excel file.
        /// Finally, the display will be reset, namely the table and album selection
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Unused</param>
        private void ProcessFilesClick(object sender, EventArgs e)
        {

            ProcessFilesButton.Text = "Processing...";
            ProcessFilesButton.Enabled = false;
            
            try
            {
                if (dirHandler == null)
                {
                    throw new Exception("Album Directory Not Selected");
                }
                if (excelHandler.manifestPath == null || excelHandler.manifestPath == "")
                {
                    throw new Exception("UPC Manifest Not Selected");
                }

                dirHandler.ProcessAlbum();

                string[] startEndDates = GetStartEndDates();

                excelHandler.CreateMetaData(dirHandler.Album, startEndDates);

                DoneLabel.Visible = true;
                DoneLabel.Text = "Finished processing: " + dirHandler.Album.GetAlbumName();

                ResetDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            
        }

        /// <summary>
        /// Resets album instance, clears the file table,
        /// and resets Process Files button
        /// </summary>
        private void ResetDisplay()
        {
            ProcessFilesButton.Text = "Process Files";
            ProcessFilesButton.Enabled = true;
            dirHandler.ResetAlbum();
            this.SelectAlbumButton.Text = "Select Album Directory";
            FileTable.Controls.Clear();
            FileTable.RowStyles.Clear();
        }

        /// <summary>
        /// Handler for closing of application
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Unused</param>
        private void FormClosingHandler(object sender, FormClosingEventArgs e)
        {
            excelHandler.CleanUp();
        }
        /// <summary>
        /// Ran on application load
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Unused</param>
        private void Application_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(FormClosingHandler);
        }

        /// <summary>
        /// Listener for EndDatePicker check box
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Unused</param>
        private void EndDateCheck_CheckedChanged(object sender, EventArgs e)
        {
            EndDatePicker.Enabled = EndDateCheck.Checked;
        }

        /// <summary>
        /// Helper for retrieving date data for start and end dates
        /// </summary>
        /// <returns></returns>
        private string[] GetStartEndDates()
        {
            string[] dates = new string[2];
            dates[0] = String.Join("-", new string[]{StartDatePicker.Value.Year.ToString(), 
                                                                 StartDatePicker.Value.Month.ToString(),
                                                                 StartDatePicker.Value.Day.ToString()});
            string endDate = String.Join("-", new string[]{StartDatePicker.Value.Year.ToString(), 
                                                                 StartDatePicker.Value.Month.ToString(),
                                                                 StartDatePicker.Value.Day.ToString()});
            dates[1] = EndDateCheck.Checked ? endDate : "";

            return dates;
        }
    }
}
