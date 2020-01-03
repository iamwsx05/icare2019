using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;


using HRP; 
using weCare.Core.Entity;

namespace iCare
{
	/// <summary>
	/// Summary description for frmPrimaryForm.
	/// </summary>
	public class frmPrimaryForm : iCare.iCareBaseForm.frmBaseForm
	{
		private System.ComponentModel.IContainer components;

		public frmPrimaryForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
            m_objBorderTool = new clsBorderTool(Color.White);
			//Liu RongGuo 2003.4.11
			m_strTemplatePath = m_strGetFilePathHeader() + "Templates\\\\" ;	

//			#region ��ȡ��ǰԱ��������Ȩ��
//			objPIArr = clsLoginContext.s_ObjLoginContext.m_ObjPIArr;
//			#endregion

			if(MDIParent.s_bolIAnaSystem)
				m_objHighLight = new ctlHighLightFocus(Color.White);
			else
				m_objHighLight = new ctlHighLightFocus(clsHRPColor.s_ClrHightLight);
		}

		/// <summary>
		/// �ؼ��߿���ɫ���趨
		/// </summary>
        protected com.digitalwave.Utility.Controls.clsBorderTool m_objBorderTool;
		/// <summary>
		/// Crystal Reportģ���·��
		/// </summary>
		protected string m_strTemplatePath = "";
//		/// <summary>
//		/// ��¼Ա����Ȩ����Ϣ
//		/// </summary>
//		protected clsPrivilegeInfo[] objPIArr;
		/// <summary>
		/// ������ʾ��ý���Ŀռ�
		/// </summary>
		protected ctlHighLightFocus m_objHighLight;
		private System.Windows.Forms.ToolTip m_ttpTextInfo;
		/// <summary>
		/// ����ģ�����
		/// </summary>
		protected clsTemplatesetInvoke m_objTempTool;
		private System.Windows.Forms.ContextMenu m_cmuRichTextBoxMenu;
		private System.Windows.Forms.MenuItem mniDoubleStrikeOutDelete;

		/// <summary>
		/// ��Сʱ��
		/// </summary>
		protected readonly DateTime m_dtmEmptyDate = new DateTime(1900,1,1);

