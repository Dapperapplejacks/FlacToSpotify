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

        private CoverArt coverArt;
        private FlacFile[] flacFiles;

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

        public FlacFile[] FlacFiles
        {
            get
            {
                return flacFiles;
            }
            private set
            {
                flacFiles = value;
            }
        }

        #endregion

        public Album(CoverArt coverArt, FlacFile[] flacFiles)
        {
            CoverArt = coverArt;
            FlacFiles = flacFiles;
        }

    }
}
