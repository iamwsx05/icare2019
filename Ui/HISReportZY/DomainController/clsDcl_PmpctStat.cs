using System;
using System.Collections.Generic;
using System.Text;
using System.Data; 

namespace com.digitalwave.iCare.gui.HIS.Reports
{
    public class clsDcl_PmpctStat : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_PmpctStat()
        {

        }

        #region 免费母婴阻断检测工作情况记录
        /// <summary>
        /// 免费母婴阻断检测工作情况记录
        /// </summary>
        /// <param name="dteStart"></param>
        /// <param name="dteEnd"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetPmpcStat(string dteStart, string dteEnd,string patType,string applyUnitId, out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.GetPmpctStat(dteStart, dteEnd,patType, applyUnitId, out dtbResult);
            //}
        }
        #endregion

        #region 免费母婴阻断阳性结果汇总
        /// <summary>
        /// 免费母婴阻断阳性结果汇总
        /// </summary>
        /// <param name="dteStart"></param>
        /// <param name="dteEnd"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetPmpcDetail(string dteStart, string dteEnd, string patType,string applyUnitId, out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.GetPmpctDetail(dteStart, dteEnd, patType, applyUnitId, out dtbResult);
            //}
        }
        #endregion

        #region 获取专业组
        /// <summary>
        /// 获取专业组
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long GetAllCheckSpec(out DataTable dtbResult)
        {
            long lngRes = 0;

            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
            dtbResult = (new weCare.Proxy.ProxyReport()).Service.GetGategoryType();
            return lngRes;
            //}
        }
        #endregion

        #region 获取所有检验项目
        /// <summary>
        /// 获取所有检验项目
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetAllCheckItem(out DataTable dtbResult, string groupId)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
            return (new weCare.Proxy.ProxyReport()).Service.GetAllCheckItemCpy(out dtbResult, groupId);
            //}
        }
        #endregion

        #region 名称检索检验项目
        /// <summary>
        /// 名称检索检验项目
        /// </summary>
        /// <param name="strTempName"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetCheckItemByName(string strTempName, string groudId, out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
            return (new weCare.Proxy.ProxyReport()).Service.GetCheckItemByNameCpy(strTempName, groudId, out dtbResult);
            //}
        }
        #endregion
    }
}
