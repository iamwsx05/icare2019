using System;
using System.Data;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
using weCare.Core.Entity;
using System.Windows.Forms; 

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlChengePriceRpt 的摘要说明。
	/// </summary>
	public class clsControlChengePriceRpt:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlChengePriceRpt()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 设置窗体对象
		/// <summary>
		/// 窗体对象
		/// </summary>
		frmChangePriceRpt m_objViewer;
		 clsDomainConrolChangPrice objSVC=new clsDomainConrolChangPrice();
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			m_objViewer = (frmChangePriceRpt)frmMDI_Child_Base_in;
		}
		#endregion

		#region 统计调价报告单
		public void ChangePriceStat()
		{
			//System.Data.DataTable dt = null;
			//objSVC.m_lngGetChangePriceRpt(null,out dt);
			//com.digitalwave.iCare.gui.HIS.baotable.ChangePriceRptRpt rpt = new com.digitalwave.iCare.gui.HIS.baotable.ChangePriceRptRpt();
			//((TextObject)rpt.ReportDefinition.ReportObjects["Text3"]).Text = this.m_objViewer.m_dtpStartDate.Value.ToString("yyyy-MM-dd");
			////((TextObject)detail.ReportDefinition.ReportObjects["TextPeriod"]).Text = this.m_objViewer.m_txtCreator.Text;		
			//((TextObject)rpt.ReportDefinition.ReportObjects["Text5"]).Text = this.m_objViewer.m_dtpEndDate.Value.ToString("yyyy-MM-dd");
			//rpt.SetDataSource(dt);			
			//rpt.Refresh();
			//this.m_objViewer.m_CryView.ReportSource = rpt;
			
		}
		#endregion 统计调价报告单
	}
}
