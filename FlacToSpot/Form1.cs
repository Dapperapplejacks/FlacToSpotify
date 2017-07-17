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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FlacToSpot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stream dialogStream = null;

            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            bool fileOpen = false;

            //openFileDialog.InitialDirectory = "c:\\";
            //openFileDialog.Filter = "FLAC files (*.flac)|*.flac";


            /*
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.label1.Text = folderBrowserDialog.SelectedPath;
            }
             * */

            CommonOpenFileDialog
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (Stream stream = openFileDialog.OpenFile())
                {
                    
                }

                this.label1.Text = ;
            }

        }
    }
}
