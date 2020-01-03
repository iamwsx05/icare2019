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
//using com.digitalwave.clsGeneralNurseRecord_CSService;
using com.digitalwave.Emr.Signature_gui;
//using com.digitalwave.clsGeneralNurseRecordWK_CSService; 
namespace iCare
{
    /// <summary>
    /// ��Σ�ػ��߻����¼(��ɽ���)
    /// </summary>
    public partial class frmIntensiveTendMain_CSWK : iCare.frmRecordsBase
    {

        private System.Windows.Forms.DataGridTextBoxColumn clmRecordDateofDay;
        private System.Windows.Forms.DataGridTextBoxColumn clmCreateTime;
        private cltDataGridDSTRichTextBox m_dtcTemperature;
        private cltDataGridDSTRichTextBox m_dtcHeartRate;
        private cltDataGridDSTRichTextBox m_dtcRespiration;
        private cltDataGridDSTRichTextBox m_dtcBloodPress;
        private cltDataGridDSTRichTextBox m_dtcSPO2;
        private cltDataGridDSTRichTextBox m_dtcCVP;
        private cltDataGridDSTRichTextBox m_dtcMind;
        private cltDataGridDSTRichTextBox m_dtcPupilSizeLeft;
        private cltDataGridDSTRichTextBox m_dtcPupilSizeRight;
        private cltDataGridDSTRichTextBox m_dtcPupilReflectLeft;
        private cltDataGridDSTRichTextBox m_dtcPupilReflectRight;
        private cltDataGridDSTRichTextBox m_dtcInceptKind;
        private cltDataGridDSTRichTextBox m_dtcInceptMete;
        private cltDataGridDSTRichTextBox m_dtcEductionKind;
        private cltDataGridDSTRichTextBox m_dtcEductionMete;
        private cltDataGridDSTRichTextBox m_dtcEductionColor;
        private cltDataGridDSTRichTextBox m_dtcPiWen;
        private cltDataGridDSTRichTextBox m_dtcColor;
        private cltDataGridDSTRichTextBox m_dtcZhangLi;
        private cltDataGridDSTRichTextBox m_dtcCap;
        private cltDataGridDSTRichTextBox m_dtcCustom;
        private cltDataGridDSTRichTextBox m_dtcCustom2;
        private cltDataGridDSTRichTextBox m_dtcContent;
        private System.Windows.Forms.DataGridTextBoxColumn clmRecordSign;
        private string m_strCustomColumn = "";
        private string m_strCustomColumn2 = "";
        private DataTable dtTempTable;
        private string m_strTempColumnName = "";
        public frmIntensiveTendMain_CSWK()
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
        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>


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
            p_dtbRecordTable.Columns.Add("BloodPress", typeof(clsDSTRichTextBoxValue));//9
            p_dtbRecordTable.Columns.Add("SPO2", typeof(clsDSTRichTextBoxValue));//10
            p_dtbRecordTable.Columns.Add("CVP", typeof(clsDSTRichTextBoxValue));//11
            p_dtbRecordTable.Columns.Add("Mind", typeof(clsDSTRichTextBoxValue));//12
            p_dtbRecordTable.Columns.Add("Pupil_SizeLeft", typeof(clsDSTRichTextBoxValue));//13
            p_dtbRecordTable.Columns.Add("Pupil_SizeRight", typeof(clsDSTRichTextBoxValue));//14
            p_dtbRecordTable.Columns.Add("Pupil_ReflectLeft", typeof(clsDSTRichTextBoxValue));//15
            p_dtbRecordTable.Columns.Add("Pupil_ReflectRight", typeof(clsDSTRichTextBoxValue));//16
            p_dtbRecordTable.Columns.Add("Incept_Kind", typeof(clsDSTRichTextBoxValue));//17
            p_dtbRecordTable.Columns.Add("Incept_Mete", typeof(clsDSTRichTextBoxValue));//18
            p_dtbRecordTable.Columns.Add("Eduction_Kind", typeof(clsDSTRichTextBoxValue));//19
            p_dtbRecordTable.Columns.Add("Eduction_Mete", typeof(clsDSTRichTextBoxValue));//20
            p_dtbRecordTable.Columns.Add("Eduction_Color", typeof(clsDSTRichTextBoxValue));//21
            p_dtbRecordTable.Columns.Add("PiWen", typeof(clsDSTRichTextBoxValue));//22
            p_dtbRecordTable.Columns.Add("Color", typeof(clsDSTRichTextBoxValue));//23
            p_dtbRecordTable.Columns.Add("ZhangLi", typeof(clsDSTRichTextBoxValue));//24
            p_dtbRecordTable.Columns.Add("Cap", typeof(clsDSTRichTextBoxValue));//25
            p_dtbRecordTable.Columns.Add("Custom", typeof(clsDSTRichTextBoxValue));//26
            p_dtbRecordTable.Columns.Add("Custom2", typeof(clsDSTRichTextBoxValue));//27
            p_dtbRecordTable.Columns.Add("Content", typeof(clsDSTRichTextBoxValue));//28
            p_dtbRecordTable.Columns.Add("DetailRecordTime");//29
            p_dtbRecordTable.Columns.Add("RecordAndDetailSign");//30
            //��ż�¼������ID
            p_dtbRecordTable.Columns.Add("CreateUserID");//31

