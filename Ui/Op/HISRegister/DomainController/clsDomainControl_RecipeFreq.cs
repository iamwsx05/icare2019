using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControl_RecipeFreq 的摘要说明。
    /// </summary>
    public class clsDomainControl_RecipeFreq : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControl_RecipeFreq()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        //用药频率
        #region 获取用药频率列表	张国良		2004-8-11
        public long m_lngFindRecipeFrequencyeList(out clsRecipefreq_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindRecipeFrequencyTypeList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 新增用药频率	张国良		2004-8-11
        public long m_lngAddRecipeFrequencyType(clsRecipefreq_VO p_objResult, out string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngAddNewRecipeFrequencyType(p_objResult, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 修改用药频率	张国良		2004-8-11
        public long m_lngDoUpdRecipeFrequencyTypeByID(clsRecipefreq_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDoUpdRecipeFrequencyByID(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 删除用药频率	张国良		2004-8-11
        public long m_lngDelTRecipeFrequencyTypeByID(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDeleteRecipeFrequencyByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }
}
