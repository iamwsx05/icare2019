using System.EnterpriseServices;
using System;
using System.Text ;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.BabyInjuryCaseEvaluationServ
{
	/// <summary>
	/// 
	/// </summary>
	
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsBabyInjuryCaseEvaluationServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{		
		[AutoComplete]
		public long add_new_record( string strBabyInjuryCaseEvaluation)
		{
			long lngRes = -1;
            clsHRPTableService hs=new clsHRPTableService();
            try
            { 
                if (strBabyInjuryCaseEvaluation == null || strBabyInjuryCaseEvaluation == "")
                    return -1;



                StringBuilder tem = new StringBuilder(strBabyInjuryCaseEvaluation);
                int index = strBabyInjuryCaseEvaluation.IndexOf(" ");

                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tem.Insert(index, " Status=0ACTIVITYTIME=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(strCurrentTime));
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
		public long deactive_record( string PatientID,string ActivityTime,string IsNewBaby,string DeEmployeeID)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (PatientID == null || PatientID == "" || DeEmployeeID == null || DeEmployeeID == "" || ActivityTime == null || ActivityTime == "" || IsNewBaby == null || IsNewBaby == "")
                    return -1;

                string strCommand = "update BabyInjuryCaseEvaluation set STATUS=1,DEACTIVEDOPERATORID=? ,DeactivatedDate=? where INPATIENTNO=? and ACTIVITYTIME=? and ISNEWBABY=?";

                IDataParameter[] objDPArr = null;
                clsTablService.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = DeEmployeeID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[2].Value = PatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(ActivityTime);
                objDPArr[4].Value = IsNewBaby;

                long lngEff = -1;
                lngRes = clsTablService.lngExecuteParameterSQL(strCommand,ref lngEff,objDPArr);
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
		public long modify_record( string strBabyInjuryCaseEvaluationXML)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
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
		public long lngAddNewRecordOfAutoEval( string strBabyInjuryCaseEvaluation)
		{
			long lngRes = -1;
            clsHRPTableService hs=new clsHRPTableService();
			try
            { 
				if(strBabyInjuryCaseEvaluation==null || strBabyInjuryCaseEvaluation=="")
					return -1;

				
			
				StringBuilder tem=new StringBuilder(strBabyInjuryCaseEvaluation);
				int index=strBabyInjuryCaseEvaluation.IndexOf(" ");

				string strCurrentTime=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				tem.Insert(index," Status=0");
				strBabyInjuryCaseEvaluation=tem.ToString();

				lngRes = new clsHRPTableService().add_new_record("BabyInjuryCaseEvaluation",strBabyInjuryCaseEvaluation);
			}
			catch(Exception objEx)
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
		public bool lngRecordExist( string PatientID,string ActivityTime,string IsNewBaby)
		{
			bool blnRes = false;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                //			return new clsHRPTableService().bolRecordExist("BabyInjuryCaseEvaluation",PatientID,ActivityTime,"ActivityTime");
                blnRes = clsTablService.bolMulRecordExist("BabyInjuryCaseEvaluation", "<root INPATIENTNO='" + PatientID + "' ACTIVITYTIME=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(ActivityTime) + " IsNewBaby='" + IsNewBaby + "' />", "INPATIENTNO", "ACTIVITYTIME", "ISNEWBABY");
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
		public long lngGetXMLTable( string PatientID,string strActivityTime,string IsNewBaby,ref string strXMLTable,ref int rows)
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
                //			if(IsNewBaby !="")
                //				strWhere += "and IsNewBaby='"+IsNewBaby+"' ";
                //			if(strWhere == " where ")
                //				return 0;
                //			string strCommand = "select * from BabyInjuryCaseEvaluation "+strWhere;
                string strCommand = "";
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strCommand = "select top 1* from BabyInjuryCaseEvaluation where INPATIENTNO=? and ActivityTime=? and IsNewBaby=? ORDER BY ActivityTime DESC";
                else
                    strCommand = "select * from(select * from BabyInjuryCaseEvaluation where INPATIENTNO=? and ActivityTime=? and IsNewBaby=? ORDER BY ActivityTime DESC)where rownum = 1";

                IDataParameter[] objDPArr = null;
                clsTablService.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = PatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(strActivityTime);
                objDPArr[2].Value = IsNewBaby;

                lngRes = clsTablService.lngGetXMLTableWithParameter(strCommand, ref strXMLTable, ref rows, objDPArr);
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
                string strCommand = "select * from DepartmentSickRoom  where INHOSPITALNO=? and  OUTHOSPITALDATE=?";

                IDataParameter[] objDPArr = null;
                clsTablService.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = strPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = new DateTime(1900,1,1);

                lngRes = clsTablService.lngGetXMLTableWithParameter(strCommand, ref strXMLTable, ref rows, objDPArr);
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


		[AutoComplete]
		public long lngXMLLikeQuery( string pid,ref string strXMLSet,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                //			string strCommand ="SELECT PatientID, ActivityTime FROM BabyInjuryCaseEvaluation WHERE IsNewBaby='"+IsNewBaby+"' and (PatientID in(SELECT InHospitalNO FROM PatientInHospitalInfo WHERE Status=0and OutOfHospital is null  and  InHospitalNO like'"+ pid+"%'))";
                //			string strCommand = "Select PatientID,ActivityTime from BabyInjuryCaseEvaluation where IsNewBaby='"+IsNewBaby+"' and  Status=0and PatientID like'"+ pid+"%' " ;//been 2002-8-24
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
		public long lngGetXMLDatesArr( string strID,string strInHospiatlNO,string strDateFrom,string strDateTo,ref string strXMLTable,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                string strCommand = "";
                if (strID.Trim().Length == 0 && strInHospiatlNO.Trim().Length == 0)
                    return 0;
                //			else if(strID.Trim().Length!=0)//此处strID其实不需要，但为了以后可能要增加的功能而保留
                //				strCommand = "select distinct ActivityTime from BabyInjuryCaseEvaluation where PatientID='"+strID+"' and Status=0 ";//注意：此处由于数据库的字段写误，导致后面的编程将错就错。
                //			else if(strInHospiatlNO.Trim().Length!=0)
                strCommand = "select distinct ActivityTime from BabyInjuryCaseEvaluation where INPATIENTNO='" + strInHospiatlNO + "' and Status=0  and ActivityTime>=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(strDateFrom) + " and ActivityTime<=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(strDateTo);//PatientID字段实际的意义应该是InHospiatlNO

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
		public long lngGetDoctorNameXMLTable( string strDoctorID,ref string strXMLTable,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (strDoctorID.Trim().Length == 0)
                    return -1;
                string strCommand = "select distinct FirstName from EmployeeBaseInfo where EmployeeID='" + strDoctorID + "' and Status=0";
                lngRes = new clsHRPTableService().lngGetXMLTable(strCommand, ref strXMLTable, ref rows);
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

                //			strCommand="SELECT distinct ActivityTime FROM BabyInjuryCaseEvaluation WHERE InPatientNO ='" +
                //				p_strInPatientID +"'" +" AND Status =0 and InPatientDate="+clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+" and ActivityTime>=" + clsHRPTableService.s_strOracleDateTime(p_strFromDate) + " and ActivityTime<=" + clsHRPTableService.s_strOracleDateTime(p_strToDate) ;
                strCommand = "SELECT distinct ActivityTime FROM BabyInjuryCaseEvaluation WHERE InPatientNO ='" +
                    p_strInPatientID + "'" + " AND Status =0 and InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + " ";

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
		public long m_lngGetBabyInjuryCaseValuation( string p_strPatientID,string p_strInPatientDate,string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                string strCommand = "";

                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strCommand = "select top 1 *  from BabyInjuryCaseEvaluation a  " +
                        "  WHERE a.InPatientNO='" +
                        p_strPatientID + "'" + " AND a.Status =0 and a.ActivityTime=" +
                        clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCreateDate) + " and a.InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + " order by ModifyDate desc";
                else
                    strCommand = "select * from(select *  from BabyInjuryCaseEvaluation a  " +
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

                lngRes = clsTablService.add_new_record("BabyInjuryCaseEvaluation", p_strMainXml);
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
