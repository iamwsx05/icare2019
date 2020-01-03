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
using com.digitalwave.Emr.StaticObject;

namespace iCare
{
    /// <summary>
    /// �����¼
    /// </summary>
    public class frmPostPartum_Acad : iCare.frmRecordsBase
    {
        #region system define
        private System.Windows.Forms.Label label1;
        private string m_strCurrentOpenDate = "";
        private string m_strCreateUserID = "";
        protected com.digitalwave.Utility.Controls.ctlTimePicker m_dtpChildBirthingDate;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.DataGridTextBoxColumn m_dtcRecordDate_chr;
        private System.Windows.Forms.GroupBox m_gpbAnam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private PinkieControls.ButtonXP m_cmdRECORDPERSON_CHR;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcPOSTPORTUM_NUM_CHR;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcUTERUSBOTTOM_CHR;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcMILKNUM_CHR;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcBREASTBULGE_CHR;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcNIPPLE_CHR;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcDEWNUM_CHR;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcDEWCOLOR_CHR;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcDEWFUCK_CHR;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcPERINEUM_CHR;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcURINE_CHR;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcANNOTATIONS_CHR;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcSCRTATOR_CHR;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcBP_CHR;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcUTERUSPINCH_CHR;
        private System.Windows.Forms.DataGridTextBoxColumn m_dtcTime_chr;
        protected ListView lsvSign;
        private ColumnHeader clmEmployeeName;
        private com.digitalwave.Controls.ctlMaskedTextBox m_txtTOTALBLOODNUM_CHR;
        private Label label6;
        private com.digitalwave.Controls.ctlMaskedTextBox m_txtSEWPIN_CHR;
        private Label label7;
        private com.digitalwave.controls.ctlRichTextBox m_rthESPECIALRECORD_CHR;
        private com.digitalwave.controls.ctlRichTextBox m_txtPERIOD_CHR;
        private Label label8;
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;
        #endregion
        protected com.digitalwave.Utility.Controls.ctlTimePicker m_dtpRecordDate;
        private Label label9;
        private Label label10;
        private Label label11;
        private frmHRPBaseForm objHRPBaseForm;
        protected clsTemplatesetInvoke m_objTempTool;

