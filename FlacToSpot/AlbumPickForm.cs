using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Spotifyify
{
    public partial class AlbumPickForm : Form
    {
        /// <summary>
        /// Field for list of all album titles
        /// </summary>
        private List<string> albumTitleList;

        /// <summary>
        /// Selected album title from AlbumTitleListBox
        /// </summary>
        public string selectedAlbumTitle;
        
        /// <summary>
        /// Initializes an instance of the AlbumPickForm class
        /// </summary>
        /// <param name="albumTitleList">Dictionary with keys being albumt titles and </param>
        public AlbumPickForm(List<string> albumTitleList)
        {
            InitializeComponent();

            //Keep this disabled until a title is selected
            Accept.Enabled = false;

            this.albumTitleList = albumTitleList;
            //Populate Listbox
            UpdateListBox("");
        }

        /// <summary>
        /// Will determine what titles should be in the ListBox and update its contents
        /// </summary>
        /// <param name="searchString">String entered into TitleSearchBar, any title that contains this will be included in Listbox</param>
        private void UpdateListBox(string searchString)
        {
            //Nothing in search bar, return all album titles
            if (string.IsNullOrEmpty(searchString))
            {
                this.AlbumTitleListBox.DataSource = albumTitleList;
            }
            else
            {
                //Get list of all album titles that contain the search string
                List<String> newList = albumTitleList.Where<string>(title => title.Contains(searchString)).ToList<string>();
                this.AlbumTitleListBox.DataSource = newList;
            }
        }

        /// <summary>
        /// Handles TextChanged event for TitleSearchBar textbox
        /// Will update listbox with list of album titles that contain the string entered into TitleSearchBar
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void TitleSearchBar_TextChanged(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            selectedAlbumTitle = "";
            Accept.Enabled = false;
            
            //Update Listbox
            UpdateListBox(TitleSearchBar.Text);

            UseWaitCursor = false;
        }

        /// <summary>
        /// Handles change in selected value of album title listbox
        /// Will enable/disable Accept button depending on if there is a value selected
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void AlbumTitleListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedAlbumTitle = (string)AlbumTitleListBox.SelectedValue;

            //If nothing is selected, disable the accept button
            if (string.IsNullOrEmpty(selectedAlbumTitle))
            {
                Accept.Enabled = false;
            }
            else
            {
                Accept.Enabled = true;
            }
        }

        /// <summary>
        /// Handles click event for Accept button
        /// Will exit this form 
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void AcceptButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Handles click event for Cancel button
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit the Album Title Picker?\n\n" +
                "You will have to manually enter UPCs\\ISRCs in the metadata spreadsheet.", "WARNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            this.DialogResult = DialogResult.None;
        }
    }
}
