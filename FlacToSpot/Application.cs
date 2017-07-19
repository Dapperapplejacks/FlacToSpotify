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

        private DirectoryHandler directoryHandler;

        public Application()
        {
            InitializeComponent();
        }

        private void SelectDirClick(object sender, EventArgs e)
        {
            directoryHandler = new DirectoryHandler(GetDirectoryString());

            this.label1.Text = directoryHandler.Path;

            UpdateFileTable();
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

        private void UpdateFileTable()
        {

            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));

            tableLayoutPanel1.RowCount = directoryHandler.FileCount + 1;

            for (int i = 1; i < tableLayoutPanel1.RowCount; i++)
            {

                //RowStyle rowStyle = new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 
                //    100f/tableLayoutPanel1.RowCount-1);

                RowStyle rowStyle = new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25f);

                tableLayoutPanel1.RowStyles.Add(rowStyle);

                string fileName = directoryHandler.Files[i-1].FileName;
                string extension = directoryHandler.Files[i-1].Extension;

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


    }
}
