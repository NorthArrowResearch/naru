using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace naru.ui
{
    public partial class frmMsgBoxWithCheckbox : Form
    {
        public frmMsgBoxWithCheckbox(string sMessage)
        {
            InitializeComponent();
            lblMessage.Text = sMessage;
        }
    }
}
