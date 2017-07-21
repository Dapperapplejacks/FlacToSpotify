﻿using System;
using System.IO;
using System.Collections.Generic;
using TagLib;
using System.Reflection;

namespace FlacToSpot
{
    class FlacFile : MediaFile
    {
        private Tag tag;

        public Tag Tag
        {
            get
            {
                return this.tag;
            }
            private set{
                tag = value;
            }
        }

        public FlacFile(string path)
            : base(path)
        {
            var file = TagLib.File.Create(path);

            tag = file.Tag;
        }

    }
}
