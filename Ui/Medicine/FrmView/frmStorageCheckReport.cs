using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmStorageCheckReport ��ժҪ˵����
	/// </summary>
	public class frmStorageCheckReport : com.digitalwave.GUI_Base.frmMDI_Child_Base	//GUI_Base.dll
	{
		//internal CrystalDecisions.Windows.Forms.CrystalReportViewer ReportCheck;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmStorageCheckReport()
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
			this.Text = "��ӡ�̵�����";
			this.Load += new System.EventHandler(this.frmStorageCheckReport_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmStorageCheckReport_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
