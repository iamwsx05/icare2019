using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;
using com.digitalwave.Emr.Signature_gui;
namespace iCare
{
	/// <summary>
	/// 1�������һ�δ�ӡʱ�䲻���ڣ����Ե�ǰʱ��Ϊ׼����Ϊ�Ƿ����޸ĺۼ���ʱ�����
	/// 2�������һ�δ�ӡʱ����ڣ����Ե�һ�δ�ӡʱ��Ϊ׼����Ϊ�Ƿ����޸ĺۼ���ʱ����ޣ����¼���PrintDateMiddle)�������ƶϳ������Ժʱ�������ʱ���޹أ�
	/// 3������[�ӱ��е������޸ļ�¼Ϊ����Դ�ٸ�����һ����¼�Ƿ���ͬ��Ϊ�޸����ݵ�]������ӡ����Ӧ��PrintDateMiddle��Ϊ��ѯ�ӱ������֮һ�������޸�ʱ��ModifyDate���� PrintDateMiddleʱ������ȡ�ü�¼ + С��PrintDateMiddle�ļ�¼����ȡ����޸�ʱ���һ����¼��
	/// 4������[PrintContext.m_mthSetContextWithCorrectBefore()]�ķ�����ӡ������PrintDateMiddle��Ϊ���е�ʱ��������ɡ�
	/// �������PrintDateMiddle>DateTime.Now,����3��4��ѯʱ����ѯ����ȼ���ֻ��ӡ���¼�¼��
	/// ����������3�ķ�ʽ��ӡ������ʱһ��һ�ŵ���Ӧ������ͬ��CreateDate,�ж��FirstPrintDate),������Ҫ�޸�Middle Tier �е�SQL��ѯ���������漰Domain�㲻�䡣������Ҫ�ڽ����ѯ�״δ�ӡʱ�䣬����Ҫ��ӡʱ��Ϊ�գ������״δ�ӡʱ�䣩
	////////����4�ķ�ʽ��ӡ����ȡFirstPrintDate����						
	///��ʵ����4�ķ�ʽ��ӡ��һ�ŵ���Ӧһ��CreateDate,����һ��FirstPrintDate��
	/// </summary>
	public class frmBeforeOperationSummary : frmHRPBaseForm,PublicFunction
	{
		#region define
		private System.Windows.Forms.Label lblDiagnose;
		private System.Windows.Forms.Label lblDiagnoseGist;
		private System.Windows.Forms.Label lblPatientNotion;
		private System.Windows.Forms.Label lblBodyInfo;
		private System.Windows.Forms.Label lblOperationDate;
		private System.Windows.Forms.Label lblDiscussNotion;
		private System.Windows.Forms.Label lblAfterNotice;
		private System.Windows.Forms.Label lblPreparation;
		private System.Windows.Forms.Label lblSpecialHandle;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private TextBox m_txtOperateDoctor;
		private TextBox m_txtChargeDoctor;
		private com.digitalwave.controls.ctlRichTextBox m_txtDiagnoseGist;
		private com.digitalwave.controls.ctlRichTextBox m_txtBodyInfo;
		private com.digitalwave.controls.ctlRichTextBox m_txtSpecialHandle;
		private com.digitalwave.controls.ctlRichTextBox m_txtPatientNotion;
		private com.digitalwave.controls.ctlRichTextBox m_txtPreparation;
		private com.digitalwave.controls.ctlRichTextBox m_txtAnaesthesia;
		private com.digitalwave.controls.ctlRichTextBox m_txtAfterNotice;
		private com.digitalwave.controls.ctlRichTextBox m_txtDiscussNotion;
		private com.digitalwave.Utility.Controls.ctlTimePicker m_dtpOeprationTime;
		private System.Windows.Forms.TreeView m_trvInOperationDate;
		private System.Windows.Forms.MenuItem mniDoubleStrikeOutDelete;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.ComponentModel.IContainer components = null;
		#endregion
		private PinkieControls.ButtonXP m_cmdOperationDoctor;

        private clsEmployeeSignTool m_objSignTool;
		private PinkieControls.ButtonXP m_cmdChargeDoctor;
		private PinkieControls.ButtonXP m_cmdAnaesthesia;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private com.digitalwave.Utility.Controls.ctlTimePicker m_dtpOperationDate;
		private Crownwood.Magic.Controls.TabControl tabControl1;
		private Crownwood.Magic.Controls.TabPage tabPage1;
		private Crownwood.Magic.Controls.TabPage tabPage2;
		private Crownwood.Magic.Controls.TabPage tabPage3;
		private com.digitalwave.controls.ctlRichTextBox m_txtDiagnose;
		private System.Windows.Forms.ImageList imageList1;
        private com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
        private clsEmrSignToolCollection m_objSign;
        private clsCommonUseToolCollection m_objCUTC;

