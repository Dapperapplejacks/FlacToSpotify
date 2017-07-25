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


        public Application()
        {
            InitializeComponent();

            excelHandler = new ExcelHandler();
        }

        #region Select Directory

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
                    MessageBox.Show("Error: " + ex.Message);
                }

                DoneLabel.Visible = false;
            }
            catch (Exception ex)
            {
                //dont do anything, they probably pressed cancel

            }
        }

        private void DestinationFolderClick(object sender, EventArgs e)
        {
            try
            {
                dirHandler.DestinationDirectory = GetDirectoryString();
                this.SelectDeliveryButton.Text = "Destination Directory: " + dirHandler.DestinationDirectory;
            }
            catch (Exception ex)
            {
                //Dont do anything
            }
            
        }

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
                //TODO: Alarm
                throw new Exception();
            }
        }

        #endregion

        private void UpdateFileTable()
        {

            FileTable.Controls.Clear();
            FileTable.RowStyles.Clear();
            FileTable.Controls.Add(this.ExtensionLabel, 1, 0);
            FileTable.Controls.Add(this.FileLabel, 0, 0);
            FileTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));

            FileTable.RowCount = dirHandler.Files.Count() + 1;

            for (int i = 1; i < FileTable.RowCount; i++)
            {

                //RowStyle rowStyle = new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 
                //    100f/tableLayoutPanel1.RowCount-1);

                RowStyle rowStyle = new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25f);

                FileTable.RowStyles.Add(rowStyle);

                string fileName = dirHandler.Files[i-1].FileName;
                string extension = dirHandler.Files[i-1].Extension;

                AddLabelToTable(fileName, 0, i);
                AddLabelToTable(extension, 1, i);

            }
        }

        private void AddLabelToTable(string text, int col, int row)
        {
            Label newLabel = new Label();
            newLabel.Text = text;
            newLabel.AutoSize = true;
            newLabel.Font = new Font("Microsoft Sans Serif", 12);
            FileTable.Controls.Add(newLabel, col, row);
        }

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
                    MessageBox.Show("Could not read file. Error: " + ex.Message);
                }
            }
        }

        private void ProcessFilesClick(object sender, EventArgs e)
        {
           
            /*Create new directory as Delivery Folder (name being date YYYYMMDD_XX where XX is for multiple deliveries in a day)
             * Child of Delivery folder is Album folder with name as name of Album
             * 
            */
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
                excelHandler.CreateMetaData(dirHandler.Album);

                DoneLabel.Visible = true;
                DoneLabel.Text = "Finished processing: " + dirHandler.Album.GetAlbumName();

                ResetDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            
        }

        //Just reset Album select button/album instance, table rows
        //and make progress bar go away
        private void ResetDisplay()
        {
            dirHandler.ResetAlbum();
            this.SelectAlbumButton.Text = "Select Album Directory";
            FileTable.Controls.Clear();
            FileTable.RowStyles.Clear();
        }

        private void FormClosingHandler(object sender, FormClosingEventArgs e)
        {
            excelHandler.CleanUp();
        }

        private void Application_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(FormClosingHandler);
        }


    }
}
