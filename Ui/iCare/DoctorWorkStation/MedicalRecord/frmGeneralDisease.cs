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
using System.Text;

namespace iCare
{
	/// <summary>
	/// ��һ�㣩���̼�¼�Ӵ����ʵ��,Jacky-2003-5-12
	/// </summary>
	public class frmGeneralDisease : frmDiseaseTrackBase
	{
        private com.digitalwave.controls.ctlRichTextBox m_txtRecordContent;
		private System.Windows.Forms.Label lblTitle1;
		private System.Windows.Forms.Label lblTitle3;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboRecordTitleType;
		private System.Windows.Forms.ColumnHeader clmEmployeeName;
		private PinkieControls.ButtonXP cmdConfirm;
		private System.Windows.Forms.Label lblRecordTitleTypeTitle;
		private PinkieControls.ButtonXP m_cmdClose;
		private System.Windows.Forms.MenuItem mniLabCheckResult;
		private System.ComponentModel.IContainer components = null;
		private clsCommonUseToolCollection m_objCUTC;
		private System.Windows.Forms.MenuItem m_mniOperation;
		private ListBox m_lstOperation;

		private clsEmployeeSignTool m_objSignTool;

		private clsElectronIdiographDomain m_objElectronIdiographDomain;
		protected System.Windows.Forms.ListView lsvSign;
		private string m_strOpenDateRecord = "";
		private PinkieControls.ButtonXP m_cmdEmployeeSign;
        private ctlComboBox m_txtRecordTitle;

		//����ǩ����
		private clsEmrSignToolCollection m_objSign;
        /// <summary>
        /// �Ƿ������ҽԺ
        /// </summary>
        private bool m_blnIsNanNing = true;


		public frmGeneralDisease()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
            //ָ��ҽ������վ��
            intFormType = 1;
			cmdConfirm.Visible=false;
            m_cboDept.Visible = false;
            if (clsEMRLogin.m_StrCurrentHospitalNO == "450101001")//����
            {
                m_txtRecordTitle.Visible = true;
                m_cboRecordTitleType.Visible = false;
                m_blnIsNanNing = true;

                m_txtRecordTitle.AddItem("���̼�¼");
                m_txtRecordTitle.AddItem("������¼");
                m_txtRecordTitle.AddItem("�ش���¼");
                m_txtRecordTitle.AddItem("�Ǵ���¼");
                m_txtRecordTitle.AddItem("������¼");
                m_txtRecordTitle.AddItem("�������Ƽ�¼");
                m_txtRecordTitle.AddItem("����¼");
                m_txtRecordTitle.AddItem("��������¼");
                m_txtRecordTitle.AddItem("θ������¼");
                m_txtRecordTitle.AddItem("��֧������¼");
                m_txtRecordTitle.AddItem("�������Ʋ�����¼");

                m_cboRecordTitleType.AddItem("���̼�¼");
                m_cboRecordTitleType.AddItem("�����¼");
            }
            else
            {
                m_txtRecordTitle.Visible = false;
                m_cboRecordTitleType.Visible = true;
                m_blnIsNanNing = false;
                m_cboRecordTitleType.AddItem("");
                m_cboRecordTitleType.AddItem("���̼�¼");
                m_cboRecordTitleType.AddItem("�����¼");
                m_cboRecordTitleType.AddItem("�Ӱ��¼");
                //			m_cboRecordTitleType.AddItem("�����¼");
                m_cboRecordTitleType.AddItem("ת����¼");
                m_cboRecordTitleType.AddItem("ת���¼");
                m_cboRecordTitleType.AddItem("�׶�С��");
                m_cboRecordTitleType.AddItem("���β鷿��¼");
                m_cboRecordTitleType.AddItem("�����β鷿��¼");
                m_cboRecordTitleType.AddItem("����ҽʦ�鷿��¼"); // �¼�
                m_cboRecordTitleType.AddItem("ҽʦ�鷿��¼");
                m_cboRecordTitleType.AddItem("���Ѳ������ۼ�¼");
                m_cboRecordTitleType.AddItem("��ǰ���ۼ�¼");
                m_cboRecordTitleType.AddItem("�����������ۼ�¼");
                m_cboRecordTitleType.AddItem("�����󲡳̼�¼");
                m_cboRecordTitleType.AddItem("������¼");
                //			m_cboRecordTitleType.AddItem("��Ժ��¼");
                m_cboRecordTitleType.AddItem("���ȼ�¼");
                m_cboRecordTitleType.AddItem("���ھ�����������¼");
                m_cboRecordTitleType.AddItem("��ά֧���ܾ���̵+��ϴ����¼");
                m_cboRecordTitleType.AddItem("������¼");
            }

            // ����RichTextBox���ԡ����Ҽ��˵����û��������û�ID������������ɫ��˫������ɫ��
            m_mthSetRichTextBoxAttribInControl(this);

			//ǩ������ֵ
			m_objSign = new clsEmrSignToolCollection();
			//m_mthBindEmployeeSign(��ť,ǩ����,ҽ��1or��ʿ2,�����֤trueorfalse);
            m_objSign.m_mthBindEmployeeSign(m_cmdEmployeeSign, lsvSign, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);

			//m_objElectronIdiographDomain = new clsElectronIdiographDomain(this.Name);
		}