		public frmBeforeOperationSummary()
		{
			try
			{
				// This call is required by the Windows Form Designer.
				InitializeComponent();
                //ָ��ҽ������վ��
                intFormType = 1;
				// TODO: Add any initialization after the InitializeComponent call

                //m_objBorderTool = new clsBorderTool(Color.White);
                //m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{m_trvInOperationDate });

				m_objBOSDomain = new clsBeforeOperationSummaryDomain();		

				m_bytListOnDoctor = 0;

				m_blnCanDoctorTextChanged = true;

				m_blnCanOperateDoctorSelect = true;
				m_blnCanChargeDoctorSelect = true;

				m_trnRoot = new TreeNode("��¼����");
				m_trvInOperationDate.Nodes.Add(m_trnRoot);

				m_mthSetRichTextBoxAttribInControl(this);

				m_objPublicDomain = new clsPublicDomain();

				//����ֵ
                m_objCUTC = new clsCommonUseToolCollection(this);
				m_objCUTC.m_mthBindControl(new Control[]{m_cmdAnaesthesia},
					new Control[]{m_txtAnaesthesia},
					new enmCommonUseValue[]{enmCommonUseValue.frmOperationRecordDoctor_CategoryDosage});

                m_objSign = new clsEmrSignToolCollection();
                m_objSign.m_mthBindEmployeeSign(m_cmdOperationDoctor, m_txtOperateDoctor, 1, false, clsEMRLogin.LoginInfo.m_strEmpID);
                m_objSign.m_mthBindEmployeeSign(m_cmdChargeDoctor, m_txtChargeDoctor, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);				
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private clsPublicDomain m_objPublicDomain;

		/// <summary>
		/// �߿���ɫ����
		/// </summary>
        //private clsBorderTool m_objBorderTool;

		/// <summary>
		/// ��ǰС��������
		/// </summary>
		private clsBeforeOperationSummaryDomain m_objBOSDomain;

		/// <summary>
		/// ����һ��ҽ��������ѡ��0������ҽʦ��1������ҽʦ��
		/// </summary>
		private byte m_bytListOnDoctor;

		/// <summary>
		/// �Ƿ���ҽ����TextChanged�¼�
		/// </summary>
		private bool m_blnCanDoctorTextChanged;

		/// <summary>
		/// �Ƿ�ȫѡ����ҽʦ����������
		/// </summary>
		private bool m_blnCanOperateDoctorSelect;

		/// <summary>
		/// �Ƿ�ȫѡ����ҽʦ����������
		/// </summary>
		private bool m_blnCanChargeDoctorSelect;		

		private clsBeforeOperationSummary_All objclsBeforeOperationSummary_All=null;
		private clsPatient objCurrentPatient=null;

		/// <summary>
		/// �������ڵ������
		/// </summary>
		private TreeNode m_trnRoot;	
	
		#region ���峣�ø��Ӻ���		

		#region ���RichText��������(������DataGrid�ı߿�����)
		private com.digitalwave.controls.ctlRichTextBox m_txtFocusedRichTextBox=null;//��ŵ�ǰ��ý����RichTextBox
		private void m_mthSetRichTextAttrib()
		{			
			m_mthSetRichTextEvent(this);			
		}
		
		private void m_mthSetRichTextEvent(Control p_ctlControl)
		{
			#region ���õݹ���ã���ȡ���������н���ctlRichTextBox�ؼ��ĸ�������,Jacky-2003-2-25				
			if(p_ctlControl.GetType().Name=="ctlRichTextBox")
			{
                //m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{	p_ctlControl });
//				p_ctlControl.ContextMenu=m_cmuRichTextBoxMenu;
				p_ctlControl.GotFocus += new EventHandler(m_txtRichTextBox_GotFocus);

				((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_StrUserID = MDIParent.strOperatorID;
				((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_StrUserName = MDIParent.strOperatorName;
				((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_ClrOldPartInsertText = Color.White;
				((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_ClrDST = Color.Red;
				m_mthAddRichTextInfo( (com.digitalwave.controls.ctlRichTextBox)p_ctlControl );
			}
			
			if(p_ctlControl.HasChildren && p_ctlControl.GetType().Name !="DataGrid" )
			{									
				foreach(Control subcontrol in p_ctlControl.Controls)
				{										
					m_mthSetRichTextEvent(subcontrol);						
				} 	
			}				
					
			#endregion
		}

		private void mniDoubleStrikeOutDelete_Click(object sender, System.EventArgs e)
		{
			if(m_txtFocusedRichTextBox!=null)
				m_txtFocusedRichTextBox.m_mthSelectionDoubleStrikeThough(true);			
		}

		private void m_txtRichTextBox_GotFocus(object sender, System.EventArgs e)
		{
			m_txtFocusedRichTextBox=((com.digitalwave.controls.ctlRichTextBox)(sender));
		}

		#endregion

		#region ��Ӽ��̿�ݼ�
		private void m_mthSetQuickKeys()
		{			
			m_mthSetControlEvent(this);			
		}
		
		private void m_mthSetControlEvent(Control p_ctlControl)
		{
			#region ���õݹ���ã���ȡ���������н����¼�,Jacky-2003-2-21	
			string strTypeName = p_ctlControl.GetType().Name;
			if(strTypeName != "Lable" && strTypeName != "Button")
			{
				p_ctlControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);
				if(p_ctlControl.HasChildren && strTypeName !="DataGrid" && strTypeName !="DateTimePicker" && strTypeName !="ctlComboBox")
				{									
					foreach(Control subcontrol in p_ctlControl.Controls)
					{										
						m_mthSetControlEvent(subcontrol);						
					} 	
				}				
			}			
			#endregion
		}
		
		private void m_mthEvent_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch(e.KeyValue)
			{//F1 112  ����, F2 113 Save��F3  114 Del��F4 115 Print��F5 116 Refresh��F6 117 Search
				case 13:// enter				
					
//					if(sender.GetType().Name!="ctlRichTextBox")
//						SendKeys.Send(  "{tab}");
					break;

				case 38:
				case 40:
					break;	

				
				case 113://save
					this.m_lngSave(); 
					break;
				case 114://del
					this.m_lngDelete(); 
					break;
				case 115://print
					this.m_lngPrint();
					break;
				case 116://refresh
					m_mthClearAll();
					break;
				case 117://Search					
					break;
			}	
		}
		#endregion
		#endregion ���峣�ø��Ӻ���

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBeforeOperationSummary));
            this.lblDiagnose = new System.Windows.Forms.Label();
            this.lblDiagnoseGist = new System.Windows.Forms.Label();
            this.lblPatientNotion = new System.Windows.Forms.Label();
            this.lblBodyInfo = new System.Windows.Forms.Label();
            this.lblOperationDate = new System.Windows.Forms.Label();
            this.lblDiscussNotion = new System.Windows.Forms.Label();
            this.lblAfterNotice = new System.Windows.Forms.Label();
            this.lblPreparation = new System.Windows.Forms.Label();
            this.lblSpecialHandle = new System.Windows.Forms.Label();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.m_txtOperateDoctor = new System.Windows.Forms.TextBox();
            this.m_txtChargeDoctor = new System.Windows.Forms.TextBox();
            this.m_txtDiagnoseGist = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtBodyInfo = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtSpecialHandle = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtPatientNotion = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtPreparation = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtAnaesthesia = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtAfterNotice = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtDiscussNotion = new com.digitalwave.controls.ctlRichTextBox();
            this.m_dtpOeprationTime = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.m_trvInOperationDate = new System.Windows.Forms.TreeView();
            this.mniDoubleStrikeOutDelete = new System.Windows.Forms.MenuItem();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.m_cmdOperationDoctor = new PinkieControls.ButtonXP();
            this.m_cmdChargeDoctor = new PinkieControls.ButtonXP();
            this.m_cmdAnaesthesia = new PinkieControls.ButtonXP();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_dtpOperationDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.tabControl1 = new Crownwood.Magic.Controls.TabControl();
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.m_txtDiagnose = new com.digitalwave.controls.ctlRichTextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage3 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.m_pnlNewBase.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(338, 155);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(359, 169);
            this.lblAge.Size = new System.Drawing.Size(56, 19);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(329, 188);
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(338, 196);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(352, 159);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(329, 182);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(344, 153);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(329, 196);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(303, 143);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(104, 104);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(341, 156);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(303, 162);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(362, 167);
            this.m_txtBedNO.Size = new System.Drawing.Size(92, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(290, 170);
            this.m_cboArea.Size = new System.Drawing.Size(120, 23);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(309, 143);
            this.m_lsvPatientName.Visible = false;
            this.m_lsvPatientName.SelectedIndexChanged += new System.EventHandler(this.m_lsvPatientName_SelectedIndexChanged);
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(317, 155);
            this.m_lsvBedNO.Size = new System.Drawing.Size(108, 104);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(310, 170);
            this.m_cboDept.Size = new System.Drawing.Size(120, 23);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(314, 170);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(310, 196);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdNext.Location = new System.Drawing.Point(347, 155);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(332, 185);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(325, 173);
            this.m_lblForTitle.Text = "�� ǰ С ��";
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(510, 182);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(570, 38);
            this.m_cmdModifyPatientInfo.Visible = true;
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Location = new System.Drawing.Point(7, 8);
            this.m_pnlNewBase.Size = new System.Drawing.Size(809, 89);
            this.m_pnlNewBase.Visible = true;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.m_ctlPatientInfo.Dock = System.Windows.Forms.DockStyle.None;
            this.m_ctlPatientInfo.Location = new System.Drawing.Point(193, 29);
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(615, 59);
            // 
            // lblDiagnose
            // 
            this.lblDiagnose.AutoSize = true;
            this.lblDiagnose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDiagnose.ForeColor = System.Drawing.Color.Black;
            this.lblDiagnose.Location = new System.Drawing.Point(20, 8);
            this.lblDiagnose.Name = "lblDiagnose";
            this.lblDiagnose.Size = new System.Drawing.Size(42, 14);
            this.lblDiagnose.TabIndex = 511;
            this.lblDiagnose.Text = "���:";
            // 
            // lblDiagnoseGist
            // 
            this.lblDiagnoseGist.AutoSize = true;
            this.lblDiagnoseGist.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDiagnoseGist.ForeColor = System.Drawing.Color.Black;
            this.lblDiagnoseGist.Location = new System.Drawing.Point(20, 140);
            this.lblDiagnoseGist.Name = "lblDiagnoseGist";
            this.lblDiagnoseGist.Size = new System.Drawing.Size(70, 14);
            this.lblDiagnoseGist.TabIndex = 511;
            this.lblDiagnoseGist.Text = "�������:";
            // 
            // lblPatientNotion
            // 
            this.lblPatientNotion.AutoSize = true;
            this.lblPatientNotion.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientNotion.Location = new System.Drawing.Point(20, 272);
            this.lblPatientNotion.Name = "lblPatientNotion";
            this.lblPatientNotion.Size = new System.Drawing.Size(182, 14);
            this.lblPatientNotion.TabIndex = 511;
            this.lblPatientNotion.Text = "���߼�������λ���������:";
            // 
            // lblBodyInfo
            // 
            this.lblBodyInfo.AutoSize = true;
            this.lblBodyInfo.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBodyInfo.ForeColor = System.Drawing.Color.Black;
            this.lblBodyInfo.Location = new System.Drawing.Point(20, 272);
            this.lblBodyInfo.Name = "lblBodyInfo";
            this.lblBodyInfo.Size = new System.Drawing.Size(84, 14);
            this.lblBodyInfo.TabIndex = 511;
            this.lblBodyInfo.Text = "������Ӧ֢:";
            // 
            // lblOperationDate
            // 
            this.lblOperationDate.BackColor = System.Drawing.Color.Transparent;
            this.lblOperationDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOperationDate.ForeColor = System.Drawing.Color.Black;
            this.lblOperationDate.Location = new System.Drawing.Point(494, 76);
            this.lblOperationDate.Name = "lblOperationDate";
            this.lblOperationDate.Size = new System.Drawing.Size(70, 14);
            this.lblOperationDate.TabIndex = 511;
            this.lblOperationDate.Text = "��¼����:";
            // 
            // lblDiscussNotion
            // 
            this.lblDiscussNotion.AutoSize = true;
            this.lblDiscussNotion.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDiscussNotion.Location = new System.Drawing.Point(20, 272);
            this.lblDiscussNotion.Name = "lblDiscussNotion";
            this.lblDiscussNotion.Size = new System.Drawing.Size(98, 14);
            this.lblDiscussNotion.TabIndex = 511;
            this.lblDiscussNotion.Text = "��ǰ�������:";
            // 
            // lblAfterNotice
            // 
            this.lblAfterNotice.AutoSize = true;
            this.lblAfterNotice.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAfterNotice.Location = new System.Drawing.Point(20, 140);
            this.lblAfterNotice.Name = "lblAfterNotice";
            this.lblAfterNotice.Size = new System.Drawing.Size(70, 14);
            this.lblAfterNotice.TabIndex = 511;
            this.lblAfterNotice.Text = "����ע��:";
            // 
            // lblPreparation
            // 
            this.lblPreparation.AutoSize = true;
            this.lblPreparation.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreparation.ForeColor = System.Drawing.Color.Black;
            this.lblPreparation.Location = new System.Drawing.Point(20, 140);
            this.lblPreparation.Name = "lblPreparation";
            this.lblPreparation.Size = new System.Drawing.Size(70, 14);
            this.lblPreparation.TabIndex = 511;
            this.lblPreparation.Text = "��ǰ׼��:";
            // 
            // lblSpecialHandle
            // 
            this.lblSpecialHandle.AutoSize = true;
            this.lblSpecialHandle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpecialHandle.ForeColor = System.Drawing.Color.Black;
            this.lblSpecialHandle.Location = new System.Drawing.Point(20, 8);
            this.lblSpecialHandle.Name = "lblSpecialHandle";
            this.lblSpecialHandle.Size = new System.Drawing.Size(350, 14);
            this.lblSpecialHandle.TabIndex = 511;
            this.lblSpecialHandle.Text = "����������ʽ������ע��������������Ԥ��������:";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 100;
            // 
            // m_txtOperateDoctor
            // 
            this.m_txtOperateDoctor.AccessibleDescription = "����ҽʦǩ��";
            this.m_txtOperateDoctor.AccessibleName = "NoDefault";
            this.m_txtOperateDoctor.BackColor = System.Drawing.Color.White;
            this.m_txtOperateDoctor.ForeColor = System.Drawing.Color.Black;
            this.m_txtOperateDoctor.Location = new System.Drawing.Point(275, 560);
            this.m_txtOperateDoctor.Name = "m_txtOperateDoctor";
            this.m_txtOperateDoctor.ReadOnly = true;
            this.m_txtOperateDoctor.Size = new System.Drawing.Size(104, 23);
            this.m_txtOperateDoctor.TabIndex = 590;
            // 
            // m_txtChargeDoctor
            // 
            this.m_txtChargeDoctor.AccessibleDescription = "����ҽʦǩ��";
            this.m_txtChargeDoctor.AccessibleName = "NoDefault";
            this.m_txtChargeDoctor.BackColor = System.Drawing.Color.White;
            this.m_txtChargeDoctor.ForeColor = System.Drawing.Color.Black;
            this.m_txtChargeDoctor.Location = new System.Drawing.Point(526, 560);
            this.m_txtChargeDoctor.Name = "m_txtChargeDoctor";
            this.m_txtChargeDoctor.ReadOnly = true;
            this.m_txtChargeDoctor.Size = new System.Drawing.Size(104, 23);
            this.m_txtChargeDoctor.TabIndex = 600;
            // 
            // m_txtDiagnoseGist
            // 
            this.m_txtDiagnoseGist.AccessibleDescription = "�������";
            this.m_txtDiagnoseGist.BackColor = System.Drawing.Color.White;
            this.m_txtDiagnoseGist.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDiagnoseGist.ForeColor = System.Drawing.Color.Black;
            this.m_txtDiagnoseGist.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtDiagnoseGist.Location = new System.Drawing.Point(20, 160);
            this.m_txtDiagnoseGist.m_BlnIgnoreUserInfo = false;
            this.m_txtDiagnoseGist.m_BlnPartControl = false;
            this.m_txtDiagnoseGist.m_BlnReadOnly = false;
            this.m_txtDiagnoseGist.m_BlnUnderLineDST = false;
            this.m_txtDiagnoseGist.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtDiagnoseGist.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtDiagnoseGist.m_IntCanModifyTime = 6;
            this.m_txtDiagnoseGist.m_IntPartControlLength = 0;
            this.m_txtDiagnoseGist.m_IntPartControlStartIndex = 0;
            this.m_txtDiagnoseGist.m_StrUserID = "";
            this.m_txtDiagnoseGist.m_StrUserName = "";
            this.m_txtDiagnoseGist.Name = "m_txtDiagnoseGist";
            this.m_txtDiagnoseGist.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtDiagnoseGist.Size = new System.Drawing.Size(740, 100);
            this.m_txtDiagnoseGist.TabIndex = 518;
            this.m_txtDiagnoseGist.Text = "";
            // 
            // m_txtBodyInfo
            // 
            this.m_txtBodyInfo.AccessibleDescription = "������Ӧ֢";
            this.m_txtBodyInfo.BackColor = System.Drawing.Color.White;
            this.m_txtBodyInfo.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtBodyInfo.ForeColor = System.Drawing.Color.Black;
            this.m_txtBodyInfo.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtBodyInfo.Location = new System.Drawing.Point(20, 292);
            this.m_txtBodyInfo.m_BlnIgnoreUserInfo = false;
            this.m_txtBodyInfo.m_BlnPartControl = false;
            this.m_txtBodyInfo.m_BlnReadOnly = false;
            this.m_txtBodyInfo.m_BlnUnderLineDST = false;
            this.m_txtBodyInfo.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtBodyInfo.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtBodyInfo.m_IntCanModifyTime = 6;
            this.m_txtBodyInfo.m_IntPartControlLength = 0;
            this.m_txtBodyInfo.m_IntPartControlStartIndex = 0;
            this.m_txtBodyInfo.m_StrUserID = "";
            this.m_txtBodyInfo.m_StrUserName = "";
            this.m_txtBodyInfo.Name = "m_txtBodyInfo";
            this.m_txtBodyInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtBodyInfo.Size = new System.Drawing.Size(740, 100);
            this.m_txtBodyInfo.TabIndex = 520;
            this.m_txtBodyInfo.Text = "";
            // 
            // m_txtSpecialHandle
            // 
            this.m_txtSpecialHandle.AccessibleDescription = "����������ʽ������ע��������������Ԥ��������";
            this.m_txtSpecialHandle.BackColor = System.Drawing.Color.White;
            this.m_txtSpecialHandle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtSpecialHandle.ForeColor = System.Drawing.Color.White;
            this.m_txtSpecialHandle.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtSpecialHandle.Location = new System.Drawing.Point(20, 28);
            this.m_txtSpecialHandle.m_BlnIgnoreUserInfo = false;
            this.m_txtSpecialHandle.m_BlnPartControl = false;
            this.m_txtSpecialHandle.m_BlnReadOnly = false;
            this.m_txtSpecialHandle.m_BlnUnderLineDST = false;
            this.m_txtSpecialHandle.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtSpecialHandle.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtSpecialHandle.m_IntCanModifyTime = 6;
            this.m_txtSpecialHandle.m_IntPartControlLength = 0;
            this.m_txtSpecialHandle.m_IntPartControlStartIndex = 0;
            this.m_txtSpecialHandle.m_StrUserID = "";
            this.m_txtSpecialHandle.m_StrUserName = "";
            this.m_txtSpecialHandle.Name = "m_txtSpecialHandle";
            this.m_txtSpecialHandle.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtSpecialHandle.Size = new System.Drawing.Size(740, 100);
            this.m_txtSpecialHandle.TabIndex = 530;
            this.m_txtSpecialHandle.Text = "";
            // 
            // m_txtPatientNotion
            // 
            this.m_txtPatientNotion.AccessibleDescription = "���߼�������λ���������";
            this.m_txtPatientNotion.BackColor = System.Drawing.Color.White;
            this.m_txtPatientNotion.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtPatientNotion.ForeColor = System.Drawing.Color.White;
            this.m_txtPatientNotion.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtPatientNotion.Location = new System.Drawing.Point(20, 292);
            this.m_txtPatientNotion.m_BlnIgnoreUserInfo = false;
            this.m_txtPatientNotion.m_BlnPartControl = false;
            this.m_txtPatientNotion.m_BlnReadOnly = false;
            this.m_txtPatientNotion.m_BlnUnderLineDST = false;
            this.m_txtPatientNotion.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtPatientNotion.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtPatientNotion.m_IntCanModifyTime = 6;
            this.m_txtPatientNotion.m_IntPartControlLength = 0;
            this.m_txtPatientNotion.m_IntPartControlStartIndex = 0;
            this.m_txtPatientNotion.m_StrUserID = "";
            this.m_txtPatientNotion.m_StrUserName = "";
            this.m_txtPatientNotion.Name = "m_txtPatientNotion";
            this.m_txtPatientNotion.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtPatientNotion.Size = new System.Drawing.Size(740, 100);
            this.m_txtPatientNotion.TabIndex = 550;
            this.m_txtPatientNotion.Text = "";
            // 
            // m_txtPreparation
            // 
            this.m_txtPreparation.AccessibleDescription = "��ǰ׼��";
            this.m_txtPreparation.BackColor = System.Drawing.Color.White;
            this.m_txtPreparation.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtPreparation.ForeColor = System.Drawing.Color.White;
            this.m_txtPreparation.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtPreparation.Location = new System.Drawing.Point(20, 160);
            this.m_txtPreparation.m_BlnIgnoreUserInfo = false;
            this.m_txtPreparation.m_BlnPartControl = false;
            this.m_txtPreparation.m_BlnReadOnly = false;
            this.m_txtPreparation.m_BlnUnderLineDST = false;
            this.m_txtPreparation.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtPreparation.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtPreparation.m_IntCanModifyTime = 6;
            this.m_txtPreparation.m_IntPartControlLength = 0;
            this.m_txtPreparation.m_IntPartControlStartIndex = 0;
            this.m_txtPreparation.m_StrUserID = "";
            this.m_txtPreparation.m_StrUserName = "";
            this.m_txtPreparation.Name = "m_txtPreparation";
            this.m_txtPreparation.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtPreparation.Size = new System.Drawing.Size(740, 100);
            this.m_txtPreparation.TabIndex = 540;
            this.m_txtPreparation.Text = "";
            // 
            // m_txtAnaesthesia
            // 
            this.m_txtAnaesthesia.BackColor = System.Drawing.Color.White;
            this.m_txtAnaesthesia.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtAnaesthesia.ForeColor = System.Drawing.Color.Black;
            this.m_txtAnaesthesia.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtAnaesthesia.Location = new System.Drawing.Point(20, 28);
            this.m_txtAnaesthesia.m_BlnIgnoreUserInfo = false;
            this.m_txtAnaesthesia.m_BlnPartControl = false;
            this.m_txtAnaesthesia.m_BlnReadOnly = false;
            this.m_txtAnaesthesia.m_BlnUnderLineDST = false;
            this.m_txtAnaesthesia.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtAnaesthesia.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtAnaesthesia.m_IntCanModifyTime = 6;
            this.m_txtAnaesthesia.m_IntPartControlLength = 0;
            this.m_txtAnaesthesia.m_IntPartControlStartIndex = 0;
            this.m_txtAnaesthesia.m_StrUserID = "";
            this.m_txtAnaesthesia.m_StrUserName = "";
            this.m_txtAnaesthesia.Name = "m_txtAnaesthesia";
            this.m_txtAnaesthesia.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtAnaesthesia.Size = new System.Drawing.Size(740, 100);
            this.m_txtAnaesthesia.TabIndex = 560;
            this.m_txtAnaesthesia.Text = "";
            // 
            // m_txtAfterNotice
            // 
            this.m_txtAfterNotice.AccessibleDescription = "����ע��";
            this.m_txtAfterNotice.BackColor = System.Drawing.Color.White;
            this.m_txtAfterNotice.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtAfterNotice.ForeColor = System.Drawing.Color.Black;
            this.m_txtAfterNotice.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtAfterNotice.Location = new System.Drawing.Point(20, 160);
            this.m_txtAfterNotice.m_BlnIgnoreUserInfo = false;
            this.m_txtAfterNotice.m_BlnPartControl = false;
            this.m_txtAfterNotice.m_BlnReadOnly = false;
            this.m_txtAfterNotice.m_BlnUnderLineDST = false;
            this.m_txtAfterNotice.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtAfterNotice.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtAfterNotice.m_IntCanModifyTime = 6;
            this.m_txtAfterNotice.m_IntPartControlLength = 0;
            this.m_txtAfterNotice.m_IntPartControlStartIndex = 0;
            this.m_txtAfterNotice.m_StrUserID = "";
            this.m_txtAfterNotice.m_StrUserName = "";
            this.m_txtAfterNotice.Name = "m_txtAfterNotice";
            this.m_txtAfterNotice.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtAfterNotice.Size = new System.Drawing.Size(740, 100);
            this.m_txtAfterNotice.TabIndex = 570;
            this.m_txtAfterNotice.Text = "";
            // 
            // m_txtDiscussNotion
            // 
            this.m_txtDiscussNotion.AccessibleDescription = "��ǰ�������";
            this.m_txtDiscussNotion.BackColor = System.Drawing.Color.White;
            this.m_txtDiscussNotion.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDiscussNotion.ForeColor = System.Drawing.Color.Black;
            this.m_txtDiscussNotion.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtDiscussNotion.Location = new System.Drawing.Point(20, 292);
            this.m_txtDiscussNotion.m_BlnIgnoreUserInfo = false;
            this.m_txtDiscussNotion.m_BlnPartControl = false;
            this.m_txtDiscussNotion.m_BlnReadOnly = false;
            this.m_txtDiscussNotion.m_BlnUnderLineDST = false;
            this.m_txtDiscussNotion.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtDiscussNotion.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtDiscussNotion.m_IntCanModifyTime = 6;
            this.m_txtDiscussNotion.m_IntPartControlLength = 0;
            this.m_txtDiscussNotion.m_IntPartControlStartIndex = 0;
            this.m_txtDiscussNotion.m_StrUserID = "";
            this.m_txtDiscussNotion.m_StrUserName = "";
            this.m_txtDiscussNotion.Name = "m_txtDiscussNotion";
            this.m_txtDiscussNotion.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtDiscussNotion.Size = new System.Drawing.Size(740, 100);
            this.m_txtDiscussNotion.TabIndex = 580;
            this.m_txtDiscussNotion.Text = "";
            // 
            // m_dtpOeprationTime
            // 
            this.m_dtpOeprationTime.BackColor = System.Drawing.Color.White;
            this.m_dtpOeprationTime.BorderColor = System.Drawing.Color.Black;
            this.m_dtpOeprationTime.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
            this.m_dtpOeprationTime.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_dtpOeprationTime.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_dtpOeprationTime.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_dtpOeprationTime.flatFont = new System.Drawing.Font("����", 12F);
            this.m_dtpOeprationTime.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_dtpOeprationTime.ForeColor = System.Drawing.Color.Black;
            this.m_dtpOeprationTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpOeprationTime.Location = new System.Drawing.Point(570, 72);
            this.m_dtpOeprationTime.m_BlnOnlyTime = false;
            this.m_dtpOeprationTime.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.m_dtpOeprationTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.m_dtpOeprationTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.m_dtpOeprationTime.Name = "m_dtpOeprationTime";
            this.m_dtpOeprationTime.ReadOnly = false;
            this.m_dtpOeprationTime.Size = new System.Drawing.Size(212, 22);
            this.m_dtpOeprationTime.TabIndex = 511;
            this.m_dtpOeprationTime.TextBackColor = System.Drawing.Color.White;
            this.m_dtpOeprationTime.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_trvInOperationDate
            // 
            this.m_trvInOperationDate.BackColor = System.Drawing.Color.White;
            this.m_trvInOperationDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_trvInOperationDate.ForeColor = System.Drawing.Color.Black;
            this.m_trvInOperationDate.HideSelection = false;
            this.m_trvInOperationDate.Location = new System.Drawing.Point(8, 39);
            this.m_trvInOperationDate.Name = "m_trvInOperationDate";
            this.m_trvInOperationDate.ShowRootLines = false;
            this.m_trvInOperationDate.Size = new System.Drawing.Size(191, 55);
            this.m_trvInOperationDate.TabIndex = 510;
            this.m_trvInOperationDate.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_trvInOperationDate_AfterSelect);
            // 
            // mniDoubleStrikeOutDelete
            // 
            this.mniDoubleStrikeOutDelete.Index = -1;
            this.mniDoubleStrikeOutDelete.Text = "˫����ɾ��";
            this.mniDoubleStrikeOutDelete.Click += new System.EventHandler(this.mniDoubleStrikeOutDelete_Click);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 0;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Width = 100;
            // 
            // m_cmdOperationDoctor
            // 
            this.m_cmdOperationDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdOperationDoctor.DefaultScheme = true;
            this.m_cmdOperationDoctor.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdOperationDoctor.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdOperationDoctor.Hint = "";
            this.m_cmdOperationDoctor.Location = new System.Drawing.Point(159, 560);
            this.m_cmdOperationDoctor.Name = "m_cmdOperationDoctor";
            this.m_cmdOperationDoctor.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdOperationDoctor.Size = new System.Drawing.Size(112, 28);
            this.m_cmdOperationDoctor.TabIndex = 585;
            this.m_cmdOperationDoctor.Tag = "";
            this.m_cmdOperationDoctor.Text = "����ҽʦǩ��:";
            // 
            // m_cmdChargeDoctor
            // 
            this.m_cmdChargeDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdChargeDoctor.DefaultScheme = true;
            this.m_cmdChargeDoctor.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdChargeDoctor.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdChargeDoctor.Hint = "";
            this.m_cmdChargeDoctor.Location = new System.Drawing.Point(402, 560);
            this.m_cmdChargeDoctor.Name = "m_cmdChargeDoctor";
            this.m_cmdChargeDoctor.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdChargeDoctor.Size = new System.Drawing.Size(112, 28);
            this.m_cmdChargeDoctor.TabIndex = 595;
            this.m_cmdChargeDoctor.Tag = "1";
            this.m_cmdChargeDoctor.Text = "����ҽʦǩ��:";
            // 
            // m_cmdAnaesthesia
            // 
            this.m_cmdAnaesthesia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdAnaesthesia.DefaultScheme = true;
            this.m_cmdAnaesthesia.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdAnaesthesia.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdAnaesthesia.Hint = "";
            this.m_cmdAnaesthesia.Location = new System.Drawing.Point(88, 4);
            this.m_cmdAnaesthesia.Name = "m_cmdAnaesthesia";
            this.m_cmdAnaesthesia.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdAnaesthesia.Size = new System.Drawing.Size(20, 20);
            this.m_cmdAnaesthesia.TabIndex = 555;
            this.m_cmdAnaesthesia.Text = "��������:";
            this.m_cmdAnaesthesia.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(20, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 10000080;
            this.label2.Text = "��������:";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(205, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 10000082;
            this.label3.Text = "��������:";
            // 
            // m_dtpOperationDate
            // 
            this.m_dtpOperationDate.BackColor = System.Drawing.Color.White;
            this.m_dtpOperationDate.BorderColor = System.Drawing.Color.Black;
            this.m_dtpOperationDate.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
            this.m_dtpOperationDate.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_dtpOperationDate.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_dtpOperationDate.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_dtpOperationDate.flatFont = new System.Drawing.Font("����", 12F);
            this.m_dtpOperationDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtpOperationDate.ForeColor = System.Drawing.Color.Black;
            this.m_dtpOperationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpOperationDate.Location = new System.Drawing.Point(279, 72);
            this.m_dtpOperationDate.m_BlnOnlyTime = false;
            this.m_dtpOperationDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.m_dtpOperationDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.m_dtpOperationDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.m_dtpOperationDate.Name = "m_dtpOperationDate";
            this.m_dtpOperationDate.ReadOnly = false;
            this.m_dtpOperationDate.Size = new System.Drawing.Size(213, 22);
            this.m_dtpOperationDate.TabIndex = 10000081;
            this.m_dtpOperationDate.TextBackColor = System.Drawing.Color.White;
            this.m_dtpOperationDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // tabControl1
            // 
            this.tabControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabControl1.IDEPixelArea = true;
            this.tabControl1.Location = new System.Drawing.Point(7, 103);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.PositionTop = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedTab = this.tabPage1;
            this.tabControl1.Size = new System.Drawing.Size(809, 443);
            this.tabControl1.TabIndex = 10000083;
            this.tabControl1.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage3,
            this.tabPage2});
            this.tabControl1.SelectionChanged += new System.EventHandler(this.tabControl1_SelectionChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.m_txtDiagnose);
            this.tabPage1.Controls.Add(this.lblDiagnose);
            this.tabPage1.Controls.Add(this.lblDiagnoseGist);
            this.tabPage1.Controls.Add(this.m_txtDiagnoseGist);
            this.tabPage1.Controls.Add(this.lblBodyInfo);
            this.tabPage1.Controls.Add(this.m_txtBodyInfo);
            this.tabPage1.ImageIndex = 3;
            this.tabPage1.ImageList = this.imageList1;
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(809, 418);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Title = "���";
            // 
            // m_txtDiagnose
            // 
            this.m_txtDiagnose.AccessibleDescription = "���";
            this.m_txtDiagnose.BackColor = System.Drawing.Color.White;
            this.m_txtDiagnose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDiagnose.ForeColor = System.Drawing.Color.Black;
            this.m_txtDiagnose.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtDiagnose.Location = new System.Drawing.Point(20, 32);
            this.m_txtDiagnose.m_BlnIgnoreUserInfo = false;
            this.m_txtDiagnose.m_BlnPartControl = false;
            this.m_txtDiagnose.m_BlnReadOnly = false;
            this.m_txtDiagnose.m_BlnUnderLineDST = false;
            this.m_txtDiagnose.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtDiagnose.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtDiagnose.m_IntCanModifyTime = 6;
            this.m_txtDiagnose.m_IntPartControlLength = 0;
            this.m_txtDiagnose.m_IntPartControlStartIndex = 0;
            this.m_txtDiagnose.m_StrUserID = "";
            this.m_txtDiagnose.m_StrUserName = "";
            this.m_txtDiagnose.Name = "m_txtDiagnose";
            this.m_txtDiagnose.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtDiagnose.Size = new System.Drawing.Size(740, 100);
            this.m_txtDiagnose.TabIndex = 521;
            this.m_txtDiagnose.Text = "";
            this.m_txtDiagnose.TextChanged += new System.EventHandler(this.m_txtDiagnose_TextChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage3.Controls.Add(this.lblSpecialHandle);
            this.tabPage3.Controls.Add(this.m_txtSpecialHandle);
            this.tabPage3.Controls.Add(this.lblPreparation);
            this.tabPage3.Controls.Add(this.m_txtPreparation);
            this.tabPage3.Controls.Add(this.lblPatientNotion);
            this.tabPage3.Controls.Add(this.m_txtPatientNotion);
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.ImageList = this.imageList1;
            this.tabPage3.Location = new System.Drawing.Point(0, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Selected = false;
            this.tabPage3.Size = new System.Drawing.Size(809, 418);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Title = "Ԥ��";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage2.Controls.Add(this.m_cmdAnaesthesia);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.m_txtAnaesthesia);
            this.tabPage2.Controls.Add(this.lblAfterNotice);
            this.tabPage2.Controls.Add(this.m_txtAfterNotice);
            this.tabPage2.Controls.Add(this.lblDiscussNotion);
            this.tabPage2.Controls.Add(this.m_txtDiscussNotion);
            this.tabPage2.ImageIndex = 5;
            this.tabPage2.ImageList = this.imageList1;
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(809, 418);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Title = "����";
            // 
            // frmBeforeOperationSummary
            // 
            this.AccessibleDescription = "��ǰС��";
            this.ClientSize = new System.Drawing.Size(856, 609);
            this.Controls.Add(this.m_cmdOperationDoctor);
            this.Controls.Add(this.m_txtOperateDoctor);
            this.Controls.Add(this.m_cmdChargeDoctor);
            this.Controls.Add(this.m_txtChargeDoctor);
            this.Controls.Add(this.m_trvInOperationDate);
            this.Controls.Add(this.m_dtpOperationDate);
            this.Controls.Add(this.m_dtpOeprationTime);
            this.Controls.Add(this.lblOperationDate);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label3);
            this.Name = "frmBeforeOperationSummary";
            this.Text = "��ǰС��";
            this.Load += new System.EventHandler(this.frmBeforeOperationSummary_Load);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.lblOperationDate, 0);
            this.Controls.SetChildIndex(this.m_dtpOeprationTime, 0);
            this.Controls.SetChildIndex(this.m_dtpOperationDate, 0);
            this.Controls.SetChildIndex(this.m_trvInOperationDate, 0);
            this.Controls.SetChildIndex(this.m_txtChargeDoctor, 0);
            this.Controls.SetChildIndex(this.m_cmdChargeDoctor, 0);
            this.Controls.SetChildIndex(this.m_txtOperateDoctor, 0);
            this.Controls.SetChildIndex(this.m_cmdOperationDoctor, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void frmBeforeOperationSummary_Load(object sender, System.EventArgs e)
		{
            if (m_BlnNeedContextMenu)
                m_mthAddRichTemplateInContainer(this);
			m_mthSetRichTextAttrib();
			m_mthSetQuickKeys();
			

			m_lsvInPatientID.Visible = false;
			m_mthfrmLoad();

			this.m_dtpOeprationTime.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
			this.m_dtpOeprationTime.m_mthResetSize();

			this.m_dtpOperationDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
			this.m_dtpOperationDate.m_mthResetSize();

			m_txtDiagnose.Focus();
		}

		#region Override
		/// <summary>
		/// �����������
		/// </summary>
		private void m_mthClearAll()
		{
			//��ղ�����Ϣ
			m_mthClearPatientBaseInfo();
			txtInPatientID.Tag = null;
			m_trnRoot.Nodes.Clear();

			m_mthClearMain();

			m_mthClearContent();

			objclsBeforeOperationSummary_All =null;
			objCurrentPatient=null;

		}
		/// <summary>
		/// ���������Ϣ
		/// </summary>
		private void m_mthClearMain()
		{
			m_blnCanDoctorTextChanged = false;

			m_txtOperateDoctor.Text = "";
			m_txtOperateDoctor.Tag = null;
			m_txtChargeDoctor.Text = "";
			m_txtChargeDoctor.Tag = null;

			m_blnCanDoctorTextChanged = true;			

			m_dtpOeprationTime.Value = DateTime.Now;
			m_dtpOeprationTime.Enabled=true;
			m_dtpOeprationTime.Tag = null;

		}
		/// <summary>
		/// ����ӱ���Ϣ
		/// </summary>
		private void m_mthClearContent()
		{
			m_strCurrentOpenDate = "";
			m_txtDiagnose.m_mthClearText();
			m_txtDiagnoseGist.m_mthClearText();
			m_txtBodyInfo.m_mthClearText();
			m_txtSpecialHandle.m_mthClearText();
			m_txtPreparation.m_mthClearText();
			m_txtPatientNotion.m_mthClearText();
			m_txtAnaesthesia.m_mthClearText();
			m_txtAfterNotice.m_mthClearText();
			m_txtDiscussNotion.m_mthClearText();

			m_dtpOperationDate.Value = DateTime.Now;
			
			m_mthSetModifyControl(null,true);			
		}

		/// <summary>
		/// ���ø������͵�Ĭ��ֵ
		/// </summary>
		/// <param name="p_objPatient"></param>
		private void m_mthSetDefaultValue(clsPatient p_objPatient)
		{
			#region ���Ĭ��ֵ ������
//			m_txtPatientNotion.Text = clsDefaultValue.c_strPatientNotion;
//			m_txtAfterNotice.Text = clsDefaultValue.c_strAfterNotice;
//			m_txtDiscussNotion.Text = clsDefaultValue.c_strDiscussNotion;
			#endregion	

			clsEmployeeSignTool.s_mthSetDefaulEmployee(m_txtChargeDoctor);

			new clsDefaultValueTool(this,p_objPatient).m_mthSetDefaultValue();

			//�Զ����ù�����ģ��
			m_mthSetSpecialPatientTemplateSet(p_objPatient);
			m_mthSetSpecialPatientTemplateSet(p_objPatient,enmAssociate.Operation);

			#region ���ݸ���			
//			clsInPatientCaseHisoryDefaultValue [] objInPatientCaseDefaultValue = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(p_objPatient.m_StrInPatientID,p_objPatient.m_DtmLastInDate.ToString());
//			if(objInPatientCaseDefaultValue !=null && objInPatientCaseDefaultValue.Length >0)
//			{
//				this.m_txtDiagnose.Text = objInPatientCaseDefaultValue[0].m_strPrimaryDiagnose;
//			}
//
//			clsGeneralDiseaseRecordShareDomain.stuFirstDiseaseInfoShare stuFirstDiseaseInfo;
//			long lngRes = new clsGeneralDiseaseRecordShareDomain().m_lngGetFirstDiseaseInfoShareValue(p_objPatient.m_StrInPatientID,p_objPatient.m_DtmLastInDate.ToString(),out stuFirstDiseaseInfo);
//			if(lngRes > 0)
//			{
////				int intFirstIndex = stuFirstDiseaseInfo.m_strRecord.IndexOf("������ݣ�")+5;
////				int intLastIndex = stuFirstDiseaseInfo.m_strRecord.IndexOf("������ϣ�");
////				System.Text.StringBuilder sb = new System.Text.StringBuilder(stuFirstDiseaseInfo.m_strMain);
////				sb.Append("\r\n");
////				sb.Append(stuFirstDiseaseInfo.m_strDiagnose);
////				sb.Append("\r\n");
////				sb.Append(stuFirstDiseaseInfo.m_strDiagnoseDist);
////				sb.Append("\r\n");
////				sb.Append(stuFirstDiseaseInfo.m_strTreatPlan);
////				sb.Append("\r\n");
////				sb.Append(stuFirstDiseaseInfo.m_strDiffDiagnose);
////				this.m_txtDiagnoseGist.Text = sb.ToString();
//
//				this.m_txtDiagnoseGist.Text = stuFirstDiseaseInfo.m_strDiagnoseDist;
//			}
			#endregion
		}

		/// <summary>
		/// �ӽ�������������Ϣ��������Ϣ����
		/// </summary>
		/// <param name="p_objMainInfo">������Ϣ����</param>
		private void m_mthSetMainXml(clsBeforeOperationSummaryInfo p_objMainInfo)
		{			
			p_objMainInfo.m_strDiagnoseXml = m_txtDiagnose.m_strGetXmlText();
			p_objMainInfo.m_strDiagnoseGistXml = m_txtDiagnoseGist.m_strGetXmlText();
			p_objMainInfo.m_strBodyInfoXml = m_txtBodyInfo.m_strGetXmlText();
			p_objMainInfo.m_strSpecialHandleXml = m_txtSpecialHandle.m_strGetXmlText();
			p_objMainInfo.m_strPreparationXml = m_txtPreparation.m_strGetXmlText();
			p_objMainInfo.m_strPatientNotionXml = m_txtPatientNotion.m_strGetXmlText();
			p_objMainInfo.m_strAnaesthesiaXml = m_txtAnaesthesia.m_strGetXmlText();
			p_objMainInfo.m_strAfterNoticeXml = m_txtAfterNotice.m_strGetXmlText();
			p_objMainInfo.m_strDiscussNotionXml = m_txtDiscussNotion.m_strGetXmlText();
		}
		/// <summary>
		/// �ӽ��������ӱ���Ϣ���ӱ���Ϣ����
		/// </summary>
		/// <param name="p_objContentInfo">�ӱ���Ϣ����</param>
		private void m_mthSetContentText(clsBeforeOperationSummaryContentInfo p_objContentInfo)
		{
			p_objContentInfo.m_strDiagnose = m_txtDiagnose.Text;
			p_objContentInfo.m_strDiagnoseGist = m_txtDiagnoseGist.Text;
			p_objContentInfo.m_strBodyInfo = m_txtBodyInfo.Text;
			p_objContentInfo.m_strSpecialHandle = m_txtSpecialHandle.Text;
			p_objContentInfo.m_strPreparation = m_txtPreparation.Text;
			p_objContentInfo.m_strPatientNotion = m_txtPatientNotion.Text;
			p_objContentInfo.m_strAnaesthesia = m_txtAnaesthesia.Text;
			p_objContentInfo.m_strAfterNotice = m_txtAfterNotice.Text;
			p_objContentInfo.m_strDiscussNotion = m_txtDiscussNotion.Text;
			p_objContentInfo.m_strOperationDate = m_dtpOperationDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
		}
		
		/// <summary>
		/// �ڲ���ʹ��
		/// </summary>
		/// <returns></returns>
		protected override long m_lngSubAddNew()
		{
			if(objCurrentPatient == null)
			{
				clsPublicFunction.ShowInformationMessageBox("��ѡ����");
				return -5;
			}

//			if(objOperatorDoctor == null )
//			{
//				clsPublicFunction.ShowInformationMessageBox("��ѡ������ҽ����");
//				return -6;
//			}
            if (m_txtChargeDoctor.Tag == null)
			{
				clsPublicFunction.ShowInformationMessageBox("��ѡ�񾭹�ҽ����");
				return -6;
			}

            clsEmrEmployeeBase_VO objChargeDoctor = (clsEmrEmployeeBase_VO)m_txtChargeDoctor.Tag;

            bool blnIsDouble;
			long lngRes = m_objBOSDomain.m_lngCheckNewCreateDate(objCurrentPatient.m_StrInPatientID,objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"),m_dtpOeprationTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),out blnIsDouble);
			if(lngRes <= 0)
			{
				return lngRes;
			}

			if(!blnIsDouble)
			{
				m_mthShowRecordTimeDouble();
				return -8;
			}

			clsBeforeOperationSummaryInfo objMainInfo = new clsBeforeOperationSummaryInfo();
			objMainInfo.m_strInPatientID = objCurrentPatient.m_StrInPatientID;
			objMainInfo.m_strInPatientDate = objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss");
			objMainInfo.m_strCreateDate = m_dtpOeprationTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
			objMainInfo.m_strCreateID = m_objCurrentContext.m_ObjEmployee.m_StrEmployeeID;
			m_mthSetMainXml(objMainInfo);

			clsBeforeOperationSummaryContentInfo objContentInfo = new clsBeforeOperationSummaryContentInfo();
			objContentInfo.m_strInPatientID = objCurrentPatient.m_StrInPatientID;
			objContentInfo.m_strInPatientDate = objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss");
			objContentInfo.m_strModifyUserID = clsEMRLogin.LoginInfo.m_strEmpNo;
            objContentInfo.m_strOperateDoctorID = (m_txtOperateDoctor.Tag == null) ? "" : ((clsEmrEmployeeBase_VO)m_txtOperateDoctor.Tag).m_strEMPNO_CHR;
			objContentInfo.m_strChargeDoctorID = objChargeDoctor.m_strEMPNO_CHR;

			m_mthSetContentText(objContentInfo);
			
			//����ǩ�� 
			//��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
			clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
            objSign_VO.m_strFORMID_VCHR = this.Name;
            objSign_VO.m_strFORMRECORDID_VCHR = objCurrentPatient.m_StrInPatientID.Trim() + "-" + objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss");
            objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
            objSign_VO.m_strRegisterId = m_objBaseCurrentPatient.m_StrRegisterId;
            clsCheckSignersController objCheck = new clsCheckSignersController();
            if (objCheck.m_lngSign(objContentInfo, objSign_VO) == -1)
                return -1;

			lngRes = m_objBOSDomain.m_lngAddNew(objMainInfo,objContentInfo);
			if(lngRes > 0)
			{
				m_mthAddNode(objMainInfo.m_strCreateDate);
				m_trnRoot.Expand();	
			}
			return lngRes;
		}
		private void m_mthAddNode(string strTime)
		{ 
			if(strTime=="" ||strTime==null) return ;
			TreeNode trnNode=new TreeNode(strTime);			
			if(m_trnRoot.Nodes.Count==0 || trnNode.Text.CompareTo (m_trnRoot.LastNode.Text)<0)
			{
				m_trnRoot.Nodes.Add(trnNode);
				m_trvInOperationDate.SelectedNode=m_trnRoot.LastNode;//Jacky-2003-4-28
			}
			else 
			{
				for(int i=0;i<m_trnRoot.Nodes.Count;i++)
				{
					if(trnNode.Text.CompareTo (m_trnRoot.Nodes[i].Text)>0)
					{
						m_trnRoot.Nodes.Insert(i,trnNode);
						m_trvInOperationDate.SelectedNode=m_trnRoot.Nodes[i];//
						break;
					}
				}
			}			
			m_dtpOeprationTime.Enabled=false;
	     }

       
		/// <summary>
		/// �ڲ���ʹ��
		/// </summary>
		/// <returns></returns>
		protected override long m_lngSubDelete()
		{

			if(this.objCurrentPatient!=null && objclsBeforeOperationSummary_All!=null && objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo!=null &&objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo!=null)
			{
                //Ȩ���ж�
                string strDeptIDTemp = base.m_ObjCurrentEmrPatientSession.m_strAreaId;// ((clsDepartment)m_cboDept.SelectedItem).m_strDeptNewID;
                bool blnIsAllow = clsPublicFunction.IsAllowDelete(strDeptIDTemp, m_StrRecorder_ID, clsEMRLogin.LoginEmployee, 1);
                if (!blnIsAllow)
                    return -1;

				long lngRes= m_objBOSDomain. m_lngDeleteRecord(  objCurrentPatient.m_StrInPatientID,  objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strOpenDate );
				if(lngRes>0)
				{
					m_trvInOperationDate.SelectedNode.Remove();	
				}
			}
			return 0;
		}
       
		/// <summary>
		/// �ڲ���ʹ��
		/// </summary>
		/// <returns></returns>
		protected override long m_lngSubModify()
		{
			
			if(objCurrentPatient == null)
			{

				clsPublicFunction.ShowInformationMessageBox("��ѡ����");

				return -5;
			}

            clsEmrEmployeeBase_VO objOperatorDoctor = (clsEmrEmployeeBase_VO)m_txtOperateDoctor.Tag;
            clsEmrEmployeeBase_VO objChargeDoctor = (clsEmrEmployeeBase_VO)m_txtChargeDoctor.Tag;
			
			if(objOperatorDoctor == null )
			{

				clsPublicFunction.ShowInformationMessageBox("��ѡ������ҽ����");

				return -6;
			}
			if(objChargeDoctor == null)
			{

				clsPublicFunction.ShowInformationMessageBox("��ѡ�񾭹�ҽ����");

				return -6;
			}
			
			string strLastModifyDate="";
			string strModidyUserID="";

			long lngRes = m_objBOSDomain.m_lngGetLastModifyDate(objCurrentPatient.m_StrInPatientID,objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strOpenDate,out strLastModifyDate,out strModidyUserID);
           
			if(lngRes <= 0)
			{
				return lngRes;
			}

			if(strLastModifyDate!="" && strModidyUserID!="" && strLastModifyDate!=objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strModifyDate)
			{
              
				if(m_bolShowRecordModified(strModidyUserID,strLastModifyDate))
				{
					m_mthSetSummaryInfo(objCurrentPatient,m_dtpOeprationTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));

				}
				else
				{
					return -3;

				}
	
			}
			else if(strLastModifyDate=="" && strModidyUserID=="")
			{
				m_mthShowRecordDeleted(strModidyUserID,strLastModifyDate);
				return -3;


			}
//			else if(strLastModifyDate!="" && strModidyUserID!="" && strLastModifyDate==objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strModifyDate)
//			{
//				if(!m_bolShowIfModify())
//				        return -3;
//			}
																																												  

			clsBeforeOperationSummaryInfo objMainInfo = new clsBeforeOperationSummaryInfo();
			objMainInfo.m_strInPatientID = objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strInPatientID;
			objMainInfo.m_strInPatientDate = objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strInPatientDate;
			objMainInfo.m_strOpenDate=objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strOpenDate;
			objMainInfo.m_strCreateDate = objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strCreateDate;
			objMainInfo.m_strCreateID = objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strCreateID;
			
			m_mthSetMainXml(objMainInfo);

			clsBeforeOperationSummaryContentInfo objContentInfo = new clsBeforeOperationSummaryContentInfo();
			objContentInfo.m_strOpenDate=objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strOpenDate ;
			objContentInfo.m_strInPatientID = objCurrentPatient.m_StrInPatientID;
			objContentInfo.m_strInPatientDate = objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss");
            objContentInfo.m_strModifyUserID = clsEMRLogin.LoginInfo.m_strEmpNo;
			objContentInfo.m_strOperateDoctorID = objOperatorDoctor.m_strEMPNO_CHR;
			objContentInfo.m_strChargeDoctorID = objChargeDoctor.m_strEMPNO_CHR;

			m_mthSetContentText(objContentInfo);
		
			//����ǩ�� 
			//��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
			clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
            objSign_VO.m_strFORMID_VCHR = this.Name;
            objSign_VO.m_strFORMRECORDID_VCHR = objCurrentPatient.m_StrInPatientID.Trim() + "-" + objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss");
            objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
            objSign_VO.m_strRegisterId = m_objBaseCurrentPatient.m_StrRegisterId;
            clsCheckSignersController objCheck = new clsCheckSignersController();
            if (objCheck.m_lngSign(objContentInfo, objSign_VO) == -1)
                return -1;

			lngRes = m_objBOSDomain.m_lngModify(objMainInfo,objContentInfo);			
					
			if(lngRes <= 0)
			{
				m_mthShowDBError();

			}
			else 
			{
				m_mthSetSummaryInfo(objCurrentPatient,m_dtpOeprationTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                
			}

			return lngRes;
		}

		
		#region ʹ���ⲿ�Ĵ�ӡ���� Alex 2003-7-2

		System.Drawing.Printing.PrintDocument m_pdcPrintDocument;
		private void m_mthfrmLoad()
		{	
			this.m_pdcPrintDocument = new System.Drawing.Printing.PrintDocument();
			this.m_pdcPrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_BeginPrint);
			this.m_pdcPrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_EndPrint);
			this.m_pdcPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdcPrintDocument_PrintPage);		
		}
		private void m_pdcPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{			
			objPrintTool.m_mthPrintPage(e);
		}

		private void m_pdcPrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			objPrintTool.m_mthBeginPrint(e);				
		}

		private void m_pdcPrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			objPrintTool.m_mthEndPrint(e);
		}

		clsBeforeOperationSummaryLJPrintTool objPrintTool;
        /// <summary>
        /// ��ӡ
        /// </summary>
		private void m_mthDemoPrint_FromDataSource()
		{
            this.objPrintTool = new clsBeforeOperationSummaryLJPrintTool();
            this.objPrintTool.m_mthInitPrintTool(null);
            if (base.m_objBaseCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                this.objPrintTool.m_mthSetPrintInfo(base.m_objBaseCurrentPatient, DateTime.MinValue, DateTime.MinValue);
            else
            {
                m_objBaseCurrentPatient.m_StrHISInPatientID = base.m_objBaseCurrentPatient.m_StrHISInPatientID;// txtInPatientID.Text;
                if (this.m_trvInOperationDate.SelectedNode == null || this.m_trvInOperationDate.SelectedNode == this.m_trnRoot)
                    this.objPrintTool.m_mthSetPrintInfo(base.m_objBaseCurrentPatient, base.m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate, DateTime.MinValue);
                else
                    this.objPrintTool.m_mthSetPrintInfo(base.m_objBaseCurrentPatient, base.m_objBaseCurrentPatient.m_DtmSelectedInDate, this.m_dtpOeprationTime.Value);
            }
            this.objPrintTool.m_mthInitPrintContent();

            this.m_mthStartPrint();
		}
		
		private void m_mthStartPrint()
		{
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

		protected override long m_lngSubPrint()//����ԭ�����е�ͬ����ӡ����
		{			
			m_mthDemoPrint_FromDataSource();
			return 1;
		}
		#endregion ʹ�����ⲿ�Ĵ�ӡ����

		private PrintTool.frmPrintPreviewDialog printpreviewdialog = new PrintTool.frmPrintPreviewDialog();

		/// <summary>
		/// ���ò��˱���Ϣ
		/// </summary>
		/// <param name="p_objSelectedPatient">����</param>
		protected override void m_mthSetPatientFormInfo(iCare.clsPatient p_objSelectedPatient)
		{
            this.m_mthClearMain();
            this.m_mthClearContent();

            this.m_trnRoot.Nodes.Clear();

            if (p_objSelectedPatient == null)
                return;
            this.objCurrentPatient = p_objSelectedPatient;
            #region
            //string[] strOperateDateArr = null;

            //long lngRes = m_objBOSDomain.m_lngGetOperationDateArr(p_objSelectedPatient.m_StrInPatientID, p_objSelectedPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), out strOperateDateArr);

            //if (strOperateDateArr == null || strOperateDateArr.Length < 1 || lngRes <= 0)
            //{
            //    m_mthSetDefaultValue(p_objSelectedPatient);
            //    return;
            //}

            //for (int i = 0; i < strOperateDateArr.Length; i++)
            //{
            //    TreeNode trnOperateDate = new TreeNode(strOperateDateArr[i]);
            //    m_trnRoot.Nodes.Add(trnOperateDate);
            //}

            //m_trnRoot.Expand();

            //m_mthIsReadOnly();

            //m_blnCanShowDiseaseTrack = m_blnCanShowRecordContent();
            //m_trvInOperationDate.SelectedNode = m_trnRoot.Nodes[0];
            #endregion
        }
        /// <summary>
        /// ���ݲ�����Ժʱ��ĸı�ˢ�¼�¼�����б�
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        private void Refresh_m_trvInOperationDate(clsPatient p_objSelectedPatient)
        {
            this.m_mthClearMain();
            this.m_mthClearContent();

            this.m_trnRoot.Nodes.Clear();

            string[] strOperateDateArr = null;

            long lngRes = this.m_objBOSDomain.m_lngGetOperationDateArr(p_objSelectedPatient.m_StrInPatientID, p_objSelectedPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), out strOperateDateArr);

            if (strOperateDateArr == null || strOperateDateArr.Length < 1 || lngRes <= 0)
            {
                this.m_mthSetDefaultValue(p_objSelectedPatient);
                return;
            }

            for (int i = 0; i < strOperateDateArr.Length; i++)
            {
                TreeNode trnOperateDate = new TreeNode(strOperateDateArr[i]);
                this.m_trnRoot.Nodes.Add(trnOperateDate);
            }

            this.m_trnRoot.Expand();

            base.m_mthIsReadOnly();

            this.m_blnCanShowDiseaseTrack = base.m_blnCanShowRecordContent();
            this.m_trvInOperationDate.SelectedNode = this.m_trnRoot.Nodes[0];
        }

		protected override bool m_BlnCanTextChanged
		{
			get
			{
				//�Բ��˺ŵ����벻���������в���Ҫ���ơ�
				return true;
			}
		}

		protected override bool m_BlnIsAddNew
		{
			get
			{
				if(m_dtpOeprationTime.Enabled==true)
					return true;
				else 
					return false;
			}
		}

		protected override iCare.enmFormState m_EnmCurrentFormState
		{
			get
			{
				return enmFormState.NowUser;
			}
		}
		#endregion

		#region �ӿں���
		public void Verify()
		{
			//long lngRes=m_lngSignVerify(p_strFormID,p_strRecordID);
		}
		public void Delete()
		{
			long lngRes=m_lngDelete();	
//			if(lngRes>0)
//			{
//				foreach(TreeNode trnNode in m_trvInOperationDate.Nodes[0].Nodes)
//				{
//					if(trnNode.Text.ToString()==objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strCreateDate)
//					{
//						trnNode.Remove();
//						this.m_trvInOperationDate.SelectedNode=m_trnRoot ;
//						break;
//					}
//				}
//				m_mthClearMain();
//				m_mthClearContent();
//			}
		}

		public void Display()
		{
		
		}

		public void Display(string cardno, string sendcheckdate)
		{
		
		}

		public void Print()
		{
			m_lngPrint();
		}

		public void Save()
		{
			if(m_lngSave() > 0)
				MessageBox.Show("����ɹ���");
			else
				MessageBox.Show("����ʧ�ܣ�");
		}

		public void Copy()
		{
			m_lngCopy();
		}

		public void Cut()
		{
			m_lngCut();
		}

		public void Paste()
		{
			m_lngPaste();
		}

		public void Redo()
		{
		
		}

		public void Undo()
		{
		
		}	
		#endregion

        protected bool m_blnCanShowDiseaseTrack = true;
		private void m_trvInOperationDate_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			m_mthRecordChangedToSave();
			
			if(m_trvInOperationDate.SelectedNode.Equals(m_trnRoot))
			{
				/*
				 * ��ս���
				 */
				m_mthClearMain();
				m_mthClearContent();
				objclsBeforeOperationSummary_All=null;

				//��ǰ����������¼״̬
				MDIParent.m_mthChangeFormText(this,MDIParent.enmFormEditStatus.AddNew);

			}
			else
			{
				/*
				 * ��ս���
				 */
				m_mthClearMain();
				m_mthClearContent();

                if (!m_blnCanShowDiseaseTrack)
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

				//��������
			     m_mthSetSummaryInfo(objCurrentPatient,e.Node.Text);

				if(objclsBeforeOperationSummary_All==null)
				{
					clsPublicFunction.ShowInformationMessageBox("�˼�¼�ѱ�����ɾ��,�����´򿪴��壡");
					m_mthClearMain();
					m_mthClearContent();
					return;
				}
				m_mthSetModifyControl(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo,false);

				//��ǰ�����޸ļ�¼״̬
				MDIParent.m_mthChangeFormText(this,MDIParent.enmFormEditStatus.Modify );
			}

			m_mthAddFormStatusForClosingSave();
		}
		
		/// <summary>
		/// ����С����Ϣ
		/// </summary>
		/// <param name="p_objCurrentPatient">����</param>
		/// <param name="p_strOperateDate">��������</param>
		private void m_mthSetSummaryInfo(clsPatient p_objCurrentPatient,string p_strCreateDate)
		{
			clsBeforeOperationSummary_All objTemp=null;
			long lngRes=m_objBOSDomain.m_lngGetSummary_All(p_objCurrentPatient.m_StrInPatientID,p_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"),p_strCreateDate,out objTemp);
			if(lngRes>0 && objTemp==null)
			{
				objclsBeforeOperationSummary_All=null;				
			}
			if(lngRes>0 && objTemp!=null &&objTemp.m_objclsBeforeOperationSummaryContentInfo!=null && objTemp.m_objclsBeforeOperationSummaryInfo!=null)
			{
				objclsBeforeOperationSummary_All=objTemp ;

				m_strCurrentOpenDate = objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strOpenDate;
		

				m_txtDiagnose.m_mthSetNewText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiagnose,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiagnoseXml);
				m_txtDiagnoseGist.m_mthSetNewText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiagnoseGist,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiagnoseGistXml);
				m_txtBodyInfo.m_mthSetNewText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strBodyInfo,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strBodyInfoXml);
				m_txtSpecialHandle.m_mthSetNewText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strSpecialHandle,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strSpecialHandleXml);
				m_txtPreparation.m_mthSetNewText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strPreparation,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strPreparationXml);
				m_txtPatientNotion.m_mthSetNewText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strPatientNotion,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strPatientNotionXml);
				m_txtAnaesthesia.m_mthSetNewText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strAnaesthesia,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strAnaesthesiaXml);
				m_txtAfterNotice.m_mthSetNewText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strAfterNotice,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strAfterNoticeXml);
				m_txtDiscussNotion.m_mthSetNewText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiscussNotion,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiscussNotionXml);

				//��������
				m_dtpOperationDate.Value = DateTime.Parse(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strOperationDate);

				m_blnCanDoctorTextChanged=false;
                m_mthAddSignToTextBoxByEmpNo(new TextBoxBase[] { m_txtOperateDoctor, m_txtChargeDoctor }, new string[] { objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strOperateDoctorID, objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strChargeDoctorID }, new bool[] { true,true });
                //clsEmrEmployeeBase_VO objEmpVO = new clsEmrEmployeeBase_VO();
                //objEmployeeSign.m_lngGetEmpByNO(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strOperateDoctorID, out objEmpVO);
                //if (objEmpVO != null)
                //{
                //    m_txtOperateDoctor.Text = objEmpVO.m_strLASTNAME_VCHR;
                //    m_txtOperateDoctor.Tag = objEmpVO;
                //}

                //objEmployeeSign.m_lngGetEmpByNO(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strChargeDoctorID, out objEmpVO);
                //if (objEmpVO != null)
                //{
                //    m_txtChargeDoctor.Text = objEmpVO.m_strLASTNAME_VCHR;
                //    m_txtChargeDoctor.Tag = objEmpVO;
                //}
				m_blnCanDoctorTextChanged=true;

				m_dtpOeprationTime.Value=DateTime.Parse(p_strCreateDate);
				m_dtpOeprationTime.Enabled=false;

			}
			else if(lngRes>0)//��ʱΪ�����¼״̬
			{
#region ��ӳ����ֶε���Ϣ��ȡ��Jacky-2003-4-16
				clsDiagnose objclsDiagnose=new clsDiagnose(objCurrentPatient.m_StrInPatientID,objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"));
				m_txtDiagnose.Text=objclsDiagnose.m_strDiagnose;
#endregion ��ӳ����ֶε���Ϣ��ȡ��Jacky-2003-4-16
			}
		}


		


		#region �����Ƿ������޸ģ��޸����ۼ������Լ��޸���ɫ

		/// <summary>
		/// �����Ƿ�����޸ģ��޸����ۼ�����
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_blnReset"></param>
		protected void m_mthSetModifyControl(clsBeforeOperationSummaryContentInfo p_objRecordContent,
			bool p_blnReset)
		{
			//������д�淶���þ��崰�����д���ƣ����Ӵ�������ʵ��
			if(p_blnReset==true)
			{
				m_mthSetRichTextModifyColor(this,clsHRPColor.s_ClrInputFore);
				m_mthSetRichTextCanModifyLast(this,true);
			}
			else if(p_objRecordContent!=null)
			{
				m_mthSetRichTextModifyColor(this,Color.Red);
				m_mthSetRichTextCanModifyLast(this,m_blnGetCanModifyLast(p_objRecordContent.m_strModifyUserID));
			}
		}

		/// <summary>
		/// ���ô����пؼ������ı�����ɫ
		/// </summary>
		/// <param name="p_ctlControl"></param>
		/// <param name="p_clrColor"></param>
		private void m_mthSetRichTextModifyColor(Control p_ctlControl,System.Drawing.Color p_clrColor)
		{
			#region ���ÿؼ������ı�����ɫ,Jacky-2003-3-24	
			string strTypeName = p_ctlControl.GetType().Name;			
			if(strTypeName=="ctlRichTextBox")			
				((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_ClrOldPartInsertText = p_clrColor;				
			
			if(p_ctlControl.HasChildren && strTypeName !="DataGrid" )
			{									
				foreach(Control subcontrol in p_ctlControl.Controls)
				{										
					m_mthSetRichTextModifyColor(subcontrol,p_clrColor);					
				} 	
			}						
			#endregion			
		}
		
		
		private void m_mthSetRichTextCanModifyLast(Control p_ctlControl,bool p_blnCanModifyLast )
		{
			#region ���ÿؼ������ı����Ƿ�����޸�,Jacky-2003-3-24	
			string strTypeName = p_ctlControl.GetType().Name;			
			if(strTypeName=="ctlRichTextBox")
			{				
				((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_BlnCanModifyLast = p_blnCanModifyLast;
			}
			
			if(p_ctlControl.HasChildren && strTypeName !="DataGrid" )
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
		private bool m_blnGetCanModifyLast(string p_strModifyUserID)
		{			
			if(p_strModifyUserID==null || p_strModifyUserID.Trim() == MDIParent.OperatorID.Trim())
				return true;
			else 
				return false;
		}
		#endregion


		/// <summary>
		/// ����RichTextBox���ԡ����Ҽ��˵����û��������û�ID����ɫ�ȣ���
		/// </summary>
		/// <param name="p_objRichTextBox"></param>
		protected void m_mthSetRichTextBoxAttrib(com.digitalwave.controls.ctlRichTextBox p_objRichTextBox)
		{
            //m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{	p_objRichTextBox });
			//�����Ҽ��˵�			
//			p_objRichTextBox.ContextMenu=m_cmuRichTextBoxMenu;
			p_objRichTextBox.GotFocus += new EventHandler(m_txtRichTextBox_GotFocus);
			
			//������������			
			p_objRichTextBox.m_StrUserID = MDIParent.OperatorID;
			p_objRichTextBox.m_StrUserName = MDIParent.strOperatorName;
			p_objRichTextBox.m_ClrOldPartInsertText = Color.Black;
			p_objRichTextBox.m_ClrDST = Color.Red;
		}

		protected void m_mthSetRichTextBoxAttribInControl(Control p_ctlControl)
		{
			if(p_ctlControl.GetType().Name=="ctlRichTextBox")
			{
				m_mthSetRichTextBoxAttrib((com.digitalwave.controls.ctlRichTextBox)p_ctlControl);
			}

			if(p_ctlControl.HasChildren && p_ctlControl.GetType().Name !="DataGrid" )
			{									
				foreach(Control subcontrol in p_ctlControl.Controls)
				{										
					m_mthSetRichTextBoxAttribInControl(subcontrol);						
				} 	
			}	
		}

		/// <summary>
		/// ��ȡ��ǰ���˵���������
		/// </summary>
		/// <param name="p_dtmRecordDate">��¼���ڣ��˴���ʾCreateDate</param>
		/// <param name="p_intFormID">����ID</param>
		protected override void m_mthGetDeactiveContent(DateTime p_dtmRecordDate,int p_intFormID)
		{
			if(m_objBaseCurrentPatient==null || m_objBaseCurrentPatient.m_StrInPatientID==null || m_objBaseCurrentPatient.m_DtmSelectedInDate==DateTime.MinValue|| p_dtmRecordDate==DateTime.MinValue )
			{
				clsPublicFunction.ShowInformationMessageBox("��������");
				return ;
			}			
		
			clsBeforeOperationSummary_All objTemp=null;
			long lngRes=m_objBOSDomain.m_lngGetDeletedSummary_All(m_objBaseCurrentPatient.m_StrInPatientID,m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"),p_dtmRecordDate.ToString("yyyy-MM-dd HH:mm:ss"),out objTemp);
			if(lngRes>0 && objTemp==null)
			{
				objclsBeforeOperationSummary_All=null;				
			}
			if(lngRes>0 && objTemp!=null &&objTemp.m_objclsBeforeOperationSummaryContentInfo!=null && objTemp.m_objclsBeforeOperationSummaryInfo!=null)
			{
				objclsBeforeOperationSummary_All=objTemp ;
				m_txtDiagnose.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiagnose,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiagnoseXml);
				m_txtDiagnoseGist.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiagnoseGist,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiagnoseGistXml);
				m_txtBodyInfo.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strBodyInfo,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strBodyInfoXml);
				m_txtSpecialHandle.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strSpecialHandle,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strSpecialHandleXml);
				m_txtPreparation.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strPreparation,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strPreparationXml);
				m_txtPatientNotion.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strPatientNotion,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strPatientNotionXml);
				m_txtAnaesthesia.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strAnaesthesia,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strAnaesthesiaXml);
				m_txtAfterNotice.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strAfterNotice,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strAfterNoticeXml);
				m_txtDiscussNotion.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiscussNotion,objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiscussNotionXml);
				//����Ҫҽ��ǩ��
			}
				
		}


		#region ���
		private string m_strCurrentOpenDate = "";

		private void m_lsvPatientName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void m_txtDiagnose_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void tabControl1_SelectionChanged(object sender, System.EventArgs e)
		{
		
		}
	
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

