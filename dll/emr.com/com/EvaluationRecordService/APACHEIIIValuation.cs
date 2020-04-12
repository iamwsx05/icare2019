using System.EnterpriseServices;
using System;
using System.Text ;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using System.Xml;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.APACHEIIIValuation
{
	/// <summary>
	/// 
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsAPACHEIIIValuation : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		[AutoComplete]
		public long add_new_record( string strAPACHEIIIValuationXML)
		{
			long lngRes = -1;
	        clsHRPTableService hs=new clsHRPTableService();
            try
            { 
                if (strAPACHEIIIValuationXML == null || strAPACHEIIIValuationXML == "")
                    return -1;



                StringBuilder tem = new StringBuilder(strAPACHEIIIValuationXML);
                int index = strAPACHEIIIValuationXML.IndexOf(" ");
                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tem.Insert(index, " Status=0ActivityTime=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(strCurrentTime));
                strAPACHEIIIValuationXML = tem.ToString();

                lngRes = hs.add_new_record("APACHEIIIValuation", strAPACHEIIIValuationXML);
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
		public long deactive_record( string InHospitalNO,string ActivityTime,string DeEmployeeID)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (InHospitalNO == null || InHospitalNO == "" || DeEmployeeID == null || DeEmployeeID == "" || ActivityTime == null || ActivityTime == "")
                    return -1;

                string strCommand = "update APACHEIIIValuation set status=1,DEACTIVEDOPERATORID='" + DeEmployeeID + "' ,DeactivatedDate={fn CURDATE()} where INPATIENTNO='" + InHospitalNO + "' and ActivityTime=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(ActivityTime);

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
		public long modify_record( string strAPACHEIIIValuationXML)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (strAPACHEIIIValuationXML == null || strAPACHEIIIValuationXML == "")
                    return -1;

                lngRes = clsTablService.modify_record("APACHEIIIValuation", strAPACHEIIIValuationXML, "INPATIENTNO", "ACTIVITYTIME");
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
		public long lngAddNewRecordOfAutoEval( string strAPACHEIIIValuationXML)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (strAPACHEIIIValuationXML == null || strAPACHEIIIValuationXML == "")
                    return -1;

                StringBuilder tem = new StringBuilder(strAPACHEIIIValuationXML);
                int index = strAPACHEIIIValuationXML.IndexOf(" ");
                tem.Insert(index, " Status=0");
                strAPACHEIIIValuationXML = tem.ToString();

                lngRes = clsTablService.add_new_record("APACHEIIIValuation", strAPACHEIIIValuationXML);
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
		public bool lngRecordExist( string InHospitalNO,string ActivityTime)
		{
			bool blnRes = false;
			try
            { 
				blnRes = bolRecordExist( "APACHEIIIValuation",InHospitalNO,ActivityTime,"ACTIVITYTIME");
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes1 = objLogger.LogError(objEx);					
			}
			return blnRes;
		}
	
		[AutoComplete]
		public bool bolRecordExist ( string strTableName,string InHospitalNO,string Date,string PDateOfReception)
		{
			DataTable dtResult = new DataTable();
            clsHRPTableService aa = new clsHRPTableService();
            try
            { 
                if (strTableName == "" || InHospitalNO == "" || Date == "")
                    return false;
                string strCommand = "Select * from " + strTableName + " where InHospitalNO=? and " + PDateOfReception + "= ?" ;
                IDataParameter[] objDPArr = null;
                aa.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = InHospitalNO;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(Date);

                aa.lngGetDataTableWithParameters(strCommand, ref dtResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                aa.Dispose();
            }
			if(dtResult.Rows.Count < 1)
				return false;
			else 
				return true;
		}

		[AutoComplete]
		public bool bolRecordExist1Parameter( string strTableName,string ValueParameter,string FieldParameter)
		{
			DataTable dtResult = new DataTable();
            clsHRPTableService aa = new clsHRPTableService();
            try
            { 
                if (strTableName == "" || ValueParameter == "" || FieldParameter == "")
                    return false;
                string strCommand = "Select * from " + strTableName + " where " + FieldParameter + "=? ";
                IDataParameter[] objDPArr = null;
                aa.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = ValueParameter;

                aa.lngGetDataTableWithParameters(strCommand, ref dtResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                aa.Dispose();
            }
			if(dtResult.Rows.Count < 1)
				return false;
			else 
				return true;
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
                //			string strCommand ="SELECT InHospitalNO, ActivityTime FROM APACHEIIIValuation WHERE (InHospitalNO in(SELECT InHospitalNO FROM PatientInHospitalInfo WHERE Status=0and OutOfHospital is null  and  InHospitalNO like'"+ pid+"%'))";
                //			string strCommand = "Select InHospitalNO,ActivityTime from APACHEIIIValuation where Status=0and InHospitalNO like'"+ pid+"%' " ;
                string strCommand = "select INPATIENTID from PatientBaseInfo where Status=0and INPATIENTID like'" + pid + "%' ";
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
                string strCommand = "select * from DepartmentSickRoom  where InHospitalNO=? and  OutHospitalDate= ?";

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
		public long lngGetXMLTable( string InHospitalNO,string strActivityTime,ref string strXMLTable,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                string strCommand = "select * from APACHEIIIValuation where INPATIENTNO=? AND ActivityTime=?" ;
                IDataParameter[] objDPArr = null;
                clsTablService.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = InHospitalNO;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(strActivityTime);

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
                strCommand = "select distinct ActivityTime from APACHEIIIValuation where INPATIENTNO=? and Status=0  and ActivityTime>=? and ActivityTime<=?";//PatientID字段实际的意义应该是InHospiatlNO

                IDataParameter[] objDPArr = null;
                clsTablService.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = strInHospiatlNO;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(strDateFrom);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(strDateTo);

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
		public long lngGetDoctorNameXMLTable( string strDoctorID,ref string strXMLTable,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (strDoctorID.Trim().Length == 0)
                    return -1;
                string strCommand = "select distinct FirstName from EmployeeBaseInfo where EmployeeID=? and Status=0";
                IDataParameter[] objDPArr = null;
                clsTablService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strDoctorID;

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

                //			strCommand="SELECT distinct ActivityTime FROM APACHEIIIValuation WHERE InPatientNO ='" +
                //				p_strInPatientID +"'" +" AND Status =0 and InPatientDate="+clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+" and ActivityTime>=" + clsHRPTableService.s_strOracleDateTime(p_strFromDate) + " and ActivityTime<=" + clsHRPTableService.s_strOracleDateTime(p_strToDate);
                strCommand = "SELECT distinct ActivityTime FROM APACHEIIIValuation WHERE INPATIENTNO =?" 
                    + " AND Status =0 and InPatientDate=?";

                IDataParameter[] objDPArr = null;
                clsTablService.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = clsTablService.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr);
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

                lngRes = clsTablService.add_new_record("APACHEIIIValuation", p_strMainXml);
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
		public long m_lngGetApacheIIIValuation( string p_strPatientID,string p_strInPatientDate,string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                string strCommand = "";

                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strCommand = "select top 1 *  from APACHEIIIValuation a  " +
                        "  WHERE a.InPatientNO=?" 
                        + " AND a.Status =0 and a.ActivityTime=? and a.InPatientDate=? order by ModifyDate desc";
                else
                    strCommand = "select * from(select *  from APACHEIIIValuation a  " +
                        "  WHERE a.InPatientNO=?" 
                        + " AND a.Status =0 and a.ActivityTime=? and a.InPatientDate=? order by ModifyDate desc)where rownum = 1";

                IDataParameter[] objDPArr = null;
                clsTablService.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreateDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = clsTablService.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr);
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