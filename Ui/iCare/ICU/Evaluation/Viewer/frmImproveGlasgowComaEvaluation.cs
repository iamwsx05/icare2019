//#define FunctionPrivilege
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using com.digitalwave.Utility;
using com.digitalwave.Utility.Controls;
using weCare.Core.Entity;
//using iCare.Common;
using com.digitalwave.Emr.Signature_gui;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// 改良Glasgow昏迷评分
	/// </summary>
	public partial class frmImproveGlasgowComaEvaluation : frmValuationBaseForm,PublicFunction
	{
		#region Define

		public  com.digitalwave.Utility.Controls.ctlTimePicker dtpEvalDate;
		private System.Windows.Forms.Label lblEvalDate;
		private com.digitalwave.Utility.Controls.ctlComboBox cboPupil;
		private com.digitalwave.Utility.Controls.ctlComboBox cboBrain;
		private com.digitalwave.Utility.Controls.ctlComboBox cboSpontaneityBreath;
		private com.digitalwave.Utility.Controls.ctlComboBox cboTwitch;
		private System.Windows.Forms.GroupBox gpbAge;
		private System.Windows.Forms.RadioButton rdbU1;
		private System.Windows.Forms.RadioButton rdbU2;
		private System.Windows.Forms.RadioButton rdbU3;
		private System.Windows.Forms.RadioButton rdbU4;
		private System.Windows.Forms.RadioButton rdbU5;
		private System.Windows.Forms.RadioButton rdbO5;
		private com.digitalwave.Utility.Controls.ctlComboBox cboOpenEyeU1;
		private com.digitalwave.Utility.Controls.ctlComboBox cboOpenEyeO1;
		private com.digitalwave.Utility.Controls.ctlComboBox cboSportU1;
		private com.digitalwave.Utility.Controls.ctlComboBox cboSportO1;
		private com.digitalwave.Utility.Controls.ctlComboBox cboLangU2;
		private com.digitalwave.Utility.Controls.ctlComboBox cboLangU5;
		private com.digitalwave.Utility.Controls.ctlComboBox cboLangO5;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.DataGrid dtgResult;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn7;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn8;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtEvalDoctor;
		private System.Windows.Forms.Label lblTitle3;
		private System.Windows.Forms.Label lblTitle4;
		private System.Windows.Forms.Label lblTitle5;
		private System.Windows.Forms.Label lblTitle6;
		private System.Windows.Forms.Label lblTitle7;
		private System.Windows.Forms.Label lblTitle8;
		private System.Windows.Forms.Label lblTitle9;
		private System.Windows.Forms.Label lblTitle10;
		private System.Windows.Forms.Label lblTitle11;
		private System.Windows.Forms.Label lblTitle12;
		private System.Windows.Forms.Label lblTitle13;
		private System.Windows.Forms.Label lblTitle14;

		private DataTable dtlResult;
		public string strSickBedNO;		
		private clsBorderTool objBorderTool;
		#endregion
		private PinkieControls.ButtonXP m_cmdEvalDoctor;
        private PinkieControls.ButtonXP cmdCalculate;
		private System.Windows.Forms.GroupBox groupBox2;



		#region Constructor

		//private clsCommonUseToolCollection m_objCUTC;

		public frmImproveGlasgowComaEvaluation()
		{			
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_objDomain = new clsImproveGlasgowComaEvalutionDomain();

			dtlResult = new DataTable("result");
			age = 0;

			this.cboOpenEyeU1.AddRangeItems(new object[] {
															  "1.自发",
															  "2.声音刺激时",
															  "3.疼痛刺激时",
															  "4.刺激后无反应"});
			this.cboOpenEyeO1.AddRangeItems(new object[] {
															  "1.自发",
															  "2.语言刺激时",
															  "3.疼痛刺激时",
															  "4.刺激后无反应"});
			this.cboSportU1.AddRangeItems(new object[] {
															"1.自发",
															"2.因局部疼痛而动",
															"3.因痛而屈曲回缩",
															"4.因疼痛而呈现屈曲反应(似去皮层强直)",
															"5.因疼痛而呈现屈曲反应(似去大脑强直)",
															"6.无运动反应"});
			this.cboSportO1.AddRangeItems(new object[] {
															"1.服从命令动作",
															"2.因局部疼痛而动",
															"3.因痛而屈曲回缩",
															"4.因疼痛而呈现屈曲反应(似去皮层强直)",
															"5.因疼痛而呈现屈曲反应(似去大脑强直)",
															"6.无运动反应"});
			this.cboLangU2.AddRangeItems(new object[] {
														   "1.微笑,发声",
														   "2.哭闹,可安慰",
														   "3.持续哭闹,尖叫",
														   "4.呻吟,不安",
														   "5.无反应"});
			this.cboLangU5.AddRangeItems(new object[] {
														   "1.适当的单词,短语",
														   "2.词语不当",
														   "3.持续哭闹,尖叫",
														   "4.呻吟",
														   "5.无反应"});
			this.cboLangO5.AddRangeItems(new object[] {
														   "1.能定向说话",
														   "2.不能定向",
														   "3.语言不当",
														   "4.语言难于理解",
														   "5.无反应"});
			this.cboPupil.AddRangeItems(new object[] {
														  "1.正常",
														  "2.迟钝",
														  "3.两侧反应不同",
														  "4.大小不等",
														  "5.无反应"});
			this.cboBrain.AddRangeItems(new object[] {
														  "1.全部存在",
														  "2.睫毛反射消失",
														  "3.角膜反射消失",
														  "4.眼脑及眼前庭反射消失",
														  "5.上述反射消失"});
			this.cboSpontaneityBreath.AddRangeItems(new object[] {
																	  "1.正常",
																	  "2.周期性",
																	  "3.中枢过度换气",
																	  "4.不规则或低呼吸",
																	  "5.无"});
			this.cboTwitch.AddRangeItems(new object[] {
														   "1.无抽搐",
														   "2.局限性抽搐",
														   "3.阵法性大发作",
														   "4.连续性大发作",
														   "5.松弛状态"});


			objBorderTool = new clsBorderTool(Color.White);
			objBorderTool.m_mthChangedControlBorder(trvActivityTime);

			m_mthSetQuickKeys();

			m_objHighLight = new ctlHighLightFocus(clsHRPColor.s_ClrHightLight);
			
		}
		#endregion

		protected ctlHighLightFocus m_objHighLight;

		#region Member
		private clsImproveGlasgowComaEvalutionDomain m_objDomain;

		private clsImproveGlasgowComaEvaluation m_objImproveGlasgowComaEvaluation;

		private clsSystemContext m_objCurrentContext
		{
			get
			{
				return clsSystemContext.s_ObjCurrentContext;
			}
		}

		#endregion

		

		public void ReceiveID(string strID)
		{
			if(strID.Trim().Length !=0)
			{				
				this.Display(strID,"");
			}
		}
		private int age = 0;
		private void AgeGroupChanged(object sender, System.EventArgs e)
		{
			try
			{
				age = int.Parse((string)((RadioButton)sender).Tag);
				if(age<1)
				{
					cboOpenEyeU1.Enabled = true;
					cboOpenEyeO1.Enabled = false;
					cboSportU1.Enabled = true;
					cboSportO1.Enabled = false;
				}
				else
				{
					cboOpenEyeU1.Enabled = false;
					cboOpenEyeO1.Enabled = true;
					cboSportU1.Enabled = false;
					cboSportO1.Enabled = true;
				}

				if(age<2)
				{
					cboLangU2.Enabled = true;
					cboLangU5.Enabled = false;
					cboLangO5.Enabled = false;
				}
				else if(age<5)
				{
					cboLangU2.Enabled = false;
					cboLangU5.Enabled = true;
					cboLangO5.Enabled = false;
				}
				else 
				{
					cboLangU2.Enabled = false;
					cboLangU5.Enabled = false;
					cboLangO5.Enabled = true;
				}
			}
			catch
			{
			}
		}

        //定义签名类
        private clsEmrSignToolCollection m_objSign = new clsEmrSignToolCollection();

		private void ImproveGlasgowComaEvaluation_Load(object sender, System.EventArgs e)
		{
			try
			{
				cboOpenEyeO1.SelectedIndex = 0;
				cboOpenEyeU1.SelectedIndex = 0;
				cboSportO1.SelectedIndex = 0;
				cboSportU1.SelectedIndex = 0;
				cboLangO5.SelectedIndex = 0;
				cboLangU2.SelectedIndex = 0;
				cboLangU5.SelectedIndex = 0;
				cboPupil.SelectedIndex = 0;
				cboBrain.SelectedIndex = 0;
				cboTwitch.SelectedIndex = 0;
				cboSpontaneityBreath.SelectedIndex = 0;

				dtlResult.Columns.Add("睁眼");
				dtlResult.Columns.Add("运动反应");
				dtlResult.Columns.Add("语言反应");
				dtlResult.Columns.Add("瞳孔光反应");
				dtlResult.Columns.Add("脑干反射");
				dtlResult.Columns.Add("抽搐");
				dtlResult.Columns.Add("自发性呼吸");
				dtlResult.Columns.Add("总分");

				dtgResult.DataSource = dtlResult;

				dtlResult.Rows.Add(new string[]{"/","/","/","/","/","/","/","/"});


				this.txtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;

				m_objHighLight.m_mthAddControlInContainer(this);

				//this.dtpEvalDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
				this.dtpEvalDate.m_mthResetSize();
                m_objSign.m_mthBindEmployeeSign(this.m_cmdEvalDoctor, txtEvalDoctor, 1, false, clsBaseInfo.LoginEmployee.m_strEMPID_CHR);
				
			}
			catch
			{
			}
			
		}

		private void cmdCalculate_Click(object sender, System.EventArgs e)
		{
			try
			{
				int val = 0;
				int totalval = 0;
				object [] res = dtlResult.Rows[0].ItemArray;

				//cal openeye and sport
				if(age<1)
				{
					val = 4-cboOpenEyeU1.SelectedIndex;
					res[0] = val.ToString();
					totalval += val; 

					val = 6-cboSportU1.SelectedIndex;
					res[1] = val.ToString();
					totalval += val; 
				}
				else 
				{
					val = 4-cboOpenEyeO1.SelectedIndex;
					res[0] = val.ToString();
					totalval += val; 

					val = 6-cboSportO1.SelectedIndex;
					res[1] = val.ToString();
					totalval += val; 
				}

				//cal langure
				if(age<2)
					val = 5-cboLangU2.SelectedIndex;
				else if(age<5)
					val = 5-cboLangU5.SelectedIndex;
				else 
					val = 5-cboLangO5.SelectedIndex;
				res[2] = val.ToString();
				totalval += val; 

				//cal Pupil
				val = 5-cboPupil.SelectedIndex;
				res[3] = val.ToString();
				totalval += val; 

				//cal Brain
				val = 5-cboBrain.SelectedIndex;
				res[4] = val.ToString();
				totalval += val; 

				//cal Twitch
				val = 5-cboTwitch.SelectedIndex;
				res[5] = val.ToString();
				totalval += val; 

				//cal SpontaneityBreath
				val = 5-cboSpontaneityBreath.SelectedIndex;
				res[6] = val.ToString();
				totalval += val; 

				res[7] = totalval.ToString();
			
				dtlResult.Rows[0].ItemArray = res;
			}
			catch
			{
			}
		}


		#region interface

		#region Null Impletement
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

		public void Verify()
		{
			////long lngRes=m_lngSignVerify(p_strFormID,p_strRecordID);
		}

		public void Print()
		{
			m_lngPrint();
		}
		public void Display()
		{
		
		}
		public void Display(string cardno, string ActivityTime)
		{

		}

		#endregion

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

			long lngRes = m_objDomain.m_lngDeactive(clsBaseInfo.LoginEmployee.m_strEMPID_CHR, m_strInPatientID, m_strInPatientDate, m_strCreateDate);

			if(lngRes <= 0)
			{
				clsPublicFunction.ShowInformationMessageBox("删除失败，请重新操作!");
				return;
			}

			trvActivityTime.SelectedNode.Remove();

			this.trvActivityTime.SelectedNode=this.trvActivityTime.Nodes[0];
			ClearUp();
		}

		public void Save()
		{
#if FunctionPrivilege
			if(!MDIParent.s_blnCheckCurrentPrivilege(lbltxtSetionOffice.Tag.ToString(),PrivilegeData.enmPrivilegeSF.ImproveGlasgowComaEvaluation,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			m_lngSubSave();
		}
		
		public override long m_lngSubSave()
		{
			return m_lngSaveWithMessageBox();
		}
		#endregion

		private void ImproveGlasgowComaEvaluation_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
		}

		#region ClearUp
		protected override void ClearUp()
		{
			try
			{
				object [] res = {"/","/","/","/","/","/","/","/"};
                if (dtlResult.Rows.Count < 0 || dtlResult.Rows.Count == 0)
                {
                    return;
                }
				dtlResult.Rows[0].ItemArray = res;
				rdbU1.Checked = true;
				dtpEvalDate.Value = DateTime.Now;

				foreach(Control control in this.Controls)
				{				
					string typeName = control.GetType().Name;
					if(typeName == "ctlBorderTextBox"&&control.Name!="txtCardNo")
					{
						control.Text = "";
						
					}
					else if(typeName=="CheckBox")
					{
						((CheckBox)control).Checked=false;
					}
					else if(typeName=="ctlComboBox")
					{
						//if (control.Name != "m_cboDept")
						if (((ctlComboBox)control).GetItemsCount()>0)
							((ctlComboBox)control).SelectedIndex = 0;
					}
				}
                
				m_strCreateDate = "";
				this.txtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;
			}
			catch(Exception err)
			{
				clsPublicFunction.ShowInformationMessageBox(err.Message);
			}
		}

		private void m_mthClearPatientInfo()
		{
			m_strInPatientID = "";
			m_strInPatientDate = "";
		}
		#endregion

		protected override void m_mthLoadAllRecordTimeOfAPatient(string p_strPatientID,string p_strPatientDate,string p_strFromDate, string p_strToDate)
		{
			if(p_strPatientID ==null || p_strPatientID =="") 
				return ;

			this.trvActivityTime.Nodes[0].Nodes.Clear();

			DateTime [] m_dtmArr = m_objDomain.m_dtmGetTimeInfoOfAPatientArr(p_strPatientID ,p_strPatientDate, p_strFromDate, p_strToDate);

			if(m_dtmArr!=null) 
			{
				for(int i=0;i<m_dtmArr.Length ;i++)
				{
					string strDate = m_dtmArr[i].ToString("yyyy-MM-dd HH:mm:ss");
					TreeNode trnDate=new TreeNode(strDate);
					trnDate.Tag = m_dtmArr[i];
					this.trvActivityTime.Nodes[0].Nodes.Add(trnDate);
				}

			}
			this.trvActivityTime.ExpandAll();
			trvActivityTime.SelectedNode = trvActivityTime.Nodes[0];
			this.trvActivityTime_AfterSelect(this.trvActivityTime,new TreeViewEventArgs(trvActivityTime.Nodes[0]));
		}

		#region Save
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

		private long m_lngSaveWithoutMessageBox()
		{
			if(m_strInPatientID == null || m_strInPatientID == "")
			{
				clsPublicFunction.ShowInformationMessageBox("对不起，请输入病人住院编号！");
				return 0;
			}

			string strCurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

			this.cmdCalculate_Click(null,null);

			#region 赋值
			clsImproveGlasgowComaEvaluation objImproveGlasgowEvaluation = new clsImproveGlasgowComaEvaluation();

			objImproveGlasgowEvaluation.m_strInPatientNO = m_strInPatientID;
			objImproveGlasgowEvaluation.m_strInPatientDate = m_strInPatientDate;
			objImproveGlasgowEvaluation.m_strActivityTime = (m_strCreateDate == "") ? this.dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : m_strCreateDate;

			objImproveGlasgowEvaluation.m_strEvalDoctorID = clsBaseInfo.LoginEmployee.m_strEMPID_CHR;
			objImproveGlasgowEvaluation.m_strAgeGroup = age.ToString();
			objImproveGlasgowEvaluation.m_strOpenEyeU1 = this.cboOpenEyeU1.Text;
			objImproveGlasgowEvaluation.m_strOpenEyeO1 = this.cboOpenEyeO1.Text;
			objImproveGlasgowEvaluation.m_strSportFeedbackU1 = this.cboSportU1.Text;
			objImproveGlasgowEvaluation.m_strSportFeedbackO1 = this.cboSportO1.Text;
			objImproveGlasgowEvaluation.m_strTalkFeedbackU2 = this.cboLangU2.Text;
			objImproveGlasgowEvaluation.m_strTalkFeedbackU5 = this.cboLangU5.Text;
			objImproveGlasgowEvaluation.m_strTalkFeedbackO5 = this.cboLangO5.Text;
			objImproveGlasgowEvaluation.m_strPupilLightFeedback = this.cboPupil.Text;
			objImproveGlasgowEvaluation.m_strBrainReflect = this.cboBrain.Text;
			objImproveGlasgowEvaluation.m_strTwitch = this.cboTwitch.Text;
			objImproveGlasgowEvaluation.m_strSpontaneityBreath = this.cboSpontaneityBreath.Text;

			objImproveGlasgowEvaluation.m_strOpenEyeEval = this.dtlResult.Rows[0].ItemArray[0].ToString();
			objImproveGlasgowEvaluation.m_strSportFeedbackEval = this.dtlResult.Rows[0].ItemArray[1].ToString();
			objImproveGlasgowEvaluation.m_strTalkFeedbackEval = this.dtlResult.Rows[0].ItemArray[2].ToString();
			objImproveGlasgowEvaluation.m_strPupilLightFeedbackEval = this.dtlResult.Rows[0].ItemArray[3].ToString();
			objImproveGlasgowEvaluation.m_strBrainReflectEval = this.dtlResult.Rows[0].ItemArray[4].ToString();
			objImproveGlasgowEvaluation.m_strTwitchEval = this.dtlResult.Rows[0].ItemArray[5].ToString();
			objImproveGlasgowEvaluation.m_strSpontaneityBreathEval = this.dtlResult.Rows[0].ItemArray[6].ToString();
			objImproveGlasgowEvaluation.m_strTotalEval = this.dtlResult.Rows[0].ItemArray[7].ToString();
			
			#endregion

			this.cmdCalculate_Click(null,null);

			if(m_strCreateDate != "")
			{
                //if(m_objCurrentContext.m_ObjControl.m_enmModifyCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
                //    == enmDBControlCheckResult.Disable)
                //{
                //    clsPublicFunction.s_mthShowNotPermitMessage();
                //    return 0;
                //}

				clsImproveGlasgowComaEvaluation objTemp;
				long lngExist = m_objDomain.m_lngGetImproveGlasgowComaValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out objTemp);

				if(lngExist == 0)
					return -11;
				
				if(lngExist == 1)
				{
                    //if(DateTime.Parse(objTemp.m_strModifyDate) != DateTime.Parse(m_objImproveGlasgowComaEvaluation.m_strModifyDate))
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

				clsImproveGlasgowComaEvaluation objTemp;
				long lngExist = m_objDomain.m_lngGetImproveGlasgowComaValue(m_strInPatientID,m_strInPatientDate,dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),out objTemp);
				
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

			long lngRes = m_objDomain.m_lngSave(objImproveGlasgowEvaluation);
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
		#endregion

		#region Display

		protected override void m_mthDisplay()
		{
			long lngRes  = m_objDomain.m_lngGetImproveGlasgowComaValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out m_objImproveGlasgowComaEvaluation);

			if(m_objImproveGlasgowComaEvaluation == null)
				return;

            clsEmrEmployeeBase_VO objEmployee = null;
            clsBaseInfo.m_lngGetEmpByID(m_objImproveGlasgowComaEvaluation.m_strEvalDoctorID, out objEmployee);
            this.txtEvalDoctor.Text = objEmployee.m_strLASTNAME_VCHR;
			
			this.age = int.Parse(m_objImproveGlasgowComaEvaluation.m_strAgeGroup);
			foreach(Control ctrl in gpbAge.Controls)
			{
				if(int.Parse((string)ctrl.Tag) == age)
				{
					((RadioButton)ctrl).Checked = true;
					break;
				}								
			}

			this.cboOpenEyeU1.Text = m_objImproveGlasgowComaEvaluation.m_strOpenEyeU1 ;
			this.cboOpenEyeO1.Text = m_objImproveGlasgowComaEvaluation.m_strOpenEyeO1 ;
			this.cboSportU1.Text = m_objImproveGlasgowComaEvaluation.m_strSportFeedbackU1 ;
			this.cboSportO1.Text = m_objImproveGlasgowComaEvaluation.m_strSportFeedbackO1 ;
			this.cboLangU2.Text = m_objImproveGlasgowComaEvaluation.m_strTalkFeedbackU2 ;
			this.cboLangU5.Text = m_objImproveGlasgowComaEvaluation.m_strTalkFeedbackU5 ;
			this.cboLangO5.Text = m_objImproveGlasgowComaEvaluation.m_strTalkFeedbackO5 ;
			this.cboPupil.Text = m_objImproveGlasgowComaEvaluation.m_strPupilLightFeedback ;
			this.cboBrain.Text = m_objImproveGlasgowComaEvaluation.m_strBrainReflect ;
			this.cboTwitch.Text = m_objImproveGlasgowComaEvaluation.m_strTwitch ;
			this.cboSpontaneityBreath.Text = m_objImproveGlasgowComaEvaluation.m_strSpontaneityBreath ;
			this.dtpEvalDate.Value = DateTime.Parse(m_objImproveGlasgowComaEvaluation.m_strActivityTime);

			object []res = dtlResult.Rows[0].ItemArray;

			res[0] = m_objImproveGlasgowComaEvaluation.m_strOpenEyeEval ;
			res[1] = m_objImproveGlasgowComaEvaluation.m_strSportFeedbackEval ;
			res[2] = m_objImproveGlasgowComaEvaluation.m_strTalkFeedbackEval ;
			res[3] = m_objImproveGlasgowComaEvaluation.m_strPupilLightFeedbackEval ;
			res[4] = m_objImproveGlasgowComaEvaluation.m_strBrainReflectEval ;
			res[5] = m_objImproveGlasgowComaEvaluation.m_strTwitchEval ;
			res[6] = m_objImproveGlasgowComaEvaluation.m_strSpontaneityBreathEval ;
			res[7] = m_objImproveGlasgowComaEvaluation.m_strTotalEval ;

			dtlResult.Rows[0].ItemArray = res;            
		}
		#endregion


		#region 添加键盘快捷键
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
						if(strSubTypeName != "Label" &&  strSubTypeName != "CheckBox" && strSubTypeName != "ctlBorderTextBox")												
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

		#endregion

		#region Copy,Cut,Paste
		/// <summary>
		/// 复制操作
		/// </summary>
		/// <returns>操作结果</returns>
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
		/// 剪切操作
		/// </summary>
		/// <returns>操作结果</returns>
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
		/// 粘贴操作
		/// </summary>
		/// <returns>操作结果</returns>
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

		#region Print Function

		public override void m_mthSetPrint()
		{
			clsImproveGlasgowComaEvaluation objValue;
			objPrintTool=new clsGlasgow_ValuationPrintTool();
			objPrintTool.m_mthInitPrintTool(null);	
			if(m_objCurrentPatient == null)
				objPrintTool.m_mthSetPrintInfo(null,null,DateTime.MinValue);
			else
			{
				if(this.trvActivityTime.SelectedNode ==null || this.trvActivityTime.SelectedNode==trvActivityTime.Nodes[0] || trvActivityTime.SelectedNode.Tag==null)
					objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient,null,DateTime.MinValue);
				else 
				{
					m_objDomain.m_lngGetImproveGlasgowComaValue(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),trvActivityTime.SelectedNode.Tag.ToString(),out objValue);
					object obj = objValue;
					objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient,obj,DateTime.Parse(trvActivityTime.SelectedNode.Tag.ToString()));
				}
			}					
			objPrintTool.m_mthInitPrintContent();
		}
		
		#endregion

	}
}
