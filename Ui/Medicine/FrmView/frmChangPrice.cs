using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmChangPrice 的摘要说明。
	/// </summary>
	public class frmChangPrice : com.digitalwave.GUI_Base.frmMDI_Child_Base	//GUI_Base.dll
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		internal System.Windows.Forms.ListView lisvChangPriceDe;
		private System.Windows.Forms.ColumnHeader ROWNO_CHR;
		private System.Windows.Forms.ColumnHeader MEDICINEID_CHR;
		private System.Windows.Forms.ColumnHeader UNITID_CHR;
		private System.Windows.Forms.ColumnHeader QTY_DEC;
		private System.Windows.Forms.ColumnHeader CURPRICE_MNY;
		private System.Windows.Forms.ColumnHeader CHANGEPRICE_MNY;
		private System.Windows.Forms.ColumnHeader MEDICINENAME_CHR;
		internal PinkieControls.ButtonXP dntEmp;
		internal PinkieControls.ButtonXP btnesc;
		internal PinkieControls.ButtonXP btnSave;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel5;
        public TabControl tabChang;
		private System.Windows.Forms.TabPage tab1;
		internal System.Windows.Forms.ListView lsvPrice;
		private System.Windows.Forms.TabPage tab2;
		internal System.Windows.Forms.ListView livPriceOk;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader col6;
		internal System.Windows.Forms.TextBox txtMEDICINE;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		internal System.Windows.Forms.TextBox TXTCHANGEPRICE;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		internal System.Windows.Forms.DateTimePicker dateTime;
		private System.Windows.Forms.ColumnHeader col7;
		internal PinkieControls.ButtonXP btnClear;
		internal PinkieControls.ButtonXP btnAdd;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		internal System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.Label label8;
		internal System.Windows.Forms.ComboBox comPriod;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label label14;
		internal System.Windows.Forms.TextBox m_txtBillNo;
		private System.Windows.Forms.Label label24;
		internal System.Windows.Forms.ComboBox m_cmbType;
		internal PinkieControls.ButtonXP m_BtnPrint;
		private com.digitalwave.controls.ControlMedicineFind controlMedicineFind;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label25;
		internal System.Windows.Forms.Label m_txtPRICE;
		internal System.Windows.Forms.Label m_txtUNIT;
		internal System.Windows.Forms.Label txtoddsDe;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label29;
		internal System.Windows.Forms.Label txtAllAmount;
		internal System.Windows.Forms.Panel panel4;
		internal System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.GroupBox groupBox1;
		internal PinkieControls.ButtonXP buttonXP1;
		internal PinkieControls.ButtonXP buttonXP2;
		internal System.Windows.Forms.TextBox m_txtMemo;
		internal PinkieControls.ButtonXP buttonXP3;
		internal System.Windows.Forms.TextBox txtDocEnd;
        internal System.Windows.Forms.Label txtMEDSPEC;
        private IContainer components;

		public frmChangPrice()
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonXP3 = new PinkieControls.ButtonXP();
            this.buttonXP2 = new PinkieControls.ButtonXP();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.m_BtnPrint = new PinkieControls.ButtonXP();
            this.btnSave = new PinkieControls.ButtonXP();
            this.btnesc = new PinkieControls.ButtonXP();
            this.dntEmp = new PinkieControls.ButtonXP();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.txtMEDSPEC = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.TXTCHANGEPRICE = new System.Windows.Forms.TextBox();
            this.m_txtPRICE = new System.Windows.Forms.Label();
            this.txtMEDICINE = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.m_cmbType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtAllAmount = new System.Windows.Forms.Label();
            this.btnClear = new PinkieControls.ButtonXP();
            this.label9 = new System.Windows.Forms.Label();
            this.btnAdd = new PinkieControls.ButtonXP();
            this.txtoddsDe = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.m_txtUNIT = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtDocEnd = new System.Windows.Forms.TextBox();
            this.m_txtMemo = new System.Windows.Forms.TextBox();
            this.m_txtBillNo = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dateTime = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lisvChangPriceDe = new System.Windows.Forms.ListView();
            this.ROWNO_CHR = new System.Windows.Forms.ColumnHeader();
            this.MEDICINEID_CHR = new System.Windows.Forms.ColumnHeader();
            this.MEDICINENAME_CHR = new System.Windows.Forms.ColumnHeader();
            this.col6 = new System.Windows.Forms.ColumnHeader();
            this.UNITID_CHR = new System.Windows.Forms.ColumnHeader();
            this.QTY_DEC = new System.Windows.Forms.ColumnHeader();
            this.CURPRICE_MNY = new System.Windows.Forms.ColumnHeader();
            this.CHANGEPRICE_MNY = new System.Windows.Forms.ColumnHeader();
            this.col7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.panel5 = new System.Windows.Forms.Panel();
            this.comPriod = new System.Windows.Forms.ComboBox();
            this.tabChang = new System.Windows.Forms.TabControl();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.lsvPrice = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.tab2 = new System.Windows.Forms.TabPage();
            this.livPriceOk = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.controlMedicineFind = new com.digitalwave.controls.ControlMedicineFind();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabChang.SuspendLayout();
            this.tab1.SuspendLayout();
            this.tab2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.lisvChangPriceDe);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 640);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonXP3);
            this.groupBox1.Controls.Add(this.buttonXP2);
            this.groupBox1.Controls.Add(this.buttonXP1);
            this.groupBox1.Controls.Add(this.m_BtnPrint);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnesc);
            this.groupBox1.Controls.Add(this.dntEmp);
            this.groupBox1.Location = new System.Drawing.Point(336, 520);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(680, 56);
            this.groupBox1.TabIndex = 154;
            this.groupBox1.TabStop = false;
            // 
            // buttonXP3
            // 
            this.buttonXP3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXP3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP3.DefaultScheme = true;
            this.buttonXP3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP3.Hint = "";
            this.buttonXP3.Location = new System.Drawing.Point(102, 16);
            this.buttonXP3.Name = "buttonXP3";
            this.buttonXP3.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP3.Size = new System.Drawing.Size(98, 32);
            this.buttonXP3.TabIndex = 154;
            this.buttonXP3.TabStop = false;
            this.buttonXP3.Text = "合并单据(&U)";
            this.buttonXP3.Click += new System.EventHandler(this.buttonXP3_Click);
            // 
            // buttonXP2
            // 
            this.buttonXP2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP2.DefaultScheme = true;
            this.buttonXP2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP2.Enabled = false;
            this.buttonXP2.Hint = "";
            this.buttonXP2.Location = new System.Drawing.Point(394, 16);
            this.buttonXP2.Name = "buttonXP2";
            this.buttonXP2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP2.Size = new System.Drawing.Size(88, 32);
            this.buttonXP2.TabIndex = 153;
            this.buttonXP2.TabStop = false;
            this.buttonXP2.Text = "删除(&D)";
            this.buttonXP2.Click += new System.EventHandler(this.buttonXP2_Click);
            // 
            // buttonXP1
            // 
            this.buttonXP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(8, 16);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(88, 32);
            this.buttonXP1.TabIndex = 152;
            this.buttonXP1.TabStop = false;
            this.buttonXP1.Text = "新建(&N)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // m_BtnPrint
            // 
            this.m_BtnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_BtnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_BtnPrint.DefaultScheme = true;
            this.m_BtnPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_BtnPrint.Hint = "";
            this.m_BtnPrint.Location = new System.Drawing.Point(488, 16);
            this.m_BtnPrint.Name = "m_BtnPrint";
            this.m_BtnPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_BtnPrint.Size = new System.Drawing.Size(88, 32);
            this.m_BtnPrint.TabIndex = 151;
            this.m_BtnPrint.TabStop = false;
            this.m_BtnPrint.Text = "打印(&P)";
            this.m_BtnPrint.Click += new System.EventHandler(this.m_BtnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnSave.DefaultScheme = true;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.Hint = "";
            this.btnSave.Location = new System.Drawing.Point(206, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnSave.Size = new System.Drawing.Size(88, 32);
            this.btnSave.TabIndex = 17;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnesc
            // 
            this.btnesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnesc.DefaultScheme = true;
            this.btnesc.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnesc.Hint = "";
            this.btnesc.Location = new System.Drawing.Point(582, 16);
            this.btnesc.Name = "btnesc";
            this.btnesc.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnesc.Size = new System.Drawing.Size(88, 32);
            this.btnesc.TabIndex = 53;
            this.btnesc.TabStop = false;
            this.btnesc.Text = "退出(ESE)";
            this.btnesc.Click += new System.EventHandler(this.btnesc_Click);
            // 
            // dntEmp
            // 
            this.dntEmp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dntEmp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.dntEmp.DefaultScheme = true;
            this.dntEmp.DialogResult = System.Windows.Forms.DialogResult.None;
            this.dntEmp.Enabled = false;
            this.dntEmp.Hint = "";
            this.dntEmp.Location = new System.Drawing.Point(300, 16);
            this.dntEmp.Name = "dntEmp";
            this.dntEmp.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.dntEmp.Size = new System.Drawing.Size(88, 32);
            this.dntEmp.TabIndex = 54;
            this.dntEmp.TabStop = false;
            this.dntEmp.Text = "审核(&O)";
            this.dntEmp.Click += new System.EventHandler(this.dntEmp_Click);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.label25);
            this.panel6.Controls.Add(this.txtMEDSPEC);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.label24);
            this.panel6.Controls.Add(this.TXTCHANGEPRICE);
            this.panel6.Controls.Add(this.m_txtPRICE);
            this.panel6.Controls.Add(this.txtMEDICINE);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.label28);
            this.panel6.Controls.Add(this.m_cmbType);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.label26);
            this.panel6.Controls.Add(this.txtAllAmount);
            this.panel6.Controls.Add(this.btnClear);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.btnAdd);
            this.panel6.Controls.Add(this.txtoddsDe);
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.m_txtUNIT);
            this.panel6.Controls.Add(this.label29);
            this.panel6.Controls.Add(this.label27);
            this.panel6.Location = new System.Drawing.Point(336, 416);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(680, 100);
            this.panel6.TabIndex = 1;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label25.BackColor = System.Drawing.Color.SandyBrown;
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label25.Location = new System.Drawing.Point(376, 24);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(280, 1);
            this.label25.TabIndex = 152;
            this.label25.Text = "label25";
            // 
            // txtMEDSPEC
            // 
            this.txtMEDSPEC.Location = new System.Drawing.Point(376, 8);
            this.txtMEDSPEC.Name = "txtMEDSPEC";
            this.txtMEDSPEC.Size = new System.Drawing.Size(280, 23);
            this.txtMEDSPEC.TabIndex = 161;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 132;
            this.label3.Text = "原 价 格:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.Location = new System.Drawing.Point(8, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 123;
            this.label7.Text = "药品名称:";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(8, 72);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(70, 14);
            this.label24.TabIndex = 149;
            this.label24.Text = "原    因:";
            // 
            // TXTCHANGEPRICE
            // 
            this.TXTCHANGEPRICE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TXTCHANGEPRICE.BackColor = System.Drawing.SystemColors.HighlightText;
            //this.TXTCHANGEPRICE.EnableAutoValidation = false;
            //this.TXTCHANGEPRICE.EnableEnterKeyValidate = true;
            //this.TXTCHANGEPRICE.EnableEscapeKeyUndo = true;
            //this.TXTCHANGEPRICE.EnableLastValidValue = true;
            //this.TXTCHANGEPRICE.ErrorProvider = null;
            //this.TXTCHANGEPRICE.ErrorProviderMessage = "Invalid value";
            //this.TXTCHANGEPRICE.ForceFormatText = true;
            this.TXTCHANGEPRICE.Location = new System.Drawing.Point(80, 40);
            this.TXTCHANGEPRICE.MaxLength = 5;
            this.TXTCHANGEPRICE.Name = "TXTCHANGEPRICE";
            //this.TXTCHANGEPRICE.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
            this.TXTCHANGEPRICE.Size = new System.Drawing.Size(72, 23);
            this.TXTCHANGEPRICE.TabIndex = 135;
            this.TXTCHANGEPRICE.TabStop = false;
            this.TXTCHANGEPRICE.Text = "0.00";
            this.TXTCHANGEPRICE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TXTCHANGEPRICE.Enter += new System.EventHandler(this.TXTCHANGEPRICE_Enter);
            this.TXTCHANGEPRICE.Leave += new System.EventHandler(this.TXTCHANGEPRICE_Leave);
            this.TXTCHANGEPRICE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TXTCHANGEPRICE_KeyDown);
            // 
            // m_txtPRICE
            // 
            this.m_txtPRICE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_txtPRICE.Location = new System.Drawing.Point(232, 40);
            this.m_txtPRICE.Name = "m_txtPRICE";
            this.m_txtPRICE.Size = new System.Drawing.Size(64, 16);
            this.m_txtPRICE.TabIndex = 153;
            this.m_txtPRICE.Text = "0.00";
            // 
            // txtMEDICINE
            // 
            this.txtMEDICINE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMEDICINE.BackColor = System.Drawing.SystemColors.HighlightText;
            //this.txtMEDICINE.EnableAutoValidation = true;
            //this.txtMEDICINE.EnableEnterKeyValidate = true;
            //this.txtMEDICINE.EnableEscapeKeyUndo = true;
            //this.txtMEDICINE.EnableLastValidValue = true;
            //this.txtMEDICINE.ErrorProvider = null;
            //this.txtMEDICINE.ErrorProviderMessage = "Invalid value";
            //this.txtMEDICINE.ForceFormatText = true;
            this.txtMEDICINE.Location = new System.Drawing.Point(80, 8);
            this.txtMEDICINE.Name = "txtMEDICINE";
            this.txtMEDICINE.Size = new System.Drawing.Size(216, 23);
            this.txtMEDICINE.TabIndex = 130;
            this.txtMEDICINE.Enter += new System.EventHandler(this.txtMEDICINE_Enter);
            this.txtMEDICINE.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMEDICINE_KeyUp);
            this.txtMEDICINE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMEDICINE_KeyDown);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 14);
            this.label5.TabIndex = 134;
            this.label5.Text = "新 价 格:";
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label28.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label28.Location = new System.Drawing.Point(232, 56);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(64, 1);
            this.label28.TabIndex = 158;
            this.label28.Text = "label28";
            // 
            // m_cmbType
            // 
            this.m_cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cmbType.BackColor = System.Drawing.SystemColors.HighlightText;
            this.m_cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbType.Location = new System.Drawing.Point(80, 72);
            this.m_cmbType.Name = "m_cmbType";
            this.m_cmbType.Size = new System.Drawing.Size(352, 22);
            this.m_cmbType.TabIndex = 136;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.errorProvider1.SetIconAlignment(this.label6, System.Windows.Forms.ErrorIconAlignment.BottomRight);
            this.label6.Location = new System.Drawing.Point(304, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 137;
            this.label6.Text = "规    格:";
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label26.Location = new System.Drawing.Point(536, 56);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(96, 1);
            this.label26.TabIndex = 156;
            this.label26.Text = "label26";
            // 
            // txtAllAmount
            // 
            this.txtAllAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAllAmount.Location = new System.Drawing.Point(376, 40);
            this.txtAllAmount.Name = "txtAllAmount";
            this.txtAllAmount.Size = new System.Drawing.Size(56, 16);
            this.txtAllAmount.TabIndex = 160;
            this.txtAllAmount.Text = "0";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnClear.DefaultScheme = true;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.Hint = "";
            this.btnClear.Location = new System.Drawing.Point(573, 64);
            this.btnClear.Name = "btnClear";
            this.btnClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.TabIndex = 143;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "清空(&C)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.Location = new System.Drawing.Point(304, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 139;
            this.label9.Text = "库 存 量:";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnAdd.DefaultScheme = true;
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdd.Hint = "";
            this.btnAdd.Location = new System.Drawing.Point(460, 64);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnAdd.Size = new System.Drawing.Size(80, 30);
            this.btnAdd.TabIndex = 137;
            this.btnAdd.TabStop = false;
            this.btnAdd.Text = "增加(&A)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtoddsDe
            // 
            this.txtoddsDe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtoddsDe.Location = new System.Drawing.Point(536, 40);
            this.txtoddsDe.Name = "txtoddsDe";
            this.txtoddsDe.Size = new System.Drawing.Size(96, 16);
            this.txtoddsDe.TabIndex = 155;
            this.txtoddsDe.Text = "0.00";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(464, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 14);
            this.label10.TabIndex = 140;
            this.label10.Text = "差    额:";
            // 
            // m_txtUNIT
            // 
            this.m_txtUNIT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_txtUNIT.Location = new System.Drawing.Point(440, 40);
            this.m_txtUNIT.Name = "m_txtUNIT";
            this.m_txtUNIT.Size = new System.Drawing.Size(24, 16);
            this.m_txtUNIT.TabIndex = 154;
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label29.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label29.Location = new System.Drawing.Point(376, 56);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(56, 1);
            this.label29.TabIndex = 159;
            this.label29.Text = "label29";
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label27.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label27.Location = new System.Drawing.Point(648, 40);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(24, 23);
            this.label27.TabIndex = 157;
            this.label27.Text = "元";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label27.Click += new System.EventHandler(this.label27_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.txtDocEnd);
            this.panel4.Controls.Add(this.m_txtMemo);
            this.panel4.Controls.Add(this.m_txtBillNo);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.dateTime);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label23);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Location = new System.Drawing.Point(336, 344);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(680, 72);
            this.panel4.TabIndex = 0;
            // 
            // txtDocEnd
            // 
            this.txtDocEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDocEnd.BackColor = System.Drawing.SystemColors.HighlightText;
            //this.txtDocEnd.EnableAutoValidation = true;
            //this.txtDocEnd.EnableEnterKeyValidate = true;
            //this.txtDocEnd.EnableEscapeKeyUndo = true;
            //this.txtDocEnd.EnableLastValidValue = true;
            //this.txtDocEnd.ErrorProvider = null;
            //this.txtDocEnd.ErrorProviderMessage = "Invalid value";
            //this.txtDocEnd.ForceFormatText = true;
            this.txtDocEnd.Location = new System.Drawing.Point(384, 6);
            this.txtDocEnd.MaxLength = 4;
            this.txtDocEnd.Name = "txtDocEnd";
            this.txtDocEnd.Size = new System.Drawing.Size(48, 23);
            this.txtDocEnd.TabIndex = 1;
            this.txtDocEnd.Enter += new System.EventHandler(this.txtDocEnd_Enter);
            this.txtDocEnd.Leave += new System.EventHandler(this.txtDocEnd_Leave);
            // 
            // m_txtMemo
            // 
            this.m_txtMemo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.m_txtMemo.Location = new System.Drawing.Point(80, 40);
            this.m_txtMemo.Name = "m_txtMemo";
            this.m_txtMemo.Size = new System.Drawing.Size(592, 23);
            this.m_txtMemo.TabIndex = 153;
            // 
            // m_txtBillNo
            // 
            this.m_txtBillNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_txtBillNo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.m_txtBillNo.Enabled = false;
            this.m_txtBillNo.Location = new System.Drawing.Point(312, 6);
            this.m_txtBillNo.MaxLength = 10;
            this.m_txtBillNo.Name = "m_txtBillNo";
            this.m_txtBillNo.Size = new System.Drawing.Size(72, 23);
            this.m_txtBillNo.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(232, 8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 14);
            this.label14.TabIndex = 149;
            this.label14.Text = "单    据:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.label12.Location = new System.Drawing.Point(8, 8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 14);
            this.label12.TabIndex = 118;
            this.label12.Text = "日    期:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTime
            // 
            this.dateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTime.CalendarMonthBackground = System.Drawing.SystemColors.HighlightText;
            this.dateTime.Location = new System.Drawing.Point(80, 6);
            this.dateTime.MaxDate = new System.DateTime(2049, 12, 31, 0, 0, 0, 0);
            this.dateTime.MinDate = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            this.dateTime.Name = "dateTime";
            this.dateTime.Size = new System.Drawing.Size(120, 23);
            this.dateTime.TabIndex = 0;
            this.dateTime.Value = new System.DateTime(2004, 9, 19, 23, 27, 57, 453);
            this.dateTime.ValueChanged += new System.EventHandler(this.dateTime_ValueChanged);
            this.dateTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dateTime_KeyPress);
            this.dateTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTime_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 117;
            this.label4.Text = "备    注:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label22.BackColor = System.Drawing.Color.SandyBrown;
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label22.Location = new System.Drawing.Point(536, 28);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(96, 1);
            this.label22.TabIndex = 151;
            this.label22.Text = "label22";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(456, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 149;
            this.label2.Text = "总 差 额:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label23.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label23.Location = new System.Drawing.Point(648, 8);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(24, 23);
            this.label23.TabIndex = 152;
            this.label23.Text = "元";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(536, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 23);
            this.label8.TabIndex = 150;
            this.label8.Text = "0";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lisvChangPriceDe
            // 
            this.lisvChangPriceDe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lisvChangPriceDe.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ROWNO_CHR,
            this.MEDICINEID_CHR,
            this.MEDICINENAME_CHR,
            this.col6,
            this.UNITID_CHR,
            this.QTY_DEC,
            this.CURPRICE_MNY,
            this.CHANGEPRICE_MNY,
            this.col7,
            this.columnHeader2});
            this.lisvChangPriceDe.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lisvChangPriceDe.FullRowSelect = true;
            this.lisvChangPriceDe.GridLines = true;
            this.lisvChangPriceDe.HideSelection = false;
            this.lisvChangPriceDe.Location = new System.Drawing.Point(336, 0);
            this.lisvChangPriceDe.Name = "lisvChangPriceDe";
            this.lisvChangPriceDe.Size = new System.Drawing.Size(688, 336);
            this.lisvChangPriceDe.TabIndex = 1;
            this.lisvChangPriceDe.UseCompatibleStateImageBehavior = false;
            this.lisvChangPriceDe.View = System.Windows.Forms.View.Details;
            this.lisvChangPriceDe.SelectedIndexChanged += new System.EventHandler(this.lisvChangPriceDe_SelectedIndexChanged);
            this.lisvChangPriceDe.Click += new System.EventHandler(this.lisvChangPriceDe_Click_2);
            // 
            // ROWNO_CHR
            // 
            this.ROWNO_CHR.Text = "行号";
            this.ROWNO_CHR.Width = 0;
            // 
            // MEDICINEID_CHR
            // 
            this.MEDICINEID_CHR.Text = "药品代码";
            this.MEDICINEID_CHR.Width = 94;
            // 
            // MEDICINENAME_CHR
            // 
            this.MEDICINENAME_CHR.Text = "药品名称";
            this.MEDICINENAME_CHR.Width = 109;
            // 
            // col6
            // 
            this.col6.Text = "规格";
            this.col6.Width = 106;
            // 
            // UNITID_CHR
            // 
            this.UNITID_CHR.Text = "单位";
            this.UNITID_CHR.Width = 46;
            // 
            // QTY_DEC
            // 
            this.QTY_DEC.Text = "药品数量";
            this.QTY_DEC.Width = 68;
            // 
            // CURPRICE_MNY
            // 
            this.CURPRICE_MNY.Text = "原价格";
            this.CURPRICE_MNY.Width = 71;
            // 
            // CHANGEPRICE_MNY
            // 
            this.CHANGEPRICE_MNY.Text = "新价格";
            this.CHANGEPRICE_MNY.Width = 70;
            // 
            // col7
            // 
            this.col7.Text = "差额";
            this.col7.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "原因";
            this.columnHeader2.Width = 68;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.comPriod);
            this.panel5.Controls.Add(this.tabChang);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(336, 576);
            this.panel5.TabIndex = 120;
            // 
            // comPriod
            // 
            this.comPriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comPriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comPriod.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comPriod.ItemHeight = 14;
            this.comPriod.Location = new System.Drawing.Point(112, 552);
            this.comPriod.Name = "comPriod";
            this.comPriod.Size = new System.Drawing.Size(216, 22);
            this.comPriod.TabIndex = 148;
            this.comPriod.SelectedIndexChanged += new System.EventHandler(this.comPriod_SelectedIndexChanged);
            // 
            // tabChang
            // 
            this.tabChang.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabChang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tabChang.Controls.Add(this.tab1);
            this.tabChang.Controls.Add(this.tab2);
            this.tabChang.Location = new System.Drawing.Point(0, -6);
            this.tabChang.Name = "tabChang";
            this.tabChang.SelectedIndex = 0;
            this.tabChang.Size = new System.Drawing.Size(328, 580);
            this.tabChang.TabIndex = 20;
            this.tabChang.Enter += new System.EventHandler(this.tabChang_Enter);
            this.tabChang.SelectedIndexChanged += new System.EventHandler(this.tabChang_SelectedIndexChanged);
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.lsvPrice);
            this.tab1.Location = new System.Drawing.Point(4, 4);
            this.tab1.Name = "tab1";
            this.tab1.Size = new System.Drawing.Size(320, 553);
            this.tab1.TabIndex = 0;
            this.tab1.Text = "未审核";
            // 
            // lsvPrice
            // 
            this.lsvPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lsvPrice.CheckBoxes = true;
            this.lsvPrice.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader4});
            this.lsvPrice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lsvPrice.FullRowSelect = true;
            this.lsvPrice.GridLines = true;
            this.lsvPrice.HideSelection = false;
            this.lsvPrice.Location = new System.Drawing.Point(0, 0);
            this.lsvPrice.MultiSelect = false;
            this.lsvPrice.Name = "lsvPrice";
            this.lsvPrice.Size = new System.Drawing.Size(320, 546);
            this.lsvPrice.TabIndex = 24;
            this.lsvPrice.UseCompatibleStateImageBehavior = false;
            this.lsvPrice.View = System.Windows.Forms.View.Details;
            this.lsvPrice.SelectedIndexChanged += new System.EventHandler(this.lsvPrice_SelectedIndexChanged_1);
            this.lsvPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsvPrice_KeyDown);
            this.lsvPrice.Click += new System.EventHandler(this.lsvPrice_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "单据号";
            this.columnHeader1.Width = 110;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "创建人";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "创建日期";
            this.columnHeader4.Width = 140;
            // 
            // tab2
            // 
            this.tab2.Controls.Add(this.livPriceOk);
            this.tab2.Location = new System.Drawing.Point(4, 4);
            this.tab2.Name = "tab2";
            this.tab2.Size = new System.Drawing.Size(320, 553);
            this.tab2.TabIndex = 1;
            this.tab2.Text = "已审核";
            this.tab2.Visible = false;
            // 
            // livPriceOk
            // 
            this.livPriceOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.livPriceOk.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader8});
            this.livPriceOk.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.livPriceOk.FullRowSelect = true;
            this.livPriceOk.GridLines = true;
            this.livPriceOk.HideSelection = false;
            this.livPriceOk.Location = new System.Drawing.Point(0, 0);
            this.livPriceOk.Name = "livPriceOk";
            this.livPriceOk.Size = new System.Drawing.Size(317, 546);
            this.livPriceOk.TabIndex = 23;
            this.livPriceOk.UseCompatibleStateImageBehavior = false;
            this.livPriceOk.View = System.Windows.Forms.View.Details;
            this.livPriceOk.SelectedIndexChanged += new System.EventHandler(this.livPriceOk_SelectedIndexChanged);
            this.livPriceOk.Click += new System.EventHandler(this.livPriceOk_Click);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "单据号";
            this.columnHeader5.Width = 90;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "审核人";
            this.columnHeader7.Width = 70;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "审核日期";
            this.columnHeader8.Width = 150;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Location = new System.Drawing.Point(0, 584);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1024, 32);
            this.panel2.TabIndex = 1;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.label20.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.label19.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.label18.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label18.Location = new System.Drawing.Point(8, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(100, 23);
            this.label18.TabIndex = 0;
            this.label18.Text = "快捷键提示:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // controlMedicineFind
            // 
            this.controlMedicineFind.blIsMedStorage = true;
            this.controlMedicineFind.blISOutStorage = false;
            this.controlMedicineFind.blRepertory = true;
            this.controlMedicineFind.FindMedmode = 0;
            this.controlMedicineFind.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.controlMedicineFind.intIsReData = 0;
            this.controlMedicineFind.isApplMebMod = null;
            this.controlMedicineFind.isApplModel = false;
            this.controlMedicineFind.isShowFindType = true;
            this.controlMedicineFind.IsShowZero = true;
            this.controlMedicineFind.Location = new System.Drawing.Point(416, -328);
            this.controlMedicineFind.Name = "controlMedicineFind";
            this.controlMedicineFind.Size = new System.Drawing.Size(576, 336);
            this.controlMedicineFind.status = 0;
            this.controlMedicineFind.strMedstorage = null;
            this.controlMedicineFind.strSTORAGEID = "-1";
            this.controlMedicineFind.TabIndex = 1;
            this.controlMedicineFind.Visible = false;
            this.controlMedicineFind.m_evtReturnVal += new com.digitalwave.controls.dlgReturnVal(this.controlMedicineFind_m_evtReturnVal);
            // 
            // frmChangPrice
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1024, 621);
            this.Controls.Add(this.controlMedicineFind);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmChangPrice";
            this.Text = "药品调价";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmChangPrice_KeyDown);
            this.Load += new System.EventHandler(this.frmChangPrice_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.tabChang.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.tab2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region 设置窗体
		public override void CreateController()
		{
			this.objController = new clsControlChangPrice();
			this.objController.Set_GUI_Apperance(this);
		}
		#endregion

		private void frmChangPrice_Load(object sender, System.EventArgs e)
		{
			((clsControlChangPrice)this.objController).m_lngFrmLoad();
			this.m_mthSetEnter2Tab(new System.Windows.Forms.Control[] {txtMEDICINE});
			((clsControlChangPrice)this.objController).m_mthCreatBillNo();
		}

		private void TXTMEDICINEID_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{

		}

		private void dgrMedicine_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
		{
			this.TXTCHANGEPRICE.Focus();
		}

		private void TXTCHANGEPRICE_Enter(object sender, System.EventArgs e)
		{

		}

		private void tabChang_Enter(object sender, System.EventArgs e)
		{

		}

		private void groupBox3_Enter(object sender, System.EventArgs e)
		{

		}

		private void m_btnNew_Click(object sender, System.EventArgs e)
		{
			if(this.m_txtBillNo.Text.Trim() =="")
			{
				MessageBox.Show("请输入正确的单据号");
				this.m_txtBillNo.Focus();
				return;
			}
			this.m_txtBillNo.Text= clsPublicParm.m_mthGetNewDocument(this.m_txtBillNo.Text,"3",0);
			this.m_txtMemo.Text = "";
			this.label8.Text="";
			this.txtMEDICINE.Focus();
			btnSave.Enabled=true;
		}

		private void btnesc_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show("是否要退出药品调价系统？","系统提示",MessageBoxButtons.YesNo)==DialogResult.Yes)
			this.Close();
		}

		private void TXTCHANGEPRICE_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		}
		clsPublicParm publicClass=new clsPublicParm();
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if(txtMEDICINE.Text.Trim()=="")
			{
				publicClass.m_mthShowWarning(txtMEDICINE,"请先输入要调价的药品");
				txtMEDICINE.Focus();
				return;
			}
			if(TXTCHANGEPRICE.Text.Trim()==""||Double.Parse(TXTCHANGEPRICE.Text.Trim())==0)
			{
				publicClass.m_mthShowWarning(TXTCHANGEPRICE,"价格不能为空或'0'!");
				TXTCHANGEPRICE.Focus();
				return;
			}
			((clsControlChangPrice)this.objController).m_lngAddNew();
			this.label8.Text=((clsControlChangPrice)this.objController).m_getTotalMoney().ToString("0.0000");
			this.txtMEDICINE.Focus();
			((clsControlChangPrice)this.objController).m_mthClearText();
			this.btnAdd.Text = "增加(&C)";
			this.btnClear.Text = "清空(&C)";
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			((clsControlChangPrice)this.objController).m_lngSave();
		}

		private void lsvPrice_Click(object sender, System.EventArgs e)
		{
			this.btnSave.Text="修改(F2)";
			lsvPrice_SelectedIndexChanged(sender,null);
			buttonXP2.Enabled=true;
			btnAdd.Enabled=true;

		}

		private void livPriceOk_Click(object sender, System.EventArgs e)
		{
			
		}

		private void lisvChangPriceDe_Click(object sender, System.EventArgs e)
		{

			this.btnAdd.Text = "修改(&M)";
			this.btnClear.Text = "取消(&C)";
		}

		private void TXTMEDICINEID_Enter(object sender, System.EventArgs e)
		{

		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			((clsControlChangPrice)this.objController).m_mthClearText();
			this.btnAdd.Text = "增加(&C)";
			this.btnClear.Text = "清空(&C)";
		}

		private void btnDelect_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show("你确定删除此单吗？","系统提示",MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				((clsControlChangPrice)this.objController).m_lngDelEvent();
				lisvChangPriceDe.Items.Clear();
				label8.Text="0";
			}
		}

		private void dntEmp_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show("你确定审核吗？","系统提示",MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				((clsControlChangPrice)this.objController).m_lngConfirm();
				label8.Text="0";
			}
		}

		private void tabChang_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.label8.Text="0";
			if(this.tabChang.SelectedIndex!=0)
			{
				this.panel4.Enabled=false;
				this.panel6.Enabled=false;
				this.btnSave.Enabled=false;
				this.dntEmp.Enabled=false;
				this.buttonXP2.Enabled=false;
				this.buttonXP3.Enabled=false;
				this.buttonXP1.Enabled=false;
			}
			else
			{
				this.buttonXP3.Enabled=true;
				this.panel4.Enabled=true;
				this.panel6.Enabled=true;
				this.btnSave.Enabled=true;
				this.dntEmp.Enabled=true;
				this.buttonXP2.Enabled=true;
				this.buttonXP1.Enabled=true;
			}
			((clsControlChangPrice)this.objController).m_mthGreatNew();
		}

		private void btnFinddata_Click(object sender, System.EventArgs e)
		{
			((clsControlChangPrice)this.objController).m_lngFindData();
		}

		private void btnColes_Click(object sender, System.EventArgs e)
		{
			this.lisvChangPriceDe.Height+=this.panel6.Height;
			this.panel6.Visible=false;
		}

		private void btnFind_Click(object sender, System.EventArgs e)
		{
			if(this.panel6.Visible==true)
				return;
			this.lisvChangPriceDe.Height-=this.panel6.Height;
		   this.panel6.Visible=true;
		}

		private void TextID_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void GrearNAME_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		   this.m_mthSetKeyTab(e);
		}

		private void APPLDATE_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void ADUITDATE_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void ADUITEMP_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{

		}

		private void frmChangPrice_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetEnter2Tab(new System.Windows.Forms.Control[] {});
			if(e.KeyCode==Keys.Enter)
			{
				this.m_mthSetKeyTab(e);
			}
		}

		private void lsvPrice_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			
		}	
		
		
		private void comPriod_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlChangPrice)this.objController).m_lngPriodchang();
		}

		private void livPriceOk_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.livPriceOk.Items.Count > 0)
			{
				if(this.livPriceOk.SelectedItems.Count > 0)
				{
					((clsControlChangPrice)this.objController).m_mthSelectedBill(sender);
					this.label8.Text=((clsControlChangPrice)this.objController).m_getTotalMoney().ToString();
				}
			}
		}

		private void TXTMEDICINEID_DoubleClick(object sender, System.EventArgs e)
		{
		}

		private void dateTime_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		
		}
		int count=0;
		private void dateTime_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				SendKeys.Send("{Right}");
				count++;
				if(count>2)
				{
					SendKeys.Send("{TAB}");
					count=0;
				}
			}
		}

		private void txtMEDICINE_Enter(object sender, System.EventArgs e)
		{
			this.controlMedicineFind.strSTORAGEID = "-1";
					
			this.txtMEDICINE.Tag = null;
			this.txtMEDICINE.SelectAll();	
		}

		private void txtMEDICINE_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
			{
				return ;
			}
		}

		private void m_btnDel_Click(object sender, System.EventArgs e)
		{
			((clsControlChangPrice)this.objController).DelDetial();
		}

		private void lsvPrice_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.lsvPrice.Items.Count > 0)
			{
				if(this.lsvPrice.SelectedItems.Count > 0)
				{
					try
					{
                        lisvChangPriceDe.Items.Clear();
						((clsControlChangPrice)this.objController).m_mthSelectedBill(sender);
						this.label8.Text=((clsControlChangPrice)this.objController).m_getTotalMoney().ToString();
						if(lisvChangPriceDe.Items.Count>0)
						{
							dntEmp.Enabled=true;
							btnSave.Text="修改(&S)";
						}
					}
					catch
					{

					}
				}
			}
		}

		private void m_BtnPrint_Click(object sender, System.EventArgs e)
		{
			((clsControlChangPrice)this.objController).m_mthPrintChangePrice();
		}

		private void controlMedicineFind_Leave(object sender, System.EventArgs e)
		{
			this.controlMedicineFind.Visible =false;
		}

		private void controlMedicineFind_m_evtReturnVal(object sender, com.digitalwave.controls.clsEvtReturnVal e)
		{
			if(((clsControlChangPrice)this.objController).AddArr.Count!=0)
			{
				for(int i1=0;i1<((clsControlChangPrice)this.objController).AddArr.Count;i1++)
				{
					if(((clsControlChangPrice)this.objController).AddArr[i1].ToString()==e.ReturnVo.strMEDICINEID_CHR)
					{
						publicClass.m_mthShowWarning(txtMEDICINE,"此药品在调价单中已经存在!");
						this.controlMedicineFind.Visible =false;
						this.txtMEDICINE.Focus();
						return;
					}
				}
			}
			((clsControlChangPrice)this.objController).AddArr.Add(e.ReturnVo.strMEDICINEID_CHR);
			this.txtMEDICINE.Text = e.ReturnVo.strMEDICINENAME_VCHR;
			this.txtMEDICINE.Tag = e.ReturnVo.strMEDICINEID_CHR;
			this.txtMEDSPEC.Text = e.ReturnVo.strMEDSPEC_VCHR;
			this.txtMEDSPEC.Tag = e.ReturnVo.strASSISTCODE_CHR;
			this.m_txtPRICE.Text = e.ReturnVo.dlUNITPRICE_MNY.ToString();
			this.m_txtUNIT.Text = e.ReturnVo.strOPUNIT_CHR;
			this.txtAllAmount.Text = e.ReturnVo.dlAMOUNT_DEC.ToString();
			this.controlMedicineFind.Visible =false;
			this.TXTCHANGEPRICE.Focus();
		}

		private void txtMEDICINE_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				Point p=this.txtMEDICINE.Parent.PointToScreen(txtMEDICINE.Location);
				p=this.FindForm().PointToClient(p);
				p.Y-=this.controlMedicineFind.Height;
                this.controlMedicineFind.m_txtFindMed.Text = txtMEDICINE.Text;
				this.controlMedicineFind.Location=p;
				this.controlMedicineFind.Width+=20;
				this.controlMedicineFind.Visible =true;				
				this.controlMedicineFind.Focus();
			}
		}

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void m_txtMemo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		}

		private void TXTCHANGEPRICE_Leave(object sender, System.EventArgs e)
		{
			((clsControlChangPrice)this.objController).m_lngCountNuber();
			this.btnAdd.Focus();
		}

		private void label27_Click(object sender, System.EventArgs e)
		{
		
		}

		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
			((clsControlChangPrice)this.objController).m_mthGreatNew();
		}

		private void lisvChangPriceDe_Click_2(object sender, System.EventArgs e)
		{
			((clsControlChangPrice)this.objController).m_mthDClik();
		}

		private void buttonXP2_Click(object sender, System.EventArgs e)
		{
			((clsControlChangPrice)this.objController).m_mthDeleData();
		}

		private void lisvChangPriceDe_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void buttonXP3_Click(object sender, System.EventArgs e)
		{
			((clsControlChangPrice)this.objController).m_mthUnite();
		}

		private void txtDocEnd_Leave(object sender, System.EventArgs e)
		{
			try
			{
				int EndDoc=int.Parse(txtDocEnd.Text);
				if(txtDocEnd.Text.Length<3)
				{
					publicClass.m_mthShowWarning(txtDocEnd,"输入的单据号长度不够!");
					txtDocEnd.Focus();
				}
			}
			catch
			{
				publicClass.m_mthShowWarning(txtDocEnd,"输入的单据号不正确!");
				txtDocEnd.Focus();
				
			}
		}

		private void dateTime_ValueChanged(object sender, System.EventArgs e)
		{
			((clsControlChangPrice)this.objController).m_mthCreatBillNo();
		}

		private void txtDocEnd_Enter(object sender, System.EventArgs e)
		{
			txtDocEnd.SelectAll();
		}

        private void lsvPrice_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
	}
}
