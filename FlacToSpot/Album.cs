using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlacToSpot
{
    class Album
    {
        #region Properties
        private CD[] CDs;
        private CoverArt coverArt;

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
        
        private string path;
        public string Path
        {
            get
            {
                return path;
            }
            private set
            {
                path = value;
            }
        }

        #endregion

        public Album(CoverArt coverArt, FlacFile[] flacFiles, string path)
        {
            CoverArt = coverArt;
            FlacFiles = flacFiles;
            this.path = path;
        }

        public string GetAlbumName()
        {
            if (flacFiles[0] != null)
            {
                return flacFiles[0].Tag.Album;
            }
            else
            {
                return null;
            }
        }

    }
}
