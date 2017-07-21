using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlacToSpot
{
    class MediaFile
    {

        #region Properties

        private string fileName;
        private string path;
        private string extension;

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                File.Move(fileName, value);
                this.fileName = value;
                
            }
        }

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

        public MediaFile(string path)
        {
            this.path = path;
            fileName = System.IO.Path.GetFileName(path);
            extension = System.IO.Path.GetExtension(path);
        }
    }
}
