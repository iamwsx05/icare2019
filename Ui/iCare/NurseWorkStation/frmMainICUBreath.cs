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
using System.Xml;
using System.IO;


namespace iCare
{
	public class frmMainICUBreath : iCare.frmRecordsBase
	{
		private System.Windows.Forms.DataGridTextBoxColumn m_dtcDate;
		private System.Windows.Forms.DataGridTextBoxColumn m_dtcTime;
		private cltDataGridDSTRichTextBox m_dtcMachineMode;
		private cltDataGridDSTRichTextBox m_dtcBreathSoundLeft;
		private cltDataGridDSTRichTextBox m_dtcBreathSoundRight;
		private cltDataGridDSTRichTextBox m_dtcInLength;
		private cltDataGridDSTRichTextBox m_dtcGasbagPress;
		private cltDataGridDSTRichTextBox m_dtcTIDAL_VOLUME;
		private cltDataGridDSTRichTextBox m_dtcRATE;
		private cltDataGridDSTRichTextBox m_dtcPEAK_FLOW;
		private cltDataGridDSTRichTextBox m_dtcO2;
		private cltDataGridDSTRichTextBox m_dtcPS;
		private cltDataGridDSTRichTextBox m_dtcASSIST_SENSITIVITY;
		private cltDataGridDSTRichTextBox m_dtcINSPIRATORY_PAUSE;
		private cltDataGridDSTRichTextBox m_dtcMMV_LEVEL;
		private cltDataGridDSTRichTextBox m_dtcCOMPLIANCE_COMP;
		private cltDataGridDSTRichTextBox m_dtcINSPIRATORY_TIME;
		private cltDataGridDSTRichTextBox m_dtcINSPIRATORY_PRESSURE;
		private cltDataGridDSTRichTextBox m_dtcBASE_FLOW;
		private cltDataGridDSTRichTextBox m_dtcFLOW_TRIGGER;
		private cltDataGridDSTRichTextBox m_dtcPRESSURE_SLOPE;
		private cltDataGridDSTRichTextBox m_dtcPEEP;
		private cltDataGridDSTRichTextBox m_dtcTIDAL_VOL;
		private cltDataGridDSTRichTextBox m_dtcTOTAL_MV;
		private cltDataGridDSTRichTextBox m_dtcSPONT_MV;
		private cltDataGridDSTRichTextBox m_dtcTOTAL;
		private cltDataGridDSTRichTextBox m_dtcSPONT;
		private cltDataGridDSTRichTextBox m_dtcI_E_RATIO;
		private cltDataGridDSTRichTextBox m_dtcTi;
		private cltDataGridDSTRichTextBox m_dtcMMV;
		private cltDataGridDSTRichTextBox m_dtcPEAR;
		private cltDataGridDSTRichTextBox m_dtcMEAN;
		private cltDataGridDSTRichTextBox m_dtcPLATEAU;
		private System.Windows.Forms.DataGridTextBoxColumn m_dtcSign;
		private System.Windows.Forms.Panel panel1;
		private System.ComponentModel.IContainer components = null;

