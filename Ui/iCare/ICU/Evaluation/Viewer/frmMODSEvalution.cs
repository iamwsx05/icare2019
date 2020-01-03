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
	/// MODS智能评分
	/// </summary>
	public partial class frmMODSEvalution :  frmValuationBaseForm,PublicFunction
	{
		#region Define
		protected com.digitalwave.Utility.Controls.ctlComboBox m_cboOpenEyes;
		protected com.digitalwave.Utility.Controls.ctlComboBox m_cboSay;
		protected com.digitalwave.Utility.Controls.ctlComboBox m_cboSport;
		private System.Windows.Forms.Label m_lblOpenEyes;
		private System.Windows.Forms.Label m_lblSay;
		private System.Windows.Forms.Label m_lblSport;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox m_gpbBreathSystem;
		private System.Windows.Forms.GroupBox m_gpbKidney;
		private System.Windows.Forms.GroupBox m_gpbLiver;
		private System.Windows.Forms.GroupBox m_gpbNerve;
		private System.Windows.Forms.GroupBox m_gpbHeartSystem;
		private System.Windows.Forms.GroupBox m_gpbBloodSystem;
		private System.Windows.Forms.Label lblTitle31;
		private System.Windows.Forms.Label lblEvalDate;
		private System.Windows.Forms.Label lbltxtEvalDoctor;
		private com.digitalwave.Utility.Controls.ctlTimePicker dtpEvalDate;
		private System.Windows.Forms.DataGrid m_dtgResult;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtXXB;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtXJG;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtDHS;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtPJDMY;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtYFY;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtHR;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtPa02;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtFi02;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn7;
		private System.Windows.Forms.GroupBox gpbEvaluation;
		private com.digitalwave.Utility.Controls.ctlTimePicker dtpStartSample;
		private System.Windows.Forms.Label lblTitle96;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtAutoTime;
		private System.Windows.Forms.Label label10;
		private System.Timers.Timer timAutoCollect;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private PinkieControls.ButtonXP cmdStartAuto;
		private PinkieControls.ButtonXP cmdStopAuto;
		private PinkieControls.ButtonXP cmdShowResult;
		private PinkieControls.ButtonXP cmdGetData;
        private PinkieControls.ButtonXP cmdCalculate;
		private PinkieControls.ButtonXP m_cmdGetCheckData;
        private System.Windows.Forms.GroupBox groupBox1;
		private System.ComponentModel.IContainer components = null;

		#endregion

		public frmMODSEvalution()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// 初始化控件
			m_mthInit();

			
			m_dtlResult = new DataTable("result");

			#region 自动纪录窗口的初始化
			ColumnHeader chResult1 = new ColumnHeader();
			chResult1.Text = "呼吸系统";
			chResult1.Width = 100;

			ColumnHeader chResult2 = new ColumnHeader();
			chResult2.Text = "肾脏";
			chResult2.Width = 100;

			ColumnHeader chResult3 = new ColumnHeader();
			chResult3.Text = "肝脏";
			chResult3.Width = 100;

			ColumnHeader chResult4 = new ColumnHeader();
			chResult4.Text = "心血管系统";
			chResult4.Width = 100;

			ColumnHeader chResult5 = new ColumnHeader();
			chResult5.Text = "血液系统";
			chResult5.Width = 100;

			ColumnHeader chResult6 = new ColumnHeader();
			chResult6.Text = "神经系统";
			chResult6.Width = 100;
			ColumnHeader chResult7 = new ColumnHeader();
			chResult7.Text = "总分";
			chResult7.Width = 100;

			frmAutoResult = new frmAutoEvalResult(chResult7,chResult1,chResult2,chResult3,chResult4,chResult5,chResult6);
			frmAutoResult.Text = "MODS自动评分";
			frmAutoResult.Owner = this;
			frmAutoResult.Visible = false;
			#endregion

            
			objDomain = new clsMODSEvaluationDomain();
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

		protected ctlHighLightFocus									m_objHighLight;

		private DataTable											m_dtlResult;
		private com.digitalwave.Utility.Controls.clsBorderTool		m_objBorderTool;
		private frmAutoEvalResult										frmAutoResult;
		private string												strAddOrModify;
		private clsEvalInfoOfMODSEvaluation							m_objMODSEvaluationDB;//用在m_mthDisplay
		
		private clsMODSEvaluationDomain								objDomain;//MODS
		
		private clsSystemContext									m_objCurrentContext
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
            //if(m_objCurrentContext.m_ObjControl.m_enmDeleteCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
            //    == enmDBControlCheckResult.Disable)
            //{
            //    clsPublicFunction.s_mthShowNotPermitMessage();
            //    return;
            //}

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

		
		//事件处理函数***********************************************************************************

		#region 事件处理函数
		//FORM
		private void frmMODSEvalution_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			frmAutoResult.Close();		
		}

		private void frmMODSEvalution_Load(object sender, System.EventArgs e)
		{
			m_dtlResult.Columns.Add("呼吸系统");
			m_dtlResult.Columns.Add("肾脏");
			m_dtlResult.Columns.Add("肝脏");
			m_dtlResult.Columns.Add("心血管系统");
			m_dtlResult.Columns.Add("血液系统");
			m_dtlResult.Columns.Add("神经系统");
			m_dtlResult.Columns.Add("总分");

			m_dtgResult.DataSource = m_dtlResult;

			m_dtlResult.Rows.Add(new string[]{"/","/","/","/","/","/","/"});
			
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
			if(m_objCurrentPatient == null)
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
																	 (string)res[6],
																	 (string)res[0]+" 测定值：",
																	 (string)res[1]+" 测定值：",
																	 (string)res[2]+" 测定值：",
																	 (string)res[3]+" 测定值：",
																	 (string)res[4],
																	 (string)res[5],}
																	 );
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


		//用户自定义函数*********************************************************************************
		#region 用户自定义函数

		/// <summary>
		/// 初始化控件
		/// </summary>
		private void m_mthInit()
		{			
			//DropDownList增加选项
			m_cboOpenEyes.AddRangeItems( new string[] {"无","痛刺激","呼唤睁眼","自发"});
			m_cboSay.AddRangeItems( new string[] {"无","不理解","不适当","回答混乱","定向正确"});
			m_cboSport.AddRangeItems( new string[] {"无","痛刺伸直","痛刺屈曲","屈曲回避","定位疼痛","服从命令"});
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
			clsEvalInfoOfMODSEvaluation objEvalInfo = m_GetCurrentEvalInfo();

			if(m_strCreateDate != "")
			{
                //if(m_objCurrentContext.m_ObjControl.m_enmModifyCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
                //    == enmDBControlCheckResult.Disable)
                //{
                //    clsPublicFunction.s_mthShowNotPermitMessage();
                //    return 0;
                //}
				clsEvalInfoOfMODSEvaluation objTemp;
				long lngExist = objDomain.m_lngGetMODSValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out objTemp);

				if(lngExist == 0)
					return -11;
				
				if(lngExist == 1)
				{
                    //if(DateTime.Parse(objTemp.strModifyDate) != DateTime.Parse(m_objMODSEvaluationDB.strModifyDate))
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

				clsEvalInfoOfMODSEvaluation objTemp;
				long lngExist = objDomain.m_lngGetMODSValue(m_strInPatientID,m_strInPatientDate,dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),out objTemp);
				
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
		private clsEvalInfoOfMODSEvaluation m_GetCurrentEvalInfo()
		{
			string str = "$$";
			
			clsEvalInfoOfMODSEvaluation objEvalInfo = new clsEvalInfoOfMODSEvaluation();

			objEvalInfo.strPatientID		= m_strInPatientID;
			objEvalInfo.strInPatientDate	= m_strInPatientDate;
			objEvalInfo.strActivityTime		= (m_strCreateDate == "") ? this.dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : m_strCreateDate;

            objEvalInfo.strEvalDoctorID = clsBaseInfo.LoginEmployee.m_strEMPID_CHR;
			objEvalInfo.strEvalDoctorName	= clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;

			objEvalInfo.strPa02				= str + m_txtPa02.Text.Trim();
			objEvalInfo.strFi02				= str + m_txtFi02.Text.Trim();
			objEvalInfo.strXJG				= str + m_txtXJG.Text.Trim();
			objEvalInfo.strDHS				= str + m_txtDHS.Text.Trim();
			objEvalInfo.strHR				= str + m_txtHR.Text.Trim();
			objEvalInfo.strYFY				= str + m_txtYFY.Text.Trim();
			objEvalInfo.strPJDMY			= str + m_txtPJDMY.Text.Trim();
			objEvalInfo.strXXB				= str + m_txtXXB.Text.Trim();
			objEvalInfo.strOpenEyes			= m_cboOpenEyes.SelectedIndex.ToString();
			objEvalInfo.strSay				= m_cboSay.SelectedIndex.ToString();
			objEvalInfo.strSport			= m_cboSport.SelectedIndex.ToString();

			objEvalInfo.strBreathEval =str + m_dtlResult.Rows[0].ItemArray[0].ToString();
			objEvalInfo.strXJGEval = str + m_dtlResult.Rows[0].ItemArray[1].ToString();
			objEvalInfo.strDHSEval = str + m_dtlResult.Rows[0].ItemArray[2].ToString();
			objEvalInfo.strXXGEval =str + m_dtlResult.Rows[0].ItemArray[3].ToString();
			objEvalInfo.strBloodEval =str + m_dtlResult.Rows[0].ItemArray[4].ToString();
			objEvalInfo.strNerveEval = str + m_dtlResult.Rows[0].ItemArray[5].ToString();
			objEvalInfo.strTotalEval = str + m_dtlResult.Rows[0].ItemArray[6].ToString();
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
				object [] res = {"/","/","/","/","/","/","/"};
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

				m_txtDHS.Text				= "";
				m_txtFi02.Text				= "";
				m_txtHR.Text				= "";
				m_txtPa02.Text				= "";
				m_txtPJDMY.Text				= "";
				m_txtXJG.Text				= "";
				m_txtXXB.Text				= "";
				m_txtYFY.Text				= "";
				m_cboOpenEyes.SelectedIndex = -1;
				m_cboSay.SelectedIndex		= -1;
				m_cboSport.SelectedIndex	= -1;

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
			long lngRes  = objDomain.m_lngGetMODSValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out m_objMODSEvaluationDB);

			if(m_objMODSEvaluationDB == null)
				return;

			lbltxtEvalDoctor.Text = m_objMODSEvaluationDB.strEvalDoctorName;


			m_txtDHS.Text				= m_objMODSEvaluationDB.strDHS;
			m_txtFi02.Text				= m_objMODSEvaluationDB.strFi02; 								
			m_txtHR.Text				= m_objMODSEvaluationDB.strHR;									
			m_txtPa02.Text				= m_objMODSEvaluationDB.strPa02;
			m_txtPJDMY.Text				= m_objMODSEvaluationDB.strPJDMY;
			m_txtXJG.Text				= m_objMODSEvaluationDB.strXJG;
			m_txtXXB.Text				= m_objMODSEvaluationDB.strXXB;
			m_txtYFY.Text				= m_objMODSEvaluationDB.strYFY;
			m_cboOpenEyes.SelectedIndex = int.Parse(m_objMODSEvaluationDB.strOpenEyes);
			m_cboSay.SelectedIndex		= int.Parse(m_objMODSEvaluationDB.strSay);
			m_cboSport.SelectedIndex	= int.Parse(m_objMODSEvaluationDB.strSport);
			m_blnCalcSuccess();

				
			dtpEvalDate.Value = DateTime.Parse(m_objMODSEvaluationDB.strActivityTime);
            clsEmrEmployeeBase_VO objEmployee = null;
            clsBaseInfo.m_lngGetEmpByID(m_objMODSEvaluationDB.strEvalDoctorID, out objEmployee);
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
					clsEvalInfoOfMODSEvaluation objEvalInfo		= m_GetCurrentEvalInfo();
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

		private void m_mthGetData(string p_strFindDate)
		{
			bool blnIsGE=m_blnCurrApparatus();

			clsCMSData objCMSData=null;
			clsVentilatorData objVentilatorData=null;

            if (m_objCurrentPatient == null) return;

			string[] strTypeArry=new string[]{"HEARTRATE"};//
			m_mthGetICUDataByTime(p_strFindDate,out objCMSData,out objVentilatorData,strTypeArry);
			
			if (!blnIsGE)
			{
				if (objCMSData!=null)
				{
					if(objCMSData.m_strHeartRate == null || objCMSData.m_strHeartRate == "")
						m_txtHR.Text = "";
					else
						m_txtHR.Text = m_strFormatShowParamData(objCMSData.m_strHeartRate);//.Substring(0,objCMSData.m_strHeartRate.IndexOf("."));
				}
			}
			else
			{
                m_mthGetMonitorParamGE();
                clsGECMSData objGECMSData = m_objGECMSData;

				if (objGECMSData!=null)
				{
					if(objGECMSData.m_strHR == null || objGECMSData.m_strHR == "")
						m_txtHR.Text = "";
					else
						m_txtHR.Text = m_strFormatShowParamData(objGECMSData.m_strHR);//.Substring(0,objGECMSData.m_strHR.IndexOf("."));

					if(objGECMSData.m_strNBPMean == null || objGECMSData.m_strNBPMean == "")
						m_txtPJDMY.Text = "";
					else
						m_txtPJDMY.Text = objGECMSData.m_strNBPMean;
				}
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
//				clsGECMSData objGECMSData=null;
//
//				bool blnIsGE=m_blnCurrApparatus();
//
//				m_mthGetICUDataByTime(dtpStartSample.Value,out objCMSData,out objVentilatorData);
//				if (blnIsGE)
//					m_mthGetICUGEDataByTime(dtpStartSample.Value.ToString(),out objGECMSData);
//				if (!blnIsGE)
//				{
//					if (objCMSData != null)
//					{
//						if(objCMSData.m_strHeartRate == null || objCMSData.m_strHeartRate == "")
//							m_txtHR.Text = "";
//						else
//							m_txtHR.Text = objCMSData.m_strHeartRate.Substring(0,objCMSData.m_strHeartRate.Length-3);
//
//					}
//				}
//				else
//				{
//					if (objGECMSData != null)
//					{
//						if(objGECMSData.m_strHR == null || objGECMSData.m_strHR == "")
//							m_txtHR.Text = "";
//						else
//							m_txtHR.Text = objGECMSData.m_strHR;
//
//					}
//				}
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
			m_mthGetData(dtpStartSample.Value.ToString());
		}

		/// <summary>
		/// 停止自动评分
		/// </summary>
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
		//直接从FORM里取得的数据
			int intPa02			= -1;
			int intFi02			= -1;
			int intXJG			= -1;
			int intDHS			= -1;
			int intHR			= -1;
			int intYFY			= -1;
			int intPJDMY		= -1;
			int intXXB			= -1;
			int intOpenEyes		= -1;
			int intSay			= -1; 
			int intSport		= -1;

			object[] res = m_dtlResult.Rows[0].ItemArray;

			#region 从Form上的TextBox和下拉列表读取数据
			try
			{
				intPa02 = int.Parse(m_txtPa02.Text);
			}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,Pa02必须输入数字!");
				m_txtPa02.Focus();
				return false;
			}

			try
			{
				intFi02 = int.Parse(m_txtFi02.Text);
			}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,Fi02必须输入数字!");
				m_txtFi02.Focus();
				return false;
			}
			try
			{
				intXJG = int.Parse(m_txtXJG.Text);
			}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,血肌酐必须输入数字!");
				m_txtXJG.Focus();
				return false;
			}
			try
			{
				intDHS = int.Parse(m_txtDHS.Text);
			}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,胆红素必须输入数字!");
				m_txtDHS.Focus();
				return false;
			}
			try
			{
				intHR = int.Parse(m_txtHR.Text);
			}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,心率必须输入数字!");
				m_txtHR.Focus();
				return false;
			}
			try
			{
				intYFY = int.Parse(m_txtYFY.Text);
			}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,右房压必须输入数字!");
				m_txtYFY.Focus();
				return false;
			}
			try
			{
				intPJDMY = int.Parse(m_txtPJDMY.Text);
			}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,平均动脉压必须输入数字!");
				m_txtPJDMY.Focus();
				return false;
			}
			try
			{
				intXXB = int.Parse(m_txtXXB.Text);
			}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,血小板必须输入数字!");
				m_txtXXB.Focus();
				return false;
			}
			try
			{
				intOpenEyes = m_cboOpenEyes.SelectedIndex + 1;
			}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,睁眼动作必须选择!");
				m_cboOpenEyes.Focus();
				return false;
			}
			try
			{
				intSay = m_cboSay.SelectedIndex + 1;
			}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,言语反应必须选择!");
				m_cboSay.Focus();
				return false;
			}
			try
			{
				intSport = m_cboSport.SelectedIndex + 1;
			}
			catch
			{
				m_mthStopAutoSave();
				clsPublicFunction.ShowInformationMessageBox("对不起,运动反应必须选择!");
				m_cboSport.Focus();
				return false;
			}
			#endregion			
			
			#region 用读取的原始数据计算MODS各项分值
			int intBreath		= intPa02/intFi02;
			int intKidney		= intXJG;
			int intLiver		= intDHS;
			double intPAHR		= intHR*intYFY/intPJDMY;
			int intBlood		= intXXB;
			int intNerve		= intOpenEyes + intSay + intSport;

			#region intBreath
			if(intBreath >300)
			{
				intBreath = 0;
			}
			else if(intBreath<=300 && intBreath >=226)
			{
				intBreath = 1;
			}
			else if(intBreath<=255 && intBreath >=151)
			{
				intBreath = 2;
			}
			else if(intBreath<=150 && intBreath >=76)
			{
				intBreath = 3;
			}
			else if(intBreath<=75)
			{
				intBreath = 4;
			}
			#endregion intBreath

			#region intKidney
			if(intKidney<=100 && intKidney>0)
			{
				intKidney = 0;
			}
			else if(intKidney>=101 && intKidney<=200)
			{
				intKidney = 1;
			}
			else if(intKidney>=201 && intKidney <=350)
			{
				intKidney = 2;
			}
			else if(intKidney >=351 && intKidney<=500)
			{
				intKidney = 3;
			}
			else if(intKidney >=500)
			{
				intKidney =4;
			}
			#endregion intKidney

			#region intLiver
			if(intLiver<=20 && intLiver>0)
			{
				intLiver = 0;
			}
			else if(intLiver>=21 && intLiver<=60)
			{
				intLiver = 1;
			}
			else if(intLiver>=61 && intLiver<=120)
			{
				intLiver = 2;
			}
			else if(intLiver>=121 && intLiver<=240)
			{
				intLiver = 3;
			}
			else if(intLiver>=241)
			{
				intLiver = 4;
			}
			#endregion intLiver

			#region intPAHR
			if(intPAHR<=10 && intPAHR >0)
			{
				intPAHR = 0;
			}
			else if(intPAHR >10 && intPAHR<=15)
			{
				intPAHR = 1;
			}
			else if(intPAHR>15 && intPAHR<=20)
			{
				intPAHR = 2;
			}
			else if(intPAHR>20 && intPAHR <=30)
			{
				intPAHR = 3;
			}
			else if(intPAHR>30)
			{
				intPAHR = 4;
			}
			#endregion intPAHR

			#region intBlood
			if(intBlood>120)
			{
				intBlood = 0;
			}
			else if(intBlood <=120 && intBlood >=81)
			{
				intBlood = 1;
			}
			else if(intBlood <=80 && intBlood >=51)
			{
				intBlood = 2;
			}
			else if (intBlood<=50 && intBlood >=21)
			{
				intBlood = 3;
			}
			else if (intBlood<=20 && intBlood >=0)
			{
				intBlood = 4;
			}
			#endregion intBlood

			#region intNerve
				if(intNerve == 15)
			{
				intNerve = 0;
			}
			else if(intNerve<15 && intNerve >=13)
			{
				intNerve = 1;
			}
			else if(intNerve>=10 && intNerve <=12)
			{
				intNerve = 2;
			}
			else if(intNerve>=7 && intNerve <=9)
			{
				intNerve = 3;
			}
			else if(intNerve <=6)
			{
				intNerve =4;
			}
			#endregion intNerve
			#endregion 计算各项分值

			double intTotalEval	= intBreath + intKidney + intLiver + intPAHR + intBlood + intNerve;

			res[0] = intBreath;
			res[1] = intKidney;
			res[2] = intLiver;
			res[3] = intPAHR;
			res[4] = intBlood;
			res[5] = intNerve;
			res[6] = intTotalEval;

			m_dtlResult.Rows[0].ItemArray = res;

			return true;	
		}


		#endregion 用户自定义函数


		#region Print Function

		public override void m_mthSetPrint()
		{
			clsEvalInfoOfMODSEvaluation objValue;
			objPrintTool=new clsMODS_ValuationPrintTool();
			objPrintTool.m_mthInitPrintTool(null);	
			if(m_objCurrentPatient == null)
				objPrintTool.m_mthSetPrintInfo(null,null,DateTime.MinValue);
			else
			{
				if(this.trvActivityTime.SelectedNode ==null || this.trvActivityTime.SelectedNode==trvActivityTime.Nodes[0] || trvActivityTime.SelectedNode.Tag==null)
					objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient,null,DateTime.MinValue);
				else 
				{
					objDomain.m_lngGetMODSValue(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),trvActivityTime.SelectedNode.Tag.ToString(),out objValue);
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

