using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmApplStorage 的摘要说明。
	/// </summary>
	public class frmApplStorage : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.ColumnHeader Row;
		private System.Windows.Forms.ColumnHeader m_clhMedID;
		private System.Windows.Forms.ColumnHeader MedName;
		private System.Windows.Forms.ColumnHeader Unit;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader m_clhMedProductArea;
		private System.Windows.Forms.ColumnHeader m_clhMedStoNow;
		internal System.Windows.Forms.ListView m_lsvApplDetail;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		internal System.Windows.Forms.ListView m_lsvStockList;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeaderTime;
		internal System.Windows.Forms.Panel panel2;
		internal PinkieControls.ButtonXP btnClear;
		internal PinkieControls.ButtonXP btnAdd;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.GroupBox groupBox3;
		internal PinkieControls.ButtonXP dntEmp;
		internal PinkieControls.ButtonXP btnesc;
		internal PinkieControls.ButtonXP btnDelect;
		internal PinkieControls.ButtonXP btnFind;
		internal PinkieControls.ButtonXP btnSave;
		internal PinkieControls.ButtonXP m_btnNew;
		internal System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label5;
		internal System.Windows.Forms.TextBox m_txtMemo;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.TextBox m_txtQty;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label18;
		internal System.Windows.Forms.TextBox m_txtMedName;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		internal PinkieControls.ButtonXP btnRe;
		internal PinkieControls.ButtonXP btnfinddata;
		internal System.Windows.Forms.Panel panefind;
		internal System.Windows.Forms.Panel panelord;
		internal System.Windows.Forms.ListView lsvlimi;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.Label label24;
		internal System.Windows.Forms.TextBox medNam;
		internal System.Windows.Forms.TextBox txtStorageName;
		private System.Windows.Forms.Label txtname;
		private System.Windows.Forms.GroupBox ikuy;
		internal PinkieControls.ButtonXP btnOrd;
		internal PinkieControls.ButtonXP btnclose;
		internal PinkieControls.ButtonXP btnOrdAll;
		internal PinkieControls.ButtonXP btnReture;
		internal PinkieControls.ButtonXP btndatafind;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		internal System.Windows.Forms.ErrorProvider errorProvider1;
		internal PinkieControls.ButtonXP btnPrint;
		internal PinkieControls.ButtonXP btnOver;
		internal System.Windows.Forms.Label txtMedID;
		private System.Windows.Forms.Label label12;
		internal NullableDateControls.MaskDateEdit m_dtpCreateDate;
		private System.Windows.Forms.Label label25;
		internal System.Windows.Forms.Label txtTolNumber;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label15;
		internal System.Windows.Forms.Label m_txtMedSpec;
		internal System.Windows.Forms.Label m_Unit;
		private System.Windows.Forms.Label label28;
		internal System.Windows.Forms.Label m_txtProduct;
		private System.Windows.Forms.Label label30;
		internal System.Windows.Forms.Label m_txtTolBuyPrice;
		internal com.digitalwave.controls.ControlMedicineFind ctlShowMed;
		internal System.Windows.Forms.TextBox m_txtFind;
		internal System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label10;
		internal System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Panel panel1;
		internal System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		internal com.digitalwave.iCare.gui.HIS.exComboBox m_cobStorage;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		internal System.Windows.Forms.DateTimePicker dateTimePicker1;
		internal System.Windows.Forms.Label label1;
		internal System.Windows.Forms.Label label6;
		internal System.Windows.Forms.TextBox m_txtConPrice;
		internal System.Windows.Forms.TextBox m_txtUNit;
        private System.Windows.Forms.Label label13;
        private Label label20;
        public com.digitalwave.controls.ctlTextBoxFind textVENDOR;
        private ColumnHeader columnHeader17;
        private ColumnHeader columnHeader18;
        private IContainer components;

		public frmApplStorage()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmApplStorage));
            this.m_lsvApplDetail = new System.Windows.Forms.ListView();
            this.Row = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedID = new System.Windows.Forms.ColumnHeader();
            this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
            this.MedName = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.Unit = new System.Windows.Forms.ColumnHeader();
            this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedProductArea = new System.Windows.Forms.ColumnHeader();
            this.m_clhMedStoNow = new System.Windows.Forms.ColumnHeader();
            this.m_lsvStockList = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTime = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.m_txtUNit = new System.Windows.Forms.TextBox();
            this.m_txtConPrice = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.m_txtTolBuyPrice = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.m_txtProduct = new System.Windows.Forms.Label();
            this.m_Unit = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.m_txtMedSpec = new System.Windows.Forms.Label();
            this.m_txtMedName = new System.Windows.Forms.TextBox();
            this.m_txtQty = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnClear = new PinkieControls.ButtonXP();
            this.btnAdd = new PinkieControls.ButtonXP();
            this.label17 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnOver = new PinkieControls.ButtonXP();
            this.btnPrint = new PinkieControls.ButtonXP();
            this.dntEmp = new PinkieControls.ButtonXP();
            this.btnesc = new PinkieControls.ButtonXP();
            this.btnDelect = new PinkieControls.ButtonXP();
            this.btnFind = new PinkieControls.ButtonXP();
            this.btnSave = new PinkieControls.ButtonXP();
            this.m_btnNew = new PinkieControls.ButtonXP();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textVENDOR = new com.digitalwave.controls.ctlTextBoxFind();
            this.m_dtpCreateDate = new NullableDateControls.MaskDateEdit();
            this.label20 = new System.Windows.Forms.Label();
            this.m_cobStorage = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtTolNumber = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMedID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_txtMemo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panefind = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnRe = new PinkieControls.ButtonXP();
            this.btnfinddata = new PinkieControls.ButtonXP();
            this.m_txtFind = new System.Windows.Forms.TextBox();
            this.panelord = new System.Windows.Forms.Panel();
            this.ikuy = new System.Windows.Forms.GroupBox();
            this.btnOrd = new PinkieControls.ButtonXP();
            this.btnclose = new PinkieControls.ButtonXP();
            this.btnOrdAll = new PinkieControls.ButtonXP();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnReture = new PinkieControls.ButtonXP();
            this.btndatafind = new PinkieControls.ButtonXP();
            this.label24 = new System.Windows.Forms.Label();
            this.medNam = new System.Windows.Forms.TextBox();
            this.txtStorageName = new System.Windows.Forms.TextBox();
            this.txtname = new System.Windows.Forms.Label();
            this.lsvlimi = new System.Windows.Forms.ListView();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.ctlShowMed = new com.digitalwave.controls.ControlMedicineFind();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panefind.SuspendLayout();
            this.panelord.SuspendLayout();
            this.ikuy.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lsvApplDetail
            // 
            this.m_lsvApplDetail.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.m_lsvApplDetail.AllowColumnReorder = true;
            this.m_lsvApplDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lsvApplDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Row,
            this.m_clhMedID,
            this.columnHeader16,
            this.MedName,
            this.columnHeader1,
            this.Unit,
            this.columnHeader20,
            this.columnHeader7,
            this.m_clhMedProductArea,
            this.m_clhMedStoNow});
            this.m_lsvApplDetail.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvApplDetail.FullRowSelect = true;
            this.m_lsvApplDetail.GridLines = true;
            this.m_lsvApplDetail.HideSelection = false;
            this.m_lsvApplDetail.HoverSelection = true;
            this.m_lsvApplDetail.Location = new System.Drawing.Point(392, 0);
            this.m_lsvApplDetail.MultiSelect = false;
            this.m_lsvApplDetail.Name = "m_lsvApplDetail";
            this.m_lsvApplDetail.Size = new System.Drawing.Size(616, 376);
            this.m_lsvApplDetail.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.m_lsvApplDetail.TabIndex = 500;
            this.m_lsvApplDetail.TabStop = false;
            this.m_lsvApplDetail.UseCompatibleStateImageBehavior = false;
            this.m_lsvApplDetail.View = System.Windows.Forms.View.Details;
            this.m_lsvApplDetail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvApplDetail_KeyDown);
            this.m_lsvApplDetail.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_lsvApplDetail_MouseDown);
            this.m_lsvApplDetail.Click += new System.EventHandler(this.m_lsvApplDetail_Click);
            // 
            // Row
            // 
            this.Row.Text = "行号";
            this.Row.Width = 0;
            // 
            // m_clhMedID
            // 
            this.m_clhMedID.Text = "药品代码";
            this.m_clhMedID.Width = 0;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "药品助记码";
            this.columnHeader16.Width = 100;
            // 
            // MedName
            // 
            this.MedName.Text = "药品名称";
            this.MedName.Width = 130;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "规格";
            this.columnHeader1.Width = 140;
            // 
            // Unit
            // 
            this.Unit.Text = "单位";
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "数量";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "单价";
            // 
            // m_clhMedProductArea
            // 
            this.m_clhMedProductArea.Text = "生产厂家";
            this.m_clhMedProductArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_clhMedProductArea.Width = 122;
            // 
            // m_clhMedStoNow
            // 
            this.m_clhMedStoNow.Text = "金额";
            this.m_clhMedStoNow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // m_lsvStockList
            // 
            this.m_lsvStockList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader14,
            this.columnHeaderTime,
            this.columnHeader3,
            this.columnHeader17});
            this.m_lsvStockList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvStockList.FullRowSelect = true;
            this.m_lsvStockList.GridLines = true;
            this.m_lsvStockList.HideSelection = false;
            this.m_lsvStockList.Location = new System.Drawing.Point(0, 0);
            this.m_lsvStockList.MultiSelect = false;
            this.m_lsvStockList.Name = "m_lsvStockList";
            this.m_lsvStockList.Size = new System.Drawing.Size(380, 585);
            this.m_lsvStockList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.m_lsvStockList.TabIndex = 400;
            this.m_lsvStockList.TabStop = false;
            this.m_lsvStockList.UseCompatibleStateImageBehavior = false;
            this.m_lsvStockList.View = System.Windows.Forms.View.Details;
            this.m_lsvStockList.SelectedIndexChanged += new System.EventHandler(this.m_lsvStockList_SelectedIndexChanged);
            this.m_lsvStockList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvStockList_KeyDown);
            this.m_lsvStockList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_lsvStockList_MouseDown);
            this.m_lsvStockList.Click += new System.EventHandler(this.m_lsvStockList_Click);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "单据号";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "药库名称";
            this.columnHeader14.Width = 70;
            // 
            // columnHeaderTime
            // 
            this.columnHeaderTime.Text = "日期";
            this.columnHeaderTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeaderTime.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "申请人";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "供  应  商";
            this.columnHeader17.Width = 120;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.m_txtUNit);
            this.panel2.Controls.Add(this.m_txtConPrice);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label30);
            this.panel2.Controls.Add(this.m_txtTolBuyPrice);
            this.panel2.Controls.Add(this.label28);
            this.panel2.Controls.Add(this.m_txtProduct);
            this.panel2.Controls.Add(this.m_Unit);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.m_txtMedSpec);
            this.panel2.Controls.Add(this.m_txtMedName);
            this.panel2.Controls.Add(this.m_txtQty);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Location = new System.Drawing.Point(392, 456);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(616, 112);
            this.panel2.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(144, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 14);
            this.label13.TabIndex = 177;
            this.label13.Text = "单位";
            // 
            // m_txtUNit
            // 
            this.m_txtUNit.Anchor = System.Windows.Forms.AnchorStyles.None;
            //this.m_txtUNit.EnableAutoValidation = false;
            //this.m_txtUNit.EnableEnterKeyValidate = false;
            //this.m_txtUNit.EnableEscapeKeyUndo = true;
            //this.m_txtUNit.EnableLastValidValue = true;
            //this.m_txtUNit.ErrorProvider = null;
            //this.m_txtUNit.ErrorProviderMessage = "Invalid value";
            this.m_txtUNit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.m_txtUNit.ForceFormatText = true;
            this.m_txtUNit.Location = new System.Drawing.Point(190, 42);
            this.m_txtUNit.MaxLength = 6;
            this.m_txtUNit.Name = "m_txtUNit";
            this.m_txtUNit.Size = new System.Drawing.Size(58, 23);
            this.m_txtUNit.TabIndex = 7;
            this.m_txtUNit.TextChanged += new System.EventHandler(this.m_txtMedName_TextChanged);
            this.m_txtUNit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtUNit_KeyDown);
            // 
            // m_txtConPrice
            // 
            this.m_txtConPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            //this.m_txtConPrice.EnableAutoValidation = false;
            //this.m_txtConPrice.EnableEnterKeyValidate = false;
            //this.m_txtConPrice.EnableEscapeKeyUndo = true;
            //this.m_txtConPrice.EnableLastValidValue = true;
            //this.m_txtConPrice.ErrorProvider = null;
            //this.m_txtConPrice.ErrorProviderMessage = "Invalid value";
            this.m_txtConPrice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.m_txtConPrice.ForceFormatText = true;
            this.m_txtConPrice.Location = new System.Drawing.Point(80, 78);
            this.m_txtConPrice.MaxLength = 10;
            this.m_txtConPrice.Name = "m_txtConPrice";
            //this.m_txtConPrice.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
            this.m_txtConPrice.Size = new System.Drawing.Size(128, 23);
            this.m_txtConPrice.TabIndex = 9;
            this.m_txtConPrice.Text = "0";
            this.m_txtConPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtConPrice.Leave += new System.EventHandler(this.m_txtQty_Leave);
            this.m_txtConPrice.TextChanged += new System.EventHandler(this.m_txtMedName_TextChanged);
            this.m_txtConPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtConPrice_KeyDown);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(224, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 16);
            this.label6.TabIndex = 174;
            this.label6.Text = "元";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(416, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 173;
            this.label1.Text = "元";
            // 
            // label30
            // 
            this.label30.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label30.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label30.Location = new System.Drawing.Point(328, 96);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(72, 1);
            this.label30.TabIndex = 171;
            this.label30.Text = "label30";
            // 
            // m_txtTolBuyPrice
            // 
            this.m_txtTolBuyPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_txtTolBuyPrice.Location = new System.Drawing.Point(328, 78);
            this.m_txtTolBuyPrice.Name = "m_txtTolBuyPrice";
            this.m_txtTolBuyPrice.Size = new System.Drawing.Size(72, 23);
            this.m_txtTolBuyPrice.TabIndex = 170;
            this.m_txtTolBuyPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            this.label28.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label28.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label28.Location = new System.Drawing.Point(328, 64);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(280, 1);
            this.label28.TabIndex = 167;
            this.label28.Text = "label28";
            // 
            // m_txtProduct
            // 
            this.m_txtProduct.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_txtProduct.Location = new System.Drawing.Point(328, 42);
            this.m_txtProduct.Name = "m_txtProduct";
            this.m_txtProduct.Size = new System.Drawing.Size(280, 23);
            this.m_txtProduct.TabIndex = 166;
            this.m_txtProduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_Unit
            // 
            this.m_Unit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_Unit.AutoSize = true;
            this.m_Unit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_Unit.Location = new System.Drawing.Point(176, 24);
            this.m_Unit.Name = "m_Unit";
            this.m_Unit.Size = new System.Drawing.Size(0, 14);
            this.m_Unit.TabIndex = 165;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label15.Location = new System.Drawing.Point(328, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(280, 1);
            this.label15.TabIndex = 164;
            this.label15.Text = "label15";
            // 
            // m_txtMedSpec
            // 
            this.m_txtMedSpec.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_txtMedSpec.Location = new System.Drawing.Point(328, 6);
            this.m_txtMedSpec.Name = "m_txtMedSpec";
            this.m_txtMedSpec.Size = new System.Drawing.Size(280, 23);
            this.m_txtMedSpec.TabIndex = 163;
            this.m_txtMedSpec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_txtMedName
            // 
            this.m_txtMedName.Anchor = System.Windows.Forms.AnchorStyles.None;
            //this.m_txtMedName.EnableAutoValidation = false;
            //this.m_txtMedName.EnableEnterKeyValidate = false;
            //this.m_txtMedName.EnableEscapeKeyUndo = true;
            //this.m_txtMedName.EnableLastValidValue = true;
            //this.m_txtMedName.ErrorProvider = null;
            //this.m_txtMedName.ErrorProviderMessage = "Invalid value";
            this.m_txtMedName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.m_txtMedName.ForceFormatText = true;
            this.m_txtMedName.Location = new System.Drawing.Point(80, 6);
            this.m_txtMedName.Name = "m_txtMedName";
            this.m_txtMedName.Size = new System.Drawing.Size(168, 23);
            this.m_txtMedName.TabIndex = 0;
            this.m_txtMedName.TextChanged += new System.EventHandler(this.m_txtMedName_TextChanged);
            this.m_txtMedName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtMedName_KeyDown);
            // 
            // m_txtQty
            // 
            this.m_txtQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            //this.m_txtQty.EnableAutoValidation = false;
            //this.m_txtQty.EnableEnterKeyValidate = false;
            //this.m_txtQty.EnableEscapeKeyUndo = true;
            //this.m_txtQty.EnableLastValidValue = true;
            //this.m_txtQty.ErrorProvider = null;
            //this.m_txtQty.ErrorProviderMessage = "Invalid value";
            this.m_txtQty.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.m_txtQty.ForceFormatText = true;
            this.m_txtQty.Location = new System.Drawing.Point(80, 42);
            this.m_txtQty.MaxLength = 10;
            this.m_txtQty.Name = "m_txtQty";
            //this.m_txtQty.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
            this.m_txtQty.Size = new System.Drawing.Size(56, 23);
            this.m_txtQty.TabIndex = 5;
            this.m_txtQty.Text = "0";
            this.m_txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtQty.Leave += new System.EventHandler(this.m_txtQty_Leave);
            this.m_txtQty.TextChanged += new System.EventHandler(this.m_txtMedName_TextChanged);
            this.m_txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtQty_KeyDown);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(256, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 14);
            this.label9.TabIndex = 147;
            this.label9.Text = "金    额";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(16, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 14);
            this.label14.TabIndex = 144;
            this.label14.Text = "数    量";
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(256, 8);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 14);
            this.label16.TabIndex = 142;
            this.label16.Text = "规    格";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(16, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 140;
            this.label3.Text = "参 考 价";
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(16, 8);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(63, 14);
            this.label18.TabIndex = 139;
            this.label18.Text = "名    称";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnClear.DefaultScheme = true;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClear.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.Hint = "";
            this.btnClear.Location = new System.Drawing.Point(536, 80);
            this.btnClear.Name = "btnClear";
            this.btnClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnClear.Size = new System.Drawing.Size(62, 24);
            this.btnClear.TabIndex = 138;
            this.btnClear.Text = "清空(&C)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnAdd.DefaultScheme = true;
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.Hint = "";
            this.btnAdd.Location = new System.Drawing.Point(456, 80);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnAdd.Size = new System.Drawing.Size(62, 24);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "增加(&A)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(256, 44);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 14);
            this.label17.TabIndex = 129;
            this.label17.Text = "生产厂家";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label38);
            this.panel4.Controls.Add(this.label37);
            this.panel4.Controls.Add(this.label36);
            this.panel4.Controls.Add(this.label26);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Location = new System.Drawing.Point(0, 624);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1016, 32);
            this.panel4.TabIndex = 137;
            // 
            // label38
            // 
            this.label38.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label38.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label38.Location = new System.Drawing.Point(664, 8);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(144, 24);
            this.label38.TabIndex = 4;
            this.label38.Text = "F12输入采购单明细";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label37
            // 
            this.label37.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label37.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label37.Location = new System.Drawing.Point(466, 8);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(184, 24);
            this.label37.TabIndex = 3;
            this.label37.Text = "F11输入申请单状态";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label36.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label36.Location = new System.Drawing.Point(268, 8);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(184, 24);
            this.label36.TabIndex = 2;
            this.label36.Text = "F10选择采购明细";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label26.Location = new System.Drawing.Point(102, 8);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(152, 24);
            this.label26.TabIndex = 1;
            this.label26.Text = "F9选择申请单";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label22.Location = new System.Drawing.Point(8, 8);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(80, 24);
            this.label22.TabIndex = 0;
            this.label22.Text = "系统提示：";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnOver);
            this.groupBox3.Controls.Add(this.btnPrint);
            this.groupBox3.Controls.Add(this.dntEmp);
            this.groupBox3.Controls.Add(this.btnesc);
            this.groupBox3.Controls.Add(this.btnDelect);
            this.groupBox3.Controls.Add(this.btnFind);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.m_btnNew);
            this.groupBox3.Location = new System.Drawing.Point(392, 568);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(616, 48);
            this.groupBox3.TabIndex = 138;
            this.groupBox3.TabStop = false;
            // 
            // btnOver
            // 
            this.btnOver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnOver.DefaultScheme = true;
            this.btnOver.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOver.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOver.Hint = "";
            this.btnOver.Location = new System.Drawing.Point(230, 16);
            this.btnOver.Name = "btnOver";
            this.btnOver.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnOver.Size = new System.Drawing.Size(67, 24);
            this.btnOver.TabIndex = 56;
            this.btnOver.TabStop = false;
            this.btnOver.Text = "审核(&E)";
            this.btnOver.Click += new System.EventHandler(this.btnOver_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnPrint.DefaultScheme = true;
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPrint.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrint.Hint = "";
            this.btnPrint.Location = new System.Drawing.Point(309, 16);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnPrint.Size = new System.Drawing.Size(62, 24);
            this.btnPrint.TabIndex = 55;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dntEmp
            // 
            this.dntEmp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.dntEmp.DefaultScheme = true;
            this.dntEmp.DialogResult = System.Windows.Forms.DialogResult.None;
            this.dntEmp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dntEmp.Hint = "";
            this.dntEmp.Location = new System.Drawing.Point(82, 16);
            this.dntEmp.Name = "dntEmp";
            this.dntEmp.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.dntEmp.Size = new System.Drawing.Size(62, 24);
            this.dntEmp.TabIndex = 54;
            this.dntEmp.TabStop = false;
            this.dntEmp.Text = "生成(&O)";
            this.dntEmp.Click += new System.EventHandler(this.dntEmp_Click);
            // 
            // btnesc
            // 
            this.btnesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnesc.DefaultScheme = true;
            this.btnesc.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnesc.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnesc.Hint = "";
            this.btnesc.Location = new System.Drawing.Point(531, 16);
            this.btnesc.Name = "btnesc";
            this.btnesc.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnesc.Size = new System.Drawing.Size(72, 24);
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
            this.btnDelect.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelect.Hint = "";
            this.btnDelect.Location = new System.Drawing.Point(457, 16);
            this.btnDelect.Name = "btnDelect";
            this.btnDelect.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnDelect.Size = new System.Drawing.Size(62, 24);
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
            this.btnFind.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFind.Hint = "";
            this.btnFind.Location = new System.Drawing.Point(383, 16);
            this.btnFind.Name = "btnFind";
            this.btnFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnFind.Size = new System.Drawing.Size(62, 24);
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
            this.btnSave.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Hint = "";
            this.btnSave.Location = new System.Drawing.Point(156, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnSave.Size = new System.Drawing.Size(62, 24);
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
            this.m_btnNew.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_btnNew.Hint = "";
            this.m_btnNew.Location = new System.Drawing.Point(8, 16);
            this.m_btnNew.Name = "m_btnNew";
            this.m_btnNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnNew.Size = new System.Drawing.Size(62, 24);
            this.m_btnNew.TabIndex = 49;
            this.m_btnNew.TabStop = false;
            this.m_btnNew.Text = "新建(&N)";
            this.m_btnNew.Click += new System.EventHandler(this.m_btnNew_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.textVENDOR);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.m_cobStorage);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label25);
            this.panel3.Controls.Add(this.txtTolNumber);
            this.panel3.Controls.Add(this.m_dtpCreateDate);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.txtMedID);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.m_txtMemo);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(392, 376);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(616, 72);
            this.panel3.TabIndex = 0;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // textVENDOR
            // 
            this.textVENDOR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textVENDOR.BackColor = System.Drawing.SystemColors.Window;
            this.textVENDOR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textVENDOR.intHeight = 200;
            this.textVENDOR.IsEnterShow = true;
            this.textVENDOR.isHide = 4;
            this.textVENDOR.isTxt = 1;
            this.textVENDOR.isUpOrDn = 0;
            this.textVENDOR.isValuse = 4;
            this.textVENDOR.Location = new System.Drawing.Point(355, 5);
            this.textVENDOR.m_IsHaveParent = false;
            this.textVENDOR.m_strParentName = "";
            this.textVENDOR.Name = "textVENDOR";
            this.textVENDOR.nextCtl = this.m_dtpCreateDate;
            this.textVENDOR.Size = new System.Drawing.Size(131, 24);
            this.textVENDOR.TabIndex = 1;
            this.textVENDOR.txtValuse = "";
            this.textVENDOR.VsLeftOrRight = 0;
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_dtpCreateDate.Location = new System.Drawing.Point(80, 40);
            this.m_dtpCreateDate.Mask = "yyyy年MM月dd日";
            this.m_dtpCreateDate.Name = "m_dtpCreateDate";
            this.m_dtpCreateDate.Size = new System.Drawing.Size(120, 23);
            this.m_dtpCreateDate.TabIndex = 2;
            this.m_dtpCreateDate.TextChanged += new System.EventHandler(this.m_txtMedName_TextChanged);
            this.m_dtpCreateDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpCreateDate_KeyDown_1);
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(302, 10);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(49, 14);
            this.label20.TabIndex = 160;
            this.label20.Text = "供应商";
            // 
            // m_cobStorage
            // 
            this.m_cobStorage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_cobStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cobStorage.Location = new System.Drawing.Point(234, 5);
            this.m_cobStorage.Name = "m_cobStorage";
            this.m_cobStorage.Size = new System.Drawing.Size(62, 22);
            this.m_cobStorage.TabIndex = 0;
            this.m_cobStorage.SelectedIndexChanged += new System.EventHandler(this.m_cobStorage_SelectedIndexChanged);
            this.m_cobStorage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cobStorage_KeyDown);
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label19.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(592, 8);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(16, 16);
            this.label19.TabIndex = 159;
            this.label19.Text = "元";
            // 
            // label25
            // 
            this.label25.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label25.Location = new System.Drawing.Point(542, 24);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(48, 1);
            this.label25.TabIndex = 156;
            this.label25.Text = "label25";
            // 
            // txtTolNumber
            // 
            this.txtTolNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTolNumber.Location = new System.Drawing.Point(542, 8);
            this.txtTolNumber.Name = "txtTolNumber";
            this.txtTolNumber.Size = new System.Drawing.Size(48, 16);
            this.txtTolNumber.TabIndex = 155;
            this.txtTolNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Location = new System.Drawing.Point(80, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 1);
            this.label12.TabIndex = 149;
            this.label12.Text = "label12";
            // 
            // txtMedID
            // 
            this.txtMedID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMedID.Location = new System.Drawing.Point(80, 5);
            this.txtMedID.Name = "txtMedID";
            this.txtMedID.Size = new System.Drawing.Size(120, 23);
            this.txtMedID.TabIndex = 148;
            this.txtMedID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(203, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 16);
            this.label8.TabIndex = 147;
            this.label8.Text = "药库";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(16, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 16);
            this.label11.TabIndex = 128;
            this.label11.Text = "采购单号";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(16, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 118;
            this.label5.Text = "日    期";
            // 
            // m_txtMemo
            // 
            this.m_txtMemo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_txtMemo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtMemo.Location = new System.Drawing.Point(235, 40);
            this.m_txtMemo.Name = "m_txtMemo";
            this.m_txtMemo.Size = new System.Drawing.Size(365, 23);
            this.m_txtMemo.TabIndex = 3;
            this.m_txtMemo.TextChanged += new System.EventHandler(this.m_txtMedName_TextChanged);
            this.m_txtMemo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtMemo_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(203, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 117;
            this.label4.Text = "备注";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(492, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 145;
            this.label2.Text = "总金额";
            // 
            // panefind
            // 
            this.panefind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panefind.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panefind.Controls.Add(this.label10);
            this.panefind.Controls.Add(this.label7);
            this.panefind.Controls.Add(this.comboBox1);
            this.panefind.Controls.Add(this.btnRe);
            this.panefind.Controls.Add(this.btnfinddata);
            this.panefind.Controls.Add(this.m_txtFind);
            this.panefind.Location = new System.Drawing.Point(392, 344);
            this.panefind.Name = "panefind";
            this.panefind.Size = new System.Drawing.Size(616, 32);
            this.panefind.TabIndex = 3;
            this.panefind.Visible = false;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(232, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 16);
            this.label10.TabIndex = 143;
            this.label10.Text = "查找内容";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(16, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 16);
            this.label7.TabIndex = 142;
            this.label7.Text = "查找方式";
            // 
            // comboBox1
            // 
            this.comboBox1.Items.AddRange(new object[] {
            "单据号",
            "申请人"});
            this.comboBox1.Location = new System.Drawing.Point(80, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(144, 22);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyDown);
            // 
            // btnRe
            // 
            this.btnRe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnRe.DefaultScheme = true;
            this.btnRe.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRe.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRe.Hint = "";
            this.btnRe.Location = new System.Drawing.Point(528, 0);
            this.btnRe.Name = "btnRe";
            this.btnRe.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnRe.Size = new System.Drawing.Size(72, 24);
            this.btnRe.TabIndex = 140;
            this.btnRe.Text = "返回(&R)";
            this.btnRe.Click += new System.EventHandler(this.btnRe_Click);
            // 
            // btnfinddata
            // 
            this.btnfinddata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnfinddata.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnfinddata.DefaultScheme = true;
            this.btnfinddata.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnfinddata.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnfinddata.Hint = "";
            this.btnfinddata.Location = new System.Drawing.Point(432, 0);
            this.btnfinddata.Name = "btnfinddata";
            this.btnfinddata.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnfinddata.Size = new System.Drawing.Size(72, 24);
            this.btnfinddata.TabIndex = 3;
            this.btnfinddata.Text = "查找(&K)";
            this.btnfinddata.Click += new System.EventHandler(this.btnfinddata_Click);
            // 
            // m_txtFind
            // 
            this.m_txtFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            //this.m_txtFind.EnableAutoValidation = true;
            //this.m_txtFind.EnableEnterKeyValidate = true;
            //this.m_txtFind.EnableEscapeKeyUndo = true;
            //this.m_txtFind.EnableLastValidValue = true;
            //this.m_txtFind.ErrorProvider = null;
            //this.m_txtFind.ErrorProviderMessage = "Invalid value";
            this.m_txtFind.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.m_txtFind.ForceFormatText = true;
            this.m_txtFind.Location = new System.Drawing.Point(296, 0);
            this.m_txtFind.Name = "m_txtFind";
            this.m_txtFind.Size = new System.Drawing.Size(120, 23);
            this.m_txtFind.TabIndex = 2;
            this.m_txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFindDatetime_KeyDown);
            // 
            // panelord
            // 
            this.panelord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panelord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelord.Controls.Add(this.ikuy);
            this.panelord.Controls.Add(this.panel5);
            this.panelord.Controls.Add(this.lsvlimi);
            this.panelord.Location = new System.Drawing.Point(0, 32);
            this.panelord.Name = "panelord";
            this.panelord.Size = new System.Drawing.Size(384, 615);
            this.panelord.TabIndex = 410;
            this.panelord.Visible = false;
            // 
            // ikuy
            // 
            this.ikuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ikuy.Controls.Add(this.btnOrd);
            this.ikuy.Controls.Add(this.btnclose);
            this.ikuy.Controls.Add(this.btnOrdAll);
            this.ikuy.Location = new System.Drawing.Point(2, 567);
            this.ikuy.Name = "ikuy";
            this.ikuy.Size = new System.Drawing.Size(377, 44);
            this.ikuy.TabIndex = 12;
            this.ikuy.TabStop = false;
            // 
            // btnOrd
            // 
            this.btnOrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnOrd.DefaultScheme = true;
            this.btnOrd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOrd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOrd.Hint = "";
            this.btnOrd.Location = new System.Drawing.Point(148, 14);
            this.btnOrd.Name = "btnOrd";
            this.btnOrd.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnOrd.Size = new System.Drawing.Size(88, 24);
            this.btnOrd.TabIndex = 57;
            this.btnOrd.TabStop = false;
            this.btnOrd.Text = "生成(&X)";
            this.btnOrd.Click += new System.EventHandler(this.btnOrd_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnclose.DefaultScheme = true;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnclose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnclose.Hint = "";
            this.btnclose.Location = new System.Drawing.Point(256, 14);
            this.btnclose.Name = "btnclose";
            this.btnclose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnclose.Size = new System.Drawing.Size(88, 24);
            this.btnclose.TabIndex = 56;
            this.btnclose.TabStop = false;
            this.btnclose.Text = "关闭(ESC)";
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnOrdAll
            // 
            this.btnOrdAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnOrdAll.DefaultScheme = true;
            this.btnOrdAll.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOrdAll.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOrdAll.Hint = "";
            this.btnOrdAll.Location = new System.Drawing.Point(40, 14);
            this.btnOrdAll.Name = "btnOrdAll";
            this.btnOrdAll.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnOrdAll.Size = new System.Drawing.Size(96, 24);
            this.btnOrdAll.TabIndex = 55;
            this.btnOrdAll.TabStop = false;
            this.btnOrdAll.Text = "全部生成(&L)";
            this.btnOrdAll.Click += new System.EventHandler(this.btnOrdAll_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.btnReture);
            this.panel5.Controls.Add(this.btndatafind);
            this.panel5.Controls.Add(this.label24);
            this.panel5.Controls.Add(this.medNam);
            this.panel5.Controls.Add(this.txtStorageName);
            this.panel5.Controls.Add(this.txtname);
            this.panel5.Location = new System.Drawing.Point(0, 487);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(384, 80);
            this.panel5.TabIndex = 11;
            // 
            // btnReture
            // 
            this.btnReture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnReture.DefaultScheme = true;
            this.btnReture.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnReture.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReture.Hint = "";
            this.btnReture.Location = new System.Drawing.Point(256, 48);
            this.btnReture.Name = "btnReture";
            this.btnReture.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnReture.Size = new System.Drawing.Size(88, 24);
            this.btnReture.TabIndex = 143;
            this.btnReture.Text = "返回(&M)";
            this.btnReture.Click += new System.EventHandler(this.btnReture_Click);
            // 
            // btndatafind
            // 
            this.btndatafind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btndatafind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btndatafind.DefaultScheme = true;
            this.btndatafind.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btndatafind.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btndatafind.Hint = "";
            this.btndatafind.Location = new System.Drawing.Point(40, 48);
            this.btndatafind.Name = "btndatafind";
            this.btndatafind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btndatafind.Size = new System.Drawing.Size(96, 24);
            this.btndatafind.TabIndex = 142;
            this.btndatafind.Text = "查找(&B)";
            this.btndatafind.Click += new System.EventHandler(this.buttonXP2_Click);
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label24.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label24.Location = new System.Drawing.Point(200, 11);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(64, 16);
            this.label24.TabIndex = 141;
            this.label24.Text = "药 品 名 称";
            // 
            // medNam
            // 
            this.medNam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            //this.medNam.EnableAutoValidation = true;
            //this.medNam.EnableEnterKeyValidate = true;
            //this.medNam.EnableEscapeKeyUndo = true;
            //this.medNam.EnableLastValidValue = true;
            //this.medNam.ErrorProvider = null;
            //this.medNam.ErrorProviderMessage = "Invalid value";
            this.medNam.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.medNam.ForceFormatText = true;
            this.medNam.Location = new System.Drawing.Point(264, 9);
            this.medNam.Name = "medNam";
            this.medNam.Size = new System.Drawing.Size(104, 23);
            this.medNam.TabIndex = 140;
            // 
            // txtStorageName
            // 
            this.txtStorageName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            //this.txtStorageName.EnableAutoValidation = true;
            //this.txtStorageName.EnableEnterKeyValidate = true;
            //this.txtStorageName.EnableEscapeKeyUndo = true;
            //this.txtStorageName.EnableLastValidValue = true;
            //this.txtStorageName.ErrorProvider = null;
            //this.txtStorageName.ErrorProviderMessage = "Invalid value";
            this.txtStorageName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.txtStorageName.ForceFormatText = true;
            this.txtStorageName.Location = new System.Drawing.Point(80, 9);
            this.txtStorageName.Name = "txtStorageName";
            this.txtStorageName.Size = new System.Drawing.Size(104, 23);
            this.txtStorageName.TabIndex = 139;
            this.txtStorageName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStorageName_KeyDown);
            // 
            // txtname
            // 
            this.txtname.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtname.Location = new System.Drawing.Point(8, 8);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(72, 23);
            this.txtname.TabIndex = 138;
            this.txtname.Text = "仓     库";
            this.txtname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lsvlimi
            // 
            this.lsvlimi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lsvlimi.CheckBoxes = true;
            this.lsvlimi.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader13,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.lsvlimi.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lsvlimi.FullRowSelect = true;
            this.lsvlimi.GridLines = true;
            this.lsvlimi.HideSelection = false;
            this.lsvlimi.Location = new System.Drawing.Point(-2, 0);
            this.lsvlimi.Name = "lsvlimi";
            this.lsvlimi.Size = new System.Drawing.Size(384, 487);
            this.lsvlimi.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lsvlimi.TabIndex = 300;
            this.lsvlimi.TabStop = false;
            this.lsvlimi.UseCompatibleStateImageBehavior = false;
            this.lsvlimi.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "仓库";
            this.columnHeader8.Width = 90;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "药品名称";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 100;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "单位";
            this.columnHeader13.Width = 50;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "现库存";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "下限";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "采购数量";
            this.columnHeader12.Width = 80;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ctlShowMed
            // 
            this.ctlShowMed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlShowMed.blIsMedStorage = true;
            this.ctlShowMed.blISOutStorage = false;
            this.ctlShowMed.blRepertory = true;
            this.ctlShowMed.FindMedmode = 0;
            this.ctlShowMed.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctlShowMed.intIsReData = 0;
            this.ctlShowMed.isApplMebMod = null;
            this.ctlShowMed.isApplModel = true;
            this.ctlShowMed.isShowFindType = true;
            this.ctlShowMed.IsShowZero = false;
            this.ctlShowMed.Location = new System.Drawing.Point(392, -320);
            this.ctlShowMed.Name = "ctlShowMed";
            this.ctlShowMed.Size = new System.Drawing.Size(624, 336);
            this.ctlShowMed.status = 0;
            this.ctlShowMed.strMedstorage = null;
            this.ctlShowMed.strSTORAGEID = "-1";
            this.ctlShowMed.TabIndex = 401;
            this.ctlShowMed.Visible = false;
            this.ctlShowMed.m_evtReturnVal += new com.digitalwave.controls.dlgReturnVal(this.ctlShowMed_m_evtReturnVal);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(388, 612);
            this.tabControl1.TabIndex = 501;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_lsvStockList);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(380, 585);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "未审核";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(380, 587);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "已审核";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader15,
            this.columnHeader18});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(380, 587);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 401;
            this.listView1.TabStop = false;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "单据号";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "药库名称";
            this.columnHeader5.Width = 70;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "日期";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 100;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "申请人";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader15.Width = 80;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "供  应  商";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(392, 616);
            this.panel1.TabIndex = 502;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Location = new System.Drawing.Point(272, 592);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(120, 23);
            this.dateTimePicker1.TabIndex = 503;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // frmApplStorage
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1008, 653);
            this.Controls.Add(this.panelord);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panefind);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.ctlShowMed);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.m_lsvApplDetail);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Name = "frmApplStorage";
            this.Text = "药库采购";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmApplStorage_KeyDown);
            this.Load += new System.EventHandler(this.frmApplStorage_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panefind.ResumeLayout(false);
            this.panefind.PerformLayout();
            this.panelord.ResumeLayout(false);
            this.ikuy.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		private void frmApplStorage_Load(object sender, System.EventArgs e)
		{
            ((clsControlApplStorage)this.objController).m_lngSetupFrm();
            this.m_mthSetEnter2Tab(new System.Windows.Forms.Control[] { });
			
		}
		public override void CreateController()
		{
			this.objController = new clsControlApplStorage();
			this.objController.Set_GUI_Apperance(this);
		}
		private void m_lsvStockList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				((clsControlApplStorage)this.objController).m_lngShowDe();
		}

		private void m_lsvApplDetail_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				((clsControlApplStorage)this.objController).m_lngLisvSelectOfDe();
		}

		private void m_btnNew_Click(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_lngClearAll();
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_lngClearDe();
		}

		private void m_lsvStockList_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			((clsControlApplStorage)this.objController).MouseDown(2);
		}

		private void m_lsvApplDetail_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			((clsControlApplStorage)this.objController).MouseDown(1);
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_mthOkButtonClick();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_mthSave();
		}

		private void btnDelect_Click(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_lngDele();
		}

		private void btnesc_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show("是否要退出药品采购系统？","系统提示",MessageBoxButtons.YesNo)==DialogResult.Yes)
			this.Close();
		}

		private void btnFind_Click(object sender, System.EventArgs e)
		{
			if(this.panefind.Visible==false)
			{
				m_lsvApplDetail.Height=m_lsvApplDetail.Height-panefind.Height-3;
				Point p=this.panel3.Parent.PointToScreen(this.panel3.Location);
				p=this.FindForm().PointToClient(p);
				p.Y-=this.panefind.Height;
				this.panefind.Location=p;
				this.panefind.Visible=true;
			}
			this.comboBox1.Focus();
		}

		private void btnfinddata_Click(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_lngFindData();
		}

		private void btnRe_Click(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_lngReturn();
			m_lsvApplDetail.Height=m_lsvApplDetail.Height+panefind.Height+3;
		}

		private void dntEmp_Click(object sender, System.EventArgs e)
		{
			Point p=tabControl1.Location;
			panelord.Location=p;
			panelord.BringToFront();
			((clsControlApplStorage)this.objController).m_lngShowOrd();
		}

		private void btnclose_Click(object sender, System.EventArgs e)
		{
			this.panelord.Visible=false;
		}

		private void buttonXP2_Click(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_lngFindData();
		}

		private void btnReture_Click(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_lngReturnOrd();
		}

		private void btnOrdAll_Click(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_lngAutomatismAll();
		}

		private void btnOrd_Click(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_lngAutomatism();
		}

		private void frmApplStorage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{

			if(e.KeyCode==Keys.Escape)
			{
				if(ctlShowMed.Visible==true)
				{
					ctlShowMed.Visible=false;
					m_txtMedName.Focus();
					return;
				}
				if(MessageBox.Show("是否要退出药品采购系统？","Icaer",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
					this.Close();
			}

			if(e.KeyCode==Keys.F9)
			{
				this.lsvlimi.Focus();
				if(this.m_lsvStockList.Items.Count>0)
				this.m_lsvStockList.Items[0].Selected=true;
			}
			if(e.KeyCode==Keys.F10)
			{
				this.m_lsvApplDetail.Focus();
				if(this.m_lsvApplDetail.Items.Count>0)
				this.m_lsvApplDetail.Items[0].Selected=true;
			}

		}

		private void txtfindID_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtVendor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtfinddu_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtFindDatetime_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtMemo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtBuyPrice_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtQty_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtDiscount_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtStorageName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_lsvStockList_Click(object sender, System.EventArgs e)
		{

			((clsControlApplStorage)this.objController).m_lngShowDe();
			btnAdd.Enabled=true;
			btnClear.Enabled=true;
			m_cobStorage.Focus();
		}

		private void m_lsvApplDetail_Click(object sender, System.EventArgs e)
		{
			 ((clsControlApplStorage)this.objController).m_lngLisvSelectOfDe();
			  m_txtMedName.Focus();
		}
		int count=0;
		private void m_dtpCreateDate_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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

		private void m_txtMedName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				if(this.m_cobStorage.SelectItemText!="")
				{
					Point p=this.panel2.Parent.PointToScreen(this.panel2.Location);
					p=this.panel2.FindForm().PointToClient(p);
					p.Y-=ctlShowMed.Height;
					ctlShowMed.Width=panel2.Width;
					ctlShowMed.Location=p;
					ctlShowMed.Visible=true;
					ctlShowMed.Focus();
					ctlShowMed.intIsReData=0;
				}
				else
				{
					clsPublicParm publicClass=new clsPublicParm();
					publicClass.m_mthShowWarning(m_cobStorage,"请先选择一个药库!");
					m_cobStorage.Focus();
				}
				
			}
		}

		private void ctlShowMed_m_evtReturnVal(object sender, com.digitalwave.controls.clsEvtReturnVal e)
		{
			m_txtMedName.Text=e.objVO.strMEDICINENAME_VCHR;
			m_txtMedName.Tag=e.objVO.strMEDICINEID_CHR;
			m_txtMedSpec.Text=e.objVO.strMEDSPEC_VCHR;
			m_txtMedSpec.Tag=e.objVO.strASSISTCODE_CHR;
			m_txtProduct.Text=e.objVO.strPRODUCTORID_CHR;
			m_txtUNit.Text=e.objVO.strOPUNIT_CHR;
			m_txtMedSpec.Tag=e.objVO.strASSISTCODE_CHR;
			clsControlMedStorageInOrd InOrd=new clsControlMedStorageInOrd();
			m_txtConPrice.Text=InOrd.m_mthGetConsoltPrice(e.objVO.strMEDICINEID_CHR);
			ctlShowMed.Visible=false;
			ctlShowMed.intIsReData=0;
			m_txtQty.Focus();
		}

		private void comboBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_dtpCreateDate_KeyDown_1(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void textVENDOR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void btnOver_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show("是否要审核？审核后数据不能再修改！","Icare",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.No)
			{
				return;
			}
			 ((clsControlApplStorage)this.objController).m_mthAutoOrd();
		}

		private void listView1_Click(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_lngShowDe();
		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_mthTabChanged();
		}

		private void panel3_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void m_cobStorage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_lngFillToLsv();
		}

		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			((clsControlApplStorage)this.objController).m_mthprintStar(e);
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			printPreviewDialog1.ShowDialog();
		}

		private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			((clsControlApplStorage)this.objController).m_mthGetData();
		}

		private void m_cobStorage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void dateTimePicker1_ValueChanged(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_lngFillToLsv();
		}

		private void m_lsvStockList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(m_lsvStockList.SelectedItems.Count==0)
			{
				m_lsvApplDetail.Items.Clear();
				return;
			}
			this.m_lsvStockList.Items[this.m_lsvStockList.SelectedItems[0].Index].BackColor=System.Drawing.Color.PapayaWhip;
		}

		private void m_txtConPrice_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
				if(e.KeyCode==Keys.Enter)
		 {
			 if(btnAdd.Enabled==true)
				 btnAdd.Focus();
			 else
				 btnSave.Focus();
		 }
			
		}

		private void m_txtUNit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}
		private void m_txtMedName_TextChanged(object sender, System.EventArgs e)
		{
			((clsControlApplStorage)this.objController).m_mthIsSave();
		}

		private void m_txtQty_Leave(object sender, System.EventArgs e)
		{
			try
			{
				Double tomoney=Convert.ToDouble(m_txtConPrice.Text.Trim())*Convert.ToDouble(m_txtQty.Text.Trim());
				m_txtTolBuyPrice.Text=tomoney.ToString();
			}
			catch
			{
			}
		}

		private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(this.listView1.SelectedItems.Count>0)
					this.listView1.Items[this.m_lsvStockList.SelectedItems[0].Index].BackColor=System.Drawing.Color.PapayaWhip;
			}
			catch
			{
			}
		}
	}
}
