using System.EnterpriseServices;
using System;
using System.Text ;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using System.Xml;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.APACHEIIValuation
{
	/// <summary>
	/// 
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsAPACHEIIValuation : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		[AutoComplete]
		public long add_new_record( string strAPACHEIIValuationXML)
		{
			long lngRes = -1;
            clsHRPTableService hs=new clsHRPTableService();
            try
            { 
                if (strAPACHEIIValuationXML == null || strAPACHEIIValuationXML == "")
                    return -1;



                StringBuilder tem = new StringBuilder(strAPACHEIIValuationXML);
                int index = strAPACHEIIValuationXML.IndexOf(" ");
                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tem.Insert(index, " Status=0ActivityTime=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(strCurrentTime));
                strAPACHEIIValuationXML = tem.ToString();

                lngRes = hs.add_new_record("APACHEIIValuation", strAPACHEIIValuationXML);
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
		public long deactive_record( string PatientID,string ActivityTime,string DeEmployeeID)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try 
            { 
                if (PatientID == null || PatientID == "" || DeEmployeeID == null || DeEmployeeID == "" || ActivityTime == null || ActivityTime == "")
                    return -1;

                string strCommand = "update APACHEIIValuation set status=1,DEACTIVEDOPERATORID=? ,DeactivatedDate=? where INPATIENTNO=? and ActivityTime=?" ;
                IDataParameter[] objDPArr = null;
                clsTablService.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = DeEmployeeID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[2].Value = PatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(ActivityTime);

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
		public long modify_record( string strAPACHEIIValuationXML)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (strAPACHEIIValuationXML == null || strAPACHEIIValuationXML == "")
                    return -1;

                lngRes = clsTablService.modify_record("APACHEIIValuation", strAPACHEIIValuationXML, "INPATIENTNO", "ACTIVITYTIME");
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
		public long lngAddNewRecordOfAutoEval( string strAPACHEIIValuationXML)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (strAPACHEIIValuationXML == null || strAPACHEIIValuationXML == "")
                    return -1;

                StringBuilder tem = new StringBuilder(strAPACHEIIValuationXML);
                int index = strAPACHEIIValuationXML.IndexOf(" ");
                tem.Insert(index, " Status=0");
                strAPACHEIIValuationXML = tem.ToString();

                lngRes = clsTablService.add_new_record("APACHEIIValuation", strAPACHEIIValuationXML);
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
				blnRes = bolRecordExist( "APACHEIIValuation",InHospitalNO,ActivityTime,"ACTIVITYTIME");
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes1 = objLogger.LogError(objEx);					
			}
			return blnRes;
		}
		com.digitalwave.iCare.middletier.HRPService.clsHRPTableService aa=new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();

		[AutoComplete]
		public bool bolRecordExist ( string strTableName,string InHospitalNO,string Date,string PDateOfReception)
		{
			DataTable dtResult = new DataTable();
			try
			{ 
				if(strTableName == "" || InHospitalNO == "" || Date == "")
					return false;
				string strCommand = "Select * from "+strTableName+" where INPATIENTNO=? and "+PDateOfReception+"= ?";

                IDataParameter[] objDPArr = null;
                aa.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = InHospitalNO;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(Date);

                aa.lngGetDataTableWithParameters(strCommand, ref dtResult, objDPArr);
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			if(dtResult.Rows.Count < 1)
				return false;
			else 
				return true;
		}
		[AutoComplete]
		public long lngGetXMLTable( string PatientID,string strActivityTime,ref string strXMLTable,ref int rows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {  
                string strCommand = "select * from APACHEIIValuation where INPATIENTNO=? AND ActivityTime=?";
                IDataParameter[] objDPArr = null;
                clsTablService .CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = PatientID;
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
			try
			{ 
				string strCommand="";

				//			strCommand="SELECT distinct ActivityTime FROM APACHEIIValuation WHERE InPatientNO ='" +
				//				p_strInPatientID +"'" +" AND Status =0 and InPatientDate="+clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+" and ActivityTime>=" + clsHRPTableService.s_strOracleDateTime(p_strFromDate) + " and ActivityTime<=" + clsHRPTableService.s_strOracleDateTime(p_strToDate) ;
				strCommand="SELECT distinct ActivityTime FROM APACHEIIValuation WHERE InPatientNO =?" 
                    +" AND Status =0 and InPatientDate=? ";

                clsHRPTableService objServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objServ.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr);
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			return lngRes;
		}

		[AutoComplete]
		public long m_lngAddNew( string p_strMainXml)
		{
			long lngRes = -1;
			try
			{ 
				if(p_strMainXml == null || p_strMainXml == "")
					return -1;

				string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			
				int intIndex = p_strMainXml.IndexOf(' ');

				p_strMainXml = p_strMainXml.Insert(intIndex, " ModifyDate=\"" + strCurrentTime + "\"");

				lngRes = new clsHRPTableService().add_new_record("APACHEIIValuation",p_strMainXml); 
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			return lngRes;
		}

		[AutoComplete]
		public long m_lngGetApacheIIValuation( string p_strPatientID,string p_strInPatientDate,string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;
			try
            { 
				string strCommand="";

				if(clsHRPTableService.bytDatabase_Selector == 0)
					strCommand="select top 1 *  from APACHEIIValuation a  "+
						"  WHERE a.InPatientNO=?  AND a.Status =0 and a.ActivityTime= ? "+
						" and a.InPatientDate= ? order by ModifyDate desc";
				else
					strCommand="select * from(select *  from APACHEIIValuation a  "+
						"  WHERE a.InPatientNO=? AND a.Status =0 and a.ActivityTime= ? and a.InPatientDate=? order by ModifyDate desc)where rownum = 1";
                clsHRPTableService objServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreateDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objServ.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr);
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			return lngRes;

		}

		[AutoComplete]
		public long m_lngDeActiveRecord( string strXml, string strTableName)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                int intIndex = strXml.IndexOf(' ');

                strXml = strXml.Insert(intIndex, " DeactivatedDate=\"" + strCurrentTime + "\"");


                lngRes = clsTablService.modify_record(strTableName, strXml, "INPATIENTNO", "INPATIENTDATE", "ACTIVITYTIME");
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