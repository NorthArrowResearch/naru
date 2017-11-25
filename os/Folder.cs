using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace naru.os
{
    public class Folder
    {
        public static DialogResult BrowseFolder(TextBox txt, string sFormDescription, string sInitialDir)
        {
            FolderBrowserDialog frm = new FolderBrowserDialog();
            frm.Description = sFormDescription;

            if (!string.IsNullOrEmpty(txt.Text) && System.IO.Directory.Exists(txt.Text))
                frm.SelectedPath = txt.Text;

            DialogResult eResult = frm.ShowDialog();
            if (eResult == System.Windows.Forms.DialogResult.OK)
                txt.Text = frm.SelectedPath;

            return eResult;
        }

        public string BrowseToFolder(string sFormTitle, string sDefaultFolder = "")
        {
            string sFolder = string.Empty;
            //
            // If a default folder is passed, then check it exists and
            // then use it as the initial folder.
            //
            if (!string.IsNullOrEmpty(sDefaultFolder))
            {
               System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(sDefaultFolder);
                if (dir.Exists)
                {
                    sFolder = sDefaultFolder;
                }
            }

            FolderBrowserDialog FolderBrowserDialog = new FolderBrowserDialog();
            // Change the .SelectedPath property to the default location
            var _with1 = FolderBrowserDialog;
            _with1.RootFolder = Environment.SpecialFolder.Desktop;
            if (!string.IsNullOrEmpty(sFolder))
            {
                _with1.SelectedPath = sFolder;
            }
            _with1.Description = sFormTitle;

            if (_with1.ShowDialog() == DialogResult.OK)
            {
                sFolder = _with1.SelectedPath;
                //
                //Saving this setting is now the responsibility of the calling function
                // My.Settings.LastBrowsedFolder = sFolder
                //My.Settings.Save()
            }
            else
            {
                sFolder = string.Empty;
            }

            return sFolder;
        }
    }
}
