namespace Spotifyify
{
    /// <summary>
    /// Represents CD which keeps track of flac files on that CD
    /// </summary>
    class CD
    {
        /// <summary>
        /// Field for FlacFile array
        /// </summary>
        private FlacFile[] flacFiles;

        /// <summary>
        /// Gets or sets flacFiles field
        /// </summary>
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

        /// <summary>
        /// Creates instance of CD, finds flac files in the CD's directory
        /// </summary>
        /// <param name="path">Path of this CD directory</param>
        public CD(FlacFile[] flacs)
        {
            flacFiles = flacs;
        }
    }
}
