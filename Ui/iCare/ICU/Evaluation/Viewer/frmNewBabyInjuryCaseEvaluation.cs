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

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// 新生儿危重病例评分法（草案）
	/// </summary>
	public partial class frmNewBabyInjuryCaseEvaluation : frmValuationBaseForm,PublicFunction
	{
		#region Define

        public com.digitalwave.Utility.Controls.ctlTimePicker dtpEvalDate;
		private System.Windows.Forms.Label lblEvalDate;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtHeartRate;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBloodPress;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtShrinkPressure;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBreath;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtpH;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtCrumol;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtNaPlus;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtCrmg;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBUNmmol;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBUNmg;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtRedCellComp;
		private System.Windows.Forms.RadioButton rdbBloodPress;
		private System.Windows.Forms.RadioButton rdbShrinkPress;
		private System.Windows.Forms.RadioButton rdbCrumol;
		private System.Windows.Forms.RadioButton rdbPao2kPa;
		private System.Windows.Forms.RadioButton rdbPao2mmHg;
		private System.Windows.Forms.RadioButton rdbBUNmg;
		private System.Windows.Forms.RadioButton rdbBUNmmol;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtPao2kPa;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtPao2mmHg;
		private System.Windows.Forms.Label label7;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtKPlus;
        private com.digitalwave.Utility.Controls.ctlComboBox cboStomach;
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
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn9;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn10;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn11;
		private System.Windows.Forms.Label lblTitle31;
		private System.Windows.Forms.Label lblTitle2;
		private System.Windows.Forms.Label lblTitle5;
		private System.Windows.Forms.Label lblTitle8;
		private System.Windows.Forms.Label lblTitle9;
		private System.Windows.Forms.Label lblTitle11;
		private System.Windows.Forms.Label lblTitle12;
		private System.Windows.Forms.Label lblTitle14;
		private System.Windows.Forms.Label lblTitle15;
		private System.Windows.Forms.Label lblTitle16;
		private System.Windows.Forms.Label lblTitle17;
		private System.Windows.Forms.Label lblTitle18;
		private System.Windows.Forms.Label lblTitle19;
		private System.Windows.Forms.Label lblTitle20;
		private System.Windows.Forms.Label lblTitle21;
		private System.Windows.Forms.Label lblTitle13;
		private System.Windows.Forms.Label lblTitle23;
		private System.Windows.Forms.Label lblTitle25;
		private System.Windows.Forms.Label lblTitle26;
		private System.Windows.Forms.Label lblTitle28;
		private System.Windows.Forms.GroupBox gpbCrAndBUN;
		private System.Windows.Forms.Label lblTitle6;
		private System.Windows.Forms.Label lblTitle10;
		private System.Windows.Forms.GroupBox gpbBloodAndShk;
		private System.Windows.Forms.GroupBox gpbPao2;
		private System.Windows.Forms.Label lblTitle3;
		private System.Windows.Forms.Label lblTitle4;

		private DataTable dtlResult;
		private int intBldOrShkSel;
		private int intPao2Sel;
		private int intCrOrBUN;
        private com.digitalwave.Utility.Controls.ctlTimePicker dtpStartSample;
		private System.Windows.Forms.Label lblTitle96;
		private System.Windows.Forms.Label label2;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtAutoTime;
		private System.Timers.Timer timAutoCollect;
		private System.Windows.Forms.RadioButton rdbCrmg;
		
		public string strSickBedNO;
		private System.Windows.Forms.GroupBox gpbAutoEval;
        private System.Windows.Forms.Label lbltxtEvalDoctor;		
		private PinkieControls.ButtonXP cmdGetData;
		private PinkieControls.ButtonXP cmdStartAuto;
		private PinkieControls.ButtonXP cmdStopAuto;
		private PinkieControls.ButtonXP cmdShowResult;
        private PinkieControls.ButtonXP cmdCalculate;
		private PinkieControls.ButtonXP m_cmdGetCheckData;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;

		/// <summary>
		/// //设置状态 "0--增加" "1--修改"
		/// </summary>
		private string strAddOrModify;
		#endregion

		#region Constructor
		public frmNewBabyInjuryCaseEvaluation()
		{		

			InitializeComponent();

			this.cboStomach.AddRangeItems(new object[] {
															"腹胀并消化道出血",
															"腹胀或消化道出血",
															"其余"});
			dtlResult = new DataTable("result");
			intBldOrShkSel = 0;
			intPao2Sel = 0;
			intCrOrBUN = 0;

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
			chResult9.Text = "红细胞积压比";
			chResult9.Width = 120;

			ColumnHeader chResult10 = new ColumnHeader();
			chResult10.Text = "胃肠表现";
			chResult10.Width = 100;

			ColumnHeader chResult11 = new ColumnHeader();
			chResult11.Text = "总分";
			chResult11.Width = 100;

			
		
			frmAutoResult = new frmAutoEvalResult(chResult11,chResult1,chResult2,chResult3,chResult4,chResult5,chResult6,
				chResult7,chResult8,chResult9,chResult10);

			frmAutoResult.Text = "小儿危重病例自动评分";

			m_objBorderTool = new com.digitalwave.Utility.Controls.clsBorderTool(Color.White);
			m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{
																			 dtgResult,trvActivityTime
																		 });
			objDomain = new clsNewBabyInjuryCaseEvaluationDomain();

			strAddOrModify = "0";

			frmAutoResult.Owner = this;
			frmAutoResult.Visible = false;
//			objICUDataDomain = new clsICUDataDomain();

			m_mthSetQuickKeys();

			m_objHighLight = new ctlHighLightFocus(clsHRPColor.s_ClrHightLight);

//			gpbAutoEval.Visible=false;
		}
		#endregion

		protected ctlHighLightFocus m_objHighLight;

		#region Member
		private const string _strTypeMonitor = "Monitoring";
		private const string _strTypeVentilator = "Ventilator";
        private com.digitalwave.Utility.Controls.clsBorderTool m_objBorderTool;

		private frmAutoEvalResult frmAutoResult;

		private clsNewBabyInjuryCaseEvaluationDomain objDomain;

