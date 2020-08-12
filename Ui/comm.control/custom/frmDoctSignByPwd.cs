using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    public partial class frmDoctSignByPwd : System.Windows.Forms.Form
    {
        private string OrgPassWord { get; set; }

        public frmDoctSignByPwd(string empNo, string empName, string techName, string orgPassWord)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.txtEmpNo.Text = empNo;
                this.txtEmpName.Text = empName;
                this.txtTechName.Text = techName;
                this.OrgPassWord = orgPassWord;
                if (Common.Entity.GlobalHospital.Current == Entity.EnumHospitalCode.东莞茶山)
                {
                    this.OrgPassWord = (new clsSymmetricAlgorithm()).Decrypt(orgPassWord, clsSymmetricAlgorithm.enmSymmetricAlgorithmType.DES);
                }
            }
        }

        private void Confirm()
        {
            string strPwd = this.txtPassWord.Text;

            if (strPwd == this.OrgPassWord)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                DialogBox.Msg("密码输入错误，请重新输入。");
                this.txtPassWord.Focus();
            }
        }

        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Confirm();
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Confirm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDoctSignByPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