            m_dtcContent.m_RtbBase.m_BlnReadOnly = true;
            m_mthSetControl(clmRecordDateofDay);
            m_mthSetControl(clmCreateTime);
            m_mthSetControl(m_dtcTemperature);
            m_mthSetControl(m_dtcHeartRate);
            m_mthSetControl(m_dtcRespiration);
            m_mthSetControl(m_dtcBloodPress);
            m_mthSetControl(m_dtcSPO2);
            m_mthSetControl(m_dtcCVP);
            m_mthSetControl(m_dtcMind);
            m_mthSetControl(m_dtcPupilSizeLeft);
            m_mthSetControl(m_dtcPupilSizeRight);
            m_mthSetControl(m_dtcPupilReflectLeft);
            m_mthSetControl(m_dtcPupilReflectRight);
            m_mthSetControl(m_dtcInceptKind);
            m_mthSetControl(m_dtcInceptMete);
            m_mthSetControl(m_dtcEductionKind);
            m_mthSetControl(m_dtcEductionMete);
            m_mthSetControl(m_dtcEductionColor);
            m_mthSetControl(m_dtcPiWen);
            m_mthSetControl(m_dtcColor);
            m_mthSetControl(m_dtcZhangLi);
            m_mthSetControl(m_dtcCap);
            m_mthSetControl(m_dtcCustom);
            m_mthSetControl(m_dtcCustom2);
            m_mthSetControl(m_dtcContent);
            m_mthSetControl(clmRecordSign);
            //����������
            this.clmRecordDateofDay.HeaderText = "\r\n\r\n   ����";
            this.clmCreateTime.HeaderText = "\r\n\r\n  ʱ��";
            this.m_dtcTemperature.HeaderText = "  ��\r\n\r\n  ��\r\n\r\n  ��";
            this.m_dtcHeartRate.HeaderText = "  ��\r\n\r\n  ��\r\n\r\n ��/��";
            this.m_dtcRespiration.HeaderText = "  ��\r\n\r\n  ��\r\n\r\n ��/��";
            this.m_dtcBloodPress.HeaderText = "  Ѫ\r\n\r\n  ѹ\r\n\r\n mmHg";
            this.m_dtcSPO2.HeaderText = "  Sp\r\n\r\n  O2\r\n\r\n  %";
            this.m_dtcCVP.HeaderText = "  C\r\n  V\r\n  P\r\n\r\n CmH20";
            this.m_dtcMind.HeaderText = "  ��\r\n\r\n\r\n\r\n  ־";
            this.m_dtcPupilSizeLeft.HeaderText = "��ͫ��\r\n\r\n ��С\r\n\r\n  mm";
            this.m_dtcPupilSizeRight.HeaderText = "��ͫ��\r\n\r\n ��С\r\n\r\n  mm";
            this.m_dtcPupilReflectLeft.HeaderText = "��ͫ��\r\n\r\n\r\n ����";
            this.m_dtcPupilReflectRight.HeaderText = "��ͫ��\r\n\r\n\r\n ����";
            this.m_dtcInceptKind.HeaderText = " ����\r\n\r\n\r\n ����";
            this.m_dtcInceptMete.HeaderText = " ����\r\n\r\n\r\n  ��\r\n ml/g";
            this.m_dtcEductionKind.HeaderText = " �ų�\r\n\r\n\r\n ����";
            this.m_dtcEductionMete.HeaderText = " �ų�\r\n\r\n\r\n  ��\r\n ml/g";
            this.m_dtcEductionColor.HeaderText = " �ų�\r\n\r\n\r\n ��ɫ";
            this.m_dtcPiWen.HeaderText = "  Ƥ\r\n\r\n\r\n\r\n  ��";
            this.m_dtcColor.HeaderText = "  ��\r\n\r\n\r\n\r\n  ɫ";
            this.m_dtcZhangLi.HeaderText = "  ��\r\n\r\n\r\n\r\n  ��";
            this.m_dtcCap.HeaderText = "  CAP\r\n\r\n\r\n\r\n��Ӧ";
            this.m_dtcCustom.HeaderText = m_strCustomColumn;
            this.m_dtcCustom2.HeaderText = m_strCustomColumn2;
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
            return new clsRecordsDomain(enmRecordsType.IntensiveTendMain_CSWKRec);
        }

        // ��ȡ��¼����Ҫ��Ϣ�������ȡ����CreateDate,OpenDate,LastModifyDate��
        protected override clsTrackRecordContent m_objGetRecordMainContent(int p_intRecordType,
            object[] p_objDataArr)
        {
            //���� p_intRecordType ��ȡ��Ӧ�� clsTrackRecordContent
            clsTrackRecordContent objContent = null;
            switch ((enmDiseaseTrackType)p_intRecordType)
            {
                case enmDiseaseTrackType.IntensiveTendMain_CSWK:
                    objContent = new clsGeneralNurseRecordContent_CS();
                    break;
            }

            if (objContent == null)
                objContent = new clsGeneralNurseRecordContent_CS();

            if (m_objCurrentPatient != null)
                objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            else
            {
                clsPublicFunction.ShowInformationMessageBox("��ǰ����Ϊ��!");
                return null;
            }
            objContent.m_dtmInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
            objContent.m_dtmCreateDate = DateTime.Parse((string)p_objDataArr[0]);
            objContent.m_dtmOpenDate = DateTime.Parse((string)p_objDataArr[2]);
            objContent.m_dtmModifyDate = DateTime.Parse((string)p_objDataArr[3]);
            objContent.m_strCreateUserID = (string)p_objDataArr[31];

            return objContent;
        }

