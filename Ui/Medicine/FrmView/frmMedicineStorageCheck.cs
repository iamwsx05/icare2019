using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.ComponentModel.Design;
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmMedicineStorageCheck 的摘要说明。
	/// </summary>
	public class frmMedicineStorageCheck :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		internal System.Windows.Forms.Label label4;
		internal System.Windows.Forms.Label label3;
		internal System.Windows.Forms.Label label1;
		internal System.Windows.Forms.GroupBox groupBox4;
		internal com.digitalwave.controls.datagrid.ctlDataGrid ctlDataGrid1;
		internal System.Windows.Forms.TextBox m_txtRemark;
		internal PinkieControls.ButtonXP m_cmdAddCheckBill;
		internal System.Windows.Forms.Label label5;
		internal com.digitalwave.controls.datagrid.ctlDataGrid m_dtgCheckDetail;
		internal PinkieControls.ButtonXP m_cboAddCheckDetail;
		internal string strStorageFlag = "0";
		private System.ComponentModel.IContainer components;

		private int m_nOldRow=0;
		private int m_ndtgBillRowIndex = -1;
		internal PinkieControls.ButtonXP m_cmbClearDetail;
		internal PinkieControls.ButtonXP m_cmbExit;
		internal System.Windows.Forms.ComboBox m_cboPeriod;
		internal PinkieControls.ButtonXP m_cmdPrint;
		internal PinkieControls.ButtonXP m_cmbPreview;
		internal PinkieControls.ButtonXP m_cmdAudit;
		internal PinkieControls.ButtonXP m_cmdDelCheckBill;
		private System.Windows.Forms.Panel panel1;
		internal System.Windows.Forms.TabControl m_tabAduit;
		private System.Windows.Forms.TabPage m_tbpUnAduit;
		private System.Windows.Forms.TabPage m_tbpEnAduit;
		private System.Windows.Forms.Panel panel2;
		internal NullableDateControls.MaskDateEdit m_dtpCreateDate;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label7;
		internal System.Windows.Forms.Label m_labLostMoney;
		internal System.Windows.Forms.GroupBox groupBox1;
		internal com.digitalwave.controls.datagrid.ctlDataGrid m_DglCheckBill1;
		internal com.digitalwave.controls.datagrid.ctlDataGrid m_DglCheckBill;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		internal System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		internal System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Panel panel4;
		internal System.Windows.Forms.ComboBox comboBox1;
        internal System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel6;
		private com.digitalwave.Utility.Controls.CollapsibleSplitter collapsibleSplitter1;
		private System.Windows.Forms.Panel panel3;
		internal PinkieControls.ButtonXP buttonXP1;
		private System.Windows.Forms.ToolTip toolTip1;
		internal System.Windows.Forms.Label label19;
		internal System.Windows.Forms.Label label20;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Panel panel8;
		internal System.Windows.Forms.TextBox textBox3;
		internal System.Windows.Forms.ComboBox comboBox2;
		internal System.Windows.Forms.Label label18;
		internal System.Windows.Forms.Label label22;
        internal PinkieControls.ButtonXP buttonXP2;
		internal exDataGridSour.exComboBox m_cboStorage;
		internal com.digitalwave.controls.ControlMedicineFind ctlApplMedOut;
		internal PinkieControls.ButtonXP buttonXP4;
        private Label label6;
        private Label label23;
        private Label label21;
        private Label label30;
        private Label label31;
        internal Label label32;
        private Label label27;
        private Label label28;
        internal Label label29;
        private Label label24;
        private Label label25;
        internal Label label26;
		bool m_bSaveFlag = false;

		public frmMedicineStorageCheck()
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
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo28 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo29 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo30 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo31 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo32 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo33 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo34 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo35 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo36 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo37 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo38 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo39 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo40 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo41 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo42 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo43 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo44 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo45 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo46 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo47 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo48 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            this.m_DglCheckBill1 = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.m_txtRemark = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_dtgCheckDetail = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.ctlDataGrid1 = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.m_cboStorage = new exDataGridSour.exComboBox();
            this.m_dtpCreateDate = new NullableDateControls.MaskDateEdit();
            this.m_cmdDelCheckBill = new PinkieControls.ButtonXP();
            this.m_cmdAddCheckBill = new PinkieControls.ButtonXP();
            this.m_cboPeriod = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_cboAddCheckDetail = new PinkieControls.ButtonXP();
            this.m_cmbClearDetail = new PinkieControls.ButtonXP();
            this.m_cmbExit = new PinkieControls.ButtonXP();
            this.m_cmdPrint = new PinkieControls.ButtonXP();
            this.m_cmdAudit = new PinkieControls.ButtonXP();
            this.m_cmbPreview = new PinkieControls.ButtonXP();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_tabAduit = new System.Windows.Forms.TabControl();
            this.m_tbpUnAduit = new System.Windows.Forms.TabPage();
            this.m_DglCheckBill = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.m_tbpEnAduit = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_labLostMoney = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonXP4 = new PinkieControls.ButtonXP();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.buttonXP2 = new PinkieControls.ButtonXP();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ctlApplMedOut = new com.digitalwave.controls.ControlMedicineFind();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.collapsibleSplitter1 = new com.digitalwave.Utility.Controls.CollapsibleSplitter();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.m_DglCheckBill1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgCheckDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_tabAduit.SuspendLayout();
            this.m_tbpUnAduit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_DglCheckBill)).BeginInit();
            this.m_tbpEnAduit.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_DglCheckBill1
            // 
            this.m_DglCheckBill1.AllowAddNew = false;
            this.m_DglCheckBill1.AllowDelete = false;
            this.m_DglCheckBill1.AutoAppendRow = false;
            this.m_DglCheckBill1.AutoScroll = true;
            this.m_DglCheckBill1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.m_DglCheckBill1.CaptionText = "";
            this.m_DglCheckBill1.CaptionVisible = false;
            this.m_DglCheckBill1.ColumnHeadersVisible = true;
            clsColumnInfo1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo1.BackColor = System.Drawing.Color.White;
            clsColumnInfo1.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo1.ColumnIndex = 0;
            clsColumnInfo1.ColumnName = "statue";
            clsColumnInfo1.ColumnWidth = 0;
            clsColumnInfo1.Enabled = true;
            clsColumnInfo1.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo1.HeadText = "statue";
            clsColumnInfo1.ReadOnly = false;
            clsColumnInfo1.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo2.BackColor = System.Drawing.Color.White;
            clsColumnInfo2.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo2.ColumnIndex = 1;
            clsColumnInfo2.ColumnName = "STORAGECHECKID_CHR";
            clsColumnInfo2.ColumnWidth = 100;
            clsColumnInfo2.Enabled = false;
            clsColumnInfo2.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo2.HeadText = "盘点单号";
            clsColumnInfo2.ReadOnly = false;
            clsColumnInfo2.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo3.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo3.BackColor = System.Drawing.Color.White;
            clsColumnInfo3.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo3.ColumnIndex = 2;
            clsColumnInfo3.ColumnName = "CREATORID_CHR";
            clsColumnInfo3.ColumnWidth = 0;
            clsColumnInfo3.Enabled = false;
            clsColumnInfo3.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo3.HeadText = "盘点人";
            clsColumnInfo3.ReadOnly = false;
            clsColumnInfo3.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo4.BackColor = System.Drawing.Color.White;
            clsColumnInfo4.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo4.ColumnIndex = 3;
            clsColumnInfo4.ColumnName = "CREATEDATE_DAT";
            clsColumnInfo4.ColumnWidth = 0;
            clsColumnInfo4.Enabled = false;
            clsColumnInfo4.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo4.HeadText = "盘点时间";
            clsColumnInfo4.ReadOnly = false;
            clsColumnInfo4.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo5.BackColor = System.Drawing.Color.White;
            clsColumnInfo5.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo5.ColumnIndex = 4;
            clsColumnInfo5.ColumnName = "ADUITEMP_CHR";
            clsColumnInfo5.ColumnWidth = 90;
            clsColumnInfo5.Enabled = false;
            clsColumnInfo5.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo5.HeadText = "审核人";
            clsColumnInfo5.ReadOnly = false;
            clsColumnInfo5.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo6.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo6.BackColor = System.Drawing.Color.White;
            clsColumnInfo6.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo6.ColumnIndex = 5;
            clsColumnInfo6.ColumnName = "ADUITDATE_DAT";
            clsColumnInfo6.ColumnWidth = 130;
            clsColumnInfo6.Enabled = false;
            clsColumnInfo6.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo6.HeadText = "审核时间";
            clsColumnInfo6.ReadOnly = true;
            clsColumnInfo6.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo7.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo7.BackColor = System.Drawing.Color.White;
            clsColumnInfo7.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo7.ColumnIndex = 6;
            clsColumnInfo7.ColumnName = "PSTATUS_INT";
            clsColumnInfo7.ColumnWidth = 0;
            clsColumnInfo7.Enabled = false;
            clsColumnInfo7.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo7.HeadText = "审核状态";
            clsColumnInfo7.ReadOnly = false;
            clsColumnInfo7.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo8.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo8.BackColor = System.Drawing.Color.White;
            clsColumnInfo8.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo8.ColumnIndex = 7;
            clsColumnInfo8.ColumnName = "REMARK_VCHR";
            clsColumnInfo8.ColumnWidth = 200;
            clsColumnInfo8.Enabled = false;
            clsColumnInfo8.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo8.HeadText = "备注";
            clsColumnInfo8.ReadOnly = true;
            clsColumnInfo8.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo9.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo9.BackColor = System.Drawing.Color.White;
            clsColumnInfo9.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo9.ColumnIndex = 0;
            clsColumnInfo9.ColumnName = "CHECK_DAT";
            clsColumnInfo9.ColumnWidth = 0;
            clsColumnInfo9.Enabled = false;
            clsColumnInfo9.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo9.HeadText = "CHECK_DAT";
            clsColumnInfo9.ReadOnly = true;
            clsColumnInfo9.TextFont = new System.Drawing.Font("宋体", 10F);
            this.m_DglCheckBill1.Columns.Add(clsColumnInfo1);
            this.m_DglCheckBill1.Columns.Add(clsColumnInfo2);
            this.m_DglCheckBill1.Columns.Add(clsColumnInfo3);
            this.m_DglCheckBill1.Columns.Add(clsColumnInfo4);
            this.m_DglCheckBill1.Columns.Add(clsColumnInfo5);
            this.m_DglCheckBill1.Columns.Add(clsColumnInfo6);
            this.m_DglCheckBill1.Columns.Add(clsColumnInfo7);
            this.m_DglCheckBill1.Columns.Add(clsColumnInfo8);
            this.m_DglCheckBill1.Columns.Add(clsColumnInfo9);
            this.m_DglCheckBill1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DglCheckBill1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_DglCheckBill1.FullRowSelect = true;
            this.m_DglCheckBill1.Location = new System.Drawing.Point(0, 0);
            this.m_DglCheckBill1.MultiSelect = false;
            this.m_DglCheckBill1.Name = "m_DglCheckBill1";
            this.m_DglCheckBill1.ReadOnly = false;
            this.m_DglCheckBill1.RowHeadersVisible = false;
            this.m_DglCheckBill1.RowHeaderWidth = 35;
            this.m_DglCheckBill1.SelectedRowBackColor = System.Drawing.SystemColors.Desktop;
            this.m_DglCheckBill1.SelectedRowForeColor = System.Drawing.Color.White;
            this.m_DglCheckBill1.Size = new System.Drawing.Size(524, 139);
            this.m_DglCheckBill1.TabIndex = 51;
            this.m_DglCheckBill1.m_evtCurrentCellChanged += new System.EventHandler(this.m_DglCheckBill_m_evtCurrentCellChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 40;
            this.label4.Text = "备注：";
            // 
            // m_txtRemark
            // 
            this.m_txtRemark.Location = new System.Drawing.Point(56, 48);
            this.m_txtRemark.MaxLength = 20;
            this.m_txtRemark.Name = "m_txtRemark";
            this.m_txtRemark.Size = new System.Drawing.Size(312, 23);
            this.m_txtRemark.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(168, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 38;
            this.label3.Text = "盘点时间：";
            // 
            // m_dtgCheckDetail
            // 
            this.m_dtgCheckDetail.AllowAddNew = false;
            this.m_dtgCheckDetail.AllowDelete = false;
            this.m_dtgCheckDetail.AutoAppendRow = false;
            this.m_dtgCheckDetail.AutoScroll = true;
            this.m_dtgCheckDetail.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.m_dtgCheckDetail.BackgroundColor = System.Drawing.SystemColors.Window;
            this.m_dtgCheckDetail.CaptionText = "";
            this.m_dtgCheckDetail.CaptionVisible = false;
            this.m_dtgCheckDetail.ColumnHeadersVisible = true;
            clsColumnInfo10.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo10.BackColor = System.Drawing.Color.White;
            clsColumnInfo10.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo10.ColumnIndex = 0;
            clsColumnInfo10.ColumnName = "ASSISTCODE_CHR";
            clsColumnInfo10.ColumnWidth = 75;
            clsColumnInfo10.Enabled = false;
            clsColumnInfo10.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo10.HeadText = "助记码";
            clsColumnInfo10.ReadOnly = true;
            clsColumnInfo10.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo11.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo11.BackColor = System.Drawing.Color.White;
            clsColumnInfo11.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo11.ColumnIndex = 1;
            clsColumnInfo11.ColumnName = "medicinename_vchr";
            clsColumnInfo11.ColumnWidth = 130;
            clsColumnInfo11.Enabled = false;
            clsColumnInfo11.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo11.HeadText = "药品名称";
            clsColumnInfo11.ReadOnly = false;
            clsColumnInfo11.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo12.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo12.BackColor = System.Drawing.Color.White;
            clsColumnInfo12.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo12.ColumnIndex = 2;
            clsColumnInfo12.ColumnName = "medspec_vchr";
            clsColumnInfo12.ColumnWidth = 150;
            clsColumnInfo12.Enabled = false;
            clsColumnInfo12.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo12.HeadText = "规格";
            clsColumnInfo12.ReadOnly = false;
            clsColumnInfo12.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo13.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo13.BackColor = System.Drawing.Color.White;
            clsColumnInfo13.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo13.ColumnIndex = 3;
            clsColumnInfo13.ColumnName = "unitname_chr";
            clsColumnInfo13.ColumnWidth = 40;
            clsColumnInfo13.Enabled = false;
            clsColumnInfo13.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo13.HeadText = "单位";
            clsColumnInfo13.ReadOnly = false;
            clsColumnInfo13.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo14.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo14.BackColor = System.Drawing.Color.White;
            clsColumnInfo14.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo14.ColumnIndex = 4;
            clsColumnInfo14.ColumnName = "medicinnumber";
            clsColumnInfo14.ColumnWidth = 70;
            clsColumnInfo14.Enabled = false;
            clsColumnInfo14.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo14.HeadText = "药品批号";
            clsColumnInfo14.ReadOnly = false;
            clsColumnInfo14.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo15.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo15.BackColor = System.Drawing.Color.White;
            clsColumnInfo15.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo15.ColumnIndex = 5;
            clsColumnInfo15.ColumnName = "buyunitprice_mny";
            clsColumnInfo15.ColumnWidth = 60;
            clsColumnInfo15.Enabled = false;
            clsColumnInfo15.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo15.HeadText = "进货价";
            clsColumnInfo15.ReadOnly = false;
            clsColumnInfo15.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo16.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo16.BackColor = System.Drawing.Color.White;
            clsColumnInfo16.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo16.ColumnIndex = 6;
            clsColumnInfo16.ColumnName = "UNITPRICE_MNY";
            clsColumnInfo16.ColumnWidth = 60;
            clsColumnInfo16.Enabled = false;
            clsColumnInfo16.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo16.HeadText = "零售价";
            clsColumnInfo16.ReadOnly = true;
            clsColumnInfo16.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo17.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo17.BackColor = System.Drawing.Color.White;
            clsColumnInfo17.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo17.ColumnIndex = 7;
            clsColumnInfo17.ColumnName = "curqty_dec";
            clsColumnInfo17.ColumnWidth = 70;
            clsColumnInfo17.Enabled = false;
            clsColumnInfo17.ForeColor = System.Drawing.Color.Orange;
            clsColumnInfo17.HeadText = "库存数量";
            clsColumnInfo17.ReadOnly = true;
            clsColumnInfo17.TextFont = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            clsColumnInfo18.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo18.BackColor = System.Drawing.Color.White;
            clsColumnInfo18.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo18.ColumnIndex = 8;
            clsColumnInfo18.ColumnName = "CALCMoney";
            clsColumnInfo18.ColumnWidth = 90;
            clsColumnInfo18.Enabled = false;
            clsColumnInfo18.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo18.HeadText = "库存进货金额";
            clsColumnInfo18.ReadOnly = true;
            clsColumnInfo18.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo19.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo19.BackColor = System.Drawing.Color.White;
            clsColumnInfo19.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Int;
            clsColumnInfo19.ColumnIndex = 9;
            clsColumnInfo19.ColumnName = "REALQTY_DEC";
            clsColumnInfo19.ColumnWidth = 70;
            clsColumnInfo19.Enabled = true;
            clsColumnInfo19.ForeColor = System.Drawing.Color.Orange;
            clsColumnInfo19.HeadText = "实盘数量";
            clsColumnInfo19.ReadOnly = false;
            clsColumnInfo19.TextFont = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            clsColumnInfo20.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo20.BackColor = System.Drawing.Color.White;
            clsColumnInfo20.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo20.ColumnIndex = 10;
            clsColumnInfo20.ColumnName = "REALMoney";
            clsColumnInfo20.ColumnWidth = 90;
            clsColumnInfo20.Enabled = false;
            clsColumnInfo20.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo20.HeadText = "盘点进价";
            clsColumnInfo20.ReadOnly = true;
            clsColumnInfo20.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo21.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo21.BackColor = System.Drawing.Color.White;
            clsColumnInfo21.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo21.ColumnIndex = 11;
            clsColumnInfo21.ColumnName = "lostNum";
            clsColumnInfo21.ColumnWidth = 70;
            clsColumnInfo21.Enabled = false;
            clsColumnInfo21.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo21.HeadText = "盈亏数量";
            clsColumnInfo21.ReadOnly = true;
            clsColumnInfo21.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo22.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo22.BackColor = System.Drawing.Color.White;
            clsColumnInfo22.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo22.ColumnIndex = 12;
            clsColumnInfo22.ColumnName = "lostMoney";
            clsColumnInfo22.ColumnWidth = 90;
            clsColumnInfo22.Enabled = false;
            clsColumnInfo22.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo22.HeadText = "进价盈亏";
            clsColumnInfo22.ReadOnly = true;
            clsColumnInfo22.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo23.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo23.BackColor = System.Drawing.Color.White;
            clsColumnInfo23.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo23.ColumnIndex = 13;
            clsColumnInfo23.ColumnName = "lostSalmoney";
            clsColumnInfo23.ColumnWidth = 75;
            clsColumnInfo23.Enabled = false;
            clsColumnInfo23.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo23.HeadText = "零价盈亏";
            clsColumnInfo23.ReadOnly = true;
            clsColumnInfo23.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo24.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo24.BackColor = System.Drawing.Color.White;
            clsColumnInfo24.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo24.ColumnIndex = 14;
            clsColumnInfo24.ColumnName = "checkreason_vchr";
            clsColumnInfo24.ColumnWidth = 120;
            clsColumnInfo24.Enabled = true;
            clsColumnInfo24.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo24.HeadText = "盈亏原因";
            clsColumnInfo24.ReadOnly = false;
            clsColumnInfo24.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo25.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo25.BackColor = System.Drawing.Color.White;
            clsColumnInfo25.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo25.ColumnIndex = 15;
            clsColumnInfo25.ColumnName = "medicinetypeid_chr";
            clsColumnInfo25.ColumnWidth = 0;
            clsColumnInfo25.Enabled = true;
            clsColumnInfo25.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo25.HeadText = "medicinetypeid_chr";
            clsColumnInfo25.ReadOnly = false;
            clsColumnInfo25.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo26.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo26.BackColor = System.Drawing.Color.White;
            clsColumnInfo26.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo26.ColumnIndex = 16;
            clsColumnInfo26.ColumnName = "medicineid_chr";
            clsColumnInfo26.ColumnWidth = 0;
            clsColumnInfo26.Enabled = false;
            clsColumnInfo26.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo26.HeadText = "药品代码";
            clsColumnInfo26.ReadOnly = false;
            clsColumnInfo26.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo27.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo27.BackColor = System.Drawing.Color.White;
            clsColumnInfo27.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo27.ColumnIndex = 17;
            clsColumnInfo27.ColumnName = "pycode_chr";
            clsColumnInfo27.ColumnWidth = 0;
            clsColumnInfo27.Enabled = true;
            clsColumnInfo27.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo27.HeadText = "pycode_chr";
            clsColumnInfo27.ReadOnly = false;
            clsColumnInfo27.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo28.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo28.BackColor = System.Drawing.Color.White;
            clsColumnInfo28.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo28.ColumnIndex = 18;
            clsColumnInfo28.ColumnName = "wbcode_chr";
            clsColumnInfo28.ColumnWidth = 0;
            clsColumnInfo28.Enabled = true;
            clsColumnInfo28.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo28.HeadText = "wbcode_chr";
            clsColumnInfo28.ReadOnly = false;
            clsColumnInfo28.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo29.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo29.BackColor = System.Drawing.Color.White;
            clsColumnInfo29.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo29.ColumnIndex = 19;
            clsColumnInfo29.ColumnName = "PHARMAID_CHR";
            clsColumnInfo29.ColumnWidth = 0;
            clsColumnInfo29.Enabled = false;
            clsColumnInfo29.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo29.HeadText = "PHARMAID_CHR";
            clsColumnInfo29.ReadOnly = true;
            clsColumnInfo29.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo30.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo30.BackColor = System.Drawing.Color.White;
            clsColumnInfo30.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo30.ColumnIndex = 20;
            clsColumnInfo30.ColumnName = "PARENTID_CHR";
            clsColumnInfo30.ColumnWidth = 0;
            clsColumnInfo30.Enabled = false;
            clsColumnInfo30.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo30.HeadText = "PARENTID_CHR";
            clsColumnInfo30.ReadOnly = true;
            clsColumnInfo30.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo31.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo31.BackColor = System.Drawing.Color.White;
            clsColumnInfo31.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo31.ColumnIndex = 21;
            clsColumnInfo31.ColumnName = "MEDICINEPREPTYPE_CHR";
            clsColumnInfo31.ColumnWidth = 0;
            clsColumnInfo31.Enabled = false;
            clsColumnInfo31.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo31.HeadText = "MEDICINEPREPTYPE_CHR";
            clsColumnInfo31.ReadOnly = true;
            clsColumnInfo31.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo32.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo32.BackColor = System.Drawing.Color.White;
            clsColumnInfo32.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo32.ColumnIndex = 22;
            clsColumnInfo32.ColumnName = "父类";
            clsColumnInfo32.ColumnWidth = 0;
            clsColumnInfo32.Enabled = false;
            clsColumnInfo32.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo32.HeadText = "父类";
            clsColumnInfo32.ReadOnly = true;
            clsColumnInfo32.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo33.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo33.BackColor = System.Drawing.Color.White;
            clsColumnInfo33.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo33.ColumnIndex = 23;
            clsColumnInfo33.ColumnName = "分类";
            clsColumnInfo33.ColumnWidth = 0;
            clsColumnInfo33.Enabled = false;
            clsColumnInfo33.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo33.HeadText = "分类";
            clsColumnInfo33.ReadOnly = true;
            clsColumnInfo33.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo34.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo34.BackColor = System.Drawing.Color.White;
            clsColumnInfo34.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo34.ColumnIndex = 24;
            clsColumnInfo34.ColumnName = "itemid_chr";
            clsColumnInfo34.ColumnWidth = 0;
            clsColumnInfo34.Enabled = false;
            clsColumnInfo34.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo34.HeadText = "itemid_chr";
            clsColumnInfo34.ReadOnly = true;
            clsColumnInfo34.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo35.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo35.BackColor = System.Drawing.Color.White;
            clsColumnInfo35.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo35.ColumnIndex = 25;
            clsColumnInfo35.ColumnName = "SYSLOTNO_CHR";
            clsColumnInfo35.ColumnWidth = 75;
            clsColumnInfo35.Enabled = false;
            clsColumnInfo35.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo35.HeadText = "系统批号";
            clsColumnInfo35.ReadOnly = true;
            clsColumnInfo35.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo36.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo36.BackColor = System.Drawing.Color.White;
            clsColumnInfo36.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo36.ColumnIndex = 26;
            clsColumnInfo36.ColumnName = "usefullife";
            clsColumnInfo36.ColumnWidth = 0;
            clsColumnInfo36.Enabled = false;
            clsColumnInfo36.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo36.HeadText = "usefullife";
            clsColumnInfo36.ReadOnly = true;
            clsColumnInfo36.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo37.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo37.BackColor = System.Drawing.Color.White;
            clsColumnInfo37.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo37.ColumnIndex = 27;
            clsColumnInfo37.ColumnName = "productorid_chr";
            clsColumnInfo37.ColumnWidth = 0;
            clsColumnInfo37.Enabled = false;
            clsColumnInfo37.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo37.HeadText = "productorid_chr";
            clsColumnInfo37.ReadOnly = true;
            clsColumnInfo37.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo38.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo38.BackColor = System.Drawing.Color.White;
            clsColumnInfo38.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo38.ColumnIndex = 28;
            clsColumnInfo38.ColumnName = "wholesaleunitprice_mny";
            clsColumnInfo38.ColumnWidth = 0;
            clsColumnInfo38.Enabled = false;
            clsColumnInfo38.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo38.HeadText = "wholesaleunitprice_mny";
            clsColumnInfo38.ReadOnly = true;
            clsColumnInfo38.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo39.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo39.BackColor = System.Drawing.Color.White;
            clsColumnInfo39.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo39.ColumnIndex = 29;
            clsColumnInfo39.ColumnName = "storageid_chr";
            clsColumnInfo39.ColumnWidth = 0;
            clsColumnInfo39.Enabled = false;
            clsColumnInfo39.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo39.HeadText = "storageid_chr";
            clsColumnInfo39.ReadOnly = true;
            clsColumnInfo39.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo40.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo40.BackColor = System.Drawing.Color.White;
            clsColumnInfo40.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo40.ColumnIndex = 30;
            clsColumnInfo40.ColumnName = "unitid_chr";
            clsColumnInfo40.ColumnWidth = 0;
            clsColumnInfo40.Enabled = false;
            clsColumnInfo40.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo40.HeadText = "unitid_chr";
            clsColumnInfo40.ReadOnly = true;
            clsColumnInfo40.TextFont = new System.Drawing.Font("宋体", 10F);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo10);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo11);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo12);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo13);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo14);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo15);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo16);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo17);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo18);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo19);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo20);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo21);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo22);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo23);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo24);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo25);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo26);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo27);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo28);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo29);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo30);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo31);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo32);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo33);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo34);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo35);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo36);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo37);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo38);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo39);
            this.m_dtgCheckDetail.Columns.Add(clsColumnInfo40);
            this.m_dtgCheckDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dtgCheckDetail.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtgCheckDetail.FullRowSelect = false;
            this.m_dtgCheckDetail.Location = new System.Drawing.Point(0, 0);
            this.m_dtgCheckDetail.MultiSelect = false;
            this.m_dtgCheckDetail.Name = "m_dtgCheckDetail";
            this.m_dtgCheckDetail.ReadOnly = false;
            this.m_dtgCheckDetail.RowHeadersVisible = false;
            this.m_dtgCheckDetail.RowHeaderWidth = 35;
            this.m_dtgCheckDetail.SelectedRowBackColor = System.Drawing.SystemColors.Desktop;
            this.m_dtgCheckDetail.SelectedRowForeColor = System.Drawing.Color.White;
            this.m_dtgCheckDetail.Size = new System.Drawing.Size(868, 513);
            this.m_dtgCheckDetail.TabIndex = 51;
            this.m_dtgCheckDetail.m_evtCurrentCellChanged += new System.EventHandler(this.m_dtgCheckDetail_m_evtCurrentCellChanged);
            this.m_dtgCheckDetail.Enter += new System.EventHandler(this.m_dtgCheckDetail_Enter);
            this.m_dtgCheckDetail.m_evtDataGridTextBoxKeyDown += new com.digitalwave.controls.datagrid.clsDGTextKeyEventHandler(this.m_dtgCheckDetail_m_evtDataGridTextBoxKeyDown);
            // 
            // ctlDataGrid1
            // 
            this.ctlDataGrid1.AllowAddNew = true;
            this.ctlDataGrid1.AllowDelete = true;
            this.ctlDataGrid1.AutoAppendRow = true;
            this.ctlDataGrid1.AutoScroll = true;
            this.ctlDataGrid1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ctlDataGrid1.CaptionText = "";
            this.ctlDataGrid1.CaptionVisible = false;
            this.ctlDataGrid1.ColumnHeadersVisible = true;
            this.ctlDataGrid1.FullRowSelect = false;
            this.ctlDataGrid1.Location = new System.Drawing.Point(0, 0);
            this.ctlDataGrid1.MultiSelect = false;
            this.ctlDataGrid1.Name = "ctlDataGrid1";
            this.ctlDataGrid1.ReadOnly = false;
            this.ctlDataGrid1.RowHeadersVisible = true;
            this.ctlDataGrid1.RowHeaderWidth = 35;
            this.ctlDataGrid1.SelectedRowBackColor = System.Drawing.Color.Purple;
            this.ctlDataGrid1.SelectedRowForeColor = System.Drawing.Color.White;
            this.ctlDataGrid1.Size = new System.Drawing.Size(188, 108);
            this.ctlDataGrid1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 34;
            this.label1.Text = "仓库：";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.m_cboStorage);
            this.groupBox4.Controls.Add(this.m_txtRemark);
            this.groupBox4.Controls.Add(this.m_dtpCreateDate);
            this.groupBox4.Controls.Add(this.m_cmdDelCheckBill);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.m_cmdAddCheckBill);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(4, -4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(480, 76);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // m_cboStorage
            // 
            this.m_cboStorage.Location = new System.Drawing.Point(56, 13);
            this.m_cboStorage.Name = "m_cboStorage";
            this.m_cboStorage.Size = new System.Drawing.Size(112, 22);
            this.m_cboStorage.TabIndex = 47;
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.Enabled = false;
            this.m_dtpCreateDate.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.m_dtpCreateDate.Location = new System.Drawing.Point(248, 16);
            this.m_dtpCreateDate.Mask = "yyyy年MM月dd日";
            this.m_dtpCreateDate.Name = "m_dtpCreateDate";
            this.m_dtpCreateDate.Size = new System.Drawing.Size(120, 23);
            this.m_dtpCreateDate.TabIndex = 3;
            this.m_dtpCreateDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpCreateDate_KeyDown);
            // 
            // m_cmdDelCheckBill
            // 
            this.m_cmdDelCheckBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdDelCheckBill.DefaultScheme = true;
            this.m_cmdDelCheckBill.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdDelCheckBill.Hint = "";
            this.m_cmdDelCheckBill.Location = new System.Drawing.Point(376, 40);
            this.m_cmdDelCheckBill.Name = "m_cmdDelCheckBill";
            this.m_cmdDelCheckBill.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelCheckBill.Size = new System.Drawing.Size(96, 30);
            this.m_cmdDelCheckBill.TabIndex = 46;
            this.m_cmdDelCheckBill.Text = "删除(&D)";
            this.m_cmdDelCheckBill.Click += new System.EventHandler(this.m_cmdDelCheckBill_Click);
            // 
            // m_cmdAddCheckBill
            // 
            this.m_cmdAddCheckBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdAddCheckBill.DefaultScheme = true;
            this.m_cmdAddCheckBill.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdAddCheckBill.Hint = "";
            this.m_cmdAddCheckBill.Location = new System.Drawing.Point(376, 10);
            this.m_cmdAddCheckBill.Name = "m_cmdAddCheckBill";
            this.m_cmdAddCheckBill.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdAddCheckBill.Size = new System.Drawing.Size(96, 30);
            this.m_cmdAddCheckBill.TabIndex = 7;
            this.m_cmdAddCheckBill.Text = "增加(&A)";
            this.m_cmdAddCheckBill.Click += new System.EventHandler(this.m_cmdAddCheckBill_Click);
            // 
            // m_cboPeriod
            // 
            this.m_cboPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cboPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboPeriod.Location = new System.Drawing.Point(324, 140);
            this.m_cboPeriod.Name = "m_cboPeriod";
            this.m_cboPeriod.Size = new System.Drawing.Size(196, 22);
            this.m_cboPeriod.TabIndex = 2;
            this.m_cboPeriod.SelectedIndexChanged += new System.EventHandler(this.m_cboPeriod_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(260, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 45;
            this.label5.Text = "账务日期：";
            // 
            // m_cboAddCheckDetail
            // 
            this.m_cboAddCheckDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cboAddCheckDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cboAddCheckDetail.DefaultScheme = true;
            this.m_cboAddCheckDetail.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cboAddCheckDetail.Hint = "";
            this.m_cboAddCheckDetail.Location = new System.Drawing.Point(8, 12);
            this.m_cboAddCheckDetail.Name = "m_cboAddCheckDetail";
            this.m_cboAddCheckDetail.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cboAddCheckDetail.Size = new System.Drawing.Size(80, 32);
            this.m_cboAddCheckDetail.TabIndex = 10;
            this.m_cboAddCheckDetail.Text = "保存(&S)";
            this.m_cboAddCheckDetail.Click += new System.EventHandler(this.m_cboAddCheckDetail_Click);
            // 
            // m_cmbClearDetail
            // 
            this.m_cmbClearDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cmbClearDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmbClearDetail.DefaultScheme = true;
            this.m_cmbClearDetail.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmbClearDetail.Hint = "";
            this.m_cmbClearDetail.Location = new System.Drawing.Point(96, 12);
            this.m_cmbClearDetail.Name = "m_cmbClearDetail";
            this.m_cmbClearDetail.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmbClearDetail.Size = new System.Drawing.Size(80, 32);
            this.m_cmbClearDetail.TabIndex = 11;
            this.m_cmbClearDetail.Text = "恢复(&C)";
            this.m_cmbClearDetail.Click += new System.EventHandler(this.m_cmbClearDetail_Click);
            // 
            // m_cmbExit
            // 
            this.m_cmbExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cmbExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmbExit.DefaultScheme = true;
            this.m_cmbExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmbExit.Hint = "";
            this.m_cmbExit.Location = new System.Drawing.Point(184, 44);
            this.m_cmbExit.Name = "m_cmbExit";
            this.m_cmbExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmbExit.Size = new System.Drawing.Size(80, 32);
            this.m_cmbExit.TabIndex = 12;
            this.m_cmbExit.Text = "退出(&E)";
            this.m_cmbExit.Click += new System.EventHandler(this.m_cmbExit_Click);
            // 
            // m_cmdPrint
            // 
            this.m_cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cmdPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdPrint.DefaultScheme = true;
            this.m_cmdPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdPrint.Hint = "";
            this.m_cmdPrint.Location = new System.Drawing.Point(8, 44);
            this.m_cmdPrint.Name = "m_cmdPrint";
            this.m_cmdPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdPrint.Size = new System.Drawing.Size(80, 32);
            this.m_cmdPrint.TabIndex = 52;
            this.m_cmdPrint.Text = "合并(&U)";
            this.m_cmdPrint.Click += new System.EventHandler(this.m_cmdPrint_Click);
            // 
            // m_cmdAudit
            // 
            this.m_cmdAudit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cmdAudit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdAudit.DefaultScheme = true;
            this.m_cmdAudit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdAudit.Hint = "";
            this.m_cmdAudit.Location = new System.Drawing.Point(96, 44);
            this.m_cmdAudit.Name = "m_cmdAudit";
            this.m_cmdAudit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdAudit.Size = new System.Drawing.Size(80, 32);
            this.m_cmdAudit.TabIndex = 53;
            this.m_cmdAudit.Text = "审核";
            this.m_cmdAudit.Click += new System.EventHandler(this.m_cmdAudit_Click);
            // 
            // m_cmbPreview
            // 
            this.m_cmbPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cmbPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmbPreview.DefaultScheme = true;
            this.m_cmbPreview.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmbPreview.Hint = "";
            this.m_cmbPreview.Location = new System.Drawing.Point(184, 12);
            this.m_cmbPreview.Name = "m_cmbPreview";
            this.m_cmbPreview.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmbPreview.Size = new System.Drawing.Size(80, 32);
            this.m_cmbPreview.TabIndex = 54;
            this.m_cmbPreview.Text = "预览(&R)";
            this.m_cmbPreview.Click += new System.EventHandler(this.m_cmbPreview_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.m_cboPeriod);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.m_tabAduit);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(524, 164);
            this.panel1.TabIndex = 59;
            // 
            // m_tabAduit
            // 
            this.m_tabAduit.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.m_tabAduit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tabAduit.Controls.Add(this.m_tbpUnAduit);
            this.m_tabAduit.Controls.Add(this.m_tbpEnAduit);
            this.m_tabAduit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_tabAduit.ItemSize = new System.Drawing.Size(0, 17);
            this.m_tabAduit.Location = new System.Drawing.Point(-2, -4);
            this.m_tabAduit.Multiline = true;
            this.m_tabAduit.Name = "m_tabAduit";
            this.m_tabAduit.SelectedIndex = 0;
            this.m_tabAduit.Size = new System.Drawing.Size(532, 164);
            this.m_tabAduit.TabIndex = 60;
            this.m_tabAduit.TabStop = false;
            this.m_tabAduit.SelectedIndexChanged += new System.EventHandler(this.m_tabAduit_SelectedIndexChanged);
            // 
            // m_tbpUnAduit
            // 
            this.m_tbpUnAduit.Controls.Add(this.m_DglCheckBill);
            this.m_tbpUnAduit.Location = new System.Drawing.Point(4, 4);
            this.m_tbpUnAduit.Name = "m_tbpUnAduit";
            this.m_tbpUnAduit.Size = new System.Drawing.Size(524, 139);
            this.m_tbpUnAduit.TabIndex = 0;
            this.m_tbpUnAduit.Text = "未审核";
            // 
            // m_DglCheckBill
            // 
            this.m_DglCheckBill.AllowAddNew = false;
            this.m_DglCheckBill.AllowDelete = false;
            this.m_DglCheckBill.AutoAppendRow = false;
            this.m_DglCheckBill.AutoScroll = true;
            this.m_DglCheckBill.BackgroundColor = System.Drawing.SystemColors.Window;
            this.m_DglCheckBill.CaptionText = "";
            this.m_DglCheckBill.CaptionVisible = false;
            this.m_DglCheckBill.ColumnHeadersVisible = true;
            clsColumnInfo41.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo41.BackColor = System.Drawing.Color.White;
            clsColumnInfo41.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo41.ColumnIndex = 0;
            clsColumnInfo41.ColumnName = "statue";
            clsColumnInfo41.ColumnWidth = 0;
            clsColumnInfo41.Enabled = true;
            clsColumnInfo41.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo41.HeadText = "statue";
            clsColumnInfo41.ReadOnly = false;
            clsColumnInfo41.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo42.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo42.BackColor = System.Drawing.Color.White;
            clsColumnInfo42.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo42.ColumnIndex = 1;
            clsColumnInfo42.ColumnName = "STORAGECHECKID_CHR";
            clsColumnInfo42.ColumnWidth = 100;
            clsColumnInfo42.Enabled = false;
            clsColumnInfo42.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo42.HeadText = "盘点单号";
            clsColumnInfo42.ReadOnly = false;
            clsColumnInfo42.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo43.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo43.BackColor = System.Drawing.Color.White;
            clsColumnInfo43.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo43.ColumnIndex = 2;
            clsColumnInfo43.ColumnName = "CREATORID_CHR";
            clsColumnInfo43.ColumnWidth = 90;
            clsColumnInfo43.Enabled = false;
            clsColumnInfo43.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo43.HeadText = "盘点人";
            clsColumnInfo43.ReadOnly = false;
            clsColumnInfo43.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo44.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo44.BackColor = System.Drawing.Color.White;
            clsColumnInfo44.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo44.ColumnIndex = 3;
            clsColumnInfo44.ColumnName = "CREATEDATE_DAT";
            clsColumnInfo44.ColumnWidth = 130;
            clsColumnInfo44.Enabled = false;
            clsColumnInfo44.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo44.HeadText = "盘点时间";
            clsColumnInfo44.ReadOnly = false;
            clsColumnInfo44.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo45.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo45.BackColor = System.Drawing.Color.White;
            clsColumnInfo45.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo45.ColumnIndex = 4;
            clsColumnInfo45.ColumnName = "ADUITEMP_CHR";
            clsColumnInfo45.ColumnWidth = 0;
            clsColumnInfo45.Enabled = false;
            clsColumnInfo45.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo45.HeadText = "审核人";
            clsColumnInfo45.ReadOnly = false;
            clsColumnInfo45.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo46.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo46.BackColor = System.Drawing.Color.White;
            clsColumnInfo46.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo46.ColumnIndex = 5;
            clsColumnInfo46.ColumnName = "ADUITDATE_DAT";
            clsColumnInfo46.ColumnWidth = 0;
            clsColumnInfo46.Enabled = false;
            clsColumnInfo46.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo46.HeadText = "审核时间";
            clsColumnInfo46.ReadOnly = true;
            clsColumnInfo46.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo47.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo47.BackColor = System.Drawing.Color.White;
            clsColumnInfo47.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo47.ColumnIndex = 6;
            clsColumnInfo47.ColumnName = "PSTATUS_INT";
            clsColumnInfo47.ColumnWidth = 0;
            clsColumnInfo47.Enabled = false;
            clsColumnInfo47.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo47.HeadText = "审核状态";
            clsColumnInfo47.ReadOnly = false;
            clsColumnInfo47.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo48.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo48.BackColor = System.Drawing.Color.White;
            clsColumnInfo48.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo48.ColumnIndex = 7;
            clsColumnInfo48.ColumnName = "REMARK_VCHR";
            clsColumnInfo48.ColumnWidth = 200;
            clsColumnInfo48.Enabled = false;
            clsColumnInfo48.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo48.HeadText = "备注";
            clsColumnInfo48.ReadOnly = true;
            clsColumnInfo48.TextFont = new System.Drawing.Font("宋体", 10F);
            this.m_DglCheckBill.Columns.Add(clsColumnInfo41);
            this.m_DglCheckBill.Columns.Add(clsColumnInfo42);
            this.m_DglCheckBill.Columns.Add(clsColumnInfo43);
            this.m_DglCheckBill.Columns.Add(clsColumnInfo44);
            this.m_DglCheckBill.Columns.Add(clsColumnInfo45);
            this.m_DglCheckBill.Columns.Add(clsColumnInfo46);
            this.m_DglCheckBill.Columns.Add(clsColumnInfo47);
            this.m_DglCheckBill.Columns.Add(clsColumnInfo48);
            this.m_DglCheckBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DglCheckBill.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_DglCheckBill.FullRowSelect = true;
            this.m_DglCheckBill.Location = new System.Drawing.Point(0, 0);
            this.m_DglCheckBill.MultiSelect = false;
            this.m_DglCheckBill.Name = "m_DglCheckBill";
            this.m_DglCheckBill.ReadOnly = false;
            this.m_DglCheckBill.RowHeadersVisible = false;
            this.m_DglCheckBill.RowHeaderWidth = 35;
            this.m_DglCheckBill.SelectedRowBackColor = System.Drawing.SystemColors.Desktop;
            this.m_DglCheckBill.SelectedRowForeColor = System.Drawing.Color.White;
            this.m_DglCheckBill.Size = new System.Drawing.Size(524, 139);
            this.m_DglCheckBill.TabIndex = 52;
            this.m_DglCheckBill.m_evtCurrentCellChanged += new System.EventHandler(this.m_DglCheckBill_m_evtCurrentCellChanged_1);
            // 
            // m_tbpEnAduit
            // 
            this.m_tbpEnAduit.Controls.Add(this.m_DglCheckBill1);
            this.m_tbpEnAduit.Location = new System.Drawing.Point(4, 4);
            this.m_tbpEnAduit.Name = "m_tbpEnAduit";
            this.m_tbpEnAduit.Size = new System.Drawing.Size(524, 139);
            this.m_tbpEnAduit.TabIndex = 1;
            this.m_tbpEnAduit.Text = "已审核";
            this.m_tbpEnAduit.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label30);
            this.panel2.Controls.Add(this.label31);
            this.panel2.Controls.Add(this.label32);
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.label28);
            this.panel2.Controls.Add(this.label29);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.label25);
            this.panel2.Controls.Add(this.label26);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Location = new System.Drawing.Point(536, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(488, 164);
            this.panel2.TabIndex = 0;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.Peru;
            this.label30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label30.Location = new System.Drawing.Point(75, 147);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(103, 1);
            this.label30.TabIndex = 82;
            this.label30.Text = "label30";
            // 
            // label31
            // 
            this.label31.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label31.Location = new System.Drawing.Point(3, 132);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(72, 16);
            this.label31.TabIndex = 81;
            this.label31.Text = "零价盈亏:";
            // 
            // label32
            // 
            this.label32.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label32.ForeColor = System.Drawing.Color.Red;
            this.label32.Location = new System.Drawing.Point(75, 131);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(103, 19);
            this.label32.TabIndex = 80;
            this.label32.Text = "0";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.Peru;
            this.label27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label27.Location = new System.Drawing.Point(76, 119);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(103, 1);
            this.label27.TabIndex = 79;
            this.label27.Text = "label27";
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label28.Location = new System.Drawing.Point(4, 104);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(72, 16);
            this.label28.TabIndex = 78;
            this.label28.Text = "盘点零价:";
            // 
            // label29
            // 
            this.label29.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.ForeColor = System.Drawing.Color.Red;
            this.label29.Location = new System.Drawing.Point(76, 103);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(103, 19);
            this.label29.TabIndex = 77;
            this.label29.Text = "0";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Peru;
            this.label24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label24.Location = new System.Drawing.Point(75, 91);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(103, 1);
            this.label24.TabIndex = 76;
            this.label24.Text = "label24";
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label25.Location = new System.Drawing.Point(3, 76);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(72, 16);
            this.label25.TabIndex = 75;
            this.label25.Text = "库存零价:";
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(75, 75);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(103, 19);
            this.label26.TabIndex = 74;
            this.label26.Text = "0";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label15.Location = new System.Drawing.Point(184, 104);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(16, 23);
            this.label15.TabIndex = 70;
            this.label15.Text = "元";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label12.Location = new System.Drawing.Point(184, 76);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 23);
            this.label12.TabIndex = 66;
            this.label12.Text = "元";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label11.Location = new System.Drawing.Point(184, 132);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 23);
            this.label11.TabIndex = 61;
            this.label11.Text = "元";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_cmbPreview);
            this.groupBox1.Controls.Add(this.m_cboAddCheckDetail);
            this.groupBox1.Controls.Add(this.m_cmbClearDetail);
            this.groupBox1.Controls.Add(this.m_cmdPrint);
            this.groupBox1.Controls.Add(this.m_cmbExit);
            this.groupBox1.Controls.Add(this.m_cmdAudit);
            this.groupBox1.Location = new System.Drawing.Point(212, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 80);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Peru;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Location = new System.Drawing.Point(73, 416);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(73, 1);
            this.label17.TabIndex = 73;
            this.label17.Text = "label17";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Peru;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Location = new System.Drawing.Point(73, 439);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(75, 1);
            this.label16.TabIndex = 71;
            this.label16.Text = "label16";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label13.Location = new System.Drawing.Point(1, 423);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 18);
            this.label13.TabIndex = 69;
            this.label13.Text = "盘点进价:";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(73, 423);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 19);
            this.label14.TabIndex = 68;
            this.label14.Text = "0";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Peru;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Location = new System.Drawing.Point(73, 439);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 1);
            this.label8.TabIndex = 67;
            this.label8.Text = "label8";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label9.Location = new System.Drawing.Point(1, 401);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 65;
            this.label9.Text = "库存进价:";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(73, 400);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 19);
            this.label10.TabIndex = 64;
            this.label10.Text = "0";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Peru;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Location = new System.Drawing.Point(73, 463);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 1);
            this.label7.TabIndex = 62;
            this.label7.Text = "label7";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label2.Location = new System.Drawing.Point(1, 448);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 60;
            this.label2.Text = "进价盈亏:";
            // 
            // m_labLostMoney
            // 
            this.m_labLostMoney.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_labLostMoney.ForeColor = System.Drawing.Color.Red;
            this.m_labLostMoney.Location = new System.Drawing.Point(73, 447);
            this.m_labLostMoney.Name = "m_labLostMoney";
            this.m_labLostMoney.Size = new System.Drawing.Size(75, 19);
            this.m_labLostMoney.TabIndex = 59;
            this.m_labLostMoney.Text = "0";
            this.m_labLostMoney.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.m_labLostMoney);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(152, 513);
            this.panel4.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.label23);
            this.panel7.Controls.Add(this.textBox1);
            this.panel7.Controls.Add(this.comboBox1);
            this.panel7.Controls.Add(this.label19);
            this.panel7.Controls.Add(this.label20);
            this.panel7.Controls.Add(this.buttonXP1);
            this.panel7.Location = new System.Drawing.Point(4, 207);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(144, 189);
            this.panel7.TabIndex = 60;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.ForeColor = System.Drawing.Color.Goldenrod;
            this.label23.Location = new System.Drawing.Point(23, 164);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(97, 14);
            this.label23.TabIndex = 62;
            this.label23.Text = "直接定位查找";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 92);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(124, 23);
            this.textBox1.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Items.AddRange(new object[] {
            "药品助记码",
            "药品名称",
            "拼音码",
            "五笔码"});
            this.comboBox1.Location = new System.Drawing.Point(8, 36);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(125, 22);
            this.comboBox1.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(8, 12);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(81, 16);
            this.label19.TabIndex = 47;
            this.label19.Text = "查找方式：";
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(8, 68);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(96, 16);
            this.label20.TabIndex = 59;
            this.label20.Text = "查找内容：";
            // 
            // buttonXP1
            // 
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(20, 120);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(104, 32);
            this.buttonXP1.TabIndex = 57;
            this.buttonXP1.Text = "查找(&F)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.label21);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Controls.Add(this.buttonXP4);
            this.panel8.Controls.Add(this.textBox3);
            this.panel8.Controls.Add(this.comboBox2);
            this.panel8.Controls.Add(this.label18);
            this.panel8.Controls.Add(this.label22);
            this.panel8.Controls.Add(this.buttonXP2);
            this.panel8.Location = new System.Drawing.Point(4, 4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(144, 195);
            this.panel8.TabIndex = 60;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.ForeColor = System.Drawing.Color.Goldenrod;
            this.label21.Location = new System.Drawing.Point(13, 173);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(127, 14);
            this.label21.TabIndex = 61;
            this.label21.Text = "用‘＋’号分隔。";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Goldenrod;
            this.label6.Location = new System.Drawing.Point(3, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 14);
            this.label6.TabIndex = 61;
            this.label6.Text = "组合盘点，每个条件";
            // 
            // buttonXP4
            // 
            this.buttonXP4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP4.DefaultScheme = true;
            this.buttonXP4.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP4.Hint = "";
            this.buttonXP4.Location = new System.Drawing.Point(72, 116);
            this.buttonXP4.Name = "buttonXP4";
            this.buttonXP4.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP4.Size = new System.Drawing.Size(60, 32);
            this.buttonXP4.TabIndex = 65;
            this.buttonXP4.Text = "返回(&R)";
            this.buttonXP4.Click += new System.EventHandler(this.buttonXP4_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(10, 88);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(124, 23);
            this.textBox3.TabIndex = 61;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Items.AddRange(new object[] {
            "药品代码",
            "药品名称",
            "拼音码",
            "五笔码"});
            this.comboBox2.Location = new System.Drawing.Point(10, 32);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(125, 22);
            this.comboBox2.TabIndex = 60;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(10, 8);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(96, 16);
            this.label18.TabIndex = 62;
            this.label18.Text = "查找方式：";
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(10, 64);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(96, 16);
            this.label22.TabIndex = 64;
            this.label22.Text = "查找内容：";
            // 
            // buttonXP2
            // 
            this.buttonXP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP2.DefaultScheme = true;
            this.buttonXP2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP2.Hint = "";
            this.buttonXP2.Location = new System.Drawing.Point(8, 116);
            this.buttonXP2.Name = "buttonXP2";
            this.buttonXP2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP2.Size = new System.Drawing.Size(60, 32);
            this.buttonXP2.TabIndex = 63;
            this.buttonXP2.Text = "查找(&K)";
            this.buttonXP2.Click += new System.EventHandler(this.buttonXP2_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ctlApplMedOut);
            this.panel5.Controls.Add(this.panel1);
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1028, 172);
            this.panel5.TabIndex = 61;
            // 
            // ctlApplMedOut
            // 
            this.ctlApplMedOut.blIsMedStorage = true;
            this.ctlApplMedOut.blISOutStorage = true;
            this.ctlApplMedOut.blRepertory = true;
            this.ctlApplMedOut.FindMedmode = 0;
            this.ctlApplMedOut.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctlApplMedOut.intIsReData = 0;
            this.ctlApplMedOut.isApplMebMod = null;
            this.ctlApplMedOut.isApplModel = true;
            this.ctlApplMedOut.isShowFindType = true;
            this.ctlApplMedOut.IsShowZero = true;
            this.ctlApplMedOut.Location = new System.Drawing.Point(184, -336);
            this.ctlApplMedOut.Name = "ctlApplMedOut";
            this.ctlApplMedOut.Size = new System.Drawing.Size(840, 336);
            this.ctlApplMedOut.status = 0;
            this.ctlApplMedOut.strMedstorage = null;
            this.ctlApplMedOut.strSTORAGEID = "-1";
            this.ctlApplMedOut.TabIndex = 60;
            this.ctlApplMedOut.e_evtReturnMedStoreOutVal += new com.digitalwave.controls.dlgReturnMedStoreOutVal(this.ctlApplMedOut_e_evtReturnMedStoreOutVal);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel3);
            this.panel6.Controls.Add(this.collapsibleSplitter1);
            this.panel6.Controls.Add(this.panel4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 172);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1028, 513);
            this.panel6.TabIndex = 62;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_dtgCheckDetail);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(160, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(868, 513);
            this.panel3.TabIndex = 62;
            // 
            // collapsibleSplitter1
            // 
            this.collapsibleSplitter1.AnimationDelay = 20;
            this.collapsibleSplitter1.AnimationStep = 20;
            this.collapsibleSplitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter1.ControlToHide = this.panel4;
            this.collapsibleSplitter1.ExpandParentForm = true;
            this.collapsibleSplitter1.Location = new System.Drawing.Point(152, 0);
            this.collapsibleSplitter1.MinExtra = 152;
            this.collapsibleSplitter1.MinSize = 0;
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.Size = new System.Drawing.Size(8, 513);
            this.collapsibleSplitter1.TabIndex = 61;
            this.collapsibleSplitter1.TabStop = false;
            this.toolTip1.SetToolTip(this.collapsibleSplitter1, "显示/隐藏查找药品");
            this.collapsibleSplitter1.UseAnimations = true;
            this.collapsibleSplitter1.VisualStyle = com.digitalwave.Utility.Controls.VisualStyles.XP;
            this.collapsibleSplitter1.Click += new System.EventHandler(this.collapsibleSplitter1_Click);
            this.collapsibleSplitter1.MouseEnter += new System.EventHandler(this.collapsibleSplitter1_MouseEnter);
            // 
            // frmMedicineStorageCheck
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1028, 685);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.Name = "frmMedicineStorageCheck";
            this.Text = "药品盘点";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMedicineStorageCheck_Closing);
            this.Load += new System.EventHandler(this.frmMedicineStorageCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_DglCheckBill1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgCheckDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.m_tabAduit.ResumeLayout(false);
            this.m_tbpUnAduit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_DglCheckBill)).EndInit();
            this.m_tbpEnAduit.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			this.objController = new clsControlMedicineStorageCheck();
			this.objController.Set_GUI_Apperance(this);
		}


		private void frmMedicineStorageCheck_Load(object sender, System.EventArgs e)
		{
			this.panel4.Width=0;
			this.m_dtgCheckDetail.m_mthAddEnterToSpaceColumn(14);
			this.m_dtgCheckDetail.m_mthAddEnterToSpaceColumn(9);
			this.m_dtgCheckDetail.m_mthAddEnterToSpaceColumn(0);
			((clsControlMedicineStorageCheck)this.objController).m_mthInitForm();
			((com.digitalwave.controls.datagrid.clsColumnInfo)(this.m_dtgCheckDetail.Columns[9])).DataGridTextBoxColumn.TextBox.MaxLength = 6;
			((com.digitalwave.controls.datagrid.clsColumnInfo)(this.m_dtgCheckDetail.Columns[9])).DataGridTextBoxColumn.TextBox.KeyPress +=new KeyPressEventHandler(TextBox_KeyPress);
			((com.digitalwave.controls.datagrid.clsColumnInfo)(this.m_dtgCheckDetail.Columns[9])).DataGridTextBoxColumn.TextBox.Leave+=new EventHandler(TextBox_Leave);
			((com.digitalwave.controls.datagrid.clsColumnInfo)(this.m_dtgCheckDetail.Columns[9])).DataGridTextBoxColumn.TextBox.Enter+=new EventHandler(TextBox_Enter);
			((com.digitalwave.controls.datagrid.clsColumnInfo)(this.m_dtgCheckDetail.Columns[14])).DataGridTextBoxColumn.TextBox.Leave+=new EventHandler(TextBox_Leave);
			((com.digitalwave.controls.datagrid.clsColumnInfo)(this.m_dtgCheckDetail.Columns[14])).DataGridTextBoxColumn.TextBox.Enter+=new EventHandler(TextBox_Enter);
			((com.digitalwave.controls.datagrid.clsColumnInfo)(this.m_dtgCheckDetail.Columns[0])).DataGridTextBoxColumn.TextBox.Enter+=new EventHandler(TextBox_Enter);
			CreateFunction(this);
			this.m_cboStorage.Focus();
		}

		private void m_cboStorage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(m_bSaveFlag)
			{
				if(MessageBox.Show("你的盘点明细还没有保存，是否放弃操作?","Icare",System.Windows.Forms.MessageBoxButtons.YesNo,MessageBoxIcon.Warning)== DialogResult.Yes)
				{				
					return;
				}
				else
				{
					m_bSaveFlag = false;
				}
			}
			((clsControlMedicineStorageCheck)this.objController).m_mthFindCheckBill();
		}

		private void m_cmdAddCheckBill_Click(object sender, System.EventArgs e)
		{
			if(this.m_cboPeriod.SelectedValue == null)
			{
				PublicClass.m_mthShowWarning(this.m_cboPeriod,"请选择正确账务期!");
				this.m_cboPeriod.Focus();
				return;
			}
			frmCheckCondition frmShow=new frmCheckCondition();
			switch(frmShow.ShowDialog())
			{
				case DialogResult.OK:
					clsMedicinePrepType_VO[] medPrepType=null;
					clsHISMedType_VO[] medType=null;
					medPrepType=frmShow.m_GetMedPrepType();
					medType=frmShow.m_GetMedType();
					if(medPrepType!=null&&medType!=null&&medPrepType.Length>0&&medType.Length>0)
					{
                        ((clsControlMedicineStorageCheck)this.objController).m_mthAddNewCheckBill(strStorageFlag, medType, medPrepType,null, frmShow.isShowZero, frmShow.isStop);
                        m_dtgCheckDetail.CurrentCell = new DataGridCell(0, 9);
					}
					break;
				case DialogResult.Yes:
					m_dtgCheckDetail.ReadOnly=false;
					m_dtgCheckDetail.AllowAddNew=true;
                    clsMedicineType_VO[] MedTypeArr = frmShow.m_MedType();
					((com.digitalwave.controls.datagrid.clsColumnInfo)(this.m_dtgCheckDetail.Columns[0])).DataGridTextBoxColumn.ReadOnly = false;
					((com.digitalwave.controls.datagrid.clsColumnInfo)(this.m_dtgCheckDetail.Columns[0])).DataGridTextBoxColumn.TextBox.Enter+=new EventHandler(TextBox_Enter);
                    if (MedTypeArr.Length > 0)
                    {
                        ((clsControlMedicineStorageCheck)this.objController).m_mthAddNewCheckBill(strStorageFlag, null, null, MedTypeArr, frmShow.isShowZero, frmShow.isStop);
                        m_dtgCheckDetail.CurrentCell = new DataGridCell(0, 9);
                        m_dtgCheckDetail.Focus();
                    }
					break;
			}
		}

		private void m_cboMedicinePrepType_SelectedIndexChanged(object sender, System.EventArgs e)
		{		

		}

		private void m_DglCheckBill_m_evtCurrentCellChanged(object sender, System.EventArgs e)
		{	
			if(m_bSaveFlag)
			{
				if(MessageBox.Show("你的盘点明细还没有保存，是否放弃操作?","Icare",System.Windows.Forms.MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
				{
					m_DglCheckBill1.CurrentCell=new DataGridCell(m_ndtgBillRowIndex,0);
					return;
				}
			}
			m_bSaveFlag = false;			
			m_ndtgBillRowIndex =  m_DglCheckBill1.CurrentCell.RowNumber;
            string strCheckID = m_DglCheckBill1.m_objGetRow(m_ndtgBillRowIndex)["STORAGECHECKID_CHR"].ToString();

            checkDate = m_DglCheckBill1.m_objGetRow(m_ndtgBillRowIndex)["CREATEDATE_DAT"].ToString();
            m_dtpCreateDate.Text = checkDate;

            reMark = m_DglCheckBill1.m_objGetRow(m_ndtgBillRowIndex)["REMARK_VCHR"].ToString();
            m_txtRemark.Text = reMark;
			((clsControlMedicineStorageCheck)this.objController).m_mthGetCheckDetail(strCheckID,strStorageFlag);
			
		}

		private void m_cboAddCheckDetail_Click(object sender, System.EventArgs e)
		{
			if(m_dtgCheckDetail.RowCount==0)
				return;
			if(((clsControlMedicineStorageCheck)this.objController).m_mthAddCheckDetail()!= 1)
			{
				PublicClass.m_mthShowWarning(this.m_DglCheckBill,"保存失败!");
			}
			else
			{
				PublicClass.m_mthShowWarning(this.m_DglCheckBill,"保存成功!");
                m_cmdAudit.Enabled = true;
			}
			m_bSaveFlag = false;
			m_cmdAddCheckBill.Enabled=true;
		}

		private void m_dtgCheckDetail_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
		{
			m_bSaveFlag = true;		
			if(e.KeyCode == Keys.Enter)
			{
				if( e.m_intColNumber == 9)
				{
					this.m_dtgCheckDetail.CurrentCell=new DataGridCell(e.m_intRowNumber,14);
					return;
				}
				if(e.m_intColNumber == 14)
				{
                    if (e.m_intRowNumber != m_dtgCheckDetail.RowCount - 1)
                    {
                        this.m_dtgCheckDetail.CurrentCell = new DataGridCell(e.m_intRowNumber + 1, 0);
                        this.m_dtgCheckDetail.CurrentCell = new DataGridCell(e.m_intRowNumber + 1, 9);
                    }
                    else
                        m_cboAddCheckDetail.Focus();
					return;
				}
				if(e.m_intColNumber==0)
				{
					this.ctlApplMedOut.m_txtFindMed.Text=objTxtBox.Text;
					this.ctlApplMedOut.strSTORAGEID=m_cboStorage.SelectItemValue;
					Point p=e.m_ptPositionInScreen;
					p=this.FindForm().PointToClient(p);
					p.Y+=23;
					this.ctlApplMedOut.Location=p;
					this.ctlApplMedOut.Visible=true;
					this.Controls.Add(this.ctlApplMedOut);
					this.ctlApplMedOut.Focus();
					this.ctlApplMedOut.BringToFront();
					
				}			
			}
			this.m_cmdAudit.Enabled = false;
		}

		private void m_dtgCheckDetail_m_evtCurrentCellChanged(object sender, System.EventArgs e)
		{
			if(m_dtgCheckDetail.CurrentCell.ColumnNumber>3&&m_dtgCheckDetail.CurrentCell.ColumnNumber<=7)
			{
				m_dtgCheckDetail.CurrentCell=new DataGridCell(m_dtgCheckDetail.CurrentCell.RowNumber,9);
			}
			if(m_dtgCheckDetail.CurrentCell.ColumnNumber>9)
			{
				m_dtgCheckDetail.CurrentCell=new DataGridCell(m_dtgCheckDetail.CurrentCell.RowNumber,14);
			}
			if(m_dtgCheckDetail.CurrentCell.ColumnNumber<4)
			{
				m_dtgCheckDetail.CurrentCell=new DataGridCell(m_dtgCheckDetail.CurrentCell.RowNumber,0);
			}
		}

		private void m_cmdFind_Click(object sender, System.EventArgs e)
		{
		}

		private void m_cmbClearDetail_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show("是否恢复？","Icare",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
			{
				return;
			}
			else
			{
				((clsControlMedicineStorageCheck)this.objController).m_mthRenew();
			}
		}

		private void m_cmbExit_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show("确定要退出？","Icare",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
			{
				return;
			}
			this.Close();
		}

		private void m_cboPeriod_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(m_bSaveFlag)
			{
				if(MessageBox.Show("你的盘点明细还没有保存，是否放弃操作?","",System.Windows.Forms.MessageBoxButtons.YesNo)== DialogResult.Yes)
				{									
					return;
				}
				else
				{
					m_bSaveFlag = false;
				}
			}
			((clsControlMedicineStorageCheck)this.objController).m_mthFindCheckBill();
		}

		private void m_cmdPrint_Click(object sender, System.EventArgs e)
		{
			((clsControlMedicineStorageCheck)this.objController).m_mthUnionData();
		}

		private void m_cmbPreview_Click(object sender, System.EventArgs e)
		{
            ((clsControlMedicineStorageCheck)this.objController).m_mthPreDetail();
		}
		clsPublicParm PublicClass=new clsPublicParm();
		private void m_cmdAudit_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show("是否要审核？审核后数据不能再修改！","Icare",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.Yes)
			{
				if(m_DglCheckBill.RowCount>1)
				{
					PublicClass.m_mthShowWarning(this.m_DglCheckBill,"当前财务期存在多张未审核的单据,请先做合并单据的操作!");
					return;
				}
				if(((clsControlMedicineStorageCheck)this.objController).m_mthAuditCheckBill()== 1)
				{
					PublicClass.m_mthShowWarning(this.m_DglCheckBill,"审核通过!");
					m_DglCheckBill.m_mthDeleteRow(m_DglCheckBill.CurrentCell.RowNumber);
					m_dtgCheckDetail.m_mthDeleteAllRow();
					m_labLostMoney.Text="";
					label14.Text="";
					label10.Text="";
				}
			}
		}

		private void m_cmdDelCheckBill_Click(object sender, System.EventArgs e)
		{
			if(this.m_DglCheckBill.RowCount <= 0)
			{
				return;
			}
			if(MessageBox.Show("确定删除选定单据？","Icare",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK)
			{
				if(((clsControlMedicineStorageCheck)this.objController).m_lngDelCheckBill()== 1)
				{
					m_labLostMoney.Text="";
					label14.Text="";
					label10.Text="";
                    label29.Text = "";
                    label32.Text = "";
                    label26.Text = "";
					PublicClass.m_mthShowWarning(this.m_DglCheckBill,"删除成功!");
				}
			}
			m_cmdAddCheckBill.Enabled=true;
		}
		public void ShowForm(string p_strStorageFlag)
		{
			strStorageFlag = p_strStorageFlag;
			this.Show();
		}

		private void m_cboStorage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		
		}

		private void controlMedicineFind1_m_evtReturnVal(object sender, com.digitalwave.controls.clsEvtReturnVal e)
		{
		}

		private void controlMedicineFind1_Leave(object sender, System.EventArgs e)
		{
		}
		private void CreateFunction(System.Windows.Forms.Control obj)
		{			
			for(int i = 0;i < obj.Controls.Count; i++)
			{
				if(obj.Controls[i].Name == "controlMedicineFind1")
				{
					continue;
				}
				if(obj.Controls[i].HasChildren)
				{
					CreateFunction(obj.Controls[i]);
				}
				else
				{	
					if(obj.Controls[i].Name == "m_txtMedicineName" || obj.Controls[i].Name == "m_cmdFind")
					{
						continue;
					}
					obj.Controls[i].KeyDown -=new KeyEventHandler(Form1_KeyDown);			
					obj.Controls[i].KeyDown +=new KeyEventHandler(Form1_KeyDown);
					
				}
			}
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				if(sender.GetType() == typeof(System.Windows.Forms.DateTimePicker))
				{
					System.Windows.Forms.DateTimePicker obj = (System.Windows.Forms.DateTimePicker)sender;
					int i = 0;
					if(obj.Tag == null)
					{
						obj.Tag = 1;
						i = 1;
					}
					else
					{
						i = int.Parse(obj.Tag.ToString());
					}
					if( i == 3)
					{
						SendKeys.Send("{RIGHT}");
						SendKeys.Send("{TAB}");
						obj.Tag = 1;
					}
					else
					{
						SendKeys.SendWait("{RIGHT}");
						i++;
						obj.Tag = i;
					}

				}
				else
				{
					SendKeys.Send("{TAB}");
				}
			}
		}

		private void m_txtMedicineName_Enter(object sender, System.EventArgs e)
		{
		}
		
		private void m_dtgCheckDetail_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void m_txtMedicineName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{

		}
		private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			objTxtBox=(TextBox)sender;
            if(textBoxValuse!=objTxtBox.Text)
            {
                this.m_dtgCheckDetail.m_mthFormatCell(this.m_dtgCheckDetail.CurrentCell.RowNumber, this.m_dtgCheckDetail.CurrentCell.ColumnNumber, new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold), System.Drawing.Color.White, System.Drawing.Color.Red);
             }
			if((int)e.KeyChar>=46&&(int)e.KeyChar<=57&&(int)e.KeyChar!=47||(int)e.KeyChar==8)
			{
			
			}
			else
			{
				e.Handled=true;

			}
			if(e.KeyChar=='.')
			{
				if(objTxtBox.Text.Trim()=="")
				{
					objTxtBox.Text="0.";
					System.Windows.Forms.SendKeys.SendWait("{Right}");
					System.Windows.Forms.SendKeys.SendWait("{Right}");
					e.Handled=true;
				}
				if(objTxtBox.Text.IndexOf(".")>-1)
				{
					e.Handled=true;
				}
			}

		}

		private void m_cmbCondition_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void groupBox2_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void m_tabAduit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_dtgCheckDetail.m_mthDeleteAllRow();
			m_DglCheckBill.m_mthSelectAllRow(false);
			m_DglCheckBill1.m_mthSelectAllRow(false);
			m_labLostMoney.Text="0";
			label14.Text="0";
			label10.Text="0";
			if(m_tabAduit.SelectedIndex==1)
			{
				m_cmdDelCheckBill.Enabled=false;
				m_cmdAddCheckBill.Enabled=false;
				m_cmdAudit.Enabled=false;
				m_cboAddCheckDetail.Enabled=false;
				m_cmbClearDetail.Enabled=false;
				m_dtgCheckDetail.ReadOnly=true;
			}
			else
			{
				m_cmdDelCheckBill.Enabled=true;
				m_cmdAddCheckBill.Enabled=true;
				m_cmdAudit.Enabled=true;
				m_cboAddCheckDetail.Enabled=true;
				m_cmbClearDetail.Enabled=true;
				m_dtgCheckDetail.ReadOnly=false;
			}
		}
        public string checkDate = "";
        public string reMark = "";
		private void m_DglCheckBill_m_evtCurrentCellChanged_1(object sender, System.EventArgs e)
		{		
			if(m_bSaveFlag)
			{
				if(MessageBox.Show("你的盘点明细还没有保存，是否放弃操作?","Icare",System.Windows.Forms.MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
				{
					m_DglCheckBill.CurrentCell=new DataGridCell(m_ndtgBillRowIndex,0);
					return;
				}
			}
			m_bSaveFlag = false;			
			m_ndtgBillRowIndex =  m_DglCheckBill.CurrentCell.RowNumber;
            string strCheckID = m_DglCheckBill.m_objGetRow(m_ndtgBillRowIndex)["STORAGECHECKID_CHR"].ToString();
            checkDate = m_DglCheckBill.m_objGetRow(m_ndtgBillRowIndex)["CREATEDATE_DAT"].ToString();
            m_dtpCreateDate.Text = checkDate;
            
            reMark = m_DglCheckBill.m_objGetRow(m_ndtgBillRowIndex)["REMARK_VCHR"].ToString();
            m_txtRemark.Text = reMark;
			this.groupBox4.Tag=strCheckID;
			((clsControlMedicineStorageCheck)this.objController).m_mthGetCheckDetail(strCheckID,strStorageFlag);
			if(m_dtgCheckDetail.RowCount>0)
			{
				m_dtgCheckDetail.CurrentCell=new DataGridCell(0,9);
			}
			this.m_cmdAudit.Enabled = true;
			m_cmbClearDetail.Enabled=false;
			
		}
		TextBox objTxtBox;
		string textBoxValuse="";
		private void TextBox_Enter(object sender, EventArgs e)
		{
			objTxtBox=(TextBox)sender;
			textBoxValuse=objTxtBox.Text;
			objTxtBox.BackColor=System.Drawing.Color.DeepSkyBlue;
		}

		private void TextBox_Leave(object sender, EventArgs e)
		{
			if(textBoxValuse!=objTxtBox.Text)
			{
                objTxtBox.ForeColor = System.Drawing.Color.Red;
			}
			if( this.m_dtgCheckDetail.CurrentCell.RowNumber >= 0)
			{
				((clsControlMedicineStorageCheck)this.objController).m_mthCalLostName(this.m_dtgCheckDetail.CurrentCell.RowNumber);
			}
			m_nOldRow = this.m_dtgCheckDetail.CurrentCell.RowNumber;
		}

		private void m_dtpCreateDate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void frmMedicineStorageCheck_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			m_DglCheckBill.m_mthDeleteAllRow();
			m_dtgCheckDetail.m_mthDeleteAllRow();
		}

		private void label7_Click(object sender, System.EventArgs e)
		{
		
		}

		private void textBox2_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void collapsibleSplitter1_Click(object sender, System.EventArgs e)
		{
			
		}

		private void collapsibleSplitter1_MouseEnter(object sender, System.EventArgs e)
		{
			this.panel4.Width=152;
		}
		private bool m_blFindData(int intCoun,string str1)
		{
			for(int i1=0;i1<m_dtgCheckDetail.RowCount;i1++)
			{
				if(m_dtgCheckDetail[i1,intCoun].ToString().Trim().ToUpper().IndexOf(str1.Trim().ToUpper(),0)>-1)
				{
					m_dtgCheckDetail.CurrentCell=new DataGridCell(i1,9);
					return true;
				}
			}
			return false;
		}
		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
			if(comboBox1.Text!=""&&textBox1.Text!=""&&m_dtgCheckDetail.RowCount>0)
			{
				bool isOK=false;
				switch(comboBox1.SelectedIndex)
				{
					case 0:
                        isOK = m_blFindData(0,textBox1.Text.Trim());
						break;
					case 1:
                        isOK = m_blFindData(1,textBox1.Text.Trim());
						break;
					case 2:
                        isOK = m_blFindData(17, textBox1.Text.Trim().ToUpper());
						break;
					case 3:
                        isOK = m_blFindData(18, textBox1.Text.Trim().ToUpper());
						break;

				}
				if(isOK==false)
				{
					PublicClass.m_mthShowWarning(this.textBox1,"没有找到符合要求的数据！");
				}
			}
		}

		private void buttonXP3_Click(object sender, System.EventArgs e)
		{
			if(m_bSaveFlag)
			{
				if(MessageBox.Show("你的盘点明细还没有保存，是否放弃操作?","",System.Windows.Forms.MessageBoxButtons.YesNo)== DialogResult.Yes)
				{									
					return;
				}
				else
				{
					m_bSaveFlag = false;
				}
			}
			((clsControlMedicineStorageCheck)this.objController).m_mthCboMedicinePrepType();
		}

		private void buttonXP2_Click(object sender, System.EventArgs e)
		{
			((clsControlMedicineStorageCheck)this.objController).m_mthFindMedicine();
		}

		private void label15_Click(object sender, System.EventArgs e)
		{
		
		}

		private void label11_Click(object sender, System.EventArgs e)
		{
		
		}

		private void ctlApplMedOut_e_evtReturnMedStoreOutVal(object sender, com.digitalwave.controls.clsEvtReturnOutStoreVal e)
		{
			m_dtgCheckDetail.CurrentCell=new DataGridCell(m_dtgCheckDetail.CurrentCell.RowNumber,9);
		}

		private void buttonXP4_Click(object sender, System.EventArgs e)
		{
			((clsControlMedicineStorageCheck)this.objController).m_mthReturn();
		}
	}
}
