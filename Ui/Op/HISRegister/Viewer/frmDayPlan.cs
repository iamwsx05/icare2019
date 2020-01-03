using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// ҩƷ������Ϣ�б� Create by Sam 2004-5-24
	/// </summary>
	public class frmDayPlan :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.ContextMenu m_Menu;
		private System.Windows.Forms.MenuItem m_muToDay;
		private System.Windows.Forms.MenuItem m_muDay;
		private System.Windows.Forms.ContextMenu mu_M;
		private System.Windows.Forms.MenuItem mu_Add;
		private System.Windows.Forms.MenuItem mu_Edit;
		private System.Windows.Forms.MenuItem mu_Del;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ToolBar m_tb;
		private System.Windows.Forms.ToolBarButton Add;
		private System.Windows.Forms.ToolBarButton Edit;
		private System.Windows.Forms.ToolBarButton Del;
		private System.Windows.Forms.ToolBarButton Find;
		private System.Windows.Forms.ToolBarButton reNew;
		private System.Windows.Forms.ToolBarButton Re;
		private System.Windows.Forms.ToolBarButton Esc;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		internal System.Windows.Forms.TreeView m_TV;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		internal System.Windows.Forms.ListView m_lvwPlan;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.DateTimePicker m_DTP;
		private System.Windows.Forms.ColumnHeader col1;
		private System.Windows.Forms.ColumnHeader col2;
		private System.Windows.Forms.ColumnHeader col3;
		private System.Windows.Forms.ColumnHeader col4;
		private System.Windows.Forms.ColumnHeader col5;
		private System.Windows.Forms.ColumnHeader col6;
		private System.Windows.Forms.ColumnHeader col7;
		private System.Windows.Forms.ColumnHeader col8;
		private System.Windows.Forms.ColumnHeader col9;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmDayPlan()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.m_Menu = new System.Windows.Forms.ContextMenu();
			this.m_muToDay = new System.Windows.Forms.MenuItem();
			this.m_muDay = new System.Windows.Forms.MenuItem();
			this.mu_M = new System.Windows.Forms.ContextMenu();
			this.mu_Add = new System.Windows.Forms.MenuItem();
			this.mu_Edit = new System.Windows.Forms.MenuItem();
			this.mu_Del = new System.Windows.Forms.MenuItem();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.m_tb = new System.Windows.Forms.ToolBar();
			this.Add = new System.Windows.Forms.ToolBarButton();
			this.Edit = new System.Windows.Forms.ToolBarButton();
			this.Del = new System.Windows.Forms.ToolBarButton();
			this.Find = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.reNew = new System.Windows.Forms.ToolBarButton();
			this.Re = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.Esc = new System.Windows.Forms.ToolBarButton();
			this.m_TV = new System.Windows.Forms.TreeView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.m_lvwPlan = new System.Windows.Forms.ListView();
			this.col9 = new System.Windows.Forms.ColumnHeader();
			this.col1 = new System.Windows.Forms.ColumnHeader();
			this.col2 = new System.Windows.Forms.ColumnHeader();
			this.col3 = new System.Windows.Forms.ColumnHeader();
			this.col4 = new System.Windows.Forms.ColumnHeader();
			this.col5 = new System.Windows.Forms.ColumnHeader();
			this.col6 = new System.Windows.Forms.ColumnHeader();
			this.col7 = new System.Windows.Forms.ColumnHeader();
			this.col8 = new System.Windows.Forms.ColumnHeader();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.m_DTP = new System.Windows.Forms.DateTimePicker();
			this.panel3.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_Menu
			// 
			this.m_Menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.m_muToDay,
																				   this.m_muDay});
			// 
			// m_muToDay
			// 
			this.m_muToDay.Index = 0;
			this.m_muToDay.Text = "����";
			this.m_muToDay.Click += new System.EventHandler(this.m_muToDay_Click);
			// 
			// m_muDay
			// 
			this.m_muDay.Index = 1;
			this.m_muDay.Text = "ָ������";
			this.m_muDay.Click += new System.EventHandler(this.m_muDay_Click);
			// 
			// mu_M
			// 
			this.mu_M.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				 this.mu_Add,
																				 this.mu_Edit,
																				 this.mu_Del});
			// 
			// mu_Add
			// 
			this.mu_Add.Index = 0;
			this.mu_Add.Text = "����";
			this.mu_Add.Click += new System.EventHandler(this.mu_Add_Click);
			// 
			// mu_Edit
			// 
			this.mu_Edit.Index = 1;
			this.mu_Edit.Text = "�޸�";
			this.mu_Edit.Click += new System.EventHandler(this.mu_Edit_Click);
			// 
			// mu_Del
			// 
			this.mu_Del.Index = 2;
			this.mu_Del.Text = "ɾ��";
			this.mu_Del.Click += new System.EventHandler(this.mu_Del_Click);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.m_tb);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(912, 48);
			this.panel3.TabIndex = 15;
			// 
			// label2
			// 
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label2.Location = new System.Drawing.Point(0, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(912, 2);
			this.label2.TabIndex = 13;
			this.label2.Text = "label2";
			// 
			// m_tb
			// 
			this.m_tb.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.m_tb.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					this.Add,
																					this.Edit,
																					this.Del,
																					this.Find,
																					this.toolBarButton1,
																					this.reNew,
																					this.Re,
																					this.toolBarButton2,
																					this.Esc});
			this.m_tb.DropDownArrows = true;
			this.m_tb.Location = new System.Drawing.Point(0, 0);
			this.m_tb.Name = "m_tb";
			this.m_tb.ShowToolTips = true;
			this.m_tb.Size = new System.Drawing.Size(912, 43);
			this.m_tb.TabIndex = 12;
			this.m_tb.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.m_tb_ButtonClick);
			// 
			// Add
			// 
			this.Add.DropDownMenu = this.m_Menu;
			this.Add.Tag = "";
			this.Add.Text = " ����";
			this.Add.ToolTipText = "����";
			// 
			// Edit
			// 
			this.Edit.Text = "�༭";
			this.Edit.ToolTipText = "�༭";
			// 
			// Del
			// 
			this.Del.Tag = "";
			this.Del.Text = "ɾ��";
			this.Del.ToolTipText = "ɾ��";
			// 
			// Find
			// 
			this.Find.Text = "����";
			this.Find.ToolTipText = "����";
			this.Find.Visible = false;
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// reNew
			// 
			this.reNew.Text = "ˢ��";
			this.reNew.ToolTipText = "ˢ��";
			// 
			// Re
			// 
			this.Re.DropDownMenu = this.m_Menu;
			this.Re.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			this.Re.Text = "����";
			this.Re.ToolTipText = "�����ܼƻ�";
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// Esc
			// 
			this.Esc.Text = " �ر� ";
			this.Esc.ToolTipText = "�ر�";
			// 
			// m_TV
			// 
			this.m_TV.Dock = System.Windows.Forms.DockStyle.Left;
			this.m_TV.ForeColor = System.Drawing.SystemColors.WindowText;
			this.m_TV.HideSelection = false;
			this.m_TV.HotTracking = true;
			this.m_TV.ImageIndex = -1;
			this.m_TV.Location = new System.Drawing.Point(0, 48);
			this.m_TV.Name = "m_TV";
			this.m_TV.SelectedImageIndex = -1;
			this.m_TV.Size = new System.Drawing.Size(184, 501);
			this.m_TV.TabIndex = 17;
			this.m_TV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_TV_AfterSelect);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(184, 48);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 501);
			this.splitter1.TabIndex = 18;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.m_lvwPlan);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(187, 48);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(725, 501);
			this.panel1.TabIndex = 19;
			// 
			// m_lvwPlan
			// 
			this.m_lvwPlan.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.col9,
																						this.col1,
																						this.col2,
																						this.col3,
																						this.col4,
																						this.col5,
																						this.col6,
																						this.col7,
																						this.col8});
			this.m_lvwPlan.ContextMenu = this.mu_M;
			this.m_lvwPlan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lvwPlan.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_lvwPlan.FullRowSelect = true;
			this.m_lvwPlan.GridLines = true;
			this.m_lvwPlan.Location = new System.Drawing.Point(0, 32);
			this.m_lvwPlan.MultiSelect = false;
			this.m_lvwPlan.Name = "m_lvwPlan";
			this.m_lvwPlan.Size = new System.Drawing.Size(725, 469);
			this.m_lvwPlan.TabIndex = 19;
			this.m_lvwPlan.View = System.Windows.Forms.View.Details;
			this.m_lvwPlan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_lvwPlan_MouseDown);
			this.m_lvwPlan.DoubleClick += new System.EventHandler(this.m_lvwPlan_DoubleClick);
			this.m_lvwPlan.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_lvwPlan_ColumnClick);
			// 
			// col9
			// 
			this.col9.Text = "�������";
			this.col9.Width = 110;
			// 
			// col1
			// 
			this.col1.Text = "ҽ�����";
			this.col1.Width = 100;
			// 
			// col2
			// 
			this.col2.Text = "����";
			this.col2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col2.Width = 100;
			// 
			// col3
			// 
			this.col3.Text = "��������";
			this.col3.Width = 88;
			// 
			// col4
			// 
			this.col4.Text = "����ʱ���";
			this.col4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col4.Width = 100;
			// 
			// col5
			// 
			this.col5.Text = "��ʼʱ��";
			this.col5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col5.Width = 100;
			// 
			// col6
			// 
			this.col6.Text = "����ʱ��";
			this.col6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col6.Width = 100;
			// 
			// col7
			// 
			this.col7.Text = "���";
			// 
			// col8
			// 
			this.col8.Text = "�޺�";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.m_DTP);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(725, 32);
			this.panel2.TabIndex = 0;
			this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 21);
			this.label1.TabIndex = 1;
			this.label1.Text = "�Ű�����";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// m_DTP
			// 
			this.m_DTP.Location = new System.Drawing.Point(80, 8);
			this.m_DTP.Name = "m_DTP";
			this.m_DTP.Size = new System.Drawing.Size(120, 23);
			this.m_DTP.TabIndex = 0;
			this.m_DTP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_DTP_KeyPress);
			this.m_DTP.ValueChanged += new System.EventHandler(this.m_DTP_ValueChanged);
			// 
			// frmDayPlan
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(912, 549);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.m_TV);
			this.Controls.Add(this.panel3);
			this.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.KeyPreview = true;
			this.Name = "frmDayPlan";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "�������Ű�ƻ�";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDayPlan_KeyDown);
			this.Load += new System.EventHandler(this.frmDayPlan_Load);
			this.panel3.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Ӧ�ó��������ڵ㡣
		/// </summary>
