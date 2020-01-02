using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// ҩ�����������
    /// </summary>
    public class clsDcl_StorageShelf : com.digitalwave.GUI_Base.clsDomainController_Base
    {

        #region ��ȡҩƷ����
        /// <summary>
        /// ��ȡҩƷ����
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

        #region ��������
        internal long m_lngSaveStorageShelf(DataTable m_dtbModify)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRacksetSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRacksetSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageRacksetSVC));

            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngSaveStorageShelf(m_dtbModify);
            return lngRes;
        }
        #endregion

        #region ��ȡ����
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

        #region ��ȡҩƷ�������Ϣ
        /// <summary>
        /// ��ȡҩƷ�������Ϣ
        /// </summary>
        /// <param name="p_blnIsDrugStore">�Ƿ�ҩ��ʹ��</param>
        /// <param name="p_strAssistCode">��ѯ����</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_dtbMedicine">���ؽ��</param>
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
