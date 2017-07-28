using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlacToSpot
{
    /// <summary>
    /// Represents the coverart, inherits all members from MediaFile class
    /// </summary>
    class CoverArt : MediaFile
    {
        public CoverArt(string path)
            : base(path)
        {
        }
    }
}
