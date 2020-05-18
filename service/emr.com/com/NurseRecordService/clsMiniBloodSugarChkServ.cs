using System;
using System.EnterpriseServices;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.clsMiniBloodSugarChkServ
{
	/// <summary>
	/// clsMiniBloodSugarChkServ 的摘要说明。
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsMiniBloodSugarChkServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsMiniBloodSugarChkServ()
		{}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewRecoed(clsMiniBloodSugarChkValue p_objValue)
		{
			if(p_objValue == null)
				return -1;
			if(p_objValue.m_strInPatientID == string.Empty || p_objValue.m_dtmInPatientDate == DateTime.MinValue)
				return -1;
			string strSql = @"insert into nws_minibldsugarchk (inpatientid, inpatientdate, opendate, createdate,
             content_breakfast, content_lunch, content_supper, content_prerest, createduserid, status )
     values (?,?,?,?,?,?,?,?,?,'0')";
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[9];
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(9, out objDPArr);
                objDPArr[0].Value = p_objValue.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objValue.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objValue.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objValue.m_dtmCreatedDate;
                objDPArr[4].Value = p_objValue.m_strBreakfast;
                objDPArr[5].Value = p_objValue.m_strLunch;
                objDPArr[6].Value = p_objValue.m_strSupper;
                objDPArr[7].Value = p_objValue.m_strPreRest;
                objDPArr[8].Value = p_objValue.m_strCreateUserID;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			return lngRes;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyRecoed(clsMiniBloodSugarChkValue p_objValue)
		{
			if(p_objValue == null)
				return -1;
			if(p_objValue.m_strInPatientID == string.Empty || p_objValue.m_dtmInPatientDate == DateTime.MinValue)
				return -1;
			string strSql = @"update nws_minibldsugarchk
   set opendate = ?, content_breakfast = ?,content_lunch = ?,content_supper = ?,content_prerest = ?
 where inpatientid = ? and inpatientdate = ? and createdate = ?";
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[8];
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(8, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objValue.m_dtmOpenDate;
                objDPArr[1].Value = p_objValue.m_strBreakfast;
                objDPArr[2].Value = p_objValue.m_strLunch;
                objDPArr[3].Value = p_objValue.m_strSupper;
                objDPArr[4].Value = p_objValue.m_strPreRest;
                objDPArr[5].Value = p_objValue.m_strInPatientID;
                objDPArr[6].DbType = DbType.DateTime;
                objDPArr[6].Value = p_objValue.m_dtmInPatientDate;
                objDPArr[7].DbType = DbType.DateTime;
                objDPArr[7].Value = p_objValue.m_dtmCreatedDate;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			return lngRes;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteRecoed(clsMiniBloodSugarChkValue p_objValue)
		{
			if(p_objValue == null)
				return -1;
			if(p_objValue.m_strInPatientID == string.Empty || p_objValue.m_dtmInPatientDate == DateTime.MinValue)
				return -1;
			string strSql = @"update nws_minibldsugarchk set status = ?,deactiveddate = ?,deactivedoperatorid = ?
 where inpatientid = ? and inpatientdate = ? and createdate = ?";
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[6];
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = 1;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objValue.m_dtmDeActivedDate;
                objDPArr[2].Value = p_objValue.m_strDeActivedUserID;
                objDPArr[3].Value = p_objValue.m_strInPatientID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objValue.m_dtmInPatientDate;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_objValue.m_dtmCreatedDate;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            } return lngRes;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_dtmInPatientDate"></param>
		/// <param name="p_objValues"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecoedByInPatient(string p_strInPatientID,DateTime p_dtmInPatientDate,out clsMiniBloodSugarChkValue[] p_objValues)
		{
			p_objValues = null;
			if(string.IsNullOrEmpty(p_strInPatientID) || p_dtmInPatientDate == DateTime.MinValue)
				return -1;
            string strSql = @"select inpatientid,
       inpatientdate,
       opendate,
       content_breakfast,
       content_lunch,
       content_supper,
       content_prerest,
       createduserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       createdate
  from nws_minibldsugarchk
 where inpatientid = ?
   and inpatientdate = ?
   and status = '0'
 order by createdate";
			
			DataTable dtbValues = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInPatientDate;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValues, objDPArr);
                if (lngRes <= 0 || dtbValues.Rows.Count <= 0)
                    return 0;
                p_objValues = new clsMiniBloodSugarChkValue[dtbValues.Rows.Count];
                for (int i = 0; i < dtbValues.Rows.Count; i++)
                {
                    p_objValues[i] = new clsMiniBloodSugarChkValue();
                    p_objValues[i].m_strInPatientID = p_strInPatientID;
                    p_objValues[i].m_dtmInPatientDate = p_dtmInPatientDate;
                    p_objValues[i].m_dtmCreatedDate = DateTime.Parse(dtbValues.Rows[i]["CREATEDATE"].ToString());
                    p_objValues[i].m_dtmOpenDate = DateTime.Parse(dtbValues.Rows[i]["OPENDATE"].ToString());
                    p_objValues[i].m_strCreateUserID = dtbValues.Rows[i]["CREATEDUSERID"].ToString();
                    p_objValues[i].m_strBreakfast = dtbValues.Rows[i]["CONTENT_BREAKFAST"].ToString();
                    p_objValues[i].m_strLunch = dtbValues.Rows[i]["CONTENT_LUNCH"].ToString();
                    p_objValues[i].m_strPreRest = dtbValues.Rows[i]["CONTENT_PREREST"].ToString();
                    p_objValues[i].m_strSupper = dtbValues.Rows[i]["CONTENT_SUPPER"].ToString();
                }

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
		}
	}
}
