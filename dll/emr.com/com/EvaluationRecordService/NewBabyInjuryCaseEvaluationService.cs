using System.EnterpriseServices;
using System;
using System.Text ;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.NewBabyInjuryCaseEvaluationService
{
	/// <summary>
	/// Summary description for NewBabyInjuryCaseEvaluationService.
	/// alex 2002-9-25
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class NewBabyInjuryCaseEvaluationService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		[AutoComplete]
		public long add_new_record(string strBabyInjuryCaseEvaluation)
		{
			long lngRes = -1;
            clsHRPTableService hs=new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "NewBabyInjuryCaseEvaluationService", "add_new_record");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (strBabyInjuryCaseEvaluation == null || strBabyInjuryCaseEvaluation == "")
                    return -1;



                StringBuilder tem = new StringBuilder(strBabyInjuryCaseEvaluation);
                int index = strBabyInjuryCaseEvaluation.IndexOf(" ");
                tem.Insert(index, " Status=0");
                strBabyInjuryCaseEvaluation = tem.ToString();

                lngRes = hs.add_new_record("BabyInjuryCaseEvaluation", strBabyInjuryCaseEvaluation);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                hs.Dispose();
            }
			return lngRes;
		}

		[AutoComplete]
		public long add_new_record(string strLungInjuryEvaluationXML,ref string strCurrentTime)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "NewBabyInjuryCaseEvaluationService", "add_new_record");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (strLungInjuryEvaluationXML == null || strLungInjuryEvaluationXML == "")
                    return -1;

                strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                StringBuilder tem = new StringBuilder(strLungInjuryEvaluationXML);
                int index = strLungInjuryEvaluationXML.IndexOf(" ");
                tem.Insert(index, " ActivityTime=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(strCurrentTime));
                strLungInjuryEvaluationXML = tem.ToString();
                lngRes = clsTablService.add_new_record("BabyInjuryCaseEvaluation", strLungInjuryEvaluationXML);
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
		public long lngGetXMLTable(
			string strPatientID,string strBeginTime,string strEndTime,ref string strXMLTable,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "NewBabyInjuryCaseEvaluationService", "lngGetXMLTable");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "SELECT EBI.FirstName AS EvalDoctorName,SIRS.* FROM BabyInjuryCaseEvaluation SIRS, " +
                    "EmployeeBaseInfo  EBI " +
                    "WHERE SIRS.INPATIENTNO='" + strPatientID + "' AND SIRS.ActivityTime " +
                    "BETWEEN " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(strBeginTime) + " AND " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(strEndTime) +
                    " AND EBI.Status =0 AND SIRS.Status=0" +
                    "AND EBI.EmployeeID = SIRS.EvalDoctorID " +
                    "AND SIRS.IsNewBaby = '1' " +
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
		public long deactive_record(
			string PatientID,string ActivityTime,string IsNewBaby,string DeEmployeeID)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "NewBabyInjuryCaseEvaluationService", "deactive_record");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (PatientID == null || PatientID == "" || DeEmployeeID == null || DeEmployeeID == "" || ActivityTime == null || ActivityTime == "" || IsNewBaby == null || IsNewBaby == "")
                    return -1;

                string strCommand = "update BabyInjuryCaseEvaluation set status=1,DEACTIVEDOPERATORID='" + DeEmployeeID + "' ,DeactivatedDate={fn CURDATE()} where INPATIENTNO='" + PatientID + "' and ActivityTime=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(ActivityTime) + " and IsNewBaby='" + IsNewBaby + "'";

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
		public long lngDeleteRecord(
			string strPatientID,string strDeleteTime,string strOperatorID)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "NewBabyInjuryCaseEvaluationService", "lngDeleteRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (strPatientID == "" || strDeleteTime == "" || strOperatorID == "")
                    return -1;
                string strCurrentTime = DateTime.Now.ToString("yyyy-M-d HH:mm:ss");
                string strCommand = "Update BabyInjuryCaseEvaluation Set Status='1',DeactivatedDate =" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(strCurrentTime) + "," +
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
		public long modify_record(
			string strBabyInjuryCaseEvaluationXML)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "NewBabyInjuryCaseEvaluationService", "modify_record");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (strBabyInjuryCaseEvaluationXML == null || strBabyInjuryCaseEvaluationXML == "")
                    return -1;

                lngRes = clsTablService.modify_record("BabyInjuryCaseEvaluation", strBabyInjuryCaseEvaluationXML, "INPATIENTNO", "ACTIVITYTIME", "ISNEWBABY");
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
		public long lngAddNewRecordOfAutoEval(
			string strBabyInjuryCaseEvaluationXML)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "NewBabyInjuryCaseEvaluationService", "lngAddNewRecordOfAutoEval");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (strBabyInjuryCaseEvaluationXML == null || strBabyInjuryCaseEvaluationXML == "")
                    return -1;
                lngRes = clsTablService.add_new_record("BabyInjuryCaseEvaluation", strBabyInjuryCaseEvaluationXML);
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
		public bool lngRecordExist(
			string PatientID,string ActivityTime,string IsNewBaby)
		{
			bool blnRes = false;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "NewBabyInjuryCaseEvaluationService", "lngRecordExist");
                //if (lngCheckRes <= 0)
                    return false;

                blnRes = clsTablService.bolMulRecordExist("BabyInjuryCaseEvaluation", "<root INPATIENTNO='" + PatientID + "' ActivityTime=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(ActivityTime) + " IsNewBaby='" + IsNewBaby + "' />", "INPATIENTNO", "ActivityTime", "IsNewBaby");
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
		public long lngGetXMLTable(
			string PatientID,string IsNewBaby,ref string strXMLTable,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "NewBabyInjuryCaseEvaluationService", "lngGetXMLTable");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strCommand = "select top 1 * from BabyInjuryCaseEvaluation where INPATIENTNO='" + PatientID + "'and IsNewBaby='" + IsNewBaby + "' ORDER BY ActivityTime DESC";
                else
                    strCommand = "select * from(select * from BabyInjuryCaseEvaluation where PatientID='" + PatientID + "'and IsNewBaby='" + IsNewBaby + "' ORDER BY ActivityTime DESC)where rownum = 1";
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
		public long lngGetDepartmentXMLTable(
			string strPatientID,ref string strXMLTable,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "NewBabyInjuryCaseEvaluationService", "lngGetDepartmentXMLTable");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

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
		public long lngGetXMLBaseInfo(
			string strInHospitalNO,ref string strXMLTable,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "NewBabyInjuryCaseEvaluationService", "lngGetXMLBaseInfo");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

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

		[AutoComplete]
		public bool lngRecordExist(
			string PatientID,string ActivityTime)
		{
			bool blnRes = false;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "NewBabyInjuryCaseEvaluationService", "lngRecordExist");
                //if (lngCheckRes <= 0)
                    return false;

                blnRes = clsTablService.bolRecordExist("BabyInjuryCaseEvaluation", PatientID, ActivityTime, "ACTIVITYTIME");
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
		public long lngXMLLikeQuery(
			string pid,ref string strXMLSet,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "NewBabyInjuryCaseEvaluationService", "lngXMLLikeQuery");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

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
		public long m_lngGetTimeInfoOfAPatient(
			string p_strInPatientID,string p_strInPatientDate,string p_strFromDate,string p_strToDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"NewBabyInjuryCaseEvaluationService","m_lngGetTimeInfoOfAPatient");
                //if(lngCheckRes <= 0)
					//return lngCheckRes;

				string strCommand="";

				//			strCommand="SELECT distinct ActivityTime FROM NewBabyInjuryCaseEvaluation WHERE InPatientNO ='" +
				//				p_strInPatientID +"'" +" AND Status =0 and InPatientDate="+clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+" and ActivityTime>=" + clsHRPTableService.s_strOracleDateTime(p_strFromDate) + " and ActivityTime<=" + clsHRPTableService.s_strOracleDateTime(p_strToDate) ;
				strCommand="SELECT distinct ActivityTime FROM NewBabyInjuryCaseEvaluation WHERE InPatientNO ='" +
					p_strInPatientID +"'" +" AND Status =0 and InPatientDate="+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+" ";

				lngRes = new clsHRPTableService().lngGetXMLTable(strCommand ,ref p_strXml ,ref intRows );
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			return lngRes;
		}

		[AutoComplete]
		public long m_lngAddNew(
			string p_strMainXml)
		{
			long lngRes = -1;
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"NewBabyInjuryCaseEvaluationService","m_lngAddNew");
                //if (lngCheckRes <= 0)
					//return lngCheckRes;

				if(p_strMainXml == null || p_strMainXml == "")
					return -1;

				string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			
				int intIndex = p_strMainXml.IndexOf(' ');

				p_strMainXml = p_strMainXml.Insert(intIndex, " ModifyDate=\"" + strCurrentTime + "\"");

				lngRes = new clsHRPTableService().add_new_record("NewBabyInjuryCaseEvaluation",p_strMainXml);
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
 		   return lngRes;
		}

		[AutoComplete]
		public long m_lngGetNewBabyInjuryCaseValuation(
			string p_strPatientID,string p_strInPatientDate,string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();

            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "NewBabyInjuryCaseEvaluationService", "m_lngGetNewBabyInjuryCaseValuation");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strCommand = "select top 1 *  from NewBabyInjuryCaseEvaluation a  " +
                        "  WHERE a.InPatientNO='" +
                        p_strPatientID + "'" + " AND a.Status =0 and a.ActivityTime=" +
                        clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCreateDate) + " and a.InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + " order by ModifyDate desc";
                else
                    strCommand = "select * from(select *  from NewBabyInjuryCaseEvaluation a  " +
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

