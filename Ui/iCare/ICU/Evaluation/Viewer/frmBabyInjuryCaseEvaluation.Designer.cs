using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;

namespace iCare.ICU.Evaluation
{
    partial class frmBabyInjuryCaseEvaluation
    {
        private PinkieControls.ButtonXP cmdGetData;
		private PinkieControls.ButtonXP cmdStartAuto;
		private PinkieControls.ButtonXP cmdStopAuto;
		private PinkieControls.ButtonXP cmdShowResult;
		private PinkieControls.ButtonXP cmdCalculate;
        private PinkieControls.ButtonXP m_cmdEvalDoctor;
		private PinkieControls.ButtonXP m_cmdGetCheckData;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private GroupBox groupBox3;
		
		//private clsCommonUseToolCollection m_objCUTC;

        #region defines
        private ctlBorderTextBox txtEvalDoctor;
        public ctlTimePicker dtpEvalDate;
        private System.Windows.Forms.Label lblEvalDate;
        private ctlBorderTextBox txtHeartRate;
        private ctlBorderTextBox txtBloodPress;
        private ctlBorderTextBox txtShrinkPressure;
        private ctlBorderTextBox txtBreath;
        private System.Windows.Forms.Label label9;
        private ctlBorderTextBox txtpH;
        private ctlBorderTextBox txtCrumol;
        private ctlBorderTextBox txtNaPlus;
        private ctlBorderTextBox txtCrmg;
        private ctlBorderTextBox txtBUNmmol;
        private ctlBorderTextBox txtBUNmg;
        private System.Windows.Forms.RadioButton rdbBloodPress;
        private System.Windows.Forms.RadioButton rdbShrinkPress;
        private System.Windows.Forms.RadioButton rdbCrumol;
        private System.Windows.Forms.RadioButton rdbPao2kPa;
        private System.Windows.Forms.RadioButton rdbPao2mmHg;
        private System.Windows.Forms.RadioButton rdbBUNmg;
        private System.Windows.Forms.RadioButton rdbBUNmmol;
        private ctlBorderTextBox txtPao2kPa;
        private ctlBorderTextBox txtPao2mmHg;
        private System.Windows.Forms.Label label4;
        private ctlBorderTextBox txtKPlus;
        private ctlComboBox cboStomach;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.DataGrid dtgResult;
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
        private System.Windows.Forms.RadioButton rdbHbdl;
        private System.Windows.Forms.RadioButton rdbHbL;
        private ctlBorderTextBox txtHbL;
        private ctlBorderTextBox txtHbdl;
        private System.Windows.Forms.RadioButton rdbAgeU1;
        private System.Windows.Forms.RadioButton rdbAgeO1;
        private System.Windows.Forms.CheckBox chkBreath;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblTitle8;
        private System.Windows.Forms.Label lblTitle11;
        private System.Windows.Forms.Label lblTitle12;
        private System.Windows.Forms.Label lblTitle14;
        private System.Windows.Forms.Label lblTitle15;
        private System.Windows.Forms.Label lblTitle16;
        private System.Windows.Forms.Label lblTitle17;
        private System.Windows.Forms.Label lblTitle18;
        private System.Windows.Forms.Label lblTitle19;
        private System.Windows.Forms.Label lblTitle20;
        private System.Windows.Forms.Label lblTitle21;
        private System.Windows.Forms.Label lblTitle13;
        private System.Windows.Forms.Label lblTitle23;
        private System.Windows.Forms.Label lblTitle25;
        private System.Windows.Forms.Label lblTitle28;
        private System.Windows.Forms.Label lblTitle6;
        private System.Windows.Forms.GroupBox gpbBloodAndShk;
        private System.Windows.Forms.Label lblTitlel7;
        private System.Windows.Forms.Label lblTitle3;
        private System.Windows.Forms.GroupBox gpbHb;
        private System.Windows.Forms.Label lblTitle22;
        private System.Windows.Forms.Label lblTitle24;

        private System.Windows.Forms.RadioButton rdbCrmg;
        private System.Windows.Forms.GroupBox gpbCrAndBUN;
        private System.Windows.Forms.GroupBox gpbPao2;
        private ctlTimePicker dtpStartSample;
        private System.Windows.Forms.Label lblTitle96;
        private System.Windows.Forms.Label label2;
        private ctlBorderTextBox txtAutoTime;
        private System.Timers.Timer timAutoCollect;
        private int intBreathSel;
        //		public string strSickBedNO;
        private System.Windows.Forms.GroupBox gpbAuto;        
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button m_cmdGetDovueData;
        private System.Windows.Forms.ListView m_lsvJY_ItemChoice;
        private System.Windows.Forms.ColumnHeader clmPat_c_name;
        private System.Windows.Forms.ColumnHeader clmSendDate;
        private System.Windows.Forms.Button m_cmdSetLabCheckResult;
        #endregion



