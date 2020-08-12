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
	/// frmCardiogramReportrpt 的摘要说明。
	/// </summary>
	public class frmCardiogramReportrpt : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private PinkieControls.ButtonXP m_cmdreport;
		private PinkieControls.ButtonXP m_cmdTP1exit;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private com.digitalwave.iCare.gui.RIS.clsController_RISRPT objTable;
		//private rptCardiogram rptCardiogram =new rptCardiogram();
		//private rptDnmCardiogram rptDnmCardiogram =new rptDnmCardiogram();
		private System.Windows.Forms.DateTimePicker dtpfromDat;
		private System.Windows.Forms.DateTimePicker dtptoDat;
		private com.digitalwave.Utility.ctlDeptTextBox m_ctldept;
		//private CrystalDecisions.Windows.Forms.CrystalReportViewer rptCardiogramView;
		//private CrystalDecisions.Windows.Forms.CrystalReportViewer rptDnmCardiogramView;
		private PinkieControls.ButtonXP m_cmdDnmrpt;
		private PinkieControls.ButtonXP m_cmdDnmExit;
		private com.digitalwave.Utility.ctlDeptTextBox m_deptDnm;
		private System.Windows.Forms.DateTimePicker m_toDatDnm;
		private System.Windows.Forms.DateTimePicker m_fromDatDnm;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox m_txtFind;
		private System.Windows.Forms.TextBox m_txtFind2;
		private System.Windows.Forms.Label label8;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmCardiogramReportrpt()
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.m_txtFind = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.m_cmdTP1exit = new PinkieControls.ButtonXP();
			this.m_cmdreport = new PinkieControls.ButtonXP();
			//this.rptCardiogramView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.m_ctldept = new com.digitalwave.Utility.ctlDeptTextBox();
			this.dtptoDat = new System.Windows.Forms.DateTimePicker();
			this.dtpfromDat = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			//this.rptDnmCardiogramView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.m_cmdDnmExit = new PinkieControls.ButtonXP();
			this.m_cmdDnmrpt = new PinkieControls.ButtonXP();
			this.m_deptDnm = new com.digitalwave.Utility.ctlDeptTextBox();
			this.m_toDatDnm = new System.Windows.Forms.DateTimePicker();
			this.m_fromDatDnm = new System.Windows.Forms.DateTimePicker();
			this.m_txtFind2 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(-8, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(850, 610);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.m_txtFind);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.m_cmdTP1exit);
			this.tabPage1.Controls.Add(this.m_cmdreport);
			//this.tabPage1.Controls.Add(this.rptCardiogramView);
			this.tabPage1.Controls.Add(this.m_ctldept);
			this.tabPage1.Controls.Add(this.dtptoDat);
			this.tabPage1.Controls.Add(this.dtpfromDat);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Location = new System.Drawing.Point(4, 23);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(842, 583);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "心电图报表";
			// 
			// m_txtFind
			// 
			this.m_txtFind.Location = new System.Drawing.Point(520, 6);
			this.m_txtFind.Name = "m_txtFind";
			this.m_txtFind.Size = new System.Drawing.Size(88, 23);
			this.m_txtFind.TabIndex = 10;
			this.m_txtFind.Text = "";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label7.Location = new System.Drawing.Point(472, 8);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 16);
			this.label7.TabIndex = 9;
			this.label7.Text = "诊断：";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.Location = new System.Drawing.Point(320, 11);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 8;
			this.label3.Text = "部门：";
			// 
			// m_cmdTP1exit
			// 
			this.m_cmdTP1exit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_cmdTP1exit.DefaultScheme = true;
			this.m_cmdTP1exit.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_cmdTP1exit.Hint = "";
			this.m_cmdTP1exit.Location = new System.Drawing.Point(736, 5);
			this.m_cmdTP1exit.Name = "m_cmdTP1exit";
			this.m_cmdTP1exit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_cmdTP1exit.Size = new System.Drawing.Size(88, 24);
			this.m_cmdTP1exit.TabIndex = 7;
			this.m_cmdTP1exit.Text = "退出";
			this.m_cmdTP1exit.Click += new System.EventHandler(this.m_cmdTP1exit_Click);
			// 
			// m_cmdreport
			// 
			this.m_cmdreport.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_cmdreport.DefaultScheme = true;
			this.m_cmdreport.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_cmdreport.Hint = "";
			this.m_cmdreport.Location = new System.Drawing.Point(632, 5);
			this.m_cmdreport.Name = "m_cmdreport";
			this.m_cmdreport.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_cmdreport.Size = new System.Drawing.Size(88, 24);
			this.m_cmdreport.TabIndex = 6;
			this.m_cmdreport.Text = "生成报表";
			this.m_cmdreport.Click += new System.EventHandler(this.m_cmdreport_Click);
			// 
			// rptCardiogramView
			// 
			//this.rptCardiogramView.ActiveViewIndex = -1;
			//this.rptCardiogramView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			//	| System.Windows.Forms.AnchorStyles.Left) 
			//	| System.Windows.Forms.AnchorStyles.Right)));
			//this.rptCardiogramView.DisplayGroupTree = false;
			//this.rptCardiogramView.DisplayToolbar = false;
			//this.rptCardiogramView.Location = new System.Drawing.Point(8, 40);
			//this.rptCardiogramView.Name = "rptCardiogramView";
			//this.rptCardiogramView.ReportSource = null;
			//this.rptCardiogramView.ShowCloseButton = false;
			//this.rptCardiogramView.ShowGroupTreeButton = false;
			//this.rptCardiogramView.Size = new System.Drawing.Size(834, 544);
			//this.rptCardiogramView.TabIndex = 5;
			// 
			// m_ctldept
			// 
			//this.m_ctldept.EnableAutoValidation = true;
			//this.m_ctldept.EnableEnterKeyValidate = true;
			//this.m_ctldept.EnableEscapeKeyUndo = true;
			//this.m_ctldept.EnableLastValidValue = true;
			//this.m_ctldept.ErrorProvider = null;
			//this.m_ctldept.ErrorProviderMessage = "Invalid value";
			this.m_ctldept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			//this.m_ctldept.ForceFormatText = true;
			this.m_ctldept.Location = new System.Drawing.Point(368, 6);
			this.m_ctldept.m_StrDeptID = null;
			this.m_ctldept.m_StrDeptName = null;
			this.m_ctldept.Name = "m_ctldept";
			this.m_ctldept.Size = new System.Drawing.Size(96, 23);
			this.m_ctldept.TabIndex = 4;
			this.m_ctldept.Text = "";
			// 
			// dtptoDat
			// 
			this.dtptoDat.Location = new System.Drawing.Point(192, 8);
			this.dtptoDat.Name = "dtptoDat";
			this.dtptoDat.Size = new System.Drawing.Size(120, 23);
			this.dtptoDat.TabIndex = 3;
			// 
			// dtpfromDat
			// 
			this.dtpfromDat.Location = new System.Drawing.Point(40, 8);
			this.dtpfromDat.Name = "dtpfromDat";
			this.dtpfromDat.Size = new System.Drawing.Size(120, 23);
			this.dtpfromDat.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.Location = new System.Drawing.Point(168, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(16, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "到";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(16, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "从";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.m_txtFind2);
			this.tabPage2.Controls.Add(this.label8);
			//this.tabPage2.Controls.Add(this.rptDnmCardiogramView);
			this.tabPage2.Controls.Add(this.label6);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.label4);
			this.tabPage2.Controls.Add(this.m_cmdDnmExit);
			this.tabPage2.Controls.Add(this.m_cmdDnmrpt);
			this.tabPage2.Controls.Add(this.m_deptDnm);
			this.tabPage2.Controls.Add(this.m_toDatDnm);
			this.tabPage2.Controls.Add(this.m_fromDatDnm);
			this.tabPage2.Font = new System.Drawing.Font("宋体", 10.5F);
			this.tabPage2.Location = new System.Drawing.Point(4, 23);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(842, 583);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "动态心电图报表";
			// 
			// rptDnmCardiogramView
			// 
			//this.rptDnmCardiogramView.ActiveViewIndex = -1;
			//this.rptDnmCardiogramView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			//	| System.Windows.Forms.AnchorStyles.Left) 
			//	| System.Windows.Forms.AnchorStyles.Right)));
			//this.rptDnmCardiogramView.DisplayGroupTree = false;
			//this.rptDnmCardiogramView.DisplayToolbar = false;
			//this.rptDnmCardiogramView.Location = new System.Drawing.Point(8, 40);
			//this.rptDnmCardiogramView.Name = "rptDnmCardiogramView";
			//this.rptDnmCardiogramView.ReportSource = null;
			//this.rptDnmCardiogramView.ShowCloseButton = false;
			//this.rptDnmCardiogramView.ShowGroupTreeButton = false;
			//this.rptDnmCardiogramView.Size = new System.Drawing.Size(834, 538);
			//this.rptDnmCardiogramView.TabIndex = 8;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(320, 13);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 16);
			this.label6.TabIndex = 7;
			this.label6.Text = "部门：";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(168, 13);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(16, 16);
			this.label5.TabIndex = 6;
			this.label5.Text = "到";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 13);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(16, 16);
			this.label4.TabIndex = 5;
			this.label4.Text = "从";
			// 
			// m_cmdDnmExit
			// 
			this.m_cmdDnmExit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_cmdDnmExit.DefaultScheme = true;
			this.m_cmdDnmExit.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_cmdDnmExit.Hint = "";
			this.m_cmdDnmExit.Location = new System.Drawing.Point(736, 8);
			this.m_cmdDnmExit.Name = "m_cmdDnmExit";
			this.m_cmdDnmExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_cmdDnmExit.Size = new System.Drawing.Size(80, 24);
			this.m_cmdDnmExit.TabIndex = 4;
			this.m_cmdDnmExit.Text = "退出";
			this.m_cmdDnmExit.Click += new System.EventHandler(this.m_cmdDnmExit_Click);
			// 
			// m_cmdDnmrpt
			// 
			this.m_cmdDnmrpt.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_cmdDnmrpt.DefaultScheme = true;
			this.m_cmdDnmrpt.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_cmdDnmrpt.Hint = "";
			this.m_cmdDnmrpt.Location = new System.Drawing.Point(632, 8);
			this.m_cmdDnmrpt.Name = "m_cmdDnmrpt";
			this.m_cmdDnmrpt.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_cmdDnmrpt.Size = new System.Drawing.Size(80, 24);
			this.m_cmdDnmrpt.TabIndex = 3;
			this.m_cmdDnmrpt.Text = "生成报表";
			this.m_cmdDnmrpt.Click += new System.EventHandler(this.m_cmdDnmrpt_Click);
			// 
			// m_deptDnm
			// 
			//this.m_deptDnm.EnableAutoValidation = true;
			//this.m_deptDnm.EnableEnterKeyValidate = true;
			//this.m_deptDnm.EnableEscapeKeyUndo = true;
			//this.m_deptDnm.EnableLastValidValue = true;
			//this.m_deptDnm.ErrorProvider = null;
			//this.m_deptDnm.ErrorProviderMessage = "Invalid value";
			this.m_deptDnm.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			//this.m_deptDnm.ForceFormatText = true;
			this.m_deptDnm.Location = new System.Drawing.Point(368, 8);
			this.m_deptDnm.m_StrDeptID = null;
			this.m_deptDnm.m_StrDeptName = null;
			this.m_deptDnm.Name = "m_deptDnm";
			this.m_deptDnm.Size = new System.Drawing.Size(104, 23);
			this.m_deptDnm.TabIndex = 2;
			this.m_deptDnm.Text = "";
			this.m_deptDnm.TextChanged += new System.EventHandler(this.ctlDeptTextBox2_TextChanged);
			// 
			// m_toDatDnm
			// 
			this.m_toDatDnm.Location = new System.Drawing.Point(192, 8);
			this.m_toDatDnm.Name = "m_toDatDnm";
			this.m_toDatDnm.Size = new System.Drawing.Size(120, 23);
			this.m_toDatDnm.TabIndex = 1;
			// 
			// m_fromDatDnm
			// 
			this.m_fromDatDnm.Location = new System.Drawing.Point(40, 8);
			this.m_fromDatDnm.Name = "m_fromDatDnm";
			this.m_fromDatDnm.Size = new System.Drawing.Size(120, 23);
			this.m_fromDatDnm.TabIndex = 0;
			// 
			// m_txtFind2
			// 
			this.m_txtFind2.Location = new System.Drawing.Point(528, 8);
			this.m_txtFind2.Name = "m_txtFind2";
			this.m_txtFind2.Size = new System.Drawing.Size(88, 23);
			this.m_txtFind2.TabIndex = 12;
			this.m_txtFind2.Text = "";
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label8.Location = new System.Drawing.Point(480, 8);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 16);
			this.label8.TabIndex = 11;
			this.label8.Text = "诊断：";
			// 
			// frmCardiogramReportrpt
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(842, 623);
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("宋体", 10.5F);
			this.Name = "frmCardiogramReportrpt";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "心电图报表管理";
			this.Load += new System.EventHandler(this.frmCardiogramReportrpt_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void ctlDeptTextBox2_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void m_cmdreport_Click(object sender, System.EventArgs e)
		{
			DataTable dtbCardiogramReport=new DataTable();
			string strfromDat=this.dtpfromDat.Value.ToString("yyyy-MM-dd 00:00:00");
			string strtoDat=this.dtptoDat.Value.AddDays(1).ToString("yyyy-MM-dd 00:00:00");
			objTable=new com.digitalwave.iCare.gui.RIS.clsController_RISRPT();
			string strDept;
			if(this.m_ctldept.Text!="")
			{
				strDept=this.m_ctldept.Text.ToString().Trim();
			}
			else
			{
				strDept="";
			}
			
			objTable.m_mthSetCardiogramReportdtb(strfromDat,strtoDat,strDept,m_txtFind.Text.Trim(),out dtbCardiogramReport);
			//rptCardiogram.SetDataSource(dtbCardiogramReport);
			//this.rptCardiogramView.ReportSource=rptCardiogram;
			//this.rptCardiogramView.RefreshReport();
		}

		private void m_cmdDnmrpt_Click(object sender, System.EventArgs e)
		{
			DataTable dtbDnmCardiogramReport=new DataTable();
			string strfromDat=this.m_fromDatDnm.Value.ToString("yyyy-MM-dd 00:00:00");
			string strtoDat=this.m_toDatDnm.Value.AddDays(1).ToString("yyyy-MM-dd 00:00:00");
			objTable=new com.digitalwave.iCare.gui.RIS.clsController_RISRPT();
			string strDept;
			if(this.m_ctldept.Text!="")
			{
				strDept=this.m_deptDnm.Text.ToString().Trim();
			}
			else
			{
				strDept="";
			}
			
			objTable.m_mthSetDnmCardiogramReportdtb(strfromDat,strtoDat,strDept,m_txtFind2.Text.Trim(),out dtbDnmCardiogramReport);
			//rptDnmCardiogram.SetDataSource(dtbDnmCardiogramReport);
			//this.rptDnmCardiogramView.ReportSource=rptDnmCardiogram;
			//this.rptDnmCardiogramView.RefreshReport();
		
		}

		private void m_cmdTP1exit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void m_cmdDnmExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmCardiogramReportrpt_Load(object sender, System.EventArgs e)
		{
			//this.rptCardiogramView.DisplayToolbar=true;
			//this.rptDnmCardiogramView.DisplayToolbar=true;
		}
	}
}
