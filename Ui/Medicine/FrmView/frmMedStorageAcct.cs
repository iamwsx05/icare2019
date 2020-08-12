using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmMedStorageAcct 的摘要说明。
	/// </summary>
	public class frmMedStorageAcct : com.digitalwave.GUI_Base.frmMDI_Child_Base	//GUI_Base.dll
	{
		private System.Windows.Forms.Panel panel5;
		internal System.Windows.Forms.TabControl tabAccpt;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		internal System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel1;
		internal System.Windows.Forms.ListView m_lsvUnAccet;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Windows.Forms.ColumnHeader columnHeader26;
		private System.Windows.Forms.ColumnHeader columnHeader24;
		private System.Windows.Forms.ColumnHeader columnHeader27;
		private System.Windows.Forms.ColumnHeader columnHeader29;
		internal System.Windows.Forms.ListView lsvAccet;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label11;
		internal System.Windows.Forms.TextBox m_txtSaleTolMoney;
		internal System.Windows.Forms.TextBox m_txtBuyTolMoney;
		internal System.Windows.Forms.TextBox m_txtOrdID;
		internal System.Windows.Forms.TextBox m_txtAduit;
		internal System.Windows.Forms.TextBox m_txtCreator;
		internal System.Windows.Forms.TextBox m_txtOrdType;
		internal System.Windows.Forms.TextBox m_txtStorage;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		internal System.Windows.Forms.TextBox textBoxTyped1;
		internal PinkieControls.ButtonXP btnAcct;
		internal System.Windows.Forms.ListView m_lsvDetail;
		private System.Windows.Forms.ColumnHeader clhRowNo;
		private System.Windows.Forms.ColumnHeader clhMedID;
		private System.Windows.Forms.ColumnHeader clhMedName;
		private System.Windows.Forms.ColumnHeader clhMedSpec;
		private System.Windows.Forms.ColumnHeader clhUnit;
		private System.Windows.Forms.ColumnHeader 差额;
		private System.Windows.Forms.ColumnHeader 数量;
		private System.Windows.Forms.ColumnHeader 调整后价格;
		private System.Windows.Forms.ColumnHeader 原价格;
		internal System.Windows.Forms.ComboBox m_cboSelPeriod;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMedStorageAcct()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

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
			this.panel5 = new System.Windows.Forms.Panel();
			this.tabAccpt = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.m_lsvUnAccet = new System.Windows.Forms.ListView();
			this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader23 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader26 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader24 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader27 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader29 = new System.Windows.Forms.ColumnHeader();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.lsvAccet = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.textBoxTyped1 = new System.Windows.Forms.TextBox();
			this.btnAcct = new PinkieControls.ButtonXP();
			this.label11 = new System.Windows.Forms.Label();
			this.m_txtSaleTolMoney = new System.Windows.Forms.TextBox();
			this.m_txtBuyTolMoney = new System.Windows.Forms.TextBox();
			this.m_txtOrdID = new System.Windows.Forms.TextBox();
			this.m_txtAduit = new System.Windows.Forms.TextBox();
			this.m_txtCreator = new System.Windows.Forms.TextBox();
			this.m_txtOrdType = new System.Windows.Forms.TextBox();
			this.m_txtStorage = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.m_lsvDetail = new System.Windows.Forms.ListView();
			this.clhRowNo = new System.Windows.Forms.ColumnHeader();
			this.clhMedID = new System.Windows.Forms.ColumnHeader();
			this.clhMedName = new System.Windows.Forms.ColumnHeader();
			this.clhMedSpec = new System.Windows.Forms.ColumnHeader();
			this.clhUnit = new System.Windows.Forms.ColumnHeader();
			this.原价格 = new System.Windows.Forms.ColumnHeader();
			this.调整后价格 = new System.Windows.Forms.ColumnHeader();
			this.数量 = new System.Windows.Forms.ColumnHeader();
			this.差额 = new System.Windows.Forms.ColumnHeader();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.m_cboSelPeriod = new System.Windows.Forms.ComboBox();
			this.panel5.SuspendLayout();
			this.tabAccpt.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel5
			// 
			this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.panel5.BackColor = System.Drawing.SystemColors.Control;
			this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel5.Controls.Add(this.m_cboSelPeriod);
			this.panel5.Controls.Add(this.tabAccpt);
			this.panel5.Location = new System.Drawing.Point(0, 8);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(336, 584);
			this.panel5.TabIndex = 148;
			// 
			// tabAccpt
			// 
			this.tabAccpt.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabAccpt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.tabAccpt.Controls.Add(this.tabPage1);
			this.tabAccpt.Controls.Add(this.tabPage2);
			this.tabAccpt.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.tabAccpt.ItemSize = new System.Drawing.Size(48, 17);
			this.tabAccpt.Location = new System.Drawing.Point(-8, 0);
			this.tabAccpt.Name = "tabAccpt";
			this.tabAccpt.SelectedIndex = 0;
			this.tabAccpt.Size = new System.Drawing.Size(336, 579);
			this.tabAccpt.TabIndex = 0;
			this.tabAccpt.SelectedIndexChanged += new System.EventHandler(this.tabAccpt_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.m_lsvUnAccet);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(328, 554);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "未登账";
			// 
			// m_lsvUnAccet
			// 
			this.m_lsvUnAccet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lsvUnAccet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						   this.columnHeader22,
																						   this.columnHeader23,
																						   this.columnHeader26,
																						   this.columnHeader24,
																						   this.columnHeader27,
																						   this.columnHeader29});
			this.m_lsvUnAccet.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_lsvUnAccet.FullRowSelect = true;
			this.m_lsvUnAccet.GridLines = true;
			this.m_lsvUnAccet.Location = new System.Drawing.Point(0, -4);
			this.m_lsvUnAccet.MultiSelect = false;
			this.m_lsvUnAccet.Name = "m_lsvUnAccet";
			this.m_lsvUnAccet.Size = new System.Drawing.Size(328, 559);
			this.m_lsvUnAccet.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.m_lsvUnAccet.TabIndex = 1;
			this.m_lsvUnAccet.TabStop = false;
			this.m_lsvUnAccet.View = System.Windows.Forms.View.Details;
			this.m_lsvUnAccet.Click += new System.EventHandler(this.m_lsvUnAccet_Click);
			// 
			// columnHeader22
			// 
			this.columnHeader22.Text = "单据号ID";
			this.columnHeader22.Width = 110;
			// 
			// columnHeader23
			// 
			this.columnHeader23.Text = "单据类型";
			this.columnHeader23.Width = 70;
			// 
			// columnHeader26
			// 
			this.columnHeader26.Text = "创建人";
			// 
			// columnHeader24
			// 
			this.columnHeader24.Text = "创建时间";
			this.columnHeader24.Width = 150;
			// 
			// columnHeader27
			// 
			this.columnHeader27.Text = "审核人";
			// 
			// columnHeader29
			// 
			this.columnHeader29.Text = "总金额";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.lsvAccet);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(322, 545);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "己登帐";
			// 
			// lsvAccet
			// 
			this.lsvAccet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lsvAccet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.columnHeader1,
																					   this.columnHeader3,
																					   this.columnHeader4,
																					   this.columnHeader5,
																					   this.columnHeader6,
																					   this.columnHeader8});
			this.lsvAccet.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lsvAccet.FullRowSelect = true;
			this.lsvAccet.GridLines = true;
			this.lsvAccet.Location = new System.Drawing.Point(0, -3);
			this.lsvAccet.MultiSelect = false;
			this.lsvAccet.Name = "lsvAccet";
			this.lsvAccet.Size = new System.Drawing.Size(322, 550);
			this.lsvAccet.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lsvAccet.TabIndex = 2;
			this.lsvAccet.TabStop = false;
			this.lsvAccet.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "据号ID";
			this.columnHeader1.Width = 110;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "单据类型";
			this.columnHeader3.Width = 70;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "创建人";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "创建时间";
			this.columnHeader5.Width = 150;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "登账人";
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "总金额";
			// 
			// panel4
			// 
			this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel4.BackColor = System.Drawing.SystemColors.Control;
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel4.Controls.Add(this.panel3);
			this.panel4.Location = new System.Drawing.Point(344, 464);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(672, 128);
			this.panel4.TabIndex = 147;
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.BackColor = System.Drawing.SystemColors.Control;
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel3.Controls.Add(this.textBoxTyped1);
			this.panel3.Controls.Add(this.btnAcct);
			this.panel3.Controls.Add(this.label11);
			this.panel3.Controls.Add(this.m_txtSaleTolMoney);
			this.panel3.Controls.Add(this.m_txtBuyTolMoney);
			this.panel3.Controls.Add(this.m_txtOrdID);
			this.panel3.Controls.Add(this.m_txtAduit);
			this.panel3.Controls.Add(this.m_txtCreator);
			this.panel3.Controls.Add(this.m_txtOrdType);
			this.panel3.Controls.Add(this.m_txtStorage);
			this.panel3.Controls.Add(this.label8);
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.label4);
			this.panel3.Controls.Add(this.label12);
			this.panel3.Controls.Add(this.label13);
			this.panel3.Controls.Add(this.label14);
			this.panel3.Controls.Add(this.label15);
			this.panel3.Location = new System.Drawing.Point(1, 1);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(666, 138);
			this.panel3.TabIndex = 0;
			// 
			// textBoxTyped1
			// 
			this.textBoxTyped1.BackColor = System.Drawing.SystemColors.Window;
			//this.textBoxTyped1.EnableAutoValidation = true;
			//this.textBoxTyped1.EnableEnterKeyValidate = true;
			//this.textBoxTyped1.EnableEscapeKeyUndo = true;
			//this.textBoxTyped1.EnableLastValidValue = true;
			//this.textBoxTyped1.ErrorProvider = null;
			//this.textBoxTyped1.ErrorProviderMessage = "Invalid value";
			//this.textBoxTyped1.ForceFormatText = true;
			this.textBoxTyped1.Location = new System.Drawing.Point(312, 52);
			this.textBoxTyped1.Name = "textBoxTyped1";
			this.textBoxTyped1.ReadOnly = true;
			this.textBoxTyped1.Size = new System.Drawing.Size(128, 23);
			this.textBoxTyped1.TabIndex = 146;
			this.textBoxTyped1.Text = "";
			// 
			// btnAcct
			// 
			this.btnAcct.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btnAcct.DefaultScheme = true;
			this.btnAcct.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnAcct.Hint = "";
			this.btnAcct.Location = new System.Drawing.Point(552, 88);
			this.btnAcct.Name = "btnAcct";
			this.btnAcct.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btnAcct.Size = new System.Drawing.Size(100, 23);
			this.btnAcct.TabIndex = 145;
			this.btnAcct.TabStop = false;
			this.btnAcct.Text = "登账(&S)";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(224, 90);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(63, 19);
			this.label11.TabIndex = 47;
			this.label11.Text = "零售金额";
			// 
			// m_txtSaleTolMoney
			// 
			this.m_txtSaleTolMoney.BackColor = System.Drawing.SystemColors.Window;
			//this.m_txtSaleTolMoney.EnableAutoValidation = true;
			//this.m_txtSaleTolMoney.EnableEnterKeyValidate = true;
			//this.m_txtSaleTolMoney.EnableEscapeKeyUndo = true;
			//this.m_txtSaleTolMoney.EnableLastValidValue = true;
			//this.m_txtSaleTolMoney.ErrorProvider = null;
			//this.m_txtSaleTolMoney.ErrorProviderMessage = "Invalid value";
			//this.m_txtSaleTolMoney.ForceFormatText = true;
			this.m_txtSaleTolMoney.Location = new System.Drawing.Point(312, 88);
			this.m_txtSaleTolMoney.Name = "m_txtSaleTolMoney";
			this.m_txtSaleTolMoney.ReadOnly = true;
			this.m_txtSaleTolMoney.Size = new System.Drawing.Size(128, 23);
			this.m_txtSaleTolMoney.TabIndex = 46;
			this.m_txtSaleTolMoney.Text = "";
			// 
			// m_txtBuyTolMoney
			// 
			this.m_txtBuyTolMoney.BackColor = System.Drawing.SystemColors.Window;
			//this.m_txtBuyTolMoney.EnableAutoValidation = false;
			//this.m_txtBuyTolMoney.EnableEnterKeyValidate = true;
			//this.m_txtBuyTolMoney.EnableEscapeKeyUndo = true;
			//this.m_txtBuyTolMoney.EnableLastValidValue = true;
			//this.m_txtBuyTolMoney.ErrorProvider = null;
			//this.m_txtBuyTolMoney.ErrorProviderMessage = "Invalid value";
			//this.m_txtBuyTolMoney.ForceFormatText = true;
			this.m_txtBuyTolMoney.Location = new System.Drawing.Point(552, 52);
			this.m_txtBuyTolMoney.Name = "m_txtBuyTolMoney";
			this.m_txtBuyTolMoney.ReadOnly = true;
			this.m_txtBuyTolMoney.TabIndex = 45;
			this.m_txtBuyTolMoney.Text = "";
			// 
			// m_txtOrdID
			// 
			this.m_txtOrdID.BackColor = System.Drawing.SystemColors.Window;
			//this.m_txtOrdID.EnableAutoValidation = false;
			//this.m_txtOrdID.EnableEnterKeyValidate = true;
			//this.m_txtOrdID.EnableEscapeKeyUndo = true;
			//this.m_txtOrdID.EnableLastValidValue = true;
			//this.m_txtOrdID.ErrorProvider = null;
			//this.m_txtOrdID.ErrorProviderMessage = "Invalid value";
			//this.m_txtOrdID.ForceFormatText = true;
			this.m_txtOrdID.Location = new System.Drawing.Point(80, 52);
			this.m_txtOrdID.Name = "m_txtOrdID";
			this.m_txtOrdID.ReadOnly = true;
			this.m_txtOrdID.TabIndex = 43;
			this.m_txtOrdID.Text = "";
			// 
			// m_txtAduit
			// 
			this.m_txtAduit.BackColor = System.Drawing.SystemColors.Window;
			//this.m_txtAduit.EnableAutoValidation = false;
			//this.m_txtAduit.EnableEnterKeyValidate = true;
			//this.m_txtAduit.EnableEscapeKeyUndo = true;
			//this.m_txtAduit.EnableLastValidValue = true;
			//this.m_txtAduit.ErrorProvider = null;
			//this.m_txtAduit.ErrorProviderMessage = "Invalid value";
			//this.m_txtAduit.ForceFormatText = true;
			this.m_txtAduit.Location = new System.Drawing.Point(80, 88);
			this.m_txtAduit.Name = "m_txtAduit";
			this.m_txtAduit.ReadOnly = true;
			this.m_txtAduit.TabIndex = 42;
			this.m_txtAduit.Text = "";
			// 
			// m_txtCreator
			// 
			this.m_txtCreator.BackColor = System.Drawing.SystemColors.Window;
			//this.m_txtCreator.EnableAutoValidation = false;
			//this.m_txtCreator.EnableEnterKeyValidate = true;
			//this.m_txtCreator.EnableEscapeKeyUndo = true;
			//this.m_txtCreator.EnableLastValidValue = true;
			//this.m_txtCreator.ErrorProvider = null;
			//this.m_txtCreator.ErrorProviderMessage = "Invalid value";
			//this.m_txtCreator.ForceFormatText = true;
			this.m_txtCreator.Location = new System.Drawing.Point(552, 16);
			this.m_txtCreator.Name = "m_txtCreator";
			this.m_txtCreator.ReadOnly = true;
			this.m_txtCreator.TabIndex = 41;
			this.m_txtCreator.Text = "";
			// 
			// m_txtOrdType
			// 
			this.m_txtOrdType.BackColor = System.Drawing.SystemColors.Window;
			//this.m_txtOrdType.EnableAutoValidation = false;
			//this.m_txtOrdType.EnableEnterKeyValidate = true;
			//this.m_txtOrdType.EnableEscapeKeyUndo = true;
			//this.m_txtOrdType.EnableLastValidValue = true;
			//this.m_txtOrdType.ErrorProvider = null;
			//this.m_txtOrdType.ErrorProviderMessage = "Invalid value";
			//this.m_txtOrdType.ForceFormatText = true;
			this.m_txtOrdType.Location = new System.Drawing.Point(312, 16);
			this.m_txtOrdType.Name = "m_txtOrdType";
			this.m_txtOrdType.ReadOnly = true;
			this.m_txtOrdType.Size = new System.Drawing.Size(128, 23);
			this.m_txtOrdType.TabIndex = 40;
			this.m_txtOrdType.Text = "";
			// 
			// m_txtStorage
			// 
			this.m_txtStorage.BackColor = System.Drawing.SystemColors.Window;
			//this.m_txtStorage.EnableAutoValidation = false;
			//this.m_txtStorage.EnableEnterKeyValidate = true;
			//this.m_txtStorage.EnableEscapeKeyUndo = true;
			//this.m_txtStorage.EnableLastValidValue = true;
			//this.m_txtStorage.ErrorProvider = null;
			//this.m_txtStorage.ErrorProviderMessage = "Invalid value";
			//this.m_txtStorage.ForceFormatText = true;
			this.m_txtStorage.Location = new System.Drawing.Point(80, 16);
			this.m_txtStorage.Name = "m_txtStorage";
			this.m_txtStorage.ReadOnly = true;
			this.m_txtStorage.TabIndex = 39;
			this.m_txtStorage.Text = "";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(24, 54);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 19);
			this.label8.TabIndex = 38;
			this.label8.Text = "单据号";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(488, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 19);
			this.label2.TabIndex = 37;
			this.label2.Text = "购进金额";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(224, 54);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 19);
			this.label4.TabIndex = 36;
			this.label4.Text = "创建时间";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(24, 90);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(48, 19);
			this.label12.TabIndex = 35;
			this.label12.Text = "审核员";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(224, 18);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(63, 19);
			this.label13.TabIndex = 34;
			this.label13.Text = "单据类型";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(488, 18);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(48, 19);
			this.label14.TabIndex = 33;
			this.label14.Text = "制单员";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(27, 18);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(34, 19);
			this.label15.TabIndex = 32;
			this.label15.Text = "制单";
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.m_lsvDetail);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.panel5);
			this.panel1.Controls.Add(this.panel4);
			this.panel1.Location = new System.Drawing.Point(0, -10);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1024, 640);
			this.panel1.TabIndex = 149;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// m_lsvDetail
			// 
			this.m_lsvDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lsvDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.clhRowNo,
																						  this.clhMedID,
																						  this.clhMedName,
																						  this.clhMedSpec,
																						  this.clhUnit,
																						  this.原价格,
																						  this.调整后价格,
																						  this.数量,
																						  this.差额});
			this.m_lsvDetail.FullRowSelect = true;
			this.m_lsvDetail.GridLines = true;
			this.m_lsvDetail.Location = new System.Drawing.Point(344, 8);
			this.m_lsvDetail.Name = "m_lsvDetail";
			this.m_lsvDetail.Size = new System.Drawing.Size(672, 456);
			this.m_lsvDetail.TabIndex = 150;
			this.m_lsvDetail.View = System.Windows.Forms.View.Details;
			// 
			// clhRowNo
			// 
			this.clhRowNo.Text = "行号";
			// 
			// clhMedID
			// 
			this.clhMedID.Text = "药品代码";
			this.clhMedID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clhMedID.Width = 80;
			// 
			// clhMedName
			// 
			this.clhMedName.Text = "药品名称";
			this.clhMedName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clhMedName.Width = 100;
			// 
			// clhMedSpec
			// 
			this.clhMedSpec.Text = "药品规格";
			this.clhMedSpec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clhMedSpec.Width = 100;
			// 
			// clhUnit
			// 
			this.clhUnit.Text = "单位";
			this.clhUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clhUnit.Width = 100;
			// 
			// 原价格
			// 
			this.原价格.Text = "原价格";
			// 
			// 调整后价格
			// 
			this.调整后价格.Text = "调整后价格";
			this.调整后价格.Width = 88;
			// 
			// 数量
			// 
			this.数量.Text = "数量";
			// 
			// 差额
			// 
			this.差额.Text = "差额";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel2.Controls.Add(this.label23);
			this.panel2.Controls.Add(this.label22);
			this.panel2.Controls.Add(this.label21);
			this.panel2.Controls.Add(this.label20);
			this.panel2.Controls.Add(this.label19);
			this.panel2.Controls.Add(this.label18);
			this.panel2.Location = new System.Drawing.Point(-2, 600);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1024, 24);
			this.panel2.TabIndex = 149;
			// 
			// label23
			// 
			this.label23.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label23.ForeColor = System.Drawing.SystemColors.Desktop;
			this.label23.Location = new System.Drawing.Point(823, 0);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(176, 23);
			this.label23.TabIndex = 5;
			this.label23.Text = "F10使查找具有输入焦点";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label22
			// 
			this.label22.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label22.ForeColor = System.Drawing.SystemColors.Desktop;
			this.label22.Location = new System.Drawing.Point(592, 0);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(216, 23);
			this.label22.TabIndex = 4;
			this.label22.Text = "F9使药品查找输入框具有焦点";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label21
			// 
			this.label21.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label21.ForeColor = System.Drawing.SystemColors.Desktop;
			this.label21.Location = new System.Drawing.Point(441, 0);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(136, 23);
			this.label21.TabIndex = 3;
			this.label21.Text = "F8选中明细窗体";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label20
			// 
			this.label20.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label20.ForeColor = System.Drawing.SystemColors.Desktop;
			this.label20.Location = new System.Drawing.Point(274, 0);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(152, 23);
			this.label20.TabIndex = 2;
			this.label20.Text = "F7选中己审核窗体";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label19.ForeColor = System.Drawing.SystemColors.Desktop;
			this.label19.Location = new System.Drawing.Point(123, 0);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(136, 23);
			this.label19.TabIndex = 1;
			this.label19.Text = "F6选中未审核窗体";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label18
			// 
			this.label18.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label18.ForeColor = System.Drawing.SystemColors.Desktop;
			this.label18.Location = new System.Drawing.Point(8, 0);
			this.label18.Name = "label18";
			this.label18.TabIndex = 0;
			this.label18.Text = "快捷键提示:";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// m_cboSelPeriod
			// 
			this.m_cboSelPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_cboSelPeriod.Location = new System.Drawing.Point(152, 560);
			this.m_cboSelPeriod.Name = "m_cboSelPeriod";
			this.m_cboSelPeriod.Size = new System.Drawing.Size(176, 22);
			this.m_cboSelPeriod.TabIndex = 151;
			// 
			// frmMedStorageAcct
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(1024, 621);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.Name = "frmMedStorageAcct";
			this.Text = "登帐";
			this.Load += new System.EventHandler(this.frmMedStorageAcct_Load);
			this.panel5.ResumeLayout(false);
			this.tabAccpt.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			// TODO:  添加 frmStorageAcct.CreateController 实现
			this.objController = new clsControlMedStorageAcct();
			this.objController.Set_GUI_Apperance(this);
		}
		public void ShowByStorageTypeAndOrdType(string StorageType,string OrdType)
		{

		}
		private void frmMedStorageAcct_Load(object sender, System.EventArgs e)
		{
			((clsControlMedStorageAcct)this.objController).m_lngResetFrm();
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void tabAccpt_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(tabAccpt.SelectedIndex==1)
				this.btnAcct.Enabled=false;
			else
				this.btnAcct.Enabled=true;
		}

		private void m_lsvUnAccet_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageAcct)this.objController).m_lngShowDe();
		}
	}
}
