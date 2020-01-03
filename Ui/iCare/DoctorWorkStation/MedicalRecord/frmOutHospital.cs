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
    /// ����Ժ�����̼�¼�Ӵ����ʵ��,Muzhong-2003-5-26
    /// </summary>
    public class frmOutHospital : iCare.frmDiseaseTrackBase
    {
        private com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
        protected com.digitalwave.Utility.Controls.ctlTimePicker m_dtpOutHospitalDate;
        private System.Windows.Forms.Label lblInHospitalDateTitle;
        private System.Windows.Forms.Label m_lblInHospitalDate;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private TextBox m_txtMainDoctorSign;
        private System.Windows.Forms.Label lblCaseHistoryTitle;
        private System.Windows.Forms.Label lblOutHospitalDiagnoseTitle;
        private System.Windows.Forms.Label lblOriginalDiagnoseTitle;
        private System.Windows.Forms.Label lblOutHospitalReasonTitle;
        private System.Windows.Forms.Label lblNoticeTitle;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private TextBox m_txtDoctorSign;
        private PinkieControls.ButtonXP cmdConfirm;
        private com.digitalwave.controls.ctlRichTextBox m_txtInHospitalBy;
        private com.digitalwave.controls.ctlRichTextBox m_txtOutHospitalDiagnose;
        private com.digitalwave.controls.ctlRichTextBox m_txtInHospitalDiagnose;
        private com.digitalwave.controls.ctlRichTextBox m_txtOutHospitalAdvice;
        private com.digitalwave.controls.ctlRichTextBox m_txtOutHospitalCase;
        protected System.Windows.Forms.Label lblHeart;
        protected System.Windows.Forms.Label lblXRay;
        private System.Windows.Forms.Label lblInhospitalCase;
        private com.digitalwave.controls.ctlRichTextBox m_txtInHospitalCase;
        private System.Windows.Forms.Label lblEmployeeIDTitle;
        private System.Windows.Forms.Label lblEmployeeID;
        private com.digitalwave.controls.ctlRichTextBox m_txtHeartID;
        private com.digitalwave.controls.ctlRichTextBox m_txtXRayID;
        private System.Windows.Forms.Label lblOutHospitalDate;
        private PinkieControls.ButtonXP m_cmdClose;
        private System.Windows.Forms.Label lblEmployeeSign;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.ComponentModel.IContainer components = null;
        private PinkieControls.ButtonXP m_cmdMainDoctorSign;

        private PinkieControls.ButtonXP m_cmdDoctorSign;
        private clsEmrSignToolCollection m_objSign;
        private DateTime m_dtmOutHospitalDate;

        public frmOutHospital()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            //ָ��ҽ������վ��
            intFormType = 1;
            // TODO: Add any initialization after the InitializeComponent call

            cmdConfirm.Visible = false;
            m_cmdClose.Visible = false;

            m_mthSetRichTextBoxAttribInControl(this);

            this.Text = "��Ժ��¼";
            this.m_lblForTitle.Text = this.Text;

            m_objSign = new clsEmrSignToolCollection();
            m_objSign.m_mthBindEmployeeSign(m_cmdMainDoctorSign, m_txtMainDoctorSign, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
            m_objSign.m_mthBindEmployeeSign(m_cmdDoctorSign, m_txtDoctorSign, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
            //�����ص��޺ۼ��޸�����.����ʹ��
            chkModifyWithoutMatk.Left = m_trvCreateDate.Left;
            chkModifyWithoutMatk.Top = m_trvCreateDate.Top;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_blnHasInitPrintTool)
                {
                    objPrintTool.m_mthDisposePrintTools(null);
                }

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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOutHospital));
            this.lblOutHospitalDate = new System.Windows.Forms.Label();
            this.m_dtpOutHospitalDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblInHospitalDateTitle = new System.Windows.Forms.Label();
            this.m_lblInHospitalDate = new System.Windows.Forms.Label();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.m_txtMainDoctorSign = new System.Windows.Forms.TextBox();
            this.lblInhospitalCase = new System.Windows.Forms.Label();
            this.lblCaseHistoryTitle = new System.Windows.Forms.Label();
            this.lblOutHospitalDiagnoseTitle = new System.Windows.Forms.Label();
            this.lblOriginalDiagnoseTitle = new System.Windows.Forms.Label();
            this.m_txtInHospitalCase = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtInHospitalBy = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtOutHospitalDiagnose = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtInHospitalDiagnose = new com.digitalwave.controls.ctlRichTextBox();
            this.lblOutHospitalReasonTitle = new System.Windows.Forms.Label();
            this.lblNoticeTitle = new System.Windows.Forms.Label();
            this.m_txtOutHospitalAdvice = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtOutHospitalCase = new com.digitalwave.controls.ctlRichTextBox();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.m_txtDoctorSign = new System.Windows.Forms.TextBox();
            this.cmdConfirm = new PinkieControls.ButtonXP();
            this.lblHeart = new System.Windows.Forms.Label();
            this.lblXRay = new System.Windows.Forms.Label();
            this.lblEmployeeIDTitle = new System.Windows.Forms.Label();
            this.lblEmployeeID = new System.Windows.Forms.Label();
            this.m_txtHeartID = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtXRayID = new com.digitalwave.controls.ctlRichTextBox();
            this.m_cmdClose = new PinkieControls.ButtonXP();
            this.lblEmployeeSign = new System.Windows.Forms.Label();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.m_cmdMainDoctorSign = new PinkieControls.ButtonXP();
            this.m_cmdDoctorSign = new PinkieControls.ButtonXP();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_trvCreateDate
            // 
            this.m_trvCreateDate.ForeColor = System.Drawing.Color.Black;
            this.m_trvCreateDate.LineColor = System.Drawing.Color.Black;
            this.m_trvCreateDate.Location = new System.Drawing.Point(6, 35);
            this.m_trvCreateDate.Size = new System.Drawing.Size(193, 55);
            // 
            // lblCreateDateTitle
            // 
            this.lblCreateDateTitle.Location = new System.Drawing.Point(201, 67);
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.Location = new System.Drawing.Point(268, 64);
            // 
            // m_dtpGetDataTime
            // 
            this.m_dtpGetDataTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_dtpGetDataTime.Location = new System.Drawing.Point(184, 105);
            // 
            // m_lblGetDataTime
            // 
            this.m_lblGetDataTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_lblGetDataTime.Location = new System.Drawing.Point(80, 105);
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(315, 255);
            this.lblSex.Size = new System.Drawing.Size(36, 19);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(290, 252);
            this.lblAge.Size = new System.Drawing.Size(78, 19);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(312, 269);
            this.lblBedNoTitle.Size = new System.Drawing.Size(56, 14);
            this.lblBedNoTitle.Text = "��  ��:";
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(272, 260);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(289, 265);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(321, 260);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(324, 251);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(272, 269);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(256, 260);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(100, 23);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(315, 260);
            this.txtInPatientID.Size = new System.Drawing.Size(98, 23);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(294, 257);
            this.m_txtPatientName.Size = new System.Drawing.Size(102, 23);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(292, 251);
            this.m_txtBedNO.Size = new System.Drawing.Size(76, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(256, 260);
            this.m_cboArea.Size = new System.Drawing.Size(134, 23);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(275, 269);
            this.m_lsvPatientName.Size = new System.Drawing.Size(76, 14);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(275, 260);
            this.m_lsvBedNO.Size = new System.Drawing.Size(92, 23);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(256, 260);
            this.m_cboDept.Size = new System.Drawing.Size(134, 23);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(272, 260);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(275, 251);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Location = new System.Drawing.Point(324, 257);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(327, 245);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(320, 248);
            this.m_lblForTitle.Text = "�� Ժ �� ¼";
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(419, 259);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(722, 38);
            this.m_cmdModifyPatientInfo.Size = new System.Drawing.Size(69, 26);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Controls.Add(this.lblXRay);
            this.m_pnlNewBase.Controls.Add(this.lblHeart);
            this.m_pnlNewBase.Controls.Add(this.m_txtHeartID);
            this.m_pnlNewBase.Controls.Add(this.m_txtXRayID);
            this.m_pnlNewBase.Location = new System.Drawing.Point(5, 5);
            this.m_pnlNewBase.Size = new System.Drawing.Size(796, 86);
            this.m_pnlNewBase.Visible = true;
            this.m_pnlNewBase.Controls.SetChildIndex(this.m_ctlPatientInfo, 0);
            this.m_pnlNewBase.Controls.SetChildIndex(this.m_txtXRayID, 0);
            this.m_pnlNewBase.Controls.SetChildIndex(this.m_txtHeartID, 0);
            this.m_pnlNewBase.Controls.SetChildIndex(this.lblHeart, 0);
            this.m_pnlNewBase.Controls.SetChildIndex(this.lblXRay, 0);
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ctlPatientInfo.Dock = System.Windows.Forms.DockStyle.None;
            this.m_ctlPatientInfo.Location = new System.Drawing.Point(193, 29);
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(601, 56);
            // 
            // lblOutHospitalDate
            // 
            this.lblOutHospitalDate.AutoSize = true;
            this.lblOutHospitalDate.BackColor = System.Drawing.SystemColors.Control;
            this.lblOutHospitalDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOutHospitalDate.ForeColor = System.Drawing.Color.Black;
            this.lblOutHospitalDate.Location = new System.Drawing.Point(4, 101);
            this.lblOutHospitalDate.Name = "lblOutHospitalDate";
            this.lblOutHospitalDate.Size = new System.Drawing.Size(70, 14);
            this.lblOutHospitalDate.TabIndex = 29165;
            this.lblOutHospitalDate.Text = "��Ժʱ��:";
            // 
            // m_dtpOutHospitalDate
            // 
            this.m_dtpOutHospitalDate.BorderColor = System.Drawing.Color.Black;
            this.m_dtpOutHospitalDate.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
            this.m_dtpOutHospitalDate.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_dtpOutHospitalDate.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_dtpOutHospitalDate.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_dtpOutHospitalDate.flatFont = new System.Drawing.Font("����", 12F);
            this.m_dtpOutHospitalDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_dtpOutHospitalDate.ForeColor = System.Drawing.Color.Black;
            this.m_dtpOutHospitalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpOutHospitalDate.Location = new System.Drawing.Point(76, 97);
            this.m_dtpOutHospitalDate.m_BlnOnlyTime = false;
            this.m_dtpOutHospitalDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.m_dtpOutHospitalDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.m_dtpOutHospitalDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.m_dtpOutHospitalDate.Name = "m_dtpOutHospitalDate";
            this.m_dtpOutHospitalDate.ReadOnly = false;
            this.m_dtpOutHospitalDate.Size = new System.Drawing.Size(212, 22);
            this.m_dtpOutHospitalDate.TabIndex = 120;
            this.m_dtpOutHospitalDate.TextBackColor = System.Drawing.Color.White;
            this.m_dtpOutHospitalDate.TextForeColor = System.Drawing.Color.Black;
            this.m_dtpOutHospitalDate.Visible = false;
            // 
            // lblInHospitalDateTitle
            // 
            this.lblInHospitalDateTitle.AutoSize = true;
            this.lblInHospitalDateTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInHospitalDateTitle.ForeColor = System.Drawing.Color.Black;
            this.lblInHospitalDateTitle.Location = new System.Drawing.Point(199, 269);
            this.lblInHospitalDateTitle.Name = "lblInHospitalDateTitle";
            this.lblInHospitalDateTitle.Size = new System.Drawing.Size(70, 14);
            this.lblInHospitalDateTitle.TabIndex = 29162;
            this.lblInHospitalDateTitle.Text = "��Ժʱ��:";
            this.lblInHospitalDateTitle.Visible = false;
            // 
            // m_lblInHospitalDate
            // 
            this.m_lblInHospitalDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblInHospitalDate.ForeColor = System.Drawing.Color.Black;
            this.m_lblInHospitalDate.Location = new System.Drawing.Point(240, 260);
            this.m_lblInHospitalDate.Name = "m_lblInHospitalDate";
            this.m_lblInHospitalDate.Size = new System.Drawing.Size(212, 19);
            this.m_lblInHospitalDate.TabIndex = 29163;
            this.m_lblInHospitalDate.Visible = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 100;
            // 
            // m_txtMainDoctorSign
            // 
            this.m_txtMainDoctorSign.AccessibleName = "NoDefault";
            this.m_txtMainDoctorSign.BackColor = System.Drawing.Color.White;
            this.m_txtMainDoctorSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtMainDoctorSign.ForeColor = System.Drawing.Color.Black;
            this.m_txtMainDoctorSign.Location = new System.Drawing.Point(372, 97);
            this.m_txtMainDoctorSign.Name = "m_txtMainDoctorSign";
            this.m_txtMainDoctorSign.ReadOnly = true;
            this.m_txtMainDoctorSign.Size = new System.Drawing.Size(165, 23);
            this.m_txtMainDoctorSign.TabIndex = 110;
            // 
            // lblInhospitalCase
            // 
            this.lblInhospitalCase.AutoSize = true;
            this.lblInhospitalCase.BackColor = System.Drawing.Color.Transparent;
            this.lblInhospitalCase.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInhospitalCase.ForeColor = System.Drawing.Color.Black;
            this.lblInhospitalCase.Location = new System.Drawing.Point(5, 220);
            this.lblInhospitalCase.Name = "lblInhospitalCase";
            this.lblInhospitalCase.Size = new System.Drawing.Size(70, 14);
            this.lblInhospitalCase.TabIndex = 29191;
            this.lblInhospitalCase.Text = "��Ժ���:";
            // 
            // lblCaseHistoryTitle
            // 
            this.lblCaseHistoryTitle.AutoSize = true;
            this.lblCaseHistoryTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblCaseHistoryTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCaseHistoryTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCaseHistoryTitle.Location = new System.Drawing.Point(5, 285);
            this.lblCaseHistoryTitle.Name = "lblCaseHistoryTitle";
            this.lblCaseHistoryTitle.Size = new System.Drawing.Size(763, 14);
            this.lblCaseHistoryTitle.TabIndex = 29188;
            this.lblCaseHistoryTitle.Text = "���ƾ���: (����סԺʱ��Ҫ��ʷ��֢״���������������Ļ��鼰��е�������סԺ�ڼ�Ĳ���仯����鼰���ƾ���)";
            // 
            // lblOutHospitalDiagnoseTitle
            // 
            this.lblOutHospitalDiagnoseTitle.AutoSize = true;
            this.lblOutHospitalDiagnoseTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblOutHospitalDiagnoseTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOutHospitalDiagnoseTitle.ForeColor = System.Drawing.Color.Black;
            this.lblOutHospitalDiagnoseTitle.Location = new System.Drawing.Point(372, 126);
            this.lblOutHospitalDiagnoseTitle.Name = "lblOutHospitalDiagnoseTitle";
            this.lblOutHospitalDiagnoseTitle.Size = new System.Drawing.Size(70, 14);
            this.lblOutHospitalDiagnoseTitle.TabIndex = 29184;
            this.lblOutHospitalDiagnoseTitle.Text = "��Ժ���:";
            // 
            // lblOriginalDiagnoseTitle
            // 
            this.lblOriginalDiagnoseTitle.AutoSize = true;
            this.lblOriginalDiagnoseTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblOriginalDiagnoseTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOriginalDiagnoseTitle.ForeColor = System.Drawing.Color.Black;
            this.lblOriginalDiagnoseTitle.Location = new System.Drawing.Point(3, 126);
            this.lblOriginalDiagnoseTitle.Name = "lblOriginalDiagnoseTitle";
            this.lblOriginalDiagnoseTitle.Size = new System.Drawing.Size(70, 14);
            this.lblOriginalDiagnoseTitle.TabIndex = 29182;
            this.lblOriginalDiagnoseTitle.Text = "��Ժ���:";
            // 
            // m_txtInHospitalCase
            // 
            this.m_txtInHospitalCase.AccessibleDescription = "��Ժ���";
            this.m_txtInHospitalCase.BackColor = System.Drawing.Color.White;
            this.m_txtInHospitalCase.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtInHospitalCase.ForeColor = System.Drawing.Color.Black;
            this.m_txtInHospitalCase.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtInHospitalCase.Location = new System.Drawing.Point(75, 213);
            this.m_txtInHospitalCase.m_BlnIgnoreUserInfo = false;
            this.m_txtInHospitalCase.m_BlnPartControl = false;
            this.m_txtInHospitalCase.m_BlnReadOnly = false;
            this.m_txtInHospitalCase.m_BlnUnderLineDST = false;
            this.m_txtInHospitalCase.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtInHospitalCase.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtInHospitalCase.m_IntCanModifyTime = 6;
            this.m_txtInHospitalCase.m_IntPartControlLength = 0;
            this.m_txtInHospitalCase.m_IntPartControlStartIndex = 0;
            this.m_txtInHospitalCase.m_StrUserID = "";
            this.m_txtInHospitalCase.m_StrUserName = "";
            this.m_txtInHospitalCase.Name = "m_txtInHospitalCase";
            this.m_txtInHospitalCase.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtInHospitalCase.Size = new System.Drawing.Size(725, 63);
            this.m_txtInHospitalCase.TabIndex = 150;
            this.m_txtInHospitalCase.Text = "";
            // 
            // m_txtInHospitalBy
            // 
            this.m_txtInHospitalBy.AccessibleDescription = "���ƾ���";
            this.m_txtInHospitalBy.BackColor = System.Drawing.Color.White;
            this.m_txtInHospitalBy.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtInHospitalBy.ForeColor = System.Drawing.Color.White;
            this.m_txtInHospitalBy.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtInHospitalBy.Location = new System.Drawing.Point(75, 307);
            this.m_txtInHospitalBy.m_BlnIgnoreUserInfo = false;
            this.m_txtInHospitalBy.m_BlnPartControl = false;
            this.m_txtInHospitalBy.m_BlnReadOnly = false;
            this.m_txtInHospitalBy.m_BlnUnderLineDST = false;
            this.m_txtInHospitalBy.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtInHospitalBy.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtInHospitalBy.m_IntCanModifyTime = 6;
            this.m_txtInHospitalBy.m_IntPartControlLength = 0;
            this.m_txtInHospitalBy.m_IntPartControlStartIndex = 0;
            this.m_txtInHospitalBy.m_StrUserID = "";
            this.m_txtInHospitalBy.m_StrUserName = "";
            this.m_txtInHospitalBy.Name = "m_txtInHospitalBy";
            this.m_txtInHospitalBy.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtInHospitalBy.Size = new System.Drawing.Size(724, 102);
            this.m_txtInHospitalBy.TabIndex = 160;
            this.m_txtInHospitalBy.Text = "";
            // 
            // m_txtOutHospitalDiagnose
            // 
            this.m_txtOutHospitalDiagnose.AccessibleDescription = "��Ժ���";
            this.m_txtOutHospitalDiagnose.BackColor = System.Drawing.Color.White;
            this.m_txtOutHospitalDiagnose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtOutHospitalDiagnose.ForeColor = System.Drawing.Color.Black;
            this.m_txtOutHospitalDiagnose.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtOutHospitalDiagnose.Location = new System.Drawing.Point(443, 126);
            this.m_txtOutHospitalDiagnose.m_BlnIgnoreUserInfo = false;
            this.m_txtOutHospitalDiagnose.m_BlnPartControl = false;
            this.m_txtOutHospitalDiagnose.m_BlnReadOnly = false;
            this.m_txtOutHospitalDiagnose.m_BlnUnderLineDST = false;
            this.m_txtOutHospitalDiagnose.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtOutHospitalDiagnose.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtOutHospitalDiagnose.m_IntCanModifyTime = 6;
            this.m_txtOutHospitalDiagnose.m_IntPartControlLength = 0;
            this.m_txtOutHospitalDiagnose.m_IntPartControlStartIndex = 0;
            this.m_txtOutHospitalDiagnose.m_StrUserID = "";
            this.m_txtOutHospitalDiagnose.m_StrUserName = "";
            this.m_txtOutHospitalDiagnose.Name = "m_txtOutHospitalDiagnose";
            this.m_txtOutHospitalDiagnose.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtOutHospitalDiagnose.Size = new System.Drawing.Size(358, 80);
            this.m_txtOutHospitalDiagnose.TabIndex = 140;
            this.m_txtOutHospitalDiagnose.Text = "";
            // 
            // m_txtInHospitalDiagnose
            // 
            this.m_txtInHospitalDiagnose.AccessibleDescription = "��Ժ���";
            this.m_txtInHospitalDiagnose.BackColor = System.Drawing.Color.White;
            this.m_txtInHospitalDiagnose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtInHospitalDiagnose.ForeColor = System.Drawing.Color.Black;
            this.m_txtInHospitalDiagnose.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtInHospitalDiagnose.Location = new System.Drawing.Point(75, 126);
            this.m_txtInHospitalDiagnose.m_BlnIgnoreUserInfo = false;
            this.m_txtInHospitalDiagnose.m_BlnPartControl = false;
            this.m_txtInHospitalDiagnose.m_BlnReadOnly = false;
            this.m_txtInHospitalDiagnose.m_BlnUnderLineDST = false;
            this.m_txtInHospitalDiagnose.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtInHospitalDiagnose.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtInHospitalDiagnose.m_IntCanModifyTime = 6;
            this.m_txtInHospitalDiagnose.m_IntPartControlLength = 0;
            this.m_txtInHospitalDiagnose.m_IntPartControlStartIndex = 0;
            this.m_txtInHospitalDiagnose.m_StrUserID = "";
            this.m_txtInHospitalDiagnose.m_StrUserName = "";
            this.m_txtInHospitalDiagnose.Name = "m_txtInHospitalDiagnose";
            this.m_txtInHospitalDiagnose.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtInHospitalDiagnose.Size = new System.Drawing.Size(288, 80);
            this.m_txtInHospitalDiagnose.TabIndex = 130;
            this.m_txtInHospitalDiagnose.Text = "";
            // 
            // lblOutHospitalReasonTitle
            // 
            this.lblOutHospitalReasonTitle.AutoSize = true;
            this.lblOutHospitalReasonTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblOutHospitalReasonTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOutHospitalReasonTitle.ForeColor = System.Drawing.Color.Black;
            this.lblOutHospitalReasonTitle.Location = new System.Drawing.Point(4, 423);
            this.lblOutHospitalReasonTitle.Name = "lblOutHospitalReasonTitle";
            this.lblOutHospitalReasonTitle.Size = new System.Drawing.Size(252, 14);
            this.lblOutHospitalReasonTitle.TabIndex = 29187;
            this.lblOutHospitalReasonTitle.Text = "��Ժ���:��������Ҫ���������ƽ����";
            // 
            // lblNoticeTitle
            // 
            this.lblNoticeTitle.AutoSize = true;
            this.lblNoticeTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblNoticeTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNoticeTitle.ForeColor = System.Drawing.Color.Black;
            this.lblNoticeTitle.Location = new System.Drawing.Point(4, 536);
            this.lblNoticeTitle.Name = "lblNoticeTitle";
            this.lblNoticeTitle.Size = new System.Drawing.Size(70, 14);
            this.lblNoticeTitle.TabIndex = 29192;
            this.lblNoticeTitle.Text = "��Ժҽ��:";
            // 
            // m_txtOutHospitalAdvice
            // 
            this.m_txtOutHospitalAdvice.AccessibleDescription = "��Ժҽ��";
            this.m_txtOutHospitalAdvice.BackColor = System.Drawing.Color.White;
            this.m_txtOutHospitalAdvice.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtOutHospitalAdvice.ForeColor = System.Drawing.Color.White;
            this.m_txtOutHospitalAdvice.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtOutHospitalAdvice.Location = new System.Drawing.Point(75, 537);
            this.m_txtOutHospitalAdvice.m_BlnIgnoreUserInfo = false;
            this.m_txtOutHospitalAdvice.m_BlnPartControl = false;
            this.m_txtOutHospitalAdvice.m_BlnReadOnly = false;
            this.m_txtOutHospitalAdvice.m_BlnUnderLineDST = false;
            this.m_txtOutHospitalAdvice.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtOutHospitalAdvice.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtOutHospitalAdvice.m_IntCanModifyTime = 6;
            this.m_txtOutHospitalAdvice.m_IntPartControlLength = 0;
            this.m_txtOutHospitalAdvice.m_IntPartControlStartIndex = 0;
            this.m_txtOutHospitalAdvice.m_StrUserID = "";
            this.m_txtOutHospitalAdvice.m_StrUserName = "";
            this.m_txtOutHospitalAdvice.Name = "m_txtOutHospitalAdvice";
            this.m_txtOutHospitalAdvice.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtOutHospitalAdvice.Size = new System.Drawing.Size(726, 112);
            this.m_txtOutHospitalAdvice.TabIndex = 180;
            this.m_txtOutHospitalAdvice.Text = "";
            // 
            // m_txtOutHospitalCase
            // 
            this.m_txtOutHospitalCase.AccessibleDescription = "��Ժ���";
            this.m_txtOutHospitalCase.BackColor = System.Drawing.Color.White;
            this.m_txtOutHospitalCase.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtOutHospitalCase.ForeColor = System.Drawing.Color.White;
            this.m_txtOutHospitalCase.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtOutHospitalCase.Location = new System.Drawing.Point(75, 445);
            this.m_txtOutHospitalCase.m_BlnIgnoreUserInfo = false;
            this.m_txtOutHospitalCase.m_BlnPartControl = false;
            this.m_txtOutHospitalCase.m_BlnReadOnly = false;
            this.m_txtOutHospitalCase.m_BlnUnderLineDST = false;
            this.m_txtOutHospitalCase.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtOutHospitalCase.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtOutHospitalCase.m_IntCanModifyTime = 6;
            this.m_txtOutHospitalCase.m_IntPartControlLength = 0;
            this.m_txtOutHospitalCase.m_IntPartControlStartIndex = 0;
            this.m_txtOutHospitalCase.m_StrUserID = "";
            this.m_txtOutHospitalCase.m_StrUserName = "";
            this.m_txtOutHospitalCase.Name = "m_txtOutHospitalCase";
            this.m_txtOutHospitalCase.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtOutHospitalCase.Size = new System.Drawing.Size(725, 78);
            this.m_txtOutHospitalCase.TabIndex = 170;
            this.m_txtOutHospitalCase.Text = "";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 0;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 100;
            // 
            // m_txtDoctorSign
            // 
            this.m_txtDoctorSign.AccessibleName = "NoDefault";
            this.m_txtDoctorSign.BackColor = System.Drawing.Color.White;
            this.m_txtDoctorSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDoctorSign.ForeColor = System.Drawing.Color.Black;
            this.m_txtDoctorSign.Location = new System.Drawing.Point(622, 97);
            this.m_txtDoctorSign.Name = "m_txtDoctorSign";
            this.m_txtDoctorSign.ReadOnly = true;
            this.m_txtDoctorSign.Size = new System.Drawing.Size(169, 23);
            this.m_txtDoctorSign.TabIndex = 190;
            // 
            // cmdConfirm
            // 
            this.cmdConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdConfirm.DefaultScheme = true;
            this.cmdConfirm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdConfirm.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdConfirm.Hint = "";
            this.cmdConfirm.Location = new System.Drawing.Point(628, 459);
            this.cmdConfirm.Name = "cmdConfirm";
            this.cmdConfirm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdConfirm.Size = new System.Drawing.Size(80, 30);
            this.cmdConfirm.TabIndex = 300;
            this.cmdConfirm.Text = "ȷ��";
            this.cmdConfirm.Visible = false;
            this.cmdConfirm.Click += new System.EventHandler(this.cmdConfirm_Click);
            // 
            // lblHeart
            // 
            this.lblHeart.AutoSize = true;
            this.lblHeart.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHeart.ForeColor = System.Drawing.Color.Black;
            this.lblHeart.Location = new System.Drawing.Point(518, 36);
            this.lblHeart.Name = "lblHeart";
            this.lblHeart.Size = new System.Drawing.Size(70, 14);
            this.lblHeart.TabIndex = 29200;
            this.lblHeart.Text = "�ĵ�ͼ��:";
            // 
            // lblXRay
            // 
            this.lblXRay.AutoSize = true;
            this.lblXRay.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblXRay.Location = new System.Drawing.Point(533, 61);
            this.lblXRay.Name = "lblXRay";
            this.lblXRay.Size = new System.Drawing.Size(56, 14);
            this.lblXRay.TabIndex = 29202;
            this.lblXRay.Text = "X��� :";
            // 
            // lblEmployeeIDTitle
            // 
            this.lblEmployeeIDTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEmployeeIDTitle.AutoSize = true;
            this.lblEmployeeIDTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEmployeeIDTitle.Location = new System.Drawing.Point(408, 105);
            this.lblEmployeeIDTitle.Name = "lblEmployeeIDTitle";
            this.lblEmployeeIDTitle.Size = new System.Drawing.Size(70, 14);
            this.lblEmployeeIDTitle.TabIndex = 29203;
            this.lblEmployeeIDTitle.Text = "��    ��:";
            this.lblEmployeeIDTitle.Visible = false;
            // 
            // lblEmployeeID
            // 
            this.lblEmployeeID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEmployeeID.AutoSize = true;
            this.lblEmployeeID.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEmployeeID.Location = new System.Drawing.Point(484, 105);
            this.lblEmployeeID.Name = "lblEmployeeID";
            this.lblEmployeeID.Size = new System.Drawing.Size(0, 14);
            this.lblEmployeeID.TabIndex = 29204;
            this.lblEmployeeID.Visible = false;
            // 
            // m_txtHeartID
            // 
            this.m_txtHeartID.BackColor = System.Drawing.Color.White;
            this.m_txtHeartID.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtHeartID.ForeColor = System.Drawing.Color.Black;
            this.m_txtHeartID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtHeartID.Location = new System.Drawing.Point(591, 33);
            this.m_txtHeartID.m_BlnIgnoreUserInfo = false;
            this.m_txtHeartID.m_BlnPartControl = false;
            this.m_txtHeartID.m_BlnReadOnly = false;
            this.m_txtHeartID.m_BlnUnderLineDST = false;
            this.m_txtHeartID.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtHeartID.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtHeartID.m_IntCanModifyTime = 6;
            this.m_txtHeartID.m_IntPartControlLength = 0;
            this.m_txtHeartID.m_IntPartControlStartIndex = 0;
            this.m_txtHeartID.m_StrUserID = "";
            this.m_txtHeartID.m_StrUserName = "";
            this.m_txtHeartID.Multiline = false;
            this.m_txtHeartID.Name = "m_txtHeartID";
            this.m_txtHeartID.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.m_txtHeartID.Size = new System.Drawing.Size(116, 21);
            this.m_txtHeartID.TabIndex = 101;
            this.m_txtHeartID.Text = "";
            // 
            // m_txtXRayID
            // 
            this.m_txtXRayID.BackColor = System.Drawing.Color.White;
            this.m_txtXRayID.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtXRayID.ForeColor = System.Drawing.Color.White;
            this.m_txtXRayID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtXRayID.Location = new System.Drawing.Point(592, 58);
            this.m_txtXRayID.m_BlnIgnoreUserInfo = false;
            this.m_txtXRayID.m_BlnPartControl = false;
            this.m_txtXRayID.m_BlnReadOnly = false;
            this.m_txtXRayID.m_BlnUnderLineDST = false;
            this.m_txtXRayID.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtXRayID.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtXRayID.m_IntCanModifyTime = 6;
            this.m_txtXRayID.m_IntPartControlLength = 0;
            this.m_txtXRayID.m_IntPartControlStartIndex = 0;
            this.m_txtXRayID.m_StrUserID = "";
            this.m_txtXRayID.m_StrUserName = "";
            this.m_txtXRayID.Multiline = false;
            this.m_txtXRayID.Name = "m_txtXRayID";
            this.m_txtXRayID.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.m_txtXRayID.Size = new System.Drawing.Size(116, 21);
            this.m_txtXRayID.TabIndex = 102;
            this.m_txtXRayID.Text = "";
            // 
            // m_cmdClose
            // 
            this.m_cmdClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdClose.DefaultScheme = true;
            this.m_cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdClose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdClose.Hint = "";
            this.m_cmdClose.Location = new System.Drawing.Point(711, 459);
            this.m_cmdClose.Name = "m_cmdClose";
            this.m_cmdClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdClose.Size = new System.Drawing.Size(80, 30);
            this.m_cmdClose.TabIndex = 301;
            this.m_cmdClose.Text = "ȡ��";
            this.m_cmdClose.Visible = false;
            this.m_cmdClose.Click += new System.EventHandler(this.m_cmdClose_Click);
            // 
            // lblEmployeeSign
            // 
            this.lblEmployeeSign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEmployeeSign.AutoSize = true;
            this.lblEmployeeSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEmployeeSign.Location = new System.Drawing.Point(32, 105);
            this.lblEmployeeSign.Name = "lblEmployeeSign";
            this.lblEmployeeSign.Size = new System.Drawing.Size(42, 14);
            this.lblEmployeeSign.TabIndex = 10000036;
            this.lblEmployeeSign.Text = "ǩ��:";
            this.lblEmployeeSign.Visible = false;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Width = 0;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Width = 100;
            // 
            // m_cmdMainDoctorSign
            // 
            this.m_cmdMainDoctorSign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdMainDoctorSign.DefaultScheme = true;
            this.m_cmdMainDoctorSign.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdMainDoctorSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdMainDoctorSign.ForeColor = System.Drawing.Color.Black;
            this.m_cmdMainDoctorSign.Hint = "";
            this.m_cmdMainDoctorSign.Location = new System.Drawing.Point(296, 96);
            this.m_cmdMainDoctorSign.Name = "m_cmdMainDoctorSign";
            this.m_cmdMainDoctorSign.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdMainDoctorSign.Size = new System.Drawing.Size(72, 24);
            this.m_cmdMainDoctorSign.TabIndex = 105;
            this.m_cmdMainDoctorSign.Text = "����ҽʦ:";
            // 
            // m_cmdDoctorSign
            // 
            this.m_cmdDoctorSign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdDoctorSign.DefaultScheme = true;
            this.m_cmdDoctorSign.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdDoctorSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdDoctorSign.Hint = "";
            this.m_cmdDoctorSign.Location = new System.Drawing.Point(543, 96);
            this.m_cmdDoctorSign.Name = "m_cmdDoctorSign";
            this.m_cmdDoctorSign.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDoctorSign.Size = new System.Drawing.Size(77, 25);
            this.m_cmdDoctorSign.TabIndex = 185;
            this.m_cmdDoctorSign.Tag = "1";
            this.m_cmdDoctorSign.Text = "ҽʦǩ��:";
            // 
            // frmOutHospital
            // 
            this.AccessibleDescription = "��Ժ��¼";
            this.CancelButton = this.m_cmdClose;
            this.ClientSize = new System.Drawing.Size(804, 689);
            this.Controls.Add(this.lblNoticeTitle);
            this.Controls.Add(this.m_txtOutHospitalAdvice);
            this.Controls.Add(this.m_txtInHospitalBy);
            this.Controls.Add(this.lblEmployeeSign);
            this.Controls.Add(this.lblCaseHistoryTitle);
            this.Controls.Add(this.lblEmployeeIDTitle);
            this.Controls.Add(this.lblOriginalDiagnoseTitle);
            this.Controls.Add(this.lblInhospitalCase);
            this.Controls.Add(this.lblInHospitalDateTitle);
            this.Controls.Add(this.lblEmployeeID);
            this.Controls.Add(this.m_txtOutHospitalCase);
            this.Controls.Add(this.m_cmdClose);
            this.Controls.Add(this.lblOutHospitalReasonTitle);
            this.Controls.Add(this.m_txtMainDoctorSign);
            this.Controls.Add(this.cmdConfirm);
            this.Controls.Add(this.lblOutHospitalDiagnoseTitle);
            this.Controls.Add(this.m_lblInHospitalDate);
            this.Controls.Add(this.m_txtOutHospitalDiagnose);
            this.Controls.Add(this.m_txtInHospitalDiagnose);
            this.Controls.Add(this.m_dtpOutHospitalDate);
            this.Controls.Add(this.m_cmdMainDoctorSign);
            this.Controls.Add(this.m_txtDoctorSign);
            this.Controls.Add(this.m_cmdDoctorSign);
            this.Controls.Add(this.lblOutHospitalDate);
            this.Controls.Add(this.m_txtInHospitalCase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOutHospital";
            this.Text = "��Ժ��¼";
            this.Load += new System.EventHandler(this.frmOutHospital_Load);
            this.Controls.SetChildIndex(this.m_txtInHospitalCase, 0);
            this.Controls.SetChildIndex(this.lblOutHospitalDate, 0);
            this.Controls.SetChildIndex(this.m_lblGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_cmdDoctorSign, 0);
            this.Controls.SetChildIndex(this.m_txtDoctorSign, 0);
            this.Controls.SetChildIndex(this.m_cmdMainDoctorSign, 0);
            this.Controls.SetChildIndex(this.m_dtpOutHospitalDate, 0);
            this.Controls.SetChildIndex(this.m_txtInHospitalDiagnose, 0);
            this.Controls.SetChildIndex(this.m_txtOutHospitalDiagnose, 0);
            this.Controls.SetChildIndex(this.m_lblInHospitalDate, 0);
            this.Controls.SetChildIndex(this.m_dtpGetDataTime, 0);
            this.Controls.SetChildIndex(this.lblOutHospitalDiagnoseTitle, 0);
            this.Controls.SetChildIndex(this.cmdConfirm, 0);
            this.Controls.SetChildIndex(this.m_txtMainDoctorSign, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.lblOutHospitalReasonTitle, 0);
            this.Controls.SetChildIndex(this.m_cmdClose, 0);
            this.Controls.SetChildIndex(this.m_txtOutHospitalCase, 0);
            this.Controls.SetChildIndex(this.lblEmployeeID, 0);
            this.Controls.SetChildIndex(this.lblInHospitalDateTitle, 0);
            this.Controls.SetChildIndex(this.lblInhospitalCase, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.lblOriginalDiagnoseTitle, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.lblEmployeeIDTitle, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.lblCaseHistoryTitle, 0);
            this.Controls.SetChildIndex(this.lblEmployeeSign, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.m_txtInHospitalBy, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_trvCreateDate, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.lblCreateDateTitle, 0);
            this.Controls.SetChildIndex(this.m_dtpCreateDate, 0);
            this.Controls.SetChildIndex(this.m_txtOutHospitalAdvice, 0);
            this.Controls.SetChildIndex(this.lblNoticeTitle, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.m_pnlNewBase.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// ��ȡ��ǰ�����ⲡ�̼�¼��Ϣ
        /// </summary>
        /// <returns></returns>
        public override clsDiseaseTrackInfo m_objGetDiseaseTrackInfo()
        {
            clsOutHospitalInfo objTrackInfo = new clsOutHospitalInfo();

            objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;//m_objGetContentFromGUI();
            //����m_strTitle��m_dtmRecordTime
            objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
            objTrackInfo.m_StrTitle = "��Ժ��¼";

            return objTrackInfo;
        }

        /// <summary>
        /// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
        /// </summary>
        protected override void m_mthClearRecordInfo()
        {
            //��վ����¼����	
            m_lblInHospitalDate.Text = "";// m_objBaseCurrentPatient.m_DtmSelectedHISInDate.ToString("yyyy��MM��dd�� HH:mm:ss");
            m_strCurrentOpenDate = "";

            m_txtHeartID.m_mthClearText();
            m_txtXRayID.m_mthClearText();
            m_txtInHospitalDiagnose.m_mthClearText();
            m_txtOutHospitalDiagnose.m_mthClearText();
            m_txtInHospitalCase.m_mthClearText();
            m_txtInHospitalBy.m_mthClearText();
            m_txtOutHospitalCase.m_mthClearText();
            m_txtOutHospitalAdvice.m_mthClearText();

            m_txtMainDoctorSign.Text = "";
            m_txtMainDoctorSign.Tag = null;
            m_txtDoctorSign.Enabled = true;
            m_txtDoctorSign.Text = "";
            m_txtDoctorSign.Tag = null;
            lblEmployeeID.Text = "";
        }

        /// <summary>
        /// �����Ƿ����ѡ���˺ͼ�¼ʱ���б��ڴӲ��̼�¼�������ʱ��Ҫʹ�á�
        /// </summary>
        /// <param name="p_blnEnable"></param>
        protected override void m_mthEnablePatientSelectSub(bool p_blnEnable)
        {
            m_blnIsMainWindow = false;
            this.MaximizeBox = false;
            if (p_blnEnable == false)
            {
                foreach (Control control in this.Controls)
                {
                    control.Top = control.Top - 100;
                }

                cmdConfirm.Visible = true;
                m_cmdClose.Visible = true;

                int intLeft = 248;
                lblInHospitalDateTitle.Left -= intLeft;
                lblOutHospitalDate.Left -= intLeft;
                lblCreateDateTitle.Left -= intLeft;
                m_lblInHospitalDate.Left -= intLeft;
                m_dtpCreateDate.Left -= intLeft;
                m_dtpOutHospitalDate.Left -= intLeft;
                lblHeart.Left -= intLeft;
                lblXRay.Left -= intLeft;
                m_cmdMainDoctorSign.Left -= intLeft;
                m_txtHeartID.Left -= intLeft;
                m_txtXRayID.Left -= intLeft;
                m_txtMainDoctorSign.Left -= intLeft;

                this.Size = new Size(this.Size.Width, this.Size.Height - 110);
                this.CenterToParent();
            }
            m_intFormID = 15;
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
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                return null;

            //�ӽ����ȡ��ֵ
            clsOutHospitalRecordContent objContent = new clsOutHospitalRecordContent();
            objContent.m_dtmCreateDate = m_dtpCreateDate.Value;
            #region �Ƿ�����޺ۼ��޸�
            if (chkModifyWithoutMatk.Checked)
                objContent.m_intMarkStatus = 0;
            else
                objContent.m_intMarkStatus = 1;
            #endregion
            if (m_dtpOutHospitalDate.Visible && m_dtpOutHospitalDate.Enabled)
            {
                objContent.m_dtmOutHospitalDate = m_dtpOutHospitalDate.Value;
            }
            else
            {
                objContent.m_dtmOutHospitalDate = DateTime.MinValue;
            }

            objContent.m_strHeartID_Right = m_txtHeartID.m_strGetRightText();
            objContent.m_strHeartID = m_txtHeartID.Text;
            objContent.m_strHeartIDXML = m_txtHeartID.m_strGetXmlText();

            objContent.m_strXRayID_Right = m_txtXRayID.m_strGetRightText();
            objContent.m_strXRayID = m_txtXRayID.Text;
            objContent.m_strXRayIDXML = m_txtXRayID.m_strGetXmlText();

            objContent.m_strInHospitalDiagnose_Right = m_txtInHospitalDiagnose.m_strGetRightText();
            objContent.m_strInHospitalDiagnose = m_txtInHospitalDiagnose.Text;
            objContent.m_strInHospitalDiagnoseXML = m_txtInHospitalDiagnose.m_strGetXmlText();

            objContent.m_strOutHospitalDiagnose_Right = m_txtOutHospitalDiagnose.m_strGetRightText();
            objContent.m_strOutHospitalDiagnose = m_txtOutHospitalDiagnose.Text;
            objContent.m_strOutHospitalDiagnoseXML = m_txtOutHospitalDiagnose.m_strGetXmlText();

            objContent.m_strInHospitalCase_Right = m_txtInHospitalCase.m_strGetRightText();
            objContent.m_strInHospitalCase = m_txtInHospitalCase.Text;
            objContent.m_strInHospitalCaseXML = m_txtInHospitalCase.m_strGetXmlText();

            objContent.m_strInHospitalBy_Right = m_txtInHospitalBy.m_strGetRightText();
            objContent.m_strInHospitalBy = m_txtInHospitalBy.Text;
            objContent.m_strInHospitalByXML = m_txtInHospitalBy.m_strGetXmlText();

            objContent.m_strOutHospitalCase_Right = m_txtOutHospitalCase.m_strGetRightText();
            objContent.m_strOutHospitalCase = m_txtOutHospitalCase.Text;
            objContent.m_strOutHospitalCaseXML = m_txtOutHospitalCase.m_strGetXmlText();

            objContent.m_strOutHospitalAdvice_Right = m_txtOutHospitalAdvice.m_strGetRightText();
            objContent.m_strOutHospitalAdvice = m_txtOutHospitalAdvice.Text;
            objContent.m_strOutHospitalAdviceXML = m_txtOutHospitalAdvice.m_strGetXmlText();
            objContent.m_strModifyUserID = clsEMRLogin.LoginInfo.m_strEmpNo;

            if (m_txtMainDoctorSign.Tag != null && m_txtMainDoctorSign.Text.Trim() != "")
            {
                objContent.m_strMainDoctorID = ((clsEmrEmployeeBase_VO)m_txtMainDoctorSign.Tag).m_strEMPNO_CHR.Trim();
                objContent.m_strMainDoctorName = ((clsEmrEmployeeBase_VO)m_txtMainDoctorSign.Tag).m_strLASTNAME_VCHR;
            }
            else
            {
                objContent.m_strMainDoctorID = "";
                objContent.m_strMainDoctorName = "";
                //				clsPublicFunction.ShowInformationMessageBox("��������ҽʦǩ��!");
                //				return null;
            }

            if (m_txtDoctorSign.Tag != null && m_txtDoctorSign.Text.Trim() != "")
            {
                objContent.m_strDoctorID = ((clsEmrEmployeeBase_VO)m_txtDoctorSign.Tag).m_strEMPNO_CHR.Trim();
                objContent.m_strDoctorName = ((clsEmrEmployeeBase_VO)m_txtDoctorSign.Tag).m_strLASTNAME_VCHR;
            }
            else
            {
                clsPublicFunction.ShowInformationMessageBox("���뾭��ҽʦǩ��!");
                return null;
            }
            if (m_txtOutHospitalDiagnose.Text.Trim() == "")
            {
                clsPublicFunction.ShowInformationMessageBox("��Ժ������ݲ���Ϊ��");
                return null;
            }

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
            clsOutHospitalRecordContent objContent = (clsOutHospitalRecordContent)p_objContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            m_strCurrentOpenDate = objContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime dtmOut = m_dtmOutHospitalDate;
            m_dtpOutHospitalDate.Enabled = false;
            if (clsEMRLogin.m_StrCurrentHospitalNO != "450101001")
            {
                if (objContent.m_dtmOutHospitalDate != DateTime.MinValue)
                {
                    dtmOut = objContent.m_dtmOutHospitalDate;
                }
                else
                {
                    //clsPatientManagerService svc = (clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPatientManagerService));
                    DateTime? dtmTmp = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetOutHospitalDate2(objContent.m_strInPatientID, objContent.m_dtmInPatientDate);
                    if (dtmTmp != null)
                    {
                        dtmOut = dtmTmp.Value;
                    }
                    //svc = null;
                }
            }
            else
            {
                dtmOut = m_ObjCurrentEmrPatientSession.m_dtmOutDate;
            }
            if (dtmOut != DateTime.MinValue && dtmOut != new DateTime(1900, 1, 1))
            {
                m_dtpOutHospitalDate.Value = dtmOut;
                m_dtpOutHospitalDate.Visible = true;
                if (clsEMRLogin.m_StrCurrentHospitalNO != "450101001")
                    m_dtpOutHospitalDate.Enabled = true;
            }
            else
            {
                m_dtpOutHospitalDate.Visible = false;
            }
            m_dtmOutHospitalDate = dtmOut;

            //			m_dtpOutHospitalDate.Value = objContent.m_dtmOutHospitalDate;
            m_txtHeartID.m_mthSetNewText(objContent.m_strHeartID, objContent.m_strHeartIDXML);
            m_txtXRayID.m_mthSetNewText(objContent.m_strXRayID, objContent.m_strXRayIDXML);
            m_txtInHospitalDiagnose.m_mthSetNewText(objContent.m_strInHospitalDiagnose, objContent.m_strInHospitalDiagnoseXML);
            m_txtOutHospitalDiagnose.m_mthSetNewText(objContent.m_strOutHospitalDiagnose, objContent.m_strOutHospitalDiagnoseXML);
            m_txtInHospitalCase.m_mthSetNewText(objContent.m_strInHospitalCase, objContent.m_strInHospitalCaseXML);
            m_txtInHospitalBy.m_mthSetNewText(objContent.m_strInHospitalBy, objContent.m_strInHospitalByXML);
            m_txtOutHospitalCase.m_mthSetNewText(objContent.m_strOutHospitalCase, objContent.m_strOutHospitalCaseXML);
            m_txtOutHospitalAdvice.m_mthSetNewText(objContent.m_strOutHospitalAdvice, objContent.m_strOutHospitalAdviceXML);

            #region ǩ��
            clsEmrEmployeeBase_VO objEmpVO = new clsEmrEmployeeBase_VO();
            objEmployeeSign.m_lngGetEmpByNO(objContent.m_strMainDoctorID, out objEmpVO);
            if (objEmpVO != null)
            {
                m_txtMainDoctorSign.Tag = objEmpVO;
                m_txtMainDoctorSign.Text = objEmpVO.m_strGetTechnicalRankAndName;
            }

            objEmployeeSign.m_lngGetEmpByNO(objContent.m_strDoctorID, out objEmpVO);
            if (objEmpVO != null)
            {
                m_txtDoctorSign.Tag = objEmpVO;
                m_txtDoctorSign.Text = objEmpVO.m_strGetTechnicalRankAndName;
                m_txtDoctorSign.Enabled = false;
            }

            lblEmployeeID.Text = objContent.m_strDoctorID;
            #endregion ǩ��

            //			#region ��Ժԭ��(����)
            //			clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr= new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss"));
            //			if(objInPatientCaseHisoryDefaultValueArr !=null && objInPatientCaseHisoryDefaultValueArr.Length>0)
            //			{
            //				m_lblInHospitalReason.Text=objInPatientCaseHisoryDefaultValueArr[0].m_strMainDescription;
            //			}
            //			#endregion ��Ժԭ��(����)

        }

        /// <summary>
        /// ��ȡ��ǰ���˵���������
        /// </summary>
        /// <param name="p_dtmRecordDate">��¼���ڣ��˴���ʾCreateDate</param>
        /// <param name="p_intFormID">����ID</param>
        protected override void m_mthGetDeactiveContent(DateTime p_dtmRecordDate, int p_intFormID)
        {
            if (m_objBaseCurrentPatient == null || m_objBaseCurrentPatient.m_StrInPatientID == null || m_objBaseCurrentPatient.m_DtmSelectedInDate == DateTime.MinValue || p_dtmRecordDate == DateTime.MinValue)
            {
                clsPublicFunction.ShowInformationMessageBox("��������");
                return;
            }

            clsTrackRecordContent objContent = null;
            long lngRes = m_objDiseaseTrackDomain.m_lngGetDeleteRecordContent(m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), p_dtmRecordDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);
            if (lngRes > 0 && objContent != null)
            {
                m_mthSetDeletedGUIFromContent(objContent);
            }

        }

        private int m_intFormID = 27;
        public override int m_IntFormID
        {
            get
            {
                return m_intFormID;
            }
        }

        protected override void m_mthSetDeletedGUIFromContent(clsTrackRecordContent p_objContent)
        {
            if (p_objContent == null)
                return;
            clsOutHospitalRecordContent objContent = (clsOutHospitalRecordContent)p_objContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            //			m_dtpOutHospitalDate.Value = objContent.m_dtmOutHospitalDate;
            m_mthGetSetlectedOutDate();
            if (m_dtmOutHospitalDate == new DateTime(1900, 1, 1) || m_dtmOutHospitalDate == DateTime.MinValue)
                m_dtpOutHospitalDate.Visible = false;
            else
            {
                m_dtpOutHospitalDate.Visible = true;
                m_dtpOutHospitalDate.Value = m_dtmOutHospitalDate;
                if (clsEMRLogin.m_StrCurrentHospitalNO != "450101001")//������
                {
                    m_dtpOutHospitalDate.Enabled = true;
                }
                else
                {
                    m_dtpOutHospitalDate.Enabled = false;
                }
            }

            m_txtHeartID.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strHeartID, objContent.m_strHeartIDXML);
            m_txtXRayID.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strXRayID, objContent.m_strXRayIDXML);
            m_txtInHospitalDiagnose.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strInHospitalDiagnose, objContent.m_strInHospitalDiagnoseXML);
            m_txtOutHospitalDiagnose.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOutHospitalDiagnose, objContent.m_strOutHospitalDiagnoseXML);
            m_txtInHospitalCase.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strInHospitalCase, objContent.m_strInHospitalCaseXML);
            m_txtInHospitalBy.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strInHospitalBy, objContent.m_strInHospitalByXML);
            m_txtOutHospitalCase.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOutHospitalCase, objContent.m_strOutHospitalCaseXML);
            m_txtOutHospitalAdvice.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOutHospitalAdvice, objContent.m_strOutHospitalAdviceXML);

        }

        /// <summary>
        /// ��ȡ���̼�¼�������ʵ��
        /// </summary>
        /// <returns></returns>
        protected override clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
        {
            //��ȡ���̼�¼�������ʵ��
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.OutHospital);
        }

        /// <summary>
        /// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
        {
            //��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
            clsOutHospitalRecordContent objContent = (clsOutHospitalRecordContent)p_objRecordContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��

            m_txtHeartID.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strHeartID, objContent.m_strHeartIDXML);
            m_txtXRayID.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strXRayID, objContent.m_strXRayIDXML);
            m_txtInHospitalDiagnose.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strInHospitalDiagnose, objContent.m_strInHospitalDiagnoseXML);
            m_txtOutHospitalDiagnose.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOutHospitalDiagnose, objContent.m_strOutHospitalDiagnoseXML);
            m_txtInHospitalCase.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strInHospitalCase, objContent.m_strInHospitalCaseXML);
            m_txtInHospitalBy.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strInHospitalBy, objContent.m_strInHospitalByXML);
            m_txtOutHospitalCase.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOutHospitalCase, objContent.m_strOutHospitalCaseXML);
            m_txtOutHospitalAdvice.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOutHospitalAdvice, objContent.m_strOutHospitalAdviceXML);

        }

        // ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
        public override string m_strReloadFormTitle()
        {
            //���Ӵ�������ʵ��
            return "��Ժ��¼";
        }

        /// <summary>
        /// ��ѡ����ڵ�ʱ,���������Ĭ��ֵ(���Ӵ�����Ҫ,������ʵ��)
        /// </summary>
        protected override void m_mthSelectRootNode()
        {
            #region �������Ĭ��ֵ
            if (m_objCurrentPatient != null && m_ObjCurrentEmrPatientSession != null)
            {
                DateTime dtmInDate = m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate;
                if (m_ObjLastEmrPatientSession != null)
                {
                    dtmInDate = m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate;
                }
                clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID, dtmInDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (objInPatientCaseHisoryDefaultValueArr != null && objInPatientCaseHisoryDefaultValueArr.Length > 0)
                {
                    m_txtInHospitalDiagnose.Text = objInPatientCaseHisoryDefaultValueArr[0].m_strPrimaryDiagnose;
                }
            }
            #endregion �������Ĭ��ֵ
        }

        #region ҽʦǩ��
        private void m_mthEvent_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13:// enter
                    break;

                case 38:
                case 40:
                    break;
                case 113://save
                    this.Save();
                    break;
                case 114://del
                    this.Delete();
                    break;
                case 115://print
                    this.Print();
                    break;
                case 116://refresh
                    m_mthClearUp();
                    break;
                case 117://Search					
                    break;
            }
        }

        #endregion ҽʦǩ��

        private void cmdConfirm_Click(object sender, System.EventArgs e)
        {
            if (m_txtOutHospitalDiagnose.Text == "")
                MessageBox.Show("��Ժ������ݲ���Ϊ��");
            return;

            if (m_lngSave() > 0)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private bool m_blnIsMainWindow = true;
        private void frmOutHospital_Load(object sender, System.EventArgs e)
        {
            m_mthfrmLoad();

            m_cmdNewTemplate.Left = cmdConfirm.Left - m_cmdNewTemplate.Width + (cmdConfirm.Right - m_cmdClose.Left);
            m_cmdNewTemplate.Top = cmdConfirm.Top;

            if (m_blnIsMainWindow == true)
                m_mthSetQuickKeys();
            else
            {
                //				m_cmdNewTemplate.Visible=true;
            }

            if (m_objCurrentPatient != null)
            {
                m_lblInHospitalDate.Text = m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmHISInDate.ToString("yyyy��MM��dd�� HH:mm:ss");
                m_dtpOutHospitalDate.Value = (m_objCurrentPatient.m_DtmLastOutDate != new DateTime(1900, 1, 1) && m_objCurrentPatient.m_DtmLastOutDate != m_objCurrentPatient.m_DtmLastInDate) ? m_objCurrentPatient.m_DtmLastOutDate : DateTime.Now;
            }

            this.m_dtpCreateDate.m_EnmVisibleFlag = MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
            this.m_dtpCreateDate.m_mthResetSize();

            m_trvCreateDate.Focus();
        }

        protected override void m_mthSetPatientInHospitalDate(clsPatient p_objSelectedPatient)
        {
            //�жϲ�����Ϣ�Ƿ�Ϊnull������ǣ�ֱ�ӷ��ء�
            if (p_objSelectedPatient == null)
                return;

            //��¼������Ϣ
            m_objCurrentPatient = p_objSelectedPatient;
            m_lblInHospitalDate.Text = m_objCurrentPatient.m_DtmSelectedHISInDate.ToString("yyyy��MM��dd�� HH:mm:ss");
            //			m_dtpOutHospitalDate.Value = (m_objCurrentPatient.m_DtmLastOutDate != new DateTime(1900,1,1) && m_objCurrentPatient.m_DtmLastOutDate!=m_objCurrentPatient.m_DtmLastInDate) ? m_objCurrentPatient.m_DtmLastOutDate : DateTime.Now;
            m_mthGetSetlectedOutDate();
            if (m_dtmOutHospitalDate == new DateTime(1900, 1, 1) || m_dtmOutHospitalDate == DateTime.MinValue)
                m_dtpOutHospitalDate.Visible = false;
            else
            {
                m_dtpOutHospitalDate.Visible = true;
                m_dtpOutHospitalDate.Value = m_dtmOutHospitalDate;
                if (clsEMRLogin.m_StrCurrentHospitalNO != "450101001")//������
                {
                    m_dtpOutHospitalDate.Enabled = true;
                }
                else
                {
                    m_dtpOutHospitalDate.Enabled = false;
                }
            }
        }

        #region �ⲿ��ӡ.

        //System.Drawing.Printing.PrintDocument m_pdcPrintDocument;
        private void m_mthfrmLoad()
        {
            if (m_pdcPrintDocument == null)
                this.m_pdcPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.m_pdcPrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_BeginPrint);
            this.m_pdcPrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_EndPrint);
            this.m_pdcPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdcPrintDocument_PrintPage);
        }
        private void m_pdcPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            objPrintTool.m_mthPrintPage(e);
        }

        private void m_pdcPrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            objPrintTool.m_mthBeginPrint(e);
        }

        private void m_pdcPrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            objPrintTool.m_mthEndPrint(e);
        }

        private bool m_blnHasInitPrintTool = false;
        clsOutHospitalPrintTool objPrintTool;
        private void m_mthDemoPrint_FromDataSource()
        {
            if (m_blnHasInitPrintTool == false)
            {
                objPrintTool = new clsOutHospitalPrintTool();
                objPrintTool.m_mthInitPrintTool(null);
                m_blnHasInitPrintTool = true;
            }
            objPrintTool.m_mthSetOutDateValue(m_dtmOutHospitalDate);
            if (m_objBaseCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, DateTime.MinValue, DateTime.MinValue);
            else
            {
                m_objBaseCurrentPatient.m_StrHISInPatientID = m_ObjCurrentEmrPatientSession.m_strHISInpatientId;
                if (m_objCurrentRecordContent == null)
                    objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate, DateTime.MinValue);
                else
                    objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate, m_objCurrentRecordContent.m_dtmOpenDate);
            }
            objPrintTool.m_mthInitPrintContent();

            m_mthStartPrint_this();
        }

        private void m_mthStartPrint_this()
        {
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

        protected override long m_lngSubPrint()//����ԭ�����е�ͬ����ӡ����
        {
            m_mthDemoPrint_FromDataSource();
            return 1;
        }
        #endregion �ⲿ��ӡ.

        #region ��Ӽ��̿�ݼ�
        private void m_mthSetQuickKeys()
        {
            m_mthSetControlEvent(this);
        }

        private void m_mthSetControlEvent(Control p_ctlControl)
        {
            #region ���õݹ���ã���ȡ���������н����¼�,Jacky-2003-2-21
            string strTypeName = p_ctlControl.GetType().Name;
            if (strTypeName != "Lable" && strTypeName != "Button")
            {
                p_ctlControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);
                if (p_ctlControl.HasChildren && strTypeName != "DataGrid" && strTypeName != "DateTimePicker" && strTypeName != "ctlComboBox")
                {
                    foreach (Control subcontrol in p_ctlControl.Controls)
                    {
                        m_mthSetControlEvent(subcontrol);
                    }
                }
            }
            #endregion
        }

        private void m_mthClearUpInControl(Control p_ctlControl)
        {
            string strTypeName = p_ctlControl.GetType().Name;
            if (strTypeName == "ctlRichTextBox")
                ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_mthClearText();
            else if (strTypeName == "ctlBorderTextBox")
                ((ctlBorderTextBox)p_ctlControl).Text = "";
            else if (strTypeName == "TreeView")
            {
                if (((TreeView)p_ctlControl).Nodes.Count > 0)
                    ((TreeView)p_ctlControl).Nodes[0].Nodes.Clear();
            }
            else if (strTypeName == "ListView")
                ((ListView)p_ctlControl).Items.Clear();
            else if (strTypeName == "DateTimePicker")
                ((DateTimePicker)p_ctlControl).Value = DateTime.Now;

            if (p_ctlControl.HasChildren && strTypeName != "DataGrid" && strTypeName != "DateTimePicker")
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthClearUpInControl(subcontrol);
                }
            }
        }

        private void m_mthClearUp()
        {
            m_mthClearUpInControl(this);
            m_lblInHospitalDate.Text = "";
            m_mthClearPatientBaseInfo();
        }

        #endregion

        private void m_mthEvent_KeyDown1(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (m_blnIsMainWindow)
                return;

            switch (e.KeyValue)
            {
                case 13:// enter

                    break;

                case 38://up
                case 40://down			
                    break;
            }
        }

        private void m_cmdClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        #region ���
        private string m_strCurrentOpenDate = "";
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

                //				if(this.m_trvCreateDate.SelectedNode==null || this.m_trvCreateDate.SelectedNode.Tag==null)
                //				{
                //					clsPublicFunction.ShowInformationMessageBox("����ѡ���¼");
                //					return "";
                //				}
                //				return (string)this.m_trvCreateDate.SelectedNode.Tag;
            }
        }

        protected override bool m_BlnCanApprove
        {
            get
            {
                return true;
            }
        }
        #endregion

        /// <summary>
        /// ���ݸ���
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        protected override void m_mthDataShare(clsPatient p_objSelectedPatient)
        {
            clsInPatientCaseHisoryDefaultValue[] objInPatientCaseDefaultValue = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(p_objSelectedPatient.m_StrInPatientID, p_objSelectedPatient.m_DtmSelectedInDate.ToString());
            if (objInPatientCaseDefaultValue != null && objInPatientCaseDefaultValue.Length > 0)
            {
                this.m_txtInHospitalDiagnose.Text = objInPatientCaseDefaultValue[0].m_strPrimaryDiagnose;
                this.m_txtOutHospitalDiagnose.Text = objInPatientCaseDefaultValue[0].m_strFinallyDiagnose != "" ? objInPatientCaseDefaultValue[0].m_strFinallyDiagnose : objInPatientCaseDefaultValue[0].m_strPrimaryDiagnose;
            }
        }

        #region ����
        protected override enmApproveType m_EnmAppType
        {
            get { return enmApproveType.CaseHistory; }
        }
        protected override string m_StrRecorder_ID
        {
            get
            {
                if (m_txtDoctorSign.Tag != null)
                    return ((clsEmrEmployeeBase_VO)m_txtDoctorSign.Tag).m_strEMPNO_CHR.Trim();
                return "";
            }
        }
        #endregion ����

        /// <summary>
        /// ��ȡ���˳�Ժʱ�䣬��ʱ���ڸ��������ѯ
        /// </summary>
        /// <returns></returns>
        private long m_mthGetSetlectedOutDate()
        {
            m_dtmOutHospitalDate = new DateTime(1900, 1, 1);
            string strRegisterID = "";
            long lngRes = 0;
            //clsPatientManagerService objServ =
            //    (clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPatientManagerService));


            //lngRes = objServ.m_lngGetRegisterIDByPatient(m_objCurrentPatient.m_StrPatientID, m_objCurrentPatient.m_DtmSelectedHISInDate.ToString("yyyy-MM-dd HH:mm:ss"), out strRegisterID);

            lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetOutHospitalDate(MDIParent.m_objCurrentPatient.m_strREGISTERID_CHR, out m_dtmOutHospitalDate);

            if (m_dtmOutHospitalDate == DateTime.MinValue || m_dtmOutHospitalDate == new DateTime(1900, 1, 1))
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetPrepOutHospitalDate(strRegisterID, out m_dtmOutHospitalDate);
            }
            //objServ = null;
            return lngRes;
        }

        protected override void m_mthAfterSuccessfulSave()
        {
            //if (!m_dtpOutHospitalDate.Visible || !m_dtpOutHospitalDate.Enabled)
            //{
            //    return;
            //}
            //if (m_dtpOutHospitalDate.Value.ToString("yyyy-MM-dd HH:mm:ss") != m_dtmOutHospitalDate.ToString("yyyy-MM-dd HH:mm:ss"))
            //{
            //    try
            //    {
            //        clsOutHospitalDomain objDomain = new clsOutHospitalDomain();
            //        long lngRes = objDomain.m_lngUpdateOutDate(MDIParent.m_objCurrentPatient.m_strREGISTERID_CHR, Convert.ToDateTime(m_dtpOutHospitalDate.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            //        objDomain = null;
            //    }
            //    catch (Exception Ex)
            //    {
            //        string strEx = Ex.Message;
            //    }                
            //}
        }

        /// <summary>
        /// ���ø������͵�Ĭ��ֵ
        /// </summary>
        /// <param name="p_objPatient"></param>
        protected override void m_mthSetDefaultValue(clsPatient p_objPatient)
        {
            base.m_mthSetDefaultValue(p_objPatient);

            try
            {
                if (p_objPatient == null)
                {
                    return;
                }
                clsOutHospitalDomain objDomain = new clsOutHospitalDomain();
                DataTable dtbOrder = null;
                long lngRes = objDomain.m_lngGetOutOrderByRegID(p_objPatient.m_StrRegisterId, out dtbOrder);

                if (dtbOrder != null)
                {
                    int intRowsCount = dtbOrder.Rows.Count;
                    if (intRowsCount > 0)
                    {
                        System.Text.StringBuilder stbOutOrder = new System.Text.StringBuilder(100);
                        DataRow drTemp = null;
                        for (int i = 0; i < intRowsCount; i++)
                        {
                            drTemp = dtbOrder.Rows[i];
                            stbOutOrder.Append(drTemp["NAME_VCHR"].ToString());
                            stbOutOrder.Append("    ");
                            stbOutOrder.Append(drTemp["REMARK_VCHR"].ToString());
                            stbOutOrder.Append(Environment.NewLine);
                        }
                        this.m_txtOutHospitalAdvice.Text = stbOutOrder.ToString();
                    }
                }
            }
            catch (Exception Ex)
            {

                string strEx = Ex.Message;
            }
        }

        #region ��������

        protected override bool m_blnSubReuse(clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            bool blnIsOK = false;
            if (p_objSelectedValue != null)
            {
                clsTrackRecordContent m_objContent = new clsOutHospitalRecordContent();

                long lngRes = m_objGetDiseaseTrackDomain().m_lngGetDeleteRecordContent(p_objSelectedValue.m_StrInpatientId, p_objSelectedValue.m_DtmInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"), p_objSelectedValue.m_DtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out m_objContent);
                if (lngRes <= 0 || m_objContent == null)
                {
                    switch (lngRes)
                    {
                        case (long)(enmOperationResult.Not_permission):
                            m_mthShowNotPermitted(); break;
                        case (long)(enmOperationResult.DB_Fail):
                            m_mthShowDBError(); break;
                    }
                    return blnIsOK;
                }
                clsOutHospitalRecordContent p_objContent = (clsOutHospitalRecordContent)m_objContent;
                this.m_dtpOutHospitalDate.Text = p_objContent.m_dtmOutHospitalDate.ToString("yyyy-MM-dd hh:mm:ss");
                this.m_txtMainDoctorSign.Text = p_objContent.m_strMainDoctorName;
                this.m_txtDoctorSign.Text = p_objContent.m_strDoctorName;
                this.m_txtInHospitalDiagnose.Text = p_objContent.m_strInHospitalDiagnose;
                this.m_txtOutHospitalDiagnose.Text = p_objContent.m_strOutHospitalDiagnose;
                this.m_txtInHospitalCase.Text = p_objContent.m_strInHospitalCase;
                this.m_txtInHospitalBy.Text = p_objContent.m_strInHospitalBy;
                this.m_txtOutHospitalCase.Text = p_objContent.m_strOutHospitalCase;
                this.m_txtOutHospitalAdvice.Text = p_objContent.m_strOutHospitalAdvice;

                //clsEmrSignToolCollection m_objSign = new clsEmrSignToolCollection();


                blnIsOK = true;
            }
            return blnIsOK;
        }

        //infPrintRecord objPrintTool;
        protected override void m_mthSubPreviewInactiveRecord(IWin32Window p_infOwner, clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            if (p_objSelectedValue == null) return;
            objPrintTool = new clsOutHospitalPrintTool();

            if (m_objBaseCurrentPatient != null)
            {
                objPrintTool.m_mthInitPrintTool(null);
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,
                    p_objSelectedValue.m_DtmInpatientDate,
                    p_objSelectedValue.m_DtmOpenDate);

                //clsPrintInfo_Consultation objPrintInfo = new clsPrintInfo_Consultation();
                clsPrintInfo_OutHospital objPrintInfo = new clsPrintInfo_OutHospital();

                //objPrintInfo.m_dtmHISInDate = p_objSelectedValue.m_DtmInpatientDate;  //???
                objPrintInfo.m_dtmInPatientDate = p_objSelectedValue.m_DtmInpatientDate;
                objPrintInfo.m_dtmOpenDate = p_objSelectedValue.m_DtmOpenDate;
                //objPrintInfo.m_strAge = p_objSelectedValue;           
                //objPrintInfo.m_strAreaName
                //objPrintInfo.m_strBedName
                //objPrintInfo.m_strDeptName=
                //objPrintInfo.m_strHISInPatientID=
                objPrintInfo.m_strInPatentID = p_objSelectedValue.m_StrInpatientId;
                //objPrintInfo.m_strPatientName =
                //objPrintInfo.m_strSex=


                clsTrackRecordContent p_objContent = new clsOutHospitalRecordContent();
                long lngRes = m_objGetDiseaseTrackDomain().m_lngGetDeleteRecordContent(p_objSelectedValue.m_StrInpatientId, p_objSelectedValue.m_DtmInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"), p_objSelectedValue.m_DtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out p_objContent);
                clsOutHospitalRecordContent objContent = (clsOutHospitalRecordContent)p_objContent;
                //objPrintInfo.m_objContent = objContent;
                objPrintInfo.m_objRecordContent = objContent;
                //objPrintInfo.m_blnIsFirstPrint = false;

                objPrintTool.m_mthSetPrintContent(objPrintInfo);

                m_mthStartPrint();
                //ppdPrintPreview.Document = m_pdcPrintDocument;
                //ppdPrintPreview.ShowDialog(p_infOwner);
            }
        }

        protected override clsInactiveRecordInfo_VO[] m_objSubGetAllInactiveInfo(clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            clsInactiveRecordInfo_VO[] objArr = null;

            new clsOutHospitalDomain().m_lngGetAllInactiveInfo(p_objSelectedValue.m_StrInpatientId, p_objSelectedValue.m_DtmInpatientDate, out objArr);
            return objArr;
        }
        public override bool m_blnIsNewSetInactiveForm
        {
            get { return true; }
        }
        #endregion ��������
    }
}

