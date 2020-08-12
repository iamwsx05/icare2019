using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmStorageInit 的摘要说明。
	/// </summary>
	public class frmStorageInit : com.digitalwave.GUI_Base.frmMDI_Child_Base	//GUI_Base.dll
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		internal PinkieControls.ButtonXP m_cmdOK;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox3;
		internal System.Windows.Forms.TextBox m_txtFindPharm;
		internal System.Windows.Forms.TextBox m_txtPharmName;
		internal System.Windows.Forms.TextBox m_txtUnit;
		private PinkieControls.ButtonXP m_BtnClear;
		private PinkieControls.ButtonXP m_BtnSave;
		internal PinkieControls.ButtonXP m_BtnAudit;
		private PinkieControls.ButtonXP m_BtnExit;
		internal System.Windows.Forms.TextBox m_txtLotNo;
		internal System.Windows.Forms.TextBox m_txtPharmSpec;
		private PinkieControls.ButtonXP m_BtnDel;
		internal com.digitalwave.controls.ControlMedicineFind m_DlgResult;
		internal string strStorageFlag;
		private System.Windows.Forms.Label label5;
		internal com.digitalwave.controls.NumTextBox m_txtSalePrice;
		internal com.digitalwave.controls.NumTextBox m_txtTradePrice;
		internal com.digitalwave.controls.NumTextBox m_txtQty;
		internal com.digitalwave.controls.NumTextBox m_txtBuyPrice;
		internal System.Windows.Forms.CheckBox m_chkAudit;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label13;
		internal com.digitalwave.iCare.gui.HIS.exComboBox m_cmbStorage;
		internal NullableDateControls.MaskDateEdit m_dtpUseLife;
		internal System.Windows.Forms.Label m_txtVendor;
		private System.Windows.Forms.Label label14;
		internal PinkieControls.ButtonXP buttonXP1;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		private System.Windows.Forms.Label label15;
		internal System.Windows.Forms.ListView m_lsvDetail;
		internal System.Windows.Forms.ColumnHeader clhMedicineID;
		internal System.Windows.Forms.ColumnHeader clhMedicineName;
		internal System.Windows.Forms.ColumnHeader clhMedicineSpec;
		internal System.Windows.Forms.ColumnHeader clhLotNo;
		internal System.Windows.Forms.ColumnHeader clhUsefulLife;
		internal System.Windows.Forms.ColumnHeader clhQty;
		internal System.Windows.Forms.ColumnHeader clhUnit;
		internal System.Windows.Forms.ColumnHeader clhBuyPrice;
		internal System.Windows.Forms.ColumnHeader clhUnitPrice;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader clhTradePrice;
		internal System.Windows.Forms.ColumnHeader clhProductort;
		private System.Windows.Forms.Label label16;
		internal System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.GroupBox groupBox4;
		internal System.Windows.Forms.Label textBox1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label19;
		internal PinkieControls.ButtonXP buttonXP2;
		internal PinkieControls.ButtonXP buttonXP3;
		private System.Windows.Forms.Panel panel1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;		
		
		public frmStorageInit()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		#region 设置窗体
		public override void CreateController()
		{
			this.objController = new clsControlStorageInit();
			this.objController.Set_GUI_Apperance(this);
		}
		#endregion
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmStorageInit));
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.m_txtSalePrice = new com.digitalwave.controls.NumTextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.m_txtVendor = new System.Windows.Forms.Label();
			this.m_dtpUseLife = new NullableDateControls.MaskDateEdit();
			this.label13 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.m_txtBuyPrice = new com.digitalwave.controls.NumTextBox();
			this.m_txtTradePrice = new com.digitalwave.controls.NumTextBox();
			this.m_txtQty = new com.digitalwave.controls.NumTextBox();
			this.m_txtUnit = new System.Windows.Forms.TextBox();
			this.m_txtFindPharm = new System.Windows.Forms.TextBox();
			this.m_cmdOK = new PinkieControls.ButtonXP();
			this.label11 = new System.Windows.Forms.Label();
			this.m_txtLotNo = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.m_txtPharmSpec = new System.Windows.Forms.TextBox();
			this.m_txtPharmName = new System.Windows.Forms.TextBox();
			this.m_BtnClear = new PinkieControls.ButtonXP();
			this.m_BtnDel = new PinkieControls.ButtonXP();
			this.m_cmbStorage = new com.digitalwave.iCare.gui.HIS.exComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.buttonXP2 = new PinkieControls.ButtonXP();
			this.buttonXP3 = new PinkieControls.ButtonXP();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.m_lsvDetail = new System.Windows.Forms.ListView();
			this.clhMedicineID = new System.Windows.Forms.ColumnHeader();
			this.clhMedicineName = new System.Windows.Forms.ColumnHeader();
			this.clhMedicineSpec = new System.Windows.Forms.ColumnHeader();
			this.clhLotNo = new System.Windows.Forms.ColumnHeader();
			this.clhUsefulLife = new System.Windows.Forms.ColumnHeader();
			this.clhQty = new System.Windows.Forms.ColumnHeader();
			this.clhUnit = new System.Windows.Forms.ColumnHeader();
			this.clhBuyPrice = new System.Windows.Forms.ColumnHeader();
			this.clhUnitPrice = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.clhTradePrice = new System.Windows.Forms.ColumnHeader();
			this.clhProductort = new System.Windows.Forms.ColumnHeader();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.buttonXP1 = new PinkieControls.ButtonXP();
			this.m_chkAudit = new System.Windows.Forms.CheckBox();
			this.m_BtnAudit = new PinkieControls.ButtonXP();
			this.m_BtnSave = new PinkieControls.ButtonXP();
			this.m_BtnExit = new PinkieControls.ButtonXP();
			this.m_DlgResult = new com.digitalwave.controls.ControlMedicineFind();
			this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 24);
			this.label1.TabIndex = 2;
			this.label1.Text = "仓  库";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.m_txtSalePrice);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.m_txtVendor);
			this.groupBox2.Controls.Add(this.m_dtpUseLife);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.m_txtBuyPrice);
			this.groupBox2.Controls.Add(this.m_txtTradePrice);
			this.groupBox2.Controls.Add(this.m_txtQty);
			this.groupBox2.Controls.Add(this.m_txtUnit);
			this.groupBox2.Controls.Add(this.m_txtFindPharm);
			this.groupBox2.Controls.Add(this.m_cmdOK);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.m_txtLotNo);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.m_txtPharmSpec);
			this.groupBox2.Controls.Add(this.m_txtPharmName);
			this.groupBox2.Controls.Add(this.m_BtnClear);
			this.groupBox2.Controls.Add(this.m_BtnDel);
			this.groupBox2.Controls.Add(this.m_cmbStorage);
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(1008, 128);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			// 
			// m_txtSalePrice
			// 
			this.m_txtSalePrice.Location = new System.Drawing.Point(464, 56);
			this.m_txtSalePrice.MaxLength = 10;
			this.m_txtSalePrice.Name = "m_txtSalePrice";
			this.m_txtSalePrice.SendTabKey = false;
			this.m_txtSalePrice.SetFocusColor = System.Drawing.Color.White;
			this.m_txtSalePrice.Size = new System.Drawing.Size(240, 23);
			this.m_txtSalePrice.TabIndex = 5;
			this.m_txtSalePrice.Text = "";
			this.m_txtSalePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.m_txtSalePrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtSalePrice_KeyDown);
			// 
			// label14
			// 
			this.label14.BackColor = System.Drawing.Color.YellowGreen;
			this.label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label14.Location = new System.Drawing.Point(240, 118);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(392, 1);
			this.label14.TabIndex = 404;
			this.label14.Text = "label14";
			// 
			// m_txtVendor
			// 
			this.m_txtVendor.Location = new System.Drawing.Point(240, 97);
			this.m_txtVendor.Name = "m_txtVendor";
			this.m_txtVendor.Size = new System.Drawing.Size(392, 23);
			this.m_txtVendor.TabIndex = 403;
			this.m_txtVendor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// m_dtpUseLife
			// 
			this.m_dtpUseLife.Location = new System.Drawing.Point(56, 97);
			this.m_dtpUseLife.Mask = "yyyy年MM月dd日";
			this.m_dtpUseLife.Name = "m_dtpUseLife";
			this.m_dtpUseLife.Size = new System.Drawing.Size(120, 23);
			this.m_dtpUseLife.TabIndex = 8;
			this.m_dtpUseLife.Text = "";
			this.m_dtpUseLife.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpUseLife_KeyDown_1);
			// 
			// label13
			// 
			this.label13.BackColor = System.Drawing.Color.YellowGreen;
			this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label13.Location = new System.Drawing.Point(784, 34);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(216, 1);
			this.label13.TabIndex = 402;
			this.label13.Text = "label13";
			// 
			// label7
			// 
			this.label7.BackColor = System.Drawing.Color.YellowGreen;
			this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label7.Location = new System.Drawing.Point(464, 34);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(240, 1);
			this.label7.TabIndex = 401;
			this.label7.Text = "label7";
			// 
			// m_txtBuyPrice
			// 
			this.m_txtBuyPrice.Location = new System.Drawing.Point(240, 57);
			this.m_txtBuyPrice.MaxLength = 10;
			this.m_txtBuyPrice.Name = "m_txtBuyPrice";
			this.m_txtBuyPrice.SendTabKey = false;
			this.m_txtBuyPrice.SetFocusColor = System.Drawing.Color.White;
			this.m_txtBuyPrice.Size = new System.Drawing.Size(136, 23);
			this.m_txtBuyPrice.TabIndex = 4;
			this.m_txtBuyPrice.Text = "";
			this.m_txtBuyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.m_txtBuyPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBuyPrice_KeyDown);
			this.m_txtBuyPrice.Leave += new System.EventHandler(this.m_txtQty_Leave);
			// 
			// m_txtTradePrice
			// 
			this.m_txtTradePrice.Location = new System.Drawing.Point(632, 57);
			this.m_txtTradePrice.Name = "m_txtTradePrice";
			this.m_txtTradePrice.SendTabKey = false;
			this.m_txtTradePrice.SetFocusColor = System.Drawing.Color.White;
			this.m_txtTradePrice.Size = new System.Drawing.Size(72, 23);
			this.m_txtTradePrice.TabIndex = 6;
			this.m_txtTradePrice.Text = "";
			this.m_txtTradePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.m_txtTradePrice.Visible = false;
			this.m_txtTradePrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtTradePrice_KeyDown);
			// 
			// m_txtQty
			// 
			this.m_txtQty.Location = new System.Drawing.Point(56, 57);
			this.m_txtQty.MaxLength = 10;
			this.m_txtQty.Name = "m_txtQty";
			this.m_txtQty.SendTabKey = false;
			this.m_txtQty.SetFocusColor = System.Drawing.Color.White;
			this.m_txtQty.Size = new System.Drawing.Size(80, 23);
			this.m_txtQty.TabIndex = 3;
			this.m_txtQty.Text = "";
			this.m_txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.m_txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtQty_KeyUp);
			this.m_txtQty.Leave += new System.EventHandler(this.m_txtQty_Leave);
			// 
			// m_txtUnit
			// 
			this.m_txtUnit.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_txtUnit.Location = new System.Drawing.Point(144, 60);
			this.m_txtUnit.Name = "m_txtUnit";
			this.m_txtUnit.ReadOnly = true;
			this.m_txtUnit.Size = new System.Drawing.Size(32, 16);
			this.m_txtUnit.TabIndex = 400;
			this.m_txtUnit.Text = "";
			// 
			// m_txtFindPharm
			// 
			this.m_txtFindPharm.Location = new System.Drawing.Point(240, 17);
			this.m_txtFindPharm.Name = "m_txtFindPharm";
			this.m_txtFindPharm.Size = new System.Drawing.Size(136, 23);
			this.m_txtFindPharm.TabIndex = 1;
			this.m_txtFindPharm.Text = "";
			this.m_txtFindPharm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtFindPharm_KeyDown);
			this.m_txtFindPharm.Enter += new System.EventHandler(this.m_txtFindPharm_Enter);
			// 
			// m_cmdOK
			// 
			this.m_cmdOK.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_cmdOK.DefaultScheme = true;
			this.m_cmdOK.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_cmdOK.Hint = "";
			this.m_cmdOK.Location = new System.Drawing.Point(656, 92);
			this.m_cmdOK.Name = "m_cmdOK";
			this.m_cmdOK.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_cmdOK.Size = new System.Drawing.Size(80, 32);
			this.m_cmdOK.TabIndex = 12;
			this.m_cmdOK.Text = "确定(&A)";
			this.m_cmdOK.Click += new System.EventHandler(this.m_cmdOK_Click);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 96);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(48, 24);
			this.label11.TabIndex = 17;
			this.label11.Text = "失效期";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// m_txtLotNo
			// 
			this.m_txtLotNo.Location = new System.Drawing.Point(776, 57);
			this.m_txtLotNo.MaxLength = 18;
			this.m_txtLotNo.Name = "m_txtLotNo";
			this.m_txtLotNo.Size = new System.Drawing.Size(224, 23);
			this.m_txtLotNo.TabIndex = 7;
			this.m_txtLotNo.Text = "";
			this.m_txtLotNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtLotNo_KeyDown);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(712, 56);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(64, 24);
			this.label10.TabIndex = 15;
			this.label10.Text = "批    号";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 24);
			this.label2.TabIndex = 14;
			this.label2.Text = "数  量";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(712, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 24);
			this.label6.TabIndex = 11;
			this.label6.Text = "药品规格:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("宋体", 10.5F);
			this.label5.Location = new System.Drawing.Point(392, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 24);
			this.label5.TabIndex = 9;
			this.label5.Text = "药品名称:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(176, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 24);
			this.label4.TabIndex = 7;
			this.label4.Text = "查找药品";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(176, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 24);
			this.label3.TabIndex = 14;
			this.label3.Text = "买 入 价";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(392, 56);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(64, 24);
			this.label8.TabIndex = 14;
			this.label8.Text = "零 售 价";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(560, 56);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(64, 24);
			this.label9.TabIndex = 14;
			this.label9.Text = "批 发 价";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label9.Visible = false;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(176, 96);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(64, 24);
			this.label12.TabIndex = 14;
			this.label12.Text = "产    地";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// m_txtPharmSpec
			// 
			this.m_txtPharmSpec.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_txtPharmSpec.Location = new System.Drawing.Point(784, 20);
			this.m_txtPharmSpec.Name = "m_txtPharmSpec";
			this.m_txtPharmSpec.ReadOnly = true;
			this.m_txtPharmSpec.Size = new System.Drawing.Size(216, 16);
			this.m_txtPharmSpec.TabIndex = 300;
			this.m_txtPharmSpec.Text = "";
			// 
			// m_txtPharmName
			// 
			this.m_txtPharmName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_txtPharmName.Location = new System.Drawing.Point(464, 20);
			this.m_txtPharmName.Name = "m_txtPharmName";
			this.m_txtPharmName.ReadOnly = true;
			this.m_txtPharmName.Size = new System.Drawing.Size(240, 16);
			this.m_txtPharmName.TabIndex = 200;
			this.m_txtPharmName.Text = "";
			// 
			// m_BtnClear
			// 
			this.m_BtnClear.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_BtnClear.DefaultScheme = true;
			this.m_BtnClear.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_BtnClear.Hint = "";
			this.m_BtnClear.Location = new System.Drawing.Point(788, 92);
			this.m_BtnClear.Name = "m_BtnClear";
			this.m_BtnClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_BtnClear.Size = new System.Drawing.Size(80, 32);
			this.m_BtnClear.TabIndex = 22;
			this.m_BtnClear.Text = "清空(&C)";
			this.m_BtnClear.Click += new System.EventHandler(this.m_BtnClear_Click);
			// 
			// m_BtnDel
			// 
			this.m_BtnDel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_BtnDel.DefaultScheme = true;
			this.m_BtnDel.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_BtnDel.Hint = "";
			this.m_BtnDel.Location = new System.Drawing.Point(920, 92);
			this.m_BtnDel.Name = "m_BtnDel";
			this.m_BtnDel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_BtnDel.Size = new System.Drawing.Size(80, 32);
			this.m_BtnDel.TabIndex = 22;
			this.m_BtnDel.Text = "删除(&K)";
			this.m_BtnDel.Click += new System.EventHandler(this.m_BtnDel_Click);
			// 
			// m_cmbStorage
			// 
			this.m_cmbStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cmbStorage.Location = new System.Drawing.Point(56, 17);
			this.m_cmbStorage.Name = "m_cmbStorage";
			this.m_cmbStorage.Size = new System.Drawing.Size(120, 22);
			this.m_cmbStorage.TabIndex = 0;
			this.m_cmbStorage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cmbStorage_KeyDown);
			this.m_cmbStorage.SelectedIndexChanged += new System.EventHandler(this.m_cmbStorage_SelectedIndexChanged_1);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.panel1);
			this.groupBox1.Controls.Add(this.groupBox4);
			this.groupBox1.Controls.Add(this.m_lsvDetail);
			this.groupBox1.Location = new System.Drawing.Point(0, 128);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1008, 544);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.textBox3);
			this.panel1.Controls.Add(this.label19);
			this.panel1.Controls.Add(this.comboBox1);
			this.panel1.Controls.Add(this.buttonXP2);
			this.panel1.Controls.Add(this.buttonXP3);
			this.panel1.Location = new System.Drawing.Point(8, 496);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(664, 40);
			this.panel1.TabIndex = 412;
			// 
			// textBox3
			// 
			this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textBox3.Location = new System.Drawing.Point(240, 6);
			this.textBox3.Name = "textBox3";
			this.textBox3.TabIndex = 6;
			this.textBox3.Text = "";
			this.textBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox3_KeyDown);
			// 
			// label19
			// 
			this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label19.Location = new System.Drawing.Point(32, 6);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(72, 24);
			this.label19.TabIndex = 411;
			this.label19.Text = "查找方式:";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBox1
			// 
			this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Items.AddRange(new object[] {
														   "药品代码",
														   "药品名称",
														   "拼音码",
														   "五笔码"});
			this.comboBox1.Location = new System.Drawing.Point(104, 6);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 22);
			this.comboBox1.TabIndex = 5;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// buttonXP2
			// 
			this.buttonXP2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXP2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.buttonXP2.DefaultScheme = true;
			this.buttonXP2.DialogResult = System.Windows.Forms.DialogResult.None;
			this.buttonXP2.Hint = "";
			this.buttonXP2.Location = new System.Drawing.Point(528, 1);
			this.buttonXP2.Name = "buttonXP2";
			this.buttonXP2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.buttonXP2.Size = new System.Drawing.Size(96, 32);
			this.buttonXP2.TabIndex = 28;
			this.buttonXP2.Text = "返回(&R)";
			this.buttonXP2.Click += new System.EventHandler(this.buttonXP2_Click);
			// 
			// buttonXP3
			// 
			this.buttonXP3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXP3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.buttonXP3.DefaultScheme = true;
			this.buttonXP3.DialogResult = System.Windows.Forms.DialogResult.None;
			this.buttonXP3.Hint = "";
			this.buttonXP3.Location = new System.Drawing.Point(384, 1);
			this.buttonXP3.Name = "buttonXP3";
			this.buttonXP3.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.buttonXP3.Size = new System.Drawing.Size(96, 32);
			this.buttonXP3.TabIndex = 27;
			this.buttonXP3.Text = "查找(&F)";
			this.buttonXP3.Click += new System.EventHandler(this.buttonXP3_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox4.Controls.Add(this.label16);
			this.groupBox4.Controls.Add(this.textBox1);
			this.groupBox4.Controls.Add(this.label15);
			this.groupBox4.Controls.Add(this.label17);
			this.groupBox4.Controls.Add(this.label18);
			this.groupBox4.Controls.Add(this.textBox2);
			this.groupBox4.Location = new System.Drawing.Point(688, 488);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(312, 48);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			// 
			// label16
			// 
			this.label16.BackColor = System.Drawing.Color.YellowGreen;
			this.label16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label16.Location = new System.Drawing.Point(72, 32);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(48, 1);
			this.label16.TabIndex = 406;
			this.label16.Text = "label16";
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textBox1.ForeColor = System.Drawing.Color.Black;
			this.textBox1.Location = new System.Drawing.Point(72, 13);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(56, 23);
			this.textBox1.TabIndex = 410;
			this.textBox1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.textBox1.Click += new System.EventHandler(this.label19_Click);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 16);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(72, 24);
			this.label15.TabIndex = 15;
			this.label15.Text = "药品数量:";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label17
			// 
			this.label17.BackColor = System.Drawing.Color.YellowGreen;
			this.label17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label17.Location = new System.Drawing.Point(224, 32);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(80, 1);
			this.label17.TabIndex = 408;
			this.label17.Text = "label17";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(152, 16);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(72, 24);
			this.label18.TabIndex = 409;
			this.label18.Text = "金额合计:";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBox2
			// 
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textBox2.ForeColor = System.Drawing.Color.Red;
			this.textBox2.Location = new System.Drawing.Point(224, 16);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(80, 16);
			this.textBox2.TabIndex = 407;
			this.textBox2.Text = "";
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
																						  this.clhLotNo,
																						  this.clhUsefulLife,
																						  this.clhQty,
																						  this.clhUnit,
																						  this.clhBuyPrice,
																						  this.clhUnitPrice,
																						  this.columnHeader1,
																						  this.clhTradePrice,
																						  this.clhProductort});
			this.m_lsvDetail.Font = new System.Drawing.Font("宋体", 10.5F);
			this.m_lsvDetail.FullRowSelect = true;
			this.m_lsvDetail.GridLines = true;
			this.m_lsvDetail.Location = new System.Drawing.Point(7, 12);
			this.m_lsvDetail.MultiSelect = false;
			this.m_lsvDetail.Name = "m_lsvDetail";
			this.m_lsvDetail.Size = new System.Drawing.Size(993, 476);
			this.m_lsvDetail.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.m_lsvDetail.TabIndex = 3;
			this.m_lsvDetail.View = System.Windows.Forms.View.Details;
			this.m_lsvDetail.DoubleClick += new System.EventHandler(this.m_lsvDetail_DoubleClick);
			this.m_lsvDetail.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_lsvDetail_ColumnClick);
			this.m_lsvDetail.SelectedIndexChanged += new System.EventHandler(this.m_lsvDetail_SelectedIndexChanged);
			this.m_lsvDetail.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.m_lsvDetail_ItemCheck);
			// 
			// clhMedicineID
			// 
			this.clhMedicineID.Text = "药品代码";
			this.clhMedicineID.Width = 100;
			// 
			// clhMedicineName
			// 
			this.clhMedicineName.Text = "药品名称";
			this.clhMedicineName.Width = 189;
			// 
			// clhMedicineSpec
			// 
			this.clhMedicineSpec.Text = "规格";
			this.clhMedicineSpec.Width = 159;
			// 
			// clhLotNo
			// 
			this.clhLotNo.Text = "批号";
			this.clhLotNo.Width = 73;
			// 
			// clhUsefulLife
			// 
			this.clhUsefulLife.Text = "失效期";
			this.clhUsefulLife.Width = 86;
			// 
			// clhQty
			// 
			this.clhQty.Text = "数量";
			this.clhQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.clhQty.Width = 48;
			// 
			// clhUnit
			// 
			this.clhUnit.Text = "单位";
			this.clhUnit.Width = 42;
			// 
			// clhBuyPrice
			// 
			this.clhBuyPrice.Text = "买入价";
			this.clhBuyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.clhBuyPrice.Width = 80;
			// 
			// clhUnitPrice
			// 
			this.clhUnitPrice.Text = "零售价";
			this.clhUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.clhUnitPrice.Width = 80;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "零售总价";
			this.columnHeader1.Width = 80;
			// 
			// clhTradePrice
			// 
			this.clhTradePrice.Text = "批发价";
			this.clhTradePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.clhTradePrice.Width = 0;
			// 
			// clhProductort
			// 
			this.clhProductort.Text = "产地";
			this.clhProductort.Width = 150;
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.buttonXP1);
			this.groupBox3.Controls.Add(this.m_chkAudit);
			this.groupBox3.Controls.Add(this.m_BtnAudit);
			this.groupBox3.Controls.Add(this.m_BtnSave);
			this.groupBox3.Controls.Add(this.m_BtnExit);
			this.groupBox3.Location = new System.Drawing.Point(0, 680);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(1008, 56);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			// 
			// buttonXP1
			// 
			this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.buttonXP1.DefaultScheme = true;
			this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
			this.buttonXP1.Hint = "";
			this.buttonXP1.Location = new System.Drawing.Point(644, 16);
			this.buttonXP1.Name = "buttonXP1";
			this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.buttonXP1.Size = new System.Drawing.Size(96, 32);
			this.buttonXP1.TabIndex = 26;
			this.buttonXP1.Text = "打印(&P)";
			this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
			// 
			// m_chkAudit
			// 
			this.m_chkAudit.Location = new System.Drawing.Point(72, 24);
			this.m_chkAudit.Name = "m_chkAudit";
			this.m_chkAudit.TabIndex = 25;
			this.m_chkAudit.Text = "审核状态";
			this.m_chkAudit.CheckedChanged += new System.EventHandler(this.m_chkAudit_CheckedChanged);
			// 
			// m_BtnAudit
			// 
			this.m_BtnAudit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_BtnAudit.DefaultScheme = true;
			this.m_BtnAudit.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_BtnAudit.Hint = "";
			this.m_BtnAudit.Location = new System.Drawing.Point(482, 16);
			this.m_BtnAudit.Name = "m_BtnAudit";
			this.m_BtnAudit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_BtnAudit.Size = new System.Drawing.Size(96, 32);
			this.m_BtnAudit.TabIndex = 24;
			this.m_BtnAudit.Text = "审核(&D)";
			this.m_BtnAudit.Click += new System.EventHandler(this.m_BtnAudit_Click);
			// 
			// m_BtnSave
			// 
			this.m_BtnSave.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_BtnSave.DefaultScheme = true;
			this.m_BtnSave.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_BtnSave.Hint = "";
			this.m_BtnSave.Location = new System.Drawing.Point(320, 16);
			this.m_BtnSave.Name = "m_BtnSave";
			this.m_BtnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_BtnSave.Size = new System.Drawing.Size(96, 32);
			this.m_BtnSave.TabIndex = 23;
			this.m_BtnSave.Text = "保存(&S)";
			this.m_BtnSave.Click += new System.EventHandler(this.m_BtnSave_Click);
			// 
			// m_BtnExit
			// 
			this.m_BtnExit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_BtnExit.DefaultScheme = true;
			this.m_BtnExit.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_BtnExit.Hint = "";
			this.m_BtnExit.Location = new System.Drawing.Point(806, 16);
			this.m_BtnExit.Name = "m_BtnExit";
			this.m_BtnExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_BtnExit.Size = new System.Drawing.Size(96, 32);
			this.m_BtnExit.TabIndex = 24;
			this.m_BtnExit.Text = "退出(&E)";
			this.m_BtnExit.Click += new System.EventHandler(this.m_BtnExit_Click);
			// 
			// m_DlgResult
			// 
			this.m_DlgResult.blIsMedStorage = true;
			this.m_DlgResult.blISOutStorage = false;
			this.m_DlgResult.blRepertory = true;
			this.m_DlgResult.FindMedmode = 0;
			this.m_DlgResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_DlgResult.intIsReData = 0;
			this.m_DlgResult.isApplMebMod = null;
			this.m_DlgResult.isApplModel = false;
			this.m_DlgResult.isShowFindType = true;
			this.m_DlgResult.IsShowZero = false;
			this.m_DlgResult.Location = new System.Drawing.Point(240, 728);
			this.m_DlgResult.Name = "m_DlgResult";
			this.m_DlgResult.Size = new System.Drawing.Size(576, 336);
			this.m_DlgResult.strMedstorage = null;
			this.m_DlgResult.strSTORAGEID = "-1";
			this.m_DlgResult.TabIndex = 2;
			this.m_DlgResult.Visible = false;
			this.m_DlgResult.m_evtReturnVal += new com.digitalwave.controls.dlgReturnVal(this.m_DlgResult_m_evtReturnVal);
			this.m_DlgResult.Leave += new System.EventHandler(this.m_DlgResult_Leave);
			// 
			// printPreviewDialog1
			// 
			this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
			this.printPreviewDialog1.Enabled = true;
			this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
			this.printPreviewDialog1.Location = new System.Drawing.Point(88, 32);
			this.printPreviewDialog1.MinimumSize = new System.Drawing.Size(375, 250);
			this.printPreviewDialog1.Name = "printPreviewDialog1";
			this.printPreviewDialog1.TransparencyKey = System.Drawing.Color.Empty;
			this.printPreviewDialog1.Visible = false;
			// 
			// frmStorageInit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(1012, 737);
			this.Controls.Add(this.m_DlgResult);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.Font = new System.Drawing.Font("宋体", 10.5F);
			this.KeyPreview = true;
			this.Name = "frmStorageInit";
			this.Text = "库存初始化";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmStorageInit_KeyDown);
			this.Load += new System.EventHandler(this.frmStorageInit_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmStorageInit_Load(object sender, System.EventArgs e)
		{		
			((clsControlStorageInit)this.objController).InitFrmLoad();
			this.m_mthSetEnter2Tab(new System.Windows.Forms.Control[] {});
		}

		private void m_cmbStorage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(m_cmbStorage.SelectItemText == "")
			{
				return;
			}
			if(((clsControlStorageInit)this.objController).HasChange())
			{
				if(MessageBox.Show("你的操作未保存，是否放弃切换药库操作？是/否","",MessageBoxButtons.YesNo)== DialogResult.Yes)
				{
					return;
				}
			}
			string strWere="";
			((clsControlStorageInit)this.objController).m_mthInitDetail(strWere);
			this.m_DlgResult.strSTORAGEID = m_cmbStorage.SelectItemValue.ToString();
			this.m_DlgResult.intIsReData=1;
			m_BtnAudit.Enabled=true;
		}
		clsPublicParm  publiClass=new clsPublicParm();
		private void m_cmdOK_Click(object sender, System.EventArgs e)
		{
			m_BtnSave.Enabled=true;
			if(this.m_txtPharmName.Text.Trim() == "")
			{
				publiClass.m_mthShowWarning(this.m_txtFindPharm,"请选择药品");
				this.m_txtFindPharm.Focus();
				return ;
			}
			double dQty= 0;
			double dBuyPrice = 0;
			double dSPrice = 0;
			double dTrade = 0;
			if(m_txtQty.Text.Trim()=="")
			{
				publiClass.m_mthShowWarning(this.m_txtQty,"请输入正确的数量!");
				this.m_txtQty.Focus();
				return;
			}

			if(this.m_txtBuyPrice.Text.Trim()=="")
			{
				publiClass.m_mthShowWarning(this.m_txtBuyPrice,"数量或单价不能为零!");
				this.m_txtBuyPrice.Focus();
				return ;
			}
			if(m_txtLotNo.Text == "")
			{
				publiClass.m_mthShowWarning(this.m_txtLotNo,"批次不能为空!");
				this.m_txtLotNo.Focus();
				return;
			}
			if(((clsControlStorageInit)this.objController).m_lngAddPharm()<0)
			{
				publiClass.m_mthShowWarning(this.groupBox2,"增加失败!");
			}
			try
			{
				dQty = double.Parse(m_txtQty.Text.Trim());
				dBuyPrice = double.Parse(m_txtBuyPrice.Text.Trim());
				dSPrice = double.Parse(m_txtSalePrice.Text.Trim());
			}
			catch
			{
				
			}
			this.m_BtnClear.Text = "清空(&C)";
			this.m_cmdOK.Text = "确定(&A)";
			this.m_txtFindPharm.Enabled=true;
			this.m_txtFindPharm.Focus();
			m_mthClear(this);
			((clsControlStorageInit)this.objController).m_mthGetTotail();
		}

		private void m_BtnClear_Click(object sender, System.EventArgs e)
		{
			m_mthClear(this);
			this.m_BtnClear.Text = "清空(&C)";
			this.m_cmdOK.Text = "确定(&A)";
			this.m_txtFindPharm.Enabled=true;
			this.m_txtVendor.Text="";
			((clsControlStorageInit)this.objController).m_strOldPharmID = null;
			((clsControlStorageInit)this.objController).m_strOldPharmNo = null;
			this.m_txtFindPharm.Focus();
			m_cmdOK.Enabled=true;

		}
		private void m_mthClear(System.Windows.Forms.Control obj)
		{
			m_txtFindPharm.Clear();
			m_txtPharmName.Clear();
			m_txtPharmSpec.Clear();
			m_txtQty.Clear();

			m_txtBuyPrice.Clear();
			m_txtSalePrice.Clear();
			m_txtLotNo.Clear();
			m_txtVendor.Text="";
		}

		private void m_BtnDel_Click(object sender, System.EventArgs e)
		{
			if(this.m_lsvDetail.SelectedItems.Count < 1)
			{
				return;
			}
			((clsControlStorageInit)this.objController).m_lngDelPharm();
			m_mthClear(this);
			m_txtFindPharm.Focus();
			m_txtFindPharm.Enabled=true;
			((clsControlStorageInit)this.objController).m_mthGetTotail();
			m_cmdOK.Text="确定(&A)";
		}

		private void m_lsvDetail_DoubleClick(object sender, System.EventArgs e)
		{
			
			if(this.m_lsvDetail.SelectedItems.Count < 1)
			{
				return;
			}
			m_BtnSave.Enabled=false;
			if(((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["PSTATUS_INT"].ToString().Trim() == "1")
			{
				this.m_lsvDetail.SelectedItems[0].Checked = true;
				m_cmdOK.Enabled=false;
				m_mthClear(this);
				return ;
			}
			else
			{
				this.m_lsvDetail.SelectedItems[0].Checked = false;
				m_cmdOK.Enabled=true;
			}
			this.m_BtnClear.Text = "取消(&C)";
			this.m_cmdOK.Text = "修改(&M)";
			this.m_txtFindPharm.Enabled=false;
			((clsControlStorageInit)this.objController).m_strOldPharmID = ((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["MEDICINEID_CHR"].ToString().Trim();
			((clsControlStorageInit)this.objController).m_strOldPharmNo = ((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["LOTNO_CHR"].ToString().Trim();

			
			this.m_txtPharmName.Tag = ((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["MEDICINEID_CHR"].ToString().Trim();
			this.m_txtPharmName.Text = ((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["MEDICINENAME_VCHR"].ToString().Trim();
			this.m_txtFindPharm.Tag=((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["ASSISTCODE_CHR"].ToString().Trim();
			this.m_txtPharmSpec.Text = ((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["MEDSPEC_VCHR"].ToString().Trim();
			this.m_txtUnit.Text = ((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["UNITID_CHR"].ToString().Trim();
			this.m_txtQty.Text = ((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["QTY_DEC"].ToString().Trim();
			this.m_txtBuyPrice.Text = ((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["BUYPRICE_MNY"].ToString().Trim();
			this.m_txtSalePrice.Text = ((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["UNITPRICE_MNY"].ToString().Trim();
			this.m_txtTradePrice.Text = ((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["WHOLESALEUNITPRICE_MNY"].ToString().Trim();

			this.m_txtLotNo.Text = ((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["LOTNO_CHR"].ToString().Trim();
			this.m_txtVendor.Text = ((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["PRODUCTORID_CHR"].ToString().Trim();
			this.m_txtVendor.Tag = ((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["PRODUCTORID_CHR"].ToString().Trim();

			this.m_dtpUseLife.Text = DateTime.Parse(((DataRow)this.m_lsvDetail.SelectedItems[0].Tag)["USEFULLIFE_DAT"].ToString().Trim()).ToString("yyyy年MM月dd日");
		}

		private void m_BtnSave_Click(object sender, System.EventArgs e)
		{	
            //if(m_lsvDetail.Items.Count==0)
            //{
            //    publiClass.m_mthShowWarning(m_lsvDetail,"当前没有可保存的数据！");
            //    return;
            //}
			if(((clsControlStorageInit)this.objController).m_lngSaveInitDetail() > 0)
			{
				publiClass.m_mthShowWarning(m_lsvDetail,"保存成功！");
				this.m_BtnAudit.Enabled = true;
				m_BtnAudit.Enabled=true;
			}
			else
			{
				publiClass.m_mthShowWarning(m_lsvDetail,"保存失败！");
			}
		}

		private void m_BtnExit_Click(object sender, System.EventArgs e)
		{
			if(((clsControlStorageInit)this.objController).HasChange() == true)
			{
				if(MessageBox.Show("你还有没保存的信息,确定退出?","系统提示",MessageBoxButtons.OKCancel) ==DialogResult.OK)
				{
					this.Close();
				}
			}
			else
			{
				this.Close();
			}
		}
		private void m_BtnAudit_Click(object sender, System.EventArgs e)
		{
			
			if(m_lsvDetail.Items.Count==0)
			{
				publiClass.m_mthShowWarning(m_lsvDetail,"当前没有可审核的数据！");
				return;
			}
			if(MessageBox.Show("确定审核更新库存吗？","",MessageBoxButtons.OKCancel) == DialogResult.Cancel)
			{
				return;
			}
			if(((clsControlStorageInit)this.objController).m_lngAuditInitDetail() > 0)
			{
				publiClass.m_mthShowWarning(m_lsvDetail,"审核成功！");
				for(int i1 = 0; i1 < this.m_lsvDetail.Items.Count; i1 ++)
				{
					this.m_lsvDetail.Items[i1].Checked = true;
					this.m_lsvDetail.Items[i1].BackColor = System.Drawing.Color.LightYellow;
				}
				this.m_BtnAudit.Enabled = false;
				this.m_lsvDetail.Items.Clear();
			}
			else
			{
				publiClass.m_mthShowWarning(m_lsvDetail,"审核失败！");
			}
		}

		private void m_DlgResult_m_evtReturnVal(object sender, com.digitalwave.controls.clsEvtReturnVal e)
		{
			this.m_mthClear(this);
			try
			{
				this.m_txtFindPharm.Tag = e.ReturnVo.strASSISTCODE_CHR;
				this.m_txtPharmName.Tag = e.ReturnVo.strMEDICINEID_CHR;
				this.m_txtPharmName.Text = e.ReturnVo.strMEDICINENAME_VCHR;
				this.m_txtPharmSpec.Tag = e.ReturnVo.intPACKQTY_DEC;
				this.m_txtSalePrice.Text=e.ReturnVo.dlUNITPRICE_MNY.ToString();
				this.m_txtVendor.Text=e.objVO.strPRODUCTORID_CHR;
				this.m_txtPharmSpec.Text = e.ReturnVo.strMEDSPEC_VCHR;
				if(strStorageFlag == "0")//仓库
				{
					this.m_txtUnit.Text = e.ReturnVo.strOPUNIT_CHR;
				}
				else
				{
					this.m_txtUnit.Text = e.ReturnVo.strOPUNIT_CHR;
				}
			}
			catch
			{
			}
			this.m_DlgResult.Visible = false;
			this.m_txtQty.Focus();
		}

		private void m_txtFindPharm_Enter(object sender, System.EventArgs e)
		{
		//	this.m_DlgResult.Visible = true;
		//	this.m_DlgResult.Focus();
			this.m_DlgResult.Width = 574;
			this.m_DlgResult.Height =320;
			System.Windows.Forms.Control obj = (System.Windows.Forms.Control)sender;
			int intLeft = obj.Left;
			int intTop = obj.Top+obj.Height;
			while(obj.Parent !=null)
			{
				obj = obj.Parent;
				intLeft += obj.Left;
				intTop +=obj.Top;
			}
			this.m_DlgResult.Left = intLeft;
			this.m_DlgResult.Top = intTop;
		}

		private void m_txtVendor_TextChanged(object sender, System.EventArgs e)
		{
		}

		private void m_txtVendor_Enter(object sender, System.EventArgs e)
		{
			m_txtVendor.Text ="";
			m_txtVendor.Tag = null;
		}

		private void m_txtVendor_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Up || e.KeyCode== Keys.Down)
			{
				return;
			}
		}

		private void m_DlgResult_Leave(object sender, System.EventArgs e)
		{
			m_DlgResult.Visible = false;
		}
		public void ShowForm(string p_strStorageFlag)
		{
			strStorageFlag = p_strStorageFlag;
			if( strStorageFlag == "0")
			{
				this.m_DlgResult.blISOutStorage = false;

			}
			else
			{
				this.m_DlgResult.blISOutStorage = false;
               
			}
			this.Show();
		}

		private void m_txtQty_Leave(object sender, System.EventArgs e)
		{
//			TextBox obj = (TextBox)sender;
//			if(obj.Text == "")
//			{
//				return;
//			}
//			try
//			{
//				double.Parse(obj.Text);
//			}
//			catch
//			{
//				MessageBox.Show("请输入正确的数字");
//				obj.Focus();
//				obj.SelectAll();
//			}
//			if(obj.Name == "m_txtBuyPrice")
//			{
//				//根据加成率来计算
//
//					try
//					{
//						double dtemp = 0.15;
//						double dbuyprice = double.Parse(obj.Text.Trim());
//						dtemp = dbuyprice * (1+dtemp);
//						this.m_txtSalePrice.Text = dtemp.ToString();
//						this.m_txtTradePrice.Text = dtemp.ToString();
//					}
//					catch
//					{
//					}
//			}
		}

		private void frmStorageInit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				m_DlgResult.Visible = false;
				this.m_txtFindPharm.Focus();
			}
		}

		private void m_lsvDetail_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			if(e.CurrentValue == CheckState.Checked)
			{
				e.NewValue = e.CurrentValue;
			}
		}

		private void m_txtFindPharm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				Point p=this.m_txtFindPharm.Parent.PointToScreen(this.m_txtFindPharm.Location);
				p=this.m_txtFindPharm.FindForm().PointToClient(p);
				p.Y+=this.m_txtFindPharm.Height;
				m_DlgResult.Location=p;
				this.m_DlgResult.Visible =true;
				this.m_DlgResult.Focus();
				this.m_DlgResult.strSTORAGEID = this.m_cmbStorage.SelectItemValue.ToString();
				this.m_DlgResult.FindMedmode = 0;
                this.m_DlgResult.m_txtFindMed.Text = m_txtFindPharm.Text;
			}
		}

		private void m_txtQty_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				this.m_txtBuyPrice.Focus();
			}
		}

		private void m_chkAudit_CheckedChanged(object sender, System.EventArgs e)
		{
			((clsControlStorageInit)this.objController).InitDetail();
		}

		private void m_cmbStorage_SelectedIndexChanged_1(object sender, System.EventArgs e)
		{
			m_cmbStorage_SelectedIndexChanged(sender,e);
			((clsControlStorageInit)this.objController).m_mthGetTotail();
		}

		private void m_cmbStorage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				m_txtFindPharm.Focus();
		}

		private void m_txtLotNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_dtpUseLife_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtVendor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtBuyPrice_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtSalePrice_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtTradePrice_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_dtpUseLife_KeyDown_1(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageInit)this.objController).m_mthGetPrintData();
		}

		private void m_lsvDetail_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			bool IsAsc=false;//是否为升序
			if(m_lsvDetail.Sorting==SortOrder.Ascending)
				m_lsvDetail.Sorting=SortOrder.Descending;
			else
			{
				m_lsvDetail.Sorting=SortOrder.Ascending;
				IsAsc=true;
			}
			m_lsvDetail.ListViewItemSorter=new ListViewItemComparer(e.Column,IsAsc,m_lsvDetail);
			m_lsvDetail.Sort();
		}

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void label19_Click(object sender, System.EventArgs e)
		{
		
		}

		private void m_lsvDetail_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void buttonXP3_Click(object sender, System.EventArgs e)
		{
			if(m_cmbStorage.SelectItemText == "")
			{
				return;
			}
			if(((clsControlStorageInit)this.objController).HasChange())
			{
				if(MessageBox.Show("你的操作未保存，是否放弃切换药库操作？是/否","",MessageBoxButtons.YesNo)== DialogResult.Yes)
				{
					return;
				}
			}
			string strWere="";
			switch(comboBox1.SelectedIndex)
			{
				case 0:
					strWere=" and b.ASSISTCODE_CHR like '"+textBox3.Text+"% '";
					break;
				case 1:
					strWere=" and b.MEDICINENAME_VCHR like '"+textBox3.Text+"%'";
					break;
				case 2:
					strWere=" and b.PYCODE_CHR like '"+textBox3.Text+"% '";
					break;
				case 3:
					strWere=" and b.WBCODE_CHR like '"+textBox3.Text+"% '";
					break;
			}
			((clsControlStorageInit)this.objController).m_mthInitDetail(strWere);
			((clsControlStorageInit)this.objController).m_mthGetTotail();
		}

		private void buttonXP2_Click(object sender, System.EventArgs e)
		{
			if(m_cmbStorage.SelectItemText == "")
			{
				return;
			}
			if(((clsControlStorageInit)this.objController).HasChange())
			{
				if(MessageBox.Show("你的操作未保存，是否放弃切换药库操作？是/否","",MessageBoxButtons.YesNo)== DialogResult.Yes)
				{
					return;
				}
			}
			string strWere="";
			((clsControlStorageInit)this.objController).m_mthInitDetail(strWere);
			((clsControlStorageInit)this.objController).m_mthGetTotail();
		}

		private void textBox3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				buttonXP3.Focus();
		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			textBox3.Focus();
		}
		
		
	}
}
