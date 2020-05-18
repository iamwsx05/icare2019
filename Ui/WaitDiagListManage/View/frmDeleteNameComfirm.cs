using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmDeleteNameComfirm : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        string m_strOpId = "";
        public frmDeleteNameComfirm(string p_strOpId)
        {
            m_strOpId = p_strOpId;
            InitializeComponent();
        }

        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtID.Text.Trim() == "")
                {
                    MessageBox.Show("请输入工号");
                    this.txtID.Focus();
                    return;
                }
                DataTable dt;
                clsDcl_InvoiceManage m_objManage = new clsDcl_InvoiceManage();;

                long ret = m_objManage.m_mthGetEmployeeInfo(this.txtID.Text.Trim(), out dt, "");
                if (ret > 0 && dt.Rows.Count > 0)
                {
                    this.txtName.Text = dt.Rows[0]["LASTNAME_VCHR"].ToString().Trim();
                    this.txtID.Tag = dt.Rows[0]["empid_chr"].ToString().Trim();
                    this.txtPS.Tag = dt.Rows[0]["psw_chr"].ToString().Trim();
                    this.txtPS.Focus();
                }
                else
                {
                    MessageBox.Show("输入的工号不正确");
                    this.txtID.Focus();
                    return;
                }
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if ( this.txtID.Tag != null)
            {
                if (m_strOpId == this.txtID.Tag.ToString())
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("对不起,您无权限删除别人的签名");
                }
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btOK.Focus();
            }
        }
    }
}