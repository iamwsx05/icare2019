using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
 
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlReckoningReport ��ժҪ˵����
	/// </summary>
	public class clsControlDepWorkReport:com.digitalwave.GUI_Base.clsController_Base
	{
		//private CrystalDecisions.CrystalReports.Engine.ReportDocument m_rptRpt;
		clsDcl_ReckoningReport clsDomain;
		Double[]  Doe_ = new Double[18];
		string str_firstDate,str_lasttDate;
		public clsControlDepWorkReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			//m_rptRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
			clsDomain = new clsDcl_ReckoningReport();
			
		}
		
		#region ���ô������	�Ź���	 2004-9-9
		com.digitalwave.iCare.gui.HIS.frmDepWorkReport m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmDepWorkReport)frmMDI_Child_Base_in;

			
		}
		#endregion

		#region ��������  �Ź���	 2004-9-14
		public  void m_mthFindByDateReport()
		{
			for(int i=0;i<17;i++)
			{
				Doe_[i] =0;
			}

			str_firstDate=m_objViewer.m_daFinDate.Value.ToShortDateString();
			str_lasttDate=m_objViewer.m_daFinDateLast.Value.ToShortDateString();
			

			//m_rptRpt.Load("Report\\rptDepWork.rpt");
			DataTable m_dtRpt = new DataTable();
			DataTable m_dtRptDitial = new DataTable();
			long lngRes;
			lngRes =clsDomain.m_lngChargeMnothReport(str_firstDate,str_lasttDate,out m_dtRpt);
			if(lngRes>=1)
			{
				//m_rptRpt.SetDataSource(m_dtRpt);
				//m_rptRpt.Refresh();
				//m_objViewer.cryReportViewer.ReportSource=m_rptRpt;
			}
			
			
		}
		#endregion
	}
}
