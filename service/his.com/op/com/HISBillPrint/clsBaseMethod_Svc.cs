using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using System.Collections;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.billprint
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsBaseMethod_Svc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsBaseMethod_Svc()
        {
            //
            // TODO: 在此添加构造函数逻辑
            //
        }

        #region 获取新系统参数
        /// <summary>
        /// 获取新系统参数
        /// </summary>
        /// <param name="parmcode">参数代码</param>
        /// <returns>值</returns>
        [AutoComplete]
        public string m_strGetSysparm(string parmcode)
        {
            string parmvalue = "";
            string SQL = @"select parmvalue_vchr
                                 from t_bse_sysparm 
                                where status_int = 1 
                                  and parmcode_chr = ?";
            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {

                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = parmcode;
                DataTable dt = new DataTable();
                long l = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                if (l > 0 && dt.Rows.Count > 0)
                {
                    parmvalue = dt.Rows[0][0].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                ParamArr = null;
                SQL = null;
            }

            return parmvalue;
        }
        /// <summary>
        /// 批量获取系统参数
        /// </summary>
        /// <param name="p_strParamKeyArr"></param>
        /// <param name="p_hasParamValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSysparm(string[] p_strParamKeyArr, out Hashtable p_hasParamValue)
        {
            long lngRes = 0;
            p_hasParamValue = null;
            System.Text.StringBuilder sbSub = new System.Text.StringBuilder();
            foreach (string strParamKey in p_strParamKeyArr)
            {
                sbSub.Append("'").Append(strParamKey).Append("',");
            }
            sbSub.Length = sbSub.Length - 1;
            string strSub = sbSub.ToString();
            sbSub = null;

            if (strSub == string.Empty)
            {
                return lngRes;
            }

            string SQL = @"select parmcode_chr, parmvalue_vchr
                                 from t_bse_sysparm 
                                where status_int = 1 
                                  and parmcode_chr in( " + strSub + " )";
            sbSub = null;

            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                DataTable dt = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    p_hasParamValue = new Hashtable();
                    foreach (DataRow dtrTemp in dt.Rows)
                    {
                        p_hasParamValue.Add(dtrTemp["parmcode_chr"].ToString().Trim(), dtrTemp["parmvalue_vchr"].ToString().Trim());
                    }
                }
                dt = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                SQL = null;
            }

            return lngRes;
        }

        /// <summary>
        /// 获取系统配置
        /// </summary>
        /// <param name="p_strParmCode"></param>
        /// <param name="p_intVal"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSysSetting(string p_strParmCode, ref int p_intVal)
        {
            long lngRes = 0;
            p_intVal = -1;
            string strSQL = @"select a.setname_vchr,a.setdesc_vchr,a.setstatus_int
  from t_sys_setting a
 where a.setid_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strParmCode;
                DataTable dtbTemp = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbTemp, objDPArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbTemp != null)
                {
                    if (dtbTemp.Rows.Count > 0)
                        p_intVal = Convert.ToInt32(dtbTemp.Rows[0]["setstatus_int"].ToString());
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 批量获取系统功能设置
        /// </summary>
        /// <param name="p_strParamKeyArr"></param>
        /// <param name="p_hasParamValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSysSetting(string[] p_strParamKeyArr, out Hashtable p_hasParamValue)
        {
            long lngRes = 0;
            p_hasParamValue = null;
            System.Text.StringBuilder sbSub = new System.Text.StringBuilder();
            foreach (string strParamKey in p_strParamKeyArr)
            {
                sbSub.Append("'").Append(strParamKey).Append("',");
            }
            sbSub.Length = sbSub.Length - 1;
            string strSub = sbSub.ToString();
            sbSub = null;

            if (strSub == string.Empty)
            {
                return lngRes;
            }

            string SQL = @"select setid_chr, setstatus_int from t_sys_setting where setid_chr in( " + strSub + " )";
            sbSub = null;

            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                DataTable dt = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    p_hasParamValue = new Hashtable();
                    foreach (DataRow dtrTemp in dt.Rows)
                    {
                        p_hasParamValue.Add(dtrTemp["setid_chr"].ToString().Trim(), dtrTemp["setstatus_int"].ToString().Trim());
                    }
                }
                dt = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                SQL = null;
            }

            return lngRes;
        }
        #endregion

        #region 获取医院名称
        /// <summary>
        /// 获取医院名称
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetHospitalName(ref string s)
        {
            long lngRes = 0;
            DataTable dtbResult = null;
            string strSQL = @"select hospital_name_chr from t_bse_hospitalinfo";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult != null)
                {
                    if (dtbResult.Rows.Count > 0)
                    {
                        s = dtbResult.Rows[0]["hospital_name_chr"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
    }
}
