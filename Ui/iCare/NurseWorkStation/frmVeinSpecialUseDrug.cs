
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
	/// �������⻯����ҩ�۲��¼
	/// </summary>
	public class frmVeinSpecialUseDrug : iCare.frmRecordsBase
	{
		#region system define

        private string m_strCurrentOpenDate = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
		private string m_strCreateUserID = "";
		private System.Windows.Forms.Label label2;
		private PinkieControls.ButtonXP m_cmdSave;
		private System.Windows.Forms.DataGridTextBoxColumn m_dtcBEGINTIME_DATE;
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcDROP_CHR;
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcABNORMITY_CHR;
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcSOLVE_CHR;
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcREMARK_CHR;
		private System.Windows.Forms.DataGridTextBoxColumn m_dtcUNDERWRITE_CHR;
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcINGEAR_CHR;
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcMEDICINENAME_CHR;
		private System.Windows.Forms.DataGridTextBoxColumn m_dtcBEGINTIME_DATETIME;
		private System.Windows.Forms.ComboBox m_cboDateTime;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker m_dtmBeginTime;
		private System.Windows.Forms.DateTimePicker m_dtmEndTime;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox m_txtEndDate;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region constructor
		public frmVeinSpecialUseDrug()
		{
			InitializeComponent();
		}
		#endregion

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            this.label2 = new System.Windows.Forms.Label();
            this.m_dtcBEGINTIME_DATE = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcDROP_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcABNORMITY_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcSOLVE_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcREMARK_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcUNDERWRITE_CHR = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcINGEAR_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.m_dtcMEDICINENAME_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBEGINTIME_DATETIME = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_cboDateTime = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_dtmBeginTime = new System.Windows.Forms.DateTimePicker();
            this.m_dtmEndTime = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_txtEndDate = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).BeginInit();
            this.m_pnlNewBase.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgtsStyles
            // 
            this.dgtsStyles.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																										 this.m_dtcBEGINTIME_DATE,
																										 this.m_dtcMEDICINENAME_CHR,
																										 this.m_dtcDROP_CHR,
																										 this.m_dtcBEGINTIME_DATETIME,
																										 this.m_dtcINGEAR_CHR,
																										 this.m_dtcABNORMITY_CHR,
																										 this.m_dtcSOLVE_CHR,
																										 this.m_dtcREMARK_CHR,
																										 this.m_dtcUNDERWRITE_CHR});
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
            this.m_dtgRecordDetail.Location = new System.Drawing.Point(8, 111);
            this.m_dtgRecordDetail.RowHeaderWidth = 15;
            this.m_dtgRecordDetail.Size = new System.Drawing.Size(808, 504);
            this.m_dtgRecordDetail.Navigate += new System.Windows.Forms.NavigateEventHandler(this.m_dtgRecordDetail_Navigate);
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
            this.m_trvInPatientDate.Location = new System.Drawing.Point(8, 118);
            this.m_trvInPatientDate.Size = new System.Drawing.Size(176, 60);
            this.m_trvInPatientDate.Visible = false;
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(608, 158);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(696, 158);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(415, 126);
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(400, 158);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(568, 126);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(568, 158);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(656, 158);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(200, 158);
            this.lblAreaTitle.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(456, 158);
            this.txtInPatientID.Size = new System.Drawing.Size(96, 23);
            this.txtInPatientID.Visible = false;
            this.txtInPatientID.TextChanged += new System.EventHandler(this.txtInPatientID_TextChanged);
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(616, 126);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(456, 126);
            this.m_txtBedNO.Size = new System.Drawing.Size(72, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(248, 158);
            this.m_cboArea.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(248, 126);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(200, 126);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(648, 124);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cmdNext.Location = new System.Drawing.Point(528, 126);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(156, 122);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(448, 119);
            this.m_lblForTitle.Size = new System.Drawing.Size(8, 23);
            this.m_lblForTitle.Visible = false;
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(477, 39);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(665, 37);
            this.m_cmdModifyPatientInfo.Size = new System.Drawing.Size(69, 28);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Visible = true;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 10000004;
            this.label2.Text = "���м�¼:";
            // 
            // m_dtcBEGINTIME_DATE
            // 
            this.m_dtcBEGINTIME_DATE.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcBEGINTIME_DATE.Format = "";
            this.m_dtcBEGINTIME_DATE.FormatInfo = null;
            this.m_dtcBEGINTIME_DATE.MappingName = "BEGINTIME_DATE";
            this.m_dtcBEGINTIME_DATE.Width = 60;
            // 
            // m_dtcDROP_CHR
            // 
            this.m_dtcDROP_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcDROP_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcDROP_CHR.m_BlnGobleSet = true;
            this.m_dtcDROP_CHR.m_BlnUnderLineDST = false;
            this.m_dtcDROP_CHR.MappingName = "DROP_CHR";
            this.m_dtcDROP_CHR.Width = 75;
            // 
            // m_dtcABNORMITY_CHR
            // 
            this.m_dtcABNORMITY_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcABNORMITY_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcABNORMITY_CHR.m_BlnGobleSet = true;
            this.m_dtcABNORMITY_CHR.m_BlnUnderLineDST = false;
            this.m_dtcABNORMITY_CHR.MappingName = "ABNORMITY_CHR";
            this.m_dtcABNORMITY_CHR.Width = 75;
            // 
            // m_dtcSOLVE_CHR
            // 
            this.m_dtcSOLVE_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcSOLVE_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcSOLVE_CHR.m_BlnGobleSet = true;
            this.m_dtcSOLVE_CHR.m_BlnUnderLineDST = false;
            this.m_dtcSOLVE_CHR.MappingName = "SOLVE_CHR";
            this.m_dtcSOLVE_CHR.Width = 83;
            // 
            // m_dtcREMARK_CHR
            // 
            this.m_dtcREMARK_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcREMARK_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcREMARK_CHR.m_BlnGobleSet = true;
            this.m_dtcREMARK_CHR.m_BlnUnderLineDST = false;
            this.m_dtcREMARK_CHR.MappingName = "REMARK_CHR";
            this.m_dtcREMARK_CHR.Width = 180;
            // 
            // m_dtcUNDERWRITE_CHR
            // 
            this.m_dtcUNDERWRITE_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcUNDERWRITE_CHR.Format = "";
            this.m_dtcUNDERWRITE_CHR.FormatInfo = null;
            this.m_dtcUNDERWRITE_CHR.MappingName = "UNDERWRITE_CHR";
            this.m_dtcUNDERWRITE_CHR.Width = 75;
            // 
            // m_dtcINGEAR_CHR
            // 
            this.m_dtcINGEAR_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcINGEAR_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcINGEAR_CHR.m_BlnGobleSet = true;
            this.m_dtcINGEAR_CHR.m_BlnUnderLineDST = false;
            this.m_dtcINGEAR_CHR.MappingName = "INGEAR_CHR";
            this.m_dtcINGEAR_CHR.Width = 75;
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdSave.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(528, 10);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(64, 28);
            this.m_cmdSave.TabIndex = 10000042;
            this.m_cmdSave.Tag = "1";
            this.m_cmdSave.Text = "����ʱ��";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // m_dtcMEDICINENAME_CHR
            // 
            this.m_dtcMEDICINENAME_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcMEDICINENAME_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcMEDICINENAME_CHR.m_BlnGobleSet = true;
            this.m_dtcMEDICINENAME_CHR.m_BlnUnderLineDST = false;
            this.m_dtcMEDICINENAME_CHR.MappingName = "MEDICINENAME_CHR";
            this.m_dtcMEDICINENAME_CHR.Width = 120;
            // 
            // m_dtcBEGINTIME_DATETIME
            // 
            this.m_dtcBEGINTIME_DATETIME.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcBEGINTIME_DATETIME.Format = "";
            this.m_dtcBEGINTIME_DATETIME.FormatInfo = null;
            this.m_dtcBEGINTIME_DATETIME.MappingName = "TIME";
            this.m_dtcBEGINTIME_DATETIME.NullText = "";
            this.m_dtcBEGINTIME_DATETIME.Width = 55;
            // 
            // m_cboDateTime
            // 
            this.m_cboDateTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboDateTime.Location = new System.Drawing.Point(63, 79);
            this.m_cboDateTime.Name = "m_cboDateTime";
            this.m_cboDateTime.Size = new System.Drawing.Size(160, 22);
            this.m_cboDateTime.TabIndex = 10000044;
            this.m_cboDateTime.Leave += new System.EventHandler(this.m_cboDateTime_Leave);
            this.m_cboDateTime.SelectionChangeCommitted += new System.EventHandler(this.m_cboDateTime_SelectionChangeCommitted);
            this.m_cboDateTime.SelectedIndexChanged += new System.EventHandler(this.m_cboDateTime_SelectedIndexChanged);
            this.m_cboDateTime.TextChanged += new System.EventHandler(this.m_cboDateTime_TextChanged);
            this.m_cboDateTime.DropDown += new System.EventHandler(this.m_cboDateTime_DropDown);
            this.m_cboDateTime.Click += new System.EventHandler(this.m_cboDateTime_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 14);
            this.label1.TabIndex = 10000004;
            this.label1.Text = "��Һ��ʼʱ��:";
            // 
            // m_dtmBeginTime
            // 
            this.m_dtmBeginTime.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
            this.m_dtmBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtmBeginTime.Location = new System.Drawing.Point(96, 11);
            this.m_dtmBeginTime.Name = "m_dtmBeginTime";
            this.m_dtmBeginTime.Size = new System.Drawing.Size(184, 23);
            this.m_dtmBeginTime.TabIndex = 10000045;
            // 
            // m_dtmEndTime
            // 
            this.m_dtmEndTime.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
            this.m_dtmEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtmEndTime.Location = new System.Drawing.Point(344, 11);
            this.m_dtmEndTime.Name = "m_dtmEndTime";
            this.m_dtmEndTime.Size = new System.Drawing.Size(184, 23);
            this.m_dtmEndTime.TabIndex = 10000045;
            this.m_dtmEndTime.ValueChanged += new System.EventHandler(this.m_dtmEndTime_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 10000004;
            this.label3.Text = "����ʱ��:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_txtEndDate);
            this.groupBox1.Controls.Add(this.m_dtmEndTime);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.m_dtmBeginTime);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.m_cmdSave);
            this.groupBox1.Location = new System.Drawing.Point(224, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(592, 40);
            this.groupBox1.TabIndex = 10000047;
            this.groupBox1.TabStop = false;
            // 
            // m_txtEndDate
            // 
            this.m_txtEndDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtEndDate.Location = new System.Drawing.Point(348, 15);
            this.m_txtEndDate.Name = "m_txtEndDate";
            this.m_txtEndDate.Size = new System.Drawing.Size(162, 16);
            this.m_txtEndDate.TabIndex = 10000046;
            this.m_txtEndDate.Enter += new System.EventHandler(this.m_txtEndDate_Enter);
            // 
            // frmVeinSpecialUseDrug
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(832, 629);
            this.Controls.Add(this.m_cboDateTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmVeinSpecialUseDrug";
            this.Text = "�������⻯����ҩ�۲��¼��";
            this.Load += new System.EventHandler(this.frmVeinSpecialUseDrug_Load);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.m_trvInPatientDate, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.m_dtgRecordDetail, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.m_cboDateTime, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).EndInit();
            this.m_pnlNewBase.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
		// ע�⣬DataTable�ĵ�һ��Column�����Ǵ�ż�¼ʱ����ַ������ڶ���Column�����Ǵ�ż�¼���͵�intֵ��������Column�����Ǵ�ż�¼��OpenDate
		protected override void m_mthInitDataTable(DataTable p_dtbRecordTable)
		{

			//��ż�¼ʱ����ַ���
			p_dtbRecordTable.Columns.Add("RecordDate");//0
			//��ż�¼���͵�intֵ
			DataColumn dcRecordType = new DataColumn("RecordType",typeof(int));
			p_dtbRecordTable.Columns.Add(dcRecordType);//1
			//��ż�¼��OpenDate�ַ���
			p_dtbRecordTable.Columns.Add("OpenDate");  //2
			//��ż�¼��ModifyDate�ַ���
			p_dtbRecordTable.Columns.Add("ModifyDate"); //3

//			DataColumn dc1 = p_dtbRecordTable.Columns.Add("RecordDate_chr");//4
//			dc1.DefaultValue = "";
//			DataColumn dc2 = p_dtbRecordTable.Columns.Add("Time_chr");//5
//			dc2.DefaultValue = "";

			DataColumn dc3 = p_dtbRecordTable.Columns.Add("BEGINTIME_DATE");//6
			dc3.DefaultValue = "";
			p_dtbRecordTable.Columns.Add("MEDICINENAME_CHR",typeof(clsDSTRichTextBoxValue));//6
			
//			p_dtbRecordTable.Columns.Add("BEGINTIME_DATE",typeof(clsDSTRichTextBoxValue));//6
//			p_dtbRecordTable.Columns.Add("MEDICINENAME_CHR",typeof(clsDSTRichTextBoxValue));//6

			p_dtbRecordTable.Columns.Add("DROP_CHR",typeof(clsDSTRichTextBoxValue));//6
					
			p_dtbRecordTable.Columns.Add("TIME");//6

			p_dtbRecordTable.Columns.Add("INGEAR_CHR",typeof(clsDSTRichTextBoxValue));//7
			p_dtbRecordTable.Columns.Add("ABNORMITY_CHR",typeof(clsDSTRichTextBoxValue));//8
			p_dtbRecordTable.Columns.Add("SOLVE_CHR",typeof(clsDSTRichTextBoxValue));//9
			p_dtbRecordTable.Columns.Add("REMARK_CHR",typeof(clsDSTRichTextBoxValue));//10
			DataColumn dc6 = p_dtbRecordTable.Columns.Add("UNDERWRITE_CHR");//11	
			dc6.DefaultValue = "";
            p_dtbRecordTable.Columns.Add("CreateUserID");//10

			m_mthSetControl(m_dtcBEGINTIME_DATE);
			m_mthSetControl(m_dtcMEDICINENAME_CHR);

			m_mthSetControl(m_dtcDROP_CHR);
			m_mthSetControl(m_dtcBEGINTIME_DATETIME);
			m_mthSetControl(m_dtcINGEAR_CHR);
			m_mthSetControl(m_dtcABNORMITY_CHR);
			m_mthSetControl(m_dtcSOLVE_CHR);
			m_mthSetControl(m_dtcREMARK_CHR);
			m_mthSetControl(m_dtcUNDERWRITE_CHR);
			
			//����������
			this.m_dtcBEGINTIME_DATE.HeaderText = "��\r\n\r\n\r\n\r\n\r\n��";
			this.m_dtcMEDICINENAME_CHR.HeaderText = "ҩ\r\n\r\n\r\n\r\n\r\n��";

			this.m_dtcDROP_CHR.HeaderText = "��\r\n\r\n/\r\n\r\n\r\n��";
			this.m_dtcBEGINTIME_DATETIME.HeaderText = "Ѳ\r\n��\r\n\r\n\r\nʱ\r\n��";

			this.m_dtcINGEAR_CHR.HeaderText = "���̵�\r\n\r\n\r\n\r\n\r\n����";

			this.m_dtcABNORMITY_CHR.HeaderText  = "���̵�\r\n\r\n\r\n\r\n\r\n�쳣";
			this.m_dtcSOLVE_CHR.HeaderText  = "���̵�\r\n\r\n\r\n�Ƿ�\r\n����\r\n���";
			this.m_dtcREMARK_CHR.HeaderText = "��\r\n\r\n\r\n\r\n\r\nע";
			this.m_dtcUNDERWRITE_CHR.HeaderText = "ǩ\r\n\r\n\r\n\r\n\r\n��";

		}

		#region ����
		/// <summary>
		/// ��ǰ��Ժʱ��
		/// </summary>
		protected override string m_StrCurrentOpenDate
		{
			get
			{
				if(m_strCurrentOpenDate=="")
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
			get{return enmApproveType.Nurses;}
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
        private DateTime m_dtmEndRecordDate;
		// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
		protected override void m_mthClearRecordInfo()
		{			
			m_dtmPreRecordDate=DateTime.MinValue;
            m_dtmEndRecordDate = DateTime.MinValue;
			m_dtgRecordDetail.CurrentRowIndex=0;
			m_dtbRecords.Rows.Clear();
		}

		/// <summary>
		/// ��ȡ�ۼ�����
		/// </summary>
		/// <param name="p_strText"></param>
		/// <param name="p_strModifyUserID"></param>
		/// <param name="p_strModifyUserName"></param>
		/// <returns></returns>
		private string m_strGetDSTTextXML(string p_strText,string p_strModifyUserID,string p_strModifyUserName)
		{
			return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.s_strMakeDSTXml(p_strText,p_strModifyUserID,p_strModifyUserName,Color.Black,Color.White);
		}

		// ��ȡ���̼�¼�������ʵ��(��Ҫ�Ķ�)
		protected override clsRecordsDomain m_objGetRecordsDomain()
		{
            return new clsRecordsDomain(enmRecordsType.VeinSpecialUseDrug);
		}

		// ��ȡ��¼����Ҫ��Ϣ�������ȡ����CreateDate,OpenDate,LastModifyDate��
		protected override clsTrackRecordContent m_objGetRecordMainContent(int p_intRecordType,
			object[] p_objDataArr)
		{
			//���� p_intRecordType ��ȡ��Ӧ�� clsTrackRecordContent
			clsTrackRecordContent objContent = null;
			//(��Ҫ�Ķ�)
			switch((enmDiseaseTrackType)p_intRecordType)
			{ 
				case enmDiseaseTrackType.VeinSpecialUseDrug:
					objContent = new clsVeinSpecialUseDrugValue();//(��Ҫ�Ķ�)
					break;
			}

			if(objContent == null)
				objContent=new clsVeinSpecialUseDrugValue();	//(��Ҫ�Ķ�)
		
			if(m_objCurrentPatient !=null)
				objContent.m_strInPatientID=m_objCurrentPatient.m_StrInPatientID;
			else 
			{
				clsPublicFunction.ShowInformationMessageBox("��ǰ����Ϊ��!");
				return null;
			}
			objContent.m_dtmInPatientDate=m_objCurrentPatient.m_DtmSelectedInDate;
			objContent.m_dtmCreateDate = DateTime.Parse((string)p_objDataArr[0]);
			objContent.m_dtmOpenDate = DateTime.Parse((string)p_objDataArr[2]);
			objContent.m_dtmModifyDate = DateTime.Parse((string)p_objDataArr[3]);
            objContent.m_strCreateUserID =(string)p_objDataArr[13];     
			return objContent;
		}

		private void frmVeinSpecialUseDrug_Load(object sender, System.EventArgs e)
		{
			m_dtmPreRecordDate = DateTime.MinValue;
            m_dtmEndRecordDate = DateTime.MinValue;
			m_dtgRecordDetail.Focus();
			m_mniAddBlank.Visible=false;
			m_mniDeleteBlank.Visible=false;	
		
		}
		// ��ȡ������Ӻ��޸ģ���¼�Ĵ��塣
		protected override frmDiseaseTrackBase m_frmGetRecordForm(int p_intRecordType)
		{
			switch((enmDiseaseTrackType)p_intRecordType)
			{
				case enmDiseaseTrackType.VeinSpecialUseDrug://(��Ҫ�Ķ�)
					
				{
					DateTime dtmEnd = new DateTime(1900,1,1);
					if(m_txtEndDate.Text != string.Empty)
					{
						try
						{
							dtmEnd = DateTime.Parse(m_txtEndDate.Text);
						}
						catch
						{
							MessageBox.Show("����Ľ���ʱ�䲻����ʱ���ʽ��\n�����³�ʼ��Ϊ��ǰʱ�䣡");
							m_txtEndDate.Text = "";
							m_dtmEndTime.Value = DateTime.Now;
							return null;
						}
					}
					string strid ="";
					if(this.m_cboDateTime.SelectedIndex!=-1)
					strid = m_cboTempIdArr.Items[this.m_cboDateTime.SelectedIndex].ToString();
//					string strDate = m_tnSelectTemp.Text;
//					DateTime dtmInpatientDate = DateTime.Parse(strDate);
//					frmVeinSpecialUseDrugCon frmwcon = new frmVeinSpecialUseDrugCon(strid,this.m_dtmBeginTime.Value,this.m_dtmEndTime.Value,m_tnSelectTemp.Text);
                frmVeinSpecialUseDrugCon frmwcon = new frmVeinSpecialUseDrugCon(strid, this.m_dtmBeginTime.Value, dtmEnd, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"));
					return  frmwcon;//(��Ҫ�Ķ�)
					
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
            //m_mthSetPatientFormInfo(m_objCurrentPatient);
            int intIndex = m_cboDateTime.SelectedIndex;
            m_cboDateTime.SelectedIndex = 0;
            m_cboDateTime.SelectedIndex = intIndex;
		}
		/// <summary>
		/// ��Tableɾ������
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_dtmCreateRecordTime"></param>
		protected override void m_mthRemoveDataFromDataTable(int p_intRecordType,
			DateTime p_dtmCreateRecordTime)
		{
            //m_mthSetPatientFormInfo(m_objCurrentPatient);
            int intIndex = m_cboDateTime.SelectedIndex;
            m_cboDateTime.SelectedIndex = 0;
            m_cboDateTime.SelectedIndex = intIndex;
		}

		/// <summary>
		/// ��ȡ��ǰ���˵���������
		/// </summary>
		/// <param name="p_dtmRecordDate">��¼����</param>
		/// <param name="p_intFormID">����ID</param>
		protected override void m_mthGetDeactiveContent(DateTime p_dtmRecordDate,int p_intFormID)
		{			
			m_mthGetDeletedRecord(p_intFormID,p_dtmRecordDate);
		}

		protected override void m_mthModifyRecord(int p_intRecordType,
			DateTime p_dtmCreateRecordTime)
		{
			enmPrivilegeSF enmSF = (enmPrivilegeSF)Enum.Parse(typeof(enmPrivilegeSF),this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			//��ȡ��Ӽ�¼�Ĵ���
			frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(p_intRecordType); 
			frmAddNewForm.m_mthSetDiseaseTrackInfo(m_objCurrentPatient,p_dtmCreateRecordTime);
		
			m_mthShowSubForm(frmAddNewForm,p_intRecordType,true);
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
			if(this.m_cboDateTime.Text =="")
			{
				MessageBox.Show("����ѡ����½���Һ��ʼʱ��","icare");
				return;
			}
			enmPrivilegeSF enmSF = (enmPrivilegeSF)Enum.Parse(typeof(enmPrivilegeSF),this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			m_mthAddNewRecord((int)enmDiseaseTrackType.VeinSpecialUseDrug);//(��Ҫ�Ķ�)
		}

        protected override void m_mthDemoPrint_FromDataSource()
        {
            objPrintTool = m_objGetPrintTool();//new clsIntensiveTendMainPrintTool();
            if (objPrintTool == null)
            {
                //						clsPublicFunction.ShowInformationMessageBox("������m_objGetPrintTool()������");
                return;
            }
            objPrintTool.m_mthInitPrintTool(null);
            if (m_objBaseCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
            {
                clsPublicFunction.ShowInformationMessageBox("���˼���סԺ��¼����Ϊ��!");
                return;
            }
            else
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, m_objBaseCurrentPatient.m_DtmSelectedInDate, DateTime.Parse(m_strCurrentOpenDate));

            objPrintTool.m_mthInitPrintContent();

            m_mthStartPrint();
        }

		protected override infPrintRecord m_objGetPrintTool()
		{			
			clsVeinSpecialUseDrugPrintTool frmwcon = new clsVeinSpecialUseDrugPrintTool();
			DataTable  dt = m_mthCreateTable();
			string str = "";
			DataRow dr;
			for(int rowIndex=0;rowIndex<m_dtgRecordDetail.VisibleRowCount-1;++rowIndex)
			{
				dr = dt.NewRow();
				for(int columnIndex=0;columnIndex<m_dtgRecordDetail.VisibleColumnCount;++columnIndex)
				{
					str = "";
					System.Type type =m_dtgRecordDetail[rowIndex,columnIndex].GetType();
					//MessageBox.Show(type.FullName);
					if(type.FullName == "com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue")
					{
						//com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox txt = (com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox)m_dtgRecordDetail[rowIndex,columnIndex];
						com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue tv = (com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue)m_dtgRecordDetail[rowIndex,columnIndex];
						str = tv.m_strText.Trim();
						//str = txt.m_RtbBase.m_strGetRightText();
					}
					else
					str = m_dtgRecordDetail[rowIndex,columnIndex].ToString();
					dr[columnIndex] = str;
				}
				dt.Rows.Add(dr);
			}
			float[] fltArrColumnPercent = new float[9];
									 
			fltArrColumnPercent[0] = 0.055555556F; // -0.055555556F
			fltArrColumnPercent[1] = 0.111111112F;
			fltArrColumnPercent[2] = 0.055555556F;// -0.055555556F
			fltArrColumnPercent[3] = 0.111111112F;//
			fltArrColumnPercent[4] = 0.111111112F;
			fltArrColumnPercent[5] = 0.111111112F;
			fltArrColumnPercent[6] = 0.111111112F;//
			fltArrColumnPercent[7] = 0.111111112F + 0.055555556F + 0.055555556F + 0.022222226F;//+0.111111112F
			fltArrColumnPercent[8] = 0.111111112F - 0.022222226F;
			frmwcon.Source = dt;
			frmwcon.ColumnPercentArr = fltArrColumnPercent;
            if (m_objCurrentPatient != null && m_ObjCurrentEmrPatientSession != null)
            {
                frmwcon.m_strDept = m_ObjCurrentEmrPatientSession.m_strAreaName;//����
                frmwcon.m_strBegNo = m_ObjCurrentBed == null ? string.Empty : m_ObjCurrentBed.m_strCODE_CHR;//����
                frmwcon.m_strName = m_objCurrentPatient.m_StrName;//����
                frmwcon.m_strAge = m_objCurrentPatient.m_ObjPeopleInfo.m_StrAge;//Age
                frmwcon.m_strInHosNo = m_objCurrentPatient.m_StrHISInPatientID;//סԺ��
            }
            else
            {
                frmwcon.m_strDept = string.Empty;//����
                frmwcon.m_strBegNo = string.Empty;//����
                frmwcon.m_strName = string.Empty;//����
                frmwcon.m_strAge = string.Empty;//Age
                frmwcon.m_strInHosNo = string.Empty;//סԺ��
            }
			
			frmwcon.m_strBeginTime = m_dtmBeginTime.Value.ToString("yyyy-MM-dd HH:mm");//��Һ��ʼʱ��
//			frmwcon.m_strEndTime = m_dtmEndTime.Value.ToString("yyyy-MM-dd HH:mm");//��Һendʱ��
			frmwcon.m_strEndTime = m_txtEndDate.Text;

			//ȡID(ID_CHR)
			string strid ="";
			if(this.m_cboDateTime.SelectedIndex!=-1)
				strid = m_cboTempIdArr.Items[this.m_cboDateTime.SelectedIndex].ToString();
			frmwcon.m_strID_CHR = strid;
			//end
			return frmwcon;
		}

		#region  ���������ɵ�������Ҫ�õ���DataTable
		/// <summary>
		/// ���������ɵ�������Ҫ�õ���DataTable
		/// </summary>
		private DataTable  m_mthCreateTable()
		{
			DataTable dt  = new DataTable("�������⻯����ҩ�۲��¼��");
			System.Data.DataColumn dc = new DataColumn("����");
			dt.Columns.Add(dc);
			dc = new DataColumn("ҩ��");
			dt.Columns.Add(dc);
			dc = new DataColumn("��/��");
			dt.Columns.Add(dc);
			dc = new DataColumn("Ѳ��ʱ��");
			dt.Columns.Add(dc);
			dc = new DataColumn("���̵�����");
			dt.Columns.Add(dc);
			dc = new DataColumn("���̵��쳣");
			dt.Columns.Add(dc);
			dc = new DataColumn("�Ƿ���");
			dt.Columns.Add(dc);
			dc = new DataColumn("��ע");
			dt.Columns.Add(dc);
			dc = new DataColumn("ǩ��");
			dt.Columns.Add(dc);
			return dt ;
		}
		#endregion

		protected override object[][] m_objGetRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
		{
			#region ��ʾ��¼��DataGrid
			try
			{
				
				if(p_objTransDataInfo == null)
					return null;

				object[] objData;
				ArrayList objReturnData=new ArrayList();

				clsVeinSpecialUseDrugValueContentDataInfo objICUInfo=new clsVeinSpecialUseDrugValueContentDataInfo();	//(��Ҫ�Ķ�)		
				clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
				string strText,strXml;

				objICUInfo = (clsVeinSpecialUseDrugValueContentDataInfo)p_objTransDataInfo;//(��Ҫ�Ķ�)

				if(objICUInfo.m_objRecordArr == null)
					return null;

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

				for(int i=0; i<intRecordCount; i++)
				{
					//����
                    string strid = "";
                    if (this.m_cboDateTime.SelectedIndex != -1)
                        strid = m_cboTempIdArr.Items[this.m_cboDateTime.SelectedIndex].ToString();
                    if (strid == null) continue;
                    if (strid == "") continue;
                    if (objICUInfo.m_objRecordArr[i].m_strID_CHR.Trim() != strid.Trim())
                    {
                        continue;
                    }
                    
					objData = new object[14];   //(��Ҫ�Ķ�) DataTable������
					clsVeinSpecialUseDrugValue objCurrent = objICUInfo.m_objRecordArr[i];//(��Ҫ�Ķ�)
					clsVeinSpecialUseDrugValue objNext = new clsVeinSpecialUseDrugValue();//��һ����¼//(��Ҫ�Ķ�)
					if(i < intRecordCount-1)
						objNext = objICUInfo.m_objRecordArr[i+1];

					//����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ�����ʾ
					if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strModifyUserID.Trim()==objCurrent.m_strCreateUserID.Trim())
					{
						TimeSpan tsModify =  objNext.m_dtmModifyDate-objCurrent.m_dtmModifyDate;
						if((int)tsModify.TotalHours < intCanModifyTime)
							continue;
					}


					#region ��Źؼ��ֶ�
					if(objCurrent.m_dtmCreateDate!=DateTime.MinValue)
					{
						objData[0] = objCurrent.m_dtmCreateDate;//��ż�¼ʱ����ַ���
						objData[1] = (int)enmRecordsType.VeinSpecialUseDrug;//��ż�¼���͵�intֵ  //(��Ҫ�Ķ�)
						objData[2] = objCurrent.m_dtmOpenDate;//��ż�¼��OpenDate�ַ���
						objData[3] = objCurrent.m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   
                        objData[13] = objCurrent.m_strCreateUserID;//��ż�¼��CreateUserID�ַ���   
						
						
						//ͬһ����ֻ�ڵ�һ����ʾ����
						if(Convert.ToDateTime(objCurrent.m_strBEGINTIME_DATE_RIGHT).Date.ToString() != m_dtmPreRecordDate.Date.ToString())//m_dtmRECORDDATE
						{
							strText = Convert.ToDateTime(objCurrent.m_strBEGINTIME_DATE_RIGHT).Day.ToString()+"/"+Convert.ToDateTime(objCurrent.m_strBEGINTIME_DATE_RIGHT).Month.ToString();
							objData[4] = strText;
							//objData[4] = Convert.ToDateTime(objCurrent.m_strBEGINTIME_DATE_RIGHT).Date.ToString("yyyy-MM-dd") ;//�����ַ���
						}
//						//�޸ĺ���кۼ��ļ�¼������ʾʱ��
//						if(m_dtmPreRecordDate != objCurrent.m_dtmCreateDate)
//							objData[5] = objCurrent.m_dtmCreateDate.ToString("HH:mm");//ʱ���ַ���
//	
					m_dtmPreRecordDate = Convert.ToDateTime(objCurrent.m_strBEGINTIME_DATE_RIGHT);
                    m_dtmEndRecordDate = Convert.ToDateTime(objCurrent.m_dtmfluidEndTIME_DATE);
					}	
					#endregion 					
					
					#region ��ŵ�����Ϣ
					//��Һ��ʼʱ��
//					strText = Convert.ToDateTime(objCurrent.m_strBEGINTIME_DATE_RIGHT).Day.ToString()+"/"+Convert.ToDateTime(objCurrent.m_strBEGINTIME_DATE_RIGHT).Month.ToString();
//					objData[4] = strText;
//					strXml = "<root />";
//					if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strBEGINTIME_DATE_RIGHT != objCurrent.m_strBEGINTIME_DATE_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
//					{
//						strXml = m_strGetDSTTextXML(objCurrent.m_strBEGINTIME_DATE_RIGHT ,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
//					}
//					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//					objclsDSTRichTextBoxValue.m_strText=strText;						
//					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
//					objData[4] = objclsDSTRichTextBoxValue;//

					//ҩ��
					strText = objCurrent.m_strMEDICINENAME_CHR_RIGHT;
					strXml = "<root />";
					if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strMEDICINENAME_CHR_RIGHT != objCurrent.m_strMEDICINENAME_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strMEDICINENAME_CHR_RIGHT ,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
					objData[5] = objclsDSTRichTextBoxValue;

			
					//��
					#region ��
					strText = objCurrent.m_strDROP_CHR_RIGHT ;
					strXml = "<root />";
					if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strDROP_CHR_RIGHT != objCurrent.m_strDROP_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strDROP_CHR_RIGHT,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[6] = objclsDSTRichTextBoxValue;//��
					#endregion

					//ʱ��
					#region ʱ��
					strText = Convert.ToDateTime(objCurrent.m_strBEGINTIME_DATE_RIGHT).ToString("HH:mm") ;
					objData[7] = strText ;
//					string strNextText = Convert.ToDateTime(objNext.m_strBEGINTIME_DATE_RIGHT).ToString("HH:mm") ;
//					strXml = "<root />";
//					if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && strNextText != strText)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
//					{
//						strXml = m_strGetDSTTextXML(strText,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
//					}
//					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//					objclsDSTRichTextBoxValue.m_strText=strText;						
//					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
//					objData[7] = objclsDSTRichTextBoxValue;
					#endregion

					//����
					strText = objCurrent.m_strINGEAR_CHR_RIGHT;
					strXml = "<root />";
					if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strINGEAR_CHR_RIGHT != objCurrent.m_strINGEAR_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strINGEAR_CHR_RIGHT,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[8] = objclsDSTRichTextBoxValue;//����

			
					//�쳣
					strText = objCurrent.m_strABNORMITY_CHR_RIGHT;
					strXml = "<root />";
					if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strABNORMITY_CHR_RIGHT != objCurrent.m_strABNORMITY_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strABNORMITY_CHR_RIGHT,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[9] = objclsDSTRichTextBoxValue;//
			
					//�Ƿ�����
					strText = objCurrent.m_strSOLVE_CHR_RIGHT ;
					strXml = "<root />";
					if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strSOLVE_CHR_RIGHT != objCurrent.m_strSOLVE_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strSOLVE_CHR_RIGHT,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[10] = objclsDSTRichTextBoxValue;//�Ƿ�����


					#region ��ע
					string[] strGeneralInstanceArr = null;
					string[] strGeneralInstanceXMLArr = null;
					if(objCurrent.m_strREMARK_CHR_RIGHT != null ||objCurrent.m_strREMARK_CHR_RIGHT != "")
					{
						string strGeneralInstance = objCurrent.m_strREMARK_CHR_RIGHT;
						string strGeneralInstanceXML = objCurrent.m_strREMARK_CHRXML;
						string[] strGeneralInstanceArrTemp;
						string[] strGeneralInstanceXMLArrTemp;
						//�������¼��Ϊ20���ַ�һ�С����һ��Ҫ�����񣬹���ӿ��ַ���
						com.digitalwave.controls.ctlRichTextBox.m_mthSplitXml(""+strGeneralInstance,strGeneralInstanceXML,12,out strGeneralInstanceArrTemp,out strGeneralInstanceXMLArrTemp);
						
						if(objCurrent.m_strCreateUserID != null && objCurrent.m_strCreateUserID != "")
						{
							//							strGeneralInstanceArr = new string[strGeneralInstanceArrTemp.Length + 1];
							//							strGeneralInstanceXMLArr = new string[strGeneralInstanceXMLArrTemp.Length + 1];
							strGeneralInstanceArr = new string[strGeneralInstanceArrTemp.Length ];
							strGeneralInstanceXMLArr = new string[strGeneralInstanceXMLArrTemp.Length ];
							//							for(int j=0; j<strGeneralInstanceArr.Length-1; j++)
							for(int j=0; j<strGeneralInstanceArr.Length; j++)
							{
								strGeneralInstanceArr[j] = strGeneralInstanceArrTemp[j];
							}
							//							strGeneralInstanceArr[strGeneralInstanceArr.Length-1] = "";//objCurrent.m_dtmCreateDate.ToString("yyyy-MM-dd")+"    "+objCurrent.m_strCreateUserName;
							//							
							//							strGeneralInstanceXMLArr[strGeneralInstanceXMLArr.Length-1] = "";
							//							for(int j=0; j<strGeneralInstanceXMLArr.Length-1; j++)
							for(int j=0; j<strGeneralInstanceXMLArr.Length; j++)
							{
								strGeneralInstanceXMLArr[j] = strGeneralInstanceXMLArrTemp[j];
							}
						}
						else
						{
							strGeneralInstanceArr = strGeneralInstanceArrTemp;
							strGeneralInstanceXMLArr = strGeneralInstanceXMLArrTemp;
						}

						strText = strGeneralInstanceArr[0];
						strXml = strGeneralInstanceXMLArr[0];
						objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
						objclsDSTRichTextBoxValue.m_strText=strText;						
						objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
						objData[11] = objclsDSTRichTextBoxValue;
					}
					#endregion

					//ǩ��
					strText = objCurrent.m_strUNDERWRITE_CHR_RIGHT ;
					objData[12] = strText ;
//					strXml = "<root />";
//					if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strUNDERWRITE_CHR_RIGHT != objCurrent.m_strUNDERWRITE_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
//					{
//						strXml = m_strGetDSTTextXML(objCurrent.m_strUNDERWRITE_CHR_RIGHT,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
//					}
//					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//					objclsDSTRichTextBoxValue.m_strText=strText;						
//					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
//					objData[12] = objclsDSTRichTextBoxValue;

					objReturnData.Add(objData);

					object[] objInstance = null;
					
					#region ��ע�Ĵ���
					if(strGeneralInstanceArr.Length > 1)
					{
						
						for(int j=1; j<strGeneralInstanceArr.Length; j++)
						{
							objInstance = new object[12];
							strText = strGeneralInstanceArr[j];
							strXml = strGeneralInstanceXMLArr[j];
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
							objInstance[11] = objclsDSTRichTextBoxValue;


							objReturnData.Add(objInstance);
						}
					}
					#endregion

					#endregion
				}
				object[][] m_objRe=new object[objReturnData.Count][];

				for(int m=0;m<objReturnData.Count ;m++)
					m_objRe[m]=(object[])objReturnData[m];
				return m_objRe;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message );
				return null;
			}
			#endregion
		}



		private void m_dtgRecordDetail_Navigate(object sender, System.Windows.Forms.NavigateEventArgs ne)
		{
		
		}

		private void m_cmdSave_Click(object sender, System.EventArgs e)
		{
			string strid ="";
			if(this.m_cboDateTime.Text!="")
			{
				if(this.m_cboDateTime.Text!="�½�")
				{
					int intIndex = this.m_cboDateTime.SelectedIndex;
					DateTime dtmEnd = new DateTime(1900,1,1);
					if(m_txtEndDate.Text != string.Empty)
					{
						try
						{
							dtmEnd = DateTime.Parse(m_txtEndDate.Text);
						}
						catch
						{
							MessageBox.Show("����Ľ���ʱ�䲻����ʱ���ʽ��\n�����³�ʼ��Ϊ��ǰʱ�䣡");
							m_txtEndDate.Text = "";
							m_dtmEndTime.Value = DateTime.Now;
							return;
						}
					}
					strid = m_cboTempIdArr.Items[intIndex].ToString();

                //    clsVeinSpecialUseDrug_ConService Service =
                //(clsVeinSpecialUseDrug_ConService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsVeinSpecialUseDrug_ConService));

					long res = (new weCare.Proxy.ProxyEmr()).Service.m_lngUpdateBeginEndDate(this.m_dtmBeginTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),dtmEnd.ToString("yyyy-MM-dd HH:mm:ss"),strid);
					if(res>0)
					{
						m_cboDateTime.Items[intIndex] =  this.m_dtmBeginTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
						MessageBox.Show("����ʱ��ɹ�");
					}
					else
						MessageBox.Show("����ʱ��ʧ��");					
				}
				else
					MessageBox.Show("��ѡ���¼ʱ��");	
			}
			else
				MessageBox.Show("��ѡ���¼ʱ��");	
					
		}

		ComboBox  m_cboTempIdArr = new ComboBox() ;
		ComboBox  m_cboTempEndDateArr = new ComboBox() ;
		ComboBox  m_cboTempBeginDateArr = new ComboBox() ;
		
		private void SetSelectDate(string p_id,string p_date)
		{
			this.m_cboDateTime.Items.Clear();
			this.m_cboTempIdArr.Items.Clear();
			this.m_cboTempEndDateArr.Items.Clear();
			this.m_cboTempBeginDateArr.Items.Clear();
			this.m_cboDateTime.Items.Add("�½�");
			m_cboTempIdArr.Items.Add("-3");
			m_cboTempEndDateArr.Items.Add("enddate");
			m_cboTempBeginDateArr.Items.Add("begindate");
			if(p_id == "" || p_date=="")
				return ;
            //clsVeinSpecialUseDrug_MainService clsService =
            //    (clsVeinSpecialUseDrug_MainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsVeinSpecialUseDrug_MainService));

			DataTable p_dtResult = new DataTable();
			long lngres = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetSELECTCheckDate(p_id,p_date,out p_dtResult);
			if(lngres>0)
			{
				if(p_dtResult == null) return;
				for(int i=0;i<p_dtResult.Rows.Count ;++i)
				{
					m_cboDateTime.Items.Add(p_dtResult.Rows[i]["CHECKDATE_DATE"].ToString());
					m_cboTempIdArr.Items.Add(p_dtResult.Rows[i]["ID_CHR"].ToString());
					m_cboTempEndDateArr.Items.Add(p_dtResult.Rows[i]["ENDTIME_DATE"].ToString());
					m_cboTempBeginDateArr.Items.Add(p_dtResult.Rows[i]["CHECKDATE_DATE"].ToString());
				}

				m_cboDateTime.Tag = p_dtResult;
			}			
		}
		protected override void m_trvInPatientDate_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			m_txtEndDate.Text = "";
			base.m_trvInPatientDate_AfterSelect(sender,e);
			if(m_trvInPatientDate.SelectedNode==null || m_trvInPatientDate.SelectedNode==m_trvInPatientDate.Nodes[0] || m_objCurrentPatient==null)
			{						
				return;
			}
			if(m_trvInPatientDate.SelectedNode!=null)
			{
				if(m_trvInPatientDate.SelectedNode.Text.Trim() =="��Ժʱ��")
				{
					
				}
				else
				{
                    SetSelectDate(m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"));
				    if(m_cboDateTime.Items.Count>0)
				    {
					    m_cboDateTime.SelectedIndex = m_cboDateTime.Items.Count - 1;
				    }
				}
			}
			
		}

		private TreeNode m_tnSelectTemp = new TreeNode();
		private void m_cboDateTime_SelectedIndexChanged(object sender, System.EventArgs e)
		{

			if(m_cboDateTime.Text =="�½�")
			{
				m_dtmBeginTime.Value = System.DateTime.Now;
				m_dtmEndTime.Value = System.DateTime.Now;	
				m_txtEndDate.Text = "";
				m_tnSelectTemp = m_trvInPatientDate.SelectedNode;
			}
			else 
			{
				if(m_cboDateTime.Text !="")
				{
					string strEndDate = m_cboTempEndDateArr.Items[m_cboDateTime.SelectedIndex].ToString();
					if(strEndDate != null && strEndDate != string.Empty)
					{
						this.m_dtmEndTime.Value = DateTime.Parse(strEndDate);
						if(this.m_dtmEndTime.Value != new DateTime(1900,1,1))
						{
							this.m_txtEndDate.Text = this.m_dtmEndTime.Value.ToString("yyyy��MM��dd�� HH:mm");
						}
						else
						{
							this.m_txtEndDate.Text = "";
						}
					}
					else
					{
						this.m_dtmEndTime.Value = DateTime.Now;
						this.m_txtEndDate.Text = "";
					}
					this.m_dtmBeginTime.Value = DateTime.Parse(m_cboTempBeginDateArr.Items[m_cboDateTime.SelectedIndex].ToString());

				}
			}

			try
			{
				//��ղ��˼�¼��Ϣ				
				m_mthClearPatientRecordInfo();

				if(m_ObjCurrentEmrPatientSession == null || m_objCurrentPatient==null)
				{						
					return;
				}

                m_objCurrentPatient.m_DtmSelectedInDate = m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate;

				//��ȡ���˼�¼�б�
				clsTransDataInfo[] objTansDataInfoArr;
				string m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
                string m_strInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"); //m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss");
			if(m_cboDateTime.Text =="�½�"||m_cboDateTime.Text=="")
				m_strInPatientID = "-1";//Ϊ�˲鲻�����.
				long lngRes = m_objRecordsDomain.m_lngGetTransDataInfoArr(m_strInPatientID,m_strInPatientDate,out objTansDataInfoArr);

				if(lngRes <= 0 || objTansDataInfoArr == null)
				{
					return;
				} 
		
				//����¼ʱ��(CreateDate)����
				//modified by  thfzhang 2005-11-12 Σ�ػ�����Ҫ����
				if (this.Name!="frmIntensiveTendMain")
					m_mthSortTransData(ref objTansDataInfoArr);

				DataTable dtbAddBlank;
				clsDiseaseTrackAddBlankDomain objAddBlankDomain = new clsDiseaseTrackAddBlankDomain();
				objAddBlankDomain.m_lngGetBlankRecordContent(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmSelectedInDate,out dtbAddBlank);

				//��Ӽ�¼����DataTable
				object [][] objDataArr;
				for(int i1=0;i1<objTansDataInfoArr.Length;i1++)
				{ 
					if(dtbAddBlank !=null && dtbAddBlank.Rows.Count>0)
					{
						//���Ҽ�¼֮ǰ�з���м�¼,�в������
						foreach(DataRow drtAdd in dtbAddBlank.Rows) 
						{
							if(objTansDataInfoArr[i1].m_objRecordContent !=null)
							{
								if(objTansDataInfoArr[i1].m_objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse(drtAdd["opendate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"))
									//							if(objTansDataInfoArr[i1].m_objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse( drtAdd[2].ToString()).ToString("yyyy-MM-dd HH:mm:ss"))
								{
									object[] objBlank = new object[5];
									objBlank[1] = 100;
									objBlank[2] = drtAdd[2].ToString();
									m_dtbRecords.Rows.Add(objBlank);
									for(int k3 = 0; k3 < (Int32.Parse( drtAdd[3].ToString())-1); k3++)
									{
										m_dtbRecords.Rows.Add(new object[]{});
									}
									break;
								}
							}
						}
					}
					
					objDataArr = m_objGetRecordsValueArr(objTansDataInfoArr[i1]);
					
					if(objDataArr==null)
						continue;
					//						return;
					for(int j2=0;j2<objDataArr.Length;j2++)
					{
						m_dtbRecords.Rows.Add(objDataArr[j2]);	
					}
				}
				//��ʾdatagrid(Σ�ػ����¼)
				//				DisplayDataToDatagrid(m_dtbRecords);

				//				if(m_dtbRecords.Rows.Count > 0)//�����񲡳̼�¼����ֻ��һ��column����� alex 2003-05-29
				//				{
				//					m_dtgRecordDetail.CurrentCell = new DataGridCell(1,0);
				//					m_dtgRecordDetail.CurrentCell = new DataGridCell(0,0);
				//				}		

				if(m_dtbRecords.Rows.Count == 0 && !m_blnIfNewDeletedRecord)
				{
					m_mthAutoAddNewRecord();
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
			}
		}
        private void txtInPatientID_TextChanged(object sender, System.EventArgs e)
        {

            if (m_trvInPatientDate.SelectedNode == null || m_trvInPatientDate.SelectedNode == m_trvInPatientDate.Nodes[0] || m_objCurrentPatient == null)
            {
                return; 
            }
            if (m_trvInPatientDate.SelectedNode != null)
            {

                string strInDate = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_DtmEMRInDate.ToString("yyyy-MM-dd HH:mm:ss");
                string strInID = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_StrEMRInPatientID;
                SetSelectDate(strInID, strInDate);
                if (m_cboDateTime.Items.Count > 1)
                {
                    m_cboDateTime.SelectedIndex = m_cboDateTime.Items.Count - 1;
                }
            }
        }

		private string str_id = "";
		private void m_cboDateTime_DropDown(object sender, System.EventArgs e)
		{
			
			if(m_ObjCurrentEmrPatientSession ==null || m_objCurrentPatient==null)
			{						
				return;
			}

            string strInDate = m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss");
            string strInID = m_objCurrentPatient.m_StrEMRInPatientID;
			SetSelectDate(strInID,strInDate);
			
			m_dtbRecords.Rows.Clear();
			this.dgtsStyles.DataGrid.DataSource = m_dtbRecords;
		}

		private void m_cboDateTime_TextChanged(object sender, System.EventArgs e)
		{

		}

		private void m_cboDateTime_Leave(object sender, System.EventArgs e)
		{
			
		}

		private void m_cboDateTime_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
	
		}
		private void m_clearDS()
		{
				#region code
				if(m_cboDateTime.Text =="�½�")
				{
					m_dtmBeginTime.Value = System.DateTime.Now;
					m_dtmEndTime.Value = System.DateTime.Now;
					m_txtEndDate.Text = "";
					m_tnSelectTemp = m_trvInPatientDate.SelectedNode;
				}
				else 
				{
					if(m_cboDateTime.Text !="")
					{
						string strEndDate = m_cboTempEndDateArr.Items[m_cboDateTime.SelectedIndex].ToString();
						this.m_dtmEndTime.Value = DateTime.Parse(strEndDate);
						this.m_dtmBeginTime.Value = DateTime.Parse(m_cboTempBeginDateArr.Items[m_cboDateTime.SelectedIndex].ToString());

					}
				}

				try
				{
					//��ղ��˼�¼��Ϣ				
					m_mthClearPatientRecordInfo();

					if(m_trvInPatientDate.SelectedNode==null || m_trvInPatientDate.SelectedNode==m_trvInPatientDate.Nodes[0] || m_objCurrentPatient==null)
					{						
						return;
					}

                    m_objCurrentPatient.m_DtmSelectedInDate = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_DtmEMRInDate;

					//��ȡ���˼�¼�б�
					clsTransDataInfo[] objTansDataInfoArr;
					string m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
                    string m_strInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"); //m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss");
					//if(m_cboDateTime.Text =="�½�"||m_cboDateTime.Text=="")
						m_strInPatientID = "-1";//Ϊ�˲鲻�����.
					long lngRes = m_objRecordsDomain.m_lngGetTransDataInfoArr(m_strInPatientID,m_strInPatientDate,out objTansDataInfoArr);

					if(lngRes <= 0 || objTansDataInfoArr == null)
					{
						return;
					} 
		
					//����¼ʱ��(CreateDate)����
					//modified by  thfzhang 2005-11-12 Σ�ػ�����Ҫ����
					if (this.Name!="frmIntensiveTendMain")
						m_mthSortTransData(ref objTansDataInfoArr);

					DataTable dtbAddBlank;
					clsDiseaseTrackAddBlankDomain objAddBlankDomain = new clsDiseaseTrackAddBlankDomain();
					objAddBlankDomain.m_lngGetBlankRecordContent(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmSelectedInDate,out dtbAddBlank);

					//��Ӽ�¼����DataTable
					object [][] objDataArr;
					for(int i1=0;i1<objTansDataInfoArr.Length;i1++)
					{ 
						if(dtbAddBlank !=null && dtbAddBlank.Rows.Count>0)
						{
							//���Ҽ�¼֮ǰ�з���м�¼,�в������
							foreach(DataRow drtAdd in dtbAddBlank.Rows) 
							{
								if(objTansDataInfoArr[i1].m_objRecordContent !=null)
								{
									if(objTansDataInfoArr[i1].m_objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse(drtAdd["opendate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"))
										//							if(objTansDataInfoArr[i1].m_objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse( drtAdd[2].ToString()).ToString("yyyy-MM-dd HH:mm:ss"))
									{
										object[] objBlank = new object[5];
										objBlank[1] = 100;
										objBlank[2] = drtAdd[2].ToString();
										m_dtbRecords.Rows.Add(objBlank);
										for(int k3 = 0; k3 < (Int32.Parse( drtAdd[3].ToString())-1); k3++)
										{
											m_dtbRecords.Rows.Add(new object[]{});
										}
										break;
									}
								}
							}
						}
					
						objDataArr = m_objGetRecordsValueArr(objTansDataInfoArr[i1]);
					
						if(objDataArr==null)
							continue;
						//						return;
						for(int j2=0;j2<objDataArr.Length;j2++)
						{
							m_dtbRecords.Rows.Add(objDataArr[j2]);	
						}
					}
					//��ʾdatagrid(Σ�ػ����¼)
					//				DisplayDataToDatagrid(m_dtbRecords);

					//				if(m_dtbRecords.Rows.Count > 0)//�����񲡳̼�¼����ֻ��һ��column����� alex 2003-05-29
					//				{
					//					m_dtgRecordDetail.CurrentCell = new DataGridCell(1,0);
					//					m_dtgRecordDetail.CurrentCell = new DataGridCell(0,0);
					//				}		

					if(m_dtbRecords.Rows.Count == 0 && !m_blnIfNewDeletedRecord)
					{
						m_mthAutoAddNewRecord();
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
				}
				#endregion
		}

		private void m_cboDateTime_Click(object sender, System.EventArgs e)
		{
			
		}

		private void m_txtEndDate_Enter(object sender, System.EventArgs e)
		{
			if(m_txtEndDate.Text == string.Empty)
			{
				m_dtmEndTime.Value = DateTime.Now;
				m_txtEndDate.Text = m_dtmEndTime.Value.ToString("yyyy��MM��dd�� HH:mm");
			}
		}

		private void m_dtmEndTime_ValueChanged(object sender, System.EventArgs e)
		{
			if(m_dtmEndTime.Value != new DateTime(1900,1,1))
			{
				m_txtEndDate.Text = m_dtmEndTime.Value.ToString("yyyy��MM��dd�� HH:mm");
			}
		}

		protected override void m_mthDelete()
		{
			if(m_cboDateTime.Text =="�½�")
			{
				return;
			}
			else if(m_cboDateTime.Text != "")
			{
//				clsPublicMiddleTier objRoleServ = new clsPublicMiddleTier();
//				string strRoleID = "";
//				objRoleServ.m_lngCheckRoleByEmpIDAndRoleName(clsEMRLogin.LoginInfo.m_strEmpID, "��ʿ��", out strRoleID);
//				objRoleServ = null;
//				if(strRoleID == null || strRoleID == "")
//				{
//					MDIParent.ShowInformationMessageBox("ֻ�л�ʿ������ɾ����ǰ����ļ�¼��");
//					return;
//				}

				if(MDIParent.ShowQuestionMessageBox("�Ƿ�ɾ����ǰҳ���¼��",MessageBoxButtons.YesNo) == DialogResult.No)
					return;

				
				//��֤ɾ��
				clsDeleteVerify objDeleteVerify=new clsDeleteVerify();
				if (objDeleteVerify.m_mthIsDelete(null,null)==false)
				{
					clsPublicFunction.ShowInformationMessageBox("��֤ʧ�ܲ���ɾ����");
					return;
				}
				//�ͷ�
				objDeleteVerify=null;

				int intCurrentIndex = this.m_cboDateTime.SelectedIndex;
				string strid = m_cboTempIdArr.Items[intCurrentIndex].ToString();
				if(strid == null || strid == string.Empty)
					return;

                //clsVeinSpecialUseDrug_ConService objSev =
                //    (clsVeinSpecialUseDrug_ConService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsVeinSpecialUseDrug_ConService));


				long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngDeleteBeginEndDate(strid);
				if(lngRes <= 0)
				{
					MDIParent.ShowInformationMessageBox("ɾ��ʧ�ܣ�");
					return;
				}
				lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngDeleteMain(strid,MDIParent.OperatorID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				if(lngRes > 0)
				{
					m_cboDateTime.Items.RemoveAt(intCurrentIndex);					
					m_cboTempEndDateArr.Items.RemoveAt(intCurrentIndex) ;
					m_cboTempBeginDateArr.Items.RemoveAt(intCurrentIndex);
					if(m_cboDateTime.Items.Count > 0)
					{
						m_cboDateTime.SelectedIndex = m_cboDateTime.Items.Count - 1;
					}
					MDIParent.ShowInformationMessageBox("ɾ���ɹ���");
				} 
			}
        }

        protected override void m_mthPerformSessionChanged(clsEmrPatientSessionInfo_VO p_objSelectedSession, int p_intIndex)
        {
            if (p_objSelectedSession == null) return;
            if (m_dtgRecordDetail != null && m_txtEndDate != null)
            {
                m_txtEndDate.Text = "";
                base.m_mthPerformSessionChanged(p_objSelectedSession, p_intIndex);
            }
            if (p_objSelectedSession == null || m_objCurrentPatient == null)
            {
                return;
            }

            SetSelectDate(m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (m_cboDateTime.Items.Count > 0)
            {
                m_cboDateTime.SelectedIndex = m_cboDateTime.Items.Count - 1;
            }
        }
	}
}

