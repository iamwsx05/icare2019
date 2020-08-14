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
    /// �տ����սᱸעUI��
    /// </summary>
    public partial class frmDayReckoningRemark : Form
    {
        /// <summary>
        /// ��ע��Ϣ
        /// </summary>
        private string remarkinfo = "";
        /// <summary>
        /// ��ע��Ϣ
        /// </summary>
        public string RemarkInfo
        {
            get
            {
                return remarkinfo;
            }
            set
            {
                remarkinfo = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public frmDayReckoningRemark()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            remarkinfo = this.txtRemarkInfo.Text.Trim();

            //if (remarkinfo == "")
            //{
            //    MessageBox.Show("�����뱸ע��Ϣ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //else
            //{
                this.DialogResult = DialogResult.Yes;
            //}
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDayReckoningRemark_Load(object sender, EventArgs e)
        {
            this.txtRemarkInfo.Text = this.remarkinfo;
        }

        private void frmDayReckoningRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }      

    }
}