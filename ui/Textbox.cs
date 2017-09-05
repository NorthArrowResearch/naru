using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace naru.ui
{
    public class Textbox
    {
        public static void SetTextBoxToFolder(ref System.Windows.Forms.TextBox txt, string sFolder)
        {
            if (!string.IsNullOrEmpty(sFolder) && System.IO.Directory.Exists(sFolder))
                txt.Text = sFolder;
        }

        public static bool ValidateTextBoxFolder(ref System.Windows.Forms.TextBox txt)
        {
            return !(string.IsNullOrEmpty(txt.Text) || !System.IO.Directory.Exists(txt.Text));
        }

        public static DialogResult BrowseFolder(ref System.Windows.Forms.TextBox txt)
        {
            FolderBrowserDialog frm = new FolderBrowserDialog();
            DialogResult eResult = frm.ShowDialog();
            if (eResult == System.Windows.Forms.DialogResult.OK)
                txt.Text = frm.SelectedPath;

            return eResult;
        }
    }
}