        clsEmrSignToolCollection m_objSign;
        private com.digitalwave.Utility.Controls.ctlComboBox m_cmbCurtClass;
        bool m_blnIsAddNew = true;
        #region constructor
        public frmPostPartum_Acad()
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();
            m_BlnNeedContextMenu = true;
            m_mthSetRichTextBoxAttribInControl(m_gpbAnam);
            m_objSign = new clsEmrSignToolCollection();
            //m_mthBindEmployeeSign(��ť,ǩ����,ҽ��1or��ʿ2,�����֤trueorfalse);
            m_objSign.m_mthBindEmployeeSign(m_cmdRECORDPERSON_CHR, lsvSign, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
            //�Ҽ��˵�
            m_objTempTool = new clsTemplatesetInvoke();
            objHRPBaseForm = new frmHRPBaseForm();
        }
        #endregion

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPostPartum_Acad));
            this.label1 = new System.Windows.Forms.Label();
            this.m_dtpChildBirthingDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.m_dtcRecordDate_chr = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcPOSTPORTUM_NUM_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcUTERUSBOTTOM_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcMILKNUM_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBREASTBULGE_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcNIPPLE_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcDEWNUM_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcDEWCOLOR_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcDEWFUCK_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPERINEUM_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcURINE_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcANNOTATIONS_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcSCRTATOR_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBP_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcUTERUSPINCH_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_gpbAnam = new System.Windows.Forms.GroupBox();
            this.m_cmbCurtClass = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_dtpRecordDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.m_txtPERIOD_CHR = new com.digitalwave.controls.ctlRichTextBox();
            this.m_rthESPECIALRECORD_CHR = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtSEWPIN_CHR = new com.digitalwave.Controls.ctlMaskedTextBox();
            this.m_txtTOTALBLOODNUM_CHR = new com.digitalwave.Controls.ctlMaskedTextBox();
            this.lsvSign = new System.Windows.Forms.ListView();
            this.clmEmployeeName = new System.Windows.Forms.ColumnHeader();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_cmdRECORDPERSON_CHR = new PinkieControls.ButtonXP();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.m_dtcTime_chr = new System.Windows.Forms.DataGridTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).BeginInit();
            this.m_pnlNewBase.SuspendLayout();
            this.m_gpbAnam.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgtsStyles
            // 
            this.dgtsStyles.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
                                                                                                         this.m_dtcRecordDate_chr,
                                                                                                         this.m_dtcPOSTPORTUM_NUM_CHR,
                                                                                                        m_dtcUTERUSBOTTOM_CHR,
                                                                                                        m_dtcUTERUSPINCH_CHR,
                                                                                                        m_dtcMILKNUM_CHR,
                                                                                                        m_dtcBREASTBULGE_CHR,
                                                                                                        m_dtcNIPPLE_CHR,
                                                                                                        m_dtcDEWNUM_CHR,
                                                                                                        m_dtcDEWCOLOR_CHR,
                                                                                                        m_dtcDEWFUCK_CHR,
                                                                                                        m_dtcPERINEUM_CHR,
                                                                                                        m_dtcBP_CHR,
                                                                                                        m_dtcURINE_CHR,
                                                                                                        m_dtcANNOTATIONS_CHR,
                                                                                                        m_dtcSCRTATOR_CHR,});
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
            this.m_dtgRecordDetail.Location = new System.Drawing.Point(8, 95);
            this.m_dtgRecordDetail.Size = new System.Drawing.Size(812, 353);
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
            this.m_trvInPatientDate.Location = new System.Drawing.Point(-44, 128);
            this.m_trvInPatientDate.Size = new System.Drawing.Size(198, 81);
            this.m_trvInPatientDate.TabIndex = 3000;
            this.m_trvInPatientDate.Visible = false;
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(466, 69);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(542, 64);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(355, 73);
            this.lblBedNoTitle.Size = new System.Drawing.Size(56, 14);
            this.lblBedNoTitle.Text = "��  ��:";
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(466, 75);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(513, 68);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(528, 64);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(513, 70);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(480, 75);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(315, 63);
            this.txtInPatientID.Size = new System.Drawing.Size(96, 23);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(329, 70);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(329, 65);
            this.m_txtBedNO.Size = new System.Drawing.Size(72, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(280, 70);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(-39, 105);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(-29, 105);
            this.m_lsvBedNO.Size = new System.Drawing.Size(96, 119);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(280, 72);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(352, 72);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(576, 63);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cmdNext.Location = new System.Drawing.Point(355, 68);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(400, 72);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(483, 70);
            this.m_lblForTitle.Size = new System.Drawing.Size(8, 23);
            this.m_lblForTitle.Visible = false;
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(512, 67);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(729, 38);
            this.m_cmdModifyPatientInfo.Size = new System.Drawing.Size(69, 26);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Size = new System.Drawing.Size(810, 60);
            this.m_pnlNewBase.Visible = true;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(808, 29);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 24);
            this.label1.TabIndex = 10000004;
            this.label1.Text = "����24Сʱ�ܳ�Ѫ����";
            // 
            // m_dtpChildBirthingDate
            // 
            this.m_dtpChildBirthingDate.BorderColor = System.Drawing.Color.Black;
            this.m_dtpChildBirthingDate.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
            this.m_dtpChildBirthingDate.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_dtpChildBirthingDate.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_dtpChildBirthingDate.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_dtpChildBirthingDate.flatFont = new System.Drawing.Font("����", 12F);
            this.m_dtpChildBirthingDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_dtpChildBirthingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpChildBirthingDate.Location = new System.Drawing.Point(88, 70);
            this.m_dtpChildBirthingDate.m_BlnOnlyTime = false;
            this.m_dtpChildBirthingDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.m_dtpChildBirthingDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.m_dtpChildBirthingDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.m_dtpChildBirthingDate.Name = "m_dtpChildBirthingDate";
            this.m_dtpChildBirthingDate.ReadOnly = false;
            this.m_dtpChildBirthingDate.Size = new System.Drawing.Size(149, 22);
            this.m_dtpChildBirthingDate.TabIndex = 4000;
            this.m_dtpChildBirthingDate.TextBackColor = System.Drawing.Color.White;
            this.m_dtpChildBirthingDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("����", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 10000004;
            this.label2.Text = "��������:";
            // 
            // m_dtcRecordDate_chr
            // 
            this.m_dtcRecordDate_chr.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcRecordDate_chr.Format = "";
            this.m_dtcRecordDate_chr.FormatInfo = null;
            this.m_dtcRecordDate_chr.MappingName = "RecordDate_chr";
            this.m_dtcRecordDate_chr.Width = 80;
            // 
            // m_dtcPOSTPORTUM_NUM_CHR
            // 
            this.m_dtcPOSTPORTUM_NUM_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcPOSTPORTUM_NUM_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPOSTPORTUM_NUM_CHR.m_BlnGobleSet = true;
            this.m_dtcPOSTPORTUM_NUM_CHR.m_BlnUnderLineDST = false;
            this.m_dtcPOSTPORTUM_NUM_CHR.MappingName = "POSTPORTUM_NUM_CHR";
            this.m_dtcPOSTPORTUM_NUM_CHR.Width = 30;
            // 
            // m_dtcUTERUSBOTTOM_CHR
            // 
            this.m_dtcUTERUSBOTTOM_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcUTERUSBOTTOM_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcUTERUSBOTTOM_CHR.m_BlnGobleSet = true;
            this.m_dtcUTERUSBOTTOM_CHR.m_BlnUnderLineDST = false;
            this.m_dtcUTERUSBOTTOM_CHR.MappingName = "UTERUSBOTTOM_CHR";
            this.m_dtcUTERUSBOTTOM_CHR.Width = 30;
            // 
            // m_dtcMILKNUM_CHR
            // 
            this.m_dtcMILKNUM_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcMILKNUM_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcMILKNUM_CHR.m_BlnGobleSet = true;
            this.m_dtcMILKNUM_CHR.m_BlnUnderLineDST = false;
            this.m_dtcMILKNUM_CHR.MappingName = "MILKNUM_CHR";
            this.m_dtcMILKNUM_CHR.Width = 30;
            // 
            // m_dtcBREASTBULGE_CHR
            // 
            this.m_dtcBREASTBULGE_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcBREASTBULGE_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBREASTBULGE_CHR.m_BlnGobleSet = true;
            this.m_dtcBREASTBULGE_CHR.m_BlnUnderLineDST = false;
            this.m_dtcBREASTBULGE_CHR.MappingName = "BREASTBULGE_CHR";
            this.m_dtcBREASTBULGE_CHR.Width = 30;
            // 
            // m_dtcNIPPLE_CHR
            // 
            this.m_dtcNIPPLE_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcNIPPLE_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcNIPPLE_CHR.m_BlnGobleSet = true;
            this.m_dtcNIPPLE_CHR.m_BlnUnderLineDST = false;
            this.m_dtcNIPPLE_CHR.MappingName = "NIPPLE_CHR";
            this.m_dtcNIPPLE_CHR.Width = 30;
            // 
            // m_dtcDEWNUM_CHR
            // 
            this.m_dtcDEWNUM_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcDEWNUM_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcDEWNUM_CHR.m_BlnGobleSet = true;
            this.m_dtcDEWNUM_CHR.m_BlnUnderLineDST = false;
            this.m_dtcDEWNUM_CHR.MappingName = "DEWNUM_CHR";
            this.m_dtcDEWNUM_CHR.Width = 30;
            // 
            // m_dtcDEWCOLOR_CHR
            // 
            this.m_dtcDEWCOLOR_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcDEWCOLOR_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcDEWCOLOR_CHR.m_BlnGobleSet = true;
            this.m_dtcDEWCOLOR_CHR.m_BlnUnderLineDST = false;
            this.m_dtcDEWCOLOR_CHR.MappingName = "DEWCOLOR_CHR";
            this.m_dtcDEWCOLOR_CHR.Width = 30;
            // 
            // m_dtcDEWFUCK_CHR
            // 
            this.m_dtcDEWFUCK_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcDEWFUCK_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcDEWFUCK_CHR.m_BlnGobleSet = true;
            this.m_dtcDEWFUCK_CHR.m_BlnUnderLineDST = false;
            this.m_dtcDEWFUCK_CHR.MappingName = "DEWFUCK_CHR";
            this.m_dtcDEWFUCK_CHR.Width = 30;
            // 
            // m_dtcPERINEUM_CHR
            // 
            this.m_dtcPERINEUM_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcPERINEUM_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPERINEUM_CHR.m_BlnGobleSet = true;
            this.m_dtcPERINEUM_CHR.m_BlnUnderLineDST = false;
            this.m_dtcPERINEUM_CHR.MappingName = "PERINEUM_CHR";
            this.m_dtcPERINEUM_CHR.Width = 75;
            // 
            // m_dtcURINE_CHR
            // 
            this.m_dtcURINE_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcURINE_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcURINE_CHR.m_BlnGobleSet = true;
            this.m_dtcURINE_CHR.m_BlnUnderLineDST = false;
            this.m_dtcURINE_CHR.MappingName = "URINE_CHR";
            this.m_dtcURINE_CHR.Width = 50;
            // 
            // m_dtcANNOTATIONS_CHR
            // 
            this.m_dtcANNOTATIONS_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcANNOTATIONS_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcANNOTATIONS_CHR.m_BlnGobleSet = true;
            this.m_dtcANNOTATIONS_CHR.m_BlnUnderLineDST = false;
            this.m_dtcANNOTATIONS_CHR.MappingName = "ANNOTATIONS_CHR";
            this.m_dtcANNOTATIONS_CHR.Width = 200;
            // 
            // m_dtcSCRTATOR_CHR
            // 
            this.m_dtcSCRTATOR_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcSCRTATOR_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcSCRTATOR_CHR.m_BlnGobleSet = true;
            this.m_dtcSCRTATOR_CHR.m_BlnUnderLineDST = false;
            this.m_dtcSCRTATOR_CHR.MappingName = "SCRTATOR_CHR";
            this.m_dtcSCRTATOR_CHR.Width = 130;
            // 
            // m_dtcBP_CHR
            // 
            this.m_dtcBP_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcBP_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBP_CHR.m_BlnGobleSet = true;
            this.m_dtcBP_CHR.m_BlnUnderLineDST = false;
            this.m_dtcBP_CHR.MappingName = "BP_CHR";
            this.m_dtcBP_CHR.Width = 30;
            // 
            // m_dtcUTERUSPINCH_CHR
            // 
            this.m_dtcUTERUSPINCH_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcUTERUSPINCH_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcUTERUSPINCH_CHR.m_BlnGobleSet = true;
            this.m_dtcUTERUSPINCH_CHR.m_BlnUnderLineDST = false;
            this.m_dtcUTERUSPINCH_CHR.MappingName = "UTERUSPINCH_CHR";
            this.m_dtcUTERUSPINCH_CHR.Width = 75;
            // 
            // m_gpbAnam
            // 
            this.m_gpbAnam.Controls.Add(this.m_cmbCurtClass);
            this.m_gpbAnam.Controls.Add(this.m_dtpRecordDate);
            this.m_gpbAnam.Controls.Add(this.m_txtPERIOD_CHR);
            this.m_gpbAnam.Controls.Add(this.m_rthESPECIALRECORD_CHR);
            this.m_gpbAnam.Controls.Add(this.m_txtSEWPIN_CHR);
            this.m_gpbAnam.Controls.Add(this.m_txtTOTALBLOODNUM_CHR);
            this.m_gpbAnam.Controls.Add(this.lsvSign);
            this.m_gpbAnam.Controls.Add(this.label5);
            this.m_gpbAnam.Controls.Add(this.label8);
            this.m_gpbAnam.Controls.Add(this.label7);
            this.m_gpbAnam.Controls.Add(this.label6);
            this.m_gpbAnam.Controls.Add(this.label9);
            this.m_gpbAnam.Controls.Add(this.label4);
            this.m_gpbAnam.Controls.Add(this.label3);
            this.m_gpbAnam.Controls.Add(this.label1);
            this.m_gpbAnam.Controls.Add(this.m_cmdRECORDPERSON_CHR);
            this.m_gpbAnam.Controls.Add(this.label11);
            this.m_gpbAnam.Controls.Add(this.label10);
            this.m_gpbAnam.Location = new System.Drawing.Point(8, 448);
            this.m_gpbAnam.Name = "m_gpbAnam";
            this.m_gpbAnam.Size = new System.Drawing.Size(811, 166);
            this.m_gpbAnam.TabIndex = 6000;
            this.m_gpbAnam.TabStop = false;
            this.m_gpbAnam.Text = "��ע";
            // 
            // m_cmbCurtClass
            // 
            this.m_cmbCurtClass.AccessibleDescription = "��ע>>�����˿ڲ���>>���ϼ���";
            this.m_cmbCurtClass.BackColor = System.Drawing.SystemColors.Window;
            this.m_cmbCurtClass.BorderColor = System.Drawing.Color.Black;
            this.m_cmbCurtClass.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cmbCurtClass.DropButtonCursor = System.Windows.Forms.Cursors.Default;
            this.m_cmbCurtClass.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cmbCurtClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cmbCurtClass.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmbCurtClass.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmbCurtClass.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cmbCurtClass.ListBackColor = System.Drawing.Color.White;
            this.m_cmbCurtClass.ListForeColor = System.Drawing.Color.Black;
            this.m_cmbCurtClass.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cmbCurtClass.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cmbCurtClass.Location = new System.Drawing.Point(84, 100);
            this.m_cmbCurtClass.m_BlnEnableItemEventMenu = true;
            this.m_cmbCurtClass.Name = "m_cmbCurtClass";
            this.m_cmbCurtClass.SelectedIndex = -1;
            this.m_cmbCurtClass.SelectedItem = null;
            this.m_cmbCurtClass.SelectionStart = 0;
            this.m_cmbCurtClass.Size = new System.Drawing.Size(210, 23);
            this.m_cmbCurtClass.TabIndex = 10001401;
            this.m_cmbCurtClass.TextBackColor = System.Drawing.Color.White;
            this.m_cmbCurtClass.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_dtpRecordDate
            // 
            this.m_dtpRecordDate.BorderColor = System.Drawing.Color.Black;
            this.m_dtpRecordDate.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
            this.m_dtpRecordDate.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_dtpRecordDate.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_dtpRecordDate.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_dtpRecordDate.flatFont = new System.Drawing.Font("����", 12F);
            this.m_dtpRecordDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_dtpRecordDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpRecordDate.Location = new System.Drawing.Point(84, 21);
            this.m_dtpRecordDate.m_BlnOnlyTime = false;
            this.m_dtpRecordDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.m_dtpRecordDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.m_dtpRecordDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.m_dtpRecordDate.Name = "m_dtpRecordDate";
            this.m_dtpRecordDate.ReadOnly = false;
            this.m_dtpRecordDate.Size = new System.Drawing.Size(213, 22);
            this.m_dtpRecordDate.TabIndex = 10000046;
            this.m_dtpRecordDate.TextBackColor = System.Drawing.Color.White;
            this.m_dtpRecordDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_txtPERIOD_CHR
            // 
            this.m_txtPERIOD_CHR.AccessibleDescription = "";
            this.m_txtPERIOD_CHR.BackColor = System.Drawing.Color.White;
            this.m_txtPERIOD_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtPERIOD_CHR.ForeColor = System.Drawing.Color.Black;
            this.m_txtPERIOD_CHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtPERIOD_CHR.Location = new System.Drawing.Point(84, 102);
            this.m_txtPERIOD_CHR.m_BlnIgnoreUserInfo = false;
            this.m_txtPERIOD_CHR.m_BlnPartControl = false;
            this.m_txtPERIOD_CHR.m_BlnReadOnly = false;
            this.m_txtPERIOD_CHR.m_BlnUnderLineDST = false;
            this.m_txtPERIOD_CHR.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtPERIOD_CHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtPERIOD_CHR.m_IntCanModifyTime = 6;
            this.m_txtPERIOD_CHR.m_IntPartControlLength = 0;
            this.m_txtPERIOD_CHR.m_IntPartControlStartIndex = 0;
            this.m_txtPERIOD_CHR.m_StrUserID = "";
            this.m_txtPERIOD_CHR.m_StrUserName = "";
            this.m_txtPERIOD_CHR.Multiline = false;
            this.m_txtPERIOD_CHR.Name = "m_txtPERIOD_CHR";
            this.m_txtPERIOD_CHR.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtPERIOD_CHR.Size = new System.Drawing.Size(142, 23);
            this.m_txtPERIOD_CHR.TabIndex = 10001200;
            this.m_txtPERIOD_CHR.Text = "";
            this.m_txtPERIOD_CHR.Visible = false;
            // 
            // m_rthESPECIALRECORD_CHR
            // 
            this.m_rthESPECIALRECORD_CHR.AccessibleDescription = "��ע>>�����¼";
            this.m_rthESPECIALRECORD_CHR.BackColor = System.Drawing.Color.White;
            this.m_rthESPECIALRECORD_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_rthESPECIALRECORD_CHR.ForeColor = System.Drawing.Color.Black;
            this.m_rthESPECIALRECORD_CHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_rthESPECIALRECORD_CHR.Location = new System.Drawing.Point(313, 45);
            this.m_rthESPECIALRECORD_CHR.m_BlnIgnoreUserInfo = false;
            this.m_rthESPECIALRECORD_CHR.m_BlnPartControl = false;
            this.m_rthESPECIALRECORD_CHR.m_BlnReadOnly = false;
            this.m_rthESPECIALRECORD_CHR.m_BlnUnderLineDST = false;
            this.m_rthESPECIALRECORD_CHR.m_ClrDST = System.Drawing.Color.Red;
            this.m_rthESPECIALRECORD_CHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_rthESPECIALRECORD_CHR.m_IntCanModifyTime = 6;
            this.m_rthESPECIALRECORD_CHR.m_IntPartControlLength = 0;
            this.m_rthESPECIALRECORD_CHR.m_IntPartControlStartIndex = 0;
            this.m_rthESPECIALRECORD_CHR.m_StrUserID = "";
            this.m_rthESPECIALRECORD_CHR.m_StrUserName = "";
            this.m_rthESPECIALRECORD_CHR.Name = "m_rthESPECIALRECORD_CHR";
            this.m_rthESPECIALRECORD_CHR.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_rthESPECIALRECORD_CHR.Size = new System.Drawing.Size(492, 79);
            this.m_rthESPECIALRECORD_CHR.TabIndex = 10001300;
            this.m_rthESPECIALRECORD_CHR.Text = "";
            // 
            // m_txtSEWPIN_CHR
            // 
            this.m_txtSEWPIN_CHR.AccessibleDescription = "��ע>>�����˿ڲ���>>�������";
            this.m_txtSEWPIN_CHR.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.m_txtSEWPIN_CHR.Location = new System.Drawing.Point(152, 74);
            this.m_txtSEWPIN_CHR.Mask = "999";
            this.m_txtSEWPIN_CHR.Name = "m_txtSEWPIN_CHR";
            this.m_txtSEWPIN_CHR.PromptChar = ' ';
            this.m_txtSEWPIN_CHR.Size = new System.Drawing.Size(50, 23);
            this.m_txtSEWPIN_CHR.TabIndex = 10001100;
            this.m_txtSEWPIN_CHR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtSEWPIN_CHR.ValidatingType = typeof(int);
            this.m_txtSEWPIN_CHR.Visible = false;
            // 
            // m_txtTOTALBLOODNUM_CHR
            // 
            this.m_txtTOTALBLOODNUM_CHR.AccessibleDescription = "��ע>>����24Сʱ�ܳ�Ѫ��";
            this.m_txtTOTALBLOODNUM_CHR.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.m_txtTOTALBLOODNUM_CHR.Location = new System.Drawing.Point(153, 60);
            this.m_txtTOTALBLOODNUM_CHR.Mask = "99999";
            this.m_txtTOTALBLOODNUM_CHR.Name = "m_txtTOTALBLOODNUM_CHR";
            this.m_txtTOTALBLOODNUM_CHR.PromptChar = ' ';
            this.m_txtTOTALBLOODNUM_CHR.Size = new System.Drawing.Size(50, 23);
            this.m_txtTOTALBLOODNUM_CHR.TabIndex = 10001000;
            this.m_txtTOTALBLOODNUM_CHR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtTOTALBLOODNUM_CHR.ValidatingType = typeof(int);
            // 
            // lsvSign
            // 
            this.lsvSign.BackColor = System.Drawing.Color.White;
            this.lsvSign.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmEmployeeName});
            this.lsvSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lsvSign.ForeColor = System.Drawing.Color.Black;
            this.lsvSign.FullRowSelect = true;
            this.lsvSign.GridLines = true;
            this.lsvSign.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvSign.Location = new System.Drawing.Point(347, 131);
            this.lsvSign.Name = "lsvSign";
            this.lsvSign.Size = new System.Drawing.Size(458, 28);
            this.lsvSign.TabIndex = 10000043;
            this.lsvSign.UseCompatibleStateImageBehavior = false;
            this.lsvSign.View = System.Windows.Forms.View.SmallIcon;
            // 
            // clmEmployeeName
            // 
            this.clmEmployeeName.Width = 55;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(311, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(329, 14);
            this.label5.TabIndex = 10000010;
            this.label5.Text = "�����¼������¼��Ժʱ�������������ͻ�������";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(229, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 14);
            this.label8.TabIndex = 10000008;
            this.label8.Text = "��";
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(204, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 14);
            this.label7.TabIndex = 10000008;
            this.label7.Text = "��";
            this.label7.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(205, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 24);
            this.label6.TabIndex = 10000008;
            this.label6.Text = "ml";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 14);
            this.label9.TabIndex = 10000008;
            this.label9.Text = "��¼ʱ�䣺";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 10000008;
            this.label4.Text = "���������";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 14);
            this.label3.TabIndex = 10000006;
            this.label3.Text = "�����˿ڲ��ߣ����";
            this.label3.Visible = false;
            // 
            // m_cmdRECORDPERSON_CHR
            // 
            this.m_cmdRECORDPERSON_CHR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdRECORDPERSON_CHR.DefaultScheme = true;
            this.m_cmdRECORDPERSON_CHR.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdRECORDPERSON_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdRECORDPERSON_CHR.Hint = "";
            this.m_cmdRECORDPERSON_CHR.Location = new System.Drawing.Point(272, 131);
            this.m_cmdRECORDPERSON_CHR.Name = "m_cmdRECORDPERSON_CHR";
            this.m_cmdRECORDPERSON_CHR.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdRECORDPERSON_CHR.Size = new System.Drawing.Size(69, 28);
            this.m_cmdRECORDPERSON_CHR.TabIndex = 10001400;
            this.m_cmdRECORDPERSON_CHR.Tag = "1";
            this.m_cmdRECORDPERSON_CHR.Text = "��¼��";
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Location = new System.Drawing.Point(307, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(498, 112);
            this.label11.TabIndex = 10000047;
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Location = new System.Drawing.Point(6, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(297, 112);
            this.label10.TabIndex = 10000047;
            // 
            // m_dtcTime_chr
            // 
            this.m_dtcTime_chr.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcTime_chr.Format = "";
            this.m_dtcTime_chr.FormatInfo = null;
            this.m_dtcTime_chr.MappingName = "Time_chr";
            this.m_dtcTime_chr.Width = 45;
            // 
            // frmPostPartum_Acad
            // 
            this.AccessibleDescription = "�����¼";
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(776, 643);
            this.Controls.Add(this.m_gpbAnam);
            this.Controls.Add(this.m_dtpChildBirthingDate);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPostPartum_Acad";
            this.Text = "�����¼";
            this.Load += new System.EventHandler(this.frmPostPartum_Acad_Load);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.m_trvInPatientDate, 0);
            this.Controls.SetChildIndex(this.m_dtgRecordDetail, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_dtpChildBirthingDate, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.m_gpbAnam, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).EndInit();
            this.m_pnlNewBase.ResumeLayout(false);
            this.m_gpbAnam.ResumeLayout(false);
            this.m_gpbAnam.PerformLayout();
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

        // ��ʼ���������DataTable��(��Ҫ�Ķ�)
        // ע�⣬DataTable�ĵ�һ��Column�����Ǵ�ż�¼��CreateDateʱ����ַ������ڶ���Column�����Ǵ�ż�¼���͵�intֵ��������Column�����Ǵ�ż�¼��OpenDate
        protected override void m_mthInitDataTable(DataTable p_dtbRecordTable)
        {

            //��ż�¼��CreateDateʱ����ַ���
            p_dtbRecordTable.Columns.Add("RecordDate");//0
                                                       //��ż�¼���͵�intֵ
            DataColumn dcRecordType = new DataColumn("RecordType", typeof(int));
            p_dtbRecordTable.Columns.Add(dcRecordType);//1
                                                       //��ż�¼��OpenDate�ַ���
            p_dtbRecordTable.Columns.Add("OpenDate");  //2
                                                       //��ż�¼��ModifyDate�ַ���
            p_dtbRecordTable.Columns.Add("ModifyDate"); //3

            DataColumn dc1 = p_dtbRecordTable.Columns.Add("RecordDate_chr");//4
            dc1.DefaultValue = "";
            DataColumn dc2 = p_dtbRecordTable.Columns.Add("Time_chr");//5
            dc2.DefaultValue = "";


            p_dtbRecordTable.Columns.Add("POSTPORTUM_NUM_CHR", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//6

            p_dtbRecordTable.Columns.Add("UTERUSBOTTOM_CHR", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//7

            p_dtbRecordTable.Columns.Add("UTERUSPINCH_CHR", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//8
            p_dtbRecordTable.Columns.Add("MILKNUM_CHR", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//9
            p_dtbRecordTable.Columns.Add("BREASTBULGE_CHR", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//10
            p_dtbRecordTable.Columns.Add("NIPPLE_CHR", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//11
            p_dtbRecordTable.Columns.Add("DEWNUM_CHR", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//12	
            p_dtbRecordTable.Columns.Add("DEWCOLOR_CHR", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//13
            p_dtbRecordTable.Columns.Add("DEWFUCK_CHR", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//14
            p_dtbRecordTable.Columns.Add("PERINEUM_CHR", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//15
            p_dtbRecordTable.Columns.Add("BP_CHR", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//16
            p_dtbRecordTable.Columns.Add("URINE_CHR", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//17
            p_dtbRecordTable.Columns.Add("ANNOTATIONS_CHR", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//18
            p_dtbRecordTable.Columns.Add("SCRTATOR_CHR", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//19

            p_dtbRecordTable.Columns.Add("RecordAndDetailSign");//20
            p_dtbRecordTable.Columns.Add("CreateUserID");//21


            //			m_dtcGenaralInstance.m_RtbBase.m_BlnReadOnly = true;
            m_mthSetControl(m_dtcRecordDate_chr);
            m_mthSetControl(m_dtcTime_chr);

            m_mthSetControl(m_dtcPOSTPORTUM_NUM_CHR);
            m_mthSetControl(m_dtcUTERUSBOTTOM_CHR);
            m_mthSetControl(m_dtcUTERUSPINCH_CHR);
            m_mthSetControl(m_dtcMILKNUM_CHR);
            m_mthSetControl(m_dtcBREASTBULGE_CHR);
            m_mthSetControl(m_dtcNIPPLE_CHR);
            m_mthSetControl(m_dtcDEWNUM_CHR);
            m_mthSetControl(m_dtcDEWCOLOR_CHR);
            m_mthSetControl(m_dtcDEWFUCK_CHR);
            m_mthSetControl(m_dtcPERINEUM_CHR);
            m_mthSetControl(m_dtcBP_CHR);
            m_mthSetControl(m_dtcURINE_CHR);
            m_mthSetControl(m_dtcANNOTATIONS_CHR);
            m_mthSetControl(m_dtcSCRTATOR_CHR);


            //����������
            this.m_dtcRecordDate_chr.HeaderText = "��\r\n\r\n\r\n\r\n\r\n\r\n��";
            this.m_dtcTime_chr.HeaderText = "ʱ\r\n\r\n\r\n\r\n\r\n\r\n��";

            this.m_dtcPOSTPORTUM_NUM_CHR.HeaderText = "��\r\n\r\n��\r\n\r\n��\r\n\r\n��";
            this.m_dtcUTERUSBOTTOM_CHR.HeaderText = "��\r\n��\r\n��\r\n��\r\ncm";

            this.m_dtcUTERUSPINCH_CHR.HeaderText = "��\r\n��\r\n��\r\n��\r\n��\r\n��";

            this.m_dtcMILKNUM_CHR.HeaderText = "��\r\n\r\n��\r\n\r\n��\r\n\r\n��";
            this.m_dtcBREASTBULGE_CHR.HeaderText = "��\r\n\r\n��\r\n\r\n��\r\n\r\n��";
            this.m_dtcNIPPLE_CHR.HeaderText = "��\r\n\r\n��\r\n\r\n��\r\n\r\nͷ";
            this.m_dtcDEWNUM_CHR.HeaderText = "��\r\n\r\n¶\r\n\r\n\r\n\r\n��";
            this.m_dtcDEWCOLOR_CHR.HeaderText = "��\r\n\r\n¶\r\n\r\n\r\n\r\nɫ";

            this.m_dtcDEWFUCK_CHR.HeaderText = "��\r\n\r\n¶\r\n\r\n��\r\n\r\nζ";
            this.m_dtcPERINEUM_CHR.HeaderText = "��\r\n\r\n��\r\n\r\n��\r\n\r\n��";
            this.m_dtcBP_CHR.HeaderText = "BP\r\n\r\n\r\n\r\n\r\n\r\nmmHg";
            this.m_dtcURINE_CHR.HeaderText = "��";
            this.m_dtcANNOTATIONS_CHR.HeaderText = "��\r\n\r\n\r\n\r\nע";
            this.m_dtcSCRTATOR_CHR.HeaderText = "��\r\n\r\n��\r\n\r\n��";


        }


        protected override void m_mthAddRichTemplateInContainer(Control p_ctlContainer)
        {
            m_mthSetRichTemplateInContainer(m_gpbAnam);
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

        //(��Ҫ�Ķ�)
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

            m_mthClearLaycountAll();
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

        // ��ȡ���̼�¼�������ʵ��(��Ҫ�Ķ�)
        protected override clsRecordsDomain m_objGetRecordsDomain()
        {
            return new clsRecordsDomain(enmRecordsType.PostPartum_Acad);
        }

        // ��ȡ��¼����Ҫ��Ϣ�������ȡ����CreateDate,OpenDate,LastModifyDate��
        protected override clsTrackRecordContent m_objGetRecordMainContent(int p_intRecordType,
            object[] p_objDataArr)
        {
            //���� p_intRecordType ��ȡ��Ӧ�� clsTrackRecordContent
            clsTrackRecordContent objContent = null;
            //(��Ҫ�Ķ�)
            switch ((enmDiseaseTrackType)p_intRecordType)
            {
                case enmDiseaseTrackType.PostPartum_Acad:
                    objContent = new clsIcuAcad_PostPartumRecord_Value();//(��Ҫ�Ķ�)
                    break;
            }

            if (objContent == null)
                objContent = new clsIcuAcad_PostPartumRecord_Value();   //(��Ҫ�Ķ�)

            if (m_objCurrentPatient != null)
                objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            else
            {
                clsPublicFunction.ShowInformationMessageBox("��ǰ����Ϊ��!");
                return null;
            }
            objContent.m_dtmInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
            objContent.m_dtmRecordDate = DateTime.Parse((string)p_objDataArr[0]);
            objContent.m_dtmCreateDate = DateTime.Parse((string)p_objDataArr[2]);
            objContent.m_dtmOpenDate = DateTime.Parse((string)p_objDataArr[2]);
            objContent.m_dtmModifyDate = DateTime.Parse((string)p_objDataArr[3]);

            objContent.m_strCreateUserID = (string)p_objDataArr[21];
            return objContent;
        }

        private void frmPostPartum_Acad_Load(object sender, System.EventArgs e)
        {
            if (m_BlnNeedContextMenu)
                m_mthAddRichTemplateInContainer(this);
            m_dtmPreRecordDate = DateTime.MinValue;
            m_dtgRecordDetail.Focus();
            m_mniAddBlank.Visible = false;
            m_mniDeleteBlank.Visible = false;

        }


        // ��ȡ������Ӻ��޸ģ���¼�Ĵ��塣
        protected override frmDiseaseTrackBase m_frmGetRecordForm(int p_intRecordType)
        {
            switch ((enmDiseaseTrackType)p_intRecordType)
            {
                case enmDiseaseTrackType.PostPartum_Acad://(��Ҫ�Ķ�)
                    {
                        frmPostPartum_AcadCon frmwcon = new frmPostPartum_AcadCon();
                        //frmwcon.m_strTOTALBLOODNUM_CHR = m_txtTOTALBLOODNUM_CHR.Text;
                        //frmwcon.m_strSEWPIN_CHR = m_txtSEWPIN_CHR.Text;
                        //frmwcon.m_strESPECIALRECORD_CHR = m_rthESPECIALRECORD_CHR.Text;
                        //frmwcon.m_strPERIOD_CHR = m_txtPERIOD_CHR.Text;
                        //frmwcon.m_strCHILDBIRTHINGDATE = m_dtpChildBirthingDate.Value.ToString();
                        //frmwcon.m_strRECORDPERSON_CHR = m_txtRECORDPERSON_CHR.Text;

                        return frmwcon;//(��Ҫ�Ķ�)
                        break;
                    }
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
            m_mthAddNewRecord((int)enmDiseaseTrackType.PostPartum_Acad);//(��Ҫ�Ķ�)
        }

        protected override infPrintRecord m_objGetPrintTool()
        {
            clsPostPartum_Acad_PrintTool frmwcon = new clsPostPartum_Acad_PrintTool();
            return frmwcon;
        }

        private void m_mthClearLaycountAll()
        {
            #region ��ո�ע

            m_txtTOTALBLOODNUM_CHR.m_mthClearValue();
            m_txtSEWPIN_CHR.m_mthClearValue();
            //m_rthESPECIALRECORD_CHR.m_mthClearText();
            m_cmbCurtClass.Text = string.Empty;
            m_dtpChildBirthingDate.Value = System.DateTime.Now;
            m_dtpRecordDate.Value = System.DateTime.Now;
            lsvSign.Items.Clear();
            m_rthESPECIALRECORD_CHR.Clear();
            m_gpbAnam.Tag = null;
            #endregion
        }
        protected override object[][] m_objGetRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
        {
            return null;
        }
        private void m_cmdSave_Click(object sender, System.EventArgs e)
        {
        }

        protected override bool m_BlnIsAddNew
        {
            get
            {
                return m_blnIsAddNew;
            }
        }
        #region �Է������ڣ��룬ML�����������¼�Ĵ���

        protected override long m_lngSubModify()
        {
            if (/*m_trvInPatientDate.SelectedNode == null || m_trvInPatientDate.SelectedNode == m_trvInPatientDate.Nodes[0] || */m_objCurrentPatient == null)
            {
                clsPublicFunction.ShowInformationMessageBox("δѡ�����˻��¼,�޷�����!");
                return (long)enmOperationResult.Parameter_Error;
            }

            //com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService objServ =
            //    (com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService));

            long lngRes = 0;
            clsPostPartumManno_Value objValue = m_gpbAnam.Tag as clsPostPartumManno_Value;
            if (objValue == null)
                return (long)enmOperationResult.DB_Fail;
            objValue.m_strModifyUserID = clsEMR_StaticObject.s_ObjCurrentEmployee.m_strEMPID_CHR;
            objValue.m_dtmModifyDate = new clsPublicDomain().m_dtmGetServerTime();

            objValue.m_dtmChildBirthingDate = DateTime.Parse(m_dtpChildBirthingDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            objValue.m_dtmRecordDate = DateTime.Parse(m_dtpRecordDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            objValue.m_strESPECIALRECORD_CHR = m_rthESPECIALRECORD_CHR.Text;
            objValue.m_strESPECIALRECORD_CHR_RIGHT = m_rthESPECIALRECORD_CHR.m_strGetRightText();
            objValue.m_strESPECIALRECORD_CHRXML = m_rthESPECIALRECORD_CHR.m_strGetXmlText();

            objValue.m_strPERIOD_CHR = m_cmbCurtClass.Text;
            objValue.m_strPERIOD_CHR_RIGHT = m_cmbCurtClass.Text;
            objValue.m_strPERIOD_CHRXML = "";

            objValue.m_strSEWPIN_CHR = m_txtSEWPIN_CHR.m_objGetValue();
            objValue.m_strSEWPIN_CHR_RIGHT = m_txtSEWPIN_CHR.m_objGetValue();

            objValue.m_strTOTALBLOODNUM_CHR = m_txtTOTALBLOODNUM_CHR.m_objGetValue();
            objValue.m_strTOTALBLOODNUM_CHR_RIGHT = m_txtTOTALBLOODNUM_CHR.m_objGetValue();

            string strUserIDList = "";
            string strUserNameList = "";
            m_mthGetSignArr(new Control[] { lsvSign }, ref objValue.objSignerArr, ref strUserIDList, ref strUserNameList);
            objValue.m_strRecordUserID = strUserIDList;

            #region ��ǩ��ʱ��֤����ǩ���� ������

            //����ǩ�� 
            //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
            clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
            objSign_VO.m_strFORMID_VCHR = this.Name;
            objSign_VO.m_strFORMRECORDID_VCHR = m_objCurrentPatient.m_StrInPatientID.Trim() + "-" + m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss");
            objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
            objSign_VO.m_strRegisterId = m_objBaseCurrentPatient.m_StrRegisterId;
            if (objValue.objSignerArr != null)
            {
                ArrayList objSignerArr = new ArrayList();
                for (int i = 0; i < objValue.objSignerArr.Length; i++)
                {
                    if (objValue.objSignerArr[i].controlName == "lsvSign" || objValue.objSignerArr[i].controlName == "m_txtSign")
                        objSignerArr.Add(objValue.objSignerArr[i].objEmployee);
                }
                clsCheckSignersController objCheck = new clsCheckSignersController(objSignerArr, false);
                if (objCheck.CheckSigner(objValue, objSign_VO) == -1)
                    return -1;

            }
            else
            {
                clsCheckSignersController objCheck = new clsCheckSignersController();
                if (objCheck.m_lngSign(objValue, objSign_VO) == -1)
                    return -1;
            }
            #endregion
            lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngModifyRecord(objValue);
            return lngRes;
        }
        protected override long m_lngSubAddNew()
        {
            if (/*m_trvInPatientDate.SelectedNode == null || m_trvInPatientDate.SelectedNode == m_trvInPatientDate.Nodes[0] || */m_objCurrentPatient == null)
            {
                clsPublicFunction.ShowInformationMessageBox("δѡ�����˻��¼,�޷�����!");
                return (long)enmOperationResult.Parameter_Error;
            }
            if (this.lsvSign.Items.Count == 0)
            {
                clsPublicFunction.ShowInformationMessageBox("��¼��û��ǩ��!");
                return (long)enmOperationResult.Parameter_Error;
            }

            //com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService objServ =
            //    (com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService));

            long lngRes = 0;
            clsPostPartumManno_Value objValue = new clsPostPartumManno_Value();
            objValue.m_strRegisterID = m_objCurrentPatient.m_StrRegisterId;
            objValue.m_dtmCreateDate = new clsPublicDomain().m_dtmGetServerTime();
            objValue.m_dtmModifyDate = objValue.m_dtmCreateDate;
            objValue.m_strCreateUserID = clsEMR_StaticObject.s_ObjCurrentEmployee.m_strEMPID_CHR;
            objValue.m_strModifyUserID = objValue.m_strCreateUserID;
            objValue.m_dtmChildBirthingDate = DateTime.Parse(m_dtpChildBirthingDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            objValue.m_dtmRecordDate = DateTime.Parse(m_dtpRecordDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            objValue.m_strESPECIALRECORD_CHR = m_rthESPECIALRECORD_CHR.Text;
            objValue.m_strESPECIALRECORD_CHR_RIGHT = m_rthESPECIALRECORD_CHR.m_strGetRightText();
            objValue.m_strESPECIALRECORD_CHRXML = m_rthESPECIALRECORD_CHR.m_strGetXmlText();

            objValue.m_strPERIOD_CHR = m_cmbCurtClass.Text;
            objValue.m_strPERIOD_CHR_RIGHT = m_cmbCurtClass.Text;
            objValue.m_strPERIOD_CHRXML = "";

            objValue.m_strSEWPIN_CHR = m_txtSEWPIN_CHR.m_objGetValue();
            objValue.m_strSEWPIN_CHR_RIGHT = m_txtSEWPIN_CHR.m_objGetValue();

            objValue.m_strTOTALBLOODNUM_CHR = m_txtTOTALBLOODNUM_CHR.m_objGetValue();
            objValue.m_strTOTALBLOODNUM_CHR_RIGHT = m_txtTOTALBLOODNUM_CHR.m_objGetValue();

            string strUserIDList = "";
            string strUserNameList = "";
            m_mthGetSignArr(new Control[] { lsvSign }, ref objValue.objSignerArr, ref strUserIDList, ref strUserNameList);
            objValue.m_strRecordUserID = strUserIDList;

            #region ��ǩ��ʱ��֤����ǩ���� ������

            //����ǩ�� 
            //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
            clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
            objSign_VO.m_strFORMID_VCHR = this.Name;
            objSign_VO.m_strFORMRECORDID_VCHR = m_objCurrentPatient.m_StrInPatientID.Trim() + "-" + m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss");
            objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
            objSign_VO.m_strRegisterId = m_objBaseCurrentPatient.m_StrRegisterId;
            if (objValue.objSignerArr != null)
            {
                ArrayList objSignerArr = new ArrayList();
                for (int i = 0; i < objValue.objSignerArr.Length; i++)
                {
                    if (objValue.objSignerArr[i].controlName == "lsvSign" || objValue.objSignerArr[i].controlName == "m_txtSign")
                        objSignerArr.Add(objValue.objSignerArr[i].objEmployee);
                }
                clsCheckSignersController objCheck = new clsCheckSignersController(objSignerArr, false);
                if (objCheck.CheckSigner(objValue, objSign_VO) == -1)
                    return -1;
            }
            else
            {
                clsCheckSignersController objCheck = new clsCheckSignersController();
                if (objCheck.m_lngSign(objValue, objSign_VO) == -1)
                    return -1;
            }
            #endregion
            lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngAddNewRecord(objValue);
            return lngRes;
        }

        #endregion
        private object[][] m_objGetRecordsValue(clsPostPartumArr p_objTransDataInfo)
        {
            #region ��ʾ��¼��DataGrid
            try
            {
                if (p_objTransDataInfo == null)
                    return null;

                object[] objData;
                ArrayList objReturnData = new ArrayList();

                clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
                string strText, strXml;

                clsPostPartumArr objICUInfo = p_objTransDataInfo;



                int intRecordCount = objICUInfo.m_objRecordArr.Length;
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
                    objData = new object[22];   // DataTable������
                    clsIcuAcad_PostPartumRecord_Value objCurrent = objICUInfo.m_objRecordArr[i];
                    clsIcuAcad_PostPartumRecord_Value objNext = new clsIcuAcad_PostPartumRecord_Value();//��һ����¼//(��Ҫ�Ķ�)
                    if (i < intRecordCount - 1)
                        objNext = objICUInfo.m_objRecordArr[i + 1];

                    //����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ�����ʾ
                    //if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate)
                    //{
                    //    TimeSpan tsModify = objNext.m_dtmModifyDate - objCurrent.m_dtmModifyDate;
                    //    if ((int)tsModify.TotalHours < intCanModifyTime)
                    //        continue;
                    //}


                    #region ��Źؼ��ֶ�
                    if (objCurrent.m_dtmCreateDate != DateTime.MinValue)
                    {
                        objData[0] = objCurrent.m_dtmCreateDate;//��ż�¼CreateDate��ʱ����ַ���
                        objData[1] = (int)enmRecordsType.PostPartum_Acad;//��ż�¼���͵�intֵ  //(��Ҫ�Ķ�)
                        objData[2] = objCurrent.m_dtmCreateDate;//��ż�¼��OpenDate�ַ���
                        objData[3] = objCurrent.m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   
                        //objData[3] = objICUInfo.m_objRecordArr[objICUInfo.m_objRecordArr.Length - 1].m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   


                        //ͬһ����ֻ�ڵ�һ����ʾ����
                        if (objCurrent.m_dtmRecordDate.Date.ToString() != m_dtmPreRecordDate.Date.ToString())//m_dtmRECORDDATE
                        {
                            objData[4] = objCurrent.m_dtmRecordDate.Date.ToString("yyyy-MM-dd");//�����ַ���
                        }
                        //�޸ĺ���кۼ��ļ�¼������ʾʱ��
                        //if (m_dtmPreRecordDate != objCurrent.m_dtmCreateDate)
                        //    objData[5] = objCurrent.m_dtmCreateDate.ToString("HH:mm");//ʱ���ַ���

                    }
                    m_dtmPreRecordDate = objCurrent.m_dtmRecordDate;
                    #endregion

                    #region ��ŵ�����Ϣ
                    //��������1
                    strText = objCurrent.m_strPOSTPORTUM_NUM_CHR_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strPOSTPORTUM_NUM_CHR_RIGHT != objCurrent.m_strPOSTPORTUM_NUM_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strPOSTPORTUM_NUM_CHR_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[6] = objclsDSTRichTextBoxValue;//��������

                    //�ӹ�>>����
                    strText = objCurrent.m_strUTERUSBOTTOM_CHR_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strUTERUSBOTTOM_CHR_RIGHT != objCurrent.m_strUTERUSBOTTOM_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strUTERUSBOTTOM_CHR_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[7] = objclsDSTRichTextBoxValue;//�ӹ�>>����


                    //�ӹ�>>�������
                    #region �ӹ�>>������� bak
                    strText = objCurrent.m_strUTERUSPINCH_CHR_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strUTERUSPINCH_CHR_RIGHT != objCurrent.m_strUTERUSPINCH_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strUTERUSPINCH_CHR_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[8] = objclsDSTRichTextBoxValue;//�ӹ�>>�������
                    #endregion


                    //����>>����
                    strText = objCurrent.m_strMILKNUM_CHR_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strMILKNUM_CHR_RIGHT != objCurrent.m_strMILKNUM_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strMILKNUM_CHR_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[9] = objclsDSTRichTextBoxValue;//����>>����


                    //����>>����
                    strText = objCurrent.m_strBREASTBULGE_CHR_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strBREASTBULGE_CHR_RIGHT != objCurrent.m_strBREASTBULGE_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strBREASTBULGE_CHR_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[10] = objclsDSTRichTextBoxValue;//����>>����

                    //����>>��ͷ
                    strText = objCurrent.m_strNIPPLE_CHR_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strNIPPLE_CHR_RIGHT != objCurrent.m_strNIPPLE_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strNIPPLE_CHR_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[11] = objclsDSTRichTextBoxValue;//����>>��ͷ

                    //��¶>>��
                    strText = objCurrent.m_strDEWNUM_CHR_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strDEWNUM_CHR_RIGHT != objCurrent.m_strDEWNUM_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strDEWNUM_CHR_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[12] = objclsDSTRichTextBoxValue;//

                    //��¶>>ɫ
                    strText = objCurrent.m_strDEWCOLOR_CHR_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strDEWCOLOR_CHR_RIGHT != objCurrent.m_strDEWCOLOR_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strDEWCOLOR_CHR_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[13] = objclsDSTRichTextBoxValue;//����

                    //��¶>>��ζ
                    strText = objCurrent.m_strDEWFUCK_CHR_RIGHT;
                    string strNextText = objNext.m_strDEWFUCK_CHR_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && strNextText != strText)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(strText, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[14] = objclsDSTRichTextBoxValue;//

                    #region ��������
                    strText = objCurrent.m_strPERINEUM_CHR_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strPERINEUM_CHR_RIGHT != objCurrent.m_strPERINEUM_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strPERINEUM_CHR_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[15] = objclsDSTRichTextBoxValue;//
                    #endregion

                    //BP>>mmHg
                    strText = objCurrent.m_strBP_CHR_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strBP_CHR_RIGHT != objCurrent.m_strBP_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strBP_CHR_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[16] = objclsDSTRichTextBoxValue;//

                    //��
                    strText = objCurrent.m_strURINE_CHR_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strURINE_CHR_RIGHT != objCurrent.m_strURINE_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strURINE_CHR_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[17] = objclsDSTRichTextBoxValue;//

                    #region ��ע
                    string[] strGeneralInstanceArr = null;
                    string[] strGeneralInstanceXMLArr = null;
                    if (objCurrent.m_strANNOTATIONS_CHR_RIGHT != null || objCurrent.m_strANNOTATIONS_CHR_RIGHT != "")
                    {
                        //string strGeneralInstance = objCurrent.m_strANNOTATIONS_CHR_RIGHT;
                        //string strGeneralInstanceXML = objCurrent.m_strANNOTATIONS_CHRXML;
                        //string[] strGeneralInstanceArrTemp;
                        //string[] strGeneralInstanceXMLArrTemp;
                        ////�������¼��Ϊ20���ַ�һ�С����һ��Ҫ�����񣬹���ӿ��ַ���
                        //com.digitalwave.controls.ctlRichTextBox.m_mthSplitXml("" + strGeneralInstance, strGeneralInstanceXML, 4, out strGeneralInstanceArrTemp, out strGeneralInstanceXMLArrTemp);

                        //if (!string.IsNullOrEmpty(objCurrent.m_strCreateUserID))
                        //{
                        //    //							strGeneralInstanceArr = new string[strGeneralInstanceArrTemp.Length + 1];
                        //    //							strGeneralInstanceXMLArr = new string[strGeneralInstanceXMLArrTemp.Length + 1];
                        //    strGeneralInstanceArr = new string[strGeneralInstanceArrTemp.Length];
                        //    strGeneralInstanceXMLArr = new string[strGeneralInstanceXMLArrTemp.Length];
                        //    //							for(int j=0; j<strGeneralInstanceArr.Length-1; j++)
                        //    for (int j = 0 ; j < strGeneralInstanceArr.Length ; j++)
                        //    {
                        //        strGeneralInstanceArr[j] = strGeneralInstanceArrTemp[j];
                        //    }
                        //    //							strGeneralInstanceArr[strGeneralInstanceArr.Length-1] = "";//objCurrent.m_dtmCreateDate.ToString("yyyy-MM-dd")+"    "+objCurrent.m_strCreateUserName;
                        //    //							
                        //    //							strGeneralInstanceXMLArr[strGeneralInstanceXMLArr.Length-1] = "";
                        //    //							for(int j=0; j<strGeneralInstanceXMLArr.Length-1; j++)
                        //    for (int j = 0 ; j < strGeneralInstanceXMLArr.Length ; j++)
                        //    {
                        //        strGeneralInstanceXMLArr[j] = strGeneralInstanceXMLArrTemp[j];
                        //    }
                        //}
                        //else
                        //{
                        //    strGeneralInstanceArr = strGeneralInstanceArrTemp;
                        //    strGeneralInstanceXMLArr = strGeneralInstanceXMLArrTemp;
                        //}

                        //strText = strGeneralInstanceArr[0];
                        //strXml = strGeneralInstanceXMLArr[0];
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = objCurrent.m_strANNOTATIONS_CHR_RIGHT;
                        objclsDSTRichTextBoxValue.m_strDSTXml = "<root />";
                        objData[18] = objclsDSTRichTextBoxValue;
                    }
                    #endregion

                    // �����
                    //ǩ��
                    if (objCurrent.objSignerArr != null || objCurrent.objSignerArr.Length > 0)
                    {
                        string str = string.Empty;
                        if (objCurrent.objSignerArr[0].objEmployee != null)
                            str = objCurrent.objSignerArr[0].objEmployee.m_strGetTechnicalRankAndName;
                        for (int w1 = 1; w1 < objCurrent.objSignerArr.Length; w1++)
                        {
                            if (objCurrent.objSignerArr[w1].objEmployee != null)
                                str += ";" + objCurrent.objSignerArr[w1].objEmployee.m_strGetTechnicalRankAndName;
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = str;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[19] = objclsDSTRichTextBoxValue;//ǩ��
                    }

                    //
                    objData[20] = objCurrent.m_strRecordUserID;//
                    objData[21] = objCurrent.m_strCreateUserID;//

                    objReturnData.Add(objData);

                    #endregion
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
        protected override void m_mthGetTransDataInfoArr(out clsTransDataInfo[] p_objTansDataInfoArr)
        {
            m_objRecordsDomain.m_lngGetTransDataInfoArr(m_objCurrentPatient.m_StrRegisterId, out p_objTansDataInfoArr);

        }
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
                m_mthGetTransDataInfoArr(out objTansDataInfoArr);
                //long lngRes = m_objRecordsDomain.m_lngGetTransDataInfoArr(m_strInPatientID,m_strInPatientDate,out objTansDataInfoArr);

                if (objTansDataInfoArr == null)
                {
                    return;
                }

                //����¼ʱ��(CreateDate)����
                //m_mthSortTransData(ref objTansDataInfoArr);

                DataTable dtbAddBlank;
                clsDiseaseTrackAddBlankDomain objAddBlankDomain = new clsDiseaseTrackAddBlankDomain();
                objAddBlankDomain.m_lngGetBlankRecordContent(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate, out dtbAddBlank);

                clsIcuAcad_PostPartumContentValueContentDataInfo objICUInfo = new clsIcuAcad_PostPartumContentValueContentDataInfo();

                objICUInfo = (clsIcuAcad_PostPartumContentValueContentDataInfo)objTansDataInfoArr[0];

                if (objICUInfo.m_objPostPartumValues != null)
                {
                    //��Ӽ�¼����DataTable
                    object[][] objDataArr;
                    for (int i1 = 0; i1 < objICUInfo.m_objPostPartumValues.Length; i1++)
                    {
                        objDataArr = m_objGetRecordsValue(objICUInfo.m_objPostPartumValues[i1]);

                        if (objDataArr == null)
                            continue;
                        m_dtbRecords.BeginLoadData();
                        for (int j2 = 0; j2 < objDataArr.Length; j2++)
                        {
                            m_dtbRecords.LoadDataRow(objDataArr[j2], true);
                        }
                        m_dtbRecords.EndLoadData();
                        m_dtgRecordDetail.EnsureVisible(m_dtbRecords.Rows.Count - 1);
                    }
                }
                if (objICUInfo.m_objMannoValue != null)
                {
                    m_txtTOTALBLOODNUM_CHR.m_mthSetValue(new string[] { objICUInfo.m_objMannoValue.m_strTOTALBLOODNUM_CHR_RIGHT });
                    m_txtSEWPIN_CHR.m_mthSetValue(new string[] { objICUInfo.m_objMannoValue.m_strSEWPIN_CHR_RIGHT });
                    m_cmbCurtClass.Text = objICUInfo.m_objMannoValue.m_strPERIOD_CHR_RIGHT;
                    m_rthESPECIALRECORD_CHR.m_mthSetNewText(objICUInfo.m_objMannoValue.m_strESPECIALRECORD_CHR, objICUInfo.m_objMannoValue.m_strESPECIALRECORD_CHRXML);
                    m_dtpChildBirthingDate.Value = objICUInfo.m_objMannoValue.m_dtmChildBirthingDate;
                    m_dtpRecordDate.Value = objICUInfo.m_objMannoValue.m_dtmRecordDate;
                    m_mthAddSignToListView(lsvSign, objICUInfo.m_objMannoValue.objSignerArr);
                    m_strCurrentOpenDate = objICUInfo.m_objMannoValue.m_dtmCreateDate.ToString();
                    m_gpbAnam.Tag = objICUInfo.m_objMannoValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }

        }

        private void m_cmdDelete_Click(object sender, EventArgs e)
        {
        }


        protected override long m_lngSubDelete()
        {
            //��鵱ǰ���˱����Ƿ�Ϊnull  
            if (m_objCurrentPatient == null)
            {
                clsPublicFunction.ShowInformationMessageBox("δѡ������,�޷�ɾ��!");
                return (long)enmOperationResult.Parameter_Error;
            }
            //��鵱ǰ��¼�Ƿ�Ϊnull
            //			if(m_objCurrentRecordContent==null)
            if (m_gpbAnam.Tag == null)
            {
                clsPublicFunction.ShowInformationMessageBox("��ǰ��¼����Ϊ��,�޷�ɾ��!");
                return (long)enmOperationResult.Parameter_Error;
            }
            //��ȡ������ʱ��      
            clsPublicDomain m_objPDomain = new clsPublicDomain();

            clsPostPartumManno_Value objValue = m_gpbAnam.Tag as clsPostPartumManno_Value;
            if (objValue == null)
                return 0;
            //Ȩ���ж�
            string strDeptIDTemp = m_ObjCurrentEmrPatientSession.m_strAreaId;
            bool blnIsAllow = clsPublicFunction.IsAllowDelete(strDeptIDTemp, objValue.m_strCreateUserID, clsEMRLogin.LoginEmployee, 1);
            if (!blnIsAllow)
                return -1;

            objValue.m_bytStatus = 0;
            objValue.m_dtmDeActivedDate = m_objPDomain.m_dtmGetServerTime();
            objValue.m_strDeActivedOperatorID = com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjCurrentEmployee.m_strEMPID_CHR;

            //com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService objServ =
            //    (com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService));

            long lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngDeleteRecord(objValue);
            return lngRes;

        }


        protected override void m_mthSave()
        {
            if (m_gpbAnam.Tag == null)
                m_blnIsAddNew = true;
            else
                m_blnIsAddNew = false;
            long lngRes = m_lngSave();
            if (lngRes > 0)
            {
                //com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService objServ =
                //    (com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsPostPartumRecord_MainService));

                clsPostPartumManno_Value objValue = null;
                lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetRecord(m_objCurrentPatient.m_StrRegisterId, out objValue);
                if (lngRes > 0 && objValue != null)
                {
                    m_txtTOTALBLOODNUM_CHR.m_mthSetValue(new string[] { objValue.m_strTOTALBLOODNUM_CHR_RIGHT });
                    m_txtSEWPIN_CHR.m_mthSetValue(new string[] { objValue.m_strSEWPIN_CHR_RIGHT });
                    m_cmbCurtClass.Text = objValue.m_strPERIOD_CHR_RIGHT;
                    m_rthESPECIALRECORD_CHR.m_mthSetNewText(objValue.m_strESPECIALRECORD_CHR, objValue.m_strESPECIALRECORD_CHRXML);
                    m_dtpChildBirthingDate.Value = objValue.m_dtmChildBirthingDate;
                    m_dtpRecordDate.Value = objValue.m_dtmRecordDate;
                    m_mthAddSignToListView(lsvSign, objValue.objSignerArr);
                    m_gpbAnam.Tag = objValue;
                }
                clsPublicFunction.ShowInformationMessageBox("����ɹ���");
                //m_tipMain.Show("����ɹ���", this, m_gpbAnam.Left, m_gpbAnam.Top, 2000);
            }
            else
                clsPublicFunction.ShowInformationMessageBox("����ʧ�ܣ�");
            //m_tipMain.Show("����ʧ�ܣ�", this, m_gpbAnam.Left, m_gpbAnam.Top, 2000);
        }

        protected override void m_mthDelete()
        {
            long lngRes = m_lngDelete();
            if (lngRes > 0)
            {
                clsPublicFunction.ShowInformationMessageBox("ɾ���ɹ���");
                //m_tipMain.Show("ɾ���ɹ���", m_gpbAnam, m_gpbAnam.Left, m_gpbAnam.Top, 2000);
                m_mthClearLaycountAll();
            }
            else
            {
                clsPublicFunction.ShowInformationMessageBox("ɾ��ʧ�ܣ�");
                //m_tipMain.Show("ɾ��ʧ�ܣ�", m_gpbAnam, m_gpbAnam.Left, m_gpbAnam.Top, 2000);
            }
        }

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

                m_objCurrentPatient.m_DtmSelectedInDate = p_objSelectedSession.m_dtmEMRInpatientDate;
                m_objCurrentPatient.m_StrHISInPatientID = p_objSelectedSession.m_strHISInpatientId;
                m_objCurrentPatient.m_DtmSelectedHISInDate = p_objSelectedSession.m_dtmHISInpatientDate;

                m_objCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;
                m_objBaseCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;

                //���ò��˵���סԺ�Ļ�����Ϣ
                m_mthOnlySetPatientInfo(m_objCurrentPatient);

                m_mthIsReadOnly();
                if (!m_blnCanShowRecordContent())
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                //��ȡ���˼�¼�б�
                clsTransDataInfo[] objTansDataInfoArr;
                m_mthGetTransDataInfoArr(out objTansDataInfoArr);
                //long lngRes = m_objRecordsDomain.m_lngGetTransDataInfoArr(m_strInPatientID,m_strInPatientDate,out objTansDataInfoArr);

                if (objTansDataInfoArr == null)
                {
                    return;
                }

                //����¼ʱ��(CreateDate)����
                //m_mthSortTransData(ref objTansDataInfoArr);

                DataTable dtbAddBlank;
                clsDiseaseTrackAddBlankDomain objAddBlankDomain = new clsDiseaseTrackAddBlankDomain();
                objAddBlankDomain.m_lngGetBlankRecordContent(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate, out dtbAddBlank);

                clsIcuAcad_PostPartumContentValueContentDataInfo objICUInfo = new clsIcuAcad_PostPartumContentValueContentDataInfo();

                objICUInfo = (clsIcuAcad_PostPartumContentValueContentDataInfo)objTansDataInfoArr[0];

                if (objICUInfo.m_objPostPartumValues != null)
                {
                    //��Ӽ�¼����DataTable
                    object[][] objDataArr;
                    for (int i1 = 0; i1 < objICUInfo.m_objPostPartumValues.Length; i1++)
                    {
                        objDataArr = m_objGetRecordsValue(objICUInfo.m_objPostPartumValues[i1]);

                        if (objDataArr == null)
                            continue;
                        m_dtbRecords.BeginLoadData();
                        for (int j2 = 0; j2 < objDataArr.Length; j2++)
                        {
                            m_dtbRecords.LoadDataRow(objDataArr[j2], true);
                        }
                        m_dtbRecords.EndLoadData();
                        m_dtgRecordDetail.EnsureVisible(m_dtbRecords.Rows.Count - 1);
                    }
                }
                if (objICUInfo.m_objMannoValue != null)
                {
                    m_txtTOTALBLOODNUM_CHR.m_mthSetValue(new string[] { objICUInfo.m_objMannoValue.m_strTOTALBLOODNUM_CHR_RIGHT });
                    m_txtSEWPIN_CHR.m_mthSetValue(new string[] { objICUInfo.m_objMannoValue.m_strSEWPIN_CHR_RIGHT });
                    m_cmbCurtClass.Text = objICUInfo.m_objMannoValue.m_strPERIOD_CHR_RIGHT;
                    m_rthESPECIALRECORD_CHR.m_mthSetNewText(objICUInfo.m_objMannoValue.m_strESPECIALRECORD_CHR, objICUInfo.m_objMannoValue.m_strESPECIALRECORD_CHRXML);
                    m_dtpChildBirthingDate.Value = objICUInfo.m_objMannoValue.m_dtmChildBirthingDate;
                    m_dtpRecordDate.Value = objICUInfo.m_objMannoValue.m_dtmRecordDate;
                    m_mthAddSignToListView(lsvSign, objICUInfo.m_objMannoValue.objSignerArr);
                    m_strCurrentOpenDate = objICUInfo.m_objMannoValue.m_dtmCreateDate.ToString();
                    m_gpbAnam.Tag = objICUInfo.m_objMannoValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        protected override void m_mthAddFormStatusForClosingSave()
        {
            //��¼���ô��嵱ǰ״̬
            MDIParent.s_ObjSaveCue.m_mthAddFormStatus(this);
        }

        #region ͨ���Ҽ��˵�ʵ��
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_txtControl"></param>
        protected void m_mthAddRichTemplate(RichTextBox p_txtControl)
        {
            m_objTempTool.m_mthAddTextBox(this, p_txtControl, m_strGetCurFormName(), p_txtControl.Name);
        }
        /// <summary>
        /// ��ȡ��ǰ����,�Զ�������⴦��
        /// </summary>
        /// <returns></returns>
        private string m_strGetCurFormName()
        {
            string strFormName = this.Name;
            //			if(this is iCare.CustomForm.frmCustomFormBase)
            //				strFormName = ((iCare.CustomForm.frmCustomFormBase)this).m_strGetCurFormName();
            return strFormName;
        }
        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_ctlContainer"></param>
        protected virtual void m_mthAddRichTemplateInContainer(Control p_ctlContainer)
        {
            foreach (Control ctlChild in p_ctlContainer.Controls)
            {
                if (ctlChild.Name == "" && ctlChild.GetType().FullName != "System.Windows.Forms.TabPage")
                    continue;
                switch (ctlChild.GetType().FullName)
                {
                    case "com.digitalwave.controls.ctlRichTextBox":
                        m_mthAddRichTemplate((RichTextBox)ctlChild);
                        m_mthAddRichTextInfo((com.digitalwave.controls.ctlRichTextBox)ctlChild);
                        break;
                    case "System.Windows.Forms.RichTextBox":
                    case "iCare.CustomForm.ctlRichTextBox":
                        m_mthAddRichTemplate((RichTextBox)ctlChild);
                        break;
                    default:
                        m_mthAddRichTemplateInContainer(ctlChild);
                        break;
                }
            }
        }*/
        #endregion
    }
}

