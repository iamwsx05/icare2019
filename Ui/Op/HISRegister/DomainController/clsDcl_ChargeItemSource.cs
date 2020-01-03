using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{/// <summary>
 /// clsDcl_ChargeItemSource 的摘要说明。
 /// </summary>
    public class clsDcl_ChargeItemSource : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_ChargeItemSource()
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

        #region 查询收费项目分类类型
        public long m_mthFindCat(out clsCharegeItemCat_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSourceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSourceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSourceSvc));
            lngRes = proxy.Service.m_lngFindChargeItemCatList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询收费项目

        public long m_mthFindChargeItem(string strCatID, string strType, string strContent, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSourceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSourceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSourceSvc));
            long lngRes = proxy.Service.m_mthFindChargeItem(strCatID, strType, strContent, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询项目源

        public long m_mthFindChargeItemSource(string strType, out DataTable dt2, string strWhere)
        {
            dt2 = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSourceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSourceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSourceSvc));
            long lngRes = proxy.Service.m_lngFindAllSour(strType, out dt2, strWhere);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 保存数据

        public long m_mthSaveData(string strItemID, string strSourceID, string strSourceName, string strSourceCatID, string strCatName)
        {

            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSourceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSourceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSourceSvc));
            long lngRes = proxy.Service.m_mthSaveData(strItemID, strSourceID, strSourceName, strSourceCatID, strCatName);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }
}
