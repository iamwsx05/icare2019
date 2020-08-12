using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;
namespace  com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmMedStorageOutOrd 的摘要说明。
	/// </summary>
	public class frmMedStorageOutOrd : com.digitalwave.GUI_Base.frmMDI_Child_Base	//GUI_Base.dll
	{
		internal System.Windows.Forms.Panel panel1;
		internal PinkieControls.ButtonXP bnlcolse;
		internal PinkieControls.ButtonXP bnlfind;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader m_clhMedStoNow;
		private System.Windows.Forms.ColumnHeader BUYUNITPRICE;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader MedName;
		private System.Windows.Forms.ColumnHeader m_clhMedID;
		private System.Windows.Forms.ColumnHeader Unit;
		private System.Windows.Forms.ColumnHeader m_clhMedProductArea;
		private System.Windows.Forms.ColumnHeader m_clhMedUsefulLife;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		internal System.Windows.Forms.Panel panel3;
		internal System.Windows.Forms.Panel panel2;
		internal System.Windows.Forms.GroupBox groupBox3;
		internal PinkieControls.ButtonXP dntEmp;
		internal PinkieControls.ButtonXP btnesc;
		internal PinkieControls.ButtonXP btnDelect;
		internal PinkieControls.ButtonXP btnFind;
		internal PinkieControls.ButtonXP btnSave;
		internal PinkieControls.ButtonXP m_btnNew;
		private System.Windows.Forms.ColumnHeader Row;
		private System.Windows.Forms.ColumnHeader columnHeader32;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader30;
		private System.Windows.Forms.ColumnHeader columnHeader25;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader31;
		private System.Windows.Forms.ColumnHeader columnHeader33;
		private System.Windows.Forms.ColumnHeader clhInvoiceNo;
		private System.Windows.Forms.ColumnHeader m_clhMedLotNo;
		private System.Windows.Forms.ColumnHeader m_clhDisCount;
		private System.Windows.Forms.ColumnHeader m_clhMedQty;
		private System.Windows.Forms.ColumnHeader m_clhRowNO;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Windows.Forms.ColumnHeader columnHeader26;
		private System.Windows.Forms.ColumnHeader columnHeader24;
		private System.Windows.Forms.ColumnHeader columnHeader27;
		private System.Windows.Forms.ColumnHeader columnHeader28;
		private System.Windows.Forms.ColumnHeader columnHeader29;
		internal System.Windows.Forms.ListView m_lsvDetail;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader PACKAGEPRICE;
		private System.Windows.Forms.ColumnHeader tolmony;
		private System.Windows.Forms.ColumnHeader PACKAGEQTY_DEC;
		private System.Windows.Forms.ColumnHeader m_clhMedUnit;
		private System.Windows.Forms.ColumnHeader m_clhMedName;
		internal System.Windows.Forms.TabControl m_tabAduit;
		private System.Windows.Forms.TabPage m_tbpUnAduit;
		internal System.Windows.Forms.ListView m_lsvUnAduit;
		private System.Windows.Forms.TabPage m_tbpEnAduit;
		internal System.Windows.Forms.ListView m_lsvEnAduit;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.Panel panel6;
		internal System.Windows.Forms.ComboBox comPriod;
		internal System.Windows.Forms.Panel panel5;
		internal System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		internal System.Windows.Forms.TextBox textBox1;
		internal System.Windows.Forms.ComboBox comboBox1;
		internal PinkieControls.ButtonXP buttonXP1;
		private System.Windows.Forms.Panel panel7;
		private com.digitalwave.Utility.Controls.CollapsibleSplitter collapsibleSplitter1;
		internal System.Windows.Forms.ContextMenu contextMenu1;
        internal PinkieControls.ButtonXP buttonXP2;
        private Panel panel4;
        private Panel panel8;
        private com.digitalwave.Utility.Controls.CollapsibleSplitter collapsibleSplitter2;
        private IContainer components;

		public frmMedStorageOutOrd()
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
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.bnlcolse = new PinkieControls.ButtonXP();
            this.bnlfind = new PinkieControls.ButtonXP();
            this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedStoNow = new System.Windows.Forms.ColumnHeader();
            this.BUYUNITPRICE = new System.Windows.Forms.ColumnHeader();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.MedName = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedID = new System.Windows.Forms.ColumnHeader();
            this.Unit = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedProductArea = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedUsefulLife = new System.Windows.Forms.ColumnHeader();
            this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonXP2 = new PinkieControls.ButtonXP();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.dntEmp = new PinkieControls.ButtonXP();
            this.btnesc = new PinkieControls.ButtonXP();
            this.btnDelect = new PinkieControls.ButtonXP();
            this.btnFind = new PinkieControls.ButtonXP();
            this.btnSave = new PinkieControls.ButtonXP();
            this.m_btnNew = new PinkieControls.ButtonXP();
            this.Row = new System.Windows.Forms.ColumnHeader();
            this.columnHeader32 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader30 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader25 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader31 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader33 = new System.Windows.Forms.ColumnHeader();
            this.clhInvoiceNo = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedLotNo = new System.Windows.Forms.ColumnHeader();
            this.comPriod = new System.Windows.Forms.ComboBox();
            this.m_clhDisCount = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedQty = new System.Windows.Forms.ColumnHeader();
            this.m_clhRowNO = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader23 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader26 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader24 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader27 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader28 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader29 = new System.Windows.Forms.ColumnHeader();
            this.m_lsvDetail = new System.Windows.Forms.ListView();
            this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.PACKAGEPRICE = new System.Windows.Forms.ColumnHeader();
            this.tolmony = new System.Windows.Forms.ColumnHeader();
            this.PACKAGEQTY_DEC = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedUnit = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedName = new System.Windows.Forms.ColumnHeader();
            this.m_tabAduit = new System.Windows.Forms.TabControl();
            this.m_tbpUnAduit = new System.Windows.Forms.TabPage();
            this.m_lsvUnAduit = new System.Windows.Forms.ListView();
            this.m_tbpEnAduit = new System.Windows.Forms.TabPage();
            this.m_lsvEnAduit = new System.Windows.Forms.ListView();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.collapsibleSplitter2 = new com.digitalwave.Utility.Controls.CollapsibleSplitter();
            this.panel5 = new System.Windows.Forms.Panel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel7 = new System.Windows.Forms.Panel();
            this.collapsibleSplitter1 = new com.digitalwave.Utility.Controls.CollapsibleSplitter();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.m_tabAduit.SuspendLayout();
            this.m_tbpUnAduit.SuspendLayout();
            this.m_tbpEnAduit.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.bnlcolse);
            this.panel1.Controls.Add(this.bnlfind);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 108);
            this.panel1.TabIndex = 144;
            this.panel1.Visible = false;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.Location = new System.Drawing.Point(8, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 16);
            this.label13.TabIndex = 151;
            this.label13.Text = "查找内容";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.Location = new System.Drawing.Point(8, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 16);
            this.label12.TabIndex = 150;
            this.label12.Text = "查找方式";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Location = new System.Drawing.Point(72, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(168, 23);
            this.textBox1.TabIndex = 149;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox1.Items.AddRange(new object[] {
            "单据号",
            "创建人",
            "创建时间",
            "收货单位",
            "出库日期"});
            this.comboBox1.Location = new System.Drawing.Point(72, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(168, 22);
            this.comboBox1.TabIndex = 148;
            // 
            // bnlcolse
            // 
            this.bnlcolse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bnlcolse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.bnlcolse.DefaultScheme = true;
            this.bnlcolse.DialogResult = System.Windows.Forms.DialogResult.None;
            this.bnlcolse.Hint = "";
            this.bnlcolse.Location = new System.Drawing.Point(154, 67);
            this.bnlcolse.Name = "bnlcolse";
            this.bnlcolse.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.bnlcolse.Size = new System.Drawing.Size(72, 24);
            this.bnlcolse.TabIndex = 143;
            this.bnlcolse.Text = "返回(&R)";
            this.bnlcolse.Click += new System.EventHandler(this.bnlcolse_Click);
            // 
            // bnlfind
            // 
            this.bnlfind.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bnlfind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.bnlfind.DefaultScheme = true;
            this.bnlfind.DialogResult = System.Windows.Forms.DialogResult.None;
            this.bnlfind.Hint = "";
            this.bnlfind.Location = new System.Drawing.Point(58, 67);
            this.bnlfind.Name = "bnlfind";
            this.bnlfind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.bnlfind.Size = new System.Drawing.Size(72, 24);
            this.bnlfind.TabIndex = 142;
            this.bnlfind.Text = "查找(&K)";
            this.bnlfind.Click += new System.EventHandler(this.bnlfind_Click);
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "出库数量";
            this.columnHeader20.Width = 80;
            // 
            // m_clhMedStoNow
            // 
            this.m_clhMedStoNow.Text = "现库存";
            this.m_clhMedStoNow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BUYUNITPRICE
            // 
            this.BUYUNITPRICE.Text = "出库单价";
            this.BUYUNITPRICE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.BUYUNITPRICE.Width = 72;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "金额";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader15.Width = 80;
            // 
            // MedName
            // 
            this.MedName.Text = "药品名称";
            this.MedName.Width = 180;
            // 
            // m_clhMedID
            // 
            this.m_clhMedID.Text = "药品代码";
            this.m_clhMedID.Width = 85;
            // 
            // Unit
            // 
            this.Unit.Text = "单位";
            this.Unit.Width = 40;
            // 
            // m_clhMedProductArea
            // 
            this.m_clhMedProductArea.Text = "生产厂家";
            this.m_clhMedProductArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_clhMedProductArea.Width = 100;
            // 
            // m_clhMedUsefulLife
            // 
            this.m_clhMedUsefulLife.Text = "失效期";
            this.m_clhMedUsefulLife.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_clhMedUsefulLife.Width = 100;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "系统批号";
            this.columnHeader21.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "创建时间";
            this.columnHeader6.Width = 120;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "单据类型";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "单据号";
            this.columnHeader8.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "单据号";
            this.columnHeader5.Width = 80;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(696, 120);
            this.panel3.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Location = new System.Drawing.Point(0, 128);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(696, 136);
            this.panel2.TabIndex = 146;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.buttonXP2);
            this.groupBox3.Controls.Add(this.buttonXP1);
            this.groupBox3.Controls.Add(this.dntEmp);
            this.groupBox3.Controls.Add(this.btnesc);
            this.groupBox3.Controls.Add(this.btnDelect);
            this.groupBox3.Controls.Add(this.btnFind);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.m_btnNew);
            this.groupBox3.Location = new System.Drawing.Point(264, 504);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(696, 48);
            this.groupBox3.TabIndex = 138;
            this.groupBox3.TabStop = false;
            // 
            // buttonXP2
            // 
            this.buttonXP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP2.DefaultScheme = true;
            this.buttonXP2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP2.Hint = "";
            this.buttonXP2.Location = new System.Drawing.Point(520, 16);
            this.buttonXP2.Name = "buttonXP2";
            this.buttonXP2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP2.Size = new System.Drawing.Size(72, 24);
            this.buttonXP2.TabIndex = 56;
            this.buttonXP2.TabStop = false;
            this.buttonXP2.Text = "刷新(&H)";
            this.buttonXP2.Click += new System.EventHandler(this.buttonXP2_Click);
            // 
            // buttonXP1
            // 
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Enabled = false;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(280, 16);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(72, 24);
            this.buttonXP1.TabIndex = 55;
            this.buttonXP1.TabStop = false;
            this.buttonXP1.Text = "打印(&P)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // dntEmp
            // 
            this.dntEmp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.dntEmp.DefaultScheme = true;
            this.dntEmp.DialogResult = System.Windows.Forms.DialogResult.None;
            this.dntEmp.Hint = "";
            this.dntEmp.Location = new System.Drawing.Point(200, 16);
            this.dntEmp.Name = "dntEmp";
            this.dntEmp.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.dntEmp.Size = new System.Drawing.Size(72, 24);
            this.dntEmp.TabIndex = 54;
            this.dntEmp.TabStop = false;
            this.dntEmp.Text = "审核(&O)";
            this.dntEmp.Click += new System.EventHandler(this.dntEmp_Click);
            // 
            // btnesc
            // 
            this.btnesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnesc.DefaultScheme = true;
            this.btnesc.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnesc.Hint = "";
            this.btnesc.Location = new System.Drawing.Point(600, 16);
            this.btnesc.Name = "btnesc";
            this.btnesc.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnesc.Size = new System.Drawing.Size(74, 24);
            this.btnesc.TabIndex = 53;
            this.btnesc.TabStop = false;
            this.btnesc.Text = "退出(ESE)";
            this.btnesc.Click += new System.EventHandler(this.btnesc_Click);
            // 
            // btnDelect
            // 
            this.btnDelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnDelect.DefaultScheme = true;
            this.btnDelect.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDelect.Hint = "";
            this.btnDelect.Location = new System.Drawing.Point(440, 16);
            this.btnDelect.Name = "btnDelect";
            this.btnDelect.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnDelect.Size = new System.Drawing.Size(72, 24);
            this.btnDelect.TabIndex = 52;
            this.btnDelect.TabStop = false;
            this.btnDelect.Text = "删除(&D)";
            this.btnDelect.Click += new System.EventHandler(this.btnDelect_Click);
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnFind.DefaultScheme = true;
            this.btnFind.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFind.Hint = "";
            this.btnFind.Location = new System.Drawing.Point(360, 16);
            this.btnFind.Name = "btnFind";
            this.btnFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnFind.Size = new System.Drawing.Size(72, 24);
            this.btnFind.TabIndex = 51;
            this.btnFind.TabStop = false;
            this.btnFind.Text = "查找(&F)";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnSave.DefaultScheme = true;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.Hint = "";
            this.btnSave.Location = new System.Drawing.Point(120, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnSave.Size = new System.Drawing.Size(72, 24);
            this.btnSave.TabIndex = 17;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // m_btnNew
            // 
            this.m_btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnNew.DefaultScheme = true;
            this.m_btnNew.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnNew.Hint = "";
            this.m_btnNew.Location = new System.Drawing.Point(40, 16);
            this.m_btnNew.Name = "m_btnNew";
            this.m_btnNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnNew.Size = new System.Drawing.Size(72, 24);
            this.m_btnNew.TabIndex = 49;
            this.m_btnNew.TabStop = false;
            this.m_btnNew.Text = "新建(&N)";
            this.m_btnNew.Click += new System.EventHandler(this.m_btnNew_Click);
            // 
            // Row
            // 
            this.Row.Text = "行号";
            this.Row.Width = 0;
            // 
            // columnHeader32
            // 
            this.columnHeader32.Text = "创建人";
            this.columnHeader32.Width = 70;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "单据类型";
            this.columnHeader11.Width = 0;
            // 
            // columnHeader30
            // 
            this.columnHeader30.Text = "审核人";
            this.columnHeader30.Width = 70;
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "出库日期";
            this.columnHeader25.Width = 150;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "创建时间";
            this.columnHeader12.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "单据号";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader31
            // 
            this.columnHeader31.Text = "收货单位";
            this.columnHeader31.Width = 100;
            // 
            // columnHeader33
            // 
            this.columnHeader33.Text = "总金额";
            this.columnHeader33.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader33.Width = 80;
            // 
            // clhInvoiceNo
            // 
            this.clhInvoiceNo.Text = "发票号";
            this.clhInvoiceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clhInvoiceNo.Width = 80;
            // 
            // m_clhMedLotNo
            // 
            this.m_clhMedLotNo.Text = "批号";
            this.m_clhMedLotNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_clhMedLotNo.Width = 100;
            // 
            // comPriod
            // 
            this.comPriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comPriod.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comPriod.Location = new System.Drawing.Point(0, 0);
            this.comPriod.Name = "comPriod";
            this.comPriod.Size = new System.Drawing.Size(107, 22);
            this.comPriod.TabIndex = 147;
            this.comPriod.SelectedIndexChanged += new System.EventHandler(this.comPriod_SelectedIndexChanged);
            // 
            // m_clhDisCount
            // 
            this.m_clhDisCount.Text = "折扣";
            // 
            // m_clhMedQty
            // 
            this.m_clhMedQty.Text = "数量";
            this.m_clhMedQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // m_clhRowNO
            // 
            this.m_clhRowNO.Text = "行号";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "单据号";
            this.columnHeader1.Width = 110;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "单据号";
            this.columnHeader22.Width = 100;
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "单据类型";
            this.columnHeader23.Width = 0;
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "创建人";
            this.columnHeader26.Width = 90;
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "出库日期";
            this.columnHeader24.Width = 100;
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "审核人";
            this.columnHeader27.Width = 0;
            // 
            // columnHeader28
            // 
            this.columnHeader28.Text = "收货单位";
            this.columnHeader28.Width = 100;
            // 
            // columnHeader29
            // 
            this.columnHeader29.Text = "总金额";
            this.columnHeader29.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader29.Width = 80;
            // 
            // m_lsvDetail
            // 
            this.m_lsvDetail.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.m_lsvDetail.AllowColumnReorder = true;
            this.m_lsvDetail.AllowDrop = true;
            this.m_lsvDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Row,
            this.m_clhMedID,
            this.MedName,
            this.columnHeader17,
            this.m_clhMedProductArea,
            this.BUYUNITPRICE,
            this.columnHeader20,
            this.Unit,
            this.columnHeader15,
            this.m_clhMedStoNow,
            this.m_clhMedUsefulLife,
            this.columnHeader21});
            this.m_lsvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvDetail.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvDetail.FullRowSelect = true;
            this.m_lsvDetail.GridLines = true;
            this.m_lsvDetail.HideSelection = false;
            this.m_lsvDetail.Location = new System.Drawing.Point(0, 0);
            this.m_lsvDetail.MultiSelect = false;
            this.m_lsvDetail.Name = "m_lsvDetail";
            this.m_lsvDetail.Size = new System.Drawing.Size(960, 240);
            this.m_lsvDetail.TabIndex = 137;
            this.m_lsvDetail.TabStop = false;
            this.m_lsvDetail.UseCompatibleStateImageBehavior = false;
            this.m_lsvDetail.View = System.Windows.Forms.View.Details;
            this.m_lsvDetail.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_lsvDetail_DragEnter);
            this.m_lsvDetail.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_lsvDetail_DragDrop);
            this.m_lsvDetail.SelectedIndexChanged += new System.EventHandler(this.m_lsvDetail_SelectedIndexChanged);
            this.m_lsvDetail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvDetail_KeyDown);
            this.m_lsvDetail.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_lsvDetail_ItemDrag);
            this.m_lsvDetail.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_lsvDetail_MouseDown);
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "产品规格";
            this.columnHeader17.Width = 160;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "单据类型";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 100;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "创建时间";
            this.columnHeader10.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "单据类型";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "创建时间";
            this.columnHeader3.Width = 120;
            // 
            // PACKAGEPRICE
            // 
            this.PACKAGEPRICE.Text = "包装单价";
            this.PACKAGEPRICE.Width = 91;
            // 
            // tolmony
            // 
            this.tolmony.Text = "入库金额";
            this.tolmony.Width = 69;
            // 
            // PACKAGEQTY_DEC
            // 
            this.PACKAGEQTY_DEC.Text = "包装数量";
            this.PACKAGEQTY_DEC.Width = 87;
            // 
            // m_clhMedUnit
            // 
            this.m_clhMedUnit.Text = "单位";
            this.m_clhMedUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // m_clhMedName
            // 
            this.m_clhMedName.Text = "名称";
            this.m_clhMedName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_clhMedName.Width = 100;
            // 
            // m_tabAduit
            // 
            this.m_tabAduit.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.m_tabAduit.Controls.Add(this.m_tbpUnAduit);
            this.m_tabAduit.Controls.Add(this.m_tbpEnAduit);
            this.m_tabAduit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabAduit.ItemSize = new System.Drawing.Size(0, 17);
            this.m_tabAduit.Location = new System.Drawing.Point(0, 0);
            this.m_tabAduit.Multiline = true;
            this.m_tabAduit.Name = "m_tabAduit";
            this.m_tabAduit.SelectedIndex = 0;
            this.m_tabAduit.Size = new System.Drawing.Size(260, 192);
            this.m_tabAduit.TabIndex = 139;
            this.m_tabAduit.TabStop = false;
            this.m_tabAduit.SelectedIndexChanged += new System.EventHandler(this.m_tabAduit_SelectedIndexChanged);
            // 
            // m_tbpUnAduit
            // 
            this.m_tbpUnAduit.Controls.Add(this.m_lsvUnAduit);
            this.m_tbpUnAduit.Location = new System.Drawing.Point(4, 4);
            this.m_tbpUnAduit.Name = "m_tbpUnAduit";
            this.m_tbpUnAduit.Size = new System.Drawing.Size(252, 167);
            this.m_tbpUnAduit.TabIndex = 0;
            this.m_tbpUnAduit.Text = "未审核";
            // 
            // m_lsvUnAduit
            // 
            this.m_lsvUnAduit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader22,
            this.columnHeader23,
            this.columnHeader26,
            this.columnHeader24,
            this.columnHeader27,
            this.columnHeader28,
            this.columnHeader29});
            this.m_lsvUnAduit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvUnAduit.FullRowSelect = true;
            this.m_lsvUnAduit.GridLines = true;
            this.m_lsvUnAduit.HideSelection = false;
            this.m_lsvUnAduit.Location = new System.Drawing.Point(0, 0);
            this.m_lsvUnAduit.MultiSelect = false;
            this.m_lsvUnAduit.Name = "m_lsvUnAduit";
            this.m_lsvUnAduit.Size = new System.Drawing.Size(252, 167);
            this.m_lsvUnAduit.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.m_lsvUnAduit.TabIndex = 0;
            this.m_lsvUnAduit.TabStop = false;
            this.m_lsvUnAduit.UseCompatibleStateImageBehavior = false;
            this.m_lsvUnAduit.View = System.Windows.Forms.View.Details;
            this.m_lsvUnAduit.SelectedIndexChanged += new System.EventHandler(this.m_lsvUnAduit_SelectedIndexChanged);
            this.m_lsvUnAduit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvUnAduit_KeyDown);
            this.m_lsvUnAduit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_lsvUnAduit_MouseDown);
            // 
            // m_tbpEnAduit
            // 
            this.m_tbpEnAduit.Controls.Add(this.m_lsvEnAduit);
            this.m_tbpEnAduit.Location = new System.Drawing.Point(4, 4);
            this.m_tbpEnAduit.Name = "m_tbpEnAduit";
            this.m_tbpEnAduit.Size = new System.Drawing.Size(252, 167);
            this.m_tbpEnAduit.TabIndex = 1;
            this.m_tbpEnAduit.Text = "已审核";
            this.m_tbpEnAduit.Visible = false;
            // 
            // m_lsvEnAduit
            // 
            this.m_lsvEnAduit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader11,
            this.columnHeader32,
            this.columnHeader25,
            this.columnHeader30,
            this.columnHeader31,
            this.columnHeader33});
            this.m_lsvEnAduit.ContextMenu = this.contextMenu1;
            this.m_lsvEnAduit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvEnAduit.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_lsvEnAduit.FullRowSelect = true;
            this.m_lsvEnAduit.GridLines = true;
            this.m_lsvEnAduit.HideSelection = false;
            this.m_lsvEnAduit.Location = new System.Drawing.Point(0, 0);
            this.m_lsvEnAduit.MultiSelect = false;
            this.m_lsvEnAduit.Name = "m_lsvEnAduit";
            this.m_lsvEnAduit.Size = new System.Drawing.Size(252, 167);
            this.m_lsvEnAduit.TabIndex = 0;
            this.m_lsvEnAduit.TabStop = false;
            this.m_lsvEnAduit.UseCompatibleStateImageBehavior = false;
            this.m_lsvEnAduit.View = System.Windows.Forms.View.Details;
            this.m_lsvEnAduit.SelectedIndexChanged += new System.EventHandler(this.m_lsvEnAduit_SelectedIndexChanged);
            this.m_lsvEnAduit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvEnAduit_KeyDown);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.panel4);
            this.panel6.Controls.Add(this.collapsibleSplitter2);
            this.panel6.Controls.Add(this.panel1);
            this.panel6.Location = new System.Drawing.Point(0, 240);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(264, 312);
            this.panel6.TabIndex = 149;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.m_tabAduit);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(260, 192);
            this.panel4.TabIndex = 147;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.Controls.Add(this.comPriod);
            this.panel8.Location = new System.Drawing.Point(152, 171);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(107, 22);
            this.panel8.TabIndex = 0;
            // 
            // collapsibleSplitter2
            // 
            this.collapsibleSplitter2.AnimationDelay = 20;
            this.collapsibleSplitter2.AnimationStep = 20;
            this.collapsibleSplitter2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.collapsibleSplitter2.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter2.ControlToHide = this.panel1;
            this.collapsibleSplitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.collapsibleSplitter2.ExpandParentForm = false;
            this.collapsibleSplitter2.Location = new System.Drawing.Point(0, 192);
            this.collapsibleSplitter2.Name = "collapsibleSplitter2";
            this.collapsibleSplitter2.Size = new System.Drawing.Size(260, 8);
            this.collapsibleSplitter2.TabIndex = 148;
            this.collapsibleSplitter2.TabStop = false;
            this.collapsibleSplitter2.UseAnimations = false;
            this.collapsibleSplitter2.VisualStyle = com.digitalwave.Utility.Controls.VisualStyles.Mozilla;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.panel3);
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Location = new System.Drawing.Point(264, 240);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(696, 264);
            this.panel5.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Controls.Add(this.collapsibleSplitter1);
            this.panel7.Controls.Add(this.m_lsvDetail);
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(960, 240);
            this.panel7.TabIndex = 150;
            // 
            // collapsibleSplitter1
            // 
            this.collapsibleSplitter1.AnimationDelay = 20;
            this.collapsibleSplitter1.AnimationStep = 20;
            this.collapsibleSplitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter1.ControlToHide = null;
            this.collapsibleSplitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.collapsibleSplitter1.ExpandParentForm = false;
            this.collapsibleSplitter1.Location = new System.Drawing.Point(952, 0);
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.Size = new System.Drawing.Size(8, 240);
            this.collapsibleSplitter1.TabIndex = 138;
            this.collapsibleSplitter1.TabStop = false;
            this.collapsibleSplitter1.UseAnimations = false;
            this.collapsibleSplitter1.VisualStyle = com.digitalwave.Utility.Controls.VisualStyles.Mozilla;
            this.collapsibleSplitter1.Click += new System.EventHandler(this.collapsibleSplitter1_Click);
            // 
            // frmMedStorageOutOrd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(960, 557);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmMedStorageOutOrd";
            this.Text = "药品出库";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMedStorageOutOrd_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMedStorageOutOrd_KeyDown);
            this.Load += new System.EventHandler(this.frmMedStorageOutOrd_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.m_tabAduit.ResumeLayout(false);
            this.m_tbpUnAduit.ResumeLayout(false);
            this.m_tbpEnAduit.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			this.objController = new com.digitalwave.iCare.gui.HIS.clsControlMedStorageOutOrd();
			this.objController.Set_GUI_Apperance(this);
		}
		/// <summary>
		/// 1院内，0院外
		/// </summary>
		public int depttype=0;
		public com.digitalwave.controls.Control.ctlStorageInOutMedBase m_ctlStorageOutCK;
		public com.digitalwave.controls.Control.ctlStorageOrdBase m_ctlStorageOrdCK;
		public void sendOrdType(string ordTypeID)
		{
			long lngRes;
			clsDomainControlStorageOrd objManage = new clsDomainControlStorageOrd();
            clsStorageOrdType_VO ordTypeVo = new clsStorageOrdType_VO();
            lngRes = objManage.m_lngFindOrdTypeNameByID(ordTypeID, out ordTypeVo,"");
            depttype = ordTypeVo.m_intDeptType;
            
            if (lngRes == 1 && ordTypeVo.m_strStorageOrdTypeName != "")
			{
                this.Text = ordTypeVo.m_strStorageOrdTypeName;
                this.Name = ordTypeVo.m_strStorageOrdTypeName;
                if (depttype == 1)
				{
					//院内
					m_ctlStorageOrdCK=new com.digitalwave.controls.Control.ctlStorageOrdCK();
					m_ctlStorageOutCK=new com.digitalwave.controls.Control.ctlStorageOutMedCK();
                    m_ctlStorageOrdCK.intFlag = 2;
					m_ctlStorageOrdCK.blIsInside=true;
				}
				else
				{
					m_ctlStorageOrdCK=new com.digitalwave.controls.Control.ctlStorageOrdTH();
					m_ctlStorageOutCK=new com.digitalwave.controls.Control.ctlStorageOutMedTH();
					this.m_lsvDetail.Columns.Insert(5,"出单数量",80,HorizontalAlignment.Left);
					this.m_lsvDetail.Columns.Insert(6,"出单单位",80,HorizontalAlignment.Right);
					this.m_lsvDetail.Columns.Insert(7,"出单单价",80,HorizontalAlignment.Right);
					this.m_lsvDetail.Columns.Insert(8,"出单包装量",90,HorizontalAlignment.Left);
                    m_ctlStorageOrdCK.intFlag = 4;
					m_ctlStorageOrdCK.blIsInside=false;
				}
				this.m_ctlStorageOutCK.FindMedmode=0;
				this.panel2.Controls.Add(m_ctlStorageOutCK);
				m_ctlStorageOutCK.Dock=DockStyle.Fill;
				this.panel3.Controls.Add(m_ctlStorageOrdCK);
				m_ctlStorageOrdCK.Dock=DockStyle.Fill;
				m_ctlStorageOrdCK.NextControl =m_ctlStorageOutCK.m_txtMedName;
				
				m_ctlStorageOrdCK.strGetOrdTypeID=ordTypeID;
                m_ctlStorageOrdCK.strGetOrdType = ordTypeVo.m_strStorageOrdTypeName;
				m_ctlStorageOrdCK.strGetLoginName=this.LoginInfo.m_strEmpName;
				m_ctlStorageOrdCK.strGetLoginID=this.LoginInfo.m_strEmpID;
				m_ctlStorageOrdCK.e_evtStorageChang+=new EventHandler(m_ctlStorageOrdCK_e_evtStorageChang);
				m_ctlStorageOutCK.e_evtAddClick+=new EventHandler(m_ctlStorageOutCK_e_evtAddClick);
                m_ctlStorageOrdCK.strDocStar = ordTypeVo.m_strBEGINSTR_CHR;
                m_ctlStorageOrdCK.statuIN = ordTypeVo.m_intDeptType;
			}
			else
			{
				MessageBox.Show("传入的单据类型ID不正确！","icare",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}
			
			this.Show();

		}

		private void frmMedStorageOutOrd_Load(object sender, System.EventArgs e)
		{
			this.m_mthSetEnter2Tab(new System.Windows.Forms.Control[] {});
			((clsControlMedStorageOutOrd)this.objController).m_mthInit();
			if(depttype==0)
			{
				m_lsvDetail.Columns[4].Width=0;
				m_lsvDetail.Columns[8].Width=0;
				m_lsvDetail.Columns[9].Width=0;
				m_lsvDetail.Columns[10].Width=0;
				m_lsvDetail.Columns[11].Width=0;
                m_lsvDetail.Columns[13].Width = 0;
                m_lsvDetail.Columns[14].Width = 0;
				IsLongShow=true;
			}
			else
			{
				collapsibleSplitter1.Visible=false;
			}
			
		}
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_mthOkButtonClick();
		}

		private void m_cboOrdType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
			if(e.KeyCode==Keys.Escape)
			{
				if(MessageBox.Show("是否要退出药品出库系统？","系统提示",MessageBoxButtons.YesNo)==DialogResult.Yes)
				{
					this.Close();
				}
			}
			if(e.KeyCode==Keys.F1&&m_btnNew.Enabled==true)
			{
				((clsControlMedStorageOutOrd)this.objController).m_lngClearAll();
				this.m_lsvDetail.Items.Clear();
			}
			if(e.KeyCode==Keys.F2&&btnSave.Enabled==true)
			{
				((clsControlMedStorageOutOrd)this.objController).m_mthSave();
			}
			if(e.KeyCode==Keys.F3&&dntEmp.Enabled==true)
			{
				((clsControlMedStorageOutOrd)this.objController).EmpOrd();
			}
			if(e.KeyCode==Keys.F4&&btnFind.Enabled==true)
			{
				((clsControlMedStorageOutOrd)this.objController).m_ShowFind();
			}
			if(e.KeyCode==Keys.F5&&btnDelect.Enabled==true)
			{
				((clsControlMedStorageOutOrd)this.objController).m_lngDele();
			}
			if(e.KeyCode==Keys.F6)
			{
				m_lsvDetail.Focus();
				if(m_lsvDetail.Items.Count>0)
				{
					m_lsvDetail.Items[0].Selected=true;
				}
			}
			if(e.KeyCode==Keys.F7)
			{
				m_tabAduit.SelectedIndex=0;
				if(m_lsvUnAduit.Items.Count>0)
				{
					m_lsvUnAduit.Items[0].Selected=true;
					m_lsvUnAduit.Focus();
				}
			}
			if(e.KeyCode==Keys.F8)
			{
				m_tabAduit.SelectedIndex=1;

				if(m_lsvEnAduit.Items.Count>0)
				{
					m_lsvEnAduit.Items[0].Selected=true;
					m_lsvEnAduit.Focus();
				}
			}
			if(e.KeyCode==Keys.F9)
			{
				this.m_ctlStorageOrdCK.Focus();
			}
			if(e.KeyCode==Keys.F10)
			{
				this.m_ctlStorageOrdCK.Focus();
			}

		}

		private void m_cobStorage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
			if(e.KeyCode==Keys.Escape)
			{
				if(MessageBox.Show("是否要退出药品出库系统？","系统提示",MessageBoxButtons.YesNo)==DialogResult.Yes)
				{
					this.Close();
				}
			}
			if(e.KeyCode==Keys.F1&&m_btnNew.Enabled==true)
			{
				((clsControlMedStorageOutOrd)this.objController).m_lngClearAll();
				this.m_lsvDetail.Items.Clear();
			}
			if(e.KeyCode==Keys.F2&&btnSave.Enabled==true)
			{
				((clsControlMedStorageOutOrd)this.objController).m_mthSave();
			}
			if(e.KeyCode==Keys.F3&&dntEmp.Enabled==true)
			{
				((clsControlMedStorageOutOrd)this.objController).EmpOrd();
			}
			if(e.KeyCode==Keys.F4&&btnFind.Enabled==true)
			{
				((clsControlMedStorageOutOrd)this.objController).m_ShowFind();
			}
			if(e.KeyCode==Keys.F5&&btnDelect.Enabled==true)
			{
				((clsControlMedStorageOutOrd)this.objController).m_lngDele();
			}
			if(e.KeyCode==Keys.F6)
			{
				m_lsvDetail.Focus();
				if(m_lsvDetail.Items.Count>0)
				{
					m_lsvDetail.Items[0].Selected=true;
				}
			}
			if(e.KeyCode==Keys.F7)
			{
				m_tabAduit.SelectedIndex=0;
				if(m_lsvUnAduit.Items.Count>0)
				{
					m_lsvUnAduit.Items[0].Selected=true;
					m_lsvUnAduit.Focus();
				}
			}
			if(e.KeyCode==Keys.F8)
			{
				m_tabAduit.SelectedIndex=1;

				if(m_lsvEnAduit.Items.Count>0)
				{
					m_lsvEnAduit.Items[0].Selected=true;
					m_lsvEnAduit.Focus();
				}
			}
			if(e.KeyCode==Keys.F9)
			{
				this.m_ctlStorageOrdCK.Focus();
			}
			if(e.KeyCode==Keys.F10)
			{
				this.m_ctlStorageOutCK.Focus();
			}
		}


		private void btnSave_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_mthSave();
			this.m_ctlStorageOrdCK.Focus();
		}

		private void m_cboOrdType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_lngCobChang();
		}

		private void m_lsvDetail_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).MouseDown(1);
		}

		private void m_lsvUnAduit_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).MouseDown(2);
		}

		private void btnDelect_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_lngDele();
		}

		private void bnlfind_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_lngFindData();
		}

		private void bnlcolse_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_ColseFind();
		}

		private void btnFind_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_ShowFind();
			comboBox1.Focus();
		}

		private void dntEmp_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).EmpOrd();
		}

		private void frmMedStorageOutOrd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Escape)
			{
                if (!this.m_ctlStorageOutCK.m_blCloseFind())
                {
                    if (MessageBox.Show("是否要退出药品入库系统？", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
			}
			if(e.Alt)
			{
				if(e.KeyCode==Keys.L)
				{
					if(m_lsvDetail.Items.Count>0)
					{
						m_lsvDetail.Items[0].Selected=true;
						m_lsvDetail.Items[0].Focused=true;
					}
				}
				if(e.KeyCode==Keys.V)
				{
					m_tabAduit.SelectedIndex=0;
					if(m_lsvUnAduit.Items.Count>0)
					{
						m_lsvUnAduit.Items[0].Selected=true;
						m_lsvUnAduit.Items[0].Focused=true;
					}
				}

				if(e.KeyCode==Keys.O)
				{
					m_tabAduit.SelectedIndex=1;
					if(m_lsvEnAduit.Items.Count>0)
					{
						m_lsvEnAduit.Items[0].Selected=true;
						m_lsvEnAduit.Items[0].Focused=true;
					}
				}
				if(e.KeyCode==Keys.T)
				{
					comPriod.Focus();
				}
				if(e.KeyCode==Keys.M)
				{
					this.m_ctlStorageOutCK.Focus();
				}

			}
		}

		private void m_lsvUnAduit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				((clsControlMedStorageOutOrd)this.objController).m_lngLisvSelect();
			}
		}

		private void m_lsvEnAduit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				((clsControlMedStorageOutOrd)this.objController).m_lngLisvEMP();
			}
		}

		private void m_lsvDetail_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				((clsControlMedStorageOutOrd)this.objController).m_lngLisvSelectOfDe();
			}
		}

		private void m_tabAduit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_lngActivity();
		}

		private void m_btnNew_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_lngClearAll();
			this.m_lsvDetail.Items.Clear();
		}

		private void btnesc_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show("是否要退出药品出库系统？","系统提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
			this.Close();
		}

		private void m_lsvEnAduit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_lngLisvEMP();
		}

		private void m_lsvUnAduit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_lngLisvSelect();
		}
		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_mthPrint();
		}

		private void m_lsvDetail_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			m_lsvDetail.DoDragDrop(e.Item,DragDropEffects.Move);
		}

		private void m_lsvDetail_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			ListViewItem lv =e.Data.GetData(typeof(System.Windows.Forms.ListViewItem)) as ListViewItem;
			ListViewItem l =lv.Clone() as ListViewItem;
			Point p =new Point(e.X,e.Y);
			p=this.m_lsvDetail.PointToClient(p);
			ListViewItem tempItem=	this.m_lsvDetail.GetItemAt(p.X,p.Y);			
			int index  =-1;
			if(tempItem!=null)
			{
				index =tempItem.Index;
			}
			lv.Remove();
			if(index>-1)
			{
				this.m_lsvDetail.Items.Insert(index,l);
			}
			else
			{
				this.m_lsvDetail.Items.Add(l);
			}
			((clsControlMedStorageOutOrd)this.objController).isModidy=1;
		}

		private void m_lsvDetail_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(System.Windows.Forms.ListViewItem))) 
			{
				e.Effect =DragDropEffects.Move;
			}
			else
			{
				e.Effect  =  DragDropEffects.None  ;
			}
		}
		bool IsLongShow=false;
		private void collapsibleSplitter1_Click(object sender, System.EventArgs e)
		{
			if(IsLongShow==false)
			{
				m_lsvDetail.Columns[4].Width=0;
				m_lsvDetail.Columns[8].Width=0;
				m_lsvDetail.Columns[9].Width=0;
				m_lsvDetail.Columns[10].Width=0;
				m_lsvDetail.Columns[11].Width=0;
//				m_lsvDetail.Columns[13].Width=0;
//				m_lsvDetail.Columns[14].Width=0;
				IsLongShow=true;
			}
			else
			{
				m_lsvDetail.Columns[4].Width=100;
				m_lsvDetail.Columns[8].Width=90;
				m_lsvDetail.Columns[9].Width=80;
				m_lsvDetail.Columns[10].Width=90;
				m_lsvDetail.Columns[11].Width=40;
//				m_lsvDetail.Columns[13].Width=60;
//				m_lsvDetail.Columns[14].Width=80;
				IsLongShow=false;
			}
		}

		private void frmMedStorageOutOrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_mthChengRowNO();
		}

		private void m_lsvDetail_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_lngLisvSelectOfDe();
		}

		private void buttonXP2_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_lngPriodchang();
		}

		private void m_ctlStorageOrdCK_e_evtStorageChang(object sender, EventArgs e)
		{
			
			this.m_ctlStorageOutCK.strSTORAGEID=this.m_ctlStorageOrdCK.strGetStorageID;
			this.m_ctlStorageOutCK.intIsReData=1;
			try
			{
				this.m_ctlStorageOutCK.storageProfit=Double.Parse(this.m_ctlStorageOrdCK.strGetSTORAGEGROSSPROFIT)/100;
			}
			catch
			{
				this.m_ctlStorageOutCK.storageProfit=0;
			}
		}

		private void m_ctlStorageOutCK_e_evtAddClick(object sender, EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_mthOkButtonClick();
		}

		private void comPriod_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlMedStorageOutOrd)this.objController).m_lngPriodchang();
		}
	}
}
