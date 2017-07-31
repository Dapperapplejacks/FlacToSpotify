using System;
using System.IO;
using System.Collections.Generic;
using TagLib;
using System.Reflection;

namespace FlacToSpot
{
    /// <summary>
    /// Abstraction for Flac file
    /// </summary>
    class FlacFile : MediaFile
    {

        private Tag tag;
        
        /// <summary>
        /// Reference to data stored in Flac file, such as Album title, track number, Artist, etc..
        /// See TagLib for more info.
        /// </summary>
        public Tag Tag
        {
            get
            {
                return this.tag;
            }
            private set{
                tag = value;
            }
        }

        public FlacFile(string path)
            : base(path)
        {
            tag = null;
            var file = TagLib.File.Create(path);

            Tag = file.Tag;
        }

    }
}
