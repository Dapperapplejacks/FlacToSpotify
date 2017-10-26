using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spotifyify
{
    public partial class AlbumPickForm : Form
    {
        public string selectedAlbumTitle;

        public AlbumPickForm(Dictionary<string, int> albumTitleDict)
        {
            InitializeComponent();

            //Populate Listbox
        }

        private void UpdateListBox(string searchString)
        {

        }
        
        private void TitleSearchBar_TextChanged(object sender, EventArgs e)
        {
            //Update Listbox
        }

        private void Accept_Click(object sender, EventArgs e)
        {

        }

        private void Cancel_Click(object sender, EventArgs e)
        {

        }
    }
}