		#region Dispose
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBabyInjuryCaseEvaluation));
            this.txtEvalDoctor = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.dtpEvalDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblEvalDate = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.txtHeartRate = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtBloodPress = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtShrinkPressure = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtBreath = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtPao2kPa = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtpH = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblTitle8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
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
            this.lblTitle28 = new System.Windows.Forms.Label();
            this.cboStomach = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.rdbBloodPress = new System.Windows.Forms.RadioButton();
            this.rdbShrinkPress = new System.Windows.Forms.RadioButton();
            this.gpbCrAndBUN = new System.Windows.Forms.GroupBox();
            this.lblTitle6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rdbCrumol = new System.Windows.Forms.RadioButton();
            this.rdbCrmg = new System.Windows.Forms.RadioButton();
            this.rdbBUNmmol = new System.Windows.Forms.RadioButton();
            this.rdbBUNmg = new System.Windows.Forms.RadioButton();
            this.rdbPao2kPa = new System.Windows.Forms.RadioButton();
            this.rdbPao2mmHg = new System.Windows.Forms.RadioButton();
            this.gpbBloodAndShk = new System.Windows.Forms.GroupBox();
            this.gpbPao2 = new System.Windows.Forms.GroupBox();
            this.lblTitlel7 = new System.Windows.Forms.Label();
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.gpbHb = new System.Windows.Forms.GroupBox();
            this.lblTitle22 = new System.Windows.Forms.Label();
            this.lblTitle24 = new System.Windows.Forms.Label();
            this.txtHbL = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.txtHbdl = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.rdbHbdl = new System.Windows.Forms.RadioButton();
            this.rdbHbL = new System.Windows.Forms.RadioButton();
            this.rdbAgeU1 = new System.Windows.Forms.RadioButton();
            this.rdbAgeO1 = new System.Windows.Forms.RadioButton();
            this.chkBreath = new System.Windows.Forms.CheckBox();
            this.dtpStartSample = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblTitle96 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAutoTime = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.timAutoCollect = new System.Timers.Timer();
            this.gpbAuto = new System.Windows.Forms.GroupBox();
            this.cmdGetData = new PinkieControls.ButtonXP();
            this.cmdShowResult = new PinkieControls.ButtonXP();
            this.cmdStopAuto = new PinkieControls.ButtonXP();
            this.cmdStartAuto = new PinkieControls.ButtonXP();
            this.label7 = new System.Windows.Forms.Label();
            this.m_cmdGetDovueData = new System.Windows.Forms.Button();
            this.m_lsvJY_ItemChoice = new System.Windows.Forms.ListView();
            this.clmPat_c_name = new System.Windows.Forms.ColumnHeader();
            this.clmSendDate = new System.Windows.Forms.ColumnHeader();
            this.m_cmdSetLabCheckResult = new System.Windows.Forms.Button();
            this.cmdCalculate = new PinkieControls.ButtonXP();
            this.m_cmdEvalDoctor = new PinkieControls.ButtonXP();
            this.m_cmdGetCheckData = new PinkieControls.ButtonXP();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_pnlNewBase.SuspendLayout();
            this.gpbCrAndBUN.SuspendLayout();
            this.gpbBloodAndShk.SuspendLayout();
            this.gpbPao2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).BeginInit();
            this.gpbHb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).BeginInit();
            this.gpbAuto.SuspendLayout();
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
            // txtEvalDoctor
            // 
            this.txtEvalDoctor.AccessibleDescription = "";
            this.txtEvalDoctor.BackColor = System.Drawing.Color.White;
            this.txtEvalDoctor.BorderColor = System.Drawing.Color.Black;
            this.txtEvalDoctor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEvalDoctor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEvalDoctor.ForeColor = System.Drawing.Color.Black;
            this.txtEvalDoctor.Location = new System.Drawing.Point(485, 348);
            this.txtEvalDoctor.Name = "txtEvalDoctor";
            this.txtEvalDoctor.Size = new System.Drawing.Size(112, 23);
            this.txtEvalDoctor.TabIndex = 340;
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
            this.dtpEvalDate.Location = new System.Drawing.Point(87, 348);
            this.dtpEvalDate.m_BlnOnlyTime = false;
            this.dtpEvalDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpEvalDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEvalDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEvalDate.Name = "dtpEvalDate";
            this.dtpEvalDate.ReadOnly = false;
            this.dtpEvalDate.Size = new System.Drawing.Size(220, 22);
            this.dtpEvalDate.TabIndex = 330;
            this.dtpEvalDate.TextBackColor = System.Drawing.Color.White;
            this.dtpEvalDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblEvalDate
            // 
            this.lblEvalDate.AutoSize = true;
            this.lblEvalDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEvalDate.Location = new System.Drawing.Point(5, 352);
            this.lblEvalDate.Name = "lblEvalDate";
            this.lblEvalDate.Size = new System.Drawing.Size(82, 14);
            this.lblEvalDate.TabIndex = 333;
            this.lblEvalDate.Text = "评分时间：";
            this.lblEvalDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle2.Location = new System.Drawing.Point(4, 20);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(45, 14);
            this.lblTitle2.TabIndex = 338;
            this.lblTitle2.Text = "心率:";
            this.lblTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHeartRate
            // 
            this.txtHeartRate.AccessibleDescription = "";
            this.txtHeartRate.BackColor = System.Drawing.Color.White;
            this.txtHeartRate.BorderColor = System.Drawing.Color.Black;
            this.txtHeartRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHeartRate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHeartRate.ForeColor = System.Drawing.Color.Black;
            this.txtHeartRate.Location = new System.Drawing.Point(55, 20);
            this.txtHeartRate.MaxLength = 4;
            this.txtHeartRate.Name = "txtHeartRate";
            this.txtHeartRate.Size = new System.Drawing.Size(105, 23);
            this.txtHeartRate.TabIndex = 210;
            // 
            // txtBloodPress
            // 
            this.txtBloodPress.BackColor = System.Drawing.Color.White;
            this.txtBloodPress.BorderColor = System.Drawing.Color.Black;
            this.txtBloodPress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBloodPress.ForeColor = System.Drawing.Color.Black;
            this.txtBloodPress.Location = new System.Drawing.Point(53, 26);
            this.txtBloodPress.Name = "txtBloodPress";
            this.txtBloodPress.Size = new System.Drawing.Size(107, 23);
            this.txtBloodPress.TabIndex = 150;
            this.txtBloodPress.Tag = "0";
            // 
            // txtShrinkPressure
            // 
            this.txtShrinkPressure.BackColor = System.Drawing.Color.White;
            this.txtShrinkPressure.BorderColor = System.Drawing.Color.Black;
            this.txtShrinkPressure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShrinkPressure.Enabled = false;
            this.txtShrinkPressure.ForeColor = System.Drawing.Color.Black;
            this.txtShrinkPressure.Location = new System.Drawing.Point(53, 59);
            this.txtShrinkPressure.Name = "txtShrinkPressure";
            this.txtShrinkPressure.Size = new System.Drawing.Size(107, 23);
            this.txtShrinkPressure.TabIndex = 170;
            this.txtShrinkPressure.Tag = "1";
            // 
            // txtBreath
            // 
            this.txtBreath.AccessibleDescription = "";
            this.txtBreath.BackColor = System.Drawing.Color.White;
            this.txtBreath.BorderColor = System.Drawing.Color.Black;
            this.txtBreath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBreath.ForeColor = System.Drawing.Color.Black;
            this.txtBreath.Location = new System.Drawing.Point(55, 52);
            this.txtBreath.MaxLength = 4;
            this.txtBreath.Name = "txtBreath";
            this.txtBreath.Size = new System.Drawing.Size(105, 23);
            this.txtBreath.TabIndex = 180;
            // 
            // txtPao2kPa
            // 
            this.txtPao2kPa.BackColor = System.Drawing.Color.White;
            this.txtPao2kPa.BorderColor = System.Drawing.Color.Black;
            this.txtPao2kPa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPao2kPa.ForeColor = System.Drawing.Color.Black;
            this.txtPao2kPa.Location = new System.Drawing.Point(24, 26);
            this.txtPao2kPa.Name = "txtPao2kPa";
            this.txtPao2kPa.Size = new System.Drawing.Size(112, 23);
            this.txtPao2kPa.TabIndex = 70;
            this.txtPao2kPa.Tag = "0";
            // 
            // txtpH
            // 
            this.txtpH.AccessibleDescription = "";
            this.txtpH.BackColor = System.Drawing.Color.White;
            this.txtpH.BorderColor = System.Drawing.Color.Black;
            this.txtpH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpH.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtpH.ForeColor = System.Drawing.Color.Black;
            this.txtpH.Location = new System.Drawing.Point(48, 20);
            this.txtpH.MaxLength = 8;
            this.txtpH.Name = "txtpH";
            this.txtpH.Size = new System.Drawing.Size(112, 23);
            this.txtpH.TabIndex = 230;
            // 
            // lblTitle8
            // 
            this.lblTitle8.AutoSize = true;
            this.lblTitle8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle8.Location = new System.Drawing.Point(4, 52);
            this.lblTitle8.Name = "lblTitle8";
            this.lblTitle8.Size = new System.Drawing.Size(47, 14);
            this.lblTitle8.TabIndex = 338;
            this.lblTitle8.Text = "Na  :";
            this.lblTitle8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(4, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 14);
            this.label9.TabIndex = 338;
            this.label9.Text = "K   :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCrumol
            // 
            this.txtCrumol.BackColor = System.Drawing.Color.White;
            this.txtCrumol.BorderColor = System.Drawing.Color.Black;
            this.txtCrumol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCrumol.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCrumol.ForeColor = System.Drawing.Color.Black;
            this.txtCrumol.Location = new System.Drawing.Point(24, 30);
            this.txtCrumol.MaxLength = 8;
            this.txtCrumol.Name = "txtCrumol";
            this.txtCrumol.Size = new System.Drawing.Size(112, 23);
            this.txtCrumol.TabIndex = 260;
            this.txtCrumol.Tag = "0";
            // 
            // txtKPlus
            // 
            this.txtKPlus.AccessibleDescription = "";
            this.txtKPlus.BackColor = System.Drawing.Color.White;
            this.txtKPlus.BorderColor = System.Drawing.Color.Black;
            this.txtKPlus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKPlus.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtKPlus.ForeColor = System.Drawing.Color.Black;
            this.txtKPlus.Location = new System.Drawing.Point(48, 83);
            this.txtKPlus.MaxLength = 8;
            this.txtKPlus.Name = "txtKPlus";
            this.txtKPlus.Size = new System.Drawing.Size(112, 23);
            this.txtKPlus.TabIndex = 240;
            // 
            // txtNaPlus
            // 
            this.txtNaPlus.AccessibleDescription = "";
            this.txtNaPlus.BackColor = System.Drawing.Color.White;
            this.txtNaPlus.BorderColor = System.Drawing.Color.Black;
            this.txtNaPlus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNaPlus.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNaPlus.ForeColor = System.Drawing.Color.Black;
            this.txtNaPlus.Location = new System.Drawing.Point(48, 51);
            this.txtNaPlus.MaxLength = 8;
            this.txtNaPlus.Name = "txtNaPlus";
            this.txtNaPlus.Size = new System.Drawing.Size(112, 23);
            this.txtNaPlus.TabIndex = 220;
            // 
            // txtPao2mmHg
            // 
            this.txtPao2mmHg.BackColor = System.Drawing.Color.White;
            this.txtPao2mmHg.BorderColor = System.Drawing.Color.Black;
            this.txtPao2mmHg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPao2mmHg.Enabled = false;
            this.txtPao2mmHg.ForeColor = System.Drawing.Color.Black;
            this.txtPao2mmHg.Location = new System.Drawing.Point(24, 56);
            this.txtPao2mmHg.Name = "txtPao2mmHg";
            this.txtPao2mmHg.Size = new System.Drawing.Size(112, 23);
            this.txtPao2mmHg.TabIndex = 90;
            this.txtPao2mmHg.Tag = "1";
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
            this.lblTitle11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle12
            // 
            this.lblTitle12.AutoSize = true;
            this.lblTitle12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle12.Location = new System.Drawing.Point(164, 20);
            this.lblTitle12.Name = "lblTitle12";
            this.lblTitle12.Size = new System.Drawing.Size(45, 14);
            this.lblTitle12.TabIndex = 338;
            this.lblTitle12.Text = "次/分";
            this.lblTitle12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle14
            // 
            this.lblTitle14.AutoSize = true;
            this.lblTitle14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle14.Location = new System.Drawing.Point(139, 34);
            this.lblTitle14.Name = "lblTitle14";
            this.lblTitle14.Size = new System.Drawing.Size(62, 14);
            this.lblTitle14.TabIndex = 338;
            this.lblTitle14.Text = "μmol/L";
            this.lblTitle14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle15
            // 
            this.lblTitle15.AutoSize = true;
            this.lblTitle15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle15.Location = new System.Drawing.Point(164, 60);
            this.lblTitle15.Name = "lblTitle15";
            this.lblTitle15.Size = new System.Drawing.Size(39, 14);
            this.lblTitle15.TabIndex = 338;
            this.lblTitle15.Text = "mmHg";
            this.lblTitle15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle16
            // 
            this.lblTitle16.AutoSize = true;
            this.lblTitle16.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle16.Location = new System.Drawing.Point(164, 84);
            this.lblTitle16.Name = "lblTitle16";
            this.lblTitle16.Size = new System.Drawing.Size(55, 14);
            this.lblTitle16.TabIndex = 338;
            this.lblTitle16.Text = "mmol/L";
            this.lblTitle16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle17
            // 
            this.lblTitle17.AutoSize = true;
            this.lblTitle17.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle17.Location = new System.Drawing.Point(164, 52);
            this.lblTitle17.Name = "lblTitle17";
            this.lblTitle17.Size = new System.Drawing.Size(55, 14);
            this.lblTitle17.TabIndex = 338;
            this.lblTitle17.Text = "mmol/L";
            this.lblTitle17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle18
            // 
            this.lblTitle18.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle18.Location = new System.Drawing.Point(140, 60);
            this.lblTitle18.Name = "lblTitle18";
            this.lblTitle18.Size = new System.Drawing.Size(48, 24);
            this.lblTitle18.TabIndex = 338;
            this.lblTitle18.Text = "mmHg";
            this.lblTitle18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle19
            // 
            this.lblTitle19.AutoSize = true;
            this.lblTitle19.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle19.Location = new System.Drawing.Point(164, 56);
            this.lblTitle19.Name = "lblTitle19";
            this.lblTitle19.Size = new System.Drawing.Size(45, 14);
            this.lblTitle19.TabIndex = 338;
            this.lblTitle19.Text = "次/分";
            this.lblTitle19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle20
            // 
            this.lblTitle20.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle20.Location = new System.Drawing.Point(140, 28);
            this.lblTitle20.Name = "lblTitle20";
            this.lblTitle20.Size = new System.Drawing.Size(41, 16);
            this.lblTitle20.TabIndex = 338;
            this.lblTitle20.Text = "kPa";
            this.lblTitle20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle21
            // 
            this.lblTitle21.AutoSize = true;
            this.lblTitle21.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle21.Location = new System.Drawing.Point(164, 27);
            this.lblTitle21.Name = "lblTitle21";
            this.lblTitle21.Size = new System.Drawing.Size(31, 14);
            this.lblTitle21.TabIndex = 338;
            this.lblTitle21.Text = "kPa";
            this.lblTitle21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle13
            // 
            this.lblTitle13.AutoSize = true;
            this.lblTitle13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle13.Location = new System.Drawing.Point(144, 65);
            this.lblTitle13.Name = "lblTitle13";
            this.lblTitle13.Size = new System.Drawing.Size(47, 14);
            this.lblTitle13.TabIndex = 338;
            this.lblTitle13.Text = "mg/dl";
            this.lblTitle13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCrmg
            // 
            this.txtCrmg.BackColor = System.Drawing.Color.White;
            this.txtCrmg.BorderColor = System.Drawing.Color.Black;
            this.txtCrmg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCrmg.Enabled = false;
            this.txtCrmg.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCrmg.ForeColor = System.Drawing.Color.Black;
            this.txtCrmg.Location = new System.Drawing.Point(24, 60);
            this.txtCrmg.MaxLength = 8;
            this.txtCrmg.Name = "txtCrmg";
            this.txtCrmg.Size = new System.Drawing.Size(112, 23);
            this.txtCrmg.TabIndex = 280;
            this.txtCrmg.Tag = "1";
            // 
            // lblTitle23
            // 
            this.lblTitle23.AutoSize = true;
            this.lblTitle23.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle23.Location = new System.Drawing.Point(339, 35);
            this.lblTitle23.Name = "lblTitle23";
            this.lblTitle23.Size = new System.Drawing.Size(55, 14);
            this.lblTitle23.TabIndex = 338;
            this.lblTitle23.Text = "mmol/L";
            this.lblTitle23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBUNmmol
            // 
            this.txtBUNmmol.BackColor = System.Drawing.Color.White;
            this.txtBUNmmol.BorderColor = System.Drawing.Color.Black;
            this.txtBUNmmol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBUNmmol.Enabled = false;
            this.txtBUNmmol.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBUNmmol.ForeColor = System.Drawing.Color.Black;
            this.txtBUNmmol.Location = new System.Drawing.Point(220, 31);
            this.txtBUNmmol.MaxLength = 8;
            this.txtBUNmmol.Name = "txtBUNmmol";
            this.txtBUNmmol.Size = new System.Drawing.Size(112, 23);
            this.txtBUNmmol.TabIndex = 300;
            this.txtBUNmmol.Tag = "2";
            // 
            // txtBUNmg
            // 
            this.txtBUNmg.BackColor = System.Drawing.Color.White;
            this.txtBUNmg.BorderColor = System.Drawing.Color.Black;
            this.txtBUNmg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBUNmg.Enabled = false;
            this.txtBUNmg.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBUNmg.ForeColor = System.Drawing.Color.Black;
            this.txtBUNmg.Location = new System.Drawing.Point(220, 61);
            this.txtBUNmg.MaxLength = 8;
            this.txtBUNmg.Name = "txtBUNmg";
            this.txtBUNmg.Size = new System.Drawing.Size(112, 23);
            this.txtBUNmg.TabIndex = 320;
            this.txtBUNmg.Tag = "3";
            // 
            // lblTitle25
            // 
            this.lblTitle25.AutoSize = true;
            this.lblTitle25.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle25.Location = new System.Drawing.Point(340, 67);
            this.lblTitle25.Name = "lblTitle25";
            this.lblTitle25.Size = new System.Drawing.Size(47, 14);
            this.lblTitle25.TabIndex = 338;
            this.lblTitle25.Text = "mg/dl";
            this.lblTitle25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle28
            // 
            this.lblTitle28.AutoSize = true;
            this.lblTitle28.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle28.Location = new System.Drawing.Point(677, 108);
            this.lblTitle28.Name = "lblTitle28";
            this.lblTitle28.Size = new System.Drawing.Size(82, 14);
            this.lblTitle28.TabIndex = 338;
            this.lblTitle28.Text = "胃肠系统：";
            this.lblTitle28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.cboStomach.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboStomach.ForeColor = System.Drawing.Color.Black;
            this.cboStomach.ListBackColor = System.Drawing.Color.White;
            this.cboStomach.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.cboStomach.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.cboStomach.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.cboStomach.Location = new System.Drawing.Point(667, 128);
            this.cboStomach.m_BlnEnableItemEventMenu = true;
            this.cboStomach.MaxLength = 32767;
            this.cboStomach.Name = "cboStomach";
            this.cboStomach.SelectedIndex = -1;
            this.cboStomach.SelectedItem = null;
            this.cboStomach.SelectionStart = 0;
            this.cboStomach.Size = new System.Drawing.Size(188, 23);
            this.cboStomach.TabIndex = 200;
            this.cboStomach.TextBackColor = System.Drawing.Color.White;
            this.cboStomach.TextForeColor = System.Drawing.Color.Black;
            // 
            // rdbBloodPress
            // 
            this.rdbBloodPress.Checked = true;
            this.rdbBloodPress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbBloodPress.Location = new System.Drawing.Point(13, 28);
            this.rdbBloodPress.Name = "rdbBloodPress";
            this.rdbBloodPress.Size = new System.Drawing.Size(36, 16);
            this.rdbBloodPress.TabIndex = 140;
            this.rdbBloodPress.TabStop = true;
            this.rdbBloodPress.Tag = "0";
            this.rdbBloodPress.CheckedChanged += new System.EventHandler(this.PressChange);
            // 
            // rdbShrinkPress
            // 
            this.rdbShrinkPress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbShrinkPress.Location = new System.Drawing.Point(13, 60);
            this.rdbShrinkPress.Name = "rdbShrinkPress";
            this.rdbShrinkPress.Size = new System.Drawing.Size(36, 16);
            this.rdbShrinkPress.TabIndex = 160;
            this.rdbShrinkPress.Tag = "1";
            this.rdbShrinkPress.CheckedChanged += new System.EventHandler(this.PressChange);
            // 
            // gpbCrAndBUN
            // 
            this.gpbCrAndBUN.Controls.Add(this.txtCrumol);
            this.gpbCrAndBUN.Controls.Add(this.lblTitle6);
            this.gpbCrAndBUN.Controls.Add(this.txtBUNmmol);
            this.gpbCrAndBUN.Controls.Add(this.label3);
            this.gpbCrAndBUN.Controls.Add(this.lblTitle14);
            this.gpbCrAndBUN.Controls.Add(this.txtCrmg);
            this.gpbCrAndBUN.Controls.Add(this.lblTitle13);
            this.gpbCrAndBUN.Controls.Add(this.lblTitle25);
            this.gpbCrAndBUN.Controls.Add(this.txtBUNmg);
            this.gpbCrAndBUN.Controls.Add(this.lblTitle23);
            this.gpbCrAndBUN.Controls.Add(this.rdbCrumol);
            this.gpbCrAndBUN.Controls.Add(this.rdbCrmg);
            this.gpbCrAndBUN.Controls.Add(this.rdbBUNmmol);
            this.gpbCrAndBUN.Controls.Add(this.rdbBUNmg);
            this.gpbCrAndBUN.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbCrAndBUN.Location = new System.Drawing.Point(454, 232);
            this.gpbCrAndBUN.Name = "gpbCrAndBUN";
            this.gpbCrAndBUN.Size = new System.Drawing.Size(401, 96);
            this.gpbCrAndBUN.TabIndex = 248;
            this.gpbCrAndBUN.TabStop = false;
            // 
            // lblTitle6
            // 
            this.lblTitle6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle6.Location = new System.Drawing.Point(16, 9);
            this.lblTitle6.Name = "lblTitle6";
            this.lblTitle6.Size = new System.Drawing.Size(40, 21);
            this.lblTitle6.TabIndex = 341;
            this.lblTitle6.Text = "Cr：";
            this.lblTitle6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(216, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 342;
            this.label3.Text = "BUN：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdbCrumol
            // 
            this.rdbCrumol.Checked = true;
            this.rdbCrumol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbCrumol.Location = new System.Drawing.Point(8, 24);
            this.rdbCrumol.Name = "rdbCrumol";
            this.rdbCrumol.Size = new System.Drawing.Size(36, 36);
            this.rdbCrumol.TabIndex = 250;
            this.rdbCrumol.TabStop = true;
            this.rdbCrumol.Tag = "0";
            this.rdbCrumol.CheckedChanged += new System.EventHandler(this.CrChanged);
            // 
            // rdbCrmg
            // 
            this.rdbCrmg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbCrmg.Location = new System.Drawing.Point(8, 53);
            this.rdbCrmg.Name = "rdbCrmg";
            this.rdbCrmg.Size = new System.Drawing.Size(36, 36);
            this.rdbCrmg.TabIndex = 270;
            this.rdbCrmg.Tag = "1";
            this.rdbCrmg.CheckedChanged += new System.EventHandler(this.CrChanged);
            // 
            // rdbBUNmmol
            // 
            this.rdbBUNmmol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbBUNmmol.Location = new System.Drawing.Point(204, 25);
            this.rdbBUNmmol.Name = "rdbBUNmmol";
            this.rdbBUNmmol.Size = new System.Drawing.Size(32, 36);
            this.rdbBUNmmol.TabIndex = 290;
            this.rdbBUNmmol.Tag = "2";
            this.rdbBUNmmol.CheckedChanged += new System.EventHandler(this.BUNChanged);
            // 
            // rdbBUNmg
            // 
            this.rdbBUNmg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbBUNmg.Location = new System.Drawing.Point(204, 54);
            this.rdbBUNmg.Name = "rdbBUNmg";
            this.rdbBUNmg.Size = new System.Drawing.Size(32, 36);
            this.rdbBUNmg.TabIndex = 310;
            this.rdbBUNmg.Tag = "3";
            this.rdbBUNmg.CheckedChanged += new System.EventHandler(this.BUNChanged);
            // 
            // rdbPao2kPa
            // 
            this.rdbPao2kPa.Checked = true;
            this.rdbPao2kPa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbPao2kPa.Location = new System.Drawing.Point(8, 28);
            this.rdbPao2kPa.Name = "rdbPao2kPa";
            this.rdbPao2kPa.Size = new System.Drawing.Size(39, 16);
            this.rdbPao2kPa.TabIndex = 60;
            this.rdbPao2kPa.TabStop = true;
            this.rdbPao2kPa.Tag = "0";
            this.rdbPao2kPa.CheckedChanged += new System.EventHandler(this.Pao2Changed);
            // 
            // rdbPao2mmHg
            // 
            this.rdbPao2mmHg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbPao2mmHg.Location = new System.Drawing.Point(8, 64);
            this.rdbPao2mmHg.Name = "rdbPao2mmHg";
            this.rdbPao2mmHg.Size = new System.Drawing.Size(39, 16);
            this.rdbPao2mmHg.TabIndex = 80;
            this.rdbPao2mmHg.Tag = "1";
            this.rdbPao2mmHg.CheckedChanged += new System.EventHandler(this.Pao2Changed);
            // 
            // gpbBloodAndShk
            // 
            this.gpbBloodAndShk.Controls.Add(this.lblTitle15);
            this.gpbBloodAndShk.Controls.Add(this.txtBloodPress);
            this.gpbBloodAndShk.Controls.Add(this.txtShrinkPressure);
            this.gpbBloodAndShk.Controls.Add(this.lblTitle21);
            this.gpbBloodAndShk.Controls.Add(this.rdbShrinkPress);
            this.gpbBloodAndShk.Controls.Add(this.rdbBloodPress);
            this.gpbBloodAndShk.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbBloodAndShk.Location = new System.Drawing.Point(5, 104);
            this.gpbBloodAndShk.Name = "gpbBloodAndShk";
            this.gpbBloodAndShk.Size = new System.Drawing.Size(212, 100);
            this.gpbBloodAndShk.TabIndex = 138;
            this.gpbBloodAndShk.TabStop = false;
            this.gpbBloodAndShk.Text = "血压（收缩压）：";
            // 
            // gpbPao2
            // 
            this.gpbPao2.Controls.Add(this.lblTitle20);
            this.gpbPao2.Controls.Add(this.lblTitle18);
            this.gpbPao2.Controls.Add(this.txtPao2mmHg);
            this.gpbPao2.Controls.Add(this.txtPao2kPa);
            this.gpbPao2.Controls.Add(this.lblTitlel7);
            this.gpbPao2.Controls.Add(this.rdbPao2mmHg);
            this.gpbPao2.Controls.Add(this.rdbPao2kPa);
            this.gpbPao2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbPao2.Location = new System.Drawing.Point(452, 104);
            this.gpbPao2.Name = "gpbPao2";
            this.gpbPao2.Size = new System.Drawing.Size(204, 100);
            this.gpbPao2.TabIndex = 59;
            this.gpbPao2.TabStop = false;
            this.gpbPao2.Text = "PaO  ：";
            // 
            // lblTitlel7
            // 
            this.lblTitlel7.AutoSize = true;
            this.lblTitlel7.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitlel7.Location = new System.Drawing.Point(37, 8);
            this.lblTitlel7.Name = "lblTitlel7";
            this.lblTitlel7.Size = new System.Drawing.Size(11, 10);
            this.lblTitlel7.TabIndex = 346;
            this.lblTitlel7.Text = "2";
            // 
            // lblTitle3
            // 
            this.lblTitle3.AutoSize = true;
            this.lblTitle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle3.Location = new System.Drawing.Point(24, 52);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(15, 14);
            this.lblTitle3.TabIndex = 346;
            this.lblTitle3.Text = "+";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(20, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 14);
            this.label4.TabIndex = 346;
            this.label4.Text = "+";
            // 
            // dtgResult
            // 
            this.dtgResult.BackColor = System.Drawing.Color.White;
            this.dtgResult.BackgroundColor = System.Drawing.Color.White;
            this.dtgResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtgResult.CaptionBackColor = System.Drawing.Color.White;
            this.dtgResult.CaptionForeColor = System.Drawing.Color.Black;
            this.dtgResult.CaptionText = "小儿危重病例评分结果";
            this.dtgResult.DataMember = "";
            this.dtgResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtgResult.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dtgResult.Location = new System.Drawing.Point(5, 380);
            this.dtgResult.Name = "dtgResult";
            this.dtgResult.ReadOnly = true;
            this.dtgResult.Size = new System.Drawing.Size(848, 124);
            this.dtgResult.TabIndex = 347;
            this.dtgResult.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.dtgResult.TabStop = false;
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
            this.dataGridTextBoxColumn9.HeaderText = "Hb";
            this.dataGridTextBoxColumn9.MappingName = "Hb";
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
            // gpbHb
            // 
            this.gpbHb.Controls.Add(this.lblTitle22);
            this.gpbHb.Controls.Add(this.lblTitle24);
            this.gpbHb.Controls.Add(this.txtHbL);
            this.gpbHb.Controls.Add(this.txtHbdl);
            this.gpbHb.Controls.Add(this.rdbHbdl);
            this.gpbHb.Controls.Add(this.rdbHbL);
            this.gpbHb.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbHb.Location = new System.Drawing.Point(222, 104);
            this.gpbHb.Name = "gpbHb";
            this.gpbHb.Size = new System.Drawing.Size(224, 100);
            this.gpbHb.TabIndex = 99;
            this.gpbHb.TabStop = false;
            this.gpbHb.Text = "Hb：";
            // 
            // lblTitle22
            // 
            this.lblTitle22.AutoSize = true;
            this.lblTitle22.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle22.Location = new System.Drawing.Point(164, 59);
            this.lblTitle22.Name = "lblTitle22";
            this.lblTitle22.Size = new System.Drawing.Size(39, 14);
            this.lblTitle22.TabIndex = 338;
            this.lblTitle22.Text = "g/dl";
            this.lblTitle22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle24
            // 
            this.lblTitle24.AutoSize = true;
            this.lblTitle24.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle24.Location = new System.Drawing.Point(164, 27);
            this.lblTitle24.Name = "lblTitle24";
            this.lblTitle24.Size = new System.Drawing.Size(31, 14);
            this.lblTitle24.TabIndex = 338;
            this.lblTitle24.Text = "g/L";
            this.lblTitle24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHbL
            // 
            this.txtHbL.BackColor = System.Drawing.Color.White;
            this.txtHbL.BorderColor = System.Drawing.Color.Black;
            this.txtHbL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHbL.ForeColor = System.Drawing.Color.Black;
            this.txtHbL.Location = new System.Drawing.Point(48, 26);
            this.txtHbL.Name = "txtHbL";
            this.txtHbL.Size = new System.Drawing.Size(112, 23);
            this.txtHbL.TabIndex = 110;
            this.txtHbL.Tag = "0";
            // 
            // txtHbdl
            // 
            this.txtHbdl.BackColor = System.Drawing.Color.White;
            this.txtHbdl.BorderColor = System.Drawing.Color.Black;
            this.txtHbdl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHbdl.Enabled = false;
            this.txtHbdl.ForeColor = System.Drawing.Color.Black;
            this.txtHbdl.Location = new System.Drawing.Point(48, 58);
            this.txtHbdl.Name = "txtHbdl";
            this.txtHbdl.Size = new System.Drawing.Size(112, 23);
            this.txtHbdl.TabIndex = 130;
            this.txtHbdl.Tag = "1";
            // 
            // rdbHbdl
            // 
            this.rdbHbdl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbHbdl.Location = new System.Drawing.Point(14, 60);
            this.rdbHbdl.Name = "rdbHbdl";
            this.rdbHbdl.Size = new System.Drawing.Size(39, 16);
            this.rdbHbdl.TabIndex = 120;
            this.rdbHbdl.Tag = "1";
            this.rdbHbdl.CheckedChanged += new System.EventHandler(this.HbChanged);
            // 
            // rdbHbL
            // 
            this.rdbHbL.Checked = true;
            this.rdbHbL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbHbL.Location = new System.Drawing.Point(14, 28);
            this.rdbHbL.Name = "rdbHbL";
            this.rdbHbL.Size = new System.Drawing.Size(39, 16);
            this.rdbHbL.TabIndex = 100;
            this.rdbHbL.TabStop = true;
            this.rdbHbL.Tag = "0";
            this.rdbHbL.CheckedChanged += new System.EventHandler(this.HbChanged);
            // 
            // rdbAgeU1
            // 
            this.rdbAgeU1.Checked = true;
            this.rdbAgeU1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAgeU1.Location = new System.Drawing.Point(557, 213);
            this.rdbAgeU1.Name = "rdbAgeU1";
            this.rdbAgeU1.Size = new System.Drawing.Size(96, 23);
            this.rdbAgeU1.TabIndex = 40;
            this.rdbAgeU1.TabStop = true;
            this.rdbAgeU1.Tag = "0";
            this.rdbAgeU1.Text = "小于 1 岁";
            this.rdbAgeU1.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // rdbAgeO1
            // 
            this.rdbAgeO1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAgeO1.Location = new System.Drawing.Point(657, 212);
            this.rdbAgeO1.Name = "rdbAgeO1";
            this.rdbAgeO1.Size = new System.Drawing.Size(105, 24);
            this.rdbAgeO1.TabIndex = 50;
            this.rdbAgeO1.Tag = "1";
            this.rdbAgeO1.Text = "大于 1 岁";
            this.rdbAgeO1.CheckedChanged += new System.EventHandler(this.AgeGroupChanged);
            // 
            // chkBreath
            // 
            this.chkBreath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkBreath.Location = new System.Drawing.Point(32, 80);
            this.chkBreath.Name = "chkBreath";
            this.chkBreath.Size = new System.Drawing.Size(142, 27);
            this.chkBreath.TabIndex = 190;
            this.chkBreath.Text = "明显节律不齐";
            this.chkBreath.CheckedChanged += new System.EventHandler(this.BreathChange);
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
            this.dtpStartSample.Location = new System.Drawing.Point(80, 21);
            this.dtpStartSample.m_BlnOnlyTime = false;
            this.dtpStartSample.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.dtpStartSample.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartSample.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartSample.Name = "dtpStartSample";
            this.dtpStartSample.ReadOnly = false;
            this.dtpStartSample.Size = new System.Drawing.Size(217, 22);
            this.dtpStartSample.TabIndex = 360;
            this.dtpStartSample.TextBackColor = System.Drawing.Color.White;
            this.dtpStartSample.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblTitle96
            // 
            this.lblTitle96.AutoSize = true;
            this.lblTitle96.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle96.Location = new System.Drawing.Point(9, 23);
            this.lblTitle96.Name = "lblTitle96";
            this.lblTitle96.Size = new System.Drawing.Size(82, 14);
            this.lblTitle96.TabIndex = 425;
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
            this.label2.TabIndex = 420;
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
            this.txtAutoTime.Location = new System.Drawing.Point(499, 22);
            this.txtAutoTime.MaxLength = 10;
            this.txtAutoTime.Name = "txtAutoTime";
            this.txtAutoTime.Size = new System.Drawing.Size(44, 23);
            this.txtAutoTime.TabIndex = 380;
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
            // gpbAuto
            // 
            this.gpbAuto.Controls.Add(this.dtpStartSample);
            this.gpbAuto.Controls.Add(this.lblTitle96);
            this.gpbAuto.Controls.Add(this.txtAutoTime);
            this.gpbAuto.Controls.Add(this.label2);
            this.gpbAuto.Controls.Add(this.cmdGetData);
            this.gpbAuto.Controls.Add(this.cmdShowResult);
            this.gpbAuto.Controls.Add(this.cmdStopAuto);
            this.gpbAuto.Controls.Add(this.cmdStartAuto);
            this.gpbAuto.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbAuto.Location = new System.Drawing.Point(7, 512);
            this.gpbAuto.Name = "gpbAuto";
            this.gpbAuto.Size = new System.Drawing.Size(846, 56);
            this.gpbAuto.TabIndex = 359;
            this.gpbAuto.TabStop = false;
            this.gpbAuto.Text = "自动评分";
            // 
            // cmdGetData
            // 
            this.cmdGetData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdGetData.DefaultScheme = true;
            this.cmdGetData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdGetData.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdGetData.ForeColor = System.Drawing.Color.Black;
            this.cmdGetData.Hint = "";
            this.cmdGetData.Location = new System.Drawing.Point(304, 16);
            this.cmdGetData.Name = "cmdGetData";
            this.cmdGetData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdGetData.Size = new System.Drawing.Size(88, 32);
            this.cmdGetData.TabIndex = 10000027;
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
            this.cmdShowResult.Location = new System.Drawing.Point(744, 16);
            this.cmdShowResult.Name = "cmdShowResult";
            this.cmdShowResult.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdShowResult.Size = new System.Drawing.Size(92, 32);
            this.cmdShowResult.TabIndex = 10000028;
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
            this.cmdStopAuto.Size = new System.Drawing.Size(88, 32);
            this.cmdStopAuto.TabIndex = 10000029;
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
            this.cmdStartAuto.Location = new System.Drawing.Point(552, 16);
            this.cmdStartAuto.Name = "cmdStartAuto";
            this.cmdStartAuto.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdStartAuto.Size = new System.Drawing.Size(88, 32);
            this.cmdStartAuto.TabIndex = 10000026;
            this.cmdStartAuto.Text = "自动评分(&A)";
            this.cmdStartAuto.Click += new System.EventHandler(this.cmdStartAuto_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(820, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 16);
            this.label7.TabIndex = 432;
            // 
            // m_cmdGetDovueData
            // 
            this.m_cmdGetDovueData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cmdGetDovueData.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdGetDovueData.Location = new System.Drawing.Point(847, 28);
            this.m_cmdGetDovueData.Name = "m_cmdGetDovueData";
            this.m_cmdGetDovueData.Size = new System.Drawing.Size(4, 32);
            this.m_cmdGetDovueData.TabIndex = 10000004;
            this.m_cmdGetDovueData.Text = "监护仪最新结果";
            this.m_cmdGetDovueData.Visible = false;
            this.m_cmdGetDovueData.Click += new System.EventHandler(this.m_cmdGetDovueData_Click);
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
            this.m_lsvJY_ItemChoice.Location = new System.Drawing.Point(69, 440);
            this.m_lsvJY_ItemChoice.Name = "m_lsvJY_ItemChoice";
            this.m_lsvJY_ItemChoice.Size = new System.Drawing.Size(44, 44);
            this.m_lsvJY_ItemChoice.TabIndex = 10000006;
            this.m_lsvJY_ItemChoice.UseCompatibleStateImageBehavior = false;
            this.m_lsvJY_ItemChoice.View = System.Windows.Forms.View.Details;
            this.m_lsvJY_ItemChoice.Visible = false;
            this.m_lsvJY_ItemChoice.DoubleClick += new System.EventHandler(this.m_lsvJY_ItemChoice_DoubleClick);
            this.m_lsvJY_ItemChoice.Leave += new System.EventHandler(this.m_lsvJY_ItemChoice_Leave);
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
            // m_cmdSetLabCheckResult
            // 
            this.m_cmdSetLabCheckResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cmdSetLabCheckResult.Location = new System.Drawing.Point(846, 30);
            this.m_cmdSetLabCheckResult.Name = "m_cmdSetLabCheckResult";
            this.m_cmdSetLabCheckResult.Size = new System.Drawing.Size(4, 32);
            this.m_cmdSetLabCheckResult.TabIndex = 10000005;
            this.m_cmdSetLabCheckResult.Text = "最新检验结果";
            this.m_cmdSetLabCheckResult.Visible = false;
            this.m_cmdSetLabCheckResult.Click += new System.EventHandler(this.m_cmdSetLabCheckResult_Click);
            // 
            // cmdCalculate
            // 
            this.cmdCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdCalculate.DefaultScheme = true;
            this.cmdCalculate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdCalculate.ForeColor = System.Drawing.Color.Black;
            this.cmdCalculate.Hint = "";
            this.cmdCalculate.Location = new System.Drawing.Point(748, 344);
            this.cmdCalculate.Name = "cmdCalculate";
            this.cmdCalculate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCalculate.Size = new System.Drawing.Size(95, 32);
            this.cmdCalculate.TabIndex = 10000015;
            this.cmdCalculate.Text = "诊断(&C)";
            this.cmdCalculate.Click += new System.EventHandler(this.cmdCalculate_Click);
            // 
            // m_cmdEvalDoctor
            // 
            this.m_cmdEvalDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdEvalDoctor.DefaultScheme = true;
            this.m_cmdEvalDoctor.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdEvalDoctor.ForeColor = System.Drawing.Color.Black;
            this.m_cmdEvalDoctor.Hint = "";
            this.m_cmdEvalDoctor.Location = new System.Drawing.Point(393, 344);
            this.m_cmdEvalDoctor.Name = "m_cmdEvalDoctor";
            this.m_cmdEvalDoctor.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdEvalDoctor.Size = new System.Drawing.Size(84, 32);
            this.m_cmdEvalDoctor.TabIndex = 10000015;
            this.m_cmdEvalDoctor.Text = "评估者(&E)";
            // 
            // m_cmdGetCheckData
            // 
            this.m_cmdGetCheckData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdGetCheckData.DefaultScheme = true;
            this.m_cmdGetCheckData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGetCheckData.ForeColor = System.Drawing.Color.Black;
            this.m_cmdGetCheckData.Hint = "";
            this.m_cmdGetCheckData.Location = new System.Drawing.Point(666, 160);
            this.m_cmdGetCheckData.Name = "m_cmdGetCheckData";
            this.m_cmdGetCheckData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetCheckData.Size = new System.Drawing.Size(188, 28);
            this.m_cmdGetCheckData.TabIndex = 10000017;
            this.m_cmdGetCheckData.Text = "获取检验结果(&L)";
            this.m_cmdGetCheckData.Click += new System.EventHandler(this.m_cmdGetCheckData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(4, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 14);
            this.label1.TabIndex = 10000018;
            this.label1.Text = "呼吸:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTitle2);
            this.groupBox1.Controls.Add(this.lblTitle12);
            this.groupBox1.Controls.Add(this.txtHeartRate);
            this.groupBox1.Controls.Add(this.chkBreath);
            this.groupBox1.Controls.Add(this.lblTitle19);
            this.groupBox1.Controls.Add(this.txtBreath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 212);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 116);
            this.groupBox1.TabIndex = 10000019;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTitle16);
            this.groupBox2.Controls.Add(this.lblTitle3);
            this.groupBox2.Controls.Add(this.lblTitle17);
            this.groupBox2.Controls.Add(this.txtKPlus);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtNaPlus);
            this.groupBox2.Controls.Add(this.txtpH);
            this.groupBox2.Controls.Add(this.lblTitle11);
            this.groupBox2.Controls.Add(this.lblTitle8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(223, 212);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(224, 116);
            this.groupBox2.TabIndex = 10000020;
            this.groupBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(454, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 17);
            this.label5.TabIndex = 10000021;
            this.label5.Text = "小儿年龄组别:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(5, 336);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(854, 4);
            this.groupBox3.TabIndex = 10000022;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // frmBabyInjuryCaseEvaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.ClientSize = new System.Drawing.Size(869, 724);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboStomach);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpbBloodAndShk);
            this.Controls.Add(this.gpbCrAndBUN);
            this.Controls.Add(this.m_cmdGetCheckData);
            this.Controls.Add(this.cmdCalculate);
            this.Controls.Add(this.lblEvalDate);
            this.Controls.Add(this.lblTitle28);
            this.Controls.Add(this.txtEvalDoctor);
            this.Controls.Add(this.gpbAuto);
            this.Controls.Add(this.gpbHb);
            this.Controls.Add(this.gpbPao2);
            this.Controls.Add(this.dtpEvalDate);
            this.Controls.Add(this.dtgResult);
            this.Controls.Add(this.m_lsvJY_ItemChoice);
            this.Controls.Add(this.m_cmdEvalDoctor);
            this.Controls.Add(this.m_cmdSetLabCheckResult);
            this.Controls.Add(this.m_cmdGetDovueData);
            this.Controls.Add(this.rdbAgeU1);
            this.Controls.Add(this.rdbAgeO1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBabyInjuryCaseEvaluation";
            this.Text = "小儿危重病例评分";
            this.Load += new System.EventHandler(this.NewBabyInjuryCaseEvaluation_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.BabyInjuryCaseEvaluation_Closing);
            this.Controls.SetChildIndex(this.rdbAgeO1, 0);
            this.Controls.SetChildIndex(this.rdbAgeU1, 0);
            this.Controls.SetChildIndex(this.m_cmdGetDovueData, 0);
            this.Controls.SetChildIndex(this.m_cmdSetLabCheckResult, 0);
            this.Controls.SetChildIndex(this.m_cmdEvalDoctor, 0);
            this.Controls.SetChildIndex(this.m_lsvJY_ItemChoice, 0);
            this.Controls.SetChildIndex(this.dtgResult, 0);
            this.Controls.SetChildIndex(this.dtpEvalDate, 0);
            this.Controls.SetChildIndex(this.gpbPao2, 0);
            this.Controls.SetChildIndex(this.gpbHb, 0);
            this.Controls.SetChildIndex(this.gpbAuto, 0);
            this.Controls.SetChildIndex(this.txtEvalDoctor, 0);
            this.Controls.SetChildIndex(this.lblTitle28, 0);
            this.Controls.SetChildIndex(this.lblEvalDate, 0);
            this.Controls.SetChildIndex(this.cmdCalculate, 0);
            this.Controls.SetChildIndex(this.m_cmdGetCheckData, 0);
            this.Controls.SetChildIndex(this.gpbCrAndBUN, 0);
            this.Controls.SetChildIndex(this.gpbBloodAndShk, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.cboStomach, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.gpbCrAndBUN.ResumeLayout(false);
            this.gpbCrAndBUN.PerformLayout();
            this.gpbBloodAndShk.ResumeLayout(false);
            this.gpbBloodAndShk.PerformLayout();
            this.gpbPao2.ResumeLayout(false);
            this.gpbPao2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResult)).EndInit();
            this.gpbHb.ResumeLayout(false);
            this.gpbHb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timAutoCollect)).EndInit();
            this.gpbAuto.ResumeLayout(false);
            this.gpbAuto.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
    }
}
