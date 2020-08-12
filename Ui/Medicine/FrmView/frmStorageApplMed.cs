using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmStorageApplMed 的摘要说明。
	/// </summary>
	public class frmStorageApplMed : com.digitalwave.GUI_Base.frmMDI_Child_Base	//GUI_Base.dll
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		internal com.digitalwave.controls.datagrid.ctlDataGrid DglApplDe;
		internal System.Windows.Forms.TabControl tabAppl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		internal System.Windows.Forms.ListView lsvAppl;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		internal System.Windows.Forms.ListView lsvApplEnd;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		internal PinkieControls.ButtonXP btnFind;
		private System.Windows.Forms.Label label1;
		internal PinkieControls.ButtonXP btnRetrue;
		private System.Windows.Forms.Panel panel4;
		internal PinkieControls.ButtonXP btnESC;
		internal PinkieControls.ButtonXP btnSave;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		internal System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label9;
		internal System.Windows.Forms.Label ApplstorageMed;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label10;
		internal com.digitalwave.controls.ControlMedicineFind ctlApplMedOut;
		internal System.Windows.Forms.TextBox txtfind;
		internal System.Windows.Forms.ComboBox cobSele;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label45;
		internal System.Windows.Forms.Label m_txtMedSpec;
		internal System.Windows.Forms.TextBox txtMedID;
        internal PinkieControls.ButtonXP buttonXP1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmStorageApplMed()
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cobSele = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRetrue = new PinkieControls.ButtonXP();
            this.txtfind = new System.Windows.Forms.TextBox();
            this.btnFind = new PinkieControls.ButtonXP();
            this.btnSave = new PinkieControls.ButtonXP();
            this.btnESC = new PinkieControls.ButtonXP();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DglApplDe = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.tabAppl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lsvAppl = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lsvApplEnd = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label45 = new System.Windows.Forms.Label();
            this.m_txtMedSpec = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMedID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.ApplstorageMed = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.ctlApplMedOut = new com.digitalwave.controls.ControlMedicineFind();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DglApplDe)).BeginInit();
            this.tabAppl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.buttonXP1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnESC);
            this.panel1.Location = new System.Drawing.Point(488, 416);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(536, 124);
            this.panel1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cobSele);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnRetrue);
            this.groupBox2.Controls.Add(this.txtfind);
            this.groupBox2.Controls.Add(this.btnFind);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(216, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(312, 120);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // cobSele
            // 
            this.cobSele.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cobSele.Items.AddRange(new object[] {
            "申请药房",
            "申请单号",
            "申请人"});
            this.cobSele.Location = new System.Drawing.Point(76, 28);
            this.cobSele.Name = "cobSele";
            this.cobSele.Size = new System.Drawing.Size(121, 22);
            this.cobSele.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 23);
            this.label1.TabIndex = 142;
            this.label1.Text = "查找方式";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRetrue
            // 
            this.btnRetrue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRetrue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnRetrue.DefaultScheme = true;
            this.btnRetrue.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRetrue.Hint = "";
            this.btnRetrue.Location = new System.Drawing.Point(208, 80);
            this.btnRetrue.Name = "btnRetrue";
            this.btnRetrue.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnRetrue.Size = new System.Drawing.Size(88, 32);
            this.btnRetrue.TabIndex = 147;
            this.btnRetrue.Text = "返回(&R)";
            this.btnRetrue.Click += new System.EventHandler(this.btnRetrue_Click);
            // 
            // txtfind
            // 
            this.txtfind.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtfind.Location = new System.Drawing.Point(216, 28);
            this.txtfind.Name = "txtfind";
            this.txtfind.Size = new System.Drawing.Size(88, 23);
            this.txtfind.TabIndex = 2;
            this.txtfind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGreatDate_KeyDown);
            // 
            // btnFind
            // 
            this.btnFind.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnFind.DefaultScheme = true;
            this.btnFind.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFind.Hint = "";
            this.btnFind.Location = new System.Drawing.Point(68, 80);
            this.btnFind.Name = "btnFind";
            this.btnFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnFind.Size = new System.Drawing.Size(88, 32);
            this.btnFind.TabIndex = 3;
            this.btnFind.Text = "查找(&F)";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnSave.DefaultScheme = true;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.Hint = "";
            this.btnSave.Location = new System.Drawing.Point(16, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnSave.Size = new System.Drawing.Size(168, 32);
            this.btnSave.TabIndex = 139;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnESC
            // 
            this.btnESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnESC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnESC.DefaultScheme = true;
            this.btnESC.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnESC.Hint = "";
            this.btnESC.Location = new System.Drawing.Point(16, 86);
            this.btnESC.Name = "btnESC";
            this.btnESC.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnESC.Size = new System.Drawing.Size(168, 32);
            this.btnESC.TabIndex = 140;
            this.btnESC.Text = "退出(ESC)";
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(0, 544);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1044, 56);
            this.panel2.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label8.Location = new System.Drawing.Point(736, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(192, 23);
            this.label8.TabIndex = 4;
            this.label8.Text = "Alt+M使查找窗体具有焦点";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label7.Location = new System.Drawing.Point(532, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(168, 23);
            this.label7.TabIndex = 3;
            this.label7.Text = "Alt+L激活申请明细窗体";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label6.Location = new System.Drawing.Point(352, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 23);
            this.label6.TabIndex = 2;
            this.label6.Text = "Alt+O激活己配药窗体";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label5.Location = new System.Drawing.Point(156, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 23);
            this.label5.TabIndex = 1;
            this.label5.Text = "Alt+V激活未配药窗体";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label4.Location = new System.Drawing.Point(16, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "快捷键提示：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.DglApplDe);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1024, 304);
            this.panel3.TabIndex = 4;
            // 
            // DglApplDe
            // 
            this.DglApplDe.AllowAddNew = false;
            this.DglApplDe.AllowDelete = false;
            this.DglApplDe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DglApplDe.AutoAppendRow = false;
            this.DglApplDe.AutoScroll = true;
            this.DglApplDe.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DglApplDe.CaptionText = "";
            this.DglApplDe.CaptionVisible = false;
            this.DglApplDe.ColumnHeadersVisible = true;
            clsColumnInfo23.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo23.BackColor = System.Drawing.Color.White;
            clsColumnInfo23.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo23.ColumnIndex = 0;
            clsColumnInfo23.ColumnName = "ROWNO_CHR";
            clsColumnInfo23.ColumnWidth = 0;
            clsColumnInfo23.Enabled = false;
            clsColumnInfo23.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo23.HeadText = "行号";
            clsColumnInfo23.ReadOnly = true;
            clsColumnInfo23.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo24.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo24.BackColor = System.Drawing.Color.White;
            clsColumnInfo24.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo24.ColumnIndex = 1;
            clsColumnInfo24.ColumnName = "SYSLOTNO_CHR";
            clsColumnInfo24.ColumnWidth = 0;
            clsColumnInfo24.Enabled = false;
            clsColumnInfo24.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo24.HeadText = "系统号";
            clsColumnInfo24.ReadOnly = true;
            clsColumnInfo24.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo25.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo25.BackColor = System.Drawing.Color.White;
            clsColumnInfo25.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo25.ColumnIndex = 2;
            clsColumnInfo25.ColumnName = "MEDICINENAME_VCHR";
            clsColumnInfo25.ColumnWidth = 160;
            clsColumnInfo25.Enabled = false;
            clsColumnInfo25.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo25.HeadText = "药品名称";
            clsColumnInfo25.ReadOnly = true;
            clsColumnInfo25.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo26.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo26.BackColor = System.Drawing.Color.White;
            clsColumnInfo26.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo26.ColumnIndex = 3;
            clsColumnInfo26.ColumnName = "出库批号";
            clsColumnInfo26.ColumnWidth = 160;
            clsColumnInfo26.Enabled = true;
            clsColumnInfo26.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo26.HeadText = "出库批号";
            clsColumnInfo26.ReadOnly = true;
            clsColumnInfo26.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo27.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo27.BackColor = System.Drawing.Color.White;
            clsColumnInfo27.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo27.ColumnIndex = 4;
            clsColumnInfo27.ColumnName = "UNITID_CHR";
            clsColumnInfo27.ColumnWidth = 50;
            clsColumnInfo27.Enabled = false;
            clsColumnInfo27.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo27.HeadText = "单位";
            clsColumnInfo27.ReadOnly = true;
            clsColumnInfo27.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo28.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo28.BackColor = System.Drawing.Color.White;
            clsColumnInfo28.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo28.ColumnIndex = 5;
            clsColumnInfo28.ColumnName = "QTY_DEC";
            clsColumnInfo28.ColumnWidth = 65;
            clsColumnInfo28.Enabled = false;
            clsColumnInfo28.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo28.HeadText = "请领数量";
            clsColumnInfo28.ReadOnly = true;
            clsColumnInfo28.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo29.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo29.BackColor = System.Drawing.Color.White;
            clsColumnInfo29.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo29.ColumnIndex = 6;
            clsColumnInfo29.ColumnName = "当前批号的数量";
            clsColumnInfo29.ColumnWidth = 65;
            clsColumnInfo29.Enabled = false;
            clsColumnInfo29.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo29.HeadText = "当前数量";
            clsColumnInfo29.ReadOnly = true;
            clsColumnInfo29.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo30.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo30.BackColor = System.Drawing.Color.White;
            clsColumnInfo30.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo30.ColumnIndex = 7;
            clsColumnInfo30.ColumnName = "实发数量";
            clsColumnInfo30.ColumnWidth = 60;
            clsColumnInfo30.Enabled = true;
            clsColumnInfo30.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo30.HeadText = "实发数量";
            clsColumnInfo30.ReadOnly = false;
            clsColumnInfo30.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo31.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo31.BackColor = System.Drawing.Color.White;
            clsColumnInfo31.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo31.ColumnIndex = 8;
            clsColumnInfo31.ColumnName = "出库单价";
            clsColumnInfo31.ColumnWidth = 60;
            clsColumnInfo31.Enabled = false;
            clsColumnInfo31.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo31.HeadText = "出库单价";
            clsColumnInfo31.ReadOnly = true;
            clsColumnInfo31.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo32.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo32.BackColor = System.Drawing.Color.White;
            clsColumnInfo32.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo32.ColumnIndex = 9;
            clsColumnInfo32.ColumnName = "APPLDATE_DAT";
            clsColumnInfo32.ColumnWidth = 135;
            clsColumnInfo32.Enabled = false;
            clsColumnInfo32.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo32.HeadText = "请领日期";
            clsColumnInfo32.ReadOnly = true;
            clsColumnInfo32.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo33.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo33.BackColor = System.Drawing.Color.White;
            clsColumnInfo33.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo33.ColumnIndex = 10;
            clsColumnInfo33.ColumnName = "生产厂家";
            clsColumnInfo33.ColumnWidth = 240;
            clsColumnInfo33.Enabled = false;
            clsColumnInfo33.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo33.HeadText = "生产厂家";
            clsColumnInfo33.ReadOnly = true;
            clsColumnInfo33.TextFont = new System.Drawing.Font("宋体", 10F);
            this.DglApplDe.Columns.Add(clsColumnInfo23);
            this.DglApplDe.Columns.Add(clsColumnInfo24);
            this.DglApplDe.Columns.Add(clsColumnInfo25);
            this.DglApplDe.Columns.Add(clsColumnInfo26);
            this.DglApplDe.Columns.Add(clsColumnInfo27);
            this.DglApplDe.Columns.Add(clsColumnInfo28);
            this.DglApplDe.Columns.Add(clsColumnInfo29);
            this.DglApplDe.Columns.Add(clsColumnInfo30);
            this.DglApplDe.Columns.Add(clsColumnInfo31);
            this.DglApplDe.Columns.Add(clsColumnInfo32);
            this.DglApplDe.Columns.Add(clsColumnInfo33);
            this.DglApplDe.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DglApplDe.FullRowSelect = false;
            this.DglApplDe.Location = new System.Drawing.Point(0, 0);
            this.DglApplDe.MultiSelect = false;
            this.DglApplDe.Name = "DglApplDe";
            this.DglApplDe.ReadOnly = false;
            this.DglApplDe.RowHeadersVisible = false;
            this.DglApplDe.RowHeaderWidth = 35;
            this.DglApplDe.SelectedRowBackColor = System.Drawing.SystemColors.Desktop;
            this.DglApplDe.SelectedRowForeColor = System.Drawing.Color.White;
            this.DglApplDe.Size = new System.Drawing.Size(1032, 304);
            this.DglApplDe.TabIndex = 6;
            this.DglApplDe.m_evtCurrentCellChanged += new System.EventHandler(this.DglApplDe_m_evtCurrentCellChanged_1);
            this.DglApplDe.m_evtDataGridKeyDown += new System.Windows.Forms.KeyEventHandler(this.DglApplDe_m_evtDataGridKeyDown);
            this.DglApplDe.m_evtDataGridTextBoxKeyPress += new com.digitalwave.controls.datagrid.clsDGTextKeyPressEventHandler(this.DglApplDe_m_evtDataGridTextBoxKeyPress);
            this.DglApplDe.m_evtDataGridTextBoxKeyDown += new com.digitalwave.controls.datagrid.clsDGTextKeyEventHandler(this.DglApplDe_m_evtDataGridTextBoxKeyDown);
            this.DglApplDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DglApplDe_KeyDown);
            // 
            // tabAppl
            // 
            this.tabAppl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabAppl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabAppl.Controls.Add(this.tabPage1);
            this.tabAppl.Controls.Add(this.tabPage2);
            this.tabAppl.Location = new System.Drawing.Point(-3, -16);
            this.tabAppl.Name = "tabAppl";
            this.tabAppl.SelectedIndex = 0;
            this.tabAppl.Size = new System.Drawing.Size(482, 240);
            this.tabAppl.TabIndex = 5;
            this.tabAppl.SelectedIndexChanged += new System.EventHandler(this.tabAppl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lsvAppl);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(474, 213);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "未配药";
            // 
            // lsvAppl
            // 
            this.lsvAppl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lsvAppl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader9,
            this.columnHeader2,
            this.columnHeader3});
            this.lsvAppl.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lsvAppl.FullRowSelect = true;
            this.lsvAppl.GridLines = true;
            this.lsvAppl.HideSelection = false;
            this.lsvAppl.Location = new System.Drawing.Point(0, 8);
            this.lsvAppl.Name = "lsvAppl";
            this.lsvAppl.Size = new System.Drawing.Size(480, 208);
            this.lsvAppl.TabIndex = 2;
            this.lsvAppl.UseCompatibleStateImageBehavior = false;
            this.lsvAppl.View = System.Windows.Forms.View.Details;
            this.lsvAppl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsvAppl_KeyDown);
            this.lsvAppl.Click += new System.EventHandler(this.lsvAppl_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "药房名称";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "申请单号";
            this.columnHeader9.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "申请时间";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "创建人";
            this.columnHeader3.Width = 90;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lsvApplEnd);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(474, 215);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "己配药";
            // 
            // lsvApplEnd
            // 
            this.lsvApplEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvApplEnd.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader8,
            this.columnHeader6,
            this.columnHeader7});
            this.lsvApplEnd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lsvApplEnd.FullRowSelect = true;
            this.lsvApplEnd.GridLines = true;
            this.lsvApplEnd.Location = new System.Drawing.Point(0, 14);
            this.lsvApplEnd.Name = "lsvApplEnd";
            this.lsvApplEnd.Size = new System.Drawing.Size(480, 208);
            this.lsvApplEnd.TabIndex = 3;
            this.lsvApplEnd.UseCompatibleStateImageBehavior = false;
            this.lsvApplEnd.View = System.Windows.Forms.View.Details;
            this.lsvApplEnd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsvApplEnd_KeyDown);
            this.lsvApplEnd.Click += new System.EventHandler(this.lsvApplEnd_Click);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "药房名称";
            this.columnHeader5.Width = 90;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "申请单号";
            this.columnHeader8.Width = 120;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "申请时间";
            this.columnHeader6.Width = 150;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "创建人";
            this.columnHeader7.Width = 90;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label45);
            this.panel4.Controls.Add(this.m_txtMedSpec);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.txtMedID);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Location = new System.Drawing.Point(488, 344);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(536, 64);
            this.panel4.TabIndex = 0;
            // 
            // label45
            // 
            this.label45.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label45.BackColor = System.Drawing.Color.Orange;
            this.label45.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label45.Location = new System.Drawing.Point(312, 37);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(216, 1);
            this.label45.TabIndex = 152;
            // 
            // m_txtMedSpec
            // 
            this.m_txtMedSpec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtMedSpec.Location = new System.Drawing.Point(312, 21);
            this.m_txtMedSpec.Name = "m_txtMedSpec";
            this.m_txtMedSpec.Size = new System.Drawing.Size(216, 18);
            this.m_txtMedSpec.TabIndex = 153;
            this.m_txtMedSpec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.Location = new System.Drawing.Point(232, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 23);
            this.label3.TabIndex = 145;
            this.label3.Text = "所属财务期";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMedID
            // 
            this.txtMedID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMedID.Location = new System.Drawing.Point(96, 19);
            this.txtMedID.Name = "txtMedID";
            this.txtMedID.Size = new System.Drawing.Size(120, 23);
            this.txtMedID.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.Location = new System.Drawing.Point(16, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 23);
            this.label2.TabIndex = 143;
            this.label2.Text = "药库单据号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePicker1.Location = new System.Drawing.Point(360, 202);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(120, 23);
            this.dateTimePicker1.TabIndex = 9;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(488, 312);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 23);
            this.label9.TabIndex = 143;
            this.label9.Text = "配药药库";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ApplstorageMed
            // 
            this.ApplstorageMed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplstorageMed.BackColor = System.Drawing.SystemColors.Control;
            this.ApplstorageMed.Location = new System.Drawing.Point(552, 314);
            this.ApplstorageMed.Name = "ApplstorageMed";
            this.ApplstorageMed.Size = new System.Drawing.Size(472, 18);
            this.ApplstorageMed.TabIndex = 144;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.dateTimePicker1);
            this.panel5.Controls.Add(this.tabAppl);
            this.panel5.Location = new System.Drawing.Point(0, 312);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(488, 228);
            this.panel5.TabIndex = 145;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Location = new System.Drawing.Point(552, 330);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(472, 1);
            this.label10.TabIndex = 146;
            // 
            // ctlApplMedOut
            // 
            this.ctlApplMedOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlApplMedOut.blIsMedStorage = true;
            this.ctlApplMedOut.blISOutStorage = true;
            this.ctlApplMedOut.blRepertory = true;
            this.ctlApplMedOut.FindMedmode = 0;
            this.ctlApplMedOut.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctlApplMedOut.intIsReData = 0;
            this.ctlApplMedOut.isApplMebMod = null;
            this.ctlApplMedOut.isApplModel = false;
            this.ctlApplMedOut.isShowFindType = false;
            this.ctlApplMedOut.IsShowZero = false;
            this.ctlApplMedOut.Location = new System.Drawing.Point(304, -328);
            this.ctlApplMedOut.Name = "ctlApplMedOut";
            this.ctlApplMedOut.Size = new System.Drawing.Size(688, 336);
            this.ctlApplMedOut.strMedstorage = null;
            this.ctlApplMedOut.strSTORAGEID = "-1";
            this.ctlApplMedOut.TabIndex = 147;
            this.ctlApplMedOut.Visible = false;
            this.ctlApplMedOut.e_evtReturnMedStoreOutVal += new com.digitalwave.controls.dlgReturnMedStoreOutVal(this.ctlApplMedOut_e_evtReturnMedStoreOutVal);
            // 
            // buttonXP1
            // 
            this.buttonXP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(16, 48);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(168, 32);
            this.buttonXP1.TabIndex = 141;
            this.buttonXP1.Text = "刷新(&R)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // frmStorageApplMed
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1028, 581);
            this.Controls.Add(this.ctlApplMedOut);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.ApplstorageMed);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Name = "frmStorageApplMed";
            this.Text = "药库配药";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStorageApplMed_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmStorageApplMed_KeyDown);
            this.Load += new System.EventHandler(this.frmStorageApplMed_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DglApplDe)).EndInit();
            this.tabAppl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			// TODO:  添加 frmStorageInOrd.CreateController 实现
			this.objController = new com.digitalwave.iCare.gui.HIS.clsControlStorageApplMed();
			this.objController.Set_GUI_Apperance(this);
        }
        #region 变量
        /// <summary>
        /// 保存单据类型信息
        /// </summary>
        public clsStorageOrdType_VO ordTypeVo = new clsStorageOrdType_VO();
        #endregion
        private void frmStorageApplMed_Load(object sender, System.EventArgs e)
		{
		    ((clsControlStorageApplMed)this.objController).m_lngResetForm();
            this.DglApplDe.m_mthAddEnterToSpaceColumn(3);
		    this.DglApplDe.m_mthAddEnterToSpaceColumn(7);
		    this.DglApplDe.m_mthAddEnterToSpaceColumn(0);
			string p_strMaxDoc=null;
		    this.m_mthSetEnter2Tab(new System.Windows.Forms.Control[] {});
		    clsDomainControlStorageOrd m_objManage = new clsDomainControlStorageOrd();
            m_objManage.m_lngFindOrdTypeNameByID("", out ordTypeVo,"0");
            m_objManage.m_lngGetMaxDoc(out p_strMaxDoc, ordTypeVo.m_strBEGINSTR_CHR+ clsPublicParm.s_datGetServerDate().ToString("yyMMdd"), "2", (string)ApplstorageMed.Tag);
            txtMedID.Text = clsPublicParm.m_mthGetNewDocument(p_strMaxDoc, "1", int.Parse((string)ApplstorageMed.Tag), DateTime.Now.ToString("yyMMdd"), ordTypeVo.m_strBEGINSTR_CHR);
		    ((com.digitalwave.controls.datagrid.clsColumnInfo)DglApplDe.Columns[3] ).DataGridTextBoxColumn.TextBox.Enter+=new EventHandler(TextBox_Enter);
		    ((com.digitalwave.controls.datagrid.clsColumnInfo)DglApplDe.Columns[7] ).DataGridTextBoxColumn.TextBox.Enter+=new EventHandler(TextBox_Enter);

		}

		public void m_mthShowMe(string ordTypeID)
		{
			long lngRes;
			string medStroageName="";
			clsDomainConrol_Medicne objManage = new clsDomainConrol_Medicne();
			lngRes = objManage.m_lngGetMedstroage(ordTypeID,out medStroageName);
			if(lngRes==1&&medStroageName!=null)
			{
				ApplstorageMed.Text=medStroageName;
				ApplstorageMed.Tag=ordTypeID;
				this.Text=medStroageName;

			}
			else
			{
				MessageBox.Show("传入的药库ID不正确！","icare",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}
			this.Show();
		}

		private void lsvAppl_DoubleClick(object sender, System.EventArgs e)
		{
			((clsControlStorageApplMed)this.objController).m_lngLsvApplEdit();
		}

		private void DglApplDe_m_evtCurrentCellChanged(object sender, System.EventArgs e)
		{

		}

		private void DglApplDe_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{

		}

		private void DglApplDe_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				
				if(this.DglApplDe.CurrentCell.ColumnNumber==3)
				{
					((clsControlStorageApplMed)this.objController).m_lngShowMedData(e);
				}
				if(this.DglApplDe.CurrentCell.ColumnNumber==7)
				{
					this.DglApplDe.CurrentCell=new DataGridCell(this.DglApplDe.CurrentCell.RowNumber,0);
					this.DglApplDe.CurrentCell=new DataGridCell(this.DglApplDe.CurrentCell.RowNumber,7);
					if(DglApplDe[this.DglApplDe.CurrentCell.RowNumber,3].ToString()=="")
					{
						if(this.DglApplDe.CurrentCell.RowNumber+1<=this.DglApplDe.RowCount)
						{
							this.DglApplDe.CurrentCell=new DataGridCell(this.DglApplDe.CurrentCell.RowNumber+1,3);
						}
					}
					else
					{
						((clsControlStorageApplMed)this.objController).m_lngCheckValues();
					}
					
				}
			}
			if(e.KeyCode==Keys.Left)
			{
				if(this.DglApplDe.CurrentCell.ColumnNumber==3)
				{
					if(this.DglApplDe.CurrentCell.RowNumber-1>=0)
					{
						this.DglApplDe.CurrentCell=new DataGridCell(this.DglApplDe.CurrentCell.RowNumber-1,7);
					}
				}
				if(this.DglApplDe.CurrentCell.ColumnNumber==7)
				{
					this.DglApplDe.CurrentCell=new DataGridCell(this.DglApplDe.CurrentCell.RowNumber-1,3);
				}

			}
			if(e.KeyCode==Keys.Right)
			{
				if(this.DglApplDe.CurrentCell.ColumnNumber==3)
				{
					this.DglApplDe.CurrentCell=new DataGridCell(this.DglApplDe.CurrentCell.RowNumber,7);
				
				}
				if(this.DglApplDe.CurrentCell.ColumnNumber==7)
				{
					if(this.DglApplDe.CurrentCell.RowNumber+1>=this.DglApplDe.RowCount-1)
					{
						this.DglApplDe.CurrentCell=new DataGridCell(this.DglApplDe.CurrentCell.RowNumber+1,3);
					}
				}

			}

		}

		private void DglSeleSysNo_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
		{
		}

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageApplMed)this.objController).m_lngSave();
		}

		private void btnESC_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnFind_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageApplMed)this.objController).m_lngFind();
		}

		private void tabAppl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.tabAppl.SelectedIndex==0)
				((clsControlStorageApplMed)this.objController).m_lngUnLocked();
			else
				((clsControlStorageApplMed)this.objController).m_lngLocked();
			DglApplDe.m_mthDeleteAllRow();

		}

		private void DglSeleSysNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		
		}

		private void DglSeleSysNo_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
		{
		}

		private void lsvApplEnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		 if(e.KeyCode==Keys.Enter)
			 ((clsControlStorageApplMed)this.objController).m_lnglsvApplEnd();
		}

		private void lsvAppl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				((clsControlStorageApplMed)this.objController).m_lngLsvApplEdit();
		}

		private void frmStorageApplMed_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Escape)
			{
				if(ctlApplMedOut.Visible==true)
				{
					ctlApplMedOut.Visible=false;
					DglApplDe.CurrentCell =new DataGridCell(DglApplDe.CurrentCell.RowNumber,7);
					return;
				}
			}
			if(e.Alt)
			{
				if(e.KeyCode==Keys.M)
				{
					cobSele.Focus();
				}
				if(e.KeyCode==Keys.V)
				{
					tabAppl.SelectedIndex=0;
					if(lsvAppl.Items.Count>0)
					{
						lsvAppl.Items[0].Selected=true;
						lsvAppl.Items[0].Focused=true;
					}
				}
				if(e.KeyCode==Keys.O)
				{
					tabAppl.SelectedIndex=1;
					if(lsvApplEnd.Items.Count>0)
					{
						lsvApplEnd.Items[0].Selected=true;
						lsvApplEnd.Items[0].Focused=true;
					}
				}
				if(e.KeyCode==Keys.L)
				{
					if(DglApplDe.RowCount>0)
						DglApplDe.CurrentCell =new DataGridCell(0,3);
				}
			}

		}

		private void btnRetrue_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageApplMed)this.objController).m_lngReturnForm();
		}

		private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void txtGreatDate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void DglApplDe_m_evtCurrentCellChanged_1(object sender, System.EventArgs e)
		{
			if(this.DglApplDe.CurrentCell.ColumnNumber!=3&&this.DglApplDe.CurrentCell.ColumnNumber!=7)
				this.DglApplDe.CurrentCell=new DataGridCell(this.DglApplDe.CurrentCell.RowNumber,3);
		}

		private void DglApplDe_m_evtDataGridTextBoxKeyPress(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyPressEventArgs e)
		{

		}

		private void dateTimePicker1_ValueChanged(object sender, System.EventArgs e)
		{
			((clsControlStorageApplMed)this.objController).m_lngReturnForm();
		}

		private void ctlApplMedOut_e_evtReturnMedStoreOutVal(object sender, com.digitalwave.controls.clsEvtReturnOutStoreVal e)
		{
			((clsControlStorageApplMed)this.objController).m_GetseleData(e.objOutStoreVO);
			this.DglApplDe.CurrentCell=new DataGridCell(this.DglApplDe.CurrentCell.RowNumber,7);
		}

		private void lsvApplEnd_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageApplMed)this.objController).m_lnglsvApplEnd();
		}

		private void lsvAppl_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageApplMed)this.objController).m_lngLsvApplEdit();
		}

		private void DglApplDe_m_evtDataGridKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		
		}
		private TextBox objTextBox;
		private void TextBox_Enter(object sender, EventArgs e)
		{
			objTextBox=sender as TextBox;
			objTextBox.BackColor=System.Drawing.SystemColors.Desktop;
		}

		private void frmStorageApplMed_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			DglApplDe.m_mthDeleteAllRow();
		}

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonXP1_Click(object sender, EventArgs e)
        {
            frmStorageApplMed_Load(null, null);
            ((clsControlStorageApplMed)this.objController).m_lngReturnForm();
        }
	}
}