//				if(this.m_trvInOperationDate.SelectedNode==null || this.m_trvInOperationDate.SelectedNode.Tag==null)
//				{
//					clsPublicFunction.ShowInformationMessageBox("����ѡ���¼");
//					return "";
//				}
//				return (string)this.m_trvInOperationDate.SelectedNode.Tag;
			}
		}

		protected override bool m_BlnCanApprove
		{
			get
			{
				return true;
			}
		}		
		#endregion 

		/// <summary>
		/// ����ID��ֻ����������������Ĵ���
		/// </summary>
		public override int m_IntFormID
		{
			get
			{
				return 23;
			}
		}

        protected override void m_mthPerformSessionChanged(clsEmrPatientSessionInfo_VO p_objSelectedSession, int p_intIndex)
        {
            if (p_objSelectedSession == null)
            {
                return;
            }
            base.m_objBaseCurrentPatient.m_StrHISInPatientID = p_objSelectedSession.m_strHISInpatientId;
            base.m_objBaseCurrentPatient.m_DtmSelectedHISInDate = p_objSelectedSession.m_dtmHISInpatientDate;

            base.m_objBaseCurrentPatient.m_DtmSelectedInDate = p_objSelectedSession.m_dtmEMRInpatientDate;
            base.m_objBaseCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;
            base.m_mthIsReadOnly();
            if (!base.m_blnCanShowRecordContent())
            {
                clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                return;
            }
            this.Refresh_m_trvInOperationDate(base.m_objBaseCurrentPatient);
            //this.m_mthSetSummaryInfo(base.m_objBaseCurrentPatient, p_objSelectedSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"));

            base.m_mthPerformSessionChanged(p_objSelectedSession, p_intIndex);
        }
		#region Property
//		protected override string m_StrCurrentOpenDate
//		{
//			get{return m_strCurrentOpenDate;}
//		}
		protected override enmApproveType m_EnmAppType
		{
			get{return enmApproveType.CaseHistory;}
		}
		protected override string m_StrRecorder_ID
		{
			get
			{
                try
                {
			        clsBeforeOperationSummary_All objTemp=null;
			        long lngRes=m_objBOSDomain.m_lngGetSummary_All(objCurrentPatient.m_StrInPatientID,objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"),m_trvInOperationDate.SelectedNode.Text,out objTemp);
			        if(lngRes>0 && objTemp==null)
				         return "";		
                    else
                        return objTemp.m_objclsBeforeOperationSummaryInfo.m_strCreateID;

                }
                catch (Exception)
                {

                    return "";		
                }
			}
		}
		#endregion

	}

	public class clsDiagnose
	{
		public string m_strDiagnose;
		public string m_strInHospitalDiagnose;
		public clsDiagnose(string m_strInPatientID,string m_strInHospitalDate)
		{
			m_strDiagnose="���1";
			m_strInHospitalDiagnose="��Ժ���1";
		}
	}




	
}

