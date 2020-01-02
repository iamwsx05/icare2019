using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    class clsDcl_InstorageDetailReport : com.digitalwave.GUI_Base.clsDomainController_Base
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
        public long m_lngGetBaseMedicine(bool p_blnIsDrugStore, bool p_blnByStorageID, string p_strAssistCode, string p_strStorageID, out DataTable p_dtbMedicine)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecord_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecord_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecord_Supported_SVC));
            if (p_blnIsDrugStore)
            {
                if (p_blnByStorageID)
                {
                    lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetBaseMedicineForDrugStoreByStorageID(p_strAssistCode, p_strStorageID, out p_dtbMedicine);
                }
                else
                {
                    lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetBaseMedicineForDrugStoreByDeptID(p_strAssistCode, p_strStorageID, out p_dtbMedicine);
                }
            }
            else
            {
                lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetBaseMedicine(p_strAssistCode, p_strStorageID, out p_dtbMedicine);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ��Ӧ��
        /// <summary>
        /// ��ȡ��Ӧ��
        /// </summary>
        /// <param name="p_dtbVendor">��Ӧ������</param>
        /// <returns></returns>
        internal long m_lngGetVendor(out DataTable p_dtbVendor)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetVendor(out p_dtbVendor);
            return lngRes;
        }
        #endregion

        #region ��ȡ�����������Ϣ��
        public long m_mthGetImpExpTypeInfo(out DataTable m_dtImpExpType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_mthGetImpExpTypeInfo(out m_dtImpExpType);
            return lngRes;
        }
        #endregion

        #region ��ȡ�����ϸ����ӡ��
        /// <summary>
        /// ��ȡ�����ϸ����ӡ��
        /// </summary>
        /// <param name="p_blnCombine">�Ƿ�Ʒ�ֲ�ѯ</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_dtmBegin">��ʼʱ��</param>
        /// <param name="p_dtmEnd">����ʱ��</param>
        /// <param name="p_strVendor">��Ӧ��ID</param>
        /// <param name="p_strMedType">ҩƷ����</param>
        /// <param name="p_strMedicine">ҩƷID</param>
        /// <param name="p_strType">�������ID</param>        
        /// <param name="p_dtbReport">�����ϸ����</param>
        /// <returns></returns>
        internal long m_lngGetInstorageDetailReport(bool p_blnCombine, string p_strStorageID, DateTime p_dtmBegin, DateTime p_dtmEnd, string p_strVendor, string p_strMedType, string p_strMedicine, string p_strType, out DataTable p_dtbReport)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetInstorageDetailReport(p_blnCombine, p_strStorageID, p_dtmBegin, p_dtmEnd, p_strVendor, p_strMedType, p_strMedicine, p_strType, out p_dtbReport);
            return lngRes;
        }
        #endregion 

        /// <summary>
        /// ��ⱨ��
        /// </summary>
        /// <param name="p_blnCombine">�Ƿ�Ʒ�ֲ�ѯ</param>
        /// <param name="InstorageID">�ֿ��</param>
        /// <param name="Instoragetype">����</param>
        /// <param name="dtmBegin">��ʼʱ��</param>
        /// <param name="dtmEnd">����ʱ��</param>
        /// <param name="strMedID">ҩƷID</param>
        /// <param name="strMedType">ҩƷ����</param>
        /// <param name="strProduct">����</param>
        /// <param name="p_strBidYear">�б����</param>        
        /// <param name="p_strBidYear2">���б����</param>
        /// <param name="dtResult">���</param>
        /// <returns></returns>
        internal long m_lngRptInstorage(bool p_blnCombine, string InstorageID, string Instoragetype, DateTime dtmBegin, DateTime dtmEnd, string strMedID,
                                      string strMedType, string strProduct, string p_strBidYear, string p_strBidYear2, out DataTable dtResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorageStatisticsReport_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorageStatisticsReport_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorageStatisticsReport_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngRptInstorage(p_blnCombine, InstorageID, Instoragetype, dtmBegin, dtmEnd, strMedID, strMedType, strProduct, p_strBidYear, p_strBidYear2, out dtResult);
            return lngRes;
        }

        #region ҩ�����ͳ�Ʊ��������ͣ�
        /// <summary>
        /// ҩ�����ͳ�Ʊ��������ͣ�
        /// </summary>
        /// <param name="p_blnCombine">�Ƿ�Ʒ�ֲ�ѯ</param>
        /// <param name="InstorageID">�ֿ��</param>
        /// <param name="Instoragetype">����</param>
        /// <param name="dtmBegin">��ʼʱ��</param>
        /// <param name="dtmEnd">����ʱ��</param>
        /// <param name="strMedID">ҩƷID</param>
        /// <param name="strMedType">ҩƷ����</param>
        /// <param name="strProduct">����</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺҩ��ʹ��</param>
        /// <param name="dtResult">���</param>
        /// <returns></returns>
        internal long m_lngGetDrugStoreInstorageStat(bool p_blnCombine, string InstorageID, string Instoragetype, DateTime dtmBegin, DateTime dtmEnd, string strMedID,
                                      string strMedType, string strProduct, bool p_blnIsHospital, out DataTable dtResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorageStatisticsReport_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorageStatisticsReport_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorageStatisticsReport_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetDrugStoreInstorageStat(p_blnCombine, InstorageID, Instoragetype, dtmBegin, dtmEnd, strMedID, strMedType, strProduct, p_blnIsHospital, out dtResult);
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

        #region ��ȡ�����ϸ����ӡ����ҩ��ʹ�ã�
        /// <summary>
        /// ��ȡ�����ϸ����ӡ����ҩ��ʹ�ã�
        /// </summary>
        /// <param name="p_blnCombine">�Ƿ�Ʒ�ֲ�ѯ</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_dtmBegin">��ʼʱ��</param>
        /// <param name="p_dtmEnd">����ʱ��</param>
        /// <param name="p_strVendor">��Ӧ��ID</param>
        /// <param name="p_strMedTypeCode">ҩƷ����</param>
        /// <param name="p_strMedicine">ҩƷID</param>
        /// <param name="p_strType">�������ID</param>  
        /// <param name="p_blnIsHospital">�Ƿ�סԺҩ��</param>
        /// <param name="p_dtbReport">�����ϸ����</param>
        /// <returns></returns>
        internal long m_lngGetInstorageDetailReportForDrugStore(bool p_blnCombine, string p_strStorageID, DateTime p_dtmBegin, DateTime p_dtmEnd, string p_strVendor, string p_strMedTypeCode, string p_strMedicine, string p_strType, bool p_blnIsHospital, out DataTable p_dtbReport)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetInstorageDetailReportForDrugStore(p_blnCombine, p_strStorageID, p_dtmBegin, p_dtmEnd, p_strVendor, p_strMedTypeCode, p_strMedicine, p_strType, p_blnIsHospital, out p_dtbReport);
            return lngRes;
        }
        #endregion

        #region ��ȡ���ò���
        /// <summary>
        /// ��ȡ���ò���
        /// </summary>
        /// <param name="p_dtbVendor">���ò�������</param>
        /// <returns></returns>
        internal long m_lngGetExportDept(out DataTable p_dtbExportDept)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_mthGetExportDept(out p_dtbExportDept);
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

        /// <summary>
        /// ����Ƿ�סԺҩ��ʹ��
        /// </summary>
        /// <param name="p_strDrugStoreID"></param>
        /// <param name="p_blnIsHospital"></param>
        /// <returns></returns>
        internal long m_lngCheckIsHospital(string p_strDrugStoreID, out bool p_blnIsHospital)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngCheckIsHospital(p_strDrugStoreID, out p_blnIsHospital);
            return lngRes;
        }
    }
}
