using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Spotifyify
{
    /// <summary>
    /// Represents an album which contains a list of CDs, and other information about the album
    /// </summary>
    class Album
    {
        #region Fields
        /// <summary>
        /// Array of all CDs for this album
        /// </summary>
        private CD[] _CDs;

        /// <summary>
        /// Coverart object for this album
        /// </summary>
        private CoverArt coverArt;

        /// <summary>
        /// Path to this album
        /// </summary>
        private string path;

        /// <summary>
        /// UPC for this album
        /// </summary>
        private Int64 UPC;

        /// <summary>
        /// Array of ISRCS for each song in this album
        /// </summary>
        private string[] ISRCs;

        /// <summary>
        /// Array of FlacFile objects for this album
        /// </summary>
        private FlacFile[] flacs;

        #endregion

        #region Constructor

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

            //string[] cds = Directory.EnumerateDirectories(path).ToArray();

            string[] flacs = Directory.EnumerateFiles(path, "*.flac", SearchOption.AllDirectories).ToArray<string>();

            if (flacs.Length == 0)
            {
                throw new Exception("No FLAC files found");
            }

            uint discCount = TagLib.File.Create(flacs[0]).Tag.DiscCount;
            if (discCount == 0)
            {
                _CDs = new CD[1];
            }
            else
            {
                _CDs = new CD[discCount];
            }

            try
            {
                List<FlacFile> flacList = new List<FlacFile>();
                foreach (string fPath in flacs)
                {
                    FlacFile flac  = new FlacFile(fPath);
                    flacList.Add(flac);
                }

                this.flacs = flacList.ToArray<FlacFile>();

                for (int i = 0; i < _CDs.Length; i++)
                {
                     
                    FlacFile[] cdFlacs = this.flacs.Where(flac => ((flac.Tag.Disc == 0 && i == 0) || flac.Tag.Disc == i+1)).ToArray<FlacFile>();
                    _CDs[i] = new CD(cdFlacs);
                }

                /*
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
                }*/

                coverArt = ExtractCoverArt();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets CDs for this album
        /// </summary>
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

        /// <summary>
        /// Gets or sets coverart for this album
        /// </summary>
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

        /// <summary>
        /// Gets or sets path for this album
        /// </summary>
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

        /// <summary>
        /// Gets or sets FlacFile array
        /// </summary>
        public FlacFile[] Flacs
        {
            get { return flacs; }
            private set { flacs = value; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets album title
        /// </summary>
        /// <returns>Title of album</returns>
        public string GetAlbumTitle()
        {
            return _CDs[0].FlacFiles[0].Tag.Album;
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
        /// Gets Album title
        /// </summary>
        /// <returns>Album title</returns>
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

            string startISRC = manifest.GetISRC();
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

        /// <summary>
        /// Gets Artists on album
        /// </summary>
        /// <returns>Performers on album</returns>
        public string GetArtists()
        {
            string[] artists = _CDs[0].FlacFiles[0].Tag.Performers;
            return String.Join(", ", artists);
        }

        /// <summary>
        /// Gets Genre of album
        /// </summary>
        /// <returns>Genre of album</returns>
        public string GetGenre()
        {
            return _CDs[0].FlacFiles[0].Tag.FirstGenre;
        }

        /// <summary>
        /// Gets Release date of album
        /// </summary>
        /// <returns>Release data of album</returns>
        public string GetReleaseDate()
        {
            return _CDs[0].FlacFiles[0].Tag.Year.ToString();
        }

        /// <summary>
        /// Gets track count 
        /// </summary>
        /// <returns>Track count</returns>
        public int GetTrackCount()
        {
            int sum = 0;

            foreach(CD cd in _CDs){
                sum += cd.FlacFiles.Length;
            }
            return sum;
        }

        #endregion

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
    }
}
