using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iCare.ICU.Evaluation
{
    partial class frmNewBabyInjuryCaseEvaluation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewBabyInjuryCaseEvaluation));
            this.lblTitle31 = new System.Windows.Forms.Label();
            this.dtpEvalDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblEvalDate = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.txtHeartRate = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtBloodPress = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtShrinkPressure = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle5 = new System.Windows.Forms.Label();
            this.txtBreath = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtPao2kPa = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtpH = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle8 = new System.Windows.Forms.Label();
            this.lblTitle9 = new System.Windows.Forms.Label();
            this.txtCrumol = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtKPlus = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtNaPlus = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtPao2mmHg = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle11 = new System.Windows.Forms.Label();
            this.lblTitle12 = new System.Windows.Forms.Label();
            this.lblTitle14 = new System.Windows.Forms.Label();
            this.lblTitle15 = new System.Windows.Forms.Label();
            this.lblTitle16 = new System.Windows.Forms.Label();
            this.lblTitle17 = new System.Windows.Forms.Label();
            this.lblTitle18 = new System.Windows.Forms.Label();
            this.lblTitle19 = new System.Windows.Forms.Label();
            this.lblTitle20 = new System.Windows.Forms.Label();
            this.lblTitle21 = new System.Windows.Forms.Label();
            this.lblTitle13 = new System.Windows.Forms.Label();
            this.txtCrmg = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle23 = new System.Windows.Forms.Label();
            this.txtBUNmmol = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtBUNmg = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle25 = new System.Windows.Forms.Label();
            this.txtRedCellComp = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle26 = new System.Windows.Forms.Label();
            this.lblTitle28 = new System.Windows.Forms.Label();
            this.cboStomach = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.rdbBloodPress = new System.Windows.Forms.RadioButton();
            this.rdbShrinkPress = new System.Windows.Forms.RadioButton();
            this.gpbCrAndBUN = new System.Windows.Forms.GroupBox();
            this.lblTitle10 = new System.Windows.Forms.Label();
            this.lblTitle6 = new System.Windows.Forms.Label();
            this.rdbCrumol = new System.Windows.Forms.RadioButton();
            this.rdbCrmg = new System.Windows.Forms.RadioButton();
            this.rdbBUNmmol = new System.Windows.Forms.RadioButton();
            this.rdbBUNmg = new System.Windows.Forms.RadioButton();
            this.rdbPao2kPa = new System.Windows.Forms.RadioButton();
            this.rdbPao2mmHg = new System.Windows.Forms.RadioButton();
            this.gpbBloodAndShk = new System.Windows.Forms.GroupBox();
            this.gpbPao2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.lblTitle4 = new System.Windows.Forms.Label();
            this.dtgResult = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn11 = new System.Windows.Forms.DataGridTextBoxColumn();
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
            this.dtpStartSample = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblTitle96 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAutoTime = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.timAutoCollect = new System.Timers.Timer();
            this.gpbAutoEval = new System.Windows.Forms.GroupBox();
            this.cmdStartAuto = new PinkieControls.ButtonXP();
            this.cmdStopAuto = new PinkieControls.ButtonXP();
            this.cmdShowResult = new PinkieControls.ButtonXP();
            this.cmdGetData = new PinkieControls.ButtonXP();
            this.lbltxtEvalDoctor = new System.Windows.Forms.Label();
            this.cmdCalculate = new PinkieControls.ButtonXP();
            this.m_cmdGetCheckData = new PinkieControls.ButtonXP();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ctmClearUp = new System.Windows.Forms.Button();
            this.m_cmdGetDovueData = new System.Windows.Forms.Button();
            this.m_cmdSetLabCheckResult = new System.Windows.Forms.Button();
            this.clmPat_c_name = new System.Windows.Forms.ColumnHeader();
            this.clmSendDate = new System.Windows.Forms.ColumnHeader();
            this.m_lsvJY_ItemChoice = new System.Windows.Forms.ListView();
            this.m_pnlNewBase.SuspendLayout();
            this.gpbCrAndBUN.SuspendLayout();
            this.gpbBloodAndShk.SuspendLayout();
            this.gpbPao2.SuspendLayout();
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
            // lblTitle31
            // 
            this.lblTitle31.AutoSize = true;
            this.lblTitle31.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle31.Location = new System.Drawing.Point(363, 313);
            this.lblTitle31.Name = "lblTitle31";
            this.lblTitle31.Size = new System.Drawing.Size(67, 14);
            this.lblTitle31.TabIndex = 336;
            this.lblTitle31.Text = "评估者：";
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
            this.dtpEvalDate.Location = new System.Drawing.Point(80, 309);
            this.dtpEvalDate.m_BlnOnlyTime = false;
            this.dtpEvalDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpEvalDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEvalDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEvalDate.Name = "dtpEvalDate";
            this.dtpEvalDate.ReadOnly = false;
            this.dtpEvalDate.Size = new System.Drawing.Size(217, 22);
            this.dtpEvalDate.TabIndex = 280;
            this.dtpEvalDate.TextBackColor = System.Drawing.Color.White;
            this.dtpEvalDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblEvalDate
            // 
            this.lblEvalDate.AutoSize = true;
            this.lblEvalDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEvalDate.Location = new System.Drawing.Point(3, 313);
            this.lblEvalDate.Name = "lblEvalDate";
            this.lblEvalDate.Size = new System.Drawing.Size(82, 14);
            this.lblEvalDate.TabIndex = 333;
            this.lblEvalDate.Text = "评分日期：";
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle2.Location = new System.Drawing.Point(8, 28);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(45, 14);
            this.lblTitle2.TabIndex = 338;
            this.lblTitle2.Text = "心率:";
            // 
            // txtHeartRate
            // 
            this.txtHeartRate.AccessibleDescription = "心率";
            this.txtHeartRate.BackColor = System.Drawing.Color.White;
            this.txtHeartRate.BorderColor = System.Drawing.Color.Black;
            this.txtHeartRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHeartRate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHeartRate.ForeColor = System.Drawing.Color.Black;
            this.txtHeartRate.Location = new System.Drawing.Point(56, 24);
            this.txtHeartRate.MaxLength = 8;
            this.txtHeartRate.Name = "txtHeartRate";
            this.txtHeartRate.Size = new System.Drawing.Size(101, 23);
            this.txtHeartRate.TabIndex = 50;
            this.txtHeartRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBloodPress
            // 
            this.txtBloodPress.BackColor = System.Drawing.Color.White;
            this.txtBloodPress.BorderColor = System.Drawing.Color.Black;
            this.txtBloodPress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBloodPress.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBloodPress.ForeColor = System.Drawing.Color.Black;
            this.txtBloodPress.Location = new System.Drawing.Point(46, 25);
            this.txtBloodPress.MaxLength = 8;
            this.txtBloodPress.Name = "txtBloodPress";
            this.txtBloodPress.Size = new System.Drawing.Size(99, 23);
            this.txtBloodPress.TabIndex = 170;
            this.txtBloodPress.Tag = "0";
            this.txtBloodPress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtShrinkPressure
            // 
            this.txtShrinkPressure.BackColor = System.Drawing.Color.White;
            this.txtShrinkPressure.BorderColor = System.Drawing.Color.Black;
            this.txtShrinkPressure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShrinkPressure.Enabled = false;
            this.txtShrinkPressure.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtShrinkPressure.ForeColor = System.Drawing.Color.Black;
            this.txtShrinkPressure.Location = new System.Drawing.Point(46, 60);
            this.txtShrinkPressure.MaxLength = 8;
            this.txtShrinkPressure.Name = "txtShrinkPressure";
            this.txtShrinkPressure.Size = new System.Drawing.Size(99, 23);
            this.txtShrinkPressure.TabIndex = 190;
            this.txtShrinkPressure.Tag = "1";
            this.txtShrinkPressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTitle5
            // 
            this.lblTitle5.AutoSize = true;
            this.lblTitle5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle5.Location = new System.Drawing.Point(8, 64);
            this.lblTitle5.Name = "lblTitle5";
            this.lblTitle5.Size = new System.Drawing.Size(45, 14);
            this.lblTitle5.TabIndex = 338;
            this.lblTitle5.Text = "呼吸:";
            // 
            // txtBreath
            // 
            this.txtBreath.AccessibleDescription = "呼吸";
            this.txtBreath.BackColor = System.Drawing.Color.White;
            this.txtBreath.BorderColor = System.Drawing.Color.Black;
            this.txtBreath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBreath.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBreath.ForeColor = System.Drawing.Color.Black;
            this.txtBreath.Location = new System.Drawing.Point(56, 60);
            this.txtBreath.MaxLength = 8;
            this.txtBreath.Name = "txtBreath";
            this.txtBreath.Size = new System.Drawing.Size(101, 23);
            this.txtBreath.TabIndex = 70;
            this.txtBreath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPao2kPa
            // 
            this.txtPao2kPa.BackColor = System.Drawing.Color.White;
            this.txtPao2kPa.BorderColor = System.Drawing.Color.Black;
            this.txtPao2kPa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPao2kPa.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPao2kPa.ForeColor = System.Drawing.Color.Black;
            this.txtPao2kPa.Location = new System.Drawing.Point(56, 23);
            this.txtPao2kPa.Name = "txtPao2kPa";
            this.txtPao2kPa.Size = new System.Drawing.Size(101, 23);
            this.txtPao2kPa.TabIndex = 130;
            this.txtPao2kPa.Tag = "0";
            this.txtPao2kPa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtpH
            // 
            this.txtpH.AccessibleDescription = "Ph";
            this.txtpH.BackColor = System.Drawing.Color.White;
            this.txtpH.BorderColor = System.Drawing.Color.Black;
            this.txtpH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpH.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtpH.ForeColor = System.Drawing.Color.Black;
            this.txtpH.Location = new System.Drawing.Point(60, 20);
            this.txtpH.MaxLength = 8;
            this.txtpH.Name = "txtpH";
            this.txtpH.Size = new System.Drawing.Size(101, 23);
            this.txtpH.TabIndex = 90;
            this.txtpH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTitle8
            // 
            this.lblTitle8.AutoSize = true;
            this.lblTitle8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle8.Location = new System.Drawing.Point(8, 56);
            this.lblTitle8.Name = "lblTitle8";
            this.lblTitle8.Size = new System.Drawing.Size(54, 14);
            this.lblTitle8.TabIndex = 338;
            this.lblTitle8.Text = "Na  ：";
            // 
            // lblTitle9
            // 
            this.lblTitle9.AutoSize = true;
            this.lblTitle9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle9.Location = new System.Drawing.Point(8, 22);
            this.lblTitle9.Name = "lblTitle9";
            this.lblTitle9.Size = new System.Drawing.Size(46, 14);
            this.lblTitle9.TabIndex = 338;
            this.lblTitle9.Text = "K  ：";
            // 
            // txtCrumol
            // 
            this.txtCrumol.BackColor = System.Drawing.Color.White;
            this.txtCrumol.BorderColor = System.Drawing.Color.Black;
            this.txtCrumol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCrumol.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCrumol.ForeColor = System.Drawing.Color.Black;
            this.txtCrumol.Location = new System.Drawing.Point(28, 26);
            this.txtCrumol.MaxLength = 8;
            this.txtCrumol.Name = "txtCrumol";
            this.txtCrumol.Size = new System.Drawing.Size(92, 23);
            this.txtCrumol.TabIndex = 210;
            this.txtCrumol.Tag = "0";
            this.txtCrumol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtKPlus
            // 
            this.txtKPlus.AccessibleDescription = "K+";
            this.txtKPlus.BackColor = System.Drawing.Color.White;
            this.txtKPlus.BorderColor = System.Drawing.Color.Black;
            this.txtKPlus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKPlus.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtKPlus.ForeColor = System.Drawing.Color.Black;
            this.txtKPlus.Location = new System.Drawing.Point(48, 20);
            this.txtKPlus.MaxLength = 8;
            this.txtKPlus.Name = "txtKPlus";
            this.txtKPlus.Size = new System.Drawing.Size(96, 23);
            this.txtKPlus.TabIndex = 60;
            this.txtKPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNaPlus
            // 
            this.txtNaPlus.AccessibleDescription = "Na+";
            this.txtNaPlus.BackColor = System.Drawing.Color.White;
            this.txtNaPlus.BorderColor = System.Drawing.Color.Black;
            this.txtNaPlus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNaPlus.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNaPlus.ForeColor = System.Drawing.Color.Black;
            this.txtNaPlus.Location = new System.Drawing.Point(48, 52);
            this.txtNaPlus.MaxLength = 8;
            this.txtNaPlus.Name = "txtNaPlus";
            this.txtNaPlus.Size = new System.Drawing.Size(96, 23);
            this.txtNaPlus.TabIndex = 80;
            this.txtNaPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPao2mmHg
            // 
            this.txtPao2mmHg.BackColor = System.Drawing.Color.White;
            this.txtPao2mmHg.BorderColor = System.Drawing.Color.Black;
            this.txtPao2mmHg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPao2mmHg.Enabled = false;
            this.txtPao2mmHg.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPao2mmHg.ForeColor = System.Drawing.Color.Black;
            this.txtPao2mmHg.Location = new System.Drawing.Point(56, 53);
            this.txtPao2mmHg.Name = "txtPao2mmHg";
            this.txtPao2mmHg.Size = new System.Drawing.Size(101, 23);
            this.txtPao2mmHg.TabIndex = 150;
            this.txtPao2mmHg.Tag = "1";
            this.txtPao2mmHg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTitle11
            // 
            this.lblTitle11.AutoSize = true;
            this.lblTitle11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle11.Location = new System.Drawing.Point(4, 20);
            this.lblTitle11.Name = "lblTitle11";
            this.lblTitle11.Size = new System.Drawing.Size(38, 14);
            this.lblTitle11.TabIndex = 338;
            this.lblTitle11.Text = "pH：";
            // 
            // lblTitle12
            // 
            this.lblTitle12.AutoSize = true;
            this.lblTitle12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle12.Location = new System.Drawing.Point(160, 28);
            this.lblTitle12.Name = "lblTitle12";
            this.lblTitle12.Size = new System.Drawing.Size(45, 14);
            this.lblTitle12.TabIndex = 338;
            this.lblTitle12.Text = "次/分";
            // 
            // lblTitle14
            // 
            this.lblTitle14.AutoSize = true;
            this.lblTitle14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle14.Location = new System.Drawing.Point(128, 28);
            this.lblTitle14.Name = "lblTitle14";
            this.lblTitle14.Size = new System.Drawing.Size(62, 14);
            this.lblTitle14.TabIndex = 338;
            this.lblTitle14.Text = "μmol/L";
            // 
            // lblTitle15
            // 
            this.lblTitle15.AutoSize = true;
            this.lblTitle15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle15.Location = new System.Drawing.Point(151, 64);
            this.lblTitle15.Name = "lblTitle15";
            this.lblTitle15.Size = new System.Drawing.Size(39, 14);
            this.lblTitle15.TabIndex = 338;
            this.lblTitle15.Text = "mmHg";
            // 
            // lblTitle16
            // 
            this.lblTitle16.AutoSize = true;
            this.lblTitle16.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle16.Location = new System.Drawing.Point(148, 22);
            this.lblTitle16.Name = "lblTitle16";
            this.lblTitle16.Size = new System.Drawing.Size(55, 14);
            this.lblTitle16.TabIndex = 338;
            this.lblTitle16.Text = "mmol/L";
            // 
            // lblTitle17
            // 
            this.lblTitle17.AutoSize = true;
            this.lblTitle17.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle17.Location = new System.Drawing.Point(152, 56);
            this.lblTitle17.Name = "lblTitle17";
            this.lblTitle17.Size = new System.Drawing.Size(55, 14);
            this.lblTitle17.TabIndex = 338;
            this.lblTitle17.Text = "mmol/L";
            // 
            // lblTitle18
            // 
            this.lblTitle18.AutoSize = true;
            this.lblTitle18.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle18.Location = new System.Drawing.Point(160, 55);
            this.lblTitle18.Name = "lblTitle18";
            this.lblTitle18.Size = new System.Drawing.Size(39, 14);
            this.lblTitle18.TabIndex = 338;
            this.lblTitle18.Text = "mmHg";
            // 
            // lblTitle19
            // 
            this.lblTitle19.AutoSize = true;
            this.lblTitle19.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle19.Location = new System.Drawing.Point(160, 64);
            this.lblTitle19.Name = "lblTitle19";
            this.lblTitle19.Size = new System.Drawing.Size(45, 14);
            this.lblTitle19.TabIndex = 338;
            this.lblTitle19.Text = "次/分";
            // 
            // lblTitle20
            // 
            this.lblTitle20.AutoSize = true;
            this.lblTitle20.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle20.Location = new System.Drawing.Point(160, 25);
            this.lblTitle20.Name = "lblTitle20";
            this.lblTitle20.Size = new System.Drawing.Size(31, 14);
            this.lblTitle20.TabIndex = 338;
            this.lblTitle20.Text = "kPa";
            // 
            // lblTitle21
            // 
            this.lblTitle21.AutoSize = true;
            this.lblTitle21.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle21.Location = new System.Drawing.Point(151, 25);
            this.lblTitle21.Name = "lblTitle21";
            this.lblTitle21.Size = new System.Drawing.Size(31, 14);
            this.lblTitle21.TabIndex = 338;
            this.lblTitle21.Text = "kPa";
            // 
            // lblTitle13
            // 
            this.lblTitle13.AutoSize = true;
            this.lblTitle13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle13.Location = new System.Drawing.Point(128, 64);
            this.lblTitle13.Name = "lblTitle13";
            this.lblTitle13.Size = new System.Drawing.Size(47, 14);
            this.lblTitle13.TabIndex = 338;
            this.lblTitle13.Text = "mg/dl";
            // 
            // txtCrmg
            // 
            this.txtCrmg.BackColor = System.Drawing.Color.White;
            this.txtCrmg.BorderColor = System.Drawing.Color.Black;
            this.txtCrmg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCrmg.Enabled = false;
            this.txtCrmg.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCrmg.ForeColor = System.Drawing.Color.Black;
            this.txtCrmg.Location = new System.Drawing.Point(28, 60);
            this.txtCrmg.MaxLength = 8;
            this.txtCrmg.Name = "txtCrmg";
            this.txtCrmg.Size = new System.Drawing.Size(92, 23);
            this.txtCrmg.TabIndex = 230;
            this.txtCrmg.Tag = "1";
            this.txtCrmg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTitle23
            // 
            this.lblTitle23.AutoSize = true;
            this.lblTitle23.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle23.Location = new System.Drawing.Point(332, 28);
            this.lblTitle23.Name = "lblTitle23";
            this.lblTitle23.Size = new System.Drawing.Size(55, 14);
            this.lblTitle23.TabIndex = 338;
            this.lblTitle23.Text = "mmol/L";
            // 
            // txtBUNmmol
            // 
            this.txtBUNmmol.BackColor = System.Drawing.Color.White;
            this.txtBUNmmol.BorderColor = System.Drawing.Color.Black;
            this.txtBUNmmol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBUNmmol.Enabled = false;
            this.txtBUNmmol.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBUNmmol.ForeColor = System.Drawing.Color.Black;
            this.txtBUNmmol.Location = new System.Drawing.Point(232, 26);
            this.txtBUNmmol.MaxLength = 8;
            this.txtBUNmmol.Name = "txtBUNmmol";
            this.txtBUNmmol.Size = new System.Drawing.Size(100, 23);
            this.txtBUNmmol.TabIndex = 250;
            this.txtBUNmmol.Tag = "2";
            this.txtBUNmmol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBUNmg
            // 
            this.txtBUNmg.BackColor = System.Drawing.Color.White;
            this.txtBUNmg.BorderColor = System.Drawing.Color.Black;
            this.txtBUNmg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBUNmg.Enabled = false;
            this.txtBUNmg.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBUNmg.ForeColor = System.Drawing.Color.Black;
            this.txtBUNmg.Location = new System.Drawing.Point(232, 60);
            this.txtBUNmg.MaxLength = 8;
            this.txtBUNmg.Name = "txtBUNmg";
            this.txtBUNmg.Size = new System.Drawing.Size(100, 23);
            this.txtBUNmg.TabIndex = 270;
            this.txtBUNmg.Tag = "3";
            this.txtBUNmg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTitle25
            // 
            this.lblTitle25.AutoSize = true;
            this.lblTitle25.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle25.Location = new System.Drawing.Point(332, 64);
            this.lblTitle25.Name = "lblTitle25";
            this.lblTitle25.Size = new System.Drawing.Size(47, 14);
            this.lblTitle25.TabIndex = 338;
            this.lblTitle25.Text = "mg/dl";
            // 
            // txtRedCellComp
            // 
            this.txtRedCellComp.AccessibleDescription = "红细胞积压比";
            this.txtRedCellComp.BackColor = System.Drawing.Color.White;
            this.txtRedCellComp.BorderColor = System.Drawing.Color.Black;
            this.txtRedCellComp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRedCellComp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRedCellComp.ForeColor = System.Drawing.Color.Black;
            this.txtRedCellComp.Location = new System.Drawing.Point(60, 56);
            this.txtRedCellComp.MaxLength = 8;
            this.txtRedCellComp.Name = "txtRedCellComp";
            this.txtRedCellComp.Size = new System.Drawing.Size(100, 23);
            this.txtRedCellComp.TabIndex = 100;
            this.txtRedCellComp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTitle26
            // 
            this.lblTitle26.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle26.Location = new System.Drawing.Point(4, 48);
            this.lblTitle26.Name = "lblTitle26";
            this.lblTitle26.Size = new System.Drawing.Size(56, 32);
            this.lblTitle26.TabIndex = 338;
            this.lblTitle26.Text = "红细胞积压比：";
            // 
            // lblTitle28
            // 
            this.lblTitle28.AutoSize = true;
            this.lblTitle28.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle28.Location = new System.Drawing.Point(631, 237);
            this.lblTitle28.Name = "lblTitle28";
            this.lblTitle28.Size = new System.Drawing.Size(82, 14);
            this.lblTitle28.TabIndex = 338;
            this.lblTitle28.Text = "肠胃表现：";
            // 
            // cboStomach
            // 
            this.cboStomach.BackColor = System.Drawing.Color.White;
            this.cboStomach.BorderColor = System.Drawing.Color.Black;
            this.cboStomach.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.cboStomach.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.cboStomach.DropButtonForeColor = System.Drawing.Color.Black;
            this.cboStomach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboStomach.flatFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboStomach.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboStomach.ForeColor = System.Drawing.Color.Black;
            this.cboStomach.ListBackColor = System.Drawing.Color.White;
            this.cboStomach.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboStomach.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboStomach.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboStomach.Location = new System.Drawing.Point(631, 269);
            this.cboStomach.m_BlnEnableItemEventMenu = true;
            this.cboStomach.MaxLength = 32767;
            this.cboStomach.Name = "cboStomach";
            this.cboStomach.SelectedIndex = -1;
            this.cboStomach.SelectedItem = null;
            this.cboStomach.SelectionStart = 0;
            this.cboStomach.Size = new System.Drawing.Size(217, 23);
            this.cboStomach.TabIndex = 110;
            this.cboStomach.TextBackColor = System.Drawing.Color.White;
            this.cboStomach.TextForeColor = System.Drawing.Color.Black;
            // 
            // rdbBloodPress
            // 
            this.rdbBloodPress.Checked = true;
            this.rdbBloodPress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbBloodPress.Location = new System.Drawing.Point(18, 24);
            this.rdbBloodPress.Name = "rdbBloodPress";
            this.rdbBloodPress.Size = new System.Drawing.Size(29, 20);
            this.rdbBloodPress.TabIndex = 160;
            this.rdbBloodPress.TabStop = true;
            this.rdbBloodPress.Tag = "0";
            this.rdbBloodPress.CheckedChanged += new System.EventHandler(this.PressChange);
            // 
            // rdbShrinkPress
            // 
            this.rdbShrinkPress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbShrinkPress.Location = new System.Drawing.Point(18, 64);
            this.rdbShrinkPress.Name = "rdbShrinkPress";
            this.rdbShrinkPress.Size = new System.Drawing.Size(29, 20);
            this.rdbShrinkPress.TabIndex = 180;
            this.rdbShrinkPress.TabStop = true;
            this.rdbShrinkPress.Tag = "1";
            this.rdbShrinkPress.CheckedChanged += new System.EventHandler(this.PressChange);
            // 
            // gpbCrAndBUN
            // 
            this.gpbCrAndBUN.Controls.Add(this.lblTitle10);
            this.gpbCrAndBUN.Controls.Add(this.lblTitle6);
            this.gpbCrAndBUN.Controls.Add(this.rdbCrumol);
            this.gpbCrAndBUN.Controls.Add(this.txtCrumol);
            this.gpbCrAndBUN.Controls.Add(this.lblTitle14);
            this.gpbCrAndBUN.Controls.Add(this.txtCrmg);
            this.gpbCrAndBUN.Controls.Add(this.lblTitle13);
            this.gpbCrAndBUN.Controls.Add(this.rdbCrmg);
            this.gpbCrAndBUN.Controls.Add(this.rdbBUNmmol);
            this.gpbCrAndBUN.Controls.Add(this.rdbBUNmg);
            this.gpbCrAndBUN.Controls.Add(this.lblTitle25);
            this.gpbCrAndBUN.Controls.Add(this.txtBUNmg);
            this.gpbCrAndBUN.Controls.Add(this.txtBUNmmol);
            this.gpbCrAndBUN.Controls.Add(this.lblTitle23);
            this.gpbCrAndBUN.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbCrAndBUN.Location = new System.Drawing.Point(451, 105);
            this.gpbCrAndBUN.Name = "gpbCrAndBUN";
            this.gpbCrAndBUN.Size = new System.Drawing.Size(400, 104);
            this.gpbCrAndBUN.TabIndex = 199;
            this.gpbCrAndBUN.TabStop = false;
            // 
            // lblTitle10
            // 
            this.lblTitle10.AutoSize = true;
            this.lblTitle10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle10.Location = new System.Drawing.Point(283, 4);
            this.lblTitle10.Name = "lblTitle10";
            this.lblTitle10.Size = new System.Drawing.Size(46, 14);
            this.lblTitle10.TabIndex = 341;
            this.lblTitle10.Text = "BUN：";
            // 
            // lblTitle6
            // 
            this.lblTitle6.AutoSize = true;
            this.lblTitle6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle6.Location = new System.Drawing.Point(69, 4);
            this.lblTitle6.Name = "lblTitle6";
            this.lblTitle6.Size = new System.Drawing.Size(38, 14);
            this.lblTitle6.TabIndex = 341;
            this.lblTitle6.Text = "Cr：";
            // 
            // rdbCrumol
            // 
            this.rdbCrumol.Checked = true;
            this.rdbCrumol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbCrumol.Location = new System.Drawing.Point(8, 27);
            this.rdbCrumol.Name = "rdbCrumol";
            this.rdbCrumol.Size = new System.Drawing.Size(16, 20);
            this.rdbCrumol.TabIndex = 200;
            this.rdbCrumol.TabStop = true;
            this.rdbCrumol.Tag = "0";
            this.rdbCrumol.CheckedChanged += new System.EventHandler(this.CrChanged);
            // 
            // rdbCrmg
            // 
            this.rdbCrmg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbCrmg.Location = new System.Drawing.Point(8, 64);
            this.rdbCrmg.Name = "rdbCrmg";
            this.rdbCrmg.Size = new System.Drawing.Size(16, 20);
            this.rdbCrmg.TabIndex = 220;
            this.rdbCrmg.TabStop = true;
            this.rdbCrmg.Tag = "1";
            this.rdbCrmg.CheckedChanged += new System.EventHandler(this.CrChanged);
            // 
            // rdbBUNmmol
            // 
            this.rdbBUNmmol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbBUNmmol.Location = new System.Drawing.Point(204, 27);
            this.rdbBUNmmol.Name = "rdbBUNmmol";
            this.rdbBUNmmol.Size = new System.Drawing.Size(16, 20);
            this.rdbBUNmmol.TabIndex = 240;
            this.rdbBUNmmol.TabStop = true;
            this.rdbBUNmmol.Tag = "2";
            this.rdbBUNmmol.CheckedChanged += new System.EventHandler(this.BUNChanged);
            // 
            // rdbBUNmg
            // 
            this.rdbBUNmg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbBUNmg.Location = new System.Drawing.Point(204, 64);
            this.rdbBUNmg.Name = "rdbBUNmg";
            this.rdbBUNmg.Size = new System.Drawing.Size(16, 20);
            this.rdbBUNmg.TabIndex = 260;
            this.rdbBUNmg.TabStop = true;
            this.rdbBUNmg.Tag = "3";
            this.rdbBUNmg.CheckedChanged += new System.EventHandler(this.BUNChanged);
            // 
            // rdbPao2kPa
            // 
            this.rdbPao2kPa.Checked = true;
            this.rdbPao2kPa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbPao2kPa.Location = new System.Drawing.Point(28, 24);
            this.rdbPao2kPa.Name = "rdbPao2kPa";
            this.rdbPao2kPa.Size = new System.Drawing.Size(16, 20);
            this.rdbPao2kPa.TabIndex = 120;
            this.rdbPao2kPa.TabStop = true;
            this.rdbPao2kPa.Tag = "0";
            this.rdbPao2kPa.CheckedChanged += new System.EventHandler(this.Pao2Changed);
            // 
            // rdbPao2mmHg
            // 
            this.rdbPao2mmHg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbPao2mmHg.Location = new System.Drawing.Point(28, 54);
            this.rdbPao2mmHg.Name = "rdbPao2mmHg";
            this.rdbPao2mmHg.Size = new System.Drawing.Size(16, 20);
            this.rdbPao2mmHg.TabIndex = 140;
            this.rdbPao2mmHg.Tag = "1";
            this.rdbPao2mmHg.CheckedChanged += new System.EventHandler(this.Pao2Changed);
            // 
            // gpbBloodAndShk
            // 
            this.gpbBloodAndShk.Controls.Add(this.txtShrinkPressure);
            this.gpbBloodAndShk.Controls.Add(this.rdbShrinkPress);
            this.gpbBloodAndShk.Controls.Add(this.lblTitle15);
            this.gpbBloodAndShk.Controls.Add(this.txtBloodPress);
            this.gpbBloodAndShk.Controls.Add(this.rdbBloodPress);
            this.gpbBloodAndShk.Controls.Add(this.lblTitle21);
            this.gpbBloodAndShk.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbBloodAndShk.Location = new System.Drawing.Point(235, 105);
            this.gpbBloodAndShk.Name = "gpbBloodAndShk";
            this.gpbBloodAndShk.Size = new System.Drawing.Size(211, 104);
            this.gpbBloodAndShk.TabIndex = 159;
            this.gpbBloodAndShk.TabStop = false;
            this.gpbBloodAndShk.Text = "血压（收缩压）：";
            // 
            // gpbPao2
            // 
            this.gpbPao2.Controls.Add(this.lblTitle20);
            this.gpbPao2.Controls.Add(this.lblTitle18);
            this.gpbPao2.Controls.Add(this.txtPao2mmHg);
            this.gpbPao2.Controls.Add(this.txtPao2kPa);
            this.gpbPao2.Controls.Add(this.rdbPao2mmHg);
            this.gpbPao2.Controls.Add(this.rdbPao2kPa);
            this.gpbPao2.Controls.Add(this.label7);
            this.gpbPao2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbPao2.Location = new System.Drawing.Point(7, 213);
            this.gpbPao2.Name = "gpbPao2";
            this.gpbPao2.Size = new System.Drawing.Size(216, 88);
            this.gpbPao2.TabIndex = 119;
            this.gpbPao2.TabStop = false;
            this.gpbPao2.Text = "PaO  ：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(37, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 9);
            this.label7.TabIndex = 346;
            this.label7.Text = "2";
            // 
            // lblTitle3
            // 
            this.lblTitle3.AutoSize = true;
            this.lblTitle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle3.Location = new System.Drawing.Point(28, 56);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(15, 14);
            this.lblTitle3.TabIndex = 346;
            this.lblTitle3.Text = "+";
            // 
            // lblTitle4
            // 
            this.lblTitle4.AutoSize = true;
            this.lblTitle4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle4.Location = new System.Drawing.Point(20, 22);
            this.lblTitle4.Name = "lblTitle4";
            this.lblTitle4.Size = new System.Drawing.Size(15, 14);
            this.lblTitle4.TabIndex = 346;
            this.lblTitle4.Text = "+";
            // 
            // dtgResult
            // 
            this.dtgResult.BackColor = System.Drawing.Color.White;
            this.dtgResult.BackgroundColor = System.Drawing.Color.White;
            this.dtgResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtgResult.CaptionBackColor = System.Drawing.Color.White;
            this.dtgResult.CaptionForeColor = System.Drawing.Color.Black;
            this.dtgResult.CaptionText = "新生儿危重病例评分法（草案）结果";
            this.dtgResult.DataMember = "";
            this.dtgResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtgResult.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dtgResult.Location = new System.Drawing.Point(3, 345);
            this.dtgResult.Name = "dtgResult";
            this.dtgResult.ReadOnly = true;
            this.dtgResult.Size = new System.Drawing.Size(847, 120);
            this.dtgResult.TabIndex = 347;
            this.dtgResult.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.dtgResult.TabStop = false;
            this.dtgResult.Navigate += new System.Windows.Forms.NavigateEventHandler(this.dtgResult_Navigate);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.AllowSorting = false;
            this.dataGridTableStyle1.DataGrid = this.dtgResult;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn11,
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn5,
            this.dataGridTextBoxColumn6,
            this.dataGridTextBoxColumn7,
            this.dataGridTextBoxColumn8,
            this.dataGridTextBoxColumn9,
            this.dataGridTextBoxColumn10});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "result";
            this.dataGridTableStyle1.RowHeadersVisible = false;
            // 
            // dataGridTextBoxColumn11
            // 
            this.dataGridTextBoxColumn11.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn11.Format = "";
            this.dataGridTextBoxColumn11.FormatInfo = null;
            this.dataGridTextBoxColumn11.HeaderText = "总分";
            this.dataGridTextBoxColumn11.MappingName = "总分";
            this.dataGridTextBoxColumn11.NullText = "/";
            this.dataGridTextBoxColumn11.Width = 105;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "心率";
            this.dataGridTextBoxColumn1.MappingName = "心率";
            this.dataGridTextBoxColumn1.NullText = "/";
            this.dataGridTextBoxColumn1.Width = 75;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "血压（收缩压）";
            this.dataGridTextBoxColumn2.MappingName = "血压（收缩压）";
            this.dataGridTextBoxColumn2.NullText = "/";
            this.dataGridTextBoxColumn2.Width = 102;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "呼吸";
            this.dataGridTextBoxColumn3.MappingName = "呼吸";
            this.dataGridTextBoxColumn3.NullText = "/";
            this.dataGridTextBoxColumn3.Width = 75;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "PaO2";
            this.dataGridTextBoxColumn4.MappingName = "PaO2";
            this.dataGridTextBoxColumn4.NullText = "/";
            this.dataGridTextBoxColumn4.Width = 75;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "pH";
            this.dataGridTextBoxColumn5.MappingName = "pH";
            this.dataGridTextBoxColumn5.NullText = "/";
            this.dataGridTextBoxColumn5.Width = 75;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "Na+";
            this.dataGridTextBoxColumn6.MappingName = "Na+";
            this.dataGridTextBoxColumn6.NullText = "/";
            this.dataGridTextBoxColumn6.Width = 75;
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = "K+";
            this.dataGridTextBoxColumn7.MappingName = "K+";
            this.dataGridTextBoxColumn7.NullText = "/";
            this.dataGridTextBoxColumn7.Width = 75;
            // 
            // dataGridTextBoxColumn8
            // 
            this.dataGridTextBoxColumn8.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn8.Format = "";
            this.dataGridTextBoxColumn8.FormatInfo = null;
            this.dataGridTextBoxColumn8.HeaderText = "Cr 或 BUN";
            this.dataGridTextBoxColumn8.MappingName = "Cr 或 BUN";
            this.dataGridTextBoxColumn8.NullText = "/";
            this.dataGridTextBoxColumn8.Width = 80;
            // 
            // dataGridTextBoxColumn9
            // 
            this.dataGridTextBoxColumn9.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn9.Format = "";
            this.dataGridTextBoxColumn9.FormatInfo = null;
            this.dataGridTextBoxColumn9.HeaderText = "红细胞积压比";
            this.dataGridTextBoxColumn9.MappingName = "红细胞积压比";
            this.dataGridTextBoxColumn9.NullText = "/";
            this.dataGridTextBoxColumn9.Width = 103;
            // 
            // dataGridTextBoxColumn10
            // 
            this.dataGridTextBoxColumn10.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn10.Format = "";
            this.dataGridTextBoxColumn10.FormatInfo = null;
            this.dataGridTextBoxColumn10.HeaderText = "胃肠表现";
            this.dataGridTextBoxColumn10.MappingName = "胃肠表现";
            this.dataGridTextBoxColumn10.NullText = "/";
            this.dataGridTextBoxColumn10.Width = 75;
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
            this.dtpStartSample.Size = new System.Drawing.Size(216, 22);
            this.dtpStartSample.TabIndex = 300;
            this.dtpStartSample.TextBackColor = System.Drawing.Color.White;
            this.dtpStartSample.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblTitle96
            // 
            this.lblTitle96.AutoSize = true;
            this.lblTitle96.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle96.Location = new System.Drawing.Point(8, 23);
            this.lblTitle96.Name = "lblTitle96";
            this.lblTitle96.Size = new System.Drawing.Size(82, 14);
            this.lblTitle96.TabIndex = 431;
            this.lblTitle96.Text = "采集时间：";
            this.lblTitle96.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(392, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 14);
            this.label2.TabIndex = 439;
            this.label2.Text = "评分间隔(秒)：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAutoTime
            // 
            this.txtAutoTime.BackColor = System.Drawing.Color.White;
            this.txtAutoTime.BorderColor = System.Drawing.Color.Black;
            this.txtAutoTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAutoTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAutoTime.ForeColor = System.Drawing.Color.Black;
            this.txtAutoTime.Location = new System.Drawing.Point(497, 21);
            this.txtAutoTime.MaxLength = 10;
            this.txtAutoTime.Name = "txtAutoTime";
            this.txtAutoTime.Size = new System.Drawing.Size(44, 23);
            this.txtAutoTime.TabIndex = 320;
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
            // gpbAutoEval
            // 
            this.gpbAutoEval.Controls.Add(this.txtAutoTime);
            this.gpbAutoEval.Controls.Add(this.label2);
            this.gpbAutoEval.Controls.Add(this.dtpStartSample);
            this.gpbAutoEval.Controls.Add(this.lblTitle96);
            this.gpbAutoEval.Controls.Add(this.cmdStartAuto);
            this.gpbAutoEval.Controls.Add(this.cmdStopAuto);
            this.gpbAutoEval.Controls.Add(this.cmdShowResult);
            this.gpbAutoEval.Controls.Add(this.cmdGetData);
            this.gpbAutoEval.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbAutoEval.Location = new System.Drawing.Point(3, 477);
            this.gpbAutoEval.Name = "gpbAutoEval";
            this.gpbAutoEval.Size = new System.Drawing.Size(848, 60);
            this.gpbAutoEval.TabIndex = 299;
            this.gpbAutoEval.TabStop = false;
            this.gpbAutoEval.Text = "自动评分";
            // 
            // cmdStartAuto
            // 
            this.cmdStartAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdStartAuto.DefaultScheme = true;
            this.cmdStartAuto.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdStartAuto.ForeColor = System.Drawing.Color.Black;
            this.cmdStartAuto.Hint = "";
            this.cmdStartAuto.Location = new System.Drawing.Point(544, 16);
            this.cmdStartAuto.Name = "cmdStartAuto";
            this.cmdStartAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStartAuto.Size = new System.Drawing.Size(92, 32);
            this.cmdStartAuto.TabIndex = 10000009;
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
            this.cmdStopAuto.Location = new System.Drawing.Point(639, 16);
            this.cmdStopAuto.Name = "cmdStopAuto";
            this.cmdStopAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStopAuto.Size = new System.Drawing.Size(92, 32);
            this.cmdStopAuto.TabIndex = 10000012;
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
            this.cmdShowResult.Location = new System.Drawing.Point(734, 16);
            this.cmdShowResult.Name = "cmdShowResult";
            this.cmdShowResult.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdShowResult.Size = new System.Drawing.Size(92, 32);
            this.cmdShowResult.TabIndex = 10000011;
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
            this.cmdGetData.Location = new System.Drawing.Point(300, 16);
            this.cmdGetData.Name = "cmdGetData";
            this.cmdGetData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdGetData.Size = new System.Drawing.Size(92, 32);
            this.cmdGetData.TabIndex = 10000010;
            this.cmdGetData.Text = "获取数据(&G)";
            this.cmdGetData.Click += new System.EventHandler(this.cmdGetData_Click);
            // 
            // lbltxtEvalDoctor
            // 
            this.lbltxtEvalDoctor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbltxtEvalDoctor.Location = new System.Drawing.Point(439, 313);
            this.lbltxtEvalDoctor.Name = "lbltxtEvalDoctor";
            this.lbltxtEvalDoctor.Size = new System.Drawing.Size(125, 19);
            this.lbltxtEvalDoctor.TabIndex = 449;
            // 
            // cmdCalculate
            // 
            this.cmdCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdCalculate.DefaultScheme = true;
            this.cmdCalculate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdCalculate.ForeColor = System.Drawing.Color.Black;
            this.cmdCalculate.Hint = "";
            this.cmdCalculate.Location = new System.Drawing.Point(751, 305);
            this.cmdCalculate.Name = "cmdCalculate";
            this.cmdCalculate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCalculate.Size = new System.Drawing.Size(96, 32);
            this.cmdCalculate.TabIndex = 10000010;
            this.cmdCalculate.Text = "计算分值";
            this.cmdCalculate.Click += new System.EventHandler(this.cmdCalculate_Click);
            // 
            // m_cmdGetCheckData
            // 
            this.m_cmdGetCheckData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdGetCheckData.DefaultScheme = true;
            this.m_cmdGetCheckData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGetCheckData.ForeColor = System.Drawing.Color.Black;
            this.m_cmdGetCheckData.Hint = "";
            this.m_cmdGetCheckData.Location = new System.Drawing.Point(595, 309);
            this.m_cmdGetCheckData.Name = "m_cmdGetCheckData";
            this.m_cmdGetCheckData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetCheckData.Size = new System.Drawing.Size(140, 28);
            this.m_cmdGetCheckData.TabIndex = 10000014;
            this.m_cmdGetCheckData.Text = "检验最新结果-->";
            this.m_cmdGetCheckData.Click += new System.EventHandler(this.m_cmdGetCheckData_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTitle2);
            this.groupBox2.Controls.Add(this.txtHeartRate);
            this.groupBox2.Controls.Add(this.lblTitle12);
            this.groupBox2.Controls.Add(this.lblTitle5);
            this.groupBox2.Controls.Add(this.txtBreath);
            this.groupBox2.Controls.Add(this.lblTitle19);
            this.groupBox2.Location = new System.Drawing.Point(7, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 100);
            this.groupBox2.TabIndex = 10000015;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtNaPlus);
            this.groupBox3.Controls.Add(this.lblTitle3);
            this.groupBox3.Controls.Add(this.lblTitle4);
            this.groupBox3.Controls.Add(this.txtKPlus);
            this.groupBox3.Controls.Add(this.lblTitle8);
            this.groupBox3.Controls.Add(this.lblTitle9);
            this.groupBox3.Controls.Add(this.lblTitle16);
            this.groupBox3.Controls.Add(this.lblTitle17);
            this.groupBox3.Location = new System.Drawing.Point(235, 213);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(212, 88);
            this.groupBox3.TabIndex = 10000016;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblTitle11);
            this.groupBox4.Controls.Add(this.txtpH);
            this.groupBox4.Controls.Add(this.txtRedCellComp);
            this.groupBox4.Controls.Add(this.lblTitle26);
            this.groupBox4.Location = new System.Drawing.Point(451, 213);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(172, 88);
            this.groupBox4.TabIndex = 10000017;
            this.groupBox4.TabStop = false;
            // 
            // ctmClearUp
            // 
            this.ctmClearUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ctmClearUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ctmClearUp.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctmClearUp.Location = new System.Drawing.Point(777, 56);
            this.ctmClearUp.Name = "ctmClearUp";
            this.ctmClearUp.Size = new System.Drawing.Size(48, 16);
            this.ctmClearUp.TabIndex = 462;
            this.ctmClearUp.Visible = false;
            this.ctmClearUp.Click += new System.EventHandler(this.ctmClearUp_Click);
            // 
            // m_cmdGetDovueData
            // 
            this.m_cmdGetDovueData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cmdGetDovueData.Location = new System.Drawing.Point(807, 65);
            this.m_cmdGetDovueData.Name = "m_cmdGetDovueData";
            this.m_cmdGetDovueData.Size = new System.Drawing.Size(37, 27);
            this.m_cmdGetDovueData.TabIndex = 10000003;
            this.m_cmdGetDovueData.Text = "监护仪最新结果";
            this.m_cmdGetDovueData.Visible = false;
            this.m_cmdGetDovueData.Click += new System.EventHandler(this.m_cmdGetDovueData_Click);
            // 
            // m_cmdSetLabCheckResult
            // 
            this.m_cmdSetLabCheckResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cmdSetLabCheckResult.Location = new System.Drawing.Point(788, 65);
            this.m_cmdSetLabCheckResult.Name = "m_cmdSetLabCheckResult";
            this.m_cmdSetLabCheckResult.Size = new System.Drawing.Size(47, 27);
            this.m_cmdSetLabCheckResult.TabIndex = 10000007;
            this.m_cmdSetLabCheckResult.Text = "最新检验结果";
            this.m_cmdSetLabCheckResult.Visible = false;
            this.m_cmdSetLabCheckResult.Click += new System.EventHandler(this.m_cmdSetLabCheckResult_Click);
            // 
            // clmPat_c_name
            // 
            this.clmPat_c_name.Text = "组合名称";
            this.clmPat_c_name.Width = 100;
            // 
            // clmSendDate
            // 
            this.clmSendDate.Text = "送检时间";
            this.clmSendDate.Width = 180;
            // 
            // m_lsvJY_ItemChoice
            // 
            this.m_lsvJY_ItemChoice.BackColor = System.Drawing.Color.White;
            this.m_lsvJY_ItemChoice.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmPat_c_name,
            this.clmSendDate});
            this.m_lsvJY_ItemChoice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvJY_ItemChoice.ForeColor = System.Drawing.Color.Black;
            this.m_lsvJY_ItemChoice.FullRowSelect = true;
            this.m_lsvJY_ItemChoice.GridLines = true;
            this.m_lsvJY_ItemChoice.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_lsvJY_ItemChoice.Location = new System.Drawing.Point(796, 74);
            this.m_lsvJY_ItemChoice.Name = "m_lsvJY_ItemChoice";
            this.m_lsvJY_ItemChoice.Size = new System.Drawing.Size(26, 28);
            this.m_lsvJY_ItemChoice.TabIndex = 10000008;
            this.m_lsvJY_ItemChoice.UseCompatibleStateImageBehavior = false;
            this.m_lsvJY_ItemChoice.View = System.Windows.Forms.View.Details;
            this.m_lsvJY_ItemChoice.Visible = false;
            this.m_lsvJY_ItemChoice.DoubleClick += new System.EventHandler(this.m_lsvJY_ItemChoice_DoubleClick);
            // 
            // frmNewBabyInjuryCaseEvaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.ClientSize = new System.Drawing.Size(875, 615);
            this.Controls.Add(this.gpbPao2);
            this.Controls.Add(this.gpbBloodAndShk);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.m_cmdGetCheckData);
            this.Controls.Add(this.ctmClearUp);
            this.Controls.Add(this.lblTitle31);
            this.Controls.Add(this.lblEvalDate);
            this.Controls.Add(this.lblTitle28);
            this.Controls.Add(this.m_cmdSetLabCheckResult);
            this.Controls.Add(this.m_cmdGetDovueData);
            this.Controls.Add(this.lbltxtEvalDoctor);
            this.Controls.Add(this.gpbAutoEval);
            this.Controls.Add(this.dtgResult);
            this.Controls.Add(this.cboStomach);
            this.Controls.Add(this.dtpEvalDate);
            this.Controls.Add(this.gpbCrAndBUN);
            this.Controls.Add(this.m_lsvJY_ItemChoice);
            this.Controls.Add(this.cmdCalculate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNewBabyInjuryCaseEvaluation";
            this.Text = "新生儿危重病例评分法（草案）";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.NewBabyInjuryCaseEvaluation_Closing);
            this.Load += new System.EventHandler(this.NewBabyInjuryCaseEvaluation_Load);
            this.Controls.SetChildIndex(this.cmdCalculate, 0);
            this.Controls.SetChildIndex(this.m_lsvJY_ItemChoice, 0);
            this.Controls.SetChildIndex(this.gpbCrAndBUN, 0);
            this.Controls.SetChildIndex(this.dtpEvalDate, 0);
            this.Controls.SetChildIndex(this.cboStomach, 0);
            this.Controls.SetChildIndex(this.dtgResult, 0);
            this.Controls.SetChildIndex(this.gpbAutoEval, 0);
            this.Controls.SetChildIndex(this.lbltxtEvalDoctor, 0);
            this.Controls.SetChildIndex(this.m_cmdGetDovueData, 0);
            this.Controls.SetChildIndex(this.m_cmdSetLabCheckResult, 0);
            this.Controls.SetChildIndex(this.lblTitle28, 0);
            this.Controls.SetChildIndex(this.lblEvalDate, 0);
            this.Controls.SetChildIndex(this.lblTitle31, 0);
            this.Controls.SetChildIndex(this.ctmClearUp, 0);
            this.Controls.SetChildIndex(this.m_cmdGetCheckData, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.gpbBloodAndShk, 0);
            this.Controls.SetChildIndex(this.gpbPao2, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.gpbCrAndBUN.ResumeLayout(false);
            this.gpbCrAndBUN.PerformLayout();
            this.gpbBloodAndShk.ResumeLayout(false);
            this.gpbBloodAndShk.PerformLayout();
            this.gpbPao2.ResumeLayout(false);
            this.gpbPao2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).EndInit();
            this.gpbAutoEval.ResumeLayout(false);
            this.gpbAutoEval.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Button ctmClearUp;
        private Button m_cmdSetLabCheckResult;
        private Button m_cmdGetDovueData;
        private ListView m_lsvJY_ItemChoice;
        private ColumnHeader clmPat_c_name;
        private ColumnHeader clmSendDate;

    }
}
