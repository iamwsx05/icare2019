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
    /// ѡ��Ԥ�������͵�������
    /// </summary>
    public partial class frmPrePayType : Form
    {
        private string pretype = "";
        /// <summary>
        /// Ԥ�����ͣ� 0 ���� 1 �ֹ�
        /// </summary>
        public string PreType
        {
            get
            {
                return pretype;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public frmPrePayType()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (rdo1.Checked)
            {
                pretype = "0";
            }
            else if (rdo2.Checked)
            {
                pretype = "1";
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}