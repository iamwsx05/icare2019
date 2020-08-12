using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// /// 药价列表 Create by Sam 2004-5-24
	/// </summary>
	public class frmMedPriceList :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.ToolBar m_tb;
		private System.Windows.Forms.ToolBarButton Add;
		private System.Windows.Forms.ToolBarButton Del;
		private System.Windows.Forms.ToolBarButton Esc;
		private System.Windows.Forms.ToolBarButton Find;
		private System.Windows.Forms.ToolBarButton reNew;
		private System.Windows.Forms.ToolBarButton Edit;
//		private bool IsReturn=false;
//		private string ReturnID=null;
//		private string ReturnName=null;
		internal System.Windows.Forms.TabControl m_tab;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		internal System.Windows.Forms.ListView m_lvMed;
		private System.Windows.Forms.ColumnHeader col1;
		private System.Windows.Forms.ColumnHeader col2;
		private System.Windows.Forms.ColumnHeader col3;
		private System.Windows.Forms.ColumnHeader col4;
		private System.Windows.Forms.ColumnHeader col5;
		private System.Windows.Forms.ColumnHeader col6;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		internal System.Windows.Forms.ListView m_lvwHistory;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMedPriceList()
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
				if (components != null) 
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
			this.m_tb = new System.Windows.Forms.ToolBar();
			this.Add = new System.Windows.Forms.ToolBarButton();
			this.Edit = new System.Windows.Forms.ToolBarButton();
			this.Del = new System.Windows.Forms.ToolBarButton();
			this.Find = new System.Windows.Forms.ToolBarButton();
			this.reNew = new System.Windows.Forms.ToolBarButton();
			this.Esc = new System.Windows.Forms.ToolBarButton();
			this.m_tab = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.m_lvMed = new System.Windows.Forms.ListView();
			this.col1 = new System.Windows.Forms.ColumnHeader();
			this.col2 = new System.Windows.Forms.ColumnHeader();
			this.col3 = new System.Windows.Forms.ColumnHeader();
			this.col4 = new System.Windows.Forms.ColumnHeader();
			this.col5 = new System.Windows.Forms.ColumnHeader();
			this.col6 = new System.Windows.Forms.ColumnHeader();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.m_lvwHistory = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.m_tab.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_tb
			// 
			this.m_tb.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.m_tb.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					this.Add,
																					this.Edit,
																					this.Del,
																					this.Find,
																					this.reNew,
																					this.Esc});
			this.m_tb.DropDownArrows = true;
			this.m_tb.Location = new System.Drawing.Point(0, 0);
			this.m_tb.Name = "m_tb";
			this.m_tb.ShowToolTips = true;
			this.m_tb.Size = new System.Drawing.Size(776, 43);
			this.m_tb.TabIndex = 11;
			this.m_tb.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.m_tb_ButtonClick);
			// 
			// Add
			// 
			this.Add.Tag = "";
			this.Add.Text = "新增";
			this.Add.ToolTipText = "新增";
			// 
			// Edit
			// 
			this.Edit.Text = "编辑";
			this.Edit.ToolTipText = "编辑";
			// 
			// Del
			// 
			this.Del.Tag = "";
			this.Del.Text = "删除";
			this.Del.ToolTipText = "删除";
			// 
			// Find
			// 
			this.Find.Text = "查找";
			this.Find.ToolTipText = "查找";
			this.Find.Visible = false;
			// 
			// reNew
			// 
			this.reNew.Text = "刷新";
			this.reNew.ToolTipText = "刷新";
			// 
			// Esc
			// 
			this.Esc.Text = "关闭";
			this.Esc.ToolTipText = "关闭";
			// 
			// m_tab
			// 
			this.m_tab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_tab.Controls.Add(this.tabPage1);
			this.m_tab.Controls.Add(this.tabPage2);
			this.m_tab.Location = new System.Drawing.Point(-2, 48);
			this.m_tab.Name = "m_tab";
			this.m_tab.SelectedIndex = 0;
			this.m_tab.Size = new System.Drawing.Size(786, 408);
			this.m_tab.TabIndex = 12;
			this.m_tab.SelectedIndexChanged += new System.EventHandler(this.m_tab_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.m_lvMed);
			this.tabPage1.Location = new System.Drawing.Point(4, 23);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(778, 381);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "药品价格";
			// 
			// m_lvMed
			// 
			this.m_lvMed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lvMed.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.col1,
																					  this.col2,
																					  this.col3,
																					  this.col4,
																					  this.col5,
																					  this.col6});
			this.m_lvMed.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_lvMed.FullRowSelect = true;
			this.m_lvMed.GridLines = true;
			this.m_lvMed.Location = new System.Drawing.Point(-1, 0);
			this.m_lvMed.MultiSelect = false;
			this.m_lvMed.Name = "m_lvMed";
			this.m_lvMed.Size = new System.Drawing.Size(772, 378);
			this.m_lvMed.TabIndex = 1;
			this.m_lvMed.View = System.Windows.Forms.View.Details;
			this.m_lvMed.DoubleClick += new System.EventHandler(this.m_lvMed_DoubleClick);
			this.m_lvMed.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_lvMed_ColumnClick);
			// 
			// col1
			// 
			this.col1.Text = "药品编号";
			this.col1.Width = 100;
			// 
			// col2
			// 
			this.col2.Text = "药品名称";
			this.col2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col2.Width = 100;
			// 
			// col3
			// 
			this.col3.Text = "单位";
			this.col3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col3.Width = 100;
			// 
			// col4
			// 
			this.col4.Text = "起始时间";
			this.col4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col4.Width = 150;
			// 
			// col5
			// 
			this.col5.Text = "结束时间";
			this.col5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col5.Width = 150;
			// 
			// col6
			// 
			this.col6.Text = "正常价格";
			this.col6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col6.Width = 100;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.m_lvwHistory);
			this.tabPage2.Location = new System.Drawing.Point(4, 23);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(778, 381);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "价格历史";
			// 
			// m_lvwHistory
			// 
			this.m_lvwHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lvwHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						   this.columnHeader1,
																						   this.columnHeader2,
																						   this.columnHeader3,
																						   this.columnHeader4,
																						   this.columnHeader5,
																						   this.columnHeader6,
																						   this.columnHeader7});
			this.m_lvwHistory.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_lvwHistory.FullRowSelect = true;
			this.m_lvwHistory.GridLines = true;
			this.m_lvwHistory.Location = new System.Drawing.Point(0, 0);
			this.m_lvwHistory.MultiSelect = false;
			this.m_lvwHistory.Name = "m_lvwHistory";
			this.m_lvwHistory.Size = new System.Drawing.Size(772, 378);
			this.m_lvwHistory.TabIndex = 2;
			this.m_lvwHistory.View = System.Windows.Forms.View.Details;
			this.m_lvwHistory.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_lvwHistory_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "药品编号";
			this.columnHeader1.Width = 100;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "药品名称";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader2.Width = 100;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "单位";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader3.Width = 100;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "起始时间";
			this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader4.Width = 150;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "结束时间";
			this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader5.Width = 150;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "历史时间";
			this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader6.Width = 150;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "状态";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// frmMedPriceList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(776, 453);
			this.Controls.Add(this.m_tab);
			this.Controls.Add(this.m_tb);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.KeyPreview = true;
			this.Name = "frmMedPriceList";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "药品价格信息";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMedPriceList_KeyDown);
			this.Load += new System.EventHandler(this.frmMedPriceList_Load);
			this.m_tab.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

