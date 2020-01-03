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
    /// frmGeneralNurseRecord_GX ��ժҪ˵����
    /// </summary>
    public class frmGeneralNurseRecord_GX : iCare.frmRecordsBase
    {
        #region 
        private System.Windows.Forms.Panel panel1;
        private DataTable dtTempTable;
        private string m_strCurrentOpenDate = "";
        private string m_strCreateUserID = "";
        private System.Windows.Forms.DataGridTextBoxColumn clmRecordDateofDay;
        private System.Windows.Forms.DataGridTextBoxColumn clmCreateTime;
        private cltDataGridDSTRichTextBox m_dtcTemperature;
        private cltDataGridDSTRichTextBox m_dtcPulse;
        private cltDataGridDSTRichTextBox m_dtcRespiration;
        private cltDataGridDSTRichTextBox m_dtcBloodPressureA;
        private cltDataGridDSTRichTextBox m_dtcBloodPressureS;
        private System.Windows.Forms.DataGridTextBoxColumn clmSign;
        private cltDataGridDSTRichTextBox m_dtcContent;
        private cltDataGridDSTRichTextBox m_dtcHeartRate;
        private cltDataGridDSTRichTextBox m_dtcCustom1;
        private cltDataGridDSTRichTextBox m_dtcCustom2;
        private string m_strCustomColumn1 = "";
        private string m_strCustomColumn2 = "";
        private string m_strTempColumnName = "";
        #endregion
        /// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

        public frmGeneralNurseRecord_GX()
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
            dtTempTable = new DataTable("RecordDetail");
        }

        /// <summary>
        /// ������������ʹ�õ���Դ��
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

        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.clmRecordDateofDay = new System.Windows.Forms.DataGridTextBoxColumn();
            this.clmCreateTime = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcTemperature = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPulse = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcRespiration = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBloodPressureA = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBloodPressureS = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.clmSign = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcContent = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcHeartRate = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcCustom1 = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcCustom2 = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).BeginInit();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgtsStyles
            // 
            this.dgtsStyles.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
                                                                                                         this.clmRecordDateofDay,
                                                                                                         this.clmCreateTime,
                                                                                                         this.m_dtcTemperature,
                                                                                                         this.m_dtcPulse,
                                                                                                         this.m_dtcRespiration,
                                                                                                         this.m_dtcHeartRate,
                                                                                                         this.m_dtcBloodPressureA,
                                                                                                         this.m_dtcBloodPressureS,
                                                                                                         this.m_dtcCustom1,
                                                                                                         this.m_dtcCustom2,
                                                                                                         this.clmSign,
                                                                                                         this.m_dtcContent});
            this.dgtsStyles.RowHeaderWidth = 15;
            // 
            // m_dtgRecordDetail
            // 
            this.m_dtgRecordDetail.AccessibleName = "DataGrid";
            this.m_dtgRecordDetail.AccessibleRole = System.Windows.Forms.AccessibleRole.Table;
            this.m_dtgRecordDetail.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
            this.m_dtgRecordDetail.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_dtgRecordDetail.DataSource = this.m_dtbRecords;
            this.m_dtgRecordDetail.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.m_dtgRecordDetail.HeaderForeColor = System.Drawing.SystemColors.Window;
            this.m_dtgRecordDetail.Location = new System.Drawing.Point(15, 78);
            this.m_dtgRecordDetail.Size = new System.Drawing.Size(786, 565);
            this.m_dtgRecordDetail.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_dtgRecordDetail_MouseDown);
            // 
            // mniAppend
            // 
            this.mniAppend.Click += new System.EventHandler(this.mniAppend_Click);
            // 
            // m_trvInPatientDate
            // 
            this.m_trvInPatientDate.BackColor = System.Drawing.Color.White;
            this.m_trvInPatientDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_trvInPatientDate.Font = new System.Drawing.Font("����", 10.5F);
            this.m_trvInPatientDate.ForeColor = System.Drawing.Color.Black;
            this.m_trvInPatientDate.LineColor = System.Drawing.Color.Black;
            this.m_trvInPatientDate.Location = new System.Drawing.Point(-25, 139);
            this.m_trvInPatientDate.Size = new System.Drawing.Size(176, 60);
            this.m_trvInPatientDate.Visible = false;
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(522, 74);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(379, 64);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(-17, 121);
            this.lblBedNoTitle.Size = new System.Drawing.Size(56, 14);
            this.lblBedNoTitle.Text = "��  ��:";
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(-17, 136);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(-17, 118);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(474, 71);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(528, 64);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(-3, 122);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(-43, 88);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(-25, 118);
            this.txtInPatientID.Size = new System.Drawing.Size(88, 23);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(382, 60);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(-25, 118);
            this.m_txtBedNO.Size = new System.Drawing.Size(64, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(-25, 127);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(-25, 96);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(-25, 111);
            this.m_lsvBedNO.Size = new System.Drawing.Size(64, 104);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(-53, 156);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(-3, 127);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(600, 72);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Location = new System.Drawing.Point(-6, 129);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(80, 0);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(-1, 113);
            this.m_lblForTitle.Visible = false;
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(428, 61);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(662, 37);
            this.m_cmdModifyPatientInfo.Size = new System.Drawing.Size(69, 28);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Size = new System.Drawing.Size(796, 60);
            this.m_pnlNewBase.Visible = true;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(794, 29);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(9, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(797, 576);
            this.panel1.TabIndex = 10000004;
            // 
            // clmRecordDateofDay
            // 
            this.clmRecordDateofDay.Format = "";
            this.clmRecordDateofDay.FormatInfo = null;
            this.clmRecordDateofDay.MappingName = "RecordDateofDay";
            this.clmRecordDateofDay.Width = 80;
            // 
            // clmCreateTime
            // 
            this.clmCreateTime.Format = "";
            this.clmCreateTime.FormatInfo = null;
            this.clmCreateTime.MappingName = "RecordTime";
            this.clmCreateTime.Width = 60;
            // 
            // m_dtcTemperature
            // 
            this.m_dtcTemperature.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcTemperature.m_BlnGobleSet = true;
            this.m_dtcTemperature.m_BlnUnderLineDST = false;
            this.m_dtcTemperature.MappingName = "Temperature";
            this.m_dtcTemperature.Width = 50;
            // 
            // m_dtcPulse
            // 
            this.m_dtcPulse.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPulse.m_BlnGobleSet = true;
            this.m_dtcPulse.m_BlnUnderLineDST = false;
            this.m_dtcPulse.MappingName = "Pulse";
            this.m_dtcPulse.Width = 50;
            // 
            // m_dtcRespiration
            // 
            this.m_dtcRespiration.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcRespiration.m_BlnGobleSet = true;
            this.m_dtcRespiration.m_BlnUnderLineDST = false;
            this.m_dtcRespiration.MappingName = "Respiration";
            this.m_dtcRespiration.Width = 50;
            // 
            // m_dtcBloodPressureA
            // 
            this.m_dtcBloodPressureA.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBloodPressureA.m_BlnGobleSet = true;
            this.m_dtcBloodPressureA.m_BlnUnderLineDST = false;
            this.m_dtcBloodPressureA.MappingName = "BloodPressureA";
            this.m_dtcBloodPressureA.Width = 50;
            // 
            // m_dtcBloodPressureS
            // 
            this.m_dtcBloodPressureS.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBloodPressureS.m_BlnGobleSet = true;
            this.m_dtcBloodPressureS.m_BlnUnderLineDST = false;
            this.m_dtcBloodPressureS.MappingName = "BloodPressureS";
            this.m_dtcBloodPressureS.Width = 50;
            // 
            // clmSign
            // 
            this.clmSign.Format = "";
            this.clmSign.FormatInfo = null;
            this.clmSign.MappingName = "Sign";
            this.clmSign.Width = 80;
            // 
            // m_dtcContent
            // 
            this.m_dtcContent.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcContent.m_BlnGobleSet = true;
            this.m_dtcContent.m_BlnUnderLineDST = false;
            this.m_dtcContent.MappingName = "Content";
            this.m_dtcContent.Width = 270;
            // 
            // m_dtcHeartRate
            // 
            this.m_dtcHeartRate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcHeartRate.m_BlnGobleSet = true;
            this.m_dtcHeartRate.m_BlnUnderLineDST = false;
            this.m_dtcHeartRate.MappingName = "HeartRate";
            this.m_dtcHeartRate.Width = 50;
            // 
            // m_dtcCustom1
            // 
            this.m_dtcCustom1.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcCustom1.m_BlnGobleSet = true;
            this.m_dtcCustom1.m_BlnUnderLineDST = false;
            this.m_dtcCustom1.MappingName = "Custom1";
            this.m_dtcCustom1.Width = 50;
            // 
            // m_dtcCustom2
            // 
            this.m_dtcCustom2.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcCustom2.m_BlnGobleSet = true;
            this.m_dtcCustom2.m_BlnUnderLineDST = false;
            this.m_dtcCustom2.MappingName = "Custom2";
            this.m_dtcCustom2.Width = 50;
            // 
            // frmGeneralNurseRecord_GX
            // 
            this.ClientSize = new System.Drawing.Size(829, 673);
            this.Controls.Add(this.panel1);
            this.Name = "frmGeneralNurseRecord_GX";
            this.Text = "һ�㻼�߻����¼";
            this.Load += new System.EventHandler(this.frmGeneralNurseRecord_GX_Load);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.m_trvInPatientDate, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.m_dtgRecordDetail, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).EndInit();
            this.m_pnlNewBase.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

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
            p_dtbRecordTable.Columns.Add("Pulse", typeof(clsDSTRichTextBoxValue));//7
            p_dtbRecordTable.Columns.Add("Respiration", typeof(clsDSTRichTextBoxValue));//8
            p_dtbRecordTable.Columns.Add("HeartRate", typeof(clsDSTRichTextBoxValue));
            p_dtbRecordTable.Columns.Add("BloodPressureS", typeof(clsDSTRichTextBoxValue));//9
            p_dtbRecordTable.Columns.Add("BloodPressureA", typeof(clsDSTRichTextBoxValue));//9
            p_dtbRecordTable.Columns.Add("Custom1", typeof(clsDSTRichTextBoxValue));//9
            p_dtbRecordTable.Columns.Add("Custom2", typeof(clsDSTRichTextBoxValue));//9
            DataColumn dc3 = p_dtbRecordTable.Columns.Add("Sign");//10	
            dc3.DefaultValue = "";
            p_dtbRecordTable.Columns.Add("Content", typeof(clsDSTRichTextBoxValue));//11
            p_dtbRecordTable.Columns.Add("DetailRecordTime");
            p_dtbRecordTable.Columns.Add("RecordAndDetailSign");
            //��ż�¼������ID
            p_dtbRecordTable.Columns.Add("CreateUserID");

            m_dtcContent.m_RtbBase.m_BlnReadOnly = true;
            m_mthSetControl(clmRecordDateofDay);
            m_mthSetControl(clmCreateTime);
            m_mthSetControl(m_dtcTemperature);
            m_mthSetControl(m_dtcPulse);
            m_mthSetControl(m_dtcRespiration);
            m_mthSetControl(m_dtcHeartRate);
            m_mthSetControl(m_dtcBloodPressureA);
            m_mthSetControl(m_dtcBloodPressureS);
            m_mthSetControl(m_dtcCustom1);
            m_mthSetControl(m_dtcCustom2);
            m_mthSetControl(clmSign);
            m_mthSetControl(m_dtcContent);
            //����������
            this.clmRecordDateofDay.HeaderText = "\r\n\r\n   ����";
            this.clmCreateTime.HeaderText = "\r\n\r\n  ʱ��";
            this.m_dtcTemperature.HeaderText = "  ��\r\n\r\n  ��\r\n\r\n  ��";
            this.m_dtcPulse.HeaderText = "  ��\r\n\r\n  ��\r\n\r\n��/��";
            this.m_dtcRespiration.HeaderText = "  ��\r\n\r\n  ��\r\n\r\n��/��";
            this.m_dtcHeartRate.HeaderText = "  ��\r\n\r\n  ��\r\n\r\n��/��";
            this.m_dtcBloodPressureA.HeaderText = "  ��\r\n  ��\r\n  ѹ\r\n\r\n mmHg";
            this.m_dtcBloodPressureS.HeaderText = "  ��\r\n  ��\r\n  ѹ\r\n\r\n mmHg";
            this.m_dtcCustom1.HeaderText = m_strCustomColumn1;
            this.m_dtcCustom2.HeaderText = m_strCustomColumn2;
            this.clmSign.HeaderText = "\r\n\r\n   ǩ��";
            this.m_dtcContent.HeaderText = "\r\n\r\n             �����¼";

        }

        #region ����
        /// <summary>
        /// ��ǰ��Ժʱ��
        /// </summary>
        protected override string m_StrCurrentOpenDate
        {
            get
            {
                if (m_strCurrentOpenDate == "")
                {
                    clsPublicFunction.ShowInformationMessageBox("����ѡ���¼");
                    return "";
                }
                return m_strCurrentOpenDate;
            }
        }

        protected override enmApproveType m_EnmAppType
        {
            get { return enmApproveType.Nurses; }
        }
        /// <summary>
        /// ��¼��ID?
        /// </summary>
        protected override string m_StrRecorder_ID
        {
            get
            {
                return m_strCreateUserID;
            }
        }
        #endregion ����

        //���ó�ʼ�ıȽ�����
        private DateTime m_dtmPreRecordDate;
        // ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
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
            return new clsRecordsDomain(enmRecordsType.GeneralNurseRecord_GXRec);
        }

        // ��ȡ��¼����Ҫ��Ϣ�������ȡ����CreateDate,OpenDate,LastModifyDate��
        protected override clsTrackRecordContent m_objGetRecordMainContent(int p_intRecordType,
            object[] p_objDataArr)
        {
            //���� p_intRecordType ��ȡ��Ӧ�� clsTrackRecordContent
            clsTrackRecordContent objContent = null;
            switch ((enmDiseaseTrackType)p_intRecordType)
            {
                case enmDiseaseTrackType.GeneralNurseRecord_GX:
                    objContent = new clsGeneralNurseRecordContent_GX();
                    break;
            }

            if (objContent == null)
                objContent = new clsGeneralNurseRecordContent_GX();

            if (m_objCurrentPatient != null)
                objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            else
            {
                clsPublicFunction.ShowInformationMessageBox("��ǰ����Ϊ��!");
                return null;
            }
            int intSelectedRecordStartRow = m_dtgRecordDetail.CurrentCell.RowNumber;
            string strDetailSign = (m_dtbRecords.Rows[intSelectedRecordStartRow][17]).ToString();

            if (strDetailSign != null && strDetailSign.Trim() != "")
            {
                string[] strArr = strDetailSign.Split('��');
                if (strArr != null && strArr[0] != string.Empty)
                {
                    objContent.m_strCreateUserID = strArr[0];
                }
            }
            objContent.m_dtmInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
            objContent.m_dtmCreateDate = DateTime.Parse((string)p_objDataArr[0]);
            objContent.m_dtmOpenDate = DateTime.Parse((string)p_objDataArr[2]);
            objContent.m_dtmModifyDate = DateTime.Parse((string)p_objDataArr[3]);
            objContent.m_strCreateUserID = (string)p_objDataArr[18];

            return objContent;
        }

        private void frmGeneralNurseRecord_GX_Load(object sender, System.EventArgs e)
        {
            #region ����Ҽ��˵�
            System.Windows.Forms.MenuItem mniContentAdd = new System.Windows.Forms.MenuItem();
            mniContentAdd.Index = 10;
            mniContentAdd.Text = "��Ӳ��̼�¼����";
            mniContentAdd.Click += new System.EventHandler(mniContentAdd_Click);
            System.Windows.Forms.MenuItem mniContentModify = new System.Windows.Forms.MenuItem();
            mniContentModify.Index = 11;
            mniContentModify.Text = "�޸Ĳ��̼�¼����";
            mniContentModify.Click += new System.EventHandler(mniContentModify_Click);
            System.Windows.Forms.MenuItem mniContentDelete = new System.Windows.Forms.MenuItem();
            mniContentDelete.Index = 12;
            mniContentDelete.Text = "ɾ�����̼�¼����";
            mniContentDelete.Click += new System.EventHandler(mniContentDelete_Click);
            this.ctmRecordControl.MenuItems.Add(mniContentAdd);
            this.ctmRecordControl.MenuItems.Add(mniContentModify);
            this.ctmRecordControl.MenuItems.Add(mniContentDelete);

            #endregion ;
            m_dtmPreRecordDate = DateTime.MinValue;
            m_dtgRecordDetail.Focus();
            m_mniAddBlank.Visible = false;
            m_mniDeleteBlank.Visible = false;
        }

        private void m_mthSubFormClosed(object p_objSender, EventArgs p_objArg)
        {
            frmGeneralNurseRecord_GXCon frmAddNewForm = (frmGeneralNurseRecord_GXCon)p_objSender;
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
                frmGeneralNurseRecord_GXCon frm = new frmGeneralNurseRecord_GXCon(true, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), "2005");
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
                if (m_dtgRecordDetail.CurrentCell.ColumnNumber != 11)
                {
                    MessageBox.Show("��ѡ��һ�������¼���ݣ�");
                    return;
                }

                //���ݲ���
                int intSelectedRecordStartRow = m_dtgRecordDetail.CurrentCell.RowNumber;
                string strRecordTime = (m_dtbRecords.Rows[intSelectedRecordStartRow][16]).ToString();
                if (strRecordTime.Trim().Length == 0)
                    return;
                //�򿪴���
                frmGeneralNurseRecord_GXCon frm = new frmGeneralNurseRecord_GXCon(false, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), strRecordTime);
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
                if (m_dtgRecordDetail.CurrentCell.ColumnNumber != 11)
                {
                    MessageBox.Show("��ѡ��һ�������¼���ݣ�");
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

                string strDetailSign = (m_dtbRecords.Rows[intSelectedRecordStartRow][17]).ToString();

                //���� by tfzhang 2006-02-24
                if (strDetailSign != null && strDetailSign.Trim() != "")
                {
                    string[] strArr = strDetailSign.Split('��');
                    //if (strArr != null && strArr.Length > 1 && strArr[1] != string.Empty && strArr[1].Trim() != MDIParent.OperatorID.Trim())
                    //{
                    //    MDIParent.ShowInformationMessageBox("�Ǽ�¼�����߲���ɾ����¼��");
                    //    return;
                    //}
                    //Ȩ���ж�
                    string strDeptIDTemp = m_ObjCurrentEmrPatientSession.m_strAreaId;// ((clsDepartment)m_cboDept.SelectedItem).m_strDeptNewID; 
                    bool blnIsAllow = clsPublicFunction.IsAllowDelete(strDeptIDTemp, strArr[1], clsEMRLogin.LoginEmployee, intFormType);
                    if (!blnIsAllow)
                        return;
                }


                string strRecordTime = (m_dtbRecords.Rows[intSelectedRecordStartRow][16]).ToString();
                if (strRecordTime.Trim().Length == 0)
                    return;
                //ȷ��
                if (MessageBox.Show("ȷ��Ҫɾ��ѡ�еĲ����¼���ݣ�", "ɾ����ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;

                //�򿪴���
                //ɾ��

                //clsGeneralNurseRecord_GXService objserv =
                //    (clsGeneralNurseRecord_GXService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsGeneralNurseRecord_GXService));

                string strDelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string strDelID = MDIParent.OperatorID;
                lngRes = (new weCare.Proxy.ProxyEmr05()).Service.clsGeneralNurseRecord_GXService_m_lngDeleteDetail(m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), strRecordTime, strDelTime, strDelID);
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
                case enmDiseaseTrackType.GeneralNurseRecord_GX:
                    return new frmGeneralNurseRecord_GXRec();
                case enmDiseaseTrackType.GeneralNurseRecord_GXCon:
                    return new frmGeneralNurseRecord_GXCon(false, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), "");
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
            m_mthAddNewRecord((int)enmDiseaseTrackType.GeneralNurseRecord_GX);
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

                clsGeneralNurseRecordContent_GXDataInfo objGNRCInfo = new clsGeneralNurseRecordContent_GXDataInfo();
                clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
                string strText, strXml;

                objGNRCInfo = (clsGeneralNurseRecordContent_GXDataInfo)p_objTransDataInfo;

                if (objGNRCInfo.m_objRecordArr == null && objGNRCInfo.m_objDetailArr == null)
                    return null;

                #region �Բ����¼���д���
                if (objGNRCInfo.m_objDetailArr != null)
                {
                    string strDetail = "";
                    string strDetailXML = "";
                    for (int n = 0; n < objGNRCInfo.m_objDetailArr.Length; n++)
                    {
                        clsGeneralNurseRecordContent_GXDetail objDetail = objGNRCInfo.m_objDetailArr[n];
                        object[] objTemp = new object[7];
                        strDetail = objDetail.m_strRECORDCONTENTAll;
                        strDetailXML = objDetail.m_strRECORDCONTENTXML;
                        string[] strDetailArrTemp;
                        string[] strDetailXMLArrTemp;
                        //�������¼��Ϊ�С�
                        com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strDetail, strDetailXML, 34, out strDetailArrTemp, out strDetailXMLArrTemp);
                        string[] strDetailArr, strDetailXMLArr;
                        if (strDetail != string.Empty)
                        {
                            strDetailArr = new string[strDetailArrTemp.Length + 2];//���������ں�ǩ�������¼
                            strDetailXMLArr = new string[strDetailXMLArrTemp.Length + 2];//���������ں�ǩ�������¼XML

                            //�����ں�ǩ����ӽ������¼
                            strDetailArr[0] = objDetail.m_dtmRECORDDATE.ToString("yyyy-MM-dd HH:mm");
                            strDetailArr[1] = strDetailArrTemp[0];
                            for (int i = 2; i < strDetailArr.Length - 1; i++)
                            {
                                strDetailArr[i] = strDetailArrTemp[i - 1];
                            }
                            strDetailArr[strDetailArr.Length - 1] = "                         " + objDetail.m_strDetailCreateUserName;

                            strDetailXMLArr[0] = strDetailXMLArr[strDetailXMLArr.Length - 1] = "";
                            for (int i = 1; i < strDetailXMLArr.Length - 1; i++)
                            {
                                strDetailXMLArr[i] = strDetailXMLArrTemp[i - 1];
                            }

                            objTemp[0] = strDetailArr;
                            objTemp[1] = strDetailXMLArr;
                            objTemp[2] = strDetailArr.Length;
                            objTemp[3] = objDetail.m_dtmRECORDDATE;
                            objTemp[4] = objDetail.m_intClass;
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

                for (int i = 0; i < intRecordCount; i++)
                {
                    objData = new object[19];
                    clsGeneralNurseRecordContent_GX objCurrent = objGNRCInfo.m_objRecordArr[i];
                    clsGeneralNurseRecordContent_GX objNext = new clsGeneralNurseRecordContent_GX();//��һ�������¼
                    if (i < intRecordCount - 1)
                        objNext = objGNRCInfo.m_objRecordArr[i + 1];
                    clsGeneralNurseRecordContent_GX objLast = null;
                    if (i > 0)
                        objLast = objGNRCInfo.m_objRecordArr[i - 1];
                    //����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ��޸����봴����Ϊͬһ�ˣ�����ʾ
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        //  && objNext.m_strModifyUserID.Trim()==objCurrent.m_strCreateUserID.Trim()
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
                        objData[1] = (int)enmRecordsType.GeneralNurseRecord_GXRec;//��ż�¼���͵�intֵ
                        objData[2] = objCurrent.m_dtmOpenDate;//��ż�¼��OpenDate�ַ���
                        objData[3] = objCurrent.m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   

                        //ͬһ����ֻ�ڵ�һ����ʾ����
                        if (objCurrent.m_dtmRECORDDATE.Date.ToString() != m_dtmPreRecordDate.Date.ToString())
                        {
                            objData[4] = objCurrent.m_dtmRECORDDATE.Date.ToString("yyyy-MM-dd");//�����ַ���
                        }
                        //�޸ĺ���кۼ��ļ�¼������ʾʱ��
                        if (m_dtmPreRecordDate != objCurrent.m_dtmRECORDDATE)
                            objData[5] = objCurrent.m_dtmRECORDDATE.ToString("HH:mm");//ʱ���ַ���

                        objData[18] = objCurrent.m_strCreateUserID;//��ż�¼��createUserid�ַ���   

                    }
                    m_dtmPreRecordDate = objCurrent.m_dtmRECORDDATE;
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
                    strText = objCurrent.m_strPULSE_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strPULSE_RIGHT != objCurrent.m_strPULSE_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strPULSE_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strPULSE_RIGHT) && !blnPreIsHide)
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
                    objData[9] = objclsDSTRichTextBoxValue;//HR


                    //ѪѹA
                    strText = objCurrent.m_strBLOODPRESSUREA_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strBLOODPRESSUREA_RIGHT != objCurrent.m_strBLOODPRESSUREA_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strBLOODPRESSUREA_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strBLOODPRESSUREA_RIGHT) && !blnPreIsHide)
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

                    //ѪѹS
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
                    objData[11] = objclsDSTRichTextBoxValue;//

                    //�Զ�����1
                    strText = objCurrent.m_strCUSTOM1_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strCUSTOM1_RIGHT != objCurrent.m_strCUSTOM1_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strCUSTOM1_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strCUSTOM1_RIGHT) && !blnPreIsHide)
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
                    objData[12] = objclsDSTRichTextBoxValue;//

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
                    objData[13] = objclsDSTRichTextBoxValue;//

                    //ǩ��
                    strText = objCurrent.m_strContentCreateUserName;
                    objData[14] = strText;
                    blnPreIsHide = false;

                    //�����¼
                    if (arlDetail != null && intCurrentDetail < arlDetail.Count && arlDetail.Count > 0)
                    {
                        int intClass = (int)(((object[])(arlDetail[intCurrentDetail]))[4]);
                        DateTime dtDetailRecordTime = (DateTime)(((object[])(arlDetail[intCurrentDetail]))[3]);
                        //��Ϊ�ɼ�¼δ�б�������Ϣ�����½����ж�
                        if (objCurrent.m_intClass == 0 || objCurrent.m_intClass == 1 || intClass == 0)
                            blnIsSameClass = m_blnIsSameClass(objCurrent.m_dtmRECORDDATE, dtDetailRecordTime);
                        else
                            blnIsSameClass = objCurrent.m_intClass == intClass ? true : false;
                        //�����һ��Σ�ֱ�����
                        if (blnIsSameClass)
                        {
                            strText = ((string[])(((object[])arlDetail[intCurrentDetail])[0]))[intRowOfCurrentDetail];
                            strXml = ((string[])(((object[])arlDetail[intCurrentDetail])[1]))[intRowOfCurrentDetail]; ;
                            objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                            objclsDSTRichTextBoxValue.m_strText = strText;
                            objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                            objData[15] = objclsDSTRichTextBoxValue;
                            objData[16] = (DateTime)(((object[])arlDetail[intCurrentDetail])[3]);
                            objData[17] = objCurrent.m_strCreateUserID + "��" + (((object[])arlDetail[intCurrentDetail])[6]).ToString();

                            if (intRowOfCurrentDetail == ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1)
                            {
                                intRowOfCurrentDetail = 0;
                                intCurrentDetail++;
                            }
                            else
                                intRowOfCurrentDetail++;
                            objReturnData.Add(objData);
                        }
                        else if (objNext != null)
                        {
                            while (arlDetail != null &&
                                intCurrentDetail < arlDetail.Count &&
                                intRowOfCurrentDetail < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length &&
                                (DateTime)(((object[])arlDetail[intCurrentDetail])[3]) <= objCurrent.m_dtmRECORDDATE)
                            {
                                //��Ϊ�ɼ�¼δ�б�������Ϣ�����½����ж�
                                if (objNext.m_intClass == 0 || objNext.m_intClass == 1 || (int)(((object[])arlDetail[intCurrentDetail])[4]) == 0)
                                    blnIsSameClass = m_blnIsSameClass(objCurrent.m_dtmRECORDDATE, (DateTime)(((object[])arlDetail[intCurrentDetail])[3]));
                                else
                                    blnIsSameClass = objNext.m_intClass == (int)(((object[])arlDetail[intCurrentDetail])[4]) ? true : false;

                                if (!blnIsSameClass)
                                {
                                    for (int m = intRowOfCurrentDetail; m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length; m++)
                                    {
                                        object[] objOtherDetail = new object[18];
                                        m_mthSetOtherDetail(((object[])arlDetail[intCurrentDetail]), intCurrentDetail, m, objCurrent, out objOtherDetail);
                                        objReturnData.Add(objOtherDetail);
                                    }

                                    intCurrentDetail++;
                                    intRowOfCurrentDetail = 0;
                                }
                                else
                                    break;
                            }
                        }
                    }
                    if (objData != null && objData[15] == null)
                        objReturnData.Add(objData);
                    #endregion
                }

                //��������¼δ��ʾ������������¼��ȫ����ʾ�꣬��������ʣ�ಿ��
                while (arlDetail != null && intCurrentDetail < arlDetail.Count && arlDetail.Count > 0)
                {
                    if (intRowOfCurrentDetail < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length)
                    {
                        for (int m = intRowOfCurrentDetail; m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length; m++)
                        {
                            object[] objOtherDetail = new object[18];
                            m_mthSetOtherDetail(((object[])arlDetail[intCurrentDetail]), intCurrentDetail, m, null, out objOtherDetail);
                            objReturnData.Add(objOtherDetail);
                        }

                        intCurrentDetail++;
                        intRowOfCurrentDetail = 0;
                    }
                }
                object[][] m_objRe = new object[objReturnData.Count][];

                for (int m = 0; m < objReturnData.Count; m++)
                    m_objRe[m] = (object[])objReturnData[m];
                return m_objRe;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            #endregion
        }

        private void m_mthSetOtherDetail(object[] objDetail, int intCurrentDetail, int intRowOfCurrentDetail, clsGeneralNurseRecordContent_GX objCurrent, out object[] objOtherDetail)
        {
            objOtherDetail = new object[18];
            string strText = ((string[])(objDetail[0]))[intRowOfCurrentDetail];
            string strXml = ((string[])(objDetail[1]))[intRowOfCurrentDetail]; ;
            clsDSTRichTextBoxValue objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
            objclsDSTRichTextBoxValue.m_strText = strText;
            objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
            objOtherDetail[15] = objclsDSTRichTextBoxValue;
            objOtherDetail[16] = (DateTime)objDetail[3];
            if (objCurrent != null)
                objOtherDetail[17] = objCurrent.m_strCreateUserID + "��" + objDetail[6].ToString();
            else
                objOtherDetail[17] = "0��" + objDetail[6].ToString();
        }

        /// <summary>
        /// �Ծɼ�¼�����жϣ��Ƿ���ͬһ�����
        /// </summary>
        /// <param name="p_dtmMainRecord"></param>
        /// <param name="p_dtmDetail"></param>
        /// <returns></returns>
        private bool m_blnIsSameClass(DateTime p_dtmMainRecord, DateTime p_dtmDetail)
        {
            if (m_intGetClass(p_dtmMainRecord) == m_intGetClass(p_dtmDetail))
                return true;
            else
                return false;
        }

        /// <summary>
        /// ��ȡ���
        /// ����-����ʱ��8:00-14:30,14:31-18:00,18:01-����2:00,����2:01-7:59
        /// </summary>
        /// <param name="dtmRecordDate"></param>
        /// <returns></returns>
        private int m_intGetClass(DateTime dtmRecordDate)
        {
            string strDate = dtmRecordDate.Year.ToString("0000") + dtmRecordDate.Month.ToString("00") + dtmRecordDate.Day.ToString("00");
            string strYesterday = dtmRecordDate.Year.ToString() + dtmRecordDate.Month.ToString() + dtmRecordDate.AddDays(-1).Day.ToString();
            DateTime dtClass = DateTime.Parse(dtmRecordDate.ToString("yyyy-MM-dd HH:mm:00"));
            DateTime dtDt0 = dtmRecordDate.Date;
            DateTime dt1 = dtDt0.AddHours(2).AddMinutes(1);
            DateTime dt2 = dtDt0.AddHours(8);
            DateTime dt3 = dtDt0.AddHours(14).AddMinutes(31);
            DateTime dt4 = dtDt0.AddHours(18).AddMinutes(1);
            DateTime dt5 = dtDt0.AddHours(26).AddMinutes(1);

            if (dtmRecordDate >= dt1 && dtmRecordDate < dt2)
                return Convert.ToInt32(strDate + "0");
            else if (dtmRecordDate >= dt2 && dtmRecordDate < dt3)
                return Convert.ToInt32(strDate + "1");
            else if (dtmRecordDate >= dt3 && dtmRecordDate < dt4)
                return Convert.ToInt32(strDate + "2");
            else if (dtmRecordDate >= dt4 && dtmRecordDate < dt5)
                return Convert.ToInt32(strDate + "3");
            else if (dtmRecordDate < dt1)
                return Convert.ToInt32(strYesterday + "3");
            return 0;
        }
        #region ��ӡ
        protected override void m_mthStartPrint()
        {
            //ȱʡʹ�ô�ӡԤ�����Ӵ��������ṩ�µ�ʵ��
            try
            {
                if (m_blnDirectPrint)
                {
                    m_pdcPrintDocument.Print();
                }
                else
                {

                    ((clsGeneralNurseRecord_GXPrintTool)objPrintTool).m_mthPrintPage(m_pdcPrintDocument.DefaultPageSettings);

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("No Printers installed") >= 0)
                    clsPublicFunction.ShowInformationMessageBox("�Ҳ�����ӡ����");
                else MessageBox.Show(ex.Message);
            }

            //			base.m_mthStartPrint();

        }

        protected override infPrintRecord m_objGetPrintTool()
        {
            //			return new clsIntensiveTendMainPrintTool();
            string[] m_strCustomColumn = { m_strCustomColumn1, m_strCustomColumn2 };
            return new clsGeneralNurseRecord_GXPrintTool(m_strCustomColumn);
        }
        #endregion

        #region �Զ����ͷ����
        private void m_dtgRecordDetail_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (MDIParent.m_objCurrentPatient == null)
                return;
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                System.Windows.Forms.DataGrid.HitTestInfo myHitTest = m_dtgRecordDetail.HitTest(e.X, e.Y);
                if (myHitTest.Column == 8 || myHitTest.Column == 9)
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
                case 8:
                    m_mthSaveColumnNameToDB("CUSTOM1NAME", ref m_strCustomColumn1);
                    m_dtcCustom1.HeaderText = m_strCustomColumn1;
                    break;
                case 9:
                    m_mthSaveColumnNameToDB("CUSTOM2NAME", ref m_strCustomColumn2);
                    m_dtcCustom2.HeaderText = m_strCustomColumn2;
                    break;
            }
        }

        private void m_mthSaveColumnNameToDB(string p_strColumnIndex, ref string p_strColumnName)
        {
            p_strColumnName = "";
            long lngRes = 0;

            //clsGeneralNurseRecord_GXService objServ =
            //    (clsGeneralNurseRecord_GXService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsGeneralNurseRecord_GXService));

            if (m_strTempColumnName != "")
            {
                int intColumnNameLength = m_strTempColumnName.Length;
                for (int i = 0; i < intColumnNameLength; i++)
                {
                    if (intColumnNameLength > 5)//��������5������ֱ�Ӵ������ʼ��ʾ
                    {
                        if (i == 0)
                            p_strColumnName += m_strTempColumnName[i].ToString();
                        else
                            p_strColumnName += "\r\n" + m_strTempColumnName[i].ToString();
                    }
                    else
                        p_strColumnName += "\r\n" + m_strTempColumnName[i].ToString();
                }
                //objServ.Dispose();
            }
            lngRes = (new weCare.Proxy.ProxyEmr05()).Service.clsGeneralNurseRecord_GXService_m_lngSetCustomColumnName(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), p_strColumnIndex, p_strColumnName);
        }

        /// <summary>
        /// �����Զ�����ͷ
        /// </summary>
        private void m_mthSetCustomColumnName()
        {
            m_dtcCustom1.HeaderText = m_strCustomColumn1;
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

                    m_dtcCustom1.HeaderText = "";
                    m_dtcCustom2.HeaderText = "";
                    if (i1 == 0)
                    {
                        clsGeneralNurseRecordContent_GXDataInfo objITRCInfo = (clsGeneralNurseRecordContent_GXDataInfo)(objTansDataInfoArr[0]);
                        if (objITRCInfo != null && objITRCInfo.m_objRecordArr != null)
                        {
                            clsGeneralNurseRecordContent_GX objCurrent = objITRCInfo.m_objRecordArr[0];
                            if (objCurrent != null)
                            {
                                m_strCustomColumn1 = objCurrent.m_strCUSTOM1NAME == null ? "" : objCurrent.m_strCUSTOM1NAME;
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
                //��ղ��˼�¼��Ϣ
                if (m_dtgRecordDetail != null)
                {
                    m_mthClearPatientRecordInfo();
                }

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

                    m_dtcCustom1.HeaderText = "";
                    m_dtcCustom2.HeaderText = "";
                    if (i1 == 0)
                    {
                        clsGeneralNurseRecordContent_GXDataInfo objITRCInfo = (clsGeneralNurseRecordContent_GXDataInfo)(objTansDataInfoArr[0]);
                        if (objITRCInfo != null && objITRCInfo.m_objRecordArr != null)
                        {
                            clsGeneralNurseRecordContent_GX objCurrent = objITRCInfo.m_objRecordArr[0];
                            if (objCurrent != null)
                            {
                                m_strCustomColumn1 = objCurrent.m_strCUSTOM1NAME == null ? "" : objCurrent.m_strCUSTOM1NAME;
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
    }
}
