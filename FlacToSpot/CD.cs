using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


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

        public CD(string path)
        {
            this.path = path;
            
            string[] flacFilePaths = Directory.EnumerateFiles(path, "*.flac").ToArray();

            if (flacFilePaths.Length <= 0)
            {
                throw new Exception("No FLAC files found");
            }

            flacFiles = new FlacFile[flacFilePaths.Length];

            for (int i = 0; i < flacFilePaths.Length; i++)
            {
                flacFiles[i] = new FlacFile(flacFilePaths[i]);
            }
        }
    }
}
