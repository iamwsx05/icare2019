using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iCare.ICU.Evaluation
{
    partial class frmCTEvaluation
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
            this.cmdStopAuto = new PinkieControls.ButtonXP();
            this.dtpStartSample = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.cmdShowResult = new PinkieControls.ButtonXP();
            this.lblTitle96 = new System.Windows.Forms.Label();
            this.txtAutoTime = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmdGetData = new PinkieControls.ButtonXP();
            this.lblTitle31 = new System.Windows.Forms.Label();
            this.lblEvalDate = new System.Windows.Forms.Label();
            this.lbltxtEvalDoctor = new System.Windows.Forms.Label();
            this.m_dtgResult = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn8 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtpEvalDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.m_lblDHS = new System.Windows.Forms.Label();
            this.m_cboDHS = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_lblBDB = new System.Windows.Forms.Label();
            this.m_cboBDB = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_lblFS = new System.Windows.Forms.Label();
            this.m_cboFS = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_cboYYZ = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_lblYYZ = new System.Windows.Forms.Label();
            this.m_cboSJX = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_lblSJX = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdCalculate = new PinkieControls.ButtonXP();
            this.m_pnlNewBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).BeginInit();
            this.gpbEvaluation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgResult)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.gpbEvaluation.Controls.Add(this.cmdStopAuto);
            this.gpbEvaluation.Controls.Add(this.dtpStartSample);
            this.gpbEvaluation.Controls.Add(this.cmdShowResult);
            this.gpbEvaluation.Controls.Add(this.lblTitle96);
            this.gpbEvaluation.Controls.Add(this.txtAutoTime);
            this.gpbEvaluation.Controls.Add(this.label10);
            this.gpbEvaluation.Location = new System.Drawing.Point(8, 472);
            this.gpbEvaluation.Name = "gpbEvaluation";
            this.gpbEvaluation.Size = new System.Drawing.Size(840, 84);
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
            this.cmdStartAuto.Location = new System.Drawing.Point(508, 28);
            this.cmdStartAuto.Name = "cmdStartAuto";
            this.cmdStartAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStartAuto.Size = new System.Drawing.Size(96, 35);
            this.cmdStartAuto.TabIndex = 10000022;
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
            this.cmdStopAuto.Location = new System.Drawing.Point(615, 28);
            this.cmdStopAuto.Name = "cmdStopAuto";
            this.cmdStopAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStopAuto.Size = new System.Drawing.Size(96, 35);
            this.cmdStopAuto.TabIndex = 10000025;
            this.cmdStopAuto.Text = "停止评分(&S)";
            this.cmdStopAuto.Click += new System.EventHandler(this.cmdStopAuto_Click);
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
            this.dtpStartSample.Location = new System.Drawing.Point(104, 34);
            this.dtpStartSample.m_BlnOnlyTime = false;
            this.dtpStartSample.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpStartSample.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartSample.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartSample.Name = "dtpStartSample";
            this.dtpStartSample.ReadOnly = false;
            this.dtpStartSample.Size = new System.Drawing.Size(232, 22);
            this.dtpStartSample.TabIndex = 1;
            this.dtpStartSample.TextBackColor = System.Drawing.Color.White;
            this.dtpStartSample.TextForeColor = System.Drawing.Color.Black;
            // 
            // cmdShowResult
            // 
            this.cmdShowResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdShowResult.DefaultScheme = true;
            this.cmdShowResult.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdShowResult.ForeColor = System.Drawing.Color.Black;
            this.cmdShowResult.Hint = "";
            this.cmdShowResult.Location = new System.Drawing.Point(722, 28);
            this.cmdShowResult.Name = "cmdShowResult";
            this.cmdShowResult.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdShowResult.Size = new System.Drawing.Size(96, 35);
            this.cmdShowResult.TabIndex = 10000024;
            this.cmdShowResult.Text = "查看结果(&R)";
            this.cmdShowResult.Click += new System.EventHandler(this.cmdShowResult_Click);
            // 
            // lblTitle96
            // 
            this.lblTitle96.AutoSize = true;
            this.lblTitle96.BackColor = System.Drawing.SystemColors.Control;
            this.lblTitle96.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle96.ForeColor = System.Drawing.Color.Black;
            this.lblTitle96.Location = new System.Drawing.Point(16, 36);
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
            this.txtAutoTime.Location = new System.Drawing.Point(448, 34);
            this.txtAutoTime.MaxLength = 10;
            this.txtAutoTime.Name = "txtAutoTime";
            this.txtAutoTime.Size = new System.Drawing.Size(41, 23);
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
            this.label10.Location = new System.Drawing.Point(348, 36);
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
            this.cmdGetData.Location = new System.Drawing.Point(685, 20);
            this.cmdGetData.Name = "cmdGetData";
            this.cmdGetData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdGetData.Size = new System.Drawing.Size(135, 32);
            this.cmdGetData.TabIndex = 10000023;
            this.cmdGetData.Text = "获取检验数据(&G)";
            this.cmdGetData.Click += new System.EventHandler(this.cmdGetData_Click);
            // 
            // lblTitle31
            // 
            this.lblTitle31.AutoSize = true;
            this.lblTitle31.BackColor = System.Drawing.SystemColors.Control;
            this.lblTitle31.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle31.ForeColor = System.Drawing.Color.Black;
            this.lblTitle31.Location = new System.Drawing.Point(412, 224);
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
            this.lblEvalDate.Location = new System.Drawing.Point(28, 224);
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
            this.lbltxtEvalDoctor.Location = new System.Drawing.Point(480, 224);
            this.lbltxtEvalDoctor.Name = "lbltxtEvalDoctor";
            this.lbltxtEvalDoctor.Size = new System.Drawing.Size(120, 19);
            this.lbltxtEvalDoctor.TabIndex = 36;
            this.lbltxtEvalDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_dtgResult
            // 
            this.m_dtgResult.BackColor = System.Drawing.Color.White;
            this.m_dtgResult.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_dtgResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_dtgResult.CaptionBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_dtgResult.CaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.m_dtgResult.CaptionText = "CT评分结果";
            this.m_dtgResult.DataMember = "";
            this.m_dtgResult.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.m_dtgResult.Location = new System.Drawing.Point(8, 256);
            this.m_dtgResult.Name = "m_dtgResult";
            this.m_dtgResult.ReadOnly = true;
            this.m_dtgResult.Size = new System.Drawing.Size(840, 204);
            this.m_dtgResult.TabIndex = 38;
            this.m_dtgResult.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.m_dtgResult.TabStop = false;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.m_dtgResult;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn5,
            this.dataGridTextBoxColumn8,
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "result";
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "总分";
            this.dataGridTextBoxColumn5.MappingName = "总分";
            this.dataGridTextBoxColumn5.Width = 75;
            // 
            // dataGridTextBoxColumn8
            // 
            this.dataGridTextBoxColumn8.Format = "";
            this.dataGridTextBoxColumn8.FormatInfo = null;
            this.dataGridTextBoxColumn8.HeaderText = "胆红素";
            this.dataGridTextBoxColumn8.MappingName = "胆红素";
            this.dataGridTextBoxColumn8.Width = 75;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "白蛋白";
            this.dataGridTextBoxColumn1.MappingName = "白蛋白";
            this.dataGridTextBoxColumn1.Width = 75;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "腹水";
            this.dataGridTextBoxColumn2.MappingName = "腹水";
            this.dataGridTextBoxColumn2.Width = 75;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "神经系统障碍";
            this.dataGridTextBoxColumn3.MappingName = "神经系统障碍";
            this.dataGridTextBoxColumn3.Width = 75;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "营养状况";
            this.dataGridTextBoxColumn4.MappingName = "营养状况";
            this.dataGridTextBoxColumn4.Width = 75;
            // 
            // dtpEvalDate
            // 
            this.dtpEvalDate.BorderColor = System.Drawing.Color.Black;
            this.dtpEvalDate.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.dtpEvalDate.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.dtpEvalDate.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.dtpEvalDate.DropButtonForeColor = System.Drawing.Color.Black;
            this.dtpEvalDate.flatFont = new System.Drawing.Font("宋体", 12F);
            this.dtpEvalDate.Font = new System.Drawing.Font("宋体", 12F);
            this.dtpEvalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEvalDate.Location = new System.Drawing.Point(116, 220);
            this.dtpEvalDate.m_BlnOnlyTime = false;
            this.dtpEvalDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpEvalDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEvalDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEvalDate.Name = "dtpEvalDate";
            this.dtpEvalDate.ReadOnly = false;
            this.dtpEvalDate.Size = new System.Drawing.Size(232, 22);
            this.dtpEvalDate.TabIndex = 34;
            this.dtpEvalDate.TextBackColor = System.Drawing.Color.White;
            this.dtpEvalDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_lblDHS
            // 
            this.m_lblDHS.AutoSize = true;
            this.m_lblDHS.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblDHS.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblDHS.ForeColor = System.Drawing.Color.Black;
            this.m_lblDHS.Location = new System.Drawing.Point(20, 28);
            this.m_lblDHS.Name = "m_lblDHS";
            this.m_lblDHS.Size = new System.Drawing.Size(131, 14);
            this.m_lblDHS.TabIndex = 26;
            this.m_lblDHS.Text = "胆红素(μmol/L):";
            this.m_lblDHS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cboDHS
            // 
            this.m_cboDHS.BackColor = System.Drawing.Color.White;
            this.m_cboDHS.BorderColor = System.Drawing.Color.Black;
            this.m_cboDHS.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboDHS.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboDHS.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboDHS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboDHS.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboDHS.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboDHS.ForeColor = System.Drawing.Color.Black;
            this.m_cboDHS.ListBackColor = System.Drawing.Color.White;
            this.m_cboDHS.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboDHS.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboDHS.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboDHS.Location = new System.Drawing.Point(152, 26);
            this.m_cboDHS.m_BlnEnableItemEventMenu = true;
            this.m_cboDHS.MaxLength = 32767;
            this.m_cboDHS.Name = "m_cboDHS";
            this.m_cboDHS.SelectedIndex = -1;
            this.m_cboDHS.SelectedItem = null;
            this.m_cboDHS.SelectionStart = 0;
            this.m_cboDHS.Size = new System.Drawing.Size(192, 23);
            this.m_cboDHS.TabIndex = 27;
            this.m_cboDHS.TextBackColor = System.Drawing.Color.White;
            this.m_cboDHS.TextForeColor = System.Drawing.Color.Black;
            this.m_cboDHS.Load += new System.EventHandler(this.m_cboDHS_Load);
            // 
            // m_lblBDB
            // 
            this.m_lblBDB.AutoSize = true;
            this.m_lblBDB.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblBDB.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblBDB.ForeColor = System.Drawing.Color.Black;
            this.m_lblBDB.Location = new System.Drawing.Point(360, 28);
            this.m_lblBDB.Name = "m_lblBDB";
            this.m_lblBDB.Size = new System.Drawing.Size(108, 14);
            this.m_lblBDB.TabIndex = 26;
            this.m_lblBDB.Text = "白蛋白 (g/L):";
            this.m_lblBDB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cboBDB
            // 
            this.m_cboBDB.BackColor = System.Drawing.Color.White;
            this.m_cboBDB.BorderColor = System.Drawing.Color.Black;
            this.m_cboBDB.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboBDB.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboBDB.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboBDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboBDB.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboBDB.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboBDB.ForeColor = System.Drawing.Color.Black;
            this.m_cboBDB.ListBackColor = System.Drawing.Color.White;
            this.m_cboBDB.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboBDB.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboBDB.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboBDB.Location = new System.Drawing.Point(468, 26);
            this.m_cboBDB.m_BlnEnableItemEventMenu = true;
            this.m_cboBDB.MaxLength = 32767;
            this.m_cboBDB.Name = "m_cboBDB";
            this.m_cboBDB.SelectedIndex = -1;
            this.m_cboBDB.SelectedItem = null;
            this.m_cboBDB.SelectionStart = 0;
            this.m_cboBDB.Size = new System.Drawing.Size(160, 23);
            this.m_cboBDB.TabIndex = 27;
            this.m_cboBDB.TextBackColor = System.Drawing.Color.White;
            this.m_cboBDB.TextForeColor = System.Drawing.Color.Black;
            this.m_cboBDB.Load += new System.EventHandler(this.m_cboBDB_Load);
            // 
            // m_lblFS
            // 
            this.m_lblFS.AutoSize = true;
            this.m_lblFS.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblFS.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblFS.ForeColor = System.Drawing.Color.Black;
            this.m_lblFS.Location = new System.Drawing.Point(640, 68);
            this.m_lblFS.Name = "m_lblFS";
            this.m_lblFS.Size = new System.Drawing.Size(45, 14);
            this.m_lblFS.TabIndex = 26;
            this.m_lblFS.Text = "腹水:";
            this.m_lblFS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cboFS
            // 
            this.m_cboFS.BackColor = System.Drawing.Color.White;
            this.m_cboFS.BorderColor = System.Drawing.Color.Black;
            this.m_cboFS.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboFS.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboFS.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboFS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboFS.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboFS.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboFS.ForeColor = System.Drawing.Color.Black;
            this.m_cboFS.ListBackColor = System.Drawing.Color.White;
            this.m_cboFS.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboFS.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboFS.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboFS.Location = new System.Drawing.Point(686, 64);
            this.m_cboFS.m_BlnEnableItemEventMenu = true;
            this.m_cboFS.MaxLength = 32767;
            this.m_cboFS.Name = "m_cboFS";
            this.m_cboFS.SelectedIndex = -1;
            this.m_cboFS.SelectedItem = null;
            this.m_cboFS.SelectionStart = 0;
            this.m_cboFS.Size = new System.Drawing.Size(135, 23);
            this.m_cboFS.TabIndex = 27;
            this.m_cboFS.TextBackColor = System.Drawing.Color.White;
            this.m_cboFS.TextForeColor = System.Drawing.Color.Black;
            this.m_cboFS.Load += new System.EventHandler(this.m_cboFS_Load);
            // 
            // m_cboYYZ
            // 
            this.m_cboYYZ.BackColor = System.Drawing.Color.White;
            this.m_cboYYZ.BorderColor = System.Drawing.Color.Black;
            this.m_cboYYZ.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboYYZ.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboYYZ.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboYYZ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboYYZ.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboYYZ.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboYYZ.ForeColor = System.Drawing.Color.Black;
            this.m_cboYYZ.ListBackColor = System.Drawing.Color.White;
            this.m_cboYYZ.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboYYZ.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboYYZ.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboYYZ.Location = new System.Drawing.Point(152, 64);
            this.m_cboYYZ.m_BlnEnableItemEventMenu = true;
            this.m_cboYYZ.MaxLength = 32767;
            this.m_cboYYZ.Name = "m_cboYYZ";
            this.m_cboYYZ.SelectedIndex = -1;
            this.m_cboYYZ.SelectedItem = null;
            this.m_cboYYZ.SelectionStart = 0;
            this.m_cboYYZ.Size = new System.Drawing.Size(192, 23);
            this.m_cboYYZ.TabIndex = 27;
            this.m_cboYYZ.TextBackColor = System.Drawing.Color.White;
            this.m_cboYYZ.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_lblYYZ
            // 
            this.m_lblYYZ.AutoSize = true;
            this.m_lblYYZ.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblYYZ.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblYYZ.ForeColor = System.Drawing.Color.Black;
            this.m_lblYYZ.Location = new System.Drawing.Point(20, 68);
            this.m_lblYYZ.Name = "m_lblYYZ";
            this.m_lblYYZ.Size = new System.Drawing.Size(123, 14);
            this.m_lblYYZ.TabIndex = 26;
            this.m_lblYYZ.Text = "营  养  状  况:";
            this.m_lblYYZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cboSJX
            // 
            this.m_cboSJX.BackColor = System.Drawing.Color.White;
            this.m_cboSJX.BorderColor = System.Drawing.Color.Black;
            this.m_cboSJX.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboSJX.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboSJX.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboSJX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboSJX.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboSJX.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboSJX.ForeColor = System.Drawing.Color.Black;
            this.m_cboSJX.ListBackColor = System.Drawing.Color.White;
            this.m_cboSJX.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboSJX.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboSJX.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboSJX.Location = new System.Drawing.Point(468, 64);
            this.m_cboSJX.m_BlnEnableItemEventMenu = true;
            this.m_cboSJX.MaxLength = 32767;
            this.m_cboSJX.Name = "m_cboSJX";
            this.m_cboSJX.SelectedIndex = -1;
            this.m_cboSJX.SelectedItem = null;
            this.m_cboSJX.SelectionStart = 0;
            this.m_cboSJX.Size = new System.Drawing.Size(160, 23);
            this.m_cboSJX.TabIndex = 27;
            this.m_cboSJX.TextBackColor = System.Drawing.Color.White;
            this.m_cboSJX.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_lblSJX
            // 
            this.m_lblSJX.AutoSize = true;
            this.m_lblSJX.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblSJX.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblSJX.ForeColor = System.Drawing.Color.Black;
            this.m_lblSJX.Location = new System.Drawing.Point(360, 68);
            this.m_lblSJX.Name = "m_lblSJX";
            this.m_lblSJX.Size = new System.Drawing.Size(105, 14);
            this.m_lblSJX.TabIndex = 26;
            this.m_lblSJX.Text = "神经系统障碍:";
            this.m_lblSJX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdGetData);
            this.groupBox1.Controls.Add(this.m_lblDHS);
            this.groupBox1.Controls.Add(this.m_cboDHS);
            this.groupBox1.Controls.Add(this.m_lblBDB);
            this.groupBox1.Controls.Add(this.m_cboBDB);
            this.groupBox1.Controls.Add(this.m_lblFS);
            this.groupBox1.Controls.Add(this.m_cboFS);
            this.groupBox1.Controls.Add(this.m_cboYYZ);
            this.groupBox1.Controls.Add(this.m_lblYYZ);
            this.groupBox1.Controls.Add(this.m_cboSJX);
            this.groupBox1.Controls.Add(this.m_lblSJX);
            this.groupBox1.Location = new System.Drawing.Point(8, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(840, 104);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "评分项目";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cmdCalculate
            // 
            this.cmdCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdCalculate.DefaultScheme = true;
            this.cmdCalculate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdCalculate.ForeColor = System.Drawing.Color.Black;
            this.cmdCalculate.Hint = "";
            this.cmdCalculate.Location = new System.Drawing.Point(702, 216);
            this.cmdCalculate.Name = "cmdCalculate";
            this.cmdCalculate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCalculate.Size = new System.Drawing.Size(135, 32);
            this.cmdCalculate.TabIndex = 10000014;
            this.cmdCalculate.Text = "评 分(&E)";
            this.cmdCalculate.Click += new System.EventHandler(this.cmdCalculate_Click);
            // 
            // frmCTEvaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.ClientSize = new System.Drawing.Size(868, 690);
            this.Controls.Add(this.lbltxtEvalDoctor);
            this.Controls.Add(this.dtpEvalDate);
            this.Controls.Add(this.cmdCalculate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTitle31);
            this.Controls.Add(this.lblEvalDate);
            this.Controls.Add(this.gpbEvaluation);
            this.Controls.Add(this.m_dtgResult);
            this.Menu = this.mainMenu1;
            this.Name = "frmCTEvaluation";
            this.Text = "CT智能评分";
            this.Load += new System.EventHandler(this.frmCTEvaluation_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmCTEvaluation_Closing);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_dtgResult, 0);
            this.Controls.SetChildIndex(this.gpbEvaluation, 0);
            this.Controls.SetChildIndex(this.lblEvalDate, 0);
            this.Controls.SetChildIndex(this.lblTitle31, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.cmdCalculate, 0);
            this.Controls.SetChildIndex(this.dtpEvalDate, 0);
            this.Controls.SetChildIndex(this.lbltxtEvalDoctor, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).EndInit();
            this.gpbEvaluation.ResumeLayout(false);
            this.gpbEvaluation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgResult)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}
