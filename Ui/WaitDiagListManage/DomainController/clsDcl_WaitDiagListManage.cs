using System;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_WaitDiagListManage ��ժҪ˵����
    /// </summary>
    public class clsDcl_WaitDiagListManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_WaitDiagListManage()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ��ȡԱ����������
        public long m_mthGetDepartmentByID(string strEmpID, out DataTable p_dt)
        {
            p_dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_mthGetDepartmentByID(  strEmpID, out p_dt);
        }
        #endregion

        #region ���ݲ���ID���ҵ����Ű�ҽ��,
        public long m_mthGetDocByDepID(string ID, out DataTable p_dt)
        {
            p_dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_mthGetDocByDepID(  ID, out p_dt);

        }
        #endregion
        #region ���ݲ���ID��ҽ��ID���Һ��ﲡ��
        public long m_mthGetWaitListByID(string strDocID, string strDepID, out DataTable p_dt, DateTime date, DateTime date2)
        {
            p_dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_mthGetWaitListByID(  strDocID, strDepID, out p_dt, date, date2);

        }
        #endregion
        #region ���
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="strDocID">ҽ��ID</param>
        /// <param name="strDepID">����ID</param>
        /// <param name="rowNo">����Ӻ�</param>
        /// <param name="strListID">����ID(Ψһ)</param>
        /// <returns></returns>
        public long m_mthPrecedence(string strDocID, string strDepID, int rowNo, string strListID)
        {

            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_mthPrecedence(  strDocID, strDepID, rowNo, strListID);

        }
        #endregion
        #region ����ҽ��

        public long m_mthChangeDoc(string strDepID, string strDocID, string strListID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_mthChangeDoc( strDocID, strDepID, strListID);

        }
        #endregion
        #region ����ʱ��κ�Ա��ID������ﲡ��
        /// <summary>
        ///  ����ʱ��κ�Ա��ID������ﲡ��
        /// </summary>
        /// <param name="strDocName">ҽ������ģ����ѯ</param>
        /// <param name="strDepID">����ID</param>
        /// <param name="p_dt"></param>
        /// <param name="date"></param>
        /// <param name="date2"></param>
        /// <param name="strEmpID">Ա��ID</param>
        /// <param name="flag">0�½�,1����</param>
        /// <returns></returns>
        public long m_mthGetWaitListInfoByID(string strDocName, string strDepID, out DataTable p_dt, DateTime date, DateTime date2, string strEmpID, int flag)
        {
            p_dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_mthGetWaitListInfoByID(strDocName, strDepID, out p_dt, date, date2, strEmpID, flag);
        }
        #endregion
        #region תλ��
        public long m_mthMoveOrder(string row1, string row2, string ID1, string ID2)
        {
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc = 
            //	(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_mthMoveOrder(row1, row2, ID1, ID2);
        }
        #endregion
    }
}
