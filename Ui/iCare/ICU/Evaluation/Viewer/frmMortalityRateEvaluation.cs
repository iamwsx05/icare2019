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
	/// MortalityRate智能评分
	/// </summary>
	public partial class frmMortalityRateEvaluation : frmValuationBaseForm,PublicFunction
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
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn7;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn8;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.Label m_lblDHS;
		private System.Windows.Forms.Label m_lblYYZ;
		private System.Windows.Forms.Label m_lblFS;
		private System.Windows.Forms.Label m_lblBDB;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboHM;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboInfect;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboCancer;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboJZ;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboSJ;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtSBP;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.Label label3;
		private PinkieControls.ButtonXP cmdStartAuto;
		private PinkieControls.ButtonXP cmdGetData;
		private PinkieControls.ButtonXP cmdShowResult;
		private PinkieControls.ButtonXP cmdStopAuto;
        private PinkieControls.ButtonXP cmdCalculate;
		private PinkieControls.ButtonXP m_cmdGetCheckData;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.ComponentModel.IContainer components = null;
		#endregion

		public frmMortalityRateEvaluation()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// 初始化控件
			m_mthInit();

			
			m_dtlResult = new DataTable("result");

			#region 自动纪录窗口的初始化
			ColumnHeader chResult1 = new ColumnHeader();
			chResult1.Text = "昏迷或深度木僵";
			chResult1.Width = 150;

			ColumnHeader chResult2 = new ColumnHeader();
			chResult2.Text = "急诊入ICU";
			chResult2.Width = 100;


			ColumnHeader chResult3 = new ColumnHeader();
			chResult3.Text = "癌症";
			chResult3.Width = 100;

			ColumnHeader chResult4 = new ColumnHeader();
			chResult4.Text = "感染";
			chResult4.Width = 100;

			ColumnHeader chResult5 = new ColumnHeader();
			chResult5.Text = "系统器官衰竭数量";
			chResult5.Width = 150;
			
			ColumnHeader chResult7 = new ColumnHeader();
			chResult7.Text = "SBP";
			chResult7.Width = 100;

			ColumnHeader chResult8 = new ColumnHeader();
			chResult8.Text = "SBP2";
			chResult8.Width = 100;

			ColumnHeader chResult9 = new ColumnHeader();
			chResult9.Text = "死亡率";
			chResult9.Width = 100;

			frmAutoResult = new frmAutoEvalResult(chResult9,chResult1,chResult2,chResult3,chResult4,chResult5,chResult7,chResult8);
			frmAutoResult.Text = "MortalityRate自动评分";
			frmAutoResult.Owner = this;
			frmAutoResult.Visible = false;
			#endregion            
            
			objDomain = new clsMortalityRateEvaluationDomain();
			strAddOrModify = "0";

			m_objBorderTool = new com.digitalwave.Utility.Controls.clsBorderTool(Color.White);
			m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{
																			 trvActivityTime,m_dtgResult
																		 });
			//设置热键
			m_mthSetQuickKeys();

			m_objHighLight = new ctlHighLightFocus(clsHRPColor.s_ClrHightLight);
		}

		

		// 用户自定义变量*********************************************************************************

		#region 用户自定义变量

		protected ctlHighLightFocus m_objHighLight;

		private DataTable m_dtlResult;
		private com.digitalwave.Utility.Controls.clsBorderTool m_objBorderTool;
		private frmAutoEvalResult frmAutoResult;
		private string strAddOrModify;
		private clsEvalInfoOfMortalityRateEvaluation m_objMortalityRateEvaluationDB;//用在m_mthDisplay
		
		private clsMortalityRateEvaluationDomain objDomain;//MortalityRate
		
		private clsSystemContext m_objCurrentContext
		{
			get
			{
				return clsSystemContext.s_ObjCurrentContext;
			}
		}

		//暂时没有用到的变量
		private const string _strTypeMonitor = "Monitoring";
		private const string _strTypeVentilator = "Ventilator";
		#endregion//用户自定义变量


		// PublicFunction接口的实现***********************************************************************
		// 由MDIParent调用，以实现子窗口相关的各种操作

		#region Interface PublicFunction 的函数实现
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
			if(m_strInPatientID == null || m_strInPatientID == "")
			{
				clsPublicFunction.ShowInformationMessageBox("请输入病人住院号!");
				return;
			}
			if(m_strCreateDate == null || m_strCreateDate == "")
			{
				clsPublicFunction.ShowInformationMessageBox("请在列表中选择相应的评分时间！");
				return;
			}

			if(!clsPublicFunction.s_blnAskForDelete())
				return ;

			long lngRes = objDomain.m_lngDeactive(clsBaseInfo.LoginEmployee.m_strEMPID_CHR, m_strInPatientID, m_strInPatientDate, m_strCreateDate);
			
			if(lngRes <= 0)
			{
				clsPublicFunction.ShowInformationMessageBox("删除失败，请重新操作!");
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
			ClearUp();//继承自frmValuationBaseForm
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


		
		//用户自定义函数*********************************************************************************
		#region 用户自定义函数

		/// <summary>
		/// 初始化控件
		/// </summary>
		private void m_mthInit()
		{			
//			//DropDownList增加选项
			m_cboHM.AddRangeItems( new string[] {"无", "是"});
			m_cboJZ.AddRangeItems( new string[] {"无", "是"});
			m_cboCancer.AddRangeItems( new string[] {"无", "是"});
			m_cboInfect.AddRangeItems( new string[] {"无", "是"});
			m_cboSJ.AddRangeItems( new string[] {"1","2","3","4","5","6","7"});

		}

		/// <summary>
		/// 保存数据库     需要用到m_lngSaveWithoutMessageBox()
		/// </summary>
		/// <returns></returns>
		private long m_lngSaveWithMessageBox()
		{
			long lngRes=m_lngSaveWithoutMessageBox();
			if(lngRes==-11)
			{
				clsPublicFunction.ShowInformationMessageBox("你所修改的记录已被他人删除或不存在！");				
			}
			else if(lngRes==-21)
			{
				clsPublicFunction.ShowInformationMessageBox("对不起，保存失败！");
			}
			else if(lngRes==-31)
			{
				clsPublicFunction.ShowInformationMessageBox("对不起，本记录已被他人修改，请重新读取一次！");
			}
			return lngRes;
		}

		/// <summary>
		/// 保存数据库     需要获得存取数据库的权限
		/// </summary>
		/// <returns></returns>
		private long m_lngSaveWithoutMessageBox()
		{
			if(m_strInPatientID == null || m_strInPatientID == "")
			{
				clsPublicFunction.ShowInformationMessageBox("对不起，请输入病人住院编号！");
				return 0;
			}

			string strCurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			
			//赋值
			this.cmdCalculate_Click(null,null);
			clsEvalInfoOfMortalityRateEvaluation objEvalInfo = m_GetCurrentEvalInfo();

			if(m_strCreateDate != "")
			{
                //if(m_objCurrentContext.m_ObjControl.m_enmModifyCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
                //    == enmDBControlCheckResult.Disable)
                //{
                //    clsPublicFunction.s_mthShowNotPermitMessage();
                //    return 0;
                //}
				clsEvalInfoOfMortalityRateEvaluation objTemp;
				long lngExist = objDomain.m_lngGetMortalityRateValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out objTemp);

				if(lngExist == 0)
					return -11;
				
				if(lngExist == 1)
				{
                    //if(DateTime.Parse(objTemp.strModifyDate) != DateTime.Parse(m_objMortalityRateEvaluationDB.strModifyDate))
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

				clsEvalInfoOfMortalityRateEvaluation objTemp;
				long lngExist = objDomain.m_lngGetMortalityRateValue(m_strInPatientID,m_strInPatientDate,dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),out objTemp);
				
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
		/// 从WinForm里面读取各种需要的数据
		/// </summary>
		/// <returns></returns>
		private clsEvalInfoOfMortalityRateEvaluation m_GetCurrentEvalInfo()
		{
			string str = "$$";
			clsEvalInfoOfMortalityRateEvaluation objEvalInfo = new clsEvalInfoOfMortalityRateEvaluation();

			objEvalInfo.strPatientID		= m_strInPatientID;
			objEvalInfo.strInPatientDate	= m_strInPatientDate;
			objEvalInfo.strActivityTime		= (m_strCreateDate == "") ? this.dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : m_strCreateDate;

			objEvalInfo.strEvalDoctorID		= clsBaseInfo.LoginEmployee.m_strEMPID_CHR;
			objEvalInfo.strEvalDoctorName	= clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;

			objEvalInfo.strHM				= str + m_dtlResult.Rows[0].ItemArray[0].ToString();
			objEvalInfo.strJZ				= str + m_dtlResult.Rows[0].ItemArray[1].ToString();
			objEvalInfo.strCancer			= str + m_dtlResult.Rows[0].ItemArray[2].ToString();
			objEvalInfo.strInfect			= str + m_dtlResult.Rows[0].ItemArray[3].ToString();
			objEvalInfo.strSJ				= str + m_dtlResult.Rows[0].ItemArray[4].ToString();
			objEvalInfo.strSBP				= str + m_dtlResult.Rows[0].ItemArray[5].ToString();
			objEvalInfo.strSBP2				= str + m_dtlResult.Rows[0].ItemArray[6].ToString();


			objEvalInfo.strMortality		= str + m_dtlResult.Rows[0].ItemArray[7].ToString();
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
		/// 设置快捷键
		/// </summary>
		private void m_mthSetQuickKeys()
		{			
			m_mthSetControlEvent(this);			
		}
		
		private void m_mthSetControlEvent(Control p_ctlControl)
		{
			#region 利用递归调用，读取并设置所有界面事件	
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
			{//F1 112  帮助, F2 113 Save，F3  114 Del，F4 115 Print，F5 116 Refresh，F6 117 Search
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
		/// 清除各个输入的值
		/// </summary>
		protected override void ClearUp()
		{
			try
			{
				object [] res = {"/","/","/","/","/","/","/","/"};
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
                lbltxtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR ;


				m_cboCancer.SelectedIndex = -1;
				m_cboHM.SelectedIndex		= -1;
				m_cboInfect.SelectedIndex	= -1;
				m_cboJZ.SelectedIndex	= -1;
				m_cboSJ.SelectedIndex	= -1;

				m_txtSBP.Text = "";

				dtpEvalDate.Value = DateTime.Now;
				strAddOrModify = "0";//设置为可增加状态

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
		/// TIMEPICKER的事件处理函数,这个TIMEPICKER继承自父类
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
		/// 负责显示从TreeView里选择的数据
		/// </summary>
		protected override void m_mthDisplay()
		{
			long lngRes  = objDomain.m_lngGetMortalityRateValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out m_objMortalityRateEvaluationDB);

			if(m_objMortalityRateEvaluationDB == null)
				return;

			lbltxtEvalDoctor.Text = m_objMortalityRateEvaluationDB.strEvalDoctorName;

			
			m_cboHM.SelectedIndex		= int.Parse(m_objMortalityRateEvaluationDB.strHM);
			m_cboJZ.SelectedIndex		= int.Parse(m_objMortalityRateEvaluationDB.strJZ);
			m_cboCancer.SelectedIndex	= int.Parse(m_objMortalityRateEvaluationDB.strCancer);
			m_cboInfect.SelectedIndex	= int.Parse(m_objMortalityRateEvaluationDB.strInfect);
			m_cboSJ.SelectedIndex		= int.Parse(m_objMortalityRateEvaluationDB.strSJ)-1;
			m_txtSBP.Text				= m_objMortalityRateEvaluationDB.strSBP;
			m_blnCalcSuccess();

				
			dtpEvalDate.Value = DateTime.Parse(m_objMortalityRateEvaluationDB.strActivityTime);
            clsEmrEmployeeBase_VO objEmployee = null;
            clsBaseInfo.m_lngGetEmpByID(m_objMortalityRateEvaluationDB.strEvalDoctorID, out objEmployee);
			
			lbltxtEvalDoctor.Text = objEmployee.m_strLASTNAME_VCHR;

			strAddOrModify = "1";
		}

		/// <summary>
		/// 在自动评分时自动保存数据
		/// </summary>
		public void m_AutoSave(string m_strAutoEvalDate)
		{
			try
			{
				this.cmdCalculate_Click(null,null);
				if(strAddOrModify == "0")
				{
					clsEvalInfoOfMortalityRateEvaluation objEvalInfo		= m_GetCurrentEvalInfo();
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
		/// 从呼吸机和监护仪读取数据
		/// </summary>
		private void GetData()
		{
			#region Old
//			try
//			{
//				XmlDocument			objXMLDoc	= new XmlDocument();
//
//				clsCMSData			objCMSData;				//监护仪
//				clsVentilatorData	objVentilatorData;		//呼吸机
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
			//由于此处原来没有逻辑现在暂不处理.
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
		/// 计算评分的逻辑。
		/// </summary>
		private bool m_blnCalcSuccess()
		{
			
			object[] res = m_dtlResult.Rows[0].ItemArray;
            
			int intCancer, intHM, intInfect, intJZ, intSJ;
			int intSBP;

			if(m_cboHM.SelectedIndex == -1)
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,“昏迷或深度木僵”必须选择!");
				m_cboHM.Focus();
				return false;

			}
			if(m_cboJZ.SelectedIndex == -1)
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,“急症入ICU”必须选择!");
				m_cboJZ.Focus();
				return false;

			}
			if(m_cboCancer.SelectedIndex == -1)
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,“癌症”必须选择!");
				m_cboCancer.Focus();
				return false;
			}
			if(m_cboInfect.SelectedIndex == -1)
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,“感染”必须选择!");
				m_cboInfect.Focus();
				return false;

			}
			if(m_cboSJ.SelectedIndex == -1)
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,“器官系统衰竭数量”必须选择!");
				m_cboSJ.Focus();
				return false;

			}

			intCancer = m_cboCancer.SelectedIndex;
			intSJ		= m_cboSJ.SelectedIndex+1;
			intInfect	= m_cboInfect.SelectedIndex;
			intJZ		= m_cboJZ.SelectedIndex;
			intHM		= m_cboHM.SelectedIndex;
			try
			{
				intSBP = int.Parse(m_txtSBP.Text);			
			}
			catch{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,SBP必须输入数字!");
				m_txtSBP.Focus();
				return false;
			}


			int intAge = m_objCurrentPatient.m_ObjPeopleInfo.m_IntAge;
			//int intAge = 50;
			double dblSum = intHM*2.630 +intJZ*1.630 + intCancer*1.490 +intInfect*0.677+intSJ*0.595 + intAge*0.038 - intSBP*0.048 +intSBP*intSBP *0.000131 -3;
			dblSum = Math.Exp(dblSum);
			dblSum = 1-(1/(1+dblSum));
			dblSum = Math.Round(dblSum,2)*100;
			string strSum = dblSum.ToString();


			res[0] = intHM;
			res[1] = intJZ;
			res[2] = intCancer;
			res[3] = intInfect;
			res[4] = intSJ;
			res[5] = intSBP;
			res[6] = intSBP*intSBP;
			res[7] = strSum;

			m_dtlResult.Rows[0].ItemArray = res;
			return true;

		}


		#endregion 用户自定义函数

		#region 事件处理函数
		//FORM
		private void frmMortalityRateEvaluation_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			frmAutoResult.Close();		
		}

		private void frmMortalityRateEvaluation_Load(object sender, System.EventArgs e)
		{
			m_dtlResult.Columns.Add("昏迷或深度木僵");
			m_dtlResult.Columns.Add("急诊入ICU");
			m_dtlResult.Columns.Add("癌症");
			m_dtlResult.Columns.Add("是否感染");
			m_dtlResult.Columns.Add("器官系统衰竭数量");
			m_dtlResult.Columns.Add("SBP");
			m_dtlResult.Columns.Add("SBP2");

			m_dtlResult.Columns.Add("死亡率");
			m_dtgResult.DataSource = m_dtlResult;

			m_dtlResult.Rows.Add(new string[]{"/","/","/","/","/","/","/","/"});			
			lbltxtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;
			
			m_objHighLight.m_mthAddControlInContainer(this);
	
			//this.dtpEvalDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
			this.dtpEvalDate.m_mthResetSize();
			
		}
		//Button									"评分"
		private void cmdCalculate_Click(object sender, System.EventArgs e)
		{
			m_blnCalcSuccess();
		}
		
		//Button									"获取数据"
		private void cmdGetData_Click(object sender, System.EventArgs e)
		{
            if (this.m_objCurrentPatient == null)
			{
				clsPublicFunction.ShowInformationMessageBox("请输入床号！");
				return;
			}
			GetData();			
		}
		
		//TextBox			"TextChanged事件"		"评分间隔"
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
		
		//Timer				"Elapsed事件"
		private void timAutoCollect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			
			try
			{
                //int intTime				= int.Parse(txtAutoTime.Text);
                //dtpStartSample.Value	= dtpStartSample.Value.AddSeconds(intTime);
                dtpStartSample.Value = DateTime.Now;
				//从呼吸机和监护以读取数据
				GetData();
				if(!m_blnCalcSuccess())
					return;
				
				

				object [] res			= m_dtlResult.Rows[0].ItemArray;

				//**************************************************************************************
				ListViewItem item = new ListViewItem(new string[]{
																	 dtpStartSample.Value.ToString("yyyy-MM-dd HH:mm:ss"),
																	 (string)res[7],
																	 (string)res[0]+" 测定值：",
																	 (string)res[1]+" 测定值：",
																	 (string)res[2]+" 测定值：",
																	 (string)res[3]+" 测定值：",
																	 (string)res[4],
																	 (string)res[5],
																	 (string)res[6],
				});
				frmAutoResult.AddResult(item);
				m_AutoSave(dtpStartSample.Value.ToString("yyyy-MM-dd HH:mm:ss"));//alex 2002-9-29
			}
			catch
			{
			}
		}

		//Button									"自动评分"
		private void cmdStartAuto_Click(object sender, System.EventArgs e)
		{
			if(m_ctlPatientInfo.CurrentEmrPatient == null)
			{
				clsPublicFunction.ShowInformationMessageBox("请输入住院号！");
				return;
			}
			if(strAddOrModify != "0")
			{
				clsPublicFunction.ShowInformationMessageBox("请先清空数据！");
				return;
			}
			try
			{
				int intTime					= int.Parse(txtAutoTime.Text);
				timAutoCollect.Interval		= intTime*1000;
				cmdStartAuto.Enabled		= false;
				txtAutoTime.Enabled			= false;
				cmdStopAuto.Enabled			= true;
                frmAutoResult.M_lblTitle = "自动评分";
				frmAutoResult.Visible		= true;
				timAutoCollect.Start();
			}
			catch
			{
			}
		}

		//Button									"停止评分"
		private void cmdStopAuto_Click(object sender, System.EventArgs e)
		{
			timAutoCollect.Stop();
			cmdStartAuto.Enabled		= true;
			txtAutoTime.Enabled			= true;
			cmdStopAuto.Enabled			= false;
		}

		//Button									"查看结果" 
		private void cmdShowResult_Click(object sender, System.EventArgs e)
		{
            frmAutoResult.M_lblTitle = "查看结果";
            frmAutoResult.Visible = true;
		}

		#endregion

		#region Print Function

		public override void m_mthSetPrint()
		{
			clsEvalInfoOfMortalityRateEvaluation objValue;
			objPrintTool=new clsMortalityRate_ValuationPrintTool();
			objPrintTool.m_mthInitPrintTool(null);	
			if(m_objCurrentPatient == null)
				objPrintTool.m_mthSetPrintInfo(null,null,DateTime.MinValue);
			else
			{
				if(this.trvActivityTime.SelectedNode ==null || this.trvActivityTime.SelectedNode==trvActivityTime.Nodes[0] || trvActivityTime.SelectedNode.Tag==null)
					objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient,null,DateTime.MinValue);
				else 
				{
					objDomain.m_lngGetMortalityRateValue(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),trvActivityTime.SelectedNode.Tag.ToString(),out objValue);
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

		private void lsvCardNo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}



	}
}

