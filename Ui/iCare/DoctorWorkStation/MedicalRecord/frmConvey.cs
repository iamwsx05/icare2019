using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using com.digitalwave.Emr.Signature_gui;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;
using System.Data;
using HRP;


namespace iCare
{
	/// <summary>
	/// ��ת�������̼�¼�Ӵ����ʵ��,Jacky-2003-5-19
	/// </summary>
	public class frmConvey : iCare.frmDiseaseTrackBase
	{
		private PinkieControls.ButtonXP cmdConfirm;
		private System.Windows.Forms.Label m_lblInHospitalDate;
		private System.Windows.Forms.Label lblInHospitalDateTitle;
		private System.Windows.Forms.Label lblConsultationTitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtConsultation;
		private System.Windows.Forms.Label lblCaseHistoryTitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtCaseHistory;
		private System.Windows.Forms.Label lblConveyDiagnoseTitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtConveyDiagnose;
		private System.Windows.Forms.Label lblOriginalDiagnoseTitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtOriginalDiagnose;
		private System.Windows.Forms.Label lblInHospitalReasonTitle;
		private System.Windows.Forms.Label m_lblInHospitalReason;
		private System.Windows.Forms.Label lblConveyReasonTitle;
		private System.Windows.Forms.Label lblNoticeTitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtNotice;
		private com.digitalwave.controls.ctlRichTextBox m_txtConveyReason;
		private PinkieControls.ButtonXP m_cmdClose;
		private System.ComponentModel.IContainer components = null;
		private clsCommonUseToolCollection m_objCUTC;

		private clsEmployeeSignTool m_objSignTool;
		protected System.Windows.Forms.ListView lsvSign;
		private System.Windows.Forms.ColumnHeader clmEmployeeName;
		private PinkieControls.ButtonXP m_cmdEmployeeSign;

		//����ǩ����
		private clsEmrSignToolCollection m_objSign;

		public frmConvey()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
            //ָ��ҽ������վ��
            intFormType = 1;
			// TODO: Add any initialization after the InitializeComponent call

//			m_objSignTool = new clsEmployeeSignTool(m_lsvEmployee_New);
//			m_objSignTool.m_mthAddControl(m_txtSign);

			cmdConfirm.Visible=false;
			
			m_mthSetRichTextBoxAttribInControl(this);

