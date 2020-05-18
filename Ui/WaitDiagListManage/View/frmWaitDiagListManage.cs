using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class frmWaitDiagListManage : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		internal System.Windows.Forms.ListView listView2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		internal PinkieControls.ButtonXP btChangeDoc;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		internal System.Windows.Forms.ComboBox cmbDep;
		internal PinkieControls.ButtonXP btPrecedence;
		internal PinkieControls.ButtonXP btExit;
		internal PinkieControls.ButtonXP btRefresh;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		internal System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		internal System.Windows.Forms.DateTimePicker dateTimePicker2;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmWaitDiagListManage()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			Application.Idle+=new EventHandler(OnIdle);
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}
		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsCtl_WaitDiagListManage();
			objController.Set_GUI_Apperance(this);
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.btRefresh = new PinkieControls.ButtonXP();
			this.btExit = new PinkieControls.ButtonXP();
			this.btPrecedence = new PinkieControls.ButtonXP();
			this.btChangeDoc = new PinkieControls.ButtonXP();
			this.listView2 = new System.Windows.Forms.ListView();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.cmbDep = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.dateTimePicker2);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.dateTimePicker1);
			this.panel1.Controls.Add(this.btRefresh);
			this.panel1.Controls.Add(this.btExit);
			this.panel1.Controls.Add(this.btPrecedence);
			this.panel1.Controls.Add(this.btChangeDoc);
			this.panel1.Controls.Add(this.listView2);
			this.panel1.Controls.Add(this.listView1);
			this.panel1.Controls.Add(this.cmbDep);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(8, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(864, 572);
			this.panel1.TabIndex = 0;
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Location = new System.Drawing.Point(608, 48);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(128, 23);
			this.dateTimePicker2.TabIndex = 11;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(572, 52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(20, 19);
			this.label3.TabIndex = 10;
			this.label3.Text = "到";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(328, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 19);
			this.label2.TabIndex = 9;
			this.label2.Text = "时 间:   从";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(424, 47);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(128, 23);
			this.dateTimePicker1.TabIndex = 8;
			// 
			// btRefresh
			// 
			this.btRefresh.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btRefresh.DefaultScheme = true;
			this.btRefresh.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btRefresh.Hint = "";
			this.btRefresh.Location = new System.Drawing.Point(768, 320);
			this.btRefresh.Name = "btRefresh";
			this.btRefresh.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btRefresh.Size = new System.Drawing.Size(92, 36);
			this.btRefresh.TabIndex = 7;
			this.btRefresh.Text = "刷新";
			this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
			// 
			// btExit
			// 
			this.btExit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btExit.DefaultScheme = true;
			this.btExit.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btExit.Hint = "";
			this.btExit.Location = new System.Drawing.Point(768, 503);
			this.btExit.Name = "btExit";
			this.btExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btExit.Size = new System.Drawing.Size(92, 36);
			this.btExit.TabIndex = 6;
			this.btExit.Text = "退出";
			this.btExit.Click += new System.EventHandler(this.btExit_Click);
			// 
			// btPrecedence
			// 
			this.btPrecedence.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btPrecedence.DefaultScheme = true;
			this.btPrecedence.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btPrecedence.Hint = "";
			this.btPrecedence.Location = new System.Drawing.Point(768, 442);
			this.btPrecedence.Name = "btPrecedence";
			this.btPrecedence.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btPrecedence.Size = new System.Drawing.Size(92, 36);
			this.btPrecedence.TabIndex = 5;
			this.btPrecedence.Text = "优先";
			this.btPrecedence.Click += new System.EventHandler(this.btPrecedence_Click);
			// 
			// btChangeDoc
			// 
			this.btChangeDoc.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btChangeDoc.DefaultScheme = true;
			this.btChangeDoc.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btChangeDoc.Hint = "";
			this.btChangeDoc.Location = new System.Drawing.Point(768, 381);
			this.btChangeDoc.Name = "btChangeDoc";
			this.btChangeDoc.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btChangeDoc.Size = new System.Drawing.Size(92, 36);
			this.btChangeDoc.TabIndex = 4;
			this.btChangeDoc.Text = "转医生";
			this.btChangeDoc.Click += new System.EventHandler(this.btChangeDoc_Click);
			// 
			// listView2
			// 
			this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader4,
																						this.columnHeader8,
																						this.columnHeader3,
																						this.columnHeader5,
																						this.columnHeader10,
																						this.columnHeader9});
			this.listView2.FullRowSelect = true;
			this.listView2.GridLines = true;
			this.listView2.HideSelection = false;
			this.listView2.Location = new System.Drawing.Point(328, 80);
			this.listView2.Name = "listView2";
			this.listView2.Size = new System.Drawing.Size(412, 468);
			this.listView2.TabIndex = 3;
			this.listView2.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "侯诊号";
			this.columnHeader4.Width = 0;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "卡号";
			this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader8.Width = 110;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "病人姓名";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader3.Width = 120;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "性别";
			this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader5.Width = 43;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "年龄";
			this.columnHeader10.Width = 44;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "候诊队号";
			this.columnHeader9.Width = 74;
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader6,
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader7});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(36, 80);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(276, 468);
			this.listView1.TabIndex = 2;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "工号";
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "医生姓名";
			this.columnHeader1.Width = 139;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "职称";
			this.columnHeader2.Width = 57;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "ID";
			this.columnHeader7.Width = 0;
			// 
			// cmbDep
			// 
			this.cmbDep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbDep.Location = new System.Drawing.Point(92, 48);
			this.cmbDep.Name = "cmbDep";
			this.cmbDep.Size = new System.Drawing.Size(220, 22);
			this.cmbDep.TabIndex = 1;
			this.cmbDep.SelectedIndexChanged += new System.EventHandler(this.cmbDep_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(40, 52);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "科 室:";
			// 
			// frmWaitDiagListManage
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(892, 581);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("宋体", 10.5F);
			this.Name = "frmWaitDiagListManage";
			this.Text = "候诊列表管理";
			this.Resize += new System.EventHandler(this.frmWaitDiagListManage_Resize);
			this.Load += new System.EventHandler(this.frmWaitDiagListManage_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmWaitDiagListManage2());
		}

		private void frmWaitDiagListManage_Load(object sender, System.EventArgs e)
		{
			((clsCtl_WaitDiagListManage)this.objController).m_mthFormLoad();
		}

		private void cmbDep_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		((clsCtl_WaitDiagListManage)this.objController).m_mthGetDocByDepID();
		}

		private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.listView1.SelectedItems.Count>0)
			{
			this.listView1.Tag=this.listView1.SelectedItems[0].SubItems[3].Text.Trim();
			((clsCtl_WaitDiagListManage)this.objController).m_mthGetWaitListByID();
			}
			
		}

		private void btExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btRefresh_Click(object sender, System.EventArgs e)
		{
			((clsCtl_WaitDiagListManage)this.objController).m_mthGetWaitListByID();
		}

		private void btPrecedence_Click(object sender, System.EventArgs e)
		{
			((clsCtl_WaitDiagListManage)this.objController).m_mthPrecedence();
		}

		private void btChangeDoc_Click(object sender, System.EventArgs e)
		{
			((clsCtl_WaitDiagListManage)this.objController).m_mthChangeDoc();
		}
		private void OnIdle(object sender, System.EventArgs e)
		{
			if(this.listView1.Tag==null)
			{
				this.btRefresh.Enabled=false;
			}
			else
			{
			this.btRefresh.Enabled=true;
			}
			if(this.listView2.SelectedItems.Count>0)
			{
				this.btPrecedence.Enabled=true;
				this.btChangeDoc.Enabled=true;
			}
			else
			{
				this.btPrecedence.Enabled=false;
				this.btChangeDoc.Enabled=false;
			}
		}

		private void frmWaitDiagListManage_Resize(object sender, System.EventArgs e)
		{
			this.panel1.Left=(this.Width-this.panel1.Width)/2;
			this.panel1.Top=(this.Height-this.panel1.Height)/2;
		}
		public void m_mthShow()
		{
		((clsCtl_WaitDiagListManage)this.objController).flag=true;
		this.Show();
		}
	}
}
