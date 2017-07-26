using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace FlacToSpot
{
    class Album
    {
        #region Properties
        private CD[] _CDs;
        private CoverArt coverArt;
        private string path;
        private string UPC;
        private string startISRC;

        private const string label = "Orange Mountain Music";

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

                coverArt = ExtractCoverArt(path);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetAlbumTitle()
        {
            return _CDs[0].FlacFiles[0].Tag.Album;
        }

        private CoverArt ExtractCoverArt(string path)
        {
            string[] coverArtPath = Directory.EnumerateFiles(path, "*.jpg").ToArray();

            if (coverArtPath.Length < 1)
            {
                throw new Exception("No cover art found");
            }
            return new CoverArt(coverArtPath[0]);

        }

        public MediaFile[] GetAlbumFiles()
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            List<MediaFile> fileList = new List<MediaFile>();
            //MediaFile[] ret = new MediaFile[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                string ext = System.IO.Path.GetExtension(files[i]);

                if (ext.Equals(".jpg") || ext.Equals(".jpeg") || ext.Equals(".flac"))
                {
                    fileList.Add(new MediaFile(files[i]));
                }


                //ret[i] = new MediaFile(files[i]);
            }

            return fileList.ToArray<MediaFile>();
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

        public string GetUPC(Manifest manifest)
        {

            if (this.UPC != null)
            {
                return this.UPC;
            }

            Range titleCol = null;
            Range upcCol = null;
            bool found = false;

            try
            {
                titleCol = manifest.GetColumn(2);
                upcCol = manifest.GetColumn(3);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to obtain Album Title/UPC column(s) of UPC manifest");
            }


            return this.UPC;
        }

        public string GetStartISRC(Manifest manifest)
        {
            if (this.startISRC != null)
            {
                return this.startISRC;
            }
            return null;
        }

        public string GetLabel()
        {
            return this.label;
        }
    }
}
