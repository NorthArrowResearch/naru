using System;
using System.Windows.Forms;

namespace naru.ui
{
    public partial class ucInput : UserControl
    {
        public bool RequiredInput { get; internal set; }

        public event EventHandler<PathEventArgs> PathChanged;
        public event EventHandler<PathEventArgs> BrowseFile;
        public event EventHandler<PathEventArgs> SelectLayer;

        public System.IO.FileInfo Path
        {
            get
            {
                if (string.IsNullOrEmpty(txtPath.Text) || !System.IO.File.Exists(txtPath.Text))
                {
                    return null;
                }
                else
                {
                    return new System.IO.FileInfo(txtPath.Text);
                }
            }
        }

        public ucInput()
        {
            InitializeComponent();
        }

        public void Initialize(System.IO.FileInfo fiPath, bool bRequiredInput)
        {
            if (fiPath is System.IO.FileInfo)
            {
                Init(fiPath.FullName, bRequiredInput);
            }
        }

        public void Initialize(string sPath, bool bRequiredInput)
        {
            Init(sPath, bRequiredInput);
        }

        private void Init(string sPath, bool bRequiredInput)
        {
            txtPath.Text = sPath;
            RequiredInput = bRequiredInput;
        }

        private void ucInput_Load(object sender, EventArgs e)
        {
            // Hide select layer button when ArcMap is not the parent executable
            if (!System.Reflection.Assembly.GetEntryAssembly().FullName.ToLower().Contains("arcmap"))
            {
                cmdSelectLayer.Visible = false;
                cmdBrowse.Left = cmdSelectLayer.Left;
                txtPath.Width += cmdSelectLayer.Width + (cmdSelectLayer.Left - cmdBrowse.Right);
            }
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            if (PathChanged != null)
            {
                PathChanged(null, new PathEventArgs(Path, "TODO Form title"));
            }
        }

        public bool ValidateForm()
        {
            throw new NotImplementedException();
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            if (BrowseFile != null)
            {
                BrowseFile(null, new PathEventArgs(Path, "TODO Form title"));
            }
        }

        private void cmdSelectLayer_Click(object sender, EventArgs e)
        {
            if (SelectLayer != null)
            {
                SelectLayer(null, new PathEventArgs(Path, "TODO Form title"));
            }
        }
    }
}
