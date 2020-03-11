using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using SQLHelper = com.digitalwave.emr.AssistModuleSev.SQLHelper;

namespace com.digitalwave.emr.AssistModuleSev
{
    /// <summary>
    /// 作废记录查询中间件
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsInactiveViewServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 获取所有窗体
        /// </summary>
        /// <param name="p_objPrincipal">权限</param>
        /// <param name="p_objValueArr">返回列表</param>
        /// <returns>操作结果(成功 > 0)</returns>
        [AutoComplete]
        public long m_lngGetAllView( out clsInactiveFormInfo_VO[] p_objValueArr)
        {
            p_objValueArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsInactiveViewServ", "m_lngGetAllView");
                //objPrivilege = null;
                //if (lngCheckRes <= 0)
                //    return lngCheckRes; 

                DataTable dtResult = new DataTable();
                //执行
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(SQLHelper::clsSqlStringHelper.s_StrGetQueryT_AID_EMR_FORM_AllInActiveView, ref dtResult);
                int intRowCount = dtResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objValueArr = new clsInactiveFormInfo_VO[intRowCount];
                    DataRow objRow = null;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objRow = dtResult.Rows[i];
                        clsInactiveFormInfo_VO objValue = new clsInactiveFormInfo_VO();
                        objValue.m_StrDll = objRow["dllname_vchr"].ToString();
                        objValue.m_StrFormId = objRow["formid_int"].ToString();
                        objValue.m_StrFormName = objRow["formdesc_vchr"].ToString();
                        objValue.m_StrFormOperaClass = objRow["opraclassname_vchr"].ToString();
                        objValue.m_StrFormOperation = objRow["opramethodname_vchr"].ToString();
                        objValue.m_StrFormType = objRow["formnamespace_vchr"].ToString() + "." + objRow["formname_vchr"].ToString();
                        objValue.m_StrFromState = objRow["formstate_int"].ToString();
                        p_objValueArr[i] = objValue;
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        /// <summary>
        /// 根据窗体类类型返回窗体配置信息
        /// </summary>
        /// <param name="p_objPrincipal">权限</param>
        /// <param name="p_strNameSpace">命名空间</param>
        /// <param name="p_strClassName">类名称</param>
        /// <param name="p_objValue">配置信息</param>
        /// <returns>操作结果(成功 > 0)</returns>
        [AutoComplete]
        public long m_lngGetOneView( string p_strNameSpace,string p_strClassName, out clsInactiveFormInfo_VO p_objValue)
        {
            p_objValue = null;
            if (string.IsNullOrEmpty(p_strNameSpace) || string.IsNullOrEmpty(p_strClassName)) return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsInactiveViewServ", "m_lngGetAllView");
                //objPrivilege = null;
                //if (lngCheckRes <= 0)
                //    return lngCheckRes; 

                IDataParameter[] objParamArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objParamArr);
                objParamArr[0].Value = p_strNameSpace;
                objParamArr[1].Value = p_strClassName;

                DataTable dtResult = new DataTable();
                //执行
                lngRes = objHRPServ.lngGetDataTableWithParameters(SQLHelper::clsSqlStringHelper.s_StrGetQueryT_AID_EMR_FORM_OneInActiveView, ref dtResult, objParamArr);
                int intRowCount = dtResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objValue = new clsInactiveFormInfo_VO();
                    DataRow objRow = dtResult.Rows[0];
                    p_objValue.m_StrDll = objRow["dllname_vchr"].ToString();
                    p_objValue.m_StrFormId = objRow["formid_int"].ToString();
                    p_objValue.m_StrFormName = objRow["formdesc_vchr"].ToString();
                    p_objValue.m_StrFormOperaClass = objRow["opraclassname_vchr"].ToString();
                    p_objValue.m_StrFormOperation = objRow["opramethodname_vchr"].ToString();
                    p_objValue.m_StrFormType = objRow["formnamespace_vchr"].ToString() + "." + objRow["formname_vchr"].ToString();
                    p_objValue.m_StrFromState = objRow["formstate_int"].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
    }
}
