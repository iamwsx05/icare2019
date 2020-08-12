using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmStorageLimitMgr 的摘要说明。
	/// </summary>
	public class frmStorageLimitMgr :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		internal com.digitalwave.controls.datagrid.ctlDataGrid DgrLimit;
		private System.Windows.Forms.Panel panel1;
		internal PinkieControls.ButtonXP btnDel;
		internal PinkieControls.ButtonXP btnEse;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		internal System.Windows.Forms.TextBox m_txtPharmName;
		private System.Windows.Forms.Label label7;
		internal System.Windows.Forms.TextBox m_txtUnit;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		internal System.Windows.Forms.TextBox m_txtStockScale;
		internal PinkieControls.ButtonXP m_BtnAdd;
		internal PinkieControls.ButtonXP btnSave;
		internal PinkieControls.ButtonXP m_BtnFind;
		internal com.digitalwave.controls.NumTextBox m_txtMin;
		internal com.digitalwave.controls.NumTextBox m_txtMax;
		internal com.digitalwave.controls.NumTextBox m_txtPlanStock;	
		internal string m_strStorageFlag = "0";
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox m_cmbCondition;
		internal com.digitalwave.iCare.gui.HIS.exComboBox m_cmbStorage;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmStorageLimitMgr()
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
			com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo11 = new com.digitalwave.controls.datagrid.clsColumnInfo();
			com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo12 = new com.digitalwave.controls.datagrid.clsColumnInfo();
			com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo13 = new com.digitalwave.controls.datagrid.clsColumnInfo();
			this.DgrLimit = new com.digitalwave.controls.datagrid.ctlDataGrid();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnEse = new PinkieControls.ButtonXP();
			this.btnDel = new PinkieControls.ButtonXP();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.m_cmbStorage = new com.digitalwave.iCare.gui.HIS.exComboBox();
			this.m_cmbCondition = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.m_txtPharmName = new System.Windows.Forms.TextBox();
			this.m_txtPlanStock = new com.digitalwave.controls.NumTextBox();
			this.m_txtMax = new com.digitalwave.controls.NumTextBox();
			this.m_txtMin = new com.digitalwave.controls.NumTextBox();
			this.m_BtnFind = new PinkieControls.ButtonXP();
			this.m_BtnAdd = new PinkieControls.ButtonXP();
			this.m_txtStockScale = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.m_txtUnit = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnSave = new PinkieControls.ButtonXP();
			((System.ComponentModel.ISupportInitialize)(this.DgrLimit)).BeginInit();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// DgrLimit
			// 
			this.DgrLimit.AllowAddNew = false;
			this.DgrLimit.AllowDelete = false;
			this.DgrLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.DgrLimit.AutoAppendRow = false;
			this.DgrLimit.AutoScroll = true;
			this.DgrLimit.BackgroundColor = System.Drawing.SystemColors.Window;
			this.DgrLimit.CaptionText = "";
			this.DgrLimit.CaptionVisible = false;
			this.DgrLimit.ColumnHeadersVisible = true;
			clsColumnInfo1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo1.BackColor = System.Drawing.Color.White;
			clsColumnInfo1.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo1.ColumnIndex = 0;
			clsColumnInfo1.ColumnName = "assistcode_chr";
			clsColumnInfo1.ColumnWidth = 75;
			clsColumnInfo1.Enabled = false;
			clsColumnInfo1.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo1.HeadText = "助记码";
			clsColumnInfo1.ReadOnly = true;
			clsColumnInfo1.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo2.BackColor = System.Drawing.Color.White;
			clsColumnInfo2.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo2.ColumnIndex = 1;
			clsColumnInfo2.ColumnName = "Medicinename_vchr";
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
			clsColumnInfo3.HeadText = "规格";
			clsColumnInfo3.ReadOnly = true;
			clsColumnInfo3.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo4.BackColor = System.Drawing.Color.White;
			clsColumnInfo4.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo4.ColumnIndex = 0;
			clsColumnInfo4.ColumnName = "PRODUCTORID_CHR";
			clsColumnInfo4.ColumnWidth = 120;
			clsColumnInfo4.Enabled = true;
			clsColumnInfo4.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo4.HeadText = "生产厂家";
			clsColumnInfo4.ReadOnly = false;
			clsColumnInfo4.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo5.BackColor = System.Drawing.Color.White;
			clsColumnInfo5.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo5.ColumnIndex = 3;
			clsColumnInfo5.ColumnName = "unitid_chr";
			clsColumnInfo5.ColumnWidth = 40;
			clsColumnInfo5.Enabled = false;
			clsColumnInfo5.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo5.HeadText = "单位";
			clsColumnInfo5.ReadOnly = true;
			clsColumnInfo5.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo6.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo6.BackColor = System.Drawing.Color.White;
			clsColumnInfo6.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
			clsColumnInfo6.ColumnIndex = 4;
			clsColumnInfo6.ColumnName = "LOWLIMIT_DEC";
			clsColumnInfo6.ColumnWidth = 75;
			clsColumnInfo6.Enabled = true;
			clsColumnInfo6.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo6.HeadText = "库存下限";
			clsColumnInfo6.ReadOnly = false;
			clsColumnInfo6.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo7.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo7.BackColor = System.Drawing.Color.White;
			clsColumnInfo7.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
			clsColumnInfo7.ColumnIndex = 5;
			clsColumnInfo7.ColumnName = "HIGHLIMIT_DEC";
			clsColumnInfo7.ColumnWidth = 75;
			clsColumnInfo7.Enabled = true;
			clsColumnInfo7.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo7.HeadText = "库存上限";
			clsColumnInfo7.ReadOnly = false;
			clsColumnInfo7.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo8.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo8.BackColor = System.Drawing.Color.White;
			clsColumnInfo8.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
			clsColumnInfo8.ColumnIndex = 6;
			clsColumnInfo8.ColumnName = "PLANQTY_DEC";
			clsColumnInfo8.ColumnWidth = 75;
			clsColumnInfo8.Enabled = true;
			clsColumnInfo8.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo8.HeadText = "计划采购量";
			clsColumnInfo8.ReadOnly = false;
			clsColumnInfo8.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo9.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo9.BackColor = System.Drawing.Color.White;
			clsColumnInfo9.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo9.ColumnIndex = 7;
			clsColumnInfo9.ColumnName = "PLANPERCENT_DEC";
			clsColumnInfo9.ColumnWidth = 90;
			clsColumnInfo9.Enabled = false;
			clsColumnInfo9.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo9.HeadText = "采购比例(%)";
			clsColumnInfo9.ReadOnly = true;
			clsColumnInfo9.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo10.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo10.BackColor = System.Drawing.Color.White;
			clsColumnInfo10.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo10.ColumnIndex = 8;
			clsColumnInfo10.ColumnName = "medicineid_chr";
			clsColumnInfo10.ColumnWidth = 0;
			clsColumnInfo10.Enabled = true;
			clsColumnInfo10.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo10.HeadText = "medicineid_chr";
			clsColumnInfo10.ReadOnly = false;
			clsColumnInfo10.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo11.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo11.BackColor = System.Drawing.Color.White;
			clsColumnInfo11.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo11.ColumnIndex = 9;
			clsColumnInfo11.ColumnName = "pycode_chr";
			clsColumnInfo11.ColumnWidth = 0;
			clsColumnInfo11.Enabled = true;
			clsColumnInfo11.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo11.HeadText = "pycode_chr";
			clsColumnInfo11.ReadOnly = false;
			clsColumnInfo11.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo12.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo12.BackColor = System.Drawing.Color.White;
			clsColumnInfo12.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo12.ColumnIndex = 10;
			clsColumnInfo12.ColumnName = "wbcode_chr";
			clsColumnInfo12.ColumnWidth = 0;
			clsColumnInfo12.Enabled = true;
			clsColumnInfo12.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo12.HeadText = "wbcode_chr";
			clsColumnInfo12.ReadOnly = false;
			clsColumnInfo12.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo13.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo13.BackColor = System.Drawing.Color.White;
			clsColumnInfo13.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo13.ColumnIndex = 11;
			clsColumnInfo13.ColumnName = "STORAGEID_CHR";
			clsColumnInfo13.ColumnWidth = 0;
			clsColumnInfo13.Enabled = true;
			clsColumnInfo13.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo13.HeadText = "STORAGEID_CHR";
			clsColumnInfo13.ReadOnly = false;
			clsColumnInfo13.TextFont = new System.Drawing.Font("宋体", 10F);
			this.DgrLimit.Columns.Add(clsColumnInfo1);
			this.DgrLimit.Columns.Add(clsColumnInfo2);
			this.DgrLimit.Columns.Add(clsColumnInfo3);
			this.DgrLimit.Columns.Add(clsColumnInfo4);
			this.DgrLimit.Columns.Add(clsColumnInfo5);
			this.DgrLimit.Columns.Add(clsColumnInfo6);
			this.DgrLimit.Columns.Add(clsColumnInfo7);
			this.DgrLimit.Columns.Add(clsColumnInfo8);
			this.DgrLimit.Columns.Add(clsColumnInfo9);
			this.DgrLimit.Columns.Add(clsColumnInfo10);
			this.DgrLimit.Columns.Add(clsColumnInfo11);
			this.DgrLimit.Columns.Add(clsColumnInfo12);
			this.DgrLimit.Columns.Add(clsColumnInfo13);
			this.DgrLimit.FullRowSelect = false;
			this.DgrLimit.Location = new System.Drawing.Point(0, 0);
			this.DgrLimit.MultiSelect = false;
			this.DgrLimit.Name = "DgrLimit";
			this.DgrLimit.ReadOnly = false;
			this.DgrLimit.RowHeadersVisible = false;
			this.DgrLimit.RowHeaderWidth = 35;
			this.DgrLimit.SelectedRowBackColor = System.Drawing.Color.Purple;
			this.DgrLimit.SelectedRowForeColor = System.Drawing.Color.White;
			this.DgrLimit.Size = new System.Drawing.Size(976, 376);
			this.DgrLimit.TabIndex = 0;
			this.DgrLimit.m_evtDataGridKeyDown += new System.Windows.Forms.KeyEventHandler(this.DgrLimit_m_evtDataGridKeyDown);
			this.DgrLimit.m_evtCurrentCellChanged += new System.EventHandler(this.DgrLimit_m_evtCurrentCellChanged_1);
			this.DgrLimit.m_evtDataGridTextBoxKeyDown += new com.digitalwave.controls.datagrid.clsDGTextKeyEventHandler(this.DgrLimit_m_evtDataGridTextBoxKeyDown);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.DgrLimit);
			this.panel1.Location = new System.Drawing.Point(8, 56);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(976, 376);
			this.panel1.TabIndex = 3;
			// 
			// btnEse
			// 
			this.btnEse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEse.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btnEse.DefaultScheme = true;
			this.btnEse.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnEse.Hint = "";
			this.btnEse.Location = new System.Drawing.Point(704, 16);
			this.btnEse.Name = "btnEse";
			this.btnEse.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btnEse.Size = new System.Drawing.Size(88, 28);
			this.btnEse.TabIndex = 6;
			this.btnEse.Text = "退出(ESC)";
			this.btnEse.Click += new System.EventHandler(this.btnEse_Click);
			// 
			// btnDel
			// 
			this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btnDel.DefaultScheme = true;
			this.btnDel.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnDel.Hint = "";
			this.btnDel.Location = new System.Drawing.Point(520, 16);
			this.btnDel.Name = "btnDel";
			this.btnDel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btnDel.Size = new System.Drawing.Size(88, 28);
			this.btnDel.TabIndex = 5;
			this.btnDel.Text = "删除(F2)";
			this.btnDel.Visible = false;
			this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.m_cmbStorage);
			this.groupBox1.Controls.Add(this.m_cmbCondition);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.m_txtPharmName);
			this.groupBox1.Controls.Add(this.m_txtPlanStock);
			this.groupBox1.Controls.Add(this.m_txtMax);
			this.groupBox1.Controls.Add(this.m_txtMin);
			this.groupBox1.Controls.Add(this.m_BtnFind);
			this.groupBox1.Controls.Add(this.m_BtnAdd);
			this.groupBox1.Controls.Add(this.m_txtStockScale);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.m_txtUnit);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Location = new System.Drawing.Point(8, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(976, 48);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
			// 
			// m_cmbStorage
			// 
			this.m_cmbStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cmbStorage.Location = new System.Drawing.Point(80, 16);
			this.m_cmbStorage.Name = "m_cmbStorage";
			this.m_cmbStorage.Size = new System.Drawing.Size(112, 22);
			this.m_cmbStorage.TabIndex = 0;
			this.m_cmbStorage.SelectedIndexChanged += new System.EventHandler(this.m_cmbStorage_SelectedIndexChanged_1);
			// 
			// m_cmbCondition
			// 
			this.m_cmbCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cmbCondition.Items.AddRange(new object[] {
																"助记码",
																"药品名称",
																"拼音码",
																"五笔码"});
			this.m_cmbCondition.Location = new System.Drawing.Point(272, 16);
			this.m_cmbCondition.Name = "m_cmbCondition";
			this.m_cmbCondition.Size = new System.Drawing.Size(144, 22);
			this.m_cmbCondition.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(208, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 15;
			this.label2.Text = "查询条件：";
			// 
			// m_txtPharmName
			// 
			this.m_txtPharmName.Location = new System.Drawing.Point(496, 16);
			this.m_txtPharmName.Name = "m_txtPharmName";
			this.m_txtPharmName.Size = new System.Drawing.Size(216, 23);
			this.m_txtPharmName.TabIndex = 2;
			this.m_txtPharmName.Text = "";
			this.m_txtPharmName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtPharmName_KeyDown);
			this.m_txtPharmName.Enter += new System.EventHandler(this.m_txtPharmName_Enter);
			// 
			// m_txtPlanStock
			// 
			this.m_txtPlanStock.Location = new System.Drawing.Point(560, 48);
			this.m_txtPlanStock.MaxLength = 10;
			this.m_txtPlanStock.Name = "m_txtPlanStock";
			this.m_txtPlanStock.SendTabKey = false;
			this.m_txtPlanStock.SetFocusColor = System.Drawing.Color.White;
			this.m_txtPlanStock.Size = new System.Drawing.Size(80, 23);
			this.m_txtPlanStock.TabIndex = 4;
			this.m_txtPlanStock.Text = "";
			this.m_txtPlanStock.Leave += new System.EventHandler(this.m_txtPlanStock_Leave);
			// 
			// m_txtMax
			// 
			this.m_txtMax.Location = new System.Drawing.Point(416, 48);
			this.m_txtMax.MaxLength = 10;
			this.m_txtMax.Name = "m_txtMax";
			this.m_txtMax.SendTabKey = false;
			this.m_txtMax.SetFocusColor = System.Drawing.Color.White;
			this.m_txtMax.Size = new System.Drawing.Size(72, 23);
			this.m_txtMax.TabIndex = 3;
			this.m_txtMax.Text = "";
			// 
			// m_txtMin
			// 
			this.m_txtMin.Location = new System.Drawing.Point(272, 48);
			this.m_txtMin.MaxLength = 10;
			this.m_txtMin.Name = "m_txtMin";
			this.m_txtMin.SendTabKey = false;
			this.m_txtMin.SetFocusColor = System.Drawing.Color.White;
			this.m_txtMin.Size = new System.Drawing.Size(80, 23);
			this.m_txtMin.TabIndex = 2;
			this.m_txtMin.Text = "";
			this.m_txtMin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtMin_KeyDown);
			// 
			// m_BtnFind
			// 
			this.m_BtnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.m_BtnFind.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_BtnFind.DefaultScheme = true;
			this.m_BtnFind.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_BtnFind.Hint = "";
			this.m_BtnFind.Location = new System.Drawing.Point(752, 13);
			this.m_BtnFind.Name = "m_BtnFind";
			this.m_BtnFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_BtnFind.Size = new System.Drawing.Size(72, 28);
			this.m_BtnFind.TabIndex = 14;
			this.m_BtnFind.Text = "查找(&F)";
			this.m_BtnFind.Click += new System.EventHandler(this.m_BtnFind_Click);
			// 
			// m_BtnAdd
			// 
			this.m_BtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.m_BtnAdd.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_BtnAdd.DefaultScheme = true;
			this.m_BtnAdd.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_BtnAdd.Hint = "";
			this.m_BtnAdd.Location = new System.Drawing.Point(808, 56);
			this.m_BtnAdd.Name = "m_BtnAdd";
			this.m_BtnAdd.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_BtnAdd.Size = new System.Drawing.Size(72, 28);
			this.m_BtnAdd.TabIndex = 5;
			this.m_BtnAdd.Text = "增加(&A)";
			this.m_BtnAdd.Click += new System.EventHandler(this.m_BtnAdd_Click);
			// 
			// m_txtStockScale
			// 
			this.m_txtStockScale.Enabled = false;
			this.m_txtStockScale.Location = new System.Drawing.Point(704, 48);
			this.m_txtStockScale.Name = "m_txtStockScale";
			this.m_txtStockScale.ReadOnly = true;
			this.m_txtStockScale.Size = new System.Drawing.Size(96, 23);
			this.m_txtStockScale.TabIndex = 5;
			this.m_txtStockScale.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(640, 56);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(80, 16);
			this.label11.TabIndex = 13;
			this.label11.Text = "采购比例：";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(496, 56);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(80, 16);
			this.label10.TabIndex = 12;
			this.label10.Text = "计划采购：";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(352, 56);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80, 16);
			this.label9.TabIndex = 10;
			this.label9.Text = "库存上限：";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(200, 56);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(80, 16);
			this.label8.TabIndex = 8;
			this.label8.Text = "库存下限：";
			// 
			// m_txtUnit
			// 
			this.m_txtUnit.Enabled = false;
			this.m_txtUnit.Location = new System.Drawing.Point(80, 48);
			this.m_txtUnit.Name = "m_txtUnit";
			this.m_txtUnit.ReadOnly = true;
			this.m_txtUnit.Size = new System.Drawing.Size(112, 23);
			this.m_txtUnit.TabIndex = 7;
			this.m_txtUnit.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 56);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(80, 16);
			this.label7.TabIndex = 6;
			this.label7.Text = "单    位：";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "仓库名称：";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(432, 19);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 16);
			this.label6.TabIndex = 2;
			this.label6.Text = "查询内容：";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.btnSave);
			this.groupBox2.Controls.Add(this.btnDel);
			this.groupBox2.Controls.Add(this.btnEse);
			this.groupBox2.Location = new System.Drawing.Point(8, 432);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(976, 56);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btnSave.DefaultScheme = true;
			this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnSave.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.btnSave.Hint = "";
			this.btnSave.Location = new System.Drawing.Point(344, 16);
			this.btnSave.Name = "btnSave";
			this.btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btnSave.Size = new System.Drawing.Size(88, 28);
			this.btnSave.TabIndex = 21;
			this.btnSave.Text = "保存(F1)";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// frmStorageLimitMgr
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(992, 493);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.KeyPreview = true;
			this.Name = "frmStorageLimitMgr";
			this.Text = "药品限额设置";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmStorageLimitMgr_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStorageLimitMgr_Closing);
			this.Load += new System.EventHandler(this.frmStorageLimitMgr_Load);
			((System.ComponentModel.ISupportInitialize)(this.DgrLimit)).EndInit();
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsControlStorageLimitMgr();
			objController.Set_GUI_Apperance(this);
		}

		private void frmStorageLimitMgr_Load(object sender, System.EventArgs e)
		{
		    ((clsControlStorageLimitMgr)this.objController).m_lngResetFrm();
			InitcmbCondition();
			this.DgrLimit.m_mthAddEnterToSpaceColumn(0);
			m_mthChangeFocus(this);
			this.DgrLimit.m_mthAddEnterToSpaceColumn(7);
			this.DgrLimit.m_mthAddEnterToSpaceColumn(5);
			this.DgrLimit.m_mthAddEnterToSpaceColumn(6);
			((com.digitalwave.controls.datagrid.clsColumnInfo)this.DgrLimit.Columns[5]).DataGridTextBoxColumn.TextBox.MaxLength = 10;
			((com.digitalwave.controls.datagrid.clsColumnInfo)this.DgrLimit.Columns[6]).DataGridTextBoxColumn.TextBox.MaxLength = 10;
			((com.digitalwave.controls.datagrid.clsColumnInfo)this.DgrLimit.Columns[5]).DataGridTextBoxColumn.TextBox.KeyPress +=new KeyPressEventHandler(TextBox_KeyPress);
			((com.digitalwave.controls.datagrid.clsColumnInfo)this.DgrLimit.Columns[5]).DataGridTextBoxColumn.TextBox.Enter+=new EventHandler(TextBox_Enter);
			((com.digitalwave.controls.datagrid.clsColumnInfo)this.DgrLimit.Columns[6]).DataGridTextBoxColumn.TextBox.Enter+=new EventHandler(TextBox_Enter);
			((com.digitalwave.controls.datagrid.clsColumnInfo)this.DgrLimit.Columns[7]).DataGridTextBoxColumn.TextBox.Enter+=new EventHandler(TextBox_Enter);
			((com.digitalwave.controls.datagrid.clsColumnInfo)this.DgrLimit.Columns[6]).DataGridTextBoxColumn.TextBox.KeyPress +=new KeyPressEventHandler(TextBox_KeyPress);
			((com.digitalwave.controls.datagrid.clsColumnInfo)this.DgrLimit.Columns[7]).DataGridTextBoxColumn.TextBox.KeyPress +=new KeyPressEventHandler(TextBox_KeyPress);
			((com.digitalwave.controls.datagrid.clsColumnInfo)this.DgrLimit.Columns[6]).DataGridTextBoxColumn.TextBox.Leave+=new EventHandler(TextBox_Leave);

		}	

		private void DgrLimit_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				switch(DgrLimit.CurrentCell.ColumnNumber)
				{
					case 7:
						if(DgrLimit.CurrentCell.RowNumber<DgrLimit.RowCount-1)
						{
							try
							{
								DgrLimit.CurrentCell=new DataGridCell(DgrLimit.CurrentCell.RowNumber,6);
								double dtemp	=double.Parse(DgrLimit[DgrLimit.CurrentCell.RowNumber,7].ToString())/double.Parse(DgrLimit[DgrLimit.CurrentCell.RowNumber,6].ToString()) *100;
								DgrLimit[DgrLimit.CurrentCell.RowNumber,8] = dtemp.ToString("0.00");
							}
							catch
							{
							}
							DgrLimit.CurrentCell=new DataGridCell(DgrLimit.CurrentCell.RowNumber+1,5);
						}
						else
						{
							btnSave.Focus();
						}
						break;
					case 5:
						DgrLimit.CurrentCell=new DataGridCell(DgrLimit.CurrentCell.RowNumber,6);
						break;
					case 6:
						DgrLimit.CurrentCell=new DataGridCell(DgrLimit.CurrentCell.RowNumber,7);
						((clsControlStorageLimitMgr)this.objController).m_lngCheckValue();
						break;
				}
			}
		}

		private void DgrMed_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
		{
			
		}

		private void DgrMed_m_evtDataGridKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageLimitMgr)this.objController).m_lngSave();
		}

		private void btnDel_Click(object sender, System.EventArgs e)
		{
			if(this.DgrLimit.CurrentCell.RowNumber > 0)
			{
				((clsControlStorageLimitMgr)this.objController).m_lngDel();
			}
		}

		private void btnEse_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnfind_Click(object sender, System.EventArgs e)
		{
			
		}

		private void btnRe_Click(object sender, System.EventArgs e)
		{
			
		}

		private void frmStorageLimitMgr_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.F1)
				((clsControlStorageLimitMgr)this.objController).m_lngSave();
			if(e.KeyCode==Keys.F2)
				((clsControlStorageLimitMgr)this.objController).m_lngDel();			
			if(e.KeyCode==Keys.Escape)
				this.Close();
