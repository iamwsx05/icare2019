using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
 
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlReckoningReport ��ժҪ˵����
	/// </summary>
	public class clsControlStanMedReport:com.digitalwave.GUI_Base.clsController_Base
	{
		//private CrystalDecisions.CrystalReports.Engine.ReportDocument m_rptRpt;
		clsDomainControlReport clsDomain;
		public clsControlStanMedReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			//m_rptRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
			clsDomain = new clsDomainControlReport();
			
		}
		
		#region ���ô������	�Ź���	 2004-9-9
		com.digitalwave.iCare.gui.HIS.frmStanMedReport m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmStanMedReport)frmMDI_Child_Base_in;

			
		}
		#endregion

		#region ��������  �Ź���	 2004-9-14
		public  void m_mthFindByDateReport()
		{
			//m_rptRpt.Load("Report\\rptStanMedrpt.rpt");
			//DataTable m_dtRpt = new DataTable();
			//long lngRes;
			//lngRes =clsDomain.m_lngGetStanMed(out m_dtRpt);
			//if(lngRes>=1)
			//{
			//	((TextObject)m_rptRpt.ReportDefinition.ReportObjects["txtReportTitle"]).Text =this.m_objComInfo.m_strGetHospitalTitle()+"�б�ҩƷͳ�Ʊ���";
			//	m_rptRpt.SetDataSource(m_dtRpt);
			//	m_rptRpt.Refresh();
			//	m_objViewer.cryReportViewer.ReportSource=m_rptRpt;
			//}
			
			
		}
		#endregion
	}
}
