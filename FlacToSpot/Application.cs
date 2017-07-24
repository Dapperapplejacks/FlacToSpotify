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
        private Manifest UPC_UES;


        public Application()
        {
            InitializeComponent();

            excelHandler = new ExcelHandler();
        }

        #region Select Directory

        private void SelectAlbumDirClick(object sender, EventArgs e)
        {
            try
            {
                dirHandler = new DirectoryHandler(GetDirectoryString());
                this.button1.Text = "Album Directory: " + dirHandler.Album.Path;

                UpdateFileTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            

            
        }

        private void DestinationFolderClick(object sender, EventArgs e)
        {
            dirHandler.DestinationDirectory = GetDirectoryString();

            this.button4.Text = "Destination Directory: " + dirHandler.DestinationDirectory;
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

            tableLayoutPanel1.RowCount = dirHandler.Files.Count() + 1;

            for (int i = 1; i < tableLayoutPanel1.RowCount; i++)
            {

                //RowStyle rowStyle = new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 
                //    100f/tableLayoutPanel1.RowCount-1);

                RowStyle rowStyle = new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25f);

                tableLayoutPanel1.RowStyles.Add(rowStyle);

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
                            UPC_UES = excelHandler.ReadManifest(ofd.FileName);
                            button2.Text = Path.GetFileName(ofd.FileName);
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
                dirHandler.ProcessAlbum(progressBar1);
                excelHandler.CreateMetaData(dirHandler.DestinationDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
            

        }

        



    }
}
