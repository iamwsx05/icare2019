using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.common;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    #region ��ȡҩƷ�������

    public class clsDcl_RptDetailReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        /// <summary>
        /// ��ѯ�������
        /// </summary>
        /// <param name="p_blnIsDS">�Ƿ�ҩ��</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺҩ��</param>
        /// <param name="p_blnCombine">�Ƿ�Ʒ��</param>
        /// <param name="p_strStorageID">�ⷿID</param>
        /// <param name="p_strMedicineID">ҩƷID��������</param>
        /// <param name="p_dtmStart">��ʼʱ��</param>
        /// <param name="p_dtmEnd">����ʱ��</param>
        /// <param name="p_dtbResult">��ѯ���</param>
        /// <returns></returns>
        internal long m_lngGetMedicineDetailReport(bool p_blnIsDS, bool p_blnIsHospital, bool p_blnCombine, string p_strStorageID, string p_strMedicineID, DateTime p_dtmStartDate, DateTime p_dtmEndDate, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsRptSelectAllMedicine_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsRptSelectAllMedicine_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsRptSelectAllMedicine_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetMedicineDetailReport(p_blnIsDS, p_blnIsHospital, p_blnCombine, p_strStorageID, p_strMedicineID, p_dtmStartDate, p_dtmEndDate, out p_dtbResult);
            return lngRes;
        }

        internal long m_lngGetMedicineType(out DataTable dtMedType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetMedicineType(out dtMedType);
            return lngRes;
        }

        internal long m_lngGetStoreNameByID(string p_strID, out string p_strName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetStoreNameByID(p_strID, out p_strName);
            return lngRes;
        }

        internal long m_lngGetDeptIDByDrugID(string p_strId, out string p_strDeptID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetDeptIDByDrugID(p_strId, out p_strDeptID);
            return lngRes;
        }

        #region ��ȡҩƷ�������Ϣ
        /// <summary>
        /// ��ȡҩƷ�������Ϣ
        /// </summary>
        /// <param name="p_strAssistCode">��ѯ����</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_dtbMedicine">���ؽ��</param>
        /// <returns></returns>
        public long m_lngGetBaseMedicine(string p_strAssistCode, string p_strStorageID, out DataTable p_dtbMedicine)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecord_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecord_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecord_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetBaseMedicine(p_strAssistCode, p_strStorageID, out p_dtbMedicine);
            return lngRes;
        }
        #endregion

        /// <summary>
        /// �ж�ҩ���Ƿ�����סԺҩ��
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
    #endregion
}
