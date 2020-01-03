using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;

using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;
using HRP;


namespace iCare
{
	/// <summary>
	///  ���̼�¼�������¼���Ļ����塣
	/// �ڴ˴��崦���桢�޸ġ�ɾ�������������������ۼ���˫���߹�����ս����ͨ���߼���
	/// ���ò�����Ϣ�ͻ�ȡ������Ϣ��ͨ���߼���
	/// ��ӡ���ܣ���ȡ�Ѿ�ɾ���ļ�¼��
	/// </summary>
	public class frmTabTrackBase : iCare.frmHRPBaseForm,PublicFunction
	{
		protected System.Windows.Forms.TreeView m_trvCreateDate;
		protected System.Windows.Forms.Label lblCreateDateTitle;
		protected com.digitalwave.Utility.Controls.ctlTimePicker m_dtpCreateDate;
		protected System.Windows.Forms.ContextMenu m_cmuRichTextBoxMenu;
		protected System.Windows.Forms.MenuItem mniDoubleStrikeOutDelete;
		protected System.Drawing.Printing.PrintDocument m_pdtPintDocument;
		protected internal System.Windows.Forms.TabControl m_tabMain;
		protected internal System.Windows.Forms.TabPage tabPage1;
		private System.ComponentModel.IContainer components = null;

		public frmTabTrackBase()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
            //m_objBorderTool=new clsBorderTool(Color.White);
            //m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{m_trvCreateDate});
			if(m_trvCreateDate.Nodes.Count==0)
				m_trvCreateDate.Nodes.Add("��¼ʱ��");
			m_trnRoot=m_trvCreateDate.Nodes[0];

			m_objDiseaseTrackDomain=m_objGetDiseaseTrackDomain();
		}

		// ���̼�¼�������ʵ��
		protected clsDiseaseTrackDomain m_objDiseaseTrackDomain;

		protected clsTrackRecordContent m_objReAddNewOld;

		// ���浱ǰ��ʾ�ļ�¼���ݵı���
		protected clsTrackRecordContent m_objCurrentRecordContent;

		protected TreeNode m_trnRoot;

		protected clsPatient m_objCurrentPatient;
		

        //protected clsBorderTool m_objBorderTool;

		// ��ӡ����������ĵ�
		protected PrintDocument m_pdcPrintDocument;

		protected DateTime m_dtmFirstPrintDate;

		// ����Ƿ��״δ�ӡ
		protected bool m_blnIsFirstPrint;

		protected bool m_blnAlreadySetPrintTools = false;

