using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.emr.EMR_SynchronousCase
{
    /// <summary>
    /// 病案警告
    /// </summary>
    public partial class frmEMR_WarningDialog : Form
    {
        /// <summary>
        /// 病案警告
        /// </summary>
        public frmEMR_WarningDialog()
        {
            InitializeComponent();
        }

        private void m_cmdNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void m_cmdYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}