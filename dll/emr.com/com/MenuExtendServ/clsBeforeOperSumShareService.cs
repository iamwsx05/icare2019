using System;
//using Microsoft.Data.Odbc;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.iCare.Public.MenuExtend.Service
{
	/// <summary>
	/// 术前小结共享数据
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsBeforeOperSumShareService2 : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		[AutoComplete]
		public long m_lngGetShareValue(string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			p_dtbResult = null;

			string strSql = clsDatabaseSQLConvert.s_StrTop1+@" *
FROM BeforeOperationSummaryContent
WHERE (rtrim(InPatientID) = '"+p_strInPaitentID+@"') AND (InPatientDate = " +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@")
ORDER BY OpenDate DESC, ModifyDate DESC"+clsDatabaseSQLConvert.s_StrRownum;

//			p_dtbResult = new DataTable();

			return new clsHRPTableService().DoGetDataTable(strSql,ref p_dtbResult);
		}	
	}
}