		#region ��������
		/// <summary>
		/// ��ʼ���������Ƶ�ListBox
		/// </summary>
		private void m_mthInitListBox()
		{
			this.m_lstOperation = new ListBox();

			this.m_lstOperation.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.m_lstOperation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_lstOperation.Font =new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_lstOperation.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.m_lstOperation.ItemHeight = 25;
			this.m_lstOperation.Location = new System.Drawing.Point(10, 10);
			this.m_lstOperation.Name = "m_lstOperation";
			this.m_lstOperation.Size = new System.Drawing.Size(222, 200);
			this.m_lstOperation.TabIndex = 15006;
			this.m_lstOperation.BringToFront ();
			this.m_lstOperation.Visible = false;
			this.m_lstOperation.HorizontalScrollbar = true;

			m_lstOperation.DoubleClick += new EventHandler(m_lstOperation_DoubleClick);
			m_lstOperation.Leave += new EventHandler(m_lstOperation_Leave);
			
			//��������Control������ʾ
			this.Controls.Add(m_lstOperation);
		}

		private void m_lstOperation_DoubleClick(object sender,EventArgs e)
		{
			ListBox lstOperation = (ListBox)sender;
			if(lstOperation.SelectedItems.Count > 0 && lstOperation.SelectedItems[0] != null)
			{
				m_txtRecordContent.Tag = ((clsTemplateSet_Associate)lstOperation.SelectedItems[0]).strAssociateID;
			}
			m_txtRecordContent.m_mthInsertText(((clsTemplateSet_Associate)lstOperation.SelectedItems[0]).strAssociateName,m_txtRecordContent.SelectionStart);

			lstOperation.Visible = false;
			m_txtRecordContent.Focus();
		}

		private void m_lstOperation_Leave(object sender, System.EventArgs e)
		{
			m_lstOperation.Visible = false;
			m_txtRecordContent.Focus();
		}

		private void m_mniOperation_Click(object sender, System.EventArgs e)
		{
			m_lstOperation.Items.Clear();

			clsTemplateSet_Associate[] m_objArr;
			new clsTemplateDomain().m_lngGetAllAssociate((int)enmAssociate.Operation,out m_objArr);
			if(m_objArr!=null && m_objArr.Length>0)
			{
				m_lstOperation.Items.AddRange(m_objArr);
			}

			System.Drawing.Point pt= m_txtRecordContent.GetPositionFromCharIndex(m_txtRecordContent.SelectionStart );

			m_lstOperation.Left = m_txtRecordContent.Left + pt.X;
			m_lstOperation.Top = m_txtRecordContent.Top + pt.Y + 20;
			
			m_lstOperation.Visible = true;
			m_lstOperation.BringToFront();
			if(m_lstOperation.Items.Count > 0)
				m_lstOperation.SelectedIndex = 0;
			m_lstOperation.Focus();
		}
		#endregion

