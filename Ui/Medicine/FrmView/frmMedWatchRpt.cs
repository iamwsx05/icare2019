using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmMedWatchRpt 的摘要说明。
	/// </summary>
	public class frmMedWatchRpt : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.ComboBox m_cmbStorage;
		//internal CrystalDecisions.Windows.Forms.CrystalReportViewer m_crtMedWatch;
		internal PinkieControls.ButtonXP m_BtnStat;
		private System.Windows.Forms.Panel panel1;
		internal PinkieControls.ButtonXP buttonXP1;
		private System.Windows.Forms.Panel panel2;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMedWatchRpt()
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

		public override void CreateController()
		{
			this.objController = new clsControlMedWatch();
			this.objController.Set_GUI_Apperance(this);
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.m_BtnStat = new PinkieControls.ButtonXP();
			this.m_cmbStorage = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			//this.m_crtMedWatch = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonXP1 = new PinkieControls.ButtonXP();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_BtnStat
			// 
			this.m_BtnStat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.m_BtnStat.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_BtnStat.DefaultScheme = true;
			this.m_BtnStat.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_BtnStat.Hint = "";
			this.m_BtnStat.Location = new System.Drawing.Point(472, 6);
			this.m_BtnStat.Name = "m_BtnStat";
			this.m_BtnStat.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_BtnStat.Size = new System.Drawing.Size(128, 40);
			this.m_BtnStat.TabIndex = 143;
			this.m_BtnStat.Text = "统计(&S)";
			this.m_BtnStat.Click += new System.EventHandler(this.m_BtnStat_Click);
			// 
			// m_cmbStorage
			// 
			this.m_cmbStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cmbStorage.Location = new System.Drawing.Point(176, 17);
			this.m_cmbStorage.Name = "m_cmbStorage";
			this.m_cmbStorage.Size = new System.Drawing.Size(168, 22);
			this.m_cmbStorage.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(104, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "仓库：";
			// 
			// m_crtMedWatch
			// 
			//this.m_crtMedWatch.ActiveViewIndex = -1;
			//this.m_crtMedWatch.DisplayGroupTree = false;
			//this.m_crtMedWatch.Dock = System.Windows.Forms.DockStyle.Fill;
			//this.m_crtMedWatch.Location = new System.Drawing.Point(0, 0);
			//this.m_crtMedWatch.Name = "m_crtMedWatch";
			//this.m_crtMedWatch.ReportSource = null;
			//this.m_crtMedWatch.Size = new System.Drawing.Size(936, 456);
			//this.m_crtMedWatch.TabIndex = 0;
			//this.m_crtMedWatch.Load += new System.EventHandler(this.m_crtMedWatch_Load);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.buttonXP1);
			this.panel1.Controls.Add(this.m_BtnStat);
			this.panel1.Controls.Add(this.m_cmbStorage);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(944, 53);
			this.panel1.TabIndex = 1;
			// 
			// buttonXP1
			// 
			this.buttonXP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.buttonXP1.DefaultScheme = true;
			this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
			this.buttonXP1.Hint = "";
			this.buttonXP1.Location = new System.Drawing.Point(640, 6);
			this.buttonXP1.Name = "buttonXP1";
			this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.buttonXP1.Size = new System.Drawing.Size(128, 40);
			this.buttonXP1.TabIndex = 144;
			this.buttonXP1.Text = "退出(&E)";
			this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			//this.panel2.Controls.Add(this.m_crtMedWatch);
			this.panel2.Location = new System.Drawing.Point(8, 72);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(936, 456);
			this.panel2.TabIndex = 2;
			// 
			// frmMedWatchRpt
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(944, 525);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("宋体", 10.5F);
			this.Name = "frmMedWatchRpt";
			this.Text = "frmMedWatchRpt";
			this.Load += new System.EventHandler(this.frmMedWatchRpt_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void m_BtnStat_Click(object sender, System.EventArgs e)
		{
			((clsControlMedWatch)this.objController).m_mthWatchRpt();
		}

		private void frmMedWatchRpt_Load(object sender, System.EventArgs e)
		{
			((clsControlMedWatch)this.objController).m_mthInitForm();
			((clsControlMedWatch)this.objController).m_mthWatchRpt();
		}

		private void m_crtMedWatch_Load(object sender, System.EventArgs e)
		{
//		   ((clsControlMedWatch)this.objController).m_mthWatchRpt();
		}

		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
