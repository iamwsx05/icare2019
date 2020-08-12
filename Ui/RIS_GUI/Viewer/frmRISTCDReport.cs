using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PinkieControls;
using weCare.Core.Entity;
using com.digitalwave.controls;	//digitalwavecontrol.dll

namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// frmLisDeviceManage 的摘要说明。
	/// </summary>
	public class frmRISTCDReport : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
	{
		#region Define
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		internal PinkieControls.ButtonXP m_cmdExit;
		private System.Windows.Forms.Label label14;
		internal System.Windows.Forms.DateTimePicker m_dtpCHECK_DAT;
		internal System.Windows.Forms.DateTimePicker m_dtpREPORT_DAT;
		internal PinkieControls.ButtonXP m_cmdConfirm;
		internal PinkieControls.ButtonXP m_cmdPrint;
		internal PinkieControls.ButtonXP m_cmdDelete;
		internal PinkieControls.ButtonXP m_cmdSave;

		#endregion
		internal System.Windows.Forms.ComboBox m_cboSEX_CHR;
		internal TCDPrintPreviewDialog m_printPrevDlg;
//		internal System.Windows.Forms.PrintPreviewDialog m_printPrevDlg;
		internal System.Drawing.Printing.PrintDocument m_printDoc;
		internal clsPrint_RISCardiogramReport objRISCardiogramReport = new clsPrint_RISCardiogramReport();
		internal System.Windows.Forms.PrintDialog m_printDlg;
//		internal PrintDialog m_printDlg;
		internal PinkieControls.ButtonXP m_cmdCreateTemplate;
		internal com.digitalwave.controls.ctlRichTextBox  m_txtSUMMARY2_VCHR;
		internal com.digitalwave.controls.ctlRichTextBox m_txtSUMMARY1_VCHR;
		private System.Windows.Forms.ToolTip m_ttpTextInfo;
		private System.Windows.Forms.ContextMenu mnuRichTextBox;
		private System.Windows.Forms.MenuItem mnuRichTextBoxDelete;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem m_mnuUpTag;
		private System.Windows.Forms.MenuItem m_mnuDownTag;
        private System.Windows.Forms.GroupBox groupBox4;
		internal PinkieControls.ButtonXP m_cmdClear;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.GroupBox groupBox6;
		internal System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage5;
		internal com.digitalwave.controls.ctlRichTextBox m_txtCTResult;
		internal com.digitalwave.controls.ctlRichTextBox m_txtXRayResult;
		internal com.digitalwave.controls.ctlRichTextBox m_txtEKGResult;
		internal com.digitalwave.controls.ctlRichTextBox m_txtBUCResult;
		internal PinkieControls.ButtonXP m_cmdEditTemplate;
		internal com.digitalwave.controls.ctlRichTextBox m_txtDiagnose;
		internal com.digitalwave.controls.ctlRichTextBox m_txtCureCircs;
		internal com.digitalwave.controls.ctlRichTextBox m_txtMRIResult;
		internal System.Windows.Forms.TextBox m_txtREPORT_NO_CHR;
		internal System.Windows.Forms.TextBox m_txtPATIENT_NO_CHR;
		internal System.Windows.Forms.TextBox m_txtINPATIENT_NO_CHR;
		internal System.Windows.Forms.TextBox m_txtBED_NO_CHR;
		internal System.Windows.Forms.TextBox m_txtAGE_FLT;
		internal System.Windows.Forms.TextBox m_txtPATIENT_NAME_VCHR;
		internal System.Windows.Forms.ComboBox m_cmbAge;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mnuUndoDel;
		internal System.Windows.Forms.TextBox carID;
		private System.Windows.Forms.Label label25;

		private readonly DateTime m_dtmEmptyDate = new DateTime(1900,1,1);
        public ListViewBox m_txtREPORTOR_NAME_VCHR;
        public ListViewBox m_txtDEPT_NAME_VCHR;
        public frmRISEEGReportNamage m_objfrmCardiogramReportManage = null;
		public frmRISTCDReport()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
						InitializeComponent();


                        carID.Text = "";
                        m_txtREPORT_NO_CHR.Text = "";
                        m_txtPATIENT_NO_CHR.Text = "";
                        m_txtINPATIENT_NO_CHR.Text = "";
                        m_txtAGE_FLT.Text = "";

						m_cmbAge.SelectedIndex=0;
						m_cboSEX_CHR.SelectedIndex=0;		//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			m_mthInitRichTextBox();
            ((clsController_RISTCDReport)this.objController).m_mthInitData();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRISTCDReport));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_txtREPORTOR_NAME_VCHR = new ListViewBox();
            this.m_txtDEPT_NAME_VCHR = new ListViewBox();
            this.carID = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.m_cmbAge = new System.Windows.Forms.ComboBox();
            this.m_txtPATIENT_NAME_VCHR = new System.Windows.Forms.TextBox();
            this.m_txtAGE_FLT = new System.Windows.Forms.TextBox();
            this.m_txtBED_NO_CHR = new System.Windows.Forms.TextBox();
            this.m_txtINPATIENT_NO_CHR = new System.Windows.Forms.TextBox();
            this.m_txtPATIENT_NO_CHR = new System.Windows.Forms.TextBox();
            this.m_txtREPORT_NO_CHR = new System.Windows.Forms.TextBox();
            this.m_cboSEX_CHR = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_txtSUMMARY1_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.mnuRichTextBox = new System.Windows.Forms.ContextMenu();
            this.mnuRichTextBoxDelete = new System.Windows.Forms.MenuItem();
            this.mnuUndoDel = new System.Windows.Forms.MenuItem();
            this.m_mnuUpTag = new System.Windows.Forms.MenuItem();
            this.m_mnuDownTag = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.m_txtSUMMARY2_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.m_cmdConfirm = new PinkieControls.ButtonXP();
            this.m_cmdPrint = new PinkieControls.ButtonXP();
            this.m_cmdExit = new PinkieControls.ButtonXP();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_cmdEditTemplate = new PinkieControls.ButtonXP();
            this.m_cmdClear = new PinkieControls.ButtonXP();
            this.m_cmdCreateTemplate = new PinkieControls.ButtonXP();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.m_printDoc = new System.Drawing.Printing.PrintDocument();
            this.m_printDlg = new System.Windows.Forms.PrintDialog();
            this.m_ttpTextInfo = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.m_txtDiagnose = new com.digitalwave.controls.ctlRichTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.m_txtCureCircs = new com.digitalwave.controls.ctlRichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_txtCTResult = new com.digitalwave.controls.ctlRichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_txtMRIResult = new com.digitalwave.controls.ctlRichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.m_txtEKGResult = new com.digitalwave.controls.ctlRichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.m_txtXRayResult = new com.digitalwave.controls.ctlRichTextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.m_txtBUCResult = new com.digitalwave.controls.ctlRichTextBox();
            this.m_printPrevDlg = new com.digitalwave.iCare.gui.RIS.TCDPrintPreviewDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_txtREPORTOR_NAME_VCHR);
            this.groupBox1.Controls.Add(this.m_txtDEPT_NAME_VCHR);
            this.groupBox1.Controls.Add(this.carID);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.m_cmbAge);
            this.groupBox1.Controls.Add(this.m_txtPATIENT_NAME_VCHR);
            this.groupBox1.Controls.Add(this.m_txtAGE_FLT);
            this.groupBox1.Controls.Add(this.m_txtBED_NO_CHR);
            this.groupBox1.Controls.Add(this.m_txtINPATIENT_NO_CHR);
            this.groupBox1.Controls.Add(this.m_txtPATIENT_NO_CHR);
            this.groupBox1.Controls.Add(this.m_txtREPORT_NO_CHR);
            this.groupBox1.Controls.Add(this.m_cboSEX_CHR);
            this.groupBox1.Controls.Add(this.label14);
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
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 408);
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
            this.m_txtREPORTOR_NAME_VCHR.Location = new System.Drawing.Point(80, 365);
            this.m_txtREPORTOR_NAME_VCHR.m_IntMaxListLength = 25;
            this.m_txtREPORTOR_NAME_VCHR.m_IsHaveParent = false;
            this.m_txtREPORTOR_NAME_VCHR.m_strParentName = "";
            this.m_txtREPORTOR_NAME_VCHR.Name = "m_txtREPORTOR_NAME_VCHR";
            this.m_txtREPORTOR_NAME_VCHR.SelectedItemBackColor = System.Drawing.Color.ForestGreen;
            this.m_txtREPORTOR_NAME_VCHR.SelectedItemForeColor = System.Drawing.Color.White;
            this.m_txtREPORTOR_NAME_VCHR.Size = new System.Drawing.Size(124, 23);
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
            this.m_txtDEPT_NAME_VCHR.Location = new System.Drawing.Point(80, 240);
            this.m_txtDEPT_NAME_VCHR.m_IntMaxListLength = 25;
            this.m_txtDEPT_NAME_VCHR.m_IsHaveParent = false;
            this.m_txtDEPT_NAME_VCHR.m_strParentName = "";
            this.m_txtDEPT_NAME_VCHR.Name = "m_txtDEPT_NAME_VCHR";
            this.m_txtDEPT_NAME_VCHR.SelectedItemBackColor = System.Drawing.Color.ForestGreen;
            this.m_txtDEPT_NAME_VCHR.SelectedItemForeColor = System.Drawing.Color.White;
            this.m_txtDEPT_NAME_VCHR.Size = new System.Drawing.Size(124, 23);
            this.m_txtDEPT_NAME_VCHR.TabIndex = 8;
            this.m_txtDEPT_NAME_VCHR.txtValuse = "";
            this.m_txtDEPT_NAME_VCHR.VsLeftOrRight = 1;
            // 
            // carID
            // 
            //this.carID.EnableAutoValidation = false;
            //this.carID.EnableEnterKeyValidate = false;
            //this.carID.EnableEscapeKeyUndo = true;
            //this.carID.EnableLastValidValue = true;
            //this.carID.ErrorProvider = null;
            //this.carID.ErrorProviderMessage = "Invalid value";
            //this.carID.ForceFormatText = true;
            this.carID.Location = new System.Drawing.Point(80, 16);
            this.carID.MaxLength = 10;
            this.carID.Name = "carID";
            //this.carID.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
            this.carID.Size = new System.Drawing.Size(124, 23);
            this.carID.TabIndex = 0;
            this.carID.Text = "0";
            this.carID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.carID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.carID_KeyDown);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(8, 16);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(56, 14);
            this.label25.TabIndex = 122;
            this.label25.Text = "卡  号:";
            // 
            // m_cmbAge
            // 
            this.m_cmbAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbAge.Items.AddRange(new object[] {
            "岁",
            "月",
            "天"});
            this.m_cmbAge.Location = new System.Drawing.Point(148, 208);
            this.m_cmbAge.Name = "m_cmbAge";
            this.m_cmbAge.Size = new System.Drawing.Size(56, 22);
            this.m_cmbAge.TabIndex = 7;
            this.m_cmbAge.SelectedIndexChanged += new System.EventHandler(this.m_cmbAge_SelectedIndexChanged);
            this.m_cmbAge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cmbAge_KeyDown);
            // 
            // m_txtPATIENT_NAME_VCHR
            // 
            this.m_txtPATIENT_NAME_VCHR.CausesValidation = false;
            this.m_txtPATIENT_NAME_VCHR.Location = new System.Drawing.Point(80, 148);
            this.m_txtPATIENT_NAME_VCHR.MaxLength = 20;
            this.m_txtPATIENT_NAME_VCHR.Name = "m_txtPATIENT_NAME_VCHR";
            this.m_txtPATIENT_NAME_VCHR.Size = new System.Drawing.Size(124, 23);
            this.m_txtPATIENT_NAME_VCHR.TabIndex = 4;
            this.m_txtPATIENT_NAME_VCHR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtPATIENT_NAME_VCHR_KeyPress);
            // 
            // m_txtAGE_FLT
            // 
            //this.m_txtAGE_FLT.EnableAutoValidation = true;
            //this.m_txtAGE_FLT.EnableEnterKeyValidate = true;
            //this.m_txtAGE_FLT.EnableEscapeKeyUndo = true;
            //this.m_txtAGE_FLT.EnableLastValidValue = true;
            //this.m_txtAGE_FLT.ErrorProvider = null;
            //this.m_txtAGE_FLT.ErrorProviderMessage = "Invalid value";
            //this.m_txtAGE_FLT.ForceFormatText = true;
            this.m_txtAGE_FLT.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.m_txtAGE_FLT.Location = new System.Drawing.Point(80, 208);
            this.m_txtAGE_FLT.MaxLength = 3;
            this.m_txtAGE_FLT.Name = "m_txtAGE_FLT";
            //this.m_txtAGE_FLT.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.None;
            this.m_txtAGE_FLT.Size = new System.Drawing.Size(64, 23);
            this.m_txtAGE_FLT.TabIndex = 6;
            this.m_txtAGE_FLT.Text = "0";
            this.m_txtAGE_FLT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_txtBED_NO_CHR
            // 
            this.m_txtBED_NO_CHR.CausesValidation = false;
            //this.m_txtBED_NO_CHR.EnableAutoValidation = false;
            //this.m_txtBED_NO_CHR.EnableEnterKeyValidate = false;
            //this.m_txtBED_NO_CHR.EnableEscapeKeyUndo = false;
            //this.m_txtBED_NO_CHR.EnableLastValidValue = false;
            //this.m_txtBED_NO_CHR.ErrorProvider = null;
            //this.m_txtBED_NO_CHR.ErrorProviderMessage = "Invalid value";
            //this.m_txtBED_NO_CHR.ForceFormatText = true;
            this.m_txtBED_NO_CHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtBED_NO_CHR.Location = new System.Drawing.Point(80, 272);
            this.m_txtBED_NO_CHR.MaxLength = 10;
            this.m_txtBED_NO_CHR.Name = "m_txtBED_NO_CHR";
            this.m_txtBED_NO_CHR.Size = new System.Drawing.Size(124, 23);
            this.m_txtBED_NO_CHR.TabIndex = 9;
            this.m_txtBED_NO_CHR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_txtINPATIENT_NO_CHR
            // 
            //this.m_txtINPATIENT_NO_CHR.EnableAutoValidation = false;
            //this.m_txtINPATIENT_NO_CHR.EnableEnterKeyValidate = false;
            //this.m_txtINPATIENT_NO_CHR.EnableEscapeKeyUndo = true;
            //this.m_txtINPATIENT_NO_CHR.EnableLastValidValue = true;
            //this.m_txtINPATIENT_NO_CHR.ErrorProvider = null;
            //this.m_txtINPATIENT_NO_CHR.ErrorProviderMessage = "Invalid value";
            //this.m_txtINPATIENT_NO_CHR.ForceFormatText = true;
            this.m_txtINPATIENT_NO_CHR.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.m_txtINPATIENT_NO_CHR.Location = new System.Drawing.Point(80, 116);
            this.m_txtINPATIENT_NO_CHR.MaxLength = 15;
            this.m_txtINPATIENT_NO_CHR.Name = "m_txtINPATIENT_NO_CHR";
            //this.m_txtINPATIENT_NO_CHR.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol;
            this.m_txtINPATIENT_NO_CHR.Size = new System.Drawing.Size(124, 23);
            this.m_txtINPATIENT_NO_CHR.TabIndex = 3;
            this.m_txtINPATIENT_NO_CHR.Text = "0";
            this.m_txtINPATIENT_NO_CHR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.m_txtPATIENT_NO_CHR.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.m_txtPATIENT_NO_CHR.Location = new System.Drawing.Point(80, 84);
            this.m_txtPATIENT_NO_CHR.MaxLength = 10;
            this.m_txtPATIENT_NO_CHR.Name = "m_txtPATIENT_NO_CHR";
            //this.m_txtPATIENT_NO_CHR.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol;
            this.m_txtPATIENT_NO_CHR.Size = new System.Drawing.Size(124, 23);
            this.m_txtPATIENT_NO_CHR.TabIndex = 2;
            this.m_txtPATIENT_NO_CHR.Text = "0";
            this.m_txtPATIENT_NO_CHR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.m_txtREPORT_NO_CHR.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.m_txtREPORT_NO_CHR.Location = new System.Drawing.Point(80, 52);
            this.m_txtREPORT_NO_CHR.MaxLength = 10;
            this.m_txtREPORT_NO_CHR.Name = "m_txtREPORT_NO_CHR";
            //this.m_txtREPORT_NO_CHR.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol;
            this.m_txtREPORT_NO_CHR.Size = new System.Drawing.Size(124, 23);
            this.m_txtREPORT_NO_CHR.TabIndex = 1;
            this.m_txtREPORT_NO_CHR.Text = "0";
            this.m_txtREPORT_NO_CHR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_cboSEX_CHR
            // 
            this.m_cboSEX_CHR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSEX_CHR.Items.AddRange(new object[] {
            "男",
            "女",
            "不详"});
            this.m_cboSEX_CHR.Location = new System.Drawing.Point(80, 180);
            this.m_cboSEX_CHR.Name = "m_cboSEX_CHR";
            this.m_cboSEX_CHR.Size = new System.Drawing.Size(124, 22);
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
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(8, 272);
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
            this.label19.Location = new System.Drawing.Point(8, 184);
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
            this.label17.Location = new System.Drawing.Point(8, 120);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(70, 14);
            this.label17.TabIndex = 20;
            this.label17.Text = "住 院 号:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 88);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 14);
            this.label16.TabIndex = 19;
            this.label16.Text = "门 诊 号:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 56);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 14);
            this.label15.TabIndex = 18;
            this.label15.Text = "TCD号:";
            // 
            // m_dtpCHECK_DAT
            // 
            this.m_dtpCHECK_DAT.CustomFormat = "yyyy-MM-dd hh:mm:ss";
            this.m_dtpCHECK_DAT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpCHECK_DAT.Location = new System.Drawing.Point(80, 304);
            this.m_dtpCHECK_DAT.Name = "m_dtpCHECK_DAT";
            this.m_dtpCHECK_DAT.Size = new System.Drawing.Size(124, 23);
            this.m_dtpCHECK_DAT.TabIndex = 10;
            // 
            // m_dtpREPORT_DAT
            // 
            this.m_dtpREPORT_DAT.CustomFormat = "yyyy-MM-dd hh:mm:ss";
            this.m_dtpREPORT_DAT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpREPORT_DAT.Location = new System.Drawing.Point(80, 336);
            this.m_dtpREPORT_DAT.Name = "m_dtpREPORT_DAT";
            this.m_dtpREPORT_DAT.Size = new System.Drawing.Size(124, 23);
            this.m_dtpREPORT_DAT.TabIndex = 11;
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
            this.label13.Location = new System.Drawing.Point(8, 308);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 14);
            this.label13.TabIndex = 15;
            this.label13.Text = "检查日期:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_txtSUMMARY1_VCHR);
            this.groupBox2.Location = new System.Drawing.Point(248, 328);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(720, 204);
            this.groupBox2.TabIndex = 230;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "描述";
            // 
            // m_txtSUMMARY1_VCHR
            // 
            this.m_txtSUMMARY1_VCHR.AccessibleDescription = "描述";
            this.m_txtSUMMARY1_VCHR.BackColor = System.Drawing.Color.White;
            this.m_txtSUMMARY1_VCHR.ContextMenu = this.mnuRichTextBox;
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
            this.m_txtSUMMARY1_VCHR.Size = new System.Drawing.Size(696, 168);
            this.m_txtSUMMARY1_VCHR.TabIndex = 231;
            this.m_txtSUMMARY1_VCHR.Text = "";
            // 
            // mnuRichTextBox
            // 
            this.mnuRichTextBox.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuRichTextBoxDelete,
            this.mnuUndoDel,
            this.m_mnuUpTag,
            this.m_mnuDownTag,
            this.menuItem1});
            // 
            // mnuRichTextBoxDelete
            // 
            this.mnuRichTextBoxDelete.Index = 0;
            this.mnuRichTextBoxDelete.Text = "删除(&D)";
            this.mnuRichTextBoxDelete.Click += new System.EventHandler(this.mnuRichTextBoxDelete_Click);
            // 
            // mnuUndoDel
            // 
            this.mnuUndoDel.Index = 1;
            this.mnuUndoDel.Text = "撤消删除";
            this.mnuUndoDel.Click += new System.EventHandler(this.mnuUndoDel_Click);
            // 
            // m_mnuUpTag
            // 
            this.m_mnuUpTag.Index = 2;
            this.m_mnuUpTag.Text = "上标(&U)";
            this.m_mnuUpTag.Click += new System.EventHandler(this.m_mnuUpTag_Click);
            // 
            // m_mnuDownTag
            // 
            this.m_mnuDownTag.Index = 3;
            this.m_mnuDownTag.Text = "下标(&W)";
            this.m_mnuDownTag.Click += new System.EventHandler(this.m_mnuDownTag_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.Text = "撤消上下标(&G)";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // m_txtSUMMARY2_VCHR
            // 
            this.m_txtSUMMARY2_VCHR.AccessibleDescription = "印象";
            this.m_txtSUMMARY2_VCHR.ContextMenu = this.mnuRichTextBox;
            this.m_txtSUMMARY2_VCHR.Font = new System.Drawing.Font("宋体", 14F);
            this.m_txtSUMMARY2_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtSUMMARY2_VCHR.Location = new System.Drawing.Point(16, 16);
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
            this.m_txtSUMMARY2_VCHR.Size = new System.Drawing.Size(692, 156);
            this.m_txtSUMMARY2_VCHR.TabIndex = 241;
            this.m_txtSUMMARY2_VCHR.Text = "";
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(12, 16);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(184, 32);
            this.m_cmdSave.TabIndex = 251;
            this.m_cmdSave.Text = "保      存";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // m_cmdConfirm
            // 
            this.m_cmdConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdConfirm.DefaultScheme = true;
            this.m_cmdConfirm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdConfirm.Hint = "";
            this.m_cmdConfirm.Location = new System.Drawing.Point(12, 88);
            this.m_cmdConfirm.Name = "m_cmdConfirm";
            this.m_cmdConfirm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdConfirm.Size = new System.Drawing.Size(184, 32);
            this.m_cmdConfirm.TabIndex = 253;
            this.m_cmdConfirm.Text = "审      核";
            this.m_cmdConfirm.Click += new System.EventHandler(this.m_cmdConfirm_Click);
            // 
            // m_cmdPrint
            // 
            this.m_cmdPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdPrint.DefaultScheme = true;
            this.m_cmdPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdPrint.Hint = "";
            this.m_cmdPrint.Location = new System.Drawing.Point(12, 124);
            this.m_cmdPrint.Name = "m_cmdPrint";
            this.m_cmdPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdPrint.Size = new System.Drawing.Size(184, 32);
            this.m_cmdPrint.TabIndex = 254;
            this.m_cmdPrint.Text = "打      印";
            this.m_cmdPrint.Click += new System.EventHandler(this.m_cmdPrint_Click);
            // 
            // m_cmdExit
            // 
            this.m_cmdExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdExit.DefaultScheme = true;
            this.m_cmdExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdExit.Hint = "";
            this.m_cmdExit.Location = new System.Drawing.Point(12, 268);
            this.m_cmdExit.Name = "m_cmdExit";
            this.m_cmdExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdExit.Size = new System.Drawing.Size(184, 32);
            this.m_cmdExit.TabIndex = 258;
            this.m_cmdExit.Text = "退      出";
            this.m_cmdExit.Click += new System.EventHandler(this.m_cmdExit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_cmdEditTemplate);
            this.groupBox3.Controls.Add(this.m_cmdClear);
            this.groupBox3.Controls.Add(this.m_cmdCreateTemplate);
            this.groupBox3.Controls.Add(this.m_cmdDelete);
            this.groupBox3.Controls.Add(this.m_cmdSave);
            this.groupBox3.Controls.Add(this.m_cmdConfirm);
            this.groupBox3.Controls.Add(this.m_cmdPrint);
            this.groupBox3.Controls.Add(this.m_cmdExit);
            this.groupBox3.Location = new System.Drawing.Point(8, 416);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(224, 304);
            this.groupBox3.TabIndex = 250;
            this.groupBox3.TabStop = false;
            // 
            // m_cmdEditTemplate
            // 
            this.m_cmdEditTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdEditTemplate.DefaultScheme = true;
            this.m_cmdEditTemplate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdEditTemplate.Hint = "";
            this.m_cmdEditTemplate.Location = new System.Drawing.Point(12, 196);
            this.m_cmdEditTemplate.Name = "m_cmdEditTemplate";
            this.m_cmdEditTemplate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdEditTemplate.Size = new System.Drawing.Size(184, 32);
            this.m_cmdEditTemplate.TabIndex = 256;
            this.m_cmdEditTemplate.Text = "修 改 模 板";
            this.m_cmdEditTemplate.Click += new System.EventHandler(this.m_cmdEditTemplate_Click);
            // 
            // m_cmdClear
            // 
            this.m_cmdClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdClear.DefaultScheme = true;
            this.m_cmdClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdClear.Hint = "";
            this.m_cmdClear.Location = new System.Drawing.Point(12, 232);
            this.m_cmdClear.Name = "m_cmdClear";
            this.m_cmdClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdClear.Size = new System.Drawing.Size(184, 32);
            this.m_cmdClear.TabIndex = 257;
            this.m_cmdClear.Text = "清      空";
            this.m_cmdClear.Click += new System.EventHandler(this.m_cmdClear_Click);
            // 
            // m_cmdCreateTemplate
            // 
            this.m_cmdCreateTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdCreateTemplate.DefaultScheme = true;
            this.m_cmdCreateTemplate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdCreateTemplate.Hint = "";
            this.m_cmdCreateTemplate.Location = new System.Drawing.Point(12, 160);
            this.m_cmdCreateTemplate.Name = "m_cmdCreateTemplate";
            this.m_cmdCreateTemplate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCreateTemplate.Size = new System.Drawing.Size(184, 32);
            this.m_cmdCreateTemplate.TabIndex = 255;
            this.m_cmdCreateTemplate.Text = "生 成 模 板";
            this.m_cmdCreateTemplate.Click += new System.EventHandler(this.m_cmdCreateTemplate_Click);
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(12, 52);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete.Size = new System.Drawing.Size(184, 32);
            this.m_cmdDelete.TabIndex = 252;
            this.m_cmdDelete.Text = "删      除";
            this.m_cmdDelete.Click += new System.EventHandler(this.m_cmdDelete_Click);
            // 
            // m_printDoc
            // 
            this.m_printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_printDoc_PrintPage);
            this.m_printDoc.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_printDoc_EndPrint);
            this.m_printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_printDoc_BeginPrint);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.m_txtSUMMARY2_VCHR);
            this.groupBox4.Location = new System.Drawing.Point(248, 536);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(720, 180);
            this.groupBox4.TabIndex = 240;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "印象";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.m_txtDiagnose);
            this.groupBox5.Location = new System.Drawing.Point(248, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(720, 72);
            this.groupBox5.TabIndex = 200;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "临床诊断";
            // 
            // m_txtDiagnose
            // 
            this.m_txtDiagnose.AccessibleDescription = "描述";
            this.m_txtDiagnose.BackColor = System.Drawing.Color.White;
            this.m_txtDiagnose.ContextMenu = this.mnuRichTextBox;
            this.m_txtDiagnose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDiagnose.ForeColor = System.Drawing.Color.Black;
            this.m_txtDiagnose.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtDiagnose.Location = new System.Drawing.Point(16, 20);
            this.m_txtDiagnose.m_BlnIgnoreUserInfo = true;
            this.m_txtDiagnose.m_BlnPartControl = false;
            this.m_txtDiagnose.m_BlnReadOnly = false;
            this.m_txtDiagnose.m_BlnUnderLineDST = false;
            this.m_txtDiagnose.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtDiagnose.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtDiagnose.m_IntCanModifyTime = 6;
            this.m_txtDiagnose.m_IntPartControlLength = 0;
            this.m_txtDiagnose.m_IntPartControlStartIndex = 0;
            this.m_txtDiagnose.m_StrUserID = "";
            this.m_txtDiagnose.m_StrUserName = "";
            this.m_txtDiagnose.MaxLength = 50;
            this.m_txtDiagnose.Name = "m_txtDiagnose";
            this.m_txtDiagnose.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtDiagnose.Size = new System.Drawing.Size(692, 44);
            this.m_txtDiagnose.TabIndex = 201;
            this.m_txtDiagnose.Text = "";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.m_txtCureCircs);
            this.groupBox6.Location = new System.Drawing.Point(248, 84);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(720, 92);
            this.groupBox6.TabIndex = 210;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "治疗情况";
            // 
            // m_txtCureCircs
            // 
            this.m_txtCureCircs.AccessibleDescription = "描述";
            this.m_txtCureCircs.BackColor = System.Drawing.Color.White;
            this.m_txtCureCircs.ContextMenu = this.mnuRichTextBox;
            this.m_txtCureCircs.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtCureCircs.ForeColor = System.Drawing.Color.Black;
            this.m_txtCureCircs.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtCureCircs.Location = new System.Drawing.Point(12, 20);
            this.m_txtCureCircs.m_BlnIgnoreUserInfo = true;
            this.m_txtCureCircs.m_BlnPartControl = false;
            this.m_txtCureCircs.m_BlnReadOnly = false;
            this.m_txtCureCircs.m_BlnUnderLineDST = false;
            this.m_txtCureCircs.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtCureCircs.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtCureCircs.m_IntCanModifyTime = 6;
            this.m_txtCureCircs.m_IntPartControlLength = 0;
            this.m_txtCureCircs.m_IntPartControlStartIndex = 0;
            this.m_txtCureCircs.m_StrUserID = "";
            this.m_txtCureCircs.m_StrUserName = "";
            this.m_txtCureCircs.MaxLength = 1000;
            this.m_txtCureCircs.Name = "m_txtCureCircs";
            this.m_txtCureCircs.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtCureCircs.Size = new System.Drawing.Size(692, 64);
            this.m_txtCureCircs.TabIndex = 211;
            this.m_txtCureCircs.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(248, 184);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(720, 140);
            this.tabControl1.TabIndex = 220;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_txtCTResult);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(712, 113);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "CT";
            // 
            // m_txtCTResult
            // 
            this.m_txtCTResult.AccessibleDescription = "描述";
            this.m_txtCTResult.BackColor = System.Drawing.Color.White;
            this.m_txtCTResult.ContextMenu = this.mnuRichTextBox;
            this.m_txtCTResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtCTResult.ForeColor = System.Drawing.Color.Black;
            this.m_txtCTResult.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtCTResult.Location = new System.Drawing.Point(8, 8);
            this.m_txtCTResult.m_BlnIgnoreUserInfo = true;
            this.m_txtCTResult.m_BlnPartControl = false;
            this.m_txtCTResult.m_BlnReadOnly = false;
            this.m_txtCTResult.m_BlnUnderLineDST = false;
            this.m_txtCTResult.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtCTResult.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtCTResult.m_IntCanModifyTime = 6;
            this.m_txtCTResult.m_IntPartControlLength = 0;
            this.m_txtCTResult.m_IntPartControlStartIndex = 0;
            this.m_txtCTResult.m_StrUserID = "";
            this.m_txtCTResult.m_StrUserName = "";
            this.m_txtCTResult.MaxLength = 50;
            this.m_txtCTResult.Name = "m_txtCTResult";
            this.m_txtCTResult.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtCTResult.Size = new System.Drawing.Size(692, 100);
            this.m_txtCTResult.TabIndex = 221;
            this.m_txtCTResult.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_txtMRIResult);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(712, 115);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "MRI";
            // 
            // m_txtMRIResult
            // 
            this.m_txtMRIResult.AccessibleDescription = "描述";
            this.m_txtMRIResult.BackColor = System.Drawing.Color.White;
            this.m_txtMRIResult.ContextMenu = this.mnuRichTextBox;
            this.m_txtMRIResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtMRIResult.ForeColor = System.Drawing.Color.Black;
            this.m_txtMRIResult.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtMRIResult.Location = new System.Drawing.Point(10, 8);
            this.m_txtMRIResult.m_BlnIgnoreUserInfo = true;
            this.m_txtMRIResult.m_BlnPartControl = false;
            this.m_txtMRIResult.m_BlnReadOnly = false;
            this.m_txtMRIResult.m_BlnUnderLineDST = false;
            this.m_txtMRIResult.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtMRIResult.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtMRIResult.m_IntCanModifyTime = 6;
            this.m_txtMRIResult.m_IntPartControlLength = 0;
            this.m_txtMRIResult.m_IntPartControlStartIndex = 0;
            this.m_txtMRIResult.m_StrUserID = "";
            this.m_txtMRIResult.m_StrUserName = "";
            this.m_txtMRIResult.MaxLength = 1000;
            this.m_txtMRIResult.Name = "m_txtMRIResult";
            this.m_txtMRIResult.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtMRIResult.Size = new System.Drawing.Size(692, 96);
            this.m_txtMRIResult.TabIndex = 222;
            this.m_txtMRIResult.Text = "";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.m_txtEKGResult);
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(712, 115);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "心电图";
            // 
            // m_txtEKGResult
            // 
            this.m_txtEKGResult.AccessibleDescription = "描述";
            this.m_txtEKGResult.BackColor = System.Drawing.Color.White;
            this.m_txtEKGResult.ContextMenu = this.mnuRichTextBox;
            this.m_txtEKGResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtEKGResult.ForeColor = System.Drawing.Color.Black;
            this.m_txtEKGResult.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtEKGResult.Location = new System.Drawing.Point(8, 8);
            this.m_txtEKGResult.m_BlnIgnoreUserInfo = true;
            this.m_txtEKGResult.m_BlnPartControl = false;
            this.m_txtEKGResult.m_BlnReadOnly = false;
            this.m_txtEKGResult.m_BlnUnderLineDST = false;
            this.m_txtEKGResult.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtEKGResult.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtEKGResult.m_IntCanModifyTime = 6;
            this.m_txtEKGResult.m_IntPartControlLength = 0;
            this.m_txtEKGResult.m_IntPartControlStartIndex = 0;
            this.m_txtEKGResult.m_StrUserID = "";
            this.m_txtEKGResult.m_StrUserName = "";
            this.m_txtEKGResult.MaxLength = 1000;
            this.m_txtEKGResult.Name = "m_txtEKGResult";
            this.m_txtEKGResult.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtEKGResult.Size = new System.Drawing.Size(692, 96);
            this.m_txtEKGResult.TabIndex = 224;
            this.m_txtEKGResult.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_txtXRayResult);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(712, 115);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "X光";
            // 
            // m_txtXRayResult
            // 
            this.m_txtXRayResult.AccessibleDescription = "描述";
            this.m_txtXRayResult.BackColor = System.Drawing.Color.White;
            this.m_txtXRayResult.ContextMenu = this.mnuRichTextBox;
            this.m_txtXRayResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtXRayResult.ForeColor = System.Drawing.Color.Black;
            this.m_txtXRayResult.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtXRayResult.Location = new System.Drawing.Point(8, 8);
            this.m_txtXRayResult.m_BlnIgnoreUserInfo = true;
            this.m_txtXRayResult.m_BlnPartControl = false;
            this.m_txtXRayResult.m_BlnReadOnly = false;
            this.m_txtXRayResult.m_BlnUnderLineDST = false;
            this.m_txtXRayResult.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtXRayResult.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtXRayResult.m_IntCanModifyTime = 6;
            this.m_txtXRayResult.m_IntPartControlLength = 0;
            this.m_txtXRayResult.m_IntPartControlStartIndex = 0;
            this.m_txtXRayResult.m_StrUserID = "";
            this.m_txtXRayResult.m_StrUserName = "";
            this.m_txtXRayResult.MaxLength = 1000;
            this.m_txtXRayResult.Name = "m_txtXRayResult";
            this.m_txtXRayResult.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtXRayResult.Size = new System.Drawing.Size(692, 96);
            this.m_txtXRayResult.TabIndex = 223;
            this.m_txtXRayResult.Text = "";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.m_txtBUCResult);
            this.tabPage5.Location = new System.Drawing.Point(4, 21);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(712, 115);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "B超";
            // 
            // m_txtBUCResult
            // 
            this.m_txtBUCResult.AccessibleDescription = "描述";
            this.m_txtBUCResult.BackColor = System.Drawing.Color.White;
            this.m_txtBUCResult.ContextMenu = this.mnuRichTextBox;
            this.m_txtBUCResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtBUCResult.ForeColor = System.Drawing.Color.Black;
            this.m_txtBUCResult.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtBUCResult.Location = new System.Drawing.Point(8, 8);
            this.m_txtBUCResult.m_BlnIgnoreUserInfo = true;
            this.m_txtBUCResult.m_BlnPartControl = false;
            this.m_txtBUCResult.m_BlnReadOnly = false;
            this.m_txtBUCResult.m_BlnUnderLineDST = false;
            this.m_txtBUCResult.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtBUCResult.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtBUCResult.m_IntCanModifyTime = 6;
            this.m_txtBUCResult.m_IntPartControlLength = 0;
            this.m_txtBUCResult.m_IntPartControlStartIndex = 0;
            this.m_txtBUCResult.m_StrUserID = "";
            this.m_txtBUCResult.m_StrUserName = "";
            this.m_txtBUCResult.MaxLength = 1000;
            this.m_txtBUCResult.Name = "m_txtBUCResult";
            this.m_txtBUCResult.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtBUCResult.Size = new System.Drawing.Size(692, 96);
            this.m_txtBUCResult.TabIndex = 225;
            this.m_txtBUCResult.Text = "";
            // 
            // m_printPrevDlg
            // 
            this.m_printPrevDlg.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.m_printPrevDlg.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.m_printPrevDlg.ClientSize = new System.Drawing.Size(800, 600);
            this.m_printPrevDlg.Enabled = true;
            this.m_printPrevDlg.Icon = ((System.Drawing.Icon)(resources.GetObject("m_printPrevDlg.Icon")));
            this.m_printPrevDlg.Name = "m_printPrevDlg";
            this.m_printPrevDlg.Visible = false;
            // 
            // frmRISTCDReport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1016, 733);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRISTCDReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCD报告单";
            this.Load += new System.EventHandler(this.frmRISTCDReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRISTCDReport_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			this.objController = new com.digitalwave.iCare.gui.RIS.clsController_RISTCDReport(this);
			objController.Set_GUI_Apperance(this);
		}

		private void frmRISTCDReport_Load(object sender, System.EventArgs e)
		{
		
			m_mthSetFormControlCanBeNull(this);

			m_mthSetEnter2Tab(new System.Windows.Forms.Control[]
					{m_txtSUMMARY1_VCHR,m_txtSUMMARY2_VCHR,m_txtDEPT_NAME_VCHR,m_txtREPORTOR_NAME_VCHR});
		}

		public void m_mthSetReport(clsRIS_TCD_REPORT_VO p_objItem)
		{
			((clsController_RISTCDReport)this.objController).m_mthSetReport(p_objItem);
		}

		private void m_cmdExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void m_cmdSave_Click(object sender, System.EventArgs e)
		{
			((clsController_RISTCDReport)this.objController).m_mthDoSave();
		}

		private void m_cmdDelete_Click(object sender, System.EventArgs e)
		{
			((clsController_RISTCDReport)this.objController).m_mthDoDelete();
			
		}

		private void m_cmdConfirm_Click(object sender, System.EventArgs e)
		{
			((clsController_RISTCDReport)this.objController).m_mthDoConfirm();
		}

		private void m_cmdPrint_Click(object sender, System.EventArgs e)
		{
			((clsController_RISTCDReport)this.objController).m_mthPrintReport(this);
		}

		private void m_printDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
		}

		private void m_printDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
		}

		private void m_printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			((clsController_RISTCDReport)this.objController).m_mthPrintDocPrintPage(e);
		}

		private void m_cmdCreateTemplate_Click(object sender, System.EventArgs e)
		{
			((clsController_RISTCDReport)this.objController).m_mthCreateTemplate();
		}

		public void m_mthSetParentApperance(com.digitalwave.iCare.gui.RIS.frmRISEEGReportNamage infrmCardiogramReportManage)
		{
			((clsController_RISTCDReport)this.objController).SetParentApperance(infrmCardiogramReportManage);
		}


		private void m_mthInitRichTextBox()
		{

			ctlRichTextBox.m_ClrDefaultViewText=Color.Black;
			ctlRichTextBox[] rtbArr  =new ctlRichTextBox[]{m_txtSUMMARY1_VCHR ,m_txtSUMMARY2_VCHR };
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

		private void frmRISTCDReport_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			m_mthSetKeyTab(e);
		}

		private void m_cmdClear_Click(object sender, System.EventArgs e)
		{
			((clsController_RISTCDReport)this.objController).m_mthClear();
            this.m_cmdSave.Enabled = true;
		}

		private void m_cmdEditTemplate_Click(object sender, System.EventArgs e)
		{
            ((clsController_RISTCDReport)this.objController).m_mthEditTemplate();
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

		private void m_txtPATIENT_NAME_VCHR_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		}

		private void m_cmbAge_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Down)
			{
				if(this.m_cmbAge.SelectedIndex==2)
				{
					this.m_cmbAge.SelectedIndex=0;
					e.Handled=true;
				}
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

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			if(this.ActiveControl is com.digitalwave.controls.ctlRichTextBox)
			{
				((com.digitalwave.controls.ctlRichTextBox)(this.ActiveControl)).m_mthUndoSuperSubScript();
			}
		}

		private void mnuUndoDel_Click(object sender, System.EventArgs e)
		{
			if(this.ActiveControl is com.digitalwave.controls.ctlRichTextBox)
			{
				((com.digitalwave.controls.ctlRichTextBox)(this.ActiveControl)).m_mthSelectionDoubleStrikeThough(false);
			}
		}

		private void m_cmbAge_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch(m_cmbAge.SelectedIndex)
			{
				case 0:
					m_cmbAge.Tag="C";
					break;
				case 1:
					m_cmbAge.Tag="B";
					break;
				case 2:
					m_cmbAge.Tag="A";
					break;
			}
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
				((clsController_RISTCDReport)this.objController).m_lngGetPat();
			}
		}

		private void m_txtDEPT_NAME_VCHR_evtValueChanged(object sender, com.digitalwave.Utility.clsExValueChangedEventArgs e)
		{
			m_txtREPORTOR_NAME_VCHR.Tag=m_txtDEPT_NAME_VCHR.Tag;
		}

        private void m_txtINPATIENT_NO_CHR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsController_RISTCDReport)this.objController).m_lngGetPatByInPatientID();
            }
        }

		
	}


	
}
