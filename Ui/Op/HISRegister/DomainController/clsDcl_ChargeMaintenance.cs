using System;
using weCare.Core.Entity;
using System.Data;
using System.Collections;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_ChargeMaintenance 的摘要说明。
    /// </summary>
    public class clsDcl_ChargeMaintenance : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_ChargeMaintenance()
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

        #region 获取病人分类的列信息
        public void m_mthGetPatientCatInfo(out DataTable p_dt)
        {
            p_dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeMaintenanceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeMaintenanceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeMaintenanceSvc));
            proxy.Service.m_mthGetPatientCatInfo(out p_dt);
            //objSvc.Dispose();

        }
        #endregion
        #region 查找收费项目
        public long m_mthFindChargeItem(string strType, string ID, out DataTable p_dt, string strCatID)
        {
            p_dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeMaintenanceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeMaintenanceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeMaintenanceSvc));
            long lngRes = proxy.Service.m_mthFindChargeItem(strType, ID, out p_dt, strCatID);
            //objSvc.Dispose();
            return lngRes;

        }
        #endregion
        #region 更新数据
        public void m_mthUpdateData(string strItemID, string strCopayID, string strValue)
        {
            //com.digitalwave.iCare.middletier.HIS.clsChargeMaintenanceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeMaintenanceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeMaintenanceSvc));
            proxy.Service.m_mthUpdateData(strItemID, strCopayID, strValue);
            //objSvc.Dispose();
        }
        #endregion
        #region
        public long m_mthFindData(string ID, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccordRecipeSvc));
            long lngRes = proxy.Service.m_mthFindDiscountByID(ID, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }
}
