using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.middletier.RIS
{
    /// <summary>
    /// RIS工具集合类
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]    
    public class clsRISUtility_Svc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 在数据库中注册模板关联信息
        /// </summary>
        /// <param name="p_strFormID">需要注册模板的窗体名称</param>
        /// <param name="p_strControlID">控件名称</param>
        /// <param name="p_strControlDesc">控件描述</param>       
        /// <returns></returns>
        [AutoComplete]
        public long m_lngRegisterTemplate(string p_strFormID, string p_strControlID, string p_strControlDesc)
        {
            long lngRes = 0;
            string strSql = @"insert into gui_control (form_id, control_id, control_desc, order_no) 
values (?,?,?,?)";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                int order = 0;
                lngRes = m_lngGetOrder(p_strFormID, out order);
                if (lngRes < 0)
                {
                    return lngRes;
                }
                lngRes = 0;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strFormID;
                objDPArr[1].Value = p_strControlID;
                objDPArr[2].Value = p_strControlDesc;
                objDPArr[3].Value = order;
                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSql, ref lngRecEff, objDPArr);                
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }            
            return lngRes;
        }

        [AutoComplete]
        public bool m_blnIsRegistered(string p_strFormID)
        {
            long lngRes = 0;
            int count = 0;
            string strSql = @"select count(t.form_id) from gui_control t where t.form_id = ?";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtValue = null;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strFormID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtValue, objDPArr);
                if (lngRes > 0 && dtValue.Rows.Count > 0)
                {
                    count = Convert.ToInt32(dtValue.Rows[0][0]);
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return false;
        }

        [AutoComplete]
        public long m_lngGetOrder(string p_strFormID, out int p_intOrder)
        {
            long lngRes = 0;
            p_intOrder = 0;
            string strSql = @"select max(t.order_no) as max_id from gui_control t where t.form_id = ?";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtValue = null;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);                
                objDPArr[0].Value = p_strFormID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtValue, objDPArr);
                if (lngRes > 0 && dtValue.Rows.Count > 0)
                {
                    if (dtValue.Rows[0][0] is DBNull)
                    {
                        p_intOrder = 1;                        
                    }
                    else
                    {
                        p_intOrder = Convert.ToInt32(dtValue.Rows[0][0]) + 1;
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }
        ///// <summary>
        ///// 读取配置参数
        ///// </summary>
        ///// <param name="TypeID"></param>
        ///// <param name="?"></param>
        ///// <returns></returns>
        //[AutoComplete]
        //public long m_getSystemConfig(int TypeID, out string strSystemConfig)
        //{
        //    long lngRes = 0;
        //    strSystemConfig = "";
        //    string strSQL = "select PARMVALUE_VCHR from t_bse_sysparm a where parmcode_chr=?";
        //    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //    System.Data.IDataParameter[] objDPArr = null;
        //    objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
        //    objDPArr[0].Value = TypeID;
        //    DataTable objDataTable = new DataTable();
        //    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref  objDataTable, objDPArr);
        //    int objDataRowCount = objDataTable.Rows.Count;
        //    if (lngRes > 0 && objDataRowCount > 0)
        //    {
        //        strSystemConfig = Convert.ToString(objDataTable.Rows[0]["PARMVALUE_VCHR"]);
        //    }
        //    return lngRes;
        //}
    }
}
