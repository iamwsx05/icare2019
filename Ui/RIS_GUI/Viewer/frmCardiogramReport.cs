using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PinkieControls;
using weCare.Core.Entity; 
using com.digitalwave.controls;			//digitalwavecontrol.dll
using System.Data;
using com.digitalwave.emr.BEDExplorer;
using com.digitalwave.Emr.StaticObject;

namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// frmLisDeviceManage 的摘要说明。
	/// </summary>
	public class frmCardiogramReport : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
	{
		#region Define
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
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
		private System.Windows.Forms.GroupBox groupBox3;
		internal PinkieControls.ButtonXP m_cmdExit;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label23;
		internal System.Windows.Forms.TextBox m_txtBED_NO_CHR;
		internal System.Windows.Forms.DateTimePicker m_dtpCHECK_DAT;
		internal System.Windows.Forms.DateTimePicker m_dtpREPORT_DAT;
		internal System.Windows.Forms.ComboBox m_txtRHYTHM_VCHR;
		internal PinkieControls.ButtonXP m_cmdConfirm;
		internal PinkieControls.ButtonXP m_cmdPrint;
        internal PinkieControls.ButtonXP m_cmdDelete;

		#endregion
        public ButtonXP m_cmdSave;
		internal System.Windows.Forms.ComboBox m_cboSEX_CHR;
		internal System.Windows.Forms.TextBox m_txtPATIENT_NO_CHR;
		internal System.Windows.Forms.TextBox m_txtINPATIENT_NO_CHR;
		internal System.Windows.Forms.PrintPreviewDialog m_printPrevDlg;
		internal System.Drawing.Printing.PrintDocument m_printDoc;
		internal clsPrint_RISCardiogramReport objRISCardiogramReport = new clsPrint_RISCardiogramReport();
		internal System.Windows.Forms.PrintDialog m_printDlg;
		internal PinkieControls.ButtonXP m_cmdCreateTemplate;
		internal com.digitalwave.controls.ctlRichTextBox  m_txtSUMMARY2_VCHR;
		internal com.digitalwave.controls.ctlRichTextBox m_txtSUMMARY1_VCHR;
		private System.Windows.Forms.ToolTip m_ttpTextInfo;
		private System.Windows.Forms.ContextMenu mnuRichTextBox;
		private System.Windows.Forms.MenuItem mnuRichTextBoxDelete;
        private System.ComponentModel.IContainer components;
		internal PinkieControls.ButtonXP m_cmdClear;
		internal System.Windows.Forms.TextBox m_txtREPORT_NO_CHR;
		internal PinkieControls.ButtonXP m_cmdEditTemplate;
		internal System.Windows.Forms.TextBox m_txtPATIENT_NAME_VCHR;
		internal System.Windows.Forms.ComboBox m_cmbAge;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.GroupBox groupBox2;
		internal System.Windows.Forms.CheckBox m_cheIsSpical;
		internal System.Windows.Forms.TextBox m_txtAGE_FLT;
		internal System.Windows.Forms.TextBox m_txtSubAGE_FLT;
		internal System.Windows.Forms.Label m_labSubAge;
		private System.Windows.Forms.MenuItem meuUndoDel;
        internal System.Windows.Forms.CheckBox m_cheIsNew;
		private System.Windows.Forms.Label label25;
		internal System.Windows.Forms.TextBox m_txtHEART_ROOM_VCHR;
		internal System.Windows.Forms.TextBox m_txtP_R_VCHR;
		internal System.Windows.Forms.TextBox m_txtHEART_RATE_VCHR;
		internal System.Windows.Forms.TextBox m_txtQRS_VCHR;
		internal System.Windows.Forms.TextBox m_txtQ_T_VCHR;
        internal ctlRichTextBox m_txt_E_Axes;
        private Label m_lbl_E_Axes;
        public string m_strApplyID;
        internal ButtonXP m_btnDisplayApplyOrder;
        public string m_strDoctorName = "";
        public ListViewBox m_txtDept;
        public ListViewBox m_txtDoctor;
        public ListViewBox m_txtApplyDoctor;
        private Label label26;
        public PictureBox m_picLog;
        internal clsCardTextBox carID;
		private readonly DateTime m_dtmEmptyDate = new DateTime(1900,1,1);
        //public frmCardiogramReportManage m_objMainFormManage = null;
		public frmCardiogramReport()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
            ((clsController_RISCardiogramReport)this.objController).m_mthInitData();
            carID.Text = "";
            m_txtPATIENT_NO_CHR.Text = "";
            m_txtSubAGE_FLT.Text = "";

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCardiogramReport));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.carID = new com.digitalwave.controls.clsCardTextBox();
            this.m_txtApplyDoctor = new ListViewBox();
            this.m_txtDoctor = new ListViewBox();
            this.label26 = new System.Windows.Forms.Label();
            this.m_txtDept = new  ListViewBox();
            this.label25 = new System.Windows.Forms.Label();
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtRHYTHM_VCHR = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_picLog = new System.Windows.Forms.PictureBox();
            this.m_txtQ_T_VCHR = new System.Windows.Forms.TextBox();
            this.m_txt_E_Axes = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtQRS_VCHR = new System.Windows.Forms.TextBox();
            this.m_txtHEART_RATE_VCHR = new System.Windows.Forms.TextBox();
            this.m_txtP_R_VCHR = new System.Windows.Forms.TextBox();
            this.mnuRichTextBox = new System.Windows.Forms.ContextMenu();
            this.mnuRichTextBoxDelete = new System.Windows.Forms.MenuItem();
            this.meuUndoDel = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.m_txtHEART_ROOM_VCHR = new System.Windows.Forms.TextBox();
            this.m_cheIsNew = new System.Windows.Forms.CheckBox();
            this.label24 = new System.Windows.Forms.Label();
            this.m_txtSUMMARY1_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.m_txtSUMMARY2_VCHR = new com.digitalwave.controls.ctlRichTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.m_lbl_E_Axes = new System.Windows.Forms.Label();
            this.m_cheIsSpical = new System.Windows.Forms.CheckBox();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.m_cmdConfirm = new PinkieControls.ButtonXP();
            this.m_cmdPrint = new PinkieControls.ButtonXP();
            this.m_cmdExit = new PinkieControls.ButtonXP();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_cmdClear = new PinkieControls.ButtonXP();
            this.m_cmdCreateTemplate = new PinkieControls.ButtonXP();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.m_btnDisplayApplyOrder = new PinkieControls.ButtonXP();
            this.m_cmdEditTemplate = new PinkieControls.ButtonXP();
            this.m_printPrevDlg = new System.Windows.Forms.PrintPreviewDialog();
            this.m_printDoc = new System.Drawing.Printing.PrintDocument();
            this.m_printDlg = new System.Windows.Forms.PrintDialog();
            this.m_ttpTextInfo = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picLog)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.carID);
            this.groupBox1.Controls.Add(this.m_txtApplyDoctor);
            this.groupBox1.Controls.Add(this.m_txtDoctor);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.m_txtDept);
            this.groupBox1.Controls.Add(this.label25);
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
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 416);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本资料";
            // 
            // carID
            // 
            this.carID.Location = new System.Drawing.Point(76, 24);
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
            // m_txtApplyDoctor
            // 
            this.m_txtApplyDoctor.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtApplyDoctor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtApplyDoctor.intHeight = 200;
            this.m_txtApplyDoctor.IsEnterShow = true;
            this.m_txtApplyDoctor.isHide = 3;
            this.m_txtApplyDoctor.IsTextChangeHideListView = false;
            this.m_txtApplyDoctor.isTxt = 1;
            this.m_txtApplyDoctor.isUpOrDn = 0;
            this.m_txtApplyDoctor.isValuse = 3;
            this.m_txtApplyDoctor.Location = new System.Drawing.Point(76, 284);
            this.m_txtApplyDoctor.m_IntMaxListLength = 25;
            this.m_txtApplyDoctor.m_IsHaveParent = false;
            this.m_txtApplyDoctor.m_strParentName = "";
            this.m_txtApplyDoctor.Name = "m_txtApplyDoctor";
            this.m_txtApplyDoctor.SelectedItemBackColor = System.Drawing.Color.ForestGreen;
            this.m_txtApplyDoctor.SelectedItemForeColor = System.Drawing.Color.White;
            this.m_txtApplyDoctor.Size = new System.Drawing.Size(184, 23);
            this.m_txtApplyDoctor.TabIndex = 11;
            this.m_txtApplyDoctor.txtValuse = "";
            this.m_txtApplyDoctor.VsLeftOrRight = 1;
            // 
            // m_txtDoctor
            // 
            this.m_txtDoctor.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtDoctor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDoctor.intHeight = 200;
            this.m_txtDoctor.IsEnterShow = true;
            this.m_txtDoctor.isHide = 3;
            this.m_txtDoctor.IsTextChangeHideListView = false;
            this.m_txtDoctor.isTxt = 1;
            this.m_txtDoctor.isUpOrDn = 0;
            this.m_txtDoctor.isValuse = 3;
            this.m_txtDoctor.Location = new System.Drawing.Point(76, 372);
            this.m_txtDoctor.m_IntMaxListLength = 25;
            this.m_txtDoctor.m_IsHaveParent = false;
            this.m_txtDoctor.m_strParentName = "";
            this.m_txtDoctor.Name = "m_txtDoctor";
            this.m_txtDoctor.SelectedItemBackColor = System.Drawing.Color.ForestGreen;
            this.m_txtDoctor.SelectedItemForeColor = System.Drawing.Color.White;
            this.m_txtDoctor.Size = new System.Drawing.Size(184, 23);
            this.m_txtDoctor.TabIndex = 14;
            this.m_txtDoctor.txtValuse = "";
            this.m_txtDoctor.VsLeftOrRight = 1;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(8, 288);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(70, 14);
            this.label26.TabIndex = 123;
            this.label26.Text = "申请医生:";
            // 
            // m_txtDept
            // 
            this.m_txtDept.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDept.intHeight = 200;
            this.m_txtDept.IsEnterShow = true;
            this.m_txtDept.isHide = 3;
            this.m_txtDept.IsTextChangeHideListView = false;
            this.m_txtDept.isTxt = 1;
            this.m_txtDept.isUpOrDn = 0;
            this.m_txtDept.isValuse = 3;
            this.m_txtDept.Location = new System.Drawing.Point(76, 224);
            this.m_txtDept.m_IntMaxListLength = 25;
            this.m_txtDept.m_IsHaveParent = false;
            this.m_txtDept.m_strParentName = "";
            this.m_txtDept.Name = "m_txtDept";
            this.m_txtDept.SelectedItemBackColor = System.Drawing.Color.ForestGreen;
            this.m_txtDept.SelectedItemForeColor = System.Drawing.Color.White;
            this.m_txtDept.Size = new System.Drawing.Size(184, 23);
            this.m_txtDept.TabIndex = 9;
            this.m_txtDept.txtValuse = "";
            this.m_txtDept.VsLeftOrRight = 1;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(8, 28);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(70, 14);
            this.label25.TabIndex = 120;
            this.label25.Text = "卡    号:";
            // 
            // m_labSubAge
            // 
            this.m_labSubAge.AutoSize = true;
            this.m_labSubAge.Location = new System.Drawing.Point(232, 202);
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
            this.m_txtSubAGE_FLT.Location = new System.Drawing.Point(184, 197);
            this.m_txtSubAGE_FLT.MaxLength = 4;
            this.m_txtSubAGE_FLT.Name = "m_txtSubAGE_FLT";
            //this.m_txtSubAGE_FLT.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
            this.m_txtSubAGE_FLT.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtSubAGE_FLT.Size = new System.Drawing.Size(44, 23);
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
            this.m_txtAGE_FLT.Location = new System.Drawing.Point(76, 197);
            this.m_txtAGE_FLT.MaxLength = 4;
            this.m_txtAGE_FLT.Name = "m_txtAGE_FLT";
            this.m_txtAGE_FLT.Size = new System.Drawing.Size(55, 23);
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
            this.m_cmbAge.Location = new System.Drawing.Point(131, 198);
            this.m_cmbAge.Name = "m_cmbAge";
            this.m_cmbAge.Size = new System.Drawing.Size(52, 22);
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
            this.m_txtPATIENT_NAME_VCHR.Location = new System.Drawing.Point(76, 142);
            this.m_txtPATIENT_NAME_VCHR.MaxLength = 10;
            this.m_txtPATIENT_NAME_VCHR.Name = "m_txtPATIENT_NAME_VCHR";
            this.m_txtPATIENT_NAME_VCHR.Size = new System.Drawing.Size(184, 23);
            this.m_txtPATIENT_NAME_VCHR.TabIndex = 4;
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
            this.m_txtREPORT_NO_CHR.Location = new System.Drawing.Point(76, 113);
            this.m_txtREPORT_NO_CHR.MaxLength = 10;
            this.m_txtREPORT_NO_CHR.Name = "m_txtREPORT_NO_CHR";
            this.m_txtREPORT_NO_CHR.Size = new System.Drawing.Size(184, 23);
            this.m_txtREPORT_NO_CHR.TabIndex = 1;
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
            this.m_txtINPATIENT_NO_CHR.Location = new System.Drawing.Point(76, 54);
            this.m_txtINPATIENT_NO_CHR.MaxLength = 15;
            this.m_txtINPATIENT_NO_CHR.Name = "m_txtINPATIENT_NO_CHR";
            this.m_txtINPATIENT_NO_CHR.Size = new System.Drawing.Size(184, 23);
            this.m_txtINPATIENT_NO_CHR.TabIndex = 3;
            this.m_txtINPATIENT_NO_CHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtDpmt_KeyDown);
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
            this.m_txtPATIENT_NO_CHR.Location = new System.Drawing.Point(76, 84);
            this.m_txtPATIENT_NO_CHR.MaxLength = 10;
            this.m_txtPATIENT_NO_CHR.Name = "m_txtPATIENT_NO_CHR";
            //this.m_txtPATIENT_NO_CHR.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol;
            this.m_txtPATIENT_NO_CHR.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtPATIENT_NO_CHR.Size = new System.Drawing.Size(184, 23);
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
            this.m_cboSEX_CHR.Location = new System.Drawing.Point(76, 170);
            this.m_cboSEX_CHR.Name = "m_cboSEX_CHR";
            this.m_cboSEX_CHR.Size = new System.Drawing.Size(184, 22);
            this.m_cboSEX_CHR.TabIndex = 5;
            this.m_cboSEX_CHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboSEX_CHR_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 376);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 14);
            this.label14.TabIndex = 36;
            this.label14.Text = "报 告 者:";
            // 
            // m_txtBED_NO_CHR
            // 
            this.m_txtBED_NO_CHR.Location = new System.Drawing.Point(76, 253);
            this.m_txtBED_NO_CHR.MaxLength = 8;
            this.m_txtBED_NO_CHR.Name = "m_txtBED_NO_CHR";
            this.m_txtBED_NO_CHR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_txtBED_NO_CHR.Size = new System.Drawing.Size(184, 23);
            this.m_txtBED_NO_CHR.TabIndex = 10;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(8, 257);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(70, 14);
            this.label22.TabIndex = 25;
            this.label22.Text = "床    号:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 228);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(70, 14);
            this.label21.TabIndex = 24;
            this.label21.Text = "科    室:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 201);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 14);
            this.label20.TabIndex = 23;
            this.label20.Text = "年    龄:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 173);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 14);
            this.label19.TabIndex = 22;
            this.label19.Text = "性    别:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 146);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(70, 14);
            this.label18.TabIndex = 21;
            this.label18.Text = "姓    名:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 58);
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
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 117);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 14);
            this.label15.TabIndex = 18;
            this.label15.Text = "心电图号:";
            // 
            // m_dtpCHECK_DAT
            // 
            this.m_dtpCHECK_DAT.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.m_dtpCHECK_DAT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpCHECK_DAT.Location = new System.Drawing.Point(76, 313);
            this.m_dtpCHECK_DAT.Name = "m_dtpCHECK_DAT";
            this.m_dtpCHECK_DAT.Size = new System.Drawing.Size(184, 23);
            this.m_dtpCHECK_DAT.TabIndex = 12;
            // 
            // m_dtpREPORT_DAT
            // 
            this.m_dtpREPORT_DAT.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.m_dtpREPORT_DAT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpREPORT_DAT.Location = new System.Drawing.Point(76, 343);
            this.m_dtpREPORT_DAT.Name = "m_dtpREPORT_DAT";
            this.m_dtpREPORT_DAT.Size = new System.Drawing.Size(184, 23);
            this.m_dtpREPORT_DAT.TabIndex = 13;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 347);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 14);
            this.label12.TabIndex = 14;
            this.label12.Text = "报告日期:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 316);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 14);
            this.label13.TabIndex = 15;
            this.label13.Text = "检查日期:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "节律:";
            // 
            // m_txtRHYTHM_VCHR
            // 
            this.m_txtRHYTHM_VCHR.Items.AddRange(new object[] {
            "窦性",
            "异位",
            "游走",
            "起搏器",
            "窦性+交界性",
            "窦性+室心逸搏",
            "窦性+起搏"});
            this.m_txtRHYTHM_VCHR.Location = new System.Drawing.Point(96, 24);
            this.m_txtRHYTHM_VCHR.Name = "m_txtRHYTHM_VCHR";
            this.m_txtRHYTHM_VCHR.Size = new System.Drawing.Size(148, 22);
            this.m_txtRHYTHM_VCHR.TabIndex = 0;
            this.m_txtRHYTHM_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtRHYTHM_VCHR_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "心室率:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(240, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "次/分";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(328, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "P-R间期:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(556, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "秒";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 14);
            this.label6.TabIndex = 8;
            this.label6.Text = "QRS时限:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(244, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 14);
            this.label7.TabIndex = 9;
            this.label7.Text = "秒";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(356, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 14);
            this.label8.TabIndex = 10;
            this.label8.Text = "Q-T:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(524, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 14);
            this.label9.TabIndex = 11;
            this.label9.Text = "秒";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(32, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 14);
            this.label10.TabIndex = 12;
            this.label10.Text = "心电图所见一:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 409);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 14);
            this.label11.TabIndex = 13;
            this.label11.Text = "心电图诊断及见解:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_picLog);
            this.groupBox2.Controls.Add(this.m_txtQ_T_VCHR);
            this.groupBox2.Controls.Add(this.m_txt_E_Axes);
            this.groupBox2.Controls.Add(this.m_txtQRS_VCHR);
            this.groupBox2.Controls.Add(this.m_txtHEART_RATE_VCHR);
            this.groupBox2.Controls.Add(this.m_txtP_R_VCHR);
            this.groupBox2.Controls.Add(this.m_txtHEART_ROOM_VCHR);
            this.groupBox2.Controls.Add(this.m_cheIsNew);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.m_txtSUMMARY1_VCHR);
            this.groupBox2.Controls.Add(this.m_txtSUMMARY2_VCHR);
            this.groupBox2.Controls.Add(this.m_txtRHYTHM_VCHR);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.m_lbl_E_Axes);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.m_cheIsSpical);
            this.groupBox2.Location = new System.Drawing.Point(296, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(680, 712);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // m_picLog
            // 
            this.m_picLog.Image = ((System.Drawing.Image)(resources.GetObject("m_picLog.Image")));
            this.m_picLog.Location = new System.Drawing.Point(247, 155);
            this.m_picLog.Name = "m_picLog";
            this.m_picLog.Size = new System.Drawing.Size(100, 10);
            this.m_picLog.TabIndex = 1002;
            this.m_picLog.TabStop = false;
            this.m_picLog.Visible = false;
            // 
            // m_txtQ_T_VCHR
            // 
            this.m_txtQ_T_VCHR.Location = new System.Drawing.Point(400, 84);
            this.m_txtQ_T_VCHR.MaxLength = 7;
            this.m_txtQ_T_VCHR.Name = "m_txtQ_T_VCHR";
            this.m_txtQ_T_VCHR.Size = new System.Drawing.Size(120, 23);
            this.m_txtQ_T_VCHR.TabIndex = 5;
            // 
            // m_txt_E_Axes
            // 
            this.m_txt_E_Axes.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txt_E_Axes.Location = new System.Drawing.Point(96, 116);
            this.m_txt_E_Axes.m_BlnIgnoreUserInfo = true;
            this.m_txt_E_Axes.m_BlnPartControl = false;
            this.m_txt_E_Axes.m_BlnReadOnly = false;
            this.m_txt_E_Axes.m_BlnUnderLineDST = false;
            this.m_txt_E_Axes.m_ClrDST = System.Drawing.Color.Red;
            this.m_txt_E_Axes.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txt_E_Axes.m_IntCanModifyTime = 500;
            this.m_txt_E_Axes.m_IntPartControlLength = 0;
            this.m_txt_E_Axes.m_IntPartControlStartIndex = 0;
            this.m_txt_E_Axes.m_StrUserID = "";
            this.m_txt_E_Axes.m_StrUserName = "";
            this.m_txt_E_Axes.MaxLength = 7;
            this.m_txt_E_Axes.Name = "m_txt_E_Axes";
            this.m_txt_E_Axes.Size = new System.Drawing.Size(144, 23);
            this.m_txt_E_Axes.TabIndex = 4;
            this.m_txt_E_Axes.Text = "";
            // 
            // m_txtQRS_VCHR
            // 
            this.m_txtQRS_VCHR.Location = new System.Drawing.Point(96, 84);
            this.m_txtQRS_VCHR.MaxLength = 7;
            this.m_txtQRS_VCHR.Name = "m_txtQRS_VCHR";
            this.m_txtQRS_VCHR.Size = new System.Drawing.Size(144, 23);
            this.m_txtQRS_VCHR.TabIndex = 4;
            // 
            // m_txtHEART_RATE_VCHR
            // 
            this.m_txtHEART_RATE_VCHR.Location = new System.Drawing.Point(96, 56);
            this.m_txtHEART_RATE_VCHR.MaxLength = 20;
            this.m_txtHEART_RATE_VCHR.Name = "m_txtHEART_RATE_VCHR";
            this.m_txtHEART_RATE_VCHR.Size = new System.Drawing.Size(144, 23);
            this.m_txtHEART_RATE_VCHR.TabIndex = 2;
            // 
            // m_txtP_R_VCHR
            // 
            this.m_txtP_R_VCHR.AccessibleDescription = "P-R间期";
            this.m_txtP_R_VCHR.ContextMenu = this.mnuRichTextBox;
            this.m_txtP_R_VCHR.Location = new System.Drawing.Point(400, 56);
            this.m_txtP_R_VCHR.MaxLength = 15;
            this.m_txtP_R_VCHR.Name = "m_txtP_R_VCHR";
            this.m_txtP_R_VCHR.Size = new System.Drawing.Size(148, 23);
            this.m_txtP_R_VCHR.TabIndex = 3;
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
            // m_txtHEART_ROOM_VCHR
            // 
            this.m_txtHEART_ROOM_VCHR.AccessibleDescription = "心房率";
            this.m_txtHEART_ROOM_VCHR.ContextMenu = this.mnuRichTextBox;
            this.m_txtHEART_ROOM_VCHR.Location = new System.Drawing.Point(400, 24);
            this.m_txtHEART_ROOM_VCHR.MaxLength = 15;
            this.m_txtHEART_ROOM_VCHR.Name = "m_txtHEART_ROOM_VCHR";
            this.m_txtHEART_ROOM_VCHR.Size = new System.Drawing.Size(148, 23);
            this.m_txtHEART_ROOM_VCHR.TabIndex = 1;
            // 
            // m_cheIsNew
            // 
            this.m_cheIsNew.Location = new System.Drawing.Point(359, 115);
            this.m_cheIsNew.Name = "m_cheIsNew";
            this.m_cheIsNew.Size = new System.Drawing.Size(72, 24);
            this.m_cheIsNew.TabIndex = 1001;
            this.m_cheIsNew.Text = "新记录";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(556, 28);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(42, 14);
            this.label24.TabIndex = 211;
            this.label24.Text = "次/分";
            // 
            // m_txtSUMMARY1_VCHR
            // 
            this.m_txtSUMMARY1_VCHR.AccessibleDescription = "心电图所见";
            this.m_txtSUMMARY1_VCHR.BackColor = System.Drawing.Color.White;
            this.m_txtSUMMARY1_VCHR.ContextMenu = this.mnuRichTextBox;
            this.m_txtSUMMARY1_VCHR.Font = new System.Drawing.Font("宋体", 14F);
            this.m_txtSUMMARY1_VCHR.ForeColor = System.Drawing.Color.Black;
            this.m_txtSUMMARY1_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtSUMMARY1_VCHR.Location = new System.Drawing.Point(28, 174);
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
            this.m_txtSUMMARY1_VCHR.Size = new System.Drawing.Size(644, 220);
            this.m_txtSUMMARY1_VCHR.TabIndex = 7;
            this.m_txtSUMMARY1_VCHR.Text = "";
            // 
            // m_txtSUMMARY2_VCHR
            // 
            this.m_txtSUMMARY2_VCHR.AccessibleDescription = "心电图诊断及见解";
            this.m_txtSUMMARY2_VCHR.ContextMenu = this.mnuRichTextBox;
            this.m_txtSUMMARY2_VCHR.Font = new System.Drawing.Font("宋体", 14F);
            this.m_txtSUMMARY2_VCHR.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtSUMMARY2_VCHR.Location = new System.Drawing.Point(28, 431);
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
            this.m_txtSUMMARY2_VCHR.Size = new System.Drawing.Size(640, 269);
            this.m_txtSUMMARY2_VCHR.TabIndex = 8;
            this.m_txtSUMMARY2_VCHR.Text = "";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(336, 28);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(56, 14);
            this.label23.TabIndex = 20;
            this.label23.Text = "心房率:";
            // 
            // m_lbl_E_Axes
            // 
            this.m_lbl_E_Axes.AutoSize = true;
            this.m_lbl_E_Axes.Location = new System.Drawing.Point(53, 120);
            this.m_lbl_E_Axes.Name = "m_lbl_E_Axes";
            this.m_lbl_E_Axes.Size = new System.Drawing.Size(42, 14);
            this.m_lbl_E_Axes.TabIndex = 8;
            this.m_lbl_E_Axes.Text = "电轴:";
            // 
            // m_cheIsSpical
            // 
            this.m_cheIsSpical.Location = new System.Drawing.Point(486, 115);
            this.m_cheIsSpical.Name = "m_cheIsSpical";
            this.m_cheIsSpical.Size = new System.Drawing.Size(112, 24);
            this.m_cheIsSpical.TabIndex = 6;
            this.m_cheIsSpical.Text = "是否特殊病人";
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(44, 15);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(184, 28);
            this.m_cmdSave.TabIndex = 0;
            this.m_cmdSave.Text = "保      存";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // m_cmdConfirm
            // 
            this.m_cmdConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdConfirm.DefaultScheme = true;
            this.m_cmdConfirm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdConfirm.Hint = "";
            this.m_cmdConfirm.Location = new System.Drawing.Point(44, 75);
            this.m_cmdConfirm.Name = "m_cmdConfirm";
            this.m_cmdConfirm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdConfirm.Size = new System.Drawing.Size(184, 28);
            this.m_cmdConfirm.TabIndex = 2;
            this.m_cmdConfirm.Text = "审      核";
            this.m_cmdConfirm.Click += new System.EventHandler(this.m_cmdConfirm_Click);
            // 
            // m_cmdPrint
            // 
            this.m_cmdPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdPrint.DefaultScheme = true;
            this.m_cmdPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdPrint.Hint = "";
            this.m_cmdPrint.Location = new System.Drawing.Point(44, 105);
            this.m_cmdPrint.Name = "m_cmdPrint";
            this.m_cmdPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdPrint.Size = new System.Drawing.Size(184, 28);
            this.m_cmdPrint.TabIndex = 3;
            this.m_cmdPrint.Text = "打      印";
            this.m_cmdPrint.Click += new System.EventHandler(this.m_cmdPrint_Click);
            // 
            // m_cmdExit
            // 
            this.m_cmdExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdExit.DefaultScheme = true;
            this.m_cmdExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdExit.Hint = "";
            this.m_cmdExit.Location = new System.Drawing.Point(44, 255);
            this.m_cmdExit.Name = "m_cmdExit";
            this.m_cmdExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdExit.Size = new System.Drawing.Size(184, 28);
            this.m_cmdExit.TabIndex = 7;
            this.m_cmdExit.Text = "退      出";
            this.m_cmdExit.Click += new System.EventHandler(this.m_cmdExit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_cmdClear);
            this.groupBox3.Controls.Add(this.m_cmdCreateTemplate);
            this.groupBox3.Controls.Add(this.m_cmdDelete);
            this.groupBox3.Controls.Add(this.m_cmdSave);
            this.groupBox3.Controls.Add(this.m_cmdConfirm);
            this.groupBox3.Controls.Add(this.m_btnDisplayApplyOrder);
            this.groupBox3.Controls.Add(this.m_cmdPrint);
            this.groupBox3.Controls.Add(this.m_cmdExit);
            this.groupBox3.Controls.Add(this.m_cmdEditTemplate);
            this.groupBox3.Location = new System.Drawing.Point(8, 430);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(284, 290);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // m_cmdClear
            // 
            this.m_cmdClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdClear.DefaultScheme = true;
            this.m_cmdClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdClear.Hint = "";
            this.m_cmdClear.Location = new System.Drawing.Point(44, 225);
            this.m_cmdClear.Name = "m_cmdClear";
            this.m_cmdClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdClear.Size = new System.Drawing.Size(184, 28);
            this.m_cmdClear.TabIndex = 6;
            this.m_cmdClear.Text = "清      空";
            this.m_cmdClear.Click += new System.EventHandler(this.m_cmdClear_Click);
            // 
            // m_cmdCreateTemplate
            // 
            this.m_cmdCreateTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdCreateTemplate.DefaultScheme = true;
            this.m_cmdCreateTemplate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdCreateTemplate.Hint = "";
            this.m_cmdCreateTemplate.Location = new System.Drawing.Point(44, 165);
            this.m_cmdCreateTemplate.Name = "m_cmdCreateTemplate";
            this.m_cmdCreateTemplate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCreateTemplate.Size = new System.Drawing.Size(184, 28);
            this.m_cmdCreateTemplate.TabIndex = 4;
            this.m_cmdCreateTemplate.Text = "生 成 模 板";
            this.m_cmdCreateTemplate.Click += new System.EventHandler(this.m_cmdCreateTemplate_Click);
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(44, 45);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete.Size = new System.Drawing.Size(184, 28);
            this.m_cmdDelete.TabIndex = 1;
            this.m_cmdDelete.Text = "删      除";
            this.m_cmdDelete.Click += new System.EventHandler(this.m_cmdDelete_Click);
            // 
            // m_btnDisplayApplyOrder
            // 
            this.m_btnDisplayApplyOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_btnDisplayApplyOrder.DefaultScheme = true;
            this.m_btnDisplayApplyOrder.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnDisplayApplyOrder.Hint = "";
            this.m_btnDisplayApplyOrder.Location = new System.Drawing.Point(44, 135);
            this.m_btnDisplayApplyOrder.Name = "m_btnDisplayApplyOrder";
            this.m_btnDisplayApplyOrder.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnDisplayApplyOrder.Size = new System.Drawing.Size(184, 28);
            this.m_btnDisplayApplyOrder.TabIndex = 3;
            this.m_btnDisplayApplyOrder.Text = "显示申请单";
            this.m_btnDisplayApplyOrder.Click += new System.EventHandler(this.m_btnDisplayApplyOrder_Click);
            // 
            // m_cmdEditTemplate
            // 
            this.m_cmdEditTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdEditTemplate.DefaultScheme = true;
            this.m_cmdEditTemplate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdEditTemplate.Hint = "";
            this.m_cmdEditTemplate.Location = new System.Drawing.Point(44, 195);
            this.m_cmdEditTemplate.Name = "m_cmdEditTemplate";
            this.m_cmdEditTemplate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdEditTemplate.Size = new System.Drawing.Size(184, 28);
            this.m_cmdEditTemplate.TabIndex = 5;
            this.m_cmdEditTemplate.Text = "修 改 模 板";
            this.m_cmdEditTemplate.Click += new System.EventHandler(this.m_cmdEditTemplate_Click);
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
            // m_printDoc
            // 
            this.m_printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_printDoc_PrintPage);
            this.m_printDoc.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_printDoc_EndPrint);
            this.m_printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_printDoc_BeginPrint);
            // 
            // frmCardiogramReport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1016, 732);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCardiogramReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "心电图报告";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCardiogramReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCardiogramReport_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picLog)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			this.objController = new com.digitalwave.iCare.gui.RIS.clsController_RISCardiogramReport(this);
			objController.Set_GUI_Apperance(this);
		}

		private void frmCardiogramReport_Load(object sender, System.EventArgs e)
		{
			//m_mthSetFormControlCanBeNull(this);
		
			m_mthSetEnter2Tab(new System.Windows.Forms.Control[]
					{m_txtSUMMARY1_VCHR,m_txtSUMMARY2_VCHR,m_txtDept, m_txtApplyDoctor, m_txtDoctor});

            com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentArea = new clsEmrDept_VO();
            com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentArea.m_strDEPTID_CHR = LoginInfo.m_strDepartmentID;
            global::iCare.clsTransferTemplate objTemplate = new global::iCare.clsTransferTemplate();
            objTemplate.m_mthAddTextBox(this, m_txt_E_Axes, this.Name, m_txt_E_Axes.Name);
		}

		public void m_mthSetReport(clsRIS_CardiogramReport_VO p_objItem)
		{
			((clsController_RISCardiogramReport)this.objController).m_mthSetReport(p_objItem);
		}
        public void m_mthSetShow(clsApplyRecord objVO1)
        {
         
            this.m_strDoctorName = objVO1.m_strDoctorName;
            this.m_txtPATIENT_NAME_VCHR.Tag = "";
            this.m_txtREPORT_NO_CHR.Text = objVO1.m_strAreaID;
            this.m_txtINPATIENT_NO_CHR.Text = objVO1.m_strBIHNO;
            this.m_txtPATIENT_NO_CHR.Text = objVO1.m_strClinicNO;
            this.m_strApplyID = objVO1.m_strApplyID;
            m_txtApplyDoctor.txtValuse = objVO1.m_strDoctorName;
            if (objVO1.m_strAge.Length > 0)
            {
                try
                {
                    this.m_txtAGE_FLT.Text = int.Parse(objVO1.m_strAge).ToString();
                }
                catch
                {
                    this.m_txtAGE_FLT.Text = objVO1.m_strAge.Substring(0, objVO1.m_strAge.Length - 1);
                    switch (objVO1.m_strAge.Substring(objVO1.m_strAge.Length - 1, 1))
                    {
                        case "日":
                            this.m_cmbAge.Text = "天";
                            break;
                        case "岁":
                            this.m_cmbAge.Text = "年";
                            break;
                        case "月":
                            this.m_cmbAge.Text = "月";
                            break;
                    }
                }
            }
            this.m_txtPATIENT_NAME_VCHR.Text = objVO1.m_strName;
            this.m_cboSEX_CHR.Text = objVO1.m_strSex;
            this.m_txtDept.txtValuse = objVO1.m_strDepartment;
            m_txtDept.Tag = objVO1.m_strDeptID;
            this.m_txtBED_NO_CHR.Text = objVO1.m_strBedNO;
            #region    根据 卡号 检索病人ID
            this.carID.Text = objVO1.m_strCardNO;
            string m_strPatientCardID = this.carID.Text;
            string m_strInpatientNo = this.m_txtINPATIENT_NO_CHR.Text;
            DataTable m_objTabPatientInfo = new DataTable();
            clsDomainController_RISCardiogramManage m_objManage = new clsDomainController_RISCardiogramManage();
            if (m_strPatientCardID.Trim() != string.Empty)
            {
                m_objManage.m_lngGetPat(m_strPatientCardID, out m_objTabPatientInfo);
            }
            else if (m_strInpatientNo.Trim() != string.Empty)
            {
                m_objManage.m_lngGetPatientInfo(m_strInpatientNo, out m_objTabPatientInfo);
            }
            if (m_objTabPatientInfo.Rows.Count > 0)
            {
                this.m_txtPATIENT_NAME_VCHR.Tag = m_objTabPatientInfo.Rows[0]["PATIENTID_CHR"].ToString();
            }
            else
            {
                this.m_txtPATIENT_NAME_VCHR.Tag = string.Empty;
            }
            # endregion
            this.m_cmdSave.Tag = "NO";
            frmCardiogramReportManage infrmCardiogramReportManage = new frmCardiogramReportManage();
            this.m_mthSetParentApperance(infrmCardiogramReportManage);
            this.ShowDialog();
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
                ((clsController_RISCardiogramReport)this.objController).m_mthDoSave();
                //frmCardiogramReportManage objfrm = new frmCardiogramReportManage();
                //objfrm.m_cmdRefreshClick();
		}

		private void m_cmdDelete_Click(object sender, System.EventArgs e)
		{
			((clsController_RISCardiogramReport)this.objController).m_mthDoDelete();
            this.m_cmdSave.Enabled = true;
			
		}

		private void m_txtHEART_RATE_VCHR_TextChanged(object sender, System.EventArgs e)
		{
			m_txtHEART_ROOM_VCHR.Text = m_txtHEART_RATE_VCHR.Text.Trim();
		}

		private void m_cmdConfirm_Click(object sender, System.EventArgs e)
		{
			((clsController_RISCardiogramReport)this.objController).m_mthDoConfirm();
            this.m_cmdSave.Enabled = false;
		}

		private void label16_Click(object sender, System.EventArgs e)
		{
		
		}

		private void m_cmdPrint_Click(object sender, System.EventArgs e)
		{
            if (this.m_cmdSave.Tag.ToString() == "OK")
                ((clsController_RISCardiogramReport)this.objController).m_mthPrintReport(this);
            else
                MessageBox.Show(this, "请先保存数据再打印！", "心电图报告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
		
		}

		private void m_printDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
		}

		private void m_printDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
		}

		private void m_printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
   
                ((clsController_RISCardiogramReport)this.objController).m_mthPrintDocPrintPage(e);

	
		}

		private void m_cmdCreateTemplate_Click(object sender, System.EventArgs e)
		{
            ((clsController_RISCardiogramReport)this.objController).m_mthCreateTemplate();
			//((clsController_RISCardiogramReport)this.objController).m_mthCreateTemplate(this);
		}		

		public void m_mthSetParentApperance(com.digitalwave.iCare.gui.RIS.frmCardiogramReportManage infrmCardiogramReportManage)
		{
			((clsController_RISCardiogramReport)this.objController).SetParentApperance(infrmCardiogramReportManage);
		}

        //private void groupBox2_Enter(object sender, System.EventArgs e)
        //{
		
        //}
	
		private void m_mthInitRichTextBox()
		{

			ctlRichTextBox.m_ClrDefaultViewText=Color.Black;
			ctlRichTextBox[] rtbArr  =new ctlRichTextBox[]{m_txtSUMMARY1_VCHR ,m_txtSUMMARY2_VCHR , m_txt_E_Axes};
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
		#endregion

		private void frmCardiogramReport_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			m_mthSetKeyTab(e);
		}

		private void m_cmdClear_Click(object sender, System.EventArgs e)
		{
			((clsController_RISCardiogramReport)this.objController).m_mthClear();
            this.m_cmdSave.Enabled = true;
			
		}

		private void m_cmdEditTemplate_Click(object sender, System.EventArgs e)
		{
            ((clsController_RISCardiogramReport)this.objController).m_mthEditTemplate();
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

        //private void m_txtPATIENT_NAME_VCHR_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        //{
        //}

        //private void groupBox1_Enter(object sender, System.EventArgs e)
        //{
		
        //}

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

		private void m_txtRHYTHM_VCHR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				this.m_txtHEART_ROOM_VCHR.Focus();
			}
			if(e.KeyCode==Keys.Down)
			{
				if(this.m_txtRHYTHM_VCHR.SelectedIndex==6)
				{
					this.m_txtRHYTHM_VCHR.SelectedIndex=0;
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

		private void m_txtQ_T_VCHR_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
//			if((e.KeyChar>='0'&&e.KeyChar<='9')||(e.KeyChar=='.')||(e.KeyChar==8))
//				e.Handled=false;
//			else
//				e.Handled=true;

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
				((clsController_RISCardiogramReport)this.objController).m_lngGetPat();
			}
		}


        private void m_btnDisplayApplyOrder_Click(object sender, EventArgs e)
        {
            if (this.m_strApplyID.Trim().Length <= 0) return;
            com.digitalwave.GLS_WS.clsApplyForm objfrm2 = new com.digitalwave.GLS_WS.clsApplyForm();
            objfrm2.OpenForm(this.m_strApplyID.Trim());
        }

        private void m_txtDpmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsController_RISCardiogramReport)this.objController).m_lngGetPatByInPatientID();
            }
        }

        private void carID1_CardKeyDown(object sender, EventArgs e)
        {
            ((clsController_RISCardiogramReport)this.objController).m_lngGetPat();
        }

	}


	
}
