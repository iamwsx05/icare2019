using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.Emr.Signature_gui;
using weCare.Core.Entity;
using Microsoft.VisualBasic;

namespace iCare
{
    /// <summary>
    /// ͨ��ICU�����¼����������
    /// </summary>
    public partial class frmICUNurseRecordContent : frmDiseaseTrackBase
    {

        #region �ֶ�
        public bool blnEdit = false;
        /// <summary>
        /// ��ǰ���˵���Ժ�ǼǺ�
        /// </summary>
        public string mstrRegisterID;
        /// <summary>
        /// ��ǰ��¼�Ĵ���ʱ��
        /// </summary>
        public DateTime dtCreatedate;
        //����ǩ����
        private clsEmrSignToolCollection m_objSign;

        /// <summary>
        /// ��ʾ��ʽ 0 ��ʾ��¼ 1 ��ʾ������¼ 2��ʾ��������
        /// </summary>
        public int intDisplayMode = 0;

        /// <summary>
        /// ��������
        /// </summary>
        public string strAfterdays;
        public string strWeight;
        public string strOperationName;

        /// <summary>
        /// Һ���趨 ����
        /// </summary>
        public string strLIQUID1;
        public string strLIQUID2;
        public string strLIQUID3;
        public string strLIQUID4;
        public string strLIQUID5;

        /// <summary>
        /// ��Һ�趨 ����
        /// </summary>
        public string strDRAIN1;
        public string strDRAIN2;
        public string strDRAIN3;
        public string strDRAIN4;
        public string strDRAIN5;

        /// <summary>
        /// ��ǰת����ˮ��
        /// </summary>
        private string m_strTransferID = string.Empty;
        /// <summary>
        /// �Ƿ������¼
        /// </summary>
        public bool m_blnIsAddNew = true;
        /// <summary>
        /// ��ǰ�������ۼ�
        /// </summary>
        private double m_dblCountINBefore = 0;
        /// <summary>
        /// ��ǰ�ĳ����ۼ�
        /// </summary>
        private double m_dblCountOUTBefore = 0;
        /// <summary>
        /// ��ǰ��С������
        /// </summary>
        private double m_dblPissTotalBefore = 0;
        /// <summary>
        /// ��ǰ�Ĵ������
        /// </summary>
        private double m_dblStoolTotalBefore = 0;
        /// <summary>
        /// �����һ����¼�ļ�¼ʱ��
        /// </summary>
        private DateTime m_dtmFirstRecordDate = DateTime.MinValue;
        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="p_strTransferID">��ǰת����ˮ��</param>
        /// <param name="p_dtmFirstRecord">�����һ����¼�ļ�¼ʱ��</param>
        public frmICUNurseRecordContent(string p_strTransferID, DateTime p_dtmFirstRecord)
        {
            InitializeComponent();

            m_strTransferID = p_strTransferID;
            m_dtmFirstRecordDate = p_dtmFirstRecord;
            //ָ����ʿ����վ��
            intFormType = 2;
            //��richtextbox��Ϣ
            m_mthSetRichTextBoxAttribInControl(this);

            //ǩ������ֵ
            m_objSign = new clsEmrSignToolCollection();
            //m_mthBindEmployeeSign(��ť,ǩ����,ҽ��1or��ʿ2,�����֤trueorfalse);
            m_objSign.m_mthBindEmployeeSign(m_cmdEmployeeSign, lsvSign, 2, true, clsEMRLogin.LoginInfo.m_strEmpID);
            m_mthSetComboBoxItem();
            m_mthThisAddRichTextInfo(this);
        }
        #endregion

        /// <summary>
        /// ���ý����Ϲ̶�ѡ���ComboBOx
        /// </summary>
        private void m_mthSetComboBoxItem()
        {
            m_cboCONSCIOUSNESS.AddRangeItems(new string[] { "����", "��˯", "����", "ǳ����", "�����", "����" });
            m_cboPupilSizeLeft.AddRangeItems(new string[] { "1", "2", "3", "4", "5" });
            m_cboPupilSizeRight.AddRangeItems(new string[] { "1", "2", "3", "4", "5" });
            m_cboReflectLeft.AddRangeItems(new string[] { "��ʧ", "����", "�ٶ�" });
            m_cboReflectRight.AddRangeItems(new string[] { "��ʧ", "����", "�ٶ�" });
        }

        #region �����¼
        /// <summary>
        ///  �����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (m_dtmFirstRecordDate != DateTime.MinValue && m_dtpCreateDate.Value.Date != m_dtmFirstRecordDate.Date)
            {
                DialogResult dr = MessageBox.Show("��ǰѡ����¼����" + m_dtpCreateDate.Value.ToString("yyyy��MM��dd��") + "�뵱���һ����¼�ļ�¼����" + m_dtmFirstRecordDate.ToString("yyyy��MM��dd��") + "��ͬ���Ƿ�������棿", "ͨ��ICU�����¼", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return;
                }
            }
            if (m_lngSave() > 0)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }
        #endregion

