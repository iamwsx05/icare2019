using System;
using System.EnterpriseServices;
using System.Collections;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.AssistantToolService.InactiveRecorBase
{
    /// <summary>
    /// 作废记录查询
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public abstract class clsInactiveRecorBase : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public virtual long m_lngGetInactiveInfo(string p_strFormId, out clsInactiveInfo_VO p_objInactiveInfo)
        {
            p_objInactiveInfo = null;

            // 下面的表t_aid_sqlcache, t_emr_forminactiveinfo 不存在
            string strSql = @"select a.*
  from t_aid_sqlcache a, t_emr_forminactiveinfo b
 where a.sqlid_int = b.sqlid_int
   and b.formid_int = ?";
            long lngRes = -1;
            try
            {
                //IDataParameter[] objDPArr = null;
                //objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //objDPArr[0].Value = p_strFormId.Trim();

                //DataTable dtbValue = new DataTable();
                ////执行查询，填充结果到DataTable
                //long lngRes = objHRPServ.lngGetDataTableWithParameters(strSql[0], ref dtbValue, objDPArr);
                ////---------------------------------------------------
                //if (lngRes > 0 && dtbValue.Rows.Count > 0)
                //{
                //    //...
                //}
                //p_objInactiveInfo = new clsInactiveInfo_VO();
                //p_objInactiveInfo.m_strInactiveId = "1";
            }
            catch
            {
            }
            return lngRes;
        }
    }
}
