using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier;

namespace iCare.Lis.Self.ReportPrint.Svc
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsReportPrintUpdate_Svc : clsMiddleTierBase
    {
        #region 写报告打印状态
        /// <summary>
        /// 写报告打印状态
        /// </summary>
        /// <param name="m_strAPPLICATION_ID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthWriteReportPrintState(string m_strAPPLICATION_ID)
        {
            string strSQL = null;

            IDataParameter[] objDPArr = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = m_strAPPLICATION_ID;
            long lngRes = 0;

            strSQL = @"update t_opr_lis_app_report t
   set t.report_print_chr = 1,
       t.report_print_dat = ?,
       t.modify_dat       = sysdate
 where t.application_id_chr = ?
   and status_int = 2";
            objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].DbType = DbType.DateTime;
            objDPArr[0].Value = System.DateTime.Now;
            objDPArr[1].Value = m_strAPPLICATION_ID;
            long flag = 0;
            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref flag, objDPArr);
            flag = 0;
            objHRPSvc.Dispose();
            objDPArr = null;
            return lngRes;
        }
        #endregion
    }
}