		public override int m_IntFormID
		{
			get
			{
				return 0;
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
            this.m_txtRecordContent = new com.digitalwave.controls.ctlRichTextBox();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.lblRecordTitleTypeTitle = new System.Windows.Forms.Label();
            this.m_cboRecordTitleType = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.lsvSign = new System.Windows.Forms.ListView();
            this.clmEmployeeName = new System.Windows.Forms.ColumnHeader();
            this.cmdConfirm = new PinkieControls.ButtonXP();
            this.m_cmdClose = new PinkieControls.ButtonXP();
            this.mniLabCheckResult = new System.Windows.Forms.MenuItem();
            this.m_cmdEmployeeSign = new PinkieControls.ButtonXP();
            this.m_mniOperation = new System.Windows.Forms.MenuItem();
            this.m_txtRecordTitle = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_trvCreateDate
            // 
            this.m_trvCreateDate.LineColor = System.Drawing.Color.Black;
            this.m_trvCreateDate.Location = new System.Drawing.Point(174, 150);
            this.m_trvCreateDate.Size = new System.Drawing.Size(68, 24);
            this.m_trvCreateDate.Visible = false;
            // 
            // lblCreateDateTitle
            // 
            this.lblCreateDateTitle.Location = new System.Drawing.Point(20, 12);
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.Location = new System.Drawing.Point(92, 8);
            this.m_dtpCreateDate.TabIndex = 112;
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(668, 8);
            this.lblSex.Size = new System.Drawing.Size(32, 19);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(752, 8);
            this.lblAge.Size = new System.Drawing.Size(28, 19);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(182, 102);
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(332, 8);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(131, 106);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(360, 4);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(704, 8);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(262, 102);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(428, 0);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(76, 104);
            this.m_lsvInPatientID.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(544, 32);
            this.m_lsvPatientName.Size = new System.Drawing.Size(76, 104);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(428, -24);
            this.m_lsvBedNO.Size = new System.Drawing.Size(76, 104);
            this.m_lsvBedNO.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(160, 120);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(504, 208);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(396, 4);
            this.m_lblForTitle.Text = "���̼�¼";
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(16, 412);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(631, 76);
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            // 
            // m_txtRecordContent
            // 
            this.m_txtRecordContent.AccessibleDescription = "��¼����";
            this.m_txtRecordContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtRecordContent.BackColor = System.Drawing.Color.White;
            this.m_txtRecordContent.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtRecordContent.ForeColor = System.Drawing.Color.Black;
            this.m_txtRecordContent.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtRecordContent.Location = new System.Drawing.Point(16, 57);
            this.m_txtRecordContent.m_BlnIgnoreUserInfo = false;
            this.m_txtRecordContent.m_BlnPartControl = false;
            this.m_txtRecordContent.m_BlnReadOnly = false;
            this.m_txtRecordContent.m_BlnUnderLineDST = false;
            this.m_txtRecordContent.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtRecordContent.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtRecordContent.m_IntCanModifyTime = 500;
            this.m_txtRecordContent.m_IntPartControlLength = 0;
            this.m_txtRecordContent.m_IntPartControlStartIndex = 0;
            this.m_txtRecordContent.m_StrUserID = "";
            this.m_txtRecordContent.m_StrUserName = "";
            this.m_txtRecordContent.Name = "m_txtRecordContent";
            this.m_txtRecordContent.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtRecordContent.Size = new System.Drawing.Size(772, 339);
            this.m_txtRecordContent.TabIndex = 120;
            this.m_txtRecordContent.Text = "";
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle1.Location = new System.Drawing.Point(404, 12);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(42, 14);
            this.lblTitle1.TabIndex = 6074;
            this.lblTitle1.Text = "����:";
            // 
            // lblTitle3
            // 
            this.lblTitle3.AutoSize = true;
            this.lblTitle3.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle3.Location = new System.Drawing.Point(20, 40);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(70, 14);
            this.lblTitle3.TabIndex = 6076;
            this.lblTitle3.Text = "��¼����:";
            // 
            // lblRecordTitleTypeTitle
            // 
            this.lblRecordTitleTypeTitle.AutoSize = true;
            this.lblRecordTitleTypeTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRecordTitleTypeTitle.Location = new System.Drawing.Point(67, 160);
            this.lblRecordTitleTypeTitle.Name = "lblRecordTitleTypeTitle";
            this.lblRecordTitleTypeTitle.Size = new System.Drawing.Size(70, 14);
            this.lblRecordTitleTypeTitle.TabIndex = 6077;
            this.lblRecordTitleTypeTitle.Text = "��    ��:";
            this.lblRecordTitleTypeTitle.Visible = false;
            // 
            // m_cboRecordTitleType
            // 
            this.m_cboRecordTitleType.BackColor = System.Drawing.Color.White;
            this.m_cboRecordTitleType.BorderColor = System.Drawing.Color.Black;
            this.m_cboRecordTitleType.DropButtonBackColor = System.Drawing.Color.Gainsboro;
            this.m_cboRecordTitleType.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboRecordTitleType.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboRecordTitleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboRecordTitleType.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboRecordTitleType.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboRecordTitleType.ForeColor = System.Drawing.Color.Black;
            this.m_cboRecordTitleType.ListBackColor = System.Drawing.Color.White;
            this.m_cboRecordTitleType.ListForeColor = System.Drawing.Color.Black;
            this.m_cboRecordTitleType.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboRecordTitleType.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboRecordTitleType.Location = new System.Drawing.Point(450, 7);
            this.m_cboRecordTitleType.m_BlnEnableItemEventMenu = false;
            this.m_cboRecordTitleType.Name = "m_cboRecordTitleType";
            this.m_cboRecordTitleType.SelectedIndex = -1;
            this.m_cboRecordTitleType.SelectedItem = null;
            this.m_cboRecordTitleType.SelectionStart = 0;
            this.m_cboRecordTitleType.Size = new System.Drawing.Size(287, 23);
            this.m_cboRecordTitleType.TabIndex = 110;
            this.m_cboRecordTitleType.TextBackColor = System.Drawing.Color.White;
            this.m_cboRecordTitleType.TextForeColor = System.Drawing.Color.Black;
            this.m_cboRecordTitleType.Visible = false;
            this.m_cboRecordTitleType.SelectedValueChanged += new System.EventHandler(this.m_cboRecordTitleType_SelectedValueChanged);
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
            this.lsvSign.Location = new System.Drawing.Point(172, 410);
            this.lsvSign.Name = "lsvSign";
            this.lsvSign.Size = new System.Drawing.Size(424, 28);
            this.lsvSign.TabIndex = 140;
            this.lsvSign.UseCompatibleStateImageBehavior = false;
            this.lsvSign.View = System.Windows.Forms.View.SmallIcon;
            this.lsvSign.SelectedIndexChanged += new System.EventHandler(this.lsvSign_SelectedIndexChanged);
            // 
            // clmEmployeeName
            // 
            this.clmEmployeeName.Width = 55;
            // 
            // cmdConfirm
            // 
            this.cmdConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdConfirm.DefaultScheme = true;
            this.cmdConfirm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdConfirm.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdConfirm.Hint = "";
            this.cmdConfirm.Location = new System.Drawing.Point(616, 408);
            this.cmdConfirm.Name = "cmdConfirm";
            this.cmdConfirm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdConfirm.Size = new System.Drawing.Size(72, 30);
            this.cmdConfirm.TabIndex = 200;
            this.cmdConfirm.Text = "ȷ��";
            this.cmdConfirm.Click += new System.EventHandler(this.cmdConfirm_Click);
            // 
            // m_cmdClose
            // 
            this.m_cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdClose.DefaultScheme = true;
            this.m_cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdClose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdClose.Hint = "";
            this.m_cmdClose.Location = new System.Drawing.Point(704, 408);
            this.m_cmdClose.Name = "m_cmdClose";
            this.m_cmdClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdClose.Size = new System.Drawing.Size(72, 30);
            this.m_cmdClose.TabIndex = 201;
            this.m_cmdClose.Text = "ȡ��";
            this.m_cmdClose.Click += new System.EventHandler(this.m_cmdClose_Click);
            // 
            // mniLabCheckResult
            // 
            this.mniLabCheckResult.Index = -1;
            this.mniLabCheckResult.Text = "�� �� �� ��";
            this.mniLabCheckResult.Visible = false;
            this.mniLabCheckResult.Click += new System.EventHandler(this.mniLabCheckResult_Click);
            // 
            // m_cmdEmployeeSign
            // 
            this.m_cmdEmployeeSign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cmdEmployeeSign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdEmployeeSign.DefaultScheme = true;
            this.m_cmdEmployeeSign.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdEmployeeSign.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdEmployeeSign.Hint = "";
            this.m_cmdEmployeeSign.Location = new System.Drawing.Point(108, 408);
            this.m_cmdEmployeeSign.Name = "m_cmdEmployeeSign";
            this.m_cmdEmployeeSign.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdEmployeeSign.Size = new System.Drawing.Size(58, 30);
            this.m_cmdEmployeeSign.TabIndex = 125;
            this.m_cmdEmployeeSign.Tag = "1";
            this.m_cmdEmployeeSign.Text = "ǩ��:";
            // 
            // m_mniOperation
            // 
            this.m_mniOperation.Index = -1;
            this.m_mniOperation.Text = "��������";
            this.m_mniOperation.Click += new System.EventHandler(this.m_mniOperation_Click);
            // 
            // m_txtRecordTitle
            // 
            this.m_txtRecordTitle.BackColor = System.Drawing.Color.White;
            this.m_txtRecordTitle.BorderColor = System.Drawing.Color.Black;
            this.m_txtRecordTitle.DropButtonBackColor = System.Drawing.Color.Gainsboro;
            this.m_txtRecordTitle.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_txtRecordTitle.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_txtRecordTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_txtRecordTitle.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtRecordTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtRecordTitle.ForeColor = System.Drawing.Color.Black;
            this.m_txtRecordTitle.ListBackColor = System.Drawing.Color.White;
            this.m_txtRecordTitle.ListForeColor = System.Drawing.Color.Black;
            this.m_txtRecordTitle.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_txtRecordTitle.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_txtRecordTitle.Location = new System.Drawing.Point(450, 7);
            this.m_txtRecordTitle.m_BlnEnableItemEventMenu = false;
            this.m_txtRecordTitle.Name = "m_txtRecordTitle";
            this.m_txtRecordTitle.SelectedIndex = -1;
            this.m_txtRecordTitle.SelectedItem = null;
            this.m_txtRecordTitle.SelectionStart = 0;
            this.m_txtRecordTitle.Size = new System.Drawing.Size(287, 23);
            this.m_txtRecordTitle.TabIndex = 10000005;
            this.m_txtRecordTitle.TextBackColor = System.Drawing.Color.White;
            this.m_txtRecordTitle.TextForeColor = System.Drawing.Color.Black;
            // 
            // frmGeneralDisease
            // 
            this.AccessibleDescription = "���̼�¼";
            this.AutoScroll = false;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(794, 447);
            this.Controls.Add(this.m_txtRecordTitle);
            this.Controls.Add(this.lblTitle1);
            this.Controls.Add(this.m_txtRecordContent);
            this.Controls.Add(this.lblTitle3);
            this.Controls.Add(this.m_cmdEmployeeSign);
            this.Controls.Add(this.m_cmdClose);
            this.Controls.Add(this.cmdConfirm);
            this.Controls.Add(this.lblRecordTitleTypeTitle);
            this.Controls.Add(this.lsvSign);
            this.Controls.Add(this.m_cboRecordTitleType);
            this.Name = "frmGeneralDisease";
            this.Text = "���̼�¼";
            this.Load += new System.EventHandler(this.frmGeneralDisease_Load);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_cboRecordTitleType, 0);
            this.Controls.SetChildIndex(this.lsvSign, 0);
            this.Controls.SetChildIndex(this.lblRecordTitleTypeTitle, 0);
            this.Controls.SetChildIndex(this.cmdConfirm, 0);
            this.Controls.SetChildIndex(this.m_cmdClose, 0);
            this.Controls.SetChildIndex(this.m_cmdEmployeeSign, 0);
            this.Controls.SetChildIndex(this.lblTitle3, 0);
            this.Controls.SetChildIndex(this.m_lblGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_dtpCreateDate, 0);
            this.Controls.SetChildIndex(this.m_trvCreateDate, 0);
            this.Controls.SetChildIndex(this.lblCreateDateTitle, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_dtpGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.m_txtRecordContent, 0);
            this.Controls.SetChildIndex(this.lblTitle1, 0);
            this.Controls.SetChildIndex(this.m_txtRecordTitle, 0);
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
			clsGeneralDiseaseInfo objTrackInfo = new clsGeneralDiseaseInfo();

			objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;//m_objGetContentFromGUI();
			objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
			objTrackInfo.m_StrTitle = m_txtRecordTitle.Text;

			//����m_strTitle��m_dtmRecordTime
			if(objTrackInfo.m_ObjRecordContent !=null)
			{
				m_txtRecordTitle.Text=((clsGeneralDiseaseRecordContent)m_objCurrentRecordContent).m_strRecordTitle;//objTrackInfo.m_StrTitle;
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
			m_txtRecordTitle.Text="";
			m_txtRecordContent.m_mthClearText();
			m_cboRecordTitleType.SelectedIndex=-1;
			m_cboRecordTitleType.Text="";			
			//Ĭ��ǩ��
			MDIParent.m_mthSetDefaulEmployee(lsvSign);

		}

		/// <summary>
		/// �����Ƿ����ѡ���˺ͼ�¼ʱ���б��ڴӲ��̼�¼�������ʱ��Ҫʹ�á�
		/// </summary>
		/// <param name="p_blnEnable"></param>
		protected override void m_mthEnablePatientSelectSub(bool p_blnEnable)
		{
				cmdConfirm.Visible=true;
		}

		/// <summary>
		/// �����¼���������,�����Ӵ������Ҫ����ʵ��
		/// </summary>
		/// <param name="p_blnEnable">�Ƿ������޸������¼�ļ�¼��Ϣ��</param>
		protected override void m_mthEnableModifySub(bool p_blnEnable)
		{
			m_cboRecordTitleType.Enabled= p_blnEnable;
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

            //if(m_cboRecordTitleType.SelectedIndex==-1)
            //{
            //    clsPublicFunction.ShowInformationMessageBox("��ѡ���¼����!");
            //    return null;
            //}

			//�ӽ����ȡ��ֵ
			clsGeneralDiseaseRecordContent objContent=new clsGeneralDiseaseRecordContent();

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
            //    objContent.objSignerArr[i].m_strFORMID_VCHR="frmGeneralDisease";//ע���Сд
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

            if (m_blnIsNanNing)//����
            {
                objContent.m_strRecordTitle = m_txtRecordTitle.Text;
                objContent.m_intRecordTitleType = m_cboRecordTitleType.SelectedIndex;
            }
            else
            {
                //if (string.IsNullOrEmpty(m_cboRecordTitleType.Text ))
                //{
                //    clsPublicFunction.ShowInformationMessageBox("��ѡ���¼����!");
                //    return null;
                //}
                objContent.m_strRecordTitle = m_cboRecordTitleType.Text;
                objContent.m_intRecordTitleType = m_cboRecordTitleType.SelectedIndex;
            }

			objContent.m_strRecordContent_Right=m_txtRecordContent.m_strGetRightText();	
			objContent.m_strRecordContent=m_txtRecordContent.Text;
			objContent.m_strRecordContentXml=m_txtRecordContent.m_strGetXmlText();



			return objContent;	
		}

		/// <summary>
		/// �������¼��ֵ��ʾ�������ϡ�
		/// </summary>
		/// <param name="p_objContent"></param>
		protected override void m_mthSetGUIFromContent(clsTrackRecordContent p_objContent)
		{
			clsGeneralDiseaseRecordContent objContent=(clsGeneralDiseaseRecordContent)p_objContent;
            if (objContent == null) return;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            if (m_blnIsNanNing)//����
            {
                m_txtRecordTitle.Text = objContent.m_strRecordTitle;
            }
            else
            {
                m_cboRecordTitleType.Text = objContent.m_strRecordTitle;
            }

            //m_cboRecordTitleType.SelectedIndex=objContent.m_intRecordTitleType;	
			m_txtRecordContent.m_mthClearText();
			m_txtRecordContent.m_mthSetNewText(objContent.m_strRecordContent,objContent.m_strRecordContentXml);
            m_txtRecordTitle.Enabled = false;

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

            //ArrayList alt = new ArrayList();
            //alt.Add(m_txtRecordContent.Name);
            //m_strOpenDateRecord = objContent.m_dtmOpenDate.ToString();
            //m_objElectronIdiographDomain.m_mthGetIdiograph(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),m_strOpenDateRecord,alt);
		}

		protected override void m_mthSetDeletedGUIFromContent(clsTrackRecordContent p_objContent)
		{
			clsGeneralDiseaseRecordContent objContent=(clsGeneralDiseaseRecordContent)p_objContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            if (m_blnIsNanNing)//����
            {
                m_txtRecordTitle.Text = objContent.m_strRecordTitle;
            }
            else
            {
                m_cboRecordTitleType.Text = objContent.m_strRecordTitle;
            }
            //m_cboRecordTitleType.SelectedIndex=objContent.m_intRecordTitleType;	
			m_txtRecordContent.m_mthClearText();
			m_txtRecordContent.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strRecordContent,objContent.m_strRecordContentXml);

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
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.GeneralDisease);					
		}

		/// <summary>
		/// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
		{
			//��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
			clsGeneralDiseaseRecordContent objContent=(clsGeneralDiseaseRecordContent)p_objRecordContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            if (m_blnIsNanNing)//����
            {
                m_txtRecordTitle.Text = objContent.m_strRecordTitle;
            }
            else
            {
                m_cboRecordTitleType.Text = objContent.m_strRecordTitle;
            }
            //m_cboRecordTitleType.SelectedIndex=objContent.m_intRecordTitleType;	
			m_txtRecordContent.m_mthClearText();
			m_txtRecordContent.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strRecordContent,objContent.m_strRecordContentXml);		
			
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

		// ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
		public override string m_strReloadFormTitle()
		{
			//���Ӵ�������ʵ��
            if (m_blnIsNanNing)//����
            {
                return m_txtRecordTitle.Text;
            }
            else
            {
                return m_cboRecordTitleType.Text;
            }
		}		

		private void frmGeneralDisease_Load(object sender, System.EventArgs e)
		{
			try
			{
    
	//			m_cmdNewTemplate.Visible = true;		
				this.m_dtpCreateDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
				this.m_dtpCreateDate.m_mthResetSize();

                if (m_blnIsNanNing)//����
                {
                    m_txtRecordTitle.Focus();
                }
                else
                {
                    m_cboRecordTitleType.Focus();
                }

				if(m_BlnIsAddNew)
				{
					m_cboRecordTitleType.SelectedIndex = 0;
					
					m_txtRecordTitle_LostFocus(null,null);
				}

				//�ڴ˳�ʼ���������Ƶ�ListBox����Ϊ����ı���Focus����ɫ
				//frmHRPBaseForm��Loadʱ��ʼ���ؼ���HighLight
				m_mthInitListBox();
	 

	//			new clsAddOperationTool(this,m_txtRecordContent).m_mthAddContextMenu_Operation();
			}
			catch (Exception exp)
			{
				MessageBox.Show(exp.Message,"iCare Message", MessageBoxButtons.OK,MessageBoxIcon.Error);
    
			}

		}

		#region ��Ӽ��̿�ݼ�		
