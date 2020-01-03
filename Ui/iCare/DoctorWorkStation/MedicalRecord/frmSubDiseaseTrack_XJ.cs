//#define FunctionPrivilege
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using iCareData;
using com.digitalwave.Utility.Controls;
using System.Data;
using HRP;
using PrivilegeData;
using com.digitalwave.controls;
using com.digitalwave.emr.AssistModuleVO;


namespace iCare
{
    /// <summary>
    /// ���̼�¼������---�½�
    /// </summary>
    public partial class frmSubDiseaseTrack_XJ : frmRecordsBase
    {
        #region
        private cltDataGridDSTRichTextBox clmContent;
      //  private System.Windows.Forms.MenuItem mniGeneralDisease;
        private System.Windows.Forms.MenuItem mniHandOver;
        private System.Windows.Forms.MenuItem mniTakeOver;
        //private System.Windows.Forms.MenuItem mniConsultation;
        private System.Windows.Forms.MenuItem mniConvey;
        private System.Windows.Forms.MenuItem mniTurnIn;
        private System.Windows.Forms.MenuItem mniDiseaseSummary;
        //private System.Windows.Forms.MenuItem mniCheckRoom;
        //private System.Windows.Forms.MenuItem mniCaseDiscuss;
        //private System.Windows.Forms.MenuItem mniBeforeOperationDiscuss;
        //private System.Windows.Forms.MenuItem mniDeadCaseDiscuss;
        //private System.Windows.Forms.MenuItem mniDead;
        //private System.Windows.Forms.MenuItem mniOutHospital;
        //private System.Windows.Forms.MenuItem mniAfterOperation;
        //private System.Windows.Forms.MenuItem mniSave;
        private System.Windows.Forms.MenuItem mniFirstIllnessNote;
       // private System.Windows.Forms.MenuItem mniSummaryBeforeOP;
        private System.ComponentModel.IContainer components = null;
        private DataGridColumnStyle GridDateColumn;
        #endregion 
        public frmSubDiseaseTrack_XJ()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            //ָ��ҽ������վ��
            intFormType = 1;
            // TODO: Add any initialization after the InitializeComponent call
            //m_dtbRecords.Columns.Add("clmContent",typeof(clsDSTRichTextBoxValue));
            clmContent.m_RtbBase.ScrollBars = RichTextBoxScrollBars.None;
            m_lblForTitle.Text = "�� �� �� ¼";

            //this.dgtsStyles.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[]{																			
            //                                                                                             this.clmContent
            //                                                                                            });
            //                                                                                           // {dataGridTextBoxColumn1,																										
            // this.clmContent,
            // this.dataGridTextBoxColumn2});
            this.dgtsStyles.PreferredColumnWidth = 85;
            this.dgtsStyles.RowHeaderWidth = 15;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
   

        #endregion

        private void CreateDataGridStyle()
        {
            PropertyDescriptorCollection pcol = this.BindingContext[m_dtbRecords].GetItemProperties();

            GridDateColumn = new ColumnStyle(pcol[0]);
            GridDateColumn.NullText = "";
            GridDateColumn.HeaderText = "";
            GridDateColumn.MappingName = "PagiNation";
            GridDateColumn.Width = 10;
            dgtsStyles.GridColumnStyles.Add(GridDateColumn);
            //			dgtsStyles.GridColumnStyles.Add(clmContent);
            //			dgtsStyles.GridColumnStyles.Add(dataGridTextBoxColumn2);
        }

        // ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
        protected override void m_mthClearRecordInfo()
        {

        }

        // ��ʼ���������DataTable��
        // ע�⣬DataTable�ĵ�һ��Column�����Ǵ�ż�¼ʱ����ַ������ڶ���Column�����Ǵ�ż�¼���͵�intֵ��������Column�����Ǵ�ż�¼��OpenDate
        protected override void m_mthInitDataTable(DataTable p_dtbRecordTable)
        {
            //��ż�¼ʱ����ַ���
            p_dtbRecordTable.Columns.Add("CreateDate");

            //��ż�¼���͵�intֵ
            DataColumn dcRecordType = new DataColumn("RecordType", typeof(int));
            p_dtbRecordTable.Columns.Add(dcRecordType);

            //��ż�¼��OpenDate�ַ���
            p_dtbRecordTable.Columns.Add("OpenDate");

            //��ż�¼��ModifyDate�ַ���
            p_dtbRecordTable.Columns.Add("ModifyDate");

            //�����ʾ����
            p_dtbRecordTable.Columns.Add("clmContent", typeof(clsDSTRichTextBoxValue));

            //��ż�¼�ķ�ҳ��־
            p_dtbRecordTable.Columns.Add("PagiNation");

            //��Ŵ�����ID
            p_dtbRecordTable.Columns.Add("CreateUserID");

            m_mthSetControl(clmContent);
            clmContent.m_RtbBase.m_BlnReadOnly = true;
        }

