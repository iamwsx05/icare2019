using System;
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_ReckoningReport ��ժҪ˵����
    /// </summary>
    public class clsDcl_ReckoningReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_ReckoningReport()
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

        #region ��������  �Ź���  2005-1-5
        public long m_lngFindByDateReport(int p_intSelectedIndex, string p_strName, string p_strFind, out System.Data.DataTable p_tabReport, out System.Data.DataTable p_tabReportdetial)
        {
            //com.digitalwave.iCare.middletier.HIS.clsReckoningReport objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReckoningReport)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReckoningReport));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindByDateReport(p_intSelectedIndex, p_strName, p_strFind, out p_tabReport, out p_tabReportdetial);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �շѴ��½ᱨ������  �Ź���  2005-1-5
        public long m_lngChargeMnothReport(string p_strFind, string p_strFindLast, out System.Data.DataTable p_tabReport)
        {
            //com.digitalwave.iCare.middletier.HIS.clsReckoningReport objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReckoningReport)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReckoningReport));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngChargeMnothReport(p_strFind, p_strFindLast, out p_tabReport);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ӡ����  �Ź���  2004-12-28
        public long m_lngUpBALANCEFLAG(string p_strNameId, string p_strFindDate)
        {
            //com.digitalwave.iCare.middletier.HIS.clsReckoningReport objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReckoningReport)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReckoningReport));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngUpBALANCEFLAG(p_strNameId, p_strFindDate);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ҽ������  �Ź���  2005-1-5
        public long m_lngMeditionProtectReport(string p_strFindDate, string p_strFindDateLast, out System.Data.DataTable p_tabReport)
        {
            //com.digitalwave.iCare.middletier.HIS.clsReckoningReport objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReckoningReport)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReckoningReport));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngMeditionProtectReport(p_strFindDate, p_strFindDateLast, out p_tabReport);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ҽ������  �Ź���  2004-12-31
        public long m_lngPublicPayReport(int p_intFindType, string p_strPatienName, string p_strFindDate, string p_strToDate, out System.Data.DataTable p_tabReport)
        {
            //com.digitalwave.iCare.middletier.HIS.clsReckoningReport objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReckoningReport)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReckoningReport));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngPublicPayReport(p_intFindType, p_strPatienName, p_strFindDate, p_strToDate, out p_tabReport);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���Ҳ�����Ϣ
        public long m_mthFindPatientInfo(int intType, string strID, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsShowReportsSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsShowReportsSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsShowReportsSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_mthFindPatientInfo(intType, strID, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }
}
