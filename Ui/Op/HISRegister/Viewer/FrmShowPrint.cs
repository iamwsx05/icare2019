using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// FrmShowPrint ��ժҪ˵����
	/// </summary>
	public class FrmShowPrint :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		//internal CrystalDecisions.Windows.Forms.CrystalReportViewer cryReportViewer;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmShowPrint()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}
		public void m_PrintRegister(System.Data.DataTable dtSource)
		{
			((clsControlPrint)this.objController).m_ShowRegister1(dtSource);
		}

		public void m_PrintRegister1(System.Data.DataTable dtSource)
		{
			((clsControlPrint)this.objController).m_PrintRegister1(dtSource);
		}
		public void m_ShowRegister11(System.Data.DataTable dtSource)
		{
			((clsControlPrint)this.objController).m_ShowRegister11(dtSource);
		}

		public void m_PrintRegister11(System.Data.DataTable dtSource)
		{
			((clsControlPrint)this.objController).m_PrintRegister11(dtSource);
		}
		public void m_CheckOutReg()
		{
			//((clsControlPrint)this.objController).m_CheckOutReg();
		}

		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsControlPrint();
			objController.Set_GUI_Apperance(this);
		}

		#region �������
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShowPrint));
            //this.cryReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // cryReportViewer
            // 
            //this.cryReportViewer.ActiveViewIndex = -1;
            //this.cryReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.cryReportViewer.DisplayGroupTree = false;
            //this.cryReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.cryReportViewer.Location = new System.Drawing.Point(0, 0);
            //this.cryReportViewer.Name = "cryReportViewer";
            //this.cryReportViewer.SelectionFormula = "";
            //this.cryReportViewer.Size = new System.Drawing.Size(480, 413);
            //this.cryReportViewer.TabIndex = 0;
            //this.cryReportViewer.ViewTimeSelectionFormula = "";
            // 
            // FrmShowPrint
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(480, 413);
            //this.Controls.Add(this.cryReportViewer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmShowPrint";
            this.Text = "��ӡԤ��";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

		}
		#endregion

		private void cryReportViewer_Load(object sender, System.EventArgs e)
		{
		
		}
		#endregion
	}
}
