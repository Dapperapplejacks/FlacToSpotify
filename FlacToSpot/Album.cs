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

        public CoverArt coverArt
        {
            get
            {
                return coverArt;
            }
            private set
            {
                coverArt = value;
            }
        }

        public FlacFile[] flacFiles
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
            this.coverArt = coverArt;
            this.flacFiles = flacFiles;
        }

    }
}