        #region �˳���¼
        /// <summary>
        /// �˳���¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            Close();
        }
        #endregion

        #region �¼�
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmICUNurseRecordContent_Load(object sender, EventArgs e)
        {
            lblAfterDays.Text = strAfterdays;
            if (intDisplayMode == 2)
            {
                m_tabMain.TabPages.Remove(tabPage1);
                m_tabMain.TabPages.Remove(tabPage2);

            }

            if (m_BlnIsAddNew)
            {
                m_mthGetTotalBefore(DateTime.MinValue);
                m_mthCountIN();
                m_mthCountOUT();
            }

            //if ( blnEdit)
            //{
            //  //��ȡ��¼
            //    try
            //    {
            //        this.Cursor = Cursors.WaitCursor;
            //        clsICUNurseService objservice = new clsICUNurseService();
            //         clsICUNurseRecord objContent = new clsICUNurseRecord();
            //         objservice.m_lngGetRecordContent(null, mstrRegisterID, dtCreatedate.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), out m_objCurrentRecordContent);
            //         m_mthSetGUIFromContent(m_objCurrentRecordContent);

            //    }
            //    finally
            //    {
            //        this.Cursor = Cursors.Default;

            //    }
            //}
        }
        #endregion

        #region ��д����

        public override clsTrackRecordContent m_objGetRecordContent(clsPatient p_objSelectedPatient, string p_strOpenDate)
        {
            //��ȡ��¼
            try
            {
                this.Cursor = Cursors.WaitCursor;

                //clsICUNurseService objservice =
                //    (clsICUNurseService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUNurseService));

                clsICUNurseRecord objContent = new clsICUNurseRecord();
                (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetRecordContent_factory(enmDiseaseTrackType.ICUNurseRecord, mstrRegisterID, dtCreatedate.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), out m_objCurrentRecordContent);

            }
            finally
            {
                this.Cursor = Cursors.Default;

            }
            return m_objCurrentRecordContent;
        }
        /// <summary>
        /// ��ȡ��¼��Ϣ
        /// �������б��д����ñ����Ժ���
        /// </summary>
        /// <returns></returns>
        public override iCare.clsDiseaseTrackInfo m_objGetDiseaseTrackInfo()
        {

            return null;
        }

        /// <summary>
        /// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
        /// </summary>
        protected override void m_mthClearRecordInfo()
        {
            //Ĭ��ǩ��
            MDIParent.m_mthSetDefaulEmployee(lsvSign);

            //��վ����¼����

            #region MyRegion
            // ����
            this.m_txtTEMPERATURE.m_mthClearText();
            //ĩ����
            this.m_txtENDTEMPERATURE.m_mthClearText();
            //����
            this.m_txtHR.m_mthClearText();
            //����
            this.m_txtRHYTHM.m_mthClearText();
            //Ѫѹs
            this.m_txtBPS.m_mthClearText();
            //ѪѹD
            this.m_txtBPD.m_mthClearText();
            //ABP
            this.m_txtABP.m_mthClearText();
            //CVP
            this.m_txtCVP.m_mthClearText();
            //sp02
            this.m_txtSpo2.m_mthClearText();
            //��ʶ
            this.m_cboCONSCIOUSNESS.Text = "";
            //ͫ�ס���
            //this.m_cboPupilSizeLeft.SelectedIndex = -1;
            this.m_cboPupilSizeLeft.Text = "";
            //ͫ�ס���
            //this.m_cboPupilSizeRight.SelectedIndex = -1;
            this.m_cboPupilSizeRight.Text = "";
            //�Թⷴ����
            //this.m_cboReflectLeft.SelectedIndex = -1;
            this.m_cboReflectLeft.Text = "";
            //�Թⷴ����
            //this.m_cboReflectRight.SelectedIndex = -1;
            this.m_cboReflectRight.Text = "";
            // Ѫ�ܻ���ҩ��
            //this.m_cboVASOAACTIVE.SelectedIndex = -1;
            this.m_cboVASOAACTIVE.Text = "";
            //ǿ������ҩ
            //this.m_cboCARDIACDIURETIC.SelectedIndex = -1;
            this.m_cboCARDIACDIURETIC.Text = "";
            //Һ��1
            this.m_txtLIQUID1.m_mthClearText();
            //Һ��2
            this.m_txtLIQUID2.m_mthClearText();
            //Һ��3
            this.m_txtLIQUID3.m_mthClearText();
            //Һ��4
            this.m_txtLIQUID4.m_mthClearText();
            //Һ��5
            this.m_txtLIQUID5.m_mthClearText();
            //ȫѪ
            this.m_txtFullBlood.m_mthClearText();
            ///Ѫ��
            this.m_txtPLASMA.m_mthClearText();
            //����
            this.m_txtNOSE1.m_mthClearText();
            //�ǽ�
            this.m_txtNOSE2.m_mthClearText();
            //ÿСʱ����
            this.m_txtInperhour.m_mthClearText();
            //�ۼ�����
            this.m_txtIntotalAll.m_mthClearText();
            //С��
            this.m_txtstool.m_mthClearText();
            //С���ۼ�
            this.m_txtStooltoalall.m_mthClearText();
            //���
            this.m_txtPISS.m_mthClearText();
            //����ۼ�
            this.m_txtPISStoal.m_mthClearText();
            //��Һ1
            this.m_txtDRAIN1.m_mthClearText();
            //��Һ2
            this.m_txtDRAIN2.m_mthClearText();
            //��Һ3
            this.m_txtDRAIN3.m_mthClearText();
            //��Һ4
            this.m_txtDRAIN4.m_mthClearText();
            //��Һ5
            this.m_txtDRAIN5.m_mthClearText();
            //ÿСʱ��Һ�ۼ�
            this.m_txtouthour.m_mthClearText();
            //��Һ�����ۼ�
            this.m_txtouttoalall.m_mthClearText();
            //��λ
            this.cmbPOSTURE.Text = "";
            //Ƥ��
            this.cmbskin.Text = "";
            //̵ɫ
            this.cmbSPUTUM.Text = "";
            //̵��
            this.cmbSPUTUM1.Text = "";
            //��̵
            this.cmbSUCKER.Text = "";
            //��ǻ����
            this.cmbORAL.Text = "";
            //������ϴ
            this.cmbPERINEUM.Text = "";
            //����
            this.cmbGESTICULATION.Text = "";
            //��ԡ
            this.cmbSPONGEBATH.Text = "";
            //PT
            this.m_txtPT.m_mthClearText();
            //ACT
            this.m_txtACT.m_mthClearText();
            //GlU
            this.m_txtglu.m_mthClearText();
            //K
            this.m_txtKplus.m_mthClearText();
            //Na��
            this.m_txtNa.m_mthClearText();
            //CL-
            this.m_txtCI.m_mthClearText();
            //Ca����
            this.m_txtcaplus.m_mthClearText();
            //ͨ��ģʽ
            this.m_cboBREATHMACHINE.Text = "";
            //PEE
            this.m_txtPEE.m_mthClearText();
            //TV/Ti
            this.m_txtTVTi.m_mthClearText();
            //Ƶ��
            this.m_txtFREQUENCY.m_mthClearText();
            //��Ũ��
            this.m_txto2.m_mthClearText();
            //������
            this.m_txtSENSEIIVE.m_mthClearText();
            //����
            this.m_txtSPEED.m_mthClearText();
            //I:E
            this.m_txtIE.m_mthClearText();
            //����ѹ��
            this.m_txtGASPRESS.m_mthClearText();
            //TV
            this.m_txtTV.m_mthClearText();
            //MV
            this.m_txtMV.m_mthClearText();
            //�ܵ�ѹ��
            this.m_txtPIPEDEPTH.m_mthClearText();
            //����ѹ��
            this.m_txtAIRCELLPRESS.m_mthClearText();
            //ETCO2
            this.m_txtETCO2.m_mthClearText();
            //PH
            this.m_txtPH.m_mthClearText();
            //m_txtPCO2
            this.m_txtPCO2.m_mthClearText();
            //m_txtPAO2
            this.m_txtPAO2.m_mthClearText();
            //m_txtHCO3
            this.m_txtHCO3.m_mthClearText();
            //m_txtBE
            this.m_txtBE.m_mthClearText();
            //��������
            this.m_txtContent.m_mthClearText();
            //����
            this.m_txtRespiration.m_mthClearText();
            #endregion

        }

        /// <summary>
        /// �����¼���������,�����Ӵ������Ҫ����ʵ��
        /// </summary>
        /// <param name="p_blnEnable">�Ƿ������޸������¼�ļ�¼��Ϣ��</param>
        protected override void m_mthEnableModifySub(bool p_blnEnable)
        {

        }

        /// <summary>
        /// �����Ƿ�����޸ģ��޸����ۼ�����
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_blnReset">�Ƿ����ÿ����޸ģ��޸����ۼ�����
        ///���Ϊtrue�����Լ�¼���ݣ��ѽ����������Ϊ�����ƣ�
        ///������ݼ�¼���ݽ������á�
        ///</param>
        protected override void m_mthSetModifyControlSub(clsTrackRecordContent p_objRecordContent,
            bool p_blnReset)
        {
            //������д�淶���þ��崰�����д����

        }

        /// <summary>
        /// �������¼��ֵ��ʾ�������ϡ�
        /// </summary>
        /// <param name="p_objContent"></param>
        protected override void m_mthSetGUIFromContent(weCare.Core.Entity.clsTrackRecordContent p_objContent)
        {
            clsICUNurseRecord objContent = (clsICUNurseRecord)p_objContent;

            this.m_mthClearRecordInfo();

            #region �ѱ�ֵ��ֵ������
            m_mthGetTotalBefore(objContent.m_dtmRecordDate);
            m_strTransferID = objContent.m_strTRANSFERID;
            this.lblAfterDays.Text = objContent.m_strAFTEROPDAYS;
            m_dtpCreateDate.Value = objContent.m_dtmRecordDate;
            // ����
            this.m_txtTEMPERATURE.m_mthSetNewText(objContent.m_strTEMPERATURE, objContent.m_strTEMPERATUREXML);
            //ĩ����
            this.m_txtENDTEMPERATURE.m_mthSetNewText(objContent.m_strENDTEMPERATURE, objContent.m_strENDTEMPERATUREXML);
            //����
            this.m_txtHR.m_mthSetNewText(objContent.m_strHR, objContent.m_strHRXML);
            //����
            this.m_txtRHYTHM.m_mthSetNewText(objContent.m_strRHYTHM, objContent.m_strRHYTHMXML);
            //����
            this.m_txtRespiration.m_mthSetNewText(objContent.m_strRESPIRATION, objContent.m_strRESPIRATIONXML);
            //Ѫѹs
            this.m_txtBPS.m_mthSetNewText(objContent.m_strBPS, objContent.m_strBPSXML);
            //ѪѹD
            this.m_txtBPD.m_mthSetNewText(objContent.m_strBPD, objContent.m_strBPDXML);
            //ABP
            this.m_txtABP.m_mthSetNewText(objContent.m_strABP, objContent.m_strABPXML);
            //CVP
            this.m_txtCVP.m_mthSetNewText(objContent.m_strCVP, objContent.m_strCVPXML);
            //sp02
            this.m_txtSpo2.m_mthSetNewText(objContent.m_strSPO2, objContent.m_strSPO2XML);
            //��ʶ
            this.m_cboCONSCIOUSNESS.Text = objContent.m_strCONSCIOUSNESS;
            //ͫ�ס���
            this.m_cboPupilSizeLeft.Text = objContent.m_strPUPIL;
            //ͫ�ס���
            this.m_cboPupilSizeRight.Text = objContent.m_strPUPIR;
            //�Թⷴ����
            this.m_cboReflectLeft.Text = objContent.m_strLIGHTREFLEXL;
            //�Թⷴ����
            this.m_cboReflectRight.Text = objContent.m_strLIGHTREFLEXR;
            // Ѫ�ܻ���ҩ��
            this.m_cboVASOAACTIVE.Text = objContent.m_strVASOAACTIVE;
            //ǿ������ҩ
            this.m_cboCARDIACDIURETIC.Text = objContent.m_strCARDIACDIURETIC;
            //Һ��1
            this.m_txtLIQUID1.m_mthSetNewText(objContent.m_strLIQUID1, objContent.m_strLIQUID1XML);
            //Һ��2
            this.m_txtLIQUID2.m_mthSetNewText(objContent.m_strLIQUID2, objContent.m_strLIQUID2XML);
            //Һ��3
            this.m_txtLIQUID3.m_mthSetNewText(objContent.m_strLIQUID3, objContent.m_strLIQUID3XML);
            //Һ��4
            this.m_txtLIQUID4.m_mthSetNewText(objContent.m_strLIQUID4, objContent.m_strLIQUID4XML);
            //Һ��5
            this.m_txtLIQUID5.m_mthSetNewText(objContent.m_strLIQUID5, objContent.m_strLIQUID5XML);
            //ȫѪ
            this.m_txtFullBlood.m_mthSetNewText(objContent.m_strFBOOL, objContent.m_strFBOOLXML);
            ///Ѫ��
            this.m_txtPLASMA.m_mthSetNewText(objContent.m_strPLASMA, objContent.m_strPLASMAXML);
            //����
            this.m_txtNOSE1.m_mthSetNewText(objContent.m_strNOSE1, objContent.m_strNOSE1XML);
            //�ǽ�
            this.m_txtNOSE2.m_mthSetNewText(objContent.m_strNOSE2, objContent.m_strNOSE2XML);
            //ÿСʱ����
            this.m_txtInperhour.m_mthSetNewText(objContent.m_strINPERHOUR, objContent.m_strINPERHOURXML);
            //�ۼ�����
            this.m_txtIntotalAll.m_mthSetNewText(objContent.m_strINTOTAL, objContent.m_strINTOTALXML);
            //С��
            this.m_txtstool.m_mthSetNewText(objContent.m_strSTOOL, objContent.m_strSTOOLXML);
            //С���ۼ�
            this.m_txtStooltoalall.m_mthSetNewText(objContent.m_strSTOOLTOTAL, objContent.m_strSTOOLTOTALXML);
            //���
            this.m_txtPISS.m_mthSetNewText(objContent.m_strPISS, objContent.m_strPISSXML);
            //����ۼ�
            this.m_txtPISStoal.m_mthSetNewText(objContent.m_strPISSTOTAL, objContent.m_strPISSTOTALXML);
            //��Һ1
            this.m_txtDRAIN1.m_mthSetNewText(objContent.m_strDRAIN1, objContent.m_strDRAIN1XML);
            //��Һ2
            this.m_txtDRAIN2.m_mthSetNewText(objContent.m_strDRAIN2, objContent.m_strDRAIN2XML);
            //��Һ3
            this.m_txtDRAIN3.m_mthSetNewText(objContent.m_strDRAIN3, objContent.m_strDRAIN3XML);
            //��Һ4
            this.m_txtDRAIN4.m_mthSetNewText(objContent.m_strDRAIN4, objContent.m_strDRAIN4XML);
            //��Һ5
            this.m_txtDRAIN5.m_mthSetNewText(objContent.m_strDRAIN5, objContent.m_strDRAIN5XML);
            //ÿСʱ��Һ�ۼ�
            this.m_txtouthour.m_mthSetNewText(objContent.m_strOUTPERHOUR, objContent.m_strOUTPERHOURXML);
            //��Һ�����ۼ�
            this.m_txtouttoalall.m_mthSetNewText(objContent.m_strOUTTOTAL, objContent.m_strOUTTOTALXML);
            //��λ
            this.cmbPOSTURE.Text = objContent.m_strPOSTURE;
            //Ƥ��
            this.cmbskin.Text = objContent.m_strSKIN;
            //̵ɫ
            this.cmbSPUTUM.Text = objContent.m_strSPUTUM;
            //̵��
            this.cmbSPUTUM1.Text = objContent.m_strSPUTUM1;
            //��̵
            this.cmbSUCKER.Text = objContent.m_strSUCKER;
            //��ǻ����
            this.cmbORAL.Text = objContent.m_strORAL;
            //������ϴ
            this.cmbPERINEUM.Text = objContent.m_strPERINEUM;
            //����
            this.cmbGESTICULATION.Text = objContent.m_strGESTICULATION;
            //��ԡ
            this.cmbSPONGEBATH.Text = objContent.m_strSPONGEBATH;
            //PT
            this.m_txtPT.m_mthSetNewText(objContent.m_strPT, objContent.m_strPTXML);
            //ACT
            this.m_txtACT.m_mthSetNewText(objContent.m_strACT, objContent.m_strACTXML);
            //GlU
            this.m_txtglu.m_mthSetNewText(objContent.m_strGLU, objContent.m_strGLUXML);
            //K
            this.m_txtKplus.m_mthSetNewText(objContent.m_strK, objContent.m_strKXML);
            //Na��
            this.m_txtNa.m_mthSetNewText(objContent.m_strNAPLUS, objContent.m_strNAPLUSXML);
            //CL-
            this.m_txtCI.m_mthSetNewText(objContent.m_strCL, objContent.m_strCLXML);
            //Ca����
            this.m_txtcaplus.m_mthSetNewText(objContent.m_strCAPLUS2, objContent.m_strCAPLUS2XML);
            //ͨ��ģʽ
            this.m_cboBREATHMACHINE.Text = objContent.m_strMMODEL;
            //PEE
            this.m_txtPEE.m_mthSetNewText(objContent.m_strPEE, objContent.m_strPEEXML);
            //TV/Ti
            this.m_txtTVTi.m_mthSetNewText(objContent.m_strTVTI, objContent.m_strTVTIXML);
            //Ƶ��
            this.m_txtFREQUENCY.m_mthSetNewText(objContent.m_strFREQUENCY, objContent.m_strFREQUENCYXML);
            //��Ũ��
            this.m_txto2.m_mthSetNewText(objContent.m_strO2, objContent.m_strO2XML);
            //������
            this.m_txtSENSEIIVE.m_mthSetNewText(objContent.m_strSENSEIIVE, objContent.m_strSENSEIIVEXML);
            //����
            this.m_txtSPEED.m_mthSetNewText(objContent.m_strSPEED, objContent.m_strSPEEDXML);
            //I:E
            this.m_txtIE.m_mthSetNewText(objContent.m_strIE, objContent.m_strIEXML);
            //����ѹ��
            this.m_txtGASPRESS.m_mthSetNewText(objContent.m_strGASPRESS, objContent.m_strGASPRESSXML);
            //TV
            this.m_txtTV.m_mthSetNewText(objContent.m_strTV, objContent.m_strTVXML);
            //MV
            this.m_txtMV.m_mthSetNewText(objContent.m_strMV, objContent.m_strMVXML);
            //�ܵ�ѹ��
            this.m_txtPIPEDEPTH.m_mthSetNewText(objContent.m_strPIPEDEPTH, objContent.m_strPIPEDEPTHXML);
            //����ѹ��
            this.m_txtAIRCELLPRESS.m_mthSetNewText(objContent.m_strAIRCELLPRESS, objContent.m_strAIRCELLPRESSXML);
            //ETCO2
            this.m_txtETCO2.m_mthSetNewText(objContent.m_strETCO2, objContent.m_strETCO2XML);
            //PH
            this.m_txtPH.m_mthSetNewText(objContent.m_strPH, objContent.m_strPHXML);
            //m_txtPCO2
            this.m_txtPCO2.m_mthSetNewText(objContent.m_strPCO2, objContent.m_strPCO2XML);
            //m_txtPAO2
            this.m_txtPAO2.m_mthSetNewText(objContent.m_strPAO2, objContent.m_strPAO2XML);
            //m_txtHCO3
            this.m_txtHCO3.m_mthSetNewText(objContent.m_strHCO3, objContent.m_strHCO3XML);
            //m_txtBE
            this.m_txtBE.m_mthSetNewText(objContent.m_strBE, objContent.m_strBEXML);
            //��������
            this.m_txtContent.m_mthSetNewText(objContent.objRecordContent.m_strCONTENT, objContent.objRecordContent.m_strCONTENTXML);

            #endregion

            #region ǩ������
            lsvSign.Clear();
            if (objContent.objSignerArr != null)
            {
                m_mthAddSignToListView(lsvSign, objContent.objSignerArr);
                //lsvSign.Items.Clear();
                //for (int i = 0; i < objContent.objSignerArr.Length; i++)
                //{
                //    if (objContent.objSignerArr[i].controlName == "lsvSign")
                //    {
                //        ListViewItem lviNewItem = new ListViewItem(objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
                //        //ID ����ظ���
                //        lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR);
                //        //���� ������
                //        lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
                //        //tag��Ϊ����
                //        lviNewItem.Tag = objContent.objSignerArr[i].objEmployee;
                //        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                //        lsvSign.Items.Add(lviNewItem);
                //    }
                //}
            }
            #endregion ǩ��		

            this.m_dtpCreateDate.Enabled = false;
        }

        /// <summary>
        /// �����Ƿ����ѡ���˺ͼ�¼ʱ���б�
        /// </summary>
        /// <param name="p_blnEnable"></param>
        protected override void m_mthEnablePatientSelectSub(bool p_blnEnable)
        {
            if (p_blnEnable == false)
            {


                this.CenterToParent();
            }

            this.MaximizeBox = false;
        }

        /// <summary>
        /// ��ȡ�����¼�������ʵ��
        /// </summary>
        /// <returns></returns>
        protected override iCare.clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
        {
            //��ȡ�����¼�������ʵ�������Ӵ�������ʵ��
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.ICUNurseRecord);
        }

        /// <summary>
        /// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
        {
            //��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
            clsCardiovascularTend_GX objContent = (clsCardiovascularTend_GX)p_objRecordContent;
        }
        /// <summary>
        /// �ӽ����ȡֵfor save
        /// </summary>
        /// <returns></returns>
        protected override weCare.Core.Entity.clsTrackRecordContent m_objGetContentFromGUI()
        {
            //�������У��
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                return null;

            #region �ӽ����ȡ��ֵ
            clsICUNurseRecord objContent = new clsICUNurseRecord();
            objContent.objRecordContent = new clsICUNurseRecordContent();
            try
            {
                objContent.objRecordContent.m_strRegisterID = mstrRegisterID;
                objContent.m_strRegisterID = mstrRegisterID;
                objContent.m_dtmCreateDate = dtCreatedate;//��ʱ���м�˻����¸�ֵ ȡ�м����ʱ��
                objContent.m_strCreateUserID = clsEMRLogin.LoginInfo.m_strEmpID;//ȡ�ĵ�¼��ID
                objContent.objRecordContent.m_strCreateUserID = clsEMRLogin.LoginInfo.m_strEmpID;//ȡ�ĵ�¼��ID
                objContent.objRecordContent.m_dtmCreateDate = dtCreatedate;//��ʱ���м�˻����¸�ֵ ȡ�м����ʱ��
                objContent.m_dtmDeActivedDate = DateTime.Now;//Ĭ�ϣ�û��ɾ�� �������Ϊ��
                objContent.m_strDeActivedOperatorID = "";
                objContent.m_dtmFirstPrintDate = DateTime.Now;//Ĭ��;
                objContent.m_bytStatus = 1;//�����½���Ч��
                objContent.objRecordContent.m_bytStatus = 1;//�����½���Ч��
                objContent.objRecordContent.m_intMarkStatus = 0;//�����½������޺ۼ��޸�
                objContent.m_dtmRecordDate = m_dtpCreateDate.Value;//��¼ʱ�� ȡ����ʱ��
                objContent.objRecordContent.m_dtmRecordDate = m_dtpCreateDate.Value;//��¼ʱ�� ȡ����ʱ��
                objContent.m_bytIfConfirm = 0;//Ĭ��δ���
                objContent.objRecordContent.m_bytIfConfirm = 0;//Ĭ��δ���
                objContent.m_strTRANSFERID = m_strTransferID;
                objContent.objRecordContent.m_strTRANSFERID = m_strTransferID;

                #region �Ƿ�����޺ۼ��޸�
                if (chkModifyWithoutMatk.Checked)
                {
                    objContent.m_intMarkStatus = 0;//�����½������޺ۼ��޸�
                    objContent.objRecordContent.m_intMarkStatus = 0;
                }
                else
                {
                    objContent.m_intMarkStatus = 1;
                    objContent.objRecordContent.m_intMarkStatus = 1;
                }
                #endregion

                #region ��ȡԤ��ֵ
                objContent.m_strOPNAME = strOperationName;
                objContent.m_strWEIGHT = strWeight;
                objContent.m_strAFTEROPDAYS = strAfterdays;
                //Һ��
                objContent.m_strLIQUID1D = strLIQUID1;
                objContent.m_strLIQUID2D = strLIQUID2;
                objContent.m_strLIQUID3D = strLIQUID3;
                objContent.m_strLIQUID4D = strLIQUID4;
                objContent.m_strLIQUID5D = strLIQUID5;
                //��Һ
                objContent.m_strDRAIN1D = strDRAIN1;
                objContent.m_strDRAIN2D = strDRAIN2;
                objContent.m_strDRAIN3D = strDRAIN3;
                objContent.m_strDRAIN4D = strDRAIN4;
                objContent.m_strDRAIN5D = strDRAIN5;
                #endregion

                #region ��ȡǩ��
                int intSignCount = lsvSign.Items.Count;
                //��ȡlsvsignǩ��
                objContent.objSignerArr = new clsEmrSigns_VO[intSignCount];
                strUserIDList = "";
                strUserNameList = "";
                m_mthGetSignArr(new Control[] { lsvSign }, ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);
                //for (int i = 0; i < intSignCount; i++)
                //{
                //    objContent.objSignerArr[i]=new clsEmrSigns_VO();
                //    objContent.objSignerArr[i].objEmployee=new clsEmrEmployeeBase_VO();
                //    objContent.objSignerArr[i].objEmployee=(clsEmrEmployeeBase_VO)( lsvSign.Items[i].Tag);
                //    objContent.objSignerArr[i].controlName="lsvSign";
                //    objContent.objSignerArr[i].m_strFORMID_VCHR="frmICUNurseRecordContent";//ע���Сд
                //    objContent.objSignerArr[i].m_strREGISTERID_CHR=mstrRegisterID;
                //    //�ۼ���ʽ 0972,0324,

                //    strUserIDList=strUserIDList+objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR.Trim()+",";
                //    strUserNameList=strUserNameList+objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR.Trim()+",";
                //}
                objContent.m_strModifyUserID = strUserIDList;
                objContent.m_strRecordUserID = strUserIDList; //��¼��һ�β�������Ϣ 
                objContent.m_dtmRecordDate = this.m_dtpCreateDate.Value;
                objContent.objRecordContent.m_dtmRecordDate = this.m_dtpCreateDate.Value;
                objContent.objRecordContent.m_strRecordUserID = strUserIDList;
                #endregion

                #region ��������Richtextbox���û� ���˲��������ڻ�ȡǩ����
                //����Richtextbox��modifyuserID ��modifyuserName
                m_mthSetRichTextBoxAttribInControlWithIDandName(this);
                #endregion

                #region ��ȡ����
                objContent.m_strAFTEROPDAYS = this.lblAfterDays.Text.Trim();
                objContent.objRecordContent.m_strAFTEROPDAYS = this.lblAfterDays.Text.Trim();
                // ����
                objContent.m_strTEMPERATURE = this.m_txtTEMPERATURE.Text.Trim();
                objContent.m_strTEMPERATUREXML = this.m_txtTEMPERATURE.m_strGetXmlText();
                objContent.m_strTEMPERATURE_RIGHT = this.m_txtTEMPERATURE.m_strGetRightText();
                //ĩ����
                objContent.m_strENDTEMPERATURE = this.m_txtENDTEMPERATURE.Text.Trim();
                objContent.m_strENDTEMPERATUREXML = this.m_txtENDTEMPERATURE.m_strGetXmlText();
                objContent.m_strENDTEMPERATURE_RIGHT = this.m_txtENDTEMPERATURE.m_strGetRightText();
                //����
                objContent.m_strHR = this.m_txtHR.Text.Trim();
                objContent.m_strHRXML = this.m_txtHR.m_strGetXmlText();
                objContent.m_strHR_RIGHT = this.m_txtHR.m_strGetRightText();
                //����
                objContent.m_strRHYTHM = this.m_txtRHYTHM.Text.Trim();
                objContent.m_strRHYTHMXML = this.m_txtRHYTHM.m_strGetXmlText();
                objContent.m_strRHYTHM_RIGHT = this.m_txtRHYTHM.m_strGetRightText();
                //����
                objContent.m_strRESPIRATION = this.m_txtRespiration.Text.Trim();
                objContent.m_strRESPIRATIONXML = this.m_txtRespiration.m_strGetXmlText();
                objContent.m_strRESPIRATION_RIGHT = this.m_txtRespiration.m_strGetRightText();
                //Ѫѹs
                objContent.m_strBPS = this.m_txtBPS.Text.Trim();
                objContent.m_strBPSXML = this.m_txtBPS.m_strGetXmlText();
                objContent.m_strBPS_RIGHT = this.m_txtBPS.m_strGetRightText();

                //ѪѹD
                objContent.m_strBPD = this.m_txtBPD.Text.Trim();
                objContent.m_strBPDXML = this.m_txtBPD.m_strGetXmlText();
                objContent.m_strBPD_RIGHT = this.m_txtBPD.m_strGetRightText();
                //ABP
                objContent.m_strABP = this.m_txtABP.Text.Trim();
                objContent.m_strABPXML = this.m_txtABP.m_strGetXmlText();
                objContent.m_strABP_RIGHT = this.m_txtABP.m_strGetRightText();
                //CVP
                objContent.m_strCVP = this.m_txtCVP.Text.Trim();
                objContent.m_strCVPXML = this.m_txtCVP.m_strGetXmlText();
                objContent.m_strCVP_RIGHT = this.m_txtCVP.m_strGetRightText();
                //sp02
                objContent.m_strSPO2 = this.m_txtSpo2.Text.Trim();
                objContent.m_strSPO2XML = this.m_txtSpo2.m_strGetXmlText();
                objContent.m_strSPO2_RIGHT = this.m_txtSpo2.m_strGetRightText();
                //��ʶ
                objContent.m_strCONSCIOUSNESS = this.m_cboCONSCIOUSNESS.Text.Trim();
                objContent.m_strCONSCIOUSNESSXML = "";
                objContent.m_strCONSCIOUSNESS_RIGHT = this.m_cboCONSCIOUSNESS.Text.Trim();
                //ͫ�ס���
                objContent.m_strPUPIL = this.m_cboPupilSizeLeft.Text;
                objContent.m_strPUPIL_RIGHT = this.m_cboPupilSizeLeft.Text;
                //ͫ�ס���
                objContent.m_strPUPIR = this.m_cboPupilSizeRight.Text;
                objContent.m_strPUPIR_RIGHT = this.m_cboPupilSizeRight.Text;
                //�Թⷴ����
                objContent.m_strLIGHTREFLEXL = this.m_cboReflectLeft.Text;
                objContent.m_strLIGHTREFLEXL_RIGHT = this.m_cboReflectLeft.Text;
                //�Թⷴ����
                objContent.m_strLIGHTREFLEXR = this.m_cboReflectRight.Text;
                objContent.m_strLIGHTREFLEXR_RIGHT = this.m_cboReflectRight.Text;
                // Ѫ�ܻ���ҩ��
                objContent.m_strVASOAACTIVE = this.m_cboVASOAACTIVE.Text;
                objContent.m_strVASOAACTIVE_RIGHT = this.m_cboVASOAACTIVE.Text;
                //ǿ������ҩ
                objContent.m_strCARDIACDIURETIC = this.m_cboCARDIACDIURETIC.Text;
                objContent.m_strCARDIACDIURETIC_RIGHT = this.m_cboCARDIACDIURETIC.Text;

                //Һ��1
                objContent.m_strLIQUID1 = this.m_txtLIQUID1.Text.Trim();
                objContent.m_strLIQUID1XML = this.m_txtLIQUID1.m_strGetXmlText();
                objContent.m_strLIQUID1_RIGHT = this.m_txtLIQUID1.m_strGetRightText();
                //Һ��2
                objContent.m_strLIQUID2 = this.m_txtLIQUID2.Text.Trim();
                objContent.m_strLIQUID2XML = this.m_txtLIQUID2.m_strGetXmlText();
                objContent.m_strLIQUID2_RIGHT = this.m_txtLIQUID2.m_strGetRightText();
                //Һ��3
                objContent.m_strLIQUID3 = this.m_txtLIQUID3.Text.Trim();
                objContent.m_strLIQUID3XML = this.m_txtLIQUID3.m_strGetXmlText();
                objContent.m_strLIQUID3_RIGHT = this.m_txtLIQUID3.m_strGetRightText();
                //Һ��4
                objContent.m_strLIQUID4 = this.m_txtLIQUID4.Text.Trim();
                objContent.m_strLIQUID4XML = this.m_txtLIQUID4.m_strGetXmlText();
                objContent.m_strLIQUID4_RIGHT = this.m_txtLIQUID4.m_strGetRightText();
                //Һ��5
                objContent.m_strLIQUID5 = this.m_txtLIQUID5.Text.Trim();
                objContent.m_strLIQUID5XML = this.m_txtLIQUID5.m_strGetXmlText();
                objContent.m_strLIQUID5_RIGHT = this.m_txtLIQUID5.m_strGetRightText();
                //ȫѪ
                objContent.m_strFBOOL = this.m_txtFullBlood.Text.Trim();
                objContent.m_strFBOOLXML = this.m_txtFullBlood.m_strGetXmlText();
                objContent.m_strFBOOL_RIGHT = this.m_txtFullBlood.m_strGetRightText();
                ///Ѫ��
                objContent.m_strPLASMA = this.m_txtPLASMA.Text.Trim();
                objContent.m_strPLASMAXML = this.m_txtPLASMA.m_strGetXmlText();
                objContent.m_strPLASMA_RIGHT = this.m_txtPLASMA.m_strGetRightText();
                //����
                objContent.m_strNOSE1 = this.m_txtNOSE1.Text.Trim();
                objContent.m_strNOSE1XML = this.m_txtNOSE1.m_strGetXmlText();
                objContent.m_strNOSE1_RIGHT = this.m_txtNOSE1.m_strGetRightText();
                //�ǽ�
                objContent.m_strNOSE2 = this.m_txtNOSE2.Text.Trim();
                objContent.m_strNOSE2XML = this.m_txtNOSE2.m_strGetXmlText();
                objContent.m_strNOSE2_RIGHT = this.m_txtNOSE2.m_strGetRightText();
                //ÿСʱ����
                objContent.m_strINPERHOUR = this.m_txtInperhour.Text.Trim();
                objContent.m_strINPERHOURXML = this.m_txtInperhour.m_strGetXmlText();
                objContent.m_strINPERHOUR_RIGHT = this.m_txtInperhour.m_strGetRightText();
                //�ۼ�����
                objContent.m_strINTOTAL = this.m_txtIntotalAll.Text.Trim();
                objContent.m_strINTOTALXML = this.m_txtIntotalAll.m_strGetXmlText();
                objContent.m_strINTOTAL_RIGHT = this.m_txtIntotalAll.m_strGetRightText();
                //С��
                objContent.m_strSTOOL = this.m_txtPISS.Text.Trim();
                objContent.m_strSTOOLXML = this.m_txtPISS.m_strGetXmlText();
                objContent.m_strSTOOL_RIGHT = this.m_txtPISS.m_strGetRightText();
                //С���ۼ�
                objContent.m_strSTOOLTOTAL = this.m_txtPISStoal.Text.Trim();
                objContent.m_strSTOOLTOTALXML = this.m_txtPISStoal.m_strGetXmlText();
                objContent.m_strSTOOLTOTAL_RIGHT = this.m_txtPISStoal.m_strGetRightText();
                //���
                objContent.m_strPISS = this.m_txtstool.Text.Trim();
                objContent.m_strPISSXML = this.m_txtstool.m_strGetXmlText();
                objContent.m_strPISS_RIGHT = this.m_txtstool.m_strGetRightText();

                //����ۼ�
                objContent.m_strPISSTOTAL = this.m_txtStooltoalall.Text.Trim();
                objContent.m_strPISSTOTALXML = this.m_txtStooltoalall.m_strGetXmlText();
                objContent.m_strPISSTOTAL_RIGHT = this.m_txtStooltoalall.m_strGetRightText();

                //��Һ1
                objContent.m_strDRAIN1 = this.m_txtDRAIN1.Text.Trim();
                objContent.m_strDRAIN1XML = this.m_txtDRAIN1.m_strGetXmlText();
                objContent.m_strDRAIN1_RIGHT = this.m_txtDRAIN1.m_strGetRightText();
                //��Һ2
                objContent.m_strDRAIN2 = this.m_txtDRAIN2.Text.Trim();
                objContent.m_strDRAIN2XML = this.m_txtDRAIN2.m_strGetXmlText();
                objContent.m_strDRAIN2_RIGHT = this.m_txtDRAIN2.m_strGetRightText();
                //��Һ3
                objContent.m_strDRAIN3 = this.m_txtDRAIN3.Text.Trim();
                objContent.m_strDRAIN3XML = this.m_txtDRAIN3.m_strGetXmlText();
                objContent.m_strDRAIN3_RIGHT = this.m_txtDRAIN3.m_strGetRightText();
                //��Һ4
                objContent.m_strDRAIN4 = this.m_txtDRAIN4.Text.Trim();
                objContent.m_strDRAIN4XML = this.m_txtDRAIN4.m_strGetXmlText();
                objContent.m_strDRAIN4_RIGHT = this.m_txtDRAIN4.m_strGetRightText();
                //��Һ5
                objContent.m_strDRAIN5 = this.m_txtDRAIN5.Text.Trim();
                objContent.m_strDRAIN5XML = this.m_txtDRAIN5.m_strGetXmlText();
                objContent.m_strDRAIN5_RIGHT = this.m_txtDRAIN5.m_strGetRightText();
                //ÿСʱ��Һ�ۼ�
                objContent.m_strOUTPERHOUR = this.m_txtouthour.Text.Trim();
                objContent.m_strOUTPERHOURXML = this.m_txtouthour.m_strGetXmlText();
                objContent.m_strOUTPERHOUR_RIGHT = this.m_txtouthour.m_strGetRightText();
                //��Һ�����ۼ�
                objContent.m_strOUTTOTAL = this.m_txtouttoalall.Text.Trim();
                objContent.m_strOUTTOTALXML = this.m_txtouttoalall.m_strGetXmlText();
                objContent.m_strOUTTOTAL_RIGHT = this.m_txtouttoalall.m_strGetRightText();
                //��λ
                objContent.m_strPOSTURE = this.cmbPOSTURE.Text.Trim();
                objContent.m_strPOSTUREXML = "";
                objContent.m_strPOSTURE_RIGHT = this.cmbPOSTURE.Text.Trim();
                //Ƥ��
                objContent.m_strSKIN = this.cmbskin.Text.Trim();
                objContent.m_strSKINXML = "";
                objContent.m_strSKIN_RIGHT = this.cmbskin.Text.Trim();
                //̵ɫ
                objContent.m_strSPUTUM = this.cmbSPUTUM.Text.Trim();
                objContent.m_strSPUTUMXML = "";
                objContent.m_strSPUTUM_RIGHT = this.cmbSPUTUM.Text.Trim();
                //̵��
                objContent.m_strSPUTUM1 = this.cmbSPUTUM1.Text.Trim();
                objContent.m_strSPUTUM1XML = "";
                objContent.m_strSPUTUM1_RIGHT = this.cmbSPUTUM1.Text.Trim();
                //��̵
                objContent.m_strSUCKER = this.cmbSUCKER.Text.Trim();
                objContent.m_strSUCKERXML = "";
                objContent.m_strSUCKER_RIGHT = this.cmbSUCKER.Text.Trim();
                //��ǻ����
                objContent.m_strORAL = this.cmbORAL.Text.Trim();
                objContent.m_strORALXML = "";
                objContent.m_strORAL_RIGHT = this.cmbORAL.Text.Trim();
                //������ϴ
                objContent.m_strPERINEUM = this.cmbPERINEUM.Text.Trim();
                objContent.m_strPERINEUMXML = "";
                objContent.m_strPERINEUM_RIGHT = this.cmbPERINEUM.Text.Trim();
                //����
                objContent.m_strGESTICULATION = this.cmbGESTICULATION.Text.Trim();
                objContent.m_strGESTICULATIONXML = "";
                objContent.m_strGESTICULATION_RIGHT = this.cmbGESTICULATION.Text.Trim();
                //��ԡ
                objContent.m_strSPONGEBATH = this.cmbSPONGEBATH.Text.Trim();
                objContent.m_strSPONGEBATHXML = "";
                objContent.m_strSPONGEBATH_RIGHT = this.cmbSPONGEBATH.Text.Trim();
                //PT
                objContent.m_strPT = this.m_txtPT.Text.Trim();
                objContent.m_strPTXML = this.m_txtPT.m_strGetXmlText();
                objContent.m_strPT_RIGHT = this.m_txtPT.m_strGetRightText();
                //ACT
                objContent.m_strACT = this.m_txtACT.Text.Trim();
                objContent.m_strACTXML = this.m_txtACT.m_strGetXmlText();
                objContent.m_strACT_RIGHT = this.m_txtACT.m_strGetRightText();
                //GlU
                objContent.m_strGLU = this.m_txtglu.Text.Trim();
                objContent.m_strGLUXML = this.m_txtglu.m_strGetXmlText();
                objContent.m_strGLU_RIGHT = this.m_txtglu.m_strGetRightText();
                //K
                objContent.m_strK = this.m_txtKplus.Text.Trim();
                objContent.m_strKXML = this.m_txtKplus.m_strGetXmlText();
                objContent.m_strK_RIGHT = this.m_txtKplus.m_strGetRightText();
                //Na��
                objContent.m_strNAPLUS = this.m_txtNa.Text.Trim();
                objContent.m_strNAPLUSXML = this.m_txtNa.m_strGetXmlText();
                objContent.m_strNAPLUS_RIGHT = this.m_txtNa.m_strGetRightText();
                //CL-
                objContent.m_strCL = this.m_txtCI.Text.Trim();
                objContent.m_strCLXML = this.m_txtCI.m_strGetXmlText();
                objContent.m_strCL_RIGHT = this.m_txtCI.m_strGetRightText();
                //Ca����
                objContent.m_strCAPLUS2 = this.m_txtcaplus.Text.Trim();
                objContent.m_strCAPLUS2XML = this.m_txtcaplus.m_strGetXmlText();
                objContent.m_strCAPLUS2_RIGHT = this.m_txtcaplus.m_strGetRightText();
                //ͨ��ģʽ
                objContent.m_strMMODEL = this.m_cboBREATHMACHINE.Text.Trim();
                objContent.m_strMMODELXML = "";
                objContent.m_strMMODEL_RIGHT = this.m_cboBREATHMACHINE.Text.Trim();
                //PEE
                objContent.m_strPEE = this.m_txtPEE.Text.Trim();
                objContent.m_strPEEXML = this.m_txtPEE.m_strGetXmlText();
                objContent.m_strPEE_RIGHT = this.m_txtPEE.m_strGetRightText();
                //TV/Ti
                objContent.m_strTVTI = this.m_txtTVTi.Text.Trim();
                objContent.m_strTVTIXML = this.m_txtTVTi.m_strGetXmlText();
                objContent.m_strTVTI_RIGHT = this.m_txtTVTi.m_strGetRightText();
                //Ƶ��
                objContent.m_strFREQUENCY = this.m_txtFREQUENCY.Text.Trim();
                objContent.m_strFREQUENCYXML = this.m_txtFREQUENCY.m_strGetXmlText();
                objContent.m_strFREQUENCY_RIGHT = this.m_txtFREQUENCY.m_strGetRightText();
                //��Ũ��
                objContent.m_strO2 = this.m_txto2.Text.Trim();
                objContent.m_strO2XML = this.m_txto2.m_strGetXmlText();
                objContent.m_strO2_RIGHT = this.m_txto2.m_strGetRightText();
                //������
                objContent.m_strSENSEIIVE = this.m_txtSENSEIIVE.Text.Trim();
                objContent.m_strSENSEIIVEXML = this.m_txtSENSEIIVE.m_strGetXmlText();
                objContent.m_strSENSEIIVE_RIGHT = this.m_txtSENSEIIVE.m_strGetRightText();
                //����
                objContent.m_strSPEED = this.m_txtSPEED.Text.Trim();
                objContent.m_strSPEEDXML = this.m_txtSPEED.m_strGetXmlText();
                objContent.m_strSPEED_RIGHT = this.m_txtSPEED.m_strGetRightText();
                //I:E
                objContent.m_strIE = this.m_txtIE.Text.Trim();
                objContent.m_strIEXML = this.m_txtIE.m_strGetXmlText();
                objContent.m_strIE_RIGHT = this.m_txtIE.m_strGetRightText();
                //����ѹ��
                objContent.m_strGASPRESS = this.m_txtGASPRESS.Text.Trim();
                objContent.m_strGASPRESSXML = this.m_txtGASPRESS.m_strGetXmlText();
                objContent.m_strGASPRESS_RIGHT = this.m_txtGASPRESS.m_strGetRightText();
                //TV
                objContent.m_strTV = this.m_txtTV.Text.Trim();
                objContent.m_strTVXML = this.m_txtTV.m_strGetXmlText();
                objContent.m_strTV_RIGHT = this.m_txtTV.m_strGetRightText();
                //MV
                objContent.m_strMV = this.m_txtMV.Text.Trim();
                objContent.m_strMVXML = this.m_txtMV.m_strGetXmlText();
                objContent.m_strMV_RIGHT = this.m_txtMV.m_strGetRightText();
                //�ܵ�ѹ��
                objContent.m_strPIPEDEPTH = this.m_txtPIPEDEPTH.Text.Trim();
                objContent.m_strPIPEDEPTHXML = this.m_txtPIPEDEPTH.m_strGetXmlText();
                objContent.m_strPIPEDEPTH_RIGHT = this.m_txtPIPEDEPTH.m_strGetRightText();
                //����ѹ��
                objContent.m_strAIRCELLPRESS = this.m_txtAIRCELLPRESS.Text.Trim();
                objContent.m_strAIRCELLPRESSXML = this.m_txtAIRCELLPRESS.m_strGetXmlText();
                objContent.m_strAIRCELLPRESS_RIGHT = this.m_txtAIRCELLPRESS.m_strGetRightText();
                //ETCO2
                objContent.m_strETCO2 = this.m_txtETCO2.Text.Trim();
                objContent.m_strETCO2XML = this.m_txtETCO2.Text.Trim();
                objContent.m_strETCO2_RIGHT = this.m_txtETCO2.m_strGetRightText();
                //PH
                objContent.m_strPH = this.m_txtPH.Text.Trim();
                objContent.m_strPHXML = this.m_txtPH.m_strGetXmlText();
                objContent.m_strPH_RIGHT = this.m_txtPH.m_strGetRightText();
                //m_txtPCO2
                objContent.m_strPCO2 = this.m_txtPCO2.Text.Trim();
                objContent.m_strPCO2XML = this.m_txtPCO2.m_strGetXmlText();
                objContent.m_strPCO2_RIGHT = this.m_txtPCO2.m_strGetRightText();
                //m_txtPAO2
                objContent.m_strPAO2 = this.m_txtPAO2.Text.Trim();
                objContent.m_strPAO2XML = this.m_txtPAO2.m_strGetXmlText();
                objContent.m_strPAO2_RIGHT = this.m_txtPAO2.m_strGetRightText();
                //m_txtHCO3
                objContent.m_strHCO3 = this.m_txtHCO3.Text.Trim();
                objContent.m_strHCO3XML = this.m_txtHCO3.m_strGetXmlText();
                objContent.m_strHCO3_RIGHT = this.m_txtHCO3.m_strGetRightText();
                //m_txtBE
                objContent.m_strBE = this.m_txtBE.Text.Trim();
                objContent.m_strBEXML = this.m_txtBE.m_strGetXmlText();
                objContent.m_strBE_RIGHT = this.m_txtBE.m_strGetRightText();
                //��������
                objContent.objRecordContent.m_strCONTENT = this.m_txtContent.Text.Trim();
                objContent.objRecordContent.m_strCONTENTXML = this.m_txtContent.m_strGetXmlText();
                objContent.objRecordContent.m_strCONTENT_RIGHT = this.m_txtContent.m_strGetRightText();
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion

            return (objContent);
        }

        /// <summary>
        /// ���ӽ����ȡ��������
        /// </summary>
        /// <returns></returns>
        protected clsICUNurseRecordContent m_objGetRecordContentFromGUI()
        {
            //�������У��
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                return null;
            clsICUNurseRecordContent objContent = new clsICUNurseRecordContent();
            objContent.m_strRegisterID = mstrRegisterID;
            objContent.m_dtmCreateDate = DateTime.Now;//��ʱ���м�˻����¸�ֵ ȡ�м����ʱ��
            objContent.m_strCreateUserID = clsEMRLogin.LoginInfo.m_strEmpID;//ȡ�ĵ�¼��ID
            objContent.m_dtmDeActivedDate = DateTime.Now;//Ĭ�ϣ�û��ɾ�� �������Ϊ��
            objContent.m_strDeActivedOperatorID = "";
            objContent.m_dtmFirstPrintDate = DateTime.Now;//Ĭ��;
            objContent.m_bytStatus = 2;//�����½������޺ۼ��޸�
            objContent.m_dtmRecordDate = m_dtpCreateDate.Value;//ȡ����ʱ��
            objContent.m_bytIfConfirm = 0;//Ĭ��δ���
            objContent.m_strTRANSFERID = m_strTransferID;

            #region ��ȡǩ��
            int intSignCount = lsvSign.Items.Count;
            //��ȡlsvsignǩ��
            objContent.objSignerArr = new clsEmrSigns_VO[intSignCount];
            strUserIDList = "";
            strUserNameList = "";
            m_mthGetSignArr(new Control[] { lsvSign }, ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);
            //for (int i = 0; i < intSignCount; i++)
            //{
            //    objContent.objSignerArr[i] = new clsEmrSigns_VO();
            //    objContent.objSignerArr[i].objEmployee = new clsEmrEmployeeBase_VO();
            //    objContent.objSignerArr[i].objEmployee = (clsEmrEmployeeBase_VO)(lsvSign.Items[i].Tag);
            //    objContent.objSignerArr[i].controlName = "lsvSign";
            //    objContent.objSignerArr[i].m_strFORMID_VCHR = "frmICUNurseRecordContent";//ע���Сд
            //    objContent.objSignerArr[i].m_strREGISTERID_CHR = mstrRegisterID;
            //    //�ۼ���ʽ 0972,0324,

            //    strUserIDList = strUserIDList + objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR.Trim() + ",";
            //    strUserNameList = strUserNameList + objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR.Trim() + ",";
            //}
            objContent.m_strModifyUserID = strUserIDList;
            objContent.m_strRecordUserID = strUserIDList; //��¼��һ�β�������Ϣ 
            objContent.m_dtmRecordDate = this.m_dtpCreateDate.Value;
            #endregion

            //��������
            objContent.m_strCONTENT = this.m_txtContent.Text.Trim();
            objContent.m_strCONTENTXML = this.m_txtContent.m_strGetXmlText();
            objContent.m_strCONTENT_RIGHT = this.m_txtContent.m_strGetRightText();

            return (objContent);

        }

        /// <summary>
        /// ������������ʾ������
        /// </summary>
        /// <param name="p_objContent"></param>
        protected void m_mthSetRecordContentToGUI(weCare.Core.Entity.clsICUNurseRecordContent p_objContent)
        {
            //�������У��
            if (p_objContent == null)
                return;
            clsICUNurseRecordContent objContent = p_objContent;

            this.lblAfterDays.Text = objContent.m_strAFTEROPDAYS;
            //��������
            this.m_txtContent.m_mthSetNewText(objContent.m_strCONTENT, objContent.m_strCONTENTXML);

            #region ǩ������
            lsvSign.Clear();
            if (objContent.objSignerArr != null)
            {
                m_mthAddSignToListView(lsvSign, objContent.objSignerArr);
                //lsvSign.Items.Clear();
                //for (int i = 0; i < objContent.objSignerArr.Length; i++)
                //{
                //    if (objContent.objSignerArr[i].controlName == "lsvSign")
                //    {
                //        ListViewItem lviNewItem = new ListViewItem(objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR + "(" + objContent.objSignerArr[i].objEmployee.m_strTECHNICALRANK_CHR + ");");
                //        //ID ����ظ���
                //        lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR);
                //        //���� ������
                //        lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
                //        //tag��Ϊ����
                //        lviNewItem.Tag = objContent.objSignerArr[i].objEmployee;
                //        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                //        lsvSign.Items.Add(lviNewItem);
                //    }
                //}
            }
            #endregion ǩ��		


        }

        /// <summary>
        /// ��ɾ����������ʾ������
        /// </summary>
        /// <param name="p_objContent"></param>
        protected override void m_mthSetDeletedGUIFromContent(weCare.Core.Entity.clsTrackRecordContent p_objContent)
        {
            clsICUNurseRecordContent objContent = (clsICUNurseRecordContent)p_objContent;

            this.m_mthClearRecordInfo();

            this.m_dtpCreateDate.Enabled = false;
        }

        /// <summary>
        /// ���ش����ID
        /// </summary>
        public override int m_IntFormID
        {
            get
            {
                return 135;
            }
        }

        /// <summary>
        /// ���ش���ı���
        /// </summary>
        /// <returns></returns>
        public override string m_strReloadFormTitle()
        {
            //���Ӵ�������ʵ��

            return "ICU�����¼";
        }


        #endregion

        private void m_txtCountIN_TextChanged(object sender, EventArgs e)
        {
            com.digitalwave.controls.ctlRichTextBox ctlBox = sender as com.digitalwave.controls.ctlRichTextBox;
            if (ctlBox == null)
            {
                return;
            }

            bool blnIsRight = true;
            double dblTemp = 0;
            if (!double.TryParse(ctlBox.Text, out dblTemp))
            {
                if (!string.IsNullOrEmpty(ctlBox.Text))
                {
                    blnIsRight = false;
                }
                ctlBox.Text = "0";
            }

            m_mthCountIN();

            if (!blnIsRight)
            {
                clsPublicFunction.ShowInformationMessageBox("�������ۼ���Ŀֻ���������֣�");
            }
        }

        private void m_txtCountOUT_TextChanged(object sender, EventArgs e)
        {
            com.digitalwave.controls.ctlRichTextBox ctlBox = sender as com.digitalwave.controls.ctlRichTextBox;
            if (ctlBox == null)
            {
                return;
            }

            bool blnIsRight = true;
            double dblTemp = 0;
            if (!double.TryParse(ctlBox.Text, out dblTemp))
            {
                if (!string.IsNullOrEmpty(ctlBox.Text))
                {
                    blnIsRight = false;
                }
                ctlBox.Text = "0";
            }

            m_mthCountOUT();

            if (!blnIsRight)
            {
                clsPublicFunction.ShowInformationMessageBox("�������ۼ���Ŀֻ���������֣�");
            }
        }

        #region ���������
        /// <summary>
        /// ��������
        /// </summary>
        private void m_mthCountIN()
        {
            double dblIN = 0;
            double dblTemp = 0;
            if (double.TryParse(m_txtLIQUID1.Text, out dblTemp))
            {
                dblIN += dblTemp;
            }

            if (double.TryParse(m_txtLIQUID2.Text, out dblTemp))
            {
                dblIN += dblTemp;
            }

            if (double.TryParse(m_txtLIQUID3.Text, out dblTemp))
            {
                dblIN += dblTemp;
            }

            if (double.TryParse(m_txtLIQUID4.Text, out dblTemp))
            {
                dblIN += dblTemp;
            }

            if (double.TryParse(m_txtLIQUID5.Text, out dblTemp))
            {
                dblIN += dblTemp;
            }

            if (double.TryParse(m_txtFullBlood.Text, out dblTemp))
            {
                dblIN += dblTemp;
            }

            if (double.TryParse(m_txtPLASMA.Text, out dblTemp))
            {
                dblIN += dblTemp;
            }

            if (double.TryParse(m_txtNOSE1.Text, out dblTemp))
            {
                dblIN += dblTemp;
            }

            if (double.TryParse(m_txtNOSE2.Text, out dblTemp))
            {
                dblIN += dblTemp;
            }

            m_txtInperhour.Text = dblIN.ToString();
            m_txtIntotalAll.Text = (dblIN + m_dblCountINBefore).ToString();
        }

        /// <summary>
        /// �������
        /// </summary>
        private void m_mthCountOUT()
        {
            double dblOUT = 0;
            double dblTemp = 0;

            if (double.TryParse(m_txtDRAIN1.Text, out dblTemp))
            {
                dblOUT += dblTemp;
            }

            if (double.TryParse(m_txtDRAIN2.Text, out dblTemp))
            {
                dblOUT += dblTemp;
            }

            if (double.TryParse(m_txtDRAIN3.Text, out dblTemp))
            {
                dblOUT += dblTemp;
            }

            if (double.TryParse(m_txtDRAIN4.Text, out dblTemp))
            {
                dblOUT += dblTemp;
            }

            if (double.TryParse(m_txtDRAIN5.Text, out dblTemp))
            {
                dblOUT += dblTemp;
            }

            if (double.TryParse(m_txtPISS.Text, out dblTemp))
            {
                dblOUT += dblTemp;
                m_txtPISStoal.Text = (dblTemp + m_dblStoolTotalBefore).ToString();
            }

            if (double.TryParse(m_txtstool.Text, out dblTemp))
            {
                dblOUT += dblTemp;
                m_txtStooltoalall.Text = (dblTemp + m_dblPissTotalBefore).ToString();
            }

            m_txtouthour.Text = dblOUT.ToString();
            m_txtouttoalall.Text = (dblOUT + m_dblCountOUTBefore).ToString();
        }
        #endregion

        /// <summary>
        /// ��ȡָ��ʱ��֮ǰ������
        /// ���ΪDateTime.MinValue�����ȡ���һ����¼������
        /// </summary>
        /// <param name="p_dtmSpecify"></param>
        private void m_mthGetTotalBefore(DateTime p_dtmSpecify)
        {
            try
            {
                //clsICUNurseService objServ =
                //    (clsICUNurseService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUNurseService));

                long lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetLastGross(mstrRegisterID, m_strTransferID, p_dtmSpecify, out m_dblCountINBefore, out m_dblCountOUTBefore, out m_dblStoolTotalBefore, out m_dblPissTotalBefore);
                //objServ = null;
            }
            catch (Exception Ex)
            {
                string strEx = Ex.Message;
            }
        }
    }
}