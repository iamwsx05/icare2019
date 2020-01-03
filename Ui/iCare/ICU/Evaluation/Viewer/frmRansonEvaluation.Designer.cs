using System;
using System.Collections.Generic;
using System.Text;

namespace iCare.ICU.Evaluation
{
    partial class frmRansonEvaluation
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
            this.cmdGetData = new PinkieControls.ButtonXP();
            this.cmdShowResult = new PinkieControls.ButtonXP();
            this.cmdStopAuto = new PinkieControls.ButtonXP();
            this.lblTitle31 = new System.Windows.Forms.Label();
            this.lblEvalDate = new System.Windows.Forms.Label();
            this.lbltxtEvalDoctor = new System.Windows.Forms.Label();
            this.m_dtgResult = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn12 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn7 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn9 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn10 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn11 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtpEvalDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.m_rdbFds = new System.Windows.Forms.RadioButton();
            this.m_rdbDs = new System.Windows.Forms.RadioButton();
            this.m_lblXXB = new System.Windows.Forms.Label();
            this.m_txtXXB = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_lblXNS = new System.Windows.Forms.Label();
            this.m_txtXNS = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_lblXG = new System.Windows.Forms.Label();
            this.m_txtXG = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_lblDMX = new System.Windows.Forms.Label();
            this.m_txtDMX = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_lblJQS = new System.Windows.Forms.Label();
            this.m_txtJQS = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_lblYTZ = new System.Windows.Forms.Label();
            this.m_txtYTZ = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_lblRST = new System.Windows.Forms.Label();
            this.m_lblTDA = new System.Windows.Forms.Label();
            this.m_lblBXB = new System.Windows.Forms.Label();
            this.m_txtTDA = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_txtBXB = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_txtXT = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_lblXT = new System.Windows.Forms.Label();
            this.m_txtRST = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTitle11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmdCalculate = new PinkieControls.ButtonXP();
            this.m_cmdGetCheckData = new PinkieControls.ButtonXP();
            this.m_pnlNewBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).BeginInit();
            this.gpbEvaluation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgResult)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.gpbEvaluation.Controls.Add(this.cmdGetData);
            this.gpbEvaluation.Controls.Add(this.cmdShowResult);
            this.gpbEvaluation.Controls.Add(this.cmdStopAuto);
            this.gpbEvaluation.Location = new System.Drawing.Point(8, 479);
            this.gpbEvaluation.Name = "gpbEvaluation";
            this.gpbEvaluation.Size = new System.Drawing.Size(838, 68);
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
            this.cmdStartAuto.Location = new System.Drawing.Point(544, 24);
            this.cmdStartAuto.Name = "cmdStartAuto";
            this.cmdStartAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStartAuto.Size = new System.Drawing.Size(88, 32);
            this.cmdStartAuto.TabIndex = 10000009;
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
            this.dtpStartSample.Location = new System.Drawing.Point(80, 28);
            this.dtpStartSample.m_BlnOnlyTime = false;
            this.dtpStartSample.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpStartSample.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartSample.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartSample.Name = "dtpStartSample";
            this.dtpStartSample.ReadOnly = false;
            this.dtpStartSample.Size = new System.Drawing.Size(214, 22);
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
            this.lblTitle96.Location = new System.Drawing.Point(9, 28);
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
            this.txtAutoTime.ForeColor = System.Drawing.Color.Black;
            this.txtAutoTime.Location = new System.Drawing.Point(496, 28);
            this.txtAutoTime.MaxLength = 10;
            this.txtAutoTime.Name = "txtAutoTime";
            this.txtAutoTime.Size = new System.Drawing.Size(45, 23);
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
            this.label10.Location = new System.Drawing.Point(396, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 14);
            this.label10.TabIndex = 3;
            this.label10.Text = "评分间隔(秒)：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdGetData
            // 
            this.cmdGetData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdGetData.DefaultScheme = true;
            this.cmdGetData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdGetData.ForeColor = System.Drawing.Color.Black;
            this.cmdGetData.Hint = "";
            this.cmdGetData.Location = new System.Drawing.Point(296, 24);
            this.cmdGetData.Name = "cmdGetData";
            this.cmdGetData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdGetData.Size = new System.Drawing.Size(88, 32);
            this.cmdGetData.TabIndex = 10000010;
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
            this.cmdShowResult.Location = new System.Drawing.Point(738, 24);
            this.cmdShowResult.Name = "cmdShowResult";
            this.cmdShowResult.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdShowResult.Size = new System.Drawing.Size(88, 32);
            this.cmdShowResult.TabIndex = 10000012;
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
            this.cmdStopAuto.Location = new System.Drawing.Point(642, 24);
            this.cmdStopAuto.Name = "cmdStopAuto";
            this.cmdStopAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStopAuto.Size = new System.Drawing.Size(88, 32);
            this.cmdStopAuto.TabIndex = 10000013;
            this.cmdStopAuto.Text = "停止评分(&S)";
            this.cmdStopAuto.Click += new System.EventHandler(this.cmdStopAuto_Click);
            // 
            // lblTitle31
            // 
            this.lblTitle31.AutoSize = true;
            this.lblTitle31.BackColor = System.Drawing.SystemColors.Control;
            this.lblTitle31.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle31.ForeColor = System.Drawing.Color.Black;
            this.lblTitle31.Location = new System.Drawing.Point(352, 331);
            this.lblTitle31.Name = "lblTitle31";
            this.lblTitle31.Size = new System.Drawing.Size(67, 14);
            this.lblTitle31.TabIndex = 35;
            this.lblTitle31.Text = "评估者：";
            this.lblTitle31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEvalDate
            // 
            this.lblEvalDate.AutoSize = true;
            this.lblEvalDate.Location = new System.Drawing.Point(20, 331);
            this.lblEvalDate.Name = "lblEvalDate";
            this.lblEvalDate.Size = new System.Drawing.Size(82, 14);
            this.lblEvalDate.TabIndex = 33;
            this.lblEvalDate.Text = "评分日期：";
            // 
            // lbltxtEvalDoctor
            // 
            this.lbltxtEvalDoctor.BackColor = System.Drawing.SystemColors.Control;
            this.lbltxtEvalDoctor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbltxtEvalDoctor.ForeColor = System.Drawing.Color.Black;
            this.lbltxtEvalDoctor.Location = new System.Drawing.Point(416, 332);
            this.lbltxtEvalDoctor.Name = "lbltxtEvalDoctor";
            this.lbltxtEvalDoctor.Size = new System.Drawing.Size(120, 16);
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
            this.m_dtgResult.CaptionText = "Ranson评分结果";
            this.m_dtgResult.DataMember = "";
            this.m_dtgResult.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.m_dtgResult.Location = new System.Drawing.Point(8, 363);
            this.m_dtgResult.Name = "m_dtgResult";
            this.m_dtgResult.ReadOnly = true;
            this.m_dtgResult.Size = new System.Drawing.Size(840, 108);
            this.m_dtgResult.TabIndex = 38;
            this.m_dtgResult.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.m_dtgResult.TabStop = false;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.m_dtgResult;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn12,
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn5,
            this.dataGridTextBoxColumn6,
            this.dataGridTextBoxColumn7,
            this.dataGridTextBoxColumn9,
            this.dataGridTextBoxColumn10,
            this.dataGridTextBoxColumn11});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "result";
            // 
            // dataGridTextBoxColumn12
            // 
            this.dataGridTextBoxColumn12.Format = "";
            this.dataGridTextBoxColumn12.FormatInfo = null;
            this.dataGridTextBoxColumn12.HeaderText = "病死率";
            this.dataGridTextBoxColumn12.MappingName = "病死率";
            this.dataGridTextBoxColumn12.Width = 75;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "白细胞";
            this.dataGridTextBoxColumn1.MappingName = "白细胞";
            this.dataGridTextBoxColumn1.Width = 75;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "血糖";
            this.dataGridTextBoxColumn2.MappingName = "血糖";
            this.dataGridTextBoxColumn2.Width = 75;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "乳酸脱氢酶";
            this.dataGridTextBoxColumn3.MappingName = "乳酸脱氢酶";
            this.dataGridTextBoxColumn3.Width = 75;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "天冬氨酸转氨酶";
            this.dataGridTextBoxColumn4.MappingName = "天冬氨酸转氨酶";
            this.dataGridTextBoxColumn4.Width = 75;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "血细胞比容下降";
            this.dataGridTextBoxColumn5.MappingName = "血细胞比容下降";
            this.dataGridTextBoxColumn5.Width = 75;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "血尿素氮升高";
            this.dataGridTextBoxColumn6.MappingName = "血尿素氮升高";
            this.dataGridTextBoxColumn6.Width = 75;
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = "血钙";
            this.dataGridTextBoxColumn7.MappingName = "血钙";
            this.dataGridTextBoxColumn7.Width = 75;
            // 
            // dataGridTextBoxColumn9
            // 
            this.dataGridTextBoxColumn9.Format = "";
            this.dataGridTextBoxColumn9.FormatInfo = null;
            this.dataGridTextBoxColumn9.HeaderText = "动脉血氧分压";
            this.dataGridTextBoxColumn9.MappingName = "动脉血氧分压";
            this.dataGridTextBoxColumn9.Width = 75;
            // 
            // dataGridTextBoxColumn10
            // 
            this.dataGridTextBoxColumn10.Format = "";
            this.dataGridTextBoxColumn10.FormatInfo = null;
            this.dataGridTextBoxColumn10.HeaderText = "碱缺失";
            this.dataGridTextBoxColumn10.MappingName = "碱缺失";
            this.dataGridTextBoxColumn10.Width = 75;
            // 
            // dataGridTextBoxColumn11
            // 
            this.dataGridTextBoxColumn11.Format = "";
            this.dataGridTextBoxColumn11.FormatInfo = null;
            this.dataGridTextBoxColumn11.HeaderText = "液体潴留";
            this.dataGridTextBoxColumn11.MappingName = "液体潴留";
            this.dataGridTextBoxColumn11.Width = 75;
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
            this.dtpEvalDate.Location = new System.Drawing.Point(88, 329);
            this.dtpEvalDate.m_BlnOnlyTime = false;
            this.dtpEvalDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpEvalDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEvalDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEvalDate.Name = "dtpEvalDate";
            this.dtpEvalDate.ReadOnly = false;
            this.dtpEvalDate.Size = new System.Drawing.Size(217, 22);
            this.dtpEvalDate.TabIndex = 34;
            this.dtpEvalDate.TextBackColor = System.Drawing.Color.White;
            this.dtpEvalDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_rdbFds
            // 
            this.m_rdbFds.Location = new System.Drawing.Point(13, 24);
            this.m_rdbFds.Name = "m_rdbFds";
            this.m_rdbFds.Size = new System.Drawing.Size(152, 21);
            this.m_rdbFds.TabIndex = 51;
            this.m_rdbFds.TabStop = true;
            this.m_rdbFds.Tag = "0";
            this.m_rdbFds.Text = "非胆石性胰腺炎";
            this.m_rdbFds.CheckedChanged += new System.EventHandler(this.m_rdbFds_CheckedChanged);
            // 
            // m_rdbDs
            // 
            this.m_rdbDs.Location = new System.Drawing.Point(156, 24);
            this.m_rdbDs.Name = "m_rdbDs";
            this.m_rdbDs.Size = new System.Drawing.Size(152, 21);
            this.m_rdbDs.TabIndex = 52;
            this.m_rdbDs.TabStop = true;
            this.m_rdbDs.Tag = "1";
            this.m_rdbDs.Text = "胆石性胰腺炎";
            this.m_rdbDs.CheckedChanged += new System.EventHandler(this.m_rdbDs_CheckedChanged);
            // 
            // m_lblXXB
            // 
            this.m_lblXXB.AutoSize = true;
            this.m_lblXXB.Location = new System.Drawing.Point(16, 28);
            this.m_lblXXB.Name = "m_lblXXB";
            this.m_lblXXB.Size = new System.Drawing.Size(120, 14);
            this.m_lblXXB.TabIndex = 404;
            this.m_lblXXB.Text = "血细胞比容下降:";
            this.m_lblXXB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_txtXXB
            // 
            this.m_txtXXB.AccessibleDescription = "呼吸频率";
            this.m_txtXXB.BackColor = System.Drawing.Color.White;
            this.m_txtXXB.BorderColor = System.Drawing.Color.White;
            this.m_txtXXB.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtXXB.ForeColor = System.Drawing.Color.Black;
            this.m_txtXXB.Location = new System.Drawing.Point(160, 28);
            this.m_txtXXB.MaxLength = 3;
            this.m_txtXXB.Name = "m_txtXXB";
            this.m_txtXXB.Size = new System.Drawing.Size(141, 23);
            this.m_txtXXB.TabIndex = 403;
            this.m_txtXXB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_lblXNS
            // 
            this.m_lblXNS.AutoSize = true;
            this.m_lblXNS.Location = new System.Drawing.Point(16, 60);
            this.m_lblXNS.Name = "m_lblXNS";
            this.m_lblXNS.Size = new System.Drawing.Size(105, 14);
            this.m_lblXNS.TabIndex = 406;
            this.m_lblXNS.Text = "血尿素氮升高:";
            this.m_lblXNS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_txtXNS
            // 
            this.m_txtXNS.AccessibleDescription = "呼吸频率";
            this.m_txtXNS.BackColor = System.Drawing.Color.White;
            this.m_txtXNS.BorderColor = System.Drawing.Color.White;
            this.m_txtXNS.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtXNS.ForeColor = System.Drawing.Color.Black;
            this.m_txtXNS.Location = new System.Drawing.Point(160, 60);
            this.m_txtXNS.MaxLength = 3;
            this.m_txtXNS.Name = "m_txtXNS";
            this.m_txtXNS.Size = new System.Drawing.Size(141, 23);
            this.m_txtXNS.TabIndex = 405;
            this.m_txtXNS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_lblXG
            // 
            this.m_lblXG.AutoSize = true;
            this.m_lblXG.Location = new System.Drawing.Point(16, 96);
            this.m_lblXG.Name = "m_lblXG";
            this.m_lblXG.Size = new System.Drawing.Size(45, 14);
            this.m_lblXG.TabIndex = 408;
            this.m_lblXG.Text = "血钙:";
            this.m_lblXG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_txtXG
            // 
            this.m_txtXG.AccessibleDescription = "呼吸频率";
            this.m_txtXG.BackColor = System.Drawing.Color.White;
            this.m_txtXG.BorderColor = System.Drawing.Color.White;
            this.m_txtXG.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtXG.ForeColor = System.Drawing.Color.Black;
            this.m_txtXG.Location = new System.Drawing.Point(160, 92);
            this.m_txtXG.MaxLength = 3;
            this.m_txtXG.Name = "m_txtXG";
            this.m_txtXG.Size = new System.Drawing.Size(141, 23);
            this.m_txtXG.TabIndex = 407;
            this.m_txtXG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_lblDMX
            // 
            this.m_lblDMX.AutoSize = true;
            this.m_lblDMX.Location = new System.Drawing.Point(16, 124);
            this.m_lblDMX.Name = "m_lblDMX";
            this.m_lblDMX.Size = new System.Drawing.Size(105, 14);
            this.m_lblDMX.TabIndex = 410;
            this.m_lblDMX.Text = "动脉血氧分压:";
            this.m_lblDMX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_txtDMX
            // 
            this.m_txtDMX.AccessibleDescription = "呼吸频率";
            this.m_txtDMX.BackColor = System.Drawing.Color.White;
            this.m_txtDMX.BorderColor = System.Drawing.Color.White;
            this.m_txtDMX.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDMX.ForeColor = System.Drawing.Color.Black;
            this.m_txtDMX.Location = new System.Drawing.Point(160, 120);
            this.m_txtDMX.MaxLength = 3;
            this.m_txtDMX.Name = "m_txtDMX";
            this.m_txtDMX.Size = new System.Drawing.Size(141, 23);
            this.m_txtDMX.TabIndex = 409;
            this.m_txtDMX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_lblJQS
            // 
            this.m_lblJQS.AutoSize = true;
            this.m_lblJQS.Location = new System.Drawing.Point(16, 152);
            this.m_lblJQS.Name = "m_lblJQS";
            this.m_lblJQS.Size = new System.Drawing.Size(60, 14);
            this.m_lblJQS.TabIndex = 412;
            this.m_lblJQS.Text = "碱缺失:";
            this.m_lblJQS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_txtJQS
            // 
            this.m_txtJQS.AccessibleDescription = "呼吸频率";
            this.m_txtJQS.BackColor = System.Drawing.Color.White;
            this.m_txtJQS.BorderColor = System.Drawing.Color.White;
            this.m_txtJQS.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtJQS.ForeColor = System.Drawing.Color.Black;
            this.m_txtJQS.Location = new System.Drawing.Point(160, 152);
            this.m_txtJQS.MaxLength = 3;
            this.m_txtJQS.Name = "m_txtJQS";
            this.m_txtJQS.Size = new System.Drawing.Size(141, 23);
            this.m_txtJQS.TabIndex = 411;
            this.m_txtJQS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_lblYTZ
            // 
            this.m_lblYTZ.AutoSize = true;
            this.m_lblYTZ.Location = new System.Drawing.Point(16, 184);
            this.m_lblYTZ.Name = "m_lblYTZ";
            this.m_lblYTZ.Size = new System.Drawing.Size(75, 14);
            this.m_lblYTZ.TabIndex = 414;
            this.m_lblYTZ.Text = "液体潴留:";
            this.m_lblYTZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_txtYTZ
            // 
            this.m_txtYTZ.AccessibleDescription = "呼吸频率";
            this.m_txtYTZ.BackColor = System.Drawing.Color.White;
            this.m_txtYTZ.BorderColor = System.Drawing.Color.White;
            this.m_txtYTZ.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtYTZ.ForeColor = System.Drawing.Color.Black;
            this.m_txtYTZ.Location = new System.Drawing.Point(160, 184);
            this.m_txtYTZ.MaxLength = 3;
            this.m_txtYTZ.Name = "m_txtYTZ";
            this.m_txtYTZ.Size = new System.Drawing.Size(141, 23);
            this.m_txtYTZ.TabIndex = 413;
            this.m_txtYTZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.m_txtXXB);
            this.groupBox2.Controls.Add(this.m_txtXNS);
            this.groupBox2.Controls.Add(this.m_lblDMX);
            this.groupBox2.Controls.Add(this.m_txtYTZ);
            this.groupBox2.Controls.Add(this.m_lblXG);
            this.groupBox2.Controls.Add(this.m_txtJQS);
            this.groupBox2.Controls.Add(this.m_txtXG);
            this.groupBox2.Controls.Add(this.m_lblXXB);
            this.groupBox2.Controls.Add(this.m_txtDMX);
            this.groupBox2.Controls.Add(this.m_lblXNS);
            this.groupBox2.Controls.Add(this.m_lblJQS);
            this.groupBox2.Controls.Add(this.m_lblYTZ);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Location = new System.Drawing.Point(424, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(424, 216);
            this.groupBox2.TabIndex = 401;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "入院后";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.SystemColors.Control;
            this.label16.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(317, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 14);
            this.label16.TabIndex = 415;
            this.label16.Text = "%";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.SystemColors.Control;
            this.label17.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(316, 60);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 14);
            this.label17.TabIndex = 415;
            this.label17.Text = "mg/dl";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.SystemColors.Control;
            this.label18.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(316, 96);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(47, 14);
            this.label18.TabIndex = 415;
            this.label18.Text = "mg/dl";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.SystemColors.Control;
            this.label19.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(317, 124);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(39, 14);
            this.label19.TabIndex = 415;
            this.label19.Text = "mmHg";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.SystemColors.Control;
            this.label20.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(316, 152);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(55, 14);
            this.label20.TabIndex = 415;
            this.label20.Text = "mmol/L";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.SystemColors.Control;
            this.label21.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(317, 188);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(15, 14);
            this.label21.TabIndex = 415;
            this.label21.Text = "L";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(312, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 401;
            this.label4.Text = "×10 /L";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblRST
            // 
            this.m_lblRST.AutoSize = true;
            this.m_lblRST.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblRST.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblRST.ForeColor = System.Drawing.Color.Black;
            this.m_lblRST.Location = new System.Drawing.Point(12, 88);
            this.m_lblRST.Name = "m_lblRST";
            this.m_lblRST.Size = new System.Drawing.Size(90, 14);
            this.m_lblRST.TabIndex = 398;
            this.m_lblRST.Text = "乳酸脱氢酶:";
            this.m_lblRST.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblTDA
            // 
            this.m_lblTDA.AutoSize = true;
            this.m_lblTDA.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblTDA.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblTDA.ForeColor = System.Drawing.Color.Black;
            this.m_lblTDA.Location = new System.Drawing.Point(12, 120);
            this.m_lblTDA.Name = "m_lblTDA";
            this.m_lblTDA.Size = new System.Drawing.Size(120, 14);
            this.m_lblTDA.TabIndex = 400;
            this.m_lblTDA.Text = "天冬氨酸转氨酶:";
            this.m_lblTDA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblBXB
            // 
            this.m_lblBXB.AutoSize = true;
            this.m_lblBXB.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblBXB.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblBXB.ForeColor = System.Drawing.Color.Black;
            this.m_lblBXB.Location = new System.Drawing.Point(13, 24);
            this.m_lblBXB.Name = "m_lblBXB";
            this.m_lblBXB.Size = new System.Drawing.Size(60, 14);
            this.m_lblBXB.TabIndex = 394;
            this.m_lblBXB.Text = "白细胞:";
            this.m_lblBXB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtTDA
            // 
            this.m_txtTDA.AccessibleDescription = "呼吸频率";
            this.m_txtTDA.BackColor = System.Drawing.Color.White;
            this.m_txtTDA.BorderColor = System.Drawing.Color.White;
            this.m_txtTDA.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtTDA.ForeColor = System.Drawing.Color.Black;
            this.m_txtTDA.Location = new System.Drawing.Point(156, 116);
            this.m_txtTDA.MaxLength = 3;
            this.m_txtTDA.Name = "m_txtTDA";
            this.m_txtTDA.Size = new System.Drawing.Size(139, 23);
            this.m_txtTDA.TabIndex = 399;
            this.m_txtTDA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_txtBXB
            // 
            this.m_txtBXB.AccessibleDescription = "呼吸频率";
            this.m_txtBXB.BackColor = System.Drawing.Color.White;
            this.m_txtBXB.BorderColor = System.Drawing.Color.White;
            this.m_txtBXB.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtBXB.ForeColor = System.Drawing.Color.Black;
            this.m_txtBXB.Location = new System.Drawing.Point(157, 24);
            this.m_txtBXB.MaxLength = 3;
            this.m_txtBXB.Name = "m_txtBXB";
            this.m_txtBXB.Size = new System.Drawing.Size(139, 23);
            this.m_txtBXB.TabIndex = 393;
            this.m_txtBXB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_txtXT
            // 
            this.m_txtXT.AccessibleDescription = "呼吸频率";
            this.m_txtXT.BackColor = System.Drawing.Color.White;
            this.m_txtXT.BorderColor = System.Drawing.Color.White;
            this.m_txtXT.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtXT.ForeColor = System.Drawing.Color.Black;
            this.m_txtXT.Location = new System.Drawing.Point(157, 56);
            this.m_txtXT.MaxLength = 3;
            this.m_txtXT.Name = "m_txtXT";
            this.m_txtXT.Size = new System.Drawing.Size(139, 23);
            this.m_txtXT.TabIndex = 395;
            this.m_txtXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_lblXT
            // 
            this.m_lblXT.AutoSize = true;
            this.m_lblXT.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblXT.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblXT.ForeColor = System.Drawing.Color.Black;
            this.m_lblXT.Location = new System.Drawing.Point(13, 56);
            this.m_lblXT.Name = "m_lblXT";
            this.m_lblXT.Size = new System.Drawing.Size(45, 14);
            this.m_lblXT.TabIndex = 396;
            this.m_lblXT.Text = "血糖:";
            this.m_lblXT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtRST
            // 
            this.m_txtRST.AccessibleDescription = "呼吸频率";
            this.m_txtRST.BackColor = System.Drawing.Color.White;
            this.m_txtRST.BorderColor = System.Drawing.Color.White;
            this.m_txtRST.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtRST.ForeColor = System.Drawing.Color.Black;
            this.m_txtRST.Location = new System.Drawing.Point(156, 84);
            this.m_txtRST.MaxLength = 3;
            this.m_txtRST.Name = "m_txtRST";
            this.m_txtRST.Size = new System.Drawing.Size(139, 23);
            this.m_txtRST.TabIndex = 397;
            this.m_txtRST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.lblTitle11);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.m_lblRST);
            this.groupBox1.Controls.Add(this.m_lblTDA);
            this.groupBox1.Controls.Add(this.m_lblBXB);
            this.groupBox1.Controls.Add(this.m_txtTDA);
            this.groupBox1.Controls.Add(this.m_txtBXB);
            this.groupBox1.Controls.Add(this.m_txtXT);
            this.groupBox1.Controls.Add(this.m_lblXT);
            this.groupBox1.Controls.Add(this.m_txtRST);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(8, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 152);
            this.groupBox1.TabIndex = 415;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "入院前";
            // 
            // lblTitle11
            // 
            this.lblTitle11.AutoSize = true;
            this.lblTitle11.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle11.Location = new System.Drawing.Point(370, 28);
            this.lblTitle11.Name = "lblTitle11";
            this.lblTitle11.Size = new System.Drawing.Size(11, 9);
            this.lblTitle11.TabIndex = 402;
            this.lblTitle11.Text = "9";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.SystemColors.Control;
            this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(312, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 17);
            this.label13.TabIndex = 401;
            this.label13.Text = "mg/dl";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.SystemColors.Control;
            this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(308, 88);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 17);
            this.label14.TabIndex = 401;
            this.label14.Text = "IU/L";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.SystemColors.Control;
            this.label15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(312, 120);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 17);
            this.label15.TabIndex = 401;
            this.label15.Text = "IU/L";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_rdbDs);
            this.groupBox3.Controls.Add(this.m_rdbFds);
            this.groupBox3.Location = new System.Drawing.Point(8, 103);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(388, 53);
            this.groupBox3.TabIndex = 416;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "病症类型";
            // 
            // cmdCalculate
            // 
            this.cmdCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdCalculate.DefaultScheme = true;
            this.cmdCalculate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdCalculate.ForeColor = System.Drawing.Color.Black;
            this.cmdCalculate.Hint = "";
            this.cmdCalculate.Location = new System.Drawing.Point(732, 324);
            this.cmdCalculate.Name = "cmdCalculate";
            this.cmdCalculate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCalculate.Size = new System.Drawing.Size(91, 32);
            this.cmdCalculate.TabIndex = 10000011;
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
            this.m_cmdGetCheckData.Location = new System.Drawing.Point(584, 324);
            this.m_cmdGetCheckData.Name = "m_cmdGetCheckData";
            this.m_cmdGetCheckData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetCheckData.Size = new System.Drawing.Size(144, 32);
            this.m_cmdGetCheckData.TabIndex = 10000016;
            this.m_cmdGetCheckData.Text = "获取检验结果(&L)";
            this.m_cmdGetCheckData.Click += new System.EventHandler(this.m_cmdGetCheckData_Click);
            // 
            // frmRansonEvaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.ClientSize = new System.Drawing.Size(862, 707);
            this.Controls.Add(this.lbltxtEvalDoctor);
            this.Controls.Add(this.dtpEvalDate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdCalculate);
            this.Controls.Add(this.lblTitle31);
            this.Controls.Add(this.lblEvalDate);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpbEvaluation);
            this.Controls.Add(this.m_dtgResult);
            this.Controls.Add(this.m_cmdGetCheckData);
            this.Menu = this.mainMenu1;
            this.Name = "frmRansonEvaluation";
            this.Text = "Ranson 智能评分";
            this.Load += new System.EventHandler(this.frmRansonEvaluation_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmRansonEvaluation_Closing);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_cmdGetCheckData, 0);
            this.Controls.SetChildIndex(this.m_dtgResult, 0);
            this.Controls.SetChildIndex(this.gpbEvaluation, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.lblEvalDate, 0);
            this.Controls.SetChildIndex(this.lblTitle31, 0);
            this.Controls.SetChildIndex(this.cmdCalculate, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.dtpEvalDate, 0);
            this.Controls.SetChildIndex(this.lbltxtEvalDoctor, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).EndInit();
            this.gpbEvaluation.ResumeLayout(false);
            this.gpbEvaluation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgResult)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}
