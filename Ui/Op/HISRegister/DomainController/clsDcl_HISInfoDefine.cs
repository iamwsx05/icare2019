using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_HISInfoDefine 的摘要说明。
    /// </summary>
    public class clsDcl_HISInfoDefine : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_HISInfoDefine()
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

        //医院基本信息
        #region 获取医院基本信息	张国良		2004-8-12
        public long m_lngFindHospitalInfo(out clsHISInfoDefine_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc));
            lngRes = proxy.Service.m_lngFindHospitalInfo(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 修改医院基本信息	张国良		2004-8-12
        public long m_lngDoUpdHospitalInfo(clsHISInfoDefine_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc));
            lngRes = proxy.Service.m_lngDoUpdHospitalInfo(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        // 应用管理系统
        #region 查询应用管理系统 created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// 查询应用管理系统
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindModuleList(out clsHISModuleDef_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //clsHISInfoDefineSvc objSvc = (clsHISInfoDefineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISInfoDefineSvc));
            lngRes = proxy.Service.m_lngFindModuleList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 新增分系统 created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// 新增分系统
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strEngName"></param>
        /// <param name="strPYCode"></param>
        /// <param name="strWBCode"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_lngAddModule(string strModuleName, string strEngName, string strPYCode, string strWBCode, out string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc));
            lngRes = proxy.Service.m_lngDoAddNewModule(strModuleName, strEngName, strPYCode, strWBCode, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改分系统信息 created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// 修改分系统信息
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngDoUpdModuleByID(clsHISModuleDef_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc));
            lngRes = proxy.Service.m_lngDoUpdModuleByID(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除分系统 created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// 删除分系统
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_lngDelModuleByID(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc));
            lngRes = proxy.Service.m_lngDelModuleByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        // 查询系统错误信息记录
        #region 查询系统错误信息记录 created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// 查询系统错误信息记录
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindErrorLogList(out DataTable p_dtbResult)
        // not use value object which makes it works faster
        {
            long lngRes = 0;
            //clsHISInfoDefineSvc objSvc = (clsHISInfoDefineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISInfoDefineSvc));
            lngRes = proxy.Service.m_lngFindErrorLogList(out p_dtbResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除所有系统错误信息记录 created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// 删除所有系统错误信息记录
        /// </summary>
        /// <returns></returns>
        public long m_lngDelAllErrorLog()
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc));
            lngRes = proxy.Service.m_lngDelAllErrorLog();
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion


        // 查询用户登陆信息

        #region 查询用户登陆信息 created by Cameron Wong on Aug 16, 2004
        /// <summary>
        /// 查询用户登陆信息
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindLoginInfoList(out DataTable p_dtbResult)
        // not use value object which makes it works faster
        {
            long lngRes = 0;
            //clsHISInfoDefineSvc objSvc = (clsHISInfoDefineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISInfoDefineSvc));
            lngRes = proxy.Service.m_lngFindLoginInfoList(out p_dtbResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除用户登陆信息 created by Cameron Wong on Aug 16, 2004
        /// <summary>
        /// 删除用户登陆信息
        /// </summary>
        /// <returns></returns>
        public long m_lngDelAllLoginInfo()
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHISInfoDefineSvc));
            lngRes = proxy.Service.m_lngDelAllLoginInfo();
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion


    }
}
