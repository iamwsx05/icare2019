using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.iCare.Public.MenuExtend.Service
{
	/// <summary>
	/// 手术记录单数据共享
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsOperationRecordDoctorShareService2 : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 获取手术数据，只为住院病案首页提供共享
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetBaseOperationValue(string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = @"select b.OperationBeginDate,b.OperationName,b.AnaesthesiaCategoryDosage,b.Anaesther,
								b.OperationDoctorName,b.FirstAssistantName,b.SecondAssistantName,b.OperationDoctorID,b.FirstAssistantID,b.SecondAssistantID
								from OperationRecordDoctor a,
								OperationRecordContenDoctor b,
								(select Max(LastModifyDate) as LastModifyDate,InPatientID,InPatientDate,OpenDate
								from OperationRecordContenDoctor
								group by InPatientID,InPatientDate,OpenDate
								)Base
								where a.InPatientID='"+p_strInPaitentID+@"' 
								and a.InPatientDate="+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@" 
								and a.Status=0 
								and a.InPatientID=b.InPatientID
								and a.InPatientDate=b.InPatientDate
								and a.OpenDate = b.OpenDate
								and Base.InPatientID=b.InPatientID
								and Base.InPatientDate=b.InPatientDate
								and Base.OpenDate = b.OpenDate
								and Base.LastModifyDate = b.LastModifyDate
								order by a.CreateDate
								";
			#endregion SQL

			p_dtbResult = new DataTable();

			return new clsHRPTableService().DoGetDataTable (strSQL, ref p_dtbResult);
		
		}

		
		/// <summary>
		/// 获取最近的手术数据，只为手术后病程记录提供共享
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strRecordDate">记录日期，获取次日期之前最近的信息</param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLatestOperationInfo(string p_strInPaitentID,string p_strInPatientDate,string p_strRecordDate, out DataTable p_dtbResult)
		{
			#region SQL
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@" b.OperationBeginDate,b.AnaesthesiaCategoryDosage,b.OperationName,b.DiagnoseAfterOperation,b.OperationProcess
from OperationRecordDoctor a,OperationRecordContenDoctor b
where a.InPatientID = '"+p_strInPaitentID+@"'
and a.InPatientDate = " +clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
and a.Status=0 
and a.InPatientID=b.InPatientID
and a.InPatientDate=b.InPatientDate
and a.OpenDate = b.OpenDate
and b.OperationBeginDate <= "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strRecordDate)+@"
order by a.CreateDate desc,b.LastModifyDate desc"+clsDatabaseSQLConvert.s_StrRownum;
			#endregion SQL

			p_dtbResult = new DataTable();

			return new clsHRPTableService().DoGetDataTable (strSQL, ref p_dtbResult);
		
		}
	}
}
