using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;
using HRP;
using com.digitalwave.Utility.Controls;
//using com.digitalwave.controls;
using System.Drawing.Printing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using com.digitalwave.Emr.Signature_gui; 

namespace iCare
{
	public class frmBaseCaseHistory : frmHRPBaseForm,PublicFunction
	{
		private System.ComponentModel.IContainer components = null;

		public frmBaseCaseHistory()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
            //ָ��������Ϊҽ������վ
            intFormType = 1;
			// TODO: Add any initialization after the InitializeComponent call
			m_objDomain = m_objGetDomain();
            //m_objBorderTool = new clsBorderTool(Color.White);
            //m_objBorderTool.m_mthChangedControlBorder(trvTime);

			m_blnCanTreeAfterSelect = true;
			
		}

		public PinkieControls.ButtonXP m_cmdCreateID;

		protected bool m_blnCanTreeAfterSelect;

		// ���̼�¼�������ʵ��
		protected clsBaseCaseHistoryDomain m_objDomain;

		protected clsInPatientCaseHistoryContent m_objReAddNewOld;

		// ���浱ǰ��ʾ�ļ�¼���ݵı���
		protected clsInPatientCaseHistoryContent m_objCurrentRecordContent;

		protected TreeNode m_trnRoot;

		protected clsPatient m_objCurrentPatient;

        //protected clsBorderTool m_objBorderTool;

		// ��ӡ����������ĵ�
		//		protected PrintDocument m_pdcPrintDocument;

		protected DateTime m_dtmFirstPrintDate;

		// ����Ƿ��״δ�ӡ
		protected bool m_blnIsFirstPrint;
		public System.Windows.Forms.TreeView trvTime;
		private System.Windows.Forms.ContextMenu m_cmuRichTextBoxMenu;
		private System.Windows.Forms.MenuItem mniDoubleStrikeOutDelete;
		public com.digitalwave.Utility.Controls.ctlTimePicker m_dtpCreateDate;
		public System.Windows.Forms.Label lblCreateDate;
		protected System.Drawing.Printing.PrintDocument m_pdcPrintDocument;
		public System.Windows.Forms.Label lblNativePlace;
		public System.Windows.Forms.Label m_lblNativePlace;
		public System.Windows.Forms.Label lblOccupation;
		public System.Windows.Forms.Label m_lblOccupation;
		public System.Windows.Forms.Label m_lblMarriaged;
		public System.Windows.Forms.Label lblMarriaged;
		public System.Windows.Forms.Label m_lblCreateUserName;
		public System.Windows.Forms.Label m_lblLinkMan;
		public System.Windows.Forms.Label lblLinkMan;
		public System.Windows.Forms.Label lblAddress;
        public System.Windows.Forms.Label m_lblAddress;
        protected Label lblNation;
        protected Label m_lblNation;

