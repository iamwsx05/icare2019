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
    public partial class frmCovidMsg : Form
    {
        public frmCovidMsg(string _msg)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.lblMsg.Text = _msg;
            }
        }

        private void frmCovidMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
