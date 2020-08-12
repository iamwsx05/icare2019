using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
//using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
using weCare.Core.Entity;
 
namespace com.digitalwave.iCare.gui.HIS
{
	#region reated by weiling.huang  at 2005-10-10
	/// <summary>
	///reated by weiling.huang  at 2005-10-10
	/// </summary>
	public class clsPianDianNew: com.digitalwave.GUI_Base.clsController_Base	//GUI_Base.dll
	{
		#region 
		public clsPianDianNew()
		{
			m_objDomainManage = new clsDomainPianDianNew();
		}
		#endregion

		#region 
		/// <summary>
		/// DomainControl
		/// </summary>
		private clsDomainPianDianNew m_objDomainManage = null;

		/// <summary>
		/// 
		/// </summary>
		private frmPianDianNew m_objFrmViewer ;
		#endregion

		#region 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			// TODO:  
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			this.m_objFrmViewer = (frmPianDianNew)frmMDI_Child_Base_in;
		}
		#endregion

		#region
		/// <summary>
		///
		/// </summary>
		public void m_mthFrmInit()
		{
			//HIS.baotable.PdCrystalReport1 rpt = new HIS.baotable.PdCrystalReport1();
			//rpt.SetDataSource(this.m_objFrmViewer.m_dt);
			//((TextObject)rpt.ReportDefinition.ReportObjects["TextTitle"]).Text=this.m_objFrmViewer.m_title;
			//((TextObject)rpt.ReportDefinition.ReportObjects["TextHos"]).Text=this.m_objFrmViewer.m_hos;
			//((TextObject)rpt.ReportDefinition.ReportObjects["TextDate"]).Text=this.m_objFrmViewer.m_date;
   //         ((TextObject)rpt.ReportDefinition.ReportObjects["m_saleMoney"]).Text = this.m_objFrmViewer.m_SaleMoney;
   //         ((TextObject)rpt.ReportDefinition.ReportObjects["m_txtbuyMoney"]).Text = this.m_objFrmViewer.m_buyMoney;
			//rpt.Refresh();
			//this.m_objFrmViewer.crystalReportViewer1.ReportSource = rpt;
		}
		#endregion


		public void m_mthPrint()
		{
			//DataTable dt = this.m_objFrmViewer.m_dt.Copy();
			//dt = this.m_objDomainManage.m_mthGetDatap( dt);
			//HIS.baotable.PdCrystalReport1 rpt = new HIS.baotable.PdCrystalReport1();
			//rpt.SetDataSource(dt);
			//((TextObject)rpt.ReportDefinition.ReportObjects["TextTitle"]).Text=this.m_objFrmViewer.m_title;
			//((TextObject)rpt.ReportDefinition.ReportObjects["TextHos"]).Text=this.m_objFrmViewer.m_hos;
			//((TextObject)rpt.ReportDefinition.ReportObjects["TextDate"]).Text=this.m_objFrmViewer.m_date;
			//rpt.Refresh();
			//rpt.PrintToPrinter(1,true,0,0);
		}

	}
	#endregion
}
