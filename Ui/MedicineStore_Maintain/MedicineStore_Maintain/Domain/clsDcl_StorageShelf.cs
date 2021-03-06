using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// 药库库存域控制类
    /// </summary>
    public class clsDcl_StorageShelf : com.digitalwave.GUI_Base.clsDomainController_Base
    {

        #region 获取药品类型
        /// <summary>
        /// 获取药品类型
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetResultByConditionMedicineType(out clsValue_MedicineType_VO[] p_objResultArr)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRackset_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRackset_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRackset_Supported_SVC));

            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetMedicineTypeData(out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region 保存数据
        internal long m_lngSaveStorageShelf(DataTable m_dtbModify)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRacksetSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRacksetSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRacksetSVC));

            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngSaveStorageShelf(m_dtbModify);
            return lngRes;
        }
        #endregion

        #region 获取数据
        internal long m_mthGetStorageDetailData(string p_strStorageID, string p_strMedicineID, string p_strAssistCode, string p_strMedicineTypeID,
            out DataTable dtbResult, List<string> lstMedicineType)
        {
            long lngRes = 0;
            dtbResult = new DataTable();
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRackset_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRackset_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRackset_Supported_SVC));

            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetStorageShelfData(p_strStorageID, p_strMedicineID, p_strAssistCode, p_strMedicineTypeID, lstMedicineType, out dtbResult);
            return lngRes;
        }
        #endregion

        #region 获取药品最基本信息
        /// <summary>
        /// 获取药品最基本信息
        /// </summary>
        /// <param name="p_blnIsDrugStore">是否药库使用</param>
        /// <param name="p_strAssistCode">查询条件</param>
        /// <param name="p_strStorageID">仓库ID</param>
        /// <param name="p_dtbMedicine">返回结果</param>
        /// <returns></returns>
        public long m_lngGetBaseMedicine(bool p_blnIsDrugStore, string p_strAssistCode, string p_strStorageID, out DataTable p_dtbMedicine)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecord_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecord_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecord_Supported_SVC));
            if (p_blnIsDrugStore)
            {
                lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetBaseMedicineForDrugStoreByStorageID(p_strAssistCode, p_strStorageID, out p_dtbMedicine);
            }
            else
            {
                lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetBaseMedicine(p_strAssistCode, p_strStorageID, out p_dtbMedicine);
            }
            return lngRes;
        }
        #endregion

        internal void m_mthGetStorageShelfInfo(string p_strStorageID, out DataTable p_dtbShelfInfo)
        {
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRackset_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRackset_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRackset_Supported_SVC));

            (new weCare.Proxy.ProxyDrug02()).Service.m_mthGetStorageShelfInfo(p_strStorageID, out p_dtbShelfInfo);
        }
    }
}
