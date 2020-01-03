using System;
using weCare.Core.Entity;
using System.Data;
using System.Collections;
namespace com.digitalwave.iCare.gui.HIS

{
    /// <summary>
    /// clsDcl_AccordRecipe ��ժҪ˵����
    /// </summary>
    public class clsDcl_AccordRecipe : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_AccordRecipe()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
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

        #region  ѡ����������
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

        #region ������ҩ������ϸ
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
        #region ������ҩ������ϸ
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
        #region ��������������ϸ
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
