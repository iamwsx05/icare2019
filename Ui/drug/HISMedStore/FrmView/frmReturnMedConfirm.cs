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
    /// ҩ����ҩȷ�ϴ���
    /// </summary>
    public partial class frmReturnMedConfirm : Form
    {   
        /// <summary>
        /// ���캯��
        /// </summary>
        public frmReturnMedConfirm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ��ҩ������
        /// </summary>
        internal clsControlReturnMedicine objControllReturnMed;
        /// <summary>
        /// ȷ��Ա��id
        /// </summary>
        internal string m_strEmpid;
        private void m_btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.m_txtConfirmid.Text.Trim() == string.Empty)
            {
                MessageBox.Show("��������ȷ����Ա�����ţ�");
                return;
            }
            if (this.objControllReturnMed.m_mthJudgeExistEmp(this.m_txtConfirmid.Text.Trim(), this.m_txtPwd.Text.Trim(),ref m_strEmpid))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("���������Ӧ��Ա�����ź����룬��ȷ�Ϲ��ź������Ƿ���ȷ��");
                return;
            }
        }

        private void m_txtConfirmid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}