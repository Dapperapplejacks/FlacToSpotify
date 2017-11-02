using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Spotifyify
{
    /// <summary>
    /// Base class for FlacFile and CoverArt objects
    /// </summary>
    class MediaFile
    {

        #region Fields

        /// <summary>
        /// File name of media file
        /// </summary>
        private string fileName;

        /// <summary>
        /// Path to media file
        /// </summary>
        private string path;

        /// <summary>
        /// Extension of media file (.xlsx, .jpg, etc...)
        /// </summary>
        private string extension;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MediaFile class
        /// </summary>
        /// <param name="path">Path to media file</param>
        public MediaFile(string path)
        {
            this.path = path;
            fileName = System.IO.Path.GetFileName(path);
            extension = System.IO.Path.GetExtension(path);
        }

        #endregion 

        #region Properties

        /// <summary>
        /// Gets or sets file name
        /// Setting FileName will also move the file as well
        /// </summary>
        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                string newName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), value);
                File.Move(path, newName);
                this.fileName = value;
                path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), this.fileName);
            }
        }

        /// <summary>
        /// Gets or sets path 
        /// </summary>
        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }

        /// <summary>
        /// Gets or sets file extension
        /// </summary>
        public string Extension
        {
            get
            {
                return this.extension;
            }
            private set
            {
                this.extension = value;
            }
        }

        #endregion
    }
}
