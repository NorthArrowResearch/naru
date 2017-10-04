using System;
using System.Windows.Forms;

namespace error
{
    public partial class frmException : Form
    {
        private Boolean _bDetailsExpanded = true;
        private string _sFormattedException;
        private string _sType;

        public frmException(string sType, string sFormattedException)
        {
            _sType = sType;
            _sFormattedException = sFormattedException;
            InitializeComponent();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            ChangeFormState(!_bDetailsExpanded);
        }

        private void ChangeFormState(bool bExpand)
        {
            if (bExpand)
            {
                Height = 400;
                _bDetailsExpanded = true;
                btnDetails.Text = "Details <<";
            }
            else
            {
                Height = this.MinimumSize.Height;
                _bDetailsExpanded = false;
                btnDetails.Text = "Details >>";
            }
        }

        private void ExceptionForm_Load(object sender, EventArgs e)
        {
            lblException.Text = String.Format("A {0} occured", _sType);
            txtErrorMessage.Text = _sFormattedException;
            int iWidth = lblException.Width + btnOK.Width + 200;
            if (iWidth < MinimumSize.Width)
            {
                Width = MinimumSize.Width;
            }
            else if (iWidth > Width)
            {
                Width = iWidth;
            }
        }
    }
}
