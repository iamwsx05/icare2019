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
	/// ����ǰ���ۣ����̼�¼�Ӵ����ʵ��,Jacky-2003-5-19
	/// </summary>
	public class frmBeforeOperationDiscuss : iCare.frmDiseaseTrackBase
	{
		private PinkieControls.ButtonXP cmdConfirm;
		private System.Windows.Forms.Label lblDiscussContentTitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtDiscussContent;
		private System.Windows.Forms.Label lblAddressTitle;
		private com.digitalwave.controls.ctlRichTextBox m_txtAddress;
		private PinkieControls.ButtonXP m_cmdClose;
		private System.ComponentModel.IContainer components = null;
		private PinkieControls.ButtonXP m_cmdCompere;
		private PinkieControls.ButtonXP m_cmdAttendee;

		private clsEmployeeSignTool m_objSignTool;
		private PinkieControls.ButtonXP m_cmdAddress;
		private clsCommonUseToolCollection m_objCUTC;
		protected System.Windows.Forms.ListView lsvAttend;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		protected System.Windows.Forms.ListView lsvCompere;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		protected System.Windows.Forms.ListView lsvSign;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private PinkieControls.ButtonXP m_cmdEmployeeSign;

		//����ǩ����
		private clsEmrSignToolCollection m_objSign;

		public frmBeforeOperationDiscuss()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
            //ָ��ҽ������վ��
            intFormType = 1;
			// TODO: Add any initialization after the InitializeComponent call

 			cmdConfirm.Visible=false;
			
			m_mthSetRichTextBoxAttribInControl(this);

			this.Text="��ǰ���ۼ�¼";			
			this.m_lblForTitle.Text=this.Text;			
		
 
		 
			//ǩ������ֵ
			m_objSign = new clsEmrSignToolCollection();
			//m_mthBindEmployeeSign(��ť,ǩ����,ҽ��1or��ʿ2,�����֤trueorfalse);
			//��¼��
            m_objSign.m_mthBindEmployeeSign(m_cmdEmployeeSign, lsvSign, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
			//����
            m_objSign.m_mthBindEmployeeSign(m_cmdCompere, lsvCompere, 1, false, clsEMRLogin.LoginInfo.m_strEmpID);
			//�μ���Ա
            m_objSign.m_mthBindEmployeeSign(m_cmdAttendee, lsvAttend, 0, false, clsEMRLogin.LoginInfo.m_strEmpID);
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
            this.lblDiscussContentTitle = new System.Windows.Forms.Label();
            this.m_txtDiscussContent = new com.digitalwave.controls.ctlRichTextBox();
            this.lblAddressTitle = new System.Windows.Forms.Label();
            this.m_txtAddress = new com.digitalwave.controls.ctlRichTextBox();
            this.m_cmdClose = new PinkieControls.ButtonXP();
            this.m_cmdCompere = new PinkieControls.ButtonXP();
            this.m_cmdAttendee = new PinkieControls.ButtonXP();
            this.m_cmdEmployeeSign = new PinkieControls.ButtonXP();
            this.m_cmdAddress = new PinkieControls.ButtonXP();
            this.lsvAttend = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.lsvCompere = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.lsvSign = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_trvCreateDate
            // 
            this.m_trvCreateDate.ForeColor = System.Drawing.Color.Black;
            this.m_trvCreateDate.LineColor = System.Drawing.Color.Black;
            this.m_trvCreateDate.Location = new System.Drawing.Point(50, -42);
            this.m_trvCreateDate.Size = new System.Drawing.Size(100, 44);
            this.m_trvCreateDate.Visible = false;
            // 
            // lblCreateDateTitle
            // 
            this.lblCreateDateTitle.Location = new System.Drawing.Point(24, 35);
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.Location = new System.Drawing.Point(108, 31);
            // 
            // m_dtpGetDataTime
            // 
            this.m_dtpGetDataTime.Location = new System.Drawing.Point(352, 67);
            // 
            // m_lblGetDataTime
            // 
            this.m_lblGetDataTime.Location = new System.Drawing.Point(248, 67);
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(551, -20);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(529, -20);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(259, -20);
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(445, -20);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(363, -20);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(507, -20);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(477, -20);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(331, -21);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(50, -102);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(463, -25);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(411, -24);
            this.m_txtPatientName.Size = new System.Drawing.Size(84, 23);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(307, -24);
            this.m_txtBedNO.Size = new System.Drawing.Size(52, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(379, -25);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(28, -102);
            this.m_lsvPatientName.Size = new System.Drawing.Size(84, 104);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(22, -102);
            this.m_lsvBedNO.Size = new System.Drawing.Size(52, 104);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(80, -38);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(32, -30);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(704, -30);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.Location = new System.Drawing.Point(196, -66);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(156, -66);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(272, -62);
            this.m_lblForTitle.Text = "��ǰ���ۼ�¼";
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(27, 598);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(629, -28);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Location = new System.Drawing.Point(10, -71);
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
            this.cmdConfirm.Location = new System.Drawing.Point(720, 592);
            this.cmdConfirm.Name = "cmdConfirm";
            this.cmdConfirm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdConfirm.Size = new System.Drawing.Size(80, 30);
            this.cmdConfirm.TabIndex = 170;
            this.cmdConfirm.Text = "ȷ��";
            this.cmdConfirm.Click += new System.EventHandler(this.cmdConfirm_Click);
            // 
            // lblDiscussContentTitle
            // 
            this.lblDiscussContentTitle.AutoSize = true;
            this.lblDiscussContentTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblDiscussContentTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDiscussContentTitle.ForeColor = System.Drawing.Color.Black;
            this.lblDiscussContentTitle.Location = new System.Drawing.Point(24, 195);
            this.lblDiscussContentTitle.Name = "lblDiscussContentTitle";
            this.lblDiscussContentTitle.Size = new System.Drawing.Size(70, 14);
            this.lblDiscussContentTitle.TabIndex = 6091;
            this.lblDiscussContentTitle.Text = "��������:";
            // 
            // m_txtDiscussContent
            // 
            this.m_txtDiscussContent.AccessibleDescription = "��������";
            this.m_txtDiscussContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtDiscussContent.BackColor = System.Drawing.Color.White;
            this.m_txtDiscussContent.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDiscussContent.ForeColor = System.Drawing.Color.Black;
            this.m_txtDiscussContent.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtDiscussContent.Location = new System.Drawing.Point(108, 191);
            this.m_txtDiscussContent.m_BlnIgnoreUserInfo = false;
            this.m_txtDiscussContent.m_BlnPartControl = false;
            this.m_txtDiscussContent.m_BlnReadOnly = false;
            this.m_txtDiscussContent.m_BlnUnderLineDST = false;
            this.m_txtDiscussContent.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtDiscussContent.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtDiscussContent.m_IntCanModifyTime = 6;
            this.m_txtDiscussContent.m_IntPartControlLength = 0;
            this.m_txtDiscussContent.m_IntPartControlStartIndex = 0;
            this.m_txtDiscussContent.m_StrUserID = "";
            this.m_txtDiscussContent.m_StrUserName = "";
            this.m_txtDiscussContent.Name = "m_txtDiscussContent";
            this.m_txtDiscussContent.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtDiscussContent.Size = new System.Drawing.Size(776, 355);
            this.m_txtDiscussContent.TabIndex = 140;
            this.m_txtDiscussContent.Text = "";
            // 
            // lblAddressTitle
            // 
            this.lblAddressTitle.AutoSize = true;
            this.lblAddressTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.lblAddressTitle.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAddressTitle.ForeColor = System.Drawing.SystemColors.Window;
            this.lblAddressTitle.Location = new System.Drawing.Point(232, -9);
            this.lblAddressTitle.Name = "lblAddressTitle";
            this.lblAddressTitle.Size = new System.Drawing.Size(48, 16);
            this.lblAddressTitle.TabIndex = 6089;
            this.lblAddressTitle.Text = "�ص�:";
            this.lblAddressTitle.Visible = false;
            // 
            // m_txtAddress
            // 
            this.m_txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtAddress.BackColor = System.Drawing.Color.White;
            this.m_txtAddress.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtAddress.ForeColor = System.Drawing.Color.Black;
            this.m_txtAddress.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtAddress.Location = new System.Drawing.Point(108, 67);
            this.m_txtAddress.m_BlnIgnoreUserInfo = false;
            this.m_txtAddress.m_BlnPartControl = false;
            this.m_txtAddress.m_BlnReadOnly = false;
            this.m_txtAddress.m_BlnUnderLineDST = false;
            this.m_txtAddress.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtAddress.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtAddress.m_IntCanModifyTime = 6;
            this.m_txtAddress.m_IntPartControlLength = 0;
            this.m_txtAddress.m_IntPartControlStartIndex = 0;
            this.m_txtAddress.m_StrUserID = "";
            this.m_txtAddress.m_StrUserName = "";
            this.m_txtAddress.Multiline = false;
            this.m_txtAddress.Name = "m_txtAddress";
            this.m_txtAddress.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtAddress.Size = new System.Drawing.Size(780, 28);
            this.m_txtAddress.TabIndex = 100;
            this.m_txtAddress.Text = "";
            // 
            // m_cmdClose
            // 
            this.m_cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdClose.DefaultScheme = true;
            this.m_cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdClose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdClose.Hint = "";
            this.m_cmdClose.Location = new System.Drawing.Point(808, 592);
            this.m_cmdClose.Name = "m_cmdClose";
            this.m_cmdClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdClose.Size = new System.Drawing.Size(80, 30);
            this.m_cmdClose.TabIndex = 171;
            this.m_cmdClose.Text = "ȡ��";
            this.m_cmdClose.Click += new System.EventHandler(this.m_cmdClose_Click);
            // 
            // m_cmdCompere
            // 
            this.m_cmdCompere.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdCompere.DefaultScheme = true;
            this.m_cmdCompere.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdCompere.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdCompere.Hint = "";
            this.m_cmdCompere.Location = new System.Drawing.Point(12, 156);
            this.m_cmdCompere.Name = "m_cmdCompere";
            this.m_cmdCompere.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCompere.Size = new System.Drawing.Size(92, 30);
            this.m_cmdCompere.TabIndex = 10000079;
            this.m_cmdCompere.Text = "�� �� ��:";
            // 
            // m_cmdAttendee
            // 
            this.m_cmdAttendee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdAttendee.DefaultScheme = true;
            this.m_cmdAttendee.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdAttendee.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdAttendee.Hint = "";
            this.m_cmdAttendee.Location = new System.Drawing.Point(12, 102);
            this.m_cmdAttendee.Name = "m_cmdAttendee";
            this.m_cmdAttendee.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdAttendee.Size = new System.Drawing.Size(92, 30);
            this.m_cmdAttendee.TabIndex = 10000078;
            this.m_cmdAttendee.Text = "�μ���Ա:";
            // 
            // m_cmdEmployeeSign
            // 
            this.m_cmdEmployeeSign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cmdEmployeeSign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdEmployeeSign.DefaultScheme = true;
            this.m_cmdEmployeeSign.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdEmployeeSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdEmployeeSign.Hint = "";
            this.m_cmdEmployeeSign.Location = new System.Drawing.Point(28, 550);
            this.m_cmdEmployeeSign.Name = "m_cmdEmployeeSign";
            this.m_cmdEmployeeSign.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdEmployeeSign.Size = new System.Drawing.Size(76, 30);
            this.m_cmdEmployeeSign.TabIndex = 10000081;
            this.m_cmdEmployeeSign.Tag = "1";
            this.m_cmdEmployeeSign.Text = "��¼��:";
            // 
            // m_cmdAddress
            // 
            this.m_cmdAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdAddress.DefaultScheme = true;
            this.m_cmdAddress.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdAddress.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdAddress.Hint = "";
            this.m_cmdAddress.Location = new System.Drawing.Point(12, 65);
            this.m_cmdAddress.Name = "m_cmdAddress";
            this.m_cmdAddress.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdAddress.Size = new System.Drawing.Size(92, 30);
            this.m_cmdAddress.TabIndex = 90;
            this.m_cmdAddress.Text = "�ص�:";
            // 
            // lsvAttend
            // 
            this.lsvAttend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvAttend.BackColor = System.Drawing.Color.White;
            this.lsvAttend.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.lsvAttend.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lsvAttend.ForeColor = System.Drawing.Color.Black;
            this.lsvAttend.FullRowSelect = true;
            this.lsvAttend.GridLines = true;
            this.lsvAttend.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvAttend.Location = new System.Drawing.Point(108, 103);
            this.lsvAttend.Name = "lsvAttend";
            this.lsvAttend.Size = new System.Drawing.Size(780, 48);
            this.lsvAttend.TabIndex = 10000082;
            this.lsvAttend.UseCompatibleStateImageBehavior = false;
            this.lsvAttend.View = System.Windows.Forms.View.SmallIcon;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 55;
            // 
            // lsvCompere
            // 
            this.lsvCompere.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvCompere.BackColor = System.Drawing.Color.White;
            this.lsvCompere.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lsvCompere.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lsvCompere.ForeColor = System.Drawing.Color.Black;
            this.lsvCompere.FullRowSelect = true;
            this.lsvCompere.GridLines = true;
            this.lsvCompere.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvCompere.Location = new System.Drawing.Point(108, 157);
            this.lsvCompere.Name = "lsvCompere";
            this.lsvCompere.Size = new System.Drawing.Size(320, 28);
            this.lsvCompere.TabIndex = 10000083;
            this.lsvCompere.UseCompatibleStateImageBehavior = false;
            this.lsvCompere.View = System.Windows.Forms.View.SmallIcon;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 55;
            // 
            // lsvSign
            // 
            this.lsvSign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvSign.BackColor = System.Drawing.Color.White;
            this.lsvSign.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lsvSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lsvSign.ForeColor = System.Drawing.Color.Black;
            this.lsvSign.FullRowSelect = true;
            this.lsvSign.GridLines = true;
            this.lsvSign.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvSign.Location = new System.Drawing.Point(108, 552);
            this.lsvSign.Name = "lsvSign";
            this.lsvSign.Size = new System.Drawing.Size(776, 28);
            this.lsvSign.TabIndex = 10000084;
            this.lsvSign.UseCompatibleStateImageBehavior = false;
            this.lsvSign.View = System.Windows.Forms.View.SmallIcon;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 55;
            // 
            // frmBeforeOperationDiscuss
            // 
            this.AccessibleDescription = "��ǰ���ۼ�¼";
            this.CancelButton = this.m_cmdClose;
            this.ClientSize = new System.Drawing.Size(916, 645);
            this.Controls.Add(this.lsvSign);
            this.Controls.Add(this.lsvCompere);
            this.Controls.Add(this.lsvAttend);
            this.Controls.Add(this.lblDiscussContentTitle);
            this.Controls.Add(this.lblAddressTitle);
            this.Controls.Add(this.m_cmdAddress);
            this.Controls.Add(this.m_cmdEmployeeSign);
            this.Controls.Add(this.m_cmdCompere);
            this.Controls.Add(this.m_cmdAttendee);
            this.Controls.Add(this.m_cmdClose);
            this.Controls.Add(this.m_txtDiscussContent);
            this.Controls.Add(this.m_txtAddress);
            this.Controls.Add(this.cmdConfirm);
            this.Name = "frmBeforeOperationDiscuss";
            this.Text = "��ǰ���ۼ�¼";
            this.Load += new System.EventHandler(this.frmBeforeOperationDiscuss_Load);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_lblGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_dtpGetDataTime, 0);
            this.Controls.SetChildIndex(this.cmdConfirm, 0);
            this.Controls.SetChildIndex(this.m_txtAddress, 0);
            this.Controls.SetChildIndex(this.m_txtDiscussContent, 0);
            this.Controls.SetChildIndex(this.m_cmdClose, 0);
            this.Controls.SetChildIndex(this.m_cmdAttendee, 0);
            this.Controls.SetChildIndex(this.m_cmdCompere, 0);
            this.Controls.SetChildIndex(this.m_cmdEmployeeSign, 0);
            this.Controls.SetChildIndex(this.m_cmdAddress, 0);
            this.Controls.SetChildIndex(this.m_dtpCreateDate, 0);
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
            this.Controls.SetChildIndex(this.lblAddressTitle, 0);
            this.Controls.SetChildIndex(this.lblDiscussContentTitle, 0);
            this.Controls.SetChildIndex(this.m_trvCreateDate, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.lsvAttend, 0);
            this.Controls.SetChildIndex(this.lsvCompere, 0);
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
			clsBeforeOperationDiscussInfo objTrackInfo = new clsBeforeOperationDiscussInfo();

			objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;//m_objGetContentFromGUI();
			//����m_strTitle��m_dtmRecordTime
			objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
			objTrackInfo.m_StrTitle = "��ǰ���ۼ�¼";			
			
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
			m_txtAddress.m_mthClearText();			
			m_txtDiscussContent.m_mthClearText();
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
			int intCompere=lsvCompere.Items.Count;
			int intAttend=lsvAttend.Items.Count;
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)				
				return null;
			if (intSignCount==0)
			{
				clsPublicFunction.ShowInformationMessageBox("������һ����¼Աǩ��!");
				return null;
			}

			if (intCompere==0)
			{
				clsPublicFunction.ShowInformationMessageBox("��������ǩ��!");
				return null;
			}
			if (intAttend==0)
			{
				clsPublicFunction.ShowInformationMessageBox("������һ���μ���Աǩ��!");
				return null;
			}
			//�ӽ����ȡ��ֵ
			clsBeforeOperationDiscussRecordContent objContent=new clsBeforeOperationDiscussRecordContent();

			//��ȡlsvsignǩ��
			objContent.objSignerArr=new clsEmrSigns_VO[intSignCount+intCompere+intAttend];
			strUserIDList="";
			strUserNameList="";
            m_mthGetSignArr(new Control[] {lsvSign,lsvCompere,lsvAttend }, ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);
            //for (int i = 0; i < intSignCount; i++)
            //{
            //    objContent.objSignerArr[i]=new clsEmrSigns_VO();
            //    objContent.objSignerArr[i].objEmployee=new clsEmrEmployeeBase_VO();
            //    objContent.objSignerArr[i].objEmployee=(clsEmrEmployeeBase_VO)( lsvSign.Items[i].Tag);
            //    objContent.objSignerArr[i].controlName="lsvSign";
            //    objContent.objSignerArr[i].m_strFORMID_VCHR="frmBeforeOperationDiscuss";//ע���Сд
            //    objContent.objSignerArr[i].m_strREGISTERID_CHR=com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            //    //�ۼ���ʽ 0972,0324,
            //    strUserIDList=strUserIDList+objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR.Trim()+",";
            //    strUserNameList=strUserNameList+objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR.Trim()+",";
            //}
			objContent.m_strModifyUserID=strUserIDList;
			//��ȡ������ǩ��
            //for (int i = 0; i < intCompere; i++)
            //{
            //    objContent.objSignerArr[intSignCount+i]=new clsEmrSigns_VO();
            //    objContent.objSignerArr[intSignCount+i].objEmployee=new clsEmrEmployeeBase_VO();
            //    objContent.objSignerArr[intSignCount+i].objEmployee=(clsEmrEmployeeBase_VO)( lsvCompere.Items[i].Tag);
            //    objContent.objSignerArr[intSignCount+i].controlName="lsvCompere";
            //    objContent.objSignerArr[intSignCount+i].m_strFORMID_VCHR="frmBeforeOperationDiscuss";//ע���Сд
            //    objContent.objSignerArr[intSignCount+i].m_strREGISTERID_CHR=com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            //}
            ////��ȡ�μ���Աǩ��
            //for (int i = 0; i < intAttend; i++)
            //{
            //    objContent.objSignerArr[intSignCount+intCompere+i]=new clsEmrSigns_VO();
            //    objContent.objSignerArr[intSignCount+intCompere+i].objEmployee=new clsEmrEmployeeBase_VO();
            //    objContent.objSignerArr[intSignCount+intCompere+i].objEmployee=(clsEmrEmployeeBase_VO)( lsvAttend.Items[i].Tag);
            //    objContent.objSignerArr[intSignCount+intCompere+i].controlName="lsvAttend";
            //    objContent.objSignerArr[intSignCount+intCompere+i].m_strFORMID_VCHR="frmBeforeOperationDiscuss";//ע���Сд
            //    objContent.objSignerArr[intSignCount+intCompere+i].m_strREGISTERID_CHR=com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            //}
			
			//����Richtextbox��modifyuserID ��modifyuserName
			m_mthSetRichTextBoxAttribInControlWithIDandName(this);
			#region �Ƿ�����޺ۼ��޸�
			if (chkModifyWithoutMatk.Checked)
				objContent.m_intMarkStatus=0;
			else
				objContent.m_intMarkStatus=1;
			#endregion
 			
			objContent.m_dtmCreateDate=m_dtpCreateDate.Value;					
				
			objContent.m_strAddress_Right=m_txtAddress.m_strGetRightText();	
			objContent.m_strAddress=m_txtAddress.Text;
			objContent.m_strAddressXML=m_txtAddress.m_strGetXmlText();					
			
			objContent.m_strDiscussContent_Right=m_txtDiscussContent.m_strGetRightText();	
			objContent.m_strDiscussContent=m_txtDiscussContent.Text;
			objContent.m_strDiscussContentXML=m_txtDiscussContent.m_strGetXmlText();			
			


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
			clsBeforeOperationDiscussRecordContent objContent=(clsBeforeOperationDiscussRecordContent)p_objContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
			m_txtAddress.m_mthSetNewText(objContent.m_strAddress,objContent.m_strAddressXML);		
			m_txtDiscussContent.m_mthSetNewText(objContent.m_strDiscussContent,objContent.m_strDiscussContentXML);		
			
			#region ǩ������
			//��¼ǩ��
			if (objContent.objSignerArr!=null)
			{
                m_mthAddSignToListView(lsvSign, objContent.objSignerArr);
                m_mthAddSignToListView(lsvCompere, objContent.objSignerArr);
                m_mthAddSignToListView(lsvAttend, objContent.objSignerArr);
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
			//����ǩ��
            //if (objContent.objSignerArr!=null)
            //{
            //    lsvCompere.Items.Clear();
            //    for (int i = 0; i < objContent.objSignerArr.Length; i++)
            //    {
            //        if (objContent.objSignerArr[i].controlName=="lsvCompere")
            //        {
            //            ListViewItem lviNewItem=new ListViewItem(objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
            //            //ID ����ظ���
            //            lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR);
            //            //���� ������
            //            lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
            //            //tag��Ϊ����
            //            lviNewItem.Tag=objContent.objSignerArr[i].objEmployee;
            //            //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
            //            lsvCompere.Items.Add(lviNewItem);
            //        }
            //    }
            //}
            ////�μ���ǩ��
            //if (objContent.objSignerArr!=null)
            //{
            //    lsvAttend.Items.Clear();
            //    for (int i = 0; i < objContent.objSignerArr.Length; i++)
            //    {
            //        if (objContent.objSignerArr[i].controlName=="lsvAttend")
            //        {
            //            ListViewItem lviNewItem=new 						 ListViewItem(objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
            //            //ID ����ظ���
            //            lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR);
            //            //���� ������
            //            lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
            //            //tag��Ϊ����
            //            lviNewItem.Tag=objContent.objSignerArr[i].objEmployee;
            //            //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
            //            lsvAttend.Items.Add(lviNewItem);
            //        }
            //    }
            //}
			#endregion ǩ��		
		}

		public override int m_IntFormID
		{
			get
			{
				return 12;
			}
		}

		protected override void m_mthSetDeletedGUIFromContent(clsTrackRecordContent p_objContent)
		{
			if(p_objContent ==null)
				return;
			clsBeforeOperationDiscussRecordContent objContent=(clsBeforeOperationDiscussRecordContent)p_objContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
			m_txtAddress.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strAddress,objContent.m_strAddressXML);		
			m_txtDiscussContent.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strDiscussContent,objContent.m_strDiscussContentXML);

            if (objContent.objSignerArr != null)
            {
                m_mthAddSignToListView(lsvSign, objContent.objSignerArr);
                m_mthAddSignToListView(lsvCompere, objContent.objSignerArr);
                m_mthAddSignToListView(lsvAttend, objContent.objSignerArr);
            }
		}

		/// <summary>
		/// ��ȡ���̼�¼�������ʵ��
		/// </summary>
		/// <returns></returns>
		protected override clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
		{
			//��ȡ���̼�¼�������ʵ��
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.BeforeOperationDiscuss);					
		}

		/// <summary>
		/// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
		{
			//��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
			clsBeforeOperationDiscussRecordContent objContent=(clsBeforeOperationDiscussRecordContent)p_objRecordContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
			m_txtAddress.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strAddress,objContent.m_strAddressXML);		
			m_txtDiscussContent.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strDiscussContent,objContent.m_strDiscussContentXML);		
			#region ǩ������

            strUserIDList = "";
            strUserNameList = "";
            m_mthGetSignArr(new Control[] { lsvSign, lsvCompere, lsvAttend }, ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);
			//��¼ǩ��
            //if (objContent.objSignerArr!=null)
            //{
            //    lsvSign.Items.Clear();
            //    for (int i = 0; i < objContent.objSignerArr.Length; i++)
            //    {
            //        if (objContent.objSignerArr[i].controlName=="lsvSign")
            //        {
            //            ListViewItem lviNewItem = new ListViewItem(objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName);
            //            //ID ����ظ���
            //            lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR);
            //            //���� ������
            //            lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
            //            //tag��Ϊ����
            //            lviNewItem.Tag=objContent.objSignerArr[i].objEmployee;
            //            //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
            //            lsvSign.Items.Add(lviNewItem);
            //        }
            //    }
            //}
            ////����ǩ��
            //if (objContent.objSignerArr!=null)
            //{
            //    lsvCompere.Items.Clear();
            //    for (int i = 0; i < objContent.objSignerArr.Length; i++)
            //    {
            //        if (objContent.objSignerArr[i].controlName=="lsvCompere")
            //        {
            //            ListViewItem lviNewItem=new ListViewItem(objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
            //            //ID ����ظ���
            //            lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR);
            //            //���� ������
            //            lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
            //            //tag��Ϊ����
            //            lviNewItem.Tag=objContent.objSignerArr[i].objEmployee;
            //            //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
            //            lsvCompere.Items.Add(lviNewItem);
            //        }
            //    }
            //}
            ////�μ���ǩ��
            //if (objContent.objSignerArr!=null)
            //{
            //    lsvAttend.Items.Clear();
            //    for (int i = 0; i < objContent.objSignerArr.Length; i++)
            //    {
            //        if (objContent.objSignerArr[i].controlName=="lsvAttend")
            //        {
            //            ListViewItem lviNewItem=new 						 ListViewItem(objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
            //            //ID ����ظ���
            //            lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strEMPID_CHR);
            //            //���� ������
            //            lviNewItem.SubItems.Add(objContent.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
            //            //tag��Ϊ����
            //            lviNewItem.Tag=objContent.objSignerArr[i].objEmployee;
            //            //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
            //            lsvAttend.Items.Add(lviNewItem);
            //        }
            //    }
            //}
			#endregion ǩ��		
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
			return	"��ǰ���ۼ�¼";
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

		private void frmBeforeOperationDiscuss_Load(object sender, System.EventArgs e)
		{
//			m_cmdNewTemplate.Left=cmdConfirm.Left-m_cmdNewTemplate.Width+(cmdConfirm.Right-m_cmdClose.Left);
//			m_cmdNewTemplate.Top=cmdConfirm.Top;
//			m_cmdNewTemplate.Visible=true;

			this.m_dtpCreateDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
			this.m_dtpCreateDate.m_mthResetSize();

			m_txtAddress.Focus();
		}

		private void m_cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void m_lsvEmployee_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Delete:
					clsEmployeeSignTool.s_mthDeleteListViewItem((ListView)sender);
					break;
			}
		}
		
	}
}

