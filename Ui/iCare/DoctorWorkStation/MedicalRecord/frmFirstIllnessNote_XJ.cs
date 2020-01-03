using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using com.digitalwave.Emr.Signature_gui;//��ǩ��
using System.Windows.Forms;
using iCareData;
using com.digitalwave.Utility.Controls;
using System.Data;
using HRP;

namespace iCare
{
    /// <summary>
    /// �״β��̼�¼---�½�
    /// </summary>
    public partial class frmFirstIllnessNote_XJ : iCare.frmDiseaseTrackBase
    {

        //����ǩ����
        private clsEmrSignToolCollection m_objSign;


        public frmFirstIllnessNote_XJ()
        {
            InitializeComponent();

            //ָ��ҽ������վ��
            intFormType = 1;
            cmdConfirm.Visible = false;
            // ����RichTextBox���ԡ����Ҽ��˵����û��������û�ID������������ɫ��˫������ɫ��
            m_mthSetRichTextBoxAttribInControl(this);
            //ǩ������ֵ
            m_objSign = new clsEmrSignToolCollection();
            //m_mthBindEmployeeSign(��ť,ǩ����,ҽ��1or��ʿ2,�����֤trueorfalse);
            m_objSign.m_mthBindEmployeeSign(m_cmdEmployeeSign, lsvSign, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);

            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO != "440605001")
                lblCaseHistoryTitle.Text = "���������������:";

        }

