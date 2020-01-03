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
//using iCare.ICU.Espial;
//using iCare.Common;
using com.digitalwave.Emr.Signature_gui;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// CT��������
	/// </summary>
    public partial class frmCTEvaluation : frmValuationBaseForm, PublicFunction
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
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn8;
		private System.Windows.Forms.MainMenu mainMenu1;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboDHS;
		private System.Windows.Forms.Label m_lblDHS;
		private System.Windows.Forms.Label m_lblBDB;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboBDB;
		private System.Windows.Forms.Label m_lblFS;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboFS;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboYYZ;
		private System.Windows.Forms.Label m_lblYYZ;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboSJX;
		private System.Windows.Forms.Label m_lblSJX;
		private System.Windows.Forms.GroupBox groupBox1;
		private PinkieControls.ButtonXP cmdStartAuto;
		private PinkieControls.ButtonXP cmdStopAuto;
		private PinkieControls.ButtonXP cmdShowResult;
		private PinkieControls.ButtonXP cmdGetData;
        private PinkieControls.ButtonXP cmdCalculate;
		private System.ComponentModel.IContainer components = null;
		#endregion

		public frmCTEvaluation()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// ��ʼ���ؼ�
			m_mthInit();

			
			m_dtlResult = new DataTable("result");

			#region �Զ���¼���ڵĳ�ʼ��
			ColumnHeader chResult1 = new ColumnHeader();
			chResult1.Text = "������";
			chResult1.Width = 100;

			ColumnHeader chResult2 = new ColumnHeader();
			chResult2.Text = "�׵���";
			chResult2.Width = 100;


			ColumnHeader chResult3 = new ColumnHeader();
			chResult3.Text = "��ˮ";
			chResult3.Width = 100;

			ColumnHeader chResult4 = new ColumnHeader();
			chResult4.Text = "��ϵͳ�ϰ�";
			chResult4.Width = 180;

			ColumnHeader chResult5 = new ColumnHeader();
			chResult5.Text = "Ӫ��״��";
			chResult5.Width = 100;
			
			ColumnHeader chResult6 = new ColumnHeader();
			chResult6.Text = "�ܷ�";
			chResult6.Width = 100;


			frmAutoResult = new frmAutoEvalResult(chResult6,chResult1,chResult2,chResult3,chResult4,chResult5);
			frmAutoResult.Text = "CT�Զ�����";
			frmAutoResult.Owner = this;
			frmAutoResult.Visible = false;
			#endregion

            
			objDomain = new clsCTEvaluationDomain();
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
		private clsEvalInfoOfCTEvaluation							m_objCTEvaluationDB;//����m_mthDisplay
		
		private clsCTEvaluationDomain								objDomain;//CT
		
		private clsSystemContext									m_objCurrentContext
		{
			get
			{
				return clsSystemContext.s_ObjCurrentContext;
			}
		}

		private string  m_strScore;
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
			//DropDownList����ѡ��
			m_cboDHS.AddRangeItems( new string[] {"<2.0","2.0 - 3.0", ">3.0"});
			m_cboBDB.AddRangeItems( new string[] {">35","30 - 35","<30"});
			m_cboFS.AddRangeItems( new string[] {"��","���׿���","���ѿ���"});
			m_cboYYZ.AddRangeItems( new string[] {"����","�Ϻ�","�ϲ� - ����"});
			m_cboSJX.AddRangeItems( new string[] {"��","����","��Ȼ���"});
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
			//this.cmdCalculate_Click(null,null);
            if (!m_blnCalcSuccess())
                return 0;
			clsEvalInfoOfCTEvaluation objEvalInfo = m_GetCurrentEvalInfo();

			if(m_strCreateDate != "")
			{
                //if(m_objCurrentContext.m_ObjControl.m_enmModifyCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
                //    == enmDBControlCheckResult.Disable)
                //{
                //    clsPublicFunction.s_mthShowNotPermitMessage();
                //    return 0;
                //}
				clsEvalInfoOfCTEvaluation objTemp;
				long lngExist = objDomain.m_lngGetCTValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out objTemp);

				if(lngExist == 0)
					return -11;
				
				if(lngExist == 1)
				{
                    //if(DateTime.Parse(objTemp.strModifyDate) != DateTime.Parse(m_objCTEvaluationDB.strModifyDate))
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

				clsEvalInfoOfCTEvaluation objTemp;
				long lngExist = objDomain.m_lngGetCTValue(m_strInPatientID,m_strInPatientDate,dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),out objTemp);
				
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
		private clsEvalInfoOfCTEvaluation m_GetCurrentEvalInfo()
		{
			clsEvalInfoOfCTEvaluation objEvalInfo = new clsEvalInfoOfCTEvaluation();

			objEvalInfo.strPatientID		= m_strInPatientID;
			objEvalInfo.strInPatientDate	= m_strInPatientDate;
			objEvalInfo.strActivityTime		= (m_strCreateDate == "") ? this.dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : m_strCreateDate;

			objEvalInfo.strEvalDoctorID		= clsBaseInfo.LoginEmployee.m_strEMPID_CHR;
			objEvalInfo.strEvalDoctorName	= clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;

			objEvalInfo.strDHS				= m_cboDHS.SelectedIndex.ToString();
			objEvalInfo.strBDB				= m_cboBDB.SelectedIndex.ToString();
			objEvalInfo.strFS				= m_cboFS.SelectedIndex.ToString();
			objEvalInfo.strSJX				= m_cboSJX.SelectedIndex.ToString();
			objEvalInfo.strYYZ				= m_cboYYZ.SelectedIndex.ToString();
			objEvalInfo.strScore			= m_strScore;


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
					//this.txtCardNo.Focus();
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
				object [] res = {"/","/","/","/","/","/"};
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

				m_cboDHS.SelectedIndex		= -1;
				m_cboBDB.SelectedIndex		= -1;
				m_cboFS.SelectedIndex		= -1;
				m_cboSJX.SelectedIndex		= -1;
				m_cboYYZ.SelectedIndex		= -1;


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
			long lngRes  = objDomain.m_lngGetCTValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out m_objCTEvaluationDB);

			if(m_objCTEvaluationDB == null)
				return;

			//lbltxtEvalDoctor.Text = m_objCTEvaluationDB.strEvalDoctorName;



			m_cboDHS.SelectedIndex		= int.Parse(m_objCTEvaluationDB.strDHS);	 
			m_cboBDB.SelectedIndex		= int.Parse(m_objCTEvaluationDB.strBDB);
			m_cboFS.SelectedIndex		= int.Parse(m_objCTEvaluationDB.strFS);
			m_cboSJX.SelectedIndex		= int.Parse(m_objCTEvaluationDB.strSJX);
			m_cboYYZ.SelectedIndex		= int.Parse(m_objCTEvaluationDB.strYYZ);
			m_blnCalcSuccess();

				
			dtpEvalDate.Value = DateTime.Parse(m_objCTEvaluationDB.strActivityTime);
            clsEmrEmployeeBase_VO objEmployee = null;
            clsBaseInfo.m_lngGetEmpByID(m_objCTEvaluationDB.strEvalDoctorID, out objEmployee);
			
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
					clsEvalInfoOfCTEvaluation objEvalInfo		= m_GetCurrentEvalInfo();
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
			try
			{
				XmlDocument			objXMLDoc	= new XmlDocument();

				clsCMSData			objCMSData;				//�໤��
				clsVentilatorData	objVentilatorData;		//������

				m_mthGetICUDataByTime(dtpStartSample.Value,out objCMSData,out objVentilatorData);
			}
			catch
			{
			}
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
			
			object[] res = m_dtlResult.Rows[0].ItemArray;
			if(m_cboDHS.SelectedIndex == -1) 
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���,�������ء�����ѡ��!");
				m_cboDHS.Focus();
				return false;
			}


			if(m_cboBDB.SelectedIndex == -1) 
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���,���׵��ס�����ѡ��!");
				m_cboBDB.Focus();
				return false;
			}

			if(m_cboFS.SelectedIndex == -1) 
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���,����ˮ������ѡ��!");
				m_cboFS.Focus();
				return false;
			}

			if(m_cboSJX.SelectedIndex == -1) 
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���,����ϵͳ�ϰ�������ѡ��!");
				m_cboSJX.Focus();
				return false;
			}

			if(m_cboYYZ.SelectedIndex == -1) 
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("�Բ���,��Ӫ��״��������ѡ��!");
				m_cboYYZ.Focus();
				return false;
			}
			if(m_cboDHS.SelectedIndex == 0 && m_cboBDB.SelectedIndex == 0 && m_cboFS.SelectedIndex == 0 && m_cboSJX.SelectedIndex == 0 && m_cboYYZ.SelectedIndex == 0)
				m_strScore = "A";
			else if(m_cboDHS.SelectedIndex == 1 && m_cboBDB.SelectedIndex == 1 && m_cboFS.SelectedIndex == 1 && m_cboSJX.SelectedIndex == 1 && m_cboYYZ.SelectedIndex == 1)
				m_strScore = "B";
			else if(m_cboDHS.SelectedIndex == 2 && m_cboBDB.SelectedIndex == 2 && m_cboFS.SelectedIndex == 2 && m_cboSJX.SelectedIndex == 2 && m_cboYYZ.SelectedIndex == 2)
				m_strScore = "C";
			else
			{
				clsPublicFunction.ShowInformationMessageBox("�Բ���,���������ݲ�����A��B��C��������������!");
				return false;
			}
			

			res[0] = m_cboDHS.Text;
			res[1] = m_cboBDB.Text;
			res[2] = m_cboFS.Text;
			res[3] = m_cboSJX.Text;
			res[4] = m_cboYYZ.Text;
			res[5] = m_strScore;


			m_dtlResult.Rows[0].ItemArray = res;
			return true;

		}


		#endregion �û��Զ��庯��

		#region �¼�������
		//FORM
		private void frmCTEvaluation_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			frmAutoResult.Close();		
		}

		private void frmCTEvaluation_Load(object sender, System.EventArgs e)
		{
			m_dtlResult.Columns.Add("������");
			m_dtlResult.Columns.Add("�׵���");
			m_dtlResult.Columns.Add("��ˮ");
			m_dtlResult.Columns.Add("��ϵͳ�ϰ�");
			m_dtlResult.Columns.Add("Ӫ��״��");
			m_dtlResult.Columns.Add("�ܷ�");

			m_dtgResult.DataSource = m_dtlResult;

			m_dtlResult.Rows.Add(new string[]{"/","/","/","/","/","/"});
			
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
			if(m_ctlAreaPatientSelection.CurrentBed == null)
			{
				clsPublicFunction.ShowInformationMessageBox("�����봲�ţ�");
				return;
			}
			//GetData();			
            m_mthGetCheckInfo();

            if (m_hasControlByChedkedResult != null)
            {
                float fR = 0;
                #region ��������Ŀ�ж�
                try
                {
                    fR = float.Parse(m_hasControlByChedkedResult[m_cboDHS.Name].ToString());
                }
                catch
                {
                    fR = -1;
                }

                if (fR == -1)
                {
                    m_cboDHS.SelectedIndex = -1;
                }
                else if (fR < 2.0)
                {
                    m_cboDHS.SelectedIndex = 0;
                }
                else if (fR >= 2.0 && fR <= 3.0)
                {
                    m_cboDHS.SelectedIndex = 1;
                }
                else if (fR > 3.0)
                {
                    m_cboDHS.SelectedIndex = 2;
                }
                #endregion

                #region �׵�����Ŀ�ж�
                try
                {
                    fR = float.Parse(m_hasControlByChedkedResult[m_cboBDB.Name].ToString());
                }
                catch
                {
                    fR = -1;
                }

                if (fR == -1)
                {
                    m_cboBDB.SelectedIndex = -1;
                }
                else if (fR > 35)
                {
                    m_cboBDB.SelectedIndex = 0;
                }
                else if (fR >= 30 && fR <= 35)
                {
                    m_cboBDB.SelectedIndex = 1;
                }
                else if (fR < 30)
                {
                    m_cboBDB.SelectedIndex = 2;
                }
                #endregion

            }
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
																	 (string)res[5],
																	 (string)res[0]+" �ⶨֵ��",
																	 (string)res[1]+" �ⶨֵ��",
																	 (string)res[2]+" �ⶨֵ��",
																	 (string)res[3]+" �ⶨֵ��",
																	 (string)res[4],
																	
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

		private void m_cboFS_Load(object sender, System.EventArgs e)
		{
		
		}

		private void m_cboBDB_Load(object sender, System.EventArgs e)
		{
		
		}

		private void m_cboDHS_Load(object sender, System.EventArgs e)
		{
		
		}

		#region Print Function

		public override void m_mthSetPrint()
		{
			clsEvalInfoOfCTEvaluation objValue;
			objPrintTool=new clsCT_ValuationPrintTool();
			objPrintTool.m_mthInitPrintTool(null);	
			if(m_objCurrentPatient == null)
				objPrintTool.m_mthSetPrintInfo(null,null,DateTime.MinValue);
			else
			{
				if(this.trvActivityTime.SelectedNode ==null || this.trvActivityTime.SelectedNode==trvActivityTime.Nodes[0] || trvActivityTime.SelectedNode.Tag==null)
					objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient,null,DateTime.MinValue);
				else 
				{
					objDomain.m_lngGetCTValue(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),trvActivityTime.SelectedNode.Tag.ToString(),out objValue);
					object obj = objValue;
					objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient,obj,DateTime.Parse(trvActivityTime.SelectedNode.Tag.ToString()));
				}
			}					
			objPrintTool.m_mthInitPrintContent();
		}
		
		#endregion

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
		}


	}
}

