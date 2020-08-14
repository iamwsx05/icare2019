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
    /// ���밴�𵥺�UI
    /// </summary>
    public partial class frmPrePayNoInput : Form
    {
        /// <summary>
        /// ����
        /// </summary>
        public frmPrePayNoInput()
        {
            InitializeComponent();
        }

        private string newno = "";
        /// <summary>
        /// �º�
        /// </summary>
        public string NewNo
        {
            set
            {
                newno = value;
            }
            get
            {
                return newno;
            }            
        }

        /// <summary>
        /// (����)֧����ʽ
        /// </summary>
        private string cuycate = "1";
        /// <summary>
        /// (����)֧����ʽ
        /// </summary>
        public string CuyCate
        {
            get
            {
                return cuycate;
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

        private void txtNewNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnOK.Focus();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            newno = this.txtNewNo.Text.Trim();

            if (newno == "")
            {
                MessageBox.Show("�����밴�𵥾ݺš�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!clsPublic.m_blnCheckPrepayNoExpression(newno))
            {
                MessageBox.Show("���ص�ǰԤ�����վݵı�Ų����ϱ����������������(�뵱ǰ��ӡƱ�ݺ���ͬ)��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //�ڶ���������ʱĬ�� 1(����Ԥ��), �Ժ����ҵ�����������
            if (clsPublic.m_blnCheckPrepayNoIsUsed(newno, 1))
            {
                MessageBox.Show("���ص�ǰԤ�����վݵı���Ѿ���ʹ�ã�����������(�뵱ǰ��ӡƱ�ݺ���ͬ)��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);              
                return;
            }

            cuycate = Convert.ToString(this.cbopaytype.SelectedIndex + 1);

            this.DialogResult = DialogResult.OK;
        }

        private void frmPrePayNoInput_Load(object sender, EventArgs e)
        {
            if (newno.Trim() != "")
            {
                this.txtNewNo.Text = newno;
            }

            this.cbopaytype.SelectedIndex = 0;
        }       
    }
}