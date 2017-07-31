using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FlacToSpot
{
    /// <summary>
    /// Represents an album which contains a list of CDs, and other information about the album
    /// </summary>
    class Album
    {
        #region Properties
        
        private CD[] _CDs;
        private CoverArt coverArt;
        private string path;
        private Int64 UPC;
        private string[] ISRCs;


        public CD[] CDs
        {
            get
            {
                return _CDs;
            }
            private set
            {
                _CDs = value;
            }
        }
        
        public CoverArt CoverArt
        {
            get
            {
                return this.coverArt;
            }
            private set
            {
                this.coverArt = value;
            }
        }
        
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }

        #endregion
        /// <summary>
        /// Creates instance of Album object.
        /// Will also call constructors for CD object for each CD in album.
        /// This is done by enumerating the directories under the current album directory,
        /// and iterating through them.
        /// </summary>
        /// <param name="path">Path of album</param>
        public Album(string path)
        {
            this.path = path;

            string[] cds = Directory.EnumerateDirectories(path).ToArray();

            try
            {
                if (cds.Length == 0)
                {
                    CDs = new CD[1];
                    CDs[0] = new CD(path);
                }
                else
                {
                    CDs = new CD[cds.Length];

                    for (int i = 0; i < cds.Length; i++)
                    {
                        CDs[i] = new CD(cds[i]);
                    }
                }

                coverArt = ExtractCoverArt();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// Helper for getting album title
        /// </summary>
        /// <returns>Title of album</returns>
        public string GetAlbumTitle()
        {
            return _CDs[0].FlacFiles[0].Tag.Album;
        }

        /// <summary>
        /// Finds the cover art in the album directory. Will search through all .jpg and .jpeg files in directory.
        /// </summary>
        /// <returns>Instance of Coverart object representing the coverart for this album</returns>
        private CoverArt ExtractCoverArt()
        {
            string[] coverArtPath1 = Directory.EnumerateFiles(path, "*.jpg").ToArray();
            string[] coverArtPath2 = Directory.EnumerateFiles(path, "*.jpeg").ToArray();

            if (coverArtPath1.Length == 0 && coverArtPath2.Length == 0)
            {
                throw new Exception("No cover art found with .jpg or .jpeg extensions");
            }
            else if (coverArtPath1.Length + coverArtPath2.Length > 1)
            {
                throw new Exception("Too many image files found");
            }
            else if (coverArtPath1.Length == 1)
            {
                return new CoverArt(coverArtPath1[0]);
            }
            else
            {
                return new CoverArt(coverArtPath2[0]);
            }

        }

        /// <summary>
        /// Retrieves all album media files which include flac and coverart files
        /// </summary>
        /// <returns>List of Flac and Coverart objects</returns>
        public MediaFile[] GetAlbumFiles()
        {
            List<MediaFile> fileList = new List<MediaFile>();

            fileList.Add(coverArt);
            foreach (CD cd in CDs)
            {
                foreach (MediaFile file in cd.FlacFiles)
                {
                    if (file.Extension.Equals(".flac"))
                    {
                        fileList.Add(file);
                    }
                }
            }

            return fileList.ToArray<MediaFile>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all flac files in album</returns>
        public FlacFile[] GetFlacs()
        {
            List<FlacFile> flacFiles = new List<FlacFile>();

            foreach (CD cd in _CDs)
            {
                foreach (FlacFile flac in cd.FlacFiles)
                {
                    flacFiles.Add(flac);
                }
            }

            return flacFiles.ToArray();
        }

        public string GetAlbumName()
        {
            try
            {
                return _CDs[0].FlacFiles[0].Tag.Album;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves UPC number for this album from the manifest
        /// </summary>
        /// <param name="manifest">UPC/ISRC manifest</param>
        /// <returns>UPC number as an Int64</returns>
        public Int64 GetUPC(Manifest manifest)
        {

            if (this.UPC != 0)
            {
                return this.UPC;
            }

            this.UPC = manifest.GetUPC(GetAlbumTitle());

            return this.UPC;
        }

        /// <summary>
        /// Retrieves ISRC numbers for all songs on this album using the starting ISRC from the manifest.
        /// </summary>
        /// <param name="manifest">UPC/ISRC manifest</param>
        /// <returns>String array of ISRC numbers in order of song order</returns>
        public string[] GetAllISRCs(Manifest manifest)
        {
            if (this.ISRCs != null)
            {
                return this.ISRCs;
            }

            string[] ISRCs = new string[GetTrackCount()];

            string startISRC = manifest.GetISRC(GetAlbumName());
            if (startISRC.Equals(""))
            {
                ISRCs.Initialize();
                return ISRCs;
            }
            
            string[] tokens = startISRC.Split('-');
            int start = Convert.ToInt32(tokens[tokens.Length - 1]);

            for (int i = 0; i < ISRCs.Length; i++)
            {
                string[] newTokens = (string[])tokens.Clone();
                newTokens[newTokens.Length - 1] = (start + i).ToString();
                ISRCs[i] = String.Join("-", newTokens);
            }

            this.ISRCs = ISRCs;
            return ISRCs;
        }

        public string GetArtists()
        {
            string[] artists = _CDs[0].FlacFiles[0].Tag.Performers;
            return String.Join(", ", artists);
        }

        public string GetGenre()
        {
            return _CDs[0].FlacFiles[0].Tag.FirstGenre;
        }

        public string GetReleaseDate()
        {
            return _CDs[0].FlacFiles[0].Tag.Year.ToString();
        }

        public int GetTrackCount()
        {
            return (int)_CDs[0].FlacFiles[0].Tag.TrackCount;
        }
    }
}
