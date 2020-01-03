using System;
using System.Xml;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using com.digitalwave.Utility;
using com.digitalwave.Utility.Controls;
using weCare.Core.Entity;
using System.Data;
//using com.digitalwave.iCare.middletier.ICU;
using com.digitalwave.iCare.common;
//using iCare.Common;
using com.digitalwave.Emr.Signature_gui;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// Ranson��������
	/// </summary>
	public partial class frmRansonEvaluation : frmValuationBaseForm,PublicFunction
	{
		#region Define
		private System.Timers.Timer timAutoCollect;
		private System.Windows.Forms.GroupBox gpbEvaluation;
		private com.digitalwave.Utility.Controls.ctlTimePicker dtpStartSample;
		private System.Windows.Forms.Label lblTitle96;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtAutoTime;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label lblTitle31;
		private System.Windows.Forms.Label lblEvalDate;
		private System.Windows.Forms.Label lbltxtEvalDoctor;
		private System.Windows.Forms.DataGrid m_dtgResult;
		private com.digitalwave.Utility.Controls.ctlTimePicker dtpEvalDate;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.RadioButton m_rdbFds;
		private System.Windows.Forms.RadioButton m_rdbDs;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label m_lblBXB;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtBXB;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label m_lblXXB;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtXXB;
		private System.Windows.Forms.Label m_lblXNS;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtXNS;
		private System.Windows.Forms.Label m_lblXG;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtXG;
		private System.Windows.Forms.Label m_lblDMX;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtDMX;
		private System.Windows.Forms.Label m_lblJQS;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtJQS;
		private System.Windows.Forms.Label m_lblYTZ;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtYTZ;
		private System.Windows.Forms.Label m_lblRST;
		private System.Windows.Forms.Label m_lblTDA;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtTDA;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtXT;
		private System.Windows.Forms.Label m_lblXT;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtRST;
		private System.Windows.Forms.Label lblTitle11;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn7;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn9;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn10;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn11;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn12;
		private PinkieControls.ButtonXP cmdStartAuto;
		private PinkieControls.ButtonXP cmdCalculate;
		private PinkieControls.ButtonXP cmdShowResult;
		private PinkieControls.ButtonXP cmdStopAuto;
        private PinkieControls.ButtonXP cmdGetData;
		private PinkieControls.ButtonXP m_cmdGetCheckData;
		private System.ComponentModel.IContainer components = null;

		#endregion

		public frmRansonEvaluation()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// ��ʼ���ؼ�
			m_mthInit();

			
			m_dtlResult = new DataTable("result");

			#region �Զ���¼���ڵĳ�ʼ��
			ColumnHeader chResult1 = new ColumnHeader();
			chResult1.Text = "��ϸ��";
			chResult1.Width = 100;

			ColumnHeader chResult2 = new ColumnHeader();
			chResult2.Text = "Ѫ��";
			chResult2.Width = 50;

			ColumnHeader chResult3 = new ColumnHeader();
			chResult3.Text = "��������ø";
			chResult3.Width = 120;

			ColumnHeader chResult4 = new ColumnHeader();
			chResult4.Text = "�춬����ת��ø";
			chResult4.Width = 180;

			ColumnHeader chResult5 = new ColumnHeader();
			chResult5.Text = "Ѫϸ�������½�";
			chResult5.Width = 180;
			
			ColumnHeader chResult6 = new ColumnHeader();
			chResult6.Text = "Ѫ���ص�����";
			chResult6.Width = 150;

			ColumnHeader chResult7 = new ColumnHeader();
			chResult7.Text = "Ѫ��";
			chResult7.Width = 50;

			ColumnHeader chResult8 = new ColumnHeader();
			chResult8.Text = "����Ѫ����ѹ";
			chResult8.Width = 150;

			ColumnHeader chResult9 = new ColumnHeader();
			chResult9.Text = "��ȱʧ";
			chResult9.Width = 100;

			ColumnHeader chResult10 = new ColumnHeader();
			chResult10.Text = "Һ������";
			chResult10.Width = 100;

			ColumnHeader chResult11 = new ColumnHeader();
			chResult11.Text = "������";
			chResult11.Width = 100;

			frmAutoResult = new frmAutoEvalResult(chResult11,chResult1,chResult2,chResult3,chResult4,chResult5,chResult6,chResult7,chResult8, chResult9,chResult10);
			frmAutoResult.Text = "Ranson�Զ�����";
			frmAutoResult.Owner = this;
			frmAutoResult.Visible = false;
			#endregion            
            
			objDomain = new clsRansonEvaluationDomain();
			strAddOrModify = "0";

			m_objBorderTool = new com.digitalwave.Utility.Controls.clsBorderTool(Color.White);
			m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{
																			 trvActivityTime,m_dtgResult
																		 });
			//�����ȼ�
			m_mthSetQuickKeys();

			m_objHighLight = new ctlHighLightFocus(clsHRPColor.s_ClrHightLight);
		}

		


		// �û��Զ������*********************************************************************************

		#region �û��Զ������

		protected ctlHighLightFocus									m_objHighLight;

		private DataTable											m_dtlResult;
		private com.digitalwave.Utility.Controls.clsBorderTool		m_objBorderTool;
		private frmAutoEvalResult										frmAutoResult;
		private string												strAddOrModify;
		private clsEvalInfoOfRansonEvaluation							m_objRansonEvaluationDB;//����m_mthDisplay
		
		private clsRansonEvaluationDomain								objDomain;//Ranson
		private int m_intKind = 0;
		private clsSystemContext									m_objCurrentContext
		{
			get
			{
				return clsSystemContext.s_ObjCurrentContext;
			}
		}

		//��ʱû���õ��ı���
		private const string _strTypeMonitor = "Monitoring";
		private const string _strTypeVentilator = "Ventilator";
		#endregion//�û��Զ������


		// PublicFunction�ӿڵ�ʵ��***********************************************************************
		// ��MDIParent���ã���ʵ���Ӵ�����صĸ��ֲ���

		#region Interface PublicFunction �ĺ���ʵ��
		public override void Save()
		{
			m_lngSubSave();
		}
		public override long m_lngSubSave()
		{
			return m_lngSaveWithMessageBox();
		}
        public override void Delete()
		{
            //if(m_objCurrentContext.m_ObjControl.m_enmDeleteCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
            //    == enmDBControlCheckResult.Disable)
            //{
            //    clsPublicFunction.s_mthShowNotPermitMessage();
            //    return;
            //}

			if(m_strInPatientID == null || m_strInPatientID == "")
			{
				clsPublicFunction.ShowInformationMessageBox("�����벡��סԺ��!");
				return;
			}
			if(m_strCreateDate == null || m_strCreateDate == "")
			{
				clsPublicFunction.ShowInformationMessageBox("�����б���ѡ����Ӧ������ʱ�䣡");
				return;
			}

			if(!clsPublicFunction.s_blnAskForDelete())
				return ;

			long lngRes = objDomain.m_lngDeactive(clsBaseInfo.LoginEmployee.m_strEMPID_CHR, m_strInPatientID, m_strInPatientDate, m_strCreateDate);
			
			if(lngRes <= 0)
			{
				clsPublicFunction.ShowInformationMessageBox("ɾ��ʧ�ܣ������²���!");
				return;
			}

			foreach(TreeNode trnNode in trvActivityTime.Nodes[0].Nodes)
			{
				if((DateTime)trnNode.Tag == DateTime.Parse(m_strCreateDate))
				{
					trnNode.Remove();
					break;
				}
			}
			this.trvActivityTime.SelectedNode=this.trvActivityTime.Nodes[0];
			ClearUp();//�̳���frmValuationBaseForm
		}

        public override void Display() { }//NULL
        public override void Display(string cardno, string sendcheckdate) { }
        public override void Print()
		{
			m_lngPrint();
		}
        public override void Copy()
		{
			m_lngCopy();
		}
        public override void Cut()
		{
			m_lngCut();
		}
        public override void Paste()
		{
			m_lngPaste();
		}
        public override void Redo() { }//NULL
        public override void Undo() { }//NULL
        public override void Verify()
		{
			////long lngRes=m_lngSignVerify(p_strFormID,p_strRecordID);
		}

		#endregion


		
		//�û��Զ��庯��*********************************************************************************
		#region �û��Զ��庯��

		/// <summary>
		/// ��ʼ���ؼ�
		/// </summary>
		private void m_mthInit()
		{
			

		}

		/// <summary>
		/// �������ݿ�     ��Ҫ�õ�m_lngSaveWithoutMessageBox()
		/// </summary>
		/// <returns></returns>
		private long m_lngSaveWithMessageBox()
		{
			long lngRes=m_lngSaveWithoutMessageBox();
			if(lngRes==-11)
			{
				clsPublicFunction.ShowInformationMessageBox("�����޸ĵļ�¼�ѱ�����ɾ���򲻴��ڣ�");				
			}
			else if(lngRes==-21)
			{
				clsPublicFunction.ShowInformationMessageBox("�Բ��𣬱���ʧ�ܣ�");
			}
			else if(lngRes==-31)
			{
				clsPublicFunction.ShowInformationMessageBox("�Բ��𣬱���¼�ѱ������޸ģ������¶�ȡһ�Σ�");
			}
			return lngRes;
		}

		/// <summary>
		/// �������ݿ�     ��Ҫ��ô�ȡ���ݿ��Ȩ��
		/// </summary>
		/// <returns></returns>
		private long m_lngSaveWithoutMessageBox()
		{
			if(m_strInPatientID == null || m_strInPatientID == "")
			{
				clsPublicFunction.ShowInformationMessageBox("�Բ��������벡��סԺ��ţ�");
				return 0;
			}

			string strCurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			
			//��ֵ
			this.cmdCalculate_Click(null,null);
			clsEvalInfoOfRansonEvaluation objEvalInfo = m_GetCurrentEvalInfo();

			if(m_strCreateDate != "")
			{
                //if(m_objCurrentContext.m_ObjControl.m_enmModifyCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
                //    == enmDBControlCheckResult.Disable)
                //{
                //    clsPublicFunction.s_mthShowNotPermitMessage();
                //    return 0;
                //}
				clsEvalInfoOfRansonEvaluation objTemp;
				long lngExist = objDomain.m_lngGetRansonValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out objTemp);

				if(lngExist == 0)
					return -11;
				
				if(lngExist == 1)
				{
                    //if(DateTime.Parse(objTemp.strModifyDate) != DateTime.Parse(m_objRansonEvaluationDB.strModifyDate))
                    //    return -31;

					if(!clsPublicFunction.s_blnAskForModify())
						return 0;
				}
			}
			else
			{
                //if(m_objCurrentContext.m_ObjControl.m_enmAddNewCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
                //    == enmDBControlCheckResult.Disable)
                //{
                //    clsPublicFunction.s_mthShowNotPermitMessage();
                //    return 0;
                //}

				clsEvalInfoOfRansonEvaluation objTemp;
				long lngExist = objDomain.m_lngGetRansonValue(m_strInPatientID,m_strInPatientDate,dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),out objTemp);
				
				if(lngExist == 1)
				{
					m_strCreateDate = dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
					if(!clsPublicFunction.s_blnAskForModify())
						return 0;
				}
				else
				{
					m_strCreateDate = "";
				}
			}

			long lngRes = objDomain.m_lngSave(objEvalInfo);
			if(lngRes<=0)
			{
				return -21;
			}
			else
			{
				if(m_strCreateDate == "")
				{
					TreeNode m_trnNewNode = new TreeNode(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
					m_trnNewNode.Tag = DateTime.Parse(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
					trvActivityTime.Nodes[0].Nodes.Add(m_trnNewNode);
					trvActivityTime.SelectedNode = trvActivityTime.Nodes[0];
					trvActivityTime.SelectedNode = m_trnNewNode;
				}
				else
				{
					TreeNode m_trnTempNode = trvActivityTime.SelectedNode;
					trvActivityTime.SelectedNode = trvActivityTime.Nodes[0];
					trvActivityTime.SelectedNode = m_trnTempNode;
				}
			}

			return 1;
		}

	
		/// <summary>
		/// ��WinForm�����ȡ������Ҫ������
		/// </summary>
		/// <returns></returns>
		private clsEvalInfoOfRansonEvaluation m_GetCurrentEvalInfo()
		{
			string str = "$$";
			clsEvalInfoOfRansonEvaluation objEvalInfo = new clsEvalInfoOfRansonEvaluation();

			objEvalInfo.strPatientID		= m_strInPatientID;
			objEvalInfo.strInPatientDate	= m_strInPatientDate;
			objEvalInfo.strActivityTime		= (m_strCreateDate == "") ? this.dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : m_strCreateDate;

			objEvalInfo.strEvalDoctorID		= clsBaseInfo.LoginEmployee.m_strEMPID_CHR;
			objEvalInfo.strEvalDoctorName	= clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;

			objEvalInfo.strKind				= str + m_intKind.ToString();
			//objEvalInfo.strAge				= this.lbltxtAge.Text;
			objEvalInfo.strAge				= m_objCurrentPatient.m_ObjPeopleInfo.m_IntAge.ToString();
			
			objEvalInfo.strBXB				= str + m_txtBXB.Text;
			objEvalInfo.strXT				= str + m_txtXT.Text;
			objEvalInfo.strRST				= str + m_txtRST.Text;
			objEvalInfo.strTDA				= str + m_txtTDA.Text;
			objEvalInfo.strXXB				= str + m_txtXXB.Text;
			objEvalInfo.strXNS				= str + m_txtXNS.Text;
			objEvalInfo.strXG				= str + m_txtXG.Text;
			objEvalInfo.strDMX				= str + m_txtDMX.Text;
            objEvalInfo.strJQS				= str + m_txtJQS.Text;
			objEvalInfo.strYTZ				= str + m_txtYTZ.Text;
	
			objEvalInfo.strMortality = str + m_dtlResult.Rows[0].ItemArray[10].ToString();
			return objEvalInfo;
		}


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


		/// <summary>
		/// ���ÿ�ݼ�
		/// </summary>
		private void m_mthSetQuickKeys()
		{			
			m_mthSetControlEvent(this);			
		}
		
		private void m_mthSetControlEvent(Control p_ctlControl)
		{
			#region ���õݹ���ã���ȡ���������н����¼�	
			string strTypeName = p_ctlControl.GetType().Name;
			if(strTypeName != "Label"  && strTypeName != "CheckBox" )
			{
				p_ctlControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);
				if(p_ctlControl.HasChildren && strTypeName !="DataGrid" && strTypeName !="DateTimePicker" && strTypeName !="ctlComboBox")
				{									
					foreach(Control subcontrol in p_ctlControl.Controls)
					{				
						string strSubTypeName = subcontrol.GetType().Name;
						if(strSubTypeName != "Label" &&  strSubTypeName != "CheckBox" )												
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
					break;
						
				case 113://save					
					this.Save(); 
					break;
				case 114://del
					this.Delete(); 
					break;
				case 115://print
					this.Print();
					break;
				case 116://refresh
					m_blnCanTextChange = false;
					ClearUp();
					m_mthClearPatientInfo();
					this.trvActivityTime.Nodes[0].Nodes.Clear();
					m_blnCanTextChange = true;
					break;
				case 117://Search					
					break;
			}	
		}

		/// <summary>
		/// ������������ֵ
		/// </summary>
		protected override void ClearUp()
		{
			try
			{
				object [] res = {"/","/","/","/","/","/","/","/","/","/","/"};
				m_dtlResult.Rows[0].ItemArray = res;

				foreach(Control control in this.Controls)
				{				
					string typeName = control.GetType().Name;
					if(typeName == "TextBox"&&control.Name!="txtCardNo")
					{
						control.Text = "";
						
					}
					else if(typeName=="CheckBox")
					{
						((CheckBox)control).Checked=false;
					}
					else if(typeName=="dwtFlatComboBox")
					{
						control.Text="";
					}
				}
                txtAutoTime.Text = "60";	
				lbltxtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;


				m_rdbFds.Checked	= true;
				m_txtBXB.Text		= "";
				m_txtXT.Text		= "";
				m_txtRST.Text		= "";
				m_txtTDA.Text		= "";
				m_txtXXB.Text		= "";
				m_txtXNS.Text		= "";
				m_txtXG.Text		= "";
				m_txtDMX.Text		= "";
				m_txtJQS.Text		= "";
				m_txtYTZ.Text		= "";


				dtpEvalDate.Value = DateTime.Now;
				strAddOrModify = "0";//����Ϊ������״̬

				m_strCreateDate = "";
			}
			catch
			{
			}
		}

		private void m_mthClearPatientInfo()
		{			
			m_strInPatientID = "";
			m_strInPatientDate = "";
		}

		/// <summary>
		/// TIMEPICKER���¼�������,���TIMEPICKER�̳��Ը���
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strPatientDate"></param>
		/// <param name="p_strFromDate"></param>
		/// <param name="p_strToDate"></param>
		protected override void m_mthLoadAllRecordTimeOfAPatient(string p_strPatientID,string p_strPatientDate,string p_strFromDate, string p_strToDate)
		{
			if(p_strPatientID ==null || p_strPatientID =="") 
				return ;

			this.trvActivityTime.Nodes[0].Nodes.Clear();

			DateTime [] m_dtmArr = objDomain.m_dtmGetTimeInfoOfAPatientArr(p_strPatientID ,p_strPatientDate, p_strFromDate, p_strToDate);

			if(m_dtmArr==null) return ;

			for(int i=0;i<m_dtmArr.Length ;i++)
			{
				string strDate = m_dtmArr[i].ToString("yyyy-MM-dd HH:mm:ss");
				TreeNode trnDate=new TreeNode(strDate);
				trnDate.Tag = m_dtmArr[i];
				this.trvActivityTime.Nodes[0].Nodes.Add(trnDate);
			}
			this.trvActivityTime.ExpandAll();
			trvActivityTime.SelectedNode = trvActivityTime.Nodes[0];
		}

		
		/// <summary>
		/// ������ʾ��TreeView��ѡ�������
		/// </summary>
		protected override void m_mthDisplay()
		{
			long lngRes  = objDomain.m_lngGetRansonValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out m_objRansonEvaluationDB);

			if(m_objRansonEvaluationDB == null)
				return;

			lbltxtEvalDoctor.Text = m_objRansonEvaluationDB.strEvalDoctorName;
			
			if(int.Parse(m_objRansonEvaluationDB.strKind)==0)
				m_rdbFds.Checked = true;
			else
				m_rdbDs.Checked = true;
		
			m_txtBXB.Text			= m_objRansonEvaluationDB.strBXB;
			m_txtXT.Text			= m_objRansonEvaluationDB.strXT;
			m_txtRST.Text			= m_objRansonEvaluationDB.strRST;
			m_txtTDA.Text			= m_objRansonEvaluationDB.strTDA;
			m_txtXXB.Text			= m_objRansonEvaluationDB.strXXB;
			m_txtXNS.Text			= m_objRansonEvaluationDB.strXNS;
			m_txtXG.Text			= m_objRansonEvaluationDB.strXG;
			m_txtDMX.Text			= m_objRansonEvaluationDB.strDMX;
			m_txtJQS.Text			= m_objRansonEvaluationDB.strJQS;
			m_txtYTZ.Text			= m_objRansonEvaluationDB.strYTZ;


			

			//
			//			m_cboOpenEyes.SelectedIndex = int.Parse(m_objRansonEvaluationDB.strOpenEyes);
			//			m_cboSay.SelectedIndex		= int.Parse(m_objRansonEvaluationDB.strSay);
			//			m_cboSport.SelectedIndex	= int.Parse(m_objRansonEvaluationDB.strSport);
			m_blnCalcSuccess();

				
			dtpEvalDate.Value = DateTime.Parse(m_objRansonEvaluationDB.strActivityTime);
            clsEmrEmployeeBase_VO objEmployee = null;
            clsBaseInfo.m_lngGetEmpByID(m_objRansonEvaluationDB.strEvalDoctorID, out objEmployee);
			
			lbltxtEvalDoctor.Text = objEmployee.m_strLASTNAME_VCHR;

			strAddOrModify = "1";
		
		}

		/// <summary>
		/// ���Զ�����ʱ�Զ���������
		/// </summary>
		public void m_AutoSave(string m_strAutoEvalDate)
		{
			try
			{
				this.cmdCalculate_Click(null,null);
				if(strAddOrModify == "0")
				{
					clsEvalInfoOfRansonEvaluation objEvalInfo		= m_GetCurrentEvalInfo();
					objEvalInfo.strActivityTime					= m_strAutoEvalDate;
					if(objDomain.m_lngSave(objEvalInfo) > 0)
					{
						dtpEvalDate.Value = DateTime.Parse(m_strAutoEvalDate);
                        TreeNode m_trnNewNode = new TreeNode(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        m_trnNewNode.Tag = DateTime.Parse(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        trvActivityTime.Nodes[0].Nodes.Add(m_trnNewNode); 
					}
				}
			}
			catch
			{
			}
		}

		/// <summary>
		/// �Ӻ������ͼ໤�Ƕ�ȡ����
		/// </summary>
		private void GetData()
		{
			#region Old
//			try
//			{
//				XmlDocument			objXMLDoc	= new XmlDocument();
//
//				clsCMSData			objCMSData;				//�໤��
//				clsVentilatorData	objVentilatorData;		//������
//
//				m_mthGetICUDataByTime(dtpStartSample.Value,out objCMSData,out objVentilatorData);
//
//				//				if (objCMSData != null)
//				//				{
//				//					if(objCMSData.m_strHeartRate == null || objCMSData.m_strHeartRate == "")
//				//						
//				//						m_txtHR.Text = "";
//				//					else
//				//						
//				//					m_txtHR.Text = objCMSData.m_strHeartRate.Substring(0,objCMSData.m_strHeartRate.Length-3);
//				//
//				//				}
//				//				if(objVentilatorData != null)
//				//				{
//				//					if(objVentilatorData.m_strRespRate == null || objVentilatorData.m_strRespRate == "")
//				//						txtBreath.Text = "";
//				//					else
//				//						txtBreath.Text = objVentilatorData.m_strRespRate.Substring(0,objVentilatorData.m_strRespRate.Length-3);
//				//				}
//			}
//			catch
//			{
//			}
			#endregion Old
			//����ԭ������û���߼����Բ�����
		}


		private void m_mthStopAutoSave()
		{
			if(!cmdStartAuto.Enabled)
			{
				frmAutoResult.Visible = false;
				cmdStopAuto_Click(null,null);
			}
		}
		/// <summary>
		/// �������ֵ��߼���
		/// </summary>
		private bool m_blnCalcSuccess()
		{
			string strMorality;
			int			intBXB;	
			int			intXT;	
			int			intRST;	
			int			intTDA;	
			int			intXXB;	
			int			intXNS;	
			int			intXG;	
			int			intDMX;	
			int			intJQS;	
			int			intYTZ;	

			object[] res = m_dtlResult.Rows[0].ItemArray;

			int count = 0;
			int intAge = m_objCurrentPatient.m_ObjPeopleInfo.m_IntAge;
			try{ intBXB = int.Parse(m_txtBXB.Text);}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���,��ϸ��������������!");
				m_txtBXB.Focus();
				return false;
			}
			try{ intXT  = int.Parse(m_txtXT.Text);}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���,Ѫ�Ǳ�����������!");
				m_txtXT.Focus();
				return false;
			}

			try{intRST = int.Parse(m_txtRST.Text);}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���,��������ø������������!");
				m_txtRST.Focus();
				return false;
			}
			try{intTDA = int.Parse(m_txtTDA.Text);}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���,�춬����ת��ø������������!");
				m_txtTDA.Focus();
				return false;
			}
			try{intXXB = int.Parse(m_txtXXB.Text);}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���,Ѫϸ�������½�������������!");
				m_txtXXB.Focus();
				return false;
			}
			try{intXNS = int.Parse(m_txtXNS.Text);}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���,Ѫ���ص����߱�����������!");
				m_txtXNS.Focus();
				return false;
			}
			try{intXG  = int.Parse(m_txtXG.Text);}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���,Ѫ�Ʊ�����������!");
				m_txtXG.Focus();
				return false;
			}
			try{intDMX = int.Parse(m_txtDMX.Text);}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���,����Ѫ����ѹ������������!");
				m_txtDMX.Focus();
				return false;
			}
			try{intJQS = int.Parse(m_txtJQS.Text);}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���,��ȱʧ������������!");
				m_txtJQS.Focus();
				return false;
			}
			try{intYTZ = int.Parse(m_txtYTZ.Text);}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���, Һ������������������!");
				m_txtYTZ.Focus();
				return false;
			}

			res[0] = "������"; 
			res[1] = "������";  
			res[2] = "������"; 
			res[3] = "������"; 
			res[4] = "������"; 
			res[5] = "������"; 
			res[6] = "������";  
			res[7] = "������"; 
			res[8] = "������"; 
			res[9] = "������";
			
			if(m_intKind ==0)
			{
				if(intAge > 55) 
				{
					count++;
				}
				if(intBXB > 16)
				{
					count++;
					res[0] = "����"; 
				}
				if(intXT >220)
				{
					count++;
					res[1] = "����"; 
				}
				if(intRST >350)
				{
					count++;
					res[2] = "����"; 
				}
				if(intTDA >250)
				{
					count++;
					res[3] = "����"; 
				}
				if(intXXB >10)
				{
					count++;
					res[4] = "����"; 
				}
				if(intXNS >5)
				{
					count++;
					res[5] = "����"; 
				}
				if(intXG <8)
				{
					count++;
					res[6] = "����"; 
				}
				if(intDMX <60)
				{
					count++;
					res[7] = "����"; 
				}
				if(intJQS >4)
				{
					count++;
					res[8] = "����"; 
				}
				if(intYTZ >6)
				{
					count++;
					res[9] = "����"; 
				}

			}
			else
			{
				if(intAge > 70)
				{
					count++;
				}
				if(intBXB > 18)
				{
					count++;
					res[0] = "����"; 
				}
				if(intXT >220) 
				{
					count++;
					res[1] = "����"; 
				}
				if(intRST > 400)
				{
					count++;
					res[2] = "����"; 
				}
				if(intTDA >250)
				{
					count++;
					res[3] = "����"; 
				}
				if(intXXB >10)
				{
					count++;
					res[4] = "����"; 
				}
				if(intXNS >2)
				{
					count++;
					res[5] = "����"; 
				}
				if(intXG <8)
				{
					count++;
					res[6] = "����"; 
				}
				if(intDMX <60)
				{
					count++;
					res[7] = "����"; 
				}
				if(intJQS >5)
				{
					count++;
					res[8] = "����"; 
				}
				if(intYTZ >4)
				{
					count++;
					res[9] = "����"; 
				}
			}
			
			if(count <=2)
				strMorality ="С��1%";
			else if(count <=4)
				strMorality= "16%";
			else if(count <=6)
				strMorality = "40%";
			else 
				strMorality ="100%";

			res[10]= strMorality;

			m_dtlResult.Rows[0].ItemArray = res;
			return true;

		}


		#endregion �û��Զ��庯��

		#region �¼�������
		//FORM
		private void frmRansonEvaluation_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			frmAutoResult.Close();		
		}

		private void frmRansonEvaluation_Load(object sender, System.EventArgs e)
		{
			m_dtlResult.Columns.Add("��ϸ��");
			m_dtlResult.Columns.Add("Ѫ��");
			m_dtlResult.Columns.Add("��������ø");
			m_dtlResult.Columns.Add("�춬����ת��ø");
			m_dtlResult.Columns.Add("Ѫϸ�������½�");
			m_dtlResult.Columns.Add("Ѫ���ص�����");
			m_dtlResult.Columns.Add("Ѫ��");
			m_dtlResult.Columns.Add("����Ѫ����ѹ");
			m_dtlResult.Columns.Add("��ȱʧ");
			m_dtlResult.Columns.Add("Һ������");
			m_dtlResult.Columns.Add("������");

			m_dtgResult.DataSource = m_dtlResult;

			m_dtlResult.Rows.Add(new string[]{"/","/","/","/","/","/","/","/","/","/","/"});
			
			lbltxtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;
			
			m_objHighLight.m_mthAddControlInContainer(this);
	
			//this.dtpEvalDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
			this.dtpEvalDate.m_mthResetSize();
			
		}
		//Button									"����"
		private void cmdCalculate_Click(object sender, System.EventArgs e)
		{
			m_blnCalcSuccess();
		}
		
		//Button									"��ȡ����"
		private void cmdGetData_Click(object sender, System.EventArgs e)
		{
            if (this.m_objCurrentPatient == null)
			{
				clsPublicFunction.ShowInformationMessageBox("�����봲�ţ�");
				return;
			}
			GetData();			
		}
		
		//TextBox			"TextChanged�¼�"		"���ּ��"
		private void txtAutoTime_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				int.Parse(txtAutoTime.Text);
			}
			catch
			{
				txtAutoTime.Text = "60";
			}
		}
		
		//Timer				"Elapsed�¼�"
		private void timAutoCollect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			
			try
			{
                //int intTime				= int.Parse(txtAutoTime.Text);
                //dtpStartSample.Value	= dtpStartSample.Value.AddSeconds(intTime);
                dtpStartSample.Value = DateTime.Now;
				//�Ӻ������ͼ໤�Զ�ȡ����
				GetData();
				if(!m_blnCalcSuccess())
					return;
				
				

				object [] res			= m_dtlResult.Rows[0].ItemArray;

				//**************************************************************************************
				ListViewItem item = new ListViewItem(new string[]{
																	 dtpStartSample.Value.ToString("yyyy-MM-dd HH:mm:ss"),
																	(string)res[10],
																	 (string)res[0],
																	 (string)res[1],
																	 (string)res[2],
																	 (string)res[3],
																	 (string)res[4],
																	 (string)res[5],
																	 (string)res[6],
																	 (string)res[7],
																	(string)res[8],
																	(string)res[9],
				});
				frmAutoResult.AddResult(item);
				m_AutoSave(dtpStartSample.Value.ToString("yyyy-MM-dd HH:mm:ss"));//alex 2002-9-29
			}
			catch
			{
			}
		}

		//Button									"�Զ�����"
		private void cmdStartAuto_Click(object sender, System.EventArgs e)
		{
			if(m_ctlPatientInfo.CurrentEmrPatient == null)
			{
				clsPublicFunction.ShowInformationMessageBox("������סԺ�ţ�");
				return;
			}
			if(strAddOrModify != "0")
			{
				clsPublicFunction.ShowInformationMessageBox("����������ݣ�");
				return;
			}
			try
			{
				int intTime					= int.Parse(txtAutoTime.Text);
				timAutoCollect.Interval		= intTime*1000;
				cmdStartAuto.Enabled		= false;
				txtAutoTime.Enabled			= false;
				cmdStopAuto.Enabled			= true;
                frmAutoResult.M_lblTitle = "�Զ�����";
				frmAutoResult.Visible		= true;
				timAutoCollect.Start();
			}
			catch
			{
			}
		}

		//Button									"ֹͣ����"
		private void cmdStopAuto_Click(object sender, System.EventArgs e)
		{
			timAutoCollect.Stop();
			cmdStartAuto.Enabled		= true;
			txtAutoTime.Enabled			= true;
			cmdStopAuto.Enabled			= false;
		}

		//Button									"�鿴���" 
		private void cmdShowResult_Click(object sender, System.EventArgs e)
		{
            frmAutoResult.M_lblTitle = "�鿴���";
            frmAutoResult.Visible = true;
		}

		#endregion

		private void m_rdbFds_CheckedChanged(object sender, System.EventArgs e)
		{
			m_intKind = 0;
		}

		private void m_rdbDs_CheckedChanged(object sender, System.EventArgs e)
		{
			m_intKind = 1;
		}

		#region Print Function

		public override void m_mthSetPrint()
		{
			clsEvalInfoOfRansonEvaluation objValue;
			objPrintTool=new clsRanson_ValuationPrintTool();
			objPrintTool.m_mthInitPrintTool(null);	
			if(m_objCurrentPatient == null)
				objPrintTool.m_mthSetPrintInfo(null,null,DateTime.MinValue);
			else
			{
				if(this.trvActivityTime.SelectedNode ==null || this.trvActivityTime.SelectedNode==trvActivityTime.Nodes[0] || trvActivityTime.SelectedNode.Tag==null)
					objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient,null,DateTime.MinValue);
				else 
				{
					objDomain.m_lngGetRansonValue(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),trvActivityTime.SelectedNode.Tag.ToString(),out objValue);
					object obj = objValue;
					objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient,obj,DateTime.Parse(trvActivityTime.SelectedNode.Tag.ToString()));
				}
			}					
			objPrintTool.m_mthInitPrintContent();
		}
		
		#endregion

		private void m_cmdGetCheckData_Click(object sender, System.EventArgs e)
		{
			m_mthGetCheckInfo();
		}



	}
}