//		/// <summary>
//		/// 应用程序的主入口点。
//		/// </summary>
//		[STAThread]
//		static void Main() 
//		{
//			Application.Run(new frmMedicine());
//		}

		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsControlMedPriceList();
			objController.Set_GUI_Apperance(this);
		}

		private void m_tb_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch(this.m_tb.Buttons.IndexOf(e.Button))
			{
				case 0: //新增
					//					frmMedicineInfo frm=new frmMedicineInfo();
					//					frm.ShowDialog();
					((clsControlMedPriceList)this.objController).m_SetItem(true);
					break;
				case 1:
					((clsControlMedPriceList)this.objController).m_SetItem(false);
					break;
				case 2://删除
					((clsControlMedPriceList)this.objController).m_lngDelMedPrice();
					break;
				case 3://查找
					//((clsControlMedPriceList)this.objController).m_lngDelMedInfo();
					break;
				case 4:
					((clsControlMedPriceList)this.objController).GetMedPriceList();
					break;
				case 5: //退出
					this.Close();
					break;
     
			}
		}
		private void frmMedPriceList_Load(object sender, System.EventArgs e)
		{
			((clsControlMedPriceList)this.objController).GetMedPriceList();
		}

		private void m_tab_SelectedIndexChanged(object sender, System.EventArgs e)
		{
           if(m_tab.SelectedIndex==1)
			   ((clsControlMedPriceList)this.objController).GetMedPriceHistory();
		}

		private void m_lvMed_DoubleClick(object sender, System.EventArgs e)
		{
			((clsControlMedPriceList)this.objController).m_SetItem(false);
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
		}

		private void m_lvwHistory_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			bool IsAsc=false;//是否为升序
			if(m_lvwHistory.Sorting==SortOrder.Ascending)
				m_lvwHistory.Sorting=SortOrder.Descending;
			else
			{
				m_lvwHistory.Sorting=SortOrder.Ascending;
				IsAsc=true;
			}
			m_lvwHistory.ListViewItemSorter=new ListViewItemComparer(e.Column,IsAsc,m_lvwHistory);
			m_lvwHistory.Sort();
		}

		private void frmMedPriceList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Escape)
				this.Close();
		}
	}
}
