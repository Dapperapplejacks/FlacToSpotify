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
    /// <summary>
    /// Class used for handling the album directory, destination directory, and coordinates the moving/renaming of files
    /// </summary>
    class DirectoryHandler
    {

        #region Properties
        /// <summary>
        /// Instance of album currently selected by user
        /// </summary>
        private Album album;

        /// <summary>
        /// Path of Album directory
        /// </summary>
        private string albumDirPath;

        /// <summary>
        /// Path of Destination directory
        /// </summary>
        private string destDirPath;

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

        #endregion

        #region Constructor

        public DirectoryHandler(string dirpath)
        {
            NewAlbum(dirpath);
        }

        #endregion

        /// <summary>
        /// Used for handling a new instance of an album
        /// </summary>
        /// <param name="dirpath">Path of album directory</param>
        public void NewAlbum(string dirpath)
        {
            albumDirPath = dirpath;

            try
            {
                album = new Album(dirpath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// To clear reference of album, used when done with full processing of files
        /// </summary>
        public void ResetAlbum()
        {
            albumDirPath = "";
            album = null;
        }

        #region Process Album

        /// <summary>
        /// Main method used for processing the album.
        /// This includes creating the delivery directory, the new album directory,
        /// renaming the files, and moving those files to the new album directory
        /// </summary>
        public void ProcessAlbum()
        {

            /*Create new directory as Delivery Folder (name being date YYYYMMDD_XX where XX is for multiple deliveries in a day)
             * Child of Delivery folder is Album folder with directory name as Album name 
            */

            //Setup delivery folder
            string deliveryName = GetDeliveryFolderName();
            string deliveryPath = Path.Combine(DestinationDirectory, deliveryName);
            Directory.CreateDirectory(deliveryPath);
            
            try
            {
                //Setup album folder
                string albumName = album.GetAlbumName();
                albumName = CheckForValidChars(albumName);
                

                string newAlbumPath = Path.Combine(deliveryPath, albumName);
                Directory.CreateDirectory(newAlbumPath);
                album.Path = newAlbumPath;

                RenameFiles();
                MoveFiles(newAlbumPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Helper for figuring out what to call the delivery folder
        /// </summary>
        /// <returns>Name of delivery folder.
        /// Format: YYYYMMDD_XX where XX is what number delivery that day
        /// </returns>
        private string GetDeliveryFolderName()
        {
            DateTime dateTime = DateTime.Now;
            int year = dateTime.Year;
            int month = dateTime.Month;
            int day = dateTime.Day;

            string strYear = year.ToString();
            string strMonth = month.ToString();
            string strDay = day.ToString();

            if (month < 10)
            {
                strMonth = "0" + month;
            }
            if (day < 10)
            {
                strDay = "0" + day;
            }

            string date = strYear + strMonth + strDay;

            int delNumber = 1;
            string name = date + "_01";

            while (Directory.Exists(Path.Combine(DestinationDirectory, name)))
            {
                delNumber++;

                name = delNumber < 10 ? date + "_0" + delNumber : name = date + "_" + delNumber;
            }

            return name;
        }

        /// <summary>
        /// Helper for checking and altering the album name in order
        /// for it to be a valid folder name.
        /// Will warn user if name has been changed
        /// </summary>
        /// <param name="name">Name of album</param>
        /// <returns>Name of valid album folder</returns>
        private string CheckForValidChars(string name)
        {
            string startName = string.Copy(name);

            name = name.Replace("\\", "-");
            name = name.Replace("/", "-");
            name = name.Replace(":", "-");
            name = name.Replace("*", " ");
            name = name.Replace("?", "");
            name = name.Replace("\"", "\'");
            name = name.Replace("<", "-");
            name = name.Replace(">", "-");
            name = name.Replace("|", "-");

            if (!name.Equals(startName))
            {
                MessageBox.Show("Album directory name has been changed due to invalid character(s).\nNormal processing will continue.", "Warning");
            }

            return name;
        }

        /// <summary>
        /// Helper for renaming files. Properties of files themselves will handle
        /// changing the name in actual file system
        /// </summary>
        private void RenameFiles()
        {
            album.CoverArt.FileName = "coverart" + album.CoverArt.Extension;
            Application.ProcessProgress.PerformStep();

            //int cdCount = 1;
            foreach (CD cd in album.CDs)
            {
                foreach (FlacFile file in cd.FlacFiles)
                {
                    try
                    {
                        uint disc = file.Tag.Disc == 0 ? 1 : file.Tag.Disc;
                        string newName = disc + "_" + file.Tag.Track + ".flac";
                        file.FileName = newName;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    Application.ProcessProgress.PerformStep();
                }

                //cdCount++;
            }
        }

        /// <summary>
        /// Helper for moving files.
        /// </summary>
        /// <param name="destination">Where the files will be moved to. Should be the new Album directory</param>
        private void MoveFiles(string destination)
        {
            MediaFile[] files = album.GetAlbumFiles();

            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    string destPath = Path.Combine(destination, files[i].FileName);
                    File.Move(files[i].Path, destPath);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                Application.ProcessProgress.PerformStep();
            }
        }

        #endregion 
    }
}