        private void frmIntensiveTendMain_CSWK_Load(object sender, System.EventArgs e)
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
            this.label1.Dock = DockStyle.Bottom;
        }

        private void m_mthSubFormClosed(object p_objSender, EventArgs p_objArg)
        {
            frmIntensiveTendMain_CSWKCon frmAddNewForm = (frmIntensiveTendMain_CSWKCon)p_objSender;
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
                frmIntensiveTendMain_CSWKCon frm = new frmIntensiveTendMain_CSWKCon(true, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), "2005");
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
                if (m_dtgRecordDetail.CurrentCell.ColumnNumber != 24)
                {
                    MessageBox.Show("��ѡ��һ������۲졢�����ʩ��Ч�������ݣ�");
                    return;
                }

                //���ݲ���
                int intSelectedRecordStartRow = m_dtgRecordDetail.CurrentCell.RowNumber;
                string strRecordTime = (m_dtbRecords.Rows[intSelectedRecordStartRow][29]).ToString();
                if (strRecordTime.Trim().Length == 0)
                    return;
                //�򿪴���
                frmIntensiveTendMain_CSWKCon frm = new frmIntensiveTendMain_CSWKCon(false, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), strRecordTime);
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
                if (m_dtgRecordDetail.CurrentCell.ColumnNumber != 24)
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

                string strDetailSign = (m_dtbRecords.Rows[intSelectedRecordStartRow][31]).ToString();

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


                string strRecordTime = (m_dtbRecords.Rows[intSelectedRecordStartRow][29]).ToString();
                if (strRecordTime.Trim().Length == 0)
                    return;
                //ȷ��
                if (MessageBox.Show("ȷ��Ҫɾ��ѡ�еĲ���۲졢�����ʩ��Ч�������ݣ�", "ɾ����ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;

                //�򿪴���
                //ɾ��

                //clsIntensiveTendMain_CSWKService objserv =
                //      (clsIntensiveTendMain_CSWKService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsIntensiveTendMain_CSWKService));

                string strDelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string strDelID = MDIParent.OperatorID;
                lngRes = (new weCare.Proxy.ProxyEmr05()).Service.clsIntensiveTendMain_CSWKService_m_lngDeleteDetail(m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), strRecordTime, strDelTime, strDelID);
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
                case enmDiseaseTrackType.IntensiveTendMain_CSWKRec:
                    return new frmIntensiveTendMain_CSWKRec();
                case enmDiseaseTrackType.IntensiveTendMain_CSWK:
                    return new frmIntensiveTendMain_CSWKRec();
                case enmDiseaseTrackType.IntensiveTendMain_CSWKCon:
                    return new frmIntensiveTendMain_CSWKCon(false, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), "");
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
            m_mthAddNewRecord((int)enmDiseaseTrackType.IntensiveTendMain_CSWK);
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
                bool blnIsSameClass = false;//�ж��Ƿ�Ϊͬһ���
                bool blnPreIsHide = false;//�ж���һ����¼�Ƿ�����
                int intCurrentSignIndex = 0;//��¼ǩ������
                bool blnMark = false;
                clsGeneralNurseRecordContent_CSDataInfo objGNRCInfo = new clsGeneralNurseRecordContent_CSDataInfo();
                clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
                string strText, strXml;

                objGNRCInfo = (clsGeneralNurseRecordContent_CSDataInfo)p_objTransDataInfo;

                if (objGNRCInfo.m_objRecordArr == null && objGNRCInfo.m_objDetailArr == null)
                    return null;

                #region �Բ���۲졢�����ʩ��Ч�����д���
                if (objGNRCInfo.m_objDetailArr != null)
                {
                    string strDetail = "";
                    string strDetailXML = "";
                    for (int n = 0; n < objGNRCInfo.m_objDetailArr.Length; n++)
                    {
                        clsGeneralNurseRecordContent_CSDetail objDetail = objGNRCInfo.m_objDetailArr[n];
                        object[] objTemp = new object[7];
                        strDetail = objDetail.m_strRECORDCONTENTAll;
                        strDetailXML = objDetail.m_strRECORDCONTENTXML;
                        string[] strDetailArr;
                        string[] strDetailXMLArr;
                        //�������¼��Ϊ�С�
                        com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strDetail, strDetailXML, 34, out strDetailArr, out strDetailXMLArr);

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

                clsGeneralNurseRecordContent_CS objCurrent;
                clsGeneralNurseRecordContent_CS objNext;
                for (int i = 0; i < intRecordCount; i++)
                {
                    blnMark = false;
                    objData = new object[32];
                    objCurrent = objGNRCInfo.m_objRecordArr[i];
                    objNext = new clsGeneralNurseRecordContent_CS();//��һ�������¼
                    if (i < intRecordCount - 1)
                        objNext = objGNRCInfo.m_objRecordArr[i + 1];
                    clsGeneralNurseRecordContent_CS objLast = null;
                    if (i > 0)
                        objLast = objGNRCInfo.m_objRecordArr[i - 1];
                    //����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ��޸����봴����Ϊͬһ�ˣ�����ʾ
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                     //   && objNext.m_strModifyUserID.Trim() == objCurrent.m_strCreateUserID.Trim()
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
                        objData[1] = (int)enmRecordsType.IntensiveTendMain_CSWK;//��ż�¼���͵�intֵ
                        objData[2] = objCurrent.m_dtmOpenDate;//��ż�¼��OpenDate�ַ���
                        objData[3] = objCurrent.m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   
                        objData[31] = objCurrent.m_strCreateUserID;//��ż�¼��createUserid�ַ���   

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
                    //Ѫѹ
                    strText = objCurrent.m_strBLOODPRESSURES_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strBLOODPRESSURES_RIGHT != objCurrent.m_strBLOODPRESSURES_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strBLOODPRESSURES_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strBLOODPRESSURES_RIGHT) && !blnPreIsHide)
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
                    objData[9] = objclsDSTRichTextBoxValue;//
                    //spo2
                    strText = objCurrent.m_strSPO2_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strSPO2_RIGHT != objCurrent.m_strSPO2_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strSPO2_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strSPO2_RIGHT) && !blnPreIsHide)
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
                    objData[10] = objclsDSTRichTextBoxValue;//
                    //cvp
                    strText = objCurrent.m_strCVP_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strCVP_RIGHT != objCurrent.m_strCVP_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strCVP_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strCVP_RIGHT) && !blnPreIsHide)
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
                    objData[11] = objclsDSTRichTextBoxValue;//
                    //��־
                    strText = objCurrent.m_strMind;
                    strXml = "<root />";
                    //if (objNext != null && objNext.m_strMind != objCurrent.m_strMind)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    //{
                    //    strXml = m_strGetDSTTextXML(objCurrent.m_strMind, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    //}
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[12] = objclsDSTRichTextBoxValue;
                    //ͫ�״�С��
                    strText = objCurrent.m_strPupilSizeLeft_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strPupilSizeLeft_RIGHT != objCurrent.m_strPupilSizeLeft_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strPupilSizeLeft_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strPupilSizeLeft_RIGHT) && !blnPreIsHide)
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
                    objData[13] = objclsDSTRichTextBoxValue;//
                    //ͫ�״�С��
                    strText = objCurrent.m_strPupilSizeRight_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strPupilSizeRight_RIGHT != objCurrent.m_strPupilSizeRight_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strPupilSizeRight_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strPupilSizeRight_RIGHT) && !blnPreIsHide)
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
                    objData[14] = objclsDSTRichTextBoxValue;//
                    //ͫ�׷�����
                    strText = objCurrent.m_strPupilReflectLeft;
                    strXml = "<root />";
                    //if (objNext != null && objNext.m_strPupilReflectLeft != objCurrent.m_strPupilReflectLeft)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    //{
                    //    strXml = m_strGetDSTTextXML(objCurrent.m_strPupilReflectLeft, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    //}
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[15] = objclsDSTRichTextBoxValue;
                    //ͫ�׷�����
                    strText = objCurrent.m_strPupilReflectRight;
                    strXml = "<root />";
                    //if (objNext != null && objNext.m_strPupilReflectRight != objCurrent.m_strPupilReflectRight)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    //{
                    //    strXml = m_strGetDSTTextXML(objCurrent.m_strPupilReflectRight, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    //}
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[16] = objclsDSTRichTextBoxValue;
                    
                    //Ƥ��
                    strText = objCurrent.m_strPiWen;
                    strXml = "<root />";
                    //if (objNext != null && objNext.m_strMind != objCurrent.m_strMind)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    //{
                    //    strXml = m_strGetDSTTextXML(objCurrent.m_strMind, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    //}
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[22] = objclsDSTRichTextBoxValue;
                    //��ɫ
                    strText = objCurrent.m_strColor;
                    strXml = "<root />";
                    //if (objNext != null && objNext.m_strMind != objCurrent.m_strMind)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    //{
                    //    strXml = m_strGetDSTTextXML(objCurrent.m_strMind, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    //}
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[23] = objclsDSTRichTextBoxValue;
                    //����
                    strText = objCurrent.m_strZhangLi;
                    strXml = "<root />";
                    //if (objNext != null && objNext.m_strMind != objCurrent.m_strMind)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    //{
                    //    strXml = m_strGetDSTTextXML(objCurrent.m_strMind, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    //}
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[24] = objclsDSTRichTextBoxValue;
                    //CAP��Ӧ
                    strText = objCurrent.m_strCap;
                    strXml = "<root />";
                    //if (objNext != null && objNext.m_strMind != objCurrent.m_strMind)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    //{
                    //    strXml = m_strGetDSTTextXML(objCurrent.m_strMind, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    //}
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[25] = objclsDSTRichTextBoxValue;
                    //�Զ�����
                    strText = objCurrent.m_strCUSTOM_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strCUSTOM_RIGHT != objCurrent.m_strCUSTOM_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strCUSTOM_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strCUSTOM_RIGHT) && !blnPreIsHide)
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
                    objData[26] = objclsDSTRichTextBoxValue;//

                    //�Զ�����2
                    strText = objCurrent.m_strCUSTOM2_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strCUSTOM2_RIGHT != objCurrent.m_strCUSTOM2_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strCUSTOM2_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strCUSTOM2_RIGHT) && !blnPreIsHide)
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
                    objData[27] = objclsDSTRichTextBoxValue;//

                    #endregion
                    #region ����۲졢�����ʩ��Ч��
                    for (; intCurrentDetail < arlDetail.Count; intCurrentDetail++)
                    {//ѭ��������в����¼
                        if ((DateTime)((object[])arlDetail[intCurrentDetail])[3] == objCurrent.m_dtmRECORDDATE)
                        {//����ǰ��¼�����벡��۲��¼������ͬ���������ǰ��¼�����������۲��¼
                            #region ִ��ǩ������¼ǩ��
                            int m_intMax = Math.Max(Math.Max(objCurrent.m_objEductionArr == null ? 0 : objCurrent.m_objEductionArr.Length, objCurrent.m_objInpectArr == null ? 0 : objCurrent.m_objInpectArr.Length), ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length + ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])).Length - 1);
                            for (int m = 0; m < m_intMax; m++)
                            {
                                if (objCurrent.m_objInpectArr != null && m < objCurrent.m_objInpectArr.Length)
                                {
                                    //��������
                                    strText = objCurrent.m_objInpectArr[m].m_strINPECT_KIND;
                                    strXml = "<root />";
                                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                    objclsDSTRichTextBoxValue.m_strText = strText;
                                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                    objData[17] = objclsDSTRichTextBoxValue;
                                    //������
                                    strText = objCurrent.m_objInpectArr[m].m_strINPECT_METE;
                                    strXml = "<root />";
                                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                    objclsDSTRichTextBoxValue.m_strText = strText;
                                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                    if (blnIsRed)
                                    {
                                        objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                                        blnIsRed = false;
                                    }
                                    objData[18] = objclsDSTRichTextBoxValue;//
                                }
                                if (objCurrent.m_objEductionArr != null && m < objCurrent.m_objEductionArr.Length)
                                {
                                    //�ų�����
                                    strText = objCurrent.m_objEductionArr[m].m_strEDUCTION_KIND;
                                    strXml = "<root />";
                                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                    objclsDSTRichTextBoxValue.m_strText = strText;
                                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                    objData[19] = objclsDSTRichTextBoxValue;
                                    //�ų���
                                    strText = objCurrent.m_objEductionArr[m].m_strEDUCTION_METE;
                                    strXml = "<root />";
                                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                    objclsDSTRichTextBoxValue.m_strText = strText;
                                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                    if (blnIsRed)
                                    {
                                        objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                                        blnIsRed = false;
                                    }
                                    objData[20] = objclsDSTRichTextBoxValue;
                                    //�ų���ɫ
                                    strText = objCurrent.m_objEductionArr[m].m_strEDUCTION_COLOR;
                                    strXml = "<root />";
                                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                    objclsDSTRichTextBoxValue.m_strText = strText;
                                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                    objData[21] = objclsDSTRichTextBoxValue;
                                }
                                if (m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length)
                                {
                                    strText = ((string[])(((object[])arlDetail[intCurrentDetail])[0]))[m];
                                    strXml = ((string[])(((object[])arlDetail[intCurrentDetail])[1]))[m];
                                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                    objclsDSTRichTextBoxValue.m_strText = strText;
                                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                    objData[28] = objclsDSTRichTextBoxValue;
                                    objData[29] = (DateTime)((object[])arlDetail[intCurrentDetail])[3];
                                }
                                if (m >= ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1 && m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length)
                                {
                                    objData[30] = ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4]))[intCurrentSignIndex++].objEmployee.m_strLASTNAME_VCHR;
                                }
                                else
                                {
                                    objData[30] = "";
                                }
                                objReturnData.Add(objData);
                                objData = new object[32];
                            }
                            m_dtmPreRecordDate = objCurrent.m_dtmRECORDDATE;
                            intCurrentSignIndex = 0;
                            intCurrentDetail++;
                            blnMark = true;
                            break;

                            #endregion
                            
                        }
                        else if (arlDetail != null && intCurrentDetail < arlDetail.Count && arlDetail.Count > 0 &&
                            (DateTime)(((object[])arlDetail[intCurrentDetail])[3]) < objCurrent.m_dtmRECORDDATE)
                        {
                            for (int j = intRowOfCurrentDetail; j < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length; j++)
                            {
                                object[] objOtherDetail = new object[32];
                                m_mthSetOtherDetail(((object[])arlDetail[intCurrentDetail]), intCurrentDetail, j, objCurrent, out objOtherDetail);
                                if (j == 0)
                                {
                                    //ͬһ����ֻ�ڵ�һ����ʾ����
                                    if (((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("yyyyMMdd") != m_dtmPreRecordDate.Date.ToString("yyyyMMdd"))
                                    {
                                        objOtherDetail[4] = ((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("yyyy-MM-dd");
                                    }
                                    objOtherDetail[5] = ((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("HH:mm");
                                }
                                if (j == ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1)
                                {
                                    if (((object[])arlDetail[intCurrentDetail])[4] != null)
                                    {
                                        if (((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])) != null)
                                        {
                                            for (int h = 0; h < ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])).Length; h++)
                                            {
                                                objOtherDetail[30] = ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4]))[h].objEmployee.m_strLASTNAME_VCHR;
                                                objReturnData.Add(objOtherDetail);
                                                objOtherDetail = new object[32];
                                            }
                                        }
                                        else
                                        {
                                            objOtherDetail[30] = "";
                                            objReturnData.Add(objOtherDetail);
                                            objOtherDetail = new object[32];
                                        }
                                        
                                    }
                                }
                                else
                                {
                                    objOtherDetail[30] = "";
                                    objReturnData.Add(objOtherDetail);
                                }
                            }
                            m_dtmPreRecordDate = (DateTime)(((object[])arlDetail[intCurrentDetail])[3]);
                            intRowOfCurrentDetail = 0;
                        }
                        else
                        {
                            //ͬһ����ֻ�ڵ�һ����ʾ����
                            if (objCurrent.m_dtmRECORDDATE.Date.ToString() == m_dtmPreRecordDate.Date.ToString())
                            {
                                objData[4] = null;//����ʾ���ڲ���
                            }
                            #region ִ��ǩ������¼ǩ��
                            int m_intMax = Math.Max(Math.Max(objCurrent.m_objEductionArr == null ? 0 : objCurrent.m_objEductionArr.Length, objCurrent.m_objInpectArr == null ? 0 : objCurrent.m_objInpectArr.Length), objCurrent.objSignerArr == null ? 0 : objCurrent.objSignerArr.Length);
                            for (int m = 0; m < m_intMax; m++)
                            {
                                if (objCurrent.m_objInpectArr != null && m < objCurrent.m_objInpectArr.Length)
                                {
                                    //��������
                                    strText = objCurrent.m_objInpectArr[m].m_strINPECT_KIND;
                                    strXml = "<root />";
                                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                    objclsDSTRichTextBoxValue.m_strText = strText;
                                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                    objData[17] = objclsDSTRichTextBoxValue;
                                    //������
                                    strText = objCurrent.m_objInpectArr[m].m_strINPECT_METE;
                                    strXml = "<root />";
                                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                    objclsDSTRichTextBoxValue.m_strText = strText;
                                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                    if (blnIsRed)
                                    {
                                        objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                                        blnIsRed = false;
                                    }
                                    objData[18] = objclsDSTRichTextBoxValue;//
                                }
                                if (objCurrent.m_objEductionArr != null && m < objCurrent.m_objEductionArr.Length)
                                {
                                    //�ų�����
                                    strText = objCurrent.m_objEductionArr[m].m_strEDUCTION_KIND;
                                    strXml = "<root />";
                                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                    objclsDSTRichTextBoxValue.m_strText = strText;
                                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                    objData[19] = objclsDSTRichTextBoxValue;
                                    //�ų���
                                    strText = objCurrent.m_objEductionArr[m].m_strEDUCTION_METE;
                                    strXml = "<root />";
                                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                    objclsDSTRichTextBoxValue.m_strText = strText;
                                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                    if (blnIsRed)
                                    {
                                        objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                                        blnIsRed = false;
                                    }
                                    objData[20] = objclsDSTRichTextBoxValue;
                                    //�ų���ɫ
                                    strText = objCurrent.m_objEductionArr[m].m_strEDUCTION_COLOR;
                                    strXml = "<root />";
                                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                    objclsDSTRichTextBoxValue.m_strText = strText;
                                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                    objData[21] = objclsDSTRichTextBoxValue;
                                }
                                int intSignCounts = Math.Max(objCurrent.m_objEductionArr == null ? 0 : objCurrent.m_objEductionArr.Length, objCurrent.m_objInpectArr == null ? 0 : objCurrent.m_objInpectArr.Length);
                                if (objCurrent.objSignerArr != null && m < (intSignCounts == 0 ? 1 : intSignCounts) && m == m_intMax - 1)
                                {
                                    strText = objCurrent.objSignerArr[0].objEmployee.m_strLASTNAME_VCHR;
                                    objData[30] = strText;
                                }
                                else objData[30] = "";
                                objReturnData.Add(objData);
                                objData = new object[32];
                            }
                            m_dtmPreRecordDate = objCurrent.m_dtmRECORDDATE;
                            #endregion
                            break;
                        }
                    }
                    #endregion
                    #region ���̼�¼����ʾ�꣬��ʾʣ�໤���¼
                    if (!blnMark && intCurrentDetail == arlDetail.Count)
                    {
                        #region ִ��ǩ������¼ǩ��
                        int m_intMax = Math.Max(Math.Max(objCurrent.m_objEductionArr == null ? 0 : objCurrent.m_objEductionArr.Length, objCurrent.m_objInpectArr == null ? 0 : objCurrent.m_objInpectArr.Length), objCurrent.objSignerArr == null ? 0 : objCurrent.objSignerArr.Length);
                        for (int m = 0; m < m_intMax; m++)
                        {
                            if (objCurrent.m_objInpectArr != null && m < objCurrent.m_objInpectArr.Length)
                            {
                                //��������
                                strText = objCurrent.m_objInpectArr[m].m_strINPECT_KIND;
                                strXml = "<root />";
                                objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                objclsDSTRichTextBoxValue.m_strText = strText;
                                objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                objData[17] = objclsDSTRichTextBoxValue;
                                //������
                                strText = objCurrent.m_objInpectArr[m].m_strINPECT_METE;
                                strXml = "<root />";
                                objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                objclsDSTRichTextBoxValue.m_strText = strText;
                                objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                if (blnIsRed)
                                {
                                    objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                                    blnIsRed = false;
                                }
                                objData[18] = objclsDSTRichTextBoxValue;//
                            }
                            if (objCurrent.m_objEductionArr != null && m < objCurrent.m_objEductionArr.Length)
                            {
                                //�ų�����
                                strText = objCurrent.m_objEductionArr[m].m_strEDUCTION_KIND;
                                strXml = "<root />";
                                objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                objclsDSTRichTextBoxValue.m_strText = strText;
                                objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                objData[19] = objclsDSTRichTextBoxValue;
                                //�ų���
                                strText = objCurrent.m_objEductionArr[m].m_strEDUCTION_METE;
                                strXml = "<root />";
                                objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                objclsDSTRichTextBoxValue.m_strText = strText;
                                objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                if (blnIsRed)
                                {
                                    objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                                    blnIsRed = false;
                                }
                                objData[20] = objclsDSTRichTextBoxValue;
                                //�ų���ɫ
                                strText = objCurrent.m_objEductionArr[m].m_strEDUCTION_COLOR;
                                strXml = "<root />";
                                objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                objclsDSTRichTextBoxValue.m_strText = strText;
                                objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                objData[21] = objclsDSTRichTextBoxValue;
                            }
                            int intSignCounts = Math.Max(objCurrent.m_objEductionArr == null ? 0 : objCurrent.m_objEductionArr.Length, objCurrent.m_objInpectArr == null ? 0 : objCurrent.m_objInpectArr.Length);
                            if (objCurrent.objSignerArr != null && m < (intSignCounts == 0 ? 1 : intSignCounts) && m == m_intMax - 1)
                            {
                                strText = objCurrent.objSignerArr[0].objEmployee.m_strLASTNAME_VCHR;
                                objData[30] = strText;
                            }
                            else objData[30] = "";
                            objReturnData.Add(objData);
                            objData = new object[32];
                        }
                        m_dtmPreRecordDate = objCurrent.m_dtmRECORDDATE;
                        #endregion
                    }
                    #endregion
                }

                #region �������۲졢�����ʩ��Ч��δ��ʾ������������¼��ȫ����ʾ�꣬��������ʣ�ಿ��
                while (arlDetail != null && intCurrentDetail < arlDetail.Count && arlDetail.Count > 0)
                {
                    if (intRowOfCurrentDetail < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length)
                    {
                        for (int m = intRowOfCurrentDetail; m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length; m++)
                        {
                            object[] objOtherDetail = new object[33];
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
                            if (m == ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1)
                            {
                                if (((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])) != null)
                                {
                                    for (int h = 0; h < ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])).Length; h++)
                                    {
                                        objOtherDetail[30] = ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4]))[h].objEmployee.m_strLASTNAME_VCHR;
                                        objReturnData.Add(objOtherDetail);
                                        objOtherDetail = new object[32];
                                    }
                                }
                                else
                                {
                                    objOtherDetail[30] = "";
                                    objReturnData.Add(objOtherDetail);
                                    objOtherDetail = new object[32];
                                }
                                
                            }
                            else
                            {
                                objOtherDetail[30] = "";
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

        private void m_mthSetOtherDetail(object[] objDetail, int intCurrentDetail, int intRowOfCurrentDetail, clsGeneralNurseRecordContent_CS objCurrent, out object[] objOtherDetail)
        {
            objOtherDetail = new object[32];
            string strText = ((string[])(objDetail[0]))[intRowOfCurrentDetail];
            string strXml = ((string[])(objDetail[1]))[intRowOfCurrentDetail];
            clsDSTRichTextBoxValue objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
            objclsDSTRichTextBoxValue.m_strText = strText;
            objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
            objOtherDetail[28] = objclsDSTRichTextBoxValue;
            objOtherDetail[29] = (DateTime)objDetail[3];
        }

        #region �Զ����б�ͷ����
        private void m_dtgRecordDetail_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (MDIParent.m_objCurrentPatient == null)
                return;
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                System.Windows.Forms.DataGrid.HitTestInfo myHitTest = m_dtgRecordDetail.HitTest(e.X, e.Y);
                if (myHitTest.Column == 22 || myHitTest.Column == 23)
                {
                    m_strTempColumnName = "";
                    m_mthSetCustomColumn(myHitTest.Column);
                }

            }
        }

        private void m_mthSetCustomColumn(int p_intColumn)
        {
            string strHeaderText = m_dtgRecordDetail.TableStyles[0].GridColumnStyles[p_intColumn].HeaderText.Replace("\r\n", "");
            frmSetCustomDataGridColumn frm = new frmSetCustomDataGridColumn(strHeaderText);
            m_strTempColumnName = "";
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                m_strTempColumnName = frm.m_StrSetName;
            }
            else
                return;
            switch (p_intColumn)
            {
                case 22:
                    m_mthSaveColumnNameToDB("CUSTOMNAME", ref m_strCustomColumn);
                    m_dtcCustom.HeaderText = m_strCustomColumn;
                    break;
                case 23:
                    m_mthSaveColumnNameToDB("CUSTOM2NAME", ref m_strCustomColumn2);
                    m_dtcCustom2.HeaderText = m_strCustomColumn2;
                    break;
            }

            //if (p_intColumn == 21)
            //{
            //    m_mthSaveColumnNameToDB("CUSTOMNAME", ref m_strCustomColumn);
            //    m_dtcCustom.HeaderText = m_strCustomColumn;

            //}
        }

        private void m_mthSaveColumnNameToDB(string p_strColumnIndex, ref string p_strColumnName)
        {
            p_strColumnName = "";
            long lngRes = 0;

            //clsIntensiveTendMain_CSWKService objServ =
            //      (clsIntensiveTendMain_CSWKService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsIntensiveTendMain_CSWKService));

            if (m_strTempColumnName != "")
            {
                int intColumnNameLength = m_strTempColumnName.Length;
                if (intColumnNameLength > 3)//�ַ�����3����������ʾ��ÿ��3���ַ�
                {
                    while (m_strTempColumnName.Substring(3).Length > 3)
                    {
                        p_strColumnName += m_strTempColumnName.Substring(0, 3) + "\r\n";
                        m_strTempColumnName = m_strTempColumnName.Substring(3);
                    }
                    p_strColumnName += m_strTempColumnName;

                }
                else
                {
                    for (int i = 0; i < intColumnNameLength; i++)
                    {
                        p_strColumnName += "\r\n" + m_strTempColumnName[i].ToString();
                    }
                }
                //objServ.Dispose();
            }
            lngRes = (new weCare.Proxy.ProxyEmr05()).Service.clsIntensiveTendMain_CSWKService_m_lngSetCustomColumnName(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), p_strColumnIndex, p_strColumnName);
        }

        /// <summary>
        /// �����Զ�����ͷ
        /// </summary>
        private void m_mthSetCustomColumnName()
        {
            m_dtcCustom.HeaderText = m_strCustomColumn;
            m_dtcCustom2.HeaderText = m_strCustomColumn2;
        }

        #region ��дm_trvInPatientDate_AfterSelect�Ա�ѡ��һ����Ժʱ��ʱLoad���Զ�����ͷ
        protected override void m_trvInPatientDate_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            try
            {
                //��ղ��˼�¼��Ϣ				
                m_mthClearPatientRecordInfo();

                if (m_trvInPatientDate.SelectedNode == null || m_trvInPatientDate.SelectedNode == m_trvInPatientDate.Nodes[0] || m_objCurrentPatient == null)
                {
                    return;
                }

                //���ò��˵���סԺ�Ļ�����Ϣ
                m_mthOnlySetPatientInfo(m_objCurrentPatient);
                m_objCurrentPatient.m_ObjPeopleInfo = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_ObjPeopleInfo;

                string m_strInPatientID = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_StrEMRInPatientID;
                string m_strInPatientDate = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_DtmEMRInDate.ToString("yyyy-MM-dd HH:mm:ss");

                txtInPatientID.Text = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_StrHISInPatientID;
                m_objCurrentPatient.m_DtmSelectedInDate = DateTime.Parse(m_strInPatientDate);
                m_objCurrentPatient.m_StrHISInPatientID = txtInPatientID.Text;
                m_objCurrentPatient.m_DtmSelectedHISInDate = Convert.ToDateTime(m_trvInPatientDate.SelectedNode.Text);

                #region ��ȡ���˵�����Ժ�ǼǺ�
                string strRegisterID = "";

                //com.digitalwave.PatientManagerService.clsPatientManagerService objServ =
                //    (com.digitalwave.PatientManagerService.clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PatientManagerService.clsPatientManagerService));

                long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRegisterIDByPatient(m_objCurrentPatient.m_StrPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), out strRegisterID);
                if (!string.IsNullOrEmpty(strRegisterID))
                {
                    com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR = strRegisterID;
                    m_objCurrentPatient.m_StrRegisterId = strRegisterID;
                    m_objBaseCurrentPatient.m_StrRegisterId = strRegisterID;
                }
                #endregion

                m_mthIsReadOnly();
                if (!m_blnCanShowRecordContent())
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                //��ȡ���˼�¼�б�
                clsTransDataInfo[] objTansDataInfoArr;
                lngRes = m_objRecordsDomain.m_lngGetTransDataInfoArr(m_strInPatientID, m_strInPatientDate, out objTansDataInfoArr);

                if (lngRes <= 0 || objTansDataInfoArr == null)
                {
                    return;
                }

                //����¼ʱ��(CreateDate)����
                m_mthSortTransData(ref objTansDataInfoArr);

                DataTable dtbAddBlank;
                clsDiseaseTrackAddBlankDomain objAddBlankDomain = new clsDiseaseTrackAddBlankDomain();
                objAddBlankDomain.m_lngGetBlankRecordContent(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate, out dtbAddBlank);

                //��Ӽ�¼����DataTable
                object[][] objDataArr;
                for (int i1 = 0; i1 < objTansDataInfoArr.Length; i1++)
                {
                    if (dtbAddBlank != null && dtbAddBlank.Rows.Count > 0)
                    {
                        //���Ҽ�¼֮ǰ�з���м�¼,�в������
                        foreach (DataRow drtAdd in dtbAddBlank.Rows)
                        {
                            if (objTansDataInfoArr[i1].m_objRecordContent != null)
                            {
                                if (objTansDataInfoArr[i1].m_objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse(drtAdd["opendate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"))
                                {
                                    object[] objBlank = new object[5];
                                    objBlank[1] = 100;
                                    objBlank[2] = drtAdd[2].ToString();
                                    m_dtbRecords.Rows.Add(objBlank);
                                    for (int k3 = 0; k3 < (Int32.Parse(drtAdd[3].ToString()) - 1); k3++)
                                    {
                                        m_dtbRecords.Rows.Add(new object[] { });
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    m_dtcCustom.HeaderText = "";
                    m_dtcCustom2.HeaderText = "";
                    if (i1 == 0)
                    {
                        clsGeneralNurseRecordContent_CSDataInfo objITRCInfo = (clsGeneralNurseRecordContent_CSDataInfo)(objTansDataInfoArr[0]);
                        if (objITRCInfo != null && objITRCInfo.m_objRecordArr != null)
                        {
                            clsGeneralNurseRecordContent_CS objCurrent = objITRCInfo.m_objRecordArr[0];
                            if (objCurrent != null)
                            {
                                m_strCustomColumn = objCurrent.m_strCUSTOMNAME == null ? "" : objCurrent.m_strCUSTOMNAME;
                                m_strCustomColumn2 = objCurrent.m_strCUSTOM2NAME == null ? "" : objCurrent.m_strCUSTOM2NAME;
                                m_mthSetCustomColumnName();
                            }
                        }
                    }

                    objDataArr = m_objGetRecordsValueArr(objTansDataInfoArr[i1]);

                    if (objDataArr == null)
                        continue;
                    m_dtbRecords.BeginLoadData();
                    for (int j2 = 0; j2 < objDataArr.Length; j2++)
                    {
                        //m_dtbRecords.Rows.Add(objDataArr[j2] );	
                        m_dtbRecords.LoadDataRow(objDataArr[j2], true);
                    }
                    m_dtbRecords.EndLoadData();
                    m_dtgRecordDetail.EnsureVisible(m_dtbRecords.Rows.Count - 1);
                }

                if (m_dtbRecords.Rows.Count == 0 && !m_blnIfNewDeletedRecord)
                {
                    m_mthAutoAddNewRecord();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        #endregion

        protected override void m_mthPerformSessionChanged(clsEmrPatientSessionInfo_VO p_objSelectedSession, int p_intIndex)
        {
            if (p_objSelectedSession == null) return;
            try
            {
                if (m_dtgRecordDetail == null) return;
                //��ղ��˼�¼��Ϣ				
                m_mthClearPatientRecordInfo();

                if (p_objSelectedSession == null || m_objCurrentPatient == null)
                {
                    return;
                }

                //���ò��˵���סԺ�Ļ�����Ϣ
                m_mthOnlySetPatientInfo(m_objCurrentPatient);
                m_objCurrentPatient.m_DtmSelectedInDate = p_objSelectedSession.m_dtmEMRInpatientDate;
                m_objCurrentPatient.m_StrHISInPatientID = p_objSelectedSession.m_strHISInpatientId;
                m_objCurrentPatient.m_DtmSelectedHISInDate = p_objSelectedSession.m_dtmHISInpatientDate;

                m_objCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;
                m_objBaseCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;


                m_mthIsReadOnly();
                if (!m_blnCanShowRecordContent())
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                //��ȡ���˼�¼�б�
                clsTransDataInfo[] objTansDataInfoArr;

                m_mthGetTransDataInfoArr(out objTansDataInfoArr);

                if (objTansDataInfoArr == null)
                {
                    return;
                }

                //����¼ʱ��(CreateDate)����
                m_mthSortTransData(ref objTansDataInfoArr);

                DataTable dtbAddBlank;
                clsDiseaseTrackAddBlankDomain objAddBlankDomain = new clsDiseaseTrackAddBlankDomain();
                objAddBlankDomain.m_lngGetBlankRecordContent(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate, out dtbAddBlank);

                //��Ӽ�¼����DataTable
                object[][] objDataArr;
                for (int i1 = 0; i1 < objTansDataInfoArr.Length; i1++)
                {
                    if (dtbAddBlank != null && dtbAddBlank.Rows.Count > 0)
                    {
                        //���Ҽ�¼֮ǰ�з���м�¼,�в������
                        foreach (DataRow drtAdd in dtbAddBlank.Rows)
                        {
                            if (objTansDataInfoArr[i1].m_objRecordContent != null)
                            {
                                if (objTansDataInfoArr[i1].m_objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse(drtAdd["opendate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"))
                                {
                                    object[] objBlank = new object[5];
                                    objBlank[1] = 100;
                                    objBlank[2] = drtAdd[2].ToString();
                                    m_dtbRecords.Rows.Add(objBlank);
                                    for (int k3 = 0; k3 < (Int32.Parse(drtAdd[3].ToString()) - 1); k3++)
                                    {
                                        m_dtbRecords.Rows.Add(new object[] { });
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    m_dtcCustom.HeaderText = "";
                    m_dtcCustom2.HeaderText = "";

                    if (i1 == 0)
                    {
                        clsGeneralNurseRecordContent_CSDataInfo objITRCInfo = (clsGeneralNurseRecordContent_CSDataInfo)(objTansDataInfoArr[0]);
                        if (objITRCInfo != null && objITRCInfo.m_objRecordArr != null)
                        {
                            clsGeneralNurseRecordContent_CS objCurrent = objITRCInfo.m_objRecordArr[0];
                            if (objCurrent != null)
                            {
                                m_strCustomColumn = objCurrent.m_strCUSTOMNAME == null ? "" : objCurrent.m_strCUSTOMNAME;
                                m_strCustomColumn2 = objCurrent.m_strCUSTOM2NAME == null ? "" : objCurrent.m_strCUSTOM2NAME;
                                m_mthSetCustomColumnName();
                            }
                        }
                    }

                    objDataArr = m_objGetRecordsValueArr(objTansDataInfoArr[i1]);

                    if (objDataArr == null)
                        continue;
                    m_dtbRecords.BeginLoadData();
                    for (int j2 = 0; j2 < objDataArr.Length; j2++)
                    {
                        //m_dtbRecords.Rows.Add(objDataArr[j2] );	
                        m_dtbRecords.LoadDataRow(objDataArr[j2], true);
                    }
                    m_dtbRecords.EndLoadData();

                }
                m_dtgRecordDetail.EnsureVisible(m_dtbRecords.Rows.Count - 1);
                if (m_dtbRecords.Rows.Count == 0 && !m_blnIfNewDeletedRecord)
                {
                    m_mthAutoAddNewRecord();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        #endregion
        #region ��ӡ
        protected override void m_mthStartPrint()
        {
            //PageSetupDialog psd = new PageSetupDialog();
            //ȱʡʹ�ô�ӡԤ�����Ӵ��������ṩ�µ�ʵ��
            try
            {
                if (m_pdcPrintDocument.DefaultPageSettings == null)
                {
                    m_pdcPrintDocument.DefaultPageSettings = new PageSettings();
                }
                m_pdcPrintDocument.DefaultPageSettings.Landscape = true;
                //m_pdcPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 840, 1240);
                //if (m_blnDirectPrint)
                //{
                //    m_pdcPrintDocument.Print();
                //}
                //else
                //{
                //psd.PageSettings = m_pdcPrintDocument.DefaultPageSettings;
                //if (psd.ShowDialog() != DialogResult.Cancel)
                //{
                //    m_pdcPrintDocument.DefaultPageSettings.Landscape = psd.PageSettings.Landscape;
                //    m_pdcPrintDocument.DefaultPageSettings.PaperSize = psd.PageSettings.PaperSize;
                //    //((clsGeneralNurseRecord_CSPrintTool)objPrintTool).m_mthPrintPage(m_pdcPrintDocument.DefaultPageSettings);
                //}
                //else
                //{
                //    return;
                //}
                //}
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
            //			return new clsIntensiveTendMainPrintTool();
            string m_strCustomColumnName = m_strCustomColumn;
            string m_strCustomColumnName2 = m_strCustomColumn2;
            return new clsIntensiveTendMain_CSWKPrintTool(m_strCustomColumnName, m_strCustomColumnName2);
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
