namespace Spotifyify
{
    partial class AlbumPickForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SelectAlbumLabel = new System.Windows.Forms.Label();
            this.AlbumTitleListBox = new System.Windows.Forms.ListBox();
            this.Accept = new System.Windows.Forms.Button();
            this.TitleSearchBar = new System.Windows.Forms.TextBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SelectAlbumLabel
            // 
            this.SelectAlbumLabel.Location = new System.Drawing.Point(12, 13);
            this.SelectAlbumLabel.Name = "SelectAlbumLabel";
            this.SelectAlbumLabel.Size = new System.Drawing.Size(322, 25);
            this.SelectAlbumLabel.TabIndex = 0;
            this.SelectAlbumLabel.Text = "Select or search for Album title";
            this.SelectAlbumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AlbumTitleListBox
            // 
            this.AlbumTitleListBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.AlbumTitleListBox.FormattingEnabled = true;
            this.AlbumTitleListBox.Location = new System.Drawing.Point(15, 80);
            this.AlbumTitleListBox.Name = "AlbumTitleListBox";
            this.AlbumTitleListBox.Size = new System.Drawing.Size(319, 251);
            this.AlbumTitleListBox.TabIndex = 1;
            this.AlbumTitleListBox.SelectedValueChanged += new System.EventHandler(this.AlbumTitleListBox_SelectedValueChanged);
            // 
            // Accept
            // 
            this.Accept.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Accept.Location = new System.Drawing.Point(15, 352);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(155, 47);
            this.Accept.TabIndex = 2;
            this.Accept.Text = "Accept";
            this.Accept.UseVisualStyleBackColor = true;
            this.Accept.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // TitleSearchBar
            // 
            this.TitleSearchBar.Location = new System.Drawing.Point(15, 41);
            this.TitleSearchBar.Name = "TitleSearchBar";
            this.TitleSearchBar.Size = new System.Drawing.Size(319, 20);
            this.TitleSearchBar.TabIndex = 3;
            this.TitleSearchBar.TextChanged += new System.EventHandler(this.TitleSearchBar_TextChanged);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(179, 352);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(155, 47);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // AlbumPickForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 411);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.TitleSearchBar);
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.AlbumTitleListBox);
            this.Controls.Add(this.SelectAlbumLabel);
            this.Name = "AlbumPickForm";
            this.Text = "Album Selection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SelectAlbumLabel;
        private System.Windows.Forms.ListBox AlbumTitleListBox;
        private System.Windows.Forms.Button Accept;
        private System.Windows.Forms.TextBox TitleSearchBar;
        private System.Windows.Forms.Button Cancel;
    }
}