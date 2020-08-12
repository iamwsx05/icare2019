using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmMedAndStorageMene 的摘要说明。
	/// </summary>
	public class frmMedAndStorageMene :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private PinkieControls.ButtonXP m_btnDelAll;
		private PinkieControls.ButtonXP m_btnAddAll;
		private PinkieControls.ButtonXP m_btnDel;
		private PinkieControls.ButtonXP m_btnAdd;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.ComboBox m_cboStorage;
		private System.Windows.Forms.GroupBox groupBox1;
		internal System.Windows.Forms.TextBox txtMedType;
		internal System.Windows.Forms.TextBox txtMedCode;
		internal System.Windows.Forms.TextBox txtMedName;
		internal PinkieControls.ButtonXP btnFind;
		internal PinkieControls.ButtonXP btnReturn;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		public com.digitalwave.controls.datagrid.ctlDataGrid ctlDgMed;
		internal com.digitalwave.controls.datagrid.ctlDataGrid ctlDgStorageMed;
		private PinkieControls.ButtonXP btnEsc;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMedAndStorageMene()
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
			this.ctlDgMed = new com.digitalwave.controls.datagrid.ctlDataGrid();
			this.m_btnDelAll = new PinkieControls.ButtonXP();
			this.m_btnAddAll = new PinkieControls.ButtonXP();
			this.m_btnDel = new PinkieControls.ButtonXP();
			this.m_btnAdd = new PinkieControls.ButtonXP();
			this.ctlDgStorageMed = new com.digitalwave.controls.datagrid.ctlDataGrid();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.m_cboStorage = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnReturn = new PinkieControls.ButtonXP();
			this.btnFind = new PinkieControls.ButtonXP();
			this.txtMedName = new System.Windows.Forms.TextBox();
			this.txtMedCode = new System.Windows.Forms.TextBox();
			this.txtMedType = new System.Windows.Forms.TextBox();
			this.btnEsc = new PinkieControls.ButtonXP();
			((System.ComponentModel.ISupportInitialize)(this.ctlDgMed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ctlDgStorageMed)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctlDgMed
			// 
			this.ctlDgMed.AllowAddNew = false;
			this.ctlDgMed.AllowDelete = false;
			this.ctlDgMed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.ctlDgMed.AutoAppendRow = false;
			this.ctlDgMed.AutoScroll = true;
			this.ctlDgMed.BackgroundColor = System.Drawing.SystemColors.Window;
			this.ctlDgMed.CaptionText = "";
			this.ctlDgMed.CaptionVisible = false;
			this.ctlDgMed.ColumnHeadersVisible = true;
			clsColumnInfo1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo1.BackColor = System.Drawing.Color.White;
			clsColumnInfo1.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo1.ColumnIndex = 0;
			clsColumnInfo1.ColumnName = "ASSISTCODE_CHR";
			clsColumnInfo1.ColumnWidth = 100;
			clsColumnInfo1.Enabled = false;
			clsColumnInfo1.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo1.HeadText = "药品助记码";
			clsColumnInfo1.ReadOnly = true;
			clsColumnInfo1.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo2.BackColor = System.Drawing.Color.White;
			clsColumnInfo2.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo2.ColumnIndex = 1;
			clsColumnInfo2.ColumnName = "MEDICINENAME_VCHR";
			clsColumnInfo2.ColumnWidth = 170;
			clsColumnInfo2.Enabled = false;
			clsColumnInfo2.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo2.HeadText = "药品名称";
			clsColumnInfo2.ReadOnly = true;
			clsColumnInfo2.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo3.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo3.BackColor = System.Drawing.Color.White;
			clsColumnInfo3.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo3.ColumnIndex = 2;
			clsColumnInfo3.ColumnName = "MEDICINETYPENAME_VCHR";
			clsColumnInfo3.ColumnWidth = 75;
			clsColumnInfo3.Enabled = false;
			clsColumnInfo3.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo3.HeadText = "药品类型";
			clsColumnInfo3.ReadOnly = true;
			clsColumnInfo3.TextFont = new System.Drawing.Font("宋体", 10F);
			this.ctlDgMed.Columns.Add(clsColumnInfo1);
			this.ctlDgMed.Columns.Add(clsColumnInfo2);
			this.ctlDgMed.Columns.Add(clsColumnInfo3);
			this.ctlDgMed.FullRowSelect = true;
			this.ctlDgMed.Location = new System.Drawing.Point(0, 32);
			this.ctlDgMed.MultiSelect = false;
			this.ctlDgMed.Name = "ctlDgMed";
			this.ctlDgMed.ReadOnly = true;
			this.ctlDgMed.RowHeadersVisible = false;
			this.ctlDgMed.RowHeaderWidth = 35;
			this.ctlDgMed.SelectedRowBackColor = System.Drawing.Color.Purple;
			this.ctlDgMed.SelectedRowForeColor = System.Drawing.Color.White;
			this.ctlDgMed.Size = new System.Drawing.Size(376, 528);
			this.ctlDgMed.TabIndex = 0;
			// 
			// m_btnDelAll
			// 
			this.m_btnDelAll.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnDelAll.DefaultScheme = true;
			this.m_btnDelAll.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnDelAll.Hint = "";
			this.m_btnDelAll.Location = new System.Drawing.Point(448, 472);
			this.m_btnDelAll.Name = "m_btnDelAll";
			this.m_btnDelAll.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnDelAll.Size = new System.Drawing.Size(104, 32);
			this.m_btnDelAll.TabIndex = 69;
			this.m_btnDelAll.Text = "<<";
			this.m_btnDelAll.Click += new System.EventHandler(this.m_btnDelAll_Click);
			// 
			// m_btnAddAll
			// 
			this.m_btnAddAll.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnAddAll.DefaultScheme = true;
			this.m_btnAddAll.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnAddAll.Hint = "";
			this.m_btnAddAll.Location = new System.Drawing.Point(448, 424);
			this.m_btnAddAll.Name = "m_btnAddAll";
			this.m_btnAddAll.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnAddAll.Size = new System.Drawing.Size(104, 32);
			this.m_btnAddAll.TabIndex = 68;
			this.m_btnAddAll.Text = ">>";
			this.m_btnAddAll.Click += new System.EventHandler(this.m_btnAddAll_Click);
			// 
			// m_btnDel
			// 
			this.m_btnDel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnDel.DefaultScheme = true;
			this.m_btnDel.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnDel.Hint = "";
			this.m_btnDel.Location = new System.Drawing.Point(448, 376);
			this.m_btnDel.Name = "m_btnDel";
			this.m_btnDel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnDel.Size = new System.Drawing.Size(104, 32);
			this.m_btnDel.TabIndex = 67;
			this.m_btnDel.Text = "<";
			this.m_btnDel.Click += new System.EventHandler(this.m_btnDel_Click);
			// 
			// m_btnAdd
			// 
			this.m_btnAdd.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnAdd.DefaultScheme = true;
			this.m_btnAdd.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnAdd.Hint = "";
			this.m_btnAdd.Location = new System.Drawing.Point(448, 328);
			this.m_btnAdd.Name = "m_btnAdd";
			this.m_btnAdd.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnAdd.Size = new System.Drawing.Size(104, 32);
			this.m_btnAdd.TabIndex = 66;
			this.m_btnAdd.Text = ">";
			this.m_btnAdd.Click += new System.EventHandler(this.m_btnAdd_Click);
			// 
			// ctlDgStorageMed
			// 
			this.ctlDgStorageMed.AllowAddNew = true;
			this.ctlDgStorageMed.AllowDelete = true;
			this.ctlDgStorageMed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.ctlDgStorageMed.AutoAppendRow = false;
			this.ctlDgStorageMed.AutoScroll = true;
			this.ctlDgStorageMed.BackgroundColor = System.Drawing.SystemColors.Window;
			this.ctlDgStorageMed.CaptionText = "";
			this.ctlDgStorageMed.CaptionVisible = false;
			this.ctlDgStorageMed.ColumnHeadersVisible = true;
			clsColumnInfo4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo4.BackColor = System.Drawing.Color.White;
			clsColumnInfo4.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo4.ColumnIndex = 0;
			clsColumnInfo4.ColumnName = "ASSISTCODE_CHR";
			clsColumnInfo4.ColumnWidth = 100;
			clsColumnInfo4.Enabled = false;
			clsColumnInfo4.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo4.HeadText = "药品助记码";
			clsColumnInfo4.ReadOnly = true;
			clsColumnInfo4.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo5.BackColor = System.Drawing.Color.White;
			clsColumnInfo5.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo5.ColumnIndex = 1;
			clsColumnInfo5.ColumnName = "MEDICINENAME_VCHR";
			clsColumnInfo5.ColumnWidth = 200;
			clsColumnInfo5.Enabled = false;
			clsColumnInfo5.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo5.HeadText = "药品名称";
			clsColumnInfo5.ReadOnly = true;
			clsColumnInfo5.TextFont = new System.Drawing.Font("宋体", 10F);
			clsColumnInfo6.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			clsColumnInfo6.BackColor = System.Drawing.Color.White;
			clsColumnInfo6.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
			clsColumnInfo6.ColumnIndex = 2;
			clsColumnInfo6.ColumnName = "MEDICINETYPENAME_VCHR";
			clsColumnInfo6.ColumnWidth = 75;
			clsColumnInfo6.Enabled = false;
			clsColumnInfo6.ForeColor = System.Drawing.Color.Black;
			clsColumnInfo6.HeadText = "药品类型";
			clsColumnInfo6.ReadOnly = true;
			clsColumnInfo6.TextFont = new System.Drawing.Font("宋体", 10F);
			this.ctlDgStorageMed.Columns.Add(clsColumnInfo4);
			this.ctlDgStorageMed.Columns.Add(clsColumnInfo5);
			this.ctlDgStorageMed.Columns.Add(clsColumnInfo6);
			this.ctlDgStorageMed.FullRowSelect = true;
			this.ctlDgStorageMed.Location = new System.Drawing.Point(600, 32);
			this.ctlDgStorageMed.MultiSelect = false;
			this.ctlDgStorageMed.Name = "ctlDgStorageMed";
			this.ctlDgStorageMed.ReadOnly = true;
			this.ctlDgStorageMed.RowHeadersVisible = false;
			this.ctlDgStorageMed.RowHeaderWidth = 35;
			this.ctlDgStorageMed.SelectedRowBackColor = System.Drawing.Color.Purple;
			this.ctlDgStorageMed.SelectedRowForeColor = System.Drawing.Color.White;
			this.ctlDgStorageMed.Size = new System.Drawing.Size(408, 528);
			this.ctlDgStorageMed.TabIndex = 70;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(63, 19);
			this.label3.TabIndex = 71;
			this.label3.Text = "药品列表";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(600, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 19);
			this.label1.TabIndex = 73;
			this.label1.Text = "药库";
			// 
			// m_cboStorage
			// 
			this.m_cboStorage.Location = new System.Drawing.Point(640, 6);
			this.m_cboStorage.Name = "m_cboStorage";
			this.m_cboStorage.Size = new System.Drawing.Size(144, 22);
			this.m_cboStorage.TabIndex = 72;
			this.m_cboStorage.SelectedIndexChanged += new System.EventHandler(this.m_cboStorage_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.btnReturn);
			this.groupBox1.Controls.Add(this.btnFind);
			this.groupBox1.Controls.Add(this.txtMedName);
			this.groupBox1.Controls.Add(this.txtMedCode);
			this.groupBox1.Controls.Add(this.txtMedType);
			this.groupBox1.Location = new System.Drawing.Point(384, 80);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(208, 184);
			this.groupBox1.TabIndex = 74;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "查找药品";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 64);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 23);
			this.label5.TabIndex = 150;
			this.label5.Text = "助 记 码";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 104);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 23);
			this.label4.TabIndex = 149;
			this.label4.Text = "药品名称";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 23);
			this.label2.TabIndex = 148;
			this.label2.Text = "药品类型";
			// 
			// btnReturn
			// 
			this.btnReturn.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btnReturn.DefaultScheme = true;
			this.btnReturn.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnReturn.Hint = "";
			this.btnReturn.Location = new System.Drawing.Point(112, 152);
			this.btnReturn.Name = "btnReturn";
			this.btnReturn.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btnReturn.Size = new System.Drawing.Size(80, 23);
			this.btnReturn.TabIndex = 147;
			this.btnReturn.TabStop = false;
			this.btnReturn.Text = "返回(&R)";
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			// 
			// btnFind
			// 
			this.btnFind.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btnFind.DefaultScheme = true;
			this.btnFind.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnFind.Hint = "";
			this.btnFind.Location = new System.Drawing.Point(24, 152);
			this.btnFind.Name = "btnFind";
			this.btnFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btnFind.Size = new System.Drawing.Size(80, 23);
			this.btnFind.TabIndex = 146;
			this.btnFind.TabStop = false;
			this.btnFind.Text = "查找(&S)";
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// txtMedName
			// 
			this.txtMedName.Location = new System.Drawing.Point(80, 104);
			this.txtMedName.Name = "txtMedName";
			this.txtMedName.Size = new System.Drawing.Size(120, 23);
			this.txtMedName.TabIndex = 2;
			this.txtMedName.Text = "";
			// 
			// txtMedCode
			// 
			this.txtMedCode.Location = new System.Drawing.Point(80, 64);
			this.txtMedCode.Name = "txtMedCode";
			this.txtMedCode.Size = new System.Drawing.Size(120, 23);
			this.txtMedCode.TabIndex = 1;
			this.txtMedCode.Text = "";
			// 
			// txtMedType
			// 
			this.txtMedType.Location = new System.Drawing.Point(80, 32);
			this.txtMedType.Name = "txtMedType";
			this.txtMedType.Size = new System.Drawing.Size(120, 23);
			this.txtMedType.TabIndex = 0;
			this.txtMedType.Text = "";
			// 
			// btnEsc
			// 
			this.btnEsc.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btnEsc.DefaultScheme = true;
			this.btnEsc.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnEsc.Hint = "";
			this.btnEsc.Location = new System.Drawing.Point(384, 520);
			this.btnEsc.Name = "btnEsc";
			this.btnEsc.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btnEsc.Size = new System.Drawing.Size(208, 32);
			this.btnEsc.TabIndex = 75;
			this.btnEsc.Text = "退出(&E)";
			this.btnEsc.Click += new System.EventHandler(this.btnEsc_Click);
			// 
			// frmMedAndStorageMene
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(1008, 565);
			this.Controls.Add(this.btnEsc);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.m_btnDelAll);
			this.Controls.Add(this.m_btnAddAll);
			this.Controls.Add(this.m_btnDel);
			this.Controls.Add(this.m_btnAdd);
			this.Controls.Add(this.m_cboStorage);
			this.Controls.Add(this.ctlDgStorageMed);
			this.Controls.Add(this.ctlDgMed);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.Name = "frmMedAndStorageMene";
			this.Text = "药库药品维护";
			this.Load += new System.EventHandler(this.frmMedAndStorageMene_Load);
			((System.ComponentModel.ISupportInitialize)(this.ctlDgMed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ctlDgStorageMed)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsControlMedAddStorageMene();
			objController.Set_GUI_Apperance(this);
		}

		private void frmMedAndStorageMene_Load(object sender, System.EventArgs e)
		{
		    ((clsControlMedAddStorageMene)this.objController).m_lngFrmLoad();
		}

		private void btnFind_Click(object sender, System.EventArgs e)
		{
			((clsControlMedAddStorageMene)this.objController).m_lngFindData();
		}

		private void m_cboStorage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlMedAddStorageMene)this.objController).m_lngChangItem();
		}

		private void m_btnAddAll_Click(object sender, System.EventArgs e)
		{
			((clsControlMedAddStorageMene)this.objController).m_lngAddNewAll();
		}

		private void m_btnDelAll_Click(object sender, System.EventArgs e)
		{
			((clsControlMedAddStorageMene)this.objController).m_lngDelData();
		}

		private void m_btnDel_Click(object sender, System.EventArgs e)
		{
			((clsControlMedAddStorageMene)this.objController).m_lngDeleOneFromStorage();
		}

		private void m_btnAdd_Click(object sender, System.EventArgs e)
		{
			((clsControlMedAddStorageMene)this.objController).m_lngAddNewNeoData();
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			((clsControlMedAddStorageMene)this.objController).m_lngReturnClick();
		}

		private void btnEsc_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
