using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmChangePriceRpt 的摘要说明。
	/// </summary>
	public class frmChangePriceRpt : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		//internal CrystalDecisions.Windows.Forms.CrystalReportViewer m_CryView;
		internal PinkieControls.ButtonXP btnesc;
		internal PinkieControls.ButtonXP m_BtnStat;
		internal System.Windows.Forms.DateTimePicker m_dtpEndDate;
		internal System.Windows.Forms.DateTimePicker m_dtpStartDate;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmChangePriceRpt()
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
			//this.m_CryView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.m_BtnStat = new PinkieControls.ButtonXP();
			this.m_dtpEndDate = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.m_dtpStartDate = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.btnesc = new PinkieControls.ButtonXP();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_CryView
			// 
			//this.m_CryView.ActiveViewIndex = -1;
			//this.m_CryView.DisplayGroupTree = false;
			//this.m_CryView.Dock = System.Windows.Forms.DockStyle.Fill;
			//this.m_CryView.Location = new System.Drawing.Point(0, 0);
			//this.m_CryView.Name = "m_CryView";
			//this.m_CryView.ReportSource = null;
			//this.m_CryView.Size = new System.Drawing.Size(992, 512);
			//this.m_CryView.TabIndex = 0;
			// 
			// m_BtnStat
			// 
			this.m_BtnStat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.m_BtnStat.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_BtnStat.DefaultScheme = true;
			this.m_BtnStat.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_BtnStat.Hint = "";
			this.m_BtnStat.Location = new System.Drawing.Point(464, 6);
			this.m_BtnStat.Name = "m_BtnStat";
			this.m_BtnStat.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_BtnStat.Size = new System.Drawing.Size(88, 32);
			this.m_BtnStat.TabIndex = 55;
			this.m_BtnStat.TabStop = false;
			this.m_BtnStat.Text = "统计(&E)";
			this.m_BtnStat.Click += new System.EventHandler(this.m_BtnStat_Click);
			// 
			// m_dtpEndDate
			// 
			this.m_dtpEndDate.Location = new System.Drawing.Point(272, 13);
			this.m_dtpEndDate.Name = "m_dtpEndDate";
			this.m_dtpEndDate.Size = new System.Drawing.Size(120, 23);
			this.m_dtpEndDate.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(232, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "至";
			// 
			// m_dtpStartDate
			// 
			this.m_dtpStartDate.Location = new System.Drawing.Point(96, 13);
			this.m_dtpStartDate.Name = "m_dtpStartDate";
			this.m_dtpStartDate.Size = new System.Drawing.Size(120, 23);
			this.m_dtpStartDate.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "统计日期：";
			// 
			// btnesc
			// 
			this.btnesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnesc.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btnesc.DefaultScheme = true;
			this.btnesc.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnesc.Hint = "";
			this.btnesc.Location = new System.Drawing.Point(632, 6);
			this.btnesc.Name = "btnesc";
			this.btnesc.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btnesc.Size = new System.Drawing.Size(88, 32);
			this.btnesc.TabIndex = 54;
			this.btnesc.TabStop = false;
			this.btnesc.Text = "退出(&E)";
			this.btnesc.Click += new System.EventHandler(this.btnesc_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.m_dtpEndDate);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.m_dtpStartDate);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.btnesc);
			this.panel1.Controls.Add(this.m_BtnStat);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(992, 48);
			this.panel1.TabIndex = 2;
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			//this.panel2.Controls.Add(this.m_CryView);
			this.panel2.Location = new System.Drawing.Point(0, 64);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(992, 512);
			this.panel2.TabIndex = 3;
			// 
			// frmChangePriceRpt
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(992, 573);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.Name = "frmChangePriceRpt";
			this.Text = "调价报告单";
			this.Load += new System.EventHandler(this.frmChangePriceRpt_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region 设置窗体
		public override void CreateController()
		{
			this.objController = new clsControlChengePriceRpt();
			this.objController.Set_GUI_Apperance(this);
		}
		#endregion

		private void m_BtnStat_Click(object sender, System.EventArgs e)
		{
			((clsControlChengePriceRpt)this.objController).ChangePriceStat();
		}

		private void btnesc_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmChangePriceRpt_Load(object sender, System.EventArgs e)
		{
			((clsControlChengePriceRpt)this.objController).ChangePriceStat();
		}
	}
}
