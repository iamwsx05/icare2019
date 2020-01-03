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
//using iCare.ICU.Espial;

namespace iCare
{
	/// <summary>
	/// Σ�ػ����¼�Ӵ����ʵ��,Jacky-2003-5-14
	/// </summary>
	public class frmIntensiveTend : frmDiseaseTrackBase
	{
		#region �����Զ���ؼ�
		private System.Windows.Forms.Label lblSignTitle;
		private System.Windows.Forms.Label lblRecordContentTitle;
		private System.Windows.Forms.Label m_lblSign;
		private com.digitalwave.controls.ctlRichTextBox m_txtRecordContent;
		private PinkieControls.ButtonXP m_cmdOK;
		private System.Windows.Forms.Label lblBreathTitle;
		private System.Windows.Forms.Label lblBloodPressureTitle;
		private System.Windows.Forms.Label lblTemperatureTitle;
		private System.Windows.Forms.Label lblPulseTitle;
		private System.Windows.Forms.GroupBox m_gpbPupil;
		private System.Windows.Forms.GroupBox m_gpbPupil_Echo;
		private com.digitalwave.controls.ctlRichTextBox m_txtEchoLeft;
		private System.Windows.Forms.Label lblLeft1;
		private com.digitalwave.controls.ctlRichTextBox m_txtEchoRight;
		private System.Windows.Forms.Label lblRight1;
		private System.Windows.Forms.GroupBox m_gpbPupil_Size;
		private com.digitalwave.controls.ctlRichTextBox m_txtPupilLeft;
		private System.Windows.Forms.Label lblPupilLeftTitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtPupilRight;
		private System.Windows.Forms.Label lblPupilRightTitle;
		private System.Windows.Forms.GroupBox m_gpbOut;
		private System.Windows.Forms.Label lblOutUTitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtOutS;
		private System.Windows.Forms.Label lblOutSTitle;
		private System.Windows.Forms.Label lblOutVTitle;
		private System.Windows.Forms.Label lblOutETitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtOutV;
		private com.digitalwave.controls.ctlRichTextBox m_txtOutE;
		private com.digitalwave.controls.ctlRichTextBox m_txtOutU;
		private System.Windows.Forms.GroupBox m_gpbInOutTotal;
		private System.Windows.Forms.Label lblTotalOutUTitle;
		private System.Windows.Forms.Label m_lblTotalOutU;
		private System.Windows.Forms.Label lblTotalOutSTitle;
		private System.Windows.Forms.Label m_lblTotalOutS;
		private System.Windows.Forms.Label m_lblTotalOutV;
		private System.Windows.Forms.Label lblTotalOutVTitle;
		private System.Windows.Forms.Label lblTotalOutETitle;
		private System.Windows.Forms.Label m_lblTotalOutE;
		private System.Windows.Forms.Label lblTotalInITitle;
		private System.Windows.Forms.Label m_lblTotalInI;
		private System.Windows.Forms.Label m_lblTotalInD;
		private System.Windows.Forms.Label lblTotalInDTitle;
		private System.Windows.Forms.Label m_lblTotalOut;
		private System.Windows.Forms.Label lblTotalOutTitle;
		private System.Windows.Forms.Label lblTotalInTitle;
		private System.Windows.Forms.Label m_lblTotalIn;
		private System.Windows.Forms.Label lblImportantTitle0;
		private System.Windows.Forms.Label lblImportantTitle1;
		private System.Windows.Forms.Label lblImportantTitle2;
		private System.Windows.Forms.Label lblImportantTitle3;
		private System.Windows.Forms.Label lblImportantTitle4;
		private System.Windows.Forms.Label lblImportantTitle5;
		private System.Windows.Forms.Label lblImportantTitle6;
		private System.Windows.Forms.Label lblImportantTitle7;
		private System.Windows.Forms.GroupBox m_gpbIn;
		private com.digitalwave.controls.ctlRichTextBox m_txtInD;
		private System.Windows.Forms.Label lblInDTitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtInI;
		private System.Windows.Forms.Label lblInITitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtBloodPressureS;
		private com.digitalwave.controls.ctlRichTextBox m_txtTemperature;
		private com.digitalwave.controls.ctlRichTextBox m_txtBloodPressureA;
		private System.Windows.Forms.Label lblBloodPressureTitle2;
		private com.digitalwave.controls.ctlRichTextBox m_txtPulse;
		private com.digitalwave.controls.ctlRichTextBox m_txtBreath;
		private PinkieControls.ButtonXP m_cmdCancel;
		private System.Windows.Forms.Label lblEmployeeSign;
		protected System.Windows.Forms.ListView m_lsvEmployee;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtSign;
		private System.ComponentModel.IContainer components = null;
		private string strClass;                                    //���
		private bool blnCreateDateEvent=false;                      //��ֹ��¼ʱ���¼�����
		#endregion �����Զ���ؼ�
		private PinkieControls.ButtonXP m_cmdGetGEData;


		private clsEmployeeSignTool m_objSignTool;

		/// <summary>
		/// ����������
		/// </summary>
		protected clsICUGESimulateGetData m_objICUGESimulateGetData;

		public frmIntensiveTend()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			m_lblSign.Text=MDIParent.OperatorName;
			m_mthSetRichTextBoxAttribInControl(this);

			m_objSignTool = new clsEmployeeSignTool(m_lsvEmployee);
			m_objSignTool.m_mthAddControl(m_txtSign);
			m_objICUGESimulateGetData=new clsICUGESimulateGetData(this);
		}
		
