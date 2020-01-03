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
    /// modified by swh-2002-9-25
    /// APACHEIII评分
    /// medified by alex 2002-9-29
    /// 增加自动评分
    /// //been
    /// </summary>
    public partial class frmAPACHEIIIValuation : frmValuationBaseForm, PublicFunction
    {
        #region Define
        private System.Windows.Forms.Label lblEvalDate;
        private System.Windows.Forms.TabControl tabAPACHEIIValuation;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox gpbExactLife;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBreath;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtAmountLeucocyte;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtTemperature;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtAdvArteryPress;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtHR;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBloodCorpuscle;
        private System.Windows.Forms.DataGrid dtgResult;
        public com.digitalwave.Utility.Controls.ctlTimePicker dtpEvalDate;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtEvalDoctor;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtProteid;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblTitleHR4;
        private System.Windows.Forms.Label lblTitleHR;
        private System.Windows.Forms.Label lblTitle7;
        private System.Windows.Forms.Label lblTitle4;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtHematuria;
        private System.Windows.Forms.Label lblTitleHR1;
        private System.Windows.Forms.Label lblTitleHR2;
        private System.Windows.Forms.Label lblTitle9;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBloodNa;
        private System.Windows.Forms.Label lblTitleHR3;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtUrineAmount;
        private System.Windows.Forms.Label lblTitle3;
        private System.Windows.Forms.Label lblTitle8;
        private System.Windows.Forms.Label lblTitleHR5;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtHypercholesterolemia;
        private System.Windows.Forms.Label lblTitleHR6;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBloodGallbladder;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Label lblSportChanged;
        private System.Windows.Forms.GroupBox gpbAcheAndLanguage1;
        private System.Windows.Forms.CheckBox chkAccording;
        private System.Windows.Forms.CheckBox chkPositionAche;
        private System.Windows.Forms.CheckBox chkBodyBendAndVertical;
        private System.Windows.Forms.CheckBox chkBrainUnreaction;
        private System.Windows.Forms.RadioButton rdbRight;
        private System.Windows.Forms.RadioButton rdbConfusion;
        private System.Windows.Forms.RadioButton rdbBlur;
        private System.Windows.Forms.RadioButton rdbUnreaction;
        private System.Windows.Forms.RadioButton rdbCanOpenEyes;
        private System.Windows.Forms.RadioButton rdbCannotOpenEyes;
        private System.Windows.Forms.CheckBox chkMachineAerate;
        private System.Windows.Forms.CheckBox chkKidneyWane;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBloodFlesh;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtPCO2;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtPH;
        private System.Windows.Forms.Label lblPCO2;
        private System.Windows.Forms.Label lblPH;
        private APACHEIIIValuationDomain objDomain;


        /// <summary>
        /// Required designer variable.
        /// </summary>
        private DataTable dtlResult;
        private System.Windows.Forms.GroupBox gpbOpenEye;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
        private System.ComponentModel.Container components = null;
        public string strSickBedNO;
        private com.digitalwave.Utility.Controls.ctlTimePicker dtpStartSample;
        private System.Windows.Forms.Label lblTitle96;
        private System.Windows.Forms.Label label4;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtAutoTime;
        private System.Timers.Timer timAutoCollect;
        private System.Windows.Forms.Button m_cmdGetDovueData;
        private com.digitalwave.Utility.Controls.clsBorderTool m_objBorderTool;
        #endregion


        //private clsCommonUseToolCollection m_objCUTC;
        //定义签名类
        private clsEmrSignToolCollection m_objSign = new clsEmrSignToolCollection();

        #region Constructor
        public frmAPACHEIIIValuation()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            dtlResult = new DataTable("result");
            //	this.txtInHospitalNO.LostFocus += new System.EventHandler(this.txtInHospitalNO_LostFocus);
            //	this.lsvInHospitalNO.LostFocus += new System.EventHandler(this.txtInHospitalNO_LostFocus);
            objDomain = new APACHEIIIValuationDomain();

            m_objBorderTool = new com.digitalwave.Utility.Controls.clsBorderTool(Color.White);
            m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{
                                                                             trvActivityTime,dtgResult
                                                                         });
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            ColumnHeader chResult1 = new ColumnHeader();
            chResult1.Text = "心率";
            chResult1.Width = 50;

            ColumnHeader chResult2 = new ColumnHeader();
            chResult2.Text = "平均动脉压";
            chResult2.Width = 100;

            ColumnHeader chResult3 = new ColumnHeader();
            chResult3.Text = "体温";
            chResult3.Width = 50;

            ColumnHeader chResult4 = new ColumnHeader();
            chResult4.Text = "呼吸频率";
            chResult4.Width = 100;

            ColumnHeader chResult5 = new ColumnHeader();
            chResult5.Text = "PaO2";
            chResult5.Width = 50;

            ColumnHeader chResult6 = new ColumnHeader();
            chResult6.Text = "FiO2";
            chResult6.Width = 50;

            ColumnHeader chResult7 = new ColumnHeader();
            chResult7.Text = "（A－a）DO ";
            chResult7.Width = 100;

            ColumnHeader chResult8 = new ColumnHeader();
            chResult8.Text = "血细胞比容";
            chResult8.Width = 120;

            ColumnHeader chResult9 = new ColumnHeader();
            chResult9.Text = "白细胞计数";
            chResult9.Width = 120;

            ColumnHeader chResult10 = new ColumnHeader();
            chResult10.Text = "血肌酐浓度";
            chResult10.Width = 120;

            ColumnHeader chResult11 = new ColumnHeader();
            chResult11.Text = "总胆红素";
            chResult11.Width = 100;

            ColumnHeader chResult12 = new ColumnHeader();
            chResult12.Text = "PH";
            chResult12.Width = 50;

            ColumnHeader chResult13 = new ColumnHeader();
            chResult13.Text = "尿量";
            chResult13.Width = 50;

            ColumnHeader chResult14 = new ColumnHeader();
            chResult14.Text = "血尿素氮";
            chResult14.Width = 100;

            ColumnHeader chResult15 = new ColumnHeader();
            chResult15.Text = "血钠白蛋白";
            chResult15.Width = 100;

            ColumnHeader chResult16 = new ColumnHeader();
            chResult16.Text = "白蛋白";
            chResult16.Width = 80;

            ColumnHeader chResult17 = new ColumnHeader();
            chResult17.Text = "血糖";
            chResult17.Width = 50;

            ColumnHeader chResult18 = new ColumnHeader();
            chResult18.Text = "PCO2";
            chResult18.Width = 50;

            ColumnHeader chResult19 = new ColumnHeader();
            chResult19.Text = "总分";
            chResult19.Width = 100;

            frmAutoResult = new frmAutoEvalResult(chResult19, chResult1, chResult2, chResult3, chResult4, chResult5, chResult6, chResult7, chResult8, chResult9, chResult10, chResult11, chResult12, chResult13, chResult14, chResult15, chResult16, chResult17, chResult18);

            frmAutoResult.Text = "APACHEIII自动诊断";

            frmAutoResult.Owner = this;

            //			frmAutoResult.Show();
            frmAutoResult.Visible = false;

            m_mthSetQuickKeys();

            m_objHighLight = new ctlHighLightFocus(clsHRPColor.s_ClrHightLight);

            //			this.tabAPACHEIIValuation.Controls.Remove(this.tabPage4);
            //2008-6-6 haozhong.liu
            //签名常用值
            //m_objCUTC = new clsCommonUseToolCollection(this);
            //m_objCUTC.m_mthBindEmployeeSign(new Control[]{this.m_cmdEvalDoctor  },
            //    new Control[]{this.txtEvalDoctor },new int[]{1});

        }
        #endregion

        protected ctlHighLightFocus m_objHighLight;

        #region Member
        private frmAutoEvalResult frmAutoResult;

        private APACHEIIIValuationDB m_objAPACHEIIIValuationDB;
        #endregion

        #region 评分的计算
        private int intAge = 0;
        private void AgeGroupchanged(object sender, System.EventArgs e)
        {
            try
            {
                switch (((RadioButton)sender).Name)
                {
                    case "rdbAgeU44":
                        intAge = 0;
                        break;

                    case "rdbAgeU59":
                        intAge = 1;
                        break;

                    case "rdbAgeU64":
                        intAge = 2;
                        break;

                    case "rdbAgeU69":
                        intAge = 3;
                        break;

                    case "rdbAgeU74":
                        intAge = 4;
                        break;

                    case "rdbAgeU84":
                        intAge = 5;
                        break;

                    case "rdbAgeO85":
                        intAge = 6;
                        break;
                }

            }
            catch
            { }
        }

        private double dblFiO2 = 0;
        private double FiO2()
        {
            try
            {
                dblFiO2 = double.Parse(txtFiO2.Text);
            }
            catch
            {
                return 0;
            }
            return dblFiO2;
        }
        private int intOpenEyeSel = 0;
        private void OpenEyeChanged(object sender, System.EventArgs e)
        {
            try
            {
                intOpenEyeSel = ((RadioButton)sender).TabIndex;

                if (((RadioButton)sender).Name == "rdbCanOpenEyes" && ((RadioButton)sender).Checked)
                {
                    intOpenEyeSel = 0;
                }
                else
                {
                    intOpenEyeSel = 1;
                }
            }
            catch
            { }

        }
        private int intLanguage = 0;
        private void LanguageChanged(object sender, System.EventArgs e)
        {
            try
            {
                intLanguage = int.Parse((string)((RadioButton)sender).Tag);
            }
            catch
            { }

        }
        private int intOperaSel = 1;
        private void OperaSel(object sender, System.EventArgs e)
        {
            try
            {
                //				intOperaSel = ((RadioButton)sender).TabIndex;
                if (((RadioButton)sender).Name == "rdbOperaSel")
                {
                    gpbHealth.Enabled = false;
                    chkAIDS.Checked = false;
                    chkLiverWane.Checked = false;
                    chkLimphoma.Checked = false;
                    chkMetastaticTumor.Checked = false;
                    chkLeukaemia.Checked = false;
                    chkImmunity.Checked = false;
                    chkHepatocirrhosis.Checked = false;

                    intOperaSel = 0;
                }
                else
                {
                    gpbHealth.Enabled = true;
                    chkAIDS.Checked = false;
                    chkLiverWane.Checked = false;
                    chkLimphoma.Checked = false;
                    chkMetastaticTumor.Checked = false;
                    chkLeukaemia.Checked = false;
                    chkImmunity.Checked = false;
                    chkHepatocirrhosis.Checked = false;

                    intOperaSel = 1;
                }
            }
            catch
            { }

        }

        private int intAcheAndLanguage()
        {
            int[] CanA = new int[] { 0, 3, 3, 3 };
            int[] CanB = new int[] { 3, 8, 13, 13 };
            int[] CanC = new int[] { 10, 13, 24, 29 };
            int[] CanD = new int[] { 15, 15, 24, 29 };
            int[] CannotA = new int[] { 0, 0, 24, 29 };
            int[] CannotB = new int[] { 16, 16, 33, 48 };
            int intLVal = 0;
            int intLTotal = 0;


            try
            {
                if (intOpenEyeSel == 0)
                {
                    switch (intLanguage)
                    {
                        case 0:
                            {
                                if (chkAccording.Checked == true)
                                {
                                    intLVal = CanA[0];
                                    intLTotal += intLVal;
                                }
                                if (chkPositionAche.Checked == true)
                                {
                                    intLVal = CanA[1];
                                    intLTotal += intLVal;
                                }
                                if (chkBodyBendAndVertical.Checked == true && intOperaSel == 1)
                                {
                                    intLVal = CanA[2];
                                    intLTotal += intLVal;
                                }
                                if (chkBrainUnreaction.Checked == true && intOperaSel == 1)
                                {
                                    intLVal = CanA[3];
                                    intLTotal += intLVal;
                                }
                            }
                            break;
                        case 1:
                            {
                                if (chkAccording.Checked == true)
                                {
                                    intLVal = CanB[0];
                                    intLTotal += intLVal;
                                }

                                if (chkPositionAche.Checked == true)
                                {
                                    intLVal = CanB[1];
                                    intLTotal += intLVal;
                                }
                                if (chkBodyBendAndVertical.Checked == true && intOperaSel == 1)
                                {
                                    intLVal = CanB[2];
                                    intLTotal += intLVal;
                                }
                                if (chkBrainUnreaction.Checked == true && intOperaSel == 1)
                                {
                                    intLVal = CanB[3];
                                    intLTotal += intLVal;
                                }
                            }
                            break;
                        case 2:
                            {
                                if (chkAccording.Checked == true)
                                {
                                    intLVal = CanC[0];
                                    intLTotal += intLVal;
                                }

                                if (chkPositionAche.Checked == true)
                                {
                                    intLVal = CanC[1];
                                    intLTotal += intLVal;
                                }

                                if (chkBodyBendAndVertical.Checked == true)
                                {
                                    intLVal = CanC[2];
                                    intLTotal += intLVal;
                                }

                                if (chkBrainUnreaction.Checked == true && intOperaSel == 1)
                                {
                                    intLVal = CanC[3];
                                    intLTotal += intLVal;
                                }
                            }
                            break;
                        case 3:
                            {
                                if (chkAccording.Checked == true)
                                {
                                    intLVal = CanD[0];
                                    intLTotal += intLVal;
                                }

                                if (chkPositionAche.Checked == true)
                                {
                                    intLVal = CanD[1];
                                    intLTotal += intLVal;

                                }

                                if (chkBodyBendAndVertical.Checked == true)
                                {
                                    intLVal = CanD[2];
                                    intLTotal += intLVal;

                                }

                                if (chkBrainUnreaction.Checked == true)
                                {
                                    intLVal = CanD[3];
                                    intLTotal += intLVal;

                                }

                            }
                            break;
                    }

                }
                else if (intOpenEyeSel == 1)
                {
                    switch (intLanguage)
                    {
                        case 2:
                            {
                                if (chkAccording.Checked == true)
                                {
                                    intLVal = CannotA[0];
                                    intLTotal += intLVal;
                                }

                                if (chkPositionAche.Checked == true)
                                {
                                    intLVal = CannotA[1];
                                    intLTotal += intLVal;

                                }

                                if (chkBodyBendAndVertical.Checked == true && intOperaSel == 1)
                                {
                                    intLVal = CannotA[2];
                                    intLTotal += intLVal;

                                }

                                if (chkBrainUnreaction.Checked == true && intOperaSel == 1)
                                {
                                    intLVal = CannotA[3];
                                    intLTotal += intLVal;

                                }

                            }
                            break;
                        case 3:
                            {
                                if (chkAccording.Checked == true)
                                {
                                    intLVal = CannotB[0];
                                    intLTotal += intLVal;

                                }

                                if (chkPositionAche.Checked == true)
                                {
                                    intLVal = CannotB[1];
                                    intLTotal += intLVal;

                                }

                                if (chkBodyBendAndVertical.Checked == true)
                                {
                                    intLVal = CannotB[2];
                                    intLTotal += intLVal;

                                }

                                if (chkBrainUnreaction.Checked == true)
                                {
                                    intLVal = CannotB[3];
                                    intLTotal += intLVal;

                                }

                            }
                            break;
                    }
                }

            }
            catch { }
            return intLTotal;
        }
        private double dblPH = 0;
        private double PH()
        {
            try
            {
                dblPH = double.Parse(txtPH.Text);
                return dblPH;
            }
            catch
            {
                return dblPH = -1;
            }
        }
        private double dblPCO2 = 0;
        private double PCO2()
        {
            try
            {
                dblPCO2 = double.Parse(txtPCO2.Text);
                return dblPCO2;
            }
            catch
            {
                return dblPCO2 = -1;
            }
        }

        private void cmdCalculate_Click(object sender, System.EventArgs e)
        {
            try
            {
                object[] res = dtlResult.Rows[0].ItemArray;
                double doubleTotal = 0;
                double dblPao2 = 0;
                double dblDo2 = 0;

                if (FiO2() >= 0.5)
                {
                    dblDo2 = dblCalDo2();
                }
                else
                {
                    dblPao2 = dblCalPao2();
                }

                double dblBreath = 0;
                dblBreath = dblCalBreath();

                double dblAspVal = dblCalAdvArteryPress() + dblCalHR() + dblCalTemperature() + dblBreath + dblPao2 + dblDo2 + dblCalBloodCorpuscle() + dblCalAmountLeucocyte() + dblCalBloodFlesh() + dblCalUrineAmount() + dblCalHematuria() + dblCalBloodNa() + dblCalProteid() + dblCalHypercholesterolemia() + dblCalBloodGallbladder();
                res[0] = dblAspVal.ToString();

                PH();
                PCO2();
                PHAndPCO2();
                int intPHAndPCO2Val = intPHAndPCO2;

                if (intPHAndPCO2Val >= 0)
                    res[1] = intPHAndPCO2Val.ToString();
                else
                    res[1] = "/";

                #region 年龄和既往史
                int AgeAndHealthVal = 0;
                //				if(intOperaSel==1)
                //					AgeAndHealthVal=HealthVal();
                //				switch(intAge)
                //				{
                //					case 0:
                //						AgeAndHealthVal+=0;break;
                //					case 1:
                //						AgeAndHealthVal+=5;break;
                //					case 2:
                //						AgeAndHealthVal+=11;break;
                //					case 3:
                //						AgeAndHealthVal+=13;break;
                //					case 4:
                //						AgeAndHealthVal+=16;break;
                //					case 5:
                //						AgeAndHealthVal+=17;break;
                //					case 6:
                //						AgeAndHealthVal+=24;break;
                //				}

                AgeAndHealthVal = m_intCalAgeHealthVal();

                if (AgeAndHealthVal >= 0)
                    res[2] = AgeAndHealthVal.ToString();
                else
                    res[2] = "/";
                #endregion

                int Neurone = 0;

                Neurone = intAcheAndLanguage();
                res[3] = Neurone.ToString();

                doubleTotal = dblAspVal + ((intPHAndPCO2Val < 0) ? 0 : intPHAndPCO2Val) + AgeAndHealthVal + Neurone;

                res[4] = doubleTotal.ToString();
                dtlResult.Rows[0].ItemArray = res;
            }
            catch { }

        }

        private int m_intCalAgeHealthVal()
        {
            int intVal = 0;

            foreach (Control ctl in gpbAge.Controls)
            {
                if (ctl.GetType().Name == "RadioButton" && ((RadioButton)ctl).Checked == true)
                {
                    intVal += int.Parse((string)((RadioButton)ctl).Tag);
                }
            }

            foreach (Control ctl in gpbHealth.Controls)
            {
                if (ctl.GetType().Name == "CheckBox" && ((CheckBox)ctl).Checked == true)
                {
                    intVal += int.Parse((string)((CheckBox)ctl).Tag);
                }
            }

            return intVal;
        }

        private int intPHAndPCO2;
        private void PHAndPCO2()
        {
            try
            {
                if (dblPH < 0 || dblPCO2 < 0)
                {
                    intPHAndPCO2 = -1;
                    return;
                }

                if (dblPH < 7.20)
                {
                    if (dblPCO2 < 50)
                        intPHAndPCO2 = 12;
                    else if (dblPCO2 >= 50)
                        intPHAndPCO2 = 4;
                }
                else if (dblPH >= 7.20 && dblPH < 7.30)
                {
                    if (dblPCO2 < 30)
                        intPHAndPCO2 = 9;
                    else if (dblPCO2 >= 30 && dblPCO2 < 40)
                        intPHAndPCO2 = 6;
                    else if (dblPCO2 >= 40 && dblPCO2 < 50)
                        intPHAndPCO2 = 3;
                    else if (dblPCO2 >= 50)
                        intPHAndPCO2 = 2;
                    else intPHAndPCO2 = -1;
                }
                else if (dblPH >= 7.30 && dblPH < 7.35)
                {
                    if (dblPCO2 < 30)
                        intPHAndPCO2 = 9;
                    else if (dblPCO2 >= 30 && dblPCO2 < 45)
                        intPHAndPCO2 = 0;
                    else if (dblPCO2 >= 45)
                        intPHAndPCO2 = 1;
                    else intPHAndPCO2 = -1;
                }
                else if (dblPH >= 7.35 && dblPH < 7.45)
                {
                    if (dblPCO2 <= 30)
                        intPHAndPCO2 = 5;
                    else if (dblPCO2 >= 30 && dblPCO2 < 45)
                        intPHAndPCO2 = 0;
                    else if (dblPCO2 >= 45)
                        intPHAndPCO2 = 1;
                    else intPHAndPCO2 = -1;
                }
                else if (dblPH >= 7.45 && dblPH < 7.50)
                {
                    if (dblPCO2 < 30)
                        intPHAndPCO2 = 5;
                    else if (dblPCO2 >= 30 && dblPCO2 < 35)
                        intPHAndPCO2 = 0;
                    else if (dblPCO2 >= 35 && dblPCO2 < 45)
                        intPHAndPCO2 = 2;
                    else if (dblPCO2 >= 45)
                        intPHAndPCO2 = 12;
                    else intPHAndPCO2 = -1;
                }
                else if (dblPH >= 7.50 && dblPH < 7.60)
                {
                    if (dblPCO2 <= 45)
                        intPHAndPCO2 = 3;
                    else
                        intPHAndPCO2 = 12;
                }
                else if (dblPH >= 7.60)
                {
                    if (dblPCO2 < 25)
                        intPHAndPCO2 = 0;
                    else if (dblPCO2 > 25 && dblPCO2 < 45)
                        intPHAndPCO2 = 3;
                    else if (dblPCO2 >= 45)
                        intPHAndPCO2 = 12;
                    else intPHAndPCO2 = -1;
                }
                else intPHAndPCO2 = -1;
            }
            catch { }
        }

        private int HealthVal()
        {

            try
            {
                int intval = 0;
                int intHealthVal = 0;

                if (chkAIDS.Checked == true)
                {
                    intval = 23;
                    intHealthVal += intval;

                }
                else if (chkAIDS.Checked == false && intval == 23)
                {
                    intHealthVal -= intval;
                    intval = 0;
                }

                if (chkLiverWane.Checked == true)
                {
                    intval = 16;
                    intHealthVal += intval;

                }
                else if (chkLiverWane.Checked == false && intval == 16)
                {
                    intHealthVal -= intval;
                    intval = 0;
                }

                if (chkLimphoma.Checked == true)
                {
                    intval = 13;
                    intHealthVal += intval;

                }
                else if (chkLimphoma.Checked == false && intval == 13)
                {
                    intHealthVal -= intval;
                    intval = 0;
                }
                if (chkMetastaticTumor.Checked == true)
                {
                    intval = 11;
                    intHealthVal += intval;

                }
                else if (chkMetastaticTumor.Checked == false && intval == 11)
                {
                    intHealthVal -= intval;
                    intval = 0;
                }
                if (chkLeukaemia.Checked == true)
                {
                    intval = 10;
                    intHealthVal += intval;

                }
                else if (chkLeukaemia.Checked == false && intval == 10)
                {
                    intHealthVal -= intval;
                    intval = 0;
                }
                if (chkImmunity.Checked == true)
                {
                    intval = 10;
                    intHealthVal += intval;

                }
                else if (chkImmunity.Checked == false && intval == 10)
                {
                    intHealthVal -= intval;
                    intval = 0;
                }
                if (chkHepatocirrhosis.Checked == true)
                {
                    intval = 4;
                    intHealthVal += intval;

                }
                else if (chkHepatocirrhosis.Checked == false && intval == 4)
                {
                    intHealthVal -= intval;
                    intval = 0;
                }
                return intHealthVal;
            }
            catch
            {
                return 0;
            }

        }
        private double dblCalHR()
        {
            try
            {
                double dblHR;
                try
                {
                    dblHR = double.Parse(txtHR.Text);
                }
                catch
                {
                    return 0;
                }
                if (dblHR >= 50 && dblHR <= 99)
                    return 0;
                else if (dblHR >= 100 && dblHR <= 109)
                    return 1;
                else if (dblHR >= 110 && dblHR <= 119)
                    return 5;
                else if (dblHR >= 120 && dblHR <= 139)
                    return 7;
                else if (dblHR >= 140 && dblHR <= 154)
                    return 13;
                else if (dblHR >= 155)
                    return 17;
                else if (dblHR >= 40 && dblHR <= 49)
                    return 5;
                else if (dblHR <= 39)
                    return 8;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }

        private double dblCalAdvArteryPress()
        {
            try
            {
                double dblAdvArteryPress;
                try
                {
                    dblAdvArteryPress = double.Parse(txtAdvArteryPress.Text);
                }
                catch
                {
                    return 0;
                }

                if (dblAdvArteryPress >= 80 && dblAdvArteryPress <= 99)
                    return 0;
                else if (dblAdvArteryPress >= 100 && dblAdvArteryPress <= 119)
                    return 4;
                else if (dblAdvArteryPress >= 70 && dblAdvArteryPress <= 79)
                    return 6;
                else if ((dblAdvArteryPress >= 60 && dblAdvArteryPress <= 69) || (dblAdvArteryPress >= 120 && dblAdvArteryPress <= 129))
                    return 7;
                else if (dblAdvArteryPress >= 130 && dblAdvArteryPress <= 139)
                    return 9;
                else if (dblAdvArteryPress <= 39)
                    return 23;
                else if (dblAdvArteryPress >= 40 && dblAdvArteryPress <= 59)
                    return 15;
                else if (dblAdvArteryPress >= 140)
                    return 10;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }
        private double dblCalTemperature()
        {
            try
            {
                double dblTemperature;
                try
                {
                    dblTemperature = double.Parse(txtTemperature.Text);
                }
                catch
                {
                    return 0;
                }
                if (dblTemperature >= 36 && dblTemperature <= 39.9)
                    return 0;
                else if (dblTemperature >= 35 && dblTemperature <= 35.9)
                    return 2;
                else if (dblTemperature >= 34 && dblTemperature <= 34.9)
                    return 8;
                else if (dblTemperature >= 33.5 && dblTemperature <= 33.9)
                    return 13;
                else if (dblTemperature >= 40)
                    return 4;
                else if (dblTemperature >= 33 && dblTemperature <= 33.4)
                    return 16;
                else if (dblTemperature <= 32.9)
                    return 20;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }
        private double dblCalBreath()
        {
            try
            {
                double dblBreath;
                try
                {
                    dblBreath = double.Parse(txtBreath.Text);
                }
                catch
                {
                    return 0;
                }
                if (dblBreath >= 14 && dblBreath <= 24)
                    return 0;
                else if (dblBreath >= 25 && dblBreath <= 34)
                    return 6;
                else if (dblBreath == 12)
                {
                    if (!chkMachineAerate.Checked)
                        return 7;
                    else
                        return 0;
                }
                else if (dblBreath == 13)
                    return 7;
                else if (dblBreath >= 6 && dblBreath <= 11)
                {
                    if (!chkMachineAerate.Checked)
                        return 8;
                    else
                        return 0;
                }
                else if (dblBreath >= 50)
                    return 18;
                else if (dblBreath >= 40 && dblBreath <= 49)
                    return 11;
                else if (dblBreath >= 35 && dblBreath <= 39)
                    return 9;
                else if (dblBreath <= 5)
                    return 17;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }
        private double dblCalPao2()
        {
            try
            {
                double dblPao2;
                try
                {
                    dblPao2 = double.Parse(txtPao2.Text);
                }
                catch
                {
                    return 0;
                }
                if (dblPao2 >= 70 && dblPao2 <= 79)
                    return 2;
                else if (dblPao2 >= 50 && dblPao2 <= 69)
                    return 5;
                else if (dblPao2 >= 80)
                    return 0;
                else if (dblPao2 <= 49)
                    return 15;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }
        private double dblCalDo2()
        {
            try
            {
                double dblDo2;
                try
                {
                    dblDo2 = double.Parse(txtDo2.Text);
                }
                catch
                {
                    return 0;
                }
                if (dblDo2 >= 100 && dblDo2 <= 249)
                    return 7;
                else if (dblDo2 >= 250 && dblDo2 <= 349)
                    return 9;
                else if (dblDo2 >= 350 && dblDo2 <= 499)
                    return 11;
                else if (dblDo2 >= 500)
                    return 14;
                else if (dblDo2 < 100)
                    return 0;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }

        }
        private double dblCalBloodCorpuscle()
        {
            try
            {
                double dblBloodCorpuscle;
                try
                {
                    dblBloodCorpuscle = double.Parse(txtBloodCorpuscle.Text);
                }
                catch
                {
                    return 0;
                }
                if (dblBloodCorpuscle >= 41 && dblBloodCorpuscle <= 49)
                    return 0;
                else if (dblBloodCorpuscle >= 50)
                    return 3;
                else if (dblBloodCorpuscle <= 40.9)
                    return 3;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }
        private double dblCalAmountLeucocyte()
        {
            try
            {
                double dblAmountLeucocyte;
                try
                {
                    dblAmountLeucocyte = double.Parse(txtAmountLeucocyte.Text);
                }
                catch
                {
                    return 0;
                }
                if (dblAmountLeucocyte >= 3 && dblAmountLeucocyte <= 19.9)
                    return 0;
                else if (dblAmountLeucocyte >= 20 && dblAmountLeucocyte <= 24.9)
                    return 1;
                else if (dblAmountLeucocyte >= 1 && dblAmountLeucocyte <= 2.9)
                    return 5;
                else if (dblAmountLeucocyte >= 25)
                    return 5;
                else if (dblAmountLeucocyte < 1)
                    return 19;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }
        private double dblCalBloodFlesh()
        {
            try
            {
                double dblBloodFlesh;
                try
                {
                    dblBloodFlesh = (double.Parse(txtBloodFlesh.Text)) * 1000;
                }
                catch
                {
                    return 0;
                }
                if (chkKidneyWane.Checked == true)
                {
                    if (dblBloodFlesh >= 44 && dblBloodFlesh <= 132)
                        return 0;
                    else if (dblBloodFlesh >= 133 && dblBloodFlesh <= 171)
                        return 4;

                    else if (dblBloodFlesh >= 172)
                        return 17;
                    else if (dblBloodFlesh <= 43)
                        return 3;
                    else
                        return 0;
                }
                else if (chkKidneyWane.Checked == false)
                {
                    if (dblBloodFlesh >= 0 && dblBloodFlesh <= 132)
                        return 0;
                    else if (dblBloodFlesh >= 133)
                        return 10;
                    else
                        return 0;
                }
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }

        private double dblCalUrineAmount()
        {
            try
            {
                double dblUrineAmount;
                try
                {
                    dblUrineAmount = double.Parse(txtUrineAmount.Text);
                }
                catch
                {
                    return 0;
                }
                if (dblUrineAmount >= 2000 && dblUrineAmount <= 3999)
                    return 0;
                else if (dblUrineAmount >= 4000)
                    return 1;
                else if (dblUrineAmount >= 1500 && dblUrineAmount <= 1999)
                    return 4;
                else if (dblUrineAmount >= 900 && dblUrineAmount <= 1499)
                    return 5;
                else if (dblUrineAmount >= 600 && dblUrineAmount <= 899)
                    return 7;
                else if (dblUrineAmount >= 400 && dblUrineAmount <= 599)
                    return 8;
                else if (dblUrineAmount <= 399)
                    return 15;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }

        }
        private double dblCalHematuria()
        {
            try
            {
                double dblHematuria;
                try
                {
                    dblHematuria = double.Parse(txtHematuria.Text);
                }
                catch
                {
                    return 0;
                }
                if (dblHematuria <= 6.1)
                    return 0;
                else if (dblHematuria >= 6.2 && dblHematuria <= 7.1)
                    return 2;
                else if (dblHematuria >= 7.2 && dblHematuria <= 14.3)
                    return 7;
                else if (dblHematuria >= 14.4 && dblHematuria <= 28.5)
                    return 11;
                else if (dblHematuria >= 28.6)
                    return 12;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }
        private double dblCalBloodNa()
        {
            try
            {
                double dblBloodNa;
                try
                {
                    dblBloodNa = double.Parse(txtBloodNa.Text);
                }
                catch
                {
                    return 0;
                }
                if (dblBloodNa >= 135 && dblBloodNa <= 154)
                    return 0;
                else if (dblBloodNa >= 120 && dblBloodNa <= 134)
                    return 2;
                else if (dblBloodNa >= 34 && dblBloodNa <= 34.9)
                    return 8;
                else if (dblBloodNa >= 155)
                    return 4;
                else if (dblBloodNa <= 119)
                    return 3;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }
        private double dblCalProteid()
        {
            try
            {

                double dblProteid;
                try
                {
                    dblProteid = double.Parse(txtProteid.Text);
                }
                catch
                {
                    return 0;
                }
                if (dblProteid >= 25 && dblProteid <= 44)
                    return 0;
                else if (dblProteid >= 20 && dblProteid <= 24)
                    return 6;
                else if (dblProteid >= 45)
                    return 4;
                else if (dblProteid <= 19)
                    return 11;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }
        private double dblCalHypercholesterolemia()
        {
            try
            {
                double dblHypercholesterolemia;
                try
                {
                    dblHypercholesterolemia = double.Parse(txtHypercholesterolemia.Text);
                }
                catch
                {
                    return 0;
                }
                if (dblHypercholesterolemia >= 35 && dblHypercholesterolemia <= 51)
                    return 5;
                else if (dblHypercholesterolemia >= 52 && dblHypercholesterolemia <= 85)
                    return 6;
                else if (dblHypercholesterolemia >= 86 && dblHypercholesterolemia <= 135)
                    return 8;
                else if (dblHypercholesterolemia >= 136)
                    return 16;
                else if (dblHypercholesterolemia <= 34)
                    return 0;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }
        private double dblCalBloodGallbladder()
        {
            try
            {
                double dblBloodGallbladder;
                try
                {
                    dblBloodGallbladder = double.Parse(txtBloodGallbladder.Text);
                }
                catch
                {
                    return 0;
                }
                if (dblBloodGallbladder >= 3.4 && dblBloodGallbladder <= 11.1)
                    return 0;
                else if (dblBloodGallbladder >= 11.2 && dblBloodGallbladder <= 19.3)
                    return 3;
                else if (dblBloodGallbladder >= 2.2 && dblBloodGallbladder <= 3.3)
                    return 9;
                else if (dblBloodGallbladder >= 19.4)
                    return 5;
                else if (dblBloodGallbladder <= 2.1)
                    return 8;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region interface
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



        public override void Delete()
        {
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(lblSetionOffice.Tag.ToString(),PrivilegeData.enmPrivilegeSF.APACHEIIValuation,PrivilegeData.enmPrivilegeOperation.Delete))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            //2008-6-6 haozhong.liu
            //if(m_objCurrentContext.m_ObjControl.m_enmDeleteCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
            //    == enmDBControlCheckResult.Disable)
            //{
            //    clsPublicFunction.s_mthShowNotPermitMessage();
            //    return;
            //}

            if (m_strInPatientID == null || m_strInPatientID == "")
            {
                clsPublicFunction.ShowInformationMessageBox("请输入病人住院号!");
                return;
            }
            if (m_strCreateDate == null || m_strCreateDate == "")
            {
                clsPublicFunction.ShowInformationMessageBox("请在列表中选择相应的评分时间！");
                return;
            }

            if (!clsPublicFunction.s_blnAskForDelete())
                return;

            long lngRes = objDomain.m_lngDeactive(clsBaseInfo.LoginEmployee.m_strEMPID_CHR, m_strInPatientID, m_strInPatientDate, m_strCreateDate);

            if (lngRes <= 0)
            {
                clsPublicFunction.ShowInformationMessageBox("删除失败，请重新操作!");
                return;
            }

            foreach (TreeNode trnNode in trvActivityTime.Nodes[0].Nodes)
            {
                if ((DateTime)trnNode.Tag == DateTime.Parse(m_strCreateDate))
                {
                    trnNode.Remove();
                    break;
                }
            }
            this.trvActivityTime.SelectedNode = this.trvActivityTime.Nodes[0];
            ClearUp();
        }

        public override void Display()
        {

        }

        public override void Display(string cardno, string ActivityTime)
        {

        }

        private int MachineAerate = 0;

        private int KidneyWane = 0;
        private int AIDS = 0;

        private int LiverWane = 0;

        private int Limphoma = 0;
        private int MetastaticTumor = 0;
        private int Leukaemia = 0;
        private int Immunity = 0;
        private int Hepatocirrhosis = 0;
        private int According = 0;
        private int PositionAche = 0;
        private int BodyBendAndVertical = 0;
        private int BrainUnreaction = 0;
        private void convert()
        {
            try
            {

                if (chkMachineAerate.Checked)
                    MachineAerate = 1;
                else
                    MachineAerate = 0;
                if (chkKidneyWane.Checked)
                    KidneyWane = 1;
                else
                    KidneyWane = 0;
                if (chkKidneyWane.Checked)
                    KidneyWane = 1;
                else
                    KidneyWane = 0;
                if (chkAIDS.Checked)
                    AIDS = 1;
                else
                    AIDS = 0;
                if (chkLimphoma.Checked)
                    Limphoma = 1;
                else
                    Limphoma = 0;
                if (chkLiverWane.Checked)
                    LiverWane = 1;
                else
                    LiverWane = 0;
                if (chkMetastaticTumor.Checked)
                    MetastaticTumor = 1;
                else
                    MetastaticTumor = 0;

                if (chkLeukaemia.Checked)
                    Leukaemia = 1;
                else
                    Leukaemia = 0;
                if (chkAccording.Checked)
                    According = 1;
                else
                    According = 0;
                if (chkImmunity.Checked)
                    Immunity = 1;
                else
                    Immunity = 0;
                if (chkPositionAche.Checked)
                    PositionAche = 1;
                else
                    PositionAche = 0;
                if (chkBodyBendAndVertical.Checked)
                    BodyBendAndVertical = 1;
                else
                    BodyBendAndVertical = 0;
                if (chkBrainUnreaction.Checked)
                    BrainUnreaction = 1;
                else
                    BrainUnreaction = 0;


            }
            catch { }


        }

        public override long m_lngSubSave()
        {
            return m_lngSaveWithMessageBox();
        }

        public override void Save()
        {

            try
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
            catch
            { }
        }
        #endregion

        private void cmdSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                Save();
            }
            catch
            { }
        }

        protected override void m_mthLoadAllRecordTimeOfAPatient(string p_strPatientID, string p_strPatientDate, string p_strFromDate, string p_strToDate)
        {
            if (p_strPatientID == null || p_strPatientID == "")
                return;

            this.trvActivityTime.Nodes[0].Nodes.Clear();

            DateTime[] m_dtmArr = objDomain.m_dtmGetTimeInfoOfAPatientArr(p_strPatientID, p_strPatientDate, p_strFromDate, p_strToDate);

            if (m_dtmArr != null)
            {
                for (int i = 0; i < m_dtmArr.Length; i++)
                {
                    string strDate = m_dtmArr[i].ToString("yyyy-MM-dd HH:mm:ss");
                    TreeNode trnDate = new TreeNode(strDate);
                    trnDate.Tag = m_dtmArr[i];
                    this.trvActivityTime.Nodes[0].Nodes.Add(trnDate);
                }
            }
            this.trvActivityTime.ExpandAll();
            trvActivityTime.SelectedNode = trvActivityTime.Nodes[0];
            //			this.trvActivityTime_AfterSelect(this.trvActivityTime,new TreeViewEventArgs(trvActivityTime.Nodes[0]));
        }

        private void cmdDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                Delete();
            }
            catch
            { }
        }

        private void frmAPACHEIIIValuation_Load(object sender, System.EventArgs e)
        {
            try
            {
                dtlResult.Columns.Add("ASP得分");
                dtlResult.Columns.Add("酸碱失衡得分");
                dtlResult.Columns.Add("年龄和既往健康得分");
                dtlResult.Columns.Add("神经功能异常得分");
                dtlResult.Columns.Add("总分");
                dtgResult.DataSource = dtlResult;

                dtlResult.Rows.Add(new string[] { "/", "/", "/", "/", "/" });

                m_objHighLight.m_mthAddControlInContainer(this);
                m_objSign.m_mthBindEmployeeSign(this.m_cmdEvalDoctor, txtEvalDoctor, 1, false, clsBaseInfo.LoginEmployee.m_strEMPID_CHR);
            }
            catch
            { }
        }

        public override void Print()
        {
            m_lngPrint();
        }


        private void dtpEvalDate_Load(object sender, System.EventArgs e)
        {
            //2008-6-6 haozhong.liu
            //this.dtpEvalDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
            this.dtpEvalDate.m_mthResetSize();

        }

        #region Save
        private long m_lngSaveWithMessageBox()
        {
            long lngRes = m_lngSaveWithoutMessageBox();
            if (lngRes == -11)
            {
                clsPublicFunction.ShowInformationMessageBox("你所修改的记录已被他人删除或不存在！");
            }
            else if (lngRes == -21)
            {
                clsPublicFunction.ShowInformationMessageBox("对不起，保存失败！");
            }
            else if (lngRes == -31)
            {
                clsPublicFunction.ShowInformationMessageBox("对不起，本记录已被他人修改，请重新读取一次！");
            }
            return lngRes;
        }

        private APACHEIIIValuationDB m_objGetCurrentEvalInfo()
        {
            string str = "$$";

            APACHEIIIValuationDB objAPACHEIIIValuationDB = new APACHEIIIValuationDB();

            objAPACHEIIIValuationDB.strInHospitalNO = m_strInPatientID;
            objAPACHEIIIValuationDB.strInPatientDate = m_strInPatientDate;
            objAPACHEIIIValuationDB.strActivityTime = (m_strCreateDate == "") ? this.dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : m_strCreateDate;

            objAPACHEIIIValuationDB.strEvalDoctorID = clsBaseInfo.LoginEmployee.m_strEMPNO_CHR;
            objAPACHEIIIValuationDB.strAgeGroup = this.intAge.ToString();

            objAPACHEIIIValuationDB.strOperSel = this.intOperaSel.ToString();
            objAPACHEIIIValuationDB.strOpenEyeSel = this.intOpenEyeSel.ToString();
            objAPACHEIIIValuationDB.strLanguageSel = this.intLanguage.ToString();
            objAPACHEIIIValuationDB.strHR = str + this.txtHR.Text;
            objAPACHEIIIValuationDB.strAdvArteryPress = str + this.txtAdvArteryPress.Text;
            objAPACHEIIIValuationDB.strTemperature = str + this.txtTemperature.Text;
            objAPACHEIIIValuationDB.strBreath = str + this.txtBreath.Text;
            objAPACHEIIIValuationDB.strPao2 = str + this.txtPao2.Text;
            objAPACHEIIIValuationDB.strDo2 = str + this.txtDo2.Text;
            objAPACHEIIIValuationDB.strBloodCorpuscle = str + this.txtBloodCorpuscle.Text;
            objAPACHEIIIValuationDB.strAmountLeucocyte = str + this.txtAmountLeucocyte.Text;
            objAPACHEIIIValuationDB.strBloodFlesh = str + this.txtBloodFlesh.Text;
            objAPACHEIIIValuationDB.strHypercholesterolemia = str + this.txtHypercholesterolemia.Text;
            objAPACHEIIIValuationDB.strPH = str + this.txtPH.Text;
            objAPACHEIIIValuationDB.strPCO2 = str + this.txtPCO2.Text;

            objAPACHEIIIValuationDB.strUrineAmount = str + this.txtUrineAmount.Text;
            objAPACHEIIIValuationDB.strHematuria = str + this.txtHematuria.Text;
            objAPACHEIIIValuationDB.strBloodNa = str + this.txtBloodNa.Text;
            objAPACHEIIIValuationDB.strProteid = str + this.txtProteid.Text;
            objAPACHEIIIValuationDB.strBloodGallbladder = str + this.txtBloodGallbladder.Text;
            objAPACHEIIIValuationDB.strFiO2 = str + this.txtFiO2.Text;
            objAPACHEIIIValuationDB.strPaCO2 = str + this.txtPaCO2.Text;
            objAPACHEIIIValuationDB.strMachineAerateChk = str + this.MachineAerate.ToString();
            objAPACHEIIIValuationDB.strKidneyWaneChk = str + this.KidneyWane.ToString();
            objAPACHEIIIValuationDB.strAIDSChk = str + this.AIDS.ToString();
            objAPACHEIIIValuationDB.strLiverWaneChk = str + this.LiverWane.ToString();
            objAPACHEIIIValuationDB.strLimphomaChk = str + this.Limphoma.ToString();
            objAPACHEIIIValuationDB.strMetastaticTumorChk = str + this.MetastaticTumor.ToString();
            objAPACHEIIIValuationDB.strLeukaemiaChk = str + this.Leukaemia.ToString();
            objAPACHEIIIValuationDB.strImmunityChk = str + this.Immunity.ToString();
            objAPACHEIIIValuationDB.strHepatocirrhosisChk = str + this.Hepatocirrhosis.ToString();
            objAPACHEIIIValuationDB.strAccordingChk = str + this.According.ToString();

            objAPACHEIIIValuationDB.strPositionAcheChk = str + this.PositionAche.ToString();
            objAPACHEIIIValuationDB.strBodyBendAndVerticalChk = str + this.BodyBendAndVertical.ToString();
            objAPACHEIIIValuationDB.strBrainUnreactionChk = str + this.BrainUnreaction.ToString();
            objAPACHEIIIValuationDB.strAspVal = str + this.dtlResult.Rows[0].ItemArray[0].ToString();
            objAPACHEIIIValuationDB.strPHAndPCO2Val = str + this.dtlResult.Rows[0].ItemArray[1].ToString();
            objAPACHEIIIValuationDB.strAgeAndHealthVal = str + this.dtlResult.Rows[0].ItemArray[2].ToString();
            objAPACHEIIIValuationDB.strNeuroneVal = str + this.dtlResult.Rows[0].ItemArray[3].ToString();
            objAPACHEIIIValuationDB.strTotalVal = str + this.dtlResult.Rows[0].ItemArray[4].ToString();

            return objAPACHEIIIValuationDB;
        }

        private long m_lngSaveWithoutMessageBox()
        {
            if (m_strInPatientID == null || m_strInPatientID == "")
            {
                clsPublicFunction.ShowInformationMessageBox("对不起，请输入病人住院编号！");
                return 0;
            }

            string strCurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            #region 赋值
            convert();
            this.cmdCalculate_Click(null, null);
            APACHEIIIValuationDB objAPACHEIIIValuationDB = m_objGetCurrentEvalInfo();
            #endregion

            if (m_strCreateDate != "")
            {
                //if(m_objCurrentContext.m_ObjControl.m_enmModifyCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
                //    == enmDBControlCheckResult.Disable)
                //{
                //    clsPublicFunction.s_mthShowNotPermitMessage();
                //    return 0;
                //}

                APACHEIIIValuationDB objTemp;
                long lngExist = objDomain.m_lngGetApacheIIIValue(m_strInPatientID, m_strInPatientDate, m_strCreateDate, out objTemp);

                if (lngExist == 0)
                    return -11;

                if (lngExist == 1)
                {
                    //if(DateTime.Parse(objTemp.strModifyDate) != DateTime.Parse(m_objAPACHEIIIValuationDB.strModifyDate))
                    //    return -31;

                    if (!clsPublicFunction.s_blnAskForModify())
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

                APACHEIIIValuationDB objTemp;
                long lngExist = objDomain.m_lngGetApacheIIIValue(m_strInPatientID, m_strInPatientDate, dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), out objTemp);

                if (lngExist == 1)
                {
                    m_strCreateDate = dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    if (!clsPublicFunction.s_blnAskForModify())
                        return 0;
                }
                else
                {
                    m_strCreateDate = "";
                }
            }

            long lngRes = objDomain.m_lngSave(objAPACHEIIIValuationDB);
            if (lngRes <= 0)
            {
                return -21;
            }
            else
            {
                if (m_strCreateDate == "")
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

        #region ClearUP
        protected override void ClearUp()
        {
            try
            {
                m_strCreateDate = "";


                object[] res = { "/", "/", "/", "/", "/" };
                dtlResult.Rows[0].ItemArray = res;

                foreach (Control control in this.Controls)
                {
                    string typeName = control.GetType().Name;
                    if ((typeName == "TextBox" || typeName == "ctlBorderTextBox") && control.Name != "txtCardNo")
                    {
                        control.Text = "";
                    }
                    else if (typeName == "CheckBox")
                    {
                        ((CheckBox)control).Checked = false;
                    }
                    if (typeName == "dwtFlatComboBox" || typeName == "ctlComboBox")
                    {
                        control.Text = "";
                    }
                }
                foreach (Control control in this.gpbExactLife.Controls)
                {
                    string typeName = control.GetType().Name;
                    if ((typeName == "TextBox" || typeName == "ctlBorderTextBox") && control.Name != "txtInHospitalNO")
                    {
                        control.Text = "";
                    }

                }
                foreach (Control control in this.gpbHealth.Controls)
                {
                    string typeName = control.GetType().Name;

                    if (typeName == "CheckBox")
                    {
                        ((CheckBox)control).Checked = false;
                    }

                }
                foreach (Control control in this.gpbAcheAndLanguage1.Controls)
                {
                    string typeName = control.GetType().Name;
                    if (typeName == "CheckBox")
                    {
                        ((CheckBox)control).Checked = false;
                    }

                }
                foreach (Control control in this.tabPage1.Controls)
                {
                    string typeName = control.GetType().Name;
                    if (typeName == "CheckBox")
                    {
                        ((CheckBox)control).Checked = false;
                    }

                }
                txtAutoTime.Text = "60";
                this.txtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;
                this.dtpEvalDate.Value = DateTime.Now;

                rdbAgeU44.Checked = true;
                rdbNoOperaSel.Checked = true;
                rdbCanOpenEyes.Checked = true;

                txtFiO2.Text = "";
                txtPao2.Text = "";
                txtPaCO2.Text = "";
                txtDo2.Text = "";
            }
            catch { }
        }

        private void m_mthClearPatientInfo()
        {
            m_strInPatientID = "";
            m_strInPatientDate = "";
        }
        #endregion

        #region 自动评分 -- 已注释
        /// <summary>
        /// Alex 2002-9-30
        /// 自动评分的保存方法
        /// </summary>
        /// <param name="strAutoEvalTime"></param>
        private void m_AutoSave(string strAutoEvalTime)
        {
            convert();
            this.cmdCalculate_Click(null, null);
            APACHEIIIValuationDB objAPACHEIIIValuationDB = m_objGetCurrentEvalInfo();
            objAPACHEIIIValuationDB.strActivityTime = strAutoEvalTime;

            //			if(objDomain.lngAddNewRecordOfAutoEval(objAPACHEIIIValuationDB)>0)
            if (objDomain.m_lngSave(objAPACHEIIIValuationDB) > 0)
            {
                dtpEvalDate.Value = DateTime.Parse(strAutoEvalTime);
                TreeNode m_trnNewNode = new TreeNode(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                m_trnNewNode.Tag = DateTime.Parse(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                trvActivityTime.Nodes[0].Nodes.Add(m_trnNewNode);
            }
        }

        private void cmdGetData_Click(object sender, System.EventArgs e)
        {
            if (m_ctlPatientInfo.CurrentEmrPatient == null)
            {
                clsPublicFunction.ShowInformationMessageBox("请选择病人");
                return;
            }
            GetData();
        }

        private void m_mthGetData(string p_strFindDate)
        {
            bool blnIsGE = m_blnCurrApparatus();

            clsCMSData objCMSData = null;
            clsVentilatorData objVentilatorData = null;

            if (m_objCurrentPatient == null) return;

            string[] strTypeArry = new string[] { "TEMP1", "HEARTRATE", "NPBSYSTOLIC", "RESPRATE", "O2CONCENTRATION" };//
            m_mthGetICUDataByTime(p_strFindDate, out objCMSData, out objVentilatorData, strTypeArry);

            if (!blnIsGE)
            {
                if (objCMSData != null)
                {
                    txtPao2.Text = objCMSData.m_strBloodNum1;

                    if (objCMSData.m_strHeartRate == null || objCMSData.m_strHeartRate == "")
                        txtHR.Text = "";
                    else
                        txtHR.Text = m_strFormatShowParamData(objCMSData.m_strHeartRate);//.Substring(0,objCMSData.m_strHeartRate.IndexOf("."));

                    if (objCMSData.m_strTemp1 == null || objCMSData.m_strTemp1 == "")
                        txtTemperature.Text = "";
                    else
                        txtTemperature.Text = objCMSData.m_strTemp1.Trim();
                }

                if (objVentilatorData != null)
                {
                    if (objVentilatorData.m_strRespRate == null || objVentilatorData.m_strRespRate == "")
                        txtBreath.Text = "";
                    else
                        txtBreath.Text = m_strFormatShowParamData(objVentilatorData.m_strRespRate);//.Substring(0,objVentilatorData.m_strRespRate.Trim().IndexOf("."));

                    if (objVentilatorData.m_strO2Concentration == null || objVentilatorData.m_strO2Concentration == "")
                        txtFiO2.Text = "";
                    else
                        txtFiO2.Text = objVentilatorData.m_strO2Concentration.Trim();
                }
            }
            else
            {
                m_mthGetMonitorParamGE();
                clsGECMSData objGECMSData = m_objGECMSData;

                if (objGECMSData != null)
                {
                    if (objGECMSData.m_strHR == null || objGECMSData.m_strHR == "")
                        txtHR.Text = "";
                    else
                        txtHR.Text = m_strFormatShowParamData(objGECMSData.m_strHR);//.Substring(0,objGECMSData.m_strHR.IndexOf("."));

                    if (objGECMSData.m_strTEMP1 == null || objGECMSData.m_strTEMP1 == "")
                        txtTemperature.Text = "";
                    else
                        txtTemperature.Text = objGECMSData.m_strTEMP1.Trim();

                    if (objGECMSData.m_strRR == null || objGECMSData.m_strRR == "")
                        txtBreath.Text = "";
                    else
                        txtBreath.Text = m_strFormatShowParamData(objGECMSData.m_strRR);//.Substring(0,objGECMSData.m_strRR.IndexOf("."));

                    if (objGECMSData.m_strNBPMean == null || objGECMSData.m_strNBPMean == "")
                        txtAdvArteryPress.Text = "";
                    else
                        txtAdvArteryPress.Text = objGECMSData.m_strNBPMean;

                }
            }

        }

        private void GetData()
        {
            #region Old
            //			try
            //			{
            //				clsCMSData objCMSData;
            //				clsVentilatorData objVentilatorData;
            //				clsGECMSData objGECMSData=null;
            //
            //				bool blnIsGE=m_blnCurrApparatus();
            //
            //				//				objPatientInfo_Base.m_ObjCurrentInHospitalInfo.m_ObjLastICUUtil.m_mthGetICUDataByTime(strSickBedNO,dtpStartSample.Value,out objCMSData,out objVenData);
            //				m_mthGetICUDataByTime(dtpStartSample.Value,out objCMSData,out objVentilatorData);
            //				if (blnIsGE)
            //					m_mthGetICUGEDataByTime(dtpStartSample.Value.ToString(),out objGECMSData);
            //				//				m_objICUDomain.m_mthGetICUDataByTime(strSickBedNO,dtpStartSample.Value,out objCMSData,out objVenData);
            //
            //				//设置监护仪获取的数值
            //				if (!blnIsGE)
            //				{
            //					if(objCMSData.m_strHeartRate=="")
            //						txtHR.Text = "";
            //					else
            //						txtHR.Text = objCMSData.m_strHeartRate.Substring(0,objVentilatorData.m_strRespRate.Length-3);
            //			
            //					txtPao2.Text = objCMSData.m_strBloodNum1;
            //
            //					txtTemperature.Text = objCMSData.m_strTemp1;
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
            //
            //					txtTemperature.Text = objGECMSData.m_strTEMP1;
            //				}
            //				//设置呼吸机获取的数值
            //				if(objVentilatorData.m_strRespRate=="")
            //					txtBreath.Text = "";
            //				else
            //					txtBreath.Text = objVentilatorData.m_strRespRate.Substring(0,objVentilatorData.m_strRespRate.Length-3);
            //
            //				txtFiO2.Text = objVentilatorData.m_strO2Concentration;
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
            if (m_ctlPatientInfo.CurrentEmrPatient == null)
            {
                clsPublicFunction.ShowInformationMessageBox("请选择病人");
                return;
            }
            try
            {
                int intTime = int.Parse(txtAutoTime.Text);
                timAutoCollect.Interval = intTime * 1000;
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

        private void timAutoCollect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            try
            {
                dtpStartSample.Value = DateTime.Now;
                //int intTime = int.Parse(txtAutoTime.Text);
                //dtpStartSample.Value = dtpStartSample.Value.AddSeconds(intTime);
                GetData();
                cmdCalculate_Click(null, null);
                object[] res = dtlResult.Rows[0].ItemArray;
                ListViewItem item = new ListViewItem(new string[]{
                                                                     dtpStartSample.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                                                                     dtlResult.Rows[0]["总分"].ToString(),
                                                                     txtHR.Text,
                                                                     txtAdvArteryPress.Text,
                                                                     txtTemperature.Text,
                                                                     txtBreath.Text,
                                                                     txtPao2.Text,
                                                                     txtFiO2.Text,
                                                                     txtDo2.Text,
                                                                     txtBloodCorpuscle.Text,
                                                                     txtAmountLeucocyte.Text,
                                                                     txtBloodFlesh.Text,
                                                                     txtHypercholesterolemia.Text,
                                                                     txtPH.Text,
                                                                     txtUrineAmount.Text,
                                                                     txtHematuria.Text,
                                                                     txtBloodNa.Text,
                                                                     txtProteid.Text,
                                                                     txtBloodGallbladder.Text,
                                                                     txtPCO2.Text
                                                                 });
                frmAutoResult.AddResult(item);
                m_AutoSave(dtpStartSample.Value.ToString("yyyy-MM-dd HH:mm:ss"));
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
            catch { }
        }

        /// <summary>
        /// 关闭时停止评分 Alex 2002-10-16
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAPACHEIIIValuation_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                timAutoCollect.Close();
            }
            catch
            { }
        }
        #endregion

        private void lsvInHospitalNO_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void rdbAgeU74_CheckedChanged(object sender, System.EventArgs e)
        {

        }

        #region 添加键盘快捷键
        private void m_mthSetQuickKeys()
        {
            m_mthSetControlEvent(this);
        }

        private void m_mthSetControlEvent(Control p_ctlControl)
        {
            #region 利用递归调用，读取并设置所有界面事件	
            string strTypeName = p_ctlControl.GetType().Name;
            if (strTypeName != "Label" && strTypeName != "CheckBox" && strTypeName != "ctlBorderTextBox" && strTypeName != "RadioButton")
            {
                p_ctlControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);
                if (p_ctlControl.HasChildren && strTypeName != "DataGrid" && strTypeName != "DateTimePicker" && strTypeName != "ctlComboBox")
                {
                    foreach (Control subcontrol in p_ctlControl.Controls)
                    {
                        string strSubTypeName = subcontrol.GetType().Name;
                        if (strSubTypeName != "Label" && strSubTypeName != "CheckBox" && strSubTypeName != "ctlBorderTextBox" && strSubTypeName != "RadioButton")
                            m_mthSetControlEvent(subcontrol);
                    }
                }
            }
            #endregion
        }

        private void m_mthEvent_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyValue)
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
            if (strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
            {
                switch (strTypeName)
                {
                    case "ctlRichTextBox":
                        if (((ctlRichTextBox)ctlControl).Text != "")
                        {
                            ((ctlRichTextBox)ctlControl).Copy();
                            return 1;
                        }
                        break;

                    case "RichTextBox":
                        if (((RichTextBox)ctlControl).Text != "")
                        {
                            ((RichTextBox)ctlControl).Copy();
                            return 1;
                        }
                        break;

                    case "TextBox":
                        if (((TextBox)ctlControl).Text != "")
                        {
                            ((TextBox)ctlControl).Copy();
                            return 1;
                        }
                        break;

                    case "ctlBorderTextBox":
                        if (((ctlBorderTextBox)ctlControl).Text != "")
                        {
                            ((ctlBorderTextBox)ctlControl).Copy();
                            return 1;
                        }
                        break;

                    case "DataGridTextBox":
                        if (((DataGridTextBox)ctlControl).Text != "")
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
            if (strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
            {
                switch (strTypeName)
                {
                    case "ctlRichTextBox":
                        if (((ctlRichTextBox)ctlControl).Text != "")
                        {
                            ((ctlRichTextBox)ctlControl).Cut();
                            return 1;
                        }
                        break;

                    case "RichTextBox":
                        if (((RichTextBox)ctlControl).Text != "")
                        {
                            ((RichTextBox)ctlControl).Cut();
                            return 1;
                        }
                        break;

                    case "TextBox":
                        if (((TextBox)ctlControl).Text != "")
                        {
                            ((TextBox)ctlControl).Cut();
                            return 1;
                        }
                        break;

                    case "ctlBorderTextBox":
                        if (((ctlBorderTextBox)ctlControl).Text != "")
                        {
                            ((ctlBorderTextBox)ctlControl).Cut();
                            return 1;
                        }
                        break;

                    case "DataGridTextBox":
                        if (((DataGridTextBox)ctlControl).Text != "")
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

            if (strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
            {
                switch (strTypeName)
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
            if (m_strInPatientID == null || m_strInPatientID == "" || m_strInPatientDate == null || m_strInPatientDate == "") return;

            this.txtTemperature.Text = "";
            this.txtBreath.Text = "";
            this.txtAdvArteryPress.Text = "";
            this.txtPao2.Text = "";
            this.txtHR.Text = "";


            clsTrendDomain objDomain = new clsTrendDomain();
            string[] strEMFC_IDArr = new string[] { "100", "92", "91", "-1", "40" };
            //{"100","40","40","92","89","90","-1","-1"};//体温，心率，脉搏，呼吸,收缩压，舒张压,-1代表还没有找到的编号
            string[] strResultArr;
            long lngRes = objDomain.m_lngGetDocvueResultArr(m_strInPatientID, DateTime.Parse(m_strInPatientDate), strEMFC_IDArr, dtpEvalDate.Value, out strResultArr);
            if (lngRes <= 0)
            {
                switch (lngRes)
                {
                    case (long)(enmOperationResult.Not_permission):
                        clsPublicFunction.s_mthShowNotPermitMessage(); break;
                    case (long)(enmOperationResult.DB_Fail):
                        clsPublicFunction.ShowInformationMessageBox("数据库连接失败"); break;
                }
            }
            else
            {
                this.txtTemperature.Text = strResultArr[0];
                this.txtBreath.Text = strResultArr[1];
                this.txtAdvArteryPress.Text = strResultArr[2];
                this.txtPao2.Text = strResultArr[3];
                this.txtHR.Text = strResultArr[4];
            }
        }

        protected override void m_mthDisplay()
        {
            convert();

            try
            {
                long lngRes = objDomain.m_lngGetApacheIIIValue(m_strInPatientID, m_strInPatientDate, m_strCreateDate, out m_objAPACHEIIIValuationDB);

                if (m_objAPACHEIIIValuationDB == null)
                    return;

                #region 赋值
                this.intAge = int.Parse(m_objAPACHEIIIValuationDB.strAgeGroup);

                switch (intAge)
                {
                    case 0:
                        rdbAgeU44.Checked = true;
                        break;

                    case 1:
                        rdbAgeU59.Checked = true;
                        break;

                    case 2:
                        rdbAgeU64.Checked = true;
                        break;

                    case 3:
                        rdbAgeU69.Checked = true;
                        break;

                    case 4:
                        rdbAgeU74.Checked = true;
                        break;

                    case 5:
                        rdbAgeU84.Checked = true;
                        break;

                    case 6:
                        rdbAgeO85.Checked = true;
                        break;
                }
                this.intOperaSel = int.Parse(m_objAPACHEIIIValuationDB.strOperSel);
                if (intOperaSel == 0)
                    rdbOperaSel.Checked = true;
                else
                    rdbNoOperaSel.Checked = true;

                this.intOpenEyeSel = int.Parse(m_objAPACHEIIIValuationDB.strOpenEyeSel);
                if (intOpenEyeSel == 0)
                    rdbCanOpenEyes.Checked = true;
                else
                    rdbCannotOpenEyes.Checked = true;

                this.intLanguage = int.Parse(m_objAPACHEIIIValuationDB.strLanguageSel);
                foreach (Control ctr4 in gpbAcheAndLanguage1.Controls)
                {
                    try
                    {
                        if (int.Parse((string)ctr4.Tag) == intLanguage)
                        {
                            ((RadioButton)ctr4).Checked = true;
                            break;
                        }
                    }
                    catch
                    {
                    }
                }


                this.txtHR.Text = m_objAPACHEIIIValuationDB.strHR;
                this.txtAdvArteryPress.Text = m_objAPACHEIIIValuationDB.strAdvArteryPress;
                this.txtTemperature.Text = m_objAPACHEIIIValuationDB.strTemperature;
                this.txtBreath.Text = m_objAPACHEIIIValuationDB.strBreath;
                this.txtPao2.Text = m_objAPACHEIIIValuationDB.strPao2;

                this.txtDo2.Text = m_objAPACHEIIIValuationDB.strDo2;
                this.txtBloodCorpuscle.Text = m_objAPACHEIIIValuationDB.strBloodCorpuscle;
                this.txtAmountLeucocyte.Text = m_objAPACHEIIIValuationDB.strAmountLeucocyte;
                this.txtBloodFlesh.Text = m_objAPACHEIIIValuationDB.strBloodFlesh;
                this.txtHypercholesterolemia.Text = m_objAPACHEIIIValuationDB.strHypercholesterolemia;
                this.txtUrineAmount.Text = m_objAPACHEIIIValuationDB.strUrineAmount;
                this.txtHematuria.Text = m_objAPACHEIIIValuationDB.strHematuria;
                this.txtBloodNa.Text = m_objAPACHEIIIValuationDB.strBloodNa;
                this.txtProteid.Text = m_objAPACHEIIIValuationDB.strProteid;
                this.txtBloodGallbladder.Text = m_objAPACHEIIIValuationDB.strBloodGallbladder;
                this.txtFiO2.Text = m_objAPACHEIIIValuationDB.strFiO2;
                this.txtPaCO2.Text = m_objAPACHEIIIValuationDB.strPaCO2;


                this.txtPH.Text = m_objAPACHEIIIValuationDB.strPH;
                this.txtPCO2.Text = m_objAPACHEIIIValuationDB.strPCO2;

                this.MachineAerate = int.Parse(m_objAPACHEIIIValuationDB.strMachineAerateChk);
                if (MachineAerate == 1)
                    chkMachineAerate.Checked = true;
                else
                    chkMachineAerate.Checked = false;
                this.chkMachineAerate.Checked = chkMachineAerate.Checked;
                this.KidneyWane = int.Parse(m_objAPACHEIIIValuationDB.strKidneyWaneChk);
                if (KidneyWane == 1)
                    chkKidneyWane.Checked = true;
                else
                    chkKidneyWane.Checked = false;
                this.chkKidneyWane.Checked = chkKidneyWane.Checked;


                this.AIDS = int.Parse(m_objAPACHEIIIValuationDB.strAIDSChk);
                if (AIDS == 1)
                    chkAIDS.Checked = true;
                else
                    chkAIDS.Checked = false;
                this.chkAIDS.Checked = chkAIDS.Checked;


                this.LiverWane = int.Parse(m_objAPACHEIIIValuationDB.strLiverWaneChk);
                if (LiverWane == 1)
                    chkLiverWane.Checked = true;
                else
                    chkLiverWane.Checked = false;
                this.chkLiverWane.Checked = chkLiverWane.Checked;

                this.Limphoma = int.Parse(m_objAPACHEIIIValuationDB.strLimphomaChk);
                if (Limphoma == 1)
                    chkLimphoma.Checked = true;
                else
                    chkLimphoma.Checked = false;
                this.chkLimphoma.Checked = chkLimphoma.Checked;

                this.MetastaticTumor = int.Parse(m_objAPACHEIIIValuationDB.strMetastaticTumorChk);
                if (MetastaticTumor == 1)
                    chkMetastaticTumor.Checked = true;
                else
                    chkMetastaticTumor.Checked = false;
                this.chkMetastaticTumor.Checked = chkMetastaticTumor.Checked;

                this.Leukaemia = int.Parse(m_objAPACHEIIIValuationDB.strLeukaemiaChk);
                if (Leukaemia == 1)
                    chkLeukaemia.Checked = true;
                else
                    chkLeukaemia.Checked = false;
                this.chkLeukaemia.Checked = chkLeukaemia.Checked;


                this.Immunity = int.Parse(m_objAPACHEIIIValuationDB.strImmunityChk);
                if (Immunity == 1)
                    chkImmunity.Checked = true;
                else
                    chkImmunity.Checked = false;
                this.chkImmunity.Checked = chkImmunity.Checked;


                this.Hepatocirrhosis = int.Parse(m_objAPACHEIIIValuationDB.strHepatocirrhosisChk);
                if (Hepatocirrhosis == 1)
                    chkHepatocirrhosis.Checked = true;
                else
                    chkHepatocirrhosis.Checked = false;
                this.chkHepatocirrhosis.Checked = chkHepatocirrhosis.Checked;

                this.According = int.Parse(m_objAPACHEIIIValuationDB.strAccordingChk);
                if (According == 1)
                    chkAccording.Checked = true;
                else
                    chkAccording.Checked = false;
                this.chkAccording.Checked = chkAccording.Checked;


                this.PositionAche = int.Parse(m_objAPACHEIIIValuationDB.strPositionAcheChk);
                if (PositionAche == 1)
                    chkPositionAche.Checked = true;
                else
                    chkPositionAche.Checked = false;
                this.chkPositionAche.Checked = chkPositionAche.Checked;

                this.BodyBendAndVertical = int.Parse(m_objAPACHEIIIValuationDB.strBodyBendAndVerticalChk);
                if (BodyBendAndVertical == 1)
                    chkBodyBendAndVertical.Checked = true;
                else
                    chkBodyBendAndVertical.Checked = false;
                this.chkBodyBendAndVertical.Checked = chkBodyBendAndVertical.Checked;


                this.BrainUnreaction = int.Parse(m_objAPACHEIIIValuationDB.strBrainUnreactionChk);
                if (BrainUnreaction == 1)
                    chkBrainUnreaction.Checked = true;
                else
                    chkBrainUnreaction.Checked = false;
                this.chkBrainUnreaction.Checked = chkBrainUnreaction.Checked;

                object[] res = dtlResult.Rows[0].ItemArray;

                res[0] = m_objAPACHEIIIValuationDB.strAspVal;
                res[1] = m_objAPACHEIIIValuationDB.strPHAndPCO2Val;
                res[2] = m_objAPACHEIIIValuationDB.strAgeAndHealthVal;
                res[3] = m_objAPACHEIIIValuationDB.strNeuroneVal;
                res[4] = m_objAPACHEIIIValuationDB.strTotalVal;

                dtlResult.Rows[0].ItemArray = res;

                clsEmrEmployeeBase_VO objEmployee = null;
                clsBaseInfo.m_lngGetEmpByID(m_objAPACHEIIIValuationDB.strEvalDoctorID, out objEmployee);
                this.txtEvalDoctor.Text = objEmployee.m_strLASTNAME_VCHR;

                this.dtpEvalDate.Value = DateTime.Parse(m_objAPACHEIIIValuationDB.strActivityTime);
                #endregion
            }
            catch
            { }
        }

        private void m_cmdToAaDO2_Click(object sender, System.EventArgs e)
        {
            if (!m_blnInputValid(new TextBox[] { txtFiO2, txtPaCO2, txtPao2 }))
                return;

            txtDo2.Text = m_dblCalAaDO2(double.Parse(txtFiO2.Text), double.Parse(txtPaCO2.Text), double.Parse(txtPao2.Text)).ToString();
        }

        #region Print Function

        public override void m_mthSetPrint()
        {
            APACHEIIIValuationDB objValue;
            objPrintTool = new clsAPACHEIII_ValuationPrintTool();
            objPrintTool.m_mthInitPrintTool(null);
            if (m_objCurrentPatient == null)
                objPrintTool.m_mthSetPrintInfo(null, null, DateTime.MinValue);
            else
            {
                if (this.trvActivityTime.SelectedNode == null || this.trvActivityTime.SelectedNode == trvActivityTime.Nodes[0] || trvActivityTime.SelectedNode.Tag == null)
                    objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient, null, DateTime.MinValue);
                else
                {
                    objDomain.m_lngGetApacheIIIValue(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"), trvActivityTime.SelectedNode.Tag.ToString(), out objValue);
                    object obj = objValue;
                    objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient, obj, DateTime.Parse(trvActivityTime.SelectedNode.Tag.ToString()));
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
