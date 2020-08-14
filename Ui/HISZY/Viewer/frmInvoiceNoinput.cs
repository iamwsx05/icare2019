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
    /// ��Ʊ������UI
    /// </summary>
    public partial class frmInvoiceNoinput : Form
    {
        /// <summary>
        /// ����
        /// </summary>
        public frmInvoiceNoinput()
        {
            InitializeComponent();
        }        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (this.m_blnCheck(this.txtInvono.Text.Trim()))
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmInvoiceNoinput_Load(object sender, EventArgs e)
        {
            this.txtInvono.Focus();
            this.txtInvono.Select(0, 0);
        }

        private void frmInvoiceNoinput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtInvono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.m_blnCheck(this.txtInvono.Text.Trim()))
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        /// <summary>
        /// ��Ʊ����Ч�Լ��
        /// </summary>
        /// <param name="invono">��ǰ��Ʊ��</param>
        /// <returns>true ͨ��</returns>
        private bool m_blnCheck(string invono)
        {            
            if (!clsPublic.m_blnCheckInvoExpression(invono))
            {
                MessageBox.Show("����ķ�Ʊ�Ų����Ϲ涨�ķ�Ʊ�Ź������������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (clsPublic.m_blnCheckInvoIsUsed(invono))
            {
                MessageBox.Show("����ķ�Ʊ���Ѿ���ʹ�ã����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
    }
}