        // ��ȡ��ӵ�DataTable������
        protected override object[][] m_objGetRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
        {
            //���ݲ�ͬ�ı����ͣ���ȡ��Ӧ��clsDiseaseTrackInfo
            clsDiseaseTrackInfo objTrackInfo = null;
            #region
            switch ((enmDiseaseTrackType)p_objTransDataInfo.m_intFlag)
            {
                case enmDiseaseTrackType.FirstIllnessNote:
                    if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "450101001")//���
                    {
                        objTrackInfo = new clsFirstIllnessNoteInfo();
                    }
                    else
                    {
                        objTrackInfo = new clsFirstIllnessNoteInfo_F2();
                        p_objTransDataInfo.m_intFlag = (int)enmDiseaseTrackType.FirstIllnessNote_F2;
                    }
                    break;
                case enmDiseaseTrackType.GeneralDisease:
                    objTrackInfo = new clsGeneralDiseaseInfo();
                    break;

                case enmDiseaseTrackType.HandOver:
                    objTrackInfo = new clsHandOverInfo(m_objCurrentPatient);
                    break;
                case enmDiseaseTrackType.HandOver_XJ:
                    objTrackInfo = new clsHandOverInfo_XJ(m_objCurrentPatient);
                    break;
                case enmDiseaseTrackType.TakeOver:
                    objTrackInfo = new clsTakeOverInfo(m_objCurrentPatient);
                    break;
                case enmDiseaseTrackType.TakeOver_XJ:
                    objTrackInfo = new clsTakeOverInfo_XJ(m_objCurrentPatient);
                    break;
                case enmDiseaseTrackType.Consultation:
                    objTrackInfo = new clsConsultationInfo();
                    break;
                case enmDiseaseTrackType.Convey:
                    objTrackInfo = new clsConveyInfo(m_objCurrentPatient);
                    break;
                case enmDiseaseTrackType.Convey_XJ:
                    objTrackInfo = new clsConveyInfo_XJ(m_objCurrentPatient);
                    break;
                case enmDiseaseTrackType.TurnIn:
                    objTrackInfo = new clsTurnInInfo(m_objCurrentPatient);
                    break;
                case enmDiseaseTrackType.TurnIn_XJ:
                    objTrackInfo = new clsTurnInInfo_XJ(m_objCurrentPatient);
                    break;
                case enmDiseaseTrackType.DiseaseSummary:
                    objTrackInfo = new clsDiseaseSummaryInfo();
                    break;
                case enmDiseaseTrackType.DiseaseSummary_XJ:
                    objTrackInfo = new clsDiseaseSummaryInfo_XJ(m_objCurrentPatient);
                    break;
                case enmDiseaseTrackType.CheckRoom:
                    objTrackInfo = new clsCheckRoomInfo();
                    break;
                case enmDiseaseTrackType.CaseDiscuss:
                    objTrackInfo = new clsCaseDiscussInfo();
                    break;
                case enmDiseaseTrackType.BeforeOperationDiscuss:
                    objTrackInfo = new clsBeforeOperationDiscussInfo();
                    break;
                case enmDiseaseTrackType.DeadCaseDiscuss:
                    objTrackInfo = new clsDeadCaseDiscussInfo();
                    break;
                case enmDiseaseTrackType.DeathCaseDiscuss:
                    objTrackInfo = new clsDeathCaseDiscussInfo();
                    break;
                case enmDiseaseTrackType.AfterOperation:
                    objTrackInfo = new clsAfterOperationInfo();
                    break;
                case enmDiseaseTrackType.Dead:
                    objTrackInfo = new clsDeadRecordInfo(m_objCurrentPatient);
                    break;
                case enmDiseaseTrackType.Death:
                    objTrackInfo = new clsDeathRecordInfo();
                    break;
                case enmDiseaseTrackType.OutHospital:
                    objTrackInfo = new clsOutHospitalInfo();
                    break;
                case enmDiseaseTrackType.Save:
                    objTrackInfo = new clsSaveRecordInfo();
                    break;
                case enmDiseaseTrackType.FirstIllnessNote_ZY:
                    objTrackInfo = new clsFirstIllnessNote_ZYInfo();
                    break;
                case enmDiseaseTrackType.EMR_SummaryBeforeOP:
                    objTrackInfo = new clsEMR_SummaryBeforeOPInfo();
                    break;
                case enmDiseaseTrackType.FirstIllnessNote_F2:
                    objTrackInfo = new clsFirstIllnessNoteInfo_F2();
                    break;
                case enmDiseaseTrackType.FirstIllnessNote_XJ:
                    objTrackInfo = new clsFirstIllnessNoteInfo_XJ();
                    break;

            }
            #endregion
            //����clsDiseaseTrackInfo������
            objTrackInfo.m_ObjRecordContent = p_objTransDataInfo.m_objRecordContent;