//		private void m_mthEvent_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
//		{
//			switch(e.KeyValue)
//			{
//				case 13:// enter					
//					if(((Control)sender).Name=="m_txtDoctorSign")
//					{						
//						m_mthGetDoctorList(m_txtDoctorSign.Text);
//
//						if(m_lsvDoctorList.Items.Count==1 && (m_txtDoctorSign.Text==m_lsvDoctorList.Items[0].SubItems[0].Text|| m_txtDoctorSign.Text==m_lsvDoctorList.Items[0].SubItems[1].Text))
//						{
//							m_lsvDoctorList.Items[0].Selected=true;
//							m_lsvDoctorList_DoubleClick(null,null);
//							break;
//						}
//					}					
//					else if(((Control)sender).Name=="m_lsvDoctorList")
//					{
//						m_lsvDoctorList_DoubleClick(null,null);						
//					}
//
//					break;
//
//				case 38:
//				case 40:
//					if(((Control)sender).Name=="m_txtDoctorSign")
//					{
//						if(m_txtDoctorSign.Text.Length>0)
//						{	
//							if(m_lsvDoctorList.Visible==false || m_lsvDoctorList.Items.Count==0)
//							{								
//								m_mthGetDoctorList(m_txtDoctorSign.Text);
//							}
//
//							m_lsvDoctorList.BringToFront();
//							m_lsvDoctorList.Visible=true;
//							m_lsvDoctorList.Focus();
//							if( m_lsvDoctorList.Items.Count>0)
//							{
//								m_lsvDoctorList.Items[0].Selected=true;
//								m_lsvDoctorList.Items[0].Focused=true;
//							}	
//						}
//					}					
//					break;
//				case 46:
//					if(((Control)sender).Name=="m_lsvEmployee")
//					{
//						while(m_lsvEmployee.SelectedItems.Count>0)
//							m_lsvEmployee.SelectedItems[0].Remove();
//					}
//					break;
//			}	
//		}

		/// <summary>
		/// ��ʾҽ���б�
		/// </summary>
		/// <param name="p_strDoctorNameLike">ҽ����</param>
