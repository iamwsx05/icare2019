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
    //need modify
    public partial class frmYBChargeZY : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmYBChargeZY()
        {
            InitializeComponent();
        }

        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.clsCtl_YBChargeZY();
            objController.Set_GUI_Apperance(this);
        }
        /// <summary>
        /// HISҽ�Ʒ����ܽ��
        /// </summary>
        public decimal decTotal = 0;
        /// <summary>
        /// ��������  1 ��Ժ���� 2 ��;����3 Ԥ����
        /// </summary>
        public string strJslx = "1";
        /// <summary>
        /// ��Ʊ����
        /// </summary>
        public string strInvNo = string.Empty;
        /// <summary>
        /// ҽ�����㷵�ص��Էѽ��
        /// </summary>
        public decimal decYBSub = 0;
        /// <summary>
        /// ����Ա����
        /// </summary>
        public string strEmpNo = string.Empty;
        /// <summary>
        /// ����סԺID
        /// </summary>
        public string strRegisterId = string.Empty;
        /// <summary>
        /// ������ʼ����
        /// </summary>
        public string strJSQSRQ = string.Empty;
        /// <summary>
        /// ������ֹ����
        /// </summary>
        public string strJSZZRQ = string.Empty;
        #region ��ȡҩƷ�������� Add by �⺺�� 2014-12-25
        public bool m_blnDiffCostOn = false;
        #endregion

        /// <summary>
        /// ������ˮID.��;����
        /// </summary>
        public List<string> lstPChargeId = new List<string>();

        private void frmYBChargeZY_Load(object sender, EventArgs e)
        {
            long lngRes = ((clsCtl_YBChargeZY)this.objController).m_lngCheckDiagnose();
            if (lngRes == 0)
            {
                if (MessageBox.Show("�ò���û����д��Ժ��ϣ��Ƿ�������㣿", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    ((clsCtl_YBChargeZY)this.objController).blnIfDefaultCYZD = true;
                }
                else
                {
                    this.Close();
                }
            }
            ((clsCtl_YBChargeZY)this.objController).m_mthInit();
            ((clsCtl_YBChargeZY)this.objController).m_blnDiffCostOn = this.m_blnDiffCostOn;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            ((clsCtl_YBChargeZY)this.objController).m_mthUpload();
        }

        private void btnCharge_Click(object sender, EventArgs e)
        {
            //strJslx = "1";//��Ժ����
            ((clsCtl_YBChargeZY)this.objController).m_mthYBCharge(true);
        }

        private void btnCYDJ_Click(object sender, EventArgs e)
        {
            ((clsCtl_YBChargeZY)this.objController).m_mthYBCydj();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChargetest_Click(object sender, EventArgs e)
        {
            //strJslx = "3";//�籣����
            ((clsCtl_YBChargeZY)this.objController).m_mthYBCharge(false);
        }

        private void lblCancelUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((clsCtl_YBChargeZY)this.objController).m_mthCancelUpload();
        }

        private void lblCancelCharge_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((clsCtl_YBChargeZY)this.objController).m_mthCancelYBCharge();
        }

        private void btnModifyReg_Click(object sender, EventArgs e)
        {
            ((clsCtl_YBChargeZY)this.objController).m_mthModifyReg();
        }

        private void btnCheckPsw_Click(object sender, EventArgs e)
        {
            ((clsCtl_YBChargeZY)this.objController).m_mthCheckPsw();
        }

        private void llblKsydkjy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((clsCtl_YBChargeZY)this.objController).Ksydkjy();
        }

        private void llblReadQRcode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((clsCtl_YBChargeZY)this.objController).ReadQRcode();
        }
    }

}