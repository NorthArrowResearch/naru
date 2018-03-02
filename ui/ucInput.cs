using System;
using System.IO;
using System.Windows.Forms;

namespace naru.ui
{
    public partial class ucInput : UserControl
    {
        public string Noun { get; protected set; }

        private FileInfo _FullPath;
        public FileInfo FullPath
        {
            get { return _FullPath; }

            set
            {
                if (_FullPath == null && value is FileInfo || _FullPath is FileInfo && !_FullPath.Equals(value))
                {
                    _FullPath = value;
                    if (PathChanged != null)
                    {
                        PathChanged(null, new PathEventArgs(_FullPath, Noun, this.Handle));
                    }
                }
            }
        }

        public event EventHandler<PathEventArgs> PathChanged;
        public event EventHandler<PathEventArgs> Browse;
        public event EventHandler<PathEventArgs> AddToMap;

        public ucInput()
        {
            InitializeComponent();
        }

        protected void InitializeExisting(string sNoun, System.IO.FileInfo fiPath, string relativePath)
        {
            Noun = sNoun;
            FullPath = fiPath;
            txtPath.Text = relativePath;
            cmdBrowse.Visible = false;

            if (System.Reflection.Assembly.GetEntryAssembly() == null || System.Reflection.Assembly.GetEntryAssembly().FullName.ToLower().Contains("arcmap"))
            {
                txtPath.Width = cmdBrowse.Right - txtPath.Left;
            }
            else
            {
                cmdAddToMap.Visible = false;
                txtPath.Width = cmdAddToMap.Right - txtPath.Left;
            }
        }

        protected void InitializeBrowseNew(string sNoun)
        {
            Noun = sNoun;

            // Moving the browse button needs to happen after it's right edge is used to adjust textbox
            txtPath.Width = cmdBrowse.Right - txtPath.Left;
            cmdAddToMap.Visible = false;
            cmdBrowse.Left = cmdAddToMap.Left;
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            if (Browse != null)
            {
                Browse(txtPath, new PathEventArgs(FullPath, string.Format("Specify {0}", Noun), this.Handle));
            }
        }

        private void cmdAddToMap_Click(object sender, EventArgs e)
        {
            if (AddToMap != null)
            {
                AddToMap(txtPath, new PathEventArgs(FullPath, string.Format("AddTo {0} To Map", Noun), this.Handle));
            }
        }
    }
}