//		private void m_mthGetDoctorList(string p_strDoctorNameLike)
//		{
//			
//			/*
//			 * ��ȡ����ҽ���ź���������������ҽ���ŵĿؼ���־��m_bytListOnDoctor��,
//			 * ����Ӧ��λ����ʾListView��
//			 */			
//
//			if(p_strDoctorNameLike.Length == 0)
//			{
//				m_lsvDoctorList.Visible = false;
//				return;
//			}
//
//			clsEmployee [] objDoctorArr = m_objCurrentContext.m_ObjEmployeeManager.m_objGetEmployeeIDLikeArr(p_strDoctorNameLike,m_objCurrentContext.m_ObjDepartment);
//
//			if(objDoctorArr == null)
//			{
//				m_lsvDoctorList.Visible = false;
//				return;
//			}
//
//			m_lsvDoctorList.Items.Clear();
//
//			for(int i=0;i<objDoctorArr.Length;i++)
//			{
//				ListViewItem lviDoctor = new ListViewItem(
//					new string[]{
//									objDoctorArr[i].m_StrEmployeeID,
//									objDoctorArr[i].m_StrFirstName
//								});
//				lviDoctor.Tag = objDoctorArr[i];
//
//				m_lsvDoctorList.Items.Add(lviDoctor);
//			}
//
//			m_mthChangeListViewLastColumnWidth(m_lsvDoctorList);
//			m_lsvDoctorList.BringToFront();
//			m_lsvDoctorList.Visible = true;
//		}

