using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.RIS
{
	class clsController_RISRPT : com.digitalwave.GUI_Base.clsController_Base
	{
		private com.digitalwave.iCare.gui.RIS.clsDomainController_RISEEGManage objTable;
		private com.digitalwave.iCare.gui.RIS.clsDomainController_RISCardiogramManage objTableCardiogram;
		public clsController_RISRPT()
		{
		}
		public void m_mthSetTCDReportdtb(string P_fromDat,string P_toDat,string P_dept,out DataTable dtbTCDrpt,string strFirstType,string strFirstValue,string strLastType,string strLastValue,bool flag)
		{
			objTable=new clsDomainController_RISEEGManage();
			objTable.m_mthGetTCDReportdtb(P_fromDat,P_toDat,P_dept,out dtbTCDrpt,strFirstType,strFirstValue,strLastType,strLastValue,flag);
		}
		public void m_mthSetEEGReportdtb(string P_fromDat,string P_toDat,string P_dept,out DataTable dtbTCDrpt,string strFirstType,string strFirstValue,string strLastType,string strLastValue,bool flag)
		{
			objTable=new clsDomainController_RISEEGManage();
			objTable.m_mthGetEEGReportdtb(P_fromDat,P_toDat,P_dept,out dtbTCDrpt,strFirstType,strFirstValue,strLastType,strLastValue,flag);
		}
		public void m_mthSetCardiogramReportdtb(string P_fromDat,string P_toDat,string P_dept,string strDiagnoses,out DataTable dtbCardiogramReport)
		{
			objTableCardiogram=new clsDomainController_RISCardiogramManage();
			objTableCardiogram.m_mthGetCardiogramReportdtb(P_fromDat,P_toDat,P_dept,strDiagnoses,out dtbCardiogramReport);
		}
		public void m_mthSetDnmCardiogramReportdtb(string P_fromDat,string P_toDat,string P_dept,string strDiagnoses,out DataTable dtbDnmCardiogramReport)
		{
			objTableCardiogram=new clsDomainController_RISCardiogramManage();
			objTableCardiogram.m_mthGetDnmCardiogramReportdtb(P_fromDat,P_toDat,P_dept,strDiagnoses,out dtbDnmCardiogramReport);
		}
	}
}