﻿namespace FlacToSpot
{
    partial class Application
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
            this.SelectAlbumButton = new System.Windows.Forms.Button();
            this.FileTable = new System.Windows.Forms.TableLayoutPanel();
            this.ExtensionLabel = new System.Windows.Forms.Label();
            this.FileLabel = new System.Windows.Forms.Label();
            this.SelectUPCButton = new System.Windows.Forms.Button();
            this.ProcessFilesButton = new System.Windows.Forms.Button();
            this.SelectDeliveryButton = new System.Windows.Forms.Button();
            this.DoneLabel = new System.Windows.Forms.Label();
            this.FileTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // SelectAlbumButton
            // 
            this.SelectAlbumButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectAlbumButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SelectAlbumButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectAlbumButton.Location = new System.Drawing.Point(0, 0);
            this.SelectAlbumButton.Name = "SelectAlbumButton";
            this.SelectAlbumButton.Size = new System.Drawing.Size(484, 40);
            this.SelectAlbumButton.TabIndex = 1;
            this.SelectAlbumButton.Text = "Select Album Directory";
            this.SelectAlbumButton.UseVisualStyleBackColor = false;
            this.SelectAlbumButton.Click += new System.EventHandler(this.SelectAlbumDirClick);
            // 
            // FileTable
            // 
            this.FileTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileTable.AutoScroll = true;
            this.FileTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FileTable.BackColor = System.Drawing.Color.LightGray;
            this.FileTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.FileTable.ColumnCount = 2;
            this.FileTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.FileTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.FileTable.Controls.Add(this.ExtensionLabel, 1, 0);
            this.FileTable.Controls.Add(this.FileLabel, 0, 0);
            this.FileTable.Location = new System.Drawing.Point(0, 46);
            this.FileTable.Name = "FileTable";
            this.FileTable.RowCount = 2;
            this.FileTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.FileTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.FileTable.Size = new System.Drawing.Size(484, 198);
            this.FileTable.TabIndex = 2;
            // 
            // ExtensionLabel
            // 
            this.ExtensionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExtensionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExtensionLabel.Location = new System.Drawing.Point(242, 1);
            this.ExtensionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ExtensionLabel.Name = "ExtensionLabel";
            this.ExtensionLabel.Size = new System.Drawing.Size(241, 25);
            this.ExtensionLabel.TabIndex = 1;
            this.ExtensionLabel.Text = "Extension";
            this.ExtensionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FileLabel
            // 
            this.FileLabel.AutoSize = true;
            this.FileLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileLabel.Location = new System.Drawing.Point(1, 1);
            this.FileLabel.Margin = new System.Windows.Forms.Padding(0);
            this.FileLabel.Name = "FileLabel";
            this.FileLabel.Size = new System.Drawing.Size(240, 25);
            this.FileLabel.TabIndex = 0;
            this.FileLabel.Text = "File";
            this.FileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SelectUPCButton
            // 
            this.SelectUPCButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectUPCButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SelectUPCButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectUPCButton.Location = new System.Drawing.Point(0, 250);
            this.SelectUPCButton.Name = "SelectUPCButton";
            this.SelectUPCButton.Size = new System.Drawing.Size(484, 40);
            this.SelectUPCButton.TabIndex = 3;
            this.SelectUPCButton.Text = "Select UPC/USE File";
            this.SelectUPCButton.UseVisualStyleBackColor = false;
            this.SelectUPCButton.Click += new System.EventHandler(this.SelectUPCUESFileClick);
            // 
            // ProcessFilesButton
            // 
            this.ProcessFilesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessFilesButton.BackColor = System.Drawing.Color.Green;
            this.ProcessFilesButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOliveGreen;
            this.ProcessFilesButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ProcessFilesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessFilesButton.Location = new System.Drawing.Point(0, 351);
            this.ProcessFilesButton.Name = "ProcessFilesButton";
            this.ProcessFilesButton.Size = new System.Drawing.Size(484, 70);
            this.ProcessFilesButton.TabIndex = 4;
            this.ProcessFilesButton.Text = "Process Files";
            this.ProcessFilesButton.UseVisualStyleBackColor = false;
            this.ProcessFilesButton.Click += new System.EventHandler(this.ProcessFilesClick);
            // 
            // SelectDeliveryButton
            // 
            this.SelectDeliveryButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectDeliveryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectDeliveryButton.Location = new System.Drawing.Point(0, 297);
            this.SelectDeliveryButton.Name = "SelectDeliveryButton";
            this.SelectDeliveryButton.Size = new System.Drawing.Size(484, 48);
            this.SelectDeliveryButton.TabIndex = 5;
            this.SelectDeliveryButton.Text = "Select Delivery Folder Destination";
            this.SelectDeliveryButton.UseVisualStyleBackColor = true;
            this.SelectDeliveryButton.Click += new System.EventHandler(this.DestinationFolderClick);
            // 
            // DoneLabel
            // 
            this.DoneLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DoneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DoneLabel.Location = new System.Drawing.Point(0, 424);
            this.DoneLabel.Name = "DoneLabel";
            this.DoneLabel.Size = new System.Drawing.Size(483, 20);
            this.DoneLabel.TabIndex = 7;
            this.DoneLabel.Text = "Done!";
            this.DoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DoneLabel.Visible = false;
            // 
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.DoneLabel);
            this.Controls.Add(this.SelectDeliveryButton);
            this.Controls.Add(this.ProcessFilesButton);
            this.Controls.Add(this.SelectUPCButton);
            this.Controls.Add(this.FileTable);
            this.Controls.Add(this.SelectAlbumButton);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "Application";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Spotifyify v1.0";
            this.Load += new System.EventHandler(this.Application_Load);
            this.FileTable.ResumeLayout(false);
            this.FileTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SelectAlbumButton;
        private System.Windows.Forms.TableLayoutPanel FileTable;
        private System.Windows.Forms.Label FileLabel;
        private System.Windows.Forms.Label ExtensionLabel;
        private System.Windows.Forms.Button SelectUPCButton;
        private System.Windows.Forms.Button ProcessFilesButton;
        private System.Windows.Forms.Button SelectDeliveryButton;
        private System.Windows.Forms.Label DoneLabel;
    }
}

