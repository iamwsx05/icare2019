using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore
{
    class clsDcl_InStorageMedicineWithdrawDetailReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {

        internal long m_lngGetMedicineInnerWithdrawDetailDataReport(string instorageid_vchr, out DataTable dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorageStatisticsReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorageStatisticsReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorageStatisticsReportSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetMedicineInnerWithdrawDetailDataReport(instorageid_vchr, out dtbResult);
            return lngRes;
        }
        #region 获取内退单报表类型


        /// <summary>
        /// 获取内退单报表类型


        /// </summary>
        internal long m_lngGetPrinType(out int p_intCommitFolw)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetSysSetting(  "5020", out p_intCommitFolw);
            return lngRes;
        }
        #endregion

    }
}