//			if(e.KeyCode==Keys.F5)
//				this.treeViewStorage.Focus();
			if(e.KeyCode==Keys.F6)
				this.DgrLimit.Focus();
//			if(e.KeyCode==Keys.F7)
//				this.txtfind.Focus();
		}

		private void treeViewStorage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.F1)
				((clsControlStorageLimitMgr)this.objController).m_lngSave();
			if(e.KeyCode==Keys.F2)
				((clsControlStorageLimitMgr)this.objController).m_lngDel();
			
			if(e.KeyCode==Keys.Escape)
				this.Close();
//			if(e.KeyCode==Keys.F5)
//				this.treeViewStorage.Focus();
			if(e.KeyCode==Keys.F6)
				this.DgrLimit.Focus();
//			if(e.KeyCode==Keys.F7)
//				this.txtfind.Focus();
		}

		private void txtfind_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.F1)
				((clsControlStorageLimitMgr)this.objController).m_lngSave();
			if(e.KeyCode==Keys.F2)
				((clsControlStorageLimitMgr)this.objController).m_lngDel();
			
			if(e.KeyCode==Keys.Escape)
				this.Close();
//			if(e.KeyCode==Keys.F5)
//				this.treeViewStorage.Focus();
			if(e.KeyCode==Keys.F6)
				this.DgrLimit.Focus();
