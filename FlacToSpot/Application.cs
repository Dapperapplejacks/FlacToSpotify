﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Spotifyify
{
    public partial class Application : Form
    {
        #region Fields

        /// <summary>
        /// Field for DirectoryHandler object
        /// </summary>
        private DirectoryHandler dirHandler;

        /// <summary>
        /// FIeld for ExcelHandler object
        /// </summary>
        private ExcelHandler excelHandler;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for Application class.
        /// This will initialize all components,
        /// as well as construct an instance of the ExcelHandler
        /// </summary>
        public Application()
        {
            //MessageBox.Show("Initializing window components");

            try
            {
                InitializeComponent();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed initialization: " + ex.Message);
                this.Close();
            }

            //MessageBox.Show("Initialized window components");

            try
            {
                excelHandler = new ExcelHandler();
            }
            catch(Exception)
            {
                MessageBox.Show("Excel is not properly installed\nExiting application", "Excel Error");
                this.Close();
            }

            //MessageBox.Show("Exccel Handler properly initialized");
        }

        #endregion

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
            catch (Exception)
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
            catch (Exception)
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
            try
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

                    string fileName = files[i - 1].FileName;
                    string extension = files[i - 1].Extension;

                    AddLabelToTable(fileName, 0, i);
                    AddLabelToTable(extension, 1, i);

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Unexpected Error: " + ex.Message);
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

        #region Event Handlers

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
            bool success = true;
            this.Cursor = Cursors.WaitCursor;
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
                if (dirHandler.DestinationDirectory == "" || dirHandler.DestinationDirectory == null)
                {
                    throw new Exception("Destination Directory Not Selected");
                }

                try
                {
                    ProcessFilesButton.Text = "Processing...";
                    ProcessFilesButton.Enabled = false;
                    ProcessFilesButton.UseWaitCursor = true;

                    ProcessProgress.Visible = true;
                    ProcessProgress.Maximum = dirHandler.Album.GetTrackCount() * 3 + 1;  //Moving/naming each file, and going through each row for metadata
                    ProcessProgress.Step = 1;


                    dirHandler.ProcessAlbum();

                    try
                    {
                        string[] startEndDates = GetStartEndDates();

                        excelHandler.CreateMetaData(dirHandler.Album, startEndDates);
                    }
                    catch(Exception ex){
                        MessageBox.Show("Unable to create metadata file. Ending processing.\n\nError: " + ex.Message, "Error");

                        ProcessProgress.Value = 1;
                        ProcessProgress.Visible = false;
                        DoneLabel.Visible = true;
                        DoneLabel.Text = "Moved/Renamed album files, unsuccessfully created metadata for: " + dirHandler.Album.GetAlbumName();

                        success = false;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Unable to move/rename album files. Ending processing.\n\nError: " + ex.Message, "Error");

                    ProcessProgress.Value = 1;
                    ProcessProgress.Visible = false;
                    DoneLabel.Visible = true;
                    DoneLabel.Text = "Unsuccessfully moved/renamed album files, metadata file not created for: " + dirHandler.Album.GetAlbumName();

                    success = false;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            if (success)
            {
                ProcessProgress.Value = 1;
                ProcessProgress.Visible = false;
                DoneLabel.Visible = true;
                DoneLabel.Text = "Finished processing: " + dirHandler.Album.GetAlbumName();
            }

            ResetDisplay();
            this.Cursor = Cursors.Arrow;
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Resets album instance, clears the file table,
        /// and resets Process Files button
        /// </summary>
        private void ResetDisplay()
        {
            ProcessFilesButton.Text = "Process Files";
            ProcessFilesButton.Enabled = true;
            ProcessFilesButton.UseWaitCursor = false;
            dirHandler.ResetAlbum();
            this.SelectAlbumButton.Text = "Select Album Directory";
            FileTable.Controls.Clear();
            FileTable.RowStyles.Clear();
            FileTable.Refresh();
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

        #endregion
    }
}
