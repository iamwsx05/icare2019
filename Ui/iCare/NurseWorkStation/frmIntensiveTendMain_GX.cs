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
    /// Σ�ػ��߻����¼(����)
    /// </summary>
    public class frmIntensiveTendMain_GX : iCare.frmRecordsBase
    {
        private System.Windows.Forms.Label label1;
        protected com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtDiagnose;
        private System.Windows.Forms.DataGridTextBoxColumn m_clmRecordTime;
        private cltDataGridDSTRichTextBox m_dtcINITEM;
        public cltDataGridDSTRichTextBox m_dtcINFACT;
        public cltDataGridDSTRichTextBox m_dtcOUTPISS;
        public cltDataGridDSTRichTextBox m_dtcOUTSTOOL;
        private cltDataGridDSTRichTextBox m_dtcCHECKT;
        private cltDataGridDSTRichTextBox m_dtcCHECKP;
        private cltDataGridDSTRichTextBox m_dtcCHECKR;
        private cltDataGridDSTRichTextBox m_dtcCHECKBP;
        private System.Windows.Forms.DataGridTextBoxColumn m_clmNURSESIGN;
        private cltDataGridDSTRichTextBox m_dtcDETAILCONTENT;
        private DataTable dtTempTable;
        private string m_strCurrentOpenDate = "";
        private string m_strCreateUserID = "";
        private System.Windows.Forms.DataGridTextBoxColumn m_clmRecordDay;
        private cltDataGridDSTRichTextBox m_dtcCUSTOM1;
        private cltDataGridDSTRichTextBox m_dtcCUSTOM2;
        private cltDataGridDSTRichTextBox m_dtcCUSTOM3;
        private cltDataGridDSTRichTextBox m_dtcCUSTOM4;
        private string m_strCustomColumn1 = "";
        private string m_strCustomColumn2 = "";
        private string m_strCustomColumn3 = "";
        private string m_strCustomColumn4 = "";
        private string m_strTempColumnName = "";
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;

        public frmIntensiveTendMain_GX()
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

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
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtDiagnose = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_clmRecordDay = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_clmRecordTime = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcINITEM = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcINFACT = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcOUTPISS = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcOUTSTOOL = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcCHECKT = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcCHECKP = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcCHECKR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcCHECKBP = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_clmNURSESIGN = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcDETAILCONTENT = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcCUSTOM1 = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcCUSTOM2 = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcCUSTOM3 = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcCUSTOM4 = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).BeginInit();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgtsStyles
            // 
            this.dgtsStyles.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
                                                                                                         this.m_clmRecordDay,
                                                                                                         this.m_clmRecordTime,
                                                                                                         this.m_dtcINITEM,
                                                                                                         this.m_dtcINFACT,
                                                                                                         this.m_dtcOUTPISS,
                                                                                                         this.m_dtcOUTSTOOL,
                                                                                                         this.m_dtcCUSTOM1,
                                                                                                         this.m_dtcCUSTOM2,
                                                                                                         this.m_dtcCHECKT,
                                                                                                         this.m_dtcCHECKP,
                                                                                                         this.m_dtcCHECKR,
                                                                                                         this.m_dtcCHECKBP,
                                                                                                         this.m_dtcCUSTOM3,
                                                                                                         this.m_dtcCUSTOM4,
                                                                                                         this.m_clmNURSESIGN,
                                                                                                         this.m_dtcDETAILCONTENT});
            this.dgtsStyles.RowHeaderWidth = 15;
            // 
            // m_dtgRecordDetail
            // 
            this.m_dtgRecordDetail.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
            this.m_dtgRecordDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_dtgRecordDetail.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_dtgRecordDetail.DataSource = this.m_dtbRecords;
            this.m_dtgRecordDetail.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.m_dtgRecordDetail.HeaderForeColor = System.Drawing.SystemColors.Window;
            this.m_dtgRecordDetail.Location = new System.Drawing.Point(8, 72);
            this.m_dtgRecordDetail.Size = new System.Drawing.Size(787, 545);
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
            this.m_trvInPatientDate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_trvInPatientDate.LineColor = System.Drawing.Color.Black;
            this.m_trvInPatientDate.Location = new System.Drawing.Point(8, 131);
            this.m_trvInPatientDate.Size = new System.Drawing.Size(208, 80);
            this.m_trvInPatientDate.Visible = false;
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(624, 163);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(720, 163);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(416, 133);
            this.lblBedNoTitle.Size = new System.Drawing.Size(56, 14);
            this.lblBedNoTitle.Text = "��  ��:";
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(416, 165);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(584, 133);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(584, 163);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(680, 163);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(224, 165);
            this.lblAreaTitle.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(472, 163);
            this.txtInPatientID.Size = new System.Drawing.Size(104, 23);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(632, 131);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(472, 131);
            this.m_txtBedNO.Size = new System.Drawing.Size(72, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(264, 163);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(448, 243);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(312, 243);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(264, 131);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(224, 133);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(664, 106);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Location = new System.Drawing.Point(552, 131);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(156, 135);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(424, 8);
            this.m_lblForTitle.Visible = false;
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(621, 76);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(714, 37);
            this.m_cmdModifyPatientInfo.Size = new System.Drawing.Size(69, 28);
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(333, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 10000004;
            this.label1.Text = "���:";
            // 
            // m_txtDiagnose
            // 
            this.m_txtDiagnose.BackColor = System.Drawing.Color.White;
            this.m_txtDiagnose.BorderColor = System.Drawing.Color.Transparent;
            this.m_txtDiagnose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDiagnose.ForeColor = System.Drawing.Color.Black;
            this.m_txtDiagnose.Location = new System.Drawing.Point(379, 40);
            this.m_txtDiagnose.Name = "m_txtDiagnose";
            this.m_txtDiagnose.Size = new System.Drawing.Size(330, 23);
            this.m_txtDiagnose.TabIndex = 10000005;
            // 
            // m_clmRecordDay
            // 
            this.m_clmRecordDay.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_clmRecordDay.Format = "";
            this.m_clmRecordDay.FormatInfo = null;
            this.m_clmRecordDay.MappingName = "RecordDay";
            this.m_clmRecordDay.Width = 80;
            // 
            // m_clmRecordTime
            // 
            this.m_clmRecordTime.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_clmRecordTime.Format = "";
            this.m_clmRecordTime.FormatInfo = null;
            this.m_clmRecordTime.MappingName = "RecordTime";
            this.m_clmRecordTime.Width = 60;
            // 
            // m_dtcINITEM
            // 
            this.m_dtcINITEM.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcINITEM.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcINITEM.m_BlnGobleSet = false;
            this.m_dtcINITEM.m_BlnUnderLineDST = false;
            this.m_dtcINITEM.MappingName = "INITEM";
            this.m_dtcINITEM.Width = 150;
            // 
            // m_dtcINFACT
            // 
            this.m_dtcINFACT.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcINFACT.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcINFACT.m_BlnGobleSet = false;
            this.m_dtcINFACT.m_BlnUnderLineDST = false;
            this.m_dtcINFACT.MappingName = "INFACT";
            this.m_dtcINFACT.ReadOnly = true;
            this.m_dtcINFACT.Width = 60;
            // 
            // m_dtcOUTPISS
            // 
            this.m_dtcOUTPISS.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcOUTPISS.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcOUTPISS.m_BlnGobleSet = true;
            this.m_dtcOUTPISS.m_BlnUnderLineDST = false;
            this.m_dtcOUTPISS.MappingName = "OUTPISS";
            this.m_dtcOUTPISS.Width = 60;
            // 
            // m_dtcOUTSTOOL
            // 
            this.m_dtcOUTSTOOL.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcOUTSTOOL.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcOUTSTOOL.m_BlnGobleSet = true;
            this.m_dtcOUTSTOOL.m_BlnUnderLineDST = false;
            this.m_dtcOUTSTOOL.MappingName = "OUTSTOOL";
            this.m_dtcOUTSTOOL.Width = 60;
            // 
            // m_dtcCHECKT
            // 
            this.m_dtcCHECKT.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcCHECKT.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcCHECKT.m_BlnGobleSet = true;
            this.m_dtcCHECKT.m_BlnUnderLineDST = false;
            this.m_dtcCHECKT.MappingName = "CHECKT";
            this.m_dtcCHECKT.Width = 60;
            // 
            // m_dtcCHECKP
            // 
            this.m_dtcCHECKP.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcCHECKP.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcCHECKP.m_BlnGobleSet = true;
            this.m_dtcCHECKP.m_BlnUnderLineDST = false;
            this.m_dtcCHECKP.MappingName = "CHECKP";
            this.m_dtcCHECKP.Width = 60;
            // 
            // m_dtcCHECKR
            // 
            this.m_dtcCHECKR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcCHECKR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcCHECKR.m_BlnGobleSet = true;
            this.m_dtcCHECKR.m_BlnUnderLineDST = false;
            this.m_dtcCHECKR.MappingName = "CHECKR";
            this.m_dtcCHECKR.Width = 60;
            // 
            // m_dtcCHECKBP
            // 
            this.m_dtcCHECKBP.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcCHECKBP.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcCHECKBP.m_BlnGobleSet = true;
            this.m_dtcCHECKBP.m_BlnUnderLineDST = false;
            this.m_dtcCHECKBP.MappingName = "CHECKBP";
            this.m_dtcCHECKBP.Width = 80;
            // 
            // m_clmNURSESIGN
            // 
            this.m_clmNURSESIGN.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_clmNURSESIGN.Format = "";
            this.m_clmNURSESIGN.FormatInfo = null;
            this.m_clmNURSESIGN.MappingName = "NURSESIGN";
            this.m_clmNURSESIGN.Width = 80;
            // 
            // m_dtcDETAILCONTENT
            // 
            this.m_dtcDETAILCONTENT.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcDETAILCONTENT.m_BlnGobleSet = true;
            this.m_dtcDETAILCONTENT.m_BlnUnderLineDST = false;
            this.m_dtcDETAILCONTENT.MappingName = "DETAILCONTENT";
            this.m_dtcDETAILCONTENT.Width = 270;
            // 
            // m_dtcCUSTOM1
            // 
            this.m_dtcCUSTOM1.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcCUSTOM1.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcCUSTOM1.m_BlnGobleSet = true;
            this.m_dtcCUSTOM1.m_BlnUnderLineDST = false;
            this.m_dtcCUSTOM1.MappingName = "CUSTOM1";
            this.m_dtcCUSTOM1.Width = 60;
            // 
            // m_dtcCUSTOM2
            // 
            this.m_dtcCUSTOM2.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcCUSTOM2.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcCUSTOM2.m_BlnGobleSet = true;
            this.m_dtcCUSTOM2.m_BlnUnderLineDST = false;
            this.m_dtcCUSTOM2.MappingName = "CUSTOM2";
            this.m_dtcCUSTOM2.Width = 60;
            // 
            // m_dtcCUSTOM3
            // 
            this.m_dtcCUSTOM3.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcCUSTOM3.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcCUSTOM3.m_BlnGobleSet = true;
            this.m_dtcCUSTOM3.m_BlnUnderLineDST = false;
            this.m_dtcCUSTOM3.MappingName = "CUSTOM3";
            this.m_dtcCUSTOM3.Width = 60;
            // 
            // m_dtcCUSTOM4
            // 
            this.m_dtcCUSTOM4.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcCUSTOM4.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcCUSTOM4.m_BlnGobleSet = true;
            this.m_dtcCUSTOM4.m_BlnUnderLineDST = false;
            this.m_dtcCUSTOM4.MappingName = "CUSTOM4";
            this.m_dtcCUSTOM4.Width = 60;
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Size = new System.Drawing.Size(802, 60);
            this.m_pnlNewBase.Visible = true;
            // 
            // frmIntensiveTendMain_GX
            // 
            this.ClientSize = new System.Drawing.Size(807, 673);
            this.Controls.Add(this.m_txtDiagnose);
            this.Controls.Add(this.label1);
            this.Name = "frmIntensiveTendMain_GX";
            this.Text = "Σ�ػ��߻����¼";
            this.Load += new System.EventHandler(this.frmIntensiveTendMain_GX_Load);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.m_trvInPatientDate, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.m_txtDiagnose, 0);
            this.Controls.SetChildIndex(this.m_dtgRecordDetail, 0);
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
            p_dtbRecordTable.Columns.Clear();
            //��ż�¼ʱ����ַ���
            p_dtbRecordTable.Columns.Add("RecordDate");//0
                                                       //��ż�¼���͵�intֵ
            DataColumn dcRecordType = new DataColumn("RecordType", typeof(int));
            p_dtbRecordTable.Columns.Add(dcRecordType);//1
                                                       //��ż�¼��OpenDate�ַ���
            p_dtbRecordTable.Columns.Add("OpenDate");  //2
                                                       //��ż�¼��ModifyDate�ַ���
            p_dtbRecordTable.Columns.Add("ModifyDate"); //3

            DataColumn dc1 = p_dtbRecordTable.Columns.Add("RecordDay");//4
            dc1.DefaultValue = "";
            DataColumn dc2 = p_dtbRecordTable.Columns.Add("RecordTime");//5
            dc2.DefaultValue = "";
            p_dtbRecordTable.Columns.Add("INITEM", typeof(clsDSTRichTextBoxValue));//6
            p_dtbRecordTable.Columns.Add("INFACT", typeof(clsDSTRichTextBoxValue));//7
            p_dtbRecordTable.Columns.Add("OUTPISS", typeof(clsDSTRichTextBoxValue));//8
            p_dtbRecordTable.Columns.Add("OUTSTOOL", typeof(clsDSTRichTextBoxValue));//9
            p_dtbRecordTable.Columns.Add("CUSTOM1", typeof(clsDSTRichTextBoxValue));//10
            p_dtbRecordTable.Columns.Add("CUSTOM2", typeof(clsDSTRichTextBoxValue));//11
            p_dtbRecordTable.Columns.Add("CHECKT", typeof(clsDSTRichTextBoxValue));//12
            p_dtbRecordTable.Columns.Add("CHECKP", typeof(clsDSTRichTextBoxValue));//13
            p_dtbRecordTable.Columns.Add("CHECKR", typeof(clsDSTRichTextBoxValue));//14
            p_dtbRecordTable.Columns.Add("CHECKBP", typeof(clsDSTRichTextBoxValue));//15
            p_dtbRecordTable.Columns.Add("CUSTOM3", typeof(clsDSTRichTextBoxValue));//16
            p_dtbRecordTable.Columns.Add("CUSTOM4", typeof(clsDSTRichTextBoxValue));//17
            DataColumn dc3 = p_dtbRecordTable.Columns.Add("NURSESIGN");//18	
            dc3.DefaultValue = "";
            p_dtbRecordTable.Columns.Add("DETAILCONTENT", typeof(clsDSTRichTextBoxValue));//19
            p_dtbRecordTable.Columns.Add("DetailRecordTime");//20
            p_dtbRecordTable.Columns.Add("RecordAndDetailSign");//21
            p_dtbRecordTable.Columns.Add("CreateUserID");//9

            m_dtcDETAILCONTENT.m_RtbBase.m_BlnReadOnly = true;
            m_mthSetControl(m_clmRecordDay);
            m_mthSetControl(m_clmRecordTime);
            m_mthSetControl(m_dtcINITEM);
            m_mthSetControl(m_dtcINFACT);
            m_mthSetControl(m_dtcOUTPISS);
            m_mthSetControl(m_dtcOUTSTOOL);
            m_mthSetControl(m_dtcCUSTOM1);
            m_mthSetControl(m_dtcCUSTOM2);
            m_mthSetControl(m_dtcCHECKT);
            m_mthSetControl(m_dtcCHECKP);
            m_mthSetControl(m_dtcCHECKR);
            m_mthSetControl(m_dtcCHECKBP);
            m_mthSetControl(m_dtcCUSTOM3);
            m_mthSetControl(m_dtcCUSTOM4);
            m_mthSetControl(m_clmNURSESIGN);
            m_mthSetControl(m_dtcDETAILCONTENT);

            //����������
            this.m_clmRecordDay.HeaderText = "\r\n\r\n����";
            this.m_clmRecordTime.HeaderText = "\r\n\r\nʱ��";
            this.m_dtcINITEM.HeaderText = "\r\n����\r\n\r\n��\r\n\r\nĿ";
            this.m_dtcINFACT.HeaderText = "\r\n����\r\n(ml)\r\nʵ\r\n��\r\n��";
            this.m_dtcOUTPISS.HeaderText = "\r\n����\r\n(ml)\r\nС\r\n��";
            this.m_dtcOUTSTOOL.HeaderText = "\r\n����\r\n(ml)\r\n��\r\n��";
            this.m_dtcCUSTOM1.HeaderText = m_strCustomColumn1;
            this.m_dtcCUSTOM2.HeaderText = m_strCustomColumn2;
            this.m_dtcCHECKT.HeaderText = "\r\n\r\nT\r\n(��)";
            this.m_dtcCHECKP.HeaderText = "\r\n\r\nHR/P\r\n(��/��)";
            this.m_dtcCHECKR.HeaderText = "\r\n\r\nR\r\n(��/��)";
            this.m_dtcCHECKBP.HeaderText = "\r\n\r\nBP\r\n(mmHg)";
            this.m_dtcCUSTOM3.HeaderText = m_strCustomColumn3;
            this.m_dtcCUSTOM4.HeaderText = m_strCustomColumn4;
            this.m_clmNURSESIGN.HeaderText = "\r\n\r\nǩ��";
            this.m_dtcDETAILCONTENT.HeaderText = "\r\n\r\n                �����¼";
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
            return new clsRecordsDomain(enmRecordsType.IntensiveTendRecord_GX);
        }

        // ��ȡ��¼����Ҫ��Ϣ�������ȡ����CreateDate,OpenDate,LastModifyDate��
        protected override clsTrackRecordContent m_objGetRecordMainContent(int p_intRecordType,
            object[] p_objDataArr)
        {
            //���� p_intRecordType ��ȡ��Ӧ�� clsTrackRecordContent
            clsTrackRecordContent objContent = null;
            switch ((enmDiseaseTrackType)p_intRecordType)
            {
                case enmDiseaseTrackType.IntensiveTendRecord_GX:
                    objContent = new clsIntensiveTendRecord_GX();
                    break;
            }

            if (objContent == null)
                objContent = new clsIntensiveTendRecord_GX();

            if (m_objCurrentPatient != null)
                objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            else
            {
                clsPublicFunction.ShowInformationMessageBox("��ǰ����Ϊ��!");
                return null;
            }
            int intSelectedRecordStartRow = m_dtgRecordDetail.CurrentCell.RowNumber;
            string strDetailSign = (m_dtbRecords.Rows[intSelectedRecordStartRow][21]).ToString();

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
            objContent.m_strCreateUserID = (string)p_objDataArr[22];

            return objContent;
        }

        private void frmIntensiveTendMain_GX_Load(object sender, System.EventArgs e)
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
            frmIntensiveTend_GXContent frmAddNewForm = (frmIntensiveTend_GXContent)p_objSender;
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
                frmIntensiveTend_GXContent frm = new frmIntensiveTend_GXContent(true, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), "2005");
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
                    MessageBox.Show("��ѡ��һ�������¼���ݣ�");
                    return;
                }
                //���ݲ���
                int intSelectedRecordStartRow = m_dtgRecordDetail.CurrentCell.RowNumber;
                string strRecordTime = (m_dtbRecords.Rows[intSelectedRecordStartRow][20]).ToString();
                if (strRecordTime.Trim().Length == 0)
                    return;
                //��֤
                //���ݲ���
                //�򿪴���				
                frmIntensiveTend_GXContent frm = new frmIntensiveTend_GXContent(false, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), strRecordTime);
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
                    MessageBox.Show("��ѡ��һ�������¼���ݣ�");
                    return;
                }

                int intSelectedRecordStartRow = m_dtgRecordDetail.CurrentCell.RowNumber;

                string strDetailSign = (m_dtbRecords.Rows[intSelectedRecordStartRow][21]).ToString();

                //���� by tfzhang 2006-02-24
                //if(strDetailSign != null && strDetailSign.Trim() != "")
                //{
                string[] strArr = strDetailSign.Split('��');
                //    if(strArr != null && strArr.Length > 1 && strArr[1]!=string.Empty && strArr[1].Trim() != MDIParent.OperatorID.Trim())
                //    {
                //        MDIParent.ShowInformationMessageBox("�Ǽ�¼�����߲���ɾ����¼��");
                //        return;
                //    }
                //}

                //Ȩ���ж�
                string strTemp = "";
                try
                {
                    if (strArr[1] != null)
                    {
                        strTemp = strArr[1];
                    }
                }
                catch (Exception)
                {

                    strTemp = "";
                }

                string strDeptIDTemp = m_ObjCurrentArea.m_strDEPTID_CHR;
                bool blnIsAllow = clsPublicFunction.IsAllowDelete(strDeptIDTemp, strTemp, clsEMRLogin.LoginEmployee, intFormType);
                if (!blnIsAllow)
                    return;
                //���ݲ���
                string strRecordTime = (m_dtbRecords.Rows[intSelectedRecordStartRow][20]).ToString();
                if (strRecordTime.Trim().Length == 0)
                    return;
                //ȷ��
                if (MessageBox.Show("ȷ��Ҫɾ��ѡ�еĲ����¼���ݣ�", "ɾ����ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;

                //�򿪴���
                //ɾ��
                //clsIntensiveTendRecord_GXService objserv =
                //    (clsIntensiveTendRecord_GXService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsIntensiveTendRecord_GXService));

                string strDelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string strDelID = MDIParent.OperatorID;
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsIntensiveTendRecord_GXService_m_lngDeleteDetail(m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), strRecordTime, strDelTime, strDelID);
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
                case enmDiseaseTrackType.IntensiveTendRecord_GX:
                    return new frmIntensiveTend_GX(m_txtDiagnose.Text);
                case enmDiseaseTrackType.IntensiveTendRecord_GXCon:
                    return new frmIntensiveTend_GXContent(false, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), "");
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
            this.m_txtDiagnose.Text = "";
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
            m_mthAddNewRecord((int)enmDiseaseTrackType.IntensiveTendRecord_GX);
        }

        protected override object[][] m_objGetRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
        {
            #region ��ʾ��¼��DataGrid
            try
            {
                object[] objData;
                ArrayList objReturnData = new ArrayList();
                ArrayList arlDetail = new ArrayList();//��Ų����¼fjf
                int intCurrentDetail = 0;//��ǰ�����¼��ArrayList�е�����
                                         //				bool blnIsAddBlank = false;//�Ƿ��ڸ��в����¼��ʾ����������
                                         //				int intBlankRows = 0;//�ڸ��в����¼������Ŀ�������
                bool blnIsFirst = true;//�Ƿ���ͳ��ʱ��εĵ�һ����Ч��¼
                DateTime dtmBegin = DateTime.MinValue;//ͳ��ʱ��εĿ�ʼʱ��
                bool blnIsSameClass = false;
                int intRecordCount = 0;

                clsIntensiveTendRecord_GXDataInfo objITRCInfo = new clsIntensiveTendRecord_GXDataInfo();
                clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
                string strText, strXml;

                objITRCInfo = (clsIntensiveTendRecord_GXDataInfo)p_objTransDataInfo;

                if (objITRCInfo.m_objRecordArr == null && objITRCInfo.m_objDetailArr == null)
                    return null;

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

                #region �Բ����¼���д���
                if (objITRCInfo.m_objDetailArr != null)
                {
                    string strDetail = "";
                    string strDetailXML = "";
                    for (int n = 0; n < objITRCInfo.m_objDetailArr.Length; n++)
                    {
                        clsIntensiveTendRecordDetail_GX objDetail = objITRCInfo.m_objDetailArr[n];
                        object[] objTemp = new object[7];
                        strDetail = objDetail.m_strDETAILCONTENT;
                        strDetailXML = objDetail.m_strDETAILCONTENTXML;
                        string[] strDetailArrTemp;
                        string[] strDetailXMLArrTemp;
                        //�������¼���С�
                        com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strDetail, strDetailXML, 34, out strDetailArrTemp, out strDetailXMLArrTemp);
                        string[] strDetailArr, strDetailXMLArr;
                        if (strDetail != string.Empty)
                        {
                            strDetailArr = new string[strDetailArrTemp.Length + 2];//���������ں�ǩ�������¼
                            strDetailXMLArr = new string[strDetailXMLArrTemp.Length + 2];//���������ں�ǩ�������¼XML

                            //�����ں�ǩ����ӽ������¼
                            strDetailArr[0] = objDetail.m_dtmDETAILRECORDDATE.ToString("yyyy-MM-dd HH:mm");
                            strDetailArr[1] = strDetailArrTemp[0];
                            for (int i = 2; i < strDetailArr.Length - 1; i++)
                            {
                                strDetailArr[i] = strDetailArrTemp[i - 1];
                            }
                            strDetailArr[strDetailArr.Length - 1] = "                         " + objDetail.m_strDETAILSIGNNAME;

                            strDetailXMLArr[0] = strDetailXMLArr[strDetailXMLArr.Length - 1] = "";
                            for (int i = 1; i < strDetailXMLArr.Length - 1; i++)
                            {
                                strDetailXMLArr[i] = strDetailXMLArrTemp[i - 1];
                            }

                            objTemp[0] = strDetailArr;
                            objTemp[1] = strDetailXMLArr;
                            objTemp[2] = strDetailArr.Length;

                            objTemp[3] = objDetail.m_dtmOpenDate;
                            //objTemp[3] = objDetail.m_dtmDETAILRECORDDATE;

                            objTemp[4] = objDetail.m_intSTAT_STATUS;
                            objTemp[5] = objDetail.m_strDETAILSIGNNAME;
                            objTemp[6] = objDetail.m_strDETAILSIGNID;
                            arlDetail.Add(objTemp);
                        }
                    }
                }
                #endregion

                if (objITRCInfo.m_objRecordArr != null)
                    intRecordCount = objITRCInfo.m_objRecordArr.Length;
                int intRowOfCurrentDetail = 0;

                for (int i = 0; i < intRecordCount; i++)
                {
                    objData = new object[23];
                    clsIntensiveTendRecord_GX objCurrent = objITRCInfo.m_objRecordArr[i];
                    clsIntensiveTendRecord_GX objNext = new clsIntensiveTendRecord_GX();//��һ�������¼
                    if (i < intRecordCount - 1)
                        objNext = objITRCInfo.m_objRecordArr[i + 1];

                    //����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ��޸����봴����Ϊͬһ�ˣ�����ʾ
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_strModifyUserID.Trim() == objCurrent.m_strCreateUserID.Trim()
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate)
                    {
                        TimeSpan tsModify = objNext.m_dtmModifyDate - objCurrent.m_dtmModifyDate;
                        if ((int)tsModify.TotalHours < intCanModifyTime)
                            continue;
                    }

                    this.m_txtDiagnose.Text = objCurrent.m_strDIAGNOSE;//�������ʾ������

                    #region ��Źؼ��ֶ�
                    if (objCurrent.m_dtmCreateDate != DateTime.MinValue)
                    {
                        objData[0] = objCurrent.m_dtmRECORDDATE;//��ż�¼ʱ����ַ���
                        objData[1] = (int)enmRecordsType.IntensiveTendRecord_GX;//��ż�¼���͵�intֵ
                        objData[2] = objCurrent.m_dtmOpenDate;//��ż�¼��OpenDate�ַ���
                        objData[3] = objCurrent.m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   
                        objData[22] = objCurrent.m_strCreateUserID;//��ż�¼��createuserid�ַ���   

                        //ͬһ����ֻ�ڵ�һ����ʾ����
                        if (objCurrent.m_dtmRECORDDATE.Date.ToString() != m_dtmPreRecordDate.Date.ToString())
                        {
                            objData[4] = objCurrent.m_dtmRECORDDATE.Date.ToString("yyyy-MM-dd");//�����ַ���
                        }
                        //�޸ĺ���кۼ��ļ�¼������ʾʱ��
                        if (m_dtmPreRecordDate != objCurrent.m_dtmRECORDDATE)
                            objData[5] = objCurrent.m_dtmRECORDDATE.ToString("HH:mm");//ʱ���ַ���
                                                                                      //						if(blnIsFirst )
                                                                                      //						{
                                                                                      //							dtmBegin = objCurrent.m_dtmRECORDDATE;
                                                                                      //							blnIsFirst = false;
                                                                                      //						}
                                                                                      //						if(objCurrent.m_intSTAT_STATUS == 1)
                                                                                      //						{
                                                                                      //							blnIsFirst = true;
                                                                                      //							if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate)
                                                                                      //								blnIsFirst = false;
                                                                                      //						}

                    }
                    m_dtmPreRecordDate = objCurrent.m_dtmRECORDDATE;
                    #endregion ;

                    #region ��ŵ�����Ϣ
                    //����>>��Ŀ
                    strText = objCurrent.m_strINITEM_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strINITEM_RIGHT != objCurrent.m_strINITEM_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strINITEM_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[6] = objclsDSTRichTextBoxValue;

                    //����>>ʵ����
                    strText = objCurrent.m_strINFACT_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strINFACT_RIGHT != objCurrent.m_strINFACT_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strINFACT_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[7] = objclsDSTRichTextBoxValue;

                    //����>>С��
                    strText = objCurrent.m_strOUTPISS_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strOUTPISS_RIGHT != objCurrent.m_strOUTPISS_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strOUTPISS_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[8] = objclsDSTRichTextBoxValue;

                    //����>>���
                    strText = objCurrent.m_strOUTSTOOL_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strOUTSTOOL_RIGHT != objCurrent.m_strOUTSTOOL_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strOUTSTOOL_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[9] = objclsDSTRichTextBoxValue;

                    //����>>�Զ�����1
                    strText = objCurrent.m_strCUSTOM1_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strCUSTOM1_RIGHT != objCurrent.m_strCUSTOM1_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strCUSTOM1_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[10] = objclsDSTRichTextBoxValue;

                    //����>>�Զ�����2
                    strText = objCurrent.m_strCUSTOM2_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strCUSTOM2_RIGHT != objCurrent.m_strCUSTOM2_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strCUSTOM2_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[11] = objclsDSTRichTextBoxValue;

                    //����T
                    strText = objCurrent.m_strCHECKT_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strCHECKT_RIGHT != objCurrent.m_strCHECKT_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strCHECKT_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[12] = objclsDSTRichTextBoxValue;

                    //����P
                    strText = objCurrent.m_strCHECKP_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strCHECKP_RIGHT != objCurrent.m_strCHECKP_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strCHECKP_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[13] = objclsDSTRichTextBoxValue;

                    //����R
                    strText = objCurrent.m_strCHECKR_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strCHECKR_RIGHT != objCurrent.m_strCHECKR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strCHECKR_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[14] = objclsDSTRichTextBoxValue;

                    //ѪѹBP
                    strText = objCurrent.m_strCHECKBPA_RIGHT + "/" + objCurrent.m_strCHECKBPS_RIGHT;
                    if ((objCurrent.m_strCHECKBPA_RIGHT == null || objCurrent.m_strCHECKBPA_RIGHT == "") && (objCurrent.m_strCHECKBPS_RIGHT == null || objCurrent.m_strCHECKBPS_RIGHT == ""))
                        strText = "";
                    string strNextText = "";
                    if (objNext != null)
                    {
                        strNextText = objNext.m_strCHECKBPA_RIGHT + "/" + objNext.m_strCHECKBPS_RIGHT;
                        if ((objNext.m_strCHECKBPA_RIGHT == null || objNext.m_strCHECKBPA_RIGHT == "") && (objNext.m_strCHECKBPS_RIGHT == null || objNext.m_strCHECKBPS_RIGHT == ""))
                            strNextText = "";
                    }
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && strNextText != strText)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(strText, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[15] = objclsDSTRichTextBoxValue;

                    //�Զ�����3
                    strText = objCurrent.m_strCUSTOM3_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strCUSTOM3_RIGHT != objCurrent.m_strCUSTOM3_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strCUSTOM3_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[16] = objclsDSTRichTextBoxValue;

                    //�Զ�����4
                    strText = objCurrent.m_strCUSTOM4_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strCUSTOM4_RIGHT != objCurrent.m_strCUSTOM4_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strCUSTOM4_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[17] = objclsDSTRichTextBoxValue;

                    //ǩ��	
                    //if(objCurrent.m_strNURSESIGNID != "" && objCurrent.m_strNURSESIGNID != null)
                    //{
                    //    clsEmployee objEmp = new clsEmployee(objCurrent.m_strNURSESIGNID);
                    //    objData[18] = objEmp.m_StrLastName;
                    //}
                    objData[18] = objCurrent.m_strNURSESIGNNAME;

                    //�����¼
                    if (arlDetail != null && intCurrentDetail < arlDetail.Count &&
                        arlDetail.Count > 0)
                    {
                        //��Ϊ�ɼ�¼δ�б�������Ϣ�����½����ж�
                        if (objCurrent.m_intSTAT_STATUS == 0 || objCurrent.m_intSTAT_STATUS == 1 || (int)(((object[])arlDetail[intCurrentDetail])[4]) == 0)
                        {
                            object[] objDetailArr = arlDetail[intCurrentDetail] as object[];
                            string[] strArr = objDetailArr[0] as string[];
                            blnIsSameClass = m_blnIsSameClass(objCurrent.m_dtmRECORDDATE, DateTime.Parse(strArr[0]));
                        }
                        else
                            blnIsSameClass = objCurrent.m_intSTAT_STATUS == (int)(((object[])arlDetail[intCurrentDetail])[4]) ? true : false;
                        if (blnIsSameClass)
                        {
                            strText = ((string[])(((object[])arlDetail[intCurrentDetail])[0]))[intRowOfCurrentDetail];
                            strXml = ((string[])(((object[])arlDetail[intCurrentDetail])[1]))[intRowOfCurrentDetail];
                            objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                            objclsDSTRichTextBoxValue.m_strText = strText;
                            objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                            objData[19] = objclsDSTRichTextBoxValue;
                            objData[20] = (DateTime)(((object[])arlDetail[intCurrentDetail])[3]);
                            objData[21] = objCurrent.m_strCreateUserID + "��" + (((object[])arlDetail[intCurrentDetail])[6]).ToString();

                            if (intRowOfCurrentDetail == ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1)
                            {
                                intRowOfCurrentDetail = 0;
                                intCurrentDetail++;
                            }
                            else
                                intRowOfCurrentDetail++;

                            objReturnData.Add(objData);
                        }
                        else if (objNext != null)//������������¼δ��ȫ��ʾ������һ�������¼�İ�θ���ǰ��¼�İ�β�ͬ�����Ƚ������¼ȫ����ʾ
                        {
                            while (arlDetail != null &&
                                intCurrentDetail < arlDetail.Count &&
                                intRowOfCurrentDetail < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length &&
                                DateTime.Parse(((string[])(((object[])(arlDetail[intCurrentDetail]))[0]))[0]) <= objCurrent.m_dtmRECORDDATE)
                            {
                                //��Ϊ�ɼ�¼δ�б�������Ϣ�����½����ж�
                                if (objNext.m_intSTAT_STATUS == 0 || objNext.m_intSTAT_STATUS == 1 || (int)(((object[])arlDetail[intCurrentDetail])[4]) == 0)
                                {
                                    object[] objDetailArr = arlDetail[intCurrentDetail] as object[];
                                    string[] strArr = objDetailArr[0] as string[];
                                    blnIsSameClass = m_blnIsSameClass(objNext.m_dtmRECORDDATE, DateTime.Parse(strArr[0]));
                                }
                                else
                                    blnIsSameClass = objNext.m_intSTAT_STATUS == (int)(((object[])arlDetail[intCurrentDetail])[4]) ? true : false;

                                if (!blnIsSameClass)
                                {
                                    object[] objInstance = null;
                                    for (int m = intRowOfCurrentDetail; m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length; m++)
                                    {
                                        objInstance = new object[22];
                                        strText = ((string[])(((object[])arlDetail[intCurrentDetail])[0]))[m];
                                        strXml = ((string[])(((object[])arlDetail[intCurrentDetail])[1]))[m];
                                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                        objclsDSTRichTextBoxValue.m_strText = strText;
                                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                        objInstance[19] = objclsDSTRichTextBoxValue;
                                        objInstance[20] = (DateTime)(((object[])arlDetail[intCurrentDetail])[3]);
                                        objInstance[21] = objCurrent.m_strCreateUserID + "��" + (((object[])arlDetail[intCurrentDetail])[6]).ToString();
                                        objReturnData.Add(objInstance);

                                        if (m == ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1)
                                        {
                                            intRowOfCurrentDetail = 0;
                                            intCurrentDetail++;
                                            break;
                                        }
                                    }
                                }
                                else
                                    break;
                            }
                        }

                    }
                    if (objData != null && objData[19] == null)
                        objReturnData.Add(objData);

                    if (blnIsFirst)
                    {
                        blnIsFirst = false;
                        string strBegin = objCurrent.m_dtmRECORDDATE.ToString("yyyy-MM-dd HH:mm:00");
                        dtmBegin = DateTime.Parse(strBegin);
                    }
                    #region ��m_intISSTAT==-1ʱ��ʾ�ɵ�ͳ����Ϣ�����þɵ���ʾ����
                    if (objCurrent.m_intISSTAT == -1 && objNext != null && objNext.m_intISSTAT != 1 &&
                        ((objCurrent.m_dtmRECORDDATE.Hour < 8 && objNext.m_dtmRECORDDATE.Hour >= 8) ||
                        ((objNext.m_dtmRECORDDATE.Date - objCurrent.m_dtmRECORDDATE.Date).Days >= 1 && objNext.m_dtmRECORDDATE.Hour >= 8)))//ÿ������8�����ͳ��
                    {
                        if (objNext.m_dtmCreateDate != objCurrent.m_dtmCreateDate)
                        {
                            if (intRowOfCurrentDetail != 0 && arlDetail != null && intCurrentDetail < arlDetail.Count && intRowOfCurrentDetail < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length)
                            {
                                object[] objInstance = null;
                                for (int m = intRowOfCurrentDetail; m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length; m++)
                                {
                                    objInstance = new object[22];
                                    strText = ((string[])(((object[])arlDetail[intCurrentDetail])[0]))[m];
                                    strXml = ((string[])(((object[])arlDetail[intCurrentDetail])[1]))[m];
                                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                    objclsDSTRichTextBoxValue.m_strText = strText;
                                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                    objInstance[19] = objclsDSTRichTextBoxValue;
                                    objInstance[20] = (DateTime)(((object[])arlDetail[intCurrentDetail])[3]);
                                    objInstance[21] = objCurrent.m_strCreateUserID + "��" + (((object[])arlDetail[intCurrentDetail])[6]).ToString();
                                    objReturnData.Add(objInstance);

                                    if (m == ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1)
                                    {
                                        intRowOfCurrentDetail = 0;
                                        intCurrentDetail++;
                                        break;
                                    }
                                }
                                //								blnIsAddBlank = false;
                            }
                            double dblInSum = 0;
                            double dblOutSum = 0;
                            double dblSubHours = 0;
                            TimeSpan ts = objCurrent.m_dtmRECORDDATE - dtmBegin;
                            if (ts.TotalHours < 24)
                            {
                                if (objCurrent.m_dtmRECORDDATE.Hour >= 0 && objCurrent.m_dtmRECORDDATE.Hour < 8)
                                {
                                    ts = objCurrent.m_dtmRECORDDATE.Date.AddHours(8) - dtmBegin;
                                }
                                else
                                {
                                    ts = objCurrent.m_dtmRECORDDATE.Date.AddDays(1).AddHours(8) - dtmBegin;
                                }
                                dblSubHours = ts.TotalHours;
                                dblInSum = m_dblGetInSum(dtmBegin, objCurrent.m_dtmRECORDDATE);
                                dblOutSum = m_dblGetOutSum(dtmBegin, objCurrent.m_dtmRECORDDATE);
                            }
                            else
                            {
                                dblSubHours = 24;
                                if (objCurrent.m_dtmRECORDDATE.Hour >= 0 && objCurrent.m_dtmRECORDDATE.Hour < 8)
                                {
                                    dblInSum = m_dblGetInSum(objCurrent.m_dtmRECORDDATE.Date.AddDays(-1).AddHours(8), objCurrent.m_dtmRECORDDATE);
                                    dblOutSum = m_dblGetOutSum(objCurrent.m_dtmRECORDDATE.Date.AddDays(-1).AddHours(8), objCurrent.m_dtmRECORDDATE);
                                }
                                else
                                {
                                    dblInSum = m_dblGetInSum(objCurrent.m_dtmRECORDDATE.Date.AddHours(8), objCurrent.m_dtmRECORDDATE);
                                    dblOutSum = m_dblGetOutSum(objCurrent.m_dtmRECORDDATE.Date.AddHours(8), objCurrent.m_dtmRECORDDATE);
                                }
                            }
                            object[] objSum = null;
                            objSum = new object[22];
                            strText = ((int)dblSubHours).ToString() + " h��������";
                            strXml = "<root />";
                            strXml = m_strGetDSTTextXML(strText, MDIParent.OperatorID, MDIParent.OperatorName);
                            objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                            objclsDSTRichTextBoxValue.m_strText = strText;
                            objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                            objclsDSTRichTextBoxValue.m_blnUnderDST = true;
                            objSum[6] = objclsDSTRichTextBoxValue;

                            strText = dblInSum.ToString();
                            strXml = "<root />";
                            strXml = m_strGetDSTTextXML(strText, MDIParent.OperatorID, MDIParent.OperatorName);
                            objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                            objclsDSTRichTextBoxValue.m_strText = strText;
                            objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                            objclsDSTRichTextBoxValue.m_blnUnderDST = true;
                            objSum[7] = objclsDSTRichTextBoxValue;
                            objReturnData.Add(objSum);


                            objSum = new object[19];
                            strText = ((int)dblSubHours).ToString() + " h�ܳ�����";
                            strXml = "<root />";
                            strXml = m_strGetDSTTextXML(strText, MDIParent.OperatorID, MDIParent.OperatorName);
                            objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                            objclsDSTRichTextBoxValue.m_strText = strText;
                            objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                            objclsDSTRichTextBoxValue.m_blnUnderDST = true;
                            objSum[6] = objclsDSTRichTextBoxValue;

                            strText = dblOutSum.ToString();
                            strXml = "<root />";
                            strXml = m_strGetDSTTextXML(strText, MDIParent.OperatorID, MDIParent.OperatorName);
                            objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                            objclsDSTRichTextBoxValue.m_strText = strText;
                            objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                            objclsDSTRichTextBoxValue.m_blnUnderDST = true;
                            objSum[7] = objclsDSTRichTextBoxValue;
                            if (objCurrent.m_strNURSESIGNID != "" && objCurrent.m_strNURSESIGNID != null)
                            {
                                clsEmployee objEmp = new clsEmployee(objCurrent.m_strNURSESIGNID);
                                objSum[18] = objEmp.m_StrLastName;
                            }
                            objReturnData.Add(objSum);
                        }
                    }
                    #endregion
                    if (objCurrent.m_intISSTAT == 1)
                    {
                        //����ü�¼ֻ��¼��ͳ����Ϣ������������ӵĸü�¼ɾ��
                        bool isOnlySum = true;
                        String strTemp = "";
                        for (int n = 6; n <= 17; n++)
                        {
                            //							strTemp = ((clsDSTRichTextBoxValue)((object[])objReturnData[objReturnData.Count-1])[n]).m_strText;
                            //							if(strTemp != ""&& strTemp!=null)
                            //								isOnlySum = false;
                            if (((object[])objReturnData[objReturnData.Count - 1])[n] != null)
                                isOnlySum = false;
                        }
                        if (isOnlySum)
                        {
                            //���ü�¼ֻ��¼��ͳ����ʱ������ʾ�ü�¼��ʱ�估ǩ��
                            ((object[])objReturnData[objReturnData.Count - 1])[5] = null;
                            ((object[])objReturnData[objReturnData.Count - 1])[18] = null;
                        }

                        if (intRowOfCurrentDetail != 0 && arlDetail != null && intCurrentDetail < arlDetail.Count && intRowOfCurrentDetail < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length)
                        {
                            object[] objInstance = null;
                            for (int m = intRowOfCurrentDetail; m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length; m++)
                            {
                                objInstance = new object[22];
                                strText = ((string[])(((object[])arlDetail[intCurrentDetail])[0]))[m];
                                strXml = ((string[])(((object[])arlDetail[intCurrentDetail])[1]))[m];
                                objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                objclsDSTRichTextBoxValue.m_strText = strText;
                                objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                objInstance[19] = objclsDSTRichTextBoxValue;
                                objInstance[20] = (DateTime)(((object[])arlDetail[intCurrentDetail])[3]);
                                objInstance[21] = objCurrent.m_strCreateUserID + "��" + (((object[])arlDetail[intCurrentDetail])[6]).ToString();
                                objReturnData.Add(objInstance);

                                if (m == ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1)
                                {
                                    intRowOfCurrentDetail = 0;
                                    intCurrentDetail++;
                                    break;
                                }
                            }
                        }
                        object[] objSum = null;
                        objSum = new object[22];
                        strText = objCurrent.m_intSUMINTIME.ToString() + " h��������";
                        strXml = "<root />";
                        strXml = m_strGetDSTTextXML(strText, MDIParent.OperatorID, MDIParent.OperatorName);
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objclsDSTRichTextBoxValue.m_blnUnderDST = true;
                        objSum[6] = objclsDSTRichTextBoxValue;

                        strText = objCurrent.m_strSUMIN;
                        strXml = "<root />";
                        strXml = m_strGetDSTTextXML(strText, MDIParent.OperatorID, MDIParent.OperatorName);
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objclsDSTRichTextBoxValue.m_blnUnderDST = true;
                        objSum[7] = objclsDSTRichTextBoxValue;
                        objReturnData.Add(objSum);


                        objSum = new object[19];
                        strText = objCurrent.m_intSUMOUTTIME.ToString() + " h�ܳ�����";
                        strXml = "<root />";
                        strXml = m_strGetDSTTextXML(strText, MDIParent.OperatorID, MDIParent.OperatorName);
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objclsDSTRichTextBoxValue.m_blnUnderDST = true;
                        objSum[6] = objclsDSTRichTextBoxValue;

                        strText = objCurrent.m_strSUMOUT;
                        strXml = "<root />";
                        strXml = m_strGetDSTTextXML(strText, MDIParent.OperatorID, MDIParent.OperatorName);
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objclsDSTRichTextBoxValue.m_blnUnderDST = true;
                        objSum[7] = objclsDSTRichTextBoxValue;
                        if (objCurrent.m_strNURSESIGNID != "" && objCurrent.m_strNURSESIGNID != null)
                        {
                            clsEmployee objEmp = new clsEmployee(objCurrent.m_strNURSESIGNID);
                            objSum[18] = objEmp.m_StrLastName;
                        }
                        objReturnData.Add(objSum);
                    }
                    #endregion
                }
                //��������¼δ��ʾ������������¼��ȫ����ʾ�꣬��������ʣ�ಿ��
                while (arlDetail != null && intCurrentDetail < arlDetail.Count && arlDetail.Count > 0)
                {
                    if (intRowOfCurrentDetail < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length)
                    {
                        object[] objInstance = null;
                        for (int m = intRowOfCurrentDetail; m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length; m++)
                        {
                            objInstance = new object[22];
                            strText = ((string[])(((object[])arlDetail[intCurrentDetail])[0]))[m];
                            strXml = ((string[])(((object[])arlDetail[intCurrentDetail])[1]))[m];
                            objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                            objclsDSTRichTextBoxValue.m_strText = strText;
                            objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                            objInstance[19] = objclsDSTRichTextBoxValue;
                            objInstance[20] = (DateTime)(((object[])arlDetail[intCurrentDetail])[3]);
                            objInstance[21] = "0��" + (((object[])arlDetail[intCurrentDetail])[6]).ToString();

                            objReturnData.Add(objInstance);

                            if (m == ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1)
                            {
                                intRowOfCurrentDetail = 0;
                                intCurrentDetail++;
                                break;
                            }
                        }
                    }
                }
                object[][] m_objRe = new object[objReturnData.Count][];

                for (int m = 0; m < objReturnData.Count; m++)
                    m_objRe[m] = (object[])objReturnData[m];
                return m_objRe;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return null;
            }
            #endregion
        }

        #region �Զ�����ͷ����
        private void m_dtgRecordDetail_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (MDIParent.m_objCurrentPatient == null)
                return;
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                System.Windows.Forms.DataGrid.HitTestInfo myHitTest = m_dtgRecordDetail.HitTest(e.X, e.Y);
                if (myHitTest.Column == 6 || myHitTest.Column == 7 || myHitTest.Column == 12 || myHitTest.Column == 13)
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
                case 6:
                    m_mthSaveColumnNameToDB("CUSTOM1name", ref m_strCustomColumn1);
                    m_dtcCUSTOM1.HeaderText = m_strCustomColumn1;
                    break;
                case 7:
                    m_mthSaveColumnNameToDB("CUSTOM2name", ref m_strCustomColumn2);
                    m_dtcCUSTOM2.HeaderText = m_strCustomColumn2;
                    break;
                case 12:
                    m_mthSaveColumnNameToDB("CUSTOM3name", ref m_strCustomColumn3);
                    m_dtcCUSTOM3.HeaderText = m_strCustomColumn3;
                    break;
                case 13:
                    m_mthSaveColumnNameToDB("CUSTOM4name", ref m_strCustomColumn4);
                    m_dtcCUSTOM4.HeaderText = m_strCustomColumn4;
                    break;
            }
        }

        private void m_mthSaveColumnNameToDB(string p_strColumnIndex, ref string p_strColumnName)
        {
            p_strColumnName = "";
            long lngRes = 0;
            //clsIntensiveTendRecord_GXService objServ =
            //    (clsIntensiveTendRecord_GXService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsIntensiveTendRecord_GXService));

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
            lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsIntensiveTendRecord_GXService_m_lngSetCustomColumnName(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), p_strColumnIndex, p_strColumnName);
            //			if(lngRes > 0)
            //			{		
            //				TreeNode trvTemp = m_trvInPatientDate.SelectedNode;
            //				m_trvInPatientDate.SelectedNode=null;
            //				m_trvInPatientDate.SelectedNode=trvTemp;
            //			}
        }
        #endregion

        #region ��ȡ�ܳ�����
        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="dtStartTime"></param>
        private double m_dblGetInSum(DateTime dtStartTime, DateTime dtEndTime)
        {
            double dblInSum = 0;
            double[] dblInSumArr = null;

            //clsIntensiveTendRecord_GXService objServ =
            //    (clsIntensiveTendRecord_GXService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsIntensiveTendRecord_GXService));

            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetInSum(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"),
                dtEndTime.ToString("yyyy-MM-dd HH:mm:ss"), dtStartTime.ToString("yyyy-MM-dd HH:mm:ss"), out dblInSumArr);

            if (lngRes > 0 && dblInSumArr != null)
            {
                for (int i = 0; i < dblInSumArr.Length; i++)
                {
                    dblInSum += dblInSumArr[i];
                }
            }
            //objServ.Dispose(); ;
            return dblInSum;
        }

        /// <summary>
        /// ��ȡ�ܳ���
        /// </summary>
        /// <param name="dtStartTime"></param>
        private double m_dblGetOutSum(DateTime dtStartTime, DateTime dtEndTime)
        {
            double dblOutSum = 0;
            double[] dblOutPissArr = null;
            double[] dblOutStoolArr = null;
            double[] p_dblCustom1Arr = null;
            double[] p_dblCustom2Arr = null;

            //clsIntensiveTendRecord_GXService objServ =
            //    (clsIntensiveTendRecord_GXService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsIntensiveTendRecord_GXService));

            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetOutSum(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"),
                dtEndTime.ToString("yyyy-MM-dd HH:mm:ss"), dtStartTime.ToString("yyyy-MM-dd HH:mm:ss"), out dblOutPissArr, out dblOutStoolArr, out p_dblCustom1Arr, out p_dblCustom2Arr);

            if (lngRes > 0 && dblOutPissArr != null && dblOutStoolArr != null && p_dblCustom1Arr != null && p_dblCustom2Arr != null)
            {
                for (int i = 0; i < dblOutPissArr.Length; i++)
                {
                    dblOutSum += dblOutPissArr[i];
                }
                for (int i = 0; i < dblOutStoolArr.Length; i++)
                {
                    dblOutSum += dblOutStoolArr[i];
                }
                for (int i = 0; i < p_dblCustom1Arr.Length; i++)
                {
                    dblOutSum += p_dblCustom1Arr[i];
                }
                for (int i = 0; i < p_dblCustom2Arr.Length; i++)
                {
                    dblOutSum += p_dblCustom2Arr[i];
                }
            }
            //objServ.Dispose();
            return dblOutSum;
        }
        #endregion

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

                    m_dtcCUSTOM1.HeaderText = "";
                    m_dtcCUSTOM2.HeaderText = "";
                    m_dtcCUSTOM3.HeaderText = "";
                    m_dtcCUSTOM4.HeaderText = "";
                    if (i1 == 0)
                    {
                        clsIntensiveTendRecord_GXDataInfo objITRCInfo = (clsIntensiveTendRecord_GXDataInfo)(objTansDataInfoArr[0]);
                        if (objITRCInfo != null && objITRCInfo.m_objRecordArr != null)
                        {
                            clsIntensiveTendRecord_GX objCurrent = objITRCInfo.m_objRecordArr[0];
                            if (objCurrent != null)
                            {
                                m_strCustomColumn1 = objCurrent.m_strCUSTOM1NAME == null ? "" : objCurrent.m_strCUSTOM1NAME;
                                m_strCustomColumn2 = objCurrent.m_strCUSTOM2NAME == null ? "" : objCurrent.m_strCUSTOM2NAME;
                                m_strCustomColumn3 = objCurrent.m_strCUSTOM3NAME == null ? "" : objCurrent.m_strCUSTOM3NAME;
                                m_strCustomColumn4 = objCurrent.m_strCUSTOM4NAME == null ? "" : objCurrent.m_strCUSTOM4NAME;
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

        /// <summary>
        /// �����Զ�����ͷ
        /// </summary>
        private void m_mthSetCustomColumnName()
        {
            m_dtcCUSTOM1.HeaderText = m_strCustomColumn1;
            m_dtcCUSTOM2.HeaderText = m_strCustomColumn2;
            m_dtcCUSTOM3.HeaderText = m_strCustomColumn3;
            m_dtcCUSTOM4.HeaderText = m_strCustomColumn4;
        }

        /// <summary>
        /// �Ծɼ�¼�����жϣ��Ƿ���ͬһ�����
        /// </summary>
        /// <param name="p_dtmMainRecord"></param>
        /// <param name="p_dtmDetail"></param>
        /// <returns></returns>
        private bool m_blnIsSameClass(DateTime p_dtmMainRecord, DateTime p_dtmDetail)
        {
            if (p_dtmMainRecord == DateTime.MinValue || p_dtmDetail == DateTime.MinValue)
            {
                return false;
            }

            if (m_intGetClass(p_dtmMainRecord) == m_intGetClass(p_dtmDetail))
                return true;
            else
                return false;
        }
        #region ��ӡ
        protected override void m_mthStartPrint()
        {
            //ȱʡʹ�ô�ӡԤ�����Ӵ��������ṩ�µ�ʵ��
            PageSetupDialog psd = new PageSetupDialog();
            try
            {
                if (m_pdcPrintDocument.DefaultPageSettings == null)
                {
                    m_pdcPrintDocument.DefaultPageSettings = new PageSettings();
                }
                m_pdcPrintDocument.DefaultPageSettings.Landscape = false;
                m_pdcPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 1024, 768);

                psd.PageSettings = m_pdcPrintDocument.DefaultPageSettings;

                if (psd.ShowDialog() != DialogResult.Cancel)
                {
                    m_pdcPrintDocument.DefaultPageSettings.Landscape = psd.PageSettings.Landscape;
                    m_pdcPrintDocument.DefaultPageSettings.PaperSize = psd.PageSettings.PaperSize;
                }
                else
                {
                    return;
                }

                if (m_blnDirectPrint)
                {
                    m_pdcPrintDocument.Print();
                }
                else
                {

                    ((clsIntensiveTendMain_GX_PrintTool)objPrintTool).m_mthPrintPage(m_pdcPrintDocument.DefaultPageSettings);

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
            string[] m_strColumnName = { m_strCustomColumn1, m_strCustomColumn2, m_strCustomColumn3, m_strCustomColumn4, this.m_txtDiagnose.Text.ToString() };
            return new clsIntensiveTendMain_GX_PrintTool(m_strColumnName);
        }
        #endregion

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

        #region ��дm_mthPerformSessionChanged���Ա���ʾ�Զ�����ͷ
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
                string m_strInPatientID = p_objSelectedSession.m_strEMRInpatientId;
                string m_strInPatientDate = p_objSelectedSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss");

                m_objCurrentPatient.m_DtmSelectedInDate = p_objSelectedSession.m_dtmEMRInpatientDate;
                m_objCurrentPatient.m_StrHISInPatientID = p_objSelectedSession.m_strHISInpatientId;
                m_objCurrentPatient.m_DtmSelectedHISInDate = p_objSelectedSession.m_dtmHISInpatientDate;

                m_objCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;
                m_objBaseCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;

                m_mthOnlySetPatientInfo(m_objCurrentPatient);

                m_mthIsReadOnly();
                if (!m_blnCanShowRecordContent())
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                //��ȡ���˼�¼�б�
                clsTransDataInfo[] objTansDataInfoArr;
                long lngRes = m_objRecordsDomain.m_lngGetTransDataInfoArr(m_strInPatientID, m_strInPatientDate, out objTansDataInfoArr);

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

                    m_dtcCUSTOM1.HeaderText = "";
                    m_dtcCUSTOM2.HeaderText = "";
                    m_dtcCUSTOM3.HeaderText = "";
                    m_dtcCUSTOM4.HeaderText = "";
                    if (i1 == 0)
                    {
                        clsIntensiveTendRecord_GXDataInfo objITRCInfo = (clsIntensiveTendRecord_GXDataInfo)(objTansDataInfoArr[0]);
                        if (objITRCInfo != null && objITRCInfo.m_objRecordArr != null)
                        {
                            clsIntensiveTendRecord_GX objCurrent = objITRCInfo.m_objRecordArr[0];
                            if (objCurrent != null)
                            {
                                m_strCustomColumn1 = objCurrent.m_strCUSTOM1NAME == null ? "" : objCurrent.m_strCUSTOM1NAME;
                                m_strCustomColumn2 = objCurrent.m_strCUSTOM2NAME == null ? "" : objCurrent.m_strCUSTOM2NAME;
                                m_strCustomColumn3 = objCurrent.m_strCUSTOM3NAME == null ? "" : objCurrent.m_strCUSTOM3NAME;
                                m_strCustomColumn4 = objCurrent.m_strCUSTOM4NAME == null ? "" : objCurrent.m_strCUSTOM4NAME;
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
