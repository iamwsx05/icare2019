using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    class clsDcl_InOutReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ��ȡ�ֿ�����
        /// <summary>
        /// ��ȡ�ֿ�����
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_strStorageName"></param>
        /// <returns></returns>
        internal long m_lngGetStoreRoomName(string p_strStorageID, out string p_strStorageName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMiddletier_InStorageMedicineWithdraw_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMiddletier_InStorageMedicineWithdraw_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMiddletier_InStorageMedicineWithdraw_Supported_SVC));

            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetStoreRoomName(p_strStorageID, out p_strStorageName);

            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ�������Ϣ
        /// <summary>
        /// ��ȡҩƷ�������Ϣ
        /// </summary>
        /// <param name="p_blnIsDrugStore">ҩ��ʹ��</param>
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


        #region ��ȡ����������ҩ�⣩
        /// <summary>
        /// ��ȡ����������ҩ�⣩
        /// </summary>
        /// <param name="p_blnCombine">�Ƿ�Ʒ�ֲ�ѯ</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_dtmBegin">��ʼʱ��</param>
        /// <param name="p_dtmEnd">����ʱ��</param>
        /// <param name="p_strMedType">ҩƷ����</param>
        /// <param name="p_strMedicine">ҩƷID</param>
        /// <param name="p_intFilter">����</param>        
        /// <param name="p_blnShowNoAmount">�Ƿ���ʾ����</param>
        /// <param name="p_dtbReport">�����ϸ����</param>
        /// <returns></returns>
        internal long m_lngGetInOutDetail(bool p_blnCombine, string p_strStorageID, DateTime p_dtmBegin, DateTime p_dtmEnd, string p_strMedType, string p_strMedicine, int p_intFilter, bool p_blnShowNoAmount, out DataTable p_dtbReport)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetInOutDetail(p_blnCombine, p_strStorageID, p_dtmBegin, p_dtmEnd, p_strMedType, p_strMedicine, p_intFilter, p_blnShowNoAmount, out p_dtbReport);
            return lngRes;
        }
        #endregion 

        internal long m_lngGetTypeNameByID(int p_intFlag, string p_strInType, out string m_strTypeName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetTypeNameByID(p_intFlag, p_strInType, out m_strTypeName);
            return lngRes;
        }

        internal long m_lngGetDeptIDForStore(string m_strStorageID, out string m_strDeptID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetDeptIDForStore(m_strStorageID, out m_strDeptID);
            return lngRes;
        }

        internal long m_lngGetDrugStoreName(string m_strStorageID, out string m_strRoomName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetDrugStoreRoomName(m_strStorageID, out m_strRoomName);
            return lngRes;
        }

        #region ��ȡ����������ҩ��ʹ�ã�
        /// <summary>
        /// ��ȡ����������ҩ��ʹ�ã�
        /// </summary>
        /// <param name="p_blnCombine">�Ƿ�Ʒ�ֲ�ѯ</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_dtmBegin">��ʼʱ��</param>
        /// <param name="p_dtmEnd">����ʱ��</param>
        /// <param name="p_strMedTypeCode">ҩƷ����</param>
        /// <param name="p_strMedicine">ҩƷID</param>
        /// <param name="p_intFilter">�������ID</param>
        /// <param name="p_blnShowNoAmount">�Ƿ���ʾ����</param>
        /// <param name="p_dtbReport">�����ϸ����</param>
        /// <returns></returns>
        internal long m_lngGetInOutDetailForDrugStore(bool p_blnCombine, string p_strStorageID, DateTime p_dtmBegin, DateTime p_dtmEnd, string p_strMedTypeCode, string p_strMedicine, int p_intFilter, bool p_blnShowNoAmount, out DataTable p_dtbReport)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetInOutDetailForDrugStore(p_blnCombine, p_strStorageID, p_dtmBegin, p_dtmEnd, p_strMedTypeCode, p_strMedicine, p_intFilter, p_blnShowNoAmount, out p_dtbReport);
            return lngRes;
        }
        #endregion  

        internal long m_lngGetDeptIDByStoreID(string p_strInstorageType, out string p_strDeptId)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetDeptIDForStore(p_strInstorageType, out p_strDeptId);
            return lngRes;
        }

        internal long m_lngGetRoomid(out DataTable dtTemp)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsOutStorage_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsOutStorage_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsOutStorage_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetRoomid(out dtTemp);
            return lngRes;
        }

        internal long m_lngGetStoreName(string m_strStorageID, out string m_strRoomName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetStorageRoomName(m_strStorageID, out m_strRoomName);
            return lngRes;
        }
    }
}
