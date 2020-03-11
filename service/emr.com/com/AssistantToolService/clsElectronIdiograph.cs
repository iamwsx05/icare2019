using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using com.digitalwave.iCare.middletier.HRPService;
using System.Collections;
using System.EnterpriseServices;

namespace com.digitalwave.AssistantToolService
{
    /// <summary>
    /// 电子签名中间件
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsElectronIdiograph : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsElectronIdiograph()
        {

        }

        /// <summary>
        /// 根据签名关键字得到签名结果(一条记录)
        /// </summary>
        /// <param name="p_strIdiographKey">签名关键字</param>
        /// <param name="p_strGetIdiographResult">签名结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetIdiographResult(string p_strIdiographKey, ref string p_strIdiographResult)
        {
            DataTable dtRecord = null;

            string SQL = "";
            long lngRet = -1;
            clsHRPTableService objTabService = new clsHRPTableService();
            SQL = "select sign_data_vchr,sign_dat from t_crt_opr_sign_data where sign_id_vchr = ?";

            IDataParameter[] objDPArr = null;
            objTabService.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strIdiographKey.Replace("'", "''");

            lngRet = objTabService.lngGetDataTableWithParameters(SQL, ref dtRecord, objDPArr);

            if (lngRet < 0)
            {
                //objTabService.Dispose();
                return lngRet;
            }
            if (dtRecord != null && dtRecord.Rows.Count > 0)
                p_strIdiographResult = dtRecord.Rows[0]["SIGN_DATA_VCHR"].ToString();
            //objTabService.Dispose();
            return lngRet;
        }

        /// <summary>
        /// 根据签名关键字得到签名结果(一批记录)
        /// </summary>
        /// <param name="p_strIdiographKey">签名关键字</param>
        /// <param name="p_strGetIdiographResult">签名结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetIdiographResultArry(string p_strIdiographKey, ref DataTable p_dtIdiographResult)
        {
            string SQL = "";
            long lngRet = -1;
            clsHRPTableService objTabService = new clsHRPTableService();
            SQL = "select sign_data_vchr,sign_dat,sign_id_vchr from t_crt_opr_sign_data where sign_id_vchr like '%" + p_strIdiographKey.Replace("'", "''") + "%'";

            lngRet = objTabService.DoGetDataTable(SQL, ref p_dtIdiographResult);
            //objTabService.Dispose();
            return lngRet;
        }

        /// <summary>
        /// 保存签名结果(一条记录)
        /// </summary>
        /// <param name="p_strIdiographKey">签名关键字</param>
        /// <param name="p_strGetIdiographResult">签名结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveIdiographResult(string p_strIdiographKey, string p_strGetIdiographResult)
        {
            string SQL = "";
            long lngRet = -1;
            long lngRetCount = 0;

            //			SQL = "insert into T_CRT_OPR_SIGN_DATA (SIGN_ID_VCHR,SIGN_DAT,SIGN_DATA_VCHR) " + 
            //				  "values ('" + p_strIdiographKey.Replace("'","''") + "',sysdate,'" + p_strGetIdiographResult.Replace("'","''") + "')";

            SQL = @"insert into t_crt_opr_sign_data (sign_id_vchr,sign_dat,sign_data_vchr)  
				  values (?,sysdate,?)";
            clsHRPTableService objTabService = new clsHRPTableService();

            IDataParameter[] objDPArr;
            objTabService.CreateDatabaseParameter(2, out objDPArr);

            objDPArr[0].Value = p_strIdiographKey;
            objDPArr[1].Value = p_strGetIdiographResult.ToString();


            lngRet = objTabService.lngExecuteParameterSQL(SQL, ref lngRetCount, objDPArr);
            //objTabService.Dispose();
            return lngRet;
        }

        [AutoComplete]
        public long m_lngSaveIdiographResultArry(List<string> p_altKey, List<string> p_altIdiographResult)
        {
            string SQL = "";
            long lngRet = -1;
            long lngRetCount = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            if (p_altKey == null || p_altIdiographResult == null)
                return lngRet;
            IDataParameter[] objDPArr = null;
            long lngEff = -1;

            for (int i = 0; i < p_altKey.Count; i++)
            {
                string strkey = p_altKey[i].ToString();

                SQL = "delete from t_crt_opr_sign_data where sign_id_vchr = '" + strkey.Replace("'", "''") + "'";
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strkey.Replace("'", "''");

                lngRet = objTabService.lngExecuteParameterSQL(SQL, ref lngEff, objDPArr);
                if (lngRet < 0)
                    return lngRet;

                //				SQL = "insert into T_CRT_OPR_SIGN_DATA (SIGN_ID_VCHR,SIGN_DAT,SIGN_DATA_VCHR) " + 
                //					"values ('" + strkey.Replace("'","''") + "',sysdate,'" + p_altIdiographResult[i].ToString().Replace("'","''") + "')";
                SQL = @"insert into t_crt_opr_sign_data (sign_id_vchr,sign_dat,sign_data_vchr) values (?,sysdate,?)";
                objTabService.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = strkey;
                //objDPArr[1].Value=DateTime.Now;
                objDPArr[1].Value = p_altIdiographResult[i].ToString();

                lngRet = objTabService.lngExecuteParameterSQL(SQL, ref lngRetCount, objDPArr);
                if (lngRet < 0)
                {
                    //objTabService.Dispose();
                    return lngRet;
                }
            }
            //objTabService.Dispose();
            return lngRet;
        }

    }
}
