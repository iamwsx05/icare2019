using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
	#region 盘点窗体�?：created by weiling.huang  at 2005-10-10
	/// <summary>
	///盘点窗体�?：created by weiling.huang  at 2005-10-10
	/// </summary>
	public class frmPianDianNew: com.digitalwave.GUI_Base.frmMDI_Child_Base	//GUI_Base.dll
	{
		//public CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		public DataTable m_dt ;
		public string m_date="";
		public string m_title="";
		public string m_hos="";
        public string m_buyMoney = "";
        public string m_SaleMoney = "";

		/// <summary>
		/// 必需的设计器变量�?
		/// </summary>
		private System.ComponentModel.Container components = null;


		#region 构造函�?
		public frmPianDianNew()
		{
			InitializeComponent();
		}
		#endregion

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
		/// 设计器支持所需的方�?- 不要使用代码编辑器修�?
		/// 此方法的内容
		/// </summary>
		private void InitializeComponent()
		{
            //this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            //this.crystalReportViewer1.ActiveViewIndex = -1;
            //this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.crystalReportViewer1.DisplayGroupTree = false;
            //this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            //this.crystalReportViewer1.Name = "crystalReportViewer1";
            //this.crystalReportViewer1.SelectionFormula = "";
            //this.crystalReportViewer1.Size = new System.Drawing.Size(752, 517);
            //this.crystalReportViewer1.TabIndex = 0;
            //this.crystalReportViewer1.ViewTimeSelectionFormula = "";
            //this.crystalReportViewer1.Load += new System.EventHandler(this.crystalReportViewer1_Load);
            // 
            // frmPianDianNew
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(752, 517);
            //this.Controls.Add(this.crystalReportViewer1);
            this.Name = "frmPianDianNew";
            this.Text = "�̵��ӡ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPianDianNew_Load);
            this.ResumeLayout(false);

		}
		#endregion

		#region 重载CreateController
		/// <summary>
		/// 设置窗体对象
		/// </summary>
		public override void CreateController()
		{
			this.objController = new clsPianDianNew();
			this.objController.Set_GUI_Apperance(this);
		}
		#endregion

		#region 窗体Load事件 created by weiling.huang at 2005-10-10
		/// <summary>
		/// 窗体Load事件 created by weiling.huang at 2005-10-10
		/// </summary>
		private void frmPianDianNew_Load(object sender, System.EventArgs e)
		{
			//方法:窗体的初始化 ：created by weiling.huang  at 2005-10-10
			((clsPianDianNew)objController).m_mthFrmInit();
		}
		#endregion

		public void print()
		{
			((clsPianDianNew)objController).m_mthPrint();
			
		}

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

	}
	#endregion
}
