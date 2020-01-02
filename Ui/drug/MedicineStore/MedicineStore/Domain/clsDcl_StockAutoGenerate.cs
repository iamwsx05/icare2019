using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore
{
    class clsDcl_StockAutoGenerate : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 获取明细数据
        internal long m_lngGetDetailForGenerate(string m_strStorageID, out DataTable dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetDetailForGenerate(m_strStorageID, out dtbResult);
            return lngRes;
        }
        #endregion
    }
}