		public override int m_IntFormID
		{
			get
			{
				return 2;
			}
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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lblSignTitle = new System.Windows.Forms.Label();
            this.lblRecordContentTitle = new System.Windows.Forms.Label();
            this.m_lblSign = new System.Windows.Forms.Label();
            this.m_txtRecordContent = new com.digitalwave.controls.ctlRichTextBox();
            this.m_cmdOK = new PinkieControls.ButtonXP();
            this.lblBreathTitle = new System.Windows.Forms.Label();
            this.lblBloodPressureTitle = new System.Windows.Forms.Label();
            this.lblTemperatureTitle = new System.Windows.Forms.Label();
            this.lblPulseTitle = new System.Windows.Forms.Label();
            this.m_gpbPupil = new System.Windows.Forms.GroupBox();
            this.m_gpbPupil_Echo = new System.Windows.Forms.GroupBox();
            this.m_txtEchoLeft = new com.digitalwave.controls.ctlRichTextBox();
            this.lblLeft1 = new System.Windows.Forms.Label();
            this.m_txtEchoRight = new com.digitalwave.controls.ctlRichTextBox();
            this.lblRight1 = new System.Windows.Forms.Label();
            this.m_gpbPupil_Size = new System.Windows.Forms.GroupBox();
            this.m_txtPupilLeft = new com.digitalwave.controls.ctlRichTextBox();
            this.lblPupilLeftTitle = new System.Windows.Forms.Label();
            this.m_txtPupilRight = new com.digitalwave.controls.ctlRichTextBox();
            this.lblPupilRightTitle = new System.Windows.Forms.Label();
            this.m_gpbOut = new System.Windows.Forms.GroupBox();
            this.lblOutUTitle = new System.Windows.Forms.Label();
            this.m_txtOutS = new com.digitalwave.controls.ctlRichTextBox();
            this.lblOutSTitle = new System.Windows.Forms.Label();
            this.lblOutVTitle = new System.Windows.Forms.Label();
            this.lblOutETitle = new System.Windows.Forms.Label();
            this.m_txtOutV = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtOutE = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtOutU = new com.digitalwave.controls.ctlRichTextBox();
            this.m_gpbInOutTotal = new System.Windows.Forms.GroupBox();
            this.lblTotalOutUTitle = new System.Windows.Forms.Label();
            this.m_lblTotalOutU = new System.Windows.Forms.Label();
            this.lblTotalOutSTitle = new System.Windows.Forms.Label();
            this.m_lblTotalOutS = new System.Windows.Forms.Label();
            this.m_lblTotalOutV = new System.Windows.Forms.Label();
            this.lblTotalOutVTitle = new System.Windows.Forms.Label();
            this.lblTotalOutETitle = new System.Windows.Forms.Label();
            this.m_lblTotalOutE = new System.Windows.Forms.Label();
            this.lblTotalInITitle = new System.Windows.Forms.Label();
            this.m_lblTotalInI = new System.Windows.Forms.Label();
            this.m_lblTotalInD = new System.Windows.Forms.Label();
            this.lblTotalInDTitle = new System.Windows.Forms.Label();
            this.m_lblTotalOut = new System.Windows.Forms.Label();
            this.lblTotalOutTitle = new System.Windows.Forms.Label();
            this.lblTotalInTitle = new System.Windows.Forms.Label();
            this.m_lblTotalIn = new System.Windows.Forms.Label();
            this.lblImportantTitle0 = new System.Windows.Forms.Label();
            this.lblImportantTitle1 = new System.Windows.Forms.Label();
            this.lblImportantTitle2 = new System.Windows.Forms.Label();
            this.lblImportantTitle3 = new System.Windows.Forms.Label();
            this.lblImportantTitle4 = new System.Windows.Forms.Label();
            this.lblImportantTitle5 = new System.Windows.Forms.Label();
            this.lblImportantTitle6 = new System.Windows.Forms.Label();
            this.lblImportantTitle7 = new System.Windows.Forms.Label();
            this.m_gpbIn = new System.Windows.Forms.GroupBox();
            this.m_txtInD = new com.digitalwave.controls.ctlRichTextBox();
            this.lblInDTitle = new System.Windows.Forms.Label();
            this.m_txtInI = new com.digitalwave.controls.ctlRichTextBox();
            this.lblInITitle = new System.Windows.Forms.Label();
            this.m_txtBloodPressureS = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtTemperature = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtBloodPressureA = new com.digitalwave.controls.ctlRichTextBox();
            this.lblBloodPressureTitle2 = new System.Windows.Forms.Label();
            this.m_txtPulse = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtBreath = new com.digitalwave.controls.ctlRichTextBox();
            this.m_cmdCancel = new PinkieControls.ButtonXP();
            this.lblEmployeeSign = new System.Windows.Forms.Label();
            this.m_lsvEmployee = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.m_txtSign = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_cmdGetGEData = new PinkieControls.ButtonXP();
            this.m_gpbPupil.SuspendLayout();
            this.m_gpbPupil_Echo.SuspendLayout();
            this.m_gpbPupil_Size.SuspendLayout();
            this.m_gpbOut.SuspendLayout();
            this.m_gpbInOutTotal.SuspendLayout();
            this.m_gpbIn.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_trvCreateDate
            // 
            this.m_trvCreateDate.LineColor = System.Drawing.Color.Black;
            this.m_trvCreateDate.Location = new System.Drawing.Point(36, 108);
            this.m_trvCreateDate.Size = new System.Drawing.Size(212, 80);
            // 
            // lblCreateDateTitle
            // 
            this.lblCreateDateTitle.Location = new System.Drawing.Point(268, 148);
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.Location = new System.Drawing.Point(348, 144);
            // 
            // m_dtpGetDataTime
            // 
            this.m_dtpGetDataTime.Location = new System.Drawing.Point(348, 168);
            this.m_dtpGetDataTime.Visible = true;
            // 
            // m_lblGetDataTime
            // 
            this.m_lblGetDataTime.Location = new System.Drawing.Point(239, 172);
            this.m_lblGetDataTime.Visible = true;
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(320, 116);
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(444, 116);
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(516, 80);
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(268, 116);
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(392, 116);
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(36, 80);
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(588, 104);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(116, 68);
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(588, 80);
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(84, 76);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Font = new System.Drawing.Font("����", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblForTitle.Location = new System.Drawing.Point(188, 16);
            this.m_lblForTitle.Size = new System.Drawing.Size(456, 48);
            this.m_lblForTitle.Text = "Σ �� �� �� �� ¼";
            this.m_lblForTitle.Visible = true;
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(539, -29);
            // 
            // lblSignTitle
            // 
            this.lblSignTitle.AutoSize = true;
            this.lblSignTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblSignTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSignTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblSignTitle.Location = new System.Drawing.Point(228, 544);
            this.lblSignTitle.Name = "lblSignTitle";
            this.lblSignTitle.Size = new System.Drawing.Size(42, 14);
            this.lblSignTitle.TabIndex = 6083;
            this.lblSignTitle.Text = "ǩ��:";
            this.lblSignTitle.Visible = false;
            this.lblSignTitle.Click += new System.EventHandler(this.lblSignTitle_Click);
            // 
            // lblRecordContentTitle
            // 
            this.lblRecordContentTitle.AutoSize = true;
            this.lblRecordContentTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblRecordContentTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRecordContentTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRecordContentTitle.Location = new System.Drawing.Point(8, 388);
            this.lblRecordContentTitle.Name = "lblRecordContentTitle";
            this.lblRecordContentTitle.Size = new System.Drawing.Size(70, 14);
            this.lblRecordContentTitle.TabIndex = 6088;
            this.lblRecordContentTitle.Text = "��¼����:";
            // 
            // m_lblSign
            // 
            this.m_lblSign.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblSign.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_lblSign.Location = new System.Drawing.Point(284, 544);
            this.m_lblSign.Name = "m_lblSign";
            this.m_lblSign.Size = new System.Drawing.Size(100, 19);
            this.m_lblSign.TabIndex = 6082;
            this.m_lblSign.Visible = false;
            this.m_lblSign.Click += new System.EventHandler(this.m_lblSign_Click);
            // 
            // m_txtRecordContent
            // 
            this.m_txtRecordContent.AccessibleDescription = "��¼����";
            this.m_txtRecordContent.BackColor = System.Drawing.Color.White;
            this.m_txtRecordContent.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtRecordContent.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtRecordContent.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtRecordContent.Location = new System.Drawing.Point(8, 412);
            this.m_txtRecordContent.m_BlnIgnoreUserInfo = false;
            this.m_txtRecordContent.m_BlnPartControl = false;
            this.m_txtRecordContent.m_BlnReadOnly = false;
            this.m_txtRecordContent.m_BlnUnderLineDST = false;
            this.m_txtRecordContent.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtRecordContent.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtRecordContent.m_IntCanModifyTime = 6;
            this.m_txtRecordContent.m_IntPartControlLength = 0;
            this.m_txtRecordContent.m_IntPartControlStartIndex = 0;
            this.m_txtRecordContent.m_StrUserID = "";
            this.m_txtRecordContent.m_StrUserName = "";
            this.m_txtRecordContent.Name = "m_txtRecordContent";
            this.m_txtRecordContent.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtRecordContent.Size = new System.Drawing.Size(716, 120);
            this.m_txtRecordContent.TabIndex = 900;
            this.m_txtRecordContent.Text = "";
            // 
            // m_cmdOK
            // 
            this.m_cmdOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdOK.DefaultScheme = true;
            this.m_cmdOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdOK.Hint = "";
            this.m_cmdOK.Location = new System.Drawing.Point(580, 536);
            this.m_cmdOK.Name = "m_cmdOK";
            this.m_cmdOK.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdOK.Size = new System.Drawing.Size(64, 32);
            this.m_cmdOK.TabIndex = 6089;
            this.m_cmdOK.Text = "ȷ��";
            this.m_cmdOK.Click += new System.EventHandler(this.m_cmdOK_Click);
            // 
            // lblBreathTitle
            // 
            this.lblBreathTitle.AutoSize = true;
            this.lblBreathTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblBreathTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBreathTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblBreathTitle.Location = new System.Drawing.Point(8, 236);
            this.lblBreathTitle.Name = "lblBreathTitle";
            this.lblBreathTitle.Size = new System.Drawing.Size(91, 14);
            this.lblBreathTitle.TabIndex = 6099;
            this.lblBreathTitle.Text = "����(��/��):";
            // 
            // lblBloodPressureTitle
            // 
            this.lblBloodPressureTitle.AutoSize = true;
            this.lblBloodPressureTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblBloodPressureTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBloodPressureTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblBloodPressureTitle.Location = new System.Drawing.Point(12, 264);
            this.lblBloodPressureTitle.Name = "lblBloodPressureTitle";
            this.lblBloodPressureTitle.Size = new System.Drawing.Size(84, 14);
            this.lblBloodPressureTitle.TabIndex = 6100;
            this.lblBloodPressureTitle.Text = "Ѫѹ(mmHg):";
            // 
            // lblTemperatureTitle
            // 
            this.lblTemperatureTitle.AutoSize = true;
            this.lblTemperatureTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblTemperatureTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTemperatureTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblTemperatureTitle.Location = new System.Drawing.Point(28, 204);
            this.lblTemperatureTitle.Name = "lblTemperatureTitle";
            this.lblTemperatureTitle.Size = new System.Drawing.Size(70, 14);
            this.lblTemperatureTitle.TabIndex = 6103;
            this.lblTemperatureTitle.Text = "����(��):";
            // 
            // lblPulseTitle
            // 
            this.lblPulseTitle.AutoSize = true;
            this.lblPulseTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblPulseTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPulseTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPulseTitle.Location = new System.Drawing.Point(156, 204);
            this.lblPulseTitle.Name = "lblPulseTitle";
            this.lblPulseTitle.Size = new System.Drawing.Size(91, 14);
            this.lblPulseTitle.TabIndex = 6101;
            this.lblPulseTitle.Text = "����(��/��):";
            // 
            // m_gpbPupil
            // 
            this.m_gpbPupil.BackColor = System.Drawing.SystemColors.Control;
            this.m_gpbPupil.Controls.Add(this.m_gpbPupil_Echo);
            this.m_gpbPupil.Controls.Add(this.m_gpbPupil_Size);
            this.m_gpbPupil.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_gpbPupil.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_gpbPupil.Location = new System.Drawing.Point(296, 196);
            this.m_gpbPupil.Name = "m_gpbPupil";
            this.m_gpbPupil.Size = new System.Drawing.Size(428, 100);
            this.m_gpbPupil.TabIndex = 600;
            this.m_gpbPupil.TabStop = false;
            this.m_gpbPupil.Text = "ͫ��";
            // 
            // m_gpbPupil_Echo
            // 
            this.m_gpbPupil_Echo.BackColor = System.Drawing.SystemColors.Control;
            this.m_gpbPupil_Echo.Controls.Add(this.m_txtEchoLeft);
            this.m_gpbPupil_Echo.Controls.Add(this.lblLeft1);
            this.m_gpbPupil_Echo.Controls.Add(this.m_txtEchoRight);
            this.m_gpbPupil_Echo.Controls.Add(this.lblRight1);
            this.m_gpbPupil_Echo.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_gpbPupil_Echo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_gpbPupil_Echo.Location = new System.Drawing.Point(232, 16);
            this.m_gpbPupil_Echo.Name = "m_gpbPupil_Echo";
            this.m_gpbPupil_Echo.Size = new System.Drawing.Size(180, 80);
            this.m_gpbPupil_Echo.TabIndex = 313;
            this.m_gpbPupil_Echo.TabStop = false;
            this.m_gpbPupil_Echo.Text = "����";
            // 
            // m_txtEchoLeft
            // 
            this.m_txtEchoLeft.AccessibleDescription = "ͫ��>>����>>��";
            this.m_txtEchoLeft.BackColor = System.Drawing.Color.White;
            this.m_txtEchoLeft.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtEchoLeft.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtEchoLeft.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtEchoLeft.Location = new System.Drawing.Point(48, 24);
            this.m_txtEchoLeft.m_BlnIgnoreUserInfo = false;
            this.m_txtEchoLeft.m_BlnPartControl = false;
            this.m_txtEchoLeft.m_BlnReadOnly = false;
            this.m_txtEchoLeft.m_BlnUnderLineDST = false;
            this.m_txtEchoLeft.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtEchoLeft.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtEchoLeft.m_IntCanModifyTime = 6;
            this.m_txtEchoLeft.m_IntPartControlLength = 0;
            this.m_txtEchoLeft.m_IntPartControlStartIndex = 0;
            this.m_txtEchoLeft.m_StrUserID = "";
            this.m_txtEchoLeft.m_StrUserName = "";
            this.m_txtEchoLeft.MaxLength = 8000;
            this.m_txtEchoLeft.Multiline = false;
            this.m_txtEchoLeft.Name = "m_txtEchoLeft";
            this.m_txtEchoLeft.Size = new System.Drawing.Size(124, 22);
            this.m_txtEchoLeft.TabIndex = 2300;
            this.m_txtEchoLeft.Text = "";
            // 
            // lblLeft1
            // 
            this.lblLeft1.AutoSize = true;
            this.lblLeft1.BackColor = System.Drawing.SystemColors.Control;
            this.lblLeft1.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLeft1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblLeft1.Location = new System.Drawing.Point(8, 24);
            this.lblLeft1.Name = "lblLeft1";
            this.lblLeft1.Size = new System.Drawing.Size(28, 14);
            this.lblLeft1.TabIndex = 507;
            this.lblLeft1.Text = "��:";
            // 
            // m_txtEchoRight
            // 
            this.m_txtEchoRight.AccessibleDescription = "ͫ��>>����>>��";
            this.m_txtEchoRight.BackColor = System.Drawing.Color.White;
            this.m_txtEchoRight.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtEchoRight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtEchoRight.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtEchoRight.Location = new System.Drawing.Point(48, 52);
            this.m_txtEchoRight.m_BlnIgnoreUserInfo = false;
            this.m_txtEchoRight.m_BlnPartControl = false;
            this.m_txtEchoRight.m_BlnReadOnly = false;
            this.m_txtEchoRight.m_BlnUnderLineDST = false;
            this.m_txtEchoRight.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtEchoRight.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtEchoRight.m_IntCanModifyTime = 6;
            this.m_txtEchoRight.m_IntPartControlLength = 0;
            this.m_txtEchoRight.m_IntPartControlStartIndex = 0;
            this.m_txtEchoRight.m_StrUserID = "";
            this.m_txtEchoRight.m_StrUserName = "";
            this.m_txtEchoRight.MaxLength = 8000;
            this.m_txtEchoRight.Multiline = false;
            this.m_txtEchoRight.Name = "m_txtEchoRight";
            this.m_txtEchoRight.Size = new System.Drawing.Size(124, 22);
            this.m_txtEchoRight.TabIndex = 2400;
            this.m_txtEchoRight.Text = "";
            // 
            // lblRight1
            // 
            this.lblRight1.AutoSize = true;
            this.lblRight1.BackColor = System.Drawing.SystemColors.Control;
            this.lblRight1.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRight1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRight1.Location = new System.Drawing.Point(8, 52);
            this.lblRight1.Name = "lblRight1";
            this.lblRight1.Size = new System.Drawing.Size(28, 14);
            this.lblRight1.TabIndex = 507;
            this.lblRight1.Text = "��:";
            // 
            // m_gpbPupil_Size
            // 
            this.m_gpbPupil_Size.BackColor = System.Drawing.SystemColors.Control;
            this.m_gpbPupil_Size.Controls.Add(this.m_txtPupilLeft);
            this.m_gpbPupil_Size.Controls.Add(this.lblPupilLeftTitle);
            this.m_gpbPupil_Size.Controls.Add(this.m_txtPupilRight);
            this.m_gpbPupil_Size.Controls.Add(this.lblPupilRightTitle);
            this.m_gpbPupil_Size.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_gpbPupil_Size.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_gpbPupil_Size.Location = new System.Drawing.Point(16, 16);
            this.m_gpbPupil_Size.Name = "m_gpbPupil_Size";
            this.m_gpbPupil_Size.Size = new System.Drawing.Size(196, 80);
            this.m_gpbPupil_Size.TabIndex = 210;
            this.m_gpbPupil_Size.TabStop = false;
            this.m_gpbPupil_Size.Text = "��С(mm)";
            // 
            // m_txtPupilLeft
            // 
            this.m_txtPupilLeft.AccessibleDescription = "ͫ��>>��С>>��";
            this.m_txtPupilLeft.BackColor = System.Drawing.Color.White;
            this.m_txtPupilLeft.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtPupilLeft.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtPupilLeft.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtPupilLeft.Location = new System.Drawing.Point(48, 24);
            this.m_txtPupilLeft.m_BlnIgnoreUserInfo = false;
            this.m_txtPupilLeft.m_BlnPartControl = false;
            this.m_txtPupilLeft.m_BlnReadOnly = false;
            this.m_txtPupilLeft.m_BlnUnderLineDST = false;
            this.m_txtPupilLeft.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtPupilLeft.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtPupilLeft.m_IntCanModifyTime = 6;
            this.m_txtPupilLeft.m_IntPartControlLength = 0;
            this.m_txtPupilLeft.m_IntPartControlStartIndex = 0;
            this.m_txtPupilLeft.m_StrUserID = "";
            this.m_txtPupilLeft.m_StrUserName = "";
            this.m_txtPupilLeft.MaxLength = 8000;
            this.m_txtPupilLeft.Multiline = false;
            this.m_txtPupilLeft.Name = "m_txtPupilLeft";
            this.m_txtPupilLeft.Size = new System.Drawing.Size(140, 22);
            this.m_txtPupilLeft.TabIndex = 2100;
            this.m_txtPupilLeft.Text = "";
            // 
            // lblPupilLeftTitle
            // 
            this.lblPupilLeftTitle.AutoSize = true;
            this.lblPupilLeftTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblPupilLeftTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPupilLeftTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPupilLeftTitle.Location = new System.Drawing.Point(8, 24);
            this.lblPupilLeftTitle.Name = "lblPupilLeftTitle";
            this.lblPupilLeftTitle.Size = new System.Drawing.Size(28, 14);
            this.lblPupilLeftTitle.TabIndex = 507;
            this.lblPupilLeftTitle.Text = "��:";
            // 
            // m_txtPupilRight
            // 
            this.m_txtPupilRight.AccessibleDescription = "ͫ��>>��С>>��";
            this.m_txtPupilRight.BackColor = System.Drawing.Color.White;
            this.m_txtPupilRight.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtPupilRight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtPupilRight.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtPupilRight.Location = new System.Drawing.Point(48, 52);
            this.m_txtPupilRight.m_BlnIgnoreUserInfo = false;
            this.m_txtPupilRight.m_BlnPartControl = false;
            this.m_txtPupilRight.m_BlnReadOnly = false;
            this.m_txtPupilRight.m_BlnUnderLineDST = false;
            this.m_txtPupilRight.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtPupilRight.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtPupilRight.m_IntCanModifyTime = 6;
            this.m_txtPupilRight.m_IntPartControlLength = 0;
            this.m_txtPupilRight.m_IntPartControlStartIndex = 0;
            this.m_txtPupilRight.m_StrUserID = "";
            this.m_txtPupilRight.m_StrUserName = "";
            this.m_txtPupilRight.MaxLength = 8000;
            this.m_txtPupilRight.Multiline = false;
            this.m_txtPupilRight.Name = "m_txtPupilRight";
            this.m_txtPupilRight.Size = new System.Drawing.Size(140, 22);
            this.m_txtPupilRight.TabIndex = 2200;
            this.m_txtPupilRight.Text = "";
            // 
            // lblPupilRightTitle
            // 
            this.lblPupilRightTitle.AutoSize = true;
            this.lblPupilRightTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblPupilRightTitle.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPupilRightTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPupilRightTitle.Location = new System.Drawing.Point(8, 52);
            this.lblPupilRightTitle.Name = "lblPupilRightTitle";
            this.lblPupilRightTitle.Size = new System.Drawing.Size(32, 16);
            this.lblPupilRightTitle.TabIndex = 507;
            this.lblPupilRightTitle.Text = "��:";
            // 
            // m_gpbOut
            // 
            this.m_gpbOut.BackColor = System.Drawing.SystemColors.Control;
            this.m_gpbOut.Controls.Add(this.lblOutUTitle);
            this.m_gpbOut.Controls.Add(this.m_txtOutS);
            this.m_gpbOut.Controls.Add(this.lblOutSTitle);
            this.m_gpbOut.Controls.Add(this.lblOutVTitle);
            this.m_gpbOut.Controls.Add(this.lblOutETitle);
            this.m_gpbOut.Controls.Add(this.m_txtOutV);
            this.m_gpbOut.Controls.Add(this.m_txtOutE);
            this.m_gpbOut.Controls.Add(this.m_txtOutU);
            this.m_gpbOut.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_gpbOut.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_gpbOut.Location = new System.Drawing.Point(296, 300);
            this.m_gpbOut.Name = "m_gpbOut";
            this.m_gpbOut.Size = new System.Drawing.Size(428, 84);
            this.m_gpbOut.TabIndex = 800;
            this.m_gpbOut.TabStop = false;
            this.m_gpbOut.Text = "�ų���(ml)";
            // 
            // lblOutUTitle
            // 
            this.lblOutUTitle.AutoSize = true;
            this.lblOutUTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblOutUTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOutUTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblOutUTitle.Location = new System.Drawing.Point(12, 20);
            this.lblOutUTitle.Name = "lblOutUTitle";
            this.lblOutUTitle.Size = new System.Drawing.Size(28, 14);
            this.lblOutUTitle.TabIndex = 507;
            this.lblOutUTitle.Text = "��:";
            // 
            // m_txtOutS
            // 
            this.m_txtOutS.AccessibleDescription = "���";
            this.m_txtOutS.BackColor = System.Drawing.Color.White;
            this.m_txtOutS.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtOutS.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtOutS.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtOutS.Location = new System.Drawing.Point(64, 56);
            this.m_txtOutS.m_BlnIgnoreUserInfo = false;
            this.m_txtOutS.m_BlnPartControl = false;
            this.m_txtOutS.m_BlnReadOnly = false;
            this.m_txtOutS.m_BlnUnderLineDST = false;
            this.m_txtOutS.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtOutS.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtOutS.m_IntCanModifyTime = 6;
            this.m_txtOutS.m_IntPartControlLength = 0;
            this.m_txtOutS.m_IntPartControlStartIndex = 0;
            this.m_txtOutS.m_StrUserID = "";
            this.m_txtOutS.m_StrUserName = "";
            this.m_txtOutS.MaxLength = 8000;
            this.m_txtOutS.Multiline = false;
            this.m_txtOutS.Name = "m_txtOutS";
            this.m_txtOutS.Size = new System.Drawing.Size(136, 22);
            this.m_txtOutS.TabIndex = 2900;
            this.m_txtOutS.Text = "";
            // 
            // lblOutSTitle
            // 
            this.lblOutSTitle.AutoSize = true;
            this.lblOutSTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblOutSTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOutSTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblOutSTitle.Location = new System.Drawing.Point(12, 56);
            this.lblOutSTitle.Name = "lblOutSTitle";
            this.lblOutSTitle.Size = new System.Drawing.Size(42, 14);
            this.lblOutSTitle.TabIndex = 507;
            this.lblOutSTitle.Text = "���:";
            // 
            // lblOutVTitle
            // 
            this.lblOutVTitle.AutoSize = true;
            this.lblOutVTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblOutVTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOutVTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblOutVTitle.Location = new System.Drawing.Point(212, 20);
            this.lblOutVTitle.Name = "lblOutVTitle";
            this.lblOutVTitle.Size = new System.Drawing.Size(56, 14);
            this.lblOutVTitle.TabIndex = 507;
            this.lblOutVTitle.Text = "Ż����:";
            // 
            // lblOutETitle
            // 
            this.lblOutETitle.AutoSize = true;
            this.lblOutETitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblOutETitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOutETitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblOutETitle.Location = new System.Drawing.Point(212, 56);
            this.lblOutETitle.Name = "lblOutETitle";
            this.lblOutETitle.Size = new System.Drawing.Size(56, 14);
            this.lblOutETitle.TabIndex = 507;
            this.lblOutETitle.Text = "����Һ:";
            // 
            // m_txtOutV
            // 
            this.m_txtOutV.AccessibleDescription = "Ż����";
            this.m_txtOutV.BackColor = System.Drawing.Color.White;
            this.m_txtOutV.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtOutV.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtOutV.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtOutV.Location = new System.Drawing.Point(284, 20);
            this.m_txtOutV.m_BlnIgnoreUserInfo = false;
            this.m_txtOutV.m_BlnPartControl = false;
            this.m_txtOutV.m_BlnReadOnly = false;
            this.m_txtOutV.m_BlnUnderLineDST = false;
            this.m_txtOutV.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtOutV.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtOutV.m_IntCanModifyTime = 6;
            this.m_txtOutV.m_IntPartControlLength = 0;
            this.m_txtOutV.m_IntPartControlStartIndex = 0;
            this.m_txtOutV.m_StrUserID = "";
            this.m_txtOutV.m_StrUserName = "";
            this.m_txtOutV.MaxLength = 8000;
            this.m_txtOutV.Multiline = false;
            this.m_txtOutV.Name = "m_txtOutV";
            this.m_txtOutV.Size = new System.Drawing.Size(136, 22);
            this.m_txtOutV.TabIndex = 2800;
            this.m_txtOutV.Text = "";
            // 
            // m_txtOutE
            // 
            this.m_txtOutE.AccessibleDescription = "����Һ";
            this.m_txtOutE.BackColor = System.Drawing.Color.White;
            this.m_txtOutE.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtOutE.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtOutE.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtOutE.Location = new System.Drawing.Point(284, 56);
            this.m_txtOutE.m_BlnIgnoreUserInfo = false;
            this.m_txtOutE.m_BlnPartControl = false;
            this.m_txtOutE.m_BlnReadOnly = false;
            this.m_txtOutE.m_BlnUnderLineDST = false;
            this.m_txtOutE.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtOutE.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtOutE.m_IntCanModifyTime = 6;
            this.m_txtOutE.m_IntPartControlLength = 0;
            this.m_txtOutE.m_IntPartControlStartIndex = 0;
            this.m_txtOutE.m_StrUserID = "";
            this.m_txtOutE.m_StrUserName = "";
            this.m_txtOutE.MaxLength = 8000;
            this.m_txtOutE.Multiline = false;
            this.m_txtOutE.Name = "m_txtOutE";
            this.m_txtOutE.Size = new System.Drawing.Size(136, 22);
            this.m_txtOutE.TabIndex = 3000;
            this.m_txtOutE.Text = "";
            // 
            // m_txtOutU
            // 
            this.m_txtOutU.AccessibleDescription = "��";
            this.m_txtOutU.BackColor = System.Drawing.Color.White;
            this.m_txtOutU.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtOutU.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtOutU.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtOutU.Location = new System.Drawing.Point(64, 20);
            this.m_txtOutU.m_BlnIgnoreUserInfo = false;
            this.m_txtOutU.m_BlnPartControl = false;
            this.m_txtOutU.m_BlnReadOnly = false;
            this.m_txtOutU.m_BlnUnderLineDST = false;
            this.m_txtOutU.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtOutU.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtOutU.m_IntCanModifyTime = 6;
            this.m_txtOutU.m_IntPartControlLength = 0;
            this.m_txtOutU.m_IntPartControlStartIndex = 0;
            this.m_txtOutU.m_StrUserID = "";
            this.m_txtOutU.m_StrUserName = "";
            this.m_txtOutU.MaxLength = 8000;
            this.m_txtOutU.Multiline = false;
            this.m_txtOutU.Name = "m_txtOutU";
            this.m_txtOutU.Size = new System.Drawing.Size(136, 22);
            this.m_txtOutU.TabIndex = 2700;
            this.m_txtOutU.Text = "";
            // 
            // m_gpbInOutTotal
            // 
            this.m_gpbInOutTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.m_gpbInOutTotal.Controls.Add(this.lblTotalOutUTitle);
            this.m_gpbInOutTotal.Controls.Add(this.m_lblTotalOutU);
            this.m_gpbInOutTotal.Controls.Add(this.lblTotalOutSTitle);
            this.m_gpbInOutTotal.Controls.Add(this.m_lblTotalOutS);
            this.m_gpbInOutTotal.Controls.Add(this.m_lblTotalOutV);
            this.m_gpbInOutTotal.Controls.Add(this.lblTotalOutVTitle);
            this.m_gpbInOutTotal.Controls.Add(this.lblTotalOutETitle);
            this.m_gpbInOutTotal.Controls.Add(this.m_lblTotalOutE);
            this.m_gpbInOutTotal.Controls.Add(this.lblTotalInITitle);
            this.m_gpbInOutTotal.Controls.Add(this.m_lblTotalInI);
            this.m_gpbInOutTotal.Controls.Add(this.m_lblTotalInD);
            this.m_gpbInOutTotal.Controls.Add(this.lblTotalInDTitle);
            this.m_gpbInOutTotal.Controls.Add(this.m_lblTotalOut);
            this.m_gpbInOutTotal.Controls.Add(this.lblTotalOutTitle);
            this.m_gpbInOutTotal.Controls.Add(this.lblTotalInTitle);
            this.m_gpbInOutTotal.Controls.Add(this.m_lblTotalIn);
            this.m_gpbInOutTotal.Controls.Add(this.lblImportantTitle0);
            this.m_gpbInOutTotal.Controls.Add(this.lblImportantTitle1);
            this.m_gpbInOutTotal.Controls.Add(this.lblImportantTitle2);
            this.m_gpbInOutTotal.Controls.Add(this.lblImportantTitle3);
            this.m_gpbInOutTotal.Controls.Add(this.lblImportantTitle4);
            this.m_gpbInOutTotal.Controls.Add(this.lblImportantTitle5);
            this.m_gpbInOutTotal.Controls.Add(this.lblImportantTitle6);
            this.m_gpbInOutTotal.Controls.Add(this.lblImportantTitle7);
            this.m_gpbInOutTotal.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_gpbInOutTotal.ForeColor = System.Drawing.SystemColors.Window;
            this.m_gpbInOutTotal.Location = new System.Drawing.Point(8, 136);
            this.m_gpbInOutTotal.Name = "m_gpbInOutTotal";
            this.m_gpbInOutTotal.Size = new System.Drawing.Size(20, 20);
            this.m_gpbInOutTotal.TabIndex = 6098;
            this.m_gpbInOutTotal.TabStop = false;
            this.m_gpbInOutTotal.Text = "ͳ��(ml)------����������:U--�� S--��� V--Ż���� E--����Һ D--��ʳ I--��Һ";
            this.m_gpbInOutTotal.Visible = false;
            // 
            // lblTotalOutUTitle
            // 
            this.lblTotalOutUTitle.AutoSize = true;
            this.lblTotalOutUTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblTotalOutUTitle.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalOutUTitle.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTotalOutUTitle.Location = new System.Drawing.Point(8, 60);
            this.lblTotalOutUTitle.Name = "lblTotalOutUTitle";
            this.lblTotalOutUTitle.Size = new System.Drawing.Size(72, 16);
            this.lblTotalOutUTitle.TabIndex = 520;
            this.lblTotalOutUTitle.Text = "�ܳ���U:";
            // 
            // m_lblTotalOutU
            // 
            this.m_lblTotalOutU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.m_lblTotalOutU.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblTotalOutU.ForeColor = System.Drawing.SystemColors.Window;
            this.m_lblTotalOutU.Location = new System.Drawing.Point(84, 60);
            this.m_lblTotalOutU.Name = "m_lblTotalOutU";
            this.m_lblTotalOutU.Size = new System.Drawing.Size(62, 19);
            this.m_lblTotalOutU.TabIndex = 517;
            this.m_lblTotalOutU.Text = "0";
            // 
            // lblTotalOutSTitle
            // 
            this.lblTotalOutSTitle.AutoSize = true;
            this.lblTotalOutSTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblTotalOutSTitle.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalOutSTitle.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTotalOutSTitle.Location = new System.Drawing.Point(156, 60);
            this.lblTotalOutSTitle.Name = "lblTotalOutSTitle";
            this.lblTotalOutSTitle.Size = new System.Drawing.Size(72, 16);
            this.lblTotalOutSTitle.TabIndex = 518;
            this.lblTotalOutSTitle.Text = "�ܳ���S:";
            // 
            // m_lblTotalOutS
            // 
            this.m_lblTotalOutS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.m_lblTotalOutS.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblTotalOutS.ForeColor = System.Drawing.SystemColors.Window;
            this.m_lblTotalOutS.Location = new System.Drawing.Point(232, 60);
            this.m_lblTotalOutS.Name = "m_lblTotalOutS";
            this.m_lblTotalOutS.Size = new System.Drawing.Size(62, 19);
            this.m_lblTotalOutS.TabIndex = 523;
            this.m_lblTotalOutS.Text = "0";
            // 
            // m_lblTotalOutV
            // 
            this.m_lblTotalOutV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.m_lblTotalOutV.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblTotalOutV.ForeColor = System.Drawing.SystemColors.Window;
            this.m_lblTotalOutV.Location = new System.Drawing.Point(380, 60);
            this.m_lblTotalOutV.Name = "m_lblTotalOutV";
            this.m_lblTotalOutV.Size = new System.Drawing.Size(62, 19);
            this.m_lblTotalOutV.TabIndex = 524;
            this.m_lblTotalOutV.Text = "0";
            // 
            // lblTotalOutVTitle
            // 
            this.lblTotalOutVTitle.AutoSize = true;
            this.lblTotalOutVTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblTotalOutVTitle.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalOutVTitle.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTotalOutVTitle.Location = new System.Drawing.Point(304, 60);
            this.lblTotalOutVTitle.Name = "lblTotalOutVTitle";
            this.lblTotalOutVTitle.Size = new System.Drawing.Size(72, 16);
            this.lblTotalOutVTitle.TabIndex = 521;
            this.lblTotalOutVTitle.Text = "�ܳ���V:";
            // 
            // lblTotalOutETitle
            // 
            this.lblTotalOutETitle.AutoSize = true;
            this.lblTotalOutETitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblTotalOutETitle.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalOutETitle.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTotalOutETitle.Location = new System.Drawing.Point(452, 60);
            this.lblTotalOutETitle.Name = "lblTotalOutETitle";
            this.lblTotalOutETitle.Size = new System.Drawing.Size(72, 16);
            this.lblTotalOutETitle.TabIndex = 522;
            this.lblTotalOutETitle.Text = "�ܳ���E:";
            // 
            // m_lblTotalOutE
            // 
            this.m_lblTotalOutE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.m_lblTotalOutE.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblTotalOutE.ForeColor = System.Drawing.SystemColors.Window;
            this.m_lblTotalOutE.Location = new System.Drawing.Point(528, 60);
            this.m_lblTotalOutE.Name = "m_lblTotalOutE";
            this.m_lblTotalOutE.Size = new System.Drawing.Size(62, 19);
            this.m_lblTotalOutE.TabIndex = 516;
            this.m_lblTotalOutE.Text = "0";
            // 
            // lblTotalInITitle
            // 
            this.lblTotalInITitle.AutoSize = true;
            this.lblTotalInITitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblTotalInITitle.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalInITitle.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTotalInITitle.Location = new System.Drawing.Point(156, 24);
            this.lblTotalInITitle.Name = "lblTotalInITitle";
            this.lblTotalInITitle.Size = new System.Drawing.Size(72, 16);
            this.lblTotalInITitle.TabIndex = 510;
            this.lblTotalInITitle.Text = "������I:";
            // 
            // m_lblTotalInI
            // 
            this.m_lblTotalInI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.m_lblTotalInI.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblTotalInI.ForeColor = System.Drawing.SystemColors.Window;
            this.m_lblTotalInI.Location = new System.Drawing.Point(232, 24);
            this.m_lblTotalInI.Name = "m_lblTotalInI";
            this.m_lblTotalInI.Size = new System.Drawing.Size(62, 19);
            this.m_lblTotalInI.TabIndex = 511;
            this.m_lblTotalInI.Text = "0";
            // 
            // m_lblTotalInD
            // 
            this.m_lblTotalInD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.m_lblTotalInD.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblTotalInD.ForeColor = System.Drawing.SystemColors.Window;
            this.m_lblTotalInD.Location = new System.Drawing.Point(84, 24);
            this.m_lblTotalInD.Name = "m_lblTotalInD";
            this.m_lblTotalInD.Size = new System.Drawing.Size(62, 19);
            this.m_lblTotalInD.TabIndex = 508;
            this.m_lblTotalInD.Text = "0";
            // 
            // lblTotalInDTitle
            // 
            this.lblTotalInDTitle.AutoSize = true;
            this.lblTotalInDTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblTotalInDTitle.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalInDTitle.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTotalInDTitle.Location = new System.Drawing.Point(8, 24);
            this.lblTotalInDTitle.Name = "lblTotalInDTitle";
            this.lblTotalInDTitle.Size = new System.Drawing.Size(72, 16);
            this.lblTotalInDTitle.TabIndex = 509;
            this.lblTotalInDTitle.Text = "������D:";
            // 
            // m_lblTotalOut
            // 
            this.m_lblTotalOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.m_lblTotalOut.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblTotalOut.ForeColor = System.Drawing.SystemColors.Window;
            this.m_lblTotalOut.Location = new System.Drawing.Point(668, 60);
            this.m_lblTotalOut.Name = "m_lblTotalOut";
            this.m_lblTotalOut.Size = new System.Drawing.Size(72, 19);
            this.m_lblTotalOut.TabIndex = 514;
            this.m_lblTotalOut.Text = "0";
            // 
            // lblTotalOutTitle
            // 
            this.lblTotalOutTitle.AutoSize = true;
            this.lblTotalOutTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblTotalOutTitle.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalOutTitle.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTotalOutTitle.Location = new System.Drawing.Point(600, 60);
            this.lblTotalOutTitle.Name = "lblTotalOutTitle";
            this.lblTotalOutTitle.Size = new System.Drawing.Size(64, 16);
            this.lblTotalOutTitle.TabIndex = 515;
            this.lblTotalOutTitle.Text = "�ܳ���:";
            // 
            // lblTotalInTitle
            // 
            this.lblTotalInTitle.AutoSize = true;
            this.lblTotalInTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblTotalInTitle.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalInTitle.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTotalInTitle.Location = new System.Drawing.Point(304, 24);
            this.lblTotalInTitle.Name = "lblTotalInTitle";
            this.lblTotalInTitle.Size = new System.Drawing.Size(64, 16);
            this.lblTotalInTitle.TabIndex = 512;
            this.lblTotalInTitle.Text = "������:";
            // 
            // m_lblTotalIn
            // 
            this.m_lblTotalIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.m_lblTotalIn.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblTotalIn.ForeColor = System.Drawing.SystemColors.Window;
            this.m_lblTotalIn.Location = new System.Drawing.Point(372, 24);
            this.m_lblTotalIn.Name = "m_lblTotalIn";
            this.m_lblTotalIn.Size = new System.Drawing.Size(70, 19);
            this.m_lblTotalIn.TabIndex = 513;
            this.m_lblTotalIn.Text = "0";
            // 
            // lblImportantTitle0
            // 
            this.lblImportantTitle0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblImportantTitle0.Font = new System.Drawing.Font("����", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblImportantTitle0.ForeColor = System.Drawing.Color.Red;
            this.lblImportantTitle0.Location = new System.Drawing.Point(76, 36);
            this.lblImportantTitle0.Name = "lblImportantTitle0";
            this.lblImportantTitle0.Size = new System.Drawing.Size(72, 20);
            this.lblImportantTitle0.TabIndex = 525;
            this.lblImportantTitle0.Text = "=====";
            // 
            // lblImportantTitle1
            // 
            this.lblImportantTitle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblImportantTitle1.Font = new System.Drawing.Font("����", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblImportantTitle1.ForeColor = System.Drawing.Color.Red;
            this.lblImportantTitle1.Location = new System.Drawing.Point(224, 36);
            this.lblImportantTitle1.Name = "lblImportantTitle1";
            this.lblImportantTitle1.Size = new System.Drawing.Size(72, 20);
            this.lblImportantTitle1.TabIndex = 525;
            this.lblImportantTitle1.Text = "======";
            // 
            // lblImportantTitle2
            // 
            this.lblImportantTitle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblImportantTitle2.Font = new System.Drawing.Font("����", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblImportantTitle2.ForeColor = System.Drawing.Color.Red;
            this.lblImportantTitle2.Location = new System.Drawing.Point(368, 36);
            this.lblImportantTitle2.Name = "lblImportantTitle2";
            this.lblImportantTitle2.Size = new System.Drawing.Size(72, 20);
            this.lblImportantTitle2.TabIndex = 525;
            this.lblImportantTitle2.Text = "======";
            // 
            // lblImportantTitle3
            // 
            this.lblImportantTitle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblImportantTitle3.Font = new System.Drawing.Font("����", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblImportantTitle3.ForeColor = System.Drawing.Color.Red;
            this.lblImportantTitle3.Location = new System.Drawing.Point(80, 72);
            this.lblImportantTitle3.Name = "lblImportantTitle3";
            this.lblImportantTitle3.Size = new System.Drawing.Size(72, 20);
            this.lblImportantTitle3.TabIndex = 525;
            this.lblImportantTitle3.Text = "======";
            // 
            // lblImportantTitle4
            // 
            this.lblImportantTitle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblImportantTitle4.Font = new System.Drawing.Font("����", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblImportantTitle4.ForeColor = System.Drawing.Color.Red;
            this.lblImportantTitle4.Location = new System.Drawing.Point(224, 72);
            this.lblImportantTitle4.Name = "lblImportantTitle4";
            this.lblImportantTitle4.Size = new System.Drawing.Size(72, 20);
            this.lblImportantTitle4.TabIndex = 525;
            this.lblImportantTitle4.Text = "======";
            // 
            // lblImportantTitle5
            // 
            this.lblImportantTitle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblImportantTitle5.Font = new System.Drawing.Font("����", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblImportantTitle5.ForeColor = System.Drawing.Color.Red;
            this.lblImportantTitle5.Location = new System.Drawing.Point(368, 72);
            this.lblImportantTitle5.Name = "lblImportantTitle5";
            this.lblImportantTitle5.Size = new System.Drawing.Size(72, 20);
            this.lblImportantTitle5.TabIndex = 525;
            this.lblImportantTitle5.Text = "======";
            // 
            // lblImportantTitle6
            // 
            this.lblImportantTitle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblImportantTitle6.Font = new System.Drawing.Font("����", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblImportantTitle6.ForeColor = System.Drawing.Color.Red;
            this.lblImportantTitle6.Location = new System.Drawing.Point(524, 72);
            this.lblImportantTitle6.Name = "lblImportantTitle6";
            this.lblImportantTitle6.Size = new System.Drawing.Size(72, 20);
            this.lblImportantTitle6.TabIndex = 525;
            this.lblImportantTitle6.Text = "======";
            // 
            // lblImportantTitle7
            // 
            this.lblImportantTitle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblImportantTitle7.Font = new System.Drawing.Font("����", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblImportantTitle7.ForeColor = System.Drawing.Color.Red;
            this.lblImportantTitle7.Location = new System.Drawing.Point(664, 72);
            this.lblImportantTitle7.Name = "lblImportantTitle7";
            this.lblImportantTitle7.Size = new System.Drawing.Size(72, 20);
            this.lblImportantTitle7.TabIndex = 525;
            this.lblImportantTitle7.Text = "======";
            // 
            // m_gpbIn
            // 
            this.m_gpbIn.BackColor = System.Drawing.SystemColors.Control;
            this.m_gpbIn.Controls.Add(this.m_txtInD);
            this.m_gpbIn.Controls.Add(this.lblInDTitle);
            this.m_gpbIn.Controls.Add(this.m_txtInI);
            this.m_gpbIn.Controls.Add(this.lblInITitle);
            this.m_gpbIn.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_gpbIn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_gpbIn.Location = new System.Drawing.Point(8, 300);
            this.m_gpbIn.Name = "m_gpbIn";
            this.m_gpbIn.Size = new System.Drawing.Size(284, 84);
            this.m_gpbIn.TabIndex = 700;
            this.m_gpbIn.TabStop = false;
            this.m_gpbIn.Text = "������(ml)";
            // 
            // m_txtInD
            // 
            this.m_txtInD.AccessibleDescription = "��ʳ";
            this.m_txtInD.BackColor = System.Drawing.Color.White;
            this.m_txtInD.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtInD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtInD.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtInD.Location = new System.Drawing.Point(104, 20);
            this.m_txtInD.m_BlnIgnoreUserInfo = false;
            this.m_txtInD.m_BlnPartControl = false;
            this.m_txtInD.m_BlnReadOnly = false;
            this.m_txtInD.m_BlnUnderLineDST = false;
            this.m_txtInD.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtInD.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtInD.m_IntCanModifyTime = 6;
            this.m_txtInD.m_IntPartControlLength = 0;
            this.m_txtInD.m_IntPartControlStartIndex = 0;
            this.m_txtInD.m_StrUserID = "";
            this.m_txtInD.m_StrUserName = "";
            this.m_txtInD.MaxLength = 8000;
            this.m_txtInD.Multiline = false;
            this.m_txtInD.Name = "m_txtInD";
            this.m_txtInD.Size = new System.Drawing.Size(136, 22);
            this.m_txtInD.TabIndex = 2500;
            this.m_txtInD.Text = "";
            // 
            // lblInDTitle
            // 
            this.lblInDTitle.AutoSize = true;
            this.lblInDTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblInDTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInDTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInDTitle.Location = new System.Drawing.Point(52, 20);
            this.lblInDTitle.Name = "lblInDTitle";
            this.lblInDTitle.Size = new System.Drawing.Size(42, 14);
            this.lblInDTitle.TabIndex = 507;
            this.lblInDTitle.Text = "��ʳ:";
            // 
            // m_txtInI
            // 
            this.m_txtInI.AccessibleDescription = "��Һ";
            this.m_txtInI.BackColor = System.Drawing.Color.White;
            this.m_txtInI.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtInI.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtInI.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtInI.Location = new System.Drawing.Point(104, 56);
            this.m_txtInI.m_BlnIgnoreUserInfo = false;
            this.m_txtInI.m_BlnPartControl = false;
            this.m_txtInI.m_BlnReadOnly = false;
            this.m_txtInI.m_BlnUnderLineDST = false;
            this.m_txtInI.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtInI.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtInI.m_IntCanModifyTime = 6;
            this.m_txtInI.m_IntPartControlLength = 0;
            this.m_txtInI.m_IntPartControlStartIndex = 0;
            this.m_txtInI.m_StrUserID = "";
            this.m_txtInI.m_StrUserName = "";
            this.m_txtInI.MaxLength = 8000;
            this.m_txtInI.Multiline = false;
            this.m_txtInI.Name = "m_txtInI";
            this.m_txtInI.Size = new System.Drawing.Size(136, 22);
            this.m_txtInI.TabIndex = 2600;
            this.m_txtInI.Text = "";
            // 
            // lblInITitle
            // 
            this.lblInITitle.AutoSize = true;
            this.lblInITitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblInITitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInITitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInITitle.Location = new System.Drawing.Point(52, 56);
            this.lblInITitle.Name = "lblInITitle";
            this.lblInITitle.Size = new System.Drawing.Size(42, 14);
            this.lblInITitle.TabIndex = 507;
            this.lblInITitle.Text = "��Һ:";
            // 
            // m_txtBloodPressureS
            // 
            this.m_txtBloodPressureS.BackColor = System.Drawing.Color.White;
            this.m_txtBloodPressureS.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtBloodPressureS.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtBloodPressureS.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtBloodPressureS.Location = new System.Drawing.Point(100, 260);
            this.m_txtBloodPressureS.m_BlnIgnoreUserInfo = false;
            this.m_txtBloodPressureS.m_BlnPartControl = false;
            this.m_txtBloodPressureS.m_BlnReadOnly = false;
            this.m_txtBloodPressureS.m_BlnUnderLineDST = false;
            this.m_txtBloodPressureS.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtBloodPressureS.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtBloodPressureS.m_IntCanModifyTime = 6;
            this.m_txtBloodPressureS.m_IntPartControlLength = 0;
            this.m_txtBloodPressureS.m_IntPartControlStartIndex = 0;
            this.m_txtBloodPressureS.m_StrUserID = "";
            this.m_txtBloodPressureS.m_StrUserName = "";
            this.m_txtBloodPressureS.MaxLength = 8000;
            this.m_txtBloodPressureS.Multiline = false;
            this.m_txtBloodPressureS.Name = "m_txtBloodPressureS";
            this.m_txtBloodPressureS.Size = new System.Drawing.Size(76, 22);
            this.m_txtBloodPressureS.TabIndex = 400;
            this.m_txtBloodPressureS.Text = "";
            // 
            // m_txtTemperature
            // 
            this.m_txtTemperature.AccessibleDescription = "����";
            this.m_txtTemperature.BackColor = System.Drawing.Color.White;
            this.m_txtTemperature.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtTemperature.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtTemperature.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtTemperature.Location = new System.Drawing.Point(100, 204);
            this.m_txtTemperature.m_BlnIgnoreUserInfo = false;
            this.m_txtTemperature.m_BlnPartControl = false;
            this.m_txtTemperature.m_BlnReadOnly = false;
            this.m_txtTemperature.m_BlnUnderLineDST = false;
            this.m_txtTemperature.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtTemperature.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtTemperature.m_IntCanModifyTime = 6;
            this.m_txtTemperature.m_IntPartControlLength = 0;
            this.m_txtTemperature.m_IntPartControlStartIndex = 0;
            this.m_txtTemperature.m_StrUserID = "";
            this.m_txtTemperature.m_StrUserName = "";
            this.m_txtTemperature.MaxLength = 8000;
            this.m_txtTemperature.Multiline = false;
            this.m_txtTemperature.Name = "m_txtTemperature";
            this.m_txtTemperature.Size = new System.Drawing.Size(52, 22);
            this.m_txtTemperature.TabIndex = 100;
            this.m_txtTemperature.Text = "";
            // 
            // m_txtBloodPressureA
            // 
            this.m_txtBloodPressureA.BackColor = System.Drawing.Color.White;
            this.m_txtBloodPressureA.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtBloodPressureA.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtBloodPressureA.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtBloodPressureA.Location = new System.Drawing.Point(196, 260);
            this.m_txtBloodPressureA.m_BlnIgnoreUserInfo = false;
            this.m_txtBloodPressureA.m_BlnPartControl = false;
            this.m_txtBloodPressureA.m_BlnReadOnly = false;
            this.m_txtBloodPressureA.m_BlnUnderLineDST = false;
            this.m_txtBloodPressureA.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtBloodPressureA.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtBloodPressureA.m_IntCanModifyTime = 6;
            this.m_txtBloodPressureA.m_IntPartControlLength = 0;
            this.m_txtBloodPressureA.m_IntPartControlStartIndex = 0;
            this.m_txtBloodPressureA.m_StrUserID = "";
            this.m_txtBloodPressureA.m_StrUserName = "";
            this.m_txtBloodPressureA.MaxLength = 8000;
            this.m_txtBloodPressureA.Multiline = false;
            this.m_txtBloodPressureA.Name = "m_txtBloodPressureA";
            this.m_txtBloodPressureA.Size = new System.Drawing.Size(76, 22);
            this.m_txtBloodPressureA.TabIndex = 500;
            this.m_txtBloodPressureA.Text = "";
            // 
            // lblBloodPressureTitle2
            // 
            this.lblBloodPressureTitle2.BackColor = System.Drawing.SystemColors.Control;
            this.lblBloodPressureTitle2.Font = new System.Drawing.Font("����", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBloodPressureTitle2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblBloodPressureTitle2.Location = new System.Drawing.Point(176, 260);
            this.lblBloodPressureTitle2.Name = "lblBloodPressureTitle2";
            this.lblBloodPressureTitle2.Size = new System.Drawing.Size(20, 24);
            this.lblBloodPressureTitle2.TabIndex = 6102;
            this.lblBloodPressureTitle2.Text = "/";
            // 
            // m_txtPulse
            // 
            this.m_txtPulse.AccessibleDescription = "����";
            this.m_txtPulse.BackColor = System.Drawing.Color.White;
            this.m_txtPulse.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtPulse.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtPulse.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtPulse.Location = new System.Drawing.Point(248, 204);
            this.m_txtPulse.m_BlnIgnoreUserInfo = false;
            this.m_txtPulse.m_BlnPartControl = false;
            this.m_txtPulse.m_BlnReadOnly = false;
            this.m_txtPulse.m_BlnUnderLineDST = false;
            this.m_txtPulse.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtPulse.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtPulse.m_IntCanModifyTime = 6;
            this.m_txtPulse.m_IntPartControlLength = 0;
            this.m_txtPulse.m_IntPartControlStartIndex = 0;
            this.m_txtPulse.m_StrUserID = "";
            this.m_txtPulse.m_StrUserName = "";
            this.m_txtPulse.MaxLength = 8000;
            this.m_txtPulse.Multiline = false;
            this.m_txtPulse.Name = "m_txtPulse";
            this.m_txtPulse.Size = new System.Drawing.Size(44, 22);
            this.m_txtPulse.TabIndex = 200;
            this.m_txtPulse.Text = "";
            // 
            // m_txtBreath
            // 
            this.m_txtBreath.AccessibleDescription = "����";
            this.m_txtBreath.BackColor = System.Drawing.Color.White;
            this.m_txtBreath.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtBreath.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtBreath.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtBreath.Location = new System.Drawing.Point(100, 232);
            this.m_txtBreath.m_BlnIgnoreUserInfo = false;
            this.m_txtBreath.m_BlnPartControl = false;
            this.m_txtBreath.m_BlnReadOnly = false;
            this.m_txtBreath.m_BlnUnderLineDST = false;
            this.m_txtBreath.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtBreath.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtBreath.m_IntCanModifyTime = 6;
            this.m_txtBreath.m_IntPartControlLength = 0;
            this.m_txtBreath.m_IntPartControlStartIndex = 0;
            this.m_txtBreath.m_StrUserID = "";
            this.m_txtBreath.m_StrUserName = "";
            this.m_txtBreath.MaxLength = 8000;
            this.m_txtBreath.Multiline = false;
            this.m_txtBreath.Name = "m_txtBreath";
            this.m_txtBreath.Size = new System.Drawing.Size(52, 22);
            this.m_txtBreath.TabIndex = 300;
            this.m_txtBreath.Text = "";
            // 
            // m_cmdCancel
            // 
            this.m_cmdCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdCancel.DefaultScheme = true;
            this.m_cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdCancel.Hint = "";
            this.m_cmdCancel.Location = new System.Drawing.Point(660, 536);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCancel.Size = new System.Drawing.Size(64, 32);
            this.m_cmdCancel.TabIndex = 6089;
            this.m_cmdCancel.Text = "ȡ��";
            this.m_cmdCancel.Click += new System.EventHandler(this.m_cmdCancel_Click);
            // 
            // lblEmployeeSign
            // 
            this.lblEmployeeSign.AutoSize = true;
            this.lblEmployeeSign.BackColor = System.Drawing.SystemColors.Control;
            this.lblEmployeeSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEmployeeSign.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEmployeeSign.Location = new System.Drawing.Point(4, 544);
            this.lblEmployeeSign.Name = "lblEmployeeSign";
            this.lblEmployeeSign.Size = new System.Drawing.Size(42, 14);
            this.lblEmployeeSign.TabIndex = 10000021;
            this.lblEmployeeSign.Text = "ǩ��:";
            // 
            // m_lsvEmployee
            // 
            this.m_lsvEmployee.BackColor = System.Drawing.Color.White;
            this.m_lsvEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lsvEmployee.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7});
            this.m_lsvEmployee.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvEmployee.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_lsvEmployee.FullRowSelect = true;
            this.m_lsvEmployee.GridLines = true;
            this.m_lsvEmployee.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_lsvEmployee.Location = new System.Drawing.Point(68, 432);
            this.m_lsvEmployee.Name = "m_lsvEmployee";
            this.m_lsvEmployee.Size = new System.Drawing.Size(102, 105);
            this.m_lsvEmployee.TabIndex = 10000020;
            this.m_lsvEmployee.UseCompatibleStateImageBehavior = false;
            this.m_lsvEmployee.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Width = 0;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Width = 100;
            // 
            // m_txtSign
            // 
            this.m_txtSign.AccessibleName = "NoDefault";
            this.m_txtSign.BackColor = System.Drawing.Color.White;
            this.m_txtSign.BorderColor = System.Drawing.Color.White;
            this.m_txtSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtSign.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtSign.Location = new System.Drawing.Point(68, 540);
            this.m_txtSign.Name = "m_txtSign";
            this.m_txtSign.Size = new System.Drawing.Size(100, 22);
            this.m_txtSign.TabIndex = 1000;
            // 
            // m_cmdGetGEData
            // 
            this.m_cmdGetGEData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdGetGEData.DefaultScheme = true;
            this.m_cmdGetGEData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGetGEData.Hint = "";
            this.m_cmdGetGEData.Location = new System.Drawing.Point(568, 160);
            this.m_cmdGetGEData.Name = "m_cmdGetGEData";
            this.m_cmdGetGEData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetGEData.Size = new System.Drawing.Size(88, 32);
            this.m_cmdGetGEData.TabIndex = 10000022;
            this.m_cmdGetGEData.Text = "�໤�ǽ��";
            this.m_cmdGetGEData.Click += new System.EventHandler(this.m_cmdGetGEData_Click);
            // 
            // frmIntensiveTend
            // 
            this.AccessibleDescription = "Σ�ػ����¼";
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.CancelButton = this.m_cmdCancel;
            this.ClientSize = new System.Drawing.Size(734, 579);
            this.Controls.Add(this.m_cmdGetGEData);
            this.Controls.Add(this.lblEmployeeSign);
            this.Controls.Add(this.lblSignTitle);
            this.Controls.Add(this.m_lsvEmployee);
            this.Controls.Add(this.m_txtSign);
            this.Controls.Add(this.m_lblSign);
            this.Controls.Add(this.m_txtPulse);
            this.Controls.Add(this.lblBreathTitle);
            this.Controls.Add(this.lblBloodPressureTitle);
            this.Controls.Add(this.lblTemperatureTitle);
            this.Controls.Add(this.lblPulseTitle);
            this.Controls.Add(this.lblRecordContentTitle);
            this.Controls.Add(this.m_txtBloodPressureS);
            this.Controls.Add(this.m_txtTemperature);
            this.Controls.Add(this.m_txtBloodPressureA);
            this.Controls.Add(this.m_txtBreath);
            this.Controls.Add(this.m_txtRecordContent);
            this.Controls.Add(this.m_gpbPupil);
            this.Controls.Add(this.m_gpbOut);
            this.Controls.Add(this.m_gpbInOutTotal);
            this.Controls.Add(this.m_gpbIn);
            this.Controls.Add(this.lblBloodPressureTitle2);
            this.Controls.Add(this.m_cmdOK);
            this.Controls.Add(this.m_cmdCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmIntensiveTend";
            this.Text = "Σ�ػ����¼";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmIntensiveTend_Closing);
            this.Load += new System.EventHandler(this.frmIntensiveTend_Load);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_lblGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_dtpGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_cmdCancel, 0);
            this.Controls.SetChildIndex(this.m_cmdOK, 0);
            this.Controls.SetChildIndex(this.lblBloodPressureTitle2, 0);
            this.Controls.SetChildIndex(this.m_gpbIn, 0);
            this.Controls.SetChildIndex(this.m_gpbInOutTotal, 0);
            this.Controls.SetChildIndex(this.m_gpbOut, 0);
            this.Controls.SetChildIndex(this.m_gpbPupil, 0);
            this.Controls.SetChildIndex(this.m_txtRecordContent, 0);
            this.Controls.SetChildIndex(this.m_txtBreath, 0);
            this.Controls.SetChildIndex(this.m_txtBloodPressureA, 0);
            this.Controls.SetChildIndex(this.m_txtTemperature, 0);
            this.Controls.SetChildIndex(this.m_txtBloodPressureS, 0);
            this.Controls.SetChildIndex(this.m_dtpCreateDate, 0);
            this.Controls.SetChildIndex(this.m_trvCreateDate, 0);
            this.Controls.SetChildIndex(this.lblCreateDateTitle, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
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
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.lblRecordContentTitle, 0);
            this.Controls.SetChildIndex(this.lblPulseTitle, 0);
            this.Controls.SetChildIndex(this.lblTemperatureTitle, 0);
            this.Controls.SetChildIndex(this.lblBloodPressureTitle, 0);
            this.Controls.SetChildIndex(this.lblBreathTitle, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.m_txtPulse, 0);
            this.Controls.SetChildIndex(this.m_lblSign, 0);
            this.Controls.SetChildIndex(this.m_txtSign, 0);
            this.Controls.SetChildIndex(this.m_lsvEmployee, 0);
            this.Controls.SetChildIndex(this.lblSignTitle, 0);
            this.Controls.SetChildIndex(this.lblEmployeeSign, 0);
            this.Controls.SetChildIndex(this.m_cmdGetGEData, 0);
            this.m_gpbPupil.ResumeLayout(false);
            this.m_gpbPupil_Echo.ResumeLayout(false);
            this.m_gpbPupil_Echo.PerformLayout();
            this.m_gpbPupil_Size.ResumeLayout(false);
            this.m_gpbPupil_Size.PerformLayout();
            this.m_gpbOut.ResumeLayout(false);
            this.m_gpbOut.PerformLayout();
            this.m_gpbInOutTotal.ResumeLayout(false);
            this.m_gpbInOutTotal.PerformLayout();
            this.m_gpbIn.ResumeLayout(false);
            this.m_gpbIn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		

		public override iCare.clsDiseaseTrackInfo m_objGetDiseaseTrackInfo()
		{
			clsIntensiveRecordInfo objTrackInfo = new clsIntensiveRecordInfo();

			objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;//m_objGetContentFromGUI();
			objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
			objTrackInfo.m_StrTitle =this.m_lblForTitle.Text;

			//����m_dtmRecordTime
			if(objTrackInfo.m_ObjRecordContent !=null)
			{
				m_dtpCreateDate.Value=objTrackInfo.m_ObjRecordContent.m_dtmCreateDate;
			}
			return objTrackInfo;	
		}
		

		/// <summary>
		/// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
		/// </summary>
		protected override void m_mthClearRecordInfo()
		{
			//��վ����¼����			
			m_txtRecordContent.m_mthClearText();	

			this.m_txtTemperature.m_mthClearText();
			this.m_txtPulse.m_mthClearText();
			this.m_txtBreath.m_mthClearText();
			this.m_txtBloodPressureS.m_mthClearText();
			this.m_txtBloodPressureA.m_mthClearText();
			this.m_txtInD.m_mthClearText();
			this.m_txtInI.m_mthClearText();
			this.m_txtPupilLeft.m_mthClearText();
			this.m_txtPupilRight.m_mthClearText();
			this.m_txtEchoLeft.m_mthClearText();
			this.m_txtEchoRight.m_mthClearText();
			this.m_txtOutU.m_mthClearText();
			this.m_txtOutV.m_mthClearText();
			this.m_txtOutS.m_mthClearText();
			this.m_txtOutE.m_mthClearText();

			m_objSignTool.m_mthSetDefaulEmployee();
		}

		/// <summary>
		/// �����Ƿ����ѡ���˺ͼ�¼ʱ���б��ڴӲ��̼�¼�������ʱ��Ҫʹ�á�
		/// </summary>
		/// <param name="p_blnEnable"></param>
		protected override void m_mthEnablePatientSelectSub(bool p_blnEnable)
		{
			if(p_blnEnable==false)
			{
				foreach(Control control in this.Controls)
				{					
					if(control.Name!="m_dtpCreateDate")
						control.Top=control.Top-165;				
				}
			
				m_cmdOK.Visible=true;
				
				this.Size=new Size(this.Size.Width, this.Size.Height-165);
				this.CenterToParent();	

				lblCreateDateTitle.Left=m_txtRecordContent.Left;
				lblCreateDateTitle.Top=6;	
				m_dtpCreateDate.Left=lblCreateDateTitle.Right+5;
				m_dtpCreateDate.Top=lblCreateDateTitle.Top;	
				m_lblGetDataTime.Top=lblCreateDateTitle.Top;
				m_lblGetDataTime.Left=m_dtpCreateDate.Right+5;
				m_dtpGetDataTime.Top=m_lblGetDataTime.Top;
				m_dtpGetDataTime.Left=m_lblGetDataTime.Right+5;
				m_cmdGetGEData.Top=m_dtpGetDataTime.Top;
				m_cmdGetGEData.Left=m_dtpGetDataTime.Right+5;
			}	
	
			this.MaximizeBox=false;
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
		/// �������¼��ֵ��ʾ�������ϡ�
		/// </summary>
		/// <param name="p_objContent"></param>
		protected override void m_mthSetGUIFromContent(weCare.Core.Entity.clsTrackRecordContent p_objContent)
		{
			clsIntensiveTendRecordContent1 objContent=(clsIntensiveTendRecordContent1 )p_objContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��			
			this.m_mthClearRecordInfo();

			this.m_txtTemperature.m_mthSetNewText(objContent.m_strTemperatureAll,objContent.m_strTemperatureXML);
			this.m_txtPulse.m_mthSetNewText(objContent.m_strPulseAll,objContent.m_strPulseXML);
			this.m_txtBreath.m_mthSetNewText(objContent.m_strBreathAll,objContent.m_strBreathXML);
//			//��־
//			this.m_txtMind.m_mthSetNewText(objContent.m_strMindAll,objContent.m_strMindXML);
			this.m_txtBloodPressureA.m_mthSetNewText(objContent.m_strBloodPressureAAll,objContent.m_strBloodPressureAXML);
			this.m_txtBloodPressureS.m_mthSetNewText(objContent.m_strBloodPressureSAll,objContent.m_strBloodPressureSXML);
			this.m_txtInD.m_mthSetNewText(objContent.m_strInDAll,objContent.m_strInDXML);
			this.m_txtInI.m_mthSetNewText(objContent.m_strInIAll,objContent.m_strInIXML);
			this.m_txtPupilLeft.m_mthSetNewText(objContent.m_strPupilLeftAll,objContent.m_strPupilLeftXML);
			this.m_txtPupilRight.m_mthSetNewText(objContent.m_strPupilRightAll,objContent.m_strPupilRightXML);
			this.m_txtEchoLeft.m_mthSetNewText(objContent.m_strEchoLeftAll,objContent.m_strEchoLeftXML);
			this.m_txtEchoRight.m_mthSetNewText(objContent.m_strEchoRightAll,objContent.m_strEchoRightXML);
			
			this.m_txtOutU.m_mthSetNewText(objContent.m_strOutUAll ,objContent.m_strOutUXML);
			this.m_txtOutV.m_mthSetNewText(objContent.m_strOutVAll,objContent.m_strOutVXML);
			this.m_txtOutS.m_mthSetNewText(objContent.m_strOutSAll,objContent.m_strOutSXML);
			this.m_txtOutE.m_mthSetNewText(objContent.m_strOutEAll,objContent.m_strOutEXML);
			strClass=objContent.m_strClass;

           // m_txtRecordContent.m_mthSetNewText(objContent.m_strRecordContent, objContent.m_strRecordContentXml);
            strClass = objContent.m_strClass;
			clsEmployee objEmployee=new clsEmployee(objContent.m_strCreateUserID);
			if(objEmployee !=null)
				m_lblSign.Text=objEmployee.m_StrLastName;
		}

		protected override void m_mthSetDeletedGUIFromContent(weCare.Core.Entity.clsTrackRecordContent p_objContent)
		{
			clsIntensiveTendRecordContent1 objContent=(clsIntensiveTendRecordContent1 )p_objContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��			

			this.m_mthClearRecordInfo();
            
			//m_txtRecordContent.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strRecordContent,objContent.m_strRecordContentXml);			
			m_txtTemperature.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strTemperatureAll ,objContent.m_strTemperatureXML);

			this.m_txtPulse.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strPulseAll  ,objContent.m_strPulseXML );
			this.m_txtBreath.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strBreathAll  ,objContent.m_strBreathXML  );
//			//��־
//			this.m_txtMind.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strMindAll,objContent.m_strMindXML);
			this.m_txtBloodPressureA.Text =com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strBloodPressureAAll  ,objContent.m_strBloodPressureAXML );
			this.m_txtBloodPressureS.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strBloodPressureSAll ,objContent.m_strBloodPressureSXML );
			this.m_txtInD.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strInDAll ,objContent.m_strInDXML );
			this.m_txtInI.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strInIAll ,objContent.m_strInIXML );
			
			this.m_txtPupilLeft.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strPupilLeftAll ,objContent.m_strPupilLeftXML);
			this.m_txtPupilRight.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strPupilRightAll ,objContent.m_strPupilRightXML);

			this.m_txtEchoLeft.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strEchoLeftAll,objContent.m_strEchoLeftXML  );
			this.m_txtEchoRight.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strEchoRightAll,objContent.m_strEchoRightXML );
			
			this.m_txtOutU.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOutUAll ,objContent.m_strOutUXML);
			this.m_txtOutV.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOutVAll  ,objContent.m_strOutVXML);
			this.m_txtOutS.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOutSAll,objContent.m_strOutSXML);
			this.m_txtOutE.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOutEAll,objContent.m_strOutEXML);


			clsEmployee objEmployee=new clsEmployee(objContent.m_strCreateUserID);
			if(objEmployee !=null)
				m_lblSign.Text=objEmployee.m_StrLastName;
		}

		protected override weCare.Core.Entity.clsTrackRecordContent m_objGetContentFromGUI()
		{
			
				//�������У��
				if(m_objCurrentPatient==null || this.txtInPatientID.Text!=this.m_objCurrentPatient.m_StrHISInPatientID || txtInPatientID.Text=="")				
					return null;

				//�ӽ����ȡ��ֵ		
				clsIntensiveTendRecordContent1 objContent=new clsIntensiveTendRecordContent1 ();
			try
			{
				objContent.m_dtmCreateDate =m_dtpCreateDate.Value ;
				//objContent.m_strRecordContent_Right = m_txtRecordContent.m_strGetRightText () ;
                //objContent.m_strRecordContent =m_txtRecordContent.Text ;
                //objContent.m_strRecordContentXml =m_txtRecordContent.m_strGetXmlText ();

				objContent.m_strTemperature=this.m_txtTemperature.m_strGetRightText();
				objContent.m_strTemperatureAll=this.m_txtTemperature.Text;
				objContent.m_strTemperatureXML=this.m_txtTemperature.m_strGetXmlText();
		
				objContent.m_strPulse=this.m_txtPulse.m_strGetRightText();
				objContent.m_strPulseAll=this.m_txtPulse.Text;
				objContent.m_strPulseXML=this.m_txtPulse.m_strGetXmlText();

				objContent.m_strBreath=this.m_txtBreath.m_strGetRightText();
				objContent.m_strBreathAll=this.m_txtBreath.Text;
				objContent.m_strBreathXML=this.m_txtBreath.m_strGetXmlText();
//				//��־
//				objContent.m_strMind=this.m_txtMind.m_strGetRightText();
//				objContent.m_strMindAll=this.m_txtMind.Text;
//				objContent.m_strMindXML=this.m_txtMind.m_strGetXmlText();
            			
				objContent.m_strBloodPressureA=this.m_txtBloodPressureA.m_strGetRightText();
				objContent.m_strBloodPressureAAll=this.m_txtBloodPressureA.Text;
				objContent.m_strBloodPressureAXML=this.m_txtBloodPressureA.m_strGetXmlText();

				objContent.m_strBloodPressureS=this.m_txtBloodPressureS.m_strGetRightText();
				objContent.m_strBloodPressureSAll=this.m_txtBloodPressureS.Text ;
				objContent.m_strBloodPressureSXML=this.m_txtBloodPressureS.m_strGetXmlText();
			
				objContent.m_intInD=m_txtInD.m_strGetRightText()!="" ? int.Parse(m_txtInD.m_strGetRightText()) : 0;
				objContent.m_strInDAll=this.m_txtInD.Text;
				objContent.m_strInDXML=this.m_txtInD.m_strGetXmlText();

				objContent.m_intInI=m_txtInI.m_strGetRightText()!="" ? int.Parse(m_txtInI.m_strGetRightText()) : 0;
				objContent.m_strInIAll=this.m_txtInI.Text;
				objContent.m_strInIXML=this.m_txtInI.m_strGetXmlText();

				objContent.m_strPupilLeft=this.m_txtPupilLeft.m_strGetRightText();
				objContent.m_strPupilLeftAll=this.m_txtPupilLeft.Text;
				objContent.m_strPupilLeftXML=this.m_txtPupilLeft.m_strGetXmlText();

				objContent.m_strPupilRight=this.m_txtPupilRight.m_strGetRightText();
				objContent.m_strPupilRightAll=this.m_txtPupilRight.Text;
				objContent.m_strPupilRightXML=this.m_txtPupilRight.m_strGetXmlText();

				objContent.m_strEchoLeft=this.m_txtEchoLeft.m_strGetRightText();
				objContent.m_strEchoLeftAll=this.m_txtEchoLeft.Text;
				objContent.m_strEchoLeftXML=this.m_txtEchoLeft.m_strGetXmlText();
			
				objContent.m_strEchoRight=this.m_txtEchoRight.m_strGetRightText();
				objContent.m_strEchoRightAll=this.m_txtEchoRight.Text;
				objContent.m_strEchoRightXML=this.m_txtEchoRight.m_strGetXmlText();

				objContent.m_intOutU=m_txtOutU.m_strGetRightText()!="" ? int.Parse(m_txtOutU.m_strGetRightText()) : 0;
				objContent.m_strOutUAll=this.m_txtOutU.Text ;
				objContent.m_strOutUXML=this.m_txtOutU.m_strGetXmlText();
		
				objContent.m_intOutV=m_txtOutV.m_strGetRightText()!="" ? int.Parse(m_txtOutV.m_strGetRightText()) : 0;
				objContent.m_strOutVAll=this.m_txtOutV.Text;
				objContent.m_strOutVXML=this.m_txtOutV.m_strGetXmlText();

				objContent.m_intOutS=m_txtOutS.m_strGetRightText()!="" ? int.Parse(m_txtOutS.m_strGetRightText()) : 0;
				objContent.m_strOutSAll=this.m_txtOutS.Text;
				objContent.m_strOutSXML=this.m_txtOutS.m_strGetXmlText();
		
				objContent.m_intOutE=m_txtOutE.m_strGetRightText()!="" ? int.Parse(m_txtOutE.m_strGetRightText()) : 0;
				objContent.m_strOutEAll=this.m_txtOutE.Text;
				objContent.m_strOutEXML=this.m_txtOutE.m_strGetXmlText();

				//��ȡ���
				objContent.m_strClass=strClass;
			}
		
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			return(objContent );
			

		}

		protected override iCare.clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
		{
			//��ȡ�����¼�������ʵ�������Ӵ�������ʵ��
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.IntensiveTend);					
		}

		/// <summary>
		/// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
		{
			//��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
			clsIntensiveTendRecordContent1 objContent=(clsIntensiveTendRecordContent1)p_objRecordContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            //m_txtRecordContent.m_mthClearText();
            //m_txtRecordContent.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strRecordContent,objContent.m_strRecordContentXml);		
			
		}

		public override string m_strReloadFormTitle()
		{
			//���Ӵ�������ʵ��

			return	"Σ�ػ����¼";
		}

		private void frmIntensiveTend_Load(object sender, System.EventArgs e)
		{
//			m_cmdNewTemplate.Left=m_cmdOK.Left-m_cmdNewTemplate.Width+(m_cmdOK.Right-m_cmdCancel.Left);
//			m_cmdNewTemplate.Top=m_cmdOK.Top;
//			m_cmdNewTemplate.Visible=true;	
			this.m_dtpCreateDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
			this.m_dtpCreateDate.m_mthResetSize();

//			if (m_BlnIsAddNew==true)
//			{
//				//��ȡ��μ�¼���̼�¼
//				string strTempClass=GetClass();
//				strClass=strTempClass;
//				string strTempRecordConment,strTempRecordConmentXML;
//				com.digitalwave.IntensiveTendRecordService.clsIntensiveTendRecordService obj=new com.digitalwave.IntensiveTendRecordService.clsIntensiveTendRecordService();
//				
//				long lngRes=obj.m_lngGetRecordContentWithSame(strTempClass,MDIParent.s_ObjCurrentPatient.m_StrInPatientID,out strTempRecordConment,out strTempRecordConmentXML);
//				if (lngRes>0)
//				{
//					this.m_txtRecordContent.m_mthSetNewText(strTempRecordConment,strTempRecordConmentXML);			
//				}
//			}
			blnCreateDateEvent=true;
			m_txtTemperature.Focus();
		}

		#region ��ӡ
		/// <summary>
		///  ���ô�ӡ���ݡ�
		/// </summary>
		/// <param name="p_objContent"></param>
		protected override void m_mthSetPrintContent(clsTrackRecordContent p_objContent,DateTime p_dtmFirstPrintDate)
		{
			//ȱʡ�����κζ������Ӵ����������ṩ������
		}

		/// <summary>
		/// ��ʼ����ӡ����
		/// </summary>
		protected override void m_mthInitPrintTool()
		{
			//ȱʡ�����κζ������Ӵ����������ṩ����
			//��ʼ�����ݰ������д�ӡʹ�õ��ı��������塢���ʡ���ˢ����ӡ��ȡ�
		}

		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
		protected override void m_mthDisposePrintTools()
		{
			//ȱʡ�����κζ������Ӵ����������ṩ����
			//�ͷ����ݰ�����ӡʹ�õ������塢���ʡ���ˢ��ʹ��ϵͳ��Դ�ı�����
		}

		/// <summary>
		/// ��ʼ��ӡ��
		/// </summary>
		protected override void m_mthStartPrint()
		{
			//ȱʡʹ�ô�ӡԤ�����Ӵ��������ṩ�µ�ʵ��
			if(m_blnDirectPrint)
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

		/// <summary>
		/// ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		protected override void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
		{
			//ȱʡ�����κζ������Ӵ����������ṩ����
		}

		/// <summary>
		/// ��ӡҳ
		/// </summary>
		/// <param name="p_objPrintPageArg"></param>
		protected override void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
		{
		
		}

		/// <summary>
		/// ��ӡ����ʱ�Ĳ���
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		protected override void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
		{
			//���Ӵ����������ṩ����
		}
		#endregion ��ӡ

		private void m_cmdOK_Click(object sender, System.EventArgs e)
		{
			if(m_lngSave() > 0)
			{
				this.DialogResult = DialogResult.Yes;
				this.Close();
			}
		}

		private void m_cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.None;
			this.Close();
		}

		private void lblSignTitle_Click(object sender, System.EventArgs e)
		{
		
		}

		private void m_lblSign_Click(object sender, System.EventArgs e)
		{
		
		}

		#region ��ȡ�໤������
		protected override void GetData()
		{
			try
			{
				bool blnIsGE=m_blnCurrApparatus();

				clsCMSData objCMSData=null;
				clsVentilatorData objVentilatorData=null;

				//if(m_strInPatientID==null || m_strInPatientID=="" || m_strInPatientDate==null|| m_strInPatientDate=="")return;
				//��ȡ����������(��HEARTRATE�����ɣ���PULSERATE����������NPB���޴�Ѫѹ����NPBSYSTOLIC���޴�����ѹ����NPB_DIASTOLIC���޴�����ѹ����SPO21��Ѫ�����Ͷȣ���TEMP1�����£���RESPRATE������Ƶ�ʣ���O2CONCENTRATION������ENDEXPPRESSURE������EXPTIDALVOLUME������PEAKPRESSURE������BLOODNUM1��)
				string[] strTypeArry=new string[]{"PULSERATE","TEMP1","NPBSYSTOLIC","NPB_DIASTOLIC","RESPDETECTNUM"};//

				m_mthGetICUDataByTime(m_dtpGetDataTime.Value.ToString(),out objCMSData,out objVentilatorData,strTypeArry);

				if (!blnIsGE)
				{
					if (objCMSData != null)
					{
						//����
						if(objCMSData.m_strPulseRate == null || objCMSData.m_strPulseRate.Trim().Length == 0)
							m_txtPulse.Text = "";
						else
							m_txtPulse.Text = objCMSData.m_strPulseRate.Trim().Substring(0,objCMSData.m_strPulseRate.Trim().Length-3);
						
						//����
//						if(objCMSData.m_strHeartRate == null || objCMSData.m_strHeartRate.Trim().Length == 0)
//							m_txtHeartFrequency.Text = "";
//						else
//							m_txtHeartFrequency.Text = objCMSData.m_strHeartRate.Trim().Substring(0,objCMSData.m_strHeartRate.Trim().Length-3);

						//����
						if(objCMSData.m_strTemp1 == null || objCMSData.m_strTemp1.Trim().Length == 0)
							m_txtTemperature.Text="";
						else
							m_txtTemperature.Text=objCMSData.m_strTemp1.Trim();

						//����ѹ
						if(objCMSData.m_strNPBSYSTOLIC == null || objCMSData.m_strNPBSYSTOLIC.Trim().Length == 0)
							m_txtBloodPressureS.Text="";
						else
							m_txtBloodPressureS.Text=objCMSData.m_strNPBSYSTOLIC;

						//����ѹ
						if(objCMSData.m_strNPBDIASTOLIC == null || objCMSData.m_strNPBDIASTOLIC.Trim().Length == 0)
							m_txtBloodPressureA.Text="";
						else
							m_txtBloodPressureA.Text=objCMSData.m_strNPBDIASTOLIC;
						
						//����
						if(objCMSData.m_strRespDetectNum == null || objCMSData.m_strRespDetectNum.Trim().Length == 0)
							m_txtBreath.Text="";
						else
							m_txtBreath.Text=objCMSData.m_strRespDetectNum.Trim().Substring(0,objCMSData.m_strRespDetectNum.Trim().Length-3);

					}
				}
				else
				{
					clsGECMSData objGECMSData=null;
					objGECMSData=m_objICUGESimulateGetData.M_objNumericParam;
					if (objGECMSData==null)
						m_mthGetICUGEDataByTime(m_dtpGetDataTime.Value.ToString(),out objGECMSData);

					if (objGECMSData != null)
					{
						//����
						if(objGECMSData.m_strPluse == null || objGECMSData.m_strPluse.Trim().Length == 0)
							m_txtPulse.Text = "";
						else
							m_txtPulse.Text = objGECMSData.m_strPluse;
						
//						//����
//						if(objGECMSData.m_strHR  == null || objGECMSData.m_strHR.Trim().Length == 0)
//							m_txtHeartFrequency.Text = "";
//						else
//							m_txtHeartFrequency.Text = objGECMSData.m_strHR;

						//����
						if(objGECMSData.m_strTEMP1 == null || objGECMSData.m_strTEMP1.Trim().Length == 0)
							m_txtTemperature.Text="";
						else
							m_txtTemperature.Text=objGECMSData.m_strTEMP1;

						//����ѹ
						if(objGECMSData.m_strNBPSystolic == null || objGECMSData.m_strNBPSystolic.Trim().Length == 0)
							m_txtBloodPressureS.Text="";
						else
							m_txtBloodPressureS.Text=objGECMSData.m_strNBPSystolic;

						//����ѹ
						if(objGECMSData.m_strNBPDiastolic == null || objGECMSData.m_strNBPDiastolic.Trim().Length == 0)
							m_txtBloodPressureA.Text="";
						else
							m_txtBloodPressureA.Text=objGECMSData.m_strNBPDiastolic;
						
						//����
						if(objGECMSData.m_strRR == null || objGECMSData.m_strRR.Trim().Length == 0)
							m_txtBreath.Text="";
						else
							m_txtBreath.Text=objGECMSData.m_strRR;
					}
				}
			}
			catch
			{
			}
		}
		#endregion ��ȡ�໤������

		private void m_cmdGetGEData_Click(object sender, System.EventArgs e)
		{
			GetData();
		}

		private void frmIntensiveTend_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			m_objICUGESimulateGetData.m_mthStopReceiveData();
		}

		#region Jump Control
		protected override void m_mthInitJump(clsJumpControl p_objJump)
		{
			p_objJump=new clsJumpControl(this,
				new Control[]{m_txtTemperature,m_txtPulse,m_txtBreath,m_txtBloodPressureS,m_txtBloodPressureA,m_txtPupilLeft,m_txtPupilRight,m_txtEchoLeft
							 ,m_txtEchoRight,m_txtInD,m_txtInI,m_txtOutU,m_txtOutV,m_txtOutS,m_txtOutE,m_txtRecordContent,m_txtSign},Keys.Enter);
		}
		#endregion







	}
}

