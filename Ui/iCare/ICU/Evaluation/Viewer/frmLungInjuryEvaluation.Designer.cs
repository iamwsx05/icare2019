using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iCare.ICU.Evaluation
{
    partial class frmLungInjuryEvaluation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLungInjuryEvaluation));
            this.lblEvalDate = new System.Windows.Forms.Label();
            this.dtpEvalDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.cboXRayEval = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.lblTitle6 = new System.Windows.Forms.Label();
            this.lblTitle7 = new System.Windows.Forms.Label();
            this.gpbLowOxygenBlood = new System.Windows.Forms.GroupBox();
            this.txtOxyGenValkPa = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtFio2kPa = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtPao2kPa = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle15 = new System.Windows.Forms.Label();
            this.lblTitle16 = new System.Windows.Forms.Label();
            this.lblTitle11 = new System.Windows.Forms.Label();
            this.lblTitle21 = new System.Windows.Forms.Label();
            this.lblTitle20 = new System.Windows.Forms.Label();
            this.lblTitle8 = new System.Windows.Forms.Label();
            this.rdbOxygenkPa = new System.Windows.Forms.RadioButton();
            this.rdbOxygenmmHg = new System.Windows.Forms.RadioButton();
            this.txtPao2mmHg = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle9 = new System.Windows.Forms.Label();
            this.lblTitle10 = new System.Windows.Forms.Label();
            this.lblTitle12 = new System.Windows.Forms.Label();
            this.lblTitle13 = new System.Windows.Forms.Label();
            this.txtFio2mmHg = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle22 = new System.Windows.Forms.Label();
            this.lblTitle23 = new System.Windows.Forms.Label();
            this.txtOxyGenValmmHg = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle14 = new System.Windows.Forms.Label();
            this.lblTitle17 = new System.Windows.Forms.Label();
            this.txtPEEP = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle18 = new System.Windows.Forms.Label();
            this.lblTitle19 = new System.Windows.Forms.Label();
            this.lblTitle24 = new System.Windows.Forms.Label();
            this.lblTitle25 = new System.Windows.Forms.Label();
            this.lblTitle26 = new System.Windows.Forms.Label();
            this.lblTitle27 = new System.Windows.Forms.Label();
            this.txtVt = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtPIP = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtLungPEEP = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtLungSysHumorVal = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle28 = new System.Windows.Forms.Label();
            this.lblTitle29 = new System.Windows.Forms.Label();
            this.lblTitle30 = new System.Windows.Forms.Label();
            this.lblTitle35 = new System.Windows.Forms.Label();
            this.dtgResult = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.lblTitle31 = new System.Windows.Forms.Label();
            this.lblTitle96 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAutoTime = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.timAutoCollect = new System.Timers.Timer();
            this.dtpStartSample = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lbltxtEvalDoctor = new System.Windows.Forms.Label();
            this.gpbAutoEval = new System.Windows.Forms.GroupBox();
            this.cmdGetData = new PinkieControls.ButtonXP();
            this.cmdStartAuto = new PinkieControls.ButtonXP();
            this.cmdStopAuto = new PinkieControls.ButtonXP();
            this.cmdShowResult = new PinkieControls.ButtonXP();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdCalculate = new PinkieControls.ButtonXP();
            this.m_cmdGetCheckData = new PinkieControls.ButtonXP();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.m_pnlNewBase.SuspendLayout();
            this.gpbLowOxygenBlood.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).BeginInit();
            this.gpbAutoEval.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.lblEvalDate.Location = new System.Drawing.Point(4, 317);
            this.lblEvalDate.Name = "lblEvalDate";
            this.lblEvalDate.Size = new System.Drawing.Size(82, 14);
            this.lblEvalDate.TabIndex = 290;
            this.lblEvalDate.Text = "评分日期：";
            this.lblEvalDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.dtpEvalDate.Location = new System.Drawing.Point(80, 315);
            this.dtpEvalDate.m_BlnOnlyTime = false;
            this.dtpEvalDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpEvalDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEvalDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEvalDate.Name = "dtpEvalDate";
            this.dtpEvalDate.ReadOnly = false;
            this.dtpEvalDate.Size = new System.Drawing.Size(220, 22);
            this.dtpEvalDate.TabIndex = 170;
            this.dtpEvalDate.TextBackColor = System.Drawing.Color.White;
            this.dtpEvalDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // cboXRayEval
            // 
            this.cboXRayEval.BackColor = System.Drawing.Color.White;
            this.cboXRayEval.BorderColor = System.Drawing.Color.Black;
            this.cboXRayEval.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboXRayEval.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboXRayEval.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboXRayEval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboXRayEval.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboXRayEval.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboXRayEval.ForeColor = System.Drawing.Color.White;
            this.cboXRayEval.ListBackColor = System.Drawing.Color.White;
            this.cboXRayEval.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboXRayEval.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboXRayEval.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboXRayEval.Location = new System.Drawing.Point(8, 36);
            this.cboXRayEval.m_BlnEnableItemEventMenu = true;
            this.cboXRayEval.MaxLength = 32767;
            this.cboXRayEval.Name = "cboXRayEval";
            this.cboXRayEval.SelectedIndex = -1;
            this.cboXRayEval.SelectedItem = null;
            this.cboXRayEval.SelectionStart = 0;
            this.cboXRayEval.Size = new System.Drawing.Size(236, 23);
            this.cboXRayEval.TabIndex = 30;
            this.cboXRayEval.TextBackColor = System.Drawing.Color.White;
            this.cboXRayEval.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblTitle6
            // 
            this.lblTitle6.AutoSize = true;
            this.lblTitle6.Location = new System.Drawing.Point(32, 20);
            this.lblTitle6.Name = "lblTitle6";
            this.lblTitle6.Size = new System.Drawing.Size(77, 14);
            this.lblTitle6.TabIndex = 294;
            this.lblTitle6.Text = "PaO  值：";
            // 
            // lblTitle7
            // 
            this.lblTitle7.AutoSize = true;
            this.lblTitle7.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle7.Location = new System.Drawing.Point(60, 27);
            this.lblTitle7.Name = "lblTitle7";
            this.lblTitle7.Size = new System.Drawing.Size(10, 10);
            this.lblTitle7.TabIndex = 296;
            this.lblTitle7.Text = "2";
            // 
            // gpbLowOxygenBlood
            // 
            this.gpbLowOxygenBlood.Controls.Add(this.txtOxyGenValkPa);
            this.gpbLowOxygenBlood.Controls.Add(this.txtFio2kPa);
            this.gpbLowOxygenBlood.Controls.Add(this.txtPao2kPa);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle15);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle16);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle7);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle11);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle21);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle20);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle8);
            this.gpbLowOxygenBlood.Controls.Add(this.rdbOxygenkPa);
            this.gpbLowOxygenBlood.Controls.Add(this.rdbOxygenmmHg);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle6);
            this.gpbLowOxygenBlood.Controls.Add(this.txtPao2mmHg);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle9);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle10);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle12);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle13);
            this.gpbLowOxygenBlood.Controls.Add(this.txtFio2mmHg);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle22);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle23);
            this.gpbLowOxygenBlood.Controls.Add(this.txtOxyGenValmmHg);
            this.gpbLowOxygenBlood.Controls.Add(this.lblTitle14);
            this.gpbLowOxygenBlood.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gpbLowOxygenBlood.Location = new System.Drawing.Point(268, 109);
            this.gpbLowOxygenBlood.Name = "gpbLowOxygenBlood";
            this.gpbLowOxygenBlood.Size = new System.Drawing.Size(579, 100);
            this.gpbLowOxygenBlood.TabIndex = 38;
            this.gpbLowOxygenBlood.TabStop = false;
            this.gpbLowOxygenBlood.Text = "2.低氧血症";
            // 
            // txtOxyGenValkPa
            // 
            this.txtOxyGenValkPa.BackColor = System.Drawing.Color.White;
            this.txtOxyGenValkPa.BorderColor = System.Drawing.Color.Black;
            this.txtOxyGenValkPa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOxyGenValkPa.ForeColor = System.Drawing.Color.Black;
            this.txtOxyGenValkPa.Location = new System.Drawing.Point(424, 35);
            this.txtOxyGenValkPa.MaxLength = 10;
            this.txtOxyGenValkPa.Name = "txtOxyGenValkPa";
            this.txtOxyGenValkPa.ReadOnly = true;
            this.txtOxyGenValkPa.Size = new System.Drawing.Size(144, 23);
            this.txtOxyGenValkPa.TabIndex = 70;
            this.txtOxyGenValkPa.TabStop = false;
            this.txtOxyGenValkPa.Tag = "0";
            // 
            // txtFio2kPa
            // 
            this.txtFio2kPa.BackColor = System.Drawing.Color.White;
            this.txtFio2kPa.BorderColor = System.Drawing.Color.Black;
            this.txtFio2kPa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFio2kPa.ForeColor = System.Drawing.Color.Black;
            this.txtFio2kPa.Location = new System.Drawing.Point(204, 35);
            this.txtFio2kPa.MaxLength = 10;
            this.txtFio2kPa.Name = "txtFio2kPa";
            this.txtFio2kPa.Size = new System.Drawing.Size(100, 23);
            this.txtFio2kPa.TabIndex = 60;
            this.txtFio2kPa.Tag = "0";
            this.txtFio2kPa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPao2kPa
            // 
            this.txtPao2kPa.BackColor = System.Drawing.Color.White;
            this.txtPao2kPa.BorderColor = System.Drawing.Color.Black;
            this.txtPao2kPa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPao2kPa.ForeColor = System.Drawing.Color.Black;
            this.txtPao2kPa.Location = new System.Drawing.Point(32, 35);
            this.txtPao2kPa.MaxLength = 10;
            this.txtPao2kPa.Name = "txtPao2kPa";
            this.txtPao2kPa.Size = new System.Drawing.Size(92, 23);
            this.txtPao2kPa.TabIndex = 50;
            this.txtPao2kPa.Tag = "0";
            this.txtPao2kPa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTitle15
            // 
            this.lblTitle15.AutoSize = true;
            this.lblTitle15.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle15.Location = new System.Drawing.Point(500, 24);
            this.lblTitle15.Name = "lblTitle15";
            this.lblTitle15.Size = new System.Drawing.Size(10, 10);
            this.lblTitle15.TabIndex = 296;
            this.lblTitle15.Text = "2";
            // 
            // lblTitle16
            // 
            this.lblTitle16.AutoSize = true;
            this.lblTitle16.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle16.Location = new System.Drawing.Point(452, 24);
            this.lblTitle16.Name = "lblTitle16";
            this.lblTitle16.Size = new System.Drawing.Size(10, 10);
            this.lblTitle16.TabIndex = 296;
            this.lblTitle16.Text = "2";
            // 
            // lblTitle11
            // 
            this.lblTitle11.AutoSize = true;
            this.lblTitle11.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle11.Location = new System.Drawing.Point(256, 24);
            this.lblTitle11.Name = "lblTitle11";
            this.lblTitle11.Size = new System.Drawing.Size(10, 10);
            this.lblTitle11.TabIndex = 296;
            this.lblTitle11.Text = "2";
            // 
            // lblTitle21
            // 
            this.lblTitle21.AutoSize = true;
            this.lblTitle21.Location = new System.Drawing.Point(380, 38);
            this.lblTitle21.Name = "lblTitle21";
            this.lblTitle21.Size = new System.Drawing.Size(22, 14);
            this.lblTitle21.TabIndex = 303;
            this.lblTitle21.Text = "＝";
            // 
            // lblTitle20
            // 
            this.lblTitle20.AutoSize = true;
            this.lblTitle20.Location = new System.Drawing.Point(173, 38);
            this.lblTitle20.Name = "lblTitle20";
            this.lblTitle20.Size = new System.Drawing.Size(22, 14);
            this.lblTitle20.TabIndex = 302;
            this.lblTitle20.Text = "÷";
            // 
            // lblTitle8
            // 
            this.lblTitle8.AutoSize = true;
            this.lblTitle8.Location = new System.Drawing.Point(132, 38);
            this.lblTitle8.Name = "lblTitle8";
            this.lblTitle8.Size = new System.Drawing.Size(31, 14);
            this.lblTitle8.TabIndex = 298;
            this.lblTitle8.Text = "kPa";
            // 
            // rdbOxygenkPa
            // 
            this.rdbOxygenkPa.Checked = true;
            this.rdbOxygenkPa.Location = new System.Drawing.Point(13, 37);
            this.rdbOxygenkPa.Name = "rdbOxygenkPa";
            this.rdbOxygenkPa.Size = new System.Drawing.Size(16, 20);
            this.rdbOxygenkPa.TabIndex = 40;
            this.rdbOxygenkPa.TabStop = true;
            this.rdbOxygenkPa.Tag = "0";
            this.rdbOxygenkPa.CheckedChanged += new System.EventHandler(this.OxygenUnitChange);
            // 
            // rdbOxygenmmHg
            // 
            this.rdbOxygenmmHg.Location = new System.Drawing.Point(13, 67);
            this.rdbOxygenmmHg.Name = "rdbOxygenmmHg";
            this.rdbOxygenmmHg.Size = new System.Drawing.Size(16, 20);
            this.rdbOxygenmmHg.TabIndex = 80;
            this.rdbOxygenmmHg.TabStop = true;
            this.rdbOxygenmmHg.Tag = "1";
            this.rdbOxygenmmHg.CheckedChanged += new System.EventHandler(this.OxygenUnitChange);
            // 
            // txtPao2mmHg
            // 
            this.txtPao2mmHg.BackColor = System.Drawing.Color.White;
            this.txtPao2mmHg.BorderColor = System.Drawing.Color.Black;
            this.txtPao2mmHg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPao2mmHg.Enabled = false;
            this.txtPao2mmHg.ForeColor = System.Drawing.Color.Black;
            this.txtPao2mmHg.Location = new System.Drawing.Point(32, 65);
            this.txtPao2mmHg.MaxLength = 10;
            this.txtPao2mmHg.Name = "txtPao2mmHg";
            this.txtPao2mmHg.Size = new System.Drawing.Size(92, 23);
            this.txtPao2mmHg.TabIndex = 90;
            this.txtPao2mmHg.Tag = "1";
            this.txtPao2mmHg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTitle9
            // 
            this.lblTitle9.AutoSize = true;
            this.lblTitle9.Location = new System.Drawing.Point(132, 68);
            this.lblTitle9.Name = "lblTitle9";
            this.lblTitle9.Size = new System.Drawing.Size(39, 14);
            this.lblTitle9.TabIndex = 298;
            this.lblTitle9.Text = "mmHg";
            // 
            // lblTitle10
            // 
            this.lblTitle10.AutoSize = true;
            this.lblTitle10.Location = new System.Drawing.Point(228, 17);
            this.lblTitle10.Name = "lblTitle10";
            this.lblTitle10.Size = new System.Drawing.Size(77, 14);
            this.lblTitle10.TabIndex = 299;
            this.lblTitle10.Text = "FiO  值：";
            // 
            // lblTitle12
            // 
            this.lblTitle12.AutoSize = true;
            this.lblTitle12.Location = new System.Drawing.Point(308, 68);
            this.lblTitle12.Name = "lblTitle12";
            this.lblTitle12.Size = new System.Drawing.Size(39, 14);
            this.lblTitle12.TabIndex = 298;
            this.lblTitle12.Text = "mmHg";
            // 
            // lblTitle13
            // 
            this.lblTitle13.AutoSize = true;
            this.lblTitle13.Location = new System.Drawing.Point(308, 38);
            this.lblTitle13.Name = "lblTitle13";
            this.lblTitle13.Size = new System.Drawing.Size(31, 14);
            this.lblTitle13.TabIndex = 298;
            this.lblTitle13.Text = "kPa";
            // 
            // txtFio2mmHg
            // 
            this.txtFio2mmHg.BackColor = System.Drawing.Color.White;
            this.txtFio2mmHg.BorderColor = System.Drawing.Color.Black;
            this.txtFio2mmHg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFio2mmHg.Enabled = false;
            this.txtFio2mmHg.ForeColor = System.Drawing.Color.Black;
            this.txtFio2mmHg.Location = new System.Drawing.Point(204, 65);
            this.txtFio2mmHg.MaxLength = 10;
            this.txtFio2mmHg.Name = "txtFio2mmHg";
            this.txtFio2mmHg.Size = new System.Drawing.Size(100, 23);
            this.txtFio2mmHg.TabIndex = 100;
            this.txtFio2mmHg.Tag = "1";
            this.txtFio2mmHg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTitle22
            // 
            this.lblTitle22.AutoSize = true;
            this.lblTitle22.Location = new System.Drawing.Point(173, 68);
            this.lblTitle22.Name = "lblTitle22";
            this.lblTitle22.Size = new System.Drawing.Size(22, 14);
            this.lblTitle22.TabIndex = 302;
            this.lblTitle22.Text = "÷";
            // 
            // lblTitle23
            // 
            this.lblTitle23.AutoSize = true;
            this.lblTitle23.Location = new System.Drawing.Point(380, 68);
            this.lblTitle23.Name = "lblTitle23";
            this.lblTitle23.Size = new System.Drawing.Size(22, 14);
            this.lblTitle23.TabIndex = 303;
            this.lblTitle23.Text = "＝";
            // 
            // txtOxyGenValmmHg
            // 
            this.txtOxyGenValmmHg.BackColor = System.Drawing.Color.White;
            this.txtOxyGenValmmHg.BorderColor = System.Drawing.Color.Black;
            this.txtOxyGenValmmHg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOxyGenValmmHg.Enabled = false;
            this.txtOxyGenValmmHg.ForeColor = System.Drawing.Color.Black;
            this.txtOxyGenValmmHg.Location = new System.Drawing.Point(424, 65);
            this.txtOxyGenValmmHg.MaxLength = 10;
            this.txtOxyGenValmmHg.Name = "txtOxyGenValmmHg";
            this.txtOxyGenValmmHg.ReadOnly = true;
            this.txtOxyGenValmmHg.Size = new System.Drawing.Size(144, 23);
            this.txtOxyGenValmmHg.TabIndex = 110;
            this.txtOxyGenValmmHg.TabStop = false;
            this.txtOxyGenValmmHg.Tag = "1";
            // 
            // lblTitle14
            // 
            this.lblTitle14.AutoSize = true;
            this.lblTitle14.Location = new System.Drawing.Point(424, 17);
            this.lblTitle14.Name = "lblTitle14";
            this.lblTitle14.Size = new System.Drawing.Size(133, 14);
            this.lblTitle14.TabIndex = 300;
            this.lblTitle14.Text = "PaO  /FiO   值：";
            // 
            // lblTitle17
            // 
            this.lblTitle17.AutoSize = true;
            this.lblTitle17.Location = new System.Drawing.Point(8, 52);
            this.lblTitle17.Name = "lblTitle17";
            this.lblTitle17.Size = new System.Drawing.Size(77, 14);
            this.lblTitle17.TabIndex = 302;
            this.lblTitle17.Text = "PEEP 值：";
            this.lblTitle17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPEEP
            // 
            this.txtPEEP.BackColor = System.Drawing.Color.White;
            this.txtPEEP.BorderColor = System.Drawing.Color.Black;
            this.txtPEEP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPEEP.ForeColor = System.Drawing.Color.Black;
            this.txtPEEP.Location = new System.Drawing.Point(80, 49);
            this.txtPEEP.MaxLength = 4;
            this.txtPEEP.Name = "txtPEEP";
            this.txtPEEP.Size = new System.Drawing.Size(118, 23);
            this.txtPEEP.TabIndex = 120;
            this.txtPEEP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPEEP.TextChanged += new System.EventHandler(this.MakePEEPSame);
            // 
            // lblTitle18
            // 
            this.lblTitle18.AllowDrop = true;
            this.lblTitle18.AutoSize = true;
            this.lblTitle18.Location = new System.Drawing.Point(200, 52);
            this.lblTitle18.Name = "lblTitle18";
            this.lblTitle18.Size = new System.Drawing.Size(55, 14);
            this.lblTitle18.TabIndex = 304;
            this.lblTitle18.Text = "cmH  O";
            // 
            // lblTitle19
            // 
            this.lblTitle19.AutoSize = true;
            this.lblTitle19.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle19.Location = new System.Drawing.Point(232, 54);
            this.lblTitle19.Name = "lblTitle19";
            this.lblTitle19.Size = new System.Drawing.Size(10, 10);
            this.lblTitle19.TabIndex = 296;
            this.lblTitle19.Text = "2";
            // 
            // lblTitle24
            // 
            this.lblTitle24.AutoSize = true;
            this.lblTitle24.Location = new System.Drawing.Point(156, 24);
            this.lblTitle24.Name = "lblTitle24";
            this.lblTitle24.Size = new System.Drawing.Size(113, 14);
            this.lblTitle24.TabIndex = 305;
            this.lblTitle24.Text = "潮气量（Vt）：";
            this.lblTitle24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle25
            // 
            this.lblTitle25.AutoSize = true;
            this.lblTitle25.Location = new System.Drawing.Point(288, 24);
            this.lblTitle25.Name = "lblTitle25";
            this.lblTitle25.Size = new System.Drawing.Size(136, 14);
            this.lblTitle25.TabIndex = 306;
            this.lblTitle25.Text = "气道峰压（PIP）：";
            this.lblTitle25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle26
            // 
            this.lblTitle26.AutoSize = true;
            this.lblTitle26.Location = new System.Drawing.Point(424, 24);
            this.lblTitle26.Name = "lblTitle26";
            this.lblTitle26.Size = new System.Drawing.Size(152, 14);
            this.lblTitle26.TabIndex = 307;
            this.lblTitle26.Text = "呼气末正压（PEEP）:";
            this.lblTitle26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle27
            // 
            this.lblTitle27.AutoSize = true;
            this.lblTitle27.Location = new System.Drawing.Point(24, 24);
            this.lblTitle27.Name = "lblTitle27";
            this.lblTitle27.Size = new System.Drawing.Size(112, 14);
            this.lblTitle27.TabIndex = 308;
            this.lblTitle27.Text = "肺系统顺应性：";
            this.lblTitle27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtVt
            // 
            this.txtVt.BackColor = System.Drawing.Color.White;
            this.txtVt.BorderColor = System.Drawing.Color.Black;
            this.txtVt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVt.ForeColor = System.Drawing.Color.Black;
            this.txtVt.Location = new System.Drawing.Point(156, 52);
            this.txtVt.MaxLength = 10;
            this.txtVt.Name = "txtVt";
            this.txtVt.Size = new System.Drawing.Size(93, 23);
            this.txtVt.TabIndex = 140;
            // 
            // txtPIP
            // 
            this.txtPIP.BackColor = System.Drawing.Color.White;
            this.txtPIP.BorderColor = System.Drawing.Color.Black;
            this.txtPIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPIP.ForeColor = System.Drawing.Color.Black;
            this.txtPIP.Location = new System.Drawing.Point(296, 52);
            this.txtPIP.MaxLength = 4;
            this.txtPIP.Name = "txtPIP";
            this.txtPIP.Size = new System.Drawing.Size(104, 23);
            this.txtPIP.TabIndex = 150;
            // 
            // txtLungPEEP
            // 
            this.txtLungPEEP.BackColor = System.Drawing.Color.White;
            this.txtLungPEEP.BorderColor = System.Drawing.Color.Black;
            this.txtLungPEEP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLungPEEP.ForeColor = System.Drawing.Color.Black;
            this.txtLungPEEP.Location = new System.Drawing.Point(424, 52);
            this.txtLungPEEP.MaxLength = 10;
            this.txtLungPEEP.Name = "txtLungPEEP";
            this.txtLungPEEP.Size = new System.Drawing.Size(144, 23);
            this.txtLungPEEP.TabIndex = 160;
            this.txtLungPEEP.TextChanged += new System.EventHandler(this.MakePEEPSame);
            // 
            // txtLungSysHumorVal
            // 
            this.txtLungSysHumorVal.BackColor = System.Drawing.Color.White;
            this.txtLungSysHumorVal.BorderColor = System.Drawing.Color.Black;
            this.txtLungSysHumorVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLungSysHumorVal.ForeColor = System.Drawing.Color.Black;
            this.txtLungSysHumorVal.Location = new System.Drawing.Point(32, 52);
            this.txtLungSysHumorVal.MaxLength = 10;
            this.txtLungSysHumorVal.Name = "txtLungSysHumorVal";
            this.txtLungSysHumorVal.Size = new System.Drawing.Size(92, 23);
            this.txtLungSysHumorVal.TabIndex = 130;
            // 
            // lblTitle28
            // 
            this.lblTitle28.AutoSize = true;
            this.lblTitle28.Location = new System.Drawing.Point(256, 55);
            this.lblTitle28.Name = "lblTitle28";
            this.lblTitle28.Size = new System.Drawing.Size(38, 14);
            this.lblTitle28.TabIndex = 302;
            this.lblTitle28.Text = "÷ [";
            // 
            // lblTitle29
            // 
            this.lblTitle29.Location = new System.Drawing.Point(404, 58);
            this.lblTitle29.Name = "lblTitle29";
            this.lblTitle29.Size = new System.Drawing.Size(16, 13);
            this.lblTitle29.TabIndex = 310;
            this.lblTitle29.Text = "－";
            // 
            // lblTitle30
            // 
            this.lblTitle30.AutoSize = true;
            this.lblTitle30.Location = new System.Drawing.Point(566, 56);
            this.lblTitle30.Name = "lblTitle30";
            this.lblTitle30.Size = new System.Drawing.Size(15, 14);
            this.lblTitle30.TabIndex = 303;
            this.lblTitle30.Text = "]";
            // 
            // lblTitle35
            // 
            this.lblTitle35.Location = new System.Drawing.Point(124, 56);
            this.lblTitle35.Name = "lblTitle35";
            this.lblTitle35.Size = new System.Drawing.Size(24, 17);
            this.lblTitle35.TabIndex = 303;
            this.lblTitle35.Text = "＝";
            // 
            // dtgResult
            // 
            this.dtgResult.BackColor = System.Drawing.Color.White;
            this.dtgResult.BackgroundColor = System.Drawing.Color.White;
            this.dtgResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtgResult.CaptionBackColor = System.Drawing.Color.White;
            this.dtgResult.CaptionForeColor = System.Drawing.Color.Black;
            this.dtgResult.CaptionText = "急性肺损伤评分结果";
            this.dtgResult.DataMember = "";
            this.dtgResult.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dtgResult.Location = new System.Drawing.Point(4, 357);
            this.dtgResult.Name = "dtgResult";
            this.dtgResult.ReadOnly = true;
            this.dtgResult.Size = new System.Drawing.Size(843, 120);
            this.dtgResult.TabIndex = 314;
            this.dtgResult.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.dtgResult.TabStop = false;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.AllowSorting = false;
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
            this.dataGridTextBoxColumn5.HeaderText = "总分";
            this.dataGridTextBoxColumn5.MappingName = "总分";
            this.dataGridTextBoxColumn5.NullText = "/";
            this.dataGridTextBoxColumn5.Width = 250;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "胸部 X- 光照片评分";
            this.dataGridTextBoxColumn1.MappingName = "胸部 X- 光照片评分";
            this.dataGridTextBoxColumn1.NullText = "/";
            this.dataGridTextBoxColumn1.Width = 140;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "低氧血症评分";
            this.dataGridTextBoxColumn2.MappingName = "低氧血症评分";
            this.dataGridTextBoxColumn2.NullText = "/";
            this.dataGridTextBoxColumn2.Width = 120;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "PEEP评分（机械通气性评分）";
            this.dataGridTextBoxColumn3.MappingName = "PEEP评分（机械通气性评分）";
            this.dataGridTextBoxColumn3.NullText = "/";
            this.dataGridTextBoxColumn3.Width = 180;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "肺系统顺应性评分";
            this.dataGridTextBoxColumn4.MappingName = "肺系统顺应性评分";
            this.dataGridTextBoxColumn4.NullText = "/";
            this.dataGridTextBoxColumn4.Width = 120;
            // 
            // lblTitle31
            // 
            this.lblTitle31.AutoSize = true;
            this.lblTitle31.Location = new System.Drawing.Point(308, 317);
            this.lblTitle31.Name = "lblTitle31";
            this.lblTitle31.Size = new System.Drawing.Size(67, 14);
            this.lblTitle31.TabIndex = 315;
            this.lblTitle31.Text = "评估者：";
            this.lblTitle31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle96
            // 
            this.lblTitle96.AutoSize = true;
            this.lblTitle96.Location = new System.Drawing.Point(5, 20);
            this.lblTitle96.Name = "lblTitle96";
            this.lblTitle96.Size = new System.Drawing.Size(82, 14);
            this.lblTitle96.TabIndex = 412;
            this.lblTitle96.Text = "采集时间：";
            this.lblTitle96.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(392, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 14);
            this.label2.TabIndex = 407;
            this.label2.Text = "评分间隔(&秒)：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAutoTime
            // 
            this.txtAutoTime.BackColor = System.Drawing.Color.White;
            this.txtAutoTime.BorderColor = System.Drawing.Color.Black;
            this.txtAutoTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAutoTime.ForeColor = System.Drawing.Color.Black;
            this.txtAutoTime.Location = new System.Drawing.Point(495, 17);
            this.txtAutoTime.MaxLength = 10;
            this.txtAutoTime.Name = "txtAutoTime";
            this.txtAutoTime.Size = new System.Drawing.Size(45, 23);
            this.txtAutoTime.TabIndex = 210;
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
            this.dtpStartSample.Location = new System.Drawing.Point(80, 18);
            this.dtpStartSample.m_BlnOnlyTime = false;
            this.dtpStartSample.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpStartSample.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartSample.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartSample.Name = "dtpStartSample";
            this.dtpStartSample.ReadOnly = false;
            this.dtpStartSample.Size = new System.Drawing.Size(216, 22);
            this.dtpStartSample.TabIndex = 190;
            this.dtpStartSample.TextBackColor = System.Drawing.Color.White;
            this.dtpStartSample.TextForeColor = System.Drawing.Color.Black;
            // 
            // lbltxtEvalDoctor
            // 
            this.lbltxtEvalDoctor.Location = new System.Drawing.Point(384, 317);
            this.lbltxtEvalDoctor.Name = "lbltxtEvalDoctor";
            this.lbltxtEvalDoctor.Size = new System.Drawing.Size(116, 19);
            this.lbltxtEvalDoctor.TabIndex = 435;
            this.lbltxtEvalDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gpbAutoEval
            // 
            this.gpbAutoEval.Controls.Add(this.cmdGetData);
            this.gpbAutoEval.Controls.Add(this.txtAutoTime);
            this.gpbAutoEval.Controls.Add(this.dtpStartSample);
            this.gpbAutoEval.Controls.Add(this.label2);
            this.gpbAutoEval.Controls.Add(this.lblTitle96);
            this.gpbAutoEval.Controls.Add(this.cmdStartAuto);
            this.gpbAutoEval.Controls.Add(this.cmdStopAuto);
            this.gpbAutoEval.Controls.Add(this.cmdShowResult);
            this.gpbAutoEval.Location = new System.Drawing.Point(4, 489);
            this.gpbAutoEval.Name = "gpbAutoEval";
            this.gpbAutoEval.Size = new System.Drawing.Size(843, 60);
            this.gpbAutoEval.TabIndex = 189;
            this.gpbAutoEval.TabStop = false;
            this.gpbAutoEval.Text = "自动评分";
            // 
            // cmdGetData
            // 
            this.cmdGetData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdGetData.DefaultScheme = true;
            this.cmdGetData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdGetData.ForeColor = System.Drawing.Color.Black;
            this.cmdGetData.Hint = "";
            this.cmdGetData.Location = new System.Drawing.Point(300, 13);
            this.cmdGetData.Name = "cmdGetData";
            this.cmdGetData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdGetData.Size = new System.Drawing.Size(92, 32);
            this.cmdGetData.TabIndex = 10000006;
            this.cmdGetData.Text = "获取数据(&G)";
            this.cmdGetData.Click += new System.EventHandler(this.cmdGetData_Click);
            // 
            // cmdStartAuto
            // 
            this.cmdStartAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdStartAuto.DefaultScheme = true;
            this.cmdStartAuto.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdStartAuto.ForeColor = System.Drawing.Color.Black;
            this.cmdStartAuto.Hint = "";
            this.cmdStartAuto.Location = new System.Drawing.Point(547, 13);
            this.cmdStartAuto.Name = "cmdStartAuto";
            this.cmdStartAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStartAuto.Size = new System.Drawing.Size(92, 32);
            this.cmdStartAuto.TabIndex = 10000005;
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
            this.cmdStopAuto.Location = new System.Drawing.Point(642, 13);
            this.cmdStopAuto.Name = "cmdStopAuto";
            this.cmdStopAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStopAuto.Size = new System.Drawing.Size(92, 32);
            this.cmdStopAuto.TabIndex = 10000008;
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
            this.cmdShowResult.Location = new System.Drawing.Point(739, 13);
            this.cmdShowResult.Name = "cmdShowResult";
            this.cmdShowResult.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdShowResult.Size = new System.Drawing.Size(92, 32);
            this.cmdShowResult.TabIndex = 10000007;
            this.cmdShowResult.Text = "查看结果(&R)";
            this.cmdShowResult.Click += new System.EventHandler(this.cmdShowResult_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(280, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 14);
            this.label1.TabIndex = 449;
            this.label1.Text = "[";
            // 
            // cmdCalculate
            // 
            this.cmdCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdCalculate.DefaultScheme = true;
            this.cmdCalculate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdCalculate.ForeColor = System.Drawing.Color.Black;
            this.cmdCalculate.Hint = "";
            this.cmdCalculate.Location = new System.Drawing.Point(752, 310);
            this.cmdCalculate.Name = "cmdCalculate";
            this.cmdCalculate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCalculate.Size = new System.Drawing.Size(91, 32);
            this.cmdCalculate.TabIndex = 10000006;
            this.cmdCalculate.Text = "计算分值(&E)";
            this.cmdCalculate.Click += new System.EventHandler(this.cmdCalculate_Click);
            // 
            // m_cmdGetCheckData
            // 
            this.m_cmdGetCheckData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdGetCheckData.DefaultScheme = true;
            this.m_cmdGetCheckData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGetCheckData.ForeColor = System.Drawing.Color.Black;
            this.m_cmdGetCheckData.Hint = "";
            this.m_cmdGetCheckData.Location = new System.Drawing.Point(528, 312);
            this.m_cmdGetCheckData.Name = "m_cmdGetCheckData";
            this.m_cmdGetCheckData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetCheckData.Size = new System.Drawing.Size(140, 28);
            this.m_cmdGetCheckData.TabIndex = 10000015;
            this.m_cmdGetCheckData.Text = "获取检验结果(&L)";
            this.m_cmdGetCheckData.Click += new System.EventHandler(this.m_cmdGetCheckData_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboXRayEval);
            this.groupBox2.Location = new System.Drawing.Point(4, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(256, 100);
            this.groupBox2.TabIndex = 10000016;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "1.胸部 X- 光照片";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblTitle19);
            this.groupBox3.Controls.Add(this.lblTitle18);
            this.groupBox3.Controls.Add(this.lblTitle17);
            this.groupBox3.Controls.Add(this.txtPEEP);
            this.groupBox3.Location = new System.Drawing.Point(4, 217);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(256, 88);
            this.groupBox3.TabIndex = 10000017;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "3.PEEP（机械通气者）";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtLungPEEP);
            this.groupBox4.Controls.Add(this.lblTitle29);
            this.groupBox4.Controls.Add(this.lblTitle35);
            this.groupBox4.Controls.Add(this.lblTitle24);
            this.groupBox4.Controls.Add(this.txtLungSysHumorVal);
            this.groupBox4.Controls.Add(this.lblTitle26);
            this.groupBox4.Controls.Add(this.txtVt);
            this.groupBox4.Controls.Add(this.lblTitle25);
            this.groupBox4.Controls.Add(this.txtPIP);
            this.groupBox4.Controls.Add(this.lblTitle30);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.lblTitle28);
            this.groupBox4.Controls.Add(this.lblTitle27);
            this.groupBox4.Location = new System.Drawing.Point(268, 217);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(580, 88);
            this.groupBox4.TabIndex = 10000018;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "4.肺系统顺应性";
            // 
            // frmLungInjuryEvaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.ClientSize = new System.Drawing.Size(870, 606);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.m_cmdGetCheckData);
            this.Controls.Add(this.dtpEvalDate);
            this.Controls.Add(this.lblTitle31);
            this.Controls.Add(this.lblEvalDate);
            this.Controls.Add(this.gpbAutoEval);
            this.Controls.Add(this.lbltxtEvalDoctor);
            this.Controls.Add(this.dtgResult);
            this.Controls.Add(this.gpbLowOxygenBlood);
            this.Controls.Add(this.cmdCalculate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLungInjuryEvaluation";
            this.Text = "急性肺损伤评分";
            this.Load += new System.EventHandler(this.LungInjuryEvaluation_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.LungInjuryEvaluation_Closing);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.cmdCalculate, 0);
            this.Controls.SetChildIndex(this.gpbLowOxygenBlood, 0);
            this.Controls.SetChildIndex(this.dtgResult, 0);
            this.Controls.SetChildIndex(this.lbltxtEvalDoctor, 0);
            this.Controls.SetChildIndex(this.gpbAutoEval, 0);
            this.Controls.SetChildIndex(this.lblEvalDate, 0);
            this.Controls.SetChildIndex(this.lblTitle31, 0);
            this.Controls.SetChildIndex(this.dtpEvalDate, 0);
            this.Controls.SetChildIndex(this.m_cmdGetCheckData, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.gpbLowOxygenBlood.ResumeLayout(false);
            this.gpbLowOxygenBlood.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).EndInit();
            this.gpbAutoEval.ResumeLayout(false);
            this.gpbAutoEval.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}
