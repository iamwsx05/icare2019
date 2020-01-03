using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iCare.ICU.Evaluation
{
    partial class frmSIRSEvaluation
    {
        #region Dispose
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSIRSEvaluation));
            this.gpbAge = new System.Windows.Forms.GroupBox();
            this.rdbAgeO5Day = new System.Windows.Forms.RadioButton();
            this.rdbAgeU1Mon = new System.Windows.Forms.RadioButton();
            this.rdbAgeU1Year = new System.Windows.Forms.RadioButton();
            this.rdbAgeU2Year = new System.Windows.Forms.RadioButton();
            this.rdbAgeU5Year = new System.Windows.Forms.RadioButton();
            this.rdbAgeU12Year = new System.Windows.Forms.RadioButton();
            this.rdbAgeU15Year = new System.Windows.Forms.RadioButton();
            this.rdbAgeO15Year = new System.Windows.Forms.RadioButton();
            this.lblTitleBreath = new System.Windows.Forms.Label();
            this.txtBreath = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitleBreathUnit = new System.Windows.Forms.Label();
            this.lblTitleHR = new System.Windows.Forms.Label();
            this.txtHeartRate = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitleHRUnit = new System.Windows.Forms.Label();
            this.lblTitleTempUnit = new System.Windows.Forms.Label();
            this.lblTitleTemp = new System.Windows.Forms.Label();
            this.txtTemperature = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.gpbWBC = new System.Windows.Forms.GroupBox();
            this.lblTitle11 = new System.Windows.Forms.Label();
            this.lblTitleWBCUnit = new System.Windows.Forms.Label();
            this.txtWBC = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.rdbWBC = new System.Windows.Forms.RadioButton();
            this.txtBacillus = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rdbBacillus = new System.Windows.Forms.RadioButton();
            this.lblTitle31 = new System.Windows.Forms.Label();
            this.dtpEvalDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblEvalDate = new System.Windows.Forms.Label();
            this.dtgResult = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.lblTileWBCSpec = new System.Windows.Forms.Label();
            this.lblTitleAgeSpec = new System.Windows.Forms.Label();
            this.lblTitle10 = new System.Windows.Forms.Label();
            this.lblTitle96 = new System.Windows.Forms.Label();
            this.dtpStartSample = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAutoTime = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.timAutoCollect = new System.Timers.Timer();
            this.gpbEvaluation = new System.Windows.Forms.GroupBox();
            this.cmdShowResult = new PinkieControls.ButtonXP();
            this.cmdStopAuto = new PinkieControls.ButtonXP();
            this.cmdStartAuto = new PinkieControls.ButtonXP();
            this.cmdGetData = new PinkieControls.ButtonXP();
            this.lbltxtEvalDoctor = new System.Windows.Forms.Label();
            this.m_cmdGetDovueData = new PinkieControls.ButtonXP();
            this.cmdCalculate = new PinkieControls.ButtonXP();
            this.m_cmdGetCheckData = new PinkieControls.ButtonXP();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_pnlNewBase.SuspendLayout();
            this.gpbAge.SuspendLayout();
            this.gpbWBC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).BeginInit();
            this.gpbEvaluation.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            // gpbAge
            // 
            this.gpbAge.Controls.Add(this.rdbAgeO5Day);
            this.gpbAge.Controls.Add(this.rdbAgeU1Mon);
            this.gpbAge.Controls.Add(this.rdbAgeU1Year);
            this.gpbAge.Controls.Add(this.rdbAgeU2Year);
            this.gpbAge.Controls.Add(this.rdbAgeU5Year);
            this.gpbAge.Controls.Add(this.rdbAgeU12Year);
            this.gpbAge.Controls.Add(this.rdbAgeU15Year);
            this.gpbAge.Controls.Add(this.rdbAgeO15Year);
            this.gpbAge.Location = new System.Drawing.Point(8, 109);
            this.gpbAge.Name = "gpbAge";
            this.gpbAge.Size = new System.Drawing.Size(128, 220);
            this.gpbAge.TabIndex = 41;
            this.gpbAge.TabStop = false;
            this.gpbAge.Text = "年龄组:";
            // 
            // rdbAgeO5Day
            // 
            this.rdbAgeO5Day.Checked = true;
            this.rdbAgeO5Day.Location = new System.Drawing.Point(8, 20);
            this.rdbAgeO5Day.Name = "rdbAgeO5Day";
            this.rdbAgeO5Day.Size = new System.Drawing.Size(72, 21);
            this.rdbAgeO5Day.TabIndex = 50;
            this.rdbAgeO5Day.TabStop = true;
            this.rdbAgeO5Day.Tag = "0";
            this.rdbAgeO5Day.Text = "> 5天";
            this.rdbAgeO5Day.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbAgeU1Mon
            // 
            this.rdbAgeU1Mon.Location = new System.Drawing.Point(8, 44);
            this.rdbAgeU1Mon.Name = "rdbAgeU1Mon";
            this.rdbAgeU1Mon.Size = new System.Drawing.Size(72, 21);
            this.rdbAgeU1Mon.TabIndex = 50;
            this.rdbAgeU1Mon.TabStop = true;
            this.rdbAgeU1Mon.Tag = "1";
            this.rdbAgeU1Mon.Text = "< 1月";
            this.rdbAgeU1Mon.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbAgeU1Year
            // 
            this.rdbAgeU1Year.Location = new System.Drawing.Point(8, 68);
            this.rdbAgeU1Year.Name = "rdbAgeU1Year";
            this.rdbAgeU1Year.Size = new System.Drawing.Size(88, 21);
            this.rdbAgeU1Year.TabIndex = 60;
            this.rdbAgeU1Year.TabStop = true;
            this.rdbAgeU1Year.Tag = "2";
            this.rdbAgeU1Year.Text = "1～12月";
            this.rdbAgeU1Year.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbAgeU2Year
            // 
            this.rdbAgeU2Year.Location = new System.Drawing.Point(8, 92);
            this.rdbAgeU2Year.Name = "rdbAgeU2Year";
            this.rdbAgeU2Year.Size = new System.Drawing.Size(80, 21);
            this.rdbAgeU2Year.TabIndex = 70;
            this.rdbAgeU2Year.TabStop = true;
            this.rdbAgeU2Year.Tag = "3";
            this.rdbAgeU2Year.Text = "1～2岁";
            this.rdbAgeU2Year.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbAgeU5Year
            // 
            this.rdbAgeU5Year.Location = new System.Drawing.Point(8, 116);
            this.rdbAgeU5Year.Name = "rdbAgeU5Year";
            this.rdbAgeU5Year.Size = new System.Drawing.Size(80, 21);
            this.rdbAgeU5Year.TabIndex = 80;
            this.rdbAgeU5Year.TabStop = true;
            this.rdbAgeU5Year.Tag = "4";
            this.rdbAgeU5Year.Text = "2～5岁";
            this.rdbAgeU5Year.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbAgeU12Year
            // 
            this.rdbAgeU12Year.Location = new System.Drawing.Point(8, 140);
            this.rdbAgeU12Year.Name = "rdbAgeU12Year";
            this.rdbAgeU12Year.Size = new System.Drawing.Size(88, 21);
            this.rdbAgeU12Year.TabIndex = 90;
            this.rdbAgeU12Year.TabStop = true;
            this.rdbAgeU12Year.Tag = "5";
            this.rdbAgeU12Year.Text = "5～12岁";
            this.rdbAgeU12Year.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbAgeU15Year
            // 
            this.rdbAgeU15Year.Location = new System.Drawing.Point(8, 164);
            this.rdbAgeU15Year.Name = "rdbAgeU15Year";
            this.rdbAgeU15Year.Size = new System.Drawing.Size(88, 21);
            this.rdbAgeU15Year.TabIndex = 100;
            this.rdbAgeU15Year.TabStop = true;
            this.rdbAgeU15Year.Tag = "6";
            this.rdbAgeU15Year.Text = "12～15岁";
            this.rdbAgeU15Year.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbAgeO15Year
            // 
            this.rdbAgeO15Year.Location = new System.Drawing.Point(8, 188);
            this.rdbAgeO15Year.Name = "rdbAgeO15Year";
            this.rdbAgeO15Year.Size = new System.Drawing.Size(100, 21);
            this.rdbAgeO15Year.TabIndex = 110;
            this.rdbAgeO15Year.TabStop = true;
            this.rdbAgeO15Year.Tag = "7";
            this.rdbAgeO15Year.Text = "> 15岁成人";
            this.rdbAgeO15Year.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // lblTitleBreath
            // 
            this.lblTitleBreath.AutoSize = true;
            this.lblTitleBreath.Location = new System.Drawing.Point(12, 16);
            this.lblTitleBreath.Name = "lblTitleBreath";
            this.lblTitleBreath.Size = new System.Drawing.Size(75, 14);
            this.lblTitleBreath.TabIndex = 392;
            this.lblTitleBreath.Text = "呼吸频率:";
            this.lblTitleBreath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBreath
            // 
            this.txtBreath.AccessibleDescription = "呼吸频率";
            this.txtBreath.BackColor = System.Drawing.Color.White;
            this.txtBreath.BorderColor = System.Drawing.Color.Black;
            this.txtBreath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBreath.ForeColor = System.Drawing.Color.Black;
            this.txtBreath.Location = new System.Drawing.Point(104, 13);
            this.txtBreath.MaxLength = 3;
            this.txtBreath.Name = "txtBreath";
            this.txtBreath.Size = new System.Drawing.Size(116, 23);
            this.txtBreath.TabIndex = 120;
            // 
            // lblTitleBreathUnit
            // 
            this.lblTitleBreathUnit.AutoSize = true;
            this.lblTitleBreathUnit.Location = new System.Drawing.Point(220, 16);
            this.lblTitleBreathUnit.Name = "lblTitleBreathUnit";
            this.lblTitleBreathUnit.Size = new System.Drawing.Size(75, 14);
            this.lblTitleBreathUnit.TabIndex = 394;
            this.lblTitleBreathUnit.Text = "（次/分）";
            this.lblTitleBreathUnit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleHR
            // 
            this.lblTitleHR.AutoSize = true;
            this.lblTitleHR.Location = new System.Drawing.Point(12, 48);
            this.lblTitleHR.Name = "lblTitleHR";
            this.lblTitleHR.Size = new System.Drawing.Size(52, 14);
            this.lblTitleHR.TabIndex = 392;
            this.lblTitleHR.Text = "心率：";
            this.lblTitleHR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHeartRate
            // 
            this.txtHeartRate.AccessibleDescription = "心率";
            this.txtHeartRate.BackColor = System.Drawing.Color.White;
            this.txtHeartRate.BorderColor = System.Drawing.Color.Black;
            this.txtHeartRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHeartRate.ForeColor = System.Drawing.Color.Black;
            this.txtHeartRate.Location = new System.Drawing.Point(104, 44);
            this.txtHeartRate.MaxLength = 3;
            this.txtHeartRate.Name = "txtHeartRate";
            this.txtHeartRate.Size = new System.Drawing.Size(116, 23);
            this.txtHeartRate.TabIndex = 130;
            // 
            // lblTitleHRUnit
            // 
            this.lblTitleHRUnit.AutoSize = true;
            this.lblTitleHRUnit.Location = new System.Drawing.Point(220, 48);
            this.lblTitleHRUnit.Name = "lblTitleHRUnit";
            this.lblTitleHRUnit.Size = new System.Drawing.Size(75, 14);
            this.lblTitleHRUnit.TabIndex = 394;
            this.lblTitleHRUnit.Text = "（次/分）";
            this.lblTitleHRUnit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleTempUnit
            // 
            this.lblTitleTempUnit.AutoSize = true;
            this.lblTitleTempUnit.Location = new System.Drawing.Point(220, 80);
            this.lblTitleTempUnit.Name = "lblTitleTempUnit";
            this.lblTitleTempUnit.Size = new System.Drawing.Size(52, 14);
            this.lblTitleTempUnit.TabIndex = 394;
            this.lblTitleTempUnit.Text = "（℃）";
            this.lblTitleTempUnit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleTemp
            // 
            this.lblTitleTemp.AutoSize = true;
            this.lblTitleTemp.Location = new System.Drawing.Point(12, 80);
            this.lblTitleTemp.Name = "lblTitleTemp";
            this.lblTitleTemp.Size = new System.Drawing.Size(52, 14);
            this.lblTitleTemp.TabIndex = 392;
            this.lblTitleTemp.Text = "体温：";
            this.lblTitleTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTemperature
            // 
            this.txtTemperature.AccessibleDescription = "体温";
            this.txtTemperature.BackColor = System.Drawing.Color.White;
            this.txtTemperature.BorderColor = System.Drawing.Color.Black;
            this.txtTemperature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTemperature.ForeColor = System.Drawing.Color.Black;
            this.txtTemperature.Location = new System.Drawing.Point(104, 76);
            this.txtTemperature.MaxLength = 8;
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(116, 23);
            this.txtTemperature.TabIndex = 140;
            // 
            // gpbWBC
            // 
            this.gpbWBC.Controls.Add(this.lblTitle11);
            this.gpbWBC.Controls.Add(this.lblTitleWBCUnit);
            this.gpbWBC.Controls.Add(this.txtWBC);
            this.gpbWBC.Controls.Add(this.rdbWBC);
            this.gpbWBC.Controls.Add(this.txtBacillus);
            this.gpbWBC.Controls.Add(this.label1);
            this.gpbWBC.Controls.Add(this.label4);
            this.gpbWBC.Controls.Add(this.label5);
            this.gpbWBC.Controls.Add(this.rdbBacillus);
            this.gpbWBC.Location = new System.Drawing.Point(276, 225);
            this.gpbWBC.Name = "gpbWBC";
            this.gpbWBC.Size = new System.Drawing.Size(312, 104);
            this.gpbWBC.TabIndex = 148;
            this.gpbWBC.TabStop = false;
            this.gpbWBC.Text = "白细胞计数和分类";
            // 
            // lblTitle11
            // 
            this.lblTitle11.AutoSize = true;
            this.lblTitle11.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle11.Location = new System.Drawing.Point(268, 33);
            this.lblTitle11.Name = "lblTitle11";
            this.lblTitle11.Size = new System.Drawing.Size(10, 9);
            this.lblTitle11.TabIndex = 401;
            this.lblTitle11.Text = "9";
            // 
            // lblTitleWBCUnit
            // 
            this.lblTitleWBCUnit.AutoSize = true;
            this.lblTitleWBCUnit.Location = new System.Drawing.Point(224, 31);
            this.lblTitleWBCUnit.Name = "lblTitleWBCUnit";
            this.lblTitleWBCUnit.Size = new System.Drawing.Size(93, 14);
            this.lblTitleWBCUnit.TabIndex = 400;
            this.lblTitleWBCUnit.Text = "（*10  /L）";
            this.lblTitleWBCUnit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtWBC
            // 
            this.txtWBC.AccessibleDescription = "白细胞计数";
            this.txtWBC.BackColor = System.Drawing.Color.White;
            this.txtWBC.BorderColor = System.Drawing.Color.Black;
            this.txtWBC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWBC.ForeColor = System.Drawing.Color.Black;
            this.txtWBC.Location = new System.Drawing.Point(104, 28);
            this.txtWBC.MaxLength = 8;
            this.txtWBC.Name = "txtWBC";
            this.txtWBC.Size = new System.Drawing.Size(116, 23);
            this.txtWBC.TabIndex = 160;
            this.txtWBC.Tag = "0";
            this.txtWBC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // rdbWBC
            // 
            this.rdbWBC.Checked = true;
            this.rdbWBC.Location = new System.Drawing.Point(8, 28);
            this.rdbWBC.Name = "rdbWBC";
            this.rdbWBC.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rdbWBC.Size = new System.Drawing.Size(12, 24);
            this.rdbWBC.TabIndex = 150;
            this.rdbWBC.TabStop = true;
            this.rdbWBC.Tag = "0";
            this.rdbWBC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdbWBC.CheckedChanged += new System.EventHandler(this.WBCChanged);
            // 
            // txtBacillus
            // 
            this.txtBacillus.AccessibleDescription = "杆状核";
            this.txtBacillus.BackColor = System.Drawing.Color.White;
            this.txtBacillus.BorderColor = System.Drawing.Color.Black;
            this.txtBacillus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBacillus.Enabled = false;
            this.txtBacillus.ForeColor = System.Drawing.Color.Black;
            this.txtBacillus.Location = new System.Drawing.Point(104, 62);
            this.txtBacillus.MaxLength = 8;
            this.txtBacillus.Name = "txtBacillus";
            this.txtBacillus.Size = new System.Drawing.Size(116, 23);
            this.txtBacillus.TabIndex = 180;
            this.txtBacillus.Tag = "1";
            this.txtBacillus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 14);
            this.label1.TabIndex = 400;
            this.label1.Text = "％";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(20, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 10000014;
            this.label4.Text = "白细胞计数";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 10000015;
            this.label5.Text = "杆状核";
            // 
            // rdbBacillus
            // 
            this.rdbBacillus.Location = new System.Drawing.Point(4, 64);
            this.rdbBacillus.Name = "rdbBacillus";
            this.rdbBacillus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rdbBacillus.Size = new System.Drawing.Size(16, 20);
            this.rdbBacillus.TabIndex = 170;
            this.rdbBacillus.TabStop = true;
            this.rdbBacillus.Tag = "1";
            this.rdbBacillus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdbBacillus.CheckedChanged += new System.EventHandler(this.WBCChanged);
            // 
            // lblTitle31
            // 
            this.lblTitle31.AutoSize = true;
            this.lblTitle31.Location = new System.Drawing.Point(372, 337);
            this.lblTitle31.Name = "lblTitle31";
            this.lblTitle31.Size = new System.Drawing.Size(67, 14);
            this.lblTitle31.TabIndex = 399;
            this.lblTitle31.Text = "评估者：";
            this.lblTitle31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.dtpEvalDate.Location = new System.Drawing.Point(100, 335);
            this.dtpEvalDate.m_BlnOnlyTime = false;
            this.dtpEvalDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpEvalDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEvalDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEvalDate.Name = "dtpEvalDate";
            this.dtpEvalDate.ReadOnly = false;
            this.dtpEvalDate.Size = new System.Drawing.Size(227, 22);
            this.dtpEvalDate.TabIndex = 190;
            this.dtpEvalDate.TextBackColor = System.Drawing.Color.White;
            this.dtpEvalDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblEvalDate
            // 
            this.lblEvalDate.AutoSize = true;
            this.lblEvalDate.Location = new System.Drawing.Point(8, 337);
            this.lblEvalDate.Name = "lblEvalDate";
            this.lblEvalDate.Size = new System.Drawing.Size(82, 14);
            this.lblEvalDate.TabIndex = 396;
            this.lblEvalDate.Text = "评分日期：";
            this.lblEvalDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgResult
            // 
            this.dtgResult.BackColor = System.Drawing.Color.White;
            this.dtgResult.BackgroundColor = System.Drawing.Color.White;
            this.dtgResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtgResult.CaptionBackColor = System.Drawing.Color.White;
            this.dtgResult.CaptionForeColor = System.Drawing.Color.Black;
            this.dtgResult.CaptionText = "SIRS诊断结果";
            this.dtgResult.DataMember = "";
            this.dtgResult.ForeColor = System.Drawing.Color.Black;
            this.dtgResult.HeaderForeColor = System.Drawing.Color.Black;
            this.dtgResult.Location = new System.Drawing.Point(8, 365);
            this.dtgResult.Name = "dtgResult";
            this.dtgResult.ReadOnly = true;
            this.dtgResult.Size = new System.Drawing.Size(840, 118);
            this.dtgResult.TabIndex = 401;
            this.dtgResult.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.dtgResult.TabStop = false;
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
            this.dataGridTableStyle1.RowHeadersVisible = false;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "诊断结果";
            this.dataGridTextBoxColumn5.MappingName = "诊断结果";
            this.dataGridTextBoxColumn5.NullText = "/";
            this.dataGridTextBoxColumn5.Width = 200;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "呼吸频率";
            this.dataGridTextBoxColumn1.MappingName = "呼吸频率";
            this.dataGridTextBoxColumn1.NullText = "/";
            this.dataGridTextBoxColumn1.Width = 75;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "心率";
            this.dataGridTextBoxColumn2.MappingName = "心率";
            this.dataGridTextBoxColumn2.NullText = "/";
            this.dataGridTextBoxColumn2.Width = 75;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "体温";
            this.dataGridTextBoxColumn3.MappingName = "体温";
            this.dataGridTextBoxColumn3.NullText = "/";
            this.dataGridTextBoxColumn3.Width = 75;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "白细胞计数和分类";
            this.dataGridTextBoxColumn4.MappingName = "白细胞计数和分类";
            this.dataGridTextBoxColumn4.NullText = "/";
            this.dataGridTextBoxColumn4.Width = 75;
            // 
            // lblTileWBCSpec
            // 
            this.lblTileWBCSpec.Location = new System.Drawing.Point(592, 133);
            this.lblTileWBCSpec.Name = "lblTileWBCSpec";
            this.lblTileWBCSpec.Size = new System.Drawing.Size(256, 48);
            this.lblTileWBCSpec.TabIndex = 402;
            this.lblTileWBCSpec.Text = "1、白细胞计数和分类：需根据本实验室正常值进行调整。";
            this.lblTileWBCSpec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitleAgeSpec
            // 
            this.lblTitleAgeSpec.Location = new System.Drawing.Point(592, 181);
            this.lblTitleAgeSpec.Name = "lblTitleAgeSpec";
            this.lblTitleAgeSpec.Size = new System.Drawing.Size(255, 100);
            this.lblTitleAgeSpec.TabIndex = 402;
            this.lblTitleAgeSpec.Text = "2、年龄组为 “> 5”天 和“< 1月”：呼吸、心率、体温按足月胎龄后日龄计算；白细胞计数按生后日龄计算。";
            this.lblTitleAgeSpec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle10
            // 
            this.lblTitle10.AutoSize = true;
            this.lblTitle10.Location = new System.Drawing.Point(592, 109);
            this.lblTitle10.Name = "lblTitle10";
            this.lblTitle10.Size = new System.Drawing.Size(37, 14);
            this.lblTitle10.TabIndex = 403;
            this.lblTitle10.Text = "注：";
            this.lblTitle10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle96
            // 
            this.lblTitle96.AutoSize = true;
            this.lblTitle96.Location = new System.Drawing.Point(4, 23);
            this.lblTitle96.Name = "lblTitle96";
            this.lblTitle96.Size = new System.Drawing.Size(82, 14);
            this.lblTitle96.TabIndex = 392;
            this.lblTitle96.Text = "采集时间：";
            this.lblTitle96.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.dtpStartSample.Location = new System.Drawing.Point(88, 21);
            this.dtpStartSample.m_BlnOnlyTime = false;
            this.dtpStartSample.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpStartSample.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartSample.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartSample.Name = "dtpStartSample";
            this.dtpStartSample.ReadOnly = false;
            this.dtpStartSample.Size = new System.Drawing.Size(224, 22);
            this.dtpStartSample.TabIndex = 210;
            this.dtpStartSample.TextBackColor = System.Drawing.Color.White;
            this.dtpStartSample.TextForeColor = System.Drawing.Color.Black;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(404, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 14);
            this.label2.TabIndex = 387;
            this.label2.Text = "评分间隔(秒)：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAutoTime
            // 
            this.txtAutoTime.BackColor = System.Drawing.Color.White;
            this.txtAutoTime.BorderColor = System.Drawing.Color.Black;
            this.txtAutoTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAutoTime.ForeColor = System.Drawing.Color.Black;
            this.txtAutoTime.Location = new System.Drawing.Point(512, 21);
            this.txtAutoTime.MaxLength = 10;
            this.txtAutoTime.Name = "txtAutoTime";
            this.txtAutoTime.Size = new System.Drawing.Size(44, 23);
            this.txtAutoTime.TabIndex = 230;
            this.txtAutoTime.Text = "60";
            this.txtAutoTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAutoTime.TextChanged += new System.EventHandler(this.txtAutoTime_TextChanged);
            // 
            // timAutoCollect
            // 
            this.timAutoCollect.Interval = 60000;
            this.timAutoCollect.SynchronizingObject = this;
            this.timAutoCollect.Elapsed += new System.Timers.ElapsedEventHandler(this.timAutoCollect_Elapsed);
            // 
            // gpbEvaluation
            // 
            this.gpbEvaluation.Controls.Add(this.cmdShowResult);
            this.gpbEvaluation.Controls.Add(this.cmdStopAuto);
            this.gpbEvaluation.Controls.Add(this.cmdStartAuto);
            this.gpbEvaluation.Controls.Add(this.cmdGetData);
            this.gpbEvaluation.Controls.Add(this.dtpStartSample);
            this.gpbEvaluation.Controls.Add(this.lblTitle96);
            this.gpbEvaluation.Controls.Add(this.txtAutoTime);
            this.gpbEvaluation.Controls.Add(this.label2);
            this.gpbEvaluation.Location = new System.Drawing.Point(8, 489);
            this.gpbEvaluation.Name = "gpbEvaluation";
            this.gpbEvaluation.Size = new System.Drawing.Size(840, 64);
            this.gpbEvaluation.TabIndex = 429;
            this.gpbEvaluation.TabStop = false;
            this.gpbEvaluation.Text = "自动评分";
            // 
            // cmdShowResult
            // 
            this.cmdShowResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdShowResult.DefaultScheme = true;
            this.cmdShowResult.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdShowResult.ForeColor = System.Drawing.Color.Black;
            this.cmdShowResult.Hint = "";
            this.cmdShowResult.Location = new System.Drawing.Point(744, 16);
            this.cmdShowResult.Name = "cmdShowResult";
            this.cmdShowResult.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdShowResult.Size = new System.Drawing.Size(92, 32);
            this.cmdShowResult.TabIndex = 10000004;
            this.cmdShowResult.Text = "查看结果(&R)";
            this.cmdShowResult.Click += new System.EventHandler(this.cmdShowResult_Click);
            // 
            // cmdStopAuto
            // 
            this.cmdStopAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdStopAuto.DefaultScheme = true;
            this.cmdStopAuto.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdStopAuto.ForeColor = System.Drawing.Color.Black;
            this.cmdStopAuto.Hint = "";
            this.cmdStopAuto.Location = new System.Drawing.Point(652, 16);
            this.cmdStopAuto.Name = "cmdStopAuto";
            this.cmdStopAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStopAuto.Size = new System.Drawing.Size(92, 32);
            this.cmdStopAuto.TabIndex = 10000004;
            this.cmdStopAuto.Text = "停止评分(&S)";
            this.cmdStopAuto.Click += new System.EventHandler(this.cmdStopAuto_Click);
            // 
            // cmdStartAuto
            // 
            this.cmdStartAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdStartAuto.DefaultScheme = true;
            this.cmdStartAuto.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdStartAuto.ForeColor = System.Drawing.Color.Black;
            this.cmdStartAuto.Hint = "";
            this.cmdStartAuto.Location = new System.Drawing.Point(559, 16);
            this.cmdStartAuto.Name = "cmdStartAuto";
            this.cmdStartAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStartAuto.Size = new System.Drawing.Size(92, 32);
            this.cmdStartAuto.TabIndex = 10000004;
            this.cmdStartAuto.Text = "自动评分(&A)";
            this.cmdStartAuto.Click += new System.EventHandler(this.cmdStartAuto_Click);
            // 
            // cmdGetData
            // 
            this.cmdGetData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdGetData.DefaultScheme = true;
            this.cmdGetData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdGetData.ForeColor = System.Drawing.Color.Black;
            this.cmdGetData.Hint = "";
            this.cmdGetData.Location = new System.Drawing.Point(316, 16);
            this.cmdGetData.Name = "cmdGetData";
            this.cmdGetData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdGetData.Size = new System.Drawing.Size(92, 32);
            this.cmdGetData.TabIndex = 10000004;
            this.cmdGetData.Text = "获取数据(&G)";
            this.cmdGetData.Click += new System.EventHandler(this.cmdGetData_Click);
            // 
            // lbltxtEvalDoctor
            // 
            this.lbltxtEvalDoctor.Location = new System.Drawing.Point(452, 337);
            this.lbltxtEvalDoctor.Name = "lbltxtEvalDoctor";
            this.lbltxtEvalDoctor.Size = new System.Drawing.Size(108, 19);
            this.lbltxtEvalDoctor.TabIndex = 442;
            this.lbltxtEvalDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_cmdGetDovueData
            // 
            this.m_cmdGetDovueData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdGetDovueData.DefaultScheme = true;
            this.m_cmdGetDovueData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGetDovueData.ForeColor = System.Drawing.Color.Black;
            this.m_cmdGetDovueData.Hint = "";
            this.m_cmdGetDovueData.Location = new System.Drawing.Point(144, 117);
            this.m_cmdGetDovueData.Name = "m_cmdGetDovueData";
            this.m_cmdGetDovueData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetDovueData.Size = new System.Drawing.Size(124, 32);
            this.m_cmdGetDovueData.TabIndex = 10000004;
            this.m_cmdGetDovueData.Text = "获取监护结果(&G)";
            this.m_cmdGetDovueData.Click += new System.EventHandler(this.m_cmdGetDovueData_Click);
            // 
            // cmdCalculate
            // 
            this.cmdCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdCalculate.DefaultScheme = true;
            this.cmdCalculate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdCalculate.ForeColor = System.Drawing.Color.Black;
            this.cmdCalculate.Hint = "";
            this.cmdCalculate.Location = new System.Drawing.Point(752, 330);
            this.cmdCalculate.Name = "cmdCalculate";
            this.cmdCalculate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCalculate.Size = new System.Drawing.Size(96, 32);
            this.cmdCalculate.TabIndex = 10000004;
            this.cmdCalculate.Text = "诊 断(&E)";
            this.cmdCalculate.Click += new System.EventHandler(this.cmdCalculate_Click);
            // 
            // m_cmdGetCheckData
            // 
            this.m_cmdGetCheckData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdGetCheckData.DefaultScheme = true;
            this.m_cmdGetCheckData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGetCheckData.ForeColor = System.Drawing.Color.Black;
            this.m_cmdGetCheckData.Hint = "";
            this.m_cmdGetCheckData.Location = new System.Drawing.Point(144, 233);
            this.m_cmdGetCheckData.Name = "m_cmdGetCheckData";
            this.m_cmdGetCheckData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetCheckData.Size = new System.Drawing.Size(124, 32);
            this.m_cmdGetCheckData.TabIndex = 10000004;
            this.m_cmdGetCheckData.Text = "获取检验结果(&L)";
            this.m_cmdGetCheckData.Click += new System.EventHandler(this.m_cmdGetCheckData_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTitleBreathUnit);
            this.groupBox2.Controls.Add(this.txtTemperature);
            this.groupBox2.Controls.Add(this.lblTitleHRUnit);
            this.groupBox2.Controls.Add(this.lblTitleTempUnit);
            this.groupBox2.Controls.Add(this.lblTitleHR);
            this.groupBox2.Controls.Add(this.txtHeartRate);
            this.groupBox2.Controls.Add(this.txtBreath);
            this.groupBox2.Controls.Add(this.lblTitleTemp);
            this.groupBox2.Controls.Add(this.lblTitleBreath);
            this.groupBox2.Location = new System.Drawing.Point(276, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(312, 108);
            this.groupBox2.TabIndex = 10000013;
            this.groupBox2.TabStop = false;
            // 
            // frmSIRSEvaluation
            // 
            this.AccessibleDescription = "SIRS诊断评分";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.ClientSize = new System.Drawing.Size(862, 625);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gpbAge);
            this.Controls.Add(this.lblTitle10);
            this.Controls.Add(this.lblTileWBCSpec);
            this.Controls.Add(this.lblTitle31);
            this.Controls.Add(this.lblEvalDate);
            this.Controls.Add(this.lbltxtEvalDoctor);
            this.Controls.Add(this.gpbEvaluation);
            this.Controls.Add(this.dtgResult);
            this.Controls.Add(this.dtpEvalDate);
            this.Controls.Add(this.gpbWBC);
            this.Controls.Add(this.cmdCalculate);
            this.Controls.Add(this.m_cmdGetCheckData);
            this.Controls.Add(this.m_cmdGetDovueData);
            this.Controls.Add(this.lblTitleAgeSpec);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSIRSEvaluation";
            this.Text = "SIRS诊断评分";
            this.Load += new System.EventHandler(this.SIRSEvaluation_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SIRSEvaluation_Closing);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.lblTitleAgeSpec, 0);
            this.Controls.SetChildIndex(this.m_cmdGetDovueData, 0);
            this.Controls.SetChildIndex(this.m_cmdGetCheckData, 0);
            this.Controls.SetChildIndex(this.cmdCalculate, 0);
            this.Controls.SetChildIndex(this.gpbWBC, 0);
            this.Controls.SetChildIndex(this.dtpEvalDate, 0);
            this.Controls.SetChildIndex(this.dtgResult, 0);
            this.Controls.SetChildIndex(this.gpbEvaluation, 0);
            this.Controls.SetChildIndex(this.lbltxtEvalDoctor, 0);
            this.Controls.SetChildIndex(this.lblEvalDate, 0);
            this.Controls.SetChildIndex(this.lblTitle31, 0);
            this.Controls.SetChildIndex(this.lblTileWBCSpec, 0);
            this.Controls.SetChildIndex(this.lblTitle10, 0);
            this.Controls.SetChildIndex(this.gpbAge, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.gpbAge.ResumeLayout(false);
            this.gpbWBC.ResumeLayout(false);
            this.gpbWBC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).EndInit();
            this.gpbEvaluation.ResumeLayout(false);
            this.gpbEvaluation.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

    }
}
