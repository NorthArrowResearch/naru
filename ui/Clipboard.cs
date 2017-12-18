using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PipSuite.naru.ui
{
    public class Clipboard
    {
        public static void SetText(string text)
        {
            System.Windows.Forms.Clipboard.SetData(DataFormats.Text, (Object)text);
        }
    }
}
