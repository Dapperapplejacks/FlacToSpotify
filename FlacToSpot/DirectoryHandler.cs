using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FlacToSpot
{
    class DirectoryHandler
    {

        #region Properties
        
        private Album album;
        private string albumDirPath;
        private string destDirPath;
        private MediaFile[] files;
        private ProgressBar pbar;

        public Album Album
        {
            get
            {
                return album;
            }
            private set
            {
                album = value;
            }
        }

        public string DestinationDirectory
        {
            get
            {
                return destDirPath;
            }
            set
            {
                destDirPath = value;
            }
        }

        public MediaFile[] Files
        {
            get
            {
                return files;
            }
            private set
            {
                files = value;
            }
        }

        #endregion

        #region Constructor

        public DirectoryHandler(string dirpath)
        {
            NewAlbum(dirpath);
        }

        public void NewAlbum(string dirpath)
        {
            albumDirPath = dirpath;

            try
            {
                album = new Album(dirpath);

                this.files = album.GetAlbumFiles();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ResetAlbum()
        {
            albumDirPath = "";
            album = null;
        }

        #endregion

        #region Process Album

        public void ProcessAlbum(ProgressBar pbar)
        {
            this.pbar = pbar;

            //Setup delivery folder
            string deliveryName = GetDeliveryFolderName();
            string deliveryPath = Path.Combine(DestinationDirectory, deliveryName);
            Directory.CreateDirectory(deliveryPath);
            
            try
            {
                //Setup album folder
                string albumName = album.GetAlbumName();
                albumName = albumName.Replace(":", "- ");

                string newAlbumPath = Path.Combine(deliveryPath, albumName);
                Directory.CreateDirectory(newAlbumPath);
                album.Path = newAlbumPath;

                //rename all files
                RenameFiles();
                //Move files
                MoveFiles(newAlbumPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /*
         * Grab the date, and figure out what delivery number to use (First is 01)
         * */
        private string GetDeliveryFolderName()
        {
            DateTime dateTime = DateTime.Now;
            int year = dateTime.Year;
            int month = dateTime.Month;
            int day = dateTime.Day;

            string date = "" + year + month + day;

            int delNumber = 1;
            string name = date + "_01";

            while (Directory.Exists(Path.Combine(DestinationDirectory, name)))
            {
                delNumber++;

                name = delNumber < 10 ? date + "_0" + delNumber : name = date + "_" + delNumber;
            }

            return name;
        }

        private void RenameFiles()
        {
            int flacCount = 1;
            foreach (MediaFile file in files)
            {
                if (file.Extension.Equals(".flac"))
                {
                    file.FileName = "1_" + flacCount++ + ".flac";
                }
                else if (file.Extension.Equals(".jpg") || file.Extension.Equals(".jpeg"))
                {
                    file.FileName = "coverart" + file.Extension;
                }
            }
        }

        private void MoveFiles(string destination)
        {
            pbar.Visible = true;
            pbar.Minimum = 1;
            pbar.Maximum = files.Length;
            pbar.Value = 1;
            pbar.Step = 1;

            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    //string filePath = Path.Combine(files[i].Path, files[i].FileName);
                    string destPath = Path.Combine(destination, files[i].FileName);

                    File.Move(files[i].Path, destPath);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                pbar.PerformStep();
            }
        }

        #endregion 
    }
}
