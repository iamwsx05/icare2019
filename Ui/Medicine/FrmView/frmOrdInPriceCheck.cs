using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
 
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmOrdInPriceCheck 的摘要说明。
	/// </summary>
	public class frmOrdInPriceCheck :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListView listView1;
		internal com.digitalwave.iCare.gui.HIS.exComboBox m_cboOPCal;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ListView listView2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.Panel panel3;
		internal com.digitalwave.iCare.gui.HIS.exComboBox exComboBox1;
		private PinkieControls.ButtonXP buttonXP1;
		private PinkieControls.ButtonXP buttonXP2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private NullableDateControls.MaskDateEdit maskDateEdit1;
		private NullableDateControls.MaskDateEdit maskDateEdit2;
		private System.Windows.Forms.Panel panel4;
		internal com.digitalwave.iCare.gui.HIS.exComboBox exComboBox2;
		internal com.digitalwave.iCare.gui.HIS.exComboBox exComboBox3;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel6;
		internal com.digitalwave.iCare.gui.HIS.exComboBox exComboBox4;
		internal com.digitalwave.iCare.gui.HIS.exComboBox exComboBox5;
		internal com.digitalwave.iCare.gui.HIS.exComboBox m_exCobType;
		internal System.Windows.Forms.TextBox m_txtFind;
		private PinkieControls.ButtonXP buttonXP3;
		private PinkieControls.ButtonXP buttonXP4;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmOrdInPriceCheck()
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.exComboBox2 = new com.digitalwave.iCare.gui.HIS.exComboBox();
			this.exComboBox3 = new com.digitalwave.iCare.gui.HIS.exComboBox();
			this.exComboBox1 = new com.digitalwave.iCare.gui.HIS.exComboBox();
			this.m_cboOPCal = new com.digitalwave.iCare.gui.HIS.exComboBox();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.panel5 = new System.Windows.Forms.Panel();
			this.buttonXP4 = new PinkieControls.ButtonXP();
			this.buttonXP3 = new PinkieControls.ButtonXP();
			this.m_txtFind = new System.Windows.Forms.TextBox();
			this.panel6 = new System.Windows.Forms.Panel();
			this.exComboBox4 = new com.digitalwave.iCare.gui.HIS.exComboBox();
			this.exComboBox5 = new com.digitalwave.iCare.gui.HIS.exComboBox();
			this.m_exCobType = new com.digitalwave.iCare.gui.HIS.exComboBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.listView2 = new System.Windows.Forms.ListView();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.maskDateEdit2 = new NullableDateControls.MaskDateEdit();
			this.maskDateEdit1 = new NullableDateControls.MaskDateEdit();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonXP1 = new PinkieControls.ButtonXP();
			this.buttonXP2 = new PinkieControls.ButtonXP();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel6.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.listView1);
			this.panel1.Controls.Add(this.panel5);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(352, 445);
			this.panel1.TabIndex = 0;
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.SystemColors.Control;
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.panel4);
			this.panel3.Controls.Add(this.exComboBox1);
			this.panel3.Controls.Add(this.m_cboOPCal);
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(348, 40);
			this.panel3.TabIndex = 22;
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.SystemColors.Control;
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel4.Controls.Add(this.exComboBox2);
			this.panel4.Controls.Add(this.exComboBox3);
			this.panel4.Location = new System.Drawing.Point(-1, 800);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(300, 40);
			this.panel4.TabIndex = 23;
			// 
			// exComboBox2
			// 
			this.exComboBox2.BackColor = System.Drawing.Color.OldLace;
			this.exComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.exComboBox2.Location = new System.Drawing.Point(152, 8);
			this.exComboBox2.Name = "exComboBox2";
			this.exComboBox2.Size = new System.Drawing.Size(112, 22);
			this.exComboBox2.TabIndex = 22;
			// 
			// exComboBox3
			// 
			this.exComboBox3.BackColor = System.Drawing.Color.OldLace;
			this.exComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.exComboBox3.Location = new System.Drawing.Point(8, 8);
			this.exComboBox3.Name = "exComboBox3";
			this.exComboBox3.Size = new System.Drawing.Size(112, 22);
			this.exComboBox3.TabIndex = 21;
			// 
			// exComboBox1
			// 
			this.exComboBox1.BackColor = System.Drawing.Color.OldLace;
			this.exComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.exComboBox1.Location = new System.Drawing.Point(168, 8);
			this.exComboBox1.Name = "exComboBox1";
			this.exComboBox1.Size = new System.Drawing.Size(168, 22);
			this.exComboBox1.TabIndex = 22;
			this.exComboBox1.SelectedIndexChanged += new System.EventHandler(this.exComboBox1_SelectedIndexChanged);
			// 
			// m_cboOPCal
			// 
			this.m_cboOPCal.BackColor = System.Drawing.Color.OldLace;
			this.m_cboOPCal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cboOPCal.Location = new System.Drawing.Point(8, 8);
			this.m_cboOPCal.Name = "m_cboOPCal";
			this.m_cboOPCal.Size = new System.Drawing.Size(128, 22);
			this.m_cboOPCal.TabIndex = 21;
			this.m_cboOPCal.SelectedIndexChanged += new System.EventHandler(this.m_cboOPCal_SelectedIndexChanged);
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader11,
																						this.columnHeader12});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(0, 40);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(360, 360);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "助记码";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "药品名称";
			this.columnHeader2.Width = 160;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "拼音码";
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "五笔码";
			// 
			// panel5
			// 
			this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel5.BackColor = System.Drawing.SystemColors.Control;
			this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel5.Controls.Add(this.buttonXP4);
			this.panel5.Controls.Add(this.buttonXP3);
			this.panel5.Controls.Add(this.m_txtFind);
			this.panel5.Controls.Add(this.panel6);
			this.panel5.Controls.Add(this.m_exCobType);
			this.panel5.Location = new System.Drawing.Point(0, 405);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(348, 40);
			this.panel5.TabIndex = 23;
			// 
			// buttonXP4
			// 
			this.buttonXP4.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.buttonXP4.DefaultScheme = true;
			this.buttonXP4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXP4.Hint = "";
			this.buttonXP4.Location = new System.Drawing.Point(280, 3);
			this.buttonXP4.Name = "buttonXP4";
			this.buttonXP4.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.buttonXP4.Size = new System.Drawing.Size(59, 32);
			this.buttonXP4.TabIndex = 326;
			this.buttonXP4.TabStop = false;
			this.buttonXP4.Text = "返回(&R)";
			this.buttonXP4.Click += new System.EventHandler(this.buttonXP4_Click);
			// 
			// buttonXP3
			// 
			this.buttonXP3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.buttonXP3.DefaultScheme = true;
			this.buttonXP3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXP3.Hint = "";
			this.buttonXP3.Location = new System.Drawing.Point(216, 3);
			this.buttonXP3.Name = "buttonXP3";
			this.buttonXP3.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.buttonXP3.Size = new System.Drawing.Size(59, 32);
			this.buttonXP3.TabIndex = 325;
			this.buttonXP3.TabStop = false;
			this.buttonXP3.Text = "查找(&F)";
			this.buttonXP3.Click += new System.EventHandler(this.buttonXP3_Click);
			// 
			// m_txtFind
			// 
			this.m_txtFind.BackColor = System.Drawing.Color.OldLace;
			this.m_txtFind.Location = new System.Drawing.Point(104, 8);
			this.m_txtFind.Name = "m_txtFind";
			this.m_txtFind.TabIndex = 24;
			this.m_txtFind.Text = "";
			this.m_txtFind.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// panel6
			// 
			this.panel6.BackColor = System.Drawing.SystemColors.Control;
			this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel6.Controls.Add(this.exComboBox4);
			this.panel6.Controls.Add(this.exComboBox5);
			this.panel6.Location = new System.Drawing.Point(-1, 800);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(300, 40);
			this.panel6.TabIndex = 23;
			// 
			// exComboBox4
			// 
			this.exComboBox4.BackColor = System.Drawing.Color.OldLace;
			this.exComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.exComboBox4.Location = new System.Drawing.Point(152, 8);
			this.exComboBox4.Name = "exComboBox4";
			this.exComboBox4.Size = new System.Drawing.Size(112, 22);
			this.exComboBox4.TabIndex = 22;
			// 
			// exComboBox5
			// 
			this.exComboBox5.BackColor = System.Drawing.Color.OldLace;
			this.exComboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.exComboBox5.Location = new System.Drawing.Point(8, 8);
			this.exComboBox5.Name = "exComboBox5";
			this.exComboBox5.Size = new System.Drawing.Size(112, 22);
			this.exComboBox5.TabIndex = 21;
			// 
			// m_exCobType
			// 
			this.m_exCobType.BackColor = System.Drawing.Color.OldLace;
			this.m_exCobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_exCobType.Items.AddRange(new object[] {
															 "药品代码",
															 "药品名称",
															 "拼音码",
															 "五笔码"});
			this.m_exCobType.Location = new System.Drawing.Point(8, 8);
			this.m_exCobType.Name = "m_exCobType";
			this.m_exCobType.Size = new System.Drawing.Size(88, 22);
			this.m_exCobType.TabIndex = 21;
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel2.Controls.Add(this.listView2);
			this.panel2.Location = new System.Drawing.Point(360, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(600, 400);
			this.panel2.TabIndex = 1;
			// 
			// listView2
			// 
			this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5,
																						this.columnHeader6,
																						this.columnHeader7,
																						this.columnHeader8,
																						this.columnHeader9,
																						this.columnHeader10});
			this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView2.FullRowSelect = true;
			this.listView2.GridLines = true;
			this.listView2.Location = new System.Drawing.Point(0, 0);
			this.listView2.Name = "listView2";
			this.listView2.Size = new System.Drawing.Size(596, 396);
			this.listView2.TabIndex = 0;
			this.listView2.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "入库单据号";
			this.columnHeader3.Width = 120;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "供应商名称";
			this.columnHeader4.Width = 130;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "系统批次";
			this.columnHeader5.Width = 80;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "药品批号";
			this.columnHeader6.Width = 80;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "数量";
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "单位";
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "单价";
			this.columnHeader9.Width = 80;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "总价";
			this.columnHeader10.Width = 100;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.maskDateEdit2);
			this.groupBox1.Controls.Add(this.maskDateEdit1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.buttonXP1);
			this.groupBox1.Controls.Add(this.buttonXP2);
			this.groupBox1.Location = new System.Drawing.Point(360, 398);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(600, 48);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// maskDateEdit2
			// 
			this.maskDateEdit2.BackColor = System.Drawing.SystemColors.Window;
			this.maskDateEdit2.Location = new System.Drawing.Point(280, 16);
			this.maskDateEdit2.Mask = "yyyy年MM月dd日";
			this.maskDateEdit2.Name = "maskDateEdit2";
			this.maskDateEdit2.Size = new System.Drawing.Size(120, 23);
			this.maskDateEdit2.TabIndex = 330;
			this.maskDateEdit2.Text = "";
			this.maskDateEdit2.TextChanged += new System.EventHandler(this.maskDateEdit1_TextChanged);
			// 
			// maskDateEdit1
			// 
			this.maskDateEdit1.BackColor = System.Drawing.SystemColors.Window;
			this.maskDateEdit1.Location = new System.Drawing.Point(136, 16);
			this.maskDateEdit1.Mask = "yyyy年MM月dd日";
			this.maskDateEdit1.Name = "maskDateEdit1";
			this.maskDateEdit1.Size = new System.Drawing.Size(120, 23);
			this.maskDateEdit1.TabIndex = 329;
			this.maskDateEdit1.Text = "";
			this.maskDateEdit1.TextChanged += new System.EventHandler(this.maskDateEdit1_TextChanged);
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.SystemColors.Control;
			this.label2.Location = new System.Drawing.Point(256, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 23);
			this.label2.TabIndex = 328;
			this.label2.Text = "到";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.Control;
			this.label1.Location = new System.Drawing.Point(56, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 23);
			this.label1.TabIndex = 327;
			this.label1.Text = "打印日期从";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonXP1
			// 
			this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.buttonXP1.DefaultScheme = true;
			this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXP1.Hint = "";
			this.buttonXP1.Location = new System.Drawing.Point(528, 11);
			this.buttonXP1.Name = "buttonXP1";
			this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.buttonXP1.Size = new System.Drawing.Size(96, 32);
			this.buttonXP1.TabIndex = 323;
			this.buttonXP1.TabStop = false;
			this.buttonXP1.Text = "退出(&E)";
			this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
			// 
			// buttonXP2
			// 
			this.buttonXP2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.buttonXP2.DefaultScheme = true;
			this.buttonXP2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXP2.Hint = "";
			this.buttonXP2.Location = new System.Drawing.Point(416, 11);
			this.buttonXP2.Name = "buttonXP2";
			this.buttonXP2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.buttonXP2.Size = new System.Drawing.Size(96, 32);
			this.buttonXP2.TabIndex = 324;
			this.buttonXP2.TabStop = false;
			this.buttonXP2.Text = "打印(&P)";
			this.buttonXP2.Click += new System.EventHandler(this.buttonXP2_Click);
			// 
			// frmOrdInPriceCheck
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(960, 445);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.Name = "frmOrdInPriceCheck";
			this.Text = "药品入库进价查询";
			this.Load += new System.EventHandler(this.frmOrdInPriceCheck_Load);
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region 变量
		clsDomainConrolCheck Domain=new clsDomainConrolCheck();
		DataTable dtOrdMed;
		DataTable dtDe=new DataTable();
		DataTable dtDeFind=new DataTable();
		#endregion

		private void frmOrdInPriceCheck_Load(object sender, System.EventArgs e)
		{
			DataTable dtMedType;
			Domain.m_lngGetMedType(out dtMedType);
			m_cboOPCal.Item.Add("全部药品","1");
			if(dtMedType.Rows.Count>0)
			{
				for(int i1=0;i1<dtMedType.Rows.Count;i1++)
				{
					m_cboOPCal.Item.Add(dtMedType.Rows[i1]["MEDICINETYPENAME_VCHR"].ToString(),dtMedType.Rows[i1]["MEDICINETYPEID_CHR"].ToString());
				}
			}
			m_cboOPCal.SelectedIndex=0;
			exComboBox1.Item.Add("全部","0");
			exComboBox1.Item.Add("中标药","1");
			exComboBox1.Item.Add("非中标药","2");
			exComboBox1.SelectedIndex=0;
		}
		#region 把药品填充到列表
		/// <summary>
		/// 把药品填充到列表
		/// </summary>
		/// <param name="DrAdd"></param>
		private void m_mthFillLis(DataRow DrAdd)
		{
			ListViewItem newItem=new ListViewItem(DrAdd["ASSISTCODE_CHR"].ToString().Trim());
			newItem.SubItems.Add(DrAdd["MEDICINENAME_VCHR"].ToString().Trim());
			newItem.SubItems.Add(DrAdd["PYCODE_CHR"].ToString().Trim());
			newItem.SubItems.Add(DrAdd["WBCODE_CHR"].ToString().Trim());
			newItem.Tag=DrAdd;
			listView1.Items.Add(newItem);
		}

		#region 查找药品
		DataTable dtFind=new DataTable();
		public void m_mthfind()
		{
			if(this.m_exCobType.Text!=""&&this.m_txtFind.Text!="")
			{
				if(dtOrdMed.Rows.Count==0)
					return;
				dtFind=dtOrdMed.Clone();
				string strFind="";
				listView1.Items.Clear();
				listView2.Items.Clear();
				string txtFind=m_txtFind.Text;
				for(int i1=0;i1<dtOrdMed.Rows.Count;i1++)
				{
					switch(m_exCobType.SelectedIndex)
					{
						case 0:
							strFind=dtOrdMed.Rows[i1]["ASSISTCODE_CHR"].ToString();
							break;
						case 1:
							strFind=dtOrdMed.Rows[i1]["MEDICINENAME_VCHR"].ToString();
							break;
						case 2:
							strFind=dtOrdMed.Rows[i1]["PYCODE_CHR"].ToString();
							txtFind=txtFind.ToUpper();
							break;
						case 3:
							strFind=dtOrdMed.Rows[i1]["WBCODE_CHR"].ToString();
							txtFind=txtFind.ToUpper();
							break;
					}
					if(strFind.IndexOf(txtFind,0)==0)
					{
						DataRow newRow=dtFind.NewRow();
						newRow["MEDICINEID_CHR"]=dtOrdMed.Rows[i1]["MEDICINEID_CHR"];
						newRow["ASSISTCODE_CHR"]=dtOrdMed.Rows[i1]["ASSISTCODE_CHR"];
						newRow["MEDICINETYPEID_CHR"]=dtOrdMed.Rows[i1]["MEDICINETYPEID_CHR"];
						newRow["PYCODE_CHR"]=dtOrdMed.Rows[i1]["PYCODE_CHR"];
						newRow["WBCODE_CHR"]=dtOrdMed.Rows[i1]["WBCODE_CHR"];
						newRow["MEDICINENAME_VCHR"]=dtOrdMed.Rows[i1]["MEDICINENAME_VCHR"];
						newRow["STANDARD_INT"]=dtOrdMed.Rows[i1]["STANDARD_INT"];
						m_mthFillLis(newRow);
						dtFind.Rows.Add(newRow);
					}
				}

			}
			else
			{
				clsPublicParm publicClass=new clsPublicParm();
				publicClass.m_mthShowWarning(this.panel5,"请输入查询条件！");
			}

		}
		#endregion
		#endregion
		#region 把药品入库数据填充到明细列表
		/// <summary>
		/// 把药品入库数据填充到明细列表
		/// </summary>
		/// <param name="DrAdd"></param>
		private void m_mthFillLisDe(DataRow DrAdd)
		{
			ListViewItem newItem=new ListViewItem(DrAdd["DOCID_VCHR"].ToString().Trim());
			newItem.SubItems.Add(DrAdd["VENDORNAME_VCHR"].ToString().Trim());
			newItem.SubItems.Add(DrAdd["SYSLOTNO_CHR"].ToString().Trim());
			newItem.SubItems.Add(DrAdd["LOTNO_VCHR"].ToString().Trim());
			newItem.SubItems.Add(DrAdd["QTY_DEC"].ToString().Trim());
			newItem.SubItems.Add(DrAdd["UNITID_CHR"].ToString().Trim());
			newItem.SubItems.Add(DrAdd["BUYUNITPRICE_MNY"].ToString().Trim());
			newItem.SubItems.Add(DrAdd["BUYTOLPRICE_MNY"].ToString().Trim());
			listView2.Items.Add(newItem);
		}
		#endregion
		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void m_cboOPCal_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(m_cboOPCal.SelectItemValue=="1")
			{
				Domain.m_lngOrdInPriceCheck(out dtOrdMed,null);
			}
			else
			{
				Domain.m_lngOrdInPriceCheck(out dtOrdMed,m_cboOPCal.SelectItemValue.Trim());
			}
			listView1.Items.Clear();
			if(dtOrdMed.Rows.Count>0)
			{
				for(int f2=0;f2<dtOrdMed.Rows.Count;f2++)
				{
					m_mthFillLis(dtOrdMed.Rows[f2]);
				}
			}
		}

		private void exComboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			listView1.Items.Clear();
			listView2.Items.Clear();
			switch(exComboBox1.SelectItemValue)
			{
				case "0":
					if(dtOrdMed.Rows.Count>0)
					{
						for(int f2=0;f2<dtOrdMed.Rows.Count;f2++)
						{
							m_mthFillLis(dtOrdMed.Rows[f2]);
						}
					}
					break;
				case "1":
					DataRow[] SeleRow=dtOrdMed.Select("STANDARD_INT=1");
					if(SeleRow.Length>0)
					{
						for(int i1=0;i1<SeleRow.Length;i1++)
						{
							m_mthFillLis(SeleRow[i1]);
						}
					}
					break;
				case "2":
					DataRow[] SeleRow1=dtOrdMed.Select("STANDARD_INT=0");
					if(SeleRow1.Length>0)
					{
						for(int i1=0;i1<SeleRow1.Length;i1++)
						{
							m_mthFillLis(SeleRow1[i1]);
						}
					}
					break;
			}
		}

		private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			listView2.Items.Clear();
			if(listView1.SelectedItems.Count>0)
			{
				DataRow drSele=(DataRow)listView1.SelectedItems[0].Tag;
				Domain.m_lngGetMedDe(drSele["MEDICINEID_CHR"].ToString().Trim(),out dtDe);
				if(dtDe.Rows.Count>0)
				{
					for(int i1=0;i1<dtDe.Rows.Count;i1++)
					{
						m_mthFillLisDe(dtDe.Rows[i1]);
					}
				}
				groupBox1.Tag=drSele["MEDICINENAME_VCHR"].ToString();
			}
			dtDeFind=dtDe.Clone();
			listView2.Tag="dtDe";
			maskDateEdit1.Text="";
			maskDateEdit2.Text="";
		}

		private void maskDateEdit1_TextChanged(object sender, System.EventArgs e)
		{
			dtDeFind.Rows.Clear();
			if(maskDateEdit1.Text!=""&&maskDateEdit2.Text!="")
			{
				listView2.Items.Clear();
				for(int i1=0;i1<dtDe.Rows.Count;i1++)
				{
					if(DateTime.Parse(dtDe.Rows[i1]["ORD_DAT"].ToString())>=DateTime.Parse(DateTime.Parse(maskDateEdit1.Text).ToString()+" 00:00:00")&&DateTime.Parse(dtDe.Rows[i1]["ORD_DAT"].ToString())>=DateTime.Parse(DateTime.Parse(maskDateEdit1.Text).ToString()+" 23:59:59"))
					{
						m_mthAddDtFind(dtDe.Rows[i1],ref dtDeFind);
					}
				}
				if(dtDeFind.Rows.Count>0)
				{
					for(int f2=0;f2<dtDeFind.Rows.Count;f2++)
					{
						m_mthFillLisDe(dtDeFind.Rows[f2]);
					}
				}
				listView2.Tag="dtDeFind";
			}
			
		}
		#region 为查找明细表构造一行
		private void m_mthAddDtFind(DataRow drAdd,ref DataTable dtDeFind)
		{
			DataRow dr=dtDeFind.NewRow();
			dr["MEDICINEID_CHR"]=drAdd["MEDICINEID_CHR"];
			dr["SYSLOTNO_CHR"]=drAdd["SYSLOTNO_CHR"];
			dr["ORD_DAT"]=drAdd["ORD_DAT"];
			dr["UNITID_CHR"]=drAdd["UNITID_CHR"];
			dr["LOTNO_VCHR"]=drAdd["LOTNO_VCHR"];
			dr["USEFULLIFE_DAT"]=drAdd["USEFULLIFE_DAT"];
			dr["PRODCUTORID_CHR"]=drAdd["PRODCUTORID_CHR"];
			dr["QTY_DEC"]=drAdd["QTY_DEC"];
			dr["UNITID_CHR"]=drAdd["UNITID_CHR"];
			dr["BUYUNITPRICE_MNY"]=drAdd["BUYUNITPRICE_MNY"];
			dr["BUYTOLPRICE_MNY"]=drAdd["BUYTOLPRICE_MNY"];
			dr["DOCID_VCHR"]=drAdd["DOCID_VCHR"];
			dr["VENDORNAME_VCHR"]=drAdd["VENDORNAME_VCHR"];
			dr["MEDICINENAME_VCHR"]=drAdd["MEDICINENAME_VCHR"];
			dtDeFind.Rows.Add(dr);
		}
		#endregion

		private void buttonXP2_Click(object sender, System.EventArgs e)
		{
			//com.digitalwave.iCare.gui.HIS.baotable.OrdInPriceCheckRpt Rpt=new com.digitalwave.iCare.gui.HIS.baotable.OrdInPriceCheckRpt();
			//if((string)listView2.Tag=="dtDeFind")
			//{
			//	if(dtDeFind.Rows.Count>0)
			//	{
			//		Rpt.SetDataSource(dtDeFind);
			//		((TextObject)Rpt.ReportDefinition.ReportObjects["Text11"]).Text = (string)groupBox1.Tag;
			//		((TextObject)Rpt.ReportDefinition.ReportObjects["Text12"]).Text = "入库日期:";
			//		((TextObject)Rpt.ReportDefinition.ReportObjects["Text13"]).Text =maskDateEdit1.Text+" 至 "+ maskDateEdit2.Text;
			//		((TextObject)Rpt.ReportDefinition.ReportObjects["Text15"]).Text =DateTime.Now.ToShortDateString();
			//		frmShowReport ShowPrint=new frmShowReport();
			//		ShowPrint.crystalReportViewer1.ReportSource=Rpt;
			//		ShowPrint.ShowDialog();
			//	}
			//}
			//else
			//{
			//	if(dtDe.Rows.Count>0)
			//	{
   //                 Rpt.SetDataSource(dtDe);
   //                 ((TextObject)Rpt.ReportDefinition.ReportObjects["Text11"]).Text = (string)groupBox1.Tag;
   //                 ((TextObject)Rpt.ReportDefinition.ReportObjects["Text15"]).Text = DateTime.Now.ToShortDateString();
   //                 frmShowReport ShowPrint = new frmShowReport();
   //                 ShowPrint.crystalReportViewer1.ReportSource = Rpt;
   //                 ShowPrint.ShowDialog();
   //             }
			//}
		}

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void buttonXP3_Click(object sender, System.EventArgs e)
		{
			m_mthfind();
		}

		private void buttonXP4_Click(object sender, System.EventArgs e)
		{
			listView1.Items.Clear();
			listView2.Items.Clear();
			if(dtOrdMed.Rows.Count>0)
			{
				for(int f2=0;f2<dtOrdMed.Rows.Count;f2++)
				{
					m_mthFillLis(dtOrdMed.Rows[f2]);
				}
			}
		}
	}
}