        public override clsDiseaseTrackInfo m_objGetDiseaseTrackInfo()
        {
            clsDiseaseSummaryInfo objTrackInfo = new clsDiseaseSummaryInfo();

            objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;//m_objGetContentFromGUI();
            objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
            objTrackInfo.m_StrTitle = "�״β��̼�¼";

            //����m_strTitle��m_dtmRecordTime
            if (objTrackInfo.m_ObjRecordContent != null)
            {
                m_dtpCreateDate.Value = objTrackInfo.m_ObjRecordContent.m_dtmCreateDate;
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
            //m_txtMostlyContent.m_mthClearText();
            //m_txtOriginalDiagnose.m_mthClearText();
            //m_txtDiagnoseThe.m_mthClearText();
            //m_txtCurePlan.m_mthClearText();
            txtbingliteidian.m_mthClearText();
            txtzhongyibianbingyiju.m_mthClearText();
            txtxiyizhenduanyiju.m_mthClearText();
            txtzhongyijianbiezhenduan.m_mthClearText();
            txtxiyijianbiezhenduan.m_mthClearText();
            txtzhongyichubu.m_mthClearText(); 
            txtxiyichubu.m_mthClearText();
            txtzhenliaojihua.m_mthClearText();

            //Ĭ��ǩ��
            MDIParent.m_mthSetDefaulEmployee(lsvSign);

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

                //m_dtpCreateDate.Left=lblCreateDateTitle.Right+5;
                //m_dtpCreateDate.Top=lblCreateDateTitle.Top;
                //m_dtpCreateDate.Refresh();
            }
            this.MaximizeBox = false;
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
            clsFirstIllnessNoteRecordContent_XJ objContent = new clsFirstIllnessNoteRecordContent_XJ();
            #region �Ƿ�����޺ۼ��޸�
            if (chkModifyWithoutMatk.Checked)
                objContent.m_intMarkStatus = 0;
            else
                objContent.m_intMarkStatus = 1;
            #endregion
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
            //    objContent.objSignerArr[i].m_strFORMID_VCHR="frmFirstIllnessNote";
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
            //��ҽ�粡��֤����
            objContent.m_strZhongYiBianBing_Right = txtzhongyibianbingyiju.m_strGetRightText();
            objContent.m_strZhongYiBianBing = txtzhongyibianbingyiju.Text;
            objContent.m_strZhongYiBianBingXML = txtzhongyibianbingyiju.m_strGetXmlText();
            //(һ)�����ص�
            objContent.m_strBingLiTeDian_Right = txtbingliteidian.m_strGetRightText();
            objContent.m_strBingLiTeDian = txtbingliteidian.Text;
            objContent.m_strBingLiTeDianXML = txtbingliteidian.m_strGetXmlText();
            //(��)��ҽ�������
            objContent.m_strXiYiZhenDuanYiJu_Right = txtxiyizhenduanyiju.m_strGetRightText();
            objContent.m_strXiYiZhenDuanYiJu = txtxiyizhenduanyiju.Text;
            objContent.m_strXiYiZhenDuanYiJuXML = txtxiyizhenduanyiju.m_strGetXmlText();
            //���������Ƽƻ�
            objContent.m_strZhenLiaoJiHua_Right = txtzhenliaojihua.m_strGetRightText();
            objContent.m_strZhenLiaoJiHua = txtzhenliaojihua.Text;
            objContent.m_strZhenLiaoJiHuaXML = txtzhenliaojihua.m_strGetXmlText();
            //��ҽ�������
            objContent.m_strZhongYiJianBie_Right = txtzhongyijianbiezhenduan.m_strGetRightText();
            objContent.m_strZhongYiJianBie = txtzhongyijianbiezhenduan.Text;
            objContent.m_strZhongYiJianBieXML = txtzhongyijianbiezhenduan.m_strGetXmlText();
            //��ҽ�������
            objContent.m_strXiYiJianBie_Right = txtxiyijianbiezhenduan.m_strGetRightText();
            objContent.m_strXiYiJianBie = txtxiyijianbiezhenduan.Text;
            objContent.m_strXiYiJianBieXML = txtxiyijianbiezhenduan.m_strGetXmlText();

            //��ҽ�������
            objContent.m_strZhongYiChuBu_Right = txtzhongyichubu.m_strGetRightText();
            objContent.m_strZhongYiChuBu = txtzhongyichubu.Text;
            objContent.m_strZhongYiChuBuXML = txtzhongyichubu.m_strGetXmlText();
            //��ҽ�������
            objContent.m_strXiYiChuBu_Right = txtxiyichubu.m_strGetRightText();
            objContent.m_strXiYiChuBu = txtxiyichubu.Text;
            objContent.m_strXiYiChuBuXML = txtxiyichubu.m_strGetXmlText();

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
            clsFirstIllnessNoteRecordContent_XJ objContent = (clsFirstIllnessNoteRecordContent_XJ)p_objContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��

            //m_txtMostlyContent.m_mthSetNewText(objContent.m_strMostlyContent, objContent.m_strMostlyContentXML);
            //m_txtOriginalDiagnose.m_mthSetNewText(objContent.m_strOriginalDiagnose, objContent.m_strOriginalDiagnoseXML);
            //m_txtDiagnoseThe.m_mthSetNewText(objContent.m_strDiagnoseDiffe, objContent.m_strDiagnoseDiffeXML);
            //m_txtCurePlan.m_mthSetNewText(objContent.m_strCurePlan, objContent.m_strCurePlanXML);
            txtbingliteidian.m_mthSetNewText(objContent.m_strBingLiTeDian, objContent.m_strBingLiTeDianXML );
            txtzhongyibianbingyiju.m_mthSetNewText(objContent.m_strZhongYiBianBing, objContent.m_strZhongYiBianBingXML);
            txtxiyizhenduanyiju.m_mthSetNewText(objContent.m_strXiYiZhenDuanYiJu, objContent.m_strXiYiZhenDuanYiJuXML);
            txtzhongyijianbiezhenduan.m_mthSetNewText(objContent.m_strZhongYiJianBie, objContent.m_strZhongYiJianBieXML);
            txtxiyijianbiezhenduan.m_mthSetNewText(objContent.m_strXiYiJianBie, objContent.m_strXiYiJianBieXML);
            txtzhongyichubu.m_mthSetNewText(objContent.m_strZhongYiChuBu, objContent.m_strZhongYiChuBuXML);
            txtxiyichubu.m_mthSetNewText(objContent.m_strXiYiChuBu, objContent.m_strXiYiChuBuXML);
            txtzhenliaojihua.m_mthSetNewText(objContent.m_strZhenLiaoJiHua, objContent.m_strZhenLiaoJiHuaXML);
            #region ǩ������
            if (objContent.objSignerArr != null)
            {
                m_mthAddSignToListView(lsvSign, objContent.objSignerArr);
                //lsvSign.Items.Clear();
                //for (int i = 0; i < objContent.objSignerArr.Length; i++)
                //{
                //    if (objContent.objSignerArr[i].controlName=="lsvSign")
                //    {
                //        ListViewItem lviNewItem=new ListViewItem(objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
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

        }

        public override int m_IntFormID
        {
            get
            {
                return 201;
            }
        }


        protected override void m_mthSetDeletedGUIFromContent(clsTrackRecordContent p_objContent)
        {
            if (p_objContent == null)
                return;
            clsFirstIllnessNoteRecordContent_XJ objContent = (clsFirstIllnessNoteRecordContent_XJ)p_objContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��

            //m_txtMostlyContent.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strMostlyContent, objContent.m_strMostlyContentXML);
            //m_txtOriginalDiagnose.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOriginalDiagnose, objContent.m_strOriginalDiagnoseXML);
            //m_txtDiagnoseThe.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strDiagnoseDiffe, objContent.m_strDiagnoseDiffeXML);
            //m_txtCurePlan.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strCurePlan, objContent.m_strCurePlanXML);
            txtbingliteidian.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strBingLiTeDian, objContent.m_strBingLiTeDianXML);
            txtzhongyibianbingyiju.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhongYiBianBing, objContent.m_strZhongYiBianBingXML);
            txtxiyizhenduanyiju.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strXiYiZhenDuanYiJu, objContent.m_strXiYiZhenDuanYiJuXML);
            txtzhongyijianbiezhenduan.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhongYiJianBie, objContent.m_strZhongYiJianBieXML);
            txtxiyijianbiezhenduan.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strXiYiJianBie, objContent.m_strXiYiJianBieXML);
            txtzhongyichubu.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhongYiChuBu, objContent.m_strZhongYiChuBuXML);
            txtxiyichubu.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strXiYiChuBu, objContent.m_strXiYiChuBuXML);
            txtzhenliaojihua.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhenLiaoJiHua, objContent.m_strZhenLiaoJiHuaXML);

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
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.FirstIllnessNote_XJ);
        }

        /// <summary>
        /// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
        {
            //��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
            clsFirstIllnessNoteRecordContent_XJ objContent = (clsFirstIllnessNoteRecordContent_XJ)p_objRecordContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            txtbingliteidian.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strBingLiTeDian, objContent.m_strBingLiTeDianXML);
            txtzhongyibianbingyiju.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhongYiBianBing, objContent.m_strZhongYiBianBingXML);
            txtxiyizhenduanyiju.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strXiYiZhenDuanYiJu, objContent.m_strXiYiZhenDuanYiJuXML);
            txtzhongyijianbiezhenduan.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhongYiJianBie , objContent.m_strZhongYiJianBieXML);

            txtxiyijianbiezhenduan.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strXiYiJianBie, objContent.m_strXiYiJianBieXML);
            txtzhongyichubu.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhongYiChuBu, objContent.m_strZhongYiChuBuXML);
            txtxiyichubu.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strXiYiChuBu, objContent.m_strXiYiChuBuXML);
            txtzhenliaojihua.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strZhenLiaoJiHua, objContent.m_strZhenLiaoJiHuaXML);

        }


        // ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
        public override string m_strReloadFormTitle()
        {
            //���Ӵ�������ʵ��
            return "�״β��̼�¼";
        }

        /// <summary>
        /// ��ѡ����ڵ�ʱ,���������Ĭ��ֵ(���Ӵ�����Ҫ,������ʵ��)
        /// </summary>
        protected override void m_mthSelectRootNode()
        {

        }
            

        private void m_cmdClose_Click(object sender, System.EventArgs e)
        {            
            this.Close();
        }

        private void frmDiseaseSummary_Load(object sender, System.EventArgs e)
        {
            //			m_cmdNewTemplate.Visible = true;
            this.m_dtpCreateDate.m_EnmVisibleFlag = MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
            this.m_dtpCreateDate.m_mthResetSize();

            //m_txtMostlyContent.Focus();
        }


        /// <summary>
        /// ���ݸ���
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        protected override void m_mthDataShare(clsPatient p_objSelectedPatient)
        {
        }

        protected override void m_mthSetDefaultValue(clsPatient p_objPatient)
        {
            //��¼ʱ���סԺ����
            clsInPatientCaseHisoryDefaultValue[] objInPatientCaseDefaultValue = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString());
            if (objInPatientCaseDefaultValue != null && objInPatientCaseDefaultValue.Length > 0)
            {
                m_dtpCreateDate.Value = DateTime.Parse(objInPatientCaseDefaultValue[0].m_strCreateDate);
                m_dtpCreateDate.Refresh();
            }

            //Ĭ��ֵ
            new clsDefaultValueTool(this, p_objPatient).m_mthSetDefaultValue();
            //���϶˿ռ���
            //m_txtMostlyContent.m_mthInsertText("    ", 0);

            //�Զ�����ģ��
            m_mthSetSpecialPatientTemplateSet(p_objPatient);

            if (m_blnHaveAssociateTemplate)
            {
                //				int intIndex1 = m_txtRecordContent.Text.IndexOf("�������");
                //				int intIndex2 = m_txtRecordContent.Text.LastIndexOf("�������");
                //				if(intIndex1 != -1 && intIndex2 > intIndex1)
                //					m_txtRecordContent.Text = m_txtRecordContent.Text.Remove(intIndex1,intIndex2 - intIndex1);
            }


            //			//��ס�������ĸ���������
            //			string strTemplateSetID = m_objTemplateDomain.m_strGetPatientHaveDisease_TemplateSetID(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString(),this.Name,(int)enmAssociate.Disease);
            //			m_txtRecordContent.Tag = m_objTemplateDomain.m_strGetAssociateIDBySetID(strTemplateSetID,(int)enmAssociate.Operation);
        }

        private void frmFirstIllnessNote_XJ_Load(object sender, EventArgs e)
        {
            //			m_cmdNewTemplate.Visible = true;
            this.m_dtpCreateDate.m_EnmVisibleFlag = MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
            this.m_dtpCreateDate.m_mthResetSize();

            //m_txtMostlyContent.Focus();
        }

        private void cmdConfirm_Click(object sender, EventArgs e)
        {
            if (m_lngSave() > 0)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }







         

    }
}