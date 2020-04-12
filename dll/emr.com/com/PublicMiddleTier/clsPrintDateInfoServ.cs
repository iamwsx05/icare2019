using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.PrintDateInfoService
{
	/// <summary>
	/// Summary description for clsPrintDateInfoServ.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsPrintDateInfoServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 用于打印时读或者保存打印时间的Middle Tier
		/// Alex 2003-03-13
		/// </summary>
		public clsPrintDateInfoServ()
		{
			//
			// TODO: Add constructor logic here
			//
            //objHRPServ=new clsHRPTableService();
		}
        //private clsHRPTableService objHRPServ;

		/// <summary>
		/// 查找该条记录的第一次打印时间
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_strTableName"></param>
		/// <returns></returns>
		[AutoComplete]
		public string m_strGetFirstPrintDate( string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,string p_strTableName)
		{
			string strRes=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            { 
                if (string.IsNullOrEmpty(p_strInPatientID))
                    return null;
                if (string.IsNullOrEmpty(p_strInPatientDate))
                    return null;
                if (string.IsNullOrEmpty(p_strCreateDate))
                    return null;
                if (string.IsNullOrEmpty(p_strTableName))
                    return null;
                string m_strFirstPrintDate = null;
                DataTable m_dtbResult = new DataTable();
                string m_strCommand = "select firstprintdate from " + p_strTableName + "  where " +
                                    "inpatientid = ? and inpatientdate = ? " +
                                    "and createdate = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                long m_lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref m_dtbResult, objDPArr);
                if (m_lngRes > 0)
                {
                    if (m_dtbResult.Rows[0][0] == null || m_dtbResult.Rows[0][0].ToString() == "")
                        return null;
                    m_strFirstPrintDate = m_dtbResult.Rows[0][0].ToString();
                    strRes = m_strFirstPrintDate;
                }
                else
                    strRes = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			//返回
			return strRes;
			
		}

		/// <summary>
		/// 设置该条记录的第一次打印时间
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_strTableName"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSetFirstPrintDate( string[] p_strInPatientID,string[] p_strInPatientDate,string[] p_strCreateDate,string p_strTableName)
		{

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            { 
                if (p_strInPatientID == null || p_strInPatientID.Length <= 0)
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate.Length <= 0)
                    return -1;
                if (p_strCreateDate == null || p_strCreateDate.Length <= 0)
                    return -1;
                if (string.IsNullOrEmpty(p_strTableName))
                    return -1;

                string m_strCurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string m_strCommand = null;
                for (int i1 = 0; i1 < p_strInPatientID.Length; i1++)
                {
                    m_strCommand = "update " + p_strTableName + " set firstprintdate = ? where  " +
                        "inpatientid = ? and inpatientdate = ? " +
                        "and createdate = ? ";

                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = DateTime.Parse(m_strCurrentDateTime);
                    objDPArr[1].Value = p_strInPatientID[i1];
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_strInPatientDate[i1]);
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = DateTime.Parse(p_strCreateDate[i1]);

                    long lngEff = -1;
                    lngRes = objHRPServ.lngExecuteParameterSQL(m_strCommand, ref lngEff, objDPArr);
                    if (lngRes <= 0)
                        lngRes = -1;
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			//返回
			return lngRes;
		}


		/// <summary>
		/// 先判断该记录是否已经打印过，如有，则不再写入时间，否则，保存该条记录的第一次打印时间
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_strTableName"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSetFirstPrintDateWithCheck( string[] p_strInPatientID,string[] p_strInPatientDate,string[] p_strCreateDate,string p_strTableName)
		{
			long lngRes =1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{ 
                if (p_strInPatientID == null || p_strInPatientID.Length <= 0)
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate.Length <= 0)
                    return -1;
                if (p_strCreateDate == null || p_strCreateDate.Length <= 0)
					return -1;
				if(string.IsNullOrEmpty(p_strTableName))
					return -1;
				
				string m_strCurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				string m_strFirstPrintDate = null;
				string m_strCommand = null;
				for(int i1=0;i1<p_strInPatientID.Length;i1++)
				{
					m_strFirstPrintDate = m_strGetFirstPrintDate( p_strInPatientID[i1],p_strInPatientDate[i1],p_strCreateDate[i1],p_strTableName);
					if(m_strFirstPrintDate == null || m_strFirstPrintDate == "")
					{
						m_strCommand = "update "+p_strTableName+" set firstprintdate = ? where  "+
							"inpatientid = ? and inpatientdate = ? "+
							"and createdate = ? ";

                        IDataParameter[] objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                        objDPArr[0].DbType = DbType.DateTime;
                        objDPArr[0].Value = DateTime.Parse(m_strCurrentDateTime);
                        objDPArr[1].Value = p_strInPatientID[i1];
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Parse(p_strInPatientDate[i1]);
                        objDPArr[3].DbType = DbType.DateTime;
                        objDPArr[3].Value = DateTime.Parse(p_strCreateDate[i1]);

                        long lngEff = -1;
                        lngRes = objHRPServ.lngExecuteParameterSQL(m_strCommand, ref lngEff, objDPArr);
					}
					if(lngRes <= 0)
						lngRes= -1;
				}

			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
            finally
            {
                //objHRPServ.Dispose();
            }
			//返回
			return lngRes;
		}

		#region 记录病程记录打印到第几条记录
		/// <summary>
		/// 保存某个记录单打印到第几条记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strFormName"></param>
		/// <param name="p_intRecord"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSavePrintToRecord(string p_strInPatientID,string p_strInPatientDate,string p_strFormName,
			int p_intToRecord)
		{
			long lngRes = 0;
            clsHRPTableService objServ = new clsHRPTableService();
            try
            {

                string strSql = @"select inpatientid, inpatientdate, formname, record
  from continueprintrecord
 where (inpatientid = ?)
   and (inpatientdate = ?)
   and (formname = ?)";

                IDataParameter[] objDPArr = null;
                objServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strFormName;

                DataTable dtResult = new DataTable();
                lngRes = objServ.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    strSql = @"delete from continueprintrecord
							where (inpatientid = ?) and (inpatientdate = ?) and (formname = ?)";

                    IDataParameter[] objDPArr1 = null;
                    objServ.CreateDatabaseParameter(3, out objDPArr1);
                    objDPArr1[0].Value = p_strInPatientID;
                    objDPArr1[1].DbType = DbType.DateTime;
                    objDPArr1[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr1[2].Value = p_strFormName;

                    long lngEff = -1;
                    objServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr1);
                }
                strSql = @"insert into continueprintrecord (inpatientid, inpatientdate, formname, record)
							values (?, ?, ?, ?)";

                IDataParameter[] objDPArr2 = null;
                objServ.CreateDatabaseParameter(4, out objDPArr2);
                objDPArr2[0].Value = p_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr2[2].Value = p_strFormName;
                objDPArr2[3].Value = p_intToRecord;

                long lngEff1 = -1;
                lngRes = objServ.lngExecuteParameterSQL(strSql, ref lngEff1, objDPArr2);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objServ.Dispose();
            }
			//返回
			return lngRes;
			
		}

		/// <summary>
		/// 获取某个记录单打印到第几条记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strFormName"></param>
		/// <param name="p_intRecord"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPrintFromRecord(string p_strInPatientID,string p_strInPatientDate,string p_strFormName,
			out int p_intFromRecord)
		{
			long lngRes = 0;
			p_intFromRecord=0;
			clsHRPTableService objServ = new clsHRPTableService();
			try
			{
				p_intFromRecord = 0;
				string strSql = @"select record from continueprintrecord
					where (inpatientid = ?) and (inpatientdate = ?) and (formname = ?)";

                IDataParameter[] objDPArr = null;
                objServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strFormName;

				DataTable dtResult = new DataTable();
                lngRes = objServ.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);
				if(lngRes > 0 && dtResult.Rows.Count == 1)
					p_intFromRecord = int.Parse(dtResult.Rows[0][0].ToString());

			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
            finally
            {
                objServ.Dispose();
            }
			//返回
			return lngRes;
		}
		#endregion
	}
}
