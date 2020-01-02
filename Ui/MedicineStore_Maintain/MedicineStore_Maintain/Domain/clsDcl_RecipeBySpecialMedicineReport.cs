using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.common; 

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    #region �顢��ҩƷ����������ϸ��
    /// <summary>
    /// �顢��ҩƷ����������ϸ��
    /// </summary>
    /// <param name="p_objPrincipal"></param>
    /// <param name="p_strDrugID">ҩ��</param>
    /// <param name="p_dtmStartDate">��ʼ����</param>
    /// <param name="p_dtmEndDate">��������</param>
    /// <param name="p_intType">����ҩƷ������</param> 
    /// <param name="p_dtbResult">��ѯ���</param>
    /// <returns></returns>
    public class clsDcl_RecipeBySpecialMedicineReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        internal long m_lngGetRecipeBySpecialMedicine(string p_strDrugID, DateTime p_dtmStartDate, DateTime p_dtmEndDate, int p_intType, string p_strMedicineId, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsRecipeReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsRecipeReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsRecipeReportSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetRecipeBySpecialMedicine(p_strDrugID, p_dtmStartDate, p_dtmEndDate, p_intType, p_strMedicineId, out p_dtbResult);
            return lngRes;
        }

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