		protected bool m_blnAlreadySetPrintTools = false;


		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.trvTime = new System.Windows.Forms.TreeView();
            this.m_cmuRichTextBoxMenu = new System.Windows.Forms.ContextMenu();
            this.mniDoubleStrikeOutDelete = new System.Windows.Forms.MenuItem();
            this.m_dtpCreateDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblCreateDate = new System.Windows.Forms.Label();
            this.m_pdcPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.lblNativePlace = new System.Windows.Forms.Label();
            this.m_lblNativePlace = new System.Windows.Forms.Label();
            this.lblOccupation = new System.Windows.Forms.Label();
            this.m_lblOccupation = new System.Windows.Forms.Label();
            this.m_lblMarriaged = new System.Windows.Forms.Label();
            this.lblMarriaged = new System.Windows.Forms.Label();
            this.m_lblCreateUserName = new System.Windows.Forms.Label();
            this.m_lblLinkMan = new System.Windows.Forms.Label();
            this.lblLinkMan = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.m_lblAddress = new System.Windows.Forms.Label();
            this.m_cmdCreateID = new PinkieControls.ButtonXP();
            this.lblNation = new System.Windows.Forms.Label();
            this.m_lblNation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(616, 57);
            this.lblSex.Size = new System.Drawing.Size(34, 19);
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(700, 57);
            this.lblAge.Size = new System.Drawing.Size(36, 19);
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(405, 20);
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(390, 57);
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(572, 18);
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(572, 57);
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(654, 57);
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(212, 57);
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(452, 74);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(76, 104);
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(452, 55);
            this.txtInPatientID.Size = new System.Drawing.Size(76, 23);
            this.txtInPatientID.TabIndex = 3;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(622, 18);
            this.m_txtPatientName.Size = new System.Drawing.Size(112, 23);
            this.m_txtPatientName.TabIndex = 2;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(452, 16);
            this.m_txtBedNO.Size = new System.Drawing.Size(76, 23);
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(262, 53);
            this.m_cboArea.Size = new System.Drawing.Size(126, 23);
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(622, 42);
            this.m_lsvPatientName.Size = new System.Drawing.Size(98, 104);
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(452, 38);
            this.m_lsvBedNO.Size = new System.Drawing.Size(76, 104);
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(262, 14);
            this.m_cboDept.Size = new System.Drawing.Size(126, 23);
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(212, 14);
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(682, 298);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Location = new System.Drawing.Point(528, 18);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(618, 14);
            this.m_lblForTitle.Visible = true;
            // 
            // trvTime
            // 
            this.trvTime.BackColor = System.Drawing.Color.White;
            this.trvTime.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.trvTime.ForeColor = System.Drawing.SystemColors.Window;
            this.trvTime.HideSelection = false;
            this.trvTime.ItemHeight = 18;
            this.trvTime.Location = new System.Drawing.Point(20, 14);
            this.trvTime.Name = "trvTime";
            this.trvTime.ShowRootLines = false;
            this.trvTime.Size = new System.Drawing.Size(182, 62);
            this.trvTime.TabIndex = 4;
            this.trvTime.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvTime_AfterSelect);
            // 
            // m_cmuRichTextBoxMenu
            // 
            this.m_cmuRichTextBoxMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniDoubleStrikeOutDelete});
            // 
            // mniDoubleStrikeOutDelete
            // 
            this.mniDoubleStrikeOutDelete.Index = 0;
            this.mniDoubleStrikeOutDelete.Text = "˫����ɾ��";
            this.mniDoubleStrikeOutDelete.Click += new System.EventHandler(this.mniDoubleStrikeOutDelete_Click);
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.BorderColor = System.Drawing.Color.Black;
            this.m_dtpCreateDate.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
            this.m_dtpCreateDate.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_dtpCreateDate.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_dtpCreateDate.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_dtpCreateDate.flatFont = new System.Drawing.Font("����", 12F);
            this.m_dtpCreateDate.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpCreateDate.Location = new System.Drawing.Point(166, 88);
            this.m_dtpCreateDate.m_BlnOnlyTime = false;
            this.m_dtpCreateDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.m_dtpCreateDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.m_dtpCreateDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.m_dtpCreateDate.Name = "m_dtpCreateDate";
            this.m_dtpCreateDate.ReadOnly = false;
            this.m_dtpCreateDate.Size = new System.Drawing.Size(222, 22);
            this.m_dtpCreateDate.TabIndex = 5;
            this.m_dtpCreateDate.TextBackColor = System.Drawing.Color.White;
            this.m_dtpCreateDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblCreateDate
            // 
            this.lblCreateDate.AutoSize = true;
            this.lblCreateDate.Font = new System.Drawing.Font("����", 10.5F);
            this.lblCreateDate.Location = new System.Drawing.Point(86, 92);
            this.lblCreateDate.Name = "lblCreateDate";
            this.lblCreateDate.Size = new System.Drawing.Size(70, 14);
            this.lblCreateDate.TabIndex = 532;
            this.lblCreateDate.Text = "��¼����:";
            // 
            // m_pdcPrintDocument
            // 
            this.m_pdcPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdcPrintDocument_PrintPage);
            this.m_pdcPrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_EndPrint);
            this.m_pdcPrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_BeginPrint);
            // 
            // lblNativePlace
            // 
            this.lblNativePlace.AutoSize = true;
            this.lblNativePlace.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNativePlace.Location = new System.Drawing.Point(402, 152);
            this.lblNativePlace.Name = "lblNativePlace";
            this.lblNativePlace.Size = new System.Drawing.Size(42, 14);
            this.lblNativePlace.TabIndex = 543;
            this.lblNativePlace.Text = "����:";
            // 
            // m_lblNativePlace
            // 
            this.m_lblNativePlace.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblNativePlace.Location = new System.Drawing.Point(452, 151);
            this.m_lblNativePlace.Name = "m_lblNativePlace";
            this.m_lblNativePlace.Size = new System.Drawing.Size(76, 20);
            this.m_lblNativePlace.TabIndex = 542;
            // 
            // lblOccupation
            // 
            this.lblOccupation.AutoSize = true;
            this.lblOccupation.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOccupation.Location = new System.Drawing.Point(114, 122);
            this.lblOccupation.Name = "lblOccupation";
            this.lblOccupation.Size = new System.Drawing.Size(42, 14);
            this.lblOccupation.TabIndex = 541;
            this.lblOccupation.Text = "ְҵ:";
            // 
            // m_lblOccupation
            // 
            this.m_lblOccupation.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblOccupation.Location = new System.Drawing.Point(162, 121);
            this.m_lblOccupation.Name = "m_lblOccupation";
            this.m_lblOccupation.Size = new System.Drawing.Size(226, 20);
            this.m_lblOccupation.TabIndex = 545;
            // 
            // m_lblMarriaged
            // 
            this.m_lblMarriaged.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblMarriaged.Location = new System.Drawing.Point(452, 121);
            this.m_lblMarriaged.Name = "m_lblMarriaged";
            this.m_lblMarriaged.Size = new System.Drawing.Size(76, 20);
            this.m_lblMarriaged.TabIndex = 544;
            // 
            // lblMarriaged
            // 
            this.lblMarriaged.AutoSize = true;
            this.lblMarriaged.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMarriaged.Location = new System.Drawing.Point(402, 122);
            this.lblMarriaged.Name = "lblMarriaged";
            this.lblMarriaged.Size = new System.Drawing.Size(42, 14);
            this.lblMarriaged.TabIndex = 535;
            this.lblMarriaged.Text = "���:";
            // 
            // m_lblCreateUserName
            // 
            this.m_lblCreateUserName.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblCreateUserName.Location = new System.Drawing.Point(622, 151);
            this.m_lblCreateUserName.Name = "m_lblCreateUserName";
            this.m_lblCreateUserName.Size = new System.Drawing.Size(88, 20);
            this.m_lblCreateUserName.TabIndex = 536;
            this.m_lblCreateUserName.Visible = false;
            // 
            // m_lblLinkMan
            // 
            this.m_lblLinkMan.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblLinkMan.Location = new System.Drawing.Point(452, 90);
            this.m_lblLinkMan.Name = "m_lblLinkMan";
            this.m_lblLinkMan.Size = new System.Drawing.Size(76, 20);
            this.m_lblLinkMan.TabIndex = 539;
            // 
            // lblLinkMan
            // 
            this.lblLinkMan.AutoSize = true;
            this.lblLinkMan.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLinkMan.Location = new System.Drawing.Point(390, 91);
            this.lblLinkMan.Name = "lblLinkMan";
            this.lblLinkMan.Size = new System.Drawing.Size(56, 14);
            this.lblLinkMan.TabIndex = 540;
            this.lblLinkMan.Text = "��ϵ��:";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAddress.Location = new System.Drawing.Point(114, 152);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(42, 14);
            this.lblAddress.TabIndex = 537;
            this.lblAddress.Text = "��ַ:";
            // 
            // m_lblAddress
            // 
            this.m_lblAddress.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblAddress.Location = new System.Drawing.Point(162, 151);
            this.m_lblAddress.Name = "m_lblAddress";
            this.m_lblAddress.Size = new System.Drawing.Size(226, 20);
            this.m_lblAddress.TabIndex = 538;
            // 
            // m_cmdCreateID
            // 
            this.m_cmdCreateID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdCreateID.DefaultScheme = true;
            this.m_cmdCreateID.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdCreateID.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdCreateID.Hint = "";
            this.m_cmdCreateID.Location = new System.Drawing.Point(529, 143);
            this.m_cmdCreateID.Name = "m_cmdCreateID";
            this.m_cmdCreateID.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCreateID.Size = new System.Drawing.Size(84, 28);
            this.m_cmdCreateID.TabIndex = 10000080;
            this.m_cmdCreateID.Tag = "1";
            this.m_cmdCreateID.Text = "��ʷ��¼��:";
            // 
            // lblNation
            // 
            this.lblNation.AutoSize = true;
            this.lblNation.Location = new System.Drawing.Point(522, 96);
            this.lblNation.Name = "lblNation";
            this.lblNation.Size = new System.Drawing.Size(42, 14);
            this.lblNation.TabIndex = 10000081;
            this.lblNation.Text = "����:";
            // 
            // m_lblNation
            // 
            this.m_lblNation.Location = new System.Drawing.Point(564, 96);
            this.m_lblNation.Name = "m_lblNation";
            this.m_lblNation.Size = new System.Drawing.Size(30, 14);
            this.m_lblNation.TabIndex = 10000082;
            // 
            // frmBaseCaseHistory
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(792, 673);
            this.Controls.Add(this.lblNativePlace);
            this.Controls.Add(this.lblOccupation);
            this.Controls.Add(this.lblMarriaged);
            this.Controls.Add(this.lblLinkMan);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblNation);
            this.Controls.Add(this.m_lblNation);
            this.Controls.Add(this.lblCreateDate);
            this.Controls.Add(this.m_cmdCreateID);
            this.Controls.Add(this.m_lblNativePlace);
            this.Controls.Add(this.m_lblOccupation);
            this.Controls.Add(this.m_lblMarriaged);
            this.Controls.Add(this.m_lblCreateUserName);
            this.Controls.Add(this.m_lblLinkMan);
            this.Controls.Add(this.m_lblAddress);
            this.Controls.Add(this.m_dtpCreateDate);
            this.Controls.Add(this.trvTime);
            this.Name = "frmBaseCaseHistory";
            this.Load += new System.EventHandler(this.frmBaseCaseHistory_Load);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.trvTime, 0);
            this.Controls.SetChildIndex(this.m_dtpCreateDate, 0);
            this.Controls.SetChildIndex(this.m_lblAddress, 0);
            this.Controls.SetChildIndex(this.m_lblLinkMan, 0);
            this.Controls.SetChildIndex(this.m_lblCreateUserName, 0);
            this.Controls.SetChildIndex(this.m_lblMarriaged, 0);
            this.Controls.SetChildIndex(this.m_lblOccupation, 0);
            this.Controls.SetChildIndex(this.m_lblNativePlace, 0);
            this.Controls.SetChildIndex(this.m_cmdCreateID, 0);
            this.Controls.SetChildIndex(this.lblCreateDate, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lblNation, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.lblNation, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.lblAddress, 0);
            this.Controls.SetChildIndex(this.lblLinkMan, 0);
            this.Controls.SetChildIndex(this.lblMarriaged, 0);
            this.Controls.SetChildIndex(this.lblOccupation, 0);
            this.Controls.SetChildIndex(this.lblNativePlace, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

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

				m_objDomain= null;
				m_objReAddNewOld = null;
				m_objCurrentRecordContent = null;
				m_objCurrentPatient = null;
                //m_objBorderTool = null;
			}
			base.Dispose( disposing );
		}
		// ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
		public virtual void m_strReloadFormTitle()
		{
			//���Ӵ�������ʵ��
		}

		// ��ս���
		protected void m_mthClearAll()
		{
			//��ղ��˻�����Ϣ          
			base.m_mthClearPatientBaseInfo();
		
			//���ʱ���б���
            //if(this.trvTime .Nodes[0].Nodes.Count >0)
            //    trvTime.Nodes[0].Nodes.Clear();
		
			//���õ�ǰ���˱���
			m_objCurrentPatient = null;
		
			//��յ�ǰ��¼��
			m_mthClearPatientRecordInfo();
		}

		// ��ղ��˼�¼������Ϣ��
		protected void m_mthClearPatientRecordInfo()
		{
			//�Ѽ�¼ʱ��ָ�����ǰʱ��      
			
			m_mthEnableModify(true);
		                       
			//��ռ�¼����                       
			m_mthClearRecordInfo();
		
			//��ձ��浱ǰ��¼�ı���
			m_objCurrentRecordContent = null;        
		
			//��գ����ã�������Ϣ 
			m_objReAddNewOld = null;

			m_mthSetModifyControl(null,true);
			
		}

		// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
		protected virtual void m_mthClearRecordInfo()
		{
			this.m_dtpCreateDate.Text =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");  
			//��վ����¼���ݣ����Ӵ�������ʵ��

			//			m_mthSetModifyControl(null,true);
		}

		// �����Ƿ����ѡ���˺ͼ�¼ʱ���б��ڴӲ��̼�¼�������ʱ��Ҫʹ�á�
		protected void m_mthEnablePatientSelect(bool p_blnEnable)
		{
			//���ò���ѡ����Ϣ�� Enable = p_blnEnable
		
			//����ʱ���б����� Enable = p_blnEnable
		
			//���ü�¼ʱ��� Enable = p_blnEnable              
		
			m_mthEnablePatientSelectSub(p_blnEnable) ;
		}

		// �����Ƿ����ѡ���˺ͼ�¼ʱ���б��ڴӲ��̼�¼�������ʱ��Ҫʹ�á�
		protected virtual void m_mthEnablePatientSelectSub(bool p_blnEnable)
		{
		
		}

		// �Ƿ������޸ļ�¼ʱ��ȼ�¼��Ϣ��
		protected void m_mthEnableModify(bool p_blnEnable)
		{
			//���ü�¼ʱ��� Enable = p_blnEnable
			
			//���þ����¼���������
			m_mthEnableModifySub(p_blnEnable);
		}

		// �Ƿ������޸������¼�ļ�¼��Ϣ��
		protected virtual void m_mthEnableModifySub(bool p_blnEnable)
		{
			//�����¼���������,�����Ӵ������Ҫ����ʵ��
			if(p_blnEnable)
			{
				this.m_dtpCreateDate.Enabled =true;

			}
			else
			{
				this.m_dtpCreateDate.Enabled =false;
			}

		}

		#region Alex mark 2003-5-16
		//		// �����Ƿ�����޸ģ��޸����ۼ�����
		//		protected virtual void m_mthSetModifyControl(clsInPatientCaseHistoryContent p_objRecordContent,
		//			bool p_blnReset)
		//		{
		//			//������д�淶���þ��崰�����д���ƣ����Ӵ�������ʵ��
		//		}
		#endregion

		/// <summary>
		/// �����Ƿ�����޸ģ��޸����ۼ�����
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_blnReset"></param>
		protected void m_mthSetModifyControl(clsInPatientCaseHistoryContent p_objRecordContent,
			bool p_blnReset)
		{
            if (m_BlnNeedContextMenu)
                m_mthAddRichTemplateInContainer(this);
			//������д�淶���þ��崰�����д���ƣ����Ӵ�������ʵ��
			if(p_blnReset==true)
			{
				m_mthSetRichTextModifyColor(this,clsHRPColor.s_ClrInputFore);
				m_mthSetRichTextCanModifyLast(this,true);
			}
			else if(p_objRecordContent!=null)
			{
                bool blnTemp = m_blnGetCanModifyLast(p_objRecordContent.m_strModifyUserID.Trim());
				m_mthSetRichTextModifyColor(this,Color.Red);
                m_mthSetRichTextCanModifyLast(this, blnTemp);
			}

			m_mthSetModifyControlSub(p_objRecordContent,p_blnReset);
		}

		protected virtual void m_mthSetModifyControlSub(clsInPatientCaseHistoryContent p_objRecordContent,
			bool p_blnReset)
		{

		}
        /// <summary>
        /// �����ò��˵Ļ�����Ϣ
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        protected override void m_mthOnlySetPatientInfo(clsPatient p_objSelectedPatient)
        {
            clsPeopleInfo objInfo = p_objSelectedPatient.m_ObjPeopleInfo;
            //lblSex.Text = p_objSelectedPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_ObjPeopleInfo.m_StrSex;
            //lblAge.Text = p_objSelectedPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_ObjPeopleInfo.m_StrAge;


            this.m_lblAddress.Text = objInfo.m_StrHomeAddress;
            this.m_lblLinkMan.Text = objInfo.m_StrLinkManFirstName;
            this.m_lblMarriaged.Text = objInfo.m_StrMarried;
            this.m_lblOccupation.Text = objInfo.m_StrOccupation;
            this.m_lblNation.Text = objInfo.m_StrNation;
            this.m_lblNativePlace.Text = objInfo.m_StrNativePlace; 
        }
		// ���ò��˱���Ϣ
		protected override void m_mthSetPatientFormInfo(clsPatient p_objSelectedPatient)
		{

			//�жϲ�����Ϣ�Ƿ�Ϊnull������ǣ�ֱ�ӷ��ء�
			if(p_objSelectedPatient == null)
				return;   	
		
			//��ղ��˼�¼��Ϣ
			m_mthClearPatientRecordInfo();
		
			//��¼������Ϣ
			m_objCurrentPatient =p_objSelectedPatient;
            //m_mthOnlySetPatientInfo(p_objSelectedPatient);
            //this.m_lblAddress .Text = m_objCurrentPatient.m_ObjPeopleInfo.m_StrHomeAddress;
            //this.m_lblLinkMan.Text =m_objCurrentPatient.m_ObjPeopleInfo.m_StrLinkManFirstName;
            //this.m_lblMarriaged.Text =m_objCurrentPatient.m_ObjPeopleInfo.m_StrMarried;
            //this.m_lblOccupation.Text =m_objCurrentPatient.m_ObjPeopleInfo.m_StrOccupation;
            //this.m_lblNation.Text = m_objCurrentPatient.m_ObjPeopleInfo.m_StrNation;
            ////			this.m_lblCreateUserName.Text =MDIParent.strOperatorName;
            ////����
            //this.m_lblNativePlace.Text =m_objCurrentPatient.m_ObjPeopleInfo.m_StrNativePlace; 

			//��ȡ���˼�¼�б�
            //string [] strInPatientDateListArr=null;
            //string [] strCreateTimeListArr=null;
            //string [] strOpenTimeListArr=null;
            //long lngRes = m_objDomain.m_lngGetRecordTimeList(p_objSelectedPatient.m_StrInPatientID,out strInPatientDateListArr, out strCreateTimeListArr,out strOpenTimeListArr);
		
			//			if(lngRes <= 0 || strOpenTimeListArr == null || strCreateTimeListArr==null)
			//				return;  
            //if(lngRes <= 0 )
            //    return;

			//���ʱ���б�����ʱ��ڵ�   
            //if(trvTime.Nodes[0].Nodes.Count >0)
            //    trvTime.Nodes[0].Nodes.Clear();

            ////��Ӳ�ѯ������Ժʱ�䵽ʱ������
            ////			if(strCreateTimeListArr!=null)
            ////			{
            //for(int i=p_objSelectedPatient.m_ObjInBedInfo.m_intGetSessionCount()-1;i>=0;i--)
            //{			
            //    TreeNode trnRecordDate = new TreeNode(p_objSelectedPatient.m_ObjInBedInfo.m_objGetSessionByIndex(i).m_DtmHISInDate.ToString("yyyy��MM��dd�� HH:mm:ss"));
            //    if(strOpenTimeListArr!=null)
            //    {
            //        for(int j2=0;j2<strInPatientDateListArr.Length;j2++)
            //        {
            //            if (DateTime.Parse(strInPatientDateListArr[j2]) == p_objSelectedPatient.m_ObjInBedInfo.m_objGetSessionByIndex(i).m_DtmEMRInDate)
            //            {
            //                trnRecordDate.Tag =(string)strOpenTimeListArr[j2];
            //                break;
            //            }
            //        }
            //    }
            //    trvTime.Nodes[0].Nodes.Add(trnRecordDate);	
            //    trvTime.ExpandAll();	
            //}			

            ////ѡ��Ĭ�Ͻڵ�
            //for(int i = 0; i < trvTime.Nodes[0].Nodes.Count; i++)
            //{
            //    if(trvTime.Nodes[0].Nodes[i].Text == p_objSelectedPatient.m_DtmSelectedHISInDate.ToString("yyyy��MM��dd�� HH:mm:ss"))
            //        trvTime.SelectedNode = trvTime.Nodes[0].Nodes[i];
            //}
            //if(trvTime.Nodes[0].Nodes.Count>0 && (trvTime.SelectedNode==null || trvTime.SelectedNode.Text.Length==4))//������Ҫ�˾����ĬȻѡ�����ڵ��¼�				
            //    trvTime.SelectedNode=trvTime.Nodes[0].Nodes[0];

            //if(!m_dtpCreateDate.Enabled)
            //    m_EnmFormEditStatus = MDIParent.enmFormEditStatus.Modify;

			//			}

			//չ������ʾ����ʱ��ڵ�
			//			trvTime.ExpandAll();
		
			//			//��Ӳ�ѯ����ʱ�䵽ʱ������ 
			//			if(strCreateTimeListArr!=null)
			//			{
			//				for(int i=0;i<strCreateTimeListArr.Length;i++)
			//				{
			//					TreeNode trnRecordDate = new TreeNode(strCreateTimeListArr[i]);
			//					trnRecordDate.Tag =(string)strOpenTimeListArr[i];
			//					trvTime.Nodes[0].Nodes .Add(trnRecordDate);
			//
			//				}
			//			}
			//			//չ������ʾ����ʱ��ڵ㡣
			
			
		}

		// �ӽ����ȡ�����¼��ֵ���������ֵ��������null��
		protected virtual clsInPatientCaseHistoryContent m_objGetContentFromGUI()
		{
			//�ؽ����ȡ��ֵ�����Ӵ�������ʵ��
			return null;
		}
        //��ȡ��¼�����ߣ�ɾ����¼ʱ�ж��Ƿ���Ȩ��ɾ��
        protected virtual clsInPatientCaseHistoryContent m_objGetCreateUserFromGUI()
        {
            //�ؽ����ȡ��ֵ�����Ӵ�������ʵ��
            return null;
        }
		protected virtual clsPictureBoxValue[] m_objGetPicContentFromGUI()
		{
			//�ؽ����ȡ��ֵ�����Ӵ�������ʵ��
			return null;
		}

		// �������¼��ֵ��ʾ�������ϡ�
		protected virtual void m_mthSetGUIFromContent(clsInPatientCaseHistoryContent p_objContent,clsPictureBoxValue[] p_objPicValueArr)
		{
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
			this.m_dtpCreateDate.Text =p_objContent.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss");   

		}

		// ����ѡ���˵ļ�¼��Ϣ��
		protected void m_mthSetSelectedRecord(clsPatient p_objPatient)
		{
			//������
			if(p_objPatient==null /*|| p_strRecordTime==null || p_strRecordTime==""*/)  
			{
				m_objCurrentRecordContent = null;
				return ;
			}

			clsBaseCaseHistoryInfo  objContent =null;  
			clsPictureBoxValue[] objPicValueArr = null;
			//��ȡ��¼
			//			long lngRes = m_objDomain.m_lngGetRecordContent(p_objPatient.m_StrInPatientID ,p_objPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate .ToString("yyyy-MM-dd HH:mm:ss") ,/*p_strRecordTime ,*/ out objContent,out objPicValueArr);
            long lngRes = m_objDomain.m_lngGetRecordContent(p_objPatient.m_StrInPatientID, p_objPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"),/*p_strRecordTime ,*/ out objContent, out objPicValueArr);
		
			if(lngRes <= 0 || objContent == null)
			{
				m_objCurrentRecordContent = null;
				return;                            
			}
			
			//���ü�¼ʱ��     
			m_objCurrentRecordContent =(clsInPatientCaseHistoryContent )objContent;

            m_dtmCreatedDate = m_objCurrentRecordContent.m_dtmOpenDate;
			m_mthSetGUIFromContent((clsInPatientCaseHistoryContent )objContent,objPicValueArr);
			this.m_dtpCreateDate.Text =((clsInPatientCaseHistoryContent )objContent).m_dtmCreateDate.ToString("yyyy��MM��dd�� HH:mm:ss"); 
			this.m_lblCreateUserName.Text=m_objCurrentRecordContent.m_strCreateName;
			m_mthEnableModify(false);
		
			//			m_mthSetModifyControl((clsInPatientCaseHistoryContent )objContent,true);
			m_mthSetModifyControl((clsInPatientCaseHistoryContent)objContent,false);//Alex 2003-5-16			

		}

		// ��ȡ���̼�¼�������ʵ��
		protected virtual clsBaseCaseHistoryDomain m_objGetDomain()
		{
			//��ȡ���̼�¼�������ʵ�������Ӵ�������ʵ��
			return null;
		}

		// �Ƿ�����Ӽ�¼��true������ӣ�false���޸ġ�
		protected override bool m_BlnIsAddNew
		{
			get
			{
				return m_objCurrentRecordContent == null;
			}
		
		}
		/// <summary>
		/// ��Ӽ�¼
		/// </summary>
		/// <returns></returns>
		protected override long m_lngSubAddNew()
		{
            long lngRes = 0;
			if(m_objReAddNewOld != null)
                lngRes = m_lngReAddNew();
			else
                lngRes = m_lngAddNewRecord();

            if (lngRes > 0 && com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjCurrentEmployee.m_strTECHNICALRANK_CHR == "��ϰҽʦ")
            {
                clsEMR_CaseAuditVO objAuditVO = m_objGetAuditCase();
                objAuditVO.m_dtmCREATEDATE = m_objCurrentRecordContent.m_dtmOpenDate;
                m_mthAddAuditCase(objAuditVO);
            }
            return lngRes;
		}

		private string m_strGetTemplateSetID()
		{
			foreach(Control ctlSub in this.Controls)
			{
				if(ctlSub.Name=="m_lstTemplate" && ctlSub.Tag!=null)
					return ctlSub.Tag.ToString();
			}
			return "";
		}

		// ����¼�¼�����ݿⱣ�档
		protected long m_lngAddNewRecord()
		{

			//��鵱ǰ���˱����Ƿ�Ϊnull
			if(m_objCurrentPatient==null )
				return (long)enmOperationResult .Parameter_Error;
 
//            if(trvTime.SelectedNode == null || trvTime.SelectedNode.Equals(trvTime.Nodes[0]))
//            {
//#if !Debug
//                clsPublicFunction.ShowInformationMessageBox("��ѡ������Ժ���ڡ�");
//#endif
//                return -7;
//            }

            if (m_ObjCurrentEmrPatientSession == null)
            {
#if !Debug
				clsPublicFunction.ShowInformationMessageBox("��ѡ������Ժ���ڡ�");
#endif
				return -7;
            }

			//��ȡ������ʱ��
			clsPublicDomain m_objPDomain=new clsPublicDomain() ;
			
			//�ӽ����ȡ��¼��Ϣ
			clsInPatientCaseHistoryContent  objContent = m_objGetContentFromGUI();     
		           
			//��ȡ��ͼ��Ϣ
			clsPictureBoxValue [] objPicValueArr = m_objGetPicContentFromGUI();

			string strDiseaseID = new clsTemplateDomain().m_strGetAssociateIDBySetID(m_strGetTemplateSetID(),(int)enmAssociate.Disease);

			//��������ֵ����
			if(objContent == null)
				return (long)enmOperationResult.Parameter_Error;
					
			//���� clsInPatientCaseHistoryContent ����Ϣ��ʹ�÷�����ʱ������m_dtmOpenDate��m_dtmModifyDate��
			objContent.m_bytIfConfirm =0;
			objContent.m_bytStatus =0;
			objContent.m_dtmInPatientDate =m_objCurrentPatient.m_DtmSelectedInDate;
			objContent.m_dtmModifyDate =DateTime.Parse(m_objPDomain.m_strGetServerTime()); 
			objContent.m_dtmOpenDate =DateTime.Parse( m_objPDomain.m_strGetServerTime()); 
			//objContent.m_strCreateUserID =MDIParent.strOperatorID;
			objContent.m_strInPatientID =m_objCurrentPatient.m_StrInPatientID ;
			objContent.m_strModifyUserID =objContent.m_strModifyUserID;
			objContent.m_dtmCreateDate=DateTime.Parse(this.m_dtpCreateDate.Text );
			 
			//�����¼
			clsPreModifyInfo p_objModifyInfo=null;

            #region ��ǩ��ʱ��֤����ǩ���� ������

                //����ǩ�� 
                //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
                clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
                objSign_VO.m_strFORMID_VCHR = this.Name;
                objSign_VO.m_strFORMRECORDID_VCHR = objContent.m_strInPatientID.Trim() + "-" + objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"); ;
                objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
                objSign_VO.m_strRegisterId = m_objBaseCurrentPatient.m_StrRegisterId;
            if (objContent.objSignerArr != null)
            {
                ArrayList objSignerArr = new ArrayList();
                for (int i = 0; i < objContent.objSignerArr.Length; i++)
                {
                    if (objContent.objSignerArr[i].controlName == "lsvSign" || objContent.objSignerArr[i].controlName == "txtSign")
                        objSignerArr.Add(objContent.objSignerArr[i].objEmployee);
                }
                clsCheckSignersController objCheck = new clsCheckSignersController(objSignerArr, false);
                if (objCheck.CheckSigner(objContent, objSign_VO) == -1)
                    return -1;
            }
            else
            {
                objContent.m_strModifyUserID = MDIParent.OperatorID;
                clsCheckSignersController objCheck = new clsCheckSignersController();
                if (objCheck.m_lngSign(objContent, objSign_VO) == -1)
                    return -1;
            }
            #endregion	


           
			long lngRes = m_objDomain.m_lngAddNewRecord(objContent,objPicValueArr,strDiseaseID,out p_objModifyInfo);
		     
			//���ݽ������ͬ�Ĵ���
            switch ((enmOperationResult)lngRes)
            {
                case enmOperationResult.DB_Succeed:
                    //�������������
                    //					lngRes = m_lngSubAddNewRecordAfterMain(objContent);

                    m_objCurrentRecordContent = objContent;
                    m_dtmCreatedDate = objContent.m_dtmOpenDate;

                    m_mthHandleAddRecordSucceed();

                    this.m_dtpCreateDate.Enabled = false;
                    break;
                //...
                case enmOperationResult.Record_Already_Exist:
                    m_mthShowRecordTimeDouble();
                    return lngRes;
            }  
            //this.trvTime.ExpandAll(); 
			//���ؽ��
			return lngRes;
		}

		protected virtual long m_lngSubAddNewRecordAfterMain(clsInPatientCaseHistoryContent p_objNewContent)
		{
			return (long)enmOperationResult.DB_Succeed;
		}

		protected virtual void m_mthHandleAddRecordSucceed()
		{
			//��ӽڵ㵽ʱ���б���,��ѡ��
            //TreeNode tndNewNode=new TreeNode();
            //tndNewNode.Text =m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmHISInDate.ToString("yyyy��MM��dd�� HH:mm:ss")  ;
            //tndNewNode.Tag =(string)m_objCurrentRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") ;
            //this.trvTime.Nodes[0].Nodes.Add(tndNewNode);  
            //m_blnCanTreeAfterSelect = false;
            //this.trvTime.SelectedNode =tndNewNode;				
            //m_blnCanTreeAfterSelect = true;
		}

		protected override long m_lngSubModify()
		{
			//����ֻ����
			//��鵱ǰ���˱����Ƿ�Ϊnull
			if(m_objCurrentPatient ==null)
				return (long)enmOperationResult .Parameter_Error ;
			//��ȡ������ʱ��
			clsPublicDomain m_objPDomain=new clsPublicDomain() ;
			//�ӽ����ȡ��¼��Ϣ
			clsInPatientCaseHistoryContent  objContent = m_objGetContentFromGUI();     

			//��ȡ��ͼ��Ϣ
			clsPictureBoxValue [] objPicValueArr = m_objGetPicContentFromGUI();

			//��ȡ����
			string strDiseaseID = new clsTemplateDomain().m_strGetAssociateIDBySetID(m_strGetTemplateSetID(),(int)enmAssociate.Disease);
		           
			//��������ֵ����           
			if(objContent == null)
				return (long)enmOperationResult .Parameter_Error;
		
			//���� clsInPatientCaseHistoryContent ����Ϣ��ʹ�÷�����ʱ������m_dtmModifyDate��
			objContent.m_bytIfConfirm =0;
			objContent.m_bytStatus =0;
			objContent.m_dtmInPatientDate =m_objCurrentPatient.m_DtmSelectedInDate;
			objContent.m_dtmModifyDate =DateTime.Parse(m_objPDomain.m_strGetServerTime()); 
			objContent.m_dtmCreateDate=DateTime.Parse(this.m_dtpCreateDate.Text );
			objContent.m_strCreateUserID =MDIParent.strOperatorID;
			objContent.m_strInPatientID =m_objCurrentPatient.m_StrInPatientID ;
			objContent.m_strModifyUserID =objContent.m_strModifyUserID;

			//�������м�¼�Ŀ�ʼʹ��ʱ��
			objContent.m_dtmOpenDate = m_objCurrentRecordContent.m_dtmOpenDate ;  //m_objCurrentRecordContentΪ���ĳ����¼ʱ��ļ�¼����ǰ��¼
		
			//����ǩ�� 
			//��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20

                clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
                objSign_VO.m_strFORMID_VCHR = this.Name;
                objSign_VO.m_strFORMRECORDID_VCHR = objContent.m_strInPatientID.Trim() + "-" + objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"); 
                objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
                objSign_VO.m_strRegisterId = m_objBaseCurrentPatient.m_StrRegisterId;
            clsCheckSignersController objCheck = new clsCheckSignersController();
            if (objCheck.m_lngSign(objContent, objSign_VO) == -1)
                return -1;

			//�޸ļ�¼
			clsPreModifyInfo m_objModifyInfo;
			long lngRes = m_objDomain.m_lngModifyRecord(m_objCurrentRecordContent,objContent,objPicValueArr,strDiseaseID,out m_objModifyInfo);
		        
			//���ݽ������ͬ�Ĵ���
			switch((enmOperationResult)lngRes)
			{
				case enmOperationResult.DB_Succeed:

                    m_objCurrentRecordContent = objContent;
                    m_dtmCreatedDate = objContent.m_dtmOpenDate;
					break;   
					//...
			}  
			//չ������ʾ����ʱ��ڵ㡣
			//			trvTime.ExpandAll();
			//���ؽ��
			return lngRes;
		}		

		protected virtual long m_lngSubModifyRecordAfterMain(clsInPatientCaseHistoryContent p_objNewContent)
		{
			return (long)enmOperationResult.DB_Succeed;
		}

		protected override long m_lngSubDelete()
		{
			//��鵱ǰ���˱����Ƿ�Ϊnull  
			if(m_objCurrentPatient ==null || m_ObjCurrentEmrPatientSession == null)
			{
				clsPublicFunction.ShowInformationMessageBox("δѡ������,�޷�ɾ��!");//�޺�褣�2003-5-27
				return (long)enmOperationResult.Parameter_Error; 
			}
			//��鵱ǰ��¼�Ƿ�Ϊnull
			if(m_objCurrentRecordContent==null)
			{
				clsPublicFunction.ShowInformationMessageBox("��ǰ��¼����Ϊ��,�޷�ɾ��!");//�޺�褣�2003-5-27
				return (long)enmOperationResult.Parameter_Error; 
			}
			//��ȡ������ʱ��      
			clsPublicDomain m_objPDomain=new clsPublicDomain() ;
			
			//ɾ����¼
            clsInPatientCaseHistoryContent objContent = m_objGetContentFromGUI();
            //clsInPatientCaseHistoryContent objContent = new clsInPatientCaseHistoryContent();
			objContent.m_bytStatus =0;
			objContent.m_dtmCreateDate=DateTime.Parse(this.m_dtpCreateDate.Text );
			objContent.m_dtmInPatientDate =m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmEMRInDate;
			objContent.m_strInPatientID =m_objCurrentPatient.m_StrInPatientID ;
			objContent.m_strDeActivedOperatorID =MDIParent.OperatorID ;
			objContent.m_dtmOpenDate = m_objCurrentRecordContent.m_dtmOpenDate ;

            //Ȩ���ж�
            string strDeptIDTemp = m_ObjCurrentEmrPatientSession.m_strDeptId;
            bool blnIsAllow = clsPublicFunction.IsAllowDelete(strDeptIDTemp, objContent.m_strCreateUserID.Trim(), clsEMRLogin.LoginEmployee, 1);
            if (!blnIsAllow)
                return -1;
			
			//���� m_objCurrentRecordContent ����Ϣ��ʹ�÷�����ʱ������m_dtmDeActivedDate��
			objContent.m_dtmDeActivedDate =DateTime.Parse(m_objPDomain.m_strGetServerTime()); 
			
			clsPreModifyInfo m_objModifyInfo=null;

			long lngRes = m_objDomain.m_lngDeleteRecord(objContent,out m_objModifyInfo);
		
			//���ݽ������ͬ�Ĵ���
			switch((enmOperationResult)lngRes)
			{
                case enmOperationResult.DB_Succeed:
                    clsEMR_CaseAuditVO objAuditVO = m_objGetAuditCase();
                    objAuditVO.m_dtmCREATEDATE = m_objCurrentRecordContent.m_dtmOpenDate;
                    m_mthDelAuditCase(objAuditVO);
					//��ռ�¼��Ϣ  
                    m_objCurrentRecordContent = null;
                    m_dtmCreatedDate = DateTime.Now;   
					m_mthClearPatientRecordInfo();
					//ѡ�и��ڵ�
					m_blnCanTreeAfterSelect = false;
					//					this.trvTime.SelectedNode =this.trvTime.Nodes[0];
					m_mthUnEnableRichTextBox();  
					m_blnCanTreeAfterSelect = true;
					break;   
					//...
			}  
		
			//���ؽ��
			return lngRes;
		}

		protected void m_mthUseReAddNew(clsPatient p_objPatient,string p_strRecordTime)
		{
			//������
			if(p_objPatient==null || p_strRecordTime=="")
				return ;
 
			clsBaseCaseHistoryInfo  objContent=null;     
			clsPictureBoxValue[] objPicValueArr = null;
			//��ȡ��¼
            long lngRes = m_objDomain.m_lngGetRecordContent(p_objPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_StrEMRInPatientID, p_objPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmEMRInDate.ToString("yyyy-MM-dd hh:mm:ss"),/*p_strRecordTime,*/   out objContent, out objPicValueArr);
		
			if(lngRes <= 0 || objContent == null)
				return;          
			                               
			m_objReAddNewOld = (clsInPatientCaseHistoryContent )objContent;	                               
			m_objCurrentRecordContent = null;         
		
			//����ʱ��,��ʹ֮�����޸�
		
			m_mthReAddNewRecord((clsInPatientCaseHistoryContent )objContent);
			

		}

		// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
		protected virtual void m_mthReAddNewRecord(clsInPatientCaseHistoryContent p_objRecordContent)
		{
			//��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
		}

		// �������������ݿⱣ�档
		protected long m_lngReAddNew()
		{
			//��鵱ǰ���˱����Ƿ�Ϊnull
		
			//��ȡ������ʱ��
		
			//�ӽ����ȡ��¼��Ϣ
			clsInPatientCaseHistoryContent objContent = m_objGetContentFromGUI();     
		           
			//��������ֵ����           
			if(objContent == null)
				return -1;
		
			//���� clsInPatientCaseHistoryContent ����Ϣ��ʹ�÷�����ʱ������m_dtmOpenDate��m_dtmModifyDate��
		
			//����ǩ�� 
			//��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
			clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
            objSign_VO.m_strFORMID_VCHR = this.Name;
            objSign_VO.m_strFORMRECORDID_VCHR = objContent.m_strInPatientID.Trim() + "-" + objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"); 
            objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
            objSign_VO.m_strRegisterId = m_objBaseCurrentPatient.m_StrRegisterId;
            clsCheckSignersController objCheck = new clsCheckSignersController();
            if (objCheck.m_lngSign(objContent, objSign_VO) == -1)
                return -1;
			//����������¼
			clsPreModifyInfo m_objModifyInfo=null;
			long lngRes = m_objDomain.m_lngReAddNewRecord(m_objReAddNewOld,objContent,out m_objModifyInfo);
			
			//���ݽ������ͬ�Ĵ���
			switch((enmOperationResult)lngRes)
			{
				case enmOperationResult.DB_Succeed:
                    m_objCurrentRecordContent = objContent;
                    m_dtmCreatedDate = objContent.m_dtmOpenDate;
					m_objReAddNewOld = null;
					break;   
					//...
			}  
		
			//���ؽ��
			return lngRes;

		}

		// ����˫����
		#region ctlRichTextBox��˫���ߡ�������������
		/// <summary>
		/// ����˫����
		/// </summary>
		protected void m_mthSetRichTextBoxDoubleStrike()
		{
			//��ȡRichTextBox        
			//ctlRichTextBox objRichTextBox = (ctlRichTextBox)m_ctmRichTextBoxMenu.SourceControl;
		
			//objRichTextBox.m_mthSelectionDoubleStrikeThough(true);
			if(m_txtFocusedRichTextBox!=null)
			{
				if(m_txtFocusedRichTextBox is com.digitalwave.Utility.Controls.ctlRichTextBox)
					((com.digitalwave.Utility.Controls.ctlRichTextBox)m_txtFocusedRichTextBox).m_mthSelectionDoubleStrikeThough(true);	
				else if(m_txtFocusedRichTextBox is com.digitalwave.controls.ctlRichTextBox)
					((com.digitalwave.controls.ctlRichTextBox)m_txtFocusedRichTextBox).m_mthSelectionDoubleStrikeThough(true);
			}
		}

		/// <summary>
		/// ����RichTextBox���ԡ����Ҽ��˵����û��������û�ID����ɫ�ȣ���
		/// </summary>
		/// <param name="p_objRichTextBox"></param>
		protected void m_mthSetRichTextBoxAttrib(Control p_objRichTextBox)
		{
			if(p_objRichTextBox is com.digitalwave.Utility.Controls.ctlRichTextBox)
			{
                //m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{	(com.digitalwave.Utility.Controls.ctlRichTextBox)p_objRichTextBox });
				//�����Ҽ��˵�			
				//			p_objRichTextBox.ContextMenu=m_cmuRichTextBoxMenu;
				((com.digitalwave.Utility.Controls.ctlRichTextBox)p_objRichTextBox).GotFocus += new EventHandler(m_txtRichTextBox_GotFocus);
			
				//������������			
				((com.digitalwave.Utility.Controls.ctlRichTextBox)p_objRichTextBox).m_StrUserID = MDIParent.strOperatorID.Trim();
				((com.digitalwave.Utility.Controls.ctlRichTextBox)p_objRichTextBox).m_StrUserName = MDIParent.strOperatorName.Trim();
				((com.digitalwave.Utility.Controls.ctlRichTextBox)p_objRichTextBox).m_ClrOldPartInsertText = Color.Black;
				((com.digitalwave.Utility.Controls.ctlRichTextBox)p_objRichTextBox).m_ClrDST = Color.Red;
			}
			else if(p_objRichTextBox is com.digitalwave.controls.ctlRichTextBox)
			{
                //m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{	(com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox });
				//�����Ҽ��˵�			
				//			p_objRichTextBox.ContextMenu=m_cmuRichTextBoxMenu;
				((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).GotFocus += new EventHandler(m_txtRichTextBox_GotFocus);
			
				//������������			
				((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_StrUserID = MDIParent.strOperatorID.Trim();
				((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_StrUserName = MDIParent.strOperatorName.Trim();
				((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_ClrOldPartInsertText = Color.Black;
				((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_ClrDST = Color.Red;
			}
		}

		protected void m_mthSetRichTextBoxAttribInControl(Control p_ctlControl)
		{
			if(p_ctlControl.GetType().FullName=="com.digitalwave.controls.ctlRichTextBox")
			{
				m_mthSetRichTextBoxAttrib((com.digitalwave.controls.ctlRichTextBox)p_ctlControl);
			}
			else if(p_ctlControl.GetType().FullName=="com.digitalwave.Utility.Controls.ctlRichTextBox")
			{
				m_mthSetRichTextBoxAttrib((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlControl);
			}

			if(p_ctlControl.HasChildren && p_ctlControl.GetType().Name !="DataGrid" )
			{									
				foreach(Control subcontrol in p_ctlControl.Controls)
				{										
					m_mthSetRichTextBoxAttribInControl(subcontrol);						
				} 	
			}	
		}

		private void mniDoubleStrikeOutDelete_Click(object sender, System.EventArgs e)
		{
			m_mthSetRichTextBoxDoubleStrike();
		}
		private Control m_txtFocusedRichTextBox=null;//��ŵ�ǰ��ý����RichTextBox
		private void m_txtRichTextBox_GotFocus(object sender, System.EventArgs e)
		{
			m_txtFocusedRichTextBox=((Control)(sender));		
		}
		#endregion ctlRichTextBox��˫���ߡ�������������
		
		// ��ӡ

		#region �ⲿ��ӡ				
		protected virtual void m_pdcPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{			
			objPrintTool.m_mthPrintPage(e);

			if(ppdPrintPreview != null)
				while(!ppdPrintPreview.m_blnHandlePrint(e))
					objPrintTool.m_mthPrintPage(e);
		}

		protected virtual void m_pdcPrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			objPrintTool.m_mthBeginPrint(e);				
		}

		protected virtual void m_pdcPrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			objPrintTool.m_mthEndPrint(e);
		}

		protected infPrintRecord objPrintTool;
		private void m_mthPrint_FromDataSource()
		{
            if (clsEMRLogin.m_StrCurrentHospitalNO == "440104001")//��һ
			{
                objPrintTool = new clsInPatientCaseHistoryPrintTool();
			}
            else if (clsEMRLogin.m_StrCurrentHospitalNO == "450101001")//����
            {
                objPrintTool = new clsInPatientCaseHistory_GXPrintTool();
            }
            else//����
            {
                objPrintTool = new clsInPatientCaseHistory_F2PrintTool();
            }
			objPrintTool.m_mthInitPrintTool(null);
            if (m_objBaseCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,DateTime.MinValue,DateTime.MinValue);
            else if (m_objCurrentRecordContent == null)//(this.trvTime.SelectedNode ==null || this.trvTime.SelectedNode==trvTime.Nodes[0] || trvTime.SelectedNode.Tag==null)
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate, DateTime.MinValue);
			else
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,
                    m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate,
                    m_objCurrentRecordContent.m_dtmOpenDate);
			//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,m_objBaseCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate,DateTime.Parse(trvTime.SelectedNode.Tag.ToString()));
												
			objPrintTool.m_mthInitPrintContent();					
			
								
			m_mthStartPrint();
		}				
		#endregion �ⲿ��ӡ

		protected override long m_lngSubPrint()
		{
			m_mthPrint_FromDataSource();
			return 1;

			//			/*���û�����ù���ӡ���������ô�ӡ����
			//			 * ���ô�ӡ������ʱ�����m_mthInitPrintTool���潫m_objPrintContext��ֵ�����Դ�ӡ��֮��Ӧ
			//			 * ��m_blnAlreadySetPrintTools��Ϊfalse
			//			 */       
			//			if(!m_blnAlreadySetPrintTools)
			//			{
			//				m_mthInitPrintTool();
			//				m_blnAlreadySetPrintTools = true;
			//			}  		
			//
			//			//����Ƿ��д�ӡ���ݣ�����У���ӡ�����ݱ��������ӡ�ձ������ձ�����ֵ��
			//			if(m_objCurrentRecordContent != null)
			//			{	
			//
			//				//��������Ƿ����£���ȡ�������ݺ��״δ�ӡʱ��   
			//				clsBaseCaseHistoryInfo  objNewCaseInfo; 
			//				long lngRes = m_objDomain.m_lngGetPrintInfo(m_objCurrentRecordContent.m_strInPatientID,m_objCurrentRecordContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss")  ,m_objCurrentRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"),m_objCurrentRecordContent.m_dtmModifyDate,out objNewCaseInfo,out m_dtmFirstPrintDate,out m_blnIsFirstPrint);
			//				if(lngRes <= 0)
			//					return lngRes;  
			//			
			//				//���û���������������ݣ��ѵ�ǰ���ݼ�¼��objNewTrackInfo��
			//				if(objNewCaseInfo == null)
			//				{
			//					objNewCaseInfo = m_objCurrentRecordContent;
			//				}
			//		
			//				//���ñ����ݵ���ӡ��
			//				m_mthSetPrintContent((clsInPatientCaseHistoryContent )objNewCaseInfo,m_dtmFirstPrintDate);
			//			}  
			//				
			//			//��ʼ��ӡ
			//			m_mthStartPrint();
			//		
			//			return 1;
		}

		// ���ô�ӡ���ݡ�
		protected virtual void m_mthSetPrintContent(clsInPatientCaseHistoryContent p_objContent,DateTime p_dtmFirstPrintDate)
		{
			//ȱʡ�����κζ������Ӵ����������ṩ������
		}

		// ��ʼ����ӡ����
		protected virtual void m_mthInitPrintTool()
		{
			
			//ȱʡ�����κζ������Ӵ����������ṩ����
			//��ʼ�����ݰ������д�ӡʹ�õ��ı��������塢���ʡ���ˢ����ӡ��ȡ�
		}

		// �ͷŴ�ӡ����
		protected virtual void m_mthDisposePrintTools()
		{
			//ȱʡ�����κζ������Ӵ����������ṩ����
			//�ͷ����ݰ�����ӡʹ�õ������塢���ʡ���ˢ��ʹ��ϵͳ��Դ�ı�����
		}

		// ��ʼ��ӡ��
		protected PrintTool.frmPrintPreviewDialog ppdPrintPreview = new PrintTool.frmPrintPreviewDialog();
		//		private PrintPreviewDialog ppdPrintPreview = new PrintPreviewDialog();
		protected virtual void m_mthStartPrint()
		{
			//ȱʡʹ�ô�ӡԤ�����Ӵ��������ṩ�µ�ʵ��
			if(m_blnDirectPrint)
			{
				m_pdcPrintDocument.Print();
			}
			else
			{
				ppdPrintPreview.Document = m_pdcPrintDocument;
				ppdPrintPreview.ShowDialog();
			}
		}

		// ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
		protected void m_mthBeginPrint(PrintEventArgs p_objPrintArg)
		{
			m_mthBeginPrintSub(p_objPrintArg);
		}

		// ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
		protected virtual void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
		{
			//ȱʡ�����κζ������Ӵ����������ṩ����
		}

		// ��ӡҳ
		protected void m_mthPrintPage(PrintPageEventArgs p_objPrintPageArg)
		{
			m_mthPrintPageSub(p_objPrintPageArg);
		}

		// ��ӡҳ
		protected virtual void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
		{
		
		}

		// ��ӡ����ʱ�Ĳ���
		protected void m_mthEndPrint(PrintEventArgs p_objPrintArg)
		{
			//�����ӡ�ɹ������Ҳ��Ǵ�ӡ�ձ���������Ҫ�����״δ�ӡʱ�䣬�����״δ�ӡʱ�䡣
			if(!p_objPrintArg.Cancel && m_objCurrentRecordContent != null && m_blnIsFirstPrint)
			{
				m_objDomain.m_lngUpdateFirstPrintDate(m_objCurrentRecordContent.m_strInPatientID,m_objCurrentRecordContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),m_objCurrentRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss")  ,m_dtmFirstPrintDate);
				m_blnIsFirstPrint = false;
			}                          
		
			m_mthEndPrintSub(p_objPrintArg);
		}

		// ��ӡ����ʱ�Ĳ���
		protected virtual void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
		{
			//���Ӵ����������ṩ����
		}

		// ��ʾ�Ѿ�ɾ���ļ�¼���û�ѡ�񣬲����û�ѡ���������������Ϊ��ȫ��ȷ�����ݣ���ʾ�ڽ��档
		protected void m_mthLoadDeleteRecord()
		{
			//			frmSelectDeleteRecord frmSel = new frmSelectDeleteRecord(m_objDomain,m_objCurrentPatient,m_strReloadFormTitle(),MDIParent.OperatorID);
			//		
			//			if(frmSel.ShowDialog() == DialogResult.OK)
			//			{   
			//			
			//				clsInPatientCaseHistoryContent objContent = frmSel.m_objGetDeleteRecord();              
			//			
			//				if(objContent == null)
			//					return;   
			//				         
			//				//��ʾ�Ḳ�ǵ�ǰ����
			//				//����û�������
			//				//return;
			//		              
			//				m_mthClearPatientRecordInfo();                     
			//			                             
			//				m_mthReAddNewRecord(objContent);
			//			}
		}

		public void Copy()
		{
			m_lngCopy();
		}

		public void Cut()
		{
			m_lngCut();
		}
		public void Verify()
		{
			try
			{
				//��鵱ǰ���˱����Ƿ�Ϊnull 
				if(m_objCurrentPatient == null)
				{
					clsPublicFunction.ShowInformationMessageBox("δѡ������,�޷���֤!");
				}
				string strInPatientID =m_objCurrentPatient.m_StrInPatientID ;
				string strInPatientDate =m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmEMRInDate.ToString("yyyy-MM-dd HH:mm:ss");
				string strRecordID=strInPatientID.Trim()+"-"+strInPatientDate;
				long lngRes=m_lngSignVerify(this.Name.Trim(),strRecordID);
			}
			catch (Exception exp)
			{ 
				MessageBox.Show("ǩ����֤�����쳣��"+exp.Message,"Message", MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
 			
		}
		public void Delete()
        {
            if (m_blnReadOnly)
            {
                clsPublicFunction.ShowInformationMessageBox("�˲���Ϊֻ��������ɾ����");
                return;
            }
            //ָ��������Ϊҽ������վ
            //intFormType = 1;
			long m_lngRe=m_lngDelete(); 
			if(m_lngRe>0)

			{
                m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
                //if(this.trvTime.SelectedNode!=null)
                //{
                //    this.trvTime_AfterSelect(this.trvTime,new TreeViewEventArgs(this.trvTime.SelectedNode));
                //}
			}

		}

		public void Display()
		{
		
		}

		public void Display(string cardno, string sendcheckdate)
		{
		
		}

		public void Paste()
		{
			m_lngPaste();
		}

		public void Print()
		{
			m_lngPrint(); 
		}

		public void Redo()
		{
		
		}

		public void Save()
		{
			long m_lngRe=m_lngSave(); 
			if(m_lngRe==99)
			{
				return;
			}
			if(m_lngRe>0)
			{
                //if(this.trvTime.SelectedNode!=null)
                //{
                //    m_blnNeedCheckArchive = false;
                //    this.trvTime_AfterSelect(this.trvTime,new TreeViewEventArgs(this.trvTime.SelectedNode));
                //    m_blnNeedCheckArchive = true;
                //}
                m_blnNeedCheckArchive = false;
                m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
                m_blnNeedCheckArchive = true;
				clsPublicFunction.ShowInformationMessageBox("����ɹ���");
			}
			else
				clsPublicFunction.ShowInformationMessageBox("����ʧ�ܣ�");
		}


		protected override enmFormState m_EnmCurrentFormState
		{
			get
			{
				return enmFormState.NowUser ;
			}
		}		
		public void Undo()
		{
		
		}

		protected virtual void m_mthUnEnableRichTextBox()
		{
		}

		protected virtual void m_mthEnableRichTextBox()
		{
		}


		protected virtual void trvTime_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if(!m_blnCanTreeAfterSelect)
				return;

			m_mthRecordChangedToSave();

			try
            {
                DateTime dtmInDate = DateTime.Parse(trvTime.SelectedNode.Text);

                txtInPatientID.Text = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_StrHISInPatientID;
                //DateTime dtmInDate = DateTime.Parse(trvTime.SelectedNode.Text);
                //m_objBaseCurrentPatient.m_DtmSelectedInDate = dtmInDate;
                //m_objCurrentPatient.m_DtmSelectedInDate = dtmInDate;
                m_mthOnlySetPatientInfo(m_objCurrentPatient);

                m_objCurrentPatient.m_ObjPeopleInfo = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_ObjPeopleInfo;

                DateTime dtmEMRInDate = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_DtmEMRInDate;
                string strEMRInPatientID = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_StrEMRInPatientID;

                m_objCurrentPatient.m_StrHISInPatientID = txtInPatientID.Text;
                m_objCurrentPatient.m_DtmSelectedHISInDate = Convert.ToDateTime(trvTime.SelectedNode.Text);
                if (dtmEMRInDate != new DateTime(1900, 1, 1))
                {
                    m_objBaseCurrentPatient.m_DtmSelectedInDate = dtmEMRInDate;
                    m_objCurrentPatient.m_DtmSelectedInDate = dtmEMRInDate;
                }
                if (string.IsNullOrEmpty(m_objBaseCurrentPatient.m_StrRegisterId))
                {
                    string strRegisterID = string.Empty;
                    long lngRes = new clsPublicDomain().m_lngGetRegisterID(m_objCurrentPatient.m_StrPatientID, Convert.ToDateTime(trvTime.SelectedNode.Text).ToString("yyyy-MM-dd HH:mm:ss"), out strRegisterID);
                    m_objBaseCurrentPatient.m_StrRegisterId = strRegisterID;
                    m_objCurrentPatient.m_StrRegisterId = strRegisterID;

                }

//				MDIParent.s_ObjCurrentPatient.m_DtmSelectedInDate = dtmInDate;
				m_mthClearRecordInfo();
                m_mthIsReadOnly();

                if (!m_blnCanShowRecordContent())
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

				m_mthEnableRichTextBox();
			
                //m_mthSetSelectedRecord(m_objCurrentPatient,(string)this.trvTime.SelectedNode.Tag );
				if(m_objCurrentRecordContent!=null)
				{
					this.m_dtpCreateDate.Enabled=false;

					//��ǰ�����޸ļ�¼״̬
					MDIParent.m_mthChangeFormText(this,MDIParent.enmFormEditStatus.Modify );
				}
				else
				{
					m_mthSetNewRecord();
					//��ǰ����������¼״̬
					MDIParent.m_mthChangeFormText(this,MDIParent.enmFormEditStatus.AddNew);
				}
			}
			catch (Exception exp)
			{
				string strtemp=exp.Message;
				m_mthClearRecordInfo();

				m_mthUnEnableRichTextBox();

				m_objCurrentRecordContent =null;
				m_mthEnableModify(true);
				this.m_dtpCreateDate.Enabled =true;
				this.m_dtpCreateDate.Text =DateTime.Now.ToString("yyyy��MM��dd�� HH:mm:ss");  
				
				m_mthSetNullPrintContext();

				//��ǰ���ڽ�ֹ����״̬
				MDIParent.m_mthChangeFormText(this,MDIParent.enmFormEditStatus.None );
			}
			finally
			{
				m_mthDoAfterSelect();
				m_mthAddFormStatusForClosingSave();
			}
		}

		/// <summary>
		/// ���������¼
		/// </summary>
		protected virtual void m_mthSetNewRecord()
		{
		}

		protected virtual void m_mthSetNullPrintContext()
		{
		}

		private void frmBaseCaseHistory_Load(object sender, System.EventArgs e)
		{
			//			m_mthSetRichTextBoxAttribInControl(this);

		}

		
		/// <summary>
		/// ���ô����пؼ������ı�����ɫ
		/// </summary>
		/// <param name="p_ctlControl"></param>
		/// <param name="p_clrColor"></param>
		protected void m_mthSetRichTextModifyColor(Control p_ctlControl,System.Drawing.Color p_clrColor)
		{
			#region ���ÿؼ������ı�����ɫ,Jacky-2003-3-24	
			string strTypeName = p_ctlControl.GetType().FullName;			
			if(strTypeName=="com.digitalwave.Utility.Controls.ctlRichTextBox")			
				((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlControl).m_ClrOldPartInsertText = p_clrColor;
			else if(strTypeName=="com.digitalwave.controls.ctlRichTextBox")
				((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_ClrOldPartInsertText = p_clrColor;
			
			if(p_ctlControl.HasChildren && strTypeName !="System.Windows.Forms.DataGrid" )
			{									
				foreach(Control subcontrol in p_ctlControl.Controls)
				{										
					m_mthSetRichTextModifyColor(subcontrol,p_clrColor);					
				} 	
			}						
			#endregion			
		}
		
		
		protected void m_mthSetRichTextCanModifyLast(Control p_ctlControl,bool p_blnCanModifyLast )
		{
			#region ���ÿؼ������ı����Ƿ�����޸�,Jacky-2003-3-24	
			string strTypeName = p_ctlControl.GetType().FullName;			
			if(strTypeName=="com.digitalwave.Utility.Controls.ctlRichTextBox")
			{				
				((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlControl).m_BlnCanModifyLast = p_blnCanModifyLast;
			}
			else if(strTypeName=="com.digitalwave.controls.ctlRichTextBox")
			{
				((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_BlnCanModifyLast = p_blnCanModifyLast;
			}
			
			if(p_ctlControl.HasChildren && strTypeName !="System.Windows.Forms.DataGrid" )
			{									
				foreach(Control subcontrol in p_ctlControl.Controls)
				{										
					m_mthSetRichTextCanModifyLast(subcontrol,p_blnCanModifyLast);					
				} 	
			}						
			#endregion			
		}			
		/// <summary>
		/// ������ڣ�������ɫ�����÷���
		/// ����ü�¼������޸��˾��ǵ�ǰ�ĵ�½�ˣ������޸ĸü�¼
		/// ���򣬲����޸ģ�����6Сʱ�Ŀ��ƣ���liyi��richtextbox�����п��ƣ�
		/// </summary>
		/// <returns></returns>
		protected bool m_blnGetCanModifyLast(string p_strModifyUserID)
		{			
			if(p_strModifyUserID==null || p_strModifyUserID.Trim() == MDIParent.OperatorID.Trim())
				return true;
			else 
				return false;
		}

		/// <summary>
		/// ѡ�����ڵ��Ĳ���
		/// </summary>
		protected virtual void m_mthDoAfterSelect()
		{
			return;
		}

        /// <summary>
        /// ��д��ȡ��¼����������
        /// ����ָ����¼������ID
        /// </summary>
        protected override string m_StrRecorder_ID
        {
            get
            {
                clsInPatientCaseHistoryContent objContent = m_objGetCreateUserFromGUI();//m_objGetContentFromGUI();
                return objContent.m_strCreateUserID.Trim();
            }
        }
		/// <summary>
		/// �ṩ�Ӵ�����ֶ���Դ�ͷ�
		/// </summary>
//		protected override void m_mthReleaseSub()
//		{
//			if(m_objBorderTool != null)
////				m_objBorderTool.m_mthClear();
//			base.m_mthReleaseSub();
//		}
        protected override void m_mthPerformSessionChanged(clsEmrPatientSessionInfo_VO p_objSelectedSession, int p_intIndex)
        {
            if (p_objSelectedSession == null)
            {
                return;
            }

            if (!m_blnCanTreeAfterSelect)
                return;

            m_mthRecordChangedToSave();

            try
            {               
                string strEMRInPatientID = p_objSelectedSession.m_strEMRInpatientId;

                m_objCurrentPatient.m_StrHISInPatientID = p_objSelectedSession.m_strHISInpatientId;
                m_objCurrentPatient.m_DtmSelectedHISInDate = p_objSelectedSession.m_dtmHISInpatientDate;

                m_objBaseCurrentPatient.m_DtmSelectedInDate = p_objSelectedSession.m_dtmEMRInpatientDate;
                m_objCurrentPatient.m_DtmSelectedInDate = p_objSelectedSession.m_dtmEMRInpatientDate;
                m_objBaseCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;
                m_objCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;

                m_mthOnlySetPatientInfo(m_objCurrentPatient);

                m_mthClearRecordInfo();
                m_mthIsReadOnly();

                if (!m_blnCanShowRecordContent())
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                m_mthEnableRichTextBox();

                m_mthSetSelectedRecord(m_objCurrentPatient);
                if (m_objCurrentRecordContent != null)
                {
                    this.m_dtpCreateDate.Enabled = false;

                    //��ǰ�����޸ļ�¼״̬
                    MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.Modify);
                }
                else
                {
                    m_mthSetNewRecord();
                    //��ǰ����������¼״̬
                    MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.AddNew);
                }
            }
            catch (Exception exp)
            {
                string strtemp = exp.Message;
                m_mthClearRecordInfo();

                m_mthUnEnableRichTextBox();

                m_objCurrentRecordContent = null;
                m_mthEnableModify(true);
                this.m_dtpCreateDate.Enabled = true;
                this.m_dtpCreateDate.Text = DateTime.Now.ToString("yyyy��MM��dd�� HH:mm:ss");

                m_mthSetNullPrintContext();

                //��ǰ���ڽ�ֹ����״̬
                MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.None);
            }
            finally
            {
                m_mthDoAfterSelect();
                m_mthAddFormStatusForClosingSave();
            }
        }
		
	}
}

