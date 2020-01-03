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
//using iCare.ICU.Espial;
//using iCare.Common;
using com.digitalwave.Emr.Signature_gui;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// 小儿危重病例评分
	/// </summary>
	public partial class frmBabyInjuryCaseEvaluation : frmValuationBaseForm,PublicFunction
	{
      
        protected ctlHighLightFocus m_objHighLight;

        private const string _strTypeMonitor = "Monitoring";
        private const string _strTypeVentilator = "Ventilator";

        private frmAutoEvalResult frmAutoResult = null;
        BabyInjuryCaseEvaluationDomain objDomain;
        private clsBorderTool m_objBorderTool;

        private DataTable dtlResult;
        private int intBldOrShkSel;
        private int intPao2Sel;
        private int intCrOrBUNSel;
        private int intAgeGroupSel;
        private int intHbSel;

        private BabyInjuryCaseEvaluationDB m_objBabyInjuryCaseEvaluationDB;

        #region Constructor
        public frmBabyInjuryCaseEvaluation()
        {            
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            m_objBorderTool = new clsBorderTool(Color.White);
            m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{
																			 trvActivityTime,dtgResult																	 
																		 });
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            objDomain = new BabyInjuryCaseEvaluationDomain();
            dtlResult = new DataTable("result");
            intBldOrShkSel = 0;
            intPao2Sel = 0;
            intCrOrBUNSel = 0;
            intAgeGroupSel = 0;
            intHbSel = 0;
            intBreathSel = 0;

            ColumnHeader chResult1 = new ColumnHeader();
            chResult1.Text = "心率";
            chResult1.Width = 50;

            ColumnHeader chResult2 = new ColumnHeader();
            chResult2.Text = "血压（收缩压）";
            chResult2.Width = 100;

            ColumnHeader chResult3 = new ColumnHeader();
            chResult3.Text = "呼吸";
            chResult3.Width = 50;

            ColumnHeader chResult4 = new ColumnHeader();
            chResult4.Text = "PaO2";
            chResult4.Width = 50;

            ColumnHeader chResult5 = new ColumnHeader();
            chResult5.Text = "pH";
            chResult5.Width = 50;

            ColumnHeader chResult6 = new ColumnHeader();
            chResult6.Text = "Na+";
            chResult6.Width = 50;

            ColumnHeader chResult7 = new ColumnHeader();
            chResult7.Text = "K+";
            chResult7.Width = 50;

            ColumnHeader chResult8 = new ColumnHeader();
            chResult8.Text = "Cr 或 BUN";
            chResult8.Width = 100;

            ColumnHeader chResult9 = new ColumnHeader();
            chResult9.Text = "Hb";
            chResult9.Width = 50;

            ColumnHeader chResult10 = new ColumnHeader();
            chResult10.Text = "胃肠表现";
            chResult10.Width = 100;

            ColumnHeader chResult11 = new ColumnHeader();
            chResult11.Text = "总分";
            chResult11.Width = 100;



            frmAutoResult = new frmAutoEvalResult(chResult11, chResult1, chResult2, chResult3, chResult4, chResult5, chResult6,
                chResult7, chResult8, chResult9, chResult10);

            frmAutoResult.Text = "小儿危重病例自动评分";

            //			frmAutoResult.Show();
            frmAutoResult.Visible = false;
            frmAutoResult.Owner = this;

            this.cboStomach.AddRangeItems(new object[] {
															"应激性溃疡出血及肠麻痹",
															"应激性溃疡出血",
															"其余"});
            this.txtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;

            m_mthSetQuickKeys();

            m_objHighLight = new ctlHighLightFocus(clsHRPColor.s_ClrHightLight);

            //			gpbAuto.Visible=false;

            
        }
        #endregion


		#region interface

		#region Null Impletement
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

        public override void Redo()
		{
		
		}

        public override void Undo()
		{
		
		}

        public override void Verify()
		{
			////long lngRes=m_lngSignVerify(p_strFormID,p_strRecordID);
		}

		#endregion

        public override void Delete()
		{
#if FunctionPrivilege
			if(!MDIParent.s_blnCheckCurrentPrivilege(lbltxtSetionOffice.Tag.ToString(),PrivilegeData.enmPrivilegeSF.BabyInjuryCaseEvaluation,PrivilegeData.enmPrivilegeOperation.Delete))
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

			trvActivityTime.SelectedNode.Remove();

			this.trvActivityTime.SelectedNode=this.trvActivityTime.Nodes[0];
			ClearUp();

		}

        public override void Display() { }
        public override void Display(string cardno, string ActivityTime)
		{
//			try
//			{
//				long lngRes  = objDomain.m_lngGetBabyInjuryCaseValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out m_objBabyInjuryCaseEvaluationDB);
//
//				if(m_objBabyInjuryCaseEvaluationDB == null)
//					return;
//
//				this.txtHeartRate.Text =m_objBabyInjuryCaseEvaluationDB.strHeartRate ;
//
//				SetGroupSelText(gpbBloodAndShk,m_objBabyInjuryCaseEvaluationDB.strBloodOrShrinkPressure,m_objBabyInjuryCaseEvaluationDB.strBldOrShKSel );
//				
//				SetGroupSelText(gpbAgeGroup,null,m_objBabyInjuryCaseEvaluationDB.strAgeGroup);
//
//				this.txtBreath.Text = m_objBabyInjuryCaseEvaluationDB.strBreath ;
//
//				this.chkBreath.Checked = bool.Parse( m_objBabyInjuryCaseEvaluationDB.strIsrhythmWrong);
//
//				SetGroupSelText(gpbPao2,m_objBabyInjuryCaseEvaluationDB.strPao2,m_objBabyInjuryCaseEvaluationDB.strPao0kPaOrmmHgSel) ;
//
//				this.txtpH.Text = m_objBabyInjuryCaseEvaluationDB.strpH ;
//
//				this.txtNaPlus.Text = m_objBabyInjuryCaseEvaluationDB.strNaPlus ;
//
//				this.txtKPlus.Text = m_objBabyInjuryCaseEvaluationDB.strKPlus ;
//
//				SetGroupSelText(gpbCrAndBUN,m_objBabyInjuryCaseEvaluationDB.strCrOrBUN,m_objBabyInjuryCaseEvaluationDB.strCrOrBUNSel) ;
//				
//				SetGroupSelText(gpbHb,m_objBabyInjuryCaseEvaluationDB.strHb,m_objBabyInjuryCaseEvaluationDB.strHbSel );
//
//				this.cboStomach.Text = m_objBabyInjuryCaseEvaluationDB.strStomachAndIntestines ;
//
//				clsEmployee objEmployee = new clsEmployee(m_objBabyInjuryCaseEvaluationDB.strEvalDoctorID);
//				this.txtEvalDoctor.Text=objEmployee.m_StrFirstName;
//
//				object[] res = new Object[11];
//
//				res[0] = m_objBabyInjuryCaseEvaluationDB.strHeartRateEval ;
//				res[1] = m_objBabyInjuryCaseEvaluationDB.strBloodPressureOrShrinkPressureEval ;
//				res[2] = m_objBabyInjuryCaseEvaluationDB.strBreathEval ;
//				res[3] = m_objBabyInjuryCaseEvaluationDB.strPao2Eval ;
//				res[4] = m_objBabyInjuryCaseEvaluationDB.strpHEval ;
//				res[5] = m_objBabyInjuryCaseEvaluationDB.strNaPlusEval ;
//				res[6] = m_objBabyInjuryCaseEvaluationDB.strKPlusEval ;
//				res[7] = m_objBabyInjuryCaseEvaluationDB.strCrOrBUNEval ;
//				res[8] = m_objBabyInjuryCaseEvaluationDB.strRedCellCompOrHbEval ;
//				res[9] = m_objBabyInjuryCaseEvaluationDB.strStomachAndintestinesBehaveEval ;
//				res[10]= m_objBabyInjuryCaseEvaluationDB.strTotalEval ;
//
//				dtlResult.Rows[0].ItemArray = res;
//
//				dtpEvalDate.Value = DateTime.Parse(m_objBabyInjuryCaseEvaluationDB.strActivityTime);
//				
//			}
//			catch
//			{
//			}
		}

		private void SetGroupSelText(GroupBox gpb,string text,string tabindex)
		{
			try
			{
				foreach(Control control in gpb.Controls)
				{
					if(control.GetType().Name == "Label")
						continue;

					if(control.Tag.ToString() == tabindex)
					{
						if(control.GetType().Name == "RadioButton")
							((RadioButton)control).Checked = true;
						else
						{
							control.Text = text;
							control.Enabled = true;
						}
					}
					else
					{
						if(control.GetType().Name == "RadioButton")
							((RadioButton)control).Checked = false;
						else if(control.GetType().Name == "TextBox")
						{
							control.Text = "";
							control.Enabled = false;
						}
					}
				}
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
			}
		}
        public override void Save()
		{
#if FunctionPrivilege
			if(!MDIParent.s_blnCheckCurrentPrivilege(lbltxtSetionOffice.Tag.ToString(),PrivilegeData.enmPrivilegeSF.BabyInjuryCaseEvaluation,PrivilegeData.enmPrivilegeOperation.AddOrModify))
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
		
		#region InnerControl
		public void ReceiveID(string strID)
		{
			if(strID.Trim().Length !=0)
			{				
//				this.Display(strID,"");
				this.m_mthDisplay();
			}
		}

		private void PressChange(object sender, System.EventArgs e)
		{
			try
			{
				if(sender.Equals(rdbBloodPress) && rdbBloodPress.Checked)
				{
					txtBloodPress.Enabled = true;
					txtShrinkPressure.Enabled = false;
					txtShrinkPressure.Text = "";
					intBldOrShkSel = int.Parse((string)rdbBloodPress.Tag);
				}
				else if(rdbShrinkPress.Checked)
				{
					txtBloodPress.Enabled =false;
					txtBloodPress.Text = "";
					txtShrinkPressure.Enabled = true;
					intBldOrShkSel = int.Parse((string)rdbShrinkPress.Tag);
				}
			}
			catch
			{
			}
		}

		private void Pao2Changed(object sender, System.EventArgs e)
		{
			try
			{
				if(sender.Equals(rdbPao2kPa) && rdbPao2kPa.Checked)
				{
					txtPao2kPa.Enabled = true;
					txtPao2mmHg.Enabled = false;
					txtPao2mmHg.Text = "";
					intPao2Sel = int.Parse((string)rdbPao2kPa.Tag);
				}
				else if(rdbPao2mmHg.Checked)
				{
					txtPao2kPa.Enabled = false;
					txtPao2kPa.Text = "";
					txtPao2mmHg.Enabled = true;
					intPao2Sel = int.Parse((string)rdbPao2mmHg.Tag);
				}
			}
			catch
			{
			}
		}

		private void CrChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(sender.Equals(rdbCrmg) && rdbCrmg.Checked)
				{
					txtCrmg.Enabled = true;
					txtCrumol.Enabled = false;
					txtCrumol.Text = "";
					txtBUNmmol.Enabled = false;
					txtBUNmmol.Text = "";
					txtBUNmg.Enabled = false;
					txtBUNmg.Text = "";
					intCrOrBUNSel = int.Parse((string)rdbCrmg.Tag);
				}
				else if(rdbCrumol.Checked)
				{
					txtCrmg.Enabled = false;
					txtCrmg.Text = "";
					txtCrumol.Enabled = true;
					txtCrumol.Text = "";
					txtBUNmmol.Enabled = false;
					txtBUNmmol.Text = "";
					txtBUNmg.Enabled = false;
					txtBUNmg.Text = "";
					intCrOrBUNSel = int.Parse((string)rdbCrumol.Tag);
				}
			}
			catch
			{
			}
		}

		private void BUNChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(sender.Equals(rdbBUNmg) && rdbBUNmg.Checked)
				{
					txtBUNmg.Enabled = true;
					txtBUNmmol.Enabled = false;
					txtBUNmmol.Text = "";
					txtCrumol.Enabled = false;
					txtCrumol.Text = "";
					txtCrmg.Enabled = false;
					txtCrmg.Text = "";		
					intCrOrBUNSel = int.Parse((string)rdbBUNmg.Tag);
				}
				else if(rdbBUNmmol.Checked)
				{
					txtBUNmg.Enabled = false;
					txtBUNmg.Text = "";
					txtBUNmmol.Enabled = true;
					txtCrumol.Enabled = false;
					txtCrumol.Text = "";
					txtCrmg.Enabled = false;
					txtCrmg.Text = "";
					intCrOrBUNSel = int.Parse((string)rdbBUNmmol.Tag);
				}
			}
			catch
			{
			}
		}

		private void NewBabyInjuryCaseEvaluation_Load(object sender, System.EventArgs e)
		{
			try
			{
				dtlResult.Columns.Add("心率");
				dtlResult.Columns.Add("血压（收缩压）");
				dtlResult.Columns.Add("呼吸");
				dtlResult.Columns.Add("PaO2");
				dtlResult.Columns.Add("pH");
				dtlResult.Columns.Add("Na+");
				dtlResult.Columns.Add("K+");
				dtlResult.Columns.Add("Cr 或 BUN");
				dtlResult.Columns.Add("Hb");
				dtlResult.Columns.Add("胃肠表现");
				dtlResult.Columns.Add("总分");

				dtgResult.DataSource = dtlResult;

				dtlResult.Rows.Add(new string[]{"/","/","/","/","/","/","/","/","/","/","/"});

								
				m_objHighLight.m_mthAddControlInContainer(this);

				//this.dtpEvalDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
				this.dtpEvalDate.m_mthResetSize();
			}
			catch
			{
			}
		}

		private void cmdCalculate_Click(object sender, System.EventArgs e)
		{
			try
			{
				object [] res = dtlResult.Rows[0].ItemArray;
				double total = 0;
				int val;
				double count = 0;
				val = intCalHeartRate();
				if(val>=0)
				{
					total+=val;
					count+=10;
					res[0] = val.ToString();
				}
				else
					res[0] = "/";

				val = intCalBloodPressOrShrinkPress();
				if(val>=0)
				{
					total+=val;
					count+=10;				
					res[1] = val.ToString();
				}
				else
					res[1] = "/";

				val = intCalBreath();
				if(val>=0)
				{
					total+=val;
					count+=10;
					res[2] = val.ToString();
				}
				else
					res[2] = "/";

				val = intCalPao2();
				if(val>=0)
				{
					total+=val;
					count+=10;
					res[3] = val.ToString();
				}
				else
					res[3] = "/";

				val = intCalpH();
				if(val>=0)
				{
					total+=val;
					count+=10;
					res[4] = val.ToString();
				}
				else
					res[4] = "/";

				val = intCalNaPlus();
				if(val>=0)
				{
					total+=val;
					count+=10;
					res[5] = val.ToString();
				}
				else
					res[5] = "/";

				val = intCalKPlus();
				if(val>=0)
				{
					total+=val;
					count+=10;
					res[6] = val.ToString();
				}
				else
					res[6] = "/";

				val = intCalCrOrBUN();
				if(val>=0)
				{
					total+=val;
					count+=10;
					res[7] = val.ToString();
				}
				else
					res[7] = "/";

				val = intCalHb();
				if(val>=0)
				{
					total+=val;
					count+=10;
					res[8] = val.ToString();
				}
				else
					res[8] = "/";

				val = intCalStomach();
				if(val>=0)
				{
					total+=val;
					count+=10;
					res[9] = val.ToString();
				}
				else
					res[9] = "/";

				if(count!=0)
					total = (total/count)*100;

				if(total==0)
					res[10] = "/";
				else if(total<=70)
					res[10] = total.ToString("0.00")+" : "+"极危重病";
				else if(total<=80)
					res[10] = total.ToString("0.00")+" : "+"危重病";
				else 
					res[10] = total.ToString("0.00")+" : "+"非危重病";

				dtlResult.Rows[0].ItemArray = res;
			}
			catch
			{
			}
		}

		private int intCalHeartRate()
		{
			try
			{
				int intHeartrate;

				try
				{
					intHeartrate = int.Parse(txtHeartRate.Text);
				}
				catch
				{
					return -1;
				}

				if(rdbAgeU1.Checked)
				{
					if(intHeartrate<80 || intHeartrate>180)
						return 4;
					else if(intHeartrate>=80 && intHeartrate<=100)
						return 6;
					else if(intHeartrate>=160 && intHeartrate<=180)
						return 6;

					return 10;
				}
				else
				{
					if(intHeartrate<60 || intHeartrate>160)
						return 4;
					else if(intHeartrate>=60 && intHeartrate<=80)
						return 6;
					else if(intHeartrate>=140 && intHeartrate<=160)
						return 6;

					return 10;
				}
			}
			catch
			{
				return -1;
			}
		}
		private int intCalBloodPressOrShrinkPress()
		{
			try
			{
				if(rdbBloodPress.Checked)
				{
					double dBloodPress;
					try
					{
						dBloodPress = double.Parse(txtBloodPress.Text);
					}
					catch
					{
						return -1;
					}

					if(rdbAgeU1.Checked)
					{
						if(dBloodPress<7.3 || dBloodPress>17.3)
							return 4;
						else if(dBloodPress>=7.3 && dBloodPress<=8.7)
							return 6;
						else if(dBloodPress>=13.3 && dBloodPress<=17.3)
							return 6;

						return 10;
					}
					else
					{
						if(dBloodPress<8.7 || dBloodPress>20.0)
							return 4;
						else if(dBloodPress>=8.7 && dBloodPress<=10.0)
							return 6;
						else if(dBloodPress>=17.3 && dBloodPress<=20.0)
							return 6;

						return 10;
					}
				}
				else
				{
					double dShrinkPress;
					try
					{
						dShrinkPress = double.Parse(txtShrinkPressure.Text);
					}
					catch
					{
						return -1;
					}

					if(rdbAgeU1.Checked)
					{
						if(dShrinkPress<55 || dShrinkPress>130)
							return 4;
						else if(dShrinkPress>=55 && dShrinkPress<=65)
							return 6;
						else if(dShrinkPress>=100 && dShrinkPress<=130)
							return 6;

						return 10;
					}
					else
					{
						if(dShrinkPress<65 || dShrinkPress>150)
							return 4;
						else if(dShrinkPress>=65 && dShrinkPress<=75)
							return 6;
						else if(dShrinkPress>=130 && dShrinkPress<=150)
							return 6;

						return 10;
					}
				}
			}
			catch
			{
				return -1;
			}
		}
		private int intCalBreath()
		{
			try
			{
				if(chkBreath.Checked)
					return 4;

				int iBreath;
				try
				{
					iBreath = int.Parse(txtBreath.Text);
				}
				catch
				{
					return -1;
				}

				if(rdbAgeU1.Checked)
				{
					if(iBreath<20 || iBreath>70)
						return 4;
					else if(iBreath>=20 && iBreath<=25)
						return 6;
					else if(iBreath>=40 && iBreath<=70)
						return 6;
			
					return 10;
				}
				else
				{
					if(iBreath<15 || iBreath>60)
						return 4;
					else if(iBreath>=15 && iBreath<=20)
						return 6;
					else if(iBreath>=35 && iBreath<=60)
						return 6;
			
					return 10;
				}
			}
			catch
			{
				return -1;
			}
		}
		private int intCalPao2()
		{
			try
			{
				double dPao2;

				if(rdbPao2kPa.Checked)
				{
					try
					{
						dPao2 = double.Parse(txtPao2kPa.Text);
					}
					catch
					{
						return -1;
					}

					if(dPao2<6.7)
						return 4;
					else if(dPao2>=6.7 && dPao2<=9.3)
						return 6;

					return 10;
				}
				else
				{
					try
					{
						dPao2 = double.Parse(txtPao2mmHg.Text);
					}
					catch
					{
						return -1;
					}

					if(dPao2<50)
						return 4;
					else if(dPao2>=50 && dPao2<=70)
						return 6;

					return 10;
				}
			}
			catch
			{
				return -1;
			}
		}
		private int intCalpH()
		{
			try
			{
				double dpH;

				try
				{
					dpH = double.Parse(txtpH.Text);
				}
				catch
				{
					return -1;
				}

				if(dpH<7.25 || dpH>7.55)
					return 4;
				else if(dpH>=7.25 && dpH<=7.35)
					return 6;
				else if(dpH>=7.50 && dpH<=7.55)
					return 6;

				return 10;
			}
			catch
			{
				return -1;
			}
		}
		private int intCalNaPlus()
		{
			try
			{
				double dNa;

				try
				{
					dNa = double.Parse(txtNaPlus.Text);
				}
				catch
				{
					return -1;
				}

				if(dNa<120 || dNa>160)
					return 4;
				else if(dNa>=120 && dNa<=130)
					return 6;
				else if(dNa>=150 && dNa<=160)
					return 6;

				return 10;
			}
			catch
			{
				return -1;
			}
		}
		private int intCalKPlus()
		{
			try
			{
				double dK;
			
				try
				{
					dK = double.Parse(txtKPlus.Text);
				}
				catch
				{
					return -1;
				}

				if(dK<3.0 || dK>6.5)
					return 4;
				else if(dK>=3.0 && dK<=3.5)
					return 6;
				else if(dK>=5.5 && dK<=6.5)
					return 6;

				return 10;
			}
			catch
			{
				return -1;
			}
		}
		private int intCalCrOrBUN()
		{
			try
			{
				double dVal;
				if(rdbCrmg.Checked)
				{
					try
					{
						dVal = double.Parse(txtCrmg.Text);
					}
					catch
					{
						return -1;
					}

					if(dVal>1.8)
						return 4;
					else if(dVal>=1.2 && dVal<=1.8)
						return 6;

					return 10;
				}
				else if(rdbCrumol.Checked)
				{
					try
					{
						dVal = double.Parse(txtCrumol.Text);
					}
					catch
					{
						return -1;
					}

					if(dVal>159)
						return 4;
					else if(dVal>=106 && dVal<=159)
						return 6;

					return 10;
				}
				else if(rdbBUNmg.Checked)
				{
					try
					{
						dVal = double.Parse(txtBUNmg.Text);
					}
					catch
					{
						return -1;
					}

					if(dVal>40)
						return 4;
					else if(dVal>=20 && dVal<=40)
						return 6;

					return 10;
				}
				else
				{
					try
					{
						dVal = double.Parse(txtBUNmmol.Text);
					}
					catch
					{
						return -1;
					}

					if(dVal>14.3)
						return 4;
					else if(dVal>=7.1 && dVal<=14.3)
						return 6;
				
					return 10;
				}
			}
			catch
			{
				return -1;
			}
		}

		private int intCalHb()
		{
			try
			{
				double iHb;

				if(rdbHbdl.Checked)
				{
					try
					{
						iHb = double.Parse(txtHbdl.Text);
					}
					catch
					{
						return -1;
					}

					if(iHb<6)
						return 4;
					else if(iHb>=6 && iHb<=9)
						return 6;

					return 10;
				}
				else
				{
					try
					{
						iHb = double.Parse(txtHbL.Text);
					}
					catch
					{
						return -1;
					}

					if(iHb<60)
						return 4;
					else if(iHb>=60 && iHb<=90)
						return 6;

					return 10;
				}		
			}
			catch
			{
				return -1;
			}
		}
		private int intCalStomach()
		{
			try
			{
				switch(cboStomach.SelectedIndex)
				{
					case 0:
						return 4;
					case 1:
						return 6;
					case 2:
						return 10;
					default:
						return -1;
				}
			}
			catch
			{
				return -1;
			}
		}

		private void HbChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(sender.Equals(rdbHbdl) && rdbHbdl.Checked)
				{
					txtHbdl.Enabled = true;
					txtHbL.Enabled = false;
					txtHbL.Text = "";
					intHbSel = int.Parse((string)rdbHbdl.Tag);
				}
				else if(rdbHbL.Checked)
				{
					txtHbdl.Enabled = false;
					txtHbdl.Text = "";
					txtHbL.Enabled = true;
					intHbSel = int.Parse((string)rdbHbL.Tag);
				}
			}
			catch
			{
			}
		}

		private void BreathChange(object sender, System.EventArgs e)
		{
			try
			{
				if(chkBreath.Checked)
				{
					txtBreath.Enabled = false;
					intBreathSel = 1;
				}
				else
				{
					txtBreath.Enabled = true;
					intBreathSel = 0;
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

				intAgeGroupSel = int.Parse((string)((Control)sender).Tag);
			}
			catch
			{}
		}

		private void BabyInjuryCaseEvaluation_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
			frmAutoResult.Close();		
			}		
			catch
			{}
		}

        public override void Print()
		{
			m_lngPrint();
		}

		

		#endregion

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			this.ClearUp();
		}

		#region Clear Up
		protected override void ClearUp()
		{
			try
			{
				object [] res = {"/","/","/","/","/","/","/","/","/","/","/"};
				dtlResult.Rows[0].ItemArray = res;
				foreach(Control control in this.Controls)
				{				
					string typeName = control.GetType().Name;
					if((typeName == "TextBox"||typeName == "ctlBorderTextBox")&&control.Name!="txtCardNo")
					{
						control.Text = "";						
					}
						//				  else if(typeName == "ListView")
						//				  {
						//					  ((ListView)control).Items.Clear();
						//				  }
						//				  else if(typeName == "DataGrid")
						//				  {
						//					  ((System.Data.DataTable)((DataGrid)control).DataSource).Rows.Clear();
						//				  }
					else if(typeName=="CheckBox")
					{
						((CheckBox)control).Checked=false;
					}
					else if(typeName=="dwtFlatComboBox"||typeName == "ctlComboBox")
					{
						//((ctlComboBox)control).SelectedIndex = -1;
						control.Text = "";
					}
				} 				

				rdbAgeU1.Checked = true;
				rdbPao2kPa.Checked = true;
				rdbHbL.Checked = true;
				rdbBloodPress.Checked = true;
				rdbCrumol.Checked = true;
                txtAutoTime.Text = "60";
				cboStomach.SelectedIndex = -1;
				dtpEvalDate.Value = DateTime.Now;
				m_strCreateDate = "";
				txtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;
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

		private BabyInjuryCaseEvaluationDB m_objGetCurrentEvalInfo()
		{			
			string str = "$$";

			BabyInjuryCaseEvaluationDB objBabyInjuryCaseEvaluationDB = new BabyInjuryCaseEvaluationDB();

			objBabyInjuryCaseEvaluationDB.strPatientID = m_strInPatientID;
			objBabyInjuryCaseEvaluationDB.strInPatientDate = m_strInPatientDate;
			objBabyInjuryCaseEvaluationDB.strActivityTime = (m_strCreateDate == "") ? this.dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : m_strCreateDate;			

			objBabyInjuryCaseEvaluationDB.strEvalDoctorID = clsBaseInfo.LoginEmployee.m_strEMPID_CHR;			
			objBabyInjuryCaseEvaluationDB.strIsNewBaby="0";
			objBabyInjuryCaseEvaluationDB.strAgeGroup=this.intAgeGroupSel.ToString();
			objBabyInjuryCaseEvaluationDB.strHeartRate=str + this.txtHeartRate.Text;
			objBabyInjuryCaseEvaluationDB.strBloodOrShrinkPressure=str + this.txtBloodPress.Text+this.txtShrinkPressure.Text;
			objBabyInjuryCaseEvaluationDB.strBldOrShKSel=this.intBldOrShkSel.ToString();;
			objBabyInjuryCaseEvaluationDB.strBreath=str + this.txtBreath.Text;
			objBabyInjuryCaseEvaluationDB.strIsrhythmWrong=intBreathSel.ToString();;
			objBabyInjuryCaseEvaluationDB.strPao2=str + this.txtPao2kPa.Text+this.txtPao2mmHg.Text;
			objBabyInjuryCaseEvaluationDB.strPao0kPaOrmmHgSel=this.intPao2Sel.ToString();
			objBabyInjuryCaseEvaluationDB.strpH=str + this.txtpH.Text;
			objBabyInjuryCaseEvaluationDB.strNaPlus=str + this.txtNaPlus.Text;
			objBabyInjuryCaseEvaluationDB.strKPlus=str + this.txtKPlus.Text;
			objBabyInjuryCaseEvaluationDB.strCrOrBUN=str + this.txtCrmg.Text+this.txtCrumol.Text+this.txtBUNmg.Text+this.txtBUNmmol.Text;
			objBabyInjuryCaseEvaluationDB.strCrOrBUNSel=str + this.intCrOrBUNSel.ToString();
			objBabyInjuryCaseEvaluationDB.strHb=str + this.txtHbdl.Text+this.txtHbL.Text;
			objBabyInjuryCaseEvaluationDB.strHbSel=this.intHbSel.ToString();
			objBabyInjuryCaseEvaluationDB.strStomachAndIntestines=str + this.cboStomach.Text;
			objBabyInjuryCaseEvaluationDB.strHeartRateEval=str + this.dtlResult.Rows[0].ItemArray[0].ToString();
			objBabyInjuryCaseEvaluationDB.strBloodPressureOrShrinkPressureEval=str + this.dtlResult.Rows[0].ItemArray[1].ToString();
			objBabyInjuryCaseEvaluationDB.strBreathEval=str + this.dtlResult.Rows[0].ItemArray[2].ToString();
			objBabyInjuryCaseEvaluationDB.strPao2Eval=str + this.dtlResult.Rows[0].ItemArray[3].ToString();
			objBabyInjuryCaseEvaluationDB.strpHEval=str + this.dtlResult.Rows[0].ItemArray[4].ToString();
			objBabyInjuryCaseEvaluationDB.strNaPlusEval=str + this.dtlResult.Rows[0].ItemArray[5].ToString();
			objBabyInjuryCaseEvaluationDB.strKPlusEval=str + this.dtlResult.Rows[0].ItemArray[6].ToString();
			objBabyInjuryCaseEvaluationDB.strCrOrBUNEval=str + this.dtlResult.Rows[0].ItemArray[7].ToString();
			objBabyInjuryCaseEvaluationDB.strRedCellCompOrHbEval=str + this.dtlResult.Rows[0].ItemArray[8].ToString();
			objBabyInjuryCaseEvaluationDB.strStomachAndintestinesBehaveEval=str + this.dtlResult.Rows[0].ItemArray[9].ToString();
			objBabyInjuryCaseEvaluationDB.strTotalEval=str + this.dtlResult.Rows[0].ItemArray[10].ToString();

			return objBabyInjuryCaseEvaluationDB;
		}
		private long m_lngSaveWithoutMessageBox()
		{
			if(m_strInPatientID == null || m_strInPatientID == "")
			{
				clsPublicFunction.ShowInformationMessageBox("对不起，请输入病人住院编号！");
				return 0;
			}
			this.cmdCalculate_Click(null,null);

			string strCurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

			#region 赋值
			BabyInjuryCaseEvaluationDB objBabyInjuryCaseEvaluationDB = m_objGetCurrentEvalInfo();
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

				BabyInjuryCaseEvaluationDB objTemp;
				long lngExist = objDomain.m_lngGetBabyInjuryCaseValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out objTemp);

				if(lngExist == 0)
					return -11;
				
				if(lngExist == 1)
				{
                    //if(DateTime.Parse(objTemp.strModifyDate) != DateTime.Parse(m_objBabyInjuryCaseEvaluationDB.strModifyDate))
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

				BabyInjuryCaseEvaluationDB objTemp;
				long lngExist = objDomain.m_lngGetBabyInjuryCaseValue(m_strInPatientID,m_strInPatientDate,dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),out objTemp);
				
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

			long lngRes = objDomain.m_lngSave(objBabyInjuryCaseEvaluationDB);
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

		#region 自动评分---已注释
		private void cmdGetData_Click(object sender, System.EventArgs e)
		{	
			if(this.m_objCurrentPatient == null)
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

			string[] strTypeArry=new string[]{"BLOODNUM1","HEARTRATE","NPBSYSTOLIC","RESPRATE"};//
			m_mthGetICUDataByTime(p_strFindDate,out objCMSData,out objVentilatorData,strTypeArry);
			
			if (!blnIsGE)
			{
				if (objCMSData != null)
				{
					txtPao2mmHg.Text = objCMSData.m_strBloodNum1;
					if(txtPao2mmHg.Text != "")
						rdbPao2mmHg.Checked = true;
					
					if(objCMSData.m_strHeartRate == null || objCMSData.m_strHeartRate == "")
						txtHeartRate.Text = "";
					else
						txtHeartRate.Text = m_strFormatShowParamData(objCMSData.m_strHeartRate);//.Substring(0,objCMSData.m_strHeartRate.IndexOf("."));
				
					txtShrinkPressure.Text = objCMSData.m_strPressNum1;
					if(txtShrinkPressure.Text != "")
						rdbShrinkPress.Checked = true;

					if (objVentilatorData!=null)
					{
						if(objVentilatorData.m_strRespRate == null || objVentilatorData.m_strRespRate == "")
							txtBreath.Text = "";
						else
							txtBreath.Text = m_strFormatShowParamData(objVentilatorData.m_strRespRate);//.Substring(0,objVentilatorData.m_strRespRate.IndexOf("."));
					}

				}
			}
			else
			{
                m_mthGetMonitorParamGE();
                clsGECMSData objGECMSData = m_objGECMSData;

				if (objGECMSData!=null)
				{
					if(objGECMSData.m_strRR == null || objGECMSData.m_strRR == "")
						txtBreath.Text = "";
					else
						txtBreath.Text = m_strFormatShowParamData(objGECMSData.m_strRR);//.Substring(0,objGECMSData.m_strHR.IndexOf("."));


					if(objGECMSData.m_strHR == null || objGECMSData.m_strHR == "")
						txtHeartRate.Text = "";
					else
						txtHeartRate.Text = m_strFormatShowParamData(objGECMSData.m_strHR);//.Substring(0,objGECMSData.m_strHR.IndexOf("."));
					
					txtShrinkPressure.Text = objGECMSData.m_strNBPSystolic;
					if(txtShrinkPressure.Text != "")
						rdbShrinkPress.Checked = true;
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
//				clsCMSData objCMSData;
//				clsVentilatorData objVentilatorData;
//				clsGECMSData objGECMSData=null;
//
//				bool blnIsGE=m_blnCurrApparatus();
//				//				objPatientInfo_Base.m_ObjCurrentInHospitalInfo.m_ObjLastICUUtil.m_mthGetICUDataByTime(strSickBedNO,dtpStartSample.Value,out objCMSData,out objVentilatorData);
//				m_mthGetICUDataByTime(dtpStartSample.Value,out objCMSData,out objVentilatorData);
//
//				if (blnIsGE)
//					m_mthGetICUGEDataByTime(dtpStartSample.Value.ToString(),out objGECMSData);
//
//				//				objICUDataDomain.m_mthGetICUDataByTime(strSickBedNO,dtpStartSample.Value,out objCMSData,out objVentilatorData);
//
//				if (!blnIsGE)
//				{
//					if (objCMSData != null)
//					{
//						txtPao2mmHg.Text = objCMSData.m_strBloodNum1;
//						if(txtPao2mmHg.Text != "")
//							rdbPao2mmHg.Checked = true;
//
//				
//						if(objCMSData.m_strHeartRate == null || objCMSData.m_strHeartRate == "")
//							txtHeartRate.Text = "";
//						else
//							txtHeartRate.Text = objCMSData.m_strHeartRate.Substring(0,objCMSData.m_strHeartRate.Length-3);
//
//						txtShrinkPressure.Text = objCMSData.m_strPressNum1;
//						if(txtShrinkPressure.Text != "")
//							rdbShrinkPress.Checked = true;
//				
//						//					lblCMSSampleTime.Text = objCMSData.m_strDataCollectedTime;
//					}
//				}
//				else
//				{
//					if (objGECMSData != null)
//					{
//						txtPao2mmHg.Text = "";
//						if(txtPao2mmHg.Text != "")
//							rdbPao2mmHg.Checked = true;
//
//				
//						if(objGECMSData.m_strHR == null || objGECMSData.m_strHR == "")
//							txtHeartRate.Text = "";
//						else
//							txtHeartRate.Text = objGECMSData.m_strHR;
//
//						txtShrinkPressure.Text = objGECMSData.m_strARTSystolic;
//						if(txtShrinkPressure.Text != "")
//							rdbShrinkPress.Checked = true;
//				
//						//					lblCMSSampleTime.Text = objCMSData.m_strDataCollectedTime;
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
//
//			}
//			catch
//			{
//			}
			#endregion Old
			m_mthGetData(dtpStartSample.Value.ToString());
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
			try
			{
				timAutoCollect.Stop();
				cmdStartAuto.Enabled = true;
				txtAutoTime.Enabled = true;
				cmdStopAuto.Enabled = false;
			}
			catch
			{
			}
		}

		private void cmdShowResult_Click(object sender, System.EventArgs e)
		{
			try
			{
                frmAutoResult.M_lblTitle = "查看结果";
				frmAutoResult.Visible = true;
			}
			catch{}
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
																	 (string)res[10],
																	 "分值："+(string)res[0]+" 测定值："+txtHeartRate.Text,
																	 "分值："+(string)res[1]+" 测定值："+txtShrinkPressure.Text,
																	 "分值："+(string)res[2]+" 测定值："+txtBreath.Text,
																	 "分值："+(string)res[3]+" 测定值："+txtPao2mmHg.Text,
																	 "分值："+(string)res[4]+" 测定值："+txtpH.Text,
																	 "分值："+(string)res[5]+" 测定值："+txtNaPlus.Text,
																	 "分值："+(string)res[6]+" 测定值："+txtKPlus.Text,
																	 "分值："+(string)res[7]+" 测定值："+this.txtCrmg.Text+this.txtCrumol.Text+this.txtBUNmg.Text+this.txtBUNmmol.Text,
																	 "分值："+(string)res[8]+" 测定值："+txtHbdl.Text+txtHbL.Text,
																	 "分值："+(string)res[9]+" 测定值："+cboStomach.Text,
				});
				frmAutoResult.AddResult(item);
				m_AutoSave(dtpStartSample.Value.ToString("yyyy-MM-dd HH:mm:ss"));
			}
			catch
			{
			}
		}

		/// <summary>
		/// alex 2002-9-29
		/// 自动评分所使用的Save
		/// </summary>
		private void m_AutoSave(string p_strAutoEvalTime)
		{
			BabyInjuryCaseEvaluationDB objBabyInjuryCaseEvaluationDB = m_objGetCurrentEvalInfo();
			objBabyInjuryCaseEvaluationDB.strActivityTime=p_strAutoEvalTime;
			
			this.cmdCalculate_Click(null,null);

//			if(objDomain.lngAddNewRecordOfAutoEval(objBabyInjuryCaseEvaluationDB)>0)
			if(objDomain.m_lngSave(objBabyInjuryCaseEvaluationDB) > 0)
			{
				dtpEvalDate.Value = DateTime.Parse(p_strAutoEvalTime);
                TreeNode m_trnNewNode = new TreeNode(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                m_trnNewNode.Tag = DateTime.Parse(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                trvActivityTime.Nodes[0].Nodes.Add(m_trnNewNode); 
			}
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
						if(strSubTypeName != "Label" &&  strSubTypeName != "CheckBox")												
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


			this.txtHeartRate.Text="";			
			this.txtBreath.Text="";
			this.txtBloodPress.Text="";

			clsTrendDomain objDomain=new clsTrendDomain();
			string[] strEMFC_IDArr=new string[]{"40","92","89"};//心率,呼吸,收缩压
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
				this.txtHeartRate.Text=strResultArr[0];
				this.txtBreath.Text=strResultArr[1];
				this.txtBloodPress.Text=strResultArr[2];
			}
		}

		protected clsLabAnalysisOrderDomain m_objCheckResultDomain=new clsLabAnalysisOrderDomain();

		private void m_mthShowDBError()
		{
			clsPublicFunction.ShowInformationMessageBox("对不起，数据库有误！");
		}
		private void m_cmdSetLabCheckResult_Click(object sender, System.EventArgs e)
		{
			if(m_strInPatientID==null || m_strInPatientID=="")
			{
				return;
			}
			m_lsvJY_ItemChoice.Visible=false;
			m_lsvJY_ItemChoice.Items.Clear();
			clsJY_ItemChoice[] objItemChoiceArr=null;
			long lngRes=m_objCheckResultDomain.m_lngGetLabCheckItemChoiceArr(m_strInPatientID,System.DateTime.Now,out objItemChoiceArr);
			if(lngRes<=0)
			{
				m_mthShowDBError();
				return;
			}
			else 
			{
				if(objItemChoiceArr != null)
				{
					for(int i=0;i<objItemChoiceArr.Length;i++)
					{
						ListViewItem lviTemp= m_lsvJY_ItemChoice.Items.Add(objItemChoiceArr[i].m_strPat_c_name.Trim());
						lviTemp.SubItems.Add(objItemChoiceArr[i].m_dtmPat_sdate.ToString("yyyy-MM-dd HH:mm:ss"));
						lviTemp.Tag=objItemChoiceArr[i].m_strRes_id;
					}

					clsPublicFunction.s_mthChangeListViewLastColumnWidth(m_lsvJY_ItemChoice);
					m_lsvJY_ItemChoice.Visible=true;
					m_lsvJY_ItemChoice.BringToFront();
				}
				else 
				{
					clsPublicFunction.ShowInformationMessageBox("当前没有最新检验结果出来！");
					return;
				}
			}	
		}

		private string[] c_strNameArr = new string[]{"PaO2","Hb","pH","Na+","K+","Cr","BUN"};

		private void m_mthHandleLabCheckValue(string p_strName,clsJY_JG p_objResult)
		{
			switch(p_strName)
			{
				case "PaO2":
					m_mthSetLabCheckValue(txtPao2mmHg,p_objResult);
					break;
				case "Hb":
					rdbHbL.Checked = true;
					m_mthSetLabCheckValue(txtHbL,p_objResult);
					break;
				case "pH":
					m_mthSetLabCheckValue(txtpH,p_objResult);
					break;
				case "Na+":
					m_mthSetLabCheckValue(txtNaPlus,p_objResult);
					break;
				case "K+":				
					m_mthSetLabCheckValue(txtKPlus,p_objResult);
					break;
				case "Cr":
					rdbCrumol.Checked = true;
					m_mthSetLabCheckValue(txtCrumol,p_objResult);
					break;
				case "BUN":
					rdbBUNmmol.Checked = true;
					m_mthSetLabCheckValue(txtBUNmmol,p_objResult);
					break;
			}
		}

		private void m_mthSetLabCheckValue(ctlBorderTextBox p_objText,clsJY_JG p_objResult)
		{			
			if(p_objResult.m_strRes_chr != null && p_objResult.m_strRes_chr.Trim() != "")
			{
				p_objText.Text = p_objResult.m_strRes_chr.Trim();
			}
			else if(p_objResult.m_strRes_chr1 != null && p_objResult.m_strRes_chr1.Trim() != "")
			{
				p_objText.Text = p_objResult.m_strRes_chr1.Trim();
			}
		}

		private void m_lsvJY_ItemChoice_Leave(object sender, System.EventArgs e)
		{
			if( !m_lsvJY_ItemChoice.Focused  && !m_cmdSetLabCheckResult.Focused)
				m_lsvJY_ItemChoice.Visible=false;
		}

		
		private void m_lsvJY_ItemChoice_DoubleClick(object sender, System.EventArgs e)
		{
			m_lsvJY_ItemChoice.Visible=false;
			
			if(m_strInPatientID==null || m_strInPatientID=="" || m_lsvJY_ItemChoice.SelectedItems.Count==0 || m_lsvJY_ItemChoice.SelectedItems[0].Tag==null)
			{
				return;
			}
			else
			{
				clsJY_JG[] objResultArr=null;
				long lngRes=m_objCheckResultDomain.m_lngGetLabCheckItemResultArr(m_lsvJY_ItemChoice.SelectedItems[0].Tag.ToString(),c_strNameArr,out objResultArr);
				if(lngRes<=0)
				{
					switch(lngRes)
					{
						case (long)enmOperationResult.Not_permission:
							clsPublicFunction.ShowInformationMessageBox("对不起，您的权限不够！");
							break;
					}
					return;
				}				

				if(objResultArr==null || objResultArr.Length==0 || c_strNameArr==null)
					return;

				for(int i=0;i<c_strNameArr.Length;i++)
					for(int j=0;j<objResultArr.Length;j++)
					{
						if(c_strNameArr[i]==objResultArr[j].m_strRes_it_ecd || c_strNameArr[i]==objResultArr[j].m_strRes_name)
						{
							m_mthHandleLabCheckValue(c_strNameArr[i],objResultArr[j]);
						}
					}
				
			}	
		}

		protected override void m_mthDisplay()
		{
			try
			{
				long lngRes  = objDomain.m_lngGetBabyInjuryCaseValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out m_objBabyInjuryCaseEvaluationDB);

				if(m_objBabyInjuryCaseEvaluationDB == null)
					return;

				this.txtHeartRate.Text =m_objBabyInjuryCaseEvaluationDB.strHeartRate ;

				SetGroupSelText(gpbBloodAndShk,m_objBabyInjuryCaseEvaluationDB.strBloodOrShrinkPressure,m_objBabyInjuryCaseEvaluationDB.strBldOrShKSel );
				
//				SetGroupSelText(gpbAgeGroup,null,m_objBabyInjuryCaseEvaluationDB.strAgeGroup);

				this.txtBreath.Text = m_objBabyInjuryCaseEvaluationDB.strBreath ;

				this.chkBreath.Checked = bool.Parse( m_objBabyInjuryCaseEvaluationDB.strIsrhythmWrong == "1"? "true":"false");

				SetGroupSelText(gpbPao2,m_objBabyInjuryCaseEvaluationDB.strPao2,m_objBabyInjuryCaseEvaluationDB.strPao0kPaOrmmHgSel) ;

				this.txtpH.Text = m_objBabyInjuryCaseEvaluationDB.strpH ;

				this.txtNaPlus.Text = m_objBabyInjuryCaseEvaluationDB.strNaPlus ;

				this.txtKPlus.Text = m_objBabyInjuryCaseEvaluationDB.strKPlus ;

				SetGroupSelText(gpbCrAndBUN,m_objBabyInjuryCaseEvaluationDB.strCrOrBUN,m_objBabyInjuryCaseEvaluationDB.strCrOrBUNSel) ;
				
				SetGroupSelText(gpbHb,m_objBabyInjuryCaseEvaluationDB.strHb,m_objBabyInjuryCaseEvaluationDB.strHbSel );

				this.cboStomach.Text = m_objBabyInjuryCaseEvaluationDB.strStomachAndIntestines ;
                clsEmrEmployeeBase_VO objEmployee = null;
                clsBaseInfo.m_lngGetEmpByID(m_objBabyInjuryCaseEvaluationDB.strEvalDoctorID, out objEmployee);
				
				this.txtEvalDoctor.Text=objEmployee.m_strLASTNAME_VCHR;

				object[] res = new Object[11];

				res[0] = m_objBabyInjuryCaseEvaluationDB.strHeartRateEval ;
				res[1] = m_objBabyInjuryCaseEvaluationDB.strBloodPressureOrShrinkPressureEval ;
				res[2] = m_objBabyInjuryCaseEvaluationDB.strBreathEval ;
				res[3] = m_objBabyInjuryCaseEvaluationDB.strPao2Eval ;
				res[4] = m_objBabyInjuryCaseEvaluationDB.strpHEval ;
				res[5] = m_objBabyInjuryCaseEvaluationDB.strNaPlusEval ;
				res[6] = m_objBabyInjuryCaseEvaluationDB.strKPlusEval ;
				res[7] = m_objBabyInjuryCaseEvaluationDB.strCrOrBUNEval ;
				res[8] = m_objBabyInjuryCaseEvaluationDB.strRedCellCompOrHbEval ;
				res[9] = m_objBabyInjuryCaseEvaluationDB.strStomachAndintestinesBehaveEval ;
				res[10]= m_objBabyInjuryCaseEvaluationDB.strTotalEval ;

				dtlResult.Rows[0].ItemArray = res;

				dtpEvalDate.Value = DateTime.Parse(m_objBabyInjuryCaseEvaluationDB.strActivityTime);
				
			}
			catch
			{
			}
		}

		#region Print Function

		public override void m_mthSetPrint()
		{
			BabyInjuryCaseEvaluationDB objValue;
			objPrintTool=new clsBabyInjury_ValuationPrintTool();
			objPrintTool.m_mthInitPrintTool(null);	
			if(m_objCurrentPatient == null)
				objPrintTool.m_mthSetPrintInfo(null,null,DateTime.MinValue);
			else
			{
				if(this.trvActivityTime.SelectedNode ==null || this.trvActivityTime.SelectedNode==trvActivityTime.Nodes[0] || trvActivityTime.SelectedNode.Tag==null)
					objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient,null,DateTime.MinValue);
				else 
				{
					objDomain.m_lngGetBabyInjuryCaseValue(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),trvActivityTime.SelectedNode.Tag.ToString(),out objValue);
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

            if (!rdbCrmg.Checked)
                txtCrmg.Text = "";

            if (!rdbCrumol.Checked)
                txtCrumol.Text = "";

            if (!rdbBUNmg.Checked)
                txtBUNmg.Text = "";

            if (!rdbBUNmmol.Checked)
                txtBUNmmol.Text = "";
		}


	}
}
