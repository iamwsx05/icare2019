using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.common;
//using com.digitalwave.iCare.middletier.HIS;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{


    #region ��ȡ����ҩƷ����Ʒ�ֽ��ͳ�Ʊ�
    /// <summary>
    /// ��ȡ����ҩƷ����Ʒ�ֽ��ͳ�Ʊ�
    /// </summary>
    /// <param name="p_objPrincipal"></param>
    /// <param name="p_strDrugID">ҩ��</param>
    /// <param name="p_dtmStartDate">��ʼ����</param>
    /// <param name="p_dtmEndDate">��������</param>
    /// <param name="p_strMedicineTypeID">ҩƷ����</param>
    /// <param name="p_dtbResult">��ѯ���</param>
    /// <returns></returns>
    public class clsDcl_RptRecipeOutSumStat : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        /// <summary>
        /// �ϲ�����ҩ������ҩƷ����סԺ����ҩ����ҩƷ���Ľ��ͳ��[�����ر�����] 2008-10-05 wuchongkun
        /// </summary>
        /// <param name="p_dtmStartDate"></param>
        /// <param name="p_dtmEndDate"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        internal long m_lngGetUnionMedSumStat(DateTime p_dtmStartDate, DateTime p_dtmEndDate, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetUnionMedicineSumStat(  p_dtmStartDate, p_dtmEndDate, out p_dtbResult);
            return lngRes;
        }

        internal long m_lngGetRecipeOutSumStat(string p_strDrugID, DateTime p_dtmStartDate, DateTime p_dtmEndDate, string p_strMedicineTypeID, string p_strMedicineID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetRecipeOutSumStat(  p_strDrugID, p_dtmStartDate, p_dtmEndDate, p_strMedicineTypeID, p_strMedicineID, out p_dtbResult);
            return lngRes;
        }
        /// <summary>
        /// סԺҩ��ҽ����ҩ���ͳ�� 
        /// </summary>
        /// <param name="p_strDrugID">ҩ��ID</param>
        /// <param name="p_dtmStartDate">��ʼʱ��</param>
        /// <param name="p_dtmEndDate">����ʱ��</param>
        /// <param name="p_strMedicineTypeID">ҩƷ����</param>
        /// <param name="p_strMedicineID">ҩƷid </param>
        /// <param name="p_dtbResult">���ؽ��</param>
        /// <returns></returns>
        internal long m_lngGetPutMedicineSumStat(string p_strCenterStorageID, DateTime p_dtmStartDate, DateTime p_dtmEndDate, string p_strMedicineTypeID, string p_strMedicineID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetPutMedicineSumStat(  p_strCenterStorageID, p_dtmStartDate, p_dtmEndDate, p_strMedicineTypeID, p_strMedicineID, out p_dtbResult);
            return lngRes;
        }

        internal long m_lngGetMedicineType(out DataTable dtMedType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetMedicineType(out dtMedType);
            return lngRes;
        }

        internal long m_lngGetStoreNameByID(string p_strID, out string p_strName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetStoreNameByID(  p_strID, out p_strName);
            return lngRes;
        }

        internal long m_lngGetDeptIDByDrugID(string p_strId, out string p_strDeptID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBillSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetDeptIDByDrugID(  p_strId, out p_strDeptID);
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
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecordSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecordSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsInventoryRecordSVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetBaseMedicine( p_strAssistCode, p_strStorageID, out p_dtbMedicine);
            return lngRes;
        }
        #endregion

        /// <summary>
        /// ��ȡ����ҩƷ��Ϣ
        /// </summary>
        /// <param name="m_dtMedicine"></param>
        /// <returns></returns>
        public long m_mthGetMedBaseInfo(string m_strMedStoreid, out DataTable m_dtMedicine)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsAskForMedicine_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsAskForMedicine_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAskForMedicine_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetMedicineInfo( m_strMedStoreid, out m_dtMedicine);
            return lngRes;
        }
    }
    #endregion
}
