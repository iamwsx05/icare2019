using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace   com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmReturnTicket 的摘要说明。
	/// </summary>
	public class frmReturnTicket :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TabControl tab;
		private System.Windows.Forms.TabPage tabPageNot;
		private System.Windows.Forms.TabPage tabPageOk;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		internal System.Windows.Forms.ListView m_lsvPatientDetial;
		internal System.Windows.Forms.ListView m_lsvMedicineDetail;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		internal System.Windows.Forms.ListView m_lsvOpRecDetail;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		internal PinkieControls.ButtonXP m_bntcolse;
		internal System.Windows.Forms.ListView lvsreture;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		internal System.Windows.Forms.ComboBox cbxMedStoreWin;
		internal System.Windows.Forms.TextBox SEQID;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		internal PinkieControls.ButtonXP bntFind;
		internal PinkieControls.ButtonXP bntok;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.Windows.Forms.ColumnHeader col12;
		internal System.Windows.Forms.ComboBox cobMedStore;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		internal System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		internal System.Windows.Forms.TextBox OUTPATRECIPEID;
		private System.ComponentModel.IContainer components;

		public frmReturnTicket()
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
			this.cobMedStore = new System.Windows.Forms.ComboBox();
			this.cbxMedStoreWin = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tab = new System.Windows.Forms.TabControl();
			this.tabPageNot = new System.Windows.Forms.TabPage();
			this.m_lsvPatientDetial = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.col12 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
			this.tabPageOk = new System.Windows.Forms.TabPage();
			this.lvsreture = new System.Windows.Forms.ListView();
			this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
			this.m_lsvMedicineDetail = new System.Windows.Forms.ListView();
			this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.m_bntcolse = new PinkieControls.ButtonXP();
			this.bntok = new PinkieControls.ButtonXP();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.bntFind = new PinkieControls.ButtonXP();
			this.label9 = new System.Windows.Forms.Label();
			this.SEQID = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.m_lsvOpRecDetail = new System.Windows.Forms.ListView();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
			this.panel1 = new System.Windows.Forms.Panel();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.panel2 = new System.Windows.Forms.Panel();
			this.OUTPATRECIPEID = new System.Windows.Forms.TextBox();
			this.tab.SuspendLayout();
			this.tabPageNot.SuspendLayout();
			this.tabPageOk.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// cobMedStore
			// 
			this.cobMedStore.BackColor = System.Drawing.SystemColors.Info;
			this.cobMedStore.Location = new System.Drawing.Point(48, 14);
			this.cobMedStore.Name = "cobMedStore";
			this.cobMedStore.Size = new System.Drawing.Size(120, 22);
			this.cobMedStore.TabIndex = 9;
			this.cobMedStore.SelectedIndexChanged += new System.EventHandler(this.cobMedStore_SelectedIndexChanged);
			// 
			// cbxMedStoreWin
			// 
			this.cbxMedStoreWin.BackColor = System.Drawing.SystemColors.Info;
			this.cbxMedStoreWin.Location = new System.Drawing.Point(240, 14);
			this.cbxMedStoreWin.Name = "cbxMedStoreWin";
			this.cbxMedStoreWin.Size = new System.Drawing.Size(104, 22);
			this.cbxMedStoreWin.TabIndex = 8;
			this.cbxMedStoreWin.SelectedValueChanged += new System.EventHandler(this.cbxMedStoreWin_SelectedValueChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label4.Location = new System.Drawing.Point(200, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(34, 19);
			this.label4.TabIndex = 6;
			this.label4.Text = "窗口";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "药房";
			// 
			// tab
			// 
			this.tab.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tab.Controls.Add(this.tabPageNot);
			this.tab.Controls.Add(this.tabPageOk);
			this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tab.Location = new System.Drawing.Point(0, 0);
			this.tab.Name = "tab";
			this.tab.SelectedIndex = 0;
			this.tab.Size = new System.Drawing.Size(350, 566);
			this.tab.TabIndex = 9;
			this.tab.SelectedIndexChanged += new System.EventHandler(this.tab_SelectedIndexChanged);
			// 
			// tabPageNot
			// 
			this.tabPageNot.Controls.Add(this.m_lsvPatientDetial);
			this.tabPageNot.Location = new System.Drawing.Point(4, 4);
			this.tabPageNot.Name = "tabPageNot";
			this.tabPageNot.Size = new System.Drawing.Size(342, 539);
			this.tabPageNot.TabIndex = 0;
			this.tabPageNot.Text = "己发药";
			// 
			// m_lsvPatientDetial
			// 
			this.m_lsvPatientDetial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.m_lsvPatientDetial.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								 this.columnHeader1,
																								 this.col12,
																								 this.columnHeader19,
																								 this.columnHeader2,
																								 this.columnHeader3,
																								 this.columnHeader22});
			this.m_lsvPatientDetial.FullRowSelect = true;
			this.m_lsvPatientDetial.GridLines = true;
			this.m_lsvPatientDetial.HideSelection = false;
			this.m_lsvPatientDetial.Location = new System.Drawing.Point(0, 0);
			this.m_lsvPatientDetial.MultiSelect = false;
			this.m_lsvPatientDetial.Name = "m_lsvPatientDetial";
			this.m_lsvPatientDetial.Size = new System.Drawing.Size(344, 542);
			this.m_lsvPatientDetial.TabIndex = 3;
			this.m_lsvPatientDetial.View = System.Windows.Forms.View.Details;
			this.m_lsvPatientDetial.Click += new System.EventHandler(this.m_lsvPatientDetial_Click);
			this.m_lsvPatientDetial.SelectedIndexChanged += new System.EventHandler(this.m_lsvPatientDetial_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "发票号";
			this.columnHeader1.Width = 100;
			// 
			// col12
			// 
			this.col12.Text = "收费员";
			this.col12.Width = 80;
			// 
			// columnHeader19
			// 
			this.columnHeader19.Text = "流水号";
			this.columnHeader19.Width = 110;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "门诊处方记录号";
			this.columnHeader2.Width = 140;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "发票日期";
			this.columnHeader3.Width = 120;
			// 
			// columnHeader22
			// 
			this.columnHeader22.Text = "卡号";
			this.columnHeader22.Width = 0;
			// 
			// tabPageOk
			// 
			this.tabPageOk.Controls.Add(this.lvsreture);
			this.tabPageOk.Location = new System.Drawing.Point(4, 4);
			this.tabPageOk.Name = "tabPageOk";
			this.tabPageOk.Size = new System.Drawing.Size(342, 539);
			this.tabPageOk.TabIndex = 1;
			this.tabPageOk.Text = "己退药";
			this.tabPageOk.Visible = false;
			// 
			// lvsreture
			// 
			this.lvsreture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.lvsreture.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader14,
																						this.columnHeader20,
																						this.columnHeader21,
																						this.columnHeader15,
																						this.columnHeader16});
			this.lvsreture.FullRowSelect = true;
			this.lvsreture.GridLines = true;
			this.lvsreture.Location = new System.Drawing.Point(0, 0);
			this.lvsreture.Name = "lvsreture";
			this.lvsreture.Size = new System.Drawing.Size(344, 540);
			this.lvsreture.TabIndex = 4;
			this.lvsreture.View = System.Windows.Forms.View.Details;
			this.lvsreture.Click += new System.EventHandler(this.lvsreture_Click);
			this.lvsreture.SelectedIndexChanged += new System.EventHandler(this.lvsreture_SelectedIndexChanged);
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "发票号";
			this.columnHeader14.Width = 100;
			// 
			// columnHeader20
			// 
			this.columnHeader20.Text = "收费员";
			this.columnHeader20.Width = 80;
			// 
			// columnHeader21
			// 
			this.columnHeader21.Text = "流水号";
			this.columnHeader21.Width = 110;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "门诊处方记录号";
			this.columnHeader15.Width = 140;
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "发票日期";
			this.columnHeader16.Width = 120;
			// 
			// m_lsvMedicineDetail
			// 
			this.m_lsvMedicineDetail.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
			this.m_lsvMedicineDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lsvMedicineDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_lsvMedicineDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								  this.columnHeader17,
																								  this.columnHeader4,
																								  this.columnHeader5,
																								  this.columnHeader6,
																								  this.columnHeader7,
																								  this.columnHeader8,
																								  this.columnHeader9});
			this.m_lsvMedicineDetail.FullRowSelect = true;
			this.m_lsvMedicineDetail.GridLines = true;
			this.m_lsvMedicineDetail.Location = new System.Drawing.Point(360, 136);
			this.m_lsvMedicineDetail.Name = "m_lsvMedicineDetail";
			this.m_lsvMedicineDetail.Size = new System.Drawing.Size(640, 424);
			this.m_lsvMedicineDetail.TabIndex = 10;
			this.m_lsvMedicineDetail.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader17
			// 
			this.columnHeader17.Text = "行号";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "项目编码";
			this.columnHeader4.Width = 80;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "项目名称";
			this.columnHeader5.Width = 200;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "单位";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "数量";
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "单价";
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "金额";
			this.columnHeader9.Width = 80;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.m_bntcolse);
			this.groupBox2.Controls.Add(this.bntok);
			this.groupBox2.Location = new System.Drawing.Point(360, 560);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(640, 64);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			// 
			// m_bntcolse
			// 
			this.m_bntcolse.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_bntcolse.DefaultScheme = true;
			this.m_bntcolse.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_bntcolse.Hint = "";
			this.m_bntcolse.Location = new System.Drawing.Point(440, 16);
			this.m_bntcolse.Name = "m_bntcolse";
			this.m_bntcolse.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_bntcolse.Size = new System.Drawing.Size(104, 40);
			this.m_bntcolse.TabIndex = 14;
			this.m_bntcolse.Text = "退出系统(&E)";
			this.m_bntcolse.Click += new System.EventHandler(this.m_bntcolse_Click);
			// 
			// bntok
			// 
			this.bntok.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.bntok.DefaultScheme = true;
			this.bntok.DialogResult = System.Windows.Forms.DialogResult.None;
			this.bntok.Hint = "";
			this.bntok.Location = new System.Drawing.Point(192, 16);
			this.bntok.Name = "bntok";
			this.bntok.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.bntok.Size = new System.Drawing.Size(104, 40);
			this.bntok.TabIndex = 13;
			this.bntok.Text = "同意退药(&R)";
			this.bntok.Click += new System.EventHandler(this.bntok_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.OUTPATRECIPEID);
			this.groupBox3.Controls.Add(this.comboBox1);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.bntFind);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.SEQID);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Location = new System.Drawing.Point(360, -6);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(640, 52);
			this.groupBox3.TabIndex = 12;
			this.groupBox3.TabStop = false;
			// 
			// comboBox1
			// 
			this.comboBox1.BackColor = System.Drawing.SystemColors.Window;
			this.comboBox1.Items.AddRange(new object[] {
														   "诊疗卡号",
														   "发票号",
														   "流水号"});
			this.comboBox1.Location = new System.Drawing.Point(142, 16);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(104, 22);
			this.comboBox1.TabIndex = 10;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.Location = new System.Drawing.Point(16, 16);
			this.label2.Name = "label2";
			this.label2.TabIndex = 16;
			this.label2.Text = "查找方式：";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// bntFind
			// 
			this.bntFind.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.bntFind.DefaultScheme = true;
			this.bntFind.DialogResult = System.Windows.Forms.DialogResult.None;
			this.bntFind.Hint = "";
			this.bntFind.Location = new System.Drawing.Point(480, 11);
			this.bntFind.Name = "bntFind";
			this.bntFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.bntFind.Size = new System.Drawing.Size(96, 32);
			this.bntFind.TabIndex = 15;
			this.bntFind.Text = "查找数据(&F)";
			this.bntFind.Click += new System.EventHandler(this.bntFind_Click);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(208, 56);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(88, 23);
			this.label9.TabIndex = 9;
			this.label9.Text = "收费员";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SEQID
			// 
			this.SEQID.Location = new System.Drawing.Point(72, 56);
			this.SEQID.Name = "SEQID";
			this.SEQID.TabIndex = 8;
			this.SEQID.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 56);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 23);
			this.label8.TabIndex = 7;
			this.label8.Text = "流水号";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// m_lsvOpRecDetail
			// 
			this.m_lsvOpRecDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lsvOpRecDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_lsvOpRecDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeader10,
																							   this.columnHeader11,
																							   this.columnHeader12,
																							   this.columnHeader13,
																							   this.columnHeader18});
			this.m_lsvOpRecDetail.FullRowSelect = true;
			this.m_lsvOpRecDetail.GridLines = true;
			this.m_lsvOpRecDetail.Location = new System.Drawing.Point(360, 56);
			this.m_lsvOpRecDetail.Name = "m_lsvOpRecDetail";
			this.m_lsvOpRecDetail.Size = new System.Drawing.Size(640, 72);
			this.m_lsvOpRecDetail.TabIndex = 13;
			this.m_lsvOpRecDetail.View = System.Windows.Forms.View.Details;
			this.m_lsvOpRecDetail.DoubleClick += new System.EventHandler(this.m_lsvOpRecDetail_DoubleClick);
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "处方号";
			this.columnHeader10.Width = 140;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "处方医生";
			this.columnHeader11.Width = 80;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "科室";
			this.columnHeader12.Width = 100;
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "处方录入者";
			this.columnHeader13.Width = 100;
			// 
			// columnHeader18
			// 
			this.columnHeader18.Text = "取药人";
			this.columnHeader18.Width = 90;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.dateTimePicker1);
			this.panel1.Controls.Add(this.tab);
			this.panel1.Location = new System.Drawing.Point(0, 56);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(352, 568);
			this.panel1.TabIndex = 14;
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.dateTimePicker1.Location = new System.Drawing.Point(216, 544);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(136, 23);
			this.dateTimePicker1.TabIndex = 16;
			this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.cbxMedStoreWin);
			this.panel2.Controls.Add(this.cobMedStore);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(352, 48);
			this.panel2.TabIndex = 15;
			// 
			// OUTPATRECIPEID
			// 
			this.OUTPATRECIPEID.Location = new System.Drawing.Point(272, 16);
			this.OUTPATRECIPEID.Name = "OUTPATRECIPEID";
			this.OUTPATRECIPEID.Size = new System.Drawing.Size(144, 23);
			this.OUTPATRECIPEID.TabIndex = 17;
			this.OUTPATRECIPEID.Text = "";
			this.OUTPATRECIPEID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OUTPATRECIPEID_KeyDown);
			// 
			// frmReturnTicket
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(1000, 629);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.m_lsvOpRecDetail);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.m_lsvMedicineDetail);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.KeyPreview = true;
			this.Name = "frmReturnTicket";
			this.Text = "退药管理";
			this.Load += new System.EventHandler(this.frmReturnTicket_Load);
			this.tab.ResumeLayout(false);
			this.tabPageNot.ResumeLayout(false);
			this.tabPageOk.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsControlReturnTicket();
			objController.Set_GUI_Apperance(this);
		}

		private void frmReturnTicket_Load(object sender, System.EventArgs e)
		{
			((clsControlReturnTicket)this.objController).m_lngLoadFrm();
		}

		private void m_btnExit_Click(object sender, System.EventArgs e)
		{
		
		}

		private void m_bntcolse_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


		private void m_lsvOpRecDetail_DoubleClick(object sender, System.EventArgs e)
		{
			((clsControlReturnTicket)this.objController).m_mthSelRecipe();
		}

		private void bntok_Click(object sender, System.EventArgs e)
		{
			((clsControlReturnTicket)this.objController).m_lngRetureOperator();
		}

		private void bntFind_Click(object sender, System.EventArgs e)
		{
			((clsControlReturnTicket)this.objController).m_ingFid();
		}

		private void lvsreture_DoubleClick(object sender, System.EventArgs e)
		{
		((clsControlReturnTicket)this.objController).m_mthSelPatient(2);
		}

		private void tab_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlReturnTicket)this.objController).m_lngWindows();
		}

		private void cbxMedStoreWin_SelectedValueChanged(object sender, System.EventArgs e)
		{
			((clsControlReturnTicket)this.objController).m_lngCobChang();
		}

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void cobMedStore_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlReturnTicket)this.objController).m_lngShowWin();
		}

		private void m_lsvPatientDetial_DoubleClick(object sender, System.EventArgs e)
		{
		
		}

		private void m_lsvPatientDetial_Click(object sender, System.EventArgs e)
		{
			((clsControlReturnTicket)this.objController).m_mthSelPatient(1);
		}

		private void m_lsvPatientDetial_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlReturnTicket)this.objController).m_mthSelPatient(1);
		}

		private void lvsreture_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			((clsControlReturnTicket)this.objController).m_mthSelPatient(2);
		}

		private void lvsreture_Click(object sender, System.EventArgs e)
		{
			((clsControlReturnTicket)this.objController).m_mthSelPatient(2);
		}

		private void dateTimePicker1_ValueChanged(object sender, System.EventArgs e)
		{
			((clsControlReturnTicket)this.objController).m_lngCobChang();
		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			OUTPATRECIPEID.Focus();
		}

		private void OUTPATRECIPEID_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter&&this.comboBox1.SelectedIndex==0)
			{
				if(this.OUTPATRECIPEID.Text.Length<10 && this.OUTPATRECIPEID.Text.Length>0)
				{
					string strCardID = "";
					strCardID = "0000000000"+this.OUTPATRECIPEID.Text;
					this.OUTPATRECIPEID.Text = strCardID.Substring(strCardID.Length-10);
					((clsControlReturnTicket)this.objController).m_ingFid();
				}
			}
		}
	}
}
