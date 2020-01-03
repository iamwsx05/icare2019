using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using com.digitalwave.Emr.Signature_gui;//��ǩ��
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;
using System.Data;
using HRP;

namespace iCare
{
	/// <summary>
	/// frmFirstIllnessNote ��ժҪ˵����
	/// </summary>
	public class frmFirstIllnessNote : iCare.frmDiseaseTrackBase
	{
		private System.Windows.Forms.Label lblReferralTitle;
		private System.Windows.Forms.Label lblCaseHistoryTitle;
		private System.Windows.Forms.Label label8;
		private com.digitalwave.controls.ctlRichTextBox m_txtCurePlan;
		private com.digitalwave.controls.ctlRichTextBox m_txtDiagnoseDiffe;
		private com.digitalwave.controls.ctlRichTextBox m_txtDiagnoseThe;
		private com.digitalwave.controls.ctlRichTextBox m_txtOriginalDiagnose;
		private com.digitalwave.controls.ctlRichTextBox m_txtMostlyContent;
		private PinkieControls.ButtonXP m_cmdClose;
		private PinkieControls.ButtonXP cmdConfirm;
		private PinkieControls.ButtonXP m_cmdEmployeeSign;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblInHospitalCaseTitle;
		/// <summary>
		/// ����������������
		/// </summary>
		/// 
 		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListView lsvSign;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		//����ǩ����
		private clsEmrSignToolCollection m_objSign;



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
            this.lblReferralTitle = new System.Windows.Forms.Label();
            this.lblCaseHistoryTitle = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.m_txtCurePlan = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtDiagnoseDiffe = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtDiagnoseThe = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtOriginalDiagnose = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtMostlyContent = new com.digitalwave.controls.ctlRichTextBox();
            this.m_cmdClose = new PinkieControls.ButtonXP();
            this.cmdConfirm = new PinkieControls.ButtonXP();
            this.m_cmdEmployeeSign = new PinkieControls.ButtonXP();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInHospitalCaseTitle = new System.Windows.Forms.Label();
            this.lsvSign = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_trvCreateDate
            // 
            this.m_trvCreateDate.LineColor = System.Drawing.Color.Black;
            this.m_trvCreateDate.Location = new System.Drawing.Point(10, -90);
            this.m_trvCreateDate.Visible = false;
            // 
            // lblCreateDateTitle
            // 
            this.lblCreateDateTitle.Location = new System.Drawing.Point(9, 16);
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.Location = new System.Drawing.Point(83, 16);
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(528, -26);
            this.lblSex.Size = new System.Drawing.Size(56, 21);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(648, -26);
            this.lblAge.Size = new System.Drawing.Size(60, 21);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(275, -64);
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(261, -27);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(476, -64);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(472, -26);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(592, -26);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(8, -34);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(105, -124);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(135, 119);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(327, -32);
            this.txtInPatientID.Size = new System.Drawing.Size(135, 23);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(527, -68);
            this.m_txtPatientName.Size = new System.Drawing.Size(136, 23);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(327, -68);
            this.m_txtBedNO.Size = new System.Drawing.Size(135, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(64, -34);
            this.m_cboArea.Size = new System.Drawing.Size(168, 23);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(64, -124);
            this.m_lsvPatientName.Size = new System.Drawing.Size(136, 119);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(41, -124);
            this.m_lsvBedNO.Size = new System.Drawing.Size(135, 119);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(72, -74);
            this.m_cboDept.Size = new System.Drawing.Size(168, 23);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(8, -66);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(520, -38);
            this.m_cmdNewTemplate.Size = new System.Drawing.Size(98, 36);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.Location = new System.Drawing.Point(229, -68);
            this.m_cmdNext.Size = new System.Drawing.Size(28, 24);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(182, -68);
            this.m_cmdPre.Size = new System.Drawing.Size(28, 24);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(317, -64);
            this.m_lblForTitle.Size = new System.Drawing.Size(19, 27);
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(0, 560);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(629, -32);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Location = new System.Drawing.Point(10, -75);
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            // 
            // lblReferralTitle
            // 
            this.lblReferralTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblReferralTitle.AutoSize = true;
            this.lblReferralTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblReferralTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblReferralTitle.ForeColor = System.Drawing.Color.Black;
            this.lblReferralTitle.Location = new System.Drawing.Point(8, 435);
            this.lblReferralTitle.Name = "lblReferralTitle";
            this.lblReferralTitle.Size = new System.Drawing.Size(70, 14);
            this.lblReferralTitle.TabIndex = 10000056;
            this.lblReferralTitle.Text = "�������:";
            // 
            // lblCaseHistoryTitle
            // 
            this.lblCaseHistoryTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCaseHistoryTitle.AutoSize = true;
            this.lblCaseHistoryTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblCaseHistoryTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCaseHistoryTitle.ForeColor = System.Drawing.Color.Black;
            this.lblCaseHistoryTitle.Location = new System.Drawing.Point(8, 360);
            this.lblCaseHistoryTitle.Name = "lblCaseHistoryTitle";
            this.lblCaseHistoryTitle.Size = new System.Drawing.Size(70, 14);
            this.lblCaseHistoryTitle.TabIndex = 10000055;
            this.lblCaseHistoryTitle.Text = "�������:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(8, 273);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 14);
            this.label8.TabIndex = 10000054;
            this.label8.Text = "�������:";
            // 
            // m_txtCurePlan
            // 
            this.m_txtCurePlan.AccessibleDescription = "���Ƽƻ�";
            this.m_txtCurePlan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCurePlan.BackColor = System.Drawing.Color.White;
            this.m_txtCurePlan.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtCurePlan.ForeColor = System.Drawing.Color.Black;
            this.m_txtCurePlan.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtCurePlan.Location = new System.Drawing.Point(83, 500);
            this.m_txtCurePlan.m_BlnIgnoreUserInfo = false;
            this.m_txtCurePlan.m_BlnPartControl = false;
            this.m_txtCurePlan.m_BlnReadOnly = false;
            this.m_txtCurePlan.m_BlnUnderLineDST = false;
            this.m_txtCurePlan.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtCurePlan.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtCurePlan.m_IntCanModifyTime = 6;
            this.m_txtCurePlan.m_IntPartControlLength = 0;
            this.m_txtCurePlan.m_IntPartControlStartIndex = 0;
            this.m_txtCurePlan.m_StrUserID = "";
            this.m_txtCurePlan.m_StrUserName = "";
            this.m_txtCurePlan.Name = "m_txtCurePlan";
            this.m_txtCurePlan.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtCurePlan.Size = new System.Drawing.Size(805, 54);
            this.m_txtCurePlan.TabIndex = 10000052;
            this.m_txtCurePlan.Text = "";
            // 
            // m_txtDiagnoseDiffe
            // 
            this.m_txtDiagnoseDiffe.AccessibleDescription = "�������";
            this.m_txtDiagnoseDiffe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtDiagnoseDiffe.BackColor = System.Drawing.Color.White;
            this.m_txtDiagnoseDiffe.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDiagnoseDiffe.ForeColor = System.Drawing.Color.Black;
            this.m_txtDiagnoseDiffe.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtDiagnoseDiffe.Location = new System.Drawing.Point(83, 435);
            this.m_txtDiagnoseDiffe.m_BlnIgnoreUserInfo = false;
            this.m_txtDiagnoseDiffe.m_BlnPartControl = false;
            this.m_txtDiagnoseDiffe.m_BlnReadOnly = false;
            this.m_txtDiagnoseDiffe.m_BlnUnderLineDST = false;
            this.m_txtDiagnoseDiffe.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtDiagnoseDiffe.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtDiagnoseDiffe.m_IntCanModifyTime = 6;
            this.m_txtDiagnoseDiffe.m_IntPartControlLength = 0;
            this.m_txtDiagnoseDiffe.m_IntPartControlStartIndex = 0;
            this.m_txtDiagnoseDiffe.m_StrUserID = "";
            this.m_txtDiagnoseDiffe.m_StrUserName = "";
            this.m_txtDiagnoseDiffe.Name = "m_txtDiagnoseDiffe";
            this.m_txtDiagnoseDiffe.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtDiagnoseDiffe.Size = new System.Drawing.Size(805, 59);
            this.m_txtDiagnoseDiffe.TabIndex = 10000051;
            this.m_txtDiagnoseDiffe.Text = "";
            // 
            // m_txtDiagnoseThe
            // 
            this.m_txtDiagnoseThe.AccessibleDescription = "�������";
            this.m_txtDiagnoseThe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtDiagnoseThe.BackColor = System.Drawing.Color.White;
            this.m_txtDiagnoseThe.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDiagnoseThe.ForeColor = System.Drawing.Color.Black;
            this.m_txtDiagnoseThe.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtDiagnoseThe.Location = new System.Drawing.Point(83, 360);
            this.m_txtDiagnoseThe.m_BlnIgnoreUserInfo = false;
            this.m_txtDiagnoseThe.m_BlnPartControl = false;
            this.m_txtDiagnoseThe.m_BlnReadOnly = false;
            this.m_txtDiagnoseThe.m_BlnUnderLineDST = false;
            this.m_txtDiagnoseThe.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtDiagnoseThe.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtDiagnoseThe.m_IntCanModifyTime = 6;
            this.m_txtDiagnoseThe.m_IntPartControlLength = 0;
            this.m_txtDiagnoseThe.m_IntPartControlStartIndex = 0;
            this.m_txtDiagnoseThe.m_StrUserID = "";
            this.m_txtDiagnoseThe.m_StrUserName = "";
            this.m_txtDiagnoseThe.Name = "m_txtDiagnoseThe";
            this.m_txtDiagnoseThe.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtDiagnoseThe.Size = new System.Drawing.Size(805, 69);
            this.m_txtDiagnoseThe.TabIndex = 10000050;
            this.m_txtDiagnoseThe.Text = "";
            // 
            // m_txtOriginalDiagnose
            // 
            this.m_txtOriginalDiagnose.AccessibleDescription = "�������";
            this.m_txtOriginalDiagnose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtOriginalDiagnose.BackColor = System.Drawing.Color.White;
            this.m_txtOriginalDiagnose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtOriginalDiagnose.ForeColor = System.Drawing.Color.Black;
            this.m_txtOriginalDiagnose.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtOriginalDiagnose.Location = new System.Drawing.Point(83, 273);
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
            this.m_txtOriginalDiagnose.Size = new System.Drawing.Size(805, 81);
            this.m_txtOriginalDiagnose.TabIndex = 10000049;
            this.m_txtOriginalDiagnose.Text = "";
            // 
            // m_txtMostlyContent
            // 
            this.m_txtMostlyContent.AccessibleDescription = "��Ҫ����";
            this.m_txtMostlyContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtMostlyContent.BackColor = System.Drawing.Color.White;
            this.m_txtMostlyContent.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtMostlyContent.ForeColor = System.Drawing.Color.Black;
            this.m_txtMostlyContent.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtMostlyContent.Location = new System.Drawing.Point(83, 47);
            this.m_txtMostlyContent.m_BlnIgnoreUserInfo = false;
            this.m_txtMostlyContent.m_BlnPartControl = false;
            this.m_txtMostlyContent.m_BlnReadOnly = false;
            this.m_txtMostlyContent.m_BlnUnderLineDST = false;
            this.m_txtMostlyContent.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtMostlyContent.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtMostlyContent.m_IntCanModifyTime = 6;
            this.m_txtMostlyContent.m_IntPartControlLength = 0;
            this.m_txtMostlyContent.m_IntPartControlStartIndex = 0;
            this.m_txtMostlyContent.m_StrUserID = "";
            this.m_txtMostlyContent.m_StrUserName = "";
            this.m_txtMostlyContent.Name = "m_txtMostlyContent";
            this.m_txtMostlyContent.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtMostlyContent.Size = new System.Drawing.Size(805, 220);
            this.m_txtMostlyContent.TabIndex = 10000048;
            this.m_txtMostlyContent.Text = "";
            // 
            // m_cmdClose
            // 
            this.m_cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdClose.DefaultScheme = true;
            this.m_cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdClose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdClose.Hint = "";
            this.m_cmdClose.Location = new System.Drawing.Point(808, 560);
            this.m_cmdClose.Name = "m_cmdClose";
            this.m_cmdClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdClose.Size = new System.Drawing.Size(80, 30);
            this.m_cmdClose.TabIndex = 10000066;
            this.m_cmdClose.Text = "ȡ��";
            this.m_cmdClose.Click += new System.EventHandler(this.m_cmdClose_Click);
            // 
            // cmdConfirm
            // 
            this.cmdConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdConfirm.DefaultScheme = true;
            this.cmdConfirm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdConfirm.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdConfirm.Hint = "";
            this.cmdConfirm.Location = new System.Drawing.Point(712, 560);
            this.cmdConfirm.Name = "cmdConfirm";
            this.cmdConfirm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdConfirm.Size = new System.Drawing.Size(80, 30);
            this.cmdConfirm.TabIndex = 10000065;
            this.cmdConfirm.Text = "ȷ��";
            this.cmdConfirm.Click += new System.EventHandler(this.cmdConfirm_Click);
            // 
            // m_cmdEmployeeSign
            // 
            this.m_cmdEmployeeSign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cmdEmployeeSign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdEmployeeSign.DefaultScheme = true;
            this.m_cmdEmployeeSign.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdEmployeeSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdEmployeeSign.Hint = "";
            this.m_cmdEmployeeSign.Location = new System.Drawing.Point(80, 560);
            this.m_cmdEmployeeSign.Name = "m_cmdEmployeeSign";
            this.m_cmdEmployeeSign.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdEmployeeSign.Size = new System.Drawing.Size(76, 30);
            this.m_cmdEmployeeSign.TabIndex = 10000067;
            this.m_cmdEmployeeSign.Tag = "1";
            this.m_cmdEmployeeSign.Text = "ǩ��:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 500);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 10000057;
            this.label1.Text = "���Ƽƻ�:";
            // 
            // lblInHospitalCaseTitle
            // 
            this.lblInHospitalCaseTitle.AccessibleDescription = "";
            this.lblInHospitalCaseTitle.AutoSize = true;
            this.lblInHospitalCaseTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblInHospitalCaseTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInHospitalCaseTitle.ForeColor = System.Drawing.Color.Black;
            this.lblInHospitalCaseTitle.Location = new System.Drawing.Point(8, 47);
            this.lblInHospitalCaseTitle.Name = "lblInHospitalCaseTitle";
            this.lblInHospitalCaseTitle.Size = new System.Drawing.Size(70, 14);
            this.lblInHospitalCaseTitle.TabIndex = 10000053;
            this.lblInHospitalCaseTitle.Text = "��Ҫ����:";
            // 
            // lsvSign
            // 
            this.lsvSign.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.lsvSign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvSign.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lsvSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lsvSign.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvSign.Location = new System.Drawing.Point(160, 560);
            this.lsvSign.MultiSelect = false;
            this.lsvSign.Name = "lsvSign";
            this.lsvSign.Size = new System.Drawing.Size(528, 28);
            this.lsvSign.TabIndex = 10000068;
            this.lsvSign.UseCompatibleStateImageBehavior = false;
            this.lsvSign.View = System.Windows.Forms.View.SmallIcon;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 50;
            // 
            // frmFirstIllnessNote
            // 
            this.ClientSize = new System.Drawing.Size(904, 621);
            this.Controls.Add(this.lsvSign);
            this.Controls.Add(this.m_cmdEmployeeSign);
            this.Controls.Add(this.m_cmdClose);
            this.Controls.Add(this.cmdConfirm);
            this.Controls.Add(this.lblReferralTitle);
            this.Controls.Add(this.lblCaseHistoryTitle);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.m_txtCurePlan);
            this.Controls.Add(this.m_txtDiagnoseDiffe);
            this.Controls.Add(this.m_txtDiagnoseThe);
            this.Controls.Add(this.m_txtOriginalDiagnose);
            this.Controls.Add(this.m_txtMostlyContent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblInHospitalCaseTitle);
            this.Name = "frmFirstIllnessNote";
            this.Text = "�״β��̼�¼";
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_lblGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_dtpGetDataTime, 0);
            this.Controls.SetChildIndex(this.lblInHospitalCaseTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
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
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.m_txtMostlyContent, 0);
            this.Controls.SetChildIndex(this.m_txtOriginalDiagnose, 0);
            this.Controls.SetChildIndex(this.m_txtDiagnoseThe, 0);
            this.Controls.SetChildIndex(this.m_txtDiagnoseDiffe, 0);
            this.Controls.SetChildIndex(this.m_txtCurePlan, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.lblCaseHistoryTitle, 0);
            this.Controls.SetChildIndex(this.lblReferralTitle, 0);
            this.Controls.SetChildIndex(this.cmdConfirm, 0);
            this.Controls.SetChildIndex(this.m_cmdClose, 0);
            this.Controls.SetChildIndex(this.m_cmdEmployeeSign, 0);
            this.Controls.SetChildIndex(this.lsvSign, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region ���캯��
		/// <summary>
		/// ���캯��
		/// </summary>
		public frmFirstIllnessNote()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();
            //ָ��ҽ������վ��
            intFormType = 1;
 			cmdConfirm.Visible=false;
            // ����RichTextBox���ԡ����Ҽ��˵����û��������û�ID������������ɫ��˫������ɫ��
			m_mthSetRichTextBoxAttribInControl(this);
 			//ǩ������ֵ
			m_objSign = new clsEmrSignToolCollection();
			//m_mthBindEmployeeSign(��ť,ǩ����,ҽ��1or��ʿ2,�����֤trueorfalse);
            m_objSign.m_mthBindEmployeeSign(m_cmdEmployeeSign, lsvSign, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);

		}
		#endregion
		public override clsDiseaseTrackInfo m_objGetDiseaseTrackInfo()
		{
			clsDiseaseSummaryInfo objTrackInfo = new clsDiseaseSummaryInfo();

			objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;//m_objGetContentFromGUI();
			objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
			objTrackInfo.m_StrTitle = "�״β��̼�¼";			

			//����m_strTitle��m_dtmRecordTime
            if (objTrackInfo.m_ObjRecordContent != null)
            {
                m_dtpCreateDate.Value = objTrackInfo.m_ObjRecordContent.m_dtmCreateDate;
                m_dtpCreateDate.Refresh();
            }
			return objTrackInfo;		
		}

