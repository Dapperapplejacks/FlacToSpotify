namespace Spotifyify
{
    /// <summary>
    /// Represents the coverart, inherits all members from MediaFile class
    /// </summary>
    class CoverArt : MediaFile
    {
        /// <summary>
        /// Initializes a new instance of the CoverArt class
        /// </summary>
        /// <param name="path">Path to coverart file</param>
        public CoverArt(string path)
            : base(path)
        {
        }
    }
}
