using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.common;
 
namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    #region 处方出库按单位品种统计
    /// <summary>
    /// 处方出库按单位品种统计
    /// </summary>
    /// <param name="p_objPrincipal"></param>
    /// <param name="p_strDrugID">药房</param>
    /// <param name="p_dtmStartDate">开始日期</param>
    /// <param name="p_dtmEndDate">结束日期</param>
    /// <param name="p_dtbResult">查询结果</param>
    /// <returns></returns>
    public class clsDcl_RecipeByEachDeptReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        internal long m_lngGetRecipeByEachDeptReport(string p_strDrugID, string p_strDeptID, DateTime p_dtmStartDate, DateTime p_dtmEndDate, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsRecipeReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsRecipeReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsRecipeReportSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngRecipeByEachDeptReport( p_strDrugID, p_strDeptID, p_dtmStartDate, p_dtmEndDate, out p_dtbResult);
            return lngRes;
        }

        #region 获取领用部门
        /// <summary>
        /// 获取领用部门
        /// </summary>
        /// <param name="p_dtbVendor">领用部门数据</param>
        /// <returns></returns>
        internal long m_lngGetExportDeptForDrugStore(out DataTable p_dtbExportDept)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetExportDeptForDrugStore( out p_dtbExportDept);
            return lngRes;
        }
        #endregion
    }
    #endregion
}
