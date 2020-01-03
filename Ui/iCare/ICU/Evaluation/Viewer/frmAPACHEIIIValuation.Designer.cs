using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iCare.ICU.Evaluation
{
    partial class frmAPACHEIIIValuation
    {
        private System.Windows.Forms.GroupBox groupBox1;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtPaCO2;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtFiO2;
        private System.Windows.Forms.Label lblFiO2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle14;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtPao2;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtDo2;
        private System.Windows.Forms.Label m_lblSmall2;
        private PinkieControls.ButtonXP cmdStartAuto;
        private PinkieControls.ButtonXP cmdStopAuto;
        private PinkieControls.ButtonXP cmdGetData;
        private PinkieControls.ButtonXP cmdShowResult;
        private PinkieControls.ButtonXP m_cmdToAaDO2;
        private PinkieControls.ButtonXP m_cmdEvalDoctor;
        private PinkieControls.ButtonXP cmdCalculate;
        private PinkieControls.ButtonXP m_cmdGetCheckData;
        private PinkieControls.ButtonXP buttonXP1;
        private System.Windows.Forms.GroupBox gpbOperaSel;
        private System.Windows.Forms.RadioButton rdbNoOperaSel;
        private System.Windows.Forms.RadioButton rdbOperaSel;
        private System.Windows.Forms.GroupBox gpbHealth;
        private System.Windows.Forms.CheckBox chkHepatocirrhosis;
        private System.Windows.Forms.CheckBox chkImmunity;
        private System.Windows.Forms.CheckBox chkLeukaemia;
        private System.Windows.Forms.CheckBox chkMetastaticTumor;
        private System.Windows.Forms.CheckBox chkLimphoma;
        private System.Windows.Forms.CheckBox chkLiverWane;
        private System.Windows.Forms.CheckBox chkAIDS;
        private System.Windows.Forms.GroupBox gpbAge;
        private System.Windows.Forms.RadioButton rdbAgeO85;
        private System.Windows.Forms.RadioButton rdbAgeU84;
        private System.Windows.Forms.RadioButton rdbAgeU74;
        private System.Windows.Forms.RadioButton rdbAgeU69;
        private System.Windows.Forms.RadioButton rdbAgeU64;
        private System.Windows.Forms.RadioButton rdbAgeU59;
        private System.Windows.Forms.RadioButton rdbAgeU44;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;

        #region GC
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (objPrintTool != null)
                    objPrintTool.m_mthDisposePrintTools(null);
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAPACHEIIIValuation));
            this.lblEvalDate = new System.Windows.Forms.Label();
            this.tabAPACHEIIValuation = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gpbExactLife = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDo2 = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_cmdToAaDO2 = new PinkieControls.ButtonXP();
            this.txtPao2 = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle14 = new System.Windows.Forms.Label();
            this.txtPaCO2 = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFiO2 = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblFiO2 = new System.Windows.Forms.Label();
            this.m_lblSmall2 = new System.Windows.Forms.Label();
            this.txtAdvArteryPress = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtPH = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtHypercholesterolemia = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtAmountLeucocyte = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtBloodCorpuscle = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtBloodFlesh = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtBreath = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtHR = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtTemperature = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtPCO2 = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblPH = new System.Windows.Forms.Label();
            this.txtBloodGallbladder = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.lblTitleHR = new System.Windows.Forms.Label();
            this.txtProteid = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle7 = new System.Windows.Forms.Label();
            this.lblTitle4 = new System.Windows.Forms.Label();
            this.txtHematuria = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle9 = new System.Windows.Forms.Label();
            this.txtBloodNa = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtUrineAmount = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.lblTitle8 = new System.Windows.Forms.Label();
            this.lblTitleHR5 = new System.Windows.Forms.Label();
            this.lblPCO2 = new System.Windows.Forms.Label();
            this.lblTitleHR6 = new System.Windows.Forms.Label();
            this.lblTitleHR4 = new System.Windows.Forms.Label();
            this.lblTitleHR1 = new System.Windows.Forms.Label();
            this.lblTitleHR2 = new System.Windows.Forms.Label();
            this.lblTitleHR3 = new System.Windows.Forms.Label();
            this.m_cmdGetCheckData = new PinkieControls.ButtonXP();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.chkKidneyWane = new System.Windows.Forms.CheckBox();
            this.chkMachineAerate = new System.Windows.Forms.CheckBox();
            this.m_cmdGetDovueData = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.gpbAge = new System.Windows.Forms.GroupBox();
            this.rdbAgeO85 = new System.Windows.Forms.RadioButton();
            this.rdbAgeU84 = new System.Windows.Forms.RadioButton();
            this.rdbAgeU74 = new System.Windows.Forms.RadioButton();
            this.rdbAgeU69 = new System.Windows.Forms.RadioButton();
            this.rdbAgeU64 = new System.Windows.Forms.RadioButton();
            this.rdbAgeU59 = new System.Windows.Forms.RadioButton();
            this.rdbAgeU44 = new System.Windows.Forms.RadioButton();
            this.gpbHealth = new System.Windows.Forms.GroupBox();
            this.chkHepatocirrhosis = new System.Windows.Forms.CheckBox();
            this.chkImmunity = new System.Windows.Forms.CheckBox();
            this.chkLeukaemia = new System.Windows.Forms.CheckBox();
            this.chkMetastaticTumor = new System.Windows.Forms.CheckBox();
            this.chkLimphoma = new System.Windows.Forms.CheckBox();
            this.chkLiverWane = new System.Windows.Forms.CheckBox();
            this.chkAIDS = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gpbOperaSel = new System.Windows.Forms.GroupBox();
            this.rdbNoOperaSel = new System.Windows.Forms.RadioButton();
            this.rdbOperaSel = new System.Windows.Forms.RadioButton();
            this.gpbOpenEye = new System.Windows.Forms.GroupBox();
            this.rdbCannotOpenEyes = new System.Windows.Forms.RadioButton();
            this.rdbCanOpenEyes = new System.Windows.Forms.RadioButton();
            this.gpbAcheAndLanguage1 = new System.Windows.Forms.GroupBox();
            this.chkBrainUnreaction = new System.Windows.Forms.CheckBox();
            this.chkBodyBendAndVertical = new System.Windows.Forms.CheckBox();
            this.chkPositionAche = new System.Windows.Forms.CheckBox();
            this.chkAccording = new System.Windows.Forms.CheckBox();
            this.lblSportChanged = new System.Windows.Forms.Label();
            this.rdbUnreaction = new System.Windows.Forms.RadioButton();
            this.rdbBlur = new System.Windows.Forms.RadioButton();
            this.rdbConfusion = new System.Windows.Forms.RadioButton();
            this.rdbRight = new System.Windows.Forms.RadioButton();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cmdStartAuto = new PinkieControls.ButtonXP();
            this.txtAutoTime = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.dtpStartSample = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblTitle96 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdStopAuto = new PinkieControls.ButtonXP();
            this.cmdGetData = new PinkieControls.ButtonXP();
            this.cmdShowResult = new PinkieControls.ButtonXP();
            this.dtgResult = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtpEvalDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.txtEvalDoctor = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.timAutoCollect = new System.Timers.Timer();
            this.m_cmdEvalDoctor = new PinkieControls.ButtonXP();
            this.cmdCalculate = new PinkieControls.ButtonXP();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.m_pnlNewBase.SuspendLayout();
            this.tabAPACHEIIValuation.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gpbExactLife.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gpbAge.SuspendLayout();
            this.gpbHealth.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gpbOperaSel.SuspendLayout();
            this.gpbOpenEye.SuspendLayout();
            this.gpbAcheAndLanguage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvActivityTime
            // 
            this.trvActivityTime.LineColor = System.Drawing.Color.Black;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowHomePlace = true;
            this.m_ctlPatientInfo.m_BlnIsShowMarriage = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            // 
            // lblEvalDate
            // 
            this.lblEvalDate.AutoSize = true;
            this.lblEvalDate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEvalDate.Location = new System.Drawing.Point(4, 442);
            this.lblEvalDate.Name = "lblEvalDate";
            this.lblEvalDate.Size = new System.Drawing.Size(85, 16);
            this.lblEvalDate.TabIndex = 404;
            this.lblEvalDate.Text = "评分日期:";
            // 
            // tabAPACHEIIValuation
            // 
            this.tabAPACHEIIValuation.Controls.Add(this.tabPage1);
            this.tabAPACHEIIValuation.Controls.Add(this.tabPage3);
            this.tabAPACHEIIValuation.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabAPACHEIIValuation.Location = new System.Drawing.Point(4, 96);
            this.tabAPACHEIIValuation.Name = "tabAPACHEIIValuation";
            this.tabAPACHEIIValuation.SelectedIndex = 0;
            this.tabAPACHEIIValuation.Size = new System.Drawing.Size(840, 336);
            this.tabAPACHEIIValuation.TabIndex = 48;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.gpbExactLife);
            this.tabPage1.Controls.Add(this.m_cmdGetDovueData);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(832, 309);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "APACHEIII-ASP和碱酸失衡评分标准";
            // 
            // gpbExactLife
            // 
            this.gpbExactLife.Controls.Add(this.groupBox1);
            this.gpbExactLife.Controls.Add(this.txtAdvArteryPress);
            this.gpbExactLife.Controls.Add(this.txtPH);
            this.gpbExactLife.Controls.Add(this.txtHypercholesterolemia);
            this.gpbExactLife.Controls.Add(this.txtAmountLeucocyte);
            this.gpbExactLife.Controls.Add(this.txtBloodCorpuscle);
            this.gpbExactLife.Controls.Add(this.txtBloodFlesh);
            this.gpbExactLife.Controls.Add(this.txtBreath);
            this.gpbExactLife.Controls.Add(this.txtHR);
            this.gpbExactLife.Controls.Add(this.txtTemperature);
            this.gpbExactLife.Controls.Add(this.txtPCO2);
            this.gpbExactLife.Controls.Add(this.lblPH);
            this.gpbExactLife.Controls.Add(this.txtBloodGallbladder);
            this.gpbExactLife.Controls.Add(this.lblTitle2);
            this.gpbExactLife.Controls.Add(this.lblTitleHR);
            this.gpbExactLife.Controls.Add(this.txtProteid);
            this.gpbExactLife.Controls.Add(this.lblTitle7);
            this.gpbExactLife.Controls.Add(this.lblTitle4);
            this.gpbExactLife.Controls.Add(this.txtHematuria);
            this.gpbExactLife.Controls.Add(this.lblTitle9);
            this.gpbExactLife.Controls.Add(this.txtBloodNa);
            this.gpbExactLife.Controls.Add(this.txtUrineAmount);
            this.gpbExactLife.Controls.Add(this.lblTitle3);
            this.gpbExactLife.Controls.Add(this.lblTitle8);
            this.gpbExactLife.Controls.Add(this.lblTitleHR5);
            this.gpbExactLife.Controls.Add(this.lblPCO2);
            this.gpbExactLife.Controls.Add(this.lblTitleHR6);
            this.gpbExactLife.Controls.Add(this.lblTitleHR4);
            this.gpbExactLife.Controls.Add(this.lblTitleHR1);
            this.gpbExactLife.Controls.Add(this.lblTitleHR2);
            this.gpbExactLife.Controls.Add(this.lblTitleHR3);
            this.gpbExactLife.Controls.Add(this.m_cmdGetCheckData);
            this.gpbExactLife.Controls.Add(this.buttonXP1);
            this.gpbExactLife.Controls.Add(this.chkKidneyWane);
            this.gpbExactLife.Controls.Add(this.chkMachineAerate);
            this.gpbExactLife.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbExactLife.Location = new System.Drawing.Point(4, 0);
            this.gpbExactLife.Name = "gpbExactLife";
            this.gpbExactLife.Size = new System.Drawing.Size(819, 304);
            this.gpbExactLife.TabIndex = 69;
            this.gpbExactLife.TabStop = false;
            this.gpbExactLife.Text = "APS和酸碱失蘅";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDo2);
            this.groupBox1.Controls.Add(this.m_cmdToAaDO2);
            this.groupBox1.Controls.Add(this.txtPao2);
            this.groupBox1.Controls.Add(this.lblTitle14);
            this.groupBox1.Controls.Add(this.txtPaCO2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtFiO2);
            this.groupBox1.Controls.Add(this.lblFiO2);
            this.groupBox1.Controls.Add(this.m_lblSmall2);
            this.groupBox1.Location = new System.Drawing.Point(4, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 144);
            this.groupBox1.TabIndex = 215;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "(A－a)DO 肺泡动脉血氧分压差:";
            // 
            // txtDo2
            // 
            this.txtDo2.BackColor = System.Drawing.Color.White;
            this.txtDo2.BorderColor = System.Drawing.Color.Black;
            this.txtDo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDo2.ForeColor = System.Drawing.Color.Black;
            this.txtDo2.Location = new System.Drawing.Point(140, 116);
            this.txtDo2.Name = "txtDo2";
            this.txtDo2.Size = new System.Drawing.Size(87, 23);
            this.txtDo2.TabIndex = 260;
            // 
            // m_cmdToAaDO2
            // 
            this.m_cmdToAaDO2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdToAaDO2.DefaultScheme = true;
            this.m_cmdToAaDO2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdToAaDO2.ForeColor = System.Drawing.Color.Black;
            this.m_cmdToAaDO2.Hint = "";
            this.m_cmdToAaDO2.Location = new System.Drawing.Point(12, 112);
            this.m_cmdToAaDO2.Name = "m_cmdToAaDO2";
            this.m_cmdToAaDO2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdToAaDO2.Size = new System.Drawing.Size(120, 28);
            this.m_cmdToAaDO2.TabIndex = 10000018;
            this.m_cmdToAaDO2.Text = "=";
            this.m_cmdToAaDO2.Click += new System.EventHandler(this.m_cmdToAaDO2_Click);
            // 
            // txtPao2
            // 
            this.txtPao2.BackColor = System.Drawing.Color.White;
            this.txtPao2.BorderColor = System.Drawing.Color.Black;
            this.txtPao2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPao2.ForeColor = System.Drawing.Color.Black;
            this.txtPao2.Location = new System.Drawing.Point(140, 80);
            this.txtPao2.Name = "txtPao2";
            this.txtPao2.Size = new System.Drawing.Size(87, 23);
            this.txtPao2.TabIndex = 240;
            // 
            // lblTitle14
            // 
            this.lblTitle14.Location = new System.Drawing.Point(4, 80);
            this.lblTitle14.Name = "lblTitle14";
            this.lblTitle14.Size = new System.Drawing.Size(144, 32);
            this.lblTitle14.TabIndex = 0;
            this.lblTitle14.Text = "PaO2(动脉血氧分压)(mmHg):";
            // 
            // txtPaCO2
            // 
            this.txtPaCO2.BackColor = System.Drawing.Color.White;
            this.txtPaCO2.BorderColor = System.Drawing.Color.Black;
            this.txtPaCO2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPaCO2.ForeColor = System.Drawing.Color.Black;
            this.txtPaCO2.Location = new System.Drawing.Point(140, 48);
            this.txtPaCO2.Name = "txtPaCO2";
            this.txtPaCO2.Size = new System.Drawing.Size(87, 23);
            this.txtPaCO2.TabIndex = 230;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 40);
            this.label1.TabIndex = 198;
            this.label1.Text = "PaCO2(动脉血二氧化碳分压)(mmol/L):";
            // 
            // txtFiO2
            // 
            this.txtFiO2.BackColor = System.Drawing.Color.White;
            this.txtFiO2.BorderColor = System.Drawing.Color.Black;
            this.txtFiO2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFiO2.ForeColor = System.Drawing.Color.Black;
            this.txtFiO2.Location = new System.Drawing.Point(140, 20);
            this.txtFiO2.Name = "txtFiO2";
            this.txtFiO2.Size = new System.Drawing.Size(87, 23);
            this.txtFiO2.TabIndex = 220;
            // 
            // lblFiO2
            // 
            this.lblFiO2.Location = new System.Drawing.Point(4, 20);
            this.lblFiO2.Name = "lblFiO2";
            this.lblFiO2.Size = new System.Drawing.Size(128, 19);
            this.lblFiO2.TabIndex = 4;
            this.lblFiO2.Text = "FiO2(吸入氧浓度):";
            // 
            // m_lblSmall2
            // 
            this.m_lblSmall2.AutoSize = true;
            this.m_lblSmall2.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblSmall2.Location = new System.Drawing.Point(68, 0);
            this.m_lblSmall2.Name = "m_lblSmall2";
            this.m_lblSmall2.Size = new System.Drawing.Size(10, 10);
            this.m_lblSmall2.TabIndex = 403;
            this.m_lblSmall2.Text = "2";
            // 
            // txtAdvArteryPress
            // 
            this.txtAdvArteryPress.BackColor = System.Drawing.Color.White;
            this.txtAdvArteryPress.BorderColor = System.Drawing.Color.Black;
            this.txtAdvArteryPress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdvArteryPress.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAdvArteryPress.ForeColor = System.Drawing.Color.Black;
            this.txtAdvArteryPress.Location = new System.Drawing.Point(144, 48);
            this.txtAdvArteryPress.Name = "txtAdvArteryPress";
            this.txtAdvArteryPress.Size = new System.Drawing.Size(87, 23);
            this.txtAdvArteryPress.TabIndex = 100;
            // 
            // txtPH
            // 
            this.txtPH.BackColor = System.Drawing.Color.White;
            this.txtPH.BorderColor = System.Drawing.Color.Black;
            this.txtPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPH.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPH.ForeColor = System.Drawing.Color.Black;
            this.txtPH.Location = new System.Drawing.Point(144, 132);
            this.txtPH.Name = "txtPH";
            this.txtPH.Size = new System.Drawing.Size(87, 23);
            this.txtPH.TabIndex = 190;
            // 
            // txtHypercholesterolemia
            // 
            this.txtHypercholesterolemia.BackColor = System.Drawing.Color.White;
            this.txtHypercholesterolemia.BorderColor = System.Drawing.Color.Black;
            this.txtHypercholesterolemia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHypercholesterolemia.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHypercholesterolemia.ForeColor = System.Drawing.Color.Black;
            this.txtHypercholesterolemia.Location = new System.Drawing.Point(448, 104);
            this.txtHypercholesterolemia.Name = "txtHypercholesterolemia";
            this.txtHypercholesterolemia.Size = new System.Drawing.Size(87, 23);
            this.txtHypercholesterolemia.TabIndex = 170;
            // 
            // txtAmountLeucocyte
            // 
            this.txtAmountLeucocyte.BackColor = System.Drawing.Color.White;
            this.txtAmountLeucocyte.BorderColor = System.Drawing.Color.Black;
            this.txtAmountLeucocyte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountLeucocyte.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAmountLeucocyte.ForeColor = System.Drawing.Color.Black;
            this.txtAmountLeucocyte.Location = new System.Drawing.Point(448, 48);
            this.txtAmountLeucocyte.Name = "txtAmountLeucocyte";
            this.txtAmountLeucocyte.Size = new System.Drawing.Size(87, 23);
            this.txtAmountLeucocyte.TabIndex = 110;
            // 
            // txtBloodCorpuscle
            // 
            this.txtBloodCorpuscle.BackColor = System.Drawing.Color.White;
            this.txtBloodCorpuscle.BorderColor = System.Drawing.Color.Black;
            this.txtBloodCorpuscle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBloodCorpuscle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBloodCorpuscle.ForeColor = System.Drawing.Color.Black;
            this.txtBloodCorpuscle.Location = new System.Drawing.Point(448, 20);
            this.txtBloodCorpuscle.Name = "txtBloodCorpuscle";
            this.txtBloodCorpuscle.Size = new System.Drawing.Size(87, 23);
            this.txtBloodCorpuscle.TabIndex = 80;
            // 
            // txtBloodFlesh
            // 
            this.txtBloodFlesh.BackColor = System.Drawing.Color.White;
            this.txtBloodFlesh.BorderColor = System.Drawing.Color.Black;
            this.txtBloodFlesh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBloodFlesh.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBloodFlesh.ForeColor = System.Drawing.Color.Black;
            this.txtBloodFlesh.Location = new System.Drawing.Point(448, 76);
            this.txtBloodFlesh.Name = "txtBloodFlesh";
            this.txtBloodFlesh.Size = new System.Drawing.Size(87, 23);
            this.txtBloodFlesh.TabIndex = 140;
            // 
            // txtBreath
            // 
            this.txtBreath.BackColor = System.Drawing.Color.White;
            this.txtBreath.BorderColor = System.Drawing.Color.Black;
            this.txtBreath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBreath.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBreath.ForeColor = System.Drawing.Color.Black;
            this.txtBreath.Location = new System.Drawing.Point(144, 104);
            this.txtBreath.Name = "txtBreath";
            this.txtBreath.Size = new System.Drawing.Size(87, 23);
            this.txtBreath.TabIndex = 160;
            // 
            // txtHR
            // 
            this.txtHR.BackColor = System.Drawing.Color.White;
            this.txtHR.BorderColor = System.Drawing.Color.Black;
            this.txtHR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHR.ForeColor = System.Drawing.Color.Black;
            this.txtHR.Location = new System.Drawing.Point(144, 20);
            this.txtHR.Name = "txtHR";
            this.txtHR.Size = new System.Drawing.Size(87, 23);
            this.txtHR.TabIndex = 70;
            // 
            // txtTemperature
            // 
            this.txtTemperature.BackColor = System.Drawing.Color.White;
            this.txtTemperature.BorderColor = System.Drawing.Color.Black;
            this.txtTemperature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTemperature.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTemperature.ForeColor = System.Drawing.Color.Black;
            this.txtTemperature.Location = new System.Drawing.Point(144, 76);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(87, 23);
            this.txtTemperature.TabIndex = 130;
            // 
            // txtPCO2
            // 
            this.txtPCO2.BackColor = System.Drawing.Color.White;
            this.txtPCO2.BorderColor = System.Drawing.Color.Black;
            this.txtPCO2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPCO2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPCO2.ForeColor = System.Drawing.Color.Black;
            this.txtPCO2.Location = new System.Drawing.Point(448, 132);
            this.txtPCO2.Name = "txtPCO2";
            this.txtPCO2.Size = new System.Drawing.Size(87, 23);
            this.txtPCO2.TabIndex = 200;
            // 
            // lblPH
            // 
            this.lblPH.AutoSize = true;
            this.lblPH.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPH.Location = new System.Drawing.Point(8, 135);
            this.lblPH.Name = "lblPH";
            this.lblPH.Size = new System.Drawing.Size(31, 14);
            this.lblPH.TabIndex = 401;
            this.lblPH.Text = "PH:";
            // 
            // txtBloodGallbladder
            // 
            this.txtBloodGallbladder.BackColor = System.Drawing.Color.White;
            this.txtBloodGallbladder.BorderColor = System.Drawing.Color.Black;
            this.txtBloodGallbladder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBloodGallbladder.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBloodGallbladder.ForeColor = System.Drawing.Color.Black;
            this.txtBloodGallbladder.Location = new System.Drawing.Point(448, 268);
            this.txtBloodGallbladder.Name = "txtBloodGallbladder";
            this.txtBloodGallbladder.Size = new System.Drawing.Size(87, 23);
            this.txtBloodGallbladder.TabIndex = 210;
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle2.Location = new System.Drawing.Point(8, 48);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(138, 14);
            this.lblTitle2.TabIndex = 0;
            this.lblTitle2.Text = "平均动脉压(mmHg):";
            this.lblTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleHR
            // 
            this.lblTitleHR.AutoSize = true;
            this.lblTitleHR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleHR.Location = new System.Drawing.Point(8, 20);
            this.lblTitleHR.Name = "lblTitleHR";
            this.lblTitleHR.Size = new System.Drawing.Size(93, 14);
            this.lblTitleHR.TabIndex = 0;
            this.lblTitleHR.Text = "心率(/min):";
            this.lblTitleHR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtProteid
            // 
            this.txtProteid.BackColor = System.Drawing.Color.White;
            this.txtProteid.BorderColor = System.Drawing.Color.Black;
            this.txtProteid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProteid.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProteid.ForeColor = System.Drawing.Color.Black;
            this.txtProteid.Location = new System.Drawing.Point(448, 240);
            this.txtProteid.Name = "txtProteid";
            this.txtProteid.Size = new System.Drawing.Size(87, 23);
            this.txtProteid.TabIndex = 180;
            // 
            // lblTitle7
            // 
            this.lblTitle7.AutoSize = true;
            this.lblTitle7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle7.Location = new System.Drawing.Point(274, 20);
            this.lblTitle7.Name = "lblTitle7";
            this.lblTitle7.Size = new System.Drawing.Size(121, 14);
            this.lblTitle7.TabIndex = 0;
            this.lblTitle7.Text = "血细胞比容(％):";
            this.lblTitle7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle4
            // 
            this.lblTitle4.AutoSize = true;
            this.lblTitle4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle4.Location = new System.Drawing.Point(8, 104);
            this.lblTitle4.Name = "lblTitle4";
            this.lblTitle4.Size = new System.Drawing.Size(123, 14);
            this.lblTitle4.TabIndex = 0;
            this.lblTitle4.Text = "呼吸频率(/min):";
            this.lblTitle4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHematuria
            // 
            this.txtHematuria.BackColor = System.Drawing.Color.White;
            this.txtHematuria.BorderColor = System.Drawing.Color.Black;
            this.txtHematuria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHematuria.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHematuria.ForeColor = System.Drawing.Color.Black;
            this.txtHematuria.Location = new System.Drawing.Point(448, 184);
            this.txtHematuria.Name = "txtHematuria";
            this.txtHematuria.Size = new System.Drawing.Size(87, 23);
            this.txtHematuria.TabIndex = 120;
            // 
            // lblTitle9
            // 
            this.lblTitle9.AutoSize = true;
            this.lblTitle9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle9.Location = new System.Drawing.Point(274, 76);
            this.lblTitle9.Name = "lblTitle9";
            this.lblTitle9.Size = new System.Drawing.Size(154, 14);
            this.lblTitle9.TabIndex = 0;
            this.lblTitle9.Text = "血肌酐浓度(mmol/L):";
            this.lblTitle9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBloodNa
            // 
            this.txtBloodNa.BackColor = System.Drawing.Color.White;
            this.txtBloodNa.BorderColor = System.Drawing.Color.Black;
            this.txtBloodNa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBloodNa.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBloodNa.ForeColor = System.Drawing.Color.Black;
            this.txtBloodNa.Location = new System.Drawing.Point(448, 212);
            this.txtBloodNa.Name = "txtBloodNa";
            this.txtBloodNa.Size = new System.Drawing.Size(87, 23);
            this.txtBloodNa.TabIndex = 150;
            // 
            // txtUrineAmount
            // 
            this.txtUrineAmount.BackColor = System.Drawing.Color.White;
            this.txtUrineAmount.BorderColor = System.Drawing.Color.Black;
            this.txtUrineAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUrineAmount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUrineAmount.ForeColor = System.Drawing.Color.Black;
            this.txtUrineAmount.Location = new System.Drawing.Point(448, 156);
            this.txtUrineAmount.Name = "txtUrineAmount";
            this.txtUrineAmount.Size = new System.Drawing.Size(87, 23);
            this.txtUrineAmount.TabIndex = 90;
            // 
            // lblTitle3
            // 
            this.lblTitle3.AutoSize = true;
            this.lblTitle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle3.Location = new System.Drawing.Point(8, 76);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(76, 14);
            this.lblTitle3.TabIndex = 0;
            this.lblTitle3.Text = "体温(℃):";
            this.lblTitle3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle8
            // 
            this.lblTitle8.AutoSize = true;
            this.lblTitle8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle8.Location = new System.Drawing.Point(274, 48);
            this.lblTitle8.Name = "lblTitle8";
            this.lblTitle8.Size = new System.Drawing.Size(162, 14);
            this.lblTitle8.TabIndex = 2;
            this.lblTitle8.Text = "白细胞计数(*10^9/L):";
            this.lblTitle8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleHR5
            // 
            this.lblTitleHR5.AutoSize = true;
            this.lblTitleHR5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleHR5.Location = new System.Drawing.Point(274, 104);
            this.lblTitleHR5.Name = "lblTitleHR5";
            this.lblTitleHR5.Size = new System.Drawing.Size(146, 14);
            this.lblTitleHR5.TabIndex = 0;
            this.lblTitleHR5.Text = "总胆红素(μmol/L):";
            this.lblTitleHR5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPCO2
            // 
            this.lblPCO2.AutoSize = true;
            this.lblPCO2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPCO2.Location = new System.Drawing.Point(274, 132);
            this.lblPCO2.Name = "lblPCO2";
            this.lblPCO2.Size = new System.Drawing.Size(153, 14);
            this.lblPCO2.TabIndex = 402;
            this.lblPCO2.Text = "PCO2(二氧化碳分压):";
            this.lblPCO2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleHR6
            // 
            this.lblTitleHR6.AutoSize = true;
            this.lblTitleHR6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleHR6.Location = new System.Drawing.Point(274, 268);
            this.lblTitleHR6.Name = "lblTitleHR6";
            this.lblTitleHR6.Size = new System.Drawing.Size(109, 14);
            this.lblTitleHR6.TabIndex = 11;
            this.lblTitleHR6.Text = "血糖(mmol/L):";
            this.lblTitleHR6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleHR4
            // 
            this.lblTitleHR4.AutoSize = true;
            this.lblTitleHR4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleHR4.Location = new System.Drawing.Point(274, 240);
            this.lblTitleHR4.Name = "lblTitleHR4";
            this.lblTitleHR4.Size = new System.Drawing.Size(100, 14);
            this.lblTitleHR4.TabIndex = 0;
            this.lblTitleHR4.Text = "白蛋白(g/L):";
            this.lblTitleHR4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleHR1
            // 
            this.lblTitleHR1.AutoSize = true;
            this.lblTitleHR1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleHR1.Location = new System.Drawing.Point(274, 156);
            this.lblTitleHR1.Name = "lblTitleHR1";
            this.lblTitleHR1.Size = new System.Drawing.Size(93, 14);
            this.lblTitleHR1.TabIndex = 0;
            this.lblTitleHR1.Text = "尿量(ml/d):";
            this.lblTitleHR1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleHR2
            // 
            this.lblTitleHR2.AutoSize = true;
            this.lblTitleHR2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleHR2.Location = new System.Drawing.Point(274, 184);
            this.lblTitleHR2.Name = "lblTitleHR2";
            this.lblTitleHR2.Size = new System.Drawing.Size(139, 14);
            this.lblTitleHR2.TabIndex = 0;
            this.lblTitleHR2.Text = "血尿素氮(mmol/L):";
            this.lblTitleHR2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleHR3
            // 
            this.lblTitleHR3.AutoSize = true;
            this.lblTitleHR3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleHR3.Location = new System.Drawing.Point(274, 212);
            this.lblTitleHR3.Name = "lblTitleHR3";
            this.lblTitleHR3.Size = new System.Drawing.Size(109, 14);
            this.lblTitleHR3.TabIndex = 0;
            this.lblTitleHR3.Text = "血钠(mmol/L):";
            this.lblTitleHR3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_cmdGetCheckData
            // 
            this.m_cmdGetCheckData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdGetCheckData.DefaultScheme = true;
            this.m_cmdGetCheckData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGetCheckData.ForeColor = System.Drawing.Color.Black;
            this.m_cmdGetCheckData.Hint = "";
            this.m_cmdGetCheckData.Location = new System.Drawing.Point(640, 20);
            this.m_cmdGetCheckData.Name = "m_cmdGetCheckData";
            this.m_cmdGetCheckData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetCheckData.Size = new System.Drawing.Size(152, 28);
            this.m_cmdGetCheckData.TabIndex = 10000016;
            this.m_cmdGetCheckData.Text = "获取检验结果(&L)";
            this.m_cmdGetCheckData.Click += new System.EventHandler(this.m_cmdGetCheckData_Click);
            // 
            // buttonXP1
            // 
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.ForeColor = System.Drawing.Color.Black;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(640, 56);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(152, 28);
            this.buttonXP1.TabIndex = 10000016;
            this.buttonXP1.Text = "获取监护结果(&G)";
            this.buttonXP1.Click += new System.EventHandler(this.cmdGetData_Click);
            // 
            // chkKidneyWane
            // 
            this.chkKidneyWane.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkKidneyWane.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkKidneyWane.Location = new System.Drawing.Point(640, 100);
            this.chkKidneyWane.Name = "chkKidneyWane";
            this.chkKidneyWane.Size = new System.Drawing.Size(133, 24);
            this.chkKidneyWane.TabIndex = 60;
            this.chkKidneyWane.Text = "是急性肾衰竭";
            // 
            // chkMachineAerate
            // 
            this.chkMachineAerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkMachineAerate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkMachineAerate.Location = new System.Drawing.Point(640, 132);
            this.chkMachineAerate.Name = "chkMachineAerate";
            this.chkMachineAerate.Size = new System.Drawing.Size(160, 24);
            this.chkMachineAerate.TabIndex = 50;
            this.chkMachineAerate.Text = "是机械通气患者";
            // 
            // m_cmdGetDovueData
            // 
            this.m_cmdGetDovueData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cmdGetDovueData.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdGetDovueData.Location = new System.Drawing.Point(712, 4);
            this.m_cmdGetDovueData.Name = "m_cmdGetDovueData";
            this.m_cmdGetDovueData.Size = new System.Drawing.Size(31, 32);
            this.m_cmdGetDovueData.TabIndex = 10000006;
            this.m_cmdGetDovueData.Text = "监护仪最新结果";
            this.m_cmdGetDovueData.Visible = false;
            this.m_cmdGetDovueData.Click += new System.EventHandler(this.m_cmdGetDovueData_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(832, 309);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "APACHEIII-神经功能异常、-年龄和既往健康评分标准";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.gpbAge);
            this.groupBox4.Controls.Add(this.gpbHealth);
            this.groupBox4.Location = new System.Drawing.Point(404, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(404, 292);
            this.groupBox4.TabIndex = 434;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "年龄和既往健康";
            // 
            // gpbAge
            // 
            this.gpbAge.Controls.Add(this.rdbAgeO85);
            this.gpbAge.Controls.Add(this.rdbAgeU84);
            this.gpbAge.Controls.Add(this.rdbAgeU74);
            this.gpbAge.Controls.Add(this.rdbAgeU69);
            this.gpbAge.Controls.Add(this.rdbAgeU64);
            this.gpbAge.Controls.Add(this.rdbAgeU59);
            this.gpbAge.Controls.Add(this.rdbAgeU44);
            this.gpbAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbAge.Location = new System.Drawing.Point(16, 16);
            this.gpbAge.Name = "gpbAge";
            this.gpbAge.Size = new System.Drawing.Size(148, 260);
            this.gpbAge.TabIndex = 431;
            this.gpbAge.TabStop = false;
            this.gpbAge.Text = "年龄";
            // 
            // rdbAgeO85
            // 
            this.rdbAgeO85.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAgeO85.Location = new System.Drawing.Point(8, 228);
            this.rdbAgeO85.Name = "rdbAgeO85";
            this.rdbAgeO85.Size = new System.Drawing.Size(119, 24);
            this.rdbAgeO85.TabIndex = 310;
            this.rdbAgeO85.TabStop = true;
            this.rdbAgeO85.Tag = "24";
            this.rdbAgeO85.Text = "85岁以上";
            // 
            // rdbAgeU84
            // 
            this.rdbAgeU84.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAgeU84.Location = new System.Drawing.Point(8, 196);
            this.rdbAgeU84.Name = "rdbAgeU84";
            this.rdbAgeU84.Size = new System.Drawing.Size(101, 24);
            this.rdbAgeU84.TabIndex = 300;
            this.rdbAgeU84.TabStop = true;
            this.rdbAgeU84.Tag = "17";
            this.rdbAgeU84.Text = "75~84岁";
            // 
            // rdbAgeU74
            // 
            this.rdbAgeU74.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAgeU74.Location = new System.Drawing.Point(8, 164);
            this.rdbAgeU74.Name = "rdbAgeU74";
            this.rdbAgeU74.Size = new System.Drawing.Size(86, 24);
            this.rdbAgeU74.TabIndex = 290;
            this.rdbAgeU74.TabStop = true;
            this.rdbAgeU74.Tag = "16";
            this.rdbAgeU74.Text = "70~74岁";
            // 
            // rdbAgeU69
            // 
            this.rdbAgeU69.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAgeU69.Location = new System.Drawing.Point(8, 128);
            this.rdbAgeU69.Name = "rdbAgeU69";
            this.rdbAgeU69.Size = new System.Drawing.Size(87, 24);
            this.rdbAgeU69.TabIndex = 280;
            this.rdbAgeU69.TabStop = true;
            this.rdbAgeU69.Tag = "13";
            this.rdbAgeU69.Text = "65~69岁";
            // 
            // rdbAgeU64
            // 
            this.rdbAgeU64.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAgeU64.Location = new System.Drawing.Point(8, 92);
            this.rdbAgeU64.Name = "rdbAgeU64";
            this.rdbAgeU64.Size = new System.Drawing.Size(96, 24);
            this.rdbAgeU64.TabIndex = 270;
            this.rdbAgeU64.TabStop = true;
            this.rdbAgeU64.Tag = "11";
            this.rdbAgeU64.Text = "60~64岁";
            // 
            // rdbAgeU59
            // 
            this.rdbAgeU59.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAgeU59.Location = new System.Drawing.Point(8, 60);
            this.rdbAgeU59.Name = "rdbAgeU59";
            this.rdbAgeU59.Size = new System.Drawing.Size(100, 24);
            this.rdbAgeU59.TabIndex = 260;
            this.rdbAgeU59.TabStop = true;
            this.rdbAgeU59.Tag = "5";
            this.rdbAgeU59.Text = "45~59岁";
            // 
            // rdbAgeU44
            // 
            this.rdbAgeU44.Checked = true;
            this.rdbAgeU44.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAgeU44.Location = new System.Drawing.Point(8, 24);
            this.rdbAgeU44.Name = "rdbAgeU44";
            this.rdbAgeU44.Size = new System.Drawing.Size(96, 24);
            this.rdbAgeU44.TabIndex = 250;
            this.rdbAgeU44.TabStop = true;
            this.rdbAgeU44.Tag = "0";
            this.rdbAgeU44.Text = "小于44岁";
            // 
            // gpbHealth
            // 
            this.gpbHealth.Controls.Add(this.chkHepatocirrhosis);
            this.gpbHealth.Controls.Add(this.chkImmunity);
            this.gpbHealth.Controls.Add(this.chkLeukaemia);
            this.gpbHealth.Controls.Add(this.chkMetastaticTumor);
            this.gpbHealth.Controls.Add(this.chkLimphoma);
            this.gpbHealth.Controls.Add(this.chkLiverWane);
            this.gpbHealth.Controls.Add(this.chkAIDS);
            this.gpbHealth.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbHealth.Location = new System.Drawing.Point(208, 12);
            this.gpbHealth.Name = "gpbHealth";
            this.gpbHealth.Size = new System.Drawing.Size(164, 264);
            this.gpbHealth.TabIndex = 432;
            this.gpbHealth.TabStop = false;
            this.gpbHealth.Text = "既往健康状况";
            // 
            // chkHepatocirrhosis
            // 
            this.chkHepatocirrhosis.Location = new System.Drawing.Point(12, 232);
            this.chkHepatocirrhosis.Name = "chkHepatocirrhosis";
            this.chkHepatocirrhosis.Size = new System.Drawing.Size(108, 24);
            this.chkHepatocirrhosis.TabIndex = 390;
            this.chkHepatocirrhosis.Tag = "4";
            this.chkHepatocirrhosis.Text = "肝硬化";
            // 
            // chkImmunity
            // 
            this.chkImmunity.Location = new System.Drawing.Point(12, 60);
            this.chkImmunity.Name = "chkImmunity";
            this.chkImmunity.Size = new System.Drawing.Size(132, 24);
            this.chkImmunity.TabIndex = 370;
            this.chkImmunity.Tag = "10";
            this.chkImmunity.Text = "免疫抑制";
            // 
            // chkLeukaemia
            // 
            this.chkLeukaemia.Location = new System.Drawing.Point(12, 128);
            this.chkLeukaemia.Name = "chkLeukaemia";
            this.chkLeukaemia.Size = new System.Drawing.Size(148, 24);
            this.chkLeukaemia.TabIndex = 350;
            this.chkLeukaemia.Tag = "10";
            this.chkLeukaemia.Text = "白血病/多发骨髓瘤";
            // 
            // chkMetastaticTumor
            // 
            this.chkMetastaticTumor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkMetastaticTumor.Location = new System.Drawing.Point(12, 92);
            this.chkMetastaticTumor.Name = "chkMetastaticTumor";
            this.chkMetastaticTumor.Size = new System.Drawing.Size(132, 24);
            this.chkMetastaticTumor.TabIndex = 400;
            this.chkMetastaticTumor.Tag = "11";
            this.chkMetastaticTumor.Text = "转移瘤";
            // 
            // chkLimphoma
            // 
            this.chkLimphoma.Location = new System.Drawing.Point(12, 164);
            this.chkLimphoma.Name = "chkLimphoma";
            this.chkLimphoma.Size = new System.Drawing.Size(128, 24);
            this.chkLimphoma.TabIndex = 380;
            this.chkLimphoma.Tag = "13";
            this.chkLimphoma.Text = "淋巴瘤";
            // 
            // chkLiverWane
            // 
            this.chkLiverWane.Location = new System.Drawing.Point(12, 196);
            this.chkLiverWane.Name = "chkLiverWane";
            this.chkLiverWane.Size = new System.Drawing.Size(108, 24);
            this.chkLiverWane.TabIndex = 360;
            this.chkLiverWane.Tag = "16";
            this.chkLiverWane.Text = "肝功能衰竭";
            // 
            // chkAIDS
            // 
            this.chkAIDS.Location = new System.Drawing.Point(12, 24);
            this.chkAIDS.Name = "chkAIDS";
            this.chkAIDS.Size = new System.Drawing.Size(132, 24);
            this.chkAIDS.TabIndex = 340;
            this.chkAIDS.Tag = "23";
            this.chkAIDS.Text = "AIDS";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gpbOperaSel);
            this.groupBox3.Controls.Add(this.gpbOpenEye);
            this.groupBox3.Controls.Add(this.gpbAcheAndLanguage1);
            this.groupBox3.Location = new System.Drawing.Point(4, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(380, 292);
            this.groupBox3.TabIndex = 433;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "神经功能异常评判";
            // 
            // gpbOperaSel
            // 
            this.gpbOperaSel.Controls.Add(this.rdbNoOperaSel);
            this.gpbOperaSel.Controls.Add(this.rdbOperaSel);
            this.gpbOperaSel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbOperaSel.Location = new System.Drawing.Point(12, 28);
            this.gpbOperaSel.Name = "gpbOperaSel";
            this.gpbOperaSel.Size = new System.Drawing.Size(144, 60);
            this.gpbOperaSel.TabIndex = 430;
            this.gpbOperaSel.TabStop = false;
            this.gpbOperaSel.Text = "是否择期手术患者";
            // 
            // rdbNoOperaSel
            // 
            this.rdbNoOperaSel.Checked = true;
            this.rdbNoOperaSel.Location = new System.Drawing.Point(68, 24);
            this.rdbNoOperaSel.Name = "rdbNoOperaSel";
            this.rdbNoOperaSel.Size = new System.Drawing.Size(41, 24);
            this.rdbNoOperaSel.TabIndex = 330;
            this.rdbNoOperaSel.TabStop = true;
            this.rdbNoOperaSel.Text = "否";
            // 
            // rdbOperaSel
            // 
            this.rdbOperaSel.Location = new System.Drawing.Point(16, 24);
            this.rdbOperaSel.Name = "rdbOperaSel";
            this.rdbOperaSel.Size = new System.Drawing.Size(46, 24);
            this.rdbOperaSel.TabIndex = 320;
            this.rdbOperaSel.TabStop = true;
            this.rdbOperaSel.Text = "是";
            // 
            // gpbOpenEye
            // 
            this.gpbOpenEye.Controls.Add(this.rdbCannotOpenEyes);
            this.gpbOpenEye.Controls.Add(this.rdbCanOpenEyes);
            this.gpbOpenEye.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbOpenEye.Location = new System.Drawing.Point(160, 28);
            this.gpbOpenEye.Name = "gpbOpenEye";
            this.gpbOpenEye.Size = new System.Drawing.Size(200, 60);
            this.gpbOpenEye.TabIndex = 409;
            this.gpbOpenEye.TabStop = false;
            this.gpbOpenEye.Text = "对疼痛或语言能否自动睁眼";
            // 
            // rdbCannotOpenEyes
            // 
            this.rdbCannotOpenEyes.Location = new System.Drawing.Point(68, 32);
            this.rdbCannotOpenEyes.Name = "rdbCannotOpenEyes";
            this.rdbCannotOpenEyes.Size = new System.Drawing.Size(64, 24);
            this.rdbCannotOpenEyes.TabIndex = 420;
            this.rdbCannotOpenEyes.TabStop = true;
            this.rdbCannotOpenEyes.Text = "不能";
            this.rdbCannotOpenEyes.Click += new System.EventHandler(this.OpenEyeChanged);
            // 
            // rdbCanOpenEyes
            // 
            this.rdbCanOpenEyes.Checked = true;
            this.rdbCanOpenEyes.Location = new System.Drawing.Point(8, 32);
            this.rdbCanOpenEyes.Name = "rdbCanOpenEyes";
            this.rdbCanOpenEyes.Size = new System.Drawing.Size(55, 24);
            this.rdbCanOpenEyes.TabIndex = 410;
            this.rdbCanOpenEyes.TabStop = true;
            this.rdbCanOpenEyes.Text = "能";
            this.rdbCanOpenEyes.Click += new System.EventHandler(this.OpenEyeChanged);
            // 
            // gpbAcheAndLanguage1
            // 
            this.gpbAcheAndLanguage1.Controls.Add(this.chkBrainUnreaction);
            this.gpbAcheAndLanguage1.Controls.Add(this.chkBodyBendAndVertical);
            this.gpbAcheAndLanguage1.Controls.Add(this.chkPositionAche);
            this.gpbAcheAndLanguage1.Controls.Add(this.chkAccording);
            this.gpbAcheAndLanguage1.Controls.Add(this.lblSportChanged);
            this.gpbAcheAndLanguage1.Controls.Add(this.rdbUnreaction);
            this.gpbAcheAndLanguage1.Controls.Add(this.rdbBlur);
            this.gpbAcheAndLanguage1.Controls.Add(this.rdbConfusion);
            this.gpbAcheAndLanguage1.Controls.Add(this.rdbRight);
            this.gpbAcheAndLanguage1.Controls.Add(this.lblLanguage);
            this.gpbAcheAndLanguage1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbAcheAndLanguage1.Location = new System.Drawing.Point(12, 96);
            this.gpbAcheAndLanguage1.Name = "gpbAcheAndLanguage1";
            this.gpbAcheAndLanguage1.Size = new System.Drawing.Size(348, 180);
            this.gpbAcheAndLanguage1.TabIndex = 429;
            this.gpbAcheAndLanguage1.TabStop = false;
            this.gpbAcheAndLanguage1.Text = "对疼痛或语言刺激时的语言及运动变化";
            // 
            // chkBrainUnreaction
            // 
            this.chkBrainUnreaction.Location = new System.Drawing.Point(144, 140);
            this.chkBrainUnreaction.Name = "chkBrainUnreaction";
            this.chkBrainUnreaction.Size = new System.Drawing.Size(156, 32);
            this.chkBrainUnreaction.TabIndex = 500;
            this.chkBrainUnreaction.Text = "去大脑强直或无反应";
            // 
            // chkBodyBendAndVertical
            // 
            this.chkBodyBendAndVertical.Location = new System.Drawing.Point(144, 112);
            this.chkBodyBendAndVertical.Name = "chkBodyBendAndVertical";
            this.chkBodyBendAndVertical.Size = new System.Drawing.Size(172, 40);
            this.chkBodyBendAndVertical.TabIndex = 490;
            this.chkBodyBendAndVertical.Text = "肢体屈升或去皮层强直";
            // 
            // chkPositionAche
            // 
            this.chkPositionAche.Location = new System.Drawing.Point(144, 88);
            this.chkPositionAche.Name = "chkPositionAche";
            this.chkPositionAche.Size = new System.Drawing.Size(119, 24);
            this.chkPositionAche.TabIndex = 480;
            this.chkPositionAche.Text = "疼痛定位";
            // 
            // chkAccording
            // 
            this.chkAccording.Location = new System.Drawing.Point(144, 60);
            this.chkAccording.Name = "chkAccording";
            this.chkAccording.Size = new System.Drawing.Size(119, 24);
            this.chkAccording.TabIndex = 470;
            this.chkAccording.Text = "按嘱运动";
            // 
            // lblSportChanged
            // 
            this.lblSportChanged.Location = new System.Drawing.Point(144, 28);
            this.lblSportChanged.Name = "lblSportChanged";
            this.lblSportChanged.Size = new System.Drawing.Size(114, 24);
            this.lblSportChanged.TabIndex = 2;
            this.lblSportChanged.Text = "运动变化：";
            // 
            // rdbUnreaction
            // 
            this.rdbUnreaction.Location = new System.Drawing.Point(12, 140);
            this.rdbUnreaction.Name = "rdbUnreaction";
            this.rdbUnreaction.Size = new System.Drawing.Size(119, 24);
            this.rdbUnreaction.TabIndex = 460;
            this.rdbUnreaction.Tag = "3";
            this.rdbUnreaction.Text = "无反应";
            this.rdbUnreaction.Click += new System.EventHandler(this.LanguageChanged);
            // 
            // rdbBlur
            // 
            this.rdbBlur.Location = new System.Drawing.Point(12, 108);
            this.rdbBlur.Name = "rdbBlur";
            this.rdbBlur.Size = new System.Drawing.Size(174, 28);
            this.rdbBlur.TabIndex = 450;
            this.rdbBlur.Tag = "2";
            this.rdbBlur.Text = "语句或发音不清";
            this.rdbBlur.Click += new System.EventHandler(this.LanguageChanged);
            // 
            // rdbConfusion
            // 
            this.rdbConfusion.Location = new System.Drawing.Point(12, 80);
            this.rdbConfusion.Name = "rdbConfusion";
            this.rdbConfusion.Size = new System.Drawing.Size(119, 24);
            this.rdbConfusion.TabIndex = 440;
            this.rdbConfusion.Tag = "1";
            this.rdbConfusion.Text = "回答混乱";
            this.rdbConfusion.Click += new System.EventHandler(this.LanguageChanged);
            // 
            // rdbRight
            // 
            this.rdbRight.Checked = true;
            this.rdbRight.Location = new System.Drawing.Point(12, 48);
            this.rdbRight.Name = "rdbRight";
            this.rdbRight.Size = new System.Drawing.Size(119, 24);
            this.rdbRight.TabIndex = 430;
            this.rdbRight.TabStop = true;
            this.rdbRight.Tag = "0";
            this.rdbRight.Text = "回答正确";
            this.rdbRight.Click += new System.EventHandler(this.LanguageChanged);
            // 
            // lblLanguage
            // 
            this.lblLanguage.Location = new System.Drawing.Point(12, 20);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(64, 20);
            this.lblLanguage.TabIndex = 0;
            this.lblLanguage.Text = "语言";
            // 
            // cmdStartAuto
            // 
            this.cmdStartAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdStartAuto.DefaultScheme = true;
            this.cmdStartAuto.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdStartAuto.ForeColor = System.Drawing.Color.Black;
            this.cmdStartAuto.Hint = "";
            this.cmdStartAuto.Location = new System.Drawing.Point(536, 16);
            this.cmdStartAuto.Name = "cmdStartAuto";
            this.cmdStartAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStartAuto.Size = new System.Drawing.Size(96, 32);
            this.cmdStartAuto.TabIndex = 10000017;
            this.cmdStartAuto.Text = "自动评分(&A)";
            this.cmdStartAuto.Click += new System.EventHandler(this.cmdStartAuto_Click);
            // 
            // txtAutoTime
            // 
            this.txtAutoTime.BackColor = System.Drawing.Color.White;
            this.txtAutoTime.BorderColor = System.Drawing.Color.Black;
            this.txtAutoTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAutoTime.ForeColor = System.Drawing.Color.Black;
            this.txtAutoTime.Location = new System.Drawing.Point(396, 21);
            this.txtAutoTime.MaxLength = 10;
            this.txtAutoTime.Name = "txtAutoTime";
            this.txtAutoTime.Size = new System.Drawing.Size(40, 23);
            this.txtAutoTime.TabIndex = 432;
            this.txtAutoTime.Text = "60";
            this.txtAutoTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dtpStartSample
            // 
            this.dtpStartSample.BorderColor = System.Drawing.Color.Black;
            this.dtpStartSample.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.dtpStartSample.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.dtpStartSample.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.dtpStartSample.DropButtonForeColor = System.Drawing.Color.Black;
            this.dtpStartSample.flatFont = new System.Drawing.Font("宋体", 12F);
            this.dtpStartSample.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpStartSample.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartSample.Location = new System.Drawing.Point(76, 21);
            this.dtpStartSample.m_BlnOnlyTime = false;
            this.dtpStartSample.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpStartSample.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartSample.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartSample.Name = "dtpStartSample";
            this.dtpStartSample.ReadOnly = false;
            this.dtpStartSample.Size = new System.Drawing.Size(220, 22);
            this.dtpStartSample.TabIndex = 444;
            this.dtpStartSample.TextBackColor = System.Drawing.Color.White;
            this.dtpStartSample.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblTitle96
            // 
            this.lblTitle96.AutoSize = true;
            this.lblTitle96.Location = new System.Drawing.Point(4, 23);
            this.lblTitle96.Name = "lblTitle96";
            this.lblTitle96.Size = new System.Drawing.Size(82, 14);
            this.lblTitle96.TabIndex = 438;
            this.lblTitle96.Text = "采集时间：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(296, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 14);
            this.label4.TabIndex = 433;
            this.label4.Text = "评分间隔(秒)：";
            // 
            // cmdStopAuto
            // 
            this.cmdStopAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdStopAuto.DefaultScheme = true;
            this.cmdStopAuto.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdStopAuto.ForeColor = System.Drawing.Color.Black;
            this.cmdStopAuto.Hint = "";
            this.cmdStopAuto.Location = new System.Drawing.Point(636, 16);
            this.cmdStopAuto.Name = "cmdStopAuto";
            this.cmdStopAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStopAuto.Size = new System.Drawing.Size(96, 32);
            this.cmdStopAuto.TabIndex = 10000017;
            this.cmdStopAuto.Text = "停止评分(&S)";
            this.cmdStopAuto.Click += new System.EventHandler(this.cmdStopAuto_Click);
            // 
            // cmdGetData
            // 
            this.cmdGetData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdGetData.DefaultScheme = true;
            this.cmdGetData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdGetData.ForeColor = System.Drawing.Color.Black;
            this.cmdGetData.Hint = "";
            this.cmdGetData.Location = new System.Drawing.Point(440, 16);
            this.cmdGetData.Name = "cmdGetData";
            this.cmdGetData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdGetData.Size = new System.Drawing.Size(96, 32);
            this.cmdGetData.TabIndex = 10000017;
            this.cmdGetData.Text = "获取数据(&G)";
            this.cmdGetData.Click += new System.EventHandler(this.cmdGetData_Click);
            // 
            // cmdShowResult
            // 
            this.cmdShowResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdShowResult.DefaultScheme = true;
            this.cmdShowResult.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdShowResult.ForeColor = System.Drawing.Color.Black;
            this.cmdShowResult.Hint = "";
            this.cmdShowResult.Location = new System.Drawing.Point(732, 16);
            this.cmdShowResult.Name = "cmdShowResult";
            this.cmdShowResult.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdShowResult.Size = new System.Drawing.Size(96, 32);
            this.cmdShowResult.TabIndex = 10000017;
            this.cmdShowResult.Text = "查看结果(&R)";
            this.cmdShowResult.Click += new System.EventHandler(this.cmdShowResult_Click);
            // 
            // dtgResult
            // 
            this.dtgResult.BackgroundColor = System.Drawing.Color.White;
            this.dtgResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtgResult.CaptionBackColor = System.Drawing.Color.White;
            this.dtgResult.CaptionForeColor = System.Drawing.Color.Black;
            this.dtgResult.CaptionText = "APACHEIII评分法（草案）结果";
            this.dtgResult.DataMember = "";
            this.dtgResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtgResult.ForeColor = System.Drawing.Color.White;
            this.dtgResult.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dtgResult.Location = new System.Drawing.Point(4, 472);
            this.dtgResult.Name = "dtgResult";
            this.dtgResult.PreferredColumnWidth = 90;
            this.dtgResult.ReadOnly = true;
            this.dtgResult.RowHeadersVisible = false;
            this.dtgResult.RowHeaderWidth = 0;
            this.dtgResult.Size = new System.Drawing.Size(835, 107);
            this.dtgResult.TabIndex = 401;
            this.dtgResult.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.dtgResult;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn5,
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "result";
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "  总   分";
            this.dataGridTextBoxColumn5.MappingName = "总分";
            this.dataGridTextBoxColumn5.ReadOnly = true;
            this.dataGridTextBoxColumn5.Width = 170;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "  ASP得分";
            this.dataGridTextBoxColumn1.MappingName = "ASP得分";
            this.dataGridTextBoxColumn1.ReadOnly = true;
            this.dataGridTextBoxColumn1.Width = 170;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "   酸碱失衡得分";
            this.dataGridTextBoxColumn2.MappingName = "酸碱失衡得分";
            this.dataGridTextBoxColumn2.ReadOnly = true;
            this.dataGridTextBoxColumn2.Width = 170;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "  年龄和既往健康得分";
            this.dataGridTextBoxColumn3.MappingName = "年龄和既往健康得分";
            this.dataGridTextBoxColumn3.ReadOnly = true;
            this.dataGridTextBoxColumn3.Width = 172;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "  神经功能异常得分";
            this.dataGridTextBoxColumn4.MappingName = "神经功能异常得分";
            this.dataGridTextBoxColumn4.ReadOnly = true;
            this.dataGridTextBoxColumn4.Width = 172;
            // 
            // dtpEvalDate
            // 
            this.dtpEvalDate.BorderColor = System.Drawing.Color.Black;
            this.dtpEvalDate.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.dtpEvalDate.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.dtpEvalDate.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.dtpEvalDate.DropButtonForeColor = System.Drawing.Color.Black;
            this.dtpEvalDate.flatFont = new System.Drawing.Font("宋体", 12F);
            this.dtpEvalDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpEvalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEvalDate.Location = new System.Drawing.Point(80, 440);
            this.dtpEvalDate.m_BlnOnlyTime = false;
            this.dtpEvalDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpEvalDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEvalDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEvalDate.Name = "dtpEvalDate";
            this.dtpEvalDate.ReadOnly = false;
            this.dtpEvalDate.Size = new System.Drawing.Size(220, 22);
            this.dtpEvalDate.TabIndex = 510;
            this.dtpEvalDate.TextBackColor = System.Drawing.Color.White;
            this.dtpEvalDate.TextForeColor = System.Drawing.Color.Black;
            this.dtpEvalDate.Load += new System.EventHandler(this.dtpEvalDate_Load);
            // 
            // txtEvalDoctor
            // 
            this.txtEvalDoctor.AccessibleDescription = "";
            this.txtEvalDoctor.BackColor = System.Drawing.Color.White;
            this.txtEvalDoctor.BorderColor = System.Drawing.Color.Black;
            this.txtEvalDoctor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEvalDoctor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEvalDoctor.ForeColor = System.Drawing.Color.Black;
            this.txtEvalDoctor.Location = new System.Drawing.Point(456, 438);
            this.txtEvalDoctor.MaxLength = 8;
            this.txtEvalDoctor.Name = "txtEvalDoctor";
            this.txtEvalDoctor.Size = new System.Drawing.Size(106, 23);
            this.txtEvalDoctor.TabIndex = 520;
            // 
            // timAutoCollect
            // 
            this.timAutoCollect.Interval = 60000;
            this.timAutoCollect.SynchronizingObject = this;
            this.timAutoCollect.Elapsed += new System.Timers.ElapsedEventHandler(this.timAutoCollect_Elapsed);
            // 
            // m_cmdEvalDoctor
            // 
            this.m_cmdEvalDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdEvalDoctor.DefaultScheme = true;
            this.m_cmdEvalDoctor.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdEvalDoctor.ForeColor = System.Drawing.Color.Black;
            this.m_cmdEvalDoctor.Hint = "";
            this.m_cmdEvalDoctor.Location = new System.Drawing.Point(364, 434);
            this.m_cmdEvalDoctor.Name = "m_cmdEvalDoctor";
            this.m_cmdEvalDoctor.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdEvalDoctor.Size = new System.Drawing.Size(87, 32);
            this.m_cmdEvalDoctor.TabIndex = 10000018;
            this.m_cmdEvalDoctor.Text = "评估者";
            // 
            // cmdCalculate
            // 
            this.cmdCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdCalculate.DefaultScheme = true;
            this.cmdCalculate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdCalculate.ForeColor = System.Drawing.Color.Black;
            this.cmdCalculate.Hint = "";
            this.cmdCalculate.Location = new System.Drawing.Point(648, 438);
            this.cmdCalculate.Name = "cmdCalculate";
            this.cmdCalculate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCalculate.Size = new System.Drawing.Size(152, 28);
            this.cmdCalculate.TabIndex = 10000018;
            this.cmdCalculate.Text = "计算分值";
            this.cmdCalculate.Click += new System.EventHandler(this.cmdCalculate_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dtpStartSample);
            this.groupBox5.Controls.Add(this.lblTitle96);
            this.groupBox5.Controls.Add(this.cmdGetData);
            this.groupBox5.Controls.Add(this.cmdStartAuto);
            this.groupBox5.Controls.Add(this.txtAutoTime);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.cmdStopAuto);
            this.groupBox5.Controls.Add(this.cmdShowResult);
            this.groupBox5.Location = new System.Drawing.Point(4, 579);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(836, 56);
            this.groupBox5.TabIndex = 10000020;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "自动评分";
            // 
            // frmAPACHEIIIValuation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.ClientSize = new System.Drawing.Size(871, 701);
            this.Controls.Add(this.dtpEvalDate);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.cmdCalculate);
            this.Controls.Add(this.lblEvalDate);
            this.Controls.Add(this.txtEvalDoctor);
            this.Controls.Add(this.dtgResult);
            this.Controls.Add(this.tabAPACHEIIValuation);
            this.Controls.Add(this.m_cmdEvalDoctor);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAPACHEIIIValuation";
            this.Text = "APACHEIII评分";
            this.Load += new System.EventHandler(this.frmAPACHEIIIValuation_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmAPACHEIIIValuation_Closing);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_cmdEvalDoctor, 0);
            this.Controls.SetChildIndex(this.tabAPACHEIIValuation, 0);
            this.Controls.SetChildIndex(this.dtgResult, 0);
            this.Controls.SetChildIndex(this.txtEvalDoctor, 0);
            this.Controls.SetChildIndex(this.lblEvalDate, 0);
            this.Controls.SetChildIndex(this.cmdCalculate, 0);
            this.Controls.SetChildIndex(this.groupBox5, 0);
            this.Controls.SetChildIndex(this.dtpEvalDate, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.tabAPACHEIIValuation.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.gpbExactLife.ResumeLayout(false);
            this.gpbExactLife.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.gpbAge.ResumeLayout(false);
            this.gpbHealth.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.gpbOperaSel.ResumeLayout(false);
            this.gpbOpenEye.ResumeLayout(false);
            this.gpbAcheAndLanguage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        public void ReceiveID(string strID)
        {
            if (strID.Trim().Length != 0)
            {
                m_mthDisplay();
            }
        }
    }
}
