using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmCenterMetStorageSetup 的摘要说明。
	/// </summary>
	public class frmCenterMetStorageSetup : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		com.digitalwave.iCare.gui.HIS.clsControlCenterMetStorageSetup m_objController;
		#region SystemGenerate

        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ToolBarButton m_tbBtnFind;
		internal System.Windows.Forms.ToolBarButton m_tbBtnNot;
		private System.Windows.Forms.ToolBarButton m_tbBtnClose;
		private System.Windows.Forms.ToolBarButton m_tbBtnRefresh;
		internal System.Windows.Forms.ToolBarButton m_tbBtnOK;
		private System.Windows.Forms.ToolBar m_toolBar;
		private System.Windows.Forms.Label label6;
		internal System.Windows.Forms.ComboBox m_CobSele;
		private PinkieControls.ButtonXP btnClose;
		private PinkieControls.ButtonXP m_btnFind;
		internal System.Windows.Forms.TextBox m_txtVal;
		internal System.Windows.Forms.Panel gbFind;
		internal com.digitalwave.iCare.gui.HIS.exComboBox cobSelectType;
		private System.Windows.Forms.Label label5;
        internal com.digitalwave.controls.datagrid.ctlDataGrid m_dtgMedicineList;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmCenterMetStorageSetup()
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
		#endregion

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
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo14 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo15 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo16 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo17 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo18 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo19 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo20 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo21 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo22 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo23 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo24 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo25 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo26 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo27 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCenterMetStorageSetup));
            this.m_tbBtnFind = new System.Windows.Forms.ToolBarButton();
            this.m_tbBtnNot = new System.Windows.Forms.ToolBarButton();
            this.m_tbBtnClose = new System.Windows.Forms.ToolBarButton();
            this.m_tbBtnRefresh = new System.Windows.Forms.ToolBarButton();
            this.m_tbBtnOK = new System.Windows.Forms.ToolBarButton();
            this.m_toolBar = new System.Windows.Forms.ToolBar();
            this.label4 = new System.Windows.Forms.Label();
            this.gbFind = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.m_CobSele = new System.Windows.Forms.ComboBox();
            this.btnClose = new PinkieControls.ButtonXP();
            this.m_btnFind = new PinkieControls.ButtonXP();
            this.m_txtVal = new System.Windows.Forms.TextBox();
            this.cobSelectType = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_dtgMedicineList = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.gbFind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgMedicineList)).BeginInit();
            this.SuspendLayout();
            // 
            // m_tbBtnFind
            // 
            this.m_tbBtnFind.Name = "m_tbBtnFind";
            this.m_tbBtnFind.Text = "查找";
            this.m_tbBtnFind.ToolTipText = "查找";
            // 
            // m_tbBtnNot
            // 
            this.m_tbBtnNot.Name = "m_tbBtnNot";
            this.m_tbBtnNot.Text = "缺药";
            // 
            // m_tbBtnClose
            // 
            this.m_tbBtnClose.Name = "m_tbBtnClose";
            this.m_tbBtnClose.Text = "关闭";
            this.m_tbBtnClose.ToolTipText = "关闭";
            // 
            // m_tbBtnRefresh
            // 
            this.m_tbBtnRefresh.Name = "m_tbBtnRefresh";
            this.m_tbBtnRefresh.Text = "刷新";
            this.m_tbBtnRefresh.ToolTipText = "刷新";
            // 
            // m_tbBtnOK
            // 
            this.m_tbBtnOK.Name = "m_tbBtnOK";
            this.m_tbBtnOK.Text = "有药";
            // 
            // m_toolBar
            // 
            this.m_toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.m_toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.m_tbBtnFind,
            this.m_tbBtnNot,
            this.m_tbBtnOK,
            this.m_tbBtnRefresh,
            this.m_tbBtnClose});
            this.m_toolBar.ButtonSize = new System.Drawing.Size(40, 38);
            this.m_toolBar.DropDownArrows = true;
            this.m_toolBar.Location = new System.Drawing.Point(0, 0);
            this.m_toolBar.Name = "m_toolBar";
            this.m_toolBar.ShowToolTips = true;
            this.m_toolBar.Size = new System.Drawing.Size(1028, 43);
            this.m_toolBar.TabIndex = 103;
            this.m_toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.m_toolBar_ButtonClick);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(820, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 107;
            this.label4.Text = "药品分类";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbFind
            // 
            this.gbFind.Controls.Add(this.label6);
            this.gbFind.Controls.Add(this.m_CobSele);
            this.gbFind.Controls.Add(this.btnClose);
            this.gbFind.Controls.Add(this.m_btnFind);
            this.gbFind.Controls.Add(this.m_txtVal);
            this.gbFind.Location = new System.Drawing.Point(213, 15);
            this.gbFind.Name = "gbFind";
            this.gbFind.Size = new System.Drawing.Size(568, 28);
            this.gbFind.TabIndex = 109;
            this.gbFind.Visible = false;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(17, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 102;
            this.label6.Text = "查找方式";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_CobSele
            // 
            this.m_CobSele.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_CobSele.Items.AddRange(new object[] {
            "药品助记码",
            "药品名称",
            "药品通用名",
            "拼音码",
            "五笔码",
            "英文名称"});
            this.m_CobSele.Location = new System.Drawing.Point(96, 3);
            this.m_CobSele.Name = "m_CobSele";
            this.m_CobSele.Size = new System.Drawing.Size(121, 22);
            this.m_CobSele.TabIndex = 101;
            this.m_CobSele.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_CobSele_KeyDown);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnClose.DefaultScheme = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Hint = "";
            this.btnClose.Location = new System.Drawing.Point(477, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnClose.Size = new System.Drawing.Size(74, 24);
            this.btnClose.TabIndex = 100;
            this.btnClose.Text = "返回(&R)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // m_btnFind
            // 
            this.m_btnFind.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_btnFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnFind.DefaultScheme = true;
            this.m_btnFind.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnFind.Hint = "";
            this.m_btnFind.Location = new System.Drawing.Point(397, 1);
            this.m_btnFind.Name = "m_btnFind";
            this.m_btnFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnFind.Size = new System.Drawing.Size(72, 24);
            this.m_btnFind.TabIndex = 99;
            this.m_btnFind.Text = "查找(&F)";
            this.m_btnFind.Click += new System.EventHandler(this.m_btnFind_Click_1);
            // 
            // m_txtVal
            // 
            this.m_txtVal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_txtVal.Location = new System.Drawing.Point(225, 2);
            this.m_txtVal.MaxLength = 20;
            this.m_txtVal.Name = "m_txtVal";
            this.m_txtVal.Size = new System.Drawing.Size(152, 23);
            this.m_txtVal.TabIndex = 5;
            this.m_txtVal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtVal_KeyDown);
            // 
            // cobSelectType
            // 
            this.cobSelectType.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cobSelectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobSelectType.Location = new System.Drawing.Point(920, 19);
            this.cobSelectType.Name = "cobSelectType";
            this.cobSelectType.Size = new System.Drawing.Size(88, 22);
            this.cobSelectType.TabIndex = 111;
            this.cobSelectType.SelectedIndexChanged += new System.EventHandler(this.cobSelectType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(848, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 21);
            this.label5.TabIndex = 110;
            this.label5.Text = "药品分类";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.m_dtgMedicineList.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_dtgMedicineList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.m_dtgMedicineList.CaptionText = "";
            this.m_dtgMedicineList.CaptionVisible = false;
            this.m_dtgMedicineList.CausesValidation = false;
            this.m_dtgMedicineList.ColumnHeadersVisible = true;
            clsColumnInfo1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo1.BackColor = System.Drawing.Color.White;
            clsColumnInfo1.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo1.ColumnIndex = 0;
            clsColumnInfo1.ColumnName = "助记码";
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
            clsColumnInfo2.ColumnName = "药品名称";
            clsColumnInfo2.ColumnWidth = 140;
            clsColumnInfo2.Enabled = false;
            clsColumnInfo2.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo2.HeadText = "药品名称";
            clsColumnInfo2.ReadOnly = true;
            clsColumnInfo2.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo3.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo3.BackColor = System.Drawing.Color.White;
            clsColumnInfo3.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo3.ColumnIndex = 2;
            clsColumnInfo3.ColumnName = "药品通用名";
            clsColumnInfo3.ColumnWidth = 80;
            clsColumnInfo3.Enabled = false;
            clsColumnInfo3.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo3.HeadText = "药品通用名";
            clsColumnInfo3.ReadOnly = true;
            clsColumnInfo3.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo4.BackColor = System.Drawing.Color.White;
            clsColumnInfo4.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo4.ColumnIndex = 3;
            clsColumnInfo4.ColumnName = "英文名";
            clsColumnInfo4.ColumnWidth = 75;
            clsColumnInfo4.Enabled = false;
            clsColumnInfo4.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo4.HeadText = "英文名";
            clsColumnInfo4.ReadOnly = true;
            clsColumnInfo4.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo5.BackColor = System.Drawing.Color.White;
            clsColumnInfo5.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo5.ColumnIndex = 4;
            clsColumnInfo5.ColumnName = "类别";
            clsColumnInfo5.ColumnWidth = 50;
            clsColumnInfo5.Enabled = false;
            clsColumnInfo5.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo5.HeadText = "类别";
            clsColumnInfo5.ReadOnly = true;
            clsColumnInfo5.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo6.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo6.BackColor = System.Drawing.Color.White;
            clsColumnInfo6.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo6.ColumnIndex = 5;
            clsColumnInfo6.ColumnName = "制剂";
            clsColumnInfo6.ColumnWidth = 40;
            clsColumnInfo6.Enabled = false;
            clsColumnInfo6.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo6.HeadText = "制剂";
            clsColumnInfo6.ReadOnly = true;
            clsColumnInfo6.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo7.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo7.BackColor = System.Drawing.Color.White;
            clsColumnInfo7.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo7.ColumnIndex = 6;
            clsColumnInfo7.ColumnName = "规格";
            clsColumnInfo7.ColumnWidth = 120;
            clsColumnInfo7.Enabled = false;
            clsColumnInfo7.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo7.HeadText = "规格";
            clsColumnInfo7.ReadOnly = true;
            clsColumnInfo7.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo8.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo8.BackColor = System.Drawing.Color.White;
            clsColumnInfo8.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo8.ColumnIndex = 7;
            clsColumnInfo8.ColumnName = "批发价";
            clsColumnInfo8.ColumnWidth = 60;
            clsColumnInfo8.Enabled = false;
            clsColumnInfo8.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo8.HeadText = "批发价";
            clsColumnInfo8.ReadOnly = true;
            clsColumnInfo8.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo9.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo9.BackColor = System.Drawing.Color.White;
            clsColumnInfo9.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo9.ColumnIndex = 8;
            clsColumnInfo9.ColumnName = "单价";
            clsColumnInfo9.ColumnWidth = 50;
            clsColumnInfo9.Enabled = false;
            clsColumnInfo9.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo9.HeadText = "单价";
            clsColumnInfo9.ReadOnly = true;
            clsColumnInfo9.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo10.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo10.BackColor = System.Drawing.Color.White;
            clsColumnInfo10.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo10.ColumnIndex = 9;
            clsColumnInfo10.ColumnName = "生产厂家";
            clsColumnInfo10.ColumnWidth = 80;
            clsColumnInfo10.Enabled = false;
            clsColumnInfo10.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo10.HeadText = "生产厂家";
            clsColumnInfo10.ReadOnly = true;
            clsColumnInfo10.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo11.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo11.BackColor = System.Drawing.Color.White;
            clsColumnInfo11.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo11.ColumnIndex = 10;
            clsColumnInfo11.ColumnName = "剂量";
            clsColumnInfo11.ColumnWidth = 40;
            clsColumnInfo11.Enabled = false;
            clsColumnInfo11.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo11.HeadText = "剂量";
            clsColumnInfo11.ReadOnly = true;
            clsColumnInfo11.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo12.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo12.BackColor = System.Drawing.Color.White;
            clsColumnInfo12.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo12.ColumnIndex = 11;
            clsColumnInfo12.ColumnName = "剂量单位";
            clsColumnInfo12.ColumnWidth = 60;
            clsColumnInfo12.Enabled = false;
            clsColumnInfo12.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo12.HeadText = "剂量单位";
            clsColumnInfo12.ReadOnly = true;
            clsColumnInfo12.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo13.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo13.BackColor = System.Drawing.Color.White;
            clsColumnInfo13.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo13.ColumnIndex = 12;
            clsColumnInfo13.ColumnName = "门诊单位";
            clsColumnInfo13.ColumnWidth = 60;
            clsColumnInfo13.Enabled = false;
            clsColumnInfo13.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo13.HeadText = "门诊单位";
            clsColumnInfo13.ReadOnly = true;
            clsColumnInfo13.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo14.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo14.BackColor = System.Drawing.Color.White;
            clsColumnInfo14.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo14.ColumnIndex = 13;
            clsColumnInfo14.ColumnName = "门诊单价";
            clsColumnInfo14.ColumnWidth = 75;
            clsColumnInfo14.Enabled = false;
            clsColumnInfo14.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo14.HeadText = "门诊单价";
            clsColumnInfo14.ReadOnly = true;
            clsColumnInfo14.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo15.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo15.BackColor = System.Drawing.Color.White;
            clsColumnInfo15.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo15.ColumnIndex = 14;
            clsColumnInfo15.ColumnName = "住院单位";
            clsColumnInfo15.ColumnWidth = 60;
            clsColumnInfo15.Enabled = false;
            clsColumnInfo15.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo15.HeadText = "住院单位";
            clsColumnInfo15.ReadOnly = true;
            clsColumnInfo15.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo16.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo16.BackColor = System.Drawing.Color.White;
            clsColumnInfo16.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo16.ColumnIndex = 15;
            clsColumnInfo16.ColumnName = "住院单价";
            clsColumnInfo16.ColumnWidth = 75;
            clsColumnInfo16.Enabled = false;
            clsColumnInfo16.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo16.HeadText = "住院单价";
            clsColumnInfo16.ReadOnly = true;
            clsColumnInfo16.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo17.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo17.BackColor = System.Drawing.Color.White;
            clsColumnInfo17.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo17.ColumnIndex = 16;
            clsColumnInfo17.ColumnName = "包装量";
            clsColumnInfo17.ColumnWidth = 50;
            clsColumnInfo17.Enabled = false;
            clsColumnInfo17.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo17.HeadText = "包装量";
            clsColumnInfo17.ReadOnly = true;
            clsColumnInfo17.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo18.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo18.BackColor = System.Drawing.Color.White;
            clsColumnInfo18.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo18.ColumnIndex = 17;
            clsColumnInfo18.ColumnName = "缺药";
            clsColumnInfo18.ColumnWidth = 40;
            clsColumnInfo18.Enabled = false;
            clsColumnInfo18.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo18.HeadText = "缺药";
            clsColumnInfo18.ReadOnly = true;
            clsColumnInfo18.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo19.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo19.BackColor = System.Drawing.Color.White;
            clsColumnInfo19.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo19.ColumnIndex = 18;
            clsColumnInfo19.ColumnName = "麻醉药品";
            clsColumnInfo19.ColumnWidth = 0;
            clsColumnInfo19.Enabled = false;
            clsColumnInfo19.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo19.HeadText = "麻醉药品";
            clsColumnInfo19.ReadOnly = true;
            clsColumnInfo19.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo20.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo20.BackColor = System.Drawing.Color.White;
            clsColumnInfo20.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo20.ColumnIndex = 19;
            clsColumnInfo20.ColumnName = "精神药品";
            clsColumnInfo20.ColumnWidth = 0;
            clsColumnInfo20.Enabled = false;
            clsColumnInfo20.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo20.HeadText = "精神药品";
            clsColumnInfo20.ReadOnly = true;
            clsColumnInfo20.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo21.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo21.BackColor = System.Drawing.Color.White;
            clsColumnInfo21.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo21.ColumnIndex = 20;
            clsColumnInfo21.ColumnName = "贵重药品";
            clsColumnInfo21.ColumnWidth = 0;
            clsColumnInfo21.Enabled = false;
            clsColumnInfo21.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo21.HeadText = "贵重药品";
            clsColumnInfo21.ReadOnly = true;
            clsColumnInfo21.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo22.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo22.BackColor = System.Drawing.Color.White;
            clsColumnInfo22.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo22.ColumnIndex = 21;
            clsColumnInfo22.ColumnName = "院内制剂";
            clsColumnInfo22.ColumnWidth = 0;
            clsColumnInfo22.Enabled = false;
            clsColumnInfo22.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo22.HeadText = "院内制剂";
            clsColumnInfo22.ReadOnly = true;
            clsColumnInfo22.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo23.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo23.BackColor = System.Drawing.Color.White;
            clsColumnInfo23.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo23.ColumnIndex = 22;
            clsColumnInfo23.ColumnName = "进口药品";
            clsColumnInfo23.ColumnWidth = 0;
            clsColumnInfo23.Enabled = false;
            clsColumnInfo23.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo23.HeadText = "进口药品";
            clsColumnInfo23.ReadOnly = true;
            clsColumnInfo23.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo24.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo24.BackColor = System.Drawing.Color.White;
            clsColumnInfo24.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo24.ColumnIndex = 23;
            clsColumnInfo24.ColumnName = "自费药品";
            clsColumnInfo24.ColumnWidth = 0;
            clsColumnInfo24.Enabled = false;
            clsColumnInfo24.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo24.HeadText = "自费药品";
            clsColumnInfo24.ReadOnly = true;
            clsColumnInfo24.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo25.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo25.BackColor = System.Drawing.Color.White;
            clsColumnInfo25.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo25.ColumnIndex = 24;
            clsColumnInfo25.ColumnName = "五笔码";
            clsColumnInfo25.ColumnWidth = 0;
            clsColumnInfo25.Enabled = true;
            clsColumnInfo25.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo25.HeadText = "五笔码";
            clsColumnInfo25.ReadOnly = false;
            clsColumnInfo25.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo26.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo26.BackColor = System.Drawing.Color.White;
            clsColumnInfo26.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo26.ColumnIndex = 25;
            clsColumnInfo26.ColumnName = "拼音码";
            clsColumnInfo26.ColumnWidth = 0;
            clsColumnInfo26.Enabled = true;
            clsColumnInfo26.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo26.HeadText = "拼音码";
            clsColumnInfo26.ReadOnly = false;
            clsColumnInfo26.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo27.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo27.BackColor = System.Drawing.Color.White;
            clsColumnInfo27.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo27.ColumnIndex = 26;
            clsColumnInfo27.ColumnName = "MEDICINEID_CHR";
            clsColumnInfo27.ColumnWidth = 0;
            clsColumnInfo27.Enabled = false;
            clsColumnInfo27.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo27.HeadText = "MEDICINEID_CHR";
            clsColumnInfo27.ReadOnly = true;
            clsColumnInfo27.TextFont = new System.Drawing.Font("宋体", 10F);
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
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo11);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo12);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo13);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo14);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo15);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo16);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo17);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo18);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo19);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo20);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo21);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo22);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo23);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo24);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo25);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo26);
            this.m_dtgMedicineList.Columns.Add(clsColumnInfo27);
            this.m_dtgMedicineList.FullRowSelect = false;
            this.m_dtgMedicineList.Location = new System.Drawing.Point(0, 57);
            this.m_dtgMedicineList.MultiSelect = false;
            this.m_dtgMedicineList.Name = "m_dtgMedicineList";
            this.m_dtgMedicineList.ReadOnly = true;
            this.m_dtgMedicineList.RowHeadersVisible = true;
            this.m_dtgMedicineList.RowHeaderWidth = 15;
            this.m_dtgMedicineList.SelectedRowBackColor = System.Drawing.SystemColors.Desktop;
            this.m_dtgMedicineList.SelectedRowForeColor = System.Drawing.Color.White;
            this.m_dtgMedicineList.Size = new System.Drawing.Size(1028, 678);
            this.m_dtgMedicineList.TabIndex = 112;
            this.m_dtgMedicineList.m_evtCurrentCellChanged += new System.EventHandler(this.m_dtgMedicineList_m_evtCurrentCellChanged_1);
            // 
            // frmCenterMetStorageSetup
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1028, 733);
            this.Controls.Add(this.m_dtgMedicineList);
            this.Controls.Add(this.cobSelectType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gbFind);
            this.Controls.Add(this.m_toolBar);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCenterMetStorageSetup";
            this.Text = "中心药房缺药设定";
            this.Load += new System.EventHandler(this.frmCenterMetStorageSetup_Load);
            this.gbFind.ResumeLayout(false);
            this.gbFind.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgMedicineList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region CreateController
		public override void CreateController()
		{
			m_objController = new clsControlCenterMetStorageSetup();
			m_objController.Set_GUI_Apperance(this);
		}
		#endregion

		#region FormLoad
		private void frmCenterMetStorageSetup_Load(object sender, System.EventArgs e)
		{
//			m_objController.m_mthFillMedicineType();
			m_objController.m_mthFromLoad(m_strMedType);
		}
		#endregion

		#region 显示选中的药品类型的药品
		private void m_cboMedicineType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
