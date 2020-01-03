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
	/// 急性肺损伤评分
	/// </summary>
	public partial class frmLungInjuryEvaluation : frmValuationBaseForm,PublicFunction
	{
		#region Define
		private System.Windows.Forms.Label lblEvalDate;
		private com.digitalwave.Utility.Controls.ctlTimePicker dtpEvalDate;
		private com.digitalwave.Utility.Controls.ctlComboBox  cboXRayEval;
		private System.Windows.Forms.GroupBox gpbLowOxygenBlood;
		private System.Windows.Forms.RadioButton rdbOxygenkPa;
		private System.Windows.Forms.RadioButton rdbOxygenmmHg;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtPao2kPa;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtPao2mmHg;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtFio2kPa;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtFio2mmHg;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtPEEP;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtOxyGenValkPa;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtOxyGenValmmHg;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtVt;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtPIP;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtLungPEEP;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtLungSysHumorVal;

		private DataTable dtlResult;
		private System.Windows.Forms.DataGrid dtgResult;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
		private System.Windows.Forms.Label lblTitle6;
		private System.Windows.Forms.Label lblTitle7;
		private System.Windows.Forms.Label lblTitle21;
		private System.Windows.Forms.Label lblTitle20;
		private System.Windows.Forms.Label lblTitle8;
		private System.Windows.Forms.Label lblTitle9;
		private System.Windows.Forms.Label lblTitle11;
		private System.Windows.Forms.Label lblTitle10;
		private System.Windows.Forms.Label lblTitle12;
		private System.Windows.Forms.Label lblTitle13;
		private System.Windows.Forms.Label lblTitle16;
		private System.Windows.Forms.Label lblTitle14;
		private System.Windows.Forms.Label lblTitle22;
		private System.Windows.Forms.Label lblTitle23;
		private System.Windows.Forms.Label lblTitle17;
		private System.Windows.Forms.Label lblTitle18;
		private System.Windows.Forms.Label lblTitle24;
		private System.Windows.Forms.Label lblTitle25;
		private System.Windows.Forms.Label lblTitle26;
		private System.Windows.Forms.Label lblTitle27;
		private System.Windows.Forms.Label lblTitle15;
		private System.Windows.Forms.Label lblTitle19;
		private System.Windows.Forms.Label lblTitle28;
		private System.Windows.Forms.Label lblTitle29;
		private System.Windows.Forms.Label lblTitle30;
		private System.Windows.Forms.Label lblTitle35;
		private System.Windows.Forms.Label lblTitle31;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		
		int intOxygenSel;
		private System.Windows.Forms.Label lblTitle96;
		private System.Windows.Forms.Label label2;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox txtAutoTime;
		private System.Timers.Timer timAutoCollect;
		private com.digitalwave.Utility.Controls.ctlTimePicker dtpStartSample;

		private System.ComponentModel.Container components = null;
		public string strSickBedNO;
		private System.Windows.Forms.Label lbltxtEvalDoctor;
		private System.Windows.Forms.GroupBox gpbAutoEval;
        private clsLungInjuryEvaluationDomain objDomain;
        private System.Windows.Forms.Label label1;
		private PinkieControls.ButtonXP cmdGetData;
		private PinkieControls.ButtonXP cmdShowResult;
		private PinkieControls.ButtonXP cmdStopAuto;
		private PinkieControls.ButtonXP cmdStartAuto;
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
		public frmLungInjuryEvaluation()
		{
			InitializeComponent();

			this.cboXRayEval.AddRangeItems(new object[] {	"无肺泡实变",
															"肺泡实变达 1 个肺象限",
															"肺泡实变达 2 个肺象限",
															"肺泡实变达 3 个肺象限",
															"肺泡实变达全部 4 个肺象限"});


			dtlResult = new DataTable("result");
			intOxygenSel = 0;

			ColumnHeader chResult1 = new ColumnHeader();
			chResult1.Text = "胸部 X- 光照片评分";
			chResult1.Width = 180;

			ColumnHeader chResult2 = new ColumnHeader();
			chResult2.Text = "低氧血症评分";
			chResult2.Width = 180;

			ColumnHeader chResult3 = new ColumnHeader();
			chResult3.Text = "PEEP评分（机械通气性评分）";
			chResult3.Width = 200;

			ColumnHeader chResult4 = new ColumnHeader();
			chResult4.Text = "肺系统顺应性评分";
			chResult4.Width = 150;

			ColumnHeader chResult5 = new ColumnHeader();
			chResult5.Text = "总分";
			chResult5.Width = 100;

			
		
			frmAutoResult = new frmAutoEvalResult(chResult5,chResult1,chResult2,chResult3,chResult4);
            frmAutoResult.Visible = false;
			frmAutoResult.Text = "急性肺损伤自动评分";

//			frmAutoResult.Show();
			
			frmAutoResult.Owner = this;

			m_objBorderTool = new com.digitalwave.Utility.Controls.clsBorderTool(Color.White);

			m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{
																			 dtgResult,trvActivityTime
																		 });
			objDomain = new clsLungInjuryEvaluationDomain();

			strAddOrModify = "0";

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

		private frmAutoEvalResult frmAutoResult=null;

		private clsEvalInfoOfclsLungInjuryEvaluation m_objLungInjuryEvaluation;

		private clsSystemContext m_objCurrentContext
		{
			get
			{
				return clsSystemContext.s_ObjCurrentContext;
			}
		}

		#endregion
		

		#region Event
		private void OxygenUnitChange(object sender, System.EventArgs e)
		{
			try
			{
				if(sender.Equals(rdbOxygenkPa) && rdbOxygenkPa.Checked)
				{
					txtPao2kPa.Enabled = true;
					txtFio2kPa.Enabled = true;
					txtOxyGenValkPa.Enabled = true;
					txtPao2mmHg.Enabled = false;
					txtPao2mmHg.Text = "";
					txtFio2mmHg.Enabled = false;
					txtFio2mmHg.Text = "";
					txtOxyGenValmmHg.Enabled = false;
					txtOxyGenValmmHg.Text = "";
					intOxygenSel = int.Parse((string)rdbOxygenkPa.Tag);
				}
				else if(rdbOxygenmmHg.Checked)
				{
					txtPao2kPa.Enabled = false;
					txtPao2kPa.Text = "";
					txtFio2kPa.Enabled = false;
					txtFio2kPa.Text = "";
					txtOxyGenValkPa.Enabled = false;
					txtOxyGenValkPa.Text = "";
					txtPao2mmHg.Enabled = true;
					txtFio2mmHg.Enabled = true;
					txtOxyGenValmmHg.Enabled = true;
					intOxygenSel = int.Parse((string)rdbOxygenmmHg.Tag);
				}
			}
			catch
			{
			}
		}

		private void MakePEEPSame(object sender, System.EventArgs e)
		{
			try
			{
				if(sender.Equals(txtPEEP))
				{
					txtLungPEEP.Text = txtPEEP.Text;
				}
				else
					txtPEEP.Text = txtLungPEEP.Text;
			}
			catch
			{
			}
		}

		private void cmdCalculate_Click(object sender, System.EventArgs e)
		{
			try
			{
				int xray = intCalXRay();
				int oxygen = intCalOxygen();
				int peep = intCalPEEP();
				int lung = intCalLung();
				int count = 0;
				int total = 0;
				object []res = dtlResult.Rows[0].ItemArray;
			
				if(xray>=0)
				{
					count++;
					total += xray;
					res[0] = xray.ToString();
				}
				else
					res[0] = "/";

				if(oxygen>=0)
				{
					count++;
					total += oxygen;
					res[1] = oxygen.ToString();
				}
				else
					res[1] = "/";

				if(peep>=0)
				{
					count++;
					total += peep;
					res[2] = peep.ToString();
				}
				else
					res[2] = "/";

				if(lung>=0)
				{
					count++;
					total += lung;
					res[3] = lung.ToString();
				}
				else
					res[3] = "/";

				if(count == 0)
				{
					res[4] = "/";
				}
				else
				{
					double result = (double)((double)total/(double)count);
					string strResult = "/";
					if(result>=2.5)
						strResult=result.ToString("0.00")+" : 重度肺损伤（ARDS）";
					else if(result>=0.1 && result<2.5)
						strResult=result.ToString("0.00")+" : 轻－中度肺损伤";
					else if(result>=0 && result <0.1)
						strResult=result.ToString("0.00")+" : 无肺损伤";
					res[4] = strResult;			
				}

				dtlResult.Rows[0].ItemArray = res;
			}
			catch
			{
			}

		}
		#endregion

		#region Tools
		private int intCalXRay()
		{
			try
			{
				//2003-5-13 wingo 修改
//				int ret = cboXRayEval.SelectedIndex-1;
				int ret = cboXRayEval.SelectedIndex;
				return ret;
			}
			catch
			{
				return -1;
			}
		}


		private int intCalOxygen()
		{
			try
			{
				double pa;
				double fi;
				double val;
				try
				{
					if(rdbOxygenkPa.Checked)
					{
						pa = double.Parse(txtPao2kPa.Text);
						fi = double.Parse(txtFio2kPa.Text);
						val = pa/fi;
						txtOxyGenValkPa.Text = val.ToString("0.00");
					}
					else
					{
						pa = double.Parse(txtPao2mmHg.Text);
						fi = double.Parse(txtFio2mmHg.Text);
						val = pa/fi;
						txtOxyGenValmmHg.Text = val.ToString("0.00");
					}
				}
				catch
				{
					return -1;
				}
            
				int ret = -1;
				if(val>=300)
					ret = 0;
				else if(val>=225 && val<300)
					ret = 1;
				else if(val>=175 && val <225)
					ret = 2;
				else if(val>=100 && val<175)
					ret = 3;
				else if(val<100)
					ret = 4;

				return ret;
			}
			catch
			{
				return -1;
			}
		}

		private int intCalPEEP()
		{
			try
			{
				double val;
				try
				{
					val = double.Parse(txtPEEP.Text);
				}
				catch
				{
					return -1;
				}

				int ret = -1;

				if(val<=5 && val >= 0)
					ret = 0;
				else if(val>=6 && val<9)
					ret = 1;
				else if(val>=9 && val<12)
					ret = 2;
				else if(val>=12 && val<15)
					ret = 3;
				else if(val>=15)
					ret = 4;
			
				return ret;
			}
			catch
			{
				return -1;
			}
		}

		private int intCalLung()
		{
			try
			{
				double Vt;
				double PIP;
				double PEEP;
				double val;
				try
				{
					Vt = double.Parse(txtVt.Text);
					PIP = double.Parse(txtPIP.Text);
					PEEP = double.Parse(txtLungPEEP.Text);
					val = Vt/(PIP-PEEP);
				
					txtLungSysHumorVal.Text = val.ToString("0.00");
				}
				catch
				{
					return -1;
				}
				int ret = -1;

				if(val>=80)
					ret = 0;
				else if(val>=60 && val<80)
					ret = 1;
				else if(val>=40 && val<60)
					ret = 2;
				else if(val>=20 && val<40)
					ret = 3;
				else if(val<20)
					ret = 4;

				return ret;
			}
			catch
			{
				return -1;
			}
		}
		#endregion

		public void ReceiveID(string strID)
		{
			if(strID.Trim().Length !=0)
			{
				
			}
		}
		private void LungInjuryEvaluation_Load(object sender, System.EventArgs e)
		{
			try
			{
				dtlResult.Columns.Add("胸部 X- 光照片评分");
				dtlResult.Columns.Add("低氧血症评分");
				dtlResult.Columns.Add("PEEP评分（机械通气性评分）");
				dtlResult.Columns.Add("肺系统顺应性评分");
				dtlResult.Columns.Add("总分");

				dtgResult.DataSource = dtlResult;

				dtlResult.Rows.Add(new string[]{"/","/","/","/","/"});
				m_objHighLight.m_mthAddControlInContainer(this);

				//this.dtpEvalDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
				this.dtpEvalDate.m_mthResetSize();
			}
			catch
			{
			}
		}	

		#region interface


		#region null Impletement
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
#if FunctionPrivilege
			if(!MDIParent.s_blnCheckCurrentPrivilege(lbltxtSetionOffice.Tag.ToString(),PrivilegeData.enmPrivilegeSF.LungInjuryEvaluation,PrivilegeData.enmPrivilegeOperation.Delete))
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


        public override void Save()
		{
#if FunctionPrivilege
			if(!MDIParent.s_blnCheckCurrentPrivilege(lbltxtSetionOffice.Tag.ToString(),PrivilegeData.enmPrivilegeSF.LungInjuryEvaluation,PrivilegeData.enmPrivilegeOperation.AddOrModify))
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
		
		#region Clear Up
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
					else if(typeName=="CheckBox")
					{
						((CheckBox)control).Checked=false;
					}
//					else if(typeName=="dwtFlatComboBox")
//					{
//						((dwtFlatComboBox)control).SelectedIndex = -1;
//					}
				}
				foreach(Control control in this.gpbLowOxygenBlood.Controls)
				{
					string typeName=control.GetType().Name;
					if(typeName == "TextBox"&&control.Name!="txtCardNo")
					{
						control.Text = "";
					}
				}
                txtAutoTime.Text = "60";
				cboXRayEval.SelectedIndex = -1;
				txtPao2kPa.Text = "";
				txtFio2kPa.Text = "";
				txtOxyGenValkPa.Text = "";

				txtPao2mmHg.Text = "";
				txtFio2mmHg.Text = "";
				txtOxyGenValmmHg.Text = "";
				txtPEEP.Text = "";
				txtLungSysHumorVal.Text = "";
				txtVt.Text = "";
				txtPIP.Text = "";
				txtLungPEEP.Text = "";
				dtpEvalDate.Value = DateTime.Now;
				lbltxtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;

				m_strCreateDate = "";
				strAddOrModify = "0";
			}
			catch
			{
			}
		}

		private void ctmClearUp_Click(object sender, System.EventArgs e)
		{
			ClearUp();
		}

		private void m_mthClearPatientInfo()
		{			
			m_strInPatientID = "";
			m_strInPatientDate = "";
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

			this.cmdCalculate_Click(null,null);

			clsEvalInfoOfclsLungInjuryEvaluation objLungInjuryEvaluation = m_objGetCurrentEvalInfo();

			if(m_strCreateDate != "")
			{
                //if(m_objCurrentContext.m_ObjControl.m_enmModifyCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
                //    == enmDBControlCheckResult.Disable)
                //{
                //    clsPublicFunction.s_mthShowNotPermitMessage();
                //    return 0;

                //}
				clsEvalInfoOfclsLungInjuryEvaluation objTemp;
				long lngExist = objDomain.m_lngGetLungInjuryValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out objTemp);

				if(lngExist == 0)
					return -11;
				
				if(lngExist == 1)
				{
                    //if(DateTime.Parse(objTemp.strModifyDate) != DateTime.Parse(m_objLungInjuryEvaluation.strModifyDate))
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

				clsEvalInfoOfclsLungInjuryEvaluation objTemp;
				long lngExist = objDomain.m_lngGetLungInjuryValue(m_strInPatientID,m_strInPatientDate,dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),out objTemp);
				
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

			long lngRes = objDomain.m_lngSave(objLungInjuryEvaluation);

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

		private clsEvalInfoOfclsLungInjuryEvaluation m_objGetCurrentEvalInfo()
		{
			string str = "$$";
			clsEvalInfoOfclsLungInjuryEvaluation objEvalInfo = new clsEvalInfoOfclsLungInjuryEvaluation();

			objEvalInfo.strPatientID = m_strInPatientID;
			objEvalInfo.strInPatientDate = m_strInPatientDate;
			objEvalInfo.strActivityTime = (m_strCreateDate == "") ? this.dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : m_strCreateDate;

			objEvalInfo.strEvalDoctorID = clsBaseInfo.LoginEmployee.m_strEMPID_CHR;

			objEvalInfo.strLungXRay = str + cboXRayEval.Text.Trim();

			if(rdbOxygenkPa.Checked)
			{
				objEvalInfo.strPao2 = str + txtPao2kPa.Text.Trim();
				objEvalInfo.strFio2 = str + txtFio2kPa.Text.Trim();
				objEvalInfo.strPao2AndFio2Sel = "0";
				objEvalInfo.strLowOxygenBlood = str + txtOxyGenValkPa.Text.Trim();
			}

			if(rdbOxygenmmHg.Checked)
			{
				objEvalInfo.strPao2 = str + txtPao2mmHg.Text.Trim();
				objEvalInfo.strFio2 = str + txtFio2mmHg.Text.Trim();
				objEvalInfo.strPao2AndFio2Sel = "1";
				objEvalInfo.strLowOxygenBlood = str + txtOxyGenValmmHg.Text.Trim();
			}

			objEvalInfo.strPEEP = str + txtPEEP.Text.Trim();
			objEvalInfo.strVt = str + txtVt.Text.Trim();
			objEvalInfo.strPIP = str + txtPIP.Text.Trim();
			objEvalInfo.strLungSysHumor = str + txtLungSysHumorVal.Text.Trim();

			objEvalInfo.strLungXRayEval =str + dtlResult.Rows[0].ItemArray[0].ToString();
			objEvalInfo.strLowOxygenBloodEval = str + dtlResult.Rows[0].ItemArray[1].ToString();
			objEvalInfo.strPEEPEval = str + dtlResult.Rows[0].ItemArray[2].ToString();
			objEvalInfo.strLungSysHumorEval =str + dtlResult.Rows[0].ItemArray[3].ToString();
			objEvalInfo.strTotalEval = str + dtlResult.Rows[0].ItemArray[4].ToString();

			return objEvalInfo;
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

		#region Display

		protected override void m_mthDisplay()
		{
			long lngRes  = objDomain.m_lngGetLungInjuryValue(m_strInPatientID,m_strInPatientDate,m_strCreateDate,out m_objLungInjuryEvaluation);

			if(m_objLungInjuryEvaluation == null)
				return;
            clsEmrEmployeeBase_VO objEmployee = null;
            clsBaseInfo.m_lngGetEmpByID(m_objLungInjuryEvaluation.strEvalDoctorID, out objEmployee);
			
			this.lbltxtEvalDoctor.Text = objEmployee.m_strLASTNAME_VCHR;

			this.cboXRayEval.Text = m_objLungInjuryEvaluation.strLungXRay ;

			SetGroupSelText(gpbLowOxygenBlood,m_objLungInjuryEvaluation.strPao2 ,m_objLungInjuryEvaluation.strFio2 ,m_objLungInjuryEvaluation.strLowOxygenBlood,m_objLungInjuryEvaluation.strPao2AndFio2Sel);

			this.txtPEEP.Text = m_objLungInjuryEvaluation.strPEEP;
			this.txtVt.Text = m_objLungInjuryEvaluation.strVt ;
			this.txtPIP.Text = m_objLungInjuryEvaluation.strPIP ;
			this.txtLungSysHumorVal.Text = m_objLungInjuryEvaluation.strLungSysHumor;

			object []res = dtlResult.Rows[0].ItemArray;

			res[0] = m_objLungInjuryEvaluation.strLungXRayEval;
			res[1] = m_objLungInjuryEvaluation.strLowOxygenBloodEval;
			res[2] = m_objLungInjuryEvaluation.strPEEPEval;
			res[3] = m_objLungInjuryEvaluation.strLungSysHumorEval;
			res[4] = m_objLungInjuryEvaluation.strTotalEval;
			dtlResult.Rows[0].ItemArray = res;

			strAddOrModify = "1";
		}

		private void SetGroupSelText(GroupBox gpb,string strPao2Text,string strFio2Text,string strLowOxygenText,string tabindex)
		{
			try
			{
				foreach(Control control in gpb.Controls)
				{
					if(control.GetType().Name == "Label")
						continue;

					if(control.Tag.ToString() == tabindex)
					{
						if(control.Name.StartsWith("rdbOxygen"))
							((RadioButton)control).Checked = true;
						else if(control.Name.StartsWith("txtPao2"))
						{
							control.Text = strPao2Text;						
						}
						else if(control.Name.StartsWith("txtFio2"))
							control.Text = strFio2Text;
						else if(control.Name.StartsWith("txtOxy"))
							control.Text = strLowOxygenText;
					
						control.Enabled = true;
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
//				clsEvalInfoOfclsLungInjuryEvaluation objEvalInfo = m_GetCurrentEvalInfo();
				clsEvalInfoOfclsLungInjuryEvaluation objEvalInfo = m_objGetCurrentEvalInfo();
				objEvalInfo.strActivityTime = m_strAutoEvalDate;
//				if(objDomain.lngAddNewRecordOfAutoEval(objEvalInfo)>0)
				if(objDomain.m_lngSave(objEvalInfo)>0)
				{
					dtpEvalDate.Value = DateTime.Parse(m_strAutoEvalDate);
                    TreeNode m_trnNewNode = new TreeNode(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    m_trnNewNode.Tag = DateTime.Parse(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    trvActivityTime.Nodes[0].Nodes.Add(m_trnNewNode); 
				}
			}
		}

		private clsEvalInfoOfclsLungInjuryEvaluation m_GetCurrentEvalInfo()
		{
			clsEvalInfoOfclsLungInjuryEvaluation objEvalInfo = new clsEvalInfoOfclsLungInjuryEvaluation();
			objEvalInfo.strPatientID = m_strInPatientID;
			objEvalInfo.strActivityTime = "";
			objEvalInfo.strEvalDoctorID = clsBaseInfo.LoginEmployee.m_strEMPID_CHR;
			objEvalInfo.strEvalDoctorName = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;
			objEvalInfo.strLungXRay = cboXRayEval.Text.Trim();
			if(rdbOxygenkPa.Checked)
			{
				objEvalInfo.strPao2 = txtPao2kPa.Text.Trim();
				objEvalInfo.strFio2 = txtFio2kPa.Text.Trim();
				objEvalInfo.strPao2AndFio2Sel = "0";
				objEvalInfo.strLowOxygenBlood = txtOxyGenValkPa.Text.Trim();
			}
			if(rdbOxygenmmHg.Checked)
			{
				objEvalInfo.strPao2 = txtPao2mmHg.Text.Trim();
				objEvalInfo.strFio2 = txtFio2mmHg.Text.Trim();
				objEvalInfo.strPao2AndFio2Sel = "1";
				objEvalInfo.strLowOxygenBlood = txtOxyGenValmmHg.Text.Trim();
			}
			objEvalInfo.strPEEP = txtPEEP.Text.Trim();
			objEvalInfo.strVt = txtVt.Text.Trim();
			objEvalInfo.strPIP = txtPIP.Text.Trim();
			objEvalInfo.strLungSysHumor = txtLungSysHumorVal.Text.Trim();
			objEvalInfo.strLungXRayEval =dtlResult.Rows[0].ItemArray[0].ToString();
			objEvalInfo.strLowOxygenBloodEval = dtlResult.Rows[0].ItemArray[1].ToString();
			objEvalInfo.strPEEPEval = dtlResult.Rows[0].ItemArray[2].ToString();
			objEvalInfo.strLungSysHumorEval =dtlResult.Rows[0].ItemArray[3].ToString();
			objEvalInfo.strTotalEval = dtlResult.Rows[0].ItemArray[4].ToString();

			return objEvalInfo;
		}

		private void cmdGetData_Click(object sender, System.EventArgs e)
		{			
			if(m_objCurrentPatient == null)
			{
				clsPublicFunction.ShowInformationMessageBox("请输入床号");
				return;
			}
			GetData();
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
			catch
			{
			}
		}

		private void cmdShowResult_Click(object sender, System.EventArgs e)
		{
            frmAutoResult.M_lblTitle = "查看结果";
            frmAutoResult.Visible = true;
		}

		private void m_mthGetData(string p_strFindDate)
		{
			txtOxyGenValkPa.Text = "";
			txtOxyGenValmmHg.Text = "";
			txtLungSysHumorVal.Text = "";	

			bool blnIsGE=m_blnCurrApparatus();

			clsCMSData objCMSData=null;
			clsVentilatorData objVentilatorData=null;

            if (m_objCurrentPatient == null) return;

			string[] strTypeArry=new string[]{"BLOODNUM1","O2CONCENTRATION","ENDEXPPRESSURE","EXPTIDALVOLUME","PEAKPRESSURE"};//体温，心率，呼吸
			m_mthGetICUDataByTime(p_strFindDate,out objCMSData,out objVentilatorData,strTypeArry);

			if (!blnIsGE)
			{

				if (objCMSData!=null)
				{
					txtPao2mmHg.Text = objCMSData.m_strBloodNum1;
				}
				else
				{
					txtPao2mmHg.Text = "";
				}
			}
			else
			{
				#region 由于GE仪器现在未取到此项数据此处暂不处理
                //clsGECMSData objGECMSData = null;
                //objGECMSData = m_objICUGESimulateGetData.M_objNumericParam;
                m_mthGetMonitorParamGE();
                clsGECMSData objGECMSData = m_objGECMSData;
                if (objGECMSData == null)
                    m_mthGetICUGEDataByTime(p_strFindDate, out objGECMSData);
                if (objGECMSData != null)
                {
                    txtPao2mmHg.Text = objCMSData.m_strBloodNum1;
                }
                else
                {
                    txtPao2mmHg.Text = "";
                }
				#endregion 由于GE仪器现在未取到此项数据此处暂不处理
			}

			if (objVentilatorData!=null)
			{
				txtFio2mmHg.Text =objVentilatorData.m_strO2Concentration;
				
				txtPEEP.Text = objVentilatorData.m_strEndExpPressure;
				
				txtVt.Text = objVentilatorData.m_strExpTidalVolume;
				
				txtPIP.Text = objVentilatorData.m_strPeakPressure;
			}
			else
			{
				txtFio2mmHg.Text ="";
				
				txtPEEP.Text = "";
				
				txtVt.Text = "";
				
				txtPIP.Text = "";
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
//				//在getdata之前，清空其计算值。Alex 2002-10-16
//				txtOxyGenValkPa.Text = "";
//				txtOxyGenValmmHg.Text = "";
//				txtLungSysHumorVal.Text = "";
//				//End
//
//				XmlDocument objXMLDoc = new XmlDocument();
//
//				clsCMSData objCMSData=null;
//				clsVentilatorData objVentilatorData=null;
//				clsGECMSData objGECMSData=null;
//
//				bool blnIsGE=m_blnCurrApparatus();
//
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
//					
//						//						lblCMSSampleTime.Text = objCMSData.m_strDataCollectedTime;
//					}
//				}
//				else
//				{
//					if (objCMSData != null)
//					{
//						txtPao2mmHg.Text = "";
//					}
//				}
//				if(objVentilatorData != null)
//				{
//					txtFio2mmHg.Text =objVentilatorData.m_strO2Concentration;
//
//					txtPEEP.Text = objVentilatorData.m_strEndExpPressure;
//
//					txtVt.Text = objVentilatorData.m_strExpTidalVolume;
//
//					txtPIP.Text = objVentilatorData.m_strPeakPressure;
//				
//					//					lblVenSampleTime.Text = objVentilatorData.m_strDataCollectedTime;
//				}
//				if(txtPao2mmHg.Text+txtFio2mmHg != "")
//					rdbOxygenmmHg.Checked = true;
//			}
//			catch
//			{
//			}
			#endregion Old
			try
			{
				m_mthGetData(dtpStartSample.Value.ToString());
			}
			catch
			{
			}
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
																	 "分值："+(string)res[0]+" 条件："+cboXRayEval.Text,
																	 "分值："+(string)res[1]+" 条件："+txtOxyGenValmmHg.Text,
																	 "分值："+(string)res[2]+" 条件："+txtPEEP.Text,
																	 "分值："+(string)res[3]+" 条件："+txtLungSysHumorVal.Text,
				});
				frmAutoResult.AddResult(item);
				m_AutoSave(dtpStartSample.Value.ToString("yyyy-MM-dd HH:mm:ss"));//Alex 2002-9-29
			}
			catch
			{
			}
		}

		private void LungInjuryEvaluation_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			frmAutoResult.Close();
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

		#region Print Function

		public override void m_mthSetPrint()
		{
			clsEvalInfoOfclsLungInjuryEvaluation objValue;
			objPrintTool=new clsLungInjury_ValuationPrintTool();
			objPrintTool.m_mthInitPrintTool(null);	
			if(m_objCurrentPatient == null)
				objPrintTool.m_mthSetPrintInfo(null,null,DateTime.MinValue);
			else
			{
				if(this.trvActivityTime.SelectedNode ==null || this.trvActivityTime.SelectedNode==trvActivityTime.Nodes[0] || trvActivityTime.SelectedNode.Tag==null)
					objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient,null,DateTime.MinValue);
				else 
				{
					objDomain.m_lngGetLungInjuryValue(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),trvActivityTime.SelectedNode.Tag.ToString(),out objValue);
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

		private void groupBox3_Enter(object sender, System.EventArgs e)
		{
		
		}

	}
}
