using System;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// Class1 的摘要说明。
    /// </summary>
    public class clsDomainControlCompoundReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControlCompoundReport()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 获取药品种类组成列表
        public long m_lngGetStorageList(out DataTable dt, string p_strType)
        {
            dt = null;
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsCompoundReport objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsCompoundReport)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsCompoundReport));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetData(out dt, p_strType);
            return lngRes;
        }

        #endregion
    }
}
