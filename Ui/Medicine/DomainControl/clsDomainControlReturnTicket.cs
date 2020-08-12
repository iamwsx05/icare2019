using System;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControlReturnTicket 的摘要说明。
    /// </summary>
    public class clsDomainControlReturnTicket : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControlReturnTicket()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 根据窗口获得己发药数据
        /// <summary>
        /// 根据窗口获得己发药数据
        /// </summary>
        /// <param name="p_objResult">返回数据</param>
        /// <returns></returns>
        public long m_ingGetOutDataByWindowID(string p_strID, out clsOutPatienTrecipEinv_VO[] p_objResult, DateTime date)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_ingGetOutDataByWindowID(p_strID, out p_objResult, date);
            return lngRes;
        }
        #endregion


        #region 查询所有药房信息
        /// <summary>
        /// 查询所有药房信息
        /// </summary>
        /// <param name="p_objResultArr">返回数据</param>
        /// <returns></returns>
        public long m_lngGetMedStoreList(out clsMedStore_VO[] p_objResultArr)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据药房查询所有的窗口
        /// <summary>
        /// 根据药房查询所有的窗口
        /// </summary>
        /// <param name="mediD"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedStoreWinList(string mediD, out clsOPMedStoreWin_VO[] p_objResultArr)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedStoreWinList(mediD, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 通过处方ID取当前需要退药的处方记录
        /// <summary>
        /// 通过窗口ID、处方ID、处方类型取当前需要退药的处方记录
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strOPRecID">处方ID</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngGetOPRecipeListByWinAndOpRecAndType(string p_strOPRecID, string typeID, out clsOprecipeItemDe[] p_objResultArr)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetOPRecipeListByWinAndOpRecAndType(p_strOPRecID, typeID, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region  获得病人处方（状态）
        /// <summary>
        /// 获得病人处方（状态）
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strRegID">挂号ID</param>
        /// <param name="p_intStatus">状态</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngGetMainRecipe(string p_strRegID, out clsOutpatientRecipe_VO[] p_objResultArr)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMainRecipe(p_strRegID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 把门诊处方发票表的状态改为“退票”
        public long m_lngChangStatus(string p_Invoiceno)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngChangStatus(p_Invoiceno);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 把门诊处方表的状态改为“退药”
        public long m_lngReturn(string p_Invoiceno)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngReturn(p_Invoiceno);
            return lngRes;
        }
        #endregion

        #region  插入西药（成药）处方明细(退药)
        public long m_lngAddNewPwmreciPeDe(clsOutPaticntPwmreciPeDe_VO p_objRecord)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAddNewPwmreciPeDe(p_objRecord);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  插入中药处方明细(退药)
        public long m_lngAddNewCmreciPeDe(clsOutPaticntCmreciPeDe_VO p_objRecord)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAddNewCmreciPeDe(p_objRecord);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  插入中药处方明细(退药)
        public long m_ingfFidData(string p_strID, out System.Data.DataTable p_objResultArr)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_ingfFidData(p_strID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion


    }
}
