using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmCharegeItemReport ��ժҪ˵����
	/// </summary>
	public class frmCharegeItemReport :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		//private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmCharegeItemReport()
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
			//this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.SuspendLayout();
			// 
			// crystalReportViewer1
			// 
			//this.crystalReportViewer1.ActiveViewIndex = -1;
			//this.crystalReportViewer1.DisplayGroupTree = false;
			//this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			//this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
			//this.crystalReportViewer1.Name = "crystalReportViewer1";
			//this.crystalReportViewer1.ReportSource = null;
			//this.crystalReportViewer1.ShowCloseButton = false;
			//this.crystalReportViewer1.ShowGroupTreeButton = false;
			//this.crystalReportViewer1.Size = new System.Drawing.Size(536, 417);
			//this.crystalReportViewer1.TabIndex = 0;
			// 
			// frmCharegeItemReport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(536, 417);
			//this.Controls.Add(this.crystalReportViewer1);
			this.Name = "frmCharegeItemReport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "�շ���Ŀ��ϸ����";
			this.ResumeLayout(false);

		}
		#endregion
		public void m_mthShowReport(DataTable dt)
		{
			//com.digitalwave.iCare.gui.HIS.Print.CharegeItemReport objCry =new com.digitalwave.iCare.gui.HIS.Print.CharegeItemReport();
			//objCry.SetDataSource(dt);
			//this.crystalReportViewer1.ReportSource=objCry;
			//this.crystalReportViewer1.Refresh();
	
		}
	}
}
