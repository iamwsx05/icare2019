using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmCompoundReport ��ժҪ˵����
	/// </summary>
	public class frmCompoundReport :  com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		//private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmCompoundReport()
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
			//this.crystalReportViewer1.Size = new System.Drawing.Size(496, 381);
			//this.crystalReportViewer1.TabIndex = 0;
			//this.crystalReportViewer1.Load += new System.EventHandler(this.crystalReportViewer1_Load);
			// 
			// frmCompoundReport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(496, 381);
			//this.Controls.Add(this.crystalReportViewer1);
			this.Name = "frmCompoundReport";
			this.Text = "ҩƷ���ͳ�Ʊ���";
			this.Load += new System.EventHandler(this.frmCompoundReport_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void crystalReportViewer1_Load(object sender, System.EventArgs e)
		{
		
		}
		string strType="";
		private void frmCompoundReport_Load(object sender, System.EventArgs e)
		{
			if(strType=="")
				return;
			clsDomainControlCompoundReport domain=new clsDomainControlCompoundReport();
			DataTable dt=new DataTable();
			domain.m_lngGetStorageList(out dt,strType);
			//com.digitalwave.iCare.gui.HIS.baotable.CompoundReport Report=new com.digitalwave.iCare.gui.HIS.baotable.CompoundReport();
			//Report.SetDataSource(dt);
			//crystalReportViewer1.ReportSource=Report;
		}

		public void m_mthShowMe(string medType)
		{
			strType=medType;
			this.Show();
		}
	}
}
