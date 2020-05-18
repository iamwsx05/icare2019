using System;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_WaitDiagListManage 的摘要说明。
    /// </summary>
    public class clsDcl_WaitDiagListManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_WaitDiagListManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 获取员工所属部门
        public long m_mthGetDepartmentByID(string strEmpID, out DataTable p_dt)
        {
            p_dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_mthGetDepartmentByID(  strEmpID, out p_dt);
        }
        #endregion

        #region 根据部门ID查找当天排班医生,
        public long m_mthGetDocByDepID(string ID, out DataTable p_dt)
        {
            p_dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_mthGetDocByDepID(  ID, out p_dt);

        }
        #endregion
        #region 根据部门ID和医生ID查找候诊病人
        public long m_mthGetWaitListByID(string strDocID, string strDepID, out DataTable p_dt, DateTime date, DateTime date2)
        {
            p_dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_mthGetWaitListByID(  strDocID, strDepID, out p_dt, date, date2);

        }
        #endregion
        #region 插队
        /// <summary>
        /// 插队
        /// </summary>
        /// <param name="strDocID">医生ID</param>
        /// <param name="strDepID">部门ID</param>
        /// <param name="rowNo">候诊队号</param>
        /// <param name="strListID">候诊ID(唯一)</param>
        /// <returns></returns>
        public long m_mthPrecedence(string strDocID, string strDepID, int rowNo, string strListID)
        {

            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_mthPrecedence(  strDocID, strDepID, rowNo, strListID);

        }
        #endregion
        #region 更改医生

        public long m_mthChangeDoc(string strDepID, string strDocID, string strListID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_mthChangeDoc( strDocID, strDepID, strListID);

        }
        #endregion
        #region 根据时间段和员工ID查出候诊病人
        /// <summary>
        ///  根据时间段和员工ID查出候诊病人
        /// </summary>
        /// <param name="strDocName">医生名称模糊查询</param>
        /// <param name="strDepID">部门ID</param>
        /// <param name="p_dt"></param>
        /// <param name="date"></param>
        /// <param name="date2"></param>
        /// <param name="strEmpID">员工ID</param>
        /// <param name="flag">0新建,1处理</param>
        /// <returns></returns>
        public long m_mthGetWaitListInfoByID(string strDocName, string strDepID, out DataTable p_dt, DateTime date, DateTime date2, string strEmpID, int flag)
        {
            p_dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_mthGetWaitListInfoByID(strDocName, strDepID, out p_dt, date, date2, strEmpID, flag);
        }
        #endregion
        #region 转位置
        public long m_mthMoveOrder(string row1, string row2, string ID1, string ID2)
        {
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc = 
            //	(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_mthMoveOrder(row1, row2, ID1, ID2);
        }
        #endregion
    }
}
