using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.common;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    #region ��ȡ���ݡ���������ҩȥ��ͳ�Ʊ�
    /// <summary>
    /// ��ȡ���ݡ���������ҩȥ��ͳ�Ʊ�
    /// </summary>
    /// <param name="p_objPrincipal"></param>
    /// <param name="p_strDrugID">ҩ��</param>
    /// <param name="p_dtmStartDate">��ʼ����</param>
    /// <param name="p_dtmEndDate">��������</param>
    /// <param name="p_strMedicineTypeID">ҩƷ����</param>
    /// <param name="p_dtbResult">��ѯ���</param>
    /// <returns></returns>
    public class clsDcl_RptGoWayStat : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        internal long m_lngGetGoWayStat(string p_strDrugID, DateTime p_dtmStartDate, DateTime p_dtmEndDate, string p_strMedicineTypeID, out DataTable p_dtbResult, out double p_dblSumMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetGoWayStat(  p_strDrugID, p_dtmStartDate, p_dtmEndDate, p_strMedicineTypeID, out p_dtbResult, out p_dblSumMoney);
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
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetStoreNameByID( p_strID, out p_strName);
            return lngRes;
        }

        internal long m_lngGetDeptIDByDrugID(string p_strId, out string p_strDeptID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsReportOutStorageBill_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetDeptIDByDrugID(  p_strId, out p_strDeptID);
            return lngRes;
        }
    }
    #endregion
}
