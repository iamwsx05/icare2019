using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Controls.Emr
{
    public partial class frmModifyPatBasicInfo : System.Windows.Forms.Form
    {
        private string m_strCaption = string.Empty;
        private string m_strName = string.Empty;
        private bool m_blnShowCaption = true;
        public string PatientInfo { get; set; }

        public frmModifyPatBasicInfo(string p_strCaption, string p_strName, bool p_blnShowCaption)
        {
            InitializeComponent();
            this.m_strCaption = p_strCaption;
            this.m_strName = p_strName;
            this.m_blnShowCaption = p_blnShowCaption;
        }

        private void frmModifyPatBasicInfo_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.m_strCaption))
            {
                this.lblCaption.Text = this.m_strCaption + "：";
                this.m_strName = this.m_strName.Replace(this.lblCaption.Text, "");
                this.m_strName = this.m_strName.Replace(this.m_strCaption + ":", "");
            }
            this.txtName.Text = this.m_strName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PatientInfo = this.txtName.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmModifyPatBasicInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}
