using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.iCare.Template.Client;
namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// frmFlatAndsSportReport 的摘要说明。internal com.digitalwave.controls.ctlRichTextBox
	/// </summary>
	public class frmFlatAndSportReport: com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
	{
		private System.Windows.Forms.GroupBox groupBox3;
		internal PinkieControls.ButtonXP m_cmdClear;
		internal PinkieControls.ButtonXP m_cmdCreateTemplate;
		internal PinkieControls.ButtonXP m_cmdDelete;
		internal PinkieControls.ButtonXP m_cmdSave;
		internal PinkieControls.ButtonXP m_cmdConfirm;
		internal PinkieControls.ButtonXP m_cmdPrint;
		internal PinkieControls.ButtonXP m_cmdExit;
		internal PinkieControls.ButtonXP m_cmdEditTemplate;
		internal com.digitalwave.controls.ctlRichTextBox m_txtP_R_VCHR;
		internal System.Windows.Forms.ComboBox m_txtRHYTHM_VCHR;
		internal System.Windows.Forms.TextBox m_txtQ_T_VCHR;
		internal System.Windows.Forms.TextBox m_txtQRS_VCHR;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		internal System.Windows.Forms.Label m_labSubAge;
		internal System.Windows.Forms.TextBox m_txtSubAGE_FLT;
		internal System.Windows.Forms.TextBox m_txtAGE_FLT;
		internal System.Windows.Forms.ComboBox m_cmbAge;
		internal System.Windows.Forms.TextBox m_txtPATIENT_NAME_VCHR;
        internal System.Windows.Forms.TextBox m_txtREPORT_NO_CHR;
		internal System.Windows.Forms.TextBox m_txtINPATIENT_NO_CHR;
		internal System.Windows.Forms.TextBox m_txtPATIENT_NO_CHR;
		internal System.Windows.Forms.ComboBox m_cboSEX_CHR;
		private System.Windows.Forms.Label label14;
		internal System.Windows.Forms.TextBox m_txtBED_NO_CHR;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		internal System.Windows.Forms.DateTimePicker m_dtpCHECK_DAT;
		internal System.Windows.Forms.DateTimePicker m_dtpREPORT_DAT;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Panel panel1;
		internal System.Windows.Forms.TextBox m_txtLIE_PST_VCHR;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label25;
		internal System.Windows.Forms.TextBox txtSTAND_PST_VCHR;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label27;
		internal System.Windows.Forms.TextBox txtDEEP_BREATH_VCHR;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label29;
		internal System.Windows.Forms.TextBox txtBED_NO_CHR;
		internal System.Windows.Forms.TextBox textBoxTyped1;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.Label label42;
		internal System.Windows.Forms.TextBox textBoxTyped6;
		private System.Windows.Forms.Label label43;
		internal System.Windows.Forms.RadioButton rdbFORECAST_QTY_INT1;
		internal com.digitalwave.controls.ctlRichTextBox txtFORECAST_QTY_VCHR;
		internal System.Windows.Forms.RadioButton rdbFORECAST_QTY_INT2;
		internal System.Windows.Forms.ComboBox cboTEST_PLAN_VCHR;
		internal System.Windows.Forms.TextBox txtACTIVE_LOAD_LEVEL_VCHR;
		internal System.Windows.Forms.TextBox txtACTIVE_LOAD_MPH_VCHR;
		internal System.Windows.Forms.TextBox txtACTIVE_TOTAL_TIME_VCHR1;
		internal System.Windows.Forms.TextBox textBoxTyped2;
		internal System.Windows.Forms.TextBox txtACTIVE_TOTAL_TIME_VCHR2;
		private System.Windows.Forms.Label label36;
		internal System.Windows.Forms.TextBox txtHR_PER_VCHR;
		internal System.Windows.Forms.TextBox txtHR_MAX_WORK_VCHR;
		internal System.Windows.Forms.ComboBox cboSTOP_REASON_VCHR;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Label label38;
		internal com.digitalwave.controls.ctlRichTextBox  txtAPPEAR_LED_VCHR;
		internal System.Windows.Forms.ComboBox cboACTIVE_ST_VCHR;
		internal com.digitalwave.controls.ctlRichTextBox  txtHR_SCOPE_VCHR;
		private System.Windows.Forms.Label label45;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.Label label48;
		internal com.digitalwave.controls.ctlRichTextBox  txtTIME_SCOPE_VCHR;
		private System.Windows.Forms.Label label50;
		internal System.Windows.Forms.ComboBox COMACTIVE_ST_MODE_VCHR;
		internal System.Windows.Forms.RadioButton ACTIVE_ST_MAX_INT2;
		internal System.Windows.Forms.RadioButton ACTIVE_ST_MAX_INT1;
		internal System.Windows.Forms.TextBox txtACTIVE_ST_VCHR;
		private System.Windows.Forms.Label label44;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.Label label49;
		internal com.digitalwave.controls.ctlRichTextBox txtHR_WRONG_VCHR;
		private System.Windows.Forms.Label label51;
		internal com.digitalwave.controls.ctlRichTextBox txtACTIVED_BP_VCHR;

		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		internal com.digitalwave.controls.ctlRichTextBox txtTEST_RESULT_VCHR;
		internal com.digitalwave.controls.ctlRichTextBox txtACTIVE_RESULT_VCHR;
		private System.Windows.Forms.ToolTip m_ttpTextInfo;
		internal System.Drawing.Printing.PrintDocument m_printDoc;
		private System.Windows.Forms.ContextMenu mnuRichTextBox;
		private System.Windows.Forms.MenuItem mnuRichTextBoxDelete;
		private System.Windows.Forms.MenuItem meuUndoDel;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		internal System.Windows.Forms.PrintPreviewDialog m_printPrevDlg;
		internal System.Windows.Forms.PrintDialog m_printDlg;
		internal System.Windows.Forms.CheckBox m_cheIsNew;
		internal com.digitalwave.controls.ctlRichTextBox txtHR_time_VCHR;
		internal com.digitalwave.controls.ctlRichTextBox txtHR_daolink_VCHR;
		private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label23;
        public ListViewBox m_txtREPORTOR_NAME_VCHR;
        public ListViewBox m_txtDEPT_NAME_VCHR;
        public com.digitalwave.controls.clsCardTextBox carID;
		private System.ComponentModel.IContainer components;
        //public frmCardiogramReportManage m_objMainFormManage = null;
		public frmFlatAndSportReport()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
            ((clsControllerFlatAndSportReport)this.objController).m_mthInitData();
            carID.Text = "";
            m_txtPATIENT_NO_CHR.Text = "";
            m_txtSubAGE_FLT.Text = "";

			this.m_cboSEX_CHR.SelectedIndex=0;
			this.m_cmbAge.SelectedIndex=0;
//			this.m_txtRHYTHM_VCHR.SelectedIndex=0;
			m_mthInitRichTextBox();
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFlatAndSportReport));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_cmdClear = new PinkieControls.ButtonXP();
            this.m_cmdCreateTemplate = new PinkieControls.ButtonXP();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.m_cmdConfirm = new PinkieControls.ButtonXP();
            this.m_cmdPrint = new PinkieControls.ButtonXP();
            this.m_cmdExit = new PinkieControls.ButtonXP();
            this.m_cmdEditTemplate = new PinkieControls.ButtonXP();
            this.m_txtP_R_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtLIE_PST_VCHR = new System.Windows.Forms.TextBox();
            this.m_txtRHYTHM_VCHR = new System.Windows.Forms.ComboBox();
            this.m_txtQ_T_VCHR = new System.Windows.Forms.TextBox();
            this.m_txtQRS_VCHR = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_txtREPORTOR_NAME_VCHR = new ListViewBox();
            this.m_txtDEPT_NAME_VCHR = new ListViewBox();
            this.label23 = new System.Windows.Forms.Label();
            this.m_labSubAge = new System.Windows.Forms.Label();
            this.m_txtSubAGE_FLT = new System.Windows.Forms.TextBox();
            this.m_txtAGE_FLT = new System.Windows.Forms.TextBox();
            this.m_cmbAge = new System.Windows.Forms.ComboBox();
            this.m_txtPATIENT_NAME_VCHR = new System.Windows.Forms.TextBox();
            this.m_txtREPORT_NO_CHR = new System.Windows.Forms.TextBox();
            this.m_txtINPATIENT_NO_CHR = new System.Windows.Forms.TextBox();
            this.m_txtPATIENT_NO_CHR = new System.Windows.Forms.TextBox();
            this.m_cboSEX_CHR = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.m_txtBED_NO_CHR = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.m_dtpCHECK_DAT = new System.Windows.Forms.DateTimePicker();
            this.m_dtpREPORT_DAT = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtDEEP_BREATH_VCHR = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtSTAND_PST_VCHR = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.txtBED_NO_CHR = new System.Windows.Forms.TextBox();
            this.textBoxTyped1 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cboSTOP_REASON_VCHR = new System.Windows.Forms.ComboBox();
            this.txtHR_MAX_WORK_VCHR = new System.Windows.Forms.TextBox();
            this.txtHR_PER_VCHR = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtACTIVE_TOTAL_TIME_VCHR2 = new System.Windows.Forms.TextBox();
            this.textBoxTyped2 = new System.Windows.Forms.TextBox();
            this.txtACTIVE_LOAD_LEVEL_VCHR = new System.Windows.Forms.TextBox();
            this.cboTEST_PLAN_VCHR = new System.Windows.Forms.ComboBox();
            this.rdbFORECAST_QTY_INT2 = new System.Windows.Forms.RadioButton();
            this.rdbFORECAST_QTY_INT1 = new System.Windows.Forms.RadioButton();
            this.label32 = new System.Windows.Forms.Label();
            this.txtACTIVE_LOAD_MPH_VCHR = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.txtFORECAST_QTY_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.txtACTIVE_TOTAL_TIME_VCHR1 = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.textBoxTyped6 = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtHR_WRONG_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.mnuRichTextBox = new System.Windows.Forms.ContextMenu();
            this.mnuRichTextBoxDelete = new System.Windows.Forms.MenuItem();
            this.meuUndoDel = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.label49 = new System.Windows.Forms.Label();
            this.txtHR_time_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.txtHR_daolink_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txtACTIVE_ST_VCHR = new System.Windows.Forms.TextBox();
            this.ACTIVE_ST_MAX_INT2 = new System.Windows.Forms.RadioButton();
            this.ACTIVE_ST_MAX_INT1 = new System.Windows.Forms.RadioButton();
            this.COMACTIVE_ST_MODE_VCHR = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.txtAPPEAR_LED_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.cboACTIVE_ST_VCHR = new System.Windows.Forms.ComboBox();
            this.txtHR_SCOPE_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.txtTIME_SCOPE_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.txtACTIVED_BP_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTEST_RESULT_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtACTIVE_RESULT_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.m_ttpTextInfo = new System.Windows.Forms.ToolTip(this.components);
            this.m_printDoc = new System.Drawing.Printing.PrintDocument();
            this.m_printPrevDlg = new System.Windows.Forms.PrintPreviewDialog();
            this.m_printDlg = new System.Windows.Forms.PrintDialog();
            this.m_cheIsNew = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.carID = new com.digitalwave.controls.clsCardTextBox();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_cmdClear);
            this.groupBox3.Controls.Add(this.m_cmdCreateTemplate);
            this.groupBox3.Controls.Add(this.m_cmdDelete);
            this.groupBox3.Controls.Add(this.m_cmdSave);
            this.groupBox3.Controls.Add(this.m_cmdConfirm);
            this.groupBox3.Controls.Add(this.m_cmdPrint);
            this.groupBox3.Controls.Add(this.m_cmdExit);
            this.groupBox3.Controls.Add(this.m_cmdEditTemplate);
            this.groupBox3.Location = new System.Drawing.Point(8, 400);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(284, 304);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // m_cmdClear
            // 
            this.m_cmdClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdClear.DefaultScheme = true;
            this.m_cmdClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdClear.Hint = "";
            this.m_cmdClear.Location = new System.Drawing.Point(40, 226);
            this.m_cmdClear.Name = "m_cmdClear";
            this.m_cmdClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdClear.Size = new System.Drawing.Size(184, 32);
            this.m_cmdClear.TabIndex = 6;
            this.m_cmdClear.Text = "清      空";
            this.m_cmdClear.Click += new System.EventHandler(this.m_cmdClear_Click);
            // 
            // m_cmdCreateTemplate
            // 
            this.m_cmdCreateTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdCreateTemplate.DefaultScheme = true;
            this.m_cmdCreateTemplate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdCreateTemplate.Hint = "";
            this.m_cmdCreateTemplate.Location = new System.Drawing.Point(40, 156);
            this.m_cmdCreateTemplate.Name = "m_cmdCreateTemplate";
            this.m_cmdCreateTemplate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCreateTemplate.Size = new System.Drawing.Size(184, 32);
            this.m_cmdCreateTemplate.TabIndex = 4;
            this.m_cmdCreateTemplate.Text = "生 成 模 板";
            this.m_cmdCreateTemplate.Click += new System.EventHandler(this.m_cmdCreateTemplate_Click);
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(40, 51);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete.Size = new System.Drawing.Size(184, 32);
            this.m_cmdDelete.TabIndex = 1;
            this.m_cmdDelete.Text = "删      除";
            this.m_cmdDelete.Click += new System.EventHandler(this.m_cmdDelete_Click);
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(40, 16);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(184, 32);
            this.m_cmdSave.TabIndex = 0;
            this.m_cmdSave.Text = "保      存";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // m_cmdConfirm
            // 
            this.m_cmdConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdConfirm.DefaultScheme = true;
            this.m_cmdConfirm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdConfirm.Hint = "";
            this.m_cmdConfirm.Location = new System.Drawing.Point(40, 86);
            this.m_cmdConfirm.Name = "m_cmdConfirm";
            this.m_cmdConfirm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdConfirm.Size = new System.Drawing.Size(184, 32);
            this.m_cmdConfirm.TabIndex = 2;
            this.m_cmdConfirm.Text = "审      核";
            this.m_cmdConfirm.Click += new System.EventHandler(this.m_cmdConfirm_Click);
            // 
            // m_cmdPrint
            // 
            this.m_cmdPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdPrint.DefaultScheme = true;
            this.m_cmdPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdPrint.Hint = "";
            this.m_cmdPrint.Location = new System.Drawing.Point(40, 121);
            this.m_cmdPrint.Name = "m_cmdPrint";
            this.m_cmdPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdPrint.Size = new System.Drawing.Size(184, 32);
            this.m_cmdPrint.TabIndex = 3;
            this.m_cmdPrint.Text = "打      印";
            this.m_cmdPrint.Click += new System.EventHandler(this.m_cmdPrint_Click);
            // 
            // m_cmdExit
            // 
            this.m_cmdExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdExit.DefaultScheme = true;
            this.m_cmdExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdExit.Hint = "";
            this.m_cmdExit.Location = new System.Drawing.Point(40, 261);
            this.m_cmdExit.Name = "m_cmdExit";
            this.m_cmdExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdExit.Size = new System.Drawing.Size(184, 32);
            this.m_cmdExit.TabIndex = 7;
            this.m_cmdExit.Text = "退      出";
            this.m_cmdExit.Click += new System.EventHandler(this.m_cmdExit_Click);
            // 
            // m_cmdEditTemplate
            // 
            this.m_cmdEditTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdEditTemplate.DefaultScheme = true;
            this.m_cmdEditTemplate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdEditTemplate.Hint = "";
            this.m_cmdEditTemplate.Location = new System.Drawing.Point(40, 191);
            this.m_cmdEditTemplate.Name = "m_cmdEditTemplate";
            this.m_cmdEditTemplate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdEditTemplate.Size = new System.Drawing.Size(184, 32);
            this.m_cmdEditTemplate.TabIndex = 5;
            this.m_cmdEditTemplate.Text = "修 改 模 板";
            this.m_cmdEditTemplate.Click += new System.EventHandler(this.m_cmdEditTemplate_Click);
            // 
            // m_txtP_R_VCHR
            // 
            this.m_txtP_R_VCHR.AccessibleDescription = "P-R间期";
            this.m_txtP_R_VCHR.BackColor = System.Drawing.Color.White;
            this.m_txtP_R_VCHR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtP_R_VCHR.ForeColor = System.Drawing.Color.Black;
            this.m_txtP_R_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtP_R_VCHR.Location = new System.Drawing.Point(256, 61);
            this.m_txtP_R_VCHR.m_BlnIgnoreUserInfo = true;
            this.m_txtP_R_VCHR.m_BlnPartControl = false;
            this.m_txtP_R_VCHR.m_BlnReadOnly = false;
            this.m_txtP_R_VCHR.m_BlnUnderLineDST = false;
            this.m_txtP_R_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtP_R_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtP_R_VCHR.m_IntCanModifyTime = 6;
            this.m_txtP_R_VCHR.m_IntPartControlLength = 0;
            this.m_txtP_R_VCHR.m_IntPartControlStartIndex = 0;
            this.m_txtP_R_VCHR.m_StrUserID = "";
            this.m_txtP_R_VCHR.m_StrUserName = "";
            this.m_txtP_R_VCHR.MaxLength = 7;
            this.m_txtP_R_VCHR.Multiline = false;
            this.m_txtP_R_VCHR.Name = "m_txtP_R_VCHR";
            this.m_txtP_R_VCHR.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtP_R_VCHR.Size = new System.Drawing.Size(72, 24);
            this.m_txtP_R_VCHR.TabIndex = 10;
            this.m_txtP_R_VCHR.Text = "";
            this.m_txtP_R_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtP_R_VCHR_KeyDown);
            // 
            // m_txtLIE_PST_VCHR
            // 
            //this.m_txtLIE_PST_VCHR.EnableAutoValidation = false;
            //this.m_txtLIE_PST_VCHR.EnableEnterKeyValidate = true;
            //this.m_txtLIE_PST_VCHR.EnableEscapeKeyUndo = true;
            //this.m_txtLIE_PST_VCHR.EnableLastValidValue = true;
            //this.m_txtLIE_PST_VCHR.ErrorProvider = null;
            //this.m_txtLIE_PST_VCHR.ErrorProviderMessage = "Invalid value";
            //this.m_txtLIE_PST_VCHR.ForceFormatText = true;
            this.m_txtLIE_PST_VCHR.Location = new System.Drawing.Point(320, 24);
            this.m_txtLIE_PST_VCHR.MaxLength = 7;
            this.m_txtLIE_PST_VCHR.Name = "m_txtLIE_PST_VCHR";
            this.m_txtLIE_PST_VCHR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_txtLIE_PST_VCHR.Size = new System.Drawing.Size(80, 23);
            this.m_txtLIE_PST_VCHR.TabIndex = 2;
            this.m_txtLIE_PST_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtLIE_PST_VCHR_KeyDown);
            // 
            // m_txtRHYTHM_VCHR
            // 
            this.m_txtRHYTHM_VCHR.Items.AddRange(new object[] {
            "窦性",
            "异位",
            "游走",
            "起博器",
            "窦性+交界性",
            "窦性+室心逸搏",
            "窦性+起搏",
            ""});
            this.m_txtRHYTHM_VCHR.Location = new System.Drawing.Point(64, 24);
            this.m_txtRHYTHM_VCHR.Name = "m_txtRHYTHM_VCHR";
            this.m_txtRHYTHM_VCHR.Size = new System.Drawing.Size(136, 22);
            this.m_txtRHYTHM_VCHR.TabIndex = 0;
            this.m_txtRHYTHM_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtRHYTHM_VCHR_KeyDown);
            // 
            // m_txtQ_T_VCHR
            // 
            //this.m_txtQ_T_VCHR.EnableAutoValidation = false;
            //this.m_txtQ_T_VCHR.EnableEnterKeyValidate = false;
            //this.m_txtQ_T_VCHR.EnableEscapeKeyUndo = true;
            //this.m_txtQ_T_VCHR.EnableLastValidValue = false;
            //this.m_txtQ_T_VCHR.ErrorProvider = null;
            //this.m_txtQ_T_VCHR.ErrorProviderMessage = "输入非法字符";
            //this.m_txtQ_T_VCHR.ForceFormatText = true;
            this.m_txtQ_T_VCHR.Location = new System.Drawing.Point(560, 62);
            this.m_txtQ_T_VCHR.MaxLength = 7;
            this.m_txtQ_T_VCHR.Name = "m_txtQ_T_VCHR";
            this.m_txtQ_T_VCHR.Size = new System.Drawing.Size(40, 23);
            this.m_txtQ_T_VCHR.TabIndex = 16;
            this.m_txtQ_T_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtQ_T_VCHR_KeyDown);
            // 
            // m_txtQRS_VCHR
            // 
            //this.m_txtQRS_VCHR.EnableAutoValidation = false;
            //this.m_txtQRS_VCHR.EnableEnterKeyValidate = false;
            //this.m_txtQRS_VCHR.EnableEscapeKeyUndo = true;
            //this.m_txtQRS_VCHR.EnableLastValidValue = true;
            //this.m_txtQRS_VCHR.ErrorProvider = null;
            //this.m_txtQRS_VCHR.ErrorProviderMessage = "输入非法字符";
            //this.m_txtQRS_VCHR.ForceFormatText = true;
            this.m_txtQRS_VCHR.Location = new System.Drawing.Point(440, 62);
            this.m_txtQRS_VCHR.MaxLength = 7;
            this.m_txtQRS_VCHR.Name = "m_txtQRS_VCHR";
            this.m_txtQRS_VCHR.Size = new System.Drawing.Size(48, 23);
            this.m_txtQRS_VCHR.TabIndex = 13;
            this.m_txtQRS_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtQRS_VCHR_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "，心率：卧位:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(368, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 14);
            this.label6.TabIndex = 12;
            this.label6.Text = "QRS时限:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(336, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 14);
            this.label5.TabIndex = 11;
            this.label5.Text = "秒，";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(640, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 14);
            this.label9.TabIndex = 11;
            this.label9.Text = "秒。";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "节  律";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(528, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 14);
            this.label8.TabIndex = 15;
            this.label8.Text = "Q-T:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(496, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 14);
            this.label7.TabIndex = 14;
            this.label7.Text = "秒，";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(184, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 9;
            this.label4.Text = "P-R 间期";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(408, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "次/分，";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.carID);
            this.groupBox1.Controls.Add(this.m_txtREPORTOR_NAME_VCHR);
            this.groupBox1.Controls.Add(this.m_txtDEPT_NAME_VCHR);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.m_labSubAge);
            this.groupBox1.Controls.Add(this.m_txtSubAGE_FLT);
            this.groupBox1.Controls.Add(this.m_txtAGE_FLT);
            this.groupBox1.Controls.Add(this.m_cmbAge);
            this.groupBox1.Controls.Add(this.m_txtPATIENT_NAME_VCHR);
            this.groupBox1.Controls.Add(this.m_txtREPORT_NO_CHR);
            this.groupBox1.Controls.Add(this.m_txtINPATIENT_NO_CHR);
            this.groupBox1.Controls.Add(this.m_txtPATIENT_NO_CHR);
            this.groupBox1.Controls.Add(this.m_cboSEX_CHR);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.m_txtBED_NO_CHR);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.m_dtpCHECK_DAT);
            this.groupBox1.Controls.Add(this.m_dtpREPORT_DAT);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Location = new System.Drawing.Point(8, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 400);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本资料";
            // 
            // m_txtREPORTOR_NAME_VCHR
            // 
            this.m_txtREPORTOR_NAME_VCHR.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtREPORTOR_NAME_VCHR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtREPORTOR_NAME_VCHR.intHeight = 200;
            this.m_txtREPORTOR_NAME_VCHR.IsEnterShow = true;
            this.m_txtREPORTOR_NAME_VCHR.isHide = 3;
            this.m_txtREPORTOR_NAME_VCHR.IsTextChangeHideListView = false;
            this.m_txtREPORTOR_NAME_VCHR.isTxt = 1;
            this.m_txtREPORTOR_NAME_VCHR.isUpOrDn = 0;
            this.m_txtREPORTOR_NAME_VCHR.isValuse = 3;
            this.m_txtREPORTOR_NAME_VCHR.Location = new System.Drawing.Point(76, 368);
            this.m_txtREPORTOR_NAME_VCHR.m_IntMaxListLength = 25;
            this.m_txtREPORTOR_NAME_VCHR.m_IsHaveParent = false;
            this.m_txtREPORTOR_NAME_VCHR.m_strParentName = "";
            this.m_txtREPORTOR_NAME_VCHR.Name = "m_txtREPORTOR_NAME_VCHR";
            this.m_txtREPORTOR_NAME_VCHR.SelectedItemBackColor = System.Drawing.Color.ForestGreen;
            this.m_txtREPORTOR_NAME_VCHR.SelectedItemForeColor = System.Drawing.Color.White;
            this.m_txtREPORTOR_NAME_VCHR.Size = new System.Drawing.Size(184, 23);
            this.m_txtREPORTOR_NAME_VCHR.TabIndex = 13;
            this.m_txtREPORTOR_NAME_VCHR.txtValuse = "";
            this.m_txtREPORTOR_NAME_VCHR.VsLeftOrRight = 1;
            // 
            // m_txtDEPT_NAME_VCHR
            // 
            this.m_txtDEPT_NAME_VCHR.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtDEPT_NAME_VCHR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDEPT_NAME_VCHR.intHeight = 200;
            this.m_txtDEPT_NAME_VCHR.IsEnterShow = true;
            this.m_txtDEPT_NAME_VCHR.isHide = 3;
            this.m_txtDEPT_NAME_VCHR.IsTextChangeHideListView = false;
            this.m_txtDEPT_NAME_VCHR.isTxt = 1;
            this.m_txtDEPT_NAME_VCHR.isUpOrDn = 0;
            this.m_txtDEPT_NAME_VCHR.isValuse = 3;
            this.m_txtDEPT_NAME_VCHR.Location = new System.Drawing.Point(76, 240);
            this.m_txtDEPT_NAME_VCHR.m_IntMaxListLength = 25;
            this.m_txtDEPT_NAME_VCHR.m_IsHaveParent = false;
            this.m_txtDEPT_NAME_VCHR.m_strParentName = "";
            this.m_txtDEPT_NAME_VCHR.Name = "m_txtDEPT_NAME_VCHR";
            this.m_txtDEPT_NAME_VCHR.SelectedItemBackColor = System.Drawing.Color.ForestGreen;
            this.m_txtDEPT_NAME_VCHR.SelectedItemForeColor = System.Drawing.Color.White;
            this.m_txtDEPT_NAME_VCHR.Size = new System.Drawing.Size(184, 23);
            this.m_txtDEPT_NAME_VCHR.TabIndex = 9;
            this.m_txtDEPT_NAME_VCHR.txtValuse = "";
            this.m_txtDEPT_NAME_VCHR.VsLeftOrRight = 1;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 27);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(70, 14);
            this.label23.TabIndex = 118;
            this.label23.Text = "卡    号:";
            // 
            // m_labSubAge
            // 
            this.m_labSubAge.AutoSize = true;
            this.m_labSubAge.Location = new System.Drawing.Point(243, 216);
            this.m_labSubAge.Name = "m_labSubAge";
            this.m_labSubAge.Size = new System.Drawing.Size(21, 14);
            this.m_labSubAge.TabIndex = 116;
            this.m_labSubAge.Text = "月";
            // 
            // m_txtSubAGE_FLT
            // 
            this.m_txtSubAGE_FLT.CausesValidation = false;
            //this.m_txtSubAGE_FLT.EnableAutoValidation = false;
            //this.m_txtSubAGE_FLT.EnableEnterKeyValidate = false;
            //this.m_txtSubAGE_FLT.EnableEscapeKeyUndo = true;
            //this.m_txtSubAGE_FLT.EnableLastValidValue = true;
            //this.m_txtSubAGE_FLT.ErrorProvider = null;
            //this.m_txtSubAGE_FLT.ErrorProviderMessage = "Invalid value";
            //this.m_txtSubAGE_FLT.ForceFormatText = true;
            this.m_txtSubAGE_FLT.Location = new System.Drawing.Point(192, 208);
            this.m_txtSubAGE_FLT.MaxLength = 4;
            this.m_txtSubAGE_FLT.Name = "m_txtSubAGE_FLT";
            //this.m_txtSubAGE_FLT.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
            this.m_txtSubAGE_FLT.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtSubAGE_FLT.Size = new System.Drawing.Size(48, 23);
            this.m_txtSubAGE_FLT.TabIndex = 8;
            this.m_txtSubAGE_FLT.Text = "0";
            this.m_txtSubAGE_FLT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtSubAGE_FLT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtSubAGE_FLT_KeyDown);
            // 
            // m_txtAGE_FLT
            // 
            this.m_txtAGE_FLT.CausesValidation = false;
            //this.m_txtAGE_FLT.EnableAutoValidation = false;
            //this.m_txtAGE_FLT.EnableEnterKeyValidate = false;
            //this.m_txtAGE_FLT.EnableEscapeKeyUndo = true;
            //this.m_txtAGE_FLT.EnableLastValidValue = true;
            //this.m_txtAGE_FLT.ErrorProvider = null;
            //this.m_txtAGE_FLT.ErrorProviderMessage = "Invalid value";
            //this.m_txtAGE_FLT.ForceFormatText = true;
            this.m_txtAGE_FLT.Location = new System.Drawing.Point(76, 208);
            this.m_txtAGE_FLT.MaxLength = 4;
            this.m_txtAGE_FLT.Name = "m_txtAGE_FLT";
            this.m_txtAGE_FLT.Size = new System.Drawing.Size(48, 23);
            this.m_txtAGE_FLT.TabIndex = 6;
            this.m_txtAGE_FLT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtAGE_FLT_KeyDown);
            // 
            // m_cmbAge
            // 
            this.m_cmbAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbAge.Items.AddRange(new object[] {
            "岁",
            "月",
            "天",
            "小时"});
            this.m_cmbAge.Location = new System.Drawing.Point(128, 208);
            this.m_cmbAge.Name = "m_cmbAge";
            this.m_cmbAge.Size = new System.Drawing.Size(56, 22);
            this.m_cmbAge.TabIndex = 7;
            this.m_cmbAge.SelectedIndexChanged += new System.EventHandler(this.m_cmbAge_SelectedIndexChanged);
            this.m_cmbAge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cmbAge_KeyDown);
            // 
            // m_txtPATIENT_NAME_VCHR
            // 
            //this.m_txtPATIENT_NAME_VCHR.EnableAutoValidation = false;
            //this.m_txtPATIENT_NAME_VCHR.EnableEnterKeyValidate = false;
            //this.m_txtPATIENT_NAME_VCHR.EnableEscapeKeyUndo = true;
            //this.m_txtPATIENT_NAME_VCHR.EnableLastValidValue = true;
            //this.m_txtPATIENT_NAME_VCHR.ErrorProvider = null;
            //this.m_txtPATIENT_NAME_VCHR.ErrorProviderMessage = "Invalid value";
            this.m_txtPATIENT_NAME_VCHR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.m_txtPATIENT_NAME_VCHR.ForceFormatText = true;
            this.m_txtPATIENT_NAME_VCHR.Location = new System.Drawing.Point(76, 144);
            this.m_txtPATIENT_NAME_VCHR.MaxLength = 10;
            this.m_txtPATIENT_NAME_VCHR.Name = "m_txtPATIENT_NAME_VCHR";
            this.m_txtPATIENT_NAME_VCHR.Size = new System.Drawing.Size(184, 23);
            this.m_txtPATIENT_NAME_VCHR.TabIndex = 4;
            this.m_txtPATIENT_NAME_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtPATIENT_NAME_VCHR_KeyDown);
            // 
            // m_txtREPORT_NO_CHR
            // 
            //this.m_txtREPORT_NO_CHR.EnableAutoValidation = false;
            //this.m_txtREPORT_NO_CHR.EnableEnterKeyValidate = false;
            //this.m_txtREPORT_NO_CHR.EnableEscapeKeyUndo = true;
            //this.m_txtREPORT_NO_CHR.EnableLastValidValue = true;
            //this.m_txtREPORT_NO_CHR.ErrorProvider = null;
            //this.m_txtREPORT_NO_CHR.ErrorProviderMessage = "Invalid value";
            //this.m_txtREPORT_NO_CHR.ForceFormatText = true;
            this.m_txtREPORT_NO_CHR.Location = new System.Drawing.Point(76, 112);
            this.m_txtREPORT_NO_CHR.MaxLength = 8;
            this.m_txtREPORT_NO_CHR.Name = "m_txtREPORT_NO_CHR";
            this.m_txtREPORT_NO_CHR.Size = new System.Drawing.Size(184, 23);
            this.m_txtREPORT_NO_CHR.TabIndex = 1;
            this.m_txtREPORT_NO_CHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtREPORT_NO_CHR_KeyDown);
            // 
            // m_txtINPATIENT_NO_CHR
            // 
            this.m_txtINPATIENT_NO_CHR.CausesValidation = false;
            //this.m_txtINPATIENT_NO_CHR.EnableAutoValidation = true;
            //this.m_txtINPATIENT_NO_CHR.EnableEnterKeyValidate = false;
            //this.m_txtINPATIENT_NO_CHR.EnableEscapeKeyUndo = true;
            //this.m_txtINPATIENT_NO_CHR.EnableLastValidValue = false;
            //this.m_txtINPATIENT_NO_CHR.ErrorProvider = null;
            //this.m_txtINPATIENT_NO_CHR.ErrorProviderMessage = "Invalid value";
            //this.m_txtINPATIENT_NO_CHR.ForceFormatText = true;
            this.m_txtINPATIENT_NO_CHR.Location = new System.Drawing.Point(76, 52);
            this.m_txtINPATIENT_NO_CHR.MaxLength = 15;
            this.m_txtINPATIENT_NO_CHR.Name = "m_txtINPATIENT_NO_CHR";
            this.m_txtINPATIENT_NO_CHR.Size = new System.Drawing.Size(184, 23);
            this.m_txtINPATIENT_NO_CHR.TabIndex = 3;
            this.m_txtINPATIENT_NO_CHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtINPATIENT_NO_CHR_KeyDown);
            // 
            // m_txtPATIENT_NO_CHR
            // 
            //this.m_txtPATIENT_NO_CHR.EnableAutoValidation = false;
            //this.m_txtPATIENT_NO_CHR.EnableEnterKeyValidate = false;
            //this.m_txtPATIENT_NO_CHR.EnableEscapeKeyUndo = true;
            //this.m_txtPATIENT_NO_CHR.EnableLastValidValue = true;
            //this.m_txtPATIENT_NO_CHR.ErrorProvider = null;
            //this.m_txtPATIENT_NO_CHR.ErrorProviderMessage = "Invalid value";
            //this.m_txtPATIENT_NO_CHR.ForceFormatText = true;
            this.m_txtPATIENT_NO_CHR.Location = new System.Drawing.Point(76, 80);
            this.m_txtPATIENT_NO_CHR.MaxLength = 10;
            this.m_txtPATIENT_NO_CHR.Name = "m_txtPATIENT_NO_CHR";
            //this.m_txtPATIENT_NO_CHR.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol;
            this.m_txtPATIENT_NO_CHR.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtPATIENT_NO_CHR.Size = new System.Drawing.Size(184, 23);
            this.m_txtPATIENT_NO_CHR.TabIndex = 2;
            this.m_txtPATIENT_NO_CHR.Text = "0";
            this.m_txtPATIENT_NO_CHR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtPATIENT_NO_CHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtPATIENT_NO_CHR_KeyDown);
            // 
            // m_cboSEX_CHR
            // 
            this.m_cboSEX_CHR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSEX_CHR.Items.AddRange(new object[] {
            "男",
            "女",
            "不详"});
            this.m_cboSEX_CHR.Location = new System.Drawing.Point(76, 176);
            this.m_cboSEX_CHR.Name = "m_cboSEX_CHR";
            this.m_cboSEX_CHR.Size = new System.Drawing.Size(184, 22);
            this.m_cboSEX_CHR.TabIndex = 5;
            this.m_cboSEX_CHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboSEX_CHR_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 372);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 14);
            this.label14.TabIndex = 36;
            this.label14.Text = "报 告 者:";
            // 
            // m_txtBED_NO_CHR
            // 
            this.m_txtBED_NO_CHR.Location = new System.Drawing.Point(76, 272);
            this.m_txtBED_NO_CHR.MaxLength = 8;
            this.m_txtBED_NO_CHR.Name = "m_txtBED_NO_CHR";
            this.m_txtBED_NO_CHR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_txtBED_NO_CHR.Size = new System.Drawing.Size(184, 23);
            this.m_txtBED_NO_CHR.TabIndex = 10;
            this.m_txtBED_NO_CHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBED_NO_CHR_KeyDown);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(8, 276);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(70, 14);
            this.label22.TabIndex = 25;
            this.label22.Text = "床    号:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 244);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(70, 14);
            this.label21.TabIndex = 24;
            this.label21.Text = "科    室:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 212);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 14);
            this.label20.TabIndex = 23;
            this.label20.Text = "年    龄:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 180);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 14);
            this.label19.TabIndex = 22;
            this.label19.Text = "性    别:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 152);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(70, 14);
            this.label18.TabIndex = 21;
            this.label18.Text = "姓    名:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 56);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(70, 14);
            this.label17.TabIndex = 20;
            this.label17.Text = "住 院 号:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 85);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 14);
            this.label16.TabIndex = 19;
            this.label16.Text = "门 诊 号:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 117);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 14);
            this.label15.TabIndex = 18;
            this.label15.Text = "平 板 号:";
            // 
            // m_dtpCHECK_DAT
            // 
            this.m_dtpCHECK_DAT.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.m_dtpCHECK_DAT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpCHECK_DAT.Location = new System.Drawing.Point(76, 304);
            this.m_dtpCHECK_DAT.Name = "m_dtpCHECK_DAT";
            this.m_dtpCHECK_DAT.Size = new System.Drawing.Size(184, 23);
            this.m_dtpCHECK_DAT.TabIndex = 11;
            this.m_dtpCHECK_DAT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpCHECK_DAT_KeyDown);
            // 
            // m_dtpREPORT_DAT
            // 
            this.m_dtpREPORT_DAT.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.m_dtpREPORT_DAT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpREPORT_DAT.Location = new System.Drawing.Point(76, 336);
            this.m_dtpREPORT_DAT.Name = "m_dtpREPORT_DAT";
            this.m_dtpREPORT_DAT.Size = new System.Drawing.Size(184, 23);
            this.m_dtpREPORT_DAT.TabIndex = 12;
            this.m_dtpREPORT_DAT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpREPORT_DAT_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 340);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 14);
            this.label12.TabIndex = 14;
            this.label12.Text = "报告日期:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 309);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 14);
            this.label13.TabIndex = 15;
            this.label13.Text = "检查日期:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 712);
            this.panel1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.txtDEEP_BREATH_VCHR);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.txtSTAND_PST_VCHR);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.m_txtRHYTHM_VCHR);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.m_txtLIE_PST_VCHR);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.m_txtP_R_VCHR);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.m_txtQRS_VCHR);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.m_txtQ_T_VCHR);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(8, 48);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(688, 96);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "运动前心电图";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(144, 64);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(42, 14);
            this.label27.TabIndex = 8;
            this.label27.Text = "次/分";
            // 
            // txtDEEP_BREATH_VCHR
            // 
            //this.txtDEEP_BREATH_VCHR.EnableAutoValidation = false;
            //this.txtDEEP_BREATH_VCHR.EnableEnterKeyValidate = true;
            //this.txtDEEP_BREATH_VCHR.EnableEscapeKeyUndo = true;
            //this.txtDEEP_BREATH_VCHR.EnableLastValidValue = true;
            //this.txtDEEP_BREATH_VCHR.ErrorProvider = null;
            //this.txtDEEP_BREATH_VCHR.ErrorProviderMessage = "Invalid value";
            //this.txtDEEP_BREATH_VCHR.ForceFormatText = true;
            this.txtDEEP_BREATH_VCHR.Location = new System.Drawing.Point(64, 62);
            this.txtDEEP_BREATH_VCHR.MaxLength = 7;
            this.txtDEEP_BREATH_VCHR.Name = "txtDEEP_BREATH_VCHR";
            this.txtDEEP_BREATH_VCHR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDEEP_BREATH_VCHR.Size = new System.Drawing.Size(80, 23);
            this.txtDEEP_BREATH_VCHR.TabIndex = 7;
            this.txtDEEP_BREATH_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDEEP_BREATH_VCHR_KeyDown);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 64);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(49, 14);
            this.label28.TabIndex = 6;
            this.label28.Text = "深呼吸";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(624, 26);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(56, 14);
            this.label25.TabIndex = 162;
            this.label25.Text = "次/分，";
            // 
            // txtSTAND_PST_VCHR
            // 
            //this.txtSTAND_PST_VCHR.EnableAutoValidation = false;
            //this.txtSTAND_PST_VCHR.EnableEnterKeyValidate = true;
            //this.txtSTAND_PST_VCHR.EnableEscapeKeyUndo = true;
            //this.txtSTAND_PST_VCHR.EnableLastValidValue = true;
            //this.txtSTAND_PST_VCHR.ErrorProvider = null;
            //this.txtSTAND_PST_VCHR.ErrorProviderMessage = "Invalid value";
            //this.txtSTAND_PST_VCHR.ForceFormatText = true;
            this.txtSTAND_PST_VCHR.Location = new System.Drawing.Point(520, 24);
            this.txtSTAND_PST_VCHR.MaxLength = 7;
            this.txtSTAND_PST_VCHR.Name = "txtSTAND_PST_VCHR";
            this.txtSTAND_PST_VCHR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSTAND_PST_VCHR.Size = new System.Drawing.Size(96, 23);
            this.txtSTAND_PST_VCHR.TabIndex = 5;
            this.txtSTAND_PST_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSTAND_PST_VCHR_KeyDown);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(464, 26);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(42, 14);
            this.label26.TabIndex = 4;
            this.label26.Text = "立位:";
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(8, 16);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(80, 16);
            this.label29.TabIndex = 1004;
            this.label29.Text = "临床诊断：";
            // 
            // txtBED_NO_CHR
            // 
            //this.txtBED_NO_CHR.EnableAutoValidation = false;
            //this.txtBED_NO_CHR.EnableEnterKeyValidate = true;
            //this.txtBED_NO_CHR.EnableEscapeKeyUndo = true;
            //this.txtBED_NO_CHR.EnableLastValidValue = true;
            //this.txtBED_NO_CHR.ErrorProvider = null;
            //this.txtBED_NO_CHR.ErrorProviderMessage = "Invalid value";
            //this.txtBED_NO_CHR.ForceFormatText = true;
            this.txtBED_NO_CHR.Location = new System.Drawing.Point(96, 16);
            this.txtBED_NO_CHR.MaxLength = 100;
            this.txtBED_NO_CHR.Name = "txtBED_NO_CHR";
            this.txtBED_NO_CHR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtBED_NO_CHR.Size = new System.Drawing.Size(504, 23);
            this.txtBED_NO_CHR.TabIndex = 0;
            this.txtBED_NO_CHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBED_NO_CHR_KeyDown);
            // 
            // textBoxTyped1
            // 
            //this.textBoxTyped1.EnableAutoValidation = false;
            //this.textBoxTyped1.EnableEnterKeyValidate = true;
            //this.textBoxTyped1.EnableEscapeKeyUndo = true;
            //this.textBoxTyped1.EnableLastValidValue = true;
            //this.textBoxTyped1.ErrorProvider = null;
            //this.textBoxTyped1.ErrorProviderMessage = "Invalid value";
            //this.textBoxTyped1.ForceFormatText = true;
            this.textBoxTyped1.Location = new System.Drawing.Point(96, 152);
            this.textBoxTyped1.MaxLength = 20;
            this.textBoxTyped1.Name = "textBoxTyped1";
            this.textBoxTyped1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxTyped1.Size = new System.Drawing.Size(520, 23);
            this.textBoxTyped1.TabIndex = 1;
            this.textBoxTyped1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTyped1_KeyDown);
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(8, 160);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(80, 16);
            this.label30.TabIndex = 1006;
            this.label30.Text = "运动前血压：";
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(624, 160);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(64, 16);
            this.label31.TabIndex = 1008;
            this.label31.Text = "毫米汞柱";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cboSTOP_REASON_VCHR);
            this.groupBox5.Controls.Add(this.txtHR_MAX_WORK_VCHR);
            this.groupBox5.Controls.Add(this.txtHR_PER_VCHR);
            this.groupBox5.Controls.Add(this.label36);
            this.groupBox5.Controls.Add(this.txtACTIVE_TOTAL_TIME_VCHR2);
            this.groupBox5.Controls.Add(this.textBoxTyped2);
            this.groupBox5.Controls.Add(this.txtACTIVE_LOAD_LEVEL_VCHR);
            this.groupBox5.Controls.Add(this.cboTEST_PLAN_VCHR);
            this.groupBox5.Controls.Add(this.rdbFORECAST_QTY_INT2);
            this.groupBox5.Controls.Add(this.rdbFORECAST_QTY_INT1);
            this.groupBox5.Controls.Add(this.label32);
            this.groupBox5.Controls.Add(this.txtACTIVE_LOAD_MPH_VCHR);
            this.groupBox5.Controls.Add(this.label33);
            this.groupBox5.Controls.Add(this.label34);
            this.groupBox5.Controls.Add(this.label37);
            this.groupBox5.Controls.Add(this.txtFORECAST_QTY_VCHR);
            this.groupBox5.Controls.Add(this.label40);
            this.groupBox5.Controls.Add(this.txtACTIVE_TOTAL_TIME_VCHR1);
            this.groupBox5.Controls.Add(this.label42);
            this.groupBox5.Controls.Add(this.textBoxTyped6);
            this.groupBox5.Controls.Add(this.label43);
            this.groupBox5.Controls.Add(this.label41);
            this.groupBox5.Controls.Add(this.label35);
            this.groupBox5.Controls.Add(this.label39);
            this.groupBox5.Location = new System.Drawing.Point(8, 184);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(688, 128);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "运动平板运动试验";
            // 
            // cboSTOP_REASON_VCHR
            // 
            this.cboSTOP_REASON_VCHR.Items.AddRange(new object[] {
            "达到预测心率",
            "心绞痛",
            "心律失常",
            "ST段压低>=3mm",
            "心压下降",
            "血压超过200’110mmhg",
            "疲劳",
            "其它"});
            this.cboSTOP_REASON_VCHR.Location = new System.Drawing.Point(488, 96);
            this.cboSTOP_REASON_VCHR.Name = "cboSTOP_REASON_VCHR";
            this.cboSTOP_REASON_VCHR.Size = new System.Drawing.Size(192, 22);
            this.cboSTOP_REASON_VCHR.TabIndex = 19;
            this.cboSTOP_REASON_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboSTOP_REASON_VCHR_KeyDown);
            // 
            // txtHR_MAX_WORK_VCHR
            // 
            //this.txtHR_MAX_WORK_VCHR.EnableAutoValidation = false;
            //this.txtHR_MAX_WORK_VCHR.EnableEnterKeyValidate = true;
            //this.txtHR_MAX_WORK_VCHR.EnableEscapeKeyUndo = true;
            //this.txtHR_MAX_WORK_VCHR.EnableLastValidValue = true;
            //this.txtHR_MAX_WORK_VCHR.ErrorProvider = null;
            //this.txtHR_MAX_WORK_VCHR.ErrorProviderMessage = "Invalid value";
            //this.txtHR_MAX_WORK_VCHR.ForceFormatText = true;
            this.txtHR_MAX_WORK_VCHR.Location = new System.Drawing.Point(264, 96);
            this.txtHR_MAX_WORK_VCHR.MaxLength = 7;
            this.txtHR_MAX_WORK_VCHR.Name = "txtHR_MAX_WORK_VCHR";
            this.txtHR_MAX_WORK_VCHR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtHR_MAX_WORK_VCHR.Size = new System.Drawing.Size(48, 23);
            this.txtHR_MAX_WORK_VCHR.TabIndex = 17;
            this.txtHR_MAX_WORK_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHR_MAX_WORK_VCHR_KeyDown);
            // 
            // txtHR_PER_VCHR
            // 
            //this.txtHR_PER_VCHR.EnableAutoValidation = false;
            //this.txtHR_PER_VCHR.EnableEnterKeyValidate = true;
            //this.txtHR_PER_VCHR.EnableEscapeKeyUndo = true;
            //this.txtHR_PER_VCHR.EnableLastValidValue = true;
            //this.txtHR_PER_VCHR.ErrorProvider = null;
            //this.txtHR_PER_VCHR.ErrorProviderMessage = "Invalid value";
            //this.txtHR_PER_VCHR.ForceFormatText = true;
            this.txtHR_PER_VCHR.Location = new System.Drawing.Point(112, 96);
            this.txtHR_PER_VCHR.MaxLength = 7;
            this.txtHR_PER_VCHR.Name = "txtHR_PER_VCHR";
            this.txtHR_PER_VCHR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtHR_PER_VCHR.Size = new System.Drawing.Size(48, 23);
            this.txtHR_PER_VCHR.TabIndex = 15;
            this.txtHR_PER_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHR_PER_VCHR_KeyDown);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(624, 56);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(56, 14);
            this.label36.TabIndex = 14;
            this.label36.Text = "次/分，";
            // 
            // txtACTIVE_TOTAL_TIME_VCHR2
            // 
            //this.txtACTIVE_TOTAL_TIME_VCHR2.EnableAutoValidation = false;
            //this.txtACTIVE_TOTAL_TIME_VCHR2.EnableEnterKeyValidate = false;
            //this.txtACTIVE_TOTAL_TIME_VCHR2.EnableEscapeKeyUndo = true;
            //this.txtACTIVE_TOTAL_TIME_VCHR2.EnableLastValidValue = true;
            //this.txtACTIVE_TOTAL_TIME_VCHR2.ErrorProvider = null;
            //this.txtACTIVE_TOTAL_TIME_VCHR2.ErrorProviderMessage = "输入非法字符";
            //this.txtACTIVE_TOTAL_TIME_VCHR2.ForceFormatText = true;
            this.txtACTIVE_TOTAL_TIME_VCHR2.Location = new System.Drawing.Point(376, 54);
            this.txtACTIVE_TOTAL_TIME_VCHR2.MaxLength = 7;
            this.txtACTIVE_TOTAL_TIME_VCHR2.Name = "txtACTIVE_TOTAL_TIME_VCHR2";
            this.txtACTIVE_TOTAL_TIME_VCHR2.Size = new System.Drawing.Size(48, 23);
            this.txtACTIVE_TOTAL_TIME_VCHR2.TabIndex = 12;
            this.txtACTIVE_TOTAL_TIME_VCHR2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtACTIVE_TOTAL_TIME_VCHR2_KeyDown);
            // 
            // textBoxTyped2
            // 
            //this.textBoxTyped2.EnableAutoValidation = false;
            //this.textBoxTyped2.EnableEnterKeyValidate = true;
            //this.textBoxTyped2.EnableEscapeKeyUndo = true;
            //this.textBoxTyped2.EnableLastValidValue = true;
            //this.textBoxTyped2.ErrorProvider = null;
            //this.textBoxTyped2.ErrorProviderMessage = "Invalid value";
            //this.textBoxTyped2.ForceFormatText = true;
            this.textBoxTyped2.Location = new System.Drawing.Point(128, 54);
            this.textBoxTyped2.MaxLength = 7;
            this.textBoxTyped2.Name = "textBoxTyped2";
            this.textBoxTyped2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxTyped2.Size = new System.Drawing.Size(48, 23);
            this.textBoxTyped2.TabIndex = 9;
            this.textBoxTyped2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTyped2_KeyDown);
            // 
            // txtACTIVE_LOAD_LEVEL_VCHR
            // 
            //this.txtACTIVE_LOAD_LEVEL_VCHR.EnableAutoValidation = false;
            //this.txtACTIVE_LOAD_LEVEL_VCHR.EnableEnterKeyValidate = false;
            //this.txtACTIVE_LOAD_LEVEL_VCHR.EnableEscapeKeyUndo = true;
            //this.txtACTIVE_LOAD_LEVEL_VCHR.EnableLastValidValue = false;
            //this.txtACTIVE_LOAD_LEVEL_VCHR.ErrorProvider = null;
            //this.txtACTIVE_LOAD_LEVEL_VCHR.ErrorProviderMessage = "输入非法字符";
            //this.txtACTIVE_LOAD_LEVEL_VCHR.ForceFormatText = true;
            this.txtACTIVE_LOAD_LEVEL_VCHR.Location = new System.Drawing.Point(632, 24);
            this.txtACTIVE_LOAD_LEVEL_VCHR.MaxLength = 7;
            this.txtACTIVE_LOAD_LEVEL_VCHR.Name = "txtACTIVE_LOAD_LEVEL_VCHR";
            this.txtACTIVE_LOAD_LEVEL_VCHR.Size = new System.Drawing.Size(48, 23);
            this.txtACTIVE_LOAD_LEVEL_VCHR.TabIndex = 6;
            this.txtACTIVE_LOAD_LEVEL_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtACTIVE_LOAD_LEVEL_VCHR_KeyDown);
            // 
            // cboTEST_PLAN_VCHR
            // 
            this.cboTEST_PLAN_VCHR.Items.AddRange(new object[] {
            "Bruce",
            "Mod-Bruce",
            "Naughton"});
            this.cboTEST_PLAN_VCHR.Location = new System.Drawing.Point(376, 24);
            this.cboTEST_PLAN_VCHR.MaxLength = 7;
            this.cboTEST_PLAN_VCHR.Name = "cboTEST_PLAN_VCHR";
            this.cboTEST_PLAN_VCHR.Size = new System.Drawing.Size(128, 22);
            this.cboTEST_PLAN_VCHR.TabIndex = 4;
            this.cboTEST_PLAN_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboTEST_PLAN_VCHR_KeyDown);
            // 
            // rdbFORECAST_QTY_INT2
            // 
            this.rdbFORECAST_QTY_INT2.Location = new System.Drawing.Point(112, 23);
            this.rdbFORECAST_QTY_INT2.Name = "rdbFORECAST_QTY_INT2";
            this.rdbFORECAST_QTY_INT2.Size = new System.Drawing.Size(96, 24);
            this.rdbFORECAST_QTY_INT2.TabIndex = 1;
            this.rdbFORECAST_QTY_INT2.Text = "亚极量心率";
            this.rdbFORECAST_QTY_INT2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            this.rdbFORECAST_QTY_INT2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbFORECAST_QTY_INT2_KeyDown);
            // 
            // rdbFORECAST_QTY_INT1
            // 
            this.rdbFORECAST_QTY_INT1.Checked = true;
            this.rdbFORECAST_QTY_INT1.Location = new System.Drawing.Point(16, 23);
            this.rdbFORECAST_QTY_INT1.Name = "rdbFORECAST_QTY_INT1";
            this.rdbFORECAST_QTY_INT1.Size = new System.Drawing.Size(88, 24);
            this.rdbFORECAST_QTY_INT1.TabIndex = 0;
            this.rdbFORECAST_QTY_INT1.TabStop = true;
            this.rdbFORECAST_QTY_INT1.Text = "极量心率";
            this.rdbFORECAST_QTY_INT1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbFORECAST_QTY_INT1_KeyDown);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(88, 56);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(35, 14);
            this.label32.TabIndex = 8;
            this.label32.Text = "MPH/";
            // 
            // txtACTIVE_LOAD_MPH_VCHR
            // 
            //this.txtACTIVE_LOAD_MPH_VCHR.EnableAutoValidation = false;
            //this.txtACTIVE_LOAD_MPH_VCHR.EnableEnterKeyValidate = true;
            //this.txtACTIVE_LOAD_MPH_VCHR.EnableEscapeKeyUndo = true;
            //this.txtACTIVE_LOAD_MPH_VCHR.EnableLastValidValue = true;
            //this.txtACTIVE_LOAD_MPH_VCHR.ErrorProvider = null;
            //this.txtACTIVE_LOAD_MPH_VCHR.ErrorProviderMessage = "Invalid value";
            //this.txtACTIVE_LOAD_MPH_VCHR.ForceFormatText = true;
            this.txtACTIVE_LOAD_MPH_VCHR.Location = new System.Drawing.Point(40, 54);
            this.txtACTIVE_LOAD_MPH_VCHR.MaxLength = 7;
            this.txtACTIVE_LOAD_MPH_VCHR.Name = "txtACTIVE_LOAD_MPH_VCHR";
            this.txtACTIVE_LOAD_MPH_VCHR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtACTIVE_LOAD_MPH_VCHR.Size = new System.Drawing.Size(48, 23);
            this.txtACTIVE_LOAD_MPH_VCHR.TabIndex = 8;
            this.txtACTIVE_LOAD_MPH_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtACTIVE_LOAD_MPH_VCHR_KeyDown);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(8, 56);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(35, 14);
            this.label33.TabIndex = 7;
            this.label33.Text = "级（";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(504, 26);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(133, 14);
            this.label34.TabIndex = 5;
            this.label34.Text = "方案，运动负荷达第";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(304, 26);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(70, 14);
            this.label37.TabIndex = 3;
            this.label37.Text = "次/分。按";
            // 
            // txtFORECAST_QTY_VCHR
            // 
            this.txtFORECAST_QTY_VCHR.AccessibleDescription = "P-R间期";
            this.txtFORECAST_QTY_VCHR.BackColor = System.Drawing.Color.White;
            this.txtFORECAST_QTY_VCHR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFORECAST_QTY_VCHR.ForeColor = System.Drawing.Color.Black;
            this.txtFORECAST_QTY_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtFORECAST_QTY_VCHR.Location = new System.Drawing.Point(216, 23);
            this.txtFORECAST_QTY_VCHR.m_BlnIgnoreUserInfo = true;
            this.txtFORECAST_QTY_VCHR.m_BlnPartControl = false;
            this.txtFORECAST_QTY_VCHR.m_BlnReadOnly = false;
            this.txtFORECAST_QTY_VCHR.m_BlnUnderLineDST = false;
            this.txtFORECAST_QTY_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.txtFORECAST_QTY_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.txtFORECAST_QTY_VCHR.m_IntCanModifyTime = 6;
            this.txtFORECAST_QTY_VCHR.m_IntPartControlLength = 0;
            this.txtFORECAST_QTY_VCHR.m_IntPartControlStartIndex = 0;
            this.txtFORECAST_QTY_VCHR.m_StrUserID = "";
            this.txtFORECAST_QTY_VCHR.m_StrUserName = "";
            this.txtFORECAST_QTY_VCHR.MaxLength = 7;
            this.txtFORECAST_QTY_VCHR.Multiline = false;
            this.txtFORECAST_QTY_VCHR.Name = "txtFORECAST_QTY_VCHR";
            this.txtFORECAST_QTY_VCHR.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtFORECAST_QTY_VCHR.Size = new System.Drawing.Size(72, 24);
            this.txtFORECAST_QTY_VCHR.TabIndex = 2;
            this.txtFORECAST_QTY_VCHR.Text = "";
            this.txtFORECAST_QTY_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFORECAST_QTY_VCHR_KeyDown);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(192, 56);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(98, 14);
            this.label40.TabIndex = 10;
            this.label40.Text = "%),运动总时间";
            // 
            // txtACTIVE_TOTAL_TIME_VCHR1
            // 
            //this.txtACTIVE_TOTAL_TIME_VCHR1.EnableAutoValidation = false;
            //this.txtACTIVE_TOTAL_TIME_VCHR1.EnableEnterKeyValidate = false;
            //this.txtACTIVE_TOTAL_TIME_VCHR1.EnableEscapeKeyUndo = true;
            //this.txtACTIVE_TOTAL_TIME_VCHR1.EnableLastValidValue = true;
            //this.txtACTIVE_TOTAL_TIME_VCHR1.ErrorProvider = null;
            //this.txtACTIVE_TOTAL_TIME_VCHR1.ErrorProviderMessage = "输入非法字符";
            //this.txtACTIVE_TOTAL_TIME_VCHR1.ForceFormatText = true;
            this.txtACTIVE_TOTAL_TIME_VCHR1.Location = new System.Drawing.Point(296, 54);
            this.txtACTIVE_TOTAL_TIME_VCHR1.MaxLength = 7;
            this.txtACTIVE_TOTAL_TIME_VCHR1.Name = "txtACTIVE_TOTAL_TIME_VCHR1";
            this.txtACTIVE_TOTAL_TIME_VCHR1.Size = new System.Drawing.Size(48, 23);
            this.txtACTIVE_TOTAL_TIME_VCHR1.TabIndex = 11;
            this.txtACTIVE_TOTAL_TIME_VCHR1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtACTIVE_TOTAL_TIME_VCHR1_KeyDown);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(352, 56);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(21, 14);
            this.label42.TabIndex = 9;
            this.label42.Text = "分";
            // 
            // textBoxTyped6
            // 
            //this.textBoxTyped6.EnableAutoValidation = false;
            //this.textBoxTyped6.EnableEnterKeyValidate = false;
            //this.textBoxTyped6.EnableEscapeKeyUndo = true;
            //this.textBoxTyped6.EnableLastValidValue = false;
            //this.textBoxTyped6.ErrorProvider = null;
            //this.textBoxTyped6.ErrorProviderMessage = "输入非法字符";
            //this.textBoxTyped6.ForceFormatText = true;
            this.textBoxTyped6.Location = new System.Drawing.Point(528, 54);
            this.textBoxTyped6.MaxLength = 7;
            this.textBoxTyped6.Name = "textBoxTyped6";
            this.textBoxTyped6.Size = new System.Drawing.Size(80, 23);
            this.textBoxTyped6.TabIndex = 13;
            this.textBoxTyped6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTyped6_KeyDown);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(432, 56);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(91, 14);
            this.label43.TabIndex = 11;
            this.label43.Text = "秒，最高心率";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(16, 98);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(91, 14);
            this.label41.TabIndex = 14;
            this.label41.Text = "为预测心率的";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(176, 98);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(84, 14);
            this.label35.TabIndex = 16;
            this.label35.Text = "%，max work";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(328, 98);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(161, 14);
            this.label39.TabIndex = 18;
            this.label39.Text = "METS 。 运动终止原因：";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtHR_WRONG_VCHR);
            this.groupBox6.Controls.Add(this.label49);
            this.groupBox6.Controls.Add(this.txtHR_time_VCHR);
            this.groupBox6.Controls.Add(this.label47);
            this.groupBox6.Controls.Add(this.txtHR_daolink_VCHR);
            this.groupBox6.Controls.Add(this.label44);
            this.groupBox6.Controls.Add(this.txtACTIVE_ST_VCHR);
            this.groupBox6.Controls.Add(this.ACTIVE_ST_MAX_INT2);
            this.groupBox6.Controls.Add(this.ACTIVE_ST_MAX_INT1);
            this.groupBox6.Controls.Add(this.COMACTIVE_ST_MODE_VCHR);
            this.groupBox6.Controls.Add(this.label38);
            this.groupBox6.Controls.Add(this.txtAPPEAR_LED_VCHR);
            this.groupBox6.Controls.Add(this.cboACTIVE_ST_VCHR);
            this.groupBox6.Controls.Add(this.txtHR_SCOPE_VCHR);
            this.groupBox6.Controls.Add(this.label45);
            this.groupBox6.Controls.Add(this.label46);
            this.groupBox6.Controls.Add(this.label48);
            this.groupBox6.Controls.Add(this.txtTIME_SCOPE_VCHR);
            this.groupBox6.Controls.Add(this.label50);
            this.groupBox6.Location = new System.Drawing.Point(8, 312);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(688, 232);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "运动后心电图";
            // 
            // txtHR_WRONG_VCHR
            // 
            this.txtHR_WRONG_VCHR.AccessibleDescription = "心律失常";
            this.txtHR_WRONG_VCHR.BackColor = System.Drawing.Color.White;
            this.txtHR_WRONG_VCHR.ContextMenu = this.mnuRichTextBox;
            this.txtHR_WRONG_VCHR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHR_WRONG_VCHR.ForeColor = System.Drawing.Color.Black;
            this.txtHR_WRONG_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtHR_WRONG_VCHR.Location = new System.Drawing.Point(16, 192);
            this.txtHR_WRONG_VCHR.m_BlnIgnoreUserInfo = true;
            this.txtHR_WRONG_VCHR.m_BlnPartControl = false;
            this.txtHR_WRONG_VCHR.m_BlnReadOnly = false;
            this.txtHR_WRONG_VCHR.m_BlnUnderLineDST = false;
            this.txtHR_WRONG_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.txtHR_WRONG_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.txtHR_WRONG_VCHR.m_IntCanModifyTime = 6;
            this.txtHR_WRONG_VCHR.m_IntPartControlLength = 0;
            this.txtHR_WRONG_VCHR.m_IntPartControlStartIndex = 0;
            this.txtHR_WRONG_VCHR.m_StrUserID = "";
            this.txtHR_WRONG_VCHR.m_StrUserName = "";
            this.txtHR_WRONG_VCHR.MaxLength = 500;
            this.txtHR_WRONG_VCHR.Name = "txtHR_WRONG_VCHR";
            this.txtHR_WRONG_VCHR.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtHR_WRONG_VCHR.Size = new System.Drawing.Size(664, 32);
            this.txtHR_WRONG_VCHR.TabIndex = 10;
            this.txtHR_WRONG_VCHR.Text = "";
            // 
            // mnuRichTextBox
            // 
            this.mnuRichTextBox.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuRichTextBoxDelete,
            this.meuUndoDel,
            this.menuItem1,
            this.menuItem2,
            this.menuItem3});
            // 
            // mnuRichTextBoxDelete
            // 
            this.mnuRichTextBoxDelete.Index = 0;
            this.mnuRichTextBoxDelete.Text = "删除(&D)";
            this.mnuRichTextBoxDelete.Click += new System.EventHandler(this.mnuRichTextBoxDelete_Click);
            // 
            // meuUndoDel
            // 
            this.meuUndoDel.Index = 1;
            this.meuUndoDel.Text = "撤消删除";
            this.meuUndoDel.Click += new System.EventHandler(this.meuUndoDel_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "上标(&U)";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 3;
            this.menuItem2.Text = "下标(&W)";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 4;
            this.menuItem3.Text = "撤消上下标(&G)";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(8, 176);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(77, 14);
            this.label49.TabIndex = 209;
            this.label49.Text = "心律失常：";
            // 
            // txtHR_time_VCHR
            // 
            this.txtHR_time_VCHR.AccessibleDescription = "出现时间";
            this.txtHR_time_VCHR.ContextMenu = this.mnuRichTextBox;
            this.txtHR_time_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtHR_time_VCHR.Location = new System.Drawing.Point(512, 112);
            this.txtHR_time_VCHR.m_BlnIgnoreUserInfo = true;
            this.txtHR_time_VCHR.m_BlnPartControl = false;
            this.txtHR_time_VCHR.m_BlnReadOnly = false;
            this.txtHR_time_VCHR.m_BlnUnderLineDST = false;
            this.txtHR_time_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.txtHR_time_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.txtHR_time_VCHR.m_IntCanModifyTime = 6;
            this.txtHR_time_VCHR.m_IntPartControlLength = 0;
            this.txtHR_time_VCHR.m_IntPartControlStartIndex = 0;
            this.txtHR_time_VCHR.m_StrUserID = "";
            this.txtHR_time_VCHR.m_StrUserName = "";
            this.txtHR_time_VCHR.MaxLength = 100;
            this.txtHR_time_VCHR.Name = "txtHR_time_VCHR";
            this.txtHR_time_VCHR.Size = new System.Drawing.Size(168, 23);
            this.txtHR_time_VCHR.TabIndex = 6;
            this.txtHR_time_VCHR.Text = "";
            this.txtHR_time_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTyped4_KeyDown);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(440, 114);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(63, 14);
            this.label47.TabIndex = 207;
            this.label47.Text = "出现时间";
            // 
            // txtHR_daolink_VCHR
            // 
            this.txtHR_daolink_VCHR.AccessibleDescription = "出现导联";
            this.txtHR_daolink_VCHR.ContextMenu = this.mnuRichTextBox;
            this.txtHR_daolink_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtHR_daolink_VCHR.Location = new System.Drawing.Point(288, 112);
            this.txtHR_daolink_VCHR.m_BlnIgnoreUserInfo = true;
            this.txtHR_daolink_VCHR.m_BlnPartControl = false;
            this.txtHR_daolink_VCHR.m_BlnReadOnly = false;
            this.txtHR_daolink_VCHR.m_BlnUnderLineDST = false;
            this.txtHR_daolink_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.txtHR_daolink_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.txtHR_daolink_VCHR.m_IntCanModifyTime = 6;
            this.txtHR_daolink_VCHR.m_IntPartControlLength = 0;
            this.txtHR_daolink_VCHR.m_IntPartControlStartIndex = 0;
            this.txtHR_daolink_VCHR.m_StrUserID = "";
            this.txtHR_daolink_VCHR.m_StrUserName = "";
            this.txtHR_daolink_VCHR.MaxLength = 100;
            this.txtHR_daolink_VCHR.Name = "txtHR_daolink_VCHR";
            this.txtHR_daolink_VCHR.Size = new System.Drawing.Size(144, 23);
            this.txtHR_daolink_VCHR.TabIndex = 5;
            this.txtHR_daolink_VCHR.Text = "";
            this.txtHR_daolink_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTyped3_KeyDown);
            this.txtHR_daolink_VCHR.TextChanged += new System.EventHandler(this.txtHR_daolink_VCHR_TextChanged);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(224, 114);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(63, 14);
            this.label44.TabIndex = 205;
            this.label44.Text = "出现导联";
            // 
            // txtACTIVE_ST_VCHR
            // 
            //this.txtACTIVE_ST_VCHR.EnableAutoValidation = false;
            //this.txtACTIVE_ST_VCHR.EnableEnterKeyValidate = true;
            //this.txtACTIVE_ST_VCHR.EnableEscapeKeyUndo = true;
            //this.txtACTIVE_ST_VCHR.EnableLastValidValue = true;
            //this.txtACTIVE_ST_VCHR.ErrorProvider = null;
            //this.txtACTIVE_ST_VCHR.ErrorProviderMessage = "Invalid value";
            //this.txtACTIVE_ST_VCHR.ForceFormatText = true;
            this.txtACTIVE_ST_VCHR.Location = new System.Drawing.Point(168, 144);
            this.txtACTIVE_ST_VCHR.MaxLength = 100;
            this.txtACTIVE_ST_VCHR.Name = "txtACTIVE_ST_VCHR";
            this.txtACTIVE_ST_VCHR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtACTIVE_ST_VCHR.Size = new System.Drawing.Size(512, 23);
            this.txtACTIVE_ST_VCHR.TabIndex = 9;
            this.txtACTIVE_ST_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtACTIVE_ST_VCHR_KeyDown);
            // 
            // ACTIVE_ST_MAX_INT2
            // 
            this.ACTIVE_ST_MAX_INT2.Location = new System.Drawing.Point(72, 144);
            this.ACTIVE_ST_MAX_INT2.Name = "ACTIVE_ST_MAX_INT2";
            this.ACTIVE_ST_MAX_INT2.Size = new System.Drawing.Size(96, 24);
            this.ACTIVE_ST_MAX_INT2.TabIndex = 8;
            this.ACTIVE_ST_MAX_INT2.Text = "抬高最大值";
            this.ACTIVE_ST_MAX_INT2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ACTIVE_ST_MAX_INT2_KeyDown);
            // 
            // ACTIVE_ST_MAX_INT1
            // 
            this.ACTIVE_ST_MAX_INT1.Checked = true;
            this.ACTIVE_ST_MAX_INT1.Location = new System.Drawing.Point(8, 144);
            this.ACTIVE_ST_MAX_INT1.Name = "ACTIVE_ST_MAX_INT1";
            this.ACTIVE_ST_MAX_INT1.Size = new System.Drawing.Size(144, 24);
            this.ACTIVE_ST_MAX_INT1.TabIndex = 7;
            this.ACTIVE_ST_MAX_INT1.TabStop = true;
            this.ACTIVE_ST_MAX_INT1.Text = "ST压低";
            this.ACTIVE_ST_MAX_INT1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ACTIVE_ST_MAX_INT1_KeyDown);
            // 
            // COMACTIVE_ST_MODE_VCHR
            // 
            this.COMACTIVE_ST_MODE_VCHR.Items.AddRange(new object[] {
            "无",
            "水平型",
            "近似水平型"});
            this.COMACTIVE_ST_MODE_VCHR.Location = new System.Drawing.Point(256, 22);
            this.COMACTIVE_ST_MODE_VCHR.Name = "COMACTIVE_ST_MODE_VCHR";
            this.COMACTIVE_ST_MODE_VCHR.Size = new System.Drawing.Size(184, 22);
            this.COMACTIVE_ST_MODE_VCHR.TabIndex = 1;
            this.COMACTIVE_ST_MODE_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.COMACTIVE_ST_MODE_VCHR_KeyDown);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(8, 56);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(63, 14);
            this.label38.TabIndex = 197;
            this.label38.Text = "起止时间";
            // 
            // txtAPPEAR_LED_VCHR
            // 
            this.txtAPPEAR_LED_VCHR.AccessibleDescription = "出现导联1";
            this.txtAPPEAR_LED_VCHR.ContextMenu = this.mnuRichTextBox;
            this.txtAPPEAR_LED_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtAPPEAR_LED_VCHR.Location = new System.Drawing.Point(512, 22);
            this.txtAPPEAR_LED_VCHR.m_BlnIgnoreUserInfo = true;
            this.txtAPPEAR_LED_VCHR.m_BlnPartControl = false;
            this.txtAPPEAR_LED_VCHR.m_BlnReadOnly = false;
            this.txtAPPEAR_LED_VCHR.m_BlnUnderLineDST = false;
            this.txtAPPEAR_LED_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.txtAPPEAR_LED_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.txtAPPEAR_LED_VCHR.m_IntCanModifyTime = 6;
            this.txtAPPEAR_LED_VCHR.m_IntPartControlLength = 0;
            this.txtAPPEAR_LED_VCHR.m_IntPartControlStartIndex = 0;
            this.txtAPPEAR_LED_VCHR.m_StrUserID = "";
            this.txtAPPEAR_LED_VCHR.m_StrUserName = "";
            this.txtAPPEAR_LED_VCHR.MaxLength = 200;
            this.txtAPPEAR_LED_VCHR.Name = "txtAPPEAR_LED_VCHR";
            this.txtAPPEAR_LED_VCHR.Size = new System.Drawing.Size(168, 23);
            this.txtAPPEAR_LED_VCHR.TabIndex = 2;
            this.txtAPPEAR_LED_VCHR.Text = "";
            this.txtAPPEAR_LED_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAPPEAR_LED_VCHR_KeyDown);
            // 
            // cboACTIVE_ST_VCHR
            // 
            this.cboACTIVE_ST_VCHR.Items.AddRange(new object[] {
            "无",
            "<0.5mm",
            "0.5mm",
            "0.6~0.9mm",
            ">=1.0mm",
            ""});
            this.cboACTIVE_ST_VCHR.Location = new System.Drawing.Point(112, 22);
            this.cboACTIVE_ST_VCHR.Name = "cboACTIVE_ST_VCHR";
            this.cboACTIVE_ST_VCHR.Size = new System.Drawing.Size(96, 22);
            this.cboACTIVE_ST_VCHR.TabIndex = 0;
            this.cboACTIVE_ST_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboACTIVE_ST_VCHR_KeyDown);
            // 
            // txtHR_SCOPE_VCHR
            // 
            this.txtHR_SCOPE_VCHR.AccessibleDescription = "心率范围";
            this.txtHR_SCOPE_VCHR.ContextMenu = this.mnuRichTextBox;
            this.txtHR_SCOPE_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtHR_SCOPE_VCHR.Location = new System.Drawing.Point(80, 112);
            this.txtHR_SCOPE_VCHR.m_BlnIgnoreUserInfo = true;
            this.txtHR_SCOPE_VCHR.m_BlnPartControl = false;
            this.txtHR_SCOPE_VCHR.m_BlnReadOnly = false;
            this.txtHR_SCOPE_VCHR.m_BlnUnderLineDST = false;
            this.txtHR_SCOPE_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.txtHR_SCOPE_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.txtHR_SCOPE_VCHR.m_IntCanModifyTime = 6;
            this.txtHR_SCOPE_VCHR.m_IntPartControlLength = 0;
            this.txtHR_SCOPE_VCHR.m_IntPartControlStartIndex = 0;
            this.txtHR_SCOPE_VCHR.m_StrUserID = "";
            this.txtHR_SCOPE_VCHR.m_StrUserName = "";
            this.txtHR_SCOPE_VCHR.MaxLength = 50;
            this.txtHR_SCOPE_VCHR.Name = "txtHR_SCOPE_VCHR";
            this.txtHR_SCOPE_VCHR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtHR_SCOPE_VCHR.Size = new System.Drawing.Size(128, 23);
            this.txtHR_SCOPE_VCHR.TabIndex = 4;
            this.txtHR_SCOPE_VCHR.Text = "";
            this.txtHR_SCOPE_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHR_SCOPE_VCHR_KeyDown);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(8, 114);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(63, 14);
            this.label45.TabIndex = 164;
            this.label45.Text = "心率范围";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(8, 24);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(112, 14);
            this.label46.TabIndex = 162;
            this.label46.Text = "ST段压低/抬高：";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(216, 24);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(35, 14);
            this.label48.TabIndex = 6;
            this.label48.Text = "形态";
            // 
            // txtTIME_SCOPE_VCHR
            // 
            this.txtTIME_SCOPE_VCHR.AccessibleDescription = "起止时间";
            this.txtTIME_SCOPE_VCHR.ContextMenu = this.mnuRichTextBox;
            this.txtTIME_SCOPE_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtTIME_SCOPE_VCHR.Location = new System.Drawing.Point(80, 56);
            this.txtTIME_SCOPE_VCHR.m_BlnIgnoreUserInfo = true;
            this.txtTIME_SCOPE_VCHR.m_BlnPartControl = false;
            this.txtTIME_SCOPE_VCHR.m_BlnReadOnly = false;
            this.txtTIME_SCOPE_VCHR.m_BlnUnderLineDST = false;
            this.txtTIME_SCOPE_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.txtTIME_SCOPE_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.txtTIME_SCOPE_VCHR.m_IntCanModifyTime = 6;
            this.txtTIME_SCOPE_VCHR.m_IntPartControlLength = 0;
            this.txtTIME_SCOPE_VCHR.m_IntPartControlStartIndex = 0;
            this.txtTIME_SCOPE_VCHR.m_StrUserID = "";
            this.txtTIME_SCOPE_VCHR.m_StrUserName = "";
            this.txtTIME_SCOPE_VCHR.MaxLength = 100;
            this.txtTIME_SCOPE_VCHR.Name = "txtTIME_SCOPE_VCHR";
            this.txtTIME_SCOPE_VCHR.Size = new System.Drawing.Size(600, 50);
            this.txtTIME_SCOPE_VCHR.TabIndex = 3;
            this.txtTIME_SCOPE_VCHR.Text = "";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(448, 24);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(63, 14);
            this.label50.TabIndex = 11;
            this.label50.Text = "出现导联";
            // 
            // label51
            // 
            this.label51.Location = new System.Drawing.Point(624, 552);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(64, 16);
            this.label51.TabIndex = 1014;
            this.label51.Text = "毫米汞柱";
            // 
            // txtACTIVED_BP_VCHR
            // 
            this.txtACTIVED_BP_VCHR.ContextMenu = this.mnuRichTextBox;
            this.txtACTIVED_BP_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtACTIVED_BP_VCHR.Location = new System.Drawing.Point(104, 552);
            this.txtACTIVED_BP_VCHR.m_BlnIgnoreUserInfo = true;
            this.txtACTIVED_BP_VCHR.m_BlnPartControl = false;
            this.txtACTIVED_BP_VCHR.m_BlnReadOnly = false;
            this.txtACTIVED_BP_VCHR.m_BlnUnderLineDST = false;
            this.txtACTIVED_BP_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.txtACTIVED_BP_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.txtACTIVED_BP_VCHR.m_IntCanModifyTime = 6;
            this.txtACTIVED_BP_VCHR.m_IntPartControlLength = 0;
            this.txtACTIVED_BP_VCHR.m_IntPartControlStartIndex = 0;
            this.txtACTIVED_BP_VCHR.m_StrUserID = "";
            this.txtACTIVED_BP_VCHR.m_StrUserName = "";
            this.txtACTIVED_BP_VCHR.MaxLength = 50;
            this.txtACTIVED_BP_VCHR.Name = "txtACTIVED_BP_VCHR";
            this.txtACTIVED_BP_VCHR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtACTIVED_BP_VCHR.Size = new System.Drawing.Size(512, 23);
            this.txtACTIVED_BP_VCHR.TabIndex = 5;
            this.txtACTIVED_BP_VCHR.Text = "";
            this.txtACTIVED_BP_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtACTIVED_BP_VCHR_KeyDown);
            // 
            // label52
            // 
            this.label52.Location = new System.Drawing.Point(16, 552);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(96, 16);
            this.label52.TabIndex = 1012;
            this.label52.Text = "运动后血压：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTEST_RESULT_VCHR);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtACTIVE_RESULT_VCHR);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(8, 576);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(688, 136);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结论";
            // 
            // txtTEST_RESULT_VCHR
            // 
            this.txtTEST_RESULT_VCHR.AccessibleDescription = "活动平板运动试验";
            this.txtTEST_RESULT_VCHR.BackColor = System.Drawing.Color.White;
            this.txtTEST_RESULT_VCHR.ContextMenu = this.mnuRichTextBox;
            this.txtTEST_RESULT_VCHR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTEST_RESULT_VCHR.ForeColor = System.Drawing.Color.Black;
            this.txtTEST_RESULT_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtTEST_RESULT_VCHR.Location = new System.Drawing.Point(6, 96);
            this.txtTEST_RESULT_VCHR.m_BlnIgnoreUserInfo = true;
            this.txtTEST_RESULT_VCHR.m_BlnPartControl = false;
            this.txtTEST_RESULT_VCHR.m_BlnReadOnly = false;
            this.txtTEST_RESULT_VCHR.m_BlnUnderLineDST = false;
            this.txtTEST_RESULT_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.txtTEST_RESULT_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.txtTEST_RESULT_VCHR.m_IntCanModifyTime = 6;
            this.txtTEST_RESULT_VCHR.m_IntPartControlLength = 0;
            this.txtTEST_RESULT_VCHR.m_IntPartControlStartIndex = 0;
            this.txtTEST_RESULT_VCHR.m_StrUserID = "";
            this.txtTEST_RESULT_VCHR.m_StrUserName = "";
            this.txtTEST_RESULT_VCHR.MaxLength = 500;
            this.txtTEST_RESULT_VCHR.Name = "txtTEST_RESULT_VCHR";
            this.txtTEST_RESULT_VCHR.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtTEST_RESULT_VCHR.Size = new System.Drawing.Size(674, 40);
            this.txtTEST_RESULT_VCHR.TabIndex = 2;
            this.txtTEST_RESULT_VCHR.Text = "";
            this.txtTEST_RESULT_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTEST_RESULT_VCHR_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(133, 14);
            this.label11.TabIndex = 211;
            this.label11.Text = "活动平板运动试验：";
            // 
            // txtACTIVE_RESULT_VCHR
            // 
            this.txtACTIVE_RESULT_VCHR.AccessibleDescription = "运动前";
            this.txtACTIVE_RESULT_VCHR.BackColor = System.Drawing.Color.White;
            this.txtACTIVE_RESULT_VCHR.ContextMenu = this.mnuRichTextBox;
            this.txtACTIVE_RESULT_VCHR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtACTIVE_RESULT_VCHR.ForeColor = System.Drawing.Color.Black;
            this.txtACTIVE_RESULT_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtACTIVE_RESULT_VCHR.Location = new System.Drawing.Point(6, 40);
            this.txtACTIVE_RESULT_VCHR.m_BlnIgnoreUserInfo = true;
            this.txtACTIVE_RESULT_VCHR.m_BlnPartControl = false;
            this.txtACTIVE_RESULT_VCHR.m_BlnReadOnly = false;
            this.txtACTIVE_RESULT_VCHR.m_BlnUnderLineDST = false;
            this.txtACTIVE_RESULT_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.txtACTIVE_RESULT_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.txtACTIVE_RESULT_VCHR.m_IntCanModifyTime = 6;
            this.txtACTIVE_RESULT_VCHR.m_IntPartControlLength = 0;
            this.txtACTIVE_RESULT_VCHR.m_IntPartControlStartIndex = 0;
            this.txtACTIVE_RESULT_VCHR.m_StrUserID = "";
            this.txtACTIVE_RESULT_VCHR.m_StrUserName = "";
            this.txtACTIVE_RESULT_VCHR.MaxLength = 500;
            this.txtACTIVE_RESULT_VCHR.Name = "txtACTIVE_RESULT_VCHR";
            this.txtACTIVE_RESULT_VCHR.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtACTIVE_RESULT_VCHR.Size = new System.Drawing.Size(674, 24);
            this.txtACTIVE_RESULT_VCHR.TabIndex = 1;
            this.txtACTIVE_RESULT_VCHR.Text = "";
            this.txtACTIVE_RESULT_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtACTIVE_RESULT_VCHR_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 14);
            this.label10.TabIndex = 209;
            this.label10.Text = "运动前:";
            // 
            // m_printDoc
            // 
            this.m_printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_printDoc_PrintPage);
            // 
            // m_printPrevDlg
            // 
            this.m_printPrevDlg.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.m_printPrevDlg.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.m_printPrevDlg.ClientSize = new System.Drawing.Size(400, 300);
            this.m_printPrevDlg.Enabled = true;
            this.m_printPrevDlg.Icon = ((System.Drawing.Icon)(resources.GetObject("m_printPrevDlg.Icon")));
            this.m_printPrevDlg.Name = "m_printPrevDlg";
            this.m_printPrevDlg.Visible = false;
            // 
            // m_cheIsNew
            // 
            this.m_cheIsNew.Location = new System.Drawing.Point(648, 16);
            this.m_cheIsNew.Name = "m_cheIsNew";
            this.m_cheIsNew.Size = new System.Drawing.Size(32, 24);
            this.m_cheIsNew.TabIndex = 1016;
            this.m_cheIsNew.Visible = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.m_cheIsNew);
            this.groupBox7.Controls.Add(this.label29);
            this.groupBox7.Controls.Add(this.txtBED_NO_CHR);
            this.groupBox7.Controls.Add(this.groupBox4);
            this.groupBox7.Controls.Add(this.label30);
            this.groupBox7.Controls.Add(this.label31);
            this.groupBox7.Controls.Add(this.textBoxTyped1);
            this.groupBox7.Controls.Add(this.groupBox5);
            this.groupBox7.Controls.Add(this.groupBox6);
            this.groupBox7.Controls.Add(this.label51);
            this.groupBox7.Controls.Add(this.txtACTIVED_BP_VCHR);
            this.groupBox7.Controls.Add(this.label52);
            this.groupBox7.Controls.Add(this.groupBox2);
            this.groupBox7.Location = new System.Drawing.Point(320, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(704, 720);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            // 
            // carID
            // 
            this.carID.Location = new System.Drawing.Point(76, 22);
            this.carID.MaxLength = 50;
            this.carID.Name = "carID";
            this.carID.PatientCard = "";
            this.carID.PatientFlag = 0;
            this.carID.Size = new System.Drawing.Size(184, 23);
            this.carID.TabIndex = 0;
            this.carID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.carID.YBCardText = "";
            this.carID.CardKeyDown += new com.digitalwave.controls.clsCardTextBox.TxtKeyDownHandle(this.carID1_CardKeyDown);
            // 
            // frmFlatAndSportReport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1024, 733);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFlatAndSportReport";
            this.Text = "活动平板运动试验报告单";
            this.Load += new System.EventHandler(this.frmFlatAndsSportReport_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.RIS.clsControllerFlatAndSportReport(this);
			objController.Set_GUI_Apperance(this);
		}
		private void m_mthInitRichTextBox()
		{

			com.digitalwave.controls.ctlRichTextBox.m_ClrDefaultViewText=Color.Black;
			com.digitalwave.controls.ctlRichTextBox[] rtbArr  =new com.digitalwave.controls.ctlRichTextBox[]{txtACTIVE_RESULT_VCHR ,txtTEST_RESULT_VCHR,txtHR_WRONG_VCHR};
			foreach(com.digitalwave.controls.ctlRichTextBox objRTB in rtbArr)
			{
				objRTB.m_StrUserID = "";
				objRTB.m_StrUserName = "";
	
				objRTB.m_BlnPartControl = true;
				//objRTB.m_BlnPartControl=false;

				objRTB.m_ClrOldPartInsertText = Color.Red;
				objRTB.m_BlnCanModifyLast=true;
				//objRTB.m_BlnCanModifyLast=true;

				//objRTB.MouseLeave += new System.EventHandler(this.m_mthHandleMouseLeaveControl);
				//objRTB.m_evtMouseEnterInsertText += new System.EventHandler(this.m_mthHandleMouseEnterInsertText);
				//objRTB.m_evtMouseEnterDeleteText += new System.EventHandler(this.m_mthHandleMouseEnterDeleteText);
				//objRTB.m_evtMouseLeaveInsertText += new System.EventHandler(this.m_txtExamineDesc_m_evtMouseLeaveText);

				objRTB.m_mthSetNewText("","<root/>");
			}

		}
		private void frmFlatAndsSportReport_Load(object sender, System.EventArgs e)
		{
			this.m_mthSetEnter2Tab(new System.Windows.Forms.Control[] {txtHR_WRONG_VCHR,txtACTIVE_RESULT_VCHR,txtTEST_RESULT_VCHR,txtHR_WRONG_VCHR,txtTIME_SCOPE_VCHR});
			m_txtREPORT_NO_CHR.Focus();
		}

		private void label9_Click(object sender, System.EventArgs e)
		{
		
		}
		public void m_mthSetParentApperance(com.digitalwave.iCare.gui.RIS.frmCardiogramReportManage infrmCardiogramReportManage)
		{
			((clsControllerFlatAndSportReport)this.objController).SetParentApperance(infrmCardiogramReportManage);
		}
		public void m_mthSetReport(clsafmt_report_VO p_objItem)
		{
			((clsControllerFlatAndSportReport)this.objController).m_mthSetReport(p_objItem);
		}

		private void radioButton2_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

		private void m_cmdClear_Click(object sender, System.EventArgs e)
		{
			((clsControllerFlatAndSportReport)this.objController).m_mthClear();
            this.m_cmdSave.Enabled = true;
		}

		private void m_txtREPORT_NO_CHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtPATIENT_NO_CHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtINPATIENT_NO_CHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
            //this.m_mthSetKeyTab(e);
            if (e.KeyCode == Keys.Enter)
            {
                ((clsControllerFlatAndSportReport)this.objController).m_lngGetPatByInPatientID();
            }
		}

		private void m_txtPATIENT_NAME_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_cboSEX_CHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtAGE_FLT_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_cmbAge_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtSubAGE_FLT_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtBED_NO_CHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_dtpCHECK_DAT_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_dtpREPORT_DAT_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtREPORTOR_NAME_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				txtBED_NO_CHR.Text="sdfs";
				txtBED_NO_CHR.Focus();
			}
		}

		private void txtBED_NO_CHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				m_txtRHYTHM_VCHR.Focus();
		}

		private void m_txtRHYTHM_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtLIE_PST_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtSTAND_PST_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtDEEP_BREATH_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtP_R_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtFORECAST_QTY_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void cboTEST_PLAN_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtACTIVE_LOAD_LEVEL_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtACTIVE_LOAD_MPH_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void textBoxTyped2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtACTIVE_TOTAL_TIME_VCHR1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtACTIVE_TOTAL_TIME_VCHR2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void textBoxTyped6_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtHR_PER_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtHR_MAX_WORK_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void cboSTOP_REASON_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				cboACTIVE_ST_VCHR.Focus();
		}

		private void cboACTIVE_ST_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void COMACTIVE_ST_MODE_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtAPPEAR_LED_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		this.m_mthSetKeyTab(e);
		}

		private void txtHR_SCOPE_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtTIME_SCOPE_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtACTIVE_ST_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void textBoxTyped3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void textBoxTyped4_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtACTIVED_BP_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				txtACTIVE_RESULT_VCHR.Focus();
		}

		private void txtACTIVE_RESULT_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtTEST_RESULT_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				m_cmdSave.Focus();
		}

		private void textBoxTyped1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				rdbFORECAST_QTY_INT1.Focus();
		}

		private void m_txtQRS_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		 this.m_mthSetKeyTab(e);
		}

		#region RichTextBox Event

		private void m_mthHandleMouseEnterDeleteText(object p_objSender,EventArgs p_objArg)
		{
			com.digitalwave.controls.clsDoubleStrikeThoughEventArg objArg = (com.digitalwave.controls.clsDoubleStrikeThoughEventArg)p_objArg;

			string strInfo = "用户姓名 : " +	objArg.m_strUserName+"\r\n删除时间 : ";

			if(objArg.m_dtmDeleteTime != DateTime.MinValue)
			{
				strInfo += objArg.m_dtmDeleteTime.ToString("yyyy年MM月dd日 HH:mm:ss");				
			}	
			else
			{
				strInfo += "----年--月--日 --:--:--";
			}
			
			m_ttpTextInfo.SetToolTip((Control)p_objSender,strInfo);
		}

		private void m_mthHandleMouseEnterInsertText(object p_objSender,EventArgs p_objArg)
		{
			com.digitalwave.controls.clsInsertEventArg objArg = (com.digitalwave.controls.clsInsertEventArg)p_objArg;
			
			if(objArg.m_intUserSeq == 1)
			{
				return;
			}
			
			string strInfo = "用户姓名 : "+	objArg.m_strUserName+"\r\n添加时间 : ";

			if( objArg.m_dtmInsertTime != DateTime.MinValue)
			{
				strInfo += objArg.m_dtmInsertTime.ToString("yyyy年MM月dd日 HH:mm:ss");				
			}	
			else
			{
				strInfo += "----年--月--日 --:--:--";
			}
			
			m_ttpTextInfo.SetToolTip((Control)p_objSender,strInfo);
		}
		
		private void m_mthHandleMouseLeaveControl(object p_objSender,EventArgs p_objArg)
		{
			m_ttpTextInfo.RemoveAll();
		}

		private void m_txtExamineDesc_m_evtMouseLeaveText(object sender, System.EventArgs e)
		{
		
		}

		private void mnuRichTextBoxDelete_Click(object sender, System.EventArgs e)
		{
			if(this.ActiveControl is com.digitalwave.controls.ctlRichTextBox)
			{
				((com.digitalwave.controls.ctlRichTextBox)(this.ActiveControl)).m_mthSelectionDoubleStrikeThough(true);
			}
		}
		#endregion

		private void m_txtQ_T_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				textBoxTyped1.Focus();
		}

		private void m_txtREPORTOR_NAME_VCHR_KeyDown_1(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				txtBED_NO_CHR.Focus();
		}

		private void rdbFORECAST_QTY_INT1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Space)
				txtFORECAST_QTY_VCHR.Focus();
		}

		private void ACTIVE_ST_MAX_INT1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Space)
				txtACTIVE_ST_VCHR.Focus();
		}

		private void m_cmdSave_Click(object sender, System.EventArgs e)
		{
			((clsControllerFlatAndSportReport)this.objController).m_mthDoSaveSport();
		}

		private void m_cmdDelete_Click(object sender, System.EventArgs e)
		{
			((clsControllerFlatAndSportReport)this.objController).m_lngDeleData();
		}

		private void m_cmdConfirm_Click(object sender, System.EventArgs e)
		{
			((clsControllerFlatAndSportReport)this.objController).m_lngEmp();
		}

		private void m_cmdCreateTemplate_Click(object sender, System.EventArgs e)
		{
            ((clsControllerFlatAndSportReport)this.objController).m_mthCreateTemplate();
            //((clsControllerFlatAndSportReport)this.objController).m_mthCreateTemplate(this);
		}

		private void m_cmdEditTemplate_Click(object sender, System.EventArgs e)
        {
            ((clsControllerFlatAndSportReport)this.objController).m_mthEditTemplate();
        //    System.Security.Principal.IPrincipal objPrincipal = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity("00000",""),null);
        //    clsEmployeeVO objEmployee= new clsEmployeeVO();
        //    objEmployee.strEmpID = "00000";
        //    objEmployee.strName = "测试";
        //    clsDepartmentVO objDept = new clsDepartmentVO();
        //    objDept.strDeptID = "0000000";
        //    objDept.strDeptName = "测试科";

        //    com.digitalwave.iCare.gui.TemplateUtility.frmTemplateSet frm = new com.digitalwave.iCare.gui.TemplateUtility.frmTemplateSet(objPrincipal,objDept,objEmployee);
        //    //			frm.MdiParent = this;
        //    frm.Show();
		}

		private void m_printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			((clsControllerFlatAndSportReport)this.objController).m_mthPrintDocPrintPage(e);
		}

		private void m_cmdPrint_Click(object sender, System.EventArgs e)
		{
			((clsControllerFlatAndSportReport)this.objController).m_mthPrintReport(this);
		}

		private void m_cmdExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		private void meuUndoDel_Click(object sender, System.EventArgs e)
		{
			if(this.ActiveControl is com.digitalwave.controls.ctlRichTextBox)
			{
				((com.digitalwave.controls.ctlRichTextBox)(this.ActiveControl)).m_mthSelectionDoubleStrikeThough(false);
			}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			if(this.ActiveControl is com.digitalwave.controls.ctlRichTextBox)
			{
				((com.digitalwave.controls.ctlRichTextBox)(this.ActiveControl)).m_mthSetSelectionScript(0);
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			if(this.ActiveControl is com.digitalwave.controls.ctlRichTextBox)
			{
				((com.digitalwave.controls.ctlRichTextBox)(this.ActiveControl)).m_mthSetSelectionScript(1);
			}
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			if(this.ActiveControl is com.digitalwave.controls.ctlRichTextBox)
			{
				((com.digitalwave.controls.ctlRichTextBox)(this.ActiveControl)).m_mthUndoSuperSubScript();
			}
		}

		private void m_cmbAge_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch( m_cmbAge.SelectedIndex)
			{
				case 0:
					this.m_labSubAge.Text="月";
					break;
				case 1:
					this.m_labSubAge.Text="天";
					break;
				case 2:
					this.m_labSubAge.Text="小时";
					break;
				case 3:
					this.m_labSubAge.Text="分";
					break;
			}
		}

		private void rdbFORECAST_QTY_INT2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Space)
				txtFORECAST_QTY_VCHR.Focus();
		}

		private void ACTIVE_ST_MAX_INT2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Space)
				txtACTIVE_ST_VCHR.Focus();
		}

		private void txtHR_daolink_VCHR_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void carID_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				if(carID.Text.Length<10&&carID.Text.Length>0)
				{
					string newCarId="0000000000"+carID.Text;
					carID.Text=newCarId.Substring(newCarId.Length-10);
				}
				((clsControllerFlatAndSportReport)this.objController).m_lngGetPat();
			}
		}

		private void m_txtDEPT_NAME_VCHR_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if((e.KeyChar=='?'))
				e.Handled=true;
		}

		private void m_txtDEPT_NAME_VCHR_evtValueChanged(object sender, com.digitalwave.Utility.clsExValueChangedEventArgs e)
		{
			m_txtREPORTOR_NAME_VCHR.Tag=m_txtDEPT_NAME_VCHR.Tag;
		}

		private void m_txtREPORTOR_NAME_VCHR_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if((e.KeyChar=='?'))
				e.Handled=true;
		}

		private void m_txtREPORTOR_NAME_VCHR_evtLostFocus(object sender, System.EventArgs e)
		{
			txtBED_NO_CHR.Focus();
		}

        private void carID1_CardKeyDown(object sender, EventArgs e)
        {
            ((clsControllerFlatAndSportReport)this.objController).m_lngGetPat();
        }

		
	}
}
