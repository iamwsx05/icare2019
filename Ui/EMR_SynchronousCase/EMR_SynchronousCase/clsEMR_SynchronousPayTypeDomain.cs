using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace com.digitalwave.emr.EMR_SynchronousCase
{
    /// <summary>
    /// 费别同步
    /// </summary>
    public class clsEMR_SynchronousPayTypeDomain : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 获取费别列表

        /// <summary>
        /// 获取费别列表
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetRelationPayTypeList(out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;

            //clsEMR_SynchronousPayTypeServ objServ =
            //       (clsEMR_SynchronousPayTypeServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousPayTypeServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetRelationPayTypeList(out p_dtbResult);
            return lngRes;
        }

        /// <summary>
        /// 获取iCare费别列表
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetICarePayTypeList(out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;

            //clsEMR_SynchronousPayTypeServ objServ =
            //       (clsEMR_SynchronousPayTypeServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousPayTypeServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetICarePayTypeList(out p_dtbResult);
            return lngRes;
        }

        /// <summary>
        /// 获取病案系统费别列表
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetBAPayTypeList(out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;

            //clsEMR_SynchronousPayTypeServ objServ =
            //       (clsEMR_SynchronousPayTypeServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousPayTypeServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetBAPayTypeList(out p_dtbResult);
            return lngRes;
        }
        #endregion

        #region 保存费别设置
        /// <summary>
        /// 保存费别设置
        /// </summary>
        /// <param name="p_strBAPayTypeID">病案费别ID</param>
        /// <param name="p_strICarePayTypeID">与病案费别ID对应的iCare系统费别ID</param>
        /// <returns></returns>
        public long m_lngSavePayTypeConfig(string p_strBAPayTypeID, string[] p_strICarePayTypeID)
        {
            long lngRes = 0;

            //clsEMR_SynchronousPayTypeServ objServ =
            //       (clsEMR_SynchronousPayTypeServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousPayTypeServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngSavePayTypeConfig(p_strBAPayTypeID, p_strICarePayTypeID);
            return lngRes;
        }
        #endregion

        #region 获取相关的病案系统费别

        /// <summary>
        /// 获取相关的病案系统费别

        /// </summary>
        /// <param name="p_strPayTypeID">iCare系统费别</param>
        /// <param name="p_strBA_PayTypeID">病案系统费别</param>
        /// <returns></returns>
        public long m_lngGetBAPayType(string p_strPayTypeID, out string p_strBA_PayTypeID)
        {
            long lngRes = 0;

            //clsEMR_SynchronousPayTypeServ objServ =
            //       (clsEMR_SynchronousPayTypeServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousPayTypeServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetBAPayType(p_strPayTypeID, out p_strBA_PayTypeID);
            return lngRes;
        }
        #endregion

        #region 获取相关的iCare系统费别
        /// <summary>
        /// 获取相关的iCare系统费别
        /// </summary>
        /// <param name="p_strBA_PayTypeID">病案系统费别</param>
        /// <param name="p_strPayTypeIDArr">iCare系统费别</param>
        /// <returns></returns>
        public long m_lngGetICarePayType(string p_strBA_PayTypeID, out string[] p_strPayTypeIDArr)
        {
            long lngRes = 0;

            //clsEMR_SynchronousPayTypeServ objServ =
            //       (clsEMR_SynchronousPayTypeServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousPayTypeServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetICarePayType(p_strBA_PayTypeID, out p_strPayTypeIDArr);
            return lngRes;
        }
        #endregion
    }
}
