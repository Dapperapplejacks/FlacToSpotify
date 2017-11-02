using TagLib;

namespace Spotifyify
{
    /// <summary>
    /// Abstraction for Flac file
    /// </summary>
    class FlacFile : MediaFile
    {
        /// <summary>
        /// Field for tag
        /// </summary>
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

        /// <summary>
        /// Initializaes a new instance of the FlacFile class
        /// </summary>
        /// <param name="path"></param>
        public FlacFile(string path)
            : base(path)
        {
            tag = null;
            var file = TagLib.File.Create(path);

            Tag = file.Tag;
        }
    }
}
