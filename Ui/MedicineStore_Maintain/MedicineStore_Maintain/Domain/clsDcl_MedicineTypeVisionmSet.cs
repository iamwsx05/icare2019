using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// ҩƷ������ʾ����
    /// </summary>
    public class clsDcl_MedicineTypeVisionmSet : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        internal long m_lngGetAllMedicineTypeVisionm(out DataTable p_objDtb)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineTypeVisionmSetSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineTypeVisionmSetSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineTypeVisionmSetSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetAllMedicineTypeVisionm( out p_objDtb);
            return lngRes;
        }

        internal long m_lngSaverMedicineType(clsMS_MedicineTypeVisionmSet[] objMedicineType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineTypeVisionmSetSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineTypeVisionmSetSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineTypeVisionmSetSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngSaverMedicineType( objMedicineType);
            return lngRes;
        }
    }

}
