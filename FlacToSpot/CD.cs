using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlacToSpot
{
    class CD
    {
        
        private FlacFile[] flacFiles;
        private string path;

        

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

        public string Path
        {
            get
            {
                return path;
            }
            private set
            {
                path = value;
            }
        }

        public CD()
        {

        }
    }
}
