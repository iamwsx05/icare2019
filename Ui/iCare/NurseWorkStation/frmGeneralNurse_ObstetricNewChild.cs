using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;
using System.Data;
using HRP; 
using com.digitalwave.Emr.Signature_gui;

namespace iCare
{
    /// <summary>
    /// ��ɽһ�㻼�߻����¼(����������)������
    /// </summary>
    public partial class frmGeneralNurse_ObstetricNewChild : iCare.frmRecordsBase
    {
        private System.Windows.Forms.DataGridTextBoxColumn clmRecordDateofDay;
        private System.Windows.Forms.DataGridTextBoxColumn clmCreateTime;
        private cltDataGridDSTRichTextBox m_dtcTemperature;
        private cltDataGridDSTRichTextBox m_dtcHeartRate;
        private cltDataGridDSTRichTextBox m_dtcRespiration;
        private cltDataGridDSTRichTextBox m_dtcFontanel;
        private cltDataGridDSTRichTextBox m_dtcCaputsuccedaneum;
        private cltDataGridDSTRichTextBox m_dtcBloodEdema;
        private cltDataGridDSTRichTextBox m_dtcFaceColor;
        private cltDataGridDSTRichTextBox m_dtcCry;
        private cltDataGridDSTRichTextBox m_dtcSuckPower;
        private cltDataGridDSTRichTextBox m_dtcUmbilicalRegion;
        private cltDataGridDSTRichTextBox m_dtcStool;
        private cltDataGridDSTRichTextBox m_dtcUrine;
        private System.Windows.Forms.DataGridTextBoxColumn clmExecuteSign;
        private cltDataGridDSTRichTextBox m_dtcContent;
        private System.Windows.Forms.DataGridTextBoxColumn clmRecordSign;
        private DataTable dtTempTable;
        private string m_strTempColumnName = "";
        public frmGeneralNurse_ObstetricNewChild()
        {
            InitializeComponent();
            dtTempTable = new DataTable("RecordDetail");
        }
        protected override Font m_FntHeaderFont
        {
            get
            {
                return new System.Drawing.Font("SimSun", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            }
        }
        // ��ʼ���������DataTable��
        // ע�⣬DataTable�ĵ�һ��Column�����Ǵ�ż�¼ʱ����ַ������ڶ���Column�����Ǵ�ż�¼���͵�intֵ��������Column�����Ǵ�ż�¼��OpenDate
        protected override void m_mthInitDataTable(DataTable p_dtbRecordTable)
        {

            //��ż�¼ʱ����ַ���
            p_dtbRecordTable.Columns.Add("RecordDate");//0
            //��ż�¼���͵�intֵ
            DataColumn dcRecordType = new DataColumn("RecordType", typeof(int));
            p_dtbRecordTable.Columns.Add(dcRecordType);//1
            //��ż�¼��OpenDate�ַ���
            p_dtbRecordTable.Columns.Add("OpenDate");  //2
            //��ż�¼��ModifyDate�ַ���
            p_dtbRecordTable.Columns.Add("ModifyDate"); //3
            DataColumn dc1 = p_dtbRecordTable.Columns.Add("RecordDateofDay");//4
            dc1.DefaultValue = "";
            DataColumn dc2 = p_dtbRecordTable.Columns.Add("RecordTime");//5
            dc2.DefaultValue = "";
            p_dtbRecordTable.Columns.Add("Temperature", typeof(clsDSTRichTextBoxValue));//6
            p_dtbRecordTable.Columns.Add("HeartRate", typeof(clsDSTRichTextBoxValue));//7
            p_dtbRecordTable.Columns.Add("Respiration", typeof(clsDSTRichTextBoxValue));//8
            p_dtbRecordTable.Columns.Add("Fontanel", typeof(clsDSTRichTextBoxValue));//9
            p_dtbRecordTable.Columns.Add("Caputsuccedaneum", typeof(clsDSTRichTextBoxValue));//10
            p_dtbRecordTable.Columns.Add("BloodEdema", typeof(clsDSTRichTextBoxValue));//11
            p_dtbRecordTable.Columns.Add("FaceColor", typeof(clsDSTRichTextBoxValue));//12
            p_dtbRecordTable.Columns.Add("Cry", typeof(clsDSTRichTextBoxValue));//13
            p_dtbRecordTable.Columns.Add("SuckPower", typeof(clsDSTRichTextBoxValue));//14
            p_dtbRecordTable.Columns.Add("UmbilicalRegion", typeof(clsDSTRichTextBoxValue));//15
            p_dtbRecordTable.Columns.Add("Stool", typeof(clsDSTRichTextBoxValue));//16
            p_dtbRecordTable.Columns.Add("Urine", typeof(clsDSTRichTextBoxValue));//17
            DataColumn dc3 = p_dtbRecordTable.Columns.Add("ExecuseSign");//18
            dc3.DefaultValue = "";
            p_dtbRecordTable.Columns.Add("Content", typeof(clsDSTRichTextBoxValue));//19
            p_dtbRecordTable.Columns.Add("DetailRecordTime");//20
            p_dtbRecordTable.Columns.Add("RecordAndDetailSign");//21
            //��ż�¼������ID
            p_dtbRecordTable.Columns.Add("CreateUserID");//22

            m_dtcContent.m_RtbBase.m_BlnReadOnly = true;
            m_mthSetControl(clmRecordDateofDay);
            m_mthSetControl(clmCreateTime);
            m_mthSetControl(m_dtcTemperature);
            m_mthSetControl(m_dtcHeartRate);
            m_mthSetControl(m_dtcRespiration);
            m_mthSetControl(m_dtcFontanel);
            m_mthSetControl(m_dtcCaputsuccedaneum);
            m_mthSetControl(m_dtcBloodEdema);
            m_mthSetControl(m_dtcFaceColor);
            m_mthSetControl(m_dtcCry);
            m_mthSetControl(m_dtcSuckPower);
            m_mthSetControl(m_dtcUmbilicalRegion);
            m_mthSetControl(m_dtcStool);
            m_mthSetControl(m_dtcUrine);
            m_mthSetControl(clmExecuteSign);
            m_mthSetControl(m_dtcContent);
            m_mthSetControl(clmRecordSign);
            //����������
            this.clmRecordDateofDay.HeaderText = "\r\n\r\n   ����";
            this.clmCreateTime.HeaderText = "\r\n\r\n  ʱ��";
            this.m_dtcTemperature.HeaderText = "  ��\r\n\r\n  ��\r\n\r\n  ��";
            this.m_dtcHeartRate.HeaderText = "  ��\r\n\r\n  ��\r\n\r\n ��/��";
            this.m_dtcRespiration.HeaderText = "  ��\r\n\r\n  ��\r\n\r\n ��/��";
            this.m_dtcFontanel.HeaderText = "  ض\r\n\r\n\r\n\r\n  �� ";
            this.m_dtcCaputsuccedaneum.HeaderText = "  ��\r\n\r\n\r\n\r\n  �� ";
            this.m_dtcBloodEdema.HeaderText = "  Ѫ\r\n\r\n\r\n\r\n  �� ";
            this.m_dtcFaceColor.HeaderText = "  ��\r\n\r\n\r\n\r\n  ɫ";
            this.m_dtcCry.HeaderText = "  ��\r\n\r\n\r\n\r\n  ��";
            this.m_dtcSuckPower.HeaderText = "  ��\r\n\r\n  ˱\r\n\r\n  ��";
            this.m_dtcUmbilicalRegion.HeaderText = "  ��\r\n\r\n\r\n\r\n  ��";
            this.m_dtcStool.HeaderText = "  ��\r\n\r\n  ��\r\n\r\n /��";
            this.m_dtcUrine.HeaderText = "  С\r\n\r\n  ��\r\n\r\n /��";
            this.clmExecuteSign.HeaderText = "\r\n\r\n    ִ��ǩ��";
            this.m_dtcContent.HeaderText = "\r\n\r\n    ����۲졢�����ʩ��Ч��";
            this.clmRecordSign.HeaderText = "\r\n\r\n    ��¼ǩ��";
        }
        //���ó�ʼ�ıȽ�����
        private DateTime m_dtmPreRecordDate;
        protected override void m_mthClearRecordInfo()
        {
            m_dtmPreRecordDate = DateTime.MinValue;
            m_dtgRecordDetail.CurrentRowIndex = 0;
            m_dtbRecords.Rows.Clear();
        }

        /// <summary>
        /// ��ȡ�ۼ�����
        /// </summary>
        /// <param name="p_strText"></param>
        /// <param name="p_strModifyUserID"></param>
        /// <param name="p_strModifyUserName"></param>
        /// <returns></returns>
        private string m_strGetDSTTextXML(string p_strText, string p_strModifyUserID, string p_strModifyUserName)
        {
            return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.s_strMakeDSTXml(p_strText, p_strModifyUserID, p_strModifyUserName, Color.Black, Color.White);
        }

        // ��ȡ���̼�¼�������ʵ��
        protected override clsRecordsDomain m_objGetRecordsDomain()
        {
            return new clsRecordsDomain(enmRecordsType.GeneralNurseRecord_ObstetricNewChildRec);
        }

        // ��ȡ��¼����Ҫ��Ϣ�������ȡ����CreateDate,OpenDate,LastModifyDate��
        protected override clsTrackRecordContent m_objGetRecordMainContent(int p_intRecordType,
            object[] p_objDataArr)
        {
            //���� p_intRecordType ��ȡ��Ӧ�� clsTrackRecordContent
            clsTrackRecordContent objContent = null;
            switch ((enmDiseaseTrackType)p_intRecordType)
            {
                case enmDiseaseTrackType.GeneralNurseRecord_ObstetricNewChild:
                    objContent = new clsGeneralNurseRecordContent_ObstetricNewChild();
                    break;
            }

            if (objContent == null)
                objContent = new clsGeneralNurseRecordContent_ObstetricNewChild();

            if (m_objCurrentPatient != null)
                objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            else
            {
                clsPublicFunction.ShowInformationMessageBox("��ǰ����Ϊ��!");
                return null;
            }
            //int intSelectedRecordStartRow = m_dtgRecordDetail.CurrentCell.RowNumber;
            //string strExecuteSign = (m_dtbRecords.Rows[intSelectedRecordStartRow][22]).ToString();

            //if (strExecuteSign != null && strDetailSign.Trim() != "")
            //{
            //    string[] strArr = strDetailSign.Split('��');
            //    if (strArr != null && strArr[0] != string.Empty)
            //    {
            //        objContent.m_strCreateUserID = strArr[0];
            //    }
            //}
            objContent.m_dtmInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
            objContent.m_dtmCreateDate = DateTime.Parse((string)p_objDataArr[0]);
            objContent.m_dtmOpenDate = DateTime.Parse((string)p_objDataArr[2]);
            objContent.m_dtmModifyDate = DateTime.Parse((string)p_objDataArr[3]);
            objContent.m_strCreateUserID = (string)p_objDataArr[22];

            return objContent;
        }

        private void frmGeneralNurse_ObstetricNewChild_Load(object sender, System.EventArgs e)
        {
            #region ����Ҽ��˵�
            System.Windows.Forms.MenuItem mniContentAdd = new System.Windows.Forms.MenuItem();
            mniContentAdd.Index = 10;
            mniContentAdd.Text = "��Ӳ���۲졢�����ʩ��Ч��";
            mniContentAdd.Click += new System.EventHandler(mniContentAdd_Click);
            System.Windows.Forms.MenuItem mniContentModify = new System.Windows.Forms.MenuItem();
            mniContentModify.Index = 11;
            mniContentModify.Text = "�޸Ĳ���۲졢�����ʩ��Ч��";
            mniContentModify.Click += new System.EventHandler(mniContentModify_Click);
            System.Windows.Forms.MenuItem mniContentDelete = new System.Windows.Forms.MenuItem();
            mniContentDelete.Index = 12;
            mniContentDelete.Text = "ɾ������۲졢�����ʩ��Ч��";
            mniContentDelete.Click += new System.EventHandler(mniContentDelete_Click);
            this.ctmRecordControl.MenuItems.Add(mniContentAdd);
            this.ctmRecordControl.MenuItems.Add(mniContentModify);
            this.ctmRecordControl.MenuItems.Add(mniContentDelete);

            #endregion ;
            m_dtmPreRecordDate = DateTime.MinValue;
            m_dtgRecordDetail.Focus();
            m_mniAddBlank.Visible = false;
            m_mniDeleteBlank.Visible = false;
            this.m_pnlNewBase.Dock = DockStyle.Top;
            this.m_dtgRecordDetail.Dock = DockStyle.Fill;
            this.panel1.Dock = DockStyle.Fill;
            //this.label1.Dock = DockStyle.Bottom;
        }

        private void m_mthSubFormClosed(object p_objSender, EventArgs p_objArg)
        {
            frmGeneralNurse_ObstetricNewChildCon frmAddNewForm = (frmGeneralNurse_ObstetricNewChildCon)p_objSender;
            //��ʾ����

            if (frmAddNewForm.DialogResult == DialogResult.Yes)
            {
                m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
            }
            m_FrmCurrentSub = null;
        }

        /// <summary>
        /// ��Ӳ��̼�¼����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniContentAdd_Click(object sender, System.EventArgs e)
        {
            long lngRes = 0;
            try
            {
                //��֤
                //���ݲ���
                //�򿪴���	
                if (this.m_FrmCurrentSub != null)
                {
                    this.m_FrmCurrentSub.Activate();
                    this.m_FrmCurrentSub.WindowState = FormWindowState.Normal;
                    return;
                }
                frmGeneralNurse_ObstetricNewChildCon frm = new frmGeneralNurse_ObstetricNewChildCon(true, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), "2005");
                frm.Closed += new EventHandler(m_mthSubFormClosed);
                //����
                //frm.TopMost = true;
                this.m_FrmCurrentSub = frm;
                frm.Show();
                if (MDIParent.s_ObjCurrentPatient != null)
                    frm.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
        }
        /// <summary>
        /// �޸Ĳ��̼�¼����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniContentModify_Click(object sender, System.EventArgs e)
        {
            long lngRes = 0;
            try
            {
                if (this.m_FrmCurrentSub != null)
                {
                    this.m_FrmCurrentSub.Activate();
                    this.m_FrmCurrentSub.WindowState = FormWindowState.Normal;
                    return;
                }
                //��֤
                if (m_dtgRecordDetail.CurrentCell.ColumnNumber != 15)
                {
                    MessageBox.Show("��ѡ��һ������۲졢�����ʩ��Ч�������ݣ�");
                    return;
                }

                //���ݲ���
                int intSelectedRecordStartRow = m_dtgRecordDetail.CurrentCell.RowNumber;
                string strRecordTime = (m_dtbRecords.Rows[intSelectedRecordStartRow][20]).ToString();
                if (strRecordTime.Trim().Length == 0)
                    return;
                //�򿪴���
                frmGeneralNurse_ObstetricNewChildCon frm = new frmGeneralNurse_ObstetricNewChildCon(false, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), strRecordTime);
                frm.Closed += new EventHandler(m_mthSubFormClosed);
                //����
                //frm.TopMost = true;
                this.m_FrmCurrentSub = frm;
                frm.Show();
                if (MDIParent.s_ObjCurrentPatient != null)
                    frm.m_strSetRegisterId = MDIParent.s_ObjCurrentPatient.m_StrRegisterId;
                //frm.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
        }
        /// <summary>
        /// ɾ�����̼�¼����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniContentDelete_Click(object sender, System.EventArgs e)
        {
            long lngRes = 0;
            try
            {
                //��֤
                if (m_dtgRecordDetail.CurrentCell.ColumnNumber != 15)
                {
                    MessageBox.Show("��ѡ��һ������۲졢�����ʩ��Ч�������ݣ�");
                    return;
                }
                ////��֤ɾ��
                //clsDeleteVerify objDeleteVerify = new clsDeleteVerify();
                //if (objDeleteVerify.m_mthIsDelete(null, null) == false)
                //{
                //    clsPublicFunction.ShowInformationMessageBox("��֤ʧ�ܲ���ɾ����");
                //    return;
                //}
                //���ݲ���
                int intSelectedRecordStartRow = m_dtgRecordDetail.CurrentCell.RowNumber;

                string strDetailSign = (m_dtbRecords.Rows[intSelectedRecordStartRow][22]).ToString();

                //���� by tfzhang 2006-02-24
                if (strDetailSign != null && strDetailSign.Trim() != "")
                {
                    string[] strArr = strDetailSign.Split(',');
                    //if (strArr != null && strArr.Length > 1 && strArr[1] != string.Empty && strArr[1].Trim() != MDIParent.OperatorID.Trim())
                    //{
                    //    MDIParent.ShowInformationMessageBox("�Ǽ�¼�����߲���ɾ����¼��");
                    //    return;
                    //}
                    //Ȩ���ж�
                    if (strArr.Length > 1)
                    {
                        string strDeptIDTemp = m_ObjCurrentEmrPatientSession.m_strAreaId;// ((clsDepartment)m_cboDept.SelectedItem).m_strDeptNewID; 
                        bool blnIsAllow = clsPublicFunction.IsAllowDelete(strDeptIDTemp, strArr[1], clsEMRLogin.LoginEmployee, intFormType);
                        if (!blnIsAllow)
                            return;
                    }
                }


                string strRecordTime = (m_dtbRecords.Rows[intSelectedRecordStartRow][20]).ToString();
                if (strRecordTime.Trim().Length == 0)
                    return;
                //ȷ��
                if (MessageBox.Show("ȷ��Ҫɾ��ѡ�еĲ���۲졢�����ʩ��Ч�������ݣ�", "ɾ����ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;

                //�򿪴���
                //ɾ��

                //clsGeneralNurseRecord_CSObstetricNewChildService objserv =
                //    (clsGeneralNurseRecord_CSObstetricNewChildService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsGeneralNurseRecord_CSObstetricNewChildService));

                string strDelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string strDelID = MDIParent.OperatorID;
                lngRes = (new weCare.Proxy.ProxyEmr05()).Service.clsGeneralNurseRecord_CSObstetricNewChildService_m_lngDeleteDetail(m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), strRecordTime, strDelTime, strDelID);
                //����
                if (lngRes > 0)
                {
                    m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
                }

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
        }

        // ��ȡ������Ӻ��޸ģ���¼�Ĵ��塣
        protected override frmDiseaseTrackBase m_frmGetRecordForm(int p_intRecordType)
        {
            switch ((enmDiseaseTrackType)p_intRecordType)
            {
                case enmDiseaseTrackType.GeneralNurseRecord_ObstetricNewChildRec:
                    return new frmGeneralNurse_ObstetricNewChildRec();
                case enmDiseaseTrackType.GeneralNurseRecord_ObstetricNewChild:
                    return new frmGeneralNurse_ObstetricNewChildRec();
                case enmDiseaseTrackType.GeneralNurseRecord_ObstetricNewChildCon:
                    return new frmGeneralNurse_ObstetricNewChildCon(false, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), "");
            }

            return null;
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
        /// ��ȡ��ǰ���˵���������
        /// </summary>
        /// <param name="p_dtmRecordDate">��¼����</param>
        /// <param name="p_intFormID">����ID</param>
        protected override void m_mthGetDeactiveContent(DateTime p_dtmRecordDate, int p_intFormID)
        {
            m_mthGetDeletedRecord(p_intFormID, p_dtmRecordDate);
        }

        protected override void m_mthModifyRecord(int p_intRecordType,
            DateTime p_dtmCreateRecordTime)
        {
            enmPrivilegeSF enmSF = (enmPrivilegeSF)Enum.Parse(typeof(enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            //��ȡ��Ӽ�¼�Ĵ���
            frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(p_intRecordType);
            frmAddNewForm.m_mthSetDiseaseTrackInfo(m_objCurrentPatient, p_dtmCreateRecordTime);

            m_mthShowSubForm(frmAddNewForm, p_intRecordType, true);
        }

        protected override void m_mthClearPatientRecordInfo()
        {
            m_mthSetDataGridFirstRowFocus();
            m_dtgRecordDetail.CurrentRowIndex = 0;
            m_dtbRecords.Rows.Clear();
            //��ռ�¼����                       
            m_mthClearRecordInfo();
            dtTempTable.Rows.Clear();
        }

        private void mniAppend_Click(object sender, System.EventArgs e)
        {
            enmPrivilegeSF enmSF = (enmPrivilegeSF)Enum.Parse(typeof(enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            m_mthAddNewRecord((int)enmDiseaseTrackType.GeneralNurseRecord_ObstetricNewChild);
        }

        protected override object[][] m_objGetRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
        {
            #region ��ʾ��¼��DataGrid
            try
            {
                object[] objData;
                ArrayList objReturnData = new ArrayList();
                ArrayList arlDetail = new ArrayList();//��Ų����¼
                int intCurrentDetail = 0;//��ǰ�����¼��ArrayList�е�����
                int intRecordCount = 0;
                bool blnPreIsHide = false;//�ж���һ����¼�Ƿ�����
                int intCurrentSignIndex = 0;//��¼ǩ������
                bool blnMark = false;
                clsGeneralNurseRecordContent_ObstetricNewChildDataInfo objGNRCInfo = new clsGeneralNurseRecordContent_ObstetricNewChildDataInfo();
                clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
                string strText, strXml;

                objGNRCInfo = (clsGeneralNurseRecordContent_ObstetricNewChildDataInfo)p_objTransDataInfo;

                if (objGNRCInfo.m_objRecordArr == null && objGNRCInfo.m_objDetailArr == null)
                    return null;

                #region �Բ���۲졢�����ʩ��Ч�����д���
                if (objGNRCInfo.m_objDetailArr != null)
                {
                    string strDetail = "";
                    string strDetailXML = "";
                    for (int n = 0; n < objGNRCInfo.m_objDetailArr.Length; n++)
                    {
                        clsGeneralNurseRecordContent_ObstetricNewChildDetail objDetail = objGNRCInfo.m_objDetailArr[n];
                        object[] objTemp = new object[7];
                        strDetail = objDetail.m_strRECORDCONTENTAll;
                        strDetailXML = objDetail.m_strRECORDCONTENTXML;
                        string[] strDetailArr;
                        string[] strDetailXMLArr;
                        //�������¼��Ϊ�С�
                        com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strDetail, strDetailXML, 42, out strDetailArr, out strDetailXMLArr);

                        if (strDetail != string.Empty)
                        {
                            objTemp[0] = strDetailArr;
                            objTemp[1] = strDetailXMLArr;
                            objTemp[2] = strDetailArr.Length;
                            objTemp[3] = objDetail.m_dtmRECORDDATE;
                            objTemp[4] = objDetail.objSignerArr;
                            objTemp[5] = objDetail.m_strDetailCreateUserName;
                            objTemp[6] = objDetail.m_strCREATERECORDUSERID;
                            arlDetail.Add(objTemp);
                        }
                    }
                }
                #endregion

                if (objGNRCInfo.m_objRecordArr != null)
                    intRecordCount = objGNRCInfo.m_objRecordArr.Length;
                int intRowOfCurrentDetail = 0;

                #region ��ȡ�޸��޶�ʱ��
                int intCanModifyTime = 0;
                try
                {
                    intCanModifyTime = int.Parse(m_strCanModifyTime);
                }
                catch
                {
                    intCanModifyTime = 6;
                }
                #endregion

                clsGeneralNurseRecordContent_ObstetricNewChild objCurrent;
                clsGeneralNurseRecordContent_ObstetricNewChild objNext;
                for (int i = 0; i < intRecordCount; i++)
                {
                    blnMark = false;
                    objData = new object[23];
                    objCurrent = objGNRCInfo.m_objRecordArr[i];
                    objNext = new clsGeneralNurseRecordContent_ObstetricNewChild();//��һ�������¼
                    if (i < intRecordCount - 1)
                        objNext = objGNRCInfo.m_objRecordArr[i + 1];
                    clsGeneralNurseRecordContent_ObstetricNewChild objLast = null;
                    if (i > 0)
                        objLast = objGNRCInfo.m_objRecordArr[i - 1];
                    //����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ��޸����봴����Ϊͬһ�ˣ�����ʾ
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                      //  && objNext.m_strModifyUserID.Trim() == objCurrent.m_strCreateUserID.Trim()
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate)
                    {
                        TimeSpan tsModify = objNext.m_dtmModifyDate - objCurrent.m_dtmModifyDate;
                        if ((int)tsModify.TotalHours < intCanModifyTime)
                        {
                            blnPreIsHide = true;
                            continue;
                        }
                    }
                    #region ��Źؼ��ֶ�
                    if (objCurrent.m_dtmCreateDate != DateTime.MinValue)
                    {
                        objData[0] = objCurrent.m_dtmRECORDDATE;//��ż�¼ʱ����ַ���
                        objData[1] = (int)enmRecordsType.GeneralNurseRecord_ObstetricNewChildRec;//��ż�¼���͵�intֵ
                        objData[2] = objCurrent.m_dtmOpenDate;//��ż�¼��OpenDate�ַ���
                        objData[3] = objCurrent.m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   
                        objData[22] = objCurrent.m_strCreateUserID;//��ż�¼��createUserid�ַ���   
                        //ͬһ����ֻ�ڵ�һ����ʾ����
                        if (objCurrent.m_dtmRECORDDATE.Date.ToString() != m_dtmPreRecordDate.Date.ToString())
                        {
                            objData[4] = objCurrent.m_dtmRECORDDATE.Date.ToString("yyyy-MM-dd");//�����ַ���
                        }
                        //�޸ĺ���кۼ��ļ�¼������ʾʱ��
                        if (m_dtmPreRecordDate != objCurrent.m_dtmRECORDDATE)
                            objData[5] = objCurrent.m_dtmRECORDDATE.ToString("HH:mm");//ʱ���ַ���
                    }
                    #endregion ;
                    #region ��ŵ�����Ϣ
                    bool blnIsRed = false;
                    //����
                    strText = objCurrent.m_strTEMPERATURE_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strTEMPERATURE_RIGHT != objCurrent.m_strTEMPERATURE_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strTEMPERATURE_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objLast.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strTEMPERATURE_RIGHT) && !blnPreIsHide)
                    {
                        blnIsRed = true;
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    if (blnIsRed)
                    {
                        objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                        blnIsRed = false;
                    }
                    objData[6] = objclsDSTRichTextBoxValue;//T

                    //����
                    strText = objCurrent.m_strHEARTRATE_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strHEARTRATE_RIGHT != objCurrent.m_strHEARTRATE_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strHEARTRATE_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strHEARTRATE_RIGHT) && !blnPreIsHide)
                    {
                        blnIsRed = true;
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    if (blnIsRed)
                    {
                        objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                        blnIsRed = false;
                    }
                    objData[7] = objclsDSTRichTextBoxValue;//HR
                    //����
                    strText = objCurrent.m_strRESPIRATION_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strRESPIRATION_RIGHT != objCurrent.m_strRESPIRATION_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strRESPIRATION_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strRESPIRATION_RIGHT) && !blnPreIsHide)
                    {
                        blnIsRed = true;
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    if (blnIsRed)
                    {
                        objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                        blnIsRed = false;
                    }
                    objData[8] = objclsDSTRichTextBoxValue;//P

                    //ض��
                    strText = objCurrent.m_strFONTANEL;
                    strXml = "<root />";
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[9] = objclsDSTRichTextBoxValue;
                    //����
                    strText = objCurrent.m_strCAPUTSUCCEDANEUM;
                    strXml = "<root />";
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[10] = objclsDSTRichTextBoxValue;
                    //Ѫ��
                    strText = objCurrent.m_strBLOODEDEMA;
                    strXml = "<root />";
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[11] = objclsDSTRichTextBoxValue;
                    //��ɫ
                    strText = objCurrent.m_strFACECOLOR;
                    strXml = "<root />";
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[12] = objclsDSTRichTextBoxValue;
                    //����
                    strText = objCurrent.m_strCRY;
                    strXml = "<root />";
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[13] = objclsDSTRichTextBoxValue;
                    //��˱��
                    strText = objCurrent.m_strSUCKPOWER;
                    strXml = "<root />";
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[14] = objclsDSTRichTextBoxValue;
                    //�겿
                    strText = objCurrent.m_strUMBILICALREGION;
                    strXml = "<root />";
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[15] = objclsDSTRichTextBoxValue;
                    //���
                    strText = objCurrent.m_strSTOOL_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strSTOOL_RIGHT != objCurrent.m_strSTOOL_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strSTOOL_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strSTOOL_RIGHT) && !blnPreIsHide)
                    {
                        blnIsRed = true;
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    if (blnIsRed)
                    {
                        objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                        blnIsRed = false;
                    }
                    objData[16] = objclsDSTRichTextBoxValue;//
                    //С��
                    strText = objCurrent.m_strURINE_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strURINE_RIGHT != objCurrent.m_strURINE_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strURINE_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strURINE_RIGHT) && !blnPreIsHide)
                    {
                        blnIsRed = true;
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    if (blnIsRed)
                    {
                        objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                        blnIsRed = false;
                    }
                    objData[17] = objclsDSTRichTextBoxValue;//
                   
                    #endregion
                    #region ����۲졢�����ʩ��Ч��
                    for (; intCurrentDetail < arlDetail.Count; intCurrentDetail++)
                    {//ѭ��������в����¼
                        if ((DateTime)((object[])arlDetail[intCurrentDetail])[3] == objCurrent.m_dtmRECORDDATE)
                        {//����ǰ��¼�����벡��۲��¼������ͬ���������ǰ��¼�����������۲��¼
                            #region ִ��ǩ��
                            int m_intMax = Math.Max(objCurrent.objSignerArr == null ? 0 : objCurrent.objSignerArr.Length, ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length + ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])).Length - 1);
                            for (int m = 0; m < m_intMax; m++)
                            {
                                if (objCurrent.objSignerArr != null && m < objCurrent.objSignerArr.Length)
                                {
                                    strText = objCurrent.objSignerArr[m].objEmployee.m_strLASTNAME_VCHR;
                                    objData[18] = strText;
                                }
                                if (m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length)
                                {
                                    strText = ((string[])(((object[])arlDetail[intCurrentDetail])[0]))[m];
                                    strXml = ((string[])(((object[])arlDetail[intCurrentDetail])[1]))[m];
                                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                    objclsDSTRichTextBoxValue.m_strText = strText;
                                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                    objData[19] = objclsDSTRichTextBoxValue;
                                    objData[20] = (DateTime)((object[])arlDetail[intCurrentDetail])[3];
                                }
                                if (m >= ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1 && m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length)
                                {
                                    objData[21] = ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4]))[intCurrentSignIndex++].objEmployee.m_strLASTNAME_VCHR;
                                }
                                else
                                {
                                    objData[21] = "";
                                }
                                objReturnData.Add(objData);
                                objData = new object[23];
                            }

