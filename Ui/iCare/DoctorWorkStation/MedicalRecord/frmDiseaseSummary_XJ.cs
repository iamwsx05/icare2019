using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using com.digitalwave.Emr.Signature_gui;
using System.Windows.Forms;
using iCareData;
using com.digitalwave.Utility.Controls;
using System.Data;
using HRP;


namespace iCare
{
	/// <summary>
	/// ���׶�С��---�½������̼�¼�Ӵ����ʵ��,SGH-2008-1-21
	/// </summary>
    public partial class frmDiseaseSummary_XJ : iCare.frmDiseaseTrackBase
	{
        private clsEmployeeSignTool m_objSignTool;

        //����ǩ����
        private clsEmrSignToolCollection m_objSign;

        public frmDiseaseSummary_XJ()
        {
            InitializeComponent();

            //ָ��ҽ������վ��
            intFormType = 1;
            //			m_objSignTool = new clsEmployeeSignTool(m_lsvEmployee_New);
            //			m_objSignTool.m_mthAddControl(m_txtSign);

            cmdConfirm.Visible = false;

            m_mthSetRichTextBoxAttribInControl(this);

            this.Text = "�׶�С��";
            lblCreateDateTitle.Text = "�Ӱ�ʱ��:";
            this.m_lblForTitle.Text = this.Text;
            if (m_trnRoot != null)
                m_trnRoot.Text = "�Ӱ�ʱ��";

            //			m_lblSign.Text=MDIParent.OperatorName;	


            //			//ǩ������ֵ
            //			m_objCUTC = new clsCommonUseToolCollection(this);
            //			m_objCUTC.m_mthBindEmployeeSign(new Control[]{this.m_cmdEmployeeSign },
            //				new Control[]{this.m_txtSign },new int[]{1});

            //ǩ������ֵ
            m_objSign = new clsEmrSignToolCollection();
            //m_mthBindEmployeeSign(��ť,ǩ����,ҽ��1or��ʿ2,�����֤trueorfalse);
            m_objSign.m_mthBindEmployeeSign(m_cmdEmployeeSign, lsvSign, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);

        }


        public override int m_IntFormID
        {
            get
            {
                return 207;
            }
        }

        /// <summary>
        /// ��ȡ��ǰ�����ⲡ�̼�¼��Ϣ
        /// </summary>
        /// <returns></returns>
        public override clsDiseaseTrackInfo m_objGetDiseaseTrackInfo()
        {
            clsDiseaseSummaryInfo_XJ objTrackInfo = new clsDiseaseSummaryInfo_XJ(m_objCurrentPatient);

            objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;//m_objGetContentFromGUI();
            objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
            objTrackInfo.m_StrTitle = "�׶�С��";

            //����m_strTitle��m_dtmRecordTime
            if (objTrackInfo.m_ObjRecordContent != null)
            {
                m_dtpCreateDate.Value = objTrackInfo.m_ObjRecordContent.m_dtmCreateDate;
            }
            return objTrackInfo;
        }

        /// <summary>
        /// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
        /// </summary>
        protected override void m_mthClearRecordInfo()
        {
            //Ĭ��ǩ��
            MDIParent.m_mthSetDefaulEmployee(lsvSign);


            //��վ����¼����			
            txtruyuanqingkuang.m_mthClearText();
            txtzhongyiruyuan.m_mthClearText();
            txtxiyiruyuan.m_mthClearText();
            txtzhenliaojingguo.m_mthClearText();
            txtmuqianqingkuang.m_mthClearText();
            txtzhongyimuqian.m_mthClearText();
            txtxiyimuqian.m_mthClearText();
            txtzhuyishixiang.m_mthClearText();
        }

