using System;
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_ReckoningReport 的摘要说明。
    /// </summary>
    public class clsDcl_StstemSeting : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_StstemSeting()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 获得模块信息  xigui.peng 2005-12-3

        public long m_lngGetLeftInfo(out DataTable p_dtInfo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsStstemSetingSvc objSvc=
            // (com.digitalwave.iCare.middletier.HIS.clsStstemSetingSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStstemSetingSvc));
            long lngRegs = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetLeftInfo(out p_dtInfo);
            //objSvc.Dispose();
            return lngRegs;
        }
        #endregion
        #region 系统功能配置列表  张国良  2005-2-23
        public long m_lngStstemSeting(out System.Data.DataTable p_tabReport)
        {
            //com.digitalwave.iCare.middletier.HIS.clsStstemSetingSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStstemSetingSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStstemSetingSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngStstemSeting(out p_tabReport);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改状态  张国良  2005-2-23
        public long m_lngModifyStstemSeting(string p_strID, string p_strStatue, string p_strModuleID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsStstemSetingSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStstemSetingSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStstemSetingSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngModifyStstemSeting(p_strID, p_strStatue, p_strModuleID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

    }
}