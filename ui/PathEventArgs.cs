using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace naru.ui
{
    public class PathEventArgs : EventArgs
    {
        public System.IO.FileInfo Path { get; internal set; }

        public PathEventArgs(System.IO.FileInfo fiPath) : base()
        {
            Path = fiPath;
        }
    }
}