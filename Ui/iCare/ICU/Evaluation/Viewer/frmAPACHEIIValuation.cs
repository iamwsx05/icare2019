//#define FunctionPrivilege
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.Data;
using com.digitalwave.Utility;
using com.digitalwave.Utility.Controls;
using weCare.Core.Entity;
//using iCare.ICU.Espial;
//using iCare.Common;
using com.digitalwave.Emr.Signature_gui;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// Summary description for ApA.
	/// //been 
	/// modify by Alex 
	/// 增加自动评分功能
	/// </summary>
	public partial class frmAPACHEIIValuation : frmValuationBaseForm,PublicFunction
	{
        private DataTable dtlResult;

        //定义签名类
        private clsEmrSignToolCollection m_objSign = new clsEmrSignToolCollection();


        public frmAPACHEIIValuation()
        {            
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            dtlResult = new DataTable("result");

            m_objBorderTool = new com.digitalwave.Utility.Controls.clsBorderTool(Color.White);
            m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{
																			 trvActivityTime,dtgResult																			 
																		 });

            this.cboMainReasonNoIn.AddRangeItems(new object[] {
																  "选择类型",
																  "神经系统",
																  "心血管系统",
																  "胃肠道",
																  "代谢/肾脏"});
            this.cboNoInRange.AddRangeItems(new object[] {
															 "选择类型",
															 "代谢/肾脏",
															 "呼吸系统",
															 "神经系统",
															 "心血管系统",
															 "胃肠道"});
            this.cboOthers.AddRangeItems(new object[] {
														  "选择类型",
														  "药物过量",
														  "糖尿病性酮症酸中毒",
														  "消化道出血"});
            this.cboNeurotic.AddRangeItems(new object[] {
															"选择类型",
															"癫痫病",
															"ICH/SDH/SAH"});
            this.cboHurts.AddRangeItems(new object[] {
														 "选择类型",
														 "多发伤",
														 "头部创伤"});
            this.cboPatientUnOp2.AddRangeItems(new object[] {
																"选择类型",
																"高血压",
																"心律失常",
																"充血性心衰",
																"出血性休克/低血容量",
																"冠状动脉疾病",
																"全身感染",
																"心跳骤停",
																"心源性休克",
																"胸/腹主动脉瘤破裂"});
            this.cboPatientUnOp1.AddRangeItems(new object[] {
																"选择类型",
																"哮喘/过敏症",
																"COPD",
																"非心源性肺水肿",
																"呼吸暂停",
																"误吸/中毒/毒性反应",
																"肺栓塞",
																"感染",
																"肿瘤"});
            this.cboPatientSelOp.AddRangeItems(new object[] {
																"选择类型",
																"多法伤",
																"因慢性心血管疾病住ICU",
																"外周血管手术",
																"心脏瓣膜手术",
																"颅内肿瘤手术",
																"肾脏肿瘤手术",
																"肾移植术",
																"颅脑外伤手术",
																"胸腔肿瘤手术",
																"ICH/SDH/SAH手术",
																"椎板切除术及其他脊椎手术",
																"出血性休克",
																"胃肠道出血",
																"胃肠道肿瘤手术",
																"手术后呼吸功能不全",
																"胃肠道穿孔/梗阻"});

            m_cboOpenEyes.AddRangeItems(new string[] { "无", "痛刺激", "呼唤睁眼", "自发" });
            m_cboSay.AddRangeItems(new string[] { "无", "不理解", "不适当", "回答混乱", "定向正确" });
            m_cboSport.AddRangeItems(new string[] { "无", "痛刺伸直", "痛刺屈曲", "屈曲回避", "定位疼痛", "服从命令" });

            ColumnHeader chResult1 = new ColumnHeader();
            chResult1.Text = "直肠温度";
            chResult1.Width = 100;

            ColumnHeader chResult2 = new ColumnHeader();
            chResult2.Text = "平均动脉压";
            chResult2.Width = 120;

            ColumnHeader chResult3 = new ColumnHeader();
            chResult3.Text = "心率";
            chResult3.Width = 50;

            ColumnHeader chResult4 = new ColumnHeader();
            chResult4.Text = "白细胞计数";
            chResult4.Width = 120;

            ColumnHeader chResult5 = new ColumnHeader();
            chResult5.Text = "静脉血";
            chResult5.Width = 80;

            ColumnHeader chResult6 = new ColumnHeader();
            chResult6.Text = "呼吸频率";
            chResult6.Width = 100;

            ColumnHeader chResult7 = new ColumnHeader();
            chResult7.Text = "PaO2";
            chResult7.Width = 50;

            ColumnHeader chResult8 = new ColumnHeader();
            chResult8.Text = "（A－a）DO";
            chResult8.Width = 100;

            ColumnHeader chResult9 = new ColumnHeader();
            chResult9.Text = "FiO2";
            chResult9.Width = 50;

            ColumnHeader chResult10 = new ColumnHeader();
            chResult10.Text = "血肌酐浓度";
            chResult10.Width = 120;

            ColumnHeader chResult11 = new ColumnHeader();
            chResult11.Text = "动脉血pH";
            chResult11.Width = 100;

            ColumnHeader chResult12 = new ColumnHeader();
            chResult12.Text = "血钠浓度";
            chResult12.Width = 100;

            ColumnHeader chResult13 = new ColumnHeader();
            chResult13.Text = "血钾浓度";
            chResult13.Width = 100;

            ColumnHeader chResult14 = new ColumnHeader();
            chResult14.Text = "GCS";
            chResult14.Width = 50;

            ColumnHeader chResult15 = new ColumnHeader();
            chResult15.Text = "血细胞比容";
            chResult15.Width = 120;

            ColumnHeader chResult16 = new ColumnHeader();
            chResult16.Text = "危险性";
            chResult16.Width = 100;

            frmAutoResult = new frmAutoEvalResult(chResult16, chResult1, chResult2, chResult3, chResult4, chResult5, chResult6, chResult7, chResult8, chResult9, chResult10, chResult11, chResult12, chResult13, chResult14, chResult15);

            frmAutoResult.Text = "APACHEII自动诊断";

            frmAutoResult.Owner = this;

            //			frmAutoResult.Show();
            frmAutoResult.Visible = false;


            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            m_mthSetQuickKeys();

            m_objHighLight = new ctlHighLightFocus(clsHRPColor.s_ClrHightLight);

            //			this.tabAPACHEIIValuation.Controls.Remove(this.tabPage3);

            //签名常用值            

        }
        protected ctlHighLightFocus m_objHighLight;
        private frmAutoEvalResult frmAutoResult;

		#region interface
		public override void Redo()
		{
		
		}

        public override void Verify()
		{
			////long lngRes=m_lngSignVerify(p_strFormID,p_strRecordID);
		}


        public override void Undo()
		{
		
		}
        public override void Cut()
		{
			m_lngCut();
		}

        public override void Paste()
		{
			m_lngPaste();
		}
        public override void Copy()
		{
			m_lngCopy();
		}

        public override void Delete()
		{	
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(lblSetionOffice.Tag.ToString(),PrivilegeData.enmPrivilegeSF.APACHEIIValuation,PrivilegeData.enmPrivilegeOperation.AddOrModify))
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

        public override void Display() { }
        public override void Display(string cardno, string ActivityTime)
		{
			
		}

		private int KidneyProstrate=0;
		
		private int PatientUnOp1=0;
		private int PatientUnOp2=0;

		private int Others=0;

		private int Neurotic=0;
		private int NoInRange=0;
		private int Hurts=0;
		private int PatientAfterEmergency=0;
		private int MainReasonNoIn=0;
		private void convert()
		{
			try
			{
			
				if(chkKidneyProstrate.Checked) 
					KidneyProstrate=1;
				else 
					KidneyProstrate=0;
				if(chkPatientUnOp1.Checked) 
					PatientUnOp1=1;
				else 
					PatientUnOp1=0;
				if(chkPatientUnOp2.Checked) 
					PatientUnOp2=1;
				else 
					PatientUnOp2=0;
				if(chkOthers.Checked) 
					Others=1;
				else 
					Others=0;
				if(chkNeurotic.Checked) 
					Neurotic=1;
				else 
					Neurotic=0;
				if(chkNoInRange.Checked) 
					NoInRange=1;
				else 
					NoInRange=0;
				if(chkHurts.Checked) 
					Hurts=1;
				else 
					Hurts=0;

				if(chkPatientAfterEmergency.Checked) 
					PatientAfterEmergency=1;
				else 
					PatientAfterEmergency=0;
				if(chkMainReasonNoIn.Checked) 
					MainReasonNoIn=1;
				else 
					MainReasonNoIn=0;

			}
			catch{}


		}

        public override void Print()
		{
			m_lngPrint(); 
		}
		
		public override long m_lngSubSave()
		{
			return m_lngSaveWithMessageBox();
		}

        public override void Save()
		{
#if FunctionPrivilege
			if(!MDIParent.s_blnCheckCurrentPrivilege(lbltxtSetionOffice.Tag.ToString(),PrivilegeData.enmPrivilegeSF.APACHEIIValuation,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{

				        m_lngSubSave();
				
			}
			catch
			{
			}
		}

		protected override void m_mthLoadAllRecordTimeOfAPatient(string p_strPatientID,string p_strPatientDate,string p_strFromDate, string p_strToDate)
		{
			if(p_strPatientID ==null || p_strPatientID =="") 
				return ;

			this.trvActivityTime.Nodes[0].Nodes.Clear();

			DateTime [] m_dtmArr = objDomain.m_dtmGetTimeInfoOfAPatientArr(p_strPatientID ,p_strPatientDate, p_strFromDate, p_strToDate);

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
//			this.trvActivityTime_AfterSelect(this.trvActivityTime,new TreeViewEventArgs(trvActivityTime.Nodes[0]));
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

		private APACHEIIValuationDB m_objGetCurrentEvalInfo()
		{
			string str = "$$";

			APACHEIIValuationDB objAPACHEIIValuationDB=new APACHEIIValuationDB();

			objAPACHEIIValuationDB.strPatientID = m_strInPatientID;
			objAPACHEIIValuationDB.strInPatientDate = m_strInPatientDate;
			objAPACHEIIValuationDB.strActivityTime= (m_strCreateDate == "") ? this.dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : m_strCreateDate;

			objAPACHEIIValuationDB.strEvalDoctorID=clsBaseInfo.LoginEmployee.m_strEMPID_CHR;				
			objAPACHEIIValuationDB.strAgeGroup=this.intAge.ToString();
			objAPACHEIIValuationDB.strTemperature=str + this.txtTemperature.Text;
			objAPACHEIIValuationDB.strAdvArteryPress=str + this.txtAdvArteryPress.Text;
			objAPACHEIIValuationDB.strHeartRate=str + this.txtHR.Text;
			objAPACHEIIValuationDB.strAmountLeucocyte=str + this.txtAmountLeucocyte.Text;
			objAPACHEIIValuationDB.strFiO2=str + this.txtFiO2.Text;
			objAPACHEIIValuationDB.strBreath=str + this.txtBreath.Text;
			objAPACHEIIValuationDB.strPao2=str + this.txtPao2.Text;
			objAPACHEIIValuationDB.strDo2=str + this.txtDo2.Text;
			objAPACHEIIValuationDB.strArteryBloodpH=str + this.txtArteryBloodpH.Text;
			objAPACHEIIValuationDB.strBloodNa=str + this.txtBloodNa.Text;
			objAPACHEIIValuationDB.strBloodK=str + this.txtBloodK.Text;
			objAPACHEIIValuationDB.strGCS = str + m_cboOpenEyes.SelectedIndex + c_strSpecialSymbol + m_cboSay.SelectedIndex + c_strSpecialSymbol + m_cboSport.SelectedIndex;
			objAPACHEIIValuationDB.strBloodFlesh=str + this.txtBloodFlesh.Text;
			objAPACHEIIValuationDB.strBloodCorpuscle=str + this.txtBloodCorpuscle.Text;			
			objAPACHEIIValuationDB.strHCO=str + this.txtHCO.Text;
			objAPACHEIIValuationDB.strKidneyProstrate=str + this.KidneyProstrate.ToString();
			objAPACHEIIValuationDB.strHealthGroup=str + this.intHealth.ToString();
			objAPACHEIIValuationDB.strPatientUnOp1=str + this.PatientUnOp1.ToString();
			objAPACHEIIValuationDB.strPatientUnOp2=str + this.PatientUnOp2.ToString();
			objAPACHEIIValuationDB.strOthers=str + this.Others.ToString();
			objAPACHEIIValuationDB.strNeurotic=str + this.Neurotic.ToString();
			objAPACHEIIValuationDB.strNoInRange=str + this.NoInRange.ToString();
			objAPACHEIIValuationDB.strHurts=str + this.Hurts.ToString();
			
			objAPACHEIIValuationDB.strPatientUnOp1Eval=this.cboPatientUnOp1.Text;
			objAPACHEIIValuationDB.strPatientUnOp2Eval=this.cboPatientUnOp2.Text;
			objAPACHEIIValuationDB.strOthersEval=this.cboOthers.Text;
			objAPACHEIIValuationDB.strNeuroticEval=this.cboNeurotic.Text;
			objAPACHEIIValuationDB.strNoInRangeEval=this.cboNoInRange.Text;
			objAPACHEIIValuationDB.strHurtsEval=this.cboHurts.Text;
			objAPACHEIIValuationDB.strPatientAfterEmergency=str + this.PatientAfterEmergency.ToString();
			objAPACHEIIValuationDB.strMainReasonNoIn=str + this.PatientAfterEmergency.ToString();
			objAPACHEIIValuationDB.strMainReasonNoInEval=str + this.cboMainReasonNoIn.Text;
			objAPACHEIIValuationDB.strPatientSelOpEval=str + this.cboPatientSelOp.Text;	
			objAPACHEIIValuationDB.strPaCO2=str + this.txtPaCO2.Text;	
			
			objAPACHEIIValuationDB.strTemperatureEval=str + this.dtlResult.Rows[0].ItemArray[0].ToString();
			objAPACHEIIValuationDB.strAdvArteryPressEval=str + this.dtlResult.Rows[0].ItemArray[1].ToString();
			objAPACHEIIValuationDB.strHeartRateEval=str + this.dtlResult.Rows[0].ItemArray[2].ToString();
			objAPACHEIIValuationDB.strAmountLeucocyteEval=str + this.dtlResult.Rows[0].ItemArray[3].ToString();
			objAPACHEIIValuationDB.strBreathEval=str + this.dtlResult.Rows[0].ItemArray[4].ToString();
			objAPACHEIIValuationDB.strPao2Eval=str + this.dtlResult.Rows[0].ItemArray[5].ToString();
			objAPACHEIIValuationDB.strDo2Eval=str + this.dtlResult.Rows[0].ItemArray[6].ToString();
			objAPACHEIIValuationDB.strArteryBloodpHEval=str + this.dtlResult.Rows[0].ItemArray[7].ToString();
			objAPACHEIIValuationDB.strBloodNaEval=str + this.dtlResult.Rows[0].ItemArray[8].ToString();
			objAPACHEIIValuationDB.strBloodKEval=str + this.dtlResult.Rows[0].ItemArray[9].ToString();
			objAPACHEIIValuationDB.strGCSEval=str + this.dtlResult.Rows[0].ItemArray[10].ToString();
			objAPACHEIIValuationDB.strBloodFleshEval=str + this.dtlResult.Rows[0].ItemArray[11].ToString();
			objAPACHEIIValuationDB.strBloodCorpuscleEval=str + this.dtlResult.Rows[0].ItemArray[12].ToString();
			objAPACHEIIValuationDB.strHCOEval=str + this.dtlResult.Rows[0].ItemArray[13].ToString();
			
			objAPACHEIIValuationDB.strTotalEval=str + this.dtlResult.Rows[0].ItemArray[14].ToString();
			objAPACHEIIValuationDB.strREval=str + this.dtlResult.Rows[0].ItemArray[15].ToString();

			return objAPACHEIIValuationDB;
		}
 
		private long m_lngSaveWithoutMessageBox()
		{
			if(m_strInPatientID == null || m_strInPatientID == "")
			{
				clsPublicFunction.ShowInformationMessageBox("对不起，请输入病人住院编号！");
				return 0;
			}

			string strCurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

			convert();

			this.cmdCalculate_Click(null,null);

			APACHEIIValuationDB objAPACHEIIValuationDB = m_objGetCurrentEvalInfo();

			this.cmdCalculate_Click(null,null);

			if(m_strCreateDate != "")
			{
                //if(m_objCurrentContext.m_ObjControl.m_enmModifyCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
                //    == enmDBControlCheckResult.Disable)
                //{
                //    clsPublicFunction.s_mthShowNotPermitMessage();
                //    return 0;
                //}

				APACHEIIValuationDB objTemp;
				long lngExist = objDomain.m_lngGetApacheIIValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out objTemp);

				if(lngExist == 0)
					return -11;
				
				if(lngExist == 1)
				{
                    //if(DateTime.Parse(objTemp.strModifyDate) != DateTime.Parse(m_objAPACHEIIValuationDB.strModifyDate))
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

				APACHEIIValuationDB objTemp;
				long lngExist = objDomain.m_lngGetApacheIIValue(m_strInPatientID,m_strInPatientDate,dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),out objTemp);
				
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

			long lngRes = objDomain.m_lngSave(objAPACHEIIValuationDB);
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

		#region InnerControl
		public void ReceiveID(string strID)
		{
			if(strID.Trim().Length !=0)
			{				
				m_mthDisplay();
			}
		}

		private int intAge=0;	
		private void AgeGroupChanged(object sender, System.EventArgs e)
		{
			try
			{
				intAge = int.Parse((string)((RadioButton)sender).Tag);
			}
			catch
			{}
		}
		private int intHealth=0;
		private void HealthGroupChanged(object sender, System.EventArgs e)
		{
			try
			{
				intHealth=int.Parse((string)((RadioButton)sender).Tag);
			}
			catch
			{}
		}
		private double ICUEval()
		{
			double dblicu=0;
			try
			{
				
				double val=0;

				double[] PatientUnOp1=new double[]{0,-2.108,-0.367,-0.251,-0.168,-0.142,-0.128,0,0.891};		
				double[] PatientUnOp2=new double[]{0,-1.798,-1.368,-0.424,0.493,-0.191,0.113,0,393,-0.259,0.731};
				double[] Hurts=new double[]{0,-1.228,0.517};
				double[] Neurotic=new double[]{0,-0.584,0.723};
				double[] Others=new double[]{0,-3.353,-1.507,0.334};
				double[] NoInRange=new double[]{0,-0.885,0.890,-.759,0.470,0.501};
				double[] MainReasonNoIn=new double[]{0,-1.150,-0.797,-0.610,-0.613,-0.196};
				double[] PatientSelOp=new double[]{0,-1.684,-1.376,-1.315,-1.261,-1.245,-1.204,-1.042,-0.995,-0.802,-0.788,-0.699,-0.682,-0.617,-0.248,-0.140,0.60};

				if(intHealth==0)
				{
					if(chkPatientUnOp1.Checked)
					{
						cboPatientUnOp1.Enabled=true;
						val=PatientUnOp1[cboPatientUnOp1.SelectedIndex];
					
						dblicu+=val;
					
			
					}

					if(chkPatientUnOp2.Checked)
					{
						cboPatientUnOp2.Enabled=true;
						val=PatientUnOp2[cboPatientUnOp2.SelectedIndex];
						dblicu+=val;
				
			
					}


					if(chkHurts.Checked)
					{
						cboHurts.Enabled=true;
						val=Hurts[cboHurts.SelectedIndex];
						dblicu+=val;

				
					}

					if(chkNeurotic.Checked)
					{
						cboNeurotic.Enabled=true;
						val=Neurotic[cboNeurotic.SelectedIndex];
						dblicu+=val;
				 
					}


					if(chkOthers.Checked)
					{
						cboOthers.Enabled=true;
						val=Others[cboOthers.SelectedIndex];
						dblicu+=val;
				 
					}


					if(chkNoInRange.Checked)
					{
						cboNoInRange.Enabled=true;
						val=NoInRange[cboNoInRange.SelectedIndex];
						dblicu+=val;
			 
					}
	

				

				}
				else if(intHealth==1)
				{
			
					cboPatientSelOp.Enabled=true;	
					val=PatientSelOp[cboPatientSelOp.SelectedIndex];
					dblicu+=val;
		 
			
					if(cboPatientSelOp.SelectedIndex!=0&&intHealth==1)
					{
						dblicu-=PatientSelOp[cboPatientSelOp.SelectedIndex];
					}
					if(chkMainReasonNoIn.Checked)
					{
						
						val=MainReasonNoIn[cboMainReasonNoIn.SelectedIndex];
						dblicu+=val;
 
					}


				
				}
			}
			catch{}
			return dblicu;
		}
	

		private void Try()
		{
			try
			{
				dblTemperature=double.Parse(this.txtTemperature.Text);
			}
			catch
			{
				dblTemperature=-1;
			}
			try
			{
				dblAdvArteryPress=double.Parse(txtAdvArteryPress.Text);
			}
			catch
			{
				dblAdvArteryPress=-1;
			}

			try
			{
				dblHR=double.Parse(this.txtHR.Text);
			}
			catch
			{
				dblHR=-1;
			}
			try
			{
				dblAmountLeucocyte=double.Parse(txtAmountLeucocyte.Text);
			}
			catch
			{
				dblAmountLeucocyte=-1;
			}
			try
			{
				
				dblBreath=double.Parse(this.txtBreath.Text);	
			}
			catch
			{
				dblBreath=-1;
			}
			try
			{
				dblPao2=double.Parse(txtPao2.Text);
			}
			catch
			{
				dblPao2=-1;
			}
			try
			{


				dblDo2=double.Parse(txtDo2.Text);
			}
			catch
			{
				dblDo2=-1;
			}
			try
			{

				dblFiO2=double.Parse(txtFiO2.Text);
			}
			catch
			{
				dblFiO2=-1;
			}
			try
			{
				dblArteryBloodpH=double.Parse(txtArteryBloodpH.Text);
			}
			catch
			{
				dblArteryBloodpH=-1;
			}
			try
			{

				dblBloodNa=double.Parse(txtBloodNa.Text);
			}
			catch
			{
				dblBloodNa=-1;
			}
			try
			{
				dblBloodK=double.Parse(txtBloodK.Text);
			}
			catch
			{
				dblBloodK=-1;
			}
			try
			{
				dblGCS = m_cboOpenEyes.SelectedIndex + m_cboSay.SelectedIndex + m_cboSport.SelectedIndex + 3;
			}
			catch
			{
				dblGCS=-1;
			}
			try
			{
				dblBloodFlesh = (double.Parse(txtBloodFlesh.Text)) * 38.67;
			}
			catch
			{
				dblBloodFlesh=-1;
			}
			try
			{
				dblBloodCorpuscle=double.Parse(txtBloodCorpuscle.Text);	
			}	
			catch
			{
				dblBloodCorpuscle=-1;

				  
			}
			try
			{
				dblHCO=double.Parse(txtHCO.Text);
			}
			catch
			{
				dblHCO=-1;
			}
		}	

	
		private void cmdCalculate_Click(object sender, System.EventArgs e)
		{	
			try
			{
				double val = 0;
				double total = 0;
				double dblR=0;
				object [] res = dtlResult.Rows[0].ItemArray;
				//			

				Try();

				if(dblTemperature >= 0)
				{
					if(dblTemperature>=41||dblTemperature<=20.9)
						val=4;
					else if((dblTemperature>=39&&dblTemperature<=40.9)||(dblTemperature>=30&&dblTemperature<=31.9))
						val=3;
					else if(dblTemperature>=32&&dblTemperature<=33.9)
						val=2;
					else if((dblTemperature>=38.5&&dblTemperature<=38.9)||(dblTemperature>=34&&dblTemperature<=35.9))
						val=1;
					else 
						val=0;
					res[0] = val.ToString();
					total+=val;
				}
				else
				{
					res[0] = "/";
				}


				if(dblAdvArteryPress >= 0)
				{
					if(dblAdvArteryPress>=160||dblAdvArteryPress<=49)
						val=4;
					else if(dblAdvArteryPress>=130&&dblAdvArteryPress<=159)
						val=3;
					else if((dblAdvArteryPress>=110)&&(dblAdvArteryPress<=129)||(dblAdvArteryPress>=50&&dblAdvArteryPress<=69))
						val=2;
					else
						val=0;
				
					res[1] = val.ToString();
					total+=val;
				}
				else
				{
					res[1] = "/";
				}

				//
				if(dblHR >= 0)
				{
					if(dblHR>=180||dblHR<=39)
						val=4;
					else if((dblHR>=140&&dblHR<=179)||(dblHR>=40)&&(dblHR<=54))
						val=3;
					else if((dblHR>=110&&dblHR<=139)||(dblHR>=55&&dblHR<=69))
						val=2;
					else 
						val=0;
				
					res[2] = val.ToString();
					total+=val;
				}
				else
				{
					res[2] = "/";
				}
				//
				if(dblBreath >= 0)
				{
					if(dblBreath>=50||dblBreath<=5)
						val=4;
					else if(dblBreath>=35&&dblBreath<=49)
						val=3;
					else if(dblBreath>=6&&dblBreath<=9)
						val=2;
					else if((dblBreath>=25&&dblBreath<=34)||(dblBreath>=10&&dblBreath<=11))
						val=1;
					else 
						val=0;
				
					res[4] = val.ToString();
					total+=val;
				}
				else
				{
					res[4] = "/";
				}


				if(dblFiO2<0.5)
				{
					if(dblPao2 >= 0)
					{
						if(dblPao2<55)
							val=4;
						else if(dblPao2>=55&&dblPao2<=60)
							val=3;
						else if(dblPao2>=61&&dblPao2<=70)
							val=1;
						else 
							val=0;
					
						res[5] = val.ToString();
						total+=val;
						res[6]="/";
					}
					else
					{
						res[5] = "/";
						res[6]="/";
					}
				}
				else
				{
					if(dblDo2 >=0)
					{
						if(dblDo2>=500)
							val=4;
						else if(dblDo2>=350&&dblDo2<=499)
							val=3;
						else if(dblDo2>=200&&dblDo2<=349)
							val=2;
						else 
							val=0;
					
						res[6] = val.ToString();
						total+=val;
						res[5]="/";
					}
					else
					{
						res[5] = "/";
						res[6]="/";
					}
				}
			
				if(dblArteryBloodpH >= 0)
				{
					if(dblArteryBloodpH>=7.7||dblArteryBloodpH<=7.15)
						val=4;
					else if((dblArteryBloodpH>=7.6&&dblArteryBloodpH<=7.69)||(dblArteryBloodpH>=7.15&&dblArteryBloodpH<=7.24))
						val=3;
					else if(dblArteryBloodpH>=7.25&&dblArteryBloodpH<=7.32)
						val=2;
					else if(dblArteryBloodpH>=7.5&&dblArteryBloodpH<=7.59)
						val=1;
					else 
						val=0;
				
					res[7] = val.ToString();
					total+=val;
				}
				else
				{
					res[7] = "/";
				}

				if(dblBloodNa >= 0)
				{
					if(dblBloodNa>=180||dblBloodNa<=110)
						val=4;
					else if((dblBloodNa>=160&&dblBloodNa<=179)||(dblBloodNa>111&&dblBloodNa<=119))
						val=3;
					else if((dblBloodNa>=155&&dblBloodNa<=159)||(dblBloodNa>=120&&dblBloodNa<=129))
						val=2;
					else if(dblBloodNa>=150&&dblBloodNa<=154)
						val=1;
					else val=0;
				
					res[8] = val.ToString();
					total+=val;
				}
				else
				{
					res[8] = "/";
				}


				if(dblBloodK >= 0)
				{
					if(dblBloodK>=7||dblBloodK<=2.5)
						val=4;
					else if(dblBloodK>=6&&dblBloodK<=6.9)
						val=3;
					else if(dblBloodK>=2.5&&dblBloodK<=2.9)
						val=2;
					else if((dblBloodK>=5.5&&dblBloodK<=5.9)||(dblBloodK<=3.4&&dblBloodK>=3))
						val=1;
					else val=0;
				
					res[9] = val.ToString();
					total+=val;
				}
				else
				{
					res[9] = "/";
				}

				if(dblBloodFlesh >= 0)
				{
					if(dblBloodFlesh>=3.5)
						val=4;
					else if(dblBloodFlesh>=2&&dblBloodFlesh<=3.4)
						val=3;
					else if((dblBloodFlesh>=1.5&&dblBloodFlesh<=1.9)||(dblBloodFlesh<0.6))
						val=2;
					else val=0;
					if(chkKidneyProstrate.Checked)
						val*=2;
					res[11] = val.ToString();
					total+=val;
				}
				else
				{
					res[11] = "/";
				}

				if(dblBloodCorpuscle >= 0)
				{
					if(dblBloodCorpuscle>=60||dblBloodCorpuscle<20)
						val=4;
					else if((dblBloodCorpuscle>=50&&dblBloodCorpuscle<=59.9)||(dblBloodCorpuscle>=20&&dblBloodCorpuscle<=29.9))
						val=2;
					else if(dblBloodCorpuscle>=46&&dblBloodCorpuscle<=46.9)
						val=1;
					else val=0;
					res[12] = val.ToString();
					total+=val;
				}
				else
				{
					res[12] = "/";
				}


				if(dblAmountLeucocyte >= 0)
				{
					if(dblAmountLeucocyte>=40||dblAmountLeucocyte<1)
						val=4;
					else if((dblAmountLeucocyte>=20&&dblAmountLeucocyte<=39.9)||(dblAmountLeucocyte>=1&&dblAmountLeucocyte<=2.9))
						val=2;
					else if(dblAmountLeucocyte>=15&&dblAmountLeucocyte<=19.9)
						val=1;
					else val=0;
					res[3] = val.ToString();
					total+=val;
				}
				else
				{
					res[3] = "/";
				}

				if(dblGCS >= 0)
				{
					val=15-dblGCS;
					res[10] = val.ToString();
					total+=val;
				}
				else
				{
					res[10] = "/";
				}
           
				if(dblHCO >=0)
				{
					if(dblHCO>=52||dblHCO<=15)
						val=4;
					else if((dblHCO>=41&&dblHCO<=51.9)||(dblHCO>=15&&dblHCO<=17.9))
						val=3;
					else if(dblHCO>=18&&dblHCO<=21.9)
						val=2;
					else if(dblHCO>=32&&dblHCO<=40.9)
						val=1;
					else val=0;
					res[13] = val.ToString();
					total+=val;
				}
				else
				{
					res[13] = "/";
				}

				switch(intAge)
				{
					case 0:
						val=0;
						break;
					case 1:
						val=2;break;
					case 2:
						val=3;break;
					case 3:
						val=5;break;
					case 4:
						val=6;break;

					default:
						val = 0; break;
				}
				total+=val;


				switch(intHealth)
				{
					case 0:
						val=2;
						break;
					case 1:
						val=5;break;
				}
				total+=val;
				res[14] = total.ToString();




				int a=0;
				if(chkPatientAfterEmergency.Checked)
					a=1;
				else a=0;
				double b=0;
				b=-3.517+(total*0.146)+a*0.603+ICUEval();

				double c=Math.Exp(b);
				dblR=c/(c+1);
				res[15] = dblR.ToString();

				dtlResult.Rows[0].ItemArray = res;
			}
			catch{}
		}
				
		private void frmAPACHEIIValuation_Load(object sender, System.EventArgs e)
		{
			try
			{
				dtlResult.Columns.Add("直肠温度");
				dtlResult.Columns.Add("平均动脉压");
				dtlResult.Columns.Add("心率");
				dtlResult.Columns.Add("白细胞计数");
				dtlResult.Columns.Add("呼吸频率");
				dtlResult.Columns.Add("PaO2");
				dtlResult.Columns.Add("DO2");
				dtlResult.Columns.Add("动脉血");
				dtlResult.Columns.Add("血钠浓度");
				dtlResult.Columns.Add("血钾浓度");
				dtlResult.Columns.Add("GCS");
				dtlResult.Columns.Add("血肌酐浓度");
				dtlResult.Columns.Add("血细胞比容");	
				dtlResult.Columns.Add("静脉血HCO3-");
				dtlResult.Columns.Add("总数");
				dtlResult.Columns.Add("危险性");				
				dtgResult.DataSource = dtlResult;

				dtlResult.Rows.Add(new string[]{"/","/","/","/","/","/","/","/","/","/","/","/","/","/","/","/"});
				
				cboPatientUnOp1.SelectedIndex = 0;
				cboPatientUnOp2.SelectedIndex = 0;
				cboHurts.SelectedIndex = 0;
				cboNeurotic.SelectedIndex = 0;
				cboOthers.SelectedIndex = 0;
				cboNoInRange.SelectedIndex = 0;
				cboPatientUnOp2.SelectedIndex = 0;
				cboMainReasonNoIn.SelectedIndex = 0;
				txtEvalDoctor.Text= clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;

				m_objHighLight.m_mthAddControlInContainer(this);

				//this.dtpEvalDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
				this.dtpEvalDate.m_mthResetSize();
                m_objSign.m_mthBindEmployeeSign(this.m_cmdEvalDoctor, txtEvalDoctor, 1, false, clsBaseInfo.LoginEmployee.m_strEMPID_CHR);

			}
			catch{}

		}

		private void chkPatientUnOp1_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(chkPatientUnOp1.Checked==true)
					cboPatientUnOp1.Enabled=true;
				else
				{
					cboPatientUnOp1.Enabled=false;
					cboPatientUnOp1.SelectedIndex=0;
				}
			}
			catch{}


		}

		private void chkPatientUnOp2_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{

				if(chkPatientUnOp2.Checked==true)
					cboPatientUnOp2.Enabled=true;
				else 
				{
					cboPatientUnOp2.Enabled=false;
					cboPatientUnOp2.SelectedIndex=0;
				}
			}
			catch{}
		}

		private void chkOthers_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(chkOthers.Checked==true)
					cboOthers.Enabled=true;
				else
				{
					cboOthers.Enabled=false;
					cboOthers.SelectedIndex=0;
				}
			}
			catch{}

		}

		private void chkNeurotic_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(chkNeurotic.Checked==true)
					cboNeurotic.Enabled=true;
				else
				{
					cboNeurotic.Enabled=false;
					cboNeurotic.SelectedIndex=0;
				}
			}
			catch{}

		}

		private void chkNoInRange_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{

				if(chkNoInRange.Checked==true)
					cboNoInRange.Enabled=true;
				else
				{
					cboNoInRange.Enabled=false;
					cboNoInRange.SelectedIndex=0;
				}
			}
			catch{}
		}

		private void chkHurts_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(chkHurts.Checked==true)
					cboHurts.Enabled=true;
				else
				{
					cboHurts.Enabled=false;
					cboHurts.SelectedIndex=0;
				}
			}
			catch{}
		}

		private void chkMainReasonNoIn_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{

				if(chkMainReasonNoIn.Checked==true)
					cboMainReasonNoIn.Enabled=true;
				else
				{
					cboMainReasonNoIn.Enabled=false;
					cboMainReasonNoIn.SelectedIndex=0;
				}
			}
			catch{}
		

		}

		private void rdbPatientSelOp_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
			
				cboPatientSelOp.Enabled=true;
				chkPatientAfterEmergency.Enabled=true;
				chkMainReasonNoIn.Enabled=true; 
        
				chkPatientUnOp1.Enabled=false;
				chkPatientUnOp2.Enabled=false;
				chkOthers.Enabled=false;
				chkNeurotic.Enabled=false;
				chkNoInRange.Enabled=false;
				chkHurts.Enabled=false;
				cboPatientUnOp1.SelectedIndex=0;
				cboPatientUnOp2.SelectedIndex=0;
				cboOthers.SelectedIndex=0;

				cboNeurotic.SelectedIndex=0;
				cboNoInRange.SelectedIndex=0;
				cboHurts.SelectedIndex=0;
				chkPatientUnOp1.Checked=false;
				chkPatientUnOp2.Checked=false;
				chkOthers.Checked=false;
				chkNeurotic.Checked=false;
				chkNoInRange.Checked=false;
				chkHurts.Checked=false;
			}
			catch{}

			
	
		}

		private void rdbPatientUnOp_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				chkPatientUnOp1.Enabled=true;
				chkPatientUnOp2.Enabled=true;
				chkOthers.Enabled=true;
				chkNeurotic.Enabled=true;
				chkNoInRange.Enabled=true;
				chkHurts.Enabled=true;


				cboPatientSelOp.Enabled=false;
				chkPatientAfterEmergency.Enabled=false;
				chkPatientAfterEmergency.Checked=false;
				chkMainReasonNoIn.Enabled=false;
				chkMainReasonNoIn.Checked=false;
				cboMainReasonNoIn.Enabled=false;
				cboMainReasonNoIn.SelectedIndex=0;
			}
			catch{}


		}

		private void tabPage1_Click(object sender, System.EventArgs e)
		{
		
		}




		#region Tools
		protected override void ClearUp()
		{
			try
			{
				m_strCreateDate = "";				
				object [] res = {"/","/","/","/","/","/","/","/","/","/","/","/","/","/","/","/"};
				dtlResult.Rows[0].ItemArray = res;

				foreach(Control control in this.Controls)
				{				
					string typeName = control.GetType().Name;
					if((typeName == "TextBox"||typeName == "ctlBorderTextBox")&&control.Name!="txtCardNo")
					{
						control.Text = "";
						
					}

					if(typeName=="CheckBox")
					{
						((CheckBox)control).Checked=false;
					}
					if(typeName=="dwtFlatComboBox"||typeName == "ctlComboBox")
					{
						control.Text = "";
					}
				}

//				foreach(Control control in this.gpbICU.Controls)
//				{
//					string typeName = control.GetType().Name;
//					if((typeName == "TextBox"||typeName == "ctlBorderTextBox")&&control.Name!="txtCardNo")
//					{
//						control.Text = "";
//						
//					}
//					if(typeName == "ListView")
//					{
//						((ListView)control).Items.Clear();
//					}
//					if(typeName=="CheckBox")
//					{
//						((CheckBox)control).Checked=false;
//					}
//					if(typeName=="dwtFlatComboBox"||typeName == "ctlComboBox")
//					{
//						control.Text = "选择类型";
//					}
//					
//				}
				foreach(Control control in this.gpbExactLife.Controls)
				{
					string typeName = control.GetType().Name;
					if((typeName == "TextBox"||typeName == "ctlBorderTextBox")&&control.Name!="txtCardNo")
					{
						control.Text = "";
						
					}
					if(typeName == "ListView")
					{
						((ListView)control).Items.Clear();
					}
					if(typeName=="CheckBox")
					{
						((CheckBox)control).Checked=false;
					}
					if(typeName=="dwtFlatComboBox"||typeName == "ctlComboBox")
					{
						control.Text = "选择类型";
					}

				}
				rdbPatientUnOp.Checked = true;
				rdbAgeU44.Checked = true;
				txtFiO2.Text = "";
				txtPao2.Text = "";
				txtPaCO2.Text = "";
				txtDo2.Text = "";
				m_cboOpenEyes.SelectedIndex = -1;
				m_cboSay.SelectedIndex = -1;
				m_cboSport.SelectedIndex = -1;
                txtAutoTime.Text = "60";
				this.txtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;

				this.dtpEvalDate.Value = DateTime.Now;
			}
			catch{}
		}
		#endregion

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.ClearUp();
			}
			catch
			{}
		}

		private void m_mthClearPatientInfo()
		{
			m_strInPatientID = "";
			m_strInPatientDate = "";
		}

		#endregion

		#region 自动评分 已注释
		/// <summary>
		/// Alex 2002-9-30
		/// 自动评分的保存
		/// </summary>
		/// <param name="m_strAutoEvalTime"></param>
		private void m_AutoSave(string m_strAutoEvalTime)
		{
			convert();
			this.cmdCalculate_Click(null,null);

			APACHEIIValuationDB objAPACHEIIValuationDB = m_objGetCurrentEvalInfo();
			objAPACHEIIValuationDB.strActivityTime=m_strAutoEvalTime;			

			this.cmdCalculate_Click(null,null);
//			if(objDomain.lngAddNewRecordOfAutoEval(objAPACHEIIValuationDB)>0)
			if(objDomain.m_lngSave(objAPACHEIIValuationDB) > 0)
			{
				dtpEvalDate.Value = DateTime.Parse(m_strAutoEvalTime);
                TreeNode m_trnNewNode = new TreeNode(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                m_trnNewNode.Tag = DateTime.Parse(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                trvActivityTime.Nodes[0].Nodes.Add(m_trnNewNode);                
			}
		}

		private void cmdGetData_Click(object sender, System.EventArgs e)
		{
			if(m_ctlAreaPatientSelection.CurrentBed == null)
			{
				clsPublicFunction.ShowInformationMessageBox("请输入床号！");
				return;
			}
			GetData();	
		}

		private void m_mthGetData(string p_strFindDate)
		{
			bool blnIsGE=m_blnCurrApparatus();

			clsCMSData objCMSData=null;
			clsVentilatorData objVentilatorData=null;

            if (m_objCurrentPatient == null) return;

			string[] strTypeArry=new string[]{"TEMP1","HEARTRATE","NPBSYSTOLIC","RESPRATE"};//
			m_mthGetICUDataByTime(p_strFindDate,out objCMSData,out objVentilatorData,strTypeArry);
			
			if (!blnIsGE)
			{
				if (objCMSData != null)
				{
					if(objCMSData.m_strHeartRate == null || objCMSData.m_strHeartRate == "")
						txtHR.Text = "";
					else
						txtHR.Text = m_strFormatShowParamData(objCMSData.m_strHeartRate);//.Substring(0,objCMSData.m_strHeartRate.IndexOf("."));

					if(objCMSData.m_strTemp1 == null || objCMSData.m_strTemp1 == "")
						txtTemperature.Text = "";
					else
						txtTemperature.Text = objCMSData.m_strTemp1.Trim();

				}
				if (objVentilatorData!=null)
				{
					if(objVentilatorData.m_strRespRate == null || objVentilatorData.m_strRespRate == "")
						txtBreath.Text = "";
					else
						txtBreath.Text = m_strFormatShowParamData(objVentilatorData.m_strRespRate);//.Substring(0,objVentilatorData.m_strRespRate.IndexOf("."));
				}
			}
			else
			{
                m_mthGetMonitorParamGE();
                clsGECMSData objGECMSData = m_objGECMSData;

				if (objGECMSData!=null)
				{
					if(objGECMSData.m_strHR == null || objGECMSData.m_strHR == "")
						txtHR.Text = "";
					else
						txtHR.Text = m_strFormatShowParamData(objGECMSData.m_strHR);//.Substring(0,objGECMSData.m_strHR.IndexOf("."));
					
					if(objGECMSData.m_strTEMP1 == null || objGECMSData.m_strTEMP1 == "")
						txtTemperature.Text = "";
					else
						txtTemperature.Text = objGECMSData.m_strTEMP1.Trim();

					if(objGECMSData.m_strRR == null || objGECMSData.m_strRR == "")
						txtBreath.Text = "";
					else
						txtBreath.Text = m_strFormatShowParamData(objGECMSData.m_strRR.Trim());

					if(objGECMSData.m_strNBPMean == null || objGECMSData.m_strNBPMean == "")
						txtAdvArteryPress.Text = "";
					else
						txtAdvArteryPress.Text = objGECMSData.m_strNBPMean.Trim();
				}
			}

		}

		private void GetData()
		{
			#region Old			
//			try
//			{
//				clsCMSData objCMSData = null;
//				clsVentilatorData objVenData = null;
//				
//				clsGECMSData objGECMSData=null;
//				bool blnIsGE=m_blnCurrApparatus();
//
//				//				objPatientInfo_Base.m_ObjCurrentInHospitalInfo.m_ObjLastICUUtil.m_mthGetICUDataByTime(strSickBedNO,dtpStartSample.Value,out objCMSData,out objVenData);
//
//				m_mthGetICUDataByTime(dtpStartSample.Value,out objCMSData,out objVenData);
//				if (blnIsGE)
//					m_mthGetICUGEDataByTime(dtpStartSample.Value.ToString(),out objGECMSData);
//
//
//				//设置监护仪获取的数值
//				if (!blnIsGE)
//				{
//					if(objCMSData.m_strHeartRate=="")
//						txtHR.Text = "";
//					else
//						txtHR.Text = objCMSData.m_strHeartRate.Substring(0,objVenData.m_strRespRate.Length-3);
//			
//					txtPao2.Text = objCMSData.m_strBloodNum1;
//			
//					//				lblCMSSampleTime.Text = objCMSData.m_strDataCollectedTime;
//				}
//				else
//				{
//					if(objGECMSData.m_strHR=="")
//						txtHR.Text = "";
//					else
//						txtHR.Text = objGECMSData.m_strHR;
//			
//					txtPao2.Text = "";
//				}
//				//设置呼吸机获取的数值
//				if(objVenData.m_strRespRate=="")
//					txtBreath.Text = "";
//				else
//					txtBreath.Text = objVenData.m_strRespRate.Substring(0,objVenData.m_strRespRate.Length-3);
//
//				txtFiO2.Text = objVenData.m_strO2Concentration;
//			
//				//				lblVenSampleTime.Text = objVenData.m_strDataCollectedTime;		
//				
//			}
//			catch
//			{
//			}
			#endregion Old

			m_mthGetData(dtpStartSample.Value.ToString());
		}

		private void cmdStartAuto_Click(object sender, System.EventArgs e)
		{
			/*
			 * alex 2002-9-29
			 * 增加一个判断控制
			 * */
			if(m_ctlPatientInfo.CurrentEmrPatient == null)
			{
				clsPublicFunction.ShowInformationMessageBox("请输入住院号！");
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
//
		private void cmdShowResult_Click(object sender, System.EventArgs e)
		{
            frmAutoResult.M_lblTitle = "查看结果";
			frmAutoResult.Visible = true;
		}
//
//
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
																	 dtlResult.Rows[0]["危险性"].ToString(),
																	 txtTemperature.Text,
																	 txtAdvArteryPress.Text,
																	 txtHR.Text,
																	 txtAmountLeucocyte.Text,
																	 txtHCO.Text,
																	 txtBreath.Text,
																	 txtPao2.Text,
																	 txtDo2.Text,
																	 txtFiO2.Text,
																	 txtBloodFlesh.Text,
																	 txtArteryBloodpH.Text,
																	 txtBloodNa.Text,
																	 txtBloodK.Text,
																	 dblGCS.ToString(),
																	 txtBloodCorpuscle.Text
																 });
				frmAutoResult.AddResult(item);
				m_AutoSave(dtpStartSample.Value.ToString("yyyy-MM-dd HH:mm:ss"));
			}
			catch
			{
			}
		}

		/// <summary>
		/// 关闭时停止评分 Alex 2002-10-16
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmAPACHEIIValuation_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				timAutoCollect.Close();
			}
			catch
			{}
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
			if(strTypeName != "Label"  && strTypeName != "CheckBox" && strTypeName != "ctlBorderTextBox" && strTypeName != "RadioButton")
			{
				p_ctlControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);
				if(p_ctlControl.HasChildren && strTypeName !="DataGrid" && strTypeName !="DateTimePicker" && strTypeName !="ctlComboBox")
				{									
					foreach(Control subcontrol in p_ctlControl.Controls)
					{				
						string strSubTypeName = subcontrol.GetType().Name;
						if(strSubTypeName != "Label" &&  strSubTypeName != "CheckBox" && strSubTypeName != "ctlBorderTextBox" && strSubTypeName != "RadioButton")												
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
			if(m_strInPatientID==null || m_strInPatientID=="" || m_strInPatientDate==null|| m_strInPatientDate=="")return;


			this.txtTemperature.Text="";			
			this.txtBreath.Text="";
			this.txtAdvArteryPress.Text="";
			this.txtPao2.Text="";
			this.txtHR.Text="";


			clsTrendDomain objDomain=new clsTrendDomain();
			string[] strEMFC_IDArr=new string[]{"100","92","91","-1","40"};
			//{"100","40","40","92","89","90","-1","-1"};//体温，心率，脉搏，呼吸,收缩压，舒张压,-1代表还没有找到的编号
			string[] strResultArr;
			long lngRes=objDomain.m_lngGetDocvueResultArr(m_strInPatientID,DateTime.Parse(m_strInPatientDate),strEMFC_IDArr,dtpEvalDate.Value,out strResultArr);
			if(lngRes<=0)
			{
				switch(lngRes)
				{
					case (long)(enmOperationResult.Not_permission) :
						clsPublicFunction.s_mthShowNotPermitMessage();break;
					case (long)(enmOperationResult.DB_Fail) :
						clsPublicFunction.ShowInformationMessageBox("数据库连接失败");break;
				}
			}
			else 
			{
				this.txtTemperature.Text=strResultArr[0];
				this.txtBreath.Text=strResultArr[1];
				this.txtAdvArteryPress.Text=strResultArr[2];
				this.txtPao2.Text=strResultArr[1];
				this.txtHR.Text=strResultArr[2];
			}
		}

		protected override void m_mthDisplay()
		{
			try
			{
				    long lngRes = objDomain.m_lngGetApacheIIValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out m_objAPACHEIIValuationDB);

                    if (lngRes < 0 || lngRes == 0)
                    {
                        return;
                    }

                    if (m_objAPACHEIIValuationDB.strAgeGroup != null)
                    {
                        this.intAge = int.Parse(m_objAPACHEIIValuationDB.strAgeGroup);
                        foreach (Control ctrl in gpbAge.Controls)
                        {
                            if (int.Parse((string)ctrl.Tag) == intAge)
                            {
                                ((RadioButton)ctrl).Checked = true;
                                break;
                            }
                        }
                    }

				this.intHealth = int.Parse(m_objAPACHEIIValuationDB.strHealthGroup );
//				foreach(Control ctr2 in gpbICU.Controls)
//				{
//					if( ctr2.GetType().Name == "RadioButton" && int.Parse((string)ctr2.Tag) == intHealth)
//					{
//						((RadioButton)ctr2).Checked = true;
//						break;
//					}								
//				}

				this.txtBreath.Text = m_objAPACHEIIValuationDB.strBreath;
				this.txtHR.Text = m_objAPACHEIIValuationDB.strHeartRate;
				this.txtTemperature.Text =m_objAPACHEIIValuationDB.strTemperature ;
				this.txtAdvArteryPress.Text = m_objAPACHEIIValuationDB.strAdvArteryPress ;
				this.txtPao2.Text = m_objAPACHEIIValuationDB.strPao2 ;
				this.txtBloodNa.Text =m_objAPACHEIIValuationDB.strBloodNa ;
				this.txtDo2.Text = m_objAPACHEIIValuationDB.strDo2;
				this.txtArteryBloodpH.Text = m_objAPACHEIIValuationDB.strArteryBloodpH;
				this.txtBloodK.Text = m_objAPACHEIIValuationDB.strBloodK;
				this.txtAmountLeucocyte.Text = m_objAPACHEIIValuationDB.strAmountLeucocyte;
				this.txtFiO2.Text = m_objAPACHEIIValuationDB.strFiO2;
				string[] strGCSArr = m_objAPACHEIIValuationDB.strGCS.Split(c_strSpecialSymbol.ToCharArray());
				m_cboOpenEyes.SelectedIndex = int.Parse(strGCSArr[0]);
				m_cboSay.SelectedIndex = int.Parse(strGCSArr[1]);
				m_cboSport.SelectedIndex = int.Parse(strGCSArr[2]);
				this.txtBloodFlesh.Text = m_objAPACHEIIValuationDB.strBloodFlesh;
				this.txtBloodCorpuscle.Text = m_objAPACHEIIValuationDB.strBloodCorpuscle;
				this.txtHCO.Text = m_objAPACHEIIValuationDB.strHCO;
			
				this.KidneyProstrate=int.Parse(m_objAPACHEIIValuationDB.strKidneyProstrate);
				if(KidneyProstrate==1) 
					chkKidneyProstrate.Checked=true;
				else 
					chkKidneyProstrate.Checked=false;
				this.chkKidneyProstrate.Checked=chkKidneyProstrate.Checked;
				this.PatientUnOp1=int.Parse(m_objAPACHEIIValuationDB.strPatientUnOp1);
				if(PatientUnOp1==1) 
					chkPatientUnOp1.Checked=true;
				else 
					chkPatientUnOp1.Checked=false;
				this.chkPatientUnOp1.Checked=chkPatientUnOp1.Checked;
				this.PatientUnOp2=int.Parse(m_objAPACHEIIValuationDB.strPatientUnOp2);
				if(PatientUnOp2==1) 
					chkPatientUnOp2.Checked=true;
				else 
					chkPatientUnOp2.Checked=false;
				this.chkPatientUnOp2.Checked=chkPatientUnOp2.Checked;

				this.Others=int.Parse(m_objAPACHEIIValuationDB.strOthers);
				if(Others==1) 
					chkOthers.Checked=true;
				else 
					chkOthers.Checked=false;
				this.chkOthers.Checked=chkOthers.Checked;

				this.Neurotic=int.Parse(m_objAPACHEIIValuationDB.strNeurotic);
				if(Neurotic==1) 
					chkNeurotic.Checked=true;
				else 
					chkNeurotic.Checked=false;
				this.chkNeurotic.Checked=chkNeurotic.Checked;

				this.NoInRange=int.Parse(m_objAPACHEIIValuationDB.strNoInRange);
				if(NoInRange==1) 
					chkNoInRange.Checked=true;
				else 
					chkNoInRange.Checked=false;
				this.chkNoInRange.Checked=chkNoInRange.Checked;

				this.Hurts=int.Parse(m_objAPACHEIIValuationDB.strHurts);
				if(Hurts==1) 
					chkHurts.Checked=true;
				else 
					chkHurts.Checked=false;
				this.chkHurts.Checked=chkHurts.Checked;

				this.PatientAfterEmergency=int.Parse(m_objAPACHEIIValuationDB.strPatientAfterEmergency);
				if(PatientAfterEmergency==1) 
					chkPatientAfterEmergency.Checked=true;
				else 
					chkPatientAfterEmergency.Checked=false;

				this.chkPatientAfterEmergency.Checked=chkPatientAfterEmergency.Checked;

				this.MainReasonNoIn=int.Parse(m_objAPACHEIIValuationDB.strMainReasonNoIn);
				if(MainReasonNoIn==1) 
					chkMainReasonNoIn.Checked=true;
				else 
					chkMainReasonNoIn.Checked=false;

				this.chkMainReasonNoIn.Checked=chkMainReasonNoIn.Checked;
	

				this.cboPatientUnOp1.Text=m_objAPACHEIIValuationDB.strPatientUnOp1Eval;

			
				this.cboPatientUnOp2.Text=m_objAPACHEIIValuationDB.strPatientUnOp2Eval;
				this.cboOthers.Text=m_objAPACHEIIValuationDB.strOthersEval;
				this.cboNeurotic.Text=m_objAPACHEIIValuationDB.strNeuroticEval;
				this.cboNoInRange.Text=m_objAPACHEIIValuationDB.strNoInRangeEval;
				this.cboHurts.Text=m_objAPACHEIIValuationDB.strHurtsEval;
				this.cboPatientSelOp.Text=m_objAPACHEIIValuationDB.strPatientSelOpEval;
				this.cboMainReasonNoIn.Text=m_objAPACHEIIValuationDB.strMainReasonNoInEval;
				this.txtPaCO2.Text = m_objAPACHEIIValuationDB.strPaCO2;			
			
				object []res = dtlResult.Rows[0].ItemArray;

				res[0] = m_objAPACHEIIValuationDB.strTemperatureEval ;
				res[1] = m_objAPACHEIIValuationDB.strAdvArteryPressEval ;
				res[5] = m_objAPACHEIIValuationDB.strPao2Eval ;
				res[6] = m_objAPACHEIIValuationDB.strDo2Eval ;
				res[7] = m_objAPACHEIIValuationDB.strArteryBloodpHEval;
				res[8] = m_objAPACHEIIValuationDB.strBloodNaEval;
				res[9] = m_objAPACHEIIValuationDB.strBloodKEval;
				res[11] = m_objAPACHEIIValuationDB.strBloodFleshEval;
				res[12] = m_objAPACHEIIValuationDB.strBloodCorpuscleEval;
				res[3] = m_objAPACHEIIValuationDB.strAmountLeucocyteEval;
		
				res[10] = m_objAPACHEIIValuationDB.strGCSEval;
				res[13] = m_objAPACHEIIValuationDB.strHCOEval;
				res[4] = m_objAPACHEIIValuationDB.strBreathEval;
				res[2] =m_objAPACHEIIValuationDB.strHeartRateEval;
				res[14] = m_objAPACHEIIValuationDB.strTotalEval;
				res[15] = m_objAPACHEIIValuationDB.strREval;
                
				dtlResult.Rows[0].ItemArray = res;

                clsEmrEmployeeBase_VO objEmployee = null;
                clsBaseInfo.m_lngGetEmpByID(m_objAPACHEIIValuationDB.strEvalDoctorID, out objEmployee);                
				this.txtEvalDoctor.Text = objEmployee.m_strLASTNAME_VCHR;

				this.dtpEvalDate.Value = DateTime.Parse(m_objAPACHEIIValuationDB.strActivityTime);
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
			}
		}

		private void m_cmdToAaDO2_Click(object sender, System.EventArgs e)
		{
			if(!m_blnInputValid(new TextBox[]{txtFiO2,txtPaCO2,txtPao2}))
				return;

			txtDo2.Text = m_dblCalAaDO2(double.Parse(txtFiO2.Text),double.Parse(txtPaCO2.Text),double.Parse(txtPao2.Text)).ToString();
		}	
	
		#region Print Function

		public override void m_mthSetPrint()
		{
			APACHEIIValuationDB objValue;
			objPrintTool=new clsAPACHEII_ValuationPrintTool();
			objPrintTool.m_mthInitPrintTool(null);	
			if(m_objCurrentPatient == null)
				objPrintTool.m_mthSetPrintInfo(null,null,DateTime.MinValue);
			else
			{
				if(this.trvActivityTime.SelectedNode ==null || this.trvActivityTime.SelectedNode==trvActivityTime.Nodes[0] || trvActivityTime.SelectedNode.Tag==null)
					objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient,null,DateTime.MinValue);
				else 
				{
					objDomain.m_lngGetApacheIIValue(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),trvActivityTime.SelectedNode.Tag.ToString(),out objValue);
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

		private void gpbExactLife_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void groupBox3_Enter(object sender, System.EventArgs e)
		{
		
		}

        private void chkPatientUnOp1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkPatientUnOp1.Checked)
            {
                cboPatientUnOp1.Enabled = true;
            }
            else
            {
                cboPatientUnOp1.Enabled = false;
            }
        }

        private void chkPatientUnOp2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkPatientUnOp2.Checked)
            {
                cboPatientUnOp2.Enabled = true;
            }
            else
            {
                cboPatientUnOp2.Enabled = false;
            }
        }

        private void chkOthers_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkOthers.Checked)
            {
                cboOthers.Enabled = true;
            }
            else
            {
                cboOthers.Enabled = false;
            }
        }

        private void chkNeurotic_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkNeurotic.Checked)
            {
                cboNeurotic.Enabled = true;
            }
            else
            {
                cboNeurotic.Enabled = false;
            }
        }

        private void chkNoInRange_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkNoInRange.Checked)
            {
                cboNoInRange.Enabled = true;
            }
            else
            {
                cboNoInRange.Enabled = false;
            }
        }

        private void chkHurts_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkHurts.Checked)
            {
                cboHurts.Enabled = true;
            }
            else
            {
                cboHurts.Enabled = false;
            }
        }

        private void chkPatientAfterEmergency_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPatientAfterEmergency.Checked)
            {
                cboPatientSelOp.Enabled = true;
            }
            else
            {
                cboPatientSelOp.Enabled = false;
            }
        }

        private void chkMainReasonNoIn_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkMainReasonNoIn.Checked)
            {
                cboMainReasonNoIn.Enabled = true;
            }
            else
            {
                cboMainReasonNoIn.Enabled = false;
            }
        }

        private void rdbPatientSelOp_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rdbPatientSelOp.Checked)
            {
                chkPatientAfterEmergency.Enabled = true;
                chkMainReasonNoIn.Enabled = true;
            }
            else
            {
                chkPatientAfterEmergency.Enabled = false;
                chkMainReasonNoIn.Enabled = false;
                chkPatientAfterEmergency.Checked = false;
                chkMainReasonNoIn.Checked = false;
            }
        }

        private void rdbPatientUnOp_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rdbPatientUnOp.Checked)
            {
                chkPatientUnOp1.Enabled = true;
                chkPatientUnOp2.Enabled = true;
                chkOthers.Enabled = true;
                chkNeurotic.Enabled = true;
                chkNoInRange.Enabled = true;
                chkHurts.Enabled = true;
            }
            else
            {
                chkPatientUnOp1.Enabled = false;
                chkPatientUnOp2.Enabled = false;
                chkOthers.Enabled = false;
                chkNeurotic.Enabled = false;
                chkNoInRange.Enabled = false;
                chkHurts.Enabled = false;
                chkPatientUnOp1.Checked = false;
                chkPatientUnOp2.Checked = false;
                chkOthers.Checked = false;
                chkNeurotic.Checked = false;
                chkNoInRange.Checked = false;
                chkHurts.Checked = false;
            }
        }

	}
}