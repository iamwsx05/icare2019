using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data; 

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    #region 处方出库按身份统计
    /// <summary>
    /// 处方出库按身份统计
    /// </summary>
    /// <param name="p_objPrincipal"></param>
    /// <param name="p_strDrugID">药房</param>
    /// <param name="p_dtmStartDate">开始日期</param>
    /// <param name="p_dtmEndDate">结束日期</param>
    /// <param name="p_dtbResult">查询结果</param>
    /// <returns></returns>
    public class clsDcl_RecipeByIdentityReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        internal long m_lngGetRecipeByIdentity(string p_strDrugID, DateTime p_dtmStartDate, DateTime p_dtmEndDate, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsRecipeReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsRecipeReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsRecipeReportSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetRecipeByIdentity(  p_strDrugID, p_dtmStartDate, p_dtmEndDate, out p_dtbResult);
            return lngRes;
        }
    }
    #endregion
}
