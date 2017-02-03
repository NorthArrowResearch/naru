using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace naru.os
{
    public class Folder
    {
        public static void BrowseFolder(ref TextBox txt, string sFormDescription, string sInitialDir)
        {
            FolderBrowserDialog frm = new FolderBrowserDialog();
            frm.Description = sFormDescription;

            if (!string.IsNullOrEmpty(txt.Text) && System.IO.Directory.Exists(txt.Text))
                frm.SelectedPath = txt.Text;

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txt.Text = frm.SelectedPath;
        }
    }
}