		/// <summary>
		/// Clean up any resources being used.
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmPrimaryForm));
			this.m_ttpTextInfo = new System.Windows.Forms.ToolTip(this.components);
			this.m_cmuRichTextBoxMenu = new System.Windows.Forms.ContextMenu();
			this.mniDoubleStrikeOutDelete = new System.Windows.Forms.MenuItem();
			// 
			// m_ttpTextInfo
			// 
			this.m_ttpTextInfo.AutomaticDelay = 200;
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
			// 
			// frmPrimaryForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1016, 733);
			this.Font = new System.Drawing.Font("����", 10.5F);
			this.ForeColor = System.Drawing.Color.White;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmPrimaryForm";
			this.Text = "frmPrimaryForm";
			this.Load += new System.EventHandler(this.frmPrimaryForm_Load);

		}
		#endregion

		#region Copy,Cut,Paste
		/// <summary>
		/// ���Ʋ���
		/// </summary>
		/// <returns>�������</returns>
		public long m_lngCopy()
		{
			Control ctlControl = this.ActiveControl;
			string strTypeName = ctlControl.GetType().Name;
			if(strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
			{
				switch(strTypeName)
				{
					case "ctlRichTextBox":
						if(((ctlRichTextBox)ctlControl).Text != "")
						{
							((ctlRichTextBox)ctlControl).Copy();
							return 1;
						}
						break;
					
					case "RichTextBox":
						if(((RichTextBox)ctlControl).Text != "")
						{
							((RichTextBox)ctlControl).Copy();
							return 1;
						}
						break;

					case "TextBox":
						if(((TextBox)ctlControl).Text != "")
						{
							((TextBox)ctlControl).Copy();
							return 1;
						}
						break;

					case "ctlBorderTextBox":
						if(((ctlBorderTextBox)ctlControl).Text != "")
						{
							((ctlBorderTextBox)ctlControl).Copy();
							return 1;
						}
						break;

					case "DataGridTextBox":
						if(((DataGridTextBox)ctlControl).Text != "")
						{
							((DataGridTextBox)ctlControl).Copy();
							return 1;
						}
						break;

					default:
						Clipboard.SetDataObject("");
						break;
				}
			}

			return 0;
		}

		/// <summary>
		/// ���в���
		/// </summary>
		/// <returns>�������</returns>
		public long m_lngCut()
		{
			Control ctlControl = this.ActiveControl;
			string strTypeName = ctlControl.GetType().Name;
			if(strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
			{
				switch(strTypeName)
				{
					case "ctlRichTextBox":
						if(((ctlRichTextBox)ctlControl).Text != "")
						{
							((ctlRichTextBox)ctlControl).Cut();
							return 1;
						}
						break;
					
					case "RichTextBox":
						if(((RichTextBox)ctlControl).Text != "")
						{
							((RichTextBox)ctlControl).Cut();
							return 1;
						}
						break;

					case "TextBox":
						if(((TextBox)ctlControl).Text != "")
						{
							((TextBox)ctlControl).Cut();
							return 1;
						}
						break;

					case "ctlBorderTextBox":
						if(((ctlBorderTextBox)ctlControl).Text != "")
						{
							((ctlBorderTextBox)ctlControl).Cut();
							return 1;
						}
						break;

					case "DataGridTextBox":
						if(((DataGridTextBox)ctlControl).Text != "")
						{
							((DataGridTextBox)ctlControl).Cut();
							return 1;
						}
						break;
				}
			}

			return 0;
		}

		/// <summary>
		/// ճ������
		/// </summary>
		/// <returns>�������</returns>
		public long m_lngPaste()
		{
			Control ctlControl = this.ActiveControl;
			string strTypeName = ctlControl.GetType().Name;

			if(strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
			{
				switch(strTypeName)
				{
					case "ctlRichTextBox":
						((ctlRichTextBox)ctlControl).Paste();
						break;

					case "RichTextBox":
						((RichTextBox)ctlControl).Paste();
						break;

					case "TextBox":
						((TextBox)ctlControl).Paste();
						break;

					case "ctlBorderTextBox":
						((ctlBorderTextBox)ctlControl).Paste();
						break;

					case "DataGridTextBox":
						((DataGridTextBox)ctlControl).Paste();
						break;
				}
				return 1;
			}

			return 0;
		}
		#endregion

		private void frmPrimaryForm_Load(object sender, System.EventArgs e)
		{
			m_objHighLight.m_mthAddControlInContainer(this);
		}

		#region ���ģ�幦��
		/// <summary>
		/// ���ģ��
		/// </summary>
		/// <param name="p_txtControl"></param>
		protected void m_mthAddRichTemplate(RichTextBox p_txtControl)
		{
			m_objTempTool.m_mthAddTextBox(this,p_txtControl,this.Name,p_txtControl.Name);
		}

		/// <summary>
		/// ���ģ��
		/// </summary>
		/// <param name="p_txtControl"></param>
		protected void m_mthAddRichTemplateInContainer(Control p_ctlContainer)
		{
			foreach(Control ctlChild in p_ctlContainer.Controls)
			{
				switch(ctlChild.GetType().Name)
				{
					case "ctlRichTextBox":
						m_mthAddRichTemplate((RichTextBox)ctlChild);
						m_mthAddRichTextInfo((ctlRichTextBox)ctlChild);
						break;
					case "RichTextBox":
						m_mthAddRichTemplate((RichTextBox)ctlChild);
						break;
					default:
						m_mthAddRichTemplateInContainer(ctlChild);
						break;
				}				
			}
		}
		#endregion

		#region ����RichTextBox����
		/// <summary>
		/// ����RichTextBox����
		/// </summary>
		/// <param name="p_ctlControl"></param>
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

		/// <summary>
		/// ����RichTextBox���ԡ����Ҽ��˵����û��������û�ID����ɫ�ȣ���
		/// </summary>
		/// <param name="p_objRichTextBox"></param>
		protected void m_mthSetRichTextBoxAttrib(ctlRichTextBox p_objRichTextBox)
		{
            m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[] { p_objRichTextBox });
			//�����Ҽ��˵�			
//			p_objRichTextBox.ContextMenu=m_cmuRichTextBoxMenu;
//			p_objRichTextBox.GotFocus += new EventHandler(m_txtRichTextBox_GotFocus);
//			
//			//������������			
//			p_objRichTextBox.m_StrUserID = MDIParent.OperatorID;
//			p_objRichTextBox.m_StrUserName = MDIParent.strOperatorName;
//			p_objRichTextBox.m_ClrOldPartInsertText = Color.White;
//			p_objRichTextBox.m_ClrDST = Color.Red;
		}
		#endregion

		#region ��ȡ��ӡ·��
		/// <summary>
		/// ��ȡ��ӡ·��
		/// </summary>
		/// <returns></returns>
		public string m_strGetFilePathHeader() 
		{
			string [] strFilePathAll =  Application.ExecutablePath.Split('\\') ;
			string strFilePathHeader="";
			if(strFilePathAll!=null)
				for(int i=0;i<strFilePathAll.Length-3;i++)
					strFilePathHeader+=strFilePathAll[i]+"\\\\";
			return strFilePathHeader;
		}
		#endregion

		#region MessageBox ��ʾ
		/// <summary>
		/// ��ʾ���ݿ�������Ϣ���ɸ����ṩ�µ�ʵ�֣�
		/// </summary>
		protected virtual void m_mthShowDBError()
		{
			clsPublicFunction.ShowInformationMessageBox("�Բ������ݿ�����");
		}

		/// <summary>
		/// ��ʾ��¼�ѱ�ɾ����Ϣ���ɸ����ṩ�µ�ʵ�֣�
		/// </summary>
		protected virtual void m_mthShowRecordDeleted(string p_strDeleteUserID,string p_strDeleteTime)
		{
			if(p_strDeleteUserID == null || p_strDeleteUserID =="")
				return;
			if(p_strDeleteTime == null || p_strDeleteTime == "")
				return;
			string m_strDeleteTime;
			string m_strDeleteUserName;
			try
			{
				m_strDeleteUserName = new clsEmployee(p_strDeleteUserID).m_StrFirstName;
				m_strDeleteTime = DateTime.Parse(p_strDeleteTime).ToString("yyyy��MM��dd�� HH:mm:ss");
			}
			catch
			{
				return;
			}
			
			clsPublicFunction.ShowInformationMessageBox("�Բ��𣬸ü�¼�ѱ� "+ m_strDeleteUserName +" �� "+m_strDeleteTime+" ɾ����");
		}

		/// <summary>
		/// ��ʾ��¼�ѱ������޸���Ϣ���ɸ����ṩ�µ�ʵ�֣�
		/// </summary>
		protected virtual bool m_bolShowRecordModified(string p_strModifyUserID,string p_strModifyTime)
		{
			//			clsPublicFunction.ShowInformationMessageBox("�Բ��𣬸ü�¼�ѱ������޸ģ�");
			if(p_strModifyUserID == null || p_strModifyUserID =="")
				return false;
			if(p_strModifyTime == null || p_strModifyTime == "")
				return false;
			string m_strModifyTime;
			string m_strModifyUserName;
			try
			{
				m_strModifyUserName = new clsEmployee(p_strModifyUserID).m_StrFirstName;
				m_strModifyTime = DateTime.Parse(p_strModifyTime).ToString("yyyy��MM��dd�� HH:mm:ss");
			}
			catch
			{
				return false;
			}
			if(clsPublicFunction.ShowQuestionMessageBox("�Բ��𣬸ü�¼�ѱ� "+ m_strModifyUserName +" �� "+m_strModifyTime+" �޸ģ��Ƿ���¼�¼��") == DialogResult.Yes)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// ��ʾ��¼�ѱ�����������Ϣ���ɸ����ṩ�µ�ʵ�֣�
		/// </summary>
		protected virtual void m_mthShowRecordTimeDouble()
		{
			clsPublicFunction.ShowInformationMessageBox("�Բ��𣬸�ʱ���Ѿ��������û����ɹ���¼������Ҫ�޸ģ���رյ�ǰ���壬�����´򿪣�");
		}

		/// <summary>
		/// ��ʾû�иò��ˣ��ɸ����ṩ�µ�ʵ�֣�
		/// </summary>
		protected virtual void m_mthShowNoPatient()
		{
			clsPublicFunction.ShowInformationMessageBox("�Բ���û�д˲��ˣ�");
		}

		/// <summary>
		/// ��ʾû�иò��ˣ��ɸ����ṩ�µ�ʵ�֣�
		/// </summary>
		protected virtual void m_mthShowNotPermitted()
		{
			clsPublicFunction.ShowInformationMessageBox("�Բ�������Ȩ�޲�����");
		}
		#endregion

		#region RichTextBox����ʾ��ʾ

		protected void m_mthAddRichTextInfo(ctlRichTextBox p_ctlTextBox)
		{
			p_ctlTextBox.m_evtMouseEnterDeleteText += new EventHandler(m_mthHandleMouseEnterDeleteText);
			p_ctlTextBox.m_evtMouseEnterInsertText += new EventHandler(m_mthHandleMouseEnterInsertText);
			p_ctlTextBox.MouseLeave += new EventHandler(m_mthHandleMouseLeaveControl);
		}

		private void m_mthHandleMouseLeaveControl(object p_objSender,EventArgs p_objArg)
		{
			m_ttpTextInfo.RemoveAll();
		}

		private void m_mthHandleMouseEnterDeleteText(object p_objSender,EventArgs p_objArg)
		{
			clsDoubleStrikeThoughEventArg objArg = (clsDoubleStrikeThoughEventArg)p_objArg;

			string strInfo = "�û�ID : "+objArg.m_strUserID+"\r\n�û����� : "+
				objArg.m_strUserName+"\r\nɾ��ʱ�� : ";

			if(objArg.m_dtmDeleteTime != m_dtmEmptyDate && objArg.m_dtmDeleteTime != DateTime.MinValue)
			{
				strInfo += objArg.m_dtmDeleteTime.ToLongDateString()+" "+objArg.m_dtmDeleteTime.ToLongTimeString();				
			}	
			else
			{
				strInfo += "----��--��--�� --:--:--";
			}
			
			m_ttpTextInfo.SetToolTip((Control)p_objSender,strInfo);
		}

		private void m_mthHandleMouseEnterInsertText(object p_objSender,EventArgs p_objArg)
		{
			com.digitalwave.Utility.Controls.clsInsertEventArg objArg = (com.digitalwave.Utility.Controls.clsInsertEventArg)p_objArg;
			
			if(objArg.m_intUserSeq == 1)
			{
				return;
			}
			
			string strInfo = "�û�ID : "+objArg.m_strUserID+"\r\n�û����� : "+
				objArg.m_strUserName+"\r\n���ʱ�� : ";

			if(objArg.m_dtmInsertTime != m_dtmEmptyDate && objArg.m_dtmInsertTime != DateTime.MinValue)
			{
				strInfo += objArg.m_dtmInsertTime.ToLongDateString()+" "+objArg.m_dtmInsertTime.ToLongTimeString();				
			}	
			else
			{
				strInfo += "----��--��--�� --:--:--";
			}
			
			m_ttpTextInfo.SetToolTip((Control)p_objSender,strInfo);
		}

		#endregion

		#region ���RichText��������(������DataGrid�ı߿�����)
		private ctlRichTextBox m_txtFocusedRichTextBox=null;//��ŵ�ǰ��ý����RichTextBox
		private void m_mthSetRichTextAttrib()
		{			
			m_mthSetRichTextEvent(this);			
		}
		#region ���õݹ���ã���ȡ���������н���ctlRichTextBox�ؼ��ĸ�������,Jacky-2003-2-25	
		/// <summary>
		/// ���õݹ���ã���ȡ���������н���ctlRichTextBox�ؼ��ĸ�������,Jacky-2003-2-25	
		/// </summary>
		/// <param name="p_ctlControl"></param>
		private void m_mthSetRichTextEvent(Control p_ctlControl)
		{
						
			if(p_ctlControl.GetType().Name=="ctlRichTextBox")
			{
                m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[] { p_ctlControl });
//				p_ctlControl.ContextMenu=m_cmuRichTextBoxMenu;
				p_ctlControl.GotFocus += new EventHandler(m_txtRichTextBox_GotFocus);

				((ctlRichTextBox)p_ctlControl).m_StrUserID = MDIParent.strOperatorID;
				((ctlRichTextBox)p_ctlControl).m_StrUserName = MDIParent.strOperatorName;
				((ctlRichTextBox)p_ctlControl).m_ClrOldPartInsertText = Color.Black;
				((ctlRichTextBox)p_ctlControl).m_ClrDST = Color.Red;
				m_mthAddRichTextInfo( (ctlRichTextBox)p_ctlControl );
			}
			
			if(p_ctlControl.HasChildren && p_ctlControl.GetType().Name !="DataGrid" )
			{									
				foreach(Control subcontrol in p_ctlControl.Controls)
				{										
					m_mthSetRichTextEvent(subcontrol);						
				} 	
			}				
		}
		#endregion
		/// <summary>
		/// ˫����ɾ���¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void mniDoubleStrikeOutDelete_Click(object sender, System.EventArgs e)
		{
			if(m_txtFocusedRichTextBox!=null)
				m_txtFocusedRichTextBox.m_mthSelectionDoubleStrikeThough(true);			
		}
		/// <summary>
		/// RichTextBox ��ý���ʱ���¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_txtRichTextBox_GotFocus(object sender, System.EventArgs e)
		{
			m_txtFocusedRichTextBox=((ctlRichTextBox)(sender));
		}
		#endregion

		#region ��Ӽ��̿�ݼ�
		/// <summary>
		/// ��Ӽ��̿�ݼ�
		/// </summary>
		protected void m_mthSetQuickKeys()
		{			
			m_mthSetControlEvent(this);			
		}
		#region ���õݹ���ã���ȡ���������н����¼�,Jacky-2003-2-21	
		/// <summary>
		/// ���õݹ���ã���ȡ���������н����¼�,Jacky-2003-2-21	
		/// </summary>
		/// <param name="p_ctlControl"></param>
		private void m_mthSetControlEvent(Control p_ctlControl)
		{

			string strTypeName = p_ctlControl.GetType().Name;
			if(strTypeName != "Lable" && strTypeName != "Button")
			{
				p_ctlControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthBaseEvent_KeyDown);
				if(p_ctlControl.HasChildren && strTypeName !="DataGrid" && strTypeName !="DateTimePicker" && strTypeName !="ctlComboBox")
				{									
					foreach(Control subcontrol in p_ctlControl.Controls)
					{										
						m_mthSetControlEvent(subcontrol);						
					} 	
				}				
			}			

		}
		
		/// <summary>
		/// ����ɸ���ʵ��
		/// </summary>
		protected virtual void m_mthBaseEvent_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			
		}
		#endregion
		#endregion


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
	}
}
