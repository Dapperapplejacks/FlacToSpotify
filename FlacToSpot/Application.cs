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

        private DirectoryHandler albumDirectory;
        private DirectoryHandler destinationDirectory;
        private ExcelHandler excelHandler;
        private ExcelFile UPC_UES;


        public Application()
        {
            InitializeComponent();

            excelHandler = new ExcelHandler();
        }

        #region Select Directory

        private void SelectAlbumDirClick(object sender, EventArgs e)
        {
            albumDirectory = new DirectoryHandler(GetDirectoryString());

            this.button1.Text = "Album Directory: " + albumDirectory.Path;

            UpdateFileTable();
        }

        private void DestinationFolderClick(object sender, EventArgs e)
        {
            destinationDirectory = new DirectoryHandler(GetDirectoryString());

            this.button4.Text = "Destination Directory: " + destinationDirectory.Path;
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
                return "No Directory Selected yet";
            }
        }

        #endregion

        private void UpdateFileTable()
        {

            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));

            tableLayoutPanel1.RowCount = albumDirectory.FileCount + 1;

            for (int i = 1; i < tableLayoutPanel1.RowCount; i++)
            {

                //RowStyle rowStyle = new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 
                //    100f/tableLayoutPanel1.RowCount-1);

                RowStyle rowStyle = new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25f);

                tableLayoutPanel1.RowStyles.Add(rowStyle);

                string fileName = albumDirectory.Files[i-1].FileName;
                string extension = albumDirectory.Files[i-1].Extension;

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
            tableLayoutPanel1.Controls.Add(newLabel, col, row);
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
                            UPC_UES = excelHandler.ReadFile(ofd.FileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read file. Error: " + ex.Message);
                }
            }

            //Let user pick file
            //Pull out workbook from file
            //Make into Excelfile object
        }

        private void ProcessFilesClick(object sender, EventArgs e)
        {
            //Change names of songs
            //Change name of coverart
            //Generate metadata file
            //

        }

        

        



    }
}
