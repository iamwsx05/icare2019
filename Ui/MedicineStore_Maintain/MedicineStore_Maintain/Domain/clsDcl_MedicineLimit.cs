using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    public class clsDcl_MedicineLimit : com.digitalwave.GUI_Base.clsController_Base
    {
        public long m_lngGetAllMedicine(string p_strStorageID, out DataTable p_dtbMedicine)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineLimitSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineLimitSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineLimitSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.clsMedicineLimitSVC_m_lngGetAllMedicine(p_strStorageID, out p_dtbMedicine);
            return lngRes;
        }

        public long m_lngSaverMedicine(clsMedicineLimit_VO[] p_objLimit)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineLimitSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineLimitSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineLimitSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngSaverMedicine(p_objLimit);
            return lngRes;
        }
        #region 获取药品最基本信息
        /// <summary>
        /// 获取药品最基本信息
        /// </summary>
        /// <param name="p_strAssistCode">查询条件</param>
        /// <param name="p_strStorageID">仓库ID</param>
        /// <param name="p_dtbMedicine">返回结果</param>
        /// <returns></returns>
        public long m_lngGetBaseMedicine(string p_strAssistCode, string p_strStorageID, out DataTable p_dtbMedicine)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecordSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecordSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecordSVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetBaseMedicine(p_strAssistCode, p_strStorageID, out p_dtbMedicine);
            return lngRes;
        }
        #endregion
    }
}
