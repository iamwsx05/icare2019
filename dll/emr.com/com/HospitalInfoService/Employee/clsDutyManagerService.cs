using System;
using System.EnterpriseServices;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;

namespace com.digitalwave.DutyManagerService
{
	/// <summary>
	/// 职务管理的中间件�?
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsDutyManagerService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 获取员工的职务信�?
		/// </summary>
		/// <param name="p_strEmployeeID">员工�?/param>
		/// <param name="p_strResultXml">返回的结�?/param>
		/// <param name="p_intResultRows">记录的数�?/param>
		/// <returns>
		/// 操作结果�?
		/// 0：失败�?
		/// 1：成功�?
		/// </returns>
		[AutoComplete]
		public long m_lngGetEmployeeDutyInfo( string p_strEmployeeID,ref string p_strResultXml,ref int p_intResultRows)
		{
			long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            { 
                string strSQL = @"select di.dutyid,di.dutyname,di.positiveorbear
									from employee_duty ed,dutyinfo di
									where 
									ed.dutyid = di.dutyid
									and ed.employeeid = ?
									and ed.end_date_duty_emp = ?
									and di.status = 0";

                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strEmployeeID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = new DateTime(1900, 1, 1);

                lngRes = objTabService.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			//返回
			return lngRes;
		}
	}
}