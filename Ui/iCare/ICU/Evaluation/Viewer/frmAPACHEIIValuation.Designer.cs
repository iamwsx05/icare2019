using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iCare.ICU.Evaluation
{
    partial class frmAPACHEIIValuation
    {
        #region Define
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabAPACHEIIValuation;
        private System.Windows.Forms.GroupBox gpbExactLife;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBreath;
        private System.Windows.Forms.CheckBox chkKidneyProstrate;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtFiO2;
        private System.Windows.Forms.Label lblFiO2;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtAmountLeucocyte;
        private System.Windows.Forms.Label lblAmountLeucocyte;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtTemperature;
        private System.Windows.Forms.Label lblTitle10;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtAdvArteryPress;
        private System.Windows.Forms.Label lblTitle11;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtHR;
        private System.Windows.Forms.Label lblTitle12;
        private System.Windows.Forms.Label lblTitle14;
        private System.Windows.Forms.Label lblTitle20;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtDo2;
        private System.Windows.Forms.Label lblTitle13;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBloodCorpuscle;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtPao2;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBloodNa;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBloodFlesh;
        private System.Windows.Forms.Label lblTitle16;
        private System.Windows.Forms.Label lblTitle17;
        private System.Windows.Forms.Label lblTitle19;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtBloodK;
        private System.Windows.Forms.Label lblTitle18;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtArteryBloodpH;
        private System.Windows.Forms.Label lblTitle21;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtHCO;
        private System.Windows.Forms.GroupBox gpbAge;
        private System.Windows.Forms.RadioButton rdbAgeO75;
        private System.Windows.Forms.RadioButton rdbAgeU74;
        private System.Windows.Forms.RadioButton rdbAgeU64;
        private System.Windows.Forms.RadioButton rdbAgeU54;
        private System.Windows.Forms.RadioButton rdbAgeU44;
        private System.Windows.Forms.DataGrid dtgResult;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtEvalDoctor;
        public com.digitalwave.Utility.Controls.ctlTimePicker dtpEvalDate;
        private double dblTemperature;
        private double dblAdvArteryPress;
        private double dblHR;
        private double dblAmountLeucocyte;
        private double dblBreath;
        private double dblPao2;
        private double dblDo2;
        private double dblFiO2;
        private double dblArteryBloodpH;
        private double dblBloodNa;
        private double dblBloodK;
        private double dblGCS;
        private double dblBloodFlesh;
        private double dblBloodCorpuscle;
        private double dblHCO;
        /// <summary>
        /// Required designer variable.
        /// </summary>        
        private System.Windows.Forms.Label lblEvalDate;
        private System.ComponentModel.IContainer components = null;        
        public string strSickBedNO;
        APACHEIIValuationDomain objDomain = new APACHEIIValuationDomain();
        private com.digitalwave.Utility.Controls.ctlTimePicker dtpStartSample;
        private System.Windows.Forms.Label lblTitle96;
        private System.Windows.Forms.Label label2;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtAutoTime;
        private System.Timers.Timer timAutoCollect;
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
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn12;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn13;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn14;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn15;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn16;
        private com.digitalwave.Utility.Controls.clsBorderTool m_objBorderTool;
        private PinkieControls.ButtonXP m_cmdGetDovueData;
        private System.Windows.Forms.GroupBox m_gpbNerve;
        private System.Windows.Forms.Label m_lblOpenEyes;
        protected com.digitalwave.Utility.Controls.ctlComboBox m_cboOpenEyes;
        private System.Windows.Forms.Label m_lblSay;
        protected com.digitalwave.Utility.Controls.ctlComboBox m_cboSay;
        private System.Windows.Forms.Label m_lblSport;
        protected com.digitalwave.Utility.Controls.ctlComboBox m_cboSport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label m_lblSmall2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtPaCO2;
        private PinkieControls.ButtonXP cmdCalculate;
        private PinkieControls.ButtonXP m_cmdEvalDoctor;
        private PinkieControls.ButtonXP m_cmdToAaDO2;
        private PinkieControls.ButtonXP cmdStartAuto;
        private PinkieControls.ButtonXP cmdStopAuto;
        private PinkieControls.ButtonXP cmdShowResult;
        private PinkieControls.ButtonXP cmdGetData;
        private PinkieControls.ButtonXP m_cmdGetCheckData;
        private System.Windows.Forms.CheckBox chkHurts;
        private com.digitalwave.Utility.Controls.ctlComboBox cboMainReasonNoIn;
        private System.Windows.Forms.CheckBox chkMainReasonNoIn;
        private com.digitalwave.Utility.Controls.ctlComboBox cboNoInRange;
        private System.Windows.Forms.CheckBox chkNoInRange;
        private com.digitalwave.Utility.Controls.ctlComboBox cboOthers;
        private System.Windows.Forms.CheckBox chkOthers;
        private com.digitalwave.Utility.Controls.ctlComboBox cboNeurotic;
        private System.Windows.Forms.CheckBox chkNeurotic;
        private com.digitalwave.Utility.Controls.ctlComboBox cboHurts;
        private System.Windows.Forms.CheckBox chkPatientUnOp2;
        private com.digitalwave.Utility.Controls.ctlComboBox cboPatientUnOp2;
        private com.digitalwave.Utility.Controls.ctlComboBox cboPatientUnOp1;
        private System.Windows.Forms.CheckBox chkPatientAfterEmergency;
        private com.digitalwave.Utility.Controls.ctlComboBox cboPatientSelOp;
        private System.Windows.Forms.RadioButton rdbPatientSelOp;
        private System.Windows.Forms.RadioButton rdbPatientUnOp;
        private System.Windows.Forms.CheckBox chkPatientUnOp1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        //private clsCommonUseToolCollection m_objCUTC;

        

        //		private clsPatient objPatientInfo_Base;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAPACHEIIValuation));
            this.tabAPACHEIIValuation = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gpbAge = new System.Windows.Forms.GroupBox();
            this.rdbAgeO75 = new System.Windows.Forms.RadioButton();
            this.rdbAgeU74 = new System.Windows.Forms.RadioButton();
            this.rdbAgeU64 = new System.Windows.Forms.RadioButton();
            this.rdbAgeU54 = new System.Windows.Forms.RadioButton();
            this.rdbAgeU44 = new System.Windows.Forms.RadioButton();
            this.gpbExactLife = new System.Windows.Forms.GroupBox();
            this.m_lblSmall2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtHCO = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.chkKidneyProstrate = new System.Windows.Forms.CheckBox();
            this.lblTitle21 = new System.Windows.Forms.Label();
            this.lblTitle10 = new System.Windows.Forms.Label();
            this.lblTitle11 = new System.Windows.Forms.Label();
            this.lblTitle12 = new System.Windows.Forms.Label();
            this.lblTitle13 = new System.Windows.Forms.Label();
            this.txtTemperature = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtAdvArteryPress = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtHR = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtBreath = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblAmountLeucocyte = new System.Windows.Forms.Label();
            this.txtAmountLeucocyte = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle20 = new System.Windows.Forms.Label();
            this.txtBloodCorpuscle = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtBloodNa = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtArteryBloodpH = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtBloodFlesh = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle16 = new System.Windows.Forms.Label();
            this.lblTitle17 = new System.Windows.Forms.Label();
            this.lblTitle19 = new System.Windows.Forms.Label();
            this.txtBloodK = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle18 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPao2 = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_cmdToAaDO2 = new PinkieControls.ButtonXP();
            this.txtPaCO2 = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtFiO2 = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblFiO2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTitle14 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDo2 = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_gpbNerve = new System.Windows.Forms.GroupBox();
            this.m_lblOpenEyes = new System.Windows.Forms.Label();
            this.m_cboOpenEyes = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_lblSay = new System.Windows.Forms.Label();
            this.m_cboSay = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_lblSport = new System.Windows.Forms.Label();
            this.m_cboSport = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_cmdGetCheckData = new PinkieControls.ButtonXP();
            this.m_cmdGetDovueData = new PinkieControls.ButtonXP();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rdbPatientSelOp = new System.Windows.Forms.RadioButton();
            this.rdbPatientUnOp = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.chkHurts = new System.Windows.Forms.CheckBox();
            this.cboMainReasonNoIn = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.chkMainReasonNoIn = new System.Windows.Forms.CheckBox();
            this.cboNoInRange = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.chkNoInRange = new System.Windows.Forms.CheckBox();
            this.cboOthers = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.chkOthers = new System.Windows.Forms.CheckBox();
            this.cboNeurotic = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.chkNeurotic = new System.Windows.Forms.CheckBox();
            this.cboHurts = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.chkPatientUnOp2 = new System.Windows.Forms.CheckBox();
            this.cboPatientUnOp2 = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.cboPatientUnOp1 = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.chkPatientAfterEmergency = new System.Windows.Forms.CheckBox();
            this.cboPatientSelOp = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.chkPatientUnOp1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdGetData = new PinkieControls.ButtonXP();
            this.cmdStartAuto = new PinkieControls.ButtonXP();
            this.cmdStopAuto = new PinkieControls.ButtonXP();
            this.cmdShowResult = new PinkieControls.ButtonXP();
            this.txtAutoTime = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.dtpStartSample = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblTitle96 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgResult = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn16 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn7 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn8 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn9 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn10 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn11 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn12 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn13 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn14 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn15 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.txtEvalDoctor = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.dtpEvalDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblEvalDate = new System.Windows.Forms.Label();
            this.timAutoCollect = new System.Timers.Timer();
            this.cmdCalculate = new PinkieControls.ButtonXP();
            this.m_cmdEvalDoctor = new PinkieControls.ButtonXP();
            this.m_pnlNewBase.SuspendLayout();
            this.tabAPACHEIIValuation.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gpbAge.SuspendLayout();
            this.gpbExactLife.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.m_gpbNerve.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).BeginInit();
            this.SuspendLayout();
            // 
            // trvActivityTime
            // 
            this.trvActivityTime.LineColor = System.Drawing.Color.Black;
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Size = new System.Drawing.Size(844, 89);
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowHomePlace = true;
            this.m_ctlPatientInfo.m_BlnIsShowMarriage = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(652, 57);
            // 
            // tabAPACHEIIValuation
            // 
            this.tabAPACHEIIValuation.Controls.Add(this.tabPage1);
            this.tabAPACHEIIValuation.Controls.Add(this.tabPage2);
            this.tabAPACHEIIValuation.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabAPACHEIIValuation.Location = new System.Drawing.Point(8, 98);
            this.tabAPACHEIIValuation.Name = "tabAPACHEIIValuation";
            this.tabAPACHEIIValuation.SelectedIndex = 0;
            this.tabAPACHEIIValuation.Size = new System.Drawing.Size(840, 324);
            this.tabAPACHEIIValuation.TabIndex = 41;
            this.tabAPACHEIIValuation.CursorChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.gpbAge);
            this.tabPage1.Controls.Add(this.gpbExactLife);
            this.tabPage1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(832, 297);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "APS值和年龄";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // gpbAge
            // 
            this.gpbAge.Controls.Add(this.rdbAgeO75);
            this.gpbAge.Controls.Add(this.rdbAgeU74);
            this.gpbAge.Controls.Add(this.rdbAgeU64);
            this.gpbAge.Controls.Add(this.rdbAgeU54);
            this.gpbAge.Controls.Add(this.rdbAgeU44);
            this.gpbAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbAge.Location = new System.Drawing.Point(728, 4);
            this.gpbAge.Name = "gpbAge";
            this.gpbAge.Size = new System.Drawing.Size(104, 288);
            this.gpbAge.TabIndex = 250;
            this.gpbAge.TabStop = false;
            this.gpbAge.Text = "年龄";
            // 
            // rdbAgeO75
            // 
            this.rdbAgeO75.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAgeO75.Location = new System.Drawing.Point(8, 200);
            this.rdbAgeO75.Name = "rdbAgeO75";
            this.rdbAgeO75.Size = new System.Drawing.Size(88, 24);
            this.rdbAgeO75.TabIndex = 300;
            this.rdbAgeO75.Tag = "4";
            this.rdbAgeO75.Text = "75岁以上";
            this.rdbAgeO75.Click += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbAgeU74
            // 
            this.rdbAgeU74.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAgeU74.Location = new System.Drawing.Point(8, 164);
            this.rdbAgeU74.Name = "rdbAgeU74";
            this.rdbAgeU74.Size = new System.Drawing.Size(79, 24);
            this.rdbAgeU74.TabIndex = 290;
            this.rdbAgeU74.Tag = "3";
            this.rdbAgeU74.Text = "65~74岁";
            this.rdbAgeU74.Click += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbAgeU64
            // 
            this.rdbAgeU64.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAgeU64.Location = new System.Drawing.Point(8, 120);
            this.rdbAgeU64.Name = "rdbAgeU64";
            this.rdbAgeU64.Size = new System.Drawing.Size(79, 24);
            this.rdbAgeU64.TabIndex = 280;
            this.rdbAgeU64.Tag = "2";
            this.rdbAgeU64.Text = "55~64岁";
            this.rdbAgeU64.Click += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbAgeU54
            // 
            this.rdbAgeU54.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAgeU54.Location = new System.Drawing.Point(8, 80);
            this.rdbAgeU54.Name = "rdbAgeU54";
            this.rdbAgeU54.Size = new System.Drawing.Size(79, 24);
            this.rdbAgeU54.TabIndex = 270;
            this.rdbAgeU54.Tag = "1";
            this.rdbAgeU54.Text = "45~54岁";
            this.rdbAgeU54.Click += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbAgeU44
            // 
            this.rdbAgeU44.Checked = true;
            this.rdbAgeU44.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAgeU44.Location = new System.Drawing.Point(8, 40);
            this.rdbAgeU44.Name = "rdbAgeU44";
            this.rdbAgeU44.Size = new System.Drawing.Size(88, 24);
            this.rdbAgeU44.TabIndex = 260;
            this.rdbAgeU44.TabStop = true;
            this.rdbAgeU44.Tag = "0";
            this.rdbAgeU44.Text = "小于44岁";
            this.rdbAgeU44.Click += new System.EventHandler(this.AgeGroupChanged);
            // 
            // gpbExactLife
            // 
            this.gpbExactLife.Controls.Add(this.m_lblSmall2);
            this.gpbExactLife.Controls.Add(this.groupBox3);
            this.gpbExactLife.Controls.Add(this.groupBox2);
            this.gpbExactLife.Controls.Add(this.groupBox1);
            this.gpbExactLife.Controls.Add(this.m_gpbNerve);
            this.gpbExactLife.Controls.Add(this.m_cmdGetCheckData);
            this.gpbExactLife.Controls.Add(this.m_cmdGetDovueData);
            this.gpbExactLife.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbExactLife.Location = new System.Drawing.Point(4, 3);
            this.gpbExactLife.Name = "gpbExactLife";
            this.gpbExactLife.Size = new System.Drawing.Size(720, 289);
            this.gpbExactLife.TabIndex = 49;
            this.gpbExactLife.TabStop = false;
            this.gpbExactLife.Text = "APS";
            this.gpbExactLife.Enter += new System.EventHandler(this.gpbExactLife_Enter);
            // 
            // m_lblSmall2
            // 
            this.m_lblSmall2.AutoSize = true;
            this.m_lblSmall2.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblSmall2.Location = new System.Drawing.Point(296, 12);
            this.m_lblSmall2.Name = "m_lblSmall2";
            this.m_lblSmall2.Size = new System.Drawing.Size(11, 9);
            this.m_lblSmall2.TabIndex = 193;
            this.m_lblSmall2.Text = "2";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtHCO);
            this.groupBox3.Controls.Add(this.chkKidneyProstrate);
            this.groupBox3.Controls.Add(this.lblTitle21);
            this.groupBox3.Controls.Add(this.lblTitle10);
            this.groupBox3.Controls.Add(this.lblTitle11);
            this.groupBox3.Controls.Add(this.lblTitle12);
            this.groupBox3.Controls.Add(this.lblTitle13);
            this.groupBox3.Controls.Add(this.txtTemperature);
            this.groupBox3.Controls.Add(this.txtAdvArteryPress);
            this.groupBox3.Controls.Add(this.txtHR);
            this.groupBox3.Controls.Add(this.txtBreath);
            this.groupBox3.Location = new System.Drawing.Point(4, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(212, 232);
            this.groupBox3.TabIndex = 217;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "监护数据";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // txtHCO
            // 
            this.txtHCO.BackColor = System.Drawing.Color.White;
            this.txtHCO.BorderColor = System.Drawing.Color.Black;
            this.txtHCO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHCO.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHCO.ForeColor = System.Drawing.Color.Black;
            this.txtHCO.Location = new System.Drawing.Point(132, 160);
            this.txtHCO.Name = "txtHCO";
            this.txtHCO.Size = new System.Drawing.Size(68, 26);
            this.txtHCO.TabIndex = 140;
            // 
            // chkKidneyProstrate
            // 
            this.chkKidneyProstrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkKidneyProstrate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkKidneyProstrate.Location = new System.Drawing.Point(8, 204);
            this.chkKidneyProstrate.Name = "chkKidneyProstrate";
            this.chkKidneyProstrate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkKidneyProstrate.Size = new System.Drawing.Size(136, 21);
            this.chkKidneyProstrate.TabIndex = 150;
            this.chkKidneyProstrate.Text = " 急性肾功能衰竭";
            // 
            // lblTitle21
            // 
            this.lblTitle21.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle21.Location = new System.Drawing.Point(4, 148);
            this.lblTitle21.Name = "lblTitle21";
            this.lblTitle21.Size = new System.Drawing.Size(128, 52);
            this.lblTitle21.TabIndex = 0;
            this.lblTitle21.Text = "静脉血HCO(mmol/L 无动脉血气时):";
            this.lblTitle21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle10
            // 
            this.lblTitle10.AutoSize = true;
            this.lblTitle10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle10.Location = new System.Drawing.Point(4, 24);
            this.lblTitle10.Name = "lblTitle10";
            this.lblTitle10.Size = new System.Drawing.Size(138, 14);
            this.lblTitle10.TabIndex = 0;
            this.lblTitle10.Text = "直 肠 温 度 (℃):";
            this.lblTitle10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle11
            // 
            this.lblTitle11.AutoSize = true;
            this.lblTitle11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle11.Location = new System.Drawing.Point(4, 56);
            this.lblTitle11.Name = "lblTitle11";
            this.lblTitle11.Size = new System.Drawing.Size(138, 14);
            this.lblTitle11.TabIndex = 0;
            this.lblTitle11.Text = "平均动脉压(mmHg):";
            this.lblTitle11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle12
            // 
            this.lblTitle12.AutoSize = true;
            this.lblTitle12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle12.Location = new System.Drawing.Point(4, 88);
            this.lblTitle12.Name = "lblTitle12";
            this.lblTitle12.Size = new System.Drawing.Size(141, 14);
            this.lblTitle12.TabIndex = 0;
            this.lblTitle12.Text = "心      率(/min):";
            this.lblTitle12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle13
            // 
            this.lblTitle13.AutoSize = true;
            this.lblTitle13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle13.Location = new System.Drawing.Point(4, 128);
            this.lblTitle13.Name = "lblTitle13";
            this.lblTitle13.Size = new System.Drawing.Size(123, 14);
            this.lblTitle13.TabIndex = 0;
            this.lblTitle13.Text = "呼吸频率(/min):";
            this.lblTitle13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTemperature
            // 
            this.txtTemperature.BackColor = System.Drawing.Color.White;
            this.txtTemperature.BorderColor = System.Drawing.Color.Black;
            this.txtTemperature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTemperature.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTemperature.ForeColor = System.Drawing.Color.Black;
            this.txtTemperature.Location = new System.Drawing.Point(132, 20);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(68, 26);
            this.txtTemperature.TabIndex = 50;
            // 
            // txtAdvArteryPress
            // 
            this.txtAdvArteryPress.BackColor = System.Drawing.Color.White;
            this.txtAdvArteryPress.BorderColor = System.Drawing.Color.Black;
            this.txtAdvArteryPress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdvArteryPress.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAdvArteryPress.ForeColor = System.Drawing.Color.Black;
            this.txtAdvArteryPress.Location = new System.Drawing.Point(132, 56);
            this.txtAdvArteryPress.Name = "txtAdvArteryPress";
            this.txtAdvArteryPress.Size = new System.Drawing.Size(68, 26);
            this.txtAdvArteryPress.TabIndex = 80;
            // 
            // txtHR
            // 
            this.txtHR.BackColor = System.Drawing.Color.White;
            this.txtHR.BorderColor = System.Drawing.Color.Black;
            this.txtHR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHR.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHR.ForeColor = System.Drawing.Color.Black;
            this.txtHR.Location = new System.Drawing.Point(132, 88);
            this.txtHR.Name = "txtHR";
            this.txtHR.Size = new System.Drawing.Size(68, 26);
            this.txtHR.TabIndex = 110;
            // 
            // txtBreath
            // 
            this.txtBreath.BackColor = System.Drawing.Color.White;
            this.txtBreath.BorderColor = System.Drawing.Color.Black;
            this.txtBreath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBreath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBreath.ForeColor = System.Drawing.Color.Black;
            this.txtBreath.Location = new System.Drawing.Point(132, 124);
            this.txtBreath.Name = "txtBreath";
            this.txtBreath.Size = new System.Drawing.Size(68, 26);
            this.txtBreath.TabIndex = 60;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblAmountLeucocyte);
            this.groupBox2.Controls.Add(this.txtAmountLeucocyte);
            this.groupBox2.Controls.Add(this.lblTitle20);
            this.groupBox2.Controls.Add(this.txtBloodCorpuscle);
            this.groupBox2.Controls.Add(this.txtBloodNa);
            this.groupBox2.Controls.Add(this.txtArteryBloodpH);
            this.groupBox2.Controls.Add(this.txtBloodFlesh);
            this.groupBox2.Controls.Add(this.lblTitle16);
            this.groupBox2.Controls.Add(this.lblTitle17);
            this.groupBox2.Controls.Add(this.lblTitle19);
            this.groupBox2.Controls.Add(this.txtBloodK);
            this.groupBox2.Controls.Add(this.lblTitle18);
            this.groupBox2.Location = new System.Drawing.Point(483, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 224);
            this.groupBox2.TabIndex = 216;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "检验数据";
            // 
            // lblAmountLeucocyte
            // 
            this.lblAmountLeucocyte.AutoSize = true;
            this.lblAmountLeucocyte.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAmountLeucocyte.Location = new System.Drawing.Point(8, 60);
            this.lblAmountLeucocyte.Name = "lblAmountLeucocyte";
            this.lblAmountLeucocyte.Size = new System.Drawing.Size(162, 14);
            this.lblAmountLeucocyte.TabIndex = 2;
            this.lblAmountLeucocyte.Text = "白细胞计数(*10^9/L):";
            this.lblAmountLeucocyte.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAmountLeucocyte
            // 
            this.txtAmountLeucocyte.BackColor = System.Drawing.Color.White;
            this.txtAmountLeucocyte.BorderColor = System.Drawing.Color.Black;
            this.txtAmountLeucocyte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountLeucocyte.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAmountLeucocyte.ForeColor = System.Drawing.Color.Black;
            this.txtAmountLeucocyte.Location = new System.Drawing.Point(160, 56);
            this.txtAmountLeucocyte.Name = "txtAmountLeucocyte";
            this.txtAmountLeucocyte.Size = new System.Drawing.Size(64, 26);
            this.txtAmountLeucocyte.TabIndex = 120;
            // 
            // lblTitle20
            // 
            this.lblTitle20.AutoSize = true;
            this.lblTitle20.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle20.Location = new System.Drawing.Point(8, 188);
            this.lblTitle20.Name = "lblTitle20";
            this.lblTitle20.Size = new System.Drawing.Size(121, 14);
            this.lblTitle20.TabIndex = 0;
            this.lblTitle20.Text = "血细胞比容(％):";
            this.lblTitle20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBloodCorpuscle
            // 
            this.txtBloodCorpuscle.BackColor = System.Drawing.Color.White;
            this.txtBloodCorpuscle.BorderColor = System.Drawing.Color.Black;
            this.txtBloodCorpuscle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBloodCorpuscle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBloodCorpuscle.ForeColor = System.Drawing.Color.Black;
            this.txtBloodCorpuscle.Location = new System.Drawing.Point(160, 184);
            this.txtBloodCorpuscle.Name = "txtBloodCorpuscle";
            this.txtBloodCorpuscle.Size = new System.Drawing.Size(64, 26);
            this.txtBloodCorpuscle.TabIndex = 160;
            // 
            // txtBloodNa
            // 
            this.txtBloodNa.BackColor = System.Drawing.Color.White;
            this.txtBloodNa.BorderColor = System.Drawing.Color.Black;
            this.txtBloodNa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBloodNa.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBloodNa.ForeColor = System.Drawing.Color.Black;
            this.txtBloodNa.Location = new System.Drawing.Point(160, 120);
            this.txtBloodNa.Name = "txtBloodNa";
            this.txtBloodNa.Size = new System.Drawing.Size(64, 26);
            this.txtBloodNa.TabIndex = 100;
            // 
            // txtArteryBloodpH
            // 
            this.txtArteryBloodpH.BackColor = System.Drawing.Color.White;
            this.txtArteryBloodpH.BorderColor = System.Drawing.Color.Black;
            this.txtArteryBloodpH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtArteryBloodpH.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtArteryBloodpH.ForeColor = System.Drawing.Color.Black;
            this.txtArteryBloodpH.Location = new System.Drawing.Point(160, 88);
            this.txtArteryBloodpH.Name = "txtArteryBloodpH";
            this.txtArteryBloodpH.Size = new System.Drawing.Size(64, 26);
            this.txtArteryBloodpH.TabIndex = 70;
            // 
            // txtBloodFlesh
            // 
            this.txtBloodFlesh.BackColor = System.Drawing.Color.White;
            this.txtBloodFlesh.BorderColor = System.Drawing.Color.Black;
            this.txtBloodFlesh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBloodFlesh.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBloodFlesh.ForeColor = System.Drawing.Color.Black;
            this.txtBloodFlesh.Location = new System.Drawing.Point(160, 24);
            this.txtBloodFlesh.Name = "txtBloodFlesh";
            this.txtBloodFlesh.Size = new System.Drawing.Size(64, 26);
            this.txtBloodFlesh.TabIndex = 90;
            // 
            // lblTitle16
            // 
            this.lblTitle16.AutoSize = true;
            this.lblTitle16.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle16.Location = new System.Drawing.Point(8, 92);
            this.lblTitle16.Name = "lblTitle16";
            this.lblTitle16.Size = new System.Drawing.Size(83, 14);
            this.lblTitle16.TabIndex = 0;
            this.lblTitle16.Text = "动脉血pH：";
            this.lblTitle16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle17
            // 
            this.lblTitle17.AutoSize = true;
            this.lblTitle17.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle17.Location = new System.Drawing.Point(8, 124);
            this.lblTitle17.Name = "lblTitle17";
            this.lblTitle17.Size = new System.Drawing.Size(139, 14);
            this.lblTitle17.TabIndex = 0;
            this.lblTitle17.Text = "血钠浓度(mmol/L):";
            this.lblTitle17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle19
            // 
            this.lblTitle19.AutoSize = true;
            this.lblTitle19.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle19.Location = new System.Drawing.Point(8, 28);
            this.lblTitle19.Name = "lblTitle19";
            this.lblTitle19.Size = new System.Drawing.Size(154, 14);
            this.lblTitle19.TabIndex = 0;
            this.lblTitle19.Text = "血肌酐浓度(mmol/L):";
            this.lblTitle19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBloodK
            // 
            this.txtBloodK.BackColor = System.Drawing.Color.White;
            this.txtBloodK.BorderColor = System.Drawing.Color.Black;
            this.txtBloodK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBloodK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBloodK.ForeColor = System.Drawing.Color.Black;
            this.txtBloodK.Location = new System.Drawing.Point(160, 152);
            this.txtBloodK.Name = "txtBloodK";
            this.txtBloodK.Size = new System.Drawing.Size(64, 26);
            this.txtBloodK.TabIndex = 130;
            // 
            // lblTitle18
            // 
            this.lblTitle18.AutoSize = true;
            this.lblTitle18.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle18.Location = new System.Drawing.Point(8, 156);
            this.lblTitle18.Name = "lblTitle18";
            this.lblTitle18.Size = new System.Drawing.Size(139, 14);
            this.lblTitle18.TabIndex = 0;
            this.lblTitle18.Text = "血钾浓度(mmol/L):";
            this.lblTitle18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPao2);
            this.groupBox1.Controls.Add(this.m_cmdToAaDO2);
            this.groupBox1.Controls.Add(this.txtPaCO2);
            this.groupBox1.Controls.Add(this.txtFiO2);
            this.groupBox1.Controls.Add(this.lblFiO2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblTitle14);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtDo2);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(221, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 160);
            this.groupBox1.TabIndex = 165;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "(A－a)DO 肺泡动脉血氧分压差";
            // 
            // txtPao2
            // 
            this.txtPao2.BackColor = System.Drawing.Color.White;
            this.txtPao2.BorderColor = System.Drawing.Color.Black;
            this.txtPao2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPao2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPao2.ForeColor = System.Drawing.Color.Black;
            this.txtPao2.Location = new System.Drawing.Point(156, 88);
            this.txtPao2.Name = "txtPao2";
            this.txtPao2.Size = new System.Drawing.Size(88, 26);
            this.txtPao2.TabIndex = 190;
            // 
            // m_cmdToAaDO2
            // 
            this.m_cmdToAaDO2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdToAaDO2.DefaultScheme = true;
            this.m_cmdToAaDO2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdToAaDO2.ForeColor = System.Drawing.Color.Black;
            this.m_cmdToAaDO2.Hint = "";
            this.m_cmdToAaDO2.Location = new System.Drawing.Point(12, 124);
            this.m_cmdToAaDO2.Name = "m_cmdToAaDO2";
            this.m_cmdToAaDO2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdToAaDO2.Size = new System.Drawing.Size(136, 28);
            this.m_cmdToAaDO2.TabIndex = 10000016;
            this.m_cmdToAaDO2.Text = "=";
            this.m_cmdToAaDO2.Click += new System.EventHandler(this.m_cmdToAaDO2_Click);
            // 
            // txtPaCO2
            // 
            this.txtPaCO2.BackColor = System.Drawing.Color.White;
            this.txtPaCO2.BorderColor = System.Drawing.Color.Black;
            this.txtPaCO2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPaCO2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPaCO2.ForeColor = System.Drawing.Color.Black;
            this.txtPaCO2.Location = new System.Drawing.Point(156, 56);
            this.txtPaCO2.Name = "txtPaCO2";
            this.txtPaCO2.Size = new System.Drawing.Size(88, 26);
            this.txtPaCO2.TabIndex = 180;
            // 
            // txtFiO2
            // 
            this.txtFiO2.BackColor = System.Drawing.Color.White;
            this.txtFiO2.BorderColor = System.Drawing.Color.Black;
            this.txtFiO2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFiO2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFiO2.ForeColor = System.Drawing.Color.Black;
            this.txtFiO2.Location = new System.Drawing.Point(156, 24);
            this.txtFiO2.Name = "txtFiO2";
            this.txtFiO2.Size = new System.Drawing.Size(88, 26);
            this.txtFiO2.TabIndex = 170;
            // 
            // lblFiO2
            // 
            this.lblFiO2.AutoSize = true;
            this.lblFiO2.Location = new System.Drawing.Point(8, 28);
            this.lblFiO2.Name = "lblFiO2";
            this.lblFiO2.Size = new System.Drawing.Size(138, 14);
            this.lblFiO2.TabIndex = 4;
            this.lblFiO2.Text = "FiO2(吸入氧浓度):";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 32);
            this.label7.TabIndex = 198;
            this.label7.Text = "PaCO2(动脉血二氧化碳分压)(mmol/L):";
            // 
            // lblTitle14
            // 
            this.lblTitle14.Location = new System.Drawing.Point(8, 92);
            this.lblTitle14.Name = "lblTitle14";
            this.lblTitle14.Size = new System.Drawing.Size(144, 32);
            this.lblTitle14.TabIndex = 0;
            this.lblTitle14.Text = "PaO2(动脉血氧分压)(mmHg):";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("宋体", 63.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(316, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 4);
            this.label6.TabIndex = 197;
            // 
            // txtDo2
            // 
            this.txtDo2.BackColor = System.Drawing.Color.White;
            this.txtDo2.BorderColor = System.Drawing.Color.Black;
            this.txtDo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDo2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDo2.ForeColor = System.Drawing.Color.Black;
            this.txtDo2.Location = new System.Drawing.Point(156, 124);
            this.txtDo2.Name = "txtDo2";
            this.txtDo2.Size = new System.Drawing.Size(88, 26);
            this.txtDo2.TabIndex = 210;
            // 
            // m_gpbNerve
            // 
            this.m_gpbNerve.Controls.Add(this.m_lblOpenEyes);
            this.m_gpbNerve.Controls.Add(this.m_cboOpenEyes);
            this.m_gpbNerve.Controls.Add(this.m_lblSay);
            this.m_gpbNerve.Controls.Add(this.m_cboSay);
            this.m_gpbNerve.Controls.Add(this.m_lblSport);
            this.m_gpbNerve.Controls.Add(this.m_cboSport);
            this.m_gpbNerve.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_gpbNerve.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_gpbNerve.Location = new System.Drawing.Point(222, 180);
            this.m_gpbNerve.Name = "m_gpbNerve";
            this.m_gpbNerve.Size = new System.Drawing.Size(256, 104);
            this.m_gpbNerve.TabIndex = 215;
            this.m_gpbNerve.TabStop = false;
            this.m_gpbNerve.Text = "神经系统(GCS)";
            // 
            // m_lblOpenEyes
            // 
            this.m_lblOpenEyes.AutoSize = true;
            this.m_lblOpenEyes.Location = new System.Drawing.Point(13, 24);
            this.m_lblOpenEyes.Name = "m_lblOpenEyes";
            this.m_lblOpenEyes.Size = new System.Drawing.Size(99, 14);
            this.m_lblOpenEyes.TabIndex = 0;
            this.m_lblOpenEyes.Text = "睁 眼 反 应:";
            // 
            // m_cboOpenEyes
            // 
            this.m_cboOpenEyes.BackColor = System.Drawing.Color.White;
            this.m_cboOpenEyes.BorderColor = System.Drawing.Color.Black;
            this.m_cboOpenEyes.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboOpenEyes.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboOpenEyes.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboOpenEyes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboOpenEyes.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboOpenEyes.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboOpenEyes.ForeColor = System.Drawing.Color.Black;
            this.m_cboOpenEyes.ListBackColor = System.Drawing.Color.White;
            this.m_cboOpenEyes.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboOpenEyes.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboOpenEyes.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboOpenEyes.Location = new System.Drawing.Point(112, 16);
            this.m_cboOpenEyes.m_BlnEnableItemEventMenu = true;
            this.m_cboOpenEyes.MaxLength = 32767;
            this.m_cboOpenEyes.Name = "m_cboOpenEyes";
            this.m_cboOpenEyes.SelectedIndex = -1;
            this.m_cboOpenEyes.SelectedItem = null;
            this.m_cboOpenEyes.SelectionStart = 0;
            this.m_cboOpenEyes.Size = new System.Drawing.Size(132, 23);
            this.m_cboOpenEyes.TabIndex = 220;
            this.m_cboOpenEyes.TextBackColor = System.Drawing.Color.White;
            this.m_cboOpenEyes.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_lblSay
            // 
            this.m_lblSay.AutoSize = true;
            this.m_lblSay.Location = new System.Drawing.Point(13, 52);
            this.m_lblSay.Name = "m_lblSay";
            this.m_lblSay.Size = new System.Drawing.Size(99, 14);
            this.m_lblSay.TabIndex = 2;
            this.m_lblSay.Text = "言 语 反 应:";
            // 
            // m_cboSay
            // 
            this.m_cboSay.BackColor = System.Drawing.Color.White;
            this.m_cboSay.BorderColor = System.Drawing.Color.Black;
            this.m_cboSay.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboSay.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboSay.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboSay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboSay.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboSay.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboSay.ForeColor = System.Drawing.Color.Black;
            this.m_cboSay.ListBackColor = System.Drawing.Color.White;
            this.m_cboSay.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboSay.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboSay.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboSay.Location = new System.Drawing.Point(112, 44);
            this.m_cboSay.m_BlnEnableItemEventMenu = true;
            this.m_cboSay.MaxLength = 32767;
            this.m_cboSay.Name = "m_cboSay";
            this.m_cboSay.SelectedIndex = -1;
            this.m_cboSay.SelectedItem = null;
            this.m_cboSay.SelectionStart = 0;
            this.m_cboSay.Size = new System.Drawing.Size(132, 23);
            this.m_cboSay.TabIndex = 230;
            this.m_cboSay.TextBackColor = System.Drawing.Color.White;
            this.m_cboSay.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_lblSport
            // 
            this.m_lblSport.AutoSize = true;
            this.m_lblSport.Location = new System.Drawing.Point(13, 80);
            this.m_lblSport.Name = "m_lblSport";
            this.m_lblSport.Size = new System.Drawing.Size(99, 14);
            this.m_lblSport.TabIndex = 4;
            this.m_lblSport.Text = "运 动 反 应:";
            // 
            // m_cboSport
            // 
            this.m_cboSport.BackColor = System.Drawing.Color.White;
            this.m_cboSport.BorderColor = System.Drawing.Color.Black;
            this.m_cboSport.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboSport.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboSport.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboSport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboSport.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboSport.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboSport.ForeColor = System.Drawing.Color.Black;
            this.m_cboSport.ListBackColor = System.Drawing.Color.White;
            this.m_cboSport.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboSport.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboSport.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboSport.Location = new System.Drawing.Point(112, 72);
            this.m_cboSport.m_BlnEnableItemEventMenu = true;
            this.m_cboSport.MaxLength = 32767;
            this.m_cboSport.Name = "m_cboSport";
            this.m_cboSport.SelectedIndex = -1;
            this.m_cboSport.SelectedItem = null;
            this.m_cboSport.SelectionStart = 0;
            this.m_cboSport.Size = new System.Drawing.Size(132, 23);
            this.m_cboSport.TabIndex = 240;
            this.m_cboSport.TextBackColor = System.Drawing.Color.White;
            this.m_cboSport.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_cmdGetCheckData
            // 
            this.m_cmdGetCheckData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdGetCheckData.DefaultScheme = true;
            this.m_cmdGetCheckData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGetCheckData.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdGetCheckData.ForeColor = System.Drawing.Color.Black;
            this.m_cmdGetCheckData.Hint = "";
            this.m_cmdGetCheckData.Location = new System.Drawing.Point(487, 248);
            this.m_cmdGetCheckData.Name = "m_cmdGetCheckData";
            this.m_cmdGetCheckData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetCheckData.Size = new System.Drawing.Size(224, 32);
            this.m_cmdGetCheckData.TabIndex = 10000016;
            this.m_cmdGetCheckData.Text = "获取检验结果(&L)";
            this.m_cmdGetCheckData.Click += new System.EventHandler(this.m_cmdGetCheckData_Click);
            // 
            // m_cmdGetDovueData
            // 
            this.m_cmdGetDovueData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdGetDovueData.DefaultScheme = true;
            this.m_cmdGetDovueData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGetDovueData.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdGetDovueData.ForeColor = System.Drawing.Color.White;
            this.m_cmdGetDovueData.Hint = "";
            this.m_cmdGetDovueData.Location = new System.Drawing.Point(8, 252);
            this.m_cmdGetDovueData.Name = "m_cmdGetDovueData";
            this.m_cmdGetDovueData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetDovueData.Size = new System.Drawing.Size(200, 32);
            this.m_cmdGetDovueData.TabIndex = 10000005;
            this.m_cmdGetDovueData.Text = "获取监护结果(&G)";
            this.m_cmdGetDovueData.Click += new System.EventHandler(this.cmdGetData_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.rdbPatientSelOp);
            this.tabPage2.Controls.Add(this.rdbPatientUnOp);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.chkHurts);
            this.tabPage2.Controls.Add(this.cboMainReasonNoIn);
            this.tabPage2.Controls.Add(this.chkMainReasonNoIn);
            this.tabPage2.Controls.Add(this.cboNoInRange);
            this.tabPage2.Controls.Add(this.chkNoInRange);
            this.tabPage2.Controls.Add(this.cboOthers);
            this.tabPage2.Controls.Add(this.chkOthers);
            this.tabPage2.Controls.Add(this.cboNeurotic);
            this.tabPage2.Controls.Add(this.chkNeurotic);
            this.tabPage2.Controls.Add(this.cboHurts);
            this.tabPage2.Controls.Add(this.chkPatientUnOp2);
            this.tabPage2.Controls.Add(this.cboPatientUnOp2);
            this.tabPage2.Controls.Add(this.cboPatientUnOp1);
            this.tabPage2.Controls.Add(this.chkPatientAfterEmergency);
            this.tabPage2.Controls.Add(this.cboPatientSelOp);
            this.tabPage2.Controls.Add(this.chkPatientUnOp1);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.ForeColor = System.Drawing.Color.Black;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(832, 297);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "APACHE --患者住ICU的主要疾病分值";
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(12, 244);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(836, 3);
            this.groupBox6.TabIndex = 10000020;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "groupBox6";
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(12, 196);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(832, 3);
            this.groupBox5.TabIndex = 10000019;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "groupBox5";
            // 
            // rdbPatientSelOp
            // 
            this.rdbPatientSelOp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbPatientSelOp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdbPatientSelOp.ForeColor = System.Drawing.Color.Black;
            this.rdbPatientSelOp.Location = new System.Drawing.Point(12, 208);
            this.rdbPatientSelOp.Name = "rdbPatientSelOp";
            this.rdbPatientSelOp.Size = new System.Drawing.Size(176, 23);
            this.rdbPatientSelOp.TabIndex = 411;
            this.rdbPatientSelOp.Tag = "1";
            this.rdbPatientSelOp.Text = "择 期 手 术 后 患 者";
            this.rdbPatientSelOp.CheckedChanged += new System.EventHandler(this.rdbPatientSelOp_CheckedChanged_1);
            // 
            // rdbPatientUnOp
            // 
            this.rdbPatientUnOp.Checked = true;
            this.rdbPatientUnOp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbPatientUnOp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdbPatientUnOp.ForeColor = System.Drawing.Color.Black;
            this.rdbPatientUnOp.Location = new System.Drawing.Point(12, 92);
            this.rdbPatientUnOp.Name = "rdbPatientUnOp";
            this.rdbPatientUnOp.Size = new System.Drawing.Size(186, 29);
            this.rdbPatientUnOp.TabIndex = 412;
            this.rdbPatientUnOp.TabStop = true;
            this.rdbPatientUnOp.Tag = "0";
            this.rdbPatientUnOp.Text = "非手术或急诊手术后患者";
            this.rdbPatientUnOp.CheckedChanged += new System.EventHandler(this.rdbPatientUnOp_CheckedChanged_1);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 120F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(136, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 164);
            this.label1.TabIndex = 429;
            this.label1.Text = "{";
            // 
            // chkHurts
            // 
            this.chkHurts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkHurts.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkHurts.ForeColor = System.Drawing.Color.Black;
            this.chkHurts.Location = new System.Drawing.Point(244, 160);
            this.chkHurts.Name = "chkHurts";
            this.chkHurts.Size = new System.Drawing.Size(108, 23);
            this.chkHurts.TabIndex = 423;
            this.chkHurts.Text = "创伤";
            this.chkHurts.CheckedChanged += new System.EventHandler(this.chkHurts_CheckedChanged_1);
            // 
            // cboMainReasonNoIn
            // 
            this.cboMainReasonNoIn.BackColor = System.Drawing.SystemColors.Info;
            this.cboMainReasonNoIn.BorderColor = System.Drawing.Color.Black;
            this.cboMainReasonNoIn.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboMainReasonNoIn.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboMainReasonNoIn.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboMainReasonNoIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboMainReasonNoIn.Enabled = false;
            this.cboMainReasonNoIn.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboMainReasonNoIn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboMainReasonNoIn.ForeColor = System.Drawing.Color.Black;
            this.cboMainReasonNoIn.ListBackColor = System.Drawing.Color.White;
            this.cboMainReasonNoIn.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboMainReasonNoIn.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboMainReasonNoIn.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboMainReasonNoIn.Location = new System.Drawing.Point(600, 260);
            this.cboMainReasonNoIn.m_BlnEnableItemEventMenu = true;
            this.cboMainReasonNoIn.MaxLength = 32767;
            this.cboMainReasonNoIn.Name = "cboMainReasonNoIn";
            this.cboMainReasonNoIn.SelectedIndex = -1;
            this.cboMainReasonNoIn.SelectedItem = null;
            this.cboMainReasonNoIn.SelectionStart = 0;
            this.cboMainReasonNoIn.Size = new System.Drawing.Size(224, 23);
            this.cboMainReasonNoIn.TabIndex = 428;
            this.cboMainReasonNoIn.TextBackColor = System.Drawing.Color.White;
            this.cboMainReasonNoIn.TextForeColor = System.Drawing.Color.Black;
            // 
            // chkMainReasonNoIn
            // 
            this.chkMainReasonNoIn.Enabled = false;
            this.chkMainReasonNoIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkMainReasonNoIn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkMainReasonNoIn.ForeColor = System.Drawing.Color.Black;
            this.chkMainReasonNoIn.Location = new System.Drawing.Point(244, 260);
            this.chkMainReasonNoIn.Name = "chkMainReasonNoIn";
            this.chkMainReasonNoIn.Size = new System.Drawing.Size(356, 23);
            this.chkMainReasonNoIn.TabIndex = 427;
            this.chkMainReasonNoIn.Text = "主要原因不在上列范围，可选择主要器官系统";
            this.chkMainReasonNoIn.CheckedChanged += new System.EventHandler(this.chkMainReasonNoIn_CheckedChanged_1);
            // 
            // cboNoInRange
            // 
            this.cboNoInRange.BackColor = System.Drawing.SystemColors.Info;
            this.cboNoInRange.BorderColor = System.Drawing.Color.Black;
            this.cboNoInRange.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboNoInRange.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboNoInRange.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboNoInRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboNoInRange.Enabled = false;
            this.cboNoInRange.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboNoInRange.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboNoInRange.ForeColor = System.Drawing.Color.Black;
            this.cboNoInRange.ListBackColor = System.Drawing.Color.White;
            this.cboNoInRange.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboNoInRange.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboNoInRange.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboNoInRange.Location = new System.Drawing.Point(600, 132);
            this.cboNoInRange.m_BlnEnableItemEventMenu = true;
            this.cboNoInRange.MaxLength = 32767;
            this.cboNoInRange.Name = "cboNoInRange";
            this.cboNoInRange.SelectedIndex = -1;
            this.cboNoInRange.SelectedItem = null;
            this.cboNoInRange.SelectionStart = 0;
            this.cboNoInRange.Size = new System.Drawing.Size(224, 23);
            this.cboNoInRange.TabIndex = 422;
            this.cboNoInRange.TextBackColor = System.Drawing.Color.White;
            this.cboNoInRange.TextForeColor = System.Drawing.Color.Black;
            // 
            // chkNoInRange
            // 
            this.chkNoInRange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkNoInRange.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkNoInRange.ForeColor = System.Drawing.Color.Black;
            this.chkNoInRange.Location = new System.Drawing.Point(244, 128);
            this.chkNoInRange.Name = "chkNoInRange";
            this.chkNoInRange.Size = new System.Drawing.Size(344, 27);
            this.chkNoInRange.TabIndex = 421;
            this.chkNoInRange.Text = "主要疾病不在上列范围，选择主要器官系统";
            this.chkNoInRange.CheckedChanged += new System.EventHandler(this.chkNoInRange_CheckedChanged_1);
            // 
            // cboOthers
            // 
            this.cboOthers.BackColor = System.Drawing.SystemColors.Info;
            this.cboOthers.BorderColor = System.Drawing.Color.Black;
            this.cboOthers.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboOthers.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboOthers.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboOthers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboOthers.Enabled = false;
            this.cboOthers.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboOthers.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboOthers.ForeColor = System.Drawing.Color.Black;
            this.cboOthers.ListBackColor = System.Drawing.Color.White;
            this.cboOthers.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboOthers.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboOthers.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboOthers.Location = new System.Drawing.Point(600, 76);
            this.cboOthers.m_BlnEnableItemEventMenu = true;
            this.cboOthers.MaxLength = 32767;
            this.cboOthers.Name = "cboOthers";
            this.cboOthers.SelectedIndex = -1;
            this.cboOthers.SelectedItem = null;
            this.cboOthers.SelectionStart = 0;
            this.cboOthers.Size = new System.Drawing.Size(224, 23);
            this.cboOthers.TabIndex = 418;
            this.cboOthers.TextBackColor = System.Drawing.Color.White;
            this.cboOthers.TextForeColor = System.Drawing.Color.Black;
            // 
            // chkOthers
            // 
            this.chkOthers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOthers.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkOthers.ForeColor = System.Drawing.Color.Black;
            this.chkOthers.Location = new System.Drawing.Point(244, 76);
            this.chkOthers.Name = "chkOthers";
            this.chkOthers.Size = new System.Drawing.Size(140, 24);
            this.chkOthers.TabIndex = 417;
            this.chkOthers.Text = "其他";
            this.chkOthers.CheckedChanged += new System.EventHandler(this.chkOthers_CheckedChanged_1);
            // 
            // cboNeurotic
            // 
            this.cboNeurotic.BackColor = System.Drawing.SystemColors.Info;
            this.cboNeurotic.BorderColor = System.Drawing.Color.Black;
            this.cboNeurotic.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboNeurotic.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboNeurotic.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboNeurotic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboNeurotic.Enabled = false;
            this.cboNeurotic.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboNeurotic.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboNeurotic.ForeColor = System.Drawing.Color.Black;
            this.cboNeurotic.ListBackColor = System.Drawing.Color.White;
            this.cboNeurotic.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboNeurotic.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboNeurotic.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboNeurotic.Location = new System.Drawing.Point(600, 104);
            this.cboNeurotic.m_BlnEnableItemEventMenu = true;
            this.cboNeurotic.MaxLength = 32767;
            this.cboNeurotic.Name = "cboNeurotic";
            this.cboNeurotic.SelectedIndex = -1;
            this.cboNeurotic.SelectedItem = null;
            this.cboNeurotic.SelectionStart = 0;
            this.cboNeurotic.Size = new System.Drawing.Size(224, 23);
            this.cboNeurotic.TabIndex = 420;
            this.cboNeurotic.TextBackColor = System.Drawing.Color.White;
            this.cboNeurotic.TextForeColor = System.Drawing.Color.Black;
            // 
            // chkNeurotic
            // 
            this.chkNeurotic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkNeurotic.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkNeurotic.ForeColor = System.Drawing.Color.Black;
            this.chkNeurotic.Location = new System.Drawing.Point(244, 104);
            this.chkNeurotic.Name = "chkNeurotic";
            this.chkNeurotic.Size = new System.Drawing.Size(184, 23);
            this.chkNeurotic.TabIndex = 419;
            this.chkNeurotic.Text = "神经系统疾病";
            this.chkNeurotic.CheckedChanged += new System.EventHandler(this.chkNeurotic_CheckedChanged_1);
            // 
            // cboHurts
            // 
            this.cboHurts.BackColor = System.Drawing.SystemColors.Info;
            this.cboHurts.BorderColor = System.Drawing.Color.Black;
            this.cboHurts.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboHurts.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboHurts.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboHurts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboHurts.Enabled = false;
            this.cboHurts.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboHurts.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboHurts.ForeColor = System.Drawing.Color.Black;
            this.cboHurts.ListBackColor = System.Drawing.Color.White;
            this.cboHurts.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboHurts.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboHurts.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboHurts.Location = new System.Drawing.Point(600, 160);
            this.cboHurts.m_BlnEnableItemEventMenu = true;
            this.cboHurts.MaxLength = 32767;
            this.cboHurts.Name = "cboHurts";
            this.cboHurts.SelectedIndex = -1;
            this.cboHurts.SelectedItem = null;
            this.cboHurts.SelectionStart = 0;
            this.cboHurts.Size = new System.Drawing.Size(224, 23);
            this.cboHurts.TabIndex = 424;
            this.cboHurts.TextBackColor = System.Drawing.Color.White;
            this.cboHurts.TextForeColor = System.Drawing.Color.Black;
            // 
            // chkPatientUnOp2
            // 
            this.chkPatientUnOp2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPatientUnOp2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkPatientUnOp2.ForeColor = System.Drawing.Color.Black;
            this.chkPatientUnOp2.Location = new System.Drawing.Point(244, 48);
            this.chkPatientUnOp2.Name = "chkPatientUnOp2";
            this.chkPatientUnOp2.Size = new System.Drawing.Size(332, 24);
            this.chkPatientUnOp2.TabIndex = 415;
            this.chkPatientUnOp2.Text = "因下列因素导致的心血管功能衰竭或不全";
            this.chkPatientUnOp2.CheckedChanged += new System.EventHandler(this.chkPatientUnOp2_CheckedChanged_1);
            // 
            // cboPatientUnOp2
            // 
            this.cboPatientUnOp2.BackColor = System.Drawing.SystemColors.Info;
            this.cboPatientUnOp2.BorderColor = System.Drawing.Color.Black;
            this.cboPatientUnOp2.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboPatientUnOp2.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboPatientUnOp2.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboPatientUnOp2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboPatientUnOp2.Enabled = false;
            this.cboPatientUnOp2.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPatientUnOp2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPatientUnOp2.ForeColor = System.Drawing.Color.Black;
            this.cboPatientUnOp2.ListBackColor = System.Drawing.Color.White;
            this.cboPatientUnOp2.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboPatientUnOp2.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboPatientUnOp2.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboPatientUnOp2.Location = new System.Drawing.Point(600, 48);
            this.cboPatientUnOp2.m_BlnEnableItemEventMenu = true;
            this.cboPatientUnOp2.MaxLength = 32767;
            this.cboPatientUnOp2.Name = "cboPatientUnOp2";
            this.cboPatientUnOp2.SelectedIndex = -1;
            this.cboPatientUnOp2.SelectedItem = null;
            this.cboPatientUnOp2.SelectionStart = 0;
            this.cboPatientUnOp2.Size = new System.Drawing.Size(224, 23);
            this.cboPatientUnOp2.TabIndex = 416;
            this.cboPatientUnOp2.TextBackColor = System.Drawing.Color.White;
            this.cboPatientUnOp2.TextForeColor = System.Drawing.Color.Black;
            // 
            // cboPatientUnOp1
            // 
            this.cboPatientUnOp1.BackColor = System.Drawing.SystemColors.Info;
            this.cboPatientUnOp1.BorderColor = System.Drawing.Color.Black;
            this.cboPatientUnOp1.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboPatientUnOp1.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboPatientUnOp1.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboPatientUnOp1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboPatientUnOp1.Enabled = false;
            this.cboPatientUnOp1.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPatientUnOp1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPatientUnOp1.ForeColor = System.Drawing.Color.Black;
            this.cboPatientUnOp1.ListBackColor = System.Drawing.Color.White;
            this.cboPatientUnOp1.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboPatientUnOp1.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboPatientUnOp1.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboPatientUnOp1.Location = new System.Drawing.Point(600, 20);
            this.cboPatientUnOp1.m_BlnEnableItemEventMenu = true;
            this.cboPatientUnOp1.MaxLength = 32767;
            this.cboPatientUnOp1.Name = "cboPatientUnOp1";
            this.cboPatientUnOp1.SelectedIndex = -1;
            this.cboPatientUnOp1.SelectedItem = null;
            this.cboPatientUnOp1.SelectionStart = 0;
            this.cboPatientUnOp1.Size = new System.Drawing.Size(224, 23);
            this.cboPatientUnOp1.TabIndex = 414;
            this.cboPatientUnOp1.TextBackColor = System.Drawing.Color.White;
            this.cboPatientUnOp1.TextForeColor = System.Drawing.Color.Black;
            // 
            // chkPatientAfterEmergency
            // 
            this.chkPatientAfterEmergency.Enabled = false;
            this.chkPatientAfterEmergency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPatientAfterEmergency.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkPatientAfterEmergency.ForeColor = System.Drawing.Color.Black;
            this.chkPatientAfterEmergency.Location = new System.Drawing.Point(244, 208);
            this.chkPatientAfterEmergency.Name = "chkPatientAfterEmergency";
            this.chkPatientAfterEmergency.Size = new System.Drawing.Size(161, 24);
            this.chkPatientAfterEmergency.TabIndex = 426;
            this.chkPatientAfterEmergency.Text = "是急诊手术后患者";
            this.chkPatientAfterEmergency.CheckedChanged += new System.EventHandler(this.chkPatientAfterEmergency_CheckedChanged);
            // 
            // cboPatientSelOp
            // 
            this.cboPatientSelOp.BackColor = System.Drawing.SystemColors.Info;
            this.cboPatientSelOp.BorderColor = System.Drawing.Color.Black;
            this.cboPatientSelOp.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboPatientSelOp.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboPatientSelOp.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboPatientSelOp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboPatientSelOp.Enabled = false;
            this.cboPatientSelOp.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPatientSelOp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPatientSelOp.ForeColor = System.Drawing.Color.Black;
            this.cboPatientSelOp.ListBackColor = System.Drawing.Color.White;
            this.cboPatientSelOp.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboPatientSelOp.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboPatientSelOp.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboPatientSelOp.Location = new System.Drawing.Point(600, 208);
            this.cboPatientSelOp.m_BlnEnableItemEventMenu = true;
            this.cboPatientSelOp.MaxLength = 32767;
            this.cboPatientSelOp.Name = "cboPatientSelOp";
            this.cboPatientSelOp.SelectedIndex = -1;
            this.cboPatientSelOp.SelectedItem = null;
            this.cboPatientSelOp.SelectionStart = 0;
            this.cboPatientSelOp.Size = new System.Drawing.Size(224, 23);
            this.cboPatientSelOp.TabIndex = 425;
            this.cboPatientSelOp.TextBackColor = System.Drawing.Color.White;
            this.cboPatientSelOp.TextForeColor = System.Drawing.Color.Black;
            // 
            // chkPatientUnOp1
            // 
            this.chkPatientUnOp1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPatientUnOp1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkPatientUnOp1.ForeColor = System.Drawing.Color.Black;
            this.chkPatientUnOp1.Location = new System.Drawing.Point(244, 16);
            this.chkPatientUnOp1.Name = "chkPatientUnOp1";
            this.chkPatientUnOp1.Size = new System.Drawing.Size(316, 28);
            this.chkPatientUnOp1.TabIndex = 413;
            this.chkPatientUnOp1.Text = "因下列因素导致呼吸功能衰竭或不全";
            this.chkPatientUnOp1.CheckedChanged += new System.EventHandler(this.chkPatientUnOp1_CheckedChanged_1);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 120F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(168, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 88);
            this.label3.TabIndex = 430;
            this.label3.Text = "--";
            // 
            // cmdGetData
            // 
            this.cmdGetData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdGetData.DefaultScheme = true;
            this.cmdGetData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdGetData.ForeColor = System.Drawing.Color.Black;
            this.cmdGetData.Hint = "";
            this.cmdGetData.Location = new System.Drawing.Point(462, 557);
            this.cmdGetData.Name = "cmdGetData";
            this.cmdGetData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdGetData.Size = new System.Drawing.Size(84, 32);
            this.cmdGetData.TabIndex = 10000021;
            this.cmdGetData.Text = "获取数据";
            this.cmdGetData.Click += new System.EventHandler(this.cmdGetData_Click);
            // 
            // cmdStartAuto
            // 
            this.cmdStartAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdStartAuto.DefaultScheme = true;
            this.cmdStartAuto.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdStartAuto.ForeColor = System.Drawing.Color.Black;
            this.cmdStartAuto.Hint = "";
            this.cmdStartAuto.Location = new System.Drawing.Point(546, 557);
            this.cmdStartAuto.Name = "cmdStartAuto";
            this.cmdStartAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStartAuto.Size = new System.Drawing.Size(96, 32);
            this.cmdStartAuto.TabIndex = 10000020;
            this.cmdStartAuto.Text = "自动评分(&A)";
            this.cmdStartAuto.Click += new System.EventHandler(this.cmdStartAuto_Click);
            // 
            // cmdStopAuto
            // 
            this.cmdStopAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdStopAuto.DefaultScheme = true;
            this.cmdStopAuto.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdStopAuto.ForeColor = System.Drawing.Color.Black;
            this.cmdStopAuto.Hint = "";
            this.cmdStopAuto.Location = new System.Drawing.Point(646, 557);
            this.cmdStopAuto.Name = "cmdStopAuto";
            this.cmdStopAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStopAuto.Size = new System.Drawing.Size(96, 32);
            this.cmdStopAuto.TabIndex = 10000019;
            this.cmdStopAuto.Text = "停止评分(&S)";
            this.cmdStopAuto.Click += new System.EventHandler(this.cmdStopAuto_Click);
            // 
            // cmdShowResult
            // 
            this.cmdShowResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdShowResult.DefaultScheme = true;
            this.cmdShowResult.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdShowResult.ForeColor = System.Drawing.Color.Black;
            this.cmdShowResult.Hint = "";
            this.cmdShowResult.Location = new System.Drawing.Point(742, 557);
            this.cmdShowResult.Name = "cmdShowResult";
            this.cmdShowResult.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdShowResult.Size = new System.Drawing.Size(104, 32);
            this.cmdShowResult.TabIndex = 10000018;
            this.cmdShowResult.Text = "查看结果(&R)";
            this.cmdShowResult.Click += new System.EventHandler(this.cmdShowResult_Click);
            // 
            // txtAutoTime
            // 
            this.txtAutoTime.BackColor = System.Drawing.Color.White;
            this.txtAutoTime.BorderColor = System.Drawing.Color.Black;
            this.txtAutoTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAutoTime.ForeColor = System.Drawing.Color.Black;
            this.txtAutoTime.Location = new System.Drawing.Point(411, 559);
            this.txtAutoTime.MaxLength = 10;
            this.txtAutoTime.Name = "txtAutoTime";
            this.txtAutoTime.Size = new System.Drawing.Size(45, 23);
            this.txtAutoTime.TabIndex = 470;
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
            this.dtpStartSample.Location = new System.Drawing.Point(80, 562);
            this.dtpStartSample.m_BlnOnlyTime = false;
            this.dtpStartSample.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpStartSample.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartSample.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartSample.Name = "dtpStartSample";
            this.dtpStartSample.ReadOnly = false;
            this.dtpStartSample.Size = new System.Drawing.Size(218, 22);
            this.dtpStartSample.TabIndex = 450;
            this.dtpStartSample.TextBackColor = System.Drawing.Color.White;
            this.dtpStartSample.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblTitle96
            // 
            this.lblTitle96.AutoSize = true;
            this.lblTitle96.Location = new System.Drawing.Point(6, 563);
            this.lblTitle96.Name = "lblTitle96";
            this.lblTitle96.Size = new System.Drawing.Size(82, 14);
            this.lblTitle96.TabIndex = 425;
            this.lblTitle96.Text = "采集时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 563);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 14);
            this.label2.TabIndex = 420;
            this.label2.Text = "评分间隔(秒)：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgResult
            // 
            this.dtgResult.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dtgResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtgResult.CaptionBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dtgResult.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtgResult.CaptionForeColor = System.Drawing.Color.Black;
            this.dtgResult.CaptionText = "急性生理和既往健康评价系统";
            this.dtgResult.DataMember = "";
            this.dtgResult.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtgResult.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dtgResult.Location = new System.Drawing.Point(8, 465);
            this.dtgResult.Name = "dtgResult";
            this.dtgResult.PreferredColumnWidth = 80;
            this.dtgResult.ReadOnly = true;
            this.dtgResult.RowHeadersVisible = false;
            this.dtgResult.RowHeaderWidth = 0;
            this.dtgResult.Size = new System.Drawing.Size(838, 88);
            this.dtgResult.TabIndex = 390;
            this.dtgResult.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.dtgResult.TabStop = false;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.dtgResult;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn16,
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn5,
            this.dataGridTextBoxColumn6,
            this.dataGridTextBoxColumn7,
            this.dataGridTextBoxColumn8,
            this.dataGridTextBoxColumn9,
            this.dataGridTextBoxColumn10,
            this.dataGridTextBoxColumn11,
            this.dataGridTextBoxColumn12,
            this.dataGridTextBoxColumn13,
            this.dataGridTextBoxColumn14,
            this.dataGridTextBoxColumn15});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "result";
            // 
            // dataGridTextBoxColumn16
            // 
            this.dataGridTextBoxColumn16.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn16.Format = "";
            this.dataGridTextBoxColumn16.FormatInfo = null;
            this.dataGridTextBoxColumn16.HeaderText = "危险性";
            this.dataGridTextBoxColumn16.MappingName = "危险性";
            this.dataGridTextBoxColumn16.NullText = "/";
            this.dataGridTextBoxColumn16.ReadOnly = true;
            this.dataGridTextBoxColumn16.Width = 75;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "直肠温度";
            this.dataGridTextBoxColumn1.MappingName = "直肠温度";
            this.dataGridTextBoxColumn1.NullText = "/";
            this.dataGridTextBoxColumn1.ReadOnly = true;
            this.dataGridTextBoxColumn1.Width = 75;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "平均动脉压";
            this.dataGridTextBoxColumn2.MappingName = "平均动脉压";
            this.dataGridTextBoxColumn2.NullText = "/";
            this.dataGridTextBoxColumn2.ReadOnly = true;
            this.dataGridTextBoxColumn2.Width = 75;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "心率";
            this.dataGridTextBoxColumn3.MappingName = "心率";
            this.dataGridTextBoxColumn3.NullText = "/";
            this.dataGridTextBoxColumn3.ReadOnly = true;
            this.dataGridTextBoxColumn3.Width = 75;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "白细胞计数";
            this.dataGridTextBoxColumn4.MappingName = "白细胞计数";
            this.dataGridTextBoxColumn4.NullText = "/";
            this.dataGridTextBoxColumn4.ReadOnly = true;
            this.dataGridTextBoxColumn4.Width = 75;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "呼吸频率";
            this.dataGridTextBoxColumn5.MappingName = "呼吸频率";
            this.dataGridTextBoxColumn5.NullText = "/";
            this.dataGridTextBoxColumn5.ReadOnly = true;
            this.dataGridTextBoxColumn5.Width = 75;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "PaO2";
            this.dataGridTextBoxColumn6.MappingName = "PaO2";
            this.dataGridTextBoxColumn6.NullText = "/";
            this.dataGridTextBoxColumn6.ReadOnly = true;
            this.dataGridTextBoxColumn6.Width = 75;
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = "DO2";
            this.dataGridTextBoxColumn7.MappingName = "DO2";
            this.dataGridTextBoxColumn7.NullText = "/";
            this.dataGridTextBoxColumn7.ReadOnly = true;
            this.dataGridTextBoxColumn7.Width = 75;
            // 
            // dataGridTextBoxColumn8
            // 
            this.dataGridTextBoxColumn8.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn8.Format = "";
            this.dataGridTextBoxColumn8.FormatInfo = null;
            this.dataGridTextBoxColumn8.HeaderText = "动脉血";
            this.dataGridTextBoxColumn8.MappingName = "动脉血";
            this.dataGridTextBoxColumn8.NullText = "/";
            this.dataGridTextBoxColumn8.ReadOnly = true;
            this.dataGridTextBoxColumn8.Width = 75;
            // 
            // dataGridTextBoxColumn9
            // 
            this.dataGridTextBoxColumn9.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn9.Format = "";
            this.dataGridTextBoxColumn9.FormatInfo = null;
            this.dataGridTextBoxColumn9.HeaderText = "血钠浓度";
            this.dataGridTextBoxColumn9.MappingName = "血钠浓度";
            this.dataGridTextBoxColumn9.NullText = "/";
            this.dataGridTextBoxColumn9.ReadOnly = true;
            this.dataGridTextBoxColumn9.Width = 75;
            // 
            // dataGridTextBoxColumn10
            // 
            this.dataGridTextBoxColumn10.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn10.Format = "";
            this.dataGridTextBoxColumn10.FormatInfo = null;
            this.dataGridTextBoxColumn10.HeaderText = "血钾浓度";
            this.dataGridTextBoxColumn10.MappingName = "血钾浓度";
            this.dataGridTextBoxColumn10.NullText = "/";
            this.dataGridTextBoxColumn10.ReadOnly = true;
            this.dataGridTextBoxColumn10.Width = 75;
            // 
            // dataGridTextBoxColumn11
            // 
            this.dataGridTextBoxColumn11.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn11.Format = "";
            this.dataGridTextBoxColumn11.FormatInfo = null;
            this.dataGridTextBoxColumn11.HeaderText = "GCS";
            this.dataGridTextBoxColumn11.MappingName = "GCS";
            this.dataGridTextBoxColumn11.NullText = "/";
            this.dataGridTextBoxColumn11.ReadOnly = true;
            this.dataGridTextBoxColumn11.Width = 75;
            // 
            // dataGridTextBoxColumn12
            // 
            this.dataGridTextBoxColumn12.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn12.Format = "";
            this.dataGridTextBoxColumn12.FormatInfo = null;
            this.dataGridTextBoxColumn12.HeaderText = "血肌酐浓度";
            this.dataGridTextBoxColumn12.MappingName = "血肌酐浓度";
            this.dataGridTextBoxColumn12.NullText = "/";
            this.dataGridTextBoxColumn12.ReadOnly = true;
            this.dataGridTextBoxColumn12.Width = 75;
            // 
            // dataGridTextBoxColumn13
            // 
            this.dataGridTextBoxColumn13.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn13.Format = "";
            this.dataGridTextBoxColumn13.FormatInfo = null;
            this.dataGridTextBoxColumn13.HeaderText = "血细胞比容";
            this.dataGridTextBoxColumn13.MappingName = "血细胞比容";
            this.dataGridTextBoxColumn13.NullText = "/";
            this.dataGridTextBoxColumn13.ReadOnly = true;
            this.dataGridTextBoxColumn13.Width = 75;
            // 
            // dataGridTextBoxColumn14
            // 
            this.dataGridTextBoxColumn14.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn14.Format = "";
            this.dataGridTextBoxColumn14.FormatInfo = null;
            this.dataGridTextBoxColumn14.HeaderText = "静脉血HCO3-";
            this.dataGridTextBoxColumn14.MappingName = "静脉血HCO3-";
            this.dataGridTextBoxColumn14.NullText = "/";
            this.dataGridTextBoxColumn14.ReadOnly = true;
            this.dataGridTextBoxColumn14.Width = 75;
            // 
            // dataGridTextBoxColumn15
            // 
            this.dataGridTextBoxColumn15.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn15.Format = "";
            this.dataGridTextBoxColumn15.FormatInfo = null;
            this.dataGridTextBoxColumn15.HeaderText = "总数";
            this.dataGridTextBoxColumn15.MappingName = "总数";
            this.dataGridTextBoxColumn15.NullText = "/";
            this.dataGridTextBoxColumn15.ReadOnly = true;
            this.dataGridTextBoxColumn15.Width = 75;
            // 
            // txtEvalDoctor
            // 
            this.txtEvalDoctor.AccessibleDescription = "";
            this.txtEvalDoctor.BackColor = System.Drawing.Color.White;
            this.txtEvalDoctor.BorderColor = System.Drawing.Color.White;
            this.txtEvalDoctor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEvalDoctor.ForeColor = System.Drawing.Color.Black;
            this.txtEvalDoctor.Location = new System.Drawing.Point(546, 433);
            this.txtEvalDoctor.MaxLength = 8;
            this.txtEvalDoctor.Name = "txtEvalDoctor";
            this.txtEvalDoctor.Size = new System.Drawing.Size(120, 23);
            this.txtEvalDoctor.TabIndex = 430;
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
            this.dtpEvalDate.Location = new System.Drawing.Point(112, 437);
            this.dtpEvalDate.m_BlnOnlyTime = false;
            this.dtpEvalDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpEvalDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEvalDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEvalDate.Name = "dtpEvalDate";
            this.dtpEvalDate.ReadOnly = false;
            this.dtpEvalDate.Size = new System.Drawing.Size(220, 22);
            this.dtpEvalDate.TabIndex = 420;
            this.dtpEvalDate.TextBackColor = System.Drawing.Color.White;
            this.dtpEvalDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblEvalDate
            // 
            this.lblEvalDate.AutoSize = true;
            this.lblEvalDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEvalDate.Location = new System.Drawing.Point(24, 437);
            this.lblEvalDate.Name = "lblEvalDate";
            this.lblEvalDate.Size = new System.Drawing.Size(82, 14);
            this.lblEvalDate.TabIndex = 395;
            this.lblEvalDate.Text = "评分日期：";
            // 
            // timAutoCollect
            // 
            this.timAutoCollect.Interval = 60000;
            this.timAutoCollect.SynchronizingObject = this;
            this.timAutoCollect.Elapsed += new System.Timers.ElapsedEventHandler(this.timAutoCollect_Elapsed);
            // 
            // cmdCalculate
            // 
            this.cmdCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdCalculate.DefaultScheme = true;
            this.cmdCalculate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdCalculate.ForeColor = System.Drawing.Color.Black;
            this.cmdCalculate.Hint = "";
            this.cmdCalculate.Location = new System.Drawing.Point(740, 435);
            this.cmdCalculate.Name = "cmdCalculate";
            this.cmdCalculate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCalculate.Size = new System.Drawing.Size(105, 28);
            this.cmdCalculate.TabIndex = 10000017;
            this.cmdCalculate.Text = "计算分值(&C)";
            this.cmdCalculate.Click += new System.EventHandler(this.cmdCalculate_Click);
            // 
            // m_cmdEvalDoctor
            // 
            this.m_cmdEvalDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdEvalDoctor.DefaultScheme = true;
            this.m_cmdEvalDoctor.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdEvalDoctor.ForeColor = System.Drawing.Color.Black;
            this.m_cmdEvalDoctor.Hint = "";
            this.m_cmdEvalDoctor.Location = new System.Drawing.Point(444, 433);
            this.m_cmdEvalDoctor.Name = "m_cmdEvalDoctor";
            this.m_cmdEvalDoctor.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdEvalDoctor.Size = new System.Drawing.Size(96, 28);
            this.m_cmdEvalDoctor.TabIndex = 10000016;
            this.m_cmdEvalDoctor.Text = "评估者(&P)";
            // 
            // frmAPACHEIIValuation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.ClientSize = new System.Drawing.Size(861, 599);
            this.Controls.Add(this.dtpEvalDate);
            this.Controls.Add(this.m_cmdEvalDoctor);
            this.Controls.Add(this.lblEvalDate);
            this.Controls.Add(this.txtEvalDoctor);
            this.Controls.Add(this.tabAPACHEIIValuation);
            this.Controls.Add(this.dtgResult);
            this.Controls.Add(this.cmdCalculate);
            this.Controls.Add(this.cmdGetData);
            this.Controls.Add(this.cmdStartAuto);
            this.Controls.Add(this.cmdStopAuto);
            this.Controls.Add(this.cmdShowResult);
            this.Controls.Add(this.txtAutoTime);
            this.Controls.Add(this.dtpStartSample);
            this.Controls.Add(this.lblTitle96);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAPACHEIIValuation";
            this.Text = "APACHEII评分";
            this.Load += new System.EventHandler(this.frmAPACHEIIValuation_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmAPACHEIIValuation_Closing);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lblTitle96, 0);
            this.Controls.SetChildIndex(this.dtpStartSample, 0);
            this.Controls.SetChildIndex(this.txtAutoTime, 0);
            this.Controls.SetChildIndex(this.cmdShowResult, 0);
            this.Controls.SetChildIndex(this.cmdStopAuto, 0);
            this.Controls.SetChildIndex(this.cmdStartAuto, 0);
            this.Controls.SetChildIndex(this.cmdGetData, 0);
            this.Controls.SetChildIndex(this.cmdCalculate, 0);
            this.Controls.SetChildIndex(this.dtgResult, 0);
            this.Controls.SetChildIndex(this.tabAPACHEIIValuation, 0);
            this.Controls.SetChildIndex(this.txtEvalDoctor, 0);
            this.Controls.SetChildIndex(this.lblEvalDate, 0);
            this.Controls.SetChildIndex(this.m_cmdEvalDoctor, 0);
            this.Controls.SetChildIndex(this.dtpEvalDate, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.tabAPACHEIIValuation.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.gpbAge.ResumeLayout(false);
            this.gpbExactLife.ResumeLayout(false);
            this.gpbExactLife.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.m_gpbNerve.ResumeLayout(false);
            this.m_gpbNerve.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        
        private APACHEIIValuationDB m_objAPACHEIIValuationDB;
        /// <summary>
        /// 特殊符号
        /// </summary>
        private const string c_strSpecialSymbol = "§";
    
    }
}