            int intCharPerLine = clmContent.Width / 14 - 4;
            int intBytesPerLine = 100;

            //���� clsDiseaseTrackInfo ��õ��ı���Xml  
            string strText = "";
            string strXML = "";

            if ((enmDiseaseTrackType)p_objTransDataInfo.m_intFlag == enmDiseaseTrackType.CaseDiscuss)
            {
                ((clsCaseDiscussInfo)objTrackInfo).m_mthGetFormatTrackInfo(((int)(clmContent.Width / 17.5)) - 5, out strText, out strXML);
            }
            else if ((enmDiseaseTrackType)p_objTransDataInfo.m_intFlag == enmDiseaseTrackType.DeadCaseDiscuss)
            {
                ((clsDeadCaseDiscussInfo)objTrackInfo).m_mthGetFormatTrackInfo(((int)(clmContent.Width / 17.5)) - 5, out strText, out strXML);
            }
            else
            {
                strText = objTrackInfo.m_strGetTrackText();
                strXML = objTrackInfo.m_strGetTrackXml();
            }

            string strSignText = objTrackInfo.m_strGetSignText();
            string strSignXml = objTrackInfo.m_strGetSignXml();

            string strBlanks = "";
            for (int j2 = 0; j2 < intCharPerLine - strSignText.Length; j2++)
            {
                strBlanks += "��";	//ע�⣺�˴����Ŀո���ȫ��ռһ�����ֵĿո�			
            }
            strSignText = strBlanks + strSignText;

            //���DataGridÿ�е���ʾ��Ŀ
            //����ÿ�����ݵ��ı���Xml����
            string[] strTextArr, strXmlArr;

            com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strText, strXML, intBytesPerLine, out strTextArr, out strXmlArr);

            object[][] objData = new object[strTextArr.Length + 1][];

            for (int i = 0; i < objData.Length - 1; i++)
            {
                objData[i] = new object[7];

                //����ֵ
                if (i == 0)
                {
                    //ֻ�ڵ�һ�м�¼����������Ϣ
                    objData[i][0] = objTrackInfo.m_ObjRecordContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat(this.Name));//��ż�¼ʱ����ַ���
                    objData[i][1] = (int)objTrackInfo.m_enmGetTrackType();//��ż�¼���͵�intֵ
                    objData[i][2] = objTrackInfo.m_ObjRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss");//��ż�¼��OpenDate�ַ���
                    objData[i][3] = objTrackInfo.m_ObjRecordContent.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss");//��ż�¼��ModifyDate�ַ���        
                    objData[i][5] = objTrackInfo.m_ObjRecordContent.m_StrPagination;//��ŷ�ҳ
                    objData[i][6] = objTrackInfo.m_ObjRecordContent.m_strCreateUserID;//��ż�¼��createuserid�ַ���        
                }

                clsDSTRichTextBoxValue objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                objclsDSTRichTextBoxValue.m_strText = strTextArr[i];
                objclsDSTRichTextBoxValue.m_strDSTXml = strXmlArr[i];

