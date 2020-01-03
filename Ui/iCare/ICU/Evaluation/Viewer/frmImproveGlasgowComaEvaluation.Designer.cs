using System;
using System.Collections.Generic;
using System.Text;

namespace iCare.ICU.Evaluation
{
    partial class frmImproveGlasgowComaEvaluation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImproveGlasgowComaEvaluation));
            this.dtpEvalDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblEvalDate = new System.Windows.Forms.Label();
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.lblTitle4 = new System.Windows.Forms.Label();
            this.lblTitle5 = new System.Windows.Forms.Label();
            this.lblTitle6 = new System.Windows.Forms.Label();
            this.lblTitle7 = new System.Windows.Forms.Label();
            this.lblTitle8 = new System.Windows.Forms.Label();
            this.lblTitle9 = new System.Windows.Forms.Label();
            this.cboOpenEyeU1 = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.cboOpenEyeO1 = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.lblTitle10 = new System.Windows.Forms.Label();
            this.lblTitle11 = new System.Windows.Forms.Label();
            this.cboSportU1 = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.cboSportO1 = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.cboLangU2 = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.cboLangU5 = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.cboLangO5 = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.cboPupil = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.cboBrain = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.cboSpontaneityBreath = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.cboTwitch = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.lblTitle12 = new System.Windows.Forms.Label();
            this.lblTitle13 = new System.Windows.Forms.Label();
            this.lblTitle14 = new System.Windows.Forms.Label();
            this.gpbAge = new System.Windows.Forms.GroupBox();
            this.rdbU1 = new System.Windows.Forms.RadioButton();
            this.rdbU2 = new System.Windows.Forms.RadioButton();
            this.rdbU3 = new System.Windows.Forms.RadioButton();
            this.rdbU4 = new System.Windows.Forms.RadioButton();
            this.rdbU5 = new System.Windows.Forms.RadioButton();
            this.rdbO5 = new System.Windows.Forms.RadioButton();
            this.dtgResult = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn8 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn7 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.txtEvalDoctor = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_cmdEvalDoctor = new PinkieControls.ButtonXP();
            this.cmdCalculate = new PinkieControls.ButtonXP();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_pnlNewBase.SuspendLayout();
            this.gpbAge.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).BeginInit();
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
            this.dtpEvalDate.Location = new System.Drawing.Point(72, 411);
            this.dtpEvalDate.m_BlnOnlyTime = false;
            this.dtpEvalDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpEvalDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEvalDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEvalDate.Name = "dtpEvalDate";
            this.dtpEvalDate.ReadOnly = false;
            this.dtpEvalDate.Size = new System.Drawing.Size(216, 22);
            this.dtpEvalDate.TabIndex = 230;
            this.dtpEvalDate.TextBackColor = System.Drawing.Color.White;
            this.dtpEvalDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblEvalDate
            // 
            this.lblEvalDate.AutoSize = true;
            this.lblEvalDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEvalDate.Location = new System.Drawing.Point(0, 413);
            this.lblEvalDate.Name = "lblEvalDate";
            this.lblEvalDate.Size = new System.Drawing.Size(82, 14);
            this.lblEvalDate.TabIndex = 316;
            this.lblEvalDate.Text = "评分日期：";
            this.lblEvalDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle3
            // 
            this.lblTitle3.AutoSize = true;
            this.lblTitle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle3.Location = new System.Drawing.Point(112, 20);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(60, 14);
            this.lblTitle3.TabIndex = 321;
            this.lblTitle3.Text = "Ⅰ.睁眼";
            this.lblTitle3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle4
            // 
            this.lblTitle4.AutoSize = true;
            this.lblTitle4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle4.Location = new System.Drawing.Point(356, 20);
            this.lblTitle4.Name = "lblTitle4";
            this.lblTitle4.Size = new System.Drawing.Size(90, 14);
            this.lblTitle4.TabIndex = 321;
            this.lblTitle4.Text = "Ⅱ.运动反应";
            this.lblTitle4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle5
            // 
            this.lblTitle5.AutoSize = true;
            this.lblTitle5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle5.Location = new System.Drawing.Point(112, 108);
            this.lblTitle5.Name = "lblTitle5";
            this.lblTitle5.Size = new System.Drawing.Size(90, 14);
            this.lblTitle5.TabIndex = 321;
            this.lblTitle5.Text = "Ⅲ.语言反应";
            this.lblTitle5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle6
            // 
            this.lblTitle6.AutoSize = true;
            this.lblTitle6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle6.Location = new System.Drawing.Point(356, 108);
            this.lblTitle6.Name = "lblTitle6";
            this.lblTitle6.Size = new System.Drawing.Size(105, 14);
            this.lblTitle6.TabIndex = 321;
            this.lblTitle6.Text = "Ⅳ.瞳孔光反应";
            this.lblTitle6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle7
            // 
            this.lblTitle7.AutoSize = true;
            this.lblTitle7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle7.Location = new System.Drawing.Point(356, 164);
            this.lblTitle7.Name = "lblTitle7";
            this.lblTitle7.Size = new System.Drawing.Size(90, 14);
            this.lblTitle7.TabIndex = 321;
            this.lblTitle7.Text = "Ⅴ.脑干反射";
            this.lblTitle7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle8
            // 
            this.lblTitle8.AutoSize = true;
            this.lblTitle8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle8.Location = new System.Drawing.Point(356, 228);
            this.lblTitle8.Name = "lblTitle8";
            this.lblTitle8.Size = new System.Drawing.Size(105, 14);
            this.lblTitle8.TabIndex = 321;
            this.lblTitle8.Text = "Ⅶ.自发性呼吸";
            this.lblTitle8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle9
            // 
            this.lblTitle9.AutoSize = true;
            this.lblTitle9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle9.Location = new System.Drawing.Point(112, 228);
            this.lblTitle9.Name = "lblTitle9";
            this.lblTitle9.Size = new System.Drawing.Size(60, 14);
            this.lblTitle9.TabIndex = 321;
            this.lblTitle9.Text = "Ⅵ.抽搐";
            this.lblTitle9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboOpenEyeU1
            // 
            this.cboOpenEyeU1.BackColor = System.Drawing.Color.White;
            this.cboOpenEyeU1.BorderColor = System.Drawing.Color.Black;
            this.cboOpenEyeU1.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboOpenEyeU1.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboOpenEyeU1.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboOpenEyeU1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboOpenEyeU1.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboOpenEyeU1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboOpenEyeU1.ForeColor = System.Drawing.Color.Black;
            this.cboOpenEyeU1.ListBackColor = System.Drawing.Color.White;
            this.cboOpenEyeU1.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboOpenEyeU1.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboOpenEyeU1.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboOpenEyeU1.Location = new System.Drawing.Point(112, 40);
            this.cboOpenEyeU1.m_BlnEnableItemEventMenu = true;
            this.cboOpenEyeU1.MaxLength = 32767;
            this.cboOpenEyeU1.Name = "cboOpenEyeU1";
            this.cboOpenEyeU1.SelectedIndex = -1;
            this.cboOpenEyeU1.SelectedItem = null;
            this.cboOpenEyeU1.SelectionStart = 0;
            this.cboOpenEyeU1.Size = new System.Drawing.Size(220, 23);
            this.cboOpenEyeU1.TabIndex = 120;
            this.cboOpenEyeU1.TextBackColor = System.Drawing.Color.White;
            this.cboOpenEyeU1.TextForeColor = System.Drawing.Color.Black;
            // 
            // cboOpenEyeO1
            // 
            this.cboOpenEyeO1.BackColor = System.Drawing.Color.White;
            this.cboOpenEyeO1.BorderColor = System.Drawing.Color.Black;
            this.cboOpenEyeO1.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboOpenEyeO1.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboOpenEyeO1.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboOpenEyeO1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboOpenEyeO1.Enabled = false;
            this.cboOpenEyeO1.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboOpenEyeO1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboOpenEyeO1.ForeColor = System.Drawing.Color.Black;
            this.cboOpenEyeO1.ListBackColor = System.Drawing.Color.White;
            this.cboOpenEyeO1.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboOpenEyeO1.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboOpenEyeO1.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboOpenEyeO1.Location = new System.Drawing.Point(112, 76);
            this.cboOpenEyeO1.m_BlnEnableItemEventMenu = true;
            this.cboOpenEyeO1.MaxLength = 32767;
            this.cboOpenEyeO1.Name = "cboOpenEyeO1";
            this.cboOpenEyeO1.SelectedIndex = -1;
            this.cboOpenEyeO1.SelectedItem = null;
            this.cboOpenEyeO1.SelectionStart = 0;
            this.cboOpenEyeO1.Size = new System.Drawing.Size(220, 23);
            this.cboOpenEyeO1.TabIndex = 130;
            this.cboOpenEyeO1.TextBackColor = System.Drawing.Color.White;
            this.cboOpenEyeO1.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblTitle10
            // 
            this.lblTitle10.AutoSize = true;
            this.lblTitle10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle10.Location = new System.Drawing.Point(24, 40);
            this.lblTitle10.Name = "lblTitle10";
            this.lblTitle10.Size = new System.Drawing.Size(84, 14);
            this.lblTitle10.TabIndex = 323;
            this.lblTitle10.Text = "年龄 < 1岁";
            this.lblTitle10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle11
            // 
            this.lblTitle11.AutoSize = true;
            this.lblTitle11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle11.Location = new System.Drawing.Point(16, 76);
            this.lblTitle11.Name = "lblTitle11";
            this.lblTitle11.Size = new System.Drawing.Size(91, 14);
            this.lblTitle11.TabIndex = 323;
            this.lblTitle11.Text = "年龄 ≥ 1岁";
            this.lblTitle11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboSportU1
            // 
            this.cboSportU1.BackColor = System.Drawing.Color.White;
            this.cboSportU1.BorderColor = System.Drawing.Color.Black;
            this.cboSportU1.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboSportU1.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboSportU1.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboSportU1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboSportU1.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSportU1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSportU1.ForeColor = System.Drawing.Color.Black;
            this.cboSportU1.ListBackColor = System.Drawing.Color.White;
            this.cboSportU1.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboSportU1.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboSportU1.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboSportU1.Location = new System.Drawing.Point(356, 40);
            this.cboSportU1.m_BlnEnableItemEventMenu = true;
            this.cboSportU1.MaxLength = 32767;
            this.cboSportU1.Name = "cboSportU1";
            this.cboSportU1.SelectedIndex = -1;
            this.cboSportU1.SelectedItem = null;
            this.cboSportU1.SelectionStart = 0;
            this.cboSportU1.Size = new System.Drawing.Size(240, 23);
            this.cboSportU1.TabIndex = 140;
            this.cboSportU1.TextBackColor = System.Drawing.Color.White;
            this.cboSportU1.TextForeColor = System.Drawing.Color.Black;
            // 
            // cboSportO1
            // 
            this.cboSportO1.BackColor = System.Drawing.Color.White;
            this.cboSportO1.BorderColor = System.Drawing.Color.Black;
            this.cboSportO1.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboSportO1.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboSportO1.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboSportO1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboSportO1.Enabled = false;
            this.cboSportO1.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSportO1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSportO1.ForeColor = System.Drawing.Color.Black;
            this.cboSportO1.ListBackColor = System.Drawing.Color.White;
            this.cboSportO1.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboSportO1.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboSportO1.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboSportO1.Location = new System.Drawing.Point(356, 76);
            this.cboSportO1.m_BlnEnableItemEventMenu = true;
            this.cboSportO1.MaxLength = 32767;
            this.cboSportO1.Name = "cboSportO1";
            this.cboSportO1.SelectedIndex = -1;
            this.cboSportO1.SelectedItem = null;
            this.cboSportO1.SelectionStart = 0;
            this.cboSportO1.Size = new System.Drawing.Size(240, 23);
            this.cboSportO1.TabIndex = 150;
            this.cboSportO1.TextBackColor = System.Drawing.Color.White;
            this.cboSportO1.TextForeColor = System.Drawing.Color.Black;
            // 
            // cboLangU2
            // 
            this.cboLangU2.BackColor = System.Drawing.Color.White;
            this.cboLangU2.BorderColor = System.Drawing.Color.Black;
            this.cboLangU2.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboLangU2.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboLangU2.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboLangU2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboLangU2.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboLangU2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboLangU2.ForeColor = System.Drawing.Color.Black;
            this.cboLangU2.ListBackColor = System.Drawing.Color.White;
            this.cboLangU2.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboLangU2.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboLangU2.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboLangU2.Location = new System.Drawing.Point(112, 132);
            this.cboLangU2.m_BlnEnableItemEventMenu = true;
            this.cboLangU2.MaxLength = 32767;
            this.cboLangU2.Name = "cboLangU2";
            this.cboLangU2.SelectedIndex = -1;
            this.cboLangU2.SelectedItem = null;
            this.cboLangU2.SelectionStart = 0;
            this.cboLangU2.Size = new System.Drawing.Size(220, 23);
            this.cboLangU2.TabIndex = 160;
            this.cboLangU2.TextBackColor = System.Drawing.Color.White;
            this.cboLangU2.TextForeColor = System.Drawing.Color.Black;
            // 
            // cboLangU5
            // 
            this.cboLangU5.BackColor = System.Drawing.Color.White;
            this.cboLangU5.BorderColor = System.Drawing.Color.Black;
            this.cboLangU5.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboLangU5.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboLangU5.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboLangU5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboLangU5.Enabled = false;
            this.cboLangU5.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboLangU5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboLangU5.ForeColor = System.Drawing.Color.Black;
            this.cboLangU5.ListBackColor = System.Drawing.Color.White;
            this.cboLangU5.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboLangU5.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboLangU5.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboLangU5.Location = new System.Drawing.Point(112, 164);
            this.cboLangU5.m_BlnEnableItemEventMenu = true;
            this.cboLangU5.MaxLength = 32767;
            this.cboLangU5.Name = "cboLangU5";
            this.cboLangU5.SelectedIndex = -1;
            this.cboLangU5.SelectedItem = null;
            this.cboLangU5.SelectionStart = 0;
            this.cboLangU5.Size = new System.Drawing.Size(220, 23);
            this.cboLangU5.TabIndex = 170;
            this.cboLangU5.TextBackColor = System.Drawing.Color.White;
            this.cboLangU5.TextForeColor = System.Drawing.Color.Black;
            // 
            // cboLangO5
            // 
            this.cboLangO5.BackColor = System.Drawing.Color.White;
            this.cboLangO5.BorderColor = System.Drawing.Color.Black;
            this.cboLangO5.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboLangO5.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboLangO5.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboLangO5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboLangO5.Enabled = false;
            this.cboLangO5.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboLangO5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboLangO5.ForeColor = System.Drawing.Color.Black;
            this.cboLangO5.ListBackColor = System.Drawing.Color.White;
            this.cboLangO5.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboLangO5.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboLangO5.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboLangO5.Location = new System.Drawing.Point(112, 196);
            this.cboLangO5.m_BlnEnableItemEventMenu = true;
            this.cboLangO5.MaxLength = 32767;
            this.cboLangO5.Name = "cboLangO5";
            this.cboLangO5.SelectedIndex = -1;
            this.cboLangO5.SelectedItem = null;
            this.cboLangO5.SelectionStart = 0;
            this.cboLangO5.Size = new System.Drawing.Size(220, 23);
            this.cboLangO5.TabIndex = 180;
            this.cboLangO5.TextBackColor = System.Drawing.Color.White;
            this.cboLangO5.TextForeColor = System.Drawing.Color.Black;
            // 
            // cboPupil
            // 
            this.cboPupil.BackColor = System.Drawing.Color.White;
            this.cboPupil.BorderColor = System.Drawing.Color.Black;
            this.cboPupil.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboPupil.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboPupil.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboPupil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboPupil.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPupil.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPupil.ForeColor = System.Drawing.Color.Black;
            this.cboPupil.ListBackColor = System.Drawing.Color.White;
            this.cboPupil.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboPupil.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboPupil.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboPupil.Location = new System.Drawing.Point(356, 132);
            this.cboPupil.m_BlnEnableItemEventMenu = true;
            this.cboPupil.MaxLength = 32767;
            this.cboPupil.Name = "cboPupil";
            this.cboPupil.SelectedIndex = -1;
            this.cboPupil.SelectedItem = null;
            this.cboPupil.SelectionStart = 0;
            this.cboPupil.Size = new System.Drawing.Size(240, 23);
            this.cboPupil.TabIndex = 190;
            this.cboPupil.TextBackColor = System.Drawing.Color.White;
            this.cboPupil.TextForeColor = System.Drawing.Color.Black;
            // 
            // cboBrain
            // 
            this.cboBrain.BackColor = System.Drawing.Color.White;
            this.cboBrain.BorderColor = System.Drawing.Color.Black;
            this.cboBrain.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboBrain.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboBrain.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboBrain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboBrain.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboBrain.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboBrain.ForeColor = System.Drawing.Color.Black;
            this.cboBrain.ListBackColor = System.Drawing.Color.White;
            this.cboBrain.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboBrain.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboBrain.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboBrain.Location = new System.Drawing.Point(356, 196);
            this.cboBrain.m_BlnEnableItemEventMenu = true;
            this.cboBrain.MaxLength = 32767;
            this.cboBrain.Name = "cboBrain";
            this.cboBrain.SelectedIndex = -1;
            this.cboBrain.SelectedItem = null;
            this.cboBrain.SelectionStart = 0;
            this.cboBrain.Size = new System.Drawing.Size(240, 23);
            this.cboBrain.TabIndex = 200;
            this.cboBrain.TextBackColor = System.Drawing.Color.White;
            this.cboBrain.TextForeColor = System.Drawing.Color.Black;
            // 
            // cboSpontaneityBreath
            // 
            this.cboSpontaneityBreath.BackColor = System.Drawing.Color.White;
            this.cboSpontaneityBreath.BorderColor = System.Drawing.Color.Black;
            this.cboSpontaneityBreath.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboSpontaneityBreath.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboSpontaneityBreath.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboSpontaneityBreath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboSpontaneityBreath.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSpontaneityBreath.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSpontaneityBreath.ForeColor = System.Drawing.Color.Black;
            this.cboSpontaneityBreath.ListBackColor = System.Drawing.Color.White;
            this.cboSpontaneityBreath.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboSpontaneityBreath.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboSpontaneityBreath.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboSpontaneityBreath.Location = new System.Drawing.Point(356, 252);
            this.cboSpontaneityBreath.m_BlnEnableItemEventMenu = true;
            this.cboSpontaneityBreath.MaxLength = 32767;
            this.cboSpontaneityBreath.Name = "cboSpontaneityBreath";
            this.cboSpontaneityBreath.SelectedIndex = -1;
            this.cboSpontaneityBreath.SelectedItem = null;
            this.cboSpontaneityBreath.SelectionStart = 0;
            this.cboSpontaneityBreath.Size = new System.Drawing.Size(240, 23);
            this.cboSpontaneityBreath.TabIndex = 220;
            this.cboSpontaneityBreath.TextBackColor = System.Drawing.Color.White;
            this.cboSpontaneityBreath.TextForeColor = System.Drawing.Color.Black;
            // 
            // cboTwitch
            // 
            this.cboTwitch.BackColor = System.Drawing.Color.White;
            this.cboTwitch.BorderColor = System.Drawing.Color.Black;
            this.cboTwitch.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboTwitch.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboTwitch.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboTwitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboTwitch.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboTwitch.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboTwitch.ForeColor = System.Drawing.Color.Black;
            this.cboTwitch.ListBackColor = System.Drawing.Color.White;
            this.cboTwitch.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboTwitch.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboTwitch.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboTwitch.Location = new System.Drawing.Point(112, 252);
            this.cboTwitch.m_BlnEnableItemEventMenu = true;
            this.cboTwitch.MaxLength = 32767;
            this.cboTwitch.Name = "cboTwitch";
            this.cboTwitch.SelectedIndex = -1;
            this.cboTwitch.SelectedItem = null;
            this.cboTwitch.SelectionStart = 0;
            this.cboTwitch.Size = new System.Drawing.Size(220, 23);
            this.cboTwitch.TabIndex = 210;
            this.cboTwitch.TextBackColor = System.Drawing.Color.White;
            this.cboTwitch.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblTitle12
            // 
            this.lblTitle12.AutoSize = true;
            this.lblTitle12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle12.Location = new System.Drawing.Point(45, 132);
            this.lblTitle12.Name = "lblTitle12";
            this.lblTitle12.Size = new System.Drawing.Size(61, 14);
            this.lblTitle12.TabIndex = 326;
            this.lblTitle12.Text = "0～23月";
            this.lblTitle12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle13
            // 
            this.lblTitle13.AutoSize = true;
            this.lblTitle13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle13.Location = new System.Drawing.Point(52, 164);
            this.lblTitle13.Name = "lblTitle13";
            this.lblTitle13.Size = new System.Drawing.Size(53, 14);
            this.lblTitle13.TabIndex = 326;
            this.lblTitle13.Text = "2～5岁";
            this.lblTitle13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle14
            // 
            this.lblTitle14.AutoSize = true;
            this.lblTitle14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle14.Location = new System.Drawing.Point(60, 196);
            this.lblTitle14.Name = "lblTitle14";
            this.lblTitle14.Size = new System.Drawing.Size(46, 14);
            this.lblTitle14.TabIndex = 326;
            this.lblTitle14.Text = "> 5岁";
            this.lblTitle14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gpbAge
            // 
            this.gpbAge.Controls.Add(this.rdbU1);
            this.gpbAge.Controls.Add(this.rdbU2);
            this.gpbAge.Controls.Add(this.rdbU3);
            this.gpbAge.Controls.Add(this.rdbU4);
            this.gpbAge.Controls.Add(this.rdbU5);
            this.gpbAge.Controls.Add(this.rdbO5);
            this.gpbAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbAge.Location = new System.Drawing.Point(4, 107);
            this.gpbAge.Name = "gpbAge";
            this.gpbAge.Size = new System.Drawing.Size(156, 292);
            this.gpbAge.TabIndex = 59;
            this.gpbAge.TabStop = false;
            this.gpbAge.Text = "年龄组:";
            // 
            // rdbU1
            // 
            this.rdbU1.Checked = true;
            this.rdbU1.Location = new System.Drawing.Point(12, 36);
            this.rdbU1.Name = "rdbU1";
            this.rdbU1.Size = new System.Drawing.Size(75, 24);
            this.rdbU1.TabIndex = 60;
            this.rdbU1.TabStop = true;
            this.rdbU1.Tag = "0";
            this.rdbU1.Text = "1岁以下";
            this.rdbU1.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbU2
            // 
            this.rdbU2.Location = new System.Drawing.Point(12, 80);
            this.rdbU2.Name = "rdbU2";
            this.rdbU2.Size = new System.Drawing.Size(81, 20);
            this.rdbU2.TabIndex = 70;
            this.rdbU2.TabStop = true;
            this.rdbU2.Tag = "1";
            this.rdbU2.Text = "1岁～2岁";
            this.rdbU2.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbU3
            // 
            this.rdbU3.Location = new System.Drawing.Point(12, 120);
            this.rdbU3.Name = "rdbU3";
            this.rdbU3.Size = new System.Drawing.Size(84, 21);
            this.rdbU3.TabIndex = 80;
            this.rdbU3.TabStop = true;
            this.rdbU3.Tag = "2";
            this.rdbU3.Text = "2岁～3岁";
            this.rdbU3.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbU4
            // 
            this.rdbU4.Location = new System.Drawing.Point(12, 164);
            this.rdbU4.Name = "rdbU4";
            this.rdbU4.Size = new System.Drawing.Size(82, 20);
            this.rdbU4.TabIndex = 90;
            this.rdbU4.TabStop = true;
            this.rdbU4.Tag = "3";
            this.rdbU4.Text = "3岁～4岁";
            this.rdbU4.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbU5
            // 
            this.rdbU5.Location = new System.Drawing.Point(12, 204);
            this.rdbU5.Name = "rdbU5";
            this.rdbU5.Size = new System.Drawing.Size(86, 24);
            this.rdbU5.TabIndex = 100;
            this.rdbU5.TabStop = true;
            this.rdbU5.Tag = "4";
            this.rdbU5.Text = "4岁～5岁";
            this.rdbU5.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbO5
            // 
            this.rdbO5.Location = new System.Drawing.Point(16, 256);
            this.rdbO5.Name = "rdbO5";
            this.rdbO5.Size = new System.Drawing.Size(75, 20);
            this.rdbO5.TabIndex = 110;
            this.rdbO5.TabStop = true;
            this.rdbO5.Tag = "5";
            this.rdbO5.Text = "5岁以上";
            this.rdbO5.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // dtgResult
            // 
            this.dtgResult.AllowSorting = false;
            this.dtgResult.BackColor = System.Drawing.Color.White;
            this.dtgResult.BackgroundColor = System.Drawing.Color.White;
            this.dtgResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtgResult.CaptionBackColor = System.Drawing.Color.White;
            this.dtgResult.CaptionForeColor = System.Drawing.Color.Black;
            this.dtgResult.CaptionText = "改良Glasgow昏迷评分结果";
            this.dtgResult.DataMember = "";
            this.dtgResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtgResult.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dtgResult.Location = new System.Drawing.Point(8, 443);
            this.dtgResult.Name = "dtgResult";
            this.dtgResult.ReadOnly = true;
            this.dtgResult.Size = new System.Drawing.Size(840, 116);
            this.dtgResult.TabIndex = 328;
            this.dtgResult.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.dtgResult.TabStop = false;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.AllowSorting = false;
            this.dataGridTableStyle1.DataGrid = this.dtgResult;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn8,
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn5,
            this.dataGridTextBoxColumn6,
            this.dataGridTextBoxColumn7});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "result";
            this.dataGridTableStyle1.RowHeadersVisible = false;
            // 
            // dataGridTextBoxColumn8
            // 
            this.dataGridTextBoxColumn8.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn8.Format = "";
            this.dataGridTextBoxColumn8.FormatInfo = null;
            this.dataGridTextBoxColumn8.HeaderText = "  总   分";
            this.dataGridTextBoxColumn8.MappingName = "总分";
            this.dataGridTextBoxColumn8.NullText = "/";
            this.dataGridTextBoxColumn8.Width = 120;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "  睁  眼";
            this.dataGridTextBoxColumn1.MappingName = "睁眼";
            this.dataGridTextBoxColumn1.NullText = "/";
            this.dataGridTextBoxColumn1.Width = 120;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = " 运  动  反  应";
            this.dataGridTextBoxColumn2.MappingName = "运动反应";
            this.dataGridTextBoxColumn2.NullText = "/";
            this.dataGridTextBoxColumn2.Width = 120;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "  语  言  反  应";
            this.dataGridTextBoxColumn3.MappingName = "语言反应";
            this.dataGridTextBoxColumn3.NullText = "/";
            this.dataGridTextBoxColumn3.Width = 75;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "    瞳孔光反应";
            this.dataGridTextBoxColumn4.MappingName = "瞳孔光反应";
            this.dataGridTextBoxColumn4.NullText = "/";
            this.dataGridTextBoxColumn4.Width = 110;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = " 脑  干  反  射";
            this.dataGridTextBoxColumn5.MappingName = "脑干反射";
            this.dataGridTextBoxColumn5.NullText = "/";
            this.dataGridTextBoxColumn5.Width = 110;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "   抽    搐";
            this.dataGridTextBoxColumn6.MappingName = "抽搐";
            this.dataGridTextBoxColumn6.NullText = "/";
            this.dataGridTextBoxColumn6.Width = 75;
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = "   自发性呼吸";
            this.dataGridTextBoxColumn7.MappingName = "自发性呼吸";
            this.dataGridTextBoxColumn7.NullText = "/";
            this.dataGridTextBoxColumn7.Width = 130;
            // 
            // txtEvalDoctor
            // 
            this.txtEvalDoctor.BackColor = System.Drawing.Color.White;
            this.txtEvalDoctor.BorderColor = System.Drawing.Color.Black;
            this.txtEvalDoctor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEvalDoctor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEvalDoctor.ForeColor = System.Drawing.Color.Black;
            this.txtEvalDoctor.Location = new System.Drawing.Point(384, 410);
            this.txtEvalDoctor.Name = "txtEvalDoctor";
            this.txtEvalDoctor.Size = new System.Drawing.Size(124, 23);
            this.txtEvalDoctor.TabIndex = 240;
            // 
            // m_cmdEvalDoctor
            // 
            this.m_cmdEvalDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdEvalDoctor.DefaultScheme = true;
            this.m_cmdEvalDoctor.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdEvalDoctor.ForeColor = System.Drawing.Color.Black;
            this.m_cmdEvalDoctor.Hint = "";
            this.m_cmdEvalDoctor.Location = new System.Drawing.Point(296, 408);
            this.m_cmdEvalDoctor.Name = "m_cmdEvalDoctor";
            this.m_cmdEvalDoctor.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdEvalDoctor.Size = new System.Drawing.Size(80, 27);
            this.m_cmdEvalDoctor.TabIndex = 10000012;
            this.m_cmdEvalDoctor.Text = "评估者(&P)";
            // 
            // cmdCalculate
            // 
            this.cmdCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdCalculate.DefaultScheme = true;
            this.cmdCalculate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdCalculate.ForeColor = System.Drawing.Color.Black;
            this.cmdCalculate.Hint = "";
            this.cmdCalculate.Location = new System.Drawing.Point(672, 407);
            this.cmdCalculate.Name = "cmdCalculate";
            this.cmdCalculate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCalculate.Size = new System.Drawing.Size(100, 28);
            this.cmdCalculate.TabIndex = 10000012;
            this.cmdCalculate.Text = "计算分值(&E)";
            this.cmdCalculate.Click += new System.EventHandler(this.cmdCalculate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTitle3);
            this.groupBox2.Controls.Add(this.lblTitle4);
            this.groupBox2.Controls.Add(this.lblTitle11);
            this.groupBox2.Controls.Add(this.lblTitle10);
            this.groupBox2.Controls.Add(this.cboOpenEyeO1);
            this.groupBox2.Controls.Add(this.cboOpenEyeU1);
            this.groupBox2.Controls.Add(this.lblTitle5);
            this.groupBox2.Controls.Add(this.cboLangU5);
            this.groupBox2.Controls.Add(this.cboLangO5);
            this.groupBox2.Controls.Add(this.cboLangU2);
            this.groupBox2.Controls.Add(this.cboTwitch);
            this.groupBox2.Controls.Add(this.lblTitle12);
            this.groupBox2.Controls.Add(this.lblTitle9);
            this.groupBox2.Controls.Add(this.lblTitle13);
            this.groupBox2.Controls.Add(this.lblTitle14);
            this.groupBox2.Controls.Add(this.cboPupil);
            this.groupBox2.Controls.Add(this.cboBrain);
            this.groupBox2.Controls.Add(this.cboSpontaneityBreath);
            this.groupBox2.Controls.Add(this.cboSportO1);
            this.groupBox2.Controls.Add(this.cboSportU1);
            this.groupBox2.Controls.Add(this.lblTitle8);
            this.groupBox2.Controls.Add(this.lblTitle7);
            this.groupBox2.Controls.Add(this.lblTitle6);
            this.groupBox2.Location = new System.Drawing.Point(176, 107);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(671, 292);
            this.groupBox2.TabIndex = 10000014;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "评分内容";
            // 
            // frmImproveGlasgowComaEvaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.ClientSize = new System.Drawing.Size(866, 623);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gpbAge);
            this.Controls.Add(this.dtpEvalDate);
            this.Controls.Add(this.m_cmdEvalDoctor);
            this.Controls.Add(this.lblEvalDate);
            this.Controls.Add(this.txtEvalDoctor);
            this.Controls.Add(this.dtgResult);
            this.Controls.Add(this.cmdCalculate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmImproveGlasgowComaEvaluation";
            this.Text = "改良Glasgow昏迷评分";
            this.Load += new System.EventHandler(this.ImproveGlasgowComaEvaluation_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ImproveGlasgowComaEvaluation_Closing);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.cmdCalculate, 0);
            this.Controls.SetChildIndex(this.dtgResult, 0);
            this.Controls.SetChildIndex(this.txtEvalDoctor, 0);
            this.Controls.SetChildIndex(this.lblEvalDate, 0);
            this.Controls.SetChildIndex(this.m_cmdEvalDoctor, 0);
            this.Controls.SetChildIndex(this.dtpEvalDate, 0);
            this.Controls.SetChildIndex(this.gpbAge, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.gpbAge.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}
