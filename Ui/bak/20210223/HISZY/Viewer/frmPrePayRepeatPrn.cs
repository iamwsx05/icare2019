using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 按金单重打
    /// </summary>
    public partial class frmPrePayRepeatPrn : Form
    {
        private string OldNo = "";
        private string newno = "";
        /// <summary>
        /// 新号
        /// </summary>
        public string NewNo
        {
            get
            {
                return newno;
            }
        }
        private string prntype = "";
        /// <summary>
        /// 重打类型 1 原号 2 新号
        /// </summary>
        public string PrnType
        {
            get
            {
                return prntype;
            }
        }

        string EmpID { get; set; }

        /// <summary>
        /// 预交类型 1 正常 2 手工
        /// </summary>
        private int PreType = 1;

        /// <summary>
        /// 构造
        /// </summary>
        public frmPrePayRepeatPrn(string oldno, int pretype, string empId)
        {
            InitializeComponent();

            OldNo = oldno;
            PreType = pretype;
            EmpID = empId;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrePayRepeatPrn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmPrePayRepeatPrn_Load(object sender, EventArgs e)
        {
            this.lblOldNo.Text = OldNo;

            this.rdo1.Checked = true;
            this.rdo2.Checked = false;
            this.txtNewNo.Text = "";
            this.txtNewNo.Enabled = false;
        }

        private void rdo1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdo1.Checked)
            {
                this.txtNewNo.Text = "";
                this.txtNewNo.Enabled = false;

                this.txtCustomNo.Text = "";
                this.txtCustomNo.Enabled = false;
            }
        }

        private void rdo2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdo2.Checked)
            {
                this.txtCustomNo.Text = "";
                this.txtCustomNo.Enabled = false;

                this.txtNewNo.Enabled = true;
                this.txtNewNo.Focus();

                string PrepayBillNo = clsPublic.m_strGetCurrInvoiceNo(EmpID, 2);
                if (PrepayBillNo == "")
                {
                    return;
                }
                if (clsPublic.m_blnCheckPrepayNoExpression(PrepayBillNo))
                {
                    if (!clsPublic.m_blnCheckPrepayNoIsUsed(PrepayBillNo, 0))
                    {
                        this.txtNewNo.Text = PrepayBillNo;
                    }
                    else
                    {
                        MessageBox.Show("当前预交金收据的编号已被使用，请输入新号(与当前打印票据号相同)。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("当前预交金收据的编号规则不正确，请仔细检查。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void rdo3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdo2.Checked)
            {
                this.txtNewNo.Text = "";
                this.txtNewNo.Enabled = false;

                this.txtCustomNo.Enabled = true;
                this.txtCustomNo.Focus();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.rdo1.Checked)
            {
                prntype = "1";
            }
            else if (this.rdo2.Checked)
            {
                newno = this.txtNewNo.Text.Trim();
                if (newno == "")
                {
                    MessageBox.Show("请输入新号码!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!clsPublic.m_blnCheckPrepayNoExpression(newno))
                {
                    MessageBox.Show("本地当前预交金收据的编号不符合编码规则，请重新设置(与当前打印票据号相同)。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtNewNo.Focus();
                    this.txtNewNo.SelectAll();
                    return;
                }

                if (clsPublic.m_blnCheckPrepayNoIsUsed(newno, PreType))
                {
                    MessageBox.Show("本地当前预交金收据的编号已经被使用，请重新设置(与当前打印票据号相同)。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtNewNo.Focus();
                    this.txtNewNo.SelectAll();
                    return;
                }

                prntype = "2";
            }
            else if (this.rdo3.Checked)
            {
                newno = this.txtCustomNo.Text.Trim();
                if (newno == "")
                {
                    MessageBox.Show("请输入号码!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!clsPublic.m_blnCheckPrepayNoExpression(newno))
                {
                    MessageBox.Show("本地当前预交金收据的编号不符合编码规则，请重新设置(与当前打印票据号相同)。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtNewNo.Focus();
                    this.txtNewNo.SelectAll();
                    return;
                }
                prntype = "3";
            }

            this.DialogResult = DialogResult.OK;
        }

    }
}