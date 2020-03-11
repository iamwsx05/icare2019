using System;
using System.Collections;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;



namespace com.digitalwave.clsMedicalParameterCollateServ
{
    /// <summary>
    /// 检验项目参照中间件
    /// </summary>

    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsMedicalParameterCollateServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 得到数据
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_dtRecords"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetData(string p_strSQL, ref DataTable p_dtRecords)
        {
            p_dtRecords = null;
            long lngRes = -1;

            if (p_strSQL.Trim().Length <= 0)
                return lngRes;

            clsHRPTableService objHRPServer = new clsHRPTableService();

            try
            {
                lngRes = objHRPServer.lngGetDataTableWithoutParameters(p_strSQL, ref p_dtRecords);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }

            return lngRes;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="p_arlSQL"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngExecSQL(System.Collections.Generic.List<string> p_arlSQL)
        {
            long lngRes = -1;

            clsHRPTableService objHRPServer = new clsHRPTableService();

            try
            {
                for (int i = 0; i < p_arlSQL.Count; i++)
                {
                    if (p_arlSQL[i].Trim().Length > 0)
                    {
                        lngRes = objHRPServer.DoExcute(p_arlSQL[i]);
                        if (lngRes <= 0)
                        {
                            System.EnterpriseServices.ContextUtil.SetAbort();
                            return lngRes;
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
            return lngRes;
        }
    }
}
