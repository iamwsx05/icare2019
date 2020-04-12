using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;


namespace com.digitalwave.clsICUGEMaintenanceServ
{
	/// <summary>
	/// clsICUGEMaintenanceServ 的摘要说明。
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsICUGEMaintenanceServ:com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsICUGEMaintenanceServ()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		[AutoComplete]
		public long m_lngGetBedGEInfo(string p_BedID,ref bool p_blnIsExist,ref string p_strGENo)
		{
			p_blnIsExist=false;
			p_strGENo="";
			DataTable dtRecord=null;
			long lngRet = -1;
			string SQL="select GE_NO from ICUGENOBYBED where BED_ID='"+ p_BedID + "'";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                lngRet = objTabService.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
                if (lngRet < 0)
                    return lngRet;
                if (dtRecord.Rows.Count > 0)
                {
                    p_blnIsExist = true;
                    p_strGENo = dtRecord.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			return lngRet;
		}

		[AutoComplete]
		public long m_lngSaveGEInf(string p_strGENo,string p_strGEType,string p_strGEModle,string p_strGEIP,string p_strMonitor_Type)
		{
			long lngRet = -1;
			string SQL="";
			SQL="Delete from ICUGENOBYBED where GE_NO='" + p_strGENo.Replace("'","''") + "'";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                lngRet = objTabService.DoExcute(SQL);
                if (lngRet < 0)
                    return lngRet;
                SQL = "insert into ICUGENOBYBED(GE_NO,GE_TYPE,GE_MODEL,GE_IP,MONITOR_TYPE) values ('" + p_strGENo.Replace("'", "''") + "','" + p_strGEType.Replace("'", "''") + "','" + p_strGEModle.Replace("'", "''") + "','" + p_strGEIP.Replace("'", "''") + "','" + p_strMonitor_Type.Replace("'", "''") + "')";
                lngRet = new clsHRPTableService().DoExcute(SQL);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			return lngRet;
		}

		/// <summary>
		/// 得到仪器信息 p_strGENo=""取出全部仪器信息
		/// </summary>
		/// <param name="_strGENo"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetGEInf(string p_strGENo,ref DataTable p_dtRecord)
		{
			string SQL="";
			long lngRet = -1;
			if (p_strGENo.Trim().Length==0)
			{
                SQL = "select * from ICUGENOBYBED order by to_number(GE_NO)";
			}
			else
			{
				SQL="select * from ICUGENOBYBED where GE_NO='" + p_strGENo.Replace("'","''") +"'";
			}
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                lngRet = objTabService.lngGetDataTableWithoutParameters(SQL, ref p_dtRecord);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			return lngRet;
		}

		[AutoComplete]
		public long m_lngSetGeBed(string p_strGENo,string p_strBedID)
		{
			string SQL="";
			long lngRet = -1;
			if (p_strBedID.Trim().Length==0)
			{
				SQL="Update ICUGENOBYBED set BED_ID='' where GE_NO='" + p_strGENo.Replace("'","''") +"'";
			}
			else
			{
				SQL="Update ICUGENOBYBED set BED_ID='" + p_strBedID.Replace("'","''") + "' where GE_NO='" + p_strGENo.Replace("'","''") +"'";
			}
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                lngRet = objTabService.DoExcute(SQL);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			return lngRet;
		}

		[AutoComplete]
		public long m_lngUpdataGEInf(string p_strGENoOld,string p_strGENo,string p_strGEType,string p_strGEModle,string p_strGEIP)
		{
			long lngRet = -1;
			string SQL="";
			SQL="update ICUGENOBYBED set GE_NO='" + p_strGENo.Replace("'","''") + "',GE_TYPE='" + p_strGEType.Replace("'","''") + "',GE_MODEL='" + p_strGEModle.Replace("'","''") + "',GE_IP='" + p_strGEIP.Replace("'","''") + "' where GE_NO='" + p_strGENoOld.Replace("'","''") + "'";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                lngRet = objTabService.DoExcute(SQL);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			return lngRet;
		}

		[AutoComplete]
		public long m_lngDelGEInf(string p_strGENo)
		{
			long lngRet = -1;
			string SQL="delete from ICUGENOBYBED where GE_NO='" + p_strGENo.Replace("'","''") + "'";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                lngRet = objTabService.DoExcute(SQL);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			return lngRet;
		}

		/// <summary>
		/// 添加GE仪器输出参数记录
		/// </summary>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewParamRecordArr(
			clsICUGEPARAMParamValue[] p_objParamArr)
		{
			if(p_objParamArr == null || p_objParamArr.Length <=0)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objServices = new clsHRPTableService();
            try
            {

                DataTable dtbValue = new DataTable();
                for (int i1 = 0; i1 < p_objParamArr.Length; i1++)
                {
                    IDataParameter[] objDPArr = null;
                    objServices.CreateDatabaseParameter(6, out objDPArr);
                    //				for(int i=0;i<objDPArr.Length;i++)
                    //					objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();

                    objDPArr[0].Value = p_objParamArr[i1].m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_objParamArr[i1].m_strInPatientDate);
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_objParamArr[i1].m_strParamDate);
                    objDPArr[3].Value = p_objParamArr[i1].m_strParamID;
                    objDPArr[4].Value = p_objParamArr[i1].m_strMonitorID;
                    objDPArr[5].Value = float.Parse(p_objParamArr[i1].m_strParamValue);

                    objDPArr[0].ParameterName = "pInPatientID";
                    objDPArr[1].ParameterName = "pInPatientDate";
                    objDPArr[2].ParameterName = "pParamDate";
                    objDPArr[3].ParameterName = "pParamID";
                    objDPArr[4].ParameterName = "pMonitorID";
                    objDPArr[5].ParameterName = "pParamValue";

                    //lngRes = objServices.lngGetDataTableWithParameters(c_strInsertParamRecordSQL,ref dtbValue,objDPArr);
                    lngRes = objServices.lngExecuteProc("SP_INSERTGEPARAMS", objDPArr);

                    if (lngRes <= 0)
                        return (long)enmOperationResult.DB_Fail;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objServices.Dispose();
            }
			return (long)enmOperationResult.DB_Succeed;
		}
	}
}
