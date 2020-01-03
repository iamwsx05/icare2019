using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_HISInfoDefine ��ժҪ˵����
    /// </summary>
    public class clsDcl_HISInfoDefine : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_HISInfoDefine()
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

        //ҽԺ������Ϣ
        #region ��ȡҽԺ������Ϣ	�Ź���		2004-8-12
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
        #region �޸�ҽԺ������Ϣ	�Ź���		2004-8-12
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

        // Ӧ�ù���ϵͳ
        #region ��ѯӦ�ù���ϵͳ created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// ��ѯӦ�ù���ϵͳ
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

        #region ������ϵͳ created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// ������ϵͳ
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

        #region �޸ķ�ϵͳ��Ϣ created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// �޸ķ�ϵͳ��Ϣ
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

        #region ɾ����ϵͳ created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// ɾ����ϵͳ
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

        // ��ѯϵͳ������Ϣ��¼
        #region ��ѯϵͳ������Ϣ��¼ created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// ��ѯϵͳ������Ϣ��¼
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

        #region ɾ������ϵͳ������Ϣ��¼ created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// ɾ������ϵͳ������Ϣ��¼
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


        // ��ѯ�û���½��Ϣ

        #region ��ѯ�û���½��Ϣ created by Cameron Wong on Aug 16, 2004
        /// <summary>
        /// ��ѯ�û���½��Ϣ
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

        #region ɾ���û���½��Ϣ created by Cameron Wong on Aug 16, 2004
        /// <summary>
        /// ɾ���û���½��Ϣ
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
