using System;
using System.IO;

namespace FlacToSpot
{
    class FlacFile : MediaFile
    {
        private CommentFields commentFields;

        public FlacFile(CommentFields commentFields, MediaFile file) : base(file.fileName, file.path)
        {
            this.commentFields = commentFields;
        }
    }
}
