using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmStorageCheckReport 的摘要说明。
	/// </summary>
	public class frmStorageCheckReport : com.digitalwave.GUI_Base.frmMDI_Child_Base	//GUI_Base.dll
	{
		//internal CrystalDecisions.Windows.Forms.CrystalReportViewer ReportCheck;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmStorageCheckReport()
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
			//this.ReportCheck = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.SuspendLayout();
			// 
			// ReportCheck
			// 
			//this.ReportCheck.ActiveViewIndex = -1;
			//this.ReportCheck.Location = new System.Drawing.Point(-54, 8);
			//this.ReportCheck.Name = "ReportCheck";
			//this.ReportCheck.ReportSource = null;
			//this.ReportCheck.Size = new System.Drawing.Size(950, 496);
			//this.ReportCheck.TabIndex = 4;
			// 
			// frmStorageCheckReport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(904, 493);
			//this.Controls.Add(this.ReportCheck);
			this.Name = "frmStorageCheckReport";
			this.Text = "打印盘点数据";
			this.Load += new System.EventHandler(this.frmStorageCheckReport_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmStorageCheckReport_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
