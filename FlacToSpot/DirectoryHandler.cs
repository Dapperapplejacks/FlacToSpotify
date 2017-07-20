using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlacToSpot
{
    class DirectoryHandler
    {

        private string path = "";
        private MediaFile[] files;
        private int fileCount;


        public string Path
        {
            get
            {
                return this.path;
            }
            private set
            {
                this.path = value;
            }
        }

        public MediaFile[] Files
        {
            get
            {
                return this.files;
            }
            private set
            {
                this.files = value;
                this.fileCount = this.files.Length;
            }
        }

        public int FileCount
        {
            get
            {
                return this.fileCount;
            }
            private set
            {
                this.fileCount = value;
            }
        }

        public DirectoryHandler(string dirpath)
        {
            this.path = dirpath;

            IEnumerable<string> test = Directory.EnumerateFiles(dirpath);


            string[] filePaths = Directory.EnumerateFiles(dirpath).ToArray<string>();

            files = new MediaFile[filePaths.Length];

            fileCount = filePaths.Length;

            System.Console.WriteLine("Files:");

            for (int i = 0; i < files.Length; i++)
            {
                switch (System.IO.Path.GetExtension(filePaths[i]))
                {
                    case ".flac": case ".FLAC":
                        files[i] = new FlacFile(filePaths[i]);
                        break;
                    case ".jpeg": case ".JPEG": case ".png": case ".PNG":
                        files[i] = new CoverArt(filePaths[i]);
                        break;
                    default:
                        files[i] = new MediaFile(filePaths[i]);
                        break;
                }
                System.Console.WriteLine(filePaths[i]);
                
            }

            //System.Console.WriteLine("Length of file list: {0}", fileCount);
        }

    }
}