//			if(e.KeyCode==Keys.F7)
//				this.txtfind.Focus();
		}

		private void DgrLimit_m_evtDataGridKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.F1)
				((clsControlStorageLimitMgr)this.objController).m_lngSave();
			if(e.KeyCode==Keys.F2)
				((clsControlStorageLimitMgr)this.objController).m_lngDel();			
			if(e.KeyCode==Keys.Escape)
				this.Close();			
			if(e.KeyCode==Keys.F6)
				this.DgrLimit.Focus();
		}

		private void DgrLimit_m_evtCurrentCellChanged_1(object sender, System.EventArgs e)
		{
			if(DgrLimit.CurrentCell.ColumnNumber<5)
			{
				DgrLimit.CurrentCell=new DataGridCell(DgrLimit.CurrentCell.RowNumber,5);
			}
		}

		private void DgrMed_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
		{
			
		}

		private void DgrMed_m_evtCurrentCellChanged(object sender, System.EventArgs e)
		{
		}

		private void m_txtPharmName_Enter(object sender, System.EventArgs e)
		{
			this.m_txtPharmName.Tag = null;
		}

		private void controlMedicineFind_Leave(object sender, System.EventArgs e)
		{
	
		}	

		private void m_BtnAdd_Click(object sender, System.EventArgs e)
		{
			double dMin = 0;
			double dMax = 0;
			if(this.m_cmbStorage.SelectedValue == null)
			{
				MessageBox.Show("请选择正确的仓库");
				return;
			}
			if(this.m_txtPharmName.Tag == null)
			{
				MessageBox.Show("请选择正确的药品");
				return;
			}
			if(this.m_txtMin.Text.Trim() == "")
			{
				MessageBox.Show("请输入药品下限");
				this.m_txtMin.Focus();
				return;
				
			}
			else
			{
				try
				{
					dMin = double.Parse(this.m_txtMin.Text.Trim());
				}
				catch
				{
					MessageBox.Show("请输入正确的数字");
					this.m_txtMin.Focus();
					return;
				}
			}
			if(this.m_txtMax.Text.Trim() == "")
			{
				MessageBox.Show("请输入药品上限");
				this.m_txtMax.Focus();
				return;
				
			}
			else
			{
				try
				{
					dMax = double.Parse(this.m_txtMax.Text.Trim());
				}
				catch
				{
					MessageBox.Show("请输入正确的数字");
					this.m_txtMax.Focus();
					return;
				}
			}
			if(dMin >= dMax)
			{
				MessageBox.Show("上限要大于下限");
				this.m_txtMax.Focus();
				return ;
			}
			if(this.m_txtPlanStock.Text.Trim() == "")
			{
				MessageBox.Show("请输入药品计划采购");
				this.m_txtPlanStock.Focus();
				return;
				
			}
			else
			{
				try
				{
					double.Parse(this.m_txtPlanStock.Text.Trim());
				}
				catch
				{
					MessageBox.Show("请输入正确的数字");
					this.m_txtPlanStock.Focus();
					return;
				}
			}			
			if(((clsControlStorageLimitMgr)this.objController).m_lngAddNewLimit() > 0)
			{
				this.m_txtPharmName.Focus();
				m_mthClearText(this);
			}
			
		}
		private void m_mthChangeFocus(System.Windows.Forms.Control obj)
		{
			if(obj.HasChildren)
			{				
				for(int i = 0;i< obj.Controls.Count;i++)
				{
					if(obj.Name == "controlMedicineFind")
					{
						return;
					}
					m_mthChangeFocus(obj.Controls[i]);
				}
			}
			else
			{
				if(obj.Name == "m_txtPharmName" || obj.Name == "m_txtMin")
				{
					return;
				}
				obj.KeyDown +=new KeyEventHandler(obj_KeyDown);
			}
		}

		private void obj_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				SendKeys.Send("{TAB}");
			}
		}
		private void m_mthClearText(System.Windows.Forms.Control obj)
		{
			if(obj.HasChildren)
			{
				for(int i = 0;i< obj.Controls.Count;i++)
				{
					m_mthClearText(obj.Controls[i]);
				}
			}
			else
			{
				if(obj.GetType() == typeof(System.Windows.Forms.TextBox) || obj.GetType() == typeof(com.digitalwave.controls.NumTextBox))
				{
					obj.Text = "";
				}
			}
		}

		private void m_txtPlanStock_Leave(object sender, System.EventArgs e)
		{
			try
			{
				this.m_txtStockScale.Text = ((double)((double.Parse(this.m_txtPlanStock.Text)/double.Parse(this.m_txtMax.Text))*100)).ToString("0.000");
			}
			catch
			{

			}
		}

		private void m_BtnFind_Click(object sender, System.EventArgs e)
		{
			if(this.m_txtPharmName.Text =="")
			{
				return;
			}
			for(int i =0;i< this.DgrLimit.RowCount;i++)
			{
				if(this.DgrLimit.m_objGetRow(i)[this.m_cmbCondition.SelectedValue.ToString().Trim()].ToString().Trim() == this.m_txtPharmName.Text.Trim().ToUpper())
				{					
					this.DgrLimit.CurrentCell = new DataGridCell(i,0);
				}
			}
		}

		private void controlMedicineFind_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				
			}
		}

		private void controlMedicineFind_m_evtReturnVal(object sender, com.digitalwave.controls.clsEvtReturnVal e)
		{
			this.m_txtPharmName.Text = e.ReturnVo.strMEDICINENAME_VCHR;
			this.m_txtPharmName.Tag = e.ReturnVo.strMEDICINEID_CHR;

			this.m_txtUnit.Text = e.ReturnVo.strOPUNIT_CHR;
			this.m_txtMin.Focus();
		}

		private void m_txtPharmName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{

			}
		}

		private void m_txtMin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				this.m_txtMax.Focus();
			}
		}

		private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			string str = e.KeyChar.ToString();
			if(str == "\b")
			{
				return;
			}
			try
			{
				double.Parse((sender as TextBox).Text + e.KeyChar.ToString());
			}
			catch
			{
				e.Handled = true;
			} 	
		}
		public void ShowFrom(string p_strStorageFlag)
		{
			this.m_strStorageFlag = p_strStorageFlag;
			this.Show();
		}

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void frmStorageLimitMgr_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(MessageBox.Show("你确定要退出吗？","系统提示",MessageBoxButtons.YesNo) == DialogResult.No)
			{
				e.Cancel = true;
				return;
			}
			if(((clsControlStorageLimitMgr)this.objController).m_bHasChange())
			{
				if(MessageBox.Show("你修改的数据还未保有存,是否取消关闭?是/否","系统提示",MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					e.Cancel = true;
					return;
				}
			}
			DgrLimit.m_mthDeleteAllRow();
		}
		private void InitcmbCondition()
		{
			System.Data.DataTable dt = new System.Data.DataTable();
			dt.Columns.Add("Name");
			dt.Columns.Add("Value");
			DataRow dr = dt.NewRow();
			dr["Name"] = "助记码";
			dr["Value"] = "assistcode_chr";
			dt.Rows.Add(dr);
			DataRow dr1 = dt.NewRow();
			dr1["Name"]  = "药品名称";
			dr1["Value"] = "Medicinename_vchr";
			dt.Rows.Add(dr1);
			DataRow dr2 = dt.NewRow();
			dr2["Name"]  = "拼音码";
			dr2["Value"] = "pycode_chr";
			dt.Rows.Add(dr2);
			DataRow dr3 = dt.NewRow();
			dr3["Name"] = "五笔码";
			dr3["Value"] = "wbcode_chr";
			dt.Rows.Add(dr3);
			this.m_cmbCondition.DisplayMember = "Name";
			this.m_cmbCondition.ValueMember = "Value";
			this.m_cmbCondition.DataSource = dt;
		}

		private void m_cmbStorage_SelectedIndexChanged_1(object sender, System.EventArgs e)
		{
			((clsControlStorageLimitMgr)this.objController).m_lngShowByStorageID();
		}
		TextBox objTextBox;
		private void TextBox_Enter(object sender, EventArgs e)
		{
			objTextBox=(TextBox)sender;
			objTextBox.BackColor=System.Drawing.Color.DeepSkyBlue;
		}

		private void TextBox_Leave(object sender, EventArgs e)
		{
			
		}
	}
}
