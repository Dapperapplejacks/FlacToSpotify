using System;
using System.IO;

namespace FlacToSpot
{
    class FlacFile : MediaFile
    {
        private CommentFields commentFields;

        public FlacFile(string path)
            : base(path)
        {
            
        }
    }
}
