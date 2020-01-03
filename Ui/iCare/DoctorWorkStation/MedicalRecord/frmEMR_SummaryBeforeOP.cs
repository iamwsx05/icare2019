using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;
using System.Data;
using HRP;
using com.digitalwave.Emr.Signature_gui;

namespace iCare
{
    /// <summary>
    /// ��ǰС��
    /// </summary>
    public partial class frmEMR_SummaryBeforeOP : frmDiseaseTrackBase
    {
        #region ȫ�ֱ���
        private com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
        private clsEmrSignToolCollection m_objSign;

        private long m_lngCurrentEMR_SEQ = -1;
        #endregion

        #region ���캯��
        public frmEMR_SummaryBeforeOP()
        {
            InitializeComponent();
            //ָ��ҽ������վ��
            intFormType = 1;
            // ����RichTextBox���ԡ����Ҽ��˵����û��������û�ID������������ɫ��˫������ɫ��
            m_mthSetRichTextBoxAttribInControl(this);
            //ǩ������ֵ
            m_objSign = new clsEmrSignToolCollection();
            //m_mthBindEmployeeSign(��ť,ǩ����,ҽ��1or��ʿ2,�����֤trueorfalse);
            m_objSign.m_mthBindEmployeeSign(m_cmdEmployeeSign, lsvSign, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
        } 
        #endregion

        #region ����
        public override clsDiseaseTrackInfo m_objGetDiseaseTrackInfo()
        {
            clsEMR_SummaryBeforeOPInfo objTrackInfo = new clsEMR_SummaryBeforeOPInfo();

            objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;//m_objGetContentFromGUI();
            objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
            objTrackInfo.m_StrTitle = "��ǰС��";

            //����m_strTitle��m_dtmRecordTime
            if (objTrackInfo.m_ObjRecordContent != null)
            {
                m_dtpCreateDate.Value = ((clsEMR_SummaryBeforeOPValue)objTrackInfo.m_ObjRecordContent).m_dtmRECORDDATE;
                m_dtpCreateDate.Refresh();
            }
            return objTrackInfo;
        }

        /// <summary>
        /// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
        /// </summary>
        protected override void m_mthClearRecordInfo()
        {
            //��վ����¼����		
            m_dtpCreateDate.Value = DateTime.Now;
            m_txtDiseaseSummary.m_mthClearText();
            m_txtDiagnoseBeforeOP.m_mthClearText();
            m_txtDiagnosisGist.m_mthClearText();
            m_txtOPIndication.m_mthClearText();
            m_txtOPMode.m_mthClearText();
            m_txtAnaMode.m_mthClearText();
            m_txtProceeding.m_mthClearText();
            m_txtPrepareBeforeOP.m_mthClearText();

            //Ĭ��ǩ��
            MDIParent.m_mthSetDefaulEmployee(lsvSign);

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
        /// �ӽ����ȡ�����¼��ֵ���������ֵ��������null��
        /// </summary>
        /// <returns></returns>
        protected override clsTrackRecordContent m_objGetContentFromGUI()
        {
            //�������У��
            int intSignCount = lsvSign.Items.Count;

            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null || intSignCount == 0)
                return null;
            //�ӽ����ȡ��ֵ
            clsEMR_SummaryBeforeOPValue objContent = new clsEMR_SummaryBeforeOPValue();

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
            //    objContent.objSignerArr[i].m_strFORMID_VCHR = "frmEMR_SummaryBeforeOP";
            //    objContent.objSignerArr[i].m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            //    //�ۼ���ʽ 0972,0324,
            //    strUserIDList = strUserIDList + objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR.Trim() + ",";
            //    strUserNameList = strUserNameList + objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR.Trim() + ",";
            //}
            objContent.m_strModifyUserID = strUserIDList;

            //����Richtextbox��modifyuserID ��modifyuserName
            m_mthSetRichTextBoxAttribInControlWithIDandName(this);
            #region �Ƿ�����޺ۼ��޸�
            if (chkModifyWithoutMatk.Checked)
                objContent.m_intMarkStatus = 0;
            else
                objContent.m_intMarkStatus = 1;
            #endregion
            objContent.m_dtmRECORDDATE = m_dtpCreateDate.Value;

            objContent.m_strDISEASESUMMARY = m_txtDiseaseSummary.Text;
            objContent.m_strDISEASESUMMARY_RIGHT = m_txtDiseaseSummary.m_strGetRightText();
            objContent.m_strDISEASESUMMARYXML = m_txtDiseaseSummary.m_strGetXmlText();

            objContent.m_strDIAGNOSISBEFOREOP = m_txtDiagnoseBeforeOP.Text;
            objContent.m_strDIAGNOSISBEFOREOP_RIGHT = m_txtDiagnoseBeforeOP.m_strGetRightText();
            objContent.m_strDIAGNOSISBEFOREOPXML = m_txtDiagnoseBeforeOP.m_strGetXmlText();

            objContent.m_strDIAGNOSISGIST = m_txtDiagnosisGist.Text;
            objContent.m_strDIAGNOSISGIST_RIGHT = m_txtDiagnosisGist.m_strGetRightText();
            objContent.m_strDIAGNOSISGISTXML = m_txtDiagnosisGist.m_strGetXmlText();

            objContent.m_strOPINDICATION = m_txtOPIndication.Text;
            objContent.m_strOPINDICATION_RIGHT = m_txtOPIndication.m_strGetRightText();
            objContent.m_strOPINDICATIONXML = m_txtOPIndication.m_strGetXmlText();

            objContent.m_strOPMODE = m_txtOPMode.Text;
            objContent.m_strOPMODE_RIGHT = m_txtOPMode.m_strGetRightText();
            objContent.m_strOPMODEXML = m_txtOPMode.m_strGetXmlText();

            objContent.m_strANAMODE = m_txtAnaMode.Text;
            objContent.m_strANAMODE_RIGHT = m_txtAnaMode.m_strGetRightText();
            objContent.m_strANAMODEXML = m_txtAnaMode.m_strGetXmlText();

            objContent.m_strPROCEEDING = m_txtProceeding.Text;
            objContent.m_strPROCEEDING_RIGHT = m_txtProceeding.m_strGetRightText();
            objContent.m_strPROCEEDINGXML = m_txtProceeding.m_strGetXmlText();

            objContent.m_strPREPAREBEFOREOP = m_txtPrepareBeforeOP.Text;
            objContent.m_strPREPAREBEFOREOP_RIGHT = m_txtPrepareBeforeOP.m_strGetRightText();
            objContent.m_strPREPAREBEFOREOPXML = m_txtPrepareBeforeOP.m_strGetXmlText();

            objContent.m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            objContent.m_lngEMR_SEQ = m_lngCurrentEMR_SEQ;
            return objContent;
        }

        /// <summary>
        /// �������¼��ֵ��ʾ�������ϡ�
        /// </summary>
        /// <param name="p_objContent"></param>
        protected override void m_mthSetGUIFromContent(clsTrackRecordContent p_objContent)
        {
            if (p_objContent == null)
                return;
            clsEMR_SummaryBeforeOPValue objContent = (clsEMR_SummaryBeforeOPValue)p_objContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            m_dtpCreateDate.Value = objContent.m_dtmRECORDDATE;
            m_txtDiseaseSummary.m_mthSetNewText(objContent.m_strDISEASESUMMARY, objContent.m_strDISEASESUMMARYXML);
            m_txtDiagnoseBeforeOP.m_mthSetNewText(objContent.m_strDIAGNOSISBEFOREOP, objContent.m_strDIAGNOSISBEFOREOPXML);
            m_txtDiagnosisGist.m_mthSetNewText(objContent.m_strDIAGNOSISGIST, objContent.m_strDIAGNOSISGISTXML);
            m_txtOPIndication.m_mthSetNewText(objContent.m_strOPINDICATION, objContent.m_strOPINDICATIONXML);
            m_txtOPMode.m_mthSetNewText(objContent.m_strOPMODE, objContent.m_strOPMODEXML);
            m_txtAnaMode.m_mthSetNewText(objContent.m_strANAMODE, objContent.m_strANAMODEXML);
            m_txtProceeding.m_mthSetNewText(objContent.m_strPROCEEDING, objContent.m_strPROCEEDINGXML);
            m_txtPrepareBeforeOP.m_mthSetNewText(objContent.m_strPREPAREBEFOREOP, objContent.m_strPREPAREBEFOREOPXML);
            m_lngCurrentEMR_SEQ = objContent.m_lngEMR_SEQ;
            #region ǩ������
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

        }

        public override int m_IntFormID
        {
            get
            {
                return 134;
            }
        }

        protected override void m_mthSetDeletedGUIFromContent(clsTrackRecordContent p_objContent)
        {
            if (p_objContent == null)
                return;
            clsEMR_SummaryBeforeOPValue objContent = (clsEMR_SummaryBeforeOPValue)p_objContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            m_lngCurrentEMR_SEQ = objContent.m_lngEMR_SEQ;
            m_dtpCreateDate.Value = objContent.m_dtmRECORDDATE;
            m_txtDiseaseSummary.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strDISEASESUMMARY, objContent.m_strDISEASESUMMARYXML);
            m_txtDiagnoseBeforeOP.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strDIAGNOSISBEFOREOP, objContent.m_strDIAGNOSISBEFOREOPXML);
            m_txtDiagnosisGist.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strDIAGNOSISGIST, objContent.m_strDIAGNOSISGISTXML);
            m_txtOPIndication.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOPINDICATION, objContent.m_strOPINDICATIONXML);
            m_txtOPMode.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOPMODE, objContent.m_strOPMODEXML);
            m_txtAnaMode.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strANAMODE, objContent.m_strANAMODEXML);
            m_txtProceeding.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strPROCEEDING, objContent.m_strPROCEEDINGXML);
            m_txtPrepareBeforeOP.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strPREPAREBEFOREOP, objContent.m_strPREPAREBEFOREOPXML);

            if (objContent.objSignerArr != null)
            {
                m_mthAddSignToListView(lsvSign, objContent.objSignerArr);
            }
        }

        /// <summary>
        /// ��ȡ���̼�¼�������ʵ��
        /// </summary>
        /// <returns></returns>
        protected override clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
        {
            //��ȡ���̼�¼�������ʵ��
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.EMR_SummaryBeforeOP);
        }

        /// <summary>
        /// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
        {
            if (p_objRecordContent == null)
                return;
            clsEMR_SummaryBeforeOPValue objContent = (clsEMR_SummaryBeforeOPValue)p_objRecordContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            m_lngCurrentEMR_SEQ = objContent.m_lngEMR_SEQ;
            m_dtpCreateDate.Value = objContent.m_dtmRECORDDATE;
            m_txtDiseaseSummary.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strDISEASESUMMARY, objContent.m_strDISEASESUMMARYXML);
            m_txtDiagnoseBeforeOP.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strDIAGNOSISBEFOREOP, objContent.m_strDIAGNOSISBEFOREOPXML);
            m_txtDiagnosisGist.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strDIAGNOSISGIST, objContent.m_strDIAGNOSISGISTXML);
            m_txtOPIndication.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOPINDICATION, objContent.m_strOPINDICATIONXML);
            m_txtOPMode.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOPMODE, objContent.m_strOPMODEXML);
            m_txtAnaMode.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strANAMODE, objContent.m_strANAMODEXML);
            m_txtProceeding.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strPROCEEDING, objContent.m_strPROCEEDINGXML);
            m_txtPrepareBeforeOP.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strPREPAREBEFOREOP, objContent.m_strPREPAREBEFOREOPXML);
        }

        // ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
        public override string m_strReloadFormTitle()
        {
            //���Ӵ�������ʵ��
            return "��ǰС��";
        }

        /// <summary>
        /// ��ѡ����ڵ�ʱ,���������Ĭ��ֵ(���Ӵ�����Ҫ,������ʵ��)
        /// </summary>
        protected override void m_mthSelectRootNode()
        {

        } 
        #endregion

        #region �¼�
        private void cmdConfirm_Click(object sender, System.EventArgs e)
        {
            if (m_lngSave() > 0)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private void m_cmdClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void frmEMR_SummaryBeforeOP_Load(object sender, EventArgs e)
        {
            m_txtDiseaseSummary.Focus();
        } 
        #endregion
    }
}