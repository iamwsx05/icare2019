using System.EnterpriseServices;
using System;
using System.Text ;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.CTEvaluation
{
	/// <summary>
	/// CT�����м��
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsCTEvaluation : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		[AutoComplete]
		public long m_lngGetTimeInfoOfAPatient(
			string p_strInPatientID,string p_strInPatientDate,string p_strFromDate,string p_strToDate,ref string  p_strXml,ref int  intRows)
		{
			//			long lngCheckRes = new com.digitalwave.PrivilegeSystemService.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsCTEvaluation","m_lngGetTimeInfoOfAPatient");
			//			if(lngCheckRes <= 0)
			//				//return lngCheckRes;

			string strCommand="";
			long lngRes = -1;

//			strCommand="SELECT DISTINCT ACTIVITYTIME FROM CTEVALUATION WHERE INPATIENTNO ='" +
//				p_strInPatientID +"'" +" AND Status =0 and InPatientDate=" + clsHRPTableService.s_strOracleDateTime(p_strInPatientDate) +" and ActivityTime>=" + clsHRPTableService.s_strOracleDateTime(p_strFromDate) + " and ActivityTime<=" + clsHRPTableService.s_strOracleDateTime(p_strToDate);
			strCommand="SELECT DISTINCT ACTIVITYTIME FROM CTEVALUATION WHERE INPATIENTNO ='" +
				p_strInPatientID +"'" +" AND Status =0 and InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) +" ";
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                lngRes = clsTablService.lngGetXMLTable(strCommand, ref p_strXml, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                clsTablService.Dispose();
            }
			return lngRes;
		}

		[AutoComplete]
		public long m_lngAddNew(string p_strMainXml)
		{
			//			long lngCheckRes = new com.digitalwave.PrivilegeSystemService.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsCTEvaluation","m_lngAddNew");
			//			if(lngCheckRes <= 0)
			//				//return lngCheckRes;

			if(p_strMainXml == null || p_strMainXml == "")
				return -1;

			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                int intIndex = p_strMainXml.IndexOf(' ');

                p_strMainXml = p_strMainXml.Insert(intIndex, " ModifyDate=\"" + strCurrentTime + "\"");

                lngRes = clsTablService.add_new_record("CTEvaluation", p_strMainXml);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                clsTablService.Dispose();
            }
 		   return lngRes;
		}

		[AutoComplete]
		public long m_lngGetCTValuation(
			string p_strPatientID,string p_strInPatientDate,string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			//			long lngCheckRes = new com.digitalwave.PrivilegeSystemService.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsCTEvaluation","m_lngGetCTValuation");
			//			if(lngCheckRes <= 0)
			//				//return lngCheckRes;

			string strCommand="";
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
			if(clsHRPTableService.bytDatabase_Selector == 0)
				strCommand="Select top 1 * from CTEvaluation a  "+
					"  WHERE a.InPatientNO='" +
					p_strPatientID +"'" +" AND a.Status =0 and a.ActivityTime="+
					clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCreateDate) + " and a.InPatientDate="+ clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) +" order by ModifyDate desc";
			else
				strCommand="select * from(Select * from CTEvaluation a  "+
					"  WHERE a.InPatientNO='" +
					p_strPatientID +"'" +" AND a.Status =0 and a.ActivityTime="+
					clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCreateDate) + " and a.InPatientDate="+ clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) +" order by ModifyDate desc)where rownum=1";

            try
            {
                lngRes = clsTablService.lngGetXMLTable(strCommand, ref p_strXml, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                clsTablService.Dispose();
            }
			return lngRes;

		}
		

	}
}
