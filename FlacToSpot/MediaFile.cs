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


        #endregion

        public MediaFile(string fileName, string path)
        {
            this.fileName = fileName;
            this.path = path;
            
        }
    }
}
