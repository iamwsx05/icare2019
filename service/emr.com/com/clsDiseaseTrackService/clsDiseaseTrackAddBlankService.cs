using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;


namespace com.digitalwave.DiseaseTrackService
{

	/// <summary>
	/// 病程记录增加空行的中间类
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsDiseaseTrackAddBlankService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsDiseaseTrackAddBlankService()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region SQL定义	

		// 从CommonUseValue中获取指定的表单。
		private const string c_strAddNewRecordSQL=@"insert into diseasetrackaddblank (inpatientid,inpatientdate,opendate,rowsnumber)values(?,?,?,?)";
		private const string c_strDeleteRecordSQL=@"delete from diseasetrackaddblank where inpatientid = ? and inpatientdate=? and opendate = ? and rowsnumber =?";
		private const string c_strUpdateRecordSQL=@"update diseasetrackaddblank set rowsnumber=? where inpatientid=? and inpatientdate=? and opendate = ?";
		#endregion SQL定义

		#region  空白行的记录操作

		/// <summary>
		/// 获取空行记录
		/// </summary>
		[AutoComplete]
		public long m_lngGetAddBlankValue(
			string p_strInPatientID,DateTime p_dtmInPatientDate,out System.Data.DataTable p_dtbResult)
		{			
			p_dtbResult = null;

//			long lngCheckRes = new com.digitalwave.PrivilegeSystemService.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDiseaseTrackService","m_lngGetAddBlankValue");
//			if(lngCheckRes <= 0)
//				return lngCheckRes;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                 string strSQL = @"select inpatientid, inpatientdate, opendate, rowsnumber
  from diseasetrackaddblank
 where inpatientid = ?
   and inpatientdate = ?";

                 IDataParameter[] objDPArr = null;
                 objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                 objDPArr[0].Value = p_strInPatientID;
                 objDPArr[1].DbType = DbType.DateTime;
                 objDPArr[1].Value = p_dtmInPatientDate;

                 lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
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
			return lngRes;
		}
		/// <summary>
		/// 添加空行记录
		/// </summary>
		[AutoComplete]
		public  long m_lngAddNewBlankRecord2DB(
			clsTrackRecordContent p_objRecordContent)
		{
			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID == null || p_objRecordContent.m_strRowsNumber==null )
				return (long)enmOperationResult.Parameter_Error;			
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                 IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                objDPArr[3].Value = p_objRecordContent.m_strRowsNumber;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
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
		/// 修改空行记录
		/// </summary>
		[AutoComplete]
		public  long m_lngModifyBlankRecord2DB(clsTrackRecordContent p_objRecordContent)
		{
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null || p_objRecordContent.m_strRowsNumber == null)
				return (long)enmOperationResult.Parameter_Error;			
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                 //获取IDataParameter数组
                System.Data.IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRowsNumber;
                objDPArr[1].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmOpenDate;
                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateRecordSQL, ref lngEff, objDPArr);
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
		/// 删除空行记录
		/// </summary>
		[AutoComplete]
		public  long m_lngDeleteBlankRecord2DB(
			clsTrackRecordContent p_objRecordContent)
		{
			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null)
				return (long)enmOperationResult.Parameter_Error;
						
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                 IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                objDPArr[3].Value = p_objRecordContent.m_strRowsNumber;
                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

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
			return lngRes;
		}	

		#endregion
	}
	
}