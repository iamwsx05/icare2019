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
	/// �����ࣩ���̼�¼�Ӵ����ʵ��,Jacky-2003-5-19
	/// </summary>
	public class frmHandOver : iCare.frmDiseaseTrackBase
	{
		private PinkieControls.ButtonXP cmdConfirm;
		private System.Windows.Forms.Label m_lblInHospitalDate;
		private System.Windows.Forms.Label lblInHospitalDateTitle;
		private System.Windows.Forms.Label lblReferralTitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtReferral;
		private System.Windows.Forms.Label lblCaseHistoryTitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtCaseHistory;
		private System.Windows.Forms.Label lblCurrentDiagnoseTitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtCurrentDiagnose;
		private System.Windows.Forms.Label lblOriginalDiagnoseTitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtOriginalDiagnose;
		private System.Windows.Forms.Label lblInHospitalReasonTitle;
		private System.Windows.Forms.Label m_lblInHospitalReason;
		private PinkieControls.ButtonXP m_cmdClose;
		private System.ComponentModel.IContainer components = null;
		private PinkieControls.ButtonXP m_cmdEmployeeSign;
		private clsCommonUseToolCollection m_objCUTC;

		private clsEmployeeSignTool m_objSignTool;
		protected System.Windows.Forms.ListView lsvSign;
		private System.Windows.Forms.ColumnHeader clmEmployeeName;

		//����ǩ����
		private clsEmrSignToolCollection m_objSign;

		public frmHandOver()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
            //ָ��ҽ������վ��
            intFormType = 1;
//			m_objSignTool = new clsEmployeeSignTool(m_lsvEmployee_New);
//			m_objSignTool.m_mthAddControl(m_txtSign);

			cmdConfirm.Visible=false;
			
			m_mthSetRichTextBoxAttribInControl(this);

			this.Text="�����¼";	
			lblCreateDateTitle.Text="����ʱ��:";
			this.m_lblForTitle.Text=this.Text;
			if(m_trnRoot !=null)
				m_trnRoot.Text="����ʱ��";
			
//			m_lblSign.Text=MDIParent.OperatorName;	


//			//ǩ������ֵ
//			m_objCUTC = new clsCommonUseToolCollection(this);
//			m_objCUTC.m_mthBindEmployeeSign(new Control[]{this.m_cmdEmployeeSign },
//				new Control[]{this.m_txtSign },new int[]{1});

			//ǩ������ֵ
			m_objSign = new clsEmrSignToolCollection();
			//m_mthBindEmployeeSign(��ť,ǩ����,ҽ��1or��ʿ2,�����֤trueorfalse);
            m_objSign.m_mthBindEmployeeSign(m_cmdEmployeeSign, lsvSign, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);

		
		}

		public override int m_IntFormID
		{
			get
			{
				return 5;
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
            this.cmdConfirm = new PinkieControls.ButtonXP();
            this.m_lblInHospitalDate = new System.Windows.Forms.Label();
            this.lblInHospitalDateTitle = new System.Windows.Forms.Label();
            this.lblReferralTitle = new System.Windows.Forms.Label();
            this.m_txtReferral = new com.digitalwave.controls.ctlRichTextBox();
            this.lblCaseHistoryTitle = new System.Windows.Forms.Label();
            this.m_txtCaseHistory = new com.digitalwave.controls.ctlRichTextBox();
            this.lblCurrentDiagnoseTitle = new System.Windows.Forms.Label();
            this.m_txtCurrentDiagnose = new com.digitalwave.controls.ctlRichTextBox();
            this.lblOriginalDiagnoseTitle = new System.Windows.Forms.Label();
            this.m_txtOriginalDiagnose = new com.digitalwave.controls.ctlRichTextBox();
            this.lblInHospitalReasonTitle = new System.Windows.Forms.Label();
            this.m_lblInHospitalReason = new System.Windows.Forms.Label();
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
            this.m_trvCreateDate.Location = new System.Drawing.Point(518, -91);
            this.m_trvCreateDate.Visible = false;
            // 
            // lblCreateDateTitle
            // 
            this.lblCreateDateTitle.Location = new System.Drawing.Point(29, 31);
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.Location = new System.Drawing.Point(104, 27);
            // 
            // m_dtpGetDataTime
            // 
            this.m_dtpGetDataTime.Location = new System.Drawing.Point(115, -21);
            // 
            // m_lblGetDataTime
            // 
            this.m_lblGetDataTime.Location = new System.Drawing.Point(11, -21);
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(589, -3);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(693, -3);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(261, -66);
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(749, -3);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(385, -3);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(537, -3);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(641, -3);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(29, -56);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(535, -107);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(535, -10);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(433, -7);
            this.m_txtPatientName.Size = new System.Drawing.Size(96, 23);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(305, -70);
            this.m_txtBedNO.Size = new System.Drawing.Size(56, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(77, -60);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(435, -88);
            this.m_lsvPatientName.Size = new System.Drawing.Size(96, 104);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(329, -88);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(77, -43);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(29, -35);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(12, 523);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Location = new System.Drawing.Point(361, -7);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(157, -71);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(273, -67);
            this.m_lblForTitle.Text = "�����¼";
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(13, 531);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(630, -33);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Location = new System.Drawing.Point(11, -76);
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
            this.cmdConfirm.Location = new System.Drawing.Point(737, 525);
            this.cmdConfirm.Name = "cmdConfirm";
            this.cmdConfirm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdConfirm.Size = new System.Drawing.Size(80, 30);
            this.cmdConfirm.TabIndex = 200;
            this.cmdConfirm.Text = "ȷ��";
            this.cmdConfirm.Click += new System.EventHandler(this.cmdConfirm_Click);
            // 
            // m_lblInHospitalDate
            // 
            this.m_lblInHospitalDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblInHospitalDate.ForeColor = System.Drawing.Color.Black;
            this.m_lblInHospitalDate.Location = new System.Drawing.Point(414, 31);
            this.m_lblInHospitalDate.Name = "m_lblInHospitalDate";
            this.m_lblInHospitalDate.Size = new System.Drawing.Size(212, 19);
            this.m_lblInHospitalDate.TabIndex = 6087;
            // 
            // lblInHospitalDateTitle
            // 
            this.lblInHospitalDateTitle.AutoSize = true;
            this.lblInHospitalDateTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInHospitalDateTitle.ForeColor = System.Drawing.Color.Black;
            this.lblInHospitalDateTitle.Location = new System.Drawing.Point(326, 31);
            this.lblInHospitalDateTitle.Name = "lblInHospitalDateTitle";
            this.lblInHospitalDateTitle.Size = new System.Drawing.Size(70, 14);
            this.lblInHospitalDateTitle.TabIndex = 6086;
            this.lblInHospitalDateTitle.Text = "��Ժʱ��:";
            // 
            // lblReferralTitle
            // 
            this.lblReferralTitle.AutoSize = true;
            this.lblReferralTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblReferralTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblReferralTitle.ForeColor = System.Drawing.Color.Black;
            this.lblReferralTitle.Location = new System.Drawing.Point(25, 422);
            this.lblReferralTitle.Name = "lblReferralTitle";
            this.lblReferralTitle.Size = new System.Drawing.Size(70, 14);
            this.lblReferralTitle.TabIndex = 6095;
            this.lblReferralTitle.Text = "���Ƽƻ�:";
            // 
            // m_txtReferral
            // 
            this.m_txtReferral.AccessibleDescription = "���Ƽƻ�";
            this.m_txtReferral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtReferral.BackColor = System.Drawing.Color.White;
            this.m_txtReferral.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtReferral.ForeColor = System.Drawing.Color.Black;
            this.m_txtReferral.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtReferral.Location = new System.Drawing.Point(101, 419);
            this.m_txtReferral.m_BlnIgnoreUserInfo = false;
            this.m_txtReferral.m_BlnPartControl = false;
            this.m_txtReferral.m_BlnReadOnly = false;
            this.m_txtReferral.m_BlnUnderLineDST = false;
            this.m_txtReferral.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtReferral.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtReferral.m_IntCanModifyTime = 500;
            this.m_txtReferral.m_IntPartControlLength = 0;
            this.m_txtReferral.m_IntPartControlStartIndex = 0;
            this.m_txtReferral.m_StrUserID = "";
            this.m_txtReferral.m_StrUserName = "";
            this.m_txtReferral.Name = "m_txtReferral";
            this.m_txtReferral.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtReferral.Size = new System.Drawing.Size(804, 100);
            this.m_txtReferral.TabIndex = 140;
            this.m_txtReferral.Text = "";
            // 
            // lblCaseHistoryTitle
            // 
            this.lblCaseHistoryTitle.AutoSize = true;
            this.lblCaseHistoryTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblCaseHistoryTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCaseHistoryTitle.ForeColor = System.Drawing.Color.Black;
            this.lblCaseHistoryTitle.Location = new System.Drawing.Point(25, 312);
            this.lblCaseHistoryTitle.Name = "lblCaseHistoryTitle";
            this.lblCaseHistoryTitle.Size = new System.Drawing.Size(70, 14);
            this.lblCaseHistoryTitle.TabIndex = 6093;
            this.lblCaseHistoryTitle.Text = "������:";
            // 
            // m_txtCaseHistory
            // 
            this.m_txtCaseHistory.AccessibleDescription = "������";
            this.m_txtCaseHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCaseHistory.BackColor = System.Drawing.Color.White;
            this.m_txtCaseHistory.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtCaseHistory.ForeColor = System.Drawing.Color.Black;
            this.m_txtCaseHistory.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtCaseHistory.Location = new System.Drawing.Point(101, 309);
            this.m_txtCaseHistory.m_BlnIgnoreUserInfo = false;
            this.m_txtCaseHistory.m_BlnPartControl = false;
            this.m_txtCaseHistory.m_BlnReadOnly = false;
            this.m_txtCaseHistory.m_BlnUnderLineDST = false;
            this.m_txtCaseHistory.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtCaseHistory.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtCaseHistory.m_IntCanModifyTime = 500;
            this.m_txtCaseHistory.m_IntPartControlLength = 0;
            this.m_txtCaseHistory.m_IntPartControlStartIndex = 0;
            this.m_txtCaseHistory.m_StrUserID = "";
            this.m_txtCaseHistory.m_StrUserName = "";
            this.m_txtCaseHistory.Name = "m_txtCaseHistory";
            this.m_txtCaseHistory.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtCaseHistory.Size = new System.Drawing.Size(804, 104);
            this.m_txtCaseHistory.TabIndex = 130;
            this.m_txtCaseHistory.Text = "";
            // 
            // lblCurrentDiagnoseTitle
            // 
            this.lblCurrentDiagnoseTitle.AutoSize = true;
            this.lblCurrentDiagnoseTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentDiagnoseTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentDiagnoseTitle.ForeColor = System.Drawing.Color.Black;
            this.lblCurrentDiagnoseTitle.Location = new System.Drawing.Point(25, 212);
            this.lblCurrentDiagnoseTitle.Name = "lblCurrentDiagnoseTitle";
            this.lblCurrentDiagnoseTitle.Size = new System.Drawing.Size(70, 14);
            this.lblCurrentDiagnoseTitle.TabIndex = 6091;
            this.lblCurrentDiagnoseTitle.Text = "Ŀǰ���:";
            // 
            // m_txtCurrentDiagnose
            // 
            this.m_txtCurrentDiagnose.AccessibleDescription = "Ŀǰ���";
            this.m_txtCurrentDiagnose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCurrentDiagnose.BackColor = System.Drawing.Color.White;
            this.m_txtCurrentDiagnose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtCurrentDiagnose.ForeColor = System.Drawing.Color.Black;
            this.m_txtCurrentDiagnose.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtCurrentDiagnose.Location = new System.Drawing.Point(101, 209);
            this.m_txtCurrentDiagnose.m_BlnIgnoreUserInfo = false;
            this.m_txtCurrentDiagnose.m_BlnPartControl = false;
            this.m_txtCurrentDiagnose.m_BlnReadOnly = false;
            this.m_txtCurrentDiagnose.m_BlnUnderLineDST = false;
            this.m_txtCurrentDiagnose.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtCurrentDiagnose.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtCurrentDiagnose.m_IntCanModifyTime = 500;
            this.m_txtCurrentDiagnose.m_IntPartControlLength = 0;
            this.m_txtCurrentDiagnose.m_IntPartControlStartIndex = 0;
            this.m_txtCurrentDiagnose.m_StrUserID = "";
            this.m_txtCurrentDiagnose.m_StrUserName = "";
            this.m_txtCurrentDiagnose.Name = "m_txtCurrentDiagnose";
            this.m_txtCurrentDiagnose.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtCurrentDiagnose.Size = new System.Drawing.Size(804, 94);
            this.m_txtCurrentDiagnose.TabIndex = 120;
            this.m_txtCurrentDiagnose.Text = "";
            // 
            // lblOriginalDiagnoseTitle
            // 
            this.lblOriginalDiagnoseTitle.AutoSize = true;
            this.lblOriginalDiagnoseTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblOriginalDiagnoseTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOriginalDiagnoseTitle.ForeColor = System.Drawing.Color.Black;
            this.lblOriginalDiagnoseTitle.Location = new System.Drawing.Point(25, 107);
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
            this.m_txtOriginalDiagnose.Location = new System.Drawing.Point(101, 107);
            this.m_txtOriginalDiagnose.m_BlnIgnoreUserInfo = false;
            this.m_txtOriginalDiagnose.m_BlnPartControl = false;
            this.m_txtOriginalDiagnose.m_BlnReadOnly = false;
            this.m_txtOriginalDiagnose.m_BlnUnderLineDST = false;
            this.m_txtOriginalDiagnose.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtOriginalDiagnose.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtOriginalDiagnose.m_IntCanModifyTime = 500;
            this.m_txtOriginalDiagnose.m_IntPartControlLength = 0;
            this.m_txtOriginalDiagnose.m_IntPartControlStartIndex = 0;
            this.m_txtOriginalDiagnose.m_StrUserID = "";
            this.m_txtOriginalDiagnose.m_StrUserName = "";
            this.m_txtOriginalDiagnose.Name = "m_txtOriginalDiagnose";
            this.m_txtOriginalDiagnose.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtOriginalDiagnose.Size = new System.Drawing.Size(804, 94);
            this.m_txtOriginalDiagnose.TabIndex = 110;
            this.m_txtOriginalDiagnose.Text = "";
            // 
            // lblInHospitalReasonTitle
            // 
            this.lblInHospitalReasonTitle.AutoSize = true;
            this.lblInHospitalReasonTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInHospitalReasonTitle.ForeColor = System.Drawing.Color.Black;
            this.lblInHospitalReasonTitle.Location = new System.Drawing.Point(28, 52);
            this.lblInHospitalReasonTitle.Name = "lblInHospitalReasonTitle";
            this.lblInHospitalReasonTitle.Size = new System.Drawing.Size(70, 14);
            this.lblInHospitalReasonTitle.TabIndex = 6098;
            this.lblInHospitalReasonTitle.Text = "��Ժԭ��:";
            // 
            // m_lblInHospitalReason
            // 
            this.m_lblInHospitalReason.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblInHospitalReason.ForeColor = System.Drawing.Color.Black;
            this.m_lblInHospitalReason.Location = new System.Drawing.Point(101, 52);
            this.m_lblInHospitalReason.Name = "m_lblInHospitalReason";
            this.m_lblInHospitalReason.Size = new System.Drawing.Size(804, 52);
            this.m_lblInHospitalReason.TabIndex = 6099;
            // 
            // m_cmdClose
            // 
            this.m_cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdClose.DefaultScheme = true;
            this.m_cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdClose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdClose.Hint = "";
            this.m_cmdClose.Location = new System.Drawing.Point(825, 525);
            this.m_cmdClose.Name = "m_cmdClose";
            this.m_cmdClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdClose.Size = new System.Drawing.Size(80, 30);
            this.m_cmdClose.TabIndex = 201;
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
            this.m_cmdEmployeeSign.Location = new System.Drawing.Point(101, 525);
            this.m_cmdEmployeeSign.Name = "m_cmdEmployeeSign";
            this.m_cmdEmployeeSign.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdEmployeeSign.Size = new System.Drawing.Size(64, 30);
            this.m_cmdEmployeeSign.TabIndex = 10000080;
            this.m_cmdEmployeeSign.Tag = "1";
            this.m_cmdEmployeeSign.Text = "ǩ��:";
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
            this.lsvSign.Location = new System.Drawing.Point(173, 527);
            this.lsvSign.Name = "lsvSign";
            this.lsvSign.Size = new System.Drawing.Size(558, 28);
            this.lsvSign.TabIndex = 10000082;
            this.lsvSign.UseCompatibleStateImageBehavior = false;
            this.lsvSign.View = System.Windows.Forms.View.SmallIcon;
            // 
            // clmEmployeeName
            // 
            this.clmEmployeeName.Width = 55;
            // 
            // frmHandOver
            // 
            this.AccessibleDescription = "�����¼";
            this.CancelButton = this.m_cmdClose;
            this.ClientSize = new System.Drawing.Size(934, 567);
            this.Controls.Add(this.lsvSign);
            this.Controls.Add(this.lblInHospitalReasonTitle);
            this.Controls.Add(this.lblReferralTitle);
            this.Controls.Add(this.lblCaseHistoryTitle);
            this.Controls.Add(this.lblCurrentDiagnoseTitle);
            this.Controls.Add(this.lblOriginalDiagnoseTitle);
            this.Controls.Add(this.lblInHospitalDateTitle);
            this.Controls.Add(this.m_cmdEmployeeSign);
            this.Controls.Add(this.m_cmdClose);
            this.Controls.Add(this.m_txtReferral);
            this.Controls.Add(this.m_txtCaseHistory);
            this.Controls.Add(this.m_txtCurrentDiagnose);
            this.Controls.Add(this.m_txtOriginalDiagnose);
            this.Controls.Add(this.m_lblInHospitalDate);
            this.Controls.Add(this.cmdConfirm);
            this.Controls.Add(this.m_lblInHospitalReason);
            this.Name = "frmHandOver";
            this.Text = "�����¼";
            this.Load += new System.EventHandler(this.frmHandOver_Load);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_lblGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_dtpGetDataTime, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.m_lblInHospitalReason, 0);
            this.Controls.SetChildIndex(this.cmdConfirm, 0);
            this.Controls.SetChildIndex(this.m_lblInHospitalDate, 0);
            this.Controls.SetChildIndex(this.m_txtOriginalDiagnose, 0);
            this.Controls.SetChildIndex(this.m_txtCurrentDiagnose, 0);
            this.Controls.SetChildIndex(this.m_txtCaseHistory, 0);
            this.Controls.SetChildIndex(this.m_txtReferral, 0);
            this.Controls.SetChildIndex(this.m_cmdClose, 0);
            this.Controls.SetChildIndex(this.m_cmdEmployeeSign, 0);
            this.Controls.SetChildIndex(this.m_dtpCreateDate, 0);
            this.Controls.SetChildIndex(this.lblCreateDateTitle, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
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
            this.Controls.SetChildIndex(this.lblCurrentDiagnoseTitle, 0);
            this.Controls.SetChildIndex(this.lblCaseHistoryTitle, 0);
            this.Controls.SetChildIndex(this.lblReferralTitle, 0);
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
            clsHandOverInfo objTrackInfo = new clsHandOverInfo(m_objCurrentPatient);

			objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;//m_objGetContentFromGUI();
			objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
			objTrackInfo.m_StrTitle = "�����¼";			

			//����m_strTitle��m_dtmRecordTime
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
			//Ĭ��ǩ��
			MDIParent.m_mthSetDefaulEmployee(lsvSign);


			//��վ����¼����			
			m_txtOriginalDiagnose.m_mthClearText();
			m_txtCaseHistory.m_mthClearText();
			m_txtCurrentDiagnose.m_mthClearText();
			m_txtReferral.m_mthClearText();						
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
                //    if(control.Name!="m_dtpCreateDate")
                //        control.Top=control.Top-105;				
                //}
			
				cmdConfirm.Visible=true;
				
                //this.Size=new Size(this.Size.Width, this.Size.Height-105);
				this.CenterToParent();	
		
                //lblCreateDateTitle.Left=lblOriginalDiagnoseTitle.Left;//=16;
                //lblCreateDateTitle.Top=15;	
                //m_dtpCreateDate.Left=lblCreateDateTitle.Right+5;
                //m_dtpCreateDate.Top=lblCreateDateTitle.Top;				
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
			clsHandOverRecordContent objContent=new clsHandOverRecordContent();
			//��ȡlsvsignǩ��
			objContent.objSignerArr=new clsEmrSigns_VO[intSignCount];
			strUserIDList="";
			strUserNameList="";
            m_mthGetSignArr(new Control[] { lsvSign }, ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);
            //for (int i = 0; i < intSignCount; i++)
            //{
            //    objContent.objSignerArr[i]=new clsEmrSigns_VO();
            //    objContent.objSignerArr[i].objEmployee=new clsEmrEmployeeBase_VO();
            //    objContent.objSignerArr[i].objEmployee=(clsEmrEmployeeBase_VO)( lsvSign.Items[i].Tag);
            //    objContent.objSignerArr[i].controlName="lsvSign";
            //    objContent.objSignerArr[i].m_strFORMID_VCHR="frmHandOver";//ע���Сд
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
			
			objContent.m_strCurrentDiagnose_Right=m_txtCurrentDiagnose.m_strGetRightText();	
			objContent.m_strCurrentDiagnose=m_txtCurrentDiagnose.Text;
			objContent.m_strCurrentDiagnoseXML=m_txtCurrentDiagnose.m_strGetXmlText();					
			
			objContent.m_strCaseHistory_Right=m_txtCaseHistory.m_strGetRightText();	
			objContent.m_strCaseHistory=m_txtCaseHistory.Text;
			objContent.m_strCaseHistoryXML=m_txtCaseHistory.m_strGetXmlText();					
			
			objContent.m_strReferral_Right=m_txtReferral.m_strGetRightText();	
			objContent.m_strReferral=m_txtReferral.Text;
			objContent.m_strReferralXML=m_txtReferral.m_strGetXmlText();			

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
			clsHandOverRecordContent objContent=(clsHandOverRecordContent)p_objContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
			m_txtOriginalDiagnose.m_mthSetNewText(objContent.m_strOriginalDiagnose,objContent.m_strOriginalDiagnoseXML);		
			m_txtCurrentDiagnose.m_mthSetNewText(objContent.m_strCurrentDiagnose,objContent.m_strCurrentDiagnoseXML);		
			m_txtCaseHistory.m_mthSetNewText(objContent.m_strCaseHistory,objContent.m_strCaseHistoryXML);		
			m_txtReferral.m_mthSetNewText(objContent.m_strReferral,objContent.m_strReferralXML);		
			
			#region ǩ������
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

		protected override void m_mthSetDeletedGUIFromContent(clsTrackRecordContent p_objContent)
		{
			if(p_objContent ==null)
				return;
			clsHandOverRecordContent objContent=(clsHandOverRecordContent)p_objContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
			m_txtOriginalDiagnose.Text=com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOriginalDiagnose,objContent.m_strOriginalDiagnoseXML);		
			m_txtCurrentDiagnose.Text=com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strCurrentDiagnose,objContent.m_strCurrentDiagnoseXML);		
			m_txtCaseHistory.Text=com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strCaseHistory,objContent.m_strCaseHistoryXML);		
			m_txtReferral.Text=com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strReferral,objContent.m_strReferralXML);

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
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.HandOver);					
		}

		/// <summary>
		/// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
		{
			//��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
			clsHandOverRecordContent objContent=(clsHandOverRecordContent)p_objRecordContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
			m_txtOriginalDiagnose.Text=com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOriginalDiagnose,objContent.m_strOriginalDiagnoseXML);		
			m_txtCurrentDiagnose.Text=com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strCurrentDiagnose,objContent.m_strCurrentDiagnoseXML);		
			m_txtCaseHistory.Text=com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strCaseHistory,objContent.m_strCaseHistoryXML);		
			m_txtReferral.Text=com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strReferral,objContent.m_strReferralXML);		
			
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
			return	"�����¼";
		}	
	
		/// <summary>
		/// ��ѡ����ڵ�ʱ,���������Ĭ��ֵ(���Ӵ�����Ҫ,������ʵ��)
		/// </summary>
		protected override void m_mthSelectRootNode()
		{
			#region �������Ĭ��ֵ
			if(m_objCurrentPatient != null && m_ObjLastEmrPatientSession != null)
			{
				clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr= new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID,m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
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

		private void frmHandOver_Load(object sender, System.EventArgs e)
		{
			if(m_objCurrentPatient !=null && m_ObjLastEmrPatientSession != null)
			{
                m_lblInHospitalDate.Text = m_ObjLastEmrPatientSession.m_dtmHISInpatientDate.ToString("yyyy��MM��dd�� HH:mm");
				#region ��Ժԭ��(����)
                clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID, m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
				if(objInPatientCaseHisoryDefaultValueArr !=null && objInPatientCaseHisoryDefaultValueArr.Length>0)
				{
					m_lblInHospitalReason.Text=objInPatientCaseHisoryDefaultValueArr[0].m_strMainDescription;
				}
				#endregion ��Ժԭ��(����)
			}

			this.m_dtpCreateDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
			this.m_dtpCreateDate.m_mthResetSize();

			m_txtOriginalDiagnose.Focus();

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
//			clsInPatientCaseHisoryDefaultValue [] objInPatientCaseDefaultValue = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(p_objSelectedPatient.m_StrInPatientID,p_objSelectedPatient.m_DtmLastInDate.ToString());
//			if(objInPatientCaseDefaultValue !=null && objInPatientCaseDefaultValue.Length >0)
//			{
//				this.m_txtOriginalDiagnose.Text = objInPatientCaseDefaultValue[0].m_strPrimaryDiagnose;
//				this.m_txtCurrentDiagnose.Text = objInPatientCaseDefaultValue[0].m_strPrimaryDiagnose;
//			}
		}
		
	}
}

