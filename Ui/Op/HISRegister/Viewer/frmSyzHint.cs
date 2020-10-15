using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmSyzHint : Form
    {
        string HintInfo { get; set; }

        public frmSyzHint(string _hintInfo)
        {
            InitializeComponent();
            HintInfo = _hintInfo;
        }

        private void frmSyzHint_Load(object sender, EventArgs e)
        {
            this.lblInfo.Text = this.HintInfo;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
