using System.EnterpriseServices;
using System;
using System.Text ;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.SIRSEvaluation
{
	/// <summary>
	/// 
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsSIRSEvaluation : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		[AutoComplete]
		public long add_new_record( string strSIRSEvaluationXML,ref string strCurrentTime)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (strSIRSEvaluationXML == null || strSIRSEvaluationXML == "")
                    return -1;

                strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                StringBuilder tem = new StringBuilder(strSIRSEvaluationXML);
                int index = strSIRSEvaluationXML.IndexOf(" ");
                tem.Insert(index, " ActivityTime=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(strCurrentTime));
                strSIRSEvaluationXML = tem.ToString();
                lngRes = clsTablService.add_new_record("SIRSEvaluation", strSIRSEvaluationXML);
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
		public long deactive_record( string PatientID,string ActivityTime,string DeEmployeeID)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (PatientID == null || PatientID == "" || DeEmployeeID == null || DeEmployeeID == "" || ActivityTime == null || ActivityTime == "")
                    return -1;

                string strCommand = "update SIRSEvaluation set status=1,DEACTIVEDOPERATORID='" + DeEmployeeID + "' ,DeactivatedDate={fn CURDATE()} where INPATIENTNO='" + PatientID + "' and ActivityTime=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(ActivityTime);

                lngRes = clsTablService.DoExcute(strCommand);
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
		//alex 2002-9-24
		public long lngDeleteRecord( string strPatientID,string strDeleteTime,string strOperatorID)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (strPatientID == "" || strDeleteTime == "" || strOperatorID == "")
                    return -1;
                string strCurrentTime = DateTime.Now.ToString("yyyy-M-d HH:mm:ss");
                string strCommand = "Update SIRSEvaluation Set Status='1',DeactivatedDate =" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(strCurrentTime) + "," +
                    "DEACTIVEDOPERATORID='" + strOperatorID + "' WHERE INPATIENTNO ='" + strPatientID + "' " +
                    "AND ActivityTime = " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(strDeleteTime);
                lngRes = clsTablService.DoExcute(strCommand);
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
		public long modify_record( string strSIRSEvaluationXML)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService=new clsHRPTableService ();
            try
            {  
                if (strSIRSEvaluationXML == null || strSIRSEvaluationXML == "")
                    return -1;
                lngRes = new clsHRPTableService().modify_record("SIRSEvaluation", strSIRSEvaluationXML, "INPATIENTNO", "ACTIVITYTIME");
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
		public long lngAddNewRecordOfAutoEval( string strSIRSEvaluationXML)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (strSIRSEvaluationXML == null || strSIRSEvaluationXML == "")
                    return -1;
                lngRes = clsTablService.add_new_record("SIRSEvaluation", strSIRSEvaluationXML);
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
		public bool lngRecordExist( string PatientID,string ActivityTime)
		{
			bool blnRes = false;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                blnRes = clsTablService.bolRecordExist("SIRSEvaluation", PatientID, ActivityTime, "ACTIVITYTIME");
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes1 = objLogger.LogError(objEx);
            }
            finally
            {
                clsTablService.Dispose();
            }
			return blnRes;
		}
		[AutoComplete]
		public long lngGetXMLTable( string PatientID,ref string strXMLTable,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();

            try
            { 
                //			string strWhere = " where ";
                //			if(PatientID != "")
                //				strWhere += "PatientID='"+PatientID+"' ";
                //			if(ActivityTime != "")
                //				strWhere += "and ActivityTime='"+ActivityTime+"' ";
                //			if(strWhere == " where ")
                //				return 0;
                //			string strCommand = "select * from SIRSEvaluation "+strWhere;
                string strCommand = "";
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strCommand = "select top 1 * from SIRSEvaluation where INPATIENTNO='" + PatientID + "' ORDER BY ActivityTime DESC";
                else
                    strCommand = "select * from(select * from SIRSEvaluation where PatientID='" + PatientID + "' ORDER BY ActivityTime DESC)where rownum = 1";
                lngRes = clsTablService.lngGetXMLTable(strCommand, ref strXMLTable, ref rows);
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
		public long lngGetXMLTable( string strPatientID,string strBeginTime,string strEndTime,ref string strXMLTable,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                //			string strCommand="SELECT * FROM SIRSEvaluation WHERE PatientID='"+strPatientID+"' "+
                //				"AND ActivityTime BETWEEN '"+strBeginTime+"' AND '"+strEndTime+"' "+
                //				"ORDER BY ActivityTime DESC";
                string strCommand = "SELECT EBI.FirstName AS EvalDoctorName,SIRS.* FROM SIRSEvaluation SIRS, " +
                    "EmployeeBaseInfo EBI " +
                    "WHERE SIRS.INPATIENTNO='" + strPatientID + "' AND SIRS.ActivityTime " +
                    "BETWEEN " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(strBeginTime) + " AND " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(strEndTime) +
                    " AND EBI.Status =0 AND SIRS.Status=0" +
                    "AND EBI.EmployeeID = SIRS.EvalDoctorID " +
                    "ORDER BY ActivityTime";

                lngRes = clsTablService.lngGetXMLTable(strCommand, ref strXMLTable, ref rows);
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
		public long lngXMLLikeQuery( string pid,ref string strXMLSet,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                //			string strCommand ="SELECT PatientID, ActivityTime FROM SIRSEvaluation WHERE (PatientID in(SELECT InHospitalNO FROM PatientInHospitalInfo WHERE Status=0and OutOfHospital is null  and  InHospitalNO like'"+ pid+"%'))";
                //			string strCommand = "Select PatientID,ActivityTime from SIRSEvaluation where  Status=0and PatientID like'"+ pid+"%' " ;//been 2002-8-24
                string strCommand = "select INPATIENTID from PatientBaseInfo where Status=0and INPATIENTID like'" + pid + "%' ";//been 2002-8-24
                lngRes = clsTablService.lngGetXMLLikeQuery(strCommand, ref strXMLSet, ref rows);
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
		public long lngGetDepartmentXMLTable( string strPatientID,ref string strXMLTable,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (strPatientID == "" || strPatientID == null)
                    return 0;
                string strCommand = "select * from DepartmentSickRoom  where InHospitalNO=" + strPatientID + " and  OutHospitalDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()) + @"";
                lngRes = clsTablService.lngGetXMLTable(strCommand, ref strXMLTable, ref rows);
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
		public long lngGetXMLBaseInfo( string strInHospitalNO,ref string strXMLTable,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (strInHospitalNO == null || strInHospitalNO == "")
                    return -1;
                string strCommand = "select Sex,Birth from patientbaseinfo where PatientID = (select PatientID from PatientInHospitalInfo where InHospitalNO='" + strInHospitalNO + "')";
                lngRes = clsTablService.lngGetXMLTable(strCommand, ref strXMLTable, ref rows);
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


		#region New
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strFromDate"></param>
		/// <param name="p_strToDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetTimeInfoOfAPatient( string p_strInPatientID,string p_strInPatientDate,string p_strFromDate,string p_strToDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                string strCommand = "";

                //			strCommand="SELECT distinct ActivityTime FROM SIRSEvaluation WHERE InPatientNO ='" +
                //				p_strInPatientID +"'" +" AND Status =0 and InPatientDate="+clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+" and ActivityTime>=" + clsHRPTableService.s_strOracleDateTime(p_strFromDate) + " and ActivityTime<=" + clsHRPTableService.s_strOracleDateTime(p_strToDate );
                strCommand = "SELECT distinct ActivityTime FROM SIRSEvaluation WHERE InPatientNO ='" +
                    p_strInPatientID + "'" + " AND Status =0 and InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + "";

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
		public long m_lngAddNew( string p_strMainXml)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (p_strMainXml == null || p_strMainXml == "")
                    return -1;

                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                int intIndex = p_strMainXml.IndexOf(' ');

                p_strMainXml = p_strMainXml.Insert(intIndex, " ModifyDate=\"" + strCurrentTime + "\"");

                lngRes = clsTablService.add_new_record("SIRSEvaluation", p_strMainXml);
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
		public long m_lngGetSIRSValuation( string p_strPatientID,string p_strInPatientDate,string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                string strCommand = "";

                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strCommand = "select top 1 *  from SIRSEvaluation a  " +
                        "  WHERE a.InPatientNO='" +
                        p_strPatientID + "'" + " AND a.Status =0 and a.ActivityTime=" +
                        clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCreateDate) + " and a.InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + " order by ModifyDate desc";
                else
                    strCommand = "select * from(select *  from SIRSEvaluation a  " +
                        "  WHERE a.InPatientNO='" +
                        p_strPatientID + "'" + " AND a.Status =0 and a.ActivityTime=" +
                        clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCreateDate) + " and a.InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + " order by ModifyDate desc)where rownum = 1";

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
		#endregion
		
	}
}
