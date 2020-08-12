using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
//using System.EnterpriseServices;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmMedAndStorage 的摘要说明。
	/// </summary>
	public class frmMedAndStorage :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		internal System.Windows.Forms.ListView m_lvMed;
		private System.Windows.Forms.ColumnHeader col1;
		private System.Windows.Forms.ColumnHeader col2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private PinkieControls.ButtonXP m_btnAdd;
		private PinkieControls.ButtonXP m_btnDel;
		private PinkieControls.ButtonXP m_btnAddAll;
		private PinkieControls.ButtonXP m_btnDelAll;
		internal System.Windows.Forms.ListView m_lvw;
		internal System.Windows.Forms.ComboBox m_cboStorage;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.StatusBar m_stBar;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;

		public frmMedAndStorage()
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
			this.m_lvMed = new System.Windows.Forms.ListView();
			this.col1 = new System.Windows.Forms.ColumnHeader();
			this.col2 = new System.Windows.Forms.ColumnHeader();
			this.m_lvw = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.m_cboStorage = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.m_btnAdd = new PinkieControls.ButtonXP();
			this.m_btnDel = new PinkieControls.ButtonXP();
			this.m_btnAddAll = new PinkieControls.ButtonXP();
			this.m_btnDelAll = new PinkieControls.ButtonXP();
			this.m_stBar = new System.Windows.Forms.StatusBar();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// m_lvMed
			// 
			this.m_lvMed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.m_lvMed.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.col1,
																					  this.col2});
			this.m_lvMed.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_lvMed.FullRowSelect = true;
			this.m_lvMed.GridLines = true;
			this.m_lvMed.HideSelection = false;
			this.m_lvMed.Location = new System.Drawing.Point(0, 40);
			this.m_lvMed.MultiSelect = false;
			this.m_lvMed.Name = "m_lvMed";
			this.m_lvMed.Size = new System.Drawing.Size(208, 316);
			this.m_lvMed.TabIndex = 56;
			this.m_lvMed.View = System.Windows.Forms.View.Details;
			this.m_lvMed.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_lvMed_ColumnClick);
			// 
			// col1
			// 
			this.col1.Text = "药品编号";
			this.col1.Width = 80;
			// 
			// col2
			// 
			this.col2.Text = "名称";
			this.col2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col2.Width = 121;
			// 
			// m_lvw
			// 
			this.m_lvw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lvw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					this.columnHeader1,
																					this.columnHeader2});
			this.m_lvw.FullRowSelect = true;
			this.m_lvw.Location = new System.Drawing.Point(272, 72);
			this.m_lvw.Name = "m_lvw";
			this.m_lvw.Size = new System.Drawing.Size(424, 280);
			this.m_lvw.TabIndex = 57;
			this.m_lvw.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "药品编号";
			this.columnHeader1.Width = 100;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "名称";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader2.Width = 126;
			// 
			// m_cboStorage
			// 
			this.m_cboStorage.Location = new System.Drawing.Point(328, 13);
			this.m_cboStorage.Name = "m_cboStorage";
			this.m_cboStorage.Size = new System.Drawing.Size(144, 22);
			this.m_cboStorage.TabIndex = 58;
			this.m_cboStorage.SelectedIndexChanged += new System.EventHandler(this.m_cboStorage_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(288, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 19);
			this.label1.TabIndex = 59;
			this.label1.Text = "药库";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(272, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 19);
			this.label2.TabIndex = 60;
			this.label2.Text = "现有药品";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 15);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(63, 19);
			this.label3.TabIndex = 61;
			this.label3.Text = "药品列表";
			// 
			// m_btnAdd
			// 
			this.m_btnAdd.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnAdd.DefaultScheme = true;
			this.m_btnAdd.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnAdd.Hint = "";
			this.m_btnAdd.Location = new System.Drawing.Point(216, 112);
			this.m_btnAdd.Name = "m_btnAdd";
			this.m_btnAdd.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnAdd.Size = new System.Drawing.Size(40, 24);
			this.m_btnAdd.TabIndex = 62;
			this.m_btnAdd.Text = ">";
			this.toolTip1.SetToolTip(this.m_btnAdd, "添加");
			this.m_btnAdd.Click += new System.EventHandler(this.m_btnAdd_Click);
			// 
			// m_btnDel
			// 
			this.m_btnDel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnDel.DefaultScheme = true;
			this.m_btnDel.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnDel.Hint = "";
			this.m_btnDel.Location = new System.Drawing.Point(216, 160);
			this.m_btnDel.Name = "m_btnDel";
			this.m_btnDel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnDel.Size = new System.Drawing.Size(40, 24);
			this.m_btnDel.TabIndex = 63;
			this.m_btnDel.Text = "<";
			this.toolTip1.SetToolTip(this.m_btnDel, "移除");
			this.m_btnDel.Click += new System.EventHandler(this.m_btnDel_Click);
			// 
			// m_btnAddAll
			// 
			this.m_btnAddAll.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnAddAll.DefaultScheme = true;
			this.m_btnAddAll.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnAddAll.Hint = "";
			this.m_btnAddAll.Location = new System.Drawing.Point(216, 208);
			this.m_btnAddAll.Name = "m_btnAddAll";
			this.m_btnAddAll.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnAddAll.Size = new System.Drawing.Size(40, 24);
			this.m_btnAddAll.TabIndex = 64;
			this.m_btnAddAll.Text = ">>";
			this.toolTip1.SetToolTip(this.m_btnAddAll, "全部添加");
			this.m_btnAddAll.Click += new System.EventHandler(this.m_btnAddAll_Click);
			// 
			// m_btnDelAll
			// 
			this.m_btnDelAll.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnDelAll.DefaultScheme = true;
			this.m_btnDelAll.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnDelAll.Hint = "";
			this.m_btnDelAll.Location = new System.Drawing.Point(216, 256);
			this.m_btnDelAll.Name = "m_btnDelAll";
			this.m_btnDelAll.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnDelAll.Size = new System.Drawing.Size(40, 24);
			this.m_btnDelAll.TabIndex = 65;
			this.m_btnDelAll.Text = "<<";
			this.toolTip1.SetToolTip(this.m_btnDelAll, "全部移除");
			this.m_btnDelAll.Click += new System.EventHandler(this.m_btnDelAll_Click);
			// 
			// m_stBar
			// 
			this.m_stBar.Location = new System.Drawing.Point(0, 359);
			this.m_stBar.Name = "m_stBar";
			this.m_stBar.Size = new System.Drawing.Size(760, 22);
			this.m_stBar.TabIndex = 66;
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 100;
			this.toolTip1.AutoPopDelay = 1000;
			this.toolTip1.InitialDelay = 100;
			this.toolTip1.ReshowDelay = 1;
			// 
			// frmMedAndStorage
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(760, 381);
			this.Controls.Add(this.m_stBar);
			this.Controls.Add(this.m_btnDelAll);
			this.Controls.Add(this.m_btnAddAll);
			this.Controls.Add(this.m_btnDel);
			this.Controls.Add(this.m_btnAdd);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.m_cboStorage);
			this.Controls.Add(this.m_lvw);
			this.Controls.Add(this.m_lvMed);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.KeyPreview = true;
			this.Name = "frmMedAndStorage";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "仓库药品维护";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMedAndStorage_KeyDown);
			this.Load += new System.EventHandler(this.frmMedAndStorage_Load);
			this.ResumeLayout(false);

		}
		#endregion
		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsControlMedAndStorage();
			objController.Set_GUI_Apperance(this);
		}

		private void frmMedAndStorage_Load(object sender, System.EventArgs e)
		{
		  ((clsControlMedAndStorage)this.objController).FillStorage();
//          ((clsControlMedAndStorage)this.objController).GetMedicineList();
		}
		private void m_cboStorage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		  this.m_stBar.Text="正在加载数据，请稍候..";
		   ((clsControlMedAndStorage)this.objController).GetMedByStoID();
          this.m_stBar.Text="按Esc键关闭窗体";
		}

		private void m_btnAdd_Click(object sender, System.EventArgs e)
		{
		  ((clsControlMedAndStorage)this.objController).m_lngAdd();
		}

		private void m_btnDel_Click(object sender, System.EventArgs e)
		{
		   ((clsControlMedAndStorage)this.objController).m_lngDel();
		}

		private void m_btnAddAll_Click(object sender, System.EventArgs e)
		{
			((clsControlMedAndStorage)this.objController).m_lngAddAll();
		}

		private void m_btnDelAll_Click(object sender, System.EventArgs e)
		{
		    ((clsControlMedAndStorage)this.objController).m_lngDelAll();
		}

		private void m_lvMed_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			bool IsAsc=false;//是否为升序
			if(m_lvMed.Sorting==SortOrder.Ascending)
				m_lvMed.Sorting=SortOrder.Descending;
			else
			{
				m_lvMed.Sorting=SortOrder.Ascending;
				IsAsc=true;
			}
			m_lvMed.ListViewItemSorter=new ListViewItemComparer(e.Column,IsAsc,m_lvMed);
			m_lvMed.Sort();
//			m_lvMed.Columns[e.Column].Text="药品编号 ↑↓";

			//m_lvMed.Refresh();
		}

		private void frmMedAndStorage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Escape)
				this.Close();
		}
	}
}
