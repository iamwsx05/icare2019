using System;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// Class1 ��ժҪ˵����
    /// </summary>
    public class clsDomainControlCompoundReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControlCompoundReport()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ��ȡҩƷ��������б�
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
