using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmStorageQuery 的摘要说明。
	/// </summary>
	public class frmStorageQuery : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		internal PinkieControls.ButtonXP m_BtnExit;
		internal System.Windows.Forms.ListView m_lvMedStockDetail;
		internal PinkieControls.ButtonXP m_BtnPrint;
		internal PinkieControls.ButtonXP m_BtnPrintDetail;
		internal System.Windows.Forms.CheckBox m_cbZero;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		internal com.digitalwave.controls.datagrid.ctlDataGrid m_dtgMedicineList;
		internal System.Windows.Forms.ComboBox m_cmdStorage;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		internal string strStorageFlag = "0";
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		internal PinkieControls.ButtonXP buttonXP1;
		internal System.Windows.Forms.TextBox m_txtMane;
		internal PinkieControls.ButtonXP m_BtnRefresh;
		private System.Windows.Forms.Panel panel1;
		internal System.Windows.Forms.Label m_lbWhole;
		internal System.Windows.Forms.Label m_lbBuy;
		internal System.Windows.Forms.Label m_lbSale;
		private System.Windows.Forms.GroupBox groupBox1;
		internal System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label5;
		internal System.Windows.Forms.ComboBox m_cboFindType;
		internal System.Windows.Forms.Label label3;
		internal System.Windows.Forms.Label label4;
		internal System.Windows.Forms.Label label6;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmStorageQuery()
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
		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsControlStorageQuery();
			objController.Set_GUI_Apperance(this);
		}
		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo1 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo2 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo3 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo4 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo5 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo6 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo7 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo8 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo9 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo10 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            this.m_lvMedStockDetail = new System.Windows.Forms.ListView();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.m_cbZero = new System.Windows.Forms.CheckBox();
            this.m_BtnPrint = new PinkieControls.ButtonXP();
            this.m_BtnPrintDetail = new PinkieControls.ButtonXP();
            this.m_BtnExit = new PinkieControls.ButtonXP();
            this.m_dtgMedicineList = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.m_cmdStorage = new System.Windows.Forms.ComboBox();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.m_txtMane = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_BtnRefresh = new PinkieControls.ButtonXP();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_cboFindType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_lbWhole = new System.Windows.Forms.Label();
            this.m_lbSale = new System.Windows.Forms.Label();
            this.m_lbBuy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgMedicineList)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lvMedStockDetail
            // 
            this.m_lvMedStockDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lvMedStockDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lvMedStockDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader16,
            this.columnHeader1,
            this.columnHeader10,
            this.columnHeader2,
            this.columnHeader15,
            this.columnHeader11,
            this.columnHeader3,
            this.columnHeader12,
            this.columnHeader4});
            this.m_lvMedStockDetail.FullRowSelect = true;
            this.m_lvMedStockDetail.GridLines = true;
            this.m_lvMedStockDetail.Location = new System.Drawing.Point(0, 424);
            this.m_lvMedStockDetail.Name = "m_lvMedStockDetail";
            this.m_lvMedStockDetail.Size = new System.Drawing.Size(816, 200);
            this.m_lvMedStockDetail.TabIndex = 1;
            this.m_lvMedStockDetail.UseCompatibleStateImageBehavior = false;
            this.m_lvMedStockDetail.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "系统批次";
            this.columnHeader7.Width = 78;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "药品批号";
            this.columnHeader8.Width = 98;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "失效日期";
            this.columnHeader9.Width = 100;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "数量";
            this.columnHeader16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader16.Width = 58;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "单位";
            this.columnHeader1.Width = 47;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "买入价格";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader10.Width = 79;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "买入总价";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 77;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "加成率";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader15.Width = 62;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "零售价格";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader11.Width = 77;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "零售总价";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 80;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "批发价格";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader12.Width = 77;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "批发总价";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 76;
            // 
            // m_cbZero
            // 
            this.m_cbZero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cbZero.Location = new System.Drawing.Point(8, 40);
            this.m_cbZero.Name = "m_cbZero";
            this.m_cbZero.Size = new System.Drawing.Size(176, 24);
            this.m_cbZero.TabIndex = 6;
            this.m_cbZero.Text = "显示零库存药品批次";
            this.m_cbZero.CheckedChanged += new System.EventHandler(this.m_cbZero_CheckedChanged);
            // 
            // m_BtnPrint
            // 
            this.m_BtnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_BtnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_BtnPrint.DefaultScheme = true;
            this.m_BtnPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_BtnPrint.Hint = "";
            this.m_BtnPrint.Location = new System.Drawing.Point(12, 500);
            this.m_BtnPrint.Name = "m_BtnPrint";
            this.m_BtnPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_BtnPrint.Size = new System.Drawing.Size(168, 32);
            this.m_BtnPrint.TabIndex = 7;
            this.m_BtnPrint.Text = "打  印(&P)";
            this.m_BtnPrint.Click += new System.EventHandler(this.m_BtnPrint_Click);
            // 
            // m_BtnPrintDetail
            // 
            this.m_BtnPrintDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_BtnPrintDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_BtnPrintDetail.DefaultScheme = true;
            this.m_BtnPrintDetail.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_BtnPrintDetail.Hint = "";
            this.m_BtnPrintDetail.Location = new System.Drawing.Point(12, 544);
            this.m_BtnPrintDetail.Name = "m_BtnPrintDetail";
            this.m_BtnPrintDetail.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_BtnPrintDetail.Size = new System.Drawing.Size(168, 32);
            this.m_BtnPrintDetail.TabIndex = 8;
            this.m_BtnPrintDetail.Text = "打印明细(&D)";
            this.m_BtnPrintDetail.Click += new System.EventHandler(this.m_BtnPrintDetail_Click);
            // 
            // m_BtnExit
            // 
            this.m_BtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_BtnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_BtnExit.DefaultScheme = true;
            this.m_BtnExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_BtnExit.Hint = "";
            this.m_BtnExit.Location = new System.Drawing.Point(12, 584);
            this.m_BtnExit.Name = "m_BtnExit";
            this.m_BtnExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_BtnExit.Size = new System.Drawing.Size(168, 32);
            this.m_BtnExit.TabIndex = 9;
            this.m_BtnExit.Text = "退  出(&E)";
            this.m_BtnExit.Click += new System.EventHandler(this.m_BtnExit_Click);
            // 
            // m_dtgMedicineList
            // 
            this.m_dtgMedicineList.AllowAddNew = false;
            this.m_dtgMedicineList.AllowDelete = false;
            this.m_dtgMedicineList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dtgMedicineList.AutoAppendRow = false;
            this.m_dtgMedicineList.AutoScroll = true;
            this.m_dtgMedicineList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.m_dtgMedicineList.CaptionText = "";
            this.m_dtgMedicineList.CaptionVisible = false;
            this.m_dtgMedicineList.CausesValidation = false;
            this.m_dtgMedicineList.ColumnHeadersVisible = true;
            clsColumnInfo1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo1.BackColor = System.Drawing.Color.White;
            clsColumnInfo1.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo1.ColumnIndex = 0;
            clsColumnInfo1.ColumnName = "assistcode_chr";
            clsColumnInfo1.ColumnWidth = 75;
            clsColumnInfo1.Enabled = false;
            clsColumnInfo1.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo1.HeadText = "药品代码";
            clsColumnInfo1.ReadOnly = true;
            clsColumnInfo1.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo2.BackColor = System.Drawing.Color.White;
            clsColumnInfo2.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo2.ColumnIndex = 1;
            clsColumnInfo2.ColumnName = "medicinename_vchr";
            clsColumnInfo2.ColumnWidth = 200;
            clsColumnInfo2.Enabled = false;
            clsColumnInfo2.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo2.HeadText = "药品名称";
            clsColumnInfo2.ReadOnly = true;
            clsColumnInfo2.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo3.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo3.BackColor = System.Drawing.Color.White;
            clsColumnInfo3.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo3.ColumnIndex = 2;
            clsColumnInfo3.ColumnName = "medspec_vchr";
            clsColumnInfo3.ColumnWidth = 200;
            clsColumnInfo3.Enabled = false;
            clsColumnInfo3.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo3.HeadText = "药品规格";
            clsColumnInfo3.ReadOnly = true;
            clsColumnInfo3.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo4.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo4.BackColor = System.Drawing.Color.White;
            clsColumnInfo4.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo4.ColumnIndex = 3;
            clsColumnInfo4.ColumnName = "curqt";
            clsColumnInfo4.ColumnWidth = 75;
            clsColumnInfo4.Enabled = false;
            clsColumnInfo4.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo4.HeadText = "库存数量";
            clsColumnInfo4.ReadOnly = true;
            clsColumnInfo4.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo5.BackColor = System.Drawing.Color.White;
            clsColumnInfo5.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo5.ColumnIndex = 4;
            clsColumnInfo5.ColumnName = "unitid_chr";
            clsColumnInfo5.ColumnWidth = 40;
            clsColumnInfo5.Enabled = false;
            clsColumnInfo5.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo5.HeadText = "单位";
            clsColumnInfo5.ReadOnly = true;
            clsColumnInfo5.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo6.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo6.BackColor = System.Drawing.Color.White;
            clsColumnInfo6.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo6.ColumnIndex = 5;
            clsColumnInfo6.ColumnName = "salemny";
            clsColumnInfo6.ColumnWidth = 75;
            clsColumnInfo6.Enabled = false;
            clsColumnInfo6.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo6.HeadText = "库存金额";
            clsColumnInfo6.ReadOnly = true;
            clsColumnInfo6.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo7.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo7.BackColor = System.Drawing.Color.White;
            clsColumnInfo7.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo7.ColumnIndex = 6;
            clsColumnInfo7.ColumnName = "buymny";
            clsColumnInfo7.ColumnWidth = 75;
            clsColumnInfo7.Enabled = false;
            clsColumnInfo7.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo7.HeadText = "买入金额";
            clsColumnInfo7.ReadOnly = true;
            clsColumnInfo7.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo8.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo8.BackColor = System.Drawing.Color.White;
            clsColumnInfo8.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo8.ColumnIndex = 7;
            clsColumnInfo8.ColumnName = "wholemny";
            clsColumnInfo8.ColumnWidth = 75;
            clsColumnInfo8.Enabled = false;
            clsColumnInfo8.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo8.HeadText = "批发金额";
            clsColumnInfo8.ReadOnly = true;
            clsColumnInfo8.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo9.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo9.BackColor = System.Drawing.Color.White;
            clsColumnInfo9.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo9.ColumnIndex = 8;
            clsColumnInfo9.ColumnName = "medicineid_chr";
            clsColumnInfo9.ColumnWidth = 0;
            clsColumnInfo9.Enabled = true;
            clsColumnInfo9.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo9.HeadText = "medicineid_chr";
            clsColumnInfo9.ReadOnly = false;
            clsColumnInfo9.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo10.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo10.BackColor = System.Drawing.Color.White;
            clsColumnInfo10.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo10.ColumnIndex = 9;
            clsColumnInfo10.ColumnName = "VENDORNAME_VCHR";
            clsColumnInfo10.ColumnWidth = 120;
            clsColumnInfo10.Enabled = false;
            clsColumnInfo10.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo10.HeadText = "供应商";
            clsColumnInfo10.ReadOnly = true;
            clsColumnInfo10.TextFont = new System.Drawing.Font("宋体", 10F);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo1);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo2);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo3);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo4);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo5);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo6);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo7);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo8);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo9);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo10);
            this.m_dtgMedicineList.FullRowSelect = true;
            this.m_dtgMedicineList.Location = new System.Drawing.Point(0, 4);
            this.m_dtgMedicineList.MultiSelect = false;
            this.m_dtgMedicineList.Name = "m_dtgMedicineList";
            this.m_dtgMedicineList.ReadOnly = false;
            this.m_dtgMedicineList.RowHeadersVisible = false;
            this.m_dtgMedicineList.RowHeaderWidth = 35;
            this.m_dtgMedicineList.SelectedRowBackColor = System.Drawing.Color.Purple;
            this.m_dtgMedicineList.SelectedRowForeColor = System.Drawing.Color.White;
            this.m_dtgMedicineList.Size = new System.Drawing.Size(816, 416);
            this.m_dtgMedicineList.TabIndex = 106;
            this.m_dtgMedicineList.m_evtCurrentCellChanged += new System.EventHandler(this.m_dtgMedicineList_m_evtCurrentCellChanged);
            this.m_dtgMedicineList.Load += new System.EventHandler(this.m_dtgMedicineList_Load);
            // 
            // m_cmdStorage
            // 
            this.m_cmdStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmdStorage.Location = new System.Drawing.Point(4, 8);
            this.m_cmdStorage.Name = "m_cmdStorage";
            this.m_cmdStorage.Size = new System.Drawing.Size(172, 24);
            this.m_cmdStorage.TabIndex = 0;
            this.m_cmdStorage.SelectedIndexChanged += new System.EventHandler(this.m_cmdStorage_SelectedIndexChanged);
            // 
            // buttonXP1
            // 
            this.buttonXP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(52, 140);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(100, 36);
            this.buttonXP1.TabIndex = 109;
            this.buttonXP1.Text = "查询(&F)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // m_txtMane
            // 
            this.m_txtMane.Location = new System.Drawing.Point(80, 104);
            this.m_txtMane.Name = "m_txtMane";
            this.m_txtMane.Size = new System.Drawing.Size(96, 26);
            this.m_txtMane.TabIndex = 4;
            this.m_txtMane.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtMane_KeyDown);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "查找内容";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "查找方式";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_BtnRefresh
            // 
            this.m_BtnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_BtnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_BtnRefresh.DefaultScheme = true;
            this.m_BtnRefresh.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_BtnRefresh.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_BtnRefresh.Hint = "";
            this.m_BtnRefresh.Location = new System.Drawing.Point(52, 184);
            this.m_BtnRefresh.Name = "m_BtnRefresh";
            this.m_BtnRefresh.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_BtnRefresh.Size = new System.Drawing.Size(100, 32);
            this.m_BtnRefresh.TabIndex = 108;
            this.m_BtnRefresh.Text = "返回(&R)";
            this.m_BtnRefresh.Click += new System.EventHandler(this.m_BtnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.m_cmdStorage);
            this.panel1.Controls.Add(this.m_cbZero);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.m_BtnPrint);
            this.panel1.Controls.Add(this.m_BtnExit);
            this.panel1.Controls.Add(this.m_BtnPrintDetail);
            this.panel1.Location = new System.Drawing.Point(824, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 628);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_cboFindType);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.buttonXP1);
            this.groupBox2.Controls.Add(this.m_BtnRefresh);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.m_txtMane);
            this.groupBox2.Location = new System.Drawing.Point(4, 268);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 220);
            this.groupBox2.TabIndex = 112;
            this.groupBox2.TabStop = false;
            // 
            // m_cboFindType
            // 
            this.m_cboFindType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboFindType.Items.AddRange(new object[] {
            "药品代码",
            "药品名称",
            "拼音码",
            "五笔码",
            "供应商"});
            this.m_cboFindType.Location = new System.Drawing.Point(80, 48);
            this.m_cboFindType.Name = "m_cboFindType";
            this.m_cboFindType.Size = new System.Drawing.Size(96, 24);
            this.m_cboFindType.TabIndex = 113;
            this.m_cboFindType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboFindType_KeyDown);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.OrangeRed;
            this.label5.Location = new System.Drawing.Point(60, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 23);
            this.label5.TabIndex = 112;
            this.label5.Text = "查找药品";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.m_lbWhole);
            this.groupBox1.Controls.Add(this.m_lbSale);
            this.groupBox1.Controls.Add(this.m_lbBuy);
            this.groupBox1.Location = new System.Drawing.Point(4, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 196);
            this.groupBox1.TabIndex = 111;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(48, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "零售价总金额";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(48, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "买入总金额";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(48, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "库存总金额";
            // 
            // m_lbWhole
            // 
            this.m_lbWhole.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lbWhole.Location = new System.Drawing.Point(12, 161);
            this.m_lbWhole.Name = "m_lbWhole";
            this.m_lbWhole.Size = new System.Drawing.Size(168, 22);
            this.m_lbWhole.TabIndex = 5;
            this.m_lbWhole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lbSale
            // 
            this.m_lbSale.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lbSale.Location = new System.Drawing.Point(12, 45);
            this.m_lbSale.Name = "m_lbSale";
            this.m_lbSale.Size = new System.Drawing.Size(168, 22);
            this.m_lbSale.TabIndex = 3;
            this.m_lbSale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lbBuy
            // 
            this.m_lbBuy.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lbBuy.Location = new System.Drawing.Point(12, 103);
            this.m_lbBuy.Name = "m_lbBuy";
            this.m_lbBuy.Size = new System.Drawing.Size(168, 22);
            this.m_lbBuy.TabIndex = 4;
            this.m_lbBuy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmStorageQuery
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(1016, 633);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_dtgMedicineList);
            this.Controls.Add(this.m_lvMedStockDetail);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmStorageQuery";
            this.Text = "药库库存查询";
            this.Load += new System.EventHandler(this.frmStorageQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgMedicineList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void m_BtnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmStorageQuery_Load(object sender, System.EventArgs e)
		{
			((clsControlStorageQuery)this.objController).m_mthInit();
		}

		private void m_cmdStorage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if( m_cmdStorage.SelectedValue != null)
			{
				((clsControlStorageQuery)this.objController).m_mthInitMedStock();
			}
		}
		

		private void m_BtnSearch_Click(object sender, System.EventArgs e)
		{
			if(this.m_dtgMedicineList.CurrentCell.RowNumber > 0)
			{
				((clsControlStorageQuery)this.objController).m_mthlvMedStorage();
			}
		}

		private void m_dtgMedicineList_m_evtCurrentCellChanged(object sender, System.EventArgs e)
		{
			if(this.m_dtgMedicineList.CurrentCell.RowNumber >= 0)
			{
				((clsControlStorageQuery)this.objController).m_mthlvMedStorage();
			}
		}

		private void m_BtnPrint_Click(object sender, System.EventArgs e)
		{
			//((clsControlStorageQuery)this.objController).m_mthPrintMed(false);
		}

		private void m_BtnPrintDetail_Click(object sender, System.EventArgs e)
		{
			//((clsControlStorageQuery)this.objController).m_mthPrintMed(true);
		}

		private void m_BtnRefresh_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageQuery)this.objController).m_mthInitMedStock();
		}
		public void ShowForm(string strflag)
		{
			if(strflag == "0")
			{
                this.Text = "药库库存查询";
			}
			else
			{
                this.Text = "药房库存查询";
			}
            this.strStorageFlag = strflag;
			this.Show();
		}

		private void m_cbZero_CheckedChanged(object sender, System.EventArgs e)
		{
			((clsControlStorageQuery)this.objController).m_mthInitMedStock();
		}

		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageQuery)this.objController).m_mthFidData();
		}

		private void m_cboFindType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				m_txtMane.Focus();
			}
		}

		private void m_txtMane_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				buttonXP1.Focus();
			}
		}

        private void m_dtgMedicineList_Load(object sender, EventArgs e)
        {

        }
		
	}
}
