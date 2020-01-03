using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iCare.ICU.Evaluation
{
    partial class frmMODSEvalution
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
            this.m_cboOpenEyes = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_cboSay = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_cboSport = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_lblOpenEyes = new System.Windows.Forms.Label();
            this.m_lblSay = new System.Windows.Forms.Label();
            this.m_lblSport = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.m_txtXXB = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_txtXJG = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_txtDHS = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_txtPJDMY = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_txtYFY = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_txtHR = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_txtPa02 = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_txtFi02 = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_gpbBreathSystem = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.m_gpbKidney = new System.Windows.Forms.GroupBox();
            this.m_gpbLiver = new System.Windows.Forms.GroupBox();
            this.m_gpbNerve = new System.Windows.Forms.GroupBox();
            this.m_gpbHeartSystem = new System.Windows.Forms.GroupBox();
            this.m_gpbBloodSystem = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblTitle31 = new System.Windows.Forms.Label();
            this.lblEvalDate = new System.Windows.Forms.Label();
            this.lbltxtEvalDoctor = new System.Windows.Forms.Label();
            this.m_dtgResult = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn7 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtpEvalDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.gpbEvaluation = new System.Windows.Forms.GroupBox();
            this.dtpStartSample = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblTitle96 = new System.Windows.Forms.Label();
            this.txtAutoTime = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmdStartAuto = new PinkieControls.ButtonXP();
            this.cmdStopAuto = new PinkieControls.ButtonXP();
            this.cmdShowResult = new PinkieControls.ButtonXP();
            this.cmdGetData = new PinkieControls.ButtonXP();
            this.timAutoCollect = new System.Timers.Timer();
            this.cmdCalculate = new PinkieControls.ButtonXP();
            this.m_cmdGetCheckData = new PinkieControls.ButtonXP();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_pnlNewBase.SuspendLayout();
            this.m_gpbBreathSystem.SuspendLayout();
            this.m_gpbKidney.SuspendLayout();
            this.m_gpbLiver.SuspendLayout();
            this.m_gpbNerve.SuspendLayout();
            this.m_gpbHeartSystem.SuspendLayout();
            this.m_gpbBloodSystem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgResult)).BeginInit();
            this.gpbEvaluation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).BeginInit();
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
            // m_cboOpenEyes
            // 
            this.m_cboOpenEyes.BackColor = System.Drawing.Color.White;
            this.m_cboOpenEyes.BorderColor = System.Drawing.Color.Black;
            this.m_cboOpenEyes.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboOpenEyes.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboOpenEyes.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboOpenEyes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboOpenEyes.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboOpenEyes.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboOpenEyes.ForeColor = System.Drawing.Color.Black;
            this.m_cboOpenEyes.ListBackColor = System.Drawing.Color.White;
            this.m_cboOpenEyes.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboOpenEyes.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboOpenEyes.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboOpenEyes.Location = new System.Drawing.Point(108, 24);
            this.m_cboOpenEyes.m_BlnEnableItemEventMenu = true;
            this.m_cboOpenEyes.MaxLength = 32767;
            this.m_cboOpenEyes.Name = "m_cboOpenEyes";
            this.m_cboOpenEyes.SelectedIndex = -1;
            this.m_cboOpenEyes.SelectedItem = null;
            this.m_cboOpenEyes.SelectionStart = 0;
            this.m_cboOpenEyes.Size = new System.Drawing.Size(132, 23);
            this.m_cboOpenEyes.TabIndex = 1;
            this.m_cboOpenEyes.TextBackColor = System.Drawing.Color.White;
            this.m_cboOpenEyes.TextForeColor = System.Drawing.Color.Black;
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
            this.m_cboSay.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboSay.ForeColor = System.Drawing.Color.Black;
            this.m_cboSay.ListBackColor = System.Drawing.Color.White;
            this.m_cboSay.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboSay.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboSay.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboSay.Location = new System.Drawing.Point(108, 61);
            this.m_cboSay.m_BlnEnableItemEventMenu = true;
            this.m_cboSay.MaxLength = 32767;
            this.m_cboSay.Name = "m_cboSay";
            this.m_cboSay.SelectedIndex = -1;
            this.m_cboSay.SelectedItem = null;
            this.m_cboSay.SelectionStart = 0;
            this.m_cboSay.Size = new System.Drawing.Size(132, 23);
            this.m_cboSay.TabIndex = 3;
            this.m_cboSay.TextBackColor = System.Drawing.Color.White;
            this.m_cboSay.TextForeColor = System.Drawing.Color.Black;
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
            this.m_cboSport.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboSport.ForeColor = System.Drawing.Color.Black;
            this.m_cboSport.ListBackColor = System.Drawing.Color.White;
            this.m_cboSport.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboSport.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboSport.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboSport.Location = new System.Drawing.Point(108, 94);
            this.m_cboSport.m_BlnEnableItemEventMenu = true;
            this.m_cboSport.MaxLength = 32767;
            this.m_cboSport.Name = "m_cboSport";
            this.m_cboSport.SelectedIndex = -1;
            this.m_cboSport.SelectedItem = null;
            this.m_cboSport.SelectionStart = 0;
            this.m_cboSport.Size = new System.Drawing.Size(132, 23);
            this.m_cboSport.TabIndex = 5;
            this.m_cboSport.TextBackColor = System.Drawing.Color.White;
            this.m_cboSport.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_lblOpenEyes
            // 
            this.m_lblOpenEyes.Location = new System.Drawing.Point(4, 27);
            this.m_lblOpenEyes.Name = "m_lblOpenEyes";
            this.m_lblOpenEyes.Size = new System.Drawing.Size(72, 17);
            this.m_lblOpenEyes.TabIndex = 0;
            this.m_lblOpenEyes.Text = "睁眼反应";
            this.m_lblOpenEyes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lblSay
            // 
            this.m_lblSay.Location = new System.Drawing.Point(4, 64);
            this.m_lblSay.Name = "m_lblSay";
            this.m_lblSay.Size = new System.Drawing.Size(72, 17);
            this.m_lblSay.TabIndex = 2;
            this.m_lblSay.Text = "言语反应";
            this.m_lblSay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lblSport
            // 
            this.m_lblSport.Location = new System.Drawing.Point(4, 97);
            this.m_lblSport.Name = "m_lblSport";
            this.m_lblSport.Size = new System.Drawing.Size(72, 17);
            this.m_lblSport.TabIndex = 4;
            this.m_lblSport.Text = "运动反应";
            this.m_lblSport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "平均动脉压(mmHg)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "血小板(*10 /L)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "FiO (%)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "PaO (mmHg)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "胆红素(mg/dl)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "血肌酐(umol/l)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 14);
            this.label7.TabIndex = 0;
            this.label7.Text = "心率(/min):";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 14);
            this.label8.TabIndex = 2;
            this.label8.Text = "右房压(mmHg)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_txtXXB
            // 
            this.m_txtXXB.AccessibleName = "NoDefault";
            this.m_txtXXB.BackColor = System.Drawing.Color.White;
            this.m_txtXXB.BorderColor = System.Drawing.Color.White;
            this.m_txtXXB.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtXXB.ForeColor = System.Drawing.Color.Black;
            this.m_txtXXB.Location = new System.Drawing.Point(128, 28);
            this.m_txtXXB.Name = "m_txtXXB";
            this.m_txtXXB.Size = new System.Drawing.Size(101, 23);
            this.m_txtXXB.TabIndex = 1;
            // 
            // m_txtXJG
            // 
            this.m_txtXJG.AccessibleName = "NoDefault";
            this.m_txtXJG.BackColor = System.Drawing.Color.White;
            this.m_txtXJG.BorderColor = System.Drawing.Color.White;
            this.m_txtXJG.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtXJG.ForeColor = System.Drawing.Color.Black;
            this.m_txtXJG.Location = new System.Drawing.Point(128, 32);
            this.m_txtXJG.Name = "m_txtXJG";
            this.m_txtXJG.Size = new System.Drawing.Size(101, 23);
            this.m_txtXJG.TabIndex = 1;
            // 
            // m_txtDHS
            // 
            this.m_txtDHS.AccessibleName = "NoDefault";
            this.m_txtDHS.BackColor = System.Drawing.Color.White;
            this.m_txtDHS.BorderColor = System.Drawing.Color.White;
            this.m_txtDHS.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDHS.ForeColor = System.Drawing.Color.Black;
            this.m_txtDHS.Location = new System.Drawing.Point(108, 29);
            this.m_txtDHS.Name = "m_txtDHS";
            this.m_txtDHS.Size = new System.Drawing.Size(132, 23);
            this.m_txtDHS.TabIndex = 1;
            // 
            // m_txtPJDMY
            // 
            this.m_txtPJDMY.AccessibleName = "NoDefault";
            this.m_txtPJDMY.BackColor = System.Drawing.Color.White;
            this.m_txtPJDMY.BorderColor = System.Drawing.Color.White;
            this.m_txtPJDMY.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtPJDMY.ForeColor = System.Drawing.Color.Black;
            this.m_txtPJDMY.Location = new System.Drawing.Point(124, 99);
            this.m_txtPJDMY.Name = "m_txtPJDMY";
            this.m_txtPJDMY.Size = new System.Drawing.Size(100, 23);
            this.m_txtPJDMY.TabIndex = 5;
            // 
            // m_txtYFY
            // 
            this.m_txtYFY.AccessibleName = "NoDefault";
            this.m_txtYFY.BackColor = System.Drawing.Color.White;
            this.m_txtYFY.BorderColor = System.Drawing.Color.White;
            this.m_txtYFY.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtYFY.ForeColor = System.Drawing.Color.Black;
            this.m_txtYFY.Location = new System.Drawing.Point(124, 63);
            this.m_txtYFY.Name = "m_txtYFY";
            this.m_txtYFY.Size = new System.Drawing.Size(100, 23);
            this.m_txtYFY.TabIndex = 3;
            // 
            // m_txtHR
            // 
            this.m_txtHR.AccessibleName = "NoDefault";
            this.m_txtHR.BackColor = System.Drawing.Color.White;
            this.m_txtHR.BorderColor = System.Drawing.Color.White;
            this.m_txtHR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtHR.ForeColor = System.Drawing.Color.Black;
            this.m_txtHR.Location = new System.Drawing.Point(124, 26);
            this.m_txtHR.Name = "m_txtHR";
            this.m_txtHR.Size = new System.Drawing.Size(100, 23);
            this.m_txtHR.TabIndex = 1;
            // 
            // m_txtPa02
            // 
            this.m_txtPa02.AccessibleName = "NoDefault";
            this.m_txtPa02.BackColor = System.Drawing.Color.White;
            this.m_txtPa02.BorderColor = System.Drawing.Color.White;
            this.m_txtPa02.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtPa02.ForeColor = System.Drawing.Color.Black;
            this.m_txtPa02.Location = new System.Drawing.Point(124, 22);
            this.m_txtPa02.Name = "m_txtPa02";
            this.m_txtPa02.Size = new System.Drawing.Size(101, 23);
            this.m_txtPa02.TabIndex = 1;
            // 
            // m_txtFi02
            // 
            this.m_txtFi02.AccessibleName = "NoDefault";
            this.m_txtFi02.BackColor = System.Drawing.Color.White;
            this.m_txtFi02.BorderColor = System.Drawing.Color.White;
            this.m_txtFi02.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtFi02.ForeColor = System.Drawing.Color.Black;
            this.m_txtFi02.Location = new System.Drawing.Point(124, 52);
            this.m_txtFi02.Name = "m_txtFi02";
            this.m_txtFi02.Size = new System.Drawing.Size(100, 23);
            this.m_txtFi02.TabIndex = 3;
            // 
            // m_gpbBreathSystem
            // 
            this.m_gpbBreathSystem.Controls.Add(this.label13);
            this.m_gpbBreathSystem.Controls.Add(this.label12);
            this.m_gpbBreathSystem.Controls.Add(this.label4);
            this.m_gpbBreathSystem.Controls.Add(this.m_txtPa02);
            this.m_gpbBreathSystem.Controls.Add(this.label3);
            this.m_gpbBreathSystem.Controls.Add(this.m_txtFi02);
            this.m_gpbBreathSystem.Location = new System.Drawing.Point(4, 104);
            this.m_gpbBreathSystem.Name = "m_gpbBreathSystem";
            this.m_gpbBreathSystem.Size = new System.Drawing.Size(236, 84);
            this.m_gpbBreathSystem.TabIndex = 23;
            this.m_gpbBreathSystem.TabStop = false;
            this.m_gpbBreathSystem.Text = "呼吸系统";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("宋体", 7F);
            this.label13.Location = new System.Drawing.Point(32, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(8, 10);
            this.label13.TabIndex = 4;
            this.label13.Text = "2";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("宋体", 7F);
            this.label12.Location = new System.Drawing.Point(32, 32);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(8, 10);
            this.label12.TabIndex = 4;
            this.label12.Text = "2";
            // 
            // m_gpbKidney
            // 
            this.m_gpbKidney.Controls.Add(this.m_txtXJG);
            this.m_gpbKidney.Controls.Add(this.label6);
            this.m_gpbKidney.Location = new System.Drawing.Point(276, 112);
            this.m_gpbKidney.Name = "m_gpbKidney";
            this.m_gpbKidney.Size = new System.Drawing.Size(243, 76);
            this.m_gpbKidney.TabIndex = 24;
            this.m_gpbKidney.TabStop = false;
            this.m_gpbKidney.Text = "肾脏";
            // 
            // m_gpbLiver
            // 
            this.m_gpbLiver.Controls.Add(this.m_txtDHS);
            this.m_gpbLiver.Controls.Add(this.label5);
            this.m_gpbLiver.Location = new System.Drawing.Point(560, 116);
            this.m_gpbLiver.Name = "m_gpbLiver";
            this.m_gpbLiver.Size = new System.Drawing.Size(291, 71);
            this.m_gpbLiver.TabIndex = 25;
            this.m_gpbLiver.TabStop = false;
            this.m_gpbLiver.Text = "肝脏";
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
            this.m_gpbNerve.Location = new System.Drawing.Point(560, 204);
            this.m_gpbNerve.Name = "m_gpbNerve";
            this.m_gpbNerve.Size = new System.Drawing.Size(291, 128);
            this.m_gpbNerve.TabIndex = 28;
            this.m_gpbNerve.TabStop = false;
            this.m_gpbNerve.Text = "神经系统";
            // 
            // m_gpbHeartSystem
            // 
            this.m_gpbHeartSystem.Controls.Add(this.m_txtPJDMY);
            this.m_gpbHeartSystem.Controls.Add(this.label7);
            this.m_gpbHeartSystem.Controls.Add(this.m_txtHR);
            this.m_gpbHeartSystem.Controls.Add(this.label8);
            this.m_gpbHeartSystem.Controls.Add(this.m_txtYFY);
            this.m_gpbHeartSystem.Controls.Add(this.label1);
            this.m_gpbHeartSystem.Location = new System.Drawing.Point(4, 200);
            this.m_gpbHeartSystem.Name = "m_gpbHeartSystem";
            this.m_gpbHeartSystem.Size = new System.Drawing.Size(236, 136);
            this.m_gpbHeartSystem.TabIndex = 26;
            this.m_gpbHeartSystem.TabStop = false;
            this.m_gpbHeartSystem.Text = "心血管系统";
            // 
            // m_gpbBloodSystem
            // 
            this.m_gpbBloodSystem.Controls.Add(this.label2);
            this.m_gpbBloodSystem.Controls.Add(this.label11);
            this.m_gpbBloodSystem.Controls.Add(this.m_txtXXB);
            this.m_gpbBloodSystem.Location = new System.Drawing.Point(276, 200);
            this.m_gpbBloodSystem.Name = "m_gpbBloodSystem";
            this.m_gpbBloodSystem.Size = new System.Drawing.Size(243, 80);
            this.m_gpbBloodSystem.TabIndex = 27;
            this.m_gpbBloodSystem.TabStop = false;
            this.m_gpbBloodSystem.Text = "血液系统";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(84, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 10);
            this.label11.TabIndex = 2;
            this.label11.Text = "9";
            // 
            // lblTitle31
            // 
            this.lblTitle31.AutoSize = true;
            this.lblTitle31.Location = new System.Drawing.Point(348, 360);
            this.lblTitle31.Name = "lblTitle31";
            this.lblTitle31.Size = new System.Drawing.Size(67, 14);
            this.lblTitle31.TabIndex = 446;
            this.lblTitle31.Text = "评估者：";
            this.lblTitle31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEvalDate
            // 
            this.lblEvalDate.AutoSize = true;
            this.lblEvalDate.Location = new System.Drawing.Point(4, 360);
            this.lblEvalDate.Name = "lblEvalDate";
            this.lblEvalDate.Size = new System.Drawing.Size(82, 14);
            this.lblEvalDate.TabIndex = 445;
            this.lblEvalDate.Text = "评分日期：";
            this.lblEvalDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbltxtEvalDoctor
            // 
            this.lbltxtEvalDoctor.Location = new System.Drawing.Point(412, 360);
            this.lbltxtEvalDoctor.Name = "lbltxtEvalDoctor";
            this.lbltxtEvalDoctor.Size = new System.Drawing.Size(136, 19);
            this.lbltxtEvalDoctor.TabIndex = 448;
            this.lbltxtEvalDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_dtgResult
            // 
            this.m_dtgResult.BackColor = System.Drawing.Color.White;
            this.m_dtgResult.BackgroundColor = System.Drawing.Color.White;
            this.m_dtgResult.CaptionBackColor = System.Drawing.Color.White;
            this.m_dtgResult.CaptionForeColor = System.Drawing.Color.Black;
            this.m_dtgResult.CaptionText = "MODS评分结果";
            this.m_dtgResult.DataMember = "";
            this.m_dtgResult.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.m_dtgResult.Location = new System.Drawing.Point(8, 392);
            this.m_dtgResult.Name = "m_dtgResult";
            this.m_dtgResult.ReadOnly = true;
            this.m_dtgResult.Size = new System.Drawing.Size(843, 108);
            this.m_dtgResult.TabIndex = 447;
            this.m_dtgResult.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.m_dtgResult.TabStop = false;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.m_dtgResult;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn7,
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn5,
            this.dataGridTextBoxColumn6});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "result";
            this.dataGridTableStyle1.RowHeadersVisible = false;
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = "总分";
            this.dataGridTextBoxColumn7.MappingName = "总分";
            this.dataGridTextBoxColumn7.Width = 75;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "呼吸系统";
            this.dataGridTextBoxColumn1.MappingName = "呼吸系统";
            this.dataGridTextBoxColumn1.Width = 75;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "肾脏";
            this.dataGridTextBoxColumn2.MappingName = "肾脏";
            this.dataGridTextBoxColumn2.Width = 75;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "肝脏";
            this.dataGridTextBoxColumn3.MappingName = "肝脏";
            this.dataGridTextBoxColumn3.Width = 75;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "心血管系统";
            this.dataGridTextBoxColumn4.MappingName = "心血管系统";
            this.dataGridTextBoxColumn4.Width = 75;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "血液系统";
            this.dataGridTextBoxColumn5.MappingName = "血液系统";
            this.dataGridTextBoxColumn5.Width = 75;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "神经系统";
            this.dataGridTextBoxColumn6.MappingName = "神经系统";
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
            this.dtpEvalDate.Location = new System.Drawing.Point(88, 356);
            this.dtpEvalDate.m_BlnOnlyTime = false;
            this.dtpEvalDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpEvalDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEvalDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEvalDate.Name = "dtpEvalDate";
            this.dtpEvalDate.ReadOnly = false;
            this.dtpEvalDate.Size = new System.Drawing.Size(213, 22);
            this.dtpEvalDate.TabIndex = 443;
            this.dtpEvalDate.TextBackColor = System.Drawing.Color.White;
            this.dtpEvalDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // gpbEvaluation
            // 
            this.gpbEvaluation.Controls.Add(this.dtpStartSample);
            this.gpbEvaluation.Controls.Add(this.lblTitle96);
            this.gpbEvaluation.Controls.Add(this.txtAutoTime);
            this.gpbEvaluation.Controls.Add(this.label10);
            this.gpbEvaluation.Controls.Add(this.cmdStartAuto);
            this.gpbEvaluation.Controls.Add(this.cmdStopAuto);
            this.gpbEvaluation.Controls.Add(this.cmdShowResult);
            this.gpbEvaluation.Controls.Add(this.cmdGetData);
            this.gpbEvaluation.Location = new System.Drawing.Point(8, 508);
            this.gpbEvaluation.Name = "gpbEvaluation";
            this.gpbEvaluation.Size = new System.Drawing.Size(843, 56);
            this.gpbEvaluation.TabIndex = 468;
            this.gpbEvaluation.TabStop = false;
            this.gpbEvaluation.Text = "自动评分";
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
            this.dtpStartSample.Location = new System.Drawing.Point(80, 17);
            this.dtpStartSample.m_BlnOnlyTime = false;
            this.dtpStartSample.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpStartSample.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartSample.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartSample.Name = "dtpStartSample";
            this.dtpStartSample.ReadOnly = false;
            this.dtpStartSample.Size = new System.Drawing.Size(212, 22);
            this.dtpStartSample.TabIndex = 210;
            this.dtpStartSample.TextBackColor = System.Drawing.Color.White;
            this.dtpStartSample.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblTitle96
            // 
            this.lblTitle96.AutoSize = true;
            this.lblTitle96.Location = new System.Drawing.Point(5, 19);
            this.lblTitle96.Name = "lblTitle96";
            this.lblTitle96.Size = new System.Drawing.Size(82, 14);
            this.lblTitle96.TabIndex = 392;
            this.lblTitle96.Text = "采集时间：";
            this.lblTitle96.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAutoTime
            // 
            this.txtAutoTime.BackColor = System.Drawing.Color.White;
            this.txtAutoTime.BorderColor = System.Drawing.Color.White;
            this.txtAutoTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAutoTime.ForeColor = System.Drawing.Color.Black;
            this.txtAutoTime.Location = new System.Drawing.Point(492, 17);
            this.txtAutoTime.MaxLength = 10;
            this.txtAutoTime.Name = "txtAutoTime";
            this.txtAutoTime.Size = new System.Drawing.Size(36, 23);
            this.txtAutoTime.TabIndex = 230;
            this.txtAutoTime.Text = "60";
            this.txtAutoTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAutoTime.TextChanged += new System.EventHandler(this.txtAutoTime_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(392, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 14);
            this.label10.TabIndex = 387;
            this.label10.Text = "评分间隔(秒)：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdStartAuto
            // 
            this.cmdStartAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdStartAuto.DefaultScheme = true;
            this.cmdStartAuto.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdStartAuto.ForeColor = System.Drawing.Color.Black;
            this.cmdStartAuto.Hint = "";
            this.cmdStartAuto.Location = new System.Drawing.Point(534, 12);
            this.cmdStartAuto.Name = "cmdStartAuto";
            this.cmdStartAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStartAuto.Size = new System.Drawing.Size(100, 32);
            this.cmdStartAuto.TabIndex = 10000018;
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
            this.cmdStopAuto.Location = new System.Drawing.Point(642, 12);
            this.cmdStopAuto.Name = "cmdStopAuto";
            this.cmdStopAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStopAuto.Size = new System.Drawing.Size(89, 32);
            this.cmdStopAuto.TabIndex = 10000021;
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
            this.cmdShowResult.Location = new System.Drawing.Point(737, 12);
            this.cmdShowResult.Name = "cmdShowResult";
            this.cmdShowResult.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdShowResult.Size = new System.Drawing.Size(100, 32);
            this.cmdShowResult.TabIndex = 10000020;
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
            this.cmdGetData.Location = new System.Drawing.Point(300, 12);
            this.cmdGetData.Name = "cmdGetData";
            this.cmdGetData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdGetData.Size = new System.Drawing.Size(86, 32);
            this.cmdGetData.TabIndex = 10000019;
            this.cmdGetData.Text = "获取数据(&G)";
            this.cmdGetData.Click += new System.EventHandler(this.cmdGetData_Click);
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
            this.cmdCalculate.Location = new System.Drawing.Point(716, 352);
            this.cmdCalculate.Name = "cmdCalculate";
            this.cmdCalculate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCalculate.Size = new System.Drawing.Size(84, 32);
            this.cmdCalculate.TabIndex = 10000013;
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
            this.m_cmdGetCheckData.Location = new System.Drawing.Point(284, 296);
            this.m_cmdGetCheckData.Name = "m_cmdGetCheckData";
            this.m_cmdGetCheckData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetCheckData.Size = new System.Drawing.Size(236, 32);
            this.m_cmdGetCheckData.TabIndex = 10000016;
            this.m_cmdGetCheckData.Text = "获取检验结果(&L)";
            this.m_cmdGetCheckData.Click += new System.EventHandler(this.m_cmdGetCheckData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(4, 344);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(859, 2);
            this.groupBox1.TabIndex = 10000017;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // frmMODSEvalution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.ClientSize = new System.Drawing.Size(863, 690);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_gpbLiver);
            this.Controls.Add(this.m_cmdGetCheckData);
            this.Controls.Add(this.lbltxtEvalDoctor);
            this.Controls.Add(this.dtpEvalDate);
            this.Controls.Add(this.cmdCalculate);
            this.Controls.Add(this.lblTitle31);
            this.Controls.Add(this.lblEvalDate);
            this.Controls.Add(this.gpbEvaluation);
            this.Controls.Add(this.m_dtgResult);
            this.Controls.Add(this.m_gpbBloodSystem);
            this.Controls.Add(this.m_gpbHeartSystem);
            this.Controls.Add(this.m_gpbNerve);
            this.Controls.Add(this.m_gpbKidney);
            this.Controls.Add(this.m_gpbBreathSystem);
            this.Name = "frmMODSEvalution";
            this.Text = "MODS智能评分";
            this.Load += new System.EventHandler(this.frmMODSEvalution_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMODSEvalution_Closing);
            this.Controls.SetChildIndex(this.m_gpbBreathSystem, 0);
            this.Controls.SetChildIndex(this.m_gpbKidney, 0);
            this.Controls.SetChildIndex(this.m_gpbNerve, 0);
            this.Controls.SetChildIndex(this.m_gpbHeartSystem, 0);
            this.Controls.SetChildIndex(this.m_gpbBloodSystem, 0);
            this.Controls.SetChildIndex(this.m_dtgResult, 0);
            this.Controls.SetChildIndex(this.gpbEvaluation, 0);
            this.Controls.SetChildIndex(this.lblEvalDate, 0);
            this.Controls.SetChildIndex(this.lblTitle31, 0);
            this.Controls.SetChildIndex(this.cmdCalculate, 0);
            this.Controls.SetChildIndex(this.dtpEvalDate, 0);
            this.Controls.SetChildIndex(this.lbltxtEvalDoctor, 0);
            this.Controls.SetChildIndex(this.m_cmdGetCheckData, 0);
            this.Controls.SetChildIndex(this.m_gpbLiver, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.m_gpbBreathSystem.ResumeLayout(false);
            this.m_gpbBreathSystem.PerformLayout();
            this.m_gpbKidney.ResumeLayout(false);
            this.m_gpbKidney.PerformLayout();
            this.m_gpbLiver.ResumeLayout(false);
            this.m_gpbLiver.PerformLayout();
            this.m_gpbNerve.ResumeLayout(false);
            this.m_gpbHeartSystem.ResumeLayout(false);
            this.m_gpbHeartSystem.PerformLayout();
            this.m_gpbBloodSystem.ResumeLayout(false);
            this.m_gpbBloodSystem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgResult)).EndInit();
            this.gpbEvaluation.ResumeLayout(false);
            this.gpbEvaluation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}
