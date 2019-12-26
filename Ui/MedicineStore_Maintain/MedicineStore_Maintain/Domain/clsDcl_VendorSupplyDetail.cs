using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// ��ҩ��λ��ҩ��ϸ
    /// </summary>
    class clsDcl_VendorSupplyDetail : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        /// <summary>
        /// ��ҩ��λ��ҩ��ϸ
        /// </summary>
        public clsDcl_VendorSupplyDetail()
        {
        }

        #region ��ѯ��ҩ��λ��ҩ��ϸ
        /// <summary>
        /// ��ѯ��ҩ��λ��ҩ��ϸ
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_dtmBegin">��ʼ����</param>
        /// <param name="p_dtmEnd">��������</param>
        /// <param name="p_strVendorID">��ҩ��λID</param>
        /// <param name="p_intMedicineSetID">ҩƷ����</param>
        /// <param name="p_dtbData">ҩƷ�����ϸ</param>
        /// <returns></returns>
        internal long m_lngStatistics(string p_strStorageID, DateTime p_dtmBegin, DateTime p_dtmEnd, string p_strVendorID, int p_intMedicineSetID, out DataTable p_dtbData)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsVendorSupplyDetaiSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsVendorSupplyDetaiSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsVendorSupplyDetaiSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngStatistics(p_strStorageID, p_dtmBegin, p_dtmEnd, p_strVendorID, p_intMedicineSetID, out p_dtbData);
            return lngRes;
        }
        #endregion

        #region  ��òֿ�����
        /// <summary>
        /// ��òֿ�����
        /// </summary>
        /// <param name="p_strStoreRoomID">�ֿ�ID</param>
        /// <param name="p_strStoreRoomName">�ֿ�����</param>
        /// <returns></returns>
        internal long m_lngGetStoreRoomName(string p_strStoreRoomID, out string p_strStoreRoomName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetStoreRoomName(p_strStoreRoomID, out p_strStoreRoomName);
            return lngRes;
        }
        #endregion

        #region ��ù�Ӧ��
        /// <summary>
        /// ��ù�Ӧ��
        /// </summary>
        /// <param name="p_dtbVendor">��Ӧ��</param>
        /// <returns></returns>
        internal long m_lngGetVendor(out DataTable p_dtbVendor)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetVendor(out p_dtbVendor);
            return lngRes;
        }
        #endregion
    }
}
