using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data; 
using RIS_GUI.report;

namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// frmRISrepot 的摘要说明。
	/// </summary>
	public class frmRISrepot : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.TabControl tabreport;
		private System.Windows.Forms.TabPage TCDreport;
		private System.Windows.Forms.TabPage EEGreport;
		private System.Windows.Forms.Label lable1;
		private System.Windows.Forms.Label label2;
		//private CrystalDecisions.Windows.Forms.CrystalReportViewer rptTCDreportView;
		private com.digitalwave.iCare.gui.RIS.clsController_RISRPT objTable;
		private System.Windows.Forms.DateTimePicker dtptoDat;
		private System.Windows.Forms.DateTimePicker dtpfromDat;
		//private rptTCDreport rptTCD =new rptTCDreport();
		//private rptEEGreport rptEEG =new rptEEGreport();
		private PinkieControls.ButtonXP cmdExit;
		private PinkieControls.ButtonXP cmdreport;
		private PinkieControls.ButtonXP cmdEEGrpt;
		private PinkieControls.ButtonXP cmdquit;
		
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dtpfromEEGDat;
		private System.Windows.Forms.DateTimePicker dtptoEEGDat;
		//private CrystalDecisions.Windows.Forms.CrystalReportViewer rptEEGreportView;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.TextBox m_ctldept;
		private System.Windows.Forms.TextBox m_ctlEEGdept;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.ComboBox comboBox4;
		private System.Windows.Forms.ComboBox comboBox3;
		private System.Windows.Forms.ComboBox comboBox6;
		private System.Windows.Forms.ComboBox comboBox5;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox3;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmRISrepot()
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
			this.tabreport = new System.Windows.Forms.TabControl();
			this.TCDreport = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.comboBox4 = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			//this.rptTCDreportView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.cmdExit = new PinkieControls.ButtonXP();
			this.cmdreport = new PinkieControls.ButtonXP();
			this.label2 = new System.Windows.Forms.Label();
			this.lable1 = new System.Windows.Forms.Label();
			this.dtptoDat = new System.Windows.Forms.DateTimePicker();
			this.dtpfromDat = new System.Windows.Forms.DateTimePicker();
			this.m_ctldept = new System.Windows.Forms.TextBox();
			this.EEGreport = new System.Windows.Forms.TabPage();
			this.panel2 = new System.Windows.Forms.Panel();
			this.comboBox6 = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.comboBox5 = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			//this.rptEEGreportView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdquit = new PinkieControls.ButtonXP();
			this.cmdEEGrpt = new PinkieControls.ButtonXP();
			this.dtptoEEGDat = new System.Windows.Forms.DateTimePicker();
			this.dtpfromEEGDat = new System.Windows.Forms.DateTimePicker();
			this.m_ctlEEGdept = new System.Windows.Forms.TextBox();
			this.tabreport.SuspendLayout();
			this.TCDreport.SuspendLayout();
			this.panel1.SuspendLayout();
			this.EEGreport.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabreport
			// 
			this.tabreport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabreport.Controls.Add(this.TCDreport);
			this.tabreport.Controls.Add(this.EEGreport);
			this.tabreport.Font = new System.Drawing.Font("宋体", 10.5F);
			this.tabreport.Location = new System.Drawing.Point(0, 0);
			this.tabreport.Name = "tabreport";
			this.tabreport.SelectedIndex = 0;
			this.tabreport.Size = new System.Drawing.Size(1128, 618);
			this.tabreport.TabIndex = 0;
			// 
			// TCDreport
			// 
			this.TCDreport.Controls.Add(this.panel1);
			this.TCDreport.Controls.Add(this.comboBox1);
			//this.TCDreport.Controls.Add(this.rptTCDreportView);
			this.TCDreport.Controls.Add(this.cmdExit);
			this.TCDreport.Controls.Add(this.cmdreport);
			this.TCDreport.Controls.Add(this.label2);
			this.TCDreport.Controls.Add(this.lable1);
			this.TCDreport.Controls.Add(this.dtptoDat);
			this.TCDreport.Controls.Add(this.dtpfromDat);
			this.TCDreport.Controls.Add(this.m_ctldept);
			this.TCDreport.Location = new System.Drawing.Point(4, 23);
			this.TCDreport.Name = "TCDreport";
			this.TCDreport.Size = new System.Drawing.Size(1120, 591);
			this.TCDreport.TabIndex = 0;
			this.TCDreport.Text = "TCD报表";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.comboBox4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.textBox2);
			this.panel1.Controls.Add(this.comboBox3);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Location = new System.Drawing.Point(448, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(232, 24);
			this.panel1.TabIndex = 14;
			// 
			// comboBox4
			// 
			this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox4.Items.AddRange(new object[] {
														   "岁",
														   "月",
														   "天"});
			this.comboBox4.Location = new System.Drawing.Point(192, 0);
			this.comboBox4.Name = "comboBox4";
			this.comboBox4.Size = new System.Drawing.Size(40, 22);
			this.comboBox4.TabIndex = 10;
			this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(120, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(20, 19);
			this.label3.TabIndex = 9;
			this.label3.Text = "到";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(144, 0);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(40, 23);
			this.textBox2.TabIndex = 8;
			this.textBox2.Text = "";
			// 
			// comboBox3
			// 
			this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox3.Items.AddRange(new object[] {
														   "岁",
														   "月",
														   "天"});
			this.comboBox3.Location = new System.Drawing.Point(80, 0);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(40, 22);
			this.comboBox3.TabIndex = 7;
			this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(22, 22);
			this.label5.TabIndex = 6;
			this.label5.Text = "从";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(32, 0);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(40, 23);
			this.textBox1.TabIndex = 5;
			this.textBox1.Text = "";
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Items.AddRange(new object[] {
														   "按部门查询",
														   "按临床诊断查询",
														   "按年龄查询",
														   "按性别查询",
														   "按TCD诊断查询"});
			this.comboBox1.Location = new System.Drawing.Point(320, 8);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(128, 22);
			this.comboBox1.TabIndex = 12;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// rptTCDreportView
			// 
			//this.rptTCDreportView.ActiveViewIndex = -1;
			//this.rptTCDreportView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			//	| System.Windows.Forms.AnchorStyles.Left) 
			//	| System.Windows.Forms.AnchorStyles.Right)));
			//this.rptTCDreportView.DisplayGroupTree = false;
			//this.rptTCDreportView.DisplayToolbar = false;
			//this.rptTCDreportView.Font = new System.Drawing.Font("宋体", 10.5F);
			//this.rptTCDreportView.Location = new System.Drawing.Point(8, 40);
			//this.rptTCDreportView.Name = "rptTCDreportView";
			//this.rptTCDreportView.ReportSource = null;
			//this.rptTCDreportView.ShowCloseButton = false;
			//this.rptTCDreportView.ShowGroupTreeButton = false;
			//this.rptTCDreportView.Size = new System.Drawing.Size(912, 552);
			//this.rptTCDreportView.TabIndex = 0;
			// 
			// cmdExit
			// 
			this.cmdExit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.cmdExit.DefaultScheme = true;
			this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.None;
			this.cmdExit.Hint = "";
			this.cmdExit.Location = new System.Drawing.Point(816, 8);
			this.cmdExit.Name = "cmdExit";
			this.cmdExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.cmdExit.Size = new System.Drawing.Size(104, 32);
			this.cmdExit.TabIndex = 11;
			this.cmdExit.Text = "退出";
			this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
			// 
			// cmdreport
			// 
			this.cmdreport.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.cmdreport.DefaultScheme = true;
			this.cmdreport.DialogResult = System.Windows.Forms.DialogResult.None;
			this.cmdreport.Hint = "";
			this.cmdreport.Location = new System.Drawing.Point(696, 8);
			this.cmdreport.Name = "cmdreport";
			this.cmdreport.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.cmdreport.Size = new System.Drawing.Size(104, 32);
			this.cmdreport.TabIndex = 10;
			this.cmdreport.Text = "生成报表";
			this.cmdreport.Click += new System.EventHandler(this.buttonXP1_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(168, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(16, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "到";
			// 
			// lable1
			// 
			this.lable1.Location = new System.Drawing.Point(24, 11);
			this.lable1.Name = "lable1";
			this.lable1.Size = new System.Drawing.Size(16, 16);
			this.lable1.TabIndex = 7;
			this.lable1.Text = "从";
			// 
			// dtptoDat
			// 
			this.dtptoDat.Location = new System.Drawing.Point(192, 8);
			this.dtptoDat.Name = "dtptoDat";
			this.dtptoDat.Size = new System.Drawing.Size(120, 23);
			this.dtptoDat.TabIndex = 2;
			// 
			// dtpfromDat
			// 
			this.dtpfromDat.Location = new System.Drawing.Point(48, 8);
			this.dtpfromDat.Name = "dtpfromDat";
			this.dtpfromDat.Size = new System.Drawing.Size(120, 23);
			this.dtpfromDat.TabIndex = 1;
			// 
			// m_ctldept
			// 
			this.m_ctldept.Location = new System.Drawing.Point(456, 8);
			this.m_ctldept.Name = "m_ctldept";
			this.m_ctldept.Size = new System.Drawing.Size(152, 23);
			this.m_ctldept.TabIndex = 13;
			this.m_ctldept.Text = "";
			// 
			// EEGreport
			// 
			this.EEGreport.Controls.Add(this.panel2);
			this.EEGreport.Controls.Add(this.comboBox2);
			//this.EEGreport.Controls.Add(this.rptEEGreportView);
			this.EEGreport.Controls.Add(this.label4);
			this.EEGreport.Controls.Add(this.label1);
			this.EEGreport.Controls.Add(this.cmdquit);
			this.EEGreport.Controls.Add(this.cmdEEGrpt);
			this.EEGreport.Controls.Add(this.dtptoEEGDat);
			this.EEGreport.Controls.Add(this.dtpfromEEGDat);
			this.EEGreport.Controls.Add(this.m_ctlEEGdept);
			this.EEGreport.Location = new System.Drawing.Point(4, 23);
			this.EEGreport.Name = "EEGreport";
			this.EEGreport.Size = new System.Drawing.Size(1120, 591);
			this.EEGreport.TabIndex = 1;
			this.EEGreport.Text = "EEG报表";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.comboBox6);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.textBox4);
			this.panel2.Controls.Add(this.comboBox5);
			this.panel2.Controls.Add(this.label7);
			this.panel2.Controls.Add(this.textBox3);
			this.panel2.Location = new System.Drawing.Point(440, 8);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(232, 24);
			this.panel2.TabIndex = 15;
			// 
			// comboBox6
			// 
			this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox6.Items.AddRange(new object[] {
														   "岁",
														   "月",
														   "天"});
			this.comboBox6.Location = new System.Drawing.Point(192, 0);
			this.comboBox6.Name = "comboBox6";
			this.comboBox6.Size = new System.Drawing.Size(40, 22);
			this.comboBox6.TabIndex = 10;
			this.comboBox6.SelectedIndexChanged += new System.EventHandler(this.comboBox6_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(120, 3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(20, 19);
			this.label6.TabIndex = 9;
			this.label6.Text = "到";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(144, 0);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(40, 23);
			this.textBox4.TabIndex = 8;
			this.textBox4.Text = "";
			// 
			// comboBox5
			// 
			this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox5.Items.AddRange(new object[] {
														   "岁",
														   "月",
														   "天"});
			this.comboBox5.Location = new System.Drawing.Point(80, 0);
			this.comboBox5.Name = "comboBox5";
			this.comboBox5.Size = new System.Drawing.Size(40, 22);
			this.comboBox5.TabIndex = 7;
			this.comboBox5.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 3);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(22, 22);
			this.label7.TabIndex = 6;
			this.label7.Text = "从";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(32, 0);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(40, 23);
			this.textBox3.TabIndex = 5;
			this.textBox3.Text = "";
			// 
			// comboBox2
			// 
			this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox2.Items.AddRange(new object[] {
														   "按部门查询",
														   "按临床诊断查询",
														   "按年龄段查询",
														   "按性别查询",
														   "按EEG诊断查询"});
			this.comboBox2.Location = new System.Drawing.Point(320, 8);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(120, 22);
			this.comboBox2.TabIndex = 13;
			this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
			// 
			// rptEEGreportView
			// 
			//this.rptEEGreportView.ActiveViewIndex = -1;
			//this.rptEEGreportView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			//	| System.Windows.Forms.AnchorStyles.Left) 
			//	| System.Windows.Forms.AnchorStyles.Right)));
			//this.rptEEGreportView.DisplayGroupTree = false;
			//this.rptEEGreportView.DisplayToolbar = false;
			//this.rptEEGreportView.Location = new System.Drawing.Point(8, 40);
			//this.rptEEGreportView.Name = "rptEEGreportView";
			//this.rptEEGreportView.ReportSource = null;
			//this.rptEEGreportView.ShowCloseButton = false;
			//this.rptEEGreportView.ShowGroupTreeButton = false;
			//this.rptEEGreportView.Size = new System.Drawing.Size(912, 552);
			//this.rptEEGreportView.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(168, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(24, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "到";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(24, 23);
			this.label1.TabIndex = 6;
			this.label1.Text = "从";
			// 
			// cmdquit
			// 
			this.cmdquit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.cmdquit.DefaultScheme = true;
			this.cmdquit.DialogResult = System.Windows.Forms.DialogResult.None;
			this.cmdquit.Hint = "";
			this.cmdquit.Location = new System.Drawing.Point(808, 7);
			this.cmdquit.Name = "cmdquit";
			this.cmdquit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.cmdquit.Size = new System.Drawing.Size(104, 32);
			this.cmdquit.TabIndex = 4;
			this.cmdquit.Text = "退出";
			this.cmdquit.Click += new System.EventHandler(this.cmdquit_Click);
			// 
			// cmdEEGrpt
			// 
			this.cmdEEGrpt.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.cmdEEGrpt.DefaultScheme = true;
			this.cmdEEGrpt.DialogResult = System.Windows.Forms.DialogResult.None;
			this.cmdEEGrpt.Hint = "";
			this.cmdEEGrpt.Location = new System.Drawing.Point(688, 8);
			this.cmdEEGrpt.Name = "cmdEEGrpt";
			this.cmdEEGrpt.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.cmdEEGrpt.Size = new System.Drawing.Size(104, 32);
			this.cmdEEGrpt.TabIndex = 3;
			this.cmdEEGrpt.Text = "生成报表";
			this.cmdEEGrpt.Click += new System.EventHandler(this.cmdEEGrpt_Click);
			// 
			// dtptoEEGDat
			// 
			this.dtptoEEGDat.Location = new System.Drawing.Point(192, 10);
			this.dtptoEEGDat.Name = "dtptoEEGDat";
			this.dtptoEEGDat.Size = new System.Drawing.Size(120, 23);
			this.dtptoEEGDat.TabIndex = 2;
			// 
			// dtpfromEEGDat
			// 
			this.dtpfromEEGDat.Location = new System.Drawing.Point(48, 10);
			this.dtpfromEEGDat.Name = "dtpfromEEGDat";
			this.dtpfromEEGDat.Size = new System.Drawing.Size(120, 23);
			this.dtpfromEEGDat.TabIndex = 1;
			// 
			// m_ctlEEGdept
			// 
			this.m_ctlEEGdept.Location = new System.Drawing.Point(448, 8);
			this.m_ctlEEGdept.Name = "m_ctlEEGdept";
			this.m_ctlEEGdept.Size = new System.Drawing.Size(176, 23);
			this.m_ctlEEGdept.TabIndex = 14;
			this.m_ctlEEGdept.Text = "";
			// 
			// frmRISrepot
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(928, 623);
			this.Controls.Add(this.tabreport);
			this.Name = "frmRISrepot";
			this.Text = "frmRISrepot";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.frmRISrepot_Load);
			this.tabreport.ResumeLayout(false);
			this.TCDreport.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.EEGreport.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
			DataTable dtbTCDreport=new DataTable();
			string strfromDat=this.dtpfromDat.Value.ToString("yyyy-MM-dd 00:00:00");
			string strtoDat=this.dtptoDat.Value.ToString("yyyy-MM-dd 23:59:59");
			objTable=new com.digitalwave.iCare.gui.RIS.clsController_RISRPT();
			string strDept="";
			if(m_ctldept.Visible==true)
			{
				if(m_ctldept.Text.Trim()!="")
				{
					strDept="AND "+this.comboBox1.Tag.ToString()+" like '%"+m_ctldept.Text.Trim()+"%'";
				}
				
			}
			bool flag=m_ctldept.Visible==true;
//			else
//			{
//				if(textBox1.Text.Trim()!=""&&textBox2.Text.Trim()!="")
//				{
//					strDept="AND "+this.comboBox1.Tag.ToString()+" Between '"+comboBox3.Tag.ToString()+textBox1.Text.Trim()+"' AND '"+comboBox4.Tag.ToString()+textBox2.Text.Trim()+"'";
//				}
//			
//			}
			objTable.m_mthSetTCDReportdtb(strfromDat,strtoDat,strDept,out dtbTCDreport,comboBox3.Tag.ToString(),textBox1.Text.Trim(),comboBox4.Tag.ToString(),textBox2.Text.Trim(),flag);
//			rptTCD.SetDataSource(dtbTCDreport);
////			rptTCD.DataDefinition.FormulaFields[0]=dtbTCDreport.Columns["dtbTCDreport"];
//			this.rptTCDreportView.ReportSource=rptTCD;
//			this.rptTCDreportView.RefreshReport();
			
		}

		private void cmdEEGrpt_Click(object sender, System.EventArgs e)
		{
			DataTable dtbEEGreport=new DataTable();
			string strfromDat=this.dtpfromEEGDat.Value.ToString("yyyy-MM-dd 00:00:00");
			string strtoDat=this.dtptoEEGDat.Value.ToString("yyyy-MM-dd 23:59:59");
			objTable=new com.digitalwave.iCare.gui.RIS.clsController_RISRPT();
			 		
//			string	strDept=this.m_ctlEEGdept.Text.Trim();
			string strDept="";
			if(m_ctlEEGdept.Visible==true)
			{
				if(m_ctldept.Text.Trim()!="")
				{
					strDept="AND "+this.comboBox2.Tag.ToString()+" like '%"+m_ctlEEGdept.Text.Trim()+"%'";
				}
				
			}
			bool flag=m_ctlEEGdept.Visible==true;
//			else
//			{
//				if(textBox3.Text.Trim()!=""&&textBox4.Text.Trim()!="")
//				{
//					strDept="AND "+this.comboBox2.Tag.ToString()+" Between '"+comboBox5.Tag.ToString()+textBox3.Text.Trim()+"' AND '"+comboBox6.Tag.ToString()+textBox4.Text.Trim()+"'";
//				}
//			
//			}	
			objTable.m_mthSetEEGReportdtb(strfromDat,strtoDat,strDept,out dtbEEGreport,comboBox5.Tag.ToString(),textBox3.Text.Trim(),comboBox6.Tag.ToString(),textBox4.Text.Trim(),flag);
			//rptEEG.SetDataSource(dtbEEGreport);
			//this.rptEEGreportView.ReportSource=rptEEG;
			//this.rptEEGreportView.RefreshReport();
		
		}

		private void cmdExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void cmdquit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmRISrepot_Load(object sender, System.EventArgs e)
		{
			//this.rptEEGreportView.DisplayToolbar=true;
			//this.rptTCDreportView.DisplayToolbar=true;
			this.comboBox2.SelectedIndex=0;
			this.comboBox3.SelectedIndex=0;
			this.comboBox4.SelectedIndex=0;
			this.comboBox5.SelectedIndex=0;
			this.comboBox6.SelectedIndex=0;
			this.comboBox1.SelectedIndex=0;
		}

		private void comboBox2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string str="";
			switch(comboBox2.SelectedIndex)
			{
				case 0:
					str="DEPT_NAME_VCHR";
					m_ctlEEGdept.Visible=true;
					panel2.Visible=false;
					
					break;
				case 1:
					str="DIAGNOSE_VCHR";
					m_ctlEEGdept.Visible=true;
					panel2.Visible=false;
					break;
				case 2:
					str="AGE_FLT";
					m_ctlEEGdept.Visible=false;
					panel2.Visible=true;
					m_ctlEEGdept.Clear();
					break;
				case 3:
					str="SEX_CHR";
					m_ctlEEGdept.Visible=true;
					panel2.Visible=false;
					break;
				case 4:
					str="SUMMARY1_VCHR";
					m_ctlEEGdept.Visible=true;
					panel2.Visible=false;
					break;
			}
			comboBox2.Tag=str;
			this.m_ctlEEGdept.Select();
		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string str="";
			switch(comboBox1.SelectedIndex)
			{
				case 0:
					str="DEPT_NAME_VCHR";
					m_ctldept.Visible=true;
					panel1.Visible=false;
					break;
				case 1:
					str="DIAGNOSE_VCHR";
					m_ctldept.Visible=true;
					panel1.Visible=false;
					break;
				case 2:
					str="AGE_FLT";
					m_ctldept.Visible=false;
					m_ctldept.Clear();
					panel1.Visible=true;
					break;
				case 3:
					str="SEX_CHR";
					m_ctldept.Visible=true;
					panel1.Visible=false;
					break;
				case 4:
					str="SUMMARY2_VCHR";
					m_ctldept.Visible=true;
					panel1.Visible=false;
					break;
			}
			comboBox1.Tag=str;
			this.m_ctldept.Select();
		}

		private void comboBox5_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch(comboBox5.SelectedIndex)
			{
				case 0:
					comboBox5.Tag="C";
					break;
				case 1:
					comboBox5.Tag="B";
					break;
				case 2:
					comboBox5.Tag="A";
					break;
			}
				textBox4.Select();
		}

		private void comboBox3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch(comboBox3.SelectedIndex)
			{
				case 0:
					comboBox3.Tag="C";
					break;
				case 1:
					comboBox3.Tag="B";
					break;
				case 2:
					comboBox3.Tag="A";
					break;
			}
			textBox2.Select();
		}

		private void comboBox4_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch(comboBox4.SelectedIndex)
			{
				case 0:
					comboBox4.Tag="C";
					break;
				case 1:
					comboBox4.Tag="B";
					break;
				case 2:
					comboBox4.Tag="A";
					break;
			}
		
		}

		private void comboBox6_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch(comboBox6.SelectedIndex)
			{
				case 0:
					comboBox6.Tag="C";
					break;
				case 1:
					comboBox6.Tag="B";
					break;
				case 2:
					comboBox6.Tag="A";
					break;
			}
		}
	}
}
