using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore
{
    public class clsDcl_Purchase_DetailReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 打印时是否显示额外信息
        /// <summary>
        /// 打印时是否显示额外信息
        /// </summary>
        /// <param name="p_intShowInfo">是否显示额外信息帐</param>
        /// <returns></returns>
        internal long m_lngGetIfShowInfo(out int p_intShowInfo)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetSysSetting("5029", out p_intShowInfo);
            return lngRes;
        }
        #endregion
    }
}