//			m_objController.m_mthGetSelectedMedicine();
		}
		#endregion
		public string  m_strMedType;


		public void m_mthShowMe(string strMedType)
		{
			m_strMedType=strMedType;
			this.Show();
		}

		#region 工具栏功能按钮事件
		private void m_toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch(this.m_toolBar.Buttons.IndexOf(e.Button))
			{
				case 0:
				{
					gbFind.Visible=true;
					this.m_CobSele.Focus();
					break;
				}
				case 1:
				{
					m_objController.m_mthSetLackMedicineFlag(1);
					break;
				}
				case 2:
				{
					m_objController.m_mthSetLackMedicineFlag(0);
					break;
				}
				case 3:
				{
			      // m_objController.m_mthGetSelectedMedicine();
					break;
				}
				case 4:
				{
					this.Close();
					break;
				}
			}
		}
		#endregion

		#region 改变单元格事件
		private void m_dtgMedicineList_m_evtCurrentCellChanged(object sender, System.EventArgs e)
		{
			m_objController.m_mthTooBarButtonEnableSetUp();
		}
		#endregion

		#region 显示药品详细信息
		private void m_dtgMedicineList_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
		{
            //m_objController.m_mthShowMedicineInfo();
		}
		#endregion

		#region 查找
		private void m_btnFind_Click(object sender, System.EventArgs e)
		{
			
		}
		#endregion

		#region 返回
		private void m_btnClose_Click(object sender, System.EventArgs e)
		{
			m_objController.m_mthReturn();
		}
		#endregion

		private void m_btnFind_Click_1(object sender, System.EventArgs e)
		{
			m_objController.m_lngFind();
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			m_objController.m_mthReturn();
		}

		private void cobSelectType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_objController.m_mthCboSele();
		}

		private void m_txtVal_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				m_objController.m_lngFind();
		}

		private void m_CobSele_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				m_txtVal.Focus();
		}

        private void m_dtgMedicineList_m_evtCurrentCellChanged_1(object sender, EventArgs e)
        {
            m_dtgMedicineList_m_evtCurrentCellChanged(sender,e);
        }
	}
}