//		[STAThread]
//		static void Main() 
//		{
//			Application.Run(new frmDayPlan());
//		}

		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsControlDayPlan();
			objController.Set_GUI_Apperance(this);
		}

		private void m_tb_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			
			switch(this.m_tb.Buttons.IndexOf(e.Button))
			{
				case 0: //����
					((clsControlDayPlan)this.objController).m_SetItem(true);
//					((clsControlMedicine)this.objController).m_SetItem(true);
					break;
                case 1:
					((clsControlDayPlan)this.objController).m_SetItem(false);
					break;
                case 2://ɾ��
					((clsControlDayPlan)this.objController).m_lngDelPlan();
					break;
				case 3://����
					//((clsControlMedicine)this.objController).m_lngDelMedInfo();
					break;
				case 5:
                    ((clsControlDayPlan)this.objController).m_GetPlanByDepID();
					break;
				case 6:
					this.m_muToDay_Click(sender,e);
					break; 
                case 8: //�˳�
					this.Close();
					break;
			}
		}

		private void m_lvwPlan_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			bool IsAsc=false;//�Ƿ�Ϊ����
			if(m_lvwPlan.Sorting==SortOrder.Ascending)
				m_lvwPlan.Sorting=SortOrder.Descending;
			else
			{
				m_lvwPlan.Sorting=SortOrder.Ascending;
				IsAsc=true;
			}
			m_lvwPlan.ListViewItemSorter=new ListViewItemComparer(e.Column,IsAsc,m_lvwPlan);
			m_lvwPlan.Sort();
		}

		

		private void frmDayPlan_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Escape)
			{
					if(MessageBox.Show("ȷ���˳���?","iCare",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
						return;
					this.Close();
			}
		}

		private void frmDayPlan_Load(object sender, System.EventArgs e)
		{
            ((clsControlDayPlan)this.objController).fillDepartTree();
			this.m_DTP.Value=DateTime.Now.Date;
		}

		private void m_TV_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			((clsControlDayPlan)this.objController).m_GetPlanByDepID();
		}

		private void m_lvwPlan_DoubleClick(object sender, System.EventArgs e)
		{
			((clsControlDayPlan)this.objController).m_SetItem(false);
		}

		private void m_DTP_ValueChanged(object sender, System.EventArgs e)
		{
 			((clsControlDayPlan)this.objController).m_GetPlanByDepID();
		}

		private void m_muToDay_Click(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			DateTime DTP=this.m_DTP.Value;
		    clsControlCreatePlan clsPlan=new clsControlCreatePlan();
			long lngRes=clsPlan.m_lngCreatePlan(DTP,DTP);
			if(lngRes>0)
               ((clsControlDayPlan)this.objController).m_GetPlanByDepID();
			this.Cursor=Cursors.Default;
		}
		private void m_muDay_Click(object sender, System.EventArgs e)
		{
			frmCreatePlanByDate frm=new frmCreatePlanByDate();
			if(frm.ShowMe())
               ((clsControlDayPlan)this.objController).m_GetPlanByDepID();
		}

		private void m_lvwPlan_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button==MouseButtons.Right)
			{
				if(m_lvwPlan.Items.Count==0 || m_lvwPlan.SelectedItems.Count==0)
				{
					m_lvwPlan.ContextMenu.MenuItems[1].Enabled=false; //�޸�
					m_lvwPlan.ContextMenu.MenuItems[2].Enabled=false; //ɾ��
				}
				else
				{
					m_lvwPlan.ContextMenu.MenuItems[1].Enabled=true; //�޸�
					m_lvwPlan.ContextMenu.MenuItems[2].Enabled=true; //ɾ��
				}
			}
		}

		private void mu_Add_Click(object sender, System.EventArgs e)
		{
			((clsControlDayPlan)this.objController).m_SetItem(true);
		}

		private void mu_Edit_Click(object sender, System.EventArgs e)
		{
			((clsControlDayPlan)this.objController).m_SetItem(false);
		}

		private void mu_Del_Click(object sender, System.EventArgs e)
		{
			((clsControlDayPlan)this.objController).m_lngDelPlan();
		}

		private void panel2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}
		int count=0;
		private void m_DTP_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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

	}
}
