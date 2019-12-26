using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.common;
 
namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    #region �������ⰴ��λƷ��ͳ��
    /// <summary>
    /// �������ⰴ��λƷ��ͳ��
    /// </summary>
    /// <param name="p_objPrincipal"></param>
    /// <param name="p_strDrugID">ҩ��</param>
    /// <param name="p_dtmStartDate">��ʼ����</param>
    /// <param name="p_dtmEndDate">��������</param>
    /// <param name="p_dtbResult">��ѯ���</param>
    /// <returns></returns>
    public class clsDcl_RecipeByMedicineDeptReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        internal long m_lngGetRecipeByMedicineDeptReport(string p_strDrugID, string p_strDeptID, string p_strMedicineId, DateTime p_dtmStartDate, DateTime p_dtmEndDate, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsRecipeReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsRecipeReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsRecipeReportSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngRecipeByMedicineDeptReport(p_strDrugID, p_strDeptID, p_strMedicineId, p_dtmStartDate, p_dtmEndDate, out p_dtbResult);
            return lngRes;
        }

        #region ��ȡ���ò���
        /// <summary>
        /// ��ȡ���ò���
        /// </summary>
        /// <param name="p_dtbVendor">���ò�������</param>
        /// <returns></returns>
        internal long m_lngGetExportDeptForDrugStore(out DataTable p_dtbExportDept)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetExportDeptForDrugStore(out p_dtbExportDept);
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
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetMedicineInfo(m_strMedStoreid, out m_dtMedicine);
            return lngRes;
        }
    }
    #endregion
}