		public frmMainICUBreath()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}		

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		protected override Font m_FntHeaderFont
		{
			get
			{
				return new System.Drawing.Font("SimSun", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			}
		}

		/// <summary>
		/// �趨���ڱȽ����ڵĳ�ʼֵ
		/// </summary>
		private DateTime m_dtmPreRecordDate;

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_dtcDate = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcTime = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcMachineMode = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBreathSoundLeft = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBreathSoundRight = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcInLength = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcGasbagPress = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcTIDAL_VOLUME = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcRATE = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPEAK_FLOW = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcO2 = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPS = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcASSIST_SENSITIVITY = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcINSPIRATORY_PAUSE = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcMMV_LEVEL = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcCOMPLIANCE_COMP = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcINSPIRATORY_TIME = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcINSPIRATORY_PRESSURE = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBASE_FLOW = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcFLOW_TRIGGER = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPRESSURE_SLOPE = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPEEP = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcTIDAL_VOL = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcTOTAL_MV = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcSPONT_MV = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcTOTAL = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcSPONT = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcI_E_RATIO = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcTi = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcMMV = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPEAR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcMEAN = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPLATEAU = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcSign = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).BeginInit();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgtsStyles
            // 
            this.dgtsStyles.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																										 this.m_dtcDate,
																										 this.m_dtcTime,
																										 this.m_dtcMachineMode,
																										 this.m_dtcBreathSoundLeft,
																										 this.m_dtcBreathSoundRight,
																										 this.m_dtcInLength,
																										 this.m_dtcGasbagPress,
																										 this.m_dtcTIDAL_VOLUME,
																										 this.m_dtcRATE,
																										 this.m_dtcPEAK_FLOW,
																										 this.m_dtcO2,
																										 this.m_dtcPS,
																										 this.m_dtcASSIST_SENSITIVITY,
																										 this.m_dtcINSPIRATORY_PAUSE,
																										 this.m_dtcMMV_LEVEL,
																										 this.m_dtcCOMPLIANCE_COMP,
																										 this.m_dtcINSPIRATORY_TIME,
																										 this.m_dtcINSPIRATORY_PRESSURE,
																										 this.m_dtcBASE_FLOW,
																										 this.m_dtcFLOW_TRIGGER,
																										 this.m_dtcPRESSURE_SLOPE,
																										 this.m_dtcPEEP,
																										 this.m_dtcTIDAL_VOL,
																										 this.m_dtcTOTAL_MV,
																										 this.m_dtcSPONT_MV,
																										 this.m_dtcTOTAL,
																										 this.m_dtcSPONT,
																										 this.m_dtcI_E_RATIO,
																										 this.m_dtcTi,
																										 this.m_dtcMMV,
																										 this.m_dtcPEAR,
																										 this.m_dtcMEAN,
																										 this.m_dtcPLATEAU,
																										 this.m_dtcSign});
            this.dgtsStyles.RowHeaderWidth = 15;
            // 
            // m_dtgRecordDetail
            // 
            this.m_dtgRecordDetail.AccessibleName = "DataGrid";
            this.m_dtgRecordDetail.AccessibleRole = System.Windows.Forms.AccessibleRole.Table;
            this.m_dtgRecordDetail.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
            this.m_dtgRecordDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_dtgRecordDetail.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_dtgRecordDetail.CaptionFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtgRecordDetail.DataSource = this.m_dtbRecords;
            this.m_dtgRecordDetail.Location = new System.Drawing.Point(24, 81);
            this.m_dtgRecordDetail.Size = new System.Drawing.Size(748, 534);
            // 
            // mniAppend
            // 
            this.mniAppend.Click += new System.EventHandler(this.mniAppend_Click);
            // 
            // m_trvInPatientDate
            // 
            this.m_trvInPatientDate.BackColor = System.Drawing.Color.White;
            this.m_trvInPatientDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_trvInPatientDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_trvInPatientDate.ForeColor = System.Drawing.Color.Black;
            this.m_trvInPatientDate.ItemHeight = 18;
            this.m_trvInPatientDate.LineColor = System.Drawing.Color.Black;
            this.m_trvInPatientDate.Location = new System.Drawing.Point(16, 108);
            this.m_trvInPatientDate.Size = new System.Drawing.Size(166, 52);
            this.m_trvInPatientDate.Visible = false;
            // 
            // lblSex
            // 
            this.lblSex.BackColor = System.Drawing.SystemColors.Control;
            this.lblSex.Location = new System.Drawing.Point(557, 116);
            this.lblSex.Size = new System.Drawing.Size(41, 19);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.BackColor = System.Drawing.SystemColors.Control;
            this.lblAge.Location = new System.Drawing.Point(639, 116);
            this.lblAge.Size = new System.Drawing.Size(41, 19);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(388, 108);
            this.lblBedNoTitle.Size = new System.Drawing.Size(56, 14);
            this.lblBedNoTitle.Text = "��  ��:";
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(388, 138);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(518, 108);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(516, 138);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(598, 116);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(193, 138);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(452, 157);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(56, 104);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(452, 136);
            this.txtInPatientID.Size = new System.Drawing.Size(56, 23);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(568, 106);
            this.m_txtPatientName.Size = new System.Drawing.Size(92, 23);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(454, 106);
            this.m_txtBedNO.Size = new System.Drawing.Size(41, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(244, 134);
            this.m_cboArea.Size = new System.Drawing.Size(136, 23);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(568, 127);
            this.m_lsvPatientName.Size = new System.Drawing.Size(92, 104);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(454, 127);
            this.m_lsvBedNO.Size = new System.Drawing.Size(41, 104);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(244, 104);
            this.m_cboDept.Size = new System.Drawing.Size(136, 23);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(193, 108);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(308, 103);
            this.m_cmdNewTemplate.Size = new System.Drawing.Size(74, 32);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Location = new System.Drawing.Point(492, 106);
            this.m_cmdNext.Size = new System.Drawing.Size(16, 21);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(578, 42);
            this.m_cmdPre.Size = new System.Drawing.Size(8, 3);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(516, 14);
            this.m_lblForTitle.Size = new System.Drawing.Size(8, 3);
            this.m_lblForTitle.Visible = false;
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(564, 39);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(663, 38);
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
            // m_dtcDate
            // 
            this.m_dtcDate.Format = "";
            this.m_dtcDate.FormatInfo = null;
            this.m_dtcDate.MappingName = "Date";
            this.m_dtcDate.NullText = "";
            this.m_dtcDate.Width = 80;
            // 
            // m_dtcTime
            // 
            this.m_dtcTime.Format = "";
            this.m_dtcTime.FormatInfo = null;
            this.m_dtcTime.MappingName = "Time";
            this.m_dtcTime.NullText = "";
            this.m_dtcTime.Width = 50;
            // 
            // m_dtcMachineMode
            // 
            this.m_dtcMachineMode.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcMachineMode.m_BlnGobleSet = true;
            this.m_dtcMachineMode.m_BlnUnderLineDST = false;
            this.m_dtcMachineMode.MappingName = "MachineMode";
            this.m_dtcMachineMode.NullText = "";
            this.m_dtcMachineMode.Width = 30;
            // 
            // m_dtcBreathSoundLeft
            // 
            this.m_dtcBreathSoundLeft.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBreathSoundLeft.m_BlnGobleSet = true;
            this.m_dtcBreathSoundLeft.m_BlnUnderLineDST = false;
            this.m_dtcBreathSoundLeft.MappingName = "BreathSoundLeft";
            this.m_dtcBreathSoundLeft.NullText = "";
            this.m_dtcBreathSoundLeft.Width = 30;
            // 
            // m_dtcBreathSoundRight
            // 
            this.m_dtcBreathSoundRight.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBreathSoundRight.m_BlnGobleSet = true;
            this.m_dtcBreathSoundRight.m_BlnUnderLineDST = false;
            this.m_dtcBreathSoundRight.MappingName = "BreathSoundRight";
            this.m_dtcBreathSoundRight.NullText = "";
            this.m_dtcBreathSoundRight.Width = 30;
            // 
            // m_dtcInLength
            // 
            this.m_dtcInLength.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcInLength.m_BlnGobleSet = true;
            this.m_dtcInLength.m_BlnUnderLineDST = false;
            this.m_dtcInLength.MappingName = "InLength";
            this.m_dtcInLength.NullText = "";
            this.m_dtcInLength.Width = 30;
            // 
            // m_dtcGasbagPress
            // 
            this.m_dtcGasbagPress.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcGasbagPress.m_BlnGobleSet = true;
            this.m_dtcGasbagPress.m_BlnUnderLineDST = false;
            this.m_dtcGasbagPress.MappingName = "GasbagPress";
            this.m_dtcGasbagPress.NullText = "";
            this.m_dtcGasbagPress.Width = 30;
            // 
            // m_dtcTIDAL_VOLUME
            // 
            this.m_dtcTIDAL_VOLUME.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcTIDAL_VOLUME.m_BlnGobleSet = true;
            this.m_dtcTIDAL_VOLUME.m_BlnUnderLineDST = false;
            this.m_dtcTIDAL_VOLUME.MappingName = "TIDAL_VOLUME";
            this.m_dtcTIDAL_VOLUME.NullText = "";
            this.m_dtcTIDAL_VOLUME.Width = 30;
            // 
            // m_dtcRATE
            // 
            this.m_dtcRATE.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcRATE.m_BlnGobleSet = true;
            this.m_dtcRATE.m_BlnUnderLineDST = false;
            this.m_dtcRATE.MappingName = "RATE";
            this.m_dtcRATE.NullText = "";
            this.m_dtcRATE.Width = 30;
            // 
            // m_dtcPEAK_FLOW
            // 
            this.m_dtcPEAK_FLOW.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPEAK_FLOW.m_BlnGobleSet = true;
            this.m_dtcPEAK_FLOW.m_BlnUnderLineDST = false;
            this.m_dtcPEAK_FLOW.MappingName = "PEAK_FLOW";
            this.m_dtcPEAK_FLOW.NullText = "";
            this.m_dtcPEAK_FLOW.Width = 30;
            // 
            // m_dtcO2
            // 
            this.m_dtcO2.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcO2.m_BlnGobleSet = true;
            this.m_dtcO2.m_BlnUnderLineDST = false;
            this.m_dtcO2.MappingName = "O2";
            this.m_dtcO2.NullText = "";
            this.m_dtcO2.Width = 30;
            // 
            // m_dtcPS
            // 
            this.m_dtcPS.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPS.m_BlnGobleSet = true;
            this.m_dtcPS.m_BlnUnderLineDST = false;
            this.m_dtcPS.MappingName = "PS";
            this.m_dtcPS.NullText = "";
            this.m_dtcPS.Width = 30;
            // 
            // m_dtcASSIST_SENSITIVITY
            // 
            this.m_dtcASSIST_SENSITIVITY.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcASSIST_SENSITIVITY.m_BlnGobleSet = true;
            this.m_dtcASSIST_SENSITIVITY.m_BlnUnderLineDST = false;
            this.m_dtcASSIST_SENSITIVITY.MappingName = "ASSIST_SENSITIVITY";
            this.m_dtcASSIST_SENSITIVITY.NullText = "";
            this.m_dtcASSIST_SENSITIVITY.Width = 30;
            // 
            // m_dtcINSPIRATORY_PAUSE
            // 
            this.m_dtcINSPIRATORY_PAUSE.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcINSPIRATORY_PAUSE.m_BlnGobleSet = true;
            this.m_dtcINSPIRATORY_PAUSE.m_BlnUnderLineDST = false;
            this.m_dtcINSPIRATORY_PAUSE.MappingName = "INSPIRATORY_PAUSE";
            this.m_dtcINSPIRATORY_PAUSE.NullText = "";
            this.m_dtcINSPIRATORY_PAUSE.Width = 30;
            // 
            // m_dtcMMV_LEVEL
            // 
            this.m_dtcMMV_LEVEL.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcMMV_LEVEL.m_BlnGobleSet = true;
            this.m_dtcMMV_LEVEL.m_BlnUnderLineDST = false;
            this.m_dtcMMV_LEVEL.MappingName = "MMV_LEVEL";
            this.m_dtcMMV_LEVEL.NullText = "";
            this.m_dtcMMV_LEVEL.Width = 30;
            // 
            // m_dtcCOMPLIANCE_COMP
            // 
            this.m_dtcCOMPLIANCE_COMP.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcCOMPLIANCE_COMP.m_BlnGobleSet = true;
            this.m_dtcCOMPLIANCE_COMP.m_BlnUnderLineDST = false;
            this.m_dtcCOMPLIANCE_COMP.MappingName = "COMPLIANCE_COMP";
            this.m_dtcCOMPLIANCE_COMP.NullText = "";
            this.m_dtcCOMPLIANCE_COMP.Width = 30;
            // 
            // m_dtcINSPIRATORY_TIME
            // 
            this.m_dtcINSPIRATORY_TIME.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcINSPIRATORY_TIME.m_BlnGobleSet = true;
            this.m_dtcINSPIRATORY_TIME.m_BlnUnderLineDST = false;
            this.m_dtcINSPIRATORY_TIME.MappingName = "INSPIRATORY_TIME";
            this.m_dtcINSPIRATORY_TIME.NullText = "";
            this.m_dtcINSPIRATORY_TIME.Width = 30;
            // 
            // m_dtcINSPIRATORY_PRESSURE
            // 
            this.m_dtcINSPIRATORY_PRESSURE.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcINSPIRATORY_PRESSURE.m_BlnGobleSet = true;
            this.m_dtcINSPIRATORY_PRESSURE.m_BlnUnderLineDST = false;
            this.m_dtcINSPIRATORY_PRESSURE.MappingName = "INSPIRATORY_PRESSURE";
            this.m_dtcINSPIRATORY_PRESSURE.NullText = "";
            this.m_dtcINSPIRATORY_PRESSURE.Width = 30;
            // 
            // m_dtcBASE_FLOW
            // 
            this.m_dtcBASE_FLOW.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBASE_FLOW.m_BlnGobleSet = true;
            this.m_dtcBASE_FLOW.m_BlnUnderLineDST = false;
            this.m_dtcBASE_FLOW.MappingName = "BASE_FLOW";
            this.m_dtcBASE_FLOW.NullText = "";
            this.m_dtcBASE_FLOW.Width = 30;
            // 
            // m_dtcFLOW_TRIGGER
            // 
            this.m_dtcFLOW_TRIGGER.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcFLOW_TRIGGER.m_BlnGobleSet = true;
            this.m_dtcFLOW_TRIGGER.m_BlnUnderLineDST = false;
            this.m_dtcFLOW_TRIGGER.MappingName = "FLOW_TRIGGER";
            this.m_dtcFLOW_TRIGGER.NullText = "";
            this.m_dtcFLOW_TRIGGER.Width = 30;
            // 
            // m_dtcPRESSURE_SLOPE
            // 
            this.m_dtcPRESSURE_SLOPE.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPRESSURE_SLOPE.m_BlnGobleSet = true;
            this.m_dtcPRESSURE_SLOPE.m_BlnUnderLineDST = false;
            this.m_dtcPRESSURE_SLOPE.MappingName = "PRESSURE_SLOPE";
            this.m_dtcPRESSURE_SLOPE.NullText = "";
            this.m_dtcPRESSURE_SLOPE.Width = 30;
            // 
            // m_dtcPEEP
            // 
            this.m_dtcPEEP.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPEEP.m_BlnGobleSet = true;
            this.m_dtcPEEP.m_BlnUnderLineDST = false;
            this.m_dtcPEEP.MappingName = "PEEP";
            this.m_dtcPEEP.NullText = "";
            this.m_dtcPEEP.Width = 30;
            // 
            // m_dtcTIDAL_VOL
            // 
            this.m_dtcTIDAL_VOL.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcTIDAL_VOL.m_BlnGobleSet = true;
            this.m_dtcTIDAL_VOL.m_BlnUnderLineDST = false;
            this.m_dtcTIDAL_VOL.MappingName = "TIDAL_VOL";
            this.m_dtcTIDAL_VOL.NullText = "";
            this.m_dtcTIDAL_VOL.Width = 30;
            // 
            // m_dtcTOTAL_MV
            // 
            this.m_dtcTOTAL_MV.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcTOTAL_MV.m_BlnGobleSet = true;
            this.m_dtcTOTAL_MV.m_BlnUnderLineDST = false;
            this.m_dtcTOTAL_MV.MappingName = "TOTAL_MV";
            this.m_dtcTOTAL_MV.NullText = "";
            this.m_dtcTOTAL_MV.Width = 30;
            // 
            // m_dtcSPONT_MV
            // 
            this.m_dtcSPONT_MV.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcSPONT_MV.m_BlnGobleSet = true;
            this.m_dtcSPONT_MV.m_BlnUnderLineDST = false;
            this.m_dtcSPONT_MV.MappingName = "SPONT_MV";
            this.m_dtcSPONT_MV.NullText = "";
            this.m_dtcSPONT_MV.Width = 30;
            // 
            // m_dtcTOTAL
            // 
            this.m_dtcTOTAL.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcTOTAL.m_BlnGobleSet = true;
            this.m_dtcTOTAL.m_BlnUnderLineDST = false;
            this.m_dtcTOTAL.MappingName = "TOTAL";
            this.m_dtcTOTAL.NullText = "";
            this.m_dtcTOTAL.Width = 30;
            // 
            // m_dtcSPONT
            // 
            this.m_dtcSPONT.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcSPONT.m_BlnGobleSet = true;
            this.m_dtcSPONT.m_BlnUnderLineDST = false;
            this.m_dtcSPONT.MappingName = "SPONT";
            this.m_dtcSPONT.NullText = "";
            this.m_dtcSPONT.Width = 30;
            // 
            // m_dtcI_E_RATIO
            // 
            this.m_dtcI_E_RATIO.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcI_E_RATIO.m_BlnGobleSet = true;
            this.m_dtcI_E_RATIO.m_BlnUnderLineDST = false;
            this.m_dtcI_E_RATIO.MappingName = "I_E_RATIO";
            this.m_dtcI_E_RATIO.NullText = "";
            this.m_dtcI_E_RATIO.Width = 30;
            // 
            // m_dtcTi
            // 
            this.m_dtcTi.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcTi.m_BlnGobleSet = true;
            this.m_dtcTi.m_BlnUnderLineDST = false;
            this.m_dtcTi.MappingName = "Ti";
            this.m_dtcTi.NullText = "";
            this.m_dtcTi.Width = 30;
            // 
            // m_dtcMMV
            // 
            this.m_dtcMMV.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcMMV.m_BlnGobleSet = true;
            this.m_dtcMMV.m_BlnUnderLineDST = false;
            this.m_dtcMMV.MappingName = "MMV";
            this.m_dtcMMV.NullText = "";
            this.m_dtcMMV.Width = 30;
            // 
            // m_dtcPEAR
            // 
            this.m_dtcPEAR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPEAR.m_BlnGobleSet = true;
            this.m_dtcPEAR.m_BlnUnderLineDST = false;
            this.m_dtcPEAR.MappingName = "PEAR";
            this.m_dtcPEAR.NullText = "";
            this.m_dtcPEAR.Width = 30;
            // 
            // m_dtcMEAN
            // 
            this.m_dtcMEAN.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcMEAN.m_BlnGobleSet = true;
            this.m_dtcMEAN.m_BlnUnderLineDST = false;
            this.m_dtcMEAN.MappingName = "MEAN";
            this.m_dtcMEAN.NullText = "";
            this.m_dtcMEAN.Width = 30;
            // 
            // m_dtcPLATEAU
            // 
            this.m_dtcPLATEAU.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPLATEAU.m_BlnGobleSet = true;
            this.m_dtcPLATEAU.m_BlnUnderLineDST = false;
            this.m_dtcPLATEAU.MappingName = "PLATEAU";
            this.m_dtcPLATEAU.NullText = "";
            this.m_dtcPLATEAU.Width = 30;
            // 
            // m_dtcSign
            // 
            this.m_dtcSign.Format = "";
            this.m_dtcSign.FormatInfo = null;
            this.m_dtcSign.MappingName = "Sign";
            this.m_dtcSign.NullText = "";
            this.m_dtcSign.Width = 75;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(16, 74);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(764, 546);
            this.panel1.TabIndex = 10000004;
            // 
            // frmMainICUBreath
            // 
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(792, 673);
            this.Controls.Add(this.panel1);
            this.Name = "frmMainICUBreath";
            this.Load += new System.EventHandler(this.frmMainICUBreath_Load);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.m_trvInPatientDate, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.m_dtgRecordDetail, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).EndInit();
            this.m_pnlNewBase.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// ��ʼ���������DataTable��
		/// ע�⣬DataTable�ĵ�һ��Column�����Ǵ�ż�¼ʱ����ַ������ڶ���Column�����Ǵ�ż�¼���͵�intֵ��������Column�����Ǵ�ż�¼��OpenDate
		/// </summary>
		/// <param name="p_dtbRecordTable"></param>
		protected override void m_mthInitDataTable(DataTable p_dtbRecordTable)
		{
			int intPerformWith=45;
			#region Add Column

			//��ż�¼ʱ����ַ���
			p_dtbRecordTable.Columns.Add("CreateDate");
		
			//��ż�¼���͵�intֵ
			DataColumn dcRecordType = new DataColumn("RecordType",typeof(int));
			p_dtbRecordTable.Columns.Add(dcRecordType);
		
			//��ż�¼��OpenDate�ַ���
			p_dtbRecordTable.Columns.Add("OpenDate");  
		
			//��ż�¼��ModifyDate�ַ���
			p_dtbRecordTable.Columns.Add("ModifyDate");

			//��������ַ���
			p_dtbRecordTable.Columns.Add("Date");
			this.m_dtcDate.HeaderText = "  ��\r\n\r\n\r\n\r\n  ��";
			this.m_dtcDate.Width=80;

			//���ʱ���ַ���
			p_dtbRecordTable.Columns.Add("Time");
			this.m_dtcTime.HeaderText = "  ʱ\r\n\r\n\r\n\r\n  ��";			
			this.m_dtcTime.Width=65;
		
			//ͨ����ʽ���ַ��ͣ���ѡһ����
			//��Assist CMV(��������);
			//��SIMV/CPAP(PSV)(ͬ����Ъָ����ͨ��/����������ѹͨ��(ѹ��֧��));
			//��PRESSURE CONTROL(ѹ������);
			//��PC-SIMV/CPAP(PSV)(ѹ�����Ƶ�SIMV/CPAP(PSV))
			p_dtbRecordTable.Columns.Add("MachineMode",typeof(clsDSTRichTextBoxValue));
			this.m_dtcMachineMode.HeaderText = "    ͨ\r\n    ��\r\n    ��\r\n    ʽ";
			this.m_dtcMachineMode.Width=100;

			//����������
			p_dtbRecordTable.Columns.Add("BreathSoundLeft",typeof(clsDSTRichTextBoxValue));
			this.m_dtcBreathSoundLeft.HeaderText = "��\r\n��\r\n��\r\n��";
			this.m_dtcBreathSoundLeft.Width=intPerformWith+5;

			//���������ң�
			p_dtbRecordTable.Columns.Add("BreathSoundRight",typeof(clsDSTRichTextBoxValue));
			this.m_dtcBreathSoundRight.HeaderText = "��\r\n��\r\n��\r\n��";
			this.m_dtcBreathSoundRight.Width=intPerformWith+5;

			//�����ȣ������ͣ�22~28��cm
			p_dtbRecordTable.Columns.Add("InLength",typeof(clsDSTRichTextBoxValue));
			this.m_dtcInLength.HeaderText = "��\r\n��\r\n��\r\n��\r\n\r\n\r\ncm";
			this.m_dtcInLength.Width=intPerformWith;

			//����ѹ���������ͣ�0~35��cmH2O
			p_dtbRecordTable.Columns.Add("GasbagPress",typeof(clsDSTRichTextBoxValue));
			this.m_dtcGasbagPress.HeaderText = "��\r\n��\r\nѹ\r\n��\r\n\r\n\r\ncmH2O";
			this.m_dtcGasbagPress.Width=intPerformWith;

			//�趨��������������Ϊ��TIDAL VOLUME(������);RATE(Ƶ��);PEAK FLOW(��ֵ����);
			//O2%(��Ũ��);PS(ѹ��֧��);ASSIST SENSITIVITY(����������);INSPIRATORY PAUSE(������ͣ);
			//MMV LEVEL(ˮƽ����);COMPLIANCE COMP(˳Ӧ�Բ���);INSPIRATORY TIME(����ʱ��);
			//INSPIRATORY PRESSURE(����ѹ��);BASE FLOW(��������);FLOW TRIGGER(��������);PRESSURE SLOPE(ѹ��б��);PEEP
			
			//TIDAL VOLUME(������)
			p_dtbRecordTable.Columns.Add("TIDAL_VOLUME",typeof(clsDSTRichTextBoxValue));
			this.m_dtcTIDAL_VOLUME.HeaderText = "��\r\n��\r\n��\r\n(TV)\r\n\r\n\r\nL";
			this.m_dtcTIDAL_VOLUME.Width=intPerformWith;

			//RATE(Ƶ��)
			p_dtbRecordTable.Columns.Add("RATE",typeof(clsDSTRichTextBoxValue));
			this.m_dtcRATE.HeaderText = "Ƶ\r\n��\r\n(R)\r\n\r\n\r\n\r\nBPM";
			this.m_dtcRATE.Width=intPerformWith;

			//PEAK FLOW(��ֵ����)
			p_dtbRecordTable.Columns.Add("PEAK_FLOW",typeof(clsDSTRichTextBoxValue));
			this.m_dtcPEAK_FLOW.HeaderText = "��\r\nֵ\r\n��\r\n��\r\n(PF)\r\n\r\nLPM";
			this.m_dtcPEAK_FLOW.Width=intPerformWith;

			//O2%(��Ũ��)
			p_dtbRecordTable.Columns.Add("O2",typeof(clsDSTRichTextBoxValue));
			this.m_dtcO2.HeaderText = "��\r\nŨ\r\n��\r\n(O2%)\r\n\r\n\r\n%";
			this.m_dtcO2.Width=intPerformWith;

			//PS(ѹ��֧��)
			p_dtbRecordTable.Columns.Add("PS",typeof(clsDSTRichTextBoxValue));
			this.m_dtcPS.HeaderText = "ѹ\r\n��\r\n֧\r\n��\r\n(PS)\r\n\r\ncmH2O";	
			this.m_dtcPS.Width=intPerformWith;

			//ASSIST SENSITIVITY(����������)
			p_dtbRecordTable.Columns.Add("ASSIST_SENSITIVITY",typeof(clsDSTRichTextBoxValue));
			this.m_dtcASSIST_SENSITIVITY.HeaderText = "��\r\n��\r\n��\r\n��\r\n��\r\n(AS)\r\ncmH2O";
			this.m_dtcASSIST_SENSITIVITY.Width=intPerformWith;

			//INSPIRATORY PAUSE(������ͣ)
			p_dtbRecordTable.Columns.Add("INSPIRATORY_PAUSE",typeof(clsDSTRichTextBoxValue));
			this.m_dtcINSPIRATORY_PAUSE.HeaderText = "��\r\n��\r\n��\r\nͣ\r\n(IPu)\r\n\r\n��";
			this.m_dtcINSPIRATORY_PAUSE.Width=intPerformWith;

			//MMV LEVEL(ˮƽ����)
			p_dtbRecordTable.Columns.Add("MMV_LEVEL",typeof(clsDSTRichTextBoxValue));
			this.m_dtcMMV_LEVEL.HeaderText = "ˮ\r\nƽ\r\n��\r\n��\r\n(ML)\r\n\r\nLPM";
			this.m_dtcMMV_LEVEL.Width=intPerformWith;

			//COMPLIANCE COMP(˳Ӧ�Բ���)
			p_dtbRecordTable.Columns.Add("COMPLIANCE_COMP",typeof(clsDSTRichTextBoxValue));
			this.m_dtcCOMPLIANCE_COMP.HeaderText = "˳\r\nӦ\r\n��\r\n��\r\n��\r\n(CC)\r\ncmH2O";
			this.m_dtcCOMPLIANCE_COMP.Width=intPerformWith;

			//INSPIRATORY TIME(����ʱ��)
			p_dtbRecordTable.Columns.Add("INSPIRATORY_TIME",typeof(clsDSTRichTextBoxValue));
			this.m_dtcINSPIRATORY_TIME.HeaderText = "��\r\n��\r\nʱ\r\n��\r\n(IT)\r\n\r\n��";
			this.m_dtcINSPIRATORY_TIME.Width=intPerformWith;

			//INSPIRATORY PRESSURE(����ѹ��)
			p_dtbRecordTable.Columns.Add("INSPIRATORY_PRESSURE",typeof(clsDSTRichTextBoxValue));
			this.m_dtcINSPIRATORY_PRESSURE.HeaderText = "��\r\n��\r\nѹ\r\n��\r\n(IPr)\r\n\r\ncmH2O";
			this.m_dtcINSPIRATORY_PRESSURE.Width=intPerformWith;

			//BASE FLOW(��������)
			p_dtbRecordTable.Columns.Add("BASE_FLOW",typeof(clsDSTRichTextBoxValue)); 
			this.m_dtcBASE_FLOW.HeaderText = "��\r\n��\r\n��\r\n��\r\n(BF)\r\n\r\nLPM";
			this.m_dtcBASE_FLOW.Width=intPerformWith;

			//FLOW TRIGGER(��������)
			p_dtbRecordTable.Columns.Add("FLOW_TRIGGER",typeof(clsDSTRichTextBoxValue));   
			this.m_dtcFLOW_TRIGGER.HeaderText = "��\r\n��\r\n��\r\n��\r\n(FT)\r\n\r\nLPM";
			this.m_dtcFLOW_TRIGGER.Width=intPerformWith;

			//PRESSURE SLOPE(ѹ��б��)
			p_dtbRecordTable.Columns.Add("PRESSURE_SLOPE",typeof(clsDSTRichTextBoxValue)); 
			this.m_dtcPRESSURE_SLOPE.HeaderText = "ѹ\r\n��\r\nб\r\n��\r\n(PSe)";
			this.m_dtcPRESSURE_SLOPE.Width=intPerformWith;

			//PEEP
			p_dtbRecordTable.Columns.Add("PEEP",typeof(clsDSTRichTextBoxValue)); 
			this.m_dtcPEEP.HeaderText = "PEEP\r\n\r\n\r\n\r\n\r\n\r\ncmH2O";
			this.m_dtcPEEP.Width=intPerformWith;
			
			
			//�ٲ���ֵ����������Ϊ��TIDAL VOL(������);TOTAL MV(�ܷ���ͨ����);
			//SPONT MV(������������ͨ����);TOTAL(��Ƶ��);SPONT(��������Ƶ��);
			//I:E RATIO(����������);Ti(����ʱ��);MMV%(��С����ͨ����ͨ���ٷֱ�);
			//PEAR(��ѹ);MEAN(ƽ��ѹ);PLATEAU(ƽ̨ѹ)
			
			//TIDAL VOL(������)
			p_dtbRecordTable.Columns.Add("TIDAL_VOL",typeof(clsDSTRichTextBoxValue)); 
			this.m_dtcTIDAL_VOL.HeaderText = "��\r\n��\r\n��\r\n(TV)\r\n\r\n\r\nL";	
			this.m_dtcTIDAL_VOL.Width=intPerformWith;

			//TOTAL MV(�ܷ���ͨ����)
			p_dtbRecordTable.Columns.Add("TOTAL_MV",typeof(clsDSTRichTextBoxValue));
			this.m_dtcTOTAL_MV.HeaderText = "��\r\n����\r\nͨ��\r\n��\r\n(TM)\r\n\r\nLPM";
			this.m_dtcTOTAL_MV.Width=intPerformWith;

			//SPONT MV(������������ͨ����)
			p_dtbRecordTable.Columns.Add("SPONT_MV",typeof(clsDSTRichTextBoxValue));
			this.m_dtcSPONT_MV.HeaderText =  "����\r\n����\r\n����\r\nͨ��\r\n��\r\n(SM)\r\nLPM";
			this.m_dtcSPONT_MV.Width=intPerformWith;

			//TOTAL(��Ƶ��)
			p_dtbRecordTable.Columns.Add("TOTAL",typeof(clsDSTRichTextBoxValue));
			this.m_dtcTOTAL.HeaderText = "��\r\nƵ\r\n��\r\n(To)\r\n\r\n\r\nBPM";
			this.m_dtcTOTAL.Width=intPerformWith;

			//SPONT(��������Ƶ��)
			p_dtbRecordTable.Columns.Add("SPONT",typeof(clsDSTRichTextBoxValue));
			this.m_dtcSPONT.HeaderText = "����\r\n����\r\nƵ��\r\n(S)\r\n\r\n\r\nBPM";
			this.m_dtcSPONT.Width=intPerformWith;

			//I:E RATIO(����������)
			p_dtbRecordTable.Columns.Add("I_E_RATIO",typeof(clsDSTRichTextBoxValue));
			this.m_dtcI_E_RATIO.HeaderText = "��\r\n��\r\n��\r\n��\r\n��\r\n(IER)";
			this.m_dtcI_E_RATIO.Width=intPerformWith;

			//Ti(����ʱ��)
			p_dtbRecordTable.Columns.Add("Ti",typeof(clsDSTRichTextBoxValue));
			this.m_dtcTi.HeaderText = "��\r\n��\r\nʱ\r\n��\r\n(Ti)\r\n\r\n��";
			this.m_dtcTi.Width=intPerformWith;

			//MMV%(��С����ͨ����ͨ���ٷֱ�)
			p_dtbRecordTable.Columns.Add("MMV",typeof(clsDSTRichTextBoxValue));
			this.m_dtcMMV.HeaderText = "��С\r\n����\r\nͨ��\r\n��ͨ\r\n����\r\n�ֱ�\r\n(MMV)%";
			this.m_dtcMMV.Width=intPerformWith+5;

			//PEAR(��ѹ)
			p_dtbRecordTable.Columns.Add("PEAR",typeof(clsDSTRichTextBoxValue));
			this.m_dtcPEAR.HeaderText = "��\r\nѹ\r\n(Pe)\r\n\r\n\r\n\r\ncmH2O";
			this.m_dtcPEAR.Width=intPerformWith;

			//MEAN(ƽ��ѹ)
			p_dtbRecordTable.Columns.Add("MEAN",typeof(clsDSTRichTextBoxValue));
			this.m_dtcMEAN.HeaderText = "ƽ\r\n��\r\nѹ\r\n(Me)\r\n\r\n\r\ncmH2O";
			this.m_dtcMEAN.Width=intPerformWith;

			//PLATEAU(ƽ̨ѹ)
			p_dtbRecordTable.Columns.Add("PLATEAU",typeof(clsDSTRichTextBoxValue));
			this.m_dtcPLATEAU.HeaderText = "ƽ\r\n̨\r\nѹ\r\n(Pu)\r\n\r\n\r\ncmH2O";
			this.m_dtcPLATEAU.Width=intPerformWith;

			//ǩ��
			p_dtbRecordTable.Columns.Add("Sign");
			this.m_dtcSign.HeaderText = "ǩ��";
			this.m_dtcSign.Width=100;

			p_dtbRecordTable.Columns.Add("SignID");

			#endregion

			#region Set Header	

			m_mthSetControl(m_dtcDate);
			m_mthSetControl(m_dtcTime);
			m_mthSetControl(m_dtcMachineMode);
			m_mthSetControl(m_dtcBreathSoundLeft);
			m_mthSetControl(m_dtcBreathSoundRight);
			m_mthSetControl(m_dtcInLength);
			m_mthSetControl(m_dtcGasbagPress);
			m_mthSetControl(m_dtcTIDAL_VOLUME);
			m_mthSetControl(m_dtcRATE);
			m_mthSetControl(m_dtcPEAK_FLOW);
			m_mthSetControl(m_dtcO2);
			m_mthSetControl(m_dtcPS);
			m_mthSetControl(m_dtcASSIST_SENSITIVITY);
			m_mthSetControl(m_dtcINSPIRATORY_PAUSE);
			m_mthSetControl(m_dtcMMV_LEVEL);
			m_mthSetControl(m_dtcCOMPLIANCE_COMP);
			m_mthSetControl(m_dtcINSPIRATORY_TIME);
			m_mthSetControl(m_dtcINSPIRATORY_PRESSURE);
			m_mthSetControl(m_dtcBASE_FLOW);
			m_mthSetControl(m_dtcFLOW_TRIGGER);
			m_mthSetControl(m_dtcPRESSURE_SLOPE);
			m_mthSetControl(m_dtcPEEP);
			m_mthSetControl(m_dtcTIDAL_VOL);
			m_mthSetControl(m_dtcTOTAL_MV);
			m_mthSetControl(m_dtcSPONT_MV);
			m_mthSetControl(m_dtcTOTAL);
			m_mthSetControl(m_dtcSPONT);
			m_mthSetControl(m_dtcI_E_RATIO);
			m_mthSetControl(m_dtcTi);
			m_mthSetControl(m_dtcMMV);
			m_mthSetControl(m_dtcPEAR);
			m_mthSetControl(m_dtcMEAN);
			m_mthSetControl(m_dtcPLATEAU);
			m_mthSetControl(m_dtcSign);

			m_dtcDate.ReadOnly=true;
			m_dtcTime.ReadOnly=true;
			m_dtcMachineMode.ReadOnly=true;
			m_dtcBreathSoundLeft.ReadOnly=true;
			m_dtcBreathSoundRight.ReadOnly=true;
			m_dtcInLength.ReadOnly=true;
			m_dtcGasbagPress.ReadOnly=true;
			m_dtcTIDAL_VOLUME.ReadOnly=true;
			m_dtcRATE.ReadOnly=true;
			m_dtcPEAK_FLOW.ReadOnly=true;
			m_dtcO2.ReadOnly=true;
			m_dtcASSIST_SENSITIVITY.ReadOnly=true;
			m_dtcINSPIRATORY_PAUSE.ReadOnly=true;
			m_dtcMMV_LEVEL.ReadOnly=true;
			m_dtcCOMPLIANCE_COMP.ReadOnly=true;
			m_dtcINSPIRATORY_TIME.ReadOnly=true;
			m_dtcINSPIRATORY_PRESSURE.ReadOnly=true;
			m_dtcBASE_FLOW.ReadOnly=true;
			m_dtcFLOW_TRIGGER.ReadOnly=true;
			m_dtcPRESSURE_SLOPE.ReadOnly=true;
			m_dtcPEEP.ReadOnly=true;
			m_dtcTIDAL_VOL.ReadOnly=true;
			m_dtcTOTAL_MV.ReadOnly=true;
			m_dtcSPONT_MV.ReadOnly=true;
			m_dtcTOTAL.ReadOnly=true;
			m_dtcSPONT.ReadOnly=true;
			m_dtcI_E_RATIO.ReadOnly=true;
			m_dtcTi.ReadOnly=true;
			m_dtcMMV.ReadOnly=true;
			m_dtcPEAR.ReadOnly=true;
			m_dtcMEAN.ReadOnly=true;
			m_dtcPLATEAU.ReadOnly=true;
			m_dtcSign.ReadOnly=true;

			#endregion
		}


		/// <summary>
		///  ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
		/// </summary>
		protected override void m_mthClearRecordInfo()
		{			
			m_dtmPreRecordDate = DateTime.MinValue;
		}

			
		/// <summary>
		///  ��ȡ��ӵ�DataTable������
		/// </summary>
		/// <param name="p_objTransDataInfo"></param>
		/// <returns></returns>
		protected override object[][] m_objGetRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
		{
			clsICUBreath objDataInfo;
			object [][] objData;
			ArrayList objReturnData=new ArrayList();
			string strText,strXml;
			clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;

//			if(p_objTransDataInfo.m_intFlag != (int)enmRecordsType.ICUBreath)//ͳ����
//			{
//				return m_objGetPerDateSummaryRecordsValueArr(p_objTransDataInfo);
//			}
			
			if(p_objTransDataInfo==null || p_objTransDataInfo.m_intFlag !=(int)enmRecordsType.ICUBreath)
				return null;
			objDataInfo = (clsICUBreath)p_objTransDataInfo;

			#region ��ȡ��ӵ�DataTable������

			int intSingleTypeCount = objDataInfo.m_objTransDataArr.Length;			

			int intMaxCount = intSingleTypeCount;
			
			if(intMaxCount == 0)
				intMaxCount = 1;

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

			objData = new object[intMaxCount][]; 
			bool blnIsShowTime = false;

			for(int i=0;i<intMaxCount;i++)
			{
				objData[i] = new object[39];   
			
				clsICUBreathContent objCurrent = (i<intSingleTypeCount)?objDataInfo.m_objTransDataArr[i]:null;
				clsICUBreathContent objNext = (i >= intSingleTypeCount-1)?null:objDataInfo.m_objTransDataArr[i+1];

				//����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ��޸����봴����Ϊͬһ�ˣ�����ʾ
				if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strModifyUserID.Trim()==objCurrent.m_strCreateUserID.Trim())
				{
					if(i == 0)
						blnIsShowTime = true;
					TimeSpan tsModify =  objNext.m_dtmModifyDate-objCurrent.m_dtmModifyDate;
					if((int)tsModify.TotalHours < intCanModifyTime)
						continue;
				}
			
				if(i==0 || blnIsShowTime)
				{
					//ֻ�ڵ�һ�м�¼����������Ϣ
					objData[i][0] = objCurrent.m_dtmCreateDate;//��ż�¼ʱ����ַ���
					objData[i][1] = (int)enmRecordsType.ICUBreath;//��ż�¼���͵�intֵ
					objData[i][2] = objCurrent.m_dtmOpenDate;//��ż�¼��OpenDate�ַ���
					objData[i][3] = objDataInfo.m_objTransDataArr[objDataInfo.m_objTransDataArr.Length-1].m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   


					if(objCurrent.m_dtmCreateDate.Date.ToString() != m_dtmPreRecordDate.Date.ToString())
					{
						objData[i][4] = objCurrent.m_dtmCreateDate.Date.ToString("yyyy-MM-dd 00:00:00") ;//�����ַ���
					}
					if(objCurrent.m_dtmCreateDate.ToString("HH:mm") != m_dtmPreRecordDate.ToString("HH:mm"))
					{
						//objData[i][5] = objCurrent.m_dtmCreateDate.ToString("HH:mm:ss");//ʱ���ַ���
						objData[i][5] = objCurrent.m_dtmCreateDate.ToString("HH:mm");//ʱ���ַ���
					}
					m_dtmPreRecordDate = objCurrent.m_dtmCreateDate;
					blnIsShowTime = false;
				}
				
				
				
				if(i < intSingleTypeCount)
				{
					//��۲���Ŀ��ֵ
					strText = objCurrent.m_strMachineMode_Last;
					strXml = "<root />";
					//����͹۲���Ŀ�м���Ĳ�ͬ��m_strMachineMode���൱�ڹ۲���Ŀ��m_strMachineModeALL����m_strMachineModeAll�ﱣ�����޲��޸ĵ�ȫ����Ϣ����Ȼ��Զ��ͬ��
					if(objNext != null && objNext.m_strMachineMode_Last != objCurrent.m_strMachineMode_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strMachineMode_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][6] = objclsDSTRichTextBoxValue;//ͨ����ʽ
				
					strText = objCurrent.m_strBreathSoundLeft_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strBreathSoundLeft_Last != objCurrent.m_strBreathSoundLeft_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strBreathSoundLeft_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][7] = objclsDSTRichTextBoxValue;//����������
				
					strText = objCurrent.m_strBreathSoundRight_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strBreathSoundRight_Last != objCurrent.m_strBreathSoundRight_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strBreathSoundRight_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					
					objData[i][8] = objclsDSTRichTextBoxValue;//���������ң�
		

					strText = objCurrent.m_strInLength_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strInLength_Last != objCurrent.m_strInLength_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strInLength_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
					objData[i][9] = objclsDSTRichTextBoxValue;//InLength
				
					strText = objCurrent.m_strGasbagPress_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strGasbagPress_Last != objCurrent.m_strGasbagPress_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strGasbagPress_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][10] = objclsDSTRichTextBoxValue;//GasbagPress
				
					strText = objCurrent.m_strTIDAL_VOLUME_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strTIDAL_VOLUME_Last != objCurrent.m_strTIDAL_VOLUME_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strTIDAL_VOLUME_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][11] = objclsDSTRichTextBoxValue;//m_strTIDAL_VOLUME
				
					strText = objCurrent.m_strRATE_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strRATE_Last != objCurrent.m_strRATE_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strRATE_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][12] = objclsDSTRichTextBoxValue;//m_strRATE		
		

					strText = objCurrent.m_strPEAK_FLOW_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strPEAK_FLOW_Last != objCurrent.m_strPEAK_FLOW_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strPEAK_FLOW_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][13] = objclsDSTRichTextBoxValue;//m_strPEAK_FLOW
				
					strText = objCurrent.m_strO2_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strO2_Last != objCurrent.m_strO2_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strO2_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
					objData[i][14] = objclsDSTRichTextBoxValue;//m_strO2
				
					strText = objCurrent.m_strPS_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strPS_Last != objCurrent.m_strPS_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strPS_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
					objData[i][15] = objclsDSTRichTextBoxValue;//m_strPS
				
					strText = objCurrent.m_strASSIST_SENSITIVITY_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strASSIST_SENSITIVITY_Last != objCurrent.m_strASSIST_SENSITIVITY_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strASSIST_SENSITIVITY_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][16] = objclsDSTRichTextBoxValue;//m_strASSIST_SENSITIVITY
				
					strText = objCurrent.m_strINSPIRATORY_PAUSE_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strINSPIRATORY_PAUSE_Last != objCurrent.m_strINSPIRATORY_PAUSE_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strINSPIRATORY_PAUSE_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][17] = objclsDSTRichTextBoxValue;//m_strINSPIRATORY_PAUSE
				
				
					strText = objCurrent.m_strMMV_LEVEL_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strMMV_LEVEL_Last != objCurrent.m_strMMV_LEVEL_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strMMV_LEVEL_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][18] = objclsDSTRichTextBoxValue;//m_strMMV_LEVEL
				
					strText = objCurrent.m_strCOMPLIANCE_COMP_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strCOMPLIANCE_COMP_Last != objCurrent.m_strCOMPLIANCE_COMP_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strCOMPLIANCE_COMP_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][19] = objclsDSTRichTextBoxValue;//m_strCOMPLIANCE_COMP

					strText = objCurrent.m_strINSPIRATORY_TIME_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strINSPIRATORY_TIME_Last != objCurrent.m_strINSPIRATORY_TIME_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strINSPIRATORY_TIME_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
					objData[i][20] = objclsDSTRichTextBoxValue;//m_strINSPIRATORY_TIME
				
					strText = objCurrent.m_strINSPIRATORY_PRESSURE_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strINSPIRATORY_PRESSURE_Last != objCurrent.m_strINSPIRATORY_PRESSURE_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strINSPIRATORY_PRESSURE_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
					objData[i][21] = objclsDSTRichTextBoxValue;//m_strINSPIRATORY_PRESSURE
				
					strText = objCurrent.m_strBASE_FLOW_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strBASE_FLOW_Last != objCurrent.m_strBASE_FLOW_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strBASE_FLOW_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][22] = objclsDSTRichTextBoxValue;//m_strBASE_FLOW		

					strText = objCurrent.m_strFLOW_TRIGGER_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strFLOW_TRIGGER_Last != objCurrent.m_strFLOW_TRIGGER_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strFLOW_TRIGGER_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][23] = objclsDSTRichTextBoxValue;//m_strFLOW_TRIGGER
	
					strText = objCurrent.m_strPRESSURE_SLOPE_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strPRESSURE_SLOPE_Last != objCurrent.m_strPRESSURE_SLOPE_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strPRESSURE_SLOPE_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][24] = objclsDSTRichTextBoxValue;//m_strPRESSURE_SLOPE
	
					strText = objCurrent.m_strPEEP_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strPEEP_Last != objCurrent.m_strPEEP_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strPEEP_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][25] = objclsDSTRichTextBoxValue;//m_strPEEP
	
		
					strText = objCurrent.m_strTIDAL_VOL_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strTIDAL_VOL_Last != objCurrent.m_strTIDAL_VOL_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strTIDAL_VOL_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][26] = objclsDSTRichTextBoxValue;//m_strTIDAL_VOL    
	
					strText = objCurrent.m_strTOTAL_MV_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strTOTAL_MV_Last != objCurrent.m_strTOTAL_MV_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strTOTAL_MV_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][27] = objclsDSTRichTextBoxValue;//m_strTOTAL_MV    
	
					strText = objCurrent.m_strSPONT_MV_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strSPONT_MV_Last != objCurrent.m_strSPONT_MV_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strSPONT_MV_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][28] = objclsDSTRichTextBoxValue;//m_strSPONT_MV    
	
					strText = objCurrent.m_strTOTAL_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strTOTAL_Last != objCurrent.m_strTOTAL_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strTOTAL_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][29] = objclsDSTRichTextBoxValue;//m_strTOTAL    
	
					strText = objCurrent.m_strSPONT_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strSPONT_Last != objCurrent.m_strSPONT_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strSPONT_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][30] = objclsDSTRichTextBoxValue;//m_strSPONT    
	
					strText = objCurrent.m_strI_E_RATIO_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strI_E_RATIO_Last != objCurrent.m_strI_E_RATIO_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strI_E_RATIO_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][31] = objclsDSTRichTextBoxValue;//m_strI_E_RATIO    
	
					strText = objCurrent.m_strTi_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strTi_Last!= objCurrent.m_strTi_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strTi_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][32] = objclsDSTRichTextBoxValue;//m_strTi


					strText = objCurrent.m_strMMV_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strMMV_Last!= objCurrent.m_strMMV_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strMMV_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][33] = objclsDSTRichTextBoxValue;//m_strMMV

					strText = objCurrent.m_strPEAR_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strPEAR_Last!= objCurrent.m_strPEAR_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strPEAR_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][34] = objclsDSTRichTextBoxValue;//m_strPEAR

					strText = objCurrent.m_strMEAN_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strMEAN_Last!= objCurrent.m_strMEAN_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strMEAN_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][35] = objclsDSTRichTextBoxValue;//m_strMEAN

					strText = objCurrent.m_strPLATEAU_Last;
					strXml = "<root />";
					if(objNext != null && objNext.m_strPLATEAU_Last!= objCurrent.m_strPLATEAU_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strPLATEAU_Last,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[i][36] = objclsDSTRichTextBoxValue;//m_strPLATEAU	

                    objData[i][38] = objCurrent.m_strCreateUserName;
				}
				else
				{
					//����ֵ
				}
				if(i < intMaxCount-1)
					objReturnData.Add(objData[i]);		
			}
            objData[intMaxCount - 1][37] = objDataInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strModifyUserName;//ǩ��
			objReturnData.Add(objData[intMaxCount-1]);		
			
			object[][] m_objRe=new object[objReturnData.Count][];

			for(int m=0;m<objReturnData.Count ;m++)
				m_objRe[m]=(object[])objReturnData[m];
			return m_objRe;
			
			#endregion		
		}

		private string m_strGetDSTTextXML(string p_strText,string p_strModifyUserID,string p_strModifyUserName)
		{
			return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.s_strMakeDSTXml(p_strText,p_strModifyUserID,p_strModifyUserName,Color.Black,Color.White);
		}

		/*
		private object[][] m_objGetPerDateSummaryRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
		{			
			#region  ��ȡ��ӵ�DataTable��ͳ�����ݣ�����ͳ�ƣ�
			clsICUBreath objDataInfo;
			object [][] objData;
			string strText,strXml;
			clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;

			objDataInfo = (clsICUBreath)p_objTransDataInfo;
			//objData��������
			objData = new object[3][];
			//���һ���ǿ���
			objData[2] = new string[38];
			objData[0] = new object[38];   
			bool m_blnIfLastSummary = false;
			if(objDataInfo.m_objRecordContent.m_dtmCreateDate == DateTime.MaxValue)//�ж��Ƿ�������ͳ��
			{
				m_blnIfLastSummary = true;
			}

			if(m_blnIfLastSummary)//���������ͳ�ƽ�������ָı�
				strText = "�Ϲ�";
			else
				strText = "����";

			strXml = "<root />";

			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=strText;						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][18] = objclsDSTRichTextBoxValue;//�Թⷴ�����λ��

			strText = "����";
			strXml = "<root />";

			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=strText;						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][19] = objclsDSTRichTextBoxValue;//�Թⷴ���ҵ�λ��

			strText = "�ܼ�:";
			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=strText;						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][20] = objclsDSTRichTextBoxValue;//����Һ��ҩ����λ��
		
			strText = "";//objDataInfo.m_objICUSummary.m_intInDrugDosage_Total.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=strText;						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][21] = objclsDSTRichTextBoxValue;//����Һ��������λ��
		
			
			objData[1] = new object[38];

			if(m_blnIfLastSummary)
				strText = "�Ϲ�";
			else
				strText = "����";

			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=strText;						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[1][18] = objclsDSTRichTextBoxValue;//�Թⷴ�����λ��

			strText = "����";
			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=strText;						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[1][19] = objclsDSTRichTextBoxValue;//�Թⷴ���ҵ�λ��

			strText = "�ܼ�:";
			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=strText;						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[1][20] = objclsDSTRichTextBoxValue;//����Һ��ҩ����λ��

			strText = objDataInfo.m_objICUSummary.m_intTotal_In.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=strText;						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[1][21] = objclsDSTRichTextBoxValue;//����Һ��������λ��
			
			
			if(m_blnIfLastSummary==true)
			{
				object [][] objDataReturn=new object[2][];;
				objDataReturn[0]=objData[0];
				objDataReturn[1]=objData[1];
				return objDataReturn;
			}	
			
			return objData;			
		
			#endregion			
		}
		*/
		/// <summary>
		///  ��ȡ��¼����Ҫ��Ϣ�������ȡ����CreateDate,OpenDate,LastModifyDate��
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_objDataArr"></param>
		/// <returns></returns>
		protected override clsTrackRecordContent m_objGetRecordMainContent(int p_intRecordType,
			object[] p_objDataArr)
		{
			//���� p_intRecordType ��ȡ��Ӧ�� clsICUBreathContent
			clsICUBreathContent objContent = new clsICUBreathContent();

			int intSelectedRecordStartRow =m_dtgRecordDetail.CurrentCell.RowNumber;
			objContent.m_strCreateUserID = (m_dtbRecords.Rows[intSelectedRecordStartRow][38]).ToString();
		
			objContent.m_dtmCreateDate = DateTime.Parse((string)p_objDataArr[0]);
			objContent.m_dtmOpenDate = DateTime.Parse((string)p_objDataArr[2]);
			objContent.m_dtmModifyDate = DateTime.Parse((string)p_objDataArr[3]);
            objContent.m_strCreateUserID = (string)p_objDataArr[38];     
		
			return objContent;
		}

		/// <summary>
		///  ��ȡ���̼�¼�������ʵ��
		/// </summary>
		/// <returns></returns>
		protected override clsRecordsDomain m_objGetRecordsDomain()
		{
            return new clsRecordsDomain(enmRecordsType.ICUBreath);
		}
		

		private void mniAppend_Click(object sender, System.EventArgs e)
		{
//			enmPrivilegeSF enmSF = (enmPrivilegeSF)Enum.Parse(typeof(enmPrivilegeSF),this.GetType().Name);
//#if FunctionPrivilege
//			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,enmPrivilegeOperation.AddOrModify))
//			{
//				clsPublicFunction.s_mthShowNotPermitMessage();
//				return;
//			}			
//#endif
			m_mthAddNewRecord((int)enmDiseaseTrackType.ICUBreath);			
		}

		/// <summary>
		///  ��ȡ������Ӻ��޸ģ���¼�Ĵ���
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <returns></returns>
		protected override frmDiseaseTrackBase m_frmGetRecordForm(int p_intRecordType)
		{
			///����Σ���ػ���¼�����Ӧ�ı༭����
			return new frmSubICUBreath();
		}

		private int m_intColumn=6;
		private string m_strGetRightTextValue(int p_intDataTableRowCount)
		{			
			clsDSTRichTextBoxValue objDST=new clsDSTRichTextBoxValue();
			objDST=(clsDSTRichTextBoxValue)(m_dtbRecords.Rows[p_intDataTableRowCount-1][m_intColumn++]);
			return com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objDST.m_strText,objDST.m_strDSTXml);			
		}
		/// <summary>
		/// Σ���ػ���¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
		/// </summary>
		/// <param name="p_intRecordType"></param>
		protected override void m_mthAddNewRecord(int p_intRecordType)
		{
			//��ȡ��Ӽ�¼�Ĵ���
			frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(p_intRecordType);    
		
			//��ӿ���
			frmAddNewForm.m_mthSetDiseaseTrackInfoForAddNew(m_objCurrentPatient);

			clsICUBreathContent objContentLast=null;
			int intCount = m_dtbRecords.Rows.Count;		
			if(intCount>0)
			{					
				objContentLast = new clsICUBreathContent();

				m_intColumn=6;//��ʾ�ӵ�6�п�ʼΪҪ��ȡ����Ϣ
				objContentLast.m_strMachineMode_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strBreathSoundLeft_Last=m_strGetRightTextValue(intCount);					;
				objContentLast.m_strBreathSoundRight_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strInLength_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strGasbagPress_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strTIDAL_VOLUME_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strRATE_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strPEAK_FLOW_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strO2_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strPS_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strASSIST_SENSITIVITY_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strINSPIRATORY_PAUSE_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strMMV_LEVEL_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strCOMPLIANCE_COMP_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strINSPIRATORY_TIME_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strINSPIRATORY_PRESSURE_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strBASE_FLOW_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strFLOW_TRIGGER_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strPRESSURE_SLOPE_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strPEEP_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strTIDAL_VOL_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strTOTAL_MV_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strSPONT_MV_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strTOTAL_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strSPONT_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strI_E_RATIO_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strTi_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strMMV_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strPEAR_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strMEAN_Last=m_strGetRightTextValue(intCount);
				objContentLast.m_strPLATEAU_Last=m_strGetRightTextValue(intCount);
			
			}

			((frmSubICUBreath)frmAddNewForm).m_mthSetGUIFromContentOfLast(objContentLast);
		
			m_mthShowSubForm(frmAddNewForm,p_intRecordType,true);
//			//��ʾ����
//			if(frmAddNewForm.ShowDialog() == DialogResult.Yes)
//			{
//				m_mthSetPatientFormInfo(m_objCurrentPatient);
//			}		
		}

		protected override void m_mthHandleSubFormClosedWithYes(frmDiseaseTrackBase p_frmSubForm)
		{
            m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession,0);
		}


		/// <summary>
		/// Σ���ػ���¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
		/// </summary>
		protected override void m_mthModifyRecord(int p_intRecordType,
			DateTime p_dtmCreateRecordTime)
		{
//			enmPrivilegeSF enmSF = (enmPrivilegeSF)Enum.Parse(typeof(enmPrivilegeSF),this.GetType().Name);
//#if FunctionPrivilege
//			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,enmPrivilegeOperation.AddOrModify))
//			{
//				clsPublicFunction.s_mthShowNotPermitMessage();
//				return;
//			}			
//#endif
			//��ȡ��Ӽ�¼�Ĵ���
			frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(p_intRecordType); 
			frmAddNewForm.m_mthSetDiseaseTrackInfo(m_objCurrentPatient,p_dtmCreateRecordTime);
		
			m_mthShowSubForm(frmAddNewForm,p_intRecordType,true);
//			��ʾ����
//			if(frmAddNewForm.ShowDialog() == DialogResult.Yes)
//			{
//				m_mthSetPatientFormInfo(m_objCurrentPatient);
//			}			
		}

		/// <summary>
		/// Σ���ػ���¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_dtmCreateRecordTime"></param>
		protected override void m_mthRemoveDataFromDataTable(int p_intRecordType,
			DateTime p_dtmCreateRecordTime)
		{
            m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
		}

		private void frmMainICUBreath_Load(object sender, System.EventArgs e)
		{
			m_lblForTitle.Text="����ICU���������Ƽ໤��¼��";
			this.Text=m_lblForTitle.Text;
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

		protected override void m_mthGetDeletedRecord(int p_intRecordType,
			DateTime p_dtmCreateRecordTime)
		{	
			//��ȡ��Ӽ�¼�Ĵ���
			frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(p_intRecordType); 
			frmAddNewForm.m_mthSetDeletedDiseaseTrackInfo(m_objCurrentPatient,p_dtmCreateRecordTime);
		
			m_mthShowSubForm(frmAddNewForm,p_intRecordType,true);
//			//��ʾ����
//			if(frmAddNewForm.ShowDialog() == DialogResult.Yes)
//			{				
//				m_mthSetPatientFormInfo(m_objCurrentPatient);
//			}
		}


		protected override infPrintRecord m_objGetPrintTool()
		{			
			return new clsICUBreathPrintTool();
		}

		protected override void m_mthStartPrint()
		{
			//ȱʡʹ�ô�ӡԤ�����Ӵ��������ṩ�µ�ʵ��
			PageSetupDialog psd=new PageSetupDialog();
			try
			{
				if (m_pdcPrintDocument.DefaultPageSettings == null) 
				{
					m_pdcPrintDocument.DefaultPageSettings =  new PageSettings();
				}	
				m_pdcPrintDocument.DefaultPageSettings.Landscape=true;
				m_pdcPrintDocument.DefaultPageSettings.PaperSize=new PaperSize("A3",1150,1620);
			
				psd.PageSettings = m_pdcPrintDocument.DefaultPageSettings ;
				
				if(	psd.ShowDialog() !=DialogResult.Cancel)
				{
					m_pdcPrintDocument.DefaultPageSettings.Landscape=psd.PageSettings.Landscape;
					m_pdcPrintDocument.DefaultPageSettings.PaperSize=psd.PageSettings.PaperSize;
				}
				else 
				{
					return;
				}
			}
			catch(Exception ex)
			{
				if(ex.Message.IndexOf("No Printers installed")>=0)
					clsPublicFunction.ShowInformationMessageBox("�Ҳ�����ӡ����");
				else MessageBox.Show( ex.Message);
			}
			
			base.m_mthStartPrint();
			
		}

	}
	
	
}

