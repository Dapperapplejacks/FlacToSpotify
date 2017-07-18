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

        public string fileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        public string path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }

        public System.IO.File file
        {
            get
            {
                return file;
            }
            set
            {
                file = value;
            }
        }

        #endregion

        public MediaFile(string fileName, string path, File file)
        {
            this.fileName = fileName;
            this.path = path;
            this.file = file;
        }
    }
}
