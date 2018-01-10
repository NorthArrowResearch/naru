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
        public string FormTitle { get; internal set; }
        public IntPtr hWndParent { get; internal set; }

        public PathEventArgs(System.IO.FileInfo fiPath, string sFormTitle, IntPtr ParentWindowHandle) : base()
        {
            Path = fiPath;
            FormTitle = sFormTitle;
            hWndParent = ParentWindowHandle;
        }
    }
}