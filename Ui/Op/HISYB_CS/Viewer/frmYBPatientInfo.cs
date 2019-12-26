using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmYBPatientInfo : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmYBPatientInfo()
        {
            InitializeComponent();
        }

        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.clsCtl_YBPatientInfo();
            objController.Set_GUI_Apperance(this);
        }

        /// <summary>
        /// ��־ 0��סԺ�ţ�1�����ƿ��ţ�2:���֤��
        /// </summary>
        public string strFlag = "0";

        private void btnQuery_Click(object sender, EventArgs e)
        {
            ((clsCtl_YBPatientInfo)this.objController).m_mthGetPatientInfo();
        }

        private void frmYBPatientInfo_Load(object sender, EventArgs e)
        {
            ((clsCtl_YBPatientInfo)this.objController).m_mthInit();
            this.cboCondition.SelectedIndex = 0;
            this.txtInpatientid.Focus();
        }

        private void txtInpatientid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsCtl_YBPatientInfo)this.objController).m_mthGetPatientInfo();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmYBPatientInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if ((MessageBox.Show("ȷ��Ҫ�˳���������", "iCare")) == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }

        private void cboCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboCondition.SelectedIndex == 0)
            {
                strFlag = "0";//סԺ��
            }
            else if (this.cboCondition.SelectedIndex == 1)
            {
                strFlag = "1";//���ƿ���
            }
            else
            {
                strFlag = "2";//���֤��
            }
        }
    }
}