                objData[i][4] = objclsDSTRichTextBoxValue;//�����ʾ����
            }

            objData[objData.Length - 1] = new object[5];
            //   objData[objData.Length - 1] = new object[6];
            clsDSTRichTextBoxValue objclsDSTRichTextBoxValueSign = new clsDSTRichTextBoxValue();
            objclsDSTRichTextBoxValueSign.m_strText = strSignText;
            objclsDSTRichTextBoxValueSign.m_strDSTXml = strSignXml;

            objData[objData.Length - 1][4] = objclsDSTRichTextBoxValueSign;//�����ʾ����
            //  objData[objData.Length - 1][5] = objclsDSTRichTextBoxValueSign;//�����ʾ����
            return objData;


        }

        // ��ȡ���̼�¼�������ʵ��
        protected override clsRecordsDomain m_objGetRecordsDomain()
        {
            //������һ��ҽ��Ҫ����ʱֱ���ÿ��ҵ�SHORTNO_CHR�����ж�
            if (MDIParent.m_objCurrentDepartment != null && MDIParent.m_objCurrentDepartment.m_strSHORTNO_CHR != null
                && MDIParent.m_objCurrentDepartment.m_strSHORTNO_CHR.Trim() == "1500000" && com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "440104001")
                return new clsRecordsDomain(enmRecordsType.FirstIllnessNote_ZY);
            else
            {
                if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "450101001")//����
                    return new clsRecordsDomain(enmRecordsType.FirstIllnessNote);
                else
                    return new clsRecordsDomain(enmRecordsType.FirstIllnessNote_XJ);
            }
        }

        protected override infPrintRecord m_objGetPrintTool()
        {
            return new clsSubDiseaseTrack_XJPrintTool();
        }

        // ��ȡ������Ӻ��޸ģ���¼�Ĵ��塣
        protected override frmDiseaseTrackBase m_frmGetRecordForm(int p_intRecordType)
        {
            switch ((enmDiseaseTrackType)p_intRecordType)
            {
                //Add notes by jli in 2004-11-22
                case enmDiseaseTrackType.GeneralDisease://���̼�¼
                    return new frmGeneralDisease();
                case enmDiseaseTrackType.HandOver://�����¼
                    return new frmHandOver();
                case enmDiseaseTrackType.HandOver_XJ://�Ӱ��¼---�½�
                    return new frmHandOver_XJ();
                case enmDiseaseTrackType.TakeOver://�Ӱ��¼
                    return new frmTakeOver();
                case enmDiseaseTrackType.TakeOver_XJ://�����¼---�½�
                    return new frmTakeOver_XJ();
                case enmDiseaseTrackType.Consultation://�����¼
                    return new frmConsultation();
                case enmDiseaseTrackType.Convey://ת����¼
                    return new frmConvey();
                case enmDiseaseTrackType.Convey_XJ://ת����¼---�½�
                    return new frmConvey_XJ();
                case enmDiseaseTrackType.TurnIn://ת���¼
                    return new frmTurnIn();
                case enmDiseaseTrackType.TurnIn_XJ://ת���¼---�½�
                    return new frmTurnIn_XJ();
                case enmDiseaseTrackType.DiseaseSummary://�׶�С��
                    return new frmDiseaseSummary();
                case enmDiseaseTrackType.DiseaseSummary_XJ://�׶�С��---�½�
                    return new frmDiseaseSummary_XJ();
                case enmDiseaseTrackType.CheckRoom://�鷿��¼
                    return new frmCheckRoom();
                case enmDiseaseTrackType.CaseDiscuss://��������
                    return new frmCaseDiscuss();
                case enmDiseaseTrackType.BeforeOperationDiscuss://��ǰ����
                    return new frmBeforeOperationDiscuss();
                case enmDiseaseTrackType.DeadCaseDiscuss://������������
                    return new frmDeadCaseDiscuss();
                case enmDiseaseTrackType.DeathCaseDiscuss:
                    return new frmDeathCaseDiscuss();
                case enmDiseaseTrackType.AfterOperation://�����󲡳̼�¼
                    return new frmAfterOperation();
                case enmDiseaseTrackType.Dead://������¼
                    return new frmDeadRecord();
                case enmDiseaseTrackType.Death://������¼(��)
                    return new frmDeathRecord();
                case enmDiseaseTrackType.OutHospital://��Ժ��¼
                    return new frmOutHospital();
                case enmDiseaseTrackType.Save://���ȼ�¼
                    return new frmSaveRecord();
                case enmDiseaseTrackType.FirstIllnessNote:
                    if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "450101001")
                    {
                        return new frmFirstIllnessNote();
                    }
                    else
                    {
                        return new frmFirstIllnessNote_F2();
                    }
                case enmDiseaseTrackType.FirstIllnessNote_ZY:
                    return new frmFirstIllnessNote_ZY();

                case enmDiseaseTrackType.EMR_SummaryBeforeOP:
                    return new frmEMR_SummaryBeforeOP();
                case enmDiseaseTrackType.FirstIllnessNote_F2:
                    return new frmFirstIllnessNote_F2();
                case enmDiseaseTrackType.FirstIllnessNote_XJ: //�״β��̼�¼---�½�
                    return new frmFirstIllnessNote_XJ();
                //case enmDiseaseTrackType.SubDiseaseTrack:
                //    return new frmSubDiseaseTrack();
            }
            return null;
        }

        // ��ȡ��¼����Ҫ��Ϣ�������ȡ����CreateDate,OpenDate,LastModifyDate��
        protected override clsTrackRecordContent m_objGetRecordMainContent(int p_intRecordType,
            object[] p_objDataArr)
        {
            //���� p_intRecordType ��ȡ��Ӧ�� clsTrackRecordContent
            clsTrackRecordContent objContent = null;
            #region
            switch ((enmDiseaseTrackType)p_intRecordType)
            {
                case enmDiseaseTrackType.GeneralDisease:
                    objContent = new clsGeneralDiseaseRecordContent();
                    break;
                case enmDiseaseTrackType.HandOver:
                    objContent = new clsHandOverRecordContent();
                    break;
                case enmDiseaseTrackType.HandOver_XJ:
                    objContent = new clsHandOverRecordContent_XJ();
                    break;
                case enmDiseaseTrackType.TakeOver:
                    objContent = new clsTakeOverRecordContent();
                    break;
                case enmDiseaseTrackType.TakeOver_XJ:
                    objContent = new clsTakeOverRecordContent_XJ();
                    break;
                case enmDiseaseTrackType.Consultation:
                    objContent = new clsConsultationRecordContent();
                    break;
                case enmDiseaseTrackType.Convey:
                    objContent = new clsConveyRecordContent();
                    break;
                case enmDiseaseTrackType.Convey_XJ:
                    objContent = new clsConveyRecordContent_XJ();
                    break;
                case enmDiseaseTrackType.TurnIn:
                    objContent = new clsTurnInRecordContent();
                    break;
                case enmDiseaseTrackType.TurnIn_XJ:
                    objContent = new clsTurnInRecordContent_XJ();
                    break;
                case enmDiseaseTrackType.DiseaseSummary:
                    objContent = new clsDiseaseSummaryRecordContent();
                    break;
                case enmDiseaseTrackType.DiseaseSummary_XJ:
                    objContent = new clsDiseaseSummaryRecordContent_XJ();
                    break;
                case enmDiseaseTrackType.CheckRoom:
                    objContent = new clsCheckRoomRecordContent();
                    break;
                case enmDiseaseTrackType.CaseDiscuss:
                    objContent = new clsCaseDiscussRecordContent();
                    break;
                case enmDiseaseTrackType.BeforeOperationDiscuss:
                    objContent = new clsBeforeOperationDiscussRecordContent();
                    break;
                case enmDiseaseTrackType.DeadCaseDiscuss:
                    objContent = new clsDeadCaseDiscussRecordContent();
                    break;
                case enmDiseaseTrackType.DeathCaseDiscuss:
                    objContent = new clsDeadCaseDiscussRecord_VO();
                    break;
                case enmDiseaseTrackType.AfterOperation:
                    objContent = new clsAfterOperationRecordContent();
                    break;
                case enmDiseaseTrackType.Dead:
                    objContent = new clsDeadRecordContent();
                    break;
                case enmDiseaseTrackType.Death:
                    objContent = new clsDeadRecord_VO();
                    break;
                case enmDiseaseTrackType.OutHospital:
                    objContent = new clsOutHospitalRecordContent();
                    break;
                case enmDiseaseTrackType.Save:
                    objContent = new clsSaveRecordContent();
                    break;
                case enmDiseaseTrackType.FirstIllnessNote:
                    objContent = new clsFirstIllnessNoteRecordContent();
                    break;
                case enmDiseaseTrackType.FirstIllnessNote_ZY:
                    objContent = new clsFirstIllnessNote_ZYRecordContent();
                    break;
                case enmDiseaseTrackType.EMR_SummaryBeforeOP:
                    objContent = new clsEMR_SummaryBeforeOPValue();
                    break;
                case enmDiseaseTrackType.FirstIllnessNote_F2:
                    objContent = new clsFirstIllnessNoteRecordContent();
                    break;
                case enmDiseaseTrackType.FirstIllnessNote_XJ:
                    objContent = new clsFirstIllnessNoteRecordContent_XJ();
                    break;
            }
            #endregion
            if (m_objCurrentPatient != null && m_ObjCurrentEmrPatientSession != null)
                objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            else
            {
                clsPublicFunction.ShowInformationMessageBox("��ǰ����Ϊ��!");
                return null;
            }
            objContent.m_dtmInPatientDate = m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate;
            objContent.m_dtmCreateDate = DateTime.Parse((string)p_objDataArr[0]);
            objContent.m_dtmOpenDate = DateTime.Parse((string)p_objDataArr[2]);
            objContent.m_dtmModifyDate = DateTime.Parse((string)p_objDataArr[3]);
            objContent.m_strCreateUserID = (string)p_objDataArr[6];

            return objContent;
        }

        #region ��Ӳ˵�

        private void mniFirstIllnessNote_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!MDIParent.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				MDIParent.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            if (MDIParent.m_objCurrentDepartment != null)
            {
                if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "440104001")
                {
                    #region ��һ
                    if (MDIParent.m_objCurrentDepartment.m_strSHORTNO_CHR != null)
                    {
                        //������һ��ҽ��Ҫ����ʱֱ���ÿ��ҵ�SHORTNO_CHR�����ж�
                        if (MDIParent.m_objCurrentDepartment.m_strSHORTNO_CHR.Trim() == "1500000")
                            m_mthAddNewRecord((int)enmDiseaseTrackType.FirstIllnessNote_ZY);
                        else if (MDIParent.m_objCurrentDepartment.m_strSHORTNO_CHR.Trim() == "1030200")
                        {
                            bool blnYes = false;
                            for (int i = 0; i < clsEMRLogin.m_ObjCurDeptOfEmpArr.Length; i++)
                            {
                                if (clsEMRLogin.m_ObjCurDeptOfEmpArr[i].strShortNo == "1500000")
                                {
                                    blnYes = true;
                                    break;
                                }
                            }
                            if (blnYes)
                            {
                                if (MessageBox.Show(this, "ʹ����ҽ���׳��밴\"ȷ��\"������\"ȡ��\"��", "ѡ���״β��̼�¼", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    m_mthAddNewRecord((int)enmDiseaseTrackType.FirstIllnessNote_ZY);
                                }
                                else
                                    blnYes = false;
                            }
                            if (!blnYes)
                                m_mthAddNewRecord((int)enmDiseaseTrackType.FirstIllnessNote);
                        }
                        else
                            m_mthAddNewRecord((int)enmDiseaseTrackType.FirstIllnessNote);
                    }
                    #endregion ��һ
                }
                else if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "450101001")//���
                {
                    m_mthAddNewRecord((int)enmDiseaseTrackType.FirstIllnessNote);
                }
                else//����
                {
                    m_mthAddNewRecord((int)enmDiseaseTrackType.FirstIllnessNote_XJ);
                }
            }
            this.Cursor = Cursors.Default;

        }


        private void mniSummaryBeforeOP_Click(object sender, EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            try
            {
                m_mthAddNewRecord((int)enmDiseaseTrackType.EMR_SummaryBeforeOP);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void mniGeneralDisease_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            try
            {
                m_mthAddNewRecord((int)enmDiseaseTrackType.GeneralDisease);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void mniHandOver_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            m_mthAddNewRecord((int)enmDiseaseTrackType.HandOver_XJ);
            this.Cursor = Cursors.Default;
        }

        private void mniTakeOver_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            m_mthAddNewRecord((int)enmDiseaseTrackType.TakeOver_XJ);
            this.Cursor = Cursors.Default;
        }

        private void mniConsultation_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            m_mthAddNewRecord((int)enmDiseaseTrackType.Consultation);
            this.Cursor = Cursors.Default;
        }

        private void mniConvey_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            m_mthAddNewRecord((int)enmDiseaseTrackType.Convey_XJ);
            this.Cursor = Cursors.Default;
        }

        private void mniTurnIn_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            m_mthAddNewRecord((int)enmDiseaseTrackType.TurnIn_XJ);
            this.Cursor = Cursors.Default;
        }
        private void mniDiseaseSummary_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            m_mthAddNewRecord((int)enmDiseaseTrackType.DiseaseSummary_XJ);
            this.Cursor = Cursors.Default;
        }
        private void menuCheckRoom_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            m_mthAddNewRecord((int)enmDiseaseTrackType.CheckRoom);
            this.Cursor = Cursors.Default;
        }

        private void mniCaseDiscuss_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            m_mthAddNewRecord((int)enmDiseaseTrackType.CaseDiscuss);
            this.Cursor = Cursors.Default;
        }

        private void mniBeforeOperationDiscuss_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            m_mthAddNewRecord((int)enmDiseaseTrackType.BeforeOperationDiscuss);
            this.Cursor = Cursors.Default;
        }

        private void mniDeadCaseDiscuss_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            m_mthAddNewRecord((int)enmDiseaseTrackType.DeadCaseDiscuss);
            this.Cursor = Cursors.Default;
        }

        private void mniDead_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            m_mthAddNewRecord((int)enmDiseaseTrackType.Dead);
            this.Cursor = Cursors.Default;
        }

        private void mniOutHospital_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            m_mthAddNewRecord((int)enmDiseaseTrackType.OutHospital);
            this.Cursor = Cursors.Default;
        }

        private void mniAfterOperation_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            m_mthAddNewRecord((int)enmDiseaseTrackType.AfterOperation);
            this.Cursor = Cursors.Default;
        }

        private void mniSave_Click(object sender, System.EventArgs e)
        {
            PrivilegeData.enmPrivilegeSF enmSF = (PrivilegeData.enmPrivilegeSF)Enum.Parse(typeof(PrivilegeData.enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            this.Cursor = Cursors.WaitCursor;
            m_mthAddNewRecord((int)enmDiseaseTrackType.Save);
            this.Cursor = Cursors.Default;
        }
        #endregion ��Ӳ˵�

        /// <summary>
        /// ��ȡ��ǰ���˵���������
        /// </summary>
        /// <param name="p_dtmRecordDate">��¼����</param>
        /// <param name="p_intFormID">����ID</param>
        protected override void m_mthGetDeactiveContent(DateTime p_dtmRecordDate, int p_intFormID)
        {
            m_mthGetDeletedRecord(p_intFormID, p_dtmRecordDate);
        }

        /// <summary>
        /// �Զ����״β��̼�¼
        /// </summary>
        protected override void m_mthAutoAddNewRecord()
        {
            m_blnIfPromtForArchiving = false;
            mniFirstIllnessNote_Click(null, System.EventArgs.Empty);

            if (this.m_FrmCurrentSub != null)
            {
                this.m_FrmCurrentSub.Activate();
                this.m_FrmCurrentSub.WindowState = FormWindowState.Normal;
                this.m_FrmCurrentSub.TopMost = true;
            }
        }

        /// <summary>
        /// �Ƿ񵯳��鵵��ʾ
        /// </summary>
        private bool m_blnIfPromtForArchiving = true;
        /// <summary>
        /// �鵵��ʾ
        /// </summary>
        /// <param name="p_blnIfReadOnly"></param>
        /// <param name="p_strTimeRemaing"></param>
        protected override void m_mthPromtForArchiving(bool p_blnIfReadOnly, string p_strTimeRemaing)
        {
            if (m_blnIfPromtForArchiving)
            {
                if (p_blnIfReadOnly)
                {
                    //clsPublicFunction.ShowInformationMessageBox("�˲��˵����в���Ϊֻ���������޸ġ�");
                }
                else if (p_strTimeRemaing != null && p_strTimeRemaing.Trim().Length != 0)
                {
                    clsPublicFunction.ShowInformationMessageBox("�˲��˵����в�����" + p_strTimeRemaing + "�󽫱�Ϊֻ������Ҫ�޸���ע��ʱ�䡣");
                }

                m_blnIfPromtForArchiving = true;
            }
        }

        /// <summary>
        /// �����Ӵ���
        /// </summary>
        /// <param name="p_frmSubForm"></param>
        protected override void m_mthHandleSubFormClosedWithYes(frmDiseaseTrackBase p_frmSubForm)
        {
            m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
        }
        /// <summary>
        /// ��Tableɾ������
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_dtmCreateRecordTime"></param>
        protected override void m_mthRemoveDataFromDataTable(int p_intRecordType,
            DateTime p_dtmCreateRecordTime)
        {
            m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
        }

        /// <summary>
        ///  ��ʼ��ӡ�� 
        /// </summary>
        protected override void m_mthStartPrint()
        {
            if (m_blnDirectPrint)
            {
                m_pdcPrintDocument.Print();
            }
            else
            {
                ((clsSubDiseaseTrack_XJPrintTool)objPrintTool).m_mthPrintPage();

            }
        }
        /// <summary>
        /// �Ҽ���ӡ�����/��ҳ��
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSubDiseaseTrack_XJ_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.MenuItem mniPageAdd = new System.Windows.Forms.MenuItem();
            mniPageAdd.Index = 0;
            mniPageAdd.Text = "����";
            mniPageAdd.Click += new System.EventHandler(mniPageAdd_Click);
            System.Windows.Forms.MenuItem mniPageRemove = new System.Windows.Forms.MenuItem();
            mniPageRemove.Index = 1;
            mniPageRemove.Text = "���";
            mniPageRemove.Click += new System.EventHandler(mniPageRemove_Click);
            System.Windows.Forms.MenuItem menuItem1 = new System.Windows.Forms.MenuItem();
            menuItem1.Index = 5;
            menuItem1.Text = "-";
            System.Windows.Forms.MenuItem menuItem2 = new System.Windows.Forms.MenuItem();
            menuItem2.Index = 6;
            menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] 
			{
				mniPageAdd,
				mniPageRemove
			});
            menuItem2.Text = "���÷�ҳ";

            System.Windows.Forms.MenuItem menuItem3 = new System.Windows.Forms.MenuItem();
            menuItem3.Index = 7;
            menuItem3.Text = "-";
            System.Windows.Forms.MenuItem mniAbnormalInfo = new System.Windows.Forms.MenuItem();
            mniAbnormalInfo.Index = 3;
            mniAbnormalInfo.Text = "�쳣��Ϣ";
            mniAbnormalInfo.Click += new System.EventHandler(this.mniAbnormalInfo_Click);
            mniAbnormalInfo.Enabled = true;
            this.ctmRecordControl.MenuItems.Add(menuItem1);
            this.ctmRecordControl.MenuItems.Add(menuItem2);
            this.ctmRecordControl.MenuItems.Add(menuItem3);
            this.ctmRecordControl.MenuItems.Add(mniAbnormalInfo);
            CreateDataGridStyle();
        }

        private clsInPatientEvaluateDomain m_objInPatientEvaluateDomain = new clsInPatientEvaluateDomain();

        private void mniAbnormalInfo_Click(object sender, System.EventArgs e)
        {
            string strAbnormalInfo;
            if (txtInPatientID.Text.Trim() == "" || this.m_trvInPatientDate.SelectedNode == null || this.m_trvInPatientDate.SelectedNode == m_trvInPatientDate.Nodes[0])
                return;
            long lngRes = m_objInPatientEvaluateDomain.m_lngGetAbnormalInfo(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), out strAbnormalInfo);
            if (lngRes <= 0)
            {
                if (lngRes == (long)iCareData.enmOperationResult.Not_permission)
                    m_mthShowNotPermitted();
                else m_mthShowDBError();
            }
            else
            {
                frmMessageForm frmmessageform = new frmMessageForm("�쳣��Ϣ:", strAbnormalInfo);
                frmmessageform.ShowDialog();
            }
        }
        /// <summary>
        /// ��ӷ�ҳ��־
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniPageAdd_Click(object sender, System.EventArgs e)
        {
            m_mthSetPaginations("1");
        }
        /// <summary>
        /// ɾ����ҳ��־
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniPageRemove_Click(object sender, System.EventArgs e)
        {
            m_mthSetPaginations("0");
        }

        #region ��������
        protected override bool m_blnSubReuse(clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            bool blnIsOK = false;
            if (m_blnSubIsExists())
            {
                return blnIsOK;
            }
            if (p_objSelectedValue != null)
            {
                m_mthGetDeletedRecord(-1, p_objSelectedValue.m_DtmOpenDate);
                blnIsOK = true;
            }
            return blnIsOK;
        }

        protected override clsInactiveRecordInfo_VO[] m_objSubGetAllInactiveInfo(clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            clsInactiveRecordInfo_VO[] objArr = null;
            m_objGetRecordsDomain().m_lngGetAllInactiveInfo(null, p_objSelectedValue.m_StrInpatientId, p_objSelectedValue.m_DtmInpatientDate, out objArr);
            return objArr;
        }
        public override bool m_blnIsNewSetInactiveForm
        {
            get { return true; }
        }
        #endregion ��������
    }
}

