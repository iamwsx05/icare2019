using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmStorageSetup 的摘要说明。
	/// </summary>
	public class frmStorageSetup : com.digitalwave.GUI_Base.frmMDI_Child_Base	//GUI_Base.dll
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		internal System.Windows.Forms.ComboBox CobStorage;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox txtBuyTol;
		internal System.Windows.Forms.TextBox txtTolMoney;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		internal System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		internal System.Windows.Forms.TextBox FxtFindMed;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		internal System.Windows.Forms.TextBox txtMedName;
		private System.Windows.Forms.Label label6;
		internal System.Windows.Forms.TextBox txtSpece;
		private System.Windows.Forms.Label label7;
		internal System.Windows.Forms.TextBox textBox1;
		internal System.Windows.Forms.TextBox txtUnit;
		internal System.Windows.Forms.TextBox m_txtBuyPrice;
		internal System.Windows.Forms.TextBox m_txtQty;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		internal System.Windows.Forms.TextBox m_txtLotNo;
		internal System.Windows.Forms.TextBox m_txtUnitPrice;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label8;
		internal System.Windows.Forms.TextBox m_txtProductor;
		internal System.Windows.Forms.DateTimePicker m_dtpUsefulLife;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private PinkieControls.ButtonXP m_cmdOK;
		internal PinkieControls.ButtonXP btnClear;
		private System.Windows.Forms.Panel panel5;
		internal System.Windows.Forms.ListView m_lsvDetail;
		internal System.Windows.Forms.ColumnHeader clhMedicineID;
		internal System.Windows.Forms.ColumnHeader clhMedicineName;
		internal System.Windows.Forms.ColumnHeader clhMedicineSpec;
		internal System.Windows.Forms.ColumnHeader clhUnit;
		internal System.Windows.Forms.ColumnHeader clhProductort;
		internal System.Windows.Forms.ColumnHeader clhLotNo;
		internal System.Windows.Forms.ColumnHeader clhUsefulLife;
		internal System.Windows.Forms.ColumnHeader clhQty;
		internal System.Windows.Forms.ColumnHeader clhBuyPrice;
		internal System.Windows.Forms.ColumnHeader clhUnitPrice;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		internal System.Windows.Forms.ListView LSVVendor;
		private System.Windows.Forms.ColumnHeader 厂家代码;
		private System.Windows.Forms.ColumnHeader 厂家名称;
		private System.Windows.Forms.Label label18;
		internal System.Windows.Forms.TextBox m_txtWHOLESALEPRICE;
		internal com.digitalwave.controls.ControlMedicineFind controlMedicineFind;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmStorageSetup()
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtTolMoney = new System.Windows.Forms.TextBox();
			this.txtBuyTol = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.CobStorage = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.m_lsvDetail = new System.Windows.Forms.ListView();
			this.clhMedicineID = new System.Windows.Forms.ColumnHeader();
			this.clhMedicineName = new System.Windows.Forms.ColumnHeader();
			this.clhMedicineSpec = new System.Windows.Forms.ColumnHeader();
			this.clhUnit = new System.Windows.Forms.ColumnHeader();
			this.clhProductort = new System.Windows.Forms.ColumnHeader();
			this.clhLotNo = new System.Windows.Forms.ColumnHeader();
			this.clhUsefulLife = new System.Windows.Forms.ColumnHeader();
			this.clhQty = new System.Windows.Forms.ColumnHeader();
			this.clhBuyPrice = new System.Windows.Forms.ColumnHeader();
			this.clhUnitPrice = new System.Windows.Forms.ColumnHeader();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.m_txtWHOLESALEPRICE = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.btnClear = new PinkieControls.ButtonXP();
			this.m_cmdOK = new PinkieControls.ButtonXP();
			this.m_txtProductor = new System.Windows.Forms.TextBox();
			this.m_dtpUsefulLife = new System.Windows.Forms.DateTimePicker();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.m_txtLotNo = new System.Windows.Forms.TextBox();
			this.m_txtUnitPrice = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.m_txtBuyPrice = new System.Windows.Forms.TextBox();
			this.m_txtQty = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.txtUnit = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.txtSpece = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtMedName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.FxtFindMed = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.panel5 = new System.Windows.Forms.Panel();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.LSVVendor = new System.Windows.Forms.ListView();
			this.厂家代码 = new System.Windows.Forms.ColumnHeader();
			this.厂家名称 = new System.Windows.Forms.ColumnHeader();
			this.controlMedicineFind = new com.digitalwave.controls.ControlMedicineFind();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1000, 64);
			this.panel1.TabIndex = 1;
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BackColor = System.Drawing.SystemColors.Control;
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.txtTolMoney);
			this.panel2.Controls.Add(this.txtBuyTol);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.CobStorage);
			this.panel2.Location = new System.Drawing.Point(2, 2);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(992, 56);
			this.panel2.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(256, 15);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 24);
			this.label3.TabIndex = 5;
			this.label3.Text = "买入总价";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(488, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 24);
			this.label2.TabIndex = 4;
			this.label2.Text = "零售总价";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtTolMoney
			// 
			this.txtTolMoney.BackColor = System.Drawing.SystemColors.Window;
			this.txtTolMoney.Enabled = false;
			this.txtTolMoney.Location = new System.Drawing.Point(568, 16);
			this.txtTolMoney.Name = "txtTolMoney";
			this.txtTolMoney.ReadOnly = true;
			this.txtTolMoney.Size = new System.Drawing.Size(96, 23);
			this.txtTolMoney.TabIndex = 3;
			this.txtTolMoney.Text = "";
			// 
			// txtBuyTol
			// 
			this.txtBuyTol.BackColor = System.Drawing.SystemColors.Window;
			this.txtBuyTol.Enabled = false;
			this.txtBuyTol.Location = new System.Drawing.Point(328, 16);
			this.txtBuyTol.Name = "txtBuyTol";
			this.txtBuyTol.ReadOnly = true;
			this.txtBuyTol.Size = new System.Drawing.Size(128, 23);
			this.txtBuyTol.TabIndex = 2;
			this.txtBuyTol.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "仓    库";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CobStorage
			// 
			this.CobStorage.Location = new System.Drawing.Point(96, 16);
			this.CobStorage.Name = "CobStorage";
			this.CobStorage.Size = new System.Drawing.Size(121, 22);
			this.CobStorage.TabIndex = 0;
			this.CobStorage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CobStorage_KeyDown);
			this.CobStorage.SelectedIndexChanged += new System.EventHandler(this.CobStorage_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox1.Controls.Add(this.m_lsvDetail);
			this.groupBox1.Location = new System.Drawing.Point(0, 72);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1000, 208);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// m_lsvDetail
			// 
			this.m_lsvDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lsvDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.clhMedicineID,
																						  this.clhMedicineName,
																						  this.clhMedicineSpec,
																						  this.clhUnit,
																						  this.clhProductort,
																						  this.clhLotNo,
																						  this.clhUsefulLife,
																						  this.clhQty,
																						  this.clhBuyPrice,
																						  this.clhUnitPrice});
			this.m_lsvDetail.FullRowSelect = true;
			this.m_lsvDetail.GridLines = true;
			this.m_lsvDetail.Location = new System.Drawing.Point(0, 52);
			this.m_lsvDetail.Name = "m_lsvDetail";
			this.m_lsvDetail.Size = new System.Drawing.Size(995, 148);
			this.m_lsvDetail.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.m_lsvDetail.TabIndex = 2;
			this.m_lsvDetail.View = System.Windows.Forms.View.Details;
			// 
			// clhMedicineID
			// 
			this.clhMedicineID.Text = "药品代码";
			this.clhMedicineID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clhMedicineID.Width = 100;
			// 
			// clhMedicineName
			// 
			this.clhMedicineName.Text = "药品名称";
			this.clhMedicineName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clhMedicineName.Width = 255;
			// 
			// clhMedicineSpec
			// 
			this.clhMedicineSpec.Text = "规格";
			this.clhMedicineSpec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clhMedicineSpec.Width = 192;
			// 
			// clhUnit
			// 
			this.clhUnit.Text = "单位";
			this.clhUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clhUnit.Width = 80;
			// 
			// clhProductort
			// 
			this.clhProductort.Text = "产地";
			this.clhProductort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clhProductort.Width = 150;
			// 
			// clhLotNo
			// 
			this.clhLotNo.Text = "批号";
			this.clhLotNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clhLotNo.Width = 150;
			// 
			// clhUsefulLife
			// 
			this.clhUsefulLife.Text = "有效时间";
			this.clhUsefulLife.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clhUsefulLife.Width = 150;
			// 
			// clhQty
			// 
			this.clhQty.Text = "数量";
			this.clhQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// clhBuyPrice
			// 
			this.clhBuyPrice.Text = "买入价";
			this.clhBuyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clhBuyPrice.Width = 80;
			// 
			// clhUnitPrice
			// 
			this.clhUnitPrice.Text = "零售价";
			this.clhUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clhUnitPrice.Width = 80;
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.BackColor = System.Drawing.SystemColors.Control;
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel3.Controls.Add(this.panel4);
			this.panel3.Location = new System.Drawing.Point(0, 288);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(1000, 144);
			this.panel3.TabIndex = 3;
			// 
			// panel4
			// 
			this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel4.BackColor = System.Drawing.SystemColors.Control;
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel4.Controls.Add(this.m_txtWHOLESALEPRICE);
			this.panel4.Controls.Add(this.label18);
			this.panel4.Controls.Add(this.btnClear);
			this.panel4.Controls.Add(this.m_cmdOK);
			this.panel4.Controls.Add(this.m_txtProductor);
			this.panel4.Controls.Add(this.m_dtpUsefulLife);
			this.panel4.Controls.Add(this.label12);
			this.panel4.Controls.Add(this.label13);
			this.panel4.Controls.Add(this.m_txtLotNo);
			this.panel4.Controls.Add(this.m_txtUnitPrice);
			this.panel4.Controls.Add(this.label11);
			this.panel4.Controls.Add(this.label8);
			this.panel4.Controls.Add(this.m_txtBuyPrice);
			this.panel4.Controls.Add(this.m_txtQty);
			this.panel4.Controls.Add(this.label10);
			this.panel4.Controls.Add(this.label9);
			this.panel4.Controls.Add(this.txtUnit);
			this.panel4.Controls.Add(this.label7);
			this.panel4.Controls.Add(this.label6);
			this.panel4.Controls.Add(this.txtSpece);
			this.panel4.Controls.Add(this.label5);
			this.panel4.Controls.Add(this.txtMedName);
			this.panel4.Controls.Add(this.label4);
			this.panel4.Controls.Add(this.FxtFindMed);
			this.panel4.Location = new System.Drawing.Point(2, 2);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(992, 136);
			this.panel4.TabIndex = 0;
			// 
			// m_txtWHOLESALEPRICE
			// 
			this.m_txtWHOLESALEPRICE.BackColor = System.Drawing.SystemColors.Window;
			//this.m_txtWHOLESALEPRICE.EnableAutoValidation = false;
			//this.m_txtWHOLESALEPRICE.EnableEnterKeyValidate = true;
			//this.m_txtWHOLESALEPRICE.EnableEscapeKeyUndo = true;
			//this.m_txtWHOLESALEPRICE.EnableLastValidValue = true;
			//this.m_txtWHOLESALEPRICE.ErrorProvider = null;
			//this.m_txtWHOLESALEPRICE.ErrorProviderMessage = "Invalid value";
			//this.m_txtWHOLESALEPRICE.ForceFormatText = true;
			this.m_txtWHOLESALEPRICE.Location = new System.Drawing.Point(568, 56);
			this.m_txtWHOLESALEPRICE.Name = "m_txtWHOLESALEPRICE";
			//this.m_txtWHOLESALEPRICE.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
			this.m_txtWHOLESALEPRICE.Size = new System.Drawing.Size(96, 23);
			this.m_txtWHOLESALEPRICE.TabIndex = 17;
			this.m_txtWHOLESALEPRICE.Text = "0.00";
			this.m_txtWHOLESALEPRICE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.m_txtWHOLESALEPRICE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtWHOLESALEPRICE_KeyDown);
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(488, 56);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(63, 19);
			this.label18.TabIndex = 29;
			this.label18.Text = "批 发 价";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnClear
			// 
			this.btnClear.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btnClear.DefaultScheme = true;
			this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnClear.Hint = "";
			this.btnClear.Location = new System.Drawing.Point(880, 91);
			this.btnClear.Name = "btnClear";
			this.btnClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btnClear.Size = new System.Drawing.Size(96, 32);
			this.btnClear.TabIndex = 22;
			this.btnClear.Text = "清空(&C)";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// m_cmdOK
			// 
			this.m_cmdOK.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_cmdOK.DefaultScheme = true;
			this.m_cmdOK.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_cmdOK.Hint = "";
			this.m_cmdOK.Location = new System.Drawing.Point(704, 91);
			this.m_cmdOK.Name = "m_cmdOK";
			this.m_cmdOK.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_cmdOK.Size = new System.Drawing.Size(96, 32);
			this.m_cmdOK.TabIndex = 21;
			this.m_cmdOK.Text = "确定(&S)";
			this.m_cmdOK.Click += new System.EventHandler(this.m_cmdOK_Click);
			// 
			// m_txtProductor
			// 
			this.m_txtProductor.BackColor = System.Drawing.SystemColors.Window;
			//this.m_txtProductor.EnableAutoValidation = false;
			//this.m_txtProductor.EnableEnterKeyValidate = true;
			//this.m_txtProductor.EnableEscapeKeyUndo = true;
			//this.m_txtProductor.EnableLastValidValue = true;
			//this.m_txtProductor.ErrorProvider = null;
			//this.m_txtProductor.ErrorProviderMessage = "Invalid value";
			//this.m_txtProductor.ForceFormatText = true;
			this.m_txtProductor.Location = new System.Drawing.Point(328, 96);
			this.m_txtProductor.Name = "m_txtProductor";
			this.m_txtProductor.Size = new System.Drawing.Size(336, 23);
			this.m_txtProductor.TabIndex = 20;
			this.m_txtProductor.Text = "";
			this.m_txtProductor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtProductor_KeyDown);
			this.m_txtProductor.Enter += new System.EventHandler(this.m_txtProductor_Enter);
			// 
			// m_dtpUsefulLife
			// 
			this.m_dtpUsefulLife.Location = new System.Drawing.Point(96, 96);
			this.m_dtpUsefulLife.MaxDate = new System.DateTime(2049, 12, 31, 0, 0, 0, 0);
			this.m_dtpUsefulLife.MinDate = new System.DateTime(2004, 5, 1, 0, 0, 0, 0);
			this.m_dtpUsefulLife.Name = "m_dtpUsefulLife";
			this.m_dtpUsefulLife.Size = new System.Drawing.Size(120, 23);
			this.m_dtpUsefulLife.TabIndex = 19;
			this.m_dtpUsefulLife.Enter += new System.EventHandler(this.m_dtpUsefulLife_Enter);
			this.m_dtpUsefulLife.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpUsefulLife_KeyDown);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(256, 104);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(63, 19);
			this.label12.TabIndex = 24;
			this.label12.Text = "产    地";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(16, 98);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(63, 19);
			this.label13.TabIndex = 23;
			this.label13.Text = "有 效 期";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// m_txtLotNo
			// 
			//this.m_txtLotNo.EnableAutoValidation = false;
			//this.m_txtLotNo.EnableEnterKeyValidate = true;
			//this.m_txtLotNo.EnableEscapeKeyUndo = true;
			//this.m_txtLotNo.EnableLastValidValue = true;
			//this.m_txtLotNo.ErrorProvider = null;
			//this.m_txtLotNo.ErrorProviderMessage = "Invalid value";
			//this.m_txtLotNo.ForceFormatText = true;
			this.m_txtLotNo.Location = new System.Drawing.Point(800, 56);
			this.m_txtLotNo.Name = "m_txtLotNo";
			this.m_txtLotNo.Size = new System.Drawing.Size(176, 23);
			this.m_txtLotNo.TabIndex = 18;
			this.m_txtLotNo.Text = "";
			this.m_txtLotNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtLotNo_KeyDown);
			this.m_txtLotNo.Enter += new System.EventHandler(this.m_txtLotNo_Enter);
			// 
			// m_txtUnitPrice
			// 
			this.m_txtUnitPrice.BackColor = System.Drawing.SystemColors.Window;
			//this.m_txtUnitPrice.EnableAutoValidation = false;
			//this.m_txtUnitPrice.EnableEnterKeyValidate = true;
			//this.m_txtUnitPrice.EnableEscapeKeyUndo = true;
			//this.m_txtUnitPrice.EnableLastValidValue = true;
			//this.m_txtUnitPrice.ErrorProvider = null;
			//this.m_txtUnitPrice.ErrorProviderMessage = "Invalid value";
			//this.m_txtUnitPrice.ForceFormatText = true;
			this.m_txtUnitPrice.Location = new System.Drawing.Point(400, 56);
			this.m_txtUnitPrice.Name = "m_txtUnitPrice";
			//this.m_txtUnitPrice.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
			this.m_txtUnitPrice.Size = new System.Drawing.Size(80, 23);
			this.m_txtUnitPrice.TabIndex = 16;
			this.m_txtUnitPrice.Text = "0.00";
			this.m_txtUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.m_txtUnitPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtUnitPrice_KeyDown);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(336, 58);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(63, 19);
			this.label11.TabIndex = 22;
			this.label11.Text = "零 售 价";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(704, 58);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(70, 19);
			this.label8.TabIndex = 18;
			this.label8.Text = "批     号";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// m_txtBuyPrice
			// 
			//this.m_txtBuyPrice.EnableAutoValidation = false;
			//this.m_txtBuyPrice.EnableEnterKeyValidate = true;
			//this.m_txtBuyPrice.EnableEscapeKeyUndo = true;
			//this.m_txtBuyPrice.EnableLastValidValue = true;
			//this.m_txtBuyPrice.ErrorProvider = null;
			//this.m_txtBuyPrice.ErrorProviderMessage = "Invalid value";
			//this.m_txtBuyPrice.ForceFormatText = true;
			this.m_txtBuyPrice.Location = new System.Drawing.Point(256, 56);
			this.m_txtBuyPrice.MaxLength = 10;
			this.m_txtBuyPrice.Name = "m_txtBuyPrice";
			//this.m_txtBuyPrice.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
			this.m_txtBuyPrice.Size = new System.Drawing.Size(64, 23);
			this.m_txtBuyPrice.TabIndex = 15;
			this.m_txtBuyPrice.Text = "0.00";
			this.m_txtBuyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.m_txtBuyPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBuyPrice_KeyDown);
			this.m_txtBuyPrice.Enter += new System.EventHandler(this.m_txtBuyPrice_Enter);
			// 
			// m_txtQty
			// 
			//this.m_txtQty.EnableAutoValidation = false;
			//this.m_txtQty.EnableEnterKeyValidate = true;
			//this.m_txtQty.EnableEscapeKeyUndo = true;
			//this.m_txtQty.EnableLastValidValue = true;
			//this.m_txtQty.ErrorProvider = null;
			//this.m_txtQty.ErrorProviderMessage = "Invalid value";
			//this.m_txtQty.ForceFormatText = true;
			this.m_txtQty.Location = new System.Drawing.Point(96, 56);
			this.m_txtQty.MaxLength = 5;
			this.m_txtQty.Name = "m_txtQty";
			//this.m_txtQty.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
			this.m_txtQty.Size = new System.Drawing.Size(80, 23);
			this.m_txtQty.TabIndex = 14;
			this.m_txtQty.Text = "0";
			this.m_txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.m_txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtQty_KeyDown);
			this.m_txtQty.Enter += new System.EventHandler(this.m_txtQty_Enter);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(184, 56);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(63, 19);
			this.label10.TabIndex = 17;
			this.label10.Text = "买 入 价";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(16, 58);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(63, 19);
			this.label9.TabIndex = 16;
			this.label9.Text = "数    量";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtUnit
			// 
			this.txtUnit.BackColor = System.Drawing.SystemColors.Window;
			this.txtUnit.Enabled = false;
			this.txtUnit.Location = new System.Drawing.Point(568, 16);
			this.txtUnit.Name = "txtUnit";
			this.txtUnit.ReadOnly = true;
			this.txtUnit.Size = new System.Drawing.Size(96, 23);
			this.txtUnit.TabIndex = 13;
			this.txtUnit.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(488, 15);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 24);
			this.label7.TabIndex = 12;
			this.label7.Text = "单     位";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(704, 15);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 24);
			this.label6.TabIndex = 10;
			this.label6.Text = "药品规格";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtSpece
			// 
			this.txtSpece.BackColor = System.Drawing.SystemColors.Window;
			this.txtSpece.Enabled = false;
			this.txtSpece.Location = new System.Drawing.Point(800, 16);
			this.txtSpece.Name = "txtSpece";
			this.txtSpece.ReadOnly = true;
			this.txtSpece.Size = new System.Drawing.Size(176, 23);
			this.txtSpece.TabIndex = 9;
			this.txtSpece.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(256, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 24);
			this.label5.TabIndex = 8;
			this.label5.Text = "药品名称";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtMedName
			// 
			this.txtMedName.BackColor = System.Drawing.SystemColors.Window;
			this.txtMedName.Enabled = false;
			this.txtMedName.Location = new System.Drawing.Point(336, 16);
			this.txtMedName.Name = "txtMedName";
			this.txtMedName.ReadOnly = true;
			this.txtMedName.Size = new System.Drawing.Size(128, 23);
			this.txtMedName.TabIndex = 7;
			this.txtMedName.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 15);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 24);
			this.label4.TabIndex = 6;
			this.label4.Text = "查找药品";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FxtFindMed
			// 
			this.FxtFindMed.Location = new System.Drawing.Point(96, 16);
			this.FxtFindMed.Name = "FxtFindMed";
			this.FxtFindMed.Size = new System.Drawing.Size(120, 23);
			this.FxtFindMed.TabIndex = 0;
			this.FxtFindMed.Text = "";
			this.FxtFindMed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FxtFindMed_KeyDown);
			this.FxtFindMed.TextChanged += new System.EventHandler(this.FxtFindMed_TextChanged);
			this.FxtFindMed.Enter += new System.EventHandler(this.FxtFindMed_Enter);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.Name = "textBox1";
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			// 
			// panel5
			// 
			this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel5.Controls.Add(this.label17);
			this.panel5.Controls.Add(this.label16);
			this.panel5.Controls.Add(this.label15);
			this.panel5.Controls.Add(this.label14);
			this.panel5.Location = new System.Drawing.Point(0, 440);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(1000, 40);
			this.panel5.TabIndex = 4;
			// 
			// label17
			// 
			this.label17.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label17.ForeColor = System.Drawing.SystemColors.Desktop;
			this.label17.Location = new System.Drawing.Point(470, 8);
			this.label17.Name = "label17";
			this.label17.TabIndex = 3;
			this.label17.Text = "Esc—退出系统";
			// 
			// label16
			// 
			this.label16.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label16.ForeColor = System.Drawing.SystemColors.Desktop;
			this.label16.Location = new System.Drawing.Point(316, 8);
			this.label16.Name = "label16";
			this.label16.TabIndex = 2;
			this.label16.Text = "F2—查找药品";
			// 
			// label15
			// 
			this.label15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label15.ForeColor = System.Drawing.SystemColors.Desktop;
			this.label15.Location = new System.Drawing.Point(162, 8);
			this.label15.Name = "label15";
			this.label15.TabIndex = 1;
			this.label15.Text = "F1—选择仓库";
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label14.ForeColor = System.Drawing.SystemColors.Desktop;
			this.label14.Location = new System.Drawing.Point(8, 8);
			this.label14.Name = "label14";
			this.label14.TabIndex = 0;
			this.label14.Text = "系统提示：";
			// 
			// LSVVendor
			// 
			this.LSVVendor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.厂家代码,
																						this.厂家名称});
			this.LSVVendor.FullRowSelect = true;
			this.LSVVendor.GridLines = true;
			this.LSVVendor.HideSelection = false;
			this.LSVVendor.Location = new System.Drawing.Point(336, 536);
			this.LSVVendor.Name = "LSVVendor";
			this.LSVVendor.Size = new System.Drawing.Size(232, 97);
			this.LSVVendor.TabIndex = 151;
			this.LSVVendor.View = System.Windows.Forms.View.Details;
			this.LSVVendor.Visible = false;
			this.LSVVendor.Click += new System.EventHandler(this.LSVVendor_Click);
			// 
			// 厂家代码
			// 
			this.厂家代码.Text = "厂家代码";
			this.厂家代码.Width = 101;
			// 
			// 厂家名称
			// 
			this.厂家名称.Text = "厂家名称";
			this.厂家名称.Width = 116;
			// 
			// controlMedicineFind
			// 
			this.controlMedicineFind.blISOutStorage = false;
			this.controlMedicineFind.blRepertory = false;
			this.controlMedicineFind.FindMedmode = 0;
			this.controlMedicineFind.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.controlMedicineFind.isShowFindType = true;
			this.controlMedicineFind.Location = new System.Drawing.Point(96, 264);
			this.controlMedicineFind.Name = "controlMedicineFind";
			this.controlMedicineFind.Size = new System.Drawing.Size(576, 336);
			this.controlMedicineFind.strSTORAGEID = "-1";
			this.controlMedicineFind.TabIndex = 30;
			this.controlMedicineFind.Visible = false;
			this.controlMedicineFind.m_evtReturnVal += new com.digitalwave.controls.dlgReturnVal(this.controlMedicineFind_m_evtReturnVal);
			// 
			// frmStorageSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(1000, 485);
			this.Controls.Add(this.controlMedicineFind);
			this.Controls.Add(this.LSVVendor);
			this.Controls.Add(this.panel5);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.Name = "frmStorageSetup";
			this.Text = "库存初始化";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmStorageSetup_KeyDown);
			this.Load += new System.EventHandler(this.frmStorageSetup_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmStorageSetup_Load(object sender, System.EventArgs e)
		{
			((clsControlStorageSetupStar)this.objController).m_lngResetFrm();
			this.m_mthSetEnter2Tab(new System.Windows.Forms.Control[] {});

		}

		#region 设置窗体
		public override void CreateController()
		{
			this.objController = new clsControlStorageSetupStar();
			this.objController.Set_GUI_Apperance(this);
		}
		#endregion

		private void FxtFindMed_Enter(object sender, System.EventArgs e)
		{
//			if(dgrMedicine.Visible==false)
			((clsControlStorageSetupStar)this.objController).m_lngFillDgr();
		}

		private void FxtFindMed_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
//			if(e.KeyCode==Keys.Up)
//			{
//				if(dgrMedicine.RowCount>0&&dgrMedicine.CurrentCell.RowNumber!=0)
//				{
//					dgrMedicine.CurrentCell=new DataGridCell(dgrMedicine.CurrentCell.RowNumber-1,0);
//				}
//			}
//			if(e.KeyCode==Keys.Down)
//			{
//				if(dgrMedicine.RowCount>0&&dgrMedicine.CurrentCell.RowNumber<dgrMedicine.RowCount)
//				{
//					dgrMedicine.CurrentCell=new DataGridCell(dgrMedicine.CurrentCell.RowNumber+1,0);
//				}
//			}
//			Application.DoEvents();
//			FxtFindMed.Focus();
//
//			if(e.KeyCode==Keys.Enter)
//			{
//				if(dgrMedicine.Visible==true&&dgrMedicine.RowCount>0)
//				{
//					((clsControlStorageSetupStar)this.objController).m_lngSeleMed();				
//					this.m_txtQty.Focus();
//				}
//			}
		}

		private void FxtFindMed_TextChanged(object sender, System.EventArgs e)
		{
			//((clsControlStorageSetupStar)this.objController).m_lngFind();
		}

		private void dgrMedicine_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
		{
			((clsControlStorageSetupStar)this.objController).m_lngSeleMed();
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageSetupStar)this.objController).m_lngClear();
		}

		private void CobStorage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlStorageSetupStar)this.objController).m_lngCobChang();
		}

		private void m_txtProductor_Enter(object sender, System.EventArgs e)
		{
		    ((clsControlStorageSetupStar)this.objController). m_lngGetVenDor();
		}

		private void m_txtProductor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			LSVVendor.MultiSelect = false;
			if(e.KeyCode==Keys.Up)
			{
				if(LSVVendor.Items.Count>0&&LSVVendor.SelectedItems[0].Index!=0)
				{
					LSVVendor.Items[LSVVendor.SelectedItems[0].Index-1].Selected=true;
				}
			}
			if(e.KeyCode==Keys.Down)
			{
				if(LSVVendor.Items.Count>0&&LSVVendor.SelectedItems[0].Index<LSVVendor.Items.Count-1)
				{
					LSVVendor.Items[LSVVendor.SelectedItems[0].Index+1].Selected=true;
				}
			}

			if(e.KeyCode==Keys.Enter)
			{
				this.m_txtProductor.Tag=this.LSVVendor.SelectedItems[0].SubItems[0].Text.Trim();
				this.m_txtProductor.Text=this.LSVVendor.SelectedItems[0].SubItems[1].Text.Trim();
				this.LSVVendor.Visible=false;
				this.m_cmdOK.Focus();
			}
		}

		private void LSVVendor_Click(object sender, System.EventArgs e)
		{
			this.m_txtProductor.Tag=this.LSVVendor.SelectedItems[0].SubItems[0].Text.Trim();
			this.m_txtProductor.Text=this.LSVVendor.SelectedItems[0].SubItems[1].Text.Trim();
			this.LSVVendor.Visible=false;
			this.m_cmdOK.Focus();
		}

		private void m_txtQty_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtBuyPrice_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtLotNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_dtpUsefulLife_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
			if(e.KeyCode==Keys.Enter)
				m_cmdOK.Focus();
		}

		private void m_cmdOK_Click(object sender, System.EventArgs e)
		{
			if(txtMedName.Text == "")
			{
				MessageBox.Show("请选择药品");
				return ;
			}
			double dQty= 0;
			double dBuyPrice = 0;
			double dWPrice = 0;
			try
			{
				dQty = double.Parse(m_txtQty.Text);
				dBuyPrice = double.Parse(m_txtBuyPrice.Text);
				dWPrice = double.Parse(m_txtWHOLESALEPRICE.Text);
			}
			catch
			{
			}
			if(dQty == 0 || dBuyPrice == 0|| dWPrice == 0)
			{
				MessageBox.Show("数量或单价不能为空");
				return ;
			}
			if(m_txtLotNo.Text == "")
			{
				MessageBox.Show("批次不能为空");
				return;
			}
			((clsControlStorageSetupStar)this.objController).m_lngSaveDataRow();
		}

		private void m_txtQty_Enter(object sender, System.EventArgs e)
		{
			this.LSVVendor.Visible=false;
//			this.dgrMedicine.Visible=false;
		}

		private void m_txtBuyPrice_Enter(object sender, System.EventArgs e)
		{
			this.LSVVendor.Visible=false;
//			this.dgrMedicine.Visible=false;
		}

		private void m_dtpUsefulLife_Enter(object sender, System.EventArgs e)
		{
			this.LSVVendor.Visible=false;
//			this.dgrMedicine.Visible=false;
		}

		private void m_txtLotNo_Enter(object sender, System.EventArgs e)
		{
			this.LSVVendor.Visible=false;
//			this.dgrMedicine.Visible=false;
		}

		private void frmStorageSetup_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		    if(e.KeyCode==Keys.F1)
				CobStorage.Focus();
			if(e.KeyCode==Keys.F2)
				FxtFindMed.Focus();
			if(e.KeyCode==Keys.Escape)
				this.Close();
		}

		private void CobStorage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.F1)
				CobStorage.Focus();
			if(e.KeyCode==Keys.F2)
				FxtFindMed.Focus();
			if(e.KeyCode==Keys.Escape)
				this.Close();
		}		

		private void m_txtUnitPrice_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtWHOLESALEPRICE_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void controlMedicineFind_m_evtReturnVal(object sender, com.digitalwave.controls.clsEvtReturnVal e)
		{
			controlMedicineFind.Visible = false;
			((clsControlStorageSetupStar)this.objController).m_mthFillTxtBox(e);
			m_txtQty.Focus();
		}
	}
}
