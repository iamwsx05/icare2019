using System;
using weCare.Core.Entity;
using System.Data;
using System.Collections;
namespace com.digitalwave.iCare.gui.HIS

{
    /// <summary>
    /// clsDcl_AccordRecipe 的摘要说明。
    /// </summary>
    public class clsDcl_AccordRecipe : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_AccordRecipe()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP();
            }
        }
        #endregion

        #region  选择主处方号
        public long m_mthGetAccordRecipeDetail(string ID, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc));
            long lngRes = proxy.Service.m_mthGetAccordRecipeDetail(ID, out dt);
            //objSvc.Dispose();
            return lngRes;

        }
        #endregion

        #region 查找西药处方明细
        public long m_mthFindWMRecipeDetail(string ID, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc));
            long lngRes = proxy.Service.m_mthFindWMRecipeDetail(ID, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查找中药处方明细
        public long m_mthFindCMRecipeDetail(string ID, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc));
            long lngRes = proxy.Service.m_mthFindCMRecipeDetail(ID, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查找其他处方明细
        public long m_mthFindOtherRecipeDetail(string ID, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc));
            long lngRes = proxy.Service.m_mthFindOtherRecipeDetail(ID, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }
}
