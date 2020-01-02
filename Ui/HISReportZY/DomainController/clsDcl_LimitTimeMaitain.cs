using System;
using System.Collections.Generic;
using System.Text; 
using System.Data;

namespace com.digitalwave.iCare.gui.HIS.Reports
{
    public class clsDcl_LimitTimeMaitain : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 获取专业组
        /// <summary>
        /// 获取专业组
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetAllCheckSpec(out DataTable dtbResult)
        {
            long lngRes = 0;

            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                dtbResult = (new weCare.Proxy.ProxyReport()).Service.GetGategoryType();
            //}

            return lngRes;
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

        #region 获取申请单元时间维护
        /// <summary>
        /// lngGetLimitTime
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <param name="applyunitid"></param>
        /// <returns></returns>
        public long lngGetLimitTime(out DataTable dtbResult, string applyunitid)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.GetLimitTime(out dtbResult, applyunitid);
            //}
        }

        #endregion

        #region  保存申请单元时间
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long lngSaveLimitTime(DataTable dt)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.SaveLimitTime(dt);
            //}
        }
        #endregion
        
        #region 删除申请单元时间
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applyunitid"></param>
        /// <returns></returns>
        public long lngDeleteLimitTime(string applyunitid)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.DeleteLimitTime(applyunitid);
            //}
        }
        #endregion
    }
}