		/// <summary>
		/// �Ƿ���Դ������ڵ��ѡ���¼�
		/// </summary>
		protected bool m_blnCanTreeNodeAfterSelectEventTakePlace=true; 

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
			this.lblCreateDateTitle = new System.Windows.Forms.Label();
			this.m_dtpCreateDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
			this.m_trvCreateDate = new System.Windows.Forms.TreeView();
			this.m_cmuRichTextBoxMenu = new System.Windows.Forms.ContextMenu();
			this.mniDoubleStrikeOutDelete = new System.Windows.Forms.MenuItem();
			this.m_pdtPintDocument = new System.Drawing.Printing.PrintDocument();
			this.m_tabMain = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.m_tabMain.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_lblForTitle
			// 
			this.m_lblForTitle.Location = new System.Drawing.Point(272, 16);
			this.m_lblForTitle.Visible = true;
			// 
			// lblSex
			// 
			this.lblSex.Visible = true;
			// 
			// lblAge
			// 
			this.lblAge.Visible = true;
			// 
			// lblBedNoTitle
			// 
			this.lblBedNoTitle.Visible = true;
			// 
			// lblInHospitalNoTitle
			// 
			this.lblInHospitalNoTitle.Visible = true;
			// 
			// lblNameTitle
			// 
			this.lblNameTitle.Visible = true;
			// 
			// lblSexTitle
			// 
			this.lblSexTitle.Visible = true;
			// 
			// lblAgeTitle
			// 
			this.lblAgeTitle.Visible = true;
			// 
			// lblAreaTitle
			// 
			this.lblAreaTitle.Location = new System.Drawing.Point(32, 80);
			this.lblAreaTitle.Visible = true;
			// 
			// m_lsvInPatientID
			// 
			this.m_lsvInPatientID.Visible = true;
			// 
			// txtInPatientID
			// 
			this.txtInPatientID.Visible = true;
			// 
			// m_txtPatientName
			// 
			this.m_txtPatientName.Visible = true;
			// 
			// m_txtBedNO
			// 
			this.m_txtBedNO.Visible = true;
			// 
			// m_cboArea
			// 
			this.m_cboArea.Location = new System.Drawing.Point(80, 76);
			this.m_cboArea.Visible = true;
			// 
			// m_lsvPatientName
			// 
			this.m_lsvPatientName.Visible = true;
			// 
			// m_lsvBedNO
			// 
			this.m_lsvBedNO.Visible = true;
			// 
			// m_cboDept
			// 
			this.m_cboDept.Location = new System.Drawing.Point(80, 40);
			this.m_cboDept.Visible = true;
			// 
			// lblDept
			// 
			this.lblDept.Location = new System.Drawing.Point(32, 48);
			this.lblDept.Visible = true;
			// 
			// lblCreateDateTitle
			// 
			this.lblCreateDateTitle.AutoSize = true;
			this.lblCreateDateTitle.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblCreateDateTitle.Location = new System.Drawing.Point(248, 20);
			this.lblCreateDateTitle.Name = "lblCreateDateTitle";
			this.lblCreateDateTitle.Size = new System.Drawing.Size(80, 19);
			this.lblCreateDateTitle.TabIndex = 6068;
			this.lblCreateDateTitle.Text = "��¼ʱ��:";
			// 
			// m_dtpCreateDate
			// 
			this.m_dtpCreateDate.BorderColor = System.Drawing.Color.White;
			this.m_dtpCreateDate.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
			this.m_dtpCreateDate.DropButtonBackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
			this.m_dtpCreateDate.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
			this.m_dtpCreateDate.DropButtonForeColor = System.Drawing.Color.White;
			this.m_dtpCreateDate.flatFont = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_dtpCreateDate.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.m_dtpCreateDate.Location = new System.Drawing.Point(328, 16);
			this.m_dtpCreateDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
			this.m_dtpCreateDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
			this.m_dtpCreateDate.Name = "m_dtpCreateDate";
			this.m_dtpCreateDate.Size = new System.Drawing.Size(208, 26);
			this.m_dtpCreateDate.TabIndex = 11;
			this.m_dtpCreateDate.TextBackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
			this.m_dtpCreateDate.TextForeColor = System.Drawing.Color.White;
			// 
			// m_trvCreateDate
			// 
			this.m_trvCreateDate.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
			this.m_trvCreateDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_trvCreateDate.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_trvCreateDate.ForeColor = System.Drawing.SystemColors.Window;
			this.m_trvCreateDate.HideSelection = false;
			this.m_trvCreateDate.ImageIndex = -1;
			this.m_trvCreateDate.ItemHeight = 18;
			this.m_trvCreateDate.Location = new System.Drawing.Point(8, 8);
			this.m_trvCreateDate.Name = "m_trvCreateDate";
			this.m_trvCreateDate.SelectedImageIndex = -1;
			this.m_trvCreateDate.Size = new System.Drawing.Size(232, 88);
			this.m_trvCreateDate.TabIndex = 10;
			this.m_trvCreateDate.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_trvCreateDate_AfterSelect);
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
			// m_pdtPintDocument
			// 
			this.m_pdtPintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdtPintDocument_BeginPrint);
			this.m_pdtPintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdtPintDocument_EndPrint);
			this.m_pdtPintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdtPintDocument_PrintPage);
			// 
			// m_tabMain
			// 
			this.m_tabMain.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.tabPage1});
			this.m_tabMain.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_tabMain.Location = new System.Drawing.Point(16, 132);
			this.m_tabMain.Name = "m_tabMain";
			this.m_tabMain.SelectedIndex = 0;
			this.m_tabMain.Size = new System.Drawing.Size(972, 168);
			this.m_tabMain.TabIndex = 10000000;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.m_trvCreateDate,
																				   this.lblCreateDateTitle,
																				   this.m_dtpCreateDate});
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(964, 139);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			// 
			// frmTabTrackBase
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(1016, 741);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.m_cboDept,
																		  this.lblDept,
																		  this.m_lsvInPatientID,
																		  this.m_lsvPatientName,
																		  this.m_lsvBedNO,
																		  this.m_cboArea,
																		  this.m_txtBedNO,
																		  this.m_txtPatientName,
																		  this.lblSex,
																		  this.lblAge,
																		  this.lblBedNoTitle,
																		  this.lblInHospitalNoTitle,
																		  this.lblNameTitle,
																		  this.lblSexTitle,
																		  this.lblAgeTitle,
																		  this.lblAreaTitle,
																		  this.txtInPatientID,
																		  this.m_lblForTitle,
																		  this.m_tabMain});
			this.Name = "frmTabTrackBase";
			this.m_tabMain.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion		

		#region DataControl
		public void Save()
		{			
			this.m_lngSave();
		}
		
		public void Delete()
		{
			m_lngDelete();
		}
		public void Display(){}
		public void Display(string strInPatientID,string strInPatientDate)
		{
		}
		public void Print()
		{		
			this.m_lngPrint();				
		}
		public void Copy(){m_lngCopy();}
		public void Cut(){m_lngCut();}
		public void Paste(){m_lngPaste();}
		public void Redo(){}
		public void Undo(){}
		public void Verify()
		{
			//long lngRes=m_lngSignVerify(p_strFormID,p_strRecordID);
		}
		#endregion

		/// <summary>
		/// ��ȡ��ǰ��״̬
		/// </summary>
		protected override enmFormState m_EnmCurrentFormState
		{
			get
			{
				return enmFormState.NowUser;
			}
		}
		/// <summary>
		/// ��ȡ��ǰ�����ⲡ�̼�¼��Ϣ
		/// </summary>
		/// <returns></returns>
		public virtual clsDiseaseTrackInfo m_objGetDiseaseTrackInfo()
		{
			//��ȡ��ǰ�����ⲡ�̼�¼��Ϣ�����Ӵ�������ʵ��
			return null;
		}

		public void m_mthSetDiseaseTrackInfoForAddNew(clsPatient p_objPatient)
		{
			//�������  
			if(p_objPatient==null)
				return;
			
			m_mthSetPatient(p_objPatient);

			m_mthEnablePatientSelect(false);
		}

		/// <summary>
		/// ���ò��̼�¼��Ϣ������ʾ�޸ġ�
		/// </summary>
		/// <param name="p_objPatient"></param>
		/// <param name="p_dtmRecordTime"></param>
		public void m_mthSetDiseaseTrackInfo(clsPatient p_objPatient,
			DateTime p_dtmRecordTime)
		{
			//�������  
			if(p_objPatient==null)
				return;
			
			m_mthSetPatient(p_objPatient);

			//���ü�¼��Ϣ
			m_mthSetSelectedRecord(p_objPatient,p_dtmRecordTime.ToString("yyyy-MM-dd HH:mm:ss"));    
		                        
			//�������û��޸ļ�¼�Ļ�����Ϣ                        
			m_mthEnablePatientSelect(false);
		}

		public void m_mthSetDeletedDiseaseTrackInfo(clsPatient p_objPatient,
			DateTime p_dtmRecordTime)
		{
			//�������  
			if(p_objPatient==null)
				return;
			
			m_mthSetPatient(p_objPatient);

			//���ü�¼��Ϣ
			m_mthSetSelectedDeletedRecord(p_objPatient,p_dtmRecordTime.ToString("yyyy-MM-dd HH:mm:ss"));    
		   
			m_mthEnablePatientSelect(false);

			//			m_mthEnablePatientSelectSub(false);
		}

		/// <summary>
		/// ��ս���
		/// </summary>
		protected void m_mthClearAll()
		{
			//��ղ��˻�����Ϣ            
			m_mthClearPatientBaseInfo();
			//���ʱ���б���
			m_trnRoot.Nodes.Clear();
		
			//���õ�ǰ���˱���
			m_objCurrentPatient = null;
		
			//��յ�ǰ��¼��
			m_mthClearPatientRecordInfo();
		}

		/// <summary>
		/// ��ղ��˼�¼������Ϣ��
		/// </summary>
		protected void m_mthClearPatientRecordInfo()
		{
			//�Ѽ�¼ʱ��ָ�����ǰʱ��      
			m_dtpCreateDate.Value=DateTime.Now;

			m_mthEnableModify(true);

			m_mthSetModifyControl(null,true);
		                       
			//��ռ�¼����                       
			m_mthClearRecordInfo();
		
			//��ձ��浱ǰ��¼�ı���
			m_objCurrentRecordContent = null;        
		
			//��գ����ã�������Ϣ 
			m_objReAddNewOld = null;
		}

		/// <summary>
		/// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
		/// </summary>
		protected virtual void m_mthClearRecordInfo()
		{
			//��վ����¼���ݣ����Ӵ�������ʵ��
		}

		/// <summary>
		/// �����Ƿ����ѡ���˺ͼ�¼ʱ���б��ڴӲ��̼�¼�������ʱ��Ҫʹ�á�
		/// </summary>
		/// <param name="p_blnEnable"></param>
		protected void m_mthEnablePatientSelect(bool p_blnEnable)
		{
			//���ò���ѡ����Ϣ�� Enable = p_blnEnable
			if(p_blnEnable==false)
			{				
				m_lblForTitle.Visible=false;
				lblAreaTitle.Visible=false;
				m_cboArea.Visible= false;
				lblBedNoTitle.Visible=false;
				m_txtBedNO.Visible= false;
				lblNameTitle.Visible=false;
				m_txtPatientName.Visible=false;
				lblInHospitalNoTitle.Visible=false;
				txtInPatientID.Visible=false;
				m_trvCreateDate.Visible=false;
				lblSexTitle.Visible=false;
				lblSex.Visible=false;
				lblAgeTitle.Visible=false;
				lblAge.Visible=false;
                //m_objBorderTool.m_mthUnChangedControlBorder(m_trvCreateDate);			
		
				m_cboArea.Enabled= p_blnEnable;
				m_txtBedNO.ReadOnly= !p_blnEnable;
				m_txtPatientName.ReadOnly=!p_blnEnable;
				txtInPatientID.ReadOnly=!p_blnEnable;			
		
				//����ʱ���б����� Enable = p_blnEnable			
				m_trvCreateDate.Enabled=p_blnEnable;	

			}

		
			m_mthEnablePatientSelectSub(p_blnEnable); 
		}

		/// <summary>
		/// �����Ƿ����ѡ���˺ͼ�¼ʱ���б��ڴӲ��̼�¼�������ʱ��Ҫʹ�á�
		/// </summary>
		/// <param name="p_blnEnable"></param>
		protected virtual void m_mthEnablePatientSelectSub(bool p_blnEnable)
		{
		
		}

		/// <summary>
		/// �Ƿ������޸ļ�¼ʱ��ȼ�¼��Ϣ��
		/// </summary>
		/// <param name="p_blnEnable"></param>
		protected void m_mthEnableModify(bool p_blnEnable)
		{
			//���ü�¼ʱ��� Enable = p_blnEnable
			m_dtpCreateDate.Enabled=p_blnEnable;		

			//���þ����¼���������
			m_mthEnableModifySub(p_blnEnable);
		}

		/// <summary>
		/// �Ƿ������޸������¼�ļ�¼��Ϣ��
		/// </summary>
		/// <param name="p_blnEnable"></param>
		protected virtual void m_mthEnableModifySub(bool p_blnEnable)
		{
			//�����¼���������,�����Ӵ������Ҫ����ʵ��
		}

		/// <summary>
		/// �����Ƿ�����޸ģ��޸����ۼ�����
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_blnReset"></param>
		protected void m_mthSetModifyControl(clsTrackRecordContent p_objRecordContent,
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

			m_mthSetModifyControlSub(p_objRecordContent,p_blnReset);
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

		protected virtual void m_mthSetModifyControlSub(clsTrackRecordContent p_objRecordContent,
			bool p_blnReset)
		{

		}


		/// <summary>
		/// ���ò��˱���Ϣ
		/// </summary>
		/// <param name="p_objSelectedPatient"></param>
		protected override void m_mthSetPatientFormInfo(clsPatient p_objSelectedPatient)
		{
			//�жϲ�����Ϣ�Ƿ�Ϊnull������ǣ�ֱ�ӷ��ء�
			if(p_objSelectedPatient == null)
				return;   	
		
			//��ղ��˼�¼��Ϣ
			m_mthClearPatientRecordInfo();
		
			//��¼������Ϣ
			m_objCurrentPatient = p_objSelectedPatient;
		
			//���ʱ���б�����ʱ��ڵ�     
			m_trnRoot.Nodes.Clear();

			//��ȡ���˼�¼�б�
			string [] strCreateTimeListArr;
			string [] strOpenTimeListArr;
			long lngRes = m_objDiseaseTrackDomain.m_lngGetRecordTimeList(p_objSelectedPatient.m_StrInPatientID,p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss"), out strCreateTimeListArr, out strOpenTimeListArr);
		
			if(lngRes <= 0 || strCreateTimeListArr == null|| strOpenTimeListArr==null || strOpenTimeListArr.Length !=strCreateTimeListArr.Length)
				return; 		
		
			//��Ӳ�ѯ����ʱ�䵽ʱ������ 
			for(int i=strCreateTimeListArr.Length-1;i>=0;i--)
			{
				TreeNode trnRecordDate = new TreeNode(strCreateTimeListArr[i]);
				trnRecordDate.Tag=strOpenTimeListArr[i];
				m_trnRoot.Nodes.Add(trnRecordDate);
			}
		
			//չ������ʾ����ʱ��ڵ㡣
			m_trnRoot.Expand();
		}

		/// <summary>
		/// �ӽ����ȡ�����¼��ֵ���������ֵ��������null��
		/// </summary>
		/// <returns></returns>
		protected virtual clsTrackRecordContent m_objGetContentFromGUI()
		{
			//�ӽ����ȡ��ֵ�����Ӵ�������ʵ��			
			return null;
		}

		/// <summary>
		/// �������¼��ֵ��ʾ�������ϡ�
		/// </summary>
		/// <param name="p_objContent"></param>
		protected virtual void m_mthSetGUIFromContent(clsTrackRecordContent p_objContent)
		{
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
		}

		protected virtual void m_mthSetDeletedGUIFromContent(clsTrackRecordContent p_objContent)
		{
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
		}	

		/// <summary>
		/// ����ѡ���˵ļ�¼��Ϣ��
		/// </summary>
		/// <param name="p_objSelectedPatient"></param>
		/// <param name="p_strOpenDate"></param>
		protected void m_mthSetSelectedRecord(clsPatient p_objSelectedPatient,
			string p_strOpenDate)
		{
			//������
			if(p_objSelectedPatient==null|| p_strOpenDate==null ||p_strOpenDate=="")
				return;
		              
			clsTrackRecordContent objContent;              
			//��ȡ��¼
			long lngRes = m_objDiseaseTrackDomain.m_lngGetRecordContent(p_objSelectedPatient.m_StrInPatientID,p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss"),p_strOpenDate ,out objContent);
		
			if(lngRes <= 0 || objContent == null)
				return;
            
           
			//���õ�ǰ��¼����¼ʱ�� 
			m_objCurrentPatient=  p_objSelectedPatient;
			txtInPatientID.Text=this.m_objCurrentPatient.m_StrHISInPatientID;
			m_dtpCreateDate.Value=objContent.m_dtmCreateDate;
			m_objCurrentRecordContent=objContent;
		
			m_mthSetGUIFromContent(objContent);
		
			m_mthEnableModify(false);
		
			m_mthSetModifyControl(objContent,false);
		

		}

		protected void m_mthSetSelectedDeletedRecord(clsPatient p_objSelectedPatient,
			string p_strOpenDate)
		{
			//������
			if(p_objSelectedPatient==null|| p_strOpenDate==null ||p_strOpenDate=="")
				return;
		              
			clsTrackRecordContent objContent;              
			//��ȡ��¼
//			long lngRes = m_objDiseaseTrackDomain.m_lngGetDeleteRecordContent(p_objSelectedPatient.m_StrInPatientID,p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss"),p_strOpenDate ,out objContent);
			long lngRes = m_objDiseaseTrackDomain.m_lngGetDeleteRecordContent(p_objSelectedPatient.m_StrInPatientID,m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"),p_strOpenDate ,out objContent);
		
			if(lngRes <= 0 || objContent == null)
				return;
            
           
			//���õ�ǰ��¼����¼ʱ�� 
			m_objCurrentPatient=  p_objSelectedPatient;
			txtInPatientID.Text=this.m_objCurrentPatient.m_StrHISInPatientID;
			//			m_dtpCreateDate.Value=objContent.m_dtmCreateDate;

			//��һ�������ע�͵Ļ���m_BlnIsAddNew�ͻ���true�ˣ���ô�Ͳ��������¼��
			//			m_objCurrentRecordContent=objContent;
		
			m_mthSetDeletedGUIFromContent(objContent);
		
			//			m_mthSetModifyControl(objContent,false);		

		}


		/// <summary>
		/// ��ȡ���̼�¼�������ʵ��
		/// </summary>
		/// <returns></returns>
		protected virtual clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
		{
			//��ȡ���̼�¼�������ʵ�������Ӵ�������ʵ��			
			return null;
		}

		/// <summary>
		/// �Ƿ�����Ӽ�¼��true������ӣ�false���޸ġ�
		/// </summary>
		protected override bool m_BlnIsAddNew
		{
			get
			{
				return m_objCurrentRecordContent == null;
			}		
		}

		protected override long m_lngSubAddNew()
		{
			if(m_objReAddNewOld != null)
				return m_lngReAddNew();
			else  
				return m_lngAddNewRecord();	

		}

		/// <summary>
		/// ����¼�¼�����ݿⱣ�档
		/// </summary>
		/// <returns></returns>
		protected long m_lngAddNewRecord()
		{
			//��鵱ǰ���˱����Ƿ�Ϊnull
			if(m_objCurrentPatient==null)
			{
				m_mthShowNoPatient();
				return -1;
			}

			//��ȡ������ʱ��
			string strTimeNow=new clsPublicDomain().m_strGetServerTime();
		
			//�ӽ����ȡ��¼��Ϣ
			clsTrackRecordContent objContent = m_objGetContentFromGUI();     
		           
			//��������ֵ����           
			if(objContent == null)
				return -1;
		
			//���� clsTrackRecordContent ����Ϣ��ʹ�÷�����ʱ������m_dtmOpenDate��m_dtmModifyDate��
			objContent.m_dtmOpenDate=DateTime.Parse(strTimeNow);
			objContent.m_dtmModifyDate=DateTime.Parse(strTimeNow);

			objContent.m_bytIfConfirm=0;
			objContent.m_bytStatus=0;
			objContent.m_dtmCreateDate=m_dtpCreateDate.Value;
			objContent.m_dtmInPatientDate=m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate;
			objContent.m_strConfirmReason="";
			objContent.m_strConfirmReasonXML="";
			objContent.m_strCreateUserID=MDIParent.OperatorID;
			objContent.m_strInPatientID=m_objCurrentPatient.m_StrInPatientID;
			objContent.m_strModifyUserID=MDIParent.OperatorID;

			//�����¼
			clsPreModifyInfo objModifyInfo=null;
			long lngRes = m_objDiseaseTrackDomain.m_lngAddNewRecord(objContent,out objModifyInfo);
		        
			//���ݽ������ͬ�Ĵ���
			switch((enmOperationResult)lngRes)
			{
				case enmOperationResult.DB_Succeed:
					m_objCurrentRecordContent = objContent;           
					
					//��ӽڵ㵽ʱ���б���,��ѡ��
					m_mthAddNode(m_objCurrentRecordContent.m_dtmCreateDate,m_objCurrentRecordContent.m_dtmOpenDate);
					break; 
 
				case enmOperationResult.DB_Fail:
					clsPublicFunction.ShowInformationMessageBox("�Բ���,���ʧ��!");
					break;
				case enmOperationResult.Parameter_Error:
					clsPublicFunction.ShowInformationMessageBox("��������!");
					break;
				case enmOperationResult.Record_Already_Exist:
					m_mthShowRecordTimeDouble();
					break;					
					//...
			}  
		
			//���ؽ��
			return lngRes;			
		}

		/// <summary>
		/// ��ӽڵ㵽ʱ���б���,��ѡ��
		/// </summary>
		/// <param name="strTime"></param>
		private void m_mthAddNode(DateTime p_dtmCreateDate,DateTime p_dtmOpenDate)
		{ 
			string strCreateDate=p_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss");			
			TreeNode trnNode=new TreeNode(strCreateDate);	
			trnNode.Tag=p_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss");

			m_blnCanTreeNodeAfterSelectEventTakePlace=false;

			if(m_trnRoot.Nodes.Count==0 || trnNode.Text.CompareTo (m_trnRoot.LastNode.Text)<0)
			{
				m_trnRoot.Nodes.Add(trnNode);
				m_trvCreateDate.SelectedNode=m_trnRoot.LastNode;//
			}
			else 
			{
				for(int i=0;i<m_trnRoot.Nodes.Count;i++)
				{
					if(trnNode.Text.CompareTo (m_trnRoot.Nodes[i].Text)>0)
					{
						m_trnRoot.Nodes.Insert(i,trnNode);
						m_trvCreateDate.SelectedNode=m_trnRoot.Nodes[i];//
						break;
					}
				}
			}	
			m_blnCanTreeNodeAfterSelectEventTakePlace=true;
			m_dtpCreateDate.Enabled=false;
		}

		protected override long m_lngSubModify()
		{
			//��鵱ǰ���˱����Ƿ�Ϊnull
			if(m_objCurrentPatient==null)
			{
				m_mthShowNoPatient();
				return -1;
			}
			//��ȡ������ʱ��
			string strTimeNow=new clsPublicDomain().m_strGetServerTime();
			//�ӽ����ȡ��¼��Ϣ
			clsTrackRecordContent objContent = m_objGetContentFromGUI();     
		           
			//��������ֵ����           
			if(objContent == null)
				return -1;
		
			//���� clsTrackRecordContent ����Ϣ��ʹ�÷�����ʱ������m_dtmModifyDate��			
			objContent.m_dtmModifyDate=DateTime.Parse(strTimeNow);
			//�������м�¼�Ŀ�ʼʹ��ʱ��
			objContent.m_dtmOpenDate = m_objCurrentRecordContent.m_dtmOpenDate;   
			objContent.m_bytIfConfirm=0;
			objContent.m_bytStatus=0;
			objContent.m_dtmCreateDate=m_dtpCreateDate.Value;
			objContent.m_dtmInPatientDate=m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate;
			objContent.m_strConfirmReason="";
			objContent.m_strConfirmReasonXML="";
			objContent.m_strCreateUserID=m_objCurrentRecordContent.m_strCreateUserID;
			objContent.m_strInPatientID=m_objCurrentPatient.m_StrInPatientID;
			objContent.m_strModifyUserID=MDIParent.OperatorID;

			//�޸ļ�¼
			clsPreModifyInfo objModifyInfo=null;
			long lngRes = m_objDiseaseTrackDomain.m_lngModifyRecord(m_objCurrentRecordContent,objContent,out objModifyInfo);
		        
			//���ݽ������ͬ�Ĵ���
			switch((enmOperationResult)lngRes)
			{
				case enmOperationResult.DB_Succeed:
					m_objCurrentRecordContent = objContent;
					break; 
				case enmOperationResult.DB_Fail:
					clsPublicFunction.ShowInformationMessageBox("�Բ���,�޸�ʧ��!");
					break;
				case enmOperationResult.Parameter_Error:
					clsPublicFunction.ShowInformationMessageBox("��������!");
					break;
				case enmOperationResult.Record_Already_Modify:
					if(objModifyInfo !=null)
						m_bolShowRecordModified(objModifyInfo.m_strActionUserID,objModifyInfo.m_dtmActionTime.ToString("yyyy-MM-dd HH:mm:ss"));
					else m_mthShowDBError();
					break;	
				case enmOperationResult.Record_Already_Delete:
					if(objModifyInfo !=null)
						m_mthShowRecordDeleted(objModifyInfo.m_strActionUserID,objModifyInfo.m_dtmActionTime.ToString("yyyy-MM-dd HH:mm:ss"));
					else m_mthShowDBError();
					break;
					//...
			}  
		
			//���ؽ��
			return lngRes;
		}

		protected override long m_lngSubDelete()
		{
			//��鵱ǰ���˱����Ƿ�Ϊnull          
			if(m_objCurrentPatient==null)
			{
				m_mthShowNoPatient();
				return -1;
			}
			//��鵱ǰ��¼�Ƿ�Ϊnull
			if(m_objCurrentRecordContent==null)
			{				
				return -1;
			}
		
			//��ȡ������ʱ��      
			string strTimeNow=new clsPublicDomain().m_strGetServerTime();
			//���� m_objCurrentRecordContent ����Ϣ��ʹ�÷�����ʱ������m_dtmDeActivedDate��
			m_objCurrentRecordContent.m_dtmDeActivedDate=DateTime.Parse(strTimeNow);
			m_objCurrentRecordContent.m_strDeActivedOperatorID=MDIParent.OperatorID;
			
			//ɾ����¼
			clsPreModifyInfo objModifyInfo=null;
			long lngRes = m_objDiseaseTrackDomain.m_lngDeleteRecord(m_objCurrentRecordContent,out objModifyInfo);
		
			//���ݽ������ͬ�Ĵ���
			switch((enmOperationResult)lngRes)
			{
				case enmOperationResult.DB_Succeed:
					m_objCurrentRecordContent = null;       
					
					m_blnCanTreeNodeAfterSelectEventTakePlace=false;
					//ɾ��ѡ�нڵ� 
					m_trvCreateDate.SelectedNode.Remove();
					//��ռ�¼��Ϣ   
					m_mthClearPatientRecordInfo();
					//ѡ�и��ڵ�
					m_trvCreateDate.SelectedNode=m_trnRoot;
					m_blnCanTreeNodeAfterSelectEventTakePlace=true;
					break;   
				case enmOperationResult.DB_Fail:
					clsPublicFunction.ShowInformationMessageBox("�Բ���,ɾ��ʧ��!");
					break;
				case enmOperationResult.Parameter_Error:
					clsPublicFunction.ShowInformationMessageBox("��������!");
					break;
				case enmOperationResult.Record_Already_Modify:
					if(objModifyInfo !=null)
						m_bolShowRecordModified(objModifyInfo.m_strActionUserID,objModifyInfo.m_dtmActionTime.ToString("yyyy-MM-dd HH:mm:ss"));
					else m_mthShowDBError();
					break;	
				case enmOperationResult.Record_Already_Delete:
					if(objModifyInfo !=null)
						m_mthShowRecordDeleted(objModifyInfo.m_strActionUserID,objModifyInfo.m_dtmActionTime.ToString("yyyy-MM-dd HH:mm:ss"));
					else m_mthShowDBError();
					break;
			}  
		
			//���ؽ��
			return lngRes;
		}

		protected void m_mthUseReAddNew(clsPatient p_objSelectedPatient,
			string p_strOpenDate)
		{
			//������
			if(p_objSelectedPatient==null)
			{
				m_mthShowNoPatient();
				return;
			}			
			if(p_strOpenDate==null || p_strOpenDate=="")
			{	
				clsPublicFunction.ShowInformationMessageBox("��ѡ��Ҫ���ϵļ�¼��Ӧ�ļ�¼ʱ��!");
				return;
			}
         
			clsTrackRecordContent objContent;              
			//��ȡ��¼
			long lngRes = m_objDiseaseTrackDomain.m_lngGetRecordContent(p_objSelectedPatient.m_StrInPatientID,p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss"),p_strOpenDate ,out objContent);
		
			if(lngRes <= 0 || objContent == null)
				return;          
			                               
			m_objReAddNewOld = objContent;	                               
			m_objCurrentRecordContent = null;         
		
			//����ʱ��,��ʹ֮�����޸�
			m_dtpCreateDate.Enabled=false;
		
			m_mthReAddNewRecord(objContent);
			

		}

		/// <summary>
		/// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		protected virtual void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
		{
			//��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
		}

		/// <summary>
		/// �������������ݿⱣ�档
		/// </summary>
		/// <returns></returns>
		protected long m_lngReAddNew()
		{
			//��鵱ǰ���˱����Ƿ�Ϊnull
			if(m_objCurrentPatient==null)
			{
				m_mthShowNoPatient();
				return -1;
			}
			//��ȡ������ʱ��
			string strTimeNow=new clsPublicDomain().m_strGetServerTime();
			//�ӽ����ȡ��¼��Ϣ
			clsTrackRecordContent objContent = m_objGetContentFromGUI();     
		           
			//��������ֵ����           
			if(objContent == null)
				return -1;
		
			//���� clsTrackRecordContent ����Ϣ��ʹ�÷�����ʱ������m_dtmOpenDate��m_dtmModifyDate��
			objContent.m_dtmOpenDate=DateTime.Parse(strTimeNow);
			objContent.m_dtmModifyDate=DateTime.Parse(strTimeNow);
			objContent.m_bytIfConfirm=0;
			objContent.m_bytStatus=0;
			objContent.m_dtmCreateDate=m_dtpCreateDate.Value;
			objContent.m_dtmInPatientDate=m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate;
			objContent.m_strConfirmReason="";
			objContent.m_strConfirmReasonXML="";
			objContent.m_strCreateUserID=MDIParent.OperatorID;
			objContent.m_strInPatientID=m_objCurrentPatient.m_StrInPatientID;
			objContent.m_strModifyUserID=MDIParent.OperatorID;

			//����������¼
			clsPreModifyInfo objModifyInfo=null;
			long lngRes = m_objDiseaseTrackDomain.m_lngReAddNewRecord(m_objReAddNewOld,objContent,out objModifyInfo);
		        
			//���ݽ������ͬ�Ĵ���
			switch((enmOperationResult)lngRes)
			{
				case enmOperationResult.DB_Succeed:
					m_objCurrentRecordContent = objContent;  
					m_objReAddNewOld = null;
					break;
				case enmOperationResult.DB_Fail:
					clsPublicFunction.ShowInformationMessageBox("�Բ���,�޸�ʧ��!");
					break;
				case enmOperationResult.Parameter_Error:
					clsPublicFunction.ShowInformationMessageBox("��������!");
					break;
				case enmOperationResult.Record_Already_Modify:
					if(objModifyInfo !=null)
						m_bolShowRecordModified(objModifyInfo.m_strActionUserID,objModifyInfo.m_dtmActionTime.ToString("yyyy-MM-dd HH:mm:ss"));
					else m_mthShowDBError();
					break;	
				case enmOperationResult.Record_Already_Delete:
					if(objModifyInfo !=null)
						m_mthShowRecordDeleted(objModifyInfo.m_strActionUserID,objModifyInfo.m_dtmActionTime.ToString("yyyy-MM-dd HH:mm:ss"));
					else m_mthShowDBError();
					break;
					//...
			}  
		
			//���ؽ��
			return lngRes;

		}

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
				m_txtFocusedRichTextBox.m_mthSelectionDoubleStrikeThough(true);			
		}

		/// <summary>
		/// ����RichTextBox���ԡ����Ҽ��˵����û��������û�ID����ɫ�ȣ���
		/// </summary>
		/// <param name="p_objRichTextBox"></param>
		protected void m_mthSetRichTextBoxAttrib(ctlRichTextBox p_objRichTextBox)
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
				m_mthSetRichTextBoxAttrib((ctlRichTextBox)p_ctlControl);
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
		private ctlRichTextBox m_txtFocusedRichTextBox=null;//��ŵ�ǰ��ý����RichTextBox
		private void m_txtRichTextBox_GotFocus(object sender, System.EventArgs e)
		{
			m_txtFocusedRichTextBox=((ctlRichTextBox)(sender));
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
				((ctlRichTextBox)p_ctlControl).m_ClrOldPartInsertText = p_clrColor;				
			
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
				((ctlRichTextBox)p_ctlControl).m_BlnCanModifyLast = p_blnCanModifyLast;
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
		#endregion ctlRichTextBox��˫���ߡ�������������


		#region ��ӡ
		private void m_pdtPintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			m_mthPrintPage(e);
		}

		private void m_pdtPintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			m_mthBeginPrint(e);
		}

		private void m_pdtPintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			m_mthEndPrint(e);
		}	

		protected override long m_lngSubPrint()
		{
			//����Ƿ��д�ӡ���ݣ�����У���ӡ�����ݱ��������ӡ�ձ������ձ�����ֵ��
			if(m_objCurrentRecordContent != null)
			{
				//��������Ƿ����£���ȡ�������ݺ��״δ�ӡʱ��   
				clsTrackRecordContent objNewTrackInfo; 
				long lngRes = m_objDiseaseTrackDomain.m_lngGetPrintInfo(m_objCurrentRecordContent.m_strInPatientID,m_objCurrentRecordContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),m_objCurrentRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"),m_objCurrentRecordContent.m_dtmModifyDate,out objNewTrackInfo,out m_dtmFirstPrintDate,out m_blnIsFirstPrint);
				if(lngRes <= 0)
					return lngRes;  
			
				//��������������������ݣ��ѵ�ǰ���ݼ�¼��objNewTrackInfo��
				if(objNewTrackInfo == null)
				{
					objNewTrackInfo = m_objCurrentRecordContent;
				}
			
				//���ñ����ݵ���ӡ��
				m_mthSetPrintContent(objNewTrackInfo,m_dtmFirstPrintDate);
			}       
		
			//���û�����ù���ӡ���������ô�ӡ����        
			if(!m_blnAlreadySetPrintTools)
			{
				m_mthInitPrintTool();
				m_blnAlreadySetPrintTools = true;
			}                     
		
			//��ʼ��ӡ
			m_mthStartPrint();
		
			return 1;
		}

		/// <summary>
		///  ���ô�ӡ���ݡ�
		/// </summary>
		/// <param name="p_objContent"></param>
		protected virtual void m_mthSetPrintContent(clsTrackRecordContent p_objContent,DateTime p_dtmFirstPrintDate)
		{
			//ȱʡ�����κζ������Ӵ����������ṩ������
		}

		/// <summary>
		/// ��ʼ����ӡ����
		/// </summary>
		protected virtual void m_mthInitPrintTool()
		{
			//ȱʡ�����κζ������Ӵ����������ṩ����
			//��ʼ�����ݰ������д�ӡʹ�õ��ı��������塢���ʡ���ˢ����ӡ��ȡ�
		}

		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
		protected virtual void m_mthDisposePrintTools()
		{
			//ȱʡ�����κζ������Ӵ����������ṩ����
			//�ͷ����ݰ�����ӡʹ�õ������塢���ʡ���ˢ��ʹ��ϵͳ��Դ�ı�����
		}

		/// <summary>
		/// ��ʼ��ӡ��
		/// </summary>
		protected virtual void m_mthStartPrint()
		{
			//ȱʡʹ�ô�ӡԤ�����Ӵ��������ṩ�µ�ʵ��
			if(m_blnDirectPrint)
			{
				m_pdcPrintDocument.Print();
			}
			else
			{
				PrintPreviewDialog ppdPrintPreview = new PrintPreviewDialog();
				ppdPrintPreview.Document = m_pdcPrintDocument;
				ppdPrintPreview.ShowDialog();
			}
		}

		/// <summary>
		/// ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		protected void m_mthBeginPrint(PrintEventArgs p_objPrintArg)
		{
			m_mthBeginPrintSub(p_objPrintArg);
		}

		/// <summary>
		/// ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		protected virtual void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
		{
			//ȱʡ�����κζ������Ӵ����������ṩ����
		}

		/// <summary>
		/// ��ӡҳ
		/// </summary>
		/// <param name="p_objPrintPageArg"></param>
		protected void m_mthPrintPage(PrintPageEventArgs p_objPrintPageArg)
		{
			m_mthPrintPageSub(p_objPrintPageArg);
		}

		/// <summary>
		/// ��ӡҳ
		/// </summary>
		/// <param name="p_objPrintPageArg"></param>
		protected virtual void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
		{
		
		}

		/// <summary>
		/// ��ӡ����ʱ�Ĳ���
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		protected void m_mthEndPrint(PrintEventArgs p_objPrintArg)
		{
			//�����ӡ�ɹ������Ҳ��Ǵ�ӡ�ձ���������Ҫ�����״δ�ӡʱ�䣬�����״δ�ӡʱ�䡣
			if(!p_objPrintArg.Cancel)
			{
				m_mthUpdateFirstPrintDate();
			}                          
		
			m_mthEndPrintSub(p_objPrintArg);
		}

		protected void m_mthUpdateFirstPrintDate()
		{
			if(m_objCurrentRecordContent != null && m_blnIsFirstPrint)
			{
				m_objDiseaseTrackDomain.m_lngUpdateFirstPrintDate(m_objCurrentRecordContent.m_strInPatientID,m_objCurrentRecordContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),m_objCurrentRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"),m_dtmFirstPrintDate);
				m_blnIsFirstPrint = false;
			}
		}

		/// <summary>
		/// ��ӡ����ʱ�Ĳ���
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		protected virtual void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
		{
			//���Ӵ����������ṩ����
		}

		#endregion ��ӡ

		// ��ʾ�Ѿ�ɾ���ļ�¼���û�ѡ�񣬲����û�ѡ���������������Ϊ��ȫ��ȷ�����ݣ���ʾ�ڽ��档
		protected void m_mthLoadDeleteRecord()
		{
			//			frmSelectDeleteRecord frmSel = new frmSelectDeleteRecord(m_objDomain,m_objCurrentPatient,m_strReloadFormTitle(),MDIParent.OperatorID);
			//		
			//			if(frmSel.ShowDialog() == DialogResult.OK)
			//			{   
			//			
			//				clsTrackRecordContent objContent = frmSel.m_objGetDeleteRecord();              
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

		// ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
		public virtual string m_strReloadFormTitle()
		{
			//���Ӵ�������ʵ��
			return "";
		}

		private void m_trvCreateDate_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if(m_blnCanTreeNodeAfterSelectEventTakePlace==false)
				return;

			m_mthClearPatientRecordInfo();
			if(m_trvCreateDate.SelectedNode==m_trnRoot)
			{
				m_mthSelectRootNode();
				return;
			}
			else if(m_trvCreateDate.SelectedNode.Tag !=null)
				m_mthSetSelectedRecord(m_objCurrentPatient,m_trvCreateDate.SelectedNode.Tag.ToString());
		}
		
		/// <summary>
		/// ��ѡ����ڵ�ʱ,���������Ĭ��ֵ(���Ӵ�����Ҫ,������ʵ��)
		/// </summary>
		protected virtual void m_mthSelectRootNode()
		{
		}
	}
}

