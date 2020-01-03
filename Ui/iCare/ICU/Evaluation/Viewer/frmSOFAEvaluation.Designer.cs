using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iCare.ICU.Evaluation
{
    partial class frmSOFAEvaluation
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
            this.m_gpbNerve = new System.Windows.Forms.GroupBox();
            this.m_lblOpenEyes = new System.Windows.Forms.Label();
            this.m_cboOpenEyes = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_lblSay = new System.Windows.Forms.Label();
            this.m_cboSay = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_lblSport = new System.Windows.Forms.Label();
            this.m_cboSport = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_gpbBreathSystem = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_txtPa02 = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtFi02 = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_cboXJG = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_cboDHS = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_cboXXB = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_cboDXY = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.timAutoCollect = new System.Timers.Timer();
            this.gpbEvaluation = new System.Windows.Forms.GroupBox();
            this.txtAutoTime = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.cmdStartAuto = new PinkieControls.ButtonXP();
            this.dtpStartSample = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblTitle96 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmdShowResult = new PinkieControls.ButtonXP();
            this.cmdStopAuto = new PinkieControls.ButtonXP();
            this.cmdGetData = new PinkieControls.ButtonXP();
            this.lblTitle31 = new System.Windows.Forms.Label();
            this.lblEvalDate = new System.Windows.Forms.Label();
            this.lbltxtEvalDoctor = new System.Windows.Forms.Label();
            this.m_dtgResult = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn8 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtpEvalDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.cmdCalculate = new PinkieControls.ButtonXP();
            this.m_cmdGetCheckData = new PinkieControls.ButtonXP();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_pnlNewBase.SuspendLayout();
            this.m_gpbNerve.SuspendLayout();
            this.m_gpbBreathSystem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).BeginInit();
            this.gpbEvaluation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgResult)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            // m_gpbNerve
            // 
            this.m_gpbNerve.Controls.Add(this.m_lblOpenEyes);
            this.m_gpbNerve.Controls.Add(this.m_cboOpenEyes);
            this.m_gpbNerve.Controls.Add(this.m_lblSay);
            this.m_gpbNerve.Controls.Add(this.m_cboSay);
            this.m_gpbNerve.Controls.Add(this.m_lblSport);
            this.m_gpbNerve.Controls.Add(this.m_cboSport);
            this.m_gpbNerve.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_gpbNerve.Location = new System.Drawing.Point(627, 103);
            this.m_gpbNerve.Name = "m_gpbNerve";
            this.m_gpbNerve.Size = new System.Drawing.Size(221, 216);
            this.m_gpbNerve.TabIndex = 32;
            this.m_gpbNerve.TabStop = false;
            this.m_gpbNerve.Text = "神经系统";
            // 
            // m_lblOpenEyes
            // 
            this.m_lblOpenEyes.Location = new System.Drawing.Point(8, 30);
            this.m_lblOpenEyes.Name = "m_lblOpenEyes";
            this.m_lblOpenEyes.Size = new System.Drawing.Size(72, 17);
            this.m_lblOpenEyes.TabIndex = 0;
            this.m_lblOpenEyes.Text = "睁眼反应";
            this.m_lblOpenEyes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.m_cboOpenEyes.ForeColor = System.Drawing.Color.White;
            this.m_cboOpenEyes.ListBackColor = System.Drawing.Color.White;
            this.m_cboOpenEyes.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboOpenEyes.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboOpenEyes.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboOpenEyes.Location = new System.Drawing.Point(80, 24);
            this.m_cboOpenEyes.m_BlnEnableItemEventMenu = true;
            this.m_cboOpenEyes.MaxLength = 32767;
            this.m_cboOpenEyes.Name = "m_cboOpenEyes";
            this.m_cboOpenEyes.SelectedIndex = -1;
            this.m_cboOpenEyes.SelectedItem = null;
            this.m_cboOpenEyes.SelectionStart = 0;
            this.m_cboOpenEyes.Size = new System.Drawing.Size(128, 23);
            this.m_cboOpenEyes.TabIndex = 1;
            this.m_cboOpenEyes.TextBackColor = System.Drawing.Color.White;
            this.m_cboOpenEyes.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_lblSay
            // 
            this.m_lblSay.Location = new System.Drawing.Point(8, 80);
            this.m_lblSay.Name = "m_lblSay";
            this.m_lblSay.Size = new System.Drawing.Size(72, 17);
            this.m_lblSay.TabIndex = 2;
            this.m_lblSay.Text = "言语反应";
            this.m_lblSay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.m_cboSay.ForeColor = System.Drawing.Color.White;
            this.m_cboSay.ListBackColor = System.Drawing.Color.White;
            this.m_cboSay.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboSay.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboSay.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboSay.Location = new System.Drawing.Point(80, 76);
            this.m_cboSay.m_BlnEnableItemEventMenu = true;
            this.m_cboSay.MaxLength = 32767;
            this.m_cboSay.Name = "m_cboSay";
            this.m_cboSay.SelectedIndex = -1;
            this.m_cboSay.SelectedItem = null;
            this.m_cboSay.SelectionStart = 0;
            this.m_cboSay.Size = new System.Drawing.Size(128, 23);
            this.m_cboSay.TabIndex = 3;
            this.m_cboSay.TextBackColor = System.Drawing.Color.White;
            this.m_cboSay.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_lblSport
            // 
            this.m_lblSport.Location = new System.Drawing.Point(8, 136);
            this.m_lblSport.Name = "m_lblSport";
            this.m_lblSport.Size = new System.Drawing.Size(72, 17);
            this.m_lblSport.TabIndex = 4;
            this.m_lblSport.Text = "运动反应";
            this.m_lblSport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.m_cboSport.ForeColor = System.Drawing.Color.White;
            this.m_cboSport.ListBackColor = System.Drawing.Color.White;
            this.m_cboSport.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboSport.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboSport.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboSport.Location = new System.Drawing.Point(80, 128);
            this.m_cboSport.m_BlnEnableItemEventMenu = true;
            this.m_cboSport.MaxLength = 32767;
            this.m_cboSport.Name = "m_cboSport";
            this.m_cboSport.SelectedIndex = -1;
            this.m_cboSport.SelectedItem = null;
            this.m_cboSport.SelectionStart = 0;
            this.m_cboSport.Size = new System.Drawing.Size(128, 23);
            this.m_cboSport.TabIndex = 5;
            this.m_cboSport.TextBackColor = System.Drawing.Color.White;
            this.m_cboSport.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_gpbBreathSystem
            // 
            this.m_gpbBreathSystem.Controls.Add(this.label8);
            this.m_gpbBreathSystem.Controls.Add(this.label7);
            this.m_gpbBreathSystem.Controls.Add(this.label4);
            this.m_gpbBreathSystem.Controls.Add(this.m_txtPa02);
            this.m_gpbBreathSystem.Controls.Add(this.label3);
            this.m_gpbBreathSystem.Controls.Add(this.m_txtFi02);
            this.m_gpbBreathSystem.Location = new System.Drawing.Point(4, 103);
            this.m_gpbBreathSystem.Name = "m_gpbBreathSystem";
            this.m_gpbBreathSystem.Size = new System.Drawing.Size(256, 92);
            this.m_gpbBreathSystem.TabIndex = 23;
            this.m_gpbBreathSystem.TabStop = false;
            this.m_gpbBreathSystem.Text = "呼吸系统";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("宋体", 7F);
            this.label8.Location = new System.Drawing.Point(40, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(8, 10);
            this.label8.TabIndex = 2;
            this.label8.Text = "2";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 7F);
            this.label7.Location = new System.Drawing.Point(40, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(8, 10);
            this.label7.TabIndex = 2;
            this.label7.Text = "2";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Pa0 (mmHg)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtPa02
            // 
            this.m_txtPa02.AccessibleName = "NoDefault";
            this.m_txtPa02.BackColor = System.Drawing.Color.White;
            this.m_txtPa02.BorderColor = System.Drawing.Color.Black;
            this.m_txtPa02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtPa02.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtPa02.ForeColor = System.Drawing.Color.Black;
            this.m_txtPa02.Location = new System.Drawing.Point(125, 21);
            this.m_txtPa02.Name = "m_txtPa02";
            this.m_txtPa02.Size = new System.Drawing.Size(99, 26);
            this.m_txtPa02.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Fi0 (%)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtFi02
            // 
            this.m_txtFi02.AccessibleName = "NoDefault";
            this.m_txtFi02.BackColor = System.Drawing.Color.White;
            this.m_txtFi02.BorderColor = System.Drawing.Color.Black;
            this.m_txtFi02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtFi02.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtFi02.ForeColor = System.Drawing.Color.Black;
            this.m_txtFi02.Location = new System.Drawing.Point(125, 56);
            this.m_txtFi02.Name = "m_txtFi02";
            this.m_txtFi02.Size = new System.Drawing.Size(99, 26);
            this.m_txtFi02.TabIndex = 3;
            // 
            // m_cboXJG
            // 
            this.m_cboXJG.BackColor = System.Drawing.Color.White;
            this.m_cboXJG.BorderColor = System.Drawing.Color.Black;
            this.m_cboXJG.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboXJG.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboXJG.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboXJG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboXJG.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboXJG.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboXJG.ForeColor = System.Drawing.Color.White;
            this.m_cboXJG.ListBackColor = System.Drawing.Color.White;
            this.m_cboXJG.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboXJG.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboXJG.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboXJG.Location = new System.Drawing.Point(216, 28);
            this.m_cboXJG.m_BlnEnableItemEventMenu = true;
            this.m_cboXJG.MaxLength = 32767;
            this.m_cboXJG.Name = "m_cboXJG";
            this.m_cboXJG.SelectedIndex = -1;
            this.m_cboXJG.SelectedItem = null;
            this.m_cboXJG.SelectionStart = 0;
            this.m_cboXJG.Size = new System.Drawing.Size(372, 23);
            this.m_cboXJG.TabIndex = 25;
            this.m_cboXJG.TextBackColor = System.Drawing.Color.White;
            this.m_cboXJG.TextForeColor = System.Drawing.Color.Black;
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
            this.m_cboDHS.ForeColor = System.Drawing.Color.White;
            this.m_cboDHS.ListBackColor = System.Drawing.Color.White;
            this.m_cboDHS.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboDHS.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboDHS.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboDHS.Location = new System.Drawing.Point(128, 56);
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
            // 
            // m_cboXXB
            // 
            this.m_cboXXB.BackColor = System.Drawing.Color.White;
            this.m_cboXXB.BorderColor = System.Drawing.Color.Black;
            this.m_cboXXB.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboXXB.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboXXB.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboXXB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboXXB.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboXXB.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboXXB.ForeColor = System.Drawing.Color.White;
            this.m_cboXXB.ListBackColor = System.Drawing.Color.White;
            this.m_cboXXB.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboXXB.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboXXB.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboXXB.Location = new System.Drawing.Point(128, 20);
            this.m_cboXXB.m_BlnEnableItemEventMenu = true;
            this.m_cboXXB.MaxLength = 32767;
            this.m_cboXXB.Name = "m_cboXXB";
            this.m_cboXXB.SelectedIndex = -1;
            this.m_cboXXB.SelectedItem = null;
            this.m_cboXXB.SelectionStart = 0;
            this.m_cboXXB.Size = new System.Drawing.Size(192, 23);
            this.m_cboXXB.TabIndex = 31;
            this.m_cboXXB.TextBackColor = System.Drawing.Color.White;
            this.m_cboXXB.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_cboDXY
            // 
            this.m_cboDXY.BackColor = System.Drawing.Color.White;
            this.m_cboDXY.BorderColor = System.Drawing.Color.Black;
            this.m_cboDXY.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboDXY.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboDXY.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboDXY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboDXY.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboDXY.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboDXY.ForeColor = System.Drawing.Color.White;
            this.m_cboDXY.ListBackColor = System.Drawing.Color.White;
            this.m_cboDXY.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboDXY.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboDXY.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboDXY.Location = new System.Drawing.Point(216, 68);
            this.m_cboDXY.m_BlnEnableItemEventMenu = true;
            this.m_cboDXY.MaxLength = 32767;
            this.m_cboDXY.Name = "m_cboDXY";
            this.m_cboDXY.SelectedIndex = -1;
            this.m_cboDXY.SelectedItem = null;
            this.m_cboDXY.SelectionStart = 0;
            this.m_cboDXY.Size = new System.Drawing.Size(372, 23);
            this.m_cboDXY.TabIndex = 29;
            this.m_cboDXY.TextBackColor = System.Drawing.Color.White;
            this.m_cboDXY.TextForeColor = System.Drawing.Color.Black;
            // 
            // timAutoCollect
            // 
            this.timAutoCollect.Interval = 60000;
            this.timAutoCollect.SynchronizingObject = this;
            this.timAutoCollect.Elapsed += new System.Timers.ElapsedEventHandler(this.timAutoCollect_Elapsed);
            // 
            // gpbEvaluation
            // 
            this.gpbEvaluation.Controls.Add(this.txtAutoTime);
            this.gpbEvaluation.Controls.Add(this.cmdStartAuto);
            this.gpbEvaluation.Controls.Add(this.dtpStartSample);
            this.gpbEvaluation.Controls.Add(this.lblTitle96);
            this.gpbEvaluation.Controls.Add(this.label10);
            this.gpbEvaluation.Controls.Add(this.cmdShowResult);
            this.gpbEvaluation.Controls.Add(this.cmdStopAuto);
            this.gpbEvaluation.Controls.Add(this.cmdGetData);
            this.gpbEvaluation.Location = new System.Drawing.Point(8, 471);
            this.gpbEvaluation.Name = "gpbEvaluation";
            this.gpbEvaluation.Size = new System.Drawing.Size(840, 64);
            this.gpbEvaluation.TabIndex = 39;
            this.gpbEvaluation.TabStop = false;
            this.gpbEvaluation.Text = "自动评分";
            // 
            // txtAutoTime
            // 
            this.txtAutoTime.BackColor = System.Drawing.Color.White;
            this.txtAutoTime.BorderColor = System.Drawing.Color.Black;
            this.txtAutoTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAutoTime.ForeColor = System.Drawing.Color.Black;
            this.txtAutoTime.Location = new System.Drawing.Point(496, 18);
            this.txtAutoTime.MaxLength = 10;
            this.txtAutoTime.Name = "txtAutoTime";
            this.txtAutoTime.Size = new System.Drawing.Size(45, 23);
            this.txtAutoTime.TabIndex = 4;
            this.txtAutoTime.Text = "60";
            this.txtAutoTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAutoTime.TextChanged += new System.EventHandler(this.txtAutoTime_TextChanged);
            // 
            // cmdStartAuto
            // 
            this.cmdStartAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdStartAuto.DefaultScheme = true;
            this.cmdStartAuto.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdStartAuto.ForeColor = System.Drawing.Color.Black;
            this.cmdStartAuto.Hint = "";
            this.cmdStartAuto.Location = new System.Drawing.Point(548, 13);
            this.cmdStartAuto.Name = "cmdStartAuto";
            this.cmdStartAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStartAuto.Size = new System.Drawing.Size(88, 32);
            this.cmdStartAuto.TabIndex = 10000005;
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
            this.dtpStartSample.Location = new System.Drawing.Point(76, 18);
            this.dtpStartSample.m_BlnOnlyTime = false;
            this.dtpStartSample.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpStartSample.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartSample.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartSample.Name = "dtpStartSample";
            this.dtpStartSample.ReadOnly = false;
            this.dtpStartSample.Size = new System.Drawing.Size(216, 22);
            this.dtpStartSample.TabIndex = 1;
            this.dtpStartSample.TextBackColor = System.Drawing.Color.White;
            this.dtpStartSample.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblTitle96
            // 
            this.lblTitle96.AutoSize = true;
            this.lblTitle96.Location = new System.Drawing.Point(5, 20);
            this.lblTitle96.Name = "lblTitle96";
            this.lblTitle96.Size = new System.Drawing.Size(82, 14);
            this.lblTitle96.TabIndex = 0;
            this.lblTitle96.Text = "采集时间：";
            this.lblTitle96.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(392, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 14);
            this.label10.TabIndex = 3;
            this.label10.Text = "评分间隔(秒)：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdShowResult
            // 
            this.cmdShowResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdShowResult.DefaultScheme = true;
            this.cmdShowResult.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdShowResult.ForeColor = System.Drawing.Color.Black;
            this.cmdShowResult.Hint = "";
            this.cmdShowResult.Location = new System.Drawing.Point(732, 13);
            this.cmdShowResult.Name = "cmdShowResult";
            this.cmdShowResult.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdShowResult.Size = new System.Drawing.Size(88, 32);
            this.cmdShowResult.TabIndex = 10000007;
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
            this.cmdStopAuto.Location = new System.Drawing.Point(640, 13);
            this.cmdStopAuto.Name = "cmdStopAuto";
            this.cmdStopAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStopAuto.Size = new System.Drawing.Size(88, 32);
            this.cmdStopAuto.TabIndex = 10000008;
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
            this.cmdGetData.Location = new System.Drawing.Point(300, 12);
            this.cmdGetData.Name = "cmdGetData";
            this.cmdGetData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdGetData.Size = new System.Drawing.Size(88, 32);
            this.cmdGetData.TabIndex = 10000006;
            this.cmdGetData.Text = "获取数据(&G)";
            this.cmdGetData.Click += new System.EventHandler(this.cmdGetData_Click);
            // 
            // lblTitle31
            // 
            this.lblTitle31.AutoSize = true;
            this.lblTitle31.Location = new System.Drawing.Point(312, 331);
            this.lblTitle31.Name = "lblTitle31";
            this.lblTitle31.Size = new System.Drawing.Size(67, 14);
            this.lblTitle31.TabIndex = 35;
            this.lblTitle31.Text = "评估者：";
            this.lblTitle31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEvalDate
            // 
            this.lblEvalDate.AutoSize = true;
            this.lblEvalDate.Location = new System.Drawing.Point(16, 331);
            this.lblEvalDate.Name = "lblEvalDate";
            this.lblEvalDate.Size = new System.Drawing.Size(82, 14);
            this.lblEvalDate.TabIndex = 33;
            this.lblEvalDate.Text = "评分日期：";
            this.lblEvalDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbltxtEvalDoctor
            // 
            this.lbltxtEvalDoctor.Location = new System.Drawing.Point(392, 331);
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
            this.m_dtgResult.CaptionText = "SOFA评分结果";
            this.m_dtgResult.DataMember = "";
            this.m_dtgResult.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.m_dtgResult.Location = new System.Drawing.Point(8, 363);
            this.m_dtgResult.Name = "m_dtgResult";
            this.m_dtgResult.ReadOnly = true;
            this.m_dtgResult.Size = new System.Drawing.Size(840, 104);
            this.m_dtgResult.TabIndex = 38;
            this.m_dtgResult.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.m_dtgResult.TabStop = false;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.m_dtgResult;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn6,
            this.dataGridTextBoxColumn8,
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn5});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "result";
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "总分";
            this.dataGridTextBoxColumn6.MappingName = "总分";
            this.dataGridTextBoxColumn6.Width = 75;
            // 
            // dataGridTextBoxColumn8
            // 
            this.dataGridTextBoxColumn8.Format = "";
            this.dataGridTextBoxColumn8.FormatInfo = null;
            this.dataGridTextBoxColumn8.HeaderText = "呼吸系统";
            this.dataGridTextBoxColumn8.MappingName = "呼吸系统";
            this.dataGridTextBoxColumn8.Width = 75;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "血液系统";
            this.dataGridTextBoxColumn1.MappingName = "血液系统";
            this.dataGridTextBoxColumn1.Width = 75;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "肝脏";
            this.dataGridTextBoxColumn2.MappingName = "肝脏";
            this.dataGridTextBoxColumn2.Width = 75;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "心血管系统";
            this.dataGridTextBoxColumn3.MappingName = "心血管系统";
            this.dataGridTextBoxColumn3.Width = 75;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "神经系统";
            this.dataGridTextBoxColumn4.MappingName = "神经系统";
            this.dataGridTextBoxColumn4.Width = 75;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "肾脏";
            this.dataGridTextBoxColumn5.MappingName = "肾脏";
            this.dataGridTextBoxColumn5.Width = 75;
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
            this.dtpEvalDate.Size = new System.Drawing.Size(215, 22);
            this.dtpEvalDate.TabIndex = 34;
            this.dtpEvalDate.TextBackColor = System.Drawing.Color.White;
            this.dtpEvalDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "胆红素(μmol/L)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 20);
            this.label2.TabIndex = 24;
            this.label2.Text = "血肌酐(μmol/L)或尿量(ml/L)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 19);
            this.label5.TabIndex = 28;
            this.label5.Text = "低血压";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 19);
            this.label6.TabIndex = 30;
            this.label6.Text = "血小板";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdCalculate
            // 
            this.cmdCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdCalculate.DefaultScheme = true;
            this.cmdCalculate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdCalculate.ForeColor = System.Drawing.Color.Black;
            this.cmdCalculate.Hint = "";
            this.cmdCalculate.Location = new System.Drawing.Point(756, 324);
            this.cmdCalculate.Name = "cmdCalculate";
            this.cmdCalculate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCalculate.Size = new System.Drawing.Size(84, 32);
            this.cmdCalculate.TabIndex = 10000006;
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
            this.m_cmdGetCheckData.Location = new System.Drawing.Point(628, 324);
            this.m_cmdGetCheckData.Name = "m_cmdGetCheckData";
            this.m_cmdGetCheckData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetCheckData.Size = new System.Drawing.Size(128, 32);
            this.m_cmdGetCheckData.TabIndex = 10000016;
            this.m_cmdGetCheckData.Text = "获取检验结果(&L)";
            this.m_cmdGetCheckData.Click += new System.EventHandler(this.m_cmdGetCheckData_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_cboDHS);
            this.groupBox2.Controls.Add(this.m_cboXXB);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(272, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 92);
            this.groupBox2.TabIndex = 10000017;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_cboXJG);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.m_cboDXY);
            this.groupBox3.Location = new System.Drawing.Point(4, 207);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(612, 112);
            this.groupBox3.TabIndex = 10000018;
            this.groupBox3.TabStop = false;
            // 
            // frmSOFAEvaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.ClientSize = new System.Drawing.Size(870, 707);
            this.Controls.Add(this.dtpEvalDate);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.m_cmdGetCheckData);
            this.Controls.Add(this.lblTitle31);
            this.Controls.Add(this.lblEvalDate);
            this.Controls.Add(this.gpbEvaluation);
            this.Controls.Add(this.lbltxtEvalDoctor);
            this.Controls.Add(this.m_dtgResult);
            this.Controls.Add(this.m_gpbBreathSystem);
            this.Controls.Add(this.m_gpbNerve);
            this.Controls.Add(this.cmdCalculate);
            this.Menu = this.mainMenu1;
            this.Name = "frmSOFAEvaluation";
            this.Text = "SOFA智能评分";
            this.Load += new System.EventHandler(this.frmSOFAEvaluation_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmSOFAEvaluation_Closing);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.cmdCalculate, 0);
            this.Controls.SetChildIndex(this.m_gpbNerve, 0);
            this.Controls.SetChildIndex(this.m_gpbBreathSystem, 0);
            this.Controls.SetChildIndex(this.m_dtgResult, 0);
            this.Controls.SetChildIndex(this.lbltxtEvalDoctor, 0);
            this.Controls.SetChildIndex(this.gpbEvaluation, 0);
            this.Controls.SetChildIndex(this.lblEvalDate, 0);
            this.Controls.SetChildIndex(this.lblTitle31, 0);
            this.Controls.SetChildIndex(this.m_cmdGetCheckData, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.dtpEvalDate, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.m_gpbNerve.ResumeLayout(false);
            this.m_gpbBreathSystem.ResumeLayout(false);
            this.m_gpbBreathSystem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).EndInit();
            this.gpbEvaluation.ResumeLayout(false);
            this.gpbEvaluation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgResult)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

    }
}