//		private void m_lsvDoctorList_DoubleClick(object sender, System.EventArgs e)
//		{
//			/*
//			 * ѡ����ҽ��������Ӧ���������ʾ��������������Tag����ҽ����Ϣ
//			 */
//			if(m_lsvDoctorList.SelectedItems.Count <= 0)
//				return;
//
//			clsEmployee objEmp = (clsEmployee)m_lsvDoctorList.SelectedItems[0].Tag;
//
//			if(objEmp == null)
//				return;
//			
//			if(!m_blnCheckEmployeeSign(objEmp.m_StrEmployeeID,objEmp.m_StrLastName))
//				return;
//
//			for(int i=0;i<m_lsvEmployee.Items.Count;i++)
//			{
//				if(m_lsvEmployee.Items[i].SubItems[1].Text==objEmp.m_StrEmployeeID)
//				{
//					clsPublicFunction.ShowInformationMessageBox("�Բ���,ҽ�������ظ�ǩ��!");
//					return;
//				}
//			}
//			ListViewItem lviNewItem=m_lsvEmployee.Items.Add(objEmp.m_StrLastName);
//			lviNewItem.SubItems.Add(objEmp.m_StrEmployeeID);					
//
//			m_lsvDoctorList.Visible = false;
//			m_txtDoctorSign.Text="";//���
//			m_txtDoctorSign.Focus();
//		}
//
//		private void m_lsvDoctorList_LostFocus(object sender,EventArgs e)
//		{							
//			if(!m_txtDoctorSign.Focused && !m_lsvDoctorList.Focused)
//			{
//				m_lsvDoctorList.Visible=false;				
//			}				
//		}	

		#endregion

		private void cmdConfirm_Click(object sender, System.EventArgs e)
		{
		
			if(m_lngSave() > 0)
			{
				this.DialogResult = DialogResult.Yes;
				this.Close();
			}
		}
		
		private clsTemplateDomain m_objTemplateDomain = new clsTemplateDomain();

		private void m_txtRecordTitle_LostFocus(object sender,EventArgs e)
		{		
			if(m_txtRecordContent.Text.Trim()!="" )
				return;

            if (m_blnIsNanNing)//����
            {
                if (m_txtRecordTitle.Text.Trim()=="")
                {
                    return;
                }
            }
            else
            {
                if (m_cboRecordTitleType.Text.Trim() == "")
                {
                    return;
                }
            }

			if(m_BlnIsAddNew )
			{
				if(m_objCurrentPatient !=null)
				{					
//					if(m_trvCreateDate.Nodes[0].Nodes.Count==0 && m_cboRecordTitleType.Text=="���̼�¼")
//					{
//						#region ��¼ʱ���סԺ����
//						clsInPatientCaseHisoryDefaultValue [] objInPatientCaseDefaultValue = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString());
//						if(objInPatientCaseDefaultValue !=null && objInPatientCaseDefaultValue.Length >0)
//						{
//							m_dtpCreateDate.Value = DateTime.Parse(objInPatientCaseDefaultValue[0].m_strCreateDate);
//						}
//						#endregion
//						m_txtRecordContent.Text="    ����"+m_txtPatientName.Text+","+lblSex.Text+","+lblAge.Text;

//						//Ĭ��ֵ
//						new clsDefaultValueTool(this,m_objCurrentPatient).m_mthSetDefaultValue();
//						//���϶˿ռ���
//						m_txtRecordContent.m_mthInsertText("    ",0);
//	
//						//�Զ�����ģ��
//						m_mthSetSpecialPatientTemplateSet(m_objCurrentPatient);
//
//						if(m_blnHaveAssociateTemplate)
//						{
//							int intIndex1 = m_txtRecordContent.Text.IndexOf("�������");
//							int intIndex2 = m_txtRecordContent.Text.LastIndexOf("�������");
//							if(intIndex1 != -1 && intIndex2 > intIndex1)
//								m_txtRecordContent.Text = m_txtRecordContent.Text.Remove(intIndex1,intIndex2 - intIndex1);
//						}
						

						//��ס�������ĸ���������
//						string strTemplateSetID = m_objTemplateDomain.m_strGetPatientHaveDisease_TemplateSetID(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString(),this.Name,(int)enmAssociate.Disease);
//						m_txtRecordContent.Tag = m_objTemplateDomain.m_strGetAssociateIDBySetID(strTemplateSetID,(int)enmAssociate.Operation);
//					}
//					else
//					{
//						m_txtRecordContent.Text = "    ����"+m_txtPatientName.Text+","+lblSex.Text+","+lblAge.Text+",";
//					}

					//Ĭ��ֵ
                    new clsDefaultValueTool(this, m_objCurrentPatient).m_mthSetDefaultValue();
                    m_txtRecordContent.m_mthInsertText("    ", 0);
					//�Զ�����ģ��
					m_mthSetSpecialPatientTemplateSet(m_objCurrentPatient);
				}
			}			
		}

		private void m_cboRecordTitleType_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if(m_BlnIsAddNew )
			{
				if(m_objCurrentPatient !=null)
				{					
//					if(m_trvCreateDate.Nodes[0].Nodes.Count==0 && m_cboRecordTitleType.Text=="���̼�¼")
//					{
//						m_txtRecordTitle.Text="�״β��̼�¼";
//					}
//					else
//					{
						m_txtRecordTitle.Text=m_cboRecordTitleType.Text;
//					}
				}
			}
			m_txtRecordTitle.Focus();
		}

		private void m_cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void mniLabCheckResult_Click(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			frmLabCheckReport frmlabcheckreport=new frmLabCheckReport();
			frmlabcheckreport.m_mthSetPatient(m_objCurrentPatient);
			frmlabcheckreport.m_mthInitLabCheckResult(m_txtRecordContent);
			frmlabcheckreport.ShowDialog(this);
			this.Cursor=Cursors.Default;
		}