                            m_dtmPreRecordDate = objCurrent.m_dtmRECORDDATE;
                            intCurrentSignIndex = 0;
                            intCurrentDetail++;
                            blnMark = true;
                            break;
                            #endregion
                        }
                        else if ((DateTime)(((object[])arlDetail[intCurrentDetail])[3]) < objCurrent.m_dtmRECORDDATE)
                        {//����ǰ��¼���ڴ��ڲ���۲��¼���ڣ����������۲��¼��ѭ����һ������۲��¼
                            for (int j = intRowOfCurrentDetail; j < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length; j++)
                            {
                                object[] objOtherDetail = new object[23];
                                m_mthSetOtherDetail(((object[])arlDetail[intCurrentDetail]), intCurrentDetail, j, objCurrent, out objOtherDetail);
                                if (j == 0)
                                {
                                    //ͬһ����ֻ�ڵ�һ����ʾ����
                                    if (((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("yyyyMMdd") != m_dtmPreRecordDate.Date.ToString("yyyyMMdd") || m_dtmPreRecordDate == DateTime.MinValue)
                                    {
                                        objOtherDetail[4] = ((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("yyyy-MM-dd");
                                    }
                                    objOtherDetail[5] = ((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("HH:mm");
                                }
                                if ((clsEmrSigns_VO[])((object[])arlDetail[intCurrentDetail])[4] != null && j == ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1)
                                {
                                    if (((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])) != null)
                                    {
                                        for (int h = 0; h < ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])).Length; h++)
                                        {
                                            objOtherDetail[21] = ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4]))[h].objEmployee.m_strLASTNAME_VCHR;
                                            objReturnData.Add(objOtherDetail);
                                            objOtherDetail = new object[23];
                                        }
                                    }
                                    else
                                    {
                                        objOtherDetail[21] = "";
                                        objReturnData.Add(objOtherDetail);
                                        objOtherDetail = new object[23];
                                    }
                                }
                                else
                                {
                                    objOtherDetail[21] = "";
                                    objReturnData.Add(objOtherDetail);
                                }

                            }
                            m_dtmPreRecordDate = (DateTime)(((object[])arlDetail[intCurrentDetail])[3]);
                            intRowOfCurrentDetail = 0;

                        }
                        else
                        {//����ǰ��¼����С�ڲ���۲��¼���ڣ��������ǰ��¼��ѭ����һ����ǰ��¼
                            #region ִ��ǩ������¼ǩ��
                            //ͬһ����ֻ�ڵ�һ����ʾ����
                            if (objCurrent.m_dtmRECORDDATE.Date.ToString("yyyyMMdd") != m_dtmPreRecordDate.Date.ToString("yyyyMMdd"))
                            {
                                objData[4] = objCurrent.m_dtmRECORDDATE.Date.ToString("yyyy-MM-dd");//�����ַ���
                            }
                            if (objCurrent.objSignerArr != null)
                            {
                                for (int m = 0; m < objCurrent.objSignerArr.Length; m++)
                                {
                                    objData[18] = objCurrent.objSignerArr[m].objEmployee.m_strLASTNAME_VCHR;
                                    objData[21] = "";
                                    objReturnData.Add(objData);
                                    objData = new object[23];
                                }
                            }
                            m_dtmPreRecordDate = objCurrent.m_dtmRECORDDATE;
                            #endregion
                            break;
                        }
                    }
                    #endregion
                    if (!blnMark && intCurrentDetail == arlDetail.Count)
                    {
                        #region ִ��ǩ������¼ǩ��
                        //ͬһ����ֻ�ڵ�һ����ʾ����
                        if (objCurrent.m_dtmRECORDDATE.Date.ToString("yyyyMMdd") != m_dtmPreRecordDate.Date.ToString("yyyyMMdd"))
                        {
                            objData[4] = objCurrent.m_dtmRECORDDATE.Date.ToString("yyyy-MM-dd");//�����ַ���
                        }
                        if (objCurrent.objSignerArr != null)
                        {
                            for (int m = 0; m < objCurrent.objSignerArr.Length; m++)
                            {
                                objData[18] = objCurrent.objSignerArr[m].objEmployee.m_strLASTNAME_VCHR;
                                objData[21] = "";
                                objReturnData.Add(objData);
                                objData = new object[23];
                            }
                        }
                        m_dtmPreRecordDate = objCurrent.m_dtmRECORDDATE;
                        #endregion
                    }
                }

                #region �������۲졢�����ʩ��Ч��δ��ʾ������������¼��ȫ����ʾ�꣬��������ʣ�ಿ��
                while (arlDetail != null && intCurrentDetail < arlDetail.Count && arlDetail.Count > 0)
                {
                    if (intRowOfCurrentDetail < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length)
                    {
                        for (int m = intRowOfCurrentDetail; m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length; m++)
                        {
                            object[] objOtherDetail = new object[23];
                            m_mthSetOtherDetail(((object[])arlDetail[intCurrentDetail]), intCurrentDetail, m, null, out objOtherDetail);
                            if (m == 0)
                            {
                                //ͬһ����ֻ�ڵ�һ����ʾ����
                                if (((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("yyyyMMdd") != m_dtmPreRecordDate.Date.ToString("yyyyMMdd"))
                                {
                                    objOtherDetail[4] = ((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("yyyy-MM-dd");
                                }
                                objOtherDetail[5] = ((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("HH:mm");
                            }
                            if ((clsEmrSigns_VO[])((object[])arlDetail[intCurrentDetail])[4] != null && m == ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1)
                            {
                                if (((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])) != null)
                                {
                                    for (int h = 0; h < ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])).Length; h++)
                                    {
                                        objOtherDetail[21] = ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4]))[h].objEmployee.m_strLASTNAME_VCHR;
                                        objReturnData.Add(objOtherDetail);
                                        objOtherDetail = new object[23];
                                    }
                                }
                                else
                                {
                                    objOtherDetail[21] = "";
                                    objReturnData.Add(objOtherDetail);
                                    objOtherDetail = new object[23];
                                }
                            }
                            else
                            {
                                objOtherDetail[21] = "";
                                objReturnData.Add(objOtherDetail);
                            }
                        }
                        m_dtmPreRecordDate = (DateTime)((object[])arlDetail[intCurrentDetail])[3];
                        intCurrentDetail++;
                        intRowOfCurrentDetail = 0;
                    }
                }
                object[][] m_objRe = new object[objReturnData.Count][];

                for (int m = 0; m < objReturnData.Count; m++)
                    m_objRe[m] = (object[])objReturnData[m];
                return m_objRe;
            }
                #endregion
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            #endregion
        }

        private void m_mthSetOtherDetail(object[] objDetail, int intCurrentDetail, int intRowOfCurrentDetail, clsGeneralNurseRecordContent_ObstetricNewChild objCurrent, out object[] objOtherDetail)
        {
            objOtherDetail = new object[23];
            string strText = ((string[])(objDetail[0]))[intRowOfCurrentDetail];
            string strXml = ((string[])(objDetail[1]))[intRowOfCurrentDetail];
            clsDSTRichTextBoxValue objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
            objclsDSTRichTextBoxValue.m_strText = strText;
            objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
            objOtherDetail[19] = objclsDSTRichTextBoxValue;
            objOtherDetail[20] = (DateTime)objDetail[3];
        }

        #region ��ӡ
        protected override void m_mthStartPrint()
        {
            try
            {
                if (m_pdcPrintDocument.DefaultPageSettings == null)
                {
                    m_pdcPrintDocument.DefaultPageSettings = new PageSettings();
                }
                m_pdcPrintDocument.DefaultPageSettings.Landscape = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("No Printers installed") >= 0)
                    clsPublicFunction.ShowInformationMessageBox("�Ҳ�����ӡ����");
                else MessageBox.Show(ex.Message);
            }

            base.m_mthStartPrint();

        }

        protected override infPrintRecord m_objGetPrintTool()
        {
            return new clsGeneralNurseRecord_ObstetricNewChildPrintTool();
        }
        #endregion

        private void m_dtgRecordDetail_MouseUp(object sender, MouseEventArgs e)
        {
            //Point pt = new Point(e.X, e.Y);
            //DataGrid.HitTestInfo hit = this.m_dtgRecordDetail.HitTest(pt);
            //if (hit.Type == DataGrid.HitTestType.Cell)
            //{
            //    this.m_dtgRecordDetail.SelectionBackColor = Color.Blue;
            //    this.m_dtgRecordDetail.Select(hit.Row);
            //}
        }

        private void m_dtgRecordDetail_CurrentCellChanged(object sender, EventArgs e)
        {
            this.m_dtgRecordDetail.Select(m_dtgRecordDetail.CurrentCell.RowNumber);
        }
    }
}
