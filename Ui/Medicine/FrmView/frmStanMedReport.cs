using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmReckoningReport ��ժҪ˵����
	/// </summary>
	public class frmStanMedReport: com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		//internal CrystalDecisions.Windows.Forms.CrystalReportViewer cryReportViewer;
		private System.Windows.Forms.Panel panel1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmStanMedReport()
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
			//this.cryReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cryReportViewer
			// 
			//this.cryReportViewer.ActiveViewIndex = -1;
			//this.cryReportViewer.DisplayGroupTree = false;
			//this.cryReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			//this.cryReportViewer.DockPadding.Bottom = 5;
			//this.cryReportViewer.Location = new System.Drawing.Point(0, 0);
			//this.cryReportViewer.Name = "cryReportViewer";
			//this.cryReportViewer.ReportSource = null;
			//this.cryReportViewer.Size = new System.Drawing.Size(846, 515);
			//this.cryReportViewer.TabIndex = 59;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			//this.panel1.Controls.Add(this.cryReportViewer);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(848, 517);
			this.panel1.TabIndex = 63;
			// 
			// frmStanMedReport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(848, 517);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.Name = "frmStanMedReport";
			this.Text = "�б�ҩƷͳ�Ʊ���";
			this.Load += new System.EventHandler(this.frmStanMedReport_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsControlStanMedReport();
			objController.Set_GUI_Apperance(this);
		}

		private void frmStanMedReport_Load(object sender, System.EventArgs e)
		{
			((clsControlStanMedReport)this.objController).m_mthFindByDateReport();
		}
	}
}