//		public string m_StrRecordContent
//		{
//			set
//			{
//				m_txtRecordContent.Text += value;
//			}
//		}

		protected override void m_mthSetDefaultValue(clsPatient p_objPatient)
		{
            m_txtRecordContent.m_mthInsertText("    ", 0);
		}

		/// <summary>
		/// ������װģ�����������ֶΣ���������������
		/// </summary>
		protected override void m_mthSaveTemplateSet_Associate()
		{
			if(m_objCurrentPatient != null)
			{
				if(m_txtRecordContent.Tag != null)
				{
					clsPatient_Associate objContent = new clsPatient_Associate();
					objContent.strInPatientID = m_objCurrentPatient.m_StrInPatientID;
					objContent.strInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss");
					objContent.strAssociateID = m_txtRecordContent.Tag.ToString();
					
					long lngRes = m_objTemplateDomain.m_lngSavePatient_Associate(objContent,(int)enmAssociate.Operation);
				}
			}
		}

		private void lsvSign_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		
		



	}

	/// <summary>
	/// �Ҽ������������ƵĹ���
	/// </summary>
	public class clsAddOperationTool
	{
		private com.digitalwave.controls.ctlRichTextBox m_txtParent;
		private ListBox m_lstOperation;

		public clsAddOperationTool(Form p_frmParent,com.digitalwave.controls.ctlRichTextBox p_txtParent)
		{
			m_txtParent = p_txtParent;
			m_mthInitListBox(p_frmParent);
		}

		private void m_mthInitListBox(Form p_frmParent)
		{
			this.m_lstOperation = new ListBox();

			this.m_lstOperation.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.m_lstOperation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_lstOperation.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_lstOperation.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.m_lstOperation.ItemHeight = 12;
			this.m_lstOperation.Location = new System.Drawing.Point(10, 10);
			this.m_lstOperation.Name = "m_lstOperation";
			this.m_lstOperation.Size = new System.Drawing.Size(152, 182);
			this.m_lstOperation.TabIndex = 15006;
			this.m_lstOperation.BringToFront ();
			this.m_lstOperation.Visible = false;
			this.m_lstOperation.HorizontalScrollbar = true;

			m_lstOperation.DoubleClick += new EventHandler(m_lstOperation_DoubleClick);
			m_lstOperation.Leave += new EventHandler(m_lstOperation_Leave);
			
			//��������Control������ʾ
			p_frmParent.Controls.Add(m_lstOperation);
		}

		/// <summary>
		/// �Ҽ�������������
		/// </summary>
		public void m_mthAddContextMenu_Operation()
		{
			if(m_txtParent.ContextMenu != null)
			{
				m_txtParent.ContextMenu.MenuItems.Add("��������",new EventHandler(m_ctmOperation_Click));
			}
		}

		private void m_ctmOperation_Click(object sender,EventArgs e)
		{
			m_lstOperation.Items.Clear();

			clsTemplateSet_Associate[] m_objArr;
			new clsTemplateDomain().m_lngGetAllAssociate((int)enmAssociate.Operation,out m_objArr);
			if(m_objArr!=null && m_objArr.Length>0)
			{
				m_lstOperation.Items.AddRange(m_objArr);
			}

			System.Drawing.Point pt= m_txtParent.GetPositionFromCharIndex(m_txtParent.SelectionStart );

			m_lstOperation.Left = m_txtParent.Left + pt.X;
			m_lstOperation.Top = m_txtParent.Top + pt.Y + 20;
			
			m_lstOperation.Visible = true;
			m_lstOperation.BringToFront();
			if(m_lstOperation.Items.Count > 0)
				m_lstOperation.SelectedIndex = 0;
			m_lstOperation.Focus();
		}

		private void m_lstOperation_DoubleClick(object sender,EventArgs e)
		{
			ListBox lstOperation = (ListBox)sender;
			if(lstOperation.SelectedItems.Count > 0 && lstOperation.SelectedItems[0] != null)
			{
				m_txtParent.Tag = ((clsTemplateSet_Associate)lstOperation.SelectedItems[0]).strAssociateID;
			}
			m_txtParent.m_mthInsertText(((clsTemplateSet_Associate)lstOperation.SelectedItems[0]).strAssociateName,m_txtParent.SelectionStart);

			lstOperation.Visible = false;
			m_txtParent.Focus();
		}

		private void m_lstOperation_Leave(object sender, System.EventArgs e)
		{
			m_lstOperation.Visible = false;
			m_txtParent.Focus();
		}		

	}


	
}

