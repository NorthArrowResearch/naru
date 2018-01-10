using System;
using System.Windows.Forms;

namespace naru.ui
{
    public partial class ucInput : UserControl
    {
        public string Noun { get; protected set; }
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

        public void Initialize(string sNoun, System.IO.FileInfo fiPath, bool bRequiredInput)
        {
            Noun = sNoun;
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
            // Needed by the Visual Studio form designer
            if (System.Reflection.Assembly.GetEntryAssembly() == null)
                return;

            // Hide select layer button when ArcMap is not the parent executable
            if (!System.Reflection.Assembly.GetEntryAssembly().FullName.ToLower().Contains("arcmap"))
            {
                cmdSelectLayer.Visible = false;
                txtPath.Width = cmdBrowse.Right - txtPath.Left;
                cmdBrowse.Left = cmdSelectLayer.Left;
            }
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            if (PathChanged != null)
            {
                PathChanged(null, new PathEventArgs(Path, Noun, this.Handle));
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
                BrowseFile(txtPath, new PathEventArgs(Path, string.Format("Specify {0}", Noun), this.Handle));
            }
        }

        private void cmdSelectLayer_Click(object sender, EventArgs e)
        {
            if (SelectLayer != null)
            {
                SelectLayer(txtPath, new PathEventArgs(Path, string.Format("Select {0} Map Layer", Noun), this.Handle));
            }
        }

        public void ClearSelectedItem()
        {
            txtPath.TextChanged -= txtPath_TextChanged;
            txtPath.Text = string.Empty;
            txtPath.TextChanged += txtPath_TextChanged;
        }
    }
}
