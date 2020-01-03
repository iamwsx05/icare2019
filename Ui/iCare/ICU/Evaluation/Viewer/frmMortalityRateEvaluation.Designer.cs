using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iCare.ICU.Evaluation
{
    partial class frmMortalityRateEvaluation
    {
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

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timAutoCollect = new System.Timers.Timer();
            this.gpbEvaluation = new System.Windows.Forms.GroupBox();
            this.cmdStartAuto = new PinkieControls.ButtonXP();
            this.dtpStartSample = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblTitle96 = new System.Windows.Forms.Label();
            this.txtAutoTime = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmdStopAuto = new PinkieControls.ButtonXP();
            this.cmdShowResult = new PinkieControls.ButtonXP();
            this.cmdGetData = new PinkieControls.ButtonXP();
            this.lblTitle31 = new System.Windows.Forms.Label();
            this.lblEvalDate = new System.Windows.Forms.Label();
            this.lbltxtEvalDoctor = new System.Windows.Forms.Label();
            this.m_dtgResult = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn7 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn8 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtpEvalDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.m_lblDHS = new System.Windows.Forms.Label();
            this.m_cboHM = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_cboInfect = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_lblYYZ = new System.Windows.Forms.Label();
            this.m_cboCancer = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_cboJZ = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_lblFS = new System.Windows.Forms.Label();
            this.m_lblBDB = new System.Windows.Forms.Label();
            this.m_cboSJ = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtSBP = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdCalculate = new PinkieControls.ButtonXP();
            this.m_cmdGetCheckData = new PinkieControls.ButtonXP();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_pnlNewBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).BeginInit();
            this.gpbEvaluation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgResult)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            // timAutoCollect
            // 
            this.timAutoCollect.Interval = 60000;
            this.timAutoCollect.SynchronizingObject = this;
            this.timAutoCollect.Elapsed += new System.Timers.ElapsedEventHandler(this.timAutoCollect_Elapsed);
            // 
            // gpbEvaluation
            // 
            this.gpbEvaluation.Controls.Add(this.cmdStartAuto);
            this.gpbEvaluation.Controls.Add(this.dtpStartSample);
            this.gpbEvaluation.Controls.Add(this.lblTitle96);
            this.gpbEvaluation.Controls.Add(this.txtAutoTime);
            this.gpbEvaluation.Controls.Add(this.label10);
            this.gpbEvaluation.Controls.Add(this.cmdStopAuto);
            this.gpbEvaluation.Controls.Add(this.cmdShowResult);
            this.gpbEvaluation.Controls.Add(this.cmdGetData);
            this.gpbEvaluation.Location = new System.Drawing.Point(12, 414);
            this.gpbEvaluation.Name = "gpbEvaluation";
            this.gpbEvaluation.Size = new System.Drawing.Size(832, 64);
            this.gpbEvaluation.TabIndex = 39;
            this.gpbEvaluation.TabStop = false;
            this.gpbEvaluation.Text = "自动评分";
            // 
            // cmdStartAuto
            // 
            this.cmdStartAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdStartAuto.DefaultScheme = true;
            this.cmdStartAuto.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdStartAuto.ForeColor = System.Drawing.Color.Black;
            this.cmdStartAuto.Hint = "";
            this.cmdStartAuto.Location = new System.Drawing.Point(548, 20);
            this.cmdStartAuto.Name = "cmdStartAuto";
            this.cmdStartAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStartAuto.Size = new System.Drawing.Size(88, 32);
            this.cmdStartAuto.TabIndex = 10000014;
            this.cmdStartAuto.Text = "自动评分(&A)";
            this.cmdStartAuto.Click += new System.EventHandler(this.cmdStartAuto_Click);
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
            this.dtpStartSample.Location = new System.Drawing.Point(80, 25);
            this.dtpStartSample.m_BlnOnlyTime = false;
            this.dtpStartSample.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpStartSample.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartSample.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartSample.Name = "dtpStartSample";
            this.dtpStartSample.ReadOnly = false;
            this.dtpStartSample.Size = new System.Drawing.Size(222, 22);
            this.dtpStartSample.TabIndex = 1;
            this.dtpStartSample.TextBackColor = System.Drawing.Color.White;
            this.dtpStartSample.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblTitle96
            // 
            this.lblTitle96.AutoSize = true;
            this.lblTitle96.BackColor = System.Drawing.SystemColors.Control;
            this.lblTitle96.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle96.ForeColor = System.Drawing.Color.Black;
            this.lblTitle96.Location = new System.Drawing.Point(5, 27);
            this.lblTitle96.Name = "lblTitle96";
            this.lblTitle96.Size = new System.Drawing.Size(82, 14);
            this.lblTitle96.TabIndex = 0;
            this.lblTitle96.Text = "采集时间：";
            this.lblTitle96.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAutoTime
            // 
            this.txtAutoTime.BackColor = System.Drawing.Color.White;
            this.txtAutoTime.BorderColor = System.Drawing.Color.Black;
            this.txtAutoTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAutoTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAutoTime.ForeColor = System.Drawing.Color.Black;
            this.txtAutoTime.Location = new System.Drawing.Point(508, 25);
            this.txtAutoTime.MaxLength = 10;
            this.txtAutoTime.Name = "txtAutoTime";
            this.txtAutoTime.Size = new System.Drawing.Size(37, 23);
            this.txtAutoTime.TabIndex = 4;
            this.txtAutoTime.Text = "60";
            this.txtAutoTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAutoTime.TextChanged += new System.EventHandler(this.txtAutoTime_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.Control;
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(404, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 14);
            this.label10.TabIndex = 3;
            this.label10.Text = "评分间隔(秒)：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdStopAuto
            // 
            this.cmdStopAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdStopAuto.DefaultScheme = true;
            this.cmdStopAuto.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdStopAuto.ForeColor = System.Drawing.Color.Black;
            this.cmdStopAuto.Hint = "";
            this.cmdStopAuto.Location = new System.Drawing.Point(636, 20);
            this.cmdStopAuto.Name = "cmdStopAuto";
            this.cmdStopAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStopAuto.Size = new System.Drawing.Size(88, 32);
            this.cmdStopAuto.TabIndex = 10000017;
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
            this.cmdShowResult.Location = new System.Drawing.Point(732, 20);
            this.cmdShowResult.Name = "cmdShowResult";
            this.cmdShowResult.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdShowResult.Size = new System.Drawing.Size(92, 32);
            this.cmdShowResult.TabIndex = 10000016;
            this.cmdShowResult.Text = "查看结果(&R)";
            this.cmdShowResult.Click += new System.EventHandler(this.cmdShowResult_Click);
            // 
            // cmdGetData
            // 
            this.cmdGetData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdGetData.DefaultScheme = true;
            this.cmdGetData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdGetData.ForeColor = System.Drawing.Color.Black;
            this.cmdGetData.Hint = "";
            this.cmdGetData.Location = new System.Drawing.Point(312, 20);
            this.cmdGetData.Name = "cmdGetData";
            this.cmdGetData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdGetData.Size = new System.Drawing.Size(92, 32);
            this.cmdGetData.TabIndex = 10000015;
            this.cmdGetData.Text = "获取数据(&G)";
            this.cmdGetData.Click += new System.EventHandler(this.cmdGetData_Click);
            // 
            // lblTitle31
            // 
            this.lblTitle31.AutoSize = true;
            this.lblTitle31.BackColor = System.Drawing.SystemColors.Control;
            this.lblTitle31.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle31.ForeColor = System.Drawing.Color.Black;
            this.lblTitle31.Location = new System.Drawing.Point(388, 262);
            this.lblTitle31.Name = "lblTitle31";
            this.lblTitle31.Size = new System.Drawing.Size(67, 14);
            this.lblTitle31.TabIndex = 35;
            this.lblTitle31.Text = "评估者：";
            this.lblTitle31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEvalDate
            // 
            this.lblEvalDate.AutoSize = true;
            this.lblEvalDate.BackColor = System.Drawing.SystemColors.Control;
            this.lblEvalDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEvalDate.ForeColor = System.Drawing.Color.Black;
            this.lblEvalDate.Location = new System.Drawing.Point(24, 262);
            this.lblEvalDate.Name = "lblEvalDate";
            this.lblEvalDate.Size = new System.Drawing.Size(82, 14);
            this.lblEvalDate.TabIndex = 33;
            this.lblEvalDate.Text = "评分日期：";
            this.lblEvalDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbltxtEvalDoctor
            // 
            this.lbltxtEvalDoctor.BackColor = System.Drawing.SystemColors.Control;
            this.lbltxtEvalDoctor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbltxtEvalDoctor.ForeColor = System.Drawing.Color.Black;
            this.lbltxtEvalDoctor.Location = new System.Drawing.Point(456, 262);
            this.lbltxtEvalDoctor.Name = "lbltxtEvalDoctor";
            this.lbltxtEvalDoctor.Size = new System.Drawing.Size(120, 19);
            this.lbltxtEvalDoctor.TabIndex = 36;
            this.lbltxtEvalDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_dtgResult
            // 
            this.m_dtgResult.BackColor = System.Drawing.Color.White;
            this.m_dtgResult.BackgroundColor = System.Drawing.Color.White;
            this.m_dtgResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_dtgResult.CaptionBackColor = System.Drawing.Color.White;
            this.m_dtgResult.CaptionForeColor = System.Drawing.Color.Black;
            this.m_dtgResult.CaptionText = "MortalityRate评分结果";
            this.m_dtgResult.DataMember = "";
            this.m_dtgResult.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.m_dtgResult.Location = new System.Drawing.Point(8, 298);
            this.m_dtgResult.Name = "m_dtgResult";
            this.m_dtgResult.ReadOnly = true;
            this.m_dtgResult.Size = new System.Drawing.Size(836, 104);
            this.m_dtgResult.TabIndex = 38;
            this.m_dtgResult.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.m_dtgResult.TabStop = false;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.m_dtgResult;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn7,
            this.dataGridTextBoxColumn8,
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn5,
            this.dataGridTextBoxColumn6});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "result";
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = "死亡率";
            this.dataGridTextBoxColumn7.MappingName = "死亡率";
            this.dataGridTextBoxColumn7.Width = 75;
            // 
            // dataGridTextBoxColumn8
            // 
            this.dataGridTextBoxColumn8.Format = "";
            this.dataGridTextBoxColumn8.FormatInfo = null;
            this.dataGridTextBoxColumn8.HeaderText = "昏迷或深度木僵";
            this.dataGridTextBoxColumn8.MappingName = "昏迷或深度木僵";
            this.dataGridTextBoxColumn8.Width = 75;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "急诊入ICU";
            this.dataGridTextBoxColumn1.MappingName = "急诊入ICU";
            this.dataGridTextBoxColumn1.Width = 75;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "癌症";
            this.dataGridTextBoxColumn2.MappingName = "癌症";
            this.dataGridTextBoxColumn2.Width = 75;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "是否感染";
            this.dataGridTextBoxColumn3.MappingName = "是否感染";
            this.dataGridTextBoxColumn3.Width = 75;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "器官系统衰竭数量";
            this.dataGridTextBoxColumn4.MappingName = "器官系统衰竭数量";
            this.dataGridTextBoxColumn4.Width = 75;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "SBP";
            this.dataGridTextBoxColumn5.MappingName = "SBP";
            this.dataGridTextBoxColumn5.Width = 75;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "SBP2";
            this.dataGridTextBoxColumn6.MappingName = "SBP2";
            this.dataGridTextBoxColumn6.Width = 75;
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
            this.dtpEvalDate.Location = new System.Drawing.Point(100, 260);
            this.dtpEvalDate.m_BlnOnlyTime = false;
            this.dtpEvalDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpEvalDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEvalDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEvalDate.Name = "dtpEvalDate";
            this.dtpEvalDate.ReadOnly = false;
            this.dtpEvalDate.Size = new System.Drawing.Size(220, 22);
            this.dtpEvalDate.TabIndex = 34;
            this.dtpEvalDate.TextBackColor = System.Drawing.Color.White;
            this.dtpEvalDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "";
            // 
            // m_lblDHS
            // 
            this.m_lblDHS.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblDHS.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblDHS.ForeColor = System.Drawing.Color.Black;
            this.m_lblDHS.Location = new System.Drawing.Point(20, 28);
            this.m_lblDHS.Name = "m_lblDHS";
            this.m_lblDHS.Size = new System.Drawing.Size(136, 20);
            this.m_lblDHS.TabIndex = 26;
            this.m_lblDHS.Text = "昏迷或深度木僵";
            this.m_lblDHS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cboHM
            // 
            this.m_cboHM.BackColor = System.Drawing.Color.White;
            this.m_cboHM.BorderColor = System.Drawing.Color.Black;
            this.m_cboHM.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboHM.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboHM.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboHM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboHM.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboHM.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboHM.ForeColor = System.Drawing.Color.Black;
            this.m_cboHM.ListBackColor = System.Drawing.Color.White;
            this.m_cboHM.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboHM.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboHM.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboHM.Location = new System.Drawing.Point(164, 28);
            this.m_cboHM.m_BlnEnableItemEventMenu = true;
            this.m_cboHM.MaxLength = 32767;
            this.m_cboHM.Name = "m_cboHM";
            this.m_cboHM.SelectedIndex = -1;
            this.m_cboHM.SelectedItem = null;
            this.m_cboHM.SelectionStart = 0;
            this.m_cboHM.Size = new System.Drawing.Size(148, 23);
            this.m_cboHM.TabIndex = 27;
            this.m_cboHM.TextBackColor = System.Drawing.Color.White;
            this.m_cboHM.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_cboInfect
            // 
            this.m_cboInfect.BackColor = System.Drawing.Color.White;
            this.m_cboInfect.BorderColor = System.Drawing.Color.Black;
            this.m_cboInfect.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboInfect.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboInfect.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboInfect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboInfect.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboInfect.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboInfect.ForeColor = System.Drawing.Color.Black;
            this.m_cboInfect.ListBackColor = System.Drawing.Color.White;
            this.m_cboInfect.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboInfect.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboInfect.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboInfect.Location = new System.Drawing.Point(180, 28);
            this.m_cboInfect.m_BlnEnableItemEventMenu = true;
            this.m_cboInfect.MaxLength = 32767;
            this.m_cboInfect.Name = "m_cboInfect";
            this.m_cboInfect.SelectedIndex = -1;
            this.m_cboInfect.SelectedItem = null;
            this.m_cboInfect.SelectionStart = 0;
            this.m_cboInfect.Size = new System.Drawing.Size(132, 23);
            this.m_cboInfect.TabIndex = 27;
            this.m_cboInfect.TextBackColor = System.Drawing.Color.White;
            this.m_cboInfect.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_lblYYZ
            // 
            this.m_lblYYZ.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblYYZ.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblYYZ.ForeColor = System.Drawing.Color.Black;
            this.m_lblYYZ.Location = new System.Drawing.Point(16, 28);
            this.m_lblYYZ.Name = "m_lblYYZ";
            this.m_lblYYZ.Size = new System.Drawing.Size(88, 20);
            this.m_lblYYZ.TabIndex = 26;
            this.m_lblYYZ.Text = "感染";
            this.m_lblYYZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cboCancer
            // 
            this.m_cboCancer.BackColor = System.Drawing.Color.White;
            this.m_cboCancer.BorderColor = System.Drawing.Color.Black;
            this.m_cboCancer.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboCancer.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboCancer.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboCancer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboCancer.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboCancer.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboCancer.ForeColor = System.Drawing.Color.Black;
            this.m_cboCancer.ListBackColor = System.Drawing.Color.White;
            this.m_cboCancer.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboCancer.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboCancer.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboCancer.Location = new System.Drawing.Point(164, 100);
            this.m_cboCancer.m_BlnEnableItemEventMenu = true;
            this.m_cboCancer.MaxLength = 32767;
            this.m_cboCancer.Name = "m_cboCancer";
            this.m_cboCancer.SelectedIndex = -1;
            this.m_cboCancer.SelectedItem = null;
            this.m_cboCancer.SelectionStart = 0;
            this.m_cboCancer.Size = new System.Drawing.Size(148, 23);
            this.m_cboCancer.TabIndex = 27;
            this.m_cboCancer.TextBackColor = System.Drawing.Color.White;
            this.m_cboCancer.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_cboJZ
            // 
            this.m_cboJZ.BackColor = System.Drawing.Color.White;
            this.m_cboJZ.BorderColor = System.Drawing.Color.Black;
            this.m_cboJZ.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboJZ.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboJZ.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboJZ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboJZ.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboJZ.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboJZ.ForeColor = System.Drawing.Color.Black;
            this.m_cboJZ.ListBackColor = System.Drawing.Color.White;
            this.m_cboJZ.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboJZ.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboJZ.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboJZ.Location = new System.Drawing.Point(164, 64);
            this.m_cboJZ.m_BlnEnableItemEventMenu = true;
            this.m_cboJZ.MaxLength = 32767;
            this.m_cboJZ.Name = "m_cboJZ";
            this.m_cboJZ.SelectedIndex = -1;
            this.m_cboJZ.SelectedItem = null;
            this.m_cboJZ.SelectionStart = 0;
            this.m_cboJZ.Size = new System.Drawing.Size(148, 23);
            this.m_cboJZ.TabIndex = 27;
            this.m_cboJZ.TextBackColor = System.Drawing.Color.White;
            this.m_cboJZ.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_lblFS
            // 
            this.m_lblFS.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblFS.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblFS.ForeColor = System.Drawing.Color.Black;
            this.m_lblFS.Location = new System.Drawing.Point(20, 100);
            this.m_lblFS.Name = "m_lblFS";
            this.m_lblFS.Size = new System.Drawing.Size(136, 20);
            this.m_lblFS.TabIndex = 26;
            this.m_lblFS.Text = "癌症";
            this.m_lblFS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblBDB
            // 
            this.m_lblBDB.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblBDB.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblBDB.ForeColor = System.Drawing.Color.Black;
            this.m_lblBDB.Location = new System.Drawing.Point(20, 64);
            this.m_lblBDB.Name = "m_lblBDB";
            this.m_lblBDB.Size = new System.Drawing.Size(136, 20);
            this.m_lblBDB.TabIndex = 26;
            this.m_lblBDB.Text = "急诊入ICU";
            this.m_lblBDB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cboSJ
            // 
            this.m_cboSJ.BackColor = System.Drawing.Color.White;
            this.m_cboSJ.BorderColor = System.Drawing.Color.Black;
            this.m_cboSJ.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboSJ.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboSJ.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboSJ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboSJ.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboSJ.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboSJ.ForeColor = System.Drawing.Color.Black;
            this.m_cboSJ.ListBackColor = System.Drawing.Color.White;
            this.m_cboSJ.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboSJ.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboSJ.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboSJ.Location = new System.Drawing.Point(180, 64);
            this.m_cboSJ.m_BlnEnableItemEventMenu = true;
            this.m_cboSJ.MaxLength = 32767;
            this.m_cboSJ.Name = "m_cboSJ";
            this.m_cboSJ.SelectedIndex = -1;
            this.m_cboSJ.SelectedItem = null;
            this.m_cboSJ.SelectionStart = 0;
            this.m_cboSJ.Size = new System.Drawing.Size(132, 23);
            this.m_cboSJ.TabIndex = 27;
            this.m_cboSJ.TextBackColor = System.Drawing.Color.White;
            this.m_cboSJ.TextForeColor = System.Drawing.Color.Black;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "器官系统衰竭数量";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtSBP
            // 
            this.m_txtSBP.AccessibleDescription = "呼吸频率";
            this.m_txtSBP.BackColor = System.Drawing.Color.White;
            this.m_txtSBP.BorderColor = System.Drawing.Color.Black;
            this.m_txtSBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtSBP.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtSBP.ForeColor = System.Drawing.Color.Black;
            this.m_txtSBP.Location = new System.Drawing.Point(180, 100);
            this.m_txtSBP.MaxLength = 9;
            this.m_txtSBP.Name = "m_txtSBP";
            this.m_txtSBP.Size = new System.Drawing.Size(132, 23);
            this.m_txtSBP.TabIndex = 395;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(16, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 14);
            this.label2.TabIndex = 396;
            this.label2.Text = "SBP";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(316, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 14);
            this.label3.TabIndex = 397;
            this.label3.Text = "mmHg";
            // 
            // cmdCalculate
            // 
            this.cmdCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdCalculate.DefaultScheme = true;
            this.cmdCalculate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdCalculate.ForeColor = System.Drawing.Color.Black;
            this.cmdCalculate.Hint = "";
            this.cmdCalculate.Location = new System.Drawing.Point(744, 258);
            this.cmdCalculate.Name = "cmdCalculate";
            this.cmdCalculate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCalculate.Size = new System.Drawing.Size(91, 32);
            this.cmdCalculate.TabIndex = 10000012;
            this.cmdCalculate.Text = "评 分(&E)";
            this.cmdCalculate.Click += new System.EventHandler(this.cmdCalculate_Click);
            // 
            // m_cmdGetCheckData
            // 
            this.m_cmdGetCheckData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdGetCheckData.DefaultScheme = true;
            this.m_cmdGetCheckData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGetCheckData.ForeColor = System.Drawing.Color.Black;
            this.m_cmdGetCheckData.Hint = "";
            this.m_cmdGetCheckData.Location = new System.Drawing.Point(604, 258);
            this.m_cmdGetCheckData.Name = "m_cmdGetCheckData";
            this.m_cmdGetCheckData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetCheckData.Size = new System.Drawing.Size(136, 32);
            this.m_cmdGetCheckData.TabIndex = 10000017;
            this.m_cmdGetCheckData.Text = "获取检验结果(&L)";
            this.m_cmdGetCheckData.Click += new System.EventHandler(this.m_cmdGetCheckData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_lblBDB);
            this.groupBox1.Controls.Add(this.m_lblDHS);
            this.groupBox1.Controls.Add(this.m_cboJZ);
            this.groupBox1.Controls.Add(this.m_lblFS);
            this.groupBox1.Controls.Add(this.m_cboHM);
            this.groupBox1.Controls.Add(this.m_cboCancer);
            this.groupBox1.Location = new System.Drawing.Point(8, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 136);
            this.groupBox1.TabIndex = 10000018;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_cboInfect);
            this.groupBox2.Controls.Add(this.m_txtSBP);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.m_cboSJ);
            this.groupBox2.Controls.Add(this.m_lblYYZ);
            this.groupBox2.Location = new System.Drawing.Point(428, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(420, 136);
            this.groupBox2.TabIndex = 10000019;
            this.groupBox2.TabStop = false;
            // 
            // frmMortalityRateEvaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.ClientSize = new System.Drawing.Size(866, 653);
            this.Controls.Add(this.dtpEvalDate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_cmdGetCheckData);
            this.Controls.Add(this.cmdCalculate);
            this.Controls.Add(this.lblTitle31);
            this.Controls.Add(this.lblEvalDate);
            this.Controls.Add(this.gpbEvaluation);
            this.Controls.Add(this.lbltxtEvalDoctor);
            this.Controls.Add(this.m_dtgResult);
            this.Menu = this.mainMenu1;
            this.Name = "frmMortalityRateEvaluation";
            this.Text = "MortalityRate评分";
            this.Load += new System.EventHandler(this.frmMortalityRateEvaluation_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMortalityRateEvaluation_Closing);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_dtgResult, 0);
            this.Controls.SetChildIndex(this.lbltxtEvalDoctor, 0);
            this.Controls.SetChildIndex(this.gpbEvaluation, 0);
            this.Controls.SetChildIndex(this.lblEvalDate, 0);
            this.Controls.SetChildIndex(this.lblTitle31, 0);
            this.Controls.SetChildIndex(this.cmdCalculate, 0);
            this.Controls.SetChildIndex(this.m_cmdGetCheckData, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.dtpEvalDate, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).EndInit();
            this.gpbEvaluation.ResumeLayout(false);
            this.gpbEvaluation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgResult)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

    }
}
