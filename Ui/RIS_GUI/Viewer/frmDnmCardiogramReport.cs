using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PinkieControls;
using weCare.Core.Entity;
using com.digitalwave.controls;

namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// frmDnmCardiogramReport 的摘要说明。
	/// </summary>
	public class frmDnmCardiogramReport : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
	{
		#region Define
		internal System.Windows.Forms.GroupBox groupBox1;
		internal System.Windows.Forms.Label label22;
		internal System.Windows.Forms.Label label21;
		internal System.Windows.Forms.Label label20;
		internal System.Windows.Forms.Label label19;
		internal System.Windows.Forms.Label label18;
		internal System.Windows.Forms.Label label17;
		internal System.Windows.Forms.Label label16;
		internal System.Windows.Forms.Label label15;
		internal System.Windows.Forms.Label label12;
		internal System.Windows.Forms.GroupBox groupBox2;
		internal System.Windows.Forms.Label label2;
		internal System.Windows.Forms.Label label6;
		internal System.Windows.Forms.Label label4;
		internal System.Windows.Forms.Label label3;
		internal System.Windows.Forms.GroupBox groupBox3;
		internal PinkieControls.ButtonXP m_cmdAddNew;
		internal PinkieControls.ButtonXP m_cmdExit;
		internal System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label23;
		internal System.Windows.Forms.Label label24;
		internal System.Windows.Forms.Label label25;
		internal System.Windows.Forms.Label label26;
		internal System.Windows.Forms.Label label28;
		internal System.Windows.Forms.Label label29;
		internal System.Windows.Forms.Label label30;
		internal System.Windows.Forms.Label label31;
		internal System.Windows.Forms.Label label5;
		internal System.Windows.Forms.GroupBox groupBox4;
		internal System.Windows.Forms.GroupBox groupBox5;
		internal System.Windows.Forms.GroupBox groupBox6;
		internal System.Windows.Forms.GroupBox groupBox7;
		internal System.Windows.Forms.GroupBox groupBox8;
		internal System.Windows.Forms.GroupBox groupBox9;
		internal System.Windows.Forms.GroupBox groupBox10;
		internal System.Windows.Forms.TextBox m_txtBED_NO_CHR;
		internal System.Windows.Forms.DateTimePicker m_dtpREPORT_DAT;
		internal System.Windows.Forms.DateTimePicker m_dtpCHECKFROM_DAT;
		internal System.Windows.Forms.DateTimePicker m_dtpCHECKTO_DAT;
		internal System.Windows.Forms.TextBox m_txtTotalHour;
		internal System.Windows.Forms.RadioButton m_rdbHEARTRATE_BASE_INT1;
		internal System.Windows.Forms.RadioButton m_rdbHEARTRATE_BASE_INT3;
		internal System.Windows.Forms.RadioButton m_rdbHEARTRATE_BASE_INT0;
		internal System.Windows.Forms.RadioButton m_rdbHEARTRATE_BASE_INT2;
		internal System.Windows.Forms.RadioButton m_rdbGRAPH_TYPE_INT1;
		internal System.Windows.Forms.RadioButton m_rdbGRAPH_TYPE_INT3;
		internal System.Windows.Forms.RadioButton m_rdbGRAPH_TYPE_INT0;
		internal System.Windows.Forms.RadioButton m_rdbGRAPH_TYPE_INT2;

		#endregion
		internal System.Windows.Forms.DateTimePicker m_dtpHEARTRATE_MIN_DAT;
		internal System.Windows.Forms.DateTimePicker m_dtpHEARTRATE_MAX_DAT;
		internal PinkieControls.ButtonXP m_cmdDelete;
		internal PinkieControls.ButtonXP m_cmdConfirm;
		internal PinkieControls.ButtonXP m_cmdPrint;
		internal System.Windows.Forms.ComboBox m_cboSEX_CHR;
		internal System.Windows.Forms.TextBox m_txtHEARTRATE_MIN_INT;
		internal System.Windows.Forms.TextBox m_txtHEARTRATE_MAX_INT;
		internal System.Windows.Forms.TextBox m_txtHEARTRATE_AVERAGE_INT;
		internal System.Windows.Forms.TextBox m_txtQRS_TOTAL_CHR;
		internal System.Windows.Forms.TextBox m_txtINPATIENT_NO_CHR;
		internal System.Windows.Forms.TextBox m_txtPATIENT_NO_CHR;
		internal System.Windows.Forms.TextBox m_txtPATIENT_NAME_VCHR;
		internal System.Drawing.Printing.PrintDocument m_printDoc;
		internal System.Windows.Forms.PrintPreviewDialog m_printPrevDlg;
		internal PinkieControls.ButtonXP m_cmdCreateTemplate;
		internal com.digitalwave.controls.ctlRichTextBox m_txtCLINICAL_DIAGNOSE_VCHR;
		internal com.digitalwave.controls.ctlRichTextBox m_txtCHECK_CHANNELS_VCHR;
		internal com.digitalwave.controls.ctlRichTextBox m_txtSUMMARY2_VCHR;
		internal com.digitalwave.controls.ctlRichTextBox m_txtSUMMARY1_VCHR;
		private System.Windows.Forms.ContextMenu m_mnuRichText;
		private System.Windows.Forms.MenuItem m_mnuRTDelete;
		private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolTip m_ttpTextInfo;
		private PinkieControls.ButtonXP buttonXP1;
		internal System.Windows.Forms.TextBox m_txtREPORT_NO_CHR;
		internal PinkieControls.ButtonXP m_cmdEditTemplate;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		internal System.Windows.Forms.CheckBox m_cheIsSpical;
		internal System.Windows.Forms.Label m_labSubAge;
		internal System.Windows.Forms.TextBox m_txtSubAGE_FLT;
		internal System.Windows.Forms.TextBox m_txtAGE_FLT;
		internal System.Windows.Forms.ComboBox m_cmbAge;
		private System.Windows.Forms.MenuItem meuUndoDel;
		internal System.Windows.Forms.ComboBox m_cmbHEARTRATE_BASE_INT;
        internal System.Windows.Forms.CheckBox m_cheIsNew;
		private System.Windows.Forms.Label label1;
        public ListViewBox m_txtDEPT_NAME_VCHR;
        public ListViewBox m_txtREPORTOR_NAME_VCHR;
        public string m_strApplyID;
        internal clsCardTextBox carID;
		private readonly DateTime m_dtmEmptyDate = new DateTime(1900,1,1);
       // public frmCardiogramReportManage m_objMainFormManage = null;

		public frmDnmCardiogramReport()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
            ((clsController_RISDnmCardiogramReport)this.objController).m_mthInitData();
            carID.Text = "";
            m_txtPATIENT_NO_CHR.Text = "";
            m_txtQRS_TOTAL_CHR.Text = "";
            m_txtHEARTRATE_MAX_INT.Text = "";
            m_txtHEARTRATE_MIN_INT.Text = "";
            m_txtSubAGE_FLT.Text = "";
            m_txtHEARTRATE_AVERAGE_INT.Text = "";

//			m_cboSEX_CHR.SelectedIndex=0;
			this.m_cboSEX_CHR.SelectedIndex=0;
			this.m_cmbAge.SelectedIndex=0;
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			m_mthInitRichTextBox();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDnmCardiogramReport));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_txtREPORTOR_NAME_VCHR = new ListViewBox();
            this.m_txtDEPT_NAME_VCHR = new ListViewBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_labSubAge = new System.Windows.Forms.Label();
            this.m_txtSubAGE_FLT = new System.Windows.Forms.TextBox();
            this.m_txtAGE_FLT = new System.Windows.Forms.TextBox();
            this.m_cmbAge = new System.Windows.Forms.ComboBox();
            this.m_txtREPORT_NO_CHR = new System.Windows.Forms.TextBox();
            this.m_txtPATIENT_NAME_VCHR = new System.Windows.Forms.TextBox();
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
            this.m_dtpREPORT_DAT = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_cheIsNew = new System.Windows.Forms.CheckBox();
            this.m_cheIsSpical = new System.Windows.Forms.CheckBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.m_dtpCHECKFROM_DAT = new System.Windows.Forms.DateTimePicker();
            this.m_dtpCHECKTO_DAT = new System.Windows.Forms.DateTimePicker();
            this.label23 = new System.Windows.Forms.Label();
            this.m_txtTotalHour = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.m_txtCHECK_CHANNELS_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.m_mnuRichText = new System.Windows.Forms.ContextMenu();
            this.m_mnuRTDelete = new System.Windows.Forms.MenuItem();
            this.meuUndoDel = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.m_cmbHEARTRATE_BASE_INT = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.m_rdbGRAPH_TYPE_INT1 = new System.Windows.Forms.RadioButton();
            this.m_rdbGRAPH_TYPE_INT3 = new System.Windows.Forms.RadioButton();
            this.m_rdbGRAPH_TYPE_INT0 = new System.Windows.Forms.RadioButton();
            this.m_rdbGRAPH_TYPE_INT2 = new System.Windows.Forms.RadioButton();
            this.m_dtpHEARTRATE_MIN_DAT = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.m_dtpHEARTRATE_MAX_DAT = new System.Windows.Forms.DateTimePicker();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtQRS_TOTAL_CHR = new System.Windows.Forms.TextBox();
            this.m_txtHEARTRATE_AVERAGE_INT = new System.Windows.Forms.TextBox();
            this.m_txtHEARTRATE_MAX_INT = new System.Windows.Forms.TextBox();
            this.m_txtHEARTRATE_MIN_INT = new System.Windows.Forms.TextBox();
            this.m_rdbHEARTRATE_BASE_INT1 = new System.Windows.Forms.RadioButton();
            this.m_rdbHEARTRATE_BASE_INT3 = new System.Windows.Forms.RadioButton();
            this.m_rdbHEARTRATE_BASE_INT0 = new System.Windows.Forms.RadioButton();
            this.m_rdbHEARTRATE_BASE_INT2 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_cmdEditTemplate = new PinkieControls.ButtonXP();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.m_cmdAddNew = new PinkieControls.ButtonXP();
            this.m_cmdConfirm = new PinkieControls.ButtonXP();
            this.m_cmdPrint = new PinkieControls.ButtonXP();
            this.m_cmdExit = new PinkieControls.ButtonXP();
            this.m_cmdCreateTemplate = new PinkieControls.ButtonXP();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.m_txtCLINICAL_DIAGNOSE_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.m_txtSUMMARY1_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.m_txtSUMMARY2_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.m_printDoc = new System.Drawing.Printing.PrintDocument();
            this.m_printPrevDlg = new System.Windows.Forms.PrintPreviewDialog();
            this.m_ttpTextInfo = new System.Windows.Forms.ToolTip(this.components);
            this.carID = new com.digitalwave.controls.clsCardTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.carID);
            this.groupBox1.Controls.Add(this.m_txtREPORTOR_NAME_VCHR);
            this.groupBox1.Controls.Add(this.m_txtDEPT_NAME_VCHR);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.m_labSubAge);
            this.groupBox1.Controls.Add(this.m_txtSubAGE_FLT);
            this.groupBox1.Controls.Add(this.m_txtAGE_FLT);
            this.groupBox1.Controls.Add(this.m_cmbAge);
            this.groupBox1.Controls.Add(this.m_txtREPORT_NO_CHR);
            this.groupBox1.Controls.Add(this.m_txtPATIENT_NAME_VCHR);
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
            this.groupBox1.Controls.Add(this.m_dtpREPORT_DAT);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(16, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 400);
            this.groupBox1.TabIndex = 10;
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
            this.m_txtREPORTOR_NAME_VCHR.Location = new System.Drawing.Point(76, 366);
            this.m_txtREPORTOR_NAME_VCHR.m_IntMaxListLength = 25;
            this.m_txtREPORTOR_NAME_VCHR.m_IsHaveParent = false;
            this.m_txtREPORTOR_NAME_VCHR.m_strParentName = "";
            this.m_txtREPORTOR_NAME_VCHR.Name = "m_txtREPORTOR_NAME_VCHR";
            this.m_txtREPORTOR_NAME_VCHR.SelectedItemBackColor = System.Drawing.Color.ForestGreen;
            this.m_txtREPORTOR_NAME_VCHR.SelectedItemForeColor = System.Drawing.Color.White;
            this.m_txtREPORTOR_NAME_VCHR.Size = new System.Drawing.Size(148, 23);
            this.m_txtREPORTOR_NAME_VCHR.TabIndex = 12;
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
            this.m_txtDEPT_NAME_VCHR.Location = new System.Drawing.Point(76, 263);
            this.m_txtDEPT_NAME_VCHR.m_IntMaxListLength = 25;
            this.m_txtDEPT_NAME_VCHR.m_IsHaveParent = false;
            this.m_txtDEPT_NAME_VCHR.m_strParentName = "";
            this.m_txtDEPT_NAME_VCHR.Name = "m_txtDEPT_NAME_VCHR";
            this.m_txtDEPT_NAME_VCHR.SelectedItemBackColor = System.Drawing.Color.ForestGreen;
            this.m_txtDEPT_NAME_VCHR.SelectedItemForeColor = System.Drawing.Color.White;
            this.m_txtDEPT_NAME_VCHR.Size = new System.Drawing.Size(148, 23);
            this.m_txtDEPT_NAME_VCHR.TabIndex = 9;
            this.m_txtDEPT_NAME_VCHR.txtValuse = "";
            this.m_txtDEPT_NAME_VCHR.VsLeftOrRight = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 122;
            this.label1.Text = "卡    号:";
            // 
            // m_labSubAge
            // 
            this.m_labSubAge.AutoSize = true;
            this.m_labSubAge.Location = new System.Drawing.Point(212, 232);
            this.m_labSubAge.Name = "m_labSubAge";
            this.m_labSubAge.Size = new System.Drawing.Size(21, 14);
            this.m_labSubAge.TabIndex = 120;
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
            this.m_txtSubAGE_FLT.Location = new System.Drawing.Point(168, 228);
            this.m_txtSubAGE_FLT.MaxLength = 4;
            this.m_txtSubAGE_FLT.Name = "m_txtSubAGE_FLT";
            //this.m_txtSubAGE_FLT.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
            this.m_txtSubAGE_FLT.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtSubAGE_FLT.Size = new System.Drawing.Size(40, 23);
            this.m_txtSubAGE_FLT.TabIndex = 8;
            this.m_txtSubAGE_FLT.Text = "0";
            this.m_txtSubAGE_FLT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.m_txtAGE_FLT.Location = new System.Drawing.Point(76, 228);
            this.m_txtAGE_FLT.MaxLength = 4;
            this.m_txtAGE_FLT.Name = "m_txtAGE_FLT";
            this.m_txtAGE_FLT.Size = new System.Drawing.Size(40, 23);
            this.m_txtAGE_FLT.TabIndex = 6;
            this.m_txtAGE_FLT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtAGE_FLT_KeyPress);
            // 
            // m_cmbAge
            // 
            this.m_cmbAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbAge.Items.AddRange(new object[] {
            "岁",
            "月",
            "天",
            "小时"});
            this.m_cmbAge.Location = new System.Drawing.Point(116, 228);
            this.m_cmbAge.Name = "m_cmbAge";
            this.m_cmbAge.Size = new System.Drawing.Size(52, 22);
            this.m_cmbAge.TabIndex = 7;
            this.m_cmbAge.SelectedIndexChanged += new System.EventHandler(this.m_cmbAge_SelectedIndexChanged);
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
            this.m_txtREPORT_NO_CHR.Location = new System.Drawing.Point(76, 123);
            this.m_txtREPORT_NO_CHR.MaxLength = 10;
            this.m_txtREPORT_NO_CHR.Name = "m_txtREPORT_NO_CHR";
            this.m_txtREPORT_NO_CHR.Size = new System.Drawing.Size(148, 23);
            this.m_txtREPORT_NO_CHR.TabIndex = 1;
            this.m_txtREPORT_NO_CHR.TextChanged += new System.EventHandler(this.m_txtPATIENT_NAME_VCHR_TextChanged);
            // 
            // m_txtPATIENT_NAME_VCHR
            // 
            //this.m_txtPATIENT_NAME_VCHR.EnableAutoValidation = false;
            //this.m_txtPATIENT_NAME_VCHR.EnableEnterKeyValidate = false;
            //this.m_txtPATIENT_NAME_VCHR.EnableEscapeKeyUndo = true;
            //this.m_txtPATIENT_NAME_VCHR.EnableLastValidValue = true;
            //this.m_txtPATIENT_NAME_VCHR.ErrorProvider = null;
            //this.m_txtPATIENT_NAME_VCHR.ErrorProviderMessage = "Invalid value";
            //this.m_txtPATIENT_NAME_VCHR.ForceFormatText = true;
            this.m_txtPATIENT_NAME_VCHR.Location = new System.Drawing.Point(76, 160);
            this.m_txtPATIENT_NAME_VCHR.MaxLength = 20;
            this.m_txtPATIENT_NAME_VCHR.Name = "m_txtPATIENT_NAME_VCHR";
            this.m_txtPATIENT_NAME_VCHR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_txtPATIENT_NAME_VCHR.Size = new System.Drawing.Size(148, 23);
            this.m_txtPATIENT_NAME_VCHR.TabIndex = 4;
            this.m_txtPATIENT_NAME_VCHR.TextChanged += new System.EventHandler(this.m_txtPATIENT_NAME_VCHR_TextChanged);
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
            this.m_txtINPATIENT_NO_CHR.Location = new System.Drawing.Point(76, 53);
            this.m_txtINPATIENT_NO_CHR.MaxLength = 15;
            this.m_txtINPATIENT_NO_CHR.Name = "m_txtINPATIENT_NO_CHR";
            this.m_txtINPATIENT_NO_CHR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_txtINPATIENT_NO_CHR.Size = new System.Drawing.Size(148, 23);
            this.m_txtINPATIENT_NO_CHR.TabIndex = 3;
            this.m_txtINPATIENT_NO_CHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtInPatientNo_KeyDown);
            // 
            // m_txtPATIENT_NO_CHR
            // 
            //this.m_txtPATIENT_NO_CHR.EnableAutoValidation = true;
            //this.m_txtPATIENT_NO_CHR.EnableEnterKeyValidate = true;
            //this.m_txtPATIENT_NO_CHR.EnableEscapeKeyUndo = true;
            //this.m_txtPATIENT_NO_CHR.EnableLastValidValue = true;
            //this.m_txtPATIENT_NO_CHR.ErrorProvider = null;
            //this.m_txtPATIENT_NO_CHR.ErrorProviderMessage = "Invalid value";
            //this.m_txtPATIENT_NO_CHR.ForceFormatText = true;
            this.m_txtPATIENT_NO_CHR.Location = new System.Drawing.Point(76, 88);
            this.m_txtPATIENT_NO_CHR.MaxLength = 10;
            this.m_txtPATIENT_NO_CHR.Name = "m_txtPATIENT_NO_CHR";
            //this.m_txtPATIENT_NO_CHR.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol;
            this.m_txtPATIENT_NO_CHR.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtPATIENT_NO_CHR.Size = new System.Drawing.Size(148, 23);
            this.m_txtPATIENT_NO_CHR.TabIndex = 2;
            this.m_txtPATIENT_NO_CHR.Text = "0";
            this.m_txtPATIENT_NO_CHR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_cboSEX_CHR
            // 
            this.m_cboSEX_CHR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSEX_CHR.Items.AddRange(new object[] {
            "男",
            "女",
            "不详"});
            this.m_cboSEX_CHR.Location = new System.Drawing.Point(76, 196);
            this.m_cboSEX_CHR.Name = "m_cboSEX_CHR";
            this.m_cboSEX_CHR.Size = new System.Drawing.Size(148, 22);
            this.m_cboSEX_CHR.TabIndex = 5;
            this.m_cboSEX_CHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboSEX_CHR_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 372);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 14);
            this.label14.TabIndex = 34;
            this.label14.Text = "报 告 者:";
            // 
            // m_txtBED_NO_CHR
            // 
            this.m_txtBED_NO_CHR.Location = new System.Drawing.Point(76, 300);
            this.m_txtBED_NO_CHR.MaxLength = 8;
            this.m_txtBED_NO_CHR.Name = "m_txtBED_NO_CHR";
            this.m_txtBED_NO_CHR.Size = new System.Drawing.Size(148, 23);
            this.m_txtBED_NO_CHR.TabIndex = 10;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(8, 304);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(70, 14);
            this.label22.TabIndex = 25;
            this.label22.Text = "床    号:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 268);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(70, 14);
            this.label21.TabIndex = 24;
            this.label21.Text = "科    室:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 232);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 14);
            this.label20.TabIndex = 23;
            this.label20.Text = "年    龄:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 200);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 14);
            this.label19.TabIndex = 22;
            this.label19.Text = "性    别:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 164);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(70, 14);
            this.label18.TabIndex = 21;
            this.label18.Text = "姓    名:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 57);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(70, 14);
            this.label17.TabIndex = 20;
            this.label17.Text = "住 院 号:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 92);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 14);
            this.label16.TabIndex = 19;
            this.label16.Text = "门 诊 号:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 127);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 14);
            this.label15.TabIndex = 18;
            this.label15.Text = "动 态 号:";
            // 
            // m_dtpREPORT_DAT
            // 
            this.m_dtpREPORT_DAT.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.m_dtpREPORT_DAT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpREPORT_DAT.Location = new System.Drawing.Point(76, 332);
            this.m_dtpREPORT_DAT.Name = "m_dtpREPORT_DAT";
            this.m_dtpREPORT_DAT.Size = new System.Drawing.Size(148, 23);
            this.m_dtpREPORT_DAT.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 336);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 14);
            this.label12.TabIndex = 14;
            this.label12.Text = "报告日期:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_cheIsNew);
            this.groupBox2.Controls.Add(this.m_cheIsSpical);
            this.groupBox2.Controls.Add(this.groupBox10);
            this.groupBox2.Controls.Add(this.groupBox7);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.m_dtpHEARTRATE_MIN_DAT);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.m_dtpHEARTRATE_MAX_DAT);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.m_txtQRS_TOTAL_CHR);
            this.groupBox2.Controls.Add(this.m_txtHEARTRATE_AVERAGE_INT);
            this.groupBox2.Controls.Add(this.m_txtHEARTRATE_MAX_INT);
            this.groupBox2.Controls.Add(this.m_txtHEARTRATE_MIN_INT);
            this.groupBox2.Controls.Add(this.m_rdbHEARTRATE_BASE_INT1);
            this.groupBox2.Controls.Add(this.m_rdbHEARTRATE_BASE_INT3);
            this.groupBox2.Controls.Add(this.m_rdbHEARTRATE_BASE_INT0);
            this.groupBox2.Controls.Add(this.m_rdbHEARTRATE_BASE_INT2);
            this.groupBox2.Location = new System.Drawing.Point(268, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(716, 232);
            this.groupBox2.TabIndex = 140;
            this.groupBox2.TabStop = false;
            // 
            // m_cheIsNew
            // 
            this.m_cheIsNew.Location = new System.Drawing.Point(540, 168);
            this.m_cheIsNew.Name = "m_cheIsNew";
            this.m_cheIsNew.Size = new System.Drawing.Size(72, 24);
            this.m_cheIsNew.TabIndex = 10001;
            this.m_cheIsNew.Text = "新记录";
            // 
            // m_cheIsSpical
            // 
            this.m_cheIsSpical.Location = new System.Drawing.Point(540, 196);
            this.m_cheIsSpical.Name = "m_cheIsSpical";
            this.m_cheIsSpical.Size = new System.Drawing.Size(128, 28);
            this.m_cheIsSpical.TabIndex = 10000;
            this.m_cheIsSpical.Text = "是否特殊病人";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.m_dtpCHECKFROM_DAT);
            this.groupBox10.Controls.Add(this.m_dtpCHECKTO_DAT);
            this.groupBox10.Controls.Add(this.label23);
            this.groupBox10.Controls.Add(this.m_txtTotalHour);
            this.groupBox10.Controls.Add(this.label24);
            this.groupBox10.Controls.Add(this.label25);
            this.groupBox10.Controls.Add(this.label26);
            this.groupBox10.Location = new System.Drawing.Point(12, 12);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(544, 52);
            this.groupBox10.TabIndex = 150;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "监测时间";
            // 
            // m_dtpCHECKFROM_DAT
            // 
            this.m_dtpCHECKFROM_DAT.CustomFormat = "yyyy-MM-dd HH:mm";
            this.m_dtpCHECKFROM_DAT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpCHECKFROM_DAT.Location = new System.Drawing.Point(32, 24);
            this.m_dtpCHECKFROM_DAT.Name = "m_dtpCHECKFROM_DAT";
            this.m_dtpCHECKFROM_DAT.Size = new System.Drawing.Size(148, 23);
            this.m_dtpCHECKFROM_DAT.TabIndex = 160;
            this.m_dtpCHECKFROM_DAT.ValueChanged += new System.EventHandler(this.m_dtpCHECKFROM_DAT_ValueChanged);
            // 
            // m_dtpCHECKTO_DAT
            // 
            this.m_dtpCHECKTO_DAT.CustomFormat = "yyyy-MM-dd HH:mm";
            this.m_dtpCHECKTO_DAT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpCHECKTO_DAT.Location = new System.Drawing.Point(200, 24);
            this.m_dtpCHECKTO_DAT.Name = "m_dtpCHECKTO_DAT";
            this.m_dtpCHECKTO_DAT.Size = new System.Drawing.Size(148, 23);
            this.m_dtpCHECKTO_DAT.TabIndex = 170;
            this.m_dtpCHECKTO_DAT.ValueChanged += new System.EventHandler(this.m_dtpCHECKFROM_DAT_ValueChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 26);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(21, 14);
            this.label23.TabIndex = 23;
            this.label23.Text = "从";
            // 
            // m_txtTotalHour
            // 
            this.m_txtTotalHour.Location = new System.Drawing.Point(380, 24);
            this.m_txtTotalHour.MaxLength = 5;
            this.m_txtTotalHour.Name = "m_txtTotalHour";
            this.m_txtTotalHour.ReadOnly = true;
            this.m_txtTotalHour.Size = new System.Drawing.Size(96, 23);
            this.m_txtTotalHour.TabIndex = 180;
            this.m_txtTotalHour.TabStop = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(180, 26);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(21, 14);
            this.label24.TabIndex = 24;
            this.label24.Text = "到";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(356, 26);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(21, 14);
            this.label25.TabIndex = 25;
            this.label25.Text = "共";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(480, 26);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(21, 14);
            this.label26.TabIndex = 26;
            this.label26.Text = "时";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.m_txtCHECK_CHANNELS_VCHR);
            this.groupBox7.Location = new System.Drawing.Point(12, 72);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(132, 52);
            this.groupBox7.TabIndex = 190;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "检测导联";
            // 
            // m_txtCHECK_CHANNELS_VCHR
            // 
            this.m_txtCHECK_CHANNELS_VCHR.AccessibleDescription = "心电图所见";
            this.m_txtCHECK_CHANNELS_VCHR.BackColor = System.Drawing.Color.White;
            this.m_txtCHECK_CHANNELS_VCHR.ContextMenu = this.m_mnuRichText;
            this.m_txtCHECK_CHANNELS_VCHR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtCHECK_CHANNELS_VCHR.ForeColor = System.Drawing.Color.Black;
            this.m_txtCHECK_CHANNELS_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtCHECK_CHANNELS_VCHR.Location = new System.Drawing.Point(12, 20);
            this.m_txtCHECK_CHANNELS_VCHR.m_BlnIgnoreUserInfo = true;
            this.m_txtCHECK_CHANNELS_VCHR.m_BlnPartControl = false;
            this.m_txtCHECK_CHANNELS_VCHR.m_BlnReadOnly = false;
            this.m_txtCHECK_CHANNELS_VCHR.m_BlnUnderLineDST = false;
            this.m_txtCHECK_CHANNELS_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtCHECK_CHANNELS_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtCHECK_CHANNELS_VCHR.m_IntCanModifyTime = 6;
            this.m_txtCHECK_CHANNELS_VCHR.m_IntPartControlLength = 0;
            this.m_txtCHECK_CHANNELS_VCHR.m_IntPartControlStartIndex = 0;
            this.m_txtCHECK_CHANNELS_VCHR.m_StrUserID = "";
            this.m_txtCHECK_CHANNELS_VCHR.m_StrUserName = "";
            this.m_txtCHECK_CHANNELS_VCHR.MaxLength = 1000;
            this.m_txtCHECK_CHANNELS_VCHR.Multiline = false;
            this.m_txtCHECK_CHANNELS_VCHR.Name = "m_txtCHECK_CHANNELS_VCHR";
            this.m_txtCHECK_CHANNELS_VCHR.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.m_txtCHECK_CHANNELS_VCHR.Size = new System.Drawing.Size(112, 23);
            this.m_txtCHECK_CHANNELS_VCHR.TabIndex = 200;
            this.m_txtCHECK_CHANNELS_VCHR.Text = "CM5、CM1、CMF";
            // 
            // m_mnuRichText
            // 
            this.m_mnuRichText.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_mnuRTDelete,
            this.meuUndoDel,
            this.menuItem1,
            this.menuItem2,
            this.menuItem3});
            // 
            // m_mnuRTDelete
            // 
            this.m_mnuRTDelete.Index = 0;
            this.m_mnuRTDelete.Text = "删除(&D)";
            this.m_mnuRTDelete.Click += new System.EventHandler(this.mnuRichTextBoxDelete_Click);
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.m_cmbHEARTRATE_BASE_INT);
            this.groupBox5.Location = new System.Drawing.Point(396, 72);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(272, 52);
            this.groupBox5.TabIndex = 260;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "基础心律";
            // 
            // m_cmbHEARTRATE_BASE_INT
            // 
            this.m_cmbHEARTRATE_BASE_INT.Items.AddRange(new object[] {
            "窦性",
            "室上性",
            "室性",
            "起搏心律",
            "窦性+室上性",
            "窦性+室性",
            "窦性+起搏心律",
            ""});
            this.m_cmbHEARTRATE_BASE_INT.Location = new System.Drawing.Point(36, 20);
            this.m_cmbHEARTRATE_BASE_INT.Name = "m_cmbHEARTRATE_BASE_INT";
            this.m_cmbHEARTRATE_BASE_INT.Size = new System.Drawing.Size(204, 22);
            this.m_cmbHEARTRATE_BASE_INT.TabIndex = 0;
            this.m_cmbHEARTRATE_BASE_INT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cmbHEARTRATE_BASE_INT_KeyDown);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.m_rdbGRAPH_TYPE_INT1);
            this.groupBox4.Controls.Add(this.m_rdbGRAPH_TYPE_INT3);
            this.groupBox4.Controls.Add(this.m_rdbGRAPH_TYPE_INT0);
            this.groupBox4.Controls.Add(this.m_rdbGRAPH_TYPE_INT2);
            this.groupBox4.Location = new System.Drawing.Point(156, 72);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(228, 52);
            this.groupBox4.TabIndex = 210;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "图形记录情况";
            // 
            // m_rdbGRAPH_TYPE_INT1
            // 
            this.m_rdbGRAPH_TYPE_INT1.Location = new System.Drawing.Point(52, 20);
            this.m_rdbGRAPH_TYPE_INT1.Name = "m_rdbGRAPH_TYPE_INT1";
            this.m_rdbGRAPH_TYPE_INT1.Size = new System.Drawing.Size(56, 24);
            this.m_rdbGRAPH_TYPE_INT1.TabIndex = 230;
            this.m_rdbGRAPH_TYPE_INT1.Text = "较好";
            // 
            // m_rdbGRAPH_TYPE_INT3
            // 
            this.m_rdbGRAPH_TYPE_INT3.Location = new System.Drawing.Point(164, 20);
            this.m_rdbGRAPH_TYPE_INT3.Name = "m_rdbGRAPH_TYPE_INT3";
            this.m_rdbGRAPH_TYPE_INT3.Size = new System.Drawing.Size(60, 24);
            this.m_rdbGRAPH_TYPE_INT3.TabIndex = 250;
            this.m_rdbGRAPH_TYPE_INT3.Text = "欠佳";
            // 
            // m_rdbGRAPH_TYPE_INT0
            // 
            this.m_rdbGRAPH_TYPE_INT0.Checked = true;
            this.m_rdbGRAPH_TYPE_INT0.Location = new System.Drawing.Point(8, 20);
            this.m_rdbGRAPH_TYPE_INT0.Name = "m_rdbGRAPH_TYPE_INT0";
            this.m_rdbGRAPH_TYPE_INT0.Size = new System.Drawing.Size(44, 24);
            this.m_rdbGRAPH_TYPE_INT0.TabIndex = 220;
            this.m_rdbGRAPH_TYPE_INT0.TabStop = true;
            this.m_rdbGRAPH_TYPE_INT0.Text = "好";
            // 
            // m_rdbGRAPH_TYPE_INT2
            // 
            this.m_rdbGRAPH_TYPE_INT2.Location = new System.Drawing.Point(108, 20);
            this.m_rdbGRAPH_TYPE_INT2.Name = "m_rdbGRAPH_TYPE_INT2";
            this.m_rdbGRAPH_TYPE_INT2.Size = new System.Drawing.Size(56, 24);
            this.m_rdbGRAPH_TYPE_INT2.TabIndex = 240;
            this.m_rdbGRAPH_TYPE_INT2.Text = "一般";
            // 
            // m_dtpHEARTRATE_MIN_DAT
            // 
            this.m_dtpHEARTRATE_MIN_DAT.CustomFormat = "yyyy-MM-dd HH:mm";
            this.m_dtpHEARTRATE_MIN_DAT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpHEARTRATE_MIN_DAT.Location = new System.Drawing.Point(340, 200);
            this.m_dtpHEARTRATE_MIN_DAT.Name = "m_dtpHEARTRATE_MIN_DAT";
            this.m_dtpHEARTRATE_MIN_DAT.Size = new System.Drawing.Size(140, 23);
            this.m_dtpHEARTRATE_MIN_DAT.TabIndex = 360;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(288, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 14);
            this.label5.TabIndex = 36;
            this.label5.Text = "时间:";
            // 
            // m_dtpHEARTRATE_MAX_DAT
            // 
            this.m_dtpHEARTRATE_MAX_DAT.CustomFormat = "yyyy-MM-dd HH:mm";
            this.m_dtpHEARTRATE_MAX_DAT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpHEARTRATE_MAX_DAT.Location = new System.Drawing.Point(340, 168);
            this.m_dtpHEARTRATE_MAX_DAT.Name = "m_dtpHEARTRATE_MAX_DAT";
            this.m_dtpHEARTRATE_MAX_DAT.Size = new System.Drawing.Size(140, 23);
            this.m_dtpHEARTRATE_MAX_DAT.TabIndex = 340;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(12, 204);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(70, 14);
            this.label30.TabIndex = 33;
            this.label30.Text = "最低心率:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(208, 204);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(42, 14);
            this.label31.TabIndex = 34;
            this.label31.Text = "次/分";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(12, 172);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(70, 14);
            this.label28.TabIndex = 30;
            this.label28.Text = "最高心率:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(208, 172);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(42, 14);
            this.label29.TabIndex = 31;
            this.label29.Text = "次/分";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "平均心率:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 8;
            this.label6.Text = "QRS 总数:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(288, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "时间:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(484, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "次/分";
            // 
            // m_txtQRS_TOTAL_CHR
            // 
            //this.m_txtQRS_TOTAL_CHR.EnableAutoValidation = true;
            //this.m_txtQRS_TOTAL_CHR.EnableEnterKeyValidate = true;
            //this.m_txtQRS_TOTAL_CHR.EnableEscapeKeyUndo = true;
            //this.m_txtQRS_TOTAL_CHR.EnableLastValidValue = true;
            //this.m_txtQRS_TOTAL_CHR.ErrorProvider = null;
            //this.m_txtQRS_TOTAL_CHR.ErrorProviderMessage = "Invalid value";
            //this.m_txtQRS_TOTAL_CHR.ForceFormatText = true;
            this.m_txtQRS_TOTAL_CHR.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.m_txtQRS_TOTAL_CHR.Location = new System.Drawing.Point(84, 132);
            this.m_txtQRS_TOTAL_CHR.MaxLength = 10;
            this.m_txtQRS_TOTAL_CHR.Name = "m_txtQRS_TOTAL_CHR";
            //this.m_txtQRS_TOTAL_CHR.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol;
            this.m_txtQRS_TOTAL_CHR.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtQRS_TOTAL_CHR.Size = new System.Drawing.Size(116, 23);
            this.m_txtQRS_TOTAL_CHR.TabIndex = 310;
            this.m_txtQRS_TOTAL_CHR.Text = "0";
            this.m_txtQRS_TOTAL_CHR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_txtHEARTRATE_AVERAGE_INT
            // 
            //this.m_txtHEARTRATE_AVERAGE_INT.EnableAutoValidation = true;
            //this.m_txtHEARTRATE_AVERAGE_INT.EnableEnterKeyValidate = true;
            //this.m_txtHEARTRATE_AVERAGE_INT.EnableEscapeKeyUndo = true;
            //this.m_txtHEARTRATE_AVERAGE_INT.EnableLastValidValue = true;
            //this.m_txtHEARTRATE_AVERAGE_INT.ErrorProvider = null;
            //this.m_txtHEARTRATE_AVERAGE_INT.ErrorProviderMessage = "Invalid value";
            //this.m_txtHEARTRATE_AVERAGE_INT.ForceFormatText = true;
            this.m_txtHEARTRATE_AVERAGE_INT.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.m_txtHEARTRATE_AVERAGE_INT.Location = new System.Drawing.Point(340, 132);
            this.m_txtHEARTRATE_AVERAGE_INT.MaxLength = 3;
            this.m_txtHEARTRATE_AVERAGE_INT.Name = "m_txtHEARTRATE_AVERAGE_INT";
            //this.m_txtHEARTRATE_AVERAGE_INT.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol;
            this.m_txtHEARTRATE_AVERAGE_INT.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtHEARTRATE_AVERAGE_INT.Size = new System.Drawing.Size(136, 23);
            this.m_txtHEARTRATE_AVERAGE_INT.TabIndex = 320;
            this.m_txtHEARTRATE_AVERAGE_INT.Text = "0";
            this.m_txtHEARTRATE_AVERAGE_INT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_txtHEARTRATE_MAX_INT
            // 
            //this.m_txtHEARTRATE_MAX_INT.EnableAutoValidation = true;
            //this.m_txtHEARTRATE_MAX_INT.EnableEnterKeyValidate = true;
            //this.m_txtHEARTRATE_MAX_INT.EnableEscapeKeyUndo = true;
            //this.m_txtHEARTRATE_MAX_INT.EnableLastValidValue = true;
            //this.m_txtHEARTRATE_MAX_INT.ErrorProvider = null;
            //this.m_txtHEARTRATE_MAX_INT.ErrorProviderMessage = "Invalid value";
            //this.m_txtHEARTRATE_MAX_INT.ForceFormatText = true;
            this.m_txtHEARTRATE_MAX_INT.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.m_txtHEARTRATE_MAX_INT.Location = new System.Drawing.Point(84, 168);
            this.m_txtHEARTRATE_MAX_INT.MaxLength = 3;
            this.m_txtHEARTRATE_MAX_INT.Name = "m_txtHEARTRATE_MAX_INT";
            //this.m_txtHEARTRATE_MAX_INT.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol;
            this.m_txtHEARTRATE_MAX_INT.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtHEARTRATE_MAX_INT.Size = new System.Drawing.Size(116, 23);
            this.m_txtHEARTRATE_MAX_INT.TabIndex = 330;
            this.m_txtHEARTRATE_MAX_INT.Text = "0";
            this.m_txtHEARTRATE_MAX_INT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_txtHEARTRATE_MIN_INT
            // 
            //this.m_txtHEARTRATE_MIN_INT.EnableAutoValidation = true;
            //this.m_txtHEARTRATE_MIN_INT.EnableEnterKeyValidate = true;
            //this.m_txtHEARTRATE_MIN_INT.EnableEscapeKeyUndo = true;
            //this.m_txtHEARTRATE_MIN_INT.EnableLastValidValue = true;
            //this.m_txtHEARTRATE_MIN_INT.ErrorProvider = null;
            //this.m_txtHEARTRATE_MIN_INT.ErrorProviderMessage = "Invalid value";
            //this.m_txtHEARTRATE_MIN_INT.ForceFormatText = true;
            this.m_txtHEARTRATE_MIN_INT.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.m_txtHEARTRATE_MIN_INT.Location = new System.Drawing.Point(84, 200);
            this.m_txtHEARTRATE_MIN_INT.MaxLength = 3;
            this.m_txtHEARTRATE_MIN_INT.Name = "m_txtHEARTRATE_MIN_INT";
            //this.m_txtHEARTRATE_MIN_INT.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol;
            this.m_txtHEARTRATE_MIN_INT.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtHEARTRATE_MIN_INT.Size = new System.Drawing.Size(116, 23);
            this.m_txtHEARTRATE_MIN_INT.TabIndex = 350;
            this.m_txtHEARTRATE_MIN_INT.Text = "0";
            this.m_txtHEARTRATE_MIN_INT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_rdbHEARTRATE_BASE_INT1
            // 
            this.m_rdbHEARTRATE_BASE_INT1.Location = new System.Drawing.Point(604, 28);
            this.m_rdbHEARTRATE_BASE_INT1.Name = "m_rdbHEARTRATE_BASE_INT1";
            this.m_rdbHEARTRATE_BASE_INT1.Size = new System.Drawing.Size(68, 24);
            this.m_rdbHEARTRATE_BASE_INT1.TabIndex = 280;
            this.m_rdbHEARTRATE_BASE_INT1.Text = "室上性";
            this.m_rdbHEARTRATE_BASE_INT1.Visible = false;
            // 
            // m_rdbHEARTRATE_BASE_INT3
            // 
            this.m_rdbHEARTRATE_BASE_INT3.Location = new System.Drawing.Point(604, 28);
            this.m_rdbHEARTRATE_BASE_INT3.Name = "m_rdbHEARTRATE_BASE_INT3";
            this.m_rdbHEARTRATE_BASE_INT3.Size = new System.Drawing.Size(84, 24);
            this.m_rdbHEARTRATE_BASE_INT3.TabIndex = 300;
            this.m_rdbHEARTRATE_BASE_INT3.Text = "起搏心律";
            this.m_rdbHEARTRATE_BASE_INT3.Visible = false;
            // 
            // m_rdbHEARTRATE_BASE_INT0
            // 
            this.m_rdbHEARTRATE_BASE_INT0.Checked = true;
            this.m_rdbHEARTRATE_BASE_INT0.Location = new System.Drawing.Point(580, 28);
            this.m_rdbHEARTRATE_BASE_INT0.Name = "m_rdbHEARTRATE_BASE_INT0";
            this.m_rdbHEARTRATE_BASE_INT0.Size = new System.Drawing.Size(56, 24);
            this.m_rdbHEARTRATE_BASE_INT0.TabIndex = 270;
            this.m_rdbHEARTRATE_BASE_INT0.TabStop = true;
            this.m_rdbHEARTRATE_BASE_INT0.Text = "窦性";
            this.m_rdbHEARTRATE_BASE_INT0.Visible = false;
            // 
            // m_rdbHEARTRATE_BASE_INT2
            // 
            this.m_rdbHEARTRATE_BASE_INT2.Location = new System.Drawing.Point(636, 36);
            this.m_rdbHEARTRATE_BASE_INT2.Name = "m_rdbHEARTRATE_BASE_INT2";
            this.m_rdbHEARTRATE_BASE_INT2.Size = new System.Drawing.Size(52, 24);
            this.m_rdbHEARTRATE_BASE_INT2.TabIndex = 290;
            this.m_rdbHEARTRATE_BASE_INT2.Text = "室性";
            this.m_rdbHEARTRATE_BASE_INT2.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_cmdEditTemplate);
            this.groupBox3.Controls.Add(this.buttonXP1);
            this.groupBox3.Controls.Add(this.m_cmdDelete);
            this.groupBox3.Controls.Add(this.m_cmdAddNew);
            this.groupBox3.Controls.Add(this.m_cmdConfirm);
            this.groupBox3.Controls.Add(this.m_cmdPrint);
            this.groupBox3.Controls.Add(this.m_cmdExit);
            this.groupBox3.Controls.Add(this.m_cmdCreateTemplate);
            this.groupBox3.Location = new System.Drawing.Point(16, 408);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(248, 308);
            this.groupBox3.TabIndex = 410;
            this.groupBox3.TabStop = false;
            // 
            // m_cmdEditTemplate
            // 
            this.m_cmdEditTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdEditTemplate.DefaultScheme = true;
            this.m_cmdEditTemplate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdEditTemplate.Hint = "";
            this.m_cmdEditTemplate.Location = new System.Drawing.Point(28, 192);
            this.m_cmdEditTemplate.Name = "m_cmdEditTemplate";
            this.m_cmdEditTemplate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdEditTemplate.Size = new System.Drawing.Size(188, 32);
            this.m_cmdEditTemplate.TabIndex = 456;
            this.m_cmdEditTemplate.Text = "修 改 模 板";
            this.m_cmdEditTemplate.Click += new System.EventHandler(this.m_cmdEditTemplate_Click);
            // 
            // buttonXP1
            // 
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(28, 228);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(188, 32);
            this.buttonXP1.TabIndex = 458;
            this.buttonXP1.Text = "清      空";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(28, 48);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete.Size = new System.Drawing.Size(188, 32);
            this.m_cmdDelete.TabIndex = 430;
            this.m_cmdDelete.Text = "删      除";
            this.m_cmdDelete.Click += new System.EventHandler(this.buttonXP3_Click);
            // 
            // m_cmdAddNew
            // 
            this.m_cmdAddNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdAddNew.DefaultScheme = true;
            this.m_cmdAddNew.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdAddNew.Hint = "";
            this.m_cmdAddNew.Location = new System.Drawing.Point(28, 12);
            this.m_cmdAddNew.Name = "m_cmdAddNew";
            this.m_cmdAddNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdAddNew.Size = new System.Drawing.Size(188, 32);
            this.m_cmdAddNew.TabIndex = 420;
            this.m_cmdAddNew.Text = "保      存";
            this.m_cmdAddNew.Click += new System.EventHandler(this.m_cmdAddNew_Click);
            // 
            // m_cmdConfirm
            // 
            this.m_cmdConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdConfirm.DefaultScheme = true;
            this.m_cmdConfirm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdConfirm.Hint = "";
            this.m_cmdConfirm.Location = new System.Drawing.Point(28, 84);
            this.m_cmdConfirm.Name = "m_cmdConfirm";
            this.m_cmdConfirm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdConfirm.Size = new System.Drawing.Size(188, 32);
            this.m_cmdConfirm.TabIndex = 440;
            this.m_cmdConfirm.Text = "审      核";
            this.m_cmdConfirm.Click += new System.EventHandler(this.m_cmdConfirm_Click);
            // 
            // m_cmdPrint
            // 
            this.m_cmdPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdPrint.DefaultScheme = true;
            this.m_cmdPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdPrint.Hint = "";
            this.m_cmdPrint.Location = new System.Drawing.Point(28, 120);
            this.m_cmdPrint.Name = "m_cmdPrint";
            this.m_cmdPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdPrint.Size = new System.Drawing.Size(188, 32);
            this.m_cmdPrint.TabIndex = 450;
            this.m_cmdPrint.Text = "打      印";
            this.m_cmdPrint.Click += new System.EventHandler(this.m_cmdPrint_Click);
            // 
            // m_cmdExit
            // 
            this.m_cmdExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdExit.DefaultScheme = true;
            this.m_cmdExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdExit.Hint = "";
            this.m_cmdExit.Location = new System.Drawing.Point(28, 264);
            this.m_cmdExit.Name = "m_cmdExit";
            this.m_cmdExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdExit.Size = new System.Drawing.Size(188, 32);
            this.m_cmdExit.TabIndex = 460;
            this.m_cmdExit.Text = "退      出";
            this.m_cmdExit.Click += new System.EventHandler(this.m_cmdExit_Click);
            // 
            // m_cmdCreateTemplate
            // 
            this.m_cmdCreateTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdCreateTemplate.DefaultScheme = true;
            this.m_cmdCreateTemplate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdCreateTemplate.Hint = "";
            this.m_cmdCreateTemplate.Location = new System.Drawing.Point(28, 156);
            this.m_cmdCreateTemplate.Name = "m_cmdCreateTemplate";
            this.m_cmdCreateTemplate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCreateTemplate.Size = new System.Drawing.Size(188, 32);
            this.m_cmdCreateTemplate.TabIndex = 455;
            this.m_cmdCreateTemplate.Text = " 生 成 模 板";
            this.m_cmdCreateTemplate.Click += new System.EventHandler(this.m_cmdCreateTemplate_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.m_txtCLINICAL_DIAGNOSE_VCHR);
            this.groupBox6.Location = new System.Drawing.Point(268, 8);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(716, 72);
            this.groupBox6.TabIndex = 120;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "临床诊断";
            // 
            // m_txtCLINICAL_DIAGNOSE_VCHR
            // 
            this.m_txtCLINICAL_DIAGNOSE_VCHR.AccessibleDescription = "心电图所见";
            this.m_txtCLINICAL_DIAGNOSE_VCHR.BackColor = System.Drawing.Color.White;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.ContextMenu = this.m_mnuRichText;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtCLINICAL_DIAGNOSE_VCHR.ForeColor = System.Drawing.Color.Black;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.Location = new System.Drawing.Point(12, 20);
            this.m_txtCLINICAL_DIAGNOSE_VCHR.m_BlnIgnoreUserInfo = true;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.m_BlnPartControl = false;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.m_BlnReadOnly = false;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.m_BlnUnderLineDST = false;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.m_IntCanModifyTime = 6;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.m_IntPartControlLength = 0;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.m_IntPartControlStartIndex = 0;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.m_StrUserID = "";
            this.m_txtCLINICAL_DIAGNOSE_VCHR.m_StrUserName = "";
            this.m_txtCLINICAL_DIAGNOSE_VCHR.MaxLength = 500;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.Name = "m_txtCLINICAL_DIAGNOSE_VCHR";
            this.m_txtCLINICAL_DIAGNOSE_VCHR.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.Size = new System.Drawing.Size(676, 40);
            this.m_txtCLINICAL_DIAGNOSE_VCHR.TabIndex = 130;
            this.m_txtCLINICAL_DIAGNOSE_VCHR.Text = "";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.m_txtSUMMARY1_VCHR);
            this.groupBox8.Location = new System.Drawing.Point(268, 324);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(716, 168);
            this.groupBox8.TabIndex = 370;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "分析";
            // 
            // m_txtSUMMARY1_VCHR
            // 
            this.m_txtSUMMARY1_VCHR.AccessibleDescription = "心电图所见";
            this.m_txtSUMMARY1_VCHR.BackColor = System.Drawing.Color.White;
            this.m_txtSUMMARY1_VCHR.ContextMenu = this.m_mnuRichText;
            this.m_txtSUMMARY1_VCHR.Font = new System.Drawing.Font("宋体", 14F);
            this.m_txtSUMMARY1_VCHR.ForeColor = System.Drawing.Color.Black;
            this.m_txtSUMMARY1_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtSUMMARY1_VCHR.Location = new System.Drawing.Point(12, 24);
            this.m_txtSUMMARY1_VCHR.m_BlnIgnoreUserInfo = true;
            this.m_txtSUMMARY1_VCHR.m_BlnPartControl = false;
            this.m_txtSUMMARY1_VCHR.m_BlnReadOnly = false;
            this.m_txtSUMMARY1_VCHR.m_BlnUnderLineDST = false;
            this.m_txtSUMMARY1_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtSUMMARY1_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtSUMMARY1_VCHR.m_IntCanModifyTime = 6;
            this.m_txtSUMMARY1_VCHR.m_IntPartControlLength = 0;
            this.m_txtSUMMARY1_VCHR.m_IntPartControlStartIndex = 0;
            this.m_txtSUMMARY1_VCHR.m_StrUserID = "";
            this.m_txtSUMMARY1_VCHR.m_StrUserName = "";
            this.m_txtSUMMARY1_VCHR.MaxLength = 1000;
            this.m_txtSUMMARY1_VCHR.Name = "m_txtSUMMARY1_VCHR";
            this.m_txtSUMMARY1_VCHR.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtSUMMARY1_VCHR.Size = new System.Drawing.Size(680, 132);
            this.m_txtSUMMARY1_VCHR.TabIndex = 70;
            this.m_txtSUMMARY1_VCHR.Text = "";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.m_txtSUMMARY2_VCHR);
            this.groupBox9.Location = new System.Drawing.Point(268, 496);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(716, 220);
            this.groupBox9.TabIndex = 390;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "结论";
            // 
            // m_txtSUMMARY2_VCHR
            // 
            this.m_txtSUMMARY2_VCHR.AccessibleDescription = "心电图所见";
            this.m_txtSUMMARY2_VCHR.BackColor = System.Drawing.Color.White;
            this.m_txtSUMMARY2_VCHR.ContextMenu = this.m_mnuRichText;
            this.m_txtSUMMARY2_VCHR.Font = new System.Drawing.Font("宋体", 14F);
            this.m_txtSUMMARY2_VCHR.ForeColor = System.Drawing.Color.Black;
            this.m_txtSUMMARY2_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtSUMMARY2_VCHR.Location = new System.Drawing.Point(12, 28);
            this.m_txtSUMMARY2_VCHR.m_BlnIgnoreUserInfo = true;
            this.m_txtSUMMARY2_VCHR.m_BlnPartControl = false;
            this.m_txtSUMMARY2_VCHR.m_BlnReadOnly = false;
            this.m_txtSUMMARY2_VCHR.m_BlnUnderLineDST = false;
            this.m_txtSUMMARY2_VCHR.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtSUMMARY2_VCHR.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtSUMMARY2_VCHR.m_IntCanModifyTime = 6;
            this.m_txtSUMMARY2_VCHR.m_IntPartControlLength = 0;
            this.m_txtSUMMARY2_VCHR.m_IntPartControlStartIndex = 0;
            this.m_txtSUMMARY2_VCHR.m_StrUserID = "";
            this.m_txtSUMMARY2_VCHR.m_StrUserName = "";
            this.m_txtSUMMARY2_VCHR.MaxLength = 1000;
            this.m_txtSUMMARY2_VCHR.Name = "m_txtSUMMARY2_VCHR";
            this.m_txtSUMMARY2_VCHR.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtSUMMARY2_VCHR.Size = new System.Drawing.Size(680, 184);
            this.m_txtSUMMARY2_VCHR.TabIndex = 400;
            this.m_txtSUMMARY2_VCHR.Text = "";
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
            // carID
            // 
            this.carID.Location = new System.Drawing.Point(76, 20);
            this.carID.MaxLength = 50;
            this.carID.Name = "carID";
            this.carID.PatientCard = "";
            this.carID.PatientFlag = 0;
            this.carID.Size = new System.Drawing.Size(148, 23);
            this.carID.TabIndex = 0;
            this.carID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.carID.YBCardText = "";
            this.carID.CardKeyDown += new com.digitalwave.controls.clsCardTextBox.TxtKeyDownHandle(this.carID1_CardKeyDown);
            // 
            // frmDnmCardiogramReport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1016, 733);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox8);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDnmCardiogramReport";
            this.Text = "动态心电图报告";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDnmCardiogramReport_KeyDown);
            this.Load += new System.EventHandler(this.frmDnmCardiogramReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			this.objController = new com.digitalwave.iCare.gui.RIS.clsController_RISDnmCardiogramReport(this);
			objController.Set_GUI_Apperance(this);
		}

		private void frmDnmCardiogramReport_Load(object sender, System.EventArgs e)
		{
			m_mthSetFormControlCanBeNull(this);
            
//			this.m_cmbAge.SelectedIndex=0;
			m_mthSetEnter2Tab(new System.Windows.Forms.Control[] 
					{m_txtSUMMARY1_VCHR,m_txtSUMMARY2_VCHR,m_txtDEPT_NAME_VCHR,m_txtREPORTOR_NAME_VCHR});
		}

		public void m_mthSetReport(clsRIS_DCardiogramReport_VO p_objItem)
		{
			((clsController_RISDnmCardiogramReport)this.objController).m_mthSetReport(p_objItem);
		}

		private void frmCardiogramReport_Load(object sender, System.EventArgs e)
		{
	
		}

		private void m_cmdExit_Click(object sender, System.EventArgs e)
		{
			if(m_cmdDelete.Enabled==false&&m_txtREPORT_NO_CHR.Text.Trim()!=""&&m_txtPATIENT_NAME_VCHR.Text.Trim()!="")
			{
				if(MessageBox.Show("病人信息尚未保存,是否退出?","ICare",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.No)
				{
					return;
				}
			}
			objController = null;
			this.Close();
		}

		private void m_cmdSave_Click(object sender, System.EventArgs e)
		{
			((clsController_RISDnmCardiogramReport)this.objController).m_mthDoSave();
		}

		private void m_cmdDelete_Click(object sender, System.EventArgs e)
		{
			((clsController_RISDnmCardiogramReport)this.objController).m_mthDoDelete();
		}

		private void m_cmdAddNew_Click(object sender, System.EventArgs e)
		{
			((clsController_RISDnmCardiogramReport)this.objController).m_mthDoSave();
		}

		private void buttonXP3_Click(object sender, System.EventArgs e)
		{
			((clsController_RISDnmCardiogramReport)this.objController).m_mthDoDelete();
            
		}

		private void m_cmdPrint_Click(object sender, System.EventArgs e)
		{
			((clsController_RISDnmCardiogramReport)this.objController).m_mthPrintReport(this);
		}

		private void m_dtpCHECKFROM_DAT_ValueChanged(object sender, System.EventArgs e)
		{
			((clsController_RISDnmCardiogramReport)this.objController).m_mthShowBetweenTime();
		}

		private void m_printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			((clsController_RISDnmCardiogramReport)this.objController).m_mthPrintPage(e);
		}

		private void m_cmdCreateTemplate_Click(object sender, System.EventArgs e)
		{
            ((clsController_RISDnmCardiogramReport)this.objController).m_mthCreateTemplate();
			//((clsController_RISDnmCardiogramReport)this.objController).m_mthCreateTemplate(this);
		}

		public void SetParentApperance(frmCardiogramReportManage infrmCardiogramReportManage)
		{
			((clsController_RISDnmCardiogramReport)this.objController).SetParentApperance(infrmCardiogramReportManage);
		}

		private void m_mthInitRichTextBox()
		{

			ctlRichTextBox.m_ClrDefaultViewText=Color.Black;
			ctlRichTextBox[] rtbArr  =new ctlRichTextBox[]{m_txtCLINICAL_DIAGNOSE_VCHR,m_txtCHECK_CHANNELS_VCHR,m_txtSUMMARY1_VCHR ,m_txtSUMMARY2_VCHR };
			foreach(ctlRichTextBox objRTB in rtbArr)
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
			m_txtCHECK_CHANNELS_VCHR.m_mthSetNewText("CM5、CM1、CMF","<root/>");
			
		}

		
		#region RichTextBox Event

		private void m_mthHandleMouseEnterDeleteText(object p_objSender,EventArgs p_objArg)
		{
			com.digitalwave.controls.clsDoubleStrikeThoughEventArg objArg = (com.digitalwave.controls.clsDoubleStrikeThoughEventArg)p_objArg;

			string strInfo = "用户姓名 : " +	objArg.m_strUserName+"\r\n删除时间 : ";

			if(objArg.m_dtmDeleteTime != m_dtmEmptyDate && objArg.m_dtmDeleteTime != DateTime.MinValue)
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

			if(objArg.m_dtmInsertTime != m_dtmEmptyDate && objArg.m_dtmInsertTime != DateTime.MinValue)
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
		private void m_mnuDownTag_Click(object sender, System.EventArgs e)
		{
			if(this.ActiveControl is com.digitalwave.controls.ctlRichTextBox)
			{
				((com.digitalwave.controls.ctlRichTextBox)(this.ActiveControl)).m_mthSetSelectionScript(1);
			}

		}

		private void m_mnuUpTag_Click(object sender, System.EventArgs e)
		{
			if(this.ActiveControl is com.digitalwave.controls.ctlRichTextBox)
			{
				((com.digitalwave.controls.ctlRichTextBox)(this.ActiveControl)).m_mthSetSelectionScript(0);
			}

		}
		#endregion

		private void frmDnmCardiogramReport_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			m_mthSetKeyTab(e);
		}

		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
			((clsController_RISDnmCardiogramReport)this.objController).m_mthClear();
            this.m_cmdAddNew.Enabled = true;
		}

		private void m_cmdEditTemplate_Click(object sender, System.EventArgs e)
		{
            ((clsController_RISDnmCardiogramReport)this.objController).m_mthEditTemplate();
            //System.Security.Principal.IPrincipal objPrincipal = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity("00000",""),null);
            //clsEmployeeVO objEmployee= new clsEmployeeVO();
            //objEmployee.strEmpID = "00000";
            //objEmployee.strName = "测试";
            //clsDepartmentVO objDept = new clsDepartmentVO();
            //objDept.strDeptID = "0000000";
            //objDept.strDeptName = "测试科";

            //com.digitalwave.iCare.gui.TemplateUtility.frmTemplateSet frm = new com.digitalwave.iCare.gui.TemplateUtility.frmTemplateSet(objPrincipal,objDept,objEmployee);
            ////			frm.MdiParent = this;
            //frm.Show();
		}

		private void m_txtPATIENT_NAME_VCHR_TextChanged(object sender, System.EventArgs e)
		{
//			((clsController_RISDnmCardiogramReport)this.objController).TextCheck();
		}

		private void m_txtREPORTOR_NAME_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			
