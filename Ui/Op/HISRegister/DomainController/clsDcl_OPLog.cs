using System;
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_OPLog 的摘要说明。
    /// </summary>
    public class clsDcl_OPLog : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_OPLog()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public long m_mthLogData(out DataTable dt, string strEx, string strICDCode)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPLogSvc objSvc = 
            //	(com.digitalwave.iCare.middletier.HIS.clsOPLogSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPLogSvc));
            long lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_mthLogData(out dt, strEx, strICDCode);
            //objSvc.Dispose();
            return lngRes;
        }
    }
}
