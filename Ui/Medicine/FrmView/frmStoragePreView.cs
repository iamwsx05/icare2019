using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// ��ӡ�������
	/// </summary>
	public class frmStoragePreView : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		//private CrystalDecisions.Windows.Forms.CrystalReportViewer m_crvViewer;
		//private CrystalDecisions.CrystalReports.Engine.ReportDocument m_rpdReport;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// ��ӡ�������
		/// </summary>
		public frmStoragePreView()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
			this.Text = "�������";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);

		}
		#endregion

		#region ����
		#region ����
		/// <summary>
		/// ���ڱ���
		/// </summary>
		public string Title
		{
			get{return this.Text;}
			set{this.Text = value;}
		}
		#endregion
		#region ���ñ������
		/// <summary>
		/// ���ñ������
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