//			if(e.KeyCode==Keys.Enter&&m_txtREPORTOR_NAME_VCHR.Text.Trim()=="")
//             {
//			SendKeys.Send("{Tab}");
//			}
		}

		private void m_cmbAge_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Down)
			{
				if(this.m_cmbAge.SelectedIndex==3)
				{
					this.m_cmbAge.SelectedIndex=0;
					e.Handled=true;
				}
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

		private void m_cboSEX_CHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Down)
			{
				if(this.m_cboSEX_CHR.SelectedIndex==2)
				{
					this.m_cboSEX_CHR.SelectedIndex=0;
					e.Handled=true;
				}
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

		private void meuUndoDel_Click(object sender, System.EventArgs e)
		{
			if(this.ActiveControl is com.digitalwave.controls.ctlRichTextBox)
			{
				((com.digitalwave.controls.ctlRichTextBox)(this.ActiveControl)).m_mthSelectionDoubleStrikeThough(false);
			}
		}

		private void m_cmbHEARTRATE_BASE_INT_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Down)
			{
				if(m_cmbHEARTRATE_BASE_INT.SelectedIndex==6)
				{
				m_cmbHEARTRATE_BASE_INT.SelectedIndex=0;
				e.Handled=true;
				}
			}
		}

		private void m_txtAGE_FLT_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if((e.KeyChar>='0'&&e.KeyChar<='9')||(e.KeyChar==8)||(e.KeyChar=='*'))
				e.Handled=false;
			else
				e.Handled=true;

		}

		private void m_txtDEPT_NAME_VCHR_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if((e.KeyChar=='?'))
				e.Handled=true;
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
				((clsController_RISDnmCardiogramReport)this.objController).m_lngGetPat();
			}
		}

		private void m_txtDEPT_NAME_VCHR_evtValueChanged(object sender, com.digitalwave.Utility.clsExValueChangedEventArgs e)
		{
						m_txtREPORTOR_NAME_VCHR.Tag=m_txtDEPT_NAME_VCHR.Tag;
		}

        private void m_cmdConfirm_Click(object sender, EventArgs e)
        {
            ((clsController_RISDnmCardiogramReport)this.objController).m_mthDmnConfirm();
            
        }

        private void m_txtInPatientNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsController_RISDnmCardiogramReport)this.objController).m_lngGetPatByInPatientID();
            }
        }

        private void carID1_CardKeyDown(object sender, EventArgs e)
        {
            ((clsController_RISDnmCardiogramReport)this.objController).m_lngGetPat();
        }

		


	}
}
