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
//using com.digitalwave.iCare.middletier.ICU;
using com.digitalwave.iCare.common;
//using iCare.ICU.Espial;
//using iCare.Common;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// SIRS智能评分
	/// </summary>
	public partial class frmSIRSEvaluation : frmValuationBaseForm,PublicFunction
	{
		#region define

		private System.Windows.Forms.GroupBox gpbAge;
		private System.Windows.Forms.RadioButton rdbAgeO5Day;
		private System.Windows.Forms.RadioButton rdbAgeU1Mon;
		private System.Windows.Forms.RadioButton rdbAgeU1Year;
		private System.Windows.Forms.RadioButton rdbAgeU2Year;
		private System.Windows.Forms.RadioButton rdbAgeU5Year;
		private System.Windows.Forms.RadioButton rdbAgeU12Year;
		private System.Windows.Forms.RadioButton rdbAgeU15Year;
		private System.Windows.Forms.RadioButton rdbAgeO15Year;
		private System.Windows.Forms.Label lblTitleBreath;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBreath;
		private System.Windows.Forms.Label lblTitleBreathUnit;
		private System.Windows.Forms.Label lblTitleHR;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtHeartRate;
		private System.Windows.Forms.Label lblTitleHRUnit;
		private System.Windows.Forms.Label lblTitleTempUnit;
		private System.Windows.Forms.Label lblTitleTemp;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtTemperature;
		private System.Windows.Forms.GroupBox gpbWBC;
		private System.Windows.Forms.RadioButton rdbWBC;
		private System.Windows.Forms.RadioButton rdbBacillus;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtWBC;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBacillus;
		private System.Windows.Forms.Label lblTitleWBCUnit;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblTitle31;
		private com.digitalwave.Utility.Controls.ctlTimePicker dtpEvalDate;
		private System.Windows.Forms.Label lblEvalDate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		
		private System.Windows.Forms.DataGrid dtgResult;
		int intAge;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
		private System.Windows.Forms.Label lblTileWBCSpec;
		private System.Windows.Forms.Label lblTitleAgeSpec;
		private System.Windows.Forms.Label lblTitle10;
		private System.Windows.Forms.Label lblTitle96;
		private com.digitalwave.Utility.Controls.ctlTimePicker dtpStartSample;
		private System.Windows.Forms.Label label2;
		private System.Timers.Timer timAutoCollect;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtAutoTime;
		private System.Windows.Forms.Label lblTitle11;
		private DataTable dtlResult;		
        
		private System.Windows.Forms.GroupBox gpbEvaluation;
		private System.Windows.Forms.Label lbltxtEvalDoctor;
//		public string strSickBedNO;
		
        private clsSIRSEvaluationDomain objDomain;
		private PinkieControls.ButtonXP m_cmdGetDovueData;
		private PinkieControls.ButtonXP cmdCalculate;
		private PinkieControls.ButtonXP cmdGetData;
		private PinkieControls.ButtonXP cmdStartAuto;
		private PinkieControls.ButtonXP cmdStopAuto;
		private PinkieControls.ButtonXP cmdShowResult;
		private PinkieControls.ButtonXP m_cmdGetCheckData;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;

		/// <summary>
		/// //设置状态 "0--增加" "1--修改"
		/// </summary>
		private string strAddOrModify;
		#endregion

		#region Constructor

		public frmSIRSEvaluation()
		{		

			InitializeComponent();

			
			intAge = 0;
			dtlResult = new DataTable("result");

			ColumnHeader chResult1 = new ColumnHeader();
			chResult1.Text = "呼吸频率";
			chResult1.Width = 100;

			ColumnHeader chResult2 = new ColumnHeader();
			chResult2.Text = "心率";
			chResult2.Width = 100;

			ColumnHeader chResult3 = new ColumnHeader();
			chResult3.Text = "体温";
			chResult3.Width = 100;

			ColumnHeader chResult4 = new ColumnHeader();
			chResult4.Text = "白细胞计数和分类";
			chResult4.Width = 180;

			ColumnHeader chResult5 = new ColumnHeader();
			chResult5.Text = "诊断结果";
			chResult5.Width = 100;

			
		
			frmAutoResult = new frmAutoEvalResult(chResult5,chResult1,chResult2,chResult3,chResult4);

			frmAutoResult.Text = "SIRS自动诊断";

//			frmAutoResult.Show();
			frmAutoResult.Owner = this;
			frmAutoResult.Visible = false;
            
			objDomain = new clsSIRSEvaluationDomain();
			strAddOrModify = "0";

			m_objBorderTool = new com.digitalwave.Utility.Controls.clsBorderTool(Color.White);

			m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{
																			 trvActivityTime,dtgResult
																		 });

			m_mthSetQuickKeys();

			m_objHighLight = new ctlHighLightFocus(clsHRPColor.s_ClrHightLight);

//			gpbEvaluation.Visible=false;
		}
		#endregion
		protected ctlHighLightFocus m_objHighLight;
		#region Member
		private com.digitalwave.Utility.Controls.clsBorderTool m_objBorderTool;


		private const string _strTypeMonitor = "Monitoring";
		private const string _strTypeVentilator = "Ventilator";

		private clsEvalInfoOfSIRSEvaluation m_objSIRSEvaluationDB;

		private frmAutoEvalResult frmAutoResult=null;

		private clsSystemContext m_objCurrentContext
		{
			get
			{
				return clsSystemContext.s_ObjCurrentContext;
			}
		}

		#endregion

		
		#region 计算评分
		public void ReceiveID(string strID)
		{
			if(strID.Trim().Length !=0)
			{				
//				this.Display(strID,"");//Alex 2002-9-29
			}
		}
		private void WBCChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(sender.Equals(rdbWBC) && rdbWBC.Checked)
				{
					txtWBC.Enabled =true;
					txtBacillus.Enabled =false;
					txtBacillus.Text = "";
					
				}
				else if(rdbBacillus.Checked)
				{
					txtWBC.Enabled =false;
					txtBacillus.Enabled =true;
					txtWBC.Text = "";
					
				}
			}
			catch
			{
			}
		}

		private void AgeGroupChanged(object sender, System.EventArgs e)
		{
			try
			{
				intAge = int.Parse((string)((RadioButton)sender).Tag);
			}
			catch
			{
			}
		}

		private void cmdCalculate_Click(object sender, System.EventArgs e)
		{			
			try
			{
				int intBreath = -1;
				int intHR = -1;
				double dblTemp = -1;
				double dblWBC = -1;
				double dblBacillus = -1;

			
				try
				{
					intBreath = int.Parse(txtBreath.Text);
				}
				catch{}
				try
				{
					intHR = int.Parse(txtHeartRate.Text);
				}
				catch{}
				try
				{
					dblTemp = double.Parse(txtTemperature.Text);
				}
				catch{}
				try
				{
					if(rdbWBC.Checked)
						dblWBC = double.Parse(txtWBC.Text);
					else
						dblBacillus = double.Parse(txtBacillus.Text);
				}
				catch{}
				switch(intAge)
				{
					case 0:		
						CalRes(intBreath,60,intHR,190,dblTemp,38,35.5,
							dblWBC,35,4,dblBacillus,30);
						
						break;
					case 1:
						CalRes(intBreath,60,intHR,190,dblTemp,38,35.5,dblWBC,
							20,4,dblBacillus,25);
						break;
					case 2:
						CalRes(intBreath,45,intHR,160,dblTemp,38.5,36,dblWBC,
							15,4,dblBacillus,20);
						break;
					case 3:
						CalRes(intBreath,40,intHR,140,dblTemp,39,36,dblWBC,
							15,4,dblBacillus,15);
						break;
					case 4:
						CalRes(intBreath,35,intHR,130,dblTemp,39,36,dblWBC,
							15,4,dblBacillus,15);
						break;
					case 5:
						CalRes(intBreath,30,intHR,120,dblTemp,38.7,36,dblWBC,
							12,4,dblBacillus,10);
						break;
					case 6:
						CalRes(intBreath,25,intHR,100,dblTemp,38.5,36,dblWBC,
							12,4,dblBacillus,10);
						break;
					case 7:
						CalRes(intBreath,20,intHR,90,dblTemp,38,36,dblWBC,
							12,4,dblBacillus,10);
						break;
				}			
			}
			catch
			{
			}
		}

		private void CalRes(int intBreath,int intBreathEval,
			int intHR,int intHREval,double dblTemp,double dblHighTempEval,double dblLowTempEval,
			double dblWBC,double dblHighWBCEval,double dblLowWBCEval,double dblBacillus,double dblBacillusEval)
		{
			try
			{
				object []res = dtlResult.Rows[0].ItemArray;
				bool blnTemp = false;
				bool blnXB = false;

				int correctCount = 0;
				int count = 0;
				if(intBreath>-1)
				{
					correctCount++;
					if(intBreath>intBreathEval)
					{
						count++;
						res[0] = "具备";
					}
					else
						res[0] = "不具备";
				}
				else 
					res[0] = "/";


				if(intHR>-1)
				{
					correctCount++;
					if(intHR>intHREval)
					{
						count++;
						res[1] = "具备";
					}
					else
						res[1] = "不具备";
				}
				else 
					res[1] = "/";

				if(dblTemp>-1)
				{
					correctCount++;
					if(dblTemp>dblHighTempEval || dblTemp<dblLowTempEval)
					{
						count++;
						res[2] = "具备";
						blnTemp = true;
					}
					else
						res[2] = "不具备";
				}
				else 
					res[2] = "/";

				if(rdbWBC.Checked)
				{
					if(dblWBC>-1)
					{
						correctCount++;
						if(dblWBC>dblHighWBCEval || dblWBC<dblLowWBCEval)
						{
							count++;
							res[3] = "具备";
							blnXB = true;
						}
						else
							res[3] = "不具备";
					}
					else 
						res[3] = "/";
				}
				else
					if(dblBacillus > -1)
				{
					correctCount++;
					if(dblBacillus>dblBacillusEval)
					{
						count++;
						res[3] = "具备";
						blnXB = true;
					}
					else
						res[3] = "不具备";
				}
				else 
					res[3] = "/";


				if(correctCount==0)
				{
					res [4] = "/";
//					return; //2003-5-14 wingo modified
				}

				if(count<2)
					res[4] = "具备 "+count+" 项条件，不是 SIRS";
				else if(count >=2 && (blnTemp || blnXB))
					res[4] = "具备 "+count+" 项条件，诊断为 SIRS";
				else
					res[4] = "具备 "+count+" 项条件，不是 SIRS";

				dtlResult.Rows[0].ItemArray = res;
			}
			catch
			{
			}
		}
		#endregion

		#region 自动评分 -- 以注释
		/// <summary>
		/// 自定评分使用
		/// Alex 2002-9-28
		/// </summary>
		public void m_AutoSave(string m_strAutoEvalDate)
		{
			try
			{
				this.cmdCalculate_Click(null,null);
				if(strAddOrModify == "0")
				{
					clsEvalInfoOfSIRSEvaluation objEvalInfo = m_GetCurrentEvalInfo();
					objEvalInfo.strActivityTime = m_strAutoEvalDate;
//					if(objDomain.lngAddNewRecordOfAutoEval(objEvalInfo)>0)
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
			this.txtTemperature.Text="";
			this.txtHeartRate.Text="";			
			this.txtBreath.Text="";			

			bool blnIsGE=m_blnCurrApparatus();

			if (!blnIsGE)
			{
				clsCMSData objCMSData=null;
				clsVentilatorData objVentilatorData=null;

                if (m_objCurrentPatient == null) return;
			

				//clsICUDataUtil objDomain=new clsICUDataUtil();
				string[] strTypeArry=new string[]{"TEMP1","HEARTRATE","RESPRATE"};//体温，心率，呼吸
				//m_mthGetICUDataByTime("",out objCMSData,out objVentilatorData,strTypeArry);
				m_mthGetICUDataByTime(p_strFindDate,out objCMSData,out objVentilatorData,strTypeArry);
				if (objCMSData!=null)
				{
					txtTemperature.Text=objCMSData.m_strTemp1;

					txtHeartRate.Text = objCMSData.m_strHeartRate; //m_strFormatShowParamData(objCMSData.m_strHeartRate); 

					txtBreath.Text= objCMSData.m_strRespDetectNum; //m_strFormatShowParamData(objCMSData.m_strRespDetectNum);

					//txtHeartRate.Text=objCMSData.m_strHeartRate.Length>0 ? objCMSData.m_strHeartRate.Substring(0,objCMSData.m_strHeartRate.IndexOf(".")) : "";
					//txtBreath.Text=objCMSData.m_strRespDetectNum.Length>0 ? objCMSData.m_strRespDetectNum.Substring(0,objCMSData.m_strRespDetectNum.IndexOf(".")) : "";
				}
				if (objVentilatorData!=null)
				{
					//txtBreath.Text=objVentilatorData.m_strRespRate.Substring(0,objVentilatorData.m_strRespRate.IndexOf("."));
				}
			}
			else
			{
				//clsGECMSData objGECMSData=null;
                m_mthGetMonitorParamGE();
                clsGECMSData objGECMSData = m_objGECMSData;
                //objGECMSData = m_objICUGESimulateGetData.M_objNumericParam;
                //if (objGECMSData == null)
                //    m_mthGetICUGEDataByTime(p_strFindDate, out objGECMSData);
				if (objGECMSData!=null)
				{
					this.txtTemperature.Text=objGECMSData.m_strTEMP1;

					if (objGECMSData.m_strHR == null || objGECMSData.m_strHR.Trim().Length ==0)
						this.txtHeartRate.Text="";
					else
						this.txtHeartRate.Text= objGECMSData.m_strHR;//m_strFormatShowParamData(objGECMSData.m_strHR);

					if (objGECMSData.m_strRR == null || objGECMSData.m_strRR.Trim().Length ==0)
						this.txtBreath.Text="";		
					else
						this.txtBreath.Text= objGECMSData.m_strRR; //m_strFormatShowParamData(objGECMSData.m_strRR);
				}
			}
		}

		/// <summary>
		/// alex 2002-9-29 修改getdata方法
		/// 使其从clsICUDataDomain 中取数据。
		/// </summary>
		private void GetData()
		{
			#region Old
//			try
//			{
//				XmlDocument objXMLDoc = new XmlDocument();
//
//				clsCMSData objCMSData=null;
//				clsVentilatorData objVentilatorData=null;
//				clsGECMSData objGECMSData=null;
//
//				bool blnIsGE=m_blnCurrApparatus();
//
//
//				//				objSelectedPatientInfo.m_ObjCurrentInHospitalInfo.m_ObjLastICUUtil.m_mthGetICUDataByTime(strSickBedNO,dtpStartSample.Value,out objCMSData,out objVentilatorData);
//
//				m_mthGetICUDataByTime(dtpStartSample.Value,out objCMSData,out objVentilatorData);
//
//				if (blnIsGE)
//					m_mthGetICUGEDataByTime(dtpStartSample.Value,out objGECMSData);
//
//				if (!blnIsGE)
//				{
//					if (objCMSData != null)
//					{
//						if(objCMSData.m_strHeartRate == null || objCMSData.m_strHeartRate == "")
//							txtHeartRate.Text = "";
//						else
//							txtHeartRate.Text = objCMSData.m_strHeartRate.Substring(0,objCMSData.m_strHeartRate.Length-3);
//
//						txtTemperature.Text = objCMSData.m_strTemp1;
//						//					lblCMSSampleTime.Text = objCMSData.m_strDataCollectedTime;
//					}
//				}
//				else
//				{
//					if (objGECMSData != null)
//					{
//						if(objGECMSData.m_strHR == null || objGECMSData.m_strHR == "")
//							txtHeartRate.Text = "";
//						else
//							txtHeartRate.Text = objGECMSData.m_strHR;
//
//						txtTemperature.Text = objGECMSData.m_strTEMP1;
//					}
//				}
//				if(objVentilatorData != null)
//				{
//					if(objVentilatorData.m_strRespRate == null || objVentilatorData.m_strRespRate == "")
//						txtBreath.Text = "";
//					else
//						txtBreath.Text = objVentilatorData.m_strRespRate.Substring(0,objVentilatorData.m_strRespRate.Length-3);
//					//					lblVenSampleTime.Text = objVentilatorData.m_strDataCollectedTime;
//				}
//			}
//			catch
//			{
//			}
			#endregion Old

			m_mthGetData(dtpStartSample.Value.ToString());
		}
		private void cmdGetData_Click(object sender, System.EventArgs e)
		{			
			if(m_objCurrentPatient == null)
			{
				clsPublicFunction.ShowInformationMessageBox("请输入床号！");
				return;
			}
			GetData();			
		}

		private void timAutoCollect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			
			try
			{
				dtpStartSample.Value = DateTime.Now;
                //int intTime = int.Parse(txtAutoTime.Text);
                //dtpStartSample.Value = dtpStartSample.Value.AddSeconds(intTime);
				GetData();
				cmdCalculate_Click(null,null);
				object [] res = dtlResult.Rows[0].ItemArray;
				ListViewItem item = new ListViewItem(new string[]{
																	 dtpStartSample.Value.ToString("yyyy-MM-dd HH:mm:ss"),
																	 (string)res[4],
																	 (string)res[0]+" 测定值："+txtBreath.Text+" ",
																	 (string)res[1]+" 测定值："+txtHeartRate.Text,
																	 (string)res[2]+" 测定值："+txtTemperature.Text,
																	 (string)res[3]+" 测定值："+txtBacillus.Text,
				});
				frmAutoResult.AddResult(item);
				m_AutoSave(dtpStartSample.Value.ToString("yyyy-MM-dd HH:mm:ss"));//alex 2002-9-29
			}
			catch
			{
			}
		}

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

		private void cmdStartAuto_Click(object sender, System.EventArgs e)
		{
			//Alex 2002-9-28 增加两个判断控制
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
				int intTime = int.Parse(txtAutoTime.Text);
				timAutoCollect.Interval = intTime*1000;
				cmdStartAuto.Enabled = false;
				txtAutoTime.Enabled = false;
				cmdStopAuto.Enabled = true;
                frmAutoResult.M_lblTitle = "自动评分";
				frmAutoResult.Visible = true;
				timAutoCollect.Start();
			}
			catch
			{
			}
		}

		private void cmdStopAuto_Click(object sender, System.EventArgs e)
		{
			timAutoCollect.Stop();
			cmdStartAuto.Enabled = true;
			txtAutoTime.Enabled = true;
			cmdStopAuto.Enabled = false;
		}

		private void cmdShowResult_Click(object sender, System.EventArgs e)
		{
            frmAutoResult.M_lblTitle = "查看结果";
            frmAutoResult.Visible = true;
		}

		private void SIRSEvaluation_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			frmAutoResult.Close();
		}
		#endregion


		private void SIRSEvaluation_Load(object sender, System.EventArgs e)
		{
			dtlResult.Columns.Add("呼吸频率");
			dtlResult.Columns.Add("心率");
			dtlResult.Columns.Add("体温");
			dtlResult.Columns.Add("白细胞计数和分类");
			dtlResult.Columns.Add("诊断结果");

			dtgResult.DataSource = dtlResult;

			dtlResult.Rows.Add(new string[]{"/","/","/","/","/"});
			
			lbltxtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;
			
			m_objHighLight.m_mthAddControlInContainer(this);
	
			//this.dtpEvalDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
			this.dtpEvalDate.m_mthResetSize();
			
		}


		#region interface
		#region null Impletement
		public void Print()
		{
			m_lngPrint(); 
		}
		public void Copy()
		{
			m_lngCopy();
		}

		public void Cut()
		{
			m_lngCut();
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


		public void Paste()
		{
			m_lngPaste();
		}
		#endregion

		public void Save()
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(lblSetionOffice.Tag.ToString(),PrivilegeData.enmPrivilegeSF.APACHEIIValuation,PrivilegeData.enmPrivilegeOperation.AddOrModify))
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

		public override void Delete()
		{
#if FunctionPrivilege
			if(!MDIParent.s_blnCheckCurrentPrivilege(lbltxtSetionOffice.Tag.ToString(),PrivilegeData.enmPrivilegeSF.SIRSEvaluation,PrivilegeData.enmPrivilegeOperation.Delete))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif

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
			ClearUp();
		}

		public override void Display()
		{
		
		}

		/// <summary>
		/// alex 2002-9-29
		/// 已经失效
		/// </summary>
		/// <param name="cardno"></param>
		/// <param name="ActivityTime"></param>
        public override void Display(string cardno, string ActivityTime)
		{
		}

		private void SetGroupSelText(GroupBox gpb,string text,string tabindex)
		{
			try
			{
				if(tabindex == "0")
				{
					rdbWBC.Checked = true;
					txtWBC.Text = text;
					rdbBacillus.Checked = false;
					txtBacillus.Text = "";
					return;
				}
				if(tabindex == "1")
				{
					rdbWBC.Checked = false;
					txtWBC.Text = "";
					rdbBacillus.Checked = true;
					txtBacillus.Text = text;
					return;
				}
				else
				{
					rdbWBC.Checked = false;
					txtWBC.Text = "";
					rdbBacillus.Checked = false;
					txtBacillus.Text = "";
					return;
				}
			}
			catch
			{}
		}
		
		#endregion

		#region clear up
		protected override void ClearUp()
		{
			try
			{
				object [] res = {"/","/","/","/","/"};
				dtlResult.Rows[0].ItemArray = res;

				foreach(Control control in this.Controls)
				{				
					string typeName = control.GetType().Name;
					if(typeName == "TextBox"&&control.Name!="txtCardNo")
					{
						control.Text = "";
						
					}
//					else if(typeName == "ListView")
//					{
//						((ListView)control).Items.Clear();
//					}
						//				else if(typeName == "DataGrid")
						//				{
						//					((System.Data.DataTable)((DataGrid)control).DataSource).Rows.Clear();
						//				}
					else if(typeName=="CheckBox")
					{
						((CheckBox)control).Checked=false;
					}
					else if(typeName=="dwtFlatComboBox")
					{
						control.Text="";
					}
				}
					
				foreach(Control control in this.gpbWBC.Controls)
				{				
					string typeName = control.GetType().Name;
					if(typeName == "TextBox"&&control.Name!="txtCardNo")
					{
						control.Text = "";
						
					}
				}
                txtAutoTime.Text = "60";
                lbltxtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;
				txtBreath.Text = "";
				txtHeartRate.Text = "";
				txtTemperature.Text = "";
				rdbWBC.Checked = true;
				txtWBC.Text = "";
				txtBacillus.Text = "";
				rdbAgeO5Day.Checked = true;
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
		#endregion

		#region Display
		protected override void m_mthDisplay()
		{
			long lngRes  = objDomain.m_lngGetSIRSValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out m_objSIRSEvaluationDB);

			if(m_objSIRSEvaluationDB == null)
				return;

			lbltxtEvalDoctor.Text = m_objSIRSEvaluationDB.strEvalDoctorName;
			this.intAge = int.Parse(m_objSIRSEvaluationDB.strAgeGroup);

			foreach(Control ctrl in gpbAge.Controls)
			{
				if(ctrl.GetType().Name == "RadioButton" && int.Parse(ctrl.Tag.ToString()) == intAge)
				{
					((RadioButton)ctrl).Checked = true;
					break;
				}								
			}

			this.txtBreath.Text = m_objSIRSEvaluationDB.strBreath;
			this.txtHeartRate.Text = m_objSIRSEvaluationDB.strHeartRate;
			this.txtTemperature.Text = m_objSIRSEvaluationDB.strTemperature;

			SetGroupSelText(gpbWBC,m_objSIRSEvaluationDB.strWBCorBacillus ,m_objSIRSEvaluationDB.strWBCorBacillusSel) ;

			object []res = dtlResult.Rows[0].ItemArray;

			res[0] = m_objSIRSEvaluationDB.strBreathEval;
			res[1] = m_objSIRSEvaluationDB.strHeartRateEval;
			res[2] = m_objSIRSEvaluationDB.strTemperatureEval;
			res[3] = m_objSIRSEvaluationDB.strWBCorBacillusEval;
			res[4] = m_objSIRSEvaluationDB.strTotalEval;

			dtlResult.Rows[0].ItemArray = res;
				
			dtpEvalDate.Value = DateTime.Parse(m_objSIRSEvaluationDB.strActivityTime);
            clsEmrEmployeeBase_VO objEmployee = null;
            clsBaseInfo.m_lngGetEmpByID(m_objSIRSEvaluationDB.strEvalDoctorID, out objEmployee);
			lbltxtEvalDoctor.Text = objEmployee.m_strLASTNAME_VCHR;

			strAddOrModify = "1";
		}

		private void trvInhospitalTime_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			try
			{
//				MDIParent.s_ObjSaveCue.m_mthHandleRecordAfterSelect(this);

				ClearUp();

				if(this.trvActivityTime.SelectedNode.Tag ==null || this.trvActivityTime.SelectedNode.Tag.ToString() == "0")
				{
                    //if(m_strInPatientID ==null || m_strInPatientID=="") 
                    //    MDIParent.m_mthChangeFormText(this,MDIParent.enmFormEditStatus.None);
                    //else
                    //    MDIParent.m_mthChangeFormText(this,MDIParent.enmFormEditStatus.AddNew);
				}
				else

				{
					m_strCreateDate = ((DateTime)trvActivityTime.SelectedNode.Tag).ToString("yyyy-MM-dd HH:mm:ss");
				
					m_mthDisplay();

					strAddOrModify = "1";
					
					//MDIParent.m_mthChangeFormText(this,MDIParent.enmFormEditStatus.Modify );
				}

				//记录设置窗体当前状态
				//MDIParent.s_ObjSaveCue.m_mthAddFormStatus(this);
			}
			catch
			{}
		}
		#endregion

		private void ctmClearUp_Click(object sender, System.EventArgs e)
		{
			ClearUp();
		}

		private void dtpBeginTime_evtValueChanged(object sender, System.EventArgs e)
		{
//			m_mthLoadAllRecordTimeOfAPatient(m_strInPatientID, m_strInPatientDate, dtpBeginTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),dtpEndTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
		}

		private void dtpEndTime_evtValueChanged(object sender, System.EventArgs e)
		{
//			m_mthLoadAllRecordTimeOfAPatient(m_strInPatientID, m_strInPatientDate, dtpBeginTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),dtpEndTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
		}

		#region 病人信息

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

		#endregion

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
			
			#region 赋值
			this.cmdCalculate_Click(null,null);

			clsEvalInfoOfSIRSEvaluation objEvalInfo = m_GetCurrentEvalInfo();

			#endregion

			if(m_strCreateDate != "")
			{
                //if(m_objCurrentContext.m_ObjControl.m_enmModifyCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
                //    == enmDBControlCheckResult.Disable)
                //{
                //    clsPublicFunction.s_mthShowNotPermitMessage();
                //    return 0;

                //}
				clsEvalInfoOfSIRSEvaluation objTemp;
				long lngExist = objDomain.m_lngGetSIRSValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out objTemp);

				if(lngExist == 0)
					return -11;
				
				if(lngExist == 1)
				{
                    //if(DateTime.Parse(objTemp.strModifyDate) != DateTime.Parse(m_objSIRSEvaluationDB.strModifyDate))
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

				clsEvalInfoOfSIRSEvaluation objTemp;
				long lngExist = objDomain.m_lngGetSIRSValue(m_strInPatientID,m_strInPatientDate,dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),out objTemp);
				
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

		private clsEvalInfoOfSIRSEvaluation m_GetCurrentEvalInfo()
		{
			clsEvalInfoOfSIRSEvaluation objEvalInfo = new clsEvalInfoOfSIRSEvaluation();

			objEvalInfo.strPatientID = m_strInPatientID;
			objEvalInfo.strInPatientDate = m_strInPatientDate;
			objEvalInfo.strActivityTime = (m_strCreateDate == "") ? this.dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : m_strCreateDate;

			objEvalInfo.strEvalDoctorID = clsBaseInfo.LoginEmployee.m_strEMPID_CHR;
			objEvalInfo.strEvalDoctorName = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;

			objEvalInfo.strAgeGroup = intAge.ToString();
			objEvalInfo.strBreath = "$$" + txtBreath.Text.Trim();
			objEvalInfo.strHeartRate = "$$" + txtHeartRate.Text.Trim();
			objEvalInfo.strTemperature = "$$" + txtTemperature.Text.Trim();
			if(rdbWBC.Checked)
			{
				objEvalInfo.strWBCorBacillus = "$$" + txtWBC.Text.Trim();
				objEvalInfo.strWBCorBacillusSel = "0";
			}
			if(rdbBacillus.Checked)
			{
				objEvalInfo.strWBCorBacillus = "$$" + txtBacillus.Text.Trim();
				objEvalInfo.strWBCorBacillusSel = "1";
			}
			if((!rdbWBC.Checked) && (!rdbBacillus.Checked))
			{
				objEvalInfo.strWBCorBacillus = "";
				objEvalInfo.strWBCorBacillusSel = "";
			}
			objEvalInfo.strBreathEval = "$$" +dtlResult.Rows[0].ItemArray[0].ToString();
			objEvalInfo.strHeartRateEval = "$$" + dtlResult.Rows[0].ItemArray[1].ToString();
			objEvalInfo.strTemperatureEval = "$$" + dtlResult.Rows[0].ItemArray[2].ToString();
			objEvalInfo.strWBCorBacillusEval = "$$" + dtlResult.Rows[0].ItemArray[3].ToString();
			objEvalInfo.strTotalEval = "$$" + dtlResult.Rows[0].ItemArray[4].ToString();

			return objEvalInfo;
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

		private void m_cmdGetDovueData_Click(object sender, System.EventArgs e)
		{
			#region Old
//			if(m_strInPatientID==null || m_strInPatientID=="" || m_strInPatientDate==null|| m_strInPatientDate=="")return;
//
//
//			this.txtTemperature.Text="";
//			this.txtHeartRate.Text="";			
//			this.txtBreath.Text="";			
//
//			clsTrendDomain objDomain=new clsTrendDomain();
//			string[] strEMFC_IDArr=new string[]{"100","40","92"};//体温，心率，呼吸
//			string[] strResultArr;
//			long lngRes=objDomain.m_lngGetDocvueResultArr(m_strInPatientID,DateTime.Parse(m_strInPatientDate),strEMFC_IDArr,dtpEvalDate.Value,out strResultArr);
//			if(lngRes<=0)
//			{
//				switch(lngRes)
//				{
//					case (long)(iCareData.enmOperationResult.Not_permission) :
//						clsPublicFunction.s_mthShowNotPermitMessage();break;
//					case (long)(iCareData.enmOperationResult.DB_Fail) :
//						clsPublicFunction.ShowInformationMessageBox("数据库连接失败");break;
//				}
//			}
//			else 
//			{
//				this.txtTemperature.Text=strResultArr[0];
//				this.txtHeartRate.Text=strResultArr[1];
//				this.txtBreath.Text=strResultArr[2];				
//			}
			#endregion Old

			#region Old
//			this.txtTemperature.Text="";
//			this.txtHeartRate.Text="";			
//			this.txtBreath.Text="";			
//
//			bool blnIsGE=m_blnCurrApparatus();
//
//			if (!blnIsGE)
//			{
//				clsCMSData objCMSData=null;
//				clsVentilatorData objVentilatorData=null;
//
//				if(m_strInPatientID==null || m_strInPatientID=="" || m_strInPatientDate==null|| m_strInPatientDate=="")return;
//			
//
//				//clsICUDataUtil objDomain=new clsICUDataUtil();
//				string[] strTypeArry=new string[]{"TEMP1","HEARTRATE","RESPRATE"};//体温，心率，呼吸
//				m_mthGetICUDataByTime("",out objCMSData,out objVentilatorData,strTypeArry);
//				if (objCMSData!=null)
//				{
//					txtTemperature.Text=objCMSData.m_strTemp1;
//					txtHeartRate.Text=objCMSData.m_strHeartRate;
//				}
//				if (objVentilatorData!=null)
//				{
//					txtBreath.Text=objVentilatorData.m_strRespRate;
//				}
//			}
//			else
//			{
//				clsGECMSData objGECMSData=null;
//				m_mthGetICUGEDataByTime(dtpStartSample.Value,out objGECMSData);
//			}
			#endregion Old

			m_mthGetData("");
		}

		#region Print Function

		public override void m_mthSetPrint()
		{
			clsEvalInfoOfSIRSEvaluation objValue;
			objPrintTool=new clsSIRS_EvaluationPrintTool();
			objPrintTool.m_mthInitPrintTool(null);	
			if(m_objCurrentPatient == null)
				objPrintTool.m_mthSetPrintInfo(null,null,DateTime.MinValue);
			else
			{
				if(this.trvActivityTime.SelectedNode ==null || this.trvActivityTime.SelectedNode==trvActivityTime.Nodes[0] || trvActivityTime.SelectedNode.Tag==null)
					objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient,null,DateTime.MinValue);
				else 
				{
					objDomain.m_lngGetSIRSValue(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),trvActivityTime.SelectedNode.Tag.ToString(),out objValue);
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
