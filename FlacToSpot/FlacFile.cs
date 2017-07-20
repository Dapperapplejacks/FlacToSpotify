using System;
using System.IO;
using System.Collections.Generic;

namespace FlacToSpot
{
    class FlacFile : MediaFile
    {
        private Dictionary<string, string> commentFields;

        public FlacFile(string path)
            : base(path)
        {
            commentFields = new Dictionary<string, string>();
        }
    }
}
