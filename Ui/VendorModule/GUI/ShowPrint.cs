using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.VendorManage
{
	/// <summary>
	/// ShowPrint ��ժҪ˵����
	/// </summary>
	public class ShowPrint : System.Windows.Forms.Form
	{
		//public CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ShowPrint()
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
			//this.crystalReportViewer1.Size = new System.Drawing.Size(600, 373);
			//this.crystalReportViewer1.TabIndex = 0;
			// 
			// ShowPrint
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(600, 373);
			//this.Controls.Add(this.crystalReportViewer1);
			this.Name = "ShowPrint";
			this.Text = "ShowPrint";
			this.Load += new System.EventHandler(this.ShowPrint_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void ShowPrint_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
