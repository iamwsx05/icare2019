using System;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.DLL
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainConrolCheck 的摘要说明。
    /// </summary>
    public class clsDomainConrolCheck : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainConrolCheck()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 药品入库价查询模块
        /// <summary>
        /// 入库价格查询
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngOrdInPriceCheck(out DataTable dt, string strmedTypeID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsCheckSVC objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsCheckSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsCheckSVC));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngOrdInPriceCheck(out dt, strmedTypeID);
            return lngRes;
        }
        /// <summary>
        /// 获取药品类型
        /// </summary>
        /// <param name="dtMedType"></param>
        /// <returns></returns>
        public long m_lngGetMedType(out DataTable dtMedType)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsCheckSVC objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsCheckSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsCheckSVC));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedType(out dtMedType);
            return lngRes;
        }
        /// <summary>
        /// 根据药品ID获取入库明细数据
        /// </summary>
        /// <param name="strmedID"></param>
        /// <param name="dtDe"></param>
        /// <returns></returns>
        public long m_lngGetMedDe(string strmedID, out DataTable dtDe)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsCheckSVC objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsCheckSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsCheckSVC));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedDe(strmedID, out dtDe);
            return lngRes;
        }
        #endregion
    }
}