			this.Text="ת����¼";			
			this.m_lblForTitle.Text=this.Text;			
		
//			m_txtDoctorSign.LostFocus += new EventHandler(m_lsvDoctorList_LostFocus);
//			m_lsvDoctorList.LostFocus += new EventHandler(m_lsvDoctorList_LostFocus);
//			m_lsvDoctorList.Visible=false;
//
//			//ǩ������ֵ
//			m_objCUTC = new clsCommonUseToolCollection(this);
//			m_objCUTC.m_mthBindEmployeeSign(new Control[]{m_cmdMainDoctorSign },
//				new Control[]{this.m_txtDoctorSign },new int[]{1});
			//ǩ������ֵ
			m_objSign = new clsEmrSignToolCollection();
			//m_mthBindEmployeeSign(��ť,ǩ����,ҽ��1or��ʿ2,�����֤trueorfalse);
            m_objSign.m_mthBindEmployeeSign(m_cmdEmployeeSign, lsvSign, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);



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
            this.cmdConfirm = new PinkieControls.ButtonXP();
            this.m_lblInHospitalDate = new System.Windows.Forms.Label();
            this.lblInHospitalDateTitle = new System.Windows.Forms.Label();
            this.lblConsultationTitle = new System.Windows.Forms.Label();
            this.m_txtConsultation = new com.digitalwave.controls.ctlRichTextBox();
            this.lblCaseHistoryTitle = new System.Windows.Forms.Label();
            this.m_txtCaseHistory = new com.digitalwave.controls.ctlRichTextBox();
            this.lblConveyDiagnoseTitle = new System.Windows.Forms.Label();
            this.m_txtConveyDiagnose = new com.digitalwave.controls.ctlRichTextBox();
            this.lblOriginalDiagnoseTitle = new System.Windows.Forms.Label();
            this.m_txtOriginalDiagnose = new com.digitalwave.controls.ctlRichTextBox();
            this.lblInHospitalReasonTitle = new System.Windows.Forms.Label();
            this.m_lblInHospitalReason = new System.Windows.Forms.Label();
            this.lblConveyReasonTitle = new System.Windows.Forms.Label();
            this.lblNoticeTitle = new System.Windows.Forms.Label();
            this.m_txtNotice = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtConveyReason = new com.digitalwave.controls.ctlRichTextBox();
            this.m_cmdClose = new PinkieControls.ButtonXP();
            this.m_cmdEmployeeSign = new PinkieControls.ButtonXP();
            this.lsvSign = new System.Windows.Forms.ListView();
            this.clmEmployeeName = new System.Windows.Forms.ColumnHeader();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_trvCreateDate
            // 
            this.m_trvCreateDate.ForeColor = System.Drawing.Color.Black;
            this.m_trvCreateDate.LineColor = System.Drawing.Color.Black;
            this.m_trvCreateDate.Location = new System.Drawing.Point(9, -72);
            this.m_trvCreateDate.Size = new System.Drawing.Size(176, 72);
            this.m_trvCreateDate.Visible = false;
            // 
            // lblCreateDateTitle
            // 
            this.lblCreateDateTitle.Location = new System.Drawing.Point(9, 72);
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.Location = new System.Drawing.Point(81, 72);
            // 
            // m_dtpGetDataTime
            // 
            this.m_dtpGetDataTime.Location = new System.Drawing.Point(412, -21);
            // 
            // m_lblGetDataTime
            // 
            this.m_lblGetDataTime.Location = new System.Drawing.Point(308, -14);
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(689, -68);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(541, -21);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(385, -64);
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(377, -44);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(513, -64);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(645, -68);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(749, -68);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(189, -44);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(357, -105);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(76, 104);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(433, -44);
            this.txtInPatientID.Size = new System.Drawing.Size(76, 23);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(561, -68);
            this.m_txtPatientName.Size = new System.Drawing.Size(80, 23);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(433, -68);
            this.m_txtBedNO.Size = new System.Drawing.Size(52, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(233, -48);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(380, -105);
            this.m_lsvPatientName.Size = new System.Drawing.Size(80, 104);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(345, -105);
            this.m_lsvBedNO.Size = new System.Drawing.Size(68, 104);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(233, -72);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(189, -68);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(604, 624);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Location = new System.Drawing.Point(485, -68);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(229, -64);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(345, -60);
            this.m_lblForTitle.Text = "ת����¼";
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(251, -19);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(630, -30);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Location = new System.Drawing.Point(11, -69);
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            // 
            // cmdConfirm
            // 
            this.cmdConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdConfirm.DefaultScheme = true;
            this.cmdConfirm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdConfirm.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdConfirm.Hint = "";
            this.cmdConfirm.Location = new System.Drawing.Point(682, 554);
            this.cmdConfirm.Name = "cmdConfirm";
            this.cmdConfirm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdConfirm.Size = new System.Drawing.Size(80, 30);
            this.cmdConfirm.TabIndex = 170;
            this.cmdConfirm.Text = "ȷ��";
            this.cmdConfirm.Click += new System.EventHandler(this.cmdConfirm_Click);
            // 
            // m_lblInHospitalDate
            // 
            this.m_lblInHospitalDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblInHospitalDate.Location = new System.Drawing.Point(537, -20);
            this.m_lblInHospitalDate.Name = "m_lblInHospitalDate";
            this.m_lblInHospitalDate.Size = new System.Drawing.Size(212, 19);
            this.m_lblInHospitalDate.TabIndex = 6087;
            this.m_lblInHospitalDate.Visible = false;
            // 
            // lblInHospitalDateTitle
            // 
            this.lblInHospitalDateTitle.AutoSize = true;
            this.lblInHospitalDateTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInHospitalDateTitle.Location = new System.Drawing.Point(465, -20);
            this.lblInHospitalDateTitle.Name = "lblInHospitalDateTitle";
            this.lblInHospitalDateTitle.Size = new System.Drawing.Size(70, 14);
            this.lblInHospitalDateTitle.TabIndex = 6086;
            this.lblInHospitalDateTitle.Text = "��Ժʱ��:";
            this.lblInHospitalDateTitle.Visible = false;
            // 
            // lblConsultationTitle
            // 
            this.lblConsultationTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblConsultationTitle.AutoSize = true;
            this.lblConsultationTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblConsultationTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblConsultationTitle.ForeColor = System.Drawing.Color.Black;
            this.lblConsultationTitle.Location = new System.Drawing.Point(6, 325);
            this.lblConsultationTitle.Name = "lblConsultationTitle";
            this.lblConsultationTitle.Size = new System.Drawing.Size(70, 14);
            this.lblConsultationTitle.TabIndex = 6095;
            this.lblConsultationTitle.Text = "�������:";
            // 
            // m_txtConsultation
            // 
            this.m_txtConsultation.AccessibleDescription = "�������";
            this.m_txtConsultation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtConsultation.BackColor = System.Drawing.Color.White;
            this.m_txtConsultation.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtConsultation.ForeColor = System.Drawing.Color.Black;
            this.m_txtConsultation.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtConsultation.Location = new System.Drawing.Point(80, 322);
            this.m_txtConsultation.m_BlnIgnoreUserInfo = false;
            this.m_txtConsultation.m_BlnPartControl = false;
            this.m_txtConsultation.m_BlnReadOnly = false;
            this.m_txtConsultation.m_BlnUnderLineDST = false;
            this.m_txtConsultation.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtConsultation.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtConsultation.m_IntCanModifyTime = 6;
            this.m_txtConsultation.m_IntPartControlLength = 0;
            this.m_txtConsultation.m_IntPartControlStartIndex = 0;
            this.m_txtConsultation.m_StrUserID = "";
            this.m_txtConsultation.m_StrUserName = "";
            this.m_txtConsultation.Name = "m_txtConsultation";
            this.m_txtConsultation.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtConsultation.Size = new System.Drawing.Size(776, 84);
            this.m_txtConsultation.TabIndex = 130;
            this.m_txtConsultation.Text = "";
            // 
            // lblCaseHistoryTitle
            // 
            this.lblCaseHistoryTitle.AutoSize = true;
            this.lblCaseHistoryTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblCaseHistoryTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCaseHistoryTitle.ForeColor = System.Drawing.Color.Black;
            this.lblCaseHistoryTitle.Location = new System.Drawing.Point(8, 256);
            this.lblCaseHistoryTitle.Name = "lblCaseHistoryTitle";
            this.lblCaseHistoryTitle.Size = new System.Drawing.Size(70, 14);
            this.lblCaseHistoryTitle.TabIndex = 6093;
            this.lblCaseHistoryTitle.Text = "��ʷ���:";
            // 
            // m_txtCaseHistory
            // 
            this.m_txtCaseHistory.AccessibleDescription = "��ʷ���";
            this.m_txtCaseHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCaseHistory.BackColor = System.Drawing.Color.White;
            this.m_txtCaseHistory.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtCaseHistory.ForeColor = System.Drawing.Color.Black;
            this.m_txtCaseHistory.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtCaseHistory.Location = new System.Drawing.Point(80, 253);
            this.m_txtCaseHistory.m_BlnIgnoreUserInfo = false;
            this.m_txtCaseHistory.m_BlnPartControl = false;
            this.m_txtCaseHistory.m_BlnReadOnly = false;
            this.m_txtCaseHistory.m_BlnUnderLineDST = false;
            this.m_txtCaseHistory.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtCaseHistory.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtCaseHistory.m_IntCanModifyTime = 6;
            this.m_txtCaseHistory.m_IntPartControlLength = 0;
            this.m_txtCaseHistory.m_IntPartControlStartIndex = 0;
            this.m_txtCaseHistory.m_StrUserID = "";
            this.m_txtCaseHistory.m_StrUserName = "";
            this.m_txtCaseHistory.Name = "m_txtCaseHistory";
            this.m_txtCaseHistory.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtCaseHistory.Size = new System.Drawing.Size(776, 63);
            this.m_txtCaseHistory.TabIndex = 120;
            this.m_txtCaseHistory.Text = "";
            // 
            // lblConveyDiagnoseTitle
            // 
            this.lblConveyDiagnoseTitle.AutoSize = true;
            this.lblConveyDiagnoseTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblConveyDiagnoseTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblConveyDiagnoseTitle.ForeColor = System.Drawing.Color.Black;
            this.lblConveyDiagnoseTitle.Location = new System.Drawing.Point(6, 172);
            this.lblConveyDiagnoseTitle.Name = "lblConveyDiagnoseTitle";
            this.lblConveyDiagnoseTitle.Size = new System.Drawing.Size(70, 14);
            this.lblConveyDiagnoseTitle.TabIndex = 6091;
            this.lblConveyDiagnoseTitle.Text = "ת�����:";
            // 
            // m_txtConveyDiagnose
            // 
            this.m_txtConveyDiagnose.AccessibleDescription = "ת�����";
            this.m_txtConveyDiagnose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtConveyDiagnose.BackColor = System.Drawing.Color.White;
            this.m_txtConveyDiagnose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtConveyDiagnose.ForeColor = System.Drawing.Color.Black;
            this.m_txtConveyDiagnose.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtConveyDiagnose.Location = new System.Drawing.Point(80, 169);
            this.m_txtConveyDiagnose.m_BlnIgnoreUserInfo = false;
            this.m_txtConveyDiagnose.m_BlnPartControl = false;
            this.m_txtConveyDiagnose.m_BlnReadOnly = false;
            this.m_txtConveyDiagnose.m_BlnUnderLineDST = false;
            this.m_txtConveyDiagnose.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtConveyDiagnose.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtConveyDiagnose.m_IntCanModifyTime = 6;
            this.m_txtConveyDiagnose.m_IntPartControlLength = 0;
            this.m_txtConveyDiagnose.m_IntPartControlStartIndex = 0;
            this.m_txtConveyDiagnose.m_StrUserID = "";
            this.m_txtConveyDiagnose.m_StrUserName = "";
            this.m_txtConveyDiagnose.Name = "m_txtConveyDiagnose";
            this.m_txtConveyDiagnose.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtConveyDiagnose.Size = new System.Drawing.Size(776, 78);
            this.m_txtConveyDiagnose.TabIndex = 110;
            this.m_txtConveyDiagnose.Text = "";
            // 
            // lblOriginalDiagnoseTitle
            // 
            this.lblOriginalDiagnoseTitle.AutoSize = true;
            this.lblOriginalDiagnoseTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblOriginalDiagnoseTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOriginalDiagnoseTitle.ForeColor = System.Drawing.Color.Black;
            this.lblOriginalDiagnoseTitle.Location = new System.Drawing.Point(9, 100);
            this.lblOriginalDiagnoseTitle.Name = "lblOriginalDiagnoseTitle";
            this.lblOriginalDiagnoseTitle.Size = new System.Drawing.Size(70, 14);
            this.lblOriginalDiagnoseTitle.TabIndex = 6089;
            this.lblOriginalDiagnoseTitle.Text = "�������:";
            // 
            // m_txtOriginalDiagnose
            // 
            this.m_txtOriginalDiagnose.AccessibleDescription = "�������";
            this.m_txtOriginalDiagnose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtOriginalDiagnose.BackColor = System.Drawing.Color.White;
            this.m_txtOriginalDiagnose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtOriginalDiagnose.ForeColor = System.Drawing.Color.Black;
            this.m_txtOriginalDiagnose.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtOriginalDiagnose.Location = new System.Drawing.Point(81, 100);
            this.m_txtOriginalDiagnose.m_BlnIgnoreUserInfo = false;
            this.m_txtOriginalDiagnose.m_BlnPartControl = false;
            this.m_txtOriginalDiagnose.m_BlnReadOnly = false;
            this.m_txtOriginalDiagnose.m_BlnUnderLineDST = false;
            this.m_txtOriginalDiagnose.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtOriginalDiagnose.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtOriginalDiagnose.m_IntCanModifyTime = 6;
            this.m_txtOriginalDiagnose.m_IntPartControlLength = 0;
            this.m_txtOriginalDiagnose.m_IntPartControlStartIndex = 0;
            this.m_txtOriginalDiagnose.m_StrUserID = "";
            this.m_txtOriginalDiagnose.m_StrUserName = "";
            this.m_txtOriginalDiagnose.Name = "m_txtOriginalDiagnose";
            this.m_txtOriginalDiagnose.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtOriginalDiagnose.Size = new System.Drawing.Size(776, 63);
            this.m_txtOriginalDiagnose.TabIndex = 100;
            this.m_txtOriginalDiagnose.Text = "";
            // 
            // lblInHospitalReasonTitle
            // 
            this.lblInHospitalReasonTitle.AutoSize = true;
            this.lblInHospitalReasonTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInHospitalReasonTitle.Location = new System.Drawing.Point(9, 27);
            this.lblInHospitalReasonTitle.Name = "lblInHospitalReasonTitle";
            this.lblInHospitalReasonTitle.Size = new System.Drawing.Size(70, 14);
            this.lblInHospitalReasonTitle.TabIndex = 6098;
            this.lblInHospitalReasonTitle.Text = "��Ժԭ��:";
            // 
            // m_lblInHospitalReason
            // 
            this.m_lblInHospitalReason.AccessibleDescription = "��Ժԭ��";
            this.m_lblInHospitalReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblInHospitalReason.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblInHospitalReason.ForeColor = System.Drawing.Color.Black;
            this.m_lblInHospitalReason.Location = new System.Drawing.Point(82, 27);
            this.m_lblInHospitalReason.Name = "m_lblInHospitalReason";
            this.m_lblInHospitalReason.Size = new System.Drawing.Size(772, 42);
            this.m_lblInHospitalReason.TabIndex = 6099;
            // 
            // lblConveyReasonTitle
            // 
            this.lblConveyReasonTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblConveyReasonTitle.AutoSize = true;
            this.lblConveyReasonTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblConveyReasonTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblConveyReasonTitle.ForeColor = System.Drawing.Color.Black;
            this.lblConveyReasonTitle.Location = new System.Drawing.Point(9, 415);
            this.lblConveyReasonTitle.Name = "lblConveyReasonTitle";
            this.lblConveyReasonTitle.Size = new System.Drawing.Size(70, 14);
            this.lblConveyReasonTitle.TabIndex = 6093;
            this.lblConveyReasonTitle.Text = "ת��ԭ��:";
            // 
            // lblNoticeTitle
            // 
            this.lblNoticeTitle.AccessibleDescription = "���տ���ע��";
            this.lblNoticeTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNoticeTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblNoticeTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNoticeTitle.ForeColor = System.Drawing.Color.Black;
            this.lblNoticeTitle.Location = new System.Drawing.Point(4, 489);
            this.lblNoticeTitle.Name = "lblNoticeTitle";
            this.lblNoticeTitle.Size = new System.Drawing.Size(80, 56);
            this.lblNoticeTitle.TabIndex = 6095;
            this.lblNoticeTitle.Text = "������տ���ע������:";
            // 
            // m_txtNotice
            // 
            this.m_txtNotice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNotice.BackColor = System.Drawing.Color.White;
            this.m_txtNotice.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtNotice.ForeColor = System.Drawing.Color.Black;
            this.m_txtNotice.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtNotice.Location = new System.Drawing.Point(80, 489);
            this.m_txtNotice.m_BlnIgnoreUserInfo = false;
            this.m_txtNotice.m_BlnPartControl = false;
            this.m_txtNotice.m_BlnReadOnly = false;
            this.m_txtNotice.m_BlnUnderLineDST = false;
            this.m_txtNotice.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtNotice.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtNotice.m_IntCanModifyTime = 6;
            this.m_txtNotice.m_IntPartControlLength = 0;
            this.m_txtNotice.m_IntPartControlStartIndex = 0;
            this.m_txtNotice.m_StrUserID = "";
            this.m_txtNotice.m_StrUserName = "";
            this.m_txtNotice.Name = "m_txtNotice";
            this.m_txtNotice.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtNotice.Size = new System.Drawing.Size(776, 56);
            this.m_txtNotice.TabIndex = 150;
            this.m_txtNotice.Text = "";
            // 
            // m_txtConveyReason
            // 
            this.m_txtConveyReason.AccessibleDescription = "ת��ԭ��";
            this.m_txtConveyReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtConveyReason.BackColor = System.Drawing.Color.White;
            this.m_txtConveyReason.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtConveyReason.ForeColor = System.Drawing.Color.Black;
            this.m_txtConveyReason.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtConveyReason.Location = new System.Drawing.Point(80, 412);
            this.m_txtConveyReason.m_BlnIgnoreUserInfo = false;
            this.m_txtConveyReason.m_BlnPartControl = false;
            this.m_txtConveyReason.m_BlnReadOnly = false;
            this.m_txtConveyReason.m_BlnUnderLineDST = false;
            this.m_txtConveyReason.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtConveyReason.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtConveyReason.m_IntCanModifyTime = 6;
            this.m_txtConveyReason.m_IntPartControlLength = 0;
            this.m_txtConveyReason.m_IntPartControlStartIndex = 0;
            this.m_txtConveyReason.m_StrUserID = "";
            this.m_txtConveyReason.m_StrUserName = "";
            this.m_txtConveyReason.Name = "m_txtConveyReason";
            this.m_txtConveyReason.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtConveyReason.Size = new System.Drawing.Size(776, 73);
            this.m_txtConveyReason.TabIndex = 140;
            this.m_txtConveyReason.Text = "";
            // 
            // m_cmdClose
            // 
            this.m_cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdClose.DefaultScheme = true;
            this.m_cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdClose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdClose.Hint = "";
            this.m_cmdClose.Location = new System.Drawing.Point(770, 554);
            this.m_cmdClose.Name = "m_cmdClose";
            this.m_cmdClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdClose.Size = new System.Drawing.Size(80, 30);
            this.m_cmdClose.TabIndex = 171;
            this.m_cmdClose.Text = "ȡ��";
            this.m_cmdClose.Click += new System.EventHandler(this.m_cmdClose_Click);
            // 
            // m_cmdEmployeeSign
            // 
            this.m_cmdEmployeeSign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cmdEmployeeSign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdEmployeeSign.DefaultScheme = true;
            this.m_cmdEmployeeSign.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdEmployeeSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdEmployeeSign.Hint = "";
            this.m_cmdEmployeeSign.Location = new System.Drawing.Point(78, 554);
            this.m_cmdEmployeeSign.Name = "m_cmdEmployeeSign";
            this.m_cmdEmployeeSign.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdEmployeeSign.Size = new System.Drawing.Size(124, 28);
            this.m_cmdEmployeeSign.TabIndex = 10000034;
            this.m_cmdEmployeeSign.Tag = "1";
            this.m_cmdEmployeeSign.Text = "����ҽʦǩ��:";
            // 
            // lsvSign
            // 
            this.lsvSign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvSign.BackColor = System.Drawing.Color.White;
            this.lsvSign.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmEmployeeName});
            this.lsvSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lsvSign.ForeColor = System.Drawing.Color.Black;
            this.lsvSign.FullRowSelect = true;
            this.lsvSign.GridLines = true;
            this.lsvSign.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvSign.Location = new System.Drawing.Point(210, 554);
            this.lsvSign.Name = "lsvSign";
            this.lsvSign.Size = new System.Drawing.Size(456, 28);
            this.lsvSign.TabIndex = 10000035;
            this.lsvSign.UseCompatibleStateImageBehavior = false;
            this.lsvSign.View = System.Windows.Forms.View.SmallIcon;
            // 
            // clmEmployeeName
            // 
            this.clmEmployeeName.Width = 55;
            // 
            // frmConvey
            // 
            this.AccessibleDescription = "ת����¼";
            this.AutoScroll = false;
            this.CancelButton = this.m_cmdClose;
            this.ClientSize = new System.Drawing.Size(862, 596);
            this.Controls.Add(this.lsvSign);
            this.Controls.Add(this.lblInHospitalReasonTitle);
            this.Controls.Add(this.lblConsultationTitle);
            this.Controls.Add(this.lblCaseHistoryTitle);
            this.Controls.Add(this.lblConveyDiagnoseTitle);
            this.Controls.Add(this.lblOriginalDiagnoseTitle);
            this.Controls.Add(this.lblInHospitalDateTitle);
            this.Controls.Add(this.lblConveyReasonTitle);
            this.Controls.Add(this.m_cmdEmployeeSign);
            this.Controls.Add(this.m_cmdClose);
            this.Controls.Add(this.m_txtConsultation);
            this.Controls.Add(this.m_txtCaseHistory);
            this.Controls.Add(this.m_txtConveyDiagnose);
            this.Controls.Add(this.m_txtOriginalDiagnose);
            this.Controls.Add(this.m_lblInHospitalDate);
            this.Controls.Add(this.cmdConfirm);
            this.Controls.Add(this.m_lblInHospitalReason);
            this.Controls.Add(this.m_txtNotice);
            this.Controls.Add(this.m_txtConveyReason);
            this.Controls.Add(this.lblNoticeTitle);
            this.Name = "frmConvey";
            this.Text = "ת����¼";
            this.Load += new System.EventHandler(this.frmConvey_Load);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_lblGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_dtpGetDataTime, 0);
            this.Controls.SetChildIndex(this.lblNoticeTitle, 0);
            this.Controls.SetChildIndex(this.lblCreateDateTitle, 0);
            this.Controls.SetChildIndex(this.m_txtConveyReason, 0);
            this.Controls.SetChildIndex(this.m_txtNotice, 0);
            this.Controls.SetChildIndex(this.m_lblInHospitalReason, 0);
            this.Controls.SetChildIndex(this.cmdConfirm, 0);
            this.Controls.SetChildIndex(this.m_lblInHospitalDate, 0);
            this.Controls.SetChildIndex(this.m_txtOriginalDiagnose, 0);
            this.Controls.SetChildIndex(this.m_txtConveyDiagnose, 0);
            this.Controls.SetChildIndex(this.m_txtCaseHistory, 0);
            this.Controls.SetChildIndex(this.m_txtConsultation, 0);
            this.Controls.SetChildIndex(this.m_cmdClose, 0);
            this.Controls.SetChildIndex(this.m_cmdEmployeeSign, 0);
            this.Controls.SetChildIndex(this.lblConveyReasonTitle, 0);
            this.Controls.SetChildIndex(this.m_dtpCreateDate, 0);
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
            this.Controls.SetChildIndex(this.lblInHospitalDateTitle, 0);
            this.Controls.SetChildIndex(this.lblOriginalDiagnoseTitle, 0);
            this.Controls.SetChildIndex(this.lblConveyDiagnoseTitle, 0);
            this.Controls.SetChildIndex(this.lblCaseHistoryTitle, 0);
            this.Controls.SetChildIndex(this.lblConsultationTitle, 0);
            this.Controls.SetChildIndex(this.lblInHospitalReasonTitle, 0);
            this.Controls.SetChildIndex(this.m_trvCreateDate, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.lsvSign, 0);
            this.m_pnlNewBase.ResumeLayout(false);
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
            clsConveyInfo objTrackInfo = new clsConveyInfo(m_objCurrentPatient);

			objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;//m_objGetContentFromGUI();
			//����m_strTitle��m_dtmRecordTime
			objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
			objTrackInfo.m_StrTitle = "ת����¼";			
			
			return objTrackInfo;		
		}

		/// <summary>
		/// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
		/// </summary>
		protected override void m_mthClearRecordInfo()
		{
			//Ĭ��ǩ��
			MDIParent.m_mthSetDefaulEmployee(lsvSign);


			//��վ����¼����			
			m_txtOriginalDiagnose.m_mthClearText();
			m_txtCaseHistory.m_mthClearText();
			m_txtConveyDiagnose.m_mthClearText();
			m_txtConsultation.m_mthClearText();	
			m_txtConveyReason.m_mthClearText();	
			m_txtNotice.m_mthClearText();	
		}

		/// <summary>
		/// �����Ƿ����ѡ���˺ͼ�¼ʱ���б��ڴӲ��̼�¼�������ʱ��Ҫʹ�á�
		/// </summary>
		/// <param name="p_blnEnable"></param>
		protected override void m_mthEnablePatientSelectSub(bool p_blnEnable)
		{
			if(p_blnEnable==false)
			{
                //foreach(Control control in this.Controls)
                //{					
                //    control.Top=control.Top-105;				
                //}
			
				cmdConfirm.Visible=true;
				
                //this.Size=new Size(this.Size.Width, this.Size.Height-105);
				this.CenterToParent();				
			}
            //this.MaximizeBox=false;
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
			int intSignCount=lsvSign.Items.Count;
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null || intSignCount == 0)				
				return null;
			//�ӽ����ȡ��ֵ
			clsConveyRecordContent objContent=new clsConveyRecordContent();

			//��ȡlsvsignǩ��
			objContent.objSignerArr=new clsEmrSigns_VO[intSignCount];
			strUserIDList="";
			strUserNameList="";
            m_mthGetSignArr(new Control[] { lsvSign}, ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);
            //for (int i = 0; i < intSignCount; i++)
            //{
            //    objContent.objSignerArr[i]=new clsEmrSigns_VO();
            //    objContent.objSignerArr[i].objEmployee=new clsEmrEmployeeBase_VO();
            //    objContent.objSignerArr[i].objEmployee=(clsEmrEmployeeBase_VO)( lsvSign.Items[i].Tag);
            //    objContent.objSignerArr[i].controlName="lsvSign";
            //    objContent.objSignerArr[i].m_strFORMID_VCHR="frmConvey";//ע���Сд
            //    objContent.objSignerArr[i].m_strREGISTERID_CHR=com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            //    //�ۼ���ʽ 0972,0324,
            //    strUserIDList=strUserIDList+objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR.Trim()+",";
            //    strUserNameList=strUserNameList+objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR.Trim()+",";
            //}
			objContent.m_strModifyUserID=strUserIDList;


			//����Richtextbox��modifyuserID ��modifyuserName
			m_mthSetRichTextBoxAttribInControlWithIDandName(this);
			#region �Ƿ�����޺ۼ��޸�
			if (chkModifyWithoutMatk.Checked)
				objContent.m_intMarkStatus=0;
			else
				objContent.m_intMarkStatus=1;
			#endregion

			objContent.m_dtmCreateDate=m_dtpCreateDate.Value;					
				
			objContent.m_strOriginalDiagnose_Right=m_txtOriginalDiagnose.m_strGetRightText();	
			objContent.m_strOriginalDiagnose=m_txtOriginalDiagnose.Text;
			objContent.m_strOriginalDiagnoseXML=m_txtOriginalDiagnose.m_strGetXmlText();					
			
			objContent.m_strConveyDiagnose_Right=m_txtConveyDiagnose.m_strGetRightText();	
			objContent.m_strConveyDiagnose=m_txtConveyDiagnose.Text;
			objContent.m_strConveyDiagnoseXML=m_txtConveyDiagnose.m_strGetXmlText();					
			
			objContent.m_strCaseHistory_Right=m_txtCaseHistory.m_strGetRightText();	
			objContent.m_strCaseHistory=m_txtCaseHistory.Text;
			objContent.m_strCaseHistoryXML=m_txtCaseHistory.m_strGetXmlText();					
			
			objContent.m_strConsultation_Right=m_txtConsultation.m_strGetRightText();	
			objContent.m_strConsultation=m_txtConsultation.Text;
			objContent.m_strConsultationXML=m_txtConsultation.m_strGetXmlText();			

			objContent.m_strConveyReason_Right=m_txtConveyReason.m_strGetRightText();	
			objContent.m_strConveyReason=m_txtConveyReason.Text;
			objContent.m_strConveyReasonXML=m_txtConveyReason.m_strGetXmlText();

			objContent.m_strNotice_Right=m_txtNotice.m_strGetRightText();	
			objContent.m_strNotice=m_txtNotice.Text;
			objContent.m_strNoticeXML=m_txtNotice.m_strGetXmlText();
			

			return objContent;	
		}

		/// <summary>
		/// �������¼��ֵ��ʾ�������ϡ�
		/// </summary>
		/// <param name="p_objContent"></param>
		protected override void m_mthSetGUIFromContent(clsTrackRecordContent p_objContent)
		{
			if(p_objContent ==null)
				return;
			clsConveyRecordContent objContent=(clsConveyRecordContent)p_objContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
			m_txtOriginalDiagnose.m_mthSetNewText(objContent.m_strOriginalDiagnose,objContent.m_strOriginalDiagnoseXML);		
			m_txtConveyDiagnose.m_mthSetNewText(objContent.m_strConveyDiagnose,objContent.m_strConveyDiagnoseXML);		
			m_txtCaseHistory.m_mthSetNewText(objContent.m_strCaseHistory,objContent.m_strCaseHistoryXML);		
			m_txtConsultation.m_mthSetNewText(objContent.m_strConsultation,objContent.m_strConsultationXML);		
			m_txtConveyReason.m_mthSetNewText(objContent.m_strConveyReason,objContent.m_strConveyReasonXML);		
			m_txtNotice.m_mthSetNewText(objContent.m_strNotice,objContent.m_strNoticeXML);		

			#region ǩ������
            //lsvSign.Clear();
			if (objContent.objSignerArr!=null)
            {
                m_mthAddSignToListView(lsvSign, objContent.objSignerArr);
                //lsvSign.Items.Clear();
                //for (int i = 0; i < objContent.objSignerArr.Length; i++)
                //{
                //    if (objContent.objSignerArr[i].controlName=="lsvSign")
                //    {
                //        ListViewItem lviNewItem=new 						 ListViewItem(objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
                //        //ID ����ظ���
                //        lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR);
                //        //���� ������
                //        lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
                //        //tag��Ϊ����
                //        lviNewItem.Tag=objContent.objSignerArr[i].objEmployee;
                //        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                //        lsvSign.Items.Add(lviNewItem);
                //    }
                //}
			}
			#endregion ǩ��		


			#region ��Ժԭ��(����)
			clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr= new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID,m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
			if(objInPatientCaseHisoryDefaultValueArr !=null && objInPatientCaseHisoryDefaultValueArr.Length>0)
			{
				m_lblInHospitalReason.Text=objInPatientCaseHisoryDefaultValueArr[0].m_strMainDescription;
			}
			#endregion ��Ժԭ��(����)

		}

		public override int m_IntFormID
		{
			get
			{
				return 7;
			}
		}

		protected override void m_mthSetDeletedGUIFromContent(clsTrackRecordContent p_objContent)
		{
			if(p_objContent ==null)
				return;
			clsConveyRecordContent objContent=(clsConveyRecordContent)p_objContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
			m_txtOriginalDiagnose.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOriginalDiagnose,objContent.m_strOriginalDiagnoseXML);		
			m_txtConveyDiagnose.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strConveyDiagnose,objContent.m_strConveyDiagnoseXML);		
			m_txtCaseHistory.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strCaseHistory,objContent.m_strCaseHistoryXML);		
			m_txtConsultation.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strConsultation,objContent.m_strConsultationXML);		
			m_txtConveyReason.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strConveyReason,objContent.m_strConveyReasonXML);		
			m_txtNotice.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strNotice,objContent.m_strNoticeXML);

            if (objContent.objSignerArr != null)
            {
                m_mthAddSignToListView(lsvSign, objContent.objSignerArr);
            }
			#region ��Ժԭ��(����)
			clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr= new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID,m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
			if(objInPatientCaseHisoryDefaultValueArr !=null && objInPatientCaseHisoryDefaultValueArr.Length>0)
			{
				m_lblInHospitalReason.Text=objInPatientCaseHisoryDefaultValueArr[0].m_strMainDescription;
			}
			#endregion ��Ժԭ��(����)
		}

		/// <summary>
		/// ��ȡ���̼�¼�������ʵ��
		/// </summary>
		/// <returns></returns>
		protected override clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
		{
			//��ȡ���̼�¼�������ʵ��
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.Convey);					
		}

		/// <summary>
		/// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
		{
			//��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
			clsConveyRecordContent objContent=(clsConveyRecordContent)p_objRecordContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
			m_txtOriginalDiagnose.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOriginalDiagnose,objContent.m_strOriginalDiagnoseXML);		
			m_txtConveyDiagnose.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strConveyDiagnose,objContent.m_strConveyDiagnoseXML);		
			m_txtCaseHistory.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strCaseHistory,objContent.m_strCaseHistoryXML);		
			m_txtConsultation.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strConsultation,objContent.m_strConsultationXML);		
			m_txtConveyReason.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strConveyReason,objContent.m_strConveyReasonXML);		
			m_txtNotice.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strNotice,objContent.m_strNoticeXML);
		}

		#region ��ӡ(���ӵ��������в���Ҫ�ṩʵ��)
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

		// ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
		public override string m_strReloadFormTitle()
		{
			//���Ӵ�������ʵ��
			return	"ת����¼";
		}	
	
		/// <summary>
		/// ��ѡ����ڵ�ʱ,���������Ĭ��ֵ(���Ӵ�����Ҫ,������ʵ��)
		/// </summary>
		protected override void m_mthSelectRootNode()
		{
			#region �������Ĭ��ֵ
			if(m_objCurrentPatient != null)
			{
				clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr= new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss"));
				if(objInPatientCaseHisoryDefaultValueArr !=null && objInPatientCaseHisoryDefaultValueArr.Length>0)
				{
					m_txtOriginalDiagnose.Text=objInPatientCaseHisoryDefaultValueArr[0].m_strPrimaryDiagnose;
				}
			}
			#endregion �������Ĭ��ֵ
		}



		private void cmdConfirm_Click(object sender, System.EventArgs e)
		{
			if(m_lngSave() > 0)
			{
				this.DialogResult = DialogResult.Yes;
				this.Close();
			}
		}

		private void frmConvey_Load(object sender, System.EventArgs e)
		{
			if(m_objCurrentPatient !=null && m_ObjLastEmrPatientSession != null)
			{
                m_lblInHospitalDate.Text = m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy��MM��dd�� HH:mm");
				#region ��Ժԭ��(����)
				clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr= new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss"));
				if(objInPatientCaseHisoryDefaultValueArr !=null && objInPatientCaseHisoryDefaultValueArr.Length>0)
				{
					m_lblInHospitalReason.Text=objInPatientCaseHisoryDefaultValueArr[0].m_strMainDescription;
				}
				#endregion ��Ժԭ��(����)
			}


			m_txtOriginalDiagnose.Focus();

			this.m_dtpCreateDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
			this.m_dtpCreateDate.m_mthResetSize();

//			m_cmdNewTemplate.Visible = true;
		}

		private void m_cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


			
		/// <summary>
		/// ���ݸ���
		/// </summary>
		/// <param name="p_objSelectedPatient"></param>
		protected override void m_mthDataShare(clsPatient p_objSelectedPatient)
		{
			clsInPatientCaseHisoryDefaultValue [] objInPatientCaseDefaultValue = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(p_objSelectedPatient.m_StrInPatientID,p_objSelectedPatient.m_DtmSelectedInDate.ToString());
			if(objInPatientCaseDefaultValue !=null && objInPatientCaseDefaultValue.Length >0)
			{

				this.m_lblInHospitalReason.Text=objInPatientCaseDefaultValue[0].m_strMainDescription;
//				this.m_txtOriginalDiagnose.Text=objInPatientCaseDefaultValue[0].m_strPrimaryDiagnose;
//				this.m_txtConveyDiagnose.Text=objInPatientCaseDefaultValue[0].m_strPrimaryDiagnose;
//				this.m_txtCaseHistory.Text = "������" + objInPatientCaseDefaultValue[0].m_strMainDescription + "��" + DateTime.Parse(objInPatientCaseDefaultValue[0].m_strInPatientDate).ToString("yyyy��M��d��")  + "��Ժ��";

			}
		}
		
	}
}

