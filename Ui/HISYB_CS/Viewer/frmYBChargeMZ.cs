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
    public partial class frmYBChargeMZ : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmYBChargeMZ()
        {
            InitializeComponent();
        }

        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.clsCtl_YBChargeMZ();
            objController.Set_GUI_Apperance(this);
        }

        /// <summary>
        /// �����ܽ�� HIS
        /// </summary>
        public decimal decTotal = 0;
        /// <summary>
        /// ����ID
        /// </summary>
        public string strRecipeID = string.Empty;
        /// <summary>
        /// ���˿���
        /// </summary>
        public string strPatientCardNo = string.Empty;
        /// <summary>
        /// ��������
        /// </summary>
        public string strPatientName = string.Empty;
        /// <summary>
        /// ����Ա��
        /// </summary>
        public string strEmpNo = string.Empty;
        /// <summary>
        /// ��Ʊ��
        /// </summary>
        public string strInvNO = string.Empty;
        /// <summary>
        /// �������
        /// </summary>
        public string strSDYWH = string.Empty;
        /// <summary>
        /// �����¼��
        /// </summary>
        public string strJZJLH = string.Empty;
        /// <summary>
        /// �����ܽ�� ҽ�����ص�
        /// </summary>
        public decimal decYBTotal = 0;
        /// <summary>
        /// ���˽��
        /// </summary>
        public decimal decAcc = 0;
        /// <summary>
        /// �Ը����
        /// </summary>
        public decimal decSub = 0;
        /// <summary>
        /// �������֤����
        /// </summary>
        public string strIDCardNo = string.Empty;
        /// <summary>
        /// ����ҽ�ƣ�1��ͳ��֧��
        /// </summary>
        public decimal m_decBCYLTCZF1 = 0;
        /// <summary>
        /// ����ҽ�ƣ�2��ͳ��֧��
        /// </summary>
        public decimal m_decBCYLTCZF2 = 0;
        /// <summary>
        /// ����ҽ�ƣ�3��ͳ��֧��
        /// </summary>
        public decimal m_decBCYLTCZF3 = 0;
        /// <summary>
        /// ����ҽ�ƣ�4��ͳ��֧��
        /// </summary>
        public decimal m_decBCYLTCZF4 = 0;
        /// <summary>
        /// ����֧��
        /// </summary>
        public decimal m_decQTZHIFU = 0;
        /// <summary>
        /// ҽ�����˷�Ʊ���
        /// </summary>
        public decimal m_decYBJZFPJE = 0;
        /// <summary>
        /// ������
        /// </summary>
        public decimal m_decCZF = 0;
        /// <summary>
        /// ������ϸ
        /// </summary>
        public List<clsDGMzxmcs_VO> lstDGMzxmcsVo = new List<clsDGMzxmcs_VO>();

        /// <summary>
        /// �Ƿ���������
        /// </summary>
        public bool IsBirthInsurance = false;

        /// <summary>
        /// �Ƿ���������
        /// </summary>
        public bool IsCovi19 = false;

        private void frmYBChargeMZ_Load(object sender, EventArgs e)
        {
            //need modify�ж��Ƿ�����ҽ���������粻������ʾԭ�򣬲�����������˳���ť����Ϊ��ɫ��
            ((clsCtl_YBChargeMZ)this.objController).m_mthInit();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            ((clsCtl_YBChargeMZ)this.objController).m_mthRecipeUpload();
        }

        private void btnCharge_Click(object sender, EventArgs e)
        {
            ((clsCtl_YBChargeMZ)this.objController).m_mthMzybjs();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            MessageBox.Show("�ɹ��������HIS���㣡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
            this.DialogResult = DialogResult.OK;
        }

        private void lblCancelUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((clsCtl_YBChargeMZ)this.objController).m_mthUndoRecipeUpload();
        }

        private void lblCancelCharge_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((clsCtl_YBChargeMZ)this.objController).m_mthUndoMzybjs();
        }

        private void btnCheckPsw_Click(object sender, EventArgs e)
        {
            ((clsCtl_YBChargeMZ)this.objController).m_mthCheckPsw();
        }

        private void llblReadECard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((clsCtl_YBChargeMZ)this.objController).ReadQRcode();
        }
    }

}