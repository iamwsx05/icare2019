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
	public class frmWaitDiagListManage2 : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.Panel panel1;
		internal System.Windows.Forms.ListView listView2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		internal PinkieControls.ButtonXP btChangeDoc;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		internal PinkieControls.ButtonXP btPrecedence;
		internal PinkieControls.ButtonXP btExit;
		internal PinkieControls.ButtonXP btRefresh;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		internal System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		internal System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label4;
		internal System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		internal System.Windows.Forms.ComboBox cmbDep;
		private System.Windows.Forms.Label label5;
		internal System.Windows.Forms.TextBox txtDoctor;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Panel panel3;
		internal PinkieControls.ButtonXP btUp;
		internal PinkieControls.ButtonXP btDown;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmWaitDiagListManage2()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
//			Application.Idle+=new EventHandler(OnIdle);
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}
		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsCtl_WaitDiagListManage2();
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
			this.label1 = new System.Windows.Forms.Label();
			this.listView2 = new System.Windows.Forms.ListView();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.btRefresh = new PinkieControls.ButtonXP();
			this.btExit = new PinkieControls.ButtonXP();
			this.btPrecedence = new PinkieControls.ButtonXP();
			this.btChangeDoc = new PinkieControls.ButtonXP();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.txtDoctor = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cmbDep = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btDown = new PinkieControls.ButtonXP();
			this.btUp = new PinkieControls.ButtonXP();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.listView2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 92);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(528, 489);
			this.panel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("宋体", 20F);
			this.label1.Location = new System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(520, 40);
			this.label1.TabIndex = 12;
			this.label1.Text = "候诊列表";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// listView2
			// 
			this.listView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listView2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader4,
																						this.columnHeader8,
																						this.columnHeader3,
																						this.columnHeader5,
																						this.columnHeader10,
																						this.columnHeader9,
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader6,
																						this.columnHeader7});
			this.listView2.ContextMenu = this.contextMenu1;
			this.listView2.FullRowSelect = true;
			this.listView2.GridLines = true;
			this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView2.HideSelection = false;
			this.listView2.Location = new System.Drawing.Point(4, 48);
			this.listView2.Name = "listView2";
			this.listView2.Size = new System.Drawing.Size(520, 437);
			this.listView2.TabIndex = 3;
			this.listView2.View = System.Windows.Forms.View.Details;
			this.listView2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView2_KeyDown);
			this.listView2.DoubleClick += new System.EventHandler(this.listView2_DoubleClick);
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
			this.columnHeader8.Width = 111;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "病人姓名";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader3.Width = 78;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "性别";
			this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader5.Width = 42;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "年龄";
			this.columnHeader10.Width = 44;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "队号";
			this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader9.Width = 46;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "医生ID";
			this.columnHeader1.Width = 0;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "挂号医生";
			this.columnHeader2.Width = 70;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "科室ID";
			this.columnHeader6.Width = 0;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "挂号科室";
			this.columnHeader7.Width = 106;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "优先(插队)";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "转医生";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Location = new System.Drawing.Point(312, 40);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(128, 23);
			this.dateTimePicker2.TabIndex = 11;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(272, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(20, 19);
			this.label3.TabIndex = 10;
			this.label3.Text = "到";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(28, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 19);
			this.label2.TabIndex = 9;
			this.label2.Text = "时 间:   从";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(128, 40);
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
			this.btRefresh.Location = new System.Drawing.Point(496, 36);
			this.btRefresh.Name = "btRefresh";
			this.btRefresh.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btRefresh.Size = new System.Drawing.Size(92, 36);
			this.btRefresh.TabIndex = 7;
			this.btRefresh.Text = "刷新(&R)";
			this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
			// 
			// btExit
			// 
			this.btExit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btExit.DefaultScheme = true;
			this.btExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btExit.Hint = "";
			this.btExit.Location = new System.Drawing.Point(850, 36);
			this.btExit.Name = "btExit";
			this.btExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btExit.Size = new System.Drawing.Size(92, 36);
			this.btExit.TabIndex = 6;
			this.btExit.Text = "退出(ESC)";
			this.btExit.Click += new System.EventHandler(this.btExit_Click);
			// 
			// btPrecedence
			// 
			this.btPrecedence.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btPrecedence.DefaultScheme = true;
			this.btPrecedence.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btPrecedence.Hint = "";
			this.btPrecedence.Location = new System.Drawing.Point(732, 36);
			this.btPrecedence.Name = "btPrecedence";
			this.btPrecedence.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btPrecedence.Size = new System.Drawing.Size(92, 36);
			this.btPrecedence.TabIndex = 5;
			this.btPrecedence.Text = "优先(&I)";
			this.btPrecedence.Click += new System.EventHandler(this.btPrecedence_Click);
			// 
			// btChangeDoc
			// 
			this.btChangeDoc.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btChangeDoc.DefaultScheme = true;
			this.btChangeDoc.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btChangeDoc.Hint = "";
			this.btChangeDoc.Location = new System.Drawing.Point(614, 36);
			this.btChangeDoc.Name = "btChangeDoc";
			this.btChangeDoc.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btChangeDoc.Size = new System.Drawing.Size(92, 36);
			this.btChangeDoc.TabIndex = 4;
			this.btChangeDoc.Text = "转医生(&G)";
			this.btChangeDoc.Click += new System.EventHandler(this.btChangeDoc_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dateTimePicker2);
			this.groupBox1.Controls.Add(this.dateTimePicker1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.btRefresh);
			this.groupBox1.Controls.Add(this.btChangeDoc);
			this.groupBox1.Controls.Add(this.btPrecedence);
			this.groupBox1.Controls.Add(this.btExit);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1000, 92);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel2.Controls.Add(this.txtDoctor);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.cmbDep);
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.listView1);
			this.panel2.Location = new System.Drawing.Point(568, 92);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(432, 489);
			this.panel2.TabIndex = 14;
			// 
			// txtDoctor
			// 
			this.txtDoctor.Location = new System.Drawing.Point(336, 12);
			this.txtDoctor.Name = "txtDoctor";
			this.txtDoctor.Size = new System.Drawing.Size(92, 23);
			this.txtDoctor.TabIndex = 15;
			this.txtDoctor.Text = "";
			this.txtDoctor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDoctor_KeyDown);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(304, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(34, 19);
			this.label6.TabIndex = 16;
			this.label6.Text = "医生";
			// 
			// cmbDep
			// 
			this.cmbDep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbDep.Location = new System.Drawing.Point(156, 12);
			this.cmbDep.Name = "cmbDep";
			this.cmbDep.Size = new System.Drawing.Size(140, 22);
			this.cmbDep.TabIndex = 14;
			this.cmbDep.SelectedIndexChanged += new System.EventHandler(this.cmbDep_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(124, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(34, 19);
			this.label5.TabIndex = 13;
			this.label5.Text = "科室";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("宋体", 20F);
			this.label4.Location = new System.Drawing.Point(0, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(119, 34);
			this.label4.TabIndex = 12;
			this.label4.Text = "结诊列表";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.BackColor = System.Drawing.SystemColors.Info;
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader11,
																						this.columnHeader12,
																						this.columnHeader13,
																						this.columnHeader14,
																						this.columnHeader15,
																						this.columnHeader16,
																						this.columnHeader17,
																						this.columnHeader18,
																						this.columnHeader19,
																						this.columnHeader20});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 48);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(428, 437);
			this.listView1.TabIndex = 3;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "侯诊号";
			this.columnHeader11.Width = 0;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "卡号";
			this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader12.Width = 111;
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "病人姓名";
			this.columnHeader13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader13.Width = 83;
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "性别";
			this.columnHeader14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader14.Width = 41;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "年龄";
			this.columnHeader15.Width = 0;
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "候诊队号";
			this.columnHeader16.Width = 0;
			// 
			// columnHeader17
			// 
			this.columnHeader17.Text = "医生ID";
			this.columnHeader17.Width = 0;
			// 
			// columnHeader18
			// 
			this.columnHeader18.Text = "挂号医生";
			this.columnHeader18.Width = 70;
			// 
			// columnHeader19
			// 
			this.columnHeader19.Text = "科室ID";
			this.columnHeader19.Width = 0;
			// 
			// columnHeader20
			// 
			this.columnHeader20.Text = "挂号科室";
			this.columnHeader20.Width = 97;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.btDown);
			this.panel3.Controls.Add(this.btUp);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.panel3.Location = new System.Drawing.Point(528, 92);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(36, 489);
			this.panel3.TabIndex = 15;
			// 
			// btDown
			// 
			this.btDown.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btDown.DefaultScheme = true;
			this.btDown.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btDown.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.btDown.Hint = "";
			this.btDown.Location = new System.Drawing.Point(6, 392);
			this.btDown.Name = "btDown";
			this.btDown.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btDown.Size = new System.Drawing.Size(24, 60);
			this.btDown.TabIndex = 9;
			this.btDown.Tag = "";
			this.btDown.Text = "↓";
			this.btDown.Click += new System.EventHandler(this.btDown_Click);
			// 
			// btUp
			// 
			this.btUp.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btUp.DefaultScheme = true;
			this.btUp.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btUp.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.btUp.Hint = "";
			this.btUp.Location = new System.Drawing.Point(6, 300);
			this.btUp.Name = "btUp";
			this.btUp.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btUp.Size = new System.Drawing.Size(24, 60);
			this.btUp.TabIndex = 8;
			this.btUp.Tag = "";
			this.btUp.Text = "↑";
			this.btUp.Click += new System.EventHandler(this.btUp_Click);
			// 
			// frmWaitDiagListManage2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.CancelButton = this.btExit;
			this.ClientSize = new System.Drawing.Size(1000, 581);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("宋体", 10.5F);
			this.Name = "frmWaitDiagListManage2";
			this.Text = "候诊列表管理";
			this.Resize += new System.EventHandler(this.frmWaitDiagListManage_Resize);
			this.Load += new System.EventHandler(this.frmWaitDiagListManage_Load);
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

	

		private void frmWaitDiagListManage_Load(object sender, System.EventArgs e)
		{
			((clsCtl_WaitDiagListManage2)this.objController).m_mthFormLoad();
			//	this.listView2.ListViewItemSorter = new ListViewItemSort(5);
		}

	

		private void btExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btRefresh_Click(object sender, System.EventArgs e)
		{
			((clsCtl_WaitDiagListManage2)this.objController).m_mthGetWaitListInfoByID();
			((clsCtl_WaitDiagListManage2)this.objController).m_mthGetEndListInfoByID();
		}

		private void btPrecedence_Click(object sender, System.EventArgs e)
		{
			((clsCtl_WaitDiagListManage2)this.objController).m_mthPrecedence();
		}

		private void btChangeDoc_Click(object sender, System.EventArgs e)
		{
			((clsCtl_WaitDiagListManage2)this.objController).m_mthChangeDocOrDep();
		}
		private void OnIdle(object sender, System.EventArgs e)
		{
			if(this.listView2.SelectedItems.Count>0)
			{
				if(this.listView2.SelectedItems[0].Index ==0)
				{
					this.btUp.Enabled =false;
				}
				else
				{
				this.btUp.Enabled =true;
				}
				if(this.listView2.SelectedItems[0].Index ==this.listView2.Items.Count-1)
				{
					this.btDown.Enabled =false;
				}
				else
				{
					this.btDown.Enabled =true;
				}
			}
			else
			{
			this.btUp.Enabled =false;
			this.btDown.Enabled =false;
			}
		}

		private void frmWaitDiagListManage_Resize(object sender, System.EventArgs e)
		{
//			this.panel1.Left=(this.Width-this.panel1.Width)/2;
//			this.panel1.Top=(this.Height-this.panel1.Height)/2;
		}
		public void m_mthShow()
		{
			((clsCtl_WaitDiagListManage2)this.objController).flag=true;
			this.Show();
		}

		private void cmbDep_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
		this.btPrecedence_Click(null,null);
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
		this.btChangeDoc_Click(null,null);
		}

		private void listView2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode ==Keys.Enter)
			{
			this.listView2_DoubleClick(null,null);
			}
		}

		private void listView2_DoubleClick(object sender, System.EventArgs e)
		{
			this.btChangeDoc_Click(null,null);
		}

		private void txtDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode ==Keys.Enter)
			{
			this.btRefresh_Click(null,null);
			}
		}

		private void btUp_Click(object sender, System.EventArgs e)
		{
			if(this.listView2.SelectedItems.Count==0)
			{
				return;
			}
			if(this.listView2.SelectedItems[0].Index ==0)
			{
				return;
			}
            ListViewItem liSel = listView2.SelectedItems[0]; //选中
            ListViewItem liUp = this.listView2.Items[listView2.SelectedItems[0].Index-1]; ; //选中


			string strPrimary =this.listView2.SelectedItems[0].SubItems[5].Text;
			string strNewID=this.listView2.SelectedItems[0].Text;
			int intTemp =this.listView2.SelectedItems[0].Index-1;
			string strIDPrimary=this.listView2.Items[intTemp].Text;
			string strNew=this.listView2.Items[intTemp].SubItems[5].Text;
			long ret =((clsCtl_WaitDiagListManage2)this.objController).m_mthMoveOrder(strPrimary,strNew,strIDPrimary,strNewID);
			if(ret>0)
			{
                this.listView2.Items[intTemp].SubItems[5].Text = strPrimary;
                this.listView2.Items[intTemp + 1].SubItems[5].Text = strNew;
                liSel.Remove();				

                listView2.Items.Insert(liUp.Index, liSel);
			}
		}

		private void btDown_Click(object sender, System.EventArgs e)
		{
			if(this.listView2.SelectedItems.Count==0)
			{
				return;
			}
			if(this.listView2.SelectedItems[0].Index ==this.listView2.Items.Count-1)
			{
			return;
			}
            ListViewItem liSel = listView2.SelectedItems[0]; //选中
            ListViewItem liDown = this.listView2.Items[listView2.SelectedItems[0].Index + 1]; ; //选中

			string strPrimary =this.listView2.SelectedItems[0].SubItems[5].Text;
			string strNewID=this.listView2.SelectedItems[0].Text;
			int intTemp =this.listView2.SelectedItems[0].Index+1;
			string strIDPrimary=this.listView2.Items[intTemp].Text;
			string strNew=this.listView2.Items[intTemp].SubItems[5].Text;
			long ret =((clsCtl_WaitDiagListManage2)this.objController).m_mthMoveOrder(strPrimary,strNew,strIDPrimary,strNewID);
			if(ret>0)
			{
				this.listView2.Items[intTemp].SubItems[5].Text =strPrimary;
				this.listView2.Items[intTemp-1].SubItems[5].Text =strNew;
                liDown.Remove();

                listView2.Items.Insert(liSel.Index, liDown);
			}

		}
		private class ListViewItemSort : IComparer 
		{
			private int col;
			public ListViewItemSort() 
			{
				col=0;
			}
			public ListViewItemSort(int column) 
			{
				col=column;
			}
			public int Compare(object x, object y) 
			{
                return  Convert.ToInt32(x.ToString().Trim()) > Convert.ToInt32(y.ToString().Trim()) ? 1 : -1;
				//return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
			}
		}
	}
}
