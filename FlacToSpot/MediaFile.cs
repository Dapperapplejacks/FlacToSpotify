﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlacToSpot
{
    interface MediaFile
    {
        string fileName
        {
            get;
            set;
        }
        string path
        {
            get;
            set;
        }

        File file
        {
            get;
            set;
        }
        
    }
}