        /// <summary>
        /// �����Ƿ����ѡ���˺ͼ�¼ʱ���б��ڴӲ��̼�¼�������ʱ��Ҫʹ�á�
        /// </summary>
        /// <param name="p_blnEnable"></param>
        protected override void m_mthEnablePatientSelectSub(bool p_blnEnable)
        {
            if (p_blnEnable == false)
            {
                //foreach(Control control in this.Controls)
                //{					
                //    if(control.Name!="m_dtpCreateDate")
                //        control.Top=control.Top-105;				
                //}

                cmdConfirm.Visible = true;

                //this.Size=new Size(this.Size.Width, this.Size.Height-105);
                this.CenterToParent();

                //lblCreateDateTitle.Left=lblOriginalDiagnoseTitle.Left;//=16;
                //lblCreateDateTitle.Top=15;	
                //m_dtpCreateDate.Left=lblCreateDateTitle.Right+5;
                //m_dtpCreateDate.Top=lblCreateDateTitle.Top;				
            }
            //this.MaximizeBox=false;
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
            clsDiseaseSummaryRecordContent_XJ objContent = new clsDiseaseSummaryRecordContent_XJ();
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
            //    objContent.objSignerArr[i].m_strFORMID_VCHR="frmHandOver";//ע���Сд
            //    objContent.objSignerArr[i].m_strREGISTERID_CHR=com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            //    //�ۼ���ʽ 0972,0324,
            //    strUserIDList=strUserIDList+objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR.Trim()+",";
            //    strUserNameList=strUserNameList+objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR.Trim()+",";
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

            objContent.m_dtmCreateDate = m_dtpCreateDate.Value;

            objContent.m_strRuYuanQingKuang_Right = txtruyuanqingkuang.m_strGetRightText();
            objContent.m_strRuYuanQingKuang = txtruyuanqingkuang.Text;
            objContent.m_strRuYuanQingKuangXML = txtruyuanqingkuang.m_strGetXmlText();

            objContent.m_strZhongYiRuYuan_Right = txtzhongyiruyuan.m_strGetRightText();
            objContent.m_strZhongYiRuYuan = txtzhongyiruyuan.Text;
            objContent.m_strZhongYiRuYuanXML = txtzhongyiruyuan.m_strGetXmlText();

            objContent.m_strXiYiRuYuan_Right = txtxiyiruyuan.m_strGetRightText();
            objContent.m_strXiYiRuYuan = txtxiyiruyuan.Text;
            objContent.m_strXiYiRuYuanXML = txtxiyiruyuan.m_strGetXmlText();

            objContent.m_strZhenLiaoJingGuo_Right = txtzhenliaojingguo.m_strGetRightText();
            objContent.m_strZhenLiaoJingGuo = txtzhenliaojingguo.Text;
            objContent.m_strZhenLiaoJingGuoXML = txtzhenliaojingguo.m_strGetXmlText();

            objContent.m_strMuQianQingKuang_Right = txtmuqianqingkuang.m_strGetRightText();
            objContent.m_strMuQianQingKuang = txtmuqianqingkuang.Text;
            objContent.m_strMuQianQingKuangXML = txtmuqianqingkuang.m_strGetXmlText();

            objContent.m_strZhongYiMuQian_Right = txtzhongyimuqian.m_strGetRightText();
            objContent.m_strZhongYiMuQian = txtzhongyimuqian.Text;
            objContent.m_strZhongYiMuQianXML = txtzhongyimuqian.m_strGetXmlText();

            objContent.m_strXiYiMuQian_Right = txtxiyimuqian.m_strGetRightText();
            objContent.m_strXiYiMuQian = txtxiyimuqian.Text;
            objContent.m_strXiYiMuQianXML = txtxiyimuqian.m_strGetXmlText();

            objContent.m_strZhenLiaoJiHua_Right = txtzhuyishixiang.m_strGetRightText();
            objContent.m_strZhenLiaoJiHua = txtzhuyishixiang.Text;
            objContent.m_strZhenLiaoJiHuaXML = txtzhuyishixiang.m_strGetXmlText();
            
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
            clsDiseaseSummaryRecordContent_XJ objContent = (clsDiseaseSummaryRecordContent_XJ)p_objContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            txtruyuanqingkuang.m_mthSetNewText(objContent.m_strRuYuanQingKuang, objContent.m_strRuYuanQingKuangXML);
            txtzhongyiruyuan.m_mthSetNewText(objContent.m_strZhongYiRuYuan, objContent.m_strZhongYiRuYuanXML);
            txtxiyiruyuan.m_mthSetNewText(objContent.m_strXiYiRuYuan, objContent.m_strXiYiRuYuanXML);

            txtzhenliaojingguo.m_mthSetNewText(objContent.m_strZhenLiaoJingGuo, objContent.m_strZhenLiaoJingGuoXML);
            txtmuqianqingkuang.m_mthSetNewText(objContent.m_strMuQianQingKuang, objContent.m_strMuQianQingKuangXML);
            txtzhongyimuqian.m_mthSetNewText(objContent.m_strZhongYiMuQian, objContent.m_strZhongYiMuQianXML);
            txtxiyimuqian.m_mthSetNewText(objContent.m_strXiYiMuQian, objContent.m_strXiYiMuQianXML);
            txtzhuyishixiang.m_mthSetNewText(objContent.m_strZhenLiaoJiHua, objContent.m_strZhenLiaoJiHuaXML);
           // m_txtReferral.m_mthSetNewText(objContent.m_strReferral, objContent.m_strReferralXML);


            #region ǩ������
            if (objContent.objSignerArr != null)
            {
                m_mthAddSignToListView(lsvSign, objContent.objSignerArr);
                //lsvSign.Items.Clear();
                //for (int i = 0; i < objContent.objSignerArr.Length; i++)
                //{
                //    if (objContent.objSignerArr[i].controlName=="lsvSign")
                //    {
                //        ListViewItem lviNewItem=new 						 ListViewItem(objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
                //        //ID ����ظ���
                //        lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR);
                //        //���� ������
                //        lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
                //        //tag��Ϊ����
                //        lviNewItem.Tag=objContent.objSignerArr[i].objEmployee;
                //        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                //        lsvSign.Items.Add(lviNewItem);
                //    }
                //}
            }
            #endregion ǩ��


            #region ��Ժԭ��(����)
            clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID, m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (objInPatientCaseHisoryDefaultValueArr != null && objInPatientCaseHisoryDefaultValueArr.Length > 0)
            {
                m_lblInHospitalReason.Text = objInPatientCaseHisoryDefaultValueArr[0].m_strMainDescription;
            }
            #endregion ��Ժԭ��(����)
        }


