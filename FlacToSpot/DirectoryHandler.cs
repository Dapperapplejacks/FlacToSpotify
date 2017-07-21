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
        private int flacCount;
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
            
            this.files = GetAlbumFiles(dirpath);
            CoverArt coverArt = null;
            FlacFile[] flacFiles = new FlacFile[flacCount]; 

            int f = 0;
            foreach(MediaFile file in files)
            {
                if(file is FlacFile)
                {
                    flacFiles[f++] = (FlacFile) file;
                }
                else if(file is CoverArt)
                {
                    coverArt = (CoverArt) file;
                }
            }

            if (coverArt == null)
            {
                throw new Exception("No cover art found");
            }
            if (flacCount <= 0 || flacFiles.Length <= 0)
            {
                throw new Exception("No FLAC files found");
            }

            album = new Album(coverArt, flacFiles, dirpath);
        }

        private MediaFile[] GetAlbumFiles(string dirpath)
        {
            albumDirPath = dirpath;

            string[] filePaths = Directory.EnumerateFiles(dirpath).ToArray<string>();

            MediaFile[] files = new MediaFile[filePaths.Length];

            System.Console.WriteLine("Files:");

            for (int i = 0; i < files.Length; i++)
            {
                switch (System.IO.Path.GetExtension(filePaths[i]))
                {
                    case ".flac":
                    case ".FLAC":
                        flacCount++;
                        files[i] = new FlacFile(filePaths[i]);
                        break;
                    case ".jpeg":
                    case ".JPEG":
                    case ".jpg":
                    case ".JPG":
                    case ".png":
                    case ".PNG":
                        files[i] = new CoverArt(filePaths[i]);
                        break;
                    default:
                        files[i] = new MediaFile(filePaths[i]);
                        break;
                }
                System.Console.WriteLine(filePaths[i]);

            }

            return files;
        }

        #endregion

        #region Process Album

        public void ProcessAlbum(ProgressBar pbar)
        {
            this.pbar = pbar;

            //Setup delivery folder
            string deliveryName = GetDeliveryFolderName();
            string deliveryPath = Path.Combine(DestinationDirectory, deliveryName);

            //Setup album folder
            string albumName = album.GetAlbumName();
            if (albumName == null)
            {
                throw new Exception("Couldn't find album name");
            }
            string newAlbumPath = Path.Combine(deliveryPath, albumName);


            //rename all files
            RenameFiles();
            //Move files
            MoveFiles(newAlbumPath, deliveryPath);
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
                if (file is FlacFile)
                {
                    file.FileName = "1_" + flacCount;
                }
                else
                {

                }
            }
        }

        private void MoveFiles(string source, string destination)
        {
            pbar.Visible = true;
            pbar.Minimum = 1;
            pbar.Maximum = files.Length;
            pbar.Value = 1;
            pbar.Step = 1;

            for (int i = 1; i < files.Length; i++)
            {
                try
                {
                    File.Copy(files[i].Path, destination);
                }

                if (CopyFile(files[i].FileName))
                {
                    pbar.PerformStep();
                }
            }
        }

        private bool CopyFile(string fileName)
        {

        }
        #endregion 
    }
}