//		private clsICUDataDomain objICUDataDomain;

		private clsEvalInfoOfNewBabyInjuryCaseEvaluation m_objNewBabyInjuryCaseEvaluationDB;

		private clsSystemContext m_objCurrentContext
		{
			get
			{
				return clsSystemContext.s_ObjCurrentContext;
			}
		}

		#endregion

		
		#region 自动评分
		public void ReceiveID(string strID)
		{
			if(strID.Trim().Length !=0)
			{
				
//				this.Display(strID,"");
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
					intPao2Sel = int.Parse((string)(rdbPao2kPa.Tag));
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
					intCrOrBUN = int.Parse((string)rdbCrmg.Tag);
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
					intCrOrBUN = int.Parse((string)rdbCrumol.Tag);
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
					intCrOrBUN = int.Parse((string)rdbBUNmg.Tag);
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
					intCrOrBUN = int.Parse((string)rdbBUNmmol.Tag);
				}
			}
			catch
			{
			}
		}
		#endregion


		private void NewBabyInjuryCaseEvaluation_Load(object sender, System.EventArgs e)
		{
			dtlResult.Columns.Add("心率");
			dtlResult.Columns.Add("血压（收缩压）");
			dtlResult.Columns.Add("呼吸");
			dtlResult.Columns.Add("PaO2");
			dtlResult.Columns.Add("pH");
			dtlResult.Columns.Add("Na+");
			dtlResult.Columns.Add("K+");
			dtlResult.Columns.Add("Cr 或 BUN");
			dtlResult.Columns.Add("红细胞积压比");
			dtlResult.Columns.Add("胃肠表现");
			dtlResult.Columns.Add("总分");

			dtgResult.DataSource = dtlResult;

			dtlResult.Rows.Add(new string[]{"/","/","/","/","/","/","/","/","/","/","/"});			
			
			m_objHighLight.m_mthAddControlInContainer(this);

			//this.dtpEvalDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
			this.dtpEvalDate.m_mthResetSize();

		}
		#region 计算数值
		private void cmdCalculate_Click(object sender, System.EventArgs e)
		{
			try
			{
				object [] res = dtlResult.Rows[0].ItemArray;
				double total = 0;
				double count = 0;
				int val;
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

				val = intCalRedCellComp();
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

				if(intHeartrate<80 || intHeartrate>180)
					return 4;
				else if(intHeartrate>=80 && intHeartrate<=100)
					return 6;
				else if(intHeartrate>=160 && intHeartrate<=180)
					return 6;

				return 10;
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
					double dblBloodPress;
					try
					{
						dblBloodPress = double.Parse(txtBloodPress.Text);
					}
					catch
					{
						return -1;
					}

					if(dblBloodPress<5.3 || dblBloodPress>13.3)
						return 4;
					else if(dblBloodPress>=5.3 && dblBloodPress<=6.7)
						return 6;
					else if(dblBloodPress>=12.0 && dblBloodPress<=13.3)
						return 6;

					return 10;
				}
				else
				{
					double dblShrinkPress;
					try
					{
						dblShrinkPress = double.Parse(txtShrinkPressure.Text);
					}
					catch
					{
						return -1;
					}

					if(dblShrinkPress<40 || dblShrinkPress>100)
						return 4;
					else if(dblShrinkPress>=40 && dblShrinkPress<=50)
						return 6;
					else if(dblShrinkPress>=90 && dblShrinkPress<=100)
						return 6;

					return 10;
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
				int intBreath;
				try
				{
					intBreath = int.Parse(txtBreath.Text);
				}
				catch
				{
					return -1;
				}

				if(intBreath<20 || intBreath>100)
					return 4;
				else if(intBreath>=20 && intBreath<=25)
					return 6;
				else if(intBreath>=60 && intBreath<=100)
					return 6;
			
				return 10;
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
				double dblPao2;

				if(rdbPao2kPa.Checked)
				{
					try
					{
						dblPao2 = double.Parse(txtPao2kPa.Text);
					}
					catch
					{
						return -1;
					}

					if(dblPao2<6.7)
						return 4;
					else if(dblPao2>=6.7 && dblPao2<=8.0)
						return 6;

					return 10;
				}
				else
				{
					try
					{
						dblPao2 = double.Parse(txtPao2mmHg.Text);
					}
					catch
					{
						return -1;
					}

					if(dblPao2<50)
						return 4;
					else if(dblPao2>=50 && dblPao2<=60)
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
				double dblpH;

				try
				{
					dblpH = double.Parse(txtpH.Text);
				}
				catch
				{
					return -1;
				}

				if(dblpH<7.25 || dblpH>7.55)
					return 4;
				else if(dblpH>=7.25 && dblpH<=7.30)
					return 6;
				else if(dblpH>=7.50 && dblpH<=7.55)
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
				double dblNa;

				try
				{
					dblNa = double.Parse(txtNaPlus.Text);
				}
				catch
				{
					return -1;
				}

				if(dblNa<120 || dblNa>160)
					return 4;
				else if(dblNa>=120 && dblNa<=130)
					return 6;
				else if(dblNa>=150 && dblNa<=160)
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
				double dblK;
			
				try
				{
					dblK = double.Parse(txtKPlus.Text);
				}
				catch
				{
					return -1;
				}

				if(dblK<2.0 || dblK>9.0)
					return 4;
				else if(dblK>=2.0 && dblK<=2.9)
					return 6;
				else if(dblK>=7.5 && dblK<=9.0)
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
				double dblVal;
				if(rdbCrmg.Checked)
				{
					try
					{
						dblVal = double.Parse(txtCrmg.Text);
					}
					catch
					{
						return -1;
					}

					if(dblVal>1.5)
						return 4;
					else if(dblVal>=1.3 && dblVal<=1.5)
						return 6;

					return 10;
				}
				else if(rdbCrumol.Checked)
				{
					try
					{
						dblVal = double.Parse(txtCrumol.Text);
					}
					catch
					{
						return -1;
					}

					if(dblVal>132.6)
						return 4;
					else if(dblVal>=114.0 && dblVal<=132.6)
						return 6;

					return 10;
				}
				else if(rdbBUNmg.Checked)
				{
					try
					{
						dblVal = double.Parse(txtBUNmg.Text);
					}
					catch
					{
						return -1;
					}

					if(dblVal>40)
						return 4;
					else if(dblVal>=20 && dblVal<=40)
						return 6;

					return 10;
				}
				else
				{
					try
					{
						dblVal = double.Parse(txtBUNmmol.Text);
					}
					catch
					{
						return -1;
					}

					if(dblVal>14.3)
						return 4;
					else if(dblVal>=7.1 && dblVal<=14.3)
						return 6;
				
					return 10;
				}
			}
			catch
			{
				return -1;
			}
		}
		private int intCalRedCellComp()
		{
			
			try
			{
				double dblRed;

				try
				{
					dblRed = double.Parse(txtRedCellComp.Text);
				}
				catch
				{
					return -1;
				}

				if(dblRed<0.2)
					return 4;
				else if(dblRed>=0.2 && dblRed<=0.4)
					return 6;

				return 10;
			}
			catch
			{
				return -1;
			}
		}
		private int intCalStomach()
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

		#endregion


		#region Public Function

		#region Null Implement
		/// <summary>
		/// 已经失效 Alex 2002-9-28
		/// </summary>
		/// <param name="cardno"></param>
		/// <param name="ActivityTime"></param>
		public void Display(string cardno, string ActivityTime)
		{
			
		}
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

		public void Display()
		{
		
		}
		public void Print()
		{
			m_lngPrint();
		}
		#endregion

		public override void Delete()
		{
#if FunctionPrivilege
			if(!MDIParent.s_blnCheckCurrentPrivilege(lbltxtSetionOffice.Tag.ToString(),PrivilegeData.enmPrivilegeSF.NewBabyInjuryCaseEvaluation,PrivilegeData.enmPrivilegeOperation.Delete))
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

		public void Save()
		{
#if FunctionPrivilege
			if(!MDIParent.s_blnCheckCurrentPrivilege(lbltxtSetionOffice.Tag.ToString(),PrivilegeData.enmPrivilegeSF.NewBabyInjuryCaseEvaluation,PrivilegeData.enmPrivilegeOperation.AddOrModify))
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
		

		#region ClearUp
		protected override void ClearUp()
		{
			
			try
			{
				//this.lsvCardNo.Visible=false;
				object [] res = {"/","/","/","/","/","/","/","/","/","/","/"};
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
//					else if(typeName=="dwtFlatComboBox")
//					{
//						((dwtFlatComboBox)control).SelectedIndex = -1;
//					}
				}
				foreach(Control control in this.gpbBloodAndShk.Controls)
				{				
					string typeName = control.GetType().Name;
					if(typeName == "TextBox"&&control.Name!="txtCardNo")
					{
						control.Text = "";
					}
				
				}
				foreach(Control control in this.gpbCrAndBUN.Controls)
				{				
					string typeName = control.GetType().Name;
					if(typeName == "TextBox"&&control.Name!="txtCardNo")
					{
						control.Text = "";
					}
				
				}
				foreach(Control control in this.gpbPao2.Controls)
				{				
					string typeName = control.GetType().Name;
					if(typeName == "TextBox"&&control.Name!="txtCardNo")
					{
						control.Text = "";
					}
				
				}
                txtAutoTime.Text = "60";
				txtPao2kPa.Text = "";
				txtPao2mmHg.Text = "";
				txtBloodPress.Text = "";
				txtShrinkPressure.Text = "";
				txtCrumol.Text = "";
				txtCrmg.Text = "";
				txtBUNmmol.Text = "";
				txtBUNmg.Text = "";
				txtHeartRate.Text = "";
				txtBreath.Text = "";
				txtpH.Text = "";
				txtNaPlus.Text = "";
				txtKPlus.Text = "";
				txtRedCellComp.Text = "";
				cboStomach.SelectedIndex = -1;
				rdbPao2kPa.Checked = true;
				rdbBloodPress.Checked = true;
				rdbCrumol.Checked = true;
				dtpEvalDate.Value = DateTime.Now;
				lbltxtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;

				m_strCreateDate = "";
				cboStomach.SelectedIndex = -1;
				strAddOrModify = "0";
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


		private void ctmClearUp_Click(object sender, System.EventArgs e)
		{
			ClearUp();
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

			clsEvalInfoOfNewBabyInjuryCaseEvaluation objBabyInjuryCaseEvaluationDB = m_objGetCurrentEvalInfo();

			

			if(m_strCreateDate != "")
			{
                //if(m_objCurrentContext.m_ObjControl.m_enmModifyCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
                //    == enmDBControlCheckResult.Disable)
                //{
                //    clsPublicFunction.s_mthShowNotPermitMessage();
                //    return 0;
                //}

				clsEvalInfoOfNewBabyInjuryCaseEvaluation objTemp;
				long lngExist = objDomain.m_lngGetNewBabyInjuryCaseValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out objTemp);

				if(lngExist == 0)
					return -11;
				
				if(lngExist == 1)
				{
                    //if(DateTime.Parse(objTemp.strModifyDate) != DateTime.Parse(m_objNewBabyInjuryCaseEvaluationDB.strModifyDate))
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

				clsEvalInfoOfNewBabyInjuryCaseEvaluation objTemp;
				long lngExist = objDomain.m_lngGetNewBabyInjuryCaseValue(m_strInPatientID,m_strInPatientDate,dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),out objTemp);
				
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

		private clsEvalInfoOfNewBabyInjuryCaseEvaluation m_objGetCurrentEvalInfo()
		{
			clsEvalInfoOfNewBabyInjuryCaseEvaluation objEvalInfo = new clsEvalInfoOfNewBabyInjuryCaseEvaluation();

			objEvalInfo.strPatientID = m_strInPatientID;
			objEvalInfo.strInPatientDate = m_strInPatientDate;
			objEvalInfo.strActivityTime = (m_strCreateDate == "") ? this.dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : m_strCreateDate;

			objEvalInfo.strIsNewBaby = "1";
			objEvalInfo.strEvalDoctorID = clsBaseInfo.LoginEmployee.m_strEMPID_CHR;
			objEvalInfo.strAgeGroup = "";
			objEvalInfo.strHeartRate = "$$" + txtHeartRate.Text.Trim();

			if(rdbBloodPress.Checked)
			{
				objEvalInfo.strBloodOrShrinkPressure = txtBloodPress.Text.Trim();
				objEvalInfo.strBldOrShKSel = "0";
			}

			if(rdbShrinkPress.Checked)
			{
				objEvalInfo.strBloodOrShrinkPressure = "$$" + txtShrinkPressure.Text.Trim();
				objEvalInfo.strBldOrShKSel = "1";
			}

			objEvalInfo.strBreath = "$$" + txtBreath.Text.Trim();

			objEvalInfo.strIsrhythmWrong = "";

			if(rdbPao2kPa.Checked)
			{
				objEvalInfo.strPao2 = "$$" + txtPao2kPa.Text.Trim();
				objEvalInfo.strPao0kPaOrmmHgSel = "0";
			}

			if(rdbPao2mmHg.Checked)
			{
				objEvalInfo.strPao2 = "$$" + txtPao2mmHg.Text.Trim();
				objEvalInfo.strPao0kPaOrmmHgSel = "1";
			}

			objEvalInfo.strpH = "$$" + txtpH.Text.Trim();
			objEvalInfo.strNaPlus = "$$" + txtNaPlus.Text.Trim();
			objEvalInfo.strKPlus = "$$" + txtKPlus.Text.Trim();

			if(rdbCrumol.Checked)
			{
				objEvalInfo.strCrOrBUN = "$$" + txtCrumol.Text.Trim();
				objEvalInfo.strCrOrBUNSel = "0";
			}
			if(rdbCrmg.Checked)
			{
				objEvalInfo.strCrOrBUN = "$$" + txtCrmg.Text.Trim();
				objEvalInfo.strCrOrBUNSel = "1";
			}
			if(rdbBUNmmol.Checked)
			{
				objEvalInfo.strCrOrBUN = "$$" + txtBUNmmol.Text.Trim();
				objEvalInfo.strCrOrBUNSel = "2";
			}
			if(rdbBUNmg.Checked)
			{
				objEvalInfo.strCrOrBUN = "$$" + txtBUNmg.Text.Trim();
				objEvalInfo.strCrOrBUNSel = "3";
			}

			objEvalInfo.strRedCellComp = "$$" + txtRedCellComp.Text.Trim();
			objEvalInfo.strHb = "";
			objEvalInfo.strHbSel = "";
			objEvalInfo.strStomachAndIntestines = cboStomach.Text.Trim();

			objEvalInfo.strHeartRateEval ="$$" + dtlResult.Rows[0].ItemArray[0].ToString();
			objEvalInfo.strBloodPressureOrShrinkPressureEval = "$$" + dtlResult.Rows[0].ItemArray[1].ToString();
			objEvalInfo.strBreathEval = "$$" + dtlResult.Rows[0].ItemArray[2].ToString();
			objEvalInfo.strPao2Eval ="$$" + dtlResult.Rows[0].ItemArray[3].ToString();
			objEvalInfo.strpHEval = "$$" + dtlResult.Rows[0].ItemArray[4].ToString();
			objEvalInfo.strNaPlusEval ="$$" + dtlResult.Rows[0].ItemArray[5].ToString();
			objEvalInfo.strKPlusEval = "$$" + dtlResult.Rows[0].ItemArray[6].ToString();
			objEvalInfo.strCrOrBUNEval = "$$" + dtlResult.Rows[0].ItemArray[7].ToString();
			objEvalInfo.strRedCellCompOrHbEval ="$$" + dtlResult.Rows[0].ItemArray[8].ToString();
			objEvalInfo.strStomachAndintestinesBehaveEval = "$$" + dtlResult.Rows[0].ItemArray[9].ToString();
			objEvalInfo.strTotalEval = "$$" + dtlResult.Rows[0].ItemArray[10].ToString();

			return objEvalInfo;
		}
		#endregion

		#region 自动评分 -- 已注释
		/// <summary>
		/// 自定评分使用
		/// Alex 2002-9-29
		/// </summary>
		public void m_AutoSave(string m_strAutoEvalDate)
		{
			this.cmdCalculate_Click(null,null);
			if(strAddOrModify == "0")
			{
				clsEvalInfoOfNewBabyInjuryCaseEvaluation objEvalInfo = m_objGetCurrentEvalInfo();
				objEvalInfo.strActivityTime = m_strAutoEvalDate;
//				if(objDomain.lngAddNewRecordOfAutoEval(objEvalInfo)>0)
				if(objDomain.m_lngSave(objEvalInfo) > 0)
				{
					dtpEvalDate.Value = DateTime.Parse(m_strAutoEvalDate);
                    TreeNode m_trnNewNode = new TreeNode(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    m_trnNewNode.Tag = DateTime.Parse(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    trvActivityTime.Nodes[0].Nodes.Add(m_trnNewNode); 
				}
			}
		}

		private void NewBabyInjuryCaseEvaluation_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			frmAutoResult.Close();
		}
		#endregion

		#region Display
		protected override void m_mthDisplay()
		{
			try
			{
				long lngRes  = objDomain.m_lngGetNewBabyInjuryCaseValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out m_objNewBabyInjuryCaseEvaluationDB);

				if(m_objNewBabyInjuryCaseEvaluationDB == null)
					return;

				this.txtHeartRate.Text =m_objNewBabyInjuryCaseEvaluationDB.strHeartRate ;

				SetGroupSelText(gpbBloodAndShk,m_objNewBabyInjuryCaseEvaluationDB.strBloodOrShrinkPressure,m_objNewBabyInjuryCaseEvaluationDB.strBldOrShKSel );
				
//				SetGroupSelText(gpbAgeGroup,null,m_objNewBabyInjuryCaseEvaluationDB.strAgeGroup);

				this.txtBreath.Text = m_objNewBabyInjuryCaseEvaluationDB.strBreath ;

//				this.chkBreath.Checked = bool.Parse( m_objNewBabyInjuryCaseEvaluationDB.strIsrhythmWrong);

				SetGroupSelText(gpbPao2,m_objNewBabyInjuryCaseEvaluationDB.strPao2,m_objNewBabyInjuryCaseEvaluationDB.strPao0kPaOrmmHgSel) ;

				this.txtpH.Text = m_objNewBabyInjuryCaseEvaluationDB.strpH ;
				this.txtRedCellComp.Text = m_objNewBabyInjuryCaseEvaluationDB.strRedCellComp;

				this.txtNaPlus.Text = m_objNewBabyInjuryCaseEvaluationDB.strNaPlus ;

				this.txtKPlus.Text = m_objNewBabyInjuryCaseEvaluationDB.strKPlus ;

				SetGroupSelText(gpbCrAndBUN,m_objNewBabyInjuryCaseEvaluationDB.strCrOrBUN,m_objNewBabyInjuryCaseEvaluationDB.strCrOrBUNSel) ;
				
//				SetGroupSelText(gpbHb,m_objNewBabyInjuryCaseEvaluationDB.strHb,m_objNewBabyInjuryCaseEvaluationDB.strHbSel );

				this.cboStomach.Text = m_objNewBabyInjuryCaseEvaluationDB.strStomachAndIntestines ;
                clsEmrEmployeeBase_VO objEmployee = null;
                clsBaseInfo.m_lngGetEmpByID(m_objNewBabyInjuryCaseEvaluationDB.strEvalDoctorID, out objEmployee);
				
				this.lbltxtEvalDoctor.Text=objEmployee.m_strLASTNAME_VCHR;

				object[] res = new Object[11];

				res[0] = m_objNewBabyInjuryCaseEvaluationDB.strHeartRateEval ;
				res[1] = m_objNewBabyInjuryCaseEvaluationDB.strBloodPressureOrShrinkPressureEval ;
				res[2] = m_objNewBabyInjuryCaseEvaluationDB.strBreathEval ;
				res[3] = m_objNewBabyInjuryCaseEvaluationDB.strPao2Eval ;
				res[4] = m_objNewBabyInjuryCaseEvaluationDB.strpHEval ;
				res[5] = m_objNewBabyInjuryCaseEvaluationDB.strNaPlusEval ;
				res[6] = m_objNewBabyInjuryCaseEvaluationDB.strKPlusEval ;
				res[7] = m_objNewBabyInjuryCaseEvaluationDB.strCrOrBUNEval ;
				res[8] = m_objNewBabyInjuryCaseEvaluationDB.strRedCellCompOrHbEval ;
				res[9] = m_objNewBabyInjuryCaseEvaluationDB.strStomachAndintestinesBehaveEval ;
				res[10]= m_objNewBabyInjuryCaseEvaluationDB.strTotalEval ;

				dtlResult.Rows[0].ItemArray = res;

				dtpEvalDate.Value = DateTime.Parse(m_objNewBabyInjuryCaseEvaluationDB.strActivityTime);

				strAddOrModify = "1";				
			}
			catch
			{
			}
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
			catch
			{
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

		private string[] c_strNameArr = new string[]{"K+","Na+","pH","PaO2","红细胞积压比","Cr","BUN"};

		private void m_mthHandleLabCheckValue(string p_strName,clsJY_JG p_objResult)
		{
			switch(p_strName)
			{
				case "K+":				
					m_mthSetLabCheckValue(txtKPlus,p_objResult);
					break;
				case "Na+":
					m_mthSetLabCheckValue(txtNaPlus,p_objResult);
					break;		
				case "pH":
					m_mthSetLabCheckValue(txtpH,p_objResult);
					break;	
				case "PaO2":
					rdbPao2mmHg.Checked = true;
					m_mthSetLabCheckValue(txtPao2mmHg,p_objResult);
					break;
				case "红细胞积压比":
					m_mthSetLabCheckValue(txtRedCellComp,p_objResult);
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
				if (objCMSData!=null)
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
							txtBreath.Text = m_strFormatShowParamData(objVentilatorData.m_strRespRate);//.Substring(0,objVentilatorData.m_strRespRate.Length-3);
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
						txtBreath.Text = m_strFormatShowParamData(objGECMSData.m_strRR);

					if(objGECMSData.m_strHR == null || objGECMSData.m_strHR == "")
						txtHeartRate.Text = "";
					else
						txtHeartRate.Text = m_strFormatShowParamData(objGECMSData.m_strHR);//.Substring(0,objGECMSData.m_strHR.IndexOf("."));
					
					txtShrinkPressure.Text = m_strFormatShowParamData(objGECMSData.m_strNBPSystolic);
					if(txtShrinkPressure.Text != "")
						rdbShrinkPress.Checked = true;
				}
			}
		}

		#region Get ICU Data
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
//				//				objSelectedPatient.m_ObjCurrentInHospitalInfo.m_ObjLastICUUtil.m_mthGetICUDataByTime(strSickBedNO,dtpStartSample.Value,out objCMSData,out objVentilatorData);
//				m_mthGetICUDataByTime(dtpStartSample.Value,out objCMSData,out objVentilatorData);
//				if (blnIsGE)
//					m_mthGetICUGEDataByTime(dtpStartSample.Value.ToString(),out objGECMSData);
//
//				//				objICUDataDomain.m_mthGetICUDataByTime(strSickBedNO,dtpStartSample.Value,out objCMSData,out objVentilatorData);
//				if (!blnIsGE)
//				{
//					if (objCMSData != null)
//					{
//						txtPao2mmHg.Text = objCMSData.m_strBloodNum1;
//						if(txtPao2mmHg.Text != "")
//							rdbPao2mmHg.Checked = true;
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
//						if(objGECMSData.m_strHR == null || objGECMSData.m_strHR == "")
//							txtHeartRate.Text = "";
//						else
//							txtHeartRate.Text = objGECMSData.m_strHR;
//
//						txtShrinkPressure.Text = objGECMSData.m_strARTSystolic;
//						if(txtShrinkPressure.Text != "")
//							rdbShrinkPress.Checked = true;
//					}
//				}
//				if(objVentilatorData != null)
//				{
//					if(objVentilatorData.m_strRespRate == null || objVentilatorData.m_strRespRate == "")
//						txtBreath.Text = "";
//					else
//						txtBreath.Text = objVentilatorData.m_strRespRate.Substring(0,objVentilatorData.m_strRespRate.Length-3);
//				
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
			try
			{
				if(this.m_objCurrentPatient == null)
				{
					clsPublicFunction.ShowInformationMessageBox("请输入床号");
					return;
				}
				GetData();	
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
																	 "分值："+(string)res[8]+" 测定值："+txtRedCellComp.Text,
																	 "分值："+(string)res[9]+" 测定值："+cboStomach.Text,
				});
				frmAutoResult.AddResult(item);
				m_AutoSave(dtpStartSample.Value.ToString("yyyy-MM-dd HH:mm:ss"));
			}
			catch
			{
			}
		}

		private void cmdStartAuto_Click(object sender, System.EventArgs e)
		{
			//Alex 2002-9-29 增加两个判断控制
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
			try
			{
				timAutoCollect.Stop();
				cmdStartAuto.Enabled = true;
				txtAutoTime.Enabled = true;
				cmdStopAuto.Enabled = false;
			}
			catch{}
		}

		private void cmdShowResult_Click(object sender, System.EventArgs e)
		{
            frmAutoResult.M_lblTitle = "查看结果";
            frmAutoResult.Visible = true;
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
		#endregion

		#region Print Function

		public override void m_mthSetPrint()
		{
			clsEvalInfoOfNewBabyInjuryCaseEvaluation objValue;
			objPrintTool=new clsNewBabyInjury_ValuationPrintTool();
			objPrintTool.m_mthInitPrintTool(null);	
			if(m_objCurrentPatient == null)
				objPrintTool.m_mthSetPrintInfo(null,null,DateTime.MinValue);
			else
			{
				if(this.trvActivityTime.SelectedNode ==null || this.trvActivityTime.SelectedNode==trvActivityTime.Nodes[0] || trvActivityTime.SelectedNode.Tag==null)
					objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient,null,DateTime.MinValue);
				else 
				{
					objDomain.m_lngGetNewBabyInjuryCaseValue(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),trvActivityTime.SelectedNode.Tag.ToString(),out objValue);
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

		private void dtgResult_Navigate(object sender, System.Windows.Forms.NavigateEventArgs ne)
		{
		
		}

	}
}
