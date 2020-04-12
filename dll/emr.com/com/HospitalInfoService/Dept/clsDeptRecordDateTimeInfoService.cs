using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;


namespace com.digitalwave.DepartmentManagerService
{
	/// <summary>
	/// Summary description for clsDeptRecordDateTimeInfo.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsDeptRecordDateTimeInfoService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 获取指定部门的记录日期信息
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDeptID"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecordDateTimeInfo( string p_strDeptID, out DataTable p_dtbResult)
		{
			p_dtbResult = new DataTable();
            if (p_strDeptID == null)
                return -1;
            string strSQL = @"select deptid, formname, datetimeflag
  from deptrecorddatetimeinfo
 where deptid ='" + p_strDeptID + "'";
			long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {


                lngRes = objTabService.DoGetDataTable(strSQL, ref p_dtbResult);

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
