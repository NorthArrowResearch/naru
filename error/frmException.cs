using System;
using System.Windows.Forms;

namespace naru.error
{
    public partial class frmException : Form
    {
        private Boolean _bDetailsExpanded = true;
        private string _sFormattedException;
        private string _sType;
        private string _NewIssueURL;

        public frmException(string sType, string sFormattedException, string newIssueURL)
        {
            _sType = sType;
            _sFormattedException = sFormattedException;
            _NewIssueURL = newIssueURL;
            InitializeComponent();

            if (string.IsNullOrEmpty(newIssueURL))
            {
                cmdSend.Visible = false;
                txtErrorMessage.Dock = DockStyle.Fill;
            }
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
            lblException.Text = _sType;
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

            ChangeFormState(false);
        }

        private void cmdSend_Click(object sender, EventArgs e)
        {
            ui.Clipboard.SetText(txtErrorMessage.Text);
            MessageBox.Show("The error information has been copied to the clipboard." +
                " You will now be redirected to the web site where you can review existing issues" +
                " and log a new issue if your issue is not already being tracked.", cmdSend.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            System.Diagnostics.Process.Start(_NewIssueURL);
        }
    }
}
