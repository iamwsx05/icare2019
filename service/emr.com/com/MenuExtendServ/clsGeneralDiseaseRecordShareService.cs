using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.iCare.Public.MenuExtend.Service
{
	/// <summary>
	/// Summary description for clsGeneralDiseaseRecordShareService.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsGeneralDiseaseRecordShareService2 : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		[AutoComplete]
		public long m_lngGetFirstDiseaseInfoShareValue(string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			p_dtbResult = null;

//			string strSql = @"select * from(select RecordContent_Right
//FROM GeneralDiseaseRecord a
//inner join GeneralDiseaseRecordContent b
//on a.InPatientID = b.InPatientID
//and a.InPatientDate = b.InPatientDate
//and a.OpenDate = b.OpenDate
//WHERE (a.InPatientID = '"+p_strInPaitentID+@"') 
//and (a.InPatientDate = " +clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+@")
//and a.RecordTitle = '首次病程记录' 
//ORDER BY a.OpenDate DESC, b.ModifyDate DESC)where rownum = 1";		
			string strSql = clsDatabaseSQLConvert.s_StrTop1+@" MostlyContent_Right,OriginalDiagnose_Right,ThereunderDiagnose_Right,DiagnoseDiffe_Right,CurePlan_Right 
			FROM FirstIllnessNoteRecord  a
			inner join FirstIllnessNoteRecordContent  b
			on a.InPatientID = b.InPatientID
			and a.InPatientDate = b.InPatientDate
			and a.OpenDate = b.OpenDate
			WHERE (a.InPatientID = '"+p_strInPaitentID+@"') 
			and (a.InPatientDate = "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@")ORDER BY a.OpenDate DESC, b.ModifyDate DESC"+clsDatabaseSQLConvert.s_StrRownum;	

			return new clsHRPTableService().DoGetDataTable(strSql,ref p_dtbResult);
		}	
	}
}
