using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// 打印浏览窗口
	/// </summary>
	public class frmStoragePreView : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		//private CrystalDecisions.Windows.Forms.CrystalReportViewer m_crvViewer;
		//private CrystalDecisions.CrystalReports.Engine.ReportDocument m_rpdReport;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 打印浏览窗口
		/// </summary>
		public frmStoragePreView()
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
			//this.m_crvViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			//this.m_rpdReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
			this.SuspendLayout();
			// 
			// m_crvViewer
			// 
			//this.m_crvViewer.ActiveViewIndex = -1;
			//this.m_crvViewer.DisplayGroupTree = false;
			//this.m_crvViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			//this.m_crvViewer.Location = new System.Drawing.Point(0, 0);
			//this.m_crvViewer.Name = "m_crvViewer";
			//this.m_crvViewer.ReportSource = null;
			//this.m_crvViewer.ShowCloseButton = false;
			//this.m_crvViewer.ShowGroupTreeButton = false;
			//this.m_crvViewer.ShowTextSearchButton = false;
			//this.m_crvViewer.Size = new System.Drawing.Size(672, 485);
			//this.m_crvViewer.TabIndex = 0;
			// 
			// m_rpdReport
			// 
			//this.m_rpdReport.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.DefaultPaperOrientation;
			//this.m_rpdReport.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;
			//this.m_rpdReport.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Upper;
			//this.m_rpdReport.PrintOptions.PrinterDuplex = CrystalDecisions.Shared.PrinterDuplex.Default;
			// 
			// frmStoragePreView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(672, 485);
			//this.Controls.Add(this.m_crvViewer);
			this.Name = "frmStoragePreView";
			this.Text = "单据浏览";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);

		}
		#endregion

		#region 属性
		#region 标题
		/// <summary>
		/// 窗口标题
		/// </summary>
		public string Title
		{
			get{return this.Text;}
			set{this.Text = value;}
		}
		#endregion
		#region 设置报表对象
		/// <summary>
		/// 设置报表对象
		/// </summary>
		//public CrystalDecisions.CrystalReports.Engine.ReportDocument Report
		//{
		//	get{return this.m_rpdReport;}
		//	set
		//	{
		//		this.m_rpdReport = value;
		//		this.m_crvViewer.ReportSource = this.m_rpdReport;
		//	}
		//}
		#endregion
		#endregion
	}
}
