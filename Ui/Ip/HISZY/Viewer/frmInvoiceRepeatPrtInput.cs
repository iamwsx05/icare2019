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
    /// ��Ʊ�ش��������UI
    /// </summary>
    public partial class frmInvoiceRepeatPrtInput : Form
    {               
        /// <summary>
        /// ����
        /// </summary>
        public frmInvoiceRepeatPrtInput(string invono)
        {
            InitializeComponent();

            OldInvoNo = invono;
        }

        /// <summary>
        /// ԭ��Ʊ��
        /// </summary>
        private string OldInvoNo = "";
        /// <summary>
        /// �·�Ʊ��
        /// </summary>
        private string newno = "";
        /// <summary>
        /// �·�Ʊ��
        /// </summary>
        public string NewInvoNo
        {
            get
            {
                return newno;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrePayNoInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtNewInvoNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnOK.Focus();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            newno = this.txtNewInvoNo.Text.Trim();

            if (newno == "")
            {
                MessageBox.Show("�������µķ�Ʊ�š�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (newno == OldInvoNo)
            {
                MessageBox.Show("�·�Ʊ�Ų���ͬ��ԭ��Ʊ�ţ����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtNewInvoNo.SelectAll();
                this.txtNewInvoNo.Focus();
                return;
            }

            if (!clsPublic.m_blnCheckInvoExpression(newno))
            {
                MessageBox.Show("����ķ�Ʊ�Ų����Ϲ涨�ķ�Ʊ�Ź������������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtNewInvoNo.SelectAll();
                this.txtNewInvoNo.Focus();
                return;
            }

            if (clsPublic.m_blnCheckInvoIsUsed(newno))
            {
                MessageBox.Show("����ķ�Ʊ���Ѿ���ʹ�ã����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtNewInvoNo.SelectAll();
                this.txtNewInvoNo.Focus();
                return;
            }                   

            this.DialogResult = DialogResult.OK;
        }

        private void frmInvoiceRepeatPrtInput_Load(object sender, EventArgs e)
        {
            this.lblOldInvoNo.Text = OldInvoNo;
            this.txtNewInvoNo.Focus();
        }       
    }
}