		/// <summary>
		/// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
		/// </summary>
		protected override void m_mthClearRecordInfo()
		{

			//��վ����¼����			
			m_txtMostlyContent.m_mthClearText();
			m_txtOriginalDiagnose.m_mthClearText();
			m_txtDiagnoseThe.m_mthClearText();
			m_txtDiagnoseDiffe.m_mthClearText();		
			m_txtCurePlan.m_mthClearText();

			//Ĭ��ǩ��
			MDIParent.m_mthSetDefaulEmployee(lsvSign);
			
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
		
                //lblCreateDateTitle.Left=lblInHospitalCaseTitle.Left;//=16;
                //lblCreateDateTitle.Top=15;	
                //m_dtpCreateDate.Left=lblCreateDateTitle.Right+5;
                //m_dtpCreateDate.Top=lblCreateDateTitle.Top;
                //m_dtpCreateDate.Refresh();
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
			clsFirstIllnessNoteRecordContent objContent=new clsFirstIllnessNoteRecordContent();
            #region �Ƿ�����޺ۼ��޸�
            if (chkModifyWithoutMatk.Checked)
                objContent.m_intMarkStatus = 0;
            else
                objContent.m_intMarkStatus = 1;
            #endregion
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
            //    objContent.objSignerArr[i].m_strFORMID_VCHR="frmFirstIllnessNote";
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
				
			objContent.m_strMostlyContent_Right=m_txtMostlyContent.m_strGetRightText();	
			objContent.m_strMostlyContent=m_txtMostlyContent.Text;
			objContent.m_strMostlyContentXML=m_txtMostlyContent.m_strGetXmlText();					
			
			objContent.m_strOriginalDiagnose_Right=m_txtOriginalDiagnose.m_strGetRightText();	
			objContent.m_strOriginalDiagnose=m_txtOriginalDiagnose.Text;
			objContent.m_strOriginalDiagnoseXML=m_txtOriginalDiagnose.m_strGetXmlText();					
			
			objContent.m_strThereunderDiagnose_Right=m_txtDiagnoseThe.m_strGetRightText();	
			objContent.m_strThereunderDiagnose=m_txtDiagnoseThe.Text;
			objContent.m_strThereunderDiagnoseXML=m_txtDiagnoseThe.m_strGetXmlText();					
			
			objContent.m_strDiagnoseDiffe_Right=m_txtDiagnoseDiffe.m_strGetRightText();	
			objContent.m_strDiagnoseDiffe=m_txtDiagnoseDiffe.Text;
			objContent.m_strDiagnoseDiffeXML=m_txtDiagnoseDiffe.m_strGetXmlText();	
		
			objContent.m_strCurePlan_Right=m_txtCurePlan.m_strGetRightText();	
			objContent.m_strCurePlan=m_txtCurePlan.Text;
			objContent.m_strCurePlanXML=m_txtCurePlan.m_strGetXmlText();	
			
			
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
			clsFirstIllnessNoteRecordContent objContent=(clsFirstIllnessNoteRecordContent)p_objContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��

			m_txtMostlyContent.m_mthSetNewText(objContent.m_strMostlyContent,objContent.m_strMostlyContentXML);		
			m_txtOriginalDiagnose.m_mthSetNewText(objContent.m_strOriginalDiagnose,objContent.m_strOriginalDiagnoseXML);		
			m_txtDiagnoseThe.m_mthSetNewText(objContent.m_strThereunderDiagnose,objContent.m_strThereunderDiagnoseXML);		
			m_txtDiagnoseDiffe.m_mthSetNewText(objContent.m_strDiagnoseDiffe,objContent.m_strDiagnoseDiffeXML);	
			m_txtCurePlan.m_mthSetNewText(objContent.m_strCurePlan,objContent.m_strCurePlanXML);		
			
			#region ǩ������
			if (objContent.objSignerArr!=null)
            {
                m_mthAddSignToListView(lsvSign, objContent.objSignerArr);
                //lsvSign.Items.Clear();
                //for (int i = 0; i < objContent.objSignerArr.Length; i++)
                //{
                //    if (objContent.objSignerArr[i].controlName=="lsvSign")
                //    {
                //        ListViewItem lviNewItem=new ListViewItem(objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
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

		}

		public override int m_IntFormID
		{
			get
			{
				return 48;
			}
		}

		protected override void m_mthSetDeletedGUIFromContent(clsTrackRecordContent p_objContent)
		{
			if(p_objContent ==null)
				return;
			clsFirstIllnessNoteRecordContent objContent=(clsFirstIllnessNoteRecordContent)p_objContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��

			m_txtMostlyContent.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strMostlyContent,objContent.m_strMostlyContentXML);		
			m_txtOriginalDiagnose.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOriginalDiagnose,objContent.m_strOriginalDiagnoseXML);		
			m_txtDiagnoseThe.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strThereunderDiagnose,objContent.m_strThereunderDiagnoseXML);		
			m_txtDiagnoseDiffe.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strDiagnoseDiffe,objContent.m_strDiagnoseDiffeXML);	
			m_txtCurePlan.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strCurePlan,objContent.m_strCurePlanXML);

            if (objContent.objSignerArr != null)
            {
                m_mthAddSignToListView(lsvSign, objContent.objSignerArr);
            }
		}

		/// <summary>
		/// ��ȡ���̼�¼�������ʵ��
		/// </summary>
		/// <returns></returns>
		protected override clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
		{
			//��ȡ���̼�¼�������ʵ��
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.FirstIllnessNote);					
		}

		/// <summary>
		/// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
		{
			//��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
			clsFirstIllnessNoteRecordContent objContent=(clsFirstIllnessNoteRecordContent)p_objRecordContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
			m_txtMostlyContent.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strMostlyContent,objContent.m_strMostlyContentXML);		
			m_txtOriginalDiagnose.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strOriginalDiagnose,objContent.m_strOriginalDiagnoseXML);		
			m_txtDiagnoseThe.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strThereunderDiagnose,objContent.m_strThereunderDiagnoseXML);		
			m_txtDiagnoseDiffe.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strDiagnoseDiffe,objContent.m_strDiagnoseDiffeXML);	
			m_txtCurePlan.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strCurePlan,objContent.m_strCurePlanXML);		
			
		}
		

		// ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
		public override string m_strReloadFormTitle()
		{
			//���Ӵ�������ʵ��
			return	"�״β��̼�¼";
		}	
	
		/// <summary>
		/// ��ѡ����ڵ�ʱ,���������Ĭ��ֵ(���Ӵ�����Ҫ,������ʵ��)
		/// </summary>
		protected override void m_mthSelectRootNode()
		{
			
		}

		private void cmdConfirm_Click(object sender, System.EventArgs e)
		{
			if(m_lngSave() > 0)
			{
				this.DialogResult = DialogResult.Yes;
				this.Close();
			}
		}

		private void m_cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmDiseaseSummary_Load(object sender, System.EventArgs e)
		{
			//			m_cmdNewTemplate.Visible = true;
			this.m_dtpCreateDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
			this.m_dtpCreateDate.m_mthResetSize();

			m_txtMostlyContent.Focus();
		}

		


		/// <summary>
		/// ���ݸ���
		/// </summary>
		/// <param name="p_objSelectedPatient"></param>
		protected override void m_mthDataShare(clsPatient p_objSelectedPatient)
		{			
		}

		protected override void m_mthSetDefaultValue(clsPatient p_objPatient)
		{
			//��¼ʱ���סԺ����
			clsInPatientCaseHisoryDefaultValue [] objInPatientCaseDefaultValue = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmSelectedInDate.ToString());
            if (objInPatientCaseDefaultValue != null && objInPatientCaseDefaultValue.Length > 0)
            {
                m_dtpCreateDate.Value = DateTime.Parse(objInPatientCaseDefaultValue[0].m_strCreateDate);
                m_dtpCreateDate.Refresh();
            }

			//Ĭ��ֵ
			new clsDefaultValueTool(this,p_objPatient).m_mthSetDefaultValue();
			//���϶˿ռ���
			m_txtMostlyContent.m_mthInsertText("    ",0);
			
			//�Զ�����ģ��
			m_mthSetSpecialPatientTemplateSet(p_objPatient);

			if(m_blnHaveAssociateTemplate)
			{
				//				int intIndex1 = m_txtRecordContent.Text.IndexOf("�������");
				//				int intIndex2 = m_txtRecordContent.Text.LastIndexOf("�������");
				//				if(intIndex1 != -1 && intIndex2 > intIndex1)
				//					m_txtRecordContent.Text = m_txtRecordContent.Text.Remove(intIndex1,intIndex2 - intIndex1);
			}
						

			//			//��ס�������ĸ���������
			//			string strTemplateSetID = m_objTemplateDomain.m_strGetPatientHaveDisease_TemplateSetID(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString(),this.Name,(int)enmAssociate.Disease);
			//			m_txtRecordContent.Tag = m_objTemplateDomain.m_strGetAssociateIDBySetID(strTemplateSetID,(int)enmAssociate.Operation);
		}

	 

	}
}