        protected override void m_mthSetDeletedGUIFromContent(clsTrackRecordContent p_objContent)
        {
            if (p_objContent == null)
                return;
            clsDiseaseSummaryRecordContent_XJ objContent = (clsDiseaseSummaryRecordContent_XJ)p_objContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            txtruyuanqingkuang.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strRuYuanQingKuang, objContent.m_strRuYuanQingKuangXML);
            txtzhongyiruyuan.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhongYiRuYuan, objContent.m_strZhongYiRuYuanXML);
            txtxiyiruyuan.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strXiYiRuYuan, objContent.m_strXiYiRuYuanXML);            
            txtzhenliaojingguo.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhenLiaoJingGuo, objContent.m_strZhenLiaoJingGuoXML);

            txtmuqianqingkuang.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strMuQianQingKuang, objContent.m_strMuQianQingKuangXML);
            txtzhongyimuqian.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhongYiMuQian, objContent.m_strZhongYiMuQianXML);
            txtxiyimuqian.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strXiYiMuQian, objContent.m_strXiYiMuQianXML);
            txtzhuyishixiang.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhenLiaoJiHua, objContent.m_strZhenLiaoJiHuaXML);
            //txtzhenliaojingguo.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhenLiaoJingGuo, objContent.m_strZhenLiaoJingGuoXML);


            if (objContent.objSignerArr != null)
            {
                m_mthAddSignToListView(lsvSign, objContent.objSignerArr);
            }

            #region ��Ժԭ��(����)
            clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID, m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (objInPatientCaseHisoryDefaultValueArr != null && objInPatientCaseHisoryDefaultValueArr.Length > 0)
            {
                m_lblInHospitalReason.Text = objInPatientCaseHisoryDefaultValueArr[0].m_strMainDescription;
            }
            #endregion ��Ժԭ��(����)
        }

        /// <summary>
        /// ��ȡ���̼�¼�������ʵ��
        /// </summary>
        /// <returns></returns>
        protected override clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
        {
            //��ȡ���̼�¼�������ʵ��
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.DiseaseSummary_XJ);
        }

        /// <summary>
        /// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
        {
            //��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
            clsDiseaseSummaryRecordContent_XJ objContent = (clsDiseaseSummaryRecordContent_XJ)p_objRecordContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            txtruyuanqingkuang.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strRuYuanQingKuang, objContent.m_strRuYuanQingKuangXML);
            txtzhongyiruyuan.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhongYiRuYuan, objContent.m_strZhongYiRuYuanXML);
            txtxiyiruyuan.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strXiYiRuYuan, objContent.m_strXiYiRuYuanXML);
            txtzhenliaojingguo.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhenLiaoJingGuo, objContent.m_strZhenLiaoJingGuoXML);

            txtmuqianqingkuang.Text =  com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strMuQianQingKuang, objContent.m_strMuQianQingKuangXML);
            txtzhongyimuqian.Text =  com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhongYiMuQian, objContent.m_strZhongYiMuQianXML);
            txtxiyimuqian.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strXiYiMuQian, objContent.m_strXiYiMuQianXML);
            txtzhuyishixiang.Text = com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhenLiaoJiHua, objContent.m_strZhenLiaoJiHuaXML);
        }

        #region ��ӡ(���ӵ��������в���Ҫ�ṩʵ��)
        /// <summary>
        ///  ���ô�ӡ���ݡ�
        /// </summary>
        /// <param name="p_objContent"></param>
        protected override void m_mthSetPrintContent(clsTrackRecordContent p_objContent, DateTime p_dtmFirstPrintDate)
        {
            //ȱʡ�����κζ������Ӵ����������ṩ������
        }

        /// <summary>
        /// ��ʼ����ӡ����
        /// </summary>
        protected override void m_mthInitPrintTool()
        {
            //ȱʡ�����κζ������Ӵ����������ṩ����
            //��ʼ�����ݰ������д�ӡʹ�õ��ı��������塢���ʡ���ˢ����ӡ��ȡ�
        }

        /// <summary>
        /// �ͷŴ�ӡ����
        /// </summary>
        protected override void m_mthDisposePrintTools()
        {
            //ȱʡ�����κζ������Ӵ����������ṩ����
            //�ͷ����ݰ�����ӡʹ�õ������塢���ʡ���ˢ��ʹ��ϵͳ��Դ�ı�����
        }

        /// <summary>
        /// ��ʼ��ӡ��
        /// </summary>
        protected override void m_mthStartPrint()
        {
            //ȱʡʹ�ô�ӡԤ�����Ӵ��������ṩ�µ�ʵ��
            if (m_blnDirectPrint)
            {
                m_pdcPrintDocument.Print();
            }
            else
            {
                PrintTool.frmPrintPreviewDialog ppdPrintPreview = new PrintTool.frmPrintPreviewDialog();
                ppdPrintPreview.Document = m_pdcPrintDocument;
                ppdPrintPreview.ShowDialog();
            }
        }

        /// <summary>
        /// ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        protected override void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {
            //ȱʡ�����κζ������Ӵ����������ṩ����
        }

        /// <summary>
        /// ��ӡҳ
        /// </summary>
        /// <param name="p_objPrintPageArg"></param>
        protected override void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {

        }

        /// <summary>
        /// ��ӡ����ʱ�Ĳ���
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        protected override void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {
            //���Ӵ����������ṩ����
        }
        #endregion ��ӡ

        // ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
        public override string m_strReloadFormTitle()
        {
            //���Ӵ�������ʵ��
            return "�׶�С��";
        }

        /// <summary>
        /// ��ѡ����ڵ�ʱ,���������Ĭ��ֵ(���Ӵ�����Ҫ,������ʵ��)
        /// </summary>
        protected override void m_mthSelectRootNode()
        {
            #region �������Ĭ��ֵ
            if (m_objCurrentPatient != null && m_ObjLastEmrPatientSession != null)
            {
                clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID, m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (objInPatientCaseHisoryDefaultValueArr != null && objInPatientCaseHisoryDefaultValueArr.Length > 0)
                {
                    //m_txtOriginalDiagnose.Text = objInPatientCaseHisoryDefaultValueArr[0].m_strPrimaryDiagnose;
                }
            }
            #endregion �������Ĭ��ֵ
        }

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

        /// <summary>
        /// ���ݸ���
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        protected override void m_mthDataShare(clsPatient p_objSelectedPatient)
        {
            //			clsInPatientCaseHisoryDefaultValue [] objInPatientCaseDefaultValue = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(p_objSelectedPatient.m_StrInPatientID,p_objSelectedPatient.m_DtmLastInDate.ToString());
            //			if(objInPatientCaseDefaultValue !=null && objInPatientCaseDefaultValue.Length >0)
            //			{
            //				this.m_txtOriginalDiagnose.Text = objInPatientCaseDefaultValue[0].m_strPrimaryDiagnose;
            //				this.m_txtCurrentDiagnose.Text = objInPatientCaseDefaultValue[0].m_strPrimaryDiagnose;
            //			}
        }

        //private void frmHandOver_XJ_Load(object sender, EventArgs e)
        //{
        //    if (m_objCurrentPatient != null && m_ObjLastEmrPatientSession != null)
        //    {
        //        m_lblInHospitalDate.Text = m_ObjLastEmrPatientSession.m_dtmHISInpatientDate.ToString("yyyy��MM��dd�� HH:mm");
        //        #region ��Ժԭ��(����)
        //        clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID, m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
        //        if (objInPatientCaseHisoryDefaultValueArr != null && objInPatientCaseHisoryDefaultValueArr.Length > 0)
        //        {
        //            m_lblInHospitalReason.Text = objInPatientCaseHisoryDefaultValueArr[0].m_strMainDescription;
        //        }
        //        #endregion ��Ժԭ��(����)
        //    }

        //    this.m_dtpCreateDate.m_EnmVisibleFlag = MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
        //    this.m_dtpCreateDate.m_mthResetSize();

        //    txtruyuanqingkuang.Focus();
        //}

        private void frmDiseaseSummary_XJ_Load(object sender, EventArgs e)
        {
            if (m_objCurrentPatient != null && m_ObjLastEmrPatientSession != null)
            {
                m_lblInHospitalDate.Text = m_ObjLastEmrPatientSession.m_dtmHISInpatientDate.ToString("yyyy��MM��dd�� HH:mm");
                #region ��Ժԭ��(����)
                clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID, m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (objInPatientCaseHisoryDefaultValueArr != null && objInPatientCaseHisoryDefaultValueArr.Length > 0)
                {
                    m_lblInHospitalReason.Text = objInPatientCaseHisoryDefaultValueArr[0].m_strMainDescription;
                }
                #endregion ��Ժԭ��(����)
            }

            this.m_dtpCreateDate.m_EnmVisibleFlag = MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
            this.m_dtpCreateDate.m_mthResetSize();

            txtruyuanqingkuang.Focus();
        }
        

    }
}