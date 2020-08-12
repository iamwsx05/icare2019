using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;
namespace  com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// test 的摘要说明。
	/// </summary>
	public class frmMedStorageInOrd  :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		internal PinkieControls.ButtonXP dntEmp;
		internal PinkieControls.ButtonXP btnesc;
		internal PinkieControls.ButtonXP btnDelect;
		internal System.Windows.Forms.GroupBox groupBox3;
		internal PinkieControls.ButtonXP btnFind;
		internal PinkieControls.ButtonXP btnSave;
		internal PinkieControls.ButtonXP m_btnNew;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.TabPage m_tbpUnAduit;
		internal System.Windows.Forms.ListView m_lsvUnAduit;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		internal System.Windows.Forms.ListView m_lsvEnAduit;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader m_clhRowNO;
		private System.Windows.Forms.ColumnHeader m_clhMedQty;
		private System.Windows.Forms.ColumnHeader m_clhDisCount;
		private System.Windows.Forms.ColumnHeader m_clhMedLotNo;
		private System.Windows.Forms.ColumnHeader clhInvoiceNo;
		private System.Windows.Forms.ColumnHeader m_clhMedName;
		private System.Windows.Forms.ColumnHeader m_clhMedUnit;
		private System.Windows.Forms.TabPage m_tbpEnAduit;
		internal System.Windows.Forms.TabControl m_tabAduit;
		internal PinkieControls.ButtonXP bnlcolse;
		internal PinkieControls.ButtonXP bnlfind;
		private System.Windows.Forms.ColumnHeader PACKAGEQTY_DEC;
		private System.Windows.Forms.ColumnHeader PACKAGEPRICE;
		private System.Windows.Forms.ColumnHeader tolmony;
		internal System.Windows.Forms.ListView m_lsvDetail;
		private System.Windows.Forms.ColumnHeader m_clhMedID;
		private System.Windows.Forms.ColumnHeader BUYUNITPRICE;
		private System.Windows.Forms.ColumnHeader m_clhMedUsefulLife;
		private System.Windows.Forms.ColumnHeader m_clhMedProductArea;
		private System.Windows.Forms.ColumnHeader m_clhMedStoNow;
		internal System.Windows.Forms.Panel panel1;
		internal System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label28;
		internal System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader Row;
		private System.Windows.Forms.ColumnHeader MedName;
		private System.Windows.Forms.ColumnHeader Unit;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Windows.Forms.ColumnHeader columnHeader24;
		private System.Windows.Forms.ColumnHeader columnHeader25;
		private System.Windows.Forms.ColumnHeader columnHeader26;
		private System.Windows.Forms.ColumnHeader columnHeader27;
		private System.Windows.Forms.ColumnHeader columnHeader28;
		private System.Windows.Forms.ColumnHeader columnHeader29;
		private System.Windows.Forms.ColumnHeader columnHeader30;
		private System.Windows.Forms.ColumnHeader columnHeader31;
		private System.Windows.Forms.ColumnHeader columnHeader32;
		private System.Windows.Forms.ColumnHeader columnHeader33;
		private System.Windows.Forms.ColumnHeader columnHeader34;
		private System.Windows.Forms.Panel panel5;
		internal System.Windows.Forms.ComboBox m_cboSelPeriod;
		internal System.Windows.Forms.Panel panel6;
		internal System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		internal PinkieControls.ButtonXP btnPrint;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		internal System.Windows.Forms.ComboBox comboBox1;
		internal System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		internal PinkieControls.ButtonXP buttonXP1;
		private System.Windows.Forms.Panel panel4;
		private com.digitalwave.Utility.Controls.CollapsibleSplitter collapsibleSplitter1;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.ColumnHeader columnHeader35;
		private System.Windows.Forms.ColumnHeader columnHeader36;
		private System.Windows.Forms.ColumnHeader columnHeader37;
		private System.Windows.Forms.ToolTip toolTip1;
		internal System.Windows.Forms.ContextMenu contextMenu1;
		internal System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader38;
		private System.Windows.Forms.ColumnHeader columnHeader39;
		private System.Windows.Forms.ColumnHeader columnHeader40;
        private Panel panel7;
        private com.digitalwave.Utility.Controls.CollapsibleSplitter collapsibleSplitter2;
        private Panel panel8;
        private ColumnHeader columnHeader41;
		private System.ComponentModel.IContainer components;

		public frmMedStorageInOrd()
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
            this.dntEmp = new PinkieControls.ButtonXP();
            this.btnesc = new PinkieControls.ButtonXP();
            this.btnDelect = new PinkieControls.ButtonXP();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.btnPrint = new PinkieControls.ButtonXP();
            this.btnFind = new PinkieControls.ButtonXP();
            this.btnSave = new PinkieControls.ButtonXP();
            this.m_btnNew = new PinkieControls.ButtonXP();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.m_tbpUnAduit = new System.Windows.Forms.TabPage();
            this.m_lsvUnAduit = new System.Windows.Forms.ListView();
            this.columnHeader34 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader23 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader26 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader24 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader27 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader28 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader29 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.m_lsvEnAduit = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader32 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader25 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader30 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader31 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader33 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader41 = new System.Windows.Forms.ColumnHeader();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.m_clhRowNO = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedQty = new System.Windows.Forms.ColumnHeader();
            this.m_clhDisCount = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedLotNo = new System.Windows.Forms.ColumnHeader();
            this.clhInvoiceNo = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedName = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedUnit = new System.Windows.Forms.ColumnHeader();
            this.m_tbpEnAduit = new System.Windows.Forms.TabPage();
            this.m_tabAduit = new System.Windows.Forms.TabControl();
            this.bnlcolse = new PinkieControls.ButtonXP();
            this.bnlfind = new PinkieControls.ButtonXP();
            this.PACKAGEQTY_DEC = new System.Windows.Forms.ColumnHeader();
            this.PACKAGEPRICE = new System.Windows.Forms.ColumnHeader();
            this.tolmony = new System.Windows.Forms.ColumnHeader();
            this.m_lsvDetail = new System.Windows.Forms.ListView();
            this.Row = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedID = new System.Windows.Forms.ColumnHeader();
            this.MedName = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedProductArea = new System.Windows.Forms.ColumnHeader();
            this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.BUYUNITPRICE = new System.Windows.Forms.ColumnHeader();
            this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
            this.Unit = new System.Windows.Forms.ColumnHeader();
            this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader35 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader36 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader37 = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedStoNow = new System.Windows.Forms.ColumnHeader();
            this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedUsefulLife = new System.Windows.Forms.ColumnHeader();
            this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.m_cboSelPeriod = new System.Windows.Forms.ComboBox();
            this.collapsibleSplitter2 = new com.digitalwave.Utility.Controls.CollapsibleSplitter();
            this.panel6 = new System.Windows.Forms.Panel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.collapsibleSplitter1 = new com.digitalwave.Utility.Controls.CollapsibleSplitter();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader38 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader39 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader40 = new System.Windows.Forms.ColumnHeader();
            this.groupBox3.SuspendLayout();
            this.m_tbpUnAduit.SuspendLayout();
            this.m_tbpEnAduit.SuspendLayout();
            this.m_tabAduit.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dntEmp
            // 
            this.dntEmp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dntEmp.DefaultScheme = true;
            this.dntEmp.DialogResult = System.Windows.Forms.DialogResult.None;
            this.dntEmp.Hint = "";
            this.dntEmp.Location = new System.Drawing.Point(186, 16);
            this.dntEmp.Name = "dntEmp";
            this.dntEmp.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.dntEmp.Size = new System.Drawing.Size(72, 24);
            this.dntEmp.TabIndex = 51;
            this.dntEmp.TabStop = false;
            this.dntEmp.Text = "审核(&O)";
            this.dntEmp.Click += new System.EventHandler(this.dntEmp_Click);
            // 
            // btnesc
            // 
            this.btnesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnesc.DefaultScheme = true;
            this.btnesc.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnesc.Hint = "";
            this.btnesc.Location = new System.Drawing.Point(571, 16);
            this.btnesc.Name = "btnesc";
            this.btnesc.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnesc.Size = new System.Drawing.Size(80, 24);
            this.btnesc.TabIndex = 56;
            this.btnesc.TabStop = false;
            this.btnesc.Text = "退出(ESE)";
            this.btnesc.Click += new System.EventHandler(this.btnesc_Click);
            // 
            // btnDelect
            // 
            this.btnDelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnDelect.DefaultScheme = true;
            this.btnDelect.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDelect.Hint = "";
            this.btnDelect.Location = new System.Drawing.Point(417, 16);
            this.btnDelect.Name = "btnDelect";
            this.btnDelect.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnDelect.Size = new System.Drawing.Size(72, 24);
            this.btnDelect.TabIndex = 54;
            this.btnDelect.TabStop = false;
            this.btnDelect.Text = "删除(&D)";
            this.btnDelect.Click += new System.EventHandler(this.btnDelect_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.buttonXP1);
            this.groupBox3.Controls.Add(this.btnPrint);
            this.groupBox3.Controls.Add(this.dntEmp);
            this.groupBox3.Controls.Add(this.btnesc);
            this.groupBox3.Controls.Add(this.btnDelect);
            this.groupBox3.Controls.Add(this.btnFind);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.m_btnNew);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(264, 526);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(688, 48);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            // 
            // buttonXP1
            // 
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(494, 16);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(72, 24);
            this.buttonXP1.TabIndex = 58;
            this.buttonXP1.TabStop = false;
            this.buttonXP1.Text = "刷新(&M)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnPrint.DefaultScheme = true;
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPrint.Enabled = false;
            this.btnPrint.Hint = "";
            this.btnPrint.Location = new System.Drawing.Point(263, 16);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnPrint.Size = new System.Drawing.Size(72, 24);
            this.btnPrint.TabIndex = 57;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "打印(&P)";
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnFind.DefaultScheme = true;
            this.btnFind.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFind.Hint = "";
            this.btnFind.Location = new System.Drawing.Point(340, 16);
            this.btnFind.Name = "btnFind";
            this.btnFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnFind.Size = new System.Drawing.Size(72, 24);
            this.btnFind.TabIndex = 53;
            this.btnFind.TabStop = false;
            this.btnFind.Text = "查找(&F)";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnSave.DefaultScheme = true;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.Hint = "";
            this.btnSave.Location = new System.Drawing.Point(109, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnSave.Size = new System.Drawing.Size(72, 24);
            this.btnSave.TabIndex = 49;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // m_btnNew
            // 
            this.m_btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_btnNew.DefaultScheme = true;
            this.m_btnNew.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnNew.Hint = "";
            this.m_btnNew.Location = new System.Drawing.Point(32, 16);
            this.m_btnNew.Name = "m_btnNew";
            this.m_btnNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnNew.Size = new System.Drawing.Size(72, 24);
            this.m_btnNew.TabIndex = 50;
            this.m_btnNew.TabStop = false;
            this.m_btnNew.Text = "新建(&N)";
            this.m_btnNew.Click += new System.EventHandler(this.m_btnNew_Click);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "单据类型";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "创建时间";
            this.columnHeader6.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "单据号";
            this.columnHeader5.Width = 80;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "单据号";
            this.columnHeader8.Width = 80;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "创建时间";
            this.columnHeader10.Width = 120;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "单据类型";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 100;
            // 
            // m_tbpUnAduit
            // 
            this.m_tbpUnAduit.Controls.Add(this.m_lsvUnAduit);
            this.m_tbpUnAduit.Location = new System.Drawing.Point(4, 4);
            this.m_tbpUnAduit.Name = "m_tbpUnAduit";
            this.m_tbpUnAduit.Size = new System.Drawing.Size(252, 187);
            this.m_tbpUnAduit.TabIndex = 0;
            this.m_tbpUnAduit.Text = "未审核";
            // 
            // m_lsvUnAduit
            // 
            this.m_lsvUnAduit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader34,
            this.columnHeader23,
            this.columnHeader26,
            this.columnHeader24,
            this.columnHeader27,
            this.columnHeader28,
            this.columnHeader29});
            this.m_lsvUnAduit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvUnAduit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvUnAduit.FullRowSelect = true;
            this.m_lsvUnAduit.GridLines = true;
            this.m_lsvUnAduit.HideSelection = false;
            this.m_lsvUnAduit.Location = new System.Drawing.Point(0, 0);
            this.m_lsvUnAduit.MultiSelect = false;
            this.m_lsvUnAduit.Name = "m_lsvUnAduit";
            this.m_lsvUnAduit.Size = new System.Drawing.Size(252, 187);
            this.m_lsvUnAduit.TabIndex = 0;
            this.m_lsvUnAduit.TabStop = false;
            this.m_lsvUnAduit.UseCompatibleStateImageBehavior = false;
            this.m_lsvUnAduit.View = System.Windows.Forms.View.Details;
            this.m_lsvUnAduit.SelectedIndexChanged += new System.EventHandler(this.m_lsvUnAduit_SelectedIndexChanged_1);
            this.m_lsvUnAduit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_lsvUnAduit_MouseDown);
            this.m_lsvUnAduit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvUnAduit_KeyDown);
            // 
            // columnHeader34
            // 
            this.columnHeader34.Text = "单据号";
            this.columnHeader34.Width = 100;
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "单据类型";
            this.columnHeader23.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader23.Width = 0;
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "创建人";
            this.columnHeader26.Width = 70;
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "创建时间";
            this.columnHeader24.Width = 0;
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "审核人";
            this.columnHeader27.Width = 0;
            // 
            // columnHeader28
            // 
            this.columnHeader28.Text = "供应商";
            this.columnHeader28.Width = 150;
            // 
            // columnHeader29
            // 
            this.columnHeader29.Text = "总金额";
            this.columnHeader29.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader29.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "创建时间";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "单据类型";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "单据号";
            this.columnHeader1.Width = 110;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "创建时间";
            this.columnHeader12.Width = 120;
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
            this.columnHeader33,
            this.columnHeader41});
            this.m_lsvEnAduit.ContextMenu = this.contextMenu1;
            this.m_lsvEnAduit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvEnAduit.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_lsvEnAduit.FullRowSelect = true;
            this.m_lsvEnAduit.GridLines = true;
            this.m_lsvEnAduit.Location = new System.Drawing.Point(0, 0);
            this.m_lsvEnAduit.MultiSelect = false;
            this.m_lsvEnAduit.Name = "m_lsvEnAduit";
            this.m_lsvEnAduit.Size = new System.Drawing.Size(252, 187);
            this.m_lsvEnAduit.TabIndex = 0;
            this.m_lsvEnAduit.TabStop = false;
            this.m_lsvEnAduit.UseCompatibleStateImageBehavior = false;
            this.m_lsvEnAduit.View = System.Windows.Forms.View.Details;
            this.m_lsvEnAduit.SelectedIndexChanged += new System.EventHandler(this.m_lsvEnAduit_SelectedIndexChanged_1);
            this.m_lsvEnAduit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvEnAduit_KeyDown);
            this.m_lsvEnAduit.Click += new System.EventHandler(this.m_lsvEnAduit_Click);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "单据号";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "单据类型";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader11.Width = 0;
            // 
            // columnHeader32
            // 
            this.columnHeader32.Text = "创建人";
            this.columnHeader32.Width = 70;
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "创建日期";
            this.columnHeader25.Width = 0;
            // 
            // columnHeader30
            // 
            this.columnHeader30.Text = "审核人";
            this.columnHeader30.Width = 70;
            // 
            // columnHeader31
            // 
            this.columnHeader31.Text = "供应商";
            this.columnHeader31.Width = 150;
            // 
            // columnHeader33
            // 
            this.columnHeader33.Text = "总金额";
            this.columnHeader33.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader33.Width = 80;
            // 
            // columnHeader41
            // 
            this.columnHeader41.Text = "审核时间";
            this.columnHeader41.Width = 150;
            // 
            // m_clhRowNO
            // 
            this.m_clhRowNO.Text = "行号";
            // 
            // m_clhMedQty
            // 
            this.m_clhMedQty.Text = "数量";
            this.m_clhMedQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // m_clhDisCount
            // 
            this.m_clhDisCount.Text = "折扣";
            // 
            // m_clhMedLotNo
            // 
            this.m_clhMedLotNo.Text = "批号";
            this.m_clhMedLotNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_clhMedLotNo.Width = 100;
            // 
            // clhInvoiceNo
            // 
            this.clhInvoiceNo.Text = "发票号";
            this.clhInvoiceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clhInvoiceNo.Width = 80;
            // 
            // m_clhMedName
            // 
            this.m_clhMedName.Text = "名称";
            this.m_clhMedName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_clhMedName.Width = 100;
            // 
            // m_clhMedUnit
            // 
            this.m_clhMedUnit.Text = "单位";
            this.m_clhMedUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // m_tbpEnAduit
            // 
            this.m_tbpEnAduit.Controls.Add(this.m_lsvEnAduit);
            this.m_tbpEnAduit.Location = new System.Drawing.Point(4, 4);
            this.m_tbpEnAduit.Name = "m_tbpEnAduit";
            this.m_tbpEnAduit.Size = new System.Drawing.Size(252, 187);
            this.m_tbpEnAduit.TabIndex = 1;
            this.m_tbpEnAduit.Text = "已审核";
            this.m_tbpEnAduit.Visible = false;
            // 
            // m_tabAduit
            // 
            this.m_tabAduit.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.m_tabAduit.Controls.Add(this.m_tbpUnAduit);
            this.m_tabAduit.Controls.Add(this.m_tbpEnAduit);
            this.m_tabAduit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabAduit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_tabAduit.ItemSize = new System.Drawing.Size(0, 17);
            this.m_tabAduit.Location = new System.Drawing.Point(0, 0);
            this.m_tabAduit.Multiline = true;
            this.m_tabAduit.Name = "m_tabAduit";
            this.m_tabAduit.SelectedIndex = 0;
            this.m_tabAduit.Size = new System.Drawing.Size(260, 212);
            this.m_tabAduit.TabIndex = 59;
            this.m_tabAduit.TabStop = false;
            this.m_tabAduit.SelectedIndexChanged += new System.EventHandler(this.m_tabAduit_SelectedIndexChanged);
            // 
            // bnlcolse
            // 
            this.bnlcolse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bnlcolse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bnlcolse.DefaultScheme = true;
            this.bnlcolse.DialogResult = System.Windows.Forms.DialogResult.None;
            this.bnlcolse.Hint = "";
            this.bnlcolse.Location = new System.Drawing.Point(157, 61);
            this.bnlcolse.Name = "bnlcolse";
            this.bnlcolse.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.bnlcolse.Size = new System.Drawing.Size(80, 24);
            this.bnlcolse.TabIndex = 143;
            this.bnlcolse.Text = "返回(&R)";
            this.bnlcolse.Click += new System.EventHandler(this.bnlcolse_Click_1);
            // 
            // bnlfind
            // 
            this.bnlfind.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bnlfind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bnlfind.DefaultScheme = true;
            this.bnlfind.DialogResult = System.Windows.Forms.DialogResult.None;
            this.bnlfind.Hint = "";
            this.bnlfind.Location = new System.Drawing.Point(65, 61);
            this.bnlfind.Name = "bnlfind";
            this.bnlfind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.bnlfind.Size = new System.Drawing.Size(72, 24);
            this.bnlfind.TabIndex = 142;
            this.bnlfind.Text = "查找(&K)";
            this.bnlfind.Click += new System.EventHandler(this.bnlfind_Click);
            // 
            // PACKAGEQTY_DEC
            // 
            this.PACKAGEQTY_DEC.Text = "包装数量";
            this.PACKAGEQTY_DEC.Width = 87;
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
            // m_lsvDetail
            // 
            this.m_lsvDetail.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.m_lsvDetail.AllowDrop = true;
            this.m_lsvDetail.AutoArrange = false;
            this.m_lsvDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Row,
            this.m_clhMedID,
            this.MedName,
            this.columnHeader14,
            this.m_clhMedProductArea,
            this.columnHeader16,
            this.columnHeader18,
            this.columnHeader17,
            this.BUYUNITPRICE,
            this.columnHeader20,
            this.Unit,
            this.columnHeader22,
            this.columnHeader35,
            this.columnHeader36,
            this.columnHeader37,
            this.m_clhMedStoNow,
            this.columnHeader19,
            this.columnHeader15,
            this.m_clhMedUsefulLife,
            this.columnHeader21,
            this.columnHeader13});
            this.m_lsvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvDetail.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvDetail.FullRowSelect = true;
            this.m_lsvDetail.GridLines = true;
            this.m_lsvDetail.HideSelection = false;
            this.m_lsvDetail.Location = new System.Drawing.Point(0, 0);
            this.m_lsvDetail.MultiSelect = false;
            this.m_lsvDetail.Name = "m_lsvDetail";
            this.m_lsvDetail.Size = new System.Drawing.Size(952, 264);
            this.m_lsvDetail.TabIndex = 7;
            this.m_lsvDetail.TabStop = false;
            this.m_lsvDetail.UseCompatibleStateImageBehavior = false;
            this.m_lsvDetail.View = System.Windows.Forms.View.Details;
            this.m_lsvDetail.SelectedIndexChanged += new System.EventHandler(this.m_lsvDetail_SelectedIndexChanged);
            this.m_lsvDetail.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_lsvDetail_DragDrop_1);
            this.m_lsvDetail.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_lsvDetail_DragEnter_1);
            this.m_lsvDetail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvDetail_KeyDown);
            this.m_lsvDetail.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_lsvDetail_ItemDrag);
            // 
            // Row
            // 
            this.Row.Text = "行号";
            this.Row.Width = 0;
            // 
            // m_clhMedID
            // 
            this.m_clhMedID.Text = "药品编码";
            this.m_clhMedID.Width = 100;
            // 
            // MedName
            // 
            this.MedName.Text = "药品名称";
            this.MedName.Width = 180;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "药品规格";
            this.columnHeader14.Width = 150;
            // 
            // m_clhMedProductArea
            // 
            this.m_clhMedProductArea.Text = "生产厂家";
            this.m_clhMedProductArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_clhMedProductArea.Width = 144;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = " 入单数量";
            this.columnHeader16.Width = 80;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "入单单位";
            this.columnHeader18.Width = 80;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "入单单价";
            this.columnHeader17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader17.Width = 80;
            // 
            // BUYUNITPRICE
            // 
            this.BUYUNITPRICE.Text = "入库单价";
            this.BUYUNITPRICE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.BUYUNITPRICE.Width = 72;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "数量";
            // 
            // Unit
            // 
            this.Unit.Text = "单位";
            this.Unit.Width = 40;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "中标零价";
            this.columnHeader22.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader22.Width = 70;
            // 
            // columnHeader35
            // 
            this.columnHeader35.Text = "国家限价";
            this.columnHeader35.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader35.Width = 70;
            // 
            // columnHeader36
            // 
            this.columnHeader36.Text = "批发价";
            this.columnHeader36.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader37
            // 
            this.columnHeader37.Text = "零售价";
            this.columnHeader37.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_clhMedStoNow
            // 
            this.m_clhMedStoNow.Text = "现库存";
            this.m_clhMedStoNow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "入单包装量";
            this.columnHeader19.Width = 90;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "入库金额";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader15.Width = 69;
            // 
            // m_clhMedUsefulLife
            // 
            this.m_clhMedUsefulLife.Text = "失效日期";
            this.m_clhMedUsefulLife.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_clhMedUsefulLife.Width = 96;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "药品批号";
            this.columnHeader21.Width = 87;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "发票号";
            this.columnHeader13.Width = 0;
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
            this.panel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 220);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 88);
            this.panel1.TabIndex = 133;
            this.panel1.Visible = false;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.Location = new System.Drawing.Point(14, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 16);
            this.label13.TabIndex = 147;
            this.label13.Text = "查找内容";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.Location = new System.Drawing.Point(14, 2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 16);
            this.label12.TabIndex = 146;
            this.label12.Text = "查找方式";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Location = new System.Drawing.Point(83, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(165, 23);
            this.textBox1.TabIndex = 145;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox1.Items.AddRange(new object[] {
            "单据号",
            "创建人",
            "创建时间",
            "供应商",
            "采购员"});
            this.comboBox1.Location = new System.Drawing.Point(83, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(165, 22);
            this.comboBox1.TabIndex = 144;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label28);
            this.panel2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(0, 120);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(688, 144);
            this.panel2.TabIndex = 1;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(163, -64);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(0, 14);
            this.label28.TabIndex = 122;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(688, 112);
            this.panel3.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.collapsibleSplitter2);
            this.panel5.Controls.Add(this.panel1);
            this.panel5.Location = new System.Drawing.Point(0, 264);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(264, 312);
            this.panel5.TabIndex = 137;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Controls.Add(this.m_tabAduit);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(260, 212);
            this.panel7.TabIndex = 2;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.Controls.Add(this.m_cboSelPeriod);
            this.panel8.Location = new System.Drawing.Point(150, 192);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(108, 22);
            this.panel8.TabIndex = 61;
            // 
            // m_cboSelPeriod
            // 
            this.m_cboSelPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cboSelPeriod.Location = new System.Drawing.Point(0, 0);
            this.m_cboSelPeriod.Name = "m_cboSelPeriod";
            this.m_cboSelPeriod.Size = new System.Drawing.Size(108, 22);
            this.m_cboSelPeriod.TabIndex = 60;
            this.m_cboSelPeriod.SelectedIndexChanged += new System.EventHandler(this.m_cboSelPeriod_SelectedIndexChanged);
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
            this.collapsibleSplitter2.Location = new System.Drawing.Point(0, 212);
            this.collapsibleSplitter2.Name = "collapsibleSplitter2";
            this.collapsibleSplitter2.Size = new System.Drawing.Size(260, 8);
            this.collapsibleSplitter2.TabIndex = 134;
            this.collapsibleSplitter2.TabStop = false;
            this.toolTip1.SetToolTip(this.collapsibleSplitter2, "全部/精简显示");
            this.collapsibleSplitter2.UseAnimations = false;
            this.collapsibleSplitter2.VisualStyle = com.digitalwave.Utility.Controls.VisualStyles.Mozilla;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.Controls.Add(this.panel3);
            this.panel6.Controls.Add(this.panel2);
            this.panel6.Location = new System.Drawing.Point(264, 264);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(688, 264);
            this.panel6.TabIndex = 45;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.collapsibleSplitter1);
            this.panel4.Controls.Add(this.m_lsvDetail);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(952, 264);
            this.panel4.TabIndex = 304;
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
            this.collapsibleSplitter1.Location = new System.Drawing.Point(944, 0);
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.Size = new System.Drawing.Size(8, 264);
            this.collapsibleSplitter1.TabIndex = 0;
            this.collapsibleSplitter1.TabStop = false;
            this.toolTip1.SetToolTip(this.collapsibleSplitter1, "全部/精简显示");
            this.collapsibleSplitter1.UseAnimations = false;
            this.collapsibleSplitter1.VisualStyle = com.digitalwave.Utility.Controls.VisualStyles.Mozilla;
            this.collapsibleSplitter1.Click += new System.EventHandler(this.collapsibleSplitter1_Click);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader38,
            this.columnHeader39,
            this.columnHeader40});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(296, 28);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(384, 192);
            this.listView1.TabIndex = 305;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Visible = false;
            // 
            // columnHeader38
            // 
            this.columnHeader38.Text = "药品名称";
            this.columnHeader38.Width = 198;
            // 
            // columnHeader39
            // 
            this.columnHeader39.Text = "入单单价";
            this.columnHeader39.Width = 95;
            // 
            // columnHeader40
            // 
            this.columnHeader40.Text = "零售价";
            // 
            // frmMedStorageInOrd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(952, 573);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Name = "frmMedStorageInOrd";
            this.Text = "药品入库";
            this.Load += new System.EventHandler(this.test_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMedStorageInOrd_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMedStorageInOrd_KeyDown);
            this.groupBox3.ResumeLayout(false);
            this.m_tbpUnAduit.ResumeLayout(false);
            this.m_tbpEnAduit.ResumeLayout(false);
            this.m_tabAduit.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		public override void CreateController()
		{
			// TODO:  添加 frmStorageInOrd.CreateController 实现
			this.objController = new com.digitalwave.iCare.gui.HIS.clsControlMedStorageInOrd();
			this.objController.Set_GUI_Apperance(this);
		}
		int intDept1;
		/// <summary>
		/// 部门类型，1－院内，0－院外
		/// </summary>
		public int intDept
		{
			set
			{
				intDept1=value;
			}
			get
			{
				return intDept1;
			}
		}
		public com.digitalwave.controls.Control.ctlStorageOrdBase m_ctlInMedOrd=null;
		public com.digitalwave.controls.Control.ctlStorageInOutMedBase m_ctlInMed=null;
		public void sendOrdType(string ordTypeID)
		{
			long lngRes;
			clsDomainControlStorageOrd objManage = new clsDomainControlStorageOrd();
            clsStorageOrdType_VO ordTypeVo = new clsStorageOrdType_VO();
            lngRes = objManage.m_lngFindOrdTypeNameByID(ordTypeID, out ordTypeVo,"");
            if (lngRes == 1 && ordTypeVo.m_strStorageOrdTypeName!= "")
			{
                this.Text = ordTypeVo.m_strStorageOrdTypeName;
                this.Name = ordTypeVo.m_strStorageOrdTypeName;
                intDept = ordTypeVo.m_intDeptType;
                if (ordTypeVo.m_intDeptType == 0)
				{
                   
					this.panel6.Location = new System.Drawing.Point(264, 238);
					this.panel6.Size = new System.Drawing.Size(688, 288);
					this.panel4.Size = new System.Drawing.Size(952, 238);
					this.panel5.Location = new System.Drawing.Point(0, 238);
					this.panel5.Size = new System.Drawing.Size(264, 336);
					this.panel1.Location = new System.Drawing.Point(264, 200);
					this.panel3.Location = new System.Drawing.Point(0, 0);
					this.panel3.Size = new System.Drawing.Size(688, 112);
					this.panel2.Location = new System.Drawing.Point(0, 120);
					this.panel2.Size = new System.Drawing.Size(688, 168);
					this.m_cboSelPeriod.Location = new System.Drawing.Point(128, 313);
					this.m_lsvUnAduit.Location=new System.Drawing.Point(0, 0);
					this.m_lsvUnAduit.Size = new System.Drawing.Size(256, 312);
					this.m_lsvEnAduit.Location=new System.Drawing.Point(0, 0);
					this.m_lsvEnAduit.Size = new System.Drawing.Size(256, 312);
					m_ctlInMedOrd=new com.digitalwave.controls.Control.ctlStorageOrdRK();
					m_ctlInMed=new com.digitalwave.controls.Control.ctlStorageInMedRK();
					m_ctlInMed.e_evtWarning+=new EventHandler(m_ctlInMedRK_e_evtWarning);
					m_ctlInMed.e_evtAddClick+=new EventHandler(m_ctlInMedRK_e_evtAddClick);
					m_ctlInMedOrd.blIsInside=false;
                    m_ctlInMedOrd.intFlag = 1;
				}
				else
				{
                    
					this.m_lsvUnAduit.Columns[5].Text="退药药房";
					this.m_lsvEnAduit.Columns[5].Text="退药药房";
					this.m_lsvDetail.Columns[5].Width=0;
					this.m_lsvDetail.Columns[6].Width=0;
					this.m_lsvDetail.Columns[7].Width=0;
					this.m_lsvDetail.Columns[8].Width=0;
					m_ctlInMedOrd=new com.digitalwave.controls.Control.ctlStorageOrdTK();
                    if (((clsControlMedStorageInOrd)this.objController).m_objComInfo.m_lonGetModuleInfo("5000") == "0")
                    {
                        this.panel6.Location = new System.Drawing.Point(264, 238);
                        this.panel6.Size = new System.Drawing.Size(688, 288);
                        this.panel4.Size = new System.Drawing.Size(952, 238);
                        this.panel5.Location = new System.Drawing.Point(0, 238);
                        this.panel5.Size = new System.Drawing.Size(264, 336);
                        this.panel1.Location = new System.Drawing.Point(264, 200);
                        this.panel3.Location = new System.Drawing.Point(0, 0);
                        this.panel3.Size = new System.Drawing.Size(688, 112);
                        this.panel2.Location = new System.Drawing.Point(0, 120);
                        this.panel2.Size = new System.Drawing.Size(688, 168);
                        this.m_cboSelPeriod.Location = new System.Drawing.Point(128, 313);
                        this.m_lsvUnAduit.Location = new System.Drawing.Point(0, 0);
                        this.m_lsvUnAduit.Size = new System.Drawing.Size(256, 312);
                        this.m_lsvEnAduit.Location = new System.Drawing.Point(0, 0);
                        this.m_lsvEnAduit.Size = new System.Drawing.Size(256, 312);
                        m_ctlInMed = new com.digitalwave.controls.Control.ctlStorageInMedRK();
                        m_ctlInMed.e_evtWarning += new EventHandler(m_ctlInMedRK_e_evtWarning);
                        m_ctlInMed.e_evtAddClick += new EventHandler(m_ctlInMedRK_e_evtAddClick);
                        m_ctlInMedOrd.blIsInside = false;
                    }
                    else
                    {
                        m_ctlInMed = new com.digitalwave.controls.Control.ctlStorageInMedTK();
                        this.m_ctlInMed.e_evtWarning += new EventHandler(m_ctlInMedTK_e_evtWarning);
                        this.m_ctlInMed.e_evtAddClick += new EventHandler(m_ctlInMedTK_e_evtAddClick);
                    }
                    m_ctlInMedOrd.intFlag = 3;
                    m_ctlInMedOrd.e_evtStorageSelect += new EventHandler(m_ctlInMedOrd_e_evtStorageSelect);
                    m_ctlInMedOrd.blIsInside = true;

				}
			}
			else
			{
				MessageBox.Show("传入的单据类型ID不正确！","icare",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}
			this.m_ctlInMed.FindMedmode=0;
			this.panel2.Controls.Add(m_ctlInMed);
			m_ctlInMed.Dock=DockStyle.Fill;
			this.panel3.Controls.Add(m_ctlInMedOrd);
			m_ctlInMedOrd.Dock=DockStyle.Fill;
			m_ctlInMedOrd.e_evtStorageChang+=new EventHandler(m_ctlInMedOrd_e_evtStorageChang);
			m_ctlInMedOrd.NextControl =m_ctlInMed.m_txtMedName;
			m_ctlInMedOrd.strGetOrdTypeID=ordTypeID;
            m_ctlInMedOrd.strGetOrdType = ordTypeVo.m_strStorageOrdTypeName;
			m_ctlInMedOrd.strGetLoginName=this.LoginInfo.m_strEmpName;
			m_ctlInMedOrd.strGetLoginID=this.LoginInfo.m_strEmpID;
            m_ctlInMedOrd.strDocStar = ordTypeVo.m_strBEGINSTR_CHR;
            m_ctlInMedOrd.statuIN = ordTypeVo.m_intDeptType;
			this.Show();
		}

		private void frmMedStorageInOrd_Load(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_mthInit();
		}


		private void DgMedicine_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
		{

		}

		private void m_txtDoc1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{

			}
		}

		private void m_txtDoc1_DoubleClick(object sender, System.EventArgs e)
		{
		}

		private void DgMedicine_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
		{
			
		}

		private void DgMedicine_m_evtDataGridKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		}
		StatusBar MdiBar=new StatusBar();
		private void test_Load(object sender, System.EventArgs e)
		{
			m_lsvDetail.Columns[4].Width=0;
			m_lsvDetail.Columns[8].Width=0;

			m_lsvDetail.Columns[9].Width=0;
			m_lsvDetail.Columns[10].Width=0;
			m_lsvDetail.Columns[11].Width=0;
			m_lsvDetail.Columns[12].Width=0;
			m_lsvDetail.Columns[15].Width=0;
			m_lsvDetail.Columns[18].Width=0;
			m_lsvDetail.Columns[19].Width=0;
			IsLongShow=true;
            System.Windows.Forms.Form MdiForms = new Form();
            if (this.MdiParent != null)
            {
                MdiForms = this.MdiParent;
                foreach (System.Windows.Forms.Control c in MdiForms.Controls)
                {
                    if (c is StatusBar)
                    {
                        MdiBar = (StatusBar)c;
                    }
                }
                ((clsControlMedStorageInOrd)this.objController).m_mthInit();
                this.m_mthSetEnter2Tab(new System.Windows.Forms.Control[] { });
                MdiBar.Panels[3].Text = "当前状态：新建单据！";
            }
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_mthOkButtonClick();
		}


		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
			((clsControlMedStorageInOrd)this.objController).m_mthSave();
			this.m_ctlInMedOrd.m_inOrdDate.Focus();
		}

		private void m_lsvUnAduit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
              ((clsControlMedStorageInOrd)this.objController).m_lngLisvSelect();
			}
		}

		private void m_lsvDetail_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				((clsControlMedStorageInOrd)this.objController).m_lngLisvSelectOfDe();
			}
		}

		private void btnDelect_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_lngDele();
		}

		private void m_lsvDetail_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).MouseDown(1);
		}

		private void m_lsvUnAduit_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
		   ((clsControlMedStorageInOrd)this.objController).MouseDown(2);
		}

		private void btnesc_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show("是否要退出药品入库系统？","Icare",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
			this.Close();
		}

		private void m_btnNew_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_lngFrmReset();
			MdiBar.Panels[3].Text="当前状态：新建单据！";
			this.m_ctlInMedOrd.m_inOrdDate.Focus();
		}

		private void bnlfind1_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_lngFindData();
		}

		private void btnFind1_Click(object sender, System.EventArgs e)
		{
		}

		private void bnlcolse_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_ColseFind();
		}

		private void dntEmp_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show("审核后数据将不可以再修改，是否要继续？","Icare",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.Yes)
			{
				((clsControlMedStorageInOrd)this.objController).EmpOrd();	
			}
			
		}

		private void m_lsvEnAduit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				((clsControlMedStorageInOrd)this.objController).m_lngLisvEMP();
			}
		}

		private void m_tabAduit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_lngActivity();
			if(this.m_tabAduit.SelectedIndex==1)
			{
				MdiBar.Panels[3].Text="当前状态：已审核的单据不能做任何操作！";
			}
			else
			{
				if(m_lsvUnAduit.SelectedItems.Count>0)
				{
					MdiBar.Panels[3].Text="当前状态：[修改]修改入库单，[增加]向入库单添加药品明细！";
				}
				else
				{
					MdiBar.Panels[3].Text="当前状态：新建单据！";
				}
			}
		}
		private void btnAdd_Click_1(object sender, System.EventArgs e)
		{
		   ((clsControlMedStorageInOrd)this.objController).m_mthOkButtonClick();
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
		}

		private void btnFind_Click(object sender, System.EventArgs e)
		{
			panel1.Visible=true;
		}

		private void bnlfind_Click(object sender, System.EventArgs e)
		{
		    ((clsControlMedStorageInOrd)this.objController).m_lngFindData();
		}

		private void bnlcolse_Click_1(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_ColseFind();
		}
        public int intSeleteOrd = 0;
		private void m_lsvUnAduit_SelectedIndexChanged_1(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_lngLisvSelect();
            if (m_lsvUnAduit.SelectedItems.Count > 0)
            {
                intSeleteOrd = m_lsvUnAduit.SelectedItems[0].Index;
            }
			MdiBar.Panels[3].Text="当前状态：[修改]修改入库单，[增加]向入库单添加药品明细！";
			
		}

		private void frmMedStorageInOrd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.F1)
			{
				collapsibleSplitter1_Click(null,null);
			}
			if(e.KeyCode==Keys.F2)
			{
				((clsControlMedStorageInOrd)this.objController).m_mthShow();
			}
			
			if(e.KeyCode==Keys.Escape)
			{
                if (!m_ctlInMed.m_blCloseFind())
                {
                    if (listView1.Visible == true)
                    {
                        listView1.Visible = false;
                        return;
                    }
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
					m_cboSelPeriod.Focus();
				}
				if(e.KeyCode==Keys.M)
				{
//					m_txtMedName.Focus();
				}

			}
		}

		private void m_txtMemo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				 m_ctlInMed.Focus();
			}

		}
		private void m_cboSelPeriod_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_lngPriodchang();
			this.m_ctlInMedOrd.m_inOrdDate.Focus();

		}

		private void m_lsvEnAduit_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_lngLisvEMP();
		}
		private void m_lsvEnAduit_SelectedIndexChanged_1(object sender, System.EventArgs e)
		{
		   ((clsControlMedStorageInOrd)this.objController).m_lngLisvEMP();
		}
		clsPublicParm publicClass=new clsPublicParm();
		private void m_txtORDERUNITPRICE_Leave(object sender, System.EventArgs e)
		{
			if(((clsControlMedStorageInOrd)this.objController).intNewOrUpdate ==0)
			this.ImeMode=ImeMode.On;
		
		}

		private void txtDoc_TextChanged(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_mthIsSave();

		}

		private void m_txtORDERQTY_ImeModeChanged(object sender, System.EventArgs e)
		{
			this.ImeMode=ImeMode.Disable;
		}
		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_mthResetData();
		}

		private void m_lsvDetail_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			this.Cursor=Cursors.Hand;
		}

		private void m_lsvDetail_GiveFeedback(object sender, System.Windows.Forms.GiveFeedbackEventArgs e)
		{
			this.Cursor=Cursors.Hand;
		}

		private void m_lsvDetail_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			this.Cursor=Cursors.Hand;
		}

		private void textBoxTypedNumeric2_Leave(object sender, System.EventArgs e)
		{
			if(((clsControlMedStorageInOrd)this.objController).intNewOrUpdate==0)
			{
			}
			this.ImeMode=ImeMode.On;
		}

		private void frmMedStorageInOrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            if(MdiBar!=null)
			    MdiBar.Panels[3].Text="";
			((clsControlMedStorageInOrd)this.objController).m_mthChengRowNO();
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
				m_lsvDetail.Columns[12].Width=0;
				m_lsvDetail.Columns[15].Width=0;
				m_lsvDetail.Columns[18].Width=0;
				m_lsvDetail.Columns[19].Width=0;
				IsLongShow=true;
			}
			else
			{
				m_lsvDetail.Columns[4].Width=0;
				m_lsvDetail.Columns[8].Width=72;
				m_lsvDetail.Columns[9].Width=60;
				m_lsvDetail.Columns[10].Width=40;
				m_lsvDetail.Columns[11].Width=70;
				m_lsvDetail.Columns[12].Width=70;
				m_lsvDetail.Columns[15].Width=60;
				m_lsvDetail.Columns[18].Width=96;
				m_lsvDetail.Columns[19].Width=87;
				IsLongShow=false;
			}
		}

		private void radioButton1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}
		private void m_lsvDetail_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			m_lsvDetail.DoDragDrop(e.Item,DragDropEffects.Move);
		}

		private void m_lsvDetail_DragEnter_1(object sender, System.Windows.Forms.DragEventArgs e)
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

		private void m_lsvDetail_DragDrop_1(object sender, System.Windows.Forms.DragEventArgs e)
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
			((clsControlMedStorageInOrd)this.objController).isModidy=1;
		}
        public int intSeleteDe = 0;
		private void m_lsvDetail_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_lngLisvSelectOfDe();
            if (m_lsvDetail.SelectedItems.Count > 0)
            {
                intSeleteDe = m_lsvDetail.SelectedItems[0].Index;
            }
			this.m_lsvDetail.Focus();
			MdiBar.Panels[3].Text="当前状态：[修改]修改药品明细、入库单！";
		}

		private void txtDocEnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}
		private void m_txtMedSpec_TextChanged(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_mthIsSave();
		}

		private void m_txtMedName_TextChanged(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_mthIsSave();
		}

		private void m_txtQTY1_TextChanged(object sender, System.EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_mthIsSave();
		}

		private void m_ctlInMedRK_e_evtWarning(object sender, EventArgs e)
		{
			publicClass.m_mthShowWarning(this.m_ctlInMedOrd.m_cobStorage,"请先选择药库!");
			this.m_ctlInMedOrd.m_cobStorage.Focus();
		}

		private void m_ctlInMedRK_e_evtAddClick(object sender, EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_mthOkButtonClick();
			
		}

		private void m_ctlInMedTK_e_evtWarning(object sender, EventArgs e)
		{
			publicClass.m_mthShowWarning(this.m_ctlInMedOrd.textVENDOR,"请先选择药房!");
			this.m_ctlInMedOrd.textVENDOR.Focus();
		}

		private void m_ctlInMedTK_e_evtAddClick(object sender, EventArgs e)
		{
			((clsControlMedStorageInOrd)this.objController).m_mthOkButtonClick();
		}

		private void m_ctlInMedOrd_e_evtStorageChang(object sender, EventArgs e)
		{
			this.m_ctlInMed.strSTORAGEID=this.m_ctlInMedOrd.strGetStorageID;
			this.m_ctlInMed.intIsReData=1;
			try
			{
				this.m_ctlInMed.storageProfit=Double.Parse(this.m_ctlInMedOrd.strGetSTORAGEGROSSPROFIT)/100;
			}
			catch
			{
				this.m_ctlInMed.storageProfit=0;
			}
		}

		private void m_ctlInMedOrd_e_evtStorageSelect(object sender, EventArgs e)
		{
			this.m_ctlInMed.strMedstorage=(string)this.m_ctlInMedOrd.textVENDOR.Tag;
		}
	}